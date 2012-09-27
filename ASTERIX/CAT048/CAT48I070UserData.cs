using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT48I070UserData
    {
        public static void DecodeCAT48I070(byte[] Data)
        {
            // A new instance of the CAT48I070 data
            CAT48I070Types.CAT48I070Mode3UserData MyCAT48I070 = new CAT48I070Types.CAT48I070Mode3UserData();

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 2];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 1];

            //////////////////////////////////////////////////////////////////////////////////
            // Decode Code validation
            if (BO.DWord[Bit_Ops.Bit15] == true)
                MyCAT48I070.Code_Validated = CAT48I070Types.Code_Validation_Type.Code_Not_Validated;
            else
                MyCAT48I070.Code_Validated = CAT48I070Types.Code_Validation_Type.Code_Validated;

            //////////////////////////////////////////////////////////////////////////////////
            // Decode Code Garbling 
            if (BO.DWord[Bit_Ops.Bit14] == true)
                MyCAT48I070.Code_Garbled = CAT48I070Types.Code_Garbled_Type.Code_Garbled;
            else
                MyCAT48I070.Code_Garbled = CAT48I070Types.Code_Garbled_Type.Code_Not_Garbled;

            //////////////////////////////////////////////////////////////////////////////////
            // Decode Code Smothed or from Transponder
            if (BO.DWord[Bit_Ops.Bit13] == true)
                MyCAT48I070.Code_Smothed_Or_From_Transponder = CAT48I070Types.Code_Smothed_Or_From_Transporder_Type.Code_Not_Extracted;
            else
                MyCAT48I070.Code_Smothed_Or_From_Transponder = CAT48I070Types.Code_Smothed_Or_From_Transporder_Type.Code_From_Transpodner;

            //////////////////////////////////////////////////////////////////////////////////
            // Decode Code value
            int A = DetermineOctalFromTheeBoolean(BO.DWord[Bit_Ops.Bit9], BO.DWord[Bit_Ops.Bit10], BO.DWord[Bit_Ops.Bit11]);
            int B = DetermineOctalFromTheeBoolean(BO.DWord[Bit_Ops.Bit6], BO.DWord[Bit_Ops.Bit7], BO.DWord[Bit_Ops.Bit8]);
            int C = DetermineOctalFromTheeBoolean(BO.DWord[Bit_Ops.Bit3], BO.DWord[Bit_Ops.Bit4], BO.DWord[Bit_Ops.Bit5]);
            int D = DetermineOctalFromTheeBoolean(BO.DWord[Bit_Ops.Bit0], BO.DWord[Bit_Ops.Bit1], BO.DWord[Bit_Ops.Bit2]);

            MyCAT48I070.Mode3A_Code = A.ToString() + B.ToString() + C.ToString() + D.ToString();

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT48.I048DataItems[CAT48.ItemIDToIndex("070")].value = MyCAT48I070;
            //////////////////////////////////////////////////////////////////////////////////

            // Leave it at the current index for the next decode
            CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + 2;
        }

        // This method returns an octal representation of a number
        //based on the three booleans (three bits representation)
        static int DetermineOctalFromTheeBoolean(bool One, bool Two, bool Three)
        {
            int Result = -1;

            if ((One == false) && (Two == false) && (Three == false))       // 0
                Result = 0;
            else if ((One == true) && (Two == false) && (Three == false))   // 1
                Result = 1;
            else if ((One == false) && (Two == true) && (Three == false))   // 2
                Result = 2;
            else if ((One == true) && (Two == true) && (Three == false))    // 3
                Result = 3;
            else if ((One == false) && (Two == false) && (Three == true))   // 4
                Result = 4;
            else if ((One == true) && (Two == false) && (Three == true))    // 5
                Result = 5;
            else if ((One == false) && (Two == true) && (Three == true))    // 6
                Result = 6;
            else                                                            // 7
                Result = 7;

            return Result;

        }
    }
}
