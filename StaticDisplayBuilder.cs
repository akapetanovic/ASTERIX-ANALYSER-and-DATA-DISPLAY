using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GMap.NET.WindowsForms;
using GMap.NET;
using GMap.NET.MapProviders;

namespace MulticastingUDP
{
    class StaticDisplayBuilder
    {
        public static void UpdateRadarMarkers(ref GMapOverlay OverlayOut)
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Display defined radars
            //
            string FileName = @"C:\ASTERIX\IMAGES\radar.jpg";
            Image RadarImage = Image.FromFile(FileName);
            
            // Here loop through defined radars and display them on the map
            foreach (SystemAdaptationDataSet.Radar Radar in SystemAdaptationDataSet.RadarDataSet)
            {
                // Image properties
                GMapMarkerImage MyMarkerImage =
                    new GMapMarkerImage(new PointLatLng(Radar.RadarPosition.GetLatLongDecimal().LatitudeDecimal, Radar.RadarPosition.GetLatLongDecimal().LongitudeDecimal), RadarImage);
                MyMarkerImage.ToolTipMode = MarkerTooltipMode.Never;

                System.Drawing.SolidBrush myBrush;
                myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                WaypointMarker WPT_Marker = new WaypointMarker(new PointLatLng(Radar.RadarPosition.GetLatLongDecimal().LatitudeDecimal, Radar.RadarPosition.GetLatLongDecimal().LongitudeDecimal), Radar.RadarName,
                   new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold, GraphicsUnit.Pixel), myBrush);

                // Load marker to overlay
                OverlayOut.Markers.Add(MyMarkerImage);
                OverlayOut.Markers.Add(WPT_Marker);
            }
        }


        public static void UpdateWaypointMarkers(ref GMapOverlay OverlayOut)
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Display defined radars
            //
            string FileName = @"C:\ASTERIX\IMAGES\waypoint.jpg";
            Image WaypointImage = Image.FromFile(FileName);

            // Here loop through defined waypoints and display them on the map
            foreach (SystemAdaptationDataSet.Waypoint Waypoint in SystemAdaptationDataSet.WaypointDataSet)
            {
                // Image properties
                GMapMarkerImage MyMarkerImage =
                    new GMapMarkerImage(new PointLatLng(Waypoint.WaypointPosition.GetLatLongDecimal().LatitudeDecimal, Waypoint.WaypointPosition.GetLatLongDecimal().LongitudeDecimal), WaypointImage);
                MyMarkerImage.ToolTipMode = MarkerTooltipMode.Never;
                
                System.Drawing.SolidBrush myBrush;
                myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                WaypointMarker WPT_Marker = new WaypointMarker(new PointLatLng(Waypoint.WaypointPosition.GetLatLongDecimal().LatitudeDecimal, Waypoint.WaypointPosition.GetLatLongDecimal().LongitudeDecimal), Waypoint.WaypointName, 
                   new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold, GraphicsUnit.Pixel), myBrush); 

                // Load marker to overlay
                OverlayOut.Markers.Add(MyMarkerImage);
                OverlayOut.Markers.Add(WPT_Marker);
            }
        }

    }
}
