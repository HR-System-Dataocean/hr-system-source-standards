<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Main.aspx.vb" Inherits="_Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../Common/Script/component.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div runat="server" id="tabs" class="tabs">
        <nav>
            <ul>
                <li><a href="#section-1">
                    <asp:Label ID="Label1" SkinID="Label_CopyRightsBold" runat="server" Text="User Profile"></asp:Label></a></li>
                <li><a href="#section-2">
                    <asp:Label ID="Label2" SkinID="Label_CopyRightsBold" runat="server" Text="Self Services"></asp:Label></a></li>
                <li><a href="#section-3">
                    <asp:Label ID="Label3" SkinID="Label_CopyRightsBold" runat="server" Text="WorkFlow Alerts"></asp:Label></a></li>
                <li><a href="#section-4">
                    <asp:Label ID="Label4" SkinID="Label_CopyRightsBold" runat="server" Text="WorkFlow Tracking"></asp:Label></a></li>
                <li><a href="#section-5">
                    <asp:Label ID="Label5" SkinID="Label_CopyRightsBold" runat="server" Text="Expiry Documents Alerts"></asp:Label></a></li>
                
            </ul>
        </nav>
        <div class="content">
            <section id="section-1">
                <iframe id="iframe1" src="UserProfile.aspx" width="100%" height="620px" marginheight="0"
                    frameborder="0"></iframe>
            </section>
            <section id="section-2">
                <iframe id="iframe2" src="SelfServcie.aspx" width="100%" height="620px" marginheight="0"
                    frameborder="0"></iframe>
            </section>
            <section id="section-3">
                <iframe id="iframe3" src="WFAlerts.aspx" width="100%" height="620px" marginheight="0"
                    frameborder="0"></iframe>
            </section>
            <section id="section-4">
                <iframe id="iframe4" src="WFTracing.aspx" width="100%" height="620px" marginheight="0"
                    frameborder="0"></iframe>
            </section>
            <section id="section-5">
                <iframe id="iframe5" src="EDAlerts.aspx" width="100%" height="620px" marginheight="0"
                    frameborder="0"></iframe>
            </section>
            <section id="section-6">
                <iframe id="iframe6" src="Tasks.aspx" width="100%" height="620px" marginheight="0"
                    frameborder="0"></iframe>
            </section>
        </div>
    </div>
    <script src="../../../Common/Script/JQuery/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../../../Common/Script/cbpFWTabs.js" type="text/javascript"></script>
    <script type="text/javascript">
        new CBPFWTabs(document.getElementById('tabs'))
        $(document).ready(function () {
            if (getParameterByName('PageIdx') != "") {
                new CBPFWTabs(document.getElementById('tabs'))._show(getParameterByName('PageIdx'));
            }
        });
        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }
    </script>
    </form>
</body>
</html>
