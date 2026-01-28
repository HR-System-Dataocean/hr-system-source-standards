<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmReportViewer.aspx.vb"
    Inherits="Interfaces_ReportViewer" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="C1.Web.Wijmo.Controls.3" Namespace="C1.Web.Wijmo.Controls.C1ReportViewer"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body dir="ltr">
    <form id="form1" runat="server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <cc1:C1ReportViewer ID="C1ReportViewer1" runat="server" Width="100%" Height="100%"
        AvailableTools="Search, Thumbs" CollapseToolsPanel="True" FullScreen="True" PagedView="False"
        ExpandedTool="Search" Culture="ar-SA" Zoom="100%"  />
    </form>
</body>
</html>
