using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT02I041UserData
    {
        ///////////////////////////////////////////////////////////////////////
        // Define antenna rotation period
        // where LSB = 1/128s 
        ///////////////////////////////////////////////////////////////////////
        private const double LSB_1 = 1.0 / 128.0;
        private const double LSB_2 = LSB_1 * 2.0;
        private const double LSB_3 = LSB_2 * 2.0;
        private const double LSB_4 = LSB_3 * 2.0;
        private const double LSB_5 = LSB_4 * 2.0;
        private const double LSB_6 = LSB_5 * 2.0;
        private const double LSB_7 = LSB_6 * 2.0;
        private const double LSB_8 = LSB_7 * 2.0;
        private const double LSB_9 = LSB_8 * 2.0;
        private const double LSB_10 = LSB_9 * 2.0;
        private const double LSB_11 = LSB_10 * 2.0;
        private const double LSB_12 = LSB_11 * 2.0;
        private const double LSB_13 = LSB_12 * 2.0;
        private const double LSB_14 = LSB_13 * 2.0;
        private const double LSB_15 = LSB_14 * 2.0;
        private const double LSB_16 = LSB_15 * 2.0;

        public static void DecodeCAT02I041(byte[] Data)
        {

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            //Extract the first octet
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT02.CurrentDataBufferOctalIndex + 2];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT02.CurrentDataBufferOctalIndex + 1];

             double Result = 0.0;

            if (BO.DWord[Bit_Ops.Bit0] == true)
                Result = LSB_1;
            if (BO.DWord[Bit_Ops.Bit1] == true)
                Result = Result + LSB_2;
            if (BO.DWord[Bit_Ops.Bit2] == true)
                Result = Result + LSB_3;
            if (BO.DWord[Bit_Ops.Bit3] == true)
                Result = Result + LSB_4;
            if (BO.DWord[Bit_Ops.Bit4] == true)
                Result = Result + LSB_5;
            if (BO.DWord[Bit_Ops.Bit5] == true)
                Result = Result + LSB_6;
            if (BO.DWord[Bit_Ops.Bit6] == true)
                Result = Result + LSB_7;
            if (BO.DWord[Bit_Ops.Bit7] == true)
                Result = Result + LSB_8;
            if (BO.DWord[Bit_Ops.Bit8] == true)
                Result = Result + LSB_9;
            if (BO.DWord[Bit_Ops.Bit9] == true)
                Result = Result + LSB_10;
            if (BO.DWord[Bit_Ops.Bit10] == true)
                Result = Result + LSB_11;
            if (BO.DWord[Bit_Ops.Bit11] == true)
                Result = Result + LSB_12;
            if (BO.DWord[Bit_Ops.Bit12] == true)
                Result = Result + LSB_13;
            if (BO.DWord[Bit_Ops.Bit13] == true)
                Result = Result + LSB_14;
            if (BO.DWord[Bit_Ops.Bit14] == true)
                Result = Result + LSB_15;
            if (BO.DWord[Bit_Ops.Bit15] == true)
                Result = Result + LSB_16;

             //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT02.I002DataItems[CAT02.ItemIDToIndex("041")].value = Result;
            //////////////////////////////////////////////////////////////////////////////////

            // Leave it at the current index for the next decode
            CAT02.CurrentDataBufferOctalIndex = CAT02.CurrentDataBufferOctalIndex + 2;

        }

    }
}
