using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT01I141UserData
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

        public static void DecodeCAT01I141(byte[] Data)
        {

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 1];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex ];

            CAT01I141Types.CAT01141ElapsedTimeSinceMidnight Result = new CAT01I141Types.CAT01141ElapsedTimeSinceMidnight();

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

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT01.I001DataItems[CAT01.ItemIDToIndex("141")].value = Result;
            //////////////////////////////////////////////////////////////////////////////////
            // Leave it at the current index for the next decode
            CAT01.CurrentDataBufferOctalIndex = CAT01.CurrentDataBufferOctalIndex + 2;
        }
    }
}
