using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT34I030UserData
    {
        ///////////////////////////////////////////////////////////////////
        // Define time of the day stamping expressed as UTC time
        // where LSB = 1/128s 
        ///////////////////////////////////////////////////////////////////
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
        private const double LSB_17 = LSB_16 * 2.0;
        private const double LSB_18 = LSB_17 * 2.0;
        private const double LSB_19 = LSB_18 * 2.0;
        private const double LSB_20 = LSB_19 * 2.0;
        private const double LSB_21 = LSB_20 * 2.0;
        private const double LSB_22 = LSB_21 * 2.0;
        private const double LSB_23 = LSB_22 * 2.0;
        private const double LSB_24 = LSB_23 * 2.0;

        public static void DecodeCAT34I030(byte[] Data)
        {

             CAT34I030Types.CAT34I030_Time_Of_The_Day_User_Type MyCAT34I030 = new CAT34I030Types.CAT34I030_Time_Of_The_Day_User_Type();
            
            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            //Extract the first octet
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + 3];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + 2];
            BO.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + 1];

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
            if (BO.DWord[Bit_Ops.Bit16] == true)
                Result = Result + LSB_17;
            if (BO.DWord[Bit_Ops.Bit17] == true)
                Result = Result + LSB_18;
            if (BO.DWord[Bit_Ops.Bit18] == true)
                Result = Result + LSB_19;
            if (BO.DWord[Bit_Ops.Bit19] == true)
                Result = Result + LSB_20;
            if (BO.DWord[Bit_Ops.Bit20] == true)
                Result = Result + LSB_21;
            if (BO.DWord[Bit_Ops.Bit21] == true)
                Result = Result + LSB_22;
            if (BO.DWord[Bit_Ops.Bit22] == true)
                Result = Result + LSB_23;
            if (BO.DWord[Bit_Ops.Bit23] == true)
                Result = Result + LSB_24;

            MyCAT34I030.Time_Of_The_Day_In_Sec_Since_Midnight_UTC = Result;

            // Compute the time in a human friendy form
            // from sec.miliec since midnight UTC.
            double hrs, sec, min, milis;
            hrs = Math.Floor(Result / 3600.00);
            double tmp = (Result - (hrs * 3600.0)) / 60;
            min = Math.Floor(tmp);
            tmp = (tmp * 60.0) - (min * 60.0);
            sec = Math.Floor(tmp);
            milis = (tmp - sec) * 1000.0;

            MyCAT34I030.TimeOfDay = new TimeSpan(0, (int)hrs, (int)min, (int)sec, (int)milis);
            
            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT34.I034DataItems[CAT34.ItemIDToIndex("030")].value = MyCAT34I030;
            //////////////////////////////////////////////////////////////////////////////////

            // Leave it at the current index for the next decode
            CAT34.CurrentDataBufferOctalIndex = CAT34.CurrentDataBufferOctalIndex + 3;

        }
    }

}
