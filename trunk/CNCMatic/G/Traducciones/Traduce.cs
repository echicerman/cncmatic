using System;
using System.Collections.Generic ;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DXF.Entidades;
using G.Objetos;

namespace G.Traducciones
{
    public static class Traduce
    {
        public static List<string> Lineas(ReadOnlyCollection<Linea> lineas)
        {
            G01_Lineal mov;
            List<string> movs = new List<string>();

            foreach (Linea l in lineas)
            {
                mov = new G01_Lineal();

                mov.Inicio.X = l.PuntoInicio.X;
                mov.Inicio.Y = l.PuntoInicio.Y;
                mov.Inicio.Z = l.PuntoInicio.Z;
                                
                mov.Fin.X = l.PuntoFinal.X;
                mov.Fin.Y = l.PuntoFinal.Y;
                mov.Fin.Z = l.PuntoFinal.Z;

                movs.Add(IrA(mov.Inicio.X, mov.Inicio.Y, mov.Inicio.Z));
                movs.Add(mov.ToString());

            }

            return movs;
        }

        public static List<string> Arcos(ReadOnlyCollection<Arco> arcos)
        {
            G03_CirculoA mov;
            List<string> movs = new List<string>();

            foreach (Arco a in arcos)
            {
                mov = new G03_CirculoA();

                mov.Inicio.X = a.PuntoInicio.X;
                mov.Inicio.Y = a.PuntoInicio.Y;
                mov.Inicio.Z = a.PuntoInicio.Z;

                mov.Fin.X = a.PuntoFin.X ;
                mov.Fin.Y = a.PuntoFin.Y;
                mov.Fin.Z = a.PuntoFin.Z;

                mov.Radio = a.Radio;

                movs.Add(IrA(mov.Inicio.X, mov.Inicio.Y, mov.Inicio.Z));
                movs.Add(mov.ToString());

            }

            return movs;
        }

        private  static string LevantaPunta()
        {
            G00_Avance mov=new G00_Avance();
            mov.Z = 0.1000f;
            return mov.ToString();
        }

        private static string BajaPunta()
        {
            G01_Lineal mov = new G01_Lineal();
            mov.Fin.Z = 0.0000f;
            return mov.ToString();
        }

        private static string IrA(float x, float y, float z)
        {
            string s = "";
            //levantar la punta
            s += (LevantaPunta() + Environment.NewLine) ;

            //crear el mov de avance
            G00_Avance mov = new G00_Avance();
            mov.X = x;
            mov.Y = y;
            mov.Z = z;

            s += (mov.ToString()+ Environment.NewLine);
            
            //bajamos la punta
            s += BajaPunta();

            return s;
        }

    }
}

