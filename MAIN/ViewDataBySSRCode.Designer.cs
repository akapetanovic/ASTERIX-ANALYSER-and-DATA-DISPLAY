namespace AsterixDisplayAnalyser
{
    partial class ViewDataBySSRCode
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxSSRCode = new System.Windows.Forms.ComboBox();
            this.listBoxDataBySSRCode = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SSR Code:";
            // 
            // comboBoxSSRCode
            // 
            this.comboBoxSSRCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxSSRCode.FormattingEnabled = true;
            this.comboBoxSSRCode.Location = new System.Drawing.Point(79, 13);
            this.comboBoxSSRCode.Name = "comboBoxSSRCode";
            this.comboBoxSSRCode.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSSRCode.TabIndex = 1;
            this.comboBoxSSRCode.SelectedIndexChanged += new System.EventHandler(this.comboBoxSSRCode_SelectedIndexChanged);
            // 
            // listBoxDataBySSRCode
            // 
            this.listBoxDataBySSRCode.FormattingEnabled = true;
            this.listBoxDataBySSRCode.Location = new System.Drawing.Point(4, 41);
            this.listBoxDataBySSRCode.Name = "listBoxDataBySSRCode";
            this.listBoxDataBySSRCode.Size = new System.Drawing.Size(420, 407);
            this.listBoxDataBySSRCode.TabIndex = 2;
            // 
            // ViewDataBySSRCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(427, 454);
            this.Controls.Add(this.listBoxDataBySSRCode);
            this.Controls.Add(this.comboBoxSSRCode);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ViewDataBySSRCode";
            this.Text = "View Data By SSRCode";
            this.Load += new System.EventHandler(this.ViewDataBySSRCode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxSSRCode;
        private System.Windows.Forms.ListBox listBoxDataBySSRCode;
    }
}