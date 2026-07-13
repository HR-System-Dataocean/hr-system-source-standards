Imports System.Data
Imports System.Data.SqlClient
Imports Venus.Application.SystemFiles.HumanResource

Partial Class frmSelfServiceRequestsPopup
    Inherits System.Web.UI.Page

    Private ClsEmployees As Clshrs_Employees

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ClsEmployees = New Clshrs_Employees(Page)

            If Not IsPostBack Then
                Dim EmployeeID As Integer = 0
                If Request.QueryString("EmployeeID") IsNot Nothing Then
                    Integer.TryParse(Request.QueryString("EmployeeID"), EmployeeID)
                End If

                If EmployeeID > 0 Then
                    ' عرض بيانات الموظف
                    If ClsEmployees.Find("ID=" & EmployeeID) Then
                        lblEmpCode.Text = ClsEmployees.Code
                        lblEmpName.Text = ClsEmployees.FullName
                    End If

                    ' تحميل البيانات
                    LoadData(EmployeeID)
                Else
                    ' لو مفيش EmployeeID، نغلق الـ Popup
                    ClientScript.RegisterStartupScript(Me.GetType(), "ClosePopup",
                        "<script language='javascript'>window.close();</script>", False)
                End If
            End If

        Catch ex As Exception
            Response.Write("<div style='color:red;padding:20px;'>حدث خطأ: " & ex.Message & "</div>")
        End Try
    End Sub

    Private Sub LoadData(ByVal EmployeeID As Integer)
        Try
            Dim connStr As String = ClsEmployees.ConnectionString

            ' استعلام 1: الطلبات المحتاجة أكشن من الموظف
            Dim sqlActionNeeded As String = "SELECT " &
                "ROW_NUMBER() OVER (ORDER BY SS_RequestActions.ID) AS RowNumber, " &
                "SS_RequestActions.RequestSerial, " &
                "SS_RequestTypes.RequestArbName, " &
                "SS_RequestTypes.RequestEngName " &
                "FROM SS_RequestActions " &
                "JOIN SS_RequestTypes ON SS_RequestActions.FormCode = SS_RequestTypes.RequestCode " &
                "WHERE ActionID IS NULL " &
                "AND SS_EmployeeID = @EmployeeID " &
                "AND IsHidden IS NULL"

            ' استعلام 2: الطلبات المقدمة من الموظف واللسه مفتوحة
            Dim sqlSubmittedOpen As String = "SELECT " &
                "ROW_NUMBER() OVER (ORDER BY SS_RequestActions.ID) AS RowNumber, " &
                "SS_RequestActions.RequestSerial, " &
                "SS_RequestTypes.RequestArbName, " &
                "SS_RequestTypes.RequestEngName " &
                "FROM SS_RequestActions " &
                "JOIN SS_RequestTypes ON SS_RequestActions.FormCode = SS_RequestTypes.RequestCode " &
                "WHERE ActionID IS NULL " &
                "AND EmployeeID = @EmployeeID " &
                "AND IsHidden IS NULL"

            Using conn As New SqlConnection(connStr)
                conn.Open()

                ' تحميل Grid 1
                Using cmd As New SqlCommand(sqlActionNeeded, conn)
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
                    Using da As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        grdActionNeeded.DataSource = dt
                        grdActionNeeded.DataBind()
                        lblActionNeededCount.Text = dt.Rows.Count.ToString()
                    End Using
                End Using

                ' تحميل Grid 2
                Using cmd As New SqlCommand(sqlSubmittedOpen, conn)
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
                    Using da As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        grdSubmittedOpen.DataSource = dt
                        grdSubmittedOpen.DataBind()
                        lblSubmittedOpenCount.Text = dt.Rows.Count.ToString()
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Response.Write("<div style='color:red;padding:20px;'>حدث خطأ: " & ex.Message & "</div>")
        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim EmployeeID As Integer = 0
            If Request.QueryString("EmployeeID") IsNot Nothing Then
                Integer.TryParse(Request.QueryString("EmployeeID"), EmployeeID)
            End If

            If EmployeeID > 0 Then
                LoadData(EmployeeID)
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class