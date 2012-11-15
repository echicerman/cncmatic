using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G.Objetos
{
        /// <summary>
        /// Clase que representa G01 - Cuadrado
        /// </summary>
        public class G01_Cuadrado : Gcode
        {
            #region propiedades privadas
            private Punto inicio;
            private float lado;

            #endregion

            #region constructores

            /// <summary>
            /// Inicializa una nueva instancia de la clase <c>G01_Cuadrado</c>
            /// </summary>
            public G01_Cuadrado()
            {
                this.inicio = new Punto(0, 0, 0);
                this.lado = 0;
                this._moveCode = MovesCodes.lineal;
            }

            #endregion

            #region propiedades publicas
            /// <summary>
            /// Punto de inicio
            /// </summary>
            public Punto Inicio
            {
                get { return this.inicio; }
                set { this.inicio = value; }
            }

            public float Inicio_X
            {
                get { return this.Inicio.X; }
                set { this.Inicio.X = value; }
            }

            public float Inicio_Y
            {
                get { return this.Inicio.Y; }
                set { this.Inicio.Y = value; }
            }

            public float Inicio_Z
            {
                get { return this.Inicio.Z; }
                set { this.Inicio.Z = value; }
            }

            public float Lado
            {
                get { return this.lado; }
                set { this.lado = value; }
            }

            #endregion

            #region metodos publicos

            #endregion

            #region override
            /// <summary>
            /// Genera el codigo G del movimiento hacia el punto final en el caso de que algun parametro no sea 0
            /// </summary>
            /// <returns>El string en G a generar o vacio si no corresponde</returns>
            public override string ToString()
            {
                string s = "";

                //subimos la punta
                s += "G00 Z" + G.Servicios.Metodos.altoAscenso + Environment.NewLine;
                //avanzamos al inicio
                s += "G00 X" + this.Inicio_X.ToString() + " Y" + this.Inicio_Y.ToString() + Environment.NewLine;
                //bajamos la punta
                s += "G00 Z" + this.Inicio_Z.ToString() + Environment.NewLine;


                //Dibuja un lado
                s += this.MoveCode;
                s += " X" + this.inicio.X.ToString() + " Y" + (this.inicio.Y+this.lado).ToString() + Environment.NewLine;
                s += this.MoveCode;
                s += " X" + (this.inicio.X + this.lado).ToString() + " Y" + (this.inicio.Y + this.lado).ToString() + Environment.NewLine;
                s += this.MoveCode;
                s += " X" + (this.inicio.X + this.lado).ToString() + " Y" + this.inicio.Y.ToString() + Environment.NewLine;
                s += this.MoveCode;
                s += " X" + this.inicio.X.ToString() + " Y" + this.Inicio.Y.ToString();

                //si se va a generar la linea sumamos el codigo del movimiento
                //if (s != "")
                //    s = this.MoveCode + s;

                return s;
            }
            #endregion

        }
    }
