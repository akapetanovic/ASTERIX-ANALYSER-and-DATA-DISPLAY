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
    public partial class CAT_Decoder_Selector : Form
    {
        public CAT_Decoder_Selector()
        {
            InitializeComponent();
       
            this.checkBox001.Checked = Properties.Settings.Default.CAT_001_Enabled;
            this.checkBox002.Checked = Properties.Settings.Default.CAT_002_Enabled;
            this.checkBox008.Checked = Properties.Settings.Default.CAT_008_Enabled;
            this.checkBox034.Checked = Properties.Settings.Default.CAT_034_Enabled;
            this.checkBox048.Checked = Properties.Settings.Default.CAT_048_Enabled;
            this.checkBox062.Checked = Properties.Settings.Default.CAT_062_Enabled;
            this.checkBox063.Checked = Properties.Settings.Default.CAT_063_Enabled;
            this.checkBox065.Checked = Properties.Settings.Default.CAT_065_Enabled;
            this.checkBox244.Checked = Properties.Settings.Default.CAT_244_Enabled;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CAT_Decoder_Selector_Load(object sender, EventArgs e)
        {

        }

        private void checkBox001_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CAT_001_Enabled = this.checkBox001.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox002_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CAT_002_Enabled = this.checkBox002.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox008_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CAT_008_Enabled = this.checkBox008.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox034_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CAT_034_Enabled = this.checkBox034.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox048_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CAT_048_Enabled = this.checkBox048.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox062_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CAT_062_Enabled = this.checkBox062.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox063_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CAT_063_Enabled = this.checkBox063.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox065_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CAT_065_Enabled = this.checkBox065.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox244_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CAT_244_Enabled = this.checkBox244.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
