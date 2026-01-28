Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Venus.Shared.Web
Imports Infragistics.Documents.Excel.Serialization
Imports Venus.Shared

Partial Class frmEmployeesVacations
    Inherits MainPage
#Region "Public Decleration"
    Private ClsEmployeesVacations As Clshrs_EmployeesVacations
    Private ClsEmployees As Clshrs_Employees
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Const csOtherFields = 11
    Private ClsDepartment As Clssys_Departments
    Private ClsPosition As Clshrs_Positions



    Dim PrintRequestSerial As String
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim recordID As Integer
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Try
            ddlRequestDate.Enabled = False
            'Rabie
            If Not IsPostBack Then
                Dim WebHandler As New Venus.Shared.Web.WebHandler
                Dim User As String = String.Empty
                ClsEmployees = New Clshrs_Employees(Page)
                WebHandler.GetCookies(Page, "UserID", User)
                Dim _sys_User As New Clssys_Users(Page)
                _sys_User.Find("ID = '" & User & "'")
                txtEmployee.Text = _sys_User.Code
                txtRequesterUser.Text = _sys_User.Code
                ClsEmployees.Find("Code ='" & txtEmployee.Text & "'")
                If ClsEmployees.InsertRequestsForAnotherEmployee Then
                    btnSearchCode.Visible = True
                    txtEmployee.Enabled = True
                Else
                    btnSearchCode.Visible = False
                    txtEmployee.Enabled = False

                End If
                Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
                'txtEmployee.Enabled = False
                ddlRequestDate.Value = ClsEmployeesVacations.GetHigriDate(Date.Now.Date)
                GetDropDownList(DdlRequestType, False)

                'ClsVacationTypes.GetDropDownList(DdlVacationType, False, "IsAnnual=0",)
                ClsEmployeesVacations.AddNotificationOnChange(Page)
                Dim csSearchID As Integer
                Dim ClsLevels As New Clshrs_LevelTypes(Page)
                Dim ClsDataHandler As New Venus.Shared.DataHandler
                Dim StrSerial As String = String.Empty
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
                Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)
                Dim ClsObjects As New Clssys_Objects(Page)
                Dim ClsSearchs As New Clssys_Searchs(Page)
                Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language = ""javascript"">IntializeDataChanged()</script>")
                'ClsObjects.Find(" Code='SS_VacationRequest'")
                ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'")
                ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
                csSearchID = ClsSearchs.ID
                lblLage.Text = ObjNavigationHandler.SetLanguage(Page, "0/1")
                Page.Session.Add("Lage", lblLage.Text)
                Dim IntDimension As Integer = 510
                Dim UrlStringEmp = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmployee.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtEmployee.ClientID & "'"
                btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlStringEmp & ")"
                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
                Dim str As String = lbVactionID.Text

                CheckEmpCode()
                Dim DteStartDate As Date = Date.Now
                Dim DteEndDate As Date = Date.Now
                If Request.QueryString.Count > 0 Then
                    If Request.QueryString.Item("EmpCode") <> Nothing Then
                        txtEmployee.Text = Request.QueryString.Item("EmpCode")

                    End If
                    If Request.QueryString.Item("StartDate") <> Nothing Then
                        DteStartDate = Request.QueryString.Item("StartDate")
                    End If
                    If Request.QueryString.Item("ToDate") <> Nothing Then
                        DteEndDate = Request.QueryString.Item("ToDate")
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

                    SetTime()

                End If
            Else
                'txtEmployee_TextChanged(Nothing, Nothing)
                'txtAlternative_TextChanged(Nothing, Nothing)
                'lbRemainVal_TextChanged(Nothing, Nothing)
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

            Case "SaveNew"
                SavePart()

            Case "Save"
                If txtEmployee.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If SavePart() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                    ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">OpenPrintedScreen(" & PrintRequestSerial & ");</script>")

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

        SetToolBarPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, strMode)
        SetToolBarRecordPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, ClsEmployeesVacations.Table, IntId)
        If strMode = "N" Then
            'ImageButton_reelete.Enabled = False
        End If
    End Sub

    Protected Sub txtEmployee_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmployee.TextChanged
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

            With ClsEmployees
            End With

            If Not String.IsNullOrEmpty(txtEmployee.Text) Then
                clsEmployeeClass.Find("ID=" & IIf(clsContract.EmployeeClassID > 0, clsContract.EmployeeClassID, 0))
                'hdnAnnualVacId.Value = DdlVacationType.SelectedValue
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


                End If
            End If
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub




