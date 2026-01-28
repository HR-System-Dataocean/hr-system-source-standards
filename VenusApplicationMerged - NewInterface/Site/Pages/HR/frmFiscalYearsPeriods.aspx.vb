Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmFiscalYearsPeriods
    Inherits MainPage
#Region "Public Decleration"
    Private ClsFiscalYearsPeriods As Clssys_FiscalYearsPeriods
    Private ClsFiscalYears As Clssys_FiscalYears
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Const csOtherFields = 11
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        clsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Me.Page)
        ClsFiscalYears = New Clssys_FiscalYears(Me.Page)
        Dim clsNavigation = New Venus.Shared.Web.NavigationHandler(ClsFiscalYears.ConnectionString)
        Try
            Session("uwgFiscalYearsPeriods") = uwgFiscalYearsPeriods
            'ClsFiscalYearsPeriods.AddNotificationOnChange(Me.Page)
            ImageButton_Delete.Enabled = True
            If Not IsPostBack Then
                Page.Session.Add("UserID", ClsFiscalYears.DataBaseUserRelatedID)
                Page.Session.Add("ConnectionString", ClsFiscalYears.ConnectionString)
                ClsFiscalYears.GetDropDown(ddlFiscalYear)
                If ddlFiscalYear.Items.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, clsNavigation.SetLanguage(Page, "You must enter at least one fiscal year/يجب أن تدخل على الأقل سنة مالية"))
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    Exit Sub
                End If
                ddlFiscalYear.SelectedIndex = 0
                Load_DataGrid(ddlFiscalYear.SelectedValue)
                Get_New_Dates(ddlFiscalYear.SelectedValue)
                txtCode.Value = ""
                txtRowIndex.Value = "-1"
                txtRowID.Value = "-1"
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                GetFormPermission("frmFiscalYearsPeriods")

                Dim Clscompanies As New Clssys_Companies(Page)
                Clscompanies.Find("ID = " & Clscompanies.MainCompanyID)
                If (Clscompanies.IsHigry = True) Then
                    uwgFiscalYearsPeriods.Columns(4).Hidden = True
                    uwgFiscalYearsPeriods.Columns(5).Hidden = True

                    uwgFiscalYearsPeriods.Columns(11).Hidden = False
                    uwgFiscalYearsPeriods.Columns(12).Hidden = False
                    txtIsHijri.Value = "1"
                Else
                    uwgFiscalYearsPeriods.Columns(4).Hidden = False
                    uwgFiscalYearsPeriods.Columns(5).Hidden = False

                    uwgFiscalYearsPeriods.Columns(11).Hidden = True
                    uwgFiscalYearsPeriods.Columns(12).Hidden = True
                    txtIsHijri.Value = "0"
                End If
            End If

            If clsFiscalYearsPeriods.Find("ID=" & Val(txtCode.Value)) Then
                SetScreenInformation("E")
            Else
                SetScreenInformation("N")
            End If

            uwgFiscalYearsPeriods.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect
            uwgFiscalYearsPeriods.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            ddlFiscalYear.Attributes.Add("onchange", "ChangeIsDataChanged()")
            SetToolBarPermission(Me, ClsFiscalYearsPeriods.ConnectionString, ClsFiscalYearsPeriods.DataBaseUserRelatedID, ClsFiscalYearsPeriods.GroupID, "N")
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(clsFiscalYearsPeriods.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, clsFiscalYearsPeriods.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Delete.Command, ImageButton_Save.Command
        ClsFiscalYears = New Clssys_FiscalYears(Me.Page)
        ClsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsFiscalYears.ConnectionString)

        Select Case e.CommandArgument
            Case "New"
                'AfterOperation()
                Dim FYPID As Integer
                If uwgFiscalYearsPeriods.Rows.Count > 0 Then
                    Dim rowFYP As Object = uwgFiscalYearsPeriods.Rows.GetItem(0)
                    FYPID = rowFYP.Cells.All(0).Text
                    Dim FId As Integer = rowFYP.Cells.All(1).Text
                    Get_New_Dates(FId)
                    ImageButton_Delete.Enabled = False
                End If
                If (Val(txtRowIndex.Value) >= 0) Then
                    uwgFiscalYearsPeriods.Rows(Val(txtRowIndex.Value)).Selected = False
                End If
                uwgFiscalYearsPeriods.Rows(0).Selected = False
                txtRowIndex.Value = "-1"
                txtRowID.Value = "-1"
                Clear(ddlFiscalYear.SelectedValue)
                SetToolBarPermission(Me, ClsFiscalYearsPeriods.ConnectionString, ClsFiscalYearsPeriods.DataBaseUserRelatedID, ClsFiscalYearsPeriods.GroupID, "N")
                ImageButton_Delete.Enabled = False

            Case "Save"
                Dim ClsFiscalYearPeriods As New Clssys_FiscalYearsPeriods(Page)
                If (txtRowIndex.Value = "") Then Exit Sub
                Dim FromDatectrl As Date = IIf(WebDateChooser1.Text.Trim = "", Nothing, ClsFiscalYearPeriods.SetHigriDate(WebDateChooser1.Value))
                Dim ToDatectrl As Date = IIf(WebDateChooser2.Text.Trim = "", Nothing, ClsFiscalYearPeriods.SetHigriDate(WebDateChooser2.Value))
                If (IsNothing(FromDatectrl) Or IsNothing(ToDatectrl)) Then Exit Sub
                Dim IntIndex As Integer = Val(txtRowIndex.Value)
                Dim RowFromDate As Date
                Dim RowToDate As Date
                If (IntIndex >= 0) Then
                    RowFromDate = CDate(uwgFiscalYearsPeriods.Rows(IntIndex).Cells.FromKey("FromDate").Value)
                    RowToDate = CDate(uwgFiscalYearsPeriods.Rows(IntIndex).Cells.FromKey("ToDate").Value)
                End If
                Dim NextRowFromDate As New Date
                Dim NextRowToDate As New Date
                Dim PrevRowFromDate As New Date
                Dim PrevRowToDate As New Date
                Dim Row As New Infragistics.WebUI.UltraWebGrid.UltraGridRow
                Dim NextRow As New Infragistics.WebUI.UltraWebGrid.UltraGridRow
                Dim PreviousRow As New Infragistics.WebUI.UltraWebGrid.UltraGridRow
                Dim FirstRow As New Infragistics.WebUI.UltraWebGrid.UltraGridRow
                Dim LastRow As New Infragistics.WebUI.UltraWebGrid.UltraGridRow
                Dim FirstRowFromDate As New Date
                Dim FirstRowToDate As New Date
                Dim LastRowFromDate As New Date
                Dim LastRowToDate As New Date
                Row = uwgFiscalYearsPeriods.Rows(IntIndex)
                Dim clsNavigation = New Venus.Shared.Web.NavigationHandler(ClsFiscalYears.ConnectionString)
                lblGabNotify.Text = ""
                Select Case IntIndex
                    Case -1
                        FirstRow = uwgFiscalYearsPeriods.Rows(0)
                        LastRow = uwgFiscalYearsPeriods.Rows(uwgFiscalYearsPeriods.Rows.Count - 1)

                        If Not IsNothing(FirstRow) And Not IsNothing(LastRow) Then
                            FirstRowFromDate = ClsFiscalYearPeriods.SetHigriDate(FirstRow.Cells.FromKey("FromDate").Value)
                            FirstRowToDate = ClsFiscalYearPeriods.SetHigriDate(FirstRow.Cells.FromKey("ToDate").Value)
                            LastRowToDate = ClsFiscalYearPeriods.SetHigriDate(LastRow.Cells.FromKey("ToDate").Value)

                            If (FromDatectrl < FirstRowFromDate) Then
                                If (ToDatectrl = FirstRowFromDate.AddDays(-1)) Then
                                    txtRowIndex.Value = "-1"
                                    txtRowID.Value = "-1"
                                Else
                                    lblGabNotify.Text = clsNavigation.SetLanguage(Page, "Invalid Dates , Gab is not Recommended/التاريخ غير صحيح, الفراغات غير مقبولة")
                                    WebDateChooser2.Value = Nothing
                                    Exit Sub
                                End If
                            ElseIf FromDatectrl > FirstRowFromDate Then
                                txtRowIndex.Value = "-1"
                                txtRowID.Value = "-1"
                            End If
                        Else
                            txtRowIndex.Value = "-1"
                            txtRowID.Value = "-1"
                        End If
                    Case 0
                        NextRow = uwgFiscalYearsPeriods.Rows(IntIndex + 1)
                        If Not IsNothing(NextRow) Then
                            If Date.Compare(ToDatectrl, RowToDate) = 0 Then
                                Row.Cells.FromKey("FromDate").Value = FromDatectrl
                                Row.Cells.FromKey("EngName").Value = txtEngName.Text
                                Row.Cells.FromKey("ArbName").Value = txtArbName.Text
                                Exit Select
                            End If
                            NextRowToDate = NextRow.Cells.FromKey("ToDate").Value
                            Row.Cells.FromKey("FromDate").Value = FromDatectrl
                            Row.Cells.FromKey("ToDate").Value = ToDatectrl
                            NextRow.Cells.FromKey("FromDate").Value = ToDatectrl.AddDays(1)
                            Row.Cells.FromKey("EngName").Value = txtEngName.Text
                            Row.Cells.FromKey("ArbName").Value = txtArbName.Text
                        Else
                            Row.Cells.FromKey("FromDate").Value = FromDatectrl
                            Row.Cells.FromKey("ToDate").Value = ToDatectrl
                            Row.Cells.FromKey("EngName").Value = txtEngName.Text
                            Row.Cells.FromKey("ArbName").Value = txtArbName.Text
                        End If
                    Case uwgFiscalYearsPeriods.Rows.All.Length - 1
                        PreviousRow = uwgFiscalYearsPeriods.Rows(IntIndex - 1)
                        If Not IsNothing(PreviousRow) Then
                            If Date.Compare(FromDatectrl, RowFromDate) = 0 Then
                                Row.Cells.FromKey("ToDate").Value = ToDatectrl
                                Exit Select
                            End If
                            PrevRowFromDate = PreviousRow.Cells.FromKey("FromDate").Value
                            If (FromDatectrl < PrevRowFromDate.AddDays(2)) Then
                                lblGabNotify.Text = clsNavigation.SetLanguage(Page, "Invalid Dates , Gab is not Recommended/التاريخ غير صحيح, الفراغات غير مقبولة")
                                WebDateChooser1.Value = Nothing
                                Exit Sub
                            Else
                                Row.Cells.FromKey("FromDate").Value = FromDatectrl
                                Row.Cells.FromKey("ToDate").Value = ToDatectrl
                                PreviousRow.Cells.FromKey("ToDate").Value = FromDatectrl.AddDays(-1)
                                Row.Cells.FromKey("EngName").Value = txtEngName.Text
                                Row.Cells.FromKey("ArbName").Value = txtArbName.Text
                            End If
                        Else
                            Row.Cells.FromKey("FromDate").Value = FromDatectrl
                            Row.Cells.FromKey("ToDate").Value = ToDatectrl
                            Row.Cells.FromKey("EngName").Value = txtEngName.Text
                            Row.Cells.FromKey("ArbName").Value = txtArbName.Text
                        End If
                    Case Else
                        PreviousRow = uwgFiscalYearsPeriods.Rows(IntIndex - 1)
                        NextRow = uwgFiscalYearsPeriods.Rows(IntIndex + 1)
                        If Not IsNothing(NextRow) And Not IsNothing(PreviousRow) Then
                            PrevRowFromDate = PreviousRow.Cells.FromKey("FromDate").Value
                            PrevRowToDate = PreviousRow.Cells.FromKey("ToDate").Value
                            NextRowFromDate = NextRow.Cells.FromKey("FromDate").Value
                            NextRowToDate = NextRow.Cells.FromKey("ToDate").Value
                            If (ToDatectrl > NextRowToDate.AddDays(-2)) Then
                                lblGabNotify.Text = clsNavigation.SetLanguage(Page, "Invalid Dates , Gab is not Recommended/التاريخ غير صحيح, الفراغات غير مقبولة")
                            End If
                            If (FromDatectrl < PrevRowFromDate.AddDays(2)) Then
                                lblGabNotify.Text = clsNavigation.SetLanguage(Page, "Invalid Dates , Gab is not Recommended/التاريخ غير صحيح, الفراغات غير مقبولة")
                            End If
                            Row.Cells.FromKey("FromDate").Value = FromDatectrl
                            Row.Cells.FromKey("ToDate").Value = ToDatectrl
                            NextRow.Cells.FromKey("FromDate").Value = ToDatectrl.AddDays(1)
                            PreviousRow.Cells.FromKey("ToDate").Value = FromDatectrl.AddDays(-1)
                            Row.Cells.FromKey("EngName").Value = txtEngName.Text
                            Row.Cells.FromKey("ArbName").Value = txtArbName.Text
                            Row.Cells.FromKey("HFromDate").Value = ClsDataAcessLayer.GregToHijri(FromDatectrl.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
                            Row.Cells.FromKey("HToDate").Value = ClsDataAcessLayer.GregToHijri(ToDatectrl.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
                        End If
                End Select
                SavePart()
                Load_DataGrid(ddlFiscalYear.SelectedValue)
                AfterOperation()

            Case "Delete"
                If txtCode.Value.Trim <> "" Then
                    If CanDeleteFiscalPeriod(CInt(txtCode.Value)) Then
                        ClsFiscalYearsPeriods.Delete("sys_FiscalYearsPeriods.ID=" & CInt(txtCode.Value))
                        AfterOperation()
                    Else
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "an not Delete Fiscal Period / غير مسموح بالغاء الفترة المالية"))
                    End If
                End If
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
        End Select
    End Sub
    Protected Sub ddlFiscalYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFiscalYear.SelectedIndexChanged
        Change_ddlFiscalYear()
    End Sub

    'Protected Sub uwgFiscalYearsPeriods_ActiveRowChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgFiscalYearsPeriods.ActiveRowChange
    '    txtRowIndex.Value = e.Row.Index
    '    txtRowID.Value = e.Row.Cells(0).Value
    '    lblGabNotify.Text = ""

    '    ClsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Me)
    '    ClsFiscalYearsPeriods.Find("sys_FiscalYearsPeriods.ID=" & txtRowID.Value)
    '    GetValues()
    'End Sub
