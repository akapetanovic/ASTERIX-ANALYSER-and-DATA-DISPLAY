using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticastingUDP
{
    class Decode034_090
    {
        public static void Decode(byte[] Data)
        {

            // 12 I034/090 Collimation Error  2
            if (CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("090")].IsPresent == false)
            {
                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("090")].value = "N/A";
            }
            else
            {

                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("090")].value = "rr";
            }
           
        }
    }
}
