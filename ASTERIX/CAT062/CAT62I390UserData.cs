using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I390UserData
    {

        public static void DecodeCAT62I390(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops WORD0 = new Bit_Ops();
            Bit_Ops WORD1 = new Bit_Ops();
            Bit_Ops WORD2 = new Bit_Ops();

            //Extract the first octet
            WORD0.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

            if (WORD0.DWord[CAT62I390Types.WORD0_FX_Extension_Indicator] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                WORD1.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                if (WORD1.DWord[CAT62I390Types.WORD1_FX_Extension_Indicator] == true)
                {
                    CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                    WORD2.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                    if (WORD2.DWord[CAT62I390Types.WORD2_FX_Extension_Indicator] == true)
                    {
                        CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                    }
                }
            }

            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;

            // 2 SIC/SAC
            if (WORD0.DWord[CAT62I390Types.FPPS_Identification_Tag] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }

            // 7 (7 Char Callsign)
            if (WORD0.DWord[CAT62I390Types.Callsign] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 7;
            }
            
            // 4 
            if (WORD0.DWord[CAT62I390Types.IFPS_FLIGHT_ID] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 4;
            }

            // 1
            if (WORD0.DWord[CAT62I390Types.Flight_Category] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            // 4
            if (WORD0.DWord[CAT62I390Types.Type_of_Aircraft] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 4;
            }

            // 1
            if (WORD0.DWord[CAT62I390Types.Wake_Turbulence_Category] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            // 4
            if (WORD0.DWord[CAT62I390Types.Departure_Airport] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 4;
            }

            // 4
            if (WORD1.DWord[CAT62I390Types.Destination_Airport] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 4;
            }

            // 3
            if (WORD1.DWord[CAT62I390Types.Runway_Designation] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 3;
            }

            // 2
            if (WORD1.DWord[CAT62I390Types.Current_Cleared_Flight_Level] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }

            // 2
            if (WORD1.DWord[CAT62I390Types.Current_Control_Position] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }

            // 5
            if (WORD1.DWord[CAT62I390Types.Time_of_Departure_Arrival] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 5;
            }

            // 6
            if (WORD1.DWord[CAT62I390Types.Aircraft_Stand] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 6;
            }

            // 1
            if (WORD1.DWord[CAT62I390Types.Stand_Status] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            // 7
            if (WORD2.DWord[CAT62I390Types.Standard_Instrument_Departure] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 7;
            }
           
            // 7
            if (WORD2.DWord[CAT62I390Types.Standard_Instrument_Arrival] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 7;
            }

            // 2
            if (WORD2.DWord[CAT62I390Types.Pre_emergency_Mode_3_A_Code] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }

            // 7
            if (WORD2.DWord[CAT62I390Types.Pre_emergency_Callsign] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 7;
            }

            ////bits-4/2 Spare bits set to zero
        }
    }
}
