using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AsterixDisplayAnalyser
{
    class LoadWaypoints
    {
        public static void Load()
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
                            throw Bad_Waypoints;
                        if (int.TryParse(words[2], out LatMin) == false)
                            throw Bad_Waypoints;
                        if (Double.TryParse(words[3], out LatSec) == false)
                            throw Bad_Waypoints;
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
                                throw Bad_Waypoints;
                        }

                        // Get Longitude
                        if (int.TryParse(words[5], out LonDeg) == false)
                            throw Bad_Waypoints;
                        if (int.TryParse(words[6], out LonMin) == false)
                            throw Bad_Waypoints;
                        if (Double.TryParse(words[7], out LonSec) == false)
                            throw Bad_Waypoints;

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
                                throw Bad_Waypoints;
                        }

                        if (words[9] == "TRUE")
                            Is_COP = true;
                        else
                            Is_COP = false;

                        // Now add the radar
                        SystemAdaptationDataSet.WaypointDataSet.Add(new SystemAdaptationDataSet.Waypoint(ItemName, new GeoCordSystemDegMinSecUtilities.LatLongClass(LatDeg, LatMin, LatSec,
                            LatPrefix, LonDeg, LonMin, LonSec, LonPrefix), Is_COP));
                    }
                }
            }

        }

    }

}
