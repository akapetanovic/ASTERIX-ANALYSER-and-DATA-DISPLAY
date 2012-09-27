using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I015UserData
    {

        public static void DecodeCAT62I015(byte[] Data)
        {
           
            //Extract the first octet
            int ServiceNumber = Data[CAT62.CurrentDataBufferOctalIndex];

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT62.I062DataItems[CAT62.ItemIDToIndex("015")].value = ServiceNumber;
            //////////////////////////////////////////////////////////////////////////////////
            
            // Increase data buffer index so it ready for the next data item.
            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
        }
    }
}
