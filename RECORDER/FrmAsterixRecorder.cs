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

namespace AsterixDisplayAnalyser
{
    public partial class FrmAsterixRecorder : Form
    {
        public FrmAsterixRecorder()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool Input_Validated = true;

            // First make sure that all boxes are filled out
            if ((!string.IsNullOrEmpty(this.textBoxConnectionName.Text)) &&
                (!string.IsNullOrEmpty(this.txtboxIPAddress.Text)) &&
                 (!string.IsNullOrEmpty(this.textBoxInterfaceAddr.Text)) &&
                (!string.IsNullOrEmpty(this.textboxPort.Text)))
            {
                IPAddress IP;
                IPAddress Multicast;
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
                this.checkedListBoxRecordingName.Items.Add(ConnInfo);

                ConnInfo = this.textBoxInterfaceAddr.Text;
                this.listBoxLocalAddr.Items.Add(ConnInfo);

                ConnInfo = this.txtboxIPAddress.Text;
                this.listBoxIPAddress.Items.Add(ConnInfo);

                ConnInfo = this.textboxPort.Text;
                this.listBoxPort.Items.Add(ConnInfo);

                ConnInfo = this.textBoxConnectionName.Text;
                this.checkedListBoxRecordingName.Items.Add(ConnInfo);
            }

            // The last thing is to check if there is anything in the list, if so then enable the button
            // to allow setting an active multicast address
            if (this.checkedListBoxRecordingName.Items.Count > 0)
            {
                this.checkedListBoxRecordingName.SelectedIndex = this.checkedListBoxRecordingName.Items.Count - 1;
            }

            if (checkedListBoxRecordingName.Items.Count == 5)
                this.btnAdd.Enabled = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Code to remove the selected value from the list 
            if (this.checkedListBoxRecordingName.Items.Count > 0)
            {
                int IndexToDelete = this.checkedListBoxRecordingName.SelectedIndex;

                this.checkedListBoxRecordingName.Items.RemoveAt(IndexToDelete);
                this.listBoxLocalAddr.Items.RemoveAt(IndexToDelete);
                this.listBoxIPAddress.Items.RemoveAt(IndexToDelete);
                this.listBoxPort.Items.RemoveAt(IndexToDelete);

                if (this.checkedListBoxRecordingName.Items.Count > 0)
                {
                    this.checkedListBoxRecordingName.SelectedIndex = this.checkedListBoxRecordingName.Items.Count - 1;

                }
            }
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

                    for (int SelectedIndex = 0; SelectedIndex < this.checkedListBoxRecordingName.Items.Count; SelectedIndex++)
                    {
                        string LineOfData = (string)this.checkedListBoxRecordingName.Items[SelectedIndex] + " " + (string)this.listBoxLocalAddr.Items[SelectedIndex] + " " + (string)this.listBoxIPAddress.Items[SelectedIndex] + " " +
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
                for (int SelectedIndex = 0; SelectedIndex < this.checkedListBoxRecordingName.Items.Count; SelectedIndex++)
                {
                    this.checkedListBoxRecordingName.Items.RemoveAt(SelectedIndex);
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
                    this.checkedListBoxRecordingName.Items.Add(Splited[0]);
                    this.listBoxLocalAddr.Items.Add(Splited[1]);
                    this.listBoxIPAddress.Items.Add(Splited[2]);
                    this.listBoxPort.Items.Add(Splited[3]);

                    if (checkedListBoxRecordingName.Items.Count == 5)
                    {
                        this.btnAdd.Enabled = false;
                        break;
                    }
                }

                MyStreamReader.Close();
            }
            // The last thing is to check if there is anything in the list, if so then enable the button
            // to allow setting an active multicast address
            if (this.checkedListBoxRecordingName.Items.Count > 0)
            {
                this.checkedListBoxRecordingName.SelectedIndex = this.checkedListBoxRecordingName.Items.Count - 1;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
        }

