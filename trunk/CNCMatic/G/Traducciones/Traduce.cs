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

                mov.Fin.X = p.Ubicacion.X;
                mov.Fin.Y = p.Ubicacion.Y;
                mov.Fin.Z = p.Ubicacion.Z;

                movs.Add(Metodos.IrA(mov.Inicio.X, mov.Inicio.Y, mov.Inicio.Z));
                movs.Add(mov.ToString());

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


                mov.Centro = new G.Objetos.Punto(a.Centro);

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
            //G03_ArcoA mov;
            List<string> movs = new List<string>();

            foreach (Elipse e in elipses)
            {

                //mov = new G03_ArcoA();
                //mov.Inicio.X = e..PuntoInicio.X;
                //mov.Inicio.Y = a.PuntoInicio.Y;
                //mov.Inicio.Z = a.PuntoInicio.Z;

                //mov.Fin.X = a.PuntoFin.X;
                //mov.Fin.Y = a.PuntoFin.Y;
                //mov.Fin.Z = a.PuntoFin.Z;


                //mov.Radio = a.Radio;


                //movs = calculaEsfera(0,0,40,15,1000);
                //movs = calculaEsfera2(40, 0.1, 10);
                //movs.InsertRange(0, calculaElipse(e.Centro.X, e.Centro.Y, e.EjeMenor, e.EjeMayor, e.Rotacion, 1000));
                calculaElipse(e,95);

                //movs.Add(Metodos.IrA(mov.Inicio.X, mov.Inicio.Y, mov.Inicio.Z));
                //movs.Add(mov.ToString());

            }

            return movs;
        }
        
        public static void calculaElipse(Elipse elip, int precision)
        {
            float angulo = elip.AnguloInicio;
            Vector2d pi = ElipsePunto(elip.Centro.X, elip.Centro.Y, elip.EjeMayor/2, elip.EjeMenor / 2, angulo, elip.Rotacion);
            double phi = ElipseTang(elip.EjeMayor / 2, elip.EjeMenor / 2, angulo, elip.Rotacion);
            
            List<Vector2d> puntos = new List<Vector2d>();
            List<double> tans = new List<double>();

            puntos.Insert(0, pi);
            tans.Insert(0,phi);

            int i = 1;
            float theta = elip.AnguloFin - elip.AnguloInicio;

            while (i <= precision)
            {
                //theta = 360 * i / precision;

                angulo += theta / precision;

                puntos.Insert(i, ElipsePunto(elip.Centro.X, elip.Centro.Y, elip.EjeMayor/2, elip.EjeMenor/2, angulo, elip.Rotacion));
                tans.Insert(i, ElipseTang(elip.EjeMayor/2, elip.EjeMenor/2, angulo, elip.Rotacion));
                i++;
            }
            return;

        }

        //public static List<string> calculaElipse(float xc, float yc, float xr, float yr, float rotacion, int precision)
        //{
        //    double theta;
        //    int i;
        //    double x, y;

        //    i = 1;

        //    List<Vector2d> puntos = new List<Vector2d>();

        //    while (i <= precision)
        //    {
        //        theta = 360 * i / precision;
        //        x = xc + xr * Math.Cos(theta);
        //        y = yc + xr * Math.Cos(theta);

        //        puntos.Add(new Vector2d(x, y));

        //        i++;
        //    }

        //    List<string> movs = new List<string>();
        //    //movs.Add("G00 Z0.1");
        //    //movs.Add("G00 X" + (xc + xr).ToString() + " Y" + yc);
        //    movs.Add(Metodos.IrA(xc + xr, yc, 0));


        //    foreach (Vector2d punto in puntos)
        //    {
        //        int j = 0;
        //        //movs.Add("G01 X" + punto.X.ToString() + " Y" + punto.Y.ToString());
        //        movs.Add(Metodos.IrA(float.Parse(punto.X.ToString()), float.Parse(punto.Y.ToString()), 0));
        //        Vector2d puntoH = new Vector2d(10000, 100000);
        //        while (j < puntos.Count)
        //        {
        //            //if ( (punto.X - puntos[j].X < punto.X-puntoH.X) && (punto.Y < puntos[j].Y && punto.Y < puntoH.Y))
        //            //{
        //            //    puntoH = puntos[j];
        //            //}
        //            if (punto.X != puntos[j].X && punto.Y != puntos[j].Y)
        //            {
        //                if (Vector2d.Distance(punto, puntos[j]) < Vector2d.Distance(punto, puntoH))
        //                {
        //                    puntoH = puntos[j];
        //                }
        //            }
        //            j++;
        //        }
        //        movs.Add("G01 X" + puntoH.X.ToString() + " Y" + puntoH.Y.ToString());
        //    }
        //    return movs;
        //}

        public static Vector2d ElipsePunto(float xc, float yc, float a, float b, float angulo, float rotacion)
        {
            double Px = a * Math.Cos(angulo) * Math.Cos(rotacion) - b * Math.Sin(angulo) * Math.Sin(rotacion);
            double Py = a * Math.Cos(angulo) * Math.Sin(rotacion) + b * Math.Sin(angulo) * Math.Cos(rotacion);

            return new Vector2d(xc+Px, yc+Py);
        }

        public static double ElipseTang(float a, float b, float angulo, float rotacion)
        {
            double phi = Math.Atan2(a * Math.Sin(angulo), b * Math.Cos(angulo)) + rotacion + Math.PI / 2;
            return phi;
        }

        public static List<string> Polilineas(ReadOnlyCollection<IPolilinea> polilineas)
        {
            G01_Lineal mov;
            List<string> movs = new List<string>();
            
            if(polilineas.Count() > 0)
                //movs.Add("<polilinea>" + Environment.NewLine);

            foreach (IPolilinea p in polilineas)
            {
                Polilinea pi = new Polilinea();

                if (p.Tipo == EntidadTipo.Polilinea)
                {
                    pi = (Polilinea)p;
                }
                if (p.Tipo == EntidadTipo.LightWeightPolyline)
                {
                    pi = ((LightWeightPolyline)p).ToPolilinea();
                }

                int i = 0;
                foreach (PolylineVertex v in pi.Vertexes)
                {
                    mov = new G01_Lineal();

                    mov.Inicio.X = v.Location.X;
                    mov.Inicio.Y = v.Location.Y;
                    mov.Inicio.Z = pi.Elevation;

                    mov.Fin.X = v.Location.X;
                    mov.Fin.Y = v.Location.Y;
                    mov.Fin.Z = pi.Elevation;

                    if (i == 0)
                    {
                        movs.Add(Metodos.IrA(mov.Inicio.X, mov.Inicio.Y, mov.Inicio.Z));
                    }
                    else if (i == 1)
                    {
                        //movs.Add(Metodos.IrA(pi.Vertexes[i - 1].Location.X, pi.Vertexes[i - 1].Location.Y, 0));
                        movs.Add(mov.ToString());
                    }
                    else
                    {
                        movs.Add(Metodos.IrA(pi.Vertexes[i - 1].Location.X, pi.Vertexes[i - 1].Location.Y, pi.Elevation));
                        movs.Add(mov.ToString());
                    }
                    i++;
                }

                if (pi.IsClosed) //si es cerrado, adicionalmente agregamos ir al inicio
                {
                    mov = new G01_Lineal();

                    mov.Inicio.X = pi.Vertexes[0].Location.X;
                    mov.Inicio.Y = pi.Vertexes[0].Location.Y;
                    mov.Inicio.Z = pi.Elevation;

                    mov.Fin.X = pi.Vertexes[0].Location.X;
                    mov.Fin.Y = pi.Vertexes[0].Location.Y;
                    mov.Fin.Z = pi.Elevation;

                    movs.Add(Metodos.IrA(pi.Vertexes[i - 1].Location.X, pi.Vertexes[i - 1].Location.Y, pi.Elevation));
                    movs.Add(mov.ToString());
                }


                
            }

            //if(movs.Count() > 0)
                //movs.Add("</polilinea>" + Environment.NewLine);
            
            return movs;
        }

    }


}


