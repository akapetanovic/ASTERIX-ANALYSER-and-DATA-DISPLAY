using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I290UserData
    {

        public static void DecodeCAT62I290(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops WORD0 = new Bit_Ops();
            Bit_Ops WORD1 = new Bit_Ops();

            //Extract the first octet
            WORD0.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

            if (WORD0.DWord[CAT62I290Types.WORD0_FX_Extension_Indicator] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                WORD1.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];
            }

            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;

            // 2 SIC/SAC
            if (WORD0.DWord[CAT62I290Types.Track_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            // 7 (7 Char Callsign)
            if (WORD0.DWord[CAT62I290Types.PSR_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            // 4 
            if (WORD0.DWord[CAT62I290Types.SSR_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            // 1
            if (WORD0.DWord[CAT62I290Types.Mode_S_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            // 4
            if (WORD0.DWord[CAT62I290Types.ADS_C_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            // 1
            if (WORD0.DWord[CAT62I290Types.ADS_B_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            // 4
            if (WORD0.DWord[CAT62I290Types.ADS_B_VDL_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            // 4
            if (WORD1.DWord[CAT62I290Types.ADS_B_UAT_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            // 3
            if (WORD1.DWord[CAT62I290Types.Loop_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            // 2
            if (WORD1.DWord[CAT62I290Types.Multilateration_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
        }
    }
}
