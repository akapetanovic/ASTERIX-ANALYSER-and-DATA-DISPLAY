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
        public FileReadProgress()
        {
            InitializeComponent();
        }

        private void FileReadProgress_Load(object sender, EventArgs e)
        {
            // Set up progress bar marguee
            this.progressBar1.Step = 2;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 50; // 100msec
            this.progressBar1.Visible = true;
            this.labelNotify.Text = "Reading...";
            this.buttonOK.Enabled = false;
        }

        public void NotifyFinishReading(string Message)
        {
            this.labelNotify.Text = Message;
            this.buttonOK.Enabled = true;
            this.progressBar1.Visible = false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
