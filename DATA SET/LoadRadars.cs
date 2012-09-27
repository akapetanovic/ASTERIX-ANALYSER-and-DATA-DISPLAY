using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AsterixDisplayAnalyser
{
    class LoadRadars
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
                        SystemAdaptationDataSet.RadarDataSet.Add(new SystemAdaptationDataSet.Radar(ItemName, new GeoCordSystemDegMinSecUtilities.LatLongClass(LatDeg, LatMin, LatSec,
                            LatPrefix, LonDeg, LonMin, LonSec, LonPrefix), SIC, SAC));
                    }
                }
            }
            else
            {
                // As a default two radars (Jahorina and Sarajevo TWR are loaded, however it will be modified during the
                // system power on via configuration files if they exist.
                // Add SJJ TWR
                SystemAdaptationDataSet.RadarDataSet.Add(new SystemAdaptationDataSet.Radar("SJJ TWR", new GeoCordSystemDegMinSecUtilities.LatLongClass(43, 49, 11.76, GeoCordSystemDegMinSecUtilities.LatLongPrefix.E, 18, 20, 23.4, GeoCordSystemDegMinSecUtilities.LatLongPrefix.N), "8", "34"));
                // Add Jahorina
                SystemAdaptationDataSet.RadarDataSet.Add(new SystemAdaptationDataSet.Radar("Jahorina", new GeoCordSystemDegMinSecUtilities.LatLongClass(43, 43, 33.69, GeoCordSystemDegMinSecUtilities.LatLongPrefix.E, 18, 33, 5.69, GeoCordSystemDegMinSecUtilities.LatLongPrefix.N), "23", "2"));
            }
        }
    }
}
