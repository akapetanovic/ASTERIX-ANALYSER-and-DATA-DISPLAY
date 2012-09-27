using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT02I000Types
    {

        // I002/000 Message Type                        1
        //
        // 001, North marker message;
        // 002, Sector crossing message;
        // 003, South marker message;
        // 008, Activation of blind zone filtering;
        // 009, Stop of blind zone filtering.
        public enum Message_Type { Unknown_Data, North_Marker_Msg, Sector_Crossing_Msg, South_Marker_Msg, Activation_Of_Blind_Zone_Filtering, Stop_Of_Blind_Zone_Filtering };

    }
}
