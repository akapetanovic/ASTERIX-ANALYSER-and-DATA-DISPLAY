﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticastingUDP
{
    class CAT01I060UserData
    {
        public static void DecodeCAT01I060(byte[] Data)
        {
           
            // Leave it at the current index for the next decode
            CAT01.CurrentDataBufferOctalIndex = CAT01.CurrentDataBufferOctalIndex + 2;
        }
    }
}
