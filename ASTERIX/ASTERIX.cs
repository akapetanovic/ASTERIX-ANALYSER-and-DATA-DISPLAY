using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace AsterixDisplayAnalyser
{
    static class ASTERIX
    {
        private static DateTime LastDataBlockDateTimeForRecording;
        private static DateTime LastDataBlockDateTimeForStalleData;

        // This flag indicates that recording of data
        // just started
        private static bool RecordingJustStarted = true;
        private static Stream RecordingStream = null;
        private static BinaryWriter RecordingBinaryWriter = null;

        // GIT TEST
        public class SIC_SAC_Time
        {
            public int SIC;
            public int SAC;
            public DateTime TimeofReception;

            public SIC_SAC_Time()
            {
            }

            public SIC_SAC_Time(int SIC_IN, int SAC_IN, DateTime Time_IN)
            {
                SIC = SIC_IN;
                SAC = SAC_IN;
                TimeofReception = Time_IN;
            }
        }

        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private static volatile bool _shouldStop = false;

        // Saves off the time of reception
        // used by decoders to stamp individual messages
        public static DateTime TimeOfReception;

        // Define UDP connection variables
        private static UdpClient sock;
        private static IPEndPoint iep;
        // Buffer to receive raw data
        private static byte[] UDPBuffer;

        public static void CleanUp()
        {
            // If socket is opened then close it
            if (sock != null)
                sock.Close();
        }

        // This is to be called by the FrmSettings to re-initialize
        // the socket 
        public static bool ReinitializeSocket()
        {
            // Save the state of the flag that indicates that data is to be
            // acquired
            bool Listen_for_Data_Was_On = SharedData.bool_Listen_for_Data;
            bool Reinitialize_Result = true;

            // If data is being acquired then stop 
            // acquistion and wait for a 1 second
            if (Listen_for_Data_Was_On == true)
            {
                SharedData.bool_Listen_for_Data = false;
                Thread.Sleep(1000);
            }

            // If socket is opened then close it
            if (sock != null)
                sock.Close();

            // Open up a new socket with the net IP address and port number   
            try
            {
                sock = new UdpClient();
                sock.ExclusiveAddressUse = false;
                iep = new IPEndPoint(IPAddress.Any, SharedData.Current_Port);
                sock.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                sock.ExclusiveAddressUse = false;
                sock.Client.Bind(iep);
                sock.JoinMulticastGroup(IPAddress.Parse(SharedData.CurrentMulticastAddress), IPAddress.Parse(SharedData.CurrentInterfaceIPAddress));
            }
            catch
            {
                MessageBox.Show("ASTERIX ReinitializeSocket: Not possible! Make sure given IP address/port is a valid one on your system or not already used by some other process");
                Reinitialize_Result = false;
            }

            // Everything is done, now resume listening (data acqusition) if it was active
            // before new data was entered.
            if (Listen_for_Data_Was_On == true)
                SharedData.bool_Listen_for_Data = true;

            return Reinitialize_Result;
        }

        public static void RequestStop()
        {
            _shouldStop = true;
            if (sock != null)
                sock.Close();
        }

        // Returns time passed since the last data block was received
        public static TimeSpan GetTimeSpanSinceLastDataBlockRecived()
        {
            return (DateTime.Now - LastDataBlockDateTimeForStalleData);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // The method that gets invoked as a thread when a Connect button is 
        // pressed. It will listen on a given multicast address and store messages in the 
        // list box above.
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void ListenForData()
        {
            bool ThereWasAnException = false;

            // Loop forever
            while (!_shouldStop)
            {
                // Do something only if user has requested so
                if (SharedData.bool_Listen_for_Data)
                {
                    ThereWasAnException = false;
                    try
                    {
                        // Lets receive data in an array of bytes 
                        // (an octet, of course composed of 8bits)
                        UDPBuffer = sock.Receive(ref iep);
                        LastDataBlockDateTimeForStalleData = DateTime.Now;
                    }
                    catch
                    {
                        ThereWasAnException = true;
                    }

                    if (ThereWasAnException == false)
                    {

                        // Extract lenghts
                        int LengthOfASTERIX_CAT = ASTERIX.ExtractLengthOfDataBlockInBytes_Int(UDPBuffer);
                        int LenghtOfDataBuffer = UDPBuffer.Length;
                        int DataBufferIndexForThisExtraction = 0;

                        // Loop through the buffer and pass on each ASTERIX category block to
                        // the category extractor. The extractor itslef will then extract individual
                        // data records.
                        while (DataBufferIndexForThisExtraction < LenghtOfDataBuffer)
                        {
                            byte[] LocalSingle_ASTERIX_CAT_Buffer = new byte[LengthOfASTERIX_CAT];
                            Array.Copy(UDPBuffer, DataBufferIndexForThisExtraction, LocalSingle_ASTERIX_CAT_Buffer, 0, LengthOfASTERIX_CAT);
                            ExtractAndDecodeASTERIX_CAT_DataBlock(LocalSingle_ASTERIX_CAT_Buffer, true);

                            DataBufferIndexForThisExtraction = DataBufferIndexForThisExtraction + LengthOfASTERIX_CAT;

                            if (DataBufferIndexForThisExtraction < LenghtOfDataBuffer)
                            {
                                Array.Copy(UDPBuffer, DataBufferIndexForThisExtraction, LocalSingle_ASTERIX_CAT_Buffer, 0, 3);
                                LengthOfASTERIX_CAT = ASTERIX.ExtractLengthOfDataBlockInBytes_Int(LocalSingle_ASTERIX_CAT_Buffer);
                            }
                        }

                        // Check if recording of the currently live connection is requested
                        if (SharedData.DataRecordingClass.DataRecordingRequested == true)
                        {
                            if (RecordingJustStarted == true)
                            {
                                RecordingJustStarted = false;
                                RecordingStream = new FileStream(SharedData.DataRecordingClass.FilePathandName, FileMode.Create);
                                RecordingBinaryWriter = new BinaryWriter(RecordingStream);
                                LastDataBlockDateTimeForRecording = DateTime.Now;
                            }

                            // Determine the type of the recording
                            if (Properties.Settings.Default.RecordActiveInRaw == true)
                            {
                                // Just record in the raw format with not headers added
                                RecordingBinaryWriter.Write(UDPBuffer);
                            }
                            else // Here add headers
                            {
                                TimeSpan TimeDiff = DateTime.Now - LastDataBlockDateTimeForRecording;
                                LastDataBlockDateTimeForRecording = DateTime.Now;
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
                        }
                        else if (RecordingJustStarted == false)
                        {
                            RecordingJustStarted = true;
                            // Close the data stream
                            if (RecordingBinaryWriter != null)
                                RecordingBinaryWriter.Close();
                            if (RecordingStream != null)
                                RecordingStream.Close();
                        }
                    }
                }
            }

            _shouldStop = false;
        }

        public static string DecodeAsterixData(string FileName)
        {
            long Position = 0;
            // get total byte length of the file
            long TotalBytes = new System.IO.FileInfo(FileName).Length;

            try
            {
                // Open file for reading
                System.IO.FileStream FileStream = new System.IO.FileStream(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // attach filestream to binary reader
                System.IO.BinaryReader BinaryReader = new System.IO.BinaryReader(FileStream);

                while (Position < TotalBytes)
                {
                    // First determine the size of the message
                    // octet    data
                    // 0        ASTERIX CATEGORY
                    // 1 .. 2   LENGTH OF MESSAGE BLOCK
                    byte[] Data_Block_Buffer = BinaryReader.ReadBytes((Int32)3);
                    int LengthOfMessageBlock = ASTERIX.ExtractLengthOfDataBlockInBytes_Int(Data_Block_Buffer);

                    BinaryReader.BaseStream.Position = BinaryReader.BaseStream.Position - 3;
                    // Now read the message and pass it to the parser
                    // for decoding
                    Data_Block_Buffer = BinaryReader.ReadBytes((Int32)LengthOfMessageBlock);
                    ExtractAndDecodeASTERIX_CAT_DataBlock(Data_Block_Buffer, false);
                    Position = BinaryReader.BaseStream.Position;
                }

                // close file reader
                FileStream.Close();
                FileStream.Dispose();
                BinaryReader.Close();
            }
            catch (Exception Exception)
            {
                // Error
                MessageBox.Show("Exception caught in process: {0}", Exception.ToString());
            }

            return "Read " + Position.ToString() + " of total " + TotalBytes.ToString() + " bytes";
        }


        private static void ExtractAndDecodeASTERIX_CAT_DataBlock(byte[] DataBlock, bool Is_Live_Data)
        {
            // First thing is to store the time of the reception regardless of the category received
            string Time = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + ":" +
                DateTime.Now.Second.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Millisecond.ToString().PadLeft(3, '0');

            // Save off the time of reception so decoders can use it
            TimeOfReception = DateTime.Now;

            // Extract ASTERIX category
            string Category = ASTERIX.ExtractCategory(DataBlock);

            // Extract lenght in Bytes, as indicated by the ASTERIX
            string LengthOfDataBlockInBytes = ASTERIX.ExtractLengthOfDataBlockInBytes(DataBlock);

            // Here format the lenght of bytes 
            // to always use 3 characters for better alignement
            if (LengthOfDataBlockInBytes.Length < 3)
            {
                LengthOfDataBlockInBytes = "0" + LengthOfDataBlockInBytes;
            }
            else if (LengthOfDataBlockInBytes.Length < 2)
            {
                LengthOfDataBlockInBytes = "00" + LengthOfDataBlockInBytes;
            }

            // Define a string to store data not specific for all messages and add commond data
            // 1. TIME of reception
            // 2. Source IP address
            // 3. Multicast IP address
            // 4. Length of data block in bytes
            // 5. Asterix Category
            // 
            // 6. Append Category specifc data, done just below

            string Common_Message_Data_String;
            if (Is_Live_Data == true)
                Common_Message_Data_String = Time + "     " + iep.ToString() + "        " + SharedData.CurrentMulticastAddress + ':' + SharedData.Current_Port.ToString() + "             " + LengthOfDataBlockInBytes.ToString() + "        " + Category + "           ";
            else
                Common_Message_Data_String = Time + "     " + "Recorded" + "        " + "Recorded" + ':' + "Recorded" + "             " + LengthOfDataBlockInBytes.ToString() + "        " + Category + "           ";
            // Hold individual records of the messages 
            // from an individual data block
            string[] MessageData = new string[3000];

            byte[] DataBlockNoCATandLEN = new byte[DataBlock.Length - 3];

            // Now after we extracted Category and Lenght of the Data Block lets remove the first three octets from the data 
            // buffer and pass it on to individual message handlers to do message decoding
            Array.Copy(DataBlock, 3, DataBlockNoCATandLEN, 0, (DataBlock.Length - 3));

            DataBlock = DataBlockNoCATandLEN;

            // Now do a switch based on the category received
            int NumOfMsgsDecoded = 0;
            switch (Category)
            {
                // Monoradar Data Target Reports, from a Radar Surveillance System to an SDPS
                // (plots and tracks from PSRs, SSRs, MSSRs, excluding Mode S and ground surveillance)
                case "001":
                    if (Properties.Settings.Default.CAT_001_Enabled == true)
                    {
                        CAT01 MyCAT01 = new CAT01();
                        MessageData = MyCAT01.Decode(DataBlock, Time, out NumOfMsgsDecoded);
                    }
                    break;
                // Monoradar Service Messages (status, North marker, sector crossing messages)
                case "002":
                    if (Properties.Settings.Default.CAT_002_Enabled == true)
                    {
                        CAT02 MyCAT02 = new CAT02();
                        MessageData = MyCAT02.Decode(DataBlock, Time, out NumOfMsgsDecoded);
                    }
                    break;
                // Monoradar Derived Weather Information
                case "008":
                    if (Properties.Settings.Default.CAT_008_Enabled == true)
                    {
                        CAT08 MyCAT08 = new CAT08();
                    }
                    break;
                // Next version of Category 002: PSR Radar, M-SSR Radar, Mode-S Station
                case "034":
                    if (Properties.Settings.Default.CAT_034_Enabled == true)
                    {
                        CAT34 MyCAT34 = new CAT34();
                        MessageData = MyCAT34.Decode(DataBlock, Time, out NumOfMsgsDecoded);
                    }
                    break;
                // Next version of Category 001: PSR Radar, M-SSR Radar, Mode-S Station
                case "048":
                    if (Properties.Settings.Default.CAT_048_Enabled == true)
                    {
                        CAT48 MyCAT48 = new CAT48();
                        MessageData = MyCAT48.Decode(DataBlock, Time, out NumOfMsgsDecoded);
                    }
                    break;
                // System Track Data(next version of Category 030 & 011, also applicable to non-ARTAS systems)
                case "062":
                    if (Properties.Settings.Default.CAT_062_Enabled == true)
                    {
                        CAT62 MyCAT62 = new CAT62();
                        MessageData = MyCAT62.Decode(DataBlock, Time, out NumOfMsgsDecoded);
                    }
                    break;
                // Sensor Status Messages (SPDS)
                case "063":
                    if (Properties.Settings.Default.CAT_063_Enabled == true)
                    {
                        CAT63 MyCAT63 = new CAT63();
                    }
                    break;
                // SDPS Service Status Messages (SDPS)
                case "065":
                    if (Properties.Settings.Default.CAT_065_Enabled == true)
                    {
                        CAT65 MyCAT65 = new CAT65();
                    }
                    break;
                // Transmission of Reference Trajectory State Vectors
                case "244":
                    if (Properties.Settings.Default.CAT_244_Enabled == true)
                    {
                        CAT244 MyCAT244 = new CAT244();
                    }
                    break;
                // Handle unsupported data/categories
                default:
                    Common_Message_Data_String = Common_Message_Data_String + " Unsupported category " + Category + " has been received";
                    break;
            }

            if (Properties.Settings.Default.PopulateMainListBox == true)
            {
                for (int I = 0; I < NumOfMsgsDecoded; I++)
                    SharedData.DataBox.Items.Add(Common_Message_Data_String + MessageData[I]);
            }
        }

        ////////////////////////////////////////////////////////////////////////
        // Define common asterix helper methods and constants
        ////////////////////////////////////////////////////////////////////////

        // Define indexes to the byte array used to receive data
        // These indexes are used to access individual octets of 
        // common ASTERIX data received.
        private static int CAT_Octet = 0;
        private static int First_LEN_Octet = 1;
        private static int Second_LEN_Octet = 2;

        // For now assume that a maximum number of data items 
        // So, in a category is 28, So 28 / 7 = 4
        // WARRNING: This assumes that the first three octets
        // of a data block are removed as it is common for all
        // records in one data block
        private static int FirstFSPECS_Byte_Index = 0;
        private static int SecondFSPECS_Byte_Index = 1;
        private static int ThirdFSPECS_Byte_Index = 2;
        private static int FourthFSPECS_Byte_Index = 3;
        private static int FifthFSPECS_Byte_Index = 4;

        /////////////////////////////////////////////////////////////////////////
        // Extracts the first 8 bits from the given messages. These first 8 bits
        // are always the data category
        public static string ExtractCategory(byte[] Data)
        {
            string DataOut = Data[CAT_Octet].ToString();

            if (DataOut.Length == 1)
            {
                DataOut = '0' + DataOut;
            }

            // Here format the lenght of bytes 
            // to always use 3 characters for better alignement
            if (DataOut.Length < 3)
            {
                DataOut = "0" + DataOut;
            }
            else if (DataOut.Length < 2)
            {
                DataOut = "00" + DataOut;
            }

            return DataOut;
        }

        ///////////////////////////////////////////////////////////////////////////
        // This one extracts the next 16 bits, which is 
        // lenght of the data block
        public static string ExtractLengthOfDataBlockInBytes(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            // Extract first 16 bits 
            // 15..........................0
            // 00000000             00000000
            // First_LEN_Octet   Second_LEN_Octet

            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[Second_LEN_Octet];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[First_LEN_Octet];

            int Result = BO.DWord[Bit_Ops.Bits0_15_Of_DWord];
            return Result.ToString();
        }

        ///////////////////////////////////////////////////////////////////////////
        // This one extracts the next 16 bits, which is 
        // lenght of the data block
        public static int ExtractLengthOfDataBlockInBytes_Int(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            // Extract first 16 bits 
            // 15..........................0
            // 00000000             00000000
            // First_LEN_Octet   Second_LEN_Octet

            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[Second_LEN_Octet];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[First_LEN_Octet];

            int Result = BO.DWord[Bit_Ops.Bits0_15_Of_DWord];
            return Result;
        }

        // This method returns the index to the first
        // byte of the user data after FSPEC, SIC and SAC
        // assumes that the first three octets from the data
        // block are removed from the buffer
        public static int GetFirstDataIndex(byte[] Data)
        {
            // Determine Length of FSPEC fields in bytes
            int FSPEC_Length = ASTERIX.DetermineLenghtOfFSPEC(Data);

            // Determine SIC/SAC Index
            int SIC_Index = FSPEC_Length;
            int SAC_Index = SIC_Index + 1;

            return SAC_Index + 1;
        }

        // This method returns the lenght of FSPEC in bytes (8bits). 
        // The minumum value is 1, and if it is bigger than one byte then 
        // bit 8 is set to true to indicate that next byte is also used
        // as FSPEC field. After the last FSPEC field ASTERIX data items begin.
        public static int DetermineLenghtOfFSPEC(byte[] Data)
        {
            // Assume it is 1
            int LenghtOfFSPEC = 1;

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            // Move each octet into the DWORD
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[FirstFSPECS_Byte_Index];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[SecondFSPECS_Byte_Index];
            BO.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[ThirdFSPECS_Byte_Index];
            BO.DWord[Bit_Ops.Bits24_31_Of_DWord] = Data[FourthFSPECS_Byte_Index];

            ////////////////////////////////////////////////////////////////////////
            // Now check for the 8th bit of each FSPEC field. If it is set 
            // to true then the next octet is also FSPEC.
            if (BO.DWord[Bit_Ops.Bit0] == true)
            {
                LenghtOfFSPEC++;
                if (BO.DWord[Bit_Ops.Bit8] == true)
                {
                    LenghtOfFSPEC++;
                    if (BO.DWord[Bit_Ops.Bit16] == true)
                    {
                        LenghtOfFSPEC++;
                        if (BO.DWord[Bit_Ops.Bit24] == true)
                        {
                            LenghtOfFSPEC++;
                        }
                    }
                }
            }

            return LenghtOfFSPEC;
        }

        /////////////////////////////////////////////////////////////////////////
        // This method returns the first four FSPEC octets 
        // as a BitVector32. User then can evaluate the individual bits
        // and interpret their meaining depending on the ASTERIX CAT
        //
        // NOTE: It returns the four possible FSPEC, but user is first to
        // determine the length of the FSPEC and according to that to use it.
        public static BitVector32 GetFourFSPECOctets(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            // Move each octet into the DWORD
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[FirstFSPECS_Byte_Index];
            BO.DWord[Bit_Ops.Bits8_15_Of_DWord] = Data[SecondFSPECS_Byte_Index];
            BO.DWord[Bit_Ops.Bits16_23_Of_DWord] = Data[ThirdFSPECS_Byte_Index];
            BO.DWord[Bit_Ops.Bits24_31_Of_DWord] = Data[FourthFSPECS_Byte_Index];

            return BO.DWord;
        }

        /////////////////////////////////////////////////////////////////////////
        // This method returns the fifth FSPEC octet 
        // as a BitVector32. User then can evaluate the individual bits
        // and interpret their meaining depending on the ASTERIX CAT
        public static BitVector32 GetFifthFSPECOctet(byte[] Data)
        {
            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();

            // Move each octet into the DWORD
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[FifthFSPECS_Byte_Index];

            return BO.DWord;
        }
    }
}
