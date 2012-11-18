namespace AsterixDisplayAnalyser
{
    partial class GoogleEarthProvider
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBoxDisplayAsFL = new System.Windows.Forms.CheckBox();
            this.checkBoxShowModeA = new System.Windows.Forms.CheckBox();
            this.checkBoxShowCallsign = new System.Windows.Forms.CheckBox();
            this.checkBoxShowModeC = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 110);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(245, 20);
            this.textBox1.TabIndex = 1;
            // 
            // checkBoxDisplayAsFL
            // 
            this.checkBoxDisplayAsFL.AutoSize = true;
            this.checkBoxDisplayAsFL.Location = new System.Drawing.Point(138, 42);
            this.checkBoxDisplayAsFL.Name = "checkBoxDisplayAsFL";
            this.checkBoxDisplayAsFL.Size = new System.Drawing.Size(171, 17);
            this.checkBoxDisplayAsFL.TabIndex = 5;
            this.checkBoxDisplayAsFL.Text = "Display Mode C as Flight Level";
            this.checkBoxDisplayAsFL.UseVisualStyleBackColor = true;
            this.checkBoxDisplayAsFL.CheckedChanged += new System.EventHandler(this.checkBoxDisplayAsFL_CheckedChanged);
            // 
            // checkBoxShowModeA
            // 
            this.checkBoxShowModeA.AutoSize = true;
            this.checkBoxShowModeA.Location = new System.Drawing.Point(6, 19);
            this.checkBoxShowModeA.Name = "checkBoxShowModeA";
            this.checkBoxShowModeA.Size = new System.Drawing.Size(93, 17);
            this.checkBoxShowModeA.TabIndex = 6;
            this.checkBoxShowModeA.Text = "Show Mode-A";
            this.checkBoxShowModeA.UseVisualStyleBackColor = true;
            this.checkBoxShowModeA.CheckedChanged += new System.EventHandler(this.checkBoxShowModeA_CheckedChanged);
            // 
            // checkBoxShowCallsign
            // 
            this.checkBoxShowCallsign.AutoSize = true;
            this.checkBoxShowCallsign.Location = new System.Drawing.Point(7, 65);
            this.checkBoxShowCallsign.Name = "checkBoxShowCallsign";
            this.checkBoxShowCallsign.Size = new System.Drawing.Size(92, 17);
            this.checkBoxShowCallsign.TabIndex = 7;
            this.checkBoxShowCallsign.Text = "Show Callsign";
            this.checkBoxShowCallsign.UseVisualStyleBackColor = true;
            this.checkBoxShowCallsign.CheckedChanged += new System.EventHandler(this.checkBoxShowCallsign_CheckedChanged);
            // 
            // checkBoxShowModeC
            // 
            this.checkBoxShowModeC.AutoSize = true;
            this.checkBoxShowModeC.Location = new System.Drawing.Point(6, 42);
            this.checkBoxShowModeC.Name = "checkBoxShowModeC";
            this.checkBoxShowModeC.Size = new System.Drawing.Size(93, 17);
            this.checkBoxShowModeC.TabIndex = 8;
            this.checkBoxShowModeC.Text = "Show Mode C";
            this.checkBoxShowModeC.UseVisualStyleBackColor = true;
            this.checkBoxShowModeC.CheckedChanged += new System.EventHandler(this.checkBoxShowModeC_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxShowModeA);
            this.groupBox1.Controls.Add(this.checkBoxDisplayAsFL);
            this.groupBox1.Controls.Add(this.checkBoxShowCallsign);
            this.groupBox1.Controls.Add(this.checkBoxShowModeC);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 92);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(13, 107);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 2;
            this.button4.Text = "Dest Folder";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // GoogleEarthProvider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(344, 141);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GoogleEarthProvider";
            this.Text = "Google Earth Provider";
            this.Load += new System.EventHandler(this.GoogleEarthProvider_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBoxDisplayAsFL;
        private System.Windows.Forms.CheckBox checkBoxShowModeA;
        private System.Windows.Forms.CheckBox checkBoxShowCallsign;
        private System.Windows.Forms.CheckBox checkBoxShowModeC;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
    }
}