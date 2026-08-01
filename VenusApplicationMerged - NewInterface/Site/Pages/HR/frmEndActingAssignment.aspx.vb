Imports System.Data
Imports System.Data.SqlClient
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class frmEndActingAssignment
    Inherits MainPage

    Private ClsEmployees As Clshrs_Employees

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ClsEmployees = New Clshrs_Employees(Page)
            ApplyLayoutDirection()
            SetupSearchButtons()
            SetupConfirmScript()
            ApplyAssignPanelState()

            If Not IsPostBack Then
                Page.Session("ConnectionString") = ClsEmployees.ConnectionString
                txtEffectiveTo.Value = Date.Today
                txtRemarks.Text = GetRes("txtRemarksDefault")

                Dim sourceID As Integer = 0
                If Request.QueryString("SourceID") IsNot Nothing Then
                    Integer.TryParse(Request.QueryString("SourceID"), sourceID)
                End If
                If sourceID > 0 Then
                    LoadAssignmentBySourceID(sourceID)
                End If
            End If
        Catch ex As Exception
            ShowMessage(GetRes("MsgErrorPrefix") & ex.Message, False)
        End Try
    End Sub

    Private Function GetRes(ByVal key As String) As String
        Dim value As Object = GetLocalResourceObject(key)
        If value Is Nothing Then Return key
        Return value.ToString()
    End Function

    Private Sub ApplyLayoutDirection()
        Dim isArabic As Boolean = IsArabicLanguage()
        pageBody.Attributes("dir") = If(isArabic, "rtl", "ltr")
        pageBody.Style("text-align") = If(isArabic, "right", "left")
    End Sub

    Private Function IsArabicLanguage() As Boolean
        Return String.Equals(ProfileCls.CurrentLanguage, "Ar", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Sub SetupConfirmScript()
        btnEndAssignment.OnClientClick = "return confirm('" & GetRes("MsgConfirmEnd").Replace("'", "\'") & "');"
    End Sub

    Private Sub SetupSearchButtons()
        Try
            Dim objects As New Clssys_Objects(Page)
            Dim searches As New Clssys_Searchs(Page)
            Page.Session("ConnectionString") = ClsEmployees.ConnectionString

            btnSearchAssignment.OnClientClick =
                "OpenModal1('frmEndActingAssignmentSearch.aspx',520,780,false,'" & txtAssignmentCode.ClientID & "'); return false;"

            ' Employees
            objects.Find(" Code='" & ClsEmployees.Table.Trim() & "'")
            searches.Find(" ObjectID=" & objects.ID)
            Dim empSearchId As Integer = searches.ID

            btnSearchApprovalEmp.OnClientClick =
                "OpenModal1('frmModalSearchScreen.aspx?TargetControl=" & txtApprovalEmpCode.ID &
                "&SearchID=" & empSearchId & "&',510,720,false,'" & txtApprovalEmpCode.ClientID & "'); return false;"

            btnSearchNewManager.OnClientClick =
                "OpenModal1('frmModalSearchScreen.aspx?TargetControl=" & txtNewManagerCode.ID &
                "&SearchID=" & empSearchId & "&',510,720,false,'" & txtNewManagerCode.ClientID & "'); return false;"

            ' Positions
            objects.Find(" Code='hrs_Positions'")
            searches.Find(" ObjectID=" & objects.ID)
            btnSearchPosition.OnClientClick =
                "OpenModal1('frmModalSearchScreen.aspx?TargetControl=" & txtPositionCode.ID &
                "&SearchID=" & searches.ID & "&',510,720,false,'" & txtPositionCode.ClientID & "'); return false;"
        Catch
            btnSearchAssignment.Visible = False
            btnSearchApprovalEmp.Visible = False
            btnSearchNewManager.Visible = False
            btnSearchPosition.Visible = False
        End Try
    End Sub

    Private Sub ApplyAssignPanelState()
        txtApprovalEmpCode.Enabled = chkAssignEmployee.Checked
        btnSearchApprovalEmp.Enabled = chkAssignEmployee.Checked
        txtPositionCode.Enabled = chkAssignPosition.Checked
        btnSearchPosition.Enabled = chkAssignPosition.Checked
    End Sub

    Protected Sub txtAssignmentCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ClearMessage()
            Dim raw As String = txtAssignmentCode.Text.Trim()
            If raw = "" Then
                ClearDetails()
                Return
            End If

            Dim sourceID As Integer = 0
            Dim displayText As String = raw
            If raw.Contains("|") Then
                Dim parts() As String = raw.Split(New Char() {"|"c}, 2)
                Integer.TryParse(parts(0).Trim(), sourceID)
                If parts.Length > 1 Then displayText = parts(1).Trim()
            Else
                Integer.TryParse(raw.Split("-"c)(0).Trim(), sourceID)
            End If

            If sourceID <= 0 Then
                ShowMessage(GetRes("MsgSelectAssignment"), False)
                Return
            End If

            txtAssignmentCode.Text = displayText
            LoadAssignmentBySourceID(sourceID)
        Catch ex As Exception
            ShowMessage(GetRes("MsgErrorPrefix") & ex.Message, False)
        End Try
    End Sub

    Protected Sub chkAssignTarget_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ApplyAssignPanelState()
    End Sub

    Protected Sub txtApprovalEmpCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ResolveEmployeeCode(txtApprovalEmpCode, lblApprovalEmpName, hdnApprovalEmployeeID)
    End Sub

    Protected Sub txtNewManagerCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ResolveEmployeeCode(txtNewManagerCode, lblNewManagerName, hdnNewManagerID)
    End Sub

    Protected Sub txtPositionCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ResolvePositionCode(txtPositionCode, lblPositionName, hdnPositionID)
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ClearSelection()
    End Sub

    Protected Sub btnEndAssignment_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ClearMessage()

            Dim employeeAssignmentID As Integer = 0
            Dim positionAssignmentID As Integer = 0
            Dim sourceID As Integer = 0
            Integer.TryParse(hdnEmployeeAssignmentID.Value, employeeAssignmentID)
            Integer.TryParse(hdnPositionAssignmentID.Value, positionAssignmentID)
            Integer.TryParse(hdnSourceID.Value, sourceID)

            If sourceID <= 0 OrElse (employeeAssignmentID <= 0 AndAlso positionAssignmentID <= 0) Then
                ShowMessage(GetRes("MsgSelectAssignment"), False)
                Return
            End If

            Dim effectiveTo As Date
            If Convert.ToString(txtEffectiveTo.Value) = "" Then
                ShowMessage(GetRes("MsgEffectiveToRequired"), False)
                Return
            End If
            effectiveTo = CDate(txtEffectiveTo.Value).Date

            Dim effectiveFrom As Date
            If Not Date.TryParseExact(lblEffectiveFrom.Text, "dd/MM/yyyy",
                                      System.Globalization.CultureInfo.InvariantCulture,
                                      System.Globalization.DateTimeStyles.None, effectiveFrom) Then
                Date.TryParse(lblEffectiveFrom.Text, effectiveFrom)
            End If
            If effectiveFrom <> Nothing AndAlso effectiveTo < effectiveFrom.Date Then
                ShowMessage(GetRes("MsgEffectiveToInvalid"), False)
                Return
            End If

            Dim approvalEmployeeID As Integer = 0
            Dim positionID As Integer = 0
            Dim newManagerID As Integer = 0
            Integer.TryParse(hdnApprovalEmployeeID.Value, approvalEmployeeID)
            Integer.TryParse(hdnPositionID.Value, positionID)
            Integer.TryParse(hdnNewManagerID.Value, newManagerID)

            If chkAssignEmployee.Checked AndAlso approvalEmployeeID <= 0 AndAlso txtApprovalEmpCode.Text.Trim() <> "" Then
                ResolveEmployeeCode(txtApprovalEmpCode, lblApprovalEmpName, hdnApprovalEmployeeID)
                Integer.TryParse(hdnApprovalEmployeeID.Value, approvalEmployeeID)
            End If
            If chkAssignPosition.Checked AndAlso positionID <= 0 AndAlso txtPositionCode.Text.Trim() <> "" Then
                ResolvePositionCode(txtPositionCode, lblPositionName, hdnPositionID)
                Integer.TryParse(hdnPositionID.Value, positionID)
            End If
            If newManagerID <= 0 AndAlso txtNewManagerCode.Text.Trim() <> "" Then
                ResolveEmployeeCode(txtNewManagerCode, lblNewManagerName, hdnNewManagerID)
                Integer.TryParse(hdnNewManagerID.Value, newManagerID)
            End If

            Dim hasApprovals As Boolean = (hdnHasApprovals.Value = "1")
            If hasApprovals Then
                If Not chkAssignEmployee.Checked AndAlso Not chkAssignPosition.Checked Then
                    ShowMessage(GetRes("MsgSelectApprovalTarget"), False)
                    Return
                End If
                If chkAssignEmployee.Checked AndAlso approvalEmployeeID <= 0 Then
                    ShowMessage(GetRes("MsgEnterApprovalEmployee"), False)
                    Return
                End If
                If chkAssignPosition.Checked AndAlso positionID <= 0 Then
                    ShowMessage(GetRes("MsgEnterPosition"), False)
                    Return
                End If
            End If

            Dim actingEmployeeID As Integer = 0
            Integer.TryParse(hdnActingEmployeeID.Value, actingEmployeeID)
            Dim hasDirectManager As Boolean = (hdnHasDirectManager.Value = "1")
            If hasDirectManager AndAlso newManagerID <= 0 Then
                ShowMessage(GetRes("MsgEnterNewManager"), False)
                Return
            End If

            If approvalEmployeeID > 0 AndAlso approvalEmployeeID = actingEmployeeID Then
                ShowMessage(GetRes("MsgCannotAssignSame"), False)
                Return
            End If
            If newManagerID > 0 AndAlso newManagerID = actingEmployeeID Then
                ShowMessage(GetRes("MsgCannotAssignSame"), False)
                Return
            End If

            Dim updatedConfig As Integer = 0
            Dim updatedManagers As Integer = 0
            Dim remarks As String = If(txtRemarks.Text.Trim() = "", GetRes("txtRemarksDefault"), txtRemarks.Text.Trim())

            Using conn As New SqlConnection(ClsEmployees.ConnectionString)
                conn.Open()
                Using tran As SqlTransaction = conn.BeginTransaction()
                    Try
                        CloseActingAssignmentsBySource(conn, tran, sourceID, employeeAssignmentID, positionAssignmentID, effectiveTo, remarks)

                        If actingEmployeeID > 0 AndAlso (chkAssignEmployee.Checked OrElse chkAssignPosition.Checked) Then
                            updatedConfig = ReassignApprovals(conn, tran, actingEmployeeID, approvalEmployeeID, positionID)
                        End If

                        If newManagerID > 0 AndAlso actingEmployeeID > 0 Then
                            updatedManagers = UpdateSubordinatesManager(conn, tran, actingEmployeeID, newManagerID)
                        End If

                        tran.Commit()
                    Catch
                        tran.Rollback()
                        Throw
                    End Try
                End Using
            End Using

            ShowMessage(String.Format(GetRes("MsgEndSuccess"), updatedConfig, updatedManagers), True)
            LoadAssignmentBySourceID(sourceID)

        Catch ex As Exception
            ShowMessage(GetRes("MsgEndFailed") & ex.Message, False)
        End Try
    End Sub

    Private Sub CloseActingAssignmentsBySource(ByVal conn As SqlConnection, ByVal tran As SqlTransaction,
                                               ByVal sourceID As Integer,
                                               ByVal employeeAssignmentID As Integer,
                                               ByVal positionAssignmentID As Integer,
                                               ByVal effectiveTo As Date, ByVal remarks As String)
        Dim affected As Integer = 0
        Using cmd As New SqlCommand(
            "UPDATE hrs_ActingEmployeeAssignments SET EffectiveTo=@EffectiveTo, CancelDate=GETDATE()," &
            " CancelUserID=@CancelUserID, CancelReason=@CancelReason" &
            " WHERE SourceID=@SourceID AND CancelDate IS NULL", conn, tran)
            cmd.Parameters.AddWithValue("@EffectiveTo", effectiveTo)
            cmd.Parameters.AddWithValue("@CancelUserID", GetCurrentUserID())
            cmd.Parameters.AddWithValue("@CancelReason", remarks)
            cmd.Parameters.AddWithValue("@SourceID", sourceID)
            affected += cmd.ExecuteNonQuery()
        End Using
        Using cmd As New SqlCommand(
            "UPDATE hrs_ActingPositionAssignments SET EffectiveTo=@EffectiveTo, CancelDate=GETDATE()," &
            " CancelUserID=@CancelUserID, CancelReason=@CancelReason" &
            " WHERE SourceID=@SourceID AND CancelDate IS NULL", conn, tran)
            cmd.Parameters.AddWithValue("@EffectiveTo", effectiveTo)
            cmd.Parameters.AddWithValue("@CancelUserID", GetCurrentUserID())
            cmd.Parameters.AddWithValue("@CancelReason", remarks)
            cmd.Parameters.AddWithValue("@SourceID", sourceID)
            affected += cmd.ExecuteNonQuery()
        End Using

        If affected = 0 Then
            Throw New Exception(GetRes("MsgAssignmentNotActive"))
        End If
    End Sub

    Private Function ReassignApprovals(ByVal conn As SqlConnection, ByVal tran As SqlTransaction,
                                       ByVal actingEmployeeID As Integer,
                                       ByVal approvalEmployeeID As Integer,
                                       ByVal positionID As Integer) As Integer
        Dim sql As String
        If chkAssignEmployee.Checked AndAlso chkAssignPosition.Checked Then
            sql = "UPDATE SS_Configuration SET EmployeeID=@EmployeeID, PositionID=@PositionID WHERE EmployeeID=@ActingEmployeeID"
        ElseIf chkAssignEmployee.Checked Then
            sql = "UPDATE SS_Configuration SET EmployeeID=@EmployeeID WHERE EmployeeID=@ActingEmployeeID"
        Else
            sql = "UPDATE SS_Configuration SET EmployeeID=NULL, PositionID=@PositionID WHERE EmployeeID=@ActingEmployeeID"
        End If

        Using cmd As New SqlCommand(sql, conn, tran)
            cmd.Parameters.AddWithValue("@ActingEmployeeID", actingEmployeeID)
            If chkAssignEmployee.Checked Then
                cmd.Parameters.AddWithValue("@EmployeeID", approvalEmployeeID)
            End If
            If chkAssignPosition.Checked Then
                cmd.Parameters.AddWithValue("@PositionID", positionID)
            End If
            Return cmd.ExecuteNonQuery()
        End Using
    End Function

    Private Function UpdateSubordinatesManager(ByVal conn As SqlConnection, ByVal tran As SqlTransaction,
                                              ByVal actingEmployeeID As Integer, ByVal newManagerID As Integer) As Integer
        Using cmd As New SqlCommand(
            "UPDATE hrs_Employees " &
            "SET ManagerID = @NewManagerID " &
            "WHERE ManagerID = @ActingEmployeeID " &
            "AND CancelDate IS NULL " &
            "AND ExcludeDate IS NULL " &
            "AND ID <> @NewManagerID", conn, tran)
            cmd.Parameters.AddWithValue("@NewManagerID", newManagerID)
            cmd.Parameters.AddWithValue("@ActingEmployeeID", actingEmployeeID)
            Return cmd.ExecuteNonQuery()
        End Using
    End Function

    Private Sub LoadAssignmentBySourceID(ByVal sourceID As Integer)
        ClearDetails()
        If sourceID <= 0 Then
            ShowMessage(GetRes("MsgSelectAssignment"), False)
            Return
        End If

        hdnSourceID.Value = sourceID.ToString()

        Dim nameFn As String = If(IsArabicLanguage(), "dbo.fn_GetEmpName(E.Code,1)", "dbo.fn_GetEmpName(E.Code,0)")
        Dim originalNameFn As String = If(IsArabicLanguage(), "dbo.fn_GetEmpName(O.Code,1)", "dbo.fn_GetEmpName(O.Code,0)")
        Dim branchName As String = If(IsArabicLanguage(), "ISNULL(B.ArbName,B.EngName)", "ISNULL(B.EngName,B.ArbName)")
        Dim positionName As String = If(IsArabicLanguage(), "ISNULL(P.ArbName,P.EngName)", "ISNULL(P.EngName,P.ArbName)")

        Dim sqlEmp As String =
            "SELECT TOP 1 A.ID, A.Code, A.OriginalEmployeeID, A.ActingEmployeeID, A.EffectiveFrom, A.EffectiveTo, A.CancelDate, A.Remarks," &
            " O.Code AS OriginalCode, ISNULL(" & originalNameFn & ",'') AS OriginalName," &
            " E.Code AS ActingCode, ISNULL(" & nameFn & ",'') AS ActingName," &
            " ISNULL(" & branchName & ",'') AS BranchName" &
            " FROM hrs_ActingEmployeeAssignments A" &
            " INNER JOIN hrs_Employees O ON O.ID=A.OriginalEmployeeID" &
            " INNER JOIN hrs_Employees E ON E.ID=A.ActingEmployeeID" &
            " LEFT JOIN sys_Branches B ON B.ID=E.BranchID" &
            " WHERE A.SourceID=@SourceID" &
            " ORDER BY CASE WHEN A.CancelDate IS NULL THEN 0 ELSE 1 END, A.ID DESC"

        Dim sqlPos As String =
            "SELECT TOP 1 A.ID, A.Code, A.OriginalPositionID, A.ActingEmployeeID, A.EffectiveFrom, A.EffectiveTo, A.CancelDate, A.Remarks," &
            " E.Code AS ActingCode, ISNULL(" & nameFn & ",'') AS ActingName," &
            " ISNULL(" & positionName & ",'') AS PositionName," &
            " ISNULL(" & branchName & ",'') AS BranchName" &
            " FROM hrs_ActingPositionAssignments A" &
            " INNER JOIN hrs_Employees E ON E.ID=A.ActingEmployeeID" &
            " INNER JOIN hrs_Positions P ON P.ID=A.OriginalPositionID" &
            " LEFT JOIN sys_Branches B ON B.ID=E.BranchID" &
            " WHERE A.SourceID=@SourceID" &
            " ORDER BY CASE WHEN A.CancelDate IS NULL THEN 0 ELSE 1 END, A.ID DESC"

        Dim dtEmp As DataTable = ExecuteQueryBySource(sqlEmp, sourceID)
        Dim dtPos As DataTable = ExecuteQueryBySource(sqlPos, sourceID)

        If dtEmp.Rows.Count = 0 AndAlso dtPos.Rows.Count = 0 Then
            ShowMessage(GetRes("MsgAssignmentNotFound"), False)
            Return
        End If

        Dim empRow As DataRow = If(dtEmp.Rows.Count > 0, dtEmp.Rows(0), Nothing)
        Dim posRow As DataRow = If(dtPos.Rows.Count > 0, dtPos.Rows(0), Nothing)

        Dim employeeAssignmentID As Integer = If(empRow Is Nothing, 0, Convert.ToInt32(empRow("ID")))
        Dim positionAssignmentID As Integer = If(posRow Is Nothing, 0, Convert.ToInt32(posRow("ID")))
        hdnEmployeeAssignmentID.Value = employeeAssignmentID.ToString()
        hdnPositionAssignmentID.Value = positionAssignmentID.ToString()
        hdnAssignmentID.Value = If(employeeAssignmentID > 0, employeeAssignmentID, positionAssignmentID).ToString()
        hdnAssignmentType.Value = If(employeeAssignmentID > 0 AndAlso positionAssignmentID > 0, "B",
                                     If(employeeAssignmentID > 0, "E", "P"))

        Dim assignmentNo As String = ""
        Dim originalEmployee As String = "—"
        Dim actingEmployee As String = "—"
        Dim positionGrade As String = "—"
        Dim branch As String = "—"
        Dim effectiveFrom As Date = Date.Today
        Dim effectiveTo As Date = Date.Today
        Dim cancelDate As Object = DBNull.Value
        Dim remarks As String = ""
        Dim actingId As Integer = 0
        Dim originalEmpId As Integer = 0
        Dim originalPosId As Integer = 0

        If empRow IsNot Nothing Then
            assignmentNo = Convert.ToString(empRow("Code"))
            originalEmployee = Convert.ToString(empRow("OriginalCode")) & " - " & Convert.ToString(empRow("OriginalName"))
            actingEmployee = Convert.ToString(empRow("ActingCode")) & " - " & Convert.ToString(empRow("ActingName"))
            branch = Convert.ToString(empRow("BranchName"))
            effectiveFrom = Convert.ToDateTime(empRow("EffectiveFrom"))
            effectiveTo = Convert.ToDateTime(empRow("EffectiveTo"))
            cancelDate = empRow("CancelDate")
            remarks = Convert.ToString(empRow("Remarks"))
            actingId = Convert.ToInt32(empRow("ActingEmployeeID"))
            originalEmpId = Convert.ToInt32(empRow("OriginalEmployeeID"))
        End If

        If posRow IsNot Nothing Then
            If assignmentNo = "" Then
                assignmentNo = Convert.ToString(posRow("Code"))
            ElseIf Convert.ToString(posRow("Code")) <> "" AndAlso Convert.ToString(posRow("Code")) <> assignmentNo Then
                assignmentNo &= " / " & Convert.ToString(posRow("Code"))
            End If
            positionGrade = Convert.ToString(posRow("PositionName"))
            If actingEmployee = "—" Then
                actingEmployee = Convert.ToString(posRow("ActingCode")) & " - " & Convert.ToString(posRow("ActingName"))
            End If
            If branch = "—" OrElse branch = "" Then
                branch = Convert.ToString(posRow("BranchName"))
            End If
            If empRow Is Nothing Then
                effectiveFrom = Convert.ToDateTime(posRow("EffectiveFrom"))
                effectiveTo = Convert.ToDateTime(posRow("EffectiveTo"))
                cancelDate = posRow("CancelDate")
                remarks = Convert.ToString(posRow("Remarks"))
                actingId = Convert.ToInt32(posRow("ActingEmployeeID"))
            End If
            originalPosId = Convert.ToInt32(posRow("OriginalPositionID"))
            ' Prefer active cancel status from either open record
            If Not IsDBNull(posRow("CancelDate")) AndAlso Not IsDBNull(cancelDate) Then
                ' both closed
            ElseIf IsDBNull(posRow("CancelDate")) Then
                cancelDate = DBNull.Value
                If Convert.ToDateTime(posRow("EffectiveTo")) < effectiveTo Then
                    effectiveTo = Convert.ToDateTime(posRow("EffectiveTo"))
                End If
            End If
        End If

        ' If no position acting, resolve position from original employee contract
        If positionGrade = "—" AndAlso originalEmpId > 0 Then
            positionGrade = GetEmployeePositionName(originalEmpId)
            originalPosId = GetEmployeePositionID(originalEmpId)
        End If

        hdnOriginalEmployeeID.Value = originalEmpId.ToString()
        hdnActingEmployeeID.Value = actingId.ToString()
        hdnOriginalPositionID.Value = originalPosId.ToString()

        Dim hasApprovals As Boolean = actingId > 0 AndAlso HasApprovalConfig(actingId)
        Dim hasManager As Boolean = actingId > 0 AndAlso GetSubordinateCount(actingId) > 0
        hdnHasApprovals.Value = If(hasApprovals, "1", "0")
        hdnHasDirectManager.Value = If(hasManager, "1", "0")

        Dim isActive As Boolean = IsDBNull(cancelDate) AndAlso effectiveTo.Date >= Date.Today
        Dim scope As String = BuildScopeText(hasApprovals, hasManager)
        Dim statusText As String = If(isActive, GetRes("lblStatusActive"), GetRes("lblStatusClosed"))

        Dim display As String = assignmentNo
        If positionGrade <> "—" AndAlso positionGrade <> "" Then display &= " - " & positionGrade
        If actingEmployee <> "—" Then
            Dim actingNameOnly As String = actingEmployee
            If actingNameOnly.Contains("-") Then
                actingNameOnly = actingNameOnly.Substring(actingNameOnly.IndexOf("-"c) + 1).Trim()
            End If
            display &= " - " & actingNameOnly
        End If
        If remarks <> "" AndAlso Not display.Contains(remarks) Then
            ' keep assignment display style from image (code - position - acting)
        End If
        If txtAssignmentCode.Text.Trim() = "" OrElse txtAssignmentCode.Text.Contains("|") Then
            txtAssignmentCode.Text = If(remarks <> "", sourceID.ToString() & " - " & remarks, display)
        End If

        lblAssignmentNo.Text = assignmentNo
        lblOriginalEmployee.Text = originalEmployee
        lblActingEmployee.Text = actingEmployee
        lblPositionGrade.Text = positionGrade
        lblBranch.Text = branch
        lblEffectiveFrom.Text = effectiveFrom.ToString("dd/MM/yyyy")
        lblScope.Text = scope
        lblStatus.Text = statusText
        lblStatus.CssClass = If(isActive, "status-active", "detail-value")

        lblSumBranch.Text = branch
        lblSumFrom.Text = lblEffectiveFrom.Text
        lblSumScope.Text = scope
        lblSumStatus.Text = statusText

        pnlDetails.Visible = True
        pnlReassign.Visible = isActive
        btnEndAssignment.Enabled = isActive

        If isActive Then
            If effectiveTo.Year < 2900 Then
                txtEffectiveTo.Value = effectiveTo
            Else
                txtEffectiveTo.Value = Date.Today
            End If
        Else
            ShowMessage(GetRes("MsgAssignmentAlreadyClosed"), False)
        End If
    End Sub

    Private Function ExecuteQueryBySource(ByVal sql As String, ByVal sourceID As Integer) As DataTable
        Dim dt As New DataTable()
        Using conn As New SqlConnection(ClsEmployees.ConnectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@SourceID", sourceID)
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Private Function GetEmployeePositionName(ByVal employeeID As Integer) As String
        Dim positionName As String = If(IsArabicLanguage(), "ISNULL(P.ArbName,P.EngName)", "ISNULL(P.EngName,P.ArbName)")
        Dim sql As String =
            "SELECT TOP 1 " & positionName & " AS PositionName" &
            " FROM hrs_Contracts C INNER JOIN hrs_Positions P ON P.ID=C.PositionID" &
            " WHERE C.EmployeeID=@EmployeeID AND C.PositionID IS NOT NULL AND C.CancelDate IS NULL" &
            " AND (C.EndDate IS NULL OR C.EndDate >= GETDATE()) ORDER BY C.ID DESC"
        Using conn As New SqlConnection(ClsEmployees.ConnectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID)
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result Is Nothing OrElse IsDBNull(result) Then Return "—"
                Return Convert.ToString(result)
            End Using
        End Using
    End Function

    Private Function GetEmployeePositionID(ByVal employeeID As Integer) As Integer
        Dim sql As String =
            "SELECT TOP 1 C.PositionID FROM hrs_Contracts C" &
            " WHERE C.EmployeeID=@EmployeeID AND C.PositionID IS NOT NULL AND C.CancelDate IS NULL" &
            " AND (C.EndDate IS NULL OR C.EndDate >= GETDATE()) ORDER BY C.ID DESC"
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

    Private Function BuildScopeText(ByVal hasApprovals As Boolean, ByVal hasManager As Boolean) As String
        If hasApprovals AndAlso hasManager Then Return GetRes("ScopeApprovalsAndManager")
        If hasApprovals Then Return GetRes("ScopeApprovals")
        If hasManager Then Return GetRes("ScopeDirectManager")
        Return GetRes("ScopeNone")
    End Function

    Private Function HasApprovalConfig(ByVal employeeID As Integer) As Boolean
        Dim sql As String = "SELECT COUNT(1) FROM SS_Configuration WHERE EmployeeID=@EmployeeID"
        Using conn As New SqlConnection(ClsEmployees.ConnectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID)
                conn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Private Function GetSubordinateCount(ByVal employeeID As Integer) As Integer
        Dim sql As String =
            "SELECT COUNT(1) FROM hrs_Employees" &
            " WHERE ManagerID=@EmployeeID AND CancelDate IS NULL AND ExcludeDate IS NULL"
        Using conn As New SqlConnection(ClsEmployees.ConnectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID)
                conn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Private Sub ResolveEmployeeCode(ByVal txtCode As TextBox, ByVal lblName As Label, ByVal hdnID As HiddenField)
        Try
            ClearMessage()
            lblName.Text = ""
            hdnID.Value = "0"

            Dim code As String = txtCode.Text.Trim()
            If code = "" Then Return

            Dim actingEmployeeID As Integer = 0
            Integer.TryParse(hdnActingEmployeeID.Value, actingEmployeeID)

            If ClsEmployees.Find("Code='" & code.Replace("'", "''") & "' AND CancelDate IS NULL AND ExcludeDate IS NULL") Then
                If ClsEmployees.ID = actingEmployeeID Then
                    ShowMessage(GetRes("MsgCannotAssignSame"), False)
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

    Private Sub ResolvePositionCode(ByVal txtCode As TextBox, ByVal lblName As Label, ByVal hdnID As HiddenField)
        Try
            ClearMessage()
            lblName.Text = ""
            hdnID.Value = "0"

            Dim code As String = txtCode.Text.Trim()
            If code = "" Then Return

            Dim position As New Clshrs_Positions(Page)
            If position.Find("Code='" & code.Replace("'", "''") & "'") Then
                txtCode.Text = position.Code
                lblName.Text = If(IsArabicLanguage(),
                                  If(String.IsNullOrEmpty(position.ArbName), position.EngName, position.ArbName),
                                  If(String.IsNullOrEmpty(position.EngName), position.ArbName, position.EngName))
                hdnID.Value = Convert.ToInt32(position.ID).ToString()
            Else
                ShowMessage(GetRes("MsgInvalidPosition"), False)
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, False)
        End Try
    End Sub

    Private Function GetCurrentUserID() As Integer
        Try
            Dim userId As String = String.Empty
            Dim webHandler As New Venus.Shared.Web.WebHandler
            webHandler.GetCookies(Page, "UserID", userId)
            Dim id As Integer = 0
            Integer.TryParse(userId, id)
            Return id
        Catch
            Return 0
        End Try
    End Function

    Private Sub ClearSelection()
        txtAssignmentCode.Text = ""
        ClearDetails()
        ClearMessage()
    End Sub

    Private Sub ClearDetails()
        hdnAssignmentID.Value = "0"
        hdnEmployeeAssignmentID.Value = "0"
        hdnPositionAssignmentID.Value = "0"
        hdnSourceID.Value = "0"
        hdnAssignmentType.Value = ""
        hdnOriginalEmployeeID.Value = "0"
        hdnActingEmployeeID.Value = "0"
        hdnOriginalPositionID.Value = "0"
        hdnHasApprovals.Value = "0"
        hdnHasDirectManager.Value = "0"

        lblAssignmentNo.Text = ""
        lblOriginalEmployee.Text = ""
        lblActingEmployee.Text = ""
        lblPositionGrade.Text = ""
        lblBranch.Text = ""
        lblEffectiveFrom.Text = ""
        lblScope.Text = ""
        lblStatus.Text = ""
        lblSumBranch.Text = ""
        lblSumFrom.Text = ""
        lblSumScope.Text = ""
        lblSumStatus.Text = ""

        pnlDetails.Visible = False
        pnlReassign.Visible = False
        btnEndAssignment.Enabled = False
    End Sub

    Private Sub ShowMessage(ByVal message As String, ByVal isSuccess As Boolean)
        lblMessage.Text = message
        lblMessage.CssClass = "msg show " & If(isSuccess, "msg-ok", "msg-err")
    End Sub

    Private Sub ClearMessage()
        lblMessage.Text = ""
        lblMessage.CssClass = "msg"
    End Sub

End Class
