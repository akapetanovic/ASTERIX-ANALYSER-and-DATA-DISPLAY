using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    // This class is the main data storage for each message type
    // If the user selects an option to store the data, then each 
    // individual message category will store the latest received 
    // message into this storage area. 
    //
    // This is nothig more than a central place for all data to be
    // stored.
    class MainASTERIXDataStorage
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // CAT 01 Messages
        //
        // Define collection for CAT01 data items. This is where each data item will be stored 
        public class CAT01Data
        {
            public System.Collections.Generic.List<CAT01.CAT01DataItem> CAT01DataItems = new System.Collections.Generic.List<CAT01.CAT01DataItem>();
        }

        // This is the main storage of all CAT01 Messages
        public static System.Collections.Generic.List<CAT01Data> CAT01Message = new System.Collections.Generic.List<CAT01Data>();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // CAT 02 Messages
        //
        // Define collection for CAT02 data items. This is where each data item will be stored 
        public class CAT02Data
        {
            public System.Collections.Generic.List<CAT02.CAT02DataItem> CAT02DataItems = new System.Collections.Generic.List<CAT02.CAT02DataItem>();
        }

        // This is the main storage of all CAT02 Messages
        public static System.Collections.Generic.List<CAT02Data> CAT02Message = new System.Collections.Generic.List<CAT02Data>();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // CAT 48 Messages
        //
        // Define collection for CAT48 data items. This is where each data item will be stored 
        public class CAT48Data
        {
            public System.Collections.Generic.List<CAT48.CAT48DataItem> CAT48DataItems = new System.Collections.Generic.List<CAT48.CAT48DataItem>();
        }

        // This is the main storage of all CAT48 Messages
        public static System.Collections.Generic.List<CAT48Data> CAT48Message = new System.Collections.Generic.List<CAT48Data>();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // CAT 34 Messages
        //
        // Define collection for CAT34 data items. This is where each data item will be stored 
        public class CAT34Data
        {
            public System.Collections.Generic.List<CAT34.CAT34DataItem> CAT34DataItems = new System.Collections.Generic.List<CAT34.CAT34DataItem>();
        }

        // This is the main storage of all CAT48 Messages
        public static System.Collections.Generic.List<CAT34Data> CAT34Message = new System.Collections.Generic.List<CAT34Data>();
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // CAT 62 Messages
        //
        // Define collection for CAT62 data items. This is where each data item will be stored 
        public class CAT62Data
        {
            public System.Collections.Generic.List<CAT62.CAT062DataItem> CAT62DataItems = new System.Collections.Generic.List<CAT62.CAT062DataItem>();
        }

        // This is the main storage of all CAT62 Messages
        public static System.Collections.Generic.List<CAT62Data> CAT62Message = new System.Collections.Generic.List<CAT62Data>();

        // Define a method to reset all data buffers. The method is to used when switching beetween data sources.
        public static void ResetAllData()
        {
            CAT01Message.Clear();
            CAT02Message.Clear();
            CAT48Message.Clear();
            CAT34Message.Clear();
            CAT62Message.Clear();
        }

    }
}
