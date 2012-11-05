using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser
{
    public static class SharedData
    {

        // DUBUG CODE
       // public static FrmDebug DebugFrame = new FrmDebug();

        // Define type for ASTERIX categories supported.
        public enum Supported_Asterix_CAT_Type { Undefined, CAT001, CAT002, CAT008, CAT034, CAT048, CAT062, CAT063, CAT065, CAT244 };

        // This boolean is just a flag which indicates 
        // if data is to be collected. It is set ON/OFF from
        // the user interface.
        public static bool bool_Listen_for_Data = false;

        // This boolean indicates if data recording is
        // requested. If so then ASTERIX package will store
        // all the received data in the C://ASTERX/RECORDING 
        // directory
        public static class DataRecordingClass
        {
            public static bool DataRecordingRequested = false;
            public static string FilePathandName = "";
        }

        // This list box stores received data while the listener is
        // running. Once the data has been received the data is to be 
        // decoded and stored in appropriate format
        public static ListBox DataBox = new ListBox();

        // Used to convert feet to meeters
        public static double FeetToMeeters = 0.3048;

        ///////////////////////////////////////////////////////////////////////////
        //
        // Here store connection parameters
        public static string ConnName = "N/A";
        public static string CurrentMulticastAddress = "N/A";
        public static string CurrentInterfaceIPAddress = "N/A";
        public static int Current_Port = 0;

        public static void ResetConnectionParameters()
        {
            ConnName = "N/A";
            CurrentMulticastAddress = "N/A";
            CurrentInterfaceIPAddress = "N/A";
            Current_Port = 0;
        }
    }
}
