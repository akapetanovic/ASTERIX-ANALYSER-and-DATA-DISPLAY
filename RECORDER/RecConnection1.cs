using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AsterixDisplayAnalyser
{
    class RecConnection1
    {
        private static string Directory_and_FileName = "";
        private static string CurrentMulticastAddress = "N/A";
        private static string CurrentInterfaceIPAddress = "N/A";
        private static int Current_Port = 0;

        private static bool KeepGoing = true;
        private static bool RequestStop = false;

        // Define UDP connection variables
        private static UdpClient sock;
        private static IPEndPoint iep;
        // Buffer to receive raw data
        private static byte[] UDPBuffer;

        // Define the main listener thread
        Thread ListenForDataThread = new Thread(new ThreadStart(Record));

        // Constructor
        public RecConnection1(string FilePath, // Path and file name 
            IPAddress Interface_Addres, // IP address of the interface where the data is expected
            IPAddress Multicast_Address, // Multicast address of the expected data
            int PortNumber // Port number of the expected data
            )
        {

        }

        private static void Record()
        {
            while (KeepGoing)
            {
                // OK user requested that we terminate 
                // recording, so lets do it
                if (RequestStop == true)
                {

                    KeepGoing = false;
                }
            }
        }

        // Starts recording
        public void StartRecording()
        {
            KeepGoing = true;
            RequestStop = false;
            ListenForDataThread.Start();
        }

        // Terminates recording
        public void StopRecording()
        {
            RequestStop = true;
        }
    }
}
