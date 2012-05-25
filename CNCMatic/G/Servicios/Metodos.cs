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

        public static string Stop()
        {
            M00_Parada mov = new M00_Parada();
            return mov.ToString();

        }
    }
}
