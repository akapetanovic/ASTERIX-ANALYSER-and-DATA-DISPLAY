using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser
{
    class CAT34I000UserData
    {

        public CAT34I000Types.Message_Type Message_Type = CAT34I000Types.Message_Type.Unknown_Data;


        public static void DecodeCAT34I000(byte[] Data)
        {

            // First define CAT34I000 class
            CAT34I000UserData MyCAT34I000UserData = new CAT34I000UserData();

            // Get an instance of bit ops
            Bit_Ops BO = new Bit_Ops();
            //Extract the first octet
            BO.DWord[Bit_Ops.Bits0_7_Of_DWord] = Data[CAT34.CurrentDataBufferOctalIndex];

            int result = BO.DWord[Bit_Ops.Bits0_7_Of_DWord];

            switch (result)
            {
                case 1:
                    MyCAT34I000UserData.Message_Type = CAT34I000Types.Message_Type.North_Marker_Msg;
                    FormMain MainFrame = Application.OpenForms[0] as FormMain;
                    MainFrame.HandleNorthMarkMessage();
                    break;
                case 2:
                    MyCAT34I000UserData.Message_Type = CAT34I000Types.Message_Type.Sector_Crossing_Msg;
                    break;
                case 3:
                    MyCAT34I000UserData.Message_Type = CAT34I000Types.Message_Type.Geographical_Filtering_Msg;
                    break;
                case 4:
                    MyCAT34I000UserData.Message_Type = CAT34I000Types.Message_Type.Jamming_Strobe_Msg;
                    break;
               
                default:
                    break;
            }

            //////////////////////////////////////////////////////////////////////////////////
            // Now assign it to the generic list
            CAT34.I034DataItems[CAT34.ItemIDToIndex("000")].value = MyCAT34I000UserData.Message_Type;
            //////////////////////////////////////////////////////////////////////////////////

        }
    }
}
