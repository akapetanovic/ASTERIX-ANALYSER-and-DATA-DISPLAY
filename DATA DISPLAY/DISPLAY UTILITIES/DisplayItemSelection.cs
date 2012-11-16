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
    public partial class DisplayItemSelection : Form
    {
        public DisplayItemSelection()
        {
            InitializeComponent();
            this.checkBoxStateBorder.Checked = Properties.Settings.Default.StateBorder;
            this.checkBoxWaypoints.Checked = Properties.Settings.Default.Waypoints;
            this.checkBoxRadars.Checked = Properties.Settings.Default.Radars;
            this.checkBoxSectors.Checked = Properties.Settings.Default.Sectors;
            this.checkBoxRunways.Checked = Properties.Settings.Default.Runways;
        }

        private void checkBoxStateBorder_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.StateBorder = this.checkBoxStateBorder.Checked;
            Properties.Settings.Default.Save();
            DisplayAttributes.StaticDisplayBuildRequired = true;
        }

        private void checkBoxWaypoints_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Waypoints = this.checkBoxWaypoints.Checked;
            Properties.Settings.Default.Save();
            DisplayAttributes.StaticDisplayBuildRequired = true;
        }

        private void checkBoxRadars_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Radars = this.checkBoxRadars.Checked;
            Properties.Settings.Default.Save();
            DisplayAttributes.StaticDisplayBuildRequired = true;
        }

        private void checkBoxSectors_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Sectors = this.checkBoxSectors.Checked;
            Properties.Settings.Default.Save();
            DisplayAttributes.StaticDisplayBuildRequired = true;
        }

        private void DisplayItemSelection_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxRunways_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Runways = this.checkBoxRunways.Checked;
            Properties.Settings.Default.Save();
            DisplayAttributes.StaticDisplayBuildRequired = true;
        }
    }
}
