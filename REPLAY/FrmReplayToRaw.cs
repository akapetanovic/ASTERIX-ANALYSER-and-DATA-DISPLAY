using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AsterixDisplayAnalyser
{
    public partial class FrmReplayToRaw : Form
    {
        public FrmReplayToRaw()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "ASTERIX Analyser Files|*.rply";
            openFileDialog1.InitialDirectory = "Application.StartupPath";
            openFileDialog1.Title = "Open File to Read";

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                labelSource.Text = openFileDialog1.FileName;
                this.btnConvert.Enabled = true;
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            // File handling Source
            FileStream FileStream = null;
            BinaryReader BinaryReader = null;
            long FilePosition;
            long TotalFileSizeBytes = 0;

            // File Handling Destination
            // File stream
            Stream RecordingStream = null;
            BinaryWriter RecordingBinaryWriter = null;


            // Open up data source file
            try
            {
                //
                TotalFileSizeBytes = new System.IO.FileInfo(this.labelSource.Text).Length;

                // Open file for reading
                FileStream = new System.IO.FileStream(this.labelSource.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // attach filestream to binary reader
                BinaryReader = new System.IO.BinaryReader(FileStream);

                string Destination_Path = this.labelSource.Text;
                Destination_Path = Destination_Path.Replace(".rply", ".raw");
                RecordingStream = new FileStream(Destination_Path, FileMode.Create);
                RecordingBinaryWriter = new BinaryWriter(RecordingStream);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }

            FilePosition = 0;
            // Loop until either until the end of file is reached or user requested to stop
            while (FilePosition < TotalFileSizeBytes)
            {
                try
                {
                    // Lets determine the size of the block
                    int BlockSize = BinaryReader.ReadInt32();
                    // read time since the last data block so file read index moves to the data block
                    int TimeBetweenMessages = BinaryReader.ReadInt32();
                    // Now read the data block as indicated by the size
                    byte[] Data_Block_Buffer = BinaryReader.ReadBytes(BlockSize);
                    RecordingBinaryWriter.Write(Data_Block_Buffer);

                    // Assign file position
                    FilePosition = BinaryReader.BaseStream.Position;
                }
                catch (Exception e2)
                {
                    MessageBox.Show(e2.Message);
                }
            }

            MessageBox.Show("Succefully completed!, File saved in the same directory and name as the source with extension .raw");
        }

        private void FrmReplayToRaw_Load(object sender, EventArgs e)
        {

        }
    }
}
