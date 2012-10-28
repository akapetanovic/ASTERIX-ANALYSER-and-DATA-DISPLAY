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
    /// Converts a .Net DateTime object to a strings that KML
    /// can recognize
    /// </summary>
    public struct geDateTime
    {
        public DateTime dateTime;
        public bool useUTC;
        public bool useDateOnly;


        /// <summary>
        /// Renders the DateTime as a string for KML
        /// </summary>
        /// <returns></returns>
        public string ToKML()
        {
            string TimeZoneString = "Z"; //The string to append if useUTC==true
            if (useDateOnly)
            {
                return dateTime.ToString("yyyy-MM-dd");
            }
            else
            {
                if (!useUTC)
                {
                    //If our time zone is +UTC we need to add a + 
                    TimeSpan diff = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
                    if (diff.TotalMinutes > 0)
                        TimeZoneString = "+";
                    TimeZoneString += diff.Hours.ToString("00") + ":" + diff.Minutes.ToString("00");
                }
                return dateTime.ToString("yyyy-MM-ddThh:mm:ss") + TimeZoneString;
            }
        }
    }
}
