using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
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

    // Current data buffer Index
    class CAT34
    {
        public static int CurrentDataBufferOctalIndex = 0;

        public class CAT34DataItem
        {
            public string ID;
            public bool HasBeenPresent;
            public bool CurrentlyPresent;
            public string Description;
            public object value;

            // Constructor to set default
            // values.
            public CAT34DataItem()
            {
                ID = "N/A";
                Description = "N/A";
                HasBeenPresent = false;
                CurrentlyPresent = false;
                value = null;
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


        // Define collection of CAT034 data items. Used to store and retrieve basic data such as:
        // 
        public static System.Collections.Generic.List<CAT34DataItem> I034DataItems = new System.Collections.Generic.List<CAT34DataItem>();

        public static void Intitialize(bool Hard_Reset)
        {

            if (!Hard_Reset)
                foreach (CAT34.CAT34DataItem Item in CAT34.I034DataItems)
                    Item.value = null;
            else
            {
                I034DataItems.Clear();

                // 1 I034/010 Data Source Identifier 
                I034DataItems.Add(new CAT34DataItem());
                I034DataItems[ItemIDToIndex("010")].ID = "010";
                I034DataItems[ItemIDToIndex("010")].Description = "Data Source Identifier";

                I034DataItems[ItemIDToIndex("010")].HasBeenPresent = false;
                I034DataItems[ItemIDToIndex("010")].CurrentlyPresent = false;

                // 2 I034/000 Message Type
                I034DataItems.Add(new CAT34DataItem());
                I034DataItems[ItemIDToIndex("000")].ID = "000";
                I034DataItems[ItemIDToIndex("000")].Description = "Message Type";

                I034DataItems[ItemIDToIndex("000")].HasBeenPresent = false;
                I034DataItems[ItemIDToIndex("000")].CurrentlyPresent = false;

                // 3 I034/030 Time-of-Day 
                I034DataItems.Add(new CAT34DataItem());
                I034DataItems[ItemIDToIndex("030")].ID = "030";
                I034DataItems[ItemIDToIndex("030")].Description = "Time-of-Day";

                I034DataItems[ItemIDToIndex("030")].HasBeenPresent = false;
                I034DataItems[ItemIDToIndex("030")].CurrentlyPresent = false;

                // 4 I034/020 Sector Number
                I034DataItems.Add(new CAT34DataItem());
                I034DataItems[ItemIDToIndex("020")].ID = "020";
                I034DataItems[ItemIDToIndex("020")].Description = "Sector Number";

                I034DataItems[ItemIDToIndex("020")].HasBeenPresent = false;
                I034DataItems[ItemIDToIndex("020")].CurrentlyPresent = false;

                // 5 I034/041 Antenna Rotation Period 
                I034DataItems.Add(new CAT34DataItem());
                I034DataItems[ItemIDToIndex("041")].ID = "041";
                I034DataItems[ItemIDToIndex("041")].Description = "Antenna Rotation Period ";

                I034DataItems[ItemIDToIndex("041")].HasBeenPresent = false;
                I034DataItems[ItemIDToIndex("041")].CurrentlyPresent = false;

                // 6 I034/050 System Configuration and Status 
                I034DataItems.Add(new CAT34DataItem());
                I034DataItems[ItemIDToIndex("050")].ID = "050";
                I034DataItems[ItemIDToIndex("050")].Description = "System Configuration and Status";

                I034DataItems[ItemIDToIndex("050")].HasBeenPresent = false;
                I034DataItems[ItemIDToIndex("050")].CurrentlyPresent = false;

                // 7 I034/060 System Processing Mode                
                I034DataItems.Add(new CAT34DataItem());
                I034DataItems[ItemIDToIndex("060")].ID = "060";
                I034DataItems[ItemIDToIndex("060")].Description = "System Processing Mode";

                I034DataItems[ItemIDToIndex("060")].HasBeenPresent = false;
                I034DataItems[ItemIDToIndex("060")].CurrentlyPresent = false;

                // 8 I034/070 Message Count Values
                I034DataItems.Add(new CAT34DataItem());
                I034DataItems[ItemIDToIndex("070")].ID = "070";
                I034DataItems[ItemIDToIndex("070")].Description = "Message Count Values";

                I034DataItems[ItemIDToIndex("070")].HasBeenPresent = false;
                I034DataItems[ItemIDToIndex("070")].CurrentlyPresent = false;

                // 9 I034/100 Generic Polar Window
                I034DataItems.Add(new CAT34DataItem());
                I034DataItems[ItemIDToIndex("100")].ID = "100";
                I034DataItems[ItemIDToIndex("100")].Description = "Generic Polar Window";

                I034DataItems[ItemIDToIndex("100")].HasBeenPresent = false;
                I034DataItems[ItemIDToIndex("100")].CurrentlyPresent = false;

                // 10 I034/110 Data Filter  
                I034DataItems.Add(new CAT34DataItem());
                I034DataItems[ItemIDToIndex("110")].ID = "110";
                I034DataItems[ItemIDToIndex("110")].Description = "Data Filter";

                I034DataItems[ItemIDToIndex("110")].HasBeenPresent = false;
                I034DataItems[ItemIDToIndex("110")].CurrentlyPresent = false;

                // 11 I034/120 3D-Position of Data Source 
                I034DataItems.Add(new CAT34DataItem());
                I034DataItems[ItemIDToIndex("120")].ID = "120";
                I034DataItems[ItemIDToIndex("120")].Description = "3D-Position of Data Source ";

                I034DataItems[ItemIDToIndex("120")].HasBeenPresent = false;
                I034DataItems[ItemIDToIndex("120")].CurrentlyPresent = false;

                // 12 I034/090 Collimation Error  
                I034DataItems.Add(new CAT34DataItem());
                I034DataItems[ItemIDToIndex("090")].ID = "090";
                I034DataItems[ItemIDToIndex("090")].Description = "Collimation Error";

                I034DataItems[ItemIDToIndex("090")].HasBeenPresent = false;
                I034DataItems[ItemIDToIndex("090")].CurrentlyPresent = false;
            }

        }


        public string[] Decode(byte[] DataBlockBuffer, string Time, out int NumOfMessagesDecoded)
        {
            // Define output data buffer
            string[] DataOut = new string[3000];

            // Determine the size of the datablock
            int LengthOfDataBlockInBytes = DataBlockBuffer.Length;

            // Index into the array of record strings
            int DataOutIndex = 0;

            // Reset buffer indexes
            CurrentDataBufferOctalIndex = 0;
            int DataBufferIndexForThisExtraction = 0;

            // Determine SIC/SAC Index
            int SIC_Index = 0;
            int SAC_Index = 0;

            // Lenght of the current record's FSPECs
            int FSPEC_Length = 0;

            // Creates and initializes a BitVector32 with all bit flags set to FALSE.
            BitVector32 FourFSPECOctets = new BitVector32();

            while (DataBufferIndexForThisExtraction < LengthOfDataBlockInBytes)
            {
                // Assume that there will be no more than 1000 bytes in one record
                byte[] LocalSingleRecordBuffer = new byte[3000];

                Array.Copy(DataBlockBuffer, DataBufferIndexForThisExtraction, LocalSingleRecordBuffer, 0, (LengthOfDataBlockInBytes - DataBufferIndexForThisExtraction));

                // Get all four data words, but use only the number specifed 
                // by the length of FSPEC words
                FourFSPECOctets = ASTERIX.GetFourFSPECOctets(LocalSingleRecordBuffer);

                // Determine Length of FSPEC fields in bytes
                FSPEC_Length = ASTERIX.DetermineLenghtOfFSPEC(LocalSingleRecordBuffer);

                // Check wether 010 is present
                if (FourFSPECOctets[Bit_Ops.Bit7] == true)
                {
                    // Determine SIC/SAC Index
                    SIC_Index = FSPEC_Length;
                    SAC_Index = SIC_Index + 1;

                    // Extract SIC/SAC Indexes.
                    DataOut[DataOutIndex] = LocalSingleRecordBuffer[SIC_Index].ToString() + '/' + LocalSingleRecordBuffer[SAC_Index].ToString();

                    // Save of the current data buffer index so it can be used by
                    // Decoder
                    CurrentDataBufferOctalIndex = SAC_Index + 1;

                }
                else
                {
                    // Extract SIC/SAC Indexes.
                    DataOut[DataOutIndex] = "---" + '/' + "---";
                }

                ///////////////////////////////////////////////////////////////////////////
                // Populate the current SIC/SAC and Time stamp for this meesage
                //
                I034DataItems[ItemIDToIndex("010")].value =
                    new ASTERIX.SIC_SAC_Time(LocalSingleRecordBuffer[SIC_Index], LocalSingleRecordBuffer[SAC_Index], ASTERIX.TimeOfReception);

                // Loop for each FSPEC and determine what data item is present
                for (int FSPEC_Index = 1; FSPEC_Index <= FSPEC_Length; FSPEC_Index++)
                {
                    switch (FSPEC_Index)
                    {
                        case 1:

                            // 010 Data Source Identifier
                            if (FourFSPECOctets[Bit_Ops.Bit7] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  010:T";
                                I034DataItems[ItemIDToIndex("010")].HasBeenPresent = true;
                                I034DataItems[ItemIDToIndex("010")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  010:F";
                                I034DataItems[ItemIDToIndex("010")].CurrentlyPresent = false;
                            }

                            // 000 Message Type
                            if (FourFSPECOctets[Bit_Ops.Bit6] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  000:T";
                                I034DataItems[ItemIDToIndex("000")].HasBeenPresent = true;
                                I034DataItems[ItemIDToIndex("000")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  000:F";
                                I034DataItems[ItemIDToIndex("000")].CurrentlyPresent = false;
                            }

                            // 030 Time-of-Day
                            if (FourFSPECOctets[Bit_Ops.Bit5] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  030:T";
                                I034DataItems[ItemIDToIndex("030")].HasBeenPresent = true;
                                I034DataItems[ItemIDToIndex("030")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  030:F";
                                I034DataItems[ItemIDToIndex("030")].CurrentlyPresent = false;
                            }

                            // 020 Sector Number
                            if (FourFSPECOctets[Bit_Ops.Bit4] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  020:T";
                                I034DataItems[ItemIDToIndex("020")].HasBeenPresent = true;
                                I034DataItems[ItemIDToIndex("020")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  020:F";
                                I034DataItems[ItemIDToIndex("020")].CurrentlyPresent = false;
                            }

                            // 041 Antenna Rotation Period
                            if (FourFSPECOctets[Bit_Ops.Bit3] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  041:T";
                                I034DataItems[ItemIDToIndex("041")].HasBeenPresent = true;
                                I034DataItems[ItemIDToIndex("041")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  041:F";
                                I034DataItems[ItemIDToIndex("041")].CurrentlyPresent = false;
                            }

                            // 050 System Configuration and Status
                            if (FourFSPECOctets[Bit_Ops.Bit2] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  050:T";
                                I034DataItems[ItemIDToIndex("050")].HasBeenPresent = true;
                                I034DataItems[ItemIDToIndex("050")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  050:F";
                                I034DataItems[ItemIDToIndex("050")].CurrentlyPresent = false;
                            }

                            // 060 System Processing Mode
                            if (FourFSPECOctets[Bit_Ops.Bit1] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  060:T";
                                I034DataItems[ItemIDToIndex("060")].HasBeenPresent = true;
                                I034DataItems[ItemIDToIndex("060")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  060:F";
                                I034DataItems[ItemIDToIndex("060")].CurrentlyPresent = false;
                            }

                            break;

                        case 2:

                            // 070 Message Count Values
                            if (FourFSPECOctets[Bit_Ops.Bit15] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  070:T";
                                I034DataItems[ItemIDToIndex("070")].HasBeenPresent = true;
                                I034DataItems[ItemIDToIndex("070")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  070:F";
                                I034DataItems[ItemIDToIndex("070")].CurrentlyPresent = false;
                            }

                            // 100 Generic Polar Window
                            if (FourFSPECOctets[Bit_Ops.Bit14] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  100:T";
                                I034DataItems[ItemIDToIndex("100")].HasBeenPresent = true;
                                I034DataItems[ItemIDToIndex("100")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  100:F";
                                I034DataItems[ItemIDToIndex("100")].CurrentlyPresent = false;
                            }

                            // 110 Data Filter
                            if (FourFSPECOctets[Bit_Ops.Bit13] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  110:T";
                                I034DataItems[ItemIDToIndex("110")].HasBeenPresent = true;
                                I034DataItems[ItemIDToIndex("110")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  110:F";
                                I034DataItems[ItemIDToIndex("110")].CurrentlyPresent = false;
                            }

                            // 120 3D-Position of Data Source
                            if (FourFSPECOctets[Bit_Ops.Bit12] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  120:T";
                                I034DataItems[ItemIDToIndex("120")].HasBeenPresent = true;
                                I034DataItems[ItemIDToIndex("120")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  120:F";
                                I034DataItems[ItemIDToIndex("120")].CurrentlyPresent = false;
                            }

                            // 090 Collimation Error 
                            if (FourFSPECOctets[Bit_Ops.Bit11] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  090:T";
                                I034DataItems[ItemIDToIndex("090")].HasBeenPresent = true;
                                I034DataItems[ItemIDToIndex("090")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  090:F";
                                I034DataItems[ItemIDToIndex("090")].CurrentlyPresent = false;
                            }
                            break;

                        // Handle errors
                        default:
                            DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  UKN:T";
                            break;
                    }

                }

                DataOutIndex++;
                CAT34DecodeAndStore.Do(LocalSingleRecordBuffer);
                DataBufferIndexForThisExtraction = DataBufferIndexForThisExtraction + CurrentDataBufferOctalIndex + 1;
            }

            // Return decoded data
            NumOfMessagesDecoded = DataOutIndex;
            // Return decoded data
            return DataOut;
        }
    }
}
