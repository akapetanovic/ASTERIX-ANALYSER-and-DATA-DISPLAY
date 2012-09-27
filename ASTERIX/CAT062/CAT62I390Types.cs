using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I390Types
    {
        // WORD 0 
        public static int FPPS_Identification_Tag = Bit_Ops.Bit7;
        public static int Callsign = Bit_Ops.Bit6;
        public static int IFPS_FLIGHT_ID = Bit_Ops.Bit5;
        public static int Flight_Category = Bit_Ops.Bit4;
        public static int Type_of_Aircraft = Bit_Ops.Bit3;
        public static int Wake_Turbulence_Category = Bit_Ops.Bit2;
        public static int Departure_Airport = Bit_Ops.Bit1;
        public static int WORD0_FX_Extension_Indicator = Bit_Ops.Bit0;
        // WORD 1
        public static int Destination_Airport = Bit_Ops.Bit7;
        public static int Runway_Designation = Bit_Ops.Bit6;
        public static int Current_Cleared_Flight_Level = Bit_Ops.Bit5;
        public static int Current_Control_Position = Bit_Ops.Bit4;
        public static int Time_of_Departure_Arrival = Bit_Ops.Bit3;
        public static int Aircraft_Stand = Bit_Ops.Bit2;
        public static int Stand_Status = Bit_Ops.Bit1;
        public static int WORD1_FX_Extension_Indicator = Bit_Ops.Bit0;
        // WORD 2
        public static int Standard_Instrument_Departure = Bit_Ops.Bit7;
        public static int Standard_Instrument_Arrival = Bit_Ops.Bit6;
        public static int Pre_emergency_Mode_3_A_Code = Bit_Ops.Bit5;
        public static int Pre_emergency_Callsign = Bit_Ops.Bit4;
        //bits-4/2 Spare bits set to zero
        public static int WORD2_FX_Extension_Indicator = Bit_Ops.Bit0;

    }
}
