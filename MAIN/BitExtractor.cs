using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class BitExtractor
    {

        // This routine takes in a range of the bits for 
        // which a mask is required and returns specified mask
        // so it can be used for bit extraction.
        //
        // EndBit............................StartBit
        // 31.......................................0
        public UInt32 Extract(int StartBit, int EndBit, UInt32 From)
        {
            // Declare 32 character mask defaulted '0';
            char[] array = new string('0', 32).ToCharArray();

            // Set the mask bits as specifed by the parameters
            for (int I = (31 - EndBit); I <= (31 - StartBit); I++)
                array[I] = '1';

            // Extract and return the data
            return (Convert.ToUInt32(new string(array), 2) & From);
        }

        // This routine takes in a range of the bits for 
        // which a mask is required and returns specified mask
        // so it can be used for bit extraction.
        //
        // EndBit............................StartBit
        // 64.......................................0
        public UInt64 Extract(int StartBit, int EndBit, UInt64 From)
        {
            // Declare 64 character mask defaulted '0';
            char[] array = new string('0', 64).ToCharArray();

            // Set the mask bits as specifed by the parameters
            for (int I = (63 - EndBit); I <= (63 - StartBit); I++)
                array[I] = '1';

            // Extract and return the data
            return (Convert.ToUInt64(new string(array), 2) & From);
        }

        // This method returns a boolen indicating if the given bit is set
        public bool GetBitStatus(int BitToCheck, UInt64 FromThisNumber)
        {
            //       Get the mask, Check for the state, return the state                                     
            return ((Extract(BitToCheck, BitToCheck, FromThisNumber) & FromThisNumber) == 1);
        }

        // This method joins two UInt32 into UInt64
        public UInt64 JoinTwoUInt32(UInt32 FirstPart, UInt32 SecondPart)
        {
            // Convert both numbers into 32 char strings, join them together into 62 char string and then convert into UInt64
            return Convert.ToUInt64((GetTo32Char(Convert.ToString(FirstPart, 2)) + GetTo32Char(Convert.ToString(SecondPart, 2))), 2);
        }

        // This method takes in string representation of a 32
        // bit number and appends 0 to make it exactly 32. Intended to
        // be used in conjuction with JoinTo32s
        public string GetTo32Char(string In)
        {
            return In.PadLeft(32, '0');
        }

        // This method takes in string representation of a 32
        // bit number and appends 0 to make it exactly 32. Intended to
        // be used in conjuction with JoinTo32s
        public string GetTo64Char(string In)
        {
            return In.PadLeft(64, '0');
        }

        public static int GetIntFromThreeBits(bool Bit1, bool Bit2, bool Bit3)
        {
            int result = 0;

            if (Bit1 == true)
                result = 1;

            if (Bit2 == true)
                result = result + 2;

            if (Bit3 == true)
                result = result + 4;

            return result;
        }
    }
}
