namespace AsterixDisplayAnalyser
{
    partial class Debug
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxDebug1 = new System.Windows.Forms.TextBox();
            this.textBoxDebug2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxDebug1
            // 
            this.textBoxDebug1.Location = new System.Drawing.Point(35, 26);
            this.textBoxDebug1.Name = "textBoxDebug1";
            this.textBoxDebug1.Size = new System.Drawing.Size(100, 20);
            this.textBoxDebug1.TabIndex = 1;
            // 
            // textBoxDebug2
            // 
            this.textBoxDebug2.Location = new System.Drawing.Point(35, 53);
            this.textBoxDebug2.Name = "textBoxDebug2";
            this.textBoxDebug2.Size = new System.Drawing.Size(100, 20);
            this.textBoxDebug2.TabIndex = 2;
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.textBoxDebug2);
            this.Controls.Add(this.textBoxDebug1);
            this.Controls.Add(this.button1);
            this.Name = "Debug";
            this.Text = "Debug";
            this.Load += new System.EventHandler(this.Debug_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxDebug1;
        private System.Windows.Forms.TextBox textBoxDebug2;
    }
}