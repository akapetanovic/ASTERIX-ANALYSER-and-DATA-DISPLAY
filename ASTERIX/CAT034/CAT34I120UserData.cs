using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT34I120UserData
    {
        public static void DecodeCAT34I120(byte[] Data)
        {
            CAT34.CurrentDataBufferOctalIndex = CAT34.CurrentDataBufferOctalIndex + 8;
        }
    }
}
