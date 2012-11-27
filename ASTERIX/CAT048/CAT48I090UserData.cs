using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT48I090UserData
    {
        public static void DecodeCAT48I090(byte[] Data)
        {

            // A new instance of the CAT48I070 data
            CAT48I090Types.CAT48I090FlightLevelUserData MyCAT48I090 = new CAT48I090Types.CAT48I090FlightLevelUserData();

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 2];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 1];

            //////////////////////////////////////////////////////////////////////////////////
            // Decode Code validation
            if (BO.DWord[Bit_Ops.Bit15] == true)
                MyCAT48I090.Code_Validated = CAT48I090Types.Code_Validation_Type.Code_Not_Validated;
            else
                MyCAT48I090.Code_Validated = CAT48I090Types.Code_Validation_Type.Code_Validated;

            //////////////////////////////////////////////////////////////////////////////////
            // Decode Code Garbling 
            if (BO.DWord[Bit_Ops.Bit14] == true)
                MyCAT48I090.Code_Garbled = CAT48I090Types.Code_Garbled_Type.Code_Garbled;
            else
                MyCAT48I090.Code_Garbled = CAT48I090Types.Code_Garbled_Type.Code_Not_Garbled;

            //////////////////////////////////////////////////////////////////////////////////
            // Decode the flight level
            MyCAT48I090.FlightLevel = DecodeFlightLevel(BO);

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT48.I048DataItems[CAT48.ItemIDToIndex("090")].value = MyCAT48I090;
            //////////////////////////////////////////////////////////////////////////////////

            // Increase data buffer index so it ready for the next data item.
            CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + 2;
        }

        
        static double DecodeFlightLevel(Bit_Ops BO_In)
        {
            double Result = 0.0;

            // First check if this is a negative altitude.
            // and then handle it properly
            if (BO_In.DWord[Bit_Ops.Bit13] == true)
            {
                BO_In.DWord[Bit_Ops.Bit0] = !BO_In.DWord[Bit_Ops.Bit0];
                BO_In.DWord[Bit_Ops.Bit1] = !BO_In.DWord[Bit_Ops.Bit1];
                BO_In.DWord[Bit_Ops.Bit2] = !BO_In.DWord[Bit_Ops.Bit2];
                BO_In.DWord[Bit_Ops.Bit3] = !BO_In.DWord[Bit_Ops.Bit3];
                BO_In.DWord[Bit_Ops.Bit4] = !BO_In.DWord[Bit_Ops.Bit4];
                BO_In.DWord[Bit_Ops.Bit5] = !BO_In.DWord[Bit_Ops.Bit5];
                BO_In.DWord[Bit_Ops.Bit6] = !BO_In.DWord[Bit_Ops.Bit6];
                BO_In.DWord[Bit_Ops.Bit7] = !BO_In.DWord[Bit_Ops.Bit7];
                BO_In.DWord[Bit_Ops.Bit8] = !BO_In.DWord[Bit_Ops.Bit8];
                BO_In.DWord[Bit_Ops.Bit9] = !BO_In.DWord[Bit_Ops.Bit9];
                BO_In.DWord[Bit_Ops.Bit10] = !BO_In.DWord[Bit_Ops.Bit10];
                BO_In.DWord[Bit_Ops.Bit11] = !BO_In.DWord[Bit_Ops.Bit11];
                BO_In.DWord[Bit_Ops.Bit12] = !BO_In.DWord[Bit_Ops.Bit12];
                BO_In.DWord[Bit_Ops.Bit13] = false;
                BO_In.DWord[Bit_Ops.Bit14] = false;
                BO_In.DWord[Bit_Ops.Bit15] = false;

                BO_In.DWord[Bit_Ops.Bits0_15_Of_DWord] = BO_In.DWord[Bit_Ops.Bits0_15_Of_DWord] + 1;

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

                Result = -Result;
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
