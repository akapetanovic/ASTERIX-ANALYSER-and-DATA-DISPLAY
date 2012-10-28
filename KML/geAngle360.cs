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
 * 2007-02-20: boseefus00001
 *      * Added a ToString() overriding method that returns an InvariantCulture string representation
 *        of Value in order to support other languages.  For example some cultures will represent 4.563 as 4,563.  
 *        Since the Google API requires certain comma delimited formatting, especially with coordinates,
 *        this method will make sure the double is represented correctly.
 *
 */

using System;

namespace Google.KML
{
    /// <summary>
    /// A double value that must be between -360 and 360
    /// </summary>
    public struct geAngle360
    {
        private double _value;

        public double Value
        {
            get { return _value; }
            set { setValue(value); }
        }

        public geAngle360(double value)
        {
            //This is cheating.  I really should just
            //make this an object
            _value = value;
            setValue(value);
        }

        private void setValue(double newValue)
        {
            if ((newValue >= -360) && (newValue <= 360))
            {
                _value = newValue;
            }
            else
            {
                throw new Exception("Value must be >= -360 and <= 360.");
            }
        }

        public override string ToString()
        {
            return Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
	
    }
}
