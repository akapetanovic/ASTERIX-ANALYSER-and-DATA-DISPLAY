using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I220UserData
    {
        
        public static void DecodeCAT62I220(byte[] Data)
        {
            // Define a global record for all data, then down there depending on the avalability of each field
            // populate specific items. Each item has validity flag that needs to be set for each available data
            // item for this message
            CAT62I220Types.CalculatedRateOfClimbDescent CAT62I220DataRecord = new CAT62I220Types.CalculatedRateOfClimbDescent();
            
            ///////////////////////////////////////////////////////////////
            // Track_Angle and Magnetic Heading DECODE CONSTANTS
            double RCD_1 = 6.25; // LSB Feet/MIN
            double RCD_2 = RCD_1 * 2.0;
            double RCD_3 = RCD_2 * 2.0;
            double RCD_4 = RCD_3 * 2.0;
            double RCD_5 = RCD_4 * 2.0;
            double RCD_6 = RCD_5 * 2.0;
            double RCD_7 = RCD_6 * 2.0;
            double RCD_8 = RCD_7 * 2.0;
            double RCD_9 = RCD_8 * 2.0;
            double RCD_10 = RCD_9 * 2.0;
            double RCD_11 = RCD_10 * 2.0;
            double RCD_12 = RCD_11 * 2.0;
            double RCD_13 = RCD_12 * 2.0;
            double RCD_14 = RCD_13 * 2.0;
            double RCD_15 = RCD_14 * 2.0;
            double RCD_16 = RCD_15 * 2.0; // MSB
            ///////////////////////////////////////////////////////////////////

            Bit_Ops BO = new Bit_Ops();
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex + 1];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

            double Value = 0.0;
            if (BO.DWord[Bit_Ops.Bit15])
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
                BO.DWord[Bit_Ops.Bits0_15_Of_DWord] = BO.DWord[Bit_Ops.Bits0_15_Of_DWord] + 1;

                if (BO.DWord[Bit_Ops.Bit0])
                    Value = RCD_1;
                if (BO.DWord[Bit_Ops.Bit1])
                    Value = Value + RCD_2;
                if (BO.DWord[Bit_Ops.Bit2])
                    Value = Value + RCD_3;
                if (BO.DWord[Bit_Ops.Bit3])
                    Value = Value + RCD_4;
                if (BO.DWord[Bit_Ops.Bit4])
                    Value = Value + RCD_5;
                if (BO.DWord[Bit_Ops.Bit5])
                    Value = Value + RCD_6;
                if (BO.DWord[Bit_Ops.Bit6])
                    Value = Value + RCD_7;
                if (BO.DWord[Bit_Ops.Bit7])
                    Value = Value + RCD_8;
                if (BO.DWord[Bit_Ops.Bit8])
                    Value = Value + RCD_9;
                if (BO.DWord[Bit_Ops.Bit9])
                    Value = Value + RCD_10;
                if (BO.DWord[Bit_Ops.Bit10])
                    Value = Value + RCD_11;
                if (BO.DWord[Bit_Ops.Bit11])
                    Value = Value + RCD_12;
                if (BO.DWord[Bit_Ops.Bit12])
                    Value = Value + RCD_13;
                if (BO.DWord[Bit_Ops.Bit13])
                    Value = Value + RCD_14;
                if (BO.DWord[Bit_Ops.Bit14])
                    Value = Value + RCD_15;
                if (BO.DWord[Bit_Ops.Bit15])
                    Value = Value + RCD_16;

                Value = -Value;
            }
            else
            {
                if (BO.DWord[Bit_Ops.Bit0])
                    Value = RCD_1;
                if (BO.DWord[Bit_Ops.Bit1])
                    Value = Value + RCD_2;
                if (BO.DWord[Bit_Ops.Bit2])
                    Value = Value + RCD_3;
                if (BO.DWord[Bit_Ops.Bit3])
                    Value = Value + RCD_4;
                if (BO.DWord[Bit_Ops.Bit4])
                    Value = Value + RCD_5;
                if (BO.DWord[Bit_Ops.Bit5])
                    Value = Value + RCD_6;
                if (BO.DWord[Bit_Ops.Bit6])
                    Value = Value + RCD_7;
                if (BO.DWord[Bit_Ops.Bit7])
                    Value = Value + RCD_8;
                if (BO.DWord[Bit_Ops.Bit8])
                    Value = Value + RCD_9;
                if (BO.DWord[Bit_Ops.Bit9])
                    Value = Value + RCD_10;
                if (BO.DWord[Bit_Ops.Bit10])
                    Value = Value + RCD_11;
                if (BO.DWord[Bit_Ops.Bit11])
                    Value = Value + RCD_12;
                if (BO.DWord[Bit_Ops.Bit12])
                    Value = Value + RCD_13;
                if (BO.DWord[Bit_Ops.Bit13])
                    Value = Value + RCD_14;
                if (BO.DWord[Bit_Ops.Bit14])
                    Value = Value + RCD_15;
            }

            CAT62I220DataRecord.Is_Valid = true;
            CAT62I220DataRecord.Value = Value;

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT62.I062DataItems[CAT62.ItemIDToIndex("220")].value = CAT62I220DataRecord;
            //////////////////////////////////////////////////////////////////////////////////

            // Increase data buffer index so it ready for the next data item.
            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
        }
    }
}
