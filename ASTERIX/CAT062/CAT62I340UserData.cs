using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I340UserData
    {
        public static void DecodeCAT62I340(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops WORD0 = new Bit_Ops();

            //Extract the first octet
            WORD0.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT62.CurrentDataBufferOctalIndex];

            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            
            // 2 SIC/SAC
            if (WORD0.DWord[CAT62I340Types.Sensor_Identification] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

            if (WORD0.DWord[CAT62I340Types.Measured_Position] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 4;
            }

            if (WORD0.DWord[CAT62I340Types.Measured_3_D_Height] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }

            if (WORD0.DWord[CAT62I340Types.Last_Measured_Mode_C_Code] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }

            if (WORD0.DWord[CAT62I340Types.Last_Measured_Mode_3_A_Code] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 2;
            }

            if (WORD0.DWord[CAT62I340Types.Report_Type] == true)
            {
                CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 1;
            }

        }
    }
}
