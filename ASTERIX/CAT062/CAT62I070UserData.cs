using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I070UserData
    {
        private const double FIX_POINT_1 = 1.0 / 128.0;
        private const double FIX_POINT_2 = FIX_POINT_1 * 2.0;
        private const double FIX_POINT_3 = FIX_POINT_2 * 2.0;
        private const double FIX_POINT_4 = FIX_POINT_3 * 2.0;
        private const double FIX_POINT_5 = FIX_POINT_4 * 2.0;
        private const double FIX_POINT_6 = FIX_POINT_5 * 2.0;
        private const double FIX_POINT_7 = FIX_POINT_6 * 2.0;
        private const double FIX_POINT_8 = FIX_POINT_7 * 2.0;
        private const double FIX_POINT_9 = FIX_POINT_8 * 2.0;
        private const double FIX_POINT_10 = FIX_POINT_9 * 2.0;
        private const double FIX_POINT_11 = FIX_POINT_10 * 2.0;
        private const double FIX_POINT_12 = FIX_POINT_11 * 2.0;
        private const double FIX_POINT_13 = FIX_POINT_12 * 2.0;
        private const double FIX_POINT_14 = FIX_POINT_13 * 2.0;
        private const double FIX_POINT_15 = FIX_POINT_14 * 2.0;
        private const double FIX_POINT_16 = FIX_POINT_15 * 2.0;

        private const double FIX_POINT_17 = FIX_POINT_16 * 2.0;
        private const double FIX_POINT_18 = FIX_POINT_17 * 2.0;
        private const double FIX_POINT_19 = FIX_POINT_18 * 2.0;
        private const double FIX_POINT_20 = FIX_POINT_19 * 2.0;
        private const double FIX_POINT_21 = FIX_POINT_20 * 2.0;
        private const double FIX_POINT_22 = FIX_POINT_21 * 2.0;
        private const double FIX_POINT_23 = FIX_POINT_22 * 2.0;
        private const double FIX_POINT_24 = FIX_POINT_23 * 2.0;

        public static void DecodeCAT62I070(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 2];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];
            BO.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex ];

            CAT62I070Types.CAT62070ElapsedTimeSinceMidnight Result = new CAT62I070Types.CAT62070ElapsedTimeSinceMidnight();
            
            if (BO.DWord[Bit_Ops.Bit0] == true)
                Result.ElapsedTimeSinceMidnight = FIX_POINT_1;
            if (BO.DWord[Bit_Ops.Bit1] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_2;
            if (BO.DWord[Bit_Ops.Bit2] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_3;
            if (BO.DWord[Bit_Ops.Bit3] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_4;
            if (BO.DWord[Bit_Ops.Bit4] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_5;
            if (BO.DWord[Bit_Ops.Bit5] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_6;
            if (BO.DWord[Bit_Ops.Bit6] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_7;
            if (BO.DWord[Bit_Ops.Bit7] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_8;
            if (BO.DWord[Bit_Ops.Bit8] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_9;
            if (BO.DWord[Bit_Ops.Bit9] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_10;
            if (BO.DWord[Bit_Ops.Bit10] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_11;
            if (BO.DWord[Bit_Ops.Bit11] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_12;
            if (BO.DWord[Bit_Ops.Bit12] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_13;
            if (BO.DWord[Bit_Ops.Bit13] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_14;
            if (BO.DWord[Bit_Ops.Bit14] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_15;
            if (BO.DWord[Bit_Ops.Bit15] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_16;
            if (BO.DWord[Bit_Ops.Bit16] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_17;
            if (BO.DWord[Bit_Ops.Bit17] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_18;
            if (BO.DWord[Bit_Ops.Bit18] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_19;
            if (BO.DWord[Bit_Ops.Bit19] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_20;
            if (BO.DWord[Bit_Ops.Bit20] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_21;
            if (BO.DWord[Bit_Ops.Bit21] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_22;
            if (BO.DWord[Bit_Ops.Bit22] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_23;
            if (BO.DWord[Bit_Ops.Bit23] == true)
                Result.ElapsedTimeSinceMidnight = Result.ElapsedTimeSinceMidnight + FIX_POINT_24;

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT62.I062DataItems[CAT62.ItemIDToIndex("070")].value = Result;
            //////////////////////////////////////////////////////////////////////////////////

            // Increase data buffer index so it ready for the next data item.
            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 3;
        }
    }
}
