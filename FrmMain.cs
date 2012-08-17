using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using GMap.NET.WindowsForms;
using GMap.NET;
using GMap.NET.MapProviders;

namespace MulticastingUDP
{

    public partial class FormMain : Form
    {
        // Dynamic Map Overlay
        GMapOverlay DinamicOverlay;
        // Static Map Overlay
        GMapOverlay StaticOverlay;

        // Keep track of the last selected SSR code index
        int SSR_Filter_Last_Index = 0;

        // Define a lookup table for all possible SSR codes, well even more
        // then all possible but lets keep it simple.
        private bool[] SSR_Code_Lookup = new bool[7778];

        // Define the main listener thread
        Thread ListenForDataThread = new Thread(new ThreadStart(ASTERIX.ListenForData));

        public FormMain()
        {
            InitializeComponent();

            RadarData.InitializeData();

            // Here call constructor 
            // for each ASTERIX type
            CAT01.Intitialize();
            CAT02.Intitialize();
            CAT08.Intitialize();
            CAT34.Intitialize();
            CAT48.Intitialize();
            CAT62.Intitialize();
            CAT63.Intitialize();
            CAT65.Intitialize();
            CAT244.Intitialize();



            // Start the thread to listen for data
            ListenForDataThread.Start();

            // Set up progress bar marguee
            this.progressBar1.Step = 2;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 100; // 100msec
            this.progressBar1.Visible = false;


        }

        public static void Intitialize()
        {

        }

        // This is a timer driven method which will update the main 
        // display box with the currently received data
        private void DataUpdateTimer_Tick(object sender, EventArgs e)
        {

            int count = SharedData.DataBox.Items.Count;
            for (int i = 0; i < count; i++)
                listBoxManFrame.Items.Add(SharedData.DataBox.Items[i]);
            SharedData.DataBox.Items.Clear();

            this.labelActiveConnName.Text = SharedData.ConnName;

            string Port;
            if (SharedData.Current_Port == 0)
            {
                Port = "N/A";
                this.buttonStopRun.Enabled = false;
            }
            else
            {
                Port = SharedData.Current_Port.ToString();
                this.buttonStopRun.Enabled = true;
            }

            this.labelConnIpAndPort.Text = SharedData.CurrentMulticastAddress.ToString() + " : " + Port;

        }

        // Display menu box to enable users to set up connection(s)
        private void connectionSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSettings SettingDialog = new FrmSettings();
            SettingDialog.Visible = false;
            SettingDialog.Show(this);
            SettingDialog.SetDesktopLocation(this.Location.X + this.Width, this.Location.Y);
            SettingDialog.Visible = true;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //Load maps
            gMapControl.Position = new PointLatLng(44.05267, 17.6769);
            this.lblCenterLat.Text = gMapControl.Position.Lat.ToString();
            this.lblCenterLon.Text = gMapControl.Position.Lng.ToString();
            gMapControl.MinZoom = 0;
            gMapControl.MapProvider = GMapProviders.GoogleMap;
            gMapControl.MaxZoom = 20;
            gMapControl.Zoom = 8;
            this.lblZoomLevel.Text = gMapControl.Zoom.ToString();
            gMapControl.Manager.Mode = AccessMode.ServerAndCache;
            DinamicOverlay = new GMapOverlay(gMapControl, "OverlayOne");
            gMapControl.Overlays.Add(DinamicOverlay);
            StaticOverlay = new GMapOverlay(gMapControl, "OverlayTwo");
            gMapControl.Overlays.Add(StaticOverlay);
            this.label9.Text = "Current rate at: " + this.PlotDisplayTimer.Interval.ToString() + "ms";
            this.comboBox1.Text = "Plain";

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Displaying an image as marker
            //
            string FileName = @"C:\Users\bhdca\Documents\Visual Studio 2010\Projects\AsterixAnalyserWithDisplay\MulticastingUDP\Images\radar.jpg";
            Image TestImage = Image.FromFile(FileName);
            
