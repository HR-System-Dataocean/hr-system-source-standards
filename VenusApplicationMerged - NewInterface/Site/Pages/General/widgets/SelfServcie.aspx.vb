Imports System.Data
Imports OfficeWebUI
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class _SelfServcie
    Inherits MainPage
    Public ReadOnly Property MainManager() As Manager
        Get
            Return Me.Manager1
        End Get
    End Property
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim _polic As New PoliciesWs()
        Dim sys As New ClsSys_Forms(Me.Page)
        Dim dt As DataTable = _polic.AuthorizedPages()
        Dim Dr As DataRow() = dt.Select("LinkTarget = '_Blank' and LinkUrl <> ''")
        For i As Integer = 0 To Dr.Length - 1
            sys.Find("ID = " & Dr(i)("ID"))
            Dim Itm As New OfficeWebUI.ListView.ListViewItem()
            If ProfileCls.CurrentLanguage() = "Ar" Then
                Itm.Text = sys.ArbName
            Else
                Itm.Text = sys.EngName
            End If
            Itm.ImageUrl = sys.ImageUrl
            Itm.ClientClick = "Show('" & sys.LinkUrl.Replace("Pages/", "../../") & "'); return false;"
            ListView1.Items.Add(Itm)
        Next
        ListView1.DataBind()
    End Sub
End Class
