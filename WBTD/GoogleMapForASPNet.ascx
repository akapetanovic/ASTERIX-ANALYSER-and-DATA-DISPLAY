<%--//   Google Maps User Control for ASP.Net version 1.0:
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
--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoogleMapForASPNet.ascx.cs" Inherits="GoogleMapForASPNet" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:ScriptManagerProxy ID="ScriptManager1" runat="server">
 <Services>
    <asp:ServiceReference Path="~/GService.asmx" />
  </Services>
</asp:ScriptManagerProxy>
<div id="GoogleMap_Div_Container">
<div id="GoogleMap_Div" style="width:<%=GoogleMapObject.Width %>;height:<%=GoogleMapObject.Height %>;">

</div>
<%
if(ShowControls)
{
 %>
 
<input type="button" id="btnFullScreen" value="Full Screen"  onclick="ShowFullScreenMap();" />
&nbsp&nbsp
<input type="checkbox" id="chkIgnoreZero" onclick="IgnoreZeroLatLongs(this.checked);" />Ignore Zero Lat Longs
<% } %>
</div>
<div id="directions_canvas">

</div>
<asp:UpdatePanel ID="UpdatePanelXXXYYY" runat="server">
<ContentTemplate>
    <asp:HiddenField ID="hidEventName" runat="server" />
    <asp:HiddenField ID="hidEventValue" runat="server" />
</ContentTemplate>
</asp:UpdatePanel>

<script language="javascript" type="text/javascript">
    //RaiseEvent('MovePushpin','pushpin2');
function RaiseEvent(pEventName,pEventValue)
{
    document.getElementById('<%=hidEventName.ClientID %>').value = pEventName;
    document.getElementById('<%=hidEventValue.ClientID %>').value = pEventValue;
    if(document.getElementById('<%=UpdatePanelXXXYYY.ClientID %>') != null)
    {
        __doPostBack('<%=UpdatePanelXXXYYY.ClientID %>','');
    }
}

</script>
