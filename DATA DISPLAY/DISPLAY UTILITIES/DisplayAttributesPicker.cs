using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.Drawing.Drawing2D;

namespace AsterixDisplayAnalyser
{
    public partial class DisplayAttibutePicker : Form
    {
        public DisplayAttibutePicker()
        {
            InitializeComponent();
        }

        private void ColorPicker_Load(object sender, EventArgs e)
        {
            PopulateForm();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSampleText();
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
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

        private void comboBoxDataItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            SyncFormData();
        }

        private void PopulateForm()
        {

            // Populate the display origin
            GeoCordSystemDegMinSecUtilities.LatLongClass LatLon = new GeoCordSystemDegMinSecUtilities.LatLongClass(SystemAdaptationDataSet.SystemOriginPoint.Lat, SystemAdaptationDataSet.SystemOriginPoint.Lng);
            this.txtLatDDD.Text = LatLon.GetDegMinSec().Latitude.Deg.ToString();
            this.txtLatMM.Text = LatLon.GetDegMinSec().Latitude.Min.ToString();
            int Int_Sec = (int)LatLon.GetDegMinSec().Latitude.Sec;
            this.txtLatSS.Text = Int_Sec.ToString();
            if (LatLon.GetDegMinSec().Latitude.Prefix == GeoCordSystemDegMinSecUtilities.LatLongPrefix.N)
                this.comboBoxLatDirection.SelectedIndex = 0;
            else
                this.comboBoxLatDirection.SelectedIndex = 1;

            this.txtLonDDD.Text = LatLon.GetDegMinSec().Longitude.Deg.ToString();
            this.txtLonMM.Text = LatLon.GetDegMinSec().Longitude.Min.ToString();
            Int_Sec = (int)LatLon.GetDegMinSec().Longitude.Sec;
            this.txtLonSS.Text = Int_Sec.ToString();
            if (LatLon.GetDegMinSec().Longitude.Prefix == GeoCordSystemDegMinSecUtilities.LatLongPrefix.E)
                this.comboBoxLonDirection.SelectedIndex = 0;
            else
                this.comboBoxLonDirection.SelectedIndex = 1;

            // Load all display items and set it to the first one in the list
            // and it will cause the selected index to change that will then trigger
            // a call to sync data function
            foreach (DisplayAttributes.DisplayAttributesType DataItem in DisplayAttributes.GetAllDisplayAttributes())
                if (DataItem.ItemName != "BackgroundColor")
                    comboBoxDataItem.Items.Add(DataItem.ItemName);

            // Background Color
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static |
                                          BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo c in propInfoList)
                this.comboBoxBackgroundColor.Items.Add(c.Name);

            // Now set the index of the background color to the currently set background color.
            this.comboBoxBackgroundColor.SelectedIndex =
                this.comboBoxBackgroundColor.FindStringExact(DisplayAttributes.GetDisplayAttribute(DisplayAttributes.DisplayItemsType.BackgroundColor).TextColor.Name);

            /////////////////////////////////////////////////////////////
            // TEXT ATTRIBUTES
            /////////////////////////////////////////////////////////////

            // Text Attributes Color
            foreach (PropertyInfo c in propInfoList)
                this.comboBoxTextColorChoice.Items.Add(c.Name);

            // Text Attributes Font
            FontFamily[] ffArray = FontFamily.Families;
            foreach (FontFamily ff in ffArray)
                this.comboBoxTextFontChoice.Items.Add(ff.Name);

            for (int Index = 0; Index < this.comboBoxTextFontChoice.Items.Count; Index++)
            {
                try { Font TestFOnt = new Font(this.comboBoxTextFontChoice.Items[Index].ToString(), 7); }
                catch { this.comboBoxTextFontChoice.Items.RemoveAt(Index); }
            }

            /////////////////////////////////////////////////////////////
            // LINE ATTRIBUTES
            /////////////////////////////////////////////////////////////

            // Line Attributes Color
            foreach (PropertyInfo c in propInfoList)
                this.comboBoxLineColorChoice.Items.Add(c.Name);

            // Line Attributes Type
            // Hard coded


            /////////////////////////////////////////////////////////////
            // AREA/POLYGON ATTRIBUTES
            /////////////////////////////////////////////////////////////

            // Area/Polygon Attributes
            foreach (PropertyInfo c in propInfoList)
                this.comboBoxAreaPolygonColorChoice.Items.Add(c.Name);

            /////////////////////////////////////////////////////////////
            // IMAGE ATTRIBUTES
            /////////////////////////////////////////////////////////////

            // Now set index to the first display item
            this.comboBoxDataItem.SelectedIndex = 0;
        }

