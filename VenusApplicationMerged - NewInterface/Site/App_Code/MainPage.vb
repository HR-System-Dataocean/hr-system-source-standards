Imports System.Threading
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Public Class MainPage
    Inherits System.Web.UI.Page
    Protected Overrides Sub InitializeCulture()
        Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo(ProfileCls.CurrentLanguage)
        MyBase.InitializeCulture()
    End Sub
    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        'Try
        '    If Request.AppRelativeCurrentExecutionFilePath.Contains("Login.aspx") Or Request.AppRelativeCurrentExecutionFilePath.Contains("NewDefault.aspx") Or Request.AppRelativeCurrentExecutionFilePath.Contains("ChangePassword.aspx") Or Request.AppRelativeCurrentExecutionFilePath.Contains("default.aspx") Or Request.AppRelativeCurrentExecutionFilePath.Contains("Default.aspx") Or Request.AppRelativeCurrentExecutionFilePath.Contains("frmModalSearchScreen") Or Request.AppRelativeCurrentExecutionFilePath.Contains("widgets") Then
        '    Else
        '        Dim pagename As String = System.IO.Path.GetFileName(Request.AppRelativeCurrentExecutionFilePath)
        '        Dim pc As New PoliciesWs
        '        If pc.AuthorizedPagesOpen(pagename) = False Then
        '            Response.Redirect(FormsAuthentication.LoginUrl)
        '        End If
        '    End If
        'Catch ex As Exception
        '    If Request.AppRelativeCurrentExecutionFilePath.Contains("Login.aspx") Or Request.AppRelativeCurrentExecutionFilePath.Contains("NewDefault.aspx") Or Request.AppRelativeCurrentExecutionFilePath.Contains("ChangePassword.aspx") Or Request.AppRelativeCurrentExecutionFilePath.Contains("default.aspx") Or Request.AppRelativeCurrentExecutionFilePath.Contains("Default.aspx") Or Request.AppRelativeCurrentExecutionFilePath.Contains("frmModalSearchScreen") Or Request.AppRelativeCurrentExecutionFilePath.Contains("widgets") Then
        '    Else
        '        Response.Redirect(FormsAuthentication.LoginUrl)
        '    End If
        'End Try
    End Sub
    Private Sub Page_PreInit1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
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
        Page.Theme = ProfileCls.CurrentTheme()
    End Sub
End Class
