using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ControlPerformanceTest : System.Web.UI.Page
{
    string[] sIcons = { "icons/sun.png", "icons/rain.png", "icons/snow.png", "icons/storm.png" };
    Random Rand = new Random();
    protected void Page_Load(object sender, EventArgs e)
    {
        //You must specify Google Map API Key for this component. You can obtain this key from http://code.google.com/apis/maps/signup.html
        //For samples to run properly, set GoogleAPIKey in Web.Config file.
        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"]; 

        //Specify width and height for map. You can specify either in pixels or in percentage relative to it's container.
        GoogleMapForASPNet1.GoogleMapObject.Width = "800px"; // You can also specify percentage(e.g. 80%) here
        GoogleMapForASPNet1.GoogleMapObject.Height = "600px";

        //Specify initial Zoom level.
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 5;

        //Specify Center Point for map. Map will be centered on this point.
        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 40.04443, -87.45117);

        //Load 500 icons
        for (int i = 0; i < 500; i++)
        {
            //Add pushpins for map. 
            GooglePoint GP = new GooglePoint();
            GP.ID = i.ToString();
            GP.Latitude =(double)cCommon.RandomNumber(30.0m,48.0m);
            GP.Longitude =(double)cCommon.RandomNumber(-100.0m,-70.0m);
            //Specify bubble text here. You can use standard HTML tags here.
            GP.InfoHTML = "This is Pushpin "+i.ToString();

            //Specify random icon image..
            GP.IconImage = sIcons[Rand.Next(0, 3)];
            GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP);
        }

        //Load vehicle pushpins
        GooglePoint GP1 = new GooglePoint();
        GP1.ID = "RedCar";
        GP1.Latitude = 32.65669;
        GP1.Longitude = -79.47268;  //+0.001
        //Specify bubble text here. You can use standard HTML tags here.
        GP1.InfoHTML = "This is Red car";

        //Specify icon image. This should be relative to root folder.
        GP1.IconImage = "icons/RedCar.png";
        GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);

        GooglePoint GP2 = new GooglePoint();
        GP2.ID = "YellowCar";
        GP2.Latitude = 45.63619; //+0.001
        GP2.Longitude = -85.44268;
        GP2.InfoHTML = "This is Yellow car";
        GP2.IconImage = "icons/YellowCar.png";
        GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP2);


        GooglePoint GP3 = new GooglePoint();
        GP3.ID = "SchoolBus";
        GP3.Latitude = 40.67689;
        GP3.Longitude = -95.43270;
        GP3.InfoHTML = "This is School Bus";
        GP3.IconImage = "icons/SchoolBus.png";
        GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP3);

    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        //Change pushpin icons randomly
        int n = Rand.Next(450);

        //Change 50 random pushpin icons
        for (int i = n; i < n + 50; i++)
        {
            GoogleMapForASPNet1.GoogleMapObject.Points[i].IconImage = sIcons[Rand.Next(0, 3)];
        }

        //Move vehicle icons
        GoogleMapForASPNet1.GoogleMapObject.Points["RedCar"].Latitude += 0.8;
        GoogleMapForASPNet1.GoogleMapObject.Points["RedCar"].Longitude -= 0.8;

        GoogleMapForASPNet1.GoogleMapObject.Points["SchoolBus"].Latitude -= 0.8;
        GoogleMapForASPNet1.GoogleMapObject.Points["SchoolBus"].Longitude += 0.8;

        GoogleMapForASPNet1.GoogleMapObject.Points["YellowCar"].Latitude -= 0.8;
        GoogleMapForASPNet1.GoogleMapObject.Points["YellowCar"].Longitude -= 0.8;
    }
}
