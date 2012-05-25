using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G.Objetos
{
    public class M00_Parada: Gcode
    {
        #region propiedades privadas
        #endregion

        /// <summary>
        /// Inicializa una instancia de la clase M00_Parada
        /// </summary>
        public M00_Parada()
        {
            this._moveCode = MovesCodes.parada;

        }


        #region override
        /// <summary>
        /// Genera el codigo G del movimiento M00
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
