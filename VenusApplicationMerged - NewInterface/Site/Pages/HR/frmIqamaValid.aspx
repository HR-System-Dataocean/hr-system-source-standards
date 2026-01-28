<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmIqamaValid.aspx.vb"
    Inherits="Interfaces_frmAttendanceLoad" Culture="auto" UICulture="auto" EnableSessionState="True" %>

<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbar" Namespace="Infragistics.WebUI.UltraWebToolbar" Assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>* Venus Payroll * ~AttendanceLoad</title>
    <link href="app_styles.css" type="text/css" rel="stylesheet" />
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
    <link href="~/ig_common/20071CLR20/Styles/Office2007Blue/ig_WebTab.css " rel="Stylesheet"
        type="text/css" />
    <style type="text/css">
        .igwgFrameBlue2k7
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
            background-color: Transparent;
            border: solid 1px #000000;
        }
        .igwgHdrBlue2k7
        {
            border-style: none;
            border-color: inherit;
            /*border-width: 0px;*/
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
            background-color: #D4D7DB;
            background-image: url('../ig_common/20071CLR20/Styles/Office2007Blue/WebGrid/igwgHeader.jpg');
            background-repeat: repeat-x;
            height: 23px;
            font-weight: normal;
            cursor: hand;
        }
        
        .igwgRowBlue2k7
        {
            border-top: 1px solid white;
            border-bottom: 1px solid #E3EFFF;
            font-size: 11px;
        }
        .igwgRowAltBlue2k7
        {
            border-top: 1px solid #F9F9F9;
            border-bottom: 1px solid #E3EFFF;
            background-color: #f9f9f9;
            font-size: 11px;
        }
    </style>
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" style="width: 798px;
    height: 260px">
    <form id="frmAttendanceLoad" runat="server">
    <script type="text/javascript" id="Script1">
        $(function () {
            $('#<%= Btn.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
        });
    </script>
    <div style="width: 799px; height: 260px">
        &nbsp;
        <div align="left" style="border-style: outset; border-color: inherit; border-width: 0px;
            z-index: 102; left: 0px; width: 800px; position: absolute; top: 1px; height: 260px;
            background-color: #DCE8F6" id="DIV" runat="server">
            <table style="width: 100%; vertical-align: top" cellspacing="0">
                <tr>
                    <td style="height: 18px">
                        <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                            <tr>
                                <td style="width: 5px">
                                </td>
                                <td style="width: 40px; text-align: center;">
                                  
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
         <table style="width:100%">
             <tr>
                 <td style="width:20%">
  <asp:Label  ID="Label2" runat="server" Text="ÑÞã ÇáåæíÉ"></asp:Label>
                 </td>
                 <td style="width:60%">
 <asp:TextBox ID="txtiqama" runat="server" Width="100%"></asp:TextBox>
                 </td>
                 <td style="width:20%">
                     <asp:Button ID="Btn" runat="server" Text="ÊÍÞÞ" />
                 </td>
             </tr>
         </table>
          
           
            
            
            
            
            
        </div>
    </div>
    </form>
</body>
</html>
