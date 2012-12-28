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
    public partial class frmDebug : Form
    {
        public frmDebug()
        {
            InitializeComponent();
        }

        private void frmDebug_Load(object sender, EventArgs e)
        {

        }

        public void SetListBox(string Data)
        {
            this.listBoxDebug.Items.Add(Data);
        }
    }
}
