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
    /// References a local or remote KML/KMZ file
    /// </summary>
    public class geNetworkLink : geFeature
    {
        public geLink Link;
        public bool RefreshVisibility = false;
        public bool FlyToView = false;
        

        public geNetworkLink(geLink link)
        {
            Link = link;
        }

        /// <summary>
        /// Render the object to KML
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            kml.WriteStartElement("NetworkLink");
            
            if ((ID != null) && (ID.Length > 0))
                kml.WriteAttributeString("id", ID);
            
            base.ToKML(kml);
            
            Link.ToKML(kml);
            
            if (RefreshVisibility)
                kml.WriteElementString("refreshVisibility", Convert.ToInt16(RefreshVisibility).ToString());
            
            if (FlyToView)
                kml.WriteElementString("flyToView", Convert.ToInt16(FlyToView).ToString());

            kml.WriteEndElement();
            
        }
    }
}
