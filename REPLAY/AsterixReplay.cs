using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;

namespace AsterixDisplayAnalyser
{
    class AsterixReplay
    {
        // Defines possible Replay statuses
        public enum ReplayStatus { Disconnected, Connected, Replaying, Paused };

        /// <summary>
        /// ///////
        /// </summary>
        public static class LANReplay
        {
            // Thread control
            private static bool KeepGoing = true;
            private static bool RequestStop = false;
            // Define the main listener thread
            private static Thread ReplayThread;

            // Replay Status varable
            private static ReplayStatus Replay_Status = ReplayStatus.Disconnected;
            // Replay speed
            private static int ReplaySpeed = 1;

            // Define UDP-Multicast TX connection variables
            private static UdpClient tx_sock;
            private static IPEndPoint tx_iep;

            // File handling
            private static System.IO.FileStream FileStream = null;
            private static System.IO.BinaryReader BinaryReader = null;
            private static long TotalFileSizeBytes = 0;
            private static long FilePosition;

            // Tries to connect, if succefull returns true, otherwise returns false.
            // Upon succesfull connection each succesfull call to Send will send provided
            // data to the LAN. It also opens up the provided xxx.rply file. True is returned anly if all provided data
            // is OK
            public static bool Connect(string FilePath,                  // Path and file name 
                                        IPAddress Interface_Addres_IN,   // IP address of the interface where the data is expected
                                        IPAddress Multicast_Address_IN,  // Multicast address of the expected data
                                        int PortNumber_IN)               // Port number of the expected data
            {
                bool Result = true;

                // Open up outgoing socket
                // Open up a new socket with the net IP address and port number   
                try
                {
                    tx_sock = new UdpClient();
                    tx_sock.JoinMulticastGroup(Multicast_Address_IN, Interface_Addres_IN);
                    tx_iep = new IPEndPoint(Multicast_Address_IN, PortNumber_IN);
                }
                catch
                {
                    MessageBox.Show("LAN REPLAY: Not possible! Make sure given IP address/port is a valid one on your system or not already used by some other process");
                    Result = false;
                }

                // Open up data source file
                try
                {
                    TotalFileSizeBytes = new System.IO.FileInfo(FilePath).Length;
                    // Open file for reading
                    FileStream = new System.IO.FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                    // attach filestream to binary reader
                    BinaryReader = new System.IO.BinaryReader(FileStream);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                // Check if there is any data in the file
                if (TotalFileSizeBytes == 0)
                {
                    MessageBox.Show("Empty file, please select another one");
                    Result = false;
                }

                // If everything is OK so far set the status to connected
                if (Result == true)
                    SetStatus(ReplayStatus.Connected);

                return Result;
            }

            // Call to stop replay and release resources
            public static void Disconnect()
            {
                SetStatus(ReplayStatus.Disconnected);
            }
            // Call to pause the replay
            public static void Pause()
            {
                if (Replay_Status != ReplayStatus.Disconnected)
                SetStatus(ReplayStatus.Paused);
            }
            // Call to start the replay
            public static void Start()
            {
               if (Replay_Status != ReplayStatus.Disconnected)
                SetStatus(ReplayStatus.Replaying);
            }
            // Return replay status
            public static ReplayStatus GetCurrentStatus()
            {
                return Replay_Status;
            }
            
            // Status change handler
            private static void SetStatus(ReplayStatus Status)
            {
                // If status desired is already reached do nothing
                if (Status != Replay_Status)
                {
                    switch (Status)
                    {
                        case AsterixReplay.ReplayStatus.Disconnected:
                            StopThread();
                            break;
                        case AsterixReplay.ReplayStatus.Connected:
                            KeepGoing = true;
                            RequestStop = false;
                            ReplayThread = new Thread(new ThreadStart(DOWork));
                            ReplayThread.Start();
                            Replay_Status = ReplayStatus.Connected;
                            break;
                        case AsterixReplay.ReplayStatus.Paused:
                            Replay_Status = ReplayStatus.Paused;
                            break;
                        case AsterixReplay.ReplayStatus.Replaying:
                            Replay_Status = ReplayStatus.Replaying;
                            break;
                    }
                }
            }
            // Called by the thread to handle the work
            private static void DOWork()
            {
               FilePosition = 0;

                // Loop until either until the end of file is reached or user requested to stop
                while ((KeepGoing == true) && (FilePosition < TotalFileSizeBytes))
                {
                    // OK user requested that we terminate 
                    // recording, so lets do it
                    if (RequestStop == true) 
                        KeepGoing = false;
                    else
                    {
                        // If not paused
                        if (Replay_Status == ReplayStatus.Replaying)
                        {
                            try
                            {
                                // Lets determine the size of the block
                                int BlockSize = BinaryReader.ReadInt32();
                                // Now determine the time since the last data block
                                int TimeBetweenMessages = BinaryReader.ReadInt32();
                                // Now read the data block as indicated by the size
                                byte [] Data_Block_Buffer = BinaryReader.ReadBytes(BlockSize);
                                
                                // Wait the same time as in the orignal data set or faster as indicated by the 
                                // replay speed factor
                                Thread.Sleep(TimeBetweenMessages / ReplaySpeed);

                                // Send the data to the specifed interface/multicast address/port
                                tx_sock.Send(Data_Block_Buffer, Data_Block_Buffer.Length, tx_iep);

                                // Assign file position
                                FilePosition = BinaryReader.BaseStream.Position;

                            }
                            catch(Exception e)
                            {
                                MessageBox.Show(e.Message);
                            }

                        }
                    }
                }

                FrmReplayForm ReplayForm = Application.OpenForms["FrmReplayForm"] as FrmReplayForm;
                ReplayForm.NotifyReplayCompleted();

                Cleanup();
            }
            // Terminates recording
            private static void StopThread()
            {
                if (ReplayThread != null)
                {
                    RequestStop = true;
                    Thread.Sleep(200);
                    if (ReplayThread.IsAlive == true)
                    {
                        Cleanup();
                        ReplayThread.Abort();
                    }
                }
            }
            // Stops replay and releases resources
            private static void Cleanup()
            {

                if (tx_sock != null)
                    tx_sock.Close();

                if (BinaryReader != null)
                    BinaryReader.Close();
                if (FileStream != null)
                    FileStream.Close();
                if (BinaryReader != null)
                    BinaryReader.Dispose();
                if (FileStream != null)
                    FileStream.Dispose();

                TotalFileSizeBytes = 0;
                Replay_Status = ReplayStatus.Disconnected;
            }

            public static long GetBytesProcessedSoFar()
            {
                return FilePosition;
            }

            public static void SetReplaySpeed (int Speed)
            {
                ReplaySpeed = Speed;
            }
        }

        /// <summary>
        /// /////
        /// </summary>
        public class FileReplay
        {
            //private string FilePath;  // Path and file name 
        }
    }
}
