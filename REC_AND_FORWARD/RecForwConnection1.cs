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
    class RecForwConnection1
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

        private static int BytesProcessed;

        private static bool ForwardingEnabled = false;
        private static bool RecordingEnabled = false;

        // Constructor
        public static void StartRecording(string FilePath,              // Path and file name 
                                          IPAddress Interface_Addres,   // IP address of the interface where the data is expected
                                          IPAddress Multicast_Address,  // Multicast address of the expected data
                                          int PortNumber)               // Port number of the expected data
        {
            if (IsConnectionActive() == false)
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
                    MessageBox.Show("Error Rec Connection 1: " + e.ToString());
                }



                KeepGoing = true;
                RequestStop = false;
                ListenForDataThread = new Thread(new ThreadStart(DOWork));
                ListenForDataThread.Start();
            }

            RecordingEnabled = true;
            // Open up the stream

            try
            {
                RecordingStream = new FileStream(FilePath, FileMode.Create);
                RecordingBinaryWriter = new BinaryWriter(RecordingStream);
            }
            catch
            {

            }

            BytesProcessed = 0;
        }

        public static void StartForward(IPAddress Interface_Addres,  // IP address of the interface where the data is expected
                                       IPAddress Multicast_Address, // Multicast address of the expected data
                                       int PortNumber,
                                       IPAddress Frwd_Interface_Addres,  // IP address of the forward interface 
                                       IPAddress Frwd_Multicast_Address, // Multicast address of the forwarded data
                                       int Frwd_PortNumber)              // Port number of the forwarded data
        {


            // Open up outgoing socket


            // Check if recording is already in place, if so then
            // do not create a new incoming connection
            if (IsConnectionActive() == false)
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
                    MessageBox.Show("Error Rec Connection 1: " + e.ToString());
                }

                KeepGoing = true;
                RequestStop = false;
                ListenForDataThread = new Thread(new ThreadStart(DOWork));
                ListenForDataThread.Start();
            }

            ForwardingEnabled = true;
        }

        private static void DOWork()
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
                        BytesProcessed = BytesProcessed + UDPBuffer.Length;

                        if (RecordingEnabled == true)
                            RecordingBinaryWriter.Write(UDPBuffer);

                        // If forwarding enable then forward
                        // the data as soon as it is recived
                        if (ForwardingEnabled == true)
                        {

                        }
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
            if (ForwardingEnabled == false)
            {
                RequestStop = true;
                Thread.Sleep(200);
                if (ListenForDataThread.IsAlive == true)
                {
                    Cleanup();
                    ListenForDataThread.Abort();
                }
            }
            else
            {
                RecordingBinaryWriter.Close();
                RecordingStream.Close();
                RecordingBinaryWriter.Dispose();
                RecordingStream.Dispose();
            }

            RecordingEnabled = false;
        }

        // Terminates forwarding
        public static void StopForwarding()
        {
            if (RecordingEnabled == false)
            {
                RequestStop = true;
                Thread.Sleep(200);
                if (ListenForDataThread.IsAlive == true)
                {
                    Cleanup();
                    ListenForDataThread.Abort();
                }
            }

            ForwardingEnabled = false;
        }

        private static bool IsConnectionActive()
        {
            if (ListenForDataThread != null)
                return ListenForDataThread.IsAlive;
            else
                return false;
        }

        public static bool IsRecordingEnabled()
        {
            return RecordingEnabled;
        }

        public static bool IsForwardingEnabled()
        {
            return ForwardingEnabled;
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

        public static int GetBytesProcessed()
        {
            return BytesProcessed;
        }
    }
}
