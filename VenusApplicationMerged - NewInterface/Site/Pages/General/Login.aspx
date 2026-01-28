<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../../UC/General/ThemeLangManagerLogin.ascx" TagName="ThemeLangManagerLogin"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>برنامج فينوس لشئون الموظفين === Venus Application For HR</title>
</head>
<body style="margin: 0; padding: 0;">
    <form id="form1" runat="server" defaultbutton="LinkButton_Login">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <OfficeWebUI:Manager ID="Manager1" runat="server" ChromeUI="True" DirectionMode="LTR"
        IncludeJQuery="True" UITheme="Office2010Silver" CustomCSSFile="">
    </OfficeWebUI:Manager>
    <uc2:ThemeLangManagerLogin ID="ThemeLangManagerLogin1" runat="server" />
    <OfficeWebUI:OfficeMessageBox ID="OkMessage" runat="server" MessageBoxType="Default"
        ButtonsType="Ok">
    </OfficeWebUI:OfficeMessageBox>
    <OfficeWebUI:OfficeMessageBox ID="OKCancelMessage" runat="server" MessageBoxType="Default"
        ButtonsType="OkCancel">
    </OfficeWebUI:OfficeMessageBox>
    <OfficeWebUI:OfficeMessageBox ID="YesNoMessage" runat="server" MessageBoxType="Default"
        ButtonsType="YesNo">
    </OfficeWebUI:OfficeMessageBox>
    <OfficeWebUI:OfficeMessageBox ID="YesNoCancelMessage" runat="server" MessageBoxType="Default"
        ButtonsType="YesNoCancel">
    </OfficeWebUI:OfficeMessageBox>
    <OfficeWebUI:OfficePopup ID="OfficePopup_Companies" runat="server" Height="100px"
        ShowCancelButton="True" Width="550px" ShowOkButton="True">
        <Content>
            <div class="Div_FormsContainer">
                <table style="width: 500px">
                    <tr>
                        <td style="width: 100%; height: 10px" colspan="2">
                            <asp:HiddenField ID="HiddenField_UserID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelArea">
                            <asp:Label ID="Label_SelectCompany" runat="server" Text="" SkinID="Label_DefaultBold"
                                meta:resourcekey="Label_SelectCompanyResource1"></asp:Label>
                        </td>
                        <td class="SeparArea">
                            <asp:Label ID="Label_SP1" runat="server" Text=":" SkinID="Label_DefaultBold"></asp:Label>
                        </td>
                        <td class="DataArea">
                            <asp:DropDownList ID="DropDownList_Company" runat="server" SkinID="DropDownList_LargBold">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelArea">
                            <asp:Label ID="Label_SelectGroup" runat="server" Text="" SkinID="Label_DefaultBold"
                                meta:resourcekey="Label_SelectGroupResource1"></asp:Label>
                        </td>
                        <td class="SeparArea">
                            <asp:Label ID="Label_SP2" runat="server" Text=":" SkinID="Label_DefaultBold"></asp:Label>
                        </td>
                        <td class="DataArea">
                            <asp:DropDownList ID="DropDownList_Groups" runat="server" SkinID="DropDownList_LargBold">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
    <td>
         <td style="width: 20px; height: 22px">
     <asp:Image ID="ImageUpdate" runat="server" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/updatemployee.png" meta:resourcekey="Image_UpdateResource1" />
 </td>
 <td style="width: 20%; height: 22px">
     <asp:LinkButton ID="LinkButtonUpdate" runat="server" SkinID="LinkButton_DefaultBold"
         meta:resourcekey="LinkButton_UpdateResource1" CausesValidation="False"></asp:LinkButton>
 </td>
      
   
