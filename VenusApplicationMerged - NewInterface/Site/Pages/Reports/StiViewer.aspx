<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StiViewer.aspx.vb" Inherits="Pages_Reports_StiViewer" %>
<%@ Register Assembly="Stimulsoft.Report.Web" Namespace="Stimulsoft.Report.Web" TagPrefix="StiWeb" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Report Viewer</title>
</head>
<body>
    <form id="form1" runat="server">
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
