Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.IO
Imports System.Data.OleDb


Partial Class Interfaces_frmAttendanceLoad
    Inherits System.Web.UI.Page

#Region "Public Decleration"
    Dim mErrorHandler As Venus.Shared.ErrorsHandler
    Dim clsMainOtherFields As clsSys_MainOtherFields

    Dim ClsEmployee As Clshrs_Employees
  
#End Region

#Region "Protected Sub"

    Protected Overrides Sub InitializeCulture()
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        Dim _culture As String = StrLanguage
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
     
    End Sub

#End Region


    Protected Sub Btn_Click(sender As Object, e As EventArgs) Handles Btn.Click
        Try
            Dim clsDAL As New ClsDataAcessLayer(Page)
            Dim objNav As New Venus.Shared.Web.NavigationHandler(clsDAL.ConnectionString)

            ClsEmployee = New Clshrs_Employees(Page)
            ClsEmployee.Find("ssnno='" & txtiqama.Text.Trim() & "'")
            If ClsEmployee.ID > 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, " ID NO Used Before Employee No. /—ﬁ„ «·ÂÊÌ… „” Œœ„ ”«»ﬁ« ··„ÊŸ› —ﬁ„") & ClsEmployee.Code)
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, " ID NO not exist /—ﬁ„ «·ÂÊÌ… €Ì— „”Ã· "))
            End If

        Catch ex As Exception
            lblProgress.Text = "Loading..."
        End Try

    End Sub
End Class
