﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G.Objetos
{
   public class G02_ArcoH: Gcode
    {
        #region propiedades privadas
        private Punto inicio;
        private Punto fin;
        private Punto centro;
        //private float radio;
        #endregion

        #region constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>G02_Arco</c>
        /// </summary>
        public G02_ArcoH()
        {
            this.inicio = new Punto(0, 0, 0);
            this.fin = new Punto(0, 0, 0);
            this.centro = new Punto(0, 0, 0);
            //this.radio = 0;
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

        public float Centro_X
        {
            get { return this.centro.X; }
            set { this.centro.X = value; }
        }

        public float Centro_Y
        {
            get { return this.centro.Y; }
            set { this.centro.Y = value; }
        }

        public float Centro_Z
        {
            get { return this.centro.Z; }
            set { this.centro.Z = value; }
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
        /// Punto de centro
        /// </summary>
        public Punto Centro
        {
            get { return this.centro; }
            set { this.centro = value; }
        }

        /// <summary>
        /// Valor del radio
        /// </summary>
        //public float Radio
        //{
        //    get { return this.radio; }
        //    set { this.radio = value; }
        //}
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
            //Dibujo el Arco
            s += this.MoveCode;
            s += " X" + this.Fin.X.ToString();
            s += " Y" + this.Fin.Y.ToString();
            s += " Z" + this.Fin_Z.ToString();
            //s += " R" + this.radio.ToString();
            if (this.Inicio_X > this.Centro.X)
                s += " I-" + (this.Inicio_X - this.Centro.X).ToString();
            else
                s += " I" + (this.Centro.X - this.Inicio_X).ToString();

            if (this.Inicio_Y > this.Centro.Y)
                s += " J-" + (this.Inicio_Y - this.Centro.Y).ToString();
            else
                s += " J" + (this.Centro.Y - this.Inicio_Y).ToString();

            if (this.Inicio_Z > this.Centro.Z)
                s += " K-" + (this.Inicio_Z - this.Centro.Z).ToString();
            else
                s += " K" + (this.Centro.Z - this.Inicio_Z).ToString();

            return s;
        }

        public string ToString2()
        {
            string s = "";
            //Voy al punto de de inicio
            s += "G00 Z" + G.Servicios.Metodos.altoAscenso.ToString() + Environment.NewLine;
            
            s += "G00 X" + this.Inicio.X.ToString();
            s += " Y" + this.Inicio.Y.ToString() + Environment.NewLine;

            s += "G00 Z" + this.Inicio.Z.ToString() + Environment.NewLine;

            //Dibujo el Arco
            s += this.MoveCode;
            s += " X" + this.Fin.X.ToString();
            s += " Y" + this.Fin.Y.ToString();
            s += " Z" + this.Fin_Z.ToString();
            //s += " R" + this.radio.ToString();
            if (this.Inicio_X > this.Centro.X)
                s += " I-" + (this.Inicio_X - this.Centro.X).ToString();
            else
                s += " I" + (this.Centro.X - this.Inicio_X).ToString();

            if (this.Inicio_Y > this.Centro.Y)
                s += " J-" + (this.Inicio_Y - this.Centro.Y).ToString();
            else
                s += " J" + (this.Centro.Y - this.Inicio_Y).ToString();

            if (this.Inicio_Z > this.Centro.Z)
                s += " K-" + (this.Inicio_Z - this.Centro.Z).ToString();
            else
                s += " K" + (this.Centro.Z - this.Inicio_Z).ToString();


            return s;
        }

        #endregion
    }
}
