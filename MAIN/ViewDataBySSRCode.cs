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
    public partial class ViewDataBySSRCode : Form
    {
        // Define a lookup table for all possible SSR codes, well even more
        // then all possible but lets keep it simple.
        private bool[] SSR_Code_Lookup = new bool[7778];

        public ViewDataBySSRCode()
        {
            InitializeComponent();
        }

        private void ViewDataBySSRCode_Load(object sender, EventArgs e)
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
                    if (Msg.CAT01DataItems[CAT01.ItemIDToIndex("070")].CurrentlyPresent == true)
                    {
                        CAT01I070Types.CAT01070Mode3UserData MyData = (CAT01I070Types.CAT01070Mode3UserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("070")].value;
                        int Result;
                        if (int.TryParse(MyData.Mode3A_Code, out Result) == true)
                            SSR_Code_Lookup[Result] = true;
                    }
                }
            }
            else if (MainASTERIXDataStorage.CAT48Message.Count > 0)
            {
                foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                {
                    if (Msg.CAT48DataItems[CAT48.ItemIDToIndex("070")].CurrentlyPresent == true)
                    {
                        CAT48I070Types.CAT48I070Mode3UserData MyData = (CAT48I070Types.CAT48I070Mode3UserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("070")].value;

                        int Result;
                        if (int.TryParse(MyData.Mode3A_Code, out Result) == true)
                            SSR_Code_Lookup[Result] = true;
                    }
                }
            }
            else if (MainASTERIXDataStorage.CAT62Message.Count > 0)
            {
                foreach (MainASTERIXDataStorage.CAT62Data Msg in MainASTERIXDataStorage.CAT62Message)
                {
                    if (Msg.CAT62DataItems[CAT62.ItemIDToIndex("060")].CurrentlyPresent == true)
                    {
                        CAT62I060Types.CAT62060Mode3UserData MyData = (CAT62I060Types.CAT62060Mode3UserData)Msg.CAT62DataItems[CAT62.ItemIDToIndex("060")].value;

                        int Result;
                        if (int.TryParse(MyData.Mode3A_Code, out Result) == true)
                            SSR_Code_Lookup[Result] = true;
                    }
                }
            }

            this.comboBoxSSRCode.Items.Clear();
            for (int I = 0; I < SSR_Code_Lookup.Length; I++)
            {
                if (SSR_Code_Lookup[I] == true)
                    this.comboBoxSSRCode.Items.Add(I.ToString().PadLeft(4, '0'));
            }

            if (this.comboBoxSSRCode.Items.Count > 0)
                this.comboBoxSSRCode.SelectedIndex = 0;

        }

        private void comboBoxSSRCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBoxDataBySSRCode.Items.Clear();

            if (SSR_Code_Lookup.Length > 0)
            {

                // On load determine what SSR codes are present end populate the combo box
                if (MainASTERIXDataStorage.CAT01Message.Count > 0)
                {
                    foreach (MainASTERIXDataStorage.CAT01Data Msg in MainASTERIXDataStorage.CAT01Message)
                    {
                        CAT01I070Types.CAT01070Mode3UserData MyData = (CAT01I070Types.CAT01070Mode3UserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("070")].value;

                        if (MyData.Mode3A_Code == this.comboBoxSSRCode.Items[this.comboBoxSSRCode.SelectedIndex].ToString())
                        {
                            ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT01DataItems[CAT01.ItemIDToIndex("010")].value;

                            // Display time
                            string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                                SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                            this.listBoxDataBySSRCode.Items.Add("Rcvd Time: " + Time);

                            CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates PositionData = (CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates)Msg.CAT01DataItems[CAT01.ItemIDToIndex("040")].value;
                            this.listBoxDataBySSRCode.Items.Add("\tDistance:\t" + PositionData.Measured_Distance);
                            this.listBoxDataBySSRCode.Items.Add("\tAzimuth:\t" + PositionData.Measured_Azimuth.ToString());
                            string Lat, Lon;
                            PositionData.LatLong.GetDegMinSecStringFormat(out Lat, out Lon);
                            this.listBoxDataBySSRCode.Items.Add("\tLat/Long:\t" + Lat + "/" + Lon);

                            // Display Data
                            CAT01I090Types.CAT01I090FlightLevelUserData FL_Data = (CAT01I090Types.CAT01I090FlightLevelUserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("090")].value;
                            this.listBoxDataBySSRCode.Items.Add("\tMode Validated:\t" + FL_Data.Code_Validated.ToString());
                            this.listBoxDataBySSRCode.Items.Add("\tMode Garbled:\t" + FL_Data.Code_Garbled.ToString());
                            this.listBoxDataBySSRCode.Items.Add("\tFL:\t" + FL_Data.FlightLevel.ToString());

                            this.listBoxDataBySSRCode.Items.Add("    ");


                        }
                    }
                }
                else if (MainASTERIXDataStorage.CAT48Message.Count > 0)
                {
                    foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                    {
                        CAT48I070Types.CAT48I070Mode3UserData MyData = (CAT48I070Types.CAT48I070Mode3UserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("070")].value;

                        if (MyData.Mode3A_Code == this.comboBoxSSRCode.Items[this.comboBoxSSRCode.SelectedIndex].ToString())
                        {
                            ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT48DataItems[CAT48.ItemIDToIndex("010")].value;

                            // Display time
                            string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                                SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                            this.listBoxDataBySSRCode.Items.Add("Rcvd Time: " + Time);

                            // Display Data
                            CAT48I240Types.CAT48I240ACID_Data ACID_String = (CAT48I240Types.CAT48I240ACID_Data)Msg.CAT48DataItems[CAT48.ItemIDToIndex("240")].value;
                            if (ACID_String != null)
                            this.listBoxDataBySSRCode.Items.Add("\t" + "Callsign:" + ACID_String.ACID);
                            else
                                this.listBoxDataBySSRCode.Items.Add("\t" + "Callsign: N/A");

                            CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates PositionData = (CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates)Msg.CAT48DataItems[CAT48.ItemIDToIndex("040")].value;
                            this.listBoxDataBySSRCode.Items.Add("\tDistance:\t" + PositionData.Measured_Distance);
                            this.listBoxDataBySSRCode.Items.Add("\tAzimuth:\t" + PositionData.Measured_Azimuth.ToString());
                            string Lat, Lon;
                            PositionData.LatLong.GetDegMinSecStringFormat(out Lat, out Lon);
                            this.listBoxDataBySSRCode.Items.Add("\tLat/Long:\t" + Lat + "/" + Lon);

                            CAT48I090Types.CAT48I090FlightLevelUserData FL_Data = (CAT48I090Types.CAT48I090FlightLevelUserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("090")].value;
                            this.listBoxDataBySSRCode.Items.Add("\tMode Validated:\t" + FL_Data.Code_Validated.ToString());
                            this.listBoxDataBySSRCode.Items.Add("\tMode Garbled:\t" + FL_Data.Code_Garbled.ToString());
                            this.listBoxDataBySSRCode.Items.Add("\tFL:\t" + FL_Data.FlightLevel.ToString());
                            this.listBoxDataBySSRCode.Items.Add("    ");
                        }
                    }
                }
                else if (MainASTERIXDataStorage.CAT62Message.Count > 0)
                {
                    foreach (MainASTERIXDataStorage.CAT62Data Msg in MainASTERIXDataStorage.CAT62Message)
                    {
                        CAT62I060Types.CAT62060Mode3UserData MyData = (CAT62I060Types.CAT62060Mode3UserData)Msg.CAT62DataItems[CAT62.ItemIDToIndex("060")].value;

                        if (MyData.Mode3A_Code == this.comboBoxSSRCode.Items[this.comboBoxSSRCode.SelectedIndex].ToString())
                        {
                            ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT62DataItems[CAT62.ItemIDToIndex("010")].value;

                            // TIME
                            string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                                SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                            this.listBoxDataBySSRCode.Items.Add("Rcvd Time: " + Time);
                            // TRACK NUMBER
                            int TrackNumber = (int)Msg.CAT62DataItems[CAT62.ItemIDToIndex("040")].value;
                            this.listBoxDataBySSRCode.Items.Add("\t" + "TRACK#:" + TrackNumber.ToString());
                            // CALLSIGN
                            CAT62I380Types.CAT62I380Data CAT62I380Data = (CAT62I380Types.CAT62I380Data)Msg.CAT62DataItems[CAT62.ItemIDToIndex("380")].value;
                            if (CAT62I380Data != null)
                            {
                                this.listBoxDataBySSRCode.Items.Add("\t" + "Callsign:" + CAT62I380Data.ACID.ACID_String);
                            }
                            // POSITION
                            GeoCordSystemDegMinSecUtilities.LatLongClass LatLongData = (GeoCordSystemDegMinSecUtilities.LatLongClass)Msg.CAT62DataItems[CAT62.ItemIDToIndex("105")].value;
                            string Lat, Lon;
                            LatLongData.GetDegMinSecStringFormat(out Lat, out Lon);
                            this.listBoxDataBySSRCode.Items.Add("\tLat/Long:\t" + Lat + "/" + Lon);
                            // FLIGHT LEVEL
                            double FlightLevel = (double)Msg.CAT62DataItems[CAT62.ItemIDToIndex("136")].value;
                            if (Msg.CAT62DataItems[CAT62.ItemIDToIndex("136")].value != null)
                            {
                                this.listBoxDataBySSRCode.Items.Add("\tFL:\t" + FlightLevel.ToString());
                            }
                            else
                            {
                                this.listBoxDataBySSRCode.Items.Add("\tFL:\t" + "N/A");
                            }
                            this.listBoxDataBySSRCode.Items.Add("    ");
                        }
                    }
                }
            }
            else
            {
                this.listBoxDataBySSRCode.Items.Add("No data was acquired !!!");
            }
        }
    }
}
