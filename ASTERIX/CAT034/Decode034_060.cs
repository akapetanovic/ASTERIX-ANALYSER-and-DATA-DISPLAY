using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class Decode034_060
    {
        public static void Decode(byte[] Data)
        { 
            // 7 I034/060 System Processing Mode 1+              
            if (CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("060")].IsPresent == false)
            {
                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("060")].value = "N/A";
            }
            else
            {

                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("060")].value = "rr";
            } 
           
        }
    }
}
