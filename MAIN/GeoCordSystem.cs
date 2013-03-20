using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class GeoCordSystemDegMinSecUtilities
    {
        // Define the earth mean radius.
        const double EarthRadius = 6371.0;
        const double DegreesToRadians = Math.PI / 180.0;
        const double RadiansToDegrees = 180.0 / Math.PI;
        const double NMtoKM = 1.852;

        public enum LatLongPrefix { N, E, W, S, Not_Valid };

        ///////////////////////////////////////////////////////////////////////////////////////
        // Define data type for the Lat/Long  expressed in Deg Min Sec <prefixed with, N, E, W, S>
        public class DegMinSecType
        {
            public int Deg;
            public int Min;
            public double Sec;
            public LatLongPrefix Prefix = LatLongPrefix.Not_Valid;

            public DegMinSecType()
            {
                Deg = 0;
                Min = 0;
                Sec = 0;
                Prefix = LatLongPrefix.Not_Valid;
            }

            public DegMinSecType(int Deg_In, int Min_In, double Sec_In, LatLongPrefix Prefix_In)
            {
                Deg = Deg_In;
                Min = Min_In;
                Sec = Sec_In;
                Prefix = Prefix_In;
            }
        }

        // Lat/Long value expressed in deg min sec
        public class LatLongDegMinSec
        {
            public DegMinSecType Latitude = new DegMinSecType();
            public DegMinSecType Longitude = new DegMinSecType();
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        // Define data type for the Lat/Long expressed in decimal 
        public class LatLongDecimal
        {
            public double LatitudeDecimal;
            public double LongitudeDecimal;

            public LatLongDecimal()
            {
                LatitudeDecimal = 0.0;
                LongitudeDecimal = 0.0;
            }

            public LatLongDecimal(double Latitude, double Longitude)
            {
                LatitudeDecimal = Latitude;
                LongitudeDecimal = Longitude;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        // This class encapsulates Lat/Long in both
        // forms: DecMinSec and Decimal. Anytime a new value is
        // entered it will automatically convert to the other format
        // It will also allow for creating a new Lat/Long based on the
        // range/bearing.
        public class LatLongClass
        {
            private LatLongDegMinSec DegMinSec = new LatLongDegMinSec();
            private LatLongDecimal Decimal = new LatLongDecimal();

            public LatLongDegMinSec GetDegMinSec()
            {
                return DegMinSec;
            }

            public LatLongDecimal GetLatLongDecimal()
            {
                return Decimal;
            }

            // Default constructor
            public LatLongClass()
            {
                DegMinSec.Latitude.Deg = 0;
                DegMinSec.Latitude.Min = 0;
                DegMinSec.Latitude.Sec = 0.0;
                DegMinSec.Latitude.Prefix = LatLongPrefix.Not_Valid;
                DegMinSec.Longitude.Deg = 0;
                DegMinSec.Longitude.Min = 0;
                DegMinSec.Longitude.Sec = 0;
                DegMinSec.Longitude.Prefix = LatLongPrefix.Not_Valid;
                Decimal = ConvertDegMinSecToDecimal(DegMinSec);
            }

            ////////////////////////////////////////////////////////////////////////////
            // Constructor which allows intialization using
            // Deg Min Sec format
            public LatLongClass(int LatDeg, int LatMin, double LatSec, LatLongPrefix LatPrefix,
                int LonDeg, int LonMin, double LonSec, LatLongPrefix LonPrefix)
            {
                DegMinSec.Latitude.Deg = LatDeg;
                DegMinSec.Latitude.Min = LatMin;
                DegMinSec.Latitude.Sec = LatSec;
                DegMinSec.Latitude.Prefix = LatPrefix;
                DegMinSec.Longitude.Deg = LonDeg;
                DegMinSec.Longitude.Min = LonMin;
                DegMinSec.Longitude.Sec = LonSec;
                DegMinSec.Longitude.Prefix = LonPrefix;
                Decimal = ConvertDegMinSecToDecimal(DegMinSec);
            }

            public LatLongClass(LatLongDegMinSec LatLong_InDegMinSecPrefix)
            {
                DegMinSec.Latitude.Deg = LatLong_InDegMinSecPrefix.Latitude.Deg;
                DegMinSec.Latitude.Min = LatLong_InDegMinSecPrefix.Latitude.Min;
                DegMinSec.Latitude.Sec = LatLong_InDegMinSecPrefix.Latitude.Sec;
                DegMinSec.Latitude.Prefix = LatLong_InDegMinSecPrefix.Latitude.Prefix;
                DegMinSec.Longitude.Deg = LatLong_InDegMinSecPrefix.Longitude.Deg;
                DegMinSec.Longitude.Min = LatLong_InDegMinSecPrefix.Longitude.Min;
                DegMinSec.Longitude.Sec = LatLong_InDegMinSecPrefix.Longitude.Sec;
                DegMinSec.Longitude.Prefix = LatLong_InDegMinSecPrefix.Longitude.Prefix;
                Decimal = ConvertDegMinSecToDecimal(DegMinSec);
            }

            ////////////////////////////////////////////////////////////////////////////
            // Constructor which allows initialization
            // using Decimal format
            public LatLongClass(double LatitudeDeg, double LongitudeDeg)
            {
                Decimal.LatitudeDecimal = LatitudeDeg;
                Decimal.LongitudeDecimal = LongitudeDeg;
                DegMinSec = ConvertDecimalToDegMinSec(Decimal);
            }

            public LatLongClass(LatLongDecimal LatLongDec)
            {
                Decimal.LatitudeDecimal = LatLongDec.LatitudeDecimal;
                Decimal.LongitudeDecimal = LatLongDec.LongitudeDecimal;
                DegMinSec = ConvertDecimalToDegMinSec(Decimal);
            }

            public void SetPosition(LatLongDegMinSec LatLong_InDegMinSecPrefix)
            {
                DegMinSec.Latitude.Deg = LatLong_InDegMinSecPrefix.Latitude.Deg;
                DegMinSec.Latitude.Min = LatLong_InDegMinSecPrefix.Latitude.Min;
                DegMinSec.Latitude.Sec = LatLong_InDegMinSecPrefix.Latitude.Sec;
                DegMinSec.Latitude.Prefix = LatLong_InDegMinSecPrefix.Latitude.Prefix;
                DegMinSec.Longitude.Deg = LatLong_InDegMinSecPrefix.Longitude.Deg;
                DegMinSec.Longitude.Min = LatLong_InDegMinSecPrefix.Longitude.Min;
                DegMinSec.Longitude.Sec = LatLong_InDegMinSecPrefix.Longitude.Sec;
                DegMinSec.Longitude.Prefix = LatLong_InDegMinSecPrefix.Longitude.Prefix;
                Decimal = ConvertDegMinSecToDecimal(DegMinSec);
            }

            public void SetPosition(LatLongDecimal LatLongDec)
            {
                Decimal.LatitudeDecimal = LatLongDec.LatitudeDecimal;
                Decimal.LongitudeDecimal = LatLongDec.LongitudeDecimal;
                DegMinSec = ConvertDecimalToDegMinSec(Decimal);
            }

            /////////////////////////////////////////////////////////////////////////////////////
            // This method returns data in already formated for display in DEG.MIN.SEC <N,E,W,S>
            //
            public void GetDegMinSecStringFormat(out String Latitude, out string Longitude)
            {
                Latitude = DegMinSec.Latitude.Deg.ToString() + "°" + DegMinSec.Latitude.Min.ToString() + "′" + DegMinSec.Latitude.Sec.ToString("F5") + "″" + DegMinSec.Latitude.Prefix.ToString();
                Longitude = DegMinSec.Longitude.Deg.ToString() + "°" + DegMinSec.Longitude.Min.ToString() + "′" + DegMinSec.Longitude.Sec.ToString("F5") + "″" + DegMinSec.Longitude.Prefix.ToString();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        // Converts Decimal to Deg Min Sec format
        //
        public static LatLongDegMinSec ConvertDecimalToDegMinSec(LatLongDecimal InData)
        {
            LatLongDegMinSec OutData = new LatLongDegMinSec();

            /////////////////////////////
            // Define localac temp data
            int Num1;
            int Num2;
            double Num3;
            double Temp;

            ///////////////////////////////////////////////////////////////////////////////////////
            // First convert latitude
            double TempLatitude;
            if (InData.LatitudeDecimal < 0.0)
            {
                OutData.Latitude.Prefix = LatLongPrefix.S;
                TempLatitude = InData.LatitudeDecimal * -1.0;
            }
            else
            {
                OutData.Latitude.Prefix = LatLongPrefix.N;
                TempLatitude = InData.LatitudeDecimal;
            }

            // DEG
            Num1 = (int)Math.Floor(TempLatitude);
            // MIN
            Temp = TempLatitude - Math.Floor(TempLatitude);
            Temp = Temp * 60.0;
            Num2 = (int)Math.Floor(Temp);
            // SEC
            Temp = Temp - (int)Math.Floor(Temp);
            Temp = Temp * 60.0;
            Num3 = Temp;

            OutData.Latitude.Deg = Num1;
            OutData.Latitude.Min = Num2;
            OutData.Latitude.Sec = Num3;
           
            ///////////////////////////////////////////////////////////////////////////////////////
            // Then convert longitude
            double TempLongitudeDec;
            if (InData.LongitudeDecimal < 0.0)
            {
                OutData.Longitude.Prefix = LatLongPrefix.W;
                TempLongitudeDec = InData.LongitudeDecimal * -1.0;
            }
            else
            {
                OutData.Longitude.Prefix = LatLongPrefix.E;
                TempLongitudeDec = InData.LongitudeDecimal;
            }
            // DEG
            Num1 = (int)Math.Floor(TempLongitudeDec);
            // MIN
            Temp = TempLongitudeDec - Math.Floor(TempLongitudeDec);
            Temp = Temp * 60.0;
            Num2 = (int)Math.Floor(Temp);
            // SEC
            Temp = Temp - (int)Math.Floor(Temp);
            Temp = Temp * 60.0;
            Num3 = (int)Math.Floor(Temp);

            OutData.Longitude.Deg = Num1;
            OutData.Longitude.Min = Num2;
            OutData.Longitude.Sec = Num3;

            return OutData;
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        // Converts Deg Min Sec to Decimal
        //
        private static LatLongDecimal ConvertDegMinSecToDecimal(LatLongDegMinSec InData)
        {
            LatLongDecimal OutData = new LatLongDecimal();
            const double OneOverSixty = 1.0 / 60.0;
            OutData.LatitudeDecimal = InData.Latitude.Deg + (InData.Latitude.Min * OneOverSixty) + (InData.Latitude.Sec * OneOverSixty * OneOverSixty);
            OutData.LongitudeDecimal = InData.Longitude.Deg + (InData.Longitude.Min * OneOverSixty) + (InData.Longitude.Sec * OneOverSixty * OneOverSixty);

            if (InData.Latitude.Prefix == LatLongPrefix.W)
                OutData.LatitudeDecimal = OutData.LatitudeDecimal * -1.0;

            if (InData.Longitude.Prefix == LatLongPrefix.S)
                OutData.LongitudeDecimal = OutData.LongitudeDecimal * -1.0;
            
            return OutData;
        }

        //////////////////////////////////////////////////////////////////////////////////////
        // This method takes in present position (LatLongClass) and range/bearing from the
        // present position. It returns a new position (LatLongClass) 
        //
        // Distance is IN
        // Azimuth in degrees 
        public static LatLongClass CalculateNewPosition(LatLongClass PresentPosition, double Distance, double Azimuth)
        {
            LatLongClass NewPosition = new LatLongClass();

            // instantiate the calculator
            GeodeticCalculator geoCalc = new GeodeticCalculator();

            // select a reference elllipsoid
            Ellipsoid reference = Ellipsoid.WGS84;

            // set Lincoln Memorial coordinates
            GlobalCoordinates Present_Pos;
            Present_Pos = new GlobalCoordinates(new Angle(PresentPosition.GetLatLongDecimal().LatitudeDecimal), new Angle(PresentPosition.GetLatLongDecimal().LongitudeDecimal));

            // now, plug the result into to direct solution
            GlobalCoordinates dest;
            Angle endBearing = new Angle();

            double Distance_In_Meeters = (Distance * NMtoKM * 1000.0);

            dest = geoCalc.CalculateEndingGlobalCoordinates(reference, Present_Pos, Azimuth, Distance_In_Meeters, out endBearing);
            NewPosition.SetPosition(new LatLongDecimal(dest.Latitude.Degrees, dest.Longitude.Degrees));

            return NewPosition;
        }

    }


}
