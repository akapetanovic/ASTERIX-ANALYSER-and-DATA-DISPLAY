using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AsterixDisplayAnalyser
{
    class LoadCoreSettings
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
            int LatSec;
            GeoCordSystemDegMinSecUtilities.LatLongPrefix LatPrefix;
            int LonDeg;
            int LonMin;
            int LonSec;
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
                                ItemName = words[0];

                                // Get Latitude
                                if (int.TryParse(words[1], out LatDeg) == false)
                                    throw Bad_Main_Settings;
                                if (int.TryParse(words[2], out LatMin) == false)
                                    throw Bad_Main_Settings;
                                if (int.TryParse(words[3], out LatSec) == false)
                                    throw Bad_Main_Settings;

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
                                        throw Bad_Main_Settings;
                                }

                                // Get Longitude
                                if (int.TryParse(words[5], out LonDeg) == false)
                                    throw Bad_Main_Settings;
                                if (int.TryParse(words[6], out LonMin) == false)
                                    throw Bad_Main_Settings;
                                if (int.TryParse(words[7], out LonSec) == false)
                                    throw Bad_Main_Settings;

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
                                        throw Bad_Main_Settings;
                                }

                                GeoCordSystemDegMinSecUtilities.LatLongClass T = new GeoCordSystemDegMinSecUtilities.LatLongClass(LatDeg, LatMin, LatSec,
                            LatPrefix, LonDeg, LonMin, LonSec, LonPrefix);
                                SystemAdaptationDataSet.SystemOrigin = new GMap.NET.PointLatLng(T.GetLatLongDecimal().LatitudeDecimal, T.GetLatLongDecimal().LongitudeDecimal);

                                break;

                            case "BACKGROUND":

                                DisplayAttributes.DisplayAttributesType DisplayAttributeBackground = DisplayAttributes.GetDisplayAttribute(DisplayAttributes.DisplayItemsType.BackgroundColor);
                                DisplayAttributeBackground.TextColor = System.Drawing.Color.FromName(words[1]);
                                DisplayAttributes.SetDisplayAttribute(DisplayAttributes.DisplayItemsType.BackgroundColor, DisplayAttributeBackground);
                                break;
                        }
                    }
                }
            }
            else
            {
                // Here is it initialized to the center of Bosnia and Herzegovina. 
                SystemAdaptationDataSet.SystemOrigin = new GMap.NET.PointLatLng(44.05267, 17.6769);
            }

        }
    }
}
