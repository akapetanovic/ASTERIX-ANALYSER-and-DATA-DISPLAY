using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticastingUDP
{
    class CAT48I250UserData
    {
        public static void DecodeCAT48I250(byte[] Data)
        {
            // The lenght is always 1 + 8 X (repetative factor form the first octet)

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            //Extract the first octet
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 1];

            int Repetative_Factor = (int)BO.DWord[Bit_Ops.Bits0_7_Of_DWord];

            int y = 7;
            if (Repetative_Factor > 5)
            {
                y = 8;
            }
            int g = y;
            
            // Increase data buffer index so it ready for the next data item.
            CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + (1 + 8 * Repetative_Factor);
        }

    }
}
