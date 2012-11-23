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
    public partial class FrmExtendedLabel : Form
    {
        public enum DataItems { TAS, IAS, MACH, TRK, HDG, Roll_Angle, Selected_Altitudes, RateOfClimb }

        public FrmExtendedLabel()
        {
            InitializeComponent();
        }

        private void ExtendedLabel_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;

        }

        public void DefaultAll()
        {
            this.Text = "NO LABEL SELECTED";
            this.labelEXT_HDG.Text = "N/A";
            this.labelEXT_TRK.Text = "N/A";
            this.labelEXT_TAS.Text = "N/A";
            this.labelEXT_MCH.Text = "N/A";
            this.labelEXT_IAS.Text = "N/A";
            this.labelRoll_Angle.Text = "N/A";
            this.labelSelectedAltitudes.Text = "N/A";
            this.labelRateOfClimb.Text = "N/A";
        }

        public void SetDataValue(DataItems Item, string value)
        {

            switch (Item)
            {
                case DataItems.HDG:
                    this.labelEXT_HDG.Text = value;
                    break;
                case DataItems.TRK:
                    this.labelEXT_TRK.Text = value;
                    break;
                case DataItems.IAS:
                    this.labelEXT_IAS.Text = value;
                    break;
                case DataItems.MACH:
                    this.labelEXT_MCH.Text = value;
                    break;
                case DataItems.TAS:
                    this.labelEXT_TAS.Text = value;
                    break;
                case DataItems.Roll_Angle:
                    this.labelRoll_Angle.Text = value;
                    break;
                case DataItems.Selected_Altitudes:
                    this.labelSelectedAltitudes.Text = value;
                    break;
                case DataItems.RateOfClimb:
                    this.labelRateOfClimb.Text = value;
                    break;
            }
        }
    }
}
