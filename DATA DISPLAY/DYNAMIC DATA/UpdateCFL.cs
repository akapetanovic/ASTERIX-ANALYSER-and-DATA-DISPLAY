using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser.DATA_DISPLAY.DYNAMIC_DATA
{
    public partial class UpdateCFL : Form
    {
        public int TrackToUpdate = -1;
        
        public UpdateCFL()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TrackToUpdate != 1)
            {
                DynamicDisplayBuilder.UpdateCFL(TrackToUpdate, this.comboBox1.Text);
            }
        }
    }
}
