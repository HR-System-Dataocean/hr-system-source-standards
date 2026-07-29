<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmChangeJoinDate.aspx.vb" Inherits="frmChangeJoinDate"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" 
    EnableSessionState="True" %>

<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Change Join Date</title>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

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
            padding: 8px 12px 16px;
            background: #f3f6fa;
        }
        [dir="rtl"] .Div_MasterContainer { direction: rtl; }
        [dir="ltr"] .Div_MasterContainer { direction: ltr; }

        /* Toolbar */
        .toolbar-wrap {
            background: #fff;
            border: 1px solid #d5dde8;
            border-radius: 6px;
            padding: 4px 8px;
            margin-bottom: 10px;
        }
        .toolbar-wrap table { width: 100%; border-collapse: collapse; }
        .toolbar-wrap td { vertical-align: middle; padding: 2px 4px; }

        /* Page header */
        .page-header {
            display: flex;
            align-items: flex-start;
            gap: 14px;
            background: #fff;
            border: 1px solid #d5dde8;
            border-radius: 8px;
            padding: 14px 16px;
            margin-bottom: 12px;
        }
        .page-header-icon {
            width: 42px;
            height: 42px;
            border-radius: 8px;
            background: #e8f2fb;
            border: 1px solid #b7d4ef;
            display: flex;
            align-items: center;
            justify-content: center;
            flex-shrink: 0;
        }
        .page-header-icon img { width: 24px; height: 24px; }
        .page-header-main { flex: 1; min-width: 0; }
        .page-header-title {
            font-size: 18px;
            font-weight: bold;
            color: #1a4a7a;
            margin: 0 0 4px 0;
            line-height: 1.3;
        }
        .page-header-sub {
            font-size: 12px;
            color: #6b7c8f;
            margin: 0;
            line-height: 1.4;
        }
        .page-header-meta {
            flex-shrink: 0;
            text-align: right;
            font-size: 11px;
            color: #6b7c8f;
            line-height: 1.7;
            min-width: 160px;
        }
        [dir="rtl"] .page-header-meta { text-align: left; }
        .page-header-meta .meta-label { font-weight: bold; color: #4a5a6a; }
        .page-header-meta .meta-value { color: #8899aa; }

        /* Cards / sections */
        .form-card {
            background: #fff;
            border: 1px solid #c5d6e8;
            border-radius: 8px;
            margin-bottom: 12px;
            overflow: hidden;
            box-shadow: 0 1px 2px rgba(26, 74, 122, 0.04);
        }
        .form-card-header {
            background: #dceaf7;
            color: #1a4a7a;
            font-weight: bold;
            font-size: 13px;
            padding: 9px 14px;
            border-bottom: 1px solid #b7d4ef;
        }
        .form-card-body { padding: 14px; }

        .two-col {
            display: flex;
            gap: 12px;
            margin-bottom: 12px;
            align-items: stretch;
        }
        .two-col > .form-card {
            flex: 1;
            margin-bottom: 0;
            min-width: 0;
        }

        .field-grid {
            display: flex;
            flex-wrap: wrap;
            gap: 12px 18px;
        }
        .field-item {
            display: flex;
            align-items: center;
            gap: 10px;
            flex: 1 1 280px;
            min-width: 240px;
        }
        .field-item.full { flex: 1 1 100%; }
        .field-item.stacked {
            flex-direction: column;
            align-items: stretch;
            gap: 6px;
        }
        .field-label {
            font-size: 12px;
            color: #334455;
            white-space: nowrap;
            min-width: 150px;
            font-weight: normal;
        }
        .field-item.stacked .field-label { min-width: 0; }
        .field-control { flex: 1; min-width: 0; }
        .required-star { color: #e53935; font-weight: bold; margin: 0 2px; }

        .form-input,
        .form-card-body input[type="text"],
        .form-card-body select,
        .form-card-body textarea {
            width: 100%;
            padding: 6px 10px;
            border: 1px solid #c5ced9;
            border-radius: 5px;
            font-family: Tahoma, Arial, sans-serif;
            font-size: 12px;
            background: #fff;
            color: #2c3e50;
            height: 28px;
        }
        .form-card-body textarea {
            height: auto;
            min-height: 70px;
            resize: vertical;
        }
        .form-input-readonly,
        .form-card-body input[readonly],
        .form-card-body input[disabled] {
            background: #f0f3f7 !important;
            color: #445566;
            border-color: #d0d7e0;
        }
        .form-card-body select {
            height: 30px;
            padding-right: 24px;
        }

        /* Stacked fields inside side panels */
        .stack-fields .field-item {
            flex: 1 1 100%;
            margin-bottom: 10px;
        }
        .stack-fields .field-item:last-child { margin-bottom: 0; }
        .stack-fields .field-label { min-width: 170px; }

        /* Accrued balance highlight */
        .accrued-box {
            display: flex;
            align-items: center;
            justify-content: space-between;
            background: #eef8f0;
            border: 1px solid #8bc99a;
            border-radius: 8px;
            padding: 14px 18px;
            margin-bottom: 12px;
        }
        .accrued-label {
            font-size: 12px;
            color: #5a6a7a;
            margin-bottom: 4px;
        }
        .accrued-value {
            font-size: 22px;
            font-weight: bold;
            color: #1e8a3e;
            line-height: 1.2;
        }
        .accrued-icon {
            width: 36px;
            height: 36px;
            border-radius: 8px;
            background: #d4edd9;
            color: #1e8a3e;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 18px;
            font-weight: bold;
        }

        /* Balance handling radios */
        .balance-options {
            display: flex;
            flex-direction: column;
            gap: 8px;
            margin-bottom: 14px;
        }
        .balance-option {
            border: 1px solid #c5d6e8;
            border-radius: 6px;
            padding: 10px 12px;
            background: #fff;
            cursor: pointer;
            transition: border-color 0.15s, background 0.15s;
        }
        .balance-option:hover { border-color: #7eb0d9; background: #f7fbfe; }
        .balance-option.selected {
            border-color: #2f80c1;
            background: #eef6fc;
        }
        .balance-option label {
            display: block;
            cursor: pointer;
            margin: 0;
        }
        .balance-option .opt-title {
            font-weight: bold;
            color: #1a4a7a;
            font-size: 12px;
            display: flex;
            align-items: center;
            gap: 8px;
        }
        .balance-option .opt-desc {
            font-size: 11px;
            color: #6b7c8f;
            margin-top: 3px;
            padding-left: 22px;
        }
        [dir="rtl"] .balance-option .opt-desc { padding-left: 0; padding-right: 22px; }

        /* Change summary */
        .summary-rows { border: 1px solid #e0e6ee; border-radius: 6px; overflow: hidden; }
        .summary-row {
            display: flex;
            justify-content: space-between;
            align-items: center;
            gap: 12px;
            padding: 9px 12px;
            background: #f7f9fc;
            border-bottom: 1px solid #e0e6ee;
            font-size: 12px;
        }
        .summary-row:last-child { border-bottom: none; }
        .summary-row .sum-label { color: #4a5a6a; font-weight: bold; white-space: nowrap; }
        .summary-row .sum-value { color: #2c3e50; text-align: right; word-break: break-word; }
        [dir="rtl"] .summary-row .sum-value { text-align: left; }

        .warning-box {
            margin-top: 12px;
            background: #fff8e6;
            border: 1px solid #e6c76b;
            border-radius: 6px;
            padding: 10px 12px;
            color: #7a5c10;
            font-size: 12px;
            line-height: 1.5;
        }

        /* Footer actions */
        .form-footer {
            display: flex;
            justify-content: flex-end;
            gap: 10px;
            padding: 12px 0 4px;
            flex-wrap: wrap;
        }
        .btn-footer {
            min-width: 120px;
            height: 34px;
            padding: 0 16px;
            border-radius: 5px;
            font-family: Tahoma, Arial, sans-serif;
            font-size: 12px;
            font-weight: bold;
            cursor: pointer;
            border: 1px solid transparent;
        }
        .btn-cancel {
            background: #fff;
            border-color: #a8c4de;
            color: #1a4a7a;
        }
        .btn-cancel:hover { background: #f4f8fc; }
        .btn-preview {
            background: #dceaf7;
            border-color: #8bb8d9;
            color: #1a4a7a;
        }
        .btn-preview:hover { background: #cde0f2; }
        .btn-confirm {
            background: #1a4a7a;
            border-color: #1a4a7a;
            color: #fff;
        }
        .btn-confirm:hover { background: #153d66; }

        .msg-success { color: #1e8a3e; font-weight: bold; padding: 6px 0; }
        .msg-error { color: #c0392b; font-weight: bold; padding: 6px 0; }

        /* Confirm save modal */
        .cj-modal-overlay {
            display: none;
            position: fixed;
            left: 0; top: 0; right: 0; bottom: 0;
            background: rgba(20, 40, 70, 0.45);
            z-index: 10000;
            text-align: center;
            padding: 16px;
            overflow-y: auto;
        }
        .cj-modal-overlay.is-open { display: block; }
        .cj-modal {
            background: #fff;
            border-radius: 10px;
            border: 1px solid #c5d6e8;
            box-shadow: 0 12px 40px rgba(26, 74, 122, 0.25);
            width: 100%;
            max-width: 480px;
            overflow: hidden;
            text-align: left;
            display: inline-block;
            margin-top: 8%;
            vertical-align: middle;
        }
        [dir="rtl"] .cj-modal { text-align: right; }
        .cj-modal-header {
            background: #1a4a7a;
            color: #fff;
            padding: 14px 18px;
            font-size: 15px;
            font-weight: bold;
        }
        .cj-modal-body {
            padding: 18px;
            color: #334455;
            font-size: 13px;
            line-height: 1.65;
        }
        .cj-modal-body p { margin: 0 0 10px 0; }
        .cj-modal-body p:last-child { margin-bottom: 0; font-weight: bold; color: #1a4a7a; }
        .cj-modal-footer {
            display: flex;
            justify-content: flex-end;
            gap: 10px;
            padding: 12px 18px 16px;
            background: #f7f9fc;
            border-top: 1px solid #e0e6ee;
            flex-wrap: wrap;
        }
        .cj-modal-footer .btn-footer { min-width: 130px; }

        /* Hide UltraWebTab chrome visually while keeping control for server logic */
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
        .cj-tab-content { padding: 2px 0 0; }

        /* Infragistics date chooser sizing */
        .form-card-body .igte_EditWithButtons,
        .form-card-body table[id*="txtNewJoinDate"],
        .form-card-body table[id*="txtTransferExpireDate"],
        .form-card-body table[id*="txtClassEffectiveDate"] {
            width: 100% !important;
        }

        .hidden-server { display: none !important; }

        @media (max-width: 900px) {
            .two-col { flex-direction: column; }
            .page-header { flex-wrap: wrap; }
            .page-header-meta { text-align: left; width: 100%; }
            [dir="rtl"] .page-header-meta { text-align: right; }
            .field-item { flex-direction: column; align-items: stretch; }
            .field-label { min-width: 0; }
            .stack-fields .field-item { flex-direction: column; }
        }
    </style>

    <script type="text/javascript">
        var ODialoge;
        var OSender;
        var ids = {};

        function OpenModal1(pageurl, height, width, CheckID, CheckContract, SenderCtrl) {
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

        function CloseIt(retvalue) {
            if (retvalue != "") {
                var Sender = window.document.getElementById(OSender);
                if (Sender) {
                    Sender.value = retvalue;
                    Sender.focus();
                    if (Sender.onchange) {
                        Sender.onchange();
                    } else {
                        __doPostBack(Sender.name, '');
                    }
                }
            }
            if (ODialoge) {
                ODialoge.dialog('close');
            }
        }

        function getVal(id) {
            var el = document.getElementById(id);
            return el ? (el.value || el.innerHTML || '') : '';
        }

        function getText(id) {
            var el = document.getElementById(id);
            if (!el) return '';
            if (el.tagName === 'SELECT') {
                return el.options[el.selectedIndex] ? el.options[el.selectedIndex].text : '';
            }
            return el.value || el.innerHTML || '';
        }

        function setText(id, text) {
            var el = document.getElementById(id);
            if (el) el.innerHTML = text || '—';
        }

        function CheckClassChange() {
            var ddlClass = document.getElementById(ids.ddlNewClass);
            var hdnCurrentClass = document.getElementById(ids.hdnCurrentClassID);
            var divSection = document.getElementById('divClassChangeVacation');
            if (!ddlClass || !divSection) return;

            var selectedClassID = ddlClass.value;
            var currentClassID = hdnCurrentClass ? hdnCurrentClass.value : '';
            var classChanged = (selectedClassID != '' && selectedClassID != '0' && selectedClassID != currentClassID);

            var changeType = getChangeType();
            var showBalance = classChanged && (changeType === 'class' || changeType === 'both');

            divSection.style.display = showBalance ? 'block' : 'none';
            if (!showBalance) {
                setBalanceAction('0');
            }
            UpdateChangeSummary();
        }

        function getChangeType() {
            var ddl = document.getElementById(ids.ddlChangeType);
            return ddl ? ddl.value : 'both';
        }

        function ApplyChangeTypeVisibility() {
            var type = getChangeType();
            var joinRow = document.getElementById('rowNewJoinDate');
            var classRow = document.getElementById('rowNewClass');
            var effectiveRow = document.getElementById('rowClassEffectiveDate');

            if (joinRow) joinRow.style.display = (type === 'join' || type === 'both') ? '' : 'none';
            if (classRow) classRow.style.display = (type === 'class' || type === 'both') ? '' : 'none';
            if (effectiveRow) effectiveRow.style.display = (type === 'class' || type === 'both') ? '' : 'none';

            CheckClassChange();
        }

        function setBalanceAction(val) {
            var radios = document.getElementsByName('rdoBalanceAction');
            for (var i = 0; i < radios.length; i++) {
                radios[i].checked = (radios[i].value === val);
            }
            SyncBalanceActionUI();
        }

        function getBalanceAction() {
            var radios = document.getElementsByName('rdoBalanceAction');
            for (var i = 0; i < radios.length; i++) {
                if (radios[i].checked) return radios[i].value;
            }
            return '0';
        }

        function SyncBalanceActionUI() {
            var val = getBalanceAction();
            var chk = document.getElementById(ids.chkTransferBalance);
            if (chk) chk.checked = (val === '1' || val === '2');

            var opts = document.querySelectorAll('.balance-option');
            for (var i = 0; i < opts.length; i++) {
                opts[i].className = opts[i].className.replace(/\s*selected/g, '');
                if (opts[i].getAttribute('data-value') === val) {
                    opts[i].className += ' selected';
                }
            }

            var balInput = document.getElementById(ids.txtBalanceToTransfer);
            var expireWrap = document.getElementById('divTransferExpireFields');
            if (balInput) {
                balInput.readOnly = (val !== '2');
                if (val === '1' || val === '0') {
                    var due = document.getElementById(ids.lblDueBalance);
                    if (due && val === '1') balInput.value = (due.innerHTML || '').replace(/,/g, '').trim();
                    if (val === '0') balInput.value = '0.00';
                }
            }
            if (expireWrap) {
                expireWrap.style.display = (val === '1' || val === '2') ? '' : 'none';
            }

            var hdnAction = document.getElementById(ids.hdnBalanceAction);
            if (hdnAction) hdnAction.value = val;

            UpdateChangeSummary();
        }

        function ToggleTransferExpireDate() {
            SyncBalanceActionUI();
        }

        function formatArrow(from, to) {
            from = (from || '').trim() || '—';
            to = (to || '').trim() || '—';
            if (from === to) return from;
            return from + ' → ' + to;
        }

        function UpdateChangeSummary() {
            var currentJoin = getVal(ids.txtCurrentJoinDate);
            var newJoinEl = document.getElementById(ids.txtNewJoinDate);
            var newJoin = newJoinEl ? (newJoinEl.value || '') : '';
            var currentClass = getVal(ids.txtCurrentClass);
            var newClass = getText(ids.ddlNewClass);
            if (!newClass || newClass.indexOf('--') === 0 || newClass.indexOf('اختر') >= 0) newClass = '—';

            var effectiveEl = document.getElementById(ids.txtClassEffectiveDate);
            var effective = effectiveEl ? (effectiveEl.value || '') : '';
            var expireEl = document.getElementById(ids.txtTransferExpireDate);
            var expire = expireEl ? (expireEl.value || '') : '';
            var balInput = document.getElementById(ids.txtBalanceToTransfer);
            var bal = balInput ? balInput.value : '';
            var action = getBalanceAction();

            setText('sumJoinDate', formatArrow(currentJoin, newJoin));
            setText('sumClass', formatArrow(currentClass, newClass));
            setText('sumEffectiveDate', effective || '—');

            var actionText = '—';
            if (action === '1') actionText = (document.getElementById('lblSumTransfer') || {}).innerHTML || ('Transfer ' + bal + ' Days');
            else if (action === '2') actionText = (document.getElementById('lblSumManual') || {}).innerHTML || ('Manual ' + bal + ' Days');
            else if (action === '0') actionText = (document.getElementById('lblSumNoTransfer') || {}).innerHTML || 'Do not transfer';

            // inject balance into localized templates if placeholders exist
            var tplTransfer = document.getElementById('lblSumTransfer');
            var tplManual = document.getElementById('lblSumManual');
            if (action === '1' && tplTransfer) actionText = tplTransfer.innerHTML.replace('{0}', bal || '0.00');
            if (action === '2' && tplManual) actionText = tplManual.innerHTML.replace('{0}', bal || '0.00');
            if (action === '0') {
                var tplNo = document.getElementById('lblSumNoTransfer');
                if (tplNo) actionText = tplNo.innerHTML;
            }

            setText('sumBalanceAction', actionText);
            setText('sumExpireDate', (action === '1' || action === '2') ? (expire || '—') : '—');
        }

        function PreviewImpact() {
            UpdateChangeSummary();
            var el = document.getElementById('sectionChangeSummary');
            if (el && el.scrollIntoView) el.scrollIntoView({ behavior: 'smooth', block: 'start' });
            return false;
        }

        function CancelForm() {
            if (window.opener) {
                window.close();
                return false;
            }
            var btn = document.getElementById(ids.ImageButton_New);
            if (btn) {
                btn.click();
                return false;
            }
            return false;
        }

        var confirmSaveApproved = false;
        var savePostbackTarget = 'save';

        function safeId(v) {
            return (typeof v === 'undefined' || v === null) ? '' : v;
        }

        function elById(id) {
            if (!id) return null;
            return document.getElementById(id);
        }

        function getCtrlValue(id) {
            var el = elById(id);
            if (!el) return '';
            if (typeof el.value !== 'undefined' && el.value !== null && el.tagName !== 'TABLE' && el.tagName !== 'SPAN' && el.tagName !== 'DIV') {
                return el.value;
            }
            var input = elById(id + '_input') || elById(id + 'input');
            if (!input && el.querySelector) input = el.querySelector('input[type="text"]');
            if (input && typeof input.value !== 'undefined') return input.value;
            if (el.tagName === 'SELECT') return el.value || '';
            return (typeof el.value !== 'undefined' && el.value !== null) ? el.value : '';
        }

        function getLabelText(id) {
            var el = elById(id);
            return el ? (el.innerHTML || el.innerText || '') : '';
        }

        function ConfirmAndSave() {
            savePostbackTarget = 'confirm';
            return ValidateForm();
        }

        function ShowConfirmSaveModal() {
            var overlay = document.getElementById('cjConfirmModal');
            if (overlay) {
                // Avoid being trapped under UltraWebTab stacking contexts
                if (overlay.parentNode !== document.body) {
                    document.body.appendChild(overlay);
                }
                overlay.className = 'cj-modal-overlay is-open';
                overlay.style.display = 'block';
                overlay.style.zIndex = '99999';
            } else {
                alert('Confirm dialog not found');
            }
        }

        function HideConfirmSaveModal() {
            var overlay = document.getElementById('cjConfirmModal');
            if (overlay) {
                overlay.className = 'cj-modal-overlay';
                overlay.style.display = 'none';
            }
            return false;
        }

        function BackToReview() {
            confirmSaveApproved = false;
            HideConfirmSaveModal();
            return false;
        }

        function postImageButtonSave() {
            var btn = elById(ids.ImageButton_Save);
            var uniqueId = safeId(typeof ImageButton_SaveUniqueID !== 'undefined' ? ImageButton_SaveUniqueID : '');
            var form = document.forms.length ? document.forms[0] : null;
            if (!form) return;
            var name = uniqueId || (btn ? btn.name : '');
            if (!name) return;

            function ensureHidden(n, v) {
                var existing = null;
                for (var i = 0; i < form.elements.length; i++) {
                    if (form.elements[i].name === n) { existing = form.elements[i]; break; }
                }
                if (existing) { existing.value = v; return; }
                var inp = document.createElement('input');
                inp.type = 'hidden';
                inp.name = n;
                inp.value = v;
                form.appendChild(inp);
            }
            ensureHidden(name + '.x', '1');
            ensureHidden(name + '.y', '1');
            form.submit();
        }

        function ProceedConfirmSave() {
            confirmSaveApproved = true;
            HideConfirmSaveModal();
            // Always post the toolbar ImageButton_Save (reliable server Command handler)
            postImageButtonSave();
            return false;
        }

        function ValidateForm() {
            try {
                if (confirmSaveApproved) {
                    confirmSaveApproved = false;
                    return true;
                }

                savePostbackTarget = savePostbackTarget || 'save';
                var isValid = true;
                var errorMsg = '';

                var empCodeEl = elById(ids.txtEmployeeCode);
                var empCode = empCodeEl ? (empCodeEl.value || '') : '';
                if (empCode == '') {
                    errorMsg += '- ' + getLabelText(ids.lblEmployeeCodeMsg) + '\n';
                    isValid = false;
                }

                var changeType = getChangeType();
                var currentJoinDate = getCtrlValue(ids.txtCurrentJoinDate);
                var newJoinDateVal = getCtrlValue(ids.txtNewJoinDate);
                var hdnCurrentClass = elById(ids.hdnCurrentClassID);
                var ddlClass = elById(ids.ddlNewClass);
                var selectedClassID = ddlClass ? ddlClass.value : '';
                var currentClassID = hdnCurrentClass ? hdnCurrentClass.value : '';

                var classChanged = (selectedClassID != '' && selectedClassID != '0' && selectedClassID != currentClassID);
                var joinDateChanged = (newJoinDateVal != '' && newJoinDateVal != currentJoinDate);

                if (changeType === 'join' && !joinDateChanged) {
                    errorMsg += '- ' + getLabelText(ids.lblNewJoinDateMsg) + '\n';
                    isValid = false;
                } else if (changeType === 'class' && !classChanged) {
                    errorMsg += '- ' + (getLabelText(ids.lblNewClassMsg) || 'New Class is required') + '\n';
                    isValid = false;
                } else if ((!changeType || changeType === 'both') && !classChanged && !joinDateChanged) {
                    errorMsg += '- برجاء تغيير تاريخ المباشرة أو الفئة / Please change Join Date or Class\n';
                    isValid = false;
                }

                var reasonJoin = getCtrlValue(ids.txtReasonJoinDate);
                if (reasonJoin == '') {
                    errorMsg += '- ' + getLabelText(ids.lblReasonJoinDateMsg) + '\n';
                    isValid = false;
                }

                if (classChanged && (changeType === 'class' || changeType === 'both' || !changeType)) {
                    var action = getBalanceAction();
                    if (action === '1' || action === '2') {
                        var expireDate = getCtrlValue(ids.txtTransferExpireDate);
                        if (expireDate == '') {
                            errorMsg += '- ' + getLabelText(ids.lblTransferExpireDateMsg) + '\n';
                            isValid = false;
                        }
                    }
                    if (action === '2') {
                        var bal = elById(ids.txtBalanceToTransfer);
                        if (!bal || bal.value == '' || isNaN(parseFloat(bal.value))) {
                            errorMsg += '- ' + (getLabelText(ids.lblBalanceAmountMsg) || 'Please enter balance to transfer') + '\n';
                            isValid = false;
                        } else {
                            var hdnDue = elById(ids.hdnDueBalance);
                            if (hdnDue) hdnDue.value = bal.value;
                        }
                    }
                }

                if (!isValid) {
                    var title = getLabelText(ids.lblValidationTitle) || 'Validation';
                    alert(title + ':\n' + errorMsg);
                    return false;
                }

                try { SyncBalanceActionUI(); } catch (e1) { }
                try { UpdateChangeSummary(); } catch (e2) { }
                ShowConfirmSaveModal();
                return false;
            } catch (ex) {
                alert('ValidateForm error: ' + ex.message);
                return false;
            }
        }

        function SetPageDirection(dir) {
            document.documentElement.dir = dir;
            document.body.dir = dir;
            var div = document.getElementById('DIV');
            if (div) div.dir = dir;
            $('table').each(function () {
                $(this).attr('dir', dir);
            });
        }

        $(document).ready(function () {
            try {
                if (typeof pageDirection !== 'undefined') SetPageDirection(pageDirection);
            } catch (e) { }

            function pick(v) { return (typeof v === 'undefined' || v === null) ? '' : v; }

            ids.txtEmployeeCode = pick(typeof txtEmployeeCodeID !== 'undefined' ? txtEmployeeCodeID : '');
            ids.txtEmployeeName = pick(typeof txtEmployeeNameID !== 'undefined' ? txtEmployeeNameID : '');
            ids.txtCurrentJoinDate = pick(typeof txtCurrentJoinDateID !== 'undefined' ? txtCurrentJoinDateID : '');
            ids.txtLastSalary = pick(typeof txtLastSalaryID !== 'undefined' ? txtLastSalaryID : '');
            ids.txtCurrentClass = pick(typeof txtCurrentClassID !== 'undefined' ? txtCurrentClassID : '');
            ids.txtCurrentBalance = pick(typeof txtCurrentBalanceID !== 'undefined' ? txtCurrentBalanceID : '');
            ids.txtAnnualVacation = pick(typeof txtAnnualVacationID !== 'undefined' ? txtAnnualVacationID : '');
            ids.txtAnnualExpireDate = pick(typeof txtAnnualExpireDateID !== 'undefined' ? txtAnnualExpireDateID : '');
            ids.txtTransferredVacation = pick(typeof txtTransferredVacationID !== 'undefined' ? txtTransferredVacationID : '');
            ids.txtTransferredExpireDate = pick(typeof txtTransferredExpireDateID !== 'undefined' ? txtTransferredExpireDateID : '');
            ids.txtNewJoinDate = pick(typeof txtNewJoinDateID !== 'undefined' ? txtNewJoinDateID : '');
            ids.txtReasonJoinDate = pick(typeof txtReasonJoinDateID !== 'undefined' ? txtReasonJoinDateID : '');
            ids.txtTransferExpireDate = pick(typeof txtTransferExpireDateID !== 'undefined' ? txtTransferExpireDateID : '');
            ids.ddlNewClass = pick(typeof ddlNewClassID !== 'undefined' ? ddlNewClassID : '');
            ids.chkTransferBalance = pick(typeof chkTransferBalanceID !== 'undefined' ? chkTransferBalanceID : '');
            ids.hdnCurrentClassID = pick(typeof hdnCurrentClassIDID !== 'undefined' ? hdnCurrentClassIDID : '');
            ids.lblEmployeeCodeMsg = pick(typeof lblEmployeeCodeMsgID !== 'undefined' ? lblEmployeeCodeMsgID : '');
            ids.lblNewJoinDateMsg = pick(typeof lblNewJoinDateMsgID !== 'undefined' ? lblNewJoinDateMsgID : '');
            ids.lblReasonJoinDateMsg = pick(typeof lblReasonJoinDateMsgID !== 'undefined' ? lblReasonJoinDateMsgID : '');
            ids.lblTransferExpireDateMsg = pick(typeof lblTransferExpireDateMsgID !== 'undefined' ? lblTransferExpireDateMsgID : '');
            ids.lblValidationTitle = pick(typeof lblValidationTitleID !== 'undefined' ? lblValidationTitleID : '');
            ids.lblConfirmMsg = pick(typeof lblConfirmMsgID !== 'undefined' ? lblConfirmMsgID : '');
            ids.ddlChangeType = pick(typeof ddlChangeTypeID !== 'undefined' ? ddlChangeTypeID : '');
            ids.txtClassEffectiveDate = pick(typeof txtClassEffectiveDateID !== 'undefined' ? txtClassEffectiveDateID : '');
            ids.txtBalanceToTransfer = pick(typeof txtBalanceToTransferID !== 'undefined' ? txtBalanceToTransferID : '');
            ids.hdnBalanceAction = pick(typeof hdnBalanceActionID !== 'undefined' ? hdnBalanceActionID : '');
            ids.hdnDueBalance = pick(typeof hdnDueBalanceID !== 'undefined' ? hdnDueBalanceID : '');
            ids.lblDueBalance = pick(typeof lblDueBalanceID !== 'undefined' ? lblDueBalanceID : '');
            ids.ImageButton_Save = pick(typeof ImageButton_SaveID !== 'undefined' ? ImageButton_SaveID : '');
            ids.ImageButton_New = pick(typeof ImageButton_NewID !== 'undefined' ? ImageButton_NewID : '');
            ids.lblNewClassMsg = pick(typeof lblNewClassMsgID !== 'undefined' ? lblNewClassMsgID : '');
            ids.lblEffectiveDateMsg = pick(typeof lblEffectiveDateMsgID !== 'undefined' ? lblEffectiveDateMsgID : '');
            ids.lblBalanceAmountMsg = pick(typeof lblBalanceAmountMsgID !== 'undefined' ? lblBalanceAmountMsgID : '');

            // seed balance field from due balance
            var due = elById(ids.lblDueBalance);
            var balInput = elById(ids.txtBalanceToTransfer);
            if (due && balInput && (!balInput.value || balInput.value === '0' || balInput.value === '0.00')) {
                balInput.value = (due.innerHTML || '0.00').replace(/,/g, '').trim();
            }

            // sync initial radio from checkbox
            var chk = elById(ids.chkTransferBalance);
            if (chk && chk.checked) setBalanceAction('1');
            else setBalanceAction('0');

            if (ids.ddlNewClass) $('#' + ids.ddlNewClass).change(function () { CheckClassChange(); });
            if (ids.ddlChangeType) $('#' + ids.ddlChangeType).change(function () { ApplyChangeTypeVisibility(); });
            if (ids.txtBalanceToTransfer) $('#' + ids.txtBalanceToTransfer).keyup(function () { UpdateChangeSummary(); });
            if (ids.txtReasonJoinDate) $('#' + ids.txtReasonJoinDate).keyup(function () { UpdateChangeSummary(); });

            try { ApplyChangeTypeVisibility(); } catch (e3) { }
            try { UpdateChangeSummary(); } catch (e4) { }
        });
    </script>

</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmChangeJoinDate" runat="server">

        <div style="display: none">
            <asp:Label ID="lblEmployeeCodeMsg" runat="server" Text="Employee Code is required / كود الموظف مطلوب"></asp:Label>
            <asp:Label ID="lblNewJoinDateMsg" runat="server" Text="New Join Date is required / تاريخ المباشرة الجديد مطلوب"></asp:Label>
            <asp:Label ID="lblReasonJoinDateMsg" runat="server" Text="Reason is required / سبب التغيير مطلوب"></asp:Label>
            <asp:Label ID="lblValidationTitle" runat="server" Text="Please complete the following data / الرجاء إكمال البيانات التالية"></asp:Label>
            <asp:Label ID="lblConfirmMsg" runat="server" Text="Are you sure you want to save these changes?" meta:resourcekey="lblConfirmMsg"></asp:Label>
            <asp:Label ID="lblTransferExpireDateMsg" runat="server" Text="Please enter the transfer expire date / برجاء إدخال تاريخ انتهاء الرصيد المرحل"></asp:Label>
            <asp:Label ID="lblNewClassMsg" runat="server" Text="New Employee Class is required / الفئة الجديدة مطلوبة" meta:resourcekey="lblNewClassMsgResource1"></asp:Label>
            <asp:Label ID="lblEffectiveDateMsg" runat="server" Text="Class Change Effective Date is required / تاريخ سريان تغيير الفئة مطلوب" meta:resourcekey="lblEffectiveDateMsgResource1"></asp:Label>
            <asp:Label ID="lblBalanceAmountMsg" runat="server" Text="Please enter balance to transfer / برجاء إدخال الرصيد المراد ترحيله" meta:resourcekey="lblBalanceAmountMsgResource1"></asp:Label>
            <asp:Label ID="lblSumTransfer" runat="server" Text="Transfer {0} Days" meta:resourcekey="lblSumTransferResource1"></asp:Label>
            <asp:Label ID="lblSumManual" runat="server" Text="Manual {0} Days" meta:resourcekey="lblSumManualResource1"></asp:Label>
            <asp:Label ID="lblSumNoTransfer" runat="server" Text="Do not transfer" meta:resourcekey="lblSumNoTransferResource1"></asp:Label>

            <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
            <asp:Label ID="realname" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
            <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1" Width="91px"></asp:TextBox>
            <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
            <asp:HiddenField ID="hdnCurrentClassID" runat="server" />
            <asp:HiddenField ID="hdnEmployeeID" runat="server" />
            <asp:HiddenField ID="hdnDueBalance" runat="server" />
            <asp:HiddenField ID="TxtHDJoinDate" runat="server" />
            <asp:HiddenField ID="TxtHDClassID" runat="server" />
            <asp:HiddenField ID="hdnBalanceAction" runat="server" Value="0" />

            <!-- Kept for server-side compatibility; synced from radio UI -->
            <asp:CheckBox ID="chkTransferBalance" runat="server" CssClass="hidden-server"
                meta:resourcekey="chkTransferBalanceResource1" />
        </div>

        <!-- Confirm save modal -->
        <div id="cjConfirmModal" class="cj-modal-overlay" onclick="if(event.target===this) BackToReview();">
            <div class="cj-modal" role="dialog" aria-modal="true">
                <div class="cj-modal-header">
                    <asp:Label ID="lblConfirmTitleUI" runat="server" Text="Confirm Employee Data Change" meta:resourcekey="lblConfirmTitleResource1"></asp:Label>
                </div>
                <div class="cj-modal-body">
                    <p><asp:Label ID="lblConfirmBody1UI" runat="server" Text="Please review the change details before proceeding." meta:resourcekey="lblConfirmBody1Resource1"></asp:Label></p>
                    <p><asp:Label ID="lblConfirmBody2UI" runat="server" Text="This update may affect the employee’s leave balance, payroll calculations, service duration, and end-of-service benefits." meta:resourcekey="lblConfirmBody2Resource1"></asp:Label></p>
                    <p><asp:Label ID="lblConfirmBody3UI" runat="server" Text="Are you sure you want to save these changes?" meta:resourcekey="lblConfirmBody3Resource1"></asp:Label></p>
                </div>
                <div class="cj-modal-footer">
                    <button type="button" class="btn-footer btn-cancel" onclick="return BackToReview();">
                        <asp:Literal ID="litConfirmBtnBack" runat="server" Text="Back to Review" meta:resourcekey="lblConfirmBtnBackResource1"></asp:Literal>
                    </button>
                    <button type="button" class="btn-footer btn-confirm" onclick="return ProceedConfirmSave();">
                        <asp:Literal ID="litConfirmBtnSave" runat="server" Text="Confirm &amp; Save" meta:resourcekey="lblConfirmBtnSaveResource1"></asp:Literal>
                    </button>
                </div>
            </div>
        </div>

        <div class="Div_MasterContainer" runat="server" id="DIV">

            <!-- Toolbar -->
            <div class="toolbar-wrap">
                <table style="width: 100%; height: 18px; vertical-align: top;">
                    <tr>
                        <td style="display: none">
                            <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N" />
                        </td>
                        <td style="width: 24px">
                            <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                SkinID="HrSave_Command" meta:resourcekey="ImageButton_SaveResource1" CommandArgument="Save"
                                OnClientClick="savePostbackTarget='save'; return ValidateForm();" />
                        </td>
                        <td style="width: 24px">
                            <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                meta:resourcekey="ImageButton_NewResource1" CommandArgument="New" />
                        </td>
                        <td style="width: 40px">
                            <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                        </td>
                        <td style="width: 24px">
                            <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                SkinID="HrPrint_Command" meta:resourcekey="ImageButton_PrintResource1" CommandArgument="Print" />
                        </td>
                        <td style="width: 80px">
                            <asp:ImageButton ID="ImageButton_Properties" Width="16px" Height="16px" runat="server"
                                SkinID="HrProperties_Command" meta:resourcekey="ImageButton_PropertiesResource1"
                                CommandArgument="Property" />
                            <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="خصائص" meta:resourcekey="LinkButton_PropertiesResource1"
                                CommandArgument="Property"></asp:LinkButton>
                        </td>
                        <td style="width: 80px">
                            <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                SkinID="HrRemarks_Command" meta:resourcekey="ImageButton_RemarksResource1" CommandArgument="Remarks" />
                            <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="ملاحظات" meta:resourcekey="LinkButton_RemarksResource1"
                                CommandArgument="Remarks"></asp:LinkButton>
                        </td>
                        <td style="width: 40px">
                            <asp:Label ID="Label_TSP2" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                        </td>
                        <td style="width: 24px">
                            <asp:ImageButton ID="ImageButton_Last" Width="16px" Height="16px" runat="server"
                                SkinID="HrLast_Command" meta:resourcekey="ImageButton_LastResource1" CommandArgument="Last" />
                        </td>
                        <td style="width: 24px">
                            <asp:ImageButton ID="ImageButton_Next" Width="16px" Height="16px" runat="server"
                                SkinID="HrNext_Command" meta:resourcekey="ImageButton_NextResource1" CommandArgument="Next" />
                        </td>
                        <td style="width: 10px"></td>
                        <td style="width: 24px">
                            <asp:ImageButton ID="ImageButton_Back" Width="16px" Height="16px" runat="server"
                                SkinID="HrBack_Command" meta:resourcekey="ImageButton_BackResource1" CommandArgument="Previous" />
                        </td>
                        <td style="width: 24px">
                            <asp:ImageButton ID="ImageButton_First" Width="16px" Height="16px" runat="server"
                                SkinID="HrFirest_Command" meta:resourcekey="ImageButton_FirstResource1" CommandArgument="First" />
                        </td>
                        <td style="width: 30%"></td>
                        <td style="width: 80px">
                            <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                            <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
                        </td>
                        <td style="width: 5%"></td>
                    </tr>
                </table>
            </div>

            <!-- Page header -->
            <div class="page-header">
                <div class="page-header-icon">
                    <asp:Image ID="Image_Logo" runat="server" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png"
                        meta:resourcekey="Image_LogoResource2" />
                </div>
                <div class="page-header-main">
                    <div class="page-header-title">
                        <asp:Label ID="Label_Header" runat="server" meta:resourcekey="Label_HeaderResource1"></asp:Label>
                    </div>
                    <p class="page-header-sub">
                        <asp:Label ID="Label_HeaderSub" runat="server"
                            Text="Update employment data and define how accrued leave balance should be handled."
                            meta:resourcekey="Label_HeaderSubResource1"></asp:Label>
                    </p>
                </div>
                <div class="page-header-meta">
                    <div>
                        <span class="meta-label"><asp:Label ID="lblRegDate" runat="server" Text="Registered On" meta:resourcekey="lblRegDateResource2"></asp:Label></span>
                        <span class="meta-value"> <asp:Label ID="lblRegDateValue" runat="server" Text="—" meta:resourcekey="lblRegDateValueResource2"></asp:Label></span>
                    </div>
                    <div>
                        <span class="meta-label"><asp:Label ID="lblRegUser" runat="server" Text="Registered By" meta:resourcekey="lblRegUserResource2"></asp:Label></span>
                        <span class="meta-value"> <asp:Label ID="lblRegUserValue" runat="server" Text="—" meta:resourcekey="lblRegUserValueResource2"></asp:Label></span>
                    </div>
                    <div>
                        <span class="meta-label"><asp:Label ID="lblCancelDate" runat="server" Text="Cancel Date" meta:resourcekey="lblCancelDateResource2"></asp:Label></span>
                        <span class="meta-value"> <asp:Label ID="lblCancelDateValue" runat="server" Text="—" meta:resourcekey="lblCancelDateValueResource2"></asp:Label></span>
                    </div>
                </div>
            </div>

            <div class="Details">
                <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                    meta:resourcekey="UltraWebTab1Resource2">
                    <Tabs>
                        <igtab:Tab Text="تغيير تاريخ المباشرة" meta:resourcekey="TabResource2">
                            <ContentTemplate>
                                <div class="cj-tab-content">

                                    <!-- Employee Information -->
                                    <div class="form-card">
                                        <div class="form-card-header">
                                            <asp:Label ID="lblEmployeeInfo" runat="server" Text="Employee Information"
                                                meta:resourcekey="lblEmployeeInfoResource1"></asp:Label>
                                        </div>
                                        <div class="form-card-body">
                                            <div class="field-grid">
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblEmployeeCode" runat="server" Text="Employee Code"
                                                            meta:resourcekey="lblEmployeeCodeResource1"></asp:Label>
                                                        <span class="required-star">*</span>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="form-input" MaxLength="30"
                                                            AutoPostBack="True" meta:resourcekey="txtEmployeeCodeResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name"
                                                            meta:resourcekey="lblEmployeeNameResource1"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="form-input form-input-readonly"
                                                            ReadOnly="True" meta:resourcekey="txtEmployeeNameResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- Current Employment + Balance & Payroll -->
                                    <div class="two-col">
                                        <div class="form-card">
                                            <div class="form-card-header">
                                                <asp:Label ID="lblCurrentInfo" runat="server" Text="Current Employment Details"
                                                    meta:resourcekey="lblCurrentInfoResource1"></asp:Label>
                                            </div>
                                            <div class="form-card-body stack-fields">
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblCurrentJoinDate" runat="server" Text="Current Join Date"
                                                            meta:resourcekey="lblCurrentJoinDateResource1"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:TextBox ID="txtCurrentJoinDate" runat="server" CssClass="form-input form-input-readonly"
                                                            ReadOnly="True" meta:resourcekey="txtCurrentJoinDateResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblCurrentClass" runat="server" Text="Current Employee Class"
                                                            meta:resourcekey="lblCurrentClassResource1"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:TextBox ID="txtCurrentClass" runat="server" CssClass="form-input form-input-readonly"
                                                            ReadOnly="True" meta:resourcekey="txtCurrentClassResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblAnnualVacation" runat="server" Text="Annual Leave Entitlement"
                                                            meta:resourcekey="lblAnnualVacationResource1"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:TextBox ID="txtAnnualVacation" runat="server" CssClass="form-input form-input-readonly"
                                                            ReadOnly="True" meta:resourcekey="txtAnnualVacationResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblCurrentBalance" runat="server" Text="Current Available Balance"
                                                            meta:resourcekey="lblCurrentBalanceResource1"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:TextBox ID="txtCurrentBalance" runat="server" CssClass="form-input form-input-readonly"
                                                            ReadOnly="True" meta:resourcekey="txtCurrentBalanceResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-card">
                                            <div class="form-card-header">
                                                <asp:Label ID="lblBalancePayroll" runat="server" Text="Balance &amp; Payroll Information"
                                                    meta:resourcekey="lblBalancePayrollResource1"></asp:Label>
                                            </div>
                                            <div class="form-card-body stack-fields">
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblLastSalary" runat="server" Text="Last Payroll Period"
                                                            meta:resourcekey="lblLastSalaryResource1"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:TextBox ID="txtLastSalary" runat="server" CssClass="form-input form-input-readonly"
                                                            ReadOnly="True" meta:resourcekey="txtLastSalaryResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblAnnualExpireDate" runat="server" Text="Balance Expiry Date"
                                                            meta:resourcekey="lblAnnualExpireDateResource1"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:TextBox ID="txtAnnualExpireDate" runat="server" CssClass="form-input form-input-readonly"
                                                            ReadOnly="True" meta:resourcekey="txtAnnualExpireDateResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblTransferredVacation" runat="server" Text="Previously Transferred Balance"
                                                            meta:resourcekey="lblTransferredVacationResource1"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:TextBox ID="txtTransferredVacation" runat="server" CssClass="form-input form-input-readonly"
                                                            ReadOnly="True" meta:resourcekey="txtTransferredVacationResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblTransferredExpireDate" runat="server" Text="Transferred Balance Expiry"
                                                            meta:resourcekey="lblTransferredExpireDateResource1"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:TextBox ID="txtTransferredExpireDate" runat="server" CssClass="form-input form-input-readonly"
                                                            ReadOnly="True" meta:resourcekey="txtTransferredExpireDateResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- Accrued leave balance -->
                                    <div class="accrued-box">
                                        <div>
                                            <div class="accrued-label">
                                                <asp:Label ID="lblDueBalanceTitle" runat="server" Text="Accrued leave balance as of today"
                                                    meta:resourcekey="lblDueBalanceTitleResource1"></asp:Label>
                                            </div>
                                            <div class="accrued-value">
                                                <asp:Label ID="lblDueBalance" runat="server" Text="0.00"
                                                    meta:resourcekey="lblDueBalanceResource1"></asp:Label>
                                                <asp:Label ID="lblDueBalanceUnit" runat="server" Text=" Days"
                                                    meta:resourcekey="lblDueBalanceUnitResource1"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="accrued-icon" title="Balance">▣</div>
                                    </div>

                                    <!-- Proposed Changes -->
                                    <div class="form-card">
                                        <div class="form-card-header">
                                            <asp:Label ID="lblNewData" runat="server" Text="Proposed Changes"
                                                meta:resourcekey="lblNewDataResource1"></asp:Label>
                                        </div>
                                        <div class="form-card-body">
                                            <div class="field-grid">
                                                <div class="field-item" id="rowChangeType">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblChangeType" runat="server" Text="Change Type"
                                                            meta:resourcekey="lblChangeTypeResource1"></asp:Label>
                                                        <span class="required-star">*</span>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:DropDownList ID="ddlChangeType" runat="server" CssClass="form-input"
                                                            meta:resourcekey="ddlChangeTypeResource1">
                                                            <asp:ListItem Value="both" Text="Join Date and Employee Class" meta:resourcekey="ddlChangeType_Both"></asp:ListItem>
                                                            <asp:ListItem Value="join" Text="Join Date Only" meta:resourcekey="ddlChangeType_Join"></asp:ListItem>
                                                            <asp:ListItem Value="class" Text="Employee Class Only" meta:resourcekey="ddlChangeType_Class"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="field-item" id="rowClassEffectiveDate">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblClassEffectiveDate" runat="server" Text="Class Change Effective Date"
                                                            meta:resourcekey="lblClassEffectiveDateResource1"></asp:Label>
                                                        <span class="required-star">*</span>
                                                    </span>
                                                    <div class="field-control">
                                                        <igsch:WebDateChooser ID="txtClassEffectiveDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                            BorderWidth="1px" Height="28px" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                            Width="100%" meta:resourcekey="txtClassEffectiveDateResource1">
                                                        </igsch:WebDateChooser>
                                                    </div>
                                                </div>
                                                <div class="field-item" id="rowNewJoinDate">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblNewJoinDate" runat="server" Text="New Join Date"
                                                            meta:resourcekey="lblNewJoinDateResource1"></asp:Label>
                                                        <span class="required-star">*</span>
                                                    </span>
                                                    <div class="field-control">
                                                        <igsch:WebDateChooser ID="txtNewJoinDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                            BorderWidth="1px" Height="28px" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                            Width="100%">
                                                            <AutoPostBack ValueChanged="True" />
                                                        </igsch:WebDateChooser>
                                                    </div>
                                                </div>
                                                <div class="field-item" id="rowNewClass">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblNewClass" runat="server" Text="New Employee Class"
                                                            meta:resourcekey="lblNewClassResource1"></asp:Label>
                                                        <span class="required-star">*</span>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:DropDownList ID="ddlNewClass" runat="server" CssClass="form-input"
                                                            Width="100%" AutoPostBack="True" meta:resourcekey="ddlNewClassResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- Balance Handling -->
                                    <div id="divClassChangeVacation" class="form-card" style="display: none;">
                                        <div class="form-card-header">
                                            <asp:Label ID="lblBalanceHandling" runat="server" Text="Balance Handling"
                                                meta:resourcekey="lblBalanceHandlingResource1"></asp:Label>
                                        </div>
                                        <div class="form-card-body">
                                            <asp:Label ID="lblClassChangeMessage" runat="server" CssClass="hidden-server"
                                                Text="سيتم تغيير الفئة. هل تريد ترحيل الرصيد المستحق حتى اليوم؟"
                                                meta:resourcekey="lblClassChangeMessageResource1"></asp:Label>

                                            <div class="balance-options">
                                                <div class="balance-option" data-value="1" onclick="setBalanceAction('1');">
                                                    <label>
                                                        <span class="opt-title">
                                                            <input type="radio" name="rdoBalanceAction" value="1" onclick="setBalanceAction('1');" />
                                                            <asp:Label ID="lblOptTransfer" runat="server" Text="Transfer accrued balance to the new class"
                                                                meta:resourcekey="lblOptTransferResource1"></asp:Label>
                                                        </span>
                                                        <span class="opt-desc">
                                                            <asp:Label ID="lblOptTransferDesc" runat="server"
                                                                Text="The accrued balance up to the effective date will be carried forward."
                                                                meta:resourcekey="lblOptTransferDescResource1"></asp:Label>
                                                        </span>
                                                    </label>
                                                </div>
                                                <div class="balance-option" data-value="0" onclick="setBalanceAction('0');">
                                                    <label>
                                                        <span class="opt-title">
                                                            <input type="radio" name="rdoBalanceAction" value="0" onclick="setBalanceAction('0');" />
                                                            <asp:Label ID="lblOptNoTransfer" runat="server" Text="Do not transfer the accrued balance"
                                                                meta:resourcekey="lblOptNoTransferResource1"></asp:Label>
                                                        </span>
                                                        <span class="opt-desc">
                                                            <asp:Label ID="lblOptNoTransferDesc" runat="server"
                                                                Text="The old balance will not be carried to the new class."
                                                                meta:resourcekey="lblOptNoTransferDescResource1"></asp:Label>
                                                        </span>
                                                    </label>
                                                </div>
                                                <div class="balance-option" data-value="2" onclick="setBalanceAction('2');">
                                                    <label>
                                                        <span class="opt-title">
                                                            <input type="radio" name="rdoBalanceAction" value="2" onclick="setBalanceAction('2');" />
                                                            <asp:Label ID="lblOptManual" runat="server" Text="Adjust balance manually"
                                                                meta:resourcekey="lblOptManualResource1"></asp:Label>
                                                        </span>
                                                        <span class="opt-desc">
                                                            <asp:Label ID="lblOptManualDesc" runat="server"
                                                                Text="Enter the exact balance that should be transferred."
                                                                meta:resourcekey="lblOptManualDescResource1"></asp:Label>
                                                        </span>
                                                    </label>
                                                </div>
                                            </div>

                                            <div id="divTransferExpireFields" class="field-grid">
                                                <div class="field-item">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblBalanceToTransfer" runat="server" Text="Balance to Be Transferred"
                                                            meta:resourcekey="lblBalanceToTransferResource1"></asp:Label>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:TextBox ID="txtBalanceToTransfer" runat="server" CssClass="form-input form-input-readonly"
                                                            ReadOnly="True" Text="0.00" meta:resourcekey="txtBalanceToTransferResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="field-item" id="divTransferExpireDate">
                                                    <span class="field-label">
                                                        <asp:Label ID="lblTransferExpireDate" runat="server" Text="Transferred Balance Expiry Date"
                                                            meta:resourcekey="lblTransferExpireDateResource1"></asp:Label>
                                                        <span class="required-star">*</span>
                                                    </span>
                                                    <div class="field-control">
                                                        <igsch:WebDateChooser ID="txtTransferExpireDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                            BorderWidth="1px" Height="28px" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                            Width="100%" meta:resourcekey="txtTransferExpireDateResource1">
                                                        </igsch:WebDateChooser>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- Reason for Change -->
                                    <div class="form-card">
                                        <div class="form-card-header">
                                            <asp:Label ID="lblReasonSection" runat="server" Text="Reason for Change"
                                                meta:resourcekey="lblReasonSectionResource1"></asp:Label>
                                        </div>
                                        <div class="form-card-body">
                                            <div class="field-grid">
                                                <div class="field-item full" style="align-items: flex-start;">
                                                    <span class="field-label" style="padding-top: 6px;">
                                                        <asp:Label ID="lblReasonJoinDate" runat="server" Text="Reason"
                                                            meta:resourcekey="lblReasonJoinDateResource1"></asp:Label>
                                                        <span class="required-star">*</span>
                                                    </span>
                                                    <div class="field-control">
                                                        <asp:TextBox ID="txtReasonJoinDate" runat="server"
                                                            TextMode="MultiLine" CssClass="form-input" Rows="3"
                                                            meta:resourcekey="txtReasonJoinDateResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- Change Summary -->
                                    <div class="form-card" id="sectionChangeSummary">
                                        <div class="form-card-header">
                                            <asp:Label ID="lblChangeSummary" runat="server" Text="Change Summary"
                                                meta:resourcekey="lblChangeSummaryResource1"></asp:Label>
                                        </div>
                                        <div class="form-card-body">
                                            <div class="summary-rows">
                                                <div class="summary-row">
                                                    <span class="sum-label"><asp:Label ID="lblSumJoinDate" runat="server" Text="Join Date" meta:resourcekey="lblSumJoinDateResource1"></asp:Label></span>
                                                    <span class="sum-value" id="sumJoinDate">—</span>
                                                </div>
                                                <div class="summary-row">
                                                    <span class="sum-label"><asp:Label ID="lblSumClass" runat="server" Text="Employee Class" meta:resourcekey="lblSumClassResource1"></asp:Label></span>
                                                    <span class="sum-value" id="sumClass">—</span>
                                                </div>
                                                <div class="summary-row">
                                                    <span class="sum-label"><asp:Label ID="lblSumEffective" runat="server" Text="Class Effective Date" meta:resourcekey="lblSumEffectiveResource1"></asp:Label></span>
                                                    <span class="sum-value" id="sumEffectiveDate">—</span>
                                                </div>
                                                <div class="summary-row">
                                                    <span class="sum-label"><asp:Label ID="lblSumBalanceAction" runat="server" Text="Balance Action" meta:resourcekey="lblSumBalanceActionResource1"></asp:Label></span>
                                                    <span class="sum-value" id="sumBalanceAction">—</span>
                                                </div>
                                                <div class="summary-row">
                                                    <span class="sum-label"><asp:Label ID="lblSumExpire" runat="server" Text="Transferred Balance Expiry" meta:resourcekey="lblSumExpireResource1"></asp:Label></span>
                                                    <span class="sum-value" id="sumExpireDate">—</span>
                                                </div>
                                            </div>
                                            <div class="warning-box">
                                                <asp:Label ID="lblWarningSummary" runat="server"
                                                    Text="This change may affect leave balances, payroll calculations, employee service duration, and end-of-service calculations. Please review the summary before saving."
                                                    meta:resourcekey="lblWarningSummaryResource1"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div>
                                        <asp:Label ID="lblMessage" runat="server" CssClass="msg-success" />
                                        <asp:Label ID="lblErrorMessage" runat="server" CssClass="msg-error" />
                                    </div>

                                    <!-- Footer actions -->
                                    <div class="form-footer">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-footer btn-cancel"
                                            OnClientClick="return CancelForm();" CausesValidation="False"
                                            meta:resourcekey="btnCancelResource1" />
                                        <asp:Button ID="btnPreviewImpact" runat="server" Text="Preview Impact" CssClass="btn-footer btn-preview"
                                            OnClientClick="return PreviewImpact();" CausesValidation="False"
                                            meta:resourcekey="btnPreviewImpactResource1" />
                                        <asp:Button ID="btnConfirmSave" runat="server" Text="Confirm &amp; Save" CssClass="btn-footer btn-confirm"
                                            OnClientClick="savePostbackTarget='confirm'; return ConfirmAndSave();" CausesValidation="False"
                                            UseSubmitBehavior="false"
                                            meta:resourcekey="btnConfirmSaveResource1" />
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
