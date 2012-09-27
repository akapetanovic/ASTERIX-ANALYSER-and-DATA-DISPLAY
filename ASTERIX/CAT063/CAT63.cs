using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT63
    {
        //////////////////////////////////////////////////////////////////
        // Sensor Status Messages
        //
        // 1. I063/010 Data Source Identifier               2
        // 2. I063/015 Service Identification               1
        // 3. I063/030 Time of Message                      3
        // 4. I063/050 Sensor Identifier                    2
        // 5. I063/060 Sensor Configuration and Status      2
        // 6. I063/070 Time Stamping Bias                   2
        // 7. I063/080 SSR/Mode S Range Gain and Bias       4
        // FX - Field extension indicator -
        //
        // 8. I063/081 SSR/Mode S Azimuth Bias              2
        // 9. I063/090 PSR Range Gain and Bias              4
        // 10. I063/091 PSR Azimuth Bias                    2
        // 11. I063/092 PSR Elevation Bias                  2
        // 12. - Spare -                                    -
        // 13. RE Reserved Expansion Field                  1+1+
        // 14. SP Special Purpose Field                     1+1+
        // FX - Field extension indicator                   -
        /// <summary>
        /// ///////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>

        // Define a class that holds I063 data
        public class I063DataItem
        {
            public string ID;
            public string Description;
            public bool IsPresent;
            public object value;

            // Constructor to set default
            // values.
            public I063DataItem()
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
                case "015":
                    index = 1;
                    break;
                case "030":
                    index = 2;
                    break;
                case "050":
                    index = 3;
                    break;
                case "060":
                    index = 4;
                    break;
                case "070":
                    index = 5;
                    break;
                case "080":
                    index = 6;
                    break;
                case "081":
                    index = 7;
                    break;
                case "090":
                    index = 8;
                    break;
                case "091":
                    index = 9;
                    break;
                case "092":
                    index = 10;
                    break;
                default:
                    break;
            }

            return index;
        }

        // Define collection of CAT001 data items. Used to store and retrieve basic data such as:
        // 
        public static System.Collections.Generic.List<I063DataItem> I063DataItems = new System.Collections.Generic.List<I063DataItem>();
        // 1. Item presence
        // 2. Item description
        //
        // Based on the data item identifer


        public static void Intitialize()
        {
            I063DataItems.Clear();

            // 1 I063/010 Data Source Identifier 
            I063DataItems.Add(new I063DataItem());
            I063DataItems[ItemIDToIndex("010")].ID = "010";
            I063DataItems[ItemIDToIndex("010")].Description = "Data Source Identifier";
            I063DataItems[ItemIDToIndex("010")].IsPresent = false;

            // 2 I063/015 Service Identification
            I063DataItems.Add(new I063DataItem());
            I063DataItems[ItemIDToIndex("015")].ID = "015";
            I063DataItems[ItemIDToIndex("015")].Description = "Service Identification";
            I063DataItems[ItemIDToIndex("015")].IsPresent = false;

            // 3. I063/030 Time of Message
            I063DataItems.Add(new I063DataItem());
            I063DataItems[ItemIDToIndex("030")].ID = "030";
            I063DataItems[ItemIDToIndex("030")].Description = "Time of Message";
            I063DataItems[ItemIDToIndex("030")].IsPresent = false;

            // 4. I063/050 Sensor Identifier  
            I063DataItems.Add(new I063DataItem());
            I063DataItems[ItemIDToIndex("050")].ID = "050";
            I063DataItems[ItemIDToIndex("050")].Description = "Sensor Identifier";
            I063DataItems[ItemIDToIndex("050")].IsPresent = false;

            // 5. I063/060 Sensor Configuration and Status 
            I063DataItems.Add(new I063DataItem());
            I063DataItems[ItemIDToIndex("060")].ID = "060";
            I063DataItems[ItemIDToIndex("060")].Description = " Sensor Configuration and Status";
            I063DataItems[ItemIDToIndex("060")].IsPresent = false;

            // 6. I063/070 Time Stamping Bias 
            I063DataItems.Add(new I063DataItem());
            I063DataItems[ItemIDToIndex("070")].ID = "070";
            I063DataItems[ItemIDToIndex("070")].Description = "Time Stamping Bias";
            I063DataItems[ItemIDToIndex("070")].IsPresent = false;

            // 7. I063/080 SSR/Mode S Range Gain and Bias  
            I063DataItems.Add(new I063DataItem());
            I063DataItems[ItemIDToIndex("080")].ID = "080";
            I063DataItems[ItemIDToIndex("080")].Description = "SSR/Mode S Range Gain and Bias";
            I063DataItems[ItemIDToIndex("080")].IsPresent = false;

            // 8. I063/081 SSR/Mode S Azimuth Bias 
            I063DataItems.Add(new I063DataItem());
            I063DataItems[ItemIDToIndex("081")].ID = "081";
            I063DataItems[ItemIDToIndex("081")].Description = "SSR/Mode S Azimuth Bias";
            I063DataItems[ItemIDToIndex("081")].IsPresent = false;

            // 9. I063/090 PSR Range Gain and Bias        
            I063DataItems.Add(new I063DataItem());
            I063DataItems[ItemIDToIndex("090")].ID = "090";
            I063DataItems[ItemIDToIndex("090")].Description = "PSR Range Gain and Bias";
            I063DataItems[ItemIDToIndex("090")].IsPresent = false;

            // 10. I063/091 PSR Azimuth Bias 
            I063DataItems.Add(new I063DataItem());
            I063DataItems[ItemIDToIndex("091")].ID = "091";
            I063DataItems[ItemIDToIndex("091")].Description = "PSR Azimuth Bias ";
            I063DataItems[ItemIDToIndex("091")].IsPresent = false;

            // 11. I063/092 PSR Elevation Bias  
            I063DataItems.Add(new I063DataItem());
            I063DataItems[ItemIDToIndex("092")].ID = "092";
            I063DataItems[ItemIDToIndex("092")].Description = "PSR Elevation Bias";
            I063DataItems[ItemIDToIndex("092")].IsPresent = false;
        }


        public string Decode(byte[] Data, string Time)
        {
            // Define output data buffer
            string DataOut;

            // Determine Length of FSPEC fields in bytes
            int FSPEC_Length = ASTERIX.DetermineLenghtOfFSPEC(Data);

            // Determine SIC/SAC Index
            int SIC_Index = 2 + FSPEC_Length;
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
                            I063DataItems[ItemIDToIndex("010")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  010:F";

                        //  015, Service Identification 
                        if (FourFSPECOctets[Bit_Ops.Bit6] == true)
                        {
                            DataOut = DataOut + "  015:T";
                            I063DataItems[ItemIDToIndex("015")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  015:F";

                        //  030, Time of Message
                        if (FourFSPECOctets[Bit_Ops.Bit5] == true)
                        {
                            DataOut = DataOut + "  030:T";
                            I063DataItems[ItemIDToIndex("030")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  030:F";

                        //  050, Sensor Identifier 
                        if (FourFSPECOctets[Bit_Ops.Bit4] == true)
                        {
                            DataOut = DataOut + "  050:T";
                            I063DataItems[ItemIDToIndex("050")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  050:F";

                        //  060, Sensor Configuration and Status 
                        if (FourFSPECOctets[Bit_Ops.Bit3] == true)
                        {
                            DataOut = DataOut + "  060:T";
                            I063DataItems[ItemIDToIndex("060")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  060:F";

                        //  070, Time Stamping Bias 
                        if (FourFSPECOctets[Bit_Ops.Bit2] == true)
                        {
                            DataOut = DataOut + "  070:T";
                            I063DataItems[ItemIDToIndex("070")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  070:F";

                        //  080, SSR / Mode S Range Gain and Bias 
                        if (FourFSPECOctets[Bit_Ops.Bit1] == true)
                        {
                            DataOut = DataOut + "  080:T";
                            I063DataItems[ItemIDToIndex("080")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  080:F";

                        break;

                    case 2:

                        //  081, SSR / Mode S Azimuth Bias 
                        if (FourFSPECOctets[Bit_Ops.Bit15] == true)
                        {
                            DataOut = DataOut + "  081:T";
                            I063DataItems[ItemIDToIndex("081")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  081:F";

                        //  090, PSR Range Gain and Bias 
                        if (FourFSPECOctets[Bit_Ops.Bit14] == true)
                        {
                            DataOut = DataOut + "  090:T";
                            I063DataItems[ItemIDToIndex("090")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  090:F";

                        //  091, PSR Azimuth Bias 
                        if (FourFSPECOctets[Bit_Ops.Bit13] == true)
                        {
                            DataOut = DataOut + "  091:T";
                            I063DataItems[ItemIDToIndex("091")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  091:F";

                        //  092, PSR Elevation Bias  
                        if (FourFSPECOctets[Bit_Ops.Bit12] == true)
                        {
                            DataOut = DataOut + "  092:T";
                            I063DataItems[ItemIDToIndex("092")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  092:F";

                        // Spare
                        if (FourFSPECOctets[Bit_Ops.Bit11] == true)
                            DataOut = DataOut + "  SPR:T";
                        else
                            DataOut = DataOut + "  SPR:F";

                        // RE Reserved Expansion Field
                        if (FourFSPECOctets[Bit_Ops.Bit10] == true)
                            DataOut = DataOut + "  RES:T";
                        else
                            DataOut = DataOut + "  RES:F";

                        // SP Special Purpose Field
                        if (FourFSPECOctets[Bit_Ops.Bit9] == true)
                            DataOut = DataOut + "  SPF:T";
                        else
                            DataOut = DataOut + "  SPF:F";

                        break;

                    // Handle errors
                    default:
                        DataOut = DataOut + "  UKN:T";
                        break;
                }

                CAT63DecodeAndStore.Do(Data);
            }

            // Return decoded data
            return DataOut;
        }
    }
}
