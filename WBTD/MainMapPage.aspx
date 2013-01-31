<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainMapPage.aspx.cs" Inherits="MapWithAutoMovingPushpins" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Src="~/GoogleMapForASPNet.ascx" TagName="GoogleMapForASPNet" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            width: 518px;
            height: 80px;
        }
        .style3
        {
            width: 900px;
            height: 80px;
        }
        .style4
        {
            height: 85px;
            width: 290px;
        }
        .style9
        {
            width: 290px;
        }
        .style12
        {
            width: 238px;
        }
        .style13
        {
            height: 85px;
            width: 238px;
        }
        .style14
        {
            width: 221px;
        }
        .style15
        {
            height: 85px;
            width: 221px;
        }
    </style>
</head>
<body>
     <form id="form1" runat="server">
    <table>
        <tr>
            <td class="style3">
              
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

    <h3><a href="Default.aspx">Back</a></h3>
    <div>
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
        <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="4000" OnTick="Timer1_Tick">
            </asp:Timer>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   
            </td>
            <td class="style2">
                <table style="width: 300px; height: 200px; margin-left: 10px;" align="left" 
                    bgcolor="#999999" border="aaa" frame="border">
                    <tr>
                        <td class="style14">
                           
                            <asp:CheckBox ID="CheckBoxCustomMapEnabled" runat="server" 
                                oncheckedchanged="CheckBox1_CheckedChanged" Text="Custom Map" 
                                Font-Size="Small" />
                        </td>
                        <td class="style12">
                            aaaaa</td>
                        <td class="style9">
                            aaaaa</td>
                    </tr>
                    <tr>
                        <td class="style14">
                            <asp:Button ID="Button1" runat="server" Font-Size="Small" 
                                onclick="Button1_Click" 
                                style="z-index: 1; top: -10px; position: relative; right: -90px; width: 88px; left: 2px" 
                                Text="Update" />
                            <asp:TextBox ID="TextBox1" runat="server" Width="82px"></asp:TextBox>
                        </td>
                        <td class="style12">
                            aaaaa</td>
                        <td class="style9">
                            aaaaa</td>
                    </tr>
                    <tr>
                        <td class="style15">
                            aaaaa</td>
                        <td class="style13">
                            aaaaa</td>
                        <td class="style4">
                            aaaaa</td>
                    </tr>

                </table>
     
        </tr>
        </table>
         </form>
   
</body>
</html>
