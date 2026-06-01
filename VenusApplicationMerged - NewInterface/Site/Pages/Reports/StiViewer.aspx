<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StiViewer.aspx.vb" Inherits="Pages_Reports_StiViewer" %>
<%@ Register Assembly="Stimulsoft.Report.Web" Namespace="Stimulsoft.Report.Web" TagPrefix="StiWeb" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Report Viewer</title>
    <style type="text/css">
        .report-error-box {
            margin: 24px;
            padding: 16px;
            border: 1px solid #f2c7c7;
            background-color: #fff6f6;
            color: #8a1f1f;
            font-family: Arial, sans-serif;
            font-size: 14px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlReportError" runat="server" Visible="False" CssClass="report-error-box">
            <asp:Label ID="lblReportError" runat="server" />
        </asp:Panel>
        <StiWeb:StiWebViewer ID="viewer" runat="server"
            OnGetReport="viewer_GetReport"
            ShowPrintButton="True"
            ShowExportToPdf="True"
            ShowExportToExcel="True"
            ShowExportToWord="True"
            ToolbarVisible="True" />
    </form>
</body>
</html>
