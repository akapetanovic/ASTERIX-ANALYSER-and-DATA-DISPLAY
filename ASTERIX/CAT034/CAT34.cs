using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT34
    {
        /////////////////////////////////////////////////////////////////////////
        //
        // Next version of Category 002: PSR Radar, M-SSR Radar, Mode-S Station
        //                                               Length in bytes
        // 1 I034/010 Data Source Identifier                2
        // 2 I034/000 Message Type                          1
        // 3 I034/030 Time-of-Day                           3
        // 4 I034/020 Sector Number                         1
        // 5 I034/041 Antenna Rotation Period               2
        // 6 I034/050 System Configuration and Status       1+
        // 7 I034/060 System Processing Mode                1+
        // FX

        // 8 I034/070 Message Count Values                  1+2N
        // 9 I034/100 Generic Polar Window                  8
        // 10 I034/110 Data Filter                          1
        // 11 I034/120 3D-Position of Data Source           8
        // 12 I034/090 Collimation Error                    2
        // 13 RE-Data Item Reserved Expansion Field         1+1
        // 14 SP-Data Item Special Purpose Field            1+1
        // FX
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        /// 
        // Define a class that holds I001 data
        public class I034DataItem
        {
            public string ID;
            public string Description;
            public bool IsPresent;
            public object value;
            public string Time;

            // Constructor to set default
            // values.
            public I034DataItem()
            {
                ID = "N/A";
                Description = "N/A";
                IsPresent = false;
                value = null;
                Time = "N/A";
            }
        }

        // Conversion method to get index of the
        // data item from the collection based on the
        // string data item identifer
        public static int ItemIDToIndex(string ID)
        {
            int index = 0;

            switch (ID)
            {
                case "010":
                    index = 0;
                    break;
                case "000":
                    index = 1;
                    break;
                case "030":
                    index = 2;
                    break;
                case "020":
                    index = 3;
                    break;
                case "041":
                    index = 4;
                    break;
                case "050":
                    index = 5;
                    break;
                case "060":
                    index = 6;
                    break;
                case "070":
                    index = 7;
                    break;
                case "100":
                    index = 8;
                    break;
                case "110":
                    index = 9;
                    break;
                case "120":
                    index = 10;
                    break;
                case "090":
                    index = 11;
                    break;
                default:
                    break;
            }

            return index;
        }

        // Define collection of CAT001 data items. Used to store and retrieve basic data such as:
        // 
        public static System.Collections.Generic.List<I034DataItem> I034DataItemsLastValid = new System.Collections.Generic.List<I034DataItem>();
        // 1. Item presence
        // 2. Item description
        //
        // Based on the data item identifer
        public static System.Collections.Generic.List<I034DataItem> I034DataItems = new System.Collections.Generic.List<I034DataItem>();

        public static void Intitialize()
        {

            I034DataItemsLastValid.Clear();

            // 1 I034/010 Data Source Identifier 
            I034DataItemsLastValid.Add(new I034DataItem());
            I034DataItemsLastValid[ItemIDToIndex("010")].ID = "010";
            I034DataItemsLastValid[ItemIDToIndex("010")].Description = "Data Source Identifier";
            I034DataItemsLastValid[ItemIDToIndex("010")].IsPresent = false;

            // 2 I034/000 Message Type
            I034DataItemsLastValid.Add(new I034DataItem());
            I034DataItemsLastValid[ItemIDToIndex("000")].ID = "000";
            I034DataItemsLastValid[ItemIDToIndex("000")].Description = "Message Type";
            I034DataItemsLastValid[ItemIDToIndex("000")].IsPresent = false;


            // 3 I034/030 Time-of-Day 
            I034DataItemsLastValid.Add(new I034DataItem());
            I034DataItemsLastValid[ItemIDToIndex("030")].ID = "030";
            I034DataItemsLastValid[ItemIDToIndex("030")].Description = "Time-of-Day";
            I034DataItemsLastValid[ItemIDToIndex("030")].IsPresent = false;

            // 4 I034/020 Sector Number
            I034DataItemsLastValid.Add(new I034DataItem());
            I034DataItemsLastValid[ItemIDToIndex("020")].ID = "020";
            I034DataItemsLastValid[ItemIDToIndex("020")].Description = "Sector Number";
            I034DataItemsLastValid[ItemIDToIndex("020")].IsPresent = false;

            // 5 I034/041 Antenna Rotation Period 
            I034DataItemsLastValid.Add(new I034DataItem());
            I034DataItemsLastValid[ItemIDToIndex("041")].ID = "041";
            I034DataItemsLastValid[ItemIDToIndex("041")].Description = "Antenna Rotation Period ";
            I034DataItemsLastValid[ItemIDToIndex("041")].IsPresent = false;

            // 6 I034/050 System Configuration and Status 
            I034DataItemsLastValid.Add(new I034DataItem());
            I034DataItemsLastValid[ItemIDToIndex("050")].ID = "050";
            I034DataItemsLastValid[ItemIDToIndex("050")].Description = "System Configuration and Status";
            I034DataItemsLastValid[ItemIDToIndex("050")].IsPresent = false;

            // 7 I034/060 System Processing Mode                
            I034DataItemsLastValid.Add(new I034DataItem());
            I034DataItemsLastValid[ItemIDToIndex("060")].ID = "060";
            I034DataItemsLastValid[ItemIDToIndex("060")].Description = "System Processing Mode";
            I034DataItemsLastValid[ItemIDToIndex("060")].IsPresent = false;

            // 8 I034/070 Message Count Values
            I034DataItemsLastValid.Add(new I034DataItem());
            I034DataItemsLastValid[ItemIDToIndex("070")].ID = "070";
            I034DataItemsLastValid[ItemIDToIndex("070")].Description = "Message Count Values";
            I034DataItemsLastValid[ItemIDToIndex("070")].IsPresent = false;

            // 9 I034/100 Generic Polar Window
            I034DataItemsLastValid.Add(new I034DataItem());
            I034DataItemsLastValid[ItemIDToIndex("100")].ID = "100";
            I034DataItemsLastValid[ItemIDToIndex("100")].Description = "Generic Polar Window";
            I034DataItemsLastValid[ItemIDToIndex("100")].IsPresent = false;

            // 10 I034/110 Data Filter  
            I034DataItemsLastValid.Add(new I034DataItem());
            I034DataItemsLastValid[ItemIDToIndex("110")].ID = "110";
            I034DataItemsLastValid[ItemIDToIndex("110")].Description = "Data Filter";
            I034DataItemsLastValid[ItemIDToIndex("110")].IsPresent = false;

            // 11 I034/120 3D-Position of Data Source 
            I034DataItemsLastValid.Add(new I034DataItem());
            I034DataItemsLastValid[ItemIDToIndex("120")].ID = "120";
            I034DataItemsLastValid[ItemIDToIndex("120")].Description = "3D-Position of Data Source ";
            I034DataItemsLastValid[ItemIDToIndex("120")].IsPresent = false;

            // 12 I034/090 Collimation Error  
            I034DataItemsLastValid.Add(new I034DataItem());
            I034DataItemsLastValid[ItemIDToIndex("090")].ID = "090";
            I034DataItemsLastValid[ItemIDToIndex("090")].Description = "Collimation Error";
            I034DataItemsLastValid[ItemIDToIndex("090")].IsPresent = false;
        }

        public static string Extract_SIC_SAC(byte[] Data)
        {

            string Result;

            // Determine Length of FSPEC fields in bytes
            int FSPEC_Length = ASTERIX.DetermineLenghtOfFSPEC(Data);

            // Determine SIC/SAC Index
            int SIC_Index = 3 + FSPEC_Length;
            int SAC_Index = SIC_Index + 1;

            // Extract SIC/SAC Indexes.
            Result = Data[SIC_Index].ToString() + '/' + Data[SAC_Index].ToString();

            return Result;
        }

        public string Decode(byte[] Data, string Time)
        {
            // Define output data buffer
            string DataOut;

            // Determine Length of FSPEC fields in bytes
            int FSPEC_Length = ASTERIX.DetermineLenghtOfFSPEC(Data);

            // Extract SIC/SAC Indexes.
            DataOut = Extract_SIC_SAC(Data);

            // Creates and initializes a BitVector32 with all bit flags set to FALSE.
            BitVector32 FourFSPECOctets = ASTERIX.GetFourFSPECOctets(Data);

            // Loop for each FSPEC and determine what data item is present
            for (int FSPEC_Index = 1; FSPEC_Index <= FSPEC_Length; FSPEC_Index++)
            {
                switch (FSPEC_Index)
                {
                    case 1:

                        // 010 Data Source Identifier
                        if (FourFSPECOctets[Bit_Ops.Bit7] == true)
                        {
                            DataOut = DataOut + "  010:T";
                            I034DataItemsLastValid[ItemIDToIndex("010")].IsPresent = true;
                            I034DataItemsLastValid[ItemIDToIndex("010")].Time = Time;
                        }
                        else
                            DataOut = DataOut + "  010:F";

                        // 000 Message Type
                        if (FourFSPECOctets[Bit_Ops.Bit6] == true)
                        {
                            DataOut = DataOut + "  000:T";
                            I034DataItemsLastValid[ItemIDToIndex("000")].IsPresent = true;
                            I034DataItemsLastValid[ItemIDToIndex("000")].Time = Time;
                        }
                        else
                            DataOut = DataOut + "  000:F";

                        // 030 Time-of-Day
                        if (FourFSPECOctets[Bit_Ops.Bit5] == true)
                        {
                            DataOut = DataOut + "  030:T";
                            I034DataItemsLastValid[ItemIDToIndex("030")].IsPresent = true;
                            I034DataItemsLastValid[ItemIDToIndex("030")].Time = Time;
                        }
                        else
                            DataOut = DataOut + "  030:F";

                        // 020 Sector Number
                        if (FourFSPECOctets[Bit_Ops.Bit4] == true)
                        {
                            DataOut = DataOut + "  020:T";
                            I034DataItemsLastValid[ItemIDToIndex("020")].IsPresent = true;
                            I034DataItemsLastValid[ItemIDToIndex("020")].Time = Time;
                        }
                        else
                            DataOut = DataOut + "  020:F";

                        // 041 Antenna Rotation Period
                        if (FourFSPECOctets[Bit_Ops.Bit3] == true)
                        {
                            DataOut = DataOut + "  041:T";
                            I034DataItemsLastValid[ItemIDToIndex("041")].IsPresent = true;
                            I034DataItemsLastValid[ItemIDToIndex("041")].Time = Time;
                        }
                        else
                            DataOut = DataOut + "  041:F";

                        // 050 System Configuration and Status
                        if (FourFSPECOctets[Bit_Ops.Bit2] == true)
                        {
                            DataOut = DataOut + "  050:T";
                            I034DataItemsLastValid[ItemIDToIndex("050")].IsPresent = true;
                            I034DataItemsLastValid[ItemIDToIndex("050")].Time = Time;
                        }
                        else
                            DataOut = DataOut + "  050:F";

                        // 060 System Processing Mode
                        if (FourFSPECOctets[Bit_Ops.Bit1] == true)
                        {
                            DataOut = DataOut + "  060:T";
                            I034DataItemsLastValid[ItemIDToIndex("060")].IsPresent = true;
                            I034DataItemsLastValid[ItemIDToIndex("060")].Time = Time;
                        }
                        else
                            DataOut = DataOut + "  060:F";

                        break;

                    case 2:

                        // 070 Message Count Values
                        if (FourFSPECOctets[Bit_Ops.Bit15] == true)
                        {
                            DataOut = DataOut + "  070:T";
                            I034DataItemsLastValid[ItemIDToIndex("070")].IsPresent = true;
                            I034DataItemsLastValid[ItemIDToIndex("070")].Time = Time;
                        }
                        else
                            DataOut = DataOut + "  070:F";

                        // 100 Generic Polar Window
                        if (FourFSPECOctets[Bit_Ops.Bit14] == true)
                        {
                            DataOut = DataOut + "  100:T";
                            I034DataItemsLastValid[ItemIDToIndex("100")].IsPresent = true;
                            I034DataItemsLastValid[ItemIDToIndex("100")].Time = Time;
                        }
                        else
                            DataOut = DataOut + "  100:F";

                        // 110 Data Filter
                        if (FourFSPECOctets[Bit_Ops.Bit13] == true)
                        {
                            DataOut = DataOut + "  110:T";
                            I034DataItemsLastValid[ItemIDToIndex("110")].IsPresent = true;
                            I034DataItemsLastValid[ItemIDToIndex("110")].Time = Time;
                        }
                        else
                            DataOut = DataOut + "  110:F";

                        // 120 3D-Position of Data Source
                        if (FourFSPECOctets[Bit_Ops.Bit12] == true)
                        {
                            DataOut = DataOut + "  120:T";
                            I034DataItemsLastValid[ItemIDToIndex("120")].IsPresent = true;
                            I034DataItemsLastValid[ItemIDToIndex("120")].Time = Time;
                        }
                        else
                            DataOut = DataOut + "  120:F";

                        // 090 Collimation Error 
                        if (FourFSPECOctets[Bit_Ops.Bit11] == true)
                        {
                            DataOut = DataOut + "  090:T";
                            I034DataItemsLastValid[ItemIDToIndex("090")].IsPresent = true;
                            I034DataItemsLastValid[ItemIDToIndex("090")].Time = Time;
                        }
                        else
                            DataOut = DataOut + "  090:F";

                        break;

                    // Handle errors
                    default:
                        DataOut = DataOut + "  UKN:T";
                        break;
                }

                CAT34DecodeAndStore.Do(Data);
            }

            // Return decoded data
            return DataOut;
        }
    }
}
