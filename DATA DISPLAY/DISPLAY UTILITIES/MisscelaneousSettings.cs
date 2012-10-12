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
    public partial class MisscelaneousSettings : Form
    {
        public MisscelaneousSettings()
        {
            InitializeComponent();
        }

        private void MisscelaneousSettings_Load(object sender, EventArgs e)
        {
            this.checkBoxDisplaModeasFL.Checked = Properties.Settings.Default.DisplayModeC_As_FL;
        }

        private void checkBoxDisplaModeasFL_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DisplayModeC_As_FL = this.checkBoxDisplaModeasFL.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
