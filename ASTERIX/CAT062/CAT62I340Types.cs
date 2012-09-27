using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I340Types
    {
        // WORD 0 
        public static int Sensor_Identification = Bit_Ops.Bit7;
        public static int Measured_Position = Bit_Ops.Bit6;
        public static int Measured_3_D_Height = Bit_Ops.Bit5;
        public static int Last_Measured_Mode_C_Code = Bit_Ops.Bit4;
        public static int Last_Measured_Mode_3_A_Code = Bit_Ops.Bit3;
        public static int Report_Type = Bit_Ops.Bit2;
        public static int Spare_1 = Bit_Ops.Bit1;
        public static int WORD0_FX_Extension_indicator = Bit_Ops.Bit0;
    }
}
