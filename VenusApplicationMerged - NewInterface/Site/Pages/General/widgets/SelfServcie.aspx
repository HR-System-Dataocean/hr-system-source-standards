<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelfServcie.aspx.vb" Inherits="_SelfServcie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../Common/Script/JQuery/jquery-1.9.1.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript" id="igClientScript">
        function Show(LinkUrl) {
            if (LinkUrl != "") {
                $('#iframe0').attr('src', LinkUrl);
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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%; height: 100%; vertical-align: top" cellspacing="0">
                    <tr>
                        <td style="min-width: 150px; vertical-align: top">
                            <OfficeWebUI:Manager ID="Manager1" runat="server" ChromeUI="true" DirectionMode="RTL"
                                IncludeJQuery="false" UITheme="Office2010Silver" CustomCSSFile="">
                            </OfficeWebUI:Manager>
                            <OfficeWebUI:OfficeListView runat="server" ID="ListView1" DisplayMode="List" Width="130px"
                                Height="620px">
                            </OfficeWebUI:OfficeListView>
                        </td>
                        <td style="width: 100%; vertical-align: top">
                            <iframe id="iframe0" width="100%" height="600px" marginheight="0" frameborder="0">
                            </iframe>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
