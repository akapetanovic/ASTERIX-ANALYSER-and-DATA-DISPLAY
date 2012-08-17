using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticastingUDP
{
    class Decode034_010
    {

        public static void Decode(byte[] Data)
        {
            // I034/010 Data Source Identifier 2
            if (CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("010")].IsPresent == false)
            {
                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("010")].value = "N/A";
            }
            else
            {
                // OK we know that there is data in the data source, so
                // lets extract it.

                // Get the first data index
                int DSC_Index = ASTERIX.GetFirstDataIndex(Data);

                // Get an instance of bit ops
                Bit_Ops BO = new Bit_Ops();
                
                // Extract first 16 bits 
                // 15..........................0
                // 00000000             00000000
                // First_LEN_Octet   Second_LEN_Octet

                BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[DSC_Index + 1];
                BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[DSC_Index];

                int Result = BO.DWord[Bit_Ops.Bits0_15_Of_DWord];

                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("010")].value = Result;

                CAT34.I034DataItems.Add(new CAT34.I034DataItem());
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].Time = CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("010")].Time;
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].Description = CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("010")].Description;
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].ID = CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("010")].ID;
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].IsPresent = true;
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].value = CAT34.Extract_SIC_SAC(Data); 
            }
        }
    }
}
