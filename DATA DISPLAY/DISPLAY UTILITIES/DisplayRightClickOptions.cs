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
    public partial class DisplayRightClickOptions : Form
    {
        public DisplayRightClickOptions()
        {
            InitializeComponent();
        }

        private void btnDisplaySettings_Click(object sender, EventArgs e)
        {
            DisplayAttibutePicker ColorPickerForm = new DisplayAttibutePicker();
            ColorPickerForm.Show();
            this.Close();
        }

        private void btnItemsDisplay_Click(object sender, EventArgs e)
        {
            DisplayItemSelection MyForm = new DisplayItemSelection();
            MyForm.Show();
            this.Close();
        }

        private void btnLabelAttributes_Click(object sender, EventArgs e)
        {
            LabelAttributePicker MyFom = new LabelAttributePicker();
            MyFom.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormMain.ShowExtendedLabel();
            this.Close();
        }
    }
}
