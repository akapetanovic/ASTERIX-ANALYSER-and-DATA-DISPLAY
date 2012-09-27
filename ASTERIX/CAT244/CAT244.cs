using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT244
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////
        // ASTERIX for SMART-SDG: ASTERIX 244 for Transmission of Reference Trajectory State Vectors 
        /////////////////////////////////////////////////////////////////////////////////////////////////
        //
        // 1 I244/010 Trajectory Identifier             2
        // 2 I244/020 Time of Day                       3
        // 3 I244/030 Lat/Long Position                 8
        // 4 I244/040 Geometric Altitude                2
        // 5 I244/045 Flight Level                      2
        // 6 I244/050 Ground Speed                      2
        // 7 I244/055 Air Speed                         2
        // FX - Field extension indicator -
        //
        // 8 I244/60 Course                             2
        // 9 I244/65 Magnetic Heading                   2
        // 10 I244/70 Geometric Vertical Rate           2
        // 11 I244/75 Barometric Vertical Rate          2
        // 12 I244/80 Ground Accelerations              6
        // 13 I244/95 Rate Of Turn                      1
        // 14 I244/100 Projected Profile                1+N*11
        // FX - Field extension indicator -
        //
        // 15 I244/115 Selected Flight Level            2
        // 16 I244/120 ICAO 24-bit Address              3
        // 17 I244/130 Mode-3/A Code                    2
        // 18 I244/140 Target Identification            6
        // 19 I244/150 Aircraft Type                    4
        // 20 I244/160 ADS_B Emitter Category           1
        // 21 I244/170 Target Status                    1
        // FX - Field extension indicator -
        //
        // 22 I244/180 Accuracy/Integrity               2
        // 23 I244/190 Link Status                      1
        // 24 - Spare -                                 x
        // 25 - Spare -                                 x
        // 26 - Spare -                                 x
        // 27 RE Reserved Expansion Field               1+
        // 28 SP Special Purpose Field                  1+
        // FX - Field extension indicator -
        /// <summary>
        /// /////////////////////////////////////////////////////
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>


        public static void Intitialize()
        {

        }

        // Do not bother to decode CAT244, not really important for us. Just determine that 
        // it is present for now.
        public string Decode(byte[] Data, string Time)
        {
            // Define output data buffer
            string DataOut;

            // Determine Length of FSPEC fields in bytes
            int FSPEC_Length = ASTERIX.DetermineLenghtOfFSPEC(Data);

            // Determine SIC/SAC Index
            // No SIC/SAC for CAT244

            // Extract SIC/SAC Indexes.
            DataOut = "N/A" + '/' + "N/A";

            // Creates and initializes a BitVector32 with all bit flags set to FALSE.
            BitVector32 FourFSPECOctets = ASTERIX.GetFourFSPECOctets(Data);

            // Return decoded data
            return DataOut;
        }
    }
}
