<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCalendarExceptions.aspx.vb"
    Inherits="frmCalendarExceptions" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbar" Namespace="Infragistics.WebUI.UltraWebToolbar" Assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebCombo.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Sending Salary Notification</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
        function CloseMe() {
            parent.CloseIt("");
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmCalendarExceptions" runat="server" defaultbutton="ImageButton1">
    <div style="display: none">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
        <asp:Label ID="lblLage" runat="server" meta:resourcekey="lblLageResource1"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 18px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display: none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N"
                                    meta:resourcekey="ImageButton1Resource1" />
                            </td>
                            <td style="width: 20px">
                                <asp:ImageButton ID="ImageButton_Prepare" Width="14px" Height="12px" runat="server"
                                    CommandArgument="Prepare" meta:resourcekey="ImageButton_PrepareResource1" ImageUrl="~/Pages/HR/Img/save.gif" />
                            </td>
                            <td style="width: 100px">
                                <asp:LinkButton ID="LinkButton_Prepare" runat="server" Text="Update Data" CommandArgument="Prepare"
                                    meta:resourcekey="LinkButton_PrepareResource1" Style="font-family: Tahoma; font-size: 8pt;
                                    font-weight: Normal;"></asp:LinkButton>
                            </td>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td style="width: 10px;">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"
                                    Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;"></asp:LinkButton>
                            </td>
                            <td style="width: 40px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="Details">
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                        meta:resourcekey="UltraWebTab1Resource1">
                        <Tabs>
                            <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                    <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                        cellspacing="0">
                                        <tr>
                                            <td style="height: 10px" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblFromDate" runat="server" 
                                                                meta:resourcekey="lblBirthDateResource1" SkinID="Label_DefaultNormal" 
                                                                Style="height: 13px" Text="From Date" Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;<igtxt:WebMaskEdit ID="txtFromDate" runat="server" InputMask="##/##/####" 
                                                                            SkinID="WebMaskEdit_Default">
                                                                        </igtxt:WebMaskEdit>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                &nbsp;
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            &nbsp;
                                                            <asp:Label ID="lblToDate" runat="server" 
                                                                meta:resourcekey="lblBirthDate1Resource1" SkinID="Label_DefaultNormal" 
                                                                Style="height: 13px" Text="To Date" Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                         <igtxt:WebMaskEdit ID="txtToDate" runat="server" InputMask="##/##/####" 
                                                                SkinID="WebMaskEdit_Default">
                                                            </igtxt:WebMaskEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                &nbsp;</td>
                                            <td style="vertical-align: top" colspan="2" rowspan="2">
                                                         <table style="width: 100%;">
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:RadioButtonList ID="rbtnSelectedDateType" runat="server" meta:resourcekey="rbtnSelectedDateTypeResource1"
                                                                                Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black"
                                                                                Width="100%">
                                                                                <asp:ListItem meta:resourcekey="ListItemResource1" Text="الوقت الإفتراضي" Value="0"></asp:ListItem>
                                                                                <asp:ListItem meta:resourcekey="ListItemResource2" Text="لا يوجد عمل" Value="1"></asp:ListItem>
                                                                                <asp:ListItem meta:resourcekey="ListItemResource3" Text="تحديد الوقت" Value="2"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <asp:Label ID="lbl0" runat="server" meta:resourcekey="lbl0Resource1" SkinID="Label_DefaultNormal"
                                                                                Text="من :"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 50%;">
                                                                            <asp:Label ID="lbl1" runat="server" meta:resourcekey="lbl1Resource1" SkinID="Label_DefaultNormal"
                                                                                Text="إلى :"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit1" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" EnableAppStyling="False" meta:resourcekey="WebDateTimeEdit1Resource1"
                                                                                SkinID="WebDateTimeEdit_Default">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit1_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit2" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" meta:resourcekey="WebDateTimeEdit2Resource1"
                                                                                SkinID="WebDateTimeEdit_Default">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit3" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" meta:resourcekey="WebDateTimeEdit3Resource1"
                                                                                SkinID="WebDateTimeEdit_Default">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit4" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" meta:resourcekey="WebDateTimeEdit4Resource1"
                                                                                SkinID="WebDateTimeEdit_Default">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit5" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" meta:resourcekey="WebDateTimeEdit5Resource1"
                                                                                SkinID="WebDateTimeEdit_Default">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit6" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" meta:resourcekey="WebDateTimeEdit6Resource1"
                                                                                SkinID="WebDateTimeEdit_Default">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit7" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" meta:resourcekey="WebDateTimeEdit7Resource1"
                                                                                SkinID="WebDateTimeEdit_Default">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit8" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" meta:resourcekey="WebDateTimeEdit8Resource1"
                                                                                SkinID="WebDateTimeEdit_Default">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit9" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" meta:resourcekey="WebDateTimeEdit9Resource1"
                                                                                SkinID="WebDateTimeEdit_Default">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit10" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" meta:resourcekey="WebDateTimeEdit10Resource1"
                                                                                SkinID="WebDateTimeEdit_Default">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                <table style="width: 100%; height: 20px; vertical-align: bottom; border-bottom: 1px solid black"
                                                    cellspacing="6">
                                                    <tr>
                                                        <td style="vertical-align: bottom">

                                                            <asp:CheckBox ID="CheckBox1" runat="server" Text="Sat" />

                                                        </td>
                                                        <td style="vertical-align: bottom">

                                                            <asp:CheckBox ID="CheckBox2" runat="server" Text="Sun" />

                                                        </td>
                                                        <td style="vertical-align: bottom">

                                                            <asp:CheckBox ID="CheckBox3" runat="server" Text="Mon" />

                                                        </td>
                                                        <td style="vertical-align: bottom">

                                                            <asp:CheckBox ID="CheckBox4" runat="server" Text="Tue" />

                                                        </td>
                                                        <td style="vertical-align: bottom">

                                                            <asp:CheckBox ID="CheckBox5" runat="server" Text="Wed" />

                                                        </td>
                                                        <td style="vertical-align: bottom">

                                                            <asp:CheckBox ID="CheckBox6" runat="server" Text="Thu" />

                                                        </td>
                                                        <td style="vertical-align: bottom">

                                                            <asp:CheckBox ID="CheckBox7" runat="server" Text="Fri" />

                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%" colspan="3">
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                        </Tabs>
                    </igtab:UltraWebTab>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
