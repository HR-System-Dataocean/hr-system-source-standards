Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeesVacations
    Inherits MainPage
#Region "Public Decleration"
    Private ClsEmployeesVacations As Clshrs_EmployeesVacations
    Private ClsEmployees As Clshrs_Employees
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Const csOtherFields = 11
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim recordID As Integer
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Try

            ClsEmployeesVacations.AddNotificationOnChange(Page)
            Dim csSearchID As Integer
            Dim ClsLevels As New Clshrs_LevelTypes(Page)
            Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
            Dim ClsDataHandler As New Venus.Shared.DataHandler
            Dim StrSerial As String = String.Empty
            ClsEmployees = New Clshrs_Employees(Page)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
            Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)
            Dim ClsObjects As New Clssys_Objects(Page)
            Dim ClsSearchs As New Clssys_Searchs(Page)
            Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
            txtEmployee.Attributes.Add("onchange", "ChangeIsDataChanged()")
            ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language = ""javascript"">IntializeDataChanged()</script>")
            ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim() & "'")
            ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
            csSearchID = ClsSearchs.ID


            lblLage.Text = ObjNavigationHandler.SetLanguage(Page, "0/1")
            Page.Session.Add("Lage", lblLage.Text)
            Dim IntDimension As Integer = 510
            Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmployee.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtEmployee.ClientID & "'"
            btnEmployee.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

            Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)

            If Not IsPostBack Then

                Session.Add("EmpVacID", 0)
                Dim str As String = lbVactionID.Text
                WebDateChooser1.Value = ClsEmployeesVacations.GetHigriDate(Date.Now)
                txtEmployee.Focus()
                ClsVacationTypes.GetList(uwgEmployeeVacations.Bands(0).Columns(0).ValueList)
                ClsVacationTypes.GetList(uwgEmployeeVacations.Bands(1).Columns(0).ValueList)
                ClsVacationTypes.GetDropDownList(DdlVacationType, False, "IsAnnual=1")
                If DdlVacationType.Items.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "You must enter at least one vacation type/يجب أن تدخل على الاقل نوع أجازة واحد"))
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
                ClsVacationTypes.Find("IsAnnual=1")
                If ClsVacationTypes.ID > 0 Then
                    hdnAnnualVacId.Value = ClsVacationTypes.ID
                Else
                    hdnAnnualVacId.Value = 0
                End If
            End If
            If (lbVactionID.Text <> "") Then
                ClsEmployeesVacations.Find("ID=" & lbVactionID.Text)
                recordID = ClsEmployeesVacations.ID
                EmpVacationId.Value = recordID

                If (recordID > 0) Then
                    SetScreenInformation("E")
                    SetToolBarRecordPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, ClsEmployeesVacations.Table, recordID)
                Else
                    SetScreenInformation("N")
                    If Not IsPostBack Then
                        SetTime()
                    End If
                End If
            Else
                SetScreenInformation("N")
                If Not IsPostBack Then
                    SetTime()
                End If
            End If
            If ClsObjects.Find(" Code='" & ClsEmployeesVacations.Table.ToString.Trim() & "'") Then
                ImageButton_Documents.Attributes.Add("onclick", "OpenModal3('frmAttachDocuments.aspx?OId=" & ClsObjects.ID & "&',400,600,true,''); return false;")
            End If

            If Not IsPostBack Then
                Dim DteStartDate As Date = Date.Now
                Dim DteEndDate As Date = Date.Now
                If Request.QueryString.Count > 0 Then
                    If Request.QueryString.Item("EmpCode") <> Nothing Then
                        txtEmployee.Text = Request.QueryString.Item("EmpCode")
                        txtEmployee_TextChanged(Nothing, Nothing)
                    End If
                    If Request.QueryString.Item("StartDate") <> Nothing Then
                        DteStartDate = Request.QueryString.Item("StartDate")
                    End If
                    If Request.QueryString.Item("ToDate") <> Nothing Then
                        DteEndDate = Request.QueryString.Item("ToDate")
                    End If
                End If
                WebDateChooser1.Value = ClsEmployeesVacations.GetHigriDate(DteStartDate)
                WebDateChooser2.Value = ClsEmployeesVacations.GetHigriDate(DteEndDate)
                GetEmpContractVac()
                WebDateChooser2.Value = Nothing
            End If
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Delete.Command
        Dim IntId As Integer
        Dim strMode As String
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Page)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesVacations.ConnectionString)
        Select Case e.CommandArgument

            Case "Payments"
                If CheckEmployee() Then
                    ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "OpenModal1('frmEmployeesVacationTransactions.aspx?Fisical=" & WebDateChooser1.Value & "&ID=" & ClsEmployees.ID & "',450,844,false,''); return false;", True)
                End If

            Case "SaveNew"
                SavePart()

            Case "Save"
                If txtEmployee.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If SavePart() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                End If
            Case "New"
                SetNew()
            Case "Delete"
                If CheckEmployee() And CheckVacation() Then
                    ClsEmployeesVacations.Find("ID=" & lbVactionID.Text)
                    EmpVacationId.Value = ClsEmployeesVacations.ID

                    If ClsEmployeesVacations.RegComputerID > 0 Then
                        Dim Cls_EmployeeVacationOpenBalance As New Clshrs_EmployeeVacationOpenBalance(Me.Page)
                        Cls_EmployeeVacationOpenBalance.Find("ID = " & ClsEmployeesVacations.RegComputerID)
                        Cls_EmployeeVacationOpenBalance.RegComputerID = 0
                        Cls_EmployeeVacationOpenBalance.Update("ID = " & ClsEmployeesVacations.RegComputerID)
                    End If
                    ClsEmployeesTransactions.Find("EmployeesVacationsID=" & ClsEmployeesVacations.ID)

                    If (ClsEmployeesTransactions.DataSet.Tables(0).Rows.Count < 1) Then
                        ClsEmployeesVacations.Delete("ID=" & ClsEmployeesVacations.OverDueVacation)
                        ClsEmployeesVacations.Delete("ID=" & lbVactionID.Text)
                        Dim cls_OTHER_VACATION = New Clshrs_EmployeesVacations(Page)
                        If cls_OTHER_VACATION.Find("ParentVacationID = " & lbVactionID.Text) Then
                            cls_OTHER_VACATION.Delete("ParentVacationID = " & lbVactionID.Text)
                        End If
                    Else
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Cannot delete this vacation because it has a transaction  /لا يمكن حذف الاجازة لان لها مستحقات "))
                    End If
                End If
                CheckEmpCode()
                SetNew()
            Case "Property"
                If CheckEmployee() And CheckVacation() Then
                    If ClsEmployeesVacations.Find("ID=" & lbVactionID.Text) Then
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsEmployeesVacations.ID & "&TableName=" & ClsEmployeesVacations.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                    End If
                End If
            Case "Remarks"
                If CheckEmployee() And CheckVacation() Then
                    If ClsEmployeesVacations.Find("ID=" & lbVactionID.Text) Then
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsEmployeesVacations.ID & "&TableName=" & ClsEmployeesVacations.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                    End If
                End If
            Case "Print"
                If Session("EmpVacID") = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Pleases Select Vacation/الرجاء إختيار الإجازة"))
                Else
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=EmpVacID&preview=1&ReportCode=CF_LeaveRequisition&sq0=''&v=" & Session("EmpVacID"), 700, 490, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "wWindO", False, True, False, False, False, False, False, False, False)
                End If


            Case "Other Fields"

            Case "Exit"

            Case "First"
                ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                ClsEmployees.FirstRecord()
                txtEmployee.Text = ClsEmployees.Code
                txtEmployee_TextChanged(Nothing, Nothing)
            Case "Previous"
                ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                If Not ClsEmployees.previousRecord() Then
                    ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                txtEmployee.Text = ClsEmployees.Code
                txtEmployee_TextChanged(Nothing, Nothing)
            Case "Next"
                ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                If Not ClsEmployees.NextRecord() Then
                    ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                txtEmployee.Text = ClsEmployees.Code
                txtEmployee_TextChanged(Nothing, Nothing)
            Case "Last"
                ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                ClsEmployees.LastRecord()
                txtEmployee.Text = ClsEmployees.Code
                txtEmployee_TextChanged(Nothing, Nothing)
        End Select
        If uwgEmployeeVacations.Rows.Count > 0 Then
            IntId = uwgEmployeeVacations.Rows(0).Cells.FromKey("ID").Value
            strMode = "E"
        Else
            strMode = "N"
            IntId = 0
        End If
        SetToolBarPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, strMode)
        SetToolBarRecordPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, ClsEmployeesVacations.Table, IntId)
        If strMode = "N" Then
            ImageButton_Delete.Enabled = False
        End If
    End Sub

    Protected Sub txtEmployee_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmployee.TextChanged, ImageButton_Refresh.Click
        Try
            Session("EmpVacID") = 0
            CheckEmpCode()
            Dim ClsEmployeesVacations As New Clshrs_EmployeesVacations(Page)
            ClsEmployees = New Clshrs_Employees(Page)
            Dim clsContract As New Clshrs_Contracts(Page)
            Dim clsEmployeesTransactions = New Clshrs_EmployeesTransactions(Page)
            Dim clsEmployeeClass As New Clshrs_EmployeeClasses(Page)
            Dim intEmpCalssStartHour As Integer = 9
            Dim intEmpCalssStartMinutes As Integer = 0
            Dim wHoursPerDay As Double = 9
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")

            Dim dteWebDateChooser1 As Date = WebDateChooser1.Value
            With ClsEmployees
                dteWebDateChooser1 = ClsEmployees.SetHigriDate(dteWebDateChooser1)
            End With


            Dim intContractID As Integer = clsContract.ContractValidatoinId(ClsEmployees.ID, dteWebDateChooser1)
            clsContract.Find(" ID =" & intContractID)
            clsEmployeeClass.Find("ID=" & IIf(clsContract.EmployeeClassID > 0, clsContract.EmployeeClassID, 0))
            hdnAnnualVacId.Value = DdlVacationType.SelectedValue
            If (lbVactionID.Text.Trim = "" Or lbVactionID.Text.Trim = "0") Then
                If clsEmployeeClass.ID > 0 Then
                    If IsNothing(clsEmployeeClass.WorkHoursPerDay) Then
                        wHoursPerDay = 9
                    Else
                        wHoursPerDay = clsEmployeeClass.WorkHoursPerDay
                    End If
                    hdnWorkingHoursPerDay.Value = wHoursPerDay
                    If Not IsNothing(clsEmployeeClass.DefultStartTime) Then
                        intEmpCalssStartHour = CDate(clsEmployeeClass.DefultStartTime).Hour
                        intEmpCalssStartMinutes = CDate(clsEmployeeClass.DefultStartTime).Minute
                    End If
                    Dim chkIn As New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, intEmpCalssStartHour, intEmpCalssStartMinutes, 0)
                    Dim chkOut As DateTime = chkIn.AddHours(wHoursPerDay)
                End If

                If hdnAnnualVacId.Value <> 0 And DdlVacationType.SelectedValue = hdnAnnualVacId.Value And ClsEmployees.ID > 0 Then
                    GetEmpContractVac()
                End If
            End If
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub uwgEmployeeVacations_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgEmployeeVacations.InitializeRow
        Dim clsCompanies As New Clssys_Companies(Page)
        With clsCompanies
            e.Row.Cells.FromKey("actualstartdate").Value = .GetHigriDate(e.Row.Cells.FromKey("actualstartdate").Value) & IIf(Not IsNothing(e.Row.Cells.FromKey("actualstartdate").Value), " " & e.Row.Cells.FromKey("actualstartdate").Value.ToString().Split(" ")(1) & " " & e.Row.Cells.FromKey("actualstartdate").Value.ToString().Split(" ")(2), "")
            If Not IsNothing(e.Row.Cells.FromKey("actualenddate").Value) Then
                e.Row.Cells.FromKey("actualenddate").Value = .GetHigriDate(e.Row.Cells.FromKey("actualenddate").Value) & IIf(Not IsNothing(e.Row.Cells.FromKey("actualenddate").Value), " " & e.Row.Cells.FromKey("actualenddate").Value.ToString().Split(" ")(1) & " " & e.Row.Cells.FromKey("actualenddate").Value.ToString().Split(" ")(2), "")
            End If
        End With
    End Sub
    Protected Sub WebDateChooser1_ValueChanged(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs) Handles WebDateChooser1.ValueChanged

        GetEmpContractVac()
    End Sub
    Protected Sub WebDateChooser2_ValueChanged(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs) Handles WebDateChooser2.ValueChanged
        GetEmpContractVac()
    End Sub
    Protected Sub uwgEmployeeVacations_SelectedRowsChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs) Handles uwgEmployeeVacations.SelectedRowsChange

        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeeVacations.Rows
            row.Style.BackColor = System.Drawing.Color.Transparent
        Next
        If e.SelectedRows.Count > 0 Then
            e.SelectedRows.Item(0).Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#92c2fb")
        End If

        Try
            If txtEmployee.Text <> "" Then
                LinkButton_OverDueMessage.Visible = False
                DdlVacationType.SelectedValue = e.SelectedRows.Item(0).Cells.FromKey("VacationTypeID").Value
                WebDateChooser1.Value = e.SelectedRows.Item(0).Cells.FromKey("ActualStartDate").Value
                WebDateChooser2.Value = e.SelectedRows.Item(0).Cells.FromKey("ActualEndDate").Value


                If e.SelectedRows.Item(0).Cells.FromKey("ActualEndDate").Value = Nothing Then
                    WebDateChooser1.Enabled = False
                    WebDateChooser3.Enabled = False
                    txtVactiondays.Enabled = False
                    WebDateChooser2.Enabled = True
                    ImageButton_Save.Enabled = True

                Else
                    WebDateChooser1.Enabled = False
                    txtVactiondays.Enabled = False
                    WebDateChooser2.Enabled = False
                    ImageButton_Save.Enabled = False

                End If
                lbVactionID.Text = e.SelectedRows.Item(0).Cells.FromKey("ID").Value
                Dim clsEmp As New Clshrs_Employees(Page)
                Dim clsEmpVac As New Clshrs_EmployeesVacations(Page)
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmpVac.ConnectionString)
                Dim dteFiscalYear As Date = CDate(WebDateChooser1.Value)
                dteFiscalYear = ClsDataAcessLayer.HijriToGreg(dteFiscalYear, "dd/MM/yyyy")
                If clsEmp.Find("Code='" & txtEmployee.Text & "'") Then
                    If clsEmpVac.Find("ID=" & lbVactionID.Text) Then
                        Dim cls_OTHER_VACATION As New Clshrs_EmployeesVacations(Page)
                        If cls_OTHER_VACATION.Find("ParentVacationID=" & clsEmpVac.ID) Then
                            WebDateChooser2.Value = cls_OTHER_VACATION.ActualEndDate
                        End If
                        'lblTotalDays.Text = Math.Round(Convert.ToDecimal(clsEmpVac.TotalDays), 2)
                        lblTotalDays.Text = Math.Round(Convert.ToDecimal(clsEmpVac.RemainingDays), 2) + Math.Round(Convert.ToDecimal(clsEmpVac.vactiondays), 2)
                        lbTotalVal.Text = Math.Round(Convert.ToDecimal(clsEmpVac.TotalDays), 2)
                        lbRemainVal.Text = Math.Round(Convert.ToDecimal(clsEmpVac.RemainingDays), 2)
                        txtVactiondays.Text = Math.Round(Convert.ToDecimal(clsEmpVac.vactiondays), 2)
                        chk_ZeroingBalance.Checked = clsEmpVac.ZeroingBalance
                        Dim vl As DateTime = WebDateChooser1.Value
                        WebDateChooser3.Value = vl.Date.AddDays(IIf(txtVactiondays.Text < 1, 0, txtVactiondays.Text))
                        If clsEmpVac.OverDueVacation <> Nothing Then
                            clsEmpVac.Find("ID=" & clsEmpVac.OverDueVacation)
                            Dim days As Integer = (DateDiff(DateInterval.Day, clsEmpVac.ActualStartDate, clsEmpVac.ActualEndDate)) + 1
                            LinkButton_OverDueMessage.Visible = True
                            LinkButton_OverDueMessage.Text = ObjNavigationHandler.SetLanguage(Page, "There Is (" & days & ") Days OverDue Vacations/هناك عدد (" & days & ") يوم تجاوز لرصيد الأجازة")
                        End If

                        Dim ClsUser As New Clssys_Users(Page)

                        If Not clsEmpVac.RegUserID = Nothing Then
                            ClsUser.Find("ID=" & clsEmpVac.RegUserID)
                        End If
                        If ClsUser.EngName = Nothing Then
                            lblRegUserValue.Text = ""
                        Else
                            lblRegUserValue.Text = ClsUser.EngName
                        End If
                        If Convert.ToDateTime(clsEmpVac.RegDate).Date = Nothing Then
                            lblRegDateValue.Text = ""
                        Else
                            lblRegDateValue.Text = Convert.ToDateTime(clsEmpVac.RegDate).Date
                        End If
                    Else
                        lbTotalVal.Text = 0 : lbRemainVal.Text = 0 : txtVactiondays.Text = 0 : lblRegDateValue.Text = "" : lblRegUserValue.Text = ""
                        lblTotalDays.Text = lbTotalVal.Text
                        LinkButton_OverDueMessage.Visible = False
                    End If
                End If
                ImageButton_Delete.Enabled = True
                ImageButton_Print.Enabled = True
                GetRecordInfoAjax(e.SelectedRows.Item(0).Cells.FromKey("ID").Value)

                Session("EmpVacID") = e.SelectedRows.Item(0).Cells.FromKey("ID").Value
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub txtVactiondays_ValueChange(sender As Object, e As System.EventArgs) Handles txtVactiondays.TextChanged
        If txtVactiondays.Text <> "" Then
            lbRemainVal.Text = Math.Round(lblTotalDays.Text - txtVactiondays.Text, 2)
            'lbRemainVal.Text = Math.Round(lbRemainVal.Text - txtVactiondays.Text, 2)
            Dim vl As DateTime = WebDateChooser1.Value
            WebDateChooser3.Value = vl.Date.AddDays(IIf(txtVactiondays.Text < 1, 0, txtVactiondays.Text))
            WebDateChooser2.Value = Nothing

            If (Convert.ToSingle(lbRemainVal.Text) < 0) Then
                lbRemainVal.CssClass = "worningRemain"
            Else
                lbRemainVal.CssClass = "noramlRemain"
            End If
        End If
    End Sub
