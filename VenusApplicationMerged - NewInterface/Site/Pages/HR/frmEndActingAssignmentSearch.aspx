<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEndActingAssignmentSearch.aspx.vb"
    Inherits="frmEndActingAssignmentSearch" Culture="auto" UICulture="auto" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            font-family: Tahoma, Arial, sans-serif;
            font-size: 12px;
            margin: 0;
            padding: 10px;
            background: #eef2f7;
        }
        .header {
            background: #2f80c1;
            color: #fff;
            padding: 10px 12px;
            border-radius: 6px 6px 0 0;
            font-weight: bold;
        }
        .toolbar {
            background: #fff;
            border: 1px solid #9ec3e6;
            border-top: none;
            padding: 10px;
            display: flex;
            gap: 8px;
            align-items: center;
            flex-wrap: wrap;
        }
        .toolbar input[type="text"] {
            flex: 1 1 220px;
            min-width: 180px;
            padding: 6px 8px;
            border: 1px solid #c5ced9;
            border-radius: 4px;
        }
        .btn {
            background: #3498db;
            color: #fff;
            border: 1px solid #2176b3;
            padding: 6px 14px;
            border-radius: 4px;
            cursor: pointer;
            font-weight: bold;
        }
        .grid-wrap {
            background: #fff;
            border: 1px solid #9ec3e6;
            border-top: none;
            border-radius: 0 0 6px 6px;
            padding: 8px;
            max-height: 420px;
            overflow: auto;
        }
        .grid {
            width: 100%;
            border-collapse: collapse;
        }
        .grid th {
            background: #eaf4ff;
            color: #1a5276;
            padding: 8px;
            border: 1px solid #b7d4ef;
            text-align: center;
        }
        .grid td {
            padding: 7px 8px;
            border: 1px solid #d5dde8;
            cursor: pointer;
        }
        .grid tr:hover td { background: #f0f7fc; }
        .msg {
            color: #922b21;
            padding: 8px;
        }
    </style>
    <script type="text/javascript">
        function selectRow(sourceId, displayText) {
            if (window.parent && window.parent.CloseIt) {
                window.parent.CloseIt(sourceId + '|' + displayText);
            }
        }
    </script>
</head>
<body id="pageBody" runat="server">
    <form id="form1" runat="server">
        <div class="header">
            <asp:Label ID="lblTitle" runat="server" />
        </div>
        <div class="toolbar">
            <asp:TextBox ID="txtFilter" runat="server" />
            <asp:Button ID="btnFilter" runat="server" CssClass="btn" OnClick="btnFilter_Click" />
        </div>
        <div class="grid-wrap">
            <asp:Label ID="lblMsg" runat="server" CssClass="msg" />
            <asp:GridView ID="grdResults" runat="server" CssClass="grid" AutoGenerateColumns="False"
                ShowHeader="True" GridLines="None" OnRowDataBound="grdResults_RowDataBound"
                DataKeyNames="SourceID,DisplayText">
                <Columns>
                    <asp:BoundField DataField="SourceID" HeaderText="Source ID" />
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                    <asp:BoundField DataField="DisplayText" HeaderText="Display" Visible="False" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
