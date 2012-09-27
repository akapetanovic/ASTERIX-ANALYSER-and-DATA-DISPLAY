using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I380Types
    {
        // WORD 0 
        public static int Target_Address = Bit_Ops.Bit7;
        public static int Target_Identification = Bit_Ops.Bit6;
        public static int Magnetic_Heading = Bit_Ops.Bit5;
        public static int Indicated_Airspeed_Mach_Number = Bit_Ops.Bit4;
        public static int True_Airspeed = Bit_Ops.Bit3;
        public static int Selected_Altitude = Bit_Ops.Bit2;
        public static int Final_State_SelectedAltitude = Bit_Ops.Bit1;
        public static int WORD0_FX_Extension_Indicator = Bit_Ops.Bit0;
        // WORD 1
        public static int Trajectory_Intent_Status = Bit_Ops.Bit7;
        public static int Trajectory_Intent_Data = Bit_Ops.Bit6;
        public static int Communications_ACAS = Bit_Ops.Bit5;
        public static int Status_Reported_By_ADS_B = Bit_Ops.Bit4;
        public static int ACAS_Resolution_Advisory_Report = Bit_Ops.Bit3;
        public static int Barometric_Vertical_Rate = Bit_Ops.Bit2;
        public static int Geometric_Vertical_Rate = Bit_Ops.Bit1;
        public static int WORD1_FX_Extension_Indicator = Bit_Ops.Bit0;
        // WORD 2
        public static int Roll_Angle = Bit_Ops.Bit7;
        public static int Track_Angle_Rate = Bit_Ops.Bit6;
        public static int Track_Angle = Bit_Ops.Bit5;
        public static int Ground_Speed = Bit_Ops.Bit4;
        public static int Velocity_Uncertainty = Bit_Ops.Bit3;
        public static int Meteorological_Data = Bit_Ops.Bit2;
        public static int Emitter_Category = Bit_Ops.Bit1;
        public static int WORD2_FX_Extension_Indicator = Bit_Ops.Bit0;
        // WORD 3
        public static int Position_Data = Bit_Ops.Bit7;
        public static int Geometric_Altitude_Data = Bit_Ops.Bit6;
        public static int Position_Uncertainty_Data = Bit_Ops.Bit5;
        public static int Mode_S_MB_Data = Bit_Ops.Bit4;
        public static int Indicated_Airspeed = Bit_Ops.Bit3;
        public static int Mach_Number = Bit_Ops.Bit2;
        public static int Barometric_Pressure_Setting = Bit_Ops.Bit1;
        public static int WORD3_FX_Extension_Indicator = Bit_Ops.Bit0;
    }
}
