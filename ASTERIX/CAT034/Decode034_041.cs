using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class Decode034_041
    {
        public static void Decode(byte[] Data)
        {

            // 5 I034/041 Antenna Rotation Period 2
            if (CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("041")].IsPresent == false)
            {
                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("041")].value = "N/A";
            }
            else
            {

                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("041")].value = "rr";
            }
           
        }
    }
}
