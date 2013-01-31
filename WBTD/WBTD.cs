using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser
{
    // This class handles output of the track data required
    // for the implementation of the Web Based Tracker Display
    // Each update cycle all the tracks are outputed into a txt file
    // in a specified directory. This file is then used by the server
    // in order to build up track display

    class WBTD
    {
        private string FileName = @"C:\ASTERIX\WBTD\Tracks.txt";
        private string TrackBuffer = "";

        // To be called with a new target data set. This one line of the data in the text file.
        public void SetTargetData(string LAT, string LON, string CALLSIGN, string ModeA, string ModeC)
        {

            TrackBuffer = TrackBuffer + LAT + "," + LON + "," + CALLSIGN + "," + ModeA + "," + ModeC + Environment.NewLine;
        }

        // To be called once Track buffer is filled out with the new
        // track data.
        public void WriteTrackData()
        {

            if (TrackBuffer.Length > 0)
            {
                // create a writer and open the file
                StreamWriter sw = new StreamWriter(FileName, false);

                try
                {
                    sw.Write(TrackBuffer);
                }
                catch (System.IO.IOException e)
                {
                    MessageBox.Show(e.Message);
                }

                // close the stream
                sw.Close();
                sw.Dispose();
            }
        }
    }
}
