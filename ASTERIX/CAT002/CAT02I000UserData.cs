using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser
{
    class CAT02I000UserData
    {

        public CAT02I000Types.Message_Type Message_Type = CAT02I000Types.Message_Type.Unknown_Data;


        public static void DecodeCAT02I000(byte[] Data)
        {

            // First define CAT01I002 class
            CAT02I000UserData MyI002UserData = new CAT02I000UserData();

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();
            //Extract the first octet
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT02.CurrentDataBufferOctalIndex];

            int result = BO.DWord[Bit_Ops.Bits0_7_Of_DWord];

            switch (result)
            {
                case 1:
                    MyI002UserData.Message_Type = CAT02I000Types.Message_Type.North_Marker_Msg;
                    FormMain MainFrame = Application.OpenForms[0] as FormMain;
                    MainFrame.HandleNorthMarkMessage();
                    break;
                case 2:
                    MyI002UserData.Message_Type = CAT02I000Types.Message_Type.Sector_Crossing_Msg;
                    break;
                case 3:
                    MyI002UserData.Message_Type = CAT02I000Types.Message_Type.South_Marker_Msg;
                    break;
                case 8:
                    MyI002UserData.Message_Type = CAT02I000Types.Message_Type.Activation_Of_Blind_Zone_Filtering;
                    break;
                case 9:
                    MyI002UserData.Message_Type = CAT02I000Types.Message_Type.Stop_Of_Blind_Zone_Filtering;
                    break;
                default:
                    break;
            }

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT02.I002DataItems[CAT02.ItemIDToIndex("000")].value = MyI002UserData.Message_Type;
            //////////////////////////////////////////////////////////////////////////////////

        }
    }
}
