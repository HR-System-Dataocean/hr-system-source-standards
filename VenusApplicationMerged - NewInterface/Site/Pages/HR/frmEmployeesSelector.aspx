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
    <style type="text/css">
        * { box-sizing: border-box; }
        body {
            font-family: Tahoma, Arial, sans-serif;
            font-size: 12px;
            margin: 0;
            padding: 0;
            background: #f3f6fa;
            color: #2c3e50;
        }
        [dir="rtl"] body { direction: rtl; }
        [dir="ltr"] body { direction: ltr; }
        .Div_MasterContainer {
            width: 100%;
            padding: 4px 10px 8px;
            background: #f3f6fa;
        }
        [dir="rtl"] .Div_MasterContainer { direction: rtl; }
        [dir="ltr"] .Div_MasterContainer { direction: ltr; }

        .action-toolbar {
            display: flex;
            align-items: stretch;
            flex-wrap: wrap;
            gap: 0;
            background: #fff;
            border: 1px solid #d5dde8;
            border-radius: 6px 6px 0 0;
            margin-bottom: 0;
            overflow: hidden;
        }
        .action-toolbar .action-item {
            display: inline-flex;
            align-items: center;
            gap: 5px;
            padding: 5px 10px;
            border: none;
            border-inline-end: 1px solid #e4ebf3;
            background: #fff;
            color: #1a4a7a;
            font-family: Tahoma, Arial, sans-serif;
            font-size: 12px;
            font-weight: bold;
            text-decoration: none;
            white-space: nowrap;
            min-height: 30px;
        }
        .action-toolbar .action-item:hover { background: #f4f8fc; }
        .action-toolbar .action-item img { width: 14px; height: 14px; }
        .action-toolbar .action-item a {
            color: #1a4a7a;
            text-decoration: none;
            font-family: Tahoma, Arial, sans-serif;
            font-size: 12px;
            font-weight: bold;
        }
        .action-toolbar .action-spacer { flex: 1; }
        .action-toolbar .action-help {
            border-inline-end: none;
            border-inline-start: 1px solid #e4ebf3;
            color: #6b7c8f;
        }
        .action-toolbar .action-help a { color: #6b7c8f; }

        .page-header {
            display: flex;
            align-items: center;
            gap: 8px;
            background: #fff;
            border: 1px solid #d5dde8;
            border-top: none;
            border-radius: 0 0 6px 6px;
            padding: 6px 10px;
            margin-bottom: 6px;
        }
        .page-header-icon {
            width: 28px;
            height: 28px;
            border-radius: 5px;
            background: #e8f2fb;
            border: 1px solid #b7d4ef;
            display: flex;
            align-items: center;
            justify-content: center;
            flex-shrink: 0;
        }
        .page-header-icon img { width: 16px; height: 16px; }
        .page-header-main { flex: 1; min-width: 0; }
        .page-header-title {
            display: flex;
            align-items: baseline;
            flex-wrap: wrap;
            gap: 6px 10px;
            font-size: 14px;
            font-weight: bold;
            color: #1a4a7a;
            margin: 0;
            line-height: 1.2;
        }
        .page-header-sub {
            display: inline;
            font-size: 11px;
            font-weight: normal;
            color: #6b7c8f;
            margin: 0;
            line-height: 1.2;
        }
        .period-badge {
            flex-shrink: 0;
            background: #eef5fb;
            border: 1px solid #b7d4ef;
            border-radius: 5px;
            padding: 4px 10px;
            font-size: 11px;
            color: #1a4a7a;
            white-space: nowrap;
            align-self: center;
        }
        .period-badge .meta-label { font-weight: bold; color: #4a5a6a; }
        .period-badge .meta-value { font-weight: bold; color: #1a4a7a; }

        .form-card {
            background: #fff;
            border: 1px solid #c5d6e8;
            border-radius: 6px;
            margin-bottom: 6px;
            overflow: hidden;
            box-shadow: none;
        }
        .form-card-header {
            display: none !important;
        }
        .form-card-body { padding: 6px 10px; }

        .filter-grid {
            display: flex;
            flex-wrap: wrap;
            gap: 4px 18px;
        }
        .filter-col {
            flex: 1 1 300px;
            min-width: 260px;
            display: flex;
            flex-direction: column;
            gap: 3px;
        }
        .field-item {
            display: flex;
            align-items: center;
            gap: 6px;
            min-height: 26px;
        }
        .field-label {
            font-size: 11px;
            color: #334455;
            white-space: nowrap;
            min-width: 110px;
            flex-shrink: 0;
        }
        .field-control { flex: 1; min-width: 0; }
        .input-with-btn {
            display: flex;
            align-items: center;
            gap: 3px;
        }
        .input-with-btn .form-input { flex: 1; }

        .form-input,
        .form-card-body input[type="text"],
        .form-card-body select {
            width: 100%;
            padding: 2px 6px;
            border: 1px solid #c5ced9;
            border-radius: 3px;
            font-family: Tahoma, Arial, sans-serif;
            font-size: 11px;
            background: #fff;
            color: #2c3e50;
            height: 24px;
        }
        .form-card-body select { padding-inline-end: 20px; }
        [dir="rtl"] .form-card-body input[type="text"],
        [dir="rtl"] .form-card-body select { text-align: right; }
        [dir="ltr"] .form-card-body input[type="text"],
        [dir="ltr"] .form-card-body select { text-align: left; }

        .search-actions {
            display: none !important;
        }
        .btn-search {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            min-width: 90px;
        }
        .field-item-search-slot .field-label {
            visibility: hidden;
        }
        .field-item-search-slot .field-control {
            display: flex;
            align-items: center;
            justify-content: flex-start;
        }
        .btn-search-inline {
            display: inline-flex;
            align-items: center;
            flex-shrink: 0;
        }

        .alert-banner {
            display: flex;
            align-items: center;
            gap: 10px;
            background: #fff8e6;
            border: 1px solid #e6c76b;
            border-radius: 8px;
            padding: 6px 12px;
            margin-bottom: 8px;
            color: #7a5c10;
            font-size: 12px;
            line-height: 1.35;
        }
        .alert-banner .alert-icon {
            width: 22px;
            height: 22px;
            border-radius: 50%;
            background: #ffe8a3;
            color: #9a7010;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: bold;
            flex-shrink: 0;
            font-size: 12px;
        }
        .alert-banner .alert-body {
            flex: 1;
            min-width: 0;
            display: flex;
            flex-wrap: wrap;
            align-items: baseline;
            gap: 0 6px;
        }
        .alert-banner .alert-title {
            display: inline;
            font-weight: bold;
            margin: 0;
        }
        .alert-banner .alert-sub {
            display: inline;
            color: #8a6a20;
            font-size: 12px;
            margin: 0;
        }
        .btn-review {
            flex-shrink: 0;
            background: #1a4a7a;
            color: #fff !important;
            border: none;
            border-radius: 5px;
            padding: 7px 14px;
            font-family: Tahoma, Arial, sans-serif;
            font-size: 12px;
            font-weight: bold;
            cursor: pointer;
            text-decoration: none;
            white-space: nowrap;
        }
        .btn-review:hover { background: #153d66; color: #fff !important; }

        .grid-header-row {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 10px;
            margin-bottom: 8px;
        }
        .grid-title {
            font-size: 13px;
            font-weight: bold;
            color: #1a4a7a;
        }
        .grid-wrap {
            width: 100%;
            overflow: auto;
            border: 1px solid #d5dde8;
            border-radius: 6px;
            background: #fff;
        }
        .grid-wrap table { width: 100% !important; }
        .grid-note {
            margin-top: 8px;
            font-size: 11px;
            color: #6b7c8f;
            line-height: 1.45;
        }

        .Details .iguw_Control,
        .Details .iguw_ControlMain {
            border: none !important;
            background: transparent !important;
            padding: 0 !important;
            margin: 0 !important;
        }
        .Details .iguw_Tab,
        .Details .iguw_SelectedTab,
        .Details .iguw_HoverTab,
        .Details table.iguw_Control > tbody > tr:first-child {
            display: none !important;
        }
        .es-tab-content { padding: 0; }
        .hidden-server { display: none !important; }

        @media (max-width: 900px) {
            .page-header { flex-wrap: wrap; }
            .period-badge { width: 100%; text-align: center; }
            .field-item { flex-direction: column; align-items: stretch; }
            .field-label { min-width: 0; }
            .alert-banner { flex-wrap: wrap; }
        }
    </style>
    <script type="text/javascript" id="igClientScript">
        function VacYesNoConfirm(msg, yesText, noText, postBackTarget, yesArg, noArg) {
            var $dlg = $('#VacNotifyDialog');
            if ($dlg.length === 0) {
                if (window.confirm(msg)) {
                    __doPostBack(postBackTarget, yesArg);
                } else {
                    __doPostBack(postBackTarget, noArg);
                }
                return;
            }

            $dlg.html(msg);
            $dlg.dialog({
                modal: true,
                resizable: false,
                draggable: true,
                width: 520,
                closeOnEscape: false,
                buttons: [
                    {
                        text: yesText,
                        click: function () {
                            $(this).dialog('close');
                            __doPostBack(postBackTarget, yesArg);
                        }
                    },
                    {
                        text: noText,
                        click: function () {
                            $(this).dialog('close');
                            __doPostBack(postBackTarget, noArg);
                        }
                    }
                ]
            });
        }
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
            debugger
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
                else if (mode == '?SM=Dis') {
                    if (IsPrepared == true) {
                        OpenModal1("frmDistributedSalary.aspx?Fisical=" + intPeriodID + "&ID=" + intEmpID, 560, 750, false, "");
                    }
                    else {
                        var msg
                        if (window.document.all.item("lblLage").innerText == "0")
                            msg = 'Please distribute this employee salary first'
                        else
                            msg = 'الرجاء توزيع راتب هذا الموظف أولا'
                        alert(msg)
                    }
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
            try {
                if (retvalue != "" && OSender) {
                    var Sender = window.document.getElementById(OSender);
                    if (Sender) {
                        Sender.value = retvalue;
                        Sender.focus();
                    }
                }
            } catch (e) { }
            try {
                if (ODialoge) ODialoge.dialog('close');
            } catch (e2) { }
            // Do not reload parent data after retro popup close — keep current period/filters/grid as-is
        }

        function SetPageDirection(dir) {
            document.documentElement.dir = dir;
            document.body.dir = dir;
            var div = document.getElementById('DIV');
            if (div) div.dir = dir;
        }

        function SyncPeriodBadge() {
            try {
                var ddl = (typeof esClientIds !== 'undefined' && esClientIds.DdlPeriods)
                    ? document.getElementById(esClientIds.DdlPeriods) : null;
                var badge = (typeof esClientIds !== 'undefined' && esClientIds.lblCurrentPeriodValue)
                    ? document.getElementById(esClientIds.lblCurrentPeriodValue) : null;
                if (ddl && badge && ddl.selectedIndex >= 0) {
                    badge.innerHTML = ddl.options[ddl.selectedIndex].text;
                }
            } catch (e) { }
        }

        $(document).ready(function () {
            try {
                if (typeof pageDirection !== 'undefined') SetPageDirection(pageDirection);
            } catch (e) { }
            SyncPeriodBadge();

            function bindBlock(id) {
                if (!id) return;
                $('#' + id).click(function () { $.blockUI({ message: '' }); });
            }
            try {
                if (typeof esClientIds !== 'undefined') {
                    if (esClientIds.ImageButton_Prepare) {
                        $('#' + esClientIds.ImageButton_Prepare).click(function () {
                            $.blockUI({ message: '<h1>... </h1>' });
                        });
                    }
                    bindBlock(esClientIds.LinkButton_Prepare);
                    bindBlock(esClientIds.ImageButton_Refund);
                    bindBlock(esClientIds.LinkButton_Refund);
                    bindBlock(esClientIds.ImageButton1);
                    if (esClientIds.DdlPeriods) {
                        $('#' + esClientIds.DdlPeriods).change(function () {
                            $.blockUI({ message: '' });
                        });
                    }
                }
            } catch (e2) { }
        });
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmEmployeesSelector" runat="server" defaultbutton="ImageButton1">
        <div id="VacNotifyDialog" style="display: none"></div>
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
        <asp:LinkButton ID="LinkButton_VacNotifyConfirm" runat="server" Style="display: none" />
        <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N"
            meta:resourcekey="ImageButton1Resource1" />
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">

        <div class="action-toolbar">
            <span class="action-item">
                <asp:ImageButton ID="ImageButton_Prepare" Width="14px" Height="12px" runat="server"
                    CommandArgument="Prepare" meta:resourcekey="ImageButton_PrepareResource1" ImageUrl="~/Pages/HR/Img/cal_recur.gif" />
                <asp:LinkButton ID="LinkButton_Prepare" runat="server" Text="Prepare" CommandArgument="Prepare"
                    meta:resourcekey="LinkButton_PrepareResource1"></asp:LinkButton>
            </span>
            <span class="action-item">
                <asp:ImageButton ID="ImageButton_Refund" Width="16px" Height="16px" runat="server"
                    CommandArgument="Refund" meta:resourcekey="ImageButton_RefundResource1" ImageUrl="~/Pages/HR/Img/logoff_small.gif" />
                <asp:LinkButton ID="LinkButton_Refund" runat="server" Text="Refund" CommandArgument="Refund"
                    meta:resourcekey="LinkButton_RefundResource1"></asp:LinkButton>
            </span>
            <span class="action-item" id="spanImport" runat="server" visible="false">
                <asp:ImageButton ID="ImageButton_Import" Width="9px" Height="9px" runat="server"
                    CommandArgument="Import" meta:resourcekey="ImageButton_ImportResource1" ImageUrl="~/Pages/HR/Img/BttnExpnd.gif" />
                <asp:LinkButton ID="LinkButton_Import" runat="server" Text="Import Attendance" CommandArgument="Import"
                    meta:resourcekey="LinkButton_ImportResource1"></asp:LinkButton>
            </span>
            <span class="action-item" id="spanFingerprint" runat="server" visible="false">
                <asp:ImageButton ID="ImageButton_Fingerprint" Width="9px" Height="9px" runat="server"
                    CommandArgument="Import" meta:resourcekey="ImageButton_ImportResource1" ImageUrl="~/Pages/HR/Img/BttnExpnd.gif" />
                <asp:LinkButton ID="LinkButton_Fingerprint" runat="server" Text="Import Attendance Fingerprint" CommandArgument="Fingerprint"
                    meta:resourcekey="LinkButton_I_FingerprintResource"></asp:LinkButton>
            </span>
            <span class="action-spacer"></span>
            <span class="action-item action-help">
                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
            </span>
        </div>

        <div class="page-header">
            <div class="page-header-icon">
                <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png"
                    meta:resourcekey="Image_LogoResource1" />
            </div>
            <div class="page-header-main">
                <div class="page-header-title">
                    <asp:Label ID="Label_Header" runat="server" meta:resourcekey="Label_HeaderResource1"></asp:Label>
                    <span class="page-header-sub">
                        <asp:Label ID="Label_HeaderSub" runat="server" Text=""
                            meta:resourcekey="Label_HeaderSubResource1"></asp:Label>
                    </span>
                </div>
            </div>
            <div class="period-badge">
                <span class="meta-label"><asp:Label ID="lblCurrentPeriod" runat="server" Text="Current Period:"
                    meta:resourcekey="lblCurrentPeriodResource1"></asp:Label></span>
                <span class="meta-value"><asp:Label ID="lblCurrentPeriodValue" runat="server" Text="—"></asp:Label></span>
            </div>
        </div>

        <div class="Details">
            <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                meta:resourcekey="UltraWebTab1Resource1">
                <Tabs>
                    <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                        <ContentTemplate>
                            <div class="es-tab-content">

                                <div class="form-card">
                                    <asp:Label ID="lblMainInfo" runat="server" CssClass="hidden-server" Text="Main Info."
                                        meta:resourcekey="TabResource1"></asp:Label>
                                    <div class="form-card-body">
                                        <div class="filter-grid">
                                            <div class="filter-col">
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblCode" runat="server" Text="Employees Code"
                                                            meta:resourcekey="lblCodeResource1"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <div class="input-with-btn">
                                                            <asp:TextBox ID="txtCode" runat="server" MaxLength="30" CssClass="form-input"
                                                                meta:resourcekey="txtCodeResource1"></asp:TextBox>
                                                            <igtxt:WebImageButton ID="btnSearchCode" runat="server" Height="18px" AutoSubmit="False"
                                                                meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                Width="24px">
                                                                <Alignments TextImage="ImageBottom" />
                                                                <Appearance>
                                                                    <Image Url="./Img/forum_search.gif" />
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblDepartment" runat="server" meta:resourcekey="lblDepartmentResource1"
                                                            Text="Department"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-input"
                                                            meta:resourcekey="ddlDepartmentResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="Label_Contract" runat="server" meta:resourcekey="Label_ContractResource1"
                                                            Text="Contract Type"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <div class="input-with-btn">
                                                            <asp:TextBox ID="TextBox_Contract" runat="server" MaxLength="30" CssClass="form-input"></asp:TextBox>
                                                            <igtxt:WebImageButton ID="WebImageButton_Cont" runat="server" Height="18px" AutoSubmit="False"
                                                                meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                Width="24px">
                                                                <Alignments TextImage="ImageBottom" />
                                                                <Appearance>
                                                                    <Image Url="./Img/forum_search.gif" />
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="field-item" id="fltr_Row1" runat="server">
                                                    <span class="field-label">
                                                        <asp:Label ID="Label_Project" runat="server" Text="Employee Scope"
                                                            meta:resourcekey="Label_ProjectResource1"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:DropDownList ID="DropDownList_Project" runat="server" CssClass="form-input"
                                                            AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblFilter" runat="server" meta:resourcekey="lblFilterResource1"
                                                            Text="Filter Data"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:DropDownList ID="ddlFilter" runat="server" CssClass="form-input">
                                                            <asp:ListItem meta:resourcekey="AllDataRes" Text="All Data" Value="0"></asp:ListItem>
                                                            <asp:ListItem meta:resourcekey="PreparedOnlyRes" Text="Prepared Only" Value="1"></asp:ListItem>
                                                            <asp:ListItem meta:resourcekey="NotPreparedRes" Text="Not Prepared" Value="2"></asp:ListItem>
                                                            <asp:ListItem meta:resourcekey="UploadedDate" Text="Uploaded data" Value="3"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="filter-col">
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblCode1" runat="server" Text="Fisical Periods"
                                                            meta:resourcekey="lblCode1Resource1"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:DropDownList ID="DdlPeriods" runat="server" AutoPostBack="True" CssClass="form-input"
                                                            meta:resourcekey="DdlPeriodsResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblBranch" runat="server" meta:resourcekey="lblBranchResource1"
                                                            Text="Branch"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:DropDownList ID="ddlBranche" runat="server" CssClass="form-input"
                                                            meta:resourcekey="ddlBranchResource1" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="Label_Sponsor" runat="server" meta:resourcekey="Label_SponsorResource1"
                                                            Text="Sponsor"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <div class="input-with-btn">
                                                            <asp:TextBox ID="TextBox_Sponsor" runat="server" MaxLength="30" CssClass="form-input"></asp:TextBox>
                                                            <igtxt:WebImageButton ID="WebImageButton_Sponsor" runat="server" Height="18px" AutoSubmit="False"
                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px">
                                                                <Alignments TextImage="ImageBottom" />
                                                                <Appearance>
                                                                    <Image Url="./Img/forum_search.gif" />
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblNationality" runat="server" meta:resourcekey="lblNationalityResource1"
                                                            Text="Nationality"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:DropDownList ID="ddlNationality" runat="server" CssClass="form-input"
                                                            meta:resourcekey="ddlNationalityResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="field-item field-item-search-slot">
                                                    <span class="field-label">&nbsp;</span>
                                                    <div class="field-control">
                                                        <div class="btn-search-inline">
                                                            <igtxt:WebImageButton ID="btnFind" runat="server" Height="24px" meta:resourcekey="btnFindRes"
                                                                Overflow="NoWordWrap" Style="font-family: Tahoma; font-size: 11pt; font-weight: bold;
                                                                color: Black" Text=" Search " UseBrowserDefaults="False" Width="90px">
                                                                <Alignments TextImage="TextRightImageLeft" VerticalImage="Middle" />
                                                                <Appearance>
                                                                    <Image Url="./img/forum_search.gif" />
                                                                    <InnerBorder ColorBottom="160, 160, 160" ColorLeft="White" ColorRight="160, 160, 160"
                                                                        ColorTop="White" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid"
                                                                        WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <asp:Panel ID="pnlPeriodAlert" runat="server" CssClass="alert-banner" Visible="false">
                                    <div class="alert-icon">!</div>
                                    <div class="alert-body">
                                        <span class="alert-title">
                                            <asp:Label ID="lblPeriodAlertTitle" runat="server" Text=""
                                                meta:resourcekey="lblPeriodAlertTitleResource1"></asp:Label>
                                        </span>
                                        <span class="alert-sub">
                                            <asp:Label ID="lblPeriodAlertSub" runat="server" Text=""
                                                meta:resourcekey="lblPeriodAlertSubResource1"></asp:Label>
                                        </span>
                                    </div>
                                    <asp:LinkButton ID="lnkReviewNow" runat="server" CssClass="btn-review" Text="Review Now"
                                        meta:resourcekey="lnkReviewNowResource1" OnClick="lnkReviewNow_Click"></asp:LinkButton>
                                </asp:Panel>

                                <div class="form-card">
                                    <div class="form-card-body">
                                        <div class="grid-header-row">
                                            <span class="grid-title">
                                                <asp:Label ID="Label_Title1" runat="server" Text="Please select employees"
                                                    meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                            </span>
                                            <asp:ImageButton ID="ImageButton_Export" runat="server" CommandArgument="Import"
                                                Height="9px" ImageUrl="~/Pages/HR/Img/BttnExpnd.gif" meta:resourcekey="ImageButton_ImportResource1"
                                                Width="9px" />
                                        </div>
                                        <div class="grid-wrap">
                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="UwgSearchEmployees" runat="server" meta:resourcekey="uwgForNationalityResource1"
                                                SkinID="Default" Width="100%" Height="100%" EnableTheming="True">
                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="None" AllowDeleteDefault="No"
                                                    AllowSortingDefault="Yes"  AllowUpdateDefault="No" AutoGenerateColumns="False"
                                                    BorderCollapseDefault="Separate" HeaderClickActionDefault="Select" Name="uwgForNationality"
                                                    RowHeightDefault="22px" SelectTypeRowDefault="None" StationaryMargins="No" StationaryMarginsOutlookGroupBy="False"
                                                    TableLayout="Auto" Version="4.00" ViewType="Flat" AllowRowNumberingDefault="Continuous"
                                                    LoadOnDemand="Automatic">
                                                    <FrameStyle BackColor="Window" BorderColor="#d5dde8" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Names="Tahoma" Font-Size="9pt" Width="100%" Height="100%">
                                                    </FrameStyle>
                                                    <ClientSideEvents AfterCellUpdateHandler="UwgSearchEmployees_AfterCellUpdateHandler"
                                                        ClickCellButtonHandler="UwgSearchEmployees_ClickCellButtonHandler" />
                                                    <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                    </EditCellStyleDefault>
                                                    <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                    </FooterStyleDefault>
                                                    <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                                                        Height="24px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                    </HeaderStyleDefault>
                                                    <RowSelectorStyleDefault Width="40px" Font-Names="Arial" Font-Size="7pt">
                                                    </RowSelectorStyleDefault>
                                                    <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Names="tahoma" Font-Size="8pt" Height="22px">
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
                                                                <Header Caption="Employee Code">
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
                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="PaidSalary" Key="PaidSalary"
                                                                Width="100%" meta:resourcekey="PaidColumn">
                                                                <Header Caption="Paid Salary">
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
                                                                <Header Caption="Details">
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
                                        </div>
                                        <div class="grid-note">
                                            <asp:Label ID="lblGridNote" runat="server" Text=""
                                                meta:resourcekey="lblGridNoteResource1"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </ContentTemplate>
                    </igtab:Tab>
                </Tabs>
            </igtab:UltraWebTab>
        </div>
    </div>
    </form>
</body>
</html>
