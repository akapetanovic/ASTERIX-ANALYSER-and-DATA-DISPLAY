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
using System.Xml;

namespace Google.KML
{
    /// <summary>
    /// Represents the Link object
    /// </summary>
    public class geLink : geObject
    {
        /// <summary>
        /// The location of external data
        /// </summary>
        public string Href; //required
        public geRefreshModeEnum RefreshMode = geRefreshModeEnum.onChange;
        public Single RefreshInterval = 4;
        public geViewRefreshEnum ViewRefreshMode = geViewRefreshEnum.never;
        public Single ViewRefreshTime = 4;
        public Single ViewBoundScale = 1;
        public string ViewFormat = "";
        public string HttpQuery = "";

        public geLink(string href)
        {
            Href = href;
        }
        
        /// <summary>
        /// Renders the object and any children as KML
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            kml.WriteStartElement("Link");
            kml.WriteAttributeString("id", ID);
            kml.WriteElementString("href",Href);
            if (RefreshMode != geRefreshModeEnum.onChange)
                kml.WriteElementString("refreshMode", RefreshMode.ToString());
            if (RefreshInterval != 4)
                kml.WriteElementString("refreshInterval", RefreshInterval.ToString());
            if (ViewRefreshMode != geViewRefreshEnum.never)
                kml.WriteElementString("viewRefreshMode", ViewRefreshMode.ToString());
            if (ViewRefreshTime != 4)
                kml.WriteElementString("viewRefreshTime",ViewRefreshTime.ToString());
            if (ViewBoundScale != 1)
                kml.WriteElementString("viewBoundScale", ViewBoundScale.ToString());
            if (ViewFormat.Length > 0)
                kml.WriteElementString("viewFormat", ViewFormat);
            if (HttpQuery.Length > 0 )
                kml.WriteElementString("httpQuery", HttpQuery);
            kml.WriteEndElement();
            
        }
    }


}
