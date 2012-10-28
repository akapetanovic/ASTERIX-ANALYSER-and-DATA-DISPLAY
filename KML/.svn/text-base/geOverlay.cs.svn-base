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
    /// An abstract class for images that are drawn on the ground or on the screen
    /// </summary>
    public abstract class geOverlay : geFeature
    {
        public geColor SysColor;
        public int DrawOrder = 0;
        public geIcon Icon;

        /// <summary>
        /// Render to KML
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            base.ToKML(kml);
            kml.WriteElementString("color", SysColor.ToKML());
            if (DrawOrder != 0 )
                kml.WriteElementString("drawOrder", DrawOrder.ToString());

            if (Icon != null)
                Icon.ToKML(kml);
        }
    }
}