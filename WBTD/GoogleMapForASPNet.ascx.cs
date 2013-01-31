//   Google Maps User Control for ASP.Net version 1.0:
//   ========================
//   Copyright (C) 2008  Shabdar Ghata 
//   Email : ghata2002@gmail.com
//   URL : http://www.shabdar.org

//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.

//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.

//   You should have received a copy of the GNU General Public License
//   along with this program.  If not, see <http://www.gnu.org/licenses/>.

//   This program comes with ABSOLUTELY NO WARRANTY.
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
using System.Text;
using System.Drawing;
using System.Web.Services;

public partial class GoogleMapForASPNet : System.Web.UI.UserControl
{

    public delegate void PushpinDragHandler(string pID);
    public delegate void PushpinClickedHandler(string pID);
    public delegate void MapClickedHandler(double dLatitude, double dLongitude);
    public delegate void ZoomChangedHandler(int pZoomLevel);
    public event PushpinDragHandler PushpinDrag;
    public event PushpinClickedHandler PushpinClicked;
    public event MapClickedHandler MapClicked;
    public event ZoomChangedHandler ZoomChanged;
    // The method which fires the Event

    public void OnPushpinDrag(string pID)
    {
        // Check if there are any Subscribers
        if (PushpinDrag != null)
        {
            // Call the Event
            GoogleMapObject = (GoogleObject)System.Web.HttpContext.Current.Session["GOOGLE_MAP_OBJECT"];
            PushpinDrag(pID);
        }
    }

    public void OnPushpinClicked(string pID)
    {
        // Check if there are any Subscribers
        if (PushpinClicked != null)
        {
            // Call the Event
            GoogleMapObject = (GoogleObject)System.Web.HttpContext.Current.Session["GOOGLE_MAP_OBJECT"];
            PushpinClicked(pID);
        }
    }

    public void OnMapClicked(double dLatitude,double dLongitude)
    {
        // Check if there are any Subscribers
        if (MapClicked != null)
        {
            // Call the Event
            GoogleMapObject = (GoogleObject)System.Web.HttpContext.Current.Session["GOOGLE_MAP_OBJECT"];
            MapClicked(dLatitude,dLongitude);
        }
    }

    public void OnZoomChanged(int pZoomLevel)
    {
        // Check if there are any Subscribers
        if (ZoomChanged != null)
        {
            // Call the Event
            GoogleMapObject = (GoogleObject)System.Web.HttpContext.Current.Session["GOOGLE_MAP_OBJECT"];
            ZoomChanged(pZoomLevel);
        }
    }

    #region Properties

    GoogleObject _googlemapobject = new GoogleObject();
    public GoogleObject GoogleMapObject
    {
        get { return _googlemapobject; }
        set { _googlemapobject = value; }
    }


    bool _showcontrols = false;
    public bool ShowControls
    {
        get { return _showcontrols; }
        set { _showcontrols = value; }
    }


    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        //Console.Write(hidEventName.Value);
        //Console.Write(hidEventValue.Value);
        //Fire event for Pushpin Move
        if (hidEventName.Value == "MapClicked")
        {
            string[] sLatLng = hidEventValue.Value.Split(new char[] {','}, StringSplitOptions.None);
            if (sLatLng.Length > 0)
            {
                double dLat = double.Parse(sLatLng[0]);
                double dLng = double.Parse(sLatLng[1]);
                //Set event name to blank string, so on next postback same event doesn't fire again.
                hidEventName.Value = "";
                OnMapClicked(dLat,dLng);
            }
        }
        if (hidEventName.Value == "PushpinClicked")
        {
            //Set event name to blank string, so on next postback same event doesn't fire again.
            hidEventName.Value = "";
            OnPushpinClicked(hidEventValue.Value);
        }
        if (hidEventName.Value == "PushpinDrag")
        {
            //Set event name to blank string, so on next postback same event doesn't fire again.
            hidEventName.Value = "";
            OnPushpinDrag(hidEventValue.Value);
        }
        if (hidEventName.Value == "ZoomChanged")
        {
            //Set event name to blank string, so on next postback same event doesn't fire again.
            hidEventName.Value = "";
            OnZoomChanged(int.Parse(hidEventValue.Value));
        }

        
        if (!IsPostBack)
        {
            Session["GOOGLE_MAP_OBJECT"] = GoogleMapObject;
        }
        else
        {
            GoogleMapObject = (GoogleObject)Session["GOOGLE_MAP_OBJECT"];
            if (GoogleMapObject == null)
            {
                GoogleMapObject = new GoogleObject();
                Session["GOOGLE_MAP_OBJECT"] = GoogleMapObject;
            }

        }
        string sScript = "<script src='http://maps.google.com/maps?file=api&amp;v="+GoogleMapObject.APIVersion+">&amp;key="+GoogleMapObject.APIKey+"'  type='text/javascript'></script>";
        sScript += "<script type='text/javascript' src='GoogleMapAPIWrapper.js'></script>";
        sScript+= "<script language='javascript'> if (window.DrawGoogleMap) { DrawGoogleMap(); } </script>";
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "onLoadCall", sScript);
    }
}
