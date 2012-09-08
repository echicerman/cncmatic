using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G.Objetos
{
    /// <summary>
    /// Clase que representa G00 - Avance Rapido
    /// </summary>
    public class G00_Avance : Gcode
    {
        #region propiedades privadas
        private float x;
        private float y;
        private float z;
        #endregion

        #region constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>G00_Avance</c>
        /// </summary>
        public G00_Avance()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
            this._moveCode = MovesCodes.avance;
        }

        #endregion

        #region propiedades publicas
        /// <summary>
        /// Valor de avance en X
        /// </summary>
        public float X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        /// <summary>
        /// Valor de avance en Y
        /// </summary>
        public float Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        /// <summary>
        /// Valor de avance en Z
        /// </summary>
        public float Z
        {
            get { return this.z; }
            set { this.z = value; }
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

            if (this.x + this.y + this.z == 0)
            {
                s += " X" + x.ToString();
                s += " Y" + y.ToString();
                s += " Z" + z.ToString();
            }
            else
            {
                //revisamos cada uno de los parametros del movimiento
                if (this.x != 0)
                    s += " X" + x.ToString();
                if (this.y != 0)
                    s += " Y" + y.ToString();
                if (this.z != 0)
                    s += " Z" + z.ToString();
            }
            //si se va a generar la linea sumamos el codigo del movimiento
            if (s != "")
                s = this.MoveCode + s;

            return s;
        }
        #endregion

    }
}