            // Hard coded to Jahorina. In the future add option to dynamically add/remove symbols
            //GPoint RadarLocation = gMapControl.FromLatLngToLocal(new PointLatLng(43.72917, 18.55167));
            //System.Drawing.Point TestPoint = new System.Drawing.Point(RadarLocation.X, RadarLocation.Y);
            GMapMarkerImage MyMarkerImage = new GMapMarkerImage(new PointLatLng(43.72917, 18.55167), TestImage);
            MyMarkerImage.ToolTipText = "Jahorina";
            MyMarkerImage.ToolTipMode = MarkerTooltipMode.Always;
            StaticOverlay.Markers.Add(MyMarkerImage);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Displaying overlay via KML file
            // Load KML-Data into program
            //string file = @"C:\Users\bhdca\Documents\Visual Studio 2010\Projects\AsterixAnalyserWithDisplay\MulticastingUDP\Images\layer6.kml";

            // Load KML-Data into program
            //KmlFile kmlData = K =mlFile.Load(file);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SharedData.bool_Listen_for_Data == true)
            {
                SharedData.bool_Listen_for_Data = false;
                buttonStopRun.Text = "Stopped";
                this.progressBar1.Visible = false;
                this.detailedViewToolStripMenuItem.Enabled = true;
                this.toolsToolStripMenuItem.Enabled = true;
                this.dataBySSRCodeToolStripMenuItem.Enabled = true;
                this.googleEarthToolStripMenuItem.Enabled = true;
            }
            else
            {
                SharedData.bool_Listen_for_Data = true;
                buttonStopRun.Text = "Running";
                this.progressBar1.Visible = true;
                this.detailedViewToolStripMenuItem.Enabled = false;
                this.toolsToolStripMenuItem.Enabled = false;
                this.dataBySSRCodeToolStripMenuItem.Enabled = false;
                this.googleEarthToolStripMenuItem.Enabled = false;
            }

            this.checkEnableDisplay.Checked = false;
            HandlePlotDisplayEnabledChanged();
        }

        // This method will analyze received, determine what data items are present and then
        // display results in a user friendly window 
        private void cAT001DataItemPresenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Define the box
            FrmDataItemPresence FrmItemPresence = new FrmDataItemPresence();

            // Set desired asterix category
            FrmItemPresence.CAT_Type_To_Analyze = SharedData.Supported_Asterix_CAT_Type.CAT001;

            // Show the dialog
            FrmItemPresence.Show(this);

        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cAT034DataItemPresenceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Define the box
            FrmDataItemPresence FrmItemPresence = new FrmDataItemPresence();

            // Set desired asterix category
            FrmItemPresence.CAT_Type_To_Analyze = SharedData.Supported_Asterix_CAT_Type.CAT034;

