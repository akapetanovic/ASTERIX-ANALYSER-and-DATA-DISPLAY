using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT62DecodeAndStore
    {
        // This method will accept a buffer of data with the assumption that 
        // category has been determined. It will then decode the data and save 
        // it in the shared buffer. Everry time a message is passed in the data 
        // will be appended to the buffer. This means that each line will contain 
        // data for one message. For data items which are not in the message,
        // indicated by the FSPEC field, N/A will be inserted instead. The shared 
        // buffer is loacted in the SharedData and will not be saved. It is responsibility
        // of the user to save the data in a file it desired.
        public static void Do(byte[] Data)
        {

            // I048/010 	Data Source Identifier                     2 
            // NO NEED to do anything this is handled in CAT01

            // 2.     - Spare -
            
            // I062/015 Service Identification                          1
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("015")].CurrentlyPresent == true)
            {
                CAT62I015UserData.DecodeCAT62I015(Data);
            }
            
            // I062/070 Time Of Track Information                       3
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("070")].CurrentlyPresent == true)
            {
                CAT62I070UserData.DecodeCAT62I070(Data);
            }

            // I062/105 Calculated Track Position (WGS-84)              8
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("105")].CurrentlyPresent == true)
            {
                CAT62I105UserData.DecodeCAT62I105(Data);
            }

            // I062/100 Calculated Track Position (Cartesian)           6
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("100")].CurrentlyPresent == true)
            {
                CAT62I100UserData.DecodeCAT62I100(Data);
            }

            // I062/185 Calculated Track Velocity (Cartesian)           4
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("185")].CurrentlyPresent == true)
            {
                CAT62I185UserData.DecodeCAT62I185(Data);
            }
            
            // FX.     - Field extension indicator -
            //
            
            // I062/210 Calculated Acceleration (Cartesian)             2
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("210")].CurrentlyPresent == true)
            {
                CAT62I210UserData.DecodeCAT62I210(Data);
            }

            // I062/060 Track Mode 3/A Code                             2
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("060")].CurrentlyPresent == true)
            {
                CAT62I060UserData.DecodeCAT62I060(Data);
            }

            // I062/245 Target Identification                           7
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("245")].CurrentlyPresent == true)
            {
                CAT62I245UserData.DecodeCAT62I245(Data);
            }

            // I062/380 Aircraft Derived Data                           1+
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("380")].CurrentlyPresent == true)
            {
                CAT62I380UserData.DecodeCAT62I380(Data);
            }

            // I062/040 Track Number                                    2
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("040")].CurrentlyPresent == true)
            {
                CAT62I040UserData.DecodeCAT62I040(Data);
            }

            // I062/080 Track Status                                    1+
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("080")].CurrentlyPresent == true)
            {
                CAT62I080UserData.DecodeCAT62I080(Data);
            }

            // I062/290 System Track Update Ages                        1+
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("290")].CurrentlyPresent == true)
            {
                CAT62I290UserData.DecodeCAT62I290(Data);
            }
            
            // FX.     - Field extension indicator -
            //
            // I062/200 Mode of Movement                                1
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("200")].CurrentlyPresent == true)
            {
                CAT62I200UserData.DecodeCAT62I200(Data);
            }

            // I062/295 Track Data Ages                                 1+
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("295")].CurrentlyPresent == true)
            {
                CAT62I295UserData.DecodeCAT62I295(Data);
            }

            // I062/136 Measured Flight Level                           2
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("136")].CurrentlyPresent == true)
            {
                CAT62I136UserData.DecodeCAT62I136(Data);
            }

            // I062/130 Calculated Track Geometric Altitude             2
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("130")].CurrentlyPresent == true)
            {
                CAT62I130UserData.DecodeCAT62I130(Data);
            }

            // I062/135 Calculated Track Barometric Altitude            2
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("135")].CurrentlyPresent == true)
            {
                CAT62I135UserData.DecodeCAT62I135(Data);
            }

            // I062/220 Calculated Rate Of Climb/Descent                2
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("220")].CurrentlyPresent == true)
            {
                CAT62I220UserData.DecodeCAT62I220(Data);
            }

            // I062/390 Flight Plan Related Data                        1+
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("390")].CurrentlyPresent == true)
            {
                CAT62I390UserData.DecodeCAT62I390(Data);
            }
            
            // FX.     - Field extension indicator -
            //
            
            // I062/270 Target Size & Orientation                       1+
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("270")].CurrentlyPresent == true)
            {
                CAT62I270UserData.DecodeCAT62I270(Data);
            }

            // I062/300 Vehicle Fleet Identification                    1
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("300")].CurrentlyPresent == true)
            {
                CAT62I300UserData.DecodeCAT62I300(Data);
            }

            // I062/110 Mode 5 Data reports & Extended Mode 1 Code      1+
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("110")].CurrentlyPresent == true)
            {
                CAT62I110UserData.DecodeCAT62I110(Data);
            }

            // I062/120 Track Mode 2 Code                               2
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("120")].CurrentlyPresent == true)
            {
               CAT62I120UserData.DecodeCAT62I120(Data);
            }

            // I062/510 Composed Track Number                           3+
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("510")].CurrentlyPresent == true)
            {
                CAT62I510UserData.DecodeCAT62I510(Data);
            }

            // I062/500 Estimated Accuracies                            1+
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("500")].CurrentlyPresent == true)
            {
                CAT62I500UserData.DecodeCAT62I500(Data);
            }

            // I062/340 Measured Information                            1+
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("340")].CurrentlyPresent == true)
            {
                CAT62I340UserData.DecodeCAT62I340(Data);
            }
            
            // FX.     - Field extension indicator -

            // I062/500 Estimated Accuracies                            1+
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("REF")].CurrentlyPresent == true)
            {
                CAT62IREFUserData.DecodeCAT62IREF(Data);
            }

            // I062/340 Measured Information                            1+
            if (CAT62.I062DataItems[CAT62.ItemIDToIndex("SPI")].CurrentlyPresent == true)
            {
                CAT62ISPIUserData.DecodeCAT62ISPI(Data);
            }


            ////////////////////////////////////////////////////////////////////////////////////
            // All CAT62 data has been decoded, so lets save off the message data to the global
            // storage for latter usage

            MainASTERIXDataStorage.CAT62Data CAT62MessageData = new MainASTERIXDataStorage.CAT62Data();

            foreach (CAT62.CAT062DataItem Item in CAT62.I062DataItems)
            {
                CAT62.CAT062DataItem MyItem = new CAT62.CAT062DataItem();

                MyItem.CurrentlyPresent = Item.CurrentlyPresent;
                MyItem.Description = Item.Description;
                MyItem.HasBeenPresent = Item.HasBeenPresent;
                MyItem.ID = Item.ID;
                MyItem.value = Item.value;
                CAT62MessageData.CAT62DataItems.Add(MyItem);
            }

            MainASTERIXDataStorage.CAT62Message.Add(CAT62MessageData);
            CAT62.Intitialize(false);
        }
    }
}
