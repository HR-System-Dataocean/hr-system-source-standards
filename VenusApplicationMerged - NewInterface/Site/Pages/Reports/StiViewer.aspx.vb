Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Script.Serialization
Imports Venus.Application.SystemFiles.System
Imports Stimulsoft.Report
Imports Stimulsoft.Report.Dictionary
Imports Stimulsoft.Report.Components
Imports Stimulsoft.Report.Web
Imports Stimulsoft.Base.Drawing

Partial Class Pages_Reports_StiViewer
    Inherits System.Web.UI.Page

    Private mHasReportError As Boolean = False
    Private mReportErrorText As String = String.Empty
    Private mPreferArabic As Nullable(Of Boolean) = Nothing

    ' Usage examples:
    ' /Pages/Reports/StiViewer.aspx?report=SSReport/Emp.mrt&EmployeesCode=1001&ToDate=14/10/2025
    ' /Pages/Reports/StiViewer.aspx?report=SSReport/Emp.mrt&params={"EmpID":"123","Lang":"ar"}
    ' Report path is resolved to SSReport/AR/ or SSReport/EN/ based on current system language.

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        mPreferArabic = GetArabicViewFromParams()

        Dim rpt As String = Request("report")
        If String.IsNullOrEmpty(rpt) Then
            ShowUiError(GetLocalizedText("لم يتم تحديد اسم التقرير.", "Missing report name."))
            Return
        End If

        rpt = StiReportPathHelper.ResolveReportPath(rpt, Server, mPreferArabic)
        If Not File.Exists(Server.MapPath(rpt)) Then
            Dim fileName As String = Path.GetFileName(Server.MapPath(rpt))
            ShowUiError(GetLocalizedText("الملف غير موجود: " & fileName, "Report file not found: " & fileName))
            Return
        End If
    End Sub

    Protected Sub viewer_GetReport(ByVal sender As Object, ByVal e As StiReportDataEventArgs)
        mPreferArabic = GetArabicViewFromParams()
        If mHasReportError Then
            e.Report = New StiReport()
            Return
        End If

        Dim report As New StiReport()

        ' Resolve report path
        Dim rpt As String = Request("report")
        If String.IsNullOrEmpty(rpt) Then
            ShowUiError(GetLocalizedText("لم يتم تحديد اسم التقرير.", "Missing report name."))
            e.Report = New StiReport()
            Return
        End If
        rpt = StiReportPathHelper.ResolveReportPath(rpt, Server, mPreferArabic)
        Dim reportPhysicalPath As String = Server.MapPath(rpt)
        If Not File.Exists(reportPhysicalPath) Then
            Dim msgAr As String = "الملف غير موجود: " & Path.GetFileName(reportPhysicalPath)
            Dim msgEn As String = "Report file not found: " & Path.GetFileName(reportPhysicalPath)
            ShowUiError(GetLocalizedText(msgAr, msgEn))
            e.Report = New StiReport()
            Return
        End If

        Try
            report.Load(reportPhysicalPath)
        Catch
            ShowUiError(GetLocalizedText("تعذر تحميل ملف التقرير.", "Unable to load report file."))
            e.Report = New StiReport()
            Return
        End Try

        ' Apply DB connection string to any SQL sources
        Dim conn As String = System.Configuration.ConfigurationManager.AppSettings("Connstring")
        For Each db In report.Dictionary.Databases
            Dim sqlDb = TryCast(db, StiSqlDatabase)
            If sqlDb IsNot Nothing Then sqlDb.ConnectionString = conn
        Next

        ' Ensure SQL parameters have a non-zero Size to satisfy SqlCommand.Prepare
        For Each ds In report.Dictionary.DataSources
            Dim sqlSrc = TryCast(ds, StiSqlSource)
            If sqlSrc IsNot Nothing AndAlso sqlSrc.Parameters IsNot Nothing Then
                For Each p As StiDataParameter In sqlSrc.Parameters
                    ' Some builds omit parameter Size; set a safe default when zero or negative
                    If p.Size <= 0 Then p.Size = 4000
                Next
            End If
        Next

        ' Bind variables from querystring keys that match variable names
        For Each v As StiVariable In report.Dictionary.Variables
            Dim raw As String = Request(v.Name)
            If Not String.IsNullOrEmpty(raw) Then
                SetVar(report, v.Name, raw)
            End If
        Next

        EnsurePrintedBy(report)

        ' Optional: JSON payload param= {"Name":"Value",...}
        Dim json As String = Request("params")
        If Not String.IsNullOrEmpty(json) Then
            Try
                Dim dict = New JavaScriptSerializer().Deserialize(Of Dictionary(Of String, Object))(json)
                For Each kv In dict
                    Dim name = kv.Key
                    Dim val = If(kv.Value, String.Empty).ToString()
                    If report.Dictionary.Variables.Contains(name) Then
                        SetVar(report, name, val)
                    End If
                Next
            Catch
                ' ignore json errors
            End Try
        End If

        e.Report = report
    End Sub

    Private Sub EnsurePrintedBy(ByVal report As StiReport)
        Dim printedByValue As String = GetCurrentAppUserDisplayName()
        If String.IsNullOrEmpty(printedByValue) Then Return

        If Not report.Dictionary.Variables.Contains("PrintedBy") Then
            Dim v As New StiVariable()
            v.Name = "PrintedBy"
            v.Type = GetType(String)
            v.Value = printedByValue
            report.Dictionary.Variables.Add(v)
        Else
            SetVar(report, "PrintedBy", printedByValue)
        End If

        For Each p As StiPage In report.Pages
            Dim footer As StiPageFooterBand = Nothing
            For Each c As StiComponent In p.Components
                footer = TryCast(c, StiPageFooterBand)
                If footer IsNot Nothing Then Exit For
            Next
            If footer Is Nothing Then
                footer = New StiPageFooterBand()
                footer.Name = "PageFooterBandPrintedBy"
                footer.Height = 0.2
                p.Components.Add(footer)
            End If

            Dim alreadyExists As Boolean = False
            'For Each c As StiComponent In footer.Components
            '    Dim t = TryCast(c, StiText)
            '    If t IsNot Nothing AndAlso Not String.IsNullOrEmpty(t.Text) AndAlso t.Text.IndexOf("PrintedBy", StringComparison.OrdinalIgnoreCase) >= 0 Then
            '        alreadyExists = True
            '        Exit For
            '    End If
            'Next
            If alreadyExists Then Continue For

            Dim isArabic As Boolean = String.Equals(ProfileCls.CurrentLanguage(), "Ar", StringComparison.OrdinalIgnoreCase)
            Dim prefix As String = If(isArabic, "تمت الطباعة بواسطة: ", "Printed By: ")

            Dim txt As New StiText()
            txt.Name = "PrintedByText"
            txt.Text = prefix & "{PrintedBy}"
            txt.HorAlignment = If(isArabic, StiTextHorAlignment.Right, StiTextHorAlignment.Left)
            txt.VertAlignment = StiVertAlignment.Center
            txt.Font = New Drawing.Font("Arial", 8.0F, Drawing.FontStyle.Regular)
            txt.ClientRectangle = New Stimulsoft.Base.Drawing.RectangleD(0, 0, p.Width, footer.Height)
            footer.Components.Add(txt)
        Next
    End Sub

    Private Function GetCurrentAppUserDisplayName() As String
        Try
            Dim u As New Clssys_Users(New Page)
            If u.Find("ID = " & ProfileCls.RetUserID()) Then
                Dim isArabic As Boolean = String.Equals(ProfileCls.CurrentLanguage(), "Ar", StringComparison.OrdinalIgnoreCase)
                Dim name As String = If(isArabic, u.ArbName, u.EngName)
                If Not String.IsNullOrEmpty(name) Then Return name
                Return u.Code
            End If
        Catch
        End Try
        Return String.Empty
    End Function

    Private Sub SetVar(ByVal report As StiReport, ByVal name As String, ByVal value As String)
        Dim v = report.Dictionary.Variables(name)
        If v Is Nothing Then Return
        ' Try to coerce dates to dd/MM/yyyy
        Dim dt As DateTime
        If DateTime.TryParse(value, dt) Then
            v.Value = dt.ToString("dd/MM/yyyy")
        Else
            v.Value = value
        End If
    End Sub

    Private Function GetLocalizedText(ByVal arText As String, ByVal enText As String) As String
        If String.Equals(ProfileCls.CurrentLanguage(), "Ar", StringComparison.OrdinalIgnoreCase) Then
            Return arText
        End If
        Return enText
    End Function

    Private Sub ShowUiError(ByVal message As String)
        mHasReportError = True
        mReportErrorText = message
        pnlReportError.Visible = True
        lblReportError.Text = Server.HtmlEncode(message)
        viewer.Visible = False
    End Sub

    Private Function GetArabicViewFromParams() As Nullable(Of Boolean)
        Dim json As String = Request("params")
        If String.IsNullOrEmpty(json) Then Return Nothing

        Try
            Dim dict = New JavaScriptSerializer().Deserialize(Of Dictionary(Of String, Object))(json)
            If dict Is Nothing OrElse Not dict.ContainsKey("ArabicView") Then Return Nothing

            Dim raw As String = Convert.ToString(dict("ArabicView"))
            If String.IsNullOrEmpty(raw) Then Return Nothing

            If String.Equals(raw, "true", StringComparison.OrdinalIgnoreCase) OrElse raw = "1" Then
                Return True
            End If
            If String.Equals(raw, "false", StringComparison.OrdinalIgnoreCase) OrElse raw = "0" Then
                Return False
            End If
        Catch
            ' ignore params parsing errors and fallback to system language
        End Try

        Return Nothing
    End Function
End Class
