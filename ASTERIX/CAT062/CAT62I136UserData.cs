using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I136UserData
    {
        public static void DecodeCAT62I136(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

            double MeasuredFlightLevel = DecodeFlightLevel(BO);

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT62.I062DataItems[CAT62.ItemIDToIndex("136")].value = MeasuredFlightLevel;
            //////////////////////////////////////////////////////////////////////////////////
            
            // Increase data buffer index so it ready for the next data item.
            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
        }

        static double DecodeFlightLevel(Bit_Ops BO_In)
        {
            double Result = 0.0;

            // First check if this is a negative altitude.
            // and then handle it properly
            if (BO_In.DWord[Bit_Ops.Bit15] == true)
            {
                // Do not worry about negative values for now.
                Result = -1.0;
            }
            else
            { // A positive value

                if (BO_In.DWord[Bit_Ops.Bit0] == true)
                    Result = Result + 0.25;
                if (BO_In.DWord[Bit_Ops.Bit1] == true)
                    Result = Result + 0.50;
                if (BO_In.DWord[Bit_Ops.Bit2] == true)
                    Result = Result + 1.00;
                if (BO_In.DWord[Bit_Ops.Bit3] == true)
                    Result = Result + 2.00;
                if (BO_In.DWord[Bit_Ops.Bit4] == true)
                    Result = Result + 4.00;
                if (BO_In.DWord[Bit_Ops.Bit5] == true)
                    Result = Result + 8.00;
                if (BO_In.DWord[Bit_Ops.Bit6] == true)
                    Result = Result + 16.00;
                if (BO_In.DWord[Bit_Ops.Bit7] == true)
                    Result = Result + 32.00;
                if (BO_In.DWord[Bit_Ops.Bit8] == true)
                    Result = Result + 64.00;
                if (BO_In.DWord[Bit_Ops.Bit9] == true)
                    Result = Result + 128.00;
                if (BO_In.DWord[Bit_Ops.Bit10] == true)
                    Result = Result + 256.00;
                if (BO_In.DWord[Bit_Ops.Bit11] == true)
                    Result = Result + 512.00;
                if (BO_In.DWord[Bit_Ops.Bit12] == true)
                    Result = Result + 1024.00;
            }

            return Result;
        }
    }
}
