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

public partial class Samples_PushpinClickEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Add event handler for PushpinMoved event
        GoogleMapForASPNet1.PushpinClicked += new GoogleMapForASPNet.PushpinClickedHandler(OnPushpinClicked);
        if (!IsPostBack)
        {

            //You must specify Google Map API Key for this component. You can obtain this key from http://code.google.com/apis/maps/signup.html
            //For samples to run properly, set GoogleAPIKey in Web.Config file.
            GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];

            //Specify width and height for map. You can specify either in pixels or in percentage relative to it's container.
            GoogleMapForASPNet1.GoogleMapObject.Width = "700px"; // You can also specify percentage(e.g. 80%) here
            GoogleMapForASPNet1.GoogleMapObject.Height = "400px";

            //Specify initial Zoom level.
            GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 14;

            //Specify Center Point for map. Map will be centered on this point.
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 43.66619, -79.44268);

            //Add push pins for map. 
            //This should be done with intialization of GooglePoint class. 
            //ID is to identify a pushpin. It must be unique for each pin. Type is string.
            //Other properties latitude and longitude.
            GooglePoint GP1 = new GooglePoint();
            GP1.ID = "FireTruck";
            GP1.Latitude = 43.65669;
            GP1.Longitude = -79.44268;
            //Specify bubble text here. You can use standard HTML tags here.
            GP1.InfoHTML = "This is a Fire Truck";
            GP1.Draggable = true;
            GP1.ToolTip = "Fire Truck";
            //Specify icon image. This should be relative to root folder.
            GP1.IconImage = "icons/FireTruck.png";
            GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);

            GooglePoint GP2 = new GooglePoint();
            GP2.ID = "SimplePushpin";
            GP2.Latitude = 43.66619;
            GP2.Longitude = -79.44268;
            GP2.InfoHTML = "This is a Simple pushpin";
            GP2.Draggable = true;
            GP2.ToolTip = "Simple Pushpin";
            GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP2);

            GooglePoint GP3 = new GooglePoint();
            GP3.ID = "Truck";
            GP3.Latitude = 43.67689;
            GP3.Longitude = -79.43270;
            GP3.InfoHTML = "This is a Truck";
            GP3.IconImage = "icons/Truck.png";
            GP3.Draggable = true;
            GP3.ToolTip = "Truck";
            GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP3);
        }
    }

     //Add event handler for PushpinMoved event
    void OnPushpinClicked(string pID)
    {
        //pID is ID of pushpin which was moved.
        lblLastPushpin.Text = pID;
        //Print all pushpin positions
        lblPushpin1.Text = "(" + GoogleMapForASPNet1.GoogleMapObject.Points[pID].Latitude.ToString() + "," + GoogleMapForASPNet1.GoogleMapObject.Points[pID].Longitude.ToString() + ")";
    }
}
