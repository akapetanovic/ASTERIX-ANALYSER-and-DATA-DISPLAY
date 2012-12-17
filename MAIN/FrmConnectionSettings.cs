using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace AsterixDisplayAnalyser
{
    public partial class FrmConnectionSettings : Form
    {
        public FrmConnectionSettings()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool Input_Validated = true;

            // First make sure that all boxes are filled out
            if ((!string.IsNullOrEmpty(this.textBoxConnectionName.Text)) &&
                (!string.IsNullOrEmpty(this.txtboxIPAddress.Text)) &&
                 (!string.IsNullOrEmpty(this.comboBoxNetworkInterface.Text)) &&
                (!string.IsNullOrEmpty(this.textboxPort.Text)))
            {
                IPAddress IP;
                IPAddress Multicast;
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
                    catch (Exception e1)
                    {
                        MessageBox.Show("Not valid Multicast address (has to be in range 224.0.0.0 to 239.255.255.255");

                        // Just to avoid warning that e1 is not used.
                        string temp = e1.Message;
                        Input_Validated = false;
                    }
                    TempSock.Close();
                }

                int PortNumber;
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

            if (Input_Validated == true)
            {
                this.textBoxConnectionName.Text = this.textBoxConnectionName.Text.Replace(' ', '_');
                string ConnInfo = this.textBoxConnectionName.Text;
                this.listBoxConnName.Items.Add(ConnInfo);

                ConnInfo = this.comboBoxNetworkInterface.Text;
                this.listBoxLocalAddr.Items.Add(ConnInfo);
                
                ConnInfo = this.txtboxIPAddress.Text;
                this.listBoxIPAddress.Items.Add(ConnInfo);

                ConnInfo = this.textboxPort.Text;
                this.listBoxPort.Items.Add(ConnInfo);
            }

            // The last thing is to check if there is anything in the list, if so then enable the button
            // to allow setting an active multicast address
            if (this.listBoxConnName.Items.Count > 0)
            {
                this.buttonSetAsActive.Enabled = true;
                this.listBoxConnName.SelectedIndex = this.listBoxConnName.Items.Count - 1;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Code to remove the selected value from the list 
            if (this.listBoxConnName.Items.Count > 0)
            {
                int IndexToDelete = this.listBoxConnName.SelectedIndex;

                this.listBoxConnName.Items.RemoveAt(IndexToDelete);
                this.listBoxLocalAddr.Items.RemoveAt(IndexToDelete);
                this.listBoxIPAddress.Items.RemoveAt(IndexToDelete);
                this.listBoxPort.Items.RemoveAt(IndexToDelete);

                if (this.listBoxConnName.Items.Count > 0)
                {
                    this.listBoxConnName.SelectedIndex = this.listBoxConnName.Items.Count - 1;
                    this.buttonSetAsActive.Enabled = true;
                }
            }

            // The last thing is to check if there is anything in the list, if so then enable the button
            // to allow setting an active multicast address
            if (this.listBoxConnName.Items.Count == 0)
                this.buttonSetAsActive.Enabled = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "ASTERIX Analyser Files|*.astx";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog.OpenFile()) != null)
                {
                    StreamWriter wText = new StreamWriter(myStream);

                    for (int SelectedIndex = 0; SelectedIndex < this.listBoxConnName.Items.Count; SelectedIndex++)
                    {
                        string LineOfData = (string)this.listBoxConnName.Items[SelectedIndex] + " " + (string)this.listBoxLocalAddr.Items[SelectedIndex] + " " + (string)this.listBoxIPAddress.Items[SelectedIndex] + " " +
                            (string)this.listBoxPort.Items[SelectedIndex];
                        wText.WriteLine(LineOfData);
                    }

                    wText.Close();
                    myStream.Close();
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "ASTERIX Analyser Files|*.astx";
            openFileDialog1.InitialDirectory = "Application.StartupPath";
            openFileDialog1.Title = "Open File to Read";

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                // Here first clear the lists
                for (int SelectedIndex = 0; SelectedIndex < this.listBoxConnName.Items.Count; SelectedIndex++)
                {
                    this.listBoxConnName.Items.RemoveAt(SelectedIndex);
                    this.listBoxLocalAddr.Items.RemoveAt(SelectedIndex);
                    this.listBoxIPAddress.Items.RemoveAt(SelectedIndex);
                    this.listBoxPort.Items.RemoveAt(SelectedIndex);
                }

                StreamReader MyStreamReader = new StreamReader(openFileDialog1.FileName);	//Open the input file

                string Path;
                while (MyStreamReader.Peek() >= 0)
                {
                    Path = MyStreamReader.ReadLine();
                    string[] Splited = Path.Split(' ');
                    this.listBoxConnName.Items.Add(Splited[0]);
                    this.listBoxLocalAddr.Items.Add(Splited[1]);
                    this.listBoxIPAddress.Items.Add(Splited[2]);
                    this.listBoxPort.Items.Add(Splited[3]);
                }

                MyStreamReader.Close();
            }
            // The last thing is to check if there is anything in the list, if so then enable the button
            // to allow setting an active multicast address
            if (this.listBoxConnName.Items.Count > 0)
            {
                this.listBoxConnName.SelectedIndex = this.listBoxConnName.Items.Count - 1;
                this.buttonSetAsActive.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetNewConnection();
        }

        private void SetNewConnection()
        {
            SharedData.ConnName = (string)this.listBoxConnName.Items[this.listBoxConnName.SelectedIndex];
            SharedData.CurrentInterfaceIPAddress = (string)this.listBoxLocalAddr.Items[this.listBoxConnName.SelectedIndex];
            SharedData.CurrentMulticastAddress = (string)this.listBoxIPAddress.Items[this.listBoxConnName.SelectedIndex];
            SharedData.Current_Port = int.Parse((string)this.listBoxPort.Items[this.listBoxConnName.SelectedIndex]);

            if (ASTERIX.ReinitializeSocket() != true)
            {
                SharedData.ResetConnectionParameters();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBoxConnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxIPAddress.Items.Count > this.listBoxConnName.SelectedIndex)
                this.listBoxIPAddress.SelectedIndex = this.listBoxConnName.SelectedIndex;

            if (this.listBoxPort.Items.Count > this.listBoxConnName.SelectedIndex)
                this.listBoxPort.SelectedIndex = this.listBoxConnName.SelectedIndex;

            if (this.listBoxLocalAddr.Items.Count > this.listBoxConnName.SelectedIndex)
                this.listBoxLocalAddr.SelectedIndex = this.listBoxConnName.SelectedIndex;

        }

        private void listBoxIPAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxConnName.Items.Count > this.listBoxIPAddress.SelectedIndex)
                this.listBoxConnName.SelectedIndex = this.listBoxIPAddress.SelectedIndex;

            if (this.listBoxPort.Items.Count > this.listBoxIPAddress.SelectedIndex)
                this.listBoxPort.SelectedIndex = this.listBoxIPAddress.SelectedIndex;

            if (this.listBoxLocalAddr.Items.Count > this.listBoxIPAddress.SelectedIndex)
                this.listBoxLocalAddr.SelectedIndex = this.listBoxIPAddress.SelectedIndex;
        }

        private void listBoxPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxConnName.Items.Count > this.listBoxPort.SelectedIndex)
                this.listBoxConnName.SelectedIndex = this.listBoxPort.SelectedIndex;

            if (this.listBoxIPAddress.Items.Count > this.listBoxPort.SelectedIndex)
                this.listBoxIPAddress.SelectedIndex = this.listBoxPort.SelectedIndex;

            if (this.listBoxLocalAddr.Items.Count > this.listBoxPort.SelectedIndex)
                this.listBoxLocalAddr.SelectedIndex = this.listBoxPort.SelectedIndex;
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
          
        }

        private void FrmSettings_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void listBoxLocalAddr_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.listBoxConnName.Items.Count > this.listBoxLocalAddr.SelectedIndex)
                this.listBoxConnName.SelectedIndex = this.listBoxLocalAddr.SelectedIndex;

            if (this.listBoxIPAddress.Items.Count > this.listBoxLocalAddr.SelectedIndex)
                this.listBoxIPAddress.SelectedIndex = this.listBoxLocalAddr.SelectedIndex;

            if (this.listBoxPort.Items.Count > this.listBoxLocalAddr.SelectedIndex)
                this.listBoxPort.SelectedIndex = this.listBoxLocalAddr.SelectedIndex;
        }

        private void FrmConnectionSettings_VisibleChanged(object sender, EventArgs e)
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
    }
}
