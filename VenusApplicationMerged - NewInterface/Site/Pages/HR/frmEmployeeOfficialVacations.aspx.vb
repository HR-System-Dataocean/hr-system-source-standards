Imports System.Activities.Statements
Imports System.Collections.Generic
Imports System.Data
Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Application.SystemFiles.System

Partial Class frmEmployeeOfficialVacations
    Inherits MainPage
#Region "Public Decleration"
    Dim clsHrsEmployeeOfficialVacations As Clshrs_OfficialVacations
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private ClsFiscalYears As Clssys_FiscalYears
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)

        ClsFiscalYears = New Clssys_FiscalYears(Me.Page)
        Dim clsNavigation = New Venus.Shared.Web.NavigationHandler(ClsFiscalYears.ConnectionString)
        Try

            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                txtDate.Culture = System.Globalization.CultureInfo.CurrentCulture
                txtDate.DisplayModeFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern
                txtDate.EditModeFormat = txtDate.DisplayModeFormat
                Page.Session.Add("ConnectionString", clsHrsEmployeeOfficialVacations.ConnectionString)
                clsHrsEmployeeOfficialVacations.AddOnChangeEventToControls("frmEmployeeOfficialVacations", Page, UltraWebTab1)

                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-GB")
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-GB")

                ClsFiscalYears.GetDropDown(ddlFiscalYear)
                If (ddlFiscalYear.SelectedItem.Text <> "") Then
                    GetValues()

                Else
                    SetScreenInformation("N")
                End If
            End If
            GetDropDownListGrid()
            Dim IntrecordID As Integer

            CreateOtherFields(IntrecordID)
            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, clsHrsEmployeeOfficialVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Protected Sub UltraWebGrid1_UpdateRow(sender As Object, e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles UwgSearchEmployees.UpdateRow

        'If txtDate.Value IsNot Nothing And Not IsNothing(e.Row.Cells("3").Value) Then
        '    e.Row.Cells(4).Value = CType(txtDate.Value, DateTime)
        'End If
        'If txtToDate.Value IsNot Nothing And Not IsNothing(e.Row.Cells("3").Value) Then
        '    e.Row.Cells(5).Value = CType(txtToDate.Value, DateTime)
        'End If

    End Sub
    Protected Sub UltraWebGrid1_InitializeRow(sender As Object, e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles UwgSearchEmployees.InitializeRow
        If e.Row.Cells.FromKey("LineNumber") IsNot Nothing Then
            e.Row.Cells.FromKey("LineNumber").Value = e.Row.Index + 1
        End If
        If e.Row.Cells.FromKey("IsRamadan") IsNot Nothing Then
            NormalizeIsRamadanCell(e.Row.Cells.FromKey("IsRamadan"))
        End If
        ApplyEventTypeRules(e.Row)
        LockPreparedPeriodRow(e.Row)
    End Sub

    Protected Sub UwgSearchEmployees_UpdateCell(sender As Object, e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles UwgSearchEmployees.UpdateCell
        If e.Cell.Column.Key = "eventType" Then
            ApplyEventTypeRules(e.Cell.Row)
        ElseIf e.Cell.Column.Key = "IsRamadan" Then
            NormalizeIsRamadanCell(e.Cell)
            If GetIsRamadanBooleanValue(e.Cell.Value) AndAlso HasAnotherRamadanMarked(e.Cell.Row) Then
                e.Cell.Value = False
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Only one row can be marked as Ramadan / لا يمكن اختيار شهر رمضان لاكثر من سطر")
            End If
        End If
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsHrsEmployeeOfficialVacations.ConnectionString)
        Dim SqlCommand As String = String.Empty
        Dim currentRegUserId As Integer = 0
        If clsHrsEmployeeOfficialVacations.DataBaseUserRelatedID > 0 Then
            currentRegUserId = clsHrsEmployeeOfficialVacations.DataBaseUserRelatedID
        ElseIf clsHrsEmployeeOfficialVacations.RegUserID > 0 Then
            currentRegUserId = clsHrsEmployeeOfficialVacations.RegUserID
        End If
        Select Case e.CommandArgument
            Case "SaveNew"
                If ddlFiscalYear.SelectedItem.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Year /برجاء إدخال العام"))
                    Exit Sub
                End If
                If Not ValidateGridBeforeSave() Then
                    Exit Sub
                End If
                Dim Deletecommand As String = "Delete from hrs_OfficialVacations where Year='" & ddlFiscalYear.SelectedItem.Text & "' "
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsHrsEmployeeOfficialVacations.ConnectionString, Data.CommandType.Text, Deletecommand)
                'clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                Dim LineNumber = 0
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                    If ShouldSaveGridRow(DGRow) Then
                        LineNumber = LineNumber + 1
                        Dim isRamadanValue As Integer = GetGridIsRamadanSqlValue(DGRow)
                        Dim eventTypeValue As String = GetGridEventTypeSqlValue(DGRow)
                        Dim vacationTypeValue As String = GetGridVacationTypeSqlValue(DGRow, eventTypeValue)
                        Dim arbName As String = GetGridCellText(DGRow, "ArbName").Replace("'", "''")
                        Dim engName As String = GetGridCellText(DGRow, "EngName").Replace("'", "''")

                        SqlCommand &= " Set DateFormat DMY Insert Into hrs_OfficialVacations " &
"(LineNum, eventType, VacationTypeID, ArbName, EngName, FromDate, ToDate, isramadan, Year, RegUserID, RegDate)Values(" &
LineNumber & ", " &
eventTypeValue & ", " &
vacationTypeValue & ", " &
"'" & arbName & "', " &
"'" & engName & "', " &
"'" & CDate(DGRow.Cells.FromKey("FromDate").Text).ToString("yyyy-MM-dd HH:mm") & "', " &
"'" & CDate(DGRow.Cells.FromKey("ToDate").Text).ToString("yyyy-MM-dd HH:mm") & "', " &
isRamadanValue & ", " &
"'" & ddlFiscalYear.SelectedItem.Text & "', " &
currentRegUserId & ", GETDATE()) ; " & vbNewLine
                    End If
                Next
                If SqlCommand <> "" Then
                    Dim cmd As New SqlClient.SqlCommand
                    cmd.CommandText = SqlCommand
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = New SqlClient.SqlConnection(clsHrsEmployeeOfficialVacations.ConnectionString)
                    cmd.Connection.Open()
                    cmd.ExecuteNonQuery()
                    cmd.Connection.Close()
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                    AfterOperation()
                End If
            Case "Save"
                If ddlFiscalYear.SelectedItem.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Year /برجاء إدخال العام"))
                    Exit Sub
                End If
                If Not ValidateGridBeforeSave() Then
                    Exit Sub
                End If
                Dim Deletecommand As String = "Delete from hrs_OfficialVacations where Year='" & ddlFiscalYear.SelectedItem.Text & "' "
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsHrsEmployeeOfficialVacations.ConnectionString, Data.CommandType.Text, Deletecommand)
                'clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                Dim LineNumber = 0
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                    If ShouldSaveGridRow(DGRow) Then
                        LineNumber = LineNumber + 1
                        Dim isRamadanValue As Integer = GetGridIsRamadanSqlValue(DGRow)
                        Dim eventTypeValue As String = GetGridEventTypeSqlValue(DGRow)
                        Dim vacationTypeValue As String = GetGridVacationTypeSqlValue(DGRow, eventTypeValue)
                        Dim arbName As String = GetGridCellText(DGRow, "ArbName").Replace("'", "''")
                        Dim engName As String = GetGridCellText(DGRow, "EngName").Replace("'", "''")

                        SqlCommand &= " Set DateFormat DMY Insert Into hrs_OfficialVacations " &
