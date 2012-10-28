/*
 * ge-kml: A .NET 2.0 class library that implements the Google Earth 2.1 API
 * Copyright (C) 2006  Ryan M. Johnston (SourceForge-boseefus00001)
 * Questions, Comments, Praise, and Flame can be sent to ryan-gekml@homieshouse.com
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

// **** Modification History **** //
/*
 * 2006-12-19: boseefus00001
 *      * Initial release
 *
 * 2007-02-20: boseefus00001
 *      * Changed all references to geAngleXXX.Value.ToString() to geAngleXXX.ToString().
 *        The geAngleXXX.ToString() Method new overrides it's default behavior and returns an InvariantCulture
 *        string representation of it's Value in order to support other languages.
 *
 * YYYY-MM-DD: <developer>
 *      *  Modification
 * 
 */

using System;
using System.Xml;

namespace Google.KML
{
    /// <summary>
    /// Represents the Coordinates property which contains
    /// minimally a Lat/Lon, and optionally an Altitude.
    /// </summary>
    public class geCoordinates : geObject
        {
            public geAngle180 Longitude;
            public geAngle90 Latitude ;
            public Single Altitude;
            
        
        /// <summary>
        /// This constructor will pass to the Lat/Lon/Alt constructor
        /// assigning altitude the value of Single.NaN
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public geCoordinates(geAngle90 latitude, geAngle180 longitude)
            : this(latitude, longitude, Single.NaN) { }
           
        public geCoordinates(geAngle90 latitude, geAngle180 longitude, Single altitude) 
        {
            Altitude = altitude;
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Renders the object as KML, and calls upon any children to do the same
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            string coord = Longitude.ToString() + "," + Latitude.ToString();
            if (!Altitude.Equals(Single.NaN))
                coord += "," + Altitude.ToString();
            kml.WriteElementString("coordinates", coord);
        }

        /// <summary>
        /// Returns only the Lon,Lat[,Alt] string without any KML elements
        /// </summary>
        /// <returns></returns>
        public string ToTuple()
        {
            string coord = Longitude.ToString() + "," + Latitude.ToString();
            if (!Altitude.Equals(Single.NaN))
                coord += "," + Altitude.ToString();
            return coord;
        }
    }
}
