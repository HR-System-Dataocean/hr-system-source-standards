<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeesVacationSettlementDependonVacation.aspx.vb"
    Inherits="frmEmployeesVacationTransactions" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebListbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebListbar" TagPrefix="iglbar" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Employees Vacation Transactions</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function OpenPrintedScreen(v) {
            var hight = window.screen.availHeight - 35;
            var width = window.screen.availWidth - 10;
            var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=EmployeeTransactionID&preview=1&ReportCode=VacSlip&sq0=''&v=" + v, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            win.focus();
        }
        function btnPrint() {
            var hdnEmpTrans = window.document.getElementById("hdnEmpTrans")
            if (parseInt(hdnEmpTrans.value) > 0) {
                OpenPrintedScreen(parseInt(hdnEmpTrans.value));
            }
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmEmployeesVacationTransactions" runat="server">
        <script type="text/javascript" id="Script1">
            $(function () {
                $('#<%= ButtonLoad.ClientID%>').click(function () {
                    $.blockUI({ message: '' });
                });
            });
            function dopostback() {
                var clickButton = document.getElementById("<%= ButtonLoad.ClientID %>");
                clickButton.click();
                __doPostBack('<%= ButtonLoad.UniqueID %>', '');
            }
        </script>
        <div style="display: none">
            <asp:HiddenField ID="txtEmployeeId" runat="server" Value="0" />
            <asp:HiddenField ID="hdnEmpTrans" runat="server" Value="0" />
            <asp:HiddenField ID="RemaningOPenBalanceDays" runat="server" Value="0" />
            <asp:HiddenField ID="OpenBalanceId" runat="server" Value="0" />
            <asp:HiddenField ID="SettlementForTotalDays" runat="server" />
            <asp:HiddenField ID="SettlementForIsertedDays" runat="server" />


            <asp:Button ID="ButtonLoad" runat="server" Text="Button" />
        </div>
        <div class="Div_MasterContainer" runat="server" id="DIV">
            <table align="center" style="width: 100%;">
                <tr>
                    <td class="Details">
                        <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                            meta:resourcekey="UltraWebTab1Resource1">
                            <Tabs>
                                <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                    <ContentTemplate>
                                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                                            <tr>
                                                <td style="height: 18px">
                                                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                        <tr>
                                                            <td style="width: 5px;"></td>
                                                            <td style="width: 40px; text-align: center;">
                                                                <igtxt:WebImageButton ID="btnSave" runat="server" Style="cursor: pointer;" Height="18px"
                                                                    Overflow="NoWordWrap" UseBrowserDefaults="False" CommandName="Save" Width="24px"
                                                                    meta:resourcekey="btnSaveResource1">
                                                                    <Alignments TextImage="ImageBottom" />
                                                                    <Appearance>
                                                                        <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/SaveN.png" />
                                                                    </Appearance>
                                                                </igtxt:WebImageButton>
                                                            </td>
                                                            <td style="width: 5px">
                                                                <asp:Label ID="Label_TSP3" runat="server" meta:resourcekey="Label_TSP1Resource1"
                                                                    Text="|"></asp:Label>
                                                            </td>
                                                            <td style="width: 40px; text-align: center;">
                                                                <igtxt:WebImageButton ID="btnDelete" runat="server" Height="18px" CommandName="Refund"
                                                                    meta:resourcekey="btnDeleteResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                    Width="24px">

                                                                    <Alignments TextImage="ImageBottom" />
                                                                    <Appearance>
                                                                        <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/Delete.png" />
                                                                    </Appearance>
                                                                </igtxt:WebImageButton>
                                                            </td>
                                                            <td style="width: 5px">
                                                                <asp:Label ID="Label7" runat="server" meta:resourcekey="Label_TSP1Resource1" Text="|"></asp:Label>
                                                            </td>
                                                            <td style="width: 40px; text-align: center;">
                                                                <igtxt:WebImageButton ID="btnPrint" runat="server" Height="18px" meta:resourcekey="btnPrintResource1"
                                                                    Overflow="WordWrap" UseBrowserDefaults="False" Width="24px">
                                                                    <ClientSideEvents Click="btnPrint()" />
                                                                    <Alignments TextImage="ImageBottom" />
                                                                    <Appearance>
                                                                        <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/Print.png" />
                                                                    </Appearance>
                                                                </igtxt:WebImageButton>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 18px">
                                                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                        <tr>
                                                            <td style="width: 5px"></td>
                                                            <td style="width: 40px">
                                                                <asp:Label ID="lblUser" runat="server" Width="40px" SkinID="Label_CopyRightsBold"
                                                                    Text="Code" meta:resourcekey="lblUserResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 80px">
                                                                <asp:Label ID="txtCode" runat="server" Width="80px" SkinID="Label_CopyRightsNormal"
                                                                    meta:resourcekey="lblDescEmployeeCodeResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 40px">
                                                                <asp:Label ID="lblName" runat="server" SkinID="Label_CopyRightsBold" Text="Name"
                                                                    Width="40px" meta:resourcekey="lblNameResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblEmployeeName" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblDescEnglishNameResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 80px">&nbsp;
                                                            </td>
                                                            <td style="width: 120px"></td>
                                                            <td style="width: 50px"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%; height: 100%; vertical-align: top" cellspacing="0">
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="labelLastPaymentDate" runat="server" SkinID="Label_DefaultNormal" Text="Last Payment Date"
                                                                    Width="90px" meta:resourcekey="labelLastPaymentDateResource" ></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebDateTimeEdit ID="textLastPaymentDate" runat="server" AutoPostBack="True" meta:resourcekey="textLastPaymentDateResource"
                                                                    ReadOnly="True" SkinID="WebDateTimeEdit_Fix">
                                                                    <ClientSideEvents TextChanged="wdcDate_TextChanged" />
                                                                </igtxt:WebDateTimeEdit>
                                                                &nbsp;<asp:Label ID="gLastPaymentDate" runat="server" meta:resourcekey="lblGergResource1"
                                                                    SkinID="Label_CopyRightsBold" Text="(Gerg)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="labelLastPaymentDateH" Width="90px" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Payment Date" meta:resourcekey="labelLastPaymentDateHResource"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebMaskEdit ID="textLastPaymentDateH" runat="server" AutoPostBack="True" DataMode="RawText"
                                                                    ReadOnly="True" InputMask="##/##/####" meta:resourcekey="textLastPaymentDateHResource" SkinID="WebMaskEdit_Fix">
                                                                    <ClientSideEvents TextChanged="wdcHDate_TextChanged" />
                                                                </igtxt:WebMaskEdit>
                                                                &nbsp;<asp:Label ID="hLastPaymentDate" runat="server" meta:resourcekey="lblHijriResource1"
                                                                    SkinID="Label_CopyRightsBold" Text="(Hijri)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="labelPaymentDate" runat="server" SkinID="Label_DefaultNormal" Text="Payment Date"
                                                                    Width="90px" meta:resourcekey="labelPaymentDateResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebDateTimeEdit ID="wdtPaymentDate" runat="server" AutoPostBack="True" meta:resourcekey="wdtPaymentDateResource1"
                                                                    SkinID="WebDateTimeEdit_Fix" ReadOnly="true">
                                                                    <ClientSideEvents TextChanged="wdcDate_TextChanged" />
                                                                </igtxt:WebDateTimeEdit>
                                                                &nbsp;<asp:Label ID="lblHijri2" runat="server" meta:resourcekey="lblGergResource1"
                                                                    SkinID="Label_CopyRightsBold" Text="(Gerg)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="labelHPaymentDate" Width="90px" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Payment Date" meta:resourcekey="labelHPaymentDateResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebMaskEdit ID="wdtHPaymentDate" runat="server" AutoPostBack="True" DataMode="RawText"
                                                                    ReadOnly="True" InputMask="##/##/####" meta:resourcekey="wdtHPaymentDateResource1" SkinID="WebMaskEdit_Fix">
                                                                    <ClientSideEvents TextChanged="wdcHDate_TextChanged" />
                                                                </igtxt:WebMaskEdit>
                                                                &nbsp;<asp:Label ID="lblHijri" runat="server" meta:resourcekey="lblHijriResource1"
                                                                    SkinID="Label_CopyRightsBold" Text="(Hijri)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="labelTotalOfUnPaidDays" Width="90px" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="Total of un Paid Days" Visible ="false" meta:resourcekey="labelTotalOfUnPaidDaysResource"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtTotalOfUnPaidDays" runat="server" SkinID="WebNumericEdit_Fix" MinDecimalPlaces="Two"
                                                                 Nullable="False" Visible ="false" ReadOnly="true" MaxLength="5" meta:resourcekey="WebNumericEditTotalOfUnPaidDaysResource">
                                                                <ClientSideEvents ValueChange="dopostback()" />
                                                            </igtxt:WebNumericEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="labelTotalOfUnPaidsalary" Width="90px" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="Total of un paid salary" Visible ="false" meta:resourcekey="labelTotalOfUnPaidsalaryResource"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtTotalOfUnPaidsalary" runat="server" MinValue="0" Nullable="False"
                                                                ReadOnly="True" Visible ="false" SkinID="WebNumericEdit_Fix" meta:resourcekey="lblNetSalaryResource1">
                                                            </igtxt:WebNumericEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>--%>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="labelPreparedDays" Width="90px" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Deserve Days" meta:resourcekey="labelPreparedDaysResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="txtPreparedDays" runat="server" SkinID="WebNumericEdit_Fix" MinDecimalPlaces="Two"
                                                                    ReadOnly="true" Nullable="False" MaxLength="5" meta:resourcekey="txtPreparedDaysResource1">
                                                                    <ClientSideEvents ValueChange="dopostback()" />
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <%--<td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="OldSettlementBalLabel" Width="90px" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Old Settlement Balance" meta:resourcekey="OldSettlementBalLabelResource"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="OldSettlementBalText" runat="server" MinValue="0" Nullable="False"
                                                                    ReadOnly="True" SkinID="WebNumericEdit_Fix" meta:resourcekey="lblNetSalaryResource1">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>--%>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="labelNetSalary" Width="90px" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Vacation Salary" meta:resourcekey="labelNetSalaryResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="lblNetSalary" runat="server" Nullable="False"
                                                                    ReadOnly="True" SkinID="WebNumericEdit_Fix" meta:resourcekey="lblNetSalaryResource1">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="SettlementDaysLabel" Width="90px" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Settlement Days" meta:resourcekey="SettlementDaysLabelResource"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="SettlementDaysText" runat="server" SkinID="WebNumericEdit_Fix" MinDecimalPlaces="Two"
                                                                    Nullable="False" MaxLength="5" meta:resourcekey="txtPreparedDaysResource1" ReadOnly="true">
                                                                    <ClientSideEvents ValueChange="dopostback()" />
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="labelVacNetSal" Width="90px" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Vacation Net Salary" meta:resourcekey="labelVacNetSalResource"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="txtVacNetSal" runat="server" Nullable="False"
                                                                    ReadOnly="True" SkinID="WebNumericEdit_Fix" meta:resourcekey="lbltxtVacNetSalResource">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                <table style="width: 100%; vertical-align: top; border-bottom: 1px solid black" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1" SkinID="Label_DefaultNormal"
                                                                Text="الفترة المالية"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DdlPeriodsForSalary" runat="server" Enabled="False" SkinID="DropDownList_LargNormal">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="SeparArea" style="height: 16px">
                                                        </td>
                                                        <td class="LabelArea" colspan="2" style="height: 16px">
                                                            <asp:Label ID="label_Header" runat="server" SkinID="Label_DefaultBold" Text="تدفع مستحقات الإجازة مع تجهيز الرواتب"
                                                                meta:resourcekey="label_HeaderResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblIsWithSalary" meta:resourcekey="lblIsWithSalaryResource1" runat="server"
                                                                SkinID="Label_DefaultNormal" Text="تدفع المستحقات مع الراتب"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="chkWithSalary" runat="server" Enabled="False" AutoPostBack="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table style="width: 100%; vertical-align: top; border-bottom: 1px solid black" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea" colspan="2">
                                                            <asp:Label ID="label4" runat="server" SkinID="Label_DefaultBold" Text="تصفية مستحقات الراتب مع الإجازة"
                                                                meta:resourcekey="label4_HeaderResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label_SalaryPayment" runat="server" meta:resourcekey="Label_SalaryPaymentResource1"
                                                                SkinID="Label_DefaultNormal" Text="اضافة مستحقات الراتب"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="CheckBox_SalaryPayment" runat="server" Enabled="False" AutoPostBack="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lbWorkingDays" runat="server" SkinID="Label_DefaultNormal" Text="Working Days"
                                                                Width="100px" meta:resourcekey="lbWorkingDaysResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblWorkingDays" runat="server" ReadOnly="True" SkinID="TextBox_SmalltNormalC"
                                                                meta:resourcekey="lblWorkingDaysResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label_Salary" runat="server" SkinID="Label_DefaultNormal" Text="Salary Amount"
                                                                Width="100px" meta:resourcekey="Label_SalaryResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="TextBox_SalaryAmount" runat="server" ReadOnly="True" SkinID="TextBox_SmalltNormalC"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td class="SeparArea">
                                                    </td>
                                                      <td class="LabelArea">
                                                          <!-- <asp:Label ID="Label5" runat="server" SkinID="Label_DefaultNormal" Text="Add Travels Days"
                                                                Width="100px" ></asp:Label>-->
                                                        </td>
                                                        <td class="DataArea">
                                                         <!--  <asp:CheckBox ID="ChkAddTravalsDay" runat="server" AutoPostBack="True" />-->
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: top;">
                                            </td>
                                            <td style="width: 47%; vertical-align: top;">
                                            <table style="width:100%"><tr>
                                            <td class="LabelArea">
                                            <asp:Label ID="lblVactionLoan" runat="server" meta:resourcekey="Label_lblVactionLoanResource1"
                                                                SkinID="Label_DefaultNormal" Text="تخصم السلفة من مستحقات الاجازة" Visible="False"></asp:Label>
                                            </td>
                                            <td>  <asp:CheckBox ID="CheckVactionLaon"
                                                    runat="server" AutoPostBack="True" Visible="False" /></td>
                                            </tr></table>

                                                





                                                <table style="width:100%"><tr>
                                                    <td class="SeparArea">
                                                        </td>
                                            <td class="LabelArea">
                                           <asp:Label ID="lbVactionTotalDays" runat="server" SkinID="Label_DefaultNormal" Text="Vacation total days"
                                                                Width="100px" meta:resourcekey="lbVactionTotalDaysResource1" Height="16px"></asp:Label>
                                            </td>
                                            <td> 
                                                 
                                                  <asp:TextBox ID="txtVactionTotalDays" runat="server" ReadOnly="True" SkinID="TextBox_SmalltNormalC"
                                                                meta:resourcekey="txtVactionTotalDaysResource1"></asp:TextBox>
                                            </td>
                                            </tr>

                                                </table>




                                                <table style="width:100%"><tr>
                                                    <td class="SeparArea">
                                                        </td>
                                            <td class="LabelArea">
                                            <asp:Label ID="lblVactionExceededDays" runat="server" SkinID="Label_DefaultNormal" Text="exceeded days"
                                                                Width="100px" meta:resourcekey="lblVactionExceededDaysResource1" Height="16px"></asp:Label>
                                            </td>
                                            <td> 
                                                 
                                                  <asp:TextBox ID="txtVactionExceededDays" runat="server" ReadOnly="True" SkinID="TextBox_SmalltNormalC"
                                                                meta:resourcekey="txtVactionExceededDaysResource1"></asp:TextBox>
                                            </td>
                                            </tr>

                                                </table>

                                                      <table style="width:100%"><tr>
                                                    <td class="SeparArea">
                                                        </td>
                                            <td class="LabelArea">
                                           <asp:Label ID="lblVactionNetDays" runat="server" SkinID="Label_DefaultNormal" Text="Net days"
                                                                Width="100px" meta:resourcekey="lblVactionNetDaysResource1" Height="16px"></asp:Label>
                                            </td>
                                            <td> 
                                                 
                                                 <asp:TextBox ID="txtVactionNetDays" runat="server" ReadOnly="True" SkinID="TextBox_SmalltNormalC"
                                                                meta:resourcekey="txtVactionNetDaysResource1"></asp:TextBox>
                                            </td>
                                            </tr>

                                                </table>



                                            
                                            </td>
                                        </tr>
                                            <tr>
                                                <td style="width: 47%; height: 100%; vertical-align: top;">&nbsp;<asp:Label ID="Label_Title1" runat="server" meta:resourcekey="Label_Title1Resource1"
                                                    SkinID="Label_DefaultBold" Text="Current Employee Benifits"></asp:Label>
                                                    <igtbl:UltraWebGrid  Browser="UpLevel"  ID="uwgEmployeeTransaction" runat="server" EnableAppStyling="True"
                                                        Height="100%" meta:resourcekey="uwgEmployeeTransactionResource1" SkinID="Default"
                                                        Width="100%">
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                            BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                            RowHeightDefault="18px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                Width="100%">
                                                            </FrameStyle>
                                                            <Pager MinimumPagesForDisplay="2">
                                                                <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                </PagerStyle>
                                                            </Pager>
                                                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                            </EditCellStyleDefault>
                                                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                            </FooterStyleDefault>
                                                            <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                                                                Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                            </HeaderStyleDefault>
                                                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                Font-Names="tahoma" Font-Size="8pt" Height="18px">
                                                                <Padding Left="3px" />
                                                                <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                                            </RowStyleDefault>
                                                            <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                                            </GroupByRowStyleDefault>
                                                            <GroupByBox Hidden="True">
                                                                <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                                </BoxStyle>
                                                            </GroupByBox>
                                                            <AddNewBox>
                                                                <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                </BoxStyle>
                                                            </AddNewBox>
                                                            <ActivationObject BorderColor="" BorderWidth="">
                                                            </ActivationObject>
                                                            <FilterOptionsDefault>
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px" Height="300px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                    BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px">
                                                                    <Padding Left="2px" />
                                                                </FilterOperandDropDownStyle>
                                                            </FilterOptionsDefault>
                                                        </DisplayLayout>
                                                        <Bands>
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource3">
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                                <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                    </FilterHighlightRowStyle>
                                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                        Font-Size="11px" Width="200px">
                                                                        <Padding Left="2px" />
                                                                    </FilterDropDownStyle>
                                                                </FilterOptions>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn AllowGroupBy="No" AllowUpdate="No" BaseColumnName="TransactionTypeID"
                                                                        Key="TransactionTypeID" meta:resourcekey="UltraGridColumnResource11" Type="DropDownList"
                                                                        Width="45%">
                                                                        <Header Caption="Benifits">
                                                                        </Header>
                                                                        <CellStyle CssClass="DescriptionCellgrid">
                                                                        </CellStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Amount" DataType="System.Double"
                                                                        Format="###,###,###.##" Key="Value" meta:resourcekey="UltraGridColumnResource12"
                                                                        Width="20%">
                                                                        <Header Caption="Amount">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Description" Key="Description"
                                                                        meta:resourcekey="UltraGridColumnResource13" Width="35%">
                                                                        <Header Caption="Description">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="TransactionTypeID" Hidden="True"
                                                                        Key="Transaction Type" meta:resourcekey="UltraGridColumnResource14" Width="0px">
                                                                        <Header Caption="Transaction Type">
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="EmpSchID" Hidden="True" Key="ID" meta:resourcekey="UltraGridColumnResource15"
                                                                        Width="0px">
                                                                        <Header>
                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="DescriptionSign" Hidden="True" Key="DescriptionSign"
                                                                        meta:resourcekey="UltraGridColumnResource16">
                                                                        <Header>
                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                </Columns>
                                                            </igtbl:UltraGridBand>
                                                        </Bands>
                                                    </igtbl:UltraWebGrid>
                                                </td>
                                                <td style="width: 6%; vertical-align: top;">&nbsp;
                                                </td>
                                                <td style="width: 47%; vertical-align: top;">
                                                    <asp:Label ID="Label1" runat="server" Text="Current Employee Deduction" SkinID="Label_DefaultBold"
                                                        meta:resourcekey="Label1Resource1"></asp:Label>
                                                    <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgPayabilities" runat="server" EnableAppStyling="True" Height="100%"
                                                        meta:resourcekey="uwgPayabilitiesResource1" SkinID="Default" Width="100%">
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                            BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                            RowHeightDefault="18px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                Width="100%">
                                                            </FrameStyle>
                                                            <Pager MinimumPagesForDisplay="2">
                                                                <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                </PagerStyle>
                                                            </Pager>
                                                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                            </EditCellStyleDefault>
                                                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                            </FooterStyleDefault>
                                                            <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                                                                Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                            </HeaderStyleDefault>
                                                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                Font-Names="tahoma" Font-Size="8pt" Height="18px">
                                                                <Padding Left="3px" />
                                                                <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                                            </RowStyleDefault>
                                                            <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                                            </GroupByRowStyleDefault>
                                                            <GroupByBox Hidden="True">
                                                                <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                                </BoxStyle>
                                                            </GroupByBox>
                                                            <AddNewBox>
                                                                <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                </BoxStyle>
                                                            </AddNewBox>
                                                            <ActivationObject BorderColor="" BorderWidth="">
                                                            </ActivationObject>
                                                            <FilterOptionsDefault>
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px" Height="300px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                    BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px">
                                                                    <Padding Left="2px" />
                                                                </FilterOperandDropDownStyle>
                                                            </FilterOptionsDefault>
                                                        </DisplayLayout>
                                                        <Bands>
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource4">
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn AllowGroupBy="No" AllowUpdate="No" BaseColumnName="TransactionTypeID"
                                                                        Key="TransactionTypeID" meta:resourcekey="UltraGridColumnResource17" Type="DropDownList"
                                                                        Width="45%">
                                                                        <Header Caption="Deduction">
                                                                        </Header>
                                                                        <CellStyle CssClass="DescriptionCellgrid">
                                                                        </CellStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Amount" DataType="System.Double"
                                                                        Format="###,###,###.##" Key="Value" meta:resourcekey="UltraGridColumnResource18"
                                                                        Width="20%">
                                                                        <Header Caption="Amount">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Description" Key="Description"
                                                                        meta:resourcekey="UltraGridColumnResource19" Width="35%">
                                                                        <Header Caption="Description">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn Hidden="True" Key="TransactionTypeID" meta:resourcekey="UltraGridColumnResource20"
                                                                        Width="0px">
                                                                        <Header>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="EmpSchID" Hidden="True" Key="EmpSchID" meta:resourcekey="UltraGridColumnResource21"
                                                                        Width="0px">
                                                                        <Header>
                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="DescriptionSign" Hidden="True" Key="DescriptionSign"
                                                                        meta:resourcekey="UltraGridColumnResource22">
                                                                        <Header>
                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                </Columns>
                                                                <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                    </FilterHighlightRowStyle>
                                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                        Font-Size="11px" Width="200px">
                                                                        <Padding Left="2px" />
                                                                    </FilterDropDownStyle>
                                                                </FilterOptions>
                                                            </igtbl:UltraGridBand>
                                                        </Bands>
                                                    </igtbl:UltraWebGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="TotalOfBenefitsLabel" Width="120px" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Total of Benefits" meta:resourcekey="TotalOfBenefitsLabelResource"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="TotalOfBenefitsText" runat="server" SkinID="WebNumericEdit_Fix" MinDecimalPlaces="Two"
                                                                    Nullable="False" ReadOnly="True" meta:resourcekey="txtPreparedDaysResource1">
                                                                    <ClientSideEvents ValueChange="dopostback()" />
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="TotalOfDeductionsLabel" Width="120px" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Total of Deductions" meta:resourcekey="TotalOfDeductionsLabelResource"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="TotalOfDeductionsText" runat="server" MinDecimalPlaces="Two"
                                                                    Nullable="False" ReadOnly="True" SkinID="WebNumericEdit_Fix" meta:resourcekey="lbltxtVacNetSalResource">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px; vertical-align: top;" colspan="3">
                                                    <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                        cellspacing="6">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Label ID="lblother" runat="server" Text="Other benfits" SkinID="Label_DefaultBold"
                                                                    meta:resourcekey="lblotherResource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 100%; vertical-align: top;">&nbsp;<asp:Label ID="Label11" runat="server"
                                                    SkinID="Label_DefaultBold" Text="اضافات اخرى"></asp:Label>
                                                    <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgExtraBenfits" runat="server" EnableAppStyling="False"
                                                        Height="100%" SkinID="Default" Width="100%"
                                                        meta:resourcekey="uwgTransactionsResource1">
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                            BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgOnlyForProfession"
                                                            RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                            StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                            Version="4.00" ViewType="OutlookGroupBy" AllowAddNewDefault="Yes">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                Width="100%">
                                                            </FrameStyle>
                                                            <Pager MinimumPagesForDisplay="2">
                                                                <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                </PagerStyle>
                                                            </Pager>
                                                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                            </EditCellStyleDefault>
                                                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                            </FooterStyleDefault>
                                                            <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                                                                Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                            </HeaderStyleDefault>
                                                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                Font-Names="tahoma" Font-Size="8pt" Height="18px">
                                                                <Padding Left="3px" />
                                                                <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                                            </RowStyleDefault>
                                                            <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                                            </GroupByRowStyleDefault>
                                                            <GroupByBox Hidden="True">
                                                                <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                                </BoxStyle>
                                                            </GroupByBox>
                                                            <AddNewBox>
                                                                <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                </BoxStyle>
                                                            </AddNewBox>
                                                            <ActivationObject BorderColor="" BorderWidth="">
                                                            </ActivationObject>
                                                            <FilterOptionsDefault>
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px" Height="300px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                    BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px">
                                                                    <Padding Left="2px" />
                                                                </FilterOperandDropDownStyle>
                                                            </FilterOptionsDefault>

                                                        </DisplayLayout>
                                                        <Bands>
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource2" AllowAdd="Yes">
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                                <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                    </FilterHighlightRowStyle>
                                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                        Font-Size="11px" Width="200px">
                                                                        <Padding Left="2px" />
                                                                    </FilterDropDownStyle>
                                                                </FilterOptions>
                                                                <Columns>

                                                                    <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="NumericValue" DataType="System.Double"
                                                                        Format="###,###,###.##" Key="NumericValue" Width="15%" meta:resourcekey="UltraGridColumnResource49">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <Header Caption="Amount">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                        <CellStyle HorizontalAlign="Center">
                                                                        </CellStyle>
                                                                    </igtbl:UltraGridColumn>

                                                                    <igtbl:UltraGridColumn AllowUpdate="yes" BaseColumnName="TextValue"
                                                                        Key="TextValue" Width="60px" meta:resourcekey="UltraGridColumnResource50">
                                                                        <Header Caption="Desc">
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>


                                                                </Columns>
                                                            </igtbl:UltraGridBand>
                                                        </Bands>

                                                    </igtbl:UltraWebGrid>
                                                </td>
                                                <td style="width: 6%; vertical-align: top;">&nbsp;
                                                </td>
                                                <td style="width: 47%; vertical-align: top;">
                                                    <asp:Label ID="Label12" runat="server"
                                                        SkinID="Label_DefaultBold" Text=" خصومات اخرى"></asp:Label>
                                                    <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgExtraDeduction" runat="server" EnableAppStyling="False"
                                                        Height="100%" SkinID="Default" Width="100%"
                                                        meta:resourcekey="uwgTransactionsResource1">
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                            BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgOnlyForProfession"
                                                            RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                            StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                            Version="4.00" ViewType="OutlookGroupBy" AllowAddNewDefault="Yes">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                Width="100%">
                                                            </FrameStyle>
                                                            <Pager MinimumPagesForDisplay="2">
                                                                <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                </PagerStyle>
                                                            </Pager>
                                                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                            </EditCellStyleDefault>
                                                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                            </FooterStyleDefault>
                                                            <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                                                                Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                            </HeaderStyleDefault>
                                                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                Font-Names="tahoma" Font-Size="8pt" Height="18px">
                                                                <Padding Left="3px" />
                                                                <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                                            </RowStyleDefault>
                                                            <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                                            </GroupByRowStyleDefault>
                                                            <GroupByBox Hidden="True">
                                                                <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                                </BoxStyle>
                                                            </GroupByBox>
                                                            <AddNewBox>
                                                                <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                </BoxStyle>
                                                            </AddNewBox>
                                                            <ActivationObject BorderColor="" BorderWidth="">
                                                            </ActivationObject>
                                                            <FilterOptionsDefault>
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px" Height="300px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                    BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px">
                                                                    <Padding Left="2px" />
                                                                </FilterOperandDropDownStyle>
                                                            </FilterOptionsDefault>

                                                        </DisplayLayout>
                                                        <Bands>
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource2" AllowAdd="Yes">
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                                <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                    </FilterHighlightRowStyle>
                                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                        Font-Size="11px" Width="200px">
                                                                        <Padding Left="2px" />
                                                                    </FilterDropDownStyle>
                                                                </FilterOptions>
                                                                <Columns>

                                                                    <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="NumericValue" DataType="System.Double"
                                                                        Format="###,###,###.##" Key="NumericValue" Width="15%" meta:resourcekey="UltraGridColumnResource49">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <Header Caption="Amount">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                        <CellStyle HorizontalAlign="Center">
                                                                        </CellStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowUpdate="yes" BaseColumnName="TextValue"
                                                                        Key="TextValue" Width="60px" meta:resourcekey="UltraGridColumnResource50">
                                                                        <Header Caption="Desc">
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>



                                                                </Columns>
                                                            </igtbl:UltraGridBand>
                                                        </Bands>

                                                    </igtbl:UltraWebGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="TotalOfExtraBenefitsLabel" Width="120px" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Total of Extra Benefits" meta:resourcekey="TotalofExtraBenefitsLabelResource"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="TotalofExtraBenefitsText" runat="server" SkinID="WebNumericEdit_Fix" MinDecimalPlaces="Two"
                                                                    Nullable="False" ReadOnly="True" meta:resourcekey="txtPreparedDaysResource1">
                                                                    <ClientSideEvents ValueChange="dopostback()" />
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="TotalOfExtraDeductionsLabel" Width="120px" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Total of Extra Deductions" meta:resourcekey="TotalOfExtraDeductionsLabelResource"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="TotalOfExtraDeductionsText" runat="server" MinDecimalPlaces="Two"
                                                                    Nullable="False" ReadOnly="True" SkinID="WebNumericEdit_Fix" meta:resourcekey="lbltxtVacNetSalResource">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px; vertical-align: top;" colspan="3">
                                                    <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                        cellspacing="6">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Label ID="Label3" runat="server" Text="Employee Vacation History" SkinID="Label_DefaultBold"
                                                                    meta:resourcekey="Label3Resource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>


















                                            <tr>
                                                <td style="height: 100%; vertical-align: top;" colspan="3">
                                                    <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgVacationHistory" runat="server" EnableAppStyling="True"
                                                        Height="100%" SkinID="Default" Width="99%" meta:resourcekey="uwgVacationHistoryResource1">
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                                            AutoGenerateColumns="False" BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect"
                                                            CellPaddingDefault="1" CellSpacingDefault="1"   GridLinesDefault="NotSet"
                                                            HeaderClickActionDefault="SortMulti" Name="uwgEmployeeVacations" RowHeightDefault="15px"
                                                            SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                            TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                Width="99%">
                                                            </FrameStyle>
                                                            <Pager MinimumPagesForDisplay="2">
                                                                <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                </PagerStyle>
                                                            </Pager>
                                                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                            </EditCellStyleDefault>
                                                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                            </FooterStyleDefault>
                                                            <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                                                                Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                            </HeaderStyleDefault>
                                                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                Font-Names="tahoma" Font-Size="8pt" Height="18px">
                                                                <Padding Left="3px" />
                                                                <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                                            </RowStyleDefault>
                                                            <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                                            </GroupByRowStyleDefault>
                                                            <GroupByBox Hidden="True">
                                                                <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                                </BoxStyle>
                                                            </GroupByBox>
                                                            <AddNewBox>
                                                                <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                </BoxStyle>
                                                            </AddNewBox>
                                                            <ActivationObject BorderColor="" BorderWidth="">
                                                            </ActivationObject>
                                                            <FilterOptionsDefault>
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px" Height="300px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                    BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px">
                                                                    <Padding Left="2px" />
                                                                </FilterOperandDropDownStyle>
                                                            </FilterOptionsDefault>
                                                        </DisplayLayout>
                                                        <Bands>
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource6" AllowAdd="No" AllowDelete="No"
                                                                AllowUpdate="No">
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="PaidDate" DataType="System.DateTime" Format="dd/MM/yyyy"
                                                                        Key="PaidDate" Width="35%" meta:resourcekey="UltraGridColumnResource23">
                                                                        <Header Caption="Vacation Paid At">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="PaidDays" Format="###,###,###.##" Key="PaidDays"
                                                                        Width="35%" meta:resourcekey="UltraGridColumnResource24">
                                                                        <Header Caption="Paid Days">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="Amount" Format="###,###,###.##" Key="Amount"
                                                                        Width="35%" meta:resourcekey="UltraGridColumnResource25">
                                                                        <Header Caption="Amount">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Int32" Hidden="True"
                                                                        Key="ID" meta:resourcekey="UltraGridColumnResource26">
                                                                        <Header>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                </Columns>
                                                            </igtbl:UltraGridBand>
                                                        </Bands>
                                                    </igtbl:UltraWebGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 16px; vertical-align: top; display: none" colspan="3">
                                                    <igsch:WebDateChooser ID="WebDateChooser1" runat="server" meta:resourcekey="WebDateChooser1Resource1">
                                                    </igsch:WebDateChooser>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 100%" colspan="3"></td>
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

    <script type="text/javascript" id="igClientScript">
        $(document).ready(function () {

            var Deletebtn = $("#<%=btnDelete.ClientID%>")

            Deletebtn.click(function () {
                if (confirm("هل انت متأكد من الحذف؟") == false) {
                    return false;
                }

            })


        });

    </script>
</body>
</html>
