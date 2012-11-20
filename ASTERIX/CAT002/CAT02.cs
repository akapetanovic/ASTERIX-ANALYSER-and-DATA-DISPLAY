using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{

    ///////////////////////////////////////////////////////////////////////////////
    // Monoradar Service Messages (Status, North marker, Sector crossing messages)
    //
    // 1.  I002/010 Data Source Identifier              2
    // 2.  I002/000 Message Type                        1
    // 3.  I002/020 Sector Number                       1
    // 4.  I002/030 Time of Day                         3
    // 5.  I002/041 Antenna Rotation Period             2
    // 6.  I002/050 Station Configuration Status        1+
    // 7.  I002/060 Station Processing Mode             1+
    //  FX Field Extension Indicator

    // 8.  I002/070 Plot Count Values                   (1 + 2 X N)
    // 9.  I002/100 Dynamic Window - Type 1             8
    // 10. I002/090 Collimation Error                   2
    // 11. I002/080 Warning/Error Conditions            1+
    // - Spare
    // - Reserved for SP indicator
    // - Reserved for RFS indicator
    // FX Field Extension Indicator

    class CAT02
    {
        // Current data buffer Index
        public static int CurrentDataBufferOctalIndex = 0;

        // Define a class that holds I002 data
        public class CAT02DataItem
        {
            public string ID;
            public bool HasBeenPresent;
            public bool CurrentlyPresent;
            public string Description;
            public object value;

            // Constructor to set default
            // values.
            public CAT02DataItem()
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
                case "020":
                    index = 2;
                    break;
                case "030":
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
                case "090":
                    index = 9;
                    break;
                case "080":
                    index = 10;
                    break;
                default:
                    break;
            }

            return index;
        }

        // Define collection of CAT002 data items. Used to store and retrieve basic data such as:
        // 
        public static System.Collections.Generic.List<CAT02DataItem> I002DataItems = new System.Collections.Generic.List<CAT02DataItem>();

        public static void Intitialize(bool Hard_Reset)
        {
            if (!Hard_Reset)
                foreach (CAT02.CAT02DataItem Item in CAT02.I002DataItems)
                    Item.value = null;
            else
            {

                I002DataItems.Clear();

                // 1 I002/010 Data Source Identifier 
                I002DataItems.Add(new CAT02DataItem());
                I002DataItems[ItemIDToIndex("010")].ID = "010";
                I002DataItems[ItemIDToIndex("010")].Description = "Data Source Identifier";

                I002DataItems[ItemIDToIndex("010")].HasBeenPresent = false;
                I002DataItems[ItemIDToIndex("010")].CurrentlyPresent = false;

                // 2 I002/000 Message Type
                I002DataItems.Add(new CAT02DataItem());
                I002DataItems[ItemIDToIndex("000")].ID = "000";
                I002DataItems[ItemIDToIndex("000")].Description = "Message Type";

                I002DataItems[ItemIDToIndex("000")].HasBeenPresent = false;
                I002DataItems[ItemIDToIndex("000")].CurrentlyPresent = false;

                // 3.  I002/020 Sector Number
                I002DataItems.Add(new CAT02DataItem());
                I002DataItems[ItemIDToIndex("020")].ID = "020";
                I002DataItems[ItemIDToIndex("020")].Description = "Sector Number";
                I002DataItems[ItemIDToIndex("020")].HasBeenPresent = false;
                I002DataItems[ItemIDToIndex("020")].CurrentlyPresent = false;

                // 4.  I002/030 Time of Day
                I002DataItems.Add(new CAT02DataItem());
                I002DataItems[ItemIDToIndex("030")].ID = "030";
                I002DataItems[ItemIDToIndex("030")].Description = "Time of Day";

                I002DataItems[ItemIDToIndex("030")].HasBeenPresent = false;
                I002DataItems[ItemIDToIndex("030")].CurrentlyPresent = false;

                // 5.  I002/041 Antenna Rotation Period
                I002DataItems.Add(new CAT02DataItem());
                I002DataItems[ItemIDToIndex("041")].ID = "041";
                I002DataItems[ItemIDToIndex("041")].Description = "Antenna Rotation Period";

                I002DataItems[ItemIDToIndex("041")].HasBeenPresent = false;
                I002DataItems[ItemIDToIndex("041")].CurrentlyPresent = false;

                // 6.  I002/050 Station Configuration Status
                I002DataItems.Add(new CAT02DataItem());
                I002DataItems[ItemIDToIndex("050")].ID = "050";
                I002DataItems[ItemIDToIndex("050")].Description = "Station Configuration Status";

                I002DataItems[ItemIDToIndex("050")].HasBeenPresent = false;
                I002DataItems[ItemIDToIndex("050")].CurrentlyPresent = false;

                // 7.  I002/060 Station Processing Mode
                I002DataItems.Add(new CAT02DataItem());
                I002DataItems[ItemIDToIndex("060")].ID = "060";
                I002DataItems[ItemIDToIndex("060")].Description = "Station Processing Mode";

                I002DataItems[ItemIDToIndex("060")].HasBeenPresent = false;
                I002DataItems[ItemIDToIndex("060")].CurrentlyPresent = false;

                // 8.  I002/070 Plot Count Values
                I002DataItems.Add(new CAT02DataItem());
                I002DataItems[ItemIDToIndex("070")].ID = "070";
                I002DataItems[ItemIDToIndex("070")].Description = "Plot Count Values";

                I002DataItems[ItemIDToIndex("070")].HasBeenPresent = false;
                I002DataItems[ItemIDToIndex("070")].CurrentlyPresent = false;

                // 9.  I002/100 Dynamic Window - Type 1
                I002DataItems.Add(new CAT02DataItem());
                I002DataItems[ItemIDToIndex("100")].ID = "100";
                I002DataItems[ItemIDToIndex("100")].Description = "Dynamic Window - Type 1";

                I002DataItems[ItemIDToIndex("100")].HasBeenPresent = false;
                I002DataItems[ItemIDToIndex("100")].CurrentlyPresent = false;

                // 10. I002/090 Collimation Error
                I002DataItems.Add(new CAT02DataItem());
                I002DataItems[ItemIDToIndex("090")].ID = "090";
                I002DataItems[ItemIDToIndex("090")].Description = "Collimation Error";

                I002DataItems[ItemIDToIndex("090")].HasBeenPresent = false;
                I002DataItems[ItemIDToIndex("090")].CurrentlyPresent = false;

                // 11. I002/080 Warning/Error Conditions
                I002DataItems.Add(new CAT02DataItem());
                I002DataItems[ItemIDToIndex("080")].ID = "080";
                I002DataItems[ItemIDToIndex("080")].Description = "Warning/Error Conditions";

                I002DataItems[ItemIDToIndex("080")].HasBeenPresent = false;
                I002DataItems[ItemIDToIndex("080")].CurrentlyPresent = false;
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
                I002DataItems[ItemIDToIndex("010")].value =
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
                                I002DataItems[ItemIDToIndex("010")].HasBeenPresent = true;
                                I002DataItems[ItemIDToIndex("010")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  010:F";
                                I002DataItems[ItemIDToIndex("010")].CurrentlyPresent = false;
                            }

                            // 000 Message Type
                            if (FourFSPECOctets[Bit_Ops.Bit6] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  000:T";
                                I002DataItems[ItemIDToIndex("000")].HasBeenPresent = true;
                                I002DataItems[ItemIDToIndex("000")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  000:F";
                                I002DataItems[ItemIDToIndex("000")].CurrentlyPresent = false;
                            }

                            // 020 Sector Number
                            if (FourFSPECOctets[Bit_Ops.Bit5] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  020:T";
                                I002DataItems[ItemIDToIndex("020")].HasBeenPresent = true;
                                I002DataItems[ItemIDToIndex("020")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  020:F";
                                I002DataItems[ItemIDToIndex("020")].CurrentlyPresent = false;
                            }

                            // 030 Time of Day
                            if (FourFSPECOctets[Bit_Ops.Bit4] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  030:T";
                                I002DataItems[ItemIDToIndex("030")].HasBeenPresent = true;
                                I002DataItems[ItemIDToIndex("030")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  030:F";
                                I002DataItems[ItemIDToIndex("030")].CurrentlyPresent = false;
                            }

                            // 041 Antenna Rotation Period
                            if (FourFSPECOctets[Bit_Ops.Bit3] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  041:T";
                                I002DataItems[ItemIDToIndex("041")].HasBeenPresent = true;
                                I002DataItems[ItemIDToIndex("041")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  041:F";
                                I002DataItems[ItemIDToIndex("041")].CurrentlyPresent = false;
                            }

                            // 050 Station Configuration Status
                            if (FourFSPECOctets[Bit_Ops.Bit2] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  050:T";
                                I002DataItems[ItemIDToIndex("050")].HasBeenPresent = true;
                                I002DataItems[ItemIDToIndex("050")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  050:F";
                                I002DataItems[ItemIDToIndex("050")].CurrentlyPresent = false;
                            }

                            // 060 Station Processing Mode
                            if (FourFSPECOctets[Bit_Ops.Bit1] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  060:T";
                                I002DataItems[ItemIDToIndex("060")].HasBeenPresent = true;
                                I002DataItems[ItemIDToIndex("060")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  060:F";
                                I002DataItems[ItemIDToIndex("060")].CurrentlyPresent = false;
                            }
                            break;

                        case 2:

                            // 070 Plot Count Values
                            if (FourFSPECOctets[Bit_Ops.Bit15] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  070:T";
                                I002DataItems[ItemIDToIndex("070")].HasBeenPresent = true;
                                I002DataItems[ItemIDToIndex("070")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  070:F";
                                I002DataItems[ItemIDToIndex("070")].CurrentlyPresent = false;
                            }

                            // 100 Dynamic Window - Type 1
                            if (FourFSPECOctets[Bit_Ops.Bit14] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  100:T";
                                I002DataItems[ItemIDToIndex("100")].HasBeenPresent = true;
                                I002DataItems[ItemIDToIndex("100")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  100:F";
                                I002DataItems[ItemIDToIndex("100")].CurrentlyPresent = false;
                            }

                            // 090 Collimation Error
                            if (FourFSPECOctets[Bit_Ops.Bit13] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  090:T";
                                I002DataItems[ItemIDToIndex("090")].HasBeenPresent = true;
                                I002DataItems[ItemIDToIndex("090")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  090:F";
                                I002DataItems[ItemIDToIndex("090")].CurrentlyPresent = false;
                            }

                            // 080 Warning/Error Conditions
                            if (FourFSPECOctets[Bit_Ops.Bit12] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  080:T";
                                I002DataItems[ItemIDToIndex("080")].HasBeenPresent = true;
                                I002DataItems[ItemIDToIndex("080")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  080:F";
                                I002DataItems[ItemIDToIndex("080")].CurrentlyPresent = false;
                            }

                            break;

                        // Handle errors
                        default:
                            DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  UKN:T";
                            break;
                    }

                }

                DataOutIndex++;
                CAT02DecodeAndStore.Do(LocalSingleRecordBuffer);
                DataBufferIndexForThisExtraction = DataBufferIndexForThisExtraction + CurrentDataBufferOctalIndex + 1;
            }

            // Return decoded data
            NumOfMessagesDecoded = DataOutIndex;
            return DataOut;
        }
    }
}
