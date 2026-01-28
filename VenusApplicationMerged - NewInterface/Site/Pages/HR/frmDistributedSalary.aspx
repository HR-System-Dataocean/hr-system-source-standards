<%@ page language="VB" autoeventwireup="false" codefile="frmDistributedSalary.aspx.vb"
    inherits="frmDistributedSalary" culture="auto" meta:resourcekey="PageResource1"
    uiculture="auto" %>

<%@ register tagprefix="igmisc" namespace="Infragistics.WebUI.Misc" assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ register tagprefix="igsch" namespace="Infragistics.WebUI.WebSchedule" assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ register tagprefix="igtab" namespace="Infragistics.WebUI.UltraWebTab" assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ register tagprefix="igtbar" namespace="Infragistics.WebUI.UltraWebToolbar" assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ register tagprefix="igtxt" namespace="Infragistics.WebUI.WebDataInput" assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ register assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>
<%@ register assembly="Infragistics35.WebUI.WebCombo.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    namespace="Infragistics.WebUI.WebCombo" tagprefix="igcmbo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Salary Distribution Details</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
        var ODialoge;
        var OSender;
        var IsEdit = true;
        function UwgSearchEmployees_AfterCellUpdateHandler(gridName, cellId) {
            var grid = igtbl_getGridById(gridName);
            var gridLength = grid.Rows.length;
            var cell = igtbl_getCellById(cellId);
            var row = cell.getRow();
            var blSign = cell.getValue();
            var CellToChange;
            if (IsEdit) {
                if (row.Id == gridName + "_r_0") {
                    IsEdit = false;
                    for (i = 0; i < gridLength; i++) {
                        var currRow = grid.Rows.rows[i];
                        var currRow = igtbl_getCellById(gridName + "_r" + i);
                        CellToChange = igtbl_getCellById(gridName + "_rc_" + i + "_1");
                        CellToChange.setValue(blSign)
                    }
                }
                else {
                    IsEdit = false;
                    CellToChange = igtbl_getCellById(gridName + "_rc_0_1");
                    CellToChange.setValue(false)
                }
                IsEdit = true;
            }
        }

        function ddlDepartment_Change() {
            var ultraTab = igtab_getTabById("UltraWebTab1");
            var ddlDepartment = igtab_getElementById("ddlDepartment", ultraTab.element);
            PageMethods.GetRelatedDepartment(ddlDepartment.value, OnSucceeded, OnFailed);
        }

        function OnSucceeded(result, userContext, methodName) {
            if (methodName == 'GetRelatedDepartment') {
                var ultraTab = igtab_getTabById("UltraWebTab1");
                var ddlBranch = igtab_getElementById("ddlBranche", ultraTab.element);
                ddlBranch.outerHTML = result;
            }
        }

        function OnFailed(error) {
            alert();
        }

        function OpenModal1(pageurl, height, width, CheckID, SenderCtrl) {
            if (CheckID == false) {
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
                OSender = SenderCtrl;
                $dialog.dialog('open');
            }
        }

        function CloseIt(retvalue) {
            if (retvalue != "") {
                var Sender = window.document.getElementById(OSender);
                Sender.value = retvalue;
                Sender.focus();
            }
            var $dialog = ODialoge;
            $dialog.dialog('close');
        }
    </script>
    <style type="text/css">
        .style1 {
            height: 21px;
        }
        .hidden {
            display: none;
        }
    </style>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmDistributedSalary" runat="server">
        <script type="text/javascript" id="Script1">
            $(function () {
            });
        </script>
        <div style="display: none">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
            </asp:ScriptManager>
            <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
                meta:resourcekey="nameResource1"></asp:Label>
            <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
                TabIndex="-1" Width="99px"></asp:Label>
            <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
                Width="91px" meta:resourcekey="valueResource1">
            </asp:TextBox>
            <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
                meta:resourcekey="TargetControlResource1"></asp:Label>
            <asp:Label ID="lblLage" runat="server" meta:resourcekey="lblLageResource1"></asp:Label>
        </div>
        <div class="Div_MasterContainer" runat="server" id="DIV">
            <table align="center" style="width: 100%;">
                <tr>
                    <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                        <table style="width: 100%; height: 42px; vertical-align: top">
                            <tr>
                                <td style="width: 32px; vertical-align: top">
                                    <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png"
                                        meta:resourcekey="Image_LogoResource1" />
                                </td>
                                <td style="vertical-align: middle">
                                    <asp:Label ID="Label_Header" runat="server" meta:resourcekey="Label_HeaderResource1"
                                        Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="Details">
                        <igtab:ultrawebtab id="UltraWebTab1" runat="server" enableappstyling="True" skinid="Default"
                            meta:resourcekey="UltraWebTab1Resource1">
                            <tabs>
                                <igtab:tab text="عام" meta:resourcekey="TabResource1">
                                    <contenttemplate>
                                        <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                            cellspacing="0">
                                            <tr>
                                                <td style="height: 10px" colspan="3"></td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" colspan="3">
                                                    <asp:GridView ID="grdProjects" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" Width="100%" >
                                                        <footerstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" />
                                                        <headerstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" />
                                                        <pagerstyle backcolor="#284775" forecolor="White" horizontalalign="Center" />
                                                        <rowstyle backcolor="#F7F6F3" forecolor="#333333" />
                                                        <selectedrowstyle backcolor="#E2DED6" font-bold="True" forecolor="#333333" />
                                                        <columns>
                                                            <asp:BoundField DataField="ArbName" HeaderText="Project Name" />
                                                            <asp:BoundField DataField="ProjectPercentage" HeaderText="Project Percentage" />
                                                            <asp:BoundField DataField="ProjectAmount" HeaderText="Distributed Amount" />
                                                            <asp:BoundField DataField="TransactionType" HeaderText="نوع البند" />
                                                            <asp:BoundField DataField="Sign" HeaderText="اضافة/خصم" />

                                                        </columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 100%" colspan="3"></td>
                                            </tr>
                                        </table>
                                    </contenttemplate>
                                </igtab:tab>
                            </tabs>
                        </igtab:ultrawebtab>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
