using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT02I100UserData
    {
        public static void DecodeCAT02I100(byte[] Data)
        {
            CAT02.CurrentDataBufferOctalIndex = CAT02.CurrentDataBufferOctalIndex + 8;
        }
    }
}
