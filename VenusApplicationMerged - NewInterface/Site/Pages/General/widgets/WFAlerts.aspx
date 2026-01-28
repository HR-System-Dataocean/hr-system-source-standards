<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFAlerts.aspx.vb" Inherits="_WFAlerts" %>

<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <script src="../../../Common/Script/JQuery/jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
        function ctl00_WebDataTree_DocumentsAlerts_NodeClick(sender, eventArgs) {
            var node = eventArgs.getNode();
            var svalue = node.get_valueString();
            if (svalue != "") {
                $('#iframe0').attr('src', svalue);
            }
        }
        function updatemyPage() {
            setTimeout(function () {
                $('#iframe0').attr('src', '');
                __doPostBack("<%=UpdatePanel1.UniqueID %>", "");
            }, 500);
        }
    </script>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Text="" SkinID="Label_CopyRightsNormal"></asp:Label>
                <table style="width: 100%; height: 100%; vertical-align: top; border-bottom: 1px solid black"
                    cellspacing="0">
                    <tr>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 15%;">
                            <asp:RadioButton ID="RadioButton_Today" runat="server" SkinID="RadioButton_DefaultNormal"
                                AutoPostBack="True" GroupName="R1" />
                        </td>
                        <td style="width: 15%;">
                            <asp:RadioButton ID="RadioButton_Week" runat="server" SkinID="RadioButton_DefaultNormal"
                                AutoPostBack="True" GroupName="R1" Checked="True" />
                        </td>
                        <td style="width: 15%;">
                            <asp:RadioButton ID="RadioButton_Month" runat="server" SkinID="RadioButton_DefaultNormal"
                                AutoPostBack="True" GroupName="R1" />
                        </td>
                        <td style="width: 15%;">
                            <asp:RadioButton ID="RadioButton_All" runat="server" SkinID="RadioButton_DefaultNormal"
                                AutoPostBack="True" GroupName="R1" />
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
        <table style="width: 100%; height: 100%; vertical-align: top" cellspacing="0">
            <tr>
                <td style="max-width: 300px; vertical-align: top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Timer runat="server" ID="Timer1" Interval="600000" OnTick="Timer1_Tick" 
                                Enabled="false" />
                            <ig:WebDataTree ID="WebDataTree_DocumentsAlerts" runat="server" Height="100%" Width="280px"
                                EnableExpandImages="true" EnableExpandOnClick="True" Font-Bold="False" Font-Names="Tahoma"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" StyleSetName="Office2007Silver"
                                Font-Strikeout="False" Font-Underline="False" NodeIndent="20" SelectionType="Single">
                                <ClientEvents NodeClick="ctl00_WebDataTree_DocumentsAlerts_NodeClick" />
                            </ig:WebDataTree>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 100%; vertical-align: top">
                    <iframe id="iframe0" width="100%" height="600px" marginheight="0" frameborder="0">
                    </iframe>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
