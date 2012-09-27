using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    public class GeodeticCalculator
    {
        private const double TwoPi = 2.0 * Math.PI;

        /// <summary>
        /// Calculate the destination and final bearing after traveling a specified
        /// distance, and a specified starting bearing, for an initial location.
        /// This is the solution to the direct geodetic problem.
        /// </summary>
        /// <param name="ellipsoid">reference ellipsoid to use</param>
        /// <param name="start">starting location</param>
        /// <param name="startBearing">starting bearing (degrees)</param>
        /// <param name="distance">distance to travel (meters)</param>
        /// <param name="endBearing">bearing at destination (degrees)</param>
        /// <returns></returns>
        public GlobalCoordinates CalculateEndingGlobalCoordinates(Ellipsoid ellipsoid, GlobalCoordinates start, Angle startBearing, double distance, out Angle endBearing)
        {
            double a = ellipsoid.SemiMajorAxis;
            double b = ellipsoid.SemiMinorAxis;
            double aSquared = a * a;
            double bSquared = b * b;
            double f = ellipsoid.Flattening;
            double phi1 = start.Latitude.Radians;
            double alpha1 = startBearing.Radians;
            double cosAlpha1 = Math.Cos(alpha1);
            double sinAlpha1 = Math.Sin(alpha1);
            double s = distance;
            double tanU1 = (1.0 - f) * Math.Tan(phi1);
            double cosU1 = 1.0 / Math.Sqrt(1.0 + tanU1 * tanU1);
            double sinU1 = tanU1 * cosU1;

            // eq. 1
            double sigma1 = Math.Atan2(tanU1, cosAlpha1);

            // eq. 2
            double sinAlpha = cosU1 * sinAlpha1;

            double sin2Alpha = sinAlpha * sinAlpha;
            double cos2Alpha = 1 - sin2Alpha;
            double uSquared = cos2Alpha * (aSquared - bSquared) / bSquared;

            // eq. 3
            double A = 1 + (uSquared / 16384) * (4096 + uSquared * (-768 + uSquared * (320 - 175 * uSquared)));

            // eq. 4
            double B = (uSquared / 1024) * (256 + uSquared * (-128 + uSquared * (74 - 47 * uSquared)));

            // iterate until there is a negligible change in sigma
            double deltaSigma;
            double sOverbA = s / (b * A);
            double sigma = sOverbA;
            double sinSigma;
            double prevSigma = sOverbA;
            double sigmaM2;
            double cosSigmaM2;
            double cos2SigmaM2;

            for (; ; )
            {
                // eq. 5
                sigmaM2 = 2.0 * sigma1 + sigma;
                cosSigmaM2 = Math.Cos(sigmaM2);
                cos2SigmaM2 = cosSigmaM2 * cosSigmaM2;
                sinSigma = Math.Sin(sigma);
                double cosSignma = Math.Cos(sigma);

                // eq. 6
                deltaSigma = B * sinSigma * (cosSigmaM2 + (B / 4.0) * (cosSignma * (-1 + 2 * cos2SigmaM2)
                    - (B / 6.0) * cosSigmaM2 * (-3 + 4 * sinSigma * sinSigma) * (-3 + 4 * cos2SigmaM2)));

                // eq. 7
                sigma = sOverbA + deltaSigma;

                // break after converging to tolerance
                if (Math.Abs(sigma - prevSigma) < 0.0000000000001) break;

                prevSigma = sigma;
            }

            sigmaM2 = 2.0 * sigma1 + sigma;
            cosSigmaM2 = Math.Cos(sigmaM2);
            cos2SigmaM2 = cosSigmaM2 * cosSigmaM2;

            double cosSigma = Math.Cos(sigma);
            sinSigma = Math.Sin(sigma);

            // eq. 8
            double phi2 = Math.Atan2(sinU1 * cosSigma + cosU1 * sinSigma * cosAlpha1,
                                     (1.0 - f) * Math.Sqrt(sin2Alpha + Math.Pow(sinU1 * sinSigma - cosU1 * cosSigma * cosAlpha1, 2.0)));

            // eq. 9
            // This fixes the pole crossing defect spotted by Matt Feemster.  When a path
            // passes a pole and essentially crosses a line of latitude twice - once in
            // each direction - the longitude calculation got messed up.  Using Atan2
            // instead of Atan fixes the defect.  The change is in the next 3 lines.
            //double tanLambda = sinSigma * sinAlpha1 / (cosU1 * cosSigma - sinU1*sinSigma*cosAlpha1);
            //double lambda = Math.Atan(tanLambda);
            double lambda = Math.Atan2(sinSigma * sinAlpha1, cosU1 * cosSigma - sinU1 * sinSigma * cosAlpha1);

            // eq. 10
            double C = (f / 16) * cos2Alpha * (4 + f * (4 - 3 * cos2Alpha));

            // eq. 11
            double L = lambda - (1 - C) * f * sinAlpha * (sigma + C * sinSigma * (cosSigmaM2 + C * cosSigma * (-1 + 2 * cos2SigmaM2)));

            // eq. 12
            double alpha2 = Math.Atan2(sinAlpha, -sinU1 * sinSigma + cosU1 * cosSigma * cosAlpha1);

            // build result
            Angle latitude = new Angle();
            Angle longitude = new Angle();

            latitude.Radians = phi2;
            longitude.Radians = start.Longitude.Radians + L;

            endBearing = new Angle();
            endBearing.Radians = alpha2;

            return new GlobalCoordinates(latitude, longitude);
        }

        /// <summary>
        /// Calculate the destination after traveling a specified distance, and a
        /// specified starting bearing, for an initial location. This is the
        /// solution to the direct geodetic problem.
        /// </summary>
        /// <param name="ellipsoid">reference ellipsoid to use</param>
        /// <param name="start">starting location</param>
        /// <param name="startBearing">starting bearing (degrees)</param>
        /// <param name="distance">distance to travel (meters)</param>
        /// <returns></returns>
        public GlobalCoordinates CalculateEndingGlobalCoordinates(Ellipsoid ellipsoid, GlobalCoordinates start, Angle startBearing, double distance)
        {
            Angle endBearing = new Angle();

            return CalculateEndingGlobalCoordinates(ellipsoid, start, startBearing, distance, out endBearing);
        }

        /// <summary>
        /// Calculate the geodetic curve between two points on a specified reference ellipsoid.
        /// This is the solution to the inverse geodetic problem.
        /// </summary>
        /// <param name="ellipsoid">reference ellipsoid to use</param>
        /// <param name="start">starting coordinates</param>
        /// <param name="end">ending coordinates </param>
        /// <returns></returns>
        public GeodeticCurve CalculateGeodeticCurve(Ellipsoid ellipsoid, GlobalCoordinates start, GlobalCoordinates end)
        {
            //
            // All equation numbers refer back to Vincenty's publication:
            // See http://www.ngs.noaa.gov/PUBS_LIB/inverse.pdf
            //

            // get constants
            double a = ellipsoid.SemiMajorAxis;
            double b = ellipsoid.SemiMinorAxis;
            double f = ellipsoid.Flattening;

            // get parameters as radians
            double phi1 = start.Latitude.Radians;
            double lambda1 = start.Longitude.Radians;
            double phi2 = end.Latitude.Radians;
            double lambda2 = end.Longitude.Radians;

            // calculations
            double a2 = a * a;
            double b2 = b * b;
            double a2b2b2 = (a2 - b2) / b2;

            double omega = lambda2 - lambda1;

            double tanphi1 = Math.Tan(phi1);
            double tanU1 = (1.0 - f) * tanphi1;
            double U1 = Math.Atan(tanU1);
            double sinU1 = Math.Sin(U1);
            double cosU1 = Math.Cos(U1);

            double tanphi2 = Math.Tan(phi2);
            double tanU2 = (1.0 - f) * tanphi2;
            double U2 = Math.Atan(tanU2);
            double sinU2 = Math.Sin(U2);
            double cosU2 = Math.Cos(U2);

            double sinU1sinU2 = sinU1 * sinU2;
            double cosU1sinU2 = cosU1 * sinU2;
            double sinU1cosU2 = sinU1 * cosU2;
            double cosU1cosU2 = cosU1 * cosU2;

            // eq. 13
            double lambda = omega;

            // intermediates we'll need to compute 's'
            double A = 0.0;
            double B = 0.0;
            double sigma = 0.0;
            double deltasigma = 0.0;
            double lambda0;
            bool converged = false;

            for (int i = 0; i < 20; i++)
            {
                lambda0 = lambda;

                double sinlambda = Math.Sin(lambda);
                double coslambda = Math.Cos(lambda);

                // eq. 14
                double sin2sigma = (cosU2 * sinlambda * cosU2 * sinlambda) + Math.Pow(cosU1sinU2 - sinU1cosU2 * coslambda, 2.0);
                double sinsigma = Math.Sqrt(sin2sigma);

                // eq. 15
                double cossigma = sinU1sinU2 + (cosU1cosU2 * coslambda);

                // eq. 16
                sigma = Math.Atan2(sinsigma, cossigma);

                // eq. 17    Careful!  sin2sigma might be almost 0!
                double sinalpha = (sin2sigma == 0) ? 0.0 : cosU1cosU2 * sinlambda / sinsigma;
                double alpha = Math.Asin(sinalpha);
                double cosalpha = Math.Cos(alpha);
                double cos2alpha = cosalpha * cosalpha;

                // eq. 18    Careful!  cos2alpha might be almost 0!
                double cos2sigmam = cos2alpha == 0.0 ? 0.0 : cossigma - 2 * sinU1sinU2 / cos2alpha;
                double u2 = cos2alpha * a2b2b2;

                double cos2sigmam2 = cos2sigmam * cos2sigmam;

                // eq. 3
                A = 1.0 + u2 / 16384 * (4096 + u2 * (-768 + u2 * (320 - 175 * u2)));

                // eq. 4
                B = u2 / 1024 * (256 + u2 * (-128 + u2 * (74 - 47 * u2)));

                // eq. 6
                deltasigma = B * sinsigma * (cos2sigmam + B / 4 * (cossigma * (-1 + 2 * cos2sigmam2) - B / 6 * cos2sigmam * (-3 + 4 * sin2sigma) * (-3 + 4 * cos2sigmam2)));

                // eq. 10
                double C = f / 16 * cos2alpha * (4 + f * (4 - 3 * cos2alpha));

                // eq. 11 (modified)
                lambda = omega + (1 - C) * f * sinalpha * (sigma + C * sinsigma * (cos2sigmam + C * cossigma * (-1 + 2 * cos2sigmam2)));

                // see how much improvement we got
                double change = Math.Abs((lambda - lambda0) / lambda);

                if ((i > 1) && (change < 0.0000000000001))
                {
                    converged = true;
                    break;
                }
            }

            // eq. 19
            double s = b * A * (sigma - deltasigma);
            Angle alpha1;
            Angle alpha2;

            // didn't converge?  must be N/S
            if (!converged)
            {
                if (phi1 > phi2)
                {
                    alpha1 = Angle.Angle180;
                    alpha2 = Angle.Zero;
                }
                else if (phi1 < phi2)
                {
                    alpha1 = Angle.Zero;
                    alpha2 = Angle.Angle180;
                }
                else
                {
                    alpha1 = new Angle(Double.NaN);
                    alpha2 = new Angle(Double.NaN);
                }
            }

            // else, it converged, so do the math
            else
            {
                double radians;
                alpha1 = new Angle();
                alpha2 = new Angle();

                // eq. 20
                radians = Math.Atan2(cosU2 * Math.Sin(lambda), (cosU1sinU2 - sinU1cosU2 * Math.Cos(lambda)));
                if (radians < 0.0) radians += TwoPi;
                alpha1.Radians = radians;

                // eq. 21
                radians = Math.Atan2(cosU1 * Math.Sin(lambda), (-sinU1cosU2 + cosU1sinU2 * Math.Cos(lambda))) + Math.PI;
                if (radians < 0.0) radians += TwoPi;
                alpha2.Radians = radians;
            }

            if (alpha1 >= 360.0) alpha1 -= 360.0;
            if (alpha2 >= 360.0) alpha2 -= 360.0;

            return new GeodeticCurve(s, alpha1, alpha2);
        }

        /// <summary>
        /// Calculate the three dimensional geodetic measurement between two positions
        /// measured in reference to a specified ellipsoid.
        /// 
        /// This calculation is performed by first computing a new ellipsoid by expanding or contracting
        /// the reference ellipsoid such that the new ellipsoid passes through the average elevation
        /// of the two positions.  A geodetic curve across the new ellisoid is calculated.  The
        /// point-to-point distance is calculated as the hypotenuse of a right triangle where the length
        /// of one side is the ellipsoidal distance and the other is the difference in elevation.
        /// </summary>
        /// <param name="refEllipsoid">reference ellipsoid to use</param>
        /// <param name="start">starting position</param>
        /// <param name="end">ending position</param>
        /// <returns></returns>
        public GeodeticMeasurement CalculateGeodeticMeasurement(Ellipsoid refEllipsoid, GlobalPosition start, GlobalPosition end)
        {
            // get the coordinates
            GlobalCoordinates startCoords = start.Coordinates;
            GlobalCoordinates endCoords = end.Coordinates;

            // calculate elevation differences
            double elev1 = start.Elevation;
            double elev2 = end.Elevation;
            double elev12 = (elev1 + elev2) / 2.0;

            // calculate latitude differences
            double phi1 = startCoords.Latitude.Radians;
            double phi2 = endCoords.Latitude.Radians;
            double phi12 = (phi1 + phi2) / 2.0;

            // calculate a new ellipsoid to accommodate average elevation
            double refA = refEllipsoid.SemiMajorAxis;
            double f = refEllipsoid.Flattening;
            double a = refA + elev12 * (1.0 + f * Math.Sin(phi12));
            Ellipsoid ellipsoid = Ellipsoid.FromAAndF(a, f);

            // calculate the curve at the average elevation
            GeodeticCurve averageCurve = CalculateGeodeticCurve(ellipsoid, startCoords, endCoords);

            // return the measurement
            return new GeodeticMeasurement(averageCurve, elev2 - elev1);
        }
    }
}
