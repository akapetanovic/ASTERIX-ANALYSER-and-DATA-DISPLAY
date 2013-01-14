using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT01I200UserData
    {
        /////////////////////////////////////////////////////////////////
        /// Define calculated GSPD LSB to MSB
        /////////////////////////////////////////////////////////////////
        private const double GSPD_1 = 0.22; // (2** (-14))
        private const double GSPD_2 = GSPD_1 * 2.0;
        private const double GSPD_3 = GSPD_2 * 2.0;
        private const double GSPD_4 = GSPD_3 * 2.0;
        private const double GSPD_5 = GSPD_4 * 2.0;
        private const double GSPD_6 = GSPD_5 * 2.0;
        private const double GSPD_7 = GSPD_6 * 2.0;
        private const double GSPD_8 = GSPD_7 * 2.0;
        private const double GSPD_9 = GSPD_8 * 2.0;
        private const double GSPD_10 = GSPD_9 * 2.0;
        private const double GSPD_11 = GSPD_10 * 2.0;
        private const double GSPD_12 = GSPD_11 * 2.0;
        private const double GSPD_13 = GSPD_12 * 2.0;
        private const double GSPD_14 = GSPD_13 * 2.0;
        private const double GSPD_15 = GSPD_14 * 2.0;
        private const double GSPD_16 = GSPD_15 * 2.0;

        /////////////////////////////////////////////////////////////////
        /// Define calculated HDG LSB to MSB
        /////////////////////////////////////////////////////////////////
        private const double HDG_1 = 360.0 / 65536.0; // 2 ** 16 = 65536
        private const double HDG_2 = HDG_1 * 2.0;
        private const double HDG_3 = HDG_2 * 2.0;
        private const double HDG_4 = HDG_3 * 2.0;
        private const double HDG_5 = HDG_4 * 2.0;
        private const double HDG_6 = HDG_5 * 2.0;
        private const double HDG_7 = HDG_6 * 2.0;
        private const double HDG_8 = HDG_7 * 2.0;
        private const double HDG_9 = HDG_8 * 2.0;
        private const double HDG_10 = HDG_9 * 2.0;
        private const double HDG_11 = HDG_10 * 2.0;
        private const double HDG_12 = HDG_11 * 2.0;
        private const double HDG_13 = HDG_12 * 2.0;
        private const double HDG_14 = HDG_13 * 2.0;
        private const double HDG_15 = HDG_14 * 2.0;
        private const double HDG_16 = HDG_15 * 2.0;

        public static void DecodeCAT01I200(byte[] Data)
        {

            CAT01I200Types.CalculatedGSPandHDG_Type CalculatedGSPandHDG = new CAT01I200Types.CalculatedGSPandHDG_Type();

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 4];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 3];
            BO.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 2];
            BO.DWord[Bit_Ops.Bits24_31_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 1];

            CalculatedGSPandHDG.Is_Valid = true;
            
            ///////////////////////////////////////////////////////////////////////////////////////
            // Decode GSPD
            ///////////////////////////////////////////////////////////////////////////////////////
            if (BO.DWord[Bit_Ops.Bit16] == true)
                CalculatedGSPandHDG.GSPD = GSPD_1;
            if (BO.DWord[Bit_Ops.Bit17] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_2;
            if (BO.DWord[Bit_Ops.Bit18] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_3;
            if (BO.DWord[Bit_Ops.Bit19] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_4;
            if (BO.DWord[Bit_Ops.Bit20] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_5;
            if (BO.DWord[Bit_Ops.Bit21] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_6;
            if (BO.DWord[Bit_Ops.Bit22] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_7;
            if (BO.DWord[Bit_Ops.Bit23] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_8;
            if (BO.DWord[Bit_Ops.Bit24] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_9;
            if (BO.DWord[Bit_Ops.Bit25] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_10;
            if (BO.DWord[Bit_Ops.Bit26] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_11;
            if (BO.DWord[Bit_Ops.Bit27] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_12;
            if (BO.DWord[Bit_Ops.Bit28] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_13;
            if (BO.DWord[Bit_Ops.Bit29] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_14;
            if (BO.DWord[Bit_Ops.Bit30] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_15;
            if (BO.DWord[Bit_Ops.Bit31] == true)
                CalculatedGSPandHDG.GSPD = CalculatedGSPandHDG.GSPD + GSPD_16;

            ///////////////////////////////////////////////////////////////////////////////////////
            // Decode HDG
            ///////////////////////////////////////////////////////////////////////////////////////
            if (BO.DWord[Bit_Ops.Bit0] == true)
                CalculatedGSPandHDG.HDG = HDG_1;
            if (BO.DWord[Bit_Ops.Bit1] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_2;
            if (BO.DWord[Bit_Ops.Bit2] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_3;
            if (BO.DWord[Bit_Ops.Bit3] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_4;
            if (BO.DWord[Bit_Ops.Bit4] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_5;
            if (BO.DWord[Bit_Ops.Bit5] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_6;
            if (BO.DWord[Bit_Ops.Bit6] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_7;
            if (BO.DWord[Bit_Ops.Bit7] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_8;
            if (BO.DWord[Bit_Ops.Bit8] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_9;
            if (BO.DWord[Bit_Ops.Bit9] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_10;
            if (BO.DWord[Bit_Ops.Bit10] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_11;
            if (BO.DWord[Bit_Ops.Bit11] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_12;
            if (BO.DWord[Bit_Ops.Bit12] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_13;
            if (BO.DWord[Bit_Ops.Bit13] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_14;
            if (BO.DWord[Bit_Ops.Bit14] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_15;
            if (BO.DWord[Bit_Ops.Bit15] == true)
                CalculatedGSPandHDG.HDG = CalculatedGSPandHDG.HDG + HDG_16;

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT01.I001DataItems[CAT01.ItemIDToIndex("200")].value = CalculatedGSPandHDG;
            //////////////////////////////////////////////////////////////////////////////////

            CAT01.CurrentDataBufferOctalIndex = CAT01.CurrentDataBufferOctalIndex + 4;
        }
    }
}
