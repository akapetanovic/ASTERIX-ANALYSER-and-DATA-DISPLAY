using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace MulticastingUDP
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


        // Define a class that holds I062 data
        public class I062DataItem
        {
            public string ID;
            public string Description;
            public bool IsPresent;
            public object value;

            // Constructor to set default
            // values.
            public I062DataItem()
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
                default:
                    break;
            }

            return index;
        }

        // Define collection of CAT062 data items. Used to store and retrieve basic data such as:
        // 
        public static System.Collections.Generic.List<I062DataItem> I062DataItems = new System.Collections.Generic.List<I062DataItem>();
        // 1. Item presence
        // 2. Item description
        //
        // Based on the data item identifer


        public static void Intitialize()
        {
            I062DataItems.Clear();

            // I062/010 Data Source Identifier 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("010")].ID = "010";
            I062DataItems[ItemIDToIndex("010")].Description = "Data Source Identifier";
            I062DataItems[ItemIDToIndex("010")].IsPresent = false;              
            // I062/015 Service Identification 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("015")].ID = "015";
            I062DataItems[ItemIDToIndex("015")].Description = "Service Identification";
            I062DataItems[ItemIDToIndex("015")].IsPresent = false;              
            // I062/070 Time Of Track Information
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("070")].ID = "070";
            I062DataItems[ItemIDToIndex("070")].Description = "Time Of Track Information";
            I062DataItems[ItemIDToIndex("070")].IsPresent = false;            
            // I062/105 Calculated Track Position (WGS-84)
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("105")].ID = "105";
            I062DataItems[ItemIDToIndex("105")].Description = "Calculated Track Position (WGS-84)";
            I062DataItems[ItemIDToIndex("105")].IsPresent = false;   
            // I062/100 Calculated Track Position (Cartesian)
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("100")].ID = "100";
            I062DataItems[ItemIDToIndex("100")].Description = "Data Source Identifier";
            I062DataItems[ItemIDToIndex("100")].IsPresent = false;
            // I062/185 Calculated Track Velocity (Cartesian) 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("185")].ID = "185";
            I062DataItems[ItemIDToIndex("185")].Description = "Calculated Track Velocity (Cartesian)";
            I062DataItems[ItemIDToIndex("185")].IsPresent = false;
            // I062/210 Calculated Acceleration (Cartesian)
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("210")].ID = "210";
            I062DataItems[ItemIDToIndex("210")].Description = "Calculated Acceleration (Cartesian)";
            I062DataItems[ItemIDToIndex("210")].IsPresent = false;  
            // I062/060 Track Mode 3/A Code
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("060")].ID = "060";
            I062DataItems[ItemIDToIndex("060")].Description = " Track Mode 3/A Code";
            I062DataItems[ItemIDToIndex("060")].IsPresent = false;                  
            // I062/245 Target Identification 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("245")].ID = "245";
            I062DataItems[ItemIDToIndex("245")].Description = "Target Identification";
            I062DataItems[ItemIDToIndex("245")].IsPresent = false;              
            // I062/380 Aircraft Derived Data
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("380")].ID = "380";
            I062DataItems[ItemIDToIndex("380")].Description = "Aircraft Derived Data";
            I062DataItems[ItemIDToIndex("380")].IsPresent = false;               
            // I062/040 Track Number
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("040")].ID = "040";
            I062DataItems[ItemIDToIndex("040")].Description = "Track Number";
            I062DataItems[ItemIDToIndex("040")].IsPresent = false;                        
            // I062/080 Track Status
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("080")].ID = "080";
            I062DataItems[ItemIDToIndex("080")].Description = "Track Status";
            I062DataItems[ItemIDToIndex("080")].IsPresent = false;                        
            // I062/290 System Track Update Ages 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("290")].ID = "290";
            I062DataItems[ItemIDToIndex("290")].Description = "System Track Update Ages";
            I062DataItems[ItemIDToIndex("290")].IsPresent = false;           
            // I062/200 Mode of Movement 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("200")].ID = "200";
            I062DataItems[ItemIDToIndex("200")].Description = "Mode of Movement";
            I062DataItems[ItemIDToIndex("200")].IsPresent = false;                   
            // I062/295 Track Data Ages 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("295")].ID = "295";
            I062DataItems[ItemIDToIndex("295")].Description = "Track Data Ages";
            I062DataItems[ItemIDToIndex("295")].IsPresent = false;                    
            // I062/136 Measured Flight Level  
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("136")].ID = "136";
            I062DataItems[ItemIDToIndex("136")].Description = "Measured Flight Level";
            I062DataItems[ItemIDToIndex("136")].IsPresent = false;             
            // I062/130 Calculated Track Geometric Altitude 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("130")].ID = "130";
            I062DataItems[ItemIDToIndex("130")].Description = "Calculated Track Geometric Altitude";
            I062DataItems[ItemIDToIndex("130")].IsPresent = false;
            // I062/135 Calculated Track Barometric Altitude 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("135")].ID = "135";
            I062DataItems[ItemIDToIndex("135")].Description = "Calculated Track Barometric Altitude ";
            I062DataItems[ItemIDToIndex("135")].IsPresent = false;
            // I062/220 Calculated Rate Of Climb/Descent
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("220")].ID = "220";
            I062DataItems[ItemIDToIndex("220")].Description = "Calculated Rate Of Climb/Descent";
            I062DataItems[ItemIDToIndex("220")].IsPresent = false;    
            // I062/390 Flight Plan Related Data 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("390")].ID = "390";
            I062DataItems[ItemIDToIndex("390")].Description = "Flight Plan Related Data";
            I062DataItems[ItemIDToIndex("390")].IsPresent = false;           
            // I062/270 Target Size & Orientation 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("270")].ID = "270";
            I062DataItems[ItemIDToIndex("270")].Description = "Target Size & Orientation";
            I062DataItems[ItemIDToIndex("270")].IsPresent = false;          
            // I062/300 Vehicle Fleet Identification 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("300")].ID = "300";
            I062DataItems[ItemIDToIndex("300")].Description = "Vehicle Fleet Identification";
            I062DataItems[ItemIDToIndex("300")].IsPresent = false;       
            // I062/110 Mode 5 Data reports & Extended Mode 1 Code  
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("110")].ID = "110";
            I062DataItems[ItemIDToIndex("110")].Description = "Mode 5 Data reports & Extended Mode 1 Code";
            I062DataItems[ItemIDToIndex("110")].IsPresent = false;
            // I062/120 Track Mode 2 Code 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("120")].ID = "120";
            I062DataItems[ItemIDToIndex("120")].Description = "Track Mode 2 Code";
            I062DataItems[ItemIDToIndex("120")].IsPresent = false;                  
            // I062/510 Composed Track Number 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("510")].ID = "510";
            I062DataItems[ItemIDToIndex("510")].Description = "Composed Track Number";
            I062DataItems[ItemIDToIndex("510")].IsPresent = false;              
            // I062/500 Estimated Accuracies 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("500")].ID = "500";
            I062DataItems[ItemIDToIndex("500")].Description = "Estimated Accuracies";
            I062DataItems[ItemIDToIndex("500")].IsPresent = false;               
            // I062/340 Measured Information 
            I062DataItems.Add(new I062DataItem());
            I062DataItems[ItemIDToIndex("340")].ID = "340";
            I062DataItems[ItemIDToIndex("340")].Description = "Measured Information";
            I062DataItems[ItemIDToIndex("340")].IsPresent = false;               

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
                            I062DataItems[ItemIDToIndex("010")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  010:F";

                        // Spare bit
                        if (FourFSPECOctets[Bit_Ops.Bit6] == true)
                            DataOut = DataOut + "  SPR:T";
                        else
                            DataOut = DataOut + "  SPR:F";

                        // 015 Service Identification
                        if (FourFSPECOctets[Bit_Ops.Bit5] == true)
                        {
                            DataOut = DataOut + "  015:T";
                            I062DataItems[ItemIDToIndex("015")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  015:F";

                        // 070 Time Of Track Information
                        if (FourFSPECOctets[Bit_Ops.Bit4] == true)
                        {
                            DataOut = DataOut + "  070:T";
                            I062DataItems[ItemIDToIndex("070")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  070:F";

                        // 105 Calculated Track Position (WGS-84)   
                        if (FourFSPECOctets[Bit_Ops.Bit3] == true)
                        {
                            DataOut = DataOut + "  105:T";
                            I062DataItems[ItemIDToIndex("105")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  105:F";

                        // 100 Calculated Track Position (Cartesian)
                        if (FourFSPECOctets[Bit_Ops.Bit2] == true)
                        {
                            DataOut = DataOut + "  100:T";
                            I062DataItems[ItemIDToIndex("100")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  100:F";

                        // 185 Calculated Track Velocity (Cartesian)
                        if (FourFSPECOctets[Bit_Ops.Bit1] == true)
                        {
                            DataOut = DataOut + "  185:T";
                            I062DataItems[ItemIDToIndex("185")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  185:F";

                        break;

                    case 2:

                        // 210 Calculated Acceleration (Cartesian)
                        if (FourFSPECOctets[Bit_Ops.Bit15] == true)
                        {
                            DataOut = DataOut + "  210:T";
                            I062DataItems[ItemIDToIndex("210")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  210:F";

                        // 060 Track Mode 3/A Code
                        if (FourFSPECOctets[Bit_Ops.Bit14] == true)
                        {
                            DataOut = DataOut + "  060:T";
                            I062DataItems[ItemIDToIndex("060")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  060:F";

                        // 245 Target Identification 
                        if (FourFSPECOctets[Bit_Ops.Bit13] == true)
                        {
                            DataOut = DataOut + "  245:T";
                            I062DataItems[ItemIDToIndex("245")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  245:F";

                        // 380 Aircraft Derived Data
                        if (FourFSPECOctets[Bit_Ops.Bit12] == true)
                        {
                            DataOut = DataOut + "  380:T";
                            I062DataItems[ItemIDToIndex("380")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  380:F";

                        // 040 Track Number
                        if (FourFSPECOctets[Bit_Ops.Bit11] == true)
                        {
                            DataOut = DataOut + "  040:T";
                            I062DataItems[ItemIDToIndex("040")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  040:F";

                        // 080 Track Status 
                        if (FourFSPECOctets[Bit_Ops.Bit10] == true)
                        {
                            DataOut = DataOut + "  080:T";
                            I062DataItems[ItemIDToIndex("080")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  080:F";

                        // 290 System Track Update Ages
                        if (FourFSPECOctets[Bit_Ops.Bit9] == true)
                        {
                            DataOut = DataOut + "  290:T";
                            I062DataItems[ItemIDToIndex("290")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  290:F";

                        break;

                    case 3:

                        // 200 Mode of Movement 
                        if (FourFSPECOctets[Bit_Ops.Bit23] == true)
                        {
                            DataOut = DataOut + "  200:T";
                            I062DataItems[ItemIDToIndex("200")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  200:F";

                        // 295 Track Data Ages
                        if (FourFSPECOctets[Bit_Ops.Bit22] == true)
                        {
                            DataOut = DataOut + "  295:T";
                            I062DataItems[ItemIDToIndex("295")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  295:F";

                        // 136 Measured Flight Level 
                        if (FourFSPECOctets[Bit_Ops.Bit21] == true)
                        {
                            DataOut = DataOut + "  136:T";
                            I062DataItems[ItemIDToIndex("136")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  136:F";

                        // 130 Calculated Track Geometric Altitude 
                        if (FourFSPECOctets[Bit_Ops.Bit20] == true)
                        {
                            DataOut = DataOut + "  130:T";
                            I062DataItems[ItemIDToIndex("130")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  130:F";

                        // 135 Calculated Track Barometric Altitude
                        if (FourFSPECOctets[Bit_Ops.Bit19] == true)
                        {
                            DataOut = DataOut + "  135:T";
                            I062DataItems[ItemIDToIndex("135")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  135:F";

                        // 220 Calculated Rate Of Climb/Descent
                        if (FourFSPECOctets[Bit_Ops.Bit18] == true)
                        {
                            DataOut = DataOut + "  220:T";
                            I062DataItems[ItemIDToIndex("220")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  220:F";

                        // 390 Flight Plan Related Data  
                        if (FourFSPECOctets[Bit_Ops.Bit17] == true)
                        {
                            DataOut = DataOut + "  390:T";
                            I062DataItems[ItemIDToIndex("390")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  390:F";

                        break;


                    case 4:

                        // 270 Target Size & Orientation
                        if (FourFSPECOctets[Bit_Ops.Bit31] == true)
                        {
                            DataOut = DataOut + "  270:T";
                            I062DataItems[ItemIDToIndex("270")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  270:F";

                        // 300 Vehicle Fleet Identification
                        if (FourFSPECOctets[Bit_Ops.Bit30] == true)
                        {
                            DataOut = DataOut + "  300:T";
                            I062DataItems[ItemIDToIndex("300")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  300:F";

                        // 110 Mode 5 Data reports & Extended Mode 1 Code
                        if (FourFSPECOctets[Bit_Ops.Bit29] == true)
                        {
                            DataOut = DataOut + "  110:T";
                            I062DataItems[ItemIDToIndex("110")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  110:F";

                        // 120 Track Mode 2 Code
                        if (FourFSPECOctets[Bit_Ops.Bit28] == true)
                        {
                            DataOut = DataOut + "  120:T";
                            I062DataItems[ItemIDToIndex("120")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  120:F";

                        // 510 Composed Track Number
                        if (FourFSPECOctets[Bit_Ops.Bit27] == true)
                        {
                            DataOut = DataOut + "  510:T";
                            I062DataItems[ItemIDToIndex("510")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  510:F";

                        // 500 Estimated Accuracies 
                        if (FourFSPECOctets[Bit_Ops.Bit26] == true)
                        {
                            DataOut = DataOut + "  500:T";
                            I062DataItems[ItemIDToIndex("500")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  500:F";

                        // 340 Measured Information  
                        if (FourFSPECOctets[Bit_Ops.Bit25] == true)
                        {
                            DataOut = DataOut + "  340:T";
                            I062DataItems[ItemIDToIndex("340")].IsPresent = true;
                        }
                        else
                            DataOut = DataOut + "  340:F";

                        break;


                    // Handle errors
                    default:
                        DataOut = DataOut + "  UKN:T";
                        break;
                }

                // Now check if the user requested data to be decoded
                if (SharedData.Decode_CAT062 == true)
                {
                    CAT62DecodeAndStore.Do(Data);
                }

            }

            // Return decoded data
            return DataOut;
        }
    }
}
