<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSelfServiceRequestsPopup.aspx.vb" Inherits="frmSelfServiceRequestsPopup" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>طلبات الخدمة الذاتية</title>
    <style type="text/css">
        body {
            font-family: Tahoma, Arial, sans-serif;
            font-size: 12px;
            direction: rtl;
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
            text-align: right;
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
        .no-data {
            padding: 20px;
            text-align: center;
            color: #888;
            font-size: 14px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
        <!-- Header -->
        <div class="header">
            <h2>📋 طلبات الخدمة الذاتية</h2>
            <div class="emp-info">
                <strong>كود الموظف:</strong> <asp:Label ID="lblEmpCode" runat="server" />
                &nbsp;&nbsp;|&nbsp;&nbsp;
                <strong>اسم الموظف:</strong> <asp:Label ID="lblEmpName" runat="server" />
            </div>
        </div>

        <!-- Grid 1: الطلبات المحتاجة أكشن -->
        <div class="grid-container">
            <div class="grid-title">
                🔔 الطلبات المحتاجة أكشن من الموظف
                <asp:Label ID="lblActionNeededCount" runat="server" CssClass="badge" />
            </div>
            <asp:GridView ID="grdActionNeeded" runat="server" 
                AutoGenerateColumns="False" 
                Width="100%" 
                CssClass="grid-view"
                EmptyDataText="لا توجد طلبات محتاجة أكشن">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="#" />
                    <asp:BoundField DataField="RequestSerial" HeaderText="رقم الطلب" />
                    <asp:BoundField DataField="RequestArbName" HeaderText="نوع الطلب (عربي)" />
                    <asp:BoundField DataField="RequestEngName" HeaderText="نوع الطلب (إنجليزي)" />
                </Columns>
            </asp:GridView>
        </div>

        <!-- Grid 2: الطلبات المقدمة واللسه مفتوحة -->
        <div class="grid-container">
            <div class="grid-title">
                📝 الطلبات المقدمة من الموظف والتي لا زالت مفتوحة
                <asp:Label ID="lblSubmittedOpenCount" runat="server" CssClass="badge" />
            </div>
            <asp:GridView ID="grdSubmittedOpen" runat="server" 
                AutoGenerateColumns="False" 
                Width="100%" 
                CssClass="grid-view"
                EmptyDataText="لا توجد طلبات مفتوحة">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="#" />
                    <asp:BoundField DataField="RequestSerial" HeaderText="رقم الطلب" />
                    <asp:BoundField DataField="RequestArbName" HeaderText="نوع الطلب (عربي)" />
                    <asp:BoundField DataField="RequestEngName" HeaderText="نوع الطلب (إنجليزي)" />
                </Columns>
            </asp:GridView>
        </div>

        <!-- Footer -->
        <div class="footer">
            <asp:Button ID="btnClose" runat="server" Text="إغلاق" CssClass="btn-close" OnClientClick="window.close(); return false;" />
            <asp:Button ID="btnRefresh" runat="server" Text="تحديث" CssClass="btn-refresh" OnClick="btnRefresh_Click" />
            <div class="clearfix"></div>
        </div>

    </form>
</body>
</html>