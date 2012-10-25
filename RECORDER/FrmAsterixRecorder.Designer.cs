namespace AsterixDisplayAnalyser
{
    partial class FrmAsterixRecorder
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtboxIPAddress = new System.Windows.Forms.TextBox();
            this.textboxPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxConnectionName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.listBoxIPAddress = new System.Windows.Forms.ListBox();
            this.listBoxPort = new System.Windows.Forms.ListBox();
            this.textBoxInterfaceAddr = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.listBoxLocalAddr = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.checkedListBoxRecordingName = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxRecordDirectory = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Multicast Addr:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port:";
            // 
            // txtboxIPAddress
            // 
            this.txtboxIPAddress.Location = new System.Drawing.Point(129, 86);
            this.txtboxIPAddress.Name = "txtboxIPAddress";
            this.txtboxIPAddress.Size = new System.Drawing.Size(203, 20);
            this.txtboxIPAddress.TabIndex = 1;
            // 
            // textboxPort
            // 
            this.textboxPort.Location = new System.Drawing.Point(129, 113);
            this.textboxPort.Name = "textboxPort";
            this.textboxPort.Size = new System.Drawing.Size(203, 20);
            this.textboxPort.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Multicast Addr";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(287, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Port";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 140);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(152, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(335, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadToolStripMenuItem.Text = "Open";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "File name:";
            // 
            // textBoxConnectionName
            // 
            this.textBoxConnectionName.Location = new System.Drawing.Point(129, 32);
            this.textBoxConnectionName.Name = "textBoxConnectionName";
            this.textBoxConnectionName.Size = new System.Drawing.Size(203, 20);
            this.textBoxConnectionName.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "File Name";
            // 
            // listBoxIPAddress
            // 
            this.listBoxIPAddress.FormattingEnabled = true;
            this.listBoxIPAddress.Location = new System.Drawing.Point(208, 191);
            this.listBoxIPAddress.Name = "listBoxIPAddress";
            this.listBoxIPAddress.Size = new System.Drawing.Size(83, 108);
            this.listBoxIPAddress.TabIndex = 20;
            this.listBoxIPAddress.SelectedIndexChanged += new System.EventHandler(this.listBoxIPAddress_SelectedIndexChanged);
            // 
            // listBoxPort
            // 
            this.listBoxPort.FormattingEnabled = true;
            this.listBoxPort.Location = new System.Drawing.Point(291, 191);
            this.listBoxPort.Name = "listBoxPort";
            this.listBoxPort.Size = new System.Drawing.Size(41, 108);
            this.listBoxPort.TabIndex = 21;
            this.listBoxPort.SelectedIndexChanged += new System.EventHandler(this.listBoxPort_SelectedIndexChanged);
            // 
            // textBoxInterfaceAddr
            // 
            this.textBoxInterfaceAddr.Location = new System.Drawing.Point(129, 59);
            this.textBoxInterfaceAddr.Name = "textBoxInterfaceAddr";
            this.textBoxInterfaceAddr.Size = new System.Drawing.Size(203, 20);
            this.textBoxInterfaceAddr.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Local Interface Addr:";
            // 
            // listBoxLocalAddr
            // 
            this.listBoxLocalAddr.FormattingEnabled = true;
            this.listBoxLocalAddr.Location = new System.Drawing.Point(129, 191);
            this.listBoxLocalAddr.Name = "listBoxLocalAddr";
            this.listBoxLocalAddr.Size = new System.Drawing.Size(83, 108);
            this.listBoxLocalAddr.TabIndex = 24;
            this.listBoxLocalAddr.SelectedIndexChanged += new System.EventHandler(this.listBoxLocalAddr_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(126, 175);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Local Addr";
            // 
            // checkedListBoxRecordingName
            // 
            this.checkedListBoxRecordingName.FormattingEnabled = true;
            this.checkedListBoxRecordingName.Location = new System.Drawing.Point(3, 190);
            this.checkedListBoxRecordingName.Name = "checkedListBoxRecordingName";
            this.checkedListBoxRecordingName.Size = new System.Drawing.Size(126, 109);
            this.checkedListBoxRecordingName.TabIndex = 28;
            this.checkedListBoxRecordingName.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxRecordingName_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 304);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "Directory";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // textBoxRecordDirectory
            // 
            this.textBoxRecordDirectory.Location = new System.Drawing.Point(75, 306);
            this.textBoxRecordDirectory.Name = "textBoxRecordDirectory";
            this.textBoxRecordDirectory.Size = new System.Drawing.Size(257, 20);
            this.textBoxRecordDirectory.TabIndex = 30;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(248, 333);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 31;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(180, 140);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(152, 23);
            this.button3.TabIndex = 32;
            this.button3.Text = "Remove";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FrmAsterixRecorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 360);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBoxRecordDirectory);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListBoxRecordingName);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.listBoxLocalAddr);
            this.Controls.Add(this.textBoxInterfaceAddr);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.listBoxPort);
            this.Controls.Add(this.listBoxIPAddress);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxConnectionName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxPort);
            this.Controls.Add(this.txtboxIPAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmAsterixRecorder";
            this.Text = "ASTERIX Recorder";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSettings_FormClosed);
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtboxIPAddress;
        private System.Windows.Forms.TextBox textboxPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxConnectionName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listBoxIPAddress;
        private System.Windows.Forms.ListBox listBoxPort;
        private System.Windows.Forms.TextBox textBoxInterfaceAddr;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox listBoxLocalAddr;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckedListBox checkedListBoxRecordingName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxRecordDirectory;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}