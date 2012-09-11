using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace MulticastingUDP
{
    public static class LabelAttributes
    {

        // Label text attributes
        public static Color TextColor = Color.White;
        public static FontFamily TextFont = FontFamily.GenericSansSerif;
        public static int TextSize = 8;

        // Label Box and leader line attributes
        public static Color LineColor = Color.White;
        public static DashStyle LineStyle = DashStyle.Solid;
        public static int LineWidth = 2;

        // Label background color
        public static Color BackgroundColor = Color.Transparent;
        
        // Target Symbol Attributes
        public static Color TargetColor = Color.White;
        public static DashStyle TargetStyle = DashStyle.Solid;

        public static void Load()
        {

            string DisplayAdaptationDataLine;
            string FileName;
            char[] delimiterChars = { ',', '\t' };
            StreamReader MyStreamReader;

            FileName = @"C:\ASTERIX\ADAPTATION\LabelAttributes.txt";

            if (System.IO.File.Exists(FileName))
            {
                MyStreamReader = System.IO.File.OpenText(FileName);
                while (MyStreamReader.Peek() >= 0)
                {
                    DisplayAdaptationDataLine = MyStreamReader.ReadLine();
                    string[] words = DisplayAdaptationDataLine.Split(delimiterChars);
                    if (words[0][0] != '#')
                    {
                        switch (words[0])
                        {
                            case "TEXT_COLOR":
                                TextColor = Color.FromName(words[1]);
                                break;
                            case "TEXT_FONT":
                                TextFont = new FontFamily(words[1]);
                                break;
                            case "TEXT_SIZE":
                                TextSize = int.Parse(words[1]);
                                break;
                            case "LINE_COLOR":
                                LineColor = Color.FromName(words[1]);
                                break;
                            case "LINE_STYLE":
                                LineStyle = DisplayAttributes.GetLineStypefromString(words[1]);
                                break;
                            case "LINE_WIDTH":
                                LineWidth = int.Parse(words[1]);
                                break;
                            case "TARGET_COLOR":
                                TargetColor = Color.FromName(words[1]);
                                break;
                            case "TARGET_STYLE":
                                TargetStyle = DisplayAttributes.GetLineStypefromString(words[1]);
                                break;
                            default:
                                MessageBox.Show("Bad format of LabelAttributes.txt");
                                break;
                        }
                    }
                }
            }
        }

        public static void Save()
        {

        }
    }
}