</tr>
                </table>
            </div>
        </Content>
    </OfficeWebUI:OfficePopup>
    <OfficeWebUI:OfficePopup ID="OfficePopup_SignUp" runat="server" Height="150px" ShowCancelButton="True"
        Width="600px" ShowOkButton="True">
        <Content>
            <div class="Div_FormsContainer">
                <table style="width: 600px">
                    <tr>
                        <td class="LabelArea">
                            <asp:Label ID="Label_EmployeeCode" runat="server" Text="الرقم الوظيفى" SkinID="Label_DefaultBold"
                                meta:resourcekey="Label_EmployeeCodeResource1"></asp:Label>
                        </td>
                        <td class="SeparArea">
                            <asp:Label ID="Label2" runat="server" Text=":" SkinID="Label_DefaultBold"></asp:Label>
                        </td>
                        <td class="DataArea">
                            <asp:TextBox ID="TextBox_EmployeeCode" runat="server" SkinID="TextBox_SmalltNormalC"
                                meta:resourcekey="TextBox_EmployeeCodeResource1" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelArea">
                        </td>
                        <td class="SeparArea">
                        </td>
                        <td class="DataArea">
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelArea">
                            <asp:Label ID="Label_RegUsername" runat="server" Text="إسم المستخدم" SkinID="Label_DefaultBold"
                                meta:resourcekey="Label_RegUsernameResource1"></asp:Label>
                        </td>
                        <td class="SeparArea">
                            <asp:Label ID="Label4" runat="server" Text=":" SkinID="Label_DefaultBold" meta:resourcekey="Label4_Resource1"></asp:Label>
                        </td>
                        <td class="DataArea">
                            <asp:TextBox ID="TextBox_RegUsername" runat="server" SkinID="TextBox_SmalltNormalC"
                                meta:resourcekey="TextBox_RegUsernameResource1" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelArea">
                            <asp:Label ID="Label_RegPassword" runat="server" Text="كلمة المرور" SkinID="Label_DefaultBold"
                                meta:resourcekey="Label_RegPasswordResource1"></asp:Label>
                        </td>
                        <td class="SeparArea">
                            <asp:Label ID="Label3" runat="server" Text=":" SkinID="Label_DefaultBold"></asp:Label>
                        </td>
                        <td class="DataArea">
                            <asp:TextBox ID="TextBox_RegPassword" runat="server" SkinID="TextBox_SmalltNormalC"
                                TextMode="Password" meta:resourcekey="TextBox_RegPasswordResource1" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label1" runat="server" Text="" SkinID="Label_CopyRightsNormal"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </Content>
    </OfficeWebUI:OfficePopup>
    <OfficeWebUI:OfficePopup ID="OfficePopup_Help" runat="server" Height="300px" ShowCancelButton="false"
        Width="600px" ShowOkButton="True">
        <Content>
            <div class="Div_FormsContainer">
            </div>
        </Content>
    </OfficeWebUI:OfficePopup>

        <OfficeWebUI:OfficePopup ID="OfficePopup_ForgetPass" runat="server" Height="150px" ShowCancelButton="True"
        Width="600px" ShowOkButton="True">
        <Content>
            <div class="Div_FormsContainer">
                <table style="width: 600px">
                    <tr>
                        <td class="LabelArea">
                            <asp:Label ID="lblForgetPassEmp" runat="server" Text="الرقم الوظيفى" SkinID="Label_DefaultBold"
                                meta:resourcekey="Label_EmployeeCodeResource1"></asp:Label>
                        </td>
                        <td class="SeparArea">
                            <asp:Label ID="lblSeprator" runat="server" Text=":" SkinID="Label_DefaultBold"></asp:Label>
                        </td>
                        <td class="DataArea">
                            <asp:TextBox ID="txtForgetPassEmp" runat="server" SkinID="TextBox_SmalltNormalC"
                                meta:resourcekey="TextBox_EmployeeCodeResource1" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelArea">
                        </td>
                        <td class="SeparArea">
                        </td>
                        <td class="DataArea">
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblForgetPassConfirm" runat="server" Text="سيتم تغيير كلمة المرور وارسالها عبر الإيميل" meta:resourcekey="Label_forgetPassConfirm" SkinID="Label_WarningBold"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </Content>
    </OfficeWebUI:OfficePopup>
    <table align="center">
        <tr>
            <td style="height: 600px">
                <div class="Div_LoginContainer">
                    <table style="height: 100%; width: 100%; vertical-align: middle">
                        <tr>
                            <td style="width: 100%; height: 70px" colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 22%; height: 30px">
                            </td>
                            <td style="width: 78%; height: 30px; vertical-align: top" colspan="2">
                                <asp:Label ID="Label_Header" runat="server" Text="برجاء تسجيل الدخول" SkinID="Label_PageHeader"
                                    meta:resourcekey="Label_HeaderResource1"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; height: 50px" colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 22%">
                            </td>
                            <td style="width: 54%">
                                <table style="width: 100%">
                                    <tr>
                                        <td class="LabelArea">
                                            <asp:Label ID="Label_Username" runat="server" Text="إسم المستخدم" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label_UsernameResource1"></asp:Label>
                                        </td>
                                        <td class="SeparArea">
                                            <asp:Label ID="Label_SP3" runat="server" Text=":" meta:resourcekey="Label_SP1Resource1"></asp:Label>
                                        </td>
                                        <td class="DataArea">
                                            <asp:TextBox ID="TextBox_Username" runat="server" SkinID="TextBox_SmalltNormalC"
                                                meta:resourcekey="TextBox_UsernameResource1" MaxLength="30"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LabelArea">
                                            <asp:Label ID="Label_Password" runat="server" Text="كلمة المرور" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label_PasswordResource1"></asp:Label>
                                        </td>
                                        <td class="SeparArea">
                                            <asp:Label ID="Label_Sp4" runat="server" Text=":" meta:resourcekey="Label_Sp2Resource1"></asp:Label>
                                        </td>
                                        <td class="DataArea">
                                            <asp:TextBox ID="TextBox_Password" runat="server" SkinID="TextBox_SmalltNormalC"
                                                TextMode="Password" meta:resourcekey="TextBox_PasswordResource1" MaxLength="30"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LabelArea">
                                        </td>
                                        <td class="SeparArea">
                                        </td>
                                        <td class="DataArea">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 22%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 22%">
                            </td>
                            <td style="width: 78%" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 10%; height: 22px">
                                        </td>
                                        <td style="width: 22px; height: 22px">
                                            <asp:Image ID="Image_Login" runat="server" ImageUrl="~/Common/Images/Login.png" meta:resourcekey="Image_LoginResource1" />
                                        </td>
                                        <td style="width: 25%; height: 22px">
                                            <asp:LinkButton ID="LinkButton_Login" runat="server" SkinID="LinkButton_DefaultBold"
                                                meta:resourcekey="LinkButton_LoginResource1" CausesValidation="False"></asp:LinkButton>
                                        </td>
                                        <td style="width: 20px; height: 22px">
                                            <asp:Image ID="Image_Help" runat="server" ImageUrl="~/Common/Images/Help.png" meta:resourcekey="Image_HelpResource1" />
                                        </td>
                                        <td style="width: 20%; height: 22px">
                                            <asp:LinkButton ID="LinkButton_Help" runat="server" SkinID="LinkButton_DefaultBold"
                                                meta:resourcekey="LinkButton_HelpResource1" CausesValidation="False"></asp:LinkButton>
                                        </td>
                                        <td style="width: 20px; height: 22px">
                                            <asp:Image ID="Image_SignUp" runat="server" ImageUrl="~/Common/Images/fileZoom.png"
                                                meta:resourcekey="Image_HelpResource1" />
                                        </td>
                                        <td style="width: 25%; height: 22px">
                                            <asp:LinkButton ID="LinkButton_SignUp" runat="server" SkinID="LinkButton_DefaultBold"
                                                meta:resourcekey="LinkButton_SignUpResource1" CausesValidation="False"></asp:LinkButton>
                                        </td>
                                        <td style="width: 15%; height: 22px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 22%">
                            </td>
                            <td style="width: 78%" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 10%; height: 22px">
                                        </td>
                                        <td style="width: 22px; height: 22px">
                                            <asp:Image ID="Image_ForgetPass" runat="server" ImageUrl="~/Common/Images/reset-password.png" meta:resourcekey="Image_LoginResource111" />
                                        </td>
                                        <td style="width: 25%; height: 22px">
                                            <asp:LinkButton ID="LinkButton_ForgetPass" Text="نسيت كلمة المرور" runat="server" SkinID="LinkButton_DefaultBold"
                                                meta:resourcekey="LinkButton_ForgetPassResource1" CausesValidation="False"></asp:LinkButton>
                                        </td>
                                        
                                        <td style="width: 65%; height: 22px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; height: 60px" colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; height: 25px; text-align: center" colspan="3">
                                <asp:Label ID="Label_CopyRights" runat="server" Text="جميع الحقوق محفوظة 2011 ( Dataocean Venus System )"
                                    SkinID="Label_CopyRightsBold" meta:resourcekey="Label_CopyRightsResource1"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 150px" colspan="3">
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
