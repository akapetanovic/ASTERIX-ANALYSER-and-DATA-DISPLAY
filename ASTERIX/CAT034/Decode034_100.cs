using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticastingUDP
{
    class Decode034_100
    {
        public static void Decode(byte[] Data)
        {

            // 9 I034/100 Generic Polar Window 8
            if (CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("100")].IsPresent == false)
            {
                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("100")].value = "N/A";
            }
            else
            {

                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("100")].value = "rr";
            } 
           
        }
    }
}
