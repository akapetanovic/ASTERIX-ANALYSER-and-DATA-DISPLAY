using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I380UserData
    {

        public static void DecodeCAT62I380(byte[] Data)
        {
            ///////////////////////////////////////////////////////////////
            // Track_Angle and Magnetic Heading DECODE CONSTANTS
            double TA_MA_1 = 360.0 / Math.Pow(2.0, 16.0); // LSB
            double TA_MA_2 = TA_MA_1 * 2.0;
            double TA_MA_3 = TA_MA_2 * 2.0;
            double TA_MA_4 = TA_MA_3 * 2.0;
            double TA_MA_5 = TA_MA_4 * 2.0;
            double TA_MA_6 = TA_MA_5 * 2.0;
            double TA_MA_7 = TA_MA_6 * 2.0;
            double TA_MA_8 = TA_MA_7 * 2.0;
            double TA_MA_9 = TA_MA_8 * 2.0;
            double TA_MA_10 = TA_MA_9 * 2.0;
            double TA_MA_11 = TA_MA_10 * 2.0;
            double TA_MA_12 = TA_MA_11 * 2.0;
            double TA_MA_13 = TA_MA_12 * 2.0;
            double TA_MA_14 = TA_MA_13 * 2.0;
            double TA_MA_15 = TA_MA_14 * 2.0;
            double TA_MA_16 = TA_MA_15 * 2.0; // MSB
            ///////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////
            // MACH NUMBER DECODE CONSTANTS
            double MN_1 = 0.008;
            double MN_2 = MN_1 * 2.0;
            double MN_3 = MN_2 * 2.0;
            double MN_4 = MN_3 * 2.0;
            double MN_5 = MN_4 * 2.0;
            double MN_6 = MN_5 * 2.0;
            double MN_7 = MN_6 * 2.0;
            double MN_8 = MN_7 * 2.0;
            double MN_9 = MN_8 * 2.0;
            double MN_10 = MN_9 * 2.0;
            double MN_11 = MN_10 * 2.0;
            double MN_12 = MN_11 * 2.0;
            double MN_13 = MN_12 * 2.0;
            double MN_14 = MN_13 * 2.0;
            double MN_15 = MN_14 * 2.0;
            double MN_16 = MN_15 * 2.0;
            ////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////
            // GSPD NUMBER DECODE CONSTANTS
            double GSPD_1 = Math.Pow(2, (-14));
            double GSPD_2 = GSPD_1 * 2.0;
            double GSPD_3 = GSPD_2 * 2.0;
            double GSPD_4 = GSPD_3 * 2.0;
            double GSPD_5 = GSPD_4 * 2.0;
            double GSPD_6 = GSPD_5 * 2.0;
            double GSPD_7 = GSPD_6 * 2.0;
            double GSPD_8 = GSPD_7 * 2.0;
            double GSPD_9 = GSPD_8 * 2.0;
            double GSPD_10 = GSPD_9 * 2.0;
            double GSPD_11 = GSPD_10 * 2.0;
            double GSPD_12 = GSPD_11 * 2.0;
            double GSPD_13 = GSPD_12 * 2.0;
            double GSPD_14 = GSPD_13 * 2.0;
            double GSPD_15 = GSPD_14 * 2.0;
            double GSPD_16 = GSPD_15 * 2.0;
            ////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////
            // Roll Angle NUMBER DECODE CONSTANTS
            double Roll_A_1 = 0.01;
            double Roll_A_2 = Roll_A_1 * 2.0;
            double Roll_A_3 = Roll_A_2 * 2.0;
            double Roll_A_4 = Roll_A_3 * 2.0;
            double Roll_A_5 = Roll_A_4 * 2.0;
            double Roll_A_6 = Roll_A_5 * 2.0;
            double Roll_A_7 = Roll_A_6 * 2.0;
            double Roll_A_8 = Roll_A_7 * 2.0;
            double Roll_A_9 = Roll_A_8 * 2.0;
            double Roll_A_10 = Roll_A_9 * 2.0;
            double Roll_A_11 = Roll_A_10 * 2.0;
            double Roll_A_12 = Roll_A_11 * 2.0;
            double Roll_A_13 = Roll_A_12 * 2.0;
            double Roll_A_14 = Roll_A_13 * 2.0;
            double Roll_A_15 = Roll_A_14 * 2.0;
            double Roll_A_16 = Roll_A_15 * 2.0;
            ////////////////////////////////////////////////////////////////////


            // Define a global record for all data, then down there depending on the avalability of each field
            // populate specific items. Each item has validity flag that needs to be set for each available data
            // item for this message
            CAT62I380Types.CAT62I380Data CAT62DataRecord = new CAT62I380Types.CAT62I380Data();

            // Get an instance of bit ops
            Bit_Ops WORD0 = new Bit_Ops();
            Bit_Ops WORD1 = new Bit_Ops();
            Bit_Ops WORD2 = new Bit_Ops();
            Bit_Ops WORD3 = new Bit_Ops();

            //Extract the first octet
            WORD0.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

            if (WORD0.DWord[CAT62I380Types.WORD0_FX_Extension_Indicator] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                WORD1.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                if (WORD1.DWord[CAT62I380Types.WORD1_FX_Extension_Indicator] == true)
                {
                    CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                    WORD2.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                    if (WORD2.DWord[CAT62I380Types.WORD2_FX_Extension_Indicator] == true)
                    {
                        CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                        WORD3.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];
                        if (WORD3.DWord[CAT62I380Types.WORD3_FX_Extension_Indicator] == true)
                        {
                            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                        }
                    }
                }
            }

            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;

            // WORD0
            if (WORD0.DWord[CAT62I380Types.Target_Address] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 3;
            }
            if (WORD0.DWord[CAT62I380Types.Target_Identification] == true)
            {

                // Get all 6 octets
                Bit_Ops Bits_1_To_Bits_32_ = new Bit_Ops();
                Bit_Ops Bits_33_To_Bits_48_ = new Bit_Ops();

                Bits_1_To_Bits_32_.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 5];
                Bits_1_To_Bits_32_.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 4];
                Bits_1_To_Bits_32_.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 3];
                Bits_1_To_Bits_32_.DWord[Bit_Ops.Bits24_31_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 2];

                Bits_33_To_Bits_48_.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];
                Bits_33_To_Bits_48_.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];


                Bit_Ops Char1 = new Bit_Ops();
                Char1.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;
                Bit_Ops Char2 = new Bit_Ops();
                Char2.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;
                Bit_Ops Char3 = new Bit_Ops();
                Char3.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;
                Bit_Ops Char4 = new Bit_Ops();
                Char4.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;
                Bit_Ops Char5 = new Bit_Ops();
                Char5.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;
                Bit_Ops Char6 = new Bit_Ops();
                Char6.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;
                Bit_Ops Char7 = new Bit_Ops();
                Char7.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;
                Bit_Ops Char8 = new Bit_Ops();
                Char8.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;

                /////////////////////////////////////////
                // Decode character 1
                Char1.DWord[Bit_Ops.Bit5] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit15];
                Char1.DWord[Bit_Ops.Bit4] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit14];
                Char1.DWord[Bit_Ops.Bit3] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit13];
                Char1.DWord[Bit_Ops.Bit2] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit12];
                Char1.DWord[Bit_Ops.Bit1] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit11];
                Char1.DWord[Bit_Ops.Bit0] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit10];

                Char2.DWord[Bit_Ops.Bit5] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit9];
                Char2.DWord[Bit_Ops.Bit4] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit8];
                Char2.DWord[Bit_Ops.Bit3] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit7];
                Char2.DWord[Bit_Ops.Bit2] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit6];
                Char2.DWord[Bit_Ops.Bit1] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit5];
                Char2.DWord[Bit_Ops.Bit0] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit4];

                Char3.DWord[Bit_Ops.Bit5] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit3];
                Char3.DWord[Bit_Ops.Bit4] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit2];
                Char3.DWord[Bit_Ops.Bit3] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit1];
                Char3.DWord[Bit_Ops.Bit2] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit0];
                Char3.DWord[Bit_Ops.Bit1] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit31];
                Char3.DWord[Bit_Ops.Bit0] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit30];

                Char4.DWord[Bit_Ops.Bit5] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit29];
                Char4.DWord[Bit_Ops.Bit4] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit28];
                Char4.DWord[Bit_Ops.Bit3] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit27];
                Char4.DWord[Bit_Ops.Bit2] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit26];
                Char4.DWord[Bit_Ops.Bit1] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit25];
                Char4.DWord[Bit_Ops.Bit0] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit24];

                Char5.DWord[Bit_Ops.Bit5] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit23];
                Char5.DWord[Bit_Ops.Bit4] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit22];
                Char5.DWord[Bit_Ops.Bit3] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit21];
                Char5.DWord[Bit_Ops.Bit2] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit20];
                Char5.DWord[Bit_Ops.Bit1] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit19];
                Char5.DWord[Bit_Ops.Bit0] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit18];

                Char6.DWord[Bit_Ops.Bit5] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit17];
                Char6.DWord[Bit_Ops.Bit4] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit16];
                Char6.DWord[Bit_Ops.Bit3] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit15];
                Char6.DWord[Bit_Ops.Bit2] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit14];
                Char6.DWord[Bit_Ops.Bit1] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit13];
                Char6.DWord[Bit_Ops.Bit0] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit12];

                Char7.DWord[Bit_Ops.Bit5] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit11];
                Char7.DWord[Bit_Ops.Bit4] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit10];
                Char7.DWord[Bit_Ops.Bit3] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit9];
                Char7.DWord[Bit_Ops.Bit2] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit8];
                Char7.DWord[Bit_Ops.Bit1] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit7];
                Char7.DWord[Bit_Ops.Bit0] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit6];

                Char8.DWord[Bit_Ops.Bit5] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit5];
                Char8.DWord[Bit_Ops.Bit4] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit4];
                Char8.DWord[Bit_Ops.Bit3] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit3];
                Char8.DWord[Bit_Ops.Bit2] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit2];
                Char8.DWord[Bit_Ops.Bit1] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit1];
                Char8.DWord[Bit_Ops.Bit0] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit0];

                CAT62DataRecord.ACID.Is_Valid = true;
                CAT62DataRecord.ACID.ACID_String = Decode6BitASCII(Char1.DWord[Bit_Ops.Bits0_7_Of_DWord]) +
                    Decode6BitASCII(Char2.DWord[Bit_Ops.Bits0_7_Of_DWord]) +
                    Decode6BitASCII(Char3.DWord[Bit_Ops.Bits0_7_Of_DWord]) +
                    Decode6BitASCII(Char4.DWord[Bit_Ops.Bits0_7_Of_DWord]) +
                    Decode6BitASCII(Char5.DWord[Bit_Ops.Bits0_7_Of_DWord]) +
                    Decode6BitASCII(Char6.DWord[Bit_Ops.Bits0_7_Of_DWord]) +
                    Decode6BitASCII(Char7.DWord[Bit_Ops.Bits0_7_Of_DWord]) +
                    Decode6BitASCII(Char8.DWord[Bit_Ops.Bits0_7_Of_DWord]);

                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 6;
            }
            if (WORD0.DWord[CAT62I380Types.Magnetic_Heading] == true)
            {
                Bit_Ops BO = new Bit_Ops();
                BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];
                BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                double TA = 0.0;
                if (BO.DWord[Bit_Ops.Bit0])
                    TA = TA_MA_1;
                if (BO.DWord[Bit_Ops.Bit1])
                    TA = TA + TA_MA_2;
                if (BO.DWord[Bit_Ops.Bit2])
                    TA = TA + TA_MA_3;
                if (BO.DWord[Bit_Ops.Bit3])
                    TA = TA + TA_MA_4;
                if (BO.DWord[Bit_Ops.Bit4])
                    TA = TA + TA_MA_5;
                if (BO.DWord[Bit_Ops.Bit5])
                    TA = TA + TA_MA_6;
                if (BO.DWord[Bit_Ops.Bit6])
                    TA = TA + TA_MA_7;
                if (BO.DWord[Bit_Ops.Bit7])
                    TA = TA + TA_MA_8;
                if (BO.DWord[Bit_Ops.Bit8])
                    TA = TA + TA_MA_9;
                if (BO.DWord[Bit_Ops.Bit9])
                    TA = TA + TA_MA_10;
                if (BO.DWord[Bit_Ops.Bit10])
                    TA = TA + TA_MA_11;
                if (BO.DWord[Bit_Ops.Bit11])
                    TA = TA + TA_MA_12;
                if (BO.DWord[Bit_Ops.Bit12])
                    TA = TA + TA_MA_13;
                if (BO.DWord[Bit_Ops.Bit13])
                    TA = TA + TA_MA_14;
                if (BO.DWord[Bit_Ops.Bit14])
                    TA = TA + TA_MA_15;
                if (BO.DWord[Bit_Ops.Bit15])
                    TA = TA + TA_MA_16;

                CAT62DataRecord.M_HDG.Is_Valid = true;
                CAT62DataRecord.M_HDG.M_HDG = TA;
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD0.DWord[CAT62I380Types.Indicated_Airspeed_Mach_Number] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD0.DWord[CAT62I380Types.True_Airspeed] == true)
            {
                Bit_Ops BO = new Bit_Ops();
                BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];
                BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                CAT62DataRecord.TAS.Is_Valid = true;
                int Result = BO.DWord[Bit_Ops.Bits0_15_Of_DWord];
                CAT62DataRecord.TAS.TAS = Result;

                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD0.DWord[CAT62I380Types.Selected_Altitude] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD0.DWord[CAT62I380Types.Final_State_SelectedAltitude] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            // WORD1
            if (WORD1.DWord[CAT62I380Types.Trajectory_Intent_Status] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD1.DWord[CAT62I380Types.Trajectory_Intent_Data] == true)
            {
                // Repetitive Data Item starting with a one-octet Field Repetition
                // Indicator (REP) followed by at least one Trajectory Intent Point
                // comprising fifteen octets
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 16;
            }
            if (WORD1.DWord[CAT62I380Types.Communications_ACAS] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD1.DWord[CAT62I380Types.Status_Reported_By_ADS_B] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD1.DWord[CAT62I380Types.ACAS_Resolution_Advisory_Report] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 7;
            }
            if (WORD1.DWord[CAT62I380Types.Barometric_Vertical_Rate] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD1.DWord[CAT62I380Types.Geometric_Vertical_Rate] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            // WORD2
            if (WORD2.DWord[CAT62I380Types.Roll_Angle] == true)
            {
                //(LSB) = 0.01 degree
                //-180 ≤ Roll Angle ≤ 180
                
                Bit_Ops BO = new Bit_Ops();
                BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];
                BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                double RA = 0.0;

                if (BO.DWord[Bit_Ops.Bit15])
                {
                    // Invert each bit
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

                    BO.DWord[Bit_Ops.Bits0_15_Of_DWord] = BO.DWord[Bit_Ops.Bits0_15_Of_DWord] + 1;

                     if (BO.DWord[Bit_Ops.Bit0])
                        RA = Roll_A_1;
                    if (BO.DWord[Bit_Ops.Bit1])
                        RA = RA + Roll_A_2;
                    if (BO.DWord[Bit_Ops.Bit2])
                        RA = RA + Roll_A_3;
                    if (BO.DWord[Bit_Ops.Bit3])
                        RA = RA + Roll_A_4;
                    if (BO.DWord[Bit_Ops.Bit4])
                        RA = RA + Roll_A_5;
                    if (BO.DWord[Bit_Ops.Bit5])
                        RA = RA + Roll_A_6;
                    if (BO.DWord[Bit_Ops.Bit6])
                        RA = RA + Roll_A_7;
                    if (BO.DWord[Bit_Ops.Bit7])
                        RA = RA + Roll_A_8;
                    if (BO.DWord[Bit_Ops.Bit8])
                        RA = RA + Roll_A_9;
                    if (BO.DWord[Bit_Ops.Bit9])
                        RA = RA + Roll_A_10;
                    if (BO.DWord[Bit_Ops.Bit10])
                        RA = RA + Roll_A_11;
                    if (BO.DWord[Bit_Ops.Bit11])
                        RA = RA + Roll_A_12;
                    if (BO.DWord[Bit_Ops.Bit12])
                        RA = RA + Roll_A_13;
                    if (BO.DWord[Bit_Ops.Bit13])
                        RA = RA + Roll_A_14;
                    if (BO.DWord[Bit_Ops.Bit14])
                        RA = RA + Roll_A_15;

                    RA = -RA;
                }
                else
                {
                    if (BO.DWord[Bit_Ops.Bit0])
                        RA = Roll_A_1;
                    if (BO.DWord[Bit_Ops.Bit1])
                        RA = RA + Roll_A_2;
                    if (BO.DWord[Bit_Ops.Bit2])
                        RA = RA + Roll_A_3;
                    if (BO.DWord[Bit_Ops.Bit3])
                        RA = RA + Roll_A_4;
                    if (BO.DWord[Bit_Ops.Bit4])
                        RA = RA + Roll_A_5;
                    if (BO.DWord[Bit_Ops.Bit5])
                        RA = RA + Roll_A_6;
                    if (BO.DWord[Bit_Ops.Bit6])
                        RA = RA + Roll_A_7;
                    if (BO.DWord[Bit_Ops.Bit7])
                        RA = RA + Roll_A_8;
                    if (BO.DWord[Bit_Ops.Bit8])
                        RA = RA + Roll_A_9;
                    if (BO.DWord[Bit_Ops.Bit9])
                        RA = RA + Roll_A_10;
                    if (BO.DWord[Bit_Ops.Bit10])
                        RA = RA + Roll_A_11;
                    if (BO.DWord[Bit_Ops.Bit11])
                        RA = RA + Roll_A_12;
                    if (BO.DWord[Bit_Ops.Bit12])
                        RA = RA + Roll_A_13;
                    if (BO.DWord[Bit_Ops.Bit13])
                        RA = RA + Roll_A_14;
                    if (BO.DWord[Bit_Ops.Bit14])
                        RA = RA + Roll_A_15;
                }
               
                CAT62DataRecord.Rool_Angle.Is_Valid = true;
                CAT62DataRecord.Rool_Angle.Rool_Angle = RA;
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;

            }
            if (WORD2.DWord[CAT62I380Types.Track_Angle_Rate] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD2.DWord[CAT62I380Types.Track_Angle] == true)
            {
                Bit_Ops BO = new Bit_Ops();
                BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];
                BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                double TA = 0.0;
                if (BO.DWord[Bit_Ops.Bit0])
                    TA = TA_MA_1;
                if (BO.DWord[Bit_Ops.Bit1])
                    TA = TA + TA_MA_2;
                if (BO.DWord[Bit_Ops.Bit2])
                    TA = TA + TA_MA_3;
                if (BO.DWord[Bit_Ops.Bit3])
                    TA = TA + TA_MA_4;
                if (BO.DWord[Bit_Ops.Bit4])
                    TA = TA + TA_MA_5;
                if (BO.DWord[Bit_Ops.Bit5])
                    TA = TA + TA_MA_6;
                if (BO.DWord[Bit_Ops.Bit6])
                    TA = TA + TA_MA_7;
                if (BO.DWord[Bit_Ops.Bit7])
                    TA = TA + TA_MA_8;
                if (BO.DWord[Bit_Ops.Bit8])
                    TA = TA + TA_MA_9;
                if (BO.DWord[Bit_Ops.Bit9])
                    TA = TA + TA_MA_10;
                if (BO.DWord[Bit_Ops.Bit10])
                    TA = TA + TA_MA_11;
                if (BO.DWord[Bit_Ops.Bit11])
                    TA = TA + TA_MA_12;
                if (BO.DWord[Bit_Ops.Bit12])
                    TA = TA + TA_MA_13;
                if (BO.DWord[Bit_Ops.Bit13])
                    TA = TA + TA_MA_14;
                if (BO.DWord[Bit_Ops.Bit14])
                    TA = TA + TA_MA_15;
                if (BO.DWord[Bit_Ops.Bit15])
                    TA = TA + TA_MA_16;

                CAT62DataRecord.TRK.Is_Valid = true;
                CAT62DataRecord.TRK.TRK = TA;
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD2.DWord[CAT62I380Types.Ground_Speed] == true)
            {
                Bit_Ops BO = new Bit_Ops();
                BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];
                BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                double GSPD = 0.0;

                if (BO.DWord[Bit_Ops.Bit15])
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

                    BO.DWord[Bit_Ops.Bits0_15_Of_DWord] = BO.DWord[Bit_Ops.Bits0_15_Of_DWord] + 1;

                    if (BO.DWord[Bit_Ops.Bit0])
                        GSPD = GSPD_1;
                    if (BO.DWord[Bit_Ops.Bit1])
                        GSPD = GSPD + GSPD_2;
                    if (BO.DWord[Bit_Ops.Bit2])
                        GSPD = GSPD + GSPD_3;
                    if (BO.DWord[Bit_Ops.Bit3])
                        GSPD = GSPD + GSPD_4;
                    if (BO.DWord[Bit_Ops.Bit4])
                        GSPD = GSPD + GSPD_5;
                    if (BO.DWord[Bit_Ops.Bit5])
                        GSPD = GSPD + GSPD_6;
                    if (BO.DWord[Bit_Ops.Bit6])
                        GSPD = GSPD + GSPD_7;
                    if (BO.DWord[Bit_Ops.Bit7])
                        GSPD = GSPD + GSPD_8;
                    if (BO.DWord[Bit_Ops.Bit8])
                        GSPD = GSPD + GSPD_9;
                    if (BO.DWord[Bit_Ops.Bit9])
                        GSPD = GSPD + GSPD_10;
                    if (BO.DWord[Bit_Ops.Bit10])
                        GSPD = GSPD + GSPD_11;
                    if (BO.DWord[Bit_Ops.Bit11])
                        GSPD = GSPD + GSPD_12;
                    if (BO.DWord[Bit_Ops.Bit12])
                        GSPD = GSPD + GSPD_13;
                    if (BO.DWord[Bit_Ops.Bit13])
                        GSPD = GSPD + GSPD_14;
                    if (BO.DWord[Bit_Ops.Bit14])
                        GSPD = GSPD + GSPD_15;

                    GSPD  = -GSPD;
                }
                else
                {
                    if (BO.DWord[Bit_Ops.Bit0])
                        GSPD = GSPD_1;
                    if (BO.DWord[Bit_Ops.Bit1])
                        GSPD = GSPD + GSPD_2;
                    if (BO.DWord[Bit_Ops.Bit2])
                        GSPD = GSPD + GSPD_3;
                    if (BO.DWord[Bit_Ops.Bit3])
                        GSPD = GSPD + GSPD_4;
                    if (BO.DWord[Bit_Ops.Bit4])
                        GSPD = GSPD + GSPD_5;
                    if (BO.DWord[Bit_Ops.Bit5])
                        GSPD = GSPD + GSPD_6;
                    if (BO.DWord[Bit_Ops.Bit6])
                        GSPD = GSPD + GSPD_7;
                    if (BO.DWord[Bit_Ops.Bit7])
                        GSPD = GSPD + GSPD_8;
                    if (BO.DWord[Bit_Ops.Bit8])
                        GSPD = GSPD + GSPD_9;
                    if (BO.DWord[Bit_Ops.Bit9])
                        GSPD = GSPD + GSPD_10;
                    if (BO.DWord[Bit_Ops.Bit10])
                        GSPD = GSPD + GSPD_11;
                    if (BO.DWord[Bit_Ops.Bit11])
                        GSPD = GSPD + GSPD_12;
                    if (BO.DWord[Bit_Ops.Bit12])
                        GSPD = GSPD + GSPD_13;
                    if (BO.DWord[Bit_Ops.Bit13])
                        GSPD = GSPD + GSPD_14;
                    if (BO.DWord[Bit_Ops.Bit14])
                        GSPD = GSPD + GSPD_15;
                }

                CAT62DataRecord.GSPD.Is_Valid = true;
                CAT62DataRecord.GSPD.GSPD = GSPD * 60.0 * 60.0;
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD2.DWord[CAT62I380Types.Velocity_Uncertainty] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD2.DWord[CAT62I380Types.Meteorological_Data] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 8;
            }
            if (WORD2.DWord[CAT62I380Types.Emitter_Category] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            // WORD3
            if (WORD3.DWord[CAT62I380Types.Position_Data] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 6;
            }
            if (WORD3.DWord[CAT62I380Types.Geometric_Altitude_Data] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD3.DWord[CAT62I380Types.Position_Uncertainty_Data] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD3.DWord[CAT62I380Types.Mode_S_MB_Data] == true)
            {
                // Repetitive starting with an one-octet Field Repetition Indicator
                // (REP) followed by at least one BDS report comprising one seven
                // octet BDS register and one octet BDS code.
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 9;
            }
            if (WORD3.DWord[CAT62I380Types.Indicated_Airspeed] == true)
            {
                Bit_Ops BO = new Bit_Ops();
                BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];
                BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                CAT62DataRecord.IAS.Is_Valid = true;
                int Result = BO.DWord[Bit_Ops.Bits0_15_Of_DWord];
                CAT62DataRecord.IAS.IAS = Result;

                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD3.DWord[CAT62I380Types.Mach_Number] == true)
            {
                Bit_Ops BO = new Bit_Ops();
                BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];
                BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                double MN = 0.0;
                if (BO.DWord[Bit_Ops.Bit0])
                    MN = MN_1;
                if (BO.DWord[Bit_Ops.Bit1])
                    MN = MN + MN_2;
                if (BO.DWord[Bit_Ops.Bit2])
                    MN = MN + MN_3;
                if (BO.DWord[Bit_Ops.Bit3])
                    MN = MN + MN_4;
                if (BO.DWord[Bit_Ops.Bit4])
                    MN = MN + MN_5;
                if (BO.DWord[Bit_Ops.Bit5])
                    MN = MN + MN_6;
                if (BO.DWord[Bit_Ops.Bit6])
                    MN = MN + MN_7;
                if (BO.DWord[Bit_Ops.Bit7])
                    MN = MN + MN_8;
                if (BO.DWord[Bit_Ops.Bit8])
                    MN = MN + MN_9;
                if (BO.DWord[Bit_Ops.Bit9])
                    MN = MN + MN_10;
                if (BO.DWord[Bit_Ops.Bit10])
                    MN = MN + MN_11;
                if (BO.DWord[Bit_Ops.Bit11])
                    MN = MN + MN_12;
                if (BO.DWord[Bit_Ops.Bit12])
                    MN = MN + MN_13;
                if (BO.DWord[Bit_Ops.Bit13])
                    MN = MN + MN_14;
                if (BO.DWord[Bit_Ops.Bit14])
                    MN = MN + MN_15;
                if (BO.DWord[Bit_Ops.Bit15])
                    MN = MN + MN_16;

                CAT62DataRecord.MACH.Is_Valid = true;
                CAT62DataRecord.MACH.MACH = MN;
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD3.DWord[CAT62I380Types.Barometric_Pressure_Setting] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT62.I062DataItems[CAT62.ItemIDToIndex("380")].value = CAT62DataRecord;
            //////////////////////////////////////////////////////////////////////////////////
        }

        private static string Decode6BitASCII(int ASCII_Code)
        {
            string CharOut = " ";
            switch (ASCII_Code)
            {   //////////////////////////////
                // Handle numbers 0 .. 9
                //////////////////////////////
                case 48:
                    CharOut = "0";
                    break;
                case 49:
                    CharOut = "1";
                    break;
                case 50:
                    CharOut = "2";
                    break;
                case 51:
                    CharOut = "3";
                    break;
                case 52:
                    CharOut = "4";
                    break;
                case 53:
                    CharOut = "5";
                    break;
                case 54:
                    CharOut = "6";
                    break;
                case 55:
                    CharOut = "7";
                    break;
                case 56:
                    CharOut = "8";
                    break;
                case 57:
                    CharOut = "9";
                    break;
                //////////////////////////////
                // Handle letters
                //////////////////////////////
                case 1:
                    CharOut = "A";
                    break;
                case 2:
                    CharOut = "B";
                    break;
                case 3:
                    CharOut = "C";
                    break;
                case 4:
                    CharOut = "D";
                    break;
                case 5:
                    CharOut = "E";
                    break;
                case 6:
                    CharOut = "F";
                    break;
                case 7:
                    CharOut = "G";
                    break;
                case 8:
                    CharOut = "H";
                    break;
                case 9:
                    CharOut = "I";
                    break;
                case 10:
                    CharOut = "J";
                    break;
                case 11:
                    CharOut = "K";
                    break;
                case 12:
                    CharOut = "L";
                    break;
                case 13:
                    CharOut = "M";
                    break;
                case 14:
                    CharOut = "N";
                    break;
                case 15:
                    CharOut = "O";
                    break;
                case 16:
                    CharOut = "P";
                    break;
                case 17:
                    CharOut = "Q";
                    break;
                case 18:
                    CharOut = "R";
                    break;
                case 19:
                    CharOut = "S";
                    break;
                case 20:
                    CharOut = "T";
                    break;
                case 21:
                    CharOut = "U";
                    break;
                case 22:
                    CharOut = "V";
                    break;
                case 23:
                    CharOut = "W";
                    break;
                case 24:
                    CharOut = "X";
                    break;
                case 25:
                    CharOut = "Y";
                    break;
                case 26:
                    CharOut = "Z";
                    break;
                //////////////////////////////
                // Handle special characters
                //////////////////////////////
                case 32:
                    CharOut = " ";
                    break;
            }

            return CharOut;
        }
    }
}
