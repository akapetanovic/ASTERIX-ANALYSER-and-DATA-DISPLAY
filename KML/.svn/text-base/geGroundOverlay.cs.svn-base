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
 * 
 * 2007-02-20: boseefus00001
 *      * Changed all references to geAngleXXX.Value.ToString() to geAngleXXX.ToString().
 *        The geAngleXXX.ToString() Method new overrides it's default behavior and returns an InvariantCulture
 *        string representation of it's Value in order to support other languages.
 *
 * 2007-08-27: boseefus00001
 *      * Added "rotation" to ToKML().  Thanks to peter for pointing out that I missed it.
 * 
 * YYYY-MM-DD: <developer>
 *      *  Modification
 */

using System;
using System.Drawing;
using System.Xml;

namespace Google.KML
{
    /// <summary>
    /// Represents the GroundOverlay object for laying icons/images 
    /// over the Google Earth terrain
    /// </summary>
    public class geGroundOverlay : geOverlay
    {
        /// <summary>
        /// A Lat/Lon "Box" that is used in a GroundOverlay
        /// to describe how to stretch the image over the
        /// terrain.
        /// </summary>
        public class geLatLonBox : geObject
        {
            public geAngle90 North;
            public geAngle90 South;
            public geAngle180 East;
            public geAngle180 West;
            public geAngle180 Rotation;

            /// <summary>
            /// Passes the constructor on to the N/S/E/W/Rotation
            /// constructor with the Rotation value of 0
            /// </summary>
            /// <param name="north"></param>
            /// <param name="south"></param>
            /// <param name="east"></param>
            /// <param name="west"></param>
            public geLatLonBox(geAngle90 north, geAngle90 south, geAngle180 east, geAngle180 west)
                :this(north,south,east,west,new geAngle180(0)){}

            public geLatLonBox(geAngle90 north, geAngle90 south, geAngle180 east, geAngle180 west, geAngle180 rotation)
            {
                North = north;
                South = south;
                East = east;
                West = west;
                Rotation = rotation;
            }

            /// <summary>
            /// Renders the object to KML
            /// </summary>
            /// <param name="kml"></param>
            public override void ToKML(XmlTextWriter kml)
            {
                kml.WriteStartElement("LatLonBox");
                kml.WriteElementString("north", North.ToString());
                kml.WriteElementString("south", South.ToString());
                kml.WriteElementString("east", East.ToString());
                kml.WriteElementString("west", West.ToString());
                kml.WriteElementString("rotation", Rotation.ToString());
                kml.WriteEndElement();
            }
        }


        public double Altitude;
        public geAltitudeModeEnum AltitudeMode = geAltitudeModeEnum.clampToGround;
        public geLatLonBox LatLonBox;

        public geGroundOverlay(geLatLonBox latLonBox)
        {
            LatLonBox = latLonBox;
            SysColor.SysColor = Color.White;
        }

        /// <summary>
        /// Renders the object and any children to KML
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            kml.WriteStartElement("GroundOverlay");
            base.ToKML(kml);
            kml.WriteElementString("altitude", Altitude.ToString());
            kml.WriteElementString("altitudeMode", AltitudeMode.ToString());
            LatLonBox.ToKML(kml);
            kml.WriteEndElement();
        }
    }
}