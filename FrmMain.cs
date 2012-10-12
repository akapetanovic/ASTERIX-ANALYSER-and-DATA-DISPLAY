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
        // Static Map Overlay
        GMapOverlay StaticOverlay;
        // Dynamic Map Overlay
        GMapOverlay DinamicOverlay;

        // Keep track of the last selected SSR code index
        int SSR_Filter_Last_Index = 0;

        // Define a lookup table for all possible SSR codes, well even more
        // then all possible but lets keep it simple.
        private bool[] SSR_Code_Lookup = new bool[7778];

        // Flags that the display target has just been enabled
        private bool FirstCycleDisplayEnabled = true;

        // Define the main listener thread
        Thread ListenForDataThread = new Thread(new ThreadStart(ASTERIX.ListenForData));

        GMapMarker currentMarker = null;
        bool isDraggingMarker = false;

        bool North_Marker_Received = false;

        public FormMain()
        {
            System.Threading.Thread.Sleep(600);

            InitializeComponent();

            SystemAdaptationDataSet.InitializeData();
            DynamicDisplayBuilder.Initialise();

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
            SettingDialog.Visible = true;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Initialize Map
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
            this.labelTrackCoast.Text = Properties.Settings.Default.TrackCoast.ToString();
            this.PlotandTrackDisplayUpdateTimer.Interval = Properties.Settings.Default.UpdateRate;
            this.labelDisplayUpdateRate.Text = "Update rate: " + this.PlotandTrackDisplayUpdateTimer.Interval.ToString() + "ms";
            this.checkEnableDisplay.Checked = Properties.Settings.Default.DisplayEnabled;
            this.checkBoxFLFilter.Checked = Properties.Settings.Default.FL_Filter_Enabled;
            this.numericUpDownUpper.Value = Properties.Settings.Default.FL_Upper;
            this.numericUpDownLower.Value = Properties.Settings.Default.FL_Lower;
            this.checkBoxDisplayPSR.Checked = Properties.Settings.Default.DisplayPSR;

            HandlePlotDisplayEnabledChanged();
        }

        private void InitializeMap()
        {
            // Set system origin position
            gMapControl.Position = new PointLatLng(SystemAdaptationDataSet.SystemOriginPoint.Lat, SystemAdaptationDataSet.SystemOriginPoint.Lng);
            this.lblCenterLat.Text = gMapControl.Position.Lat.ToString();
            this.lblCenterLon.Text = gMapControl.Position.Lng.ToString();

            // Choose MAP provider and MAP mode
            gMapControl.MapProvider = GMapProviders.GoogleTerrainMap;
            gMapControl.Manager.Mode = AccessMode.ServerAndCache;
            gMapControl.EmptyMapBackground = Color.Gray;

            // Set MIN/MAX for the ZOOM function
            gMapControl.MinZoom = 0;
            gMapControl.MaxZoom = 20;
            // Default ZOOM
            gMapControl.Zoom = 8;
            this.lblZoomLevel.Text = gMapControl.Zoom.ToString();

            // Add overlays
            StaticOverlay = new GMapOverlay(gMapControl, "OverlayTwo");
            gMapControl.Overlays.Add(StaticOverlay);
            DinamicOverlay = new GMapOverlay(gMapControl, "OverlayOne");
            gMapControl.Overlays.Add(DinamicOverlay);

            this.labelDisplayUpdateRate.Text = "Update rate: " + this.PlotandTrackDisplayUpdateTimer.Interval.ToString() + "ms";
            this.comboBox1.Text = "Plain";

            // Now build static display
            StaticDisplayBuilder.Build(ref StaticOverlay);
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
            MessageBox.Show("Asterix Display/Sniffer 1.4 by Amer Kapetanovic\nakapetanovic@gmail.com", "About");
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

                foreach (DynamicDisplayBuilder.TargetType Target in TargetList)
                {

                    if (Passes_Check_For_Flight_Level_Filter(Target.ModeC))
                    {
                        // If SSR code filtering is to be applied 
                        if (this.checkBoxFilterBySSR.Enabled == true &&
                            this.textBoxSSRCode.Enabled == true &&
                            this.textBoxSSRCode.Text.Length == 4)
                        {
                            if (Target.ModeA == this.textBoxSSRCode.Text)
                            {
                                Target.MyMarker.ToolTipMode = MarkerTooltipMode.Never;
                                Target.MyMarker.Position = new PointLatLng(Target.Lat, Target.Lon);
                                BuildDynamicLabelText(Target, ref Target.MyMarker);
                                SetLabelAttributes(ref Target.MyMarker);
                                DinamicOverlay.Markers.Add(Target.MyMarker);
                            }
                        }
                        else // No SSR filter so just display all of them
                        {
                            Target.MyMarker.ToolTipMode = MarkerTooltipMode.Never;
                            Target.MyMarker.Position = new PointLatLng(Target.Lat, Target.Lon);
                            BuildDynamicLabelText(Target, ref Target.MyMarker);
                            SetLabelAttributes(ref Target.MyMarker);
                            DinamicOverlay.Markers.Add(Target.MyMarker);
                        }
                    }
                }
            }
            else // Here handle display of passive display (buffered data)
            {
                DynamicDisplayBuilder.GetDisplayData(true, out TargetList);

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
                if (double.Parse(Target_Data.ModeC_Previous_Cycle) > double.Parse(Target_Data.ModeC))
                    Label_Data.ModeC_STRING = Label_Data.ModeC_STRING + "↓";
                else if (double.Parse(Target_Data.ModeC_Previous_Cycle) < double.Parse(Target_Data.ModeC))
                    Label_Data.ModeC_STRING = Label_Data.ModeC_STRING + "↑";
            }

            if (Target_Data.ACID_Mode_S != null)
                Label_Data.CALLSIGN_STRING = Target_Data.ACID_Mode_S;

            Label_Data.MyTargetIndex = Target_Data.TrackNumber;
        }

        private string ApplyCModeHisterysis(string Mode_C_In)
        {
            string Result = Mode_C_In;

            if (Properties.Settings.Default.DisplayModeC_As_FL == true)
            {
                double FL = double.Parse(Result);
                int Truncated = (int)FL;
                Result = Truncated.ToString();

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

            Marker_In.CALLSIGN_FONT = new Font(LabelAttributes.TextFont, LabelAttributes.TextSize,
            FontStyle.Bold | FontStyle.Regular);

            Marker_In.ModeC_FONT = new Font(LabelAttributes.TextFont, LabelAttributes.TextSize,
            FontStyle.Bold | FontStyle.Regular);

            Marker_In.CFL_FONT = new Font(LabelAttributes.TextFont, LabelAttributes.TextSize,
           FontStyle.Bold | FontStyle.Regular);


            // Label Border color
            //Marker_In.ToolTip.Stroke = new Pen(new SolidBrush(LabelAttributes.LineColor), LabelAttributes.LineWidth);
            //Marker_In.ToolTip.Stroke.DashStyle = LabelAttributes.LineStyle;

            // Label background color
            //Marker_In.ToolTip.Fill = Brushes.Transparent;

            // Align the text
            //Marker_In.ToolTip.Format.LineAlignment = StringAlignment.Center;
            //Marker_In.ToolTip.Format.Alignment = StringAlignment.Near;
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
                double FL = double.Parse(Flight_Level);

                if (FL < (double)this.numericUpDownLower.Value || FL > (double)this.numericUpDownUpper.Value)
                    Result = false;
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
            if (this.checkEnableDisplay.Checked == true)
            {
                this.checkBoxFilterBySSR.Enabled = true;
                this.comboBoxSSRFilterBox.Enabled = true;
                this.textBoxSSRCode.Enabled = true;
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
                        this.NorthMarkerTimer.Enabled = false;
                    }
                    else
                    {
                        this.PlotandTrackDisplayUpdateTimer.Enabled = false;
                        this.NorthMarkerTimer.Enabled = true;
                        this.textBoxUpdateRate.Enabled = false;
                        this.labelDisplayUpdateRate.Enabled = false;
                    }

                    this.checkBoxSyncToNM.Enabled = true;
                    this.textBox1TrackCoast.Enabled = true;
                }
                else
                {
                    this.checkEnableDisplay.Text = "Passive Enabled";
                    this.textBoxUpdateRate.Enabled = false;
                    this.checkBoxSyncToNM.Enabled = false;
                    this.textBox1TrackCoast.Enabled = false;
                }

                // Call Update_PlotTrack_Data() twice in order 
                // to populate the display right away, 
                // then the timer/North Mark will take over.
                Update_PlotTrack_Data();
                Update_PlotTrack_Data();
            }
            else
            {
                FirstCycleDisplayEnabled = true;
                this.checkBoxFilterBySSR.Enabled = false;
                this.comboBoxSSRFilterBox.Enabled = false;
                this.textBoxSSRCode.Enabled = false;
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
                this.checkBoxFilterBySSR.BackColor = Color.Transparent;
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
            if (this.comboBox1.Text == "Google Plain")
            {
                gMapControl.MapProvider = GMapProviders.GoogleMap;
            }
            else if (this.comboBox1.Text == "Google Satellite")
            {
                gMapControl.MapProvider = GMapProviders.GoogleSatelliteMap;
            }
            else if (this.comboBox1.Text == "Google Terrain")
            {
                gMapControl.MapProvider = GMapProviders.GoogleTerrainMap;
            }
            else if (this.comboBox1.Text == "Google Hybrid")
            {
                gMapControl.MapProvider = GMapProviders.GoogleHybridMap;
            }
            else if (this.comboBox1.Text == "Custom Built")
            {
                gMapControl.MapProvider = GMapProviders.EmptyProvider;

            }

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        // Update Mouse posistion on the control
        private void gMapControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isDraggingMarker && currentMarker != null)
            {
                PointLatLng MousePosition = gMapControl.FromLocalToLatLng(e.X, e.Y);
                GPoint MarkerPositionLocal = gMapControl.FromLatLngToLocal(currentMarker.Position);
                GPoint NewLabelPosition = new GPoint(MarkerPositionLocal.X - e.X, MarkerPositionLocal.Y - e.Y);
                GMapTargetandLabel MyMarker = (GMapTargetandLabel)currentMarker;
                MyMarker.LabelOffset = new Point(NewLabelPosition.X, NewLabelPosition.Y);
                gMapControl.Refresh();
            }
            else
            {
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
                                }
                                else
                                {
                                    GMapTargetandLabel MyMarker = (GMapTargetandLabel)m;
                                    MyMarker.ShowLabelBox = false;
                                }
                            }
                }
            }

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

                gMapControl.MapProvider = GMapProviders.EmptyProvider;
            }
        }

        private void gMapControl_KeyPress(object sender, KeyPressEventArgs e)
        {

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
            this.gMapControl.Size = new Size(this.tabPlotDisplay.Size.Width - 147, this.tabPlotDisplay.Size.Height - 12);
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
        }

        private void tabMainTab_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmSettings SettingDialog = new FrmSettings();
            SettingDialog.Visible = false;
            SettingDialog.Show(this);
            SettingDialog.Visible = true;
        }

        private void gMapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DisplayRightClickOptions MyForm = new DisplayRightClickOptions();
                MyForm.StartPosition = FormStartPosition.Manual;
                MyForm.Location = new Point(e.X + 75, e.Y + 150);
                MyForm.Show();
            }
        }

        private void gMapControl_MouseDown(object sender, MouseEventArgs e)
        {
            // Check if the user is trying to move the lable. If so then flag it by 
            // setting the flag isDraggingMareker to true
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
            else if (e.Button == MouseButtons.Right && SharedData.bool_Listen_for_Data == true)// 
            {
                // Check if the user clicked on the CFL field. If so then open up the dialog to
                // let him/ger to modify/enter the CFL
                GMapOverlay[] overlays = new GMapOverlay[] { DinamicOverlay };
                for (int i = overlays.Length - 1; i >= 0; i--)
                {
                    GMapOverlay o = overlays[i];
                    if (o != null && o.IsVisibile)
                        foreach (GMapMarker m in o.Markers)
                            if (m.IsVisible && m.IsHitTestVisible)
                            {
                                if (MouseIsOnTheCFL(e, m) )
                                {
                                    GMapTargetandLabel MyMarker = (GMapTargetandLabel)m;
                                    if (MyMarker.MyTargetIndex != -1)
                                    {
                                        UpdateCFL MyForm = new UpdateCFL();
                                        MyForm.TrackToUpdate = MyMarker.MyTargetIndex;
                                        MyForm.StartPosition = FormStartPosition.Manual;
                                        MyForm.Location = new Point(e.X + 175, e.Y + 65);
                                        MyForm.Show();
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
            Rectangle MyRectangle = new Rectangle(MyMarker.GetCFLStartPoint().X, MyMarker.GetCFLStartPoint().Y, 20,  10);
            return MyRectangle.Contains(new Point(Mouse.X, Mouse.Y));
        }

        private void gMapControl_MouseUp(object sender, MouseEventArgs e)
        {
            isDraggingMarker = false;
            currentMarker = null;
        }

        private void gMapControl_OnMarkerEnter(GMapMarker item)
        {
            if (!isDraggingMarker)
                currentMarker = item;
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Start the thread to listen for data
            ASTERIX.RequestStop();
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
        }

        private void groupBoxUpdateRate_Enter(object sender, EventArgs e)
        {

        }

        private void numericUpDownUpper_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FL_Upper = this.numericUpDownUpper.Value;
            Properties.Settings.Default.Save();
        }

        private void numericUpDownLower_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FL_Lower = this.numericUpDownLower.Value;
            Properties.Settings.Default.Save();
        }
        private void NorthMarkerTimer_Tick(object sender, EventArgs e)
        {
            if (North_Marker_Received == true)
            {
                North_Marker_Received = false;
                Update_PlotTrack_Data();
            }
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
    }
}

