<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TasksMonitor.aspx.vb" Inherits="TasksMonitor"
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
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="Div_FormsContainer">
        <asp:Label ID="Label_Sent" runat="server" Text="" SkinID="Label_PageHeader"></asp:Label>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:Label ID="Label_Warning" runat="server" Text="" SkinID="Label_WarningBold"></asp:Label>
        <table style="width: 100%; height: 100%; vertical-align: top; border-bottom: 1px solid black"
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
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 6%; height: 16px; vertical-align: top">
                </td>
                <td style="width: 47%; vertical-align: top" rowspan="4">
                    <table style="width: 100%; height: 100px; vertical-align: top" cellspacing="0">
                        <tr>
                            <td class="LabelArea">
                                <asp:Label ID="Label_Details" runat="server" Text="Task Details" SkinID="Label_DefaultNormal"></asp:Label>
                            </td>
                            <td class="DataArea">
                                <div style="width: 100%; height: 100px; overflow: auto">
                                    <asp:TextBox ID="TextBox_Details" runat="server" MaxLength="1024" Height="95%" Width="95%"
                                        BorderColor="#CCCCCC" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
                                        color: black; vertical-align: middle; text-align: inherit" BorderStyle="Solid"
                                        BorderWidth="1px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </td>
                            <td class="SeparArea">
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
                                <asp:Label ID="Label_ExpiryTime" runat="server" Text="Expiry Time" SkinID="Label_DefaultNormal"></asp:Label>
                            </td>
                            <td class="DataArea">
                                <igtxt:WebMaskEdit ID="txtExpiryTime" runat="server" InputMask="##:##" SkinID="WebMaskEdit_Fix">
                                </igtxt:WebMaskEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 6%; height: 16px; vertical-align: top">
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
                </td>
            </tr>
            <tr>
                <td style="width: 47%; height: 16px; vertical-align: top">
                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                        <tr>
                            <td class="SeparArea">
                            </td>
                            <td class="LabelArea">
                                <asp:Label ID="Label_ArbName" runat="server" Text="Arabic Description" SkinID="Label_DefaultNormal"></asp:Label>
                            </td>
                            <td class="DataArea">
                                <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 6%; height: 16px; vertical-align: top">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 100%; min-height: 200px; vertical-align: top"
                        cellspacing="0">
                        <tr>
                            <td style="width: 15%; height: 16px; vertical-align: middle">
                                <asp:Label ID="Label_Attachements" runat="server" Text="Attachements" SkinID="Label_DefaultNormal"></asp:Label>
                            </td>
                            <td style="width: 70%; height: 16px; vertical-align: top">
                                <iframe id="iframe1" runat="server" width="100%" height="170px" marginheight="0"
                                    frameborder="0" style="border: thin solid #C0C0C0"></iframe>
                            </td>
                            <td style="width: 15%; height: 16px; vertical-align: top; text-align: center">
                                <asp:LinkButton ID="LinkButton1" runat="server">Delete Task</asp:LinkButton>
                                <br />
                                <br />
                                <asp:LinkButton ID="LinkButton2" runat="server">Stop Task</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table style="width: 100%; height: 100%; vertical-align: top;" cellspacing="0">
            <tr>
                <td style="width: 50%; height: 100%; vertical-align: top;">
                    <asp:Label ID="Label_Achievement" runat="server" Text="Monitor Achievement" SkinID="Label_PageHeader"></asp:Label>
                    <table style="width: 100%; vertical-align: top; border: 1px solid black">
                        <tr>
                            <td style="width: 100%">
                                <ig:WebDataTree ID="TreeView_Achievement" runat="server" Height="150px" Width="100%"
                                    EnableExpandImages="False" EnableExpandOnClick="True" Font-Bold="False" Font-Names="Tahoma"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" StyleSetName="Office2007Silver"
                                    EnableTheming="True">
                                </ig:WebDataTree>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%; height: 100%; vertical-align: top;">
                    <asp:Label ID="Label_Discussion" runat="server" Text="Monitor Discussion" SkinID="Label_PageHeader"></asp:Label>
                    <table style="width: 100%; vertical-align: top; border: 1px solid black">
                        <tr>
                            <td style="width: 100%">
                                <ig:WebDataTree ID="WebDataTree_Discussion" runat="server" Height="122px" Width="100%"
                                    EnableExpandImages="False" EnableExpandOnClick="True" Font-Bold="False" Font-Names="Tahoma"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" StyleSetName="Office2007Silver"
                                    EnableTheming="True" Font-Size="8pt">
                                </ig:WebDataTree>
                                <table style="width: 100%; vertical-align: top; border: 1px solid black">
                                    <tr>
                                        <td style="width: 80%">
                                            <asp:TextBox ID="TextBox_Msg" SkinID="TextBox_LargeBoldC" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:LinkButton ID="LinkButton_Send" runat="server">Send</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
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
