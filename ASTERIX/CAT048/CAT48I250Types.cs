using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT48I250Types
    {
        public class BDS50_Track_and_Turn_Report
        {
            public class Roll_Angle
            {
                public bool Is_Valid = false;
                public double Value = 0.0;
            }

            public class True_Track_Angle
            {
                public bool Is_Valid = false;
                public double Value = 0.0;
            }

            public class Ground_Speed
            {
                public bool Is_Valid = false;
                public double Value = 0.0;
            }

            public class Track_Angle_Rate
            {
                public bool Is_Valid = false;
                public double Value = 0.0;
            }

            public class True_Airspeed
            {
                public bool Is_Valid = false;
                public int Value = 0;
            }

            public class BDS50_Track__Turn_Report_Data
            {
                public Roll_Angle Roll_Ang = new Roll_Angle();
                public True_Track_Angle TRUE_TRK = new True_Track_Angle();
                public Ground_Speed GND_SPD = new Ground_Speed();
                public Track_Angle_Rate TRK_ANG_RATE = new Track_Angle_Rate();
                public True_Airspeed TAS = new True_Airspeed();
                public bool Present_This_Cycle = false;
            }
        }

        public class BDS60_Heading_And_Speed_Report
        {
            public class MagneticHeading
            {
                public bool Is_Valid = false;
                public double Value = 0.0;
            }
            public class IndicatedAirspeed
            {
                public bool Is_Valid = false;
                public int Value = 0;
            }
            public class MachNumber
            {
                public bool Is_Valid = false;
                public double Value = 0.0;
            }
            public class AltitudeRate_RateOfClimbDescent
            {
                public enum Sign { Up, Down };
                public bool Is_Valid = false;
                public int Value = 0;
            }

            public class BDS60_HDG_SPD_Report_Data
            {
                public MagneticHeading MAG_HDG = new MagneticHeading();
                public IndicatedAirspeed IAS = new IndicatedAirspeed();
                public MachNumber MACH = new MachNumber();
                public AltitudeRate_RateOfClimbDescent Baro_RoC = new AltitudeRate_RateOfClimbDescent();
                public AltitudeRate_RateOfClimbDescent Inertial_RoC = new AltitudeRate_RateOfClimbDescent();
                public bool Present_This_Cycle = false;
            }
        }

        public class CAT48I250DataType
        {
            public BDS60_Heading_And_Speed_Report.BDS60_HDG_SPD_Report_Data BDS60_HDG_SPD_Report = new BDS60_Heading_And_Speed_Report.BDS60_HDG_SPD_Report_Data();
            public BDS50_Track_and_Turn_Report.BDS50_Track__Turn_Report_Data BDS50_Track_Turn_Report = new BDS50_Track_and_Turn_Report.BDS50_Track__Turn_Report_Data();
        }
    }

}
