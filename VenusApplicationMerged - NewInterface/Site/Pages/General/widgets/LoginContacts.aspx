<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LoginContacts.aspx.vb"
    Inherits="LoginContacts" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>برنامج فينوس لشئون الموظفين === Venus Application For HR</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="Div_FormsContainer">
            <table align="center" style="height: 50%; width: 100%;vertical-align:top">
                <tr>
                    <td style="width: 5%"></td>
                    <td style="width: 95%">
                        <ig:WebDataTree ID="WebDataTree_DocumentsAlerts" runat="server" Height="100%" Width="100%"
                            EnableExpandImages="true" EnableExpandOnClick="True" Font-Bold="False" Font-Names="Tahoma"
                            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px">
                        </ig:WebDataTree>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
