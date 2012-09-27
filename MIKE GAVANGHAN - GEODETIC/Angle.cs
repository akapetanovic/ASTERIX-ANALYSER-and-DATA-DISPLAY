using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    [Serializable]
    public struct Angle : IComparable<Angle>
    {
        /// <summary>Degrees/Radians conversion constant.</summary>
        private const double PiOver180 = Math.PI / 180.0;

        /// <summary>Angle value in degrees.</summary>
        private double mDegrees;

        /// <summary>Zero Angle</summary>
        static public readonly Angle Zero = new Angle(0);

        /// <summary>180 degree Angle</summary>
        static public readonly Angle Angle180 = new Angle(180);

        /// <summary>
        /// Construct a new Angle from a degree measurement.
        /// </summary>
        /// <param name="degrees">angle measurement</param>
        public Angle(double degrees)
        {
            mDegrees = degrees;
        }

        /// <summary>
        /// Construct a new Angle from degrees and minutes.
        /// </summary>
        /// <param name="degrees">degree portion of angle measurement</param>
        /// <param name="minutes">minutes portion of angle measurement (0 <= minutes < 60)</param>
        public Angle(int degrees, double minutes)
        {
            mDegrees = minutes / 60.0;

            mDegrees = (degrees < 0) ? (degrees - mDegrees) : (degrees + mDegrees);
        }

        /// <summary>
        /// Construct a new Angle from degrees, minutes, and seconds.
        /// </summary>
        /// <param name="degrees">degree portion of angle measurement</param>
        /// <param name="minutes">minutes portion of angle measurement (0 <= minutes < 60)</param>
        /// <param name="seconds">seconds portion of angle measurement (0 <= seconds < 60)</param>
        public Angle(int degrees, int minutes, double seconds)
        {
            mDegrees = (seconds / 3600.0) + (minutes / 60.0);

            mDegrees = (degrees < 0) ? (degrees - mDegrees) : (degrees + mDegrees);
        }

        /// <summary>
        /// Get/set angle measured in degrees.
        /// </summary>
        public double Degrees
        {
            get { return mDegrees; }
            set { mDegrees = value; }
        }

        /// <summary>
        /// Get/set angle measured in radians.
        /// </summary>
        public double Radians
        {
            get { return mDegrees * PiOver180; }
            set { mDegrees = value / PiOver180; }
        }

        /// <summary>
        /// Get the absolute value of the angle.
        /// </summary>
        public Angle Abs()
        {
            return new Angle(Math.Abs(mDegrees));
        }

        /// <summary>
        /// Compare this angle to another angle.
        /// </summary>
        /// <param name="other">other angle to compare to.</param>
        /// <returns>result according to IComparable contract/></returns>
        public int CompareTo(Angle other)
        {
            return mDegrees.CompareTo(other.mDegrees);
        }

        /// <summary>
        /// Calculate a hash code for the angle.
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return (int)(mDegrees * 1000033);
        }

        /// <summary>
        /// Compare this Angle to another Angle for equality.  Angle comparisons
        /// are performed in absolute terms - no "wrapping" occurs.  In other
        /// words, 360 degress != 0 degrees.
        /// </summary>
        /// <param name="obj">object to compare to</param>
        /// <returns>'true' if angles are equal</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Angle)) return false;

            Angle other = (Angle)obj;

            return mDegrees == other.mDegrees;
        }

        /// <summary>
        /// Get coordinates as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return mDegrees.ToString();
        }

        #region Operators
        public static Angle operator +(Angle lhs, Angle rhs)
        {
            return new Angle(lhs.mDegrees + rhs.mDegrees);
        }

        public static Angle operator -(Angle lhs, Angle rhs)
        {
            return new Angle(lhs.mDegrees - rhs.mDegrees);
        }

        public static bool operator >(Angle lhs, Angle rhs)
        {
            return lhs.mDegrees > rhs.mDegrees;
        }

        public static bool operator >=(Angle lhs, Angle rhs)
        {
            return lhs.mDegrees >= rhs.mDegrees;
        }

        public static bool operator <(Angle lhs, Angle rhs)
        {
            return lhs.mDegrees < rhs.mDegrees;
        }

        public static bool operator <=(Angle lhs, Angle rhs)
        {
            return lhs.mDegrees <= rhs.mDegrees;
        }

        public static bool operator ==(Angle lhs, Angle rhs)
        {
            return lhs.mDegrees == rhs.mDegrees;
        }

        public static bool operator !=(Angle lhs, Angle rhs)
        {
            return lhs.mDegrees != rhs.mDegrees;
        }

        /// <summary>
        /// Imlplicity cast a double as an Angle measured in degrees.
        /// </summary>
        /// <param name="degrees">angle in degrees</param>
        /// <returns>double cast as an Angle</returns>
        public static implicit operator Angle(double degrees)
        {
            return new Angle(degrees);
        }
        #endregion
    }
}
