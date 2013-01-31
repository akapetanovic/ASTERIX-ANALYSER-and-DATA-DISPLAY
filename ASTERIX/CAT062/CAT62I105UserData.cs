using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I105UserData
    {

        /////////////////////////////////////////////////////////////////
        /// Define lat/long fix point values from LSB to MSB
        /////////////////////////////////////////////////////////////////
        
       // private const double POS_FIX_1 = 180.0 / Math.Pow(2.0, 25.0);

        private const double POS_FIX_1 = 180.0 / 33554432.0;
        private const double POS_FIX_2 = POS_FIX_1 * 2.0;
        private const double POS_FIX_3 = POS_FIX_2 * 2.0;
        private const double POS_FIX_4 = POS_FIX_3 * 2.0;
        private const double POS_FIX_5 = POS_FIX_4 * 2.0;
        private const double POS_FIX_6 = POS_FIX_5 * 2.0;
        private const double POS_FIX_7 = POS_FIX_6 * 2.0;
        private const double POS_FIX_8 = POS_FIX_7 * 2.0;
        private const double POS_FIX_9 = POS_FIX_8 * 2.0;
        private const double POS_FIX_10 = POS_FIX_9 * 2.0;
        private const double POS_FIX_11 = POS_FIX_10 * 2.0;
        private const double POS_FIX_12 = POS_FIX_11 * 2.0;
        private const double POS_FIX_13 = POS_FIX_12 * 2.0;
        private const double POS_FIX_14 = POS_FIX_13 * 2.0;
        private const double POS_FIX_15 = POS_FIX_14 * 2.0;
        private const double POS_FIX_16 = POS_FIX_15 * 2.0;

        private const double POS_FIX_17 = POS_FIX_16 * 2.0;
        private const double POS_FIX_18 = POS_FIX_17 * 2.0;
        private const double POS_FIX_19 = POS_FIX_18 * 2.0;
        private const double POS_FIX_20 = POS_FIX_19 * 2.0;
        private const double POS_FIX_21 = POS_FIX_20 * 2.0;
        private const double POS_FIX_22 = POS_FIX_21 * 2.0;
        private const double POS_FIX_23 = POS_FIX_22 * 2.0;
        private const double POS_FIX_24 = POS_FIX_23 * 2.0;
        // 45
        private const double POS_FIX_25 = POS_FIX_24 * 2.0;
        private const double POS_FIX_26 = POS_FIX_25 * 2.0;
        // 180
        private const double POS_FIX_27 = POS_FIX_26 * 2.0;
        private const double POS_FIX_28 = POS_FIX_27 * 2.0;
        private const double POS_FIX_29 = POS_FIX_28 * 2.0;
        private const double POS_FIX_30 = POS_FIX_29 * 2.0;
        private const double POS_FIX_31 = POS_FIX_30 * 2.0;
        
        public static void DecodeCAT62I105(byte[] Data)
        {

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 3];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 2];
            BO.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];
            BO.DWord[Bit_Ops.Bits24_31_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

            double Result = 0.0;
            double Latitude = 0.0;
            double Longitude = 0.0;
            
            // Check for negative values
            if (BO.DWord[Bit_Ops.Bit31] == true)
            {
                BO.DWord[Bit_Ops.Bit0] = !BO.DWord[Bit_Ops.Bit0];
                BO.DWord[Bit_Ops.Bit1] = !BO.DWord[Bit_Ops.Bit1];
                BO.DWord[Bit_Ops.Bit2] = !BO.DWord[Bit_Ops.Bit2];
                BO.DWord[Bit_Ops.Bit3] = !BO.DWord[Bit_Ops.Bit3];
                BO.DWord[Bit_Ops.Bit4] = !BO.DWord[Bit_Ops.Bit4];
                BO.DWord[Bit_Ops.Bit5] = !BO.DWord[Bit_Ops.Bit5];
                BO.DWord[Bit_Ops.Bit6] = !BO.DWord[Bit_Ops.Bit6];
                BO.DWord[Bit_Ops.Bit7] = !BO.DWord[Bit_Ops.Bit7];
                BO.DWord[Bit_Ops.Bit8] = !BO.DWord[Bit_Ops.Bit8];
                BO.DWord[Bit_Ops.Bit9] = !BO.DWord[Bit_Ops.Bit9];
                BO.DWord[Bit_Ops.Bit10] = !BO.DWord[Bit_Ops.Bit10];
                BO.DWord[Bit_Ops.Bit11] = !BO.DWord[Bit_Ops.Bit11];
                BO.DWord[Bit_Ops.Bit12] = !BO.DWord[Bit_Ops.Bit12];
                BO.DWord[Bit_Ops.Bit13] = !BO.DWord[Bit_Ops.Bit13];
                BO.DWord[Bit_Ops.Bit14] = !BO.DWord[Bit_Ops.Bit14];
                BO.DWord[Bit_Ops.Bit15] = !BO.DWord[Bit_Ops.Bit15];
                BO.DWord[Bit_Ops.Bit16] = !BO.DWord[Bit_Ops.Bit16];
                BO.DWord[Bit_Ops.Bit17] = !BO.DWord[Bit_Ops.Bit17];
                BO.DWord[Bit_Ops.Bit18] = !BO.DWord[Bit_Ops.Bit18];
                BO.DWord[Bit_Ops.Bit19] = !BO.DWord[Bit_Ops.Bit19];
                BO.DWord[Bit_Ops.Bit20] = !BO.DWord[Bit_Ops.Bit20];
                BO.DWord[Bit_Ops.Bit21] = !BO.DWord[Bit_Ops.Bit21];
                BO.DWord[Bit_Ops.Bit22] = !BO.DWord[Bit_Ops.Bit22];
                BO.DWord[Bit_Ops.Bit23] = !BO.DWord[Bit_Ops.Bit23];
                BO.DWord[Bit_Ops.Bit24] = !BO.DWord[Bit_Ops.Bit24];
                BO.DWord[Bit_Ops.Bit25] = !BO.DWord[Bit_Ops.Bit25];
                BO.DWord[Bit_Ops.Bit26] = !BO.DWord[Bit_Ops.Bit26];
                BO.DWord[Bit_Ops.Bit27] = !BO.DWord[Bit_Ops.Bit27];
                BO.DWord[Bit_Ops.Bit28] = !BO.DWord[Bit_Ops.Bit28];
                BO.DWord[Bit_Ops.Bit29] = !BO.DWord[Bit_Ops.Bit29];
                BO.DWord[Bit_Ops.Bit30] = !BO.DWord[Bit_Ops.Bit30];
                BO.DWord[Bit_Ops.Bit31] = !BO.DWord[Bit_Ops.Bit31];
                BO.DWord[Bit_Ops.Bits16_31_Of_DWord] = BO.DWord[Bit_Ops.Bits16_31_Of_DWord] + 1;

                if (BO.DWord[Bit_Ops.Bit0] == true)
                    Result = POS_FIX_1;
                if (BO.DWord[Bit_Ops.Bit1] == true)
                    Result = Result + POS_FIX_2;
                if (BO.DWord[Bit_Ops.Bit2] == true)
                    Result = Result + POS_FIX_3;
                if (BO.DWord[Bit_Ops.Bit3] == true)
                    Result = Result + POS_FIX_4;
                if (BO.DWord[Bit_Ops.Bit4] == true)
                    Result = Result + POS_FIX_5;
                if (BO.DWord[Bit_Ops.Bit5] == true)
                    Result = Result + POS_FIX_6;
                if (BO.DWord[Bit_Ops.Bit6] == true)
                    Result = Result + POS_FIX_7;
                if (BO.DWord[Bit_Ops.Bit7] == true)
                    Result = Result + POS_FIX_8;
                if (BO.DWord[Bit_Ops.Bit8] == true)
                    Result = Result + POS_FIX_9;
                if (BO.DWord[Bit_Ops.Bit9] == true)
                    Result = Result + POS_FIX_10;
                if (BO.DWord[Bit_Ops.Bit10] == true)
                    Result = Result + POS_FIX_11;
                if (BO.DWord[Bit_Ops.Bit11] == true)
                    Result = Result + POS_FIX_12;
                if (BO.DWord[Bit_Ops.Bit12] == true)
                    Result = Result + POS_FIX_13;
                if (BO.DWord[Bit_Ops.Bit13] == true)
                    Result = Result + POS_FIX_14;
                if (BO.DWord[Bit_Ops.Bit14] == true)
                    Result = Result + POS_FIX_15;
                if (BO.DWord[Bit_Ops.Bit15] == true)
                    Result = Result + POS_FIX_16;
                if (BO.DWord[Bit_Ops.Bit16] == true)
                    Result = Result + POS_FIX_17;
                if (BO.DWord[Bit_Ops.Bit17] == true)
                    Result = Result + POS_FIX_18;
                if (BO.DWord[Bit_Ops.Bit18] == true)
                    Result = Result + POS_FIX_19;
                if (BO.DWord[Bit_Ops.Bit19] == true)
                    Result = Result + POS_FIX_20;
                if (BO.DWord[Bit_Ops.Bit20] == true)
                    Result = Result + POS_FIX_21;
                if (BO.DWord[Bit_Ops.Bit21] == true)
                    Result = Result + POS_FIX_22;
                if (BO.DWord[Bit_Ops.Bit22] == true)
                    Result = Result + POS_FIX_23;
                if (BO.DWord[Bit_Ops.Bit23] == true)
                    Result = Result + POS_FIX_24;
                if (BO.DWord[Bit_Ops.Bit24] == true)
                    Result = Result + POS_FIX_25;
                if (BO.DWord[Bit_Ops.Bit25] == true)
                    Result = Result + POS_FIX_26;
                if (BO.DWord[Bit_Ops.Bit26] == true)
                    Result = Result + POS_FIX_27;
                if (BO.DWord[Bit_Ops.Bit27] == true)
                    Result = Result + POS_FIX_28;

                Latitude = -Result;

            }
            else
            {
                ///////////////////////////////////////////////////////////////////////////////////////
                if (BO.DWord[Bit_Ops.Bit0] == true)
                    Result = POS_FIX_1;
                if (BO.DWord[Bit_Ops.Bit1] == true)
                    Result = Result + POS_FIX_2;
                if (BO.DWord[Bit_Ops.Bit2] == true)
                    Result = Result + POS_FIX_3;
                if (BO.DWord[Bit_Ops.Bit3] == true)
                    Result = Result + POS_FIX_4;
                if (BO.DWord[Bit_Ops.Bit4] == true)
                    Result = Result + POS_FIX_5;
                if (BO.DWord[Bit_Ops.Bit5] == true)
                    Result = Result + POS_FIX_6;
                if (BO.DWord[Bit_Ops.Bit6] == true)
                    Result = Result + POS_FIX_7;
                if (BO.DWord[Bit_Ops.Bit7] == true)
                    Result = Result + POS_FIX_8;
                if (BO.DWord[Bit_Ops.Bit8] == true)
                    Result = Result + POS_FIX_9;
                if (BO.DWord[Bit_Ops.Bit9] == true)
                    Result = Result + POS_FIX_10;
                if (BO.DWord[Bit_Ops.Bit10] == true)
                    Result = Result + POS_FIX_11;
                if (BO.DWord[Bit_Ops.Bit11] == true)
                    Result = Result + POS_FIX_12;
                if (BO.DWord[Bit_Ops.Bit12] == true)
                    Result = Result + POS_FIX_13;
                if (BO.DWord[Bit_Ops.Bit13] == true)
                    Result = Result + POS_FIX_14;
                if (BO.DWord[Bit_Ops.Bit14] == true)
                    Result = Result + POS_FIX_15;
                if (BO.DWord[Bit_Ops.Bit15] == true)
                    Result = Result + POS_FIX_16;
                if (BO.DWord[Bit_Ops.Bit16] == true)
                    Result = Result + POS_FIX_17;
                if (BO.DWord[Bit_Ops.Bit17] == true)
                    Result = Result + POS_FIX_18;
                if (BO.DWord[Bit_Ops.Bit18] == true)
                    Result = Result + POS_FIX_19;
                if (BO.DWord[Bit_Ops.Bit19] == true)
                    Result = Result + POS_FIX_20;
                if (BO.DWord[Bit_Ops.Bit20] == true)
                    Result = Result + POS_FIX_21;
                if (BO.DWord[Bit_Ops.Bit21] == true)
                    Result = Result + POS_FIX_22;
                if (BO.DWord[Bit_Ops.Bit22] == true)
                    Result = Result + POS_FIX_23;
                if (BO.DWord[Bit_Ops.Bit23] == true)
                    Result = Result + POS_FIX_24;
                if (BO.DWord[Bit_Ops.Bit24] == true)
                    Result = Result + POS_FIX_25;
                if (BO.DWord[Bit_Ops.Bit25] == true)
                    Result = Result + POS_FIX_26;
                if (BO.DWord[Bit_Ops.Bit26] == true)
                    Result = Result + POS_FIX_27;
                if (BO.DWord[Bit_Ops.Bit27] == true)
                    Result = Result + POS_FIX_28;

                Latitude = Result;
            }

            Result = 0.0;

            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 7];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 6];
            BO.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 5];
            BO.DWord[Bit_Ops.Bits24_31_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 4];

             // Check for negative values
            if (BO.DWord[Bit_Ops.Bit31] == true)
            {
                BO.DWord[Bit_Ops.Bit0] = !BO.DWord[Bit_Ops.Bit0];
                BO.DWord[Bit_Ops.Bit1] = !BO.DWord[Bit_Ops.Bit1];
                BO.DWord[Bit_Ops.Bit2] = !BO.DWord[Bit_Ops.Bit2];
                BO.DWord[Bit_Ops.Bit3] = !BO.DWord[Bit_Ops.Bit3];
                BO.DWord[Bit_Ops.Bit4] = !BO.DWord[Bit_Ops.Bit4];
                BO.DWord[Bit_Ops.Bit5] = !BO.DWord[Bit_Ops.Bit5];
                BO.DWord[Bit_Ops.Bit6] = !BO.DWord[Bit_Ops.Bit6];
                BO.DWord[Bit_Ops.Bit7] = !BO.DWord[Bit_Ops.Bit7];
                BO.DWord[Bit_Ops.Bit8] = !BO.DWord[Bit_Ops.Bit8];
                BO.DWord[Bit_Ops.Bit9] = !BO.DWord[Bit_Ops.Bit9];
                BO.DWord[Bit_Ops.Bit10] = !BO.DWord[Bit_Ops.Bit10];
                BO.DWord[Bit_Ops.Bit11] = !BO.DWord[Bit_Ops.Bit11];
                BO.DWord[Bit_Ops.Bit12] = !BO.DWord[Bit_Ops.Bit12];
                BO.DWord[Bit_Ops.Bit13] = !BO.DWord[Bit_Ops.Bit13];
                BO.DWord[Bit_Ops.Bit14] = !BO.DWord[Bit_Ops.Bit14];
                BO.DWord[Bit_Ops.Bit15] = !BO.DWord[Bit_Ops.Bit15];
                BO.DWord[Bit_Ops.Bit16] = !BO.DWord[Bit_Ops.Bit16];
                BO.DWord[Bit_Ops.Bit17] = !BO.DWord[Bit_Ops.Bit17];
                BO.DWord[Bit_Ops.Bit18] = !BO.DWord[Bit_Ops.Bit18];
                BO.DWord[Bit_Ops.Bit19] = !BO.DWord[Bit_Ops.Bit19];
                BO.DWord[Bit_Ops.Bit20] = !BO.DWord[Bit_Ops.Bit20];
                BO.DWord[Bit_Ops.Bit21] = !BO.DWord[Bit_Ops.Bit21];
                BO.DWord[Bit_Ops.Bit22] = !BO.DWord[Bit_Ops.Bit22];
                BO.DWord[Bit_Ops.Bit23] = !BO.DWord[Bit_Ops.Bit23];
                BO.DWord[Bit_Ops.Bit24] = !BO.DWord[Bit_Ops.Bit24];
                BO.DWord[Bit_Ops.Bit25] = !BO.DWord[Bit_Ops.Bit25];
                BO.DWord[Bit_Ops.Bit26] = !BO.DWord[Bit_Ops.Bit26];
                BO.DWord[Bit_Ops.Bit27] = !BO.DWord[Bit_Ops.Bit27];
                BO.DWord[Bit_Ops.Bit28] = !BO.DWord[Bit_Ops.Bit28];
                BO.DWord[Bit_Ops.Bit29] = !BO.DWord[Bit_Ops.Bit29];
                BO.DWord[Bit_Ops.Bit30] = !BO.DWord[Bit_Ops.Bit30];
                BO.DWord[Bit_Ops.Bit31] = !BO.DWord[Bit_Ops.Bit31];
                BO.DWord[Bit_Ops.Bits16_31_Of_DWord] = BO.DWord[Bit_Ops.Bits16_31_Of_DWord] + 1;

                if (BO.DWord[Bit_Ops.Bit0] == true)
                    Result = POS_FIX_1;
                if (BO.DWord[Bit_Ops.Bit1] == true)
                    Result = Result + POS_FIX_2;
                if (BO.DWord[Bit_Ops.Bit2] == true)
                    Result = Result + POS_FIX_3;
                if (BO.DWord[Bit_Ops.Bit3] == true)
                    Result = Result + POS_FIX_4;
                if (BO.DWord[Bit_Ops.Bit4] == true)
                    Result = Result + POS_FIX_5;
                if (BO.DWord[Bit_Ops.Bit5] == true)
                    Result = Result + POS_FIX_6;
                if (BO.DWord[Bit_Ops.Bit6] == true)
                    Result = Result + POS_FIX_7;
                if (BO.DWord[Bit_Ops.Bit7] == true)
                    Result = Result + POS_FIX_8;
                if (BO.DWord[Bit_Ops.Bit8] == true)
                    Result = Result + POS_FIX_9;
                if (BO.DWord[Bit_Ops.Bit9] == true)
                    Result = Result + POS_FIX_10;
                if (BO.DWord[Bit_Ops.Bit10] == true)
                    Result = Result + POS_FIX_11;
                if (BO.DWord[Bit_Ops.Bit11] == true)
                    Result = Result + POS_FIX_12;
                if (BO.DWord[Bit_Ops.Bit12] == true)
                    Result = Result + POS_FIX_13;
                if (BO.DWord[Bit_Ops.Bit13] == true)
                    Result = Result + POS_FIX_14;
                if (BO.DWord[Bit_Ops.Bit14] == true)
                    Result = Result + POS_FIX_15;
                if (BO.DWord[Bit_Ops.Bit15] == true)
                    Result = Result + POS_FIX_16;
                if (BO.DWord[Bit_Ops.Bit16] == true)
                    Result = Result + POS_FIX_17;
                if (BO.DWord[Bit_Ops.Bit17] == true)
                    Result = Result + POS_FIX_18;
                if (BO.DWord[Bit_Ops.Bit18] == true)
                    Result = Result + POS_FIX_19;
                if (BO.DWord[Bit_Ops.Bit19] == true)
                    Result = Result + POS_FIX_20;
                if (BO.DWord[Bit_Ops.Bit20] == true)
                    Result = Result + POS_FIX_21;
                if (BO.DWord[Bit_Ops.Bit21] == true)
                    Result = Result + POS_FIX_22;
                if (BO.DWord[Bit_Ops.Bit22] == true)
                    Result = Result + POS_FIX_23;
                if (BO.DWord[Bit_Ops.Bit23] == true)
                    Result = Result + POS_FIX_24;
                if (BO.DWord[Bit_Ops.Bit24] == true)
                    Result = Result + POS_FIX_25;
                if (BO.DWord[Bit_Ops.Bit25] == true)
                    Result = Result + POS_FIX_26;
                if (BO.DWord[Bit_Ops.Bit26] == true)
                    Result = Result + POS_FIX_27;
                if (BO.DWord[Bit_Ops.Bit27] == true)
                    Result = Result + POS_FIX_28;

                Longitude = -Result;
            }
            else
            {
                if (BO.DWord[Bit_Ops.Bit0] == true)
                    Result = POS_FIX_1;
                if (BO.DWord[Bit_Ops.Bit1] == true)
                    Result = Result + POS_FIX_2;
                if (BO.DWord[Bit_Ops.Bit2] == true)
                    Result = Result + POS_FIX_3;
                if (BO.DWord[Bit_Ops.Bit3] == true)
                    Result = Result + POS_FIX_4;
                if (BO.DWord[Bit_Ops.Bit4] == true)
                    Result = Result + POS_FIX_5;
                if (BO.DWord[Bit_Ops.Bit5] == true)
                    Result = Result + POS_FIX_6;
                if (BO.DWord[Bit_Ops.Bit6] == true)
                    Result = Result + POS_FIX_7;
                if (BO.DWord[Bit_Ops.Bit7] == true)
                    Result = Result + POS_FIX_8;
                if (BO.DWord[Bit_Ops.Bit8] == true)
                    Result = Result + POS_FIX_9;
                if (BO.DWord[Bit_Ops.Bit9] == true)
                    Result = Result + POS_FIX_10;
                if (BO.DWord[Bit_Ops.Bit10] == true)
                    Result = Result + POS_FIX_11;
                if (BO.DWord[Bit_Ops.Bit11] == true)
                    Result = Result + POS_FIX_12;
                if (BO.DWord[Bit_Ops.Bit12] == true)
                    Result = Result + POS_FIX_13;
                if (BO.DWord[Bit_Ops.Bit13] == true)
                    Result = Result + POS_FIX_14;
                if (BO.DWord[Bit_Ops.Bit14] == true)
                    Result = Result + POS_FIX_15;
                if (BO.DWord[Bit_Ops.Bit15] == true)
                    Result = Result + POS_FIX_16;
                if (BO.DWord[Bit_Ops.Bit16] == true)
                    Result = Result + POS_FIX_17;
                if (BO.DWord[Bit_Ops.Bit17] == true)
                    Result = Result + POS_FIX_18;
                if (BO.DWord[Bit_Ops.Bit18] == true)
                    Result = Result + POS_FIX_19;
                if (BO.DWord[Bit_Ops.Bit19] == true)
                    Result = Result + POS_FIX_20;
                if (BO.DWord[Bit_Ops.Bit20] == true)
                    Result = Result + POS_FIX_21;
                if (BO.DWord[Bit_Ops.Bit21] == true)
                    Result = Result + POS_FIX_22;
                if (BO.DWord[Bit_Ops.Bit22] == true)
                    Result = Result + POS_FIX_23;
                if (BO.DWord[Bit_Ops.Bit23] == true)
                    Result = Result + POS_FIX_24;
                if (BO.DWord[Bit_Ops.Bit24] == true)
                    Result = Result + POS_FIX_25;
                if (BO.DWord[Bit_Ops.Bit25] == true)
                    Result = Result + POS_FIX_26;
                if (BO.DWord[Bit_Ops.Bit26] == true)
                    Result = Result + POS_FIX_27;

                Longitude = Result;
            }

            GeoCordSystemDegMinSecUtilities.LatLongClass LatLong = new GeoCordSystemDegMinSecUtilities.LatLongClass(Latitude, Longitude);

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT62.I062DataItems[CAT62.ItemIDToIndex("105")].value = LatLong;
            //////////////////////////////////////////////////////////////////////////////////

            // Increase data buffer index so it ready for the next data item.
            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 8;
        }
    }
}
