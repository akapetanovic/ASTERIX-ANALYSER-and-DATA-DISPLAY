using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using System.Drawing;
using GMap.NET;
using System.Reflection;

namespace AsterixDisplayAnalyser
{
    class RunwayBorderDisplay
    {
        public static void Build(ref GMapOverlay OverlayOut)
        {
            // Here loop through defined Runways and display them on the map
            foreach (SystemAdaptationDataSet.RunwayBorder Runway in SystemAdaptationDataSet.RunwayBorderDataSet)
            {
                System.Collections.Generic.List<PointLatLng> RunwayPointList = new System.Collections.Generic.List<PointLatLng>();
                foreach (GeoCordSystemDegMinSecUtilities.LatLongClass RunwayPoint in Runway.RunwayBorderPoints)
                {
                    RunwayPointList.Add(new PointLatLng(RunwayPoint.GetLatLongDecimal().LatitudeDecimal, RunwayPoint.GetLatLongDecimal().LongitudeDecimal));
                }

                // Get Runway border display attributes
                DisplayAttributes.DisplayAttributesType RunwayBorderDisplayAttribute = DisplayAttributes.GetDisplayAttribute(DisplayAttributes.DisplayItemsType.RunwayBorder);

                GMapPolygon RunwayPolygon = new GMapPolygon(RunwayPointList, Runway.RunwayName);
                RunwayPolygon.Stroke = new Pen(RunwayBorderDisplayAttribute.LineColor, RunwayBorderDisplayAttribute.LineWidth);

                Type brushType = typeof(Brushes);

                Brush myBrush = (Brush)brushType.InvokeMember(RunwayBorderDisplayAttribute.AreaPolygonColor.Name,
                 BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty,
                 null, null, new object[] { });

                RunwayPolygon.Fill = myBrush;
                OverlayOut.Polygons.Add(RunwayPolygon);

            }
        }
    }
}
