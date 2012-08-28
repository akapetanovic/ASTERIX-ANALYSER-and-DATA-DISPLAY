using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MulticastingUDP
{
    class LoadCoreSettings
    {
        public static void Load()
        {
            string ConfigurationData;
            string FileName;
            char[] delimiterChars = { ',', '\t' };
            StreamReader MyStreamReader;

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
                                SystemAdaptationDataSet.SystemOrigin = new GMap.NET.PointLatLng(Lat, Lon);
                                break;

                            case "BACKGROUND":

                                DisplayAttributes.DisplayAttributesType DisplayAttributeBackground = DisplayAttributes.GetDisplayAttribute(DisplayAttributes.DisplayItemsType.BackgroundColor);
                                DisplayAttributeBackground.TextColor = System.Drawing.Color.FromName(words[1]);
                                DisplayAttributes.SetDisplayAttribute(DisplayAttributes.DisplayItemsType.BackgroundColor, DisplayAttributeBackground);
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
                SystemAdaptationDataSet.SystemOrigin = new GMap.NET.PointLatLng(44.05267, 17.6769);
            }

        }
    }
}
