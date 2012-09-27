using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AsterixDisplayAnalyser
{
    class LoadSectorBoundaries
    {
        public static void Load()
        {

            string ConfigurationData;
            string FileName;
            char[] delimiterChars = { ',', '\t' };
            StreamReader MyStreamReader;

            string CurrentItem;
            string SectorName = "NONE";

            int LatDeg;
            int LatMin;
            double LatSec;
            GeoCordSystemDegMinSecUtilities.LatLongPrefix LatPrefix;
            int LonDeg;
            int LonMin;
            double LonSec;
            GeoCordSystemDegMinSecUtilities.LatLongPrefix LonPrefix;

            System.Collections.Generic.List<GeoCordSystemDegMinSecUtilities.LatLongClass> Sector_Points = new List<GeoCordSystemDegMinSecUtilities.LatLongClass>();

            FileName = @"C:\ASTERIX\ADAPTATION\Sectors.txt";
            Exception Bad_Sectors = new Exception("Bad Sectors.txt file");

            if (System.IO.File.Exists(FileName))
            {
                MyStreamReader = System.IO.File.OpenText(FileName);
                while (MyStreamReader.Peek() >= 0)
                {
                    ConfigurationData = MyStreamReader.ReadLine();
                    string[] words = ConfigurationData.Split(delimiterChars);
                    if (words[0][0] != '#')
                    {
                        // Get Item
                        CurrentItem = words[0];

                        // If the is a name, then it is a new sector
                        if (words[0] == "SECTOR_NAME")
                        {
                            // If we have reached a new name it means that we have parsed 
                            // the data for previous sector
                            if (SectorName != "NONE")
                            {
                                // Now add the new sector to the data set
                                SystemAdaptationDataSet.SectorBorderDataSet.Add(new SystemAdaptationDataSet.SectorBorder(SectorName, Sector_Points));

                                // Save off the new name
                                SectorName = words[1];

                                // Empty the list so it is ready for the next sector
                                Sector_Points = new List<GeoCordSystemDegMinSecUtilities.LatLongClass>();
                            }
                            // This is first sector so just save off the name
                            else
                            {
                                SectorName = words[1];
                            }
                        }
                        // This a new point, so extract it an save it into a local list
                        else
                        {

                            // Get Latitude
                            if (int.TryParse(words[0], out LatDeg) == false)
                                throw Bad_Sectors;
                            if (int.TryParse(words[1], out LatMin) == false)
                                throw Bad_Sectors;
                            if (Double.TryParse(words[2], out LatSec) == false)
                                throw Bad_Sectors;
                            switch (words[3])
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
                                    throw Bad_Sectors;
                            }

                            // Get Longitude
                            if (int.TryParse(words[4], out LonDeg) == false)
                                throw Bad_Sectors;
                            if (int.TryParse(words[5], out LonMin) == false)
                                throw Bad_Sectors;
                            if (Double.TryParse(words[6], out LonSec) == false)
                                throw Bad_Sectors;

                            switch (words[7])
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
                                    throw Bad_Sectors;
                            }

                            Sector_Points.Add((new GeoCordSystemDegMinSecUtilities.LatLongClass(LatDeg, LatMin, LatSec,
                            LatPrefix, LonDeg, LonMin, LonSec, LonPrefix)));
                        }
                    }
                }

                // Now add the last processed sector
                SystemAdaptationDataSet.SectorBorderDataSet.Add(new SystemAdaptationDataSet.SectorBorder(SectorName, Sector_Points));
            }
        }
    }
}
