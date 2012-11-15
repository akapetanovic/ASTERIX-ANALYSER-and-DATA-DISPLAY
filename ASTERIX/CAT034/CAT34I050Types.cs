using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT34I050Types
    {

        /// <summary>
        /// /////////////////////////////////////////////////////
        /// // Define indexes for the subfields (COM, PSR, SSR)
        /// //
        /// </summary>
        public static int Subfiled_1 = Bit_Ops.Bit7;
        public static int Subfiled_2 = Bit_Ops.Bit6;
        public static int Subfiled_3 = Bit_Ops.Bit5;
        public static int Subfiled_4 = Bit_Ops.Bit4;
        public static int Subfiled_5 = Bit_Ops.Bit3;
        public static int Subfiled_6 = Bit_Ops.Bit2;
        public static int Subfiled_7 = Bit_Ops.Bit1;
        public static int FX_Primary_Subfiled = Bit_Ops.Bit0;

        public class COM
        {
            public bool Data_Present = false;
            public bool System_is_NOGO = false;
            public bool RDPC2_Selected = false;
            public bool RDPC_Reset = false;
            public bool RDP_Overloaded = false;
            public bool Transmision_Sys_Overloaded = false;
            public bool Monitor_Sys_Disconected = false;
            public bool Time_Source_Invalid = false;

        }

        public class PSR
        {
            public bool Data_Present = false;
            public enum Channel_Status { No_Channel, Channel_A, Channel_B, Channel_A_and_B };
            public bool Ant_2_Selected = false;
            public bool PSR_Overloaded = false;
            public Channel_Status CH_Status = Channel_Status.No_Channel;
            public bool Monitor_Sys_Disconected = false;
        }

        public class SSR
        {
            public bool Data_Present = false;
            public enum Channel_Status { No_Channel, Channel_A, Channel_B, Invalid_Combination };
            public bool Ant_2_Selected = false;
            public bool SSR_Overloaded = false;
            public Channel_Status CH_Status = Channel_Status.No_Channel;
            public bool Monitor_Sys_Disconected = false;

        }

        public class MDS
        {
            public bool Data_Present = false;
            public enum Channel_Status { No_Channel, Channel_A, Channel_B, Illegal_Combination };
            public bool Ant_2_Selected = false;
            public Channel_Status CH_Status = Channel_Status.No_Channel;
            public bool ModeS_Overloaded = false;
            public bool Monitor_Sys_Disconected = false;
            public bool CH2_For_Coordination_In_Use = false;
            public bool CH2_For_DataLink_In_Use = false;
            public bool Coordination_Func_Overload = false;
            public bool DataLink_Func_Overload = false;

        }

        public class CAT34I050UserData
        {
            public COM COM_Data = new COM();
            public PSR PSR_Data = new PSR();
            public SSR SSR_Data = new SSR();
            public MDS MDS_Data = new MDS();
        }
    }
}
