Imports Venus.Application.SystemFiles.System

Partial Class Interfaces_frmModalSearchScreen
    Inherits MainPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim IntSearchID As String = Request.QueryString.Item("SearchID")
        Dim StrTargetControl As String = Request.QueryString.Item("TargetControl")
        Dim ClsDataAccessLayer As New ClsDataAcessLayer(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDataAccessLayer.ConnectionString)
        Dim ObjDs As New Data.DataSet
        Dim ObjSearch As New ClsSys_Search_Main(Page)
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty

        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        TargetControl.Text = StrTargetControl
        lblMainHeader.Text = ObjSearch.ReadSearchName(IntSearchID, StrLanguage)
        ObjSearch.ReadModalSearchParameter(IntSearchID, pnlCriterias, Me.name, Me.realname, Nothing, StrLanguage)
        txtSearchAll.Focus()
        SetData()
    End Sub
    Public Function SetData() As Boolean
        Dim ObjSearch As New ClsSys_Search_Main(Page)
        Dim ObjDs As New Data.DataSet
        Dim IntSearchID As String = Request.QueryString.Item("SearchID")
        Dim StrTargetControl As String = Request.QueryString.Item("TargetControl")
        Dim Cond As String = IIf(Request.QueryString.Item("Cond") = Nothing, "", Request.QueryString.Item("Cond"))

        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        Try
            WebHandler.GetCookies(Page, "Lang", StrLanguage)
            If Me.value.Text = "" Then
            ElseIf Me.value.Text = "0" Then
                If ObjSearch.ViewData(IntSearchID, PrepareSearchCriteria(""), ObjDs, StrLanguage, Cond, chkShowCancel.Checked) Then

                    Me.GridView1.DataSource = ObjDs.Tables(0).DefaultView
                    Me.GridView1.DataBind()

                    txtRankofCodeCell.Value = ""
                End If
            Else
                If ObjSearch.ViewData(IntSearchID, PrepareSearchCriteria(Me.value.Text), ObjDs, StrLanguage, Cond, chkShowCancel.Checked) Then
                    Me.GridView1.DataSource = ObjDs.Tables(0).DefaultView
                    Me.GridView1.DataBind()

                    txtRankofCodeCell.Value = ""
                End If
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function PrepareSearchCriteria(ByVal StrCriteria As String) As String
        Try
            Dim StrPrepared As String = String.Empty
            Dim ObjStringHandler As New Venus.Shared.StringHandler()
            Dim StrFinalStr As String = String.Empty
            Dim Separator() As Char = {"$", "@"}
            Dim StrFields As String() = StrCriteria.Replace(" And ", "$").Replace(" or  ", "@").Split(Separator)
            Dim StrFieldName As String = String.Empty
            For Each StrField As String In StrFields
                If StrField <> "" Then
                    StrPrepared = StrField
                    If StrPrepared.Contains("4S") Then
                        StrFieldName = StrField.Replace("like", "!").Split("!")(0).Trim()
                        StrPrepared = StrPrepared.Substring(StrPrepared.IndexOf("like", 0) + 4).Trim()
                        StrPrepared = StrPrepared.Replace("'%", "").Replace("%'", "")
                        StrPrepared = ObjStringHandler.ReplaceHamza(StrPrepared)
                        StrFinalStr &= StrFieldName & " like '%" & StrPrepared & IIf(Len(txtSearchAll.Text), "%' or  ", "%' And ")
                    Else
                        StrFinalStr &= StrField & IIf(Len(txtSearchAll.Text), " or  ", " And ")
                    End If
                End If
            Next
            If StrFinalStr.EndsWith(" And ") Then
                StrFinalStr = StrFinalStr.Remove(StrFinalStr.LastIndexOf(" And "), 4)
            ElseIf StrFinalStr.EndsWith(" or  ") Then
                StrFinalStr = StrFinalStr.Remove(StrFinalStr.LastIndexOf(" or  "), 4)
            End If
            Return StrFinalStr
        Catch ex As Exception
            Return StrCriteria
        End Try
    End Function

    Protected Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.Header
                    Exit Select
                Case DataControlRowType.DataRow
                    e.Row.Attributes.Add("onmouseover", "setMouseOverColor(this);")
                    e.Row.Attributes.Add("onmouseout", "setMouseOutColor(this);")
                    e.Row.Attributes.Add("ondblclick", "setGridValue('" + e.Row.Cells(0).Text + "');")
                    Exit Select
            End Select
        Catch
        End Try
    End Sub
End Class
