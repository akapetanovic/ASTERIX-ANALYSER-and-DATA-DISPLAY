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
 * 2006-12-19: boseefus00001 - Initial release
 * 
 * 2007-02-20: boseefus00001
 *      * Changed all references to geAngleXXX.Value.ToString() to geAngleXXX.ToString().
 *        The geAngleXXX.ToString() Method new overrides it's default behavior and returns an InvariantCulture
 *        string representation of it's Value in order to support other languages.
 *
 * 2007-08-27: boseefus00001
 *      * Changed "Lookat" to "LookAt" in ToKML().  Thanks to peter for pointing out the typo.
 * 
 */

using System;
using System.Xml;

namespace Google.KML
{
    /// <summary>
    /// Defines the camera position while looking at a objects
    /// </summary>
    public class geLookAt : geObject
    {
        public geAngle180 Longitude;
        public geAngle90 Latitude;
        public double Altitude = 0;
        public double Range;
        public geAnglePos90 Tilt = new geAnglePos90(0);
        public Single Heading = 0;
        public geAltitudeModeEnum AltitudeMode = geAltitudeModeEnum.clampToGround;

        public geLookAt(geAngle90 latitude, geAngle180 longitude, double range)
        {
            Latitude = latitude;
            Longitude = longitude;
            Range = range;
        }

        /// <summary>
        /// Renders the object as KML
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            kml.WriteStartElement("LookAt");
            if ((ID !=null) && (ID.Length > 0))
                kml.WriteAttributeString("id", this.ID);

            kml.WriteElementString("latitude", Latitude.ToString());
            kml.WriteElementString("longitude", Longitude.ToString());
            kml.WriteElementString("range", Range.ToString());

            if (Altitude != 0)
                kml.WriteElementString("altitude", Altitude.ToString()); 
            
            if (Heading != 0)
                kml.WriteElementString("heading", Heading.ToString());

            if (Tilt.Value != 0)
                kml.WriteElementString("tilt", Tilt.ToString());

            if (AltitudeMode != geAltitudeModeEnum.clampToGround)
                kml.WriteElementString("altitudeMode", AltitudeMode.ToString());

            kml.WriteEndElement();

        }
    }
}