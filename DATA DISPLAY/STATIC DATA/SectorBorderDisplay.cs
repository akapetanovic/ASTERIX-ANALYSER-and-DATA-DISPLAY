using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using System.Drawing;
using GMap.NET;

namespace MulticastingUDP
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
                SectorPolygon.Stroke = new Pen(SectorBorderDisplayAttribute.TextColor, SectorBorderDisplayAttribute.LineWidth);
                SectorPolygon.Fill = Brushes.Black;
                OverlayOut.Polygons.Add(SectorPolygon);
            }
        }
    }
}
