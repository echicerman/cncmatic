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

        public static string CilindroCentrado(float baseMenor, float baseMayor, float radio, float deltaY)
        {
            try
            { //validacion 1: el radio no puede ser mas largo que la baseMenor/2
                if (radio > baseMenor / 2)
                {
                    throw (new Exception("Error de validacion: el cilindro que se intenta fresar supera el area del material ingresado"));
                }

                //esquina inferior izquierda
                float x = baseMenor / 2 - radio;
                float y = baseMenor / 2 - radio;
                List<String> movimientos;
                float dist;

                for (int i = 0; i <= (baseMenor / deltaY); i++)
                {
                    if (i % 2 == 0) //es par
                    {
                        movimientos += "G01 X" & x.ToString & " Y" & (y + deltaY * i).ToString & Environment.NewLine;
                        //calcular punto de la circunsferencia 
                        dist = Math.Sqrt(Math.Pow(radio,2) - Math.Pow(radio - deltaY * i)); 
                        movimientos += "G01 X" & ()
                    }
                    else
                    {//es impar

                    }

                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
