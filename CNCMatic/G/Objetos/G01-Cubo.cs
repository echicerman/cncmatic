using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G.Objetos
{
    /// <summary>
    /// Clase que representa G01 - Cubo
    /// </summary>

    public class G01_Cubo : Gcode
    {

        #region propiedades privadas
        private Punto inicio;
        private float alto;
        private float ancho;
        private float largo;

        #endregion

        #region constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>G01_Cubo</c>
        /// </summary>
        public G01_Cubo()
        {
            this.inicio = new Punto(0, 0, 0);
            this.ancho = 0;
            this.alto = 0;
            this.largo = 0;
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

        public float Alto
        {
            get { return this.alto; }
            set { this.alto = value; }
        }

        public float Ancho
        {
            get { return this.ancho; }
            set { this.ancho = value; }
        }

        public float Largo
        {
            get { return this.largo; }
            set { this.largo = value; }
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
            
            //Se mueve al punto inicial
            s += G.Servicios.Metodos.Avance(this.Inicio.X, this.inicio.Y, this.inicio.Z);

            //Dibuja un lado
            s += this.MoveCode;
            s += " X" + (this.inicio.X + this.ancho).ToString() + " Y" + this.inicio.Y.ToString() + " Z" + this.inicio.Z.ToString() + Environment.NewLine;
            s += this.MoveCode;
            s += " X" + (this.inicio.X + this.ancho).ToString() + " Y" + (this.inicio.Y + this.largo).ToString() + " Z" + this.inicio.Z.ToString() + Environment.NewLine;
            s += this.MoveCode;
            s += " X" + this.inicio.X.ToString() + " Y" + (this.inicio.Y + this.largo).ToString() + " Z" + this.inicio.Z.ToString() + Environment.NewLine;
            s += this.MoveCode;
            s += " X" + this.inicio.X.ToString() + " Y" + this.Inicio.Y.ToString() + " Z" + this.inicio.Z.ToString() + Environment.NewLine;

            //Subimos un nivel
            s += this.MoveCode;
            s += " X" + this.inicio.X.ToString() + " Y" + this.inicio.Y.ToString() + " Z" + (this.inicio.Z + this.alto).ToString() + Environment.NewLine;

            //Dibujamos el segundo cuadrado
            s += this.MoveCode;
            s += " X" + (this.inicio.X + this.ancho).ToString() + " Y" + this.inicio.Y.ToString() + " Z" + (this.inicio.Z + this.alto).ToString() + Environment.NewLine;
            s += this.MoveCode;
            s += " X" + (this.inicio.X + this.ancho).ToString() + " Y" + (this.inicio.Y + this.largo).ToString() + " Z" + (this.inicio.Z + this.alto).ToString() + Environment.NewLine;
            s += this.MoveCode;
            s += " X" + this.inicio.X.ToString() + " Y" + (this.inicio.Y + this.largo).ToString() + " Z" + (this.inicio.Z + this.alto).ToString() + Environment.NewLine;
            s += this.MoveCode;
            s += " X" + this.inicio.X.ToString() + " Y" + this.Inicio.Y.ToString() + " Z" + (this.inicio.Z + this.alto).ToString() + Environment.NewLine;

            //Intercalamos los lados faltantes
            s += G.Servicios.Metodos.Avance((this.inicio.X + this.ancho), this.inicio.Y, this.inicio.Z);
            s += this.MoveCode;
            s += " X" + (this.inicio.X + this.ancho).ToString() + " Y" + this.inicio.Y.ToString() + " Z" + (this.inicio.Z + this.alto).ToString() + Environment.NewLine;

            s += G.Servicios.Metodos.Avance((this.inicio.X + this.ancho), (this.inicio.Y + this.largo), this.inicio.Z);


            s += this.MoveCode;
            s += " X" + (this.inicio.X + this.ancho).ToString() + " Y" + (this.inicio.Y + this.largo).ToString() + " Z" + (this.inicio.Z + this.alto).ToString() + Environment.NewLine;

            s += G.Servicios.Metodos.Avance(this.inicio.X, (this.inicio.Y + this.largo), this.inicio.Z);
            
            s += this.MoveCode;
            s += " X" + this.inicio.X.ToString() + " Y" + (this.inicio.Y + this.largo).ToString() + " Z" + (this.inicio.Z + this.alto).ToString() + Environment.NewLine;

            //G00 X1 Y1 Z1 OK
            //G01 X3 Y1 Z1 OK
            //G01 X3 Y3 Z1 OK
            //G01 X1 Y3 Z1 OK
            //G01 X1 Y1 Z1 OK
            //G01 X1 Y1 Z3 OK
            //G01 X3 Y1 Z3 ok
            //G01 X3 Y3 Z3 ok
            //G01 X1 Y3 Z3 ok
            //G01 X1 Y1 Z3 ok
            //G00 X3 Y1 Z1 ok
            //G01 X3 Y1 Z3 ok
            //G00 X3 Y3 Z1 ok
            //G01 X3 Y3 Z3 ok
            //G00 X1 Y3 Z1 ok
            //G01 X1 Y3 Z3 ok


            return s;
        }
        #endregion

    }

}



