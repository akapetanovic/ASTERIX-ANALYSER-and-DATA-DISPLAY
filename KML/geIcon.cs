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
    /// Represents the Icon object
    /// </summary>
    public class geIcon : geObject
    {

        /// <summary>
        /// The http, local file, or document relative address of the 
        /// image to use as an icon/overlay
        /// </summary>
        public string Href = ""; 
        /// <summary>
        /// The number of seconds to wait before refreshing the image
        /// </summary>
        public int RefreshInterval = 4;
        /// <summary>
        /// When to refresh the image
        /// </summary>
        public geRefreshModeEnum RefreshMode = geRefreshModeEnum.onChange;
        /// <summary>
        /// 
        /// </summary>
        private double ViewBoundScale = 1;
        /// <summary>
        /// 
        /// </summary>
        private string ViewFormat = "";
        /// <summary>
        /// 
        /// </summary>
        public geViewRefreshEnum ViewRefreshMode = geViewRefreshEnum.never;
        /// <summary>
        /// 
        /// </summary>
        public int ViewRefreshTime = 4;
        /// <summary>
        /// 
        /// </summary>
        public string HttpQuery = "";
                
        public geIcon(string href)
        {
            Href = href;
        }
            
        /// <summary>
        /// Renders this object and any children to KML
        /// </summary>
        /// <param name="kml"></param>
        public override void  ToKML(XmlTextWriter kml)
        {
            kml.WriteStartElement("Icon");
            if ((ID != null) && (ID.Length > 0))
                kml.WriteAttributeString("id", ID);
            if (Href.Length > 0)
                kml.WriteElementString("href", Href);
            if (RefreshMode != geRefreshModeEnum.onChange)
                kml.WriteElementString("refreshMode", RefreshMode.ToString());
            if (RefreshInterval != 4)
                kml.WriteElementString("refreshInterval", RefreshInterval.ToString());
            if (ViewRefreshMode != geViewRefreshEnum.never)
                kml.WriteElementString("viewRefreshMode", ViewRefreshMode.ToString());
            if (ViewBoundScale != 1)
                kml.WriteElementString("viewBoundScale", ViewBoundScale.ToString());
            if (ViewFormat.Length > 0)
                kml.WriteElementString("viewFormat", ViewFormat);
            if (ViewRefreshTime != 4)
                kml.WriteElementString("viewRefreshTime", ViewRefreshTime.ToString());
            if (HttpQuery.Length >0)
                kml.WriteElementString("httpQuery", HttpQuery);
            kml.WriteEndElement();
            
        }
    }
}