<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Tasks.aspx.vb" Inherits="_Tasks"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../Common/Script/JQuery/jquery-1.6.2.js"></script>
    <script src="../../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript" id="igClientScript">
        var ODialoge;
        function OpenModal1(pageurl, height, width) {
            var page = pageurl;
            var $dialog = $('<div></div>')
           .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
           .dialog({
               autoOpen: false,
               modal: true,
               height: height,
               width: width
           });
            ODialoge = $dialog;
            $dialog.dialog('open');
        }
        function CloseIt(retvalue) {
            var $dialog = ODialoge;
            $dialog.dialog('close');
            updatemyPage();
        }
        function updatemyPage() {
            setTimeout(function () {
                $('#iframe1').attr('src', '');
                __doPostBack("<%=UpdatePanel1.UniqueID %>", "");
            }, 500);
        }
        function ctl_TreeView_Tasks_NodeClick(sender, eventArgs) {
            var node = eventArgs.getNode();
            var svalue = node.get_valueString();
            if (svalue != "") {
                OpenModal1('TasksMonitor.aspx?ID=' + svalue, 550, 900);
                return false;
            }
        }
        function ctl_TreeView_Missions_NodeClick(sender, eventArgs) {
            var node = eventArgs.getNode();
            var svalue = node.get_valueString();
            if (svalue != "") {
                OpenModal1('TasksActions.aspx?ID=' + svalue, 600, 900);
                return false;
            }
        }
    </script>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" cellspacing="1" cellpadding="1" class="MainContainerMaster">
            <tr>
                <td style="width: 100%; height: 615px; vertical-align: top;">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Timer runat="server" ID="Timer1" Interval="600000" OnTick="Timer1_Tick" Enabled="false" />
                            <asp:Label ID="Label1" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="Label1Resource1"></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="" SkinID="Label_CopyRightsNormal"></asp:Label>
                            <table style="width: 100%; height: 20px; vertical-align: top; border-bottom: 1px solid black"
                                cellspacing="0">
                                <tr>
                                    <td style="width: 10%;">
                                    </td>
                                    <td style="width: 15%;">
                                        <asp:RadioButton ID="RadioButton_Today" runat="server" SkinID="RadioButton_DefaultNormal"
                                            AutoPostBack="True" GroupName="R1" />
                                    </td>
                                    <td style="width: 15%;">
                                        <asp:RadioButton ID="RadioButton_Week" runat="server" SkinID="RadioButton_DefaultNormal"
                                            AutoPostBack="True" GroupName="R1" Checked="True" />
                                    </td>
                                    <td style="width: 15%;">
                                        <asp:RadioButton ID="RadioButton_Month" runat="server" SkinID="RadioButton_DefaultNormal"
                                            AutoPostBack="True" GroupName="R1" />
                                    </td>
                                    <td style="width: 15%;">
                                        <asp:RadioButton ID="RadioButton_All" runat="server" SkinID="RadioButton_DefaultNormal"
                                            AutoPostBack="True" GroupName="R1" />
                                    </td>
                                    <td style="width: 15%;">
                                    </td>
                                    <td style="width: 15%;">
                                        <asp:ImageButton ID="ImageButton_Refresh" runat="server" ImageUrl="~/Common/Images/refresh.png" />
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                <tr>
                                    <td style="height: 16px; vertical-align: top">
                                        <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                            <tr>
                                                <td style="width: 90%">
                                                    <asp:Label ID="Label_Title1" runat="server" Text="My Orders" SkinID="Label_DefaultBold"
                                                        meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Common/Images/SServices/AddTask.png"
                                                        OnClientClick="OpenModal1('TasksNew.aspx',500,900); return false;" />
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:LinkButton ID="LinkButton_AddTask" runat="server" Text="Add New Task" OnClientClick="OpenModal1('TasksNew.aspx',500,900); return false;"
                                                        SkinID="LinkButton_DefaultNormal"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%; vertical-align: top; border-bottom: 1px solid black">
                                            <tr>
                                                <td style="width: 70%">
                                                    <ig:WebDataTree ID="TreeView_Tasks" runat="server" Height="250px" Width="100%" EnableExpandImages="False"
                                                        EnableExpandOnClick="True" Font-Bold="False" Font-Names="Tahoma" BorderColor="#CCCCCC"
                                                        BorderStyle="None" BorderWidth="1px" StyleSetName="Office2007Silver" EnableTheming="True">
                                                        <ClientEvents NodeClick="ctl_TreeView_Tasks_NodeClick" />
                                                    </ig:WebDataTree>
                                                </td>
                                                <td style="width: 30%;">
                                                    <iframe id="iframe1" src="TasksSentStatistics.aspx" runat="server" width="100%" height="250px"
                                                        marginheight="0" frameborder="0"></iframe>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                <tr>
                                    <td style="height: 16px; vertical-align: top">
                                        <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                            <tr>
                                                <td style="width: 90%">
                                                    <asp:Label ID="Label3" runat="server" Text="My Missions" SkinID="Label_DefaultBold"
                                                        meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="width: 150px;">
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%; vertical-align: top; border-bottom: 1px solid black">
                                            <tr>
                                                <td style="width: 70%">
                                                    <ig:WebDataTree ID="TreeView_Missions" runat="server" Height="250px" Width="100%"
                                                        EnableExpandImages="False" EnableExpandOnClick="True" Font-Bold="False" Font-Names="Tahoma"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" StyleSetName="Office2007Silver"
                                                        EnableTheming="True">
                                                        <ClientEvents NodeClick="ctl_TreeView_Missions_NodeClick" />
                                                    </ig:WebDataTree>
                                                </td>
                                                <td style="width: 30%;">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
