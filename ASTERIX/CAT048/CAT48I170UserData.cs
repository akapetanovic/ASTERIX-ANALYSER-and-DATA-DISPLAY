using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT48I170UserData
    {
        public static void DecodeCAT48I170(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            //Extract the first octet
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 1];

            if (BO.DWord[CAT48I170Types.Word1_FX_Index] == true)
                // Increase data buffer index so it ready for the next data item.
                CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + 1;

            // Increase data buffer index so it ready for the next data item.
            CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + 1;
        }

    }
}
