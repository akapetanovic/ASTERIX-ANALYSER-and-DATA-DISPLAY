using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT01DecodeAndStore
    {
        // This method will accept a buffer of data with the assumption that 
        // category has been determined. It will then decode the data and save 
        // it in the shared buffer. Every time a message is passed in the data 
        // will be appended to the buffer. This means that each line will contain 
        // data for one message. For data items which are not in the message,
        // indicated by the FSPEC field, N/A will be inserted instead. The shared 
        // buffer is loacted in the SharedData and will not be saved. It is responsibility
        // of the user to save the data in a file if desired.

        public static void Do(byte[] Data)
        {
            // First check what type of data is received?
            if (CAT01.Type_Of_Report == CAT01I020Types.Type_Of_Report_Type.Plot)
            {
                ///////////////////////////////////////////////////////////////////////
                //                              PLOT DATA                            //
                ///////////////////////////////////////////////////////////////////////

                // I001/010 Data Source Identifier                       2
                // NO NEED to do anything this is handled in CAT01

                // I001/020 Target Report Descriptor                     +1
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("020")].CurrentlyPresent == true)
                {
                    CAT01I020UserData.DecodeCAT01I002(Data);
                }

                // I001/040 Measured Position in Polar Coordinates       4
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("040")].CurrentlyPresent == true)
                {
                    CAT01I040UserData.DecodeCAT01I040(Data);
                }

                // I001/070 Mode-3/A Code in Octal Representation        2
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("070")].CurrentlyPresent == true)
                {
                    CAT01I070UserData.DecodeCAT01I070(Data);
                }

                // I001/090 Mode-C Code in Binary Representation         2
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("090")].CurrentlyPresent == true)
                {
                    CAT01I090UserData.DecodeCAT01I090(Data);
                }

                // I001/130 Radar Plot Characteristics                  1+
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("130")].CurrentlyPresent == true)
                {
                    CAT01I130UserData.DecodeCAT01I130(Data);
                }

                // I001/141 Truncated Time of Day                       2
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("141")].CurrentlyPresent == true)
                {
                    CAT01I141UserData.DecodeCAT01I141(Data);
                }

                // FX -------- Field Extension Indicator                    -

                // I001/050 Mode-2 Code in Octal Representation         2
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("050")].CurrentlyPresent == true)
                {
                    CAT01I050UserData.DecodeCAT01I050(Data);
                }

                // I001/120 Measured Radial Doppler Speed               1
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("120")].CurrentlyPresent == true)
                {
                    CAT01I120UserData.DecodeCAT01I120(Data);
                }

                // I001/131 Received Power                              1
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("131")].CurrentlyPresent == true)
                {
                    CAT01I131UserData.DecodeCAT01I131(Data);
                }

                // I001/080 Mode-3/A Code Confidence Indicator          2
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("080")].CurrentlyPresent == true)
                {
                    CAT01I080UserData.DecodeCAT01I080(Data);
                }

                // I001/100 Mode-C Code and Code Confidence Indicator   4
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("100")].CurrentlyPresent == true)
                {
                    CAT01I100UserData.DecodeCAT01I100(Data);
                }

                // I001/060 Mode-2 Code Confidence Indicator            2
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("060")].CurrentlyPresent == true)
                {
                    CAT01I060UserData.DecodeCAT01I060(Data);
                }

                // I001/030 Warning/Error Conditions                    1+
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("030")].CurrentlyPresent == true)
                {
                    CAT01I030UserData.DecodeCAT01I030(Data);
                }

                // FX  -------- Field Extension Indicator                   -

                // I001/150 Presence of X-Pulse                         1
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("150")].CurrentlyPresent == true)
                {
                    CAT01I150UserData.DecodeCAT01I150(Data);
                }

                StoreDecodedData();
            }
            else
            {
                ///////////////////////////////////////////////////////////////////////
                //                             TRACK DATA                            //
                ///////////////////////////////////////////////////////////////////////

                // I001/010 Data Source Identifier
                // NO NEED to do anything this is handled in CAT01

                // I001/020 Target Report Descriptor                        +1
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("020")].CurrentlyPresent == true)
                {
                    CAT01I020UserData.DecodeCAT01I002(Data);
                }

                // I001/161 Track/Plot Number                               2
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("161")].CurrentlyPresent == true)
                {
                    CAT01I161UserData.DecodeCAT01I161(Data);
                }

                // I001/040 Measured Position in Polar Coordinates          4
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("040")].CurrentlyPresent == true)
                {
                    CAT01I040UserData.DecodeCAT01I040(Data);
                }

                // I001/042 Calculated Position in Cartesian Coordinates    4
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("042")].CurrentlyPresent == true)
                {
                    CAT01I042UserData.DecodeCAT01I042(Data);
                }

                // I001/200 Calculated Track Velocity in polar Coordinates  4
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("200")].CurrentlyPresent == true)
                {
                    CAT01I200UserData.DecodeCAT01I200(Data);
                }

                // I001/070 Mode-3/A Code in Octal Representation           2
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("070")].CurrentlyPresent == true)
                {
                    CAT01I070UserData.DecodeCAT01I070(Data);
                }

                // I001/090 Mode-C Code in Binary Representation            2
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("090")].CurrentlyPresent == true)
                {
                    CAT01I090UserData.DecodeCAT01I090(Data);
                }

                // I001/141 Truncated Time of Day                           2
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("141")].CurrentlyPresent == true)
                {
                    CAT01I141UserData.DecodeCAT01I141(Data);
                }

                // I001/130 Radar Plot Characteristics                      1+
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("130")].CurrentlyPresent == true)
                {
                    CAT01I130UserData.DecodeCAT01I130(Data);
                }

                // I001/131 Received Power                                  1
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("131")].CurrentlyPresent == true)
                {
                    CAT01I131UserData.DecodeCAT01I131(Data);
                }

                // I001/120 Measured Radial Doppler Speed                   1
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("120")].CurrentlyPresent == true)
                {
                    CAT01I120UserData.DecodeCAT01I120(Data);
                }

                // I001/170 Track Status                                    1+
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("170")].CurrentlyPresent == true)
                {
                    CAT01I170UserData.DecodeCAT01I170(Data);
                }

                // I001/210 Track Quality                                   1+
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("210")].CurrentlyPresent == true)
                {
                    CAT01I210UserData.DecodeCAT01I210(Data);
                }

                // FX Field Extension Indicator

                // I001/050 Mode-2 Code in Octal Representation             2
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("050")].CurrentlyPresent == true)
                {
                    CAT01I050UserData.DecodeCAT01I050(Data);
                }

                // I001/080 Mode-3/A Code Confidence Indicator              2     
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("080")].CurrentlyPresent == true)
                {
                    CAT01I080UserData.DecodeCAT01I080(Data);
                }

                // I001/100 Mode-C Code and Code Confidence Indicator       4
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("100")].CurrentlyPresent == true)
                {
                    CAT01I100UserData.DecodeCAT01I100(Data);
                }

                // I001/060 Mode-2 Code Confidence Indicator                2
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("060")].CurrentlyPresent == true)
                {
                    CAT01I060UserData.DecodeCAT01I060(Data);
                }

                // I001/030 Warning/Error Conditions                        1+
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("030")].CurrentlyPresent == true)
                {
                    CAT01I030UserData.DecodeCAT01I030(Data);
                }

                //  Reserved for Special Purpose Indicator (SP)

                //  Reserved for RFS Indicator (RS-bit)

                // FX Field Extension Indicator

                //  I001/150 Presence of X-Pulse                            1
                if (CAT01.I001DataItems[CAT01.ItemIDToIndex("150")].CurrentlyPresent == true)
                {
                    CAT01I150UserData.DecodeCAT01I150(Data);
                }

                StoreDecodedData();
            }
        }

        private static void StoreDecodedData()
        {
            ////////////////////////////////////////////////////////////////////////////////////
            // All CAT01 data has been decoded, so lets save off the message data to the global
            // storage for latter usage

            MainASTERIXDataStorage.CAT01Data CAT01MessageData = new MainASTERIXDataStorage.CAT01Data();

            foreach (CAT01.CAT01DataItem Item in CAT01.I001DataItems)
            {
                CAT01.CAT01DataItem MyItem = new CAT01.CAT01DataItem();

                MyItem.CurrentlyPresent = Item.CurrentlyPresent;
                MyItem.Description = Item.Description;
                MyItem.HasBeenPresent = Item.HasBeenPresent;
                MyItem.ID = Item.ID;
                MyItem.value = Item.value;

                CAT01MessageData.CAT01DataItems.Add(MyItem);
            }

            MainASTERIXDataStorage.CAT01Message.Add(CAT01MessageData);
            CAT01.Intitialize(false);

        }
    }
}
