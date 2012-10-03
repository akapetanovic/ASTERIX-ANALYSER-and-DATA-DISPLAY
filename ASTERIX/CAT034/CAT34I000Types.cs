using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT34I000Types
    {

        // I002/000 Message Type                        1
        //
        // 001 North Marker message
        // 002 Sector crossing message
        // 003 Geographical filtering message
        // 004 Jamming Strobe message
        public enum Message_Type { Unknown_Data, North_Marker_Msg, Sector_Crossing_Msg, Geographical_Filtering_Msg,  Jamming_Strobe_Msg };

    }
}
