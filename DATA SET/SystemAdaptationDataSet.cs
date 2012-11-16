using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using System.IO;

namespace AsterixDisplayAnalyser
{
    /// <summary>
    /// /////////////////////////////////////////////////////////////////////
    /// This class encapsulates all the system wide configuration parameters.
    /// Its main task is to check for the configuration files during power up
    /// and configure the system accordingly. In the case configuration files 
    /// do not exist the system is initialised to the default configuration
    /// Bosnia and Herzegovina.
    /// </summary>
    class SystemAdaptationDataSet
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Here define the system origin point. It is the point to be used as the center of the data display.
        public static PointLatLng SystemOrigin;

        public static GMap.NET.PointLatLng SystemOriginPoint
        {
            get { return SystemOrigin; }
            set { SystemOrigin = value; }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Define radar data type
        public class Radar
        {
            public string RadarName;
            public GeoCordSystemDegMinSecUtilities.LatLongClass RadarPosition = new GeoCordSystemDegMinSecUtilities.LatLongClass();
            public string SIC;
            public string SAC;

            public Radar(string R_Name, GeoCordSystemDegMinSecUtilities.LatLongClass R_Position, string R_SIC, string R_SAC)
            {
                RadarName = R_Name;
                RadarPosition = new GeoCordSystemDegMinSecUtilities.LatLongClass(R_Position.GetLatLongDecimal().LatitudeDecimal, R_Position.GetLatLongDecimal().LongitudeDecimal);
                SIC = R_SIC;
                SAC = R_SAC;
            }
        }
        public static System.Collections.Generic.List<Radar> RadarDataSet = new System.Collections.Generic.List<Radar>();
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Define waypoint data type
        public class Waypoint
        {
            public string WaypointName;
            public GeoCordSystemDegMinSecUtilities.LatLongClass WaypointPosition = new GeoCordSystemDegMinSecUtilities.LatLongClass();
            bool Is_COP;

            public Waypoint(string W_Name, GeoCordSystemDegMinSecUtilities.LatLongClass W_Position, bool Is_COP_In)
            {
                WaypointName = W_Name;
                WaypointPosition = new GeoCordSystemDegMinSecUtilities.LatLongClass(W_Position.GetLatLongDecimal().LatitudeDecimal, W_Position.GetLatLongDecimal().LongitudeDecimal);
                Is_COP = Is_COP_In;
            }
        }
        public static System.Collections.Generic.List<Waypoint> WaypointDataSet = new System.Collections.Generic.List<Waypoint>();
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Define state data type
        public class StateBorder
        {
            public string StateName;
            public System.Collections.Generic.List<GeoCordSystemDegMinSecUtilities.LatLongClass> StateBorderPoints = new System.Collections.Generic.List<GeoCordSystemDegMinSecUtilities.LatLongClass>();

            public StateBorder(string S_Name, System.Collections.Generic.List<GeoCordSystemDegMinSecUtilities.LatLongClass> S_Points)
            {
                StateName = S_Name;
                StateBorderPoints = S_Points;
            }
        }
        public static System.Collections.Generic.List<StateBorder> StateBorderDataSet = new System.Collections.Generic.List<StateBorder>();
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Define sector data type
        public class SectorBorder
        {
            public string SectorName;
            public System.Collections.Generic.List<GeoCordSystemDegMinSecUtilities.LatLongClass> SectorBorderPoints = new System.Collections.Generic.List<GeoCordSystemDegMinSecUtilities.LatLongClass>();

            public SectorBorder(string S_Name, System.Collections.Generic.List<GeoCordSystemDegMinSecUtilities.LatLongClass> S_Points)
            {
                SectorName = S_Name;
                SectorBorderPoints = S_Points;
            }
        }
        public static System.Collections.Generic.List<SectorBorder> SectorBorderDataSet = new System.Collections.Generic.List<SectorBorder>();
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Define runway data type
        public class RunwayBorder
        {
            public string RunwayName;
            public System.Collections.Generic.List<GeoCordSystemDegMinSecUtilities.LatLongClass> RunwayBorderPoints = new System.Collections.Generic.List<GeoCordSystemDegMinSecUtilities.LatLongClass>();

            public RunwayBorder(string S_Name, System.Collections.Generic.List<GeoCordSystemDegMinSecUtilities.LatLongClass> S_Points)
            {
                RunwayName = S_Name;
                RunwayBorderPoints = S_Points;
            }
        }
        public static System.Collections.Generic.List<RunwayBorder> RunwayBorderDataSet = new System.Collections.Generic.List<RunwayBorder>();
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public static void InitializeData()
        {
            DisplayAttributes.Load();
            LoadAdaptationData.Load();
        }
    }
}
