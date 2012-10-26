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
                    catch
                    {
                        MessageBox.Show("Not valid Multicast address (has to be in range 224.0.0.0 to 239.255.255.255");
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

                UpdateForwardingCheckBoxes();
            }

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

                this.checkedListBoxRecordingName.SelectedIndex = 0;
                UpdateForwardingCheckBoxes();
            }
            // The last thing is to check if there is anything in the list, if so then enable the button
            // to allow setting an active multicast address
            if (this.checkedListBoxRecordingName.Items.Count > 0)
            {
                this.checkedListBoxRecordingName.SelectedIndex = 0;

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

            this.listBoxForwardingInterface.Items.Add(Properties.Settings.Default.FrwdInterface1);
            this.listBoxForwardingInterface.Items.Add(Properties.Settings.Default.FrwdInterface2);
            this.listBoxForwardingInterface.Items.Add(Properties.Settings.Default.FrwdInterface3);
            this.listBoxForwardingInterface.Items.Add(Properties.Settings.Default.FrwdInterface4);
            this.listBoxForwardingInterface.Items.Add(Properties.Settings.Default.FrwdInterface5);

            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast1);
            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast2);
            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast3);
            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast4);
            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast5);

            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort1);
            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort2);
            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort3);
            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort4);
            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort5);
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

                UpdateForwardingCheckBoxes();
            }

            if (checkedListBoxRecordingName.Items.Count < 5)
                this.btnAdd.Enabled = true;
            else
                this.btnAdd.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (RecForwConnection1.IsRecordingEnabled() == true || RecForwConnection1.IsForwardingEnabled() == true)
            {
                this.labelBytes1.Text = RecForwConnection1.GetBytesProcessed().ToString();
            }
            if (RecForwConnection2.IsConnectionActive() == true)
            {
                this.labelBytes2.Text = RecForwConnection2.GetBytesWritten().ToString();
            }
            if (RecForwConnection3.IsConnectionActive() == true)
            {
                this.labelBytes3.Text = RecForwConnection3.GetBytesWritten().ToString();
            }
            if (RecForwConnection4.IsConnectionActive() == true)
            {
                this.labelBytes4.Text = RecForwConnection4.GetBytesWritten().ToString();
            }
            if (RecForwConnection5.IsConnectionActive() == true)
            {
                this.labelBytes5.Text = RecForwConnection5.GetBytesWritten().ToString();
            }
        }

        private void FrmAsterixRecorder_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
                this.timer1.Start();
            else
                this.timer1.Stop();
        }

        private void checkedListBoxRecordingName_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Check if this item is checked
            if (e.NewValue == CheckState.Checked)
            {
                AppendDateTime DateTimeAppend = new AppendDateTime();
                string path_and_name = this.textBoxRecordDirectory.Text + "\\" + DateTimeAppend.ApendDateandTimeToFront(this.checkedListBoxRecordingName.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString() + ".raw");
                switch (this.checkedListBoxRecordingName.SelectedIndex)
                {
                    case 0:
                        if (RecForwConnection1.IsRecordingEnabled() == false)
                        {
                            RecForwConnection1.StartRecording(path_and_name,
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                  int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()));
                            progressBar1.Visible = true;
                            this.checkBoxReplay1.Enabled = false;
                        }
                        break;
                    case 1:
                        if (RecForwConnection2.IsConnectionActive() == false)
                        {
                            RecForwConnection2.StartRecording(path_and_name,
                                 IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                 IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()));
                            progressBar2.Visible = true;
                            this.checkBoxReplay2.Enabled = false;
                        }
                        break;
                    case 2:
                        if (RecForwConnection3.IsConnectionActive() == false)
                        {
                            RecForwConnection3.StartRecording(path_and_name,
                                 IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                 IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()));
                            progressBar3.Visible = true;
                            this.checkBoxReplay3.Enabled = false;
                        }
                        break;
                    case 3:
                        if (RecForwConnection4.IsConnectionActive() == false)
                        {
                            RecForwConnection4.StartRecording(path_and_name,
                                 IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                 IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()));
                            progressBar4.Visible = true;
                            this.checkBoxReplay4.Enabled = false;
                        }
                        break;
                    case 4:
                        if (RecForwConnection5.IsConnectionActive() == false)
                        {
                            RecForwConnection5.StartRecording(path_and_name,
                                 IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                 IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()));
                            progressBar5.Visible = true;
                            this.checkBoxReplay5.Enabled = false;
                        }
                        break;
                    default:
                        break;
                }
            }
            else // No it is not checked
            {
                switch (this.checkedListBoxRecordingName.SelectedIndex)
                {
                    case 0:
                        if (RecForwConnection1.IsRecordingEnabled() == true)
                        {
                            RecForwConnection1.StopRecording();
                            progressBar1.Visible = false;
                            this.checkBoxReplay1.Enabled = true;
                        }
                        break;
                    case 1:
                        if (RecForwConnection2.IsConnectionActive() == true)
                        {
                            RecForwConnection2.StopRecording();
                            progressBar2.Visible = false;
                            this.checkBoxReplay2.Enabled = true;
                        }
                        break;
                    case 2:
                        if (RecForwConnection3.IsConnectionActive() == true)
                        {
                            RecForwConnection3.StopRecording();
                            progressBar3.Visible = false;
                            this.checkBoxReplay3.Enabled = true;
                        }
                        break;
                    case 3:
                        if (RecForwConnection4.IsConnectionActive() == true)
                        {
                            RecForwConnection4.StopRecording();
                            progressBar4.Visible = false;
                            this.checkBoxReplay4.Enabled = true;
                        }
                        break;
                    case 4:
                        if (RecForwConnection5.IsConnectionActive() == true)
                        {
                            RecForwConnection5.StopRecording();
                            progressBar5.Visible = false;
                            this.checkBoxReplay5.Enabled = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string myDocspath = this.textBoxRecordDirectory.Text;
            string windir = Environment.GetEnvironmentVariable("WINDIR");
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = windir + @"\explorer.exe";
            prc.StartInfo.Arguments = myDocspath;
            prc.Start();

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnAddForwarding_Click(object sender, EventArgs e)
        {

            bool ChecForActivePassed = true;
            // First check if forwarding connection is already active
            switch ((int)this.numericUpDown1.Value)
            {
                case 1:
                    if (this.checkBoxF1.Checked)
                        ChecForActivePassed = false;
                    break;
                case 2:
                    if (this.checkBoxF2.Checked)
                        ChecForActivePassed = false;
                    break;
                case 3:
                    if (this.checkBoxF3.Checked)
                        ChecForActivePassed = false;
                    break;
                case 4:
                    if (this.checkBoxF4.Checked)
                        ChecForActivePassed = false;
                    break;
                case 5:
                    if (this.checkBoxF5.Checked)
                        ChecForActivePassed = false;
                    break;
            }

            if (ChecForActivePassed == false)
            {
                MessageBox.Show("Not allowed, connection currently active");
            }
            else
            {
                bool Input_Validated = true;

                // First make sure that all required boxes are filled out
                if ((!string.IsNullOrEmpty(this.txtboxIPAddress.Text)) &&
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
                        catch
                        {
                            MessageBox.Show("Not valid Multicast address (has to be in range 224.0.0.0 to 239.255.255.255");
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
                    this.listBoxForwardingInterface.Items[(int)this.numericUpDown1.Value - 1] = this.textBoxInterfaceAddr.Text;
                    this.listBoxForwardingMulticast.Items[(int)this.numericUpDown1.Value - 1] = this.txtboxIPAddress.Text;
                    this.listBoxForwardingPort.Items[(int)this.numericUpDown1.Value - 1] = this.textboxPort.Text;

                    // Update the properties so the data gets saved accross sessions.
                    switch ((int)this.numericUpDown1.Value)
                    {
                        case 1:
                            Properties.Settings.Default.FrwdInterface1 = this.textBoxInterfaceAddr.Text;
                            Properties.Settings.Default.FrwdMulticast1 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort1 = this.textboxPort.Text;
                            break;
                        case 2: Properties.Settings.Default.FrwdInterface2 = this.textBoxInterfaceAddr.Text;
                            Properties.Settings.Default.FrwdMulticast2 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort2 = this.textboxPort.Text;
                            break;
                        case 3:
                            Properties.Settings.Default.FrwdInterface3 = this.textBoxInterfaceAddr.Text;
                            Properties.Settings.Default.FrwdMulticast3 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort3 = this.textboxPort.Text;
                            break;
                        case 4:
                            Properties.Settings.Default.FrwdInterface4 = this.textBoxInterfaceAddr.Text;
                            Properties.Settings.Default.FrwdMulticast4 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort4 = this.textboxPort.Text;
                            break;
                        case 5:
                            Properties.Settings.Default.FrwdInterface5 = this.textBoxInterfaceAddr.Text;
                            Properties.Settings.Default.FrwdMulticast5 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort5 = this.textboxPort.Text;
                            break;
                    }

                    Properties.Settings.Default.Save();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            bool ChecForActivePassed = true;
            // First check if forwarding connection is already active
            switch ((int)this.numericUpDown1.Value)
            {
                case 1:
                    if (this.checkBoxF1.Checked)
                        ChecForActivePassed = false;
                    break;
                case 2:
                    if (this.checkBoxF2.Checked)
                        ChecForActivePassed = false;
                    break;
                case 3:
                    if (this.checkBoxF3.Checked)
                        ChecForActivePassed = false;
                    break;
                case 4:
                    if (this.checkBoxF4.Checked)
                        ChecForActivePassed = false;
                    break;
                case 5:
                    if (this.checkBoxF5.Checked)
                        ChecForActivePassed = false;
                    break;
            }

            if (ChecForActivePassed == false)
            {
                MessageBox.Show("Not allowed, connection currently active");
            }
            else
            {
                this.listBoxForwardingInterface.Items[(int)this.numericUpDown1.Value - 1] = "None";
                this.listBoxForwardingMulticast.Items[(int)this.numericUpDown1.Value - 1] = "None";
                this.listBoxForwardingPort.Items[(int)this.numericUpDown1.Value - 1] = "None";
            }
        }

        private void UpdateForwardingCheckBoxes()
        {
            this.checkBoxF1.Enabled = false;
            this.checkBoxF2.Enabled = false;
            this.checkBoxF3.Enabled = false;
            this.checkBoxF4.Enabled = false;
            this.checkBoxF5.Enabled = false;

            for (int I = 1; I <= this.checkedListBoxRecordingName.Items.Count; I++)
            {
                switch (I)
                {
                    case 1:
                        this.checkBoxF1.Enabled = true;
                        break;
                    case 2:
                        this.checkBoxF2.Enabled = true;
                        break;
                    case 3:
                        this.checkBoxF3.Enabled = true;
                        break;
                    case 4:
                        this.checkBoxF4.Enabled = true;
                        break;
                    case 5:
                        this.checkBoxF5.Enabled = true;
                        break;
                }

            }
        }
    }
}
