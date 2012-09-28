using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I040UserData
    {
        public static void DecodeCAT62I040(byte[] Data)
        {

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];

            int Track_Number = BO.DWord[Bit_Ops.Bits0_15_Of_DWord];

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT62.I062DataItems[CAT62.ItemIDToIndex("040")].value = Track_Number;
            //////////////////////////////////////////////////////////////////////////////////
            
            // Increase data buffer index so it ready for the next data item.
            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
        }
    }
}
