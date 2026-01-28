<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangePassword.aspx.vb"
    Inherits="ChangePassword" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>برنامج فينوس لشئون الموظفين === Venus Application For HR</title>
        <script type="text/javascript">
            function CloseMe() {
                parent.CloseIt("");
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Div_FormsContainer">
        <table align="center" style="height: 150px; width: 500px; vertical-align: middle;
            border: 1px solid Black;">
            <tr>
                <td style="width: 5%">
                </td>
                <td style="width: 95%">
                    <table style="width: 100%">
                        <tr>
                            <td class="LabelArea">
                                <asp:Label ID="Label_OldPassword" runat="server" Text="كلمة المرور القديمة" SkinID="Label_DefaultBold"
                                    meta:resourcekey="Label_UsernameResource1"></asp:Label>
                            </td>
                            <td class="SeparArea">
                                <asp:Label ID="Label_SP3" runat="server" Text=":" meta:resourcekey="Label_SP1Resource1"></asp:Label>
                            </td>
                            <td class="DataArea">
                                <asp:TextBox ID="TextBox_OldPassword" runat="server" SkinID="TextBox_SmallBoldC"
                                    TextMode="Password" MaxLength="30" 
                                    meta:resourcekey="TextBox_OldPasswordResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelArea">
                                <asp:Label ID="Label_NewPassword" runat="server" Text="كلمة المرور الجديدة" SkinID="Label_DefaultBold"
                                    meta:resourcekey="Label_PasswordResource1"></asp:Label>
                            </td>
                            <td class="SeparArea">
                                <asp:Label ID="Label_Sp4" runat="server" Text=":" meta:resourcekey="Label_Sp2Resource1"></asp:Label>
                            </td>
                            <td class="DataArea">
                                <asp:TextBox ID="TextBox_NewPassword" runat="server" SkinID="TextBox_SmallBoldC"
                                    TextMode="Password" MaxLength="30" 
                                    meta:resourcekey="TextBox_NewPasswordResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelArea">
                            </td>
                            <td class="SeparArea">
                            </td>
                            <td class="DataArea">
                                <asp:LinkButton ID="LinkButton_Change" runat="server" SkinID="LinkButton_DefaultBold"
                                    CausesValidation="False" meta:resourcekey="LinkButton_ChangeResource1">تغيير كلمة المرور</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 25px; text-align: center" colspan="2">
                    <asp:Label ID="Label_CopyRights" runat="server" Text="جميع الحقوق محفوظة 2011 ( Dataocean Venus System )"
                        SkinID="Label_CopyRightsBold" meta:resourcekey="Label_CopyRightsResource1"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
