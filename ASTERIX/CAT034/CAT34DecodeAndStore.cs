using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsterixDisplayAnalyser;

namespace AsterixDisplayAnalyser
{
    class CAT34DecodeAndStore
    {
        // This method will accept a buffer of data with the assumption that 
        // category has been determined. It will then decode the data and save 
        // it in the shared buffer. Everry time a message is passed in the data 
        // will be appended to the buffer. This means that each line will contain 
        // data for one message. For data items which are not in the message,
        // indicated by the FSPEC field, N/A will be inserted instead. The shared 
        // buffer is loacted in the SharedData and will not be saved. It is responsibility
        // of the user to save the data in a file it desired.
        public static void Do(byte[] Data)
        {
            /////////////////////////////////////////////////////////////////////////
            //
            // Next version of Category 002: PSR Radar, M-SSR Radar, Mode-S Station
            //                                               Length in bytes
            //
            // 1 I034/010 Data Source Identifier                2 
            Decode034_010.Decode(Data);
            // 2 I034/000 Message Type                          1
            Decode034_000.Decode(Data);
            // 3 I034/030 Time-of-Day                           3
            Decode034_030.Decode(Data);
            // 4 I034/020 Sector Number                         1
            Decode034_020.Decode(Data);
            // 5 I034/041 Antenna Rotation Period               2
            Decode034_041.Decode(Data);
            // 6 I034/050 System Configuration and Status       1+
            Decode034_050.Decode(Data);
            // 7 I034/060 System Processing Mode                1+
            Decode034_060.Decode(Data);
            // FX

            // 8 I034/070 Message Count Values                  1+2N
            Decode034_070.Decode(Data);
            // 9 I034/100 Generic Polar Window                  8
            Decode034_100.Decode(Data);
            // 10 I034/110 Data Filter                          1
            Decode034_110.Decode(Data);
            // 11 I034/120 3D-Position of Data Source           8
            Decode034_120.Decode(Data);
            // 12 I034/090 Collimation Error                    2
            Decode034_090.Decode(Data);
            // 13 RE-Data Item Reserved Expansion Field         1+1
            
            // 14 SP-Data Item Special Purpose Field            1+1
            
            // FX

           
        }
      
    }
}
