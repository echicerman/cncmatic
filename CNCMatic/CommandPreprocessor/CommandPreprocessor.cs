using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandPreprocessor
{
    public class CommandPreprocessor
    {
        #region Properties
        // workingPlane
        Position currentPosition;
        #endregion

        #region Constructor
        private static CommandPreprocessor instance;
        protected CommandPreprocessor()
        {
            currentPosition = new Position(0, 0, 0);
            Configuration.absoluteProgamming = true;
            Configuration.millimetersProgramming = true;
            Configuration.millimetersCurveSection = 0.5;
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

        internal double GetValueParameter(char parameterName, string command)
        {
            int length = command.Length;
            for(int i = 0; i < length; i++)
            {
                if(command[i] == parameterName)
                {
                    string value = command.Substring(i, command.IndexOf(' ', i));
                    return double.Parse(value);
                }
            }
            return 0;
        }
        internal bool HasValueParameter(char parameterName, string command)
        { 
            int length = command.Length;
            for (int i = 0; i < length; i++)
            {
                if (command[i] == parameterName)
                {
                    return true;
                }
            }
            return false;
        }
        
        internal Position GetFinalPosition(string command)
        {
            Position result = new Position();
            
            if (Configuration.absoluteProgamming)
            {
                result.X = HasValueParameter('X', command) ? GetValueParameter('X', command) : currentPosition.X;
                result.Y = HasValueParameter('Y', command) ? GetValueParameter('Y', command) : currentPosition.Y;
                result.Z = HasValueParameter('Z', command) ? GetValueParameter('Z', command) : currentPosition.Z;
            }
            else
            { 
                // Translate to absolute programming mode
                result.X = GetValueParameter('X', command) + this.currentPosition.X;
                result.Y = GetValueParameter('Y', command) + this.currentPosition.Y;
                result.Z = GetValueParameter('Z', command) + this.currentPosition.Z;
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
        internal Position GetCenterPosition(string command)
        {
            Position result = new Position();
            
            // The center point is relative to the start position... Translate to absolute programming mode
            result.X = GetValueParameter('I', command) + this.currentPosition.X;
            result.Y = GetValueParameter('J', command) + this.currentPosition.Y;
            result.Z = GetValueParameter('K', command) + this.currentPosition.Z;

            return result;
        }

        void ProcessCurveCommand(string code)
        {
            Position finalPosition = this.GetFinalPosition(code);
            Position centerPosition = this.GetCenterPosition(code);
            bool clockwise = GetValueParameter('G', code) == 2 ? true : false;

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

            Position stepPosition = new Position();
            for (int s = 1; s <= steps; s++)
            {
                int step = clockwise ? steps - s : s;
                stepPosition.X = centerPosition.X + radius * Math.Cos(angleA + angle * ((float)step / steps));
                stepPosition.Y = centerPosition.Y + radius * Math.Sin(angleA + angle * ((float)step / steps));
            }
        }
    }
}
