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
//using System.Drawing;
using System.Xml;

namespace Google.KML
{

    public class geIconStyle : geColorStyle
    {
        /// <summary>
        /// The icon to display
        /// </summary>
        public geIcon Icon;
        /// <summary>
        /// relative size of the icon
        /// </summary>
        public Single Scale = 1;
        /// <summary>
        /// The position of the icon bounding box around a point
        /// </summary>
        public geVec2 HotSpot;

        public geIconStyle()
        {

        }

        /// <summary>
        /// Renders the object and any children as KML
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            kml.WriteStartElement("IconStyle");
            kml.WriteAttributeString("id", this.ID);
            base.ToKML(kml);
            
            kml.WriteElementString("scale", Scale.ToString());

            if (Icon != null)
                Icon.ToKML(kml);
      

            kml.WriteStartElement("hotSpot");
            kml.WriteAttributeString("x", HotSpot.x.ToString());
            kml.WriteAttributeString("y", HotSpot.y.ToString());
            kml.WriteAttributeString("xunits", HotSpot.xunits.ToString());
            kml.WriteAttributeString("yunits", HotSpot.yunits.ToString());
            kml.WriteEndElement();

            kml.WriteEndElement();
           
        }
    }
}
