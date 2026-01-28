<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EDAlerts.aspx.vb" Inherits="_EDAlerts" %>

<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript" id="igClientScript">
        function ctl00_WebDataTree_DocumentsAlerts_NodeClick(sender, eventArgs) {
            var node = eventArgs.getNode();
            var svalue = node.get_valueString();
            if (svalue != "") {
                var winopen = window.open(svalue, "_blank", "height=450,width=700,left=0,top=0,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
                winopen.document.focus();
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Text="" SkinID="Label_CopyRightsNormal"></asp:Label>
                <table style="width: 100%; height: 100%; vertical-align: top; border-bottom: 1px solid black"
                    cellspacing="0">
                    <tr>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 15%;">
                        </td>
                        <td style="width: 15%;">
                        </td>
                        <td style="width: 15%;">
                        </td>
                        <td style="width: 15%;">
                        </td>
                        <td style="width: 15%;">
                        </td>
                        <td style="width: 15%;">
                            <asp:ImageButton ID="ImageButton_Refresh" runat="server" ImageUrl="~/Common/Images/refresh.png" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table align="center" cellspacing="1" cellpadding="1" class="MainContainerMaster">
            <tr>
                <td style="width: 100%; height: 180px; vertical-align: top;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Timer runat="server" ID="Timer4" Interval="300000" OnTick="Timer4_Tick" Enabled="false" />
                            <ig:WebDataTree ID="WebDataTree2" runat="server" Height="100%" Width="100%" EnableExpandImages="true"
                                EnableExpandOnClick="True" Font-Bold="False" Font-Names="Tahoma" BorderColor="#CCCCCC"
                                BorderStyle="Solid" BorderWidth="1px" StyleSetName="Office2007Silver" NodeIndent="20"
                                SelectionType="Single">
                                <ClientEvents NodeClick="ctl00_WebDataTree_DocumentsAlerts_NodeClick" />
                            </ig:WebDataTree>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
