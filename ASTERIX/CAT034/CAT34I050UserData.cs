using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT34I050UserData
    {
        public static void DecodeCAT34I050(byte[] Data)
        {

            // At this time we do not care about the content of CAT034I050.
            // However it is necessary to determine how many subfields of 8 octets
            // is present so that data buffer can be updated accordingly on order to
            // decode the other fileds of interest
            // NOTE: We assume that only first 7 defined subfuled

            // I034/050 	Radar Plot Characteristics                          1 + 1+
            int Number_Of_Octets_Present = 1; // at least two, but that will be determined below

            // Decode 020
            if (CAT34.I034DataItems[CAT34.ItemIDToIndex("050")].HasBeenPresent == true)
            {
                // Get an instance of bit ops
                Bit_Ops BO = new Bit_Ops();
                Bit_Ops CAT34_I050_Data = new Bit_Ops();

                //Extract the first octet
                BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + 1];

                CAT34I050Types.CAT34I050UserData ThisCycleData = new CAT34I050Types.CAT34I050UserData();
                
                // Now check how many data octets is present and 
                // decode the data at the same time
                if (BO.DWord[CAT34I050Types.Subfiled_1] == true)
                {
                    Number_Of_Octets_Present++;
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];
                    ThisCycleData.COM_Data.Data_Present = true;
                    ThisCycleData.COM_Data.System_is_NOGO = BO.DWord[CAT34I050Types.Subfiled_1];
                    ThisCycleData.COM_Data.RDPC2_Selected = BO.DWord[CAT34I050Types.Subfiled_2];
                    ThisCycleData.COM_Data.RDPC_Reset = BO.DWord[CAT34I050Types.Subfiled_3];
                    ThisCycleData.COM_Data.RDP_Overloaded = BO.DWord[CAT34I050Types.Subfiled_4];
                    ThisCycleData.COM_Data.Transmision_Sys_Overloaded = BO.DWord[CAT34I050Types.Subfiled_5];
                    ThisCycleData.COM_Data.Monitor_Sys_Disconected = BO.DWord[CAT34I050Types.Subfiled_6];
                    ThisCycleData.COM_Data.Time_Source_Invalid = BO.DWord[CAT34I050Types.Subfiled_7];
                }
                if (BO.DWord[CAT34I050Types.Subfiled_2] == true)
                {
                    Number_Of_Octets_Present++;
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];
                }
                if (BO.DWord[CAT34I050Types.Subfiled_3] == true)
                {
                    Number_Of_Octets_Present++;
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];
                }
                if (BO.DWord[CAT34I050Types.Subfiled_4] == true)
                {
                    Number_Of_Octets_Present++;
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];
                }
                if (BO.DWord[CAT34I050Types.Subfiled_5] == true)
                {
                    Number_Of_Octets_Present++;
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];
                }
                if (BO.DWord[CAT34I050Types.Subfiled_6] == true)
                {
                    Number_Of_Octets_Present++;
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];
                }
                if (BO.DWord[CAT34I050Types.Subfiled_7] == true)
                {
                    Number_Of_Octets_Present++;
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];
                }

                // Increase data buffer index so it ready for the next data item.
                CAT34.CurrentDataBufferOctalIndex = CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present;
            }
        }
    }
}