#End Region

#Region "Private Functions"
    Private Function SetNew() As Boolean
        Dim ClsEmployeesVacations As New Clshrs_EmployeesVacations(Page)
        Try
            Session("EmpVacID") = 0
            LinkButton_OverDueMessage.Visible = False
            lbVactionID.Text = ""
            DdlVacationType.SelectedIndex = 0
            DdlVacationType.Enabled = True

            WebDateChooser1.Value = ClsEmployeesVacations.GetHigriDate(Date.Now)
            WebDateChooser3.Value = ClsEmployeesVacations.GetHigriDate(Date.Now)
            WebDateChooser2.Value = Nothing
            txtVactiondays.Enabled = True
            WebDateChooser1.Enabled = True
            WebDateChooser2.Enabled = True
            WebDateChooser3.Enabled = True
            SetTime()
            lbTotalVal.Text = 0
            lblTotalDays.Text = 0
            txtVactiondays.Text = 0
            lbRemainVal.Text = 0
            lbRemainVal.CssClass = "noramlRemain"

            If CheckEmployee() Then
                FillEmployeeVacations(ClsEmployees.ID)
            End If

            If (lbVactionID.Text.Trim = "" Or lbVactionID.Text.Trim = "0") Then
                ClsEmployees = New Clshrs_Employees(Page)
                Dim clsContract As New Clshrs_Contracts(Page)
                Dim clsEmployeesTransactions = New Clshrs_EmployeesTransactions(Page)
                Dim clsEmployeeClass As New Clshrs_EmployeeClasses(Page)
                Dim dateExStartDate As Date
                Dim dateExEndDate As Date
                Dim intEmpCalssStartHour As Integer = 9
                Dim intEmpCalssStartMinutes As Integer = 0
                Dim wHoursPerDay As Double = 9
                ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                Dim intContractID As Integer = clsContract.ContractValidatoinId(ClsEmployees.ID, Date.Now)
                If intContractID = 0 Then
                    Return False
                End If


                clsContract.Find(" ID =" & intContractID)
                clsEmployeeClass.Find("ID=" & IIf(clsContract.EmployeeClassID > 0, clsContract.EmployeeClassID, 0))
                If clsEmployeeClass.ID > 0 Then
                    If IsNothing(clsEmployeeClass.WorkHoursPerDay) Then
                        wHoursPerDay = 9
                    Else
                        wHoursPerDay = clsEmployeeClass.WorkHoursPerDay
                    End If
                    hdnWorkingHoursPerDay.Value = wHoursPerDay
                    If Not IsNothing(clsEmployeeClass.DefultStartTime) Then
                        intEmpCalssStartHour = CDate(clsEmployeeClass.DefultStartTime).Hour
                        intEmpCalssStartMinutes = CDate(clsEmployeeClass.DefultStartTime).Minute
                    End If
                    Dim chkIn As New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, intEmpCalssStartHour, intEmpCalssStartMinutes, 0)
                    Dim chkOut As DateTime = chkIn.AddHours(wHoursPerDay)
                End If
                '================ set Time [E]
                'If hdnAnnualVacId.Value <> 0 And DdlVacationType.SelectedValue = hdnAnnualVacId.Value And ClsEmployees.ID > 0 Then
                '    Dim DteVacationDate As Date
                '    Try
                '        Dim resetDate As Date? = Nothing
                '        Dim Cls_EmployeeVacationOpenBalance As New Clshrs_EmployeeVacationOpenBalance(Me.Page)
                '        Cls_EmployeeVacationOpenBalance.Find("EmployeeID=" & ClsEmployees.ID & " and VacationTypeID = " & DdlVacationType.SelectedValue)
                '        If Cls_EmployeeVacationOpenBalance.GBalanceDate <> Nothing Then
                '            resetDate = Cls_EmployeeVacationOpenBalance.GBalanceDate
                '        End If
                '        DteVacationDate = ClsEmployeesVacations.GetDefaultVacation(ClsEmployees.ID, intContractID, resetDate, False)
                '    Catch ex As Exception
                '        DteVacationDate = Date.Now
                '    End Try
                '    dateExStartDate = DteVacationDate
                '    If (hdnDurationDays.Value = "") Then hdnDurationDays.Value = 0
                '    dateExEndDate = dateExStartDate.AddDays(IIf(hdnDurationDays.Value = 0, 1, hdnDurationDays.Value))
                '    With ClsEmployeesVacations
                '        WebDateChooser1.Value = .GetHigriDate(dateExStartDate)
                '    End With
                '    GetEmpContractVac()
                'End If
            End If
            lblRegUserValue.Text = ""
            lblRegDateValue.Text = ""

            GetEmpContractVac()

            SetToolBarPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, "N")
            ImageButton_Delete.Enabled = False
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Sub AddOnChangeEventToControls(ByVal formName As String)
        Try
            Dim clsForms As New ClsSys_Forms(Page)
            clsForms.Find(" code = REPLACE('" & formName & "',' ','')")
            Dim clsFormsControls As New Clssys_FormsControls(Page)
            If clsForms.ID > 0 Then
                clsFormsControls.Find(" FormID=" & clsForms.ID)
                Dim tab As Data.DataTable = clsFormsControls.DataSet.Tables(0).Copy()
                For Each row As Data.DataRow In tab.Rows
                    Dim currCtrl As Control = Me.FindControl(row("Name"))
                    If TypeOf (currCtrl) Is TextBox Then
                    End If
                Next
            End If
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Private Function CheckDateBetween2Dates(ByVal d As Date, ByVal d1 As Date, ByVal d2 As Date) As Boolean
        If (d1 = Nothing Or d2 = Nothing) Then
            Return False
        End If
        If (Date.Compare(d, d1) >= 0 And Date.Compare(d, d2) <= 0) Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CheckDateBetween2DatesNew(ByVal d As Date, ByVal d1 As Date, ByVal d2 As Date) As Boolean
        If (d1 = Nothing Or d2 = Nothing) Then
            Return False
        End If
        If (Date.Compare(d, d1) >= 0 And Date.Compare(d, d2) < 0) Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CheckDateBetween2DatesNew2(ByVal d As Date, ByVal d1 As Date, ByVal d2 As Date) As Boolean
        If (d1 = Nothing Or d2 = Nothing) Then
            Return False
        End If
        If (Date.Compare(d, d1) > 0 And Date.Compare(d, d2) < 0) Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CheckEmployee() As Boolean
        Dim BolExist As Boolean
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        If txtEmployee.Text <> "" Then
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            If (ClsEmployees.ID > 0) Then
                BolExist = True
            Else
                BolExist = False
            End If
        Else
            BolExist = False
        End If
        If (Not BolExist) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Employee not found/هذا الموظف غير موجود"))
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CheckVacation() As Boolean
        ' Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        'If lbVactionID.Text.Trim = "" Then
        'Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "You must select vacation/يجب أن تختار أجازة"))
        'Return False
        'Else
        Return True
        'End If
    End Function
    Private Function SetTime() As Boolean
        Dim clsCompanies As New Clssys_Companies(Page)
        Dim dteNow As Date = Format(Date.Now, "dd/MM/yyyy")
        Try
            With clsCompanies
            End With
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function GetEmpContractVac() As Boolean
        Dim ClsVacations As New Clshrs_VacationsTypes(Page)
        Dim ClsContracts As New Clshrs_Contracts(Page)
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim DteVacationStartDate As DateTime
        Dim totalDays As Single = 0
        Dim unpaidDays As Single = 0
        Dim NetDays As Single = 0
        ClsVacations.Find(" IsAnnual=1 ")
        ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
        DteVacationStartDate = ClsEmployees.SetHigriDate(WebDateChooser1.Value)
        ClsContracts.ContractValidatoinId(ClsEmployees.ID, DteVacationStartDate)
        Dim dblVacationDays As Double = 0
        If ClsContracts.ID > 0 Then
            Dim isconract As Boolean = False

            ClsContracts.CheckAnnualVacationDaysNew(ClsContracts.ID, ClsVacations.ID, False, dblVacationDays, totalDays, unpaidDays, NetDays, DteVacationStartDate, isconract)

            Dim OpenBalanceAmount As Double = 0
            Dim VacTypeID As Integer = 1
            Dim Diffe As Single = 0

            Try
                If WebDateChooser2.Value <> Nothing Then
                    Diffe = (DateDiff(DateInterval.Day, WebDateChooser1.Value, WebDateChooser2.Value))
                End If
            Catch ex As Exception
                Diffe = 0
            End Try
            'Rabie  --CalculationMistak
            'dblVacationDays = ClsContracts.GET_EMPLOYEE_VACATION_BALANCE_TO_DATE(ClsEmployees.Code, DteVacationStartDate)
            If dblVacationDays < 0 Then dblVacationDays = 0

            If lbVactionID.Text = "" Then
                lblTotalDays.Text = Math.Round(dblVacationDays, 2)
                lbTotalVal.Text = Math.Round(dblVacationDays, 0)
                txtVactiondays.Text = Math.Round(Diffe, 0)
                lbRemainVal.Text = Math.Round(dblVacationDays - Diffe, 2)
                'lbRemainVal.Text = dblVacationDays - Diffe
            End If

            If (Convert.ToSingle(lbRemainVal.Text) < 0) Then
                lbRemainVal.CssClass = "worningRemain"
            Else
                lbRemainVal.CssClass = "noramlRemain"
            End If
            Dim vl As DateTime = WebDateChooser1.Value
            WebDateChooser3.Value = vl.Date.AddDays(IIf(txtVactiondays.Text < 1, 0, txtVactiondays.Text))
            Return True
        Else
            lblTotalDays.Text = 0
            lbTotalVal.Text = 0
            txtVactiondays.Text = 0
            lbRemainVal.Text = 0
            Return False
        End If
    End Function
    Private Function GetDateDiffAccordingWH(ByVal dteActualStartDate As Date, ByVal dteactualEndDate As Date, ByVal WorkingHoursPerDay As Double, ByVal dteStartTime As Date, ByVal dteEndTime As Date) As Double
        Dim NoOfWorkingHours As Double = WorkingHoursPerDay
        Dim NoOfNonWorkingHours As Double = 24 - NoOfWorkingHours
        Dim vacDays As Double = 0
        Dim dteThisStartDateStartTime As Date
        Dim dteThisStartDateEndTime As Date
        Dim dteThisEndDateStartTime As Date
        Dim dteThisEndDateEndTime As Date
        If dteactualEndDate < dteActualStartDate Then
            Return 0
        End If
        Dim dteTempActualStartDate As Date = New Date(dteActualStartDate.Year, dteActualStartDate.Month, dteActualStartDate.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
        Dim dteTempActualEndDate As Date = New Date(dteactualEndDate.Year, dteactualEndDate.Month, dteactualEndDate.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
        dteThisStartDateStartTime = New Date(dteActualStartDate.Year, dteActualStartDate.Month, dteActualStartDate.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
        dteThisStartDateEndTime = New Date(dteActualStartDate.Year, dteActualStartDate.Month, dteActualStartDate.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
        dteThisEndDateStartTime = New Date(dteactualEndDate.Year, dteactualEndDate.Month, dteactualEndDate.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
        dteThisEndDateEndTime = New Date(dteactualEndDate.Year, dteactualEndDate.Month, dteactualEndDate.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
        dteTempActualStartDate = New Date(dteActualStartDate.Year, dteActualStartDate.Month, dteActualStartDate.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
        dteTempActualEndDate = New Date(dteactualEndDate.Year, dteactualEndDate.Month, dteactualEndDate.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
        If dteActualStartDate.Day = dteactualEndDate.Day And
           dteActualStartDate.Month = dteactualEndDate.Month And
           dteActualStartDate.Year = dteactualEndDate.Year And
           ((dteActualStartDate > dteTempActualEndDate And dteactualEndDate > dteTempActualEndDate) Or
             (dteActualStartDate < dteTempActualStartDate And dteactualEndDate < dteTempActualStartDate)) Then
            Return 0
        End If
        If DateDiff(DateInterval.Day, dteActualStartDate, dteactualEndDate) = 0 And
            (dteActualStartDate >= dteThisStartDateEndTime And dteactualEndDate <= dteThisEndDateStartTime) Then
            Return 0
        End If
        If dteActualStartDate < dteTempActualStartDate Then
            dteActualStartDate = dteTempActualStartDate
        ElseIf dteActualStartDate > dteTempActualStartDate And dteActualStartDate > dteThisStartDateEndTime Then
            Dim dteActualStartDatePlusDay As Date = dteActualStartDate.AddDays(1)
            dteActualStartDate = New Date(dteActualStartDatePlusDay.Year, dteActualStartDatePlusDay.Month, dteActualStartDatePlusDay.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
        End If
        If dteactualEndDate > dteTempActualEndDate Then
            dteactualEndDate = dteTempActualEndDate
        ElseIf dteactualEndDate < dteTempActualEndDate And dteactualEndDate < dteThisEndDateStartTime Then
            Dim dteActualEndDateMiunsDay As Date = dteactualEndDate.AddDays(-1)
            dteactualEndDate = New Date(dteActualEndDateMiunsDay.Year, dteActualEndDateMiunsDay.Month, dteActualEndDateMiunsDay.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
        End If
        Dim diffH As Double = DateDiff(DateInterval.Minute, dteActualStartDate, dteactualEndDate) / 60
        Dim diffD As Double = DateDiff(DateInterval.Day, dteActualStartDate, dteactualEndDate)
        vacDays += (diffH - (diffD * NoOfNonWorkingHours)) / NoOfWorkingHours
        Return vacDays
    End Function
    Function GetNDate(ByVal date1 As Object) As Date
        If IsDBNull(date1) Then
            Return Date.Now
        Else
            Return CDate(date1)
        End If
    End Function
    Function GetNDate(ByVal date1 As Object, ByVal dteDefault As Date) As Date
        If IsDBNull(date1) Then
            Return dteDefault
        Else
            Return CDate(date1)
        End If
    End Function
    Public Shared Function GetNDate_Shared(ByVal date1 As Object, ByVal dteDefault As Date) As Date
        If IsDBNull(date1) Then
            Return dteDefault
        Else
            Return CDate(date1)
        End If
    End Function
    Private Sub FillEmployeeVacations(ByVal EmployeeID As Integer, Optional ByVal showFirstRecord As Boolean = False)
        Try
            Dim DS1 As New Data.DataSet()
            DS1.Clear()
            For x As Integer = 0 To DS1.Tables.Count - 1
                DS1.Tables(x).Constraints.Clear()
            Next
            DS1.Relations.Clear()
            DS1.Tables.Clear()
            Dim connetionString As String
            Dim connection As Data.SqlClient.SqlConnection
            Dim command As Data.SqlClient.SqlCommand
            Dim adapter As New Data.SqlClient.SqlDataAdapter
            connetionString = ClsEmployees.ConnectionString
            connection = New Data.SqlClient.SqlConnection(connetionString)

            Dim str1 As String = "select A.ID AS ID,A.EmployeeID,A.VacationTypeID,A.ExpectedStartDate,A.ExpectedEndDate,A.EmployeeRequestRemarks" &
                                 ",A.IsContracial,A.ActualStartDate,ISNULL((select Due.ActualEndDate from hrs_EmployeesVacations AS Due where Due.ID = A.OverDueVacation and due.CancelDate is null),A.ActualEndDate) as ActualEndDate,a.TotalDays as totaldays,a.ConsumDays ConsumDays,vactiondays as vactiondays,OverdueDays as OverdueDays,RemainingDays as RemainingDays,PaymentTrnID as PaymentTrnID,A.ZeroingBalance from hrs_EmployeesVacations AS A where A.canceldate is null and A.EmployeeID=" & EmployeeID & " and A.VacationTypeID = " & DdlVacationType.SelectedValue

            Dim str2 As String = "select A.ID,A.EmployeeID,A.VacationTypeID,A.ActualStartDate,A.ActualEndDate,isnull((select B.ID from hrs_EmployeesVacations as B where B.OverDueVacation = A.ID ),A.ID) AS RelCol,A.ZeroingBalance from  hrs_EmployeesVacations AS A where A.EmployeeID=" & EmployeeID

            command = New Data.SqlClient.SqlCommand(str1, connection)
            adapter.SelectCommand = command
            adapter.Fill(DS1, "Table1")
            command = New Data.SqlClient.SqlCommand(str2, connection)
            adapter.SelectCommand = command
            adapter.Fill(DS1, "Table2")
            adapter.Dispose()
            command.Dispose()
            connection.Close()
            Dim DataCol1 As Data.DataColumn
            Dim DataCol2 As Data.DataColumn
            DataCol1 = DS1.Tables(0).Columns(0)
            DataCol2 = DS1.Tables(1).Columns(5)
            Dim Rel1 As Data.DataRelation = New Data.DataRelation("Rel1", DataCol1, DataCol2, False)
            DS1.Relations.Add(Rel1)

            uwgEmployeeVacations.DataSource = Nothing
            uwgEmployeeVacations.DataBind()

            uwgEmployeeVacations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
            uwgEmployeeVacations.DataSource = DS1
            uwgEmployeeVacations.DataBind()

            If lbVactionID.Text.Trim = "" Then
                txtVactiondays.Enabled = True
                WebDateChooser1.Enabled = True
                WebDateChooser2.Enabled = True
            End If
            '======================================== Show First Record [ Start ]
            If showFirstRecord Then
                If uwgEmployeeVacations.Rows.Count > 0 Then
                    Dim row As Infragistics.WebUI.UltraWebGrid.UltraGridRow = uwgEmployeeVacations.Rows(0)
                    DdlVacationType.SelectedValue = row.Cells(0).Value
                    DdlVacationType.Enabled = False

                    WebDateChooser1.Value = row.Cells(3).Value
                    WebDateChooser2.Value = row.Cells(4).Value
                    If Not IsNothing(row.Cells(4).Value) Then
                        WebDateChooser1.Enabled = False
                        WebDateChooser2.Enabled = False
                        txtVactiondays.Enabled = False
                    Else
                        WebDateChooser1.Enabled = True
                        WebDateChooser2.Enabled = True
                        txtVactiondays.Enabled = True
                    End If
                    lbVactionID.Text = row.Cells(9).Value
                    row.Selected = True
                    row.Activated = True
                    Dim ClsUser As New Clssys_Users(Page)
                    ClsEmployeesVacations.Find("ID=" & row.Cells(9).Value)
                    '=============================== Record Info [Start]
                    If Not ClsEmployeesVacations.RegUserID = Nothing Then
                        If (ClsUser.Find("ID=" & ClsEmployeesVacations.RegUserID)) Then
                            lblRegUserValue.Text = ClsUser.EngName
                        Else
                            lblRegUserValue.Text = ""
                        End If
                    End If
                    If Convert.ToDateTime(ClsEmployeesVacations.RegDate).Date = Nothing Then
                        lblRegDateValue.Text = ""
                    Else
                        lblRegDateValue.Text = CDate(ClsEmployeesVacations.RegDate).ToShortDateString()
                    End If

                    '=============================== Record Info [ End ]
                    SetToolBarRecordPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, ClsEmployeesVacations.Table, row.Cells(9).Value)
                Else
                    ' [0261]
                    DdlVacationType.Enabled = True
                End If
            End If
            '======================================== Show First Record [ End ]
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Private Function CheckEmpCode() As Boolean
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsNationality = New Clssys_Nationality(Page)
        Dim BolExist As Boolean
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Try
            SetTime()
            lbVactionID.Text = ""
            DdlVacationType.SelectedIndex = 0
            WebDateChooser1.Value = ClsEmployees.GetHigriDate(Date.Now)
            WebDateChooser2.Value = Nothing
            WebDateChooser1.Enabled = True
            WebDateChooser2.Enabled = True
            txtVactiondays.Enabled = True
            lbTotalVal.Text = 0
            lblTotalDays.Text = 0
            txtVactiondays.Text = 0
            lbRemainVal.Text = 0
            LinkButton_OverDueMessage.Visible = False
            lbRemainVal.CssClass = "noramlRemain"
            SetScreenInformation("N")
            If (txtEmployee.Text.Trim <> "") Then
                ClsEmployees.Find("Code ='" & txtEmployee.Text & "'")
                If ClsEmployees.ID > 0 Then
                    txtEmployee.Text = ClsEmployees.Code
                    lblDescEnglishName.Text = ClsEmployees.EnglishName
                    If Not IsNothing(ClsEmployees.NationalityID) Then
                        ClsNationality.Find("Id=" & ClsEmployees.NationalityID)
                        lblDescNationality.Text = ClsNationality.EngName
                    End If
                    FillEmployeeVacations(ClsEmployees.ID, False)

                    Dim ClsEmployeeRelated = New Clshrs_Employees(Page)
                    If (ClsEmployees.IsProjectRelated = True) Then
                        If ClsEmployeeRelated.Find("Code = '" & ClsEmployees.Code & "cop'") Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Kindly Review The Cop Employee and Do The Same Procedure To Him /برجاء مراجعة الموظف الرتبط وعمل نفس الاجراء له COP"))
                        End If
                    End If
                    BolExist = True
                Else
                    BolExist = False
                End If
            Else
                BolExist = False
            End If
            If (Not BolExist) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Employee not found/هذا الموظف غير موجود"))
                lblDescEnglishName.Text = ""
                lblDescNationality.Text = ""
                lbTotalVal.Text = 0
                lblTotalDays.Text = 0
                txtVactiondays.Text = 0
                LinkButton_OverDueMessage.Visible = False
                lbRemainVal.Text = 0
                txtEmployee.Text = ""
                lbVactionID.Text = ""
                uwgEmployeeVacations.Clear()
                txtEmployee.Focus()
            End If
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Private Function SavePart() As Boolean


        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Dim ClsEmployeesVacations2 = New Clshrs_EmployeesVacations(Page)

        Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesVacations.ConnectionString)
        If txtVactiondays.Text = 0 And lbVactionID.Text = "" Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please set vaction days  /برجاء إدخال عدد ايام الاجازة"))
            Exit Function
        End If

        Dim cls_Company = New Clssys_Companies(Page)
        Dim cls_Employee = New Clshrs_Employees(Page)
        'Dim dec_PaidVacation As Decimal = 0
        'Dim dec_UnPaidVacation As Decimal = 0
        cls_Employee.Find("Code = " & txtEmployee.Text)
        cls_Company.Find("ID = " & cls_Employee.CompanyID)
        If CDec(txtVactiondays.Text) > CDec(lbTotalVal.Text) Then
            If Not cls_Company.AllowOverVacation Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Vaction days more the current balance / أيام الإجازة أكثر من الرصيد المستحق"))
                Exit Function
                'Else
                '    dec_UnPaidVacation = CDec(txtVactiondays.Text) - CDec(lbTotalVal.Text)
                '    dec_PaidVacation = CDec(lbTotalVal.Text)

                '    Dim dat_VacationStartDate As DateTime = WebDateChooser1.Value
                '    WebDateChooser3.Value = dat_VacationStartDate.AddDays(dec_PaidVacation)
                '    WebDateChooser2.Value = WebDateChooser3.Value
                '    txtVactiondays.Text = dec_PaidVacation.ToString()
            End If
        End If

        ClsVacationTypes.Find("ID = " & DdlVacationType.SelectedValue)
        clsMainOtherFields = New clsSys_MainOtherFields(Page)
        Dim recordId As Integer
        Dim RemVal As Integer = 0
        Try


            If Not String.IsNullOrEmpty(WebDateChooser2.Value) Then
                If WebDateChooser1.Value >= WebDateChooser2.Value Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Vaction return date must be greater than start date /تاريخ الرجوع من الاجازة يجب أن يكون اكبر من تاريخ البداية "))
                    Exit Function
                End If
            End If

            If lbVactionID.Text.Trim <> "" Then
                If CheckEmployee() Then
                    ClsEmployeesVacations.Find("ID=" & lbVactionID.Text)
                    If Not AssignValue(ClsEmployeesVacations) Then
                        Exit Function
                    End If

                    If ClsEmployeesVacations2.GetEmployeeLastVacation(ClsEmployeesVacations.EmployeeID) Then
                        If IsDBNull(ClsEmployeesVacations2.ActualEndDate) Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a vacation without return date /يوجد اجازة سابقة من غير تاريخ رجوع  "))
                            Exit Function
                        End If
                    End If

                    Dim Cls_EmployeeVacationOpenBalance As New Clshrs_EmployeeVacationOpenBalance(Me.Page)
                    If Cls_EmployeeVacationOpenBalance.Find("RegComputerID <> 1 and EmployeeID=" & ClsEmployeesVacations.EmployeeID & " and VacationTypeID = " & DdlVacationType.SelectedValue) Then
                        ClsEmployeesVacations.RegComputerID = Cls_EmployeeVacationOpenBalance.ID
                        Cls_EmployeeVacationOpenBalance.RegComputerID = 1
                        Cls_EmployeeVacationOpenBalance.Update("EmployeeID=" & ClsEmployeesVacations.EmployeeID & " and VacationTypeID = " & DdlVacationType.SelectedValue)
                    End If
                    If WebDateChooser2.Value <> Nothing Then
                        'Dim dteWebDateChooser2 As Date = WebDateChooser2.Value
                        Dim Cls_Contracts As New Clshrs_Contracts(Page)
                        Dim dec_ALLOWED_ANNUAL = Cls_Contracts.GET_EMPLOYEE_VACATION_BALANCE_TO_DATE(cls_Employee.Code, CDate(WebDateChooser1.Value))
                        Dim dat_ACTUALL_RETRUN As Date = CDate(WebDateChooser2.Value)
                        ' WebDateChooser2.Value = CDate(WebDateChooser1.Value).AddDays(dec_ALLOWED_ANNUAL)
                        Dim dat_NEW_RETURN As Date = CDate(WebDateChooser2.Value)
                        If dat_ACTUALL_RETRUN > CDate(WebDateChooser2.Value) Then
                            Dim dat_VacationStartDate As DateTime = WebDateChooser2.Value
                            WebDateChooser1.Value = dat_VacationStartDate
                            WebDateChooser2.Value = dat_ACTUALL_RETRUN
                            WebDateChooser3.Value = dat_ACTUALL_RETRUN
                            lbVactionID.Text = ""
                            txtVactiondays.Text = ""
                            lbTotalVal.Text = ""
                            Dim ClsEmployeesUnpaidVacations = New Clshrs_EmployeesVacations(Page)
                            'AssignValueUnpaid(ClsEmployeesUnpaidVacations)
                            'ClsEmployeesUnpaidVacations.ParentVacationID = ClsEmployeesVacations.ID
                            'ClsEmployeesUnpaidVacations.SaveVacation()
                        Else
                            WebDateChooser2.Value = dat_ACTUALL_RETRUN
                            dat_NEW_RETURN = WebDateChooser2.Value
                        End If

                        '===================== Overdue =======================
                        setOverDueDays(ClsVacationTypes, dat_NEW_RETURN)
                        '=====================================================
                        ClsEmployeesVacations.TotalBalance = lblTotalDays.Text
                        'ClsEmployeesVacations.RemainingDays = lblTotalDays.Text - txtVactiondays.Text
                        If chk_ZeroingBalance.Checked Then
                            ClsEmployeesVacations.RemainingDays = 0
                        Else
                            ClsEmployeesVacations.RemainingDays = Convert.ToDouble(ClsEmployeesVacations.TotalDays) - Convert.ToDouble(ClsEmployeesVacations.ConsumDays)
                        End If

                        ClsEmployeesVacations.ZeroingBalance = chk_ZeroingBalance.Checked

                        recordId = ClsEmployeesVacations.ID
                        ClsEmployeesVacations.ActualEndDate = CDate(CDate(dat_NEW_RETURN).ToShortDateString() & " " & "00:00")

                        ClsEmployeesVacations.Update("ID=" & ClsEmployeesVacations.ID)

                        If ClsEmployees.IsProjectRelated Then
                            Dim hrsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
                            If hrsProjectPlacementEmployees.Find("EmployeeID = " & ClsEmployeesVacations.EmployeeID & " and (ToDate is null or ToDate >= convert(Datetime," & ClsEmployeesVacations.ActualStartDate & "))") Then
                                Dim DTPlacement As DataTable = hrsProjectPlacementEmployees.DataSet.Tables(0)
                                For Each DrPlacement As DataRow In DTPlacement.Rows
                                    hrsProjectPlacementEmployees = New Clshrs_ProjectPlacementEmployees(Me)
                                    hrsProjectPlacementEmployees.Find("ID = " & DrPlacement("ID"))
                                    hrsProjectPlacementEmployees.ToDate = ClsEmployeesVacations.ActualStartDate.AddDays(-1)
                                    hrsProjectPlacementEmployees.Update("ID = " & DrPlacement("ID"))
                                Next
                            End If
                            Dim strDelcommand As String = "set dateformat dmy; delete from Att_AttendTransactions where EmployeeID = " & ClsEmployeesVacations.EmployeeID & " and TrnsDatetime > '" & ClsEmployeesVacations.ActualStartDate.AddDays(-1).ToString("dd/MM/yyyy") & "'"
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectPlacementEmployees.ConnectionString, Data.CommandType.Text, strDelcommand)
                        End If

                        recordId = ClsEmployeesVacations.ID
                        If RemVal <> 0 Then
                            'Dim ClsEmployeesVacations2 As New Clshrs_EmployeesVacations(Page)
                            'ClsEmployeesVacations2.EmployeeID = ClsEmployeesVacations.EmployeeID
                            'ClsEmployeesVacations2.VacationTypeID = ClsVacationTypes.OverDueVacationID
                            'ClsEmployeesVacations2.ExpectedStartDate = ClsEmployeesVacations.SetHigriDate(ClsEmployeesVacations.ActualEndDate.AddDays(1))
                            'ClsEmployeesVacations2.ExpectedEndDate = ClsEmployeesVacations.SetHigriDate(ClsEmployeesVacations.ActualEndDate.AddDays(RemVal))
                            'ClsEmployeesVacations2.ActualStartDate = ClsEmployeesVacations.SetHigriDate(ClsEmployeesVacations.ActualEndDate.AddDays(1))
                            'ClsEmployeesVacations2.ActualEndDate = ClsEmployeesVacations.SetHigriDate(ClsEmployeesVacations.ActualEndDate.AddDays(RemVal))
                            'ClsEmployeesVacations2.Remarks = "OverDue Days From Vacation No " & recordId & " ==== " & "إيام تجاوز لاجازة رقم " & recordId
                            'ClsEmployeesVacations2.RegUserID = ClsEmployeesVacations.RegUserID
                            'ClsEmployeesVacations2.RegDate = ClsEmployeesVacations.RegDate
                            'ClsEmployeesVacations2.TotalDays = ClsEmployeesVacations2.ActualEndDate.Subtract(ClsEmployeesVacations2.ActualStartDate).Days + 1
                            'Dim OVDID As Integer = ClsEmployeesVacations2.SaveVacation()
                            'ClsEmployeesVacations.OverDueVacation = OVDID
                            'ClsEmployeesVacations.Update("ID=" & recordId)
                        End If
                    Else
                        recordId = ClsEmployeesVacations.ID
                        ClsEmployeesVacations.Update("ID=" & ClsEmployeesVacations.ID)

                        If ClsEmployees.IsProjectRelated Then
                            Dim hrsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
                            If hrsProjectPlacementEmployees.Find("EmployeeID = " & ClsEmployeesVacations.EmployeeID & " and (ToDate is null or ToDate >= '" & ClsEmployeesVacations.ActualStartDate.ToString("dd/MM/yyyy") & "')") Then
                                Dim DTPlacement As DataTable = hrsProjectPlacementEmployees.DataSet.Tables(0)
                                For Each DrPlacement As DataRow In DTPlacement.Rows
                                    hrsProjectPlacementEmployees = New Clshrs_ProjectPlacementEmployees(Me)
                                    hrsProjectPlacementEmployees.Find("ID = " & DrPlacement("ID"))
                                    hrsProjectPlacementEmployees.ToDate = ClsEmployeesVacations.ActualStartDate.AddDays(-1)
                                    hrsProjectPlacementEmployees.Update("ID = " & DrPlacement("ID"))
                                Next
                            End If
                            Dim strDelcommand As String = "set dateformat dmy; delete from Att_AttendTransactions where EmployeeID = " & ClsEmployeesVacations.EmployeeID & " and TrnsDatetime > '" & ClsEmployeesVacations.ActualStartDate.AddDays(-1).ToString("dd/MM/yyyy") & "'"
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectPlacementEmployees.ConnectionString, Data.CommandType.Text, strDelcommand)
                        End If
                        recordId = ClsEmployeesVacations.ID
                    End If
                Else
                    Exit Function
                End If
            Else
                If CheckEmployee() Then
                    If Not AssignValue(ClsEmployeesVacations) Then
                        Exit Function
                    End If

                    If ClsEmployeesVacations2.GetEmployeeLastVacation(ClsEmployeesVacations.EmployeeID) Then
                        If ClsEmployeesVacations2.ActualEndDate = Date.MinValue Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a vacation without return date /يوجد اجازة سابقة من غير تاريخ رجوع  "))
                            Exit Function
                        End If
                        If ClsEmployeesVacations2.ActualEndDate > WebDateChooser1.Text Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a vacation with end date greater than the inserted date /يوجد اجازة سابقة بتاريخ رجوع أكبر من التاريخ المدخل  "))
                            Exit Function
                        End If
                    End If

                    Dim Cls_EmployeeVacationOpenBalance As New Clshrs_EmployeeVacationOpenBalance(Me.Page)
                    If Cls_EmployeeVacationOpenBalance.Find("RegComputerID <> 1 and EmployeeID=" & ClsEmployeesVacations.EmployeeID & " and VacationTypeID = " & DdlVacationType.SelectedValue) Then
                        ClsEmployeesVacations.RegComputerID = Cls_EmployeeVacationOpenBalance.ID
                        Cls_EmployeeVacationOpenBalance.RegComputerID = 1
                        Cls_EmployeeVacationOpenBalance.Update("EmployeeID=" & ClsEmployeesVacations.EmployeeID & " and VacationTypeID = " & DdlVacationType.SelectedValue)
                    End If
                    If WebDateChooser2.Value <> Nothing Then
                        Dim Cls_Contracts As New Clshrs_Contracts(Page)
                        Dim dec_ALLOWED_ANNUAL = Cls_Contracts.GET_EMPLOYEE_VACATION_BALANCE_TO_DATE(cls_Employee.Code, CDate(WebDateChooser1.Value))
                        Dim dat_ACTUALL_RETRUN As Date = CDate(WebDateChooser2.Value)
                        'Dim dat_MAX_RETURN = CDate(WebDateChooser1.Value).AddDays(dec_ALLOWED_ANNUAL)
                        'WebDateChooser2.Value = CDate(WebDateChooser1.Value).AddDays(dec_ALLOWED_ANNUAL)
                        'If dat_ACTUALL_RETRUN > dat_MAX_RETURN Then
                        'ClsEmployeesVacations.ActualEndDate = dat_MAX_RETURN
                        'WebDateChooser2.Value = dat_MAX_RETURN
                        'End If
                        '===================== Overdue =======================
                        setOverDueDays(ClsVacationTypes, dat_ACTUALL_RETRUN)
                        '=====================================================
                        recordId = ClsEmployeesVacations.SaveVacation()
                        If dat_ACTUALL_RETRUN <> CDate(WebDateChooser2.Value) Then
                            Dim dat_VacationStartDate As DateTime = WebDateChooser2.Value
                            WebDateChooser1.Value = dat_VacationStartDate
                            WebDateChooser2.Value = dat_ACTUALL_RETRUN
                            WebDateChooser3.Value = dat_ACTUALL_RETRUN
                            lbVactionID.Text = ""
                            txtVactiondays.Text = ""
                            lbTotalVal.Text = ""
                            Dim ClsEmployeesUnpaidVacations = New Clshrs_EmployeesVacations(Page)
                            'AssignValueUnpaid(ClsEmployeesUnpaidVacations)
                            'ClsEmployeesUnpaidVacations.ParentVacationID = recordId
                            'ClsEmployeesUnpaidVacations.SaveVacation()
                        End If

                        If ClsEmployees.IsProjectRelated Then
                            Dim hrsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
                            If hrsProjectPlacementEmployees.Find("EmployeeID = " & ClsEmployeesVacations.EmployeeID & " and (ToDate is null or ToDate >= '" & ClsEmployeesVacations.ActualStartDate.ToString("dd/MM/yyyy") & "')") Then
                                Dim DTPlacement As DataTable = hrsProjectPlacementEmployees.DataSet.Tables(0)
                                For Each DrPlacement As DataRow In DTPlacement.Rows
                                    hrsProjectPlacementEmployees = New Clshrs_ProjectPlacementEmployees(Me)
                                    hrsProjectPlacementEmployees.Find("ID = " & DrPlacement("ID"))
                                    hrsProjectPlacementEmployees.ToDate = ClsEmployeesVacations.ActualStartDate.AddDays(-1)
                                    hrsProjectPlacementEmployees.Update("ID = " & DrPlacement("ID"))
                                Next
                            End If
                            Dim strDelcommand As String = "set dateformat dmy; delete from Att_AttendTransactions where EmployeeID = " & ClsEmployeesVacations.EmployeeID & " and TrnsDatetime > '" & ClsEmployeesVacations.ActualStartDate.AddDays(-1).ToString("dd/MM/yyyy") & "'"
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectPlacementEmployees.ConnectionString, Data.CommandType.Text, strDelcommand)
                        End If
                    Else
                        recordId = ClsEmployeesVacations.SaveVacation()
                        If ClsEmployees.IsProjectRelated Then
                            Dim hrsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
                            If hrsProjectPlacementEmployees.Find("EmployeeID = " & ClsEmployeesVacations.EmployeeID & " and (ToDate is null or ToDate >= '" & ClsEmployeesVacations.ActualStartDate.ToString("dd/MM/yyyy") & "')") Then
                                Dim DTPlacement As DataTable = hrsProjectPlacementEmployees.DataSet.Tables(0)
                                For Each DrPlacement As DataRow In DTPlacement.Rows
                                    hrsProjectPlacementEmployees = New Clshrs_ProjectPlacementEmployees(Me)
                                    hrsProjectPlacementEmployees.Find("ID = " & DrPlacement("ID"))
                                    hrsProjectPlacementEmployees.ToDate = ClsEmployeesVacations.ActualStartDate.AddDays(-1)
                                    hrsProjectPlacementEmployees.Update("ID = " & DrPlacement("ID"))
                                Next
                            End If
                            Dim strDelcommand As String = "set dateformat dmy; delete from Att_AttendTransactions where EmployeeID = " & ClsEmployeesVacations.EmployeeID & " and TrnsDatetime > '" & ClsEmployeesVacations.ActualStartDate.AddDays(-1).ToString("dd/MM/yyyy") & "'"
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectPlacementEmployees.ConnectionString, Data.CommandType.Text, strDelcommand)
                        End If
                    End If

                    'If dec_UnPaidVacation > 0 Then
                    '    Dim dat_VacationStartDate As DateTime = WebDateChooser2.Value
                    '    WebDateChooser1.Value = dat_VacationStartDate.AddDays(1)
                    '    WebDateChooser2.Value = Nothing
                    '    WebDateChooser3.Value = Nothing
                    '    lbVactionID.Text = ""
                    '    txtVactiondays.Text = ""
                    '    lbTotalVal.Text = ""
                    '    Dim ClsEmployeesUnpaidVacations = New Clshrs_EmployeesVacations(Page)
                    '    AssignValueUnpaid(ClsEmployeesUnpaidVacations)
                    '    ClsEmployeesUnpaidVacations.SaveVacation()
                    'End If
                Else
                    Exit Function
                End If
            End If

            clsMainOtherFields.CollectDataAndSave(value.Text, ClsEmployeesVacations.Table, recordId)
            value.Text = ""
            CheckEmpCode()
            SetNew()
            Return True
        Catch ex As Exception
            Return False
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Private Sub setOverDueDays(ClsVacationTypes As Clshrs_VacationsTypes, dat_NEW_RETURN As Date)

        ClsEmployeesVacations.ConsumDays = dat_NEW_RETURN.Subtract(ClsEmployeesVacations.ActualStartDate).Days
        If ClsVacationTypes.ExceededDaysType = 0 Then

            If Convert.ToInt32(ClsEmployeesVacations.ConsumDays) > Convert.ToInt32(Math.Round(CDec(ClsEmployeesVacations.TotalDays))) Then
                ClsEmployeesVacations.OverdueDays = Convert.ToInt32(ClsEmployeesVacations.ConsumDays) - Convert.ToInt32(ClsEmployeesVacations.TotalDays)
            Else
                ClsEmployeesVacations.OverdueDays = 0
            End If
        ElseIf ClsVacationTypes.ExceededDaysType = 1 Then

            If Convert.ToInt32(ClsEmployeesVacations.ConsumDays) > Convert.ToInt32(ClsEmployeesVacations.vactiondays) Then
                ClsEmployeesVacations.OverdueDays = Convert.ToInt32(ClsEmployeesVacations.ConsumDays) - Convert.ToInt32(ClsEmployeesVacations.vactiondays)
            End If
        End If
    End Sub

    Private Function AssignValue(ByRef ClsEmployeesVacations As Clshrs_EmployeesVacations) As Boolean
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
        Dim ClsEmployeeVacation As New Clshrs_EmployeesVacations(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeeVacation.ConnectionString)
        Dim strErrorMsg As String = String.Empty
        Dim bExceed As Boolean = False
        Dim bErorr As Boolean = False
        Dim intContractID As Integer = 0
        Dim clsContract As New Clshrs_Contracts(Page)
        Dim clsTempEmployeeVac As New Clshrs_EmployeesVacations(Page)
        Dim clsEmployeeClass As New Clshrs_EmployeeClasses(Page)
        Dim wHoursPerDay As Double = 0
        Dim dteStartTime As Date
        Dim dteEndTime As Date
        Dim dteWebDateChooser1 As Date = WebDateChooser1.Value
        Dim dteWebDateChooser2 As Date = WebDateChooser2.Value
        With ClsEmployees
            dteWebDateChooser1 = .SetHigriDate(dteWebDateChooser1)
            dteWebDateChooser2 = .SetHigriDate(dteWebDateChooser2)
        End With
        Try
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            intContractID = clsContract.ContractValidatoinId(ClsEmployees.ID, dteWebDateChooser1)
            clsContract.Find(" ID =" & intContractID)
            clsEmployeeClass.Find("ID=" & IIf(clsContract.EmployeeClassID > 0, clsContract.EmployeeClassID, 0))
            If clsEmployeeClass.ID > 0 Then
                If IsNothing(clsEmployeeClass.WorkHoursPerDay) Then
                    wHoursPerDay = 9
                    dteStartTime = New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, 8, 0, 0)
                    dteEndTime = dteStartTime.AddHours(wHoursPerDay)
                Else
                    wHoursPerDay = clsEmployeeClass.WorkHoursPerDay
                    dteStartTime = clsEmployeeClass.DefultStartTime
                    dteEndTime = dteStartTime.AddHours(wHoursPerDay)
                End If
            End If
            Dim startDate As Date
            Dim endDate As Date
            If clsContract.ID > 0 Then
                startDate = clsContract.StartDate
                endDate = IIf(IsNothing(clsContract.EndDate), Date.Now, clsContract.EndDate)
            End If
            Dim vacStartDate As Date
            Dim vacEndDate As Date
            Dim prevvacStartDate As Date
            Dim prevvacEndDate As Date
            Dim vacDaysDiff As Single = 0
            Dim prevvacDaysDiff As Single = 0
            '================== Check on contract [Start]
            vacStartDate = CDate(CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00")
            vacEndDate = CDate(CDate(dteWebDateChooser2).ToShortDateString() & " " & "00:00")
            If (Not IsNothing(clsContract.EndDate)) And (Not IsNothing(vacEndDate)) Then
                If vacEndDate > clsContract.EndDate Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Vacation end date not in contract period/نهاية الأجازة ليست فى فترة العقد"))
                    Return False
                End If
            End If
            '================== Check on contract [End]
            If lbVactionID.Text.Trim <> "" Then
                clsTempEmployeeVac.Find(" hrs_EmployeesVacations.ID=" & lbVactionID.Text)
                prevvacStartDate = clsTempEmployeeVac.ActualStartDate
                prevvacEndDate = clsTempEmployeeVac.ActualEndDate
                If Not IsNothing(prevvacStartDate) And prevvacStartDate >= startDate And prevvacStartDate <= endDate Then
                    If CDate(prevvacEndDate).Year = 1 Then
                        prevvacEndDate = Date.Now
                    End If
                    If prevvacEndDate > endDate Then
                        prevvacEndDate = endDate
                    End If
                    prevvacDaysDiff = GetDateDiffAccordingWH(prevvacStartDate, prevvacEndDate, wHoursPerDay, dteStartTime, dteEndTime)
                End If
                If Not IsNothing(vacStartDate) And vacStartDate >= startDate And vacStartDate <= endDate Then
                    If CDate(vacEndDate).Year = 1 Then
                        vacEndDate = Date.Now
                    End If
                    If vacEndDate > endDate Then
                        vacEndDate = endDate
                    End If
                    vacDaysDiff = GetDateDiffAccordingWH(vacStartDate, vacEndDate, wHoursPerDay, dteStartTime, dteEndTime)
                End If
                Dim remainVal As Single = lbRemainVal.Text
                remainVal = remainVal + prevvacDaysDiff
                remainVal = remainVal - vacDaysDiff
                If Convert.ToSingle(lbRemainVal.Text) < 0 Then
                    strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This Vacation exceed allowed vacation \n / \n هذه الاجازة تجاوزت الاجازة المسموحة")
                    bExceed = True
                End If
            Else
                If Not IsNothing(vacStartDate) And vacStartDate >= startDate And vacStartDate <= endDate Then
                    If CDate(vacEndDate).Year = 1 Then
                        vacEndDate = Date.Now
                    End If
                    If vacEndDate > endDate Then
                        vacEndDate = endDate
                    End If
                    vacDaysDiff = GetDateDiffAccordingWH(vacStartDate, vacEndDate, wHoursPerDay, dteStartTime, dteEndTime) 'Math.Abs((DateDiff(DateInterval.Hour, vacStartDate, vacEndDate) / 24))
                End If
                If Convert.ToSingle(lbRemainVal.Text) < 0 Then
                    strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This Vacation exceed allowed vacation \n / \n هذه الاجازة تجاوزت الاجازة المسموحة")
                    bExceed = True
                End If
            End If
            '-------------------------===============------------------------------------------
            Dim SDate As Date
            Dim EDate As Date
            Dim ASDate As Date
            Dim AEDate As Date
            ASDate = CDate(CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00")
            AEDate = CDate(CDate(dteWebDateChooser2).ToShortDateString() & " " & "00:00")
            If AEDate.Year = 1 Then
                AEDate = Date.Now
            End If
            If AEDate < ASDate Then
                AEDate = ASDate
            End If
            If (ASDate.Year = 1 And AEDate <> Nothing) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Start must have value")
                Return False
            End If
            If (AEDate.Year <> 1 And Date.Compare(AEDate, ASDate) < 0) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Return date must greater than start date")
                Return False
            End If
            If (AEDate.Year <> 1 And Date.Compare(AEDate, ASDate) < 0) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Expected Return date must greater than Expected start date")
                Return False
            End If
            Try
                If (ClsEmployeesVacations.FindEmployeeVacations("hrs_EmployeesVacations.EmployeeID=" & ClsEmployees.ID & IIf(lbVactionID.Text.Trim <> "", " AND hrs_EmployeesVacations.ID <>" & lbVactionID.Text, ""))) Then
                    Dim tab As DataTable = ClsEmployeesVacations.DataSet.Tables(0).Copy()
                    For Each row As DataRow In tab.Rows
                        SDate = row("ActualStartDate")
                        EDate = IIf(IsDBNull(row("ActualEndDate")), Date.Now, row("ActualEndDate"))
                        If (EDate < SDate) Then
                            EDate = SDate
                        End If
                        If (CheckDateBetween2DatesNew(ASDate, SDate, EDate)) Then
                            strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This Employee is already in vacation \n /  الموظف موجود فى أجازة بالفعل \n ")
                            bErorr = True
                            Exit For
                        End If
                        If (CheckDateBetween2Dates(AEDate, SDate, EDate)) Then
                            strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This Employee is already in vacation \n /  الموظف موجود فى أجازة بالفعل \n ")
                            bErorr = True
                            Exit For
                        End If
                        If (CheckDateBetween2Dates(SDate, ASDate, AEDate)) Then
                            strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This Employee is already in vacation \n /  الموظف موجود فى أجازة بالفعل \n ")
                            bErorr = True
                            Exit For
                        End If
                        If (CheckDateBetween2DatesNew2(EDate, ASDate, AEDate)) Then
                            strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This Employee is already in vacation \n /  الموظف موجود فى أجازة بالفعل \n ")
                            bErorr = True
                            Exit For
                        End If
                    Next
                End If
            Catch ex As Exception
                Page.Session.Add("ErrorValue", ex)
                Page.Response.Redirect("ErrorPage.aspx")
            End Try
            Try
                With ClsEmployeesVacations
                    If ClsEmployees.Find("Code='" & txtEmployee.Text & "'") Then
                        .EmployeeID = ClsEmployees.ID
                        Select Case ClsEmployees.Sex
                            Case "M"
                                ClsVacationTypes.Find(" ID=" & DdlVacationType.SelectedItem.Value)
                                If Not ClsVacationTypes.Sex = "M" And Not ClsVacationTypes.Sex.ToString.Trim = "" Then
                                    strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This kind of Vacation is not suitable for this employee \n / هذا النوع من الأجازة غير ملائم لهذا الموظف \n")
                                    bErorr = True
                                End If
                            Case "F"
                                ClsVacationTypes.Find(" ID=" & DdlVacationType.SelectedItem.Value)
                                If Not ClsVacationTypes.Sex = "F" And Not ClsVacationTypes.Sex.ToString.Trim = "" Then
                                    strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This kind of Vacation is not suitable for this employee \n / هذا النوع من الأجازة غير ملائم لهذا الموظف \n")
                                    bErorr = True
                                End If
                        End Select

                    End If
                    .VacationTypeID = DdlVacationType.SelectedItem.Value
                    .ActualStartDate = CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00"
                    .ActualEndDate = CDate(dteWebDateChooser2).ToShortDateString() & " " & "00:00"
                    .ExpectedStartDate = CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00"
                    .ExpectedEndDate = .ActualStartDate.AddDays(IIf(txtVactiondays.Text < 1, 0, txtVactiondays.Text))

                    If .ActualEndDate.Year = 1 Then
                        .ActualEndDate = Nothing
                    End If
                    .TotalDays = lbTotalVal.Text
                    '.RemainingDays = lbRemainVal.Text
                    .RemainingDays = lblTotalDays.Text - txtVactiondays.Text
                    If lbRemainVal.Text < 0 Then
                        .RemainingDays = 0
                    End If

                    .vactiondays = txtVactiondays.Text
                    .TotalBalance = lblTotalDays.Text
                    .ConsumDays = txtVactiondays.Text
                    'If lbRemainVal.Text < 0 Then
                    '    .OverdueDays = Math.Abs(Convert.ToInt32(lbRemainVal.Text))
                    'End If
                    'If lbVactionID.Text.Trim = "" And WebDateChooser2.Value <> Nothing Then
                    '    .ConsumDays = txtVactiondays.Text
                    'End If
                End With
                If bExceed And (Not bErorr) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, strErrorMsg)
                ElseIf bErorr Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, strErrorMsg)
                    Return False
                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function AssignValueUnpaid(ByRef ClsEmployeesVacations As Clshrs_EmployeesVacations) As Boolean
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
        Dim ClsEmployeeVacation As New Clshrs_EmployeesVacations(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeeVacation.ConnectionString)
        Dim strErrorMsg As String = String.Empty
        Dim bExceed As Boolean = False
        Dim bErorr As Boolean = False
        Dim intContractID As Integer = 0
        Dim clsContract As New Clshrs_Contracts(Page)
        Dim clsTempEmployeeVac As New Clshrs_EmployeesVacations(Page)
        Dim clsEmployeeClass As New Clshrs_EmployeeClasses(Page)
        Dim wHoursPerDay As Double = 0
        Dim dteStartTime As Date
        Dim dteEndTime As Date
        Dim dteWebDateChooser1 As Date = WebDateChooser1.Value
        Dim dteWebDateChooser2 As Date = WebDateChooser2.Value
        With ClsEmployees
            dteWebDateChooser1 = .SetHigriDate(dteWebDateChooser1)
            dteWebDateChooser2 = .SetHigriDate(dteWebDateChooser2)
        End With
        Try
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            intContractID = clsContract.ContractValidatoinId(ClsEmployees.ID, dteWebDateChooser1)
            clsContract.Find(" ID =" & intContractID)
            clsEmployeeClass.Find("ID=" & IIf(clsContract.EmployeeClassID > 0, clsContract.EmployeeClassID, 0))
            If clsEmployeeClass.ID > 0 Then
                If IsNothing(clsEmployeeClass.WorkHoursPerDay) Then
                    wHoursPerDay = 9
                    dteStartTime = New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, 8, 0, 0)
                    dteEndTime = dteStartTime.AddHours(wHoursPerDay)
                Else
                    wHoursPerDay = clsEmployeeClass.WorkHoursPerDay
                    dteStartTime = clsEmployeeClass.DefultStartTime
                    dteEndTime = dteStartTime.AddHours(wHoursPerDay)
                End If
            End If
            Dim startDate As Date
            Dim endDate As Date
            If clsContract.ID > 0 Then
                startDate = clsContract.StartDate
                endDate = IIf(IsNothing(clsContract.EndDate), Date.Now, clsContract.EndDate)
            End If
            Dim vacStartDate As Date
            Dim vacEndDate As Date
            Dim prevvacStartDate As Date
            Dim prevvacEndDate As Date
            Dim vacDaysDiff As Single = 0
            Dim prevvacDaysDiff As Single = 0
            '================== Check on contract [Start]
            vacStartDate = CDate(CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00")
            vacEndDate = CDate(CDate(dteWebDateChooser2).ToShortDateString() & " " & "00:00")
            If (Not IsNothing(clsContract.EndDate)) And (Not IsNothing(vacEndDate)) Then
                If vacEndDate > clsContract.EndDate Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Vacation end date not in contract period/نهاية الأجازة ليست فى فترة العقد"))
                    Return False
                End If
            End If
            '================== Check on contract [End]
            If lbVactionID.Text.Trim <> "" Then
                clsTempEmployeeVac.Find(" hrs_EmployeesVacations.ID=" & lbVactionID.Text)
                prevvacStartDate = clsTempEmployeeVac.ActualStartDate
                prevvacEndDate = clsTempEmployeeVac.ActualEndDate
                If Not IsNothing(prevvacStartDate) And prevvacStartDate >= startDate And prevvacStartDate <= endDate Then
                    If CDate(prevvacEndDate).Year = 1 Then
                        prevvacEndDate = Date.Now
                    End If
                    If prevvacEndDate > endDate Then
                        prevvacEndDate = endDate
                    End If
                    prevvacDaysDiff = GetDateDiffAccordingWH(prevvacStartDate, prevvacEndDate, wHoursPerDay, dteStartTime, dteEndTime)
                End If
                If Not IsNothing(vacStartDate) And vacStartDate >= startDate And vacStartDate <= endDate Then
                    If CDate(vacEndDate).Year = 1 Then
                        vacEndDate = Date.Now
                    End If
                    If vacEndDate > endDate Then
                        vacEndDate = endDate
                    End If
                    vacDaysDiff = GetDateDiffAccordingWH(vacStartDate, vacEndDate, wHoursPerDay, dteStartTime, dteEndTime)
                End If
            Else
                If Not IsNothing(vacStartDate) And vacStartDate >= startDate And vacStartDate <= endDate Then
                    If CDate(vacEndDate).Year = 1 Then
                        vacEndDate = Date.Now
                    End If
                    If vacEndDate > endDate Then
                        vacEndDate = endDate
                    End If
                    vacDaysDiff = GetDateDiffAccordingWH(vacStartDate, vacEndDate, wHoursPerDay, dteStartTime, dteEndTime) 'Math.Abs((DateDiff(DateInterval.Hour, vacStartDate, vacEndDate) / 24))
                End If
            End If
            If (dteWebDateChooser2 <= dteWebDateChooser1 And dteWebDateChooser2.Date > Date.MinValue) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "This End date must be more than Start date \n / تاريخ الرجوع يجب أن يكون أكبر من تاريخ البدء \n"))
                Return False
            End If
            '-------------------------===============------------------------------------------
            Dim SDate As Date
            Dim EDate As Date
            Dim ASDate As Date
            Dim AEDate As Date
            ASDate = CDate(CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00")
            AEDate = CDate(CDate(dteWebDateChooser2).ToShortDateString() & " " & "00:00")
            If AEDate.Year = 1 Then
                AEDate = Date.Now
            End If
            If AEDate < ASDate Then
                AEDate = ASDate
            End If
            If (ASDate.Year = 1 And AEDate <> Nothing) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Start must have value")
                Return False
            End If
            If (AEDate.Year <> 1 And Date.Compare(AEDate, ASDate) < 0) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Return date must greater than start date")
                Return False
            End If
            If (AEDate.Year <> 1 And Date.Compare(AEDate, ASDate) < 0) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Expected Return date must greater than Expected start date")
                Return False
            End If
            Try
                If (ClsEmployeesVacations.FindEmployeeVacations("hrs_EmployeesVacations.EmployeeID=" & ClsEmployees.ID & IIf(lbVactionID.Text.Trim <> "", " AND hrs_EmployeesVacations.ID <>" & lbVactionID.Text, ""))) Then
                    Dim tab As DataTable = ClsEmployeesVacations.DataSet.Tables(0).Copy()
                    For Each row As DataRow In tab.Rows
                        SDate = row("ActualStartDate")
                        EDate = IIf(IsDBNull(row("ActualEndDate")), Date.Now, row("ActualEndDate"))
                        If (EDate < SDate) Then
                            EDate = SDate
                        End If
                        If (CheckDateBetween2Dates(ASDate, SDate, EDate.AddDays(-1))) Then
                            strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This Employee is already in vacation \n / هذه الموظف موجود فى أجازة بالفعل \n ")
                            bErorr = True
                            Exit For
                        End If
                        If (CheckDateBetween2Dates(AEDate, SDate, EDate.AddDays(-1))) Then
                            strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This Employee is already in vacation \n / هذه الموظف موجود فى أجازة بالفعل \n ")
                            bErorr = True
                            Exit For
                        End If
                        If (CheckDateBetween2Dates(SDate, ASDate, AEDate)) Then
                            strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This Employee is already in vacation \n / هذه الموظف موجود فى أجازة بالفعل \n ")
                            bErorr = True
                            Exit For
                        End If
                        If (CheckDateBetween2Dates(EDate.AddDays(-1), ASDate, AEDate)) Then
                            strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This Employee is already in vacation \n / هذه الموظف موجود فى أجازة بالفعل \n ")
                            bErorr = True
                            Exit For
                        End If
                    Next
                End If
            Catch ex As Exception
                Page.Session.Add("ErrorValue", ex)
                Page.Response.Redirect("ErrorPage.aspx")
            End Try
            Try
                With ClsEmployeesVacations
                    If ClsEmployees.Find("Code='" & txtEmployee.Text & "'") Then
                        .EmployeeID = ClsEmployees.ID
                        Select Case ClsEmployees.Sex
                            Case "M"
                                ClsVacationTypes.Find(" Code = 113")
                                If Not ClsVacationTypes.Sex = "M" And Not ClsVacationTypes.Sex.ToString.Trim = "" Then
                                    strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This kind of Vacation is not suitable for this employee \n / هذا النوع من الأجازة غير ملائم لهذا الموظف \n")
                                    bErorr = True
                                End If
                            Case "F"
                                ClsVacationTypes.Find(" Code = 113")
                                If Not ClsVacationTypes.Sex = "F" And Not ClsVacationTypes.Sex.ToString.Trim = "" Then
                                    strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This kind of Vacation is not suitable for this employee \n / هذا النوع من الأجازة غير ملائم لهذا الموظف \n")
                                    bErorr = True
                                End If
                        End Select

                    End If
                    .VacationTypeID = ClsVacationTypes.ID
                    .ActualStartDate = CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00"
                    .ActualEndDate = CDate(dteWebDateChooser2).ToShortDateString() & " " & "00:00"
                    .ExpectedStartDate = CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00"
                    .ExpectedEndDate = CDate(dteWebDateChooser2).ToShortDateString() & " " & "00:00"



                    If .ActualEndDate.Year = 1 Then
                        .ActualEndDate = Nothing
                    End If
                    If .ExpectedEndDate.Year = 1 Then
                        .ExpectedEndDate = Nothing
                    End If
                    If .ActualEndDate <> Nothing Then
                        .ConsumDays = (DateDiff(DateInterval.Day, .ActualStartDate, .ActualEndDate))
                    End If

                End With
                If bExceed And (Not bErorr) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, strErrorMsg)
                ElseIf bErorr Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, strErrorMsg)
                    Return False
                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function GetValues(ByRef ClsEmployeesVacations As Clshrs_EmployeesVacations) As Boolean
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ClsNationality As New ClsBasicFiles(Page, "sys_Nationalities")
        Dim ClsUser As New Clssys_Users(Page)
        Try

            SetToolBarDefaults()
            With ClsEmployeesVacations
                If ClsEmployees.Find("ID=" & .EmployeeID) Then
                    txtEmployee.Text = ClsEmployees.Code
                    lblDescEnglishName.Text = ClsEmployees.EnglishName
                    If Not IsNothing(ClsEmployees.NationalityID) Then
                        ClsNationality.Find("Id=" & ClsEmployees.NationalityID)
                    End If
                    lblDescNationality.Text = ClsNationality.EngName
                End If
                lbVactionID.Text = .ID.ToString()
                WebDateChooser1.Value = Math.Round(.GetHigriDate(.ActualStartDate), 2)
                WebDateChooser2.Value = Math.Round(.GetHigriDate(.ActualEndDate), 2)
                If Not .ActualEndDate.Year = 1 Then
                    WebDateChooser1.Enabled = False
                    WebDateChooser2.Enabled = False
                    txtVactiondays.Enabled = False
                Else
                    WebDateChooser1.Enabled = True
                    WebDateChooser2.Enabled = True
                    txtVactiondays.Enabled = True
                End If
                If Not .RegUserID = Nothing Then
                    ClsUser.Find("ID=" & .RegUserID)
                End If
                If ClsUser.EngName = Nothing Then
                    lblRegUserValue.Text = ""
                Else
                    lblRegUserValue.Text = ClsUser.EngName
                End If
                If Convert.ToDateTime(.RegDate).Date = Nothing Then
                    lblRegDateValue.Text = ""
                Else
                    lblRegDateValue.Text = Convert.ToDateTime(.RegDate).Date
                End If

                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                Else
                    ImageButton_Delete.Enabled = True
                End If
                Dim item As New System.Web.UI.WebControls.ListItem()
                Dim ClsVacType As New Clshrs_VacationsTypes(Page)
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(.ConnectionString)
                ClsVacType.GetDropDownList(DdlVacationType)
                ClsVacType.Find(" ID= " & IIf(IsNothing(.VacationTypeID), 0, .VacationTypeID))
                If ClsVacType.ID > 0 Then
                    item.Value = .VacationTypeID
                    item.Text = ObjNavigationHandler.SetLanguage(Page, ClsVacType.EngName & "/" & ClsVacType.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Page, ClsVacType.ArbName & "/" & ClsVacType.EngName)
                    End If
                    If Not DdlVacationType.Items.Contains(item) Then
                        DdlVacationType.Items.Add(item)
                        DdlVacationType.SelectedValue = item.Value
                    Else
                        DdlVacationType.SelectedValue = .VacationTypeID
                    End If
                End If

                Dim StrMode As String = String.Empty
                If (.ID > 0) Then
                    StrMode = "E"
                Else
                    StrMode = "N"
                End If
                SetToolBarPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, StrMode)
                SetToolBarRecordPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, ClsEmployeesVacations.Table, .ID)
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                End If
                If Page.IsPostBack Then
                    CreateOtherFields(.ID)
                End If
            End With
            Return True
        Catch ex As Exception
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
    Private Function SetToolbarSetting(ByVal ptrType As String, ByVal ClsClass As Object, ByVal intID As Integer) As Boolean
        Try
            Select Case ptrType
                Case "N", "R"


                    ImageButton_Delete.Enabled = False
                    ImageButton_Properties.Visible = False
                    LinkButton_Properties.Visible = False
                    ImageButton_Remarks.Visible = False
                    LinkButton_Remarks.Visible = False

                Case "D"
                    ClsEmployeesVacations.Find("ID=" & intID)
                    GetValues(ClsEmployeesVacations)

                    ImageButton_Save.Visible = False

                Case "E"
                    ClsEmployeesVacations.Find("ID=" & intID)
                    GetValues(ClsEmployeesVacations)

                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsEmployeesVacations
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
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Me.Page)
        Try
            With ClsEmployeesVacations
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
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Me.Page)
        If IntId > 0 Then
            ClsEmployeesVacations.Find("ID=" & IntId)
            GetValues(ClsEmployeesVacations)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Me.Page)
        ClsEmployeesVacations.Find(" code = '" & txtEmployee.Text & "'")
        Dim recordID As Integer = ClsEmployeesVacations.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEmployeesVacations.Table) = True Then
            Dim StrTablename As String
            ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
            StrTablename = ClsEmployeesVacations.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID &
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID &
                                    " And sys_OtherFields.CancelDate is Null ")
            objDS = clsOtherFieldsData.DataSet
            name.Text = ""
            realname.Text = ""
            If objDS.Tables(0).Rows.Count > 0 Then
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "U", objDS, "Interfaces_frmRegions")
            Else
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "A", objDS, "Interfaces_frmRegions")
            End If
        End If
    End Function
    Public Function GetRecordInfoAjax(ByVal recordID As Integer) As Boolean
        Try
            Dim dsContractsTransactions As New DataSet
            Dim dsUser As New DataSet
            Dim clsUser As New Clssys_Users(Page)
            Dim clsContr As New Clshrs_ContractsTransactions(Page)
            clsContr.Find("ID=" & recordID)
            If clsContr.ID > 0 Then
                If Not clsContr.RegUserID = Nothing Then
                    clsUser.Find("ID=" & clsContr.RegUserID)
                    If clsUser.ID > 0 Then
                        lblRegUserValue.Text = clsUser.EngName
                    Else
                        lblRegUserValue.Text = ""
                    End If
                End If
                If Convert.ToDateTime(clsContr.RegDate).Date = Nothing Then
                    lblRegDateValue.Text = ""
                Else
                    lblRegDateValue.Text = clsContr.RegDate
                End If

            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Shared Function"
    Public Shared Function Find(ByVal Table As String, ByVal Filter As String, ByRef DataSet As DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Dim mSelectCommand = " Select * From " & Table
        Dim mSqlDataAdapter As New SqlClient.SqlDataAdapter
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where " & Filter & " And CancelDate IS Null", " Where CancelDate IS Null")
            StrSelectCommand = StrSelectCommand '& orderByStr
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, ConnStr)
            DataSet = New DataSet
            mSqlDataAdapter.Fill(DataSet)
            If DataSet.Tables(0).Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function Contracts_ContractValidatoinId(ByVal EmployeeID As Integer, ByVal CurrentDate As Date) As Integer
        Dim dsContracts As New DataSet
        Dim DateNow As String
        If Not ClsDataAcessLayer.IsGreg(DateTime.Now.ToString("dd/MM/yyyy")) Then
            DateNow = ClsDataAcessLayer.FormatGregString(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        Else
            DateNow = DateTime.Now.ToString("dd/MM/yyyy")
        End If

        If Find("hrs_Contracts", "Employeeid=" & EmployeeID & "  And StartDate <= '" & DateNow & "' And (enddate is null or '" & DateNow & "' Between StartDate and EndDate)", dsContracts) Then
            Return dsContracts.Tables(0).Rows(0).Item("ID")
        Else
            Return 0
        End If
    End Function
    Public Shared Function Contracts_ContractValidatoinId(ByVal EmployeeID As Integer) As Integer
        Try
            Dim dsContracts As New DataSet
            Dim DateNow As String
            If Not ClsDataAcessLayer.IsGreg(DateTime.Now.ToString("dd/MM/yyyy")) Then
                DateNow = ClsDataAcessLayer.FormatGregString(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            Else
                DateNow = DateTime.Now.ToString("dd/MM/yyyy")
            End If
            If Find("hrs_Contracts", "Employeeid=" & EmployeeID & "  And StartDate <= '" & DateNow & "' And (enddate is null or '" & DateNow & "' Between StartDate and EndDate)", dsContracts) Then
                Return dsContracts.Tables(0).Rows(0).Item("ID")
            Else
                Return 0
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function GetEmployeeVacSum(ByVal intEmployeeID As Integer, ByVal intVacTypeID As Integer, ByVal dateStartDate As Date, ByVal dateEndDate As Date, ByVal intContractId As Integer) As Double
        Dim wHoursPerDay As Double = 0
        Dim dteActualStartDate As Date
        Dim dteactualEndDate As Date
        Dim dteTempActualStartDate As Date
        Dim dteTempActualEndDate As Date
        Dim dblDiffHours As Double
        Dim dblDiffDays As Double
        Dim dteStartTime As Date
        Dim dteEndTime As Date
        Dim dteThisStartDateStartTime As Date
        Dim dteThisStartDateEndTime As Date
        Dim dteThisEndDateStartTime As Date
        Dim dteThisEndDateEndTime As Date
        Dim dsEmpVac As New DataSet
        Dim dsContracts As New DataSet
        Dim dsEmployeesClasses As New DataSet
        Try
            Find("hrs_Contracts", " ID =" & intContractId, dsContracts)

            If Find("hrs_EmployeesClasses", "ID=" & IIf(dsContracts.Tables(0).Rows(0).Item("EmployeeClassID") > 0, dsContracts.Tables(0).Rows(0).Item("EmployeeClassID"), 0), dsEmployeesClasses) Then
                With dsEmployeesClasses.Tables(0).Rows(0)
                    If IsNothing(.Item("WorkHoursPerDay")) Then
                        wHoursPerDay = 9
                        dteStartTime = New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, 9, 0, 0)
                        dateStartDate = New Date(dateStartDate.Year, dateStartDate.Month, dateStartDate.Day, 9, 0, 0)
                        dteEndTime = dteStartTime.AddHours(wHoursPerDay)
                        dateEndDate = New Date(dateEndDate.Year, dateEndDate.Month, dateEndDate.Day, 9, 0, 0).AddHours(wHoursPerDay)
                    Else
                        wHoursPerDay = .Item("WorkHoursPerDay")
                        dteStartTime = .Item("DefultStartTime")
                        dateStartDate = New Date(dateStartDate.Year, dateStartDate.Month, dateStartDate.Day, dteStartTime.Hour, dteStartTime.Minute, 0)
                        dteEndTime = CDate(.Item("DefultStartTime")).AddHours(wHoursPerDay)
                        dateEndDate = New Date(dateEndDate.Year, dateEndDate.Month, dateEndDate.Day, dteStartTime.Hour, dteStartTime.Minute, 0).AddHours(wHoursPerDay)
                    End If
                End With
            End If
            Dim NoOfWorkingHours As Double = wHoursPerDay
            Dim NoOfNonWorkingHours As Double = 24 - NoOfWorkingHours
            Find("hrs_EmployeesVacations", "EmployeeID=" & intEmployeeID & " And VacationTypeID=" & intVacTypeID & " And (ActualStartDate Between '" & dateStartDate & "' And '" & dateEndDate & "' OR IsNull(ActualEndDate,'" & IIf(Date.Now > dateEndDate, dateEndDate, Date.Now) & "') Between '" & dateStartDate & "' And '" & dateEndDate & "' OR  '" & dateStartDate & "' Between ActualStartDate AND IsNull(ActualEndDate,'" & IIf(Date.Now > dateEndDate, dateEndDate, Date.Now) & "') ) ", dsEmpVac)
            Dim vacDays As Double = 0
            For Each row As DataRow In dsEmpVac.Tables(0).Rows
                If IsDBNull(row("ActualStartDate")) Then
                    row("ActualStartDate") = CDate(Nothing)
                End If
                dteActualStartDate = IIf(CDate(row("ActualStartDate")) < dateStartDate, dateStartDate, row("ActualStartDate"))
                dteactualEndDate = GetNDate_Shared(row("ActualEndDate"), IIf(Date.Now > dateEndDate, dateEndDate, Date.Now))
                dteactualEndDate = IIf(dteactualEndDate > dateEndDate, dateEndDate, dteactualEndDate)
                If dteactualEndDate < dteActualStartDate Then
                    Continue For
                End If
                dteThisStartDateStartTime = New Date(dteActualStartDate.Year, dteActualStartDate.Month, dteActualStartDate.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
                dteThisStartDateEndTime = New Date(dteActualStartDate.Year, dteActualStartDate.Month, dteActualStartDate.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
                dteThisEndDateStartTime = New Date(dteactualEndDate.Year, dteactualEndDate.Month, dteactualEndDate.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
                dteThisEndDateEndTime = New Date(dteactualEndDate.Year, dteactualEndDate.Month, dteactualEndDate.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
                dteTempActualStartDate = New Date(dteActualStartDate.Year, dteActualStartDate.Month, dteActualStartDate.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
                dteTempActualEndDate = New Date(dteactualEndDate.Year, dteactualEndDate.Month, dteactualEndDate.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
                If dteActualStartDate.Day = dteactualEndDate.Day And
                   dteActualStartDate.Month = dteactualEndDate.Month And
                   dteActualStartDate.Year = dteactualEndDate.Year And
                   ((dteActualStartDate > dteTempActualEndDate And dteactualEndDate > dteTempActualEndDate) Or
                     (dteActualStartDate < dteTempActualStartDate And dteactualEndDate < dteTempActualStartDate)) Then
                    Continue For
                End If
                If DateDiff(DateInterval.Day, dteActualStartDate, dteactualEndDate) = 0 And
                    (dteActualStartDate >= dteThisStartDateEndTime And dteactualEndDate <= dteThisEndDateStartTime) Then
                    Continue For
                End If
                If dteActualStartDate < dteTempActualStartDate Then
                    dteActualStartDate = dteTempActualStartDate
                ElseIf dteActualStartDate > dteTempActualStartDate And dteActualStartDate > dteThisStartDateEndTime Then
                    Dim dteActualStartDatePlusDay As Date = dteActualStartDate.AddDays(1)
                    dteActualStartDate = New Date(dteActualStartDatePlusDay.Year, dteActualStartDatePlusDay.Month, dteActualStartDatePlusDay.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
                End If
                If dteactualEndDate > dteTempActualEndDate Then
                    dteactualEndDate = dteTempActualEndDate
                ElseIf dteactualEndDate < dteTempActualEndDate And dteactualEndDate < dteThisEndDateStartTime Then
                    Dim dteActualEndDateMiunsDay As Date = dteactualEndDate.AddDays(-1)
                    dteactualEndDate = New Date(dteActualEndDateMiunsDay.Year, dteActualEndDateMiunsDay.Month, dteActualEndDateMiunsDay.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
                End If
                dblDiffHours = DateDiff(DateInterval.Minute, dteActualStartDate, dteactualEndDate) / 60
                dblDiffDays = DateDiff(DateInterval.Day, dteActualStartDate, dteactualEndDate)
                vacDays += (dblDiffHours - (dblDiffDays * NoOfNonWorkingHours)) / NoOfWorkingHours
            Next
            Return vacDays
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function Contract_GetDurationDaysForPeriod(ByVal intContractID As Integer, ByVal intVacType As Integer, ByVal dteStartDate As DateTime) As DataSet
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(CType(HttpContext.Current.Session("ConnectionString"), String), "GetDurationDaysForPeriod", intContractID, intVacType, dteStartDate)
    End Function
    Public Shared Function GetFieldDescription(ByVal StrCode As String, ByVal StrTableName As String) As String
        Dim StrReturnData As Object
        Try
            StrReturnData = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, " Select EngName + '/' + ArbName From " & StrTableName & " Where Code = '" & StrCode.ToString.TrimStart.TrimEnd & "'")
            If IsNothing(StrReturnData) Then Return "/"
            If IsDBNull(StrReturnData) Then Return "/"
            Return StrReturnData
        Catch ex As Exception
            Return "/"
        End Try
    End Function

#End Region

#Region "PageMethods"
    <System.Web.Services.WebMethod()>
    Public Shared Function GetEmployeeID(ByVal strEmpCode As String) As String
        Dim dsEmp As New DataSet
        If Find("hrs_Employees", "Code='" & strEmpCode & "'", dsEmp) Then
            Return dsEmp.Tables(0).Rows(0).Item("ID").ToString
        Else
            Return "0"
        End If
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function CheckEmployeeContract(ByVal IntEmployeeId As Integer, ByVal dteDateToCheck As Date) As String
        If Contracts_ContractValidatoinId(IntEmployeeId, dteDateToCheck) > 0 Then
            Return "1"
        Else
            Return "0"
        End If
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function RetTable(ByVal strTableName As String) As Object
        Dim tbl As New Data.DataTable(strTableName)
        tbl.Columns.Add(New Data.DataColumn("EmpID", GetType(Integer)))
        tbl.Columns.Add(New Data.DataColumn("EmpName", GetType(String)))
        For i As Int16 = 1 To 4
            Dim nRow As Data.DataRow = tbl.NewRow()
            nRow(0) = i
            nRow(1) = "Employee " + i.ToString()
            tbl.Rows.Add(nRow)
        Next
        Return tbl
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function Get_Searched_Description(ByVal IntSearchId As Integer, ByVal strCode As String) As String
        Dim dsSearchs As New Data.DataSet
        Find("sys_Searchs", " sys_Searchs.Id = " & IntSearchId, dsSearchs)
        Dim dsObjects As New Data.DataSet
        Find("sys_Objects", " sys_Objects.Id = " & dsSearchs.Tables(0).Rows(0).Item("ObjectID"), dsObjects)
        Return GetFieldDescription(strCode, dsObjects.Tables(0).Rows(0).Item("Code"))
    End Function
#End Region

    Protected Sub LinkButton_Payments_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Payments.Click
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim ClsCompanies As New Clssys_Companies(Page)
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim url As String = ""

        'If uwgEmployeeVacations.DisplayLayout.SelectedRows.Count > 0 Then
        If CheckEmployee() Then
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            ClsCompanies.Find("ID = " & ClsEmployees.CompanyID)

            If (ClsCompanies.VacSettlement) Then
                If (lbVactionID.Text <> "") Then
                    url = "OpenModal1('frmEmployeesVacationSettlementDependonVacation.aspx?Fisical=" & WebDateChooser1.Value & "&ID=" & ClsEmployees.ID & "&TrnsID=" & IIf(lbVactionID.Text = "", 0, lbVactionID.Text) & "&Dys=" & IIf(lbVactionID.Text = "", 0, lblTotalDays.Text) & "',600,900,false,'');"
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "You must select vacation/يجب أن تختار إجازة"))
                    Exit Sub
                End If
            Else
                url = "OpenModal1('frmEmployeesVacationTransactions.aspx?Fisical=" & WebDateChooser1.Value & "&ID=" & ClsEmployees.ID & "&TrnsID=" & IIf(lbVactionID.Text = "", 0, lbVactionID.Text) & "&Dys=" & IIf(lbVactionID.Text = "", 0, lblTotalDays.Text) & "',600,900,false,'');"
            End If

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", url, True)
        End If
        ' Else
        ' Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "You must select vacation/يجب أن تختار أجازة"))
        '  End If

    End Sub


    Protected Sub WebDateChooser3_ValueChanged(sender As Object, e As Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs) Handles WebDateChooser3.ValueChanged
        If lbVactionID.Text = "" Then
            txtVactiondays.Text = CDate(WebDateChooser3.Value).Subtract(CDate(WebDateChooser1.Value)).Days
            txtVactiondays_ValueChange(Nothing, Nothing)
        End If

    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        'If uwgEmployeeVacations.DisplayLayout.SelectedRows.Count > 0 Then
        If CheckEmployee() Then
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            Dim url As String = "OpenModal1('frmEmployeesVacationTransactions2.aspx?Fisical=" & WebDateChooser1.Value & "&ID=" & ClsEmployees.ID & "&TrnsID=" & IIf(lbVactionID.Text = "", 0, lbVactionID.Text) & "&Dys=" & IIf(lbVactionID.Text = "", 0, lblTotalDays.Text) & "',800,1000,false,'');"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", url, True)
        End If
        ' Else
        ' Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "You must select vacation/يجب أن تختار أجازة"))
        '  End If
    End Sub
End Class
