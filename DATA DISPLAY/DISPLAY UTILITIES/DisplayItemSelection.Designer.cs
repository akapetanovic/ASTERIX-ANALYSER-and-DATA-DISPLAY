namespace MulticastingUDP
{
    partial class DisplayItemSelection
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
            this.checkBoxStateBorder = new System.Windows.Forms.CheckBox();
            this.checkBoxSectors = new System.Windows.Forms.CheckBox();
            this.checkBoxRadars = new System.Windows.Forms.CheckBox();
            this.checkBoxWaypoints = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBoxStateBorder
            // 
            this.checkBoxStateBorder.AutoSize = true;
            this.checkBoxStateBorder.Checked = true;
            this.checkBoxStateBorder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStateBorder.Location = new System.Drawing.Point(12, 12);
            this.checkBoxStateBorder.Name = "checkBoxStateBorder";
            this.checkBoxStateBorder.Size = new System.Drawing.Size(85, 17);
            this.checkBoxStateBorder.TabIndex = 0;
            this.checkBoxStateBorder.Text = "State Border";
            this.checkBoxStateBorder.UseVisualStyleBackColor = true;
            this.checkBoxStateBorder.CheckedChanged += new System.EventHandler(this.checkBoxStateBorder_CheckedChanged);
            // 
            // checkBoxSectors
            // 
            this.checkBoxSectors.AutoSize = true;
            this.checkBoxSectors.Checked = true;
            this.checkBoxSectors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSectors.Location = new System.Drawing.Point(12, 81);
            this.checkBoxSectors.Name = "checkBoxSectors";
            this.checkBoxSectors.Size = new System.Drawing.Size(62, 17);
            this.checkBoxSectors.TabIndex = 1;
            this.checkBoxSectors.Text = "Sectors";
            this.checkBoxSectors.UseVisualStyleBackColor = true;
            this.checkBoxSectors.CheckedChanged += new System.EventHandler(this.checkBoxSectors_CheckedChanged);
            // 
            // checkBoxRadars
            // 
            this.checkBoxRadars.AutoSize = true;
            this.checkBoxRadars.Checked = true;
            this.checkBoxRadars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRadars.Location = new System.Drawing.Point(12, 58);
            this.checkBoxRadars.Name = "checkBoxRadars";
            this.checkBoxRadars.Size = new System.Drawing.Size(60, 17);
            this.checkBoxRadars.TabIndex = 2;
            this.checkBoxRadars.Text = "Radars";
            this.checkBoxRadars.UseVisualStyleBackColor = true;
            this.checkBoxRadars.CheckedChanged += new System.EventHandler(this.checkBoxRadars_CheckedChanged);
            // 
            // checkBoxWaypoints
            // 
            this.checkBoxWaypoints.AutoSize = true;
            this.checkBoxWaypoints.Checked = true;
            this.checkBoxWaypoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWaypoints.Location = new System.Drawing.Point(12, 35);
            this.checkBoxWaypoints.Name = "checkBoxWaypoints";
            this.checkBoxWaypoints.Size = new System.Drawing.Size(76, 17);
            this.checkBoxWaypoints.TabIndex = 3;
            this.checkBoxWaypoints.Text = "Waypoints";
            this.checkBoxWaypoints.UseVisualStyleBackColor = true;
            this.checkBoxWaypoints.CheckedChanged += new System.EventHandler(this.checkBoxWaypoints_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 123);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DisplayItemSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(116, 158);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxWaypoints);
            this.Controls.Add(this.checkBoxRadars);
            this.Controls.Add(this.checkBoxSectors);
            this.Controls.Add(this.checkBoxStateBorder);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DisplayItemSelection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DisplayItemSelection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxStateBorder;
        private System.Windows.Forms.CheckBox checkBoxSectors;
        private System.Windows.Forms.CheckBox checkBoxRadars;
        private System.Windows.Forms.CheckBox checkBoxWaypoints;
        private System.Windows.Forms.Button button1;
    }
}