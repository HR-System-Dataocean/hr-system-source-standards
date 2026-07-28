Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
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

                    If Request.QueryString("EmpCode") IsNot Nothing AndAlso Request.QueryString("EmpCode").Trim() <> "" Then
                        lblEmpCode.Text = Request.QueryString("EmpCode").Trim()
                    End If

                    If Request.QueryString("EmpName") IsNot Nothing AndAlso Request.QueryString("EmpName").Trim() <> "" Then
                        lblEmpName.Text = Request.QueryString("EmpName").Trim()
                    End If

                    Dim effectiveFromDate As Date = Date.Today
                    If Request.QueryString("LastWorkingDate") IsNot Nothing AndAlso Request.QueryString("LastWorkingDate").Trim() <> "" Then
                        lblLastWorkingDate.Text = Request.QueryString("LastWorkingDate").Trim()
                        Dim parsedEosDate As Date
                        If Date.TryParseExact(lblLastWorkingDate.Text, "dd/MM/yyyy",
                                              System.Globalization.CultureInfo.InvariantCulture,
                                              System.Globalization.DateTimeStyles.None, parsedEosDate) Then
                            effectiveFromDate = parsedEosDate
                        ElseIf Date.TryParse(lblLastWorkingDate.Text, parsedEosDate) Then
                            effectiveFromDate = parsedEosDate
                        End If
                    End If

                    txtEffectiveFrom.Value = effectiveFromDate
                    txtEffectiveTo.Value = New Date(3000, 1, 1)
                    lblEffectiveTo.Visible = False
                    txtEffectiveTo.Visible = False
                    txtRemarks.Text = GetRes("txtRemarksDefault")

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

        pageBody.Attributes("dir") = direction
        pageBody.Style("text-align") = alignment
    End Sub

    Private Sub SetupConfirmScript()
        Dim confirmText As String = GetRes("MsgConfirmHandover").Replace("'", "\'")
        btnTransferApprovals.OnClientClick = "return confirm('" & confirmText & "');"
        btnCancelSelectedOpen.OnClientClick = "return confirm('" & GetRes("MsgConfirmCancelSelected").Replace("'", "\'") & "');"
        btnRejectCancelSelectedOpen.OnClientClick = "return confirm('" & GetRes("MsgConfirmRejectSelected").Replace("'", "\'") & "');"
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

            Dim UrlStringManager As String = "'frmModalSearchScreen.aspx?TargetControl=" & txtNewManagerCode.ID &
                "&SearchID=" & csSearchID & "&',510,720,false,'" & txtNewManagerCode.ClientID & "'"
            btnSearchNewManager.OnClientClick = "OpenModal1(" & UrlStringManager & "); return false;"
        Catch ex As Exception
            btnSearchReplacementEmp.Visible = False
            btnSearchDelegateEmp.Visible = False
            btnSearchNewManager.Visible = False
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
                "ROW_NUMBER() OVER (ORDER BY v.RequestDate DESC, v.ID DESC) AS RowNumber, " &
                "v.ID AS RequestSerial, " &
                "v.FormCode, " &
                "ISNULL(NULLIF(LTRIM(RTRIM(CAST(v.RequestSerial AS nvarchar(50)))), ''), CAST(v.ID AS nvarchar(50))) AS RequestNumber, " &
                If(IsArabicLanguage(),
                   "ISNULL(v.RequestArbName, v.RequestEngName) AS RequestName, ",
                   "ISNULL(v.RequestEngName, v.RequestArbName) AS RequestName, ") &
                "ISNULL(v.RequestStautsTypeID, 4) AS StatusTypeID, " &
                "ISNULL((" &
                "  SELECT TOP 1 CASE " &
                "    WHEN ISNULL(c.UserTypeID,0)=1 THEN N'DirectManager' " &
                "    ELSE N'ApprovalStage' END " &
                "  FROM SS_RequestActions a " &
                "  LEFT JOIN SS_Configuration c ON a.ConfigID = c.ID " &
                "  WHERE a.RequestSerial = v.ID AND a.FormCode = v.FormCode " &
                "  AND a.ActionID IS NULL AND a.IsHidden IS NULL AND a.Seen=0 " &
                "  ORDER BY a.ActionSerial" &
                "), N'ApprovalStage') AS StageKey, " &
                "ISNULL((" &
                "  SELECT TOP 1 ISNULL(c.Rank,0) " &
                "  FROM SS_RequestActions a " &
                "  LEFT JOIN SS_Configuration c ON a.ConfigID = c.ID " &
                "  WHERE a.RequestSerial = v.ID AND a.FormCode = v.FormCode " &
                "  AND a.ActionID IS NULL AND a.IsHidden IS NULL AND a.Seen=0 " &
                "  ORDER BY a.ActionSerial" &
                "), 0) AS StageRank " &
                "FROM SS_VFollowup v " &
                "WHERE v.EmployeeID = @EmployeeID " &
                "AND ISNULL(v.RequestStautsTypeID, 0) IN (3, 4) " &
                "AND EXISTS (" &
                "  SELECT 1 FROM SS_RequestActions a " &
                "  WHERE a.RequestSerial = v.ID AND a.FormCode = v.FormCode " &
                "  AND a.ActionID IS NULL AND a.IsHidden IS NULL AND a.Seen=0 " &
                ")"

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
                        dt.Columns.Add("CurrentStage", GetType(String))
                        For Each row As DataRow In dt.Rows
                            Dim stageKey As String = Convert.ToString(row("StageKey"))
                            Dim stageRank As Integer = 0
                            Integer.TryParse(Convert.ToString(row("StageRank")), stageRank)
                            If stageKey = "DirectManager" Then
                                row("CurrentStage") = GetRes("StageDirectManager")
                            Else
                                row("CurrentStage") = String.Format(GetRes("StageApprovalLevel"), stageRank)
                            End If
                        Next
                        grdSubmittedOpen.DataSource = dt
                        grdSubmittedOpen.DataBind()
                        lblSubmittedOpenCount.Text = dt.Rows.Count.ToString()
                        lblOpenSelectedCount.Text = "0 " & GetRes("lblSelectedCountSuffix")
                        pnlOpenRequests.Visible = True
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

            LoadDirectManagerSection(EmployeeID)
            LoadAutoApprovalSection(EmployeeID)
            LoadMaterialCustodySection(EmployeeID)
            UpdateSectionVisibility()
            UpdateActionsBadge()
            UpdateSummaryMetrics(EmployeeID)

        Catch ex As Exception
            Response.Write("<div style='color:red;padding:20px;'>" & GetRes("MsgErrorPrefix") & ex.Message & "</div>")
        End Try
    End Sub

    Private Sub UpdateSummaryMetrics(ByVal EmployeeID As Integer)
        Dim pendingCount As Integer = 0
        Dim rulesCount As Integer = 0
        Dim reportsCount As Integer = 0
        Dim actionsCount As Integer = 0
        Dim custodyTotal As Integer = 0
        Dim custodyPending As Integer = 0

        Integer.TryParse(lblActionNeededCount.Text, pendingCount)
        Integer.TryParse(lblConfigurationCount.Text, rulesCount)
        Integer.TryParse(hdnSubordinateCount.Value, reportsCount)
        Integer.TryParse(lblCustodyTotal.Text, custodyTotal)
        Integer.TryParse(lblCustodyPending.Text, custodyPending)

        If rulesCount > 0 Then actionsCount += 1
        If pendingCount > 0 Then actionsCount += 1
        If reportsCount > 0 Then actionsCount += 1
        If custodyPending > 0 Then actionsCount += 1

        lblMetricPending.Text = pendingCount.ToString()
        lblMetricRules.Text = rulesCount.ToString()
        lblMetricReports.Text = reportsCount.ToString()
        lblMetricCustody.Text = custodyTotal.ToString()
        lblMetricActions.Text = actionsCount.ToString()
    End Sub

    Private Function GetItemNameSql() As String
        If IsArabicLanguage() Then
            Return "ISNULL(i.ArbName, i.EngName)"
        End If
        Return "ISNULL(i.EngName, i.ArbName)"
    End Function

    Private Sub EnsureCustodyFilterItems()
        If ddlCustodyFilter.Items.Count > 0 Then Return
        ddlCustodyFilter.Items.Add(New ListItem(GetRes("ddlCustodyFilterAllResource1.Text"), "All"))
        ddlCustodyFilter.Items.Add(New ListItem(GetRes("ddlCustodyFilterPendingResource1.Text"), "Pending"))
        ddlCustodyFilter.Items.Add(New ListItem(GetRes("ddlCustodyFilterReturnedResource1.Text"), "Returned"))
    End Sub

    Private Sub LoadMaterialCustodySection(ByVal EmployeeID As Integer)
        Try
            EnsureCustodyFilterItems()
            txtCustodySearch.Attributes("placeholder") = GetRes("txtCustodySearchPlaceholder")
            btnMarkAllReturned.OnClientClick = "return confirm('" & GetRes("MsgConfirmMarkAllReturned").Replace("'", "\'") & "');"

            Dim filter As String = ddlCustodyFilter.SelectedValue
            If String.IsNullOrEmpty(filter) Then filter = "All"
            Dim search As String = txtCustodySearch.Text.Trim()

            Dim sql As String =
                "SELECT ROW_NUMBER() OVER (ORDER BY ei.ID) AS RowNumber," &
                " ei.ID, ISNULL(i.Code,'') AS AssetCode, " & GetItemNameSql() & " AS AssetName," &
                " ISNULL(i.LicenseNumber,'') AS SerialNo," &
                " CONVERT(varchar(10), ei.ReceivedDate, 103) AS ReceivedDateText," &
                " CONVERT(varchar(10), ei.ReturnedDate, 103) AS ReturnedDateText," &
                " ISNULL(ei.ReturningItemstatus,'') AS ReturningItemstatus," &
                " CASE WHEN ei.ReturnedDate IS NULL THEN 0 ELSE 1 END AS IsReturned," &
                " ISNULL(ei.IsFromAssets,0) AS IsFromAssets" &
                " FROM hrs_EmployeesItems ei" &
                " LEFT JOIN hrs_Items i ON i.ID = ei.ItemID" &
                " WHERE ei.EmployeeID=@EmployeeID" &
                " AND ei.CancelDate IS NULL" &
                " AND ISNULL(ei.IsConfirmed,0)=1"

            If filter = "Pending" Then
                sql &= " AND ei.ReturnedDate IS NULL"
            ElseIf filter = "Returned" Then
                sql &= " AND ei.ReturnedDate IS NOT NULL"
            End If

            If search <> "" Then
                sql &= " AND (ISNULL(i.Code,'') LIKE @Search OR " & GetItemNameSql() & " LIKE @Search OR ISNULL(i.LicenseNumber,'') LIKE @Search)"
            End If

            sql &= " ORDER BY ei.ID"

            Dim dt As New DataTable()
            Using conn As New SqlConnection(ClsEmployees.ConnectionString)
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
                    If search <> "" Then
                        cmd.Parameters.AddWithValue("@Search", "%" & search & "%")
                    End If
                    conn.Open()
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using

            Dim total As Integer = GetCustodyCount(EmployeeID, "All")
            Dim pending As Integer = GetCustodyCount(EmployeeID, "Pending")
            Dim returned As Integer = GetCustodyCount(EmployeeID, "Returned")

            lblCustodyTotal.Text = total.ToString()
            lblCustodyPending.Text = pending.ToString()
            lblCustodyReturned.Text = returned.ToString()
            lblCustodyCountBadge.Text = total.ToString()
            lblCustodyClearanceStatus.Text = If(pending > 0, GetRes("lblCustodyStatusIncomplete"), GetRes("lblCustodyStatusComplete"))

            grdCustody.DataSource = dt
            grdCustody.DataBind()

            'pnlCustodySection.Visible = (total > 0)
            btnMarkAllReturned.Enabled = (pending > 0)

        Catch ex As Exception
            'pnlCustodySection.Visible = False
        End Try
    End Sub

    Private Function GetCustodyCount(ByVal EmployeeID As Integer, ByVal mode As String) As Integer
        Dim sql As String =
            "SELECT COUNT(*) FROM hrs_EmployeesItems" &
            " WHERE EmployeeID=@EmployeeID AND CancelDate IS NULL AND ISNULL(IsConfirmed,0)=1"

        If mode = "Pending" Then
            sql &= " AND ReturnedDate IS NULL"
        ElseIf mode = "Returned" Then
            sql &= " AND ReturnedDate IS NOT NULL"
        End If

        Using conn As New SqlConnection(ClsEmployees.ConnectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
                conn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Private Sub BindReturnConditionList(ByVal ddl As DropDownList, ByVal selectedValue As String)
        ddl.Items.Clear()
        ddl.Items.Add(New ListItem(GetRes("ddlConditionSelect"), ""))
        ddl.Items.Add(New ListItem(GetRes("ddlConditionGood"), GetRes("ddlConditionGood")))
        ddl.Items.Add(New ListItem(GetRes("ddlConditionDamaged"), GetRes("ddlConditionDamaged")))
        ddl.Items.Add(New ListItem(GetRes("ddlConditionLost"), GetRes("ddlConditionLost")))
        ddl.Items.Add(New ListItem(GetRes("ddlConditionOther"), GetRes("ddlConditionOther")))
        If Not String.IsNullOrEmpty(selectedValue) Then
            Dim item As ListItem = ddl.Items.FindByValue(selectedValue)
            If item Is Nothing Then
                ddl.Items.Add(New ListItem(selectedValue, selectedValue))
            End If
            ddl.SelectedValue = selectedValue
        End If
    End Sub

    Protected Sub grdCustody_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType <> DataControlRowType.DataRow Then Return

        Dim drv As DataRowView = TryCast(e.Row.DataItem, DataRowView)
        If drv Is Nothing Then Return

        Dim isReturned As Boolean = Convert.ToInt32(drv("IsReturned")) = 1
        Dim isFromAssets As Boolean = Convert.ToBoolean(drv("IsFromAssets"))

        Dim lblItemStatus As Label = TryCast(e.Row.FindControl("lblItemStatus"), Label)
        Dim lblReturnedDate As Label = TryCast(e.Row.FindControl("lblReturnedDate"), Label)
        Dim txtReturnedDate As TextBox = TryCast(e.Row.FindControl("txtReturnedDate"), TextBox)
        Dim lblReturnCondition As Label = TryCast(e.Row.FindControl("lblReturnCondition"), Label)
        Dim ddlReturnCondition As DropDownList = TryCast(e.Row.FindControl("ddlReturnCondition"), DropDownList)
        Dim btnConfirmReturn As Button = TryCast(e.Row.FindControl("btnConfirmReturn"), Button)

        If lblItemStatus IsNot Nothing Then
            If isReturned Then
                lblItemStatus.Text = GetRes("lblStatusReturned")
                lblItemStatus.CssClass = "status-returned"
            Else
                lblItemStatus.Text = GetRes("lblStatusPendingReturn")
                lblItemStatus.CssClass = "status-pending"
            End If
        End If

        If isReturned Then
            If lblReturnedDate IsNot Nothing Then
                lblReturnedDate.Text = Convert.ToString(drv("ReturnedDateText"))
                lblReturnedDate.Visible = True
            End If
            If txtReturnedDate IsNot Nothing Then txtReturnedDate.Visible = False
            If lblReturnCondition IsNot Nothing Then
                lblReturnCondition.Text = Convert.ToString(drv("ReturningItemstatus"))
                lblReturnCondition.Visible = True
            End If
            If ddlReturnCondition IsNot Nothing Then ddlReturnCondition.Visible = False
            If btnConfirmReturn IsNot Nothing Then
                btnConfirmReturn.Text = GetRes("btnCompleted")
                btnConfirmReturn.CssClass = "btn-completed"
                btnConfirmReturn.Enabled = False
            End If
        Else
            If lblReturnedDate IsNot Nothing Then lblReturnedDate.Visible = False
            If txtReturnedDate IsNot Nothing Then
                txtReturnedDate.Visible = True
                txtReturnedDate.Text = Date.Today.ToString("dd/MM/yyyy")
                txtReturnedDate.Attributes("placeholder") = "dd/MM/yyyy"
            End If
            If lblReturnCondition IsNot Nothing Then lblReturnCondition.Visible = False
            If ddlReturnCondition IsNot Nothing Then
                ddlReturnCondition.Visible = True
                BindReturnConditionList(ddlReturnCondition, "")
            End If
            If btnConfirmReturn IsNot Nothing Then
                btnConfirmReturn.Text = GetRes("btnConfirmReturn")
                btnConfirmReturn.CssClass = "btn-confirm-return"
                btnConfirmReturn.Enabled = Not isFromAssets
                If isFromAssets Then
                    btnConfirmReturn.ToolTip = GetRes("MsgAssetLocked")
                End If
            End If
        End If
    End Sub

    Protected Sub grdCustody_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName <> "ConfirmReturn" Then Return

        Try
            ClearMessage()
            Dim itemID As Integer = 0
            Integer.TryParse(Convert.ToString(e.CommandArgument), itemID)
            If itemID <= 0 Then Return

            Dim row As GridViewRow = Nothing
            For Each r As GridViewRow In grdCustody.Rows
                Dim btn As Button = TryCast(r.FindControl("btnConfirmReturn"), Button)
                If btn IsNot Nothing AndAlso btn.CommandArgument = itemID.ToString() Then
                    row = r
                    Exit For
                End If
            Next
            If row Is Nothing Then Return

            Dim txtReturnedDate As TextBox = TryCast(row.FindControl("txtReturnedDate"), TextBox)
            Dim ddlReturnCondition As DropDownList = TryCast(row.FindControl("ddlReturnCondition"), DropDownList)

            Dim returnedDateText As String = If(txtReturnedDate Is Nothing, "", txtReturnedDate.Text.Trim())
            Dim condition As String = If(ddlReturnCondition Is Nothing, "", ddlReturnCondition.SelectedValue)

            If returnedDateText = "" Then
                ShowMessage(GetRes("MsgReturnDateRequired"), False)
                Return
            End If
            If condition = "" Then
                ShowMessage(GetRes("MsgReturnConditionRequired"), False)
                Return
            End If

            Dim returnedDate As Date
            If Not Date.TryParseExact(returnedDateText, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, returnedDate) Then
                If Not Date.TryParse(returnedDateText, returnedDate) Then
                    ShowMessage(GetRes("MsgReturnDateRequired"), False)
                    Return
                End If
            End If

            Dim clsItem As New Clshrs_EmployeesItems(Page)
            If Not clsItem.Find("ID=" & itemID) Then Return

            If clsItem.IsFromAssets IsNot Nothing AndAlso Convert.ToBoolean(clsItem.IsFromAssets) Then
                ShowMessage(GetRes("MsgAssetLocked"), False)
                Return
            End If

            clsItem.ReturnedDate = returnedDate
            clsItem.ReturningItemstatus = condition
            clsItem.Update("ID=" & itemID)

            ShowMessage(GetRes("MsgReturnSuccess"), True)
            LoadMaterialCustodySection(GetSourceEmployeeID())
            UpdateSummaryMetrics(GetSourceEmployeeID())

        Catch ex As Exception
            ShowMessage(GetRes("MsgErrorPrefix") & ex.Message, False)
        End Try
    End Sub

    Protected Sub btnMarkAllReturned_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ClearMessage()
            Dim employeeID As Integer = GetSourceEmployeeID()
            If employeeID <= 0 Then Return

            Dim clsItem As New Clshrs_EmployeesItems(Page)
            Dim sql As String =
                "SELECT ID FROM hrs_EmployeesItems" &
                " WHERE EmployeeID=@EmployeeID AND CancelDate IS NULL" &
                " AND ISNULL(IsConfirmed,0)=1 AND ReturnedDate IS NULL" &
                " AND ISNULL(IsFromAssets,0)=0"

            Dim updated As Integer = 0
            Using conn As New SqlConnection(ClsEmployees.ConnectionString)
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeID)
                    conn.Open()
                    Using rdr As SqlDataReader = cmd.ExecuteReader()
                        Dim ids As New List(Of Integer)
                        While rdr.Read()
                            ids.Add(Convert.ToInt32(rdr("ID")))
                        End While
                        rdr.Close()

                        For Each id As Integer In ids
                            clsItem = New Clshrs_EmployeesItems(Page)
                            If clsItem.Find("ID=" & id) Then
                                clsItem.ReturnedDate = Date.Today
                                If String.IsNullOrEmpty(Convert.ToString(clsItem.ReturningItemstatus)) Then
                                    clsItem.ReturningItemstatus = GetRes("ddlConditionGood")
                                End If
                                clsItem.Update("ID=" & id)
                                updated += 1
                            End If
                        Next
                    End Using
                End Using
            End Using

            ShowMessage(String.Format(GetRes("MsgMarkAllSuccess"), updated), True)
            LoadMaterialCustodySection(employeeID)
            UpdateSummaryMetrics(employeeID)

        Catch ex As Exception
            ShowMessage(GetRes("MsgErrorPrefix") & ex.Message, False)
        End Try
    End Sub

    Protected Sub btnCustodySearch_Click(ByVal sender As Object, ByVal e As EventArgs)
        LoadMaterialCustodySection(GetSourceEmployeeID())
        UpdateSummaryMetrics(GetSourceEmployeeID())
    End Sub

    Protected Sub ddlCustodyFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        LoadMaterialCustodySection(GetSourceEmployeeID())
        UpdateSummaryMetrics(GetSourceEmployeeID())
    End Sub

    Private Function GetMaterialCustodyCount(ByVal EmployeeID As Integer) As Integer
        Return GetCustodyCount(EmployeeID, "All")
    End Function

    Private Sub LoadDirectManagerSection(ByVal EmployeeID As Integer)
        Dim count As Integer = GetSubordinateCount(EmployeeID)
        hdnSubordinateCount.Value = count.ToString()
        cardDirectManager.Visible = (count > 0)

        If count > 0 Then
            lblDirectManagerBadge.Text = String.Format(GetRes("lblEmployeesBadgeFormat"), count)
            lblDirectManagerSectionDesc.Text = String.Format(GetRes("lblDirectManagerSectionDescFormat"), count)
            lblDirectManagerInfo.Text = String.Format(GetRes("lblDirectManagerInfoFormat"), count)
        End If
    End Sub

    Private Function GetSubordinateCount(ByVal managerID As Integer) As Integer
        Dim sql As String = "SELECT COUNT(*) FROM hrs_Employees" &
            " WHERE ManagerID=@ManagerID AND CancelDate IS NULL AND ExcludeDate IS NULL"
        Using conn As New SqlConnection(ClsEmployees.ConnectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ManagerID", managerID)
                conn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Private Sub LoadAutoApprovalSection(ByVal EmployeeID As Integer)
        Dim hasAuto As Boolean = HasAutoContinueApprovals(EmployeeID)
        cardAutoApproval.Visible = hasAuto
    End Sub

    ' إعدادات موافقة ApplyForAll على منصب الموظف ويوجد موظفون نشطون آخرون على نفس المنصب
    Private Function HasAutoContinueApprovals(ByVal EmployeeID As Integer) As Boolean
        Dim sql As String =
            "SELECT CASE WHEN EXISTS (" &
            "  SELECT 1 FROM SS_Configuration c" &
            "  WHERE ISNULL(c.ApplyForAll,0)=1" &
            "    AND ISNULL(c.PositionID,0)>0" &
            "    AND c.PositionID IN (" &
            "      SELECT PositionID FROM hrs_Contracts" &
            "      WHERE EmployeeID=@EmployeeID AND CancelDate IS NULL" &
            "        AND (EndDate IS NULL OR EndDate >= GETDATE())" &
            "    )" &
            "    AND EXISTS (" &
            "      SELECT 1 FROM hrs_Contracts oc" &
            "      INNER JOIN hrs_Employees oe ON oe.ID=oc.EmployeeID" &
            "      WHERE oc.PositionID=c.PositionID" &
            "        AND oc.EmployeeID<>@EmployeeID" &
            "        AND oc.CancelDate IS NULL" &
            "        AND (oc.EndDate IS NULL OR oc.EndDate >= GETDATE())" &
            "        AND oe.CancelDate IS NULL AND oe.ExcludeDate IS NULL" &
            "    )" &
            ") THEN 1 ELSE 0 END"

        Using conn As New SqlConnection(ClsEmployees.ConnectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
                conn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Private Sub UpdateActionsBadge()
        Dim actionsCount As Integer = 0
        Dim actionNeeded As Integer = 0
        Dim configCount As Integer = 0
        Dim subordinateCount As Integer = 0
        Integer.TryParse(lblActionNeededCount.Text, actionNeeded)
        Integer.TryParse(lblConfigurationCount.Text, configCount)
        Integer.TryParse(hdnSubordinateCount.Value, subordinateCount)

        If configCount > 0 Then actionsCount += 1
        If actionNeeded > 0 Then actionsCount += 1
        If subordinateCount > 0 Then actionsCount += 1
        If actionsCount = 0 Then actionsCount = 1

        lblActionsBadge.Text = String.Format(GetRes("lblActionsBadgeFormat"), actionsCount)
    End Sub

    Private Sub UpdateSectionVisibility()
        Dim actionNeeded As Integer = 0
        Dim configCount As Integer = 0
        Integer.TryParse(lblActionNeededCount.Text, actionNeeded)
        Integer.TryParse(lblConfigurationCount.Text, configCount)

        cardReplacement.Visible = (configCount > 0)
        cardDelegate.Visible = (actionNeeded > 0)
        lblReplacementStatus.Visible = (configCount > 0)
        lblDelegateStatus.Visible = (actionNeeded > 0)
    End Sub

    Protected Sub txtReplacementEmpCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ResolveEmployeeCode(txtReplacementEmpCode, lblReplacementEmpName, hdnReplacementEmployeeID)
        If chkSameEmployee.Checked Then CopyReplacementToDelegate()
    End Sub

    Protected Sub txtDelegateEmpCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ResolveEmployeeCode(txtDelegateEmpCode, lblDelegateEmpName, hdnDelegateEmployeeID)
    End Sub

    Protected Sub txtNewManagerCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ResolveEmployeeCode(txtNewManagerCode, lblNewManagerName, hdnNewManagerID)
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
            Dim newManagerID As Integer = 0
            Integer.TryParse(hdnReplacementEmployeeID.Value, replacementEmployeeID)
            Integer.TryParse(hdnDelegateEmployeeID.Value, delegateEmployeeID)
            Integer.TryParse(hdnNewManagerID.Value, newManagerID)

            ' إعادة حل المدير المباشر من الكود إذا لم يُحفظ المعرف في الحقل المخفي
            If newManagerID <= 0 AndAlso txtNewManagerCode.Text.Trim() <> "" Then
                ResolveEmployeeCode(txtNewManagerCode, lblNewManagerName, hdnNewManagerID)
                Integer.TryParse(hdnNewManagerID.Value, newManagerID)
            End If

            If delegateEmployeeID <= 0 Then delegateEmployeeID = replacementEmployeeID

            Dim subordinateCount As Integer = GetSubordinateCount(sourceEmployeeID)
            Dim needsManagerUpdate As Boolean = (subordinateCount > 0) OrElse cardDirectManager.Visible

            If sourceEmployeeID <= 0 Then
                ShowMessage(GetRes("MsgSourceNotSpecified"), False)
                Return
            End If

            If needsManagerUpdate AndAlso newManagerID <= 0 Then
                ShowMessage(GetRes("MsgEnterNewManager"), False)
                Return
            End If

            If cardReplacement.Visible AndAlso replacementEmployeeID <= 0 AndAlso (Not cardDelegate.Visible OrElse delegateEmployeeID <= 0) Then
                ShowMessage(GetRes("MsgEnterEmployee"), False)
                Return
            End If

            If cardDelegate.Visible AndAlso Not chkSameEmployee.Checked AndAlso delegateEmployeeID <= 0 AndAlso replacementEmployeeID <= 0 Then
                ShowMessage(GetRes("MsgEnterEmployee"), False)
                Return
            End If

            If replacementEmployeeID = sourceEmployeeID OrElse delegateEmployeeID = sourceEmployeeID OrElse newManagerID = sourceEmployeeID Then
                ShowMessage(GetRes("MsgCannotTransferSame"), False)
                Return
            End If

            Dim effectiveFrom As Date = Date.Today
            Dim effectiveTo As Date = New Date(3000, 1, 1)
            If cardReplacement.Visible Then
                If Not TryGetEffectiveDates(effectiveFrom, effectiveTo) Then Return
            End If

            Dim updatedActions As Integer = 0
            Dim updatedConfig As Integer = 0
            Dim updatedManagers As Integer = 0

            Using conn As New SqlConnection(ClsEmployees.ConnectionString)
                conn.Open()
                Using tran As SqlTransaction = conn.BeginTransaction()
                    Try
                        If delegateEmployeeID > 0 AndAlso cardDelegate.Visible Then
                            Using cmd As New SqlCommand(
                                "INSERT INTO SS_RequestActions (RequestSerial, SS_EmployeeID, FormCode, EmployeeID, Seen, ConfigID) " &
                                "SELECT RequestSerial, @TargetEmployeeID, FormCode, EmployeeID, 0, ConfigID " &
                                "FROM SS_RequestActions " &
                                "WHERE ActionID IS NULL AND IsHidden IS NULL AND Seen = 0 AND SS_EmployeeID = @SourceEmployeeID", conn, tran)
                                cmd.Parameters.AddWithValue("@TargetEmployeeID", delegateEmployeeID)
                                cmd.Parameters.AddWithValue("@SourceEmployeeID", sourceEmployeeID)
                                cmd.ExecuteNonQuery()
                            End Using

                            Using cmd As New SqlCommand(
                                "UPDATE SS_RequestActions " &
                                "SET Seen = 1, ActionID = 3, ActionDate = GETDATE(), ActionRemarks = @Remarks " &
                                "WHERE ActionID IS NULL AND IsHidden IS NULL AND Seen = 0 AND SS_EmployeeID = @SourceEmployeeID", conn, tran)
                                cmd.Parameters.AddWithValue("@SourceEmployeeID", sourceEmployeeID)
                                cmd.Parameters.AddWithValue("@Remarks", If(txtRemarks.Text.Trim() = "", "End of Service", txtRemarks.Text.Trim()))
                                updatedActions = cmd.ExecuteNonQuery()
                            End Using
                        End If

                        If replacementEmployeeID > 0 AndAlso cardReplacement.Visible Then
                            Using cmd As New SqlCommand(
                                "UPDATE SS_Configuration " &
                                "SET EmployeeID = @TargetEmployeeID " &
                                "WHERE EmployeeID = @SourceEmployeeID", conn, tran)
                                cmd.Parameters.AddWithValue("@TargetEmployeeID", replacementEmployeeID)
                                cmd.Parameters.AddWithValue("@SourceEmployeeID", sourceEmployeeID)
                                updatedConfig = cmd.ExecuteNonQuery()
                            End Using
                        End If

                        ' تحديث جميع الموظفين الذين مديرهم المباشر هو موظف نهاية الخدمة
                        If newManagerID > 0 AndAlso needsManagerUpdate Then
                            updatedManagers = UpdateSubordinatesManager(conn, tran, sourceEmployeeID, newManagerID)
                        End If

                        tran.Commit()
                    Catch
                        tran.Rollback()
                        Throw
                    End Try
                End Using
            End Using

            If replacementEmployeeID > 0 AndAlso cardReplacement.Visible Then
                CreateActingAssignments(sourceEmployeeID, replacementEmployeeID, effectiveFrom, effectiveTo)
            End If

            LoadData(sourceEmployeeID)

            Dim successMsg As String = String.Format(GetRes("MsgTransferSuccess"), updatedActions, updatedConfig)
            If updatedManagers > 0 Then
                successMsg &= " " & String.Format(GetRes("MsgManagerUpdated"), updatedManagers)
            End If
            ShowMessage(successMsg, True)

        Catch ex As Exception
            ShowMessage(GetRes("MsgTransferFailed") & ex.Message, False)
        End Try
    End Sub

    ' تعيين المدير المباشر الجديد لكل الموظفين التابعين لموظف نهاية الخدمة
    Private Function UpdateSubordinatesManager(ByVal conn As SqlConnection, ByVal tran As SqlTransaction,
                                              ByVal sourceEmployeeID As Integer, ByVal newManagerID As Integer) As Integer
        Using cmd As New SqlCommand(
            "UPDATE hrs_Employees " &
            "SET ManagerID = @NewManagerID " &
            "WHERE ManagerID = @SourceEmployeeID " &
            "AND CancelDate IS NULL " &
            "AND ExcludeDate IS NULL " &
            "AND ID <> @NewManagerID", conn, tran)
            cmd.Parameters.AddWithValue("@NewManagerID", newManagerID)
            cmd.Parameters.AddWithValue("@SourceEmployeeID", sourceEmployeeID)
            Return cmd.ExecuteNonQuery()
        End Using
    End Function

    Private Function TryGetEffectiveDates(ByRef effectiveFrom As Date, ByRef effectiveTo As Date) As Boolean
        If Convert.ToString(txtEffectiveFrom.Value) = "" Then
            ShowMessage(GetRes("MsgEffectiveDatesRequired"), False)
            Return False
        End If

        effectiveFrom = CDate(txtEffectiveFrom.Value).Date
        If Convert.ToString(txtEffectiveTo.Value) = "" Then
            txtEffectiveTo.Value = New Date(3000, 1, 1)
        End If
        effectiveTo = CDate(txtEffectiveTo.Value).Date

        If effectiveFrom > effectiveTo Then
            ShowMessage(GetRes("MsgEffectiveDatesInvalid"), False)
            Return False
        End If

        Return True
    End Function

    ' تسجيل حركات الإنابة (عن موظف/على وظيفة) للمعتمد البديل بسبب نهاية الخدمة
    Private Sub CreateActingAssignments(ByVal sourceEmployeeID As Integer, ByVal actingEmployeeID As Integer,
                                        ByVal effectiveFrom As Date, ByVal effectiveTo As Date)
        Const ACTING_REASON As String = "End of Service"

        Dim employeeActing As New Clshrs_ActingEmployeeAssignments(Page)
        If Not employeeActing.Find("CancelDate IS NULL AND OriginalEmployeeID=" & sourceEmployeeID &
                                   " AND ActingEmployeeID=" & actingEmployeeID) Then
            employeeActing.Clear()
            employeeActing.Code = NextAssignmentCode("hrs_ActingEmployeeAssignments", "EA")
            employeeActing.OriginalEmployeeID = sourceEmployeeID
            employeeActing.ActingEmployeeID = actingEmployeeID
            employeeActing.EffectiveFrom = effectiveFrom
            employeeActing.EffectiveTo = effectiveTo
            employeeActing.Reason = ACTING_REASON
            employeeActing.Remarks = ""
            employeeActing.SourceForm = "frmEmployeesEndofService"
            employeeActing.SourceID = sourceEmployeeID
            employeeActing.Save()
        End If

        Dim positionID As Integer = GetActivePositionID(sourceEmployeeID)
        If positionID > 0 Then
            Dim positionActing As New Clshrs_ActingPositionAssignments(Page)
            If Not positionActing.Find("CancelDate IS NULL AND OriginalPositionID=" & positionID &
                                       " AND ActingEmployeeID=" & actingEmployeeID) Then
                positionActing.Clear()
                positionActing.Code = NextAssignmentCode("hrs_ActingPositionAssignments", "PA")
                positionActing.OriginalPositionID = positionID
                positionActing.ActingEmployeeID = actingEmployeeID
                positionActing.EffectiveFrom = effectiveFrom
                positionActing.EffectiveTo = effectiveTo
                positionActing.Reason = ACTING_REASON
                positionActing.Remarks = ""
                positionActing.SourceForm = "frmEmployeesEndofService"
                positionActing.SourceID = sourceEmployeeID
                positionActing.Save()
            End If
        End If
    End Sub

    Private Function NextAssignmentCode(ByVal tableName As String, ByVal prefix As String) As String
        Dim sql As String = "SELECT ISNULL(MAX(CASE" &
            " WHEN ISNUMERIC(RIGHT(Code, LEN(Code) - CHARINDEX('-', Code)))=1" &
            " THEN CAST(RIGHT(Code, LEN(Code) - CHARINDEX('-', Code)) AS int)" &
            " ELSE 0 END),0)+1 FROM " & tableName & " WHERE CHARINDEX('-', Code) > 0"
        Dim number As Integer = Convert.ToInt32(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(
            ClsEmployees.ConnectionString, CommandType.Text, sql))
        Return prefix & "-" & number.ToString("000000")
    End Function

    Private Function GetActivePositionID(ByVal employeeID As Integer) As Integer
        Dim sql As String = "SELECT TOP 1 PositionID FROM hrs_Contracts" &
            " WHERE EmployeeID=@EmployeeID AND PositionID IS NOT NULL AND CancelDate IS NULL" &
            " AND (EndDate IS NULL OR EndDate >= GETDATE()) ORDER BY ID DESC"
        Using conn As New SqlConnection(ClsEmployees.ConnectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID)
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result Is Nothing OrElse IsDBNull(result) Then Return 0
                Return Convert.ToInt32(result)
            End Using
        End Using
    End Function

    Protected Sub grdSubmittedOpen_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType <> DataControlRowType.DataRow Then Return

        Dim chk As CheckBox = TryCast(e.Row.FindControl("chkSelect"), CheckBox)
        If chk IsNot Nothing Then
            chk.InputAttributes("class") = "chk-open-req"
            chk.InputAttributes("onclick") = "updateOpenRequestSelection();"
        End If

        Dim lblOpenStatus As Label = TryCast(e.Row.FindControl("lblOpenStatus"), Label)
        If lblOpenStatus IsNot Nothing Then
            lblOpenStatus.Text = GetRes("lblStatusPending")
        End If

        Dim btnView As Button = TryCast(e.Row.FindControl("btnViewOpen"), Button)
        Dim btnCancel As Button = TryCast(e.Row.FindControl("btnCancelOpen"), Button)
        Dim btnReject As Button = TryCast(e.Row.FindControl("btnRejectOpen"), Button)
        If btnView IsNot Nothing Then btnView.Text = GetRes("btnViewOpenResource1.Text")
        If btnCancel IsNot Nothing Then
            btnCancel.Text = GetRes("btnCancelOpenRowResource1.Text")
            btnCancel.OnClientClick = "return confirm('" & GetRes("MsgConfirmCancelRequest").Replace("'", "\'") & "');"
        End If
        If btnReject IsNot Nothing Then
            btnReject.Text = GetRes("btnRejectOpenRowResource1.Text")
            btnReject.OnClientClick = "return confirm('" & GetRes("MsgConfirmRejectRequest").Replace("'", "\'") & "');"
        End If
    End Sub

    Protected Sub grdSubmittedOpen_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Try
            ClearMessage()
            Dim parts() As String = Convert.ToString(e.CommandArgument).Split("|"c)
            If parts.Length < 2 Then Return

            Dim requestSerial As Integer = 0
            Integer.TryParse(parts(0), requestSerial)
            Dim formCode As String = parts(1)
            If requestSerial <= 0 OrElse formCode = "" Then Return

            Select Case e.CommandName
                Case "ViewOpen"
                    Dim url As String = GetStatusPageUrl(formCode, requestSerial)
                    If url <> "" Then
                        ClientScript.RegisterStartupScript(Me.GetType(), "OpenStatus",
                            "<script>window.open('" & url.Replace("'", "\'") & "','_blank','width=800,height=560,scrollbars=yes,resizable=yes');</script>", False)
                    End If
                Case "CancelOpen"
                    CloseOpenRequest(requestSerial, formCode, False)
                    ShowMessage(GetRes("MsgRequestCanceled"), True)
                    LoadData(GetSourceEmployeeID())
                Case "RejectOpen"
                    CloseOpenRequest(requestSerial, formCode, True)
                    ShowMessage(GetRes("MsgRequestRejected"), True)
                    LoadData(GetSourceEmployeeID())
            End Select
        Catch ex As Exception
            ShowMessage(GetRes("MsgErrorPrefix") & ex.Message, False)
        End Try
    End Sub

    Protected Sub btnCancelSelectedOpen_Click(ByVal sender As Object, ByVal e As EventArgs)
        ProcessSelectedOpenRequests(False)
    End Sub

    Protected Sub btnRejectCancelSelectedOpen_Click(ByVal sender As Object, ByVal e As EventArgs)
        ProcessSelectedOpenRequests(True)
    End Sub

    Protected Sub btnRefreshOpenRequests_Click(ByVal sender As Object, ByVal e As EventArgs)
        LoadData(GetSourceEmployeeID())
    End Sub

    Private Sub ProcessSelectedOpenRequests(ByVal rejectMode As Boolean)
        Try
            ClearMessage()
            Dim count As Integer = 0
            For Each row As GridViewRow In grdSubmittedOpen.Rows
                Dim chk As CheckBox = TryCast(row.FindControl("chkSelect"), CheckBox)
                If chk Is Nothing OrElse Not chk.Checked Then Continue For

                Dim requestSerial As Integer = Convert.ToInt32(grdSubmittedOpen.DataKeys(row.RowIndex).Values("RequestSerial"))
                Dim formCode As String = Convert.ToString(grdSubmittedOpen.DataKeys(row.RowIndex).Values("FormCode"))
                CloseOpenRequest(requestSerial, formCode, rejectMode)
                count += 1
            Next

            If count = 0 Then
                ShowMessage(GetRes("MsgNoRequestSelected"), False)
                Return
            End If

            ShowMessage(String.Format(If(rejectMode, GetRes("MsgBulkRejectSuccess"), GetRes("MsgBulkCancelSuccess")), count), True)
            LoadData(GetSourceEmployeeID())
        Catch ex As Exception
            ShowMessage(GetRes("MsgErrorPrefix") & ex.Message, False)
        End Try
    End Sub

    ' rejectMode=False → Cancel (ActionID=4, status=5); True → Reject (ActionID=2, status=2)
    Private Sub CloseOpenRequest(ByVal requestSerial As Integer, ByVal formCode As String, ByVal rejectMode As Boolean)
        Dim actorID As Integer = GetCurrentUserEmployeeID()
        If actorID <= 0 Then actorID = GetSourceEmployeeID()

        Dim sourceEmployeeID As Integer = GetSourceEmployeeID()
        Dim remarks As String = If(txtRemarks.Text.Trim() = "", "End of Service clearance", txtRemarks.Text.Trim())
        Dim actionID As Integer = If(rejectMode, 2, 4)
        Dim statusID As Integer = If(rejectMode, 2, 5)

        Using conn As New SqlConnection(ClsEmployees.ConnectionString)
            conn.Open()
            Using tran As SqlTransaction = conn.BeginTransaction()
                Try
                    Dim alreadyClosed As Integer = 0
                    Using cmdCheck As New SqlCommand(
                        "SELECT COUNT(1) FROM SS_RequestActions WHERE RequestSerial=@RequestSerial AND FormCode=@FormCode AND ActionID IN (2,4)",
                        conn, tran)
                        cmdCheck.Parameters.AddWithValue("@RequestSerial", requestSerial)
                        cmdCheck.Parameters.AddWithValue("@FormCode", formCode)
                        alreadyClosed = Convert.ToInt32(cmdCheck.ExecuteScalar())
                    End Using
                    If alreadyClosed > 0 Then
                        tran.Commit()
                        Return
                    End If

                    If rejectMode Then
                        Using cmd As New SqlCommand(
                            "UPDATE SS_RequestActions SET Seen=1, ActionID=2, ActionDate=GETDATE(), ActionRemarks=@Remarks " &
                            "WHERE RequestSerial=@RequestSerial AND FormCode=@FormCode AND ActionID IS NULL AND IsHidden IS NULL",
                            conn, tran)
                            cmd.Parameters.AddWithValue("@Remarks", remarks)
                            cmd.Parameters.AddWithValue("@RequestSerial", requestSerial)
                            cmd.Parameters.AddWithValue("@FormCode", formCode)
                            cmd.ExecuteNonQuery()
                        End Using
                    Else
                        Using cmd As New SqlCommand(
                            "INSERT INTO SS_RequestActions (RequestSerial, SS_EmployeeID, FormCode, EmployeeID, Seen, ActionID, ActionDate, ActionRemarks) " &
                            "VALUES (@RequestSerial, @ActorID, @FormCode, @EmployeeID, 1, 4, GETDATE(), @Remarks)",
                            conn, tran)
                            cmd.Parameters.AddWithValue("@RequestSerial", requestSerial)
                            cmd.Parameters.AddWithValue("@ActorID", actorID)
                            cmd.Parameters.AddWithValue("@FormCode", formCode)
                            cmd.Parameters.AddWithValue("@EmployeeID", sourceEmployeeID)
                            cmd.Parameters.AddWithValue("@Remarks", remarks)
                            cmd.ExecuteNonQuery()
                        End Using

                        Using cmd As New SqlCommand(
                            "UPDATE SS_RequestActions SET Seen=1, IsHidden=1 " &
                            "WHERE RequestSerial=@RequestSerial AND FormCode=@FormCode AND ActionID IS NULL AND IsHidden IS NULL",
                            conn, tran)
                            cmd.Parameters.AddWithValue("@RequestSerial", requestSerial)
                            cmd.Parameters.AddWithValue("@FormCode", formCode)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If

                    Dim headerTable As String = GetRequestHeaderTable(formCode)
                    If headerTable <> "" Then
                        Using cmd As New SqlCommand(
                            "UPDATE " & headerTable & " SET RequestStautsTypeID=@StatusID WHERE ID=@RequestSerial",
                            conn, tran)
                            cmd.Parameters.AddWithValue("@StatusID", statusID)
                            cmd.Parameters.AddWithValue("@RequestSerial", requestSerial)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If

                    tran.Commit()
                Catch
                    tran.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Sub

    Private Function GetCurrentUserEmployeeID() As Integer
        Try
            Dim userId As String = String.Empty
            Dim webHandler As New Venus.Shared.Web.WebHandler
            webHandler.GetCookies(Page, "UserID", userId)
            Dim sysUser As New Clssys_Users(Page)
            If sysUser.Find("ID='" & userId.Replace("'", "''") & "'") Then
                If ClsEmployees.Find("Code='" & Convert.ToString(sysUser.Code).Replace("'", "''") & "'") Then
                    Return ClsEmployees.ID
                End If
            End If
        Catch
        End Try
        Return 0
    End Function

    Private Function GetRequestHeaderTable(ByVal formCode As String) As String
        Select Case formCode
            Case "SS_0011", "SS_0012", "SS_0013", "SS_0018", "SS_0030", "SS_0031", "SS_0032", "SS_0033", "SS_0034", "SS_0035", "SS_0036", "SS_0037"
                Return "SS_VacationRequest"
            Case "SS_0014"
                Return "SS_ExecuseRequest"
            Case "SS_0015", "SS_0019"
                Return "SS_EndOfServiceRequest"
            Case "SS_00191"
                Return "SS_ExitEntryRequest"
            Case "SS_00192"
                Return "SS_VisaRequest"
            Case "SS_00193"
                Return "SS_LoanLetterRequest"
            Case "SS_00194"
                Return "SS_OtherLetterRequest"
            Case "SS_00195"
                Return "SS_TrainingRequest"
            Case "SS_00196"
                Return "SS_GrievanceFormRequest"
            Case "SS_00197"
                Return "SS_InterviewEvaluationFormRequest"
            Case "SS_00198"
                Return "SS_AssaultEscalationFormRequest"
            Case "SS_00199"
                Return "SS_ConflictofInterestFormRequest"
            Case "SS_001910"
                Return "SS_PhysiciansPrivilegingFormRequest"
            Case "SS_001911"
                Return "SS_DaycareSupportReaquest"
            Case "SS_001912"
                Return "SS_EducationSupportRequest"
            Case "SS_001913"
                Return "SS_AdvanceHousingRequest"
            Case "SS_001914"
                Return "SS_AdvanceSalaryRequest"
            Case "SS_001915"
                Return "SS_ChamberofCommerceLetterRequest"
            Case "SS_001916"
                Return "SS_SCFHSLetterRequest"
            Case "SS_001917"
                Return "SS_PaySlipRequest"
            Case "SS_001918"
                Return "SS_OccurrenceVarianceReportingLetters"
            Case "SS_001919"
                Return "SS_OvertimeRequest"
            Case "SS_001920"
                Return "SS_EducationFeesCompensationApplication"
            Case "SS_001921"
                Return "SS_BankAccountUpdate"
            Case "SS_001922"
                Return "SS_ContactInformationUpdate"
            Case "SS_001923"
                Return "SS_DependentsInformationUpdate"
            Case "SS_001924"
                Return "SS_MedicalInsuranceAdjustments"
            Case "SS_001925"
                Return "SS_OtherLegalDocumentUpdates"
            Case "SS_001926"
                Return "SS_EmployeeFileUpdate"
            Case "SS_001927"
                Return "SS_BusinessORTrainingTravel"
            Case "SS_001928"
                Return "SS_AnnualTicketRelatedRequests"
            Case "SS_001929"
                Return "SS_ChangeWorkHoursRequest"
            Case Else
                Return ""
        End Select
    End Function

    Private Function GetStatusPageUrl(ByVal formCode As String, ByVal requestSerial As Integer) As String
        Dim pageName As String = ""
        Select Case formCode
            Case "SS_0011", "SS_0012", "SS_0013", "SS_0018", "SS_0030", "SS_0031", "SS_0032", "SS_0033", "SS_0034", "SS_0035", "SS_0036", "SS_0037"
                pageName = "frmAnnualVacationsRequestStatus.aspx"
            Case "SS_0014"
                pageName = "frmExecuseRequestStatus.aspx"
            Case "SS_0015", "SS_0019"
                pageName = "frmEndServiceRequestStatus.aspx"
            Case "SS_00191"
                pageName = "frmExitEntryRequestStatus.aspx"
            Case "SS_00192"
                pageName = "frmVisaRequestStatus.aspx"
            Case "SS_00193"
                pageName = "FrmLoanLetterRequestStatus.aspx"
            Case "SS_00194"
                pageName = "FrmOtherLetterRequestStatus.aspx"
            Case "SS_00195"
                pageName = "FrmTrainingRequestStatus.aspx"
            Case "SS_00196"
                pageName = "FrmGrievanceFormRequestStatus.aspx"
            Case "SS_00197"
                pageName = "FrmInterviewEvaluationFormRequestStatus.aspx"
            Case "SS_00198"
                pageName = "FrmAssaultEscalationFormRequestStatus.aspx"
            Case "SS_00199"
                pageName = "FrmConflictofInterestFormRequestStatus.aspx"
            Case "SS_001910"
                pageName = "FrmPhysiciansPrivilegingFormRequestStatus.aspx"
            Case "SS_001911"
                pageName = "FrmDaycareSupportReaquestStatus.aspx"
            Case "SS_001912"
                pageName = "FrmEducationSupportRequestStatus.aspx"
            Case "SS_001913"
                pageName = "FrmAdvanceHousingRequestStatus.aspx"
            Case "SS_001914"
                pageName = "frmAdvanceSalaryRequestStatus.aspx"
            Case "SS_001915"
                pageName = "FrmChamberofCommerceLetterRequestStatus.aspx"
            Case "SS_001916"
                pageName = "frmSCFHSLetterRequestStatus.aspx"
            Case "SS_001917"
                pageName = "FrmPaySlipRequestStatus.aspx"
            Case "SS_001918"
                pageName = "frmOccurrenceVarianceReportingLettersStatus.aspx"
            Case "SS_001919"
                pageName = "frmOvertimeRequestStatus.aspx"
            Case "SS_001920"
                pageName = "frmEducationFeesCompensationApplicationStatus.aspx"
            Case "SS_001921"
                pageName = "frmBankAccountUpdateStatus.aspx"
            Case "SS_001922"
                pageName = "frmContactInformationUpdateStatus.aspx"
            Case "SS_001923"
                pageName = "frmDependentsInformationUpdateStatus.aspx"
            Case "SS_001924"
                pageName = "frmMedicalInsuranceAdjustmentsStatus.aspx"
            Case "SS_001925"
                pageName = "frmOtherLegalDocumentUpdatesStatus.aspx"
            Case "SS_001926"
                pageName = "frmEmployeeFileUpdateStatus.aspx"
            Case "SS_001927"
                pageName = "frmBusinessORTrainingTravelStatus.aspx"
            Case "SS_001928"
                pageName = "frmAnnualTicketRelatedRequestsStatus.aspx"
            Case "SS_001929"
                pageName = "frmChangeWorkHoursRequestStatus.aspx"
        End Select

        If pageName = "" Then Return ""
        Return pageName & "?RequestSerial=" & requestSerial & "&Type=2&FormCode=" & formCode & "&CanBeCanceled=true"
    End Function

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
