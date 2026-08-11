<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRetroTransactionMapping.aspx.vb"
    Inherits="frmRetroTransactionMapping" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Retro Transaction Mapping</title>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <style type="text/css">
        * { box-sizing: border-box; }
        body {
            font-family: Tahoma, Arial, sans-serif;
            font-size: 13px;
            margin: 0;
            padding: 0;
            background: #f5f7fb;
            color: #1f2937;
        }
        .page-wrap {
            max-width: 1180px;
            margin: 0 auto;
            padding: 18px 20px 28px;
        }
        .page-title {
            margin: 0 0 6px;
            font-size: 26px;
            font-weight: bold;
            color: #111827;
        }
        .page-desc {
            margin: 0 0 14px;
            color: #6b7280;
            line-height: 1.6;
            max-width: 920px;
        }
        .info-banner {
            display: flex;
            align-items: flex-start;
            gap: 10px;
            background: #eff6ff;
            border: 1px solid #bfdbfe;
            border-radius: 8px;
            padding: 12px 14px;
            color: #1e40af;
            margin-bottom: 14px;
            line-height: 1.55;
        }
        .info-banner .info-icon {
            width: 20px;
            height: 20px;
            border-radius: 50%;
            background: #3b82f6;
            color: #fff;
            font-size: 12px;
            font-weight: bold;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            flex-shrink: 0;
            margin-top: 1px;
        }
        .filter-bar {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            align-items: flex-end;
            background: #fff;
            border: 1px solid #e5e7eb;
            border-radius: 8px;
            padding: 12px;
            margin-bottom: 12px;
        }
        .filter-field {
            display: flex;
            flex-direction: column;
            gap: 4px;
            min-width: 160px;
        }
        .filter-field.search { flex: 1 1 240px; }
        .filter-field label {
            font-size: 12px;
            color: #6b7280;
        }
        .filter-bar input[type="text"],
        .filter-bar select {
            height: 34px;
            border: 1px solid #d1d5db;
            border-radius: 6px;
            padding: 4px 10px;
            font-family: Tahoma, Arial, sans-serif;
            font-size: 13px;
            background: #fff;
            width: 100%;
        }
        .btn {
            height: 36px;
            padding: 0 16px;
            border-radius: 6px;
            border: 1px solid transparent;
            font-family: Tahoma, Arial, sans-serif;
            font-size: 13px;
            font-weight: bold;
            cursor: pointer;
        }
        .btn-clear {
            background: #fff !important;
            color: #374151 !important;
            border-color: #d1d5db !important;
            white-space: nowrap;
        }
        .btn-clear:hover { background: #f9fafb !important; }
        .summary-row {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            align-items: center;
            gap: 10px;
            margin-bottom: 10px;
            padding: 0 2px;
        }
        .summary-stats {
            color: #4b5563;
            font-size: 13px;
        }
        .summary-stats .sep { margin: 0 8px; color: #9ca3af; }
        .legend {
            display: flex;
            align-items: center;
            gap: 14px;
            color: #6b7280;
            font-size: 12px;
        }
        .dot {
            display: inline-block;
            width: 8px;
            height: 8px;
            border-radius: 50%;
            margin-inline-end: 6px;
            vertical-align: middle;
        }
        .dot-green { background: #22c55e; }
        .dot-orange { background: #f59e0b; }
        .grid-wrap {
            background: #fff;
            border: 1px solid #e5e7eb;
            border-radius: 10px;
            overflow: auto;
            max-height: calc(100vh - 360px);
            min-height: 280px;
        }
        .grid-wrap table {
            width: 100%;
            border-collapse: collapse;
        }
        .grid-wrap th {
            background: #f8fafc;
            color: #374151;
            padding: 12px 10px;
            border-bottom: 1px solid #e5e7eb;
            text-align: start;
            white-space: nowrap;
            font-weight: bold;
            font-size: 12px;
            position: sticky;
            top: 0;
            z-index: 1;
        }
        .grid-wrap td {
            padding: 12px 10px;
            border-bottom: 1px solid #f1f5f9;
            vertical-align: middle;
        }
        .grid-wrap tr:hover td { background: #f8fbff; }
        .grid-wrap tr.row-hidden { display: none; }
        .col-check { width: 70px; text-align: center !important; }
        .col-arrow { width: 40px; text-align: center !important; color: #9ca3af; font-size: 16px; }
        .col-status { width: 120px; }
        .col-retro { min-width: 260px; width: 32%; }
        .txn-name {
            font-weight: bold;
            color: #111827;
        }
        .txn-code {
            color: #6b7280;
            font-weight: normal;
            margin-inline-end: 4px;
        }
        .group-badge {
            color: #059669;
            font-size: 12px;
            font-weight: bold;
            white-space: nowrap;
        }
        .status-label {
            display: inline-flex;
            align-items: center;
            font-size: 12px;
            color: #374151;
            white-space: nowrap;
        }
        .grid-wrap select {
            width: 100%;
            height: 34px;
            border: 1px solid #d1d5db;
            border-radius: 6px;
            padding: 2px 8px;
            font-size: 12px;
            font-family: Tahoma, Arial, sans-serif;
            background: #fff;
        }
        .grid-wrap input[type="checkbox"] {
            width: 16px;
            height: 16px;
            cursor: pointer;
        }
        .footer-bar {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            align-items: center;
            gap: 12px;
            margin-top: 14px;
            padding-top: 4px;
        }
        .footer-note {
            color: #6b7280;
            font-size: 12px;
            max-width: 620px;
            line-height: 1.5;
        }
        .footer-actions {
            display: flex;
            gap: 8px;
            flex-wrap: wrap;
        }
        input.btn-save,
        .btn-save,
        input[type="submit"].btn-save {
            background: #2563eb !important;
            background-color: #2563eb !important;
            background-image: none !important;
            color: #ffffff !important;
            border: 1px solid #1d4ed8 !important;
            min-width: 140px;
        }
        input.btn-save:hover,
        .btn-save:hover {
            background: #1d4ed8 !important;
            background-color: #1d4ed8 !important;
        }
        input.btn-reset,
        .btn-reset,
        input[type="submit"].btn-reset {
            background: #ffffff !important;
            background-color: #ffffff !important;
            background-image: none !important;
            color: #374151 !important;
            border: 1px solid #d1d5db !important;
            min-width: 110px;
        }
        input.btn-reset:hover,
        .btn-reset:hover {
            background: #f9fafb !important;
        }
        .msg {
            padding: 10px 12px;
            border-radius: 6px;
            margin-bottom: 12px;
            font-weight: bold;
        }
        .msg-ok {
            background: #ecfdf5;
            color: #047857;
            border: 1px solid #a7f3d0;
        }
        .msg-err {
            background: #fef2f2;
            color: #b91c1c;
            border: 1px solid #fecaca;
        }
        .empty-state {
            padding: 36px 16px;
            text-align: center;
            color: #6b7280;
        }
        [dir="rtl"] .page-title,
        [dir="rtl"] .page-desc,
        [dir="rtl"] .grid-wrap th,
        [dir="rtl"] .grid-wrap td {
            text-align: right;
        }
        [dir="ltr"] .page-title,
        [dir="ltr"] .page-desc,
        [dir="ltr"] .grid-wrap th,
        [dir="ltr"] .grid-wrap td {
            text-align: left;
        }
        [dir="rtl"] .col-check,
        [dir="ltr"] .col-check,
        [dir="rtl"] .col-arrow,
        [dir="ltr"] .col-arrow {
            text-align: center !important;
        }
    </style>
    <script type="text/javascript">
        function byIdEndsWith(suffix) {
            var all = document.getElementsByTagName('*');
            for (var i = 0; i < all.length; i++) {
                var id = all[i].id || '';
                if (id === suffix || id.indexOf('_' + suffix) >= 0 || id.lastIndexOf(suffix) === id.length - suffix.length) {
                    if (id === suffix || id.substring(id.length - suffix.length) === suffix) return all[i];
                }
            }
            return null;
        }

        function applyClientFilters() {
            try {
                var searchBox = byIdEndsWith('txtSearch');
                var groupDdl = byIdEndsWith('ddlGroup');
                var statusDdl = byIdEndsWith('ddlStatus');
                var q = (searchBox && searchBox.value ? searchBox.value : '').toLowerCase().replace(/^\s+|\s+$/g, '');
                var groupVal = groupDdl ? String(groupDdl.value) : '0';
                var statusVal = statusDdl ? String(statusDdl.value) : '0';

                var rows = document.querySelectorAll('#mappingTable tbody tr.map-row');
                var visible = 0;
                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    var name = (row.getAttribute('data-name') || '').toLowerCase();
                    var code = (row.getAttribute('data-code') || '').toLowerCase();
                    var groupId = String(row.getAttribute('data-group') || '0');
                    var mapped = String(row.getAttribute('data-mapped') || '0');
                    var matchSearch = !q || name.indexOf(q) >= 0 || code.indexOf(q) >= 0;
                    var matchGroup = groupVal === '0' || groupId === groupVal;
                    var matchStatus = statusVal === '0'
                        || (statusVal === '1' && mapped === '1')
                        || (statusVal === '2' && mapped === '0');
                    var show = matchSearch && matchGroup && matchStatus;
                    if (show) {
                        row.className = row.className.replace(/\s*row-hidden\b/g, '');
                        visible++;
                    } else {
                        if (row.className.indexOf('row-hidden') < 0) row.className += ' row-hidden';
                    }
                }
                var empty = byIdEndsWith('pnlFilterEmpty');
                if (empty) empty.style.display = (visible === 0 ? 'block' : 'none');
            } catch (e) { }
            return false;
        }

        function clearClientFilters() {
            try {
                var searchBox = byIdEndsWith('txtSearch');
                var groupDdl = byIdEndsWith('ddlGroup');
                var statusDdl = byIdEndsWith('ddlStatus');
                if (searchBox) searchBox.value = '';
                if (groupDdl) groupDdl.selectedIndex = 0;
                if (statusDdl) statusDdl.selectedIndex = 0;
                applyClientFilters();
            } catch (e) { }
            return false;
        }

        function refreshRowStatus(row) {
            if (!row) return;
            var ddl = row.querySelector('select[id*="ddlRetro"]');
            var statusSpan = row.querySelector('.status-label');
            var mapped = (ddl && String(ddl.value) !== '0') ? '1' : '0';
            row.setAttribute('data-mapped', mapped);
            if (statusSpan) {
                var configuredText = statusSpan.getAttribute('data-configured') || 'Configured';
                var needsText = statusSpan.getAttribute('data-needs') || 'Needs Mapping';
                if (mapped === '1') {
                    statusSpan.innerHTML = '<span class="dot dot-green"></span>' + configuredText;
                } else {
                    statusSpan.innerHTML = '<span class="dot dot-orange"></span>' + needsText;
                }
            }
            updateSummaryCounts();
        }

        function updateSummaryCounts() {
            try {
                var rows = document.querySelectorAll('#mappingTable tbody tr.map-row');
                var active = 0, mapped = 0;
                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    var chk = row.querySelector('input[type="checkbox"][id*="chkActive"]');
                    var ddl = row.querySelector('select[id*="ddlRetro"]');
                    if (chk && chk.checked) active++;
                    if (ddl && String(ddl.value) !== '0') mapped++;
                }
                var lblActive = byIdEndsWith('lblActiveCount');
                var lblMapped = byIdEndsWith('lblMappedCount');
                if (lblActive) lblActive.innerHTML = String(active);
                if (lblMapped) lblMapped.innerHTML = String(mapped);
            } catch (e) { }
        }

        $(document).ready(function () {
            $('input[id$="txtSearch"]').bind('keyup', applyClientFilters);
            $('select[id$="ddlGroup"], select[id$="ddlStatus"]').bind('change', applyClientFilters);
            $('select[id*="ddlRetro"]').live('change', function () {
                var row = this.parentNode;
                while (row && String(row.tagName).toUpperCase() !== 'TR') row = row.parentNode;
                refreshRowStatus(row);
                applyClientFilters();
            });
            $('input[type="checkbox"][id*="chkActive"]').live('change', updateSummaryCounts);
            updateSummaryCounts();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-wrap" runat="server" id="DIV">
            <h1 class="page-title">
                <asp:Label ID="lblTitle" runat="server" Text="Retro Transaction Mapping"></asp:Label>
            </h1>
            <p class="page-desc">
                <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
            </p>

            <div class="info-banner">
                <span class="info-icon">i</span>
                <asp:Label ID="lblInfoBanner" runat="server" Text=""></asp:Label>
            </div>

            <asp:Label ID="lblMessage" runat="server" CssClass="msg" Visible="false"></asp:Label>

            <div class="filter-bar">
                <div class="filter-field search">
                    <label><asp:Label ID="lblSearchCaption" runat="server" Text="Search Transaction"></asp:Label></label>
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                </div>
                <div class="filter-field">
                    <label><asp:Label ID="lblGroupCaption" runat="server" Text="Transaction Group"></asp:Label></label>
                    <asp:DropDownList ID="ddlGroup" runat="server"></asp:DropDownList>
                </div>
                <div class="filter-field">
                    <label><asp:Label ID="lblStatusCaption" runat="server" Text="Retro Status"></asp:Label></label>
                    <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                </div>
                <asp:Button ID="btnClearFilter" runat="server" CssClass="btn btn-clear" Text="Clear Filters"
                    OnClientClick="return clearClientFilters();" UseSubmitBehavior="false" />
            </div>

            <div class="summary-row">
                <div class="summary-stats">
                    <asp:Label ID="lblActiveCount" runat="server" Text="0"></asp:Label>
                    <asp:Literal ID="litActiveSuffix" runat="server" Text=" transactions included"></asp:Literal>
                    <span class="sep">•</span>
                    <asp:Label ID="lblMappedCount" runat="server" Text="0"></asp:Label>
                    <asp:Literal ID="litMappedSuffix" runat="server" Text=" transactions mapped"></asp:Literal>
                </div>
                <div class="legend">
                    <span><span class="dot dot-green"></span><asp:Literal ID="litLegendConfigured" runat="server" Text="Configured"></asp:Literal></span>
                    <span><span class="dot dot-orange"></span><asp:Literal ID="litLegendNeeds" runat="server" Text="Needs Mapping"></asp:Literal></span>
                </div>
            </div>

            <div class="grid-wrap">
                <table id="mappingTable">
                    <thead>
                        <tr>
                            <th class="col-check"><asp:Literal ID="litColInclude" runat="server" Text="Include"></asp:Literal></th>
                            <th><asp:Literal ID="litColPayroll" runat="server" Text="Payroll Transaction"></asp:Literal></th>
                            <th><asp:Literal ID="litColGroup" runat="server" Text="Transaction Group"></asp:Literal></th>
                            <th class="col-arrow"></th>
                            <th class="col-retro"><asp:Literal ID="litColRetro" runat="server" Text="Retro Transaction"></asp:Literal></th>
                            <th class="col-status"><asp:Literal ID="litColStatus" runat="server" Text="Status"></asp:Literal></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptMappings" runat="server" OnItemDataBound="rptMappings_ItemDataBound">
                            <ItemTemplate>
                                <tr class="map-row"
                                    data-code='<%# Eval("Code") %>'
                                    data-name='<%# Eval("TxnName") %>'
                                    data-group='<%# Eval("TransactionGroupID") %>'
                                    data-mapped='<%# If(Convert.ToInt32(Eval("RetroTransactionTypeID")) > 0, "1", "0") %>'>
                                    <td class="col-check">
                                        <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsActive")) %>' />
                                        <asp:HiddenField ID="hdnTxnID" runat="server" Value='<%# Eval("TransactionTypeID") %>' />
                                    </td>
                                    <td>
                                        <span class="txn-name">
                                            <span class="txn-code"><%# Eval("Code") %></span>
                                            <%# Eval("TxnName") %>
                                        </span>
                                    </td>
                                    <td>
                                        <span class="group-badge"><%# Eval("GroupDisplay") %></span>
                                    </td>
                                    <td class="col-arrow">
                                        <asp:Literal ID="litArrow" runat="server" Text="→"></asp:Literal>
                                    </td>
                                    <td class="col-retro">
                                        <asp:DropDownList ID="ddlRetro" runat="server" CssClass="retro-ddl"></asp:DropDownList>
                                    </td>
                                    <td class="col-status">
                                        <asp:Literal ID="litStatus" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <asp:Panel ID="pnlEmpty" runat="server" CssClass="empty-state" Visible="false">
                    <asp:Label ID="lblEmpty" runat="server" Text="No payroll transactions found."></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlFilterEmpty" runat="server" CssClass="empty-state" style="display:none;">
                    <asp:Label ID="lblFilterEmpty" runat="server" Text="No transactions match the current filters."></asp:Label>
                </asp:Panel>
            </div>

            <div class="footer-bar">
                <div class="footer-note">
                    <asp:Label ID="lblFooterNote" runat="server" Text=""></asp:Label>
                </div>
                <div class="footer-actions">
                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-reset" Text="Reset"
                        EnableTheming="false" CausesValidation="false"
                        OnClick="btnReset_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-save" Text="Save Configuration"
                        EnableTheming="false" CausesValidation="false"
                        OnClick="btnSave_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
