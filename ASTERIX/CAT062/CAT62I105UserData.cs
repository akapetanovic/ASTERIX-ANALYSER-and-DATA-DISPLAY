using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticastingUDP
{
    class CAT62I105UserData
    {

        /////////////////////////////////////////////////////////////////
        /// Define lat/long fix point values from LSB to MSB
        /////////////////////////////////////////////////////////////////
        
       // private const double POS_FIX_1 = 180.0 / Math.Pow(2.0, 25.0);

        private const double POS_FIX_1 = 180.0 / 33554432.0;
        private const double POS_FIX_2 = POS_FIX_1 * 2.0;
        private const double POS_FIX_3 = POS_FIX_2 * 2.0;
        private const double POS_FIX_4 = POS_FIX_3 * 2.0;
        private const double POS_FIX_5 = POS_FIX_4 * 2.0;
        private const double POS_FIX_6 = POS_FIX_5 * 2.0;
        private const double POS_FIX_7 = POS_FIX_6 * 2.0;
        private const double POS_FIX_8 = POS_FIX_7 * 2.0;
        private const double POS_FIX_9 = POS_FIX_8 * 2.0;
        private const double POS_FIX_10 = POS_FIX_9 * 2.0;
        private const double POS_FIX_11 = POS_FIX_10 * 2.0;
        private const double POS_FIX_12 = POS_FIX_11 * 2.0;
        private const double POS_FIX_13 = POS_FIX_12 * 2.0;
        private const double POS_FIX_14 = POS_FIX_13 * 2.0;
        private const double POS_FIX_15 = POS_FIX_14 * 2.0;
        private const double POS_FIX_16 = POS_FIX_15 * 2.0;

        private const double POS_FIX_17 = POS_FIX_16 * 2.0;
        private const double POS_FIX_18 = POS_FIX_17 * 2.0;
        private const double POS_FIX_19 = POS_FIX_18 * 2.0;
        private const double POS_FIX_20 = POS_FIX_19 * 2.0;
        private const double POS_FIX_21 = POS_FIX_20 * 2.0;
        private const double POS_FIX_22 = POS_FIX_21 * 2.0;
        private const double POS_FIX_23 = POS_FIX_21 * 2.0;
        private const double POS_FIX_24 = POS_FIX_23 * 2.0;
        // 45
        private const double POS_FIX_25 = POS_FIX_24 * 2.0;
        private const double POS_FIX_26 = POS_FIX_25 * 2.0;
        // 180
        private const double POS_FIX_27 = POS_FIX_26 * 2.0;
        private const double POS_FIX_28 = POS_FIX_27 * 2.0;
        private const double POS_FIX_29 = POS_FIX_28 * 2.0;
        private const double POS_FIX_30 = POS_FIX_29 * 2.0;
        private const double POS_FIX_31 = POS_FIX_30 * 2.0;
        
        public static void DecodeCAT62I105(byte[] Data)
        {
            // Increase data buffer index so it ready for the next data item.
            CAT62.CurrentDataBufferOctalIndex = CAT62.CurrentDataBufferOctalIndex + 8;
        }
    }
}
