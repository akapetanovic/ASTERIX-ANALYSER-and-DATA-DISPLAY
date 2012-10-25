namespace AsterixDisplayAnalyser
{
    partial class FrmSettings
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
            this.listBoxConnName = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxConnectionName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonSetAsActive = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelLocalInterface = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelConnAddress = new System.Windows.Forms.Label();
            this.labelConnName = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.listBoxIPAddress = new System.Windows.Forms.ListBox();
            this.listBoxPort = new System.Windows.Forms.ListBox();
            this.textBoxInterfaceAddr = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.listBoxLocalAddr = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.txtboxIPAddress.Size = new System.Drawing.Size(143, 20);
            this.txtboxIPAddress.TabIndex = 1;
            // 
            // textboxPort
            // 
            this.textboxPort.Location = new System.Drawing.Point(129, 113);
            this.textboxPort.Name = "textboxPort";
            this.textboxPort.Size = new System.Drawing.Size(143, 20);
            this.textboxPort.TabIndex = 2;
            // 
            // listBoxConnName
            // 
            this.listBoxConnName.FormattingEnabled = true;
            this.listBoxConnName.Location = new System.Drawing.Point(3, 192);
            this.listBoxConnName.Name = "listBoxConnName";
            this.listBoxConnName.Size = new System.Drawing.Size(74, 108);
            this.listBoxConnName.TabIndex = 6;
            this.listBoxConnName.SelectedIndexChanged += new System.EventHandler(this.listBoxConnName_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(159, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Multicast Addr";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Port";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 140);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(276, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(144, 306);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(135, 23);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "Delete";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
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
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.loadToolStripMenuItem.Text = "Open";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Connection name:";
            // 
            // textBoxConnectionName
            // 
            this.textBoxConnectionName.Location = new System.Drawing.Point(129, 32);
            this.textBoxConnectionName.Name = "textBoxConnectionName";
            this.textBoxConnectionName.Size = new System.Drawing.Size(143, 20);
            this.textBoxConnectionName.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Conn Name";
            // 
            // buttonSetAsActive
            // 
            this.buttonSetAsActive.Enabled = false;
            this.buttonSetAsActive.Location = new System.Drawing.Point(3, 306);
            this.buttonSetAsActive.Name = "buttonSetAsActive";
            this.buttonSetAsActive.Size = new System.Drawing.Size(135, 23);
            this.buttonSetAsActive.TabIndex = 17;
            this.buttonSetAsActive.Text = "Set as active";
            this.buttonSetAsActive.UseVisualStyleBackColor = true;
            this.buttonSetAsActive.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelLocalInterface);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.labelPort);
            this.groupBox1.Controls.Add(this.labelConnAddress);
            this.groupBox1.Controls.Add(this.labelConnName);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(3, 335);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 122);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Active connection";
            // 
            // labelLocalInterface
            // 
            this.labelLocalInterface.AutoSize = true;
            this.labelLocalInterface.Location = new System.Drawing.Point(98, 47);
            this.labelLocalInterface.Name = "labelLocalInterface";
            this.labelLocalInterface.Size = new System.Drawing.Size(27, 13);
            this.labelLocalInterface.TabIndex = 7;
            this.labelLocalInterface.Text = "N/A";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Local Interface:";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(98, 91);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(27, 13);
            this.labelPort.TabIndex = 5;
            this.labelPort.Text = "N/A";
            // 
            // labelConnAddress
            // 
            this.labelConnAddress.AutoSize = true;
            this.labelConnAddress.Location = new System.Drawing.Point(98, 69);
            this.labelConnAddress.Name = "labelConnAddress";
            this.labelConnAddress.Size = new System.Drawing.Size(27, 13);
            this.labelConnAddress.TabIndex = 4;
            this.labelConnAddress.Text = "N/A";
            // 
            // labelConnName
            // 
            this.labelConnName.AutoSize = true;
            this.labelConnName.Location = new System.Drawing.Point(98, 25);
            this.labelConnName.Name = "labelConnName";
            this.labelConnName.Size = new System.Drawing.Size(27, 13);
            this.labelConnName.TabIndex = 3;
            this.labelConnName.Text = "N/A";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Port:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Multicast Addr:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Name:";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(3, 463);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(276, 23);
            this.buttonClose.TabIndex = 19;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // listBoxIPAddress
            // 
            this.listBoxIPAddress.FormattingEnabled = true;
            this.listBoxIPAddress.Location = new System.Drawing.Point(155, 192);
            this.listBoxIPAddress.Name = "listBoxIPAddress";
            this.listBoxIPAddress.Size = new System.Drawing.Size(83, 108);
            this.listBoxIPAddress.TabIndex = 20;
            this.listBoxIPAddress.SelectedIndexChanged += new System.EventHandler(this.listBoxIPAddress_SelectedIndexChanged);
            // 
            // listBoxPort
            // 
            this.listBoxPort.FormattingEnabled = true;
            this.listBoxPort.Location = new System.Drawing.Point(238, 192);
            this.listBoxPort.Name = "listBoxPort";
            this.listBoxPort.Size = new System.Drawing.Size(41, 108);
            this.listBoxPort.TabIndex = 21;
            this.listBoxPort.SelectedIndexChanged += new System.EventHandler(this.listBoxPort_SelectedIndexChanged);
            // 
            // textBoxInterfaceAddr
            // 
            this.textBoxInterfaceAddr.Location = new System.Drawing.Point(129, 59);
            this.textBoxInterfaceAddr.Name = "textBoxInterfaceAddr";
            this.textBoxInterfaceAddr.Size = new System.Drawing.Size(143, 20);
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
            this.listBoxLocalAddr.Location = new System.Drawing.Point(76, 192);
            this.listBoxLocalAddr.Name = "listBoxLocalAddr";
            this.listBoxLocalAddr.Size = new System.Drawing.Size(83, 108);
            this.listBoxLocalAddr.TabIndex = 24;
            this.listBoxLocalAddr.SelectedIndexChanged += new System.EventHandler(this.listBoxLocalAddr_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(73, 176);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Local Addr";
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 489);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.listBoxLocalAddr);
            this.Controls.Add(this.textBoxInterfaceAddr);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.listBoxPort);
            this.Controls.Add(this.listBoxIPAddress);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonSetAsActive);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxConnectionName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBoxConnName);
            this.Controls.Add(this.textboxPort);
            this.Controls.Add(this.txtboxIPAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmSettings";
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSettings_FormClosed);
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtboxIPAddress;
        private System.Windows.Forms.TextBox textboxPort;
        private System.Windows.Forms.ListBox listBoxConnName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxConnectionName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonSetAsActive;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelConnAddress;
        private System.Windows.Forms.Label labelConnName;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ListBox listBoxIPAddress;
        private System.Windows.Forms.ListBox listBoxPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelLocalInterface;
        private System.Windows.Forms.TextBox textBoxInterfaceAddr;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox listBoxLocalAddr;
        private System.Windows.Forms.Label label12;
    }
}