using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandPreprocessor
{
    public class CommandPreprocessor
    {
        #region Properties
        private static readonly log4net.ILog logger = LogManager.LogManager.GetLogger();

        private WorkingPlane workingPlane;
        private UnitsPosition currentPosition;
        private UnitsPosition referencePosition; // Origen de la pieza a fresar
        #endregion

        #region Constructor
        private static CommandPreprocessor instance;
        protected CommandPreprocessor()
        {
            this.workingPlane = WorkingPlane.XY;
            this.currentPosition = new UnitsPosition(0, 0, 0);
            this.referencePosition = new UnitsPosition(0, 0, 0);
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
        public UnitsPosition CurrentPosition
        {
            get { return this.currentPosition; }
            set { this.currentPosition = value; }
        }
        public UnitsPosition ReferencePosition
        {
            get { return this.referencePosition; }
            set { this.referencePosition = value; }
        }
        #endregion

        #region Private Methods
        private double GetValueParameter(char parameterName, string command)
        {
            foreach(string parameter in command.ToUpper().Split(' '))
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
            return command.ToUpper().IndexOf(parameterName) != -1;
        }

        private UnitsPosition GetFinalPosition(string command)
        {
            UnitsPosition result = new UnitsPosition();

            if (Configuration.absoluteProgamming)
            {
                result.X = HasValueParameter('X', command) ? GetValueParameter('X', command) + this.ReferencePosition.X : CurrentPosition.X;
                result.Y = HasValueParameter('Y', command) ? GetValueParameter('Y', command) + this.ReferencePosition.Y : CurrentPosition.Y;
                result.Z = HasValueParameter('Z', command) ? this.ReferencePosition.Z - GetValueParameter('Z', command) : CurrentPosition.Z;
            }
            else
            {
                // Translate to relative programming mode
                result.X = HasValueParameter('X', command) ? GetValueParameter('X', command) + this.CurrentPosition.X : CurrentPosition.X;
                result.Y = HasValueParameter('Y', command) ? GetValueParameter('Y', command) + this.CurrentPosition.Y : CurrentPosition.Y;
                result.Z = HasValueParameter('Z', command) ? this.CurrentPosition.Z - GetValueParameter('Z', command) : CurrentPosition.Z;
            }

            logger.Info("voy a ir al Z " + result.Z);
            if (result.Z < 0) throw new Exception("Movimiento Inválido. (Eje Z sobrepasa límite superior)");

            // If the values are in inches, convert to millimeters
            if (!Configuration.millimetersProgramming)
            {
                result.X *= 25.4;
                result.Y *= 25.4;
                result.Z *= 25.4;
            }

            return result;
        }
        private UnitsPosition GetFromRadius(double r, string command)
        {
            double midX, midY, midZ, cat1x, cat1y, cat1z, cat1, cat2x, cat2y, cat2z, cat2;
            logger.Info("Get from radius");
            UnitsPosition final = GetFinalPosition(command);
            UnitsPosition centerPosition = new UnitsPosition();

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
                    {
                        logger.Error("Imposible realizar arco: " + command + ". (Distancia media entre punto inicial y final es mayor al radio)");
                        throw new Exception("Imposible realizar arco: " + command + ". (Distancia media entre punto inicial y final es mayor al radio)");
                    }

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
                    {
                        logger.Error("Imposible realizar arco: " + command + ". (Distancia media entre punto inicial y final es mayor al radio)");
                        throw new Exception("Imposible realizar arco: " + command + ". (Distancia media entre punto inicial y final es mayor al radio)");
                    }

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
                    {
                        logger.Error("Imposible realizar arco: " + command + ". (Distancia media entre punto inicial y final es mayor al radio)");
                        throw new Exception("Imposible realizar arco: " + command + ". (Distancia media entre punto inicial y final es mayor al radio)");
                    }

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
        private UnitsPosition GetCenterPosition(string command)
        {
            UnitsPosition result = new UnitsPosition();

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

        private List<UnitsPosition> ProcessCurvePlaneXY(string code)
        {
            List<UnitsPosition> result = new List<UnitsPosition>();
            double feedRate = HasValueParameter('F', code) ? GetValueParameter('F', code) : Configuration.defaultFeedrate; // Ver qué valor va cuando no hay valor - CONFIGURACION? -

            UnitsPosition startPosition = this.CurrentPosition;
            logger.Info("procesando curva XY");
            UnitsPosition finalPosition = this.GetFinalPosition(code);
            UnitsPosition centerPosition = this.GetCenterPosition(code);
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

            for (int s = 1; s <= steps; s++)
            {
                UnitsPosition sectionPosition = new UnitsPosition();
                int step = clockwise ? steps - s : s;
                sectionPosition.X = centerPosition.X + radius * Math.Cos(angleA + angle * ((float)step / steps));
                sectionPosition.Y = centerPosition.Y + radius * Math.Sin(angleA + angle * ((float)step / steps));
                sectionPosition.Z = (finalPosition.Z - startPosition.Z) * ((float)s / steps) + this.ReferencePosition.Z;

                if ((sectionPosition.X < 0) || (sectionPosition.Y < 0) || (sectionPosition.Z < 0))
                {
                    logger.Error("Underflow procesando: " + code + ".");
                    throw new Exception("Underflow procesando: " + code + ". (PuntoDestino: X" + sectionPosition.X + " Y" + sectionPosition.Y + " Z" + sectionPosition.Z);
                }
                
                // Traducir la curva (G02 / G03) como varias rectas (G01)
                result.Add(sectionPosition);
            }

            return result;
        }
        private List<UnitsPosition> ProcessCurvePlaneXZ(string code)
        {
            List<UnitsPosition> result = new List<UnitsPosition>();
            double feedRate = HasValueParameter('F', code) ? GetValueParameter('F', code) : Configuration.defaultFeedrate; // Ver qué valor va cuando no hay valor - CONFIGURACION? -

            UnitsPosition startPosition = this.CurrentPosition;
            UnitsPosition finalPosition = this.GetFinalPosition(code);
            UnitsPosition centerPosition = this.GetCenterPosition(code);
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

            for (int s = 1; s <= steps; s++)
            {
                UnitsPosition sectionPosition = new UnitsPosition();
                int step = clockwise ? steps - s : s;
                sectionPosition.X = centerPosition.X + radius * Math.Cos(angleA + angle * ((float)step / steps));
                sectionPosition.Y = (finalPosition.Y - startPosition.Y) * ((float)s / steps);
                sectionPosition.Z = centerPosition.Z + radius * Math.Sin(angleA + angle * ((float)step / steps));

                if ((sectionPosition.X < 0) || (sectionPosition.Y < 0) || (sectionPosition.Z < 0))
                {
                    logger.Error("Underflow procesando: " + code + ".");
                    throw new Exception("Underflow procesando: " + code + ".");
                }

                result.Add(sectionPosition);
            }

            return result;
        }
        private List<UnitsPosition> ProcessCurvePlaneYZ(string code)
        {
            List<UnitsPosition> result = new List<UnitsPosition>();
            double feedRate = HasValueParameter('F', code) ? GetValueParameter('F', code) : Configuration.defaultFeedrate; // Ver qué valor va cuando no hay valor - CONFIGURACION? -

            UnitsPosition startPosition = this.CurrentPosition;
            UnitsPosition finalPosition = this.GetFinalPosition(code);
            UnitsPosition centerPosition = this.GetCenterPosition(code);
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

            for (int s = 1; s <= steps; s++)
            {
                UnitsPosition sectionPosition = new UnitsPosition();
                int step = clockwise ? steps - s : s;
                sectionPosition.X = (finalPosition.X - startPosition.X) * ((float)s / steps);
                sectionPosition.Y = centerPosition.Y + radius * Math.Cos(angleA + angle * ((float)step / steps));
                sectionPosition.Z = centerPosition.Z + radius * Math.Sin(angleA + angle * ((float)step / steps));

                if ((sectionPosition.X < 0) || (sectionPosition.Y < 0) || (sectionPosition.Z < 0))
                {
                    logger.Error("Underflow procesando: " + code + ".");
                    throw new Exception("Underflow procesando: " + code + ".");
                }

                result.Add(sectionPosition);
            }

            return result;
        }

        private List<UnitsPosition> ProcessCurveCommand(string code)
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
            // no deberia llegar nunca aca
            return new List<UnitsPosition>();
        }
        #endregion

        #region Public Methods
        private List<string> ProcessCommand(string command)
        {
            int code;
            List<string> result = new List<string>();
            double feedRate = HasValueParameter('F', command) ? GetValueParameter('F', command) : Configuration.defaultFeedrate; // Ver qué valor va cuando no hay valor - CONFIGURACION? -

            if (HasValueParameter('G', command))
            {
                code = Convert.ToInt32(GetValueParameter('G', command));
                string finalLine;
                switch (code)
                {
                    case 0:
                    case 1:
                        UnitsPosition line = this.GetFinalPosition(command);
                        finalLine = line.ToStepsPosition(CurrentPosition, feedRate, code == 0).ToString(-1);
                        logger.Debug("Resultado Linea: " + finalLine);
                        result.Add(finalLine);
                        CurrentPosition = line;
                        break;

                    case 2:
                    case 3:
                        List<UnitsPosition> curveLines = this.ProcessCurveCommand(command);

                        foreach (UnitsPosition curveLine in curveLines)
                        {
                            finalLine = curveLine.ToStepsPosition(CurrentPosition, feedRate).ToString(-1);
                            logger.Debug("Resultado Sección de Curva: " + finalLine);
                            result.Add(finalLine);
                            CurrentPosition = curveLine;
                        }
                        break;

                    case 4:
                        result.Add(command);
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
                        logger.Error("Comando no soportado: " + command + ".");
                        throw new Exception("Comando no soportado: " + command + ".");
                }
            }
            else if (HasValueParameter('M', command))
            {
                code = Convert.ToInt32(GetValueParameter('M', command));
                switch(code)
                {
                    case 0:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        result.Add(command);
                        break;

                    default:
                        logger.Error("Comando no soportado: " + command + ".");
                        throw new Exception("Comando no soportado: " + command + ".");
                }
            }

            return result;
        }
        public List<string> ProcessProgram(List<string> program)
        {
            List<string> result = new List<string>();
            try
            {
                logger.Info("Iniciando Preprocesamiento de programa. Parametros:" + Environment.NewLine +
                                "Programación Absoluta: " + Configuration.absoluteProgamming + Environment.NewLine +
                                "Programación en Milímetros: " + Configuration.millimetersProgramming + Environment.NewLine +
                                "Milímetros de Sección Curva: " + Configuration.millimetersCurveSection + Environment.NewLine +
                                "Milímetros por Minuto (default): " + Configuration.defaultFeedrate);
                this.CurrentPosition = this.ReferencePosition;

                // Process every command in the program
                foreach (string cmd in program)
                {
                    logger.Info("Procesando comando: " + cmd);
                    if (!string.IsNullOrEmpty(cmd))
                    {
                        result.AddRange(this.ProcessCommand(cmd));
                        logger.Info("actualizando currentPosition");
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