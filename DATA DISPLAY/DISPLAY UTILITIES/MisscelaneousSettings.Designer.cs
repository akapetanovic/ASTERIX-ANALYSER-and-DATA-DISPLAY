namespace AsterixDisplayAnalyser
{
    partial class MisscelaneousSettings
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
            this.checkBoxDisplaModeasFL = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayPosInDecimals = new System.Windows.Forms.CheckBox();
            this.numericSepToolStep = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxRMCDE_ASTERIX = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericSepToolStep)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxDisplaModeasFL
            // 
            this.checkBoxDisplaModeasFL.AutoSize = true;
            this.checkBoxDisplaModeasFL.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxDisplaModeasFL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxDisplaModeasFL.Location = new System.Drawing.Point(12, 12);
            this.checkBoxDisplaModeasFL.Name = "checkBoxDisplaModeasFL";
            this.checkBoxDisplaModeasFL.Size = new System.Drawing.Size(126, 17);
            this.checkBoxDisplaModeasFL.TabIndex = 0;
            this.checkBoxDisplaModeasFL.Text = "Display Mode C as FL";
            this.checkBoxDisplaModeasFL.UseVisualStyleBackColor = false;
            this.checkBoxDisplaModeasFL.CheckedChanged += new System.EventHandler(this.checkBoxDisplaModeasFL_CheckedChanged);
            // 
            // checkBoxDisplayPosInDecimals
            // 
            this.checkBoxDisplayPosInDecimals.AutoSize = true;
            this.checkBoxDisplayPosInDecimals.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxDisplayPosInDecimals.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxDisplayPosInDecimals.Location = new System.Drawing.Point(12, 35);
            this.checkBoxDisplayPosInDecimals.Name = "checkBoxDisplayPosInDecimals";
            this.checkBoxDisplayPosInDecimals.Size = new System.Drawing.Size(151, 17);
            this.checkBoxDisplayPosInDecimals.TabIndex = 1;
            this.checkBoxDisplayPosInDecimals.Text = "Display position in decimals";
            this.checkBoxDisplayPosInDecimals.UseVisualStyleBackColor = false;
            this.checkBoxDisplayPosInDecimals.CheckedChanged += new System.EventHandler(this.checkBoxDisplayPosInDecimals_CheckedChanged);
            // 
            // numericSepToolStep
            // 
            this.numericSepToolStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericSepToolStep.Location = new System.Drawing.Point(12, 76);
            this.numericSepToolStep.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericSepToolStep.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericSepToolStep.Name = "numericSepToolStep";
            this.numericSepToolStep.Size = new System.Drawing.Size(32, 20);
            this.numericSepToolStep.TabIndex = 15;
            this.numericSepToolStep.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericSepToolStep.ValueChanged += new System.EventHandler(this.numericSepToolStep_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "SEP Tool Step";
            // 
            // checkBoxRMCDE_ASTERIX
            // 
            this.checkBoxRMCDE_ASTERIX.AutoSize = true;
            this.checkBoxRMCDE_ASTERIX.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxRMCDE_ASTERIX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxRMCDE_ASTERIX.Location = new System.Drawing.Point(12, 53);
            this.checkBoxRMCDE_ASTERIX.Name = "checkBoxRMCDE_ASTERIX";
            this.checkBoxRMCDE_ASTERIX.Size = new System.Drawing.Size(147, 17);
            this.checkBoxRMCDE_ASTERIX.TabIndex = 17;
            this.checkBoxRMCDE_ASTERIX.Text = "RMCDE header ASTERIX";
            this.checkBoxRMCDE_ASTERIX.UseVisualStyleBackColor = false;
            this.checkBoxRMCDE_ASTERIX.CheckedChanged += new System.EventHandler(this.checkBoxRMCDE_ASTERIX_CheckedChanged);
            // 
            // MisscelaneousSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(171, 108);
            this.Controls.Add(this.checkBoxRMCDE_ASTERIX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericSepToolStep);
            this.Controls.Add(this.checkBoxDisplayPosInDecimals);
            this.Controls.Add(this.checkBoxDisplaModeasFL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MisscelaneousSettings";
            this.Text = "Misscelaneous Settings";
            this.Load += new System.EventHandler(this.MisscelaneousSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericSepToolStep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxDisplaModeasFL;
        private System.Windows.Forms.CheckBox checkBoxDisplayPosInDecimals;
        private System.Windows.Forms.NumericUpDown numericSepToolStep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxRMCDE_ASTERIX;
    }
}