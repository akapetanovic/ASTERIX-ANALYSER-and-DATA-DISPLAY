using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using System.Drawing;
using GMap.NET;
using System.Resources;

namespace AsterixDisplayAnalyser
{
    class RadarDisplay
    {

        public static void Build(ref GMapOverlay OverlayOut)
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Display defined radars
            //
            Image RadarImage = (Image)AsterixDisplayAnalyser.Properties.Resources.ResourceManager.GetObject("radar");

            // Get radar display attributes
            DisplayAttributes.DisplayAttributesType RadarDisplayAttribute = DisplayAttributes.GetDisplayAttribute(DisplayAttributes.DisplayItemsType.Radar);
            RadarImage = GraphicUtilities.ResizeImage(RadarImage, new Size(RadarDisplayAttribute.ImageSize.Width, RadarDisplayAttribute.ImageSize.Height), false);
            
            // Here loop through defined radars and display them on the map
            foreach (SystemAdaptationDataSet.Radar Radar in SystemAdaptationDataSet.RadarDataSet)
            {
                // Image properties
                GMapMarkerImage MyMarkerImage =
                    new GMapMarkerImage(new PointLatLng(Radar.RadarPosition.GetLatLongDecimal().LatitudeDecimal, Radar.RadarPosition.GetLatLongDecimal().LongitudeDecimal), RadarImage);
                MyMarkerImage.ToolTipMode = MarkerTooltipMode.Never;
                System.Drawing.SolidBrush myBrush;
                
                myBrush = new System.Drawing.SolidBrush(RadarDisplayAttribute.TextColor);
                
                // Get radar marker image
                WaypointMarker WPT_Marker = new WaypointMarker(new PointLatLng(Radar.RadarPosition.GetLatLongDecimal().LatitudeDecimal, Radar.RadarPosition.GetLatLongDecimal().LongitudeDecimal), Radar.RadarName,
                   new Font(RadarDisplayAttribute.TextFont, RadarDisplayAttribute.TextSize, FontStyle.Bold, GraphicsUnit.Pixel), myBrush);

                // Load radar marker and label to overlay
                OverlayOut.Markers.Add(MyMarkerImage);
                OverlayOut.Markers.Add(WPT_Marker);
            }
        }
    }
}
