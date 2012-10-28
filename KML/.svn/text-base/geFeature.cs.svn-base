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
 * 2007-02-13: boseefus00001
 *      * Added Region property.  Thanks to Steve Hayles for finding that I had left it out.
 * 
 * YYYY-MM-DD: <developer>
 *      *  Modification
 * 
 */



using System;
using System.Collections.Generic;
using System.Xml;

namespace Google.KML
{
    /// <summary>
    /// Represents the Feature abstract, one of the most important
    /// elements in a KML.  This is what will be displayed.
    /// </summary>
    public abstract class geFeature : geObject
    {
        /// <summary>
        /// The name of the feature.  This will be the label that
        /// is displayed in both the Places window and the Map window
        /// </summary>
        public string Name = "";
        /// <summary>
        /// Defines whether to draw the feature in the map 
        /// window by default.
        /// </summary>
        public bool Visibility = true;
        /// <summary>
        /// If the feature contains other features, defines whether
        /// this feature is expanded by default or not.
        /// </summary>
        public bool Open = true;
        /// <summary>
        /// A street address
        /// </summary>
        public string Address = "";
        /// <summary>
        /// A structured xAL street address
        /// </summary>
        public string AddressDetails = "";
        /// <summary>
        /// an RFC2806 formatted phone number
        /// </summary>
        public string PhoneNumber = "";
        /// <summary>
        /// Text that can be displayed under the icon in the 
        /// places window, otherwise any description available
        /// is shown
        /// </summary>
        public string Snippet = "";
        /// <summary>
        /// The text that is displayed in the balloon.  Can be HTML
        /// formatted text.  Special characters should be surrounded
        /// by <![CDATA[ special chars here ]]>
        /// </summary>
        public string Description = "";
        /// <summary>
        /// Where Google Earth should orient itself to look
        /// at this feature
        /// </summary>
        public geLookAt LookAt;
        /// <summary>
        /// A specified time or range of times when this 
        /// feature should be displayed.  Useful for 
        /// showing/hiding features while looping over a 
        /// given time.
        /// </summary>
        public geTimePrimitive TimePrimitive;
        /// <summary>
        /// The style this feature should use, can be relative
        /// to the document if there is one, or an http reference
        /// </summary>
        public string StyleUrl = "";
        /// <summary>
        /// A collection of styles that can be used by different
        /// elements in this feature and child features
        /// </summary>
        public List<geStyleSelector> StyleSelectors = new List<geStyleSelector>();

        /// <summary>
        /// Defines the user view bounding box for when features should be displayed
        /// </summary>
        public geRegion Region;

        /// <summary>
        /// Renders the object as KML, and calls upon any children to do the same
        /// </summary>
        /// <param name="kml"></param>
        public override void ToKML(XmlTextWriter kml)
        {
            if (Name.Length > 0)
                kml.WriteElementString("name", this.Name);
            if (!Visibility)
                kml.WriteElementString("visibility", Convert.ToInt16(this.Visibility).ToString());
            if (!Open)
                kml.WriteElementString("open", Convert.ToInt16(this.Open).ToString());
            if (Address.Length > 0)
                kml.WriteElementString("address", this.Address);
            if (AddressDetails.Length > 0)
                kml.WriteElementString("AddressDetails", this.AddressDetails);
            if (PhoneNumber.Length > 0)
                kml.WriteElementString("phoneNumber", this.PhoneNumber);
            if (Snippet.Length > 0)
                kml.WriteElementString("Snippet", this.Snippet);
            if (Description.Length > 0)
                kml.WriteElementString("description", this.Description);
            if (LookAt != null)
                LookAt.ToKML(kml);
            if (TimePrimitive != null)
                TimePrimitive.ToKML(kml);
            if (StyleUrl.Length > 0)
                kml.WriteElementString("styleUrl", this.StyleUrl);
            foreach (geStyleSelector StyleSelector in StyleSelectors)
            {
                StyleSelector.ToKML(kml);
            }
            if (Region != null)
                Region.ToKML(kml);

        }
    }
}
