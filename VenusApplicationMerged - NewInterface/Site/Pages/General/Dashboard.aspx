<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dashboard.aspx.vb" Inherits="Dashboard" %>

<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../Common/Script/JQuery/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/HChart/highcharts.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/HChart/exporting.js" type="text/javascript"></script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="form1" runat="server">
    <script language="javascript" type="text/javascript">
        $(window).load(function () {
            var isPostback = $("#<%=hdnIsPostback.ClientID%>").val().toLowerCase() === "true";
            if (!isPostback) {
                var newheight;
                var newwidth;
                newheight = $(window).height();
                newwidth = $(window).width();
                document.getElementById("HiddenField_H").value = newheight;
                document.getElementById("HiddenField_W").value = newwidth;
                __doPostBack('<%= LinkButton2.ClientID  %>', '');
            }
        })
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="display: none; height: 200px">
        <asp:LinkButton ID="LinkButton2" runat="server"></asp:LinkButton>
        <asp:HiddenField ID="HiddenField_H" runat="server" />
        <asp:HiddenField ID="HiddenField_W" runat="server" />
        <asp:HiddenField runat="server" ID="hdnIsPostback" />
    </div>
    <div runat="server" style="width: 100%; height: 100%; overflow: hidden;" id="DIV">
        <ig:WebTab ID="WebTab1" runat="server" Width="100%" Height="100%" StyleSetName="Office2007Blue"
            TabLocation="BottomRight" BorderStyle="None">
            <PostBackOptions EnableAjax="True" EnableLoadOnDemand="false" />
        </ig:WebTab>
    </div>
    </form>
</body>
</html>
