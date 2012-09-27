using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    [Serializable]
    public struct GeodeticCurve
    {
        /// <summary>Ellipsoidal distance (in meters).</summary>
        private readonly double mEllipsoidalDistance;

        /// <summary>Azimuth (degrees from north).</summary>
        private readonly Angle mAzimuth;

        /// <summary>Reverse azimuth (degrees from north).</summary>
        private readonly Angle mReverseAzimuth;

        /// <summary>
        /// Create a new GeodeticCurve.
        /// </summary>
        /// <param name="ellipsoidalDistance">ellipsoidal distance in meters</param>
        /// <param name="azimuth">azimuth in degrees</param>
        /// <param name="reverseAzimuth">reverse azimuth in degrees</param>
        public GeodeticCurve(double ellipsoidalDistance, Angle azimuth, Angle reverseAzimuth)
        {
            mEllipsoidalDistance = ellipsoidalDistance;
            mAzimuth = azimuth;
            mReverseAzimuth = reverseAzimuth;
        }

        /// <summary>Ellipsoidal distance (in meters).</summary>
        public double EllipsoidalDistance
        {
            get { return mEllipsoidalDistance; }
        }

        /// <summary>
        /// Get the azimuth.  This is angle from north from start to end.
        /// </summary>
        public Angle Azimuth
        {
            get { return mAzimuth; }
        }

        /// <summary>
        /// Get the reverse azimuth.  This is angle from north from end to start.
        /// </summary>
        public Angle ReverseAzimuth
        {
            get { return mReverseAzimuth; }
        }

        /// <summary>
        /// Get curve as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("s=");
            builder.Append(mEllipsoidalDistance);
            builder.Append(";a12=");
            builder.Append(mAzimuth);
            builder.Append(";a21=");
            builder.Append(mReverseAzimuth);
            builder.Append(";");

            return builder.ToString();
        }
    }
}
