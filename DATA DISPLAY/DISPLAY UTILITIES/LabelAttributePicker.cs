using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace MulticastingUDP
{
    public partial class LabelAttributePicker : Form
    {
        public LabelAttributePicker()
        {
            InitializeComponent();
        }

        private void LabelAttributePicker_Load(object sender, EventArgs e)
        {
            PopulateForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LabelAttributes.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void PopulateForm()
        {
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static |
                                          BindingFlags.DeclaredOnly | BindingFlags.Public);
            
            // Populate label text color box and then set it to the currently used value
            foreach (PropertyInfo c in propInfoList)
                this.comboBoxTextColorChoice.Items.Add(c.Name);
            this.comboBoxTextColorChoice.SelectedIndex =
                this.comboBoxTextColorChoice.FindStringExact(LabelAttributes.TextColor.Name);

            // Populate label text font box and then set it to the currently used value
            FontFamily[] ffArray = FontFamily.Families;
            foreach (FontFamily ff in ffArray)
                this.comboBoxTextFontChoice.Items.Add(ff.Name);
            for (int Index = 0; Index < this.comboBoxTextFontChoice.Items.Count; Index++)
            {
                try { Font TestFOnt = new Font(this.comboBoxTextFontChoice.Items[Index].ToString(), 7); }
                catch { this.comboBoxTextFontChoice.Items.RemoveAt(Index); }
            }

            // Set font size to the currently used value
            this.comboBoxTextSizeChoice.SelectedIndex = LabelAttributes.TextSize - 1;
          
            // Populate label box and leader line color box and then set it to the currently used value
            foreach (PropertyInfo c in propInfoList)
                this.comboBoxLineColorChoice.Items.Add(c.Name);
            this.comboBoxLineColorChoice.SelectedIndex =
                this.comboBoxLineColorChoice.FindStringExact(LabelAttributes.LineColor.Name);

            // Set line width to the currently used value
            this.comboBoxLineWidth.SelectedIndex = LabelAttributes.LineWidth - 1;

            // Populate label background color and then set it to the currently used value
            foreach (PropertyInfo c in propInfoList)
                this.comboBoxBackroundColor.Items.Add(c.Name);
            this.comboBoxBackroundColor.SelectedIndex =
                this.comboBoxBackroundColor.FindStringExact(LabelAttributes.BackgroundColor.Name);

            // Populate target color and then set it to the currently used value
            foreach (PropertyInfo c in propInfoList)
                this.comboBoxTargetColor.Items.Add(c.Name);
            this.comboBoxTargetColor.SelectedIndex =
                this.comboBoxTargetColor.FindStringExact(LabelAttributes.TargetColor.Name);
        }
    }
}
