<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAttendanceLoad.aspx.vb"
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
       <%-- $(function () {
            $('#<%= ImageButton.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
        });--%>
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
                                    <asp:ImageButton ID="ImageButton" runat="server" CommandArgument="Print" Height="12px"
                                        Width="12px" ImageUrl="~/Pages/HR/Img/save.gif" UseSubmitBehavior="false"/>
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
                                    <asp:Label ID="lblProgress" runat="server" SkinID="Label_WarningBold" Height="30px" Width="403px"></asp:Label>
                                </td>
                                  <td style="width: 200px">
                                    <asp:Label ID="lblCounter" runat="server" SkinID="Label_WarningBold" Height="30px" Width="403px"></asp:Label>
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
            <asp:Label meta:resourcekey="Label1Recourcekey" ID="Label1" runat="server" Style="z-index: 1;
                left: 20px; top: 68px; position: absolute; height: 19px" Text="File Extension"></asp:Label>
            
            <asp:Label  meta:resourcekey="Label3Recourcekey" ID="Label3" runat="server" Style="z-index: 1;
                left: 13px; top: 97px; position: absolute; height: 19px" Text="Loaded Project" Visible="False"></asp:Label>
             <asp:Label meta:resourcekey="Label4Recourcekey" ID="Label4" runat="server" Style="z-index: 1;
                left: 13px; top: 126px; position: absolute; height: 19px; right: 752px;" 
                Text="Month"></asp:Label>
             <asp:Label meta:resourcekey="Label5Recourcekey" ID="Label5" runat="server" Style="z-index: 1;
                left: 13px; top: 155px; position: absolute; height: 19px" Text="year"></asp:Label>
            <asp:DropDownList meta:resourcekey="DropDownList1Recourcekey" ID="ddlFileExtension"
                runat="server" Style="z-index: 1; left: 122px; top: 67px; position: absolute;
                width: 235px; margin-top: 0px" Enabled="False">
                <asp:ListItem Selected="True" Value="0">Excel Files</asp:ListItem>
                <asp:ListItem Value="1">CSV File</asp:ListItem>
                <asp:ListItem Value="2">NMK File</asp:ListItem>
                <asp:ListItem Value="3">Elkhiriji Excel</asp:ListItem>
                <asp:ListItem Value="4">Carlo</asp:ListItem>
                <asp:ListItem Value="5">ExcelTotal</asp:ListItem>
                <asp:ListItem Value="6">ExcelKenaaz</asp:ListItem>
                <asp:ListItem Value="7">ExcelTadawi</asp:ListItem>
                <asp:ListItem Value="8">ExcelByProject</asp:ListItem>
                <asp:ListItem Value="9">ExcelByProjectSSnNo</asp:ListItem>
                <asp:ListItem Value="10">ExcelByProjectandSSnNoAndNationlityAndDep</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList meta:resourcekey="DropDownList2Recourcekey" ID="DropDownList2"
                runat="server" Style="z-index: 1; left: 122px; top: 97px; position: absolute;
                width: 235px; margin-top: 0px" Visible="False">
            </asp:DropDownList>
            <asp:DropDownList meta:resourcekey="DropDownListMRecourcekey" ID="DropDownListM"
                runat="server" Style="z-index: 1; left: 122px; top: 126px; position: absolute;
                width: 235px; margin-top: 0px">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
                <asp:ListItem>9</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
               
            </asp:DropDownList>
            
            <asp:DropDownList meta:resourcekey="DropDownList3Recourcekey" ID="DropDownList3"
                runat="server" Style="z-index: 1; left: 122px; top:155px; position: absolute;
                width: 235px; margin-top: 0px; ">
                <asp:ListItem>2000</asp:ListItem>
                <asp:ListItem>2001</asp:ListItem>
                <asp:ListItem>2002</asp:ListItem>
                <asp:ListItem>2003</asp:ListItem>
                <asp:ListItem>2004</asp:ListItem>
                <asp:ListItem>2005</asp:ListItem>
                <asp:ListItem>2006</asp:ListItem>
                <asp:ListItem>2007</asp:ListItem>
                <asp:ListItem>2008</asp:ListItem>
                <asp:ListItem>2009</asp:ListItem>
                <asp:ListItem>2010</asp:ListItem>
                <asp:ListItem>2011</asp:ListItem>
                <asp:ListItem>2012</asp:ListItem>
                <asp:ListItem>2013</asp:ListItem>
                <asp:ListItem>2014</asp:ListItem>
                <asp:ListItem>2015</asp:ListItem>
                <asp:ListItem>2016</asp:ListItem>
                <asp:ListItem>2017</asp:ListItem>
                <asp:ListItem>2018</asp:ListItem>
                <asp:ListItem>2019</asp:ListItem>
                <asp:ListItem>2020</asp:ListItem>
                <asp:ListItem>2021</asp:ListItem>
                <asp:ListItem>2022</asp:ListItem>
                <asp:ListItem>2023</asp:ListItem>
                <asp:ListItem>2024</asp:ListItem>
                <asp:ListItem>2025</asp:ListItem>
                <asp:ListItem>2026</asp:ListItem>
                <asp:ListItem>2027</asp:ListItem>
                <asp:ListItem>2028</asp:ListItem>
                <asp:ListItem>2029</asp:ListItem>
                <asp:ListItem>2030</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    </form>
</body>
</html>
