using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT01I042Types
    {
        public class CAT01I042CalculatedPositionInCartesianCoordinates
        {
            public double X;
            public double Y;
            public GeoCordSystemDegMinSecUtilities.LatLongClass LatLong = new GeoCordSystemDegMinSecUtilities.LatLongClass();
        }
    }
}
