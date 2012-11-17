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

            // CAT 34
            CAT34I000, CAT34I020, CAT34I030, CAT34I041, CAT34I050, CAT34I060,

            // CAT 62
            CAT62I015, CAT62I105, CAT62I060, CAT62I040, CAT62I136, CAT62I380_SUBF2_ACID,

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
            switch (WhatToDisplay)
            {
                //////////////////////////////////////////////////////////////////////////////////////
                //                                      CAT 01 Data                                 //
                //////////////////////////////////////////////////////////////////////////////////////
                #region CAT01_Call_Display_Region
                // TARGET REPORT DESCRIPTOR                    
                case DisplayType.CAT01I020:
                    DisplayCAT01I020Data();
                    break;
                // MEASURED POSITION IN POLAR                  
                case DisplayType.CAT01I040:
                    DisplayCAT01I040Data();
                    break;
                // Mode 3A Code                                
                case DisplayType.CAT01I070:
                    DisplayCAT01I070Data();
                    break;
                // FLIGHT LEVEL                                
                case DisplayType.CAT01I090:
                    DisplayCAT01I090Data();
                    break;
                #endregion

                //////////////////////////////////////////////////////////////////////////////////////
                //                                      CAT 02 Data                                 //
                //////////////////////////////////////////////////////////////////////////////////////
                #region CAT02_Call_Display_Region
                // MESSAGE TYPE                    
                case DisplayType.CAT02I000:
                    DisplayCAT02I000Data();
                    break;
                // SECTOR NUMBER
                case DisplayType.CAT02I020:
                    DisplayCAT02I020Data();
                    break;
                // TIME OF DAY
                case DisplayType.CAT02I030:
                    DisplayCAT02I030Data();
                    break;
                // ANTENNA ROTATION TYPE
                case DisplayType.CAT02I041:
                    DisplayCAT02I041Data();
                    break;
                #endregion

                //////////////////////////////////////////////////////////////////////////////////////
                //                                      CAT 48 Data                                 //
                //////////////////////////////////////////////////////////////////////////////////////
                #region CAT48_Call_Display_Region
                // TARGET REPORT DESCRIPTOR                    
                case DisplayType.CAT48I020:
                    DisplayCAT48I020Data();
                    break;
                // MEASURED POSITION IN POLAR                  
                case DisplayType.CAT48I040:
                    DisplayCAT48I040Data();
                    break;
                // Mode 3A Code                                
                case DisplayType.CAT48I070:
                    DisplayCAT48I070Data();
                    break;
                // FLIGHT LEVEL                                
                case DisplayType.CAT48I090:
                    DisplayCAT48I090Data();
                    break;
                case DisplayType.CAT48I240:
                    DisplayCAT48I240Data();
                    break;
                #endregion

                //////////////////////////////////////////////////////////////////////////////////////
                //                                      CAT 34 Data                                 //
                //////////////////////////////////////////////////////////////////////////////////////
                #region CAT34_Call_Display_Region
                // MESSAGE TYPE                    
                case DisplayType.CAT34I000:
                    DisplayCAT34I000Data();
                    break;
                // SECTOR NUMBER
                case DisplayType.CAT34I020:
                    DisplayCAT34I020Data();
                    break;
                // TIME OF DAY
                case DisplayType.CAT34I030:
                    DisplayCAT34I030Data();
                    break;
                // ANTENNA ROTATION PERIOD
                case DisplayType.CAT34I041:
                    DisplayCAT34I041Data();
                    break;
                // System Configuration and Status
                case DisplayType.CAT34I050:
                    DisplayCAT34I050Data();
                    break;
                #endregion
                //////////////////////////////////////////////////////////////////////////////////////
                //                                      CAT 62 Data                                 //
                //////////////////////////////////////////////////////////////////////////////////////
                #region CAT62_Call_Display_Region
                // MESSAGE TYPE                    
                case DisplayType.CAT62I015:
                    DisplayCAT62I015Data();
                    break;
                case DisplayType.CAT62I105:
                    DisplayCAT62I105Data();
                    break;
                case DisplayType.CAT62I060:
                    DisplayCAT62I060Data();
                    break;
                case DisplayType.CAT62I040:
                    DisplayCAT62I040Data();
                    break;
                case DisplayType.CAT62I136:
                    DisplayCAT62I136Data();
                    break;
                case DisplayType.CAT62I380_SUBF2_ACID:
                    DisplayCAT62I380_SUBF2_ACIDData();
                    break;
                #endregion
            }
        }

        #region CAT048_Display_Method_Region
        private void DisplayCAT48I090Data()
        {
            if (MainASTERIXDataStorage.CAT48Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT48Message[0].CAT48DataItems[CAT48.ItemIDToIndex("090")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT48Message[0].CAT48DataItems[CAT48.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                {
                    if (Msg.CAT48DataItems[CAT48.ItemIDToIndex("090")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT48DataItems[CAT48.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        // Display Data
                        CAT48I090Types.CAT48I090FlightLevelUserData MyData = (CAT48I090Types.CAT48I090FlightLevelUserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("090")].value;
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

                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT48Message[0].CAT48DataItems[CAT48.ItemIDToIndex("070")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT48Message[0].CAT48DataItems[CAT48.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                {

                    if (Msg.CAT48DataItems[CAT48.ItemIDToIndex("070")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT48DataItems[CAT48.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);


                        CAT48I070Types.CAT48I070Mode3UserData MyData = (CAT48I070Types.CAT48I070Mode3UserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("070")].value;
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

                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT48Message[0].CAT48DataItems[CAT48.ItemIDToIndex("040")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT48Message[0].CAT48DataItems[CAT48.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                {
                    if (Msg.CAT48DataItems[CAT48.ItemIDToIndex("040")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT48DataItems[CAT48.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates MyData = (CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates)Msg.CAT48DataItems[CAT48.ItemIDToIndex("040")].value;
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

                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT48Message[0].CAT48DataItems[CAT48.ItemIDToIndex("020")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT48Message[0].CAT48DataItems[CAT48.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                {
                    if (Msg.CAT48DataItems[CAT48.ItemIDToIndex("020")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT48DataItems[CAT48.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT48I020UserData MyData = (CAT48I020UserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("020")].value;
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

                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT48Message[0].CAT48DataItems[CAT48.ItemIDToIndex("240")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT48Message[0].CAT48DataItems[CAT48.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT48Data Msg in MainASTERIXDataStorage.CAT48Message)
                {

                    if (Msg.CAT48DataItems[CAT48.ItemIDToIndex("240")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT48DataItems[CAT48.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT48I240Types.CAT48I240ACID_Data ACID_String = (CAT48I240Types.CAT48I240ACID_Data)Msg.CAT48DataItems[CAT48.ItemIDToIndex("240")].value;
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
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT01.ItemIDToIndex("090")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT01.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT01Data Msg in MainASTERIXDataStorage.CAT01Message)
                {
                    if (Msg.CAT01DataItems[CAT01.ItemIDToIndex("090")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT01DataItems[CAT01.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        // Display Data
                        CAT01I090Types.CAT01I090FlightLevelUserData MyData = (CAT01I090Types.CAT01I090FlightLevelUserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("090")].value;
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
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT01.ItemIDToIndex("070")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT01.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT01Data Msg in MainASTERIXDataStorage.CAT01Message)
                {
                    if (Msg.CAT01DataItems[CAT01.ItemIDToIndex("070")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT01DataItems[CAT01.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);
                        CAT01I070Types.CAT01070Mode3UserData MyData = (CAT01I070Types.CAT01070Mode3UserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("070")].value;

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
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT01.ItemIDToIndex("040")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT01.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT01Data Msg in MainASTERIXDataStorage.CAT01Message)
                {
                    if (Msg.CAT01DataItems[CAT01.ItemIDToIndex("040")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT01DataItems[CAT01.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates MyData = (CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates)Msg.CAT01DataItems[CAT01.ItemIDToIndex("040")].value;
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
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT01.ItemIDToIndex("020")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT01.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT01Data Msg in MainASTERIXDataStorage.CAT01Message)
                {
                    if (Msg.CAT01DataItems[CAT01.ItemIDToIndex("020")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT01DataItems[CAT01.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT01I020UserData MyData = (CAT01I020UserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("020")].value;
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
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT02.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT02.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT02Data Msg in MainASTERIXDataStorage.CAT02Message)
                {
                    if (Msg.CAT02DataItems[CAT02.ItemIDToIndex("000")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT02DataItems[CAT02.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT02I000Types.Message_Type MyData = (CAT02I000Types.Message_Type)Msg.CAT02DataItems[CAT02.ItemIDToIndex("000")].value;

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
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT02.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT02.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT02Data Msg in MainASTERIXDataStorage.CAT02Message)
                {
                    if (Msg.CAT02DataItems[CAT02.ItemIDToIndex("020")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT02DataItems[CAT02.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        double Sector_Number = (double)Msg.CAT02DataItems[CAT02.ItemIDToIndex("020")].value;

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
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT02.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT02.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT02Data Msg in MainASTERIXDataStorage.CAT02Message)
                {
                    if (Msg.CAT02DataItems[CAT02.ItemIDToIndex("010")].value != null)
                    {

                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT02DataItems[CAT02.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT02I030Types.CAT02I030_Time_Of_The_Day_User_Type MyData = (CAT02I030Types.CAT02I030_Time_Of_The_Day_User_Type)Msg.CAT02DataItems[CAT02.ItemIDToIndex("030")].value;


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


                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT02.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT01Message[0].CAT01DataItems[CAT02.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT02Data Msg in MainASTERIXDataStorage.CAT02Message)
                {

                    if (Msg.CAT02DataItems[CAT02.ItemIDToIndex("041")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT02DataItems[CAT02.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        double Antenna_Rotation_Period = (double)Msg.CAT02DataItems[CAT02.ItemIDToIndex("041")].value;

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

        #region CAT034_Display_Method_Region
        private void DisplayCAT34I000Data()
        {
            if (MainASTERIXDataStorage.CAT34Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT34Message[0].CAT34DataItems[CAT34.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT34Message[0].CAT34DataItems[CAT34.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT34Data Msg in MainASTERIXDataStorage.CAT34Message)
                {
                    if (Msg.CAT34DataItems[CAT34.ItemIDToIndex("000")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT34DataItems[CAT34.ItemIDToIndex("010")].value;
                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);
                        CAT34I000Types.Message_Type MyData = (CAT34I000Types.Message_Type)Msg.CAT34DataItems[CAT34.ItemIDToIndex("000")].value;
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
        private void DisplayCAT34I020Data()
        {
            if (MainASTERIXDataStorage.CAT34Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT34Message[0].CAT34DataItems[CAT34.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT34Message[0].CAT34DataItems[CAT34.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT34Data Msg in MainASTERIXDataStorage.CAT34Message)
                {
                    if (Msg.CAT34DataItems[CAT34.ItemIDToIndex("020")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT34DataItems[CAT34.ItemIDToIndex("010")].value;
                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);
                        double Sector_Number = (double)Msg.CAT34DataItems[CAT34.ItemIDToIndex("020")].value;
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
        private void DisplayCAT34I030Data()
        {
            if (MainASTERIXDataStorage.CAT34Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT34Message[0].CAT34DataItems[CAT34.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT34Message[0].CAT34DataItems[CAT34.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT34Data Msg in MainASTERIXDataStorage.CAT34Message)
                {
                    if (Msg.CAT34DataItems[CAT34.ItemIDToIndex("010")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT34DataItems[CAT34.ItemIDToIndex("010")].value;
                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);
                        CAT34I030Types.CAT34I030_Time_Of_The_Day_User_Type MyData = (CAT34I030Types.CAT34I030_Time_Of_The_Day_User_Type)Msg.CAT34DataItems[CAT34.ItemIDToIndex("030")].value;
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
        private void DisplayCAT34I041Data()
        {
            if (MainASTERIXDataStorage.CAT34Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT34Message[0].CAT34DataItems[CAT02.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT34Message[0].CAT34DataItems[CAT02.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT34Data Msg in MainASTERIXDataStorage.CAT34Message)
                {
                    if (Msg.CAT34DataItems[CAT34.ItemIDToIndex("041")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT34DataItems[CAT34.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);
                        double Antenna_Rotation_Period = (double)Msg.CAT34DataItems[CAT34.ItemIDToIndex("041")].value;
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
        private void DisplayCAT34I050Data()
        {
            if (MainASTERIXDataStorage.CAT34Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT34Message[0].CAT34DataItems[CAT02.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT34Message[0].CAT34DataItems[CAT02.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT34Data Msg in MainASTERIXDataStorage.CAT34Message)
                {
                    if (Msg.CAT34DataItems[CAT34.ItemIDToIndex("050")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT34DataItems[CAT34.ItemIDToIndex("010")].value;

                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        CAT34I050Types.CAT34I050UserData CAT35I050Data = (CAT34I050Types.CAT34I050UserData)Msg.CAT34DataItems[CAT34.ItemIDToIndex("050")].value;
                        if (CAT35I050Data.COM_Data.Data_Present == true)
                        {
                            this.listBoxMainDataBox.Items.Add(" COM Subfield");
                            this.listBoxMainDataBox.Items.Add("   System NOGO: " + CAT35I050Data.COM_Data.System_is_NOGO.ToString());
                            this.listBoxMainDataBox.Items.Add("   RDPC-2 Selected: " + CAT35I050Data.COM_Data.RDPC2_Selected.ToString());
                            this.listBoxMainDataBox.Items.Add("   Reset of RDPC: " + CAT35I050Data.COM_Data.RDPC_Reset.ToString());
                            this.listBoxMainDataBox.Items.Add("   Overload in RDP: " + CAT35I050Data.COM_Data.RDP_Overloaded.ToString());
                            this.listBoxMainDataBox.Items.Add("   Overload in XMT: " + CAT35I050Data.COM_Data.Transmision_Sys_Overloaded.ToString());
                            this.listBoxMainDataBox.Items.Add("   RMC Disconnected: " + CAT35I050Data.COM_Data.Monitor_Sys_Disconected.ToString());
                            this.listBoxMainDataBox.Items.Add("   Time Src Invalid: " + CAT35I050Data.COM_Data.Time_Source_Invalid.ToString());
                        }
                        this.listBoxMainDataBox.Items.Add("    ");
                        
                        if (CAT35I050Data.PSR_Data.Data_Present == true)
                        {
                            this.listBoxMainDataBox.Items.Add(" PSR Subfield");
                            this.listBoxMainDataBox.Items.Add("   ANT-2 Selected: " + CAT35I050Data.PSR_Data.Ant_2_Selected.ToString());
                            this.listBoxMainDataBox.Items.Add("   Channel selected: " + CAT35I050Data.PSR_Data.CH_Status.ToString());
                            this.listBoxMainDataBox.Items.Add("   Overloaded: " + CAT35I050Data.PSR_Data.PSR_Overloaded.ToString());
                            this.listBoxMainDataBox.Items.Add("   RMC Disconnected: " + CAT35I050Data.PSR_Data.Monitor_Sys_Disconected.ToString());
                        }
                        this.listBoxMainDataBox.Items.Add("    ");
                        
                        if (CAT35I050Data.SSR_Data.Data_Present == true)
                        {
                            this.listBoxMainDataBox.Items.Add(" SSR Subfield");
                            this.listBoxMainDataBox.Items.Add("   ANT-2 Selected: " + CAT35I050Data.SSR_Data.Ant_2_Selected.ToString());
                            this.listBoxMainDataBox.Items.Add("   Channel selected: " + CAT35I050Data.SSR_Data.CH_Status.ToString());
                            this.listBoxMainDataBox.Items.Add("   Overloaded: " + CAT35I050Data.SSR_Data.SSR_Overloaded.ToString());
                            this.listBoxMainDataBox.Items.Add("   RMC Disconnected: " + CAT35I050Data.SSR_Data.Monitor_Sys_Disconected.ToString());
                        }
                        this.listBoxMainDataBox.Items.Add("    ");
                        
                        if (CAT35I050Data.MDS_Data.Data_Present == true)
                        {
                            this.listBoxMainDataBox.Items.Add(" MDS Subfield");
                            this.listBoxMainDataBox.Items.Add("   ANT-2 Selected: " + CAT35I050Data.MDS_Data.Ant_2_Selected.ToString());
                            this.listBoxMainDataBox.Items.Add("   Channel selected: " + CAT35I050Data.MDS_Data.CH_Status.ToString());
                            this.listBoxMainDataBox.Items.Add("   Overloaded: " + CAT35I050Data.MDS_Data.ModeS_Overloaded.ToString());
                            this.listBoxMainDataBox.Items.Add("   RMC Disconnected: " + CAT35I050Data.MDS_Data.Monitor_Sys_Disconected.ToString());
                            this.listBoxMainDataBox.Items.Add("   CH2 Sel Surrv Cor: " + CAT35I050Data.MDS_Data.CH2_For_Coordination_In_Use.ToString());
                            this.listBoxMainDataBox.Items.Add("   CH2 Sel Data Link: " + CAT35I050Data.MDS_Data.CH2_For_DataLink_In_Use.ToString());
                            this.listBoxMainDataBox.Items.Add("   Overl Surrv Cor: " + CAT35I050Data.MDS_Data.Coordination_Func_Overload.ToString());
                            this.listBoxMainDataBox.Items.Add("   Overl Data Link: " + CAT35I050Data.MDS_Data.DataLink_Func_Overload.ToString());
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

        #region CAT062_Display_Method_Region
        private void DisplayCAT62I015Data()
        {
            if (MainASTERIXDataStorage.CAT62Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT62Message[0].CAT62DataItems[CAT62.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT62Message[0].CAT62DataItems[CAT62.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT62Data Msg in MainASTERIXDataStorage.CAT62Message)
                {
                    if (Msg.CAT62DataItems[CAT62.ItemIDToIndex("015")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT62DataItems[CAT62.ItemIDToIndex("010")].value;
                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);
                        int MyData = (int)Msg.CAT62DataItems[CAT62.ItemIDToIndex("015")].value;
                        this.listBoxMainDataBox.Items.Add("\tService ID: " + MyData.ToString());
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }
        }
        private void DisplayCAT62I105Data()
        {
            if (MainASTERIXDataStorage.CAT62Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT62Message[0].CAT62DataItems[CAT62.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT62Message[0].CAT62DataItems[CAT62.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT62Data Msg in MainASTERIXDataStorage.CAT62Message)
                {
                    if (Msg.CAT62DataItems[CAT62.ItemIDToIndex("105")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT62DataItems[CAT62.ItemIDToIndex("010")].value;
                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);
                        GeoCordSystemDegMinSecUtilities.LatLongClass MyData = (GeoCordSystemDegMinSecUtilities.LatLongClass)Msg.CAT62DataItems[CAT62.ItemIDToIndex("105")].value;
                        string Lat, Lon = "";
                        MyData.GetDegMinSecStringFormat(out Lat, out Lon);
                        this.listBoxMainDataBox.Items.Add("\tLat: " + Lat);
                        this.listBoxMainDataBox.Items.Add("\tLon: " + Lon);
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
            else
            {
                this.listBoxMainDataBox.Items.Add("No data of this CAT/Item was received !!!");
            }
        }
        private void DisplayCAT62I060Data()
        {
            if (MainASTERIXDataStorage.CAT62Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT62Message[0].CAT62DataItems[CAT62.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT62Message[0].CAT62DataItems[CAT62.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT62Data Msg in MainASTERIXDataStorage.CAT62Message)
                {
                    if (Msg.CAT62DataItems[CAT62.ItemIDToIndex("060")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT62DataItems[CAT62.ItemIDToIndex("010")].value;
                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);
                        CAT62I060Types.CAT62060Mode3UserData MyData = (CAT62I060Types.CAT62060Mode3UserData)Msg.CAT62DataItems[CAT62.ItemIDToIndex("060")].value;
                        this.listBoxMainDataBox.Items.Add("\tMode 3A has changed: " + MyData.Mode_3A_Has_Changed.ToString());
                        this.listBoxMainDataBox.Items.Add("\tMode 3A Code: " + MyData.Mode3A_Code);
                        this.listBoxMainDataBox.Items.Add("    ");
                    }

                }
            }
        }
        private void DisplayCAT62I040Data()
        {
            if (MainASTERIXDataStorage.CAT62Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT62Message[0].CAT62DataItems[CAT62.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT62Message[0].CAT62DataItems[CAT62.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT62Data Msg in MainASTERIXDataStorage.CAT62Message)
                {
                    if (Msg.CAT62DataItems[CAT62.ItemIDToIndex("040")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT62DataItems[CAT62.ItemIDToIndex("010")].value;
                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        int MyData = (int)Msg.CAT62DataItems[CAT62.ItemIDToIndex("040")].value;
                        this.listBoxMainDataBox.Items.Add("\tTrack Number: " + MyData.ToString());
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
        }
        private void DisplayCAT62I136Data()
        {
            if (MainASTERIXDataStorage.CAT62Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT62Message[0].CAT62DataItems[CAT62.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT62Message[0].CAT62DataItems[CAT62.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT62Data Msg in MainASTERIXDataStorage.CAT62Message)
                {
                    if (Msg.CAT62DataItems[CAT62.ItemIDToIndex("136")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT62DataItems[CAT62.ItemIDToIndex("010")].value;
                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);

                        double MyData = (double)Msg.CAT62DataItems[CAT62.ItemIDToIndex("136")].value;
                        this.listBoxMainDataBox.Items.Add("\tMeasured FL: " + MyData.ToString());
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
        }
        private void DisplayCAT62I380_SUBF2_ACIDData()
        {
            if (MainASTERIXDataStorage.CAT62Message.Count > 0)
            {
                this.listBoxMainDataBox.Items.Add("Detailed view of: " + MainASTERIXDataStorage.CAT62Message[0].CAT62DataItems[CAT62.ItemIDToIndex("010")].Description);
                ASTERIX.SIC_SAC_Time SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)MainASTERIXDataStorage.CAT62Message[0].CAT62DataItems[CAT62.ItemIDToIndex("010")].value;
                this.listBoxMainDataBox.Items.Add("SIC/SAC: " + SIC_SAC_TIME.SIC.ToString() + "/" + SIC_SAC_TIME.SAC.ToString());
                this.listBoxMainDataBox.Items.Add("    ");

                // Here determine the flag indicating what data is to be shown
                foreach (MainASTERIXDataStorage.CAT62Data Msg in MainASTERIXDataStorage.CAT62Message)
                {
                    if (Msg.CAT62DataItems[CAT62.ItemIDToIndex("380")].value != null)
                    {
                        SIC_SAC_TIME = (ASTERIX.SIC_SAC_Time)Msg.CAT62DataItems[CAT62.ItemIDToIndex("010")].value;
                        // Display time
                        string Time = SIC_SAC_TIME.TimeofReception.Hour.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Minute.ToString().PadLeft(2, '0') + ":" +
                            SIC_SAC_TIME.TimeofReception.Second.ToString().PadLeft(2, '0') + ":" + SIC_SAC_TIME.TimeofReception.Millisecond.ToString().PadLeft(3, '0');
                        this.listBoxMainDataBox.Items.Add("Rcvd Time: " + Time);
                        CAT62I380Types.CAT62I380Data MyData = (CAT62I380Types.CAT62I380Data)Msg.CAT62DataItems[CAT62.ItemIDToIndex("380")].value;
                        this.listBoxMainDataBox.Items.Add("\tACID: " + MyData.ACID.ACID_String);
                        this.listBoxMainDataBox.Items.Add("    ");
                    }
                }
            }
        }
        #endregion
    }
}
