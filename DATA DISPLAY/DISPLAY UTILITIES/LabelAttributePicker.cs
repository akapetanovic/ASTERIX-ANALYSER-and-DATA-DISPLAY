using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace AsterixDisplayAnalyser
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
            // Label text attributes
            LabelAttributes.TextColor = Color.FromName(this.comboBoxTextColorChoice.Text);
            LabelAttributes.TextFont = new FontFamily(this.comboBoxTextFontChoice.Text);
            LabelAttributes.TextSize = int.Parse(this.comboBoxTextSizeChoice.Text);

            // Label Box and leader line attributes
            LabelAttributes.LineColor = Color.FromName(this.comboBoxLineColorChoice.Text);
            LabelAttributes.LineStyle = DisplayAttributes.GetLineStypefromString(this.comboBoxLineStyleChoice.Text);
            LabelAttributes.LineWidth = int.Parse(this.comboBoxLineWidth.Text);

            // Label background color
            LabelAttributes.BackgroundColor = Color.FromName(this.comboBoxBackroundColor.Text);

            // Target Symbol Attributes
            LabelAttributes.TargetColor = Color.FromName(this.comboBoxTargetColor.Text);
            LabelAttributes.TargetStyle = DisplayAttributes.GetLineStypefromString(this.comboBoxTargetStyle.Text);
            LabelAttributes.TargetSize = int.Parse(this.comboBoxTargetSize.Text);

        }

        private void DrawStringandRectangleinComboBox(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);

                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);

                g.FillRectangle(b, rect.X + 110, rect.Y + 5,
                                rect.Width - 10, rect.Height - 10);
            }
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
            this.comboBoxTextFontChoice.SelectedIndex =
               this.comboBoxTextFontChoice.FindStringExact(LabelAttributes.TextFont.Name);


            // Set font size to the currently used value
            this.comboBoxTextSizeChoice.SelectedIndex = LabelAttributes.TextSize - 1;

            // Populate label box and leader line color box and then set it to the currently used value
            foreach (PropertyInfo c in propInfoList)
                this.comboBoxLineColorChoice.Items.Add(c.Name);
            this.comboBoxLineColorChoice.SelectedIndex =
                this.comboBoxLineColorChoice.FindStringExact(LabelAttributes.LineColor.Name);

            // Set the Label box and leader line type to the selected value
            this.comboBoxLineStyleChoice.SelectedIndex =
               this.comboBoxLineStyleChoice.FindStringExact(LabelAttributes.LineStyle.ToString());

            // Set target symbol line type
            this.comboBoxTargetStyle.SelectedIndex =
              this.comboBoxTargetStyle.FindStringExact(LabelAttributes.TargetStyle.ToString());

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

            // Set line width to the currently used value
            this.comboBoxTargetSize.SelectedIndex = LabelAttributes.TargetSize - 1;
        }

        private void comboBoxTextColorChoice_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxLineColorChoice_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxTargetColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxBackroundColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxTextColorChoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxTextFontChoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxTextSizeChoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxBackroundColor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxTargetSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
