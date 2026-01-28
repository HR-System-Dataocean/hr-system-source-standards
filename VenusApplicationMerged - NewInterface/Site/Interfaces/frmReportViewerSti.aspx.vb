Imports System.Xml
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.IO
Imports C1.Web.Wijmo.Controls.C1ReportViewer

Partial Class Interfaces_ReportViewerSti
    Inherits System.Web.UI.Page
    Protected Overrides Sub InitializeCulture()
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        Dim _culture As String = StrLanguage
        'Use this
        If (_culture <> "Auto") Then
            Dim ci As New System.Globalization.CultureInfo(_culture)
            Dim StrDateFormat As String = System.Configuration.ConfigurationManager.AppSettings("DATEFORMAT")
            Dim myDateTimePatterns() As String = {StrDateFormat, StrDateFormat}
            Dim DateTimeFormat As New System.Globalization.DateTimeFormatInfo
            DateTimeFormat = ci.DateTimeFormat
            DateTimeFormat.SetAllDateTimePatterns(myDateTimePatterns, "d"c)
            System.Threading.Thread.CurrentThread.CurrentCulture = ci
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci
        End If
        MyBase.InitializeCulture()
    End Sub
    Protected Sub Page_Init1(sender As Object, e As System.EventArgs) Handles Me.Init
        Dim strfilename As String = DateTime.Now.Ticks
        HiddenField1.Value = IIf(Request.QueryString.Item("Language") = 0, "0", "1")
        Dim XDocument As New System.Xml.XmlDocument
        Dim DsReprotContent As New Data.DataSet
        Dim ObjReportCriteria As New Data.DataTable
        DsReprotContent = CType(Session("SessionReportProperties"), Data.DataSet)
        ObjReportCriteria = CType(Session("SessionCriteria"), Data.DataTable)
        Dim ClsReportProperty As New ClsRpw_ReportsProperties(Page)
        Dim ClsReportsDriver As New ReportViewerSti(HiddenField1.Value)
        ClsReportProperty.SetReportsProperties(DsReprotContent)
        Dim ClsUsers As New Clssys_Users(Page)
        Dim ClsCompanies As New Clssys_Companies(Page)
        ClsUsers.Find("ID=" & ClsReportProperty.DataBaseUserRelatedID)
        ClsCompanies.Find("ID=" & ClsReportProperty.MainCompanyID)
        ClsReportsDriver.UserCode = ClsUsers.Code
        ClsReportsDriver.UserName = IIf(HiddenField1.Value = 0, ClsUsers.EngName, ClsUsers.ArbName)
        ClsReportsDriver.CompanyName = IIf(HiddenField1.Value = 0, ClsCompanies.EngName, ClsCompanies.ArbName)
        ClsReportsDriver.CompanyHeader_1 = ConfigurationManager.AppSettings.Item("CompanyHeader_1")
        ClsReportsDriver.CompanyHeader_2 = ConfigurationManager.AppSettings.Item("CompanyHeader_1")
        ClsReportsDriver.CompanyHeader_3 = ConfigurationManager.AppSettings.Item("CompanyHeader_1")



        ClsReportsDriver.CompanyID = ProfileCls.RetCompany.ID
        Dim IntIncomingLanguage As Integer = IIf(HiddenField1.Value = 0 = "1", 1, 0)
        Dim ClsReports As New Clssys_Reports(Page)
        ClsReports.Find("Code='" & ClsReportProperty.Code & "'")
        If ClsReports.XMLReport = String.Empty Then
            XDocument = ClsReportsDriver.ViewReportDetails(Server.MapPath("ReportTempletes_Folder\" & ClsReportProperty.Code & ".xml"), ClsReportProperty, ObjReportCriteria)
        Else
            XDocument = ClsReportsDriver.ViewReportDetailsxml(ClsReports.XMLReport, ClsReportProperty, ObjReportCriteria, IntIncomingLanguage)
        End If
        If XDocument Is Nothing Then
            Return
        End If
        XDocument.Save(Server.MapPath("~/C1ReportXML/" & strfilename & ".xml"))
        C1ReportViewer1.FileName = "~/C1ReportXML/" & strfilename & ".xml"
    End Sub
End Class





