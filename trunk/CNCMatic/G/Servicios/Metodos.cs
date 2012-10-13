using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using G.Objetos;


namespace G.Servicios
{
    public static class Metodos
    {
        public static string LevantaPunta()
        {
            G00_Avance mov = new G00_Avance();
            mov.Z = 0.1000f;
            return mov.ToString();
        }

        public static string BajaPunta()
        {
            G01_Lineal mov = new G01_Lineal();
            mov.Fin.Z = 0.0000f;
            return mov.ToString();
        }

        public static string IrA(float x, float y, float z)
        {
            string s = "";
            //levantar la punta
            s += (LevantaPunta() + Environment.NewLine);

            //crear el mov de avance
            G00_Avance mov = new G00_Avance();
            mov.X = x;
            mov.Y = y;
            mov.Z = z;

            s += (mov.ToString() + Environment.NewLine);

            //bajamos la punta
            s += BajaPunta();

            return s;
        }

        public static string Avance(float x, float y, float z)
        {
            G00_Avance mov = new G00_Avance();
            mov.X = x;
            mov.Y = y;
            mov.Z = z;
            return (mov.ToString() + Environment.NewLine);
        }

        public static string Stop()
        {
            M00_Parada mov = new M00_Parada();
            return mov.ToString();

        }

        //public static string GastarUnPlano(float x, float y, float z, float deltaX)
        //{

