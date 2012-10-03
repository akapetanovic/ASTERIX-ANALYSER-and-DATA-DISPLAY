using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT02I070UserData
    {
        public static void DecodeCAT02I070(byte[] Data)
        {
            CAT02.CurrentDataBufferOctalIndex = CAT02.CurrentDataBufferOctalIndex + 3;
        }
    }
}
