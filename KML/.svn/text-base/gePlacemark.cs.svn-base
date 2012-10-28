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
 * YYYY-MM-DD: <developer>
 *      *  Modification
 * 
 */

using System;
using System.Xml;


namespace Google.KML
{
    /// <summary>
    /// A Placemark is a Feature with associated Geometry. In Google Earth, a 
    /// Placemark appears as a list item in the Places panel. A Placemark with 
    /// a Point has an icon associated with it that marks a point on the earth 
    /// in the 3D viewer. (In the Google Earth 3D viewer, a Point Placemark is 
    /// the only object you can click or roll over. Other Geometry objects do 
    /// not have an icon in the 3D viewer. To give the user something to click, 
    /// you would need to add a Point to the Polygon.)
    /// </summary>
    public class gePlacemark : geFeature
    {
        public geGeometry Geometry;

        /// <summary>
        /// Render to KML
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            kml.WriteStartElement("Placemark");
            if ((ID != null) && (ID.Length > 0))
                kml.WriteAttributeString("id", ID);
            base.ToKML(kml);
            Geometry.ToKML(kml);
            kml.WriteEndElement();
            
        }
               

    }
}