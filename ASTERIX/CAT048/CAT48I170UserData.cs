using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticastingUDP
{
    class CAT48I170UserData
    {
        public static void DecodeCAT48I170(byte[] Data)
        {
            // Increase data buffer index so it ready for the next data item.
            CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + 1; // 1+
        }

    }
}
