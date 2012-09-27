using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class Bit_Ops
    {
        // Define constant values for bit operations
        //

        // Define a double word
        public BitVector32 DWord = new BitVector32(0);

        // Creates masks to isolate each of the 32 bit flags.
        public static int Bit0 = BitVector32.CreateMask();
        public static int Bit1 = BitVector32.CreateMask(Bit0);
        public static int Bit2 = BitVector32.CreateMask(Bit1);
        public static int Bit3 = BitVector32.CreateMask(Bit2);
        public static int Bit4 = BitVector32.CreateMask(Bit3);
        public static int Bit5 = BitVector32.CreateMask(Bit4);
        public static int Bit6 = BitVector32.CreateMask(Bit5);
        public static int Bit7 = BitVector32.CreateMask(Bit6);
        public static int Bit8 = BitVector32.CreateMask(Bit7);
        public static int Bit9 = BitVector32.CreateMask(Bit8);
        public static int Bit10 = BitVector32.CreateMask(Bit9);
        public static int Bit11 = BitVector32.CreateMask(Bit10);
        public static int Bit12 = BitVector32.CreateMask(Bit11);
        public static int Bit13 = BitVector32.CreateMask(Bit12);
        public static int Bit14 = BitVector32.CreateMask(Bit13);
        public static int Bit15 = BitVector32.CreateMask(Bit14);
        public static int Bit16 = BitVector32.CreateMask(Bit15);
        public static int Bit17 = BitVector32.CreateMask(Bit16);
        public static int Bit18 = BitVector32.CreateMask(Bit17);
        public static int Bit19 = BitVector32.CreateMask(Bit18);
        public static int Bit20 = BitVector32.CreateMask(Bit19);
        public static int Bit21 = BitVector32.CreateMask(Bit20);
        public static int Bit22 = BitVector32.CreateMask(Bit21);
        public static int Bit23 = BitVector32.CreateMask(Bit22);
        public static int Bit24 = BitVector32.CreateMask(Bit23);
        public static int Bit25 = BitVector32.CreateMask(Bit24);
        public static int Bit26 = BitVector32.CreateMask(Bit25);
        public static int Bit27 = BitVector32.CreateMask(Bit26);
        public static int Bit28 = BitVector32.CreateMask(Bit27);
        public static int Bit29 = BitVector32.CreateMask(Bit28);
        public static int Bit30 = BitVector32.CreateMask(Bit29);
        public static int Bit31 = BitVector32.CreateMask(Bit30);

        
        // Define 2 sectors of a double word
        public static BitVector32.Section Bits0_15_Of_DWord = BitVector32.CreateSection(short.MaxValue);
        public static BitVector32.Section Bits16_31_Of_DWord = BitVector32.CreateSection(short.MaxValue, Bits0_15_Of_DWord);

        // Define 4 sectors of a double word
        public static BitVector32.Section Bits0_7_Of_DWord = BitVector32.CreateSection(255);
        public static BitVector32.Section Bits8_15_Of_DWord = BitVector32.CreateSection(255, Bits0_7_Of_DWord);
        public static BitVector32.Section Bits16_23_Of_DWord = BitVector32.CreateSection(255, Bits8_15_Of_DWord);
        public static BitVector32.Section Bits24_31_Of_DWord = BitVector32.CreateSection(255, Bits16_23_Of_DWord);

    }
}
