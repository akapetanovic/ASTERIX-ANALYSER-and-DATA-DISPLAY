using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MulticastingUDP
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            GeoCordSystemDegMinSecUtilities.LatLongClass PP = new GeoCordSystemDegMinSecUtilities.LatLongClass(43.81993, 18.33983);
            GeoCordSystemDegMinSecUtilities.LatLongClass NewPosition = GeoCordSystemDegMinSecUtilities.CalculateNewPosition(PP, 2.37, 29.53);

            string Lat, Long;
            NewPosition.GetDegMinSecStringFormat(out Lat, out Long);

            MessageBox.Show(Lat + " / " + Long);
        }
    }
}
