using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    ////////////////////////////////////////////////////////////////////////////
    //
    // Next version of Category 001: PSR Radar, M-SSR Radar, Mode-S Station
    //
    // I048/010 	Data Source Identifier                              2
    // I048/140 	Time-of-Day                                         3
    // I048/020 	Target Report Descriptor                            1+
    // I048/040 	Measured Position in Slant Polar Coordinates        4
    // I048/070 	Mode-3/A Code in Octal Representation               2
    // I048/090 	Flight Level in Binary Representation               2
    // I048/130 	Radar Plot Characteristics                          1 + 1+
    // n.a. 	    Field Extension Indicator 

    // I048/220 	Aircraft Address                                    3
    // I048/240 	Aircraft Identification                             6
    // I048/250 	Mode S MB Data                                      1+8*N
    // I048/161 	Track Number                                        2
    // I048/042 	Calculated Position in Cartesian Coordinates        4
    // I048/200 	Calculated Track Velocity in Polar Representation   4
    // I048/170 	Track Status                                        1+
    // n.a. 	    Field Extension Indicator 

    // I048/210 	Track Quality                                       4
    // I048/030 	Warning/Error Conditions                            1+
    // I048/080 	Mode-3/A Code Confidence Indicator                  2
    // I048/100 	Mode-C Code and Confidence Indicator                4
    // I048/110 	Height Measured by 3D Radar                         2
    // I048/120 	Radial Doppler Speed                                1+
    // I048/230 	Communications / ACAS Capability and Flight Status  2
    // n.a. 	    Field Extension Indicator 

    // I048/260 	ACAS Resolution Advisory Report                     7
    // I048/055 	Mode-1 Code in Octal Representation                 1
    // I048/050 	Mode-2 Code in Octal Representation                 2
    // I048/065 	Mode-1 Code Confidence Indicator                    1
    // I048/060 	Mode-2 Code Confidence Indicator                    2
    // SP-Data Item 	Special Purpose Field                           1+1+
    // RE-Data Item 	Reserved Expansion Field                        1+1+
    // n.a. 	Field Extension Indicator
    ////////////////////////////////////////////////////////////////////////////

    class CAT48
    {
        // Current data buffer Index
        public static int CurrentDataBufferOctalIndex = 0;

        // Define a class that holds I048 data
        public class CAT48DataItem
        {
            public string ID;
            public bool HasBeenPresent;
            public bool CurrentlyPresent;
            public string Description;
            public object value;

            // Constructor to set default
            // values.
            public CAT48DataItem()
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
                case "140":
                    index = 1;
                    break;
                case "020":
                    index = 2;
                    break;
                case "040":
                    index = 3;
                    break;
                case "070":
                    index = 4;
                    break;
                case "090":
                    index = 5;
                    break;
                case "130":
                    index = 6;
                    break;
                case "220":
                    index = 7;
                    break;
                case "240":
                    index = 8;
                    break;
                case "250":
                    index = 9;
                    break;
                case "161":
                    index = 10;
                    break;
                case "042":
                    index = 11;
                    break;
                case "200":
                    index = 12;
                    break;
                case "170":
                    index = 13;
                    break;
                case "210":
                    index = 14;
                    break;
                case "030":
                    index = 15;
                    break;
                case "080":
                    index = 16;
                    break;
                case "100":
                    index = 17;
                    break;
                case "110":
                    index = 18;
                    break;
                case "120":
                    index = 19;
                    break;
                case "230":
                    index = 20;
                    break;
                case "260":
                    index = 21;
                    break;
                case "055":
                    index = 22;
                    break;
                case "050":
                    index = 23;
                    break;
                case "065":
                    index = 24;
                    break;
                case "060":
                    index = 25;
                    break;
                default:
                    break;
            }

            return index;
        }

        // Define collection of CAT048 data items. Used to store and retrieve basic data such as:
        // 
        public static System.Collections.Generic.List<CAT48DataItem> I048DataItems = new System.Collections.Generic.List<CAT48DataItem>();
        // 1. Item presence
        // 2. Item description
        //
        // Based on the data item identifer


        public static void Intitialize(bool Hard_Reset)
        {
            if (!Hard_Reset)
            {
                foreach (CAT48.CAT48DataItem Item in CAT48.I048DataItems)
                    Item.value = null;
            }
            else
            {
                I048DataItems.Clear();

                // I048/010 Data Source Identifier
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("010")].ID = "010";
                I048DataItems[ItemIDToIndex("010")].Description = "Data Source Identifier";

                I048DataItems[ItemIDToIndex("010")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("010")].CurrentlyPresent = false;

                // I048/140 Time-of-Day   
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("140")].ID = "140";
                I048DataItems[ItemIDToIndex("140")].Description = "Time-of-Day";

                I048DataItems[ItemIDToIndex("140")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("140")].CurrentlyPresent = false;
                // I048/020 Target Report Descriptor 
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("020")].ID = "020";
                I048DataItems[ItemIDToIndex("020")].Description = "Target Report Descriptor";

                I048DataItems[ItemIDToIndex("020")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("020")].CurrentlyPresent = false;
                // I048/040 Measured Position in Slant Polar Coordinates  
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("040")].ID = "040";
                I048DataItems[ItemIDToIndex("040")].Description = "Measured Position in Slant Polar Coordinates";

                I048DataItems[ItemIDToIndex("040")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("040")].CurrentlyPresent = false;
                // I048/070 Mode-3/A Code in Octal Representation  
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("070")].ID = "070";
                I048DataItems[ItemIDToIndex("070")].Description = "Mode-3/A Code in Octal Representation";

                I048DataItems[ItemIDToIndex("070")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("070")].CurrentlyPresent = false;
                // I048/090 Flight Level in Binary Representation 
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("090")].ID = "090";
                I048DataItems[ItemIDToIndex("090")].Description = "Flight Level in Binary Representation";

                I048DataItems[ItemIDToIndex("090")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("090")].CurrentlyPresent = false;
                // I048/130 Radar Plot Characteristics 
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("130")].ID = "130";
                I048DataItems[ItemIDToIndex("130")].Description = "Radar Plot Characteristics";

                I048DataItems[ItemIDToIndex("130")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("130")].CurrentlyPresent = false;
                // I048/220 Aircraft Address
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("220")].ID = "220";
                I048DataItems[ItemIDToIndex("220")].Description = "Aircraft Address";

                I048DataItems[ItemIDToIndex("220")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("220")].CurrentlyPresent = false;
                // I048/240 Aircraft Identification 
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("240")].ID = "240";
                I048DataItems[ItemIDToIndex("240")].Description = "Aircraft Identification";

                I048DataItems[ItemIDToIndex("240")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("240")].CurrentlyPresent = false;
                // I048/250 Mode S MB Data 
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("250")].ID = "250";
                I048DataItems[ItemIDToIndex("250")].Description = "Mode S MB Data";

                I048DataItems[ItemIDToIndex("250")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("250")].CurrentlyPresent = false;
                // I048/161 Track Number 
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("161")].ID = "161";
                I048DataItems[ItemIDToIndex("161")].Description = "Track Number";

                I048DataItems[ItemIDToIndex("161")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("161")].CurrentlyPresent = false;
                // I048/042 Calculated Position in Cartesian Coordinates 
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("042")].ID = "042";
                I048DataItems[ItemIDToIndex("042")].Description = "Calculated Position in Cartesian Coordinates";

                I048DataItems[ItemIDToIndex("042")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("042")].CurrentlyPresent = false;
                // I048/200 Calculated Track Velocity in Polar Representation 
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("200")].ID = "200";
                I048DataItems[ItemIDToIndex("200")].Description = "Calculated Track Velocity in Polar Representation";

                I048DataItems[ItemIDToIndex("200")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("200")].CurrentlyPresent = false;
                // I048/170 Track Status  
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("170")].ID = "170";
                I048DataItems[ItemIDToIndex("170")].Description = "Track Status";

                I048DataItems[ItemIDToIndex("170")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("170")].CurrentlyPresent = false;
                // I048/210 Track Quality   
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("210")].ID = "210";
                I048DataItems[ItemIDToIndex("210")].Description = "Track Quality";

                I048DataItems[ItemIDToIndex("210")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("210")].CurrentlyPresent = false;
                // I048/030 Warning/Error Conditions  
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("030")].ID = "030";
                I048DataItems[ItemIDToIndex("030")].Description = "Warning/Error Conditions";

                I048DataItems[ItemIDToIndex("030")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("030")].CurrentlyPresent = false;
                // I048/080 Mode-3/A Code Confidence Indicator 
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("080")].ID = "080";
                I048DataItems[ItemIDToIndex("080")].Description = "Mode-3/A Code Confidence Indicator";
                I048DataItems[ItemIDToIndex("080")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("080")].CurrentlyPresent = false;
                // I048/100 Mode-C Code and Confidence Indicator 
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("100")].ID = "100";
                I048DataItems[ItemIDToIndex("100")].Description = "Mode-C Code and Confidence Indicator";

                I048DataItems[ItemIDToIndex("100")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("100")].CurrentlyPresent = false;
                // I048/110 Height Measured by 3D Radar 
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("110")].ID = "110";
                I048DataItems[ItemIDToIndex("110")].Description = "Height Measured by 3D Radar";

                I048DataItems[ItemIDToIndex("110")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("110")].CurrentlyPresent = false;
                // I048/120 Radial Doppler Speed 
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("120")].ID = "120";
                I048DataItems[ItemIDToIndex("120")].Description = "Radial Doppler Speed";

                I048DataItems[ItemIDToIndex("120")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("120")].CurrentlyPresent = false;
                // I048/230 Communications / ACAS Capability and Flight Status
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("230")].ID = "230";
                I048DataItems[ItemIDToIndex("230")].Description = "Communications / ACAS Capability and Flight Status";

                I048DataItems[ItemIDToIndex("230")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("230")].CurrentlyPresent = false;
                // I048/260 ACAS Resolution Advisory Report
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("260")].ID = "260";
                I048DataItems[ItemIDToIndex("260")].Description = "ACAS Resolution Advisory Report";

                I048DataItems[ItemIDToIndex("260")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("260")].CurrentlyPresent = false;
                // I048/055 Mode-1 Code in Octal Representation
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("055")].ID = "055";
                I048DataItems[ItemIDToIndex("055")].Description = "Mode-1 Code in Octal Representation";

                I048DataItems[ItemIDToIndex("055")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("055")].CurrentlyPresent = false;
                // I048/050 Mode-2 Code in Octal Representation 
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("050")].ID = "050";
                I048DataItems[ItemIDToIndex("050")].Description = "Mode-2 Code in Octal Representation";

                I048DataItems[ItemIDToIndex("050")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("050")].CurrentlyPresent = false;
                // I048/065 Mode-1 Code Confidence Indicator
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("065")].ID = "065";
                I048DataItems[ItemIDToIndex("065")].Description = "Mode-1 Code Confidence Indicator";

                I048DataItems[ItemIDToIndex("065")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("065")].CurrentlyPresent = false;
                // I048/060 Mode-2 Code Confidence Indicator 
                I048DataItems.Add(new CAT48DataItem());
                I048DataItems[ItemIDToIndex("060")].ID = "060";
                I048DataItems[ItemIDToIndex("060")].Description = "Mode-2 Code Confidence Indicator";

                I048DataItems[ItemIDToIndex("060")].HasBeenPresent = false;
                I048DataItems[ItemIDToIndex("060")].CurrentlyPresent = false;
            }
        }

        // Resets all Data Item presence flags toi false
        private static void Reset_Currently_Present_Flags()
        {
            foreach (CAT48DataItem Item in I048DataItems)
            {
                Item.CurrentlyPresent = false;
            }

        }

        public string[] Decode(byte[] DataBlockBuffer, string Time, out int NumOfMessagesDecoded)
        {
            // Define output data buffer
            string[] DataOut = new string[1000];

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

            // The four possible FSPEC octets
            BitVector32 FourFSPECOctets = new BitVector32();

            while ((DataBufferIndexForThisExtraction) < LengthOfDataBlockInBytes)
            {
                // Assume that there will be no more than 300 bytes in one record
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

                    ///////////////////////////////////////////////////////////////////////////
                    // Populate the current SIC/SAC and Time stamp for this meesage
                    //
                    I048DataItems[ItemIDToIndex("010")].value =
                        new ASTERIX.SIC_SAC_Time(LocalSingleRecordBuffer[SIC_Index], LocalSingleRecordBuffer[SAC_Index], ASTERIX.TimeOfReception);
                }
                else
                {
                    CurrentDataBufferOctalIndex = FSPEC_Length;
                    // Extract SIC/SAC Indexes.
                    DataOut[DataOutIndex] = "---" + '/' + "---";
                }

                Reset_Currently_Present_Flags();

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
                                I048DataItems[ItemIDToIndex("010")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("010")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  010:F";

                            // 140 Time-of-Day
                            if (FourFSPECOctets[Bit_Ops.Bit6] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  140:T";
                                I048DataItems[ItemIDToIndex("140")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("140")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  140:F";

                            // 020 Target Report Descriptor
                            if (FourFSPECOctets[Bit_Ops.Bit5] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  020:T";
                                I048DataItems[ItemIDToIndex("020")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("020")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  020:F";

                            // 040 Measured Position in Slant Polar Coordinates
                            if (FourFSPECOctets[Bit_Ops.Bit4] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  040:T";
                                I048DataItems[ItemIDToIndex("040")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("040")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  040:F";

                            // 070 Mode-3/A Code in Octal Representation
                            if (FourFSPECOctets[Bit_Ops.Bit3] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  070:T";
                                I048DataItems[ItemIDToIndex("070")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("070")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  070:F";

                            // 090 Flight Level in Binary Representation
                            if (FourFSPECOctets[Bit_Ops.Bit2] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  090:T";
                                I048DataItems[ItemIDToIndex("090")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("090")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  090:F";

                            // 130 Radar Plot Characteristics
                            if (FourFSPECOctets[Bit_Ops.Bit1] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  130:T";
                                I048DataItems[ItemIDToIndex("130")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("130")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  130:F";

                            break;
                        case 2:

                            // 220 Aircraft Address
                            if (FourFSPECOctets[Bit_Ops.Bit15] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  220:T";
                                I048DataItems[ItemIDToIndex("220")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("220")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  220:F";

                            // 240 Aircraft Identification
                            if (FourFSPECOctets[Bit_Ops.Bit14] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  240:T";
                                I048DataItems[ItemIDToIndex("240")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("240")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  240:F";

                            // 250 Mode S MB Data
                            if (FourFSPECOctets[Bit_Ops.Bit13] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  250:T";
                                I048DataItems[ItemIDToIndex("250")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("250")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  250:F";

                            // 161 Track Number
                            if (FourFSPECOctets[Bit_Ops.Bit12] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  161:T";
                                I048DataItems[ItemIDToIndex("161")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("161")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  161:F";

                            // 042 Calculated Position in Cartesian Coordinates 
                            if (FourFSPECOctets[Bit_Ops.Bit11] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  042:T";
                                I048DataItems[ItemIDToIndex("042")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("042")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  042:F";

                            // 200 Calculated Track Velocity in Polar Representation
                            if (FourFSPECOctets[Bit_Ops.Bit10] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  200:T";
                                I048DataItems[ItemIDToIndex("200")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("200")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  200:F";

                            // 170 Track Status
                            if (FourFSPECOctets[Bit_Ops.Bit9] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  170:T";
                                I048DataItems[ItemIDToIndex("170")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("170")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  170:F";

                            break;
                        case 3:

                            // 210 Track Quality 
                            if (FourFSPECOctets[Bit_Ops.Bit23] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  210:T";
                                I048DataItems[ItemIDToIndex("210")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("210")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  210:F";

                            // 030 Warning/Error Conditions
                            if (FourFSPECOctets[Bit_Ops.Bit22] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  030:T";
                                I048DataItems[ItemIDToIndex("030")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("030")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  030:F";

                            // 080 Mode-3/A Code Confidence Indicator
                            if (FourFSPECOctets[Bit_Ops.Bit21] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  080:T";
                                I048DataItems[ItemIDToIndex("080")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("080")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  080:F";

                            // 100 Mode-C Code and Confidence Indicator 
                            if (FourFSPECOctets[Bit_Ops.Bit20] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  100:T";
                                I048DataItems[ItemIDToIndex("100")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("100")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  100:F";

                            // 110 Height Measured by 3D Radar
                            if (FourFSPECOctets[Bit_Ops.Bit19] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  110:T";
                                I048DataItems[ItemIDToIndex("110")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("110")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  110:F";

                            // 120 Radial Doppler Speed 
                            if (FourFSPECOctets[Bit_Ops.Bit18] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  120:T";
                                I048DataItems[ItemIDToIndex("120")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("120")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  120:F";

                            // 230 Communications / ACAS Capability and Flight Status
                            if (FourFSPECOctets[Bit_Ops.Bit17] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  230:T";
                                I048DataItems[ItemIDToIndex("230")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("230")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  230:F";

                            break;
                        case 4:

                            // 260 ACAS Resolution Advisory Report 
                            if (FourFSPECOctets[Bit_Ops.Bit31] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  260:T";
                                I048DataItems[ItemIDToIndex("260")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("260")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  260:F";

                            // 55 Mode-1 Code in Octal Representation
                            if (FourFSPECOctets[Bit_Ops.Bit30] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  55:T";
                                I048DataItems[ItemIDToIndex("055")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("055")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  55:F";

                            // 50 Mode-2 Code in Octal Representation
                            if (FourFSPECOctets[Bit_Ops.Bit29] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  50:T";
                                I048DataItems[ItemIDToIndex("050")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("050")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  50:F";

                            // 65 Mode-1 Code Confidence Indicator
                            if (FourFSPECOctets[Bit_Ops.Bit28] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  65:T";
                                I048DataItems[ItemIDToIndex("065")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("065")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  65:F";

                            // 60 Mode-2 Code Confidence Indicator
                            if (FourFSPECOctets[Bit_Ops.Bit27] == true)
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  60:T";
                                I048DataItems[ItemIDToIndex("060")].HasBeenPresent = true;
                                I048DataItems[ItemIDToIndex("060")].CurrentlyPresent = true;
                            }
                            else
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  60:F";


                            if ((FourFSPECOctets[Bit_Ops.Bit26] == true) || (FourFSPECOctets[Bit_Ops.Bit25] == true)
                                || (FourFSPECOctets[Bit_Ops.Bit24] == true))
                            {
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  SPECIAL PURPOSE";
                            }


                            break;

                        // Handle errors
                        default:
                            DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  UKN:T";
                            break;
                    }
                }

                DataOutIndex++;
                CAT48DecodeAndStore.Do(LocalSingleRecordBuffer);
                DataBufferIndexForThisExtraction = DataBufferIndexForThisExtraction + CurrentDataBufferOctalIndex + 1;
            }

            // Return decoded data
            NumOfMessagesDecoded = DataOutIndex;
            return DataOut;
        }
    }
}
