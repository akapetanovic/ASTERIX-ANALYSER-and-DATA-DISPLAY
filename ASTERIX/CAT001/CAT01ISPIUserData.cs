using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT01ISPIUserData
    {
        public static void DecodeCAT01ISPI(byte[] Data)
        {

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            //Extract the first octet
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 1];

            int OctetsToSkip = (int)BO.DWord[Bit_Ops.Bits0_7_Of_DWord]; 
            
            // Bump the index to this octet
            CAT01.CurrentDataBufferOctalIndex = CAT01.CurrentDataBufferOctalIndex + OctetsToSkip;
        }
    }
}
