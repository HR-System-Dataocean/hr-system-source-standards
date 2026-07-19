<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSelfServiceRequestsPopup.aspx.vb"
    Inherits="frmSelfServiceRequestsPopup" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <style type="text/css">
        body {
            font-family: Tahoma, Arial, sans-serif;
            font-size: 12px;
            padding: 15px;
            background: #f5f5f5;
        }
        .header {
            background: #2c3e50;
            color: white;
            padding: 15px 20px;
            border-radius: 5px;
            margin-bottom: 20px;
        }
        .header h2 {
            margin: 0 0 10px 0;
        }
        .header .emp-info {
            font-size: 14px;
            color: #ecf0f1;
        }
        .grid-container {
            background: white;
            border-radius: 5px;
            padding: 15px;
            margin-bottom: 20px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }
        .grid-title {
            background: #3498db;
            color: white;
            padding: 10px 15px;
            border-radius: 4px;
            font-weight: bold;
            margin-bottom: 10px;
            font-size: 14px;
        }
        table {
            width: 100%;
            border-collapse: collapse;
        }
        th {
            background: #ecf0f1;
            padding: 10px 8px;
            border: 1px solid #ddd;
            font-weight: bold;
        }
        td {
            padding: 8px;
            border: 1px solid #ddd;
        }
        tr:nth-child(even) {
            background: #f9f9f9;
        }
        tr:hover {
            background: #f1f1f1;
        }
        .btn-close {
            background: #e74c3c;
            color: white;
            border: none;
            padding: 10px 30px;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px;
            float: left;
        }
        .btn-close:hover {
            background: #c0392b;
        }
        .btn-refresh {
            background: #2ecc71;
            color: white;
            border: none;
            padding: 10px 30px;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px;
            float: left;
            margin-left: 10px;
        }
        .btn-refresh:hover {
            background: #27ae60;
        }
        .clearfix {
            clear: both;
        }
        .footer {
            margin-top: 20px;
            padding-top: 15px;
            border-top: 1px solid #ddd;
        }
        .badge {
            display: inline-block;
            background: #e74c3c;
            color: white;
            border-radius: 50%;
            padding: 2px 10px;
            font-size: 11px;
            margin-right: 8px;
        }
        .transfer-row {
            display: flex;
            flex-wrap: wrap;
            align-items: center;
            gap: 10px;
            margin-top: 10px;
        }
        .transfer-row label {
            font-weight: bold;
            min-width: 180px;
        }
        .transfer-row input[type="text"] {
            padding: 6px 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 12px;
        }
        .emp-name-display {
            display: inline-block;
            min-width: 280px;
            padding: 6px 10px;
            background: #eaf2f8;
            border: 1px solid #aed6f1;
            border-radius: 4px;
            color: #1a5276;
            font-weight: bold;
        }
        .btn-transfer {
            background: #f39c12;
            color: white;
            border: none;
            padding: 8px 20px;
            border-radius: 4px;
            cursor: pointer;
            font-size: 13px;
        }
        .btn-transfer:hover {
            background: #d68910;
        }
        .msg {
            margin-top: 10px;
            padding: 8px 12px;
            border-radius: 4px;
            display: none;
        }
        .msg.show {
            display: block;
        }
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
        </script>

        <div class="header">
            <h2>📋 <asp:Label ID="lblPageTitle" runat="server" meta:resourcekey="lblPageTitleResource1" /></h2>
            <div class="emp-info">
                <strong><asp:Label ID="lblEmployeeHeader" runat="server" meta:resourcekey="lblEmployeeHeaderResource1" /></strong>
                <br />
                <strong><asp:Label ID="lblEmployeeCodeCaption" runat="server" meta:resourcekey="lblEmployeeCodeCaptionResource1" /></strong>
                <asp:Label ID="lblEmpCode" runat="server" />
                &nbsp;&nbsp;|&nbsp;&nbsp;
                <strong><asp:Label ID="lblEmployeeNameCaption" runat="server" meta:resourcekey="lblEmployeeNameCaptionResource1" /></strong>
                <asp:Label ID="lblEmpName" runat="server" />
            </div>
        </div>

        <div class="grid-container">
            <div class="grid-title">
                <asp:Label ID="lblTransferTitle" runat="server" meta:resourcekey="lblTransferTitleResource1" />
            </div>
            <div class="transfer-row">
                <asp:CheckBox ID="chkSameEmployee" runat="server" AutoPostBack="True"
                    OnCheckedChanged="chkSameEmployee_CheckedChanged"
                    meta:resourcekey="chkSameEmployeeResource1" />
            </div>
            <div class="transfer-row">
                <asp:Label ID="lblReplacementEmployee" runat="server"
                    AssociatedControlID="txtReplacementEmpCode" meta:resourcekey="lblReplacementEmployeeResource1" />
                <asp:TextBox ID="txtReplacementEmpCode" runat="server"
                    Width="100px" MaxLength="30" AutoPostBack="True"
                    OnTextChanged="txtReplacementEmpCode_TextChanged" />
                <asp:ImageButton ID="btnSearchReplacementEmp" runat="server"
                    ImageUrl="./Img/forum_search.gif" Width="24px" Height="18px"
                    meta:resourcekey="btnSearchReplacementEmpResource1" />
                <asp:Label ID="lblReplacementEmpName" runat="server" CssClass="emp-name-display" />
            </div>
            <div class="transfer-row" id="rowDelegate" runat="server">
                <asp:Label ID="lblDelegateEmployee" runat="server"
                    AssociatedControlID="txtDelegateEmpCode" meta:resourcekey="lblDelegateEmployeeResource1" />
                <asp:TextBox ID="txtDelegateEmpCode" runat="server"
                    Width="100px" MaxLength="30" AutoPostBack="True"
                    OnTextChanged="txtDelegateEmpCode_TextChanged" />
                <asp:ImageButton ID="btnSearchDelegateEmp" runat="server"
                    ImageUrl="./Img/forum_search.gif" Width="24px" Height="18px"
                    meta:resourcekey="btnSearchDelegateEmpResource1" />
                <asp:Label ID="lblDelegateEmpName" runat="server" CssClass="emp-name-display" />
            </div>
            <div class="transfer-row">
                <asp:Button ID="btnTransferApprovals" runat="server"
                    CssClass="btn-transfer" OnClick="btnTransferApprovals_Click"
                    meta:resourcekey="btnTransferApprovalsResource1" />
            </div>
            <asp:Label ID="lblTransferMessage" runat="server" CssClass="msg" />
            <asp:HiddenField ID="hdnSourceEmployeeID" runat="server" />
            <asp:HiddenField ID="hdnReplacementEmployeeID" runat="server" />
            <asp:HiddenField ID="hdnDelegateEmployeeID" runat="server" />
        </div>

        <div class="grid-container">
            <div class="grid-title">
                🔔 <asp:Label ID="lblActionNeededTitle" runat="server" meta:resourcekey="lblActionNeededTitleResource1" />
                <asp:Label ID="lblActionNeededCount" runat="server" CssClass="badge" />
            </div>
            <asp:GridView ID="grdActionNeeded" runat="server"
                AutoGenerateColumns="False"
                Width="100%"
                CssClass="grid-view"
                meta:resourcekey="grdActionNeededResource1">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="#" meta:resourcekey="colRowNumberResource1" />
                    <asp:BoundField DataField="RequestSerial" meta:resourcekey="colRequestSerialResource1" />
                    <asp:BoundField DataField="RequestName" meta:resourcekey="colRequestNameResource1" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="grid-container">
            <div class="grid-title">
                📝 <asp:Label ID="lblSubmittedOpenTitle" runat="server" meta:resourcekey="lblSubmittedOpenTitleResource1" />
                <asp:Label ID="lblSubmittedOpenCount" runat="server" CssClass="badge" />
            </div>
            <asp:GridView ID="grdSubmittedOpen" runat="server"
                AutoGenerateColumns="False"
                Width="100%"
                CssClass="grid-view"
                meta:resourcekey="grdSubmittedOpenResource1">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="#" meta:resourcekey="colRowNumberResource1" />
                    <asp:BoundField DataField="RequestSerial" meta:resourcekey="colRequestSerialResource1" />
                    <asp:BoundField DataField="RequestName" meta:resourcekey="colRequestNameResource1" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="grid-container">
            <div class="grid-title">
                ⚙️ <asp:Label ID="lblConfigurationTitle" runat="server" meta:resourcekey="lblConfigurationTitleResource1" />
                <asp:Label ID="lblConfigurationCount" runat="server" CssClass="badge" />
            </div>
            <asp:GridView ID="grdConfiguration" runat="server"
                AutoGenerateColumns="False"
                Width="100%"
                CssClass="grid-view"
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

        <div class="footer">
            <asp:Button ID="btnClose" runat="server" CssClass="btn-close"
                OnClientClick="window.close(); return false;" meta:resourcekey="btnCloseResource1" />
            <asp:Button ID="btnRefresh" runat="server" CssClass="btn-refresh"
                OnClick="btnRefresh_Click" meta:resourcekey="btnRefreshResource1" />
            <div class="clearfix"></div>
        </div>
    </form>
</body>
</html>
