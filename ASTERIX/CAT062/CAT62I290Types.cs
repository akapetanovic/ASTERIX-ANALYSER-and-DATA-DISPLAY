using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I290Types
    {
        // WORD 0 
        public static int Track_Age = Bit_Ops.Bit7;
        public static int PSR_Age = Bit_Ops.Bit6;
        public static int SSR_Age = Bit_Ops.Bit5;
        public static int Mode_S_Age = Bit_Ops.Bit4;
        public static int ADS_C_Age = Bit_Ops.Bit3;
        public static int ADS_B_Age = Bit_Ops.Bit2;
        public static int ADS_B_VDL_Age = Bit_Ops.Bit1;
        public static int WORD0_FX_Extension_Indicator = Bit_Ops.Bit0;
        // WORD 1
        public static int ADS_B_UAT_Age = Bit_Ops.Bit7;
        public static int Loop_Age = Bit_Ops.Bit6;
        public static int Multilateration_Age = Bit_Ops.Bit5;
    }
}
