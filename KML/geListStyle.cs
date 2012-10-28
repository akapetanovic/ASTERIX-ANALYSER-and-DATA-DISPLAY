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
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace Google.KML
{
    /// <summary>
    /// Represents the ListStyle object, or how icons should show up in the places window
    /// </summary>
    public class geListStyle : geObject
    {
        /// <summary>
        /// Represents an ItemIcon, the icon that you see in the Places window
        /// </summary>
        public class geItemIcon : geObject
        {
            /// <summary>
            /// List the valid icon states
            /// </summary>
            public enum geItemIconStates
            {
                open,
                closed,
                error,
                fetching0,
                fetching1,
                fetching2
            }
            public geItemIconStates State;
            public string Href = "";

            /// <summary>
            /// Renders the object as KML
            /// </summary>
            /// <param name="kml"></param>
            public override void ToKML(XmlTextWriter kml)
            {
                kml.WriteStartElement("ItemIcon");
                kml.WriteElementString("state", State.ToString());
                kml.WriteElementString("href", Href);
                kml.WriteEndElement();
                
            }
        }

        /// <summary>
        /// The collection of icons to be used for this style
        /// </summary>
        public List<geItemIcon> ItemIcons = new List<geItemIcon>();
        /// <summary>
        /// Background color for the snippet
        /// </summary>
        public geColor BgColor = new geColor();
        /// <summary>
        /// The type of item to display in the places window
        /// </summary>
        public geListItemTypeEnum ListItemType = geListItemTypeEnum.check;
        
        public geListStyle()
        {
            BgColor.SysColor = Color.White;
        }

        /// <summary>
        /// Renders the object and any children as KML
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            kml.WriteStartElement("ListStyle");
            kml.WriteAttributeString("id", ID);
            if (BgColor.SysColor != Color.White)
                kml.WriteElementString("bgColor", BgColor.ToKML());
            if (ListItemType != geListItemTypeEnum.check)
                kml.WriteElementString("listItemType", ListItemType.ToString());
            foreach (geItemIcon ico in ItemIcons)
            {
                ico.ToKML(kml);
            }
            kml.WriteEndElement();
           
        }
    }
      
}
