using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticastingUDP
{
    class Decode034_050
    {
        public static void Decode(byte[] Data)
        {

            // 6 I034/050 System Configuration and Status  1+
            if (CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("050")].IsPresent == false)
            {
                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("050")].value = "N/A";
            }
            else
            {

                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("050")].value = "rr";
            }
           
        }
    }
}
