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
            this.SuspendLayout();
            // 
            // checkBoxDisplaModeasFL
            // 
            this.checkBoxDisplaModeasFL.AutoSize = true;
            this.checkBoxDisplaModeasFL.Location = new System.Drawing.Point(12, 12);
            this.checkBoxDisplaModeasFL.Name = "checkBoxDisplaModeasFL";
            this.checkBoxDisplaModeasFL.Size = new System.Drawing.Size(129, 17);
            this.checkBoxDisplaModeasFL.TabIndex = 0;
            this.checkBoxDisplaModeasFL.Text = "Display Mode C as FL";
            this.checkBoxDisplaModeasFL.UseVisualStyleBackColor = true;
            this.checkBoxDisplaModeasFL.CheckedChanged += new System.EventHandler(this.checkBoxDisplaModeasFL_CheckedChanged);
            // 
            // MisscelaneousSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(149, 221);
            this.Controls.Add(this.checkBoxDisplaModeasFL);
            this.Name = "MisscelaneousSettings";
            this.Text = "Misscelaneous Settings";
            this.Load += new System.EventHandler(this.MisscelaneousSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxDisplaModeasFL;
    }
}