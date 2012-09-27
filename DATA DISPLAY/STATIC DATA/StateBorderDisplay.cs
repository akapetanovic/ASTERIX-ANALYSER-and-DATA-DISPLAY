using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using System.Drawing;
using GMap.NET;

namespace AsterixDisplayAnalyser
{
    class StateBorderDisplay
    {
        public static void Build(ref GMapOverlay OverlayOut)
        {
            // Here loop through defined state borders and display them on the map
            foreach (SystemAdaptationDataSet.StateBorder State in SystemAdaptationDataSet.StateBorderDataSet)
            {
                System.Collections.Generic.List<PointLatLng> SectorPointList = new System.Collections.Generic.List<PointLatLng>();
                foreach (GeoCordSystemDegMinSecUtilities.LatLongClass SectorPoint in State.StateBorderPoints)
                {
                    SectorPointList.Add(new PointLatLng(SectorPoint.GetLatLongDecimal().LatitudeDecimal, SectorPoint.GetLatLongDecimal().LongitudeDecimal));
                }

                GMapRoute StateBoundaryData = new GMapRoute(SectorPointList, State.StateName);
                StateBoundaryData.Stroke.Width = DisplayAttributes.GetDisplayAttribute(DisplayAttributes.DisplayItemsType.StateBorder).LineWidth;
                StateBoundaryData.Stroke.DashStyle = DisplayAttributes.GetDisplayAttribute(DisplayAttributes.DisplayItemsType.StateBorder).LineStyle;
                StateBoundaryData.Stroke.Color = DisplayAttributes.GetDisplayAttribute(DisplayAttributes.DisplayItemsType.StateBorder).LineColor;
                OverlayOut.Routes.Add(StateBoundaryData);
            }
        }
    }
}
