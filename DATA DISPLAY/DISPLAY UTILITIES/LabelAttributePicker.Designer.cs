namespace AsterixDisplayAnalyser
{
    partial class LabelAttributePicker
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxTextColorChoice = new System.Windows.Forms.ComboBox();
            this.label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxTextFontChoice = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxTextSizeChoice = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxBackroundColor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxLineStyleChoice = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxLineColorChoice = new System.Windows.Forms.ComboBox();
            this.labelLineColor = new System.Windows.Forms.Label();
            this.comboBoxLineWidth = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxTargetSize = new System.Windows.Forms.ComboBox();
            this.comboBoxTargetStyle = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxTargetColor = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxTextColorChoice);
            this.groupBox1.Controls.Add(this.label);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxTextFontChoice);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBoxTextSizeChoice);
            this.groupBox1.Location = new System.Drawing.Point(7, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 109);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Label Text Attributes";
            // 
            // comboBoxTextColorChoice
            // 
            this.comboBoxTextColorChoice.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxTextColorChoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTextColorChoice.FormattingEnabled = true;
            this.comboBoxTextColorChoice.Location = new System.Drawing.Point(48, 19);
            this.comboBoxTextColorChoice.Name = "comboBoxTextColorChoice";
            this.comboBoxTextColorChoice.Size = new System.Drawing.Size(178, 21);
            this.comboBoxTextColorChoice.TabIndex = 11;
            this.comboBoxTextColorChoice.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxTextColorChoice_DrawItem);
            this.comboBoxTextColorChoice.SelectedIndexChanged += new System.EventHandler(this.comboBoxTextColorChoice_SelectedIndexChanged);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(7, 27);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(31, 13);
            this.label.TabIndex = 13;
            this.label.Text = "Color";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Font";
            // 
            // comboBoxTextFontChoice
            // 
            this.comboBoxTextFontChoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTextFontChoice.FormattingEnabled = true;
            this.comboBoxTextFontChoice.Location = new System.Drawing.Point(48, 46);
            this.comboBoxTextFontChoice.Name = "comboBoxTextFontChoice";
            this.comboBoxTextFontChoice.Size = new System.Drawing.Size(178, 21);
            this.comboBoxTextFontChoice.TabIndex = 17;
            this.comboBoxTextFontChoice.SelectedIndexChanged += new System.EventHandler(this.comboBoxTextFontChoice_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 15;
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
            this.comboBoxTextSizeChoice.Location = new System.Drawing.Point(48, 73);
            this.comboBoxTextSizeChoice.Name = "comboBoxTextSizeChoice";
            this.comboBoxTextSizeChoice.Size = new System.Drawing.Size(38, 21);
            this.comboBoxTextSizeChoice.TabIndex = 16;
            this.comboBoxTextSizeChoice.SelectedIndexChanged += new System.EventHandler(this.comboBoxTextSizeChoice_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxBackroundColor);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBoxLineStyleChoice);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.comboBoxLineColorChoice);
            this.groupBox2.Controls.Add(this.labelLineColor);
            this.groupBox2.Controls.Add(this.comboBoxLineWidth);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(7, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 162);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Label Box and leader line Attributes";
            // 
            // comboBoxBackroundColor
            // 
            this.comboBoxBackroundColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxBackroundColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxBackroundColor.FormattingEnabled = true;
            this.comboBoxBackroundColor.Location = new System.Drawing.Point(73, 120);
            this.comboBoxBackroundColor.Name = "comboBoxBackroundColor";
            this.comboBoxBackroundColor.Size = new System.Drawing.Size(153, 21);
            this.comboBoxBackroundColor.TabIndex = 30;
            this.comboBoxBackroundColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxBackroundColor_DrawItem);
            this.comboBoxBackroundColor.SelectedIndexChanged += new System.EventHandler(this.comboBoxBackroundColor_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Bckg Color";
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
            this.comboBoxLineStyleChoice.Location = new System.Drawing.Point(48, 51);
            this.comboBoxLineStyleChoice.Name = "comboBoxLineStyleChoice";
            this.comboBoxLineStyleChoice.Size = new System.Drawing.Size(178, 21);
            this.comboBoxLineStyleChoice.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Type";
            // 
            // comboBoxLineColorChoice
            // 
            this.comboBoxLineColorChoice.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxLineColorChoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxLineColorChoice.FormattingEnabled = true;
            this.comboBoxLineColorChoice.Location = new System.Drawing.Point(48, 24);
            this.comboBoxLineColorChoice.Name = "comboBoxLineColorChoice";
            this.comboBoxLineColorChoice.Size = new System.Drawing.Size(178, 21);
            this.comboBoxLineColorChoice.TabIndex = 25;
            this.comboBoxLineColorChoice.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxLineColorChoice_DrawItem);
            // 
            // labelLineColor
            // 
            this.labelLineColor.AutoSize = true;
            this.labelLineColor.Location = new System.Drawing.Point(7, 27);
            this.labelLineColor.Name = "labelLineColor";
            this.labelLineColor.Size = new System.Drawing.Size(31, 13);
            this.labelLineColor.TabIndex = 24;
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
            this.comboBoxLineWidth.Location = new System.Drawing.Point(48, 78);
            this.comboBoxLineWidth.Name = "comboBoxLineWidth";
            this.comboBoxLineWidth.Size = new System.Drawing.Size(38, 21);
            this.comboBoxLineWidth.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Width";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.comboBoxTargetSize);
            this.groupBox3.Controls.Add(this.comboBoxTargetStyle);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.comboBoxTargetColor);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(7, 295);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(230, 119);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Target Symbol Attributes";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Size";
            // 
            // comboBoxTargetSize
            // 
            this.comboBoxTargetSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTargetSize.FormattingEnabled = true;
            this.comboBoxTargetSize.Items.AddRange(new object[] {
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
            this.comboBoxTargetSize.Location = new System.Drawing.Point(52, 82);
            this.comboBoxTargetSize.Name = "comboBoxTargetSize";
            this.comboBoxTargetSize.Size = new System.Drawing.Size(38, 21);
            this.comboBoxTargetSize.TabIndex = 35;
            this.comboBoxTargetSize.SelectedIndexChanged += new System.EventHandler(this.comboBoxTargetSize_SelectedIndexChanged);
            // 
            // comboBoxTargetStyle
            // 
            this.comboBoxTargetStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTargetStyle.FormattingEnabled = true;
            this.comboBoxTargetStyle.Items.AddRange(new object[] {
            "Solid",
            "Dash",
            "DashDot",
            "DashDotDot",
            "Dot"});
            this.comboBoxTargetStyle.Location = new System.Drawing.Point(52, 53);
            this.comboBoxTargetStyle.Name = "comboBoxTargetStyle";
            this.comboBoxTargetStyle.Size = new System.Drawing.Size(172, 21);
            this.comboBoxTargetStyle.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Type";
            // 
            // comboBoxTargetColor
            // 
            this.comboBoxTargetColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxTargetColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTargetColor.FormattingEnabled = true;
            this.comboBoxTargetColor.Location = new System.Drawing.Point(52, 26);
            this.comboBoxTargetColor.Name = "comboBoxTargetColor";
            this.comboBoxTargetColor.Size = new System.Drawing.Size(172, 21);
            this.comboBoxTargetColor.TabIndex = 30;
            this.comboBoxTargetColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxTargetColor_DrawItem);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Color";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(127, 420);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Save Settings";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Location = new System.Drawing.Point(7, 420);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(110, 23);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "Update Display";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // LabelAttributePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(248, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LabelAttributePicker";
            this.Text = "Label Attribute Picker";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LabelAttributePicker_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox comboBoxTextColorChoice;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxTextFontChoice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxTextSizeChoice;
        private System.Windows.Forms.ComboBox comboBoxLineStyleChoice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxLineColorChoice;
        private System.Windows.Forms.Label labelLineColor;
        private System.Windows.Forms.ComboBox comboBoxLineWidth;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxBackroundColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxTargetStyle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxTargetColor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxTargetSize;
    }
}