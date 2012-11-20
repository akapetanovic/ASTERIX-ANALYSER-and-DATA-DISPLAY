using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT02DecodeAndStore
    {
        // This method will accept a buffer of data with the assumption that 
        // category has been determined. It will then decode the data and save 
        // it in the shared buffer. Every time a message is passed in the data 
        // will be appended to the buffer. This means that each line will contain 
        // data for one message. For data items which are not in the message,
        // indicated by the FSPEC field, N/A will be inserted instead. The shared 
        // buffer is loacted in the SharedData and will not be saved. It is responsibility
        // of the user to save the data in a file it desired.
        public static void Do(byte[] Data)
        {
           // BitExtractor BE = new BitExtractor();

            // I002/000, Message Type                        1
            if (CAT02.I002DataItems[CAT02.ItemIDToIndex("000")].CurrentlyPresent == true)
            {
                CAT02I000UserData.DecodeCAT02I000(Data);
            }

            //  I002/020 Sector Number                       1
            if (CAT02.I002DataItems[CAT02.ItemIDToIndex("020")].CurrentlyPresent == true)
            {
                CAT02I020UserData.DecodeCAT02I020(Data);
            }

            //  I002/030 Time of Day                         3
            if (CAT02.I002DataItems[CAT02.ItemIDToIndex("030")].CurrentlyPresent == true)
            {
                CAT02I030UserData.DecodeCAT02I030(Data);
            }

            //  I002/041 Antenna Rotation Period             2
            if (CAT02.I002DataItems[CAT02.ItemIDToIndex("041")].CurrentlyPresent == true)
            {
                CAT02I041UserData.DecodeCAT02I041(Data);
            }
            //  I002/050 Station Configuration Status        1+
            if (CAT02.I002DataItems[CAT02.ItemIDToIndex("050")].CurrentlyPresent == true)
            {
                CAT02I050UserData.DecodeCAT02I050(Data);
            }

            //  I002/060 Station Processing Mode             1+
            if (CAT02.I002DataItems[CAT02.ItemIDToIndex("060")].CurrentlyPresent == true)
            {
                CAT02I060UserData.DecodeCAT02I060(Data);
            }

            //  FX Field Extension Indicator

            //  I002/070 Plot Count Values                   (1 + 2 X N)
            if (CAT02.I002DataItems[CAT02.ItemIDToIndex("070")].CurrentlyPresent == true)
            {
                CAT02I070UserData.DecodeCAT02I070(Data);
            }

            //  I002/100 Dynamic Window - Type 1             8
            if (CAT02.I002DataItems[CAT02.ItemIDToIndex("100")].CurrentlyPresent == true)
            {
                CAT02I100UserData.DecodeCAT02I100(Data);
            }

            //  I002/090 Collimation Error 2
            if (CAT02.I002DataItems[CAT02.ItemIDToIndex("090")].CurrentlyPresent == true)
            {
                CAT02I090UserData.DecodeCAT02I090(Data);
            }

            //  I002/080 Warning/Error Conditions            1+
            if (CAT02.I002DataItems[CAT02.ItemIDToIndex("080")].CurrentlyPresent == true)
            {
                CAT02I080UserData.DecodeCAT02I080(Data);
            }

            ////////////////////////////////////////////////////////////////////////////////////
            // All CAT02 data has been decoded, so lets save off the message data to the global
            // storage for latter usage
            MainASTERIXDataStorage.CAT02Data CAT02MessageData = new MainASTERIXDataStorage.CAT02Data();
            foreach (CAT02.CAT02DataItem Item in CAT02.I002DataItems)
            {
                CAT02.CAT02DataItem MyItem = new CAT02.CAT02DataItem();
                MyItem.CurrentlyPresent = Item.CurrentlyPresent;
                MyItem.Description = Item.Description;
                MyItem.HasBeenPresent = Item.HasBeenPresent;
                MyItem.ID = Item.ID;
                MyItem.value = Item.value;
                CAT02MessageData.CAT02DataItems.Add(MyItem);
            }
            MainASTERIXDataStorage.CAT02Message.Add(CAT02MessageData);
            CAT02.Intitialize(false);
        }
    }
}
