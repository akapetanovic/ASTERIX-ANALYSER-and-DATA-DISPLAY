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
 * 2007-05-29: Maksim Sestic
 *      * Updated XML tags
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
    /// Describes rotation of a 3D model's coordinate system to position the object in Google Earth.
    /// </summary>
    public class geOrientation : geObject
    {
        /// <summary>
        /// Rotation about the z axis. A value of 0 (the default) equals North.
        /// </summary>
        /// <remarks>
        /// A positive rotation is clockwise around the z axis and specified in degrees from 0 to ±180.
        /// </remarks>
        public geAngle180 Heading = new geAngle180(0);

        /// <summary>
        /// Rotation about the x axis. Default equals to 0.
        /// </summary>
        /// <remarks>
        /// A positive rotation is clockwise around the x axis and specified in degrees from 0 to ±180.
        /// </remarks>
        public geAngle180 Tilt = new geAngle180(0);

        /// <summary>
        /// Rotation about the y axis. Default equals to 0.
        /// </summary>
        /// <remarks>
        /// A positive rotation is clockwise around the y axis and specified in degrees from 0 to ±180.
        /// </remarks>
        public geAngle180 Roll = new geAngle180(0);

        /// <summary>
        /// Renders the object to KML.
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            
            kml.WriteStartElement("Orientation");
            if (Heading.Value != 0)
                kml.WriteElementString("heading", Heading.ToString());
            if (Tilt.Value != 0)
                kml.WriteElementString("tilt", Tilt.ToString());
            if (Roll.Value != 0 )
                kml.WriteElementString("roll", Roll.ToString());
            kml.WriteEndElement();
           
        }
    }
}
