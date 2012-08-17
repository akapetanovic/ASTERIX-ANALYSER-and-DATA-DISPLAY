using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticastingUDP
{
    class Decode034_030
    {
        public static void Decode(byte[] Data)
        {

            // 3 I034/030 Time-of-Day  3
            if (CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("030")].IsPresent == false)
            {
                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("030")].value = "N/A";
            }
            else
            {

                
                
                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("030")].value = "rr";
            }
           
        }
    }
}
