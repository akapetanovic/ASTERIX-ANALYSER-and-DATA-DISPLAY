using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticastingUDP
{
    class Decode034_110
    {
        public static void Decode(byte[] Data)
        {
            // 10 I034/110 Data Filter 1
            if (CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("110")].IsPresent == false)
            {
                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("110")].value = "N/A";
            }
            else
            {

                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("110")].value = "rr";
            }  
        }
    }
}
