using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GMap.NET;

namespace AsterixDisplayAnalyser
{
    // This class handles Separation Tool logic
    // It takes two tracks and its speed vector
    // and then using extrapolation calculates the minimum 
    // horizontal separation distance, if the tracks are converging.
    // Based on this it is then possible to visually draw thepoint and 
    // predicted time of violation

    class SEP_Tool_Calculator
    {
        public class OutData
        {
            public bool Is_Converging = false;
            public Point Track_1_Pos_Min = new Point();
            public Point Track_2_Pos_Min = new Point();
            public double MinDistance = 0;
            public int SecondsToMinimum = 10;
        }

        private GlobalPosition Track_1_Pos;
        private GlobalPosition Track_2_Pos;
        private double Track_1_SPD = 0;
        private double Track_2_SPD = 0;
        private double Track_1_TRK = 0;
        private double Track_2_TRK = 0;
        private int MaxLookAheadTimeInMinutes = 1;
        private OutData ReturnData = new OutData();

        public SEP_Tool_Calculator(GlobalPosition Track_1_Position, GlobalPosition Track_2_Position,
                        double Track_1_Speed, double Track_2_Speed,
                        double Track_1_Track, double Track_2_Track,
                        int LookAheadTimeInMinutes)
        {
            Track_1_Pos = new GlobalPosition(new GlobalCoordinates(Track_1_Position.Latitude, Track_1_Position.Longitude));
            Track_2_Pos = new GlobalPosition(new GlobalCoordinates(Track_2_Position.Latitude, Track_2_Position.Longitude));
            Track_1_SPD = Track_1_Speed;
            Track_2_SPD = Track_2_Speed;
            Track_1_TRK = Track_1_Track;
            Track_2_TRK = Track_2_Track;
            MaxLookAheadTimeInMinutes = LookAheadTimeInMinutes;
        }

        public OutData GetResult()
        {
            // select a reference elllipsoid
            Ellipsoid reference = Ellipsoid.WGS84;
            // instantiate the calculator
            GeodeticCalculator geoCalc = new GeodeticCalculator();
            // Used to calculate the time to the min distance 
            GlobalPosition Orig_Track_1_Pos = new GlobalPosition(new GlobalCoordinates(Track_1_Pos.Latitude, Track_1_Pos.Longitude));

            int UpdateStep = 60 / Properties.Settings.Default.SEepToolUpdateRate;
            for (int LookAheadIndex = 1; LookAheadIndex <= ((MaxLookAheadTimeInMinutes * 60) / UpdateStep); LookAheadIndex++)
            {
                // Calculate new position X seconds ahead for track 1
                double Range = (Track_1_SPD / 60) / UpdateStep;
                GeoCordSystemDegMinSecUtilities.LatLongClass ResultPosition_1 =
                    GeoCordSystemDegMinSecUtilities.CalculateNewPosition(new GeoCordSystemDegMinSecUtilities.LatLongClass(Track_1_Pos.Latitude.Degrees, Track_1_Pos.Longitude.Degrees), (double)Range, (double)Track_1_TRK);
                GPoint MarkerPositionLocal = FormMain.gMapControl.FromLatLngToLocal(new PointLatLng(ResultPosition_1.GetLatLongDecimal().LatitudeDecimal, ResultPosition_1.GetLatLongDecimal().LongitudeDecimal));
                ReturnData.Track_1_Pos_Min.X = MarkerPositionLocal.X;
                ReturnData.Track_1_Pos_Min.Y = MarkerPositionLocal.Y;

                // Calculate new position X seconds ahead for track 2
                Range = (Track_2_SPD / 60) / 15;
                GeoCordSystemDegMinSecUtilities.LatLongClass ResultPosition_2 =
                   GeoCordSystemDegMinSecUtilities.CalculateNewPosition(new GeoCordSystemDegMinSecUtilities.LatLongClass(Track_2_Pos.Latitude.Degrees, Track_2_Pos.Longitude.Degrees), (double)Range, (double)Track_2_TRK);
                MarkerPositionLocal = FormMain.gMapControl.FromLatLngToLocal(new PointLatLng(ResultPosition_2.GetLatLongDecimal().LatitudeDecimal, ResultPosition_2.GetLatLongDecimal().LongitudeDecimal));
                ReturnData.Track_2_Pos_Min.X = MarkerPositionLocal.X;
                ReturnData.Track_2_Pos_Min.Y = MarkerPositionLocal.Y;

                double distance1 = geoCalc.CalculateGeodeticMeasurement(reference, Track_1_Pos, Track_2_Pos).PointToPointDistance;
                double distance2 = geoCalc.CalculateGeodeticMeasurement(reference, new GlobalPosition(new GlobalCoordinates(ResultPosition_1.GetLatLongDecimal().LatitudeDecimal, ResultPosition_1.GetLatLongDecimal().LongitudeDecimal)),
                    new GlobalPosition(new GlobalCoordinates(ResultPosition_2.GetLatLongDecimal().LatitudeDecimal, ResultPosition_2.GetLatLongDecimal().LongitudeDecimal))).PointToPointDistance;

                // Calculate distance between present and new position
                if ((distance1 < distance2) && (LookAheadIndex != 1))
                {
                    ReturnData.Is_Converging = true;
                    ReturnData.MinDistance = distance2 * 0.00053996; // Convert to nautical miles

                    double DistanceToTravel = geoCalc.CalculateGeodeticMeasurement(reference, Orig_Track_1_Pos, new GlobalPosition(new GlobalCoordinates(ResultPosition_1.GetLatLongDecimal().LatitudeDecimal, ResultPosition_1.GetLatLongDecimal().LongitudeDecimal))).PointToPointDistance;
                    DistanceToTravel = DistanceToTravel * 0.00053996; // Convert to nautical miles
                    ReturnData.SecondsToMinimum = (int)((DistanceToTravel / Track_1_SPD) * 60.0 * 60.0);
                    // We have reached the minimum distance
                    break;
                }
                else if ((distance1 < distance2) && (LookAheadIndex == 1))
                {
                    ReturnData.Is_Converging = false;
                    break;
                }

                Track_1_Pos = new GlobalPosition(new GlobalCoordinates(ResultPosition_1.GetLatLongDecimal().LatitudeDecimal, ResultPosition_1.GetLatLongDecimal().LongitudeDecimal));
                Track_2_Pos = new GlobalPosition(new GlobalCoordinates(ResultPosition_2.GetLatLongDecimal().LatitudeDecimal, ResultPosition_2.GetLatLongDecimal().LongitudeDecimal));
            }

            return ReturnData;
        }
    }
}
