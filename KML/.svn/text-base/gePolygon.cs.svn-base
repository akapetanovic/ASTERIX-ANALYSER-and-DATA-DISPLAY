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
    /// A Polygon is defined by an outer boundary and 0 or more inner boundaries.
    /// The boundaries, in turn, are defined by LinearRings. When a Polygon is 
    /// extruded, its boundaries are connected to the ground to form additional 
    /// polygons, which gives the appearance of a building. When a Polygon is 
    /// extruded, each point is extruded individually. Extruded Polygons use <PolyStyle> 
    /// for their color, color mode, and fill. 
    /// </summary>
    public class gePolygon : geGeometry
    {
        public bool Extrude = false;
        public bool Tessellate = false;
        public geAltitudeModeEnum AltitudeMode = geAltitudeModeEnum.clampToGround;
        public List<geInnerBoundaryIs> InnerBoundaries = new List<geInnerBoundaryIs>();
        public geOuterBoundaryIs OuterBoundaryIs;


        public gePolygon(geOuterBoundaryIs outerBoundaryIs) 
        {
            OuterBoundaryIs = outerBoundaryIs;
        }
        
       /// <summary>
       /// Render to KML
       /// </summary>
       /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {

            kml.WriteStartElement("Polygon");
            if ((ID != null) && (ID.Length > 0 ))
                kml.WriteAttributeString("id", ID);
            if (Extrude)
                kml.WriteElementString("extrude", Convert.ToInt16(Extrude).ToString());
            if (Tessellate)
                kml.WriteElementString("tessellate", Convert.ToInt16(Tessellate).ToString());
            if (AltitudeMode != geAltitudeModeEnum.clampToGround)
                kml.WriteElementString("altitudeMode", AltitudeMode.ToString());
            OuterBoundaryIs.ToKML(kml);

            foreach (geInnerBoundaryIs inner in InnerBoundaries)
            {
                inner.ToKML(kml);
            }
                       
            kml.WriteEndElement();

        }


    }

}
