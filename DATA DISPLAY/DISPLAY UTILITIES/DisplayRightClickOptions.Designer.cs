namespace AsterixDisplayAnalyser
{
    partial class DisplayRightClickOptions
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
            this.btnDisplaySettings = new System.Windows.Forms.Button();
            this.btnItemsDisplay = new System.Windows.Forms.Button();
            this.btnLabelAttributes = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownSpeedVector = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownHistoryPoints = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeedVector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHistoryPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDisplaySettings
            // 
            this.btnDisplaySettings.BackColor = System.Drawing.Color.Black;
            this.btnDisplaySettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplaySettings.ForeColor = System.Drawing.Color.Silver;
            this.btnDisplaySettings.Location = new System.Drawing.Point(0, 3);
            this.btnDisplaySettings.Name = "btnDisplaySettings";
            this.btnDisplaySettings.Size = new System.Drawing.Size(101, 23);
            this.btnDisplaySettings.TabIndex = 0;
            this.btnDisplaySettings.Text = "Display Attributes";
            this.btnDisplaySettings.UseVisualStyleBackColor = false;
            this.btnDisplaySettings.Click += new System.EventHandler(this.btnDisplaySettings_Click);
            // 
            // btnItemsDisplay
            // 
            this.btnItemsDisplay.BackColor = System.Drawing.Color.Black;
            this.btnItemsDisplay.ForeColor = System.Drawing.Color.Silver;
            this.btnItemsDisplay.Location = new System.Drawing.Point(0, 61);
            this.btnItemsDisplay.Name = "btnItemsDisplay";
            this.btnItemsDisplay.Size = new System.Drawing.Size(101, 23);
            this.btnItemsDisplay.TabIndex = 1;
            this.btnItemsDisplay.Text = "Display Items";
            this.btnItemsDisplay.UseVisualStyleBackColor = false;
            this.btnItemsDisplay.Click += new System.EventHandler(this.btnItemsDisplay_Click);
            // 
            // btnLabelAttributes
            // 
            this.btnLabelAttributes.BackColor = System.Drawing.Color.Black;
            this.btnLabelAttributes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLabelAttributes.ForeColor = System.Drawing.Color.Silver;
            this.btnLabelAttributes.Location = new System.Drawing.Point(0, 32);
            this.btnLabelAttributes.Name = "btnLabelAttributes";
            this.btnLabelAttributes.Size = new System.Drawing.Size(101, 23);
            this.btnLabelAttributes.TabIndex = 2;
            this.btnLabelAttributes.Text = "Label Attributes";
            this.btnLabelAttributes.UseVisualStyleBackColor = false;
            this.btnLabelAttributes.Click += new System.EventHandler(this.btnLabelAttributes_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.ForeColor = System.Drawing.Color.Silver;
            this.button1.Location = new System.Drawing.Point(0, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Extended Label";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(0, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Speed vector:";
            // 
            // numericUpDownSpeedVector
            // 
            this.numericUpDownSpeedVector.Location = new System.Drawing.Point(71, 114);
            this.numericUpDownSpeedVector.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDownSpeedVector.Name = "numericUpDownSpeedVector";
            this.numericUpDownSpeedVector.Size = new System.Drawing.Size(30, 20);
            this.numericUpDownSpeedVector.TabIndex = 5;
            this.numericUpDownSpeedVector.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSpeedVector.ValueChanged += new System.EventHandler(this.numericUpDownSpeedVector_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(0, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "History points:";
            // 
            // numericUpDownHistoryPoints
            // 
            this.numericUpDownHistoryPoints.Location = new System.Drawing.Point(71, 134);
            this.numericUpDownHistoryPoints.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDownHistoryPoints.Name = "numericUpDownHistoryPoints";
            this.numericUpDownHistoryPoints.Size = new System.Drawing.Size(30, 20);
            this.numericUpDownHistoryPoints.TabIndex = 7;
            this.numericUpDownHistoryPoints.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownHistoryPoints.ValueChanged += new System.EventHandler(this.numericUpDownHistoryPoints_ValueChanged);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DarkGray;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(3, 153);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 19);
            this.button2.TabIndex = 8;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DisplayRightClickOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(103, 175);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.numericUpDownHistoryPoints);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownSpeedVector);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnLabelAttributes);
            this.Controls.Add(this.btnItemsDisplay);
            this.Controls.Add(this.btnDisplaySettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(103, 175);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(103, 175);
            this.Name = "DisplayRightClickOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DisplayRightClickOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeedVector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHistoryPoints)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDisplaySettings;
        private System.Windows.Forms.Button btnItemsDisplay;
        private System.Windows.Forms.Button btnLabelAttributes;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownSpeedVector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownHistoryPoints;
        private System.Windows.Forms.Button button2;
    }
}