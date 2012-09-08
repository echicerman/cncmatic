using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G.Objetos
{
    /// <summary>
    /// Clase que representa G03 - Circulo en sentido antihorario
    /// </summary>
    public class G03_CirculoA : Gcode
    {
        #region propiedades privadas
        private Punto inicio;
        private float radio;
        #endregion

        #region constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>G03_CirculoA</c>
        /// </summary>
        public G03_CirculoA()
        {
            this.inicio = new Punto(0, 0, 0);
            this.radio = 0;
            this._moveCode = MovesCodes.circuloAntihorario;
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
            //s += "G00 X" + this.Inicio.X.ToString();
            //s += " Y" + this.Inicio.Y.ToString() + Environment.NewLine;
            //Dibujo el Circulo
            s += this.MoveCode;
            s += " X" + this.Inicio.X.ToString();
            s += " Y" + this.Inicio.Y.ToString();
            s += " R" + (this.radio).ToString();

            return s;
        }
        #endregion

    }

}
