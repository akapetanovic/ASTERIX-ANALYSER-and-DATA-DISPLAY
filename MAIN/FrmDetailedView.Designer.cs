namespace AsterixDisplayAnalyser
{
    partial class FrmDetailedView
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
            this.listBoxMainDataBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxMainDataBox
            // 
            this.listBoxMainDataBox.FormattingEnabled = true;
            this.listBoxMainDataBox.Location = new System.Drawing.Point(12, 12);
            this.listBoxMainDataBox.Name = "listBoxMainDataBox";
            this.listBoxMainDataBox.Size = new System.Drawing.Size(457, 420);
            this.listBoxMainDataBox.TabIndex = 0;
            // 
            // FrmDetailedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(481, 447);
            this.Controls.Add(this.listBoxMainDataBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmDetailedView";
            this.Text = "Data Item Detailed View";
            this.Load += new System.EventHandler(this.FrmDetailedView_Load);
            this.Shown += new System.EventHandler(this.FrmDetailedView_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox listBoxMainDataBox;

    }
}