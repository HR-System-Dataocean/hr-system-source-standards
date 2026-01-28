<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmIncreasesLoad.aspx.vb"
    Inherits="Interfaces_frmIncreasesLoad" Culture="auto" UICulture="auto" EnableSessionState="True" %>

<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbar" Namespace="Infragistics.WebUI.UltraWebToolbar" Assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>* Venus Payroll * ~AttendanceLoad</title>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
        function CloseMe() {
            parent.CloseIt("");
        }
    </script>
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" style="width: 798px;
    height: 114px">
    <form id="frmAttendanceLoad" runat="server">
    <script type="text/javascript" id="Script1">
        $(function () {
            $('#<%= ImageButton.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
        });
    </script>
    <div style="width: 799px; height: 112px">
        &nbsp;
        <div align="left" style="border-style: outset; border-color: inherit; border-width: 0px;
            z-index: 102; left: 0px; width: 800px; position: absolute; top: 1px; height: 111px;
            background-color: #DCE8F6" id="DIV" runat="server">
            <table style="width: 100%; vertical-align: top" cellspacing="0">
                <tr>
                    <td style="height: 18px">
                        <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                            <tr>
                                <td style="width: 5px">
                                </td>
                                <td style="width: 40px; text-align: center;">
                                    <asp:ImageButton ID="ImageButton" runat="server" CommandArgument="Print" Height="12px"
                                        Width="12px" ImageUrl="~/Pages/HR/Img/save.gif" />
                                </td>
                                <td style="width: 5px">
                                    &nbsp;
                                </td>
                                <td style="width: 40px; text-align: center;">
                                    &nbsp;
                                </td>
                                <td style="width: 5px">
                                    &nbsp;
                                </td>
                                <td style="width: 200px">
                                    <asp:Label ID="lblProgress" runat="server" SkinID="Label_WarningBold"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:FileUpload meta:resourcekey="FileUpload1Recourcekey" ID="FileUpload1" runat="server"
                Style="z-index: 1; left: 122px; top: 38px; position: absolute; height: 22px;
                width: 577px; right: 101px; direction: rtl" />
            <asp:Label meta:resourcekey="Label2Recourcekey" ID="Label2" runat="server" Style="z-index: 1;
                left: 43px; top: 39px; position: absolute; height: 19px" Text="Select File"></asp:Label>
            <asp:Label meta:resourcekey="Label1Recourcekey" Visible="false" ID="Label4" runat="server"
                Style="z-index: 1; left: 39px; top: 84px; position: absolute; height: 19px" Text="Fiscal Period"></asp:Label>
            <asp:DropDownList ID="DdlPeriods" Visible="false" runat="server" Style="z-index: 1;
                left: 122px; top: 73px; position: absolute; width: 235px; margin-top: 0px" meta:resourcekey="DropDownList1Recourcekey">
            </asp:DropDownList>
        </div>
    </div>
    </form>
</body>
</html>
