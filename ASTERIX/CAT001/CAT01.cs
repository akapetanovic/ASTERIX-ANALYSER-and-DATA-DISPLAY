using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{

    // Monoradar Data Target Reports, from a Radar Surveillance System to an SDPS
    // (plots and tracks from PSRs, SSRs, MSSRs, excluding Mode S and ground surveillance)
    //
    // PLOT DATA
    //2
    // 2  I001/020 Target Report Descriptor         
    // 1  I001/010 Data Source Identifier                                   1+
    // 3  I001/040 Measured Position in Polar Coordinates       4
    // 4  I001/070 Mode-3/A Code in Octal Representation        2
    // 5  I001/090 Mode-C Code in Binary Representation         2
    // 6  I001/130 Radar Plot Characteristics                   1+
    // 7  I001/141 Truncated Time of Day                        2
    // FX -------- Field Extension Indicator                    -
    //
    // 8   I001/050 Mode-2 Code in Octal Representation         2
    // 9   I001/120 Measured Radial Doppler Speed               1
    // 10  I001/131 Received Power                              1
    // 11  I001/080 Mode-3/A Code Confidence Indicator          2
    // 12  I001/100 Mode-C Code and Code Confidence Indicator   4
    // 13  I001/060 Mode-2 Code Confidence Indicator            2
    // 14  I001/030 Warning/Error Conditions                    1+
    // FX  -------- Field Extension Indicator                   -
    //
    // 15  I001/150 Presence of X-Pulse                         1

    //
    //
    // TRACK DATA
    //
    // 1.  I001/010 Data Source Identifier
    // 2.  I001/020 Target Report Descriptor
    // 3.  I001/161 Track/Plot Number
    // 4.  I001/040 Measured Position in Polar Coordinates
    // 5.  I001/042 Calculated Position in Cartesian Coordinates
    // 6.  I001/200 Calculated Track Velocity in polar Coordinates
    // 7.  I001/070 Mode-3/A Code in Octal Representation
    // FX Field Extension Indicator
    //
    // 8.  I001/090 Mode-C Code in Binary Representation
    // 9.  I001/141 Truncated Time of Day
    // 10. I001/130 Radar Plot Characteristics
    // 11. I001/131 Received Power
    // 12. I001/120 Measured Radial Doppler Speed
    // 13. I001/170 Track Status
    // 14. I001/210 Track Quality
    // FX Field Extension Indicator
    //
    // 15. I001/050 Mode-2 Code in Octal Representation
    // 16. I001/080 Mode-3/A Code Confidence Indica
    // 17. I001/100 Mode-C Code and Code Confidence Indicator
    // 18. I001/060 Mode-2 Code Confidence Indicator
    // 19. I001/030 Warning/Error Conditions
    // 20. - Reserved for Special Purpose Indicator (SP)
    // 21. - Reserved for RFS Indicator (RS-bit)
    // FX Field Extension Indicator
    //
    // 24. I001/150 Presence of X-Pulse

    class CAT01
    {

        // Current data buffer Index
        public static int CurrentDataBufferOctalIndex = 0;

        // Holds the type of last received report, can be undetermined if
        // no report has been received since the application has started.
        public static CAT01I020Types.Type_Of_Report_Type Type_Of_Report = CAT01I020Types.Type_Of_Report_Type.Plot;
        public static CAT01I020Types.Type_Of_Report_Type Previous_Type_Of_Report = CAT01I020Types.Type_Of_Report_Type.Plot;

        // Define a class that holds I001 data
        public class CAT01DataItem
        {
            public string ID;
            public bool HasBeenPresent;
            public bool CurrentlyPresent;
            public string Description;
            public object value;

            // Constructor to set default
            // values.
            public CAT01DataItem()
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

            if (Type_Of_Report == CAT01I020Types.Type_Of_Report_Type.Plot)
            {
                switch (ID)
                {
                    case "010":
                        index = 0;
                        break;
                    case "020":
                        index = 1;
                        break;
                    case "040":
                        index = 2;
                        break;
                    case "070":
                        index = 3;
                        break;
                    case "090":
                        index = 4;
                        break;
                    case "130":
                        index = 5;
                        break;
                    case "141":
                        index = 6;
                        break;
                    case "050":
                        index = 7;
                        break;
                    case "120":
                        index = 8;
                        break;
                    case "131":
                        index = 9;
                        break;
                    case "080":
                        index = 10;
                        break;
                    case "100":
                        index = 11;
                        break;
                    case "060":
                        index = 12;
                        break;
                    case "030":
                        index = 13;
                        break;
                    case "150":
                        index = 14;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (ID)
                {
                    case "010":
                        index = 0;
                        break;
                    case "020":
                        index = 1;
                        break;
                    case "161":
                        index = 2;
                        break;
                    case "040":
                        index = 3;
                        break;
                    case "042":
                        index = 4;
                        break;
                    case "200":
                        index = 5;
                        break;
                    case "070":
                        index = 6;
                        break;
                    case "090":
                        index = 7;
                        break;
                    case "141":
                        index = 8;
                        break;
                    case "130":
                        index = 9;
                        break;
                    case "131":
                        index = 10;
                        break;
                    case "120":
                        index = 11;
                        break;
                    case "170":
                        index = 12;
                        break;
                    case "210":
                        index = 13;
                        break;
                    case "050":
                        index = 14;
                        break;
                    case "080":
                        index = 15;
                        break;
                    case "100":
                        index = 16;
                        break;
                    case "060":
                        index = 17;
                        break;
                    case "030":
                        index = 18;
                        break;
                    case "150":
                        index = 19;
                        break;
                    default:
                        break;
                }
            }

            return index;
        }

        // Define collection of CAT001 data items
        // 
        public static System.Collections.Generic.List<CAT01DataItem> I001DataItems = new System.Collections.Generic.List<CAT01DataItem>();
        // 1. Item presence
        // 2. Item description
        //
        // Based on the data item identifer

        private static void Handle_Type_Of_Report(bool Hard_R)
        {
            if (!Hard_R)
            {
                foreach (CAT01.CAT01DataItem Item in CAT01.I001DataItems)
                    Item.value = null;
            }
            else
            {
                I001DataItems.Clear();

                if (Type_Of_Report == CAT01I020Types.Type_Of_Report_Type.Plot)
                {
                    // 1  I001/010 Data Source Identifier 
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("010")].ID = "010";
                    I001DataItems[ItemIDToIndex("010")].Description = "Data Source Identifier";

                    I001DataItems[ItemIDToIndex("010")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("010")].CurrentlyPresent = false;

                    // 2  I001/020 Target Report Descriptor  
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("020")].ID = "020";
                    I001DataItems[ItemIDToIndex("020")].Description = "Target Report Descriptor";

                    I001DataItems[ItemIDToIndex("020")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("020")].CurrentlyPresent = false;

                    // 3  I001/040 Measured Position in Polar Coordinates
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("040")].ID = "040";
                    I001DataItems[ItemIDToIndex("040")].Description = "Measured Position in Polar Coordinates";

                    I001DataItems[ItemIDToIndex("040")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("040")].CurrentlyPresent = false;

                    // 4  I001/070 Mode-3/A Code in Octal Representation 
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("070")].ID = "070";
                    I001DataItems[ItemIDToIndex("070")].Description = "Mode-3/A Code in Octal Representation";

                    I001DataItems[ItemIDToIndex("070")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("070")].CurrentlyPresent = false;

                    // 5  I001/090 Mode-C Code in Binary Representation 
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("090")].ID = "090";
                    I001DataItems[ItemIDToIndex("090")].Description = "Mode-C Code in Binary Representation";

                    I001DataItems[ItemIDToIndex("090")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("090")].CurrentlyPresent = false;

                    // 6  I001/130 Radar Plot Characteristics  
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("130")].ID = "130";
                    I001DataItems[ItemIDToIndex("130")].Description = "Radar Plot Characteristics";

                    I001DataItems[ItemIDToIndex("130")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("130")].CurrentlyPresent = false;

                    // 7  I001/141 Truncated Time of Day                        
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("141")].ID = "141";
                    I001DataItems[ItemIDToIndex("141")].Description = "Truncated Time of Day";

                    I001DataItems[ItemIDToIndex("141")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("141")].CurrentlyPresent = false;

                    // 8   I001/050 Mode-2 Code in Octal Representation  
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("050")].ID = "050";
                    I001DataItems[ItemIDToIndex("050")].Description = "Mode-2 Code in Octal Representation";

                    I001DataItems[ItemIDToIndex("050")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("050")].CurrentlyPresent = false;

                    // 9   I001/120 Measured Radial Doppler Speed  
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("120")].ID = "120";
                    I001DataItems[ItemIDToIndex("120")].Description = "Measured Radial Doppler Speed";

                    I001DataItems[ItemIDToIndex("120")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("120")].CurrentlyPresent = false;

                    // 10  I001/131 Received Power  
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("131")].ID = "131";
                    I001DataItems[ItemIDToIndex("131")].Description = "Received Power";

                    I001DataItems[ItemIDToIndex("131")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("131")].CurrentlyPresent = false;

                    // 11  I001/080 Mode-3/A Code Confidence Indicator 
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("080")].ID = "080";
                    I001DataItems[ItemIDToIndex("080")].Description = "Mode-3/A Code Confidence Indicator ";

                    I001DataItems[ItemIDToIndex("080")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("080")].CurrentlyPresent = false;

                    // 12  I001/100 Mode-C Code and Code Confidence Indicator
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("100")].ID = "100";
                    I001DataItems[ItemIDToIndex("100")].Description = "Mode-C Code and Code Confidence Indicator";

                    I001DataItems[ItemIDToIndex("100")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("100")].CurrentlyPresent = false;

                    // 13  I001/060 Mode-2 Code Confidence Indicator  
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("060")].ID = "060";
                    I001DataItems[ItemIDToIndex("060")].Description = "Mode-2 Code Confidence Indicator ";

                    I001DataItems[ItemIDToIndex("060")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("060")].CurrentlyPresent = false;

                    // 14  I001/030 Warning/Error Conditions                    
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("030")].ID = "030";
                    I001DataItems[ItemIDToIndex("030")].Description = "Warning/Error Conditions";

                    I001DataItems[ItemIDToIndex("030")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("030")].CurrentlyPresent = false;

                    // 15  I001/150 Presence of X-Pulse                         
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("150")].ID = "150";
                    I001DataItems[ItemIDToIndex("150")].Description = "Presence of X-Pulse";

                    I001DataItems[ItemIDToIndex("150")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("150")].CurrentlyPresent = false;

                }
                else
                {

                    // 1.  I001/010 Data Source Identifier
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("010")].ID = "010";
                    I001DataItems[ItemIDToIndex("010")].Description = "Data Source Identifier";

                    I001DataItems[ItemIDToIndex("010")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("010")].CurrentlyPresent = false;

                    // 2.  I001/020 Target Report Descriptor
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("020")].ID = "020";
                    I001DataItems[ItemIDToIndex("020")].Description = "Target Report Descriptor";

                    I001DataItems[ItemIDToIndex("020")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("020")].CurrentlyPresent = false;

                    // 3.  I001/161 Track/Plot Number
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("161")].ID = "161";
                    I001DataItems[ItemIDToIndex("161")].Description = "Track/Plot Number";

                    I001DataItems[ItemIDToIndex("161")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("161")].CurrentlyPresent = false;

                    // 4.  I001/040 Measured Position in Polar Coordinates
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("040")].ID = "040";
                    I001DataItems[ItemIDToIndex("040")].Description = "Measured Position in Polar Coordinates";

                    I001DataItems[ItemIDToIndex("040")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("040")].CurrentlyPresent = false;

                    // 5.  I001/042 Calculated Position in Cartesian Coordinates
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("042")].ID = "042";
                    I001DataItems[ItemIDToIndex("042")].Description = "Calculated Position in Cartesian Coordinates";

                    I001DataItems[ItemIDToIndex("042")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("042")].CurrentlyPresent = false;

                    // 6.  I001/200 Calculated Track Velocity in polar Coordinates
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("200")].ID = "200";
                    I001DataItems[ItemIDToIndex("200")].Description = "Calculated Track Velocity in polar Coordinates";

                    I001DataItems[ItemIDToIndex("200")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("200")].CurrentlyPresent = false;

                    // 7.  I001/070 Mode-3/A Code in Octal Representation
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("070")].ID = "070";
                    I001DataItems[ItemIDToIndex("070")].Description = "Mode-3/A Code in Octal Representation";

                    I001DataItems[ItemIDToIndex("070")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("070")].CurrentlyPresent = false;

                    // 8.  I001/090 Mode-C Code in Binary Representation
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("090")].ID = "090";
                    I001DataItems[ItemIDToIndex("090")].Description = "Mode-C Code in Binary Representation";

                    I001DataItems[ItemIDToIndex("090")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("090")].CurrentlyPresent = false;

                    // 9.  I001/141 Truncated Time of Day
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("141")].ID = "141";
                    I001DataItems[ItemIDToIndex("141")].Description = "Truncated Time of Day";

                    I001DataItems[ItemIDToIndex("141")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("141")].CurrentlyPresent = false;

                    // 10. I001/130 Radar Plot Characteristics
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("130")].ID = "130";
                    I001DataItems[ItemIDToIndex("130")].Description = "Radar Plot Characteristics";

                    I001DataItems[ItemIDToIndex("130")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("130")].CurrentlyPresent = false;

                    // 11. I001/131 Received Power
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("131")].ID = "131";
                    I001DataItems[ItemIDToIndex("131")].Description = "Received Power";

                    I001DataItems[ItemIDToIndex("131")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("131")].CurrentlyPresent = false;

                    // 12. I001/120 Measured Radial Doppler Speed
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("120")].ID = "120";
                    I001DataItems[ItemIDToIndex("120")].Description = "Measured Radial Doppler Speed";

                    I001DataItems[ItemIDToIndex("120")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("120")].CurrentlyPresent = false;

                    // 13. I001/170 Track Status
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("170")].ID = "170";
                    I001DataItems[ItemIDToIndex("170")].Description = "Track Status";

                    I001DataItems[ItemIDToIndex("170")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("170")].CurrentlyPresent = false;

                    // 14. I001/210 Track Quality
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("210")].ID = "210";
                    I001DataItems[ItemIDToIndex("210")].Description = "Track Quality";

                    I001DataItems[ItemIDToIndex("210")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("210")].CurrentlyPresent = false;


                    // 15. I001/050 Mode-2 Code in Octal Representation
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("050")].ID = "050";
                    I001DataItems[ItemIDToIndex("050")].Description = "Mode-2 Code in Octal Representation";

                    I001DataItems[ItemIDToIndex("050")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("050")].CurrentlyPresent = false;

                    // 16. I001/080 Mode-3/A Code Confidence Indica
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("080")].ID = "080";
                    I001DataItems[ItemIDToIndex("080")].Description = "Mode-3/A Code Confidence Indica";

                    I001DataItems[ItemIDToIndex("080")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("080")].CurrentlyPresent = false;

                    // 17. I001/100 Mode-C Code and Code Confidence Indicator
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("100")].ID = "100";
                    I001DataItems[ItemIDToIndex("100")].Description = "Mode-C Code and Code Confidence Indicator";

                    I001DataItems[ItemIDToIndex("100")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("100")].CurrentlyPresent = false;

                    // 18. I001/060 Mode-2 Code Confidence Indicator
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("060")].ID = "060";
                    I001DataItems[ItemIDToIndex("060")].Description = "Mode-2 Code Confidence Indicator";

                    I001DataItems[ItemIDToIndex("060")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("060")].CurrentlyPresent = false;

                    // 19. I001/030 Warning/Error Conditions
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("030")].ID = "030";
                    I001DataItems[ItemIDToIndex("030")].Description = "Warning/Error Conditions";

                    I001DataItems[ItemIDToIndex("030")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("010")].CurrentlyPresent = false;

                    // 20  I001/150 Presence of X-Pulse                         
                    I001DataItems.Add(new CAT01DataItem());
                    I001DataItems[ItemIDToIndex("150")].ID = "150";
                    I001DataItems[ItemIDToIndex("150")].Description = "Presence of X-Pulse";

                    I001DataItems[ItemIDToIndex("150")].HasBeenPresent = false;
                    I001DataItems[ItemIDToIndex("150")].CurrentlyPresent = false;
                }
            }
        }

        // Resets all Data Item presence flags toi false
        private static void Reset_Currently_Present_Flags()
        {
            foreach (CAT01DataItem Item in I001DataItems)
            {
                Item.CurrentlyPresent = false;
            }

        }

        public static void Intitialize(bool Hard_Reset)
        {
            Handle_Type_Of_Report(Hard_Reset);
        }

        // This method extracts the 8th bit of the Data Item I001/020, Target Report Descriptor
        // to determine the type of the report that was received (1 = tracks, 0 = plot)
        private void Determine_Type_Of_Report(byte[] Data, int Target_Report_Desc_Index)
        {
            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            //Extract the first 
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[Target_Report_Desc_Index];

            if (BO.DWord[CAT01I020Types.Word1_TYP_Index] == true)
            {
                Type_Of_Report = CAT01I020Types.Type_Of_Report_Type.Track;
            }
            else
            {
                Type_Of_Report = CAT01I020Types.Type_Of_Report_Type.Plot;
            }

            if (Previous_Type_Of_Report != Type_Of_Report)
            {
                Previous_Type_Of_Report = Type_Of_Report;
                Handle_Type_Of_Report(true);
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

            // SIC/SAC Indexes
            int SIC_Index = 0;
            int SAC_Index = 0;

            // Lenght of the current record's FSPECs
            int FSPEC_Length = 0;

            // The four possible FSPEC octets
            BitVector32 FourFSPECOctets = new BitVector32();

            while (DataBufferIndexForThisExtraction < LengthOfDataBlockInBytes)
            {
                // Assume that there will be no more than 100 bytes in one record
                byte[] LocalSingleRecordBuffer = new byte[1000];

                Array.Copy(DataBlockBuffer, DataBufferIndexForThisExtraction, LocalSingleRecordBuffer, 0, (LengthOfDataBlockInBytes - DataBufferIndexForThisExtraction));

                // Get all four data words, but use only the number specifed 
                // by the length of FSPEC words
                FourFSPECOctets = ASTERIX.GetFourFSPECOctets(LocalSingleRecordBuffer);

                // Determine Length of FSPEC fields in bytes
                FSPEC_Length = ASTERIX.DetermineLenghtOfFSPEC(LocalSingleRecordBuffer);

                // Check whether 010 is present
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

                    //Call method to determine the type of the report 
                    // that has been received, (plot or track)
                    Determine_Type_Of_Report(LocalSingleRecordBuffer, CurrentDataBufferOctalIndex);

                    ///////////////////////////////////////////////////////////////////////////
                    // Populate the current SIC/SAC and Time stamp for this meesage
                    //
                    I001DataItems[ItemIDToIndex("010")].value =
                        new ASTERIX.SIC_SAC_Time(LocalSingleRecordBuffer[SIC_Index], LocalSingleRecordBuffer[SAC_Index], ASTERIX.TimeOfReception);
                }
                else
                {
                    // Extract SIC/SAC Indexes.
                    DataOut[DataOutIndex] = "---" + '/' + "---";
                    CurrentDataBufferOctalIndex = FSPEC_Length;

                    //Call method to determine the type of the report 
                    // that has been received, (plot or track)
                    Determine_Type_Of_Report(LocalSingleRecordBuffer, CurrentDataBufferOctalIndex);
                }

                Reset_Currently_Present_Flags();

                // Loop for each FSPEC and determine what data item is present
                for (int FSPEC_Index = 1; FSPEC_Index <= FSPEC_Length; FSPEC_Index++)
                {
                    if (Type_Of_Report == CAT01I020Types.Type_Of_Report_Type.Plot)
                    {
                        switch (FSPEC_Index)
                        {
                            case 1:

                                // 010 Data Source Identifier
                                if (FourFSPECOctets[Bit_Ops.Bit7] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  010:T";
                                    I001DataItems[ItemIDToIndex("010")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("010")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  010:F";

                                // 020 Target Report Descriptor
                                if (FourFSPECOctets[Bit_Ops.Bit6] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  020:T";
                                    I001DataItems[ItemIDToIndex("020")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("020")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  020:F";

                                // I001/040 Measured Position in Polar Coordinates
                                if (FourFSPECOctets[Bit_Ops.Bit5] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  040:T";
                                    I001DataItems[ItemIDToIndex("040")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("040")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  040:F";

                                // I001/070 Mode-3/A Code in Octal Representation 
                                if (FourFSPECOctets[Bit_Ops.Bit4] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  070:T";
                                    I001DataItems[ItemIDToIndex("070")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("070")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  070:F";

                                // I001/090 Mode-C Code in Binary Representation
                                if (FourFSPECOctets[Bit_Ops.Bit3] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  090:T";
                                    I001DataItems[ItemIDToIndex("090")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("090")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  090:F";

                                // I001/130 Radar Plot Characteristics 
                                if (FourFSPECOctets[Bit_Ops.Bit2] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  130:T";
                                    I001DataItems[ItemIDToIndex("130")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("130")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  130:F";

                                // I001/141 Truncated Time of Day 
                                if (FourFSPECOctets[Bit_Ops.Bit1] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  141:T";
                                    I001DataItems[ItemIDToIndex("141")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("141")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  141:F";

                                break;
                            case 2:

                                // I001/050 Mode-2 Code in Octal Representation
                                if (FourFSPECOctets[Bit_Ops.Bit15] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  050:T";
                                    I001DataItems[ItemIDToIndex("050")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("050")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  050:F";

                                // I001/120 Measured Radial Doppler Speed  
                                if (FourFSPECOctets[Bit_Ops.Bit14] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  120:T";
                                    I001DataItems[ItemIDToIndex("120")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("120")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  120:F";

                                // I001/131 Received Power  
                                if (FourFSPECOctets[Bit_Ops.Bit13] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  131:T";
                                    I001DataItems[ItemIDToIndex("131")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("131")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  131:F";

                                // I001/080 Mode-3/A Code Confidence Indicator
                                if (FourFSPECOctets[Bit_Ops.Bit12] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  080:T";
                                    I001DataItems[ItemIDToIndex("080")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("080")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  080:F";

                                // I001/100 Mode-C Code and Code Confidence Indicator
                                if (FourFSPECOctets[Bit_Ops.Bit11] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  100:T";
                                    I001DataItems[ItemIDToIndex("100")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("100")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  100:F";

                                // I001/060 Mode-2 Code Confidence Indicator
                                if (FourFSPECOctets[Bit_Ops.Bit10] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  060:T";
                                    I001DataItems[ItemIDToIndex("060")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("060")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  060:F";

                                //  I001/030 Warning/Error Conditions
                                if (FourFSPECOctets[Bit_Ops.Bit9] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  030:T";
                                    I001DataItems[ItemIDToIndex("030")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("030")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  030:F";

                                break;
                            case 3:

                                // I001/150 Presence of X-Pulse
                                if (FourFSPECOctets[Bit_Ops.Bit23] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  150:T";
                                    I001DataItems[ItemIDToIndex("150")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("150")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  150:F";

                                break;

                            // Handle errors
                            default:
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  UKN:T";
                                break;
                        }

                    }
                    else
                    {
                        switch (FSPEC_Index)
                        {
                            case 1:

                                // 010 Data Source Identifier
                                if (FourFSPECOctets[Bit_Ops.Bit7] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  010:T";
                                    I001DataItems[ItemIDToIndex("010")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("010")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  010:F";

                                // 020 Target Report Descriptor
                                if (FourFSPECOctets[Bit_Ops.Bit6] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  020:T";
                                    I001DataItems[ItemIDToIndex("020")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("020")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  020:F";

                                // 161 Track/Plot Number
                                if (FourFSPECOctets[Bit_Ops.Bit5] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  161:T";
                                    I001DataItems[ItemIDToIndex("161")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("161")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  161:F";

                                // 040 Measured Position in Polar Coordinates
                                if (FourFSPECOctets[Bit_Ops.Bit4] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  040:T";
                                    I001DataItems[ItemIDToIndex("040")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("040")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  040:F";

                                // 042 Calculated Position in Cartesian Coordinates
                                if (FourFSPECOctets[Bit_Ops.Bit3] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  042:T";
                                    I001DataItems[ItemIDToIndex("042")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("042")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  042:F";

                                // 200 Calculated Track Velocity in polar Coordinates
                                if (FourFSPECOctets[Bit_Ops.Bit2] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  200:T";
                                    I001DataItems[ItemIDToIndex("200")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("200")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  200:F";

                                // 070 Mode-3/A Code in Octal Representation 
                                if (FourFSPECOctets[Bit_Ops.Bit1] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  070:T";
                                    I001DataItems[ItemIDToIndex("070")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("070")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  070:F";

                                break;
                            case 2:

                                // 090 Mode-C Code in Binary Representation
                                if (FourFSPECOctets[Bit_Ops.Bit15] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  090:T";
                                    I001DataItems[ItemIDToIndex("090")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("090")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  090:F";

                                // 141 Truncated Time of Day
                                if (FourFSPECOctets[Bit_Ops.Bit14] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  141:T";
                                    I001DataItems[ItemIDToIndex("141")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("141")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  141:F";

                                // 130 Radar Plot Characteristics
                                if (FourFSPECOctets[Bit_Ops.Bit13] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  130:T";
                                    I001DataItems[ItemIDToIndex("130")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("130")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  130:F";

                                // 131 Received Power
                                if (FourFSPECOctets[Bit_Ops.Bit12] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  131:T";
                                    I001DataItems[ItemIDToIndex("131")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("131")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  131:F";

                                // 120 Measured Radial Doppler Speed
                                if (FourFSPECOctets[Bit_Ops.Bit11] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  120:T";
                                    I001DataItems[ItemIDToIndex("120")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("120")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  120:F";

                                // 170 Track Status
                                if (FourFSPECOctets[Bit_Ops.Bit10] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  170:T";
                                    I001DataItems[ItemIDToIndex("170")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("170")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  170:F";

                                //  210 Track Quality
                                if (FourFSPECOctets[Bit_Ops.Bit9] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  210:T";
                                    I001DataItems[ItemIDToIndex("210")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("210")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  210:F";

                                break;
                            case 3:

                                // 050 Mode-2 Code in Octal Representation
                                if (FourFSPECOctets[Bit_Ops.Bit23] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  050:T";
                                    I001DataItems[ItemIDToIndex("050")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("050")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  050:F";

                                //  080 Mode-3/A Code Confidence Indica
                                if (FourFSPECOctets[Bit_Ops.Bit22] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  080:T";
                                    I001DataItems[ItemIDToIndex("080")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("080")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  080:F";

                                //  100 Mode-C Code and Code Confidence Indicator
                                if (FourFSPECOctets[Bit_Ops.Bit21] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  100:T";
                                    I001DataItems[ItemIDToIndex("100")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("100")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  100:F";

                                //  060 Mode-2 Code Confidence Indicator
                                if (FourFSPECOctets[Bit_Ops.Bit20] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  060:T";
                                    I001DataItems[ItemIDToIndex("060")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("060")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  060:F";

                                //  030 Warning/Error Conditions
                                if (FourFSPECOctets[Bit_Ops.Bit19] == true)
                                {
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  030:T";
                                    I001DataItems[ItemIDToIndex("030")].HasBeenPresent = true;
                                    I001DataItems[ItemIDToIndex("030")].CurrentlyPresent = true;
                                }
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  030:F";

                                //  Reserved for Special Purpose Indicator (SP)
                                if (FourFSPECOctets[Bit_Ops.Bit18] == true)
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  SPI:T";
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  SPI:F";

                                //  Reserved for RFS Indicator (RS-bit)
                                if (FourFSPECOctets[Bit_Ops.Bit17] == true)
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  RSB:T";
                                else
                                    DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  RSB:F";

                                break;
                            // Handle errors
                            default:
                                DataOut[DataOutIndex] = DataOut[DataOutIndex] + "  UKN:T";
                                break;
                        }
                    }
                }

                DataOutIndex++;
                CAT01DecodeAndStore.Do(LocalSingleRecordBuffer);
                DataBufferIndexForThisExtraction = DataBufferIndexForThisExtraction + CurrentDataBufferOctalIndex + 1;
            }

            // Return decoded data
            NumOfMessagesDecoded = DataOutIndex;
            return DataOut;
        }
    }
}