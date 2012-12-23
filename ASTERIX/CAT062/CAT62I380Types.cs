using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62I380Types
    {
        // WORD 0 
        public static int Target_Address = Bit_Ops.Bit7;
        public static int Target_Identification = Bit_Ops.Bit6;
        public static int Magnetic_Heading = Bit_Ops.Bit5;
        public static int Indicated_Airspeed_Mach_Number = Bit_Ops.Bit4;
        public static int True_Airspeed = Bit_Ops.Bit3;
        public static int Selected_Altitude = Bit_Ops.Bit2;
        public static int Final_State_SelectedAltitude = Bit_Ops.Bit1;
        public static int WORD0_FX_Extension_Indicator = Bit_Ops.Bit0;
        // WORD 1
        public static int Trajectory_Intent_Status = Bit_Ops.Bit7;
        public static int Trajectory_Intent_Data = Bit_Ops.Bit6;
        public static int Communications_ACAS = Bit_Ops.Bit5;
        public static int Status_Reported_By_ADS_B = Bit_Ops.Bit4;
        public static int ACAS_Resolution_Advisory_Report = Bit_Ops.Bit3;
        public static int Barometric_Vertical_Rate = Bit_Ops.Bit2;
        public static int Geometric_Vertical_Rate = Bit_Ops.Bit1;
        public static int WORD1_FX_Extension_Indicator = Bit_Ops.Bit0;
        // WORD 2
        public static int Roll_Angle = Bit_Ops.Bit7;
        public static int Track_Angle_Rate = Bit_Ops.Bit6;
        public static int Track_Angle = Bit_Ops.Bit5;
        public static int Ground_Speed = Bit_Ops.Bit4;
        public static int Velocity_Uncertainty = Bit_Ops.Bit3;
        public static int Meteorological_Data = Bit_Ops.Bit2;
        public static int Emitter_Category = Bit_Ops.Bit1;
        public static int WORD2_FX_Extension_Indicator = Bit_Ops.Bit0;
        // WORD 3
        public static int Position_Data = Bit_Ops.Bit7;
        public static int Geometric_Altitude_Data = Bit_Ops.Bit6;
        public static int Position_Uncertainty_Data = Bit_Ops.Bit5;
        public static int Mode_S_MB_Data = Bit_Ops.Bit4;
        public static int Indicated_Airspeed = Bit_Ops.Bit3;
        public static int Mach_Number = Bit_Ops.Bit2;
        public static int Barometric_Pressure_Setting = Bit_Ops.Bit1;
        public static int WORD3_FX_Extension_Indicator = Bit_Ops.Bit0;

        public class CAT62AC_Address_Type
        {
            public string AC_ADDRESS_String = "------";
            public bool Is_Valid = false;
        }

        public class CAT62ACIDType
        {
            public string ACID_String = "------";
            public bool Is_Valid = false;
        }

        public class CAT62TASType
        {
            public int TAS = 0;
            public bool Is_Valid = false;
        }

        public class CAT62IASType
        {
            public int IAS = 0;
            public bool Is_Valid = false;
        }

        public class CAT62MACHType
        {
            public double MACH = 0.0;
            public bool Is_Valid = false;
        }

        public class CAT62TrackAngleType
        {
            public double TRK = 0.0;
            public bool Is_Valid = false;
        }

        public class CAT62MagneticHeadingType
        {
            public double M_HDG = 0.0;
            public bool Is_Valid = false;
        }

        public class CAT62GroundSpeedType
        {
            public double GSPD = 0.0;
            public bool Is_Valid = false;
        }

        public class CAT62BarometricPressureSettingType
        {
            public double Baro_Pressure_Setting = 0.0;
            public bool Is_Valid = false;
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////
        /// COM, ACAS and Flight Status Reportin types
        /// </summary>
        public class CAT62COM_ACAS_Flight_Status_Type
        {
            public enum COM { NO_COM_STATUS, COM_A_B, COM_A_B_Uplink_ELM, COM_A_B_Uplink_ELM_Downlink_ELM, Level_5_Transponde };
            public enum Flight_Status { No_Alert_No_Spi_Airborne, No_Alert_No_Spi_Ground, Alert_No_Spi_Airborne, Alert_No_Spi_Ground, Alert_Spi_Ground_or_Airborne, No_Alert_Spi_Ground_or_Airborne };

            // Do not care about BDS fow now....
            
            public COM COM_Reporting = COM.COM_A_B;
            public Flight_Status FL_Status = Flight_Status.Alert_No_Spi_Airborne;
            public bool SSC = false;
            public bool ARC_Is_25_Feet = false;
            public bool AIC = false;
            public bool Is_Valid = false;
        }

        public class CAT62Rool_Angle_Type
        {
            public double Rool_Angle = 0.0;
            public bool Is_Valid = false;
        }

        public class CAT62FinalStateSelectedAltitudeType
        {
            public bool Manage_Mode_Active = false;
            public bool Altitude_Hold_Active = false;
            public bool Approach_Mode_Active = false;
            public int SelectedAltitude = 0;
            public bool Is_Valid = false;
        }

        public class CAT62SelectedAltitudeType
        {
            public enum SourceType { Unknown, AircraftAltitude, FCU_MCP, FMS_Selected };
            public bool Source_Info_Provided = false;
            public SourceType Source = SourceType.Unknown;
            public int SelectedAltitude = 0;
            public bool Is_Valid = false;
        }

        // Encapsulate the whole CAT62I380 data
        // into one class
        public class CAT62I380Data
        {
            public CAT62AC_Address_Type AC_Address = new CAT62AC_Address_Type();
            public CAT62ACIDType ACID = new CAT62ACIDType();
            public CAT62TASType TAS = new CAT62TASType();
            public CAT62IASType IAS = new CAT62IASType();
            public CAT62MACHType MACH = new CAT62MACHType();
            public CAT62TrackAngleType TRK = new CAT62TrackAngleType();
            public CAT62MagneticHeadingType M_HDG = new CAT62MagneticHeadingType();
            public CAT62GroundSpeedType GSPD = new CAT62GroundSpeedType();
            public CAT62COM_ACAS_Flight_Status_Type COM_ACAS_FL_Reporting = new CAT62COM_ACAS_Flight_Status_Type();
            public CAT62Rool_Angle_Type Rool_Angle = new CAT62Rool_Angle_Type();
            public CAT62FinalStateSelectedAltitudeType FS_Selected_Altitude = new CAT62FinalStateSelectedAltitudeType();
            public CAT62SelectedAltitudeType Selected_Altitude = new CAT62SelectedAltitudeType();
            public CAT62BarometricPressureSettingType Baro_Press_Setting = new CAT62BarometricPressureSettingType();
        }
    }
}
