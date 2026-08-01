<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEndActingAssignment.aspx.vb"
    Inherits="frmEndActingAssignment" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

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
            background: #1f3a5f;
            color: #fff;
            padding: 10px 18px;
            font-weight: bold;
            border-left: 1px solid #16304f;
            border-right: 1px solid #16304f;
        }
        .section-card {
            background: #fff;
            border: 1px solid #9ec3e6;
            border-radius: 8px;
            margin-bottom: 14px;
            overflow: hidden;
            box-shadow: 0 1px 3px rgba(0,0,0,0.06);
        }
        .section-header {
            background: #2f80c1;
            color: #fff;
            padding: 10px 14px;
            font-weight: bold;
        }
        .section-body {
            padding: 14px;
        }
        .search-row {
            display: flex;
            flex-wrap: wrap;
            align-items: center;
            gap: 10px;
            margin-bottom: 12px;
        }
        .field-label {
            background: #eaf4ff;
            border: 1px solid #b7d4ef;
            border-radius: 4px;
            padding: 6px 10px;
            color: #1a5276;
            font-weight: bold;
            white-space: nowrap;
            min-width: 160px;
            display: inline-block;
        }
        .assignment-display {
            flex: 1 1 280px;
            min-width: 220px;
            padding: 8px 10px;
            background: #f4f8fc;
            border: 1px solid #c9daf0;
            border-radius: 4px;
            color: #1a5276;
            font-weight: bold;
        }
        .btn-search {
            background: #3498db !important;
            color: #fff !important;
            border: 1px solid #2176b3 !important;
            padding: 8px 16px !important;
            border-radius: 4px;
            cursor: pointer;
            font-size: 13px !important;
            font-weight: bold !important;
            white-space: nowrap;
        }
        .btn-search:hover { background: #2980b9 !important; }
        .info-box {
            background: #fff8e6;
            border: 1px solid #f0d78c;
            border-radius: 6px;
            padding: 10px 12px;
            color: #6a5500;
            font-size: 12px;
            line-height: 1.5;
        }
        .details-grid {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
        }
        .detail-box {
            flex: 1 1 200px;
            min-width: 180px;
            border: 1px solid #d5dde8;
            border-radius: 8px;
            padding: 12px;
            background: #f8fbfe;
        }
        .detail-label {
            display: block;
            font-size: 11px;
            color: #667;
            margin-bottom: 6px;
        }
        .detail-value {
            display: block;
            font-size: 14px;
            font-weight: bold;
            color: #1f3a5f;
        }
        .status-active {
            display: inline-block;
            background: #e8f8ef;
            color: #1e7e34;
            border: 1px solid #a9dfbf;
            border-radius: 12px;
            padding: 4px 12px;
            font-size: 12px;
            font-weight: bold;
        }
        .summary-strip {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            margin-bottom: 14px;
        }
        .summary-item {
            flex: 1 1 160px;
            background: #fff;
            border: 1px solid #9ec3e6;
            border-radius: 8px;
            padding: 10px 12px;
        }
        .summary-item .detail-label { margin-bottom: 4px; }
        .form-row {
            display: flex;
            flex-wrap: wrap;
            align-items: flex-start;
            gap: 10px;
            margin-bottom: 14px;
        }
        .form-row .field-label {
            min-width: 180px;
            margin-top: 4px;
        }
        .form-control-wrap {
            flex: 1 1 320px;
            min-width: 260px;
        }
        .assign-panels {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
        }
        .assign-panel {
            flex: 1 1 260px;
            border: 1px solid #c5d6e8;
            border-radius: 8px;
            padding: 12px;
            background: #f8fbfe;
            min-width: 240px;
        }
        .assign-panel.active {
            border-color: #2f80c1;
            background: #eaf4ff;
            box-shadow: inset 0 0 0 1px #9ec3e6;
        }
        .assign-panel.disabled-panel {
            opacity: 0.65;
        }
        .assign-help {
            margin-top: 8px;
            font-size: 11px;
            color: #556;
            line-height: 1.45;
        }
        .emp-pick-row {
            display: flex;
            flex-wrap: wrap;
            align-items: center;
            gap: 8px;
            margin-top: 8px;
        }
        .emp-pick-row input[type="text"] {
            padding: 6px 8px;
            border: 1px solid #c5ced9;
            border-radius: 4px;
            font-size: 12px;
        }
        .emp-name-display {
            display: inline-block;
            min-width: 160px;
            padding: 6px 10px;
            background: #fff;
            border: 1px solid #c9daf0;
            border-radius: 4px;
            color: #1a5276;
            font-weight: bold;
        }
        .remarks-box {
            width: 100%;
            min-height: 80px;
            padding: 8px;
            border: 1px solid #c5ced9;
            border-radius: 4px;
            font-family: Tahoma, Arial, sans-serif;
            font-size: 12px;
            box-sizing: border-box;
        }
        .footer {
            margin-top: 8px;
            display: flex;
            gap: 10px;
            flex-wrap: wrap;
            justify-content: flex-end;
            align-items: center;
        }
        .btn-end {
            background: #e74c3c !important;
            color: #fff !important;
            border: 1px solid #c0392b !important;
            padding: 10px 22px !important;
            border-radius: 4px;
            cursor: pointer;
            font-size: 13px !important;
            font-weight: bold !important;
        }
        .btn-end:hover { background: #c0392b !important; }
        .btn-cancel {
            background: #d6e6f5 !important;
            color: #1f3a5f !important;
            border: 1px solid #9ec3e6 !important;
            padding: 10px 22px !important;
            border-radius: 4px;
            cursor: pointer;
            font-size: 13px !important;
            font-weight: bold !important;
        }
        .btn-cancel:hover { background: #c5d6e8 !important; }
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

            function syncAssignPanels() {
                var chkEmp = document.getElementById('<%= chkAssignEmployee.ClientID %>');
                var chkPos = document.getElementById('<%= chkAssignPosition.ClientID %>');
                var pnlEmp = document.getElementById('pnlAssignEmployee');
                var pnlPos = document.getElementById('pnlAssignPosition');
                if (pnlEmp) {
                    pnlEmp.className = 'assign-panel' + (chkEmp && chkEmp.checked ? ' active' : ' disabled-panel');
                }
                if (pnlPos) {
                    pnlPos.className = 'assign-panel' + (chkPos && chkPos.checked ? ' active' : ' disabled-panel');
                }
            }
        </script>

        <div class="page-title-bar">
            <asp:Label ID="lblPageTitle" runat="server" meta:resourcekey="lblPageTitleResource1" />
        </div>
        <div class="clearance-bar">
            <asp:Label ID="lblSubtitle" runat="server" meta:resourcekey="lblSubtitleResource1" />
        </div>

        <!-- Select Active Acting Assignment -->
        <div class="section-card">
            <div class="section-header">
                <asp:Label ID="lblSelectSection" runat="server" meta:resourcekey="lblSelectSectionResource1" />
            </div>
            <div class="section-body">
                <div class="search-row">
                    <asp:Label ID="lblActiveAssignment" runat="server" CssClass="field-label" meta:resourcekey="lblActiveAssignmentResource1" />
                    <asp:TextBox ID="txtAssignmentCode" runat="server" CssClass="assignment-display"
                        AutoPostBack="True" OnTextChanged="txtAssignmentCode_TextChanged" />
                    <asp:Button ID="btnSearchAssignment" runat="server" CssClass="btn-search"
                        meta:resourcekey="btnSearchAssignmentResource1" />
                </div>
                <div class="info-box">
                    <asp:Label ID="lblSelectInfo" runat="server" meta:resourcekey="lblSelectInfoResource1" />
                </div>
            </div>
        </div>

        <!-- Current Acting Assignment Details -->
        <asp:Panel ID="pnlDetails" runat="server" CssClass="section-card" Visible="False">
            <div class="section-header">
                <asp:Label ID="lblDetailsSection" runat="server" meta:resourcekey="lblDetailsSectionResource1" />
            </div>
            <div class="section-body">
                <div class="details-grid">
                    <div class="detail-box">
                        <span class="detail-label"><asp:Label ID="lblAssignmentNoCaption" runat="server" meta:resourcekey="lblAssignmentNoCaptionResource1" /></span>
                        <span class="detail-value"><asp:Label ID="lblAssignmentNo" runat="server" /></span>
                    </div>
                    <div class="detail-box">
                        <span class="detail-label"><asp:Label ID="lblOriginalEmployeeCaption" runat="server" meta:resourcekey="lblOriginalEmployeeCaptionResource1" /></span>
                        <span class="detail-value"><asp:Label ID="lblOriginalEmployee" runat="server" /></span>
                    </div>
                    <div class="detail-box">
                        <span class="detail-label"><asp:Label ID="lblActingEmployeeCaption" runat="server" meta:resourcekey="lblActingEmployeeCaptionResource1" /></span>
                        <span class="detail-value"><asp:Label ID="lblActingEmployee" runat="server" /></span>
                    </div>
                    <div class="detail-box">
                        <span class="detail-label"><asp:Label ID="lblPositionGradeCaption" runat="server" meta:resourcekey="lblPositionGradeCaptionResource1" /></span>
                        <span class="detail-value"><asp:Label ID="lblPositionGrade" runat="server" /></span>
                    </div>
                    <div class="detail-box">
                        <span class="detail-label"><asp:Label ID="lblBranchCaption" runat="server" meta:resourcekey="lblBranchCaptionResource1" /></span>
                        <span class="detail-value"><asp:Label ID="lblBranch" runat="server" /></span>
                    </div>
                    <div class="detail-box">
                        <span class="detail-label"><asp:Label ID="lblEffectiveFromCaption" runat="server" meta:resourcekey="lblEffectiveFromCaptionResource1" /></span>
                        <span class="detail-value"><asp:Label ID="lblEffectiveFrom" runat="server" /></span>
                    </div>
                    <div class="detail-box">
                        <span class="detail-label"><asp:Label ID="lblScopeCaption" runat="server" meta:resourcekey="lblScopeCaptionResource1" /></span>
                        <span class="detail-value"><asp:Label ID="lblScope" runat="server" /></span>
                    </div>
                    <div class="detail-box">
                        <span class="detail-label"><asp:Label ID="lblStatusCaption" runat="server" meta:resourcekey="lblStatusCaptionResource1" /></span>
                        <span class="detail-value"><asp:Label ID="lblStatus" runat="server" CssClass="status-active" /></span>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- End & Reassign Responsibilities -->
        <asp:Panel ID="pnlReassign" runat="server" Visible="False">
            <div class="summary-strip">
                <div class="summary-item">
                    <span class="detail-label"><asp:Label ID="lblSumBranchCaption" runat="server" meta:resourcekey="lblBranchCaptionResource1" /></span>
                    <span class="detail-value"><asp:Label ID="lblSumBranch" runat="server" /></span>
                </div>
                <div class="summary-item">
                    <span class="detail-label"><asp:Label ID="lblSumFromCaption" runat="server" meta:resourcekey="lblEffectiveFromCaptionResource1" /></span>
                    <span class="detail-value"><asp:Label ID="lblSumFrom" runat="server" /></span>
                </div>
                <div class="summary-item">
                    <span class="detail-label"><asp:Label ID="lblSumScopeCaption" runat="server" meta:resourcekey="lblScopeCaptionResource1" /></span>
                    <span class="detail-value"><asp:Label ID="lblSumScope" runat="server" /></span>
                </div>
                <div class="summary-item">
                    <span class="detail-label"><asp:Label ID="lblSumStatusCaption" runat="server" meta:resourcekey="lblStatusCaptionResource1" /></span>
                    <span class="detail-value"><asp:Label ID="lblSumStatus" runat="server" CssClass="status-active" /></span>
                </div>
            </div>

            <div class="section-card">
                <div class="section-header">
                    <asp:Label ID="lblReassignSection" runat="server" meta:resourcekey="lblReassignSectionResource1" />
                </div>
                <div class="section-body">
                    <div class="form-row">
                        <asp:Label ID="lblEffectiveTo" runat="server" CssClass="field-label" meta:resourcekey="lblEffectiveToResource1" />
                        <div class="form-control-wrap">
                            <igsch:WebDateChooser ID="txtEffectiveTo" runat="server" Width="180px" Height="22px" />
                        </div>
                    </div>

                    <div class="form-row">
                        <asp:Label ID="lblNewApproval" runat="server" CssClass="field-label" meta:resourcekey="lblNewApprovalResource1" />
                        <div class="form-control-wrap">
                            <div class="assign-panels">
                                <div id="pnlAssignEmployee" class="assign-panel active">
                                    <asp:CheckBox ID="chkAssignEmployee" runat="server" Checked="True" AutoPostBack="True"
                                        OnCheckedChanged="chkAssignTarget_CheckedChanged"
                                        onclick="syncAssignPanels();"
                                        meta:resourcekey="chkAssignEmployeeResource1" />
                                    <div class="emp-pick-row">
                                        <asp:TextBox ID="txtApprovalEmpCode" runat="server" Width="100px" MaxLength="30"
                                            AutoPostBack="True" OnTextChanged="txtApprovalEmpCode_TextChanged" />
                                        <asp:ImageButton ID="btnSearchApprovalEmp" runat="server"
                                            ImageUrl="./Img/forum_search.gif" Width="24px" Height="18px"
                                            meta:resourcekey="btnSearchApprovalEmpResource1" />
                                        <asp:Label ID="lblApprovalEmpName" runat="server" CssClass="emp-name-display" />
                                    </div>
                                    <div class="assign-help">
                                        <asp:Label ID="lblAssignEmployeeHelp" runat="server" meta:resourcekey="lblAssignEmployeeHelpResource1" />
                                    </div>
                                </div>
                                <div id="pnlAssignPosition" class="assign-panel disabled-panel">
                                    <asp:CheckBox ID="chkAssignPosition" runat="server" AutoPostBack="True"
                                        OnCheckedChanged="chkAssignTarget_CheckedChanged"
                                        onclick="syncAssignPanels();"
                                        meta:resourcekey="chkAssignPositionResource1" />
                                    <div class="emp-pick-row">
                                        <asp:TextBox ID="txtPositionCode" runat="server" Width="100px" MaxLength="30"
                                            AutoPostBack="True" OnTextChanged="txtPositionCode_TextChanged" />
                                        <asp:ImageButton ID="btnSearchPosition" runat="server"
                                            ImageUrl="./Img/forum_search.gif" Width="24px" Height="18px"
                                            meta:resourcekey="btnSearchPositionResource1" />
                                        <asp:Label ID="lblPositionName" runat="server" CssClass="emp-name-display" />
                                    </div>
                                    <div class="assign-help">
                                        <asp:Label ID="lblAssignPositionHelp" runat="server" meta:resourcekey="lblAssignPositionHelpResource1" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-row">
                        <asp:Label ID="lblNewManager" runat="server" CssClass="field-label" meta:resourcekey="lblNewManagerResource1" />
                        <div class="form-control-wrap">
                            <div class="emp-pick-row" style="margin-top:0;">
                                <asp:TextBox ID="txtNewManagerCode" runat="server" Width="100px" MaxLength="30"
                                    AutoPostBack="True" OnTextChanged="txtNewManagerCode_TextChanged" />
                                <asp:ImageButton ID="btnSearchNewManager" runat="server"
                                    ImageUrl="./Img/forum_search.gif" Width="24px" Height="18px"
                                    meta:resourcekey="btnSearchNewManagerResource1" />
                                <asp:Label ID="lblNewManagerName" runat="server" CssClass="emp-name-display" />
                            </div>
                        </div>
                    </div>

                    <div class="form-row">
                        <asp:Label ID="lblRemarks" runat="server" CssClass="field-label" meta:resourcekey="lblRemarksResource1" />
                        <div class="form-control-wrap">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="remarks-box" Rows="3" />
                        </div>
                    </div>

                    <div class="info-box">
                        <asp:Label ID="lblEndInfo" runat="server" meta:resourcekey="lblEndInfoResource1" />
                    </div>

                    <div class="footer">
                        <asp:Button ID="btnEndAssignment" runat="server" CssClass="btn-end"
                            OnClick="btnEndAssignment_Click"
                            meta:resourcekey="btnEndAssignmentResource1" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn-cancel"
                            OnClick="btnCancel_Click"
                            meta:resourcekey="btnCancelResource1" />
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Label ID="lblMessage" runat="server" CssClass="msg" />

        <asp:HiddenField ID="hdnAssignmentID" runat="server" Value="0" />
        <asp:HiddenField ID="hdnEmployeeAssignmentID" runat="server" Value="0" />
        <asp:HiddenField ID="hdnPositionAssignmentID" runat="server" Value="0" />
        <asp:HiddenField ID="hdnSourceID" runat="server" Value="0" />
        <asp:HiddenField ID="hdnAssignmentType" runat="server" Value="" />
        <asp:HiddenField ID="hdnOriginalEmployeeID" runat="server" Value="0" />
        <asp:HiddenField ID="hdnActingEmployeeID" runat="server" Value="0" />
        <asp:HiddenField ID="hdnOriginalPositionID" runat="server" Value="0" />
        <asp:HiddenField ID="hdnApprovalEmployeeID" runat="server" Value="0" />
        <asp:HiddenField ID="hdnNewManagerID" runat="server" Value="0" />
        <asp:HiddenField ID="hdnPositionID" runat="server" Value="0" />
        <asp:HiddenField ID="hdnHasApprovals" runat="server" Value="0" />
        <asp:HiddenField ID="hdnHasDirectManager" runat="server" Value="0" />
    </form>
    <script type="text/javascript">
        if (window.addEventListener) {
            window.addEventListener('load', syncAssignPanels, false);
        } else if (window.attachEvent) {
            window.attachEvent('onload', syncAssignPanels);
        }
    </script>
</body>
</html>
