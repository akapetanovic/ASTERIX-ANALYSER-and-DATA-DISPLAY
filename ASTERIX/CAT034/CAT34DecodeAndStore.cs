using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsterixDisplayAnalyser;

namespace AsterixDisplayAnalyser
{
    class CAT34DecodeAndStore
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
            /////////////////////////////////////////////////////////////////////////
            //
            // Next version of Category 002: PSR Radar, M-SSR Radar, Mode-S Station
            //                                               Length in bytes
            //
           
            // I002/000, Message Type                        1
            if (CAT34.I034DataItems[CAT34.ItemIDToIndex("000")].CurrentlyPresent == true)
            {
                CAT34I000UserData.DecodeCAT34I000(Data);
            }
          
            // 3 I034/030 Time-of-Day                           3
            if (CAT34.I034DataItems[CAT34.ItemIDToIndex("030")].CurrentlyPresent == true)
            {
                CAT34I030UserData.DecodeCAT34I030(Data);
            }
            // 4 I034/020 Sector Number                         1
            if (CAT34.I034DataItems[CAT34.ItemIDToIndex("020")].CurrentlyPresent == true)
            {
                CAT34I020UserData.DecodeCAT34I020(Data);
            }
            // 5 I034/041 Antenna Rotation Period               2
            if (CAT34.I034DataItems[CAT34.ItemIDToIndex("041")].CurrentlyPresent == true)
            {
                CAT34I041UserData.DecodeCAT34I041(Data);
            }
            // 6 I034/050 System Configuration and Status       1+
            if (CAT34.I034DataItems[CAT34.ItemIDToIndex("050")].CurrentlyPresent == true)
            {
                CAT34I050UserData.DecodeCAT34I050(Data);
            }
            // 7 I034/060 System Processing Mode                1+
            if (CAT34.I034DataItems[CAT34.ItemIDToIndex("060")].CurrentlyPresent == true)
            {
                CAT34I060UserData.DecodeCAT34I060(Data);
            }
            // FX

            // 8 I034/070 Message Count Values                  1+2N
            if (CAT34.I034DataItems[CAT34.ItemIDToIndex("070")].CurrentlyPresent == true)
            {
                CAT34I070UserData.DecodeCAT34I070(Data);
            }
            // 9 I034/100 Generic Polar Window                  8
            if (CAT34.I034DataItems[CAT34.ItemIDToIndex("100")].CurrentlyPresent == true)
            {
                CAT34I100UserData.DecodeCAT34I100(Data);
            }
            // 10 I034/110 Data Filter                          1
            if (CAT34.I034DataItems[CAT34.ItemIDToIndex("110")].CurrentlyPresent == true)
            {
                CAT34I110UserData.DecodeCAT34I110(Data);
            }
            // 11 I034/120 3D-Position of Data Source           8
            if (CAT34.I034DataItems[CAT34.ItemIDToIndex("120")].CurrentlyPresent == true)
            {
                CAT34I120UserData.DecodeCAT34I120(Data);
            }
            // 12 I034/090 Collimation Error                    2
            if (CAT34.I034DataItems[CAT34.ItemIDToIndex("090")].CurrentlyPresent == true)
            {
                CAT34I090UserData.DecodeCAT34I090(Data);
            }

            ////////////////////////////////////////////////////////////////////////////////////
            // All CAT34 data has been decoded, so lets save off the message data to the global
            // storage for latter usage

            MainASTERIXDataStorage.CAT34Data CAT34MessageData = new MainASTERIXDataStorage.CAT34Data();
            foreach (CAT34.CAT34DataItem Item in CAT34.I034DataItems)
            {
                CAT34.CAT34DataItem MyItem = new CAT34.CAT34DataItem();
                MyItem.CurrentlyPresent = Item.CurrentlyPresent;
                MyItem.Description = Item.Description;
                MyItem.HasBeenPresent = Item.HasBeenPresent;
                MyItem.ID = Item.ID;
                MyItem.value = Item.value;
                CAT34MessageData.CAT34DataItems.Add(MyItem);
            }
            MainASTERIXDataStorage.CAT34Message.Add(CAT34MessageData);
            CAT34.Intitialize(false);
        }
    }
}
