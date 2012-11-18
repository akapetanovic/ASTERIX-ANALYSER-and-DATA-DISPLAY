namespace AsterixDisplayAnalyser
{
    partial class DisplayAttibutePicker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayAttibutePicker));
            this.comboBoxTextColorChoice = new System.Windows.Forms.ComboBox();
            this.panelTextAttributes = new System.Windows.Forms.Panel();
            this.labelSampleText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDataItem = new System.Windows.Forms.ComboBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBoxAreaPolygonColorChoice = new System.Windows.Forms.ComboBox();
            this.panelAreaPolygonColor = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBoxLineAttributes = new System.Windows.Forms.GroupBox();
            this.comboBoxLineStyleChoice = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelLineAttributes = new System.Windows.Forms.Panel();
            this.comboBoxLineColorChoice = new System.Windows.Forms.ComboBox();
            this.labelLineColor = new System.Windows.Forms.Label();
            this.comboBoxLineWidth = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxTextAttributes = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxTextFontChoice = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxTextSizeChoice = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_Y = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_X = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBoxBackgroundColor = new System.Windows.Forms.ComboBox();
            this.panelBackgroundColor = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtLonMM = new System.Windows.Forms.TextBox();
            this.txtLonSS = new System.Windows.Forms.TextBox();
            this.txtLonDDD = new System.Windows.Forms.TextBox();
            this.comboBoxLonDirection = new System.Windows.Forms.ComboBox();
            this.txtLatMM = new System.Windows.Forms.TextBox();
            this.txtLatSS = new System.Windows.Forms.TextBox();
            this.txtLatDDD = new System.Windows.Forms.TextBox();
            this.comboBoxLatDirection = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panelTextAttributes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBoxLineAttributes.SuspendLayout();
            this.groupBoxTextAttributes.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxTextColorChoice
            // 
            this.comboBoxTextColorChoice.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxTextColorChoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTextColorChoice.FormattingEnabled = true;
            this.comboBoxTextColorChoice.Location = new System.Drawing.Point(50, 19);
            this.comboBoxTextColorChoice.Name = "comboBoxTextColorChoice";
            this.comboBoxTextColorChoice.Size = new System.Drawing.Size(178, 21);
            this.comboBoxTextColorChoice.TabIndex = 0;
            this.comboBoxTextColorChoice.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox1_DrawItem);
            this.comboBoxTextColorChoice.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // panelTextAttributes
            // 
            this.panelTextAttributes.Controls.Add(this.labelSampleText);
            this.panelTextAttributes.Location = new System.Drawing.Point(232, 19);
            this.panelTextAttributes.Name = "panelTextAttributes";
            this.panelTextAttributes.Size = new System.Drawing.Size(110, 75);
            this.panelTextAttributes.TabIndex = 1;
            // 
            // labelSampleText
            // 
            this.labelSampleText.AutoSize = true;
            this.labelSampleText.Location = new System.Drawing.Point(4, 27);
            this.labelSampleText.Name = "labelSampleText";
            this.labelSampleText.Size = new System.Drawing.Size(73, 13);
            this.labelSampleText.TabIndex = 0;
            this.labelSampleText.Text = "Sample TEXT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Display Item";
            // 
            // comboBoxDataItem
            // 
            this.comboBoxDataItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxDataItem.FormattingEnabled = true;
            this.comboBoxDataItem.Location = new System.Drawing.Point(73, 19);
            this.comboBoxDataItem.Name = "comboBoxDataItem";
            this.comboBoxDataItem.Size = new System.Drawing.Size(282, 21);
            this.comboBoxDataItem.TabIndex = 3;
            this.comboBoxDataItem.SelectedIndexChanged += new System.EventHandler(this.comboBoxDataItem_SelectedIndexChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Location = new System.Drawing.Point(146, 627);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(93, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Update Display";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(9, 27);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(31, 13);
            this.label.TabIndex = 5;
            this.label.Text = "Color";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBoxLineAttributes);
            this.groupBox1.Controls.Add(this.groupBoxTextAttributes);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.comboBoxDataItem);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 482);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Display Attributes";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBoxAreaPolygonColorChoice);
            this.groupBox4.Controls.Add(this.panelAreaPolygonColor);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(8, 264);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(349, 102);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Area/Polygon/Sector Attributes";
            // 
            // comboBoxAreaPolygonColorChoice
            // 
            this.comboBoxAreaPolygonColorChoice.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxAreaPolygonColorChoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxAreaPolygonColorChoice.FormattingEnabled = true;
            this.comboBoxAreaPolygonColorChoice.Location = new System.Drawing.Point(47, 26);
            this.comboBoxAreaPolygonColorChoice.Name = "comboBoxAreaPolygonColorChoice";
            this.comboBoxAreaPolygonColorChoice.Size = new System.Drawing.Size(178, 21);
            this.comboBoxAreaPolygonColorChoice.TabIndex = 9;
            this.comboBoxAreaPolygonColorChoice.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxAreaPolygonColorChoice_DrawItem);
            this.comboBoxAreaPolygonColorChoice.SelectedIndexChanged += new System.EventHandler(this.comboBoxAreaPolygonColorChoice_SelectedIndexChanged);
            // 
            // panelAreaPolygonColor
            // 
            this.panelAreaPolygonColor.Location = new System.Drawing.Point(231, 26);
            this.panelAreaPolygonColor.Name = "panelAreaPolygonColor";
            this.panelAreaPolygonColor.Size = new System.Drawing.Size(110, 70);
            this.panelAreaPolygonColor.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Color";
            // 
            // groupBoxLineAttributes
            // 
            this.groupBoxLineAttributes.Controls.Add(this.comboBoxLineStyleChoice);
            this.groupBoxLineAttributes.Controls.Add(this.label2);
            this.groupBoxLineAttributes.Controls.Add(this.panelLineAttributes);
            this.groupBoxLineAttributes.Controls.Add(this.comboBoxLineColorChoice);
            this.groupBoxLineAttributes.Controls.Add(this.labelLineColor);
            this.groupBoxLineAttributes.Controls.Add(this.comboBoxLineWidth);
            this.groupBoxLineAttributes.Controls.Add(this.label7);
            this.groupBoxLineAttributes.Location = new System.Drawing.Point(6, 152);
            this.groupBoxLineAttributes.Name = "groupBoxLineAttributes";
            this.groupBoxLineAttributes.Size = new System.Drawing.Size(349, 106);
            this.groupBoxLineAttributes.TabIndex = 18;
            this.groupBoxLineAttributes.TabStop = false;
            this.groupBoxLineAttributes.Text = "Line Attributes";
            // 
            // comboBoxLineStyleChoice
            // 
            this.comboBoxLineStyleChoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxLineStyleChoice.FormattingEnabled = true;
            this.comboBoxLineStyleChoice.Items.AddRange(new object[] {
            "Solid",
            "Dash",
            "DashDot",
            "DashDotDot",
            "Dot"});
            this.comboBoxLineStyleChoice.Location = new System.Drawing.Point(50, 49);
            this.comboBoxLineStyleChoice.Name = "comboBoxLineStyleChoice";
            this.comboBoxLineStyleChoice.Size = new System.Drawing.Size(178, 21);
            this.comboBoxLineStyleChoice.TabIndex = 21;
            this.comboBoxLineStyleChoice.SelectedIndexChanged += new System.EventHandler(this.comboBoxLineStyleChoice_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Type";
            // 
            // panelLineAttributes
            // 
            this.panelLineAttributes.Location = new System.Drawing.Point(231, 22);
            this.panelLineAttributes.Name = "panelLineAttributes";
            this.panelLineAttributes.Size = new System.Drawing.Size(110, 75);
            this.panelLineAttributes.TabIndex = 19;
            // 
            // comboBoxLineColorChoice
            // 
            this.comboBoxLineColorChoice.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxLineColorChoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxLineColorChoice.FormattingEnabled = true;
            this.comboBoxLineColorChoice.Location = new System.Drawing.Point(50, 22);
            this.comboBoxLineColorChoice.Name = "comboBoxLineColorChoice";
            this.comboBoxLineColorChoice.Size = new System.Drawing.Size(178, 21);
            this.comboBoxLineColorChoice.TabIndex = 18;
            this.comboBoxLineColorChoice.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxLineColorChoice_DrawItem);
            this.comboBoxLineColorChoice.SelectedIndexChanged += new System.EventHandler(this.comboBoxLineColorChoice_SelectedIndexChanged);
            // 
            // labelLineColor
            // 
            this.labelLineColor.AutoSize = true;
            this.labelLineColor.Location = new System.Drawing.Point(9, 25);
            this.labelLineColor.Name = "labelLineColor";
            this.labelLineColor.Size = new System.Drawing.Size(31, 13);
            this.labelLineColor.TabIndex = 17;
            this.labelLineColor.Text = "Color";
            // 
            // comboBoxLineWidth
            // 
            this.comboBoxLineWidth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxLineWidth.FormattingEnabled = true;
            this.comboBoxLineWidth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.comboBoxLineWidth.Location = new System.Drawing.Point(50, 76);
            this.comboBoxLineWidth.Name = "comboBoxLineWidth";
            this.comboBoxLineWidth.Size = new System.Drawing.Size(38, 21);
            this.comboBoxLineWidth.TabIndex = 16;
            this.comboBoxLineWidth.SelectedIndexChanged += new System.EventHandler(this.comboBoxLineWidth_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Width";
            // 
            // groupBoxTextAttributes
            // 
            this.groupBoxTextAttributes.Controls.Add(this.comboBoxTextColorChoice);
            this.groupBoxTextAttributes.Controls.Add(this.panelTextAttributes);
            this.groupBoxTextAttributes.Controls.Add(this.label);
            this.groupBoxTextAttributes.Controls.Add(this.label4);
            this.groupBoxTextAttributes.Controls.Add(this.comboBoxTextFontChoice);
            this.groupBoxTextAttributes.Controls.Add(this.label5);
            this.groupBoxTextAttributes.Controls.Add(this.comboBoxTextSizeChoice);
            this.groupBoxTextAttributes.Location = new System.Drawing.Point(6, 46);
            this.groupBoxTextAttributes.Name = "groupBoxTextAttributes";
            this.groupBoxTextAttributes.Size = new System.Drawing.Size(349, 100);
            this.groupBoxTextAttributes.TabIndex = 17;
            this.groupBoxTextAttributes.TabStop = false;
            this.groupBoxTextAttributes.Text = "Text Attributes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Font";
            // 
            // comboBoxTextFontChoice
            // 
            this.comboBoxTextFontChoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTextFontChoice.FormattingEnabled = true;
            this.comboBoxTextFontChoice.Location = new System.Drawing.Point(50, 46);
            this.comboBoxTextFontChoice.Name = "comboBoxTextFontChoice";
            this.comboBoxTextFontChoice.Size = new System.Drawing.Size(178, 21);
            this.comboBoxTextFontChoice.TabIndex = 10;
            this.comboBoxTextFontChoice.SelectedIndexChanged += new System.EventHandler(this.comboBoxTextFontChoice_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Size";
            // 
            // comboBoxTextSizeChoice
            // 
            this.comboBoxTextSizeChoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTextSizeChoice.FormattingEnabled = true;
            this.comboBoxTextSizeChoice.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.comboBoxTextSizeChoice.Location = new System.Drawing.Point(50, 73);
            this.comboBoxTextSizeChoice.Name = "comboBoxTextSizeChoice";
            this.comboBoxTextSizeChoice.Size = new System.Drawing.Size(38, 21);
            this.comboBoxTextSizeChoice.TabIndex = 9;
            this.comboBoxTextSizeChoice.SelectedIndexChanged += new System.EventHandler(this.comboBoxTextSizeChoice_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numericUpDown_Y);
            this.groupBox2.Controls.Add(this.numericUpDown_X);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.shapeContainer1);
            this.groupBox2.Location = new System.Drawing.Point(7, 372);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 102);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Image Size (PIXELS)";
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox.ErrorImage")));
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox.InitialImage")));
            this.pictureBox.Location = new System.Drawing.Point(211, 53);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(15, 15);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 16;
            this.pictureBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Y";
            // 
            // numericUpDown_Y
            // 
            this.numericUpDown_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown_Y.Location = new System.Drawing.Point(20, 14);
            this.numericUpDown_Y.Name = "numericUpDown_Y";
            this.numericUpDown_Y.Size = new System.Drawing.Size(39, 20);
            this.numericUpDown_Y.TabIndex = 14;
            this.numericUpDown_Y.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDown_Y.ValueChanged += new System.EventHandler(this.numericUpDown_Y_ValueChanged);
            // 
            // numericUpDown_X
            // 
            this.numericUpDown_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown_X.Location = new System.Drawing.Point(60, 76);
            this.numericUpDown_X.Name = "numericUpDown_X";
            this.numericUpDown_X.Size = new System.Drawing.Size(39, 20);
            this.numericUpDown_X.TabIndex = 13;
            this.numericUpDown_X.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDown_X.ValueChanged += new System.EventHandler(this.numericUpDown_X_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "X";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 16);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(343, 83);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape2
            // 
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 7;
            this.lineShape2.X2 = 6;
            this.lineShape2.Y1 = 51;
            this.lineShape2.Y2 = 1;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 8;
            this.lineShape1.X2 = 55;
            this.lineShape1.Y1 = 51;
            this.lineShape1.Y2 = 51;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBoxBackgroundColor);
            this.groupBox3.Controls.Add(this.panelBackgroundColor);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(7, 89);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(363, 44);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Background Color";
            // 
            // comboBoxBackgroundColor
            // 
            this.comboBoxBackgroundColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxBackgroundColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxBackgroundColor.FormattingEnabled = true;
            this.comboBoxBackgroundColor.Location = new System.Drawing.Point(54, 17);
            this.comboBoxBackgroundColor.Name = "comboBoxBackgroundColor";
            this.comboBoxBackgroundColor.Size = new System.Drawing.Size(178, 21);
            this.comboBoxBackgroundColor.TabIndex = 6;
            this.comboBoxBackgroundColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxBackgroundColor_DrawItem);
            this.comboBoxBackgroundColor.SelectedIndexChanged += new System.EventHandler(this.comboBoxBackgroundColor_SelectedIndexChanged);
            // 
            // panelBackgroundColor
            // 
            this.panelBackgroundColor.Location = new System.Drawing.Point(236, 17);
            this.panelBackgroundColor.Name = "panelBackgroundColor";
            this.panelBackgroundColor.Size = new System.Drawing.Size(110, 21);
            this.panelBackgroundColor.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Color";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(15, 626);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(277, 627);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Save Settings";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtLonMM);
            this.groupBox5.Controls.Add(this.txtLonSS);
            this.groupBox5.Controls.Add(this.txtLonDDD);
            this.groupBox5.Controls.Add(this.comboBoxLonDirection);
            this.groupBox5.Controls.Add(this.txtLatMM);
            this.groupBox5.Controls.Add(this.txtLatSS);
            this.groupBox5.Controls.Add(this.txtLatDDD);
            this.groupBox5.Controls.Add(this.comboBoxLatDirection);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Location = new System.Drawing.Point(7, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(361, 71);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Default display origin (DDD:MM:SS) + compas direction";
            // 
            // txtLonMM
            // 
            this.txtLonMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLonMM.Location = new System.Drawing.Point(92, 41);
            this.txtLonMM.Name = "txtLonMM";
            this.txtLonMM.Size = new System.Drawing.Size(24, 20);
            this.txtLonMM.TabIndex = 9;
            // 
            // txtLonSS
            // 
            this.txtLonSS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLonSS.Location = new System.Drawing.Point(121, 41);
            this.txtLonSS.Name = "txtLonSS";
            this.txtLonSS.Size = new System.Drawing.Size(24, 20);
            this.txtLonSS.TabIndex = 8;
            // 
            // txtLonDDD
            // 
            this.txtLonDDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLonDDD.Location = new System.Drawing.Point(63, 41);
            this.txtLonDDD.Name = "txtLonDDD";
            this.txtLonDDD.Size = new System.Drawing.Size(24, 20);
            this.txtLonDDD.TabIndex = 7;
            // 
            // comboBoxLonDirection
            // 
            this.comboBoxLonDirection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxLonDirection.FormattingEnabled = true;
            this.comboBoxLonDirection.Items.AddRange(new object[] {
            "E",
            "W"});
            this.comboBoxLonDirection.Location = new System.Drawing.Point(150, 41);
            this.comboBoxLonDirection.Name = "comboBoxLonDirection";
            this.comboBoxLonDirection.Size = new System.Drawing.Size(35, 21);
            this.comboBoxLonDirection.TabIndex = 6;
            // 
            // txtLatMM
            // 
            this.txtLatMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLatMM.Location = new System.Drawing.Point(92, 16);
            this.txtLatMM.Name = "txtLatMM";
            this.txtLatMM.Size = new System.Drawing.Size(24, 20);
            this.txtLatMM.TabIndex = 5;
            // 
            // txtLatSS
            // 
            this.txtLatSS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLatSS.Location = new System.Drawing.Point(121, 16);
            this.txtLatSS.Name = "txtLatSS";
            this.txtLatSS.Size = new System.Drawing.Size(24, 20);
            this.txtLatSS.TabIndex = 4;
            // 
            // txtLatDDD
            // 
            this.txtLatDDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLatDDD.Location = new System.Drawing.Point(63, 16);
            this.txtLatDDD.Name = "txtLatDDD";
            this.txtLatDDD.Size = new System.Drawing.Size(24, 20);
            this.txtLatDDD.TabIndex = 3;
            // 
            // comboBoxLatDirection
            // 
            this.comboBoxLatDirection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxLatDirection.FormattingEnabled = true;
            this.comboBoxLatDirection.Items.AddRange(new object[] {
            "N",
            "S"});
            this.comboBoxLatDirection.Location = new System.Drawing.Point(150, 16);
            this.comboBoxLatDirection.Name = "comboBoxLatDirection";
            this.comboBoxLatDirection.Size = new System.Drawing.Size(35, 21);
            this.comboBoxLatDirection.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Longitude:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Latitude:";
            // 
            // DisplayAttibutePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(384, 656);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnUpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DisplayAttibutePicker";
            this.Text = "Display Attribute Picker";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ColorPicker_Load);
            this.panelTextAttributes.ResumeLayout(false);
            this.panelTextAttributes.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBoxLineAttributes.ResumeLayout(false);
            this.groupBoxLineAttributes.PerformLayout();
            this.groupBoxTextAttributes.ResumeLayout(false);
            this.groupBoxTextAttributes.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxTextColorChoice;
        private System.Windows.Forms.Panel panelTextAttributes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDataItem;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxTextFontChoice;
        private System.Windows.Forms.ComboBox comboBoxTextSizeChoice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown_X;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.NumericUpDown numericUpDown_Y;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxLineWidth;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBoxTextAttributes;
        private System.Windows.Forms.GroupBox groupBoxLineAttributes;
        private System.Windows.Forms.Panel panelLineAttributes;
        private System.Windows.Forms.ComboBox comboBoxLineColorChoice;
        private System.Windows.Forms.Label labelLineColor;
        private System.Windows.Forms.ComboBox comboBoxLineStyleChoice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxBackgroundColor;
        private System.Windows.Forms.Panel panelBackgroundColor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBoxAreaPolygonColorChoice;
        private System.Windows.Forms.Panel panelAreaPolygonColor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label labelSampleText;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxLatDirection;
        private System.Windows.Forms.TextBox txtLatMM;
        private System.Windows.Forms.TextBox txtLatSS;
        private System.Windows.Forms.TextBox txtLatDDD;
        private System.Windows.Forms.TextBox txtLonMM;
        private System.Windows.Forms.TextBox txtLonSS;
        private System.Windows.Forms.TextBox txtLonDDD;
        private System.Windows.Forms.ComboBox comboBoxLonDirection;
    }
}