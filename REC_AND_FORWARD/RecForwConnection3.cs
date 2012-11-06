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

        // Define UDP-Multicast RCV connection variables
        private static UdpClient rcv_sock;
        private static IPEndPoint rcv_iep;

        // Define UDP-Multicast TX connection variables
        private static UdpClient tx_sock;
        private static IPEndPoint tx_iep;

        // Same buffer is used for sending and receiving
        private static byte[] UDPBuffer;

        // File stream
        private static Stream RecordingStream = null;
        private static BinaryWriter RecordingBinaryWriter = null;

        // Define the main listener thread
        private static Thread ListenForDataThread;

        private static int BytesProcessed;

        private static bool ForwardingEnabled = false;
        private static bool RecordingEnabled = false;
        private static bool ReplayFormatRequested = false;
        private static DateTime LastDataBlockDateTime;

        // Constructor
        public static bool StartRecording(bool Is_ReplayFormat, 
                                          string FilePath,              // Path and file name 
                                          IPAddress Interface_Addres,   // IP address of the interface where the data is expected
                                          IPAddress Multicast_Address,  // Multicast address of the expected data
                                          int PortNumber)               // Port number of the expected data
        {
            if (IsConnectionActive() == false)
            {
                // Open up a new socket with the net IP address and port number   
                try
                {
                    rcv_sock = new UdpClient();
                    rcv_sock.ExclusiveAddressUse = false;
                    rcv_iep = new IPEndPoint(IPAddress.Any, PortNumber);
                    rcv_sock.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    rcv_sock.ExclusiveAddressUse = false;
                    rcv_sock.Client.Bind(rcv_iep);
                    rcv_sock.JoinMulticastGroup(Multicast_Address, Interface_Addres);
                }
                catch
                {
                    MessageBox.Show("Not possible! Make sure given IP address/port is a valid one on your system or not already used by some other process");
                    return false;
                }

                KeepGoing = true;
                RequestStop = false;
                ListenForDataThread = new Thread(new ThreadStart(DOWork));
                ListenForDataThread.Start();
            }

            RecordingEnabled = true;
            ReplayFormatRequested = Is_ReplayFormat;
            LastDataBlockDateTime = DateTime.Now;

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
            return true;
        }

        public static bool StartForward(IPAddress Interface_Addres,  // IP address of the interface where the data is expected
                                       IPAddress Multicast_Address, // Multicast address of the expected data
                                       int PortNumber,
                                       IPAddress Frwd_Interface_Addres,  // IP address of the forward interface 
                                       IPAddress Frwd_Multicast_Address, // Multicast address of the forwarded data
                                       int Frwd_PortNumber)              // Port number of the forwarded data
        {
            // Open up outgoing socket
            // Open up a new socket with the net IP address and port number   
            try
            {
                tx_sock = new UdpClient();
                tx_sock.JoinMulticastGroup(Frwd_Multicast_Address, Frwd_Interface_Addres);
                tx_iep = new IPEndPoint(Frwd_Multicast_Address, Frwd_PortNumber);
            }
            catch
            {
                MessageBox.Show("Not possible! Make sure given IP address/port is a valid one on your system or not already used by some other process");
                return false;
            }

            // Check if recording is already in place, if so then
            // do not create a new incoming connection
            if (IsConnectionActive() == false)
            {
                // Open up a new socket with the net IP address and port number   
                try
                {
                    rcv_sock = new UdpClient(PortNumber);
                    rcv_sock.JoinMulticastGroup(Multicast_Address, Interface_Addres);
                    rcv_iep = new IPEndPoint(IPAddress.Any, PortNumber);
                }
                catch
                {
                    MessageBox.Show("Not possible! Make sure given IP address/port is a valid one on your system or not already used by some other process");
                    return false;
                }

                KeepGoing = true;
                RequestStop = false;
                ListenForDataThread = new Thread(new ThreadStart(DOWork));
                ListenForDataThread.Start();
            }

            ForwardingEnabled = true;
            return true;
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
                        UDPBuffer = rcv_sock.Receive(ref rcv_iep);
                        BytesProcessed = BytesProcessed + UDPBuffer.Length;

                        if (RecordingEnabled == true)
                        {
                            if (ReplayFormatRequested == true)
                            {
                                TimeSpan TimeDiff = DateTime.Now - LastDataBlockDateTime;
                                LastDataBlockDateTime = DateTime.Now;
                                // Header 1: Size of the original data block
                                // Header 2: The time between two data blocks (current and the last one)
                                //-----------------------------------------------------------------------
                                // Header 1 // Header 2
                                // ----------------------------------------------------------------------
                                // 4 bytes  // 4 bytes 
                                // First add the size of the data block, not including the two headers

                                // Block size
                                RecordingBinaryWriter.Write(UDPBuffer.Length);
                                // Time between last and this block
                                RecordingBinaryWriter.Write(TimeDiff.Milliseconds);
                                // Now write the data block
                                RecordingBinaryWriter.Write(UDPBuffer);
                            }
                            else
                            {
                                RecordingBinaryWriter.Write(UDPBuffer);
                            }
                        }

                        // If forwarding enable then forward
                        // the data as soon as it is recived
                        if (ForwardingEnabled == true)
                        {
                            tx_sock.Send(UDPBuffer, UDPBuffer.Length, tx_iep);
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
            if (ListenForDataThread != null)
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
        }

        // Terminates forwarding
        public static void StopForwarding()
        {
            if (ListenForDataThread != null)
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
                if (tx_sock != null)
                    tx_sock.Close();
            }
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
            if (rcv_sock != null)
                rcv_sock.Close();

            if (tx_sock != null)
                tx_sock.Close();

            if (RecordingBinaryWriter != null)
                RecordingBinaryWriter.Close();
            if (RecordingStream != null)
                RecordingStream.Close();
            if (RecordingBinaryWriter != null)
                RecordingBinaryWriter.Dispose();
            if (RecordingStream != null)
                RecordingStream.Dispose();
        }

        public static int GetBytesProcessed()
        {
            return BytesProcessed;
        }
    }
}
