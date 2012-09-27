using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT48I020UserData
    {
        public CAT48I020Types.Type_Of_Report_Type Type_Of_Report = CAT48I020Types.Type_Of_Report_Type.Unknown_Data;

        public CAT48I020Types.Simulated_Or_Actual_Type Simulated_Or_Actual = CAT48I020Types.Simulated_Or_Actual_Type.Unknown_Data;

        public CAT48I020Types.RDP_Chain_Type RDP_Chain = CAT48I020Types.RDP_Chain_Type.Unknown_Data;

        public CAT48I020Types.Special_Position_Ind_Type Special_Position_Ind = CAT48I020Types.Special_Position_Ind_Type.Unknown_Data;

        public CAT48I020Types.Data_From_FFT_Type Data_From_FFT = CAT48I020Types.Data_From_FFT_Type.Unknown_Data;

        public CAT48I020Types.Next_Extension_Type Next_Extension_1 = CAT48I020Types.Next_Extension_Type.Unknown_Data;

        ///// <summary>
        ///// //////////////////////////////
        ///// // Extension data
        ///// </summary>

        public CAT48I020Types.Test_Target_Indicator_Type Test_Target_Indicator = CAT48I020Types.Test_Target_Indicator_Type.Unknown_Data;

        public CAT48I020Types.Military_Emergency_Type Military_Emergency = CAT48I020Types.Military_Emergency_Type.Unknown_Data;

        public CAT48I020Types.Military_Identification_Type Military_Identification = CAT48I020Types.Military_Identification_Type.Unknown_Data;

        public CAT48I020Types.FOE_or_FRI_Type FOE_or_FRI = CAT48I020Types.FOE_or_FRI_Type.Unknown_Data;

    
        public static void DecodeCAT48I002(byte[] Data)
        {
            // Decode 020
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("020")].HasBeenPresent == true)
            {
                // First define CAT48I002 class
                CAT48I020UserData MyI002UserData = new CAT48I020UserData();

                // Get an instance of bit ops
                Bit_Ops BO = new Bit_Ops();

                //Extract the first octet
                BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex];

                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //
                int Type_Result = BitExtractor.GetIntFromThreeBits(BO.DWord[CAT48I020Types.Word1_TYP_Index_1], BO.DWord[CAT48I020Types.Word1_TYP_Index_2], BO.DWord[CAT48I020Types.Word1_TYP_Index_3]);

                switch (Type_Result)
                {
                    // Monoradar Data Target Reports, from a Radar Surveillance System to an SDPS
                    // (plots and tracks from PSRs, SSRs, MSSRs, excluding Mode S and ground surveillance)
                    case 0:
                        MyI002UserData.Type_Of_Report = CAT48I020Types.Type_Of_Report_Type.No_Detection;
                        break;
                    case 1:
                        MyI002UserData.Type_Of_Report = CAT48I020Types.Type_Of_Report_Type.Single_PSR;
                        break;
                    case 2:
                        MyI002UserData.Type_Of_Report = CAT48I020Types.Type_Of_Report_Type.Single_SSR;
                        break;
                    case 3:
                        MyI002UserData.Type_Of_Report = CAT48I020Types.Type_Of_Report_Type.SSR_PSR_Detection;
                        break;
                    case 4:
                        MyI002UserData.Type_Of_Report = CAT48I020Types.Type_Of_Report_Type.Single_Mode_S_All_Call;
                        break;
                    case 5:
                        MyI002UserData.Type_Of_Report = CAT48I020Types.Type_Of_Report_Type.Single_Mode_S_Roll_Call;
                        break;
                    case 6:
                        MyI002UserData.Type_Of_Report = CAT48I020Types.Type_Of_Report_Type.Mode_S_All_Call_PSR;
                        break;
                    case 7:
                        MyI002UserData.Type_Of_Report = CAT48I020Types.Type_Of_Report_Type.Mode_S_Roll_Call_PSR;
                        break;

                    // Handle unsupported data/categories
                    default:
                        break;
                }


                if (BO.DWord[CAT48I020Types.Word1_SIM_Index] == true)
                    MyI002UserData.Simulated_Or_Actual = CAT48I020Types.Simulated_Or_Actual_Type.Simulated;
                else
                    MyI002UserData.Simulated_Or_Actual = CAT48I020Types.Simulated_Or_Actual_Type.Actual;


                if (BO.DWord[CAT48I020Types.Word1_RDP_Index] == true)
                    MyI002UserData.RDP_Chain = CAT48I020Types.RDP_Chain_Type.RDP_2;
                else
                    MyI002UserData.RDP_Chain = CAT48I020Types.RDP_Chain_Type.RDP_1;


                if (BO.DWord[CAT48I020Types.Word1_SPI_Index] == true)
                    MyI002UserData.Special_Position_Ind = CAT48I020Types.Special_Position_Ind_Type.SPI;
                else
                    MyI002UserData.Special_Position_Ind = CAT48I020Types.Special_Position_Ind_Type.NO_SPI;


                if (BO.DWord[CAT48I020Types.Word1_FFT_Index] == true)
                    MyI002UserData.Data_From_FFT = CAT48I020Types.Data_From_FFT_Type.From_FFT;
                else
                    MyI002UserData.Data_From_FFT = CAT48I020Types.Data_From_FFT_Type.Not_from_FFT;


                 ///////////////////////////////////////////////////////////////////////////////////////////////////
                //
                if (BO.DWord[CAT48I020Types.Word1_FX_Index] == false)
                    MyI002UserData.Next_Extension_1 = CAT48I020Types.Next_Extension_Type.No;

                else
                {
                    // OK we have a filed extension, so lets first move the data buffer to the next 
                    // octet
                    CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + 1;

                    //Extract the next octet 
                    BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex];

                    if (BO.DWord[CAT48I020Types.Word2_TST_Index] == true)
                        MyI002UserData.Test_Target_Indicator = CAT48I020Types.Test_Target_Indicator_Type.Test_Target_Indicator;
                    else
                        MyI002UserData.Test_Target_Indicator = CAT48I020Types.Test_Target_Indicator_Type.Real_Target_Indicator;

                    if (BO.DWord[CAT48I020Types.Word2_MI_Index] == true)
                        MyI002UserData.Military_Emergency = CAT48I020Types.Military_Emergency_Type.Unknown_Data;
                    else
                        MyI002UserData.Military_Emergency = CAT48I020Types.Military_Emergency_Type.Unknown_Data;

                    if (BO.DWord[CAT48I020Types.Word2_ME_Index] == true)
                        MyI002UserData.Military_Identification = CAT48I020Types.Military_Identification_Type.Unknown_Data;
                    else
                        MyI002UserData.Military_Identification = CAT48I020Types.Military_Identification_Type.Unknown_Data;


                    if ((BO.DWord[CAT48I020Types.Word2_FOE_FRI_Start_Index] == true) && (BO.DWord[CAT48I020Types.Word2_FOE_FRI_End_Index] == true))
                    {
                        MyI002UserData.FOE_or_FRI = CAT48I020Types.FOE_or_FRI_Type.No_Replay;
                    }
                    else if (BO.DWord[CAT48I020Types.Word2_FOE_FRI_Start_Index] == true)
                    {
                        MyI002UserData.FOE_or_FRI = CAT48I020Types.FOE_or_FRI_Type.Frendly_Target;
                    }
                    else if (BO.DWord[CAT48I020Types.Word2_FOE_FRI_End_Index] == true)
                    {
                        MyI002UserData.FOE_or_FRI = CAT48I020Types.FOE_or_FRI_Type.Unknown_Target;
                    }
                    else
                        MyI002UserData.FOE_or_FRI = CAT48I020Types.FOE_or_FRI_Type.No_Mode_4;


                }
                //////////////////////////////////////////////////////////////////////////////////
                // Now assign it to the generic list
                CAT48.I048DataItems[CAT48.ItemIDToIndex("020")].value = MyI002UserData;
                //////////////////////////////////////////////////////////////////////////////////
            }
        }
    }
}
