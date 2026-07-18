Imports System.Data
Imports System.Data.SqlClient
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class frmSelfServiceRequestsPopup
    Inherits MainPage

    Private ClsEmployees As Clshrs_Employees

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ClsEmployees = New Clshrs_Employees(Page)

            ApplyLayoutDirection()
            SetupConfirmScript()
            SetupEmployeeSearchButton()
            ApplySameEmployeeState()

            If Not IsPostBack Then
                Dim EmployeeID As Integer = 0
                If Request.QueryString("EmployeeID") IsNot Nothing Then
                    Integer.TryParse(Request.QueryString("EmployeeID"), EmployeeID)
                End If

                If EmployeeID > 0 Then
                    hdnSourceEmployeeID.Value = EmployeeID.ToString()

                    If ClsEmployees.Find("ID=" & EmployeeID) Then
                        lblEmpCode.Text = ClsEmployees.Code
                        lblEmpName.Text = ClsEmployees.FullName
                    End If

                    LoadData(EmployeeID)
                Else
                    ClientScript.RegisterStartupScript(Me.GetType(), "ClosePopup",
                        "<script language='javascript'>window.close();</script>", False)
                End If
            End If

        Catch ex As Exception
            Response.Write("<div style='color:red;padding:20px;'>" & GetRes("MsgErrorPrefix") & ex.Message & "</div>")
        End Try
    End Sub

    Private Function GetRes(ByVal key As String) As String
        Dim value As Object = GetLocalResourceObject(key)
        If value Is Nothing Then Return key
        Return value.ToString()
    End Function

    Private Sub ApplyLayoutDirection()
        Dim isArabic As Boolean = IsArabicLanguage()
        Dim direction As String = If(isArabic, "rtl", "ltr")
        Dim alignment As String = If(isArabic, "right", "left")
        Dim buttonFloat As String = If(isArabic, "left", "right")

        pageBody.Attributes("dir") = direction
        pageBody.Style("text-align") = alignment
        btnClose.Style("float") = buttonFloat
        btnRefresh.Style("float") = buttonFloat
    End Sub

    Private Sub SetupConfirmScript()
        Dim confirmText As String = GetRes("MsgConfirmHandover").Replace("'", "\'")
        btnTransferApprovals.OnClientClick = "return confirm('" & confirmText & "');"
    End Sub

    Private Function IsArabicLanguage() As Boolean
        Return String.Equals(ProfileCls.CurrentLanguage, "Ar", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Function GetRequestNameSql(ByVal tableAlias As String) As String
        If IsArabicLanguage() Then
            Return "ISNULL(" & tableAlias & ".RequestArbName, " & tableAlias & ".RequestEngName) AS RequestName"
        End If
        Return "ISNULL(" & tableAlias & ".RequestEngName, " & tableAlias & ".RequestArbName) AS RequestName"
    End Function

    Private Function GetConfigurationRequestNameSql() As String
        If IsArabicLanguage() Then
            Return "ISNULL(SS_RequestTypes.RequestArbName, ISNULL(SS_RequestTypes.RequestEngName, SS_Configuration.FormCode)) AS RequestName"
        End If
        Return "ISNULL(SS_RequestTypes.RequestEngName, ISNULL(SS_RequestTypes.RequestArbName, SS_Configuration.FormCode)) AS RequestName"
    End Function

    Private Sub SetupEmployeeSearchButton()
        Try
            Dim ClsObjects As New Clssys_Objects(Page)
            Dim ClsSearchs As New Clssys_Searchs(Page)
            ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim() & "'")
            ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
            Dim csSearchID As Integer = ClsSearchs.ID

            Page.Session("ConnectionString") = ClsEmployees.ConnectionString

            Dim UrlString As String = "'frmModalSearchScreen.aspx?TargetControl=" & txtReplacementEmpCode.ID &
                "&SearchID=" & csSearchID & "&',510,720,false,'" & txtReplacementEmpCode.ClientID & "'"
            btnSearchReplacementEmp.OnClientClick = "OpenModal1(" & UrlString & "); return false;"

            Dim UrlStringDelegate As String = "'frmModalSearchScreen.aspx?TargetControl=" & txtDelegateEmpCode.ID &
                "&SearchID=" & csSearchID & "&',510,720,false,'" & txtDelegateEmpCode.ClientID & "'"
            btnSearchDelegateEmp.OnClientClick = "OpenModal1(" & UrlStringDelegate & "); return false;"
        Catch ex As Exception
            btnSearchReplacementEmp.Visible = False
            btnSearchDelegateEmp.Visible = False
        End Try
    End Sub

    Private Sub LoadData(ByVal EmployeeID As Integer)
        Try
            Dim connStr As String = ClsEmployees.ConnectionString
            Dim requestNameSql As String = GetRequestNameSql("SS_RequestTypes")

            Dim sqlActionNeeded As String = "SELECT " &
                "ROW_NUMBER() OVER (ORDER BY SS_RequestActions.ActionSerial) AS RowNumber, " &
                "SS_RequestActions.RequestSerial, " &
                requestNameSql & " " &
                "FROM SS_RequestActions " &
                "JOIN SS_RequestTypes ON SS_RequestActions.FormCode = SS_RequestTypes.RequestCode " &
                "WHERE ActionID IS NULL " &
                "AND SS_EmployeeID = @EmployeeID " &
                "AND IsHidden IS NULL"

            Dim sqlSubmittedOpen As String = "SELECT " &
                "ROW_NUMBER() OVER (ORDER BY SS_RequestActions.ActionSerial) AS RowNumber, " &
                "SS_RequestActions.RequestSerial, " &
                requestNameSql & " " &
                "FROM SS_RequestActions " &
                "JOIN SS_RequestTypes ON SS_RequestActions.FormCode = SS_RequestTypes.RequestCode " &
                "WHERE ActionID IS NULL " &
                "AND EmployeeID = @EmployeeID " &
                "AND IsHidden IS NULL"

            Dim sqlConfiguration As String = "SELECT " &
                "ROW_NUMBER() OVER (ORDER BY SS_Configuration.ID) AS RowNumber, " &
                "SS_Configuration.FormCode, " &
                GetConfigurationRequestNameSql() & ", " &
                "SS_Configuration.Rank, " &
                "CASE " &
                "  WHEN ISNULL(SS_Configuration.EmployeeID, 0) = @EmployeeID THEN N'Employee' " &
                "  WHEN SS_Configuration.UserTypeID = 1 THEN N'DirectManager' " &
                "  ELSE N'Position' " &
                "END AS MatchType " &
                "FROM SS_Configuration " &
                "LEFT JOIN SS_RequestTypes ON SS_Configuration.FormCode = SS_RequestTypes.RequestCode " &
                "WHERE SS_Configuration.EmployeeID = @EmployeeID " &
                "   OR (ISNULL(SS_Configuration.PositionID, 0) > 0 AND SS_Configuration.PositionID IN (" &
                "       SELECT PositionID FROM hrs_Contracts " &
                "       WHERE EmployeeID = @EmployeeID " &
                "       AND CancelDate IS NULL " &
                "       AND (EndDate IS NULL OR EndDate >= GETDATE())" &
                "   ))" &
                "   OR (SS_Configuration.UserTypeID = 1 AND EXISTS (" &
                "       SELECT 1 FROM hrs_Employees " &
                "       WHERE ManagerID = @EmployeeID " &
                "       AND CancelDate IS NULL " &
                "       AND ExcludeDate IS NULL" &
                "   ))"

            Using conn As New SqlConnection(connStr)
                conn.Open()

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

                Using cmd As New SqlCommand(sqlConfiguration, conn)
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
                    Using da As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        For Each row As DataRow In dt.Rows
                            Select Case row("MatchType").ToString()
                                Case "Employee"
                                    row("MatchType") = GetRes("MatchTypeEmployee")
                                Case "DirectManager"
                                    row("MatchType") = GetRes("MatchTypeDirectManager")
                                Case "Position"
                                    row("MatchType") = GetRes("MatchTypePosition")
                            End Select
                        Next
                        grdConfiguration.DataSource = dt
                        grdConfiguration.DataBind()
                        lblConfigurationCount.Text = dt.Rows.Count.ToString()
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Response.Write("<div style='color:red;padding:20px;'>" & GetRes("MsgErrorPrefix") & ex.Message & "</div>")
        End Try
    End Sub

    Protected Sub txtReplacementEmpCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ResolveEmployeeCode(txtReplacementEmpCode, lblReplacementEmpName, hdnReplacementEmployeeID)
        If chkSameEmployee.Checked Then CopyReplacementToDelegate()
    End Sub

    Protected Sub txtDelegateEmpCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ResolveEmployeeCode(txtDelegateEmpCode, lblDelegateEmpName, hdnDelegateEmployeeID)
    End Sub

    Protected Sub chkSameEmployee_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ClearMessage()
        ApplySameEmployeeState()
    End Sub

    ' إظهار/إخفاء حقل المفوض ونسخ بيانات المعتمد البديل إليه عند تفعيل الخيار
    Private Sub ApplySameEmployeeState()
        rowDelegate.Visible = Not chkSameEmployee.Checked
        If chkSameEmployee.Checked Then CopyReplacementToDelegate()
    End Sub

    ' النسخ التلقائي من المعتمد البديل إلى المفوض للطلبات المعلقة
    Private Sub CopyReplacementToDelegate()
        txtDelegateEmpCode.Text = txtReplacementEmpCode.Text
        lblDelegateEmpName.Text = lblReplacementEmpName.Text
        hdnDelegateEmployeeID.Value = hdnReplacementEmployeeID.Value
    End Sub

    Private Sub ResolveEmployeeCode(ByVal txtCode As TextBox, ByVal lblName As Label, ByVal hdnID As HiddenField)
        Try
            ClearMessage()
            lblName.Text = ""
            hdnID.Value = "0"

            Dim code As String = txtCode.Text.Trim()
            If code = "" Then Return

            Dim sourceEmployeeID As Integer = GetSourceEmployeeID()
            If ClsEmployees.Find("Code='" & code.Replace("'", "''") & "' AND CancelDate IS NULL AND ExcludeDate IS NULL") Then
                If ClsEmployees.ID = sourceEmployeeID Then
                    ShowMessage(GetRes("MsgSameEmployee"), False)
                    Return
                End If

                txtCode.Text = ClsEmployees.Code
                lblName.Text = ClsEmployees.FullName
                hdnID.Value = ClsEmployees.ID.ToString()
            Else
                ShowMessage(GetRes("MsgInvalidEmployee"), False)
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, False)
        End Try
    End Sub

    Protected Sub btnTransferApprovals_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ClearMessage()

            Dim sourceEmployeeID As Integer = GetSourceEmployeeID()
            Dim replacementEmployeeID As Integer = 0
            Dim delegateEmployeeID As Integer = 0
            Integer.TryParse(hdnReplacementEmployeeID.Value, replacementEmployeeID)
            Integer.TryParse(hdnDelegateEmployeeID.Value, delegateEmployeeID)

            If delegateEmployeeID <= 0 Then delegateEmployeeID = replacementEmployeeID

            If sourceEmployeeID <= 0 Then
                ShowMessage(GetRes("MsgSourceNotSpecified"), False)
                Return
            End If

            If replacementEmployeeID <= 0 AndAlso delegateEmployeeID <= 0 Then
                ShowMessage(GetRes("MsgEnterEmployee"), False)
                Return
            End If

            If replacementEmployeeID = sourceEmployeeID OrElse delegateEmployeeID = sourceEmployeeID Then
                ShowMessage(GetRes("MsgCannotTransferSame"), False)
                Return
            End If

            Dim updatedActions As Integer = 0
            Dim updatedConfig As Integer = 0

            Using conn As New SqlConnection(ClsEmployees.ConnectionString)
                conn.Open()
                Using tran As SqlTransaction = conn.BeginTransaction()
                    Try
                        If delegateEmployeeID > 0 Then
                            Using cmd As New SqlCommand(
                                "UPDATE SS_RequestActions " &
                                "SET SS_EmployeeID = @TargetEmployeeID " &
                                "WHERE ActionID IS NULL AND IsHidden IS NULL AND SS_EmployeeID = @SourceEmployeeID", conn, tran)
                                cmd.Parameters.AddWithValue("@TargetEmployeeID", delegateEmployeeID)
                                cmd.Parameters.AddWithValue("@SourceEmployeeID", sourceEmployeeID)
                                updatedActions = cmd.ExecuteNonQuery()
                            End Using
                        End If

                        If replacementEmployeeID > 0 Then
                            Using cmd As New SqlCommand(
                                "UPDATE SS_Configuration " &
                                "SET EmployeeID = @TargetEmployeeID " &
                                "WHERE EmployeeID = @SourceEmployeeID", conn, tran)
                                cmd.Parameters.AddWithValue("@TargetEmployeeID", replacementEmployeeID)
                                cmd.Parameters.AddWithValue("@SourceEmployeeID", sourceEmployeeID)
                                updatedConfig = cmd.ExecuteNonQuery()
                            End Using
                        End If

                        tran.Commit()
                    Catch
                        tran.Rollback()
                        Throw
                    End Try
                End Using
            End Using

            LoadData(sourceEmployeeID)
            ShowMessage(String.Format(GetRes("MsgTransferSuccess"), updatedActions, updatedConfig), True)

        Catch ex As Exception
            ShowMessage(GetRes("MsgTransferFailed") & ex.Message, False)
        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim EmployeeID As Integer = GetSourceEmployeeID()
            If EmployeeID > 0 Then
                LoadData(EmployeeID)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetSourceEmployeeID() As Integer
        Dim employeeID As Integer = 0
        If Not Integer.TryParse(hdnSourceEmployeeID.Value, employeeID) Then
            If Request.QueryString("EmployeeID") IsNot Nothing Then
                Integer.TryParse(Request.QueryString("EmployeeID"), employeeID)
            End If
        End If
        Return employeeID
    End Function

    Private Sub ShowMessage(ByVal message As String, ByVal isSuccess As Boolean)
        lblTransferMessage.Text = message
        lblTransferMessage.CssClass = "msg show " & If(isSuccess, "msg-ok", "msg-err")
    End Sub

    Private Sub ClearMessage()
        lblTransferMessage.Text = ""
        lblTransferMessage.CssClass = "msg"
    End Sub

End Class