        private void SyncFormData()
        {
            // First get all the display attributes for the selected data item
            DisplayAttributes.DisplayAttributesType DisplayAttribute =
                DisplayAttributes.GetDisplayAttribute((DisplayAttributes.DisplayItemsType)Enum.Parse(typeof(DisplayAttributes.DisplayItemsType),
                this.comboBoxDataItem.Text, true));

            /////////////////////////////////////////////////////////////
            // TEXT ATTRIBUTES
            /////////////////////////////////////////////////////////////

            // Text Attributes Color
            this.comboBoxTextColorChoice.SelectedIndex =
                this.comboBoxTextColorChoice.FindStringExact(DisplayAttribute.TextColor.Name);

            // Text Attributes Font
            this.comboBoxTextFontChoice.SelectedIndex =
                 this.comboBoxTextFontChoice.FindStringExact(DisplayAttribute.TextFont.Name);

            // Text Attributes Size
            this.comboBoxTextSizeChoice.SelectedIndex = DisplayAttribute.TextSize - 1;

            /////////////////////////////////////////////////////////////
            // LINE ATTRIBUTES
            /////////////////////////////////////////////////////////////

            // Line Attributes Color
            this.comboBoxLineColorChoice.SelectedIndex = this.comboBoxLineColorChoice.FindStringExact(DisplayAttribute.LineColor.Name);

            // Line Attributes Type

            this.comboBoxLineStyleChoice.SelectedIndex = this.comboBoxLineStyleChoice.FindStringExact(DisplayAttribute.LineStyle.ToString());

            // Line Attributes Size.
            this.comboBoxLineWidth.SelectedIndex = DisplayAttribute.LineWidth - 1;

            /////////////////////////////////////////////////////////////
            // AREA/POLYGON ATTRIBUTES
            /////////////////////////////////////////////////////////////
            this.comboBoxAreaPolygonColorChoice.SelectedIndex =
               this.comboBoxAreaPolygonColorChoice.FindStringExact(DisplayAttribute.AreaPolygonColor.Name);

            /////////////////////////////////////////////////////////////
            // IMAGE ATTRIBUTES
            /////////////////////////////////////////////////////////////
            this.numericUpDown_X.Value = DisplayAttribute.ImageSize.Width;
            this.numericUpDown_Y.Value = DisplayAttribute.ImageSize.Height;
            this.pictureBox.BackColor = Color.FromName(this.comboBoxBackgroundColor.Text);

            UpdateSampleLine();
            UpdateSampleText();
            UpdateAreaPolygonSample();
            UpdatePictureSample();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DisplayAttributes.DisplayAttributesType NewDisplayAttribute = new DisplayAttributes.DisplayAttributesType();
            NewDisplayAttribute.ItemName = this.comboBoxDataItem.Text;
            NewDisplayAttribute.TextSize = int.Parse(this.comboBoxTextSizeChoice.Text);
            NewDisplayAttribute.TextFont = new FontFamily(this.comboBoxTextFontChoice.Text);
            NewDisplayAttribute.TextColor = Color.FromName(this.comboBoxTextColorChoice.Text);
            NewDisplayAttribute.LineWidth = int.Parse(this.comboBoxLineWidth.Text);
            NewDisplayAttribute.LineColor = Color.FromName(this.comboBoxLineColorChoice.Text);
            NewDisplayAttribute.LineStyle = DisplayAttributes.GetLineStypefromString(this.comboBoxLineStyleChoice.Text);

            NewDisplayAttribute.AreaPolygonColor = Color.FromName(this.comboBoxAreaPolygonColorChoice.Text);
            NewDisplayAttribute.ImageSize = new Size((int)this.numericUpDown_X.Value, (int)this.numericUpDown_Y.Value);
            DisplayAttributes.SetDisplayAttribute((DisplayAttributes.DisplayItemsType)Enum.Parse(typeof(DisplayAttributes.DisplayItemsType), NewDisplayAttribute.ItemName, true), NewDisplayAttribute);

           // Always update the background color as well
            NewDisplayAttribute = new DisplayAttributes.DisplayAttributesType();
            NewDisplayAttribute.TextColor = Color.FromName(this.comboBoxBackgroundColor.Text);
            NewDisplayAttribute.ItemName = "BackgroundColor";
            DisplayAttributes.SetDisplayAttribute(DisplayAttributes.DisplayItemsType.BackgroundColor, NewDisplayAttribute); 
            
            // Populate the display origin
            GeoCordSystemDegMinSecUtilities.LatLongPrefix LatPrefix;
            GeoCordSystemDegMinSecUtilities.LatLongPrefix LoNPrefix;

            if (this.comboBoxLatDirection.SelectedIndex == 0)
                LatPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.N;
            else
                LatPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.W;

            if (this.comboBoxLonDirection.SelectedIndex == 0)
                LoNPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.E;
            else
                LoNPrefix = GeoCordSystemDegMinSecUtilities.LatLongPrefix.S;

            GeoCordSystemDegMinSecUtilities.LatLongClass LatLon =
                new GeoCordSystemDegMinSecUtilities.LatLongClass(int.Parse(this.txtLatDDD.Text), int.Parse(this.txtLatMM.Text), int.Parse(this.txtLatSS.Text), LatPrefix,
                    int.Parse(this.txtLonDDD.Text), int.Parse(this.txtLonMM.Text), int.Parse(this.txtLonSS.Text), LoNPrefix);

            SystemAdaptationDataSet.SystemOrigin = new GMap.NET.PointLatLng(LatLon.GetLatLongDecimal().LatitudeDecimal, LatLon.GetLatLongDecimal().LongitudeDecimal);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxBackgroundColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string color = this.comboBoxBackgroundColor.SelectedItem.ToString();
            this.panelBackgroundColor.BackColor = Color.FromName(color);
            this.panelTextAttributes.BackColor = Color.FromName(color);
            this.panelLineAttributes.BackColor = Color.FromName(color);
            this.panelAreaPolygonColor.BackColor = Color.FromName(color);
            this.pictureBox.BackColor = Color.FromName(this.comboBoxBackgroundColor.Text);
        }

