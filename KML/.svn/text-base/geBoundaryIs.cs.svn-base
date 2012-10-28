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


namespace Google.KML
{
    /// <summary>
    /// An abstract class that contains a LinearRing, this is not a Google Earth
    /// defined Abstract, but it seemed natural for the InnerBoundaryIs and 
    /// OuterBoundaryIs objects to share this.
    /// </summary>
    public abstract class geBoundaryIs :geObject
    {
        /// <summary>
        /// The closed ring that defines a boundary.  The ring's
        /// first and last point must be the same (closed)
        /// </summary>
        public geLinearRing LinearRing;

        public geBoundaryIs(geLinearRing linearRing)
        {
            LinearRing = linearRing;
        }

        //This class doesn't implement a ToKML()
        //because the linear ring can be either an
        //inner or outer ring, and they will have 
        //different element names.  The inheriting
        //classes will have to do the KML rendering
               
    }
    
}
