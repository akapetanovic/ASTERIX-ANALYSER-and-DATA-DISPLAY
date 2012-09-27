using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT01I020UserData
    {

        public CAT01I020Types.Type_Of_Report_Type Type_Of_Report = CAT01I020Types.Type_Of_Report_Type.Unknown_Data;

        public CAT01I020Types.Simulated_Or_Actual_Type Simulated_Or_Actual_Report = CAT01I020Types.Simulated_Or_Actual_Type.Unknown_Data;

        public CAT01I020Types.Radar_Detection_Type Type_Of_Radar_Detection = CAT01I020Types.Radar_Detection_Type.Unknown_Data;

        public CAT01I020Types.Antena_Source_Type Antena_Source = CAT01I020Types.Antena_Source_Type.Unknown_Data;

        public CAT01I020Types.Special_Position_Ind_Type Special_Position_Ind = CAT01I020Types.Special_Position_Ind_Type.Unknown_Data;

        public CAT01I020Types.Data_From_FFT_Type Data_Is_From_FFT = CAT01I020Types.Data_From_FFT_Type.Unknown_Data;

        public CAT01I020Types.Next_Extension_Type Next_Extension_1 = CAT01I020Types.Next_Extension_Type.Unknown_Data;

        /// <summary>
        /// //////////////////////////////
        /// // Extension data
        /// </summary>

        public CAT01I020Types.Test_Target_Indicator_Type Is_Test_Target_Indicator = CAT01I020Types.Test_Target_Indicator_Type.Unknown_Data;

        public CAT01I020Types.Special_SSR_Codes_Type Special_SSR_Codes = CAT01I020Types.Special_SSR_Codes_Type.Unknown_Data;

        public CAT01I020Types.Military_Emergency_Type Is_Military_Emergency = CAT01I020Types.Military_Emergency_Type.Unknown_Data;

        public CAT01I020Types.Military_Identification_Type Is_Military_Identification = CAT01I020Types.Military_Identification_Type.Unknown_Data;

        public CAT01I020Types.Next_Extension_Type Next_Extension_2 = CAT01I020Types.Next_Extension_Type.Unknown_Data;

        public static void DecodeCAT01I002(byte[] Data)
        {
           
            // Decode 020
            if (CAT01.I001DataItems[CAT01.ItemIDToIndex("020")].CurrentlyPresent == true)
            {
                 // First define CAT01I002 class
                CAT01I020UserData MyI002UserData = new CAT01I020UserData();

                // Get an instance of bit ops
                Bit_Ops BO = new Bit_Ops();
                //Extract the first octet
                BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex];

                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //
                if (BO.DWord[CAT01I020Types.Word1_TYP_Index] == true)
                    MyI002UserData.Type_Of_Report = CAT01I020Types.Type_Of_Report_Type.Track;
                else
                    MyI002UserData.Type_Of_Report = CAT01I020Types.Type_Of_Report_Type.Plot;

                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //
                if (BO.DWord[CAT01I020Types.Word1_SIM_Index] == true)
                    MyI002UserData.Simulated_Or_Actual_Report = CAT01I020Types.Simulated_Or_Actual_Type.Simulated;
                else
                    MyI002UserData.Simulated_Or_Actual_Report = CAT01I020Types.Simulated_Or_Actual_Type.Actual;


                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //
                if ((BO.DWord[CAT01I020Types.Word1_SSR_PSR_Start_Index] == true) &&
                    (BO.DWord[CAT01I020Types.Word1_SSR_PSR_End_Index] == true))
                {
                    MyI002UserData.Type_Of_Radar_Detection = CAT01I020Types.Radar_Detection_Type.Combined;
                }
                else if ((BO.DWord[CAT01I020Types.Word1_SSR_PSR_Start_Index] == false) &&
                    (BO.DWord[CAT01I020Types.Word1_SSR_PSR_End_Index] == false))
                {
                    MyI002UserData.Type_Of_Radar_Detection = CAT01I020Types.Radar_Detection_Type.No_Detection;
                }
                else if (BO.DWord[CAT01I020Types.Word1_SSR_PSR_Start_Index] == true)
                {
                    MyI002UserData.Type_Of_Radar_Detection = CAT01I020Types.Radar_Detection_Type.Secondary;
                }
                else
                {
                    MyI002UserData.Type_Of_Radar_Detection = CAT01I020Types.Radar_Detection_Type.Primary;
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //
                if (BO.DWord[CAT01I020Types.Word1_ANT_Index] == true)
                    MyI002UserData.Antena_Source = CAT01I020Types.Antena_Source_Type.Antena_2;
                else
                    MyI002UserData.Antena_Source = CAT01I020Types.Antena_Source_Type.Antena_1;


                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //
                if (BO.DWord[CAT01I020Types.Word1_SPI_Index] == true)
                    MyI002UserData.Special_Position_Ind = CAT01I020Types.Special_Position_Ind_Type.SPI_Special_Position_Indicator;
                else
                    MyI002UserData.Special_Position_Ind = CAT01I020Types.Special_Position_Ind_Type.Default_Position;


                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //
                if (BO.DWord[CAT01I020Types.Word1_FFT_Index] == true)
                    MyI002UserData.Data_Is_From_FFT = CAT01I020Types.Data_From_FFT_Type.From_FFT;

                else
                    MyI002UserData.Data_Is_From_FFT = CAT01I020Types.Data_From_FFT_Type.Not_from_FFT;

                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //
                if (BO.DWord[CAT01I020Types.Word1_FX_Index] == false)
                    MyI002UserData.Next_Extension_1 = CAT01I020Types.Next_Extension_Type.No;

                else
                {
                    MyI002UserData.Next_Extension_1 = CAT01I020Types.Next_Extension_Type.Yes;

                    // OK we have a filed extension, so lets first move the data buffer to the next 
                    // octet
                    CAT01.CurrentDataBufferOctalIndex = CAT01.CurrentDataBufferOctalIndex + 1;

                    //Extract the next octet 
                    BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex];


                    if (BO.DWord[CAT01I020Types.Word2_TST_Index] == true)
                        MyI002UserData.Is_Test_Target_Indicator = CAT01I020Types.Test_Target_Indicator_Type.Test_Target_Indicator;
                    else
                        MyI002UserData.Is_Test_Target_Indicator = CAT01I020Types.Test_Target_Indicator_Type.Default_Target_Indicator_Type;


                    MyI002UserData.Special_SSR_Codes = CAT01I020Types.Special_SSR_Codes_Type.Unknown_Data;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    //
                    if ((BO.DWord[CAT01I020Types.Word2_DS1_DS2_Start_Index] == true) &&
                        (BO.DWord[CAT01I020Types.Word2_DS1_DS2_End_Index] == true))
                    {
                        MyI002UserData.Special_SSR_Codes = CAT01I020Types.Special_SSR_Codes_Type.C7700_Emergency;
                    }
                    else if ((BO.DWord[CAT01I020Types.Word2_DS1_DS2_Start_Index] == false) &&
                        (BO.DWord[CAT01I020Types.Word2_DS1_DS2_End_Index] == false))
                    {
                        MyI002UserData.Special_SSR_Codes = CAT01I020Types.Special_SSR_Codes_Type.Default_SSR_Code;
                    }
                    else if (BO.DWord[CAT01I020Types.Word2_DS1_DS2_Start_Index] == true)
                    {
                        MyI002UserData.Special_SSR_Codes = CAT01I020Types.Special_SSR_Codes_Type.C7600_Radio_Com_Failure;
                    }
                    else
                    {
                        MyI002UserData.Special_SSR_Codes = CAT01I020Types.Special_SSR_Codes_Type.C7500_Unlawful_Interference;
                    }


                    if (BO.DWord[CAT01I020Types.Word2_ME_Index] == true)
                        MyI002UserData.Is_Military_Emergency = CAT01I020Types.Military_Emergency_Type.Yes;
                    else
                        MyI002UserData.Is_Military_Emergency = CAT01I020Types.Military_Emergency_Type.No;


                    if (BO.DWord[CAT01I020Types.Word2_MI_Index] == true)
                        MyI002UserData.Is_Military_Identification = CAT01I020Types.Military_Identification_Type.Yes;
                    else
                        MyI002UserData.Is_Military_Identification = CAT01I020Types.Military_Identification_Type.No;

                    if (BO.DWord[CAT01I020Types.Word2_FX_Index] == true)
                        MyI002UserData.Next_Extension_2 = CAT01I020Types.Next_Extension_Type.Yes;
                    else
                        MyI002UserData.Next_Extension_2 = CAT01I020Types.Next_Extension_Type.No;

                }

                //////////////////////////////////////////////////////////////////////////////////
                // Now assign it to the generic list
                CAT01.I001DataItems[CAT01.ItemIDToIndex("020")].value = MyI002UserData;
                //////////////////////////////////////////////////////////////////////////////////
            }
        }
        ///////////////////////////////////////////////////////////
        /// // Common type
        /// </summary>

      
    }
}
