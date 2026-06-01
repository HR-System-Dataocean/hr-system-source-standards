Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Web
Imports Resources

Public Class StiReportPathHelper
    Public Shared Function GetLanguageFolder(Optional ByVal preferArabic As Nullable(Of Boolean) = Nothing) As String
        If preferArabic.HasValue Then
            Return If(preferArabic.Value, "AR", "EN")
        End If
        Return If(IsArabicLanguage(), "AR", "EN")
    End Function

    Private Shared Function IsArabicLanguage() As Boolean
        Dim lang As String = Convert.ToString(HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey))
        If Not String.IsNullOrEmpty(lang) Then
            Return String.Equals(lang, "Ar", StringComparison.OrdinalIgnoreCase)
        End If

        Dim loginCookies As HttpCookie = HttpContext.Current.Request.Cookies("LoginCookies")
        If loginCookies IsNot Nothing Then
            lang = loginCookies.Values(MetaData.SessionCurrentLanguageKey)
            If Not String.IsNullOrEmpty(lang) Then
                Return String.Equals(lang, "Ar", StringComparison.OrdinalIgnoreCase)
            End If
        End If

        Dim langCookie As HttpCookie = HttpContext.Current.Request.Cookies("Lang")
        If langCookie IsNot Nothing AndAlso Not String.IsNullOrEmpty(langCookie.Value) Then
            Return Not String.Equals(langCookie.Value, "en-US", StringComparison.OrdinalIgnoreCase)
        End If

        Return False
    End Function

    Public Shared Function ResolveReportPath(ByVal relativePath As String, ByVal server As HttpServerUtility, Optional ByVal preferArabic As Nullable(Of Boolean) = Nothing) As String
        If String.IsNullOrEmpty(relativePath) Then Return relativePath

        Dim path As String = relativePath.Replace("\"c, "/"c).Trim("/"c)
        If path.StartsWith("~/") Then path = path.Substring(2)

        If path.StartsWith("SSReport/AR/", StringComparison.OrdinalIgnoreCase) OrElse
           path.StartsWith("SSReport/EN/", StringComparison.OrdinalIgnoreCase) Then
            Return "~/" & path
        End If

        If Not path.StartsWith("SSReport/", StringComparison.OrdinalIgnoreCase) Then
            Return If(relativePath.StartsWith("~/"), relativePath, "~/" & relativePath)
        End If

        Dim fileName As String = path.Substring("SSReport/".Length)
        Dim langFolder As String = GetLanguageFolder(preferArabic)
        Dim langPath As String = "SSReport/" & langFolder & "/" & fileName

        If File.Exists(server.MapPath("~/" & langPath)) Then
            Return "~/" & langPath
        End If

        'Dim otherFolder As String = If(langFolder = "AR", "EN", "AR")
        'Dim otherPath As String = "SSReport/" & otherFolder & "/" & fileName
        'If File.Exists(server.MapPath("~/" & otherPath)) Then
        '    Return "~/" & otherPath
        'End If

        If File.Exists(server.MapPath("~/" & path)) Then
            Return "~/" & path
        End If

        Return "~/" & langPath
    End Function
End Class
