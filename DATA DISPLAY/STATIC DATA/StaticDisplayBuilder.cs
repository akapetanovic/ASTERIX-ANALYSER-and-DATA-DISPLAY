using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GMap.NET.WindowsForms;
using GMap.NET;
using GMap.NET.MapProviders;

namespace AsterixDisplayAnalyser
{
    class StaticDisplayBuilder
    {
        public static void Build(ref GMapOverlay Overlay)
        {
            
            if (Properties.Settings.Default.Radars)
                RadarDisplay.Build(ref Overlay);

            if (Properties.Settings.Default.Waypoints)
                WaypointDisplay.Build(ref Overlay);

            if (Properties.Settings.Default.StateBorder)
                StateBorderDisplay.Build(ref Overlay);

            if (Properties.Settings.Default.Sectors)
                SectorBorderDisplay.Build(ref Overlay);

            if (Properties.Settings.Default.Runways)
                RunwayBorderDisplay.Build(ref Overlay);
        }
    }
}
