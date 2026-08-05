Imports System.Data
Imports System.Data.SqlClient
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class frmActingEmployeeAssignment
    Inherits MainPage

    Private Assignment As Clshrs_ActingEmployeeAssignments

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Assignment = New Clshrs_ActingEmployeeAssignments(Page)
        Dim phaseName As String = "init"
        Try
            Dim isAr As Boolean = (ProfileCls.CurrentLanguage = "Ar")
            DIV.Style("direction") = If(isAr, "rtl", "ltr")
            DIV.Attributes("dir") = If(isAr, "rtl", "ltr")
            phaseName = "ConfigureSearches"
            ConfigureSearches()
            phaseName = "ApplyGridAlignment"
            ApplyGridAlignment()
            phaseName = "LocalizeFilterControls"
            LocalizeFilterControls()
            If Not IsPostBack Then
                Session("ConnectionString") = Assignment.ConnectionString
                UltraWebTab1.SelectedTab = 0
                txtSearch.Attributes("placeholder") = GetLocal("txtSearchPlaceholder", "Search employee or transaction...")
                phaseName = "ApplyScreenSettings"
                ApplyScreenSettings()
                If Val(Request.QueryString("ID")) > 0 AndAlso Assignment.Find("ID=" & Val(Request.QueryString("ID"))) Then
                    phaseName = "ShowRecord"
                    ShowRecord()
                Else
                    phaseName = "NewRecord"
                    NewRecord()
                End If
            End If
        Catch ex As Exception
            Try
                Dim logPath As String = Server.MapPath("~/App_Data/aea_error.txt")
                System.IO.File.WriteAllText(logPath, "Phase=" & phaseName & vbCrLf & ex.ToString())
            Catch
            End Try
            Session("ErrorValue") = ex
            Try
                Dim handler As New Venus.Shared.ErrorsHandler(If(Assignment Is Nothing, "", Assignment.ConnectionString))
                handler.RecordExceptions_DataBase("Phase=" & phaseName, ex, Err.Number, "", Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Catch
            End Try
            Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Protected Sub Toolbar_Command(ByVal sender As Object, ByVal e As CommandEventArgs) Handles _
        ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command,
        ImageButton_New.Command, ImageButton_Delete.Command, LinkButton_Delete.Command,
        ImageButton_Print.Command, LinkButton_PrintText.Command, ImageButton_Properties.Command, LinkButton_Properties.Command,
        ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_First.Command,
        ImageButton_Back.Command, ImageButton_Next.Command, ImageButton_Last.Command

        Assignment = New Clshrs_ActingEmployeeAssignments(Page)
        Try
            Select Case Convert.ToString(e.CommandArgument)
                Case "Save", "SaveNew"
                    If Not ValidateInput() Then Exit Sub
                    If Not SaveRecord() Then Exit Sub
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, Message("Save Done/تم الحفظ"))
                    If Convert.ToString(e.CommandArgument) = "SaveNew" Then NewRecord()
                Case "New"
                    NewRecord()
                Case "Delete"
                    If Assignment.Find("Code='" & SqlText(txtCode.Text) & "'") AndAlso Assignment.Cancel("ID=" & Assignment.ID) Then
                        Assignment.Find("Code='" & SqlText(txtCode.Text) & "'")
                        ShowRecord()
                        BindGrid()
                    End If
                Case "Print"
                    Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
                Case "Property"
                    OpenAuxiliary("frmPropertyScreen.aspx?ID={0}&TableName={1}", "Property", 477, 313)
                Case "Remarks"
                    OpenAuxiliary("frmRemarks.aspx?ID={0}&TableName={1}", "Remarks", 410, 210)
                Case "First"
                    If Assignment.FirstRecord() Then ShowRecord()
                Case "Previous"
                    LoadCurrent()
                    If Assignment.PreviousRecord() Then ShowRecord()
                Case "Next"
                    LoadCurrent()
                    If Assignment.NextRecord() Then ShowRecord()
                Case "Last"
                    If Assignment.LastRecord() Then ShowRecord()
            End Select
        Catch ex As Exception
            ReportError(ex)
        End Try
    End Sub

    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCode.TextChanged
        Assignment = New Clshrs_ActingEmployeeAssignments(Page)
        If Assignment.Find("Code='" & SqlText(txtCode.Text) & "'") Then
            ShowRecord()
        Else
            ClearFields(False)
        End If
    End Sub

    Protected Sub txtOriginalEmployee_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtOriginalEmployee.TextChanged
        Dim employee As New Clshrs_Employees(Page)
        If employee.Find("Code='" & SqlText(txtOriginalEmployee.Text) & "'") Then
            txtOriginalEmployeeName.Text = EmployeeName(txtOriginalEmployee.Text)
        Else
            txtOriginalEmployeeName.Text = ""
        End If
        RefreshDisplayFields()
    End Sub

    Protected Sub txtActingEmployee_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtActingEmployee.TextChanged
        Dim employee As New Clshrs_Employees(Page)
        If employee.Find("Code='" & SqlText(txtActingEmployee.Text) & "'") Then
            txtActingEmployeeName.Text = EmployeeName(txtActingEmployee.Text)
        Else
            txtActingEmployeeName.Text = ""
        End If
        RefreshDisplayFields()
    End Sub

    Protected Sub ddlStatusFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlStatusFilter.SelectedIndexChanged
        Assignment = New Clshrs_ActingEmployeeAssignments(Page)
        BindGrid()
    End Sub

    Protected Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSearch.TextChanged
        Assignment = New Clshrs_ActingEmployeeAssignments(Page)
        BindGrid()
    End Sub

    Protected Sub rptAssignments_ItemCommand(ByVal sender As Object, ByVal e As CommandEventArgs)
        If e.CommandName = "View" Then
            Assignment = New Clshrs_ActingEmployeeAssignments(Page)
            txtCode.Text = Convert.ToString(e.CommandArgument)
            If Assignment.Find("Code='" & SqlText(txtCode.Text) & "'") Then
                ShowRecord()
            End If
        End If
    End Sub

    Private Sub ConfigureSearches()
        Dim objects As New Clssys_Objects(Page)
        Dim searches As New Clssys_Searchs(Page)
        ConfigureSearch(objects, searches, "hrs_ActingEmployeeAssignments", txtCode, btnSearchCode)
        If Not ConfigureSearch(objects, searches, "V_ActiveEmployees", txtOriginalEmployee, btnOriginalEmployeeSearch) Then
            ConfigureSearch(objects, searches, "hrs_Employees", txtOriginalEmployee, btnOriginalEmployeeSearch)
        End If
        If Not ConfigureSearch(objects, searches, "V_ActiveEmployees", txtActingEmployee, btnActingEmployeeSearch) Then
            ConfigureSearch(objects, searches, "hrs_Employees", txtActingEmployee, btnActingEmployeeSearch)
        End If
    End Sub

    Private Function ConfigureSearch(ByVal objects As Clssys_Objects, ByVal searches As Clssys_Searchs,
                                     ByVal objectCode As String, ByVal target As TextBox,
                                     ByVal button As Infragistics.WebUI.WebDataInput.WebImageButton) As Boolean
        If objects.Find("Code='" & objectCode & "'") AndAlso searches.Find("ObjectID=" & objects.ID) Then
            Dim url As String = "'frmModalSearchScreen.aspx?TargetControl=" & target.ID &
                "&SearchID=" & searches.ID & "',510,720,false,'" & target.ClientID & "'"
            button.ClientSideEvents.Click = "OpenModal1(" & url & ")"
            Return True
        End If
        Return False
    End Function

    Private Function ValidateInput() As Boolean
        If txtCode.Text.Trim() = "" OrElse txtOriginalEmployee.Text.Trim() = "" OrElse
           txtActingEmployee.Text.Trim() = "" OrElse Convert.ToString(txtEffectiveFrom.Value) = "" OrElse
           Convert.ToString(txtEffectiveTo.Value) = "" Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, Message("Please complete all required fields/برجاء استكمال الحقول المطلوبة"))
            Return False
        End If
        If CDate(txtEffectiveFrom.Value).Date > CDate(txtEffectiveTo.Value).Date Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, Message("Acting End Date must be after Acting Start Date/يجب أن يكون تاريخ نهاية الإنابة بعد تاريخ بدايتها"))
            Return False
        End If
        If txtOriginalEmployee.Text.Trim() = txtActingEmployee.Text.Trim() Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, Message("Employee Being Replaced and Acting Employee must be different/يجب أن يختلف الموظف المُناب عنه عن الموظف القائم بالإنابة"))
            Return False
        End If
        Dim originalEmployee As New Clshrs_Employees(Page)
        Dim actingEmployee As New Clshrs_Employees(Page)
        If Not originalEmployee.Find("Code='" & SqlText(txtOriginalEmployee.Text) & "'") Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, Message("Employee Being Replaced is invalid/الموظف المُناب عنه غير صحيح"))
            Return False
        End If
        If Not actingEmployee.Find("Code='" & SqlText(txtActingEmployee.Text) & "'") Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, Message("Acting Employee is invalid/الموظف القائم بالإنابة غير صحيح"))
            Return False
        End If
        Return Not HasOverlap(actingEmployee.ID)
    End Function

    Private Function HasOverlap(ByVal employeeID As Integer) As Boolean
        Dim currentID As Integer = 0
        If Assignment.Find("Code='" & SqlText(txtCode.Text) & "'") Then currentID = Assignment.ID
        Dim sql As String = "SELECT COUNT(*) FROM hrs_ActingEmployeeAssignments WHERE CancelDate IS NULL" &
            " AND ActingEmployeeID=@EmployeeID AND ID<>@ID AND EffectiveFrom<=@EffectiveTo AND EffectiveTo>=@EffectiveFrom"
        Using connection As New SqlConnection(Assignment.ConnectionString)
            Using command As New SqlCommand(sql, connection)
                command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID
                command.Parameters.Add("@ID", SqlDbType.Int).Value = currentID
                command.Parameters.Add("@EffectiveFrom", SqlDbType.DateTime).Value = CDate(txtEffectiveFrom.Value).Date
                command.Parameters.Add("@EffectiveTo", SqlDbType.DateTime).Value = CDate(txtEffectiveTo.Value).Date
                connection.Open()
                If Convert.ToInt32(command.ExecuteScalar()) > 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, Message("This employee has an overlapping acting assignment/يوجد تكليف متداخل لنفس الموظف"))
                    Return True
                End If
            End Using
        End Using
        Return False
    End Function

    Private Function SaveRecord() As Boolean
        Dim originalEmployee As New Clshrs_Employees(Page)
        Dim actingEmployee As New Clshrs_Employees(Page)
        originalEmployee.Find("Code='" & SqlText(txtOriginalEmployee.Text) & "'")
        actingEmployee.Find("Code='" & SqlText(txtActingEmployee.Text) & "'")

        Dim exists As Boolean = Assignment.Find("Code='" & SqlText(txtCode.Text) & "'")
        Assignment.Code = txtCode.Text.Trim()
        Assignment.OriginalEmployeeID = Convert.ToInt32(originalEmployee.ID)
        Assignment.ActingEmployeeID = Convert.ToInt32(actingEmployee.ID)
        Assignment.EffectiveFrom = CDate(txtEffectiveFrom.Value).Date
        Assignment.EffectiveTo = CDate(txtEffectiveTo.Value).Date
        Assignment.Reason = txtReason.Text.Trim()
        Assignment.Remarks = txtRemarks.Text.Trim()
        Dim result As Boolean = If(exists, Assignment.Update("ID=" & Assignment.ID), Assignment.Save())
        If result Then
            Assignment.Find("Code='" & SqlText(txtCode.Text) & "'")
            ShowRecord()
            BindGrid()
        End If
        Return result
    End Function

    Private Sub LoadCurrent()
        If txtCode.Text <> "" Then Assignment.Find("Code='" & SqlText(txtCode.Text) & "'")
    End Sub

    Private Sub ShowRecord()
        txtCode.Text = Assignment.Code
        Dim originalEmployee As New Clshrs_Employees(Page)
        If originalEmployee.Find("ID=" & Assignment.OriginalEmployeeID) Then
            txtOriginalEmployee.Text = originalEmployee.Code
            txtOriginalEmployeeName.Text = EmployeeName(originalEmployee.Code)
        End If
        Dim actingEmployee As New Clshrs_Employees(Page)
        If actingEmployee.Find("ID=" & Assignment.ActingEmployeeID) Then
            txtActingEmployee.Text = actingEmployee.Code
            txtActingEmployeeName.Text = EmployeeName(actingEmployee.Code)
        End If
        txtEffectiveFrom.Value = Assignment.EffectiveFrom
        txtEffectiveTo.Value = If(Assignment.EffectiveTo.HasValue, CType(Assignment.EffectiveTo.Value, Object), Nothing)
        txtReason.Text = Assignment.Reason
        txtRemarks.Text = Assignment.Remarks
        lblRegDateValue.Text = If(Assignment.RegDate = Nothing, "", FormatDateTimeDisplay(Assignment.RegDate))
        lblCancelDateValue.Text = If(Assignment.CancelDate = Nothing, "", FormatDateDisplay(Assignment.CancelDate))
        Dim user As New Clssys_Users(Page)
        Dim regUserName As String = ""
        If Assignment.RegUserID > 0 AndAlso user.Find("ID=" & Assignment.RegUserID) Then
            regUserName = If(ProfileCls.CurrentLanguage = "Ar" AndAlso Not String.IsNullOrEmpty(Convert.ToString(user.ArbName)),
                             Convert.ToString(user.ArbName), Convert.ToString(user.EngName))
        End If
        lblRegUserValue.Text = regUserName
        ImageButton_Delete.Enabled = Assignment.CancelDate = Nothing
        LinkButton_Delete.Enabled = ImageButton_Delete.Enabled
        SetPermissions("E")
        RefreshDisplayFields()
        BindGrid()
    End Sub

    Private Sub NewRecord()
        Assignment.Clear()
        ClearFields(True)
        txtCode.Text = NextCode()
        txtEffectiveFrom.Value = Date.Today
        txtEffectiveTo.Value = Date.Today
        SetPermissions("N")
        RefreshDisplayFields()
        BindGrid()
    End Sub

    Private Sub ClearFields(ByVal clearCode As Boolean)
        If clearCode Then txtCode.Text = ""
        txtOriginalEmployee.Text = ""
        txtOriginalEmployeeName.Text = ""
        txtActingEmployee.Text = ""
        txtActingEmployeeName.Text = ""
        txtEffectiveFrom.Value = Nothing
        txtEffectiveTo.Value = Nothing
        txtReason.Text = ""
        txtRemarks.Text = ""
        lblRegDateValue.Text = ""
        lblRegUserValue.Text = ""
        lblCancelDateValue.Text = ""
        ImageButton_Delete.Enabled = False
        LinkButton_Delete.Enabled = False
        ClearDisplayFields()
    End Sub

    Private Sub ClearDisplayFields()
        Try
            If lblSummaryCode IsNot Nothing Then lblSummaryCode.Text = "—"
            If lblSummaryRegDate IsNot Nothing Then lblSummaryRegDate.Text = "—"
            If lblSidebarCode IsNot Nothing Then lblSidebarCode.Text = "—"
            If lblReplacedEmployeeDisplay IsNot Nothing Then lblReplacedEmployeeDisplay.Text = "—"
            If lblReplacedPositionDisplay IsNot Nothing Then lblReplacedPositionDisplay.Text = "—"
            If lblReplacedDepartmentDisplay IsNot Nothing Then lblReplacedDepartmentDisplay.Text = "—"
            If lblActingEmployeeDisplay IsNot Nothing Then lblActingEmployeeDisplay.Text = "—"
            If lblActingPositionDisplay IsNot Nothing Then lblActingPositionDisplay.Text = "—"
            If lblActingDepartmentDisplay IsNot Nothing Then lblActingDepartmentDisplay.Text = "—"
            If lblStartDisplay IsNot Nothing Then lblStartDisplay.Text = "—"
            If lblEndDisplay IsNot Nothing Then lblEndDisplay.Text = "—"
            If lblDurationDisplay IsNot Nothing Then lblDurationDisplay.Text = "—"
            If lblReasonDisplay IsNot Nothing Then lblReasonDisplay.Text = "—"
            If lblRemarksDisplay IsNot Nothing Then lblRemarksDisplay.Text = "—"
            If lblMetaRegBy IsNot Nothing Then lblMetaRegBy.Text = "—"
            If lblMetaRegOn IsNot Nothing Then lblMetaRegOn.Text = "—"
            If lblMetaCancel IsNot Nothing Then lblMetaCancel.Text = "—"
            If lblTlRegisteredMeta IsNot Nothing Then lblTlRegisteredMeta.Text = "—"
            If lblTlActiveMeta IsNot Nothing Then lblTlActiveMeta.Text = "—"
            If lblAuditRegMeta IsNot Nothing Then lblAuditRegMeta.Text = "—"
            If lblAuditStatusMeta IsNot Nothing Then lblAuditStatusMeta.Text = "—"
            ApplyStatusVisual("1", StatusText("1"))
        Catch
        End Try
    End Sub

    Private Sub RefreshDisplayFields()
        Dim phase As String = "start"
        Try
            phase = "code"
            Dim code As String = ""
            If txtCode IsNot Nothing AndAlso txtCode.Text IsNot Nothing Then code = txtCode.Text.Trim()
            If code = "" Then code = "—"
            If lblSummaryCode IsNot Nothing Then lblSummaryCode.Text = code
            If lblSidebarCode IsNot Nothing Then lblSidebarCode.Text = code

            phase = "replaced"
            Dim origCode As String = ""
            Dim origName As String = ""
            If txtOriginalEmployee IsNot Nothing AndAlso txtOriginalEmployee.Text IsNot Nothing Then origCode = txtOriginalEmployee.Text.Trim()
            If txtOriginalEmployeeName IsNot Nothing AndAlso txtOriginalEmployeeName.Text IsNot Nothing Then origName = txtOriginalEmployeeName.Text.Trim()
            Dim origText As String = CombineCodeName(origCode, origName)
            If lblReplacedEmployeeDisplay IsNot Nothing Then lblReplacedEmployeeDisplay.Text = If(origText = "", "—", origText)

            phase = "acting"
            Dim actCode As String = ""
            Dim actName As String = ""
            If txtActingEmployee IsNot Nothing AndAlso txtActingEmployee.Text IsNot Nothing Then actCode = txtActingEmployee.Text.Trim()
            If txtActingEmployeeName IsNot Nothing AndAlso txtActingEmployeeName.Text IsNot Nothing Then actName = txtActingEmployeeName.Text.Trim()
            Dim actText As String = CombineCodeName(actCode, actName)
            If lblActingEmployeeDisplay IsNot Nothing Then lblActingEmployeeDisplay.Text = If(actText = "", "—", actText)

            phase = "reason"
            Dim reasonVal As String = ""
            Dim remarksVal As String = ""
            If txtReason IsNot Nothing AndAlso txtReason.Text IsNot Nothing Then reasonVal = txtReason.Text
            If txtRemarks IsNot Nothing AndAlso txtRemarks.Text IsNot Nothing Then remarksVal = txtRemarks.Text
            If lblReasonDisplay IsNot Nothing Then lblReasonDisplay.Text = If(reasonVal = "", "—", reasonVal)
            If lblRemarksDisplay IsNot Nothing Then lblRemarksDisplay.Text = If(remarksVal = "", "—", remarksVal)

            phase = "dates"
            Dim startDate As Date? = Nothing
            Dim endDate As Date? = Nothing
            Try
                If txtEffectiveFrom IsNot Nothing AndAlso txtEffectiveFrom.Value IsNot Nothing AndAlso Convert.ToString(txtEffectiveFrom.Value) <> "" Then
                    startDate = CDate(txtEffectiveFrom.Value).Date
                End If
            Catch
            End Try
            Try
                If txtEffectiveTo IsNot Nothing AndAlso txtEffectiveTo.Value IsNot Nothing AndAlso Convert.ToString(txtEffectiveTo.Value) <> "" Then
                    endDate = CDate(txtEffectiveTo.Value).Date
                End If
            Catch
            End Try

            If lblStartDisplay IsNot Nothing Then lblStartDisplay.Text = If(startDate.HasValue, FormatDateDisplay(startDate.Value), "—")
            If lblEndDisplay IsNot Nothing Then
                If endDate.HasValue Then
                    lblEndDisplay.Text = FormatDateDisplay(endDate.Value)
                ElseIf startDate.HasValue Then
                    lblEndDisplay.Text = GetLocal("OpenEnded", "Open-ended")
                Else
                    lblEndDisplay.Text = "—"
                End If
            End If
            If lblDurationDisplay IsNot Nothing Then lblDurationDisplay.Text = BuildDurationText(startDate, endDate)

            phase = "lookups"
            Dim origPos As String = ""
            Dim origDept As String = ""
            Dim actPos As String = ""
            Dim actDept As String = ""
            Try
                If origCode <> "" Then
                    Dim employee As New Clshrs_Employees(Page)
                    If employee.Find("Code='" & SqlText(origCode) & "'") Then
                        origDept = GetDepartmentName(employee.DepartmentID)
                        origPos = GetEmployeePositionName(Convert.ToInt32(employee.ID))
                    End If
                End If
            Catch
            End Try
            Try
                If actCode <> "" Then
                    Dim employee As New Clshrs_Employees(Page)
                    If employee.Find("Code='" & SqlText(actCode) & "'") Then
                        actDept = GetDepartmentName(employee.DepartmentID)
                        actPos = GetEmployeePositionName(Convert.ToInt32(employee.ID))
                    End If
                End If
            Catch
            End Try
            If lblReplacedPositionDisplay IsNot Nothing Then lblReplacedPositionDisplay.Text = If(origPos = "", "—", origPos)
            If lblReplacedDepartmentDisplay IsNot Nothing Then lblReplacedDepartmentDisplay.Text = If(origDept = "", "—", origDept)
            If lblActingPositionDisplay IsNot Nothing Then lblActingPositionDisplay.Text = If(actPos = "", "—", actPos)
            If lblActingDepartmentDisplay IsNot Nothing Then lblActingDepartmentDisplay.Text = If(actDept = "", "—", actDept)

            phase = "status"
            Dim cancelDate As Date? = Nothing
            Dim regDate As DateTime? = Nothing
            Dim regUser As String = ""
            If lblRegUserValue IsNot Nothing AndAlso lblRegUserValue.Text IsNot Nothing Then regUser = lblRegUserValue.Text
            Try
                If Assignment IsNot Nothing AndAlso Assignment.ID > 0 Then
                    If Assignment.CancelDate <> Nothing Then cancelDate = Assignment.CancelDate
                    If Assignment.RegDate <> Nothing Then regDate = Assignment.RegDate
                End If
            Catch
            End Try

            Dim statusKey As String = ResolveStatusKey(startDate, endDate, cancelDate)
            Dim statusText As String = StatusText(statusKey)
            ApplyStatusVisual(statusKey, statusText)

            phase = "meta"
            Dim regDisplay As String = If(regDate.HasValue, FormatDateTimeDisplay(regDate.Value), "—")
            If lblSummaryRegDate IsNot Nothing Then lblSummaryRegDate.Text = regDisplay
            If lblMetaRegOn IsNot Nothing Then lblMetaRegOn.Text = regDisplay
            If lblMetaRegBy IsNot Nothing Then lblMetaRegBy.Text = If(regUser = "", "—", regUser)
            If lblMetaCancel IsNot Nothing Then lblMetaCancel.Text = If(cancelDate.HasValue, FormatDateDisplay(cancelDate.Value), "—")

            Dim byUser As String = If(regUser = "", "", " · " & GetLocal("ByPrefix", "by") & " " & regUser)
            Dim regMeta As String = If(regDate.HasValue, FormatDateTimeDisplay(regDate.Value) & byUser, "—")
            If lblTlRegisteredMeta IsNot Nothing Then lblTlRegisteredMeta.Text = regMeta
            If lblAuditRegMeta IsNot Nothing Then lblAuditRegMeta.Text = regMeta

            phase = "timeline"
            Dim activeTitle As String = GetLocal("litTlActive", "Assignment Active")
            Dim activeMeta As String = "—"
            Dim activeDotClass As String = "tl-dot green"
            Select Case statusKey
                Case "3"
                    activeTitle = GetLocal("litTlCancelled", "Assignment Cancelled")
                    activeDotClass = "tl-dot red"
                    activeMeta = If(cancelDate.HasValue, FormatDateDisplay(cancelDate.Value), "—")
                Case "2"
                    activeTitle = GetLocal("litTlEnded", "Assignment Ended")
                    activeDotClass = "tl-dot grey"
                    activeMeta = If(endDate.HasValue, FormatDateDisplay(endDate.Value), "—")
                Case "4"
                    activeTitle = GetLocal("litTlPending", "Assignment Scheduled")
                    activeDotClass = "tl-dot"
                    activeMeta = If(startDate.HasValue, GetLocal("StartsOn", "Starts on") & " " & FormatDateDisplay(startDate.Value), "—")
                Case Else
                    If startDate.HasValue AndAlso Not endDate.HasValue Then
                        activeMeta = GetLocal("ActiveFromOngoing", "Effective from {0}, currently ongoing").Replace("{0}", FormatDateDisplay(startDate.Value))
                    ElseIf startDate.HasValue AndAlso endDate.HasValue Then
                        activeMeta = GetLocal("ActiveFromTo", "Effective from {0} to {1}").Replace("{0}", FormatDateDisplay(startDate.Value)).Replace("{1}", FormatDateDisplay(endDate.Value))
                    End If
            End Select
            If lblTlActiveTitle IsNot Nothing Then lblTlActiveTitle.Text = activeTitle
            If lblTlActiveMeta IsNot Nothing Then lblTlActiveMeta.Text = activeMeta
            If lblAuditStatusTitle IsNot Nothing Then lblAuditStatusTitle.Text = activeTitle
            If lblAuditStatusMeta IsNot Nothing Then lblAuditStatusMeta.Text = activeMeta
            SetDotClass(spanTlActiveDot, activeDotClass)
            SetDotClass(spanAuditStatusDot, activeDotClass)
        Catch ex As Exception
            Try
                System.IO.File.WriteAllText(Server.MapPath("~/App_Data/aea_error.txt"), "RefreshDisplayFields phase=" & phase & vbCrLf & ex.ToString())
            Catch
            End Try
        End Try
    End Sub

    Private Function StatusText(ByVal statusKey As String) As String
        Dim isAr As Boolean = False
        Try
            isAr = (ProfileCls.CurrentLanguage = "Ar")
        Catch
        End Try
        Select Case statusKey
            Case "2" : Return If(isAr, "منتهٍ", "Ended")
            Case "3" : Return If(isAr, "ملغى", "Cancelled")
            Case "4" : Return If(isAr, "مجدول", "Scheduled")
            Case Else : Return If(isAr, "ساري", "Active")
        End Select
    End Function

    Private Sub ApplyStatusVisual(ByVal statusKey As String, ByVal statusText As String)
        Dim pillCss As String = "aea-status-pill aea-status-active"
        Dim badgeCss As String = "aea-badge aea-badge-active"
        Select Case statusKey
            Case "2"
                pillCss = "aea-status-pill aea-status-ended"
                badgeCss = "aea-badge aea-badge-ended"
            Case "3"
                pillCss = "aea-status-pill aea-status-cancelled"
                badgeCss = "aea-badge aea-badge-cancelled"
            Case "4"
                pillCss = "aea-status-pill aea-status-pending"
                badgeCss = "aea-badge aea-badge-pending"
        End Select
        If spanSummaryStatus IsNot Nothing Then spanSummaryStatus.CssClass = pillCss
        If lblSummaryStatus IsNot Nothing Then lblSummaryStatus.Text = statusText
        If lblSidebarBadge IsNot Nothing Then
            lblSidebarBadge.CssClass = badgeCss
            lblSidebarBadge.Text = statusText
        End If
    End Sub

    Private Sub SetDotClass(ByVal ctrl As Label, ByVal cssClass As String)
        If ctrl IsNot Nothing Then ctrl.CssClass = cssClass
    End Sub

    Private Function ResolveStatusKey(ByVal startDate As Date?, ByVal endDate As Date?, ByVal cancelDate As Date?) As String
        If cancelDate.HasValue Then Return "3"
        If startDate.HasValue AndAlso startDate.Value.Date > Date.Today Then Return "4"
        If endDate.HasValue AndAlso endDate.Value.Date < Date.Today Then Return "2"
        Return "1"
    End Function

    Private Function BuildDurationText(ByVal startDate As Date?, ByVal endDate As Date?) As String
        If Not startDate.HasValue Then Return "—"
        If Not endDate.HasValue Then Return GetLocal("DurationOngoing", "Ongoing")
        Dim days As Integer = (endDate.Value.Date - startDate.Value.Date).Days + 1
        If days <= 0 Then Return "—"
        If days < 30 Then
            Return days.ToString() & " " & GetLocal("DaysUnit", "days")
        End If
        Dim months As Integer = CInt(Math.Round(days / 30.0))
        If months < 12 Then
            Return months.ToString() & " " & GetLocal("MonthsUnit", "months")
        End If
        Dim years As Integer = CInt(Math.Floor(months / 12.0))
        Dim remMonths As Integer = months Mod 12
        If remMonths = 0 Then Return years.ToString() & " " & GetLocal("YearsUnit", "years")
        Return years.ToString() & " " & GetLocal("YearsUnit", "years") & " " & remMonths.ToString() & " " & GetLocal("MonthsUnit", "months")
    End Function

    Private Function NextCode() As String
        Dim sql As String = "SELECT ISNULL(MAX(CASE" &
            " WHEN Code LIKE 'EA-%' AND ISNUMERIC(REPLACE(Code,'EA-',''))=1 THEN CAST(REPLACE(Code,'EA-','') AS int)" &
            " WHEN Code LIKE 'AE-%' AND ISNUMERIC(REPLACE(Code,'AE-',''))=1 THEN CAST(REPLACE(Code,'AE-','') AS int)" &
            " ELSE 0 END),0)+1 FROM hrs_ActingEmployeeAssignments"
        Dim number As Integer = Convert.ToInt32(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Assignment.ConnectionString, CommandType.Text, sql))
        Return "EA-" & number.ToString("000000")
    End Function

    Private Sub BindGrid()
        Dim isAr As Boolean = (ProfileCls.CurrentLanguage = "Ar")
        Dim nameExpression As String = If(isAr, "dbo.fn_GetEmpName(E.Code,1)", "dbo.fn_GetEmpName(E.Code,0)")
        Dim originalNameExpression As String = If(isAr, "dbo.fn_GetEmpName(O.Code,1)", "dbo.fn_GetEmpName(O.Code,0)")
        Dim empPosName As String = If(isAr, "ISNULL(P.ArbName,P.EngName)", "ISNULL(P.EngName,P.ArbName)")

        Dim sql As String =
            "SELECT A.Code, A.Reason, A.EffectiveFrom, A.EffectiveTo, A.CancelDate, A.RegDate," &
            " O.Code+' - '+ISNULL(" & originalNameExpression & ",'') AS OriginalEmployeeDisplay," &
            " ISNULL((" &
            "   SELECT TOP 1 " & empPosName &
            "   FROM hrs_Contracts OC INNER JOIN hrs_Positions P ON P.ID=OC.PositionID" &
            "   WHERE OC.EmployeeID=O.ID AND OC.CancelDate IS NULL AND OC.PositionID IS NOT NULL" &
            "     AND (OC.EndDate IS NULL OR OC.EndDate>=GETDATE()) ORDER BY OC.ID DESC" &
            " ),'') AS OriginalEmployeePosition," &
            " E.Code+' - '+ISNULL(" & nameExpression & ",'') AS ActingEmployeeDisplay," &
            " ISNULL((" &
            "   SELECT TOP 1 " & empPosName &
            "   FROM hrs_Contracts EC INNER JOIN hrs_Positions P ON P.ID=EC.PositionID" &
            "   WHERE EC.EmployeeID=E.ID AND EC.CancelDate IS NULL AND EC.PositionID IS NOT NULL" &
            "     AND (EC.EndDate IS NULL OR EC.EndDate>=GETDATE()) ORDER BY EC.ID DESC" &
            " ),'') AS ActingEmployeePosition," &
            " CASE" &
            "   WHEN A.CancelDate IS NOT NULL THEN '3'" &
            "   WHEN A.EffectiveFrom > CONVERT(date,GETDATE()) THEN '4'" &
            "   WHEN A.EffectiveTo IS NOT NULL AND A.EffectiveTo < CONVERT(date,GETDATE()) THEN '2'" &
            "   ELSE '1' END AS StatusKey" &
            " FROM hrs_ActingEmployeeAssignments A" &
            " INNER JOIN hrs_Employees O ON O.ID=A.OriginalEmployeeID" &
            " INNER JOIN hrs_Employees E ON E.ID=A.ActingEmployeeID" &
            " ORDER BY A.RegDate DESC, A.ID DESC"

        Dim data As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Assignment.ConnectionString, CommandType.Text, sql)
        Dim table As DataTable = If(data IsNot Nothing AndAlso data.Tables.Count > 0, data.Tables(0).Copy(), New DataTable())

        EnsureGridColumns(table)
        Dim filtered As DataTable = table.Clone()
        Dim search As String = If(txtSearch Is Nothing, "", txtSearch.Text.Trim().ToLowerInvariant())
        Dim statusFilter As String = If(ddlStatusFilter Is Nothing OrElse ddlStatusFilter.SelectedValue = "0", "", ddlStatusFilter.SelectedValue)

        For Each row As DataRow In table.Rows
            Dim statusKey As String = Convert.ToString(row("StatusKey"))
            If statusFilter <> "" AndAlso Not String.Equals(statusKey, statusFilter, StringComparison.OrdinalIgnoreCase) Then Continue For
            If search <> "" Then
                Dim hay As String = (Convert.ToString(row("Code")) & " " & Convert.ToString(row("OriginalEmployeeDisplay")) & " " &
                    Convert.ToString(row("OriginalEmployeePosition")) & " " & Convert.ToString(row("ActingEmployeeDisplay")) & " " &
                    Convert.ToString(row("ActingEmployeePosition")) & " " & Convert.ToString(row("Reason"))).ToLowerInvariant()
                If Not hay.Contains(search) Then Continue For
            End If

            Dim outRow As DataRow = filtered.NewRow()
            outRow("Code") = row("Code")
            outRow("OriginalEmployeeDisplay") = row("OriginalEmployeeDisplay")
            outRow("OriginalEmployeePosition") = row("OriginalEmployeePosition")
            outRow("ActingEmployeeDisplay") = row("ActingEmployeeDisplay")
            outRow("ActingEmployeePosition") = row("ActingEmployeePosition")
            outRow("Reason") = If(Convert.ToString(row("Reason")) = "", "—", row("Reason"))
            outRow("StatusKey") = statusKey
            outRow("StatusText") = StatusText(statusKey)
            outRow("StatusCss") = StatusCss(statusKey)

            Dim ef As Date? = Nothing
            Dim et As Date? = Nothing
            If Not IsDBNull(row("EffectiveFrom")) Then ef = Convert.ToDateTime(row("EffectiveFrom")).Date
            If Not IsDBNull(row("EffectiveTo")) Then et = Convert.ToDateTime(row("EffectiveTo")).Date
            outRow("StartText") = If(ef.HasValue, FormatDateDisplay(ef.Value), "—")
            outRow("EndText") = If(et.HasValue, FormatDateDisplay(et.Value), GetLocal("OpenEnded", "Open-ended"))
            filtered.Rows.Add(outRow)
        Next

        rptAssignments.DataSource = filtered
        rptAssignments.DataBind()
        Dim hasRows As Boolean = (filtered.Rows.Count > 0)
        pnlEmptyGrid.Visible = Not hasRows
        pnlGridTable.Visible = hasRows
        rptAssignments.Visible = hasRows

        Try
            Dim legacy As DataTable = New DataTable()
            legacy.Columns.Add("Code", GetType(String))
            legacy.Columns.Add("OriginalEmployee", GetType(String))
            legacy.Columns.Add("ActingEmployee", GetType(String))
            legacy.Columns.Add("EffectiveFrom", GetType(DateTime))
            legacy.Columns.Add("EffectiveTo", GetType(DateTime))
            For Each row As DataRow In filtered.Rows
                Dim lr As DataRow = legacy.NewRow()
                lr("Code") = row("Code")
                lr("OriginalEmployee") = row("OriginalEmployeeDisplay")
                lr("ActingEmployee") = row("ActingEmployeeDisplay")
                Dim match = table.Select("Code='" & SqlText(Convert.ToString(row("Code"))) & "'")
                If match.Length > 0 Then
                    If Not IsDBNull(match(0)("EffectiveFrom")) Then lr("EffectiveFrom") = match(0)("EffectiveFrom") Else lr("EffectiveFrom") = DBNull.Value
                    If Not IsDBNull(match(0)("EffectiveTo")) Then lr("EffectiveTo") = match(0)("EffectiveTo") Else lr("EffectiveTo") = DBNull.Value
                End If
                legacy.Rows.Add(lr)
            Next
            uwgAssignments.DataSource = legacy
            uwgAssignments.DataBind()
            ApplyGridAlignment()
        Catch
        End Try
    End Sub

    Private Sub EnsureGridColumns(ByVal table As DataTable)
        Dim cols As String() = {"Code", "OriginalEmployeeDisplay", "OriginalEmployeePosition", "ActingEmployeeDisplay",
                                "ActingEmployeePosition", "Reason", "StatusKey", "StatusText", "StatusCss", "StartText", "EndText"}
        For Each c As String In cols
            If Not table.Columns.Contains(c) Then table.Columns.Add(c, GetType(String))
        Next
    End Sub

    Private Function StatusCss(ByVal statusKey As String) As String
        Select Case statusKey
            Case "2" : Return "aea-status-ended"
            Case "3" : Return "aea-status-cancelled"
            Case "4" : Return "aea-status-pending"
            Case Else : Return "aea-status-active"
        End Select
    End Function

    Private Sub ApplyGridAlignment()
        Try
            Dim align As HorizontalAlign = If(ProfileCls.CurrentLanguage = "Ar", HorizontalAlign.Right, HorizontalAlign.Left)
            Dim widths As New Dictionary(Of String, String) From {
                {"Code", "14%"},
                {"OriginalPosition", "28%"},
                {"OriginalEmployee", "28%"},
                {"ActingEmployee", "28%"},
                {"EffectiveFrom", "15%"},
                {"EffectiveTo", "15%"}
            }
            uwgAssignments.Width = Unit.Percentage(100)
            uwgAssignments.DisplayLayout.FrameStyle.Width = Unit.Percentage(100)
            uwgAssignments.DisplayLayout.HeaderStyleDefault.HorizontalAlign = align
            uwgAssignments.DisplayLayout.RowStyleDefault.HorizontalAlign = align
            For Each col As Infragistics.WebUI.UltraWebGrid.UltraGridColumn In uwgAssignments.Columns
                col.Header.Style.HorizontalAlign = align
                col.CellStyle.HorizontalAlign = align
                If widths.ContainsKey(col.Key) Then col.Width = Unit.Parse(widths(col.Key))
            Next
        Catch
        End Try
    End Sub

    Private Sub uwgAssignments_DblClick(ByVal sender As Object, ByVal e As EventArgs) Handles uwgAssignments.DblClick
        Dim row As Infragistics.WebUI.UltraWebGrid.UltraGridRow = uwgAssignments.DisplayLayout.ActiveRow
        If row IsNot Nothing Then
            txtCode.Text = Convert.ToString(row.Cells.FromKey("Code").Value)
            txtCode_TextChanged(Nothing, Nothing)
        End If
    End Sub

    Private Function EmployeeName(ByVal code As String) As String
        Dim language As Integer = If(ProfileCls.CurrentLanguage = "Ar", 1, 0)
        Dim sql As String = "SELECT dbo.fn_GetEmpName(Code," & language & ") FROM hrs_Employees WHERE Code=@Code"
        Using connection As New SqlConnection(Assignment.ConnectionString)
            Using command As New SqlCommand(sql, connection)
                command.Parameters.Add("@Code", SqlDbType.VarChar, 30).Value = code
                connection.Open()
                Return Convert.ToString(command.ExecuteScalar())
            End Using
        End Using
    End Function

    Private Function GetEmployeePositionName(ByVal employeeID As Integer) As String
        Dim positionName As String = If(ProfileCls.CurrentLanguage = "Ar", "ISNULL(P.ArbName,P.EngName)", "ISNULL(P.EngName,P.ArbName)")
        Dim sql As String =
            "SELECT TOP 1 " & positionName & " AS PositionName" &
            " FROM hrs_Contracts C INNER JOIN hrs_Positions P ON P.ID=C.PositionID" &
            " WHERE C.EmployeeID=@EmployeeID AND C.PositionID IS NOT NULL AND C.CancelDate IS NULL" &
            " AND (C.EndDate IS NULL OR C.EndDate >= GETDATE()) ORDER BY C.ID DESC"
        Using connection As New SqlConnection(Assignment.ConnectionString)
            Using command As New SqlCommand(sql, connection)
                command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID
                connection.Open()
                Dim result As Object = command.ExecuteScalar()
                If result Is Nothing OrElse IsDBNull(result) Then Return ""
                Return Convert.ToString(result)
            End Using
        End Using
    End Function

    Private Function GetDepartmentName(ByVal departmentID As Object) As String
        Dim id As Integer = 0
        If departmentID Is Nothing OrElse IsDBNull(departmentID) OrElse Not Integer.TryParse(Convert.ToString(departmentID), id) OrElse id <= 0 Then
            Return ""
        End If
        Dim dept As New Clssys_Departments(Page)
        If dept.Find("ID=" & id) Then
            Return If(ProfileCls.CurrentLanguage = "Ar", Convert.ToString(dept.ArbName), Convert.ToString(dept.EngName))
        End If
        Return ""
    End Function

    Private Function CombineCodeName(ByVal code As String, ByVal name As String) As String
        code = If(code, "").Trim()
        name = If(name, "").Trim()
        If code = "" AndAlso name = "" Then Return ""
        If code = "" Then Return name
        If name = "" Then Return code
        Return code & " - " & name
    End Function

    Private Function FormatDateDisplay(ByVal value As Date) As String
        If ProfileCls.CurrentLanguage = "Ar" Then
            Dim ci As New System.Globalization.CultureInfo("ar-EG")
            ci.DateTimeFormat.Calendar = New System.Globalization.GregorianCalendar()
            Return value.ToString("dd MMMM yyyy", ci)
        End If
        Return value.ToString("dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture)
    End Function

    Private Function FormatDateTimeDisplay(ByVal value As DateTime) As String
        If ProfileCls.CurrentLanguage = "Ar" Then
            Dim ci As New System.Globalization.CultureInfo("ar-EG")
            ci.DateTimeFormat.Calendar = New System.Globalization.GregorianCalendar()
            Return value.ToString("dd MMMM yyyy, hh:mm tt", ci)
        End If
        Return value.ToString("dd MMM yyyy, hh:mm tt", System.Globalization.CultureInfo.InvariantCulture)
    End Function

    Private Sub LocalizeFilterControls()
        Try
            If ddlStatusFilter.Items.Count >= 4 Then
                ddlStatusFilter.Items(0).Text = GetLocal("liAllStatusesResource1.Text", "All Statuses")
                ddlStatusFilter.Items(1).Text = GetLocal("liActiveResource1.Text", "Active")
                ddlStatusFilter.Items(2).Text = GetLocal("liEndedResource1.Text", "Ended")
                ddlStatusFilter.Items(3).Text = GetLocal("liCancelledResource1.Text", "Cancelled")
            End If
        Catch
        End Try
    End Sub

    Protected Function GetLocal(ByVal key As String, ByVal fallback As String) As String
        Try
            Dim value As Object = GetLocalResourceObject(key)
            If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then Return Convert.ToString(value)
        Catch
        End Try
        Return fallback
    End Function

    Private Sub OpenAuxiliary(ByVal template As String, ByVal title As String, ByVal width As Integer, ByVal height As Integer)
        If Assignment.Find("Code='" & SqlText(txtCode.Text) & "'") Then
            Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, String.Format(template, Assignment.ID, Assignment.Table.Trim()),
                width, height, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, title, False)
        End If
    End Sub

    Private Sub ApplyScreenSettings()
        Try
            Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, Assignment.ConnectionString, "UltraWebTab1")
            Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, Assignment.ConnectionString, "UltraWebTab1")
            Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, Assignment.ConnectionString, "UltraWebTab1")
        Catch
        End Try

        Dim savedFilterTexts(ddlStatusFilter.Items.Count - 1) As String
        Try
            For i As Integer = 0 To ddlStatusFilter.Items.Count - 1
                savedFilterTexts(i) = ddlStatusFilter.Items(i).Text
                ddlStatusFilter.Items(i).Text = ddlStatusFilter.Items(i).Value
            Next
            Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, Assignment.ConnectionString, DIV)
        Catch
        Finally
            For i As Integer = 0 To ddlStatusFilter.Items.Count - 1
                If savedFilterTexts(i) IsNot Nothing Then ddlStatusFilter.Items(i).Text = savedFilterTexts(i)
            Next
        End Try

        Try
            Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, Assignment.ConnectionString, Assignment.DataBaseUserRelatedID, Assignment.GroupID)
        Catch
        End Try
    End Sub

    Private Sub SetPermissions(ByVal mode As String)
        Try
            Dim data As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(
                Assignment.ConnectionString, "hrs_GetFormsPermissions", Assignment.DataBaseUserRelatedID,
                Assignment.GroupID, Page.Form.ID)
            If Venus.Shared.DataHandler.CheckValidDataObject(data) Then
                Dim row As DataRow = data.Tables(0).Rows(0)
                Dim canSave As Boolean = Convert.ToBoolean(row(If(mode = "N", "AllowAdd", "AllowEdit")))
                ImageButton_Save.Enabled = canSave
                ImageButton_SaveN.Enabled = canSave
                LinkButton_SaveN.Enabled = canSave
                ImageButton_Delete.Enabled = ImageButton_Delete.Enabled AndAlso Convert.ToBoolean(row("AllowDelete"))
                LinkButton_Delete.Enabled = ImageButton_Delete.Enabled
                ImageButton_Print.Enabled = Convert.ToBoolean(row("AllowPrint"))
                LinkButton_PrintText.Enabled = ImageButton_Print.Enabled
            End If
        Catch
        End Try
    End Sub

    Private Function Message(ByVal bilingual As String) As String
        Return New Venus.Shared.Web.NavigationHandler(Assignment.ConnectionString).SetLanguage(Page, bilingual)
    End Function

    Private Function SqlText(ByVal value As String) As String
        Return value.Replace("'", "''")
    End Function

    Private Sub ReportError(ByVal ex As Exception)
        Session("ErrorValue") = ex
        Dim handler As New Venus.Shared.ErrorsHandler(Assignment.ConnectionString)
        handler.RecordExceptions_DataBase("", ex, Err.Number, "", Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
        Response.Redirect("ErrorPage.aspx")
    End Sub
End Class
