using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser
{
    class CAT48I130UserData
    {
        public static void DecodeCAT48I130(byte[] Data)
        {

            // At this time we do not care about the content of CAT048I130.
            // However it is necessary to determine how many subfields of 8 octets
            // is present so that data buffer can be updated accordingly on order to
            // decode the other fileds of interest
            // NOTE: We assume that only first 7 defined subfuled

            // I048/130 	Radar Plot Characteristics                          1 + 1+
            int Number_Of_Octets_Present = 1; // at least two, but that will be determined below

            // Decode 020
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("130")].HasBeenPresent == true)
            {
                // Get an instance of bit ops
                Bit_Ops BO = new Bit_Ops();

                //Extract the first octet
                BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 1];

                // Now check how many data octets is present.
                if (BO.DWord[CAT48I130Types.Subfiled_1] == true)
                {
                    Number_Of_Octets_Present++;
                }
                if (BO.DWord[CAT48I130Types.Subfiled_2] == true)
                {
                    Number_Of_Octets_Present++;
                }
                if (BO.DWord[CAT48I130Types.Subfiled_3] == true)
                {
                    Number_Of_Octets_Present++;
                }
                if (BO.DWord[CAT48I130Types.Subfiled_4] == true)
                {
                    Number_Of_Octets_Present++;
                }
                if (BO.DWord[CAT48I130Types.Subfiled_5] == true)
                {
                    Number_Of_Octets_Present++;
                }
                if (BO.DWord[CAT48I130Types.Subfiled_6] == true)
                {
                    Number_Of_Octets_Present++;
                }
                if (BO.DWord[CAT48I130Types.Subfiled_7] == true)
                {
                    Number_Of_Octets_Present++;
                }

                // Increase data buffer index so it ready for the next data item.
                CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + Number_Of_Octets_Present;
            }
        }
    }
}
