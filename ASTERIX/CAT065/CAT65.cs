using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT65
    {   ////////////////////////////////////////////////////////////////////
        // SDPS Service Status Messages (SDPS)
        //
        // 1.   I065/010    Data Source Identifier              2
        // 2.   I065/000    Message Type                        1        
        // 3.   I065/015    Service Identification              1
        // 4.   I065/030    Time of Message                     3
        // 5.   I065/020    Batch Number                        1
        // 6.   I065/040    SDPS Configuration and Status       1
        // 7.   I065/050    Service Status Report               1
        // FXField extension indicator
        /// <summary>
        /// ////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>

        // Define a class that holds I065 data
        public class I065DataItem
        {
            public string ID;
            public string Description;
            public bool IsPresent;
            public object value;

            // Constructor to set default
            // values.
            public I065DataItem()
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
                case "015":
                    index = 2;
                    break;
                case "030":
                    index = 3;
                    break;
                case "020":
                    index = 4;
                    break;
                case "040":
                    index = 5;
                    break;
                case "050":
                    index = 6;
                    break;
                default:
                    break;
            }

            return index;
        }

        // Define collection of CAT065 data items. Used to store and retrieve basic data such as:
        // 
        public static System.Collections.Generic.List<I065DataItem> I065DataItems = new System.Collections.Generic.List<I065DataItem>();
        // 1. Item presence
        // 2. Item description
        //
        // Based on the data item identifer


        public static void Intitialize()
        {
            I065DataItems.Clear();

            // 1 I065/010 Data Source Identifier 
            I065DataItems.Add(new I065DataItem());
            I065DataItems[ItemIDToIndex("010")].ID = "010";
            I065DataItems[ItemIDToIndex("010")].Description = "Data Source Identifier";
            I065DataItems[ItemIDToIndex("010")].IsPresent = false;

            // 2 I065/000 Message Type
            I065DataItems.Add(new I065DataItem());
            I065DataItems[ItemIDToIndex("000")].ID = "000";
            I065DataItems[ItemIDToIndex("000")].Description = "Message Type";
            I065DataItems[ItemIDToIndex("000")].IsPresent = false;

            // 3 I065/015  Service Identification              
            I065DataItems.Add(new I065DataItem());
            I065DataItems[ItemIDToIndex("015")].ID = "015";
            I065DataItems[ItemIDToIndex("015")].Description = "Service Identification";
            I065DataItems[ItemIDToIndex("015")].IsPresent = false;

            // 4 I065/030  Time of Message                     
            I065DataItems.Add(new I065DataItem());
            I065DataItems[ItemIDToIndex("030")].ID = "030";
            I065DataItems[ItemIDToIndex("030")].Description = "Time of Message";
            I065DataItems[ItemIDToIndex("030")].IsPresent = false;

            // 5 I065/020  Batch Number                        
            I065DataItems.Add(new I065DataItem());
            I065DataItems[ItemIDToIndex("020")].ID = "020";
            I065DataItems[ItemIDToIndex("020")].Description = "Batch Number";
            I065DataItems[ItemIDToIndex("020")].IsPresent = false;

            // 6 I065/040  SDPS Configuration and Status       
            I065DataItems.Add(new I065DataItem());
            I065DataItems[ItemIDToIndex("040")].ID = "040";
            I065DataItems[ItemIDToIndex("040")].Description = "SDPS Configuration and Status";
            I065DataItems[ItemIDToIndex("040")].IsPresent = false;

            // 7 I065/050  Service Status Report               
            I065DataItems.Add(new I065DataItem());
            I065DataItems[ItemIDToIndex("050")].ID = "050";
            I065DataItems[ItemIDToIndex("050")].Description = "Service Status Report";
            I065DataItems[ItemIDToIndex("050")].IsPresent = false;

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
                        if (FourFSPECOctets[Bit_Ops.Bit7] == true)
                        {
                            DataOut = DataOut + "  010:T";
                            I065DataItems[ItemIDToIndex("010")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  010:F";

                        // 000 Message Type
                        if (FourFSPECOctets[Bit_Ops.Bit6] == true)
                        {
                            DataOut = DataOut + "  000:T";
                            I065DataItems[ItemIDToIndex("000")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  000:F";

                        // 015 Service Identification
                        if (FourFSPECOctets[Bit_Ops.Bit5] == true)
                        {
                            DataOut = DataOut + "  015:T";
                            I065DataItems[ItemIDToIndex("015")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  015:F";

                        // 030 Time of Message
                        if (FourFSPECOctets[Bit_Ops.Bit4] == true)
                        {
                            DataOut = DataOut + "  030:T";
                            I065DataItems[ItemIDToIndex("030")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  030:F";

                        // 020 Batch Number
                        if (FourFSPECOctets[Bit_Ops.Bit3] == true)
                        {
                            DataOut = DataOut + "  020:T";
                            I065DataItems[ItemIDToIndex("020")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  020:F";

                        // 040 SDPS Configuration and Status
                        if (FourFSPECOctets[Bit_Ops.Bit2] == true)
                        {
                            DataOut = DataOut + "  040:T";
                            I065DataItems[ItemIDToIndex("040")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  040:F";

                        // 050 Service Status Report
                        if (FourFSPECOctets[Bit_Ops.Bit1] == true)
                        {
                            DataOut = DataOut + "  050:T";
                            I065DataItems[ItemIDToIndex("050")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  050:F";

                        break;

                    // Handle errors
                    default:
                        DataOut = DataOut + "  UKN:T";
                        break;
                }

                CAT65DecodeAndStore.Do(Data);
            }

            // Return decoded data
            return DataOut;
        }
    }
}
