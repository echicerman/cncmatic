using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DXF.Entidades;
using DXF.Objetos;
using G.Objetos;
using G.Servicios;

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

                movs.Add(Metodos.IrA(mov.Inicio.X, mov.Inicio.Y, mov.Inicio.Z));
                movs.Add(mov.ToString());

            }

            return movs;
        }

        public static List<string> Puntos(ReadOnlyCollection<DXF.Entidades.Punto> puntos)
        {
            G01_Lineal mov;
            List<string> movs = new List<string>();

            foreach (DXF.Entidades.Punto p in puntos)
            {
                mov = new G01_Lineal();

                mov.Inicio.X = p.Ubicacion.X;
                mov.Inicio.Y = p.Ubicacion.Y;
                mov.Inicio.Z = p.Ubicacion.Z;

                movs.Add(Metodos.IrA(mov.Inicio.X, mov.Inicio.Y, mov.Inicio.Z));
                //movs.Add(mov.ToString());

            }

            return movs;
        }

        public static List<string> Arcos(ReadOnlyCollection<Arco> arcos)
        {
            //G02_ArcoH mov;
            List<string> movs = new List<string>();

            foreach (Arco a in arcos)
            {
                //if (a.AnguloInicio > a.AnguloFin)
                //{
                //    G02_ArcoH mov = new G02_ArcoH();
                //    mov.Inicio.X = a.PuntoInicio.X;
                //    mov.Inicio.Y = a.PuntoInicio.Y;
                //    mov.Inicio.Z = a.PuntoInicio.Z;

                //    mov.Fin.X = a.PuntoFin.X;
                //    mov.Fin.Y = a.PuntoFin.Y;
                //    mov.Fin.Z = a.PuntoFin.Z;


                //    mov.Radio = a.Radio;

                //    movs.Add(Metodos.IrA(mov.Inicio.X, mov.Inicio.Y, mov.Inicio.Z));
                //    movs.Add(mov.ToString());
                //}
                //if (a.AnguloInicio<a.AnguloFin)
                //{
                G03_ArcoA mov = new G03_ArcoA();
                mov.Inicio.X = a.PuntoInicio.X;
                mov.Inicio.Y = a.PuntoInicio.Y;
                mov.Inicio.Z = a.PuntoInicio.Z;

                mov.Fin.X = a.PuntoFin.X;
                mov.Fin.Y = a.PuntoFin.Y;
                mov.Fin.Z = a.PuntoFin.Z;


                mov.Radio = a.Radio;

                movs.Add(Metodos.IrA(mov.Inicio.X, mov.Inicio.Y, mov.Inicio.Z));
                movs.Add(mov.ToString());

                //}



            }

            return movs;
        }

        public static List<string> Circulos(ReadOnlyCollection<Circulo> circulos)
        {
            G03_CirculoA mov;
            List<string> movs = new List<string>();

            foreach (Circulo a in circulos)
            {
                mov = new G03_CirculoA();

                mov.Inicio.X = a.Inicio.X;
                mov.Inicio.Y = a.Inicio.Y;
                mov.Inicio.Z = a.Inicio.Z;

                mov.Radio = a.Radio;

                movs.Add(Metodos.IrA(mov.Inicio.X, mov.Inicio.Y, mov.Inicio.Z));
                movs.Add(mov.ToString());

            }

            return movs;
        }

        public static List<string> Elipses(ReadOnlyCollection<Elipse> elipses)
        {
            G03_ArcoA mov;
            List<string> movs = new List<string>();

            foreach (Elipse e in elipses)
            {

                mov = new G03_ArcoA();
                //mov.Inicio.X = e..PuntoInicio.X;
                //mov.Inicio.Y = a.PuntoInicio.Y;
                //mov.Inicio.Z = a.PuntoInicio.Z;

                //mov.Fin.X = a.PuntoFin.X;
                //mov.Fin.Y = a.PuntoFin.Y;
                //mov.Fin.Z = a.PuntoFin.Z;


                //mov.Radio = a.Radio;


                //movs = calculaEsfera(0,0,40,15,1000);
                //movs = calculaEsfera2(40, 0.1, 10);
                movs = calculaEsfera(e.Centro.X, e.Centro.Y, e.EjeMenor, e.EjeMayor, 1000);

                //movs.Add(Metodos.IrA(mov.Inicio.X, mov.Inicio.Y, mov.Inicio.Z));
                //movs.Add(mov.ToString());

            }

            return movs;
        }
        public static List<string> calculaEsfera(float xc, float yc, float xr, float yr, int precision)
        {
            double theta;
            int i;
            double x, y;

            i = 1;
                      
            List<Vector2d> puntos = new List<Vector2d>();

            while (i <= precision)
            {
                theta = 360 * i / precision;
                x = xc + xr * Math.Cos(theta);
                y = yc + yr * Math.Sin(theta);

                puntos.Add(new Vector2d(x, y));
                
                i++;
            }

            List<string> movs = new List<string>();
            //movs.Add("G00 Z0.1");
            //movs.Add("G00 X" + (xc + xr).ToString() + " Y" + yc);
            movs.Add(Metodos.IrA(xc + xr, yc, 0));

            
            foreach (Vector2d punto in puntos)
            {
                int j = 0;
                //movs.Add("G01 X" + punto.X.ToString() + " Y" + punto.Y.ToString());
                movs.Add(Metodos.IrA(float.Parse(punto.X.ToString()), float.Parse(punto.Y.ToString()), 0));
                Vector2d puntoH = new Vector2d(10000,100000);
                while (j < puntos.Count)
                {
                    //if ( (punto.X - puntos[j].X < punto.X-puntoH.X) && (punto.Y < puntos[j].Y && punto.Y < puntoH.Y))
                    //{
                    //    puntoH = puntos[j];
                    //}
                    if (punto.X != puntos[j].X && punto.Y != puntos[j].Y)
                    {
                        if (Vector2d.Distance(punto, puntos[j]) < Vector2d.Distance(punto, puntoH))
                        {
                            puntoH = puntos[j];
                        }
                    }
                    j++;
                }
                movs.Add("G01 X" + puntoH.X.ToString() + " Y" + puntoH.Y.ToString());
            }
            return movs;
        }


        public static List<string> calculaEsfera2(double radio, double escalaY, int pasos)
        {
            int i = 0;
            List<string> movs = new List<string>();
            //movs.Add("G01 X" + (radio * Math.Cos(i)).ToString() + " Y" + (radio * escalaY * Math.Sin(i)).ToString());

            while (i < 360)
            {
                movs.Add("G01 X" + (radio * Math.Cos(i)).ToString() + " Y" + (radio * escalaY * Math.Sin(i)).ToString());
                i = i + pasos;
            }

            movs.Add("G01 X" + (radio * Math.Cos(360)).ToString() + " Y" + (radio * escalaY * Math.Sin(360)).ToString());

            return movs;
        }
    }

    
}