        private void listBoxIPAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.checkedListBoxRecordingName.Items.Count > this.listBoxIPAddress.SelectedIndex)
                this.checkedListBoxRecordingName.SelectedIndex = this.listBoxIPAddress.SelectedIndex;

            if (this.listBoxPort.Items.Count > this.listBoxIPAddress.SelectedIndex)
                this.listBoxPort.SelectedIndex = this.listBoxIPAddress.SelectedIndex;

            if (this.listBoxLocalAddr.Items.Count > this.listBoxIPAddress.SelectedIndex)
                this.listBoxLocalAddr.SelectedIndex = this.listBoxIPAddress.SelectedIndex;
        }

        private void listBoxPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.checkedListBoxRecordingName.Items.Count > this.listBoxPort.SelectedIndex)
                this.checkedListBoxRecordingName.SelectedIndex = this.listBoxPort.SelectedIndex;

            if (this.listBoxIPAddress.Items.Count > this.listBoxPort.SelectedIndex)
                this.listBoxIPAddress.SelectedIndex = this.listBoxPort.SelectedIndex;

            if (this.listBoxLocalAddr.Items.Count > this.listBoxPort.SelectedIndex)
                this.listBoxLocalAddr.SelectedIndex = this.listBoxPort.SelectedIndex;
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.textBoxRecordDirectory.Text = Properties.Settings.Default.RecordingDirectory;

        }

        private void FrmSettings_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void listBoxLocalAddr_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.checkedListBoxRecordingName.Items.Count > this.listBoxLocalAddr.SelectedIndex)
                this.checkedListBoxRecordingName.SelectedIndex = this.listBoxLocalAddr.SelectedIndex;

            if (this.listBoxIPAddress.Items.Count > this.listBoxLocalAddr.SelectedIndex)
                this.listBoxIPAddress.SelectedIndex = this.listBoxLocalAddr.SelectedIndex;

            if (this.listBoxPort.Items.Count > this.listBoxLocalAddr.SelectedIndex)
                this.listBoxPort.SelectedIndex = this.listBoxLocalAddr.SelectedIndex;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBoxRecordingName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxIPAddress.Items.Count > this.checkedListBoxRecordingName.SelectedIndex)
                this.listBoxIPAddress.SelectedIndex = this.checkedListBoxRecordingName.SelectedIndex;

            if (this.listBoxPort.Items.Count > this.checkedListBoxRecordingName.SelectedIndex)
                this.listBoxPort.SelectedIndex = this.checkedListBoxRecordingName.SelectedIndex;

            if (this.listBoxLocalAddr.Items.Count > this.checkedListBoxRecordingName.SelectedIndex)
                this.listBoxLocalAddr.SelectedIndex = this.checkedListBoxRecordingName.SelectedIndex;

            // Check if this item is checked
            if (this.checkedListBoxRecordingName.GetItemChecked(this.checkedListBoxRecordingName.SelectedIndex))
            {

            }
            else // No it is not checked
            {

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();

            if (FBD.ShowDialog() == DialogResult.OK)
            {
                this.textBoxRecordDirectory.Text = FBD.SelectedPath;
                Properties.Settings.Default.RecordingDirectory = this.textBoxRecordDirectory.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Code to remove the selected value from the list 
            if (this.checkedListBoxRecordingName.Items.Count > 0)
            {
                int IndexToDelete = this.checkedListBoxRecordingName.SelectedIndex;

                this.checkedListBoxRecordingName.Items.RemoveAt(IndexToDelete);
                this.listBoxLocalAddr.Items.RemoveAt(IndexToDelete);
                this.listBoxIPAddress.Items.RemoveAt(IndexToDelete);
                this.listBoxPort.Items.RemoveAt(IndexToDelete);

                if (this.checkedListBoxRecordingName.Items.Count > 0)
                {
                    this.checkedListBoxRecordingName.SelectedIndex = this.checkedListBoxRecordingName.Items.Count - 1;
                }
            }

            if (checkedListBoxRecordingName.Items.Count < 5)
                this.btnAdd.Enabled = true;
            else
                this.btnAdd.Enabled = false;
        }
    }
}
