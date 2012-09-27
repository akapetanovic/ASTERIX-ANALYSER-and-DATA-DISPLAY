using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AsterixDisplayAnalyser
{
    class LoadStateBoundaries
    {
        public static void Load()
        {
            string ConfigurationData;
            string FileName;
            char[] delimiterChars = { ',', '\t' };
            StreamReader MyStreamReader;

            string CurrentItem;
            string StateName = "NONE";
            double Lat;
            double Lon;
           
           
            System.Collections.Generic.List<GeoCordSystemDegMinSecUtilities.LatLongClass> Sector_Points = new List<GeoCordSystemDegMinSecUtilities.LatLongClass>();

            FileName = @"C:\ASTERIX\ADAPTATION\States.txt";
            Exception Bad_States = new Exception("Bad States.txt file");

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
                        if (words[0] == "STATE_NAME")
                        {
                            // If we have reached a new name it means that we have parsed 
                            // the data for previous sector
                            if (StateName != "NONE")
                            {
                                // Now add the new sector to the data set
                                SystemAdaptationDataSet.StateBorderDataSet.Add(new SystemAdaptationDataSet.StateBorder(StateName, Sector_Points));

                                // Save off the new name
                                StateName = words[1];

                                // Empty the list so it is ready for the next sector
                                Sector_Points = new List<GeoCordSystemDegMinSecUtilities.LatLongClass>();
                            }
                            // This is first sector so just save off the name
                            else
                            {
                                StateName = words[1];
                            }
                        }
                        // This a new point, so extract it an save it into a local list
                        else
                        {

                            // Get Longitude
                            if (double.TryParse(words[0], out Lon) == false)
                                throw Bad_States;
                            // Get Latitude
                            if (double.TryParse(words[1], out Lat) == false)
                                throw Bad_States;

                            Sector_Points.Add((new GeoCordSystemDegMinSecUtilities.LatLongClass(Lat, Lon)));
                        }
                    }
                }

                // Now add the last processed sector
                SystemAdaptationDataSet.StateBorderDataSet.Add(new SystemAdaptationDataSet.StateBorder(StateName, Sector_Points));
            }
        }
    }
}
