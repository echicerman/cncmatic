using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace G.Objetos
{
    /// <summary>
    /// Clase que representa G02 - Circulo en sentido horario
    /// </summary>
    public class G02_CirculoH : Gcode
    {
        #region propiedades privadas
        private Punto inicio;
        private float radio;
        #endregion

        #region constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>G02_CirculoH</c>
        /// </summary>
        public G02_CirculoH()
        {
            this.inicio = new Punto(0, 0, 0);
            this.radio = 0;
            this._moveCode = MovesCodes.circuloHorario;
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

        ///// <summary>
        ///// Punto de fin
        ///// </summary>
        //public Punto Fin
        //{
        //    get { return this.fin; }
        //    set { this.fin = value; }
        //}

        /// <summary>
        /// Valor del radio
        /// </summary>
        public float Radio
        {
            get { return this.radio; }
            set { this.radio = value; }
        }
        #endregion

        #region metodos publicos

        #endregion

        #region override
        /// <summary>
        /// Genera el codigo G del movimiento en el caso de que algun parametro no sea 0
        /// </summary>
        /// <returns>El string en G a generar o vacio si no corresponde</returns>
        public override string ToString()
        {
            string s = "";

            //Voy al punto de de inicio (que coincide con el de fin)
            s += "G00 X" + this.Inicio.X.ToString("F4");
            s += " Y" + this.Inicio.Y.ToString("F4") + Environment.NewLine;
            //Dibujo el Circulo
            s += this.MoveCode;
            s += " X" + this.Inicio.X.ToString("F4");
            s += " Y" + this.Inicio.Y.ToString("F4");
            s += " R" + this.radio.ToString("F4");

            return s;
        }
        #endregion

    }

}
