using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class Decode034_020
    {
        public static void Decode(byte[] Data)
        {

            // 4 I034/020 Sector Number 1
            if (CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("020")].IsPresent == false)
            {
                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("020")].value = "N/A";
            }
            else
            {
                int Index = ASTERIX.GetFirstDataIndex(Data);

                // Get an instance of bit ops
                Bit_Ops BO = new Bit_Ops();
                
                // Move each octet into the DWORD
                int SectorNumber = BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[Index];

                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("020")].value = SectorNumber;

                CAT34.I034DataItems.Add(new CAT34.I034DataItem());
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].Time = CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("020")].Time;
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].Description = CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("020")].Description;
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].ID = CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("020")].ID;
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].IsPresent = true;
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].value = CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("020")].value;
            }
           
        }
    }
}