using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT34I020UserData
    {

        ///////////////////////////////////////////////////////////////////
        // Define antenna azimuth as sector number 
        // where (LSB) = 360°/(2 ** 8) = 1.40625°
        ///////////////////////////////////////////////////////////////////
        private const double LSB_1 = 1.40625;
        private const double LSB_2 = LSB_1 * 2.0;
        private const double LSB_3 = LSB_2 * 2.0;
        private const double LSB_4 = LSB_3 * 2.0;
        private const double LSB_5 = LSB_4 * 2.0;
        private const double LSB_6 = LSB_5 * 2.0;
        private const double LSB_7 = LSB_6 * 2.0;
        private const double LSB_8 = LSB_7 * 2.0;

        public static void DecodeCAT34I020(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();
           
            //Extract the first octet
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + 1];

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

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT34.I034DataItems[CAT34.ItemIDToIndex("020")].value = Result;
            //////////////////////////////////////////////////////////////////////////////////

            // Leave it at the current index for the next decode
            CAT34.CurrentDataBufferOctalIndex = CAT34.CurrentDataBufferOctalIndex + 1;
        }

    }
}
