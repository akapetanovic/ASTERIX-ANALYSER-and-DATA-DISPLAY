using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser
{
    class AsterixReplay
    {
        public static FileReplay File_Replay = new FileReplay();
        public static LANReplay LAN_Replay = new LANReplay();

        /// <summary>
        /// ///////
        /// </summary>
        public  class LANReplay
        {
            // Define UDP-Multicast TX connection variables
            private static UdpClient tx_sock;
            private static IPEndPoint tx_iep;

            // Tries to connect, if succefull returns true, otherwise returns false.
            // Upon succesfull connection each succesfull call to Send will send provided
            // data to the LAN
            public bool Connect(IPAddress Interface_Addres_IN,   // IP address of the interface where the data is expected
                                IPAddress Multicast_Address_IN,  // Multicast address of the expected data
                                int PortNumber_IN)               // Port number of the expected data
            {
                bool Result = true;

                // Open up outgoing socket
                // Open up a new socket with the net IP address and port number   
                try
                {
                    tx_sock = new UdpClient(PortNumber_IN);
                    tx_sock.JoinMulticastGroup(Multicast_Address_IN, Interface_Addres_IN);
                    tx_iep = new IPEndPoint(Multicast_Address_IN, PortNumber_IN);
                }
                catch
                {
                    MessageBox.Show("LAN REPLAY: Not possible! Make sure given IP address/port is a valid one on your system or not already used by some other process");
                    Result = false;
                }

                return Result;
            }

            // To be called opon succefull Connect
            public void Send(byte[] UDPBuffer)
            {
                tx_sock.Send(UDPBuffer, UDPBuffer.Length, tx_iep);
            }
        }

        /// <summary>
        /// /////
        /// </summary>
        public class FileReplay
        {
            private string FilePath;  // Path and file name 
        }
    }
}
