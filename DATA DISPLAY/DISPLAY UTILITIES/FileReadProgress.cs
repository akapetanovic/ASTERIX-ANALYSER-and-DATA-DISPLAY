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
    public partial class FileReadProgress : Form
    {
        public bool AbortRequested = false;
      
        public FileReadProgress()
        {
            InitializeComponent();
        }

        private void FileReadProgress_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbortRequested = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
