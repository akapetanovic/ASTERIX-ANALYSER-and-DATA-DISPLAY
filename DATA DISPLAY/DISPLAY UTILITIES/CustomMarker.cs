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

    

}