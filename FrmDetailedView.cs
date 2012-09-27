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
    public partial class FrmDetailedView : Form
    {
        public enum DisplayType
        {
            // CAT 001
            CAT01I020, CAT01I040, CAT01I070, CAT01I090,

            // CAT 002
            CAT02I000, CAT02I020, CAT02I030, CAT02I041,

            // CAT 048
            CAT48I020, CAT48I040, CAT48I070, CAT48I090, CAT48I240,

            Nothing
        };

        // To determine what to display
        public DisplayType WhatToDisplay = DisplayType.Nothing;

        public FrmDetailedView()
        {
            InitializeComponent();
        }

        private void FrmDetailedView_Load(object sender, EventArgs e)
        {

        }

        private void FrmDetailedView_Shown(object sender, EventArgs e)
        {
            // Always clear the prviosuky displayed data
            this.listBoxMainDataBox.Items.Clear();

            if (WhatToDisplay == DisplayType.Nothing)
            {
                this.listBoxMainDataBox.Items.Add("Hey moron, specify something to display !!!");
            }

            //////////////////////////////////////////////////////////////////////////////////////
            //                                      CAT 01 Data                                 //
            //////////////////////////////////////////////////////////////////////////////////////
            #region CAT01_Call_Display_Region
            // TARGET REPORT DESCRIPTOR                    
            else if (WhatToDisplay == DisplayType.CAT01I020)
            {
                DisplayCAT01I020Data();
            }
            // MEASURED POSITION IN POLAR                  
            else if (WhatToDisplay == DisplayType.CAT01I040)
            {
                DisplayCAT01I040Data();
            }
            // Mode 3A Code                                
            else if (WhatToDisplay == DisplayType.CAT01I070)
            {
                DisplayCAT01I070Data();
            }
            // FLIGHT LEVEL                                
            else if (WhatToDisplay == DisplayType.CAT01I090)
            {
                DisplayCAT01I090Data();
            }
            #endregion

            //////////////////////////////////////////////////////////////////////////////////////
            //                                      CAT 02 Data                                 //
            //////////////////////////////////////////////////////////////////////////////////////
            #region CAT02_Call_Display_Region
            // MESSAGE TYPE                    
            else if (WhatToDisplay == DisplayType.CAT02I000)
            {
                DisplayCAT02I000Data();
            }
            // SECTOR NUMBER
            else if (WhatToDisplay == DisplayType.CAT02I020)
            {
                DisplayCAT02I020Data();
            }
            // TIME OF DAY
            else if (WhatToDisplay == DisplayType.CAT02I030)
            {
                DisplayCAT02I030Data();
            }
            // ANTENNA ROTATION TYPE
            else if (WhatToDisplay == DisplayType.CAT02I041)
            {
                DisplayCAT02I041Data();
            }
            #endregion

            //////////////////////////////////////////////////////////////////////////////////////
            //                                      CAT 48 Data                                 //
            //////////////////////////////////////////////////////////////////////////////////////
            #region CAT48_Call_Display_Region
            // TARGET REPORT DESCRIPTOR                    
            else if (WhatToDisplay == DisplayType.CAT48I020)
            {
                DisplayCAT48I020Data();
            }
            // MEASURED POSITION IN POLAR                  
            else if (WhatToDisplay == DisplayType.CAT48I040)
            {
                DisplayCAT48I040Data();
            }
            // Mode 3A Code                                
            else if (WhatToDisplay == DisplayType.CAT48I070)
            {
                DisplayCAT48I070Data();
            }
            // FLIGHT LEVEL                                
            else if (WhatToDisplay == DisplayType.CAT48I090)
            {
                DisplayCAT48I090Data();
            }
            else if (WhatToDisplay == DisplayType.CAT48I240)
            {
                DisplayCAT48I240Data();
            }
            #endregion
        }

        #region CAT048_Display_Method_Region
        private void DisplayCAT48I090Data()
        {
            if (MainASTERIXDataStorage.CAT48Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT48Message[0].I048DataItems[CAT48.ItemIDToIndex("090")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT48Message[0].I048DataItems[CAT48.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                {
                    if (Msg.I048DataItems[CAT48.ItemIDToIndex("090")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.I048DataItems[CAT48.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        // Display Data
                        CAT48I090Types.CAT48I090FlightLevelUserData MyData = (CAT48I090Types.CAT48I090FlightLevelUserData)Msg.I048DataItems[CAT48.ItemIDToIndex("090")].value;
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Code_Validated.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Code_Garbled.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.FlightLevel.ToString());
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }
        }

        private void DisplayCAT48I070Data()
        {

            if (MainASTERIXDataStorage.CAT48Message.Count > 0)
            {

                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT48Message[0].I048DataItems[CAT48.ItemIDToIndex("070")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT48Message[0].I048DataItems[CAT48.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                {

                    if (Msg.I048DataItems[CAT48.ItemIDToIndex("070")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.I048DataItems[CAT48.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);


                        CAT48I070Types.CAT48I070Mode3UserData MyData = (CAT48I070Types.CAT48I070Mode3UserData)Msg.I048DataItems[CAT48.ItemIDToIndex("070")].value;
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Code_Validated.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Code_Garbled.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Code_Smothed_Or_From_Transponder.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Mode3A_Code.ToString());
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }
        }

        private void DisplayCAT48I040Data()
        {
            if (MainASTERIXDataStorage.CAT48Message.Count > 0)
            {

                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT48Message[0].I048DataItems[CAT48.ItemIDToIndex("040")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT48Message[0].I048DataItems[CAT48.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                {
                    if (Msg.I048DataItems[CAT48.ItemIDToIndex("040")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.I048DataItems[CAT48.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates MyData = (CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates)Msg.I048DataItems[CAT48.ItemIDToIndex("040")].value;
                        this.listBoxMainDataBox.Items.Add("\tDistance:\t" + MyData.Measured_Distance);
                        this.listBoxMainDataBox.Items.Add("\tAzimuth:\t" + MyData.Measured_Azimuth.ToString());
                        string Lat, Lon;
                        MyData.LatLong.GetDegMinSecStringFormat(out Lat, out Lon);
                        this.listBoxMainDataBox.Items.Add("\tLat/Long:\t" + Lat + "/" + Lon);
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }
        }

        private void DisplayCAT48I020Data()
        {
            if (MainASTERIXDataStorage.CAT48Message.Count > 0)
            {

                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT48Message[0].I048DataItems[CAT48.ItemIDToIndex("020")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT48Message[0].I048DataItems[CAT48.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                {
                    if (Msg.I048DataItems[CAT48.ItemIDToIndex("020")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.I048DataItems[CAT48.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT48I020UserData MyData = (CAT48I020UserData)Msg.I048DataItems[CAT48.ItemIDToIndex("020")].value;
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Type_Of_Report.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Simulated_Or_Actual.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.RDP_Chain.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Special_Position_Ind.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Data_From_FFT.ToString());

                        if (MyData.Next_Extension_1 == CAT48I020Types.Next_Extension_Type.Yes)
                        {
                            this.listBoxMainDataBox.Items.Add("  *** Extension fileds ***");
                            this.listBoxMainDataBox.Items.Add("\t" + MyData.Test_Target_Indicator.ToString());
                            this.listBoxMainDataBox.Items.Add("\t" + MyData.Military_Emergency.ToString());
                            this.listBoxMainDataBox.Items.Add("\t" + MyData.Military_Identification.ToString());
                            this.listBoxMainDataBox.Items.Add("\t" + MyData.FOE_or_FRI.ToString());
                        }
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }
        }
        private void DisplayCAT48I240Data()
        {
            if (MainASTERIXDataStorage.CAT48Message.Count > 0)
            {

                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT48Message[0].I048DataItems[CAT48.ItemIDToIndex("240")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT48Message[0].I048DataItems[CAT48.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                {

                    if (Msg.I048DataItems[CAT48.ItemIDToIndex("240")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.I048DataItems[CAT48.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT48I240Types.CAT48I240ACID_Data ACID_String = (CAT48I240Types.CAT48I240ACID_Data)Msg.I048DataItems[CAT48.ItemIDToIndex("240")].value;
                        this.listBoxMainDataBox.Items.Add("\t" + "Callsign:" + ACID_String.ACID);


                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }
        }
        #endregion

        #region CAT001_Display_Method_Region
        private void DisplayCAT01I090Data()
        {
            if (MainASTERIXDataStorage.CAT01Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT01.ItemIDToIndex("090")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT01.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT01Data Msg in MainASTERIXDataStorage.CAT01Message)
                {
                    if (Msg.I001DataItems[CAT01.ItemIDToIndex("090")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.I001DataItems[CAT01.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        // Display Data
                        CAT01I090Types.CAT01I090FlightLevelUserData MyData = (CAT01I090Types.CAT01I090FlightLevelUserData)Msg.I001DataItems[CAT01.ItemIDToIndex("090")].value;
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Code_Validated.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Code_Garbled.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.FlightLevel.ToString());
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }
        }

        private void DisplayCAT01I070Data()
        {
            if (MainASTERIXDataStorage.CAT01Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT01.ItemIDToIndex("070")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT01.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT01Data Msg in MainASTERIXDataStorage.CAT01Message)
                {
                    if (Msg.I001DataItems[CAT01.ItemIDToIndex("070")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.I001DataItems[CAT01.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);
                        CAT01I070Types.CAT01070Mode3UserData MyData = (CAT01I070Types.CAT01070Mode3UserData)Msg.I001DataItems[CAT01.ItemIDToIndex("070")].value;

                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Code_Validated.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Code_Garbled.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Code_Smothed_Or_From_Transponder.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Mode3A_Code.ToString());
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }
        }

        private void DisplayCAT01I040Data()
        {
            if (MainASTERIXDataStorage.CAT01Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT01.ItemIDToIndex("040")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT01.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT01Data Msg in MainASTERIXDataStorage.CAT01Message)
                {
                    if (Msg.I001DataItems[CAT01.ItemIDToIndex("040")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.I001DataItems[CAT01.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates MyData = (CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates)Msg.I001DataItems[CAT01.ItemIDToIndex("040")].value;
                        this.listBoxMainDataBox.Items.Add("\tDistance:\t" + MyData.Measured_Distance);
                        this.listBoxMainDataBox.Items.Add("\tAzimuth:\t" + MyData.Measured_Azimuth.ToString());
                        string Lat, Lon;
                        MyData.LatLong.GetDegMinSecStringFormat(out Lat, out Lon);
                        this.listBoxMainDataBox.Items.Add("\tLat/Long:\t" + Lat + "/" + Lon);
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }
        }

        private void DisplayCAT01I020Data()
        {
            if (MainASTERIXDataStorage.CAT01Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT01.ItemIDToIndex("020")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT01.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT01Data Msg in MainASTERIXDataStorage.CAT01Message)
                {
                    if (Msg.I001DataItems[CAT01.ItemIDToIndex("020")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.I001DataItems[CAT01.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT01I020UserData MyData = (CAT01I020UserData)Msg.I001DataItems[CAT01.ItemIDToIndex("020")].value;
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Type_Of_Report.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Simulated_Or_Actual_Report.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Type_Of_Radar_Detection.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Antena_Source.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Special_Position_Ind.ToString());
                        this.listBoxMainDataBox.Items.Add("\t" + MyData.Data_Is_From_FFT.ToString());

                        if (MyData.Next_Extension_1 == CAT01I020Types.Next_Extension_Type.Yes)
                        {
                            this.listBoxMainDataBox.Items.Add("  *** Extension fields ***  ");
                            this.listBoxMainDataBox.Items.Add("\t" + MyData.Is_Test_Target_Indicator.ToString());
                            this.listBoxMainDataBox.Items.Add("\t" + MyData.Special_SSR_Codes.ToString());
                            this.listBoxMainDataBox.Items.Add("\t" + MyData.Is_Military_Emergency.ToString());
                            this.listBoxMainDataBox.Items.Add("\t" + MyData.Is_Military_Identification.ToString());
                            this.listBoxMainDataBox.Items.Add("\t" + MyData.Next_Extension_2.ToString());
                        }
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }
        }
        #endregion

        #region CAT002_Display_Method_Region
        private void DisplayCAT02I000Data()
        {
            if (MainASTERIXDataStorage.CAT02Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT02.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT02.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT02Data Msg in MainASTERIXDataStorage.CAT02Message)
                {
                    if (Msg.I002DataItems[CAT02.ItemIDToIndex("000")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.I002DataItems[CAT02.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT02I000Types.Message_Type MyData = (CAT02I000Types.Message_Type)Msg.I002DataItems[CAT02.ItemIDToIndex("000")].value;

                        this.listBoxMainDataBox.Items.Add("\t" + MyData.ToString());
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }
        }
        private void DisplayCAT02I020Data()
        {
            if (MainASTERIXDataStorage.CAT02Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT02.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT02.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT02Data Msg in MainASTERIXDataStorage.CAT02Message)
                {
                    if (Msg.I002DataItems[CAT02.ItemIDToIndex("020")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.I002DataItems[CAT02.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        double Sector_Number = (double)Msg.I002DataItems[CAT02.ItemIDToIndex("020")].value;

                        this.listBoxMainDataBox.Items.Add("\t" + "Sector Number: " + Sector_Number.ToString());
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }
        }
        private void DisplayCAT02I030Data()
        {

            if (MainASTERIXDataStorage.CAT02Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT02.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT02.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT02Data Msg in MainASTERIXDataStorage.CAT02Message)
                {
                    if (Msg.I002DataItems[CAT02.ItemIDToIndex("010")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.I002DataItems[CAT02.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT02I030Types.CAT02I030_Time_Of_The_Day_User_Type MyData = (CAT02I030Types.CAT02I030_Time_Of_The_Day_User_Type)Msg.I002DataItems[CAT02.ItemIDToIndex("030")].value;


                        this.listBoxMainDataBox.Items.Add("\t" + "Time of Day: " + MyData.TimeOfDay.ToString());
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }

        }
        private void DisplayCAT02I041Data()
        {
            if (MainASTERIXDataStorage.CAT02Message.Count > 0)
            {
                
                
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT02.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].I001DataItems[CAT02.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT02Data Msg in MainASTERIXDataStorage.CAT02Message)
                {

                    if (Msg.I002DataItems[CAT02.ItemIDToIndex("041")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.I002DataItems[CAT02.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        double Antenna_Rotation_Period = (double)Msg.I002DataItems[CAT02.ItemIDToIndex("041")].value;

                        this.listBoxMainDataBox.Items.Add("\t" + "Antenna Rotation Period: " + Antenna_Rotation_Period.ToString());
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }
        }
        #endregion

    }
}
