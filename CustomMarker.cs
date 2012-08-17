using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using GMap.NET.WindowsForms;
using GMap.NET;

namespace MulticastingUDP
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
                Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);
            }

            public override void OnRender(Graphics g)
            {
                g.DrawImage(img, LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);
            }
        }
}
