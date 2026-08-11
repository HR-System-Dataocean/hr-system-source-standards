<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRetroactiveSalaryReview.aspx.vb"
    Inherits="frmRetroactiveSalaryReview" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Retroactive Salary Review</title>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
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
        .popup-wrap { padding: 12px; }
        .popup-header {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 12px;
            background: #1a4a7a;
            color: #fff;
            padding: 12px 16px;
            border-radius: 8px 8px 0 0;
        }
        .popup-header h1 {
            margin: 0;
            font-size: 16px;
            font-weight: bold;
        }
        .popup-body {
            background: #fff;
            border: 1px solid #c5d6e8;
            border-top: none;
            border-radius: 0 0 8px 8px;
            padding: 14px;
        }
        .info-banner {
            background: #fff8e6;
            border: 1px solid #e6c76b;
            border-radius: 6px;
            padding: 10px 12px;
            color: #7a5c10;
            margin-bottom: 12px;
            line-height: 1.5;
        }
        .summary-tiles {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            margin-bottom: 12px;
        }
        .summary-tile {
            flex: 1 1 140px;
            background: #eef5fb;
            border: 1px solid #b7d4ef;
            border-radius: 6px;
            padding: 10px 12px;
        }
        .summary-tile .lbl { display: block; font-size: 11px; color: #6b7c8f; margin-bottom: 4px; }
        .summary-tile .val { font-size: 14px; font-weight: bold; color: #1a4a7a; }
        .filter-bar {
            display: flex;
            flex-wrap: wrap;
            gap: 8px;
            margin-bottom: 12px;
            align-items: center;
        }
        .filter-bar input[type="text"],
        .filter-bar select {
            height: 30px;
            border: 1px solid #c5ced9;
            border-radius: 5px;
            padding: 4px 8px;
            font-family: Tahoma, Arial, sans-serif;
            font-size: 12px;
            min-width: 140px;
        }
        .filter-bar .search-box { flex: 1 1 180px; min-width: 160px; }
        .btn {
            height: 36px;
            padding: 0 18px;
            border-radius: 4px;
            border: 1px solid transparent;
            font-family: Tahoma, Arial, sans-serif;
            font-size: 13px;
            font-weight: bold;
            cursor: pointer;
        }
        .btn-filter {
            background: #1a4a7a !important;
            background-color: #1a4a7a !important;
            color: #fff !important;
            border-color: #1a4a7a !important;
        }
        input.btn-save,
        .btn-save,
        input[type="submit"].btn-save {
            background: #218838 !important;
            background-color: #218838 !important;
            background-image: none !important;
            color: #ffffff !important;
            border: 1px solid #1a6b2c !important;
            min-width: 160px;
        }
        input.btn-save:hover,
        .btn-save:hover,
        input.btn-save:focus,
        .btn-save:focus,
        input.btn-save:active,
        .btn-save:active {
            background: #1a6b2c !important;
            background-color: #1a6b2c !important;
            color: #ffffff !important;
        }
        input.btn-cancel,
        .btn-cancel,
        input[type="submit"].btn-cancel {
            background: #ffffff !important;
            background-color: #ffffff !important;
            background-image: none !important;
            color: #2c3e50 !important;
            border: 1px solid #ced4da !important;
            min-width: 150px;
        }
        input.btn-cancel:hover,
        .btn-cancel:hover,
        input.btn-cancel:focus,
        .btn-cancel:focus,
        input.btn-cancel:active,
        .btn-cancel:active {
            background: #f8fafc !important;
            background-color: #f8fafc !important;
            color: #2c3e50 !important;
        }
        .grid-wrap {
            border: 1px solid #d5dde8;
            border-radius: 6px;
            overflow: auto;
            max-height: 340px;
            margin-bottom: 12px;
        }
        .grid-wrap table { width: 100%; border-collapse: collapse; }
        .grid-wrap th {
            background: #dceaf7;
            color: #1a4a7a;
            padding: 8px 6px;
            border-bottom: 1px solid #b7d4ef;
            text-align: center;
            white-space: nowrap;
        }
        .grid-wrap td {
            padding: 7px 6px;
            border-bottom: 1px solid #e8eef5;
            vertical-align: middle;
        }
        .grid-wrap tr:nth-child(even) td { background: #f7fbfe; }
        .emp-name { font-weight: bold; color: #2c3e50; }
        .emp-dept { font-size: 11px; color: #6b7c8f; }
        .grid-wrap select, .grid-wrap input[type="text"] {
            width: 100%;
            height: 28px;
            border: 1px solid #c5ced9;
            border-radius: 4px;
            padding: 2px 6px;
            font-size: 11px;
        }
        .grid-wrap input.exclusion-reason[readonly] {
            background: #f0f3f7;
            cursor: not-allowed;
        }
        .grid-wrap input.exclusion-reason:not([readonly]) {
            background: #ffffff !important;
            cursor: text;
        }
        .footer-actions {
            display: flex;
            justify-content: space-between;
            align-items: center;
            gap: 10px;
            flex-wrap: wrap;
            background: #e9f2fb;
            border: 1px solid #b7d4ef;
            border-radius: 6px;
            padding: 10px 14px;
            margin-top: 4px;
        }
        .msg { padding: 8px 0; font-weight: bold; }
        .msg-ok { color: #1e8a3e; }
        .msg-err { color: #c0392b; }
        .empty-state { padding: 24px; text-align: center; color: #6b7c8f; }
    </style>
    <script type="text/javascript">
        function syncExclusionReason(sel) {
            try {
                if (!sel) return;
                var row = sel.parentNode;
                while (row && String(row.tagName).toUpperCase() !== 'TR') {
                    row = row.parentNode;
                }
                if (!row) return;

                var txt = null;
                var inputs = row.getElementsByTagName('input');
                for (var i = 0; i < inputs.length; i++) {
                    var t = inputs[i];
                    var cls = (t.className || '');
                    if (cls.indexOf('exclusion-reason') >= 0 || t.type === 'text') {
                        if (cls.indexOf('exclusion-reason') >= 0 || t.id.indexOf('txtExclusionReason') >= 0) {
                            txt = t;
                            break;
                        }
                        if (!txt && t.type === 'text') txt = t;
                    }
                }
                if (!txt) return;

                if (String(sel.value) === '2') {
                    txt.readOnly = false;
                    if (txt.removeAttribute) txt.removeAttribute('readonly');
                    txt.disabled = false;
                    if (txt.removeAttribute) txt.removeAttribute('disabled');
                    txt.style.backgroundColor = '#ffffff';
                    txt.style.cursor = 'text';
                    txt.style.pointerEvents = 'auto';
                    setTimeout(function () { try { txt.focus(); } catch (e2) { } }, 50);
                } else {
                    txt.value = '';
                    txt.readOnly = true;
                    txt.setAttribute('readonly', 'readonly');
                    txt.style.backgroundColor = '#f0f3f7';
                    txt.style.cursor = 'not-allowed';
                }
            } catch (e) { }
        }

        function prepareSaveReasons() {
            try {
                var selects = document.getElementsByTagName('select');
                for (var s = 0; s < selects.length; s++) {
                    var sel = selects[s];
                    if (!sel.id || sel.id.indexOf('ddlAction') < 0) continue;
                    var row = sel.parentNode;
                    while (row && String(row.tagName).toUpperCase() !== 'TR') {
                        row = row.parentNode;
                    }
                    if (!row) continue;

                    var txt = null;
                    var hdn = null;
                    var inputs = row.getElementsByTagName('input');
                    for (var i = 0; i < inputs.length; i++) {
                        var el = inputs[i];
                        var cls = (el.className || '');
                        var id = (el.id || '');
                        if (cls.indexOf('exclusion-reason') >= 0 || id.indexOf('txtExclusionReason') >= 0) {
                            txt = el;
                        }
                        if (id.indexOf('hdnReasonPosted') >= 0) {
                            hdn = el;
                        }
                    }
                    if (!txt) continue;

                    txt.readOnly = false;
                    if (txt.removeAttribute) txt.removeAttribute('readonly');
                    txt.disabled = false;
                    if (txt.removeAttribute) txt.removeAttribute('disabled');

                    if (String(sel.value) === '2') {
                        if (hdn) hdn.value = txt.value || '';
                    } else {
                        txt.value = '';
                        if (hdn) hdn.value = '';
                    }
                }
            } catch (e) { }
            return true;
        }

        function closePopup(reloadParent) {
            try {
                if (window.parent && typeof window.parent.CloseIt === 'function') {
                    window.parent.CloseIt(reloadParent ? '1' : '');
                    return;
                }
            } catch (e) { }
            window.close();
        }

        $(document).ready(function () {
            $('select[id*="ddlAction"]').live('change', function () {
                syncExclusionReason(this);
            });
            $('select[id*="ddlAction"]').each(function () {
                syncExclusionReason(this);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnCurrentPeriodID" runat="server" />
        <asp:HiddenField ID="hdnAccrualPeriodID" runat="server" />
        <asp:HiddenField ID="hdnSelectedEmpIDs" runat="server" />
        <div class="popup-wrap" runat="server" id="DIV">
            <div class="popup-header">
                <h1>
                    <asp:Label ID="lblTitle" runat="server" Text="Retroactive Processing of Unprepared Salaries"></asp:Label>
                </h1>
            </div>
            <div class="popup-body">
                <div class="info-banner">
                    <asp:Label ID="lblInfoBanner" runat="server" Text=""></asp:Label>
                </div>

                <div class="summary-tiles">
                    <div class="summary-tile">
                        <span class="lbl"><asp:Label ID="lblAccrualCaption" runat="server" Text="Accrual Period"></asp:Label></span>
                        <span class="val"><asp:Label ID="lblAccrualPeriod" runat="server" Text="—"></asp:Label></span>
                    </div>
                    <div class="summary-tile">
                        <span class="lbl"><asp:Label ID="lblPaymentCaption" runat="server" Text="Current Payment Period"></asp:Label></span>
                        <span class="val"><asp:Label ID="lblPaymentPeriod" runat="server" Text="—"></asp:Label></span>
                    </div>
                    <div class="summary-tile">
                        <span class="lbl"><asp:Label ID="lblUnpreparedCaption" runat="server" Text="Unprepared Employees"></asp:Label></span>
                        <span class="val"><asp:Label ID="lblUnpreparedCount" runat="server" Text="0"></asp:Label></span>
                    </div>
                    <div class="summary-tile" id="tileSelected" runat="server" visible="false">
                        <span class="lbl"><asp:Label ID="lblSelectedCaption" runat="server" Text="Selected for Preparation"></asp:Label></span>
                        <span class="val"><asp:Label ID="lblSelectedCount" runat="server" Text="0"></asp:Label></span>
                    </div>
                </div>

                <div class="filter-bar">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="search-box"></asp:TextBox>
                    <asp:DropDownList ID="ddlDepartment" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="ddlSponsor" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="ddlContractType" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-filter" Text="Filter" OnClick="btnFilter_Click" />
                </div>

                <asp:Label ID="lblMessage" runat="server" CssClass="msg" Visible="false"></asp:Label>

                <div class="grid-wrap">
                    <asp:Repeater ID="rptEmployees" runat="server" OnItemDataBound="rptEmployees_ItemDataBound">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <th style="width:40px;"><asp:Literal ID="litNo" runat="server" Text="No."></asp:Literal></th>
                                    <th style="width:90px;"><asp:Literal ID="litCode" runat="server" Text="Employee Code"></asp:Literal></th>
                                    <th><asp:Literal ID="litDept" runat="server" Text="Department"></asp:Literal></th>
                                    <th style="width:140px;"><asp:Literal ID="litSponsor" runat="server" Text="Sponsor"></asp:Literal></th>
                                    <th style="width:110px;"><asp:Literal ID="litContract" runat="server" Text="Contract Type"></asp:Literal></th>
                                    <th style="width:160px;"><asp:Literal ID="litItem" runat="server" Text="Retroactive Payroll Item"></asp:Literal></th>
                                    <th style="width:150px;"><asp:Literal ID="litAction" runat="server" Text="Action"></asp:Literal></th>
                                    <th style="width:160px;"><asp:Literal ID="litReason" runat="server" Text="Exclusion Reason"></asp:Literal></th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center;">
                                    <%# Container.ItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnEmpID" runat="server" Value='<%# Eval("EmployeeID") %>' />
                                    <asp:HiddenField ID="hdnEmpCode" runat="server" Value='<%# Eval("EmployeeCode") %>' />
                                    <asp:HiddenField ID="hdnAmount" runat="server" Value='<%# Eval("Amount") %>' />
                                    <asp:HiddenField ID="hdnTxnCode" runat="server" Value='<%# Eval("TransactionCode") %>' />
                                </td>
                                <td style="text-align:center;"><%# Eval("EmployeeCode") %></td>
                                <td>
                                    <div class="emp-name"><%# Eval("EmployeeName") %></div>
                                    <div class="emp-dept"><%# Eval("DepartmentName") %></div>
                                </td>
                                <td><%# Eval("SponsorName") %></td>
                                <td><%# Eval("ContractTypeName") %></td>
                                <td><%# Eval("PayrollItemName") %></td>
                                <td>
                                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="action-ddl">
                                        <asp:ListItem Value="1" Text="Prepare Retroactively"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Permanently Exclude"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExclusionReason" runat="server" CssClass="exclusion-reason"
                                        Text=""></asp:TextBox>
                                    <asp:HiddenField ID="hdnReasonPosted" runat="server" Value="" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pnlEmpty" runat="server" CssClass="empty-state" Visible="false">
                        <asp:Label ID="lblEmpty" runat="server" Text="No unprepared employees found for the previous period."></asp:Label>
                    </asp:Panel>
                </div>

                <div class="footer-actions">
                    <asp:Button ID="btnSaveProcess" runat="server" CssClass="btn btn-save" Text="Save and Process"
                        EnableTheming="false" CausesValidation="false" UseSubmitBehavior="true"
                        BackColor="#218838" ForeColor="White"
                        OnClientClick="return prepareSaveReasons();" OnClick="btnSaveProcess_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-cancel" Text="Cancel and Return"
                        EnableTheming="false" BackColor="White" ForeColor="#2c3e50"
                        OnClientClick="closePopup(false); return false;" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
