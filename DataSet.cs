using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using System.IO;

namespace MulticastingUDP
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
        private static PointLatLng SystemOrigin;

        public static GMap.NET.PointLatLng SystemOriginPoint
        {
            get { return SystemOrigin; }
            set { SystemOrigin = value; }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Define radar data structures
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Define waypoint data structures
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void InitializeData()
        {
            ReadAdaptationData();
        }

        // This method reads in system adaptation data set from configuration files at system power up and adjust the system in accordance to the system defined settings.
        // It expects configuration files in a specified directory. In the case they are not found the system will default to the default development data set.
        private static void ReadAdaptationData()
        {
            string ConfigurationData;
            string FileName;
            char[] delimiterChars = { ',', '\t' };
            StreamReader MyStreamReader;

            string ItemName;
            int LatDeg;
            int LatMin;
            double LatSec;
            GeoCordSystemDegMinSecUtilities.LatLongPrefix LatPrefix;
            int LonDeg;
            int LonMin;
            double LonSec;
            GeoCordSystemDegMinSecUtilities.LatLongPrefix LonPrefix;

            /////////////////////////////////////////////////////////////////////////
            // First set the system origin
            /////////////////////////////////////////////////////////////////////////
            FileName = @"C:\ASTERIX\ADAPTATION\Main_Settings.txt";
            Exception Bad_Main_Settings = new Exception("Bad Main_Settings.txt file");
            if (System.IO.File.Exists(FileName))
            {
                MyStreamReader = System.IO.File.OpenText(FileName);
                while (MyStreamReader.Peek() >= 0)
                {
                    ConfigurationData = MyStreamReader.ReadLine();
                    string[] words = ConfigurationData.Split(delimiterChars);
                    if (words[0][0] != '#')
                    {
                        switch (words[0])
                        {
                            case "SYS_ORIGIN":

                                double Lat = double.Parse(words[1]);
                                double Lon = double.Parse(words[2]);

                                if (Double.TryParse(words[1], out Lat) == false)
                                    throw Bad_Main_Settings;
                                if (Double.TryParse(words[2], out Lon) == false)
                                    throw Bad_Main_Settings;
                                SystemOrigin = new GMap.NET.PointLatLng(Lat, Lon);
                                break;


                            default:
                                throw Bad_Main_Settings;
                        }
                    }
                }
            }
            else
            {
                // Here is it initialized to the center of Bosnia and Herzegovina. 
                SystemOrigin = new GMap.NET.PointLatLng(44.05267, 17.6769);
            }

            /////////////////////////////////////////////////////////////////////////
            // Now define radars
            /////////////////////////////////////////////////////////////////////////
            FileName = @"C:\ASTERIX\ADAPTATION\Radars.txt";
            Exception Bad_Radars = new Exception("Bad Radars.txt file");
            string SIC, SAC;

            if (System.IO.File.Exists(FileName))
            {
                MyStreamReader = System.IO.File.OpenText(FileName);
                while (MyStreamReader.Peek() >= 0)
                {
                    ConfigurationData = MyStreamReader.ReadLine();
                    string[] words = ConfigurationData.Split(delimiterChars);
                    if (words[0][0] != '#')
                    {
                        // Sarajevo_TWR,	43, 49, 11.76, E, 18, 20, 23.4, N, 8, 34
                        ItemName = words[0];
                        // Get Radar Name

                        // Get Latitude
                        if (int.TryParse(words[1], out LatDeg) == false)
                            throw Bad_Radars;
                        if (int.TryParse(words[2], out LatMin) == false)
                            throw Bad_Radars;
                        if (Double.TryParse(words[3], out LatSec) == false)
                            throw Bad_Radars;
                        switch (words[4])
                        {
                            case "E":
                                LatPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.E;
                                break;
                            case "W":
                                LatPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.W;
                                break;
                            case "N":
                                LatPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.N;
                                break;
                            case "S":
                                LatPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.S;
                                break;
                            default:
                                throw Bad_Radars;
                        }

                        // Get Longitude
                        if (int.TryParse(words[5], out LonDeg) == false)
                            throw Bad_Radars;
                        if (int.TryParse(words[6], out LonMin) == false)
                            throw Bad_Radars;
                        if (Double.TryParse(words[7], out LonSec) == false)
                            throw Bad_Radars;

                        switch (words[8])
                        {
                            case "E":
                                LonPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.E;
                                break;
                            case "W":
                                LonPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.W;
                                break;
                            case "N":
                                LonPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.N;
                                break;
                            case "S":
                                LonPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.S;
                                break;
                            default:
                                throw Bad_Radars;
                        }

                        // Get SIC/SAC
                        SIC = words[9];
                        SAC = words[10];

                        // Now add the radar
                        RadarDataSet.Add(new Radar(ItemName, new GeoCordSystemDegMinSecUtilities.LatLongClass(LatDeg, LatMin, LatSec,
                            LatPrefix, LonDeg, LonMin, LonSec, LonPrefix), SIC, SAC));
                    }
                }
            }
            else
            {
                // As a default two radars (Jahorina and Sarajevo TWR are loaded, however it will be modified during the
                // system power on via configuration files if they exist.
                // Add SJJ TWR
                RadarDataSet.Add(new Radar("SJJ TWR", new GeoCordSystemDegMinSecUtilities.LatLongClass(43, 49, 11.76, GeoCordSystemDegMinSecUtilities.LatLongPrefix.E, 18, 20, 23.4, GeoCordSystemDegMinSecUtilities.LatLongPrefix.N), "8", "34"));
                // Add Jahorina
                RadarDataSet.Add(new Radar("Jahorina", new GeoCordSystemDegMinSecUtilities.LatLongClass(43, 43, 33.69, GeoCordSystemDegMinSecUtilities.LatLongPrefix.E, 18, 33, 5.69, GeoCordSystemDegMinSecUtilities.LatLongPrefix.N), "23", "2"));
            }

            /////////////////////////////////////////////////////////////////////////
            // Now load waypoints
            /////////////////////////////////////////////////////////////////////////
            FileName = @"C:\ASTERIX\ADAPTATION\Waypoints.txt";
            Exception Bad_Waypoints = new Exception("Bad Waypoints.txt file");
            bool Is_COP;

            if (System.IO.File.Exists(FileName))
            {
                MyStreamReader = System.IO.File.OpenText(FileName);
                while (MyStreamReader.Peek() >= 0)
                {
                    ConfigurationData = MyStreamReader.ReadLine();
                    string[] words = ConfigurationData.Split(delimiterChars);
                    if (words[0][0] != '#')
                    {
                       
                        ItemName = words[0];
                        // Get Radar Name

                        // Get Latitude
                        if (int.TryParse(words[1], out LatDeg) == false)
                            throw Bad_Radars;
                        if (int.TryParse(words[2], out LatMin) == false)
                            throw Bad_Radars;
                        if (Double.TryParse(words[3], out LatSec) == false)
                            throw Bad_Radars;
                        switch (words[4])
                        {
                            case "E":
                                LatPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.E;
                                break;
                            case "W":
                                LatPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.W;
                                break;
                            case "N":
                                LatPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.N;
                                break;
                            case "S":
                                LatPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.S;
                                break;
                            default:
                                throw Bad_Radars;
                        }

                        // Get Longitude
                        if (int.TryParse(words[5], out LonDeg) == false)
                            throw Bad_Radars;
                        if (int.TryParse(words[6], out LonMin) == false)
                            throw Bad_Radars;
                        if (Double.TryParse(words[7], out LonSec) == false)
                            throw Bad_Radars;

                        switch (words[8])
                        {
                            case "E":
                                LonPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.E;
                                break;
                            case "W":
                                LonPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.W;
                                break;
                            case "N":
                                LonPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.N;
                                break;
                            case "S":
                                LonPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.S;
                                break;
                            default:
                                throw Bad_Radars;
                        }

                        if (words[9] == "TRUE")
                            Is_COP = true;
                        else
                            Is_COP = false;

                        // Now add the radar
                        WaypointDataSet.Add(new Waypoint(ItemName, new GeoCordSystemDegMinSecUtilities.LatLongClass(LatDeg, LatMin, LatSec,
                            LatPrefix, LonDeg, LonMin, LonSec, LonPrefix), Is_COP));
                    }
                }
            }

            /////////////////////////////////////////////////////////////////////////
            // Now handle display preferences
            /////////////////////////////////////////////////////////////////////////
        }
    }
}
