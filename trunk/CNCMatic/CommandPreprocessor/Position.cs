using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandPreprocessor
{
    public class Position
    {
        #region Properties
        private double x;
        private double y;
        private double z;
        #endregion

        #region Getters & Setters
        public double X
        {
            get { return this.x; }
            set { this.x = value; }
        }
        public double Y
        {
            get { return this.y; }
            set { this.y = value; }
        }
        public double Z
        {
            get { return this.z; }
            set { this.z = value; }
        }
        #endregion

        #region Constructor
        public Position(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public Position()
        {
            this.X = this.Y = this.Z = 0;
        }
        #endregion
    
        #region public
        public string ToString(int code)
        {
            StringBuilder result = new StringBuilder();

            result
                .AppendFormat("G{0} ", code)
                .AppendFormat("X{0:0.0000} ", this.X)
                .AppendFormat("Y{0:0.0000} ", this.Y)
                .AppendFormat("Z{0:0.0000} ", this.Z);

            return result.ToString();
        }
        #endregion
    }
}