        //}
        public static List<string> CilindroCentrado(double baseMenor, double baseMayor, double radio, double deltaY, int altura)
        {
            try
            {
                List<string> movimientos = new List<string>();

                for (int z = altura; z >= 0; z--)
                {
                    movimientos.Add("G01 Z" + z.ToString() + Environment.NewLine);

                    movimientos.AddRange(Metodos.CirculoCentrado(baseMenor, baseMayor, radio, deltaY));
                }

                return movimientos;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public static List<string> CirculoCentrado(double baseMenor, double baseMayor, double radio, double deltaY)
        {
            try
            { //validacion 1: el radio no puede ser mas largo que la baseMenor/2
                if (radio > baseMenor / 2)
                {
                    throw (new Exception("Error de validacion: el cilindro que se intenta fresar supera el area del material ingresado"));
                }

                //esquina inferior izquierda
                double xi = baseMenor / 2 - radio;
                double yi = baseMenor / 2 - radio;

                //x e y actuales
                double xa = xi;
                double ya = yi;

                List<String> movimientos = new List<string>();
                string movimiento = "";
                double dist;

                double veces = (radio * 2) / deltaY;

                //primera mitad
                for (int i = 1; i < veces; i++)
                {
                    movimiento = "";

                    //estamos en la primer mitad del circulo
                    if (i <= (veces / 2))
                    {
                        ya += deltaY;

                        movimiento += "G01 Y" + ya.ToString() + Environment.NewLine;

                        //calcular punto de la circunsferencia 
                        dist = Math.Sqrt(Math.Pow(radio, 2) - Math.Pow(radio - deltaY * i, 2));

                        if (i % 2 != 0) //es impar
                        {
                            xa = (baseMayor / 2) - dist;

                            movimiento += "G01 X" + xa.ToString() + Environment.NewLine;

                            xa += dist * 2;
                            movimiento += "G03 X" + xa.ToString() + " R" + radio.ToString() + Environment.NewLine;

                            xa = baseMayor - xi;
                            movimiento += "G01 X" + xa.ToString() + Environment.NewLine;
                        }
                        else
                        {//es par
                            xa = (baseMayor / 2) + dist;

                            movimiento += "G01 X" + xa.ToString() + Environment.NewLine;

                            xa -= dist * 2;
                            movimiento += "G02 X" + xa.ToString() + " R" + radio.ToString() + Environment.NewLine;

                            xa = xi;
                            movimiento += "G01 X" + xa.ToString() + Environment.NewLine;
                        }
                    }
                    else
                    {//segunda mitad del circulo
                        ya += deltaY;

                        movimiento += "G01 Y" + ya.ToString() + Environment.NewLine;

                        //calcular punto de la circunsferencia 
                        dist = Math.Sqrt(Math.Pow(radio, 2) - Math.Pow(radio - deltaY * i, 2));

                        if (i % 2 != 0) //es impar
                        {
                            xa = (baseMayor / 2) - dist;

                            movimiento += "G01 X" + xa.ToString() + Environment.NewLine;

                            xa += dist * 2;
                            movimiento += "G02 X" + xa.ToString() + " R" + radio.ToString() + Environment.NewLine;

                            xa = baseMayor - xi;
                            movimiento += "G01 X" + xa.ToString() + Environment.NewLine;
                        }
                        else
                        {//es par
                            xa = (baseMayor / 2) + dist;

                            movimiento += "G01 X" + xa.ToString() + Environment.NewLine;

                            xa -= dist * 2;
                            movimiento += "G03 X" + xa.ToString() + " R" + radio.ToString() + Environment.NewLine;

                            xa = xi;
                            movimiento += "G01 X" + xa.ToString() + Environment.NewLine;
                        }

                    }
                    //agregamos el string a la lista
                    movimientos.Add(movimiento);
                }

                //hacemos el circulo completo
                xa = baseMayor / 2 + radio;
                ya = baseMenor / 2;
                movimiento = "G00 X" + xa.ToString() + " Y" + ya.ToString() + Environment.NewLine;
                movimiento += "G02 X" + xa.ToString() + " Y" + ya.ToString() + " R" + radio.ToString() + Environment.NewLine;
                movimientos.Add(movimiento);

                //volvemos al inicio
                movimiento = "G00 Y" + yi.ToString() + Environment.NewLine;
                movimiento += "G00 X" + xi.ToString() + Environment.NewLine;
                movimientos.Add(movimiento);

                return movimientos;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static List<string> GastarVolumen(double xi, double yi, double baseMenor, double baseMayor, double deltaY, int altura, double zAbs)
        {
            try
            {
                List<string> movimientos = new List<string>();

                for (int z = altura; z >= 0; z--)
                {
                    //para que en el primero no vaya Z, ya que se supone que la herramienta
                    //deberia estar en el nivel Z de inicio
                    if (z != altura)
                    {
                        movimientos.Add("G01 Z" + (zAbs - (altura-z)).ToString() + Environment.NewLine);
                    }
                    movimientos.AddRange(Metodos.GastarPlano(xi, yi, baseMenor, baseMayor, deltaY));
                }

                return movimientos;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public static List<string> GastarPlano(double xi, double yi, double baseMenor, double baseMayor, double deltaY)
        {
            try
            {

                List<String> movimientos = new List<string>();
                string movimiento = "";

                double xa = xi;
                double ya = yi;

                double veces = (baseMayor / deltaY) / 2;

                //primera mitad
                for (int i = 1; i <= veces; i++)
                {
                    movimiento = "";

                    xa = baseMayor;

                    movimiento += "G01 X" + xa.ToString() + Environment.NewLine;

                    ya += deltaY;

                    movimiento += "G01 Y" + ya.ToString() + Environment.NewLine;

                    xa = xi;

                    movimiento += "G01 X" + xa.ToString() + Environment.NewLine;

                    ya += deltaY;

                    movimiento += "G01 Y" + ya.ToString() + Environment.NewLine;

                    movimientos.Add(movimiento);
                }

                //llegamos al final
                movimiento = "G01 X" + baseMayor.ToString() + Environment.NewLine;

                movimientos.Add(movimiento);

                //volvemos al inicio
                movimiento = "G00 Y" + yi.ToString() + Environment.NewLine;
                movimiento += "G00 X" + xi.ToString() + Environment.NewLine;
                movimientos.Add(movimiento);

                return movimientos;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        /// <summary>
        /// funcion que genera el G a fresar
        /// </summary>
        /// <param name="ancho">es el ancho del material (Y)</param>
        /// <param name="largo">es el largo del material (X)</param>
        /// <param name="alto">es el alto del material (Z)</param>
        /// <param name="anchoEscalon">es el ancho del escalon a fresar</param>
        /// <param name="altoEscalon">es el alto del escalon a fresar</param>
        /// <returns></returns>
        public static List<string> Escalera(double ancho, double largo, double alto, double anchoEscalon, double altoEscalon)
        {
            try
            {
                //cantidad total de escalones
                int escalones = Convert.ToInt32(ancho / anchoEscalon);
                List<string> movimientos = new List<string>();

                string movimiento = "";

                for (int i = 1; i < escalones; i++)
                {
                    //vamos hasta el final del escalon
                    movimiento += "G00 X" + (anchoEscalon * i).ToString();

                    movimientos.Add(movimiento);

                    //vaciamos lo que no es escalon
                    movimientos.AddRange(GastarVolumen((anchoEscalon*i), 0, (largo - anchoEscalon * i), largo, 1, Convert.ToInt32(altoEscalon), alto - altoEscalon * (i-1)));

                }
                return movimientos;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }
}
