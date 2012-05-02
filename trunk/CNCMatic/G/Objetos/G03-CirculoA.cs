﻿using System;
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
        private Punto fin;
        private float radio;
        #endregion

        #region constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>G02_CirculoH</c>
        /// </summary>
        public G03_CirculoA()
        {
            this.inicio = new Punto(0, 0, 0);
            this.fin = new Punto(0, 0, 0);
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
        /// Punto de fin
        /// </summary>
        public Punto Fin
        {
            get { return this.fin; }
            set { this.fin = value; }
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

            //si algun parametro no es nulo
            if (this.Fin.X != 0 || this.Fin.Y != 0 || this.Fin.Z != 0)
            {
                s += " X" + this.Fin.X.ToString("F4");
                s += " Y" + this.Fin.Y.ToString("F4");
                s += " R" + this.radio.ToString("F4");
            }

            //si se va a generar la linea sumamos el codigo del movimiento
            if (s != "")
                s = this.MoveCode + s;

            return s;
        }
        #endregion

    }

}
