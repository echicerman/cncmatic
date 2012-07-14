using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G.Objetos
{
    public class Gcode
    {
        public string _moveCode;

        /// <summary>
        /// Codigo ISO de movimiento de fresado
        /// </summary>
        public string MoveCode
        {
            get { return this._moveCode; }
            set { this._moveCode = value; }
        }

       
    }
}
