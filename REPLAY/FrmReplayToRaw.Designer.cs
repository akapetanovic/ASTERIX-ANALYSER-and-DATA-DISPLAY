namespace AsterixDisplayAnalyser
{
    partial class FrmReplayToRaw
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
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnSource = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelSource = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConvert
            // 
            this.btnConvert.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnConvert.Enabled = false;
            this.btnConvert.Location = new System.Drawing.Point(94, 45);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(503, 21);
            this.btnConvert.TabIndex = 1;
            this.btnConvert.Text = ">";
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnSource
            // 
            this.btnSource.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnSource.Location = new System.Drawing.Point(3, 2);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(76, 34);
            this.btnSource.TabIndex = 2;
            this.btnSource.Text = "Source";
            this.btnSource.UseVisualStyleBackColor = false;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "convert";
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Location = new System.Drawing.Point(85, 23);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(99, 13);
            this.labelSource.TabIndex = 5;
            this.labelSource.Text = "Choose Source File";
            // 
            // FrmReplayToRaw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(600, 69);
            this.Controls.Add(this.labelSource);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.btnConvert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmReplayToRaw";
            this.Text = "Replay to Raw";
            this.Load += new System.EventHandler(this.FrmReplayToRaw_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelSource;
    }
}