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
            // First check if the two targets are already in separation violation status.


            // No they are not, then check if the two targets are going to be in 
            // the separation violation status a parameter set time in the future
            // This is so called "violation prediction status"
        }
    }
}
