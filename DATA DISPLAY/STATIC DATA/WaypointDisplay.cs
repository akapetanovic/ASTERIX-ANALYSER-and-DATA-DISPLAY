using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using System.Drawing;
using GMap.NET;

namespace AsterixDisplayAnalyser
{
    class WaypointDisplay
    {

        public static void Build(ref GMapOverlay OverlayOut)
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Display defined radars
            //
            string FileName = @"C:\ASTERIX\IMAGES\waypoint.jpg";
            Image WaypointImage = Image.FromFile(FileName);

            DisplayAttributes.DisplayAttributesType WaypointDisplayAttribute = DisplayAttributes.GetDisplayAttribute(DisplayAttributes.DisplayItemsType.Waypoint);

            WaypointImage = GraphicUtilities.ResizeImage(WaypointImage, new Size(WaypointDisplayAttribute.ImageSize.Width, WaypointDisplayAttribute.ImageSize.Height), false);
            
            // Here loop through defined waypoints and display them on the map
            foreach (SystemAdaptationDataSet.Waypoint Waypoint in SystemAdaptationDataSet.WaypointDataSet)
            {
                // Image properties
                GMapMarkerImage MyMarkerImage =
                    new GMapMarkerImage(new PointLatLng(Waypoint.WaypointPosition.GetLatLongDecimal().LatitudeDecimal, Waypoint.WaypointPosition.GetLatLongDecimal().LongitudeDecimal), WaypointImage);
                MyMarkerImage.ToolTipMode = MarkerTooltipMode.Never;

                System.Drawing.SolidBrush myBrush;
                myBrush = new System.Drawing.SolidBrush(WaypointDisplayAttribute.TextColor);
                WaypointMarker WPT_Marker = new WaypointMarker(new PointLatLng(Waypoint.WaypointPosition.GetLatLongDecimal().LatitudeDecimal, Waypoint.WaypointPosition.GetLatLongDecimal().LongitudeDecimal), Waypoint.WaypointName,
                   new Font(WaypointDisplayAttribute.TextFont, WaypointDisplayAttribute.TextSize, FontStyle.Regular, GraphicsUnit.Pixel), myBrush);

                // Load marker to overlay
                OverlayOut.Markers.Add(MyMarkerImage);
                OverlayOut.Markers.Add(WPT_Marker);
            }
        }
    }
}
