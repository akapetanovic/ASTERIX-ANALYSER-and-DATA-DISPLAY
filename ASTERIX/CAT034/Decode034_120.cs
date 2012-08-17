using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticastingUDP
{
    class Decode034_120
    {
        public static void Decode(byte[] Data)
        {

            // 11 I034/120 3D-Position of Data Source 8
            if (CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("120")].IsPresent == false)
            {
                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("120")].value = "N/A";
            }
            else
            {

                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("120")].value = "rr";
            }
           
        }
    }
}
