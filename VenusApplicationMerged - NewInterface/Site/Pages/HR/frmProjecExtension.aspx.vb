Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmProjecExtension
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                If clsProjects.Find("ID = " & Request.QueryString.Item("ProjID")) Then
                    Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsProjects.ConnectionString)
                    lblProjectCode.Text = clsProjects.Code
                    lblProjectName.Text = IIf(ClsNavHandler.SetLanguage(Me, "0/1") = 0, clsProjects.EngName, clsProjects.ArbName)
                    txtToDate.Value = clsProjects.EndDate.ToString("ddMMyyyy")
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub LinkButton2_Click(sender As Object, e As System.EventArgs) Handles LinkButton2.Click
        Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        If clsProjects.Find("ID = " & Request.QueryString.Item("ProjID")) Then
            clsProjects.EndDate = SetDate(txtToDate.Text, txtToDate.Text)
            clsProjects.Update("ID = " & Request.QueryString.Item("ProjID"))

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", "CloseMe()", True)
        End If
    End Sub
    Private Function SetDate(gData As Object, hDate As Object) As Date
        Try

            If gData <> "  /  /    " Then
                If ClsDataAcessLayer.IsGreg(gData) Then
                    Return ClsDataAcessLayer.FormatGreg(gData, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.HijriToGreg(gData, "dd/MM/yyyy")
                End If
            ElseIf hDate <> "  /  /    " Then
                If ClsDataAcessLayer.IsHijri(hDate) Then
                    Return ClsDataAcessLayer.HijriToGreg(hDate, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.FormatGreg(hDate, "dd/MM/yyyy")
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
