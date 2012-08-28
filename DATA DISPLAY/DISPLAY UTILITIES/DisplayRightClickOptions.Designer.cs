namespace MulticastingUDP
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
            this.SuspendLayout();
            // 
            // btnDisplaySettings
            // 
            this.btnDisplaySettings.Location = new System.Drawing.Point(0, 3);
            this.btnDisplaySettings.Name = "btnDisplaySettings";
            this.btnDisplaySettings.Size = new System.Drawing.Size(90, 23);
            this.btnDisplaySettings.TabIndex = 0;
            this.btnDisplaySettings.Text = "Display Settings";
            this.btnDisplaySettings.UseVisualStyleBackColor = true;
            this.btnDisplaySettings.Click += new System.EventHandler(this.btnDisplaySettings_Click);
            // 
            // btnItemsDisplay
            // 
            this.btnItemsDisplay.Location = new System.Drawing.Point(0, 32);
            this.btnItemsDisplay.Name = "btnItemsDisplay";
            this.btnItemsDisplay.Size = new System.Drawing.Size(90, 23);
            this.btnItemsDisplay.TabIndex = 1;
            this.btnItemsDisplay.Text = "Display Items";
            this.btnItemsDisplay.UseVisualStyleBackColor = true;
            this.btnItemsDisplay.Click += new System.EventHandler(this.btnItemsDisplay_Click);
            // 
            // DisplayRightClickOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(92, 59);
            this.Controls.Add(this.btnItemsDisplay);
            this.Controls.Add(this.btnDisplaySettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(98, 87);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(98, 87);
            this.Name = "DisplayRightClickOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDisplaySettings;
        private System.Windows.Forms.Button btnItemsDisplay;
    }
}