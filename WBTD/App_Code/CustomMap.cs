using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

/// <summary>
/// Summary description for CustomMap
/// </summary>
public class CustomMap
{
	public CustomMap()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static GooglePolygon GetBlankPoligon()
    {
        GooglePolygon BP = new GooglePolygon();

        GooglePoint GP1 = new GooglePoint();
        GP1.ID = "GP1";
        GP1.Latitude = 45.0;
        GP1.Longitude = 12.0;

        GooglePoint GP2 = new GooglePoint();
        GP2.ID = "GP2";
        GP2.Latitude = 45.0;
        GP2.Longitude = 22.0;

        GooglePoint GP3 = new GooglePoint();
        GP3.ID = "GP3";
        GP3.Latitude = 41.0;
        GP3.Longitude = 22.0;

        GooglePoint GP4 = new GooglePoint();
        GP4.ID = "GP4";
        GP4.Latitude = 41.0;
        GP4.Longitude = 12.0;

        BP.ID = "BLANK";
        //Give Hex code for line color
        Color color = Color.FromName(Color.Black.Name);
        string ColorCode = String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);

        BP.FillColor = ColorCode;
        BP.FillOpacity = 1;
        BP.StrokeColor = ColorCode;
        BP.StrokeOpacity = 1;
        BP.StrokeWeight = 1;

        BP.Points.Add(GP1);
        BP.Points.Add(GP2);
        BP.Points.Add(GP3);
        BP.Points.Add(GP4);

        return BP;
    }
}