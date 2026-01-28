Imports System
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports Venus.Application.SystemFiles.System

<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
Public Class ReportsWs
    Inherits System.Web.Services.WebService

    <WebMethod(EnableSession:=True)> _
    Public Function GetReportType(ByVal Code As String) As String
        Try
            If String.IsNullOrEmpty(Code) Then Return ""
            Dim repo As New Clssys_Reports(New System.Web.UI.Page)
            If repo.Find("Code = '" & Code.Replace("'", "''") & "'") Then
                Dim dt As DataTable = repo.DataSet.Tables(0)
                If dt.Rows.Count > 0 AndAlso dt.Columns.Contains("ReportType") Then
                    If Not IsDBNull(dt.Rows(0)("ReportType")) Then
                        Return Convert.ToString(dt.Rows(0)("ReportType")).Trim()
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
        Return ""
    End Function
End Class
