<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSelfServiceRequestsPopup.aspx.vb"
    Inherits="frmSelfServiceRequestsPopup" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <style type="text/css">
        body {
            font-family: Tahoma, Arial, sans-serif;
            font-size: 13px;
            padding: 16px;
            background: #eef2f7;
            margin: 0;
        }
        .page-title-bar {
            background: #2c3e50;
            color: #fff;
            padding: 14px 18px;
            border-radius: 8px 8px 0 0;
            font-size: 17px;
            font-weight: bold;
        }
        .clearance-bar {
            background: #d6e6f5;
            color: #1f3a5f;
            padding: 10px 18px;
            font-weight: bold;
            border-left: 1px solid #c5d6e8;
            border-right: 1px solid #c5d6e8;
        }
        .emp-card {
            background: #2c3e50;
            color: #fff;
            padding: 14px 18px;
            border-radius: 0 0 8px 8px;
            margin-bottom: 14px;
            display: flex;
            flex-wrap: wrap;
            gap: 24px;
        }
        .emp-card .emp-field {
            min-width: 180px;
        }
        .emp-card .emp-label {
            display: block;
            font-size: 11px;
            color: #b8c7d6;
            margin-bottom: 4px;
        }
        .emp-card .emp-value {
            font-size: 14px;
            font-weight: bold;
        }
        .summary-card {
            background: #fff;
            border: 1px solid #9ec3e6;
            border-radius: 8px;
            margin-bottom: 14px;
            overflow: hidden;
        }
        .summary-header {
            background: #2f80c1;
            color: #fff;
            padding: 10px 14px;
            display: flex;
            align-items: center;
            justify-content: space-between;
            font-weight: bold;
        }
        .completed-badge {
            background: #27ae60;
            color: #fff;
            border-radius: 12px;
            padding: 3px 10px;
            font-size: 11px;
        }
        .metrics-row {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            padding: 14px;
        }
        .metric-box {
            flex: 1 1 140px;
            border: 1px solid #9ec3e6;
            border-radius: 8px;
            padding: 12px 10px;
            text-align: center;
            background: #fff;
            min-width: 130px;
        }
        .metric-box .metric-number {
            font-size: 28px;
            font-weight: bold;
            color: #2f80c1;
            line-height: 1.1;
            margin-bottom: 6px;
        }
        .metric-box .metric-label {
            font-size: 11px;
            color: #445;
            line-height: 1.35;
        }
        .summary-desc {
            margin: 0 14px 14px 14px;
            background: #eaf4ff;
            border: 1px solid #b7d4ef;
            border-radius: 6px;
            padding: 10px 12px;
            color: #345;
            font-size: 12px;
            line-height: 1.5;
        }
        .handover-header {
            background: #2f80c1;
            color: #fff;
            padding: 10px 14px;
            border-radius: 8px 8px 0 0;
            display: flex;
            align-items: center;
            justify-content: space-between;
            font-weight: bold;
            margin-bottom: 0;
        }
        .actions-badge {
            background: #f0c14b;
            color: #3a2e00;
            border-radius: 16px;
            padding: 4px 12px;
            font-size: 12px;
            font-weight: bold;
            white-space: nowrap;
        }
        .handover-wrap {
            border: 1px solid #9ec3e6;
            border-top: none;
            border-radius: 0 0 8px 8px;
            padding: 14px;
            margin-bottom: 14px;
            background: #f8fbfe;
        }
        .card {
            background: #fff;
            border: 1px solid #9ec3e6;
            border-radius: 8px;
            margin-bottom: 14px;
            overflow: hidden;
            box-shadow: 0 1px 3px rgba(0,0,0,0.06);
        }
        .card-header {
            background: #e8f2fb;
            border-bottom: 1px solid #b7d4ef;
            padding: 10px 14px;
            display: flex;
            align-items: flex-start;
            justify-content: space-between;
            gap: 12px;
        }
        .card-header .left {
            flex: 1;
        }
        .card-title {
            font-weight: bold;
            color: #1f3a5f;
            font-size: 14px;
            margin-bottom: 4px;
        }
        .card-desc {
            color: #4a5a6a;
            font-size: 12px;
            line-height: 1.5;
        }
        .status-badge-ok {
            background: #e8f8ef;
            color: #1e7e34;
            border: 1px solid #a9dfbf;
            border-radius: 6px;
            padding: 6px 10px;
            font-size: 11px;
            font-weight: bold;
            white-space: nowrap;
        }
        .status-value {
            display: inline-block;
            min-width: 180px;
            padding: 6px 10px;
            background: #eaf4ff;
            border: 1px solid #9ec3e6;
            border-radius: 4px;
            color: #1a5276;
            font-weight: bold;
        }
        .required-mark {
            color: #d00;
            font-weight: bold;
        }
        .remarks-box {
            width: 100%;
            min-height: 70px;
            padding: 8px;
            border: 1px solid #c5ced9;
            border-radius: 4px;
            font-family: Tahoma, Arial, sans-serif;
            font-size: 12px;
            box-sizing: border-box;
        }
        .card-body {
            padding: 14px;
        }
        .field-row {
            display: flex;
            flex-wrap: wrap;
            align-items: center;
            gap: 10px;
            margin-bottom: 12px;
        }
        .field-label {
            font-weight: bold;
            color: #334;
            min-width: 180px;
        }
        .field-row input[type="text"] {
            padding: 6px 8px;
            border: 1px solid #c5ced9;
            border-radius: 4px;
            font-size: 12px;
        }
        .emp-name-display {
            display: inline-block;
            min-width: 260px;
            padding: 6px 10px;
            background: #f4f8fc;
            border: 1px solid #c9daf0;
            border-radius: 4px;
            color: #1a5276;
            font-weight: bold;
        }
        .readonly-value {
            display: inline-block;
            min-width: 220px;
            padding: 6px 10px;
            background: #f7f7f7;
            border: 1px solid #ddd;
            border-radius: 4px;
            color: #444;
        }
        .info-box {
            background: #fff8e6;
            border: 1px solid #f0d78c;
            border-radius: 6px;
            padding: 10px 12px;
            color: #6a5500;
            font-size: 12px;
            line-height: 1.5;
            margin-top: 4px;
        }
        .grid-card {
            background: #fff;
            border: 1px solid #d5dde8;
            border-radius: 8px;
            margin-bottom: 14px;
            overflow: hidden;
        }
        .grid-title {
            background: #3498db;
            color: #fff;
            padding: 10px 14px;
            font-weight: bold;
            font-size: 13px;
        }
        .grid-body {
            padding: 10px 12px;
        }
        .open-req-toolbar {
            display: flex;
            flex-wrap: wrap;
            gap: 8px;
            align-items: center;
            padding: 10px 12px;
            border-bottom: 1px solid #d5dde8;
            background: #f8fbfe;
        }
        .open-req-toolbar .selected-count {
            color: #555;
            font-size: 12px;
            margin-inline-end: auto;
        }
        .btn-select-all {
            background: #d6eaf8 !important;
            color: #1a5276 !important;
            border: 1px solid #85c1e9 !important;
            padding: 5px 12px !important;
            border-radius: 3px;
            font-size: 12px !important;
            cursor: pointer;
        }
        .btn-cancel-sel {
            background: #fdecea !important;
            color: #c0392b !important;
            border: 1px solid #e74c3c !important;
            padding: 5px 12px !important;
            border-radius: 3px;
            font-size: 12px !important;
            cursor: pointer;
        }
        .btn-reject-sel {
            background: #fef5e7 !important;
            color: #b9770e !important;
            border: 1px solid #f39c12 !important;
            padding: 5px 12px !important;
            border-radius: 3px;
            font-size: 12px !important;
            cursor: pointer;
        }
        .btn-refresh-open {
            background: #27ae60 !important;
            color: #fff !important;
            border: 1px solid #1e8449 !important;
            padding: 5px 14px !important;
            border-radius: 3px;
            font-size: 12px !important;
            cursor: pointer;
            font-weight: bold;
        }
        .open-req-grid {
            width: 100%;
            border-collapse: collapse;
            font-size: 12px;
        }
        .open-req-grid th {
            background: #fef9e7;
            color: #1a5276;
            padding: 8px 6px;
            border: 1px solid #aed6f1;
            text-align: center;
            white-space: nowrap;
        }
        .open-req-grid td {
            padding: 7px 6px;
            border: 1px solid #d5dde8;
            vertical-align: middle;
            text-align: center;
        }
        .open-req-grid tr:nth-child(even) td {
            background: #f7fbfe;
        }
        .status-badge-pending {
            display: inline-block;
            background: #f9e79f;
            color: #7d6608;
            border-radius: 12px;
            padding: 3px 10px;
            font-size: 11px;
            font-weight: bold;
            white-space: nowrap;
        }
        .btn-view-req {
            background: #fff !important;
            color: #2980b9 !important;
            border: 1px solid #3498db !important;
            padding: 3px 8px !important;
            border-radius: 3px;
            font-size: 11px !important;
            cursor: pointer;
            margin: 1px;
        }
        .btn-cancel-req {
            background: #fff !important;
            color: #c0392b !important;
            border: 1px solid #e74c3c !important;
            padding: 3px 8px !important;
            border-radius: 3px;
            font-size: 11px !important;
            cursor: pointer;
            margin: 1px;
        }
        .btn-reject-req {
            background: #fff !important;
            color: #b9770e !important;
            border: 1px solid #f39c12 !important;
            padding: 3px 8px !important;
            border-radius: 3px;
            font-size: 11px !important;
            cursor: pointer;
            margin: 1px;
        }
        .open-req-info {
            margin: 10px 12px 12px 12px;
            background: #eaf4ff;
            border: 1px solid #5dade2;
            border-radius: 6px;
            padding: 10px 12px;
            color: #1a5276;
            font-size: 12px;
            line-height: 1.5;
        }
        table {
            width: 100%;
            border-collapse: collapse;
        }
        th {
            background: #ecf0f1;
            padding: 8px;
            border: 1px solid #ddd;
            font-weight: bold;
        }
        td {
            padding: 8px;
            border: 1px solid #ddd;
        }
        tr:nth-child(even) { background: #f9f9f9; }
        .count-badge {
            display: inline-block;
            background: #e74c3c;
            color: #fff;
            border-radius: 50%;
            padding: 2px 8px;
            font-size: 11px;
            margin-inline-start: 8px;
        }
        .footer {
            margin-top: 8px;
            padding-top: 12px;
            border-top: 1px solid #d5dde8;
            display: flex;
            gap: 10px;
            flex-wrap: wrap;
            justify-content: flex-end;
            align-items: center;
        }
        .remarks-actions {
            margin-top: 12px;
            padding-top: 12px;
            border-top: 1px solid #d5dde8;
            display: flex;
            justify-content: flex-end;
            gap: 10px;
            flex-wrap: wrap;
        }
        input.btn-transfer,
        .btn-transfer {
            background: #3498db !important;
            background-color: #3498db !important;
            background-image: none !important;
            color: #ffffff !important;
            border: 1px solid #2176b3 !important;
            box-shadow: inset 0 0 0 1px rgba(255,255,255,0.45);
            padding: 10px 22px !important;
            border-radius: 3px;
            cursor: pointer;
            font-size: 13px !important;
            font-weight: bold !important;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
        }
        input.btn-transfer:hover,
        .btn-transfer:hover {
            background: #2980b9 !important;
            background-color: #2980b9 !important;
            border-color: #1f6ea0 !important;
        }
        .btn-close {
            background: #e74c3c;
            color: #fff;
            border: none;
            padding: 9px 20px;
            border-radius: 4px;
            cursor: pointer;
            font-size: 13px;
        }
        .btn-close:hover { background: #c0392b; }
        .btn-refresh {
            background: #2ecc71;
            color: #fff;
            border: none;
            padding: 9px 20px;
            border-radius: 4px;
            cursor: pointer;
            font-size: 13px;
        }
        .btn-refresh:hover { background: #27ae60; }
        .custody-section {
            background: #fff;
            border: 1px solid #9ec3e6;
            border-radius: 8px;
            margin-bottom: 14px;
            overflow: hidden;
        }
        .custody-header {
            background: #2f80c1;
            color: #fff;
            padding: 10px 14px;
            display: flex;
            align-items: center;
            justify-content: space-between;
            font-weight: bold;
        }
        .custody-header-left {
            display: flex;
            align-items: center;
            gap: 8px;
        }
        .custody-count {
            background: #f1c40f;
            color: #3a2e00;
            border-radius: 50%;
            min-width: 24px;
            height: 24px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            font-size: 12px;
            font-weight: bold;
            padding: 0 6px;
        }
        .custody-metrics {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            padding: 12px 14px;
        }
        .custody-metric {
            flex: 1 1 140px;
            border: 1px solid #b7d4ef;
            border-radius: 6px;
            overflow: hidden;
            background: #fff;
            min-width: 130px;
        }
        .custody-metric-top {
            padding: 10px 12px 6px 12px;
            background: #eaf4ff;
        }
        .custody-metric-number {
            font-size: 26px;
            font-weight: bold;
            color: #2f80c1;
            line-height: 1.1;
        }
        .custody-metric-caption {
            font-size: 12px;
            color: #345;
            margin-top: 2px;
        }
        .custody-metric-sub {
            background: #fef5e7;
            padding: 6px 12px;
            font-size: 11px;
            color: #7d6608;
            border-top: 1px solid #f5e6c8;
        }
        .custody-toolbar {
            display: flex;
            flex-wrap: wrap;
            gap: 8px;
            padding: 0 14px 12px 14px;
            align-items: center;
        }
        .custody-search {
            flex: 1 1 220px;
            min-width: 180px;
            padding: 7px 10px;
            border: 1px solid #c5d6e8;
            border-radius: 4px;
            font-size: 12px;
        }
        .custody-filter {
            padding: 7px 10px;
            border: 1px solid #c5d6e8;
            border-radius: 4px;
            font-size: 12px;
            min-width: 140px;
        }
        .btn-mark-all {
            background: #fff !important;
            color: #345 !important;
            border: 1px solid #b0bcc8 !important;
            padding: 7px 12px !important;
            border-radius: 4px;
            cursor: pointer;
            font-size: 12px !important;
            white-space: nowrap;
        }
        .btn-mark-all:hover {
            background: #f4f7fb !important;
        }
        .custody-body {
            padding: 0 14px 14px 14px;
            overflow-x: auto;
        }
        .custody-grid {
            width: 100%;
            border-collapse: collapse;
            font-size: 12px;
        }
        .custody-grid th {
            background: #d6eaf8;
            color: #1a5276;
            padding: 8px 6px;
            border: 1px solid #aed6f1;
            text-align: center;
            white-space: nowrap;
        }
        .custody-grid td {
            padding: 7px 6px;
            border: 1px solid #d5dde8;
            vertical-align: middle;
            text-align: center;
        }
        .custody-grid tr:nth-child(even) td {
            background: #f7fbfe;
        }
        .status-pending {
            display: inline-block;
            background: #f9e79f;
            color: #7d6608;
            border-radius: 12px;
            padding: 3px 10px;
            font-size: 11px;
            font-weight: bold;
            white-space: nowrap;
        }
        .status-returned {
            display: inline-block;
            background: #d5f5e3;
            color: #1e8449;
            border-radius: 12px;
            padding: 3px 10px;
            font-size: 11px;
            font-weight: bold;
            white-space: nowrap;
        }
        .btn-confirm-return {
            background: #27ae60 !important;
            color: #fff !important;
            border: 1px solid #1e8449 !important;
            padding: 5px 10px !important;
            border-radius: 3px;
            cursor: pointer;
            font-size: 11px !important;
            white-space: nowrap;
        }
        .btn-confirm-return:hover {
            background: #1e8449 !important;
        }
        .btn-completed {
            background: #bdc3c7 !important;
            color: #fff !important;
            border: 1px solid #95a5a6 !important;
            padding: 5px 10px !important;
            border-radius: 3px;
            font-size: 11px !important;
            cursor: default;
        }
        .custody-date,
        .custody-condition {
            width: 110px;
            padding: 4px 6px;
            border: 1px solid #c5d6e8;
            border-radius: 3px;
            font-size: 11px;
        }
        .custody-warning {
            margin: 0 14px 14px 14px;
            background: #fef5e7;
            border: 1px solid #f5b041;
            border-radius: 6px;
            padding: 10px 12px;
            color: #7d6608;
            font-size: 12px;
            line-height: 1.5;
        }
        .msg {
            margin-top: 10px;
            padding: 8px 12px;
            border-radius: 4px;
            display: none;
        }
        .msg.show { display: block; }
        .msg-ok {
            background: #d5f5e3;
            color: #1e8449;
            border: 1px solid #82e0aa;
        }
        .msg-err {
            background: #fadbd8;
            color: #922b21;
            border: 1px solid #f1948a;
        }
    </style>
</head>
<body id="pageBody" runat="server">
    <form id="form1" runat="server">
        <script type="text/javascript">
            var ODialoge;
            var OSender;

            function OpenModal1(pageurl, height, width, CheckID, SenderCtrl) {
                if (CheckID == false) {
                    var $dialog = $('<div></div>')
                        .html('<iframe style="border: 0px; " src="' + pageurl + '" width="100%" height="100%"></iframe>')
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
                var Sender = window.document.getElementById(OSender);
                if (retvalue != "") {
                    Sender.value = retvalue;
                }
                var $dialog = ODialoge;
                $dialog.dialog('close');
                if (retvalue != "" && Sender != null) {
                    __doPostBack(Sender.name, '');
                }
            }
            function updateOpenRequestSelection() {
                var boxes = document.querySelectorAll('.chk-open-req');
                var n = 0;
                for (var i = 0; i < boxes.length; i++) {
                    if (boxes[i].checked) n++;
                }
                var lbl = document.getElementById('<%= lblOpenSelectedCount.ClientID %>');
                if (lbl) {
                    lbl.innerHTML = n + ' <%= GetLocalResourceObject("lblSelectedCountSuffix") %>';
                }
                var master = document.getElementById('chkOpenSelectAllHeader');
                if (master) {
                    master.checked = (boxes.length > 0 && n === boxes.length);
                }
            }
            function toggleOpenSelectAll(src) {
                var boxes = document.querySelectorAll('.chk-open-req');
                for (var i = 0; i < boxes.length; i++) {
                    boxes[i].checked = src.checked;
                }
                updateOpenRequestSelection();
            }
        </script>

        <div class="page-title-bar">
            <asp:Label ID="lblPageTitle" runat="server" meta:resourcekey="lblPageTitleResource1" />
        </div>
        <div class="clearance-bar">
            <asp:Label ID="lblEmployeeHeader" runat="server" meta:resourcekey="lblEmployeeHeaderResource1" />
        </div>
        <div class="emp-card">
            <div class="emp-field">
                <span class="emp-label"><asp:Label ID="lblEmployeeCodeCaption" runat="server" meta:resourcekey="lblEmployeeCodeCaptionResource1" /></span>
                <span class="emp-value"><asp:Label ID="lblEmpCode" runat="server" /></span>
            </div>
            <div class="emp-field">
                <span class="emp-label"><asp:Label ID="lblEmployeeNameCaption" runat="server" meta:resourcekey="lblEmployeeNameCaptionResource1" /></span>
                <span class="emp-value"><asp:Label ID="lblEmpName" runat="server" /></span>
            </div>
            <div class="emp-field">
                <span class="emp-label"><asp:Label ID="lblLastWorkingDateCaption" runat="server" meta:resourcekey="lblLastWorkingDateCaptionResource1" /></span>
                <span class="emp-value"><asp:Label ID="lblLastWorkingDate" runat="server" /></span>
            </div>
        </div>

        <div class="summary-card">
            <div class="summary-header">
                <asp:Label ID="lblSummaryTitle" runat="server" meta:resourcekey="lblSummaryTitleResource1" />
                <asp:Label ID="lblSummaryCompleted" runat="server" CssClass="completed-badge" meta:resourcekey="lblSummaryCompletedResource1" />
            </div>
            <div class="metrics-row">
                <div class="metric-box">
                    <div class="metric-number"><asp:Label ID="lblMetricPending" runat="server" Text="0" /></div>
                    <div class="metric-label"><asp:Label ID="lblMetricPendingCaption" runat="server" meta:resourcekey="lblMetricPendingCaptionResource1" /></div>
                </div>
                <div class="metric-box">
                    <div class="metric-number"><asp:Label ID="lblMetricRules" runat="server" Text="0" /></div>
                    <div class="metric-label"><asp:Label ID="lblMetricRulesCaption" runat="server" meta:resourcekey="lblMetricRulesCaptionResource1" /></div>
                </div>
                <div class="metric-box">
                    <div class="metric-number"><asp:Label ID="lblMetricReports" runat="server" Text="0" /></div>
                    <div class="metric-label"><asp:Label ID="lblMetricReportsCaption" runat="server" meta:resourcekey="lblMetricReportsCaptionResource1" /></div>
                </div>
                <div class="metric-box">
                    <div class="metric-number"><asp:Label ID="lblMetricCustody" runat="server" Text="0" /></div>
                    <div class="metric-label"><asp:Label ID="lblMetricCustodyCaption" runat="server" meta:resourcekey="lblMetricCustodyCaptionResource1" /></div>
                </div>
                <div class="metric-box">
                    <div class="metric-number"><asp:Label ID="lblMetricActions" runat="server" Text="0" /></div>
                    <div class="metric-label"><asp:Label ID="lblMetricActionsCaption" runat="server" meta:resourcekey="lblMetricActionsCaptionResource1" /></div>
                </div>
            </div>
            <div class="summary-desc">
                <asp:Label ID="lblSummaryDesc" runat="server" meta:resourcekey="lblSummaryDescResource1" />
            </div>
        </div>

        <div class="handover-header">
            <asp:Label ID="lblHandoverTitle" runat="server" meta:resourcekey="lblHandoverTitleResource1" />
            <asp:Label ID="lblActionsBadge" runat="server" CssClass="actions-badge" />
        </div>
        <div class="handover-wrap">

        <!-- Direct Manager Responsibilities -->
        <div class="card" id="cardDirectManager" runat="server" visible="false">
            <div class="card-header">
                <div class="left">
                    <div class="card-title">
                        <asp:Label ID="lblDirectManagerSectionTitle" runat="server" meta:resourcekey="lblDirectManagerSectionTitleResource1" />
                    </div>
                    <div class="card-desc">
                        <asp:Label ID="lblDirectManagerSectionDesc" runat="server" />
                    </div>
                </div>
                <asp:Label ID="lblDirectManagerBadge" runat="server" CssClass="status-badge" />
            </div>
            <div class="card-body">
                <div class="field-row">
                    <asp:Label ID="lblNewDirectManager" runat="server" CssClass="field-label"
                        AssociatedControlID="txtNewManagerCode" meta:resourcekey="lblNewDirectManagerResource1" />
                    <span class="required-mark">*</span>
                    <asp:TextBox ID="txtNewManagerCode" runat="server"
                        Width="100px" MaxLength="30" AutoPostBack="True"
                        OnTextChanged="txtNewManagerCode_TextChanged" />
                    <asp:ImageButton ID="btnSearchNewManager" runat="server"
                        ImageUrl="./Img/forum_search.gif" Width="24px" Height="18px"
                        meta:resourcekey="btnSearchNewManagerResource1" />
                    <asp:Label ID="lblNewManagerName" runat="server" CssClass="emp-name-display" />
                </div>
                <div class="field-row">
                    <asp:Label ID="lblEmployeeHistoryUpdate" runat="server" CssClass="field-label" meta:resourcekey="lblEmployeeHistoryUpdateResource1" />
                    <asp:Label ID="lblEmployeeHistoryUpdateValue" runat="server" CssClass="status-value" meta:resourcekey="lblEmployeeHistoryUpdateValueResource1" />
                </div>
                <div class="info-box">
                    <asp:Label ID="lblDirectManagerInfo" runat="server" />
                </div>
            </div>
        </div>

        <!-- Automatic Approval Handling -->
        <div class="card" id="cardAutoApproval" runat="server" visible="false">
            <div class="card-header">
                <div class="left">
                    <div class="card-title">
                        <asp:Label ID="lblAutoApprovalSectionTitle" runat="server" meta:resourcekey="lblAutoApprovalSectionTitleResource1" />
                    </div>
                    <div class="card-desc">
                        <asp:Label ID="lblAutoApprovalSectionDesc" runat="server" meta:resourcekey="lblAutoApprovalSectionDescResource1" />
                    </div>
                </div>
                <asp:Label ID="lblAutoApprovalStatus" runat="server" CssClass="status-badge-ok" meta:resourcekey="lblNoActionRequiredResource1" />
            </div>
            <div class="card-body">
                <div class="info-box">
                    <asp:Label ID="lblAutoApprovalInfo" runat="server" meta:resourcekey="lblAutoApprovalInfoResource1" />
                </div>
            </div>
        </div>

        <!-- Section 1: Replacement Approver -->
        <div class="card" id="cardReplacement" runat="server">
            <div class="card-header">
                <div class="left">
                    <div class="card-title">
                        <asp:Label ID="lblReplacementSectionTitle" runat="server" meta:resourcekey="lblReplacementSectionTitleResource1" />
                    </div>
                    <div class="card-desc">
                        <asp:Label ID="lblReplacementSectionDesc" runat="server" meta:resourcekey="lblReplacementSectionDescResource1" />
                    </div>
                </div>
                <asp:Label ID="lblReplacementStatus" runat="server" CssClass="status-badge" meta:resourcekey="lblUserActionRequiredResource1" />
            </div>
            <div class="card-body">
                <div class="field-row">
                    <asp:Label ID="lblReplacementEmployee" runat="server" CssClass="field-label"
                        AssociatedControlID="txtReplacementEmpCode" meta:resourcekey="lblReplacementEmployeeResource1" />
                    <asp:TextBox ID="txtReplacementEmpCode" runat="server"
                        Width="100px" MaxLength="30" AutoPostBack="True"
                        OnTextChanged="txtReplacementEmpCode_TextChanged" />
                    <asp:ImageButton ID="btnSearchReplacementEmp" runat="server"
                        ImageUrl="./Img/forum_search.gif" Width="24px" Height="18px"
                        meta:resourcekey="btnSearchReplacementEmpResource1" />
                    <asp:Label ID="lblReplacementEmpName" runat="server" CssClass="emp-name-display" />
                </div>
                <div class="field-row">
                    <asp:Label ID="lblEffectiveFrom" runat="server" CssClass="field-label" meta:resourcekey="lblEffectiveFromResource1" />
                    <igsch:WebDateChooser ID="txtEffectiveFrom" runat="server" Width="145px" Height="18px" />
                    <asp:Label ID="lblEffectiveTo" runat="server" CssClass="field-label" Visible="False" meta:resourcekey="lblEffectiveToResource1" />
                    <igsch:WebDateChooser ID="txtEffectiveTo" runat="server" Width="145px" Height="18px" Visible="False" />
                </div>
                <div class="info-box">
                    <asp:Label ID="lblReplacementInfo" runat="server" meta:resourcekey="lblReplacementInfoResource1" />
                </div>
            </div>
        </div>

        <!-- Section 2: Pending Requests Delegation -->
        <div class="card" id="cardDelegate" runat="server">
            <div class="card-header">
                <div class="left">
                    <div class="card-title">
                        <asp:Label ID="lblDelegateSectionTitle" runat="server" meta:resourcekey="lblDelegateSectionTitleResource1" />
                    </div>
                    <div class="card-desc">
                        <asp:Label ID="lblDelegateSectionDesc" runat="server" meta:resourcekey="lblDelegateSectionDescResource1" />
                    </div>
                </div>
                <asp:Label ID="lblDelegateStatus" runat="server" CssClass="status-badge" meta:resourcekey="lblUserActionRequiredResource1" />
            </div>
            <div class="card-body">
                <div class="field-row">
                    <asp:CheckBox ID="chkSameEmployee" runat="server" AutoPostBack="True"
                        OnCheckedChanged="chkSameEmployee_CheckedChanged"
                        meta:resourcekey="chkSameEmployeeResource1" />
                </div>
                <div class="field-row" id="rowDelegate" runat="server">
                    <asp:Label ID="lblDelegateEmployee" runat="server" CssClass="field-label"
                        AssociatedControlID="txtDelegateEmpCode" meta:resourcekey="lblDelegateEmployeeResource1" />
                    <asp:TextBox ID="txtDelegateEmpCode" runat="server"
                        Width="100px" MaxLength="30" AutoPostBack="True"
                        OnTextChanged="txtDelegateEmpCode_TextChanged" />
                    <asp:ImageButton ID="btnSearchDelegateEmp" runat="server"
                        ImageUrl="./Img/forum_search.gif" Width="24px" Height="18px"
                        meta:resourcekey="btnSearchDelegateEmpResource1" />
                    <asp:Label ID="lblDelegateEmpName" runat="server" CssClass="emp-name-display" />
                </div>
                <div class="field-row">
                    <asp:Label ID="lblHandlingMethod" runat="server" CssClass="field-label" meta:resourcekey="lblHandlingMethodResource1" />
                    <asp:Label ID="lblHandlingMethodValue" runat="server" CssClass="readonly-value" meta:resourcekey="lblHandlingMethodValueResource1" />
                </div>
            </div>
        </div>

        <asp:Label ID="lblTransferMessage" runat="server" CssClass="msg" />
        <asp:HiddenField ID="hdnSourceEmployeeID" runat="server" />
        <asp:HiddenField ID="hdnReplacementEmployeeID" runat="server" />
        <asp:HiddenField ID="hdnDelegateEmployeeID" runat="server" />
        <asp:HiddenField ID="hdnNewManagerID" runat="server" />
        <asp:HiddenField ID="hdnSubordinateCount" runat="server" Value="0" />

        <div class="card">
            <div class="card-body">
                <div class="field-row" style="align-items:flex-start;">
                    <asp:Label ID="lblRemarks" runat="server" CssClass="field-label" meta:resourcekey="lblRemarksResource1" />
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="remarks-box" Rows="3" />
                </div>
                <div class="field-row">
                    <asp:Label ID="lblApprovalConfig" runat="server" CssClass="field-label" meta:resourcekey="lblApprovalConfigResource1" />
                    <asp:Label ID="lblApprovalConfigValue" runat="server" CssClass="status-value" meta:resourcekey="lblApprovalConfigValueResource1" />
                </div>
                <div class="remarks-actions">
                    <asp:Button ID="btnTransferApprovals" runat="server"
                        CssClass="btn-transfer" OnClick="btnTransferApprovals_Click"
                        BackColor="#3498db" ForeColor="White"
                        BorderColor="#2176b3" BorderStyle="Solid" BorderWidth="1px"
                        Font-Bold="True"
                        meta:resourcekey="btnTransferApprovalsResource1" />
                </div>
            </div>
        </div>

        </div><!-- handover-wrap -->

        <div class="custody-section" id="pnlCustodySection" runat="server">
            <div class="custody-header">
                <div class="custody-header-left">
                    <asp:Label ID="lblCustodyTitle" runat="server" meta:resourcekey="lblCustodyTitleResource1" />
                </div>
                <asp:Label ID="lblCustodyCountBadge" runat="server" CssClass="custody-count" Text="0" />
            </div>
            <div class="custody-metrics">
                <div class="custody-metric">
                    <div class="custody-metric-top">
                        <div class="custody-metric-number"><asp:Label ID="lblCustodyTotal" runat="server" Text="0" /></div>
                        <div class="custody-metric-caption"><asp:Label ID="lblCustodyTotalCaption" runat="server" meta:resourcekey="lblCustodyTotalCaptionResource1" /></div>
                    </div>
                    <div class="custody-metric-sub"><asp:Label ID="lblCustodyTotalSub" runat="server" meta:resourcekey="lblCustodyTotalSubResource1" /></div>
                </div>
                <div class="custody-metric">
                    <div class="custody-metric-top">
                        <div class="custody-metric-number"><asp:Label ID="lblCustodyPending" runat="server" Text="0" /></div>
                        <div class="custody-metric-caption"><asp:Label ID="lblCustodyPendingCaption" runat="server" meta:resourcekey="lblCustodyPendingCaptionResource1" /></div>
                    </div>
                    <div class="custody-metric-sub"><asp:Label ID="lblCustodyPendingSub" runat="server" meta:resourcekey="lblCustodyPendingSubResource1" /></div>
                </div>
                <div class="custody-metric">
                    <div class="custody-metric-top">
                        <div class="custody-metric-number"><asp:Label ID="lblCustodyReturned" runat="server" Text="0" /></div>
                        <div class="custody-metric-caption"><asp:Label ID="lblCustodyReturnedCaption" runat="server" meta:resourcekey="lblCustodyReturnedCaptionResource1" /></div>
                    </div>
                    <div class="custody-metric-sub"><asp:Label ID="lblCustodyReturnedSub" runat="server" meta:resourcekey="lblCustodyReturnedSubResource1" /></div>
                </div>
                <div class="custody-metric">
                    <div class="custody-metric-top">
                        <div class="custody-metric-number" style="font-size:18px;padding-top:6px;">
                            <asp:Label ID="lblCustodyClearanceStatus" runat="server" />
                        </div>
                        <div class="custody-metric-caption"><asp:Label ID="lblCustodyClearanceCaption" runat="server" meta:resourcekey="lblCustodyClearanceCaptionResource1" /></div>
                    </div>
                    <div class="custody-metric-sub"><asp:Label ID="lblCustodyClearanceSub" runat="server" meta:resourcekey="lblCustodyClearanceSubResource1" /></div>
                </div>
            </div>
            <div class="custody-toolbar">
                <asp:TextBox ID="txtCustodySearch" runat="server" CssClass="custody-search"
                    meta:resourcekey="txtCustodySearchResource1" />
                <asp:DropDownList ID="ddlCustodyFilter" runat="server" CssClass="custody-filter"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlCustodyFilter_SelectedIndexChanged" />
                <asp:Button ID="btnCustodySearch" runat="server" CssClass="btn-mark-all"
                    OnClick="btnCustodySearch_Click" meta:resourcekey="btnCustodySearchResource1" />
                <asp:Button ID="btnMarkAllReturned" runat="server" CssClass="btn-mark-all"
                    OnClick="btnMarkAllReturned_Click" meta:resourcekey="btnMarkAllReturnedResource1" />
            </div>
            <div class="custody-body">
                <asp:GridView ID="grdCustody" runat="server"
                    AutoGenerateColumns="False" Width="100%" CssClass="custody-grid"
                    DataKeyNames="ID"
                    OnRowDataBound="grdCustody_RowDataBound"
                    OnRowCommand="grdCustody_RowCommand"
                    meta:resourcekey="grdCustodyResource1">
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="#" meta:resourcekey="colCustodyRowResource1" />
                        <asp:BoundField DataField="AssetCode" meta:resourcekey="colAssetCodeResource1" />
                        <asp:BoundField DataField="AssetName" meta:resourcekey="colAssetNameResource1" />
                        <asp:BoundField DataField="SerialNo" meta:resourcekey="colSerialNoResource1" />
                        <asp:BoundField DataField="ReceivedDateText" meta:resourcekey="colReceivedDateResource1" />
                        <asp:TemplateField meta:resourcekey="colCustodyStatusResource1">
                            <ItemTemplate>
                                <asp:Label ID="lblItemStatus" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField meta:resourcekey="colReturnedDateResource1">
                            <ItemTemplate>
                                <asp:Label ID="lblReturnedDate" runat="server" />
                                <asp:TextBox ID="txtReturnedDate" runat="server" CssClass="custody-date" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField meta:resourcekey="colReturnConditionResource1">
                            <ItemTemplate>
                                <asp:Label ID="lblReturnCondition" runat="server" />
                                <asp:DropDownList ID="ddlReturnCondition" runat="server" CssClass="custody-condition" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField meta:resourcekey="colCustodyActionResource1">
                            <ItemTemplate>
                                <asp:Button ID="btnConfirmReturn" runat="server" CssClass="btn-confirm-return"
                                    CommandName="ConfirmReturn" CommandArgument='<%# Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="custody-warning">
                <asp:Label ID="lblCustodyWarning" runat="server" meta:resourcekey="lblCustodyWarningResource1" />
            </div>
        </div>

        <div class="grid-card">
            <div class="grid-title">
                <asp:Label ID="lblActionNeededTitle" runat="server" meta:resourcekey="lblActionNeededTitleResource1" />
                <asp:Label ID="lblActionNeededCount" runat="server" CssClass="count-badge" />
            </div>
            <div class="grid-body">
                <asp:GridView ID="grdActionNeeded" runat="server"
                    AutoGenerateColumns="False" Width="100%" CssClass="grid-view"
                    meta:resourcekey="grdActionNeededResource1">
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="#" meta:resourcekey="colRowNumberResource1" />
                        <asp:BoundField DataField="RequestSerial" meta:resourcekey="colRequestSerialResource1" />
                        <asp:BoundField DataField="RequestName" meta:resourcekey="colRequestNameResource1" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="grid-card" id="pnlOpenRequests" runat="server">
            <div class="grid-title">
                <asp:Label ID="lblSubmittedOpenTitle" runat="server" meta:resourcekey="lblSubmittedOpenTitleResource1" />
                <asp:Label ID="lblSubmittedOpenCount" runat="server" CssClass="count-badge" />
            </div>
            <div class="open-req-toolbar">
                <input type="checkbox" id="chkOpenSelectAllHeader" onclick="toggleOpenSelectAll(this);" />
                <asp:Button ID="btnOpenSelectAll" runat="server" CssClass="btn-select-all"
                    OnClientClick="var m=document.getElementById('chkOpenSelectAllHeader'); if(m){m.checked=true; toggleOpenSelectAll(m);} return false;"
                    meta:resourcekey="btnOpenSelectAllResource1" />
                <asp:Label ID="lblOpenSelectedCount" runat="server" CssClass="selected-count" Text="0" />
                <asp:Button ID="btnCancelSelectedOpen" runat="server" CssClass="btn-cancel-sel"
                    OnClick="btnCancelSelectedOpen_Click" meta:resourcekey="btnCancelSelectedOpenResource1" />
                <asp:Button ID="btnRejectCancelSelectedOpen" runat="server" CssClass="btn-reject-sel"
                    OnClick="btnRejectCancelSelectedOpen_Click" meta:resourcekey="btnRejectCancelSelectedOpenResource1" />
                <asp:Button ID="btnRefreshOpenRequests" runat="server" CssClass="btn-refresh-open"
                    OnClick="btnRefreshOpenRequests_Click" meta:resourcekey="btnRefreshOpenRequestsResource1" />
            </div>
            <div class="grid-body">
                <asp:GridView ID="grdSubmittedOpen" runat="server"
                    AutoGenerateColumns="False" Width="100%" CssClass="open-req-grid"
                    DataKeyNames="RequestSerial,FormCode"
                    OnRowCommand="grdSubmittedOpen_RowCommand"
                    OnRowDataBound="grdSubmittedOpen_RowDataBound"
                    meta:resourcekey="grdSubmittedOpenResource1">
                    <Columns>
                        <asp:TemplateField meta:resourcekey="colOpenSelectResource1">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RowNumber" HeaderText="#" meta:resourcekey="colRowNumberResource1" />
                        <asp:BoundField DataField="RequestNumber" meta:resourcekey="colRequestSerialResource1" />
                        <asp:BoundField DataField="RequestName" meta:resourcekey="colRequestNameResource1" />
                        <asp:TemplateField meta:resourcekey="colOpenStatusResource1">
                            <ItemTemplate>
                                <asp:Label ID="lblOpenStatus" runat="server" CssClass="status-badge-pending" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CurrentStage" meta:resourcekey="colCurrentStageResource1" />
                        <asp:TemplateField meta:resourcekey="colOpenActionResource1">
                            <ItemTemplate>
                                <asp:Button ID="btnViewOpen" runat="server" CssClass="btn-view-req"
                                    CommandName="ViewOpen" CommandArgument='<%# Eval("RequestSerial") & "|" & Eval("FormCode") %>'
                                    meta:resourcekey="btnViewOpenResource1" />
                                <asp:Button ID="btnCancelOpen" runat="server" CssClass="btn-cancel-req"
                                    CommandName="CancelOpen" CommandArgument='<%# Eval("RequestSerial") & "|" & Eval("FormCode") %>'
                                    meta:resourcekey="btnCancelOpenRowResource1" />
                                <asp:Button ID="btnRejectOpen" runat="server" CssClass="btn-reject-req"
                                    CommandName="RejectOpen" CommandArgument='<%# Eval("RequestSerial") & "|" & Eval("FormCode") %>'
                                    meta:resourcekey="btnRejectOpenRowResource1" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="open-req-info">
                <asp:Label ID="lblOpenRequestsInfo" runat="server" meta:resourcekey="lblOpenRequestsInfoResource1" />
            </div>
        </div>

        <div class="grid-card">
            <div class="grid-title">
                <asp:Label ID="lblConfigurationTitle" runat="server" meta:resourcekey="lblConfigurationTitleResource1" />
                <asp:Label ID="lblConfigurationCount" runat="server" CssClass="count-badge" />
            </div>
            <div class="grid-body">
                <asp:GridView ID="grdConfiguration" runat="server"
                    AutoGenerateColumns="False" Width="100%" CssClass="grid-view"
                    meta:resourcekey="grdConfigurationResource1">
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="#" meta:resourcekey="colRowNumberResource1" />
                        <asp:BoundField DataField="FormCode" meta:resourcekey="colFormCodeResource1" />
                        <asp:BoundField DataField="RequestName" meta:resourcekey="colRequestNameResource1" />
                        <asp:BoundField DataField="Rank" meta:resourcekey="colRankResource1" />
                        <asp:BoundField DataField="MatchType" meta:resourcekey="colMatchTypeResource1" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="footer">
            <asp:Button ID="btnRefresh" runat="server" CssClass="btn-refresh"
                OnClick="btnRefresh_Click" meta:resourcekey="btnRefreshResource1" />
            <asp:Button ID="btnClose" runat="server" CssClass="btn-close"
                OnClientClick="window.close(); return false;" meta:resourcekey="btnCloseResource1" />
        </div>
    </form>
</body>
</html>
