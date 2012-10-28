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
    /// A Style defines an addressable style group that can be referenced by StyleMaps 
    /// and Features. Styles affect how Geometry is presented in the 3D viewer and 
    /// how Features appear in the Places panel of the List view. Shared styles are 
    /// collected in a <Document> and must have an id defined for them so that they 
    /// can be referenced by the individual Features that use them. 
    /// </summary>
    public class geStyle : geStyleSelector
    {
        public geIconStyle IconStyle;
        public geLabelStyle LabelStyle;
        public geLineStyle LineStyle;
        public gePolyStyle PolyStyle;
        public geBaloonStyle BaloonStyle;
        public geListStyle ListStyle;

        public geStyle(string id)
        {
            ID = id;
        }

        public override void ToKML(XmlTextWriter kml)
        {
            kml.WriteStartElement("Style");
            kml.WriteAttributeString("id", ID);

            if (IconStyle != null)
                IconStyle.ToKML(kml);

            if (LabelStyle != null)
                LabelStyle.ToKML(kml);

            if (LineStyle != null)
                LineStyle.ToKML(kml);

            if (PolyStyle != null)
                PolyStyle.ToKML(kml);

            if (BaloonStyle != null)
                BaloonStyle.ToKML(kml);

            if (ListStyle != null)
                ListStyle.ToKML(kml);
            kml.WriteEndElement();
            
        }
    }
}