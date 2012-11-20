using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT48DecodeAndStore
    {
        // This method will accept a buffer of data with the assumption that 
        // category has been determined. It will then decode the data and save 
        // it in the shared buffer. Everry time a message is passed in the data 
        // will be appended to the buffer. This means that each line will contain 
        // data for one message. For data items which are not in the message,
        // indicated by the FSPEC field, N/A will be inserted instead. The shared 
        // buffer is loacted in the SharedData and will not be saved. It is
        //  responsibility of the user to save the data in a file it desired.
        public static void Do(byte[] Data)
        {
            // I048/010 	Data Source Identifier                          2 
            // NO NEED to do anything this is handled in CAT01

            // I048/140 Time-of-Day                                         3 
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("140")].CurrentlyPresent == true)
            {
                CAT48I140UserData.DecodeCAT48I140(Data);
            }

            // I048/020 Target Report Descriptor                            1+ 
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("020")].CurrentlyPresent == true)
            {
                CAT48I020UserData.DecodeCAT48I002(Data);
            }

            // I048/040 Measured Position in Slant Polar Coordinates        4
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("040")].CurrentlyPresent == true)
            {
                CAT48I040UserData.DecodeCAT48I040(Data);
            }

            // I048/070 Mode-3/A Code in Octal Representation               2
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("070")].CurrentlyPresent == true)
            {
                CAT48I070UserData.DecodeCAT48I070(Data);
            }

            // I048/090 Flight Level in Binary Representation               2
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("090")].CurrentlyPresent == true)
            {
                CAT48I090UserData.DecodeCAT48I090(Data);
            }

            // I048/130 Radar Plot Characteristics                          1 + 1+
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("130")].CurrentlyPresent == true)
            {
                CAT48I130UserData.DecodeCAT48I130(Data);
            }

            // n.a. 	    Field Extension Indicator 

            // I048/220 Aircraft Address                                    3
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("220")].CurrentlyPresent == true)
            {
                CAT48I220UserData.DecodeCAT48I220(Data);
            }

            // I048/240 Aircraft Identification                             6
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("240")].CurrentlyPresent == true)
            {
                CAT48I240UserData.DecodeCAT48I240(Data);
            }

            // I048/250 Mode S MB Data                                      1+8*N
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("250")].CurrentlyPresent == true)
            {
                CAT48I250UserData.DecodeCAT48I250(Data);
            }

            // I048/161 Track Number                                        2
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("161")].CurrentlyPresent == true)
            {
                CAT48I161UserData.DecodeCAT48I161(Data);
            }

            // I048/042 Calculated Position in Cartesian Coordinates        4
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("042")].CurrentlyPresent == true)
            {
                CAT48I042UserData.DecodeCAT48I042(Data);
            }

            // I048/200 Calculated Track Velocity in Polar Representation   4
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("200")].CurrentlyPresent == true)
            {
                CAT48I200UserData.DecodeCAT48I200(Data);
            }

            // I048/170 Track Status                                        1+
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("170")].CurrentlyPresent == true)
            {
                CAT48I170UserData.DecodeCAT48I170(Data);
            }

            // n.a. 	    Field Extension Indicator 

            // I048/210 Track Quality                                       4
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("210")].CurrentlyPresent == true)
            {
                CAT48I210UserData.DecodeCAT48I210(Data);
            }

            // I048/030 Warning/Error Conditions                            1+
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("030")].CurrentlyPresent == true)
            {
                CAT48I030UserData.DecodeCAT48I030(Data);
            }
            // I048/080 Mode-3/A Code Confidence Indicator                  2
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("080")].CurrentlyPresent == true)
            {
                CAT48I080UserData.DecodeCAT48I080(Data);
            }

            // I048/100 Mode-C Code and Confidence Indicator                4
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("100")].CurrentlyPresent == true)
            {
                CAT48I100UserData.DecodeCAT48I100(Data);
            }

            // I048/110 Height Measured by 3D Radar                         2
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("110")].CurrentlyPresent == true)
            {
                CAT48I110UserData.DecodeCAT48I110(Data);
            }

            // I048/120 Radial Doppler Speed                                1+
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("120")].CurrentlyPresent == true)
            {
                CAT48I120UserData.DecodeCAT48I120(Data);
            }

            // I048/230 Communications / ACAS Capability and Flight Status  2
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("230")].CurrentlyPresent == true)
            {
                CAT48I230UserData.DecodeCAT48I230(Data);
            }

            // n.a. 	    Field Extension Indicator 

            // I048/260 ACAS Resolution Advisory Report                     7
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("260")].CurrentlyPresent == true)
            {
                CAT48I260UserData.DecodeCAT48I260(Data);
            }

            // I048/055 Mode-1 Code in Octal Representation                 1
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("055")].CurrentlyPresent == true)
            {
                CAT48I055UserData.DecodeCAT48I055(Data);
            }

            // I048/050, Mode-2 Code in Octal Representation 
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("050")].CurrentlyPresent == true)
            {
                CAT48I050UserData.DecodeCAT48I050(Data);
            }

            // I048/065 Mode-1 Code Confidence Indicator                    1
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("065")].CurrentlyPresent == true)
            {
                CAT48I065UserData.DecodeCAT48I065(Data);
            }

            // I048/060 Mode-2 Code Confidence Indicator                    2
            if (CAT48.I048DataItems[CAT48.ItemIDToIndex("060")].CurrentlyPresent == true)
            {
                CAT48I060UserData.DecodeCAT48I060(Data);
            }

            ////////////////////////////////////////////////////////////////////////////////////
            // All CAT48 data has been decoded, so lets save off the message data to the global
            // storage for latter usage

            MainASTERIXDataStorage.CAT48Data CAT48MessageData = new MainASTERIXDataStorage.CAT48Data();
            
            foreach (CAT48.CAT48DataItem Item in CAT48.I048DataItems)
            {
                CAT48.CAT48DataItem MyItem = new CAT48.CAT48DataItem();

                MyItem.CurrentlyPresent = Item.CurrentlyPresent;
                MyItem.Description = Item.Description;
                MyItem.HasBeenPresent = Item.HasBeenPresent;
                MyItem.ID = Item.ID;
                MyItem.value = Item.value;

                CAT48MessageData.CAT48DataItems.Add(MyItem);
            }
            
            MainASTERIXDataStorage.CAT48Message.Add(CAT48MessageData);
            CAT48.Intitialize(false);
        }

    }
}
