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

namespace AsterixDisplayAnalyser
{
    public partial class FrmReplayForm : Form
    {
        public FrmReplayForm()
        {
            InitializeComponent();
        }

        // Do not provide lower/max/close buttons
        private void ReplayForm_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
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
                this.lblFileSize.Text = new System.IO.FileInfo(openFileDialog1.FileName).Length.ToString() + " Byes";
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
                             (!string.IsNullOrEmpty(this.textBoxInterfaceAddr.Text)) &&
                            (!string.IsNullOrEmpty(this.textboxPort.Text)))
                        {

                            // Validate that a valid IP address is entered
                            if ((IPAddress.TryParse(this.txtboxIPAddress.Text, out Multicast) != true) || (IPAddress.TryParse(this.textBoxInterfaceAddr.Text, out IP) != true))
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
                        if (AsterixReplay.LANReplay.Connect(labelSourceFile.Text, IP, Multicast, PortNumber) == true)
                        {
                            this.btnStartPause.Enabled = true;
                            this.btnStartPause.Text = "Start";
                            this.btnConnectDisconnect.Text = "Disconnect";
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
                    this.btnStartPause.Enabled = false;
                    this.progressBar1.Visible = false;
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
                this.progressBar1.Visible = false;
            }
            else if (AsterixReplay.LANReplay.GetCurrentStatus() == AsterixReplay.ReplayStatus.Replaying)
            {
                AsterixReplay.LANReplay.Pause();
                this.btnStartPause.Text = "Start";
                this.progressBar1.Visible = true;
            }
        }
    }
}
