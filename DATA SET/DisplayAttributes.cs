using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AsterixDisplayAnalyser
{
    class DisplayAttributes
    {
        // Define a boolean to be used as display invalidation. This is to signal MainForm
        // that a display attribute has changed and that new display build is required.
        public static bool StaticDisplayBuildRequired = false;

        // Define all available data display items
        public enum DisplayItemsType { Radar, Waypoint, StateBorder, BackgroundColor, SectorBorder, RunwayBorder };


        // Define type for the possibloe display attributes.
        // Not all attributes are applicable for all display items, but for simplicity
        // define all possible here and use those that are applicable
        public class DisplayAttributesType
        {
            public string ItemName = "Default";

            // Text attributes
            public int TextSize = 8;
            public FontFamily TextFont = FontFamily.GenericSansSerif;
            public Color TextColor = Color.White;

            // Line/border attributes
            public int LineWidth = 2;
            public Color LineColor = Color.White;
            public DashStyle LineStyle = DashStyle.Solid;

            // Polygon/Areas attributes
            public Color AreaPolygonColor = Color.Black;

            // Image attributes
            public Size ImageSize = new Size(30, 30);
        }

        // To be called upon system startup in order to load display parameters.
        // In fact it would be possible to re-load configuration files during runtime 
        // and dynamically change display attributes.
        public static void Load()
        {
            // First populate the DisplayAttributeDataSet with each DisplayItem
            /////////////////////////////////////////////////////////////////////
            //                          IMPORTANT !!!
            // Make sure that items are added in the same sequence as defined in
            // the DisplayItems enumeration. Later on the data is to be retrieved 
            // by the name only.

            // Radar 0
            DisplayAttributesType DataItem = new DisplayAttributesType();
            DataItem.ItemName = "Radar";
            DisplayAttributeDataSet.Add(DataItem);
            // Waypoint 1
            DataItem = new DisplayAttributesType();
            DataItem.ItemName = "Waypoint";
            DisplayAttributeDataSet.Add(DataItem);
            // StateBorder 2
            DataItem = new DisplayAttributesType();
            DataItem.ItemName = "StateBorder";
            DisplayAttributeDataSet.Add(DataItem);
            // BackgroundColor 3
            DataItem = new DisplayAttributesType();
            DataItem.ItemName = "BackgroundColor";
            DisplayAttributeDataSet.Add(DataItem);
            // SectorBorder 4
            DataItem = new DisplayAttributesType();
            DataItem.ItemName = "SectorBorder";
            DisplayAttributeDataSet.Add(DataItem);
            // RunwayBorder 5
            DataItem = new DisplayAttributesType();
            DataItem.ItemName = "RunwayBorder";
            DisplayAttributeDataSet.Add(DataItem);
        }

        // Returns list index based on the enumerated type.
        private static int DisplayItemToIndex(DisplayItemsType ItemToGet)
        {
            int Index = 0;

            switch (ItemToGet)
            {
                case DisplayItemsType.Radar:
                    Index = 0;
                    break;
                case DisplayItemsType.Waypoint:
                    Index = 1;
                    break;
                case DisplayItemsType.StateBorder:
                    Index = 2;
                    break;
                case DisplayItemsType.BackgroundColor:
                    Index = 3;
                    break;
                case DisplayItemsType.SectorBorder:
                    Index = 4;
                    break;
                case DisplayItemsType.RunwayBorder:
                    Index = 5;
                    break;
                default:
                    break;

            }
            return Index;
        }

        // Define storage for all display attribute items
        private static System.Collections.Generic.List<DisplayAttributesType> DisplayAttributeDataSet = new System.Collections.Generic.List<DisplayAttributesType>();

        // Gets display attribute based on the passed in display item name.
        public static DisplayAttributesType GetDisplayAttribute(DisplayItemsType ItemToGet)
        {
            return DisplayAttributeDataSet[DisplayItemToIndex(ItemToGet)];
        }

        // Set display attribute
        public static void SetDisplayAttribute(DisplayItemsType Item, DisplayAttributesType Attribute)
        {
            DisplayAttributeDataSet[DisplayItemToIndex(Item)] = Attribute;

            //DisplayAttribute.ItemName = words[0];
            //DisplayAttribute.TextSize = int.Parse(words[1]);
            //DisplayAttribute.TextFont = new FontFamily(words[2]);
            //DisplayAttribute.TextColor = Color.FromName(words[3]);
            //DisplayAttribute.LineWidth = int.Parse(words[4]);
            //DisplayAttribute.LineColor = Color.FromName(words[5]);
            //DisplayAttribute.LineStyle = DashStyle.Solid;         
            //DisplayAttribute.AreaPolygonColor = Color.FromName(words[7]);
            //DisplayAttribute.ImageSize = new Size(int.Parse(words[8]), int.Parse((words[9])));

            StaticDisplayBuildRequired = true;
        }

        public static System.Collections.Generic.List<DisplayAttributesType> GetAllDisplayAttributes()
        {
            return DisplayAttributeDataSet;
        }

        public static DashStyle GetLineStypefromString(string LineStyleIn)
        {
            DashStyle DashStyleOut = new DashStyle();

            switch (LineStyleIn)
            {
                case "Solid":
                    DashStyleOut = DashStyle.Solid;
                    break;
                case "Dash":
                    DashStyleOut = DashStyle.Dash;
                    break;
                case "DashDot":
                    DashStyleOut = DashStyle.DashDot;
                    break;
                case "DashDotDot":
                    DashStyleOut = DashStyle.DashDotDot;
                    break;
                case "Dot":
                    DashStyleOut = DashStyle.Dot;
                    break;
                default:
                    DashStyleOut = DashStyle.Solid;
                    break;
            }
            return DashStyleOut;
        }
    }
}


