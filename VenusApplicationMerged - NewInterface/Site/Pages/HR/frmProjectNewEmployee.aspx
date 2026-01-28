<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProjectNewEmployee.aspx.vb"
    Inherits="frmProjectNewEmployee" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ frmProjectNewEmployee</title>
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
            parent.CloseIt();
        }
        function PrintContract(EmpCode) {
            var hight = window.screen.availHeight - 35;
            var width = window.screen.availWidth - 10;

            var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=true&Criteria=EmployeeCode&preview=1&ReportCode=EmployeeDetails&sq0=''&v=" + EmpCode, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            win.focus();
        }
        function PrintAttend(ProjectID, PlacementID) {
            var hight = window.screen.availHeight - 35;
            var width = window.screen.availWidth - 10;
            var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=true&Criteria=ProjectCode|BranchCode|Status|ProjectID|PlacementID&preview=1&ReportCode=ProjectTimeTableContract&sq0=''&v=||A|" + ProjectID + "|" + PlacementID, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            win.focus();
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmProjectNewEmployee" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="display: none">
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td class="Details">
                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                        <tr>
                            <td style="height: 18px">
                                <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCode" runat="server" Width="80px" SkinID="Label_CopyRightsBold"
                                                Text="الكود" meta:resourcekey="lblCodeResource1"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblProjectCode" runat="server" Width="80px" SkinID="Label_CopyRightsNormal"
                                                meta:resourcekey="lblProjectCodeResource1"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblName" runat="server" SkinID="Label_CopyRightsBold" Text="التوصيف"
                                                Width="80px" meta:resourcekey="lblNameResource1"></asp:Label>
                                        </td>
                                        <td style="width: 40%;">
                                            <asp:Label ID="lblProjectName" runat="server" Width="100%" SkinID="Label_CopyRightsNormal"
                                                meta:resourcekey="lblProjectNameResource1"></asp:Label>
                                        </td>
                                        <td style="width: 60%">
                                            &nbsp;
                                            <asp:HiddenField ID="HiddenField_ProjectID" runat="server" />
                                            <asp:HiddenField ID="HiddenField_MaxSalary" runat="server" />
                                            <asp:HiddenField ID="HiddenField_EmpNewCode" runat="server" />
                                            <asp:HiddenField ID="HiddenField_ContID" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <igtab:UltraWebTab ID="UltraWebTab1" TabOrientation="LeftTop" runat="server" EnableAppStyling="True"
                        SkinID="Default" meta:resourcekey="UltraWebTab1Resource1">
                        <Tabs>
                            <igtab:Tab Enabled="true" Text="التحقق من الشخص المتقدم" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: left; width: 50%">
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton2" runat="server" SkinID="LinkButton_DefaultBold" Text="التالى"
                                                    ValidationGroup="G1" meta:resourcekey="LinkButton2Resource1"></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td class="SeparArea">
                                            </td>
                                            <td class="LabelArea">
                                                <asp:Label ID="Label1" runat="server" SkinID="Label_DefaultNormal" Text="أدخل رقم هوية المتقدم"
                                                    meta:resourcekey="Label1Resource1"></asp:Label>
                                            </td>
                                            <td class="DataArea" style="width: 50%">
                                                <asp:TextBox ID="txtCode" runat="server" MaxLength="30" SkinID="TextBox_LargeNormalC"
                                                    meta:resourcekey="txtCodeResource1"></asp:TextBox>
                                            </td>
                                            <td class="DataArea" style="width: 50%">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCode"
                                                    ErrorMessage="*" ValidationGroup="G1" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Enabled="true" Text="البيانات الأساسية" meta:resourcekey="TabResource2">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: right; width: 50%">
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton5" runat="server" SkinID="LinkButton_DefaultBold" Text="السابق"
                                                    meta:resourcekey="LinkButton5Resource1"></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td style="height: 18px; text-align: left; width: 50%">
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton6" runat="server" ValidationGroup="G2" SkinID="LinkButton_DefaultBold"
                                                    Text="التالى" meta:resourcekey="LinkButton6Resource1"></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 1%; height: 16px">
                                                <asp:Label ID="td1" runat="server" Text=" " Width="5px" meta:resourcekey="td1Resource1"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 21%; text-align: center;">
                                                <asp:Label ID="Label2" runat="server" Text="Name" SkinID="Label_DefaultNormal" meta:resourcekey="Label2Resource1"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 21%; text-align: center;">
                                                <asp:Label ID="Label3" runat="server" SkinID="Label_DefaultNormal" Text="Father Name"
                                                    meta:resourcekey="Label3Resource1"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 21%; text-align: center;">
                                                <asp:Label ID="Label4" runat="server" SkinID="Label_DefaultNormal" Text="Grand Name"
                                                    meta:resourcekey="Label4Resource1"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 21%; text-align: center;">
                                                <asp:Label ID="Label5" runat="server" SkinID="Label_DefaultNormal" Text="Family Name"
                                                    meta:resourcekey="Label5Resource1"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%; height: 16px">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEngName" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                    Text="English name" meta:resourcekey="lblEngNameResource1"></asp:Label>
                                            </td>
                                            <td style="width: 20%;">
                                                <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEngNameResource1"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEngName"
                                                    ErrorMessage="*" ValidationGroup="G2" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 20%;">
                                                <asp:TextBox ID="txtEngFathername" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEngFathernameResource1"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 20%;">
                                                <asp:TextBox ID="txtEngGrandName" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEngGrandNameResource1"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 20%;">
                                                <asp:TextBox ID="txtEngFamilyName" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEngFamilyNameResource1"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEngFamilyName"
                                                    ErrorMessage="*" ValidationGroup="G2" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%; height: 16px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lblArbName" runat="server" SkinID="Label_DefaultNormal" Text="Arabic name"
                                                    meta:resourcekey="lblArbNameResource1"></asp:Label>
                                            </td>
                                            <td style="width: 20%;">
                                                <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtArbNameResource1"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtArbName"
                                                    ErrorMessage="*" ValidationGroup="G2" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 20%;">
                                                <asp:TextBox ID="txtArbFatherName" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtArbFatherNameResource1"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 20%;">
                                                <asp:TextBox ID="txtArbGrandName" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtArbGrandNameResource1"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 20%;">
                                                <asp:TextBox ID="txtArbFamilyName" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtArbFamilyNameResource1"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtArbFamilyName"
                                                    ErrorMessage="*" ValidationGroup="G2" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="width: 10%;">
                                                <asp:Label ID="lblBirthDate" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                    Text="Birth Date" Style="height: 13px" meta:resourcekey="lblBirthDateResource1"></asp:Label>
                                            </td>
                                            <td style="width: 39%;">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 40%;">
                                                            <igtxt:WebMaskEdit ID="txtBirthDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default"
                                                                meta:resourcekey="txtBirthDateResource1">
                                                            </igtxt:WebMaskEdit>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblHijri2" runat="server" SkinID="Label_CopyRightsBold" Text="G" meta:resourcekey="lblHijri2Resource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 40%;">
                                                            <igtxt:WebMaskEdit ID="txtBirthDateH" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default"
                                                                meta:resourcekey="txtBirthDateHResource1">
                                                            </igtxt:WebMaskEdit>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblHijri" runat="server" SkinID="Label_CopyRightsBold" Text="H" meta:resourcekey="lblHijriResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 5%;">
                                                        </td>
                                                        <td style="width: 15%;">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 2%;">
                                                <asp:Label ID="td2" runat="server" Text=" " Width="24px" meta:resourcekey="td2Resource1"></asp:Label>
                                            </td>
                                            <td style="width: 10%;">
                                                <asp:Label ID="lblBirthCitys" runat="server" SkinID="Label_DefaultNormal" Text="Birth City"
                                                    Width="80px" meta:resourcekey="lblBirthCitysResource1"></asp:Label>
                                            </td>
                                            <td style="width: 39%;">
                                                <asp:DropDownList ID="DdlBirthCity" runat="server" SkinID="DropDownList_LargNormal"
                                                    meta:resourcekey="DdlBirthCityResource1">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNationality" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                    Text="Nationality" meta:resourcekey="lblNationalityResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DdlNationality" runat="server" SkinID="DropDownList_LargNormal"
                                                    meta:resourcekey="DdlNationalityResource1">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="DdlNationality"
                                                    ErrorMessage="*" Operator="NotEqual" ValidationGroup="G2" ValueToCompare="0"
                                                    meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblReligion" runat="server" SkinID="Label_DefaultNormal" Text="Religion"
                                                    Width="80px" meta:resourcekey="lblReligionResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DdlReligion" runat="server" SkinID="DropDownList_LargNormal"
                                                    meta:resourcekey="DdlReligionResource1">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="DdlReligion"
                                                    ErrorMessage="*" Operator="NotEqual" ValidationGroup="G2" ValueToCompare="0"
                                                    meta:resourcekey="CompareValidator4Resource1"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblGender" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                    Text="Gender" meta:resourcekey="lblGenderResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DdlGender" runat="server" SkinID="DropDownList_LargNormal"
                                                    meta:resourcekey="DdlGenderResource1">
                                                    <asp:ListItem Selected="True" Text="Male" Value="M" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                    <asp:ListItem Text="Female" Value="F" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMaritalStatus" runat="server" SkinID="Label_DefaultNormal" Text="Marital Status"
                                                    Width="80px" meta:resourcekey="lblMaritalStatusResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DdlMaritalStatus" runat="server" SkinID="DropDownList_LargNormal"
                                                    meta:resourcekey="DdlMaritalStatusResource1">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="DdlMaritalStatus"
                                                    ErrorMessage="*" Operator="NotEqual" ValidationGroup="G2" ValueToCompare="0"
                                                    meta:resourcekey="CompareValidator5Resource1"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblBloodGroups" runat="server" SkinID="Label_DefaultNormal" Text="Blood Group"
                                                    Width="80px" meta:resourcekey="lblBloodGroupsResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DdlBloodGroups" runat="server" SkinID="DropDownList_LargNormal"
                                                    meta:resourcekey="DdlBloodGroupsResource1">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEmail" runat="server" SkinID="Label_DefaultNormal" Text="Email"
                                                    Width="80px" meta:resourcekey="lblEmailResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEmail" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMobile" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                    Text="Mobile No" meta:resourcekey="lblMobileResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMobile" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtMobileResource1"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMobile"
                                                    ErrorMessage="*" ValidationGroup="G2" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPhone" runat="server" SkinID="Label_DefaultNormal" Text="Phone No"
                                                    Width="80px" meta:resourcekey="lblPhoneResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPhone" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtPhoneResource1"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%;">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLastEducations" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                    Text="Last Educations" meta:resourcekey="lblLastEducationsResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLastEducations" runat="server" SkinID="DropDownList_LargNormal"
                                                    meta:resourcekey="ddlLastEducationsResource1">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlLastEducations"
                                                    ErrorMessage="*" Operator="NotEqual" ValidationGroup="G2" ValueToCompare="0"
                                                    meta:resourcekey="CompareValidator3Resource1"></asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblGraduationDate" runat="server" SkinID="Label_DefaultNormal" Text="Graduation Date"
                                                    Width="80px" meta:resourcekey="lblGraduationDateResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <igtxt:WebMaskEdit ID="txtGraduationDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default"
                                                    meta:resourcekey="txtGraduationDateResource1">
                                                </igtxt:WebMaskEdit>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%;">
                                            </td>
                                            <td>
                                                <asp:Label ID="Label_ExpectedJoinDate" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                    Text="Expected Join Date" meta:resourcekey="Label_ExpectedJoinDateResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <igtxt:WebMaskEdit ID="WebMaskEdit_ExpectedJoinDate" runat="server" InputMask="##/##/####"
                                                    SkinID="WebMaskEdit_Default" meta:resourcekey="WebMaskEdit_ExpectedJoinDateResource1">
                                                </igtxt:WebMaskEdit>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="WebMaskEdit_ExpectedJoinDate"
                                                    ErrorMessage="*" ValidationGroup="G2" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSponsor" runat="server" SkinID="Label_DefaultNormal" Text="Sponsor"
                                                    Width="80px" meta:resourcekey="lblSponsorResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSponsor" runat="server" SkinID="DropDownList_LargNormal"
                                                    meta:resourcekey="ddlSponsorResource1">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="ddlSponsor"
                                                    ErrorMessage="*" Operator="NotEqual" ValidationGroup="G2" ValueToCompare="0"
                                                    meta:resourcekey="CompareValidator8Resource1"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="display: none">
                                        <igtxt:WebMaskEdit ID="WebMaskEdit1" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default"
                                            meta:resourcekey="WebMaskEdit1Resource1">
                                        </igtxt:WebMaskEdit>
                                    </div>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Text="البيانات المالية" Enabled="false" meta:resourcekey="TabResource3">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: right; width: 50%">
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton7" runat="server" SkinID="LinkButton_DefaultBold" Text="السابق"
                                                    meta:resourcekey="LinkButton7Resource1"></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td style="height: 18px; text-align: left; width: 50%">
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton8" runat="server" SkinID="LinkButton_DefaultBold" Text="التالى"
                                                    ValidationGroup="G3" meta:resourcekey="LinkButton8Resource1"></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                                <div style="display: none">
                                                    <igtxt:WebNumericEdit ID="OrgAmount" runat="server" DataMode="Decimal" MinValue="0"
                                                        NullText="0" SkinID="WebNumericEdit_Default" ValueText="0" meta:resourcekey="OrgAmountResource1">
                                                    </igtxt:WebNumericEdit>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_CreditLimit" runat="server" Text="قيمة الراتب" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="Label_CreditLimitResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtSalary" runat="server" DataMode="Decimal" MinValue="0"
                                                                NullText="0" SkinID="WebNumericEdit_Default" ValueText="0" meta:resourcekey="txtSalaryResource1">
                                                            </igtxt:WebNumericEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            &nbsp;
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" colspan="3">
                                                <ignav:UltraWebTree ID="UltraWebTree1" runat="server" CheckBoxes="True" EnableAppStyling="False"
                                                    Font-Names="Tahoma" Font-Size="10pt" Height="200px" Selectable="False" StyleSetName="Default"
                                                    DefaultImage="" HoverClass="" Indentation="20" meta:resourcekey="UltraWebTree1Resource1">
                                                    <NodePaddings Bottom="7px" />
                                                </ignav:UltraWebTree>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Text="أرشفة الوثائق والمستندات" Enabled="false" meta:resourcekey="TabResource4">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: right; width: 50%">
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton9" runat="server" SkinID="LinkButton_DefaultBold" Text="السابق"
                                                    meta:resourcekey="LinkButton9Resource1"></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td style="height: 18px; text-align: left; width: 50%">
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton10" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="التالى" meta:resourcekey="LinkButton10Resource1"></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; height: 83%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 100%; vertical-align: top">
                                                <iframe id="frame1" width="100%" height="500px" scrolling="auto" runat="server">
                                                </iframe>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Text="الطباعة" Enabled="false" meta:resourcekey="TabResource5">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: right; width: 50%">
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton15" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="السابق" meta:resourcekey="LinkButton15Resource1"></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td style="height: 18px; text-align: left; width: 50%">
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton16" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="إنهاء" meta:resourcekey="LinkButton16Resource1"></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: center; width: 50%">
                                                <asp:LinkButton ID="LinkButton1" runat="server" SkinID="LinkButton_DefaultBold" Text="طباعة عقد الموظف"
                                                    meta:resourcekey="LinkButton1Resource1"></asp:LinkButton>
                                            </td>
                                            <td style="height: 18px; text-align: center; width: 50%">
                                                <asp:LinkButton ID="LinkButton3" runat="server" SkinID="LinkButton_DefaultBold" Text="طباعة جدول الدوامات والتعليمات الأمنية للمشروع"
                                                    meta:resourcekey="LinkButton3Resource1"></asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 18px; text-align: center; width: 50%">
                                                <asp:HiddenField ID="HiddenField_ContPrint" runat="server" />
                                            </td>
                                            <td style="height: 18px; text-align: center; width: 50%">
                                                <asp:HiddenField ID="HiddenField_DetailsPrint" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                        </Tabs>
                    </igtab:UltraWebTab>
                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                        <tr>
                            <td style="height: 18px; text-align: center; width: 33%">
                                <asp:LinkButton ID="LinkButton4" runat="server" SkinID="LinkButton_DefaultBold" Text="عرض بنود التعاقد"
                                    meta:resourcekey="LinkButton4Resource1"></asp:LinkButton>
                            </td>
                            <td style="height: 18px; text-align: center; width: 33%">
                                <asp:LinkButton ID="LinkButton11" runat="server" SkinID="LinkButton_DefaultBold"
                                    Text="عرض الوثائق والمستندات" meta:resourcekey="LinkButton11Resource1"></asp:LinkButton>
                            </td>
                            <td style="height: 18px; text-align: center; width: 34%">
                                <asp:LinkButton ID="LinkButton12" runat="server" SkinID="LinkButton_DefaultBold"
                                    Text="إعتماد الموظف" meta:resourcekey="LinkButton12Resource1"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
