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
using System.Collections.Generic;

namespace Google.KML
{
    /// <summary>
    /// Represents the Container abstract and inherits the Feature abstract
    /// </summary>
    public abstract class geContainer: geFeature
    {
        /// <summary>
        /// A collection of Features that can exist within the container
        /// </summary>
        public List<geFeature> Features = new List<geFeature>();

        /// <summary>
        /// Renders the object as KML, and calls upon any children to do the same
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            base.ToKML(kml);
            foreach (geFeature Feature in Features)
            {
                Feature.ToKML(kml);
            }
           
        }
    }
}
