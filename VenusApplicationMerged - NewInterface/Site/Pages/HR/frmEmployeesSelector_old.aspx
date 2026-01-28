<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeesSelector.aspx.vb"
    Inherits="frmEmployeesSelector" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>* Venus Payroll * ~Employees Selector</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
        var IsEdit = true;
        function UwgSearchEmployees_AfterCellUpdateHandler(gridName, cellId) {
            var grid = igtbl_getGridById(gridName);
            var gridLength = grid.Rows.length;
            var cell = igtbl_getCellById(cellId);
            var row = cell.getRow();
            var blSign = cell.getValue();

            if (IsEdit) {
                if (row.Id == gridName + "_r_0") {
                    IsEdit = false;
                    for (i = 0; i < gridLength; i++) {
                        igtbl_getCellById(gridName + "_rc_" + i + "_1").setValue(blSign);
                    }
                }
                else {
                    IsEdit = false;
                    igtbl_getCellById(gridName + "_rc_0_1").setValue(false);

                }
                IsEdit = true;
            }
        }

        function UwgSearchEmployees_ClickCellButtonHandler(gridName, cellId) {
            var ultraTab = igtab_getTabById("UltraWebTab1");
            var intPeriodID = igtab_getElementById("DdlPeriods", ultraTab.element).value;
            var Row = igtbl_getActiveRow(gridName);
            var intEmpID = Row.getCellFromKey("ID").getValue();
            var mode = window.location.search.split('&')[0];
            var IsPrepared = Row.getCellFromKey("Prepared").getValue();

            if (intEmpID > 0) {
                if (mode == '?SM=Att') {
                    if (IsPrepared == true) {
                        OpenModal1("frmAttendancePreparation.aspx?EmpID=" + intEmpID + "&PeriodID=" + intPeriodID, 560, 800, false, "");
                    }
                    else {
                        var msg
                        if (window.document.all.item("lblLage").innerText == "0")
                            msg = 'Please prepare this employee'
                        else
                            msg = 'الرجاء تجهيز هذا الموظف أولا'
                        alert(msg)
                    }
                }
                else if (mode == '?SM=Sal') {
                    OpenModal1("frmEmployeesMonthlyTransactions.aspx?Fisical=" + intPeriodID + "&ID=" + intEmpID + "&Mode=E", 560, 750, false, "");
                }
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

        }

        var ODialoge;
        var OSender;
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
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmEmployeesSelector" runat="server" defaultbutton="ImageButton1">
    <script type="text/javascript" id="Script1">
        $(function () {
            $('#<%= ImageButton_Prepare.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= LinkButton_Prepare.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= ImageButton_Refund.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= LinkButton_Refund.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= DdlPeriods.ClientID%>').change(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= ImageButton1.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
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
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
        <asp:Label ID="lblLage" runat="server" meta:resourcekey="lblLageResource1"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display: none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N"
                                    meta:resourcekey="ImageButton1Resource1" />
                            </td>
                            <td style="width: 20px">
                                <asp:ImageButton ID="ImageButton_Prepare" Width="14px" Height="12px" runat="server"
                                    CommandArgument="Prepare" meta:resourcekey="ImageButton_PrepareResource1" ImageUrl="~/Pages/HR/Img/cal_recur.gif" />
                            </td>
                            <td style="width: 100px">
                                <asp:LinkButton ID="LinkButton_Prepare" runat="server" Text="Prepare" CommandArgument="Prepare"
                                    meta:resourcekey="LinkButton_PrepareResource1" Style="font-family: Tahoma; font-size: 8pt;
                                    font-weight: Normal;"></asp:LinkButton>
                            </td>
                            <td style="width: 20px">
                                <asp:ImageButton ID="ImageButton_Refund" Width="16px" Height="16px" runat="server"
                                    CommandArgument="Refund" meta:resourcekey="ImageButton_RefundResource1" ImageUrl="~/Pages/HR/Img/logoff_small.gif" />
                            </td>
                            <td style="width: 100px">
                                <asp:LinkButton ID="LinkButton_Refund" runat="server" Text="Refund" CommandArgument="Refund"
                                    meta:resourcekey="LinkButton_RefundResource1" Style="font-family: Tahoma; font-size: 8pt;
                                    font-weight: Normal;"></asp:LinkButton>
                            </td>
                            <td style="width: 20px;">
                                <asp:ImageButton ID="ImageButton_Import" Width="9px" Height="9px" runat="server"
                                    CommandArgument="Import" meta:resourcekey="ImageButton_ImportResource1" ImageUrl="~/Pages/HR/Img/BttnExpnd.gif"
                                    Visible="false" />
                            </td>
                            <td style="width: 100px">
                                <asp:LinkButton ID="LinkButton_Import" runat="server" Text="Import Attendance" CommandArgument="Import"
                                    meta:resourcekey="LinkButton_ImportResource1" Visible="false" Style="font-family: Tahoma;
                                    font-size: 8pt; font-weight: Normal;"></asp:LinkButton>
                            </td>
                            <td style="width: 20px;">
                                <asp:ImageButton ID="ImageButton_Fingerprint" Width="9px" Height="9px" runat="server"
                                    CommandArgument="Import" meta:resourcekey="ImageButton_ImportResource1" ImageUrl="~/Pages/HR/Img/BttnExpnd.gif"
                                    Visible="false" />
                            </td>
                            <td style="width: 100px">
                                <asp:LinkButton ID="LinkButton_Fingerprint" runat="server" Text="Import Attendance Fingerprint" CommandArgument="Fingerprint"
                                    meta:resourcekey="LinkButton_I_FingerprintResource" Visible="false" Style="font-family: Tahoma;
                                    font-size: 8pt; font-weight: Normal;"></asp:LinkButton>
                            </td>
                            <td style="width: 20px;">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td style="width: 20px;">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"
                                    Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;"></asp:LinkButton>
                            </td>
                            <td style="width: 40px">
                            </td>
                        </tr>
                    </table>
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
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                        meta:resourcekey="UltraWebTab1Resource1">
                        <Tabs>
                            <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                    <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                        cellspacing="0">
                                        <tr>
                                            <td style="height: 10px" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblCode" runat="server" Text="Employees Code" Width="90px" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCode" runat="server" MaxLength="30" meta:resourcekey="txtCodeResource1"
                                                                            SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="btnSearchCode" runat="server" Height="18px" AutoSubmit="False"
                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                            Width="24px">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/forum_search.gif" />
                                                                            </Appearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblCode1" runat="server" SkinID="Label_DefaultNormal" Text="Fisical Periods"
                                                                Width="90px" meta:resourcekey="lblCode1Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DdlPeriods" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="DdlPeriodsResource1">
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="lblDepartment" runat="server" meta:resourcekey="lblDepartmentResource1"
                                                                SkinID="Label_DefaultNormal" Text="Department" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlDepartment" runat="server" meta:resourcekey="ddlDepartmentResource1"
                                                                SkinID="DropDownList_LargNormal">
                                                            </asp:DropDownList>
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
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblBranch" runat="server" meta:resourcekey="lblBranchResource1" SkinID="Label_DefaultNormal"
                                                                Text="Branch" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlBranche" runat="server" meta:resourcekey="ddlBranchResource1"
                                                                SkinID="DropDownList_LargNormal" AutoPostBack="True">
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="Label_Contract" runat="server" meta:resourcekey="Label_ContractResource1"
                                                                SkinID="Label_DefaultNormal" Text="Contract Type" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox_Contract" runat="server" MaxLength="30" SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="WebImageButton_Cont" runat="server" Height="18px" AutoSubmit="False"
                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                            Width="24px">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/forum_search.gif" />
                                                                            </Appearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label_Sponsor" runat="server" meta:resourcekey="Label_SponsorResource1"
                                                                SkinID="Label_DefaultNormal" Text="Sponsor" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox_Sponsor" runat="server" MaxLength="30" SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="WebImageButton_Sponsor" runat="server" Height="18px" AutoSubmit="False"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/forum_search.gif" />
                                                                            </Appearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label_Project" runat="server" meta:resourcekey="lblFilterResource1"
                                                                SkinID="Label_DefaultNormal" Text="المشاريع" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DropDownList_Project" runat="server" SkinID="DropDownList_LargNormal"
                                                                AutoPostBack="True">
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="lblNationality" runat="server" meta:resourcekey="lblNationalityResource1" SkinID="Label_DefaultNormal"
                                                                Text="Nationality" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlNationality" runat="server" meta:resourcekey="ddlNationalityResource1"
                                                                SkinID="DropDownList_LargNormal">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblFilter" runat="server" meta:resourcekey="lblFilterResource1" SkinID="Label_DefaultNormal"
                                                                Text="Filter Data" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlFilter" runat="server" SkinID="DropDownList_LargNormal">
                                                                <asp:ListItem meta:resourcekey="AllDataRes" Text="All Data" Value="0"></asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="PreparedOnlyRes" Text="Prepared Only" Value="1"></asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="NotPreparedRes" Text="Not Prepared" Value="2"></asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="UploadedDate" Text="Uploaded data" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                &nbsp;
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <igtxt:WebImageButton ID="btnFind" runat="server" Height="5px" meta:resourcekey="btnFindRes"
                                                    Overflow="NoWordWrap" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
                                                    color: Black" Text=" Search " UseBrowserDefaults="False" Width="80px">
                                                    <Alignments TextImage="TextRightImageLeft" VerticalImage="Middle" />
                                                    <Appearance>
                                                        <Image Url="./img/forum_search.gif" />
                                                        <InnerBorder ColorBottom="160, 160, 160" ColorLeft="White" ColorRight="160, 160, 160"
                                                            ColorTop="White" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid"
                                                            WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                                    </Appearance>
                                                </igtxt:WebImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                <table style="width: 100%; height: 20px; vertical-align: bottom; border-bottom: 1px solid black"
                                                    cellspacing="6">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label_Title1" runat="server" Text="Please select employees" SkinID="Label_DefaultBold"
                                                                meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: middle; text-align: center;">
                                            </td>
                                            <td style="width: 47%; vertical-align: middle; text-align: right">
                                                <asp:ImageButton ID="ImageButton_Export" runat="server" CommandArgument="Import"
                                                    Height="9px" ImageUrl="~/Pages/HR/Img/BttnExpnd.gif" meta:resourcekey="ImageButton_ImportResource1"
                                                    Width="9px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;" colspan="3">
                                                <igtbl:UltraWebGrid   Browser="UpLevel"  ID="UwgSearchEmployees" runat="server" meta:resourcekey="uwgForNationalityResource1"
                                                    SkinID="Default" Width="325px" EnableTheming="True">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="None" AllowDeleteDefault="No"
                                                        AllowSortingDefault="Yes"  AllowUpdateDefault="No" AutoGenerateColumns="False"
                                                       
                                                         BorderCollapseDefault="Separate" HeaderClickActionDefault="Select" Name="uwgForNationality"
                                                        RowHeightDefault="18px" SelectTypeRowDefault="None" StationaryMargins="No" StationaryMarginsOutlookGroupBy="False"
                                                        TableLayout="Auto" Version="4.00" ViewType="Flat" AllowRowNumberingDefault="Continuous"
                                                        LoadOnDemand="Automatic">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Width="325px">
                                                        </FrameStyle>
                                                        <ClientSideEvents AfterCellUpdateHandler="UwgSearchEmployees_AfterCellUpdateHandler"
                                                            ClickCellButtonHandler="UwgSearchEmployees_ClickCellButtonHandler" />
                                                        <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                        </EditCellStyleDefault>
                                                        <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                        </FooterStyleDefault>
                                                        <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                                                            Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                        </HeaderStyleDefault>
                                                        <RowSelectorStyleDefault Width="40px" Font-Names="Arial" Font-Size="7pt">
                                                        </RowSelectorStyleDefault>
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
                                                        <igtbl:UltraGridBand AllowSorting="No" meta:resourcekey="UltraGridBandResource1">
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                            <FilterOptions EmptyString="" AllString="" NonEmptyString="">
                                                                <FilterDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px"
                                                                    Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="SteelBlue" Width="200px"
                                                                    CustomRules="overflow:auto;">
                                                                    <Padding Left="2px"></Padding>
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle ForeColor="White" BackColor="#151C55">
                                                                </FilterHighlightRowStyle>
                                                            </FilterOptions>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" Hidden="True" meta:resourcekey="UltraGridColumnResource1">
                                                                    <Header Caption="Payroll Id">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn Width="23px" AllowUpdate="Yes" Type="CheckBox" meta:resourcekey="UltraGridColumnResource2">
                                                                    <Header Caption="√">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Code" Key="Code" Width="90px"
                                                                    meta:resourcekey="UltraGridColumnResource3">
                                                                    <Header Caption="Code">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="FullName" Key="FullName"
                                                                    Width="100%" meta:resourcekey="UltraGridColumnResource4">
                                                                    <Header Caption="Employee Name">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="No" CellButtonDisplay="Always" Key="Prepared"
                                                                    BaseColumnName="Prepared" Type="CheckBox" Width="80px" meta:resourcekey="UltraGridColumnResource5">
                                                                    <Header Caption="Prepared">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn Type="Button" Width="25px" AllowRowFiltering="False" CellButtonDisplay="Always"
                                                                    meta:resourcekey="UltraGridColumnResource6">
                                                                    <Header Caption="">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/i.p.edit.gif" BorderStyle="None"
                                                                        Cursor="Hand" Height="12px" Width="13px">
                                                                    </CellButtonStyle>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
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
