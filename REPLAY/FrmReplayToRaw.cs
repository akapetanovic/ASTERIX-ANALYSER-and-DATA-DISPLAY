using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser.REPLAY
{
    public partial class FrmReplayToRaw : Form
    {
        public FrmReplayToRaw()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ValidatePaths()
        {
            if (this.labelSource.Text.Length > 0 && this.labelDestination.Text.Length > 0)
                this.btnConvert.Enabled = true;
        }

        private void labelSource_TextChanged(object sender, EventArgs e)
        {
            ValidatePaths();
        }

        private void labelDestination_TextChanged(object sender, EventArgs e)
        {
            ValidatePaths();
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "ASTERIX Analyser Files|*.rply";
            openFileDialog1.InitialDirectory = "Application.StartupPath";
            openFileDialog1.Title = "Open File to Read";

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                labelSource.Text = openFileDialog1.FileName;
            }
        }

        private void btnDestination_Click(object sender, EventArgs e)
        {

        }
    }
}
