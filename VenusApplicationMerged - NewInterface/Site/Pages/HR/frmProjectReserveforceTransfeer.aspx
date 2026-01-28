<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProjectReserveforceTransfeer.aspx.vb"
    Inherits="frmProjectReserveforceTransfeer" Culture="auto" UICulture="auto"  %>

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
    <title>* Venus Payroll * ~ frmProjectReserveforceTransfeer</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
        function CloseMe() {
            parent.CloseIt();
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmProjectReserveforceTransfeer" runat="server" defaultbutton="ImageButton1">
    <div style="display: none">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" 
            Width="99px" ></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" TabIndex="-1" 
            Width="99px" ></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" ></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" 
            Width="99px" ></asp:Label>
        <asp:Label ID="lblLage" runat="server" ></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 18px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display: none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" 
                                    CommandArgument="N"  />
                            </td>
                            <td style="width: 20px">
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
                                    SkinID="HrHelp_Command"  />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" Style="font-family: Tahoma;
                                    font-size: 8pt; font-weight: Normal;" 
                                    ></asp:LinkButton>
                            </td>
                            <td style="width: 40px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="Details">
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" 
                        SkinID="Default" >
                        <Tabs>
                            <igtab:Tab Text="عام" >
                                <ContentTemplate>
                                    <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                        cellspacing="0">
                                        <tr>
                                            <td style="height: 10px" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" colspan="3">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                                <tr>
                                                                    <td style="width: 5px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCode" runat="server" Width="80px" SkinID="Label_CopyRightsBold"
                                                                            Text="الكود" ></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblProjectCode" runat="server" Width="80px" 
                                                                            SkinID="Label_CopyRightsNormal" ></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblName" runat="server" SkinID="Label_CopyRightsBold" Text="التوصيف"
                                                                            Width="80px" ></asp:Label>
                                                                    </td>
                                                                    <td style="width: 40%;">
                                                                        <asp:Label ID="lblProjectName" runat="server" Width="100%" 
                                                                            SkinID="Label_CopyRightsNormal" ></asp:Label>
                                                                    </td>
                                                                    <td style="width: 60%">
                                                                        <asp:Label ID="Msg" runat="server" SkinID="Label_WarningBold" 
                                                                            ></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_StartDate" runat="server" Text="تاريخ التنقل" 
                                                                SkinID="Label_DefaultNormal" ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtStartDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix"
                                                                AutoPostBack="True" >
                                                            </igtxt:WebMaskEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="lblProject0" runat="server" SkinID="Label_DefaultNormal" Text="الرقم المرجعى"
                                                                Width="90px" ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtrefno" runat="server" MaxLength="255" 
                                                                SkinID="TextBox_LargeNormalltr" ></asp:TextBox>
                                                        </td>
                                                        <td class="SeparArea">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtrefno"
                                                                ErrorMessage="*" SetFocusOnError="True" ValidationGroup="G" 
                                                                ></asp:RequiredFieldValidator>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                    cellspacing="6">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label1" runat="server" Text="معلومات التطبيق" 
                                                                SkinID="Label_DefaultBold" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" class="style1">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblProject" runat="server" SkinID="Label_DefaultNormal" Text="إختر المشروع"
                                                                Width="90px" ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlProject" runat="server" SkinID="DropDownList_LargNormal"
                                                                AutoPostBack="True" >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top" class="style2">
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlProject"
                                                    ErrorMessage="*" Operator="NotEqual" ValidationGroup="G" 
                                                    ValueToCompare="0" ></asp:CompareValidator>
                                                &nbsp;
                                            </td>
                                            <td style="vertical-align: top" class="style1">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblLocations" runat="server" SkinID="Label_DefaultNormal" Text="إختر الموقع"
                                                                Width="90px" ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlLocation" runat="server" SkinID="DropDownList_LargNormal"
                                                                AutoPostBack="True" >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlLocation"
                                                    ErrorMessage="*" Operator="NotEqual" ValidationGroup="G" 
                                                    ValueToCompare="0" ></asp:CompareValidator>
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label_Shift" runat="server" SkinID="Label_DefaultNormal" Text="إختر الدوام"
                                                                Width="90px" ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DropDownList_Shift" runat="server" 
                                                                SkinID="DropDownList_LargNormal" >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="DropDownList_Shift"
                                                    ErrorMessage="*" Operator="NotEqual" ValidationGroup="G" 
                                                    ValueToCompare="0" ></asp:CompareValidator>
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <asp:LinkButton ID="LinkButton_Transfeer" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="إعتماد حركة التنقل" ValidationGroup="G" 
                                                    ></asp:LinkButton>
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