        private void comboBoxBackgroundColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxLineColorChoice_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxAreaPolygonColorChoice_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void numericUpDown_Y_ValueChanged(object sender, EventArgs e)
        {
            UpdatePictureSample();

        }

        private void numericUpDown_X_ValueChanged(object sender, EventArgs e)
        {
            UpdatePictureSample();
        }

        private void UpdateSampleText()
        {
            if (this.comboBoxTextSizeChoice.Text.Length > 0)
            {
                this.labelSampleText.Font = new Font(this.comboBoxTextFontChoice.Text, (float)int.Parse(this.comboBoxTextSizeChoice.Text));
                this.labelSampleText.ForeColor = Color.FromName(this.comboBoxTextColorChoice.Text);
            }
        }

        private void comboBoxTextFontChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSampleText();
        }

        private void comboBoxTextSizeChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSampleText();
        }

        private void comboBoxLineColorChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSampleLine();
        }

        private void UpdateSampleLine()
        {
            if (this.comboBoxLineColorChoice.Text.Length > 0 && this.comboBoxLineWidth.Text.Length > 0 && this.comboBoxLineStyleChoice.Text.Length > 0)
            {
                // Create pen.
                Pen MyPen = new Pen(Color.FromName(this.comboBoxLineColorChoice.Text), int.Parse(this.comboBoxLineWidth.Text));

                // Set line style
                MyPen.DashStyle = DisplayAttributes.GetLineStypefromString(this.comboBoxLineStyleChoice.Text);

                int x1 = panelLineAttributes.Width / int.Parse(this.comboBoxTextSizeChoice.Text);
                int y1 = panelLineAttributes.Height / int.Parse(this.comboBoxTextSizeChoice.Text);

                // Draw line to screen.
                Graphics MyGraphics = this.panelLineAttributes.CreateGraphics();
                panelLineAttributes.Refresh();
                MyGraphics.DrawLine(MyPen, 0, 0, panelLineAttributes.Width, panelLineAttributes.Height);
                MyGraphics.Dispose();
                MyPen.Dispose();
            }
        }

        private void comboBoxLineStyleChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSampleLine();
        }

        private void comboBoxLineWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSampleLine();
        }

        private void comboBoxAreaPolygonColorChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAreaPolygonSample();
        }

        private void UpdateAreaPolygonSample()
        {
            // Draw line to screen.
            Graphics MyGraphics = this.panelAreaPolygonColor.CreateGraphics();
            panelAreaPolygonColor.Refresh();

            SolidBrush myBrush = new SolidBrush(Color.FromName(this.comboBoxAreaPolygonColorChoice.Text));
            Point[] MyPoints = { new Point(10, 10), new Point(10, 40), new Point(40, 40), new Point(40, 10), new Point(10, 10) };
            MyGraphics.FillPolygon(myBrush, MyPoints);
            MyGraphics.Dispose();
            myBrush.Dispose();
        }

        private void UpdatePictureSample()
        {
            this.pictureBox.Size = new Size((int)this.numericUpDown_X.Value, (int)this.numericUpDown_Y.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Display_Attributes_IO.Save();
        }
    }
}
