using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I060UserData
    {

        public static void DecodeCAT62I060(byte[] Data)
        {

            // A new instance of the CAT48I070 data
            CAT62I060Types.CAT62060Mode3UserData MyCAT62I060 = new CAT62I060Types.CAT62060Mode3UserData();

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

            //////////////////////////////////////////////////////////////////////////////////
            // Has code has changed
            MyCAT62I060.Mode_3A_Has_Changed = (BO.DWord[Bit_Ops.Bit13] == true);

            //////////////////////////////////////////////////////////////////////////////////
            // Decode Code value
            int A = DetermineOctalFromTheeBoolean(BO.DWord[Bit_Ops.Bit9], BO.DWord[Bit_Ops.Bit10], BO.DWord[Bit_Ops.Bit11]);
            int B = DetermineOctalFromTheeBoolean(BO.DWord[Bit_Ops.Bit6], BO.DWord[Bit_Ops.Bit7], BO.DWord[Bit_Ops.Bit8]);
            int C = DetermineOctalFromTheeBoolean(BO.DWord[Bit_Ops.Bit3], BO.DWord[Bit_Ops.Bit4], BO.DWord[Bit_Ops.Bit5]);
            int D = DetermineOctalFromTheeBoolean(BO.DWord[Bit_Ops.Bit0], BO.DWord[Bit_Ops.Bit1], BO.DWord[Bit_Ops.Bit2]);

            MyCAT62I060.Mode3A_Code = A.ToString() + B.ToString() + C.ToString() + D.ToString();

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT62.I062DataItems[CAT62.ItemIDToIndex("060")].value = MyCAT62I060;
            //////////////////////////////////////////////////////////////////////////////////
            
            // Increase data buffer index so it ready for the next data item.
            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
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
