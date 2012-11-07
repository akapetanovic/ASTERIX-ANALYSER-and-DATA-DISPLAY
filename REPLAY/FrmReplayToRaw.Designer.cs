namespace AsterixDisplayAnalyser.REPLAY
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnSource = new System.Windows.Forms.Button();
            this.btnDestination = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelSource = new System.Windows.Forms.Label();
            this.labelDestination = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(358, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Enabled = false;
            this.btnConvert.Location = new System.Drawing.Point(328, 17);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(43, 46);
            this.btnConvert.TabIndex = 1;
            this.btnConvert.Text = ">";
            this.btnConvert.UseVisualStyleBackColor = true;
            // 
            // btnSource
            // 
            this.btnSource.Location = new System.Drawing.Point(12, 12);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(76, 23);
            this.btnSource.TabIndex = 2;
            this.btnSource.Text = "Source";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // btnDestination
            // 
            this.btnDestination.Location = new System.Drawing.Point(13, 40);
            this.btnDestination.Name = "btnDestination";
            this.btnDestination.Size = new System.Drawing.Size(75, 23);
            this.btnDestination.TabIndex = 3;
            this.btnDestination.Text = "Destination";
            this.btnDestination.UseVisualStyleBackColor = true;
            this.btnDestination.Click += new System.EventHandler(this.btnDestination_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(328, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "convert";
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Location = new System.Drawing.Point(94, 17);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(80, 13);
            this.labelSource.TabIndex = 5;
            this.labelSource.Text = "Choose Source";
            this.labelSource.TextChanged += new System.EventHandler(this.labelSource_TextChanged);
            // 
            // labelDestination
            // 
            this.labelDestination.AutoSize = true;
            this.labelDestination.Location = new System.Drawing.Point(94, 45);
            this.labelDestination.Name = "labelDestination";
            this.labelDestination.Size = new System.Drawing.Size(99, 13);
            this.labelDestination.TabIndex = 6;
            this.labelDestination.Text = "Choose Destination";
            this.labelDestination.TextChanged += new System.EventHandler(this.labelDestination_TextChanged);
            // 
            // FrmReplayToRaw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 97);
            this.Controls.Add(this.labelDestination);
            this.Controls.Add(this.labelSource);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDestination);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.button1);
            this.Name = "FrmReplayToRaw";
            this.Text = "Replay to Raw";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Button btnDestination;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelSource;
        private System.Windows.Forms.Label labelDestination;
    }
}