using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT48ISPIUserData
    {
        public static void DecodeCAT48ISPI(byte[] Data)
        {

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            //Extract the first octet
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 1];

            int OctetsToSkip = (int)BO.DWord[Bit_Ops.Bits0_7_Of_DWord]; 
            
            // Bump the index to this octet
            CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + OctetsToSkip;
        }
    }
}
