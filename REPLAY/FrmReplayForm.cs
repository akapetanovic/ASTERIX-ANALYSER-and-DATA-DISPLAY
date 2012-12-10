using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace AsterixDisplayAnalyser
{
    public partial class FrmReplayForm : Form
    {

        private static bool ReplayHasCompleted = false;

        public FrmReplayForm()
        {
            InitializeComponent();
        }


        public void NotifyReplayCompleted()
        {
            ReplayHasCompleted = true;
        }

        // Do not provide lower/max/close buttons
        private void ReplayForm_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.txtboxIPAddress.Text = Properties.Settings.Default.ReplayMulticast;
            this.textboxPort.Text = Properties.Settings.Default.ReplayPort;
        }

        // Handle file open dialog
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "ASTERIX Analyser Files|*.rply";
            openFileDialog1.InitialDirectory = "Application.StartupPath";
            openFileDialog1.Title = "Open File to Read";

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                this.labelSourceFile.Text = openFileDialog1.FileName;
                this.lblFileSize.Text = new System.IO.FileInfo(openFileDialog1.FileName).Length.ToString() + " Bytes";
            }
        }

        // Just hide the form
        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        // Handle CONNECT/DISCONNECT button
        private void btnStartStop_Click(object sender, EventArgs e)
        {
            AsterixReplay.ReplayStatus ReplayStatus = AsterixReplay.LANReplay.GetCurrentStatus();

            switch (ReplayStatus)
            {
                case AsterixReplay.ReplayStatus.Disconnected:

                    bool Input_Validated = true;
                    IPAddress IP = IPAddress.Any;
                    IPAddress Multicast = IPAddress.Any;
                    int PortNumber = 2222;

                    if (labelSourceFile.Text == "N/A")
                    {
                        MessageBox.Show("Please set the input file");
                        Input_Validated = false;
                    }
                    else
                    {

                        // First make sure that all boxes are filled out
                        if ((!string.IsNullOrEmpty(this.txtboxIPAddress.Text)) &&
                             (!string.IsNullOrEmpty(this.comboBoxNetworkInterface.Text)) &&
                            (!string.IsNullOrEmpty(this.textboxPort.Text)))
                        {

                            // Validate that a valid IP address is entered
                            if ((IPAddress.TryParse(this.txtboxIPAddress.Text, out Multicast) != true) || (IPAddress.TryParse(this.comboBoxNetworkInterface.Text, out IP) != true))
                            {
                                MessageBox.Show("Not a valid IP address");
                                Input_Validated = false;
                            }
                            else // Add a check that this is a valid multicast address
                            {
                                UdpClient TempSock;
                                TempSock = new UdpClient(2222);// Port does not matter
                                // Open up a new socket with the net IP address and port number   
                                try
                                {
                                    TempSock.JoinMulticastGroup(Multicast, 50); // 50 is TTL value
                                }
                                catch
                                {
                                    MessageBox.Show("Not valid Multicast address (has to be in range 224.0.0.0 to 239.255.255.255");
                                    Input_Validated = false;
                                }
                                if (TempSock != null)
                                    TempSock.Close();
                            }

                            if (int.TryParse(this.textboxPort.Text, out PortNumber) && (PortNumber >= 1 && PortNumber <= 65535))
                            {
                            }
                            else
                            {
                                MessageBox.Show("Invalid Port number");
                                Input_Validated = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please fill out all data fileds");
                            Input_Validated = false;
                        }
                    }

                    // Input has been validated, so lets connect to the provided multicast address and interface
                    if (Input_Validated == true)
                    {
                        // Syntatically all the provided data is valid, so save it off so it persists over the sessions
                        Properties.Settings.Default.ReplayMulticast = this.txtboxIPAddress.Text;
                        Properties.Settings.Default.ReplayPort = this.textboxPort.Text;
                        Properties.Settings.Default.Save();

                        if (AsterixReplay.LANReplay.Connect(labelSourceFile.Text, IP, Multicast, PortNumber) == true)
                        {
                            this.btnStartPause.Enabled = true;
                            this.btnStartPause.Text = "Start";
                            this.btnConnectDisconnect.Text = "Disconnect";
                            lblBytesSent.Text = "0 Bytes";
                        }
                        else
                        {
                            MessageBox.Show("No connection is possible, please check multicast address and port and interface address");
                        }
                    }

                    break;

                // Anything else just disconnect
                case AsterixReplay.ReplayStatus.Connected:
                case AsterixReplay.ReplayStatus.Paused:
                case AsterixReplay.ReplayStatus.Replaying:
                    AsterixReplay.LANReplay.Disconnect();
                    this.btnStartPause.Text = "Start";
                    this.btnConnectDisconnect.Text = "Connect";
                    this.btnStartPause.Enabled = false;
                    this.progressBar1.Visible = false;
                    ReplayHasCompleted = false;
                    break;
            }
        }

        // Handle PAUSE/START button
        // Enabled only after succefull connection is established and
        // input file opened
        private void btnStartPause_Click(object sender, EventArgs e)
        {
            if ((AsterixReplay.LANReplay.GetCurrentStatus() == AsterixReplay.ReplayStatus.Connected) || (AsterixReplay.LANReplay.GetCurrentStatus() == AsterixReplay.ReplayStatus.Paused))
            {
                AsterixReplay.LANReplay.Start();
                this.btnStartPause.Text = "Pause";
                this.progressBar1.Visible = true;
                timerMonitorReplay.Enabled = true;
            }
            else if (AsterixReplay.LANReplay.GetCurrentStatus() == AsterixReplay.ReplayStatus.Replaying)
            {
                AsterixReplay.LANReplay.Pause();
                this.btnStartPause.Text = "Start";
                this.progressBar1.Visible = false;
                timerMonitorReplay.Enabled = false;
            }
        }

        private void timerMonitorReplay_Tick(object sender, EventArgs e)
        {
            if (ReplayHasCompleted == true)
            {
                this.btnStartPause.Text = "Start";
                this.btnConnectDisconnect.Text = "Connect";
                this.btnStartPause.Enabled = false;
                this.progressBar1.Visible = false;
                timerMonitorReplay.Enabled = false;
                ReplayHasCompleted = false;
            }
            else
            {
                lblBytesSent.Text = AsterixReplay.LANReplay.GetBytesProcessedSoFar().ToString() + " Bytes";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSetConnection_Click(object sender, EventArgs e)
        {
            bool Input_Validated = true;
            IPAddress IP = IPAddress.Any;
            IPAddress Multicast = IPAddress.Any;
            int PortNumber = 2222;


            // First make sure that all boxes are filled out
            if ((!string.IsNullOrEmpty(this.txtboxIPAddress.Text)) &&
                 (!string.IsNullOrEmpty(this.comboBoxNetworkInterface.Text)) &&
                (!string.IsNullOrEmpty(this.textboxPort.Text)))
            {

                // Validate that a valid IP address is entered
                if ((IPAddress.TryParse(this.txtboxIPAddress.Text, out Multicast) != true) || (IPAddress.TryParse(this.comboBoxNetworkInterface.Text, out IP) != true))
                {
                    MessageBox.Show("Not a valid IP address");
                    Input_Validated = false;
                }
                else // Add a check that this is a valid multicast address
                {
                    UdpClient TempSock;
                    TempSock = new UdpClient(2222);// Port does not matter
                    // Open up a new socket with the net IP address and port number   
                    try
                    {
                        TempSock.JoinMulticastGroup(Multicast, 50); // 50 is TTL value
                    }
                    catch
                    {
                        MessageBox.Show("Not valid Multicast address (has to be in range 224.0.0.0 to 239.255.255.255");
                        Input_Validated = false;
                    }
                    if (TempSock != null)
                        TempSock.Close();
                }

                if (int.TryParse(this.textboxPort.Text, out PortNumber) && (PortNumber >= 1 && PortNumber <= 65535))
                {
                }
                else
                {
                    MessageBox.Show("Invalid Port number");
                    Input_Validated = false;
                }
            }
            else
            {
                MessageBox.Show("Please fill out all data fileds");
                Input_Validated = false;
            }


            // Input has been validated, so lets connect to the provided multicast address and interface
            if (Input_Validated == true)
            {
                // Syntatically all the provided data is valid, so save it off so it persists over the sessions
                Properties.Settings.Default.ReplayMulticast = this.txtboxIPAddress.Text;
                Properties.Settings.Default.ReplayPort = this.textboxPort.Text;
                Properties.Settings.Default.Save();

                SharedData.ConnName = "Loc Replay";
                SharedData.CurrentInterfaceIPAddress = this.comboBoxNetworkInterface.Text;
                SharedData.CurrentMulticastAddress = this.txtboxIPAddress.Text;
                SharedData.Current_Port = int.Parse(this.textboxPort.Text);

                if (ASTERIX.ReinitializeSocket() != true)
                {
                    SharedData.ResetConnectionParameters();
                }
            }
        }

        private void ValidateInputConnectionParameters()
        {
            if (this.textboxPort.Text.Length > 0 && this.txtboxIPAddress.Text.Length > 0 && this.comboBoxNetworkInterface.Text.Length > 0)
                this.btnSetConnection.Enabled = true;
            else
                this.btnSetConnection.Enabled = false;
        }
        private void textboxPort_TextChanged(object sender, EventArgs e)
        {
            ValidateInputConnectionParameters();
        }

        private void txtboxIPAddress_TextChanged(object sender, EventArgs e)
        {
            ValidateInputConnectionParameters();
        }

        private void comboBoxNetworkInterface_TextChanged(object sender, EventArgs e)
        {
            ValidateInputConnectionParameters();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
              AsterixReplay.LANReplay.SetReplaySpeed((int)this.numericUpDown1.Value);
        }

        private void FrmReplayForm_VisibleChanged(object sender, EventArgs e)
        {
            comboBoxNetworkInterface.Items.Clear();
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            comboBoxNetworkInterface.Items.Add(ip.Address.ToString());
                        }
                    }
                }
            }
            if (comboBoxNetworkInterface.Items.Count > 0)
                comboBoxNetworkInterface.SelectedIndex = 0;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
