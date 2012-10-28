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
using System.Drawing;

namespace Google.KML
{
    /// <summary>
    /// This element draws an image overlay fixed to the screen. Sample uses for
    /// ScreenOverlays are compasses, logos, and heads-up displays. ScreenOverlay
    /// sizing is determined by the <size> element. Positioning of the overlay is
    /// handled by mapping a point in the image specified by <overlayXY> to a point
    /// on the screen specified by <screenXY>. Then the image is rotated by 
    /// <rotation> degrees about a point relative to the screen specified by 
    /// <rotationXY>. 
    /// </summary>
    public class geScreenOverlay : geOverlay
    {
        private Single Rotation = 0;
        public geVec2 OverlayXY;
        public geVec2 ScreenXY;
        public geVec2 RotationXY;
        public geVec2 Size;

        public geScreenOverlay()
        {
            SysColor.SysColor = Color.White;
            
        }

        public override void ToKML(XmlTextWriter kml)
        {
            kml.WriteStartElement("ScreenOverlay");
            if (ID != null) 
                kml.WriteAttributeString("id", this.ID);
            base.ToKML(kml);
            
            kml.WriteStartElement("overlayXY");
            kml.WriteAttributeString("x", OverlayXY.x.ToString());
            kml.WriteAttributeString("y", OverlayXY.y.ToString());
            kml.WriteAttributeString("xunits", OverlayXY.xunits.ToString());
            kml.WriteAttributeString("yunits", OverlayXY.yunits.ToString());
            kml.WriteEndElement();

            kml.WriteStartElement("screenXY");
            kml.WriteAttributeString("x", ScreenXY.x.ToString());
            kml.WriteAttributeString("y", ScreenXY.y.ToString());
            kml.WriteAttributeString("xunits", ScreenXY.xunits.ToString());
            kml.WriteAttributeString("yunits", ScreenXY.yunits.ToString());
            kml.WriteEndElement();

            kml.WriteStartElement("rotationXY");
            kml.WriteAttributeString("x", RotationXY.x.ToString());
            kml.WriteAttributeString("y", RotationXY.y.ToString());
            kml.WriteAttributeString("xunits", RotationXY.xunits.ToString());
            kml.WriteAttributeString("yunits", RotationXY.yunits.ToString());
            kml.WriteEndElement();

            kml.WriteStartElement("size");
            kml.WriteAttributeString("x", Size.x.ToString());
            kml.WriteAttributeString("y", Size.y.ToString());
            kml.WriteAttributeString("xunits", Size.xunits.ToString());
            kml.WriteAttributeString("yunits", Size.yunits.ToString());
            kml.WriteEndElement();

            kml.WriteElementString("rotation", Rotation.ToString());

            kml.WriteEndElement();
            
        }
    }
}
