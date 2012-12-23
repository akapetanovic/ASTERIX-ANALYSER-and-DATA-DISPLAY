using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT48I240UserData
    {   
        private static string Decode6BitASCII(int ASCII_Code)
        {
          
            
            string CharOut = " ";
            switch (ASCII_Code)
            {   //////////////////////////////
                // Handle numbers 0 .. 9
                //////////////////////////////
                case 48:
                    CharOut = "0";
                    break;
                case 49:
                    CharOut = "1";
                    break;
                case 50:
                    CharOut = "2";
                    break;
                case 51:
                    CharOut = "3";
                    break;
                case 52:
                    CharOut = "4";
                    break;
                case 53:
                    CharOut = "5";
                    break;
                case 54:
                    CharOut = "6";
                    break;
                case 55:
                    CharOut = "7";
                    break;
                case 56:
                    CharOut = "8";
                    break;
                case 57:
                    CharOut = "9";
                    break;
                //////////////////////////////
                // Handle letters
                //////////////////////////////
                case 1:
                    CharOut = "A";
                    break;
                case 2:
                    CharOut = "B";
                    break;
                case 3:
                    CharOut = "C";
                    break;
                case 4:
                    CharOut = "D";
                    break;
                case 5:
                    CharOut = "E";
                    break;
                case 6:
                    CharOut = "F";
                    break;
                case 7:
                    CharOut = "G";
                    break;
                case 8:
                    CharOut = "H";
                    break;
                case 9:
                    CharOut = "I";
                    break;
                case 10:
                    CharOut = "J";
                    break;
                case 11:
                    CharOut = "K";
                    break;
                case 12:
                    CharOut = "L";
                    break;
                case 13:
                    CharOut = "M";
                    break;
                case 14:
                    CharOut = "N";
                    break;
                case 15:
                    CharOut = "O";
                    break;
                case 16:
                    CharOut = "P";
                    break;
                case 17:
                    CharOut = "Q";
                    break;
                case 18:
                    CharOut = "R";
                    break;
                case 19:
                    CharOut = "S";
                    break;
                case 20:
                    CharOut = "T";
                    break;
                case 21:
                    CharOut = "U";
                    break;
                case 22:
                    CharOut = "V";
                    break;
                case 23:
                    CharOut = "W";
                    break;
                case 24:
                    CharOut = "X";
                    break;
                case 25:
                    CharOut = "Y";
                    break;
                case 26:
                    CharOut = "Z";
                    break;
                //////////////////////////////
                // Handle special characters
                //////////////////////////////
                case 32:
                    CharOut = " ";
                    break;
            }

           return CharOut;
        }

        public static void DecodeCAT48I240(byte[] Data)
        {

            // A new instance of the CAT48I240 data
            CAT48I240Types.CAT48I240ACID_Data MyCAT48I240 = new CAT48I240Types.CAT48I240ACID_Data();

            // Get all 6 octets
            Bit_Ops Bits_1_To_Bits_32_ = new Bit_Ops();
            Bit_Ops Bits_33_To_Bits_48_ = new Bit_Ops();

            Bits_1_To_Bits_32_.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 6];
            Bits_1_To_Bits_32_.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 5];
            Bits_1_To_Bits_32_.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 4];
            Bits_1_To_Bits_32_.DWord[Bit_Ops.Bits24_31_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 3];

            Bits_33_To_Bits_48_.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 2];
            Bits_33_To_Bits_48_.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[CAT48.CurrentDataBufferOctalIndex + 1];


            Bit_Ops Char1 = new Bit_Ops();
            Char1.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;
            Bit_Ops Char2 = new Bit_Ops();
            Char2.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;
            Bit_Ops Char3 = new Bit_Ops();
            Char3.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;
            Bit_Ops Char4 = new Bit_Ops();
            Char4.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;
            Bit_Ops Char5 = new Bit_Ops();
            Char5.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;
            Bit_Ops Char6 = new Bit_Ops();
            Char6.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;
            Bit_Ops Char7 = new Bit_Ops();
            Char7.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;
            Bit_Ops Char8 = new Bit_Ops();
            Char8.DWord[Bit_Ops.Bits0_7_Of_DWord] = 0;

            /////////////////////////////////////////
            // Decode character 1
            Char1.DWord[Bit_Ops.Bit5] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit15];
            Char1.DWord[Bit_Ops.Bit4] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit14];
            Char1.DWord[Bit_Ops.Bit3] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit13];
            Char1.DWord[Bit_Ops.Bit2] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit12];
            Char1.DWord[Bit_Ops.Bit1] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit11];
            Char1.DWord[Bit_Ops.Bit0] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit10];

            Char2.DWord[Bit_Ops.Bit5] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit9];
            Char2.DWord[Bit_Ops.Bit4] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit8];
            Char2.DWord[Bit_Ops.Bit3] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit7];
            Char2.DWord[Bit_Ops.Bit2] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit6];
            Char2.DWord[Bit_Ops.Bit1] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit5];
            Char2.DWord[Bit_Ops.Bit0] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit4];

            Char3.DWord[Bit_Ops.Bit5] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit3];
            Char3.DWord[Bit_Ops.Bit4] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit2];
            Char3.DWord[Bit_Ops.Bit3] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit1];
            Char3.DWord[Bit_Ops.Bit2] = Bits_33_To_Bits_48_.DWord[Bit_Ops.Bit0];
            Char3.DWord[Bit_Ops.Bit1] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit31];
            Char3.DWord[Bit_Ops.Bit0] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit30];

            Char4.DWord[Bit_Ops.Bit5] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit29];
            Char4.DWord[Bit_Ops.Bit4] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit28];
            Char4.DWord[Bit_Ops.Bit3] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit27];
            Char4.DWord[Bit_Ops.Bit2] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit26];
            Char4.DWord[Bit_Ops.Bit1] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit25];
            Char4.DWord[Bit_Ops.Bit0] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit24];

            Char5.DWord[Bit_Ops.Bit5] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit23];
            Char5.DWord[Bit_Ops.Bit4] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit22];
            Char5.DWord[Bit_Ops.Bit3] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit21];
            Char5.DWord[Bit_Ops.Bit2] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit20];
            Char5.DWord[Bit_Ops.Bit1] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit19];
            Char5.DWord[Bit_Ops.Bit0] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit18];

            Char6.DWord[Bit_Ops.Bit5] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit17];
            Char6.DWord[Bit_Ops.Bit4] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit16];
            Char6.DWord[Bit_Ops.Bit3] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit15];
            Char6.DWord[Bit_Ops.Bit2] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit14];
            Char6.DWord[Bit_Ops.Bit1] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit13];
            Char6.DWord[Bit_Ops.Bit0] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit12];

            Char7.DWord[Bit_Ops.Bit5] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit11];
            Char7.DWord[Bit_Ops.Bit4] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit10];
            Char7.DWord[Bit_Ops.Bit3] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit9];
            Char7.DWord[Bit_Ops.Bit2] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit8];
            Char7.DWord[Bit_Ops.Bit1] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit7];
            Char7.DWord[Bit_Ops.Bit0] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit6];

            Char8.DWord[Bit_Ops.Bit5] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit5];
            Char8.DWord[Bit_Ops.Bit4] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit4];
            Char8.DWord[Bit_Ops.Bit3] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit3];
            Char8.DWord[Bit_Ops.Bit2] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit2];
            Char8.DWord[Bit_Ops.Bit1] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit1];
            Char8.DWord[Bit_Ops.Bit0] = Bits_1_To_Bits_32_.DWord[Bit_Ops.Bit0];

            MyCAT48I240.ACID = Decode6BitASCII(Char1.DWord[Bit_Ops.Bits0_7_Of_DWord]) +
                Decode6BitASCII(Char2.DWord[Bit_Ops.Bits0_7_Of_DWord]) +
                Decode6BitASCII(Char3.DWord[Bit_Ops.Bits0_7_Of_DWord]) +
                Decode6BitASCII(Char4.DWord[Bit_Ops.Bits0_7_Of_DWord]) +
                Decode6BitASCII(Char5.DWord[Bit_Ops.Bits0_7_Of_DWord]) +
                Decode6BitASCII(Char6.DWord[Bit_Ops.Bits0_7_Of_DWord]) +
                Decode6BitASCII(Char7.DWord[Bit_Ops.Bits0_7_Of_DWord]) +
                Decode6BitASCII(Char8.DWord[Bit_Ops.Bits0_7_Of_DWord]);

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT48.I048DataItems[CAT48.ItemIDToIndex("240")].value = MyCAT48I240;
            //////////////////////////////////////////////////////////////////////////////////

            // Increase data buffer index so it ready for the next data item.
            CAT48.CurrentDataBufferOctalIndex = CAT48.CurrentDataBufferOctalIndex + 6;
        }
    }
}
