Imports System.Data
Imports System.Data.SqlClient
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class frmActingEmployeeAssignment
    Inherits MainPage

    Private Assignment As Clshrs_ActingEmployeeAssignments

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Assignment = New Clshrs_ActingEmployeeAssignments(Page)
        Try
            DIV.Style("direction") = If(ProfileCls.CurrentLanguage = "Ar", "rtl", "ltr")
            ConfigureSearches()
            ApplyGridAlignment()
            If Not IsPostBack Then
                Session("ConnectionString") = Assignment.ConnectionString
                UltraWebTab1.SelectedTab = 0
                If Val(Request.QueryString("ID")) > 0 AndAlso Assignment.Find("ID=" & Val(Request.QueryString("ID"))) Then
                    ShowRecord()
                Else
                    NewRecord()
                End If
                ApplyScreenSettings()
            End If
        Catch ex As Exception
            ReportError(ex)
        End Try
    End Sub

    Protected Sub Toolbar_Command(ByVal sender As Object, ByVal e As CommandEventArgs) Handles _
        ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, _
        ImageButton_New.Command, ImageButton_Delete.Command, LinkButton_Delete.Command, _
        ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, _
        ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_First.Command, _
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
    End Sub

    Protected Sub txtActingEmployee_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtActingEmployee.TextChanged
        Dim employee As New Clshrs_Employees(Page)
        If employee.Find("Code='" & SqlText(txtActingEmployee.Text) & "'") Then
            txtActingEmployeeName.Text = EmployeeName(txtActingEmployee.Text)
        Else
            txtActingEmployeeName.Text = ""
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
        txtEffectiveTo.Value = Assignment.EffectiveTo
        txtReason.Text = Assignment.Reason
        txtRemarks.Text = Assignment.Remarks
        lblRegDateValue.Text = If(Assignment.RegDate = Nothing, "", Assignment.RegDate.ToString("dd/MM/yyyy"))
        lblCancelDateValue.Text = If(Assignment.CancelDate = Nothing, "", Assignment.CancelDate.ToString("dd/MM/yyyy"))
        Dim user As New Clssys_Users(Page)
        lblRegUserValue.Text = If(Assignment.RegUserID > 0 AndAlso user.Find("ID=" & Assignment.RegUserID), user.EngName, "")
        ImageButton_Delete.Enabled = Assignment.CancelDate = Nothing
        LinkButton_Delete.Enabled = ImageButton_Delete.Enabled
        SetPermissions("E")
        BindGrid()
    End Sub

    Private Sub NewRecord()
        Assignment.Clear()
        ClearFields(True)
        txtCode.Text = NextCode()
        txtEffectiveFrom.Value = Date.Today
        txtEffectiveTo.Value = Date.Today
        SetPermissions("N")
        BindGrid()
        txtOriginalEmployee.Focus()
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
    End Sub

    Private Function NextCode() As String
        Dim sql As String = "SELECT ISNULL(MAX(CASE" &
            " WHEN Code LIKE 'EA-%' AND ISNUMERIC(REPLACE(Code,'EA-',''))=1 THEN CAST(REPLACE(Code,'EA-','') AS int)" &
            " WHEN Code LIKE 'AE-%' AND ISNUMERIC(REPLACE(Code,'AE-',''))=1 THEN CAST(REPLACE(Code,'AE-','') AS int)" &
            " ELSE 0 END),0)+1 FROM hrs_ActingEmployeeAssignments"
        Dim number As Integer = Convert.ToInt32(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Assignment.ConnectionString, CommandType.Text, sql))
        Return "EA-" & number.ToString("000000")
    End Function

    Private Sub BindGrid()
        Dim nameExpression As String = If(ProfileCls.CurrentLanguage = "Ar",
            "dbo.fn_GetEmpName(E.Code,1)", "dbo.fn_GetEmpName(E.Code,0)")
        Dim originalNameExpression As String = If(ProfileCls.CurrentLanguage = "Ar",
            "dbo.fn_GetEmpName(O.Code,1)", "dbo.fn_GetEmpName(O.Code,0)")
        Dim sql As String = "SELECT A.Code, O.Code+' - '+ISNULL(" & originalNameExpression & ",'') OriginalEmployee," &
            " E.Code+' - '+ISNULL(" & nameExpression & ",'') ActingEmployee, A.EffectiveFrom, A.EffectiveTo" &
            " FROM hrs_ActingEmployeeAssignments A INNER JOIN hrs_Employees O ON O.ID=A.OriginalEmployeeID" &
            " INNER JOIN hrs_Employees E ON E.ID=A.ActingEmployeeID ORDER BY A.RegDate DESC, A.ID DESC"
        Dim data As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Assignment.ConnectionString, CommandType.Text, sql)
        uwgAssignments.DataSource = data
        uwgAssignments.DataBind()
        ApplyGridAlignment()
    End Sub

    Private Sub ApplyGridAlignment()
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

    Private Sub OpenAuxiliary(ByVal template As String, ByVal title As String, ByVal width As Integer, ByVal height As Integer)
        If Assignment.Find("Code='" & SqlText(txtCode.Text) & "'") Then
            Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, String.Format(template, Assignment.ID, Assignment.Table.Trim()),
                width, height, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, title, False)
        End If
    End Sub

    Private Sub ApplyScreenSettings()
        Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, Assignment.ConnectionString, "UltraWebTab1")
        Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, Assignment.ConnectionString, "UltraWebTab1")
        Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, Assignment.ConnectionString, "UltraWebTab1")
        Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, Assignment.ConnectionString, DIV)
        Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, Assignment.ConnectionString, Assignment.DataBaseUserRelatedID, Assignment.GroupID)
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
