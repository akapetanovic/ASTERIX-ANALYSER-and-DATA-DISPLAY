using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT01I042UserData
    {
        public class Azimuth_And_Distance_Type
        {
            public double Distance = 0.0;
            public double Azimuth = 0.0;
        }

        private static double XY_1 = 1.0 / 64.0; // 2 ** (-6) = 0.015625
        private static double XY_2 = XY_1 * 2.0;
        private static double XY_3 = XY_2 * 2.0;
        private static double XY_4 = XY_3 * 2.0;
        private static double XY_5 = XY_4 * 2.0;
        private static double XY_6 = XY_5 * 2.0;
        private static double XY_7 = XY_6 * 2.0;
        private static double XY_8 = XY_7 * 2.0;
        private static double XY_9 = XY_8 * 2.0;
        private static double XY_10 = XY_9 * 2.0;
        private static double XY_11 = XY_10 * 2.0;
        private static double XY_12 = XY_11 * 2.0;
        private static double XY_13 = XY_12 * 2.0;
        private static double XY_14 = XY_13 * 2.0;
        private static double XY_15 = XY_14 * 2.0;
        private static double XY_16 = XY_15 * 2.0;

        public static void DecodeCAT01I042(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();
            double Result = 0.0;

            CAT01I042Types.CAT01I042CalculatedPositionInCartesianCoordinates MyCAT01I042 = new CAT01I042Types.CAT01I042CalculatedPositionInCartesianCoordinates();

            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 4];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 3];
            BO.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 2];
            BO.DWord[Bit_Ops.Bits24_31_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 1];

            // Decode first X component
            // Check if this is a negative altitude.
            // and then handle it properly
            if (BO.DWord[Bit_Ops.Bit31] == true)
            {
                BO.DWord[Bit_Ops.Bit16] = !BO.DWord[Bit_Ops.Bit16];
                BO.DWord[Bit_Ops.Bit17] = !BO.DWord[Bit_Ops.Bit17];
                BO.DWord[Bit_Ops.Bit18] = !BO.DWord[Bit_Ops.Bit18];
                BO.DWord[Bit_Ops.Bit19] = !BO.DWord[Bit_Ops.Bit19];
                BO.DWord[Bit_Ops.Bit20] = !BO.DWord[Bit_Ops.Bit20];
                BO.DWord[Bit_Ops.Bit21] = !BO.DWord[Bit_Ops.Bit21];
                BO.DWord[Bit_Ops.Bit22] = !BO.DWord[Bit_Ops.Bit22];
                BO.DWord[Bit_Ops.Bit23] = !BO.DWord[Bit_Ops.Bit23];
                BO.DWord[Bit_Ops.Bit24] = !BO.DWord[Bit_Ops.Bit24];
                BO.DWord[Bit_Ops.Bit25] = !BO.DWord[Bit_Ops.Bit25];
                BO.DWord[Bit_Ops.Bit26] = !BO.DWord[Bit_Ops.Bit26];
                BO.DWord[Bit_Ops.Bit27] = !BO.DWord[Bit_Ops.Bit27];
                BO.DWord[Bit_Ops.Bit28] = !BO.DWord[Bit_Ops.Bit28];
                BO.DWord[Bit_Ops.Bit29] = !BO.DWord[Bit_Ops.Bit29];
                BO.DWord[Bit_Ops.Bit30] = !BO.DWord[Bit_Ops.Bit30];
                BO.DWord[Bit_Ops.Bit31] = !BO.DWord[Bit_Ops.Bit31];

                BO.DWord[Bit_Ops.Bits0_15_Of_DWord] = BO.DWord[Bit_Ops.Bits0_15_Of_DWord] + 1;

                if (BO.DWord[Bit_Ops.Bit16] == true)
                    Result = Result + XY_1;
                if (BO.DWord[Bit_Ops.Bit17] == true)
                    Result = Result + XY_2;
                if (BO.DWord[Bit_Ops.Bit18] == true)
                    Result = Result + XY_3;
                if (BO.DWord[Bit_Ops.Bit19] == true)
                    Result = Result + XY_4;
                if (BO.DWord[Bit_Ops.Bit20] == true)
                    Result = Result + XY_5;
                if (BO.DWord[Bit_Ops.Bit21] == true)
                    Result = Result + XY_6;
                if (BO.DWord[Bit_Ops.Bit22] == true)
                    Result = Result + XY_7;
                if (BO.DWord[Bit_Ops.Bit23] == true)
                    Result = Result + XY_8;
                if (BO.DWord[Bit_Ops.Bit24] == true)
                    Result = Result + XY_9;
                if (BO.DWord[Bit_Ops.Bit25] == true)
                    Result = Result + XY_10;
                if (BO.DWord[Bit_Ops.Bit26] == true)
                    Result = Result + XY_11;
                if (BO.DWord[Bit_Ops.Bit27] == true)
                    Result = Result + XY_12;
                if (BO.DWord[Bit_Ops.Bit28] == true)
                    Result = Result + XY_13;
                if (BO.DWord[Bit_Ops.Bit29] == true)
                    Result = Result + XY_14;
                if (BO.DWord[Bit_Ops.Bit30] == true)
                    Result = Result + XY_15;

                Result = -Result;
            }
            else
            {
                if (BO.DWord[Bit_Ops.Bit16] == true)
                    Result = Result + XY_1;
                if (BO.DWord[Bit_Ops.Bit17] == true)
                    Result = Result + XY_2;
                if (BO.DWord[Bit_Ops.Bit18] == true)
                    Result = Result + XY_3;
                if (BO.DWord[Bit_Ops.Bit19] == true)
                    Result = Result + XY_4;
                if (BO.DWord[Bit_Ops.Bit20] == true)
                    Result = Result + XY_5;
                if (BO.DWord[Bit_Ops.Bit21] == true)
                    Result = Result + XY_6;
                if (BO.DWord[Bit_Ops.Bit22] == true)
                    Result = Result + XY_7;
                if (BO.DWord[Bit_Ops.Bit23] == true)
                    Result = Result + XY_8;
                if (BO.DWord[Bit_Ops.Bit24] == true)
                    Result = Result + XY_9;
                if (BO.DWord[Bit_Ops.Bit25] == true)
                    Result = Result + XY_10;
                if (BO.DWord[Bit_Ops.Bit26] == true)
                    Result = Result + XY_11;
                if (BO.DWord[Bit_Ops.Bit27] == true)
                    Result = Result + XY_12;
                if (BO.DWord[Bit_Ops.Bit28] == true)
                    Result = Result + XY_13;
                if (BO.DWord[Bit_Ops.Bit29] == true)
                    Result = Result + XY_14;
                if (BO.DWord[Bit_Ops.Bit30] == true)
                    Result = Result + XY_15;
                if (BO.DWord[Bit_Ops.Bit31] == true)
                    Result = Result + XY_16;
            }

            MyCAT01I042.X = Result;
            Result = 0.0;

            // Decode first Y component
            // Check if this is a negative altitude.
            // and then handle it properly
            if (BO.DWord[Bit_Ops.Bit15] == true)
            {
                BO.DWord[Bit_Ops.Bit0] = !BO.DWord[Bit_Ops.Bit0];
                BO.DWord[Bit_Ops.Bit1] = !BO.DWord[Bit_Ops.Bit1];
                BO.DWord[Bit_Ops.Bit2] = !BO.DWord[Bit_Ops.Bit2];
                BO.DWord[Bit_Ops.Bit3] = !BO.DWord[Bit_Ops.Bit3];
                BO.DWord[Bit_Ops.Bit4] = !BO.DWord[Bit_Ops.Bit4];
                BO.DWord[Bit_Ops.Bit5] = !BO.DWord[Bit_Ops.Bit5];
                BO.DWord[Bit_Ops.Bit6] = !BO.DWord[Bit_Ops.Bit6];
                BO.DWord[Bit_Ops.Bit7] = !BO.DWord[Bit_Ops.Bit7];
                BO.DWord[Bit_Ops.Bit8] = !BO.DWord[Bit_Ops.Bit8];
                BO.DWord[Bit_Ops.Bit9] = !BO.DWord[Bit_Ops.Bit9];
                BO.DWord[Bit_Ops.Bit10] = !BO.DWord[Bit_Ops.Bit10];
                BO.DWord[Bit_Ops.Bit11] = !BO.DWord[Bit_Ops.Bit11];
                BO.DWord[Bit_Ops.Bit12] = !BO.DWord[Bit_Ops.Bit12];
                BO.DWord[Bit_Ops.Bit13] = !BO.DWord[Bit_Ops.Bit13];
                BO.DWord[Bit_Ops.Bit14] = !BO.DWord[Bit_Ops.Bit14];
                BO.DWord[Bit_Ops.Bit15] = !BO.DWord[Bit_Ops.Bit15];

                BO.DWord[Bit_Ops.Bits16_31_Of_DWord] = BO.DWord[Bit_Ops.Bits16_31_Of_DWord] + 1;

                if (BO.DWord[Bit_Ops.Bit0] == true)
                    Result = Result + XY_1;
                if (BO.DWord[Bit_Ops.Bit1] == true)
                    Result = Result + XY_2;
                if (BO.DWord[Bit_Ops.Bit2] == true)
                    Result = Result + XY_3;
                if (BO.DWord[Bit_Ops.Bit3] == true)
                    Result = Result + XY_4;
                if (BO.DWord[Bit_Ops.Bit4] == true)
                    Result = Result + XY_5;
                if (BO.DWord[Bit_Ops.Bit5] == true)
                    Result = Result + XY_6;
                if (BO.DWord[Bit_Ops.Bit6] == true)
                    Result = Result + XY_7;
                if (BO.DWord[Bit_Ops.Bit7] == true)
                    Result = Result + XY_8;
                if (BO.DWord[Bit_Ops.Bit8] == true)
                    Result = Result + XY_9;
                if (BO.DWord[Bit_Ops.Bit9] == true)
                    Result = Result + XY_10;
                if (BO.DWord[Bit_Ops.Bit10] == true)
                    Result = Result + XY_11;
                if (BO.DWord[Bit_Ops.Bit11] == true)
                    Result = Result + XY_12;
                if (BO.DWord[Bit_Ops.Bit12] == true)
                    Result = Result + XY_13;
                if (BO.DWord[Bit_Ops.Bit13] == true)
                    Result = Result + XY_14;
                if (BO.DWord[Bit_Ops.Bit14] == true)
                    Result = Result + XY_15;

                Result = -Result;
            }
            else
            {
                if (BO.DWord[Bit_Ops.Bit0] == true)
                    Result = Result + XY_1;
                if (BO.DWord[Bit_Ops.Bit1] == true)
                    Result = Result + XY_2;
                if (BO.DWord[Bit_Ops.Bit2] == true)
                    Result = Result + XY_3;
                if (BO.DWord[Bit_Ops.Bit3] == true)
                    Result = Result + XY_4;
                if (BO.DWord[Bit_Ops.Bit4] == true)
                    Result = Result + XY_5;
                if (BO.DWord[Bit_Ops.Bit5] == true)
                    Result = Result + XY_6;
                if (BO.DWord[Bit_Ops.Bit6] == true)
                    Result = Result + XY_7;
                if (BO.DWord[Bit_Ops.Bit7] == true)
                    Result = Result + XY_8;
                if (BO.DWord[Bit_Ops.Bit8] == true)
                    Result = Result + XY_9;
                if (BO.DWord[Bit_Ops.Bit9] == true)
                    Result = Result + XY_10;
                if (BO.DWord[Bit_Ops.Bit10] == true)
                    Result = Result + XY_11;
                if (BO.DWord[Bit_Ops.Bit11] == true)
                    Result = Result + XY_12;
                if (BO.DWord[Bit_Ops.Bit12] == true)
                    Result = Result + XY_13;
                if (BO.DWord[Bit_Ops.Bit13] == true)
                    Result = Result + XY_14;
                if (BO.DWord[Bit_Ops.Bit14] == true)
                    Result = Result + XY_15;
                if (BO.DWord[Bit_Ops.Bit15] == true)
                    Result = Result + XY_16;
            }

            MyCAT01I042.Y = Result;

            Azimuth_And_Distance_Type CalculatedGSPDandHDG = ToPolarFromCarteisan(MyCAT01I042.X, MyCAT01I042.Y);

            DecodeAzimuthAndDistance(ref MyCAT01I042.LatLong, CalculatedGSPDandHDG.Distance, CalculatedGSPDandHDG.Azimuth);
            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT01.I001DataItems[CAT01.ItemIDToIndex("042")].value = MyCAT01I042;
            //////////////////////////////////////////////////////////////////////////////////

            CAT01.CurrentDataBufferOctalIndex = CAT01.CurrentDataBufferOctalIndex + 4;
        }

        public static Azimuth_And_Distance_Type ToPolarFromCarteisan(double Vx, double Vy)
        {
            Azimuth_And_Distance_Type ReturnValue = new Azimuth_And_Distance_Type();
            ReturnValue.Distance = Math.Pow((Math.Pow(Vx, 2) + Math.Pow(Vy, 2)), 0.5);
            ReturnValue.Azimuth = (Math.Atan2(Vx, Vy) * (180.0 / Math.PI));
            return ReturnValue;
        }

        private static void DecodeAzimuthAndDistance(ref GeoCordSystemDegMinSecUtilities.LatLongClass NewPosition, double Distance, double Azimuth)
        {
            GeoCordSystemDegMinSecUtilities.LatLongClass ResultPosition = new GeoCordSystemDegMinSecUtilities.LatLongClass();

            //////////////////////////////////////////////////////////////////////////////////
            //
            // Here loop through the defined radars and determine the source of the data.
            // Once the source is determined calculate the extact position of the target
            // by taking the position of the radar and applying the range and bearing.
            // Display time of reception
            // Extract the cu
            // rrent SIC/SAC so the correct radar can be applied
            //
            ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)CAT01.I001DataItems[CAT01.ItemIDToIndex("010")].value;
            foreach (SystemAdaptationDataSet.Radar RDS in SystemAdaptationDataSet.RadarDataSet)
            {
                // If the current SIC/SAC code matched the code of one of the defined radars
                // then go ahead and calculate the Lat/Long position.
                if (RDS.SIC == SIC_SAC_TIME.SIC.ToString() && RDS.SAC == SIC_SAC_TIME.SAC.ToString())
                {
                    ResultPosition = GeoCordSystemDegMinSecUtilities.CalculateNewPosition(RDS.RadarPosition, Distance, Azimuth);
                }
            }

            NewPosition.SetPosition(ResultPosition.GetLatLongDecimal());
        }
    }
}
