using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT48I030UserData
    {
        public static void DecodeCAT48I030(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops B0 = new Bit_Ops();

            //Extract the first octet
            B0.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 1];

            if (B0.DWord[CAT48I030Types.Word1_FX_Index] == true)
            {
                // Increase data buffer index so it ready for the next data item.
                CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + 1;

                //Extract the first octet
                B0.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 1];
                if (B0.DWord[CAT48I030Types.Word1_FX_Index] == true)
                    // Increase data buffer index so it ready for the next data item.
                    CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + 1;
            }

            // Increase data buffer index so it ready for the next data item.
            CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + 1;
        }
    }
}
