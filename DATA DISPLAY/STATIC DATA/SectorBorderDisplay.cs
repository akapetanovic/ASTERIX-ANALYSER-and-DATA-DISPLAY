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
    class SectorBorderDisplay
    {
        public static void Build(ref GMapOverlay OverlayOut)
        {
            // Here loop through defined sectors and display them on the map
            foreach (SystemAdaptationDataSet.SectorBorder Sector in SystemAdaptationDataSet.SectorBorderDataSet)
            {
                System.Collections.Generic.List<PointLatLng> SectorPointList = new System.Collections.Generic.List<PointLatLng>();
                foreach (GeoCordSystemDegMinSecUtilities.LatLongClass SectorPoint in Sector.SectorBorderPoints)
                {
                    SectorPointList.Add(new PointLatLng(SectorPoint.GetLatLongDecimal().LatitudeDecimal, SectorPoint.GetLatLongDecimal().LongitudeDecimal));
                }

                // Get sector border display attributes
                DisplayAttributes.DisplayAttributesType SectorBorderDisplayAttribute = DisplayAttributes.GetDisplayAttribute(DisplayAttributes.DisplayItemsType.SectorBorder);
                
                GMapPolygon SectorPolygon = new GMapPolygon(SectorPointList, Sector.SectorName);
                SectorPolygon.Stroke = new Pen(SectorBorderDisplayAttribute.LineColor, SectorBorderDisplayAttribute.LineWidth);

                Type brushType = typeof(Brushes);

                Brush myBrush = (Brush)brushType.InvokeMember(SectorBorderDisplayAttribute.AreaPolygonColor.Name,
                 BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty,
                 null, null, new object[] { });

                SectorPolygon.Fill = myBrush;
                OverlayOut.Polygons.Add(SectorPolygon);
              
            }
        }
    }
}
