using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT01I200UserData
    {
        public static void DecodeCAT01I200(byte[] Data)
        {
            
            CAT01.CurrentDataBufferOctalIndex = CAT01.CurrentDataBufferOctalIndex + 4;
        }
    }
}
