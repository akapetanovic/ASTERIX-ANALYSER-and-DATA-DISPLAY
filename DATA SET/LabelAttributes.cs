using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser
{
    public static class LabelAttributes
    {

        // Label text attributes
        public static Color TextColor = Color.DarkGreen;
        public static FontFamily TextFont = FontFamily.GenericSansSerif;
        public static int TextSize = 8;

        // Label Box and leader line attributes
        public static Color LineColor = Color.White;
        public static DashStyle LineStyle = DashStyle.Solid;
        public static int LineWidth = 2;

        // Label background color
        public static Color BackgroundColor = Color.Transparent;

        // Target Symbol Attributes
        public static Color TargetColor = Color.DarkGreen;
        public static DashStyle TargetStyle = DashStyle.Solid;
        public static int TargetSize = 2;

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
                            case "TARGET_SIZE":
                                TargetSize = int.Parse(words[1]);
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

            string FileName;
            FileName = @"C:\ASTERIX\ADAPTATION\LabelAttributes.txt";
            DisplayAttributes.DisplayAttributesType DisplayAttribute = new DisplayAttributes.DisplayAttributesType();
            string DisplayAttributesStream =
                "#TEXT_COLOR,value" + Environment.NewLine +
                "#TEXT_FONT,value" + Environment.NewLine +
                "#TEXT_SIZE,value" + Environment.NewLine +
                "#LINE_COLOR,value" + Environment.NewLine +
                "#LINE_STYLE,value" + Environment.NewLine +
                "#LINE_WIDTH,value" + Environment.NewLine +
                "#TARGET_COLOR,value" + Environment.NewLine +
                "#TARGET_STYLE,value" + Environment.NewLine +
                "#TARGET_SIZE,value" + Environment.NewLine;

            DisplayAttributesStream = DisplayAttributesStream +
                "TEXT_COLOR," + LabelAttributes.TextColor.Name + Environment.NewLine +
                "TEXT_FONT," + LabelAttributes.TextFont.Name + Environment.NewLine +
                "TEXT_SIZE," + LabelAttributes.TextSize.ToString() + Environment.NewLine +
                "LINE_COLOR," + LabelAttributes.LineColor.Name + Environment.NewLine +
                "LINE_STYLE," + LabelAttributes.LineStyle.ToString() + Environment.NewLine +
                "LINE_WIDTH," + LabelAttributes.LineWidth.ToString() + Environment.NewLine +
                "TARGET_COLOR," + LabelAttributes.TargetColor.Name + Environment.NewLine +
                "TARGET_STYLE," + LabelAttributes.TargetStyle.ToString() + Environment.NewLine +
                "TARGET_SIZE," + LabelAttributes.TargetSize.ToString();

            // create a writer and open the file
            TextWriter tw = new StreamWriter(FileName);

            try
            {
                // write a line of text to the file
                tw.Write(DisplayAttributesStream);
                MessageBox.Show("Label attributes succefully saved");
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
