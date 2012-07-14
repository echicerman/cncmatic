using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G.Objetos
{
    /// <summary>
    /// Clase que representa G01 - Movimiento Lineal
    /// </summary>
    public class G01_Lineal : Gcode
    {
        #region propiedades privadas
        private Punto inicio;
        private Punto fin;
        
        #endregion

        #region constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>G01_Lineal</c>
        /// </summary>
        public G01_Lineal()
        {
            this.inicio = new Punto(0,0,0);
            this.fin = new Punto(0, 0, 0);
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

        
        /// <summary>
        /// Punto de fin
        /// </summary>
        public Punto Fin
        {
            get { return this.fin; }
            set { this.fin = value; }
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

        public float Fin_X
        {
            get { return this.Fin.X; }
            set { this.Fin.X = value; }
        }

        public float Fin_Y
        {
            get { return this.Fin.Y; }
            set { this.Fin.Y = value; }
        }

        public float Fin_Z
        {
            get { return this.Fin.Z; }
            set { this.Fin.Z = value; }
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

            //si algun parametro no es nulo
            if (this.Fin.X != 0 || this.Fin.Y != 0 || this.Fin.Z != 0)
            {
                s += " X" + this.Fin.X.ToString("F4");
                s += " Y" + this.Fin.Y.ToString("F4");
                s += " Z" + this.Fin.Z.ToString("F4");
            }
            else
            {
                s += " Z" + this.Fin.Z.ToString("F4");
            }

            //si se va a generar la linea sumamos el codigo del movimiento
            if (s != "")
                s = this.MoveCode + s;

            return s;
        }
        #endregion

    }
}

