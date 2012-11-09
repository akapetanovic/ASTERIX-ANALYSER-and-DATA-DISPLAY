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
            // is present so that data buffer can be updated accordingly in order to
            // decode the other fileds of interest
            // NOTE: We assume that only first 7 defined subfields are present

            // I034/050 	System Configuration and Status                          1 + 1+
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

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Now check how many data octets is present and decode the present subfileds at the same time
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                //////////////////////////////////////////////////////////////////////////////////
                // COM - Common elements of a System
                //////////////////////////////////////////////////////////////////////////////////
                if (BO.DWord[CAT34I050Types.Subfiled_1] == true)
                {
                    Number_Of_Octets_Present++;
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];
                    ThisCycleData.COM_Data.Data_Present = true;
                    
                    ThisCycleData.COM_Data.System_is_NOGO = CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_1];
                    ThisCycleData.COM_Data.RDPC2_Selected = CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_2];
                    ThisCycleData.COM_Data.RDPC_Reset = CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_3];
                    ThisCycleData.COM_Data.RDP_Overloaded = CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_4];
                    ThisCycleData.COM_Data.Transmision_Sys_Overloaded = CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_5];
                    ThisCycleData.COM_Data.Monitor_Sys_Disconected = CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_6];
                    ThisCycleData.COM_Data.Time_Source_Invalid = CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_7];
                }
                //////////////////////////////////////////////////////////////////////////////////
                // Spare
                //////////////////////////////////////////////////////////////////////////////////

                if (BO.DWord[CAT34I050Types.Subfiled_2] == true)
                {
                    Number_Of_Octets_Present++;
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];
                }
                //////////////////////////////////////////////////////////////////////////////////
                // Spare
                //////////////////////////////////////////////////////////////////////////////////

                if (BO.DWord[CAT34I050Types.Subfiled_3] == true)
                {
                    Number_Of_Octets_Present++;
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];
                }
                //////////////////////////////////////////////////////////////////////////////////
                // PSR - Primary sensor component
                //////////////////////////////////////////////////////////////////////////////////
                if (BO.DWord[CAT34I050Types.Subfiled_4] == true)
                {
                    Number_Of_Octets_Present++;
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];
                    ThisCycleData.PSR_Data.Data_Present = true;

                    ThisCycleData.PSR_Data.Ant_2_Selected = CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_1];
                    if (CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_3] == true && CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_2] == true)
                        ThisCycleData.PSR_Data.CH_Status = CAT34I050Types.PSR.Channel_Status.Channel_A_and_B;
                    else if (CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_3] == false && CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_2] == false)
                        ThisCycleData.PSR_Data.CH_Status = CAT34I050Types.PSR.Channel_Status.No_Channel;
                    else if (CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_3] == true)
                        ThisCycleData.PSR_Data.CH_Status = CAT34I050Types.PSR.Channel_Status.Channel_A;
                    else
                        ThisCycleData.PSR_Data.CH_Status = CAT34I050Types.PSR.Channel_Status.Channel_B;

                    ThisCycleData.PSR_Data.PSR_Overloaded = CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_4];
                    ThisCycleData.PSR_Data.Monitor_Sys_Disconected = CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_5];
                }
                //////////////////////////////////////////////////////////////////////////////////
                // SSR - Secondary sensor component
                //////////////////////////////////////////////////////////////////////////////////
                if (BO.DWord[CAT34I050Types.Subfiled_5] == true)
                {
                    Number_Of_Octets_Present++;
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];
                    ThisCycleData.SSR_Data.Data_Present = true;

                    ThisCycleData.SSR_Data.Ant_2_Selected = CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_1];
                    if (CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_3] == true && CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_2] == true)
                        ThisCycleData.SSR_Data.CH_Status = CAT34I050Types.SSR.Channel_Status.Invalid_Combination;
                    else if (CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_3] == false && CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_2] == false)
                        ThisCycleData.SSR_Data.CH_Status = CAT34I050Types.SSR.Channel_Status.No_Channel;
                    else if (CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_3] == true)
                        ThisCycleData.SSR_Data.CH_Status = CAT34I050Types.SSR.Channel_Status.Channel_A;
                    else
                        ThisCycleData.SSR_Data.CH_Status = CAT34I050Types.SSR.Channel_Status.Channel_B;

                    ThisCycleData.SSR_Data.SSR_Overloaded = CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_4];
                    ThisCycleData.SSR_Data.Monitor_Sys_Disconected = CAT34_I050_Data.DWord[CAT34I050Types.Subfiled_5]; 
                }
                //////////////////////////////////////////////////////////////////////////////////
                // MDS - Mode-S sensor component
                //////////////////////////////////////////////////////////////////////////////////

                if (BO.DWord[CAT34I050Types.Subfiled_6] == true)
                {
                    Number_Of_Octets_Present++;
                    // Mode S subfield is 2 octets
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];
                    ThisCycleData.MDS_Data.Data_Present = true;


                    ThisCycleData.MDS_Data.Ant_2_Selected = BO.DWord[CAT34I050Types.Subfiled_1];

                    if (BO.DWord[CAT34I050Types.Subfiled_3] == true && BO.DWord[CAT34I050Types.Subfiled_2] == true)
                        ThisCycleData.MDS_Data.CH_Status = CAT34I050Types.MDS.Channel_Status.Illegal_Combination;
                    else if (BO.DWord[CAT34I050Types.Subfiled_3] == false && BO.DWord[CAT34I050Types.Subfiled_2] == false)
                        ThisCycleData.MDS_Data.CH_Status = CAT34I050Types.MDS.Channel_Status.No_Channel;
                    else if (BO.DWord[CAT34I050Types.Subfiled_3] == true)
                        ThisCycleData.MDS_Data.CH_Status = CAT34I050Types.MDS.Channel_Status.Channel_A;
                    else
                        ThisCycleData.MDS_Data.CH_Status = CAT34I050Types.MDS.Channel_Status.Channel_B;

                    ThisCycleData.MDS_Data.ModeS_Overloaded = BO.DWord[CAT34I050Types.Subfiled_4];
                    ThisCycleData.MDS_Data.Monitor_Sys_Disconected = BO.DWord[CAT34I050Types.Subfiled_5];
                    ThisCycleData.MDS_Data.CH2_For_Coordination_In_Use = BO.DWord[CAT34I050Types.Subfiled_6];
                    ThisCycleData.MDS_Data.CH2_For_DataLink_In_Use = BO.DWord[CAT34I050Types.Subfiled_7];
                    ThisCycleData.MDS_Data.Coordination_Func_Overload = BO.DWord[CAT34I050Types.FX_Primary_Subfiled]; // Not really FX but the index is correct

                    Number_Of_Octets_Present++;
                    // Mode S subfield is 2 octets
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];

                    ThisCycleData.MDS_Data.DataLink_Func_Overload = BO.DWord[CAT34I050Types.Subfiled_1];
                }
                //////////////////////////////////////////////////////////////////////////////////
                // Spare
                //////////////////////////////////////////////////////////////////////////////////

                if (BO.DWord[CAT34I050Types.Subfiled_7] == true)
                {
                    Number_Of_Octets_Present++;
                    CAT34_I050_Data.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present];
                }

                //////////////////////////////////////////////////////////////////////////////////
                // Now assign it to the generic list
                CAT34.I034DataItems[CAT34.ItemIDToIndex("050")].value = ThisCycleData;
                //////////////////////////////////////////////////////////////////////////////////

                // Increase data buffer index so it ready for the next data item.
                CAT34.CurrentDataBufferOctalIndex = CAT34.CurrentDataBufferOctalIndex + Number_Of_Octets_Present;
            }
        }
    }
}
