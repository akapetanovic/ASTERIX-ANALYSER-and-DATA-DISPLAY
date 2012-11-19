using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser
{
    class Display_Attributes_IO
    {
        public static void Load()
        {
            string DisplayAdaptationDataLine;
            string FileName;
            char[] delimiterChars = { ',', '\t' };
            StreamReader MyStreamReader;

            FileName = @"C:\ASTERIX\ADAPTATION\DisplayAttributes.txt";

            if (System.IO.File.Exists(FileName))
            {
                MyStreamReader = System.IO.File.OpenText(FileName);
                while (MyStreamReader.Peek() >= 0)
                {
                    DisplayAdaptationDataLine = MyStreamReader.ReadLine();
                    string[] words = DisplayAdaptationDataLine.Split(delimiterChars);
                    if (words[0][0] != '#')
                    {
                        DisplayAttributes.DisplayAttributesType DisplayAttribute = new DisplayAttributes.DisplayAttributesType();

                        DisplayAttribute.ItemName = words[0];
                        DisplayAttribute.TextSize = int.Parse(words[1]);
                        DisplayAttribute.TextFont = new FontFamily(words[2]);
                        DisplayAttribute.TextColor = Color.FromName(words[3]);
                        DisplayAttribute.LineWidth = int.Parse(words[4]);
                        DisplayAttribute.LineColor = Color.FromName(words[5]);
                        DisplayAttribute.LineStyle = DisplayAttributes.GetLineStypefromString(words[6]);
                        DisplayAttribute.AreaPolygonColor = Color.FromName(words[7]);
                        DisplayAttribute.ImageSize = new Size(int.Parse(words[8]), int.Parse((words[9])));
                        DisplayAttributes.SetDisplayAttribute((DisplayAttributes.DisplayItemsType)Enum.Parse(typeof(DisplayAttributes.DisplayItemsType), DisplayAttribute.ItemName, true), DisplayAttribute);
                    }
                }
            }
        }

        public static void Save()
        {
            string BackgroundColor = "Black";
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // HANLDE Display Attributes First
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string FileName;
            FileName = @"C:\ASTERIX\ADAPTATION\DisplayAttributes.txt";
            DisplayAttributes.DisplayAttributesType DisplayAttribute = new DisplayAttributes.DisplayAttributesType();
            string DisplayAttributesStream = "#ITEM_NAME,TEXT_SIZE,TEXT_FONT,TEXT_COLOR,LINE_WIDTH,LINE_COLOR,LINE_STYLE,AREA_COLOR,IMAGE_SIZE" + Environment.NewLine;

            System.Collections.Generic.List<DisplayAttributes.DisplayAttributesType> AllDisplayAtributes = DisplayAttributes.GetAllDisplayAttributes();

            foreach (DisplayAttributes.DisplayAttributesType DataItem in DisplayAttributes.GetAllDisplayAttributes())
            {
                if (DataItem.ItemName != "BackgroundColor")
                {
                    DisplayAttributesStream = DisplayAttributesStream + DataItem.ItemName + ',';
                    DisplayAttributesStream = DisplayAttributesStream + DataItem.TextSize.ToString() + ',';
                    DisplayAttributesStream = DisplayAttributesStream + DataItem.TextFont.Name + ',';
                    DisplayAttributesStream = DisplayAttributesStream + DataItem.TextColor.Name + ',';
                    DisplayAttributesStream = DisplayAttributesStream + DataItem.LineWidth.ToString() + ',';
                    DisplayAttributesStream = DisplayAttributesStream + DataItem.LineColor.Name + ',';
                    DisplayAttributesStream = DisplayAttributesStream + DataItem.LineStyle.ToString() + ',';
                    DisplayAttributesStream = DisplayAttributesStream + DataItem.AreaPolygonColor.Name + ',';
                    DisplayAttributesStream = DisplayAttributesStream + DataItem.ImageSize.Width.ToString() + ',';
                    DisplayAttributesStream = DisplayAttributesStream + DataItem.ImageSize.Height.ToString() + Environment.NewLine;
                }
                else
                {
                    BackgroundColor = DataItem.TextColor.Name;
                }
            }

            // Remove the last character, which is the new line.
            DisplayAttributesStream.Remove(DisplayAttributesStream.Length - 1);

            // create a writer and open the file
            TextWriter tw = new StreamWriter(FileName);

            try
            {
                // write a line of text to the file
                tw.Write(DisplayAttributesStream);
                MessageBox.Show("DisplayAttributes succefully saved");
            }
            catch (System.IO.IOException e)
            {
                MessageBox.Show(e.Message);
            }

            // close the stream
            tw.Close();


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // HANLDE MainSettings First
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            FileName = @"C:\ASTERIX\ADAPTATION\Main_Settings.txt";
            DisplayAttributesStream = "# System center is : Deg (int), Min (int), Sec (int), Prefix (E/W/N/S)" + Environment.NewLine;

            GeoCordSystemDegMinSecUtilities.LatLongClass LatLon = new GeoCordSystemDegMinSecUtilities.LatLongClass(SystemAdaptationDataSet.SystemOriginPoint.Lat, SystemAdaptationDataSet.SystemOriginPoint.Lng);
            int Int_Lat_Sec = (int)LatLon.GetDegMinSec().Latitude.Sec;
            string LatPrefix = "N";
            if (LatLon.GetDegMinSec().Latitude.Prefix == GeoCordSystemDegMinSecUtilities.LatLongPrefix.S)
                LatPrefix = "S";
            int Int_Lon_Sec = (int)LatLon.GetDegMinSec().Longitude.Sec;
            string LonPrefix = "E";
            if (LatLon.GetDegMinSec().Longitude.Prefix == GeoCordSystemDegMinSecUtilities.LatLongPrefix.W)
                LonPrefix = "W";

            // 44,6,0,N,20,0,0,E
            DisplayAttributesStream = DisplayAttributesStream + "SYS_ORIGIN," +
                LatLon.GetDegMinSec().Latitude.Deg.ToString() + ',' + LatLon.GetDegMinSec().Latitude.Min.ToString() + ',' + Int_Lat_Sec.ToString() + ',' + LatPrefix + ',' +
                LatLon.GetDegMinSec().Longitude.Deg.ToString() + ',' + LatLon.GetDegMinSec().Longitude.Min.ToString() + ',' + Int_Lon_Sec.ToString() + ',' + LonPrefix + 
                Environment.NewLine; ;

            DisplayAttributesStream = DisplayAttributesStream + "BACKGROUND," + BackgroundColor;

            // create a writer and open the file
            tw = new StreamWriter(FileName);

            try
            {
                // write a line of text to the file
                tw.Write(DisplayAttributesStream);
                MessageBox.Show("Main_Settings succefully saved");
            }
            catch (System.IO.IOException e)
            {
                MessageBox.Show(e.Message);
            }

            // close the stream
            tw.Close();

        }
    }
}
