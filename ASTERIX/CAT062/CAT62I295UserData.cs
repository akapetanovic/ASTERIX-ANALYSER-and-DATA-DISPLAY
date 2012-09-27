using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I295UserData
    {

        public static void DecodeCAT62I295(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops WORD0 = new Bit_Ops();
            Bit_Ops WORD1 = new Bit_Ops();
            Bit_Ops WORD2 = new Bit_Ops();
            Bit_Ops WORD3 = new Bit_Ops();
            Bit_Ops WORD4 = new Bit_Ops();

            //Extract the first octet
            WORD0.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

            if (WORD0.DWord[CAT62I295Types.WORD0_FX_Extension_Indicator] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                WORD1.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                if (WORD1.DWord[CAT62I295Types.WORD1_FX_Extension_Indicator] == true)
                {
                    CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                    WORD2.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                    if (WORD2.DWord[CAT62I295Types.WORD2_FX_Extension_Indicator] == true)
                    {
                        CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                        WORD3.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];
                        
                        if (WORD3.DWord[CAT62I295Types.WORD3_FX_Extension_Indicator] == true)
                        {
                            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                            WORD4.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

                            if (WORD4.DWord[CAT62I295Types.WORD4_FX_Extension_Indicator] == true)
                            {
                                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
                            }
                        }
                    }
                }
            }

            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;

            // WORD0
            if (WORD0.DWord[CAT62I295Types.Measured_Flight_Level_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD0.DWord[CAT62I295Types.Mode_1_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD0.DWord[CAT62I295Types.Mode_2_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD0.DWord[CAT62I295Types.Mode_3_A_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD0.DWord[CAT62I295Types.Mode_4_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD0.DWord[CAT62I295Types.Mode_5_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD0.DWord[CAT62I295Types.Magnetic_Heading_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            //WORD1
            if (WORD1.DWord[CAT62I295Types.Indicated_Airspeed_Mach_Nb_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD1.DWord[CAT62I295Types.True_Airspeed_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD1.DWord[CAT62I295Types.Selected_Altitude_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD1.DWord[CAT62I295Types.Final_State_Selected_Altitude_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD1.DWord[CAT62I295Types.Trajectory_Intent_Data_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD1.DWord[CAT62I295Types.COM_ACAS_Capability_and_Flight_Status_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD1.DWord[CAT62I295Types.Status_Reported_by_ADS_B_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            //WORD2
            if (WORD2.DWord[CAT62I295Types.ACAS_Resolution_Advisory_Report_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD2.DWord[CAT62I295Types.Barometric_Vertical_Rate_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD2.DWord[CAT62I295Types.Geometric_Vertical_Rate_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD2.DWord[CAT62I295Types.Roll_Angle_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD2.DWord[CAT62I295Types.Track_Angle_Rate_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD2.DWord[CAT62I295Types.Track_Angle_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD2.DWord[CAT62I295Types.Ground_Speed_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            //WORD3
            if (WORD3.DWord[CAT62I295Types.Velocity_Uncertainty_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD3.DWord[CAT62I295Types.Meteorological_Data_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD3.DWord[CAT62I295Types.Emitter_Category_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD3.DWord[CAT62I295Types.Position_Data_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD3.DWord[CAT62I295Types.Geometric_Altitude_Data_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD3.DWord[CAT62I295Types.Position_Uncertainty_Data_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD3.DWord[CAT62I295Types.Mode_S_MB_Data_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            //WORD4
            if (WORD4.DWord[CAT62I295Types.Indicated_Airspeed_Data_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD4.DWord[CAT62I295Types.Mach_NumberAData_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
            if (WORD4.DWord[CAT62I295Types.Barometric_Pressure_Setting_Data_Age] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }
        }
    }
}
