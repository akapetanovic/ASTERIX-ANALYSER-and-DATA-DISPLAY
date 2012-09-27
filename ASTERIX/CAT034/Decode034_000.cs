using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class Decode034_000
    {
        public static void Decode(byte[] Data)
        {

            // 2 I034/000 Message Type 1
            if (CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("000")].IsPresent == false)
            {
                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("000")].value = "N/A";
            }
            else
            {

                int Index = ASTERIX.GetFirstDataIndex(Data);

                // Get an instance of bit ops
                Bit_Ops BO = new Bit_Ops();

                // Move each octet into the DWORD
                int MessageType =  BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[Index];

                CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("000")].value = MessageType;

                CAT34.I034DataItems.Add(new CAT34.I034DataItem());
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].Time = CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("000")].Time;
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].Description = CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("000")].Description;
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].ID = CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("000")].ID;
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].IsPresent = true;
                CAT34.I034DataItems[CAT34.I034DataItems.Count - 1].value = CAT34.I034DataItemsLastValid[CAT34.ItemIDToIndex("000")].value;
            }  
        }

        public static string DecodeToHuman000(object Data)
        {
            string value = "";

            switch ((int)Data)
            {
                case 1:
                    value = "North Marker message";
                    break;
                case 2:
                    value = "Sector crossing message";
                    break;
                case 3:
                    value = "Geographical flitering message";
                    break;
                case 4:
                    value = "Jamming Strobe message";
                    break;
                default:
                    value = "Unknown";
                    break;

            }

            return value;
        }    
    }
}
