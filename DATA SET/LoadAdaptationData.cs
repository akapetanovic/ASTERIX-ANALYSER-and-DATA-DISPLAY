﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser
{
    class LoadAdaptationData
    {

        // This method reads in system adaptation data set from configuration files at system power up and adjust the system in accordance to the system defined settings.
        // It expects configuration files in a specified directory. In the case they are not found the system will default to the default development data set.
        public static void Load()
        {
            
            /////////////////////////////////////////////////////////////////////////
            // First set the system origin
            /////////////////////////////////////////////////////////////////////////
            LoadCoreSettings.Load();

            /////////////////////////////////////////////////////////////////////////
            // Load radars
            /////////////////////////////////////////////////////////////////////////
            LoadRadars.Load();

            /////////////////////////////////////////////////////////////////////////
            // Load waypoints
            /////////////////////////////////////////////////////////////////////////
            LoadWaypoints.Load();

            /////////////////////////////////////////////////////////////////////////
            // Load state boundaries
            /////////////////////////////////////////////////////////////////////////
            LoadStateBoundaries.Load();

            /////////////////////////////////////////////////////////////////////////
            // Load sector boundaries
            /////////////////////////////////////////////////////////////////////////
            LoadSectorBoundaries.Load();

            /////////////////////////////////////////////////////////////////////////
            // Load runway boundaries
            /////////////////////////////////////////////////////////////////////////
            LoadRunwayBoundaries.Load();

            /////////////////////////////////////////////////////////////////////////
            // Load label attributes
            /////////////////////////////////////////////////////////////////////////
            LabelAttributes.Load();
            
            /////////////////////////////////////////////////////////////////////////
            // Now handle display preferences
            /////////////////////////////////////////////////////////////////////////
            Display_Attributes_IO.Load();
        }
    }
}
