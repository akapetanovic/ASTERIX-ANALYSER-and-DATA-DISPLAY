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

public partial class Samples_SimpleMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
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

        //Add pushpins for map. 
        //This should be done with intialization of GooglePoint class. 
        //In constructor of GooglePoint, First argument is ID of this pushpin. It must be unique for each pin. Type is string.
        //Second and third arguments are latitude and longitude.
        GoogleMapForASPNet1.GoogleMapObject.Points.Add(new GooglePoint("1", 43.65669, -79.45278));
        GoogleMapForASPNet1.GoogleMapObject.Points.Add(new GooglePoint("2", 43.66619, -79.44268));
        GoogleMapForASPNet1.GoogleMapObject.Points.Add(new GooglePoint("3", 43.67689, -79.43270));

        GooglePoint GP = new GooglePoint();
        GP.ID = "1";
        GP.Latitude = 43.65669;
        GP.Longitude = -79.43270;
        GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP);
    }
}
