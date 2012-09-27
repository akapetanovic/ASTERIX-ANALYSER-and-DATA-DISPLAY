using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT48I020Types
    {

        /// <summary>
        /// /////////////////////////////////////////////////////
        /// // Define indexes for the first Target Report word
        /// //
        /// </summary>
        public static int Word1_TYP_Index_1 = Bit_Ops.Bit5;
        public static int Word1_TYP_Index_2 = Bit_Ops.Bit6;
        public static int Word1_TYP_Index_3 = Bit_Ops.Bit7;

        public static int Word1_SIM_Index = Bit_Ops.Bit4;

        public static int Word1_RDP_Index = Bit_Ops.Bit3;

        public static int Word1_SPI_Index = Bit_Ops.Bit2;

        public static int Word1_FFT_Index = Bit_Ops.Bit1;
        public static int Word1_FX_Index = Bit_Ops.Bit0;

        /////////////////////////////////////////////////////////////
        // Define indexes to the first extension of the Target Report
        //
        public static int Word2_TST_Index = Bit_Ops.Bit7;

        // Bits 6 and 5 are always 0;
       
        public static int Word2_ME_Index = Bit_Ops.Bit4;
        public static int Word2_MI_Index = Bit_Ops.Bit3;

        public static int Word2_FOE_FRI_Start_Index = Bit_Ops.Bit2;
        public static int Word2_FOE_FRI_End_Index = Bit_Ops.Bit1;

        public static int Word2_FX_Index = Bit_Ops.Bit0;

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////
        /// // Here define all the user friendly typs for the Target Descriptor
        /// </summary>
        public enum Type_Of_Report_Type { Unknown_Data, No_Detection, Single_PSR, Single_SSR, SSR_PSR_Detection, Single_Mode_S_All_Call, Single_Mode_S_Roll_Call, 
        Mode_S_All_Call_PSR, Mode_S_Roll_Call_PSR};

        public enum Simulated_Or_Actual_Type { Unknown_Data, Simulated, Actual };

        public enum RDP_Chain_Type { Unknown_Data, RDP_1, RDP_2 };

        public enum Special_Position_Ind_Type { Unknown_Data, NO_SPI, SPI };

        public enum Data_From_FFT_Type { Unknown_Data, Not_from_FFT, From_FFT };

        /// <summary>
        /// //////////////////////////////
        /// // Extension data
        /// </summary>

        public enum Test_Target_Indicator_Type { Unknown_Data, Real_Target_Indicator, Test_Target_Indicator };

        public enum Military_Emergency_Type { Unknown_Data, Yes, No };

        public enum Military_Identification_Type { Unknown_Data, Yes, No };

        public enum FOE_or_FRI_Type { Unknown_Data, No_Mode_4, Frendly_Target, Unknown_Target, No_Replay };

        ///////////////////////////////////////////////////////////
        /// // Common type
        /// </summary>
        public enum Next_Extension_Type { Unknown_Data, Yes, No };
    }
}
