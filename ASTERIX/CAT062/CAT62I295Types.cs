using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I295Types
    {
        // WORD 0 
        public static int Measured_Flight_Level_Age = Bit_Ops.Bit7;
        public static int Mode_1_Age = Bit_Ops.Bit6;
        public static int Mode_2_Age = Bit_Ops.Bit5;
        public static int Mode_3_A_Age = Bit_Ops.Bit4;
        public static int Mode_4_Age = Bit_Ops.Bit3;
        public static int Mode_5_Age = Bit_Ops.Bit2;
        public static int Magnetic_Heading_Age = Bit_Ops.Bit1;
        public static int WORD0_FX_Extension_Indicator = Bit_Ops.Bit0;
        // WORD 1
        public static int Indicated_Airspeed_Mach_Nb_Age = Bit_Ops.Bit7;
        public static int True_Airspeed_Age = Bit_Ops.Bit6;
        public static int Selected_Altitude_Age = Bit_Ops.Bit5;
        public static int Final_State_Selected_Altitude_Age = Bit_Ops.Bit4;
        public static int Trajectory_Intent_Data_Age = Bit_Ops.Bit3;
        public static int COM_ACAS_Capability_and_Flight_Status_Age = Bit_Ops.Bit2;
        public static int Status_Reported_by_ADS_B_Age = Bit_Ops.Bit1;
        public static int WORD1_FX_Extension_Indicator = Bit_Ops.Bit0;
        // WORD 2
        public static int ACAS_Resolution_Advisory_Report_Age = Bit_Ops.Bit7;
        public static int Barometric_Vertical_Rate_Age = Bit_Ops.Bit6;
        public static int Geometric_Vertical_Rate_Age = Bit_Ops.Bit5;
        public static int Roll_Angle_Age = Bit_Ops.Bit4;
        public static int Track_Angle_Rate_Age = Bit_Ops.Bit3;
        public static int Track_Angle_Age = Bit_Ops.Bit2;
        public static int Ground_Speed_Age = Bit_Ops.Bit1;
        public static int WORD2_FX_Extension_Indicator = Bit_Ops.Bit0;
        // WORD 3
        public static int Velocity_Uncertainty_Age = Bit_Ops.Bit7;
        public static int Meteorological_Data_Age = Bit_Ops.Bit6;
        public static int Emitter_Category_Age = Bit_Ops.Bit5;
        public static int Position_Data_Age = Bit_Ops.Bit4;
        public static int Geometric_Altitude_Data_Age = Bit_Ops.Bit3;
        public static int Position_Uncertainty_Data_Age = Bit_Ops.Bit2;
        public static int Mode_S_MB_Data_Age = Bit_Ops.Bit1;
        public static int WORD3_FX_Extension_Indicator = Bit_Ops.Bit0;
        // WORD 4
        public static int Indicated_Airspeed_Data_Age = Bit_Ops.Bit7;
        public static int Mach_NumberAData_Age = Bit_Ops.Bit6;
        public static int Barometric_Pressure_Setting_Data_Age = Bit_Ops.Bit5;
        public static int Spare_1 = Bit_Ops.Bit4;
        public static int Spare_2 = Bit_Ops.Bit3;
        public static int Spare_3 = Bit_Ops.Bit2;
        public static int Spare_4 = Bit_Ops.Bit1;
        public static int WORD4_FX_Extension_Indicator = Bit_Ops.Bit0;
    }
}
