Imports System.Data
Imports System.Data.SqlClient
Imports Venus.Application.SystemFiles.HumanResource

Partial Class frmEndActingAssignmentSearch
    Inherits MainPage

    Private ClsEmployees As Clshrs_Employees

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ClsEmployees = New Clshrs_Employees(Page)
            ApplyLayoutDirection()
            ApplyCaptions()
            If Not IsPostBack Then
                BindResults("")
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub ApplyLayoutDirection()
        Dim isArabic As Boolean = String.Equals(ProfileCls.CurrentLanguage, "Ar", StringComparison.OrdinalIgnoreCase)
        pageBody.Attributes("dir") = If(isArabic, "rtl", "ltr")
    End Sub

    Private Sub ApplyCaptions()
        Dim isArabic As Boolean = String.Equals(ProfileCls.CurrentLanguage, "Ar", StringComparison.OrdinalIgnoreCase)
        If isArabic Then
            lblTitle.Text = "اختيار التكليف النشط"
            btnFilter.Text = "بحث"
            txtFilter.Attributes("placeholder") = "ابحث برقم المصدر أو الملاحظات"
            grdResults.Columns(0).HeaderText = "رقم المصدر"
            grdResults.Columns(1).HeaderText = "الملاحظات"
        Else
            lblTitle.Text = "Select Active Acting Assignment"
            btnFilter.Text = "Search"
            txtFilter.Attributes("placeholder") = "Search by Source ID or Remarks"
            grdResults.Columns(0).HeaderText = "Source ID"
            grdResults.Columns(1).HeaderText = "Remarks"
        End If
    End Sub

    Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        BindResults(txtFilter.Text.Trim())
    End Sub

    Private Sub BindResults(ByVal filter As String)
        lblMsg.Text = ""
        Dim sql As String =
            "SELECT DISTINCT SourceID, ISNULL(Remarks,'') AS Remarks," &
            " CAST(SourceID AS varchar(30)) + CASE WHEN ISNULL(Remarks,'')='' THEN '' ELSE ' - ' + Remarks END AS DisplayText" &
            " FROM (" &
            "   SELECT SourceID, Remarks FROM hrs_ActingEmployeeAssignments" &
            "   WHERE CancelDate IS NULL AND SourceID IS NOT NULL" &
            "   UNION" &
            "   SELECT SourceID, Remarks FROM hrs_ActingPositionAssignments" &
            "   WHERE CancelDate IS NULL AND SourceID IS NOT NULL" &
            " ) X" &
            " WHERE 1=1"

        If filter <> "" Then
            sql &= " AND (CAST(SourceID AS varchar(30)) LIKE @Filter OR ISNULL(Remarks,'') LIKE @Filter)"
        End If
        sql &= " ORDER BY SourceID DESC"

        Dim dt As New DataTable()
        Using conn As New SqlConnection(ClsEmployees.ConnectionString)
            Using cmd As New SqlCommand(sql, conn)
                If filter <> "" Then
                    cmd.Parameters.AddWithValue("@Filter", "%" & filter & "%")
                End If
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        grdResults.DataSource = dt
        grdResults.DataBind()

        If dt.Rows.Count = 0 Then
            lblMsg.Text = If(String.Equals(ProfileCls.CurrentLanguage, "Ar", StringComparison.OrdinalIgnoreCase),
                             "لا توجد سجلات نشطة.", "No active records found.")
        End If
    End Sub

    Protected Sub grdResults_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType <> DataControlRowType.DataRow Then Return

        Dim sourceId As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SourceID"))
        Dim displayText As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DisplayText"))
        e.Row.Attributes("onclick") =
            "selectRow('" & sourceId.Replace("'", "\'") & "','" & displayText.Replace("'", "\'").Replace(vbCr, " ").Replace(vbLf, " ") & "');"
        e.Row.Attributes("title") = If(String.Equals(ProfileCls.CurrentLanguage, "Ar", StringComparison.OrdinalIgnoreCase),
                                       "انقر للاختيار", "Click to select")
    End Sub

End Class
