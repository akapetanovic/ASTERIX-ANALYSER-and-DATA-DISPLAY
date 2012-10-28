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
 * 2007-05-29: Maksim Sestic
 *      * Updated XML tags
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
    /// This is an abstract element and cannot be used directly in a KML file. It provides elements for specifying the color and color mode of extended style types.
    /// </summary>
    public abstract class geColorStyle : geObject
    {
        /// <summary>
        /// Color and opacity (alpha) values are expressed in hexadecimal notation.
        /// </summary>
        /// <remarks>
        /// The range of values for any one color is 0 to 255 (00 to ff). For alpha, 00 is fully transparent and ff is fully opaque.
        /// The order of expression is aabbggrr, where aa=alpha (00 to ff); bb=blue (00 to ff); gg=green (00 to ff); rr=red (00 to ff).
        /// Default value is ffffffff.
        /// </remarks>
        /// <example>
        /// If you want to apply a blue color with 50 percent opacity to an overlay, you would specify the following: <color>7fff0000</color>, where alpha=0x7f, blue=0xff, green=0x00, and red=0x00. 
        /// </example>
        public geColor Color;

        /// <summary>
        /// Values for colorMode are normal (no effect) and random. A value of random applies a random linear scale to the base color as follows.
        /// </summary>
        /// <remarks>
        /// To achieve a truly random selection of colors, specify a base color of white (ffffffff). 
        /// If you specify a single color component (for example, a value of ff0000ff for red), random color values for that one component (red) will be selected. In this case, the values would range from 00 (black) to ff (full red). 
        /// If you specify values for two or for all three color components, a random linear scale is applied to each color component, with results ranging from black to the maximum values specified for each component. 
        /// The opacity of a color comes from the alpha component of color and is never randomized. 
        /// Default value is normal.
        /// </remarks>
        public geColorModeEnum ColorMode = geColorModeEnum.normal;

        /// <summary>
        /// 
        /// </summary>
        public geColorStyle()
        {
            Color.SysColor = System.Drawing.Color.White;
        }

        /// <summary>
        /// Renders the object as KML, and calls upon any children to do the same
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            if (Color.SysColor != System.Drawing.Color.White)
                kml.WriteElementString("color", Color.ToKML());
            if (ColorMode != geColorModeEnum.normal)
                kml.WriteElementString("colorMode", ColorMode.ToString());
           
        }
    }
}
