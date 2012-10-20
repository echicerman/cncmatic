﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandPreprocessor
{
    public class CommandPreprocessor
    {
        #region Properties
        private WorkingPlane workingPlane;
        private Position currentPosition;
        private Position referencePosition; // Origen de la pieza a fresar
        #endregion

        #region Constructor
        private static CommandPreprocessor instance;
        protected CommandPreprocessor()
        {
            this.workingPlane = WorkingPlane.XY;
            this.currentPosition = new Position(0, 0, 0);
            this.referencePosition = new Position(0, 0, 0);

            Configuration.absoluteProgamming = true;
            Configuration.millimetersProgramming = true;
            Configuration.millimetersCurveSection = 0.5;
            Configuration.defaultFeedrate = 60;
        }
        public static CommandPreprocessor GetInstance()
        {
            if (instance == null)
            {
                instance = new CommandPreprocessor();
            }
            return instance;
        }
        #endregion

        #region Getters & Setter
        public WorkingPlane WorkingPlane
        {
            get { return this.workingPlane; }
            set { this.workingPlane = value; }
        }
        public Position CurrentPosition
        {
            get { return this.currentPosition; }
            set { this.currentPosition = value; }
        }
        public Position ReferencePosition
        {
            get { return this.referencePosition; }
            set { this.referencePosition = value; }
        }
        #endregion

        #region Private Methods
        private double GetValueParameter(char parameterName, string command)
        {
            foreach(string parameter in command.Split(' '))
            {
                if (parameter[0] == parameterName)
                { 
                    return double.Parse(parameter.Substring(1));
                }
            }
            return 0;
        }
        private bool HasValueParameter(char parameterName, string command)
        {
            return command.IndexOf(parameterName) != -1;
        }

        private Position GetFinalPosition(string command)
        {
            Position result = new Position();

            if (Configuration.absoluteProgamming)
            {
                result.X = GetValueParameter('X', command) + this.ReferencePosition.X;
                result.Y = GetValueParameter('Y', command) + this.ReferencePosition.Y;
                result.Z = GetValueParameter('Z', command) + this.ReferencePosition.Z;
            }
            else
            {
                // Translate to absolute programming mode
                result.X = GetValueParameter('X', command) + this.CurrentPosition.X;
                result.Y = GetValueParameter('Y', command) + this.CurrentPosition.Y;
                result.Z = GetValueParameter('Z', command) + this.CurrentPosition.Z;
            }

            // If the values are in inches, convert to millimeters
            if (!Configuration.millimetersProgramming)
            {
                result.X *= 25.4;
                result.Y *= 25.4;
                result.Z *= 25.4;
            }

            return result;
        }
        private Position GetFromRadius(double r, string command)
        {
            double midX, midY, midZ, deltaX, deltaY, deltaZ, xToRadius, yToRadius, zToRadius;
            Position final = GetFinalPosition(command);
            Position centerPosition = new Position();
            centerPosition.X = final.X > CurrentPosition.X ? CurrentPosition.X : final.X;
            centerPosition.Y = final.Y > CurrentPosition.Y ? CurrentPosition.Y : final.Y;
            centerPosition.Z = final.Z > CurrentPosition.Z ? CurrentPosition.Z : final.Z;

            switch (this.WorkingPlane)
            { 
                case WorkingPlane.XY:
                    deltaZ = Math.Abs(final.Z - CurrentPosition.Z) / 2;
                    midY = Math.Abs(final.Y - CurrentPosition.Y) / 2;
                    midX = Math.Abs(final.X - CurrentPosition.X) / 2;
                    xToRadius = Math.Sqrt(r * r - midY * midY);

                    centerPosition.X += r > 0 ? midX + xToRadius : midX - xToRadius;
                    centerPosition.Y += midY;
                    centerPosition.Z += deltaZ;
                    return centerPosition;

                case WorkingPlane.XZ:
                    deltaY = Math.Abs(final.Y - CurrentPosition.Y) / 2;
                    midX = Math.Abs(final.X - CurrentPosition.X) / 2;
                    midZ = Math.Abs(final.Z - CurrentPosition.Z) / 2;
                    zToRadius = Math.Sqrt(r * r - midX * midX);

                    centerPosition.X += midX;
                    centerPosition.Y += deltaY;
                    centerPosition.Z += r > 0 ? midZ + zToRadius : midZ - zToRadius;
                    return centerPosition;

                case WorkingPlane.YZ:
                    deltaX = Math.Abs(final.X - CurrentPosition.X) / 2;
                    midZ = Math.Abs(final.Z - CurrentPosition.Z) / 2;
                    midY = Math.Abs(final.Y - CurrentPosition.Y) / 2;
                    yToRadius = Math.Sqrt(r * r - midZ * midZ);

                    centerPosition.X += deltaX;
                    centerPosition.Y += r > 0 ? midY + yToRadius : midY - yToRadius;
                    centerPosition.Z += midZ;
                    return centerPosition;
            }
            //return 0 ... ( no deberia llegar nunca aca )
            return centerPosition;
        }
        private Position GetCenterPosition(string command)
        {
            Position result = new Position();

            if (HasValueParameter('R', command))
            {
                double r = GetValueParameter('R', command);
                result = GetFromRadius(r, command);
            }
            else
            {
                // The center point is relative to the start position... Translate to absolute programming mode
                result.X = GetValueParameter('I', command) + this.CurrentPosition.X;
                result.Y = GetValueParameter('J', command) + this.CurrentPosition.Y;
                result.Z = GetValueParameter('K', command) + this.CurrentPosition.Z;
            }

            return result;
        }

        private List<string> ProcessCurvePlaneXY(string code)
        {
            List<string> result = new List<string>();
            double feedRate = HasValueParameter('F', code) ? GetValueParameter('F', code) : Configuration.defaultFeedrate; // Ver qué valor va cuando no hay valor - CONFIGURACION? -

            Position startPosition = this.CurrentPosition;
            Position finalPosition = this.GetFinalPosition(code);
            Position centerPosition = this.GetCenterPosition(code);
            int gCode = Convert.ToInt32(GetValueParameter('G', code));
            bool clockwise = gCode == 2 ? true : false;

            double aX, aY, bX, bY, angleA, angleB, angle, radius, length;
            int steps;

            aX = currentPosition.X - centerPosition.X;
            aY = currentPosition.Y - centerPosition.Y;
            bX = finalPosition.X - centerPosition.X;
            bY = finalPosition.Y - centerPosition.Y;

            angleA = clockwise ? Math.Atan2(bY, bX) : Math.Atan2(aY, aX);
            angleB = clockwise ? Math.Atan2(aY, aX) : Math.Atan2(bY, bX);

            if (angleB <= angleA) angleB += 2 * Math.PI;
            angle = angleB - angleA;

            radius = Math.Sqrt(aX * aX + aY * aY);
            length = radius * angle;
            steps = (int)(length / Configuration.millimetersCurveSection);

            Position sectionPosition = new Position();
            for (int s = 1; s <= steps; s++)
            {
                int step = clockwise ? steps - s : s;
                sectionPosition.X = centerPosition.X + radius * Math.Cos(angleA + angle * ((float)step / steps));
                sectionPosition.Y = centerPosition.Y + radius * Math.Sin(angleA + angle * ((float)step / steps));
                sectionPosition.Z = (finalPosition.Z - startPosition.Z) * ((float)s / steps);
                result.Add(sectionPosition.ToString(gCode) + string.Format("F{0} ", feedRate));
            }

            return result;
        }
        private List<string> ProcessCurvePlaneXZ(string code)
        {
            List<string> result = new List<string>();
            double feedRate = HasValueParameter('F', code) ? GetValueParameter('F', code) : Configuration.defaultFeedrate; // Ver qué valor va cuando no hay valor - CONFIGURACION? -

            Position startPosition = this.CurrentPosition;
            Position finalPosition = this.GetFinalPosition(code);
            Position centerPosition = this.GetCenterPosition(code);
            int gCode = Convert.ToInt32(GetValueParameter('G', code));
            bool clockwise = gCode == 2 ? true : false;

            double aX, aZ, bX, bZ, angleA, angleB, angle, radius, length;
            int steps;

            aX = currentPosition.X - centerPosition.X;
            aZ = currentPosition.Z - centerPosition.Z;
            bX = finalPosition.X - centerPosition.X;
            bZ = finalPosition.Z - centerPosition.Z;

            angleA = clockwise ? Math.Atan2(bZ, bX) : Math.Atan2(aZ, aX);
            angleB = clockwise ? Math.Atan2(aZ, aX) : Math.Atan2(bZ, bX);

            if (angleB <= angleA) angleB += 2 * Math.PI;
            angle = angleB - angleA;

            radius = Math.Sqrt(aX * aX + aZ * aZ);
            length = radius * angle;
            steps = (int)(length / Configuration.millimetersCurveSection);

            Position sectionPosition = new Position();
            for (int s = 1; s <= steps; s++)
            {
                int step = clockwise ? steps - s : s;
                sectionPosition.X = centerPosition.X + radius * Math.Cos(angleA + angle * ((float)step / steps));
                sectionPosition.Y = (finalPosition.Y - startPosition.Y) * ((float)s / steps);
                sectionPosition.Z = centerPosition.Z + radius * Math.Sin(angleA + angle * ((float)step / steps));
                result.Add(sectionPosition.ToString(gCode) + string.Format("F{0} ", feedRate));
            }

            return result;
        }
        private List<string> ProcessCurvePlaneYZ(string code)
        {
            List<string> result = new List<string>();
            double feedRate = HasValueParameter('F', code) ? GetValueParameter('F', code) : Configuration.defaultFeedrate; // Ver qué valor va cuando no hay valor - CONFIGURACION? -

            Position startPosition = this.CurrentPosition;
            Position finalPosition = this.GetFinalPosition(code);
            Position centerPosition = this.GetCenterPosition(code);
            int gCode = Convert.ToInt32(GetValueParameter('G', code));
            bool clockwise = gCode == 2 ? true : false;

            double aY, aZ, bY, bZ, angleA, angleB, angle, radius, length;
            int steps;

            aY = currentPosition.Y - centerPosition.Y;
            aZ = currentPosition.Z - centerPosition.Z;
            bY = finalPosition.Y - centerPosition.Y;
            bZ = finalPosition.Z - centerPosition.Z;

            angleA = clockwise ? Math.Atan2(bZ, bY) : Math.Atan2(aZ, aY);
            angleB = clockwise ? Math.Atan2(aZ, aY) : Math.Atan2(bZ, bY);

            if (angleB <= angleA) angleB += 2 * Math.PI;
            angle = angleB - angleA;

            radius = Math.Sqrt(aY * aY + aZ * aZ);
            length = radius * angle;
            steps = (int)(length / Configuration.millimetersCurveSection);

            Position sectionPosition = new Position();
            for (int s = 1; s <= steps; s++)
            {
                int step = clockwise ? steps - s : s;
                sectionPosition.X = (finalPosition.X - startPosition.X) * ((float)s / steps);
                sectionPosition.Y = centerPosition.Y + radius * Math.Cos(angleA + angle * ((float)step / steps));
                sectionPosition.Z = centerPosition.Z + radius * Math.Sin(angleA + angle * ((float)step / steps));
                result.Add(sectionPosition.ToString(gCode) + string.Format("F{0} ", feedRate));
            }

            return result;
        }

        private List<string> ProcessCurveCommand(string code)
        {
            switch (this.WorkingPlane)
            {
                case WorkingPlane.XY:
                    return this.ProcessCurvePlaneXY(code);

                case WorkingPlane.XZ:
                    return this.ProcessCurvePlaneXZ(code);

                case WorkingPlane.YZ:
                    return this.ProcessCurvePlaneYZ(code);
            }
            //return empty list ... ( no deberia llegar nunca aca )
            return new List<string>();
        }
        #endregion

        #region Public Methods
        public List<string> ProcessCommand(string command)
        {
            int code;
            List<string> result = new List<string>();
            double feedRate = HasValueParameter('F', command) ? GetValueParameter('F', command) : Configuration.defaultFeedrate; // Ver qué valor va cuando no hay valor - CONFIGURACION? -

            if (HasValueParameter('G', command))
            {
                code = Convert.ToInt32(GetValueParameter('G', command));
                switch (code)
                {
                    case 0:
                    case 1:
                        result.Add(this.GetFinalPosition(command).ToString(code) + string.Format("F{0} ", feedRate));
                        break;

                    case 4:
                        result.Add(command);
                        break;

                    case 2:
                    case 3:
                        result.AddRange(this.ProcessCurveCommand(command));
                        break;

                    case 17:
                        this.WorkingPlane = WorkingPlane.XY;
                        break;

                    case 18:
                        this.WorkingPlane = WorkingPlane.XZ;
                        break;

                    case 19:
                        this.WorkingPlane = WorkingPlane.YZ;
                        break;

                    case 20:
                        Configuration.millimetersProgramming = false;
                        break;

                    case 21:
                        Configuration.millimetersProgramming = true;
                        break;

                    case 90:
                        Configuration.absoluteProgamming = true;
                        break;

                    case 91:
                        Configuration.absoluteProgamming = false;
                        break;

                    default:
                        //controlamos o mandamos lo que venga?
                        break;
                }
            }
            else if (HasValueParameter('M', command))
            {
                result.Add(command);
            }

            return result;
        }
        #endregion
    }
}