            // Show the dialog
            FrmItemPresence.Show(this);

        }

        private void cAT002DataItemPresenceToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            // Define the box
            FrmDataItemPresence FrmItemPresence = new FrmDataItemPresence();

            // Set desired asterix category
            FrmItemPresence.CAT_Type_To_Analyze = SharedData.Supported_Asterix_CAT_Type.CAT048;

            // Show the dialog
            FrmItemPresence.Show(this);

        }

        private void cAT002DataItemPresenceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Define the box
            FrmDataItemPresence FrmItemPresence = new FrmDataItemPresence();

            // Set desired asterix category
            FrmItemPresence.CAT_Type_To_Analyze = SharedData.Supported_Asterix_CAT_Type.CAT002;

            // Show the dialog
            FrmItemPresence.Show(this);

        }

        private void cAT008DataItemPresenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Define the box
            FrmDataItemPresence FrmItemPresence = new FrmDataItemPresence();

            // Set desired asterix category
            FrmItemPresence.CAT_Type_To_Analyze = SharedData.Supported_Asterix_CAT_Type.CAT008;

            // Show the dialog
            FrmItemPresence.Show(this);

        }

        private void cAT062DataItemPresenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Define the box
            FrmDataItemPresence FrmItemPresence = new FrmDataItemPresence();

            // Set desired asterix category
            FrmItemPresence.CAT_Type_To_Analyze = SharedData.Supported_Asterix_CAT_Type.CAT062;

            // Show the dialog
            FrmItemPresence.Show(this);
        }

        private void cAT065DataItemPresenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Define the box
            FrmDataItemPresence FrmItemPresence = new FrmDataItemPresence();

            // Set desired asterix category
            FrmItemPresence.CAT_Type_To_Analyze = SharedData.Supported_Asterix_CAT_Type.CAT065;

            // Show the dialog
            FrmItemPresence.Show(this);
        }

        private void cAT063ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Define the box
            FrmDataItemPresence FrmItemPresence = new FrmDataItemPresence();

            // Set desired asterix category
            FrmItemPresence.CAT_Type_To_Analyze = SharedData.Supported_Asterix_CAT_Type.CAT063;

            // Show the dialog
            FrmItemPresence.Show(this);
        }

        private void cAT244DataItemPresenceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Asterix Sniffer 1.2 by Amer Kapetanovic\nakapetanovic@gmail.com", "About");
        }

        private void resetDataBufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ListenigForForData = SharedData.bool_Listen_for_Data;
            if (SharedData.bool_Listen_for_Data == true)
            {
                SharedData.bool_Listen_for_Data = false;
                buttonStopRun.Text = "Stopped";
                this.progressBar1.Visible = false;
            }


            // Here reset the data buffer, this will empty data buffer.
            int NumOfItems = listBoxManFrame.Items.Count - 1;
            for (int Index = NumOfItems; Index >= 0; Index--)
                listBoxManFrame.Items.RemoveAt(Index);

            // Reset data buffer for each
            // category
            CAT01.Intitialize();
            CAT02.Intitialize();
            CAT08.Intitialize();
            CAT34.Intitialize();
            CAT48.Intitialize();
            CAT62.Intitialize();
            CAT63.Intitialize();
            CAT65.Intitialize();
            CAT244.Intitialize();

            if (ListenigForForData == true)
            {
                SharedData.bool_Listen_for_Data = true;
                buttonStopRun.Text = "Running";
                this.progressBar1.Visible = true;
            }
        }

        private void dataItem000MessageTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contourIdentifier040ContourIdentifierToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataSourceIdentifierToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataSourceIdentifierToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void CAT02MessageTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT02I000;
            MyDetailedView.Show();
        }

        private void messageTypeToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void timeofDayToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void timeOfDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT02I030;
            MyDetailedView.Show();
        }

        private void messageTypeToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void sectorNumberToolStripMenuItem1_Click(object sender, EventArgs e)
        {



        }

        private void targetReportDescriptorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT01I020;
            MyDetailedView.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            TestForm MyTest = new TestForm();
            MyTest.Show();
        }

        private void measuredPositionInPolarCoordinatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT01I040;
            MyDetailedView.Show();
        }

        private void mode3ACodeInOctalRepresentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT01I070;
            MyDetailedView.Show();
        }

        private void targetReportDescriptorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT48I020;
            MyDetailedView.Show();
        }

        private void mode3ACodeInOctalRepresentationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT48I070;
            MyDetailedView.Show();
        }

        private void flightLevelInBinaryRepresentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT48I090;
            MyDetailedView.Show();
        }

        private void modeCCodeInBinaryRepresentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT01I090;
            MyDetailedView.Show();
        }

        private void measuredPositionInSlantPolarCoordinatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT48I040;
            MyDetailedView.Show();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EarthPlotExporter MyForm = new EarthPlotExporter();
            MyForm.TypeOfExporter = EarthPlotExporter.ExporterType.EarthPlot;
            MyForm.Show();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewDataBySSRCode MyForm = new ViewDataBySSRCode();
            MyForm.Show();
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EarthPlotExporter MyForm = new EarthPlotExporter();
            MyForm.TypeOfExporter = EarthPlotExporter.ExporterType.GePath;
            MyForm.Show();
        }

        private void mode1CodeInOctalRepresentationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void radarPlotCharacteristicsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void sectorNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT02I020;
            MyDetailedView.Show();
        }

        private void antennaRotationPeriodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT02I041;
            MyDetailedView.Show();
        }

        // This is a timer that is resposible for updating the data display
        private void PlotDisplayTimer_Tick(object sender, EventArgs e)
        {
            // First clear all the data from the previous cycle.
            if (DinamicOverlay.Markers.Count > 0)
            {
                DinamicOverlay.Markers.Clear();
            }

            // Now get the data since the last cycle and display it on the map
            MapDisplayProvider DP = new MapDisplayProvider();
            System.Collections.Generic.List<MapDisplayProvider.TargetType> TargetList = new System.Collections.Generic.List<MapDisplayProvider.TargetType>();

            // Here hanlde display od live data
            if (SharedData.bool_Listen_for_Data == true)
            {
                MapDisplayProvider.GetDisplayData(false, out TargetList);

                foreach (MapDisplayProvider.TargetType Target in TargetList)
                {
                    // If SSR code filtering is to be applied 
                    if (this.checkBoxFilterBySSR.Enabled == true &&
                        this.textBoxSSRCode.Enabled == true &&
                        this.textBoxSSRCode.Text.Length == 4)
                    {
                        if (Target.ModeA == this.textBoxSSRCode.Text)
                        {
                            GMap.NET.WindowsForms.Markers.GMapMarkerCross MyMarker = new GMap.NET.WindowsForms.Markers.GMapMarkerCross(new PointLatLng(Target.Lat, Target.Lon));
                            MyMarker.ToolTipMode = MarkerTooltipMode.Always;

                            if (Target.ACID_Modes != "--------")
                                MyMarker.ToolTipText = Target.ModeA + "\n" + Target.ACID_Modes + "\n" + Target.ModeC;
                            else
                                MyMarker.ToolTipText = Target.ModeA + "\n" + Target.ModeC;

                            DinamicOverlay.Markers.Add(MyMarker);
                        }
                    }
                    else // No filter so just display all of them
                    {
                        GMap.NET.WindowsForms.Markers.GMapMarkerCross MyMarker = new GMap.NET.WindowsForms.Markers.GMapMarkerCross(new PointLatLng(Target.Lat, Target.Lon));
                        MyMarker.ToolTipMode = MarkerTooltipMode.Always;

                        if (Target.ACID_Modes != "--------")
                            MyMarker.ToolTipText = Target.ModeA + "\n" + Target.ACID_Modes + "\n" + Target.ModeC;
                        else
                            MyMarker.ToolTipText = Target.ModeA + "\n" + Target.ModeC;

                        DinamicOverlay.Markers.Add(MyMarker);
                    }
                }
            }
            else // Here hanlde display of passive display (buffered data)
            {
                MapDisplayProvider.GetDisplayData(true, out TargetList);

                foreach (MapDisplayProvider.TargetType Target in TargetList)
                {
                    // If SSR code filtering is to be applied 
                    if (this.checkBoxFilterBySSR.Checked == true && (this.comboBoxSSRFilterBox.Items.Count > 0))
                    {
                        if (Target.ModeA == this.comboBoxSSRFilterBox.Items[SSR_Filter_Last_Index].ToString())
                        {
                            GMap.NET.WindowsForms.Markers.GMapMarkerCross MyMarker = new GMap.NET.WindowsForms.Markers.GMapMarkerCross(new PointLatLng(Target.Lat, Target.Lon));
                            MyMarker.ToolTipMode = MarkerTooltipMode.Always;

                            if (Target.ACID_Modes != "--------")
                                MyMarker.ToolTipText = Target.ModeA + "\n" + Target.ACID_Modes + "\n" + Target.ModeC;
                            else
                                MyMarker.ToolTipText = Target.ModeA + "\n" + Target.ModeC;

                            DinamicOverlay.Markers.Add(MyMarker);
                        }
                    }
                    else // No filter so just display all of them
                    {
                        GMap.NET.WindowsForms.Markers.GMapMarkerCross MyMarker = new GMap.NET.WindowsForms.Markers.GMapMarkerCross(new PointLatLng(Target.Lat, Target.Lon));
                        MyMarker.ToolTipMode = MarkerTooltipMode.Always;
                        if (Target.ACID_Modes != "--------")
                            MyMarker.ToolTipText = Target.ModeA + "\n" + Target.ACID_Modes + "\n" + Target.ModeC;
                        else
                            MyMarker.ToolTipText = Target.ModeA + "\n" + Target.ModeC;
                        DinamicOverlay.Markers.Add(MyMarker);
                    }
                }
            }
        }

        private void tabPlotDisplay_Click(object sender, EventArgs e)
        {

        }

        private void checkEnableDisplay_CheckedChanged(object sender, EventArgs e)
        {
            HandlePlotDisplayEnabledChanged();
        }

        private void HandlePlotDisplayEnabledChanged()
        {
            if (this.checkEnableDisplay.Checked == true)
            {
                this.checkEnableDisplay.BackColor = Color.Green;
                this.groupBoxSSRFilter.Enabled = true;
                this.checkBoxFilterBySSR.Enabled = true;

                if (SharedData.bool_Listen_for_Data == true)
                {
                    this.checkEnableDisplay.Text = "Live Enabled";
                    this.groupBoxUpdateRate.Enabled = true;
                    this.groupBoxUpdateRate.Enabled = true;
                    this.textBoxUpdateRate.Enabled = true; ;
                }
                else
                {
                    this.checkEnableDisplay.Text = "Passive Enabled";
                    this.groupBoxUpdateRate.Enabled = false;
                    this.groupBoxUpdateRate.Enabled = false;
                    this.textBoxUpdateRate.Enabled = false;
                }

                // Start the timer
                this.PlotDisplayTimer.Enabled = true;
            }
            else
            {
                this.checkEnableDisplay.Text = "Disabled";
                this.checkEnableDisplay.BackColor = Color.Red;
                this.checkBoxFilterBySSR.BackColor = Color.Red;
                this.textBoxSSRCode.Enabled = false;
                this.textBoxUpdateRate.Enabled = false;
                this.groupBoxSSRFilter.Enabled = false;
                this.groupBoxUpdateRate.Enabled = false;
                this.checkBoxFilterBySSR.Enabled = false;
                this.checkBoxFilterBySSR.Checked = false;

                // Stop the timer
                this.PlotDisplayTimer.Enabled = false;

                // Clear the latest map display
                DinamicOverlay.Markers.Clear();
            }
        }

        private void textBoxUpdateRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            string allowedCharacterSet = "0123456789\b";    	   //Allowed character set

            if (allowedCharacterSet.Contains(e.KeyChar.ToString()))
            {

            }
            else if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;

                int UpdateRateinMS = 4000;
                if (int.TryParse(this.textBoxUpdateRate.Text, out UpdateRateinMS) == true)
                {
                    if (UpdateRateinMS > 0 && UpdateRateinMS < 100001)
                    {
                        this.PlotDisplayTimer.Interval = UpdateRateinMS;
                        this.label9.Text = "Current rate at: " + this.PlotDisplayTimer.Interval.ToString() + "ms";
                        this.textBoxUpdateRate.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Please enter an integer in range of 1000 to 10000");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter an integer in range of 1000 to 10000");
                }
            }
            else
            {
                MessageBox.Show("Please enter an integer in range of 1000 to 10000");
            }
        }

        private void checkBoxFilterBySSR_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxFilterBySSR.Checked == true)
            {
                this.checkBoxFilterBySSR.Text = "Enabled";
                this.checkBoxFilterBySSR.BackColor = Color.Green;

                if (SharedData.bool_Listen_for_Data == true)
                {
                    this.comboBoxSSRFilterBox.Enabled = false;
                    this.textBoxSSRCode.Enabled = true;
                }
                else
                {
                    this.comboBoxSSRFilterBox.Enabled = true;
                    this.textBoxSSRCode.Enabled = false;
                }
            }
            else
            {
                this.checkBoxFilterBySSR.Text = "Disbaled";
                this.checkBoxFilterBySSR.BackColor = Color.Red;
                this.comboBoxSSRFilterBox.Enabled = false;
                this.textBoxSSRCode.Enabled = false;
            }
        }

        private void comboBoxSSRFilterBox_MouseClick(object sender, MouseEventArgs e)
        {
            // Each time the form is opened reset code lookup
            // and then populate based on the latest received
            // data
            for (int I = 0; I < SSR_Code_Lookup.Length; I++)
                SSR_Code_Lookup[I] = false;

            // On load determine what SSR codes are present end populate the combo box
            if (MainDataStorage.CAT01Message.Count > 0)
            {
                foreach (MainDataStorage.CAT01Data Msg in MainDataStorage.CAT01Message)
                {
                    if (Msg.I001DataItems[CAT01.ItemIDToIndex("070")].CurrentlyPresent == true)
                    {
                        CAT01I070Types.CAT01070Mode3UserData MyData = (CAT01I070Types.CAT01070Mode3UserData)Msg.I001DataItems[CAT01.ItemIDToIndex("070")].value;
                        int Result;
                        if (int.TryParse(MyData.Mode3A_Code, out Result) == true)
                            SSR_Code_Lookup[Result] = true;
                    }
                }
            }
            else if (MainDataStorage.CAT48Message.Count > 0)
            {
                foreach (MainDataStorage.CAT48Data Msg in MainDataStorage.CAT48Message)
                {
                    if (Msg.I048DataItems[CAT48.ItemIDToIndex("070")].CurrentlyPresent == true)
                    {
                        CAT48I070Types.CAT48I070Mode3UserData MyData = (CAT48I070Types.CAT48I070Mode3UserData)Msg.I048DataItems[CAT48.ItemIDToIndex("070")].value;

                        int Result;
                        if (int.TryParse(MyData.Mode3A_Code, out Result) == true)
                            SSR_Code_Lookup[Result] = true;
                    }
                }
            }
            else
            {

            }

            this.comboBoxSSRFilterBox.Items.Clear();
            for (int I = 0; I < SSR_Code_Lookup.Length; I++)
            {
                if (SSR_Code_Lookup[I] == true)
                    this.comboBoxSSRFilterBox.Items.Add(I.ToString().PadLeft(4, '0'));
            }

            if (this.comboBoxSSRFilterBox.Items.Count > 0)
                this.comboBoxSSRFilterBox.SelectedIndex = 0;
        }

        private void comboBoxSSRFilterBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SSR_Filter_Last_Index = this.comboBoxSSRFilterBox.SelectedIndex;
        }

        private void gMapControl_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            gMapControl.Zoom = gMapControl.Zoom + 1;
            this.lblZoomLevel.Text = gMapControl.Zoom.ToString();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            gMapControl.Zoom = gMapControl.Zoom - 1;
            this.lblZoomLevel.Text = gMapControl.Zoom.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (gMapControl.Zoom > 10)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat + 0.1, gMapControl.Position.Lng);
            else if (gMapControl.Zoom > 8)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat + 0.2, gMapControl.Position.Lng);
            else
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat + 0.5, gMapControl.Position.Lng);
            this.lblCenterLat.Text = gMapControl.Position.Lat.ToString();
            this.lblCenterLon.Text = gMapControl.Position.Lng.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (gMapControl.Zoom > 10)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat - 0.1, gMapControl.Position.Lng);
            else if (gMapControl.Zoom > 8)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat - 0.2, gMapControl.Position.Lng);
            else
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat - 0.5, gMapControl.Position.Lng);
            this.lblCenterLat.Text = gMapControl.Position.Lat.ToString();
            this.lblCenterLon.Text = gMapControl.Position.Lng.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (gMapControl.Zoom > 10)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng - 0.1);
            else if (gMapControl.Zoom > 8)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng - 0.2);
            else
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng - 0.5);
            this.lblCenterLat.Text = gMapControl.Position.Lat.ToString();
            this.lblCenterLon.Text = gMapControl.Position.Lng.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (gMapControl.Zoom > 10)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng + 0.1);
            if (gMapControl.Zoom > 8)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng + 0.2);
            else
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng + 0.5);
            this.lblCenterLat.Text = gMapControl.Position.Lat.ToString();
            this.lblCenterLon.Text = gMapControl.Position.Lng.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            gMapControl.Position = new PointLatLng(44.05267, 17.6769);
            this.lblCenterLat.Text = gMapControl.Position.Lat.ToString();
            this.lblCenterLon.Text = gMapControl.Position.Lng.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "Plain")
            {
                gMapControl.MapProvider = GMapProviders.GoogleMap;
            }
            else if (this.comboBox1.Text == "Satellite")
            {
                gMapControl.MapProvider = GMapProviders.GoogleSatelliteMap;
            }
            else if (this.comboBox1.Text == "Terrain")
            {
                gMapControl.MapProvider = GMapProviders.GoogleTerrainMap;
            }
            else if (this.comboBox1.Text == "Hybrid")
            {
                gMapControl.MapProvider = GMapProviders.GoogleHybridMap;
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        // Update Mouse posistion on the control
        private void gMapControl_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void aircraftAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aircraftIdentificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT48I240;
            MyDetailedView.Show();
        }
    }
}

