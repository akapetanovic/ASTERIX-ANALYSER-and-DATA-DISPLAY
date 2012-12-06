using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser
{
    public partial class EarthPlotExporter : Form
    {

        public enum ExporterType { EarthPlot, GePath };

        public ExporterType TypeOfExporter = ExporterType.GePath;

        // Define a lookup table for all possible SSR codes, well even more
        // then all possible but lets keep it simple.
        private bool[] SSR_Code_Lookup = new bool[7778];

        public EarthPlotExporter()
        {
            InitializeComponent();
        }

        private void EarthPlotExporter_Load(object sender, EventArgs e)
        {
            // Each time the form is opened reset code lookup
            // and then populate based on the latest received
            // data
            for (int I = 0; I < SSR_Code_Lookup.Length; I++)
                SSR_Code_Lookup[I] = false;

            // On load determine what SSR codes are present end populate the combo box
            if (MainASTERIXDataStorage.CAT01Message.Count > 0)
            {
                foreach (MainASTERIXDataStorage.CAT01Data Msg in MainASTERIXDataStorage.CAT01Message)
                {
                    CAT01I070Types.CAT01070Mode3UserData MyData = (CAT01I070Types.CAT01070Mode3UserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("070")].value;
                    int Result;
                    if (MyData != null)
                    {
                        if (int.TryParse(MyData.Mode3A_Code, out Result) == true)
                            SSR_Code_Lookup[Result] = true;
                    }
                }
            }
            else if (MainASTERIXDataStorage.CAT48Message.Count > 0)
            {
                foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                {
                    CAT48I070Types.CAT48I070Mode3UserData MyData = (CAT48I070Types.CAT48I070Mode3UserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("070")].value;

                    int Result;
                    if (MyData != null)
                    {
                        if (int.TryParse(MyData.Mode3A_Code, out Result) == true)
                            SSR_Code_Lookup[Result] = true;
                    }
                }
            }
            else if (MainASTERIXDataStorage.CAT62Message.Count > 0)
            {
                foreach (MainASTERIXDataStorage.CAT62Data Msg in MainASTERIXDataStorage.CAT62Message)
                {
                    CAT62I060Types.CAT62060Mode3UserData MyData = (CAT62I060Types.CAT62060Mode3UserData)Msg.CAT62DataItems[CAT62.ItemIDToIndex("060")].value;

                    int Result;
                    if (MyData != null)
                    {
                        if (int.TryParse(MyData.Mode3A_Code, out Result) == true)
                            SSR_Code_Lookup[Result] = true;
                    }
                }
            }

            this.comboBox1.Items.Clear();
            for (int I = 0; I < SSR_Code_Lookup.Length; I++)
            {
                if (SSR_Code_Lookup[I] == true)
                    this.comboBox1.Items.Add(I.ToString().PadLeft(4, '0'));
            }

            if (this.comboBox1.Items.Count > 0)
                this.comboBox1.SelectedIndex = 0;

            this.labelNumberofCodes.Text = this.comboBox1.Items.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Now open up the temporary file named act.in
            // Combine the new file name with the path
            string FileName = System.IO.Path.Combine(@"C:\ASTERIX\", "Asterix_Export.txt");

            // We assume the file does not exist
            System.IO.File.WriteAllText(FileName, BuildExportedData());
            MessageBox.Show("Data has been saved at C:\\ASTERIX\\Asterix_Export.txt");
            this.Close();
        }

        private string BuildExportedData()
        {
            string Data = "";
            int TargetNumber = 1;

            if (TypeOfExporter == ExporterType.GePath)
            {
                // On load determine what SSR codes are present end populate the combo box
                if (MainASTERIXDataStorage.CAT01Message.Count > 0)
                {
                    foreach (MainASTERIXDataStorage.CAT01Data Msg in MainASTERIXDataStorage.CAT01Message)
                    {
                        CAT01I070Types.CAT01070Mode3UserData Mode3AData = (CAT01I070Types.CAT01070Mode3UserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("070")].value;
                        if (Mode3AData != null)
                        {
                            if (Mode3AData.Code_Validated == CAT01I070Types.Code_Validation_Type.Code_Validated)
                            {
                                if (Mode3AData.Mode3A_Code == (string)this.comboBox1.Items[this.comboBox1.SelectedIndex])
                                {

                                    // Get Lat/Long
                                    CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates LatLongData = (CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates)Msg.CAT01DataItems[CAT01.ItemIDToIndex("040")].value;
                                    // Get Flight Level
                                    CAT01I090Types.CAT01I090FlightLevelUserData FlightLevelData = (CAT01I090Types.CAT01I090FlightLevelUserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("090")].value;

                                    double LevelInMeeters = 0.0;
                                    if (FlightLevelData != null)
                                    {
                                        try
                                        {
                                            LevelInMeeters = (FlightLevelData.FlightLevel * 100.00) * SharedData.FeetToMeeters;
                                        }
                                        catch
                                        {
                                        }
                                    }

                                    Data = Data + "P" + TargetNumber.ToString() + "," + "SSR" + Mode3AData.Mode3A_Code + "_" + TargetNumber.ToString() + "," + LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal.ToString() +
                                       "," + LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal.ToString() + "," + LevelInMeeters.ToString() + Environment.NewLine;

                                    TargetNumber++;
                                }
                            }
                        }
                    }
                }
                else if (MainASTERIXDataStorage.CAT48Message.Count > 0)
                {
                    foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                    {
                        CAT48I070Types.CAT48I070Mode3UserData Mode3AData = (CAT48I070Types.CAT48I070Mode3UserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("070")].value;
                        if (Mode3AData != null)
                        {
                            if (Mode3AData.Code_Validated == CAT48I070Types.Code_Validation_Type.Code_Validated)
                            {
                                if (Mode3AData.Mode3A_Code == (string)this.comboBox1.Items[this.comboBox1.SelectedIndex])
                                {
                                    // Get Lat/Long in decimal
                                    CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates LatLongData = (CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates)Msg.CAT48DataItems[CAT48.ItemIDToIndex("040")].value;
                                    // Get Flight Level
                                    CAT48I090Types.CAT48I090FlightLevelUserData FlightLevelData = (CAT48I090Types.CAT48I090FlightLevelUserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("090")].value;

                                    double LevelInMeeters = 0.0;
                                    if (FlightLevelData != null)
                                    {
                                        try
                                        {
                                            LevelInMeeters = (FlightLevelData.FlightLevel * 100.00) * SharedData.FeetToMeeters;
                                        }
                                        catch
                                        {

                                        }
                                    }

                                    Data = Data + "P" + TargetNumber.ToString() + "," + "SSR" + Mode3AData.Mode3A_Code + "_" + TargetNumber.ToString() + "," + LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal.ToString() +
                                        "," + LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal.ToString() + "," + LevelInMeeters.ToString() + Environment.NewLine;

                                    TargetNumber++;
                                }
                            }
                        }
                    }
                }
                else if (MainASTERIXDataStorage.CAT62Message.Count > 0)
                {
                    foreach (MainASTERIXDataStorage.CAT62Data Msg in MainASTERIXDataStorage.CAT62Message)
                    {
                        CAT62I060Types.CAT62060Mode3UserData Mode3AData = (CAT62I060Types.CAT62060Mode3UserData)Msg.CAT62DataItems[CAT62.ItemIDToIndex("060")].value;

                        if (Mode3AData != null)
                        {
                            if (Mode3AData.Mode3A_Code == (string)this.comboBox1.Items[this.comboBox1.SelectedIndex])
                            {
                                // Get Lat/Long in decimal
                                GeoCordSystemDegMinSecUtilities.LatLongClass LatLongData = (GeoCordSystemDegMinSecUtilities.LatLongClass)Msg.CAT62DataItems[CAT62.ItemIDToIndex("105")].value;

                                double FlightLevelData = 0.0;
                                if (Msg.CAT62DataItems[CAT62.ItemIDToIndex("136")].value != null)
                                {
                                    try
                                    {
                                        // Get Flight Level
                                        FlightLevelData = (double)Msg.CAT62DataItems[CAT62.ItemIDToIndex("136")].value;
                                    }
                                    catch
                                    {

                                    }
                                }

                                double LevelInMeeters = (FlightLevelData * 100.00) * SharedData.FeetToMeeters;
                                Data = Data + "P" + TargetNumber.ToString() + "," + "SSR" + Mode3AData.Mode3A_Code + "_" + TargetNumber.ToString() + "," + LatLongData.GetLatLongDecimal().LongitudeDecimal.ToString() +
                                    "," + LatLongData.GetLatLongDecimal().LatitudeDecimal.ToString() + "," + LevelInMeeters.ToString() + Environment.NewLine;

                                TargetNumber++;
                            }
                        }
                    }
                }
            }
            else
            {
                // On load determine what SSR codes are present end populate the combo box
                if (MainASTERIXDataStorage.CAT01Message.Count > 0)
                {
                    foreach (MainASTERIXDataStorage.CAT01Data Msg in MainASTERIXDataStorage.CAT01Message)
                    {
                        CAT01I070Types.CAT01070Mode3UserData Mode3AData = (CAT01I070Types.CAT01070Mode3UserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("070")].value;

                        if (Mode3AData != null)
                        {
                            if (Mode3AData.Code_Validated == CAT01I070Types.Code_Validation_Type.Code_Validated)
                            {
                                if (Mode3AData.Mode3A_Code == (string)this.comboBox1.Items[this.comboBox1.SelectedIndex])
                                {
                                    // Get Lat/Long
                                    CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates LatLongData = (CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates)Msg.CAT01DataItems[CAT01.ItemIDToIndex("040")].value;
                                    // Get Flight Level
                                    CAT01I090Types.CAT01I090FlightLevelUserData FlightLevelData = (CAT01I090Types.CAT01I090FlightLevelUserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("090")].value;

                                    double LevelInMeeters = 0.0;
                                    if (FlightLevelData != null)
                                    {
                                        try
                                        {
                                           LevelInMeeters = (FlightLevelData.FlightLevel * 100.00) * SharedData.FeetToMeeters;
                                        }
                                        catch
                                        {
                                        }
                                    }

                                    Data = Data + LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal.ToString() +
                                       "," + LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal.ToString() + "," + LevelInMeeters.ToString() + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
                else if (MainASTERIXDataStorage.CAT48Message.Count > 0)
                {
                    foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                    {
                        CAT48I070Types.CAT48I070Mode3UserData Mode3AData = (CAT48I070Types.CAT48I070Mode3UserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("070")].value;
                        if (Mode3AData != null)
                        {
                            if (Mode3AData.Code_Validated == CAT48I070Types.Code_Validation_Type.Code_Validated)
                            {

                                if (Mode3AData.Mode3A_Code == (string)this.comboBox1.Items[this.comboBox1.SelectedIndex])
                                {

                                    // Get Lat/Long in decimal
                                    CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates LatLongData = (CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates)Msg.CAT48DataItems[CAT48.ItemIDToIndex("040")].value;
                                    // Get Flight Level
                                    CAT48I090Types.CAT48I090FlightLevelUserData FlightLevelData = (CAT48I090Types.CAT48I090FlightLevelUserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("090")].value;

                                    double LevelInMeeters = 0.0;
                                    if (FlightLevelData != null)
                                    {
                                        try
                                        {
                                            LevelInMeeters = (FlightLevelData.FlightLevel * 100.00) * SharedData.FeetToMeeters;
                                        }
                                        catch
                                        {

                                        }
                                    }
                                    Data = Data + LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal.ToString() +
                                       "," + LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal.ToString() + "," + LevelInMeeters.ToString() + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
                else if (MainASTERIXDataStorage.CAT62Message.Count > 0)
                {
                    foreach (MainASTERIXDataStorage.CAT62Data Msg in MainASTERIXDataStorage.CAT62Message)
                    {
                        CAT62I060Types.CAT62060Mode3UserData Mode3AData = (CAT62I060Types.CAT62060Mode3UserData)Msg.CAT62DataItems[CAT62.ItemIDToIndex("060")].value;

                        if (Mode3AData != null)
                        {
                            if (Mode3AData.Mode3A_Code == (string)this.comboBox1.Items[this.comboBox1.SelectedIndex])
                            {
                                // Get Lat/Long in decimal
                                GeoCordSystemDegMinSecUtilities.LatLongClass LatLongData = (GeoCordSystemDegMinSecUtilities.LatLongClass)Msg.CAT62DataItems[CAT62.ItemIDToIndex("105")].value;

                                double FlightLevelData = 0.0;
                                if (Msg.CAT62DataItems[CAT62.ItemIDToIndex("136")].value != null)
                                {
                                    try
                                    {
                                        // Get Flight Level
                                        FlightLevelData = (double)Msg.CAT62DataItems[CAT62.ItemIDToIndex("136")].value;
                                    }
                                    catch
                                    {

                                    }
                                }

                                double LevelInMeeters = (FlightLevelData * 100.00) * SharedData.FeetToMeeters;

                                Data = Data + LatLongData.GetLatLongDecimal().LongitudeDecimal.ToString() +
                          "," + LatLongData.GetLatLongDecimal().LatitudeDecimal.ToString() + "," + LevelInMeeters.ToString() + Environment.NewLine;

                                TargetNumber++;
                            }
                        }
                    }
                }

            }

            return Data;
        }
    }
}
