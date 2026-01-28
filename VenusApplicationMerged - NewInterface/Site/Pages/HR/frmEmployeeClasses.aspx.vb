Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmEmployeeClasses
    Inherits MainPage
#Region "Public Decleration"
    Dim ClsEmployeeClasses As Clshrs_EmployeeClasses
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim ClsProjects As New Clshrs_Projects(Page, "hrs_Projects")
        Dim clsTransactionsTypes = New Clshrs_TransactionsTypes(Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsProjects.ConnectionString)
        ClsEmployeeClasses = New Clshrs_EmployeeClasses(Me)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsEmployeeClasses.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsEmployeeClasses.ConnectionString)
                ClsEmployeeClasses.AddOnChangeEventToControls("frmEmployeeClasses", Page, UltraWebTab1)
                ClsProjects.GetDropDownList(DdlProjects, False)
                clsTransactionsTypes.GetDropDownList(DropDownList1, True, " CancelDate is null and Sign = -1 and IsPaid = 1")
                clsTransactionsTypes.GetDropDownList(DropDownList4, True, " CancelDate is null and Sign = -1 and IsPaid = 1")
                clsTransactionsTypes.GetDropDownList(DropDownList6, True, " CancelDate is null and Sign = 1 and IsPaid = 1")
                clsTransactionsTypes.GetDropDownList(DropDownList7, True, " CancelDate is null and Sign = 1 and IsPaid = 1")

                clsTransactionsTypes.GetDropDownList(ddlEOSCosting, True, " CancelDate is null and TransactionGroupID = 3")
                clsTransactionsTypes.GetDropDownList(ddlVacCosting, True, " CancelDate is null and TransactionGroupID = 3")
                clsTransactionsTypes.GetDropDownList(ddlHI, True, " CancelDate is null and TransactionGroupID = 3")
                clsTransactionsTypes.GetDropDownList(ddlTicketsCosting, True, " CancelDate is null and TransactionGroupID = 3")

                Setsetting(IntId)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                hdnLang.Value = objNav.SetLanguage(Page, "0/1")
                uwgVacationTypes.DataSource = ClsEmployeeClasses.GetAllVacationTypes()
                uwgVacationTypes.DataBind()

                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            End If
            DdlProjects.Attributes.Add("onBlur", "DDL_LostFocusToGrid('" & uwgVacationTypes.ID & "')")
            btnFormula.Attributes.Add("onclick", "Open_Formula_Screen_NEW('" & txtFormula.ID & "')")
            btnOvertimeFormula.Attributes.Add("onclick", "Open_Formula_Screen_NEW('" & txtOvertimeFormula.ID & "')")
            btnHolidayFormula.Attributes.Add("onclick", "Open_Formula_Screen_NEW('" & txtHolidayFormula.ID & "')")
            'Added by: Hassan Kurdi
            'Date:2021-10-12
            LateButton.Attributes.Add("onclick", "Open_Formula_Screen_NEW('" & LateFormulaTextBox.ID & "')")
            AbsentButton.Attributes.Add("onclick", "Open_Formula_Screen_NEW('" & AbsentFormulaTextBox.ID & "')")
            VacCostButton.Attributes.Add("onclick", "Open_Formula_Screen_NEW('" & VacCostFormulaTextBox.ID & "')")
            'End
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsEmployeeClasses.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsEmployeeClasses.ID
                If (IntrecordID > 0) Then
                    SetScreenInformation("E")
                Else
                    SetScreenInformation("N")
                End If
            Else
                SetScreenInformation("N")
            End If
            CreateOtherFields(IntrecordID)
            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeeClasses.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsEmployeeClasses = New Clshrs_EmployeeClasses(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeeClasses.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsEmployeeClasses.ID > 0 Then
                    ClsEmployeeClasses.Update("code='" & txtCode.Text & "'")
                    ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
                    ClsEmployeeClasses.SetEmployeeClassVacation(ClsEmployeeClasses.ID, uwgVacationDetails)
                    SaveEmployeesClassesDelay(ClsEmployeeClasses.ID)
                Else
                    ClsEmployeeClasses.Save()
                    ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
                    ClsEmployeeClasses.SetEmployeeClassVacation(ClsEmployeeClasses.ID, uwgVacationDetails)
                    SaveEmployeesClassesDelay(ClsEmployeeClasses.ID)
                End If
                ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsEmployeeClasses.Table, ClsEmployeeClasses.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsEmployeeClasses.ID > 0 Then
                    ClsEmployeeClasses.Update("code='" & txtCode.Text & "'")
                    ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
                    ClsEmployeeClasses.SetEmployeeClassVacation(ClsEmployeeClasses.ID, uwgVacationDetails)
                    SaveEmployeesClassesDelay(ClsEmployeeClasses.ID)
                Else
                    ClsEmployeeClasses.Save()
                    ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
                    ClsEmployeeClasses.SetEmployeeClassVacation(ClsEmployeeClasses.ID, uwgVacationDetails)
                    SaveEmployeesClassesDelay(ClsEmployeeClasses.ID)
                End If
                ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsEmployeeClasses.Table, ClsEmployeeClasses.ID)
                value.Text = ""
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsEmployeeClasses.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsEmployeeClasses.ID & "&TableName=" & ClsEmployeeClasses.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsEmployeeClasses.ID & "&TableName=" & ClsEmployeeClasses.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsEmployeeClasses.Table
                ClsEmployeeClasses.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsEmployeeClasses.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsEmployeeClasses.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsEmployeeClasses.Find(" Code= '" & txtCode.Text & "'")
                If ClsEmployeeClasses.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsEmployeeClasses.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsEmployeeClasses.CheckDiff(ClsEmployeeClasses, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsEmployeeClasses.FirstRecord()
                GetValues()
            Case "Previous"
                ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
                If Not ClsEmployeeClasses.previousRecord() Then
                    ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
                If Not ClsEmployeeClasses.NextRecord() Then
                    ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsEmployeeClasses.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function SaveEmployeesClassesDelay(ByVal clsid As Integer) As Boolean
        Dim ClshrsEmployeesClassesDelay As New Clshrs_EmployeesClassesDelay(Page)
        Try
            Dim str As String = "delete from hrs_EmployeesClassesDelay where ClassID = " & clsid
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClshrsEmployeesClassesDelay.ConnectionString, Data.CommandType.Text, str)
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgDelayingSlice.Rows
                If IsNothing(DGRow.Cells(1).Value) And (Not DGRow.Cells(2).Value > 0) Then
                    Continue For
                End If
                ClshrsEmployeesClassesDelay = New Clshrs_EmployeesClassesDelay(Page)
                ClshrsEmployeesClassesDelay.ClassID = clsid
                ClshrsEmployeesClassesDelay.FromMin = DGRow.Cells(1).Value
                ClshrsEmployeesClassesDelay.ToMin = DGRow.Cells(2).Value
                ClshrsEmployeesClassesDelay.PunishPCT = DGRow.Cells(3).Value
                ClshrsEmployeesClassesDelay.Save()
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub CreateEmptyRows(ByVal intNoOfrows As Integer, ByVal objGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid)
        For i As Integer = 0 To intNoOfrows
            objGrid.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow())
        Next
    End Sub
    Private Sub HideDetailsRows(ByVal intVacationTypeID As Integer)
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgVacationDetails.Rows
            If row.Cells.FromKey("VacationTypeID").Value = intVacationTypeID Then
                row.Hidden = False
            Else
                row.Hidden = True
            End If
        Next
    End Sub
    Private Function AssignValues() As Boolean
        Try
            With ClsEmployeeClasses
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .NoOfDaysPerPeriod = txtNoofdaysperperiod.Text
                .MaxInstallementPCT = txtMaxInstallementPCT.Text
                .MaxLoanAmtPCT = txtMaxLoanAmtPCT.Text
                .MinServiceMonth = txtMinServiceMonth.Text
                .WorkHoursPerDay = txtWorkhoursperday.Text
                .NoOfHoursPerWeek = txtNoofhoursperweek.Text
                .NoOfHoursPerPeriod = txtNoofhoursperperiod.Text
                .OvertimeFactor = txtOvertimefactor.Text
                .HolidayFactor = txtHolidayfactor.Text
                .FirstDayOfWeek = DdlFirstdayofweek.SelectedItem.Value
                .DefultStartTime = Convert.ToDateTime(txtStartTime.Text).ToShortTimeString()
                .DefultEndTime = Convert.ToDateTime(txtEndTime.Text).ToShortTimeString()
                .WorkingUnitsIsHours = IIf(DdlWorkingUnitsIsHours.SelectedItem.Value = "True", True, False)
                .PerDailyDelaying = txtDDelayingfactor.Text
                .PerMonthlyDelaying = txtMDelayingfactor.Text
                .NonProfitOverTimeH = txtNonProfitOverTimeH.Text
                .EOBFormula = txtFormula.Text

                .PunishementCalc = DropDownList2.SelectedValue
                .OnNoExit = DropDownList3.SelectedValue
                .DeductionMethod = DropDownList5.SelectedValue

                .EOSCostingTrns = ddlEOSCosting.SelectedValue
                .VacCostingTrns = ddlVacCosting.SelectedValue
                .TicketsCostingTrns = ddlTicketsCosting.SelectedValue
                .HICostingTrns = ddlHI.SelectedValue

                .PolicyCheckMachine = IIf(DdlPolicyCheckMachine.SelectedItem.Value = "True", True, False)
                .HasAttendance = IIf(DdlHasAttendance.SelectedItem.Value = "True", True, False)

                .OvertimeFormula = txtOvertimeFormula.Text
                .HolidayFormula = txtHolidayFormula.Text
                'Added by: Hassan Kurdi
                'Date:2021-10-12
                .AbsentFormula = AbsentFormulaTextBox.Text
                .LateFormula = LateFormulaTextBox.Text
                .VacCostFormula = VacCostFormulaTextBox.Text
                .HasFingerPrint = IIf(HasFingerPrintCheckBox.Checked = True, True, False)
                .HasflexableFingerPrint = IIf(HasFlexableFingerPrintCheckBox.Checked = True, True, False)

                .HasOvertimeList = IIf(hasOvertimeListCheckBox.Checked = True, True, False)
                .AttendanceFromTimeSheet = IIf(AttendanceFromTimeSheetCheckBox.Checked = True, True, False)

                .AdvanceBalance = chkAdvanceBalance.Checked
                .AccumulatedBalance = chkAccumulatedBalance.Checked
                .VacationTrans = chkVacationTrans.Checked
                .VactionTransType = ddlVactionTransType.SelectedValue
                .TransValue = IIf(txtTransValue.Text <> "", Convert.ToInt16(txtTransValue.Text), 0)
                .AddBalanceInAddEmp = chkAddBalanceInAddEmp.Checked
                'End
                Try
                    .DefaultProjectID = DdlProjects.SelectedValue
                Catch ex As Exception
                    .DefaultProjectID = 0
                End Try
                Try
                    .NonPermiLatTransaction = DropDownList1.SelectedValue
                Catch ex As Exception
                    .NonPermiLatTransaction = 0
                End Try
                Try
                    .RegComputerID = DropDownList4.SelectedValue
                Catch ex As Exception
                    .RegComputerID = 0
                End Try
                Try
                    .OvertimeTransaction = DropDownList6.SelectedValue
                Catch ex As Exception
                    .OvertimeTransaction = 0
                End Try
                Try
                    .HOvertimeTransaction = DropDownList7.SelectedValue
                Catch ex As Exception
                    .HOvertimeTransaction = 0
                End Try
            End With

            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim Clsprojects As New Clshrs_Projects(Page, "hrs_Projects")
        Try
            SetToolBarDefaults()

            With ClsEmployeeClasses

                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                txtNoofdaysperperiod.Text = .NoOfDaysPerPeriod
                txtMaxInstallementPCT.Text = .MaxInstallementPCT
                txtMaxLoanAmtPCT.Text = .MaxLoanAmtPCT
                txtMinServiceMonth.Text = .MinServiceMonth
                txtWorkhoursperday.Text = .WorkHoursPerDay
                txtNoofhoursperweek.Text = .NoOfHoursPerWeek
                txtNoofhoursperperiod.Text = .NoOfHoursPerPeriod
                txtOvertimefactor.Text = .OvertimeFactor
                txtHolidayfactor.Text = .HolidayFactor
                txtDDelayingfactor.Text = .PerDailyDelaying
                txtMDelayingfactor.Text = .PerMonthlyDelaying
                txtNonProfitOverTimeH.Text = .NonProfitOverTimeH
                txtFormula.Text = .EOBFormula

                chkAdvanceBalance.Checked = .AdvanceBalance
                chkAccumulatedBalance.Checked = .AccumulatedBalance
                chkVacationTrans.Checked = .VacationTrans
                ddlVactionTransType.SelectedValue = .VactionTransType
                txtTransValue.Text = .TransValue
                chkAddBalanceInAddEmp.Checked = .AddBalanceInAddEmp

                'Added by: Hassan Kurdi
                'Date:2021-10-12
                AbsentFormulaTextBox.Text = .AbsentFormula
                LateFormulaTextBox.Text = .LateFormula
                VacCostFormulaTextBox.Text = .VacCostFormula
                HasFingerPrintCheckBox.Checked = IIf(.HasFingerPrint = True, True, False)
                HasFlexableFingerPrintCheckBox.Checked = IIf(.HasflexableFingerPrint = True, True, False)
                hasOvertimeListCheckBox.Checked = IIf(.HasOvertimeList = True, True, False)
                AttendanceFromTimeSheetCheckBox.Checked = IIf(.AttendanceFromTimeSheet = True, True, False)
                'End
                DropDownList1.SelectedValue = .NonPermiLatTransaction
                DropDownList4.SelectedValue = .RegComputerID

                DropDownList2.SelectedValue = .PunishementCalc
                DropDownList3.SelectedValue = .OnNoExit
                DropDownList5.SelectedValue = .DeductionMethod
                DropDownList6.SelectedValue = .OvertimeTransaction
                DropDownList7.SelectedValue = .HOvertimeTransaction

                ddlEOSCosting.SelectedValue = .EOSCostingTrns
                ddlVacCosting.SelectedValue = .VacCostingTrns
                ddlTicketsCosting.SelectedValue = .TicketsCostingTrns
                ddlHI.SelectedValue = .HICostingTrns

                DdlPolicyCheckMachine.SelectedValue = .PolicyCheckMachine
                DdlHasAttendance.SelectedValue = .HasAttendance

                txtOvertimeFormula.Text = .OvertimeFormula
                txtHolidayFormula.Text = .HolidayFormula
                Try
                    DdlFirstdayofweek.SelectedValue = .FirstDayOfWeek
                Catch ex As Exception
                    DdlFirstdayofweek.SelectedValue = ""
                End Try
                txtStartTime.Text = Convert.ToDateTime(.DefultStartTime).ToShortTimeString()
                txtEndTime.Text = Convert.ToDateTime(.DefultEndTime).ToShortTimeString()
                DdlWorkingUnitsIsHours.SelectedValue = .WorkingUnitsIsHours

                Dim item As New System.Web.UI.WebControls.ListItem()
                Clsprojects.GetDropDownList(DdlProjects, False)
                item.Value = .DefaultProjectID
                Clsprojects.Find("ID=" & ClsEmployeeClasses.DefaultProjectID)
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(.ConnectionString)

                item.Text = ObjNavigationHandler.SetLanguage(Page, Clsprojects.EngName & "/" & Clsprojects.ArbName)
                If Not DdlProjects.Items.Contains(item) Then
                    If Not item.Value = 0 Then
                        DdlProjects.Items.Add(item)
                        DdlProjects.SelectedValue = item.Value
                    End If
                Else
                    DdlProjects.SelectedValue = .DefaultProjectID
                End If

                uwgVacationTypes.DataSource = ClsEmployeeClasses.GetAllVacationTypes()
                uwgVacationTypes.DataBind()
                uwgVacationDetails.DataSource = .GetEmployeeClassVacations(.ID)
                uwgVacationDetails.DataBind()
                uwgVacationTypes.DisplayLayout.ActiveRow = uwgVacationTypes.Rows(0)
                uwgVacationTypes.Rows(0).Selected = True
                hdnVacationTypeID.Value = uwgVacationTypes.Rows(0).Cells.FromKey("VacationTypeID").Value
                HideDetailsRows(uwgVacationTypes.Rows(0).Cells.FromKey("VacationTypeID").Value)
                Dim ClshrsEmployeesClassesDelay As New Clshrs_EmployeesClassesDelay(Page)
                ClshrsEmployeesClassesDelay.Find("ClassID = " & .ID)
                uwgDelayingSlice.DataSource = ClshrsEmployeesClassesDelay.DataSet.Tables(0)
                uwgDelayingSlice.DataBind()

            End With

            CreateEmptyRows(1, uwgDelayingSlice)
            CreateEmptyRows(1, uwgVacationDetails)
            CreateEmptyRows(1, uwgVacationTypes)

            If Not ClsEmployeeClasses.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsEmployeeClasses.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsEmployeeClasses.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsEmployeeClasses.RegDate).Date
            End If
            If ClsEmployeeClasses.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsEmployeeClasses.CancelDate).Date
            End If
            If Not ClsEmployeeClasses.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If

            If (ClsEmployeeClasses.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsEmployeeClasses.ConnectionString, ClsEmployeeClasses.DataBaseUserRelatedID, ClsEmployeeClasses.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEmployeeClasses.ConnectionString, ClsEmployeeClasses.DataBaseUserRelatedID, ClsEmployeeClasses.GroupID, ClsEmployeeClasses.Table, ClsEmployeeClasses.ID)
            If Not ClsEmployeeClasses.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsEmployeeClasses.ID)
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
                    txtCode.Text = String.Empty
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
                    ClsEmployeeClasses.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsEmployeeClasses.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsEmployeeClasses
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
        ClsEmployeeClasses = New Clshrs_EmployeeClasses(Me)
        Try
            With ClsEmployeeClasses
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
        ClsEmployeeClasses = New Clshrs_EmployeeClasses(Me)
        If IntId > 0 Then
            ClsEmployeeClasses.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEmployeeClasses = New Clshrs_EmployeeClasses(Me)
        Try
            ClsEmployeeClasses.Find("Code='" & txtCode.Text & "'")
            IntId = ClsEmployeeClasses.ID
            txtEngName.Focus()
            If ClsEmployeeClasses.ID > 0 Then
                GetValues()
                StrMode = "E"
            Else
                If ClsEmployeeClasses.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsEmployeeClasses.ConnectionString, ClsEmployeeClasses.DataBaseUserRelatedID, ClsEmployeeClasses.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEmployeeClasses.ConnectionString, ClsEmployeeClasses.DataBaseUserRelatedID, ClsEmployeeClasses.GroupID, ClsEmployeeClasses.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                ImageButton_Delete.Enabled = False
            End If
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
        ClsEmployeeClasses.Clear()
        GetValues()
        ImageButton_Delete.Enabled = False

        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Page)
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        txtHolidayfactor.Text = 0
        txtNoofdaysperperiod.Text = 0
        txtMaxInstallementPCT.Text = 0
        txtMaxLoanAmtPCT.Text = 0
        txtMinServiceMonth.Text = 0
        txtNoofhoursperperiod.Text = 0
        txtNoofhoursperweek.Text = 0
        txtHolidayfactor.Text = ""
        txtOvertimefactor.Text = ""
        txtWorkhoursperday.Text = ""
        txtStartTime.Text = String.Empty
        txtEndTime.Text = String.Empty
        DdlFirstdayofweek.SelectedValue = 1
        DdlProjects.SelectedIndex = 0
        DropDownList1.SelectedIndex = 0
        DropDownList4.SelectedIndex = 0
        txtDDelayingfactor.Text = 0
        txtMDelayingfactor.Text = 0
        txtNonProfitOverTimeH.Text = 0
        txtFormula.Text = ""
        txtOvertimeFormula.Text = ""
        txtHolidayFormula.Text = ""

        chkAdvanceBalance.Checked = False
        chkAccumulatedBalance.Checked = False
        chkVacationTrans.Checked = False
        ddlVactionTransType.SelectedIndex = 0
        txtTransValue.Text = String.Empty
        chkAddBalanceInAddEmp.Checked = False

        'Added by: Hassan Kurdi
        'Date:2021-10-12
        AbsentFormulaTextBox.Text = ""
        LateFormulaTextBox.Text = ""
        VacCostFormulaTextBox.Text = ""
        HasFingerPrintCheckBox.Checked = False
        HasFlexableFingerPrintCheckBox.Checked = False
        hasOvertimeListCheckBox.Checked = False
        AttendanceFromTimeSheetCheckBox.Checked = False
        'End
        DdlHasAttendance.SelectedIndex = 0
        DdlPolicyCheckMachine.SelectedIndex = 0
        ddlEOSCosting.SelectedValue = 0
        ddlVacCosting.SelectedValue = 0
        ddlTicketsCosting.SelectedValue = 0
        ddlHI.SelectedValue = 0

        uwgVacationDetails.DataSource = Nothing
        uwgVacationDetails.DataBind()

        uwgDelayingSlice.DataSource = Nothing
        uwgDelayingSlice.DataBind()

        uwgVacationTypes.DataSource = ClsEmployeeClasses.GetAllVacationTypes()
        uwgVacationTypes.DataBind()
        uwgVacationTypes.DisplayLayout.ActiveRow = uwgVacationTypes.Rows(0)
        uwgVacationTypes.Rows(0).Selected = True
        hdnVacationTypeID.Value = uwgVacationTypes.Rows(0).Cells.FromKey("VacationTypeID").Value

        DropDownList2.SelectedIndex = 0
        DropDownList3.SelectedIndex = 0
        DropDownList5.SelectedIndex = 0
        DropDownList6.SelectedIndex = 0
        DropDownList7.SelectedIndex = 0

        CreateEmptyRows(1, uwgDelayingSlice)
        CreateEmptyRows(1, uwgVacationDetails)
        CreateEmptyRows(1, uwgVacationTypes)

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsEmployeeClasses = New Clshrs_EmployeeClasses(Page)
        ClsEmployeeClasses.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsEmployeeClasses.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEmployeeClasses.Table) = True Then
            Dim StrTablename As String
            ClsEmployeeClasses = New Clshrs_EmployeeClasses(Me)
            StrTablename = ClsEmployeeClasses.Table
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

#End Region

End Class
