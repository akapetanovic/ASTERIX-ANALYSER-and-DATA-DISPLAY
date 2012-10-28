
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
 * 2006-02-13: boseefus00001
 *      * Fixed geRegion.geLatLonAltBox.ToKML() so that it includes the altitudeMode element
 * 
 * 2007-02-20: boseefus00001
 *      * Changed all references to geAngleXXX.Value.ToString() to geAngleXXX.ToString().
 *        The geAngleXXX.ToString() Method new overrides it's default behavior and returns an InvariantCulture
 *        string representation of it's Value in order to support other languages.
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
    /// A region contains a bounding box (<LatLonAltBox>) that describes an area 
    /// of interest defined by geographic coordinates and altitudes. In addition,
    /// a Region contains an LOD (level of detail) extent (<Lod>) that defines a
    /// validity range of the associated Region in terms of projected screen size. 
    /// A Region is said to be "active" when the bounding box is within the user's
    /// view and the LOD requirements are met. Objects associated with a Region are 
    /// drawn only when the Region is active. When the <viewRefreshMode> is onRegion, 
    /// the Link or Icon is loaded only when the Region is active.
    /// </summary>
    public class geRegion : geObject
    {
        /// <summary>
        /// A bounding box that describes an area of interest defined by geographic 
        /// coordinates and altitudes
        /// </summary>
        public class geLatLonAltBox : geObject
        {
            public geAngle90 North;
            public geAngle90 South;
            public geAngle180 East;
            public geAngle180 West;
            public Single MinAltitude = 0;
            public Single MaxAltitude = 0;
            public geAltitudeModeEnum AltitudeMode = geAltitudeModeEnum.clampToGround;

            public geLatLonAltBox(geAngle90 north, geAngle90 south, geAngle180 east, geAngle180 west)
            {
                North = north;
                South = south;
                East = east;
                West = west;
            }

            public override void ToKML(XmlTextWriter kml)
            {
                kml.WriteStartElement("LatLonAltBox");
                kml.WriteElementString("north", North.ToString());
                kml.WriteElementString("south", South.ToString());
                kml.WriteElementString("east", East.ToString());
                kml.WriteElementString("west", West.ToString());
                if (MinAltitude != 0)
                    kml.WriteElementString("minAltitude", MinAltitude.ToString());
                if (MaxAltitude !=0 )
                    kml.WriteElementString("maxAltitude", MaxAltitude.ToString());
                kml.WriteEndElement();

                if (AltitudeMode != geAltitudeModeEnum.clampToGround)
                    kml.WriteElementString("altitudeMode", AltitudeMode.ToString());
                
            }
        }

        public class geLod : geObject
        {
            public int MinLodPixels = 0;
            public int MaxLodPixels = -1;
            public int MinFadeExtent = 0;
            public int MaxFadeExtent = 0;

            public override void ToKML(XmlTextWriter kml)
            {
                kml.WriteStartElement("Lod");
                if (MinLodPixels != 0)
                    kml.WriteElementString("minLodPixels", MinLodPixels.ToString());
                if (MaxLodPixels != -1)
                    kml.WriteElementString("maxLodPixels", MaxLodPixels.ToString());
                if (MinFadeExtent != 0)
                    kml.WriteElementString("minFadeExtent", MinFadeExtent.ToString());
                if (MaxFadeExtent != 0)
                    kml.WriteElementString("minFadeExtent", MaxFadeExtent.ToString());
                kml.WriteEndElement();
            }
        }

        public geLatLonAltBox LatLonAltBox;
        public geLod Lod;

        public geRegion(geLatLonAltBox latLonAltBox)
        {
            LatLonAltBox = latLonAltBox;
        }

        public override void ToKML(XmlTextWriter kml)
        {
            kml.WriteStartElement("Region");
            if ((ID != null) && (ID.Length > 0 ))
                kml.WriteAttributeString("id", ID);
            LatLonAltBox.ToKML(kml);

            if (Lod != null)
                Lod.ToKML(kml);

            kml.WriteEndElement();
            
        }

    }
}
