using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MulticastingUDP
{
    public static class SharedData
    {
        
        // Define type for ASTERIX categories supported.
        public enum Supported_Asterix_CAT_Type { Undefined, CAT001, CAT002, CAT008, CAT034, CAT048, CAT062, CAT063, CAT065, CAT244 };

        // This boolean is just a flag which indicates 
        // if data is to be collected. It is set ON/OFF from
        // the user interface.
        public static bool bool_Listen_for_Data = false;

        // This list box stores received data while the listener is
        // running. Once the data has been received the data is to be 
        // decoded and stored in appropriate format
        public static ListBox DataBox = new ListBox();

        // Used to convert feet to meeters
        public  static double FeetToMeeters = 0.3048;

        /////////////////////////////////////////////////////////////////////////
        // This section contains data buffers for decoded data, for each
        // category. Once the data is stored, it is up to the user to save
        // decoded data in a file for later analasis.
        //
        // The data is stored in the following format:
        //
        // Line 1: CATXXX 
        // Line 2: DIXXX DIXXX 
        // Line 2: ----------------------------------
        // Line 3: DIXXX:Data DIXXX:Data etc
        // 
        // Example:  CAT001 
        //           010 020 ...
        //           ------------------
        //           8/34 PRI ...
        ///////////////////////////////////////////////////////////////////////////
        public static StringBuilder CAT000Decoded;
        public static StringBuilder CAT001Decoded;
        public static StringBuilder CAT002Decoded;
        public static StringBuilder CAT008Decoded;
        public static StringBuilder CAT034Decoded;
        public static StringBuilder CAT048Decoded;
        public static StringBuilder CAT062Decoded;
        public static StringBuilder CAT063Decoded;
        public static StringBuilder CAT065Decoded;
        public static StringBuilder CAT244Decoded;
        ///////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////
        // This section contains flags indicating which categories are to be 
        // decoded. This is implemented so we do not decode messages we are not
        // interested in.
        // In the future it is to be incluced in GUI interface.
        public static bool Decode_CAT000 = false;
        public static bool Decode_CAT001 = true;
        public static bool Decode_CAT002 = true;
        public static bool Decode_CAT008 = false;
        public static bool Decode_CAT034 = false;
        public static bool Decode_CAT048 = true;
        public static bool Decode_CAT062 = false;
        public static bool Decode_CAT063 = false;
        public static bool Decode_CAT065 = false;
        public static bool Decode_CAT244 = false;

        ///////////////////////////////////////////////////////////////////////////
        //
        // Here store the currently selected IP and Multicast address
        public static string ConnName = "N/A";
        public static string CurrentMulticastAddress = "N/A";
        public static int Current_Port = 0;
    }
}
