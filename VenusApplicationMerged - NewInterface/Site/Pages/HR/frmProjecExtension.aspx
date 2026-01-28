<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProjecExtension.aspx.vb"
    Inherits="frmProjecExtension" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1"  %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ frmProjecExtension</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CloseMe() {
            parent.CloseIt();
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmProjecExtension" runat="server">
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
                                                Text="الكود" meta:resourcekey="lblCodeResource1" ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblProjectCode" runat="server" Width="80px" 
                                                SkinID="Label_CopyRightsNormal" 
                                                meta:resourcekey="lblProjectCodeResource1" ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblName" runat="server" SkinID="Label_CopyRightsBold" Text="التوصيف"
                                                Width="80px" meta:resourcekey="lblNameResource1" ></asp:Label>
                                        </td>
                                        <td style="width: 40%;">
                                            <asp:Label ID="lblProjectName" runat="server" Width="100%" 
                                                SkinID="Label_CopyRightsNormal" 
                                                meta:resourcekey="lblProjectNameResource1" ></asp:Label>
                                        </td>
                                        <td style="width: 60%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" 
                        SkinID="Default" meta:resourcekey="UltraWebTab1Resource1" >
                        <Tabs>
                            <igtab:Tab Enabled="true" Text="برجاء إدخال تاريخ الإنتهاء الجديد" meta:resourcekey="TabResource1" 
                                >
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 100%; vertical-align: top" colspan="3">
                                                <div>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 10px;">
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lblLocation" runat="server" SkinID="Label_DefaultNormal" Text="تاريح الإنتهاء"
                                                                    Width="80px" meta:resourcekey="lblLocationResource1" ></asp:Label>
                                                            </td>
                                                            <td style="width: 39%;">
                                                                <igtxt:WebMaskEdit ID="txtToDate" runat="server" InputMask="##/##/####" 
                                                                    SkinID="WebMaskEdit_Fix" meta:resourcekey="txtToDateResource1" >
                                                                </igtxt:WebMaskEdit>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 10%;">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 39%;">
                                                                <asp:LinkButton ID="LinkButton2" runat="server" SkinID="LinkButton_DefaultBold" Text="إعتماد التعديل"
                                                                    ValidationGroup="G1" meta:resourcekey="LinkButton2Resource1" ></asp:LinkButton>
                                                            </td>
                                                            <td style="width: 5px;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; vertical-align: top; text-align: center" colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%; vertical-align: top">
                                            </td>
                                            <td style="width: 96%; vertical-align: top">
                                                &nbsp;
                                            </td>
                                            <td style="width: 2%; vertical-align: top">
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
