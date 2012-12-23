using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT48I220UserData
    {
        public static void DecodeCAT48I220(byte[] Data)
        {

            CAT48I220Types.CAT48AC_Address_Type MyCAT48AC_Address = new CAT48I220Types.CAT48AC_Address_Type();

            // Get all octets
            Bit_Ops Bits_1_To_Bits_32_ = new Bit_Ops();
            Bits_1_To_Bits_32_.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 3];
            Bits_1_To_Bits_32_.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 2];
            Bits_1_To_Bits_32_.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 1];
            MyCAT48AC_Address.Is_Valid = true;
            MyCAT48AC_Address.AC_ADDRESS_String = Bits_1_To_Bits_32_.DWord.Data.ToString("X");

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT48.I048DataItems[CAT48.ItemIDToIndex("220")].value = MyCAT48AC_Address;
            
            // Increase data buffer index so it ready for the next data item.
            CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + 3;
        }
    }
}
