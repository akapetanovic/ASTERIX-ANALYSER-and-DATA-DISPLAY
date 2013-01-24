using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62
    {
        // SDPS Track Messages
        //
        // 1.     I062/010 Data Source Identifier                           2
        // 2.     - Spare -
        // 3.     I062/015 Service Identification                           1
        // 4.     I062/070 Time Of Track Information                        3
        // 5.     I062/105 Calculated Track Position (WGS-84)               8
        // 6.     I062/100 Calculated Track Position (Cartesian)            6
        // 7.     I062/185 Calculated Track Velocity (Cartesian)            4
        // FX.     - Field extension indicator -
        //
        // 8.     I062/210 Calculated Acceleration (Cartesian)              2
        // 9.     I062/060 Track Mode 3/A Code                              2
        // 10.     I062/245 Target Identification                           7
        // 11.     I062/380 Aircraft Derived Data                           1+
        // 12.     I062/040 Track Number                                    2
        // 13.     I062/080 Track Status                                    1+
        // 14.     I062/290 System Track Update Ages                        1+
        // FX.     - Field extension indicator -
        //
        // 15.     I062/200 Mode of Movement                                1
        // 16.     I062/295 Track Data Ages                                 1+
        // 17.     I062/136 Measured Flight Level                           2
        // 18.     I062/130 Calculated Track Geometric Altitude             2
        // 19.     I062/135 Calculated Track Barometric Altitude            2
        // 20.     I062/220 Calculated Rate Of Climb/Descent                2
        // 21.     I062/390 Flight Plan Related Data                        1+
        // FX.     - Field extension indicator -
        //
        // 22.     I062/270 Target Size & Orientation                       1+
        // 23.     I062/300 Vehicle Fleet Identification                    1
        // 24.     I062/110 Mode 5 Data reports & Extended Mode 1 Code      1+
        // 25.     I062/120 Track Mode 2 Code                               2
        // 26.     I062/510 Composed Track Number                           3+
        // 27.     I062/500 Estimated Accuracies                            1+
        // 28.     I062/340 Measured Information                            1+
        // FX.     - Field extension indicator -
        //
        // 29.     - Spare
        // 30.     - Spare -
        // 31.     - Spare -
        // 32.     - Spare -
        // 33.     - Spare -
        // 34.     RE Reserved Expansion Field                              1+
        // 35.     SP Reserved For Special Purpose Indicator                1+
        // FX.     - Field extension indicator -


        // Current data buffer Index
        public static int CurrentDataBufferOctalIndex = 0;

        // Define a class that holds I062 data
        public class CAT062DataItem
        {
            public string ID;
            public bool HasBeenPresent;
            public bool CurrentlyPresent;
            public string Description;
            public object value;


            // Constructor to set default
            // values.
            public CAT062DataItem()
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
                case "015":
                    index = 1;
                    break;
                case "070":
                    index = 2;
                    break;
                case "105":
                    index = 3;
                    break;
                case "100":
                    index = 4;
                    break;
                case "185":
                    index = 5;
                    break;
                case "210":
                    index = 6;
                    break;
                case "060":
                    index = 7;
                    break;
                case "245":
                    index = 8;
                    break;
                case "380":
                    index = 9;
                    break;
                case "040":
                    index = 10;
                    break;
                case "080":
                    index = 11;
                    break;
                case "290":
                    index = 12;
                    break;
                case "200":
                    index = 13;
                    break;
                case "295":
                    index = 14;
                    break;
                case "136":
                    index = 15;
                    break;
                case "130":
                    index = 16;
                    break;
                case "135":
                    index = 17;
                    break;
                case "220":
                    index = 18;
                    break;
                case "390":
                    index = 19;
                    break;
                case "270":
                    index = 20;
                    break;
                case "300":
                    index = 21;
                    break;
                case "110":
                    index = 22;
                    break;
                case "120":
                    index = 23;
                    break;
                case "510":
                    index = 24;
                    break;
                case "500":
                    index = 25;
                    break;
                case "340":
                    index = 26;
                    break;
                case "REF":
                    index = 27;
                    break;
                case "SPI":
                    index = 28;
                    break;
                default:
                    break;
            }

            return index;
        }

        // Define collection of CAT062 data items. Used to store and retrieve basic data such as:
        // 
        public static System.Collections.Generic.List<CAT062DataItem> I062DataItems = new System.Collections.Generic.List<CAT062DataItem>();
        // 1. Item presence
        // 2. Item description
        //
        // Based on the data item identifer


        public static void Intitialize(bool Hard_Reset)
        {
            if (!Hard_Reset)
            {
                foreach (CAT62.CAT062DataItem Item in CAT62.I062DataItems)
                    Item.value = null;
            }
            else
            {

                I062DataItems.Clear();

                // I062/010 Data Source Identifier 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("010")].ID = "010";
                I062DataItems[ItemIDToIndex("010")].Description = "Data Source Identifier";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("010")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("010")].CurrentlyPresent = false;
                // I062/015 Service Identification 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("015")].ID = "015";
                I062DataItems[ItemIDToIndex("015")].Description = "Service Identification";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("015")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("015")].CurrentlyPresent = false;
                // I062/070 Time Of Track Information
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("070")].ID = "070";
                I062DataItems[ItemIDToIndex("070")].Description = "Time Of Track Information";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("070")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("070")].CurrentlyPresent = false;
                // I062/105 Calculated Track Position (WGS-84)
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("105")].ID = "105";
                I062DataItems[ItemIDToIndex("105")].Description = "Calculated Track Position (WGS-84)";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("105")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("105")].CurrentlyPresent = false;
                // I062/100 Calculated Track Position (Cartesian)
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("100")].ID = "100";
                I062DataItems[ItemIDToIndex("100")].Description = "Data Source Identifier";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("100")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("100")].CurrentlyPresent = false;
                // I062/185 Calculated Track Velocity (Cartesian) 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("185")].ID = "185";
                I062DataItems[ItemIDToIndex("185")].Description = "Calculated Track Velocity (Cartesian)";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("185")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("185")].CurrentlyPresent = false;
                // I062/210 Calculated Acceleration (Cartesian)
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("210")].ID = "210";
                I062DataItems[ItemIDToIndex("210")].Description = "Calculated Acceleration (Cartesian)";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("210")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("210")].CurrentlyPresent = false;
                // I062/060 Track Mode 3/A Code
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("060")].ID = "060";
                I062DataItems[ItemIDToIndex("060")].Description = " Track Mode 3/A Code";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("060")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("060")].CurrentlyPresent = false;
                // I062/245 Target Identification 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("245")].ID = "245";
                I062DataItems[ItemIDToIndex("245")].Description = "Target Identification";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("245")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("245")].CurrentlyPresent = false;
                // I062/380 Aircraft Derived Data
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("380")].ID = "380";
                I062DataItems[ItemIDToIndex("380")].Description = "Aircraft Derived Data";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("380")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("380")].CurrentlyPresent = false;
                // I062/040 Track Number
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("040")].ID = "040";
                I062DataItems[ItemIDToIndex("040")].Description = "Track Number";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("040")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("040")].CurrentlyPresent = false;
                // I062/080 Track Status
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("080")].ID = "080";
                I062DataItems[ItemIDToIndex("080")].Description = "Track Status";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("080")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("080")].CurrentlyPresent = false;
                // I062/290 System Track Update Ages 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("290")].ID = "290";
                I062DataItems[ItemIDToIndex("290")].Description = "System Track Update Ages";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("290")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("290")].CurrentlyPresent = false;
                // I062/200 Mode of Movement 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("200")].ID = "200";
                I062DataItems[ItemIDToIndex("200")].Description = "Mode of Movement";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("200")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("200")].CurrentlyPresent = false;
                // I062/295 Track Data Ages 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("295")].ID = "295";
                I062DataItems[ItemIDToIndex("295")].Description = "Track Data Ages";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("295")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("295")].CurrentlyPresent = false;
                // I062/136 Measured Flight Level  
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("136")].ID = "136";
                I062DataItems[ItemIDToIndex("136")].Description = "Measured Flight Level";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("136")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("136")].CurrentlyPresent = false;
                // I062/130 Calculated Track Geometric Altitude 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("130")].ID = "130";
                I062DataItems[ItemIDToIndex("130")].Description = "Calculated Track Geometric Altitude";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("130")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("130")].CurrentlyPresent = false;
                // I062/135 Calculated Track Barometric Altitude 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("135")].ID = "135";
                I062DataItems[ItemIDToIndex("135")].Description = "Calculated Track Barometric Altitude ";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("135")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("135")].CurrentlyPresent = false;
                // I062/220 Calculated Rate Of Climb/Descent
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("220")].ID = "220";
                I062DataItems[ItemIDToIndex("220")].Description = "Calculated Rate Of Climb/Descent";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("220")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("220")].CurrentlyPresent = false;
                // I062/390 Flight Plan Related Data 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("390")].ID = "390";
                I062DataItems[ItemIDToIndex("390")].Description = "Flight Plan Related Data";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("390")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("390")].CurrentlyPresent = false;
                // I062/270 Target Size & Orientation 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("270")].ID = "270";
                I062DataItems[ItemIDToIndex("270")].Description = "Target Size & Orientation";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("270")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("270")].CurrentlyPresent = false;
                // I062/300 Vehicle Fleet Identification 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("300")].ID = "300";
                I062DataItems[ItemIDToIndex("300")].Description = "Vehicle Fleet Identification";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("300")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("300")].CurrentlyPresent = false;
                // I062/110 Mode 5 Data reports & Extended Mode 1 Code  
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("110")].ID = "110";
                I062DataItems[ItemIDToIndex("110")].Description = "Mode 5 Data reports & Extended Mode 1 Code";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("110")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("110")].CurrentlyPresent = false;
                // I062/120 Track Mode 2 Code 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("120")].ID = "120";
                I062DataItems[ItemIDToIndex("120")].Description = "Track Mode 2 Code";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("120")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("120")].CurrentlyPresent = false;
                // I062/510 Composed Track Number 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("510")].ID = "510";
                I062DataItems[ItemIDToIndex("510")].Description = "Composed Track Number";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("510")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("510")].CurrentlyPresent = false;
                // I062/500 Estimated Accuracies 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("500")].ID = "500";
                I062DataItems[ItemIDToIndex("500")].Description = "Estimated Accuracies";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("500")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("500")].CurrentlyPresent = false;
                // I062/340 Measured Information 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("340")].ID = "340";
                I062DataItems[ItemIDToIndex("340")].Description = "Measured Information";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("340")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("340")].CurrentlyPresent = false;
                // I062/REF 
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("REF")].ID = "REF";
                I062DataItems[ItemIDToIndex("REF")].Description = "Reserved Expansion Field";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("REF")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("REF")].CurrentlyPresent = false;
                // I062/SPI
                I062DataItems.Add(new CAT062DataItem());
                I062DataItems[ItemIDToIndex("SPI")].ID = "SPI";
                I062DataItems[ItemIDToIndex("SPI")].Description = "Special Purpose Indicator";
                if (Hard_Reset)
                    I062DataItems[ItemIDToIndex("SPI")].HasBeenPresent = false;
                I062DataItems[ItemIDToIndex("SPI")].CurrentlyPresent = false;
            }
        }

        public string[] Decode(byte[] DataBlockBuffer, string Time, out int NumOfMessagesDecoded)
        {
            // Define output data buffer
            string[] DataOut = new string[2000];

            // Determine the size of the datablock
            int LengthOfDataBlockInBytes = DataBlockBuffer.Length;

            // Index into the array of record strings
            int DataOutIndex = 0;

            // Reset buffer indexes
            CurrentDataBufferOctalIndex = 0;
            int DataBufferIndexForThisExtraction = 0;

            // SIC/SAC Indexes
            int SIC_Index = 0;
            int SAC_Index = 0;

            // Lenght of the current record's FSPECs
            int FSPEC_Length = 0;

            // The first four possible FSPEC octets
            BitVector32 FourFSPECOctets = new BitVector32();

            // The fifth FSPEC octet in the case RE or SP field is present
            BitVector32 TheFifthFSPECOctet = new BitVector32();

            while (DataBufferIndexForThisExtraction < LengthOfDataBlockInBytes)
            {

                // Assume that there will be no more than 200 bytes in one record
                byte[] LocalSingleRecordBuffer = new byte[3000];

                Array.Copy(DataBlockBuffer, DataBufferIndexForThisExtraction, LocalSingleRecordBuffer, 0, (LengthOfDataBlockInBytes - DataBufferIndexForThisExtraction));

                // Get all four data words, but use only the number specifed 
                // by the length of FSPEC words
                FourFSPECOctets = ASTERIX.GetFourFSPECOctets(LocalSingleRecordBuffer);
                TheFifthFSPECOctet = ASTERIX.GetFifthFSPECOctet(LocalSingleRecordBuffer);

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
                I062DataItems[ItemIDToIndex("010")].value =
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
                                I062DataItems[ItemIDToIndex("010")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("010")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  010:F";
                                I062DataItems[ItemIDToIndex("010")].CurrentlyPresent = false;
                            }

                            // Spare bit
                            if (FourFSPECOctets[Bit_Ops.Bit6] == true)
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  SPR:T";
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  SPR:F";

                            // 015 Service Identification
                            if (FourFSPECOctets[Bit_Ops.Bit5] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  015:T";
                                I062DataItems[ItemIDToIndex("015")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("015")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  015:F";
                                I062DataItems[ItemIDToIndex("015")].CurrentlyPresent = false;
                            }

                            // 070 Time Of Track Information
                            if (FourFSPECOctets[Bit_Ops.Bit4] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  070:T";
                                I062DataItems[ItemIDToIndex("070")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("070")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  070:F";
                                I062DataItems[ItemIDToIndex("070")].CurrentlyPresent = false;
                            }

                            // 105 Calculated Track Position (WGS-84)   
                            if (FourFSPECOctets[Bit_Ops.Bit3] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  105:T";
                                I062DataItems[ItemIDToIndex("105")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("105")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  105:F";
                                I062DataItems[ItemIDToIndex("105")].CurrentlyPresent = false;
                            }

                            // 100 Calculated Track Position (Cartesian)
                            if (FourFSPECOctets[Bit_Ops.Bit2] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  100:T";
                                I062DataItems[ItemIDToIndex("100")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("100")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  100:F";
                                I062DataItems[ItemIDToIndex("100")].CurrentlyPresent = false;
                            }

                            // 185 Calculated Track Velocity (Cartesian)
                            if (FourFSPECOctets[Bit_Ops.Bit1] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  185:T";
                                I062DataItems[ItemIDToIndex("185")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("185")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  185:F";
                                I062DataItems[ItemIDToIndex("185")].CurrentlyPresent = false;
                            }

                            break;

                        case 2:

                            // 210 Calculated Acceleration (Cartesian)
                            if (FourFSPECOctets[Bit_Ops.Bit15] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  210:T";
                                I062DataItems[ItemIDToIndex("210")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("210")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  210:F";
                                I062DataItems[ItemIDToIndex("210")].CurrentlyPresent = false;
                            }

                            // 060 Track Mode 3/A Code
                            if (FourFSPECOctets[Bit_Ops.Bit14] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  060:T";
                                I062DataItems[ItemIDToIndex("060")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("060")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  060:F";
                                I062DataItems[ItemIDToIndex("060")].CurrentlyPresent = false;
                            }

                            // 245 Target Identification 
                            if (FourFSPECOctets[Bit_Ops.Bit13] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  245:T";
                                I062DataItems[ItemIDToIndex("245")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("245")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  245:F";
                                I062DataItems[ItemIDToIndex("245")].CurrentlyPresent = false;
                            }

                            // 380 Aircraft Derived Data
                            if (FourFSPECOctets[Bit_Ops.Bit12] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  380:T";
                                I062DataItems[ItemIDToIndex("380")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("380")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  380:F";
                                I062DataItems[ItemIDToIndex("380")].CurrentlyPresent = false;
                            }

                            // 040 Track Number
                            if (FourFSPECOctets[Bit_Ops.Bit11] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  040:T";
                                I062DataItems[ItemIDToIndex("040")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("040")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  040:F";
                                I062DataItems[ItemIDToIndex("040")].CurrentlyPresent = false;
                            }

                            // 080 Track Status 
                            if (FourFSPECOctets[Bit_Ops.Bit10] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  080:T";
                                I062DataItems[ItemIDToIndex("080")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("080")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  080:F";
                                I062DataItems[ItemIDToIndex("080")].CurrentlyPresent = false;
                            }

                            // 290 System Track Update Ages
                            if (FourFSPECOctets[Bit_Ops.Bit9] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  290:T";
                                I062DataItems[ItemIDToIndex("290")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("290")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  290:F";
                                I062DataItems[ItemIDToIndex("290")].CurrentlyPresent = false;
                            }

                            break;

                        case 3:

                            // 200 Mode of Movement 
                            if (FourFSPECOctets[Bit_Ops.Bit23] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  200:T";
                                I062DataItems[ItemIDToIndex("200")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("200")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  200:F";
                                I062DataItems[ItemIDToIndex("200")].CurrentlyPresent = false;
                            }

                            // 295 Track Data Ages
                            if (FourFSPECOctets[Bit_Ops.Bit22] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  295:T";
                                I062DataItems[ItemIDToIndex("295")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("295")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  295:F";
                                I062DataItems[ItemIDToIndex("295")].CurrentlyPresent = false;
                            }

                            // 136 Measured Flight Level 
                            if (FourFSPECOctets[Bit_Ops.Bit21] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  136:T";
                                I062DataItems[ItemIDToIndex("136")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("136")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  136:F";
                                I062DataItems[ItemIDToIndex("136")].CurrentlyPresent = false;
                            }

                            // 130 Calculated Track Geometric Altitude 
                            if (FourFSPECOctets[Bit_Ops.Bit20] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  130:T";
                                I062DataItems[ItemIDToIndex("130")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("130")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  130:F";
                                I062DataItems[ItemIDToIndex("130")].CurrentlyPresent = false;
                            }

                            // 135 Calculated Track Barometric Altitude
                            if (FourFSPECOctets[Bit_Ops.Bit19] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  135:T";
                                I062DataItems[ItemIDToIndex("135")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("135")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  135:F";
                                I062DataItems[ItemIDToIndex("135")].CurrentlyPresent = false;
                            }

                            // 220 Calculated Rate Of Climb/Descent
                            if (FourFSPECOctets[Bit_Ops.Bit18] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  220:T";
                                I062DataItems[ItemIDToIndex("220")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("220")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  220:F";
                                I062DataItems[ItemIDToIndex("220")].CurrentlyPresent = false;
                            }


                            // 390 Flight Plan Related Data  
                            if (FourFSPECOctets[Bit_Ops.Bit17] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  390:T";
                                I062DataItems[ItemIDToIndex("390")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("390")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  390:F";
                                I062DataItems[ItemIDToIndex("390")].CurrentlyPresent = false;
                            }

                            break;


                        case 4:

                            // 270 Target Size & Orientation
                            if (FourFSPECOctets[Bit_Ops.Bit31] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  270:T";
                                I062DataItems[ItemIDToIndex("270")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("270")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  270:F";
                                I062DataItems[ItemIDToIndex("270")].CurrentlyPresent = false;
                            }

                            // 300 Vehicle Fleet Identification
                            if (FourFSPECOctets[Bit_Ops.Bit30] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  300:T";
                                I062DataItems[ItemIDToIndex("300")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("300")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  300:F";
                                I062DataItems[ItemIDToIndex("300")].CurrentlyPresent = false;
                            }

                            // 110 Mode 5 Data reports & Extended Mode 1 Code
                            if (FourFSPECOctets[Bit_Ops.Bit29] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  110:T";
                                I062DataItems[ItemIDToIndex("110")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("110")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  110:F";
                                I062DataItems[ItemIDToIndex("110")].CurrentlyPresent = false;
                            }

                            // 120 Track Mode 2 Code
                            if (FourFSPECOctets[Bit_Ops.Bit28] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  120:T";
                                I062DataItems[ItemIDToIndex("120")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("120")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  120:F";
                                I062DataItems[ItemIDToIndex("120")].CurrentlyPresent = false;
                            }

                            // 510 Composed Track Number
                            if (FourFSPECOctets[Bit_Ops.Bit27] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  510:T";
                                I062DataItems[ItemIDToIndex("510")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("510")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  510:F";
                                I062DataItems[ItemIDToIndex("510")].CurrentlyPresent = false;
                            }

                            // 500 Estimated Accuracies 
                            if (FourFSPECOctets[Bit_Ops.Bit26] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  500:T";
                                I062DataItems[ItemIDToIndex("500")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("500")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  500:F";
                                I062DataItems[ItemIDToIndex("500")].CurrentlyPresent = false;
                            }

                            // 340 Measured Information  
                            if (FourFSPECOctets[Bit_Ops.Bit25] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  340:T";
                                I062DataItems[ItemIDToIndex("340")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("340")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  340:F";
                                I062DataItems[ItemIDToIndex("340")].CurrentlyPresent = false;
                            }

                            break;

                        // These are Reserved Expansion and Special Purpose fileds.
                        case 5:
 
                            // RE Reserved Expansion Field
                            if (TheFifthFSPECOctet[Bit_Ops.Bit2] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  REF:T";
                                I062DataItems[ItemIDToIndex("REF")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("REF")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  REF:F";
                                I062DataItems[ItemIDToIndex("REF")].CurrentlyPresent = false;
                            }

                            // SP Special Purpose Indicator
                            if (TheFifthFSPECOctet[Bit_Ops.Bit1] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  SPI:T";
                                I062DataItems[ItemIDToIndex("SPI")].HasBeenPresent = true;
                                I062DataItems[ItemIDToIndex("SPI")].CurrentlyPresent = true;
                            }
                            else
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  SPI:F";
                                I062DataItems[ItemIDToIndex("SPI")].CurrentlyPresent = false;
                            }

                            break;

                        // Handle errors
                        default:
                            DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  UKN:T";
                            break;
                    }

                }

                DataOutIndex++;
                CAT62DecodeAndStore.Do(LocalSingleRecordBuffer);
                DataBufferIndexForThisExtraction = DataBufferIndexForThisExtraction + CurrentDataBufferOctalIndex;

            }

            // Return decoded data
            NumOfMessagesDecoded = DataOutIndex;
            return DataOut;

        }
    }
}
