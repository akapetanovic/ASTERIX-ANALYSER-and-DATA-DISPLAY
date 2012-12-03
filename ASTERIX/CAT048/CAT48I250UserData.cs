using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT48I250UserData
    {
        public static void DecodeCAT48I250(byte[] Data)
        {
            #region DecodeConstantsRegion
            ////////////////////////////////////////////////////////////////
            // MAG HDG DECODE CONSTANTS
            double MAG_HDG_1 = 90.0 / 512.0;
            double MAG_HDG_2 = MAG_HDG_1 * 2.0;
            double MAG_HDG_3 = MAG_HDG_2 * 2.0;
            double MAG_HDG_4 = MAG_HDG_3 * 2.0;
            double MAG_HDG_5 = MAG_HDG_4 * 2.0;
            double MAG_HDG_6 = MAG_HDG_5 * 2.0;
            double MAG_HDG_7 = MAG_HDG_6 * 2.0;
            double MAG_HDG_8 = MAG_HDG_7 * 2.0;
            double MAG_HDG_9 = MAG_HDG_8 * 2.0;
            double MAG_HDG_10 = MAG_HDG_9 * 2.0;
            ////////////////////////////////////////////////////////////////
            // IAS DECODE CONSTANTS
            int IAS_1 = 1;
            int IAS_2 = IAS_1 * 2;
            int IAS_3 = IAS_2 * 2;
            int IAS_4 = IAS_3 * 2;
            int IAS_5 = IAS_4 * 2;
            int IAS_6 = IAS_5 * 2;
            int IAS_7 = IAS_6 * 2;
            int IAS_8 = IAS_7 * 2;
            int IAS_9 = IAS_8 * 2;
            int IAS_10 = IAS_9 * 2;
            ////////////////////////////////////////////////////////////////
            // MACH DECODE CONSTANTS
            double MACH_1 = 2.048 / 512.0;
            double MACH_2 = MACH_1 * 2.0;
            double MACH_3 = MACH_2 * 2.0;
            double MACH_4 = MACH_3 * 2.0;
            double MACH_5 = MACH_4 * 2.0;
            double MACH_6 = MACH_5 * 2.0;
            double MACH_7 = MACH_6 * 2.0;
            double MACH_8 = MACH_7 * 2.0;
            double MACH_9 = MACH_8 * 2.0;
            double MACH_10 = MAG_HDG_9 * 2.0;
            ////////////////////////////////////////////////////////////////
            // RoC DECODE CONSTANTS
            int RoC_1 = 8192 / 256; // 32 feet/min
            int RoC_2 = RoC_1 * 2;
            int RoC_3 = RoC_2 * 2;
            int RoC_4 = RoC_3 * 2;
            int RoC_5 = RoC_4 * 2;
            int RoC_6 = RoC_5 * 2;
            int RoC_7 = RoC_6 * 2;
            int RoC_8 = RoC_7 * 2;
            int RoC_9 = RoC_8 * 2;
            ////////////////////////////////////////////////////////////////
            // ROLL ANGLE CONSTANTS
            double ROLL_ANG_1 = 45.0 / 256.0;
            double ROLL_ANG_2 = ROLL_ANG_1 * 2;
            double ROLL_ANG_3 = ROLL_ANG_2 * 2;
            double ROLL_ANG_4 = ROLL_ANG_3 * 2;
            double ROLL_ANG_5 = ROLL_ANG_4 * 2;
            double ROLL_ANG_6 = ROLL_ANG_5 * 2;
            double ROLL_ANG_7 = ROLL_ANG_6 * 2;
            double ROLL_ANG_8 = ROLL_ANG_7 * 2;
            double ROLL_ANG_9 = ROLL_ANG_8 * 2;
            ////////////////////////////////////////////////////////////////
            // TRUE TRACK ANGLE CONSTANTS
            double TRUE_TRK_ANG_1 = 90.0 / 512.0;
            double TRUE_TRK_ANG_2 = TRUE_TRK_ANG_1 * 2;
            double TRUE_TRK_ANG_3 = TRUE_TRK_ANG_2 * 2;
            double TRUE_TRK_ANG_4 = TRUE_TRK_ANG_3 * 2;
            double TRUE_TRK_ANG_5 = TRUE_TRK_ANG_4 * 2;
            double TRUE_TRK_ANG_6 = TRUE_TRK_ANG_5 * 2;
            double TRUE_TRK_ANG_7 = TRUE_TRK_ANG_6 * 2;
            double TRUE_TRK_ANG_8 = TRUE_TRK_ANG_7 * 2;
            double TRUE_TRK_ANG_9 = TRUE_TRK_ANG_8 * 2;
            double TRUE_TRK_ANG_10 = TRUE_TRK_ANG_9 * 2;
            ////////////////////////////////////////////////////////////////
            // GND SPD CONSTANTS
            double GND_SPD_1 = 1024.0 / 512.0;
            double GND_SPD_2 = GND_SPD_1 * 2;
            double GND_SPD_3 = GND_SPD_2 * 2;
            double GND_SPD_4 = GND_SPD_3 * 2;
            double GND_SPD_5 = GND_SPD_4 * 2;
            double GND_SPD_6 = GND_SPD_5 * 2;
            double GND_SPD_7 = GND_SPD_6 * 2;
            double GND_SPD_8 = GND_SPD_7 * 2;
            double GND_SPD_9 = GND_SPD_8 * 2;
            double GND_SPD_10 = GND_SPD_9 * 2;
            ////////////////////////////////////////////////////////////////
            // TRK AND RFATE CONSTANTS
            double TRK_ANG_RATE_1 = 8.0 / 256.0;
            double TRK_ANG_RATE_2 = TRK_ANG_RATE_1 * 2;
            double TRK_ANG_RATE_3 = TRK_ANG_RATE_2 * 2;
            double TRK_ANG_RATE_4 = TRK_ANG_RATE_3 * 2;
            double TRK_ANG_RATE_5 = TRK_ANG_RATE_4 * 2;
            double TRK_ANG_RATE_6 = TRK_ANG_RATE_5 * 2;
            double TRK_ANG_RATE_7 = TRK_ANG_RATE_6 * 2;
            double TRK_ANG_RATE_8 = TRK_ANG_RATE_7 * 2;
            double TRK_ANG_RATE_9 = TRK_ANG_RATE_8 * 2;
            ////////////////////////////////////////////////////////////////
            // TRUE AIRSPEED
            int TAS_1 = 2;
            int TAS_2 = TAS_1 * 2;
            int TAS_3 = TAS_2 * 2;
            int TAS_4 = TAS_3 * 2;
            int TAS_5 = TAS_4 * 2;
            int TAS_6 = TAS_5 * 2;
            int TAS_7 = TAS_6 * 2;
            int TAS_8 = TAS_7 * 2;
            int TAS_9 = TAS_8 * 2;
            int TAS_10 = TAS_9 * 2;
            ////////////////////////////////////////////////////////////////
            // SELECTED_ALTITUDE
            int SEL_ALT_1 = 16; //feet
            int SEL_ALT_2 = SEL_ALT_1 * 2;
            int SEL_ALT_3 = SEL_ALT_2 * 2;
            int SEL_ALT_4 = SEL_ALT_3 * 2;
            int SEL_ALT_5 = SEL_ALT_4 * 2;
            int SEL_ALT_6 = SEL_ALT_5 * 2;
            int SEL_ALT_7 = SEL_ALT_6 * 2;
            int SEL_ALT_8 = SEL_ALT_7 * 2;
            int SEL_ALT_9 = SEL_ALT_8 * 2;
            int SEL_ALT_10 = SEL_ALT_9 * 2;
            int SEL_ALT_11 = SEL_ALT_10 * 2;
            int SEL_ALT_12 = SEL_ALT_11 * 2;
            ////////////////////////////////////////////////////////////////
            // BARO SETTING
            double BARO_STNG_1 = 0.1; //mb (add 800.0 mb to the value computed)
            double BARO_STNG_2 = BARO_STNG_1 * 2;
            double BARO_STNG_3 = BARO_STNG_2 * 2;
            double BARO_STNG_4 = BARO_STNG_3 * 2;
            double BARO_STNG_5 = BARO_STNG_4 * 2;
            double BARO_STNG_6 = BARO_STNG_5 * 2;
            double BARO_STNG_7 = BARO_STNG_6 * 2;
            double BARO_STNG_8 = BARO_STNG_7 * 2;
            double BARO_STNG_9 = BARO_STNG_8 * 2;
            double BARO_STNG_10 = BARO_STNG_9 * 2;
            double BARO_STNG_11 = BARO_STNG_10 * 2;
            double BARO_STNG_12 = BARO_STNG_11 * 2;
            #endregion

            CAT48I250Types.CAT48I250DataType CAT48I250Data = new CAT48I250Types.CAT48I250DataType();
            // The lenght is always 1 + 8 X (repetative factor form the first octet)

            // Get an instance of bit ops
            Bit_Ops BO1 = new Bit_Ops();
            Bit_Ops BO2 = new Bit_Ops();

            //Extract the first octet
            BO1.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 1];

            int Repetative_Factor = (int)BO1.DWord[Bit_Ops.Bits0_7_Of_DWord];
            CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + 1;

            //////////////////////////////////////////////////////////////////////////////////////////
            for (int I = 0; I < Repetative_Factor; I++)
            {
                BO1.DWord[Bit_Ops.Bits24_31_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 1 + (8 * I)];
                BO1.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 2 + (8 * I)];
                BO1.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 3 + (8 * I)];
                BO1.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 4 + (8 * I)];

                BO2.DWord[Bit_Ops.Bits24_31_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 5 + (8 * I)];
                BO2.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 6 + (8 * I)];
                BO2.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 7 + (8 * I)];
                BO2.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 8 + (8 * I)];

                // Fist check whether this is a Broadcst report or CIGB initiated
                int Register_Number = System.Convert.ToInt16(BO2.DWord[Bit_Ops.Bits0_7_Of_DWord]);
                string Test = "NONE";
                double Value = 0.0;

                if (Register_Number == 0)
                {
                    #region BroadcastMessages
                    Register_Number = System.Convert.ToInt16(BO1.DWord[Bit_Ops.Bits0_7_Of_DWord]);

                    // Parse the data depending on the register number
                    switch (Register_Number.ToString("X"))
                    {
                        default:
                            Test = Register_Number.ToString("X");
                            Test = Test + Test;
                            break;
                    }
                    #endregion
                }
                else
                {
                    #region GCIB_Messages
                    // Parse the data depending on the register number
                    switch (Register_Number.ToString("X"))
                    {
                        // Data link capability report
                        case "10":

                            break;
                        // Common usage GICB capability report
                        case "17":

                            break;
                        // Selected vertical intention
                        case "40":

                            // BDS40 is present
                            CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.Present_This_Cycle = true;

                            #region MCP_FCU_SEL_ALT_REGION
                            // Bit 1 MCP_FCU_SEL_ALTD Status
                            CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.MCP_FCU_Sel_ALT.Is_Valid = BO1.DWord[Bit_Ops.Bit31];

                            // Bits 2 .. 13 MCP_FCU_SEL_ALT Value
                            Value = 0.0;

                            if (BO1.DWord[Bit_Ops.Bit19])
                                Value = SEL_ALT_1;
                            if (BO1.DWord[Bit_Ops.Bit20])
                                Value = Value + SEL_ALT_2;
                            if (BO1.DWord[Bit_Ops.Bit21])
                                Value = Value + SEL_ALT_3;
                            if (BO1.DWord[Bit_Ops.Bit22])
                                Value = Value + SEL_ALT_4;
                            if (BO1.DWord[Bit_Ops.Bit23])
                                Value = Value + SEL_ALT_5;
                            if (BO1.DWord[Bit_Ops.Bit24])
                                Value = Value + SEL_ALT_6;
                            if (BO1.DWord[Bit_Ops.Bit25])
                                Value = Value + SEL_ALT_7;
                            if (BO1.DWord[Bit_Ops.Bit26])
                                Value = Value + SEL_ALT_8;
                            if (BO1.DWord[Bit_Ops.Bit27])
                                Value = Value + SEL_ALT_9;
                            if (BO1.DWord[Bit_Ops.Bit28])
                                Value = Value + SEL_ALT_10;
                            if (BO1.DWord[Bit_Ops.Bit29])
                                Value = Value + SEL_ALT_11;
                            if (BO1.DWord[Bit_Ops.Bit30])
                                Value = Value + SEL_ALT_12;

                            CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.MCP_FCU_Sel_ALT.Value = (int)Value / 100; // Use FL
                            #endregion
                            #region FMS_SELECTED_ALT_REGION
                            // Bit 14 FMS_SELECTED_ALT Status
                            CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.FMS_Sel_ALT.Is_Valid = BO1.DWord[Bit_Ops.Bit18];

                            // Bits 15 .. 26 MCP_FCU_SEL_ALT Value
                            Value = 0.0;

                            if (BO1.DWord[Bit_Ops.Bit6])
                                Value = SEL_ALT_1;
                            if (BO1.DWord[Bit_Ops.Bit7])
                                Value = Value + SEL_ALT_2;
                            if (BO1.DWord[Bit_Ops.Bit8])
                                Value = Value + SEL_ALT_3;
                            if (BO1.DWord[Bit_Ops.Bit9])
                                Value = Value + SEL_ALT_4;
                            if (BO1.DWord[Bit_Ops.Bit10])
                                Value = Value + SEL_ALT_5;
                            if (BO1.DWord[Bit_Ops.Bit11])
                                Value = Value + SEL_ALT_6;
                            if (BO1.DWord[Bit_Ops.Bit12])
                                Value = Value + SEL_ALT_7;
                            if (BO1.DWord[Bit_Ops.Bit13])
                                Value = Value + SEL_ALT_8;
                            if (BO1.DWord[Bit_Ops.Bit14])
                                Value = Value + SEL_ALT_9;
                            if (BO1.DWord[Bit_Ops.Bit15])
                                Value = Value + SEL_ALT_10;
                            if (BO1.DWord[Bit_Ops.Bit16])
                                Value = Value + SEL_ALT_11;
                            if (BO1.DWord[Bit_Ops.Bit17])
                                Value = Value + SEL_ALT_12;

                            CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.FMS_Sel_ALT.Value = (int)Value / 100; // Use FL
                            #endregion
                            #region BARO_SETTING_REGION
                            // Bit 27 BARO_SETTING Status
                            CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.Baro_Sel_ALT.Is_Valid = BO1.DWord[Bit_Ops.Bit5];

                            // Bits 28 .. 39 MCP_FCU_SEL_ALT Value
                            Value = 0.0;

                            if (BO2.DWord[Bit_Ops.Bit25])
                                Value = BARO_STNG_1;
                            if (BO2.DWord[Bit_Ops.Bit26])
                                Value = Value + BARO_STNG_2;
                            if (BO2.DWord[Bit_Ops.Bit27])
                                Value = Value + BARO_STNG_3;
                            if (BO2.DWord[Bit_Ops.Bit28])
                                Value = Value + BARO_STNG_4;
                            if (BO2.DWord[Bit_Ops.Bit29])
                                Value = Value + BARO_STNG_5;
                            if (BO2.DWord[Bit_Ops.Bit30])
                                Value = Value + BARO_STNG_6;
                            if (BO2.DWord[Bit_Ops.Bit31])
                                Value = Value + BARO_STNG_7;
                            if (BO1.DWord[Bit_Ops.Bit0])
                                Value = Value + BARO_STNG_8;
                            if (BO1.DWord[Bit_Ops.Bit1])
                                Value = Value + BARO_STNG_9;
                            if (BO1.DWord[Bit_Ops.Bit2])
                                Value = Value + BARO_STNG_10;
                            if (BO1.DWord[Bit_Ops.Bit3])
                                Value = Value + BARO_STNG_11;
                            if (BO1.DWord[Bit_Ops.Bit4])
                                Value = Value + BARO_STNG_12;

                            CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.Baro_Sel_ALT.Value = (int)Value + 800; // Always add 800mb
                            #endregion
                            // BIT 40 .. 47 ARE RESERVED (24 ..17)
                            #region STATUS_REGION
                            // Bit 48 .. 56 STATUS

                            // Bit 48 STATUS OF MCP/FCU MODE BITS
                            CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.Status_Data.MCP_FCU_Mode_Bits_Populated = BO2.DWord[Bit_Ops.Bit16];
                            // Bit 49 VNAV MODE
                            CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.Status_Data.VNAV_Mode_Active = BO2.DWord[Bit_Ops.Bit15];
                            // Bit 50 ALT HOLD MODE MODE
                            CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.Status_Data.ALT_Hold_Active = BO2.DWord[Bit_Ops.Bit14];
                            // Bit 51 ALT HOLD MODE MODE
                            CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.Status_Data.APP_Mode_Active = BO2.DWord[Bit_Ops.Bit13];

                            // Bits 52 .. 53 are reserved

                            // Bit 54 ALT STATUS OF TARGET ALT SOURCE BITS and 55..56 TARGET ALT SOURCE
                            if (!BO2.DWord[Bit_Ops.Bit10])
                            {
                                CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.Status_Data.Target_Altitude_Source = CAT48I250Types.BDS40_Selected_Vertical_Intention_Report.Status.Target_Altitude_Mode_Type.Unknown;
                            }
                            else
                            {
                                if (BO2.DWord[Bit_Ops.Bit9] && BO2.DWord[Bit_Ops.Bit8])
                                    CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.Status_Data.Target_Altitude_Source = CAT48I250Types.BDS40_Selected_Vertical_Intention_Report.Status.Target_Altitude_Mode_Type.FMS_Selected_Alt;
                                else if (BO2.DWord[Bit_Ops.Bit9])
                                    CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.Status_Data.Target_Altitude_Source = CAT48I250Types.BDS40_Selected_Vertical_Intention_Report.Status.Target_Altitude_Mode_Type.Aircraft_Alt;
                                else if (BO2.DWord[Bit_Ops.Bit8])
                                    CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.Status_Data.Target_Altitude_Source = CAT48I250Types.BDS40_Selected_Vertical_Intention_Report.Status.Target_Altitude_Mode_Type.FCU_MCP_Selected_Alt;
                                else
                                    CAT48I250Data.BDS40_Selected_Vertical_Intention_Report.Status_Data.Target_Altitude_Source = CAT48I250Types.BDS40_Selected_Vertical_Intention_Report.Status.Target_Altitude_Mode_Type.Unknown;
                            }
                            #endregion

                            break;
                        // Track and turn report
                        case "50":

                            // BDS50 is present
                            CAT48I250Data.BDS50_Track_Turn_Report.Present_This_Cycle = true;

                            #region ROLL_ANGLE_REGION
                            // Bit 1 ROLL ANGLE Status
                            CAT48I250Data.BDS50_Track_Turn_Report.Roll_Ang.Is_Valid = BO1.DWord[Bit_Ops.Bit31];

                            // Bits 3 .. 11 ROLL ANGLE Value
                            Value = 0.0;
                            // Bit 2 ROLL ANGLE SIGN
                            if (BO1.DWord[Bit_Ops.Bit30])
                            {
                                Bit_Ops BO1_Temp = new Bit_Ops();
                                BO1_Temp.DWord[Bit_Ops.Bit0] = !BO1.DWord[Bit_Ops.Bit21];
                                BO1_Temp.DWord[Bit_Ops.Bit1] = !BO1.DWord[Bit_Ops.Bit22];
                                BO1_Temp.DWord[Bit_Ops.Bit2] = !BO1.DWord[Bit_Ops.Bit23];
                                BO1_Temp.DWord[Bit_Ops.Bit3] = !BO1.DWord[Bit_Ops.Bit24];
                                BO1_Temp.DWord[Bit_Ops.Bit4] = !BO1.DWord[Bit_Ops.Bit25];
                                BO1_Temp.DWord[Bit_Ops.Bit5] = !BO1.DWord[Bit_Ops.Bit26];
                                BO1_Temp.DWord[Bit_Ops.Bit6] = !BO1.DWord[Bit_Ops.Bit27];
                                BO1_Temp.DWord[Bit_Ops.Bit7] = !BO1.DWord[Bit_Ops.Bit28];
                                BO1_Temp.DWord[Bit_Ops.Bit8] = !BO1.DWord[Bit_Ops.Bit29];
                                BO1_Temp.DWord[Bit_Ops.Bit9] = !BO1.DWord[Bit_Ops.Bit30];
                                BO1_Temp.DWord[Bit_Ops.Bit10] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit11] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit12] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit13] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit14] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit15] = false;

                                BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] = BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] + 1;

                                if (BO1_Temp.DWord[Bit_Ops.Bit0])
                                    Value = ROLL_ANG_1;
                                if (BO1_Temp.DWord[Bit_Ops.Bit1])
                                    Value = Value + ROLL_ANG_2;
                                if (BO1_Temp.DWord[Bit_Ops.Bit2])
                                    Value = Value + ROLL_ANG_3;
                                if (BO1_Temp.DWord[Bit_Ops.Bit3])
                                    Value = Value + ROLL_ANG_4;
                                if (BO1_Temp.DWord[Bit_Ops.Bit4])
                                    Value = Value + ROLL_ANG_5;
                                if (BO1_Temp.DWord[Bit_Ops.Bit5])
                                    Value = Value + ROLL_ANG_6;
                                if (BO1_Temp.DWord[Bit_Ops.Bit6])
                                    Value = Value + ROLL_ANG_7;
                                if (BO1_Temp.DWord[Bit_Ops.Bit7])
                                    Value = Value + ROLL_ANG_8;
                                if (BO1_Temp.DWord[Bit_Ops.Bit8])
                                    Value = Value + ROLL_ANG_9;

                                CAT48I250Data.BDS50_Track_Turn_Report.Roll_Ang.Value = -Value;
                            }
                            else
                            {
                                if (BO1.DWord[Bit_Ops.Bit21])
                                    Value = ROLL_ANG_1;
                                if (BO1.DWord[Bit_Ops.Bit22])
                                    Value = Value + ROLL_ANG_2;
                                if (BO1.DWord[Bit_Ops.Bit23])
                                    Value = Value + ROLL_ANG_3;
                                if (BO1.DWord[Bit_Ops.Bit24])
                                    Value = Value + ROLL_ANG_4;
                                if (BO1.DWord[Bit_Ops.Bit25])
                                    Value = Value + ROLL_ANG_5;
                                if (BO1.DWord[Bit_Ops.Bit26])
                                    Value = Value + ROLL_ANG_6;
                                if (BO1.DWord[Bit_Ops.Bit27])
                                    Value = Value + ROLL_ANG_7;
                                if (BO1.DWord[Bit_Ops.Bit28])
                                    Value = Value + ROLL_ANG_8;
                                if (BO1.DWord[Bit_Ops.Bit29])
                                    Value = Value + ROLL_ANG_9;

                                CAT48I250Data.BDS50_Track_Turn_Report.Roll_Ang.Value = Value;
                            }
                            #endregion
                            #region TRUE_TRACK_ANGLE_REGION
                            // Bit 12 TRUE TRACK ANGLE Status
                            CAT48I250Data.BDS50_Track_Turn_Report.TRUE_TRK.Is_Valid = BO1.DWord[Bit_Ops.Bit20];

                            // Bits 14 .. 23 TRUE TRACK ANGLE Value
                            Value = 0.0;
                            // Bit 13 TRUE TRACK ANGLE SIGN
                            if (BO1.DWord[Bit_Ops.Bit19])
                            {
                                Bit_Ops BO1_Temp = new Bit_Ops();
                                BO1_Temp.DWord[Bit_Ops.Bit0] = !BO1.DWord[Bit_Ops.Bit9];
                                BO1_Temp.DWord[Bit_Ops.Bit1] = !BO1.DWord[Bit_Ops.Bit10];
                                BO1_Temp.DWord[Bit_Ops.Bit2] = !BO1.DWord[Bit_Ops.Bit11];
                                BO1_Temp.DWord[Bit_Ops.Bit3] = !BO1.DWord[Bit_Ops.Bit12];
                                BO1_Temp.DWord[Bit_Ops.Bit4] = !BO1.DWord[Bit_Ops.Bit13];
                                BO1_Temp.DWord[Bit_Ops.Bit5] = !BO1.DWord[Bit_Ops.Bit14];
                                BO1_Temp.DWord[Bit_Ops.Bit6] = !BO1.DWord[Bit_Ops.Bit15];
                                BO1_Temp.DWord[Bit_Ops.Bit7] = !BO1.DWord[Bit_Ops.Bit16];
                                BO1_Temp.DWord[Bit_Ops.Bit8] = !BO1.DWord[Bit_Ops.Bit17];
                                BO1_Temp.DWord[Bit_Ops.Bit9] = !BO1.DWord[Bit_Ops.Bit18];
                                BO1_Temp.DWord[Bit_Ops.Bit10] = !BO1.DWord[Bit_Ops.Bit19];
                                BO1_Temp.DWord[Bit_Ops.Bit11] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit12] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit13] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit14] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit15] = false;

                                BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] = BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] + 1;

                                if (BO1_Temp.DWord[Bit_Ops.Bit0])
                                    Value = TRUE_TRK_ANG_1;
                                if (BO1_Temp.DWord[Bit_Ops.Bit1])
                                    Value = Value + TRUE_TRK_ANG_2;
                                if (BO1_Temp.DWord[Bit_Ops.Bit2])
                                    Value = Value + TRUE_TRK_ANG_3;
                                if (BO1_Temp.DWord[Bit_Ops.Bit3])
                                    Value = Value + TRUE_TRK_ANG_4;
                                if (BO1_Temp.DWord[Bit_Ops.Bit4])
                                    Value = Value + TRUE_TRK_ANG_5;
                                if (BO1_Temp.DWord[Bit_Ops.Bit5])
                                    Value = Value + TRUE_TRK_ANG_6;
                                if (BO1_Temp.DWord[Bit_Ops.Bit6])
                                    Value = Value + TRUE_TRK_ANG_7;
                                if (BO1_Temp.DWord[Bit_Ops.Bit7])
                                    Value = Value + TRUE_TRK_ANG_8;
                                if (BO1_Temp.DWord[Bit_Ops.Bit8])
                                    Value = Value + TRUE_TRK_ANG_9;
                                if (BO1_Temp.DWord[Bit_Ops.Bit9])
                                    Value = Value + TRUE_TRK_ANG_10;

                                CAT48I250Data.BDS50_Track_Turn_Report.TRUE_TRK.Value = 360.0 - Value;
                            }
                            else
                            {
                                if (BO1.DWord[Bit_Ops.Bit9])
                                    Value = TRUE_TRK_ANG_1;
                                if (BO1.DWord[Bit_Ops.Bit10])
                                    Value = Value + TRUE_TRK_ANG_2;
                                if (BO1.DWord[Bit_Ops.Bit11])
                                    Value = Value + TRUE_TRK_ANG_3;
                                if (BO1.DWord[Bit_Ops.Bit12])
                                    Value = Value + TRUE_TRK_ANG_4;
                                if (BO1.DWord[Bit_Ops.Bit13])
                                    Value = Value + TRUE_TRK_ANG_5;
                                if (BO1.DWord[Bit_Ops.Bit14])
                                    Value = Value + TRUE_TRK_ANG_6;
                                if (BO1.DWord[Bit_Ops.Bit15])
                                    Value = Value + TRUE_TRK_ANG_7;
                                if (BO1.DWord[Bit_Ops.Bit16])
                                    Value = Value + TRUE_TRK_ANG_8;
                                if (BO1.DWord[Bit_Ops.Bit17])
                                    Value = Value + TRUE_TRK_ANG_9;
                                if (BO1.DWord[Bit_Ops.Bit18])
                                    Value = Value + TRUE_TRK_ANG_10;

                                CAT48I250Data.BDS50_Track_Turn_Report.TRUE_TRK.Value = Value;
                            }
                            #endregion
                            #region GROUND_SPEED_REGION
                            // Bit 24 GROUND SPEED Status
                            CAT48I250Data.BDS50_Track_Turn_Report.GND_SPD.Is_Valid = BO1.DWord[Bit_Ops.Bit8];

                            // Bits 25 .. 34 GROUND SPEED Value
                            Value = 0.0;

                            if (BO2.DWord[Bit_Ops.Bit30])
                                Value = GND_SPD_1;
                            if (BO2.DWord[Bit_Ops.Bit31])
                                Value = Value + GND_SPD_2;
                            if (BO1.DWord[Bit_Ops.Bit0])
                                Value = Value + GND_SPD_3;
                            if (BO1.DWord[Bit_Ops.Bit1])
                                Value = Value + GND_SPD_4;
                            if (BO1.DWord[Bit_Ops.Bit2])
                                Value = Value + GND_SPD_5;
                            if (BO1.DWord[Bit_Ops.Bit3])
                                Value = Value + GND_SPD_6;
                            if (BO1.DWord[Bit_Ops.Bit4])
                                Value = Value + GND_SPD_7;
                            if (BO1.DWord[Bit_Ops.Bit5])
                                Value = Value + GND_SPD_8;
                            if (BO1.DWord[Bit_Ops.Bit6])
                                Value = Value + GND_SPD_9;
                            if (BO1.DWord[Bit_Ops.Bit7])
                                Value = Value + GND_SPD_10;

                            CAT48I250Data.BDS50_Track_Turn_Report.GND_SPD.Value = Value;
                            #endregion
                            #region TRACK_ANGLE_RATE_REGION
                            // Bit 35 TRACK ANGLE RATE Status
                            CAT48I250Data.BDS50_Track_Turn_Report.TRK_ANG_RATE.Is_Valid = BO2.DWord[Bit_Ops.Bit29];

                            // Bits 37 .. 45 TRACK ANGLE RATE Value
                            Value = 0.0;
                            // Bit 36 TRACK ANGLE RATE SIGN
                            if (BO2.DWord[Bit_Ops.Bit28])
                            {
                                Bit_Ops BO1_Temp = new Bit_Ops();
                                BO1_Temp.DWord[Bit_Ops.Bit0] = !BO2.DWord[Bit_Ops.Bit19];
                                BO1_Temp.DWord[Bit_Ops.Bit1] = !BO2.DWord[Bit_Ops.Bit20];
                                BO1_Temp.DWord[Bit_Ops.Bit2] = !BO2.DWord[Bit_Ops.Bit21];
                                BO1_Temp.DWord[Bit_Ops.Bit3] = !BO2.DWord[Bit_Ops.Bit22];
                                BO1_Temp.DWord[Bit_Ops.Bit4] = !BO2.DWord[Bit_Ops.Bit23];
                                BO1_Temp.DWord[Bit_Ops.Bit5] = !BO2.DWord[Bit_Ops.Bit24];
                                BO1_Temp.DWord[Bit_Ops.Bit6] = !BO2.DWord[Bit_Ops.Bit25];
                                BO1_Temp.DWord[Bit_Ops.Bit7] = !BO2.DWord[Bit_Ops.Bit26];
                                BO1_Temp.DWord[Bit_Ops.Bit8] = !BO2.DWord[Bit_Ops.Bit27];
                                BO1_Temp.DWord[Bit_Ops.Bit9] = !BO2.DWord[Bit_Ops.Bit28];
                                BO1_Temp.DWord[Bit_Ops.Bit10] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit11] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit12] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit13] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit14] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit15] = false;

                                BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] = BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] + 1;

                                if (BO1_Temp.DWord[Bit_Ops.Bit0])
                                    Value = Value + TRK_ANG_RATE_1;
                                if (BO1_Temp.DWord[Bit_Ops.Bit1])
                                    Value = Value + TRK_ANG_RATE_2;
                                if (BO1_Temp.DWord[Bit_Ops.Bit2])
                                    Value = Value + TRK_ANG_RATE_3;
                                if (BO1_Temp.DWord[Bit_Ops.Bit3])
                                    Value = Value + TRK_ANG_RATE_4;
                                if (BO1_Temp.DWord[Bit_Ops.Bit4])
                                    Value = Value + TRK_ANG_RATE_5;
                                if (BO1_Temp.DWord[Bit_Ops.Bit5])
                                    Value = Value + TRK_ANG_RATE_6;
                                if (BO1_Temp.DWord[Bit_Ops.Bit6])
                                    Value = Value + TRK_ANG_RATE_7;
                                if (BO1_Temp.DWord[Bit_Ops.Bit7])
                                    Value = Value + TRK_ANG_RATE_8;
                                if (BO1_Temp.DWord[Bit_Ops.Bit8])
                                    Value = Value + TRK_ANG_RATE_9;

                                CAT48I250Data.BDS50_Track_Turn_Report.TRK_ANG_RATE.Value = -Value;
                            }
                            else
                            {
                                if (BO2.DWord[Bit_Ops.Bit19])
                                    Value = TRK_ANG_RATE_1;
                                if (BO2.DWord[Bit_Ops.Bit20])
                                    Value = Value + TRK_ANG_RATE_2;
                                if (BO2.DWord[Bit_Ops.Bit21])
                                    Value = Value + TRK_ANG_RATE_3;
                                if (BO2.DWord[Bit_Ops.Bit22])
                                    Value = Value + TRK_ANG_RATE_4;
                                if (BO2.DWord[Bit_Ops.Bit23])
                                    Value = Value + TRK_ANG_RATE_5;
                                if (BO2.DWord[Bit_Ops.Bit24])
                                    Value = Value + TRK_ANG_RATE_6;
                                if (BO2.DWord[Bit_Ops.Bit25])
                                    Value = Value + TRK_ANG_RATE_7;
                                if (BO2.DWord[Bit_Ops.Bit26])
                                    Value = Value + TRK_ANG_RATE_8;
                                if (BO2.DWord[Bit_Ops.Bit27])
                                    Value = Value + TRK_ANG_RATE_9;

                                CAT48I250Data.BDS50_Track_Turn_Report.TRK_ANG_RATE.Value = Value;
                            }
                            #endregion
                            #region TRUE_AIRSPEED_REGION
                            // Bit 46 TAS Status
                            CAT48I250Data.BDS50_Track_Turn_Report.TAS.Is_Valid = BO1.DWord[Bit_Ops.Bit18];

                            // Bits 47 .. 56 TAS Value
                            Value = 0.0;

                            if (BO2.DWord[Bit_Ops.Bit8])
                                Value = TAS_1;
                            if (BO2.DWord[Bit_Ops.Bit9])
                                Value = Value + TAS_2;
                            if (BO2.DWord[Bit_Ops.Bit10])
                                Value = Value + TAS_3;
                            if (BO2.DWord[Bit_Ops.Bit11])
                                Value = Value + TAS_4;
                            if (BO2.DWord[Bit_Ops.Bit12])
                                Value = Value + TAS_5;
                            if (BO2.DWord[Bit_Ops.Bit13])
                                Value = Value + TAS_6;
                            if (BO2.DWord[Bit_Ops.Bit14])
                                Value = Value + TAS_7;
                            if (BO2.DWord[Bit_Ops.Bit15])
                                Value = Value + TAS_8;
                            if (BO2.DWord[Bit_Ops.Bit16])
                                Value = Value + TAS_9;
                            if (BO2.DWord[Bit_Ops.Bit17])
                                Value = Value + TAS_10;

                            CAT48I250Data.BDS50_Track_Turn_Report.TAS.Value = (int)Value;
                            #endregion

                            break;
                        // Heading and speed report
                        case "60":

                            // BDS60 is present
                            CAT48I250Data.BDS60_HDG_SPD_Report.Present_This_Cycle = true;

                            #region MAG_HSG_REGION
                            // Bit 1 MAG HDG Status
                            CAT48I250Data.BDS60_HDG_SPD_Report.MAG_HDG.Is_Valid = BO1.DWord[Bit_Ops.Bit31];

                            // Bits 3 .. 12 MAG HDG Value
                            Value = 0.0;
                            // Bit 2 MAG HDG SIGN
                            if (BO1.DWord[Bit_Ops.Bit30])
                            {
                                Bit_Ops BO1_Temp = new Bit_Ops();
                                BO1_Temp.DWord[Bit_Ops.Bit0] = !BO1.DWord[Bit_Ops.Bit20];
                                BO1_Temp.DWord[Bit_Ops.Bit1] = !BO1.DWord[Bit_Ops.Bit21];
                                BO1_Temp.DWord[Bit_Ops.Bit2] = !BO1.DWord[Bit_Ops.Bit22];
                                BO1_Temp.DWord[Bit_Ops.Bit3] = !BO1.DWord[Bit_Ops.Bit23];
                                BO1_Temp.DWord[Bit_Ops.Bit4] = !BO1.DWord[Bit_Ops.Bit24];
                                BO1_Temp.DWord[Bit_Ops.Bit5] = !BO1.DWord[Bit_Ops.Bit25];
                                BO1_Temp.DWord[Bit_Ops.Bit6] = !BO1.DWord[Bit_Ops.Bit26];
                                BO1_Temp.DWord[Bit_Ops.Bit7] = !BO1.DWord[Bit_Ops.Bit27];
                                BO1_Temp.DWord[Bit_Ops.Bit8] = !BO1.DWord[Bit_Ops.Bit28];
                                BO1_Temp.DWord[Bit_Ops.Bit9] = !BO1.DWord[Bit_Ops.Bit29];
                                BO1_Temp.DWord[Bit_Ops.Bit10] = !BO1.DWord[Bit_Ops.Bit30];
                                BO1_Temp.DWord[Bit_Ops.Bit11] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit12] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit13] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit14] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit15] = false;

                                BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] = BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] + 1;

                                if (BO1_Temp.DWord[Bit_Ops.Bit0])
                                    Value = MAG_HDG_1;
                                if (BO1_Temp.DWord[Bit_Ops.Bit1])
                                    Value = Value + MAG_HDG_2;
                                if (BO1_Temp.DWord[Bit_Ops.Bit2])
                                    Value = Value + MAG_HDG_3;
                                if (BO1_Temp.DWord[Bit_Ops.Bit3])
                                    Value = Value + MAG_HDG_4;
                                if (BO1_Temp.DWord[Bit_Ops.Bit4])
                                    Value = Value + MAG_HDG_5;
                                if (BO1_Temp.DWord[Bit_Ops.Bit5])
                                    Value = Value + MAG_HDG_6;
                                if (BO1_Temp.DWord[Bit_Ops.Bit6])
                                    Value = Value + MAG_HDG_7;
                                if (BO1_Temp.DWord[Bit_Ops.Bit7])
                                    Value = Value + MAG_HDG_8;
                                if (BO1_Temp.DWord[Bit_Ops.Bit8])
                                    Value = Value + MAG_HDG_9;
                                if (BO1_Temp.DWord[Bit_Ops.Bit9])
                                    Value = Value + MAG_HDG_10;

                                CAT48I250Data.BDS60_HDG_SPD_Report.MAG_HDG.Value = 360.0 - Value;
                            }
                            else
                            {
                                if (BO1.DWord[Bit_Ops.Bit20])
                                    Value = MAG_HDG_1;
                                if (BO1.DWord[Bit_Ops.Bit21])
                                    Value = Value + MAG_HDG_2;
                                if (BO1.DWord[Bit_Ops.Bit22])
                                    Value = Value + MAG_HDG_3;
                                if (BO1.DWord[Bit_Ops.Bit23])
                                    Value = Value + MAG_HDG_4;
                                if (BO1.DWord[Bit_Ops.Bit24])
                                    Value = Value + MAG_HDG_5;
                                if (BO1.DWord[Bit_Ops.Bit25])
                                    Value = Value + MAG_HDG_6;
                                if (BO1.DWord[Bit_Ops.Bit26])
                                    Value = Value + MAG_HDG_7;
                                if (BO1.DWord[Bit_Ops.Bit27])
                                    Value = Value + MAG_HDG_8;
                                if (BO1.DWord[Bit_Ops.Bit28])
                                    Value = Value + MAG_HDG_9;
                                if (BO1.DWord[Bit_Ops.Bit29])
                                    Value = Value + MAG_HDG_10;

                                CAT48I250Data.BDS60_HDG_SPD_Report.MAG_HDG.Value = Value;
                            }
                            #endregion
                            #region IAS_REGION
                            // BIT 13 IAS STATUS
                            CAT48I250Data.BDS60_HDG_SPD_Report.IAS.Is_Valid = BO1.DWord[Bit_Ops.Bit19];
                            // BITS 14 .. 23 IAS VALUE
                            Value = 0.0;
                            if (BO1.DWord[Bit_Ops.Bit9])
                                Value = IAS_1;
                            if (BO1.DWord[Bit_Ops.Bit10])
                                Value = Value + IAS_2;
                            if (BO1.DWord[Bit_Ops.Bit11])
                                Value = Value + IAS_3;
                            if (BO1.DWord[Bit_Ops.Bit12])
                                Value = Value + IAS_4;
                            if (BO1.DWord[Bit_Ops.Bit13])
                                Value = Value + IAS_5;
                            if (BO1.DWord[Bit_Ops.Bit14])
                                Value = Value + IAS_6;
                            if (BO1.DWord[Bit_Ops.Bit15])
                                Value = Value + IAS_7;
                            if (BO1.DWord[Bit_Ops.Bit16])
                                Value = Value + IAS_8;
                            if (BO1.DWord[Bit_Ops.Bit17])
                                Value = Value + IAS_9;
                            if (BO1.DWord[Bit_Ops.Bit18])
                                Value = Value + IAS_10;
                            // Assign new IAS value
                            CAT48I250Data.BDS60_HDG_SPD_Report.IAS.Value = (int)Value;
                            #endregion
                            #region MACH_REGION
                            // BIT 24 MACH STATUS
                            CAT48I250Data.BDS60_HDG_SPD_Report.MACH.Is_Valid = BO1.DWord[Bit_Ops.Bit8];
                            // BITS 25 .. 34 MACH VALUE
                            Value = 0.0;
                            if (BO2.DWord[Bit_Ops.Bit30])
                                Value = MACH_1;
                            if (BO2.DWord[Bit_Ops.Bit31])
                                Value = Value + MACH_2;
                            if (BO1.DWord[Bit_Ops.Bit0])
                                Value = Value + MACH_3;
                            if (BO1.DWord[Bit_Ops.Bit1])
                                Value = Value + MACH_4;
                            if (BO1.DWord[Bit_Ops.Bit2])
                                Value = Value + MACH_5;
                            if (BO1.DWord[Bit_Ops.Bit3])
                                Value = Value + MACH_6;
                            if (BO1.DWord[Bit_Ops.Bit4])
                                Value = Value + MACH_7;
                            if (BO1.DWord[Bit_Ops.Bit5])
                                Value = Value + MACH_8;
                            if (BO1.DWord[Bit_Ops.Bit6])
                                Value = Value + MACH_9;
                            if (BO1.DWord[Bit_Ops.Bit7])
                                Value = Value + MACH_10;
                            // Assign new MACH value
                            CAT48I250Data.BDS60_HDG_SPD_Report.MACH.Value = Value;
                            #endregion
                            #region BARO_ALT_RATE
                            // BIT 35 BARO ALT RATE STATUS
                            CAT48I250Data.BDS60_HDG_SPD_Report.Baro_RoC.Is_Valid = BO2.DWord[Bit_Ops.Bit29];
                            // BITS 37 .. 45 BARO ALT RATE VALUE
                            Value = 0.0;

                            // Bit 36 BARO ALT RATE SIGN
                            if (BO2.DWord[Bit_Ops.Bit28])
                            {
                                Bit_Ops BO1_Temp = new Bit_Ops();
                                BO1_Temp.DWord[Bit_Ops.Bit0] = !BO2.DWord[Bit_Ops.Bit19];
                                BO1_Temp.DWord[Bit_Ops.Bit1] = !BO2.DWord[Bit_Ops.Bit20];
                                BO1_Temp.DWord[Bit_Ops.Bit2] = !BO2.DWord[Bit_Ops.Bit21];
                                BO1_Temp.DWord[Bit_Ops.Bit3] = !BO2.DWord[Bit_Ops.Bit22];
                                BO1_Temp.DWord[Bit_Ops.Bit4] = !BO2.DWord[Bit_Ops.Bit23];
                                BO1_Temp.DWord[Bit_Ops.Bit5] = !BO2.DWord[Bit_Ops.Bit24];
                                BO1_Temp.DWord[Bit_Ops.Bit6] = !BO2.DWord[Bit_Ops.Bit25];
                                BO1_Temp.DWord[Bit_Ops.Bit7] = !BO2.DWord[Bit_Ops.Bit26];
                                BO1_Temp.DWord[Bit_Ops.Bit8] = !BO2.DWord[Bit_Ops.Bit27];
                                BO1_Temp.DWord[Bit_Ops.Bit9] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit10] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit11] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit12] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit13] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit14] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit15] = false;

                                if (BO1_Temp.DWord[Bit_Ops.Bit0])
                                    Value = RoC_1;
                                if (BO1_Temp.DWord[Bit_Ops.Bit1])
                                    Value = Value + RoC_2;
                                if (BO1_Temp.DWord[Bit_Ops.Bit2])
                                    Value = Value + RoC_3;
                                if (BO1_Temp.DWord[Bit_Ops.Bit3])
                                    Value = Value + RoC_4;
                                if (BO1_Temp.DWord[Bit_Ops.Bit4])
                                    Value = Value + RoC_5;
                                if (BO1_Temp.DWord[Bit_Ops.Bit5])
                                    Value = Value + RoC_6;
                                if (BO1_Temp.DWord[Bit_Ops.Bit6])
                                    Value = Value + RoC_7;
                                if (BO1_Temp.DWord[Bit_Ops.Bit7])
                                    Value = Value + RoC_8;
                                if (BO1_Temp.DWord[Bit_Ops.Bit8])
                                    Value = Value + RoC_9;

                                BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] = BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] + 1;

                                CAT48I250Data.BDS60_HDG_SPD_Report.Baro_RoC.Value = -(int)Value;
                            }
                            else
                            {
                                if (BO2.DWord[Bit_Ops.Bit19])
                                    Value = RoC_1;
                                if (BO2.DWord[Bit_Ops.Bit20])
                                    Value = Value + RoC_2;
                                if (BO2.DWord[Bit_Ops.Bit21])
                                    Value = Value + RoC_3;
                                if (BO2.DWord[Bit_Ops.Bit22])
                                    Value = Value + RoC_4;
                                if (BO2.DWord[Bit_Ops.Bit23])
                                    Value = Value + RoC_5;
                                if (BO2.DWord[Bit_Ops.Bit24])
                                    Value = Value + RoC_6;
                                if (BO2.DWord[Bit_Ops.Bit25])
                                    Value = Value + RoC_7;
                                if (BO2.DWord[Bit_Ops.Bit26])
                                    Value = Value + RoC_8;
                                if (BO2.DWord[Bit_Ops.Bit27])
                                    Value = Value + RoC_9;

                                CAT48I250Data.BDS60_HDG_SPD_Report.Baro_RoC.Value = (int)Value;
                            }
                            #endregion
                            #region INERTIAL_ALT_RATE
                            // BIT 48 BARO ALT RATE STATUS
                            CAT48I250Data.BDS60_HDG_SPD_Report.Inertial_RoC.Is_Valid = BO2.DWord[Bit_Ops.Bit18];
                            // BITS 47 .. 56 BARO ALT RATE VALUE
                            Value = 0.0;

                            // Bit 36 BARO ALT RATE SIGN
                            if (BO2.DWord[Bit_Ops.Bit17])
                            {
                                Bit_Ops BO1_Temp = new Bit_Ops();
                                BO1_Temp.DWord[Bit_Ops.Bit0] = !BO2.DWord[Bit_Ops.Bit8];
                                BO1_Temp.DWord[Bit_Ops.Bit1] = !BO2.DWord[Bit_Ops.Bit9];
                                BO1_Temp.DWord[Bit_Ops.Bit2] = !BO2.DWord[Bit_Ops.Bit10];
                                BO1_Temp.DWord[Bit_Ops.Bit3] = !BO2.DWord[Bit_Ops.Bit11];
                                BO1_Temp.DWord[Bit_Ops.Bit4] = !BO2.DWord[Bit_Ops.Bit12];
                                BO1_Temp.DWord[Bit_Ops.Bit5] = !BO2.DWord[Bit_Ops.Bit13];
                                BO1_Temp.DWord[Bit_Ops.Bit6] = !BO2.DWord[Bit_Ops.Bit14];
                                BO1_Temp.DWord[Bit_Ops.Bit7] = !BO2.DWord[Bit_Ops.Bit15];
                                BO1_Temp.DWord[Bit_Ops.Bit8] = !BO2.DWord[Bit_Ops.Bit16];
                                BO1_Temp.DWord[Bit_Ops.Bit9] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit10] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit11] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit12] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit13] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit14] = false;
                                BO1_Temp.DWord[Bit_Ops.Bit15] = false;

                                if (BO1_Temp.DWord[Bit_Ops.Bit0])
                                    Value = RoC_1;
                                if (BO1_Temp.DWord[Bit_Ops.Bit1])
                                    Value = Value + RoC_2;
                                if (BO1_Temp.DWord[Bit_Ops.Bit2])
                                    Value = Value + RoC_3;
                                if (BO1_Temp.DWord[Bit_Ops.Bit3])
                                    Value = Value + RoC_4;
                                if (BO1_Temp.DWord[Bit_Ops.Bit4])
                                    Value = Value + RoC_5;
                                if (BO1_Temp.DWord[Bit_Ops.Bit5])
                                    Value = Value + RoC_6;
                                if (BO1_Temp.DWord[Bit_Ops.Bit6])
                                    Value = Value + RoC_7;
                                if (BO1_Temp.DWord[Bit_Ops.Bit7])
                                    Value = Value + RoC_8;
                                if (BO1_Temp.DWord[Bit_Ops.Bit8])
                                    Value = Value + RoC_9;

                                BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] = BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] + 1;

                                CAT48I250Data.BDS60_HDG_SPD_Report.Inertial_RoC.Value = -(int)Value;
                            }
                            else
                            {
                                if (BO2.DWord[Bit_Ops.Bit8])
                                    Value = RoC_1;
                                if (BO2.DWord[Bit_Ops.Bit9])
                                    Value = Value + RoC_2;
                                if (BO2.DWord[Bit_Ops.Bit10])
                                    Value = Value + RoC_3;
                                if (BO2.DWord[Bit_Ops.Bit11])
                                    Value = Value + RoC_4;
                                if (BO2.DWord[Bit_Ops.Bit12])
                                    Value = Value + RoC_5;
                                if (BO2.DWord[Bit_Ops.Bit13])
                                    Value = Value + RoC_6;
                                if (BO2.DWord[Bit_Ops.Bit14])
                                    Value = Value + RoC_7;
                                if (BO2.DWord[Bit_Ops.Bit15])
                                    Value = Value + RoC_8;
                                if (BO2.DWord[Bit_Ops.Bit16])
                                    Value = Value + RoC_9;

                                CAT48I250Data.BDS60_HDG_SPD_Report.Inertial_RoC.Value = (int)Value;
                            }
                            #endregion

                            break;
                    #endregion
                    }
                }
            }

            // Increase data buffer index so it ready for the next data item.
            CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + (8 * Repetative_Factor);

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT48.I048DataItems[CAT48.ItemIDToIndex("250")].value = CAT48I250Data;
            //////////////////////////////////////////////////////////////////////////////////
        }
    }
}
