using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.KML;
using System.IO;
using System.Drawing;

namespace AsterixDisplayAnalyser
{
    class Asterix_To_KML_Provider
    {
        // This is the main storage of all the targets
        private System.Collections.Generic.List<DynamicDisplayBuilder.TargetType> TargetList = new System.Collections.Generic.List<DynamicDisplayBuilder.TargetType>();

        public void AddNewTarget(DynamicDisplayBuilder.TargetType NewTarget)
        {
            TargetList.Add(NewTarget);
        }

        // This method is to be called once all the targets
        // are loaded via AddNewTarget. Once KML is created the list
        // will be emptied.
        public void BuildKML()
        {

            string exampleFileName = "";
            string exampleFileExt = "kmz";
            string file_name = "ASTX_TO_KML";

            // Use a Document as the root of the KML
            geDocument doc = new geDocument();
            doc.Name = "Asterix to KML";

            // Create a new style to be added the doc
            geStyle myStyle = new geStyle("myStyle");

            //add a new IconStyle to the style
            myStyle.IconStyle = new geIconStyle();
            myStyle.IconStyle.Icon = new geIcon("ac_image.png");
            myStyle.IconStyle.Scale = 1.0F; //or (float)1

            myStyle.LabelStyle = new geLabelStyle();
            myStyle.LabelStyle.Color.SysColor = Color.White;

            myStyle.LineStyle = new geLineStyle();
            myStyle.LineStyle.Color.SysColor = Color.Black;
            myStyle.LineStyle.Width = 4;


            //Add the style
            doc.StyleSelectors.Add(myStyle);

            foreach (DynamicDisplayBuilder.TargetType Target in TargetList)
            {
                //Create a Placemark to put in the document
                //This placemark is going to be a point
                //but it could be anything in the Geometry class
                gePlacemark pm = new gePlacemark();

                //Create some coordinates for the point at which
                //this placemark will sit. (Lat / Lon)
                geCoordinates coords = new geCoordinates(
                    new geAngle90(Target.Lat),
                new geAngle180(Target.Lon));

                double LevelInMeeters = 0.0;

                // Assign the altitude
                if (Target.ModeC != null)
                {
                    if (Target.ModeC != "---")
                        LevelInMeeters = (double.Parse(Target.ModeC) * 100.00) * SharedData.FeetToMeeters;
                }

                coords.Altitude = (float)LevelInMeeters;

                //Create a point with these new coordinates
                gePoint point = new gePoint(coords);
                point.AltitudeMode = geAltitudeModeEnum.absolute;
                point.Extrude = true;

                //Assign the point to the Geometry property of the
                //placemark.
                pm.Geometry = point;

                //Set the placemark's style to the style we created above
                pm.StyleUrl = "#myStyle";


                if (Properties.Settings.Default.GE_Show_ModeA)
                    pm.Name = Target.ModeA;

                if (Properties.Settings.Default.GE_Show_ModeC)
                {
                    if (Properties.Settings.Default.Show_ModeC_as_FL)
                        pm.Name = pm.Name + "  " + "FL:" + Target.ModeC;
                    else
                        pm.Name = pm.Name + "  " + LevelInMeeters.ToString() + "m";
                }

                if (Properties.Settings.Default.GE_Show_ModeC)
                {
                    pm.Name = pm.Name + " " + Target.ACID_Mode_S;
                }

                pm.Snippet = "Snipet Test";
                pm.Description = "Blaa Bla Blaaaa";

                //Finally, add the placemark to the document
                doc.Features.Add(pm);
            }

            geKML kml = new geKML(doc);

            //Add supporting files to the KMZ (assuming it's going to be rendered as KMZ
            byte[] myFile = File.ReadAllBytes(@"C:\ASTERIX\GE\ac_image.png");
            kml.Files.Add("ac_image.png", myFile);

            exampleFileName = Properties.Settings.Default.GE_Dest_Path + "\\" + file_name + "." + exampleFileExt;
            File.WriteAllBytes(exampleFileName, kml.ToKMZ());

            // Clear the list
            TargetList.Clear();
        }
    }
}
