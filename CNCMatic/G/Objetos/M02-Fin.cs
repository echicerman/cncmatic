using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G.Objetos
{
    public class M02_Fin: Gcode
    {
        #region propiedades privadas
        #endregion

        /// <summary>
        /// Inicializa una instancia de la clase M00_Parada
        /// </summary>
        public M02_Fin()
        {
            this._moveCode = MovesCodes.fin;

        }


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
