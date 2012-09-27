using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    /// <summary>
    /// Encapsulation of an ellipsoid, and declaration of common reference ellipsoids.
    /// </summary>
    [Serializable]
    public struct Ellipsoid
    {
        /// <summary>Semi major axis (meters).</summary>
        private readonly double mSemiMajorAxis;

        /// <summary>Semi minor axis (meters).</summary>
        private readonly double mSemiMinorAxis;

        /// <summary>Flattening.</summary>
        private readonly double mFlattening;

        /// <summary>Inverse flattening.</summary>
        private readonly double mInverseFlattening;

        /// <summary>
        /// Construct a new Ellipsoid.  This is private to ensure the values are
        /// consistent (flattening = 1.0 / inverseFlattening).  Use the methods 
        /// FromAAndInverseF() and FromAAndF() to create new instances.
        /// </summary>
        /// <param name="semiMajor"></param>
        /// <param name="semiMinor"></param>
        /// <param name="flattening"></param>
        /// <param name="inverseFlattening"></param>
        private Ellipsoid(double semiMajor, double semiMinor, double flattening, double inverseFlattening)
        {
            mSemiMajorAxis = semiMajor;
            mSemiMinorAxis = semiMinor;
            mFlattening = flattening;
            mInverseFlattening = inverseFlattening;
        }

        #region References Ellipsoids
        /// <summary>The WGS84 ellipsoid.</summary>
        static public readonly Ellipsoid WGS84 = FromAAndInverseF(6378137.0, 298.257223563);

        /// <summary>The GRS80 ellipsoid.</summary>
        static public readonly Ellipsoid GRS80 = FromAAndInverseF(6378137.0, 298.257222101);

        /// <summary>The GRS67 ellipsoid.</summary>
        static public readonly Ellipsoid GRS67 = FromAAndInverseF(6378160.0, 298.25);

        /// <summary>The ANS ellipsoid.</summary>
        static public readonly Ellipsoid ANS = FromAAndInverseF(6378160.0, 298.25);

        /// <summary>The WGS72 ellipsoid.</summary>
        static public readonly Ellipsoid WGS72 = FromAAndInverseF(6378135.0, 298.26);

        /// <summary>The Clarke1858 ellipsoid.</summary>
        static public readonly Ellipsoid Clarke1858 = FromAAndInverseF(6378293.645, 294.26);

        /// <summary>The Clarke1880 ellipsoid.</summary>
        static public readonly Ellipsoid Clarke1880 = FromAAndInverseF(6378249.145, 293.465);

        /// <summary>A spherical "ellipsoid".</summary>
        static public readonly Ellipsoid Sphere = FromAAndF(6371000, 0.0);
        #endregion

        /// <summary>
        /// Build an Ellipsoid from the semi major axis measurement and the inverse flattening.
        /// </summary>
        /// <param name="semiMajor">semi major axis (meters)</param>
        /// <param name="inverseFlattening"></param>
        /// <returns></returns>
        static public Ellipsoid FromAAndInverseF(double semiMajor, double inverseFlattening)
        {
            double f = 1.0 / inverseFlattening;
            double b = (1.0 - f) * semiMajor;

            return new Ellipsoid(semiMajor, b, f, inverseFlattening);
        }

        /// <summary>
        /// Build an Ellipsoid from the semi major axis measurement and the flattening.
        /// </summary>
        /// <param name="semiMajor">semi major axis (meters)</param>
        /// <param name="flattening"></param>
        /// <returns></returns>
        static public Ellipsoid FromAAndF(double semiMajor, double flattening)
        {
            double inverseF = 1.0 / flattening;
            double b = (1.0 - flattening) * semiMajor;

            return new Ellipsoid(semiMajor, b, flattening, inverseF);
        }

        /// <summary>Get semi major axis (meters).</summary>
        public double SemiMajorAxis
        {
            get { return mSemiMajorAxis; }
        }

        /// <summary>Get semi minor axis (meters).</summary>
        public double SemiMinorAxis
        {
            get { return mSemiMinorAxis; }
        }

        /// <summary>Get flattening.</summary>
        public double Flattening
        {
            get { return mFlattening; }
        }

        /// <summary>Get inverse flattening.</summary>
        public double InverseFlattening
        {
            get { return mInverseFlattening; }
        }
    }
}
