using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    [Serializable]
    public struct GlobalCoordinates : IComparable<GlobalCoordinates>
    {
        /// <summary>Latitude.  Negative latitude is southern hemisphere.</summary>
        private Angle mLatitude;

        /// <summary>Longitude.  Negative longitude is western hemisphere.</summary>
        private Angle mLongitude;

        /// <summary>
        /// Canonicalize the current latitude and longitude values such that:
        /// 
        ///      -90 <= latitude <= +90
        ///     -180 < longitude <= +180
        /// </summary>
        private void Canonicalize()
        {
            double latitude = mLatitude.Degrees;
            double longitude = mLongitude.Degrees;

            latitude = (latitude + 180) % 360;
            if (latitude < 0) latitude += 360;
            latitude -= 180;

            if (latitude > 90)
            {
                latitude = 180 - latitude;
                longitude += 180;
            }
            else if (latitude < -90)
            {
                latitude = -180 - latitude;
                longitude += 180;
            }

            longitude = ((longitude + 180) % 360);
            if (longitude <= 0) longitude += 360;
            longitude -= 180;

            mLatitude = new Angle(latitude);
            mLongitude = new Angle(longitude);
        }

        /// <summary>
        /// Construct a new GlobalCoordinates.  Angles will be canonicalized.
        /// </summary>
        /// <param name="latitude">latitude</param>
        /// <param name="longitude">longitude</param>
        public GlobalCoordinates(Angle latitude, Angle longitude)
        {
            mLatitude = latitude;
            mLongitude = longitude;
            Canonicalize();
        }

        /// <summary>
        /// Get/set latitude.  The latitude value will be canonicalized (which might
        /// result in a change to the longitude). Negative latitude is southern hemisphere.
        /// </summary>
        public Angle Latitude
        {
            get { return mLatitude; }
            set
            {
                mLatitude = value;
                Canonicalize();
            }
        }

        /// <summary>
        /// Get/set longitude.  The longitude value will be canonicalized. Negative
        /// longitude is western hemisphere.
        /// </summary>
        public Angle Longitude
        {
            get { return mLongitude; }
            set
            {
                mLongitude = value;
                Canonicalize();
            }
        }

        /// <summary>
        /// Compare these coordinates to another set of coordiates.  Western
        /// longitudes are less than eastern logitudes.  If longitudes are equal,
        /// then southern latitudes are less than northern latitudes.
        /// </summary>
        /// <param name="other">instance to compare to</param>
        /// <returns>-1, 0, or +1 as per IComparable contract</returns>
        public int CompareTo(GlobalCoordinates other)
        {
            int retval;

            if (mLongitude < other.mLongitude) retval = -1;
            else if (mLongitude > other.mLongitude) retval = +1;
            else if (mLatitude < other.mLatitude) retval = -1;
            else if (mLatitude > other.mLatitude) retval = +1;
            else retval = 0;

            return retval;
        }

        /// <summary>
        /// Get a hash code for these coordinates.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ((int)(mLongitude.GetHashCode() * (mLatitude.GetHashCode() + 1021))) * 1000033;
        }

        /// <summary>
        /// Compare these coordinates to another object for equality.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is GlobalCoordinates)) return false;

            GlobalCoordinates other = (GlobalCoordinates)obj;

            return (mLongitude == other.mLongitude) && (mLatitude == other.mLatitude);
        }

        /// <summary>
        /// Get coordinates as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(mLatitude.Abs().ToString());
            builder.Append((mLatitude >= Angle.Zero) ? 'N' : 'S');
            builder.Append(';');
            builder.Append(mLongitude.Abs().ToString());
            builder.Append((mLongitude >= Angle.Zero) ? 'E' : 'W');
            builder.Append(';');

            return builder.ToString();
        }
    }
}