#End Region

#Region "Private Functions"
    Private Function SetNew() As Boolean
        Dim ClsEmployeesVacations As New Clshrs_EmployeesVacations(Page)
        Try
            DdlRequestType.Enabled = True

            SetTime()
            LinkButton_OverDueMessage.Visible = False
            lbVactionID.Text = ""
            txtEmployee.Focus()
            TxtRemarks.Text = ""
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
                    'If TypeOf (currCtrl) Is TextBox Then
                    'End If
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


    Private Function CheckEmpCode() As Boolean
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsNationality = New Clssys_Nationality(Page)
        Dim BolExist As Boolean
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Try
            SetTime()
            'lbVactionID.Text = ""
            'DdlVacationType.SelectedIndex = 0
            'WebDateChooser1.Value = ClsEmployees.GetHigriDate(Date.Now)
            'WebDateChooser2.Value = Nothing
            'WebDateChooser1.Enabled = True
            'WebDateChooser2.Enabled = True
            'txtVactiondays.Enabled = True
            'lbTotalVal.Text = 0
            'lblTotalDays.Text = 0
            ''txtVactiondays.Text = 0
            'lbRemainVal.Text = 0
            'LinkButton_OverDueMessage.Visible = False
            'lbRemainVal.CssClass = "noramlRemain"

            ClsEmployees.Find("Code ='" & txtEmployee.Text & "'")
            If ClsEmployees.ID > 0 Then
                txtEmployee.Text = ClsEmployees.Code
                lblDescEnglishName.Text = ClsEmployees.EnglishName
                lblDescEnglishName.Enabled = False
                Dim ClsDepartment = New Clssys_Departments(Page)
                ClsDepartment.Find("id=" & ClsEmployees.DepartmentID & "")

                If ProfileCls.CurrentLanguage = "Ar" Then
                    txtDepartment.Text = ClsDepartment.ArbName
                Else

                    txtDepartment.Text = ClsDepartment.EngName

                End If
                txtDepartment.Enabled = False
                GetEmpPosition()


                If Not IsNothing(ClsEmployees.NationalityID) Then
                    ClsNationality.Find("Id=" & ClsEmployees.NationalityID)
                    SetScreenInformation("N")

                    'FillEmployeeVacations(ClsEmployees.ID, False)
                    'GetEmployeeVacationRequest()
                    Dim ClsEmployeeRelated = New Clshrs_Employees(Page)

                    BolExist = True
                Else
                    BolExist = False
                End If
            Else
                BolExist = False
            End If
            'If (Not BolExist) Then
            '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Employee not found/هذا الموظف غير موجود"))
            '    lblDescEnglishName.Text = ""
            '    lblDescNationality.Text = ""
            '    lbTotalVal.Text = 0
            '    lblTotalDays.Text = 0
            '    LinkButton_OverDueMessage.Visible = False
            '    lbRemainVal.Text = 0
            '    txtEmployee.Text = ""
            '    lbVactionID.Text = ""
            '    'uwgEmployeeVacations.Clear()
            '    txtEmployee.Focus()
            'End If
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
        Dim cls_Company = New Clssys_Companies(Page)
        Dim cls_Employee = New Clshrs_Employees(Page)

        Dim recordId As Integer
        Dim RemVal As Integer = 0
        Try

            If SaveVariousRequest() Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Request Saved Successfully /تم حفظ الطلب بنجاح "))

            End If

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
    Public Function SaveVariousRequest() As Boolean
        Dim InsertCommand As String
        Try
            Dim ClsEmployeeVacation As New Clshrs_EmployeesVacations(Page)

            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeeVacation.ConnectionString)
            Dim ClsEmployeesVacations2 = New Clshrs_EmployeesVacations(Page)



            Dim ConnectionString As String
            ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
            Dim SqlCommand As Data.SqlClient.SqlCommand

            Dim ClsEmployees As New Clshrs_Employees(Page)
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler





            WebHandler.GetCookies(Page, "UserID", User)
            'InsertCommand = "Insert Into    Values(''," & ClsEmployees.ID & "," & DdlRequestType.SelectedItem.Value & ",'SS_0013',convert(datetime, '" & ddlRequestDate.Value & "',103),convert(datetime, '" & WebDateChooser1.Value & "',103) ,convert(datetime, '" & WebDateChooser2.Value & "',103),'', " & lbRemainVal.Text & " ,'" & txtContactNo.Text & "', ' ','" & TxtRemarks.Text & "'," & User & " ,convert(datetime,  '" & ClsEmployeesVacations.GetHigriDate(Date.Now) & "' ,103))"
            SqlCommand = New SqlClient.SqlCommand
            SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
            SqlCommand.CommandType = CommandType.Text
            SqlCommand.CommandText = InsertCommand
            SqlCommand.Connection.Open()
            SqlCommand.ExecuteNonQuery()
            SqlCommand.Connection.Close()

            Dim UpdateCommand = "Update  SS_VacationRequest  set Code ='" & GenerateRequestCode() & "' where ID=" & GetInertedID() & " "
            PrintRequestSerial = GetInertedID().ToString()
            SqlCommand = New SqlClient.SqlCommand
            SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
            SqlCommand.CommandType = CommandType.Text
            SqlCommand.CommandText = UpdateCommand
            SqlCommand.Connection.Open()
            SqlCommand.ExecuteNonQuery()
            SqlCommand.Connection.Close()
            SaveFirstLeveNotification()

            Return True
        Catch ex As Exception
            Page.Session.Add("ErrorValue", ex)
            'mErrorHandler.RecordExceptions_DataBase(InsertCommand, ex, Err.Number, DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function SaveFirstLeveNotification() As Boolean
        Try
            Dim SqlCommand As Data.SqlClient.SqlCommand
            Dim ClsEmployees As New Clshrs_Employees(Page)
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            Dim ConnectionString As String
            ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
            Dim strselect As String
            strselect = "select * from SS_Configuration where FormCode='SS_0013'and Rank=1"
            Dim DsFirstLevel As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
            If DsFirstLevel.Tables(0).Rows.Count > 0 Then
                For Each Row As Data.DataRow In DsFirstLevel.Tables(0).Rows
                    'DirectManager
                    If Row("UserTypeID") = 1 Then
                        Dim strdirectmanager As String
                        strdirectmanager = "select ManagerID from hrs_Employees where Code='" & txtEmployee.Text & "'"
                        Dim DirectManagerID As String
                        DirectManagerID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, strdirectmanager)
                        '==================CheckDelegation===========
                        Dim DelegatedEmpID As Integer
                        DelegatedEmpID = CheckDelegationSchedule(DirectManagerID)
                        If DelegatedEmpID > 0 Then

                            DirectManagerID = DelegatedEmpID

                        End If
                        Dim strinsert As String
                        Dim strMaxRequestSerial As String
                        strMaxRequestSerial = "select isnull(Max (ID),1) from SS_VacationRequest where EmployeeID=" & ClsEmployees.ID & " "
                        Dim MaxSerial As Integer
                        SqlCommand = New SqlClient.SqlCommand
                        SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
                        SqlCommand.CommandType = CommandType.Text
                        SqlCommand.CommandText = strMaxRequestSerial
                        SqlCommand.Connection.Open()
                        MaxSerial = CInt(SqlCommand.ExecuteScalar())
                        SqlCommand.Connection.Close()




                        strinsert = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen,ConfigID)  values(" & MaxSerial & " , " & DirectManagerID & ",'SS_0013'," & ClsEmployees.ID & ",0," & Row("ID") & ")"
                        SqlCommand = New SqlClient.SqlCommand
                        SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
                        SqlCommand.CommandType = CommandType.Text
                        SqlCommand.CommandText = strinsert
                        SqlCommand.Connection.Open()
                        SqlCommand.ExecuteNonQuery()
                        SqlCommand.Connection.Close()
                    End If

                    'Position
                    If Row("UserTypeID") = 2 Then
                        Dim clshrspositions As New Clshrs_Positions(Page)
                        Dim AppIDStr As String = "SELECT MultiBranchedPosition FROM sys_SystemConfig"
                        Dim MultiBranchedPosition As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, AppIDStr)


                        Dim strempinposition As String = "select  distinct EmployeeID from hrs_Contracts where PositionID=" & Row("PositionID") & " and CancelDate is null And (EndDate>=getdate() or EndDate  is null)"
                        If Not IsDBNull(MultiBranchedPosition) AndAlso CBool(MultiBranchedPosition) Then
                            If CBool(MultiBranchedPosition) Then
                                strempinposition = "SELECT hrs_JobBranchesPermission.EmployeeId as EmployeeID FROM hrs_JobBranchesPermission INNER JOIN hrs_JobBranchesPermissionDetails ON hrs_JobBranchesPermission.ID =  hrs_JobBranchesPermissionDetails.JobBranchesPermissionId  where hrs_JobBranchesPermission.PositionID=" & Row("PositionID") & " and hrs_JobBranchesPermissionDetails.BranchId=" & ClsEmployees.BranchID
                            End If
                        End If
                        Dim DsPositionEmployees As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strempinposition)
                        If DsPositionEmployees.Tables(0).Rows.Count > 0 Then
                            Dim strinsert As String
                            Dim strMaxRequestSerial As String
                            strMaxRequestSerial = "select isnull(Max (ID),1) from SS_VacationRequest where EmployeeID=" & ClsEmployees.ID & " and AnnualVacationType<>'1' "
                            Dim MaxSerial As Integer
                            SqlCommand = New SqlClient.SqlCommand
                            SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
                            SqlCommand.CommandType = CommandType.Text
                            SqlCommand.CommandText = strMaxRequestSerial
                            SqlCommand.Connection.Open()
                            MaxSerial = CInt(SqlCommand.ExecuteScalar())
                            SqlCommand.Connection.Close()

                            For Each RW In DsPositionEmployees.Tables(0).Rows
                                Dim DelegatedEmpID As Integer
                                DelegatedEmpID = CheckDelegationSchedule(RW("EmployeeID"))
                                If DelegatedEmpID > 0 Then

                                    RW("EmployeeID") = DelegatedEmpID

                                End If
                                strinsert = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen)  values(" & MaxSerial & " , " & RW("EmployeeID") & ",'SS_0013'," & ClsEmployees.ID & ",0)"
                                SqlCommand = New SqlClient.SqlCommand
                                SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
                                SqlCommand.CommandType = CommandType.Text
                                SqlCommand.CommandText = strinsert
                                SqlCommand.Connection.Open()
                                SqlCommand.ExecuteNonQuery()
                                SqlCommand.Connection.Close()

                            Next
                        Else
                            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry Can not proceed your request because there are no employees in the next level ...Please contact system admin  / عفوا لايمكن تسجيل الطلب لعدم وجود موظفين في المرحلة التالية ... يرجي مراجعة مدير النظام"))
                        End If
                    End If
                    If Row("UserTypeID") = 3 Then
                        Dim strinsert As String
                        Dim strMaxRequestSerial As String
                        strMaxRequestSerial = "select isnull(Max (ID),1) from SS_VacationRequest where EmployeeID=" & ClsEmployees.ID & " "
                        Dim MaxSerial As Integer
                        SqlCommand = New SqlClient.SqlCommand
                        SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
                        SqlCommand.CommandType = CommandType.Text
                        SqlCommand.CommandText = strMaxRequestSerial
                        SqlCommand.Connection.Open()
                        MaxSerial = CInt(SqlCommand.ExecuteScalar())
                        SqlCommand.Connection.Close()


                        Dim DelegatedEmpID As Integer
                        DelegatedEmpID = CheckDelegationSchedule(Row("EmployeeID"))
                        If DelegatedEmpID > 0 Then

                            Row("EmployeeID") = DelegatedEmpID

                        End If


                        strinsert = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen)  values(" & MaxSerial & " , " & Row("EmployeeID") & ",'SS_0013'," & ClsEmployees.ID & ",0)"
                        SqlCommand = New SqlClient.SqlCommand
                        SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
                        SqlCommand.CommandType = CommandType.Text
                        SqlCommand.CommandText = strinsert
                        SqlCommand.Connection.Open()
                        SqlCommand.ExecuteNonQuery()
                        SqlCommand.Connection.Close()
                    End If
                Next
            End If


        Catch ex As Exception

        End Try
    End Function

    Public Function CheckDelegationSchedule(EmpID As Integer) As Integer
        Dim StrGetDelegation As String
        StrGetDelegation = "select isnull(DelegatedEmployeeID,0) as DelegatedEmployeeID from SS_DelegationSChedule inner join SS_DelegationSCheduleRequests on SS_DelegationSCheduleRequests.ScheduleId=SS_DelegationSChedule.ID where DelegatorEmployeeID =" & EmpID & " and GetDate() >=FromDate and GetDate() <= Todate and SS_DelegationSCheduleRequests.RequestTypeId='SS_0013' and  (SS_DelegationSChedule.IsCanceled=0 OR SS_DelegationSChedule.IsCanceled is null Or SS_DelegationSChedule.CancelDate> GetDate() )union all select 0 as DelegatedEmployeeID "
        Dim DelegatedEmpID As Integer
        DelegatedEmpID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, StrGetDelegation)

        Return DelegatedEmpID

    End Function

    Public Function GetInertedID() As Integer
        Dim SqlCommand As Data.SqlClient.SqlCommand
        Dim ClsEmployees As New Clshrs_Employees(Page)
        ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strMaxRequestSerial As String

        strMaxRequestSerial = "select isnull(Max (ID),1) from SS_VacationRequest where EmployeeID=" & ClsEmployees.ID & ""
        Dim InsertedID As Integer
        SqlCommand = New SqlClient.SqlCommand
        SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
        SqlCommand.CommandType = CommandType.Text
        SqlCommand.CommandText = strMaxRequestSerial
        SqlCommand.Connection.Open()
        InsertedID = CInt(SqlCommand.ExecuteScalar())
        SqlCommand.Connection.Close()
        Return InsertedID
    End Function
    Public Function GenerateRequestCode() As String
        Dim RequestCode As String
        Dim SqlCommand As Data.SqlClient.SqlCommand
        Dim ClsEmployees As New Clshrs_Employees(Page)
        ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strMaxRequestSerial As String

        strMaxRequestSerial = "select isnull(Max (ID),1) from SS_VacationRequest where EmployeeID=" & ClsEmployees.ID & ""
        Dim MaxSerial As Integer
        SqlCommand = New SqlClient.SqlCommand
        SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
        SqlCommand.CommandType = CommandType.Text
        SqlCommand.CommandText = strMaxRequestSerial
        SqlCommand.Connection.Open()
        MaxSerial = CInt(SqlCommand.ExecuteScalar())
        SqlCommand.Connection.Close()
        RequestCode = "V_OTH_ " & MaxSerial & ""
        Return RequestCode
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

        'With ClsEmployees
        '    dteWebDateChooser1 = .SetHigriDate(dteWebDateChooser1)
        '    dteWebDateChooser2 = .SetHigriDate(dteWebDateChooser2)
        'End With
        Try
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            'intContractID = clsContract.ContractValidatoinId(ClsEmployees.ID, dteWebDateChooser1)
            'clsContract.Find(" ID =" & intContractID)
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
            'vacStartDate = CDate(CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00")
            'vacEndDate = CDate(CDate(dteWebDateChooser2).ToShortDateString() & " " & "00:00")
            'If (Not IsNothing(clsContract.EndDate)) And (Not IsNothing(vacEndDate)) Then
            '    If vacEndDate > clsContract.EndDate Then
            '        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Vacation end date not in contract period/نهاية الأجازة ليست فى فترة العقد"))
            '        Return False
            '    End If
            'End If
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
                'Dim remainVal As Single = lbRemainVal.Text
                'remainVal = remainVal + prevvacDaysDiff
                'remainVal = remainVal - vacDaysDiff
                'If Convert.ToSingle(lbRemainVal.Text) < 0 Then
                '    strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This Vacation exceed allowed vacation \n / \n هذه الاجازة تجاوزت الاجازة المسموحة")
                '    bExceed = True
                'End If
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
                'If Convert.ToSingle(lbRemainVal.Text) < 0 Then
                '    strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This Vacation exceed allowed vacation \n / \n هذه الاجازة تجاوزت الاجازة المسموحة")
                '    bExceed = True
                'End If
            End If
            '-------------------------===============------------------------------------------
            Dim SDate As Date
            Dim EDate As Date
            Dim ASDate As Date
            Dim AEDate As Date
            'ASDate = CDate(CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00")
            'AEDate = CDate(CDate(dteWebDateChooser2).ToShortDateString() & " " & "00:00")
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
                                'ClsVacationTypes.Find(" ID=" & DdlVacationType.SelectedItem.Value)
                                If Not ClsVacationTypes.Sex = "M" And Not ClsVacationTypes.Sex.ToString.Trim = "" Then
                                    strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This kind of Vacation is not suitable for this employee \n / هذا النوع من الأجازة غير ملائم لهذا الموظف \n")
                                    bErorr = True
                                End If
                            Case "F"
                                'ClsVacationTypes.Find(" ID=" & DdlVacationType.SelectedItem.Value)
                                If Not ClsVacationTypes.Sex = "F" And Not ClsVacationTypes.Sex.ToString.Trim = "" Then
                                    strErrorMsg += ObjNavigationHandler.SetLanguage(Page, "This kind of Vacation is not suitable for this employee \n / هذا النوع من الأجازة غير ملائم لهذا الموظف \n")
                                    bErorr = True
                                End If
                        End Select

                    End If
                    '.VacationTypeID = DdlVacationType.SelectedItem.Value
                    '.ActualStartDate = CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00"
                    '.ActualEndDate = CDate(dteWebDateChooser2).ToShortDateString() & " " & "00:00"
                    '.ExpectedStartDate = CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00"
                    '.ExpectedEndDate = .ActualStartDate.AddDays(IIf(txtVactiondays.Text < 1, 0, txtVactiondays.Text))

                    If .ActualEndDate.Year = 1 Then
                        .ActualEndDate = Nothing
                    End If

                    '.RemainingDays = lbRemainVal.Text
                    '.RemainingDays = lblTotalDays.Text - txtVactiondays.Text
                    'If lbRemainVal.Text < 0 Then
                    '    .RemainingDays = 0
                    'End If

                    '.vactiondays = txtVactiondays.Text
                    '.ConsumDays = txtVactiondays.Text
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

        'With ClsEmployees
        '    dteWebDateChooser1 = .SetHigriDate(dteWebDateChooser1)
        '    dteWebDateChooser2 = .SetHigriDate(dteWebDateChooser2)
        'End With
        Try
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            'intContractID = clsContract.ContractValidatoinId(ClsEmployees.ID, dteWebDateChooser1)
            'clsContract.Find(" ID =" & intContractID)
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
            'vacStartDate = CDate(CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00")
            'vacEndDate = CDate(CDate(dteWebDateChooser2).ToShortDateString() & " " & "00:00")
            'If (Not IsNothing(clsContract.EndDate)) And (Not IsNothing(vacEndDate)) Then
            '    If vacEndDate > clsContract.EndDate Then
            '        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Vacation end date not in contract period/نهاية الأجازة ليست فى فترة العقد"))
            '        Return False
            '    End If
            'End If
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
            'If (dteWebDateChooser2 <= dteWebDateChooser1 And dteWebDateChooser2.Date > Date.MinValue) Then
            '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "This End date must be more than Start date \n / تاريخ الرجوع يجب أن يكون أكبر من تاريخ البدء \n"))
            '    Return False
            'End If
            '-------------------------===============------------------------------------------
            Dim SDate As Date
            Dim EDate As Date
            Dim ASDate As Date
            Dim AEDate As Date
            'ASDate = CDate(CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00")
            'AEDate = CDate(CDate(dteWebDateChooser2).ToShortDateString() & " " & "00:00")
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
                    '.VacationTypeID = ClsVacationTypes.ID
                    '.ActualStartDate = CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00"
                    '.ActualEndDate = CDate(dteWebDateChooser2).ToShortDateString() & " " & "00:00"
                    '.ExpectedStartDate = CDate(dteWebDateChooser1).ToShortDateString() & " " & "00:00"
                    '.ExpectedEndDate = CDate(dteWebDateChooser2).ToShortDateString() & " " & "00:00"



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
                End If
                lbVactionID.Text = .ID.ToString()
                'WebDateChooser1.Value = Math.Round(.GetHigriDate(.ActualStartDate), 2)
                'WebDateChooser2.Value = Math.Round(.GetHigriDate(.ActualEndDate), 2)
                'If Not .ActualEndDate.Year = 1 Then
                '    WebDateChooser1.Enabled = False
                '    WebDateChooser2.Enabled = False
                '    'txtVactiondays.Enabled = False
                'Else
                '    WebDateChooser1.Enabled = True
                '    WebDateChooser2.Enabled = True
                '    'txtVactiondays.Enabled = True
                'End If
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
                'ClsVacType.GetDropDownList(DdlVacationType)
                ClsVacType.Find(" ID= " & IIf(IsNothing(.VacationTypeID), 0, .VacationTypeID))
                If ClsVacType.ID > 0 Then
                    item.Value = .VacationTypeID
                    item.Text = ObjNavigationHandler.SetLanguage(Page, ClsVacType.EngName & "/" & ClsVacType.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Page, ClsVacType.ArbName & "/" & ClsVacType.EngName)
                    End If
                    'If Not DdlVacationType.Items.Contains(item) Then
                    '    DdlVacationType.Items.Add(item)
                    '    DdlVacationType.SelectedValue = item.Value
                    'Else
                    '    DdlVacationType.SelectedValue = .VacationTypeID
                    'End If
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
                'If (bIsArabic Or row("Name").ToString.ToLower.IndexOf("arb") > -1) And (TypeOf (currCtrl) Is TextBox) Then
                '    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedulesForArabicText(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                'ElseIf (TypeOf (currCtrl) Is TextBox) Then
                '    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                'ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebSchedule.WebDateChooser) Then
                '    CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                'End If
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
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, Optional ByVal NullNode As Boolean = False, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        ClsEmployees = New Clshrs_Employees(Page)
        ClsEmployeesVacations.AddNotificationOnChange(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

        Try

            StrSelectCommand = "Select * From SS_VariousRequestTypes  "
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()


            Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = ObjNavigationHandler.SetLanguage(Page, "[Select Your Choice]/[ برجاء الاختيار ]")
                Item.Value = 0
                DdlValues.SelectedValue = Item.Value
                DdlValues.Items.Add(Item)



                For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = ObjDataRow("ArbName")
                Item.Value = ObjDataRow("Code")
                DdlValues.Items.Add(Item)
            Next

            If DdlValues.Items.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            Page.Session.Add("ErrorValue", ex)
            Page.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    Public Function GetEmpPosition()
        Dim SqlCommand As Data.SqlClient.SqlCommand
        Dim ClsEmployees As New Clshrs_Employees(Page)
        ClsEmployees.Find("Code='" & txtEmployee.Text & "'")

        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strselect As String
        strselect = "select * from hrs_Contracts where EmployeeID= " & ClsEmployees.ID & ""
        Dim ContractID As Integer = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strselect)
        Dim ClsContract As New Clshrs_Contracts(Page)
        ClsContract.Find("ID=" & ContractID & "")
        Dim ClsPositions As New Clshrs_Positions(Page)
        ClsPositions.Find("ID=" & ClsContract.PositionID & "")
        If ProfileCls.CurrentLanguage = "Ar" Then
            txtPosition.Text = ClsPositions.ArbName
        Else
            txtPosition.Text = ClsPositions.EngName
        End If

        txtPosition.Enabled = False

    End Function



End Class
