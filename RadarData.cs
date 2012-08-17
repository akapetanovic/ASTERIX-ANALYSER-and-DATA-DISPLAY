using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticastingUDP
{
    class RadarData
    {
        public class RadarDataSet
        {
            public string RadarName;
            public GeoCordSystemDegMinSecUtilities.LatLongClass RadarPosition = new GeoCordSystemDegMinSecUtilities.LatLongClass();
            public string SIC;
            public string SAC;

            public RadarDataSet(string R_Name, GeoCordSystemDegMinSecUtilities.LatLongClass R_Position, string R_SIC, string R_SAC)
            {
                RadarName = R_Name;
                RadarPosition = new GeoCordSystemDegMinSecUtilities.LatLongClass(R_Position.GetLatLongDecimal().LatitudeDecimal, R_Position.GetLatLongDecimal().LongitudeDecimal);
                SIC = R_SIC;
                SAC = R_SAC;
            }
        }

        public static System.Collections.Generic.List<RadarDataSet> RadarDataSets = new System.Collections.Generic.List<RadarDataSet>();

        public static void InitializeData()
        {
            // Add SJJ TWR
            RadarDataSets.Add(new RadarDataSet("Sarajevo TWR", new GeoCordSystemDegMinSecUtilities.LatLongClass(43, 49, 11.76, GeoCordSystemDegMinSecUtilities.LatLongPrefix.E, 18, 20, 23.4, GeoCordSystemDegMinSecUtilities.LatLongPrefix.N), "8", "34"));
            // Add Jahorina
            RadarDataSets.Add(new RadarDataSet("Jahorina", new GeoCordSystemDegMinSecUtilities.LatLongClass(43, 43, 33.69, GeoCordSystemDegMinSecUtilities.LatLongPrefix.E, 18, 33, 5.69, GeoCordSystemDegMinSecUtilities.LatLongPrefix.N), "23", "2"));
        }
    }



}
