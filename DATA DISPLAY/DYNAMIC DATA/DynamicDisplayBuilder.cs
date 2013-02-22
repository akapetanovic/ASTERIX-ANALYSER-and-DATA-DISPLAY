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
            ////////////////////////////////////////////////////
            public double TimeSinceMidnight;

            /// <summary>
            /// ////////////////////////////////////////////////
            /// Track Label Items
            /// </summary>
            public string ModeA;
            public string ModeC;
            public string CALC_GSPD;
            public string CALC_HDG;
            public string ModeC_Previous_Cycle;
            public string ACID_Mode_S;
            public double Lat;
            public double Lon;
            /// <summary>
            /// ////////////////////////////////////////////////////
            /// Extended label items, Applicable to CAT48 and CAT62
            /// </summary>
            public string Mode_S_Addr = "N/A";
            public string TAS = "N/A";
            public string IAS = "N/A";
            public string MACH = "N/A";
            public string DAP_HDG = "N/A";
            public string DAP_GSPD = "N/A";
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
            // STCA parameters
            // Holds targets this target is in STCA conflict with
            public System.Collections.Generic.List<STCA_Target_Item> STCA_List = new List<STCA_Target_Item>();
        }

        // Keeps track of the data index from the last update of the 
        // display. Used in order to be able to extract only targets recived
        // since the last data update. 
        private static int LastDataIndex = 0;

        // Max number of history points
        private static int Max_History_Points = 10;

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

        public static string GetTarget_CALC_GSPD_ByIndex(int Index)
        {
            return GlobalTargetList[Index].MyMarker.CALC_GSPD_STRING;
        }

        public static string GetTarget_DAP_GSPD_ByIndex(int Index)
        {
            return GlobalTargetList[Index].MyMarker.DAP_GSPD;
        }

        public static string GetTargetM_HDGByIndex(int Index)
        {
            return GlobalTargetList[Index].MyMarker.DAP_HDG;
        }

        public static string GetTargetTRKByIndex(int Index)
        {
            return GlobalTargetList[Index].MyMarker.TRK;
        }

        public static string GetTarget_CALC_HDG_ByIndex(int Index)
        {
            return GlobalTargetList[Index].MyMarker.CALC_HDG_STRING;
        }

        public static string GetTargetMode_C_ByIndex(int Index)
        {
            return GlobalTargetList[Index].MyMarker.ModeC_STRING;
        }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////
        /// DO NOT CHANGE THE ORDER OF CALLS BELOW !!!
        /// 
        /// </summary>
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
                    GlobalTargetList[CurrentTarget.TrackNumber].CALC_GSPD = CurrentTarget.CALC_GSPD;
                    if (CurrentTarget.DAP_GSPD != "N/A")
                        GlobalTargetList[CurrentTarget.TrackNumber].DAP_GSPD = CurrentTarget.DAP_GSPD;
                    GlobalTargetList[CurrentTarget.TrackNumber].ACID_Mode_S = CurrentTarget.ACID_Mode_S;
                    if (CurrentTarget.Mode_S_Addr != "N/A")
                        GlobalTargetList[CurrentTarget.TrackNumber].Mode_S_Addr = CurrentTarget.Mode_S_Addr;
                    if (CurrentTarget.DAP_HDG != "N/A")
                        GlobalTargetList[CurrentTarget.TrackNumber].DAP_HDG = CurrentTarget.DAP_HDG;
                    GlobalTargetList[CurrentTarget.TrackNumber].CALC_HDG = CurrentTarget.CALC_HDG;
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
                    GlobalTargetList[CurrentTarget.TrackNumber].TimeSinceMidnight = CurrentTarget.TimeSinceMidnight;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    // Handle history points
                    if (GlobalTargetList[CurrentTarget.TrackNumber].MyMarker.HistoryPoints.Count > 0)
                    {
                        // select a reference elllipsoid
                        Ellipsoid reference = Ellipsoid.WGS84;
                        // instantiate the calculator
                        GeodeticCalculator geoCalc = new GeodeticCalculator();
                        GlobalPosition Track_1 = new GlobalPosition(new GlobalCoordinates(CurrentTarget.Lat, CurrentTarget.Lon));
                        GlobalPosition Track_2 = new GlobalPosition(new GlobalCoordinates(GlobalTargetList[CurrentTarget.TrackNumber].MyMarker.HistoryPoints.Last().LatLong.Lat,
                            GlobalTargetList[CurrentTarget.TrackNumber].MyMarker.HistoryPoints.Last().LatLong.Lng));

                        // Calculate distance traveled
                        double DistanceTraveled = geoCalc.CalculateGeodeticMeasurement(reference, Track_1, Track_2).PointToPointDistance;
                        DistanceTraveled = DistanceTraveled * 0.00053996; // Convert to nautical miles
                        double BetweenTwoUpdates = CurrentTarget.TimeSinceMidnight - GlobalTargetList[CurrentTarget.TrackNumber].MyMarker.HistoryPoints.Last().TimeSinceMidnight;

                        int Miliseconds = (int)(((BetweenTwoUpdates - Math.Floor(BetweenTwoUpdates)) * 10.0));
                        TimeSpan TimeDifference = new TimeSpan(0, 0, 0, (int)Math.Floor(BetweenTwoUpdates), Miliseconds);

                        // Only update history position if there was actually a change in the distance
                        if (DistanceTraveled > 0)
                        {
                            if (GlobalTargetList[CurrentTarget.TrackNumber].MyMarker.HistoryPoints.Count > Max_History_Points)
                                GlobalTargetList[CurrentTarget.TrackNumber].MyMarker.HistoryPoints.Dequeue();
                            GMapTargetandLabel.HistoryPointsType HP = new GMapTargetandLabel.HistoryPointsType();
                            HP.LatLong = new PointLatLng(CurrentTarget.Lat, CurrentTarget.Lon);
                            HP.TimeSinceMidnight = CurrentTarget.TimeSinceMidnight;
                            GlobalTargetList[CurrentTarget.TrackNumber].MyMarker.HistoryPoints.Enqueue(HP);
                        }
                    }
                    else
                    {
                        GMapTargetandLabel.HistoryPointsType HP = new GMapTargetandLabel.HistoryPointsType();
                        HP.LatLong = new PointLatLng(CurrentTarget.Lat, CurrentTarget.Lon);
                        HP.TimeSinceMidnight = CurrentTarget.TimeSinceMidnight;
                        GlobalTargetList[CurrentTarget.TrackNumber].MyMarker.HistoryPoints.Enqueue(HP);
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
                    if (CurrentTarget.DAP_GSPD != "N/A")
                        GlobalTargetList[ModeAIndex].DAP_GSPD = CurrentTarget.DAP_GSPD;
                    GlobalTargetList[ModeAIndex].CALC_GSPD = CurrentTarget.CALC_GSPD;
                    GlobalTargetList[ModeAIndex].ACID_Mode_S = CurrentTarget.ACID_Mode_S;
                    if (CurrentTarget.Mode_S_Addr != "N/A")
                        GlobalTargetList[ModeAIndex].Mode_S_Addr = CurrentTarget.Mode_S_Addr;
                    if (CurrentTarget.DAP_HDG != "N/A")
                        GlobalTargetList[ModeAIndex].DAP_HDG = CurrentTarget.DAP_HDG;
                    GlobalTargetList[ModeAIndex].CALC_HDG = CurrentTarget.CALC_HDG;
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
                    GlobalTargetList[ModeAIndex].TimeSinceMidnight = CurrentTarget.TimeSinceMidnight;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    // Handle history points
                    if (GlobalTargetList[ModeAIndex].MyMarker.HistoryPoints.Count > 0)
                    {
                        // select a reference elllipsoid
                        Ellipsoid reference = Ellipsoid.WGS84;
                        // instantiate the calculator
                        GeodeticCalculator geoCalc = new GeodeticCalculator();
                        GlobalPosition Track_1 = new GlobalPosition(new GlobalCoordinates(CurrentTarget.Lat, CurrentTarget.Lon));
                        GlobalPosition Track_2 = new GlobalPosition(new GlobalCoordinates(GlobalTargetList[ModeAIndex].MyMarker.HistoryPoints.Last().LatLong.Lat,
                            GlobalTargetList[ModeAIndex].MyMarker.HistoryPoints.Last().LatLong.Lng));

                        // Calculate distance traveled
                        double DistanceTraveled = geoCalc.CalculateGeodeticMeasurement(reference, Track_1, Track_2).PointToPointDistance;
                        DistanceTraveled = DistanceTraveled * 0.00053996; // Convert to nautical miles
                        double BetweenTwoUpdates = CurrentTarget.TimeSinceMidnight - GlobalTargetList[ModeAIndex].MyMarker.HistoryPoints.Last().TimeSinceMidnight;

                        int Miliseconds = (int)(((BetweenTwoUpdates - Math.Floor(BetweenTwoUpdates)) * 10.0));
                        TimeSpan TimeDifference = new TimeSpan(0, 0, 0, (int)Math.Floor(BetweenTwoUpdates), Miliseconds);

                        // Only update history position if there was actually a change in the distance
                        if (DistanceTraveled > 0)
                        {
                            if (GlobalTargetList[ModeAIndex].MyMarker.HistoryPoints.Count > Max_History_Points)
                                GlobalTargetList[ModeAIndex].MyMarker.HistoryPoints.Dequeue();
                            GMapTargetandLabel.HistoryPointsType HP = new GMapTargetandLabel.HistoryPointsType();
                            HP.LatLong = new PointLatLng(CurrentTarget.Lat, CurrentTarget.Lon);
                            HP.TimeSinceMidnight = CurrentTarget.TimeSinceMidnight;
                            GlobalTargetList[ModeAIndex].MyMarker.HistoryPoints.Enqueue(HP);
                        }
                    }
                    else
                    {
                        GMapTargetandLabel.HistoryPointsType HP = new GMapTargetandLabel.HistoryPointsType();
                        HP.LatLong = new PointLatLng(CurrentTarget.Lat, CurrentTarget.Lon);
                        HP.TimeSinceMidnight = CurrentTarget.TimeSinceMidnight;
                        GlobalTargetList[ModeAIndex].MyMarker.HistoryPoints.Enqueue(HP);
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
                    NewTarget.CALC_GSPD = GlobalTarget.CALC_GSPD;
                    NewTarget.DAP_GSPD = GlobalTarget.DAP_GSPD;
                    NewTarget.ACID_Mode_S = GlobalTarget.ACID_Mode_S;
                    NewTarget.Mode_S_Addr = GlobalTarget.Mode_S_Addr;
                    NewTarget.TRK = GlobalTarget.TRK;
                    NewTarget.TAS = GlobalTarget.TAS;
                    NewTarget.Roll_Ang = GlobalTarget.Roll_Ang;
                    NewTarget.SelectedAltitude_ShortTerm = GlobalTarget.SelectedAltitude_ShortTerm;
                    NewTarget.SelectedAltitude_LongTerm = GlobalTarget.SelectedAltitude_LongTerm;
                    NewTarget.Rate_Of_Climb = GlobalTarget.Rate_Of_Climb;
                    NewTarget.MACH = GlobalTarget.MACH;
                    NewTarget.DAP_HDG = GlobalTarget.DAP_HDG;
                    NewTarget.CALC_HDG = GlobalTarget.CALC_HDG;
                    NewTarget.IAS = GlobalTarget.IAS;
                    NewTarget.Barometric_Setting = GlobalTarget.Barometric_Setting;
                    NewTarget.Lat = GlobalTarget.Lat;
                    NewTarget.Lon = GlobalTarget.Lon;
                    NewTarget.TimeSinceMidnight = GlobalTarget.TimeSinceMidnight;
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
                        // Get Time Since Midnight
                        CAT01I141Types.CAT01141ElapsedTimeSinceMidnight TimeSinceMidnight = (CAT01I141Types.CAT01141ElapsedTimeSinceMidnight)Msg.CAT01DataItems[CAT01.ItemIDToIndex("141")].value;
                        // Get Calculated GSPD and HDG_Type
                        CAT01I200Types.CalculatedGSPandHDG_Type CalculatedGSPandHDG = (CAT01I200Types.CalculatedGSPandHDG_Type)Msg.CAT01DataItems[CAT01.ItemIDToIndex("200")].value;


                        TargetType Target = new TargetType();
                        if (MyCAT01I020UserData != null)
                        {
                            if (MyCAT01I020UserData.Type_Of_Radar_Detection == CAT01I020Types.Radar_Detection_Type.Primary)
                            {
                                Target.ModeA = "PSR";
                                Target.ModeC = "";
                                Target.Lat = LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal;
                                Target.Lon = LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal;
                                Target.TimeSinceMidnight = TimeSinceMidnight.ElapsedTimeSinceMidnight;
                                Target.TrackTerminateTreshold = 0;

                                if (CalculatedGSPandHDG != null)
                                {
                                    if (CalculatedGSPandHDG.Is_Valid)
                                    {
                                        Target.CALC_GSPD = Math.Round(CalculatedGSPandHDG.GSPD, 0).ToString();
                                        Target.CALC_HDG = Math.Round(CalculatedGSPandHDG.HDG, 0).ToString();
                                    }
                                }

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

                                        if (TimeSinceMidnight != null)
                                            Target.TimeSinceMidnight = TimeSinceMidnight.ElapsedTimeSinceMidnight;

                                        if (CalculatedGSPandHDG != null)
                                        {
                                            if (CalculatedGSPandHDG.Is_Valid)
                                            {
                                                Target.CALC_GSPD = Math.Round(CalculatedGSPandHDG.GSPD, 0).ToString();
                                                Target.CALC_HDG = Math.Round(CalculatedGSPandHDG.HDG, 0).ToString();
                                            }
                                        }

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
                        // Get Mode S Address
                        CAT48I220Types.CAT48AC_Address_Type Mode_S_Address = (CAT48I220Types.CAT48AC_Address_Type)Msg.CAT48DataItems[CAT48.ItemIDToIndex("220")].value;
                        // Get ACID data for Mode-S
                        CAT48I240Types.CAT48I240ACID_Data ACID_Mode_S = (CAT48I240Types.CAT48I240ACID_Data)Msg.CAT48DataItems[CAT48.ItemIDToIndex("240")].value;
                        // Get Mode-S MB Data
                        CAT48I250Types.CAT48I250DataType CAT48I250Mode_S_MB = (CAT48I250Types.CAT48I250DataType)Msg.CAT48DataItems[CAT48.ItemIDToIndex("250")].value;
                        // Get Time since midnight
                        CAT48I140Types.CAT48140ElapsedTimeSinceMidnight TimeSinceMidnight = (CAT48I140Types.CAT48140ElapsedTimeSinceMidnight)Msg.CAT48DataItems[CAT48.ItemIDToIndex("140")].value;
                        // Get Calculated GSPD and HDG_Type
                        CAT48I200Types.CalculatedGSPandHDG_Type CalculatedGSPandHDG = (CAT48I200Types.CalculatedGSPandHDG_Type)Msg.CAT48DataItems[CAT48.ItemIDToIndex("200")].value;

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
                                Target.TimeSinceMidnight = TimeSinceMidnight.ElapsedTimeSinceMidnight;
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
                                        Target.TimeSinceMidnight = TimeSinceMidnight.ElapsedTimeSinceMidnight;

                                        if (Mode_S_Address != null)
                                        {
                                            if (Mode_S_Address.Is_Valid)
                                                Target.Mode_S_Addr = Mode_S_Address.AC_ADDRESS_String;
                                            else
                                                Target.Mode_S_Addr = "N/A";
                                        }
                                        else
                                            Target.Mode_S_Addr = "N/A";


                                        if (CAT48I250Mode_S_MB != null)
                                        {
                                            if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.Present_This_Cycle)
                                            {
                                                if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.MAG_HDG.Is_Valid)
                                                    Target.DAP_HDG = Math.Round(CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.MAG_HDG.Value).ToString();
                                                else
                                                    Target.DAP_HDG = "N/A";

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
                                                    Target.DAP_GSPD = Math.Round(CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.GND_SPD.Value).ToString();
                                                else
                                                    Target.DAP_GSPD = "N/A";

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

                                        // If GSPD and HDG are available from CAT48/I200 then use it
                                        if (CalculatedGSPandHDG != null)
                                        {
                                            if (CalculatedGSPandHDG.Is_Valid)
                                            {
                                                Target.CALC_GSPD = Math.Round(CalculatedGSPandHDG.GSPD, 0).ToString();
                                                Target.CALC_HDG = Math.Round(CalculatedGSPandHDG.HDG, 0).ToString();
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
                        CAT62I070Types.CAT62070ElapsedTimeSinceMidnight TimeSinceMidnight = (CAT62I070Types.CAT62070ElapsedTimeSinceMidnight)Msg.CAT62DataItems[CAT62.ItemIDToIndex("070")].value;
                        CAT62I185Types.CalculatedGSPandHDG_Type GSPD_and_HDG = (CAT62I185Types.CalculatedGSPandHDG_Type)Msg.CAT62DataItems[CAT62.ItemIDToIndex("185")].value;

                        TargetType Target = new TargetType();

                        if (Msg.CAT62DataItems[CAT62.ItemIDToIndex("040")].value != null)
                            Target.TrackNumber = (int)Msg.CAT62DataItems[CAT62.ItemIDToIndex("040")].value;

                        if (Mode3AData != null && LatLongData != null)
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
                                if (CAT62I380Data.AC_Address.Is_Valid)
                                    Target.Mode_S_Addr = CAT62I380Data.AC_Address.AC_ADDRESS_String;
                                else
                                    Target.Mode_S_Addr = "N/A";

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
                                    Target.DAP_HDG = Math.Round(CAT62I380Data.M_HDG.M_HDG).ToString();
                                else
                                    Target.DAP_HDG = "N/A";

                                if (CAT62I380Data.TRK.Is_Valid)
                                    Target.TRK = Math.Round(CAT62I380Data.TRK.TRK).ToString();
                                else
                                    Target.TRK = "N/A";

                                if (CAT62I380Data.GSPD.Is_Valid)
                                    Target.DAP_GSPD = Math.Round(CAT62I380Data.GSPD.GSPD).ToString();
                                else
                                    Target.DAP_GSPD = "N/A";

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

                            if (TimeSinceMidnight != null)
                                Target.TimeSinceMidnight = TimeSinceMidnight.ElapsedTimeSinceMidnight;

                            if (GSPD_and_HDG != null)
                            {
                                if (GSPD_and_HDG.Is_Valid)
                                {
                                    Target.CALC_GSPD = Math.Round(GSPD_and_HDG.GSPD, 0).ToString();
                                    Target.CALC_HDG = Math.Round(GSPD_and_HDG.HDG, 0).ToString();
                                }
                            }

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
                        // Get Time Since Midnight
                        CAT01I141Types.CAT01141ElapsedTimeSinceMidnight TimeSinceMidnight = (CAT01I141Types.CAT01141ElapsedTimeSinceMidnight)Msg.CAT01DataItems[CAT01.ItemIDToIndex("141")].value;
                        // Get Calculated GSPD and HDG_Type
                        CAT01I200Types.CalculatedGSPandHDG_Type CalculatedGSPandHDG = (CAT01I200Types.CalculatedGSPandHDG_Type)Msg.CAT01DataItems[CAT01.ItemIDToIndex("200")].value;

                        TargetType Target = new TargetType();
                        if (MyCAT01I020UserData != null)
                        {
                            if (MyCAT01I020UserData.Type_Of_Radar_Detection == CAT01I020Types.Radar_Detection_Type.Primary)
                            {
                                Target.ModeA = "PSR";
                                Target.ModeC = "";
                                Target.Lat = LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal;
                                Target.Lon = LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal;
                                Target.TimeSinceMidnight = TimeSinceMidnight.ElapsedTimeSinceMidnight;
                                Target.MyMarker.Position = new PointLatLng(Target.Lat, Target.Lon);

                                if (CalculatedGSPandHDG != null)
                                {
                                    if (CalculatedGSPandHDG.Is_Valid)
                                    {
                                        Target.CALC_GSPD = Math.Round(CalculatedGSPandHDG.GSPD, 0).ToString();
                                        Target.CALC_HDG = Math.Round(CalculatedGSPandHDG.HDG, 0).ToString();
                                    }
                                }

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
                                        Target.TimeSinceMidnight = TimeSinceMidnight.ElapsedTimeSinceMidnight;

                                        if (CalculatedGSPandHDG != null)
                                        {
                                            if (CalculatedGSPandHDG.Is_Valid)
                                            {
                                                Target.CALC_GSPD = Math.Round(CalculatedGSPandHDG.GSPD, 0).ToString();
                                                Target.CALC_HDG = Math.Round(CalculatedGSPandHDG.HDG, 0).ToString();
                                            }
                                        }

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
                        // Get Mode S Address
                        CAT48I220Types.CAT48AC_Address_Type Mode_S_Address = (CAT48I220Types.CAT48AC_Address_Type)Msg.CAT48DataItems[CAT48.ItemIDToIndex("220")].value;
                        // Get ACID data for Mode-S
                        CAT48I240Types.CAT48I240ACID_Data ACID_Mode_S = (CAT48I240Types.CAT48I240ACID_Data)Msg.CAT48DataItems[CAT48.ItemIDToIndex("240")].value;
                        // Get Mode-S MB Data
                        CAT48I250Types.CAT48I250DataType CAT48I250Mode_S_MB = (CAT48I250Types.CAT48I250DataType)Msg.CAT48DataItems[CAT48.ItemIDToIndex("250")].value;
                        // Get Time since midnight
                        CAT48I140Types.CAT48140ElapsedTimeSinceMidnight TimeSinceMidnight = (CAT48I140Types.CAT48140ElapsedTimeSinceMidnight)Msg.CAT48DataItems[CAT48.ItemIDToIndex("140")].value;
                        // Get Calculated GSPD and HDG_Type
                        CAT48I200Types.CalculatedGSPandHDG_Type CalculatedGSPandHDG = (CAT48I200Types.CalculatedGSPandHDG_Type)Msg.CAT48DataItems[CAT48.ItemIDToIndex("200")].value;

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
                                Target.TimeSinceMidnight = TimeSinceMidnight.ElapsedTimeSinceMidnight;
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
                                        Target.TimeSinceMidnight = TimeSinceMidnight.ElapsedTimeSinceMidnight;

                                        if (Mode_S_Address != null)
                                        {
                                            if (Mode_S_Address.Is_Valid)
                                                Target.Mode_S_Addr = Mode_S_Address.AC_ADDRESS_String;
                                            else
                                                Target.Mode_S_Addr = "N/A";
                                        }
                                        else
                                            Target.Mode_S_Addr = "N/A";

                                        if (CAT48I250Mode_S_MB != null)
                                        {
                                            if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.Present_This_Cycle)
                                            {
                                                if (CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.MAG_HDG.Is_Valid)
                                                    Target.DAP_HDG = Math.Round(CAT48I250Mode_S_MB.BDS60_HDG_SPD_Report.MAG_HDG.Value).ToString();
                                                else
                                                    Target.DAP_HDG = "N/A";

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
                                                    Target.DAP_GSPD = Math.Round(CAT48I250Mode_S_MB.BDS50_Track_Turn_Report.GND_SPD.Value).ToString();
                                                else
                                                    Target.DAP_GSPD = "N/A";

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

                                        // If GSPD and HDG are available from CAT48/I200 then use it
                                        if (CalculatedGSPandHDG != null)
                                        {
                                            if (CalculatedGSPandHDG.Is_Valid)
                                            {
                                                Target.CALC_GSPD = Math.Round(CalculatedGSPandHDG.GSPD, 0).ToString();
                                                Target.CALC_HDG = Math.Round(CalculatedGSPandHDG.HDG, 0).ToString();
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
                        CAT62I070Types.CAT62070ElapsedTimeSinceMidnight TimeSinceMidnight = (CAT62I070Types.CAT62070ElapsedTimeSinceMidnight)Msg.CAT62DataItems[CAT62.ItemIDToIndex("070")].value;
                        CAT62I185Types.CalculatedGSPandHDG_Type GSPD_and_HDG = (CAT62I185Types.CalculatedGSPandHDG_Type)Msg.CAT62DataItems[CAT62.ItemIDToIndex("185")].value;

                        TargetType Target = new TargetType();

                        if (Msg.CAT62DataItems[CAT62.ItemIDToIndex("040")].value != null)
                            Target.TrackNumber = (int)Msg.CAT62DataItems[CAT62.ItemIDToIndex("040")].value;

                        if (Mode3AData != null && LatLongData != null)
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
                                if (CAT62I380Data.AC_Address.Is_Valid)
                                    Target.Mode_S_Addr = CAT62I380Data.AC_Address.AC_ADDRESS_String;
                                else
                                    Target.Mode_S_Addr = "N/A";

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
                                    Target.DAP_HDG = Math.Round(CAT62I380Data.M_HDG.M_HDG).ToString();
                                else
                                    Target.DAP_HDG = "N/A";

                                if (CAT62I380Data.TRK.Is_Valid)
                                    Target.TRK = Math.Round(CAT62I380Data.TRK.TRK).ToString();
                                else
                                    Target.TRK = "N/A";

                                if (CAT62I380Data.GSPD.Is_Valid)
                                    Target.DAP_GSPD = Math.Round(CAT62I380Data.GSPD.GSPD).ToString();
                                else
                                    Target.DAP_GSPD = "N/A";

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

                            if (TimeSinceMidnight != null)
                                Target.TimeSinceMidnight = TimeSinceMidnight.ElapsedTimeSinceMidnight;

                            if (GSPD_and_HDG != null)
                            {
                                if (GSPD_and_HDG.Is_Valid)
                                {
                                    Target.CALC_GSPD = Math.Round(GSPD_and_HDG.GSPD, 0).ToString();
                                    Target.CALC_HDG = Math.Round(GSPD_and_HDG.HDG, 0).ToString();
                                }
                            }

                            CurrentTargetList.Add(Target);
                        }
                    }
                }
            }

            if (Return_Buffered == false)
            {
                UpdateGlobalList();
            }

            STCA.RUN(ref CurrentTargetList);
            TargetList = CurrentTargetList;
        }
    }
}
