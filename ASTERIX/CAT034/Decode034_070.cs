using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class Decode034_070
    {
        public static void Decode(byte[] Data)
        {
            // 8 I034/070 Message Count Values 1+2N
            if (CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("070")].IsPresent == false)
            {
                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("070")].value = "N/A";
            }
            else
            {

                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("070")].value = "rr";
            }
        }
    }
}
