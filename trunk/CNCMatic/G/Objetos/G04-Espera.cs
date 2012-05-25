using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G.Objetos
{
    public class G04_Espera: Gcode
    {
        #region propiedades privadas
        long tiempo;
        #endregion

        /// <summary>
        /// Inicializa una instancia de la clase M00_Parada
        /// </summary>
        public G04_Espera()
        {
            this.tiempo = 0;
            this._moveCode = MovesCodes.fin;

        }

        #region propiedades publicas
        /// <summary>
        /// Tiempo de espera
        /// </summary>
        public long Tiempo
        {
            get { return this.tiempo; }
            set { this.tiempo = value; }
        }
        #endregion

        #region override
        /// <summary>
        /// Genera el codigo G del movimiento M02
        /// </summary>
        /// <returns>El string en G a generar</returns>
        public override string ToString()
        {
            string s = "";
            
            //generamos la linea de stop
            s = this.MoveCode;

            return s;
        }
        #endregion
    }
}
