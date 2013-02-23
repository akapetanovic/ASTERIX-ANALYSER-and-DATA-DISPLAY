using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I185UserData
    {

        /////////////////////////////////////////////////////////////////
        /// Define calculated Track velocity (Carteisan)
        /// LSB = 0.25m/s
        /// -8192m/s...8191.75m/s
        /// Two's complement form
        /////////////////////////////////////////////////////////////////
        private const double V_1 = 0.25; 
        private const double V_2 = V_1 * 2.0;
        private const double V_3 = V_2 * 2.0;
        private const double V_4 = V_3 * 2.0;
        private const double V_5 = V_4 * 2.0;
        private const double V_6 = V_5 * 2.0;
        private const double V_7 = V_6 * 2.0;
        private const double V_8 = V_7 * 2.0;
        private const double V_9 = V_8 * 2.0;
        private const double V_10 = V_9 * 2.0;
        private const double V_11 = V_10 * 2.0;
        private const double V_12 = V_11 * 2.0;
        private const double V_13 = V_12 * 2.0;
        private const double V_14 = V_13 * 2.0;
        private const double V_15 = V_14 * 2.0;
        private const double V_16 = V_15 * 2.0;

        public static void DecodeCAT62I185(byte[] Data)
        {
            CAT62I185Types.CalculatedGSPandHDG_Type CalculatedGSPDandHDG = new CAT62I185Types.CalculatedGSPandHDG_Type();

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 3];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 2];
            BO.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];
            BO.DWord[Bit_Ops.Bits24_31_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

            double Vx = 0.0;
            double Vy = 0.0;
            ///////////////////////////////////////////////////////////////////////////////////////
            // Decode Vx
            ///////////////////////////////////////////////////////////////////////////////////////
            if (BO.DWord[Bit_Ops.Bit31] == true)
            {
                Bit_Ops BO1_Temp = new Bit_Ops();

                BO1_Temp.DWord[Bit_Ops.Bit0] = !BO.DWord[Bit_Ops.Bit16];
                BO1_Temp.DWord[Bit_Ops.Bit1] = !BO.DWord[Bit_Ops.Bit17];
                BO1_Temp.DWord[Bit_Ops.Bit2] = !BO.DWord[Bit_Ops.Bit18];
                BO1_Temp.DWord[Bit_Ops.Bit3] = !BO.DWord[Bit_Ops.Bit19];
                BO1_Temp.DWord[Bit_Ops.Bit4] = !BO.DWord[Bit_Ops.Bit20];
                BO1_Temp.DWord[Bit_Ops.Bit5] = !BO.DWord[Bit_Ops.Bit21];
                BO1_Temp.DWord[Bit_Ops.Bit6] = !BO.DWord[Bit_Ops.Bit22];
                BO1_Temp.DWord[Bit_Ops.Bit7] = !BO.DWord[Bit_Ops.Bit23];
                BO1_Temp.DWord[Bit_Ops.Bit8] = !BO.DWord[Bit_Ops.Bit24];
                BO1_Temp.DWord[Bit_Ops.Bit9] = !BO.DWord[Bit_Ops.Bit25];
                BO1_Temp.DWord[Bit_Ops.Bit10] = !BO.DWord[Bit_Ops.Bit26];
                BO1_Temp.DWord[Bit_Ops.Bit11] = !BO.DWord[Bit_Ops.Bit27];
                BO1_Temp.DWord[Bit_Ops.Bit12] = !BO.DWord[Bit_Ops.Bit28];
                BO1_Temp.DWord[Bit_Ops.Bit13] = !BO.DWord[Bit_Ops.Bit29];
                BO1_Temp.DWord[Bit_Ops.Bit14] = !BO.DWord[Bit_Ops.Bit30];
                BO1_Temp.DWord[Bit_Ops.Bit15] = !BO.DWord[Bit_Ops.Bit31];
                
                BO1_Temp.DWord[Bit_Ops.Bit16] = false;
                BO1_Temp.DWord[Bit_Ops.Bit17] = false;
                BO1_Temp.DWord[Bit_Ops.Bit18] = false;
                BO1_Temp.DWord[Bit_Ops.Bit19] = false;
                BO1_Temp.DWord[Bit_Ops.Bit20] = false;
                BO1_Temp.DWord[Bit_Ops.Bit21] = false;
                BO1_Temp.DWord[Bit_Ops.Bit22] = false;
                BO1_Temp.DWord[Bit_Ops.Bit23] = false;
                BO1_Temp.DWord[Bit_Ops.Bit24] = false;
                BO1_Temp.DWord[Bit_Ops.Bit25] = false;
                BO1_Temp.DWord[Bit_Ops.Bit26] = false;
                BO1_Temp.DWord[Bit_Ops.Bit27] = false;
                BO1_Temp.DWord[Bit_Ops.Bit28] = false;
                BO1_Temp.DWord[Bit_Ops.Bit29] = false;
                BO1_Temp.DWord[Bit_Ops.Bit30] = false;
                BO1_Temp.DWord[Bit_Ops.Bit31] = false;

                BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] = BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] + 1;

                if (BO1_Temp.DWord[Bit_Ops.Bit0] == true)
                    Vx = V_1;
                if (BO1_Temp.DWord[Bit_Ops.Bit1] == true)
                    Vx = Vx + V_2;
                if (BO1_Temp.DWord[Bit_Ops.Bit2] == true)
                    Vx = Vx + V_3;
                if (BO1_Temp.DWord[Bit_Ops.Bit3] == true)
                    Vx = Vx + V_4;
                if (BO1_Temp.DWord[Bit_Ops.Bit4] == true)
                    Vx = Vx + V_5;
                if (BO1_Temp.DWord[Bit_Ops.Bit5] == true)
                    Vx = Vx + V_6;
                if (BO1_Temp.DWord[Bit_Ops.Bit6] == true)
                    Vx = Vx + V_7;
                if (BO1_Temp.DWord[Bit_Ops.Bit7] == true)
                    Vx = Vx + V_8;
                if (BO1_Temp.DWord[Bit_Ops.Bit8] == true)
                    Vx = Vx + V_9;
                if (BO1_Temp.DWord[Bit_Ops.Bit9] == true)
                    Vx = Vx + V_10;
                if (BO1_Temp.DWord[Bit_Ops.Bit10] == true)
                    Vx = Vx + V_11;
                if (BO1_Temp.DWord[Bit_Ops.Bit11] == true)
                    Vx = Vx + V_12;
                if (BO1_Temp.DWord[Bit_Ops.Bit12] == true)
                    Vx = Vx + V_13;
                if (BO1_Temp.DWord[Bit_Ops.Bit13] == true)
                    Vx = Vx + V_14;
                if (BO1_Temp.DWord[Bit_Ops.Bit14] == true)
                    Vx = Vx + V_15;
                if (BO1_Temp.DWord[Bit_Ops.Bit15] == true)
                    Vx = Vx + V_16;

                Vx = -Vx;
            }
            else
            {
                if (BO.DWord[Bit_Ops.Bit16] == true)
                    Vx = V_1;
                if (BO.DWord[Bit_Ops.Bit17] == true)
                    Vx = Vx + V_2;
                if (BO.DWord[Bit_Ops.Bit18] == true)
                    Vx = Vx + V_3;
                if (BO.DWord[Bit_Ops.Bit19] == true)
                    Vx = Vx + V_4;
                if (BO.DWord[Bit_Ops.Bit20] == true)
                    Vx = Vx + V_5;
                if (BO.DWord[Bit_Ops.Bit21] == true)
                    Vx = Vx + V_6;
                if (BO.DWord[Bit_Ops.Bit22] == true)
                    Vx = Vx + V_7;
                if (BO.DWord[Bit_Ops.Bit23] == true)
                    Vx = Vx + V_8;
                if (BO.DWord[Bit_Ops.Bit24] == true)
                    Vx = Vx + V_9;
                if (BO.DWord[Bit_Ops.Bit25] == true)
                    Vx = Vx + V_10;
                if (BO.DWord[Bit_Ops.Bit26] == true)
                    Vx = Vx + V_11;
                if (BO.DWord[Bit_Ops.Bit27] == true)
                    Vx = Vx + V_12;
                if (BO.DWord[Bit_Ops.Bit28] == true)
                    Vx = Vx + V_13;
                if (BO.DWord[Bit_Ops.Bit29] == true)
                    Vx = Vx + V_14;
                if (BO.DWord[Bit_Ops.Bit30] == true)
                    Vx = Vx + V_15;
                if (BO.DWord[Bit_Ops.Bit31] == true)
                    Vx = Vx + V_16;
            }

            ///////////////////////////////////////////////////////////////////////////////////////
            // Decode Vy
            ///////////////////////////////////////////////////////////////////////////////////////
            if (BO.DWord[Bit_Ops.Bit15] == true)
            {
                Bit_Ops BO1_Temp = new Bit_Ops();

                BO1_Temp.DWord[Bit_Ops.Bit0] = !BO.DWord[Bit_Ops.Bit0];
                BO1_Temp.DWord[Bit_Ops.Bit1] = !BO.DWord[Bit_Ops.Bit1];
                BO1_Temp.DWord[Bit_Ops.Bit2] = !BO.DWord[Bit_Ops.Bit2];
                BO1_Temp.DWord[Bit_Ops.Bit3] = !BO.DWord[Bit_Ops.Bit3];
                BO1_Temp.DWord[Bit_Ops.Bit4] = !BO.DWord[Bit_Ops.Bit4];
                BO1_Temp.DWord[Bit_Ops.Bit5] = !BO.DWord[Bit_Ops.Bit5];
                BO1_Temp.DWord[Bit_Ops.Bit6] = !BO.DWord[Bit_Ops.Bit6];
                BO1_Temp.DWord[Bit_Ops.Bit7] = !BO.DWord[Bit_Ops.Bit7];
                BO1_Temp.DWord[Bit_Ops.Bit8] = !BO.DWord[Bit_Ops.Bit8];
                BO1_Temp.DWord[Bit_Ops.Bit9] = !BO.DWord[Bit_Ops.Bit9];
                BO1_Temp.DWord[Bit_Ops.Bit10] = !BO.DWord[Bit_Ops.Bit10];
                BO1_Temp.DWord[Bit_Ops.Bit11] = !BO.DWord[Bit_Ops.Bit11];
                BO1_Temp.DWord[Bit_Ops.Bit12] = !BO.DWord[Bit_Ops.Bit12];
                BO1_Temp.DWord[Bit_Ops.Bit13] = !BO.DWord[Bit_Ops.Bit13];
                BO1_Temp.DWord[Bit_Ops.Bit14] = !BO.DWord[Bit_Ops.Bit14];
                BO1_Temp.DWord[Bit_Ops.Bit15] = !BO.DWord[Bit_Ops.Bit15];

                BO1_Temp.DWord[Bit_Ops.Bit16] = false;
                BO1_Temp.DWord[Bit_Ops.Bit17] = false;
                BO1_Temp.DWord[Bit_Ops.Bit18] = false;
                BO1_Temp.DWord[Bit_Ops.Bit19] = false;
                BO1_Temp.DWord[Bit_Ops.Bit20] = false;
                BO1_Temp.DWord[Bit_Ops.Bit21] = false;
                BO1_Temp.DWord[Bit_Ops.Bit22] = false;
                BO1_Temp.DWord[Bit_Ops.Bit23] = false;
                BO1_Temp.DWord[Bit_Ops.Bit24] = false;
                BO1_Temp.DWord[Bit_Ops.Bit25] = false;
                BO1_Temp.DWord[Bit_Ops.Bit26] = false;
                BO1_Temp.DWord[Bit_Ops.Bit27] = false;
                BO1_Temp.DWord[Bit_Ops.Bit28] = false;
                BO1_Temp.DWord[Bit_Ops.Bit29] = false;
                BO1_Temp.DWord[Bit_Ops.Bit30] = false;
                BO1_Temp.DWord[Bit_Ops.Bit31] = false;

                BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] = BO1_Temp.DWord[Bit_Ops.Bits0_15_Of_DWord] + 1;

                if (BO1_Temp.DWord[Bit_Ops.Bit0] == true)
                    Vy = V_1;
                if (BO1_Temp.DWord[Bit_Ops.Bit1] == true)
                    Vy = Vy + V_2;
                if (BO1_Temp.DWord[Bit_Ops.Bit2] == true)
                    Vy = Vy + V_3;
                if (BO1_Temp.DWord[Bit_Ops.Bit3] == true)
                    Vy = Vy + V_4;
                if (BO1_Temp.DWord[Bit_Ops.Bit4] == true)
                    Vy = Vy + V_5;
                if (BO1_Temp.DWord[Bit_Ops.Bit5] == true)
                    Vy = Vy + V_6;
                if (BO1_Temp.DWord[Bit_Ops.Bit6] == true)
                    Vy = Vy + V_7;
                if (BO1_Temp.DWord[Bit_Ops.Bit7] == true)
                    Vy = Vy + V_8;
                if (BO1_Temp.DWord[Bit_Ops.Bit8] == true)
                    Vy = Vy + V_9;
                if (BO1_Temp.DWord[Bit_Ops.Bit9] == true)
                    Vy = Vy + V_10;
                if (BO1_Temp.DWord[Bit_Ops.Bit10] == true)
                    Vy = Vy + V_11;
                if (BO1_Temp.DWord[Bit_Ops.Bit11] == true)
                    Vy = Vy + V_12;
                if (BO1_Temp.DWord[Bit_Ops.Bit12] == true)
                    Vy = Vy + V_13;
                if (BO1_Temp.DWord[Bit_Ops.Bit13] == true)
                    Vy = Vy + V_14;
                if (BO1_Temp.DWord[Bit_Ops.Bit14] == true)
                    Vy = Vy + V_15;
                if (BO1_Temp.DWord[Bit_Ops.Bit15] == true)
                    Vy = Vy + V_16;

                Vy = -Vy;
            }
            else
            {
                if (BO.DWord[Bit_Ops.Bit0] == true)
                    Vy = V_1;
                if (BO.DWord[Bit_Ops.Bit1] == true)
                    Vy = Vy + V_2;
                if (BO.DWord[Bit_Ops.Bit2] == true)
                    Vy = Vy + V_3;
                if (BO.DWord[Bit_Ops.Bit3] == true)
                    Vy = Vy + V_4;
                if (BO.DWord[Bit_Ops.Bit4] == true)
                    Vy = Vy + V_5;
                if (BO.DWord[Bit_Ops.Bit5] == true)
                    Vy = Vy + V_6;
                if (BO.DWord[Bit_Ops.Bit6] == true)
                    Vy = Vy + V_7;
                if (BO.DWord[Bit_Ops.Bit7] == true)
                    Vy = Vy + V_8;
                if (BO.DWord[Bit_Ops.Bit8] == true)
                    Vy = Vy + V_9;
                if (BO.DWord[Bit_Ops.Bit9] == true)
                    Vy = Vy + V_10;
                if (BO.DWord[Bit_Ops.Bit10] == true)
                    Vy = Vy + V_11;
                if (BO.DWord[Bit_Ops.Bit11] == true)
                    Vy = Vy + V_12;
                if (BO.DWord[Bit_Ops.Bit12] == true)
                    Vy = Vy + V_13;
                if (BO.DWord[Bit_Ops.Bit13] == true)
                    Vy = Vy + V_14;
                if (BO.DWord[Bit_Ops.Bit14] == true)
                    Vy = Vy + V_15;
                if (BO.DWord[Bit_Ops.Bit15] == true)
                    Vy = Vy + V_16;
            }

            CalculatedGSPDandHDG = ToPolarFromCarteisan(Vx, Vy);

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT62.I062DataItems[CAT62.ItemIDToIndex("185")].value = CalculatedGSPDandHDG;
            //////////////////////////////////////////////////////////////////////////////////

            // Increase data buffer index so it ready for the next data item.
            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 4;
        }

        public static CAT62I185Types.CalculatedGSPandHDG_Type ToPolarFromCarteisan(double Vx, double Vy)
        {
            CAT62I185Types.CalculatedGSPandHDG_Type ReturnValue = new CAT62I185Types.CalculatedGSPandHDG_Type();
            ReturnValue.GSPD = Math.Pow((Math.Pow(Vx, 2) + Math.Pow(Vy, 2)), 0.5);
            ReturnValue.HDG = (Math.Atan2(Vx, Vy) * (180.0 / Math.PI));
            ReturnValue.Is_Valid = true;
            return ReturnValue;
        }
    }

}
