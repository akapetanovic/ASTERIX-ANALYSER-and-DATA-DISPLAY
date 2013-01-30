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


namespace AsterixDisplayAnalyser
{

    public partial class FormMain : Form
    {
        ////////////////////////
        // DEBUG SECTION

        //public static int DebugX = 50;
        //public static int DebugY = 30;
        //Debug MyDebug = new Debug();

        ////////////////////////



        // Static Map Overlay
        GMapOverlay StaticOverlay;
        // Dynamic Map Overlay
        GMapOverlay DinamicOverlay;
        // Tools Map Overlay
        GMapOverlay ToolsOverlay;

        // Keep track of the last selected SSR code index
        int SSR_Filter_Last_Index = 0;

        // Define a lookup table for all possible SSR codes, well even more
        // then all possible but lets keep it simple.
        private bool[] SSR_Code_Lookup = new bool[7778];

        // Flags that the display target has just been enabled
        private bool FirstCycleDisplayEnabled = true;
        private bool CAT34_Declated_Unknown = false;

        // Define the main listener thread
        Thread ListenForDataThread = new Thread(new ThreadStart(ASTERIX.ListenForData));
        FileReadProgress ProgressForm;

        GMapMarker currentMarker = null;
        bool isDraggingMarker = false;
        Point StartMousePoint;
        int SEPToolStartTarget = -1;

        public static bool SystemCenterUpdated = false;

        // Used to flag the reception of the NM message, that will
        // then trigger update of the display
        bool North_Marker_Received = false;
        string ReadDataReportMessage;

        // Google Earth flag/methods
        //
        private static bool Send_Data_To_Google_Earth = false;

        public static bool Is_Sending_Data_To_Google_Earth()
        {
            return Send_Data_To_Google_Earth;
        }
        public static void Set_Sending_Data_To_Google_Earth(bool Is_Sending)
        {
            Send_Data_To_Google_Earth = Is_Sending;
        }

        public static void ShowExtendedLabel()
        {
            ExtendedLabel.Visible = true;
        }

        CAT34I050Types.CAT34I050UserData MyCAT34I050UserData_Last_Cycle = new CAT34I050Types.CAT34I050UserData();

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////
        /// </summary>

        private static FrmAstxRecFrwdForm AsterixRecorder = new FrmAstxRecFrwdForm();
        private static FrmReplayForm ReplayForm = new FrmReplayForm();
        private static FrmExtendedLabel ExtendedLabel = new FrmExtendedLabel();

        public FormMain()
        {
            System.Threading.Thread.Sleep(600);

            InitializeComponent();

            SystemAdaptationDataSet.InitializeData();
            DynamicDisplayBuilder.Initialise();

            // Here call constructor 
            // for each ASTERIX type
            CAT01.Intitialize(true);
            CAT02.Intitialize(true);
            CAT08.Intitialize();
            CAT34.Intitialize(true);
            CAT48.Intitialize(true);
            CAT62.Intitialize(true);
            CAT63.Intitialize();
            CAT65.Intitialize();
            CAT244.Intitialize();

            // Start the thread to listen for data
            //ListenForDataThread.Start();

            // Start display update 
            this.StaticDisplayTimer.Enabled = true;

            // Set up progress bar marguee
            this.progressBar1.Step = 2;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 100; // 100msec
            this.progressBar1.Visible = false;
        }

        public void HandleNorthMarkMessage()
        {
            North_Marker_Received = true;
        }

        public void UpdateDIsplay()
        {
            gMapControl.Refresh();
        }

        // This is a timer driven method which will update the main 
        // display box with the currently received data
        private void DataUpdateTimer_Tick(object sender, EventArgs e)
        {
            int count = SharedData.DataBox.Items.Count;
            for (int i = 0; i < count; i++)
                listBoxManFrame.Items.Add(SharedData.DataBox.Items[i]);
            SharedData.DataBox.Items.Clear();

            UpdateConnectionBoxInfo();

            if (AsterixReplay.LANReplay.GetCurrentStatus() == AsterixReplay.ReplayStatus.Replaying)
            {
                labelBytesReplayed.Text = AsterixReplay.LANReplay.GetBytesProcessedSoFar().ToString() + " Bytes";
                this.progressBarReplayActive.Visible = true;
            }
            else
            {
                this.progressBarReplayActive.Visible = false;
                labelBytesReplayed.Text = "N/A";
            }

            if (checkBoxIs_UTC.Checked)
                this.labelClock.Text = DateTime.UtcNow.ToLongTimeString();
            else
                this.labelClock.Text = DateTime.Now.ToLongTimeString();

            bool Data_Rcv_Timeout = (ASTERIX.GetTimeSpanSinceLastDataBlockRecived().Seconds > 2);
            this.labelFrozeDisplay.Visible = (Data_Rcv_Timeout && SharedData.bool_Listen_for_Data);

            if ((Data_Rcv_Timeout && SharedData.bool_Listen_for_Data) && checkBoxSystMonEnabled.Checked && !CAT34_Declated_Unknown)
            {
                HandleNoDataForCAT034I050(false);
                CAT34_Declated_Unknown = true;
            }
            else if (Data_Rcv_Timeout == false)
            {
                CAT34_Declated_Unknown = false;
            }
        }

        // Updates connection information box
        public void UpdateConnectionBoxInfo()
        {
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
            this.labelLocalInterface.Text = SharedData.CurrentInterfaceIPAddress.ToString();
            this.Text = "AMER KAPETANOVIC - ASTERIX DARR  2.7";
        }

        // Display menu box to enable users to set up connection(s)
        private void connectionSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConnectionSettings SettingDialog = new FrmConnectionSettings();
            SettingDialog.Visible = false;
            SettingDialog.Show(this);
            SettingDialog.Visible = true;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //DynamicDisplayBuilder.DebugFrame.Show();

            // Initialize Map
            gMapControl.Width = 2400;
            gMapControl.Height = 2000;
            InitializeMap();

            ToolTip toolTip1 = new ToolTip();
            toolTip1.ShowAlways = false;
            toolTip1.SetToolTip(this.lblNumberofTargets, "Number of targets since the last update cycle");

            ToolTip toolTip2 = new ToolTip();
            toolTip1.ShowAlways = false;
            toolTip1.SetToolTip(this.labelTargetCount, "Number of targets since the last update cycle");

            ToolTip toolTip3 = new ToolTip();
            toolTip3.ShowAlways = false;
            toolTip3.SetToolTip(this.labelTrackCoastLabel, "Number of update cycles before track is declared as lost");

            ToolTip toolTip4 = new ToolTip();
            toolTip4.ShowAlways = false;
            toolTip4.SetToolTip(this.labelTrackCoast, "Number of update cycles before track is declared as lost");

            ToolTip toolTip5 = new ToolTip();
            toolTip4.ShowAlways = false;
            toolTip4.SetToolTip(this.checkBoxFillListBox, "Check OFF for performance and when working with big data files");

            this.labelTrackCoast.Text = Properties.Settings.Default.TrackCoast.ToString();
            this.PlotandTrackDisplayUpdateTimer.Interval = Properties.Settings.Default.UpdateRate;
            this.labelDisplayUpdateRate.Text = "Update rate: " + this.PlotandTrackDisplayUpdateTimer.Interval.ToString() + "ms";
            this.checkEnableDisplay.Checked = Properties.Settings.Default.DisplayEnabled;
            this.checkBoxFLFilter.Checked = Properties.Settings.Default.FL_Filter_Enabled;
            this.numericUpDownUpper.Value = Properties.Settings.Default.FL_Upper;
            this.numericUpDownLower.Value = Properties.Settings.Default.FL_Lower;
            this.checkBoxDisplayPSR.Checked = Properties.Settings.Default.DisplayPSR;
            this.checkBoxFillListBox.Checked = Properties.Settings.Default.PopulateMainListBox;
            this.checkBoxSyncToNM.Checked = Properties.Settings.Default.SyncDisplayToNorthMark;
            this.checkBoxRecordInRaw.Checked = Properties.Settings.Default.RecordActiveInRaw;
            this.checkBoxSystMonEnabled.Checked = Properties.Settings.Default.SystMonEnabled;
            comboBoxLiveDisplayMode.SelectedIndex = 0;
            HandlePlotDisplayEnabledChanged();

            comboBoxLiveDisplayMode.Items.Add("Web");
            comboBoxLiveDisplayMode.Items.Add("Local & Web");
        }

        // This method populates combo box with a color coded message. 
        // "R XXXXX" in red and removes first two characters
        // "G XXXXX" in green and removes the first two characters
        // "W XXXXX" in white and removes the first two characters
        private void DrawStringandRectangleinComboBox(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                int EventIndex = ((ComboBox)sender).Items.Count - e.Index;
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 8, FontStyle.Bold);
                Color c;
                if (n[0] == 'R')
                    c = Color.Red;
                else if (n[0] == 'G')
                    c = Color.Green;
                else
                    c = Color.WhiteSmoke;

                n = n.Remove(0, 2);
                Brush b = new SolidBrush(c);

                // Append to the front event number
                n = EventIndex.ToString() + " " + n;

