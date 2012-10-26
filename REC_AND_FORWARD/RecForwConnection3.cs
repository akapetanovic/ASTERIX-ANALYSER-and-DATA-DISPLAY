using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser
{
    class RecForwConnection3
    {
        private static bool KeepGoing = true;
        private static bool RequestStop = false;

        // Define UDP connection variables
        private static UdpClient sock;
        private static IPEndPoint iep;
        // Buffer to receive raw data
        private static byte[] UDPBuffer;
        // File stream
        private static Stream RecordingStream = null;
        private static BinaryWriter RecordingBinaryWriter = null;

        // Define the main listener thread
        private static Thread ListenForDataThread;

        private static int BytesWritten;

        // Constructor
        public static void StartRecording(string FilePath, // Path and file name 
            IPAddress Interface_Addres, // IP address of the interface where the data is expected
            IPAddress Multicast_Address, // Multicast address of the expected data
            int PortNumber // Port number of the expected data
            )
        {
            // Open up a new socket with the net IP address and port number   
            try
            {
                sock = new UdpClient(PortNumber);
                sock.JoinMulticastGroup(Multicast_Address, Interface_Addres); 
                iep = new IPEndPoint(IPAddress.Any, PortNumber);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Rec Connection 3 Starting: " + e.ToString());
            }

            // Open up the stream
            RecordingStream = new FileStream(FilePath, FileMode.Create);
            RecordingBinaryWriter = new BinaryWriter(RecordingStream);
            BytesWritten = 0;

            KeepGoing = true;
            RequestStop = false;
            ListenForDataThread = new Thread(new ThreadStart(DoWork));
            ListenForDataThread.Start();
        }

        private static void DoWork()
        {
            while (KeepGoing)
            {
                // OK user requested that we terminate 
                // recording, so lets do it
                if (RequestStop == true)
                    KeepGoing = false;
                else
                {
                    try
                    {
                        // Lets receive data in an array of bytes 
                        // (an octet, of course composed of 8bits)
                        UDPBuffer = sock.Receive(ref iep);
                        RecordingBinaryWriter.Write(UDPBuffer);
                        BytesWritten = BytesWritten + UDPBuffer.Length;
                    }
                    catch 
                    {
                    }
                }
            }

            Cleanup();
        }

        // Terminates recording
        public static void StopRecording()
        {
            RequestStop = true;
            Thread.Sleep(200);
            if (ListenForDataThread.IsAlive == true)
            {
                Cleanup();
                ListenForDataThread.Abort();
            }
        }

        public static bool IsConnectionActive()
        {
            if (ListenForDataThread != null)
                return ListenForDataThread.IsAlive;
            else
                return false;
        }

        private static void Cleanup()
        {
            // Do a cleanup
            if (sock != null)
                sock.Close();
            RecordingBinaryWriter.Close();
            RecordingStream.Close();
            RecordingBinaryWriter.Dispose();
            RecordingStream.Dispose();
        }

        public static int GetBytesWritten()
        {
            return BytesWritten;
        }
    }
}
