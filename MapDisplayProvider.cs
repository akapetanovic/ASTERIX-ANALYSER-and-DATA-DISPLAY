using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticastingUDP
{
    class MapDisplayProvider
    {
        public class TargetType
        {
            public string ModeA;
            public string ModeC;
            public string ACID_Modes;
            public double Lat;
            public double Lon;
        }

        // Keeps track of the data index from the lasts update of the 
        // display. Used in order to be able to extract only targets recived
        // since the last data update. 
        private static int LastDataIndex = 0;

        // This method is to be used to reset the data index when the main data buffer is reseted
        // and a new block of data is te be recived.
        public int ResetGetDataIndex
        {
            get { return LastDataIndex; }
            set { LastDataIndex = 0; }
        }

        private static System.Collections.Generic.List<TargetType> CurrentTargetList = new System.Collections.Generic.List<TargetType>();

        // Each time this method is called it will extract the targets recived since the
        // the method was last called. It returns a list of the targets in a user friendly
        // format (The TargetType)
        public static void GetDisplayData(bool Return_Buffered, out System.Collections.Generic.List<TargetType> TargetList)
        {

            // First remove all the previous data
            CurrentTargetList.Clear();

            if (Return_Buffered == true)
            {
                if (MainDataStorage.CAT01Message.Count > 0)
                {
                    for (int Start_Idx = 0; Start_Idx < MainDataStorage.CAT01Message.Count; Start_Idx++)
                    {
                        MainDataStorage.CAT01Data Msg = MainDataStorage.CAT01Message[Start_Idx];

                        // Get Mode3A
                        CAT01I070Types.CAT01070Mode3UserData Mode3AData = (CAT01I070Types.CAT01070Mode3UserData)Msg.I001DataItems[CAT01.ItemIDToIndex("070")].value;
                        // Get Lat/Long
                        CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates LatLongData = (CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates)Msg.I001DataItems[CAT01.ItemIDToIndex("040")].value;
                        // Get Flight Level
                        CAT01I090Types.CAT01I090FlightLevelUserData FlightLevelData = (CAT01I090Types.CAT01I090FlightLevelUserData)Msg.I001DataItems[CAT01.ItemIDToIndex("090")].value;

                        TargetType Target = new TargetType();
                        Target.ModeA = Mode3AData.Mode3A_Code;
                        Target.ModeC = FlightLevelData.FlightLevel.ToString();
                        Target.Lat = LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal;
                        Target.Lon = LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal;
                        CurrentTargetList.Add(Target);
                    }
                }
                else if (MainDataStorage.CAT48Message.Count > 0)
                {

                    for (int Start_Idx = 0; Start_Idx < MainDataStorage.CAT48Message.Count; Start_Idx++)
                    {

                        MainDataStorage.CAT48Data Msg = MainDataStorage.CAT48Message[Start_Idx];

                        CAT48I070Types.CAT48I070Mode3UserData Mode3AData = (CAT48I070Types.CAT48I070Mode3UserData)Msg.I048DataItems[CAT48.ItemIDToIndex("070")].value;
                        // Get Lat/Long in decimal
                        CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates LatLongData = (CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates)Msg.I048DataItems[CAT48.ItemIDToIndex("040")].value;
                        // Get Flight Level
                        CAT48I090Types.CAT48I090FlightLevelUserData FlightLevelData = (CAT48I090Types.CAT48I090FlightLevelUserData)Msg.I048DataItems[CAT48.ItemIDToIndex("090")].value;
                        // Get ACID data for Mode-S
                        CAT48I240Types.CAT48I240ACID_Data ACID_Mode_S = (CAT48I240Types.CAT48I240ACID_Data)Msg.I048DataItems[CAT48.ItemIDToIndex("240")].value;
                        
                        TargetType Target = new TargetType();
                        Target.ModeA = Mode3AData.Mode3A_Code;
                        Target.ModeC = FlightLevelData.FlightLevel.ToString();
                        Target.ACID_Modes = ACID_Mode_S.ACID;
                        Target.Lat = LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal;
                        Target.Lon = LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal;
                        CurrentTargetList.Add(Target);
                    }
                }
            }
            else
            {
                if (MainDataStorage.CAT01Message.Count > 0)
                {
                    for (int Start_Idx = LastDataIndex; Start_Idx < MainDataStorage.CAT01Message.Count; Start_Idx++)
                    {
                        LastDataIndex++;
                        MainDataStorage.CAT01Data Msg = MainDataStorage.CAT01Message[Start_Idx];

                        // Get Mode3A
                        CAT01I070Types.CAT01070Mode3UserData Mode3AData = (CAT01I070Types.CAT01070Mode3UserData)Msg.I001DataItems[CAT01.ItemIDToIndex("070")].value;
                        // Get Lat/Long
                        CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates LatLongData = (CAT01I040Types.CAT01I040MeasuredPosInPolarCoordinates)Msg.I001DataItems[CAT01.ItemIDToIndex("040")].value;
                        // Get Flight Level
                        CAT01I090Types.CAT01I090FlightLevelUserData FlightLevelData = (CAT01I090Types.CAT01I090FlightLevelUserData)Msg.I001DataItems[CAT01.ItemIDToIndex("090")].value;

                        TargetType Target = new TargetType();
                        Target.ModeA = Mode3AData.Mode3A_Code;
                        Target.ModeC = FlightLevelData.FlightLevel.ToString();
                        Target.Lat = LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal;
                        Target.Lon = LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal;
                        CurrentTargetList.Add(Target);
                    }
                }
                else if (MainDataStorage.CAT48Message.Count > 0)
                {

                    for (int Start_Idx = LastDataIndex; Start_Idx < MainDataStorage.CAT48Message.Count; Start_Idx++)
                    {
                        LastDataIndex++;

                        MainDataStorage.CAT48Data Msg = MainDataStorage.CAT48Message[Start_Idx];

                        CAT48I070Types.CAT48I070Mode3UserData Mode3AData = (CAT48I070Types.CAT48I070Mode3UserData)Msg.I048DataItems[CAT48.ItemIDToIndex("070")].value;
                        // Get Lat/Long in decimal
                        CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates LatLongData = (CAT48I040Types.CAT48I040MeasuredPosInPolarCoordinates)Msg.I048DataItems[CAT48.ItemIDToIndex("040")].value;
                        // Get Flight Level
                        CAT48I090Types.CAT48I090FlightLevelUserData FlightLevelData = (CAT48I090Types.CAT48I090FlightLevelUserData)Msg.I048DataItems[CAT48.ItemIDToIndex("090")].value;
                        // Get ACID data for Mode-S
                        CAT48I240Types.CAT48I240ACID_Data ACID_Mode_S = (CAT48I240Types.CAT48I240ACID_Data)Msg.I048DataItems[CAT48.ItemIDToIndex("240")].value;
                        

                        TargetType Target = new TargetType();
                        Target.ModeA = Mode3AData.Mode3A_Code;
                        Target.ModeC = FlightLevelData.FlightLevel.ToString();
                        Target.ACID_Modes = ACID_Mode_S.ACID;
                        Target.Lat = LatLongData.LatLong.GetLatLongDecimal().LatitudeDecimal;
                        Target.Lon = LatLongData.LatLong.GetLatLongDecimal().LongitudeDecimal;
                        CurrentTargetList.Add(Target);
                    }

                }
            }

            TargetList = CurrentTargetList;
        }
    }
}