                g.FillRectangle(b, rect.X, rect.Y,
                                rect.Width, rect.Height);

                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
            }
        }

        private void InitializeMap()
        {
            // Set system origin position
            gMapControl.Position = new PointLatLng(SystemAdaptationDataSet.SystemOriginPoint.Lat, SystemAdaptationDataSet.SystemOriginPoint.Lng);
            UpdatelblCenter();

            gMapControl.DragButton = System.Windows.Forms.MouseButtons.Middle;

            // Choose MAP provider and MAP mode
            gMapControl.MapProvider = GMapProviders.GoogleTerrainMap;
            gMapControl.Manager.Mode = AccessMode.ServerAndCache;
            gMapControl.EmptyMapBackground = Color.Gray;

            // Set MIN/MAX for the ZOOM function
            gMapControl.MinZoom = 0;
            gMapControl.MaxZoom = 30;
            // Default ZOOM
            gMapControl.Zoom = 8;
            this.lblZoomLevel.Text = gMapControl.Zoom.ToString();

            // Add overlays
            StaticOverlay = new GMapOverlay(gMapControl, "OverlayTwo");
            gMapControl.Overlays.Add(StaticOverlay);
            DinamicOverlay = new GMapOverlay(gMapControl, "OverlayOne");
            gMapControl.Overlays.Add(DinamicOverlay);
            ToolsOverlay = new GMapOverlay(gMapControl, "OverlayThree");
            gMapControl.Overlays.Add(ToolsOverlay);

            this.labelDisplayUpdateRate.Text = "Update rate: " + this.PlotandTrackDisplayUpdateTimer.Interval.ToString() + "ms";
            this.comboBoxMapType.Text = "Plain";

            // Now build static display
            StaticDisplayBuilder.Build(ref StaticOverlay);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SharedData.bool_Listen_for_Data == true)
            {
                TerminateASTERIXListenForDataThread();
                SharedData.bool_Listen_for_Data = false;
                buttonStopRun.Text = "Stopped";
                this.progressBar1.Visible = false;
                this.detailedViewToolStripMenuItem.Enabled = true;
                this.toolsToolStripMenuItem.Enabled = true;
                this.dataBySSRCodeToolStripMenuItem.Enabled = true;
                this.googleEarthToolStripMenuItem.Enabled = true;
                this.openToolStripMenuItem.Enabled = true;
                this.checkBoxRecording.Enabled = false;
                this.checkBoxRecording.Checked = false;
                HandleNoDataForCAT034I050(false);

            }
            else
            {
                SharedData.bool_Listen_for_Data = true;
                ResetDataBuffers();
                MainASTERIXDataStorage.ResetAllData();
                buttonStopRun.Text = "Running";
                this.progressBar1.Visible = true;
                this.detailedViewToolStripMenuItem.Enabled = false;
                this.toolsToolStripMenuItem.Enabled = false;
                this.dataBySSRCodeToolStripMenuItem.Enabled = false;
                this.googleEarthToolStripMenuItem.Enabled = false;
                this.openToolStripMenuItem.Enabled = false;
                this.checkBoxRecording.Enabled = true;
                SetNewConnection();
                Thread ListenForDataThread = new Thread(new ThreadStart(ASTERIX.ListenForData));
                ListenForDataThread.Start();
            }

            HandlePlotDisplayEnabledChanged();
        }

        private void SetNewConnection()
        {
            if (ASTERIX.ReinitializeSocket() != true)
            {
                SharedData.ResetConnectionParameters();
            }
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
            FrmAbout FAbout = new FrmAbout();
            FAbout.ShowDialog();
        }

        private void resetDataBufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetDataBuffers();
            MainASTERIXDataStorage.ResetAllData();
            Update_PlotTrack_Data();
        }

        private void ResetDataBuffers()
        {
            bool ListenigForData = SharedData.bool_Listen_for_Data;
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

            DynamicDisplayBuilder.ResetGetDataIndex = 0;
            DynamicDisplayBuilder.Initialise();

            // Reset data buffer for each
            // category
            CAT01.Intitialize(true);
            CAT02.Intitialize(true);
            CAT08.Intitialize();
            CAT34.Intitialize(true);
            CAT48.Intitialize(true);
            CAT62.Intitialize(true);
            CAT63.Intitialize();
            CAT65.Intitialize();
            CAT244.Intitialize();

            if (ListenigForData == true)
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
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT34I030;
            MyDetailedView.Show();
        }

        private void timeOfDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT02I030;
            MyDetailedView.Show();
        }

        private void messageTypeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT34I000;
            MyDetailedView.Show();
        }

        private void sectorNumberToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT34I020;
            MyDetailedView.Show();
        }

        private void targetReportDescriptorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT01I020;
            MyDetailedView.Show();
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
            Update_PlotTrack_Data();
        }

        private void Update_PlotTrack_Data()
        {
            if (this.checkEnableDisplay.Checked == true)
            {

                // First clear all the data from the previous cycle.
                if (DinamicOverlay.Markers.Count > 0)
                {
                    DinamicOverlay.Markers.Clear();
                }

                // Now get the data since the last cycle and display it on the map
                DynamicDisplayBuilder DP = new DynamicDisplayBuilder();
                System.Collections.Generic.List<DynamicDisplayBuilder.TargetType> TargetList = new System.Collections.Generic.List<DynamicDisplayBuilder.TargetType>();

                // Here hanlde display od live data
                if (SharedData.bool_Listen_for_Data == true)
                {
                    DynamicDisplayBuilder.GetDisplayData(false, out TargetList);

                    if (FirstCycleDisplayEnabled)
                    {
                        FirstCycleDisplayEnabled = false;
                        TargetList.Clear();
                    }

                    this.lblNumberofTargets.Text = TargetList.Count.ToString();

                    bool Build_Local_Display = comboBoxLiveDisplayMode.Text != "Google Earth";
                    bool Provide_To_Google_Earth = comboBoxLiveDisplayMode.Text != "Local";
                    bool ProvideWebData = comboBoxLiveDisplayMode.Text == "Local & Web" || comboBoxLiveDisplayMode.Text == "Web";
                    Asterix_To_KML_Provider ASTX_TO_KML = new Asterix_To_KML_Provider();
                    WBTD WebBasedDisplayProvider = new WBTD();

                    foreach (DynamicDisplayBuilder.TargetType Target in TargetList)
                    {
                        if (Passes_Check_For_Flight_Level_Filter(Target.ModeC))
                        {
                            // If SSR code filtering is to be applied 
                            if (this.checkBoxFilterBySSR.Checked == true && (this.textBoxSSRCode.Text.Length == 4))
                            {
                                if (Target.ModeA == this.textBoxSSRCode.Text)
                                {
                                    Target.MyMarker.ToolTipMode = MarkerTooltipMode.Never;
                                    Target.MyMarker.Position = new PointLatLng(Target.Lat, Target.Lon);
                                    BuildDynamicLabelText(Target, ref Target.MyMarker);
                                    SetLabelAttributes(ref Target.MyMarker);

                                    if (Build_Local_Display)
                                        DinamicOverlay.Markers.Add(Target.MyMarker);

                                    if (Provide_To_Google_Earth)
                                        ASTX_TO_KML.AddNewTarget(Target);

                                    if (ProvideWebData)
                                        WebBasedDisplayProvider.SetTargetData(Target.Lat.ToString(), Target.Lon.ToString(), Target.ACID_Mode_S,
                                            Target.ModeA, Target.ModeC);
                                }
                            }
                            else // No SSR filter so just display all of them
                            {
                                Target.MyMarker.ToolTipMode = MarkerTooltipMode.Never;
                                Target.MyMarker.Position = new PointLatLng(Target.Lat, Target.Lon);
                                BuildDynamicLabelText(Target, ref Target.MyMarker);
                                SetLabelAttributes(ref Target.MyMarker);

                                if (Build_Local_Display)
                                    DinamicOverlay.Markers.Add(Target.MyMarker);

                                if (Provide_To_Google_Earth)
                                    ASTX_TO_KML.AddNewTarget(Target);

                                if (ProvideWebData)
                                    WebBasedDisplayProvider.SetTargetData(Target.Lat.ToString(), Target.Lon.ToString(), Target.ACID_Mode_S,
                                        Target.ModeA, Target.ModeC);
                            }
                        }
                    }

                    Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y);
                    Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y);

                    // Check if there were any items, if so then tell KML to build the file
                    if (Provide_To_Google_Earth)
                        ASTX_TO_KML.BuildKML();

                    if (ProvideWebData)
                        WebBasedDisplayProvider.WriteTrackData();
                        
                }
                else // Here handle display of passive display (buffered data)
                {
                    DynamicDisplayBuilder.GetDisplayData(true, out TargetList);

                    this.lblNumberofTargets.Text = TargetList.Count.ToString();

                    foreach (DynamicDisplayBuilder.TargetType Target in TargetList)
                    {
                        if (Passes_Check_For_Flight_Level_Filter(Target.ModeC))
                        {
                            // If SSR code filtering is to be applied 
                            if (this.checkBoxFilterBySSR.Checked == true && (this.comboBoxSSRFilterBox.Items.Count > 0))
                            {
                                if (Target.ModeA == this.comboBoxSSRFilterBox.Items[SSR_Filter_Last_Index].ToString())
                                {
                                    GMap.NET.WindowsForms.Markers.GMapMarkerCross MyMarker = new GMap.NET.WindowsForms.Markers.GMapMarkerCross(new PointLatLng(Target.Lat, Target.Lon));
                                    MyMarker.ToolTipMode = MarkerTooltipMode.Always;
                                    MyMarker.ToolTipText = BuildPassiveLabelText(Target);
                                    SetLabelAttributes(ref MyMarker);
                                    DinamicOverlay.Markers.Add(MyMarker);
                                }
                            }
                            else // No filter so just display all of them
                            {
                                GMap.NET.WindowsForms.Markers.GMapMarkerCross MyMarker = new GMap.NET.WindowsForms.Markers.GMapMarkerCross(new PointLatLng(Target.Lat, Target.Lon));
                                MyMarker.ToolTipMode = MarkerTooltipMode.Always;
                                MyMarker.ToolTipText = BuildPassiveLabelText(Target);
                                SetLabelAttributes(ref MyMarker);
                                DinamicOverlay.Markers.Add(MyMarker);
                            }
                        }
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////
        // This method builds the label text
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="Target_In"></param>
        /// <returns></returns>
        private void BuildDynamicLabelText(DynamicDisplayBuilder.TargetType Target_Data, ref GMapTargetandLabel Label_Data)
        {
            string CoastIndicator;
            if (Target_Data.TrackTerminateTreshold > 1)
                CoastIndicator = " ↘";
            else
                CoastIndicator = "";

            Label_Data.ModeA_CI_STRING = Target_Data.ModeA + CoastIndicator;

            Label_Data.ModeC_STRING = ApplyCModeHisterysis(Target_Data.ModeC);

            if (Target_Data.ModeC_Previous_Cycle != null && Target_Data.ModeC != null && Target_Data.ModeC_Previous_Cycle != "")
            {
                try
                {
                    if (double.Parse(Target_Data.ModeC_Previous_Cycle) > double.Parse(Target_Data.ModeC))
                        Label_Data.ModeC_STRING = Label_Data.ModeC_STRING + "↓";
                    else if (double.Parse(Target_Data.ModeC_Previous_Cycle) < double.Parse(Target_Data.ModeC))
                        Label_Data.ModeC_STRING = Label_Data.ModeC_STRING + "↑";
                }
                catch
                {
                    Label_Data.ModeC_STRING = "---";
                }
            }

            if (Target_Data.ACID_Mode_S != null)
                Label_Data.CALLSIGN_STRING = Target_Data.ACID_Mode_S;

            Label_Data.MyTargetIndex = Target_Data.TrackNumber;

            // At the end move extended lable data to the marker, so it is ready for dynamic manipulation by the client
            Label_Data.Mode_S_Addr = Target_Data.Mode_S_Addr;
            Label_Data.TRK = Target_Data.TRK;
            Label_Data.DAP_HDG = Target_Data.DAP_HDG;
            Label_Data.CALC_HDG_STRING = Target_Data.CALC_HDG;
            Label_Data.IAS = Target_Data.IAS;
            Label_Data.MACH = Target_Data.MACH;
            Label_Data.TAS = Target_Data.TAS;
            Label_Data.CALC_GSPD_STRING = Target_Data.CALC_GSPD;
            Label_Data.DAP_GSPD = Target_Data.DAP_GSPD;
            Label_Data.Roll_Angle = Target_Data.Roll_Ang;
            Label_Data.SelectedAltitude_ShortTerm = Target_Data.SelectedAltitude_ShortTerm;
            Label_Data.SelectedAltitude_LongTerm = Target_Data.SelectedAltitude_LongTerm;
            Label_Data.Rate_Of_Climb = Target_Data.Rate_Of_Climb;
            Label_Data.Barometric_Setting = Target_Data.Barometric_Setting;

            // Set STCA parameters
            foreach (STCA_Target_Item STCA_Item in Target_Data.STCA_List)
                Label_Data.STCA_List.Add(STCA_Item);
        }

        private string ApplyCModeHisterysis(string Mode_C_In)
        {
            string Result = Mode_C_In;
            double FL;

            if (Properties.Settings.Default.DisplayModeC_As_FL == true)
            {
                if (double.TryParse(Mode_C_In, out FL) == true)
                {
                    int Truncated = (int)FL;
                    Result = Truncated.ToString();
                }
            }
            return Result;
        }

        private string BuildPassiveLabelText(DynamicDisplayBuilder.TargetType Target_In)
        {
            string Label_Text_Out = "";

            if (Target_In.ACID_Mode_S != null)
                Label_Text_Out = Target_In.ModeA + "\n" + Target_In.ACID_Mode_S + "\n" + ApplyCModeHisterysis(Target_In.ModeC);
            else
                Label_Text_Out = Target_In.ModeA + "\n" + ApplyCModeHisterysis(Target_In.ModeC);

            return Label_Text_Out;
        }

        private void SetLabelAttributes(ref GMapTargetandLabel Marker_In)
        {
            // Label Text Font and Size
            Marker_In.ModeA_CI_FONT = new Font(LabelAttributes.TextFont, LabelAttributes.TextSize,
            FontStyle.Bold | FontStyle.Regular);
            Marker_In.ModeA_CI_BRUSH = new SolidBrush(LabelAttributes.TextColor);

            Marker_In.CALLSIGN_FONT = new Font(LabelAttributes.TextFont, LabelAttributes.TextSize,
            FontStyle.Bold | FontStyle.Regular);
            Marker_In.CALLSIGN_BRUSH = new SolidBrush(LabelAttributes.TextColor);

            Marker_In.ModeC_FONT = new Font(LabelAttributes.TextFont, LabelAttributes.TextSize,
            FontStyle.Bold | FontStyle.Regular);
            Marker_In.ModeC_BRUSH = new SolidBrush(LabelAttributes.TextColor);

            Marker_In.GSPD_FONT = new Font(LabelAttributes.TextFont, LabelAttributes.TextSize,
            FontStyle.Bold | FontStyle.Regular);
            Marker_In.GSPD_BRUSH = new SolidBrush(LabelAttributes.TextColor);

            Marker_In.CFL_FONT = new Font(LabelAttributes.TextFont, LabelAttributes.TextSize,
           FontStyle.Bold | FontStyle.Regular);
            Marker_In.CFL_BRUSH = new SolidBrush(LabelAttributes.TextColor);

            Marker_In.A_HDG_FONT = new Font(LabelAttributes.TextFont, LabelAttributes.TextSize,
            FontStyle.Bold | FontStyle.Regular);
            Marker_In.A_HDG_BRUSH = new SolidBrush(LabelAttributes.TextColor);

            Marker_In.A_ROC_FONT = new Font(LabelAttributes.TextFont, LabelAttributes.TextSize,
            FontStyle.Bold | FontStyle.Regular);
            Marker_In.A_ROC_BRUSH = new SolidBrush(LabelAttributes.TextColor);

            Marker_In.A_SPD_FONT = new Font(LabelAttributes.TextFont, LabelAttributes.TextSize,
            FontStyle.Bold | FontStyle.Regular);
            Marker_In.A_SPD_BRUSH = new SolidBrush(LabelAttributes.TextColor);
        }

        private void SetLabelAttributes(ref GMap.NET.WindowsForms.Markers.GMapMarkerCross Marker_In)
        {
            // Label Text Font and Size
            Marker_In.ToolTip.Font = new Font(LabelAttributes.TextFont, LabelAttributes.TextSize,
            FontStyle.Bold | FontStyle.Regular);
            Marker_In.ToolTip.Foreground = new SolidBrush(LabelAttributes.TextColor);

            // Label Border color
            Marker_In.ToolTip.Stroke = new Pen(new SolidBrush(LabelAttributes.LineColor), LabelAttributes.LineWidth);
            Marker_In.ToolTip.Stroke.DashStyle = LabelAttributes.LineStyle;

            // Label background color
            Marker_In.ToolTip.Fill = Brushes.Transparent;

            // Symbol color
            Marker_In.Pen = new Pen(new SolidBrush(LabelAttributes.TargetColor), LabelAttributes.TargetSize);
            Marker_In.Pen.DashStyle = LabelAttributes.TargetStyle;

            // Align the text
            Marker_In.ToolTip.Format.LineAlignment = StringAlignment.Center;
            Marker_In.ToolTip.Format.Alignment = StringAlignment.Near;
        }

        private bool Passes_Check_For_Flight_Level_Filter(string Flight_Level)
        {
            bool Result = true;
            if (this.checkBoxFLFilter.Checked)
            {
                try
                {
                    double FL = double.Parse(Flight_Level);
                    if (FL < (double)this.numericUpDownLower.Value || FL > (double)this.numericUpDownUpper.Value)
                        Result = false;
                }
                catch
                {
                    Result = false;
                }
            }


            return Result;
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
            if (this.checkEnableDisplay.Checked == true || Send_Data_To_Google_Earth == true)
            {
                this.checkBoxFilterBySSR.Enabled = true;
                this.checkEnableDisplay.BackColor = Color.Green;

                if (SharedData.bool_Listen_for_Data == true)
                {
                    this.checkEnableDisplay.Text = "Live Enabled";

                    // Start the timer if Sync to coast is not set
                    if (this.checkBoxSyncToNM.Checked == false)
                    {
                        this.PlotandTrackDisplayUpdateTimer.Enabled = true;
                        this.textBoxUpdateRate.Enabled = true;
                        this.labelDisplayUpdateRate.Enabled = true;
                    }
                    else
                    {
                        this.PlotandTrackDisplayUpdateTimer.Enabled = false;
                        this.labelDisplayUpdateRate.Enabled = false;
                    }

                    this.checkBoxSyncToNM.Enabled = true;
                    this.textBox1TrackCoast.Enabled = true;
                    this.comboBoxSSRFilterBox.Enabled = false;
                }
                else
                {
                    this.checkEnableDisplay.Text = "Passive Enabled";
                    this.textBoxUpdateRate.Enabled = false;
                    this.checkBoxSyncToNM.Enabled = false;
                    this.textBox1TrackCoast.Enabled = false;
                    this.comboBoxSSRFilterBox.Enabled = true;
                }

                Update_PlotTrack_Data();
            }
            else
            {
                FirstCycleDisplayEnabled = true;
                this.checkBoxFilterBySSR.Enabled = false;
                this.textBoxUpdateRate.Enabled = false;
                this.checkBoxSyncToNM.Enabled = false;
                this.textBox1TrackCoast.Enabled = false;
                this.checkEnableDisplay.BackColor = Color.Red;
                this.checkEnableDisplay.Text = "Disabled";

                // Clear the latest map display
                if (DinamicOverlay != null)
                    DinamicOverlay.Markers.Clear();
            }

            Properties.Settings.Default.DisplayEnabled = this.checkEnableDisplay.Checked;
            Properties.Settings.Default.Save();
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

                int UpdateRateinMS = Properties.Settings.Default.UpdateRate;
                if (int.TryParse(this.textBoxUpdateRate.Text, out UpdateRateinMS) == true)
                {
                    if (UpdateRateinMS > 0 && UpdateRateinMS < 100001)
                    {
                        Properties.Settings.Default.UpdateRate = UpdateRateinMS;
                        Properties.Settings.Default.Save();
                        this.PlotandTrackDisplayUpdateTimer.Interval = UpdateRateinMS;
                        this.labelDisplayUpdateRate.Text = "Update rate: " + this.PlotandTrackDisplayUpdateTimer.Interval.ToString() + "ms";
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
                this.checkBoxFilterBySSR.BackColor = Color.Red;
            }
            else
            {
                this.checkBoxFilterBySSR.Text = "Disabled";
                this.checkBoxFilterBySSR.BackColor = Color.Transparent;
            }

            Update_PlotTrack_Data();
        }

        private void comboBoxSSRFilterBox_MouseClick(object sender, MouseEventArgs e)
        {
            PopulateSSRCodeLookup();
        }

        private void PopulateSSRCodeLookup()
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
            Update_PlotTrack_Data();
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
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat - 0.1, gMapControl.Position.Lng);
            else if (gMapControl.Zoom > 8)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat - 0.2, gMapControl.Position.Lng);
            else
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat - 0.5, gMapControl.Position.Lng);

            UpdatelblCenter();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (gMapControl.Zoom > 10)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat + 0.1, gMapControl.Position.Lng);
            else if (gMapControl.Zoom > 8)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat + 0.2, gMapControl.Position.Lng);
            else
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat + 0.5, gMapControl.Position.Lng);

            UpdatelblCenter();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (gMapControl.Zoom > 10)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng + 0.1);
            if (gMapControl.Zoom > 8)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng + 0.2);
            else
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng + 0.5);
            UpdatelblCenter();
        }

        private void UpdatelblCenter()
        {
            GeoCordSystemDegMinSecUtilities.LatLongClass ToConvert = new GeoCordSystemDegMinSecUtilities.LatLongClass(gMapControl.Position.Lat, gMapControl.Position.Lng);
            string Latitude_S, Longitude_S;
            ToConvert.GetDegMinSecStringFormat(out Latitude_S, out Longitude_S);
            this.lblCenterLat.Text = Latitude_S;
            this.lblCenterLon.Text = Longitude_S;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (gMapControl.Zoom > 10)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng - 0.1);
            else if (gMapControl.Zoom > 8)
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng - 0.2);
            else
                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng - 0.5);


            UpdatelblCenter();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UpdateSystemCenter();
        }

        private void UpdateSystemCenter()
        {
            gMapControl.Position = new PointLatLng(SystemAdaptationDataSet.SystemOrigin.Lat, SystemAdaptationDataSet.SystemOrigin.Lng);
            UpdatelblCenter();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxMapType.Text == "Google Plain")
            {
                gMapControl.MapProvider = GMapProviders.GoogleMap;
            }
            else if (this.comboBoxMapType.Text == "Google Satellite")
            {
                gMapControl.MapProvider = GMapProviders.GoogleSatelliteMap;
            }
            else if (this.comboBoxMapType.Text == "Google Terrain")
            {
                gMapControl.MapProvider = GMapProviders.GoogleTerrainMap;
            }
            else if (this.comboBoxMapType.Text == "Google Hybrid")
            {
                gMapControl.MapProvider = GMapProviders.GoogleHybridMap;
            }
            else if (this.comboBoxMapType.Text == "Custom Built")
            {
                gMapControl.MapProvider = GMapProviders.EmptyProvider;

            }

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        public static PointLatLng FromLocalToLatLng(int X, int Y)
        {
            return gMapControl.FromLocalToLatLng(X, Y);
        }

        public static GPoint FromLatLngToLocal(PointLatLng LatLng)
        {
            return gMapControl.FromLatLngToLocal(LatLng);
        }

        // Update Mouse possition on the control
        private void gMapControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RngBrngMarker RB_Marker = new RngBrngMarker(gMapControl.FromLocalToLatLng(StartMousePoint.X, StartMousePoint.Y), "Test", new Font(FontFamily.GenericSansSerif, 3), Brushes.Yellow, new Point(StartMousePoint.X, StartMousePoint.Y), new Point(e.X, e.Y));
                ToolsOverlay.Markers.Clear();
                ToolsOverlay.Markers.Add(RB_Marker);
                gMapControl.Refresh();
            }

            PointLatLng MouseLatLong = gMapControl.FromLocalToLatLng(e.X, e.Y);
            this.labelLat_Long.Location = new Point(this.labelLat_Long.Location.X, (gMapControl.Size.Height - 4));

            if (Properties.Settings.Default.DisplayPosInDecimals)
            {
                this.labelLat_Long.Text = MouseLatLong.Lat + " " + MouseLatLong.Lng;
            }
            else
            {
                GeoCordSystemDegMinSecUtilities.LatLongClass ToConvert = new GeoCordSystemDegMinSecUtilities.LatLongClass(MouseLatLong.Lat, MouseLatLong.Lng);
                string Latitude_S, Longitude_S;
                ToConvert.GetDegMinSecStringFormat(out Latitude_S, out Longitude_S);
                this.labelLat_Long.Text = Latitude_S + " " + Longitude_S;
            }

            if (e.Button == MouseButtons.Left && isDraggingMarker && currentMarker != null)
            {
                PointLatLng MousePosition = gMapControl.FromLocalToLatLng(e.X, e.Y);
                GPoint MarkerPositionLocal = gMapControl.FromLatLngToLocal(currentMarker.Position);
                GPoint NewLabelPosition = new GPoint(MarkerPositionLocal.X - e.X, MarkerPositionLocal.Y - e.Y);
                GMapTargetandLabel MyMarker = (GMapTargetandLabel)currentMarker;
                MyMarker.LabelOffset = new Point(NewLabelPosition.X, NewLabelPosition.Y);
                gMapControl.Refresh();
            }
            else if (SharedData.bool_Listen_for_Data == true)
            {
                bool Mouse_Not_Over_Label = true;
                GMapOverlay[] overlays = new GMapOverlay[] { DinamicOverlay };
                for (int i = overlays.Length - 1; i >= 0; i--)
                {
                    GMapOverlay o = overlays[i];
                    if (o != null && o.IsVisibile)
                        foreach (GMapMarker m in o.Markers)
                            if (m.IsVisible && m.IsHitTestVisible)
                            {
                                PointLatLng MousePosition = gMapControl.FromLocalToLatLng(e.X, e.Y);
                                GPoint MarkerPositionLocal = gMapControl.FromLatLngToLocal(m.Position);
                                GPoint NewLabelPosition = new GPoint(MarkerPositionLocal.X - e.X, MarkerPositionLocal.Y - e.Y);
                                if (MouseIsOnTheLabel(e, m))
                                {
                                    GMapTargetandLabel MyMarker = (GMapTargetandLabel)m;
                                    MyMarker.ShowLabelBox = true;
                                    gMapControl.Refresh();

                                    if (ExtendedLabel.Visible)
                                    {
                                        Mouse_Not_Over_Label = false;
                                        HandleExtendedLabel(MyMarker);
                                    }
                                }
                                else
                                {
                                    GMapTargetandLabel MyMarker = (GMapTargetandLabel)m;
                                    MyMarker.ShowLabelBox = false;
                                }

                            }

                    if (Mouse_Not_Over_Label)
                        ExtendedLabel.DefaultAll();
                }
            }
        }

        private void HandleExtendedLabel(GMapTargetandLabel MarkerData)
        {
            //////////////////////////////////////
            // First set title to Track ID
            ExtendedLabel.Text = MarkerData.ModeA_CI_STRING + "/" + MarkerData.CALLSIGN_STRING;
            // Now set the values
            ExtendedLabel.SetDataValue(FrmExtendedLabel.DataItems.HDG, MarkerData.DAP_HDG);
            ExtendedLabel.SetDataValue(FrmExtendedLabel.DataItems.IAS, MarkerData.IAS);
            ExtendedLabel.SetDataValue(FrmExtendedLabel.DataItems.MACH, MarkerData.MACH);
            ExtendedLabel.SetDataValue(FrmExtendedLabel.DataItems.TAS, MarkerData.TAS);
            ExtendedLabel.SetDataValue(FrmExtendedLabel.DataItems.TRK, MarkerData.TRK);
            ExtendedLabel.SetDataValue(FrmExtendedLabel.DataItems.Roll_Angle, MarkerData.Roll_Angle);
            ExtendedLabel.SetDataValue(FrmExtendedLabel.DataItems.RateOfClimb, MarkerData.Rate_Of_Climb);
            ExtendedLabel.SetDataValue(FrmExtendedLabel.DataItems.Selected_Altitude, MarkerData.SelectedAltitude_ShortTerm);
            ExtendedLabel.SetDataValue(FrmExtendedLabel.DataItems.Selected_Altitude_F, MarkerData.SelectedAltitude_LongTerm);
            ExtendedLabel.SetDataValue(FrmExtendedLabel.DataItems.BaroSetting, MarkerData.Barometric_Setting);
            ExtendedLabel.SetDataValue(FrmExtendedLabel.DataItems.Mode_S_Addr, MarkerData.Mode_S_Addr);
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

        private void colorDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayAttibutePicker ColorPickerForm = new DisplayAttibutePicker();
            ColorPickerForm.Show();
        }

        private void StaticDisplayTimer_Tick(object sender, EventArgs e)
        {
            if (DisplayAttributes.StaticDisplayBuildRequired)
            {
                // Always check for the change to the background color
                gMapControl.EmptyMapBackground = DisplayAttributes.GetDisplayAttribute(DisplayAttributes.DisplayItemsType.BackgroundColor).TextColor;

                // rebuild static display
                StaticOverlay.Markers.Clear();
                StaticOverlay.Routes.Clear();
                StaticOverlay.Polygons.Clear();

                StaticDisplayBuilder.Build(ref StaticOverlay);
                DisplayAttributes.StaticDisplayBuildRequired = false;

                UpdateSystemCenter();

                gMapControl.MapProvider = GMapProviders.EmptyProvider;
            }
        }

        private void gMapControl_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            this.tabMainTab.Size = new Size(this.Size.Width - 16, this.Size.Height - 90);

        }

        private void tabMainTab_SizeChanged(object sender, EventArgs e)
        {
            this.tabPlotDisplay.Size = new Size(this.tabMainTab.Size.Width - 8, this.tabMainTab.Size.Height - 26);
        }

        private void tabPlotDisplay_SizeChanged(object sender, EventArgs e)
        {
            gMapControl.Size = new Size(this.tabPlotDisplay.Size.Width - 147, this.tabPlotDisplay.Size.Height - 12);
            this.groupBoxConnection.Location = new Point(this.Size.Width - 408, 1);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxFLFilter.Checked)
            {
                this.checkBoxFLFilter.Text = "Enabled";
                this.checkBoxFLFilter.BackColor = Color.Red;
            }
            else
            {
                this.checkBoxFLFilter.Text = "Disabled";
                this.checkBoxFLFilter.BackColor = Color.Transparent;
            }

            Properties.Settings.Default.FL_Filter_Enabled = this.checkBoxFLFilter.Checked;
            Properties.Settings.Default.Save();


            Update_PlotTrack_Data();
        }

        private void tabMainTab_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmConnectionSettings SettingDialog = new FrmConnectionSettings();
            SettingDialog.Visible = false;
            SettingDialog.Owner = this;
            SettingDialog.Show(this);
            SettingDialog.Location = System.Windows.Forms.Cursor.Position;
            SettingDialog.Visible = true;
        }

        private void gMapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DisplayRightClickOptions MyForm = new DisplayRightClickOptions();
                MyForm.StartPosition = FormStartPosition.Manual;
                Point relativeToForm = this.PointToScreen(new Point(e.X, e.Y));

                if (this.checkBoxFullscreen.Checked)
                    MyForm.Location = new Point(relativeToForm.X - 25, relativeToForm.Y + 55);
                else
                    MyForm.Location = new Point(relativeToForm.X + 95, relativeToForm.Y + 55);

                MyForm.Show();
            }
        }

        private void gMapControl_MouseDown(object sender, MouseEventArgs e)
        {
            StartMousePoint = new Point(e.X, e.Y);

            // Check if the user is trying to move the label If so then flag it by 
            // setting the flag isDraggingMarker to true
            if (e.Button == MouseButtons.Left && SharedData.bool_Listen_for_Data == true)
            {
                GMapOverlay[] overlays = new GMapOverlay[] { DinamicOverlay };
                for (int i = overlays.Length - 1; i >= 0; i--)
                {
                    GMapOverlay o = overlays[i];
                    if (o != null && o.IsVisibile)
                        foreach (GMapMarker m in o.Markers)
                            if (m.IsVisible && m.IsHitTestVisible)
                            {
                                if (MouseIsOnTheLabel(e, m))
                                {
                                    currentMarker = m;
                                    isDraggingMarker = true;
                                    return;
                                }
                            }
                }
            }
            // Check if the user clicked on one of the interactive label fields.
            else if (e.Button == MouseButtons.Right && SharedData.bool_Listen_for_Data == true)
            {
                GMapOverlay[] overlays = new GMapOverlay[] { DinamicOverlay };
                for (int i = overlays.Length - 1; i >= 0; i--)
                {
                    GMapOverlay o = overlays[i];
                    if (o != null && o.IsVisibile)
                        foreach (GMapMarker m in o.Markers)
                            if (m.IsVisible && m.IsHitTestVisible)
                            {
                                if (MouseIsOnTheCFL(e, m))
                                {
                                    GMapTargetandLabel MyMarker = (GMapTargetandLabel)m;
                                    if (MyMarker.MyTargetIndex != -1)
                                    {
                                        UpdateCFL MyForm = new UpdateCFL();
                                        MyForm.TrackToUpdate = MyMarker.MyTargetIndex;
                                        MyForm.StartPosition = FormStartPosition.Manual;
                                        Point relativeToForm = this.PointToScreen(new Point(e.X, e.Y));
                                        if (this.checkBoxFullscreen.Checked)
                                            MyForm.Location = new Point(relativeToForm.X, relativeToForm.Y + 65);
                                        else
                                            MyForm.Location = new Point(relativeToForm.X + 140, relativeToForm.Y + 65);
                                        MyForm.Show();
                                    }
                                }
                                else if (MouseIsOnTheHDG(e, m))
                                {
                                    GMapTargetandLabel MyMarker = (GMapTargetandLabel)m;
                                    if (MyMarker.MyTargetIndex != -1)
                                    {
                                        UpdateHDG MyForm = new UpdateHDG();
                                        MyForm.TrackToUpdate = MyMarker.MyTargetIndex;
                                        MyForm.StartPosition = FormStartPosition.Manual;
                                        Point relativeToForm = this.PointToScreen(new Point(e.X, e.Y));
                                        if (this.checkBoxFullscreen.Checked)
                                            MyForm.Location = new Point(relativeToForm.X, relativeToForm.Y + 65);
                                        else
                                            MyForm.Location = new Point(relativeToForm.X + 140, relativeToForm.Y + 65);
                                        MyForm.Show();
                                    }
                                }
                                else if (MouseIsOnTheSPD(e, m))
                                {
                                    GMapTargetandLabel MyMarker = (GMapTargetandLabel)m;
                                    if (MyMarker.MyTargetIndex != -1)
                                    {
                                        UpdateSPD MyForm = new UpdateSPD();
                                        MyForm.TrackToUpdate = MyMarker.MyTargetIndex;
                                        MyForm.StartPosition = FormStartPosition.Manual;
                                        Point relativeToForm = this.PointToScreen(new Point(e.X, e.Y));
                                        if (this.checkBoxFullscreen.Checked)
                                            MyForm.Location = new Point(relativeToForm.X, relativeToForm.Y + 65);
                                        else
                                            MyForm.Location = new Point(relativeToForm.X + 140, relativeToForm.Y + 65);
                                        MyForm.Show();
                                    }
                                }
                                else if (MouseIsOnTheAC_Symbol(e, m))
                                {
                                    GMapTargetandLabel MyMarker = (GMapTargetandLabel)m;
                                    if (MyMarker.MyTargetIndex != -1)
                                    {
                                        if (MyMarker.TargetToMonitor != -1)
                                            DynamicDisplayBuilder.DeactivateSEPTool(MyMarker.MyTargetIndex, MyMarker.TargetToMonitor);
                                        else if (MyMarker.TargetMonitoredBy != -1)
                                            DynamicDisplayBuilder.DeactivateSEPTool(MyMarker.TargetMonitoredBy, MyMarker.MyTargetIndex);
                                        else
                                            SEPToolStartTarget = MyMarker.MyTargetIndex;
                                    }
                                }

                            }
                }

            }
        }

        bool MouseIsOnTheLabel(MouseEventArgs Mouse, GMapMarker Marker)
        {
            GMapTargetandLabel MyMarker = (GMapTargetandLabel)Marker;
            Rectangle MyRectangle = new Rectangle(MyMarker.GetLabelStartingPoint().X, MyMarker.GetLabelStartingPoint().Y, MyMarker.GetLabelWidth(), MyMarker.GetLabelHeight());
            return MyRectangle.Contains(new Point(Mouse.X, Mouse.Y));
        }

        bool MouseIsOnTheCFL(MouseEventArgs Mouse, GMapMarker Marker)
        {
            GMapTargetandLabel MyMarker = (GMapTargetandLabel)Marker;
            Rectangle MyRectangle = new Rectangle(MyMarker.GetCFLStartPoint().X, MyMarker.GetCFLStartPoint().Y, 20, 15);
            return MyRectangle.Contains(new Point(Mouse.X, Mouse.Y));
        }

        bool MouseIsOnTheHDG(MouseEventArgs Mouse, GMapMarker Marker)
        {
            GMapTargetandLabel MyMarker = (GMapTargetandLabel)Marker;
            Rectangle MyRectangle = new Rectangle(MyMarker.GetHDGStartPoint().X, MyMarker.GetHDGStartPoint().Y, 20, 15);
            return MyRectangle.Contains(new Point(Mouse.X, Mouse.Y));
        }

        bool MouseIsOnTheSPD(MouseEventArgs Mouse, GMapMarker Marker)
        {
            GMapTargetandLabel MyMarker = (GMapTargetandLabel)Marker;
            Rectangle MyRectangle = new Rectangle(MyMarker.GetSPDStartPoint().X, MyMarker.GetSPDStartPoint().Y, 20, 15);
            return MyRectangle.Contains(new Point(Mouse.X, Mouse.Y));
        }

        bool MouseIsOnTheAC_Symbol(MouseEventArgs Mouse, GMapMarker Marker)
        {
            GMapTargetandLabel MyMarker = (GMapTargetandLabel)Marker;
            Rectangle MyRectangle = new Rectangle(MyMarker.GetAC_SYMB_StartPoint().X - 1, MyMarker.GetAC_SYMB_StartPoint().Y - 1, 12, 12);
            return MyRectangle.Contains(new Point(Mouse.X, Mouse.Y));
        }

        private void gMapControl_MouseUp(object sender, MouseEventArgs e)
        {
            isDraggingMarker = false;
            currentMarker = null;
            ToolsOverlay.Markers.Clear();
            /////////////////////////////////////////////////
            // Here check if mouse was over a mouse symbol
            // and started to drag RNG/BRNG tool and now over
            // a different symbol. If true then initiate SEP tool
            if (SEPToolStartTarget != -1)
            {
                GMapOverlay[] overlays = new GMapOverlay[] { DinamicOverlay };
                for (int i = overlays.Length - 1; i >= 0; i--)
                {
                    GMapOverlay o = overlays[i];
                    if (o != null && o.IsVisibile)
                        foreach (GMapMarker m in o.Markers)
                            if (m.IsVisible && m.IsHitTestVisible)
                            {
                                if (MouseIsOnTheAC_Symbol(e, m))
                                {
                                    GMapTargetandLabel MyMarker = (GMapTargetandLabel)m;
                                    if (MyMarker.MyTargetIndex != -1 && MyMarker.MyTargetIndex != SEPToolStartTarget)
                                    {
                                        DynamicDisplayBuilder.ActivateSEPTool(MyMarker.MyTargetIndex, SEPToolStartTarget);
                                    }
                                }
                            }
                }
                SEPToolStartTarget = -1;
            }
        }

        private void gMapControl_OnMarkerEnter(GMapMarker item)
        {
            if (!isDraggingMarker)
                currentMarker = item;
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Start the thread to listen for data
            TerminateASTERIXListenForDataThread();
        }

        private void TerminateASTERIXListenForDataThread()
        {
            ASTERIX.RequestStop();
            if (ListenForDataThread.IsAlive)
                ListenForDataThread.Join();
            ASTERIX.CleanUp();
        }

        private void cATDecoderSelectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CAT_Decoder_Selector MY_CAT_Decoder_Selector = new CAT_Decoder_Selector();
            MY_CAT_Decoder_Selector.Show();
        }

        private void checkBoxSyncToNM_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxSyncToNM.Checked == true)
            {
                this.PlotandTrackDisplayUpdateTimer.Enabled = false;
            }
            else
            {
                this.PlotandTrackDisplayUpdateTimer.Enabled = true;
            }
        }

        private void textBox1TrackCoast_KeyPress(object sender, KeyPressEventArgs e)
        {
            string allowedCharacterSet = "0123456789\b";    	   //Allowed character set

            if (allowedCharacterSet.Contains(e.KeyChar.ToString()))
            {

            }
            else if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;

                int TrackCoastCycle = Properties.Settings.Default.TrackCoast;
                if (int.TryParse(this.textBox1TrackCoast.Text, out TrackCoastCycle) == true)
                {
                    if (TrackCoastCycle > 0 && TrackCoastCycle < 10)
                    {
                        Properties.Settings.Default.TrackCoast = TrackCoastCycle;
                        Properties.Settings.Default.Save();
                        this.labelTrackCoast.Text = Properties.Settings.Default.TrackCoast.ToString();
                        this.textBox1TrackCoast.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Please enter an integer in range of 0 to 10");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter an integer in range of 0 to 10");
                }
            }
            else
            {
                MessageBox.Show("Please enter an integer in range of 10 to 10");
            }
        }

        private void checkBoxSyncToNM_CheckedChanged_1(object sender, EventArgs e)
        {
            HandlePlotDisplayEnabledChanged();

            Properties.Settings.Default.SyncDisplayToNorthMark = this.checkBoxSyncToNM.Checked;
            Properties.Settings.Default.Save();
        }

        private void groupBoxUpdateRate_Enter(object sender, EventArgs e)
        {

        }

        private void numericUpDownUpper_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FL_Upper = this.numericUpDownUpper.Value;
            Properties.Settings.Default.Save();
            Update_PlotTrack_Data();
        }

        private void numericUpDownLower_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FL_Lower = this.numericUpDownLower.Value;
            Properties.Settings.Default.Save();
            Update_PlotTrack_Data();
        }
        private void NorthMarkerTimer_Tick(object sender, EventArgs e)
        {
            if (North_Marker_Received == true)
            {
                North_Marker_Received = false;

                if (checkBoxSystMonEnabled.Checked)
                {
                    HandleSystemStatusCAT34I50();
                }

                if (this.checkBoxSyncToNM.Checked == true)
                    Update_PlotTrack_Data();
            }
        }

        private void HandleSystemStatusCAT34I50()
        {
            if (MainASTERIXDataStorage.CAT34Message.Count > 0)
            {
                MainASTERIXDataStorage.CAT34Data Msg = MainASTERIXDataStorage.CAT34Message[MainASTERIXDataStorage.CAT34Message.Count - 1];

                // Get the antenna rotation period
                if (Msg.CAT34DataItems[CAT34.ItemIDToIndex("041")].CurrentlyPresent)
                    this.labelAntenaPeriod034.Text = (string)((double)Msg.CAT34DataItems[CAT34.ItemIDToIndex("041")].value).ToString();
                else
                    this.labelAntenaPeriod034.Text = "N/A";

                // Get the latest System Configuration and Status message
                CAT34I050Types.CAT34I050UserData MyCAT34I050UserData = (CAT34I050Types.CAT34I050UserData)Msg.CAT34DataItems[CAT34.ItemIDToIndex("050")].value;

                string Time_String = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

                if (MyCAT34I050UserData != null)
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////
                    // COM BLOCK
                    /////////////////////////////////////////////////////////////////////////////////////////////////////

                    if (MyCAT34I050UserData.COM_Data.Data_Present)
                    {
                        if ((MyCAT34I050UserData_Last_Cycle.COM_Data.Data_Present == false) || (MyCAT34I050UserData.COM_Data.System_is_NOGO != MyCAT34I050UserData_Last_Cycle.COM_Data.System_is_NOGO))
                        {
                            if (MyCAT34I050UserData.COM_Data.System_is_NOGO)
                                this.comboBoxCOM_Top_Status.Items.Insert(0, "R " + Time_String + " NOGO");
                            else
                                this.comboBoxCOM_Top_Status.Items.Insert(0, "G " + Time_String + " GO");

                            this.comboBoxCOM_Top_Status.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.COM_Data.Data_Present == false) || (MyCAT34I050UserData.COM_Data.RDPC2_Selected != MyCAT34I050UserData_Last_Cycle.COM_Data.RDPC2_Selected))
                        {
                            if (MyCAT34I050UserData.COM_Data.RDPC2_Selected)
                                this.comboBoxCOM_RDPC_Selected.Items.Insert(0, "W " + Time_String + " RDPC 2 SELECTED");
                            else
                                this.comboBoxCOM_RDPC_Selected.Items.Insert(0, "W " + Time_String + " RDPC 1 SELECTED");

                            this.comboBoxCOM_RDPC_Selected.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.COM_Data.Data_Present == false) || (MyCAT34I050UserData.COM_Data.RDPC_Reset != MyCAT34I050UserData_Last_Cycle.COM_Data.RDPC_Reset))
                        {
                            if (MyCAT34I050UserData.COM_Data.RDPC_Reset)
                                this.comboBoxCOM_RDPC_Reset.Items.Insert(0, "R " + Time_String + " RDPC RESET");
                            else
                                this.comboBoxCOM_RDPC_Reset.Items.Insert(0, "G " + Time_String + " NO RDPC RESET");

                            this.comboBoxCOM_RDPC_Reset.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.COM_Data.Data_Present == false) || (MyCAT34I050UserData.COM_Data.RDP_Overloaded != MyCAT34I050UserData_Last_Cycle.COM_Data.RDP_Overloaded))
                        {
                            if (MyCAT34I050UserData.COM_Data.RDPC_Reset)
                                this.comboBoxCOM_RDP_Overload.Items.Insert(0, "R " + Time_String + " RDP OVERLOAD");
                            else
                                this.comboBoxCOM_RDP_Overload.Items.Insert(0, "G " + Time_String + " NO RDP OVERLOAD");

                            this.comboBoxCOM_RDP_Overload.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.COM_Data.Data_Present == false) || (MyCAT34I050UserData.COM_Data.Transmision_Sys_Overloaded != MyCAT34I050UserData_Last_Cycle.COM_Data.Transmision_Sys_Overloaded))
                        {
                            if (MyCAT34I050UserData.COM_Data.RDPC_Reset)
                                this.comboBoxCOM_Transmit_Overload.Items.Insert(0, "R " + Time_String + " TXMT SYS OVERLOAD");
                            else
                                this.comboBoxCOM_Transmit_Overload.Items.Insert(0, "G " + Time_String + " NO TXMT SYS OVERLOAD");

                            this.comboBoxCOM_Transmit_Overload.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.COM_Data.Data_Present == false) || (MyCAT34I050UserData.COM_Data.Monitor_Sys_Disconected != MyCAT34I050UserData_Last_Cycle.COM_Data.Monitor_Sys_Disconected))
                        {
                            if (MyCAT34I050UserData.COM_Data.RDPC_Reset)
                                this.comboBoxCOM_MON_Sys_Disconect.Items.Insert(0, "R " + Time_String + " MON SYS DISCONECT");
                            else
                                this.comboBoxCOM_MON_Sys_Disconect.Items.Insert(0, "G " + Time_String + " MON SYS CONNECTED");

                            this.comboBoxCOM_MON_Sys_Disconect.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.COM_Data.Data_Present == false) || (MyCAT34I050UserData.COM_Data.Time_Source_Invalid != MyCAT34I050UserData_Last_Cycle.COM_Data.Time_Source_Invalid))
                        {
                            if (MyCAT34I050UserData.COM_Data.RDPC_Reset)
                                this.comboBoxCOM_Time_Source_Status.Items.Insert(0, "R " + Time_String + " TIME SRC INVALID");
                            else
                                this.comboBoxCOM_Time_Source_Status.Items.Insert(0, "G " + Time_String + " TIME SRC VALID");

                            this.comboBoxCOM_Time_Source_Status.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        if ((MyCAT34I050UserData_Last_Cycle.COM_Data.Data_Present == true) && (MyCAT34I050UserData.COM_Data.Data_Present == false))
                        {
                            this.comboBoxCOM_Top_Status.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO COM DATA");
                            this.comboBoxCOM_Top_Status.SelectedIndex = 0;
                            this.comboBoxCOM_RDPC_Selected.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO COM DATA");
                            this.comboBoxCOM_RDPC_Selected.SelectedIndex = 0;
                            this.comboBoxCOM_RDPC_Reset.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO COM DATA");
                            this.comboBoxCOM_RDPC_Reset.SelectedIndex = 0;
                            this.comboBoxCOM_RDP_Overload.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO COM DATA");
                            this.comboBoxCOM_RDP_Overload.SelectedIndex = 0;
                            this.comboBoxCOM_Transmit_Overload.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO COM DATA");
                            this.comboBoxCOM_Transmit_Overload.SelectedIndex = 0;
                            this.comboBoxCOM_MON_Sys_Disconect.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO COM DATA");
                            this.comboBoxCOM_MON_Sys_Disconect.SelectedIndex = 0;
                            this.comboBoxCOM_Time_Source_Status.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO COM DATA");
                            this.comboBoxCOM_Time_Source_Status.SelectedIndex = 0;
                        }
                    }


                    /////////////////////////////////////////////////////////////////////////////////////////////////////
                    // SSR BLOCK
                    /////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (MyCAT34I050UserData.SSR_Data.Data_Present)
                    {
                        if ((MyCAT34I050UserData_Last_Cycle.SSR_Data.Data_Present == false) || (MyCAT34I050UserData.SSR_Data.CH_Status != MyCAT34I050UserData_Last_Cycle.SSR_Data.CH_Status))
                        {
                            if (MyCAT34I050UserData.SSR_Data.CH_Status == CAT34I050Types.SSR.Channel_Status.No_Channel)
                                this.comboBoxSSR_Channel_Status.Items.Insert(0, "R " + Time_String + " NO Channel");
                            else if (MyCAT34I050UserData.SSR_Data.CH_Status == CAT34I050Types.SSR.Channel_Status.Invalid_Combination)
                                this.comboBoxSSR_Channel_Status.Items.Insert(0, "R " + Time_String + " Invalid CH combination");
                            else if (MyCAT34I050UserData.SSR_Data.CH_Status == CAT34I050Types.SSR.Channel_Status.Channel_A)
                                this.comboBoxSSR_Channel_Status.Items.Insert(0, "G " + Time_String + " Channel A");
                            else
                                this.comboBoxSSR_Channel_Status.Items.Insert(0, "G " + Time_String + " Channel B");

                            this.comboBoxSSR_Channel_Status.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.SSR_Data.Data_Present == false) || (MyCAT34I050UserData.SSR_Data.Ant_2_Selected != MyCAT34I050UserData_Last_Cycle.SSR_Data.Ant_2_Selected))
                        {
                            if (MyCAT34I050UserData.SSR_Data.Ant_2_Selected)
                                this.comboBoxSSR_Antenna_Selected.Items.Insert(0, "W " + Time_String + " Antenna 2 selected");
                            else
                                this.comboBoxSSR_Antenna_Selected.Items.Insert(0, "W " + Time_String + " Antenna 1 selected");

                            this.comboBoxSSR_Antenna_Selected.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.SSR_Data.Data_Present == false) || (MyCAT34I050UserData.SSR_Data.SSR_Overloaded != MyCAT34I050UserData_Last_Cycle.SSR_Data.SSR_Overloaded))
                        {
                            if (MyCAT34I050UserData.SSR_Data.SSR_Overloaded)
                                this.comboBoxSSR_Overloaded.Items.Insert(0, "R " + Time_String + " SYS Overload");
                            else
                                this.comboBoxSSR_Overloaded.Items.Insert(0, "G " + Time_String + " NO SYS Overload");

                            this.comboBoxSSR_Overloaded.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.SSR_Data.Data_Present == false) || (MyCAT34I050UserData.SSR_Data.Monitor_Sys_Disconected != MyCAT34I050UserData_Last_Cycle.SSR_Data.Monitor_Sys_Disconected))
                        {
                            if (MyCAT34I050UserData.SSR_Data.Monitor_Sys_Disconected)
                                this.comboBoxSSR_Mon_Sys_Disconect.Items.Insert(0, "R " + Time_String + " MON sys disconnect");
                            else
                                this.comboBoxSSR_Mon_Sys_Disconect.Items.Insert(0, "G " + Time_String + " MON sys connect");

                            this.comboBoxSSR_Mon_Sys_Disconect.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        if ((MyCAT34I050UserData_Last_Cycle.SSR_Data.Data_Present == true) && (MyCAT34I050UserData.SSR_Data.Data_Present == false))
                        {
                            this.comboBoxSSR_Channel_Status.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO SSR DATA");
                            this.comboBoxSSR_Channel_Status.SelectedIndex = 0;
                            this.comboBoxSSR_Antenna_Selected.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO SSR DATA");
                            this.comboBoxSSR_Antenna_Selected.SelectedIndex = 0;
                            this.comboBoxSSR_Overloaded.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO SSR DATA");
                            this.comboBoxSSR_Overloaded.SelectedIndex = 0;
                            this.comboBoxSSR_Mon_Sys_Disconect.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO SSR DATA");
                            this.comboBoxSSR_Mon_Sys_Disconect.SelectedIndex = 0;

                        }
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////
                    // MDS BLOCK
                    /////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (MyCAT34I050UserData.MDS_Data.Data_Present)
                    {
                        if ((MyCAT34I050UserData_Last_Cycle.MDS_Data.Data_Present == false) || (MyCAT34I050UserData.MDS_Data.CH_Status != MyCAT34I050UserData_Last_Cycle.MDS_Data.CH_Status))
                        {
                            if (MyCAT34I050UserData.MDS_Data.CH_Status == CAT34I050Types.MDS.Channel_Status.No_Channel)
                                this.comboBoxMDS_Channel_Status.Items.Insert(0, "R " + Time_String + " NO Channel");
                            else if (MyCAT34I050UserData.MDS_Data.CH_Status == CAT34I050Types.MDS.Channel_Status.Illegal_Combination)
                                this.comboBoxMDS_Channel_Status.Items.Insert(0, "R " + Time_String + " Illegal CH combination");
                            else if (MyCAT34I050UserData.MDS_Data.CH_Status == CAT34I050Types.MDS.Channel_Status.Channel_A)
                                this.comboBoxMDS_Channel_Status.Items.Insert(0, "G " + Time_String + " Channel A");
                            else
                                this.comboBoxMDS_Channel_Status.Items.Insert(0, "G " + Time_String + " Channel B");

                            this.comboBoxMDS_Channel_Status.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.MDS_Data.Data_Present == false) || (MyCAT34I050UserData.MDS_Data.Ant_2_Selected != MyCAT34I050UserData_Last_Cycle.MDS_Data.Ant_2_Selected))
                        {
                            if (MyCAT34I050UserData.MDS_Data.Ant_2_Selected)
                                this.comboBoxMDS_Antena_Selected.Items.Insert(0, "W " + Time_String + " Antenna 2 selected");
                            else
                                this.comboBoxMDS_Antena_Selected.Items.Insert(0, "W " + Time_String + " Antenna 1 selected");

                            this.comboBoxMDS_Antena_Selected.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.SSR_Data.Data_Present == false) || (MyCAT34I050UserData.SSR_Data.SSR_Overloaded != MyCAT34I050UserData_Last_Cycle.SSR_Data.SSR_Overloaded))
                        {
                            if (MyCAT34I050UserData.MDS_Data.ModeS_Overloaded)
                                this.ccomboBoxMDS_Overoaded.Items.Insert(0, "R " + Time_String + " SYS Overload");
                            else
                                this.ccomboBoxMDS_Overoaded.Items.Insert(0, "G " + Time_String + " NO SYS Overload");

                            this.ccomboBoxMDS_Overoaded.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.MDS_Data.Data_Present == false) || (MyCAT34I050UserData.MDS_Data.Monitor_Sys_Disconected != MyCAT34I050UserData_Last_Cycle.MDS_Data.Monitor_Sys_Disconected))
                        {
                            if (MyCAT34I050UserData.MDS_Data.Monitor_Sys_Disconected)
                                this.comboBoxMDS_Mon_Sys_Disconect.Items.Insert(0, "R " + Time_String + " MON sys disconnect");
                            else
                                this.comboBoxMDS_Mon_Sys_Disconect.Items.Insert(0, "G " + Time_String + " MON sys connect");

                            this.comboBoxMDS_Mon_Sys_Disconect.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.MDS_Data.Data_Present == false) || (MyCAT34I050UserData.MDS_Data.CH2_For_Coordination_In_Use != MyCAT34I050UserData_Last_Cycle.MDS_Data.CH2_For_Coordination_In_Use))
                        {
                            if (MyCAT34I050UserData.MDS_Data.CH2_For_Coordination_In_Use)
                                this.comboBoxMDS_Ch_For_Cord.Items.Insert(0, "W " + Time_String + " CH#2 for Coordination");
                            else
                                this.comboBoxMDS_Ch_For_Cord.Items.Insert(0, "G " + Time_String + " CH#1 for Coordination");

                            this.comboBoxMDS_Ch_For_Cord.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.MDS_Data.Data_Present == false) || (MyCAT34I050UserData.MDS_Data.CH2_For_DataLink_In_Use != MyCAT34I050UserData_Last_Cycle.MDS_Data.CH2_For_DataLink_In_Use))
                        {
                            if (MyCAT34I050UserData.MDS_Data.CH2_For_DataLink_In_Use)
                                this.comboBoxMDS_Ch_For_Cord.Items.Insert(0, "W " + Time_String + " CH#2 for Data Link");
                            else
                                this.comboBoxMDS_Ch_For_Cord.Items.Insert(0, "W " + Time_String + " CH#1 for Data Link");

                            this.comboBoxMDS_Ch_For_Cord.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.MDS_Data.Data_Present == false) || (MyCAT34I050UserData.MDS_Data.DataLink_Func_Overload != MyCAT34I050UserData_Last_Cycle.MDS_Data.CH2_For_DataLink_In_Use))
                        {
                            if (MyCAT34I050UserData.MDS_Data.DataLink_Func_Overload)
                                this.comboBoxMDS_DataLink_Overload.Items.Insert(0, "R " + Time_String + " Overload Data Link");
                            else
                                this.comboBoxMDS_DataLink_Overload.Items.Insert(0, "G " + Time_String + " NO Overload Data Link");

                            this.comboBoxMDS_DataLink_Overload.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.MDS_Data.Data_Present == false) || (MyCAT34I050UserData.MDS_Data.Coordination_Func_Overload != MyCAT34I050UserData_Last_Cycle.MDS_Data.Coordination_Func_Overload))
                        {
                            if (MyCAT34I050UserData.MDS_Data.Coordination_Func_Overload)
                                this.comboBoxMDS_Comm_Overload.Items.Insert(0, "R " + Time_String + " Overload Coordination");
                            else
                                this.comboBoxMDS_Comm_Overload.Items.Insert(0, "G " + Time_String + " NO Overload Coordination");

                            this.comboBoxMDS_Comm_Overload.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        if ((MyCAT34I050UserData_Last_Cycle.MDS_Data.Data_Present == true) && (MyCAT34I050UserData.MDS_Data.Data_Present == false))
                        {
                            this.comboBoxMDS_Channel_Status.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO MDS DATA");
                            this.comboBoxMDS_Channel_Status.SelectedIndex = 0;
                            this.comboBoxMDS_Antena_Selected.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO MDS DATA");
                            this.comboBoxMDS_Antena_Selected.SelectedIndex = 0;
                            this.ccomboBoxMDS_Overoaded.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO MDS DATA");
                            this.ccomboBoxMDS_Overoaded.SelectedIndex = 0;
                            this.comboBoxMDS_Mon_Sys_Disconect.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO MDS DATA");
                            this.comboBoxMDS_Mon_Sys_Disconect.SelectedIndex = 0;
                            this.comboBoxMDS_Ch_For_Cord.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO MDS DATA");
                            this.comboBoxMDS_Ch_For_Cord.SelectedIndex = 0;
                            this.comboBoxMDS_Ch_For_DataLink.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO MDS DATA");
                            this.comboBoxMDS_Ch_For_DataLink.SelectedIndex = 0;
                            this.comboBoxMDS_DataLink_Overload.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO MDS DATA");
                            this.comboBoxMDS_DataLink_Overload.SelectedIndex = 0;
                            this.comboBoxMDS_Comm_Overload.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO MDS DATA");
                            this.comboBoxMDS_Comm_Overload.SelectedIndex = 0;
                        }
                    }


                    /////////////////////////////////////////////////////////////////////////////////////////////////////
                    // PSR BLOCK
                    /////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (MyCAT34I050UserData.PSR_Data.Data_Present)
                    {
                        if ((MyCAT34I050UserData_Last_Cycle.PSR_Data.Data_Present == false) || (MyCAT34I050UserData.PSR_Data.CH_Status != MyCAT34I050UserData_Last_Cycle.PSR_Data.CH_Status))
                        {
                            if (MyCAT34I050UserData.PSR_Data.CH_Status == CAT34I050Types.PSR.Channel_Status.No_Channel)
                                this.comboBoxPSR_Channel_Status.Items.Insert(0, "R " + Time_String + " NO Channel");
                            else if (MyCAT34I050UserData.PSR_Data.CH_Status == CAT34I050Types.PSR.Channel_Status.Channel_A_and_B)
                                this.comboBoxPSR_Channel_Status.Items.Insert(0, "R " + Time_String + "  Channel A & B");
                            else if (MyCAT34I050UserData.PSR_Data.CH_Status == CAT34I050Types.PSR.Channel_Status.Channel_A)
                                this.comboBoxPSR_Channel_Status.Items.Insert(0, "G " + Time_String + " Channel A");
                            else
                                this.comboBoxPSR_Channel_Status.Items.Insert(0, "G " + Time_String + " Channel B");

                            this.comboBoxPSR_Channel_Status.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.PSR_Data.Data_Present == false) || (MyCAT34I050UserData.PSR_Data.Ant_2_Selected != MyCAT34I050UserData_Last_Cycle.PSR_Data.Ant_2_Selected))
                        {
                            if (MyCAT34I050UserData.PSR_Data.Ant_2_Selected)
                                this.comboBoxPSR_Antenna_Selected.Items.Insert(0, "W " + Time_String + " Antenna 2 selected");
                            else
                                this.comboBoxPSR_Antenna_Selected.Items.Insert(0, "W " + Time_String + " Antenna 1 selected");

                            this.comboBoxPSR_Antenna_Selected.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.PSR_Data.Data_Present == false) || (MyCAT34I050UserData.PSR_Data.PSR_Overloaded != MyCAT34I050UserData_Last_Cycle.PSR_Data.PSR_Overloaded))
                        {
                            if (MyCAT34I050UserData.PSR_Data.PSR_Overloaded)
                                this.comboBoxPSR_Overloaded.Items.Insert(0, "R " + Time_String + " SYS Overload");
                            else
                                this.comboBoxPSR_Overloaded.Items.Insert(0, "G " + Time_String + " NO SYS Overload");

                            this.comboBoxPSR_Overloaded.SelectedIndex = 0;
                        }

                        if ((MyCAT34I050UserData_Last_Cycle.PSR_Data.Data_Present == false) || (MyCAT34I050UserData.PSR_Data.Monitor_Sys_Disconected != MyCAT34I050UserData_Last_Cycle.PSR_Data.Monitor_Sys_Disconected))
                        {
                            if (MyCAT34I050UserData.PSR_Data.Monitor_Sys_Disconected)
                                this.comboBoxPSR_Mon_Sys_Disconect.Items.Insert(0, "R " + Time_String + " MON sys disconnect");
                            else
                                this.comboBoxPSR_Mon_Sys_Disconect.Items.Insert(0, "G " + Time_String + " MON sys connect");

                            this.comboBoxPSR_Mon_Sys_Disconect.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        if ((MyCAT34I050UserData_Last_Cycle.PSR_Data.Data_Present == true) && (MyCAT34I050UserData.PSR_Data.Data_Present == false))
                        {
                            this.comboBoxPSR_Channel_Status.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO PSR DATA");
                            this.comboBoxPSR_Channel_Status.SelectedIndex = 0;
                            this.comboBoxPSR_Antenna_Selected.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO PSR DATA");
                            this.comboBoxPSR_Antenna_Selected.SelectedIndex = 0;
                            this.comboBoxPSR_Overloaded.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO PSR DATA");
                            this.comboBoxPSR_Overloaded.SelectedIndex = 0;
                            this.comboBoxPSR_Mon_Sys_Disconect.Items.Insert(0, "W " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO PSR DATA");
                            this.comboBoxPSR_Mon_Sys_Disconect.SelectedIndex = 0;
                        }
                    }

                    // Sync the data for this cycle
                    MyCAT34I050UserData_Last_Cycle = MyCAT34I050UserData;
                }
            }
        }

        private void HandleNoDataForCAT034I050(bool Blank_Out)
        {
            if (Blank_Out)
            {
                ClearAllRadarOneEvents();
            }
            else
            {
                // COM part
                this.comboBoxCOM_Top_Status.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxCOM_Top_Status.SelectedIndex = 0;
                this.comboBoxCOM_RDPC_Selected.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxCOM_RDPC_Selected.SelectedIndex = 0;
                this.comboBoxCOM_RDPC_Reset.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxCOM_RDPC_Reset.SelectedIndex = 0;
                this.comboBoxCOM_RDP_Overload.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxCOM_RDP_Overload.SelectedIndex = 0;
                this.comboBoxCOM_Transmit_Overload.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxCOM_Transmit_Overload.SelectedIndex = 0;
                this.comboBoxCOM_MON_Sys_Disconect.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxCOM_MON_Sys_Disconect.SelectedIndex = 0;
                this.comboBoxCOM_Time_Source_Status.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxCOM_Time_Source_Status.SelectedIndex = 0;

                // SSR part
                this.comboBoxSSR_Channel_Status.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxSSR_Channel_Status.SelectedIndex = 0;
                this.comboBoxSSR_Antenna_Selected.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxSSR_Antenna_Selected.SelectedIndex = 0;
                this.comboBoxSSR_Overloaded.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxSSR_Overloaded.SelectedIndex = 0;
                this.comboBoxSSR_Mon_Sys_Disconect.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxSSR_Mon_Sys_Disconect.SelectedIndex = 0;

                // MDS part
                this.comboBoxMDS_Channel_Status.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxMDS_Channel_Status.SelectedIndex = 0;
                this.comboBoxMDS_Antena_Selected.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxMDS_Antena_Selected.SelectedIndex = 0;
                this.ccomboBoxMDS_Overoaded.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.ccomboBoxMDS_Overoaded.SelectedIndex = 0;
                this.comboBoxMDS_Mon_Sys_Disconect.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxMDS_Mon_Sys_Disconect.SelectedIndex = 0;
                this.comboBoxMDS_Ch_For_Cord.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxMDS_Ch_For_Cord.SelectedIndex = 0;
                this.comboBoxMDS_Ch_For_DataLink.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxMDS_Ch_For_DataLink.SelectedIndex = 0;
                this.comboBoxMDS_DataLink_Overload.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxMDS_DataLink_Overload.SelectedIndex = 0;
                this.comboBoxMDS_Comm_Overload.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxMDS_Comm_Overload.SelectedIndex = 0;

                // PSR part
                this.comboBoxPSR_Channel_Status.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxPSR_Channel_Status.SelectedIndex = 0;
                this.comboBoxPSR_Antenna_Selected.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxPSR_Antenna_Selected.SelectedIndex = 0;
                this.comboBoxPSR_Overloaded.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxPSR_Overloaded.SelectedIndex = 0;
                this.comboBoxPSR_Mon_Sys_Disconect.Items.Insert(0, "R " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " NO DATA");
                this.comboBoxPSR_Mon_Sys_Disconect.SelectedIndex = 0;


                this.labelAntenaPeriod034.Text = "N/A";
                MyCAT34I050UserData_Last_Cycle = new CAT34I050Types.CAT34I050UserData();
            }
        }

        private void ClearAllRadarOneEvents()
        {
            // COM part
            this.comboBoxCOM_Top_Status.Items.Clear();
            this.comboBoxCOM_RDPC_Selected.Items.Clear();
            this.comboBoxCOM_RDPC_Reset.Items.Clear();
            this.comboBoxCOM_RDP_Overload.Items.Clear();
            this.comboBoxCOM_Transmit_Overload.Items.Clear();
            this.comboBoxCOM_MON_Sys_Disconect.Items.Clear();
            this.comboBoxCOM_Time_Source_Status.Items.Clear();

            // SSR part
            this.comboBoxSSR_Channel_Status.Items.Clear();
            this.comboBoxSSR_Antenna_Selected.Items.Clear();
            this.comboBoxSSR_Overloaded.Items.Clear();
            this.comboBoxSSR_Mon_Sys_Disconect.Items.Clear();

            // MDS part
            this.comboBoxMDS_Channel_Status.Items.Clear();
            this.comboBoxMDS_Antena_Selected.Items.Clear();
            this.ccomboBoxMDS_Overoaded.Items.Clear();
            this.comboBoxMDS_Mon_Sys_Disconect.Items.Clear();
            this.comboBoxMDS_Ch_For_Cord.Items.Clear();
            this.comboBoxMDS_Ch_For_DataLink.Items.Clear();
            this.comboBoxMDS_DataLink_Overload.Items.Clear();
            this.comboBoxMDS_Comm_Overload.Items.Clear();

            // PSR part
            this.comboBoxPSR_Channel_Status.Items.Clear();
            this.comboBoxPSR_Antenna_Selected.Items.Clear();
            this.comboBoxPSR_Overloaded.Items.Clear();
            this.comboBoxPSR_Mon_Sys_Disconect.Items.Clear();

            MyCAT34I050UserData_Last_Cycle = new CAT34I050Types.CAT34I050UserData();
        }

        private void checkBoxDisplayPSR_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DisplayPSR = this.checkBoxDisplayPSR.Checked;
            Properties.Settings.Default.Save();
        }

        private void antennaRotationPeriodToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT34I041;
            MyDetailedView.Show();
        }

        private void serviceIdentificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT62I015;
            MyDetailedView.Show();
        }

        private void trackNumberToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT62I040;
            MyDetailedView.Show();
        }

        private void trackMode3ACodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT62I060;
            MyDetailedView.Show();
        }

        private void calculatedTrackPositionWGS84ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT62I105;
            MyDetailedView.Show();
        }

        private void measuredFlightLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT62I136;
            MyDetailedView.Show();
        }

        private void aircraftDerivedDataSUBF2ACIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT62I380_SUBF2_ACID;
            MyDetailedView.Show();
        }

        private void miscellaneousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MisscelaneousSettings MyForm = new MisscelaneousSettings();
            MyForm.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "ASTERIX Analyser Raw|*.raw";
            openFileDialog1.InitialDirectory = "Application.StartupPath";
            openFileDialog1.Title = "Open File to Read";

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                ResetDataBuffers();
                MainASTERIXDataStorage.ResetAllData();
                ProgressForm = new FileReadProgress();
                ProgressForm.Show();
                backgroundWorkerLoadData.RunWorkerAsync(openFileDialog1.FileName);
            }
        }

        private void textBoxSSRCode_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxSSRCode.Text.Length == 4)
            {
                if (this.comboBoxSSRFilterBox.Enabled)
                {
                    PopulateSSRCodeLookup();
                    int Index = 0;
                    foreach (var Item in this.comboBoxSSRFilterBox.Items)
                    {
                        if (this.textBoxSSRCode.Text == Item.ToString())
                        {
                            this.comboBoxSSRFilterBox.SelectedIndex = Index;
                            break;
                        }
                        Index++;
                    }

                }
                Update_PlotTrack_Data();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ReadDataReportMessage = ASTERIX.DecodeAsterixData((string)e.Argument);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressForm.NotifyFinishReading(ReadDataReportMessage);
            if (this.checkEnableDisplay.Checked == true)
                Update_PlotTrack_Data();
        }

        private void checkBoxFillListBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PopulateMainListBox = this.checkBoxFillListBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBoxRecording_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxRecording.Checked == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                if (Properties.Settings.Default.RecordActiveInRaw == true)
                    saveFileDialog.Filter = "ASTERIX Analyser Raw|*.raw";
                else
                    saveFileDialog.Filter = "ASTERIX Analyser Replay|*.rply";

                saveFileDialog.InitialDirectory = "Application.StartupPath";
                saveFileDialog.Title = "Select file location and file name";

                if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
                {
                    SharedData.DataRecordingClass.FilePathandName = saveFileDialog.FileName;
                    SharedData.DataRecordingClass.DataRecordingRequested = true;
                }
                else
                {
                    this.checkBoxRecording.Checked = false;
                }
            }
            else
            {
                SharedData.DataRecordingClass.DataRecordingRequested = false;
            }

            this.checkBoxRecordInRaw.Enabled = (this.checkBoxRecording.Checked == false);
        }

        private void recorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsterixRecorder.Visible = true;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void recorderAndDataForwarderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsterixRecorder.Visible = true;
        }

        private void googleEarthToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GoogleEarthProvider MyGE_Provider = new GoogleEarthProvider();
            MyGE_Provider.Show();
        }

        private void googleEarthToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            GoogleEarthProvider MyGE_Provider = new GoogleEarthProvider();
            MyGE_Provider.Show();
        }

        private void comboBoxLiveDisplayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Update_PlotTrack_Data();
        }

        private void replayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplayForm.Location = System.Windows.Forms.Cursor.Position;
            if (ReplayForm.WindowState == FormWindowState.Minimized)
                ReplayForm.WindowState = FormWindowState.Normal;
            else
            {
                ReplayForm.Visible = true;
            }
        }

        private void checkBoxRecordInRaw_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RecordActiveInRaw = this.checkBoxRecordInRaw.Checked;
            Properties.Settings.Default.Save();
        }

        private void openAsterixReplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "ASTERIX Analyser Replay|*.rply";
            openFileDialog1.InitialDirectory = "Application.StartupPath";
            openFileDialog1.Title = "Open File to Read";

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                this.labelBytesReplayed.Text = openFileDialog1.SafeFileName;
            }
        }

        private void btnStartStopFileReplay_Click(object sender, EventArgs e)
        {
            if (ReplayForm.WindowState == FormWindowState.Minimized)
                ReplayForm.WindowState = FormWindowState.Normal;
            else
            {
                ReplayForm.Visible = true;
            }
            ReplayForm.Location = System.Windows.Forms.Cursor.Position;
        }

        private void replayToRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReplayToRaw RtoR = new FrmReplayToRaw();
            RtoR.Show();
        }

        private void systemConfigurationAndStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetailedView MyDetailedView = new FrmDetailedView();
            MyDetailedView.WhatToDisplay = FrmDetailedView.DisplayType.CAT34I050;
            MyDetailedView.Show();
        }

        private void tabPageSysStatus_Click(object sender, EventArgs e)
        {

        }

        private void groupBoxDisplayStatus_Enter(object sender, EventArgs e)
        {

        }

        private void groupBoxDisplaySSR_Enter(object sender, EventArgs e)
        {

        }

        private void groupBoxOneModeS_Enter(object sender, EventArgs e)
        {

        }

        private void groupBoxOneCOM_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void listViewCOM_NOGO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxTop_Status_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void comboBoxTop_Status_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxCOM_Top_Status_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxCOM_RDPC_Selected_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxCOM_RDPC_Reset_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxCOM_RDP_Overload_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxCOM_Transmit_Overload_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxCOM_MON_Sys_Disconect_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxCOM_Time_Source_Status_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void checkBoxSystMonEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxSystMonEnabled.Checked)
            {
                this.checkBoxSystMonEnabled.Text = "System Monitoring ENABLED";
                Properties.Settings.Default.SystMonEnabled = this.checkBoxSystMonEnabled.Checked;
                Properties.Settings.Default.Save();
                groupBoxSysStatCAT34.Enabled = true;
            }
            else
            {
                this.checkBoxSystMonEnabled.Text = "System Monitoring DISABLED";
                Properties.Settings.Default.SystMonEnabled = this.checkBoxSystMonEnabled.Checked;
                Properties.Settings.Default.Save();
                HandleNoDataForCAT034I050(true);
                groupBoxSysStatCAT34.Enabled = false;
            }
        }

        private void btnAckowledgeRadar1_Click(object sender, EventArgs e)
        {
            ClearAllRadarOneEvents();
        }

        private void comboBoxSSR_Channel_Status_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxSSR_Antenna_Selected_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxSSR_Overloaded_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxSSR_Mon_Sys_Disconect_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxMDS_Channel_Status_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxMDS_Antena_Selected_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void ccomboBoxMDS_Overoaded_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxMDS_Mon_Sys_Disconect_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxMDS_Ch_For_Cord_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxMDS_Ch_For_DataLink_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxMDS_DataLink_Overload_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxMDS_Comm_Overload_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxPSR_Channel_Status_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxPSR_Antenna_Selected_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxPSR_Overloaded_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void comboBoxPSR_Mon_Sys_Disconect_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawStringandRectangleinComboBox(sender, e);
        }

        private void gMapControl_OnMapZoomChanged()
        {
            this.lblZoomLevel.Text = gMapControl.Zoom.ToString();
        }

        private void gMapControl_OnMapDrag()
        {
            UpdatelblCenter();
        }

        // Handle custom key presses
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (gMapControl.Visible)
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (gMapControl.Zoom > 12)
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat + 0.005, gMapControl.Position.Lng);
                    else if (gMapControl.Zoom > 10)
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat + 0.05, gMapControl.Position.Lng);
                    else if (gMapControl.Zoom > 8)
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat + 0.2, gMapControl.Position.Lng);
                    else
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat + 0.5, gMapControl.Position.Lng);
                    UpdatelblCenter();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (gMapControl.Zoom > 12)
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat - 0.005, gMapControl.Position.Lng);
                    else if (gMapControl.Zoom > 10)
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat - 0.05, gMapControl.Position.Lng);
                    else if (gMapControl.Zoom > 8)
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat - 0.2, gMapControl.Position.Lng);
                    else
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat - 0.5, gMapControl.Position.Lng);
                    UpdatelblCenter();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Left)
                {
                    if (gMapControl.Zoom > 12)
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng - 0.005);
                    else if (gMapControl.Zoom > 10)
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng - 0.05);
                    else if (gMapControl.Zoom > 8)
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng - 0.2);
                    else
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng - 0.5);
                    UpdatelblCenter();
                    e.Handled = true;

                }
                else if (e.KeyCode == Keys.Right)
                {
                    if (gMapControl.Zoom > 12)
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng + 0.005);
                    else if (gMapControl.Zoom > 10)
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng + 0.05);
                    if (gMapControl.Zoom > 8)
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng + 0.2);
                    else
                        gMapControl.Position = new PointLatLng(gMapControl.Position.Lat, gMapControl.Position.Lng + 0.5);
                    UpdatelblCenter();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Add)
                {
                    gMapControl.Zoom = gMapControl.Zoom + 1;
                    this.lblZoomLevel.Text = gMapControl.Zoom.ToString();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Subtract)
                {
                    gMapControl.Zoom = gMapControl.Zoom - 1;
                    this.lblZoomLevel.Text = gMapControl.Zoom.ToString();
                    e.Handled = true;
                }
            }
        }

        private void extendedLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtendedLabel.Visible = true;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (gMapControl.Location.X != 0)
                gMapControl.Location = new Point(0, 0);
            else
                gMapControl.Location = new Point(136, 0);
        }

        private void checkBoxFullscreen_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxFullscreen.Checked)
            {
                gMapControl.Location = new Point(0, 0);
                gMapControl.Width = this.Width - 35;
            }
            else
            {
                gMapControl.Location = new Point(136, 0);
                gMapControl.Width = this.Width - 170;
            }
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void labelTemp_Click(object sender, EventArgs e)
        {

        }

    }
}

