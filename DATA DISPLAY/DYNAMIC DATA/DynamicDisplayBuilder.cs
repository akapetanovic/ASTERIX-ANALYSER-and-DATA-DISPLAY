using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using GMap.NET;
using GMap.NET.MapProviders;
using System.Drawing;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser
{
    class DynamicDisplayBuilder
    {
        public class TargetType
        {
            /// <summary>
            /// ////////////////////////////////////////////////
            /// Track Label Items
            /// </summary>
            public string ModeA;
            public string ModeC;
            public string GSPD;
            public string ModeC_Previous_Cycle;
            public string ACID_Mode_S;
            public double Lat;
            public double Lon;
            /// <summary>
            /// ////////////////////////////////////////////////////
            /// Extended label items, Applicable to CAT48 and CAT62
            /// </summary>
            public string TAS = "N/A";
            public string IAS = "N/A";
            public string MACH = "N/A";
            public string M_HDG = "N/A";
            public string TRK = "N/A";
            public string Roll_Ang = "N/A";
            public string SelectedAltitude_ShortTerm = "N/A";
            public string SelectedAltitude_LongTerm = "N/A";
            public string Rate_Of_Climb = "N/A";
            public string Barometric_Setting = "N/A";
            /// <summary>
            /// ////////////////////////////////////////////////////
            /// Internal stuff
            /// </summary>
            public int TrackNumber = -1;
            public int TrackTerminateTreshold = Properties.Settings.Default.TrackCoast;
            // Marker properties
            public GMapTargetandLabel MyMarker = new GMapTargetandLabel(new PointLatLng(0, 0));
        }

        // Keeps track of the data index from the last update of the 
        // display. Used in order to be able to extract only targets recived
        // since the last data update. 
        private static int LastDataIndex = 0;

        // This method is to be used to reset the data index when the main data buffer is reseted
        // and a new block of data is te be recived.
        public static int ResetGetDataIndex
        {
            get { return LastDataIndex; }
            set { LastDataIndex = 0; }
        }

        private static System.Collections.Generic.List<TargetType> CurrentTargetList = new System.Collections.Generic.List<TargetType>();
        private static System.Collections.Generic.List<TargetType> GlobalTargetList = new System.Collections.Generic.List<TargetType>();
        private static System.Collections.Generic.List<TargetType> PSRTargetList = new System.Collections.Generic.List<TargetType>();

        public static void UpdateCFL(int Index, string CFL_Value)
        {
            GlobalTargetList[Index].MyMarker.CFL_STRING = ' ' + CFL_Value;
        }

        public static void UpdateHDG(int Index, string HDG_Value)
        {
            GlobalTargetList[Index].MyMarker.A_HDG_STRING = 'h' + HDG_Value;
        }

        public static void UpdateSPD(int Index, string SPD_Value)
        {
            GlobalTargetList[Index].MyMarker.A_SPD_STRING = 's' + SPD_Value;
        }

        public static void ActivateSEPTool(int Index, int TrackToMonitor)
        {
            GlobalTargetList[Index].MyMarker.TargetToMonitor = TrackToMonitor;
            GlobalTargetList[TrackToMonitor].MyMarker.TargetMonitoredBy = Index;
        }

        public static void DeactivateSEPTool(int Index, int TargetMonitoredBy)
        {
            GlobalTargetList[Index].MyMarker.TargetToMonitor = -1;
            GlobalTargetList[TargetMonitoredBy].MyMarker.TargetMonitoredBy = -1;
        }

        public static void Initialise()
        {
            GlobalTargetList.Clear();
            for (int I = 0; I < 65536; I++)
            {
                GlobalTargetList.Add(new TargetType());
            }
        }

        public static Point GetTargetPositionByIndex(int Index)
        {
            return GlobalTargetList[Index].MyMarker.LocalPosition;
        }

        private static void UpdateGlobalList()
        {
            foreach (TargetType CurrentTarget in CurrentTargetList)
            {
                CurrentTarget.TrackTerminateTreshold = 0;
                if (CurrentTarget.TrackNumber != -1)
                {
                    GlobalTargetList[CurrentTarget.TrackNumber].ModeA = CurrentTarget.ModeA;
                    GlobalTargetList[CurrentTarget.TrackNumber].ModeC_Previous_Cycle = "";
                    if (GlobalTargetList[CurrentTarget.TrackNumber].ModeC != null)
                        GlobalTargetList[CurrentTarget.TrackNumber].ModeC_Previous_Cycle = "" + GlobalTargetList[CurrentTarget.TrackNumber].ModeC;
                    GlobalTargetList[CurrentTarget.TrackNumber].ModeC = CurrentTarget.ModeC;
                    if (CurrentTarget.GSPD != null)
                        GlobalTargetList[CurrentTarget.TrackNumber].GSPD = CurrentTarget.GSPD;
                    GlobalTargetList[CurrentTarget.TrackNumber].ACID_Mode_S = CurrentTarget.ACID_Mode_S;
                    if (CurrentTarget.M_HDG != "N/A")
                        GlobalTargetList[CurrentTarget.TrackNumber].M_HDG = CurrentTarget.M_HDG;
                    if (CurrentTarget.IAS != "N/A")
                        GlobalTargetList[CurrentTarget.TrackNumber].IAS = CurrentTarget.IAS;
                    if (CurrentTarget.TRK != "N/A")
                        GlobalTargetList[CurrentTarget.TrackNumber].TRK = CurrentTarget.TRK;
                    if (CurrentTarget.MACH != "N/A")
                        GlobalTargetList[CurrentTarget.TrackNumber].MACH = CurrentTarget.MACH;
                    if (CurrentTarget.TAS != "N/A")
                        GlobalTargetList[CurrentTarget.TrackNumber].TAS = CurrentTarget.TAS;
                    if (CurrentTarget.Roll_Ang != "N/A")
                        GlobalTargetList[CurrentTarget.TrackNumber].Roll_Ang = CurrentTarget.Roll_Ang;
                    if (CurrentTarget.SelectedAltitude_ShortTerm != "N/A")
                        GlobalTargetList[CurrentTarget.TrackNumber].SelectedAltitude_ShortTerm = CurrentTarget.SelectedAltitude_ShortTerm;
                    if (CurrentTarget.SelectedAltitude_LongTerm != "N/A")
                        GlobalTargetList[CurrentTarget.TrackNumber].SelectedAltitude_LongTerm = CurrentTarget.SelectedAltitude_LongTerm;
                    if (CurrentTarget.Rate_Of_Climb != "N/A")
                        GlobalTargetList[CurrentTarget.TrackNumber].Rate_Of_Climb = CurrentTarget.Rate_Of_Climb;
                    if (CurrentTarget.Barometric_Setting != "N/A")
                        GlobalTargetList[CurrentTarget.TrackNumber].Barometric_Setting = CurrentTarget.Barometric_Setting;
                    GlobalTargetList[CurrentTarget.TrackNumber].Lat = CurrentTarget.Lat;
                    GlobalTargetList[CurrentTarget.TrackNumber].Lon = CurrentTarget.Lon;
                    if (GlobalTargetList[CurrentTarget.TrackNumber].MyMarker.HistoryPoints.Count > Properties.Settings.Default.HistoryPoints)
                        GlobalTargetList[CurrentTarget.TrackNumber].MyMarker.HistoryPoints.Dequeue();
                    GlobalTargetList[CurrentTarget.TrackNumber].MyMarker.HistoryPoints.Enqueue(new PointLatLng(CurrentTarget.Lat, CurrentTarget.Lon));
                    GlobalTargetList[CurrentTarget.TrackNumber].TrackNumber = CurrentTarget.TrackNumber;
                    GlobalTargetList[CurrentTarget.TrackNumber].TrackTerminateTreshold = CurrentTarget.TrackTerminateTreshold;
                }
                else
                {
                    int ModeAIndex = int.Parse(CurrentTarget.ModeA.ToString());
                    GlobalTargetList[ModeAIndex].ModeA = CurrentTarget.ModeA;
                    GlobalTargetList[ModeAIndex].ModeC_Previous_Cycle = "";
                    if (GlobalTargetList[ModeAIndex].ModeC != null)
                        GlobalTargetList[ModeAIndex].ModeC_Previous_Cycle = "" + GlobalTargetList[ModeAIndex].ModeC;
                    GlobalTargetList[ModeAIndex].ModeC = CurrentTarget.ModeC;
                    if (CurrentTarget.GSPD != null)
                        GlobalTargetList[ModeAIndex].GSPD = CurrentTarget.GSPD;
                    GlobalTargetList[ModeAIndex].ACID_Mode_S = CurrentTarget.ACID_Mode_S;
                    if (CurrentTarget.M_HDG != "N/A")
                        GlobalTargetList[ModeAIndex].M_HDG = CurrentTarget.M_HDG;
                    if (CurrentTarget.IAS != "N/A")
                        GlobalTargetList[ModeAIndex].IAS = CurrentTarget.IAS;
                    if (CurrentTarget.TRK != "N/A")
                        GlobalTargetList[ModeAIndex].TRK = CurrentTarget.TRK;
                    if (CurrentTarget.MACH != "N/A")
                        GlobalTargetList[ModeAIndex].MACH = CurrentTarget.MACH;
                    if (CurrentTarget.TAS != "N/A")
                        GlobalTargetList[ModeAIndex].TAS = CurrentTarget.TAS;
                    if (CurrentTarget.Roll_Ang != "N/A")
                        GlobalTargetList[ModeAIndex].Roll_Ang = CurrentTarget.Roll_Ang;
                    if (CurrentTarget.SelectedAltitude_ShortTerm != "N/A")
                        GlobalTargetList[ModeAIndex].SelectedAltitude_ShortTerm = CurrentTarget.SelectedAltitude_ShortTerm;
                    if (CurrentTarget.SelectedAltitude_LongTerm != "N/A")
                        GlobalTargetList[ModeAIndex].SelectedAltitude_LongTerm = CurrentTarget.SelectedAltitude_LongTerm;
                    if (CurrentTarget.Rate_Of_Climb != "N/A")
                        GlobalTargetList[ModeAIndex].Rate_Of_Climb = CurrentTarget.Rate_Of_Climb;
                    if (CurrentTarget.Barometric_Setting != "N/A")
                        GlobalTargetList[ModeAIndex].Barometric_Setting = CurrentTarget.Barometric_Setting;
                    GlobalTargetList[ModeAIndex].Lat = CurrentTarget.Lat;
                    GlobalTargetList[ModeAIndex].Lon = CurrentTarget.Lon;
                    if (GlobalTargetList[ModeAIndex].MyMarker.HistoryPoints.Count > Properties.Settings.Default.HistoryPoints)
                        GlobalTargetList[ModeAIndex].MyMarker.HistoryPoints.Dequeue();
                    GlobalTargetList[ModeAIndex].MyMarker.HistoryPoints.Enqueue(new PointLatLng(CurrentTarget.Lat, CurrentTarget.Lon));
                    GlobalTargetList[ModeAIndex].TrackNumber = ModeAIndex;
                    GlobalTargetList[ModeAIndex].TrackTerminateTreshold = CurrentTarget.TrackTerminateTreshold;
                }
            }

            CurrentTargetList.Clear();
            foreach (TargetType GlobalTarget in GlobalTargetList)
            {
                if (GlobalTarget.TrackTerminateTreshold < Properties.Settings.Default.TrackCoast)
                {
                    TargetType NewTarget = new TargetType();
                    GlobalTarget.TrackTerminateTreshold++;
                    NewTarget.ModeA = GlobalTarget.ModeA;
                    NewTarget.ModeC_Previous_Cycle = GlobalTarget.ModeC_Previous_Cycle;
                    NewTarget.ModeC = GlobalTarget.ModeC;
                    NewTarget.GSPD = GlobalTarget.GSPD;
                    NewTarget.ACID_Mode_S = GlobalTarget.ACID_Mode_S;
                    NewTarget.TRK = GlobalTarget.TRK;
                    NewTarget.TAS = GlobalTarget.TAS;
                    NewTarget.Roll_Ang = GlobalTarget.Roll_Ang;
                    NewTarget.SelectedAltitude_ShortTerm = GlobalTarget.SelectedAltitude_ShortTerm;
                    NewTarget.SelectedAltitude_LongTerm = GlobalTarget.SelectedAltitude_LongTerm;
                    NewTarget.Rate_Of_Climb = GlobalTarget.Rate_Of_Climb;
                    NewTarget.MACH = GlobalTarget.MACH;
                    NewTarget.M_HDG = GlobalTarget.M_HDG;
                    NewTarget.IAS = GlobalTarget.IAS;
                    NewTarget.Barometric_Setting = GlobalTarget.Barometric_Setting;
                    NewTarget.Lat = GlobalTarget.Lat;
                    NewTarget.Lon = GlobalTarget.Lon;
                    NewTarget.TrackNumber = GlobalTarget.TrackNumber;
                    NewTarget.TrackTerminateTreshold = GlobalTarget.TrackTerminateTreshold;
                    NewTarget.MyMarker = GlobalTarget.MyMarker;
                    CurrentTargetList.Add(NewTarget);
                }
                else
                {
                    if (GlobalTarget.MyMarker != null)
                        GlobalTarget.MyMarker.TerminateTarget();
                }
            }

            if (Properties.Settings.Default.DisplayPSR == true)
            {
                // Now append all the PSR tracks to the end of the display list
                foreach (TargetType PSRTgtList in PSRTargetList)
                {
                    TargetType NewTarget = new TargetType();
                    NewTarget.ModeC_Previous_Cycle = PSRTgtList.ModeC_Previous_Cycle;
                    NewTarget.Lat = PSRTgtList.Lat;
                    NewTarget.Lon = PSRTgtList.Lon;
                    NewTarget.TrackNumber = PSRTgtList.TrackNumber;
                    NewTarget.TrackTerminateTreshold = 0;
                    NewTarget.MyMarker = PSRTgtList.MyMarker;
                    CurrentTargetList.Add(NewTarget);
                }
            }
        }

        // Each time this method is called it will extract the targets recived since the
        // the method was last called. It returns a list of the targets in a user friendly
        // format (The TargetType)
        public static void GetDisplayData(bool Return_Buffered, out System.Collections.Generic.List<TargetType> TargetList)
        {
            // First remove all the previous data
            CurrentTargetList.Clear();
            PSRTargetList.Clear();

            if (Return_Buffered == true)
            {
                if (MainASTERIXDataStorage.CAT01Message.Count > 0)
                {
                    for (int Start_Idx = 0; Start_Idx < MainASTERIXDataStorage.CAT01Message.Count; Start_Idx++)
                    {
                        MainASTERIXDataStorage.CAT01Data Msg = MainASTERIXDataStorage.CAT01Message[Start_Idx];

                        // Get Target Descriptor
                        CAT01I020UserData MyCAT01I020UserData = (CAT01I020UserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("020")].value;
                        // Get Mode3A
                        CAT01I070Types.CAT01070Mode3UserData Mode3AData = (CAT01I070Types.CAT01070Mode3UserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("070")].value;
                        // Get Lat/Long
                        CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates LatLongData = (CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates)Msg.CAT01DataItems[CAT01.ItemIDToIndex("040")].value;
                        // Get Flight Level
                        CAT01I090Types.CAT01I090FlightLevelUserData FlightLevelData = (CAT01I090Types.CAT01I090FlightLevelUserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("090")].value;

                        TargetType Target = new TargetType();
                        if (MyCAT01I020UserData != null)
                        {
                            if (MyCAT01I020UserData.Type_Of_Radar_Detection == CAT01I020Types.Radar_Detection_Type.Primary)
                            {
                                Target.ModeA = "PSR";
                                Target.ModeC = "";
                                Target.Lat = LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal;
                                Target.Lon = LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal;
                                Target.TrackTerminateTreshold = 0;
                                PSRTargetList.Add(Target);
                            }
                            else if ((MyCAT01I020UserData.Type_Of_Radar_Detection != CAT01I020Types.Radar_Detection_Type.No_Detection) && (MyCAT01I020UserData.Type_Of_Radar_Detection != CAT01I020Types.Radar_Detection_Type.Unknown_Data))
                            {
                                if (Mode3AData != null)
                                {
                                    if (Mode3AData.Code_Validated == CAT01I070Types.Code_Validation_Type.Code_Validated)
                                    {
                                        Target.ModeA = Mode3AData.Mode3A_Code;
                                        if (FlightLevelData != null)
                                            if (FlightLevelData.Code_Validated == CAT01I090Types.Code_Validation_Type.Code_Validated)
                                                Target.ModeC = FlightLevelData.FlightLevel.ToString();
                                            else
                                                Target.ModeC = "---";
                                        else
                                            Target.ModeC = "---";

                                        Target.Lat = LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal;
                                        Target.Lon = LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal;
                                        CurrentTargetList.Add(Target);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (MainASTERIXDataStorage.CAT48Message.Count > 0)
                {
                    for (int Start_Idx = 0; Start_Idx < MainASTERIXDataStorage.CAT48Message.Count; Start_Idx++)
                    {
                        MainASTERIXDataStorage.CAT48Data Msg = MainASTERIXDataStorage.CAT48Message[Start_Idx];

                        // Get Target Descriptor
                        CAT48I020UserData MyCAT48I020UserData = (CAT48I020UserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("020")].value;
                        //
                        CAT48I070Types.CAT48I070Mode3UserData Mode3AData = (CAT48I070Types.CAT48I070Mode3UserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("070")].value;
                        // Get Lat/Long in decimal
                        CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates LatLongData = (CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates)Msg.CAT48DataItems[CAT48.ItemIDToIndex("040")].value;
                        // Get Flight Level
                        CAT48I090Types.CAT48I090FlightLevelUserData FlightLevelData = (CAT48I090Types.CAT48I090FlightLevelUserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("090")].value;
                        // Get ACID data for Mode-S
                        CAT48I240Types.CAT48I240ACID_Data ACID_Mode_S = (CAT48I240Types.CAT48I240ACID_Data)Msg.CAT48DataItems[CAT48.ItemIDToIndex("240")].value;
                        // Get Mode-S MB Data
                        CAT48I250Types.CAT48I250DataType CAT48I250Mode_S_MB = (CAT48I250Types.CAT48I250DataType)Msg.CAT48DataItems[CAT48.ItemIDToIndex("250")].value;

                        TargetType Target = new TargetType();

                        if (MyCAT48I020UserData != null)
                        {
                            if ((MyCAT48I020UserData.Type_Of_Report == CAT48I020Types.Type_Of_Report_Type.Single_PSR) || (MyCAT48I020UserData.Type_Of_Report == CAT48I020Types.Type_Of_Report_Type.Mode_S_Roll_Call_PSR))
                            {
                                Target.ModeA = "PSR";
                                Target.ModeC = "";
                                Target.ACID_Mode_S = "";
                                Target.Lat = LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal;
                                Target.Lon = LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal;
                                Target.TrackTerminateTreshold = 0;
                                PSRTargetList.Add(Target);
                            }
                            else if ((MyCAT48I020UserData.Type_Of_Report != CAT48I020Types.Type_Of_Report_Type.No_Detection) &&
                            (MyCAT48I020UserData.Type_Of_Report != CAT48I020Types.Type_Of_Report_Type.Unknown_Data))
                            {
                                if (Mode3AData != null)
                                {
                                    if (Mode3AData.Code_Validated == CAT48I070Types.Code_Validation_Type.Code_Validated)
                                    {
                                        Target.ModeA = Mode3AData.Mode3A_Code;

                                        if (FlightLevelData != null)
                                        {
                                            try
                                            {
                                                if (FlightLevelData.Code_Validated == CAT48I090Types.Code_Validation_Type.Code_Validated)
                                                    Target.ModeC = FlightLevelData.FlightLevel.ToString();
                                                else
                                                    Target.ModeC = "---";
                                            }
                                            catch
                                            {
                                                MessageBox.Show("Dynamic Display Builder, CAT48, Flight Level");
                                            }
                                        }
                                        else
                                            Target.ModeC = "---";

                                        if (ACID_Mode_S != null)
                                            Target.ACID_Mode_S = ACID_Mode_S.ACID;
                                        else
                                            Target.ACID_Mode_S = "N/A";

                                        Target.Lat = LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal;
                                        Target.Lon = LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal;

                                        if (CAT48I250Mode_S_MB != null)
                                        {
                                            if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.Present_This_Cycle)
                                            {
                                                if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.MAG_HDG.Is_Valid)
                                                    Target.M_HDG = Math.Round(CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.MAG_HDG.Value).ToString();
                                                else
                                                    Target.M_HDG = "N/A";

                                                if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.MACH.Is_Valid)
                                                    Target.MACH = Math.Round(CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.MACH.Value, 3).ToString();
                                                else
                                                    Target.MACH = "N/A";

                                                if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.IAS.Is_Valid)
                                                    Target.IAS = CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.IAS.Value.ToString();
                                                else
                                                    Target.IAS = "N/A";

                                                if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.Inertial_RoC.Is_Valid)
                                                    Target.Rate_Of_Climb = "I:" + CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.Inertial_RoC.Value.ToString();
                                                else
                                                    Target.Rate_Of_Climb = "I:N/A";

                                                if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.Baro_RoC.Is_Valid)
                                                    Target.Rate_Of_Climb = Target.Rate_Of_Climb + "/" + "B:" + CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.Baro_RoC.Value.ToString();
                                                else
                                                    Target.Rate_Of_Climb = Target.Rate_Of_Climb + "/" + "B:N/A";
                                            }

                                            if (CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.Present_This_Cycle)
                                            {

                                                if (CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.Roll_Ang.Is_Valid)
                                                    Target.Roll_Ang = Math.Round(CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.Roll_Ang.Value).ToString();
                                                else
                                                    Target.Roll_Ang = "N/A";

                                                if (CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.TAS.Is_Valid)
                                                    Target.TAS = CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.TAS.Value.ToString();
                                                else
                                                    Target.TAS = "N/A";

                                                if (CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.GND_SPD.Is_Valid)
                                                    Target.GSPD = Math.Round(CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.GND_SPD.Value).ToString();
                                                else
                                                    Target.GSPD = "N/A";

                                                if (CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.TRUE_TRK.Is_Valid)
                                                    Target.TRK = Math.Round(CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.TRUE_TRK.Value).ToString();
                                                else
                                                    Target.TRK = "N/A";
                                            }

                                            if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Present_This_Cycle)
                                            {
                                                switch (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Status_Data.Target_Altitude_Source)
                                                {
                                                    case CAT48I250Types.BDS40_Selected_Vertical_Intention_Report.Status.Target_Altitude_Mode_Type.Aircraft_Alt:
                                                        Target.SelectedAltitude_ShortTerm = "A/C:" + Target.ModeC;
                                                        break;
                                                    case CAT48I250Types.BDS40_Selected_Vertical_Intention_Report.Status.Target_Altitude_Mode_Type.FCU_MCP_Selected_Alt:
                                                        if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.MCP_FCU_Sel_ALT.Is_Valid)
                                                            Target.SelectedAltitude_ShortTerm = "MCP:" + CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.MCP_FCU_Sel_ALT.Value.ToString();
                                                        else
                                                            Target.SelectedAltitude_ShortTerm = "MCP:N/A";
                                                        break;
                                                    case CAT48I250Types.BDS40_Selected_Vertical_Intention_Report.Status.Target_Altitude_Mode_Type.FMS_Selected_Alt:
                                                        if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.FMS_Sel_ALT.Is_Valid)
                                                            Target.SelectedAltitude_ShortTerm = "FMS:" + CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.FMS_Sel_ALT.Value.ToString();
                                                        else
                                                            Target.SelectedAltitude_ShortTerm = "FMS:N/A";
                                                        break;
                                                    case CAT48I250Types.BDS40_Selected_Vertical_Intention_Report.Status.Target_Altitude_Mode_Type.Unknown:
                                                        if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.MCP_FCU_Sel_ALT.Is_Valid)
                                                            Target.SelectedAltitude_ShortTerm = "UNK:" + CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.FMS_Sel_ALT.Value.ToString();
                                                        else
                                                            Target.SelectedAltitude_ShortTerm = "N/A";
                                                        break;
                                                }


                                                if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.MCP_FCU_Sel_ALT.Is_Valid)
                                                {
                                                    if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Status_Data.MCP_FCU_Mode_Bits_Populated)
                                                    {
                                                        Target.SelectedAltitude_LongTerm = "";
                                                        if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Status_Data.ALT_Hold_Active)
                                                            Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + "AH:";
                                                        if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Status_Data.APP_Mode_Active)
                                                            Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + "AM:";
                                                        if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Status_Data.VNAV_Mode_Active)
                                                            Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + "MV:";
                                                    }
                                                    Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.MCP_FCU_Sel_ALT.Value.ToString();
                                                }
                                                else
                                                    Target.SelectedAltitude_LongTerm = "N/A";

                                                if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Baro_Sel_ALT.Is_Valid)
                                                    Target.Barometric_Setting = Math.Round(CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Baro_Sel_ALT.Value, 1).ToString() + "mb";
                                                else
                                                    Target.Barometric_Setting = "N/A";
                                            }

                                        }

                                        CurrentTargetList.Add(Target);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (MainASTERIXDataStorage.CAT62Message.Count > 0)
                {

                    for (int Start_Idx = 0; Start_Idx < MainASTERIXDataStorage.CAT62Message.Count; Start_Idx++)
                    {
                        MainASTERIXDataStorage.CAT62Data Msg = MainASTERIXDataStorage.CAT62Message[Start_Idx];
                        CAT62I060Types.CAT62060Mode3UserData Mode3AData = (CAT62I060Types.CAT62060Mode3UserData)Msg.CAT62DataItems[CAT62.ItemIDToIndex("060")].value;
                        GeoCordSystemDegMinSecUtilities.LatLongClass LatLongData = (GeoCordSystemDegMinSecUtilities.LatLongClass)Msg.CAT62DataItems[CAT62.ItemIDToIndex("105")].value;
                        CAT62I380Types.CAT62I380Data CAT62I380Data = (CAT62I380Types.CAT62I380Data)Msg.CAT62DataItems[CAT62.ItemIDToIndex("380")].value;
                        CAT62I220Types.CalculatedRateOfClimbDescent CAT62I220Data = (CAT62I220Types.CalculatedRateOfClimbDescent)Msg.CAT62DataItems[CAT62.ItemIDToIndex("220")].value;

                        TargetType Target = new TargetType();

                        if (Mode3AData != null)
                        {
                            Target.ModeA = Mode3AData.Mode3A_Code;

                            if (Msg.CAT62DataItems[CAT62.ItemIDToIndex("136")].value != null)
                            {
                                try
                                {
                                    double FlightLevel = (double)Msg.CAT62DataItems[CAT62.ItemIDToIndex("136")].value;
                                    Target.ModeC = FlightLevel.ToString();
                                }
                                catch
                                {
                                    Target.ModeC = "---";
                                }
                            }
                            else
                            {
                                Target.ModeC = "---";
                            }

                            if (CAT62I380Data != null)
                            {
                                if (CAT62I380Data.ACID.Is_Valid)
                                    Target.ACID_Mode_S = CAT62I380Data.ACID.ACID_String;
                                else
                                    Target.ACID_Mode_S = "N/A";

                                if (CAT62I380Data.TAS.Is_Valid)
                                    Target.TAS = CAT62I380Data.TAS.TAS.ToString();
                                else
                                    Target.TAS = "N/A";

                                if (CAT62I380Data.IAS.Is_Valid)
                                    Target.IAS = CAT62I380Data.IAS.IAS.ToString();
                                else
                                    Target.IAS = "N/A";

                                if (CAT62I380Data.MACH.Is_Valid)
                                    Target.MACH = Math.Round(CAT62I380Data.MACH.MACH, 3).ToString();
                                else
                                    Target.MACH = "N/A";

                                if (CAT62I380Data.M_HDG.Is_Valid)
                                    Target.M_HDG = Math.Round(CAT62I380Data.M_HDG.M_HDG).ToString();
                                else
                                    Target.M_HDG = "N/A";

                                if (CAT62I380Data.TRK.Is_Valid)
                                    Target.TRK = Math.Round(CAT62I380Data.TRK.TRK).ToString();
                                else
                                    Target.TRK = "N/A";

                                if (CAT62I380Data.GSPD.Is_Valid)
                                    Target.GSPD = Math.Round(CAT62I380Data.GSPD.GSPD).ToString();
                                else
                                    Target.GSPD = "N/A";

                                if (CAT62I380Data.Rool_Angle.Is_Valid)
                                    Target.Roll_Ang = Math.Round(CAT62I380Data.Rool_Angle.Rool_Angle, 1).ToString();
                                else
                                    Target.Roll_Ang = "N/A";

                                if (CAT62I380Data.FS_Selected_Altitude.Is_Valid)
                                {
                                    Target.SelectedAltitude_LongTerm = "";
                                    if (CAT62I380Data.FS_Selected_Altitude.Altitude_Hold_Active)
                                        Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + "AH:";
                                    if (CAT62I380Data.FS_Selected_Altitude.Approach_Mode_Active)
                                        Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + "AM:";
                                    if (CAT62I380Data.FS_Selected_Altitude.Manage_Mode_Active)
                                        Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + "MV:";

                                    Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + CAT62I380Data.FS_Selected_Altitude.SelectedAltitude.ToString();
                                }
                                else
                                    Target.SelectedAltitude_LongTerm = "N/A";

                                if (CAT62I380Data.Selected_Altitude.Is_Valid)
                                {
                                    Target.SelectedAltitude_ShortTerm = "";
                                    switch (CAT62I380Data.Selected_Altitude.Source)
                                    {
                                        case CAT62I380Types.CAT62SelectedAltitudeType.SourceType.AircraftAltitude:
                                            Target.SelectedAltitude_ShortTerm = Target.SelectedAltitude_ShortTerm + "A/C:";
                                            break;
                                        case CAT62I380Types.CAT62SelectedAltitudeType.SourceType.FCU_MCP:
                                            Target.SelectedAltitude_ShortTerm = Target.SelectedAltitude_ShortTerm + "MCP:";
                                            break;
                                        case CAT62I380Types.CAT62SelectedAltitudeType.SourceType.FMS_Selected:
                                            Target.SelectedAltitude_ShortTerm = Target.SelectedAltitude_ShortTerm + "FMS:";
                                            break;
                                        case CAT62I380Types.CAT62SelectedAltitudeType.SourceType.Unknown:
                                            Target.SelectedAltitude_ShortTerm = Target.SelectedAltitude_ShortTerm + "UKN:";
                                            break;
                                    }
                                    Target.SelectedAltitude_ShortTerm = Target.SelectedAltitude_ShortTerm + CAT62I380Data.Selected_Altitude.SelectedAltitude.ToString();
                                }
                                else
                                    Target.SelectedAltitude_ShortTerm = "N/A";

                                if (CAT62I380Data.Baro_Press_Setting.Is_Valid)
                                    Target.Barometric_Setting = Math.Round(CAT62I380Data.Baro_Press_Setting.Baro_Pressure_Setting, 1).ToString() + "mb";
                                else
                                    Target.Barometric_Setting = "N/A";
                            }

                            if (CAT62I220Data != null)
                            {
                                if (CAT62I220Data.Is_Valid == true)
                                    Target.Rate_Of_Climb = CAT62I220Data.Value.ToString();
                                else
                                    Target.Rate_Of_Climb = "N/A";
                            }
                            else
                            {
                                Target.Rate_Of_Climb = "N/A";
                            }

                            Target.Lat = LatLongData.GetLatLongDecimal().LatitudeDecimal;
                            Target.Lon = LatLongData.GetLatLongDecimal().LongitudeDecimal;
                            CurrentTargetList.Add(Target);
                        }
                    }
                }
            }
            else
            {
                if (MainASTERIXDataStorage.CAT01Message.Count > 0)
                {
                    for (int Start_Idx = LastDataIndex; Start_Idx < MainASTERIXDataStorage.CAT01Message.Count; Start_Idx++)
                    {
                        LastDataIndex++;
                        MainASTERIXDataStorage.CAT01Data Msg = MainASTERIXDataStorage.CAT01Message[Start_Idx];

                        // Get Target Descriptor
                        CAT01I020UserData MyCAT01I020UserData = (CAT01I020UserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("020")].value;
                        // Get Mode3A
                        CAT01I070Types.CAT01070Mode3UserData Mode3AData = (CAT01I070Types.CAT01070Mode3UserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("070")].value;
                        // Get Lat/Long
                        CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates LatLongData = (CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates)Msg.CAT01DataItems[CAT01.ItemIDToIndex("040")].value;
                        // Get Flight Level
                        CAT01I090Types.CAT01I090FlightLevelUserData FlightLevelData = (CAT01I090Types.CAT01I090FlightLevelUserData)Msg.CAT01DataItems[CAT01.ItemIDToIndex("090")].value;

                        TargetType Target = new TargetType();
                        if (MyCAT01I020UserData != null)
                        {
                            if (MyCAT01I020UserData.Type_Of_Radar_Detection == CAT01I020Types.Radar_Detection_Type.Primary)
                            {
                                Target.ModeA = "PSR";
                                Target.ModeC = "";
                                Target.Lat = LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal;
                                Target.Lon = LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal;
                                Target.MyMarker.Position = new PointLatLng(Target.Lat, Target.Lon);
                                PSRTargetList.Add(Target);
                            }
                            else if ((MyCAT01I020UserData.Type_Of_Radar_Detection != CAT01I020Types.Radar_Detection_Type.No_Detection) && (MyCAT01I020UserData.Type_Of_Radar_Detection != CAT01I020Types.Radar_Detection_Type.Unknown_Data))
                            {

                                if (Mode3AData != null)
                                {
                                    if (Mode3AData.Code_Validated == CAT01I070Types.Code_Validation_Type.Code_Validated)
                                    {
                                        Target.ModeA = Mode3AData.Mode3A_Code;

                                        if (FlightLevelData != null)
                                            if (FlightLevelData.Code_Validated == CAT01I090Types.Code_Validation_Type.Code_Validated)
                                                Target.ModeC = FlightLevelData.FlightLevel.ToString();
                                            else
                                                Target.ModeC = "---";
                                        else
                                            Target.ModeC = "---";

                                        Target.Lat = LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal;
                                        Target.Lon = LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal;
                                        CurrentTargetList.Add(Target);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (MainASTERIXDataStorage.CAT48Message.Count > 0)
                {
                    for (int Start_Idx = LastDataIndex; Start_Idx < MainASTERIXDataStorage.CAT48Message.Count; Start_Idx++)
                    {
                        LastDataIndex++;

                        MainASTERIXDataStorage.CAT48Data Msg = MainASTERIXDataStorage.CAT48Message[Start_Idx];

                        CAT48I020UserData MyCAT48I020UserData = (CAT48I020UserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("020")].value;
                        CAT48I070Types.CAT48I070Mode3UserData Mode3AData = (CAT48I070Types.CAT48I070Mode3UserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("070")].value;
                        // Get Lat/Long in decimal
                        CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates LatLongData = (CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates)Msg.CAT48DataItems[CAT48.ItemIDToIndex("040")].value;
                        // Get Flight Level
                        CAT48I090Types.CAT48I090FlightLevelUserData FlightLevelData = (CAT48I090Types.CAT48I090FlightLevelUserData)Msg.CAT48DataItems[CAT48.ItemIDToIndex("090")].value;
                        // Get ACID data for Mode-S
                        CAT48I240Types.CAT48I240ACID_Data ACID_Mode_S = (CAT48I240Types.CAT48I240ACID_Data)Msg.CAT48DataItems[CAT48.ItemIDToIndex("240")].value;
                        // Get Mode-S MB Data
                        CAT48I250Types.CAT48I250DataType CAT48I250Mode_S_MB = (CAT48I250Types.CAT48I250DataType)Msg.CAT48DataItems[CAT48.ItemIDToIndex("250")].value;

                        TargetType Target = new TargetType();
                        if (MyCAT48I020UserData != null)
                        {
                            if ((MyCAT48I020UserData.Type_Of_Report == CAT48I020Types.Type_Of_Report_Type.Single_PSR) ||
                                (MyCAT48I020UserData.Type_Of_Report == CAT48I020Types.Type_Of_Report_Type.Mode_S_Roll_Call_PSR))
                            {
                                Target.ModeA = "PSR";
                                Target.ModeC = "";
                                Target.ACID_Mode_S = "";
                                Target.Lat = LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal;
                                Target.Lon = LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal;
                                PSRTargetList.Add(Target);
                            }
                            else if ((MyCAT48I020UserData.Type_Of_Report != CAT48I020Types.Type_Of_Report_Type.No_Detection) &&
                             (MyCAT48I020UserData.Type_Of_Report != CAT48I020Types.Type_Of_Report_Type.Unknown_Data))
                            {

                                if (Mode3AData != null)
                                {
                                    if (Mode3AData.Code_Validated == CAT48I070Types.Code_Validation_Type.Code_Validated)
                                    {
                                        Target.ModeA = Mode3AData.Mode3A_Code;

                                        if (FlightLevelData != null)
                                        {
                                            if (FlightLevelData.Code_Validated == CAT48I090Types.Code_Validation_Type.Code_Validated)
                                                Target.ModeC = FlightLevelData.FlightLevel.ToString();
                                            else
                                                Target.ModeC = "---";
                                        }
                                        else
                                            Target.ModeC = "---";

                                        if (ACID_Mode_S != null)
                                        {
                                            Target.ACID_Mode_S = ACID_Mode_S.ACID;
                                        }
                                        else
                                        {
                                            Target.ACID_Mode_S = "N/A";
                                        }
                                        Target.Lat = LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal;
                                        Target.Lon = LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal;
                                        if (CAT48I250Mode_S_MB != null)
                                        {
                                            if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.Present_This_Cycle)
                                            {
                                                if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.MAG_HDG.Is_Valid)
                                                    Target.M_HDG = Math.Round(CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.MAG_HDG.Value).ToString();
                                                else
                                                    Target.M_HDG = "N/A";

                                                if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.MACH.Is_Valid)
                                                    Target.MACH = Math.Round(CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.MACH.Value, 3).ToString();
                                                else
                                                    Target.MACH = "N/A";

                                                if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.IAS.Is_Valid)
                                                    Target.IAS = CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.IAS.Value.ToString();
                                                else
                                                    Target.IAS = "N/A";

                                                if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.Inertial_RoC.Is_Valid)
                                                    Target.Rate_Of_Climb = "I:" + CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.Inertial_RoC.Value.ToString();
                                                else
                                                    Target.Rate_Of_Climb = "I:N/A";

                                                if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.Baro_RoC.Is_Valid)
                                                    Target.Rate_Of_Climb = Target.Rate_Of_Climb + "/" + "B:" + CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.Baro_RoC.Value.ToString();
                                                else
                                                    Target.Rate_Of_Climb = Target.Rate_Of_Climb + "/" + "B:N/A";
                                            }

                                            if (CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.Present_This_Cycle)
                                            {

                                                if (CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.Roll_Ang.Is_Valid)
                                                    Target.Roll_Ang = Math.Round(CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.Roll_Ang.Value).ToString();
                                                else
                                                    Target.Roll_Ang = "N/A";

                                                if (CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.TAS.Is_Valid)
                                                    Target.TAS = CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.TAS.Value.ToString();
                                                else
                                                    Target.TAS = "N/A";

                                                if (CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.GND_SPD.Is_Valid)
                                                    Target.GSPD = Math.Round(CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.GND_SPD.Value).ToString();
                                                else
                                                    Target.GSPD = "N/A";

                                                if (CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.TRUE_TRK.Is_Valid)
                                                    Target.TRK = Math.Round(CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.TRUE_TRK.Value).ToString();
                                                else
                                                    Target.TRK = "N/A";
                                            }

                                            if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Present_This_Cycle)
                                            {
                                                switch (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Status_Data.Target_Altitude_Source)
                                                {
                                                    case CAT48I250Types.BDS40_Selected_Vertical_Intention_Report.Status.Target_Altitude_Mode_Type.Aircraft_Alt:
                                                        Target.SelectedAltitude_ShortTerm = "A/C:" + Target.ModeC;
                                                        break;
                                                    case CAT48I250Types.BDS40_Selected_Vertical_Intention_Report.Status.Target_Altitude_Mode_Type.FCU_MCP_Selected_Alt:
                                                        if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.MCP_FCU_Sel_ALT.Is_Valid)
                                                            Target.SelectedAltitude_ShortTerm = "MCP:" + CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.MCP_FCU_Sel_ALT.Value.ToString();
                                                        else
                                                            Target.SelectedAltitude_ShortTerm = "MCP:N/A";
                                                        break;
                                                    case CAT48I250Types.BDS40_Selected_Vertical_Intention_Report.Status.Target_Altitude_Mode_Type.FMS_Selected_Alt:
                                                        if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.FMS_Sel_ALT.Is_Valid)
                                                            Target.SelectedAltitude_ShortTerm = "FMS:" + CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.FMS_Sel_ALT.Value.ToString();
                                                        else
                                                            Target.SelectedAltitude_ShortTerm = "FMS:N/A";
                                                        break;
                                                    case CAT48I250Types.BDS40_Selected_Vertical_Intention_Report.Status.Target_Altitude_Mode_Type.Unknown:
                                                        if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.MCP_FCU_Sel_ALT.Is_Valid)
                                                            Target.SelectedAltitude_ShortTerm = "UNK:" + CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.MCP_FCU_Sel_ALT.Value.ToString();
                                                        else
                                                            Target.SelectedAltitude_ShortTerm = "N/A";
                                                        break;
                                                }


                                                if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.MCP_FCU_Sel_ALT.Is_Valid)
                                                {
                                                    Target.SelectedAltitude_LongTerm = "";
                                                    if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Status_Data.MCP_FCU_Mode_Bits_Populated)
                                                    {
                                                        if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Status_Data.ALT_Hold_Active)
                                                            Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + "AH:";
                                                        if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Status_Data.APP_Mode_Active)
                                                            Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + "AM:";
                                                        if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Status_Data.VNAV_Mode_Active)
                                                            Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + "MV:";
                                                    }
                                                    Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.MCP_FCU_Sel_ALT.Value.ToString();
                                                }
                                                else
                                                    Target.SelectedAltitude_LongTerm = "N/A";

                                                if (CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Baro_Sel_ALT.Is_Valid)
                                                    Target.Barometric_Setting = Math.Round(CAT48I250Mode_S_MB.BDS40_Selected_Vertical_Intention_Report.Baro_Sel_ALT.Value, 1).ToString() + "mb";
                                                else
                                                    Target.Barometric_Setting = "N/A";
                                            }

                                        }

                                        CurrentTargetList.Add(Target);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (MainASTERIXDataStorage.CAT62Message.Count > 0)
                {

                    for (int Start_Idx = LastDataIndex; Start_Idx < MainASTERIXDataStorage.CAT62Message.Count; Start_Idx++)
                    {
                        LastDataIndex++;

                        MainASTERIXDataStorage.CAT62Data Msg = MainASTERIXDataStorage.CAT62Message[Start_Idx];

                        CAT62I060Types.CAT62060Mode3UserData Mode3AData = (CAT62I060Types.CAT62060Mode3UserData)Msg.CAT62DataItems[CAT62.ItemIDToIndex("060")].value;
                        // Get Lat/Long in decimal
                        GeoCordSystemDegMinSecUtilities.LatLongClass LatLongData = (GeoCordSystemDegMinSecUtilities.LatLongClass)Msg.CAT62DataItems[CAT62.ItemIDToIndex("105")].value;
                        CAT62I220Types.CalculatedRateOfClimbDescent CAT62I220Data = (CAT62I220Types.CalculatedRateOfClimbDescent)Msg.CAT62DataItems[CAT62.ItemIDToIndex("220")].value;
                        CAT62I380Types.CAT62I380Data CAT62I380Data = (CAT62I380Types.CAT62I380Data)Msg.CAT62DataItems[CAT62.ItemIDToIndex("380")].value;

                        TargetType Target = new TargetType();

                        if (Mode3AData != null)
                        {
                            Target.ModeA = Mode3AData.Mode3A_Code;

                            if (Msg.CAT62DataItems[CAT62.ItemIDToIndex("136")].value != null)
                            {
                                try
                                {
                                    double FlightLevel = (double)Msg.CAT62DataItems[CAT62.ItemIDToIndex("136")].value;
                                    Target.ModeC = FlightLevel.ToString();
                                }
                                catch
                                {
                                    Target.ModeC = "---";
                                }
                            }
                            else
                            {
                                Target.ModeC = "---";
                            }

                            if (CAT62I380Data != null)
                            {
                                if (CAT62I380Data.ACID.Is_Valid)
                                    Target.ACID_Mode_S = CAT62I380Data.ACID.ACID_String;
                                else
                                    Target.ACID_Mode_S = "N/A";

                                if (CAT62I380Data.TAS.Is_Valid)
                                    Target.TAS = CAT62I380Data.TAS.TAS.ToString();
                                else
                                    Target.TAS = "N/A";

                                if (CAT62I380Data.IAS.Is_Valid)
                                    Target.IAS = CAT62I380Data.IAS.IAS.ToString();
                                else
                                    Target.IAS = "N/A";

                                if (CAT62I380Data.MACH.Is_Valid)
                                    Target.MACH = Math.Round(CAT62I380Data.MACH.MACH, 3).ToString();
                                else
                                    Target.MACH = "N/A";

                                if (CAT62I380Data.M_HDG.Is_Valid)
                                    Target.M_HDG = Math.Round(CAT62I380Data.M_HDG.M_HDG).ToString();
                                else
                                    Target.M_HDG = "N/A";

                                if (CAT62I380Data.TRK.Is_Valid)
                                    Target.TRK = Math.Round(CAT62I380Data.TRK.TRK).ToString();
                                else
                                    Target.TRK = "N/A";

                                if (CAT62I380Data.GSPD.Is_Valid)
                                    Target.GSPD = Math.Round(CAT62I380Data.GSPD.GSPD).ToString();
                                else
                                    Target.GSPD = "N/A";

                                if (CAT62I380Data.Rool_Angle.Is_Valid)
                                    Target.Roll_Ang = Math.Round(CAT62I380Data.Rool_Angle.Rool_Angle, 1).ToString();
                                else
                                    Target.Roll_Ang = "N/A";

                                if (CAT62I380Data.FS_Selected_Altitude.Is_Valid)
                                {
                                    Target.SelectedAltitude_LongTerm = "";
                                    if (CAT62I380Data.FS_Selected_Altitude.Altitude_Hold_Active)
                                        Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + "AH:";
                                    if (CAT62I380Data.FS_Selected_Altitude.Approach_Mode_Active)
                                        Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + "AM:";
                                    if (CAT62I380Data.FS_Selected_Altitude.Manage_Mode_Active)
                                        Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + "MV:";

                                    Target.SelectedAltitude_LongTerm = Target.SelectedAltitude_LongTerm + CAT62I380Data.FS_Selected_Altitude.SelectedAltitude.ToString();
                                }
                                else
                                    Target.SelectedAltitude_LongTerm = "N/A";

                                if (CAT62I380Data.Selected_Altitude.Is_Valid)
                                {
                                    Target.SelectedAltitude_ShortTerm = "";
                                    switch (CAT62I380Data.Selected_Altitude.Source)
                                    {
                                        case CAT62I380Types.CAT62SelectedAltitudeType.SourceType.AircraftAltitude:
                                            Target.SelectedAltitude_ShortTerm = Target.SelectedAltitude_ShortTerm + "A/C:";
                                            break;
                                        case CAT62I380Types.CAT62SelectedAltitudeType.SourceType.FCU_MCP:
                                            Target.SelectedAltitude_ShortTerm = Target.SelectedAltitude_ShortTerm + "MCP:";
                                            break;
                                        case CAT62I380Types.CAT62SelectedAltitudeType.SourceType.FMS_Selected:
                                            Target.SelectedAltitude_ShortTerm = Target.SelectedAltitude_ShortTerm + "FMS:";
                                            break;
                                        case CAT62I380Types.CAT62SelectedAltitudeType.SourceType.Unknown:
                                            Target.SelectedAltitude_ShortTerm = Target.SelectedAltitude_ShortTerm + "UKN:";
                                            break;
                                    }
                                    Target.SelectedAltitude_ShortTerm = Target.SelectedAltitude_ShortTerm  + CAT62I380Data.Selected_Altitude.SelectedAltitude.ToString();
                                }
                                else
                                    Target.SelectedAltitude_ShortTerm = "N/A";

                                if (CAT62I380Data.Baro_Press_Setting.Is_Valid)
                                    Target.Barometric_Setting = Math.Round(CAT62I380Data.Baro_Press_Setting.Baro_Pressure_Setting, 1).ToString() + "mb";
                                else
                                    Target.Barometric_Setting = "N/A";
                            }

                            if (CAT62I220Data != null)
                            {
                                if (CAT62I220Data.Is_Valid == true)
                                    Target.Rate_Of_Climb = CAT62I220Data.Value.ToString();
                                else
                                    Target.Rate_Of_Climb = "N/A";
                            }
                            else
                            {
                                Target.Rate_Of_Climb = "N/A";
                            }

                            Target.Lat = LatLongData.GetLatLongDecimal().LatitudeDecimal;
                            Target.Lon = LatLongData.GetLatLongDecimal().LongitudeDecimal;

                            CurrentTargetList.Add(Target);
                        }
                    }
                }
            }

            if (Return_Buffered == false)
            {
                UpdateGlobalList();
            }

            TargetList = CurrentTargetList;
        }
    }
}
