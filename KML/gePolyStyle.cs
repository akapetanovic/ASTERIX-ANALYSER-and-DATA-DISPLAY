
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
    /// Specifies the drawing style for all polygons, including polygon 
    /// extrusions (which look like the walls of buildings) and line extrusions 
    /// (which look like solid fences). 
    /// </summary>
    public class gePolyStyle : geColorStyle
    {
        /// <summary>
        /// Boolean value (default=1). Specifies whether to fill the polygon
        /// </summary>
        public bool Fill = true;
        /// <summary>
        /// Boolean value (default=1). Specifies whether to outline the polygon. 
        /// Polygon outlines use the current LineStyle. 
        /// </summary>
        public bool Outline = true;

        /// <summary>
        /// Render to KML
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            kml.WriteStartElement("PolyStyle");
            if ((ID != null) && (ID.Length > 0))
                kml.WriteAttributeString("id", this.ID);
            base.ToKML(kml);
            if (!Fill)
                kml.WriteElementString("fill", Convert.ToInt16(Fill).ToString());
            if (!Outline)
                kml.WriteElementString("outline", Convert.ToInt16(Outline).ToString());
            kml.WriteEndElement();
            
        }
             

    }

}