#End Region

#Region "Private Functions"
    Private Function SavePart() As Boolean
        ClsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Page)
        Dim intTemp As Integer = ddlFiscalYear.SelectedValue
        Dim IntIndex As Integer = Val(txtRowID.Value)
        Try
            clsFiscalYearsPeriods.Find(" sys_FiscalYearsPeriods.ID=" & IntIndex)
            If Not AssignValues() Then
                Exit Function
            End If

            If IntIndex > 0 Then
                clsFiscalYearsPeriods.Update(" sys_FiscalYearsPeriods.ID=" & IntIndex)
            Else
                clsFiscalYearsPeriods.Save()
            End If
            ClsFiscalYearsPeriods.LastRecord()
            clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
            clsMainOtherFields.CollectDataAndSave(value.Text, ClsFiscalYearsPeriods.Table, ClsFiscalYearsPeriods.ID)
            value.Text = ""
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function Load_DataGrid(ByVal intFYID As Integer) As Boolean
        uwgFiscalYearsPeriods.DataSource = Nothing
        uwgFiscalYearsPeriods.DataBind()
        ClsFiscalYearsPeriods.Find(" sys_FiscalYearsPeriods.FiscalYearID = " & intFYID & " And IsNull(sys_FiscalYearsPeriods.CancelDate,'')=''   Order by FromDate ")
        uwgFiscalYearsPeriods.DataSource = ClsFiscalYearsPeriods.DataSet.Tables(0).DefaultView
        uwgFiscalYearsPeriods.DataBind()
        lblGabNotify.Text = ""
    End Function
    Private Function Get_New_Dates(ByVal intFYID As Integer) As Boolean
        Dim ClsFiscalYearsPeriods As New Clssys_FiscalYearsPeriods(Page)
        Dim strDates As String = GetLastDate(intFYID)
        If strDates <> String.Empty Then
            Dim arrDates As String() = strDates.Split("$")
            WebDateChooser1.Value = arrDates(0)
            WebDateChooser2.Value = arrDates(1)
        Else
            WebDateChooser1.Value = ClsFiscalYearsPeriods.GetHigriDate(Date.Now)
            WebDateChooser2.Value = ClsFiscalYearsPeriods.GetHigriDate(Date.Now.AddDays(1))
        End If
    End Function
    Private Function Change_ddlFiscalYear() As Boolean
        ClsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Me.Page)
        If ddlFiscalYear.SelectedValue > 0 Then
            Load_DataGrid(ddlFiscalYear.SelectedValue)
        End If
        Clear(ddlFiscalYear.SelectedValue)
        Get_New_Dates(ddlFiscalYear.SelectedValue)
        txtRowIndex.Value = "-1"
        txtRowID.Value = "-1"

    End Function
    Private Function CanDeleteFiscalPeriod(ByVal IntFiscalPeriodID As Integer) As Boolean
        Dim ClsFiscalYearPeriodModules As New Clssys_FiscalYearsPeriodsModules(Page)
        Dim ClsFiscalYearPeriodObjects As New Clssys_FiscalYearsPeriodsObjects(Page)
        Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Page)
        If ClsEmployeesTransactions.Find(" FiscalYearPeriodID= " & IntFiscalPeriodID) Then
            Return False
        End If
        Return True
    End Function

    Private Function AssignValues() As Boolean
        Try
            Dim intFId As Integer
            If ddlFiscalYear.SelectedValue > 0 Then
                intFId = ddlFiscalYear.SelectedValue
            End If
            ClsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Page)
            With ClsFiscalYearsPeriods
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .FiscalYearID = ddlFiscalYear.SelectedValue
                .FromDate = ClsFiscalYearsPeriods.SetHigriDate(WebDateChooser1.Value)
                .ToDate = ClsFiscalYearsPeriods.SetHigriDate(WebDateChooser2.Value)
                .HFromDate = ClsDataAcessLayer.GregToHijri(.FromDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
                .HToDate = ClsDataAcessLayer.GregToHijri(.ToDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            End With
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")

        Dim FId As Integer = ddlFiscalYear.SelectedValue
        Dim blast As Boolean
        Dim bfirst As Boolean
        Dim TempClsFiscalYearsPeriods As New Clssys_FiscalYearsPeriods(Page)
        TempClsFiscalYearsPeriods.Find(" sys_FiscalYearsPeriods.ID=" & ClsFiscalYearsPeriods.ID)
        TempClsFiscalYearsPeriods.LastRecord(" FiscalYearID=" & FId & " And IsNull(sys_FiscalYearsPeriods.CancelDate,'')='' ")
        If (TempClsFiscalYearsPeriods.ID = ClsFiscalYearsPeriods.ID) Then
            blast = True
        End If
        TempClsFiscalYearsPeriods.Find(" sys_FiscalYearsPeriods.ID=" & ClsFiscalYearsPeriods.ID)
        TempClsFiscalYearsPeriods.FirstRecord(" FiscalYearID=" & FId & " And IsNull(sys_FiscalYearsPeriods.CancelDate,'')='' ")
        If (TempClsFiscalYearsPeriods.ID = ClsFiscalYearsPeriods.ID) Then
            bfirst = True
        End If
        Try
            SetToolBarDefaults()
            With ClsFiscalYearsPeriods
                txtCode.Value = .ID
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName

                Dim item As New System.Web.UI.WebControls.ListItem()
                Dim ClsFYear As New Clssys_FiscalYears(Page)
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(.ConnectionString)
                ClsFYear.Find(" ID= " & IIf(IsNothing(.FiscalYearID), 0, .FiscalYearID))
                If ClsFYear.ID > 0 Then
                    item.Value = .FiscalYearID
                    item.Text = ObjNavigationHandler.SetLanguage(Page, ClsFYear.EngName & "/" & ClsFYear.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Page, ClsFYear.ArbName & "/" & ClsFYear.EngName)
                    End If
                    If Not ddlFiscalYear.Items.Contains(item) Then
                        ddlFiscalYear.Items.Add(item)
                        ddlFiscalYear.SelectedValue = item.Value
                    Else
                        ddlFiscalYear.SelectedValue = .FiscalYearID
                    End If
                End If

                WebDateChooser1.Value = .FromDate
                WebDateChooser2.Value = .ToDate

            End With
            If Not ClsFiscalYearsPeriods.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsFiscalYearsPeriods.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsFiscalYearsPeriods.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsFiscalYearsPeriods.RegDate).Date
            End If
            If ClsFiscalYearsPeriods.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsFiscalYearsPeriods.CancelDate).Date
            End If
            If Not ClsFiscalYearsPeriods.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If

            If (ClsFiscalYearsPeriods.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsFiscalYearsPeriods.ConnectionString, ClsFiscalYearsPeriods.DataBaseUserRelatedID, ClsFiscalYearsPeriods.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsFiscalYearsPeriods.ConnectionString, ClsFiscalYearsPeriods.DataBaseUserRelatedID, ClsFiscalYearsPeriods.GroupID, ClsFiscalYearsPeriods.Table, ClsFiscalYearsPeriods.ID)
            If Not ClsFiscalYearsPeriods.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsFiscalYearsPeriods.ID)
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
                            
                        Case "E"
                            ImageButton_Save.Enabled = .Item("AllowEdit")

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
                    txtCode.Value = String.Empty

                    ImageButton_Delete.Enabled = False


                Case "D"
                    ClsFiscalYearsPeriods.Find("ID=" & intID)
                    GetValues()
                    ImageButton_Save.Visible = False

                Case "E"
                    ClsFiscalYearsPeriods.Find("ID=" & intID)
                    GetValues()
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsFiscalYearsPeriods
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
        ClsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Me)
        Try
            With ClsFiscalYearsPeriods
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
        ClsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Me)
        If IntId > 0 Then
            ClsFiscalYearsPeriods.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        Dim intFYID As Integer = ddlFiscalYear.SelectedValue
        ClsFiscalYearsPeriods.Clear()
        GetValues()
        Clear(intFYID)
        If intFYID > 0 Then
            Load_DataGrid(intFYID)
        End If
        Get_New_Dates(intFYID)

        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear(ByVal intFYID As Integer) As Boolean
        txtCode.Value = String.Empty
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        Get_New_Dates(intFYID)
        lblGabNotify.Text = ""
        If uwgFiscalYearsPeriods.Rows.Count > 0 Then
            uwgFiscalYearsPeriods.Rows(0).Selected = False
        End If
        txtRowIndex.Value = "-1"
        txtRowID.Value = "-1"

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Page)
        ClsFiscalYearsPeriods.Find(" code = '" & txtCode.Value & "'")
        Dim recordID As Integer = ClsFiscalYearsPeriods.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsFiscalYearsPeriods.Table) = True Then
            Dim StrTablename As String
            ClsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Me)
            StrTablename = ClsFiscalYearsPeriods.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID & _
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID & _
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

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetControlsStatus(ByVal intFYID As Integer, ByVal intFYPID As Integer) As String

        Dim blast As Boolean
        Dim bfirst As Boolean
        Dim WebDateChooser1Enabled As Boolean
        Dim WebDateChooser2Enabled As Boolean

        Dim dsFiscalYearsPeriods As New DataSet

        Find("sys_FiscalYearsPeriods", " FiscalYearID=" & intFYID & "  ORDER BY ID DESC", dsFiscalYearsPeriods)

        If (dsFiscalYearsPeriods.Tables(0).Rows(0).Item("ID") = intFYPID) Then
            blast = True
        End If
        Find("sys_FiscalYearsPeriods", " FiscalYearID=" & intFYID & "  ORDER BY ID ASC", dsFiscalYearsPeriods)

        If (dsFiscalYearsPeriods.Tables(0).Rows(0).Item("ID") = intFYPID) Then
            bfirst = True
        End If
        If (Not bfirst And Not blast) Then
            WebDateChooser1Enabled = False
            WebDateChooser2Enabled = False
        ElseIf (bfirst And blast) Then
            WebDateChooser1Enabled = True
            WebDateChooser2Enabled = True
        ElseIf bfirst Then
            WebDateChooser1Enabled = True
            WebDateChooser2Enabled = False
        ElseIf blast Then
            WebDateChooser1Enabled = False
            WebDateChooser2Enabled = True
        End If

        Return WebDateChooser1Enabled & "/" & WebDateChooser2Enabled

    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetLastDate(ByVal intFYID As Integer) As String
        Dim uwg As New Infragistics.WebUI.UltraWebGrid.UltraWebGrid
        uwg = HttpContext.Current.Session("uwgFiscalYearsPeriods")

        If uwg.Rows.Count > 0 Then
            Dim dtFromDate As Date = CDate(uwg.Rows(uwg.Rows.Count - 1).Cells(5).Value).AddDays(1)
            Dim dtToDate As Date = CDate(uwg.Rows(uwg.Rows.Count - 1).Cells(5).Value).AddDays(2)
            Return dtFromDate & "$" & dtToDate
        End If
        Return String.Empty
    End Function
    Public Shared Function Find(ByVal Table As String, ByVal Filter As String, ByRef DataSet As Data.DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Dim mSelectCommand = " Select * From " & Table
        Dim mSqlDataAdapter As New Data.SqlClient.SqlDataAdapter
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where " & Filter & " And CancelDate IS Null", " Where CancelDate IS Null")
            StrSelectCommand = StrSelectCommand '& orderByStr
            mSqlDataAdapter = New Data.SqlClient.SqlDataAdapter(StrSelectCommand, ConnStr)
            DataSet = New Data.DataSet
            mSqlDataAdapter.Fill(DataSet)
            If DataSet.Tables(0).Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
        End Try
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetRecordInfoAjax(ByVal recordID As Integer) As String
        'InitializeCulture()

        Dim dsContractsTransactions As New DataSet
        Dim dsUser As New DataSet
        Dim retStr As String = ",,"

        Find("sys_FiscalYearsPeriods", "ID=" & recordID, dsContractsTransactions)

        With dsContractsTransactions.Tables(0).Rows(0)

            If .Item("ID") > 0 Then
                retStr = ""
                If Not .Item("RegUserID") = Nothing Then

                    Find("sys_Users", "ID=" & .Item("RegUserID"), dsUser)

                    If dsUser.Tables(0).Rows.Count > 0 Then
                        retStr = dsUser.Tables(0).Rows(0).Item("EngName")
                    Else
                        retStr = ""
                    End If
                End If
                If Convert.ToDateTime(.Item("RegDate")).Date = Nothing Then
                    retStr &= ","
                Else
                    retStr &= "," & CDate(.Item("RegDate")).ToShortDateString()
                End If
                If IsDBNull(.Item("CancelDate")) Then
                    retStr &= ","
                Else
                    retStr &= "," & CDate(.Item("CancelDate")).ToShortDateString()
                End If
            End If
        End With

        Return retStr
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetRecordPermissionAjax(ByVal recordID As Integer) As String
        Dim StrRetStr As String = "1,1,1"
        Dim dsObjects As New DataSet
        Dim dsRecordsPermissions As New DataSet
        Dim ObjectsID As Int64

        Find("sys_Objects", "Code='sys_FiscalYearsPeriods'", dsObjects)

        ObjectsID = dsObjects.Tables(0).Rows(0).Item("ID")

        If ObjectsID > 0 And recordID > 0 Then

            Find("sys_RecordsPermissions", "ObjectID=" & ObjectsID & " And RecordID=" & recordID & " And UserID=" & HttpContext.Current.Session("UserID"), dsRecordsPermissions)

            If dsRecordsPermissions.Tables(0).Rows.Count > 0 Then

                With dsRecordsPermissions.Tables(0).Rows(0)
                    If .Item("ID") > 0 Then
                        StrRetStr = ""
                        StrRetStr = IIf(.Item("CanEdit"), "0", "1")
                        StrRetStr &= IIf(.Item("CanDelete"), ",0", ",1")
                        StrRetStr &= IIf(.Item("CanPrint"), ",0", ",1")
                    End If
                End With
            End If
        End If
        Return StrRetStr
    End Function
    <System.Web.Services.WebMethod()> _
    Private Shared Function GetFormPermission(ByVal frmCode As String) As Boolean
        Dim StrFormPermission As String = "1,1,1"
        Dim dsForms As New DataSet
        Dim dsFormsPermissions As New DataSet

        If Find("sys_Forms", " Code='" & frmCode & "'", dsForms) Then
            Find("sys_FormsPermissions", "FormID=" & dsForms.Tables(0).Rows(0).Item("ID") & " and UserID=" & HttpContext.Current.Session("UserID"), dsFormsPermissions)

            With dsFormsPermissions.Tables(0).Rows(0)
                If .Item("ID") > 0 Then
                    StrFormPermission = ""
                    If .Item("AllowEdit") Then
                        StrFormPermission = "0"
                    Else
                        StrFormPermission = "1"
                    End If
                    If .Item("AllowDelete") Then
                        StrFormPermission &= ",0"
                    Else
                        StrFormPermission &= ",1"
                    End If

                    If .Item("AllowPrint") Then
                        StrFormPermission &= ",0"
                    Else
                        StrFormPermission &= ",1"
                    End If
                End If
            End With
        End If
        'txtFormPermission.Value = StrFormPermission
    End Function
#End Region

End Class
