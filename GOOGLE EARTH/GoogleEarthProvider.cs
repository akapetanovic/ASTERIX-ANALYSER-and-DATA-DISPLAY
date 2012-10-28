using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser
{
    public partial class GoogleEarthProvider : Form
    {
        public GoogleEarthProvider()
        {
            InitializeComponent();
        }

        private void GoogleEarthProvider_Load(object sender, EventArgs e)
        {
            this.checkBoxShowModeA.Checked = Properties.Settings.Default.GE_Show_ModeA;
            this.checkBoxShowCallsign.Checked = Properties.Settings.Default.GE_Show_Show_Callsign;
            this.checkBoxShowModeC.Checked = Properties.Settings.Default.GE_Show_ModeC;
            this.checkBoxDisplayAsFL.Checked = Properties.Settings.Default.Show_ModeC_as_FL;
            this.textBox1.Text = Properties.Settings.Default.GE_Dest_Path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxShowModeA_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.GE_Show_ModeA = this.checkBoxShowModeA.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBoxShowModeC_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.GE_Show_ModeC = this.checkBoxShowModeC.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBoxShowCallsign_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.GE_Show_Show_Callsign = this.checkBoxShowCallsign.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBoxDisplayAsFL_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Show_ModeC_as_FL = this.checkBoxDisplayAsFL.Checked;
            Properties.Settings.Default.Save();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();

            if (FBD.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = FBD.SelectedPath;
                Properties.Settings.Default.GE_Dest_Path = this.textBox1.Text;
                Properties.Settings.Default.Save();
            }
        }
    }
}
