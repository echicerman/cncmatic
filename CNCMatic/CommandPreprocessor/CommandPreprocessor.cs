using System;
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
        private double maxZ;
        #endregion

        #region Constructor
        private static CommandPreprocessor instance;
        protected CommandPreprocessor()
        {
            this.workingPlane = WorkingPlane.XY;
            this.currentPosition = new Position(0, 0, 0);
            this.referencePosition = new Position(0, 0, 0);
            this.maxZ = 0;

            //Configuration.absoluteProgamming = true;
            //Configuration.millimetersProgramming = true;
            //Configuration.millimetersCurveSection = 0.5;
            //Configuration.defaultFeedrate = 60;
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

        #region Getters & Setters
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
        public double MaxZ
        {
            get { return this.maxZ; }
            set { this.maxZ = value; }
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

            // Restar a MaxZ invierte las alturas... el techo pasa a ser 0
            if (Configuration.absoluteProgamming)
            {
                result.X = GetValueParameter('X', command) + this.ReferencePosition.X;
                result.Y = GetValueParameter('Y', command) + this.ReferencePosition.Y;
                result.Z = this.MaxZ - GetValueParameter('Z', command) + this.ReferencePosition.Z;
            }
            else
            {
                // Translate to relative programming mode
                result.X = GetValueParameter('X', command) + this.CurrentPosition.X;
                result.Y = GetValueParameter('Y', command) + this.CurrentPosition.Y;
                result.Z = this.MaxZ - GetValueParameter('Z', command) + this.CurrentPosition.Z;
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
            double midX, midY, midZ, cat1x, cat1y, cat1z, cat1, cat2x, cat2y, cat2z, cat2;
            Position final = GetFinalPosition(command);
            Position centerPosition = new Position();

            // componentes de Punto Medio de segmento que une Punto Inicial con Punto Final
            midZ = (final.Z + CurrentPosition.Z) / 2;
            midY = (final.Y + CurrentPosition.Y) / 2;
            midX = (final.X + CurrentPosition.X) / 2;
            // componentes de Longitud desde Punto Inicial a Punto Medio de segmento
            cat1x = midX - CurrentPosition.X;
            cat1y = midY - CurrentPosition.Y;
            cat1z = midZ - CurrentPosition.Z;

            switch (this.WorkingPlane)
            { 
                case WorkingPlane.XY:
                    cat1 = Math.Sqrt(cat1x * cat1x + cat1y * cat1y);

                    if (cat1 > Math.Abs(r))
                        throw new Exception("Imposible realizar arco: " + command + ". (Distancia media entre punto inicial y final es mayor al radio)");

                    cat2 = Math.Sqrt(r * r - cat1 * cat1);
                    cat2x = cat1y * cat2 / cat1;
                    cat2y = -cat1x * cat2 / cat1;

                    if (r > 0)
                    {
                        centerPosition.X = midX + cat2x;
                        centerPosition.Y = midY + cat2y;
                    }
                    else
                    {
                        centerPosition.X = midX - cat2x;
                        centerPosition.Y = midY - cat2y;
                    }
                    centerPosition.Z = midZ;
                    break;

                case WorkingPlane.XZ:
                    cat1 = Math.Sqrt(cat1x * cat1x + cat1z * cat1z);

                    if (cat1 > Math.Abs(r))
                        throw new Exception("Imposible realizar arco: " + command + ". (Distancia media entre punto inicial y final es mayor al radio)");

                    cat2 = Math.Sqrt(r * r - cat1 * cat1);
                    cat2x = cat1z * cat2 / cat1;
                    cat2z = -cat1x * cat2 / cat1;

                    if (r > 0)
                    {
                        centerPosition.X = midX + cat2x;
                        centerPosition.Z = midZ + cat2z;
                    }
                    else
                    {
                        centerPosition.X = midX - cat2x;
                        centerPosition.Z = midZ - cat2z;
                    }
                    centerPosition.Y = midY;
                    break;

                case WorkingPlane.YZ:
                    cat1 = Math.Sqrt(cat1y * cat1y + cat1z * cat1z);

                    if (cat1 > Math.Abs(r)) 
                        throw new Exception("Imposible realizar arco: " + command + ". (Distancia media entre punto inicial y final es mayor al radio)");

                    cat2 = Math.Sqrt(r * r - cat1 * cat1);
                    cat2y = cat1z * cat2 / cat1;
                    cat2z = -cat1y * cat2 / cat1;

                    if (r > 0)
                    {
                        centerPosition.Y = midY + cat2y;
                        centerPosition.Z = midZ + cat2z;
                    }
                    else
                    {
                        centerPosition.Y = midY - cat2y;
                        centerPosition.Z = midZ - cat2z;
                    }
                    centerPosition.X = midX;
                    break;
            }
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
            steps = steps == 0 ? 1 : steps;

            Position sectionPosition = new Position();
            for (int s = 1; s <= steps; s++)
            {
                int step = clockwise ? steps - s : s;
                sectionPosition.X = centerPosition.X + radius * Math.Cos(angleA + angle * ((float)step / steps));
                sectionPosition.Y = centerPosition.Y + radius * Math.Sin(angleA + angle * ((float)step / steps));
                sectionPosition.Z = (finalPosition.Z - startPosition.Z) * ((float)s / steps);

                if ((sectionPosition.X < 0) || (sectionPosition.Y < 0) || (sectionPosition.Z < 0))
                {
                    throw new Exception("Underflow procesando: " + code + ". (PuntoDestino: X" + sectionPosition.X + " Y" + sectionPosition.Y + " Z" + sectionPosition.Z);
                }
                
                // Traducir la curva (G02 / G03) como varias rectas (G01)
                result.Add(sectionPosition.ToString(1) + string.Format("F{0} ", feedRate));
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

                if ((sectionPosition.X < 0) || (sectionPosition.Y < 0) || (sectionPosition.Z < 0))
                {
                    throw new Exception("Underflow procesando: " + code + ".");
                }
                
                // Traducir la curva (G02 / G03) como varias rectas (G01)
                result.Add(sectionPosition.ToString(1) + string.Format("F{0} ", feedRate));
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

                if ((sectionPosition.X < 0) || (sectionPosition.Y < 0) || (sectionPosition.Z < 0))
                {
                    throw new Exception("Underflow procesando: " + code + ".");
                }

                // Traducir la curva (G02 / G03) como varias rectas (G01)
                result.Add(sectionPosition.ToString(1) + string.Format("F{0} ", feedRate));
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
        //private List<string> ProcessCommand(string command)
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
                        throw new Exception("Comando no soportado: " + code + ".");
                        break;
                }
            }
            else if (HasValueParameter('M', command))
            {
                result.Add(command);
            }

            return result;
        }
        public List<string> ProcessProgram(List<string> program)
        {
            List<string> result = new List<string>();
            try
            {
                this.MaxZ = 0;
                // Get max Z looking into every command
                foreach (string cmd in program)
                {
                    if (!string.IsNullOrEmpty(cmd))
                    {
                        double newZ = GetValueParameter('Z', cmd);
                        this.MaxZ = newZ > this.MaxZ ? newZ : this.MaxZ;
                    }
                }

                // Process every command in the program
                foreach (string cmd in program)
                {
                    if (!string.IsNullOrEmpty(cmd))
                    {
                        result.AddRange(this.ProcessCommand(cmd));
                        this.CurrentPosition = this.GetFinalPosition(cmd);
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}