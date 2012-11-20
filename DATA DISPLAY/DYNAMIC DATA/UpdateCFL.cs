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
    public partial class UpdateCFL : Form
    {
        public int TrackToUpdate = -1;
        private bool Is_Visible = false;

        public UpdateCFL()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TrackToUpdate != 1)
            {
                if (this.comboBox1.Text.Length > 0)
                    DynamicDisplayBuilder.UpdateCFL(TrackToUpdate, this.comboBox1.Text);
                FormMain MainFrame = Application.OpenForms[0] as FormMain;
                MainFrame.UpdateDIsplay();
                this.Close();
            }
        }

        private void UpdateCFL_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 5;
        }

        private void UpdateCFL_MouseCaptureChanged(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateCFL_VisibleChanged(object sender, EventArgs e)
        {
            if (Is_Visible == false)
                Is_Visible = true;
            else
                this.Close();
        }

        private void UpdateCFL_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
             string allowedCharacterSet = "0123456789\b";    	   //Allowed character set

            if (allowedCharacterSet.Contains(e.KeyChar.ToString()))
            {

            }
            else if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;

                if (TrackToUpdate != 1)
                {
                    if (this.comboBox1.Text.Length > 0)
                        DynamicDisplayBuilder.UpdateCFL(TrackToUpdate, this.comboBox1.Text);
                    FormMain MainFrame = Application.OpenForms[0] as FormMain;
                    MainFrame.UpdateDIsplay();
                    this.Close();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
