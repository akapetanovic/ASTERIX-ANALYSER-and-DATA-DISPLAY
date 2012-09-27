using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I380UserData
    {

        public static void DecodeCAT62I380(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops WORD0 = new Bit_Ops();
            Bit_Ops WORD1 = new Bit_Ops();
            Bit_Ops WORD2 = new Bit_Ops();
            Bit_Ops WORD3 = new Bit_Ops();

            //Extract the first octet
            WORD0.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

            if (WORD0.DWord[CAT62I380Types.WORD0_FX_Extension_Indicator] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                WORD1.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                if (WORD1.DWord[CAT62I380Types.WORD1_FX_Extension_Indicator] == true)
                {
                    CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                    WORD2.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                    if (WORD2.DWord[CAT62I380Types.WORD2_FX_Extension_Indicator] == true)
                    {
                        CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                        WORD3.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];
                        if (WORD3.DWord[CAT62I380Types.WORD3_FX_Extension_Indicator] == true)
                        {
                            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                        }
                    }
                }
            }

            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;

            // WORD0
            if (WORD0.DWord[CAT62I380Types.Target_Address] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 3;
            }
            if (WORD0.DWord[CAT62I380Types.Target_Identification] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 6;
            }
            if (WORD0.DWord[CAT62I380Types.Magnetic_Heading] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD0.DWord[CAT62I380Types.Indicated_Airspeed_Mach_Number] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD0.DWord[CAT62I380Types.True_Airspeed] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD0.DWord[CAT62I380Types.Selected_Altitude] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD0.DWord[CAT62I380Types.Final_State_SelectedAltitude] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            // WORD1
            if (WORD1.DWord[CAT62I380Types.Trajectory_Intent_Status] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD1.DWord[CAT62I380Types.Trajectory_Intent_Data] == true)
            {
                // Repetitive Data Item starting with a one-octet Field Repetition
                // Indicator (REP) followed by at least one Trajectory Intent Point
                // comprising fifteen octets
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 16;
            }
            if (WORD1.DWord[CAT62I380Types.Communications_ACAS] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD1.DWord[CAT62I380Types.Status_Reported_By_ADS_B] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD1.DWord[CAT62I380Types.ACAS_Resolution_Advisory_Report] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 7;
            }
            if (WORD1.DWord[CAT62I380Types.Barometric_Vertical_Rate] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD1.DWord[CAT62I380Types.Geometric_Vertical_Rate] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            // WORD2
            if (WORD2.DWord[CAT62I380Types.Roll_Angle] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD2.DWord[CAT62I380Types.Track_Angle_Rate] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD2.DWord[CAT62I380Types.Track_Angle] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD2.DWord[CAT62I380Types.Ground_Speed] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD2.DWord[CAT62I380Types.Velocity_Uncertainty] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD2.DWord[CAT62I380Types.Meteorological_Data] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 8;
            }
            if (WORD2.DWord[CAT62I380Types.Emitter_Category] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            // WORD3
            if (WORD3.DWord[CAT62I380Types.Position_Data] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 6;
            }
            if (WORD3.DWord[CAT62I380Types.Geometric_Altitude_Data] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD3.DWord[CAT62I380Types.Position_Uncertainty_Data] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD3.DWord[CAT62I380Types.Mode_S_MB_Data] == true)
            {
                // Repetitive starting with an one-octet Field Repetition Indicator
                // (REP) followed by at least one BDS report comprising one seven
                // octet BDS register and one octet BDS code.
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 9;
            }
            if (WORD3.DWord[CAT62I380Types.Indicated_Airspeed] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD3.DWord[CAT62I380Types.Mach_Number] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }
            if (WORD3.DWord[CAT62I380Types.Barometric_Pressure_Setting] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }

        }
    }
}
