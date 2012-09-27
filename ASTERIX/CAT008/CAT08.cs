using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT08
    {
        ///////////////////////////////////////////////////////////////////////////////////////
        // Monoradar Derived Weather Information
        ///////////////////////////////////////////////////////////////////////////////////////
        //
        // I008/010   Data Source Identifier                                    21
        // I008/000   Message Type                                              1
        // I008/020   Vector Qualifier                                          1+
        // I008/036   Sequence of Cartesian Vectors in SPF Notation             (1 + 3 x n)
        // I008/034   Sequence of Polar Vectors in SPF Notation                 (1 + 4 x n)
        // I008/040   Contour Identifier                                         2
        // I008/050   Sequence of Contour Points in SPF Notation                (1 + 2 x n)
        // FX         Field Extension Indicator
        //
        // I008/090   Time of Day                                               3
        // I008/100   Processing Status                                         3+
        // I008/110   Station Configuration Status                              1+
        // I008/120   Total Number of Items Constituting One Weather Picture    2
        // I008/038   Sequence of Weather Vectors in SPF Notation               (1 + 4 x n)
        //////////////////////////////////////////////////////////////////////////////////////

        // Define a class that holds I008 data
        public class I008DataItem
        {
            public string ID;
            public string Description;
            public bool IsPresent;
            public object value;

            // Constructor to set default
            // values.
            public I008DataItem()
            {
                ID = "N/A";
                Description = "N/A";
                IsPresent = false;
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
                case "036":
                    index = 3;
                    break;
                case "034":
                    index = 4;
                    break;
                case "040":
                    index = 5;
                    break;
                case "050":
                    index = 6;
                    break;
                case "090":
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
                case "038":
                    index = 11;
                    break;

                default:
                    break;
            }

            return index;
        }

        // Define collection of CAT008 data items. Used to store and retrieve basic data such as:
        // 
        public static System.Collections.Generic.List<I008DataItem> I008DataItems = new System.Collections.Generic.List<I008DataItem>();
        // 1. Item presence
        // 2. Item description
        //
        // Based on the data item identifer


        public static void Intitialize()
        {
            I008DataItems.Clear();

            // 1 /010 Data Source Identifier 
            I008DataItems.Add(new I008DataItem());
            I008DataItems[ItemIDToIndex("010")].ID = "010";
            I008DataItems[ItemIDToIndex("010")].Description = "Data Source Identifier";
            I008DataItems[ItemIDToIndex("010")].IsPresent = false;

            // 2 /000 Message Type
            I008DataItems.Add(new I008DataItem());
            I008DataItems[ItemIDToIndex("000")].ID = "000";
            I008DataItems[ItemIDToIndex("000")].Description = "Message Type";
            I008DataItems[ItemIDToIndex("000")].IsPresent = false;

            // 3 /020 Vector Qualifier
            I008DataItems.Add(new I008DataItem());
            I008DataItems[ItemIDToIndex("020")].ID = "020";
            I008DataItems[ItemIDToIndex("020")].Description = "Vector Qualifier";
            I008DataItems[ItemIDToIndex("020")].IsPresent = false;

            // 4 /036 Sequence of Cartesian Vectors in SPF Notation 
            I008DataItems.Add(new I008DataItem());
            I008DataItems[ItemIDToIndex("036")].ID = "036";
            I008DataItems[ItemIDToIndex("036")].Description = "Sequence of Cartesian Vectors in SPF Notation";
            I008DataItems[ItemIDToIndex("036")].IsPresent = false;

            // 5 /034 Sequence of Polar Vectors in SPF Notation 
            I008DataItems.Add(new I008DataItem());
            I008DataItems[ItemIDToIndex("034")].ID = "034";
            I008DataItems[ItemIDToIndex("034")].Description = "Sequence of Polar Vectors in SPF Notation";
            I008DataItems[ItemIDToIndex("034")].IsPresent = false;

            // 6 /040 Contour Identifier  
            I008DataItems.Add(new I008DataItem());
            I008DataItems[ItemIDToIndex("040")].ID = "040";
            I008DataItems[ItemIDToIndex("040")].Description = "Contour Identifier";
            I008DataItems[ItemIDToIndex("040")].IsPresent = false;

            // 7 /050 Sequence of Contour Points in SPF Notation
            I008DataItems.Add(new I008DataItem());
            I008DataItems[ItemIDToIndex("050")].ID = "050";
            I008DataItems[ItemIDToIndex("050")].Description = "Sequence of Contour Points in SPF Notation";
            I008DataItems[ItemIDToIndex("050")].IsPresent = false;

            // 8 /090 Time of Day
            I008DataItems.Add(new I008DataItem());
            I008DataItems[ItemIDToIndex("090")].ID = "090";
            I008DataItems[ItemIDToIndex("090")].Description = "Time of Day";
            I008DataItems[ItemIDToIndex("090")].IsPresent = false;

            // 9 /100 Processing Status 
            I008DataItems.Add(new I008DataItem());
            I008DataItems[ItemIDToIndex("100")].ID = "100";
            I008DataItems[ItemIDToIndex("100")].Description = "Processing Status";
            I008DataItems[ItemIDToIndex("100")].IsPresent = false;

            // 10 /110 Station Configuration Status 
            I008DataItems.Add(new I008DataItem());
            I008DataItems[ItemIDToIndex("110")].ID = "110";
            I008DataItems[ItemIDToIndex("110")].Description = "Station Configuration Status";
            I008DataItems[ItemIDToIndex("110")].IsPresent = false;

            // 11 /120 Total Number of Items Constituting One Weather Picture 
            I008DataItems.Add(new I008DataItem());
            I008DataItems[ItemIDToIndex("120")].ID = "120";
            I008DataItems[ItemIDToIndex("120")].Description = "Total Number of Items Constituting One Weather Picture";
            I008DataItems[ItemIDToIndex("120")].IsPresent = false;

            // 12 /038 Sequence of Weather Vectors in SPF Notation
            I008DataItems.Add(new I008DataItem());
            I008DataItems[ItemIDToIndex("038")].ID = "038";
            I008DataItems[ItemIDToIndex("038")].Description = "Sequence of Weather Vectors in SPF Notation";
            I008DataItems[ItemIDToIndex("038")].IsPresent = false;
        }


        public string Decode(byte[] Data, string Time)
        {
            // Define output data buffer
            string DataOut;

            // Determine Length of FSPEC fields in bytes
            int FSPEC_Length = ASTERIX.DetermineLenghtOfFSPEC(Data);

            // Determine SIC/SAC Index
            int SIC_Index = 3 + FSPEC_Length;
            int SAC_Index = SIC_Index + 1;

            // Extract SIC/SAC Indexes.
            DataOut = Data[SIC_Index].ToString() + '/' + Data[SAC_Index].ToString();

            // Creates and initializes a BitVector32 with all bit flags set to FALSE.
            BitVector32 FourFSPECOctets = ASTERIX.GetFourFSPECOctets(Data);

            // Loop for each FSPEC and determine what data item is present
            for (int FSPEC_Index = 1; FSPEC_Index <= FSPEC_Length; FSPEC_Index++)
            {

                switch (FSPEC_Index)
                {
                    case 1:

                        // 010 Data Source Identifier
                        if (FourFSPECOctets[Bit_Ops.Bit0] == true)
                        {
                            DataOut = DataOut + "  010:T";
                            I008DataItems[ItemIDToIndex("010")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  010:F";

                        // 000 Message Type
                        if (FourFSPECOctets[Bit_Ops.Bit1] == true)
                        {
                            DataOut = DataOut + "  000:T";
                            I008DataItems[ItemIDToIndex("000")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  000:F";

                        // 020 Vector Qualifier
                        if (FourFSPECOctets[Bit_Ops.Bit2] == true)
                        {
                            DataOut = DataOut + "  020:T";
                            I008DataItems[ItemIDToIndex("020")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  020:F";

                        // 036 Sequence of Cartesian Vectors in SPF Notation
                        if (FourFSPECOctets[Bit_Ops.Bit3] == true)
                        {
                            DataOut = DataOut + "  036:T";
                            I008DataItems[ItemIDToIndex("036")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  036:F";

                        // 034 Sequence of Polar Vectors in SPF Notation 
                        if (FourFSPECOctets[Bit_Ops.Bit4] == true)
                        {
                            DataOut = DataOut + "  034:T";
                            I008DataItems[ItemIDToIndex("034")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  034:F";

                        // 040 Contour Identifier
                        if (FourFSPECOctets[Bit_Ops.Bit5] == true)
                        {
                            DataOut = DataOut + "  040:T";
                            I008DataItems[ItemIDToIndex("040")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  040:F";

                        // 050 Sequence of Contour Points in SPF Notation
                        if (FourFSPECOctets[Bit_Ops.Bit6] == true)
                        {
                            DataOut = DataOut + "  050:T";
                            I008DataItems[ItemIDToIndex("050")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  050:F";

                        break;

                    case 2:

                        // 090 Time of Day
                        if (FourFSPECOctets[Bit_Ops.Bit8] == true)
                        {
                            DataOut = DataOut + "  090:T";
                            I008DataItems[ItemIDToIndex("090")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  090:F";

                        // 100 Processing Status
                        if (FourFSPECOctets[Bit_Ops.Bit9] == true)
                        {
                            DataOut = DataOut + "  100:T";
                            I008DataItems[ItemIDToIndex("100")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  100:F";

                        // 110 Station Configuration Status  
                        if (FourFSPECOctets[Bit_Ops.Bit10] == true)
                        {
                            DataOut = DataOut + "  110:T";
                            I008DataItems[ItemIDToIndex("110")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  110:F";

                        // 120 Total Number of Items Constituting One Weather Picture
                        if (FourFSPECOctets[Bit_Ops.Bit11] == true)
                        {
                            DataOut = DataOut + "  120:T";
                            I008DataItems[ItemIDToIndex("120")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  120:F";

                        // 038 Sequence of Weather Vectors in SPF Notation
                        if (FourFSPECOctets[Bit_Ops.Bit12] == true)
                        {
                            DataOut = DataOut + "  038:T";
                            I008DataItems[ItemIDToIndex("038")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  038:F";

                        break;

                    // Handle errors
                    default:
                        DataOut = DataOut + "  UKN:T";
                        break;
                }

                CAT08DecodeAndStore.Do(Data);
            }

            // Return decoded data
            return DataOut;
        }
    }
}
