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
 */

using System;
using System.Configuration;
using System.Xml;

namespace Google.KML
{
    /// <summary>
    /// A 3d object from a Collada (.dae) file
    /// </summary>
    public class geModel : geGeometry
    {
        public geAltitudeModeEnum AltitudeMode = geAltitudeModeEnum.clampToGround;
        public geLocation Location;
        public geOrientation Orientation;
        public geScale Scale;
        public geLink Link;

        /// <summary>
        /// Render the object to KML
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            kml.WriteStartElement("Model");
            if ((ID != null) && (ID.Length > 0))
                kml.WriteAttributeString("id", ID);
            if (AltitudeMode != geAltitudeModeEnum.clampToGround)
                kml.WriteElementString("altitudeMode", AltitudeMode.ToString());
            
            if (Location!=null)
                Location.ToKML(kml);
            
            if (Orientation!=null)
               Orientation.ToKML(kml);

            if (Scale!=null)
                Scale.ToKML(kml);

            if (Link !=null )
                Link.ToKML(kml);
            
            kml.WriteEndElement();
           
        }
    }
}

