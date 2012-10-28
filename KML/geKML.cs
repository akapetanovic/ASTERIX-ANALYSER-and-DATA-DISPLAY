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
 * 2006-12-19: boseefus00001 - Initial release
 * 
 */

using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip;
//using ICSharpCode.SharpZipLib.Checksums;

namespace Google.KML
{

    /// <summary>
    /// This class will be the entry point for creating and 
    /// organizing the KML output
    /// </summary>
    public class geKML
    {
        /// <summary>
        /// We need a root KML object to start with.
        /// The most common would be Features, Containers, or Overlays
        /// </summary>
        public geObject kmlRoot;
        public SortedList<string, byte[]> Files = new SortedList<string,byte[]>();
        
        public geKML(geObject rootKmlObj)
        {
            //Setup how we're goint to write our kml
            kmlRoot = rootKmlObj;

           
        }

        /// <summary>
        /// Return a byte array that is the final KML file.
        /// </summary>
        /// <returns></returns>
        public byte[] ToKML()
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            XmlTextWriter kml = new XmlTextWriter(sw);

            //kml.Namespaces = true;
            kml.Formatting = Formatting.Indented;
            kml.WriteStartDocument();
            kml.WriteStartElement("kml");
            kmlRoot.ToKML(kml);
            kml.WriteEndElement();
            kml.WriteEndDocument();
            kml.Flush();
            kml.Close();
            
            return ms.ToArray();
        }

        /// <summary>
        /// Return the compressed version of the KML file (KMZ)
        /// </summary>
        /// <returns></returns>
        public byte[] ToKMZ()
        {
            MemoryStream ms = new MemoryStream();
            ZipOutputStream zip = new ZipOutputStream(ms);
            ZipEntry entry = new ZipEntry("ge-kml.kml");
            //ZipFile zip = new ZipFile(ms);
            
            byte[] myKml = ToKML();
            zip.SetLevel(9);
            zip.PutNextEntry(entry);
            zip.Write(myKml, 0, myKml.Length);
            zip.CloseEntry();

            //entry = new ZipEntry("images");
            //entry.IsDirectory = true;
            //zip.PutNextEntry(entry);
            //zip.CloseEntry();

            foreach (string file in Files.Keys)
            {
                entry = new ZipEntry(file);
                zip.PutNextEntry(entry);
                zip.Write(Files[file],0,Files[file].Length);
                zip.CloseEntry();
            }

            zip.Close();

            return ms.ToArray();

        }
    



    
    }
}
