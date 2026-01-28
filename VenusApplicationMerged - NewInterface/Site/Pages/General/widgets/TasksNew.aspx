<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TasksNew.aspx.vb" Inherits="TasksNew"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>برنامج فينوس لشئون الموظفين === Venus Application For HR</title>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        function CloseMe() {
            parent.CloseIt("");
        }
        function client_OnTreeNodeChecked(event) {
            var treeNode = event.srcElement || event.target;
            if (treeNode.tagName == "INPUT" && treeNode.type == "checkbox") {
                if (treeNode.checked) {
                    uncheckOthers(treeNode.id);
                }
            }
        }

        function uncheckOthers(id) {
            var elements = document.getElementsByTagName('input');
            for (var i = 0; i < elements.length; i++) {
                if (elements.item(i).type == "checkbox") {
                    if (elements.item(i).id != id) {
                        elements.item(i).checked = false;
                    }
                }
            }
        }

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="Div_FormsContainer">
        <asp:HiddenField ID="HiddenField_CID" runat="server" />
        <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
            <tr>
                <td style="display: none">
                    <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N"
                        meta:resourcekey="ImageButton1Resource1" />
                </td>
                <td style="width: 24px">
                    <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                        SkinID="HrSave_Command" CommandArgument="Save" meta:resourcekey="ImageButton_SaveResource1"
                        ValidationGroup="G0" />
                </td>
                <td style="width: 120px">
                </td>
                <td style="width: 24px">
                    <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                        CommandArgument="New" meta:resourcekey="ImageButton_NewResource1" ValidationGroup="G1" />
                </td>
                <td style="width: 24px">
                </td>
                <td style="width: 40px">
                </td>
                <td style="width: 24px">
                </td>
                <td style="width: 80px">
                </td>
                <td style="width: 80px">
                </td>
                <td style="width: 40px">
                </td>
                <td style="width: 24px">
                </td>
                <td style="width: 24px">
                </td>
                <td style="width: 10px">
                </td>
                <td style="width: 24px">
                </td>
                <td style="width: 24px">
                </td>
                <td style="width: 30%">
                </td>
                <td style="width: 80px">
                </td>
                <td style="width: 5%">
                </td>
            </tr>
        </table>
        <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
            cellspacing="0">
            <tr>
                <td style="width: 47%; height: 16px; vertical-align: top">
                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                        <tr>
                            <td class="SeparArea">
                            </td>
                            <td class="LabelArea">
                                <asp:Label ID="Label_ExpiryDate" runat="server" Text="Expiry Date" SkinID="Label_DefaultNormal"></asp:Label>
                            </td>
                            <td class="DataArea">
                                <igtxt:WebMaskEdit ID="txtExpiryDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix">
                                </igtxt:WebMaskEdit>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExpiryDate"
                                    ErrorMessage="*" ToolTip="G0"></asp:RequiredFieldValidator>
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
                            <td class="LabelArea">
                                <asp:Label ID="Label_ExpiryTime" runat="server" Text="Expiry Time" SkinID="Label_DefaultNormal"></asp:Label>
                            </td>
                            <td class="DataArea">
                                <igtxt:WebMaskEdit ID="txtExpiryTime" runat="server" InputMask="##:##" SkinID="WebMaskEdit_Fix">
                                </igtxt:WebMaskEdit>
                            </td>
                            <td class="SeparArea">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 47%; height: 16px; vertical-align: top">
                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                        <tr>
                            <td class="SeparArea">
                            </td>
                            <td class="LabelArea">
                                <asp:Label ID="Label_EngName" runat="server" Text="English Description" SkinID="Label_DefaultNormal"></asp:Label>
                            </td>
                            <td class="DataArea">
                                <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalltr" MaxLength="255"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 6%; height: 16px; vertical-align: top">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEngName"
                        ErrorMessage="*" ValidationGroup="G0"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 47%; height: 16px; vertical-align: top">
                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                        <tr>
                            <td class="LabelArea">
                                <asp:Label ID="Label_ArbName" runat="server" Text="Arabic Description" SkinID="Label_DefaultNormal"></asp:Label>
                            </td>
                            <td class="DataArea">
                                <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"></asp:TextBox>
                            </td>
                            <td class="SeparArea">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtArbName"
                                    ErrorMessage="*" ValidationGroup="G0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 47%; height: 120px; vertical-align: top">
                    <table style="width: 100%; height: 120px; vertical-align: top" cellspacing="0">
                        <tr>
                            <td class="SeparArea">
                            </td>
                            <td class="LabelArea">
                                <asp:Label ID="Label_Details" runat="server" Text="Task Details" SkinID="Label_DefaultNormal"></asp:Label>
                            </td>
                            <td class="DataArea">
                                <asp:TextBox ID="TextBox_Details" runat="server" MaxLength="1024" Height="120px"
                                    Width="100%" BorderColor="#CCCCCC" Style="font-family: Tahoma; font-size: 8pt;
                                    font-weight: Normal; color: black; vertical-align: middle; text-align: inherit"
                                    BorderStyle="Solid" BorderWidth="1px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 6%; height: 16px; vertical-align: top">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox_Details"
                        ErrorMessage="*" ValidationGroup="G0"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 47%; height: 16px; vertical-align: top">
                    <table style="width: 100%; height: 120px; vertical-align: top" cellspacing="0">
                        <tr>
                            <td class="LabelArea">
                                <asp:Label ID="Label_Subordinates" runat="server" Text="Subordinates" SkinID="Label_DefaultNormal"></asp:Label>
                            </td>
                            <td class="DataArea">
                                <div style="width: 100%; height: 120px; overflow: auto">
                                    <asp:TreeView ID="TreeView_Subordinates" runat="server" Width="100%" BorderStyle="None"
                                        Height="100%" ShowCheckBoxes="All">
                                    </asp:TreeView>
                                </div>
                            </td>
                            <td class="SeparArea">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
            cellspacing="0">
            <tr>
                <td style="width: 20%; height: 16px; vertical-align: middle">
                    <asp:Label ID="Label_Attachements" runat="server" Text="Attachements" SkinID="Label_DefaultNormal"></asp:Label>
                </td>
                <td style="width: 80%; height: 16px; vertical-align: top">
                    <iframe id="iframe1" runat="server" width="100%" height="200px" marginheight="0"
                        frameborder="0"></iframe>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
