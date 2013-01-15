using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    public enum STCA_Status_Type { Prediction, Violation };
    public class STCA_Target_Item
    {
        public STCA_Status_Type STCA_Status = STCA_Status_Type.Prediction;
        public int STCA_Partner = -1;
        public double CurrentDistance = 0.0;
        public int TimeToImpact_Sec = 0;
        public int TimeToConflictSec = 0;
    }

    class STCA
    {
        // Define STCA parameters
        private static double Min_Horizontal_Separation_Nm = 4.9;
        private static int Min_Vertical_Separation_Feet = 900;
        private static int Prediction_Time_Seconds = 120;
        private static int Violation_Time_Seconds = 60;

        // To be called with STCA parameters.
        // Can be called at any time
        public static void Set_STCA_Parameters(double Min_HZ_Sep, int Min_Vert_Sep, int Pred_Time_Sec, int Viol_Time_Sec)
        {
            Min_Horizontal_Separation_Nm = Min_HZ_Sep;
            Min_Vertical_Separation_Feet = Min_Vert_Sep;
            Prediction_Time_Seconds = Pred_Time_Sec;
            Violation_Time_Seconds = Viol_Time_Sec;
        }

        // This method takes in a list of all targets from the last update cycle (by reference) and checks each
        // active target against each other active target to determine if there has been an STCA prediction/conflict.
        // If determined it adds the detected STCA prediction/violation to the STCA target list. 
        // It is possible that a single target is in prediction/violation condition with more than one other target
        // in any possible combination of predictions/violations.
        public static void RUN(ref System.Collections.Generic.List<DynamicDisplayBuilder.TargetType> CurrentTargets)
        {
            // No need to check for STCA if only one target is present
            if (CurrentTargets.Count > 1)
            {
                for (int Out_Index = 0; Out_Index < CurrentTargets.Count; Out_Index++)
                {
                    DynamicDisplayBuilder.TargetType MasterTarget = CurrentTargets[Out_Index];
                    // First check if this master target is active
                    if (MasterTarget.TrackNumber != -1)
                    {
                        // It is active, so lets check its position/altitude against every other 
                        // active target, except itself. Loop through all active targets
                        for (int Inner_Index = Out_Index + 1; Out_Index < CurrentTargets.Count; Out_Index++)
                        {
                            // Get the Target to compare against and make sure it is an active one
                            DynamicDisplayBuilder.TargetType CurrentTarget = CurrentTargets[Inner_Index];
                            if (CurrentTarget.TrackNumber != -1)
                            {
                                // First check if the vertical separation condition is infridged  
                                // as it is much less  processing extensive operation.
                                if (Is_Vertical_Infridged(MasterTarget.ModeC, CurrentTarget.ModeC))
                                {
                                    // Now when we know that we have two active targets whose vertical
                                    // separation is infringed lets check the vertial separation. In the case
                                    // it is infringed then the following method will calculate its attributes
                                    // and add the STCA to the targets STCA list. The front end display code will
                                    // then use this information to properly indicate/draw STCA warning to the controller
                                    Check_And_Set_Horizontal_Infringed(ref MasterTarget, CurrentTarget);
                                }
                            }
                        }
                    }
                }
            }
        }

        // This method takes in two target Flight Levels and determines if
        // vertical separation is infridged. The parameters are as set by the 
        // Set_STCA_Parameters method.
        private static bool Is_Vertical_Infridged(string T1_FL, string T2_FL)
        {
            bool Result = false;
            double T1_Level, T2_Level = 0.0;

            if (double.TryParse(T1_FL, out T1_Level))
            {
                if (double.TryParse(T2_FL, out T2_Level))
                {
                    double Absolute_Difference = Math.Abs(T1_Level - T2_Level);
                    if (Absolute_Difference < (Min_Vertical_Separation_Feet + 0.1))
                        Result = true;
                }
            }

            return Result;
        }

        // This method takes in two Target Positions and determines if horizontal 
        // separation is infringed. The first parameter is passed by reference as
        // the method will, in the case it determines that separation is infringed
        // set appropriate inication in the passed in Target. It actually sets all
        // items in the STCA_List
        private static void Check_And_Set_Horizontal_Infringed(ref DynamicDisplayBuilder.TargetType T1, DynamicDisplayBuilder.TargetType T2)
        {

            ////////////////////////////////////////////////////////////////////////////////////////////////
            // First extract and validate all the data 
            //
            GlobalPosition Track_1_Pos = new GlobalPosition(new GlobalCoordinates(T1.Lat, T1.Lon));
            GlobalPosition Track_2_Pos = new GlobalPosition(new GlobalCoordinates(T2.Lat, T2.Lon));
            bool DataValid = false;
            double Track_1_SPD;
            double Track_2_SPD;
            double Track_1_TRK;
            double Track_2_TRK;

            if (!double.TryParse(T1.CALC_GSPD, out Track_1_SPD))
                DataValid = false;
            if (!double.TryParse(T2.CALC_GSPD, out Track_2_SPD))
                DataValid = false;
            if (!double.TryParse(T1.TRK, out Track_1_TRK))
            {
                if (!double.TryParse(T1.DAP_HDG, out Track_1_TRK))
                    DataValid = false;
            }
            if (!double.TryParse(T2.TRK, out Track_2_TRK))
            {
                if (!double.TryParse(T2.DAP_HDG, out Track_2_TRK))
                    DataValid = false;
            }

            // Data validated 
            if (DataValid)
            {
                // select a reference elllipsoid
                Ellipsoid reference = Ellipsoid.WGS84;
                // instantiate the calculator
                GeodeticCalculator geoCalc = new GeodeticCalculator();
                // Used to calculate the time to the min distance 
                GlobalPosition Track_1 = new GlobalPosition(new GlobalCoordinates(Track_1_Pos.Latitude, Track_1_Pos.Longitude));
                GlobalPosition Track_2 = new GlobalPosition(new GlobalCoordinates(Track_2_Pos.Latitude, Track_2_Pos.Longitude));

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // First check if the two targets are already in separation violation status.
                // If so, then add the STCA items to the STCA pair target
                double DistanceToTravel = geoCalc.CalculateGeodeticMeasurement(reference, Track_1, Track_2).PointToPointDistance;
                DistanceToTravel = DistanceToTravel * 0.00053996; // Convert to nautical miles

                if (DistanceToTravel < Min_Horizontal_Separation_Nm)
                {
                    STCA_Target_Item STCA_Item = new STCA_Target_Item();
                    STCA_Item.CurrentDistance = DistanceToTravel;
                    STCA_Item.STCA_Partner = T2.TrackNumber;
                    STCA_Item.STCA_Status = STCA_Status_Type.Violation;
                    STCA_Item.TimeToImpact_Sec = 10;
                    STCA_Item.TimeToConflictSec = 0;
                    T1.STCA_List.Add(STCA_Item);
                }

                // No they are not, then check if the two targets are going to be in 
                // the separation violation status a parameter set time in the future
                // This is so called "violation prediction status"

            }
        }
    }
}
