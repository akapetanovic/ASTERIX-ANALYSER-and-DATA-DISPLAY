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
    public partial class FrmAstxRecFrwdForm : Form
    {
        public FrmAstxRecFrwdForm()
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

                ConnInfo = this.comboBoxNetworkInterface.Text;
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

            if (checkedListBoxRecordingName.Items.Count == 10)
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
            this.listBoxForwardingInterface.Items.Add(Properties.Settings.Default.FrwdInterface6);
            this.listBoxForwardingInterface.Items.Add(Properties.Settings.Default.FrwdInterface7);
            this.listBoxForwardingInterface.Items.Add(Properties.Settings.Default.FrwdInterface8);
            this.listBoxForwardingInterface.Items.Add(Properties.Settings.Default.FrwdInterface9);
            this.listBoxForwardingInterface.Items.Add(Properties.Settings.Default.FrwdInterface10);

            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast1);
            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast2);
            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast3);
            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast4);
            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast5);
            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast6);
            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast7);
            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast8);
            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast9);
            this.listBoxForwardingMulticast.Items.Add(Properties.Settings.Default.FrwdMulticast10);

            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort1);
            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort2);
            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort3);
            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort4);
            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort5);
            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort6);
            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort7);
            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort8);
            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort9);
            this.listBoxForwardingPort.Items.Add(Properties.Settings.Default.FrwdPort10);

            this.listBoxForwardingInterface.SelectedIndex = 0;
            this.listBoxForwardingMulticast.SelectedIndex = 0;
            this.listBoxForwardingPort.SelectedIndex = 0;
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
            if (RecForwConnection2.IsRecordingEnabled() == true || RecForwConnection2.IsForwardingEnabled() == true)
            {
                this.labelBytes2.Text = RecForwConnection2.GetBytesProcessed().ToString();
            }
            if (RecForwConnection3.IsRecordingEnabled() == true || RecForwConnection3.IsForwardingEnabled() == true)
            {
                this.labelBytes3.Text = RecForwConnection3.GetBytesProcessed().ToString();
            }
            if (RecForwConnection4.IsRecordingEnabled() == true || RecForwConnection4.IsForwardingEnabled() == true)
            {
                this.labelBytes4.Text = RecForwConnection4.GetBytesProcessed().ToString();
            }
            if (RecForwConnection5.IsRecordingEnabled() == true || RecForwConnection5.IsForwardingEnabled() == true)
            {
                this.labelBytes5.Text = RecForwConnection5.GetBytesProcessed().ToString();
            }
            if (RecForwConnection6.IsRecordingEnabled() == true || RecForwConnection6.IsForwardingEnabled() == true)
            {
                this.labelBytes6.Text = RecForwConnection6.GetBytesProcessed().ToString();
            }
            if (RecForwConnection7.IsRecordingEnabled() == true || RecForwConnection7.IsForwardingEnabled() == true)
            {
                this.labelBytes7.Text = RecForwConnection7.GetBytesProcessed().ToString();
            }
            if (RecForwConnection8.IsRecordingEnabled() == true || RecForwConnection8.IsForwardingEnabled() == true)
            {
                this.labelBytes8.Text = RecForwConnection8.GetBytesProcessed().ToString();
            }
            if (RecForwConnection9.IsRecordingEnabled() == true || RecForwConnection9.IsForwardingEnabled() == true)
            {
                this.labelBytes9.Text = RecForwConnection9.GetBytesProcessed().ToString();
            }
            if (RecForwConnection10.IsRecordingEnabled() == true || RecForwConnection10.IsForwardingEnabled() == true)
            {
                this.labelBytes10.Text = RecForwConnection10.GetBytesProcessed().ToString();
            }
        }

        private void FrmAsterixRecorder_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
                this.timer1.Start();
            else
                this.timer1.Stop();

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

        private string GetFileExtension(bool IsReplay)
        {
            if (IsReplay)
                return ".rply";
            else
                return ".raw";
        }

        private void checkedListBoxRecordingName_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Check if this item is checked
            if (e.NewValue == CheckState.Checked)
            {
                AppendDateTime DateTimeAppend = new AppendDateTime();
                string path_and_name = this.textBoxRecordDirectory.Text + "\\" + DateTimeAppend.ApendDateandTimeToFront(this.checkedListBoxRecordingName.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString());

                switch (this.checkedListBoxRecordingName.SelectedIndex)
                {
                    case 0:
                        if (RecForwConnection1.IsRecordingEnabled() == false)
                        {
                            if (RecForwConnection1.StartRecording(!this.chkBoxReplayFormatEnabled1.Checked,
                                   path_and_name + GetFileExtension(!this.chkBoxReplayFormatEnabled1.Checked),
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                  int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString())) == false)
                            {
                                this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                            }
                            else
                            {
                                progressBar1.Visible = true;
                                this.chkBoxReplayFormatEnabled1.Enabled = false;
                            }

                        }
                        break;
                    case 1:
                        if (RecForwConnection2.IsRecordingEnabled() == false)
                        {
                            if (RecForwConnection2.StartRecording(!this.chkBoxReplayFormatEnabled2.Checked,
                                path_and_name + GetFileExtension(!this.chkBoxReplayFormatEnabled2.Checked),
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                  int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString())) == false)
                            {
                                this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                            }
                            else
                            {
                                progressBar2.Visible = true;
                                this.chkBoxReplayFormatEnabled2.Enabled = false;
                            }

                        }
                        break;
                    case 2:
                        if (RecForwConnection3.IsRecordingEnabled() == false)
                        {
                            if (RecForwConnection3.StartRecording(!this.chkBoxReplayFormatEnabled3.Checked,
                                path_and_name + GetFileExtension(!this.chkBoxReplayFormatEnabled3.Checked),
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                  int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString())) == false)
                            {
                                this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                            }
                            else
                            {
                                progressBar3.Visible = true;
                                this.chkBoxReplayFormatEnabled3.Enabled = false;
                            }

                        }
                        break;
                    case 3:
                        if (RecForwConnection4.IsRecordingEnabled() == false)
                        {
                            if (RecForwConnection4.StartRecording(!this.chkBoxReplayFormatEnabled4.Checked,
                                path_and_name + GetFileExtension(!this.chkBoxReplayFormatEnabled4.Checked),
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                  int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString())) == false)
                            {
                                this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                            }
                            else
                            {
                                progressBar4.Visible = true;
                                this.chkBoxReplayFormatEnabled4.Enabled = false;
                            }

                        }
                        break;
                    case 4:
                        if (RecForwConnection5.IsRecordingEnabled() == false)
                        {
                            if (RecForwConnection5.StartRecording(!this.chkBoxReplayFormatEnabled5.Checked,
                                path_and_name + GetFileExtension(!this.chkBoxReplayFormatEnabled5.Checked),
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                  int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString())) == false)
                            {
                                this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                            }
                            else
                            {
                                progressBar5.Visible = true;
                                this.chkBoxReplayFormatEnabled5.Enabled = false;
                            }

                        }
                        break;

                    case 5:
                        if (RecForwConnection6.IsRecordingEnabled() == false)
                        {
                            if (RecForwConnection6.StartRecording(!this.chkBoxReplayFormatEnabled6.Checked,
                                path_and_name + GetFileExtension(!this.chkBoxReplayFormatEnabled6.Checked),
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                  int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString())) == false)
                            {
                                this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                            }
                            else
                            {
                                progressBar6.Visible = true;
                                this.chkBoxReplayFormatEnabled6.Enabled = false;
                            }

                        }
                        break;
                    case 6:
                        if (RecForwConnection7.IsRecordingEnabled() == false)
                        {
                            if (RecForwConnection7.StartRecording(!this.chkBoxReplayFormatEnabled7.Checked,
                                path_and_name + GetFileExtension(!this.chkBoxReplayFormatEnabled7.Checked),
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                  int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString())) == false)
                            {
                                this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                            }
                            else
                            {
                                progressBar7.Visible = true;
                                this.chkBoxReplayFormatEnabled6.Enabled = false;
                            }

                        }
                        break;
                    case 7:
                        if (RecForwConnection8.IsRecordingEnabled() == false)
                        {
                            if (RecForwConnection8.StartRecording(!this.chkBoxReplayFormatEnabled8.Checked,
                                path_and_name + GetFileExtension(!this.chkBoxReplayFormatEnabled8.Checked),
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                  int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString())) == false)
                            {
                                this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                            }
                            else
                            {
                                progressBar8.Visible = true;
                                this.chkBoxReplayFormatEnabled8.Enabled = false;
                            }

                        }
                        break;
                    case 8:
                        if (RecForwConnection9.IsRecordingEnabled() == false)
                        {
                            if (RecForwConnection9.StartRecording(!this.chkBoxReplayFormatEnabled9.Checked,
                                path_and_name + GetFileExtension(!this.chkBoxReplayFormatEnabled9.Checked),
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                  int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString())) == false)
                            {
                                this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                            }
                            else
                            {
                                progressBar9.Visible = true;
                                this.chkBoxReplayFormatEnabled9.Enabled = false;
                            }

                        }
                        break;
                    case 9:
                        if (RecForwConnection10.IsRecordingEnabled() == false)
                        {
                            if (RecForwConnection10.StartRecording(!this.chkBoxReplayFormatEnabled10.Checked,
                                path_and_name + GetFileExtension(!this.chkBoxReplayFormatEnabled10.Checked),
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString()),
                                  int.Parse(this.listBoxPort.Items[this.checkedListBoxRecordingName.SelectedIndex].ToString())) == false)
                            {
                                this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                            }
                            else
                            {
                                progressBar10.Visible = true;
                                this.chkBoxReplayFormatEnabled10.Enabled = false;
                            }

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
                            this.chkBoxReplayFormatEnabled1.Enabled = true;
                        }
                        break;
                    case 1:
                        if (RecForwConnection2.IsRecordingEnabled() == true)
                        {
                            RecForwConnection2.StopRecording();
                            progressBar2.Visible = false;
                            this.chkBoxReplayFormatEnabled2.Enabled = true;
                        }
                        break;
                    case 2:
                        if (RecForwConnection3.IsRecordingEnabled() == true)
                        {
                            RecForwConnection3.StopRecording();
                            progressBar3.Visible = false;
                            this.chkBoxReplayFormatEnabled3.Enabled = true;
                        }
                        break;
                    case 3:
                        if (RecForwConnection4.IsRecordingEnabled() == true)
                        {
                            RecForwConnection4.StopRecording();
                            progressBar4.Visible = false;
                            this.chkBoxReplayFormatEnabled4.Enabled = true;
                        }
                        break;
                    case 4:
                        if (RecForwConnection5.IsRecordingEnabled() == true)
                        {
                            RecForwConnection5.StopRecording();
                            progressBar5.Visible = false;
                            this.chkBoxReplayFormatEnabled5.Enabled = true;
                        }
                        break;

                    case 5:
                        if (RecForwConnection6.IsRecordingEnabled() == true)
                        {
                            RecForwConnection6.StopRecording();
                            progressBar6.Visible = false;
                            this.chkBoxReplayFormatEnabled6.Enabled = true;
                        }
                        break;
                    case 6:
                        if (RecForwConnection7.IsRecordingEnabled() == true)
                        {
                            RecForwConnection7.StopRecording();
                            progressBar7.Visible = false;
                            this.chkBoxReplayFormatEnabled7.Enabled = true;
                        }
                        break;
                    case 7:
                        if (RecForwConnection8.IsRecordingEnabled() == true)
                        {
                            RecForwConnection8.StopRecording();
                            progressBar8.Visible = false;
                            this.chkBoxReplayFormatEnabled8.Enabled = true;
                        }
                        break;
                    case 8:
                        if (RecForwConnection9.IsRecordingEnabled() == true)
                        {
                            RecForwConnection9.StopRecording();
                            progressBar9.Visible = false;
                            this.chkBoxReplayFormatEnabled9.Enabled = true;
                        }
                        break;
                    case 9:
                        if (RecForwConnection10.IsRecordingEnabled() == true)
                        {
                            RecForwConnection10.StopRecording();
                            progressBar10.Visible = false;
                            this.chkBoxReplayFormatEnabled10.Enabled = true;
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
                case 6:
                    if (this.checkBoxF6.Checked)
                        ChecForActivePassed = false;
                    break;
                case 7:
                    if (this.checkBoxF7.Checked)
                        ChecForActivePassed = false;
                    break;
                case 8:
                    if (this.checkBoxF8.Checked)
                        ChecForActivePassed = false;
                    break;
                case 9:
                    if (this.checkBoxF9.Checked)
                        ChecForActivePassed = false;
                    break;
                case 10:
                    if (this.checkBoxF10.Checked)
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
                    this.listBoxForwardingInterface.Items[(int)this.numericUpDown1.Value - 1] = this.comboBoxNetworkInterface.Text;
                    this.listBoxForwardingMulticast.Items[(int)this.numericUpDown1.Value - 1] = this.txtboxIPAddress.Text;
                    this.listBoxForwardingPort.Items[(int)this.numericUpDown1.Value - 1] = this.textboxPort.Text;

                    // Update the properties so the data gets saved accross sessions.
                    switch ((int)this.numericUpDown1.Value)
                    {
                        case 1:
                            Properties.Settings.Default.FrwdInterface1 = this.comboBoxNetworkInterface.Text;
                            Properties.Settings.Default.FrwdMulticast1 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort1 = this.textboxPort.Text;
                            break;
                        case 2: Properties.Settings.Default.FrwdInterface2 = this.comboBoxNetworkInterface.Text;
                            Properties.Settings.Default.FrwdMulticast2 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort2 = this.textboxPort.Text;
                            break;
                        case 3:
                            Properties.Settings.Default.FrwdInterface3 = this.comboBoxNetworkInterface.Text;
                            Properties.Settings.Default.FrwdMulticast3 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort3 = this.textboxPort.Text;
                            break;
                        case 4:
                            Properties.Settings.Default.FrwdInterface4 = this.comboBoxNetworkInterface.Text;
                            Properties.Settings.Default.FrwdMulticast4 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort4 = this.textboxPort.Text;
                            break;
                        case 5:
                            Properties.Settings.Default.FrwdInterface5 = this.comboBoxNetworkInterface.Text;
                            Properties.Settings.Default.FrwdMulticast5 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort5 = this.textboxPort.Text;
                            break;
                        case 6:
                            Properties.Settings.Default.FrwdInterface6 = this.comboBoxNetworkInterface.Text;
                            Properties.Settings.Default.FrwdMulticast6 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort6 = this.textboxPort.Text;
                            break;
                        case 7: Properties.Settings.Default.FrwdInterface7 = this.comboBoxNetworkInterface.Text;
                            Properties.Settings.Default.FrwdMulticast7 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort7 = this.textboxPort.Text;
                            break;
                        case 8:
                            Properties.Settings.Default.FrwdInterface8 = this.comboBoxNetworkInterface.Text;
                            Properties.Settings.Default.FrwdMulticast8 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort8 = this.textboxPort.Text;
                            break;
                        case 9:
                            Properties.Settings.Default.FrwdInterface9 = this.comboBoxNetworkInterface.Text;
                            Properties.Settings.Default.FrwdMulticast9 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort9 = this.textboxPort.Text;
                            break;
                        case 10:
                            Properties.Settings.Default.FrwdInterface10 = this.comboBoxNetworkInterface.Text;
                            Properties.Settings.Default.FrwdMulticast10 = this.txtboxIPAddress.Text;
                            Properties.Settings.Default.FrwdPort10 = this.textboxPort.Text;
                            break;
                    }

                    Properties.Settings.Default.Save();

                    this.listBoxForwardingInterface.SelectedIndex = (int)this.numericUpDown1.Value - 1;
                    this.listBoxForwardingMulticast.SelectedIndex = (int)this.numericUpDown1.Value - 1;
                    this.listBoxForwardingPort.SelectedIndex = (int)this.numericUpDown1.Value - 1;

                    UpdateForwardingCheckBoxes();
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
                case 6:
                    if (this.checkBoxF6.Checked)
                        ChecForActivePassed = false;
                    break;
                case 7:
                    if (this.checkBoxF7.Checked)
                        ChecForActivePassed = false;
                    break;
                case 8:
                    if (this.checkBoxF8.Checked)
                        ChecForActivePassed = false;
                    break;
                case 9:
                    if (this.checkBoxF9.Checked)
                        ChecForActivePassed = false;
                    break;
                case 10:
                    if (this.checkBoxF10.Checked)
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
                UpdateForwardingCheckBoxes();
            }
        }

        private void UpdateForwardingCheckBoxes()
        {
            this.checkBoxF1.Enabled = false;
            this.checkBoxF2.Enabled = false;
            this.checkBoxF3.Enabled = false;
            this.checkBoxF4.Enabled = false;
            this.checkBoxF5.Enabled = false;
            this.checkBoxF6.Enabled = false;
            this.checkBoxF7.Enabled = false;
            this.checkBoxF8.Enabled = false;
            this.checkBoxF9.Enabled = false;
            this.checkBoxF10.Enabled = false;

            for (int I = 0; I < this.checkedListBoxRecordingName.Items.Count; I++)
            {
                switch (I)
                {
                    case 0:
                        if ((string)listBoxForwardingInterface.Items[0] != "None")
                            this.checkBoxF1.Enabled = true;
                        break;
                    case 1:
                        if ((string)listBoxForwardingInterface.Items[1] != "None")
                            this.checkBoxF2.Enabled = true;
                        break;
                    case 2:
                        if ((string)listBoxForwardingInterface.Items[2] != "None")
                            this.checkBoxF3.Enabled = true;
                        break;
                    case 3:
                        if ((string)listBoxForwardingInterface.Items[3] != "None")
                            this.checkBoxF4.Enabled = true;
                        break;
                    case 4:
                        if ((string)listBoxForwardingInterface.Items[4] != "None")
                            this.checkBoxF5.Enabled = true;
                        break;
                    case 5:
                        if ((string)listBoxForwardingInterface.Items[5] != "None")
                            this.checkBoxF6.Enabled = true;
                        break;
                    case 6:
                        if ((string)listBoxForwardingInterface.Items[6] != "None")
                            this.checkBoxF7.Enabled = true;
                        break;
                    case 7:
                        if ((string)listBoxForwardingInterface.Items[7] != "None")
                            this.checkBoxF8.Enabled = true;
                        break;
                    case 8:
                        if ((string)listBoxForwardingInterface.Items[8] != "None")
                            this.checkBoxF9.Enabled = true;
                        break;
                    case 9:
                        if ((string)listBoxForwardingInterface.Items[9] != "None")
                            this.checkBoxF10.Enabled = true;
                        break;
                }

            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.listBoxForwardingInterface.SelectedIndex = (int)this.numericUpDown1.Value - 1;
            this.listBoxForwardingMulticast.SelectedIndex = (int)this.numericUpDown1.Value - 1;
            this.listBoxForwardingPort.SelectedIndex = (int)this.numericUpDown1.Value - 1;
        }

        private void listBoxForwardingInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBoxForwardingMulticast.SelectedIndex = this.listBoxForwardingInterface.SelectedIndex;
            this.listBoxForwardingPort.SelectedIndex = this.listBoxForwardingInterface.SelectedIndex;
            if (this.listBoxForwardingInterface.SelectedIndex != -1)
                this.numericUpDown1.Value = this.listBoxForwardingInterface.SelectedIndex + 1;
        }

        private void listBoxForwardingMulticast_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBoxForwardingInterface.SelectedIndex = this.listBoxForwardingMulticast.SelectedIndex;
            this.listBoxForwardingPort.SelectedIndex = this.listBoxForwardingMulticast.SelectedIndex;
            if (this.listBoxForwardingMulticast.SelectedIndex != -1)
                this.numericUpDown1.Value = this.listBoxForwardingMulticast.SelectedIndex + 1;
        }

        private void listBoxForwardingPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBoxForwardingInterface.SelectedIndex = this.listBoxForwardingPort.SelectedIndex;
            this.listBoxForwardingMulticast.SelectedIndex = this.listBoxForwardingPort.SelectedIndex;
            if (this.listBoxForwardingPort.SelectedIndex != -1)
                this.numericUpDown1.Value = this.listBoxForwardingPort.SelectedIndex + 1;
        }

        private void checkBoxF1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxF1.Checked == true)
            {
                if (RecForwConnection1.StartForward(
                    //Source parameters (always passed in, however the connection manager will use it just in the case the
                    // connection is not active already)
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[0].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[0].ToString()),
                                  int.Parse(this.listBoxPort.Items[0].ToString()),
                    // Forward parameters
                                  IPAddress.Parse(this.listBoxForwardingInterface.Items[0].ToString()),
                                   IPAddress.Parse(this.listBoxForwardingMulticast.Items[0].ToString()),
                                  int.Parse(this.listBoxForwardingPort.Items[0].ToString())) == false)
                {
                    this.checkBoxF1.Checked = false;
                }
                else
                {
                    this.progressBarF1.Visible = true;
                }

            }
            else
            {
                RecForwConnection1.StopForwarding();
                this.progressBarF1.Visible = false;
            }
        }

        private void checkedListBoxRecordingName_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (checkedListBoxRecordingName.SelectedIndex)
            {
                case 0:
                    if (progressBar1.Visible == false)
                        this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                    break;
                case 1:
                    if (progressBar2.Visible == false)
                        this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                    break;
                case 2:
                    if (progressBar3.Visible == false)
                        this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                    break;
                case 3:
                    if (progressBar4.Visible == false)
                        this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                    break;
                case 4:
                    if (progressBar5.Visible == false)
                        this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                    break;
                case 5:
                    if (progressBar6.Visible == false)
                        this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                    break;
                case 6:
                    if (progressBar7.Visible == false)
                        this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                    break;
                case 7:
                    if (progressBar8.Visible == false)
                        this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                    break;
                case 8:
                    if (progressBar9.Visible == false)
                        this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                    break;
                case 9:
                    if (progressBar10.Visible == false)
                        this.checkedListBoxRecordingName.SetItemCheckState(this.checkedListBoxRecordingName.SelectedIndex, CheckState.Unchecked);
                    break;
            }
        }

        private void checkBoxF2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxF2.Checked == true)
            {
                if (RecForwConnection2.StartForward(
                    //Source parameters (always passed in, however the connection manager will use it just in the case the
                    // connection is not active already)
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[1].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[1].ToString()),
                                  int.Parse(this.listBoxPort.Items[1].ToString()),
                    // Forward parameters
                                  IPAddress.Parse(this.listBoxForwardingInterface.Items[1].ToString()),
                                   IPAddress.Parse(this.listBoxForwardingMulticast.Items[1].ToString()),
                                  int.Parse(this.listBoxForwardingPort.Items[1].ToString())) == false)
                {
                    this.checkBoxF2.Checked = false;
                }
                else
                {
                    this.progressBarF2.Visible = true;
                }

            }
            else
            {
                RecForwConnection2.StopForwarding();
                this.progressBarF2.Visible = false;
            }
        }

        private void checkBoxF3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxF3.Checked == true)
            {
                if (RecForwConnection3.StartForward(
                    //Source parameters (always passed in, however the connection manager will use it just in the case the
                    // connection is not active already)
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[2].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[2].ToString()),
                                  int.Parse(this.listBoxPort.Items[2].ToString()),
                    // Forward parameters
                                  IPAddress.Parse(this.listBoxForwardingInterface.Items[2].ToString()),
                                   IPAddress.Parse(this.listBoxForwardingMulticast.Items[2].ToString()),
                                  int.Parse(this.listBoxForwardingPort.Items[2].ToString())) == false)
                {
                    this.checkBoxF3.Checked = false;
                }
                else
                {
                    this.progressBarF3.Visible = true;
                }

            }
            else
            {
                RecForwConnection3.StopForwarding();
                this.progressBarF3.Visible = false;
            }
        }

        private void checkBoxF4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxF4.Checked == true)
            {
                if (RecForwConnection4.StartForward(
                    //Source parameters (always passed in, however the connection manager will use it just in the case the
                    // connection is not active already)
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[3].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[3].ToString()),
                                  int.Parse(this.listBoxPort.Items[3].ToString()),
                    // Forward parameters
                                  IPAddress.Parse(this.listBoxForwardingInterface.Items[3].ToString()),
                                   IPAddress.Parse(this.listBoxForwardingMulticast.Items[3].ToString()),
                                  int.Parse(this.listBoxForwardingPort.Items[3].ToString())) == false)
                {
                    this.checkBoxF4.Checked = false;
                }
                else
                {
                    this.progressBarF4.Visible = true;
                }

            }
            else
            {
                RecForwConnection4.StopForwarding();
                this.progressBarF4.Visible = false;
            }
        }

        private void checkBoxF5_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxF5.Checked == true)
            {
                if (RecForwConnection5.StartForward(
                    //Source parameters (always passed in, however the connection manager will use it just in the case the
                    // connection is not active already)
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[4].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[4].ToString()),
                                  int.Parse(this.listBoxPort.Items[4].ToString()),
                    // Forward parameters
                                  IPAddress.Parse(this.listBoxForwardingInterface.Items[4].ToString()),
                                   IPAddress.Parse(this.listBoxForwardingMulticast.Items[4].ToString()),
                                  int.Parse(this.listBoxForwardingPort.Items[4].ToString())) == false)
                {
                    this.checkBoxF5.Checked = false;
                }
                else
                {
                    this.progressBarF5.Visible = true;
                }

            }
            else
            {
                RecForwConnection5.StopForwarding();
                this.progressBarF5.Visible = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void checkBoxF6_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxF6.Checked == true)
            {
                if (RecForwConnection6.StartForward(
                    //Source parameters (always passed in, however the connection manager will use it just in the case the
                    // connection is not active already)
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[5].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[5].ToString()),
                                  int.Parse(this.listBoxPort.Items[5].ToString()),
                    // Forward parameters
                                  IPAddress.Parse(this.listBoxForwardingInterface.Items[5].ToString()),
                                   IPAddress.Parse(this.listBoxForwardingMulticast.Items[5].ToString()),
                                  int.Parse(this.listBoxForwardingPort.Items[5].ToString())) == false)
                {
                    this.checkBoxF6.Checked = false;
                }
                else
                {
                    this.progressBarF6.Visible = true;
                }

            }
            else
            {
                RecForwConnection6.StopForwarding();
                this.progressBarF6.Visible = false;
            }
        }

        private void checkBoxF7_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxF7.Checked == true)
            {
                if (RecForwConnection7.StartForward(
                    //Source parameters (always passed in, however the connection manager will use it just in the case the
                    // connection is not active already)
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[6].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[6].ToString()),
                                  int.Parse(this.listBoxPort.Items[6].ToString()),
                    // Forward parameters
                                  IPAddress.Parse(this.listBoxForwardingInterface.Items[6].ToString()),
                                   IPAddress.Parse(this.listBoxForwardingMulticast.Items[6].ToString()),
                                  int.Parse(this.listBoxForwardingPort.Items[6].ToString())) == false)
                {
                    this.checkBoxF7.Checked = false;
                }
                else
                {
                    this.progressBarF7.Visible = true;
                }

            }
            else
            {
                RecForwConnection7.StopForwarding();
                this.progressBarF7.Visible = false;
            }
        }

        private void checkBoxF8_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxF8.Checked == true)
            {
                if (RecForwConnection8.StartForward(
                    //Source parameters (always passed in, however the connection manager will use it just in the case the
                    // connection is not active already)
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[7].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[7].ToString()),
                                  int.Parse(this.listBoxPort.Items[7].ToString()),
                    // Forward parameters
                                  IPAddress.Parse(this.listBoxForwardingInterface.Items[7].ToString()),
                                   IPAddress.Parse(this.listBoxForwardingMulticast.Items[7].ToString()),
                                  int.Parse(this.listBoxForwardingPort.Items[7].ToString())) == false)
                {
                    this.checkBoxF8.Checked = false;
                }
                else
                {
                    this.progressBarF8.Visible = true;
                }

            }
            else
            {
                RecForwConnection8.StopForwarding();
                this.progressBarF8.Visible = false;
            }
        }

        private void checkBoxF9_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxF9.Checked == true)
            {
                if (RecForwConnection9.StartForward(
                    //Source parameters (always passed in, however the connection manager will use it just in the case the
                    // connection is not active already)
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[8].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[8].ToString()),
                                  int.Parse(this.listBoxPort.Items[8].ToString()),
                    // Forward parameters
                                  IPAddress.Parse(this.listBoxForwardingInterface.Items[8].ToString()),
                                   IPAddress.Parse(this.listBoxForwardingMulticast.Items[8].ToString()),
                                  int.Parse(this.listBoxForwardingPort.Items[8].ToString())) == false)
                {
                    this.checkBoxF9.Checked = false;
                }
                else
                {
                    this.progressBarF9.Visible = true;
                }

            }
            else
            {
                RecForwConnection9.StopForwarding();
                this.progressBarF9.Visible = false;
            }
        }

        private void checkBoxF10_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxF10.Checked == true)
            {
                if (RecForwConnection10.StartForward(
                    //Source parameters (always passed in, however the connection manager will use it just in the case the
                    // connection is not active already)
                                   IPAddress.Parse(this.listBoxLocalAddr.Items[9].ToString()),
                                   IPAddress.Parse(this.listBoxIPAddress.Items[9].ToString()),
                                  int.Parse(this.listBoxPort.Items[9].ToString()),
                    // Forward parameters
                                  IPAddress.Parse(this.listBoxForwardingInterface.Items[9].ToString()),
                                   IPAddress.Parse(this.listBoxForwardingMulticast.Items[9].ToString()),
                                  int.Parse(this.listBoxForwardingPort.Items[9].ToString())) == false)
                {
                    this.checkBoxF10.Checked = false;
                }
                else
                {
                    this.progressBarF10.Visible = true;
                }

            }
            else
            {
                RecForwConnection10.StopForwarding();
                this.progressBarF10.Visible = false;
            }
        }
    }
}