"(LineNum, eventType, VacationTypeID, ArbName, EngName, FromDate, ToDate, isramadan, Year, RegUserID, RegDate)Values(" &
LineNumber & ", " &
eventTypeValue & ", " &
vacationTypeValue & ", " &
"'" & arbName & "', " &
"'" & engName & "', " &
"'" & CDate(DGRow.Cells.FromKey("FromDate").Text).ToString("yyyy-MM-dd HH:mm") & "', " &
"'" & CDate(DGRow.Cells.FromKey("ToDate").Text).ToString("yyyy-MM-dd HH:mm") & "', " &
isRamadanValue & ", " &
"'" & ddlFiscalYear.SelectedItem.Text & "', " &
currentRegUserId & ", GETDATE()) ; " & vbNewLine
                    End If
                Next
                If SqlCommand <> "" Then
                    Dim cmd As New SqlClient.SqlCommand
                    cmd.CommandText = SqlCommand
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = New SqlClient.SqlConnection(clsHrsEmployeeOfficialVacations.ConnectionString)
                    cmd.Connection.Open()
                    cmd.ExecuteNonQuery()
                    cmd.Connection.Close()
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                End If
            Case "New"
                AfterOperation()
            Case "Delete"
                If Not DeleteSelectedOfficialVacationRow() Then
                    Exit Sub
                End If
            Case "Property"
                clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & clsHrsEmployeeOfficialVacations.ID & "&TableName=" & clsHrsEmployeeOfficialVacations.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & clsHrsEmployeeOfficialVacations.ID & "&TableName=" & clsHrsEmployeeOfficialVacations.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = clsHrsEmployeeOfficialVacations.Table
                clsHrsEmployeeOfficialVacations.Find(" year = '" & ddlFiscalYear.SelectedItem.Text & "'")
                Dim recordID As Integer = clsHrsEmployeeOfficialVacations.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & clsHrsEmployeeOfficialVacations.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                clsHrsEmployeeOfficialVacations.Find(" Year= '" & ddlFiscalYear.SelectedItem.Text & "'")
                If clsHrsEmployeeOfficialVacations.ID > 0 Then
                    Dim Ds As Data.DataSet = clsHrsEmployeeOfficialVacations.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If clsHrsEmployeeOfficialVacations.CheckDiff(clsHrsEmployeeOfficialVacations, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                clsHrsEmployeeOfficialVacations.FirstRecord()
                GetValues()
            Case "Previous"
                clsHrsEmployeeOfficialVacations.Find("year='" & ddlFiscalYear.SelectedItem.Text & "'")
                If Not clsHrsEmployeeOfficialVacations.previousRecord() Then
                    clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                If Not clsHrsEmployeeOfficialVacations.NextRecord() Then
                    clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                clsHrsEmployeeOfficialVacations.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFiscalYear.TextChanged
        CheckCode()
    End Sub
    Protected Sub txthdDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles hfTxtDateID.ValueChanged
        txtDate.Value = hfTxtDateID.Value
    End Sub
    Protected Sub txthdToDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles hfTxtToDateID.ValueChanged
        txtToDate.Value = hfTxtToDateID.Value
    End Sub
#End Region

#Region "Private Functions"
    Private Function AssignValues() As Boolean
        Try
            With clsHrsEmployeeOfficialVacations
                .Year = ddlFiscalYear.SelectedItem.Text
            End With
            Return True
        Catch ex As Exception
        End Try
    End Function

    Private Function ValidateGridBeforeSave() As Boolean
        Try
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsHrsEmployeeOfficialVacations.ConnectionString)
            Dim rowNumber As Integer = 0
            Dim rowDataList As New List(Of OfficialVacationRowData)
            Dim isRamadanRowsCount As Integer = 0
            Dim yearFromDate As Date = Date.MinValue
            Dim yearToDate As Date = Date.MinValue
            Dim hasYearRange As Boolean = TryGetSelectedFiscalYearDateRange(yearFromDate, yearToDate)

            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                rowNumber += 1
                If Not ShouldSaveGridRow(DGRow) Then
                    Continue For
                End If

                Dim arbName As String = GetGridCellText(DGRow, "ArbName")
                Dim engName As String = GetGridCellText(DGRow, "EngName")
                If arbName = "" OrElse engName = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                        ObjNavigationHandler.SetLanguage(Page, " Please enter Arabic and English names / برجاء إدخال الاسم العربي والاسم الانجليزي ") & " - " & rowNumber)
                    Return False
                End If

                Dim rowData As OfficialVacationRowData
                If Not TryGetOfficialVacationRowData(DGRow, rowNumber, rowData) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                        ObjNavigationHandler.SetLanguage(Page, " Please enter valid dates in row / برجاء إدخال تواريخ صحيحة بالصف ") & rowNumber)
                    Return False
                End If

                If rowData.ToDate <= rowData.FromDate Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                        ObjNavigationHandler.SetLanguage(Page, " Please review and adjust dates / برجاء مراجعة واعادة ضبط التواريخ ") & " - " & rowNumber)
                    Return False
                End If

                If hasYearRange AndAlso (rowData.FromDate < yearFromDate OrElse rowData.ToDate > yearToDate) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                        ObjNavigationHandler.SetLanguage(Page, " Dates must be within the selected year / ادخال تواريخ فى سنه غير المختارة ") & " - " & rowNumber)
                    Return False
                End If

                ' During save, do not block because of already-existing prepared rows.
                ' Apply this check only for newly-added rows.
                Dim isNewRow As Boolean = IsNothing(DGRow.Cells.FromKey("ID").Value) OrElse DGRow.Cells.FromKey("ID").Value.ToString().Trim() = ""
                If isNewRow Then
                    Dim lockMessage As String = String.Empty
                    If Not CanModifyGridRow(DGRow, lockMessage) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, lockMessage))
                        Return False
                    End If
                End If

                If GetGridIsRamadanBooleanValue(DGRow) Then
                    isRamadanRowsCount += 1
                    If isRamadanRowsCount > 1 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                            ObjNavigationHandler.SetLanguage(Page, "Only one row can be marked as Ramadan / لا يمكن اختيار شهر رمضان لاكثر من سطر"))
                        Return False
                    End If
                End If

                rowDataList.Add(rowData)
            Next

            For i As Integer = 0 To rowDataList.Count - 1
                For j As Integer = i + 1 To rowDataList.Count - 1
                    If IsDuplicateOfficialVacationName(rowDataList(i), rowDataList(j)) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                            ObjNavigationHandler.SetLanguage(Page, " Duplicate official holiday name within the same year / تكرار العيد الرسمى - نفس الاسم داخل السنة "))
                        Return False
                    End If

                    If DatesOverlap(rowDataList(i).FromDate, rowDataList(i).ToDate, rowDataList(j).FromDate, rowDataList(j).ToDate) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                            ObjNavigationHandler.SetLanguage(Page, " There is another event that overlaps with the specified period / توجد مناسبة أخرى تتداخل مع الفترة المحددة "))
                        Return False
                    End If
                Next
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function DeleteSelectedOfficialVacationRow() As Boolean
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsHrsEmployeeOfficialVacations.ConnectionString)

        If hfSelectedRowIndex.Value = "" OrElse hfSelectedRowIndex.Value = "-1" Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please select a row to delete / برجاء اختيار السجل المراد حذفه"))
            Return False
        End If

        Dim rowIndex As Integer
        If Not Integer.TryParse(hfSelectedRowIndex.Value, rowIndex) OrElse rowIndex < 0 OrElse rowIndex >= UwgSearchEmployees.Rows.Count Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please select a row to delete / برجاء اختيار السجل المراد حذفه"))
            Return False
        End If

        Dim selectedRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow = UwgSearchEmployees.Rows(rowIndex)
        Dim lockMessage As String = String.Empty
        If Not CanModifyGridRow(selectedRow, lockMessage) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, lockMessage))
            Return False
        End If

        If Not IsNothing(selectedRow.Cells.FromKey("ID").Value) AndAlso selectedRow.Cells.FromKey("ID").Value.ToString() <> "" Then
            Dim rowId As Integer = CInt(selectedRow.Cells.FromKey("ID").Value)
            Dim sqlCommand As String = "DELETE FROM [dbo].[hrs_OfficialVacations] WHERE ID=" & rowId
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsHrsEmployeeOfficialVacations.ConnectionString, CommandType.Text, sqlCommand)
        End If

        UwgSearchEmployees.Rows.Remove(selectedRow)
        RenumberOfficialVacationGridLines()
        EnsureOfficialVacationAddNewRow()

        hfSelectedRowIndex.Value = "-1"
        hfSelectedRowID.Value = ""
        Return True
    End Function

    Private Sub RenumberOfficialVacationGridLines()
        Dim lineNum As Integer = 0
        For Each dgRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
            If IsNothing(dgRow.Cells.FromKey("Year").Value) Then
                Continue For
            End If
            lineNum += 1
            If dgRow.Cells.FromKey("LineNumber") IsNot Nothing Then
                dgRow.Cells.FromKey("LineNumber").Value = lineNum
            End If
        Next
    End Sub

    Private Sub EnsureOfficialVacationAddNewRow()
        If UwgSearchEmployees.Rows.Count = 0 Then
            UwgSearchEmployees.Rows.Add()
            Return
        End If

        Dim lastRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow = UwgSearchEmployees.Rows(UwgSearchEmployees.Rows.Count - 1)
        If Not IsNothing(lastRow.Cells.FromKey("Year").Value) OrElse ShouldSaveGridRow(lastRow) Then
            UwgSearchEmployees.Rows.Add()
        End If
    End Sub

    Private Function ValidateYearCanBeDeleted() As Boolean
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsHrsEmployeeOfficialVacations.ConnectionString)
        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
            If IsNothing(DGRow.Cells.FromKey("ID").Value) OrElse DGRow.Cells.FromKey("ID").Value.ToString() = "" Then
                Continue For
            End If

            Dim lockMessage As String = String.Empty
            If Not CanModifyGridRow(DGRow, lockMessage) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, lockMessage))
                Return False
            End If
        Next
        Return True
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clshrs_OfficialVacations(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            'ddlFiscalYear.SelectedItem.Text = clsHrsEmployeeOfficialVacations.Year
            'txtEngName.Text = clsHrsEmployeeOfficialVacations.EngName
            'txtArbName.Text = clsHrsEmployeeOfficialVacations.ArbName
            Dim mFindDataset As New DataSet
            mFindDataset = ClsUser.Find("Year=" & ddlFiscalYear.SelectedItem.Text)

            UwgSearchEmployees.DataSource = mFindDataset.Tables(0)
            UwgSearchEmployees.DataBind()
            UwgSearchEmployees.Rows.Add()
            Dim lineNum As Integer = 0
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                If IsNothing(DGRow.Cells(1).Value) Then
                    Continue For
                End If
                lineNum += 1
                DGRow.Cells.FromKey("LineNumber").Value = lineNum
                If Not IsNothing(DGRow.Cells.FromKey("VacationTypeID").Value) AndAlso DGRow.Cells.FromKey("VacationTypeID").Value.ToString() <> "" Then
                    DGRow.Cells.FromKey("VacationTypeID").Value = Convert.ToInt32(DGRow.Cells.FromKey("VacationTypeID").Value)
                End If
                If Not IsNothing(DGRow.Cells.FromKey("eventType").Value) AndAlso DGRow.Cells.FromKey("eventType").Value.ToString() <> "" Then
                    DGRow.Cells.FromKey("eventType").Value = Convert.ToInt32(DGRow.Cells.FromKey("eventType").Value)
                End If
                ApplyEventTypeRules(DGRow)
                LockPreparedPeriodRow(DGRow)
            Next
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsHrsEmployeeOfficialVacations.ConnectionString)
            If (UwgSearchEmployees.Rows.Count > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
                'UwgSearchEmployees.Rows.Add()
                'UwgSearchEmployees.Rows(0).Cells("LineNum").Value = 1

            End If
            SetToolBarPermission(Me, clsHrsEmployeeOfficialVacations.ConnectionString, clsHrsEmployeeOfficialVacations.DataBaseUserRelatedID, clsHrsEmployeeOfficialVacations.GroupID, StrMode)
            SetToolBarRecordPermission(Me, clsHrsEmployeeOfficialVacations.ConnectionString, clsHrsEmployeeOfficialVacations.DataBaseUserRelatedID, clsHrsEmployeeOfficialVacations.GroupID, clsHrsEmployeeOfficialVacations.Table, clsHrsEmployeeOfficialVacations.ID)
            If Not clsHrsEmployeeOfficialVacations.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(clsHrsEmployeeOfficialVacations.ID)
            End If
            Return True
        Catch ex As Exception
        End Try
    End Function
    Public Function SetToolBarPermission(ByVal pgSender As System.Web.UI.Page, ByVal ConnectionString As String, ByVal UserID As Integer, ByVal GroupID As Integer, ByVal Mode As String) As Boolean
        Dim StrCommandStored As String
        Dim StrFormName As String
        Dim ObjDataSet As New Data.DataSet
        Try
            StrFormName = pgSender.Form.ID
            StrCommandStored = "hrs_GetFormsPermissions"
            ObjDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, StrCommandStored, UserID, GroupID, StrFormName)
            If Venus.Shared.DataHandler.CheckValidDataObject(ObjDataSet) Then
                With ObjDataSet.Tables(0).Rows(0)
                    ImageButton_Delete.Enabled = .Item("AllowDelete")
                    ImageButton_Print.Enabled = .Item("AllowPrint")
                    Select Case Mode
                        Case "N", "R"
                            ImageButton_Save.Enabled = .Item("AllowAdd")
                            ImageButton_SaveN.Enabled = .Item("AllowAdd")
                            LinkButton_SaveN.Enabled = .Item("AllowAdd")
                        Case "E"
                            ImageButton_Save.Enabled = .Item("AllowEdit")
                            ImageButton_SaveN.Enabled = .Item("AllowEdit")
                            LinkButton_SaveN.Enabled = .Item("AllowAdd")
                    End Select
                End With
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function SetToolBarRecordPermission(ByVal pgSender As System.Web.UI.Page, ByVal ConnectionString As String, ByVal UserID As Integer, ByVal GroupID As Integer, ByVal StrTableName As String, ByVal RecordID As Integer) As Boolean
        Dim StrCommandStored As String
        Dim StrFormName As String
        Dim ObjDataSet As New Data.DataSet
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Try
            StrFormName = pgSender.Form.ID
            StrCommandStored = "hrs_GetRecordsPermissions"
            ObjDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, StrCommandStored, UserID, GroupID, Replace(StrTableName, " ", ""), RecordID)
            If Venus.Shared.DataHandler.CheckValidDataObject(ObjDataSet) Then
                With ObjDataSet.Tables(0).Rows(0)

                    If ImageButton_Save.Enabled = True And .Item("CanEdit") = True Then
                        ImageButton_Save.Enabled = Not .Item("CanEdit")
                        ImageButton_SaveN.Enabled = Not .Item("CanEdit")
                        LinkButton_SaveN.Enabled = Not .Item("CanEdit")
                    End If

                    If ImageButton_Delete.Enabled = True And .Item("CanDelete") = True Then
                        ImageButton_Delete.Enabled = Not .Item("CanDelete")
                    End If

                    If ImageButton_Print.Enabled = True And .Item("CanPrint") = True Then
                        ImageButton_Print.Enabled = Not .Item("CanPrint")
                    End If
                End With
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SetToolbarSetting(ByVal ptrType As String, ByVal ClsClass As Object, ByVal intID As Integer) As Boolean
        Try
            Select Case ptrType
                Case "N", "R"
                    ddlFiscalYear.SelectedItem.Text = String.Empty
                    ImageButton_First.Visible = False
                    ImageButton_Back.Visible = False
                    ImageButton_Next.Visible = False
                    ImageButton_Last.Visible = False
                    ImageButton_Delete.Enabled = False
                    ImageButton_Properties.Visible = False
                    LinkButton_Properties.Visible = False
                    ImageButton_Remarks.Visible = False
                    LinkButton_Remarks.Visible = False

                Case "D"
                    clsHrsEmployeeOfficialVacations.Find("ID=" & intID)
                    GetValues()
                    ddlFiscalYear.Enabled = False
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    clsHrsEmployeeOfficialVacations.Find("ID=" & intID)
                    GetValues()
                    ddlFiscalYear.Enabled = False
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With clsHrsEmployeeOfficialVacations
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                    ImageButton_Delete.Enabled = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation() As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
        Try
            With clsHrsEmployeeOfficialVacations
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Page, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
        If IntId > 0 Then
            clsHrsEmployeeOfficialVacations.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
        Try

            GetValues()

        Catch ex As Exception
        End Try
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        clsHrsEmployeeOfficialVacations.Clear()
        GetValues()
        ImageButton_Delete.Enabled = False

        Venus.Shared.Web.ClientSideActions.SetFocus(Page, ddlFiscalYear, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean


        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Page)
        clsHrsEmployeeOfficialVacations.Find(" Year = '" & ddlFiscalYear.SelectedItem.Text & "'")
        Dim recordID As Integer = clsHrsEmployeeOfficialVacations.ID
        If (recordID > 0) Then
            Dim clsForms As New ClsSys_Forms(Page)
            clsForms.Find(" code = REPLACE('" & formName & "',' ','')")
            Dim clsFormsControls As New Clssys_FormsControls(Page)
            clsFormsControls.Find(" FormID=" & clsForms.ID)
            Dim tab As Data.DataTable = clsFormsControls.DataSet.Tables(0).Copy()
            For Each row As Data.DataRow In tab.Rows
                clsFormsControls.Find(" FormID=" & clsForms.ID & " And Name='" & row("Name") & "'")
                Dim sys_Fields As New Clssys_Fields(Page)
                sys_Fields.Find(" ID=" & clsFormsControls.FieldID)
                If (sys_Fields.FieldName.Trim() = "Code" Or sys_Fields.FieldName.Trim() = "Number" Or sys_Fields.FieldName.Trim() = "ID") Then
                    Continue For
                End If
                Dim currCtrl As Control = Me.FindControl(row("Name"))
                Dim bIsArabic As Boolean = IIf(IsDBNull(row("IsArabic")), False, row("IsArabic"))
                If (bIsArabic Or row("Name").ToString.ToLower.IndexOf("arb") > -1) And (TypeOf (currCtrl) Is TextBox) Then
                    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedulesForArabicText(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                ElseIf (TypeOf (currCtrl) Is TextBox) Then
                    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebSchedule.WebDateChooser) Then
                    CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                End If
            Next
        End If
    End Sub
    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, clsHrsEmployeeOfficialVacations.Table) = True Then
            Dim StrTablename As String
            clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
            StrTablename = clsHrsEmployeeOfficialVacations.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID &
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID &
                                    " And sys_OtherFields.CancelDate is Null ")
            objDS = clsOtherFieldsData.DataSet
            name.Text = ""
            realname.Text = ""
            If objDS.Tables(0).Rows.Count > 0 Then
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "U", objDS, "Interfaces_frmDocumentsTypes")
            Else
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "A", objDS, "Interfaces_frmDocumentsTypes")
            End If
        End If
    End Function

    Protected Sub UwgSearchEmployees_DeleteRow(sender As Object, e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles UwgSearchEmployees.DeleteRow
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
        If e.Row.Cells.FromKey("ID").Value <> Nothing Then
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsHrsEmployeeOfficialVacations.ConnectionString)
            Dim validationMessage As String = String.Empty
            If Not CanModifyGridRow(e.Row, validationMessage) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, validationMessage))
                Exit Sub
            End If

            clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
            Dim SqlCommand As String = String.Empty

            SqlCommand = "DELETE FROM [dbo].[hrs_OfficialVacations] WHERE ID=" & e.Row.Cells.FromKey("ID").Value
            Dim cmd As New SqlClient.SqlCommand
            cmd.CommandText = SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.Connection = New SqlClient.SqlConnection(clsHrsEmployeeOfficialVacations.ConnectionString)
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
            cmd.Connection.Close()
        End If
    End Sub

    Public Function GetDropDownListGrid() As Boolean
        Try
            If UwgSearchEmployees Is Nothing OrElse UwgSearchEmployees.DisplayLayout.Bands.Count = 0 Then
                Return False
            End If

            Dim ConnectionString As String = ConfigurationManager.AppSettings("Connstring").ToString()
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)

            Dim eventTypeCol = UwgSearchEmployees.DisplayLayout.Bands(0).Columns.FromKey("eventType")
            If eventTypeCol IsNot Nothing Then
                If eventTypeCol.ValueList Is Nothing Then
                    eventTypeCol.ValueList = New Infragistics.WebUI.UltraWebGrid.ValueList()
                End If
                eventTypeCol.ValueList.ValueListItems.Clear()
                eventTypeCol.ValueList.ValueListItems.Add(1, ObjNavigationHandler.SetLanguage(Page, "1-Vacation/1-إجازة رسمية"))
                eventTypeCol.ValueList.ValueListItems.Add(2, ObjNavigationHandler.SetLanguage(Page, "2-Event/2-مناسبة"))
            End If

            Dim vacationTypeCol = UwgSearchEmployees.DisplayLayout.Bands(0).Columns.FromKey("VacationTypeID")
            If vacationTypeCol IsNot Nothing Then
                If vacationTypeCol.ValueList Is Nothing Then
                    vacationTypeCol.ValueList = New Infragistics.WebUI.UltraWebGrid.ValueList()
                End If
                vacationTypeCol.ValueList.ValueListItems.Clear()
                Dim strselect2 As String = "select ID, Code,EngName,ArbName from hrs_VacationsTypes where IsOfficial=1"
                Dim DSOfficialvacations As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect2)
                For Each Row As Data.DataRow In DSOfficialvacations.Tables(0).Rows
                    vacationTypeCol.ValueList.ValueListItems.Add(Row("ID"), Row("Code") & " - " & ObjNavigationHandler.SetLanguage(Page, "" & Row("EngName") & "/ " & Row("ArbName") & ""))
                Next
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ApplyEventTypeRules(ByVal row As Infragistics.WebUI.UltraWebGrid.UltraGridRow)
        If row Is Nothing Then Exit Sub
        If row.Cells.FromKey("eventType") Is Nothing Then Exit Sub

        Dim eventTypeVal As String = String.Empty
        If Not IsNothing(row.Cells.FromKey("eventType").Value) Then
            eventTypeVal = row.Cells.FromKey("eventType").Value.ToString()
        End If

        If row.Cells.FromKey("VacationTypeID") IsNot Nothing Then
            If eventTypeVal = "2" Then
                row.Cells.FromKey("VacationTypeID").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                row.Cells.FromKey("VacationTypeID").Value = Nothing
                row.Cells.FromKey("VacationTypeID").Text = String.Empty
            ElseIf eventTypeVal = "1" Then
                row.Cells.FromKey("VacationTypeID").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes
            End If
        End If

        If row.Cells.FromKey("IsRamadan") IsNot Nothing Then
            NormalizeIsRamadanCell(row.Cells.FromKey("IsRamadan"))
            If eventTypeVal = "1" Then
                row.Cells.FromKey("IsRamadan").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                row.Cells.FromKey("IsRamadan").Value = False
            ElseIf eventTypeVal = "2" Then
                row.Cells.FromKey("IsRamadan").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes
            End If
        End If
    End Sub

    Private Function GetGridCellText(ByVal row As Infragistics.WebUI.UltraWebGrid.UltraGridRow, ByVal columnKey As String) As String
        If row Is Nothing OrElse row.Cells.FromKey(columnKey) Is Nothing Then
            Return String.Empty
        End If
        If row.Cells.FromKey(columnKey).Text Is Nothing Then
            Return String.Empty
        End If
        Return row.Cells.FromKey(columnKey).Text.Trim()
    End Function

    Private Function GetGridEventTypeValue(ByVal row As Infragistics.WebUI.UltraWebGrid.UltraGridRow) As String
        If row Is Nothing OrElse row.Cells.FromKey("eventType") Is Nothing OrElse IsNothing(row.Cells.FromKey("eventType").Value) Then
            Return String.Empty
        End If
        Return row.Cells.FromKey("eventType").Value.ToString()
    End Function

    Private Function GetGridEventTypeSqlValue(ByVal row As Infragistics.WebUI.UltraWebGrid.UltraGridRow) As String
        Dim eventTypeVal As String = GetGridEventTypeValue(row)
        If eventTypeVal = String.Empty Then
            Return "NULL"
        End If
        Return eventTypeVal
    End Function

    Private Function GetGridVacationTypeSqlValue(ByVal row As Infragistics.WebUI.UltraWebGrid.UltraGridRow, ByVal eventTypeValue As String) As String
        If eventTypeValue = "2" Then
            Return "NULL"
        End If
        If row.Cells.FromKey("VacationTypeID") Is Nothing OrElse IsNothing(row.Cells.FromKey("VacationTypeID").Value) OrElse row.Cells.FromKey("VacationTypeID").Value.ToString() = "" Then
            Return "NULL"
        End If
        Return row.Cells.FromKey("VacationTypeID").Value.ToString()
    End Function

    Private Function ShouldSaveGridRow(ByVal row As Infragistics.WebUI.UltraWebGrid.UltraGridRow) As Boolean
        Dim eventTypeVal As String = GetGridEventTypeValue(row)
        If eventTypeVal = "2" Then
            Return row.Cells.FromKey("FromDate").Text.Trim() <> String.Empty
        End If
        If eventTypeVal = "1" Then
            Return Not IsNothing(row.Cells.FromKey("VacationTypeID").Value) AndAlso row.Cells.FromKey("VacationTypeID").Value.ToString() <> ""
        End If
        Return Not IsNothing(row.Cells.FromKey("VacationTypeID").Value) AndAlso row.Cells.FromKey("VacationTypeID").Value.ToString() <> ""
    End Function

    Private Function GetGridIsRamadanSqlValue(ByVal row As Infragistics.WebUI.UltraWebGrid.UltraGridRow) As Integer
        If GetGridEventTypeValue(row) = "1" Then
            Return 0
        End If
        If row.Cells.FromKey("IsRamadan") Is Nothing Then
            Return 0
        End If
        Return If(GetGridIsRamadanBooleanValue(row), 1, 0)
    End Function

    Private Function HasAnotherRamadanMarked(ByVal excludedRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow) As Boolean
        For Each dgRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
            If Object.ReferenceEquals(dgRow, excludedRow) Then
                Continue For
            End If
            If Not ShouldSaveGridRow(dgRow) Then
                Continue For
            End If
            If GetGridIsRamadanBooleanValue(dgRow) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function GetGridIsRamadanBooleanValue(ByVal row As Infragistics.WebUI.UltraWebGrid.UltraGridRow) As Boolean
        If row Is Nothing OrElse row.Cells.FromKey("IsRamadan") Is Nothing Then
            Return False
        End If
        Return GetIsRamadanBooleanValue(row.Cells.FromKey("IsRamadan").Value)
    End Function

    Private Function GetIsRamadanBooleanValue(ByVal cellValue As Object) As Boolean
        If IsNothing(cellValue) OrElse cellValue.ToString().Trim() = "" Then
            Return False
        End If
        If TypeOf cellValue Is Boolean Then
            Return CBool(cellValue)
        End If
        Dim parsed As Boolean
        If Boolean.TryParse(cellValue.ToString(), parsed) Then
            Return parsed
        End If
        If cellValue.ToString() = "1" Then
            Return True
        End If
        Return False
    End Function

    Private Sub NormalizeIsRamadanCell(ByVal cell As Infragistics.WebUI.UltraWebGrid.UltraGridCell)
        If cell Is Nothing Then Exit Sub
        cell.Value = GetIsRamadanBooleanValue(cell.Value)
    End Sub

    Private Structure OfficialVacationRowData
        Public RowNumber As Integer
        Public EventType As String
        Public VacationTypeID As String
        Public ArbName As String
        Public FromDate As Date
        Public ToDate As Date
    End Structure

    Private Function TryGetOfficialVacationRowData(ByVal row As Infragistics.WebUI.UltraWebGrid.UltraGridRow, ByVal rowNumber As Integer, ByRef rowData As OfficialVacationRowData) As Boolean
        Try
            rowData = New OfficialVacationRowData()
            rowData.RowNumber = rowNumber
            rowData.EventType = GetGridEventTypeValue(row)
            rowData.VacationTypeID = String.Empty
            If row.Cells.FromKey("VacationTypeID") IsNot Nothing AndAlso Not IsNothing(row.Cells.FromKey("VacationTypeID").Value) Then
                rowData.VacationTypeID = row.Cells.FromKey("VacationTypeID").Value.ToString()
            End If
            rowData.ArbName = GetGridCellText(row, "ArbName")
            rowData.FromDate = CDate(row.Cells.FromKey("FromDate").Text)
            rowData.ToDate = CDate(row.Cells.FromKey("ToDate").Text)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function TryGetSelectedFiscalYearDateRange(ByRef yearFromDate As Date, ByRef yearToDate As Date) As Boolean
        Try
            If ddlFiscalYear.SelectedValue = "" OrElse ddlFiscalYear.SelectedValue = "0" Then
                Return False
            End If

            Dim sql As String = "SELECT MIN(FromDate) AS YearFromDate, MAX(ToDate) AS YearToDate " &
                                "FROM sys_FiscalYearsPeriods " &
                                "WHERE ISNULL(CancelDate,'')='' AND FiscalYearID=" & ddlFiscalYear.SelectedValue
            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsHrsEmployeeOfficialVacations.ConnectionString, CommandType.Text, sql)
            If ds Is Nothing OrElse ds.Tables(0).Rows.Count = 0 OrElse IsDBNull(ds.Tables(0).Rows(0)("YearFromDate")) Then
                Return False
            End If

            yearFromDate = CDate(ds.Tables(0).Rows(0)("YearFromDate"))
            yearToDate = CDate(ds.Tables(0).Rows(0)("YearToDate"))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function IsDuplicateOfficialVacationName(ByVal firstRow As OfficialVacationRowData, ByVal secondRow As OfficialVacationRowData) As Boolean
        If firstRow.EventType = "1" AndAlso secondRow.EventType = "1" Then
            Return firstRow.VacationTypeID <> "" AndAlso firstRow.VacationTypeID = secondRow.VacationTypeID
        End If

        If firstRow.EventType = "2" AndAlso secondRow.EventType = "2" Then
            Return firstRow.ArbName <> "" AndAlso String.Equals(firstRow.ArbName, secondRow.ArbName, StringComparison.OrdinalIgnoreCase)
        End If

        Return False
    End Function

    Private Function DatesOverlap(ByVal firstFromDate As Date, ByVal firstToDate As Date, ByVal secondFromDate As Date, ByVal secondToDate As Date) As Boolean
        Return firstFromDate <= secondToDate AndAlso secondFromDate <= firstToDate
    End Function

    Private Function CanModifyGridRow(ByVal row As Infragistics.WebUI.UltraWebGrid.UltraGridRow, ByRef message As String) As Boolean
        message = "The event cannot be deleted because the period is prepared / لا يمكن حذف المناسبة لان الفترة مجهزة"
        Try
            If row.Cells.FromKey("FromDate") Is Nothing OrElse row.Cells.FromKey("ToDate") Is Nothing Then
                Return True
            End If
            If row.Cells.FromKey("FromDate").Text.Trim() = "" OrElse row.Cells.FromKey("ToDate").Text.Trim() = "" Then
                Return True
            End If

            Dim fromDate As Date = CDate(row.Cells.FromKey("FromDate").Text)
            Dim toDate As Date = CDate(row.Cells.FromKey("ToDate").Text)
            Return Not IsDateRangeInPreparedPeriod(fromDate, toDate)
        Catch ex As Exception
            Return True
        End Try
    End Function

    Private Function IsDateRangeInPreparedPeriod(ByVal fromDate As Date, ByVal toDate As Date) As Boolean
        Try
            Dim sql As String = "SET DATEFORMAT DMY; SELECT COUNT(1) " &
                                "FROM sys_FiscalYearsPeriods FYP " &
                                "WHERE ISNULL(FYP.CancelDate,'')='' " &
                                "AND FYP.FromDate <= '" & toDate.ToString("dd/MM/yyyy") & "' " &
                                "AND FYP.ToDate >= '" & fromDate.ToString("dd/MM/yyyy") & "' " &
                                "AND EXISTS (SELECT 1 FROM hrs_EmployeesTransactions ET " &
                                "WHERE ET.FiscalYearPeriodID = FYP.ID " &
                                "AND ISNULL(ET.CancelDate,'')='' and PrepareType='n' )"
            Dim count As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsHrsEmployeeOfficialVacations.ConnectionString, CommandType.Text, sql)
            Return Convert.ToInt32(count) > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub LockPreparedPeriodRow(ByVal row As Infragistics.WebUI.UltraWebGrid.UltraGridRow)
        Dim lockMessage As String = String.Empty
        If Not CanModifyGridRow(row, lockMessage) Then
            For Each cell As Infragistics.WebUI.UltraWebGrid.UltraGridCell In row.Cells
                If cell.Column.Key <> "LineNumber" Then
                    cell.AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                End If
            Next
        End If
    End Sub

#End Region
End Class
