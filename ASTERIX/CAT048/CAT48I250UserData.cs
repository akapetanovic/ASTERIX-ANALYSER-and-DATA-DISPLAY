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

                            break;
                        // Track and turn report
                        case "50":



                            break;
                        // Heading and speed report
                        case "60":

                            // BDS60 is present
                            CAT48I250Data.BDS60_HDG_SPD_Report.Present_This_Cycle = true;

                            #region MAG_HSG_REGION
                            // Bit 1 MAG HDG Status
                            CAT48I250Data.BDS60_HDG_SPD_Report.MAG_HDG.Is_Valid = BO1.DWord[Bit_Ops.Bit31];

                            // Bits 3 .. 12 MAG HDG Value
                            double Value = 0.0;
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
