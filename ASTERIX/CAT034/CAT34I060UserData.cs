using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT34I060UserData
    {

        public static void DecodeCAT34I060(byte[] Data)
        {

            // At this time we do not care about the content of CAT034I060.
            // However it is necessary to determine how many subfields of 8 octets
            // is present so that data buffer can be updated accordingly on order to
            // decode the other fileds of interest
            // NOTE: We assume that only first 7 defined subfuled

            // I034/050 	Radar Plot Characteristics                          1 + 1+
            int Number_Of_Octets_Present = 1; // at least two, but that will be determined below

            // Decode 020
            if (CAT34.I034DataItems[CAT34.ItemIDToIndex("060")].HasBeenPresent == true)
            {
                // Get an instance of bit ops
                Bit_Ops BO = new Bit_Ops();

                //Extract the first octet
                BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + 1];

                // Now check how many data octets is present.
                if (BO.DWord[CAT34I060Types.Subfiled_1] == true)
                {
                    Number_Of_Octets_Present++;
                }
                if (BO.DWord[CAT34I060Types.Subfiled_2] == true)
                {
                    Number_Of_Octets_Present++;
                }
                if (BO.DWord[CAT34I060Types.Subfiled_3] == true)
                {
                    Number_Of_Octets_Present++;
                }
                if (BO.DWord[CAT34I060Types.Subfiled_4] == true)
                {
                    Number_Of_Octets_Present++;
                }
                if (BO.DWord[CAT34I060Types.Subfiled_5] == true)
                {
                    Number_Of_Octets_Present++;
                }
                if (BO.DWord[CAT34I060Types.Subfiled_6] == true)
                {
                    Number_Of_Octets_Present++;
                }
                if (BO.DWord[CAT34I060Types.Subfiled_7] == true)
                {
                    Number_Of_Octets_Present++;
                }

                // Increase data buffer index so it ready for the next data item.
                CAT34.CurrentDataBufferOctalIndex = CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present;

            }
        }
    }
}
