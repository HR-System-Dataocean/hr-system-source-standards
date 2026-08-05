<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmActingEmployeeAssignment.aspx.vb"
    Inherits="frmActingEmployeeAssignment" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Acting Assignment</title>
    <script language="javascript" src="Scripts/App_JScript.js"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var actingDialog, actingSender;
        function OpenModal1(pageurl, height, width, checkID, senderCtrl) {
            actingSender = senderCtrl;
            actingDialog = $('<div></div>').html('<iframe style="border:0" src="' + pageurl + '" width="100%" height="100%"></iframe>')
                .dialog({ autoOpen: true, modal: true, height: height, width: width });
        }
        function CloseIt(value) {
            if (value && actingSender) {
                var sender = document.getElementById(actingSender);
                sender.value = value;
                sender.focus();
                if (sender.name) __doPostBack(sender.name, '');
            }
            if (actingDialog) actingDialog.dialog('close');
        }
        function aeaSwitchTab(idx) {
            var tab = igtab_getTabById('UltraWebTab1');
            if (tab) tab.setSelectedIndex(idx);
            $('.aea-tab').removeClass('active');
            $('.aea-tab[data-tab="' + idx + '"]').addClass('active');
            $('.aea-tab-panel').hide();
            $('#aeaPanel' + idx).show();
            return false;
        }
        $(function () {
            try {
                var tab = igtab_getTabById('UltraWebTab1');
                var idx = tab ? tab.getSelectedIndex() : 0;
                aeaSwitchTab(idx);
            } catch (e) {
                aeaSwitchTab(0);
            }
        });
    </script>
    <style type="text/css">
        * { box-sizing: border-box; }
        body {
            margin: 0; padding: 0;
            font-family: Tahoma, 'Segoe UI', Arial, sans-serif;
            font-size: 13px;
            background: #eef2f7;
            color: #1f3a5f;
        }
        .aea-wrap { padding: 12px 16px 20px; min-height: 100%; }
        .aea-toolbar {
            display: flex; align-items: center; flex-wrap: wrap; gap: 6px;
            background: #fff; border: 1px solid #d5e0ec; border-radius: 8px;
            padding: 8px 12px; margin-bottom: 14px;
        }
        .aea-toolbar .tb-spacer { flex: 1; }
        .aea-toolbar a, .aea-toolbar .tb-link {
            display: inline-flex; align-items: center; gap: 5px;
            color: #2c5282; text-decoration: none; font-size: 12px;
            padding: 4px 8px; border-radius: 4px;
        }
        .aea-toolbar a:hover { background: #eef5fc; }
        .aea-toolbar img { vertical-align: middle; }
        .aea-nav { display: inline-flex; gap: 2px; align-items: center; }

        .aea-header {
            display: flex; flex-wrap: wrap; align-items: flex-start;
            justify-content: space-between; gap: 16px; margin-bottom: 12px;
        }
        .aea-title-block { display: flex; align-items: flex-start; gap: 12px; min-width: 240px; flex: 1; }
        .aea-hero-icon {
            width: 48px; height: 48px; border-radius: 50%;
            background: linear-gradient(135deg, #3b82c4, #2563a8);
            display: flex; align-items: center; justify-content: center;
            color: #fff; flex-shrink: 0;
            box-shadow: 0 2px 8px rgba(37,99,168,.25);
            position: relative;
        }
        .aea-hero-icon:before {
            content: '';
            width: 16px; height: 16px; border: 2px solid #fff; border-radius: 50%;
            position: absolute; top: 12px;
        }
        .aea-hero-icon:after {
            content: '+';
            position: absolute; bottom: 8px; font-size: 16px; font-weight: 700; line-height: 1;
        }
        .aea-page-title {
            font-size: 22px; font-weight: 700; color: #1a365d; margin: 0 0 4px; line-height: 1.2;
        }
        .aea-page-sub { font-size: 12px; color: #718096; margin: 0; line-height: 1.45; max-width: 420px; }

        .aea-summary {
            display: flex; flex-wrap: wrap; gap: 10px; align-items: stretch;
        }
        .aea-summary-card {
            background: #fff; border: 1px solid #d0dde9; border-radius: 8px;
            padding: 10px 14px; min-width: 130px;
        }
        .aea-summary-card .lbl {
            display: block; font-size: 11px; color: #718096; margin-bottom: 4px;
        }
        .aea-summary-card .val {
            display: block; font-size: 14px; font-weight: 700; color: #1a365d;
        }

        .aea-status-pill {
            display: inline-flex; align-items: center; gap: 6px;
            font-weight: 700; font-size: 13px;
        }
        .aea-dot {
            width: 8px; height: 8px; border-radius: 50%; display: inline-block;
        }
        .aea-status-active { color: #1e7e34; }
        .aea-status-active .aea-dot { background: #22c55e; }
        .aea-status-ended { color: #64748b; }
        .aea-status-ended .aea-dot { background: #94a3b8; }
        .aea-status-cancelled { color: #c0392b; }
        .aea-status-cancelled .aea-dot { background: #ef4444; }
        .aea-status-pending { color: #b45309; }
        .aea-status-pending .aea-dot { background: #f59e0b; }

        .aea-tabs {
            display: flex; gap: 4px; margin-bottom: 14px; border-bottom: 1px solid #d5e0ec;
        }
        .aea-tab {
            padding: 9px 18px; font-size: 13px; font-weight: 600; color: #64748b;
            background: transparent; border: none; border-radius: 8px 8px 0 0;
            cursor: pointer; text-decoration: none;
        }
        .aea-tab.active {
            background: #e8f1fb; color: #2563a8;
        }

        .aea-main-grid {
            display: flex; flex-wrap: wrap; gap: 14px; align-items: flex-start;
            margin-bottom: 16px;
        }
        .aea-details {
            flex: 1 1 520px; min-width: 0;
            background: #fff; border: 1px solid #d0dde9; border-radius: 10px;
            padding: 16px 18px 18px;
        }
        .aea-details-head {
            display: flex; align-items: center; justify-content: space-between;
            gap: 10px; margin-bottom: 6px;
        }
        .aea-details-title { font-size: 16px; font-weight: 700; color: #1a365d; margin: 0; }
        .aea-details-sub { font-size: 12px; color: #718096; margin: 0 0 14px; }
        .aea-readonly-badge {
            background: #e8f1fb; color: #2563a8; border: 1px solid #b7d4ef;
            border-radius: 12px; padding: 3px 10px; font-size: 11px; font-weight: 700;
            white-space: nowrap;
        }

        .aea-section { margin-bottom: 18px; }
        .aea-section-head {
            display: flex; align-items: center; gap: 8px;
            font-size: 13px; font-weight: 700; color: #1a365d;
            padding-bottom: 8px; margin-bottom: 10px;
            border-bottom: 1px solid #e8eef5;
        }
        .aea-section-icon {
            width: 22px; height: 22px; border-radius: 6px;
            background: #e8f1fb; color: #2563a8;
            display: inline-flex; align-items: center; justify-content: center;
            font-size: 11px; font-weight: 700; flex-shrink: 0;
        }
        .aea-fields-row {
            display: flex; flex-wrap: wrap; gap: 10px;
        }
        .aea-field {
            flex: 1 1 160px; min-width: 140px;
            background: #f7fafc; border: 1px solid #dde5ee; border-radius: 8px;
            padding: 8px 12px 10px;
        }
        .aea-field.full { flex: 1 1 100%; }
        .aea-field-label {
            display: block; font-size: 11px; color: #718096; margin-bottom: 4px;
        }
        .aea-field-value, .aea-field input[type=text], .aea-field textarea {
            display: block; width: 100%; border: none !important; background: transparent !important;
            font-size: 13px !important; font-weight: 600; color: #1a365d !important;
            padding: 0 !important; margin: 0; font-family: inherit;
            box-shadow: none !important; outline: none;
        }
        .aea-field textarea { font-weight: 500 !important; min-height: 56px; resize: vertical; }
        .aea-hidden-ctrls { display: none !important; }

        .aea-sidebar {
            flex: 0 1 280px; width: 280px; max-width: 100%;
            background: #fff; border: 1px solid #d0dde9; border-radius: 10px;
            padding: 14px 16px;
        }
        .aea-sidebar-title {
            display: flex; align-items: center; gap: 8px;
            font-size: 14px; font-weight: 700; color: #1a365d; margin: 0 0 12px;
        }
        .aea-tx-row {
            display: flex; align-items: center; justify-content: space-between;
            gap: 8px; margin-bottom: 14px; flex-wrap: wrap;
        }
        .aea-tx-id { font-size: 13px; font-weight: 700; color: #1a365d; }
        .aea-badge {
            display: inline-flex; align-items: center; gap: 5px;
            border-radius: 12px; padding: 3px 10px; font-size: 11px; font-weight: 700;
        }
        .aea-badge-active { background: #e8f8ef; color: #1e7e34; border: 1px solid #a9dfbf; }
        .aea-badge-ended { background: #f1f5f9; color: #64748b; border: 1px solid #cbd5e1; }
        .aea-badge-cancelled { background: #fdecea; color: #c0392b; border: 1px solid #f5c6cb; }
        .aea-badge-pending { background: #fff7ed; color: #b45309; border: 1px solid #fcd34d; }

        .aea-timeline { position: relative; padding: 0 0 8px 0; margin: 0 0 14px; list-style: none; }
        .aea-timeline:before {
            content: ''; position: absolute; top: 8px; bottom: 8px;
            width: 2px; background: #dbe4ee;
        }
        [dir=ltr] .aea-timeline:before { left: 7px; }
        [dir=rtl] .aea-timeline:before { right: 7px; }
        .aea-timeline li {
            position: relative; padding-bottom: 14px;
        }
        [dir=ltr] .aea-timeline li { padding-left: 24px; }
        [dir=rtl] .aea-timeline li { padding-right: 24px; }
        .aea-timeline .tl-dot,
        span.tl-dot {
            position: absolute; top: 4px;
            display: inline-block;
            width: 12px; height: 12px;
            border-radius: 50%; border: 2px solid #fff;
            box-shadow: 0 0 0 2px #3b82c4; background: #3b82c4;
            font-size: 0; line-height: 0;
            overflow: hidden;
        }
        [dir=ltr] .aea-timeline .tl-dot { left: 1px; }
        [dir=rtl] .aea-timeline .tl-dot { right: 1px; }
        .aea-timeline .tl-dot.green {
            background: #22c55e; box-shadow: 0 0 0 2px #22c55e;
        }
        .aea-timeline .tl-dot.grey {
            background: #94a3b8; box-shadow: 0 0 0 2px #94a3b8;
        }
        .aea-timeline .tl-dot.red {
            background: #ef4444; box-shadow: 0 0 0 2px #ef4444;
        }
        .tl-title { font-size: 12px; font-weight: 700; color: #1a365d; margin-bottom: 2px; }
        .tl-meta { font-size: 11px; color: #718096; line-height: 1.4; }

        .aea-meta-list { margin: 0 0 12px; }
        .aea-meta-row {
            display: flex; justify-content: space-between; gap: 8px;
            padding: 6px 0; border-bottom: 1px solid #eef2f7; font-size: 12px;
        }
        .aea-meta-row .k { color: #718096; }
        .aea-meta-row .v { color: #1a365d; font-weight: 600; text-align: end; }

        .aea-info-box {
            background: #eef6fc; border: 1px solid #b7d4ef; border-radius: 8px;
            padding: 10px 12px; font-size: 11px; color: #1a5276; line-height: 1.5;
            display: flex; gap: 8px; align-items: flex-start;
        }
        .aea-info-box .i {
            width: 18px; height: 18px; border-radius: 50%; background: #3b82c4; color: #fff;
            display: inline-flex; align-items: center; justify-content: center;
            font-size: 11px; font-weight: 700; flex-shrink: 0;
        }

        .aea-records {
            background: #fff; border: 1px solid #d0dde9; border-radius: 10px;
            padding: 14px 16px 16px;
        }
        .aea-records-head {
            display: flex; flex-wrap: wrap; align-items: flex-start;
            justify-content: space-between; gap: 12px; margin-bottom: 12px;
        }
        .aea-records-title {
            display: flex; align-items: center; gap: 8px;
            font-size: 15px; font-weight: 700; color: #1a365d; margin: 0;
        }
        .aea-records-sub { font-size: 11px; color: #718096; margin: 4px 0 0; }
        .aea-filters { display: flex; flex-wrap: wrap; gap: 8px; align-items: center; }
        .aea-filters input[type=text], .aea-filters select {
            border: 1px solid #c9d7e6; border-radius: 6px; padding: 7px 10px;
            font-size: 12px; color: #1a365d; background: #fff; min-width: 140px;
        }
        .aea-filters input[type=text] { min-width: 220px; }

        .aea-table-wrap { overflow-x: auto; border: 1px solid #e2e8f0; border-radius: 8px; }
        .aea-table {
            width: 100%; border-collapse: collapse; font-size: 12px;
        }
        .aea-table th {
            background: #f1f5f9; color: #475569; font-weight: 700; font-size: 11px;
            text-transform: uppercase; letter-spacing: .02em;
            padding: 10px 12px; border-bottom: 1px solid #e2e8f0; white-space: nowrap;
        }
        .aea-table td {
            padding: 10px 12px; border-bottom: 1px solid #eef2f7;
            color: #1a365d; vertical-align: middle;
        }
        .aea-table tr:nth-child(even) td { background: #f8fbfe; }
        .aea-table tr:hover td { background: #eef6fc; }
        .aea-cell-main { font-weight: 600; }
        .aea-cell-sub { display: block; font-size: 11px; color: #718096; font-weight: 400; margin-top: 2px; }
        .aea-view-btn {
            display: inline-flex; align-items: center; gap: 5px;
            background: #fff; border: 1px solid #c9d7e6; border-radius: 6px;
            padding: 4px 10px; font-size: 12px; color: #2563a8; cursor: pointer;
            text-decoration: none; font-weight: 600;
        }
        .aea-view-btn:hover { background: #eef6fc; }
        .aea-empty {
            text-align: center; padding: 28px; color: #718096; font-size: 13px;
        }

        .aea-ig-tab-host { display: none !important; height: 0; overflow: hidden; }

        @media (max-width: 900px) {
            .aea-sidebar { flex: 1 1 100%; width: 100%; }
            .aea-filters input[type=text] { min-width: 160px; }
        }
    </style>
</head>
<body style="height:100%; margin:0; padding:0">
<form id="frmActingEmployeeAssignment" runat="server">
<div id="DIV" runat="server" class="Div_MasterContainer aea-wrap">

    <!-- Toolbar -->
    <div class="aea-toolbar">
        <span runat="server" visible="false">
            <asp:ImageButton ID="ImageButton_Save" runat="server" Width="16px" Height="16px" SkinID="HrSave_Command" CommandArgument="Save" meta:resourcekey="ImageButton_SaveResource1" />
            <asp:ImageButton ID="ImageButton_SaveN" runat="server" Width="16px" Height="16px" SkinID="HrSaveN_Command" CommandArgument="SaveNew" meta:resourcekey="ImageButton_SaveNResource1" />
            <asp:LinkButton ID="LinkButton_SaveN" runat="server" Text="Save With New" CommandArgument="SaveNew" meta:resourcekey="LinkButton_SaveNResource1" />
            <asp:ImageButton ID="ImageButton_New" runat="server" Width="16px" Height="16px" SkinID="HrNew_Command" CommandArgument="New" meta:resourcekey="ImageButton_NewResource1" />
            <asp:ImageButton ID="ImageButton_Delete" runat="server" Width="16px" Height="16px" SkinID="HrDelete_Command" CommandArgument="Delete" meta:resourcekey="ImageButton_DeleteResource1" />
            <asp:LinkButton ID="LinkButton_Delete" runat="server" Text="Cancel" CommandArgument="Delete" meta:resourcekey="LinkButton_DeleteResource1" />
        </span>
        <asp:ImageButton ID="ImageButton_Print" runat="server" Width="16px" Height="16px" SkinID="HrPrint_Command" CommandArgument="Print" meta:resourcekey="ImageButton_PrintResource1" />
        <asp:LinkButton ID="LinkButton_PrintText" runat="server" Text="Print" CommandArgument="Print" CssClass="tb-link" meta:resourcekey="LinkButton_PrintTextResource1" />
        <asp:ImageButton ID="ImageButton_Properties" runat="server" Width="16px" Height="16px" SkinID="HrProperties_Command" CommandArgument="Property" meta:resourcekey="ImageButton_PropertiesResource1" />
        <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="Properties" CommandArgument="Property" CssClass="tb-link" meta:resourcekey="LinkButton_PropertiesResource1" />
        <asp:ImageButton ID="ImageButton_Remarks" runat="server" Width="16px" Height="16px" SkinID="HrRemarks_Command" CommandArgument="Remarks" meta:resourcekey="ImageButton_RemarksResource1" />
        <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="Remarks" CommandArgument="Remarks" CssClass="tb-link" meta:resourcekey="LinkButton_RemarksResource1" />
        <span class="tb-spacer"></span>
        <span class="aea-nav">
            <asp:ImageButton ID="ImageButton_First" runat="server" Width="16px" Height="16px" SkinID="HrFirest_Command" CommandArgument="First" />
            <asp:ImageButton ID="ImageButton_Back" runat="server" Width="16px" Height="16px" SkinID="HrBack_Command" CommandArgument="Previous" />
            <asp:ImageButton ID="ImageButton_Next" runat="server" Width="16px" Height="16px" SkinID="HrNext_Command" CommandArgument="Next" />
            <asp:ImageButton ID="ImageButton_Last" runat="server" Width="16px" Height="16px" SkinID="HrLast_Command" CommandArgument="Last" />
        </span>
    </div>

    <!-- Page header + summary -->
    <div class="aea-header">
        <div class="aea-title-block">
            <div class="aea-hero-icon" aria-hidden="true"></div>
            <div>
                <h1 class="aea-page-title"><asp:Label ID="Label_Header" runat="server" Text="Employee Acting Assignment" meta:resourcekey="Label_HeaderResource1" /></h1>
                <p class="aea-page-sub"><asp:Label ID="lblSubtitle" runat="server" Text="View employee acting assignment details, effective period, status, and registration information." meta:resourcekey="lblSubtitleResource1" /></p>
            </div>
        </div>
        <div class="aea-summary">
            <div class="aea-summary-card">
                <span class="lbl"><asp:Literal ID="litSumTxn" runat="server" Text="Transaction No." meta:resourcekey="litSumTxnResource1" /></span>
                <span class="val"><asp:Label ID="lblSummaryCode" runat="server" Text="—" /></span>
            </div>
            <div class="aea-summary-card">
                <span class="lbl"><asp:Literal ID="litSumReg" runat="server" Text="Registered On" meta:resourcekey="litSumRegResource1" /></span>
                <span class="val"><asp:Label ID="lblSummaryRegDate" runat="server" Text="—" /></span>
            </div>
            <div class="aea-summary-card">
                <span class="lbl"><asp:Literal ID="litSumStatus" runat="server" Text="Status" meta:resourcekey="litSumStatusResource1" /></span>
                <span class="val">
                    <asp:Panel ID="spanSummaryStatus" runat="server" CssClass="aea-status-pill aea-status-active" style="display:inline-flex;align-items:center;gap:6px;">
                        <span class="aea-dot"></span>
                        <asp:Label ID="lblSummaryStatus" runat="server" Text="—" />
                    </asp:Panel>
                </span>
            </div>
        </div>
    </div>

    <!-- Custom tabs -->
    <div class="aea-tabs">
        <a href="#" class="aea-tab active" data-tab="0" onclick="return aeaSwitchTab(0);">
            <asp:Literal ID="litTabGeneral" runat="server" Text="General" meta:resourcekey="TabResource1" />
        </a>
        <a href="#" class="aea-tab" data-tab="1" onclick="return aeaSwitchTab(1);">
            <asp:Literal ID="litTabAudit" runat="server" Text="Audit Trail" meta:resourcekey="TabAuditResource1" />
        </a>
    </div>

    <!-- Hidden data fields (outside UltraWebTab so controls are always available) -->
    <asp:Panel ID="pnlHiddenData" runat="server" CssClass="aea-hidden-ctrls" style="display:none;">
        <asp:Label ID="lblCode" runat="server" Text="Code" meta:resourcekey="lblCodeResource1" />
        <asp:TextBox ID="txtCode" runat="server" Width="145px" AutoPostBack="True" MaxLength="30" Enabled="False" />
        <igtxt:WebImageButton ID="btnSearchCode" runat="server" AutoSubmit="False" Width="24px" Height="18px" Enabled="False"><Appearance><Image Url="./Img/forum_search.gif" /></Appearance></igtxt:WebImageButton>
        <asp:Label ID="lblOriginalEmployee" runat="server" Text="Employee Being Replaced" meta:resourcekey="lblOriginalEmployeeResource1" />
        <asp:TextBox ID="txtOriginalEmployee" runat="server" Width="145px" AutoPostBack="True" MaxLength="30" Enabled="False" />
        <igtxt:WebImageButton ID="btnOriginalEmployeeSearch" runat="server" AutoSubmit="False" Width="24px" Height="18px" Enabled="False"><Appearance><Image Url="./Img/forum_search.gif" /></Appearance></igtxt:WebImageButton>
        <asp:TextBox ID="txtOriginalEmployeeName" runat="server" Width="255px" CssClass="readonly" ReadOnly="True" Enabled="False" />
        <asp:Label ID="lblActingEmployee" runat="server" Text="Acting Employee" meta:resourcekey="lblActingEmployeeResource1" />
        <asp:TextBox ID="txtActingEmployee" runat="server" Width="145px" AutoPostBack="True" MaxLength="30" Enabled="False" />
        <igtxt:WebImageButton ID="btnActingEmployeeSearch" runat="server" AutoSubmit="False" Width="24px" Height="18px" Enabled="False"><Appearance><Image Url="./Img/forum_search.gif" /></Appearance></igtxt:WebImageButton>
        <asp:TextBox ID="txtActingEmployeeName" runat="server" Width="255px" CssClass="readonly" ReadOnly="True" Enabled="False" />
        <asp:Label ID="lblEffectiveFrom" runat="server" Text="Acting Start Date" meta:resourcekey="lblEffectiveFromResource1" />
        <igsch:WebDateChooser ID="txtEffectiveFrom" runat="server" Width="145px" Height="18px" Enabled="False" />
        <asp:Label ID="lblEffectiveTo" runat="server" Text="Acting End Date" meta:resourcekey="lblEffectiveToResource1" />
        <igsch:WebDateChooser ID="txtEffectiveTo" runat="server" Width="145px" Height="18px" Enabled="False" />
        <asp:Label ID="lblReason" runat="server" Text="Acting Reason" meta:resourcekey="lblReasonResource1" />
        <asp:TextBox ID="txtReason" runat="server" Width="400px" MaxLength="500" Enabled="False" />
        <asp:Label ID="lblRemarks" runat="server" Text="Remarks" meta:resourcekey="lblRemarksResource1" />
        <asp:TextBox ID="txtRemarks" runat="server" Width="400px" Height="50px" TextMode="MultiLine" MaxLength="1000" Enabled="False" />
        <igtbl:UltraWebGrid ID="uwgAssignments" runat="server" Width="100%" EnableAppStyling="False" AutoPostBack="True" Browser="UpLevel" Visible="False">
            <DisplayLayout Name="uwgAssignments" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
                CellClickActionDefault="RowSelect" SelectTypeRowDefault="Single" RowSelectorsDefault="No"
                TableLayout="Fixed" StationaryMargins="No" RowHeightDefault="18px"
                BorderCollapseDefault="Separate" Version="4.00">
                <FrameStyle BackColor="Window" BorderStyle="None" Font-Names="Tahoma" Font-Size="8pt" Width="100%"></FrameStyle>
                <HeaderStyleDefault BackColor="#DFDFDF" Font-Names="Tahoma" Font-Size="9pt" Height="22px" VerticalAlign="Middle" />
                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Font-Names="Tahoma" Font-Size="8pt" Height="18px" />
            </DisplayLayout>
            <Bands><igtbl:UltraGridBand>
                <Columns>
                    <igtbl:UltraGridColumn BaseColumnName="Code" Key="Code" Width="14%" meta:resourcekey="GridCodeResource1"><Header Caption="Transaction No." /></igtbl:UltraGridColumn>
                    <igtbl:UltraGridColumn BaseColumnName="OriginalEmployee" Key="OriginalEmployee" Width="28%" meta:resourcekey="GridOriginalEmployeeResource1"><Header Caption="Employee Being Replaced" /></igtbl:UltraGridColumn>
                    <igtbl:UltraGridColumn BaseColumnName="ActingEmployee" Key="ActingEmployee" Width="28%" meta:resourcekey="GridActingEmployeeResource1"><Header Caption="Acting Employee" /></igtbl:UltraGridColumn>
                    <igtbl:UltraGridColumn BaseColumnName="EffectiveFrom" Key="EffectiveFrom" DataType="System.DateTime" Format="dd/MM/yyyy" Width="15%" meta:resourcekey="GridEffectiveFromResource1"><Header Caption="Acting Start Date" /></igtbl:UltraGridColumn>
                    <igtbl:UltraGridColumn BaseColumnName="EffectiveTo" Key="EffectiveTo" DataType="System.DateTime" Format="dd/MM/yyyy" Width="15%" meta:resourcekey="GridEffectiveToResource1"><Header Caption="Acting End Date" /></igtbl:UltraGridColumn>
                </Columns>
            </igtbl:UltraGridBand></Bands>
        </igtbl:UltraWebGrid>
        <asp:Label ID="lblRegDate" runat="server" Text="Registered In" meta:resourcekey="lblRegDateResource1" />
        <asp:Label ID="lblRegDateValue" runat="server" />
        <asp:Label ID="lblRegUser" runat="server" Text="Registered By" meta:resourcekey="lblRegUserResource1" />
        <asp:Label ID="lblRegUserValue" runat="server" />
        <asp:Label ID="lblCancelDate" runat="server" Text="Cancel Date" meta:resourcekey="lblCancelDateResource1" />
        <asp:Label ID="lblCancelDateValue" runat="server" />
    </asp:Panel>

    <!-- Hidden UltraWebTab (kept for screen settings / compatibility) -->
    <div class="aea-ig-tab-host">
        <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default">
            <Tabs>
                <igtab:Tab Text="General" meta:resourcekey="TabResource1"><ContentTemplate>&nbsp;</ContentTemplate></igtab:Tab>
                <igtab:Tab Text="Audit Trail" meta:resourcekey="TabAuditResource1"><ContentTemplate>&nbsp;</ContentTemplate></igtab:Tab>
            </Tabs>
        </igtab:UltraWebTab>
    </div>

    <!-- General panel -->
    <div id="aeaPanel0" class="aea-tab-panel">
        <div class="aea-main-grid">
            <!-- Assignment details -->
            <div class="aea-details">
                <div class="aea-details-head">
                    <h2 class="aea-details-title"><asp:Literal ID="litDetailsTitle" runat="server" Text="Assignment Details" meta:resourcekey="litDetailsTitleResource1" /></h2>
                    <span class="aea-readonly-badge"><asp:Literal ID="litReadonly" runat="server" Text="Read-only" meta:resourcekey="litReadonlyResource1" /></span>
                </div>
                <p class="aea-details-sub"><asp:Literal ID="litDetailsSub" runat="server" Text="This screen is read-only. Information is displayed for review and reference." meta:resourcekey="litDetailsSubResource1" /></p>

                <!-- Replaced Employee Information -->
                <div class="aea-section">
                    <div class="aea-section-head">
                        <span class="aea-section-icon">R</span>
                        <asp:Literal ID="litSecReplaced" runat="server" Text="Replaced Employee Information" meta:resourcekey="litSecReplacedResource1" />
                    </div>
                    <div class="aea-fields-row">
                        <div class="aea-field">
                            <span class="aea-field-label"><asp:Literal ID="litFldReplacedEmp" runat="server" Text="Employee Being Replaced" meta:resourcekey="lblOriginalEmployeeResource1" /></span>
                            <asp:Label ID="lblReplacedEmployeeDisplay" runat="server" CssClass="aea-field-value" Text="—" />
                        </div>
                        <div class="aea-field">
                            <span class="aea-field-label"><asp:Literal ID="litFldReplacedPos" runat="server" Text="Current Position" meta:resourcekey="litFldCurrPosResource1" /></span>
                            <asp:Label ID="lblReplacedPositionDisplay" runat="server" CssClass="aea-field-value" Text="—" />
                        </div>
                        <div class="aea-field">
                            <span class="aea-field-label"><asp:Literal ID="litFldReplacedDept" runat="server" Text="Department" meta:resourcekey="litFldDeptResource1" /></span>
                            <asp:Label ID="lblReplacedDepartmentDisplay" runat="server" CssClass="aea-field-value" Text="—" />
                        </div>
                    </div>
                </div>

                <!-- Acting Employee Information -->
                <div class="aea-section">
                    <div class="aea-section-head">
                        <span class="aea-section-icon">A</span>
                        <asp:Literal ID="litSecActing" runat="server" Text="Acting Employee Information" meta:resourcekey="litSecActingResource1" />
                    </div>
                    <div class="aea-fields-row">
                        <div class="aea-field">
                            <span class="aea-field-label"><asp:Literal ID="litFldActingEmp" runat="server" Text="Acting Employee" meta:resourcekey="lblActingEmployeeResource1" /></span>
                            <asp:Label ID="lblActingEmployeeDisplay" runat="server" CssClass="aea-field-value" Text="—" />
                        </div>
                        <div class="aea-field">
                            <span class="aea-field-label"><asp:Literal ID="litFldActingPos" runat="server" Text="Current Position" meta:resourcekey="litFldCurrPosResource1" /></span>
                            <asp:Label ID="lblActingPositionDisplay" runat="server" CssClass="aea-field-value" Text="—" />
                        </div>
                        <div class="aea-field">
                            <span class="aea-field-label"><asp:Literal ID="litFldActingDept" runat="server" Text="Department" meta:resourcekey="litFldDeptResource1" /></span>
                            <asp:Label ID="lblActingDepartmentDisplay" runat="server" CssClass="aea-field-value" Text="—" />
                        </div>
                    </div>
                </div>

                <!-- Effective Period -->
                <div class="aea-section">
                    <div class="aea-section-head">
                        <span class="aea-section-icon">D</span>
                        <asp:Literal ID="litSecPeriod" runat="server" Text="Effective Period" meta:resourcekey="litSecPeriodResource1" />
                    </div>
                    <div class="aea-fields-row">
                        <div class="aea-field">
                            <span class="aea-field-label"><asp:Literal ID="litFldStart" runat="server" Text="Acting Start Date" meta:resourcekey="lblEffectiveFromResource1" /></span>
                            <asp:Label ID="lblStartDisplay" runat="server" CssClass="aea-field-value" Text="—" />
                        </div>
                        <div class="aea-field">
                            <span class="aea-field-label"><asp:Literal ID="litFldEnd" runat="server" Text="Acting End Date" meta:resourcekey="lblEffectiveToResource1" /></span>
                            <asp:Label ID="lblEndDisplay" runat="server" CssClass="aea-field-value" Text="—" />
                        </div>
                        <div class="aea-field">
                            <span class="aea-field-label"><asp:Literal ID="litFldDuration" runat="server" Text="Assignment Duration" meta:resourcekey="litFldDurationResource1" /></span>
                            <asp:Label ID="lblDurationDisplay" runat="server" CssClass="aea-field-value" Text="—" />
                        </div>
                    </div>
                </div>

                <!-- Reason and Remarks -->
                <div class="aea-section" style="margin-bottom:0">
                    <div class="aea-section-head">
                        <span class="aea-section-icon">N</span>
                        <asp:Literal ID="litSecReason" runat="server" Text="Reason and Remarks" meta:resourcekey="litSecReasonResource1" />
                    </div>
                    <div class="aea-fields-row" style="margin-bottom:10px">
                        <div class="aea-field full">
                            <span class="aea-field-label"><asp:Literal ID="litFldReason" runat="server" Text="Acting Reason" meta:resourcekey="lblReasonResource1" /></span>
                            <asp:Label ID="lblReasonDisplay" runat="server" CssClass="aea-field-value" Text="—" />
                        </div>
                    </div>
                    <div class="aea-fields-row">
                        <div class="aea-field full">
                            <span class="aea-field-label"><asp:Literal ID="litFldRemarks" runat="server" Text="Remarks" meta:resourcekey="lblRemarksResource1" /></span>
                            <asp:Label ID="lblRemarksDisplay" runat="server" CssClass="aea-field-value" Text="—" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Record Status sidebar -->
            <div class="aea-sidebar">
                <h3 class="aea-sidebar-title">
                    <span class="aea-section-icon">S</span>
                    <asp:Literal ID="litRecordStatus" runat="server" Text="Record Status" meta:resourcekey="litRecordStatusResource1" />
                </h3>
                <div class="aea-tx-row">
                    <span class="aea-tx-id">
                        <asp:Literal ID="litTxnPrefix" runat="server" Text="Transaction" meta:resourcekey="litTxnPrefixResource1" />
                        <asp:Label ID="lblSidebarCode" runat="server" Text="—" />
                    </span>
                    <asp:Label ID="lblSidebarBadge" runat="server" CssClass="aea-badge aea-badge-active" Text="—" />
                </div>

                <ul class="aea-timeline">
                    <li>
                        <span class="tl-dot"></span>
                        <div class="tl-title"><asp:Literal ID="litTlRegistered" runat="server" Text="Assignment Registered" meta:resourcekey="litTlRegisteredResource1" /></div>
                        <div class="tl-meta"><asp:Label ID="lblTlRegisteredMeta" runat="server" Text="—" /></div>
                    </li>
                    <li id="liTlActive" runat="server">
                        <asp:Label ID="spanTlActiveDot" runat="server" CssClass="tl-dot green" Text="" />
                        <div class="tl-title"><asp:Label ID="lblTlActiveTitle" runat="server" Text="Assignment Active" meta:resourcekey="litTlActiveResource1" /></div>
                        <div class="tl-meta"><asp:Label ID="lblTlActiveMeta" runat="server" Text="—" /></div>
                    </li>
                </ul>

                <div class="aea-meta-list">
                    <div class="aea-meta-row">
                        <span class="k"><asp:Literal ID="litMetaRegBy" runat="server" Text="Registered By" meta:resourcekey="lblRegUserResource1" /></span>
                        <span class="v"><asp:Label ID="lblMetaRegBy" runat="server" Text="—" /></span>
                    </div>
                    <div class="aea-meta-row">
                        <span class="k"><asp:Literal ID="litMetaRegOn" runat="server" Text="Registered On" meta:resourcekey="litSumRegResource1" /></span>
                        <span class="v"><asp:Label ID="lblMetaRegOn" runat="server" Text="—" /></span>
                    </div>
                    <div class="aea-meta-row">
                        <span class="k"><asp:Literal ID="litMetaCancel" runat="server" Text="Cancel Date" meta:resourcekey="lblCancelDateResource1" /></span>
                        <span class="v"><asp:Label ID="lblMetaCancel" runat="server" Text="—" /></span>
                    </div>
                    <div class="aea-meta-row">
                        <span class="k"><asp:Literal ID="litMetaMode" runat="server" Text="Record Mode" meta:resourcekey="litMetaModeResource1" /></span>
                        <span class="v"><asp:Literal ID="litModeValue" runat="server" Text="View Only" meta:resourcekey="litModeValueResource1" /></span>
                    </div>
                </div>

                <div class="aea-info-box">
                    <span class="i">i</span>
                    <asp:Literal ID="litInfoReadonly" runat="server" Text="No data can be added, edited, activated, or cancelled from this screen." meta:resourcekey="litInfoReadonlyResource1" />
                </div>
            </div>
        </div>

        <!-- Records table -->
        <div class="aea-records">
            <div class="aea-records-head">
                <div>
                    <h3 class="aea-records-title">
                        <span class="aea-section-icon">T</span>
                        <asp:Literal ID="litRecordsTitle" runat="server" Text="Acting Employee Assignment Records" meta:resourcekey="litRecordsTitleResource1" />
                    </h3>
                    <p class="aea-records-sub"><asp:Literal ID="litRecordsSub" runat="server" Text="Browse and open employee acting assignment transactions." meta:resourcekey="litRecordsSubResource1" /></p>
                </div>
                <div class="aea-filters">
                    <asp:DropDownList ID="ddlStatusFilter" runat="server" AutoPostBack="True">
                        <asp:ListItem Text="All Statuses" Value="0" meta:resourcekey="liAllStatusesResource1" />
                        <asp:ListItem Text="Active" Value="1" meta:resourcekey="liActiveResource1" />
                        <asp:ListItem Text="Ended" Value="2" meta:resourcekey="liEndedResource1" />
                        <asp:ListItem Text="Cancelled" Value="3" meta:resourcekey="liCancelledResource1" />
                    </asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" meta:resourcekey="txtSearchResource1" />
                </div>
            </div>

            <asp:Panel ID="pnlGridTable" runat="server" CssClass="aea-table-wrap">
                <table class="aea-table">
                    <thead>
                        <tr>
                            <th><asp:Literal ID="litColCode" runat="server" Text="Transaction No." meta:resourcekey="litColCodeResource1" /></th>
                            <th><asp:Literal ID="litColOriginalEmployee" runat="server" Text="Employee Being Replaced" meta:resourcekey="litColOriginalEmployeeResource1" /></th>
                            <th><asp:Literal ID="litColActingEmployee" runat="server" Text="Acting Employee" meta:resourcekey="litColActingEmployeeResource1" /></th>
                            <th><asp:Literal ID="litColStart" runat="server" Text="Start Date" meta:resourcekey="litColStartResource1" /></th>
                            <th><asp:Literal ID="litColEnd" runat="server" Text="End Date" meta:resourcekey="litColEndResource1" /></th>
                            <th><asp:Literal ID="litColReason" runat="server" Text="Reason" meta:resourcekey="litColReasonResource1" /></th>
                            <th><asp:Literal ID="litColStatus" runat="server" Text="Status" meta:resourcekey="litColStatusResource1" /></th>
                            <th><asp:Literal ID="litColView" runat="server" Text="View" meta:resourcekey="litColViewResource1" /></th>
                        </tr>
                    </thead>
                    <tbody>
                <asp:Repeater ID="rptAssignments" runat="server">
                    <ItemTemplate>
                                <tr>
                                    <td class="aea-cell-main"><%# Eval("Code") %></td>
                                    <td>
                                        <span class="aea-cell-main"><%# Eval("OriginalEmployeeDisplay") %></span>
                                        <span class="aea-cell-sub"><%# Eval("OriginalEmployeePosition") %></span>
                                    </td>
                                    <td>
                                        <span class="aea-cell-main"><%# Eval("ActingEmployeeDisplay") %></span>
                                        <span class="aea-cell-sub"><%# Eval("ActingEmployeePosition") %></span>
                                    </td>
                                    <td><%# Eval("StartText") %></td>
                                    <td><%# Eval("EndText") %></td>
                                    <td><%# Eval("Reason") %></td>
                                    <td>
                                        <span class='aea-status-pill <%# Eval("StatusCss") %>'>
                                            <span class="aea-dot"></span><%# Eval("StatusText") %>
                                        </span>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnView" runat="server" CssClass="aea-view-btn"
                                            CommandName="View" CommandArgument='<%# Eval("Code") %>'
                                            OnCommand="rptAssignments_ItemCommand"
                                            Text='<%# GetLocal("litColViewResource1.Text", "View") %>' />
                                    </td>
                                </tr>
                    </ItemTemplate>
                </asp:Repeater>
                    </tbody>
                </table>
            </asp:Panel>
                <asp:Panel ID="pnlEmptyGrid" runat="server" Visible="False" CssClass="aea-empty">
                    <asp:Literal ID="litEmptyGrid" runat="server" Text="No acting employee assignment records found." meta:resourcekey="litEmptyGridResource1" />
                </asp:Panel>
        </div>
    </div>

    <!-- Audit Trail panel -->
    <div id="aeaPanel1" class="aea-tab-panel" style="display:none">
        <div class="aea-details">
            <div class="aea-details-head">
                <h2 class="aea-details-title"><asp:Literal ID="litAuditTitle" runat="server" Text="Audit Trail" meta:resourcekey="TabAuditResource1" /></h2>
            </div>
            <p class="aea-details-sub"><asp:Literal ID="litAuditSub" runat="server" Text="Registration and status milestones for this transaction." meta:resourcekey="litAuditSubResource1" /></p>
            <ul class="aea-timeline" style="max-width:480px">
                <li>
                    <span class="tl-dot"></span>
                    <div class="tl-title"><asp:Literal ID="litAuditReg" runat="server" Text="Assignment Registered" meta:resourcekey="litTlRegisteredResource1" /></div>
                    <div class="tl-meta"><asp:Label ID="lblAuditRegMeta" runat="server" Text="—" /></div>
                </li>
                <li>
                    <asp:Label ID="spanAuditStatusDot" runat="server" CssClass="tl-dot green" Text="" />
                    <div class="tl-title"><asp:Label ID="lblAuditStatusTitle" runat="server" Text="—" /></div>
                    <div class="tl-meta"><asp:Label ID="lblAuditStatusMeta" runat="server" Text="—" /></div>
                </li>
            </ul>
        </div>
    </div>

</div>
</form>
</body>
</html>
