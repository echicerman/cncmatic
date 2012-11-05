using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandPreprocessor
{
    public class UnitsPosition
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
        public UnitsPosition(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public UnitsPosition()
        {
            this.X = this.Y = this.Z = 0;
        }
        #endregion

        #region public
        public string ToString(int code)
        {
            StringBuilder result = new StringBuilder();

            result
                .AppendFormat("G{0:00} ", code)
                .AppendFormat("X{0:0.0000} ", this.X)
                .AppendFormat("Y{0:0.0000} ", this.Y)
                .AppendFormat("Z{0:0.0000} ", this.Z);

            return result.ToString();
        }
        #endregion
    
        #region Conversor
        public StepsPosition ToStepsPosition(UnitsPosition current, double feedrate)//, double curveLength)
        {
            // Delta Position & Delta Steps -> with this movement
            double xDelta = Math.Abs(this.X - current.X);
            double yDelta = Math.Abs(this.Y - current.Y);
            double zDelta = Math.Abs(this.Z - current.Z);
            // creamos el vector EN PASOS del movimiento que va a hacer la punta
            StepsPosition lineSteps = new StepsPosition(
                            Convert.ToInt64(Math.Ceiling(this.X * Configuration.configValueX)),
                            Convert.ToInt64(Math.Ceiling(this.Y * Configuration.configValueY)),
                            Convert.ToInt64(Math.Ceiling(this.Z * Configuration.configValueZ)));
            // delay parameters
            double totalDistance = Math.Sqrt(xDelta * xDelta + yDelta * yDelta + zDelta * zDelta);
            //if (curveLength == -1) curveLength = totalDistance * totalDistance; // To handle lines and curveSections

            long maxDeltaSteps = Convert.ToInt64(Math.Ceiling(Math.Max(xDelta * Configuration.configValueX, yDelta * Configuration.configValueY)));
            maxDeltaSteps = Convert.ToInt64(Math.Ceiling(Math.Max(maxDeltaSteps, zDelta * Configuration.configValueZ)));
            // Calculating Delay: totalDistance * 1 second(microseconds) / speed / maxDelta / 2-changeStates per step / milliseconds
            if (maxDeltaSteps > 0)
            {
                lineSteps.Delay = Convert.ToInt64(Math.Ceiling( totalDistance * 60000000.0 / feedrate / maxDeltaSteps / 2 / 1000)); // time between clock for most dinamyc axis - microseconds
            }
            else
            {
                lineSteps.Delay = 0;
            }
            return lineSteps;
        }
        #endregion

    }
    public class StepsPosition
    { 
        #region Properties
        private long x;
        private long y;
        private long z;
        private long delay;
        #endregion

        #region Getters & Setters
        public long X
        {
            get { return this.x; }
            set { this.x = value; }
        }
        public long Y
        {
            get { return this.y; }
            set { this.y = value; }
        }
        public long Z
        {
            get { return this.z; }
            set { this.z = value; }
        }
        public long Delay
        {
            get { return this.delay; }
            set { this.delay = value; }
        }
        #endregion

        #region Constructor
        public StepsPosition(long x, long y, long z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public StepsPosition()
        {
            this.X = this.Y = this.Z = 0;
        }
        #endregion

        #region public
        public string ToString(int code)
        {
            StringBuilder result = new StringBuilder();

            result
                .AppendFormat(code < 0 ? "G{0} " : "G{0:00} ", code)
                .AppendFormat("X{0} ", this.X)
                .AppendFormat("Y{0} ", this.Y)
                .AppendFormat("Z{0} ", this.Z)
                .AppendFormat("D{0}", this.Delay);

            return result.ToString();
        }
        #endregion
    }
}
