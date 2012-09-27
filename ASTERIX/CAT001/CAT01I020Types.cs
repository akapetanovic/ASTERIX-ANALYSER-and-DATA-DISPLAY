using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT01I020Types
    {

        /// <summary>
        /// /////////////////////////////////////////////////////
        /// // Define indexes for the first Target Report word
        /// //
        /// </summary>
        public static int Word1_TYP_Index = Bit_Ops.Bit7;
        public static int Word1_SIM_Index = Bit_Ops.Bit6;

        // 2 and 3
        public static int Word1_SSR_PSR_Start_Index = Bit_Ops.Bit5;
        public static int Word1_SSR_PSR_End_Index = Bit_Ops.Bit4;

        public static int Word1_ANT_Index = Bit_Ops.Bit3;
        public static int Word1_SPI_Index = Bit_Ops.Bit2;
        public static int Word1_FFT_Index = Bit_Ops.Bit1;
        public static int Word1_FX_Index = Bit_Ops.Bit0;


        /////////////////////////////////////////////////////////////
        // Define indexes to the first extension of the Target Report
        //
        public static int Word2_TST_Index = Bit_Ops.Bit7;

        public static int Word2_DS1_DS2_Start_Index = Bit_Ops.Bit6;
        public static int Word2_DS1_DS2_End_Index = Bit_Ops.Bit5;

        public static int Word2_ME_Index = Bit_Ops.Bit4;
        public static int Word2_MI_Index = Bit_Ops.Bit3;

        // 5 and 6 are spare, set always to 0

        public static int Word2_FX_Index = Bit_Ops.Bit0;

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////
        /// // Here define all the user friendly typs for the Target Descriptor
        /// </summary>
        public enum Type_Of_Report_Type { Unknown_Data, Plot, Track };

        public enum Simulated_Or_Actual_Type { Unknown_Data, Simulated, Actual };

        public enum Radar_Detection_Type { Unknown_Data, No_Detection, Primary, Secondary, Combined };

        public enum Antena_Source_Type { Unknown_Data, Antena_1, Antena_2 };

        public enum Special_Position_Ind_Type { Unknown_Data, Default_Position, SPI_Special_Position_Indicator };

        public enum Data_From_FFT_Type { Unknown_Data, Not_from_FFT, From_FFT };

        /// <summary>
        /// //////////////////////////////
        /// // Extension data
        /// </summary>
        
        public enum Test_Target_Indicator_Type { Unknown_Data, Default_Target_Indicator_Type, Test_Target_Indicator };

        public enum Special_SSR_Codes_Type { Unknown_Data, Default_SSR_Code, C7500_Unlawful_Interference, C7600_Radio_Com_Failure, C7700_Emergency };

        public enum Military_Emergency_Type { Unknown_Data, Yes, No };

        public enum Military_Identification_Type { Unknown_Data, Yes, No };


        ///////////////////////////////////////////////////////////
        /// // Common type
        /// </summary>
        public enum Next_Extension_Type { Unknown_Data, Yes, No };

    }
}
