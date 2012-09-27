using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT01I030UserData
    {
        public static void DecodeCAT01I030(byte[] Data)
        {

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            //Extract the first octet
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 1];

            // Bump the index to this octet
            CAT01.CurrentDataBufferOctalIndex = CAT01.CurrentDataBufferOctalIndex + 1;

            // If the field extension is on then bump the index again
            if (BO.DWord[CAT01I020Types.Word1_FX_Index] == true)
                CAT01.CurrentDataBufferOctalIndex = CAT01.CurrentDataBufferOctalIndex + 1;
        }
    }
}
