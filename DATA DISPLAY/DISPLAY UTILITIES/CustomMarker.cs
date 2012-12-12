using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using GMap.NET.WindowsForms;
using GMap.NET;

namespace AsterixDisplayAnalyser
{
    public class GMapMarkerImage : GMap.NET.WindowsForms.GMapMarker
    {
        private Image img;        /// <summary>
        /// The image to display as a marker.
        /// </summary>
        public Image MarkerImage
        {
            get
            {
                return img;
            }
            set
            {
                img = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p">The position of the marker</param>
        public GMapMarkerImage(PointLatLng p, Image image)
            : base(p)
        {
            img = image;
            Size = img.Size;
            Offset = new System.Drawing.Point(-Size.Width, Size.Height / 7);

        }

        public override void OnRender(Graphics g)
        {
            g.DrawImage(img, LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);
        }


    }

    public class GMapTargetandLabel : GMap.NET.WindowsForms.GMapMarker
    {

        // Define an index of the track this marker is used for
        // Needed so GUI is able to update the right track (CFL, XFL, etc..
        public int MyTargetIndex = -1;

        // Defines starting position of the label relative to the AC symbol
        public Point LabelOffset = new Point(25, 25);

        // Start positions of AC SYMBOL
        // X and Y
        private int AC_SYMB_START_X = 0;
        private int AC_SYMB_START_Y = 0;
        
        // Defines the size of the label
        private int LabelWidth = 110;
        private int LabelHeight = 50;
        private int SpacingIndex = 2;
        public bool ShowLabelBox = false;

        // Define Mode A attributes + Coast Indicator
        public Point ModeA_CI_OFFSET = new Point(2, 0);
        public Brush ModeA_CI_BRUSH = Brushes.Green;
        public static FontFamily ModeA_CI_FONT_FAMILLY = FontFamily.GenericSansSerif;
        public Font ModeA_CI_FONT = new Font(ModeA_CI_FONT_FAMILLY, 10, FontStyle.Regular, GraphicsUnit.Pixel);
        public string ModeA_CI_STRING = "---- -";

        // Define CALLSIGN attributes 
        public Point CALLSIGN_OFFSET = new Point(2, 0);
        public Brush CALLSIGN_BRUSH = Brushes.Green;
        public static FontFamily CALLSIGN_FONT_FAMILLY = FontFamily.GenericSansSerif;
        public Font CALLSIGN_FONT = new Font(CALLSIGN_FONT_FAMILLY, 10, FontStyle.Regular, GraphicsUnit.Pixel);
        public string CALLSIGN_STRING = "--------";

        // Define Mode C attributes
        public Point ModeC_OFFSET = new Point(2, 0);
        public Brush ModeC_BRUSH = Brushes.Green;
        public static FontFamily ModeC_FONT_FAMILLY = FontFamily.GenericSansSerif;
        public Font ModeC_FONT = new Font(ModeC_FONT_FAMILLY, 10, FontStyle.Regular, GraphicsUnit.Pixel);
        public string ModeC_STRING = "---";

        // Define CFL attributes
        public Point CFL_OFFSET = new Point(2, 0);
        private int CFL_START_X = 0;
        private int CFL_START_Y = 0;
        public Brush CFL_BRUSH = Brushes.Green;
        public static FontFamily CFL_FONT_FAMILLY = FontFamily.GenericSansSerif;
        public Font CFL_FONT = new Font(CFL_FONT_FAMILLY, 10, FontStyle.Regular, GraphicsUnit.Pixel);
        public string CFL_STRING = " ---";

        // Define GSPD attributes
        public Point GSPD_OFFSET = new Point(2, 0);
        private int GSPD_START_X = 0;
        private int GSPD_START_Y = 0;
        public Brush GSPD_BRUSH = Brushes.Green;
        public static FontFamily GSPD_FONT_FAMILLY = FontFamily.GenericSansSerif;
        public Font GSPD_FONT = new Font(GSPD_FONT_FAMILLY, 10, FontStyle.Regular, GraphicsUnit.Pixel);
        public string GSPD_STRING = " ---";

        // Define Assigned HDG attributes
        public Point A_HDG_OFFSET = new Point(2, 0);
        private int HDG_START_X = 0;
        private int HDG_START_Y = 0;
        public Brush A_HDG_BRUSH = Brushes.Green;
        public static FontFamily A_HDG_FONT_FAMILLY = FontFamily.GenericSansSerif;
        public Font A_HDG_FONT = new Font(A_HDG_FONT_FAMILLY, 10, FontStyle.Regular, GraphicsUnit.Pixel);
        public string A_HDG_STRING = "h---";

        // Define Assigned SPD attributes
        public Point A_SPD_OFFSET = new Point(2, 0);
        private int SPD_START_X = 0;
        private int SPD_START_Y = 0;
        public Brush A_SPD_BRUSH = Brushes.Green;
        public static FontFamily A_SPD_FONT_FAMILLY = FontFamily.GenericSansSerif;
        public Font A_SPD_FONT = new Font(A_SPD_FONT_FAMILLY, 10, FontStyle.Regular, GraphicsUnit.Pixel);
        public string A_SPD_STRING = "s---";

        // Define Assigned ROC attributes
        public Point A_ROC_OFFSET = new Point(2, 0);
        public Brush A_ROC_BRUSH = Brushes.Green;
        public static FontFamily A_ROC_FONT_FAMILLY = FontFamily.GenericSansSerif;
        public Font A_ROC_FONT = new Font(A_ROC_FONT_FAMILLY, 10, FontStyle.Regular, GraphicsUnit.Pixel);
        public string A_ROC_STRING = "R---";

        public System.Collections.Generic.Queue<PointLatLng> HistoryPoints = new System.Collections.Generic.Queue<PointLatLng>();

        ////////////////////////////////////////////////////////////
        // These are Extended label data items.
        // 
        public string TAS = "N/A";
        public string IAS = "N/A";
        public string MACH = "N/A";
        public string M_HDG = "N/A";
        public string TRK = "N/A";
        public string Roll_Angle = "N/A";
        public string SelectedAltitude_ShortTerm = "N/A";
        public string SelectedAltitude_LongTerm = "N/A";
        public string Rate_Of_Climb = "N/A";
        public string Barometric_Setting = "N/A";
        ///////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////
        // Here define variable needed for SEP tool implementation
        //
        // This the index of the target 
        // to be monitored for separation
        // If -1 it means no monitoring is required
        public int TargetToMonitor = -1;
        public int TargetMonitoredBy = -1;

        // To be called once the track is terminated
        public void TerminateTarget()
        {
            LabelOffset = new Point(25, 25);
            CFL_STRING = " ---";
            A_HDG_STRING = "h---";
            A_SPD_STRING = "s---";
            A_ROC_STRING = "R---";
            MyTargetIndex = -1;
            HistoryPoints.Clear();
            TargetToMonitor = -1;
            TargetMonitoredBy = -1;
        }

        public int GetLabelWidth()
        {
            return LabelWidth;
        }

        public int GetLabelHeight()
        {
            return LabelHeight;
        }

        public Point GetAC_SYMB_StartPoint()
        {
            return new Point(AC_SYMB_START_X, AC_SYMB_START_Y);
        }

        public Point GetCFLStartPoint()
        {
            return new Point(CFL_START_X, CFL_START_Y);
        }

        public Point GetHDGStartPoint()
        {
            return new Point(HDG_START_X, HDG_START_Y);
        }

        public Point GetSPDStartPoint()
        {
            return new Point(SPD_START_X, SPD_START_Y);
        }

        public Point GetGSPDStartPoint()
        {
            return new Point(GSPD_START_X, GSPD_START_Y);
        }

        // Returns starting point of the label box
        public Point GetLabelStartingPoint()
        {
            return new Point(((LocalPosition.X - LabelOffset.X) - LabelWidth), ((LocalPosition.Y - LabelOffset.Y) - LabelHeight));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p">The position of the marker</param>
        public GMapTargetandLabel(PointLatLng p)
            : base(p)
        {

        }

        public override void OnRender(Graphics g)
        {
            Pen MyPen = new Pen(new SolidBrush(LabelAttributes.TargetColor), LabelAttributes.TargetSize);
            MyPen.DashStyle = LabelAttributes.TargetStyle;

            // Draw AC Symbol
            g.DrawRectangle(MyPen, LocalPosition.X - 5, LocalPosition.Y - 5, 10, 10);
            AC_SYMB_START_X = LocalPosition.X - 5;
            AC_SYMB_START_Y = LocalPosition.Y - 5;

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Here handle drawing of SEP tool
            if (TargetToMonitor != -1)
            {
                Point StartPosition = new Point(LocalPosition.X, LocalPosition.Y);
                Point EndPosition = DynamicDisplayBuilder.GetTargetPositionByIndex(TargetToMonitor);
                g.DrawLine(new Pen(Brushes.Yellow, 1), StartPosition, EndPosition);

                // select a reference elllipsoid
                Ellipsoid reference = Ellipsoid.WGS84;
                // instantiate the calculator
                GeodeticCalculator geoCalc = new GeodeticCalculator();
                GlobalPosition Start = new GlobalPosition(new GlobalCoordinates(this.Position.Lat, this.Position.Lng));
                PointLatLng End_LatLng = FormMain.FromLocalToLatLng(EndPosition.X, EndPosition.Y);
                GlobalPosition End = new GlobalPosition(new GlobalCoordinates(End_LatLng.Lat, End_LatLng.Lng));
                GeodeticMeasurement GM = geoCalc.CalculateGeodeticMeasurement(reference, End, Start);
                
                // Now compute position half way between two points.        
                double distance = GM.PointToPointDistance / 2.0;
                if (distance > 0.0)
                {
                    GlobalCoordinates GC = geoCalc.CalculateEndingGlobalCoordinates(reference, new GlobalCoordinates(End_LatLng.Lat, End_LatLng.Lng), GM.Azimuth, distance);
                    GPoint GP = FormMain.FromLatLngToLocal(new PointLatLng(GC.Latitude.Degrees, GC.Longitude.Degrees));
                    double Distane_NM = 0.00053996 * GM.PointToPointDistance;
                    g.DrawString(Math.Round(GM.Azimuth.Degrees).ToString() + "°/" + Math.Round(Distane_NM, 1).ToString() + "nm", new Font(FontFamily.GenericSansSerif, 9), Brushes.Yellow, new PointF(GP.X, GP.Y));
                }
            }

            // Here handle history points
            // First draw all previous history points
            foreach (PointLatLng I in HistoryPoints)
            {
                GPoint MarkerPositionLocal = FormMain.gMapControl.FromLatLngToLocal(new PointLatLng(I.Lat, I.Lng));
                g.DrawEllipse(MyPen, MarkerPositionLocal.X, MarkerPositionLocal.Y, 3, 3);
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Here draw speed vector
            // // Find out what data should be used for speed vector? IAS, TAS, GSPD, MACH?
            if ((M_HDG != "N/A" || TRK != "N/A") && (GSPD_STRING != null))
            {
                if (GSPD_STRING != "N/A")
                {
                    double Azimuth = 0;
                    double Range = double.Parse(GSPD_STRING);
                    if (TRK != "N/A")
                        Azimuth = double.Parse(TRK);
                    else
                        Azimuth = double.Parse(M_HDG);

                    Range = (Range / 60) * (double)Properties.Settings.Default.SpeedVector;

                    GeoCordSystemDegMinSecUtilities.LatLongClass ResultPosition =
                        GeoCordSystemDegMinSecUtilities.CalculateNewPosition(new GeoCordSystemDegMinSecUtilities.LatLongClass(Position.Lat, Position.Lng), (double)Range, (double)Azimuth);

                    GPoint MarkerPositionLocal = FormMain.gMapControl.FromLatLngToLocal(new PointLatLng(ResultPosition.GetLatLongDecimal().LatitudeDecimal, ResultPosition.GetLatLongDecimal().LongitudeDecimal));
                    g.DrawLine(MyPen, new Point(LocalPosition.X, LocalPosition.Y), new Point(MarkerPositionLocal.X, MarkerPositionLocal.Y));
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MyPen = new Pen(new SolidBrush(LabelAttributes.LineColor), LabelAttributes.LineWidth);
            MyPen.DashStyle = LabelAttributes.LineStyle;

            // Draw leader line
            g.DrawLine(MyPen, new Point(LocalPosition.X, LocalPosition.Y), new Point(LocalPosition.X - LabelOffset.X, LocalPosition.Y - LabelOffset.Y));

            // Draw label box
            Point LabelStartPosition = GetLabelStartingPoint();

            // Recalculate Label Width each cycle to adjust for the possible changes in the number of lines
            // and changes in the text size
            LabelHeight = 0;

            // Draw ModeA and coast indicator
            g.DrawString(ModeA_CI_STRING, ModeA_CI_FONT, ModeA_CI_BRUSH, LabelStartPosition.X + ModeA_CI_OFFSET.X, LabelStartPosition.Y + SpacingIndex);
            LabelHeight = LabelHeight + (int)ModeA_CI_FONT.Size + SpacingIndex * 2;


            if (CALLSIGN_STRING != "--------")
            {
                // Draw CALLSIGN
                g.DrawString(CALLSIGN_STRING, CALLSIGN_FONT, CALLSIGN_BRUSH, LabelStartPosition.X + CALLSIGN_OFFSET.X, LabelStartPosition.Y + LabelHeight);
                LabelHeight = LabelHeight + (int)CALLSIGN_FONT.Size + SpacingIndex * 2;
            }

            // Draw ModeC
            g.DrawString(ModeC_STRING, ModeC_FONT, ModeC_BRUSH, LabelStartPosition.X + ModeC_OFFSET.X, LabelStartPosition.Y + LabelHeight);

            // Draw CFL on the same line
            if (ModeC_STRING == null)
                ModeC_STRING = "---";
            CFL_OFFSET.X = ModeC_STRING.Length * (int)ModeC_FONT.Size;
            CFL_OFFSET.Y = LabelStartPosition.Y + LabelHeight;
            g.DrawString(CFL_STRING, CFL_FONT, CFL_BRUSH, LabelStartPosition.X + CFL_OFFSET.X, CFL_OFFSET.Y);
            CFL_START_X = LabelStartPosition.X + CFL_OFFSET.X;
            CFL_START_Y = CFL_OFFSET.Y;

            // Draw GSPD on the same line
            GSPD_OFFSET.X = (ModeC_STRING.Length * (int)ModeC_FONT.Size) + (CFL_STRING.Length * (int)CFL_FONT.Size);
            GSPD_OFFSET.Y = LabelStartPosition.Y + LabelHeight;
            g.DrawString(GSPD_STRING, GSPD_FONT, GSPD_BRUSH, LabelStartPosition.X + GSPD_OFFSET.X, GSPD_OFFSET.Y);
            GSPD_START_X = LabelStartPosition.X + GSPD_OFFSET.X;
            GSPD_START_Y = GSPD_OFFSET.Y;

            LabelHeight = LabelHeight + (int)GSPD_FONT.Size + SpacingIndex * 2;

            if (ShowLabelBox == true)
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //  DRAW Assigned HDG, SPD and ROC
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // HDG
                g.DrawString(A_HDG_STRING, A_HDG_FONT, A_HDG_BRUSH, LabelStartPosition.X + A_HDG_OFFSET.X, LabelStartPosition.Y + LabelHeight);
                HDG_START_X = LabelStartPosition.X + A_HDG_OFFSET.X;
                HDG_START_Y = LabelStartPosition.Y + LabelHeight;

                // SPD
                A_SPD_OFFSET.X = A_HDG_STRING.Length * (int)A_HDG_FONT.Size;
                A_SPD_OFFSET.Y = LabelStartPosition.Y + LabelHeight;
                g.DrawString(A_SPD_STRING, A_SPD_FONT, A_SPD_BRUSH, LabelStartPosition.X + A_SPD_OFFSET.X, A_SPD_OFFSET.Y);
                SPD_START_X = LabelStartPosition.X + A_SPD_OFFSET.X;
                SPD_START_Y = A_SPD_OFFSET.Y;

                // ROC
                //A_ROC_OFFSET.X = A_SPD_OFFSET.X + A_SPD_OFFSET.X + A_SPD_STRING.Length * (int)A_SPD_FONT.Size;
                //A_ROC_OFFSET.Y = LabelStartPosition.Y + LabelHeight;
                // g.DrawString(A_ROC_STRING, A_ROC_FONT, A_ROC_BRUSH, LabelStartPosition.X + A_ROC_OFFSET.X, A_ROC_OFFSET.Y);

                LabelHeight = LabelHeight + (int)A_SPD_FONT.Size + SpacingIndex * 2;

                // Add the final spacing index and draw the box
                LabelHeight = LabelHeight + SpacingIndex * 2;
                g.DrawRectangle(MyPen, LabelStartPosition.X, LabelStartPosition.Y, LabelWidth, LabelHeight);
            }

        }
    }

    public class WaypointMarker : GMap.NET.WindowsForms.GMapMarker
    {
        private string WPT_Name;
        private Font Font_To_Use;
        private Brush Brush_To_Use;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p">The position of the marker</param>
        public WaypointMarker(PointLatLng p, string WPT_Name_In, Font Font_To_Use_In, Brush Brush_To_Use_In)
            : base(p)
        {
            WPT_Name = WPT_Name_In;
            Font_To_Use = Font_To_Use_In;
            Brush_To_Use = Brush_To_Use_In;
        }

        public override void OnRender(Graphics g)
        {
            g.DrawString(WPT_Name, Font_To_Use, Brush_To_Use, LocalPosition.X, LocalPosition.Y);
        }
    }

    public class RngBrngMarker : GMap.NET.WindowsForms.GMapMarker
    {
        private string WPT_Name;
        private Font Font_To_Use;
        private Brush Brush_To_Use;
        Point StartPosition;
        Point EndPosition;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p">The position of the marker</param>
        public RngBrngMarker(PointLatLng p, string WPT_Name_In, Font Font_To_Use_In, Brush Brush_To_Use_In, Point S_Position, Point E_Position)
            : base(p)
        {
            WPT_Name = WPT_Name_In;
            Font_To_Use = Font_To_Use_In;
            Brush_To_Use = Brush_To_Use_In;
            StartPosition = S_Position;
            EndPosition = E_Position;
        }

        public override void OnRender(Graphics g)
        {
            g.DrawLine(new Pen(Brush_To_Use), StartPosition, EndPosition);

            // select a reference elllipsoid
            Ellipsoid reference = Ellipsoid.WGS84;
            // instantiate the calculator
            GeodeticCalculator geoCalc = new GeodeticCalculator();
            GlobalPosition Start = new GlobalPosition(new GlobalCoordinates(this.Position.Lat, this.Position.Lng));
            PointLatLng End_LatLng = FormMain.FromLocalToLatLng(EndPosition.X, EndPosition.Y);
            GlobalPosition End = new GlobalPosition(new GlobalCoordinates(End_LatLng.Lat, End_LatLng.Lng));
            GeodeticMeasurement GM = geoCalc.CalculateGeodeticMeasurement(reference, Start, End);

            // Now compute position half way between two points.        
            double distance = GM.PointToPointDistance / 2.0;
            if (distance > 0.0)
            {
                GlobalCoordinates GC = geoCalc.CalculateEndingGlobalCoordinates(reference, new GlobalCoordinates(this.Position.Lat, this.Position.Lng), GM.Azimuth, distance);
                GPoint GP = FormMain.FromLatLngToLocal(new PointLatLng(GC.Latitude.Degrees, GC.Longitude.Degrees));
                double Distane_NM = 0.00053996 * GM.PointToPointDistance;
                g.DrawString(Math.Round(GM.Azimuth.Degrees).ToString() + "°/" + Math.Round(Distane_NM, 1).ToString() + "nm", new Font(FontFamily.GenericSansSerif, 9), Brush_To_Use, new PointF(GP.X, GP.Y));
            }
        }
    }
}