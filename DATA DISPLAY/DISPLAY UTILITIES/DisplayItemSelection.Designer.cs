namespace AsterixDisplayAnalyser
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
            this.checkBoxRunways = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxStateBorder
            // 
            this.checkBoxStateBorder.AutoSize = true;
            this.checkBoxStateBorder.Checked = true;
            this.checkBoxStateBorder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStateBorder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxStateBorder.Location = new System.Drawing.Point(12, 12);
            this.checkBoxStateBorder.Name = "checkBoxStateBorder";
            this.checkBoxStateBorder.Size = new System.Drawing.Size(82, 17);
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
            this.checkBoxSectors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxSectors.Location = new System.Drawing.Point(12, 81);
            this.checkBoxSectors.Name = "checkBoxSectors";
            this.checkBoxSectors.Size = new System.Drawing.Size(59, 17);
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
            this.checkBoxRadars.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxRadars.Location = new System.Drawing.Point(12, 58);
            this.checkBoxRadars.Name = "checkBoxRadars";
            this.checkBoxRadars.Size = new System.Drawing.Size(57, 17);
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
            this.checkBoxWaypoints.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxWaypoints.Location = new System.Drawing.Point(12, 35);
            this.checkBoxWaypoints.Name = "checkBoxWaypoints";
            this.checkBoxWaypoints.Size = new System.Drawing.Size(73, 17);
            this.checkBoxWaypoints.TabIndex = 3;
            this.checkBoxWaypoints.Text = "Waypoints";
            this.checkBoxWaypoints.UseVisualStyleBackColor = true;
            this.checkBoxWaypoints.CheckedChanged += new System.EventHandler(this.checkBoxWaypoints_CheckedChanged);
            // 
            // checkBoxRunways
            // 
            this.checkBoxRunways.AutoSize = true;
            this.checkBoxRunways.Checked = true;
            this.checkBoxRunways.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRunways.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxRunways.Location = new System.Drawing.Point(12, 104);
            this.checkBoxRunways.Name = "checkBoxRunways";
            this.checkBoxRunways.Size = new System.Drawing.Size(67, 17);
            this.checkBoxRunways.TabIndex = 5;
            this.checkBoxRunways.Text = "Runways";
            this.checkBoxRunways.UseVisualStyleBackColor = true;
            this.checkBoxRunways.CheckedChanged += new System.EventHandler(this.checkBoxRunways_CheckedChanged);
            // 
            // DisplayItemSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(103, 129);
            this.Controls.Add(this.checkBoxRunways);
            this.Controls.Add(this.checkBoxWaypoints);
            this.Controls.Add(this.checkBoxRadars);
            this.Controls.Add(this.checkBoxSectors);
            this.Controls.Add(this.checkBoxStateBorder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
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
        private System.Windows.Forms.CheckBox checkBoxRunways;
    }
}