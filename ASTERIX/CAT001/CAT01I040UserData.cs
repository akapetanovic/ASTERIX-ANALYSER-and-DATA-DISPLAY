using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT01I040UserData
    {

        /// <summary>
        /////////////////////////////////////////////////////////////////
        /// Define distance fix point values from LSB to MSB
        /////////////////////////////////////////////////////////////////
        private const double RHO_1 = 1.0 / 128.0;
        private const double RHO_2 = RHO_1 * 2.0;
        private const double RHO_3 = RHO_2 * 2.0;
        private const double RHO_4 = RHO_3 * 2.0;
        private const double RHO_5 = RHO_4 * 2.0;
        private const double RHO_6 = RHO_5 * 2.0;
        private const double RHO_7 = RHO_6 * 2.0;
        private const double RHO_8 = RHO_7 * 2.0;
        private const double RHO_9 = RHO_8 * 2.0;
        private const double RHO_10 = RHO_9 * 2.0;
        private const double RHO_11 = RHO_10 * 2.0;
        private const double RHO_12 = RHO_11 * 2.0;
        private const double RHO_13 = RHO_12 * 2.0;
        private const double RHO_14 = RHO_13 * 2.0;
        private const double RHO_15 = RHO_14 * 2.0;
        private const double RHO_16 = RHO_15 * 2.0;

        ///////////////////////////////////////////////////////////////////
        /// Define azumuth fix point values from LSB to MSB
        ///////////////////////////////////////////////////////////////////
        private const double THETA_1 = 360.0 / 65536.0; // 2 ** 16 = 65536
        private const double THETA_2 = THETA_1 * 2.0;
        private const double THETA_3 = THETA_2 * 2.0;
        private const double THETA_4 = THETA_3 * 2.0;
        private const double THETA_5 = THETA_4 * 2.0;
        private const double THETA_6 = THETA_5 * 2.0;
        private const double THETA_7 = THETA_6 * 2.0;
        private const double THETA_8 = THETA_7 * 2.0;
        private const double THETA_9 = THETA_8 * 2.0;
        private const double THETA_10 = THETA_9 * 2.0;
        private const double THETA_11 = THETA_10 * 2.0;
        private const double THETA_12 = THETA_11 * 2.0;
        private const double THETA_13 = THETA_12 * 2.0;
        private const double THETA_14 = THETA_13 * 2.0;
        private const double THETA_15 = THETA_14 * 2.0;
        private const double THETA_16 = THETA_15 * 2.0;

        public static void DecodeCAT01I040(byte[] Data)
        {
            CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates MyCAT01I040 = new CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates();

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 4];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 3];
            BO.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 2];
            BO.DWord[Bit_Ops.Bits24_31_Of_DWord] = Data[CAT01.CurrentDataBufferOctalIndex + 1];

            DecodeAzimuthAndDistance(ref MyCAT01I040.LatLong, out MyCAT01I040.Measured_Distance, out MyCAT01I040.Measured_Azimuth, BO);

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT01.I001DataItems[CAT01.ItemIDToIndex("040")].value = MyCAT01I040;
            //////////////////////////////////////////////////////////////////////////////////

            // Leave it at the current index for the next decode
            CAT01.CurrentDataBufferOctalIndex = CAT01.CurrentDataBufferOctalIndex + 4;
        }

        private static void DecodeAzimuthAndDistance(ref GeoCordSystemDegMinSecUtilities.LatLongClass NewPosition, out double Distance, out double Azimuth, Bit_Ops BO)
        {
            double Distance_Loc = 0.0;
            double Azimuth_Loc = 0.0;
            GeoCordSystemDegMinSecUtilities.LatLongClass ResultPosition = new GeoCordSystemDegMinSecUtilities.LatLongClass();
            ///////////////////////////////////////////////////////////////////////////////////////
            // Decode Distance
            ///////////////////////////////////////////////////////////////////////////////////////
            if (BO.DWord[Bit_Ops.Bit16] == true)
                Distance_Loc = RHO_1;
            if (BO.DWord[Bit_Ops.Bit17] == true)
                Distance_Loc = Distance_Loc + RHO_2;
            if (BO.DWord[Bit_Ops.Bit18] == true)
                Distance_Loc = Distance_Loc + RHO_3;
            if (BO.DWord[Bit_Ops.Bit19] == true)
                Distance_Loc = Distance_Loc + RHO_4;
            if (BO.DWord[Bit_Ops.Bit20] == true)
                Distance_Loc = Distance_Loc + RHO_5;
            if (BO.DWord[Bit_Ops.Bit21] == true)
                Distance_Loc = Distance_Loc + RHO_6;
            if (BO.DWord[Bit_Ops.Bit22] == true)
                Distance_Loc = Distance_Loc + RHO_7;
            if (BO.DWord[Bit_Ops.Bit23] == true)
                Distance_Loc = Distance_Loc + RHO_8;
            if (BO.DWord[Bit_Ops.Bit24] == true)
                Distance_Loc = Distance_Loc + RHO_9;
            if (BO.DWord[Bit_Ops.Bit25] == true)
                Distance_Loc = Distance_Loc + RHO_10;
            if (BO.DWord[Bit_Ops.Bit26] == true)
                Distance_Loc = Distance_Loc + RHO_11;
            if (BO.DWord[Bit_Ops.Bit27] == true)
                Distance_Loc = Distance_Loc + RHO_12;
            if (BO.DWord[Bit_Ops.Bit28] == true)
                Distance_Loc = Distance_Loc + RHO_13;
            if (BO.DWord[Bit_Ops.Bit29] == true)
                Distance_Loc = Distance_Loc + RHO_14;
            if (BO.DWord[Bit_Ops.Bit30] == true)
                Distance_Loc = Distance_Loc + RHO_15;
            if (BO.DWord[Bit_Ops.Bit31] == true)
                Distance_Loc = Distance_Loc + RHO_16;

            ///////////////////////////////////////////////////////////////////////////////////////
            // Decode Azimuth
            ///////////////////////////////////////////////////////////////////////////////////////
            if (BO.DWord[Bit_Ops.Bit0] == true)
                Azimuth_Loc = THETA_1;
            if (BO.DWord[Bit_Ops.Bit1] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_2;
            if (BO.DWord[Bit_Ops.Bit2] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_3;
            if (BO.DWord[Bit_Ops.Bit3] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_4;
            if (BO.DWord[Bit_Ops.Bit4] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_5;
            if (BO.DWord[Bit_Ops.Bit5] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_6;
            if (BO.DWord[Bit_Ops.Bit6] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_7;
            if (BO.DWord[Bit_Ops.Bit7] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_8;
            if (BO.DWord[Bit_Ops.Bit8] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_9;
            if (BO.DWord[Bit_Ops.Bit9] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_10;
            if (BO.DWord[Bit_Ops.Bit10] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_11;
            if (BO.DWord[Bit_Ops.Bit11] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_12;
            if (BO.DWord[Bit_Ops.Bit12] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_13;
            if (BO.DWord[Bit_Ops.Bit13] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_14;
            if (BO.DWord[Bit_Ops.Bit14] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_15;
            if (BO.DWord[Bit_Ops.Bit15] == true)
                Azimuth_Loc = Azimuth_Loc + THETA_16;

            Azimuth = Azimuth_Loc;
            Distance = Distance_Loc;

            //////////////////////////////////////////////////////////////////////////////////
            //
            // Here loop through the defined radars and determine the source of the data.
            // Once the source is determined calculate the extact position of the target
            // by taking the position of the radar and applying the range and bearing.
            // Display time of reception
            //
            // Extract the current SIC/SAC so the correct radar can be applied
            //
            ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)CAT01.I001DataItems[CAT01.ItemIDToIndex("010")].value;
            foreach (SystemAdaptationDataSet.Radar RDS in SystemAdaptationDataSet.RadarDataSet)
            {
                // If the current SIC/SAC code matched the code of one of the defined radars
                // then go ahead and calculate the Lat/Long position.
                if (RDS.SIC == SIC_SAC_TIME.SIC.ToString() && RDS.SAC == SIC_SAC_TIME.SAC.ToString())
                {
                    ResultPosition = GeoCordSystemDegMinSecUtilities.CalculateNewPosition(RDS.RadarPosition, Distance_Loc, Azimuth_Loc);
                }
            }

            NewPosition.SetPosition(ResultPosition.GetLatLongDecimal());
        }
    }
}
