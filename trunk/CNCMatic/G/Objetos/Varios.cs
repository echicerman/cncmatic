using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G.Objetos
{
    public class Punto
    {
        private float x;
        private float y;
        private float z;

        public Punto()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }


        public Punto(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Valor de coordenada X
        /// </summary>
        public float X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        /// <summary>
        /// Valor de coordenada Y
        /// </summary>
        public float Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        /// <summary>
        /// Valor de coordenada Z
        /// </summary>
        public float Z
        {
            get { return this.z; }
            set { this.z = value; }
        }

    }
}
