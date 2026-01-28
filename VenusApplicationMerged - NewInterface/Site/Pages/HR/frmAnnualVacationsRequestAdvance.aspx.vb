Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Venus.Shared.Web
Imports Infragistics.Documents.Excel.Serialization

Partial Class frmAnnualVacationsRequestAdvance
    Inherits MainPage
#Region "Public Decleration"
    Private ClsEmployeesVacations As Clshrs_EmployeesVacations
    Private ClsEmployees As Clshrs_Employees
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Const csOtherFields = 11
    Dim PrintRequestSerial As String
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim recordID As Integer
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Try
            ddlRequestDate.Enabled = False
            TxtAlternativeEmpName.Enabled = False
            lbTotalVal.Enabled = False
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
                    btnSearchCodeEmp.Visible = True
                    txtEmployee.Enabled = True
                Else
                    btnSearchCodeEmp.Visible = False
                    txtEmployee.Enabled = False

                End If
                Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
                'txtEmployee.Enabled = False
                ddlRequestDate.Value = ClsEmployeesVacations.GetHigriDate(DateTime.Now.ToString("dd/MM/yyyy"))
                WebDateChooser1.Value = ClsEmployeesVacations.GetHigriDate(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                ClsVacationTypes.GetDropDownList(DdlVacationType, True, "IsAnnual=1")
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
                ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'")
                ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
                csSearchID = ClsSearchs.ID
                lblLage.Text = ObjNavigationHandler.SetLanguage(Page, "0/1")
                Page.Session.Add("Lage", lblLage.Text)
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtAlternativeUser.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtAlternativeUser.ClientID & "'"
                btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                Dim UrlStringEmp = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmployee.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtEmployee.ClientID & "'"
                btnSearchCodeEmp.ClientSideEvents.Click = "OpenModal1(" & UrlStringEmp & ")"
                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
                Dim str As String = lbVactionID.Text

                If DdlVacationType.Items.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "You must enter at least one vacation type/يجب أن تدخل على الاقل نوع أجازة واحد"))
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
                ClsVacationTypes.Find("IsAnnual=1")
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
                WebDateChooser1.Value = ClsEmployeesVacations.GetHigriDate(DteStartDate)
                WebDateChooser2.Value = ClsEmployeesVacations.GetHigriDate(DteEndDate)
                GetEmpContractVac()
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
                If SavePart() Then
                    FillEmployeeVacations()
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                    ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">OpenPrintedScreen(" & PrintRequestSerial & ");</script>")

                End If
            Case "Save"
                If txtEmployee.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If SavePart() Then
                    FillEmployeeVacations()
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

            Dim dteWebDateChooser1 As Date = WebDateChooser1.Value
            With ClsEmployees
                dteWebDateChooser1 = ClsEmployees.SetHigriDate(dteWebDateChooser1)
            End With

            If Not String.IsNullOrEmpty(txtEmployee.Text) Then
                Dim intContractID As Integer = clsContract.ContractValidatoinId(ClsEmployees.ID, dteWebDateChooser1)
                clsContract.Find(" ID =" & intContractID)
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

                    'If hdnAnnualVacId.Value <> 0 And DdlVacationType.SelectedValue = hdnAnnualVacId.Value And ClsEmployees.ID > 0 Then
                    GetEmpContractVac()
                    'End If
                End If
            End If
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub txtAlternative_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAlternativeUser.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtAlternativeUser.Text) Then
                ClsEmployees = New Clshrs_Employees(Page)
                ClsEmployees.Find("Code='" & txtAlternativeUser.Text & "'")
                If ClsEmployees.ID > 0 Then

                    TxtAlternativeEmpName.Text = ClsEmployees.EnglishName
                Else
                    TxtAlternativeEmpName.Text = ""
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lbRemainVal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbRemainVal.TextChanged
        Try
            If CInt(lbRemainVal.Text) > 0 Then
                Dim Enddate As String
                Enddate = CDate(WebDateChooser1.Value).AddDays(CInt(lbRemainVal.Text))
                'Dim OfficialVac = GetOverlappingOfficialVacationDays(CDate(WebDateChooser1.Value), CDate(WebDateChooser2.Value))
                'If OfficialVac > 0 Then
                '    Enddate = CDate(Enddate).AddDays(OfficialVac)
                'End If
                WebDateChooser2.Value = Enddate
                WebDateChooser2_ValueChanged(Nothing, Nothing)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub WebDateChooser1_ValueChanged(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs) Handles WebDateChooser1.ValueChanged

        GetEmpContractVac()
    End Sub
    Protected Sub WebDateChooser2_ValueChanged(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs) Handles WebDateChooser2.ValueChanged, WebDateChooser1.ValueChanged
        If CDate(WebDateChooser2.Value) > CDate(WebDateChooser1.Value) Then
            Dim Diffe As Integer
            Diffe = (DateDiff(DateInterval.Day, WebDateChooser1.Value, WebDateChooser2.Value))
            Dim OfficialVac = GetOverlappingOfficialVacationDays(WebDateChooser1.Value, CDate(WebDateChooser2.Value).AddDays(-1))
            If OfficialVac > 0 Then
                Diffe = Diffe - OfficialVac
            End If
            lbRemainVal.Text = CStr(Diffe)
        End If
    End Sub

#End Region

#Region "Private Functions"
    Private Function SetNew() As Boolean
        Dim ClsEmployeesVacations As New Clshrs_EmployeesVacations(Page)
        Try
            DdlVacationType.Enabled = True
            WebDateChooser1.Value = ClsEmployeesVacations.GetHigriDate(Date.Now)
            WebDateChooser2.Value = ClsEmployeesVacations.GetHigriDate(Date.Now)
            WebDateChooser1.Enabled = True
            WebDateChooser2.Enabled = True
            SetTime()
            lblTotalDays.Text = 0
            LinkButton_OverDueMessage.Visible = False
            lbRemainVal.Text = 0
            lbVactionID.Text = ""
            txtEmployee.Focus()
            txtContactNo.Text = ""
            lbRemainVal.Text = 0
            lbRemainVal.CssClass = "noramlRemain"
            txtAlternativeUser.Text = ""
            TxtAlternativeEmpName.Text = ""
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
            Dim ClsEmployeeClass = New Clshrs_EmployeeClasses(Page)
            ClsEmployeeClass.Find("ID=" & ClsContracts.EmployeeClassID & "")
            If ClsEmployeeClass.AdvanceBalance Then
                Dim str = "SELECT   SUM( Remaining) FROM hrs_VacationsBalance where ISNULL(Posted,0)=0 and hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID=2 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "' and DueDate<='" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "')"
                Dim remain As Decimal = 0
                Dim result = Convert.ToString(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContracts.ConnectionString, Data.CommandType.Text, str))
                If Not String.IsNullOrWhiteSpace(result) Then
                    remain = CDec(result)
                End If
                str = "SELECT    SUM( Remaining) FROM hrs_VacationsBalance where ISNULL(Posted,0)=0 and hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID=1 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "'  and DueDate<='" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "')"
                Dim Balance As Decimal = 0
                result = Convert.ToString(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContracts.ConnectionString, Data.CommandType.Text, str))
                If Not String.IsNullOrWhiteSpace(result) Then
                    Balance = CDec(result)
                End If
                str = "SELECT    SUM( Remaining) FROM hrs_VacationsBalance where ISNULL(Posted,0)=0 and hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID=3 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "'  and DueDate<='" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "')"
                Dim remainComp As Decimal = 0
                Dim remComp = Convert.ToString(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContracts.ConnectionString, Data.CommandType.Text, str))
                If Not String.IsNullOrWhiteSpace(remComp) Then
                    remainComp = CDec(remComp)
                End If



                str = "SELECT   SUM(Balance) FROM hrs_VacationsBalance where ISNULL(Posted,0)=0 and hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID=2 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "' and DueDate<='" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "')"
                Dim TransferBalance As Decimal = 0
                Dim resultBalance = Convert.ToString(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContracts.ConnectionString, Data.CommandType.Text, str))
                If Not String.IsNullOrWhiteSpace(resultBalance) Then
                    TransferBalance = CDec(resultBalance)
                End If
                str = "SELECT    SUM(Balance) FROM hrs_VacationsBalance where ISNULL(Posted,0)=0 and hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID=1 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "'  and DueDate<='" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "')"
                Dim NewBalance As Decimal = 0
                resultBalance = Convert.ToString(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContracts.ConnectionString, Data.CommandType.Text, str))
                If Not String.IsNullOrWhiteSpace(resultBalance) Then
                    resultBalance = CDec(resultBalance)
                    NewBalance = CDec(resultBalance)
                End If

                str = "SELECT    SUM(Balance) FROM hrs_VacationsBalance where ISNULL(Posted,0)=0 and hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID=3 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "' and DueDate<='" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(WebDateChooser1.Value).ToString("yyyy-MM-dd") & "')"
                Dim CompensationBalance As Decimal = 0
                resultBalance = Convert.ToString(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContracts.ConnectionString, Data.CommandType.Text, str))
                If Not String.IsNullOrWhiteSpace(resultBalance) Then
                    CompensationBalance = CDec(resultBalance)
                End If

                dblVacationDays = Balance + remain + remainComp
                If ClsVacations.RoundAnnualVacBalance = True Then
                    lbTotalVal.Text = RoundVacationBalance(Math.Round(dblVacationDays, 2)).ToString()
                    lblNewRemain.Text = (RoundVacationBalance(Math.Round(Balance, 2))).ToString()
                    lblTransferBalance.Text = (RoundVacationBalance(Math.Round(TransferBalance, 2))).ToString()


                    lblNewBalance.Text = (RoundVacationBalance(Math.Round(NewBalance, 2))).ToString()
                    lblNewConsumed.Text = (RoundVacationBalance(Math.Round(NewBalance - Balance, 2))).ToString()

                    lblTransferRemain.Text = (RoundVacationBalance(Math.Round(remain, 2))).ToString()
                    lblTransferConsumed.Text = (RoundVacationBalance(Math.Round(TransferBalance - remain, 2))).ToString()

                    lblCompData.Text = (RoundVacationBalance(Math.Round(CompensationBalance, 2))).ToString()
                    lblCompRemainData.Text = (RoundVacationBalance(Math.Round(remainComp, 2))).ToString()
                    lblCompConsumedData.Text = (RoundVacationBalance(Math.Round(CompensationBalance - remainComp, 2))).ToString()
                Else
                    lbTotalVal.Text = Math.Round(dblVacationDays, 2)
                    lblNewRemain.Text = Math.Round(Balance, 2)
                    lblTransferBalance.Text = Math.Round(TransferBalance, 2)
                    lblNewBalance.Text = Math.Round(NewBalance, 2)
                    lblNewConsumed.Text = Math.Round(NewBalance - Balance, 2)

                    lblTransferRemain.Text = Math.Round(remain, 2)
                    lblTransferConsumed.Text = Math.Round(TransferBalance - remain, 2)

                    lblCompData.Text = Math.Round(CompensationBalance, 2)
                    lblCompRemainData.Text = Math.Round(remainComp, 2)
                    lblCompConsumedData.Text = Math.Round(CompensationBalance - remainComp, 2)

                End If
            End If

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

            If ProfileCls.CurrentLanguage = "Ar" Then
                lblNewYear.Text = "جديد" & " :"
                lblRemain.Text = "باقي" & " :"
                lblConsumed.Text = "م" & " :"

                lblTransferYear.Text = "مرحل" & " :"
                lblRemainT.Text = "باقي" & " :"
                lblConsumedT.Text = "م" & " :"

                lblCompLbl.Text = "تعويض" & " :"
                lblCompRemainLbl.Text = "باقي" & " :"
                lblCompConsumedLbl.Text = "م" & " :"

            Else
                lblNewYear.Text = "New" & " :"
                lblTransferYear.Text = "Transfered" & " :"

                lblRemain.Text = "Remain" & " :"
                lblConsumed.Text = "C" & " :"
                lblRemainT.Text = "Remain" & " :"
                lblConsumedT.Text = "C" & " :"

                lblCompLbl.Text = "Compensation" & " :"
                lblCompRemainLbl.Text = "Remain" & " :"
                lblCompConsumedLbl.Text = "C" & " :"
            End If

            If dblVacationDays < 0 Then dblVacationDays = 0

            If lbVactionID.Text = "" Then
                'lblTotalDays.Text = Math.Round(dblVacationDays, 2)
                ClsVacations.Find(" IsAnnual=1 ")
                If ClsVacations.RoundAnnualVacBalance = True Then
                    lbTotalVal.Text = RoundVacationBalance(Math.Round(dblVacationDays, 2)).ToString()
                    'lblNewBalance.Text = (RoundVacationBalance(Math.Round(Balance, 2))).ToString()
                    'lblTransferBalance.Text = (RoundVacationBalance(Math.Round(remain, 2))).ToString()
                Else
                    'lbRemainVal.Text = Math.Round(lblTotalDays.Text - txtVactiondays.Text, 2)
                    lbTotalVal.Text = Math.Round(dblVacationDays, 2)
                    'lblNewBalance.Text = Math.Round(Balance, 2).ToString()
                    'lblTransferBalance.Text = Math.Round(remain, 2).ToString()

                End If

                'txtVactiondays.Text = Math.Round(Diffe, 0)
                lbRemainVal.Text = Math.Round(Diffe)
                'lbRemainVal.Text = dblVacationDays - Diffe
            End If

            If (Convert.ToSingle(lbRemainVal.Text) < 0) Then
                lbRemainVal.CssClass = "worningRemain"
            Else
                lbRemainVal.CssClass = "noramlRemain"
            End If
            Dim vl As DateTime = WebDateChooser1.Value
            'WebDateChooser3.Value = vl.Date.AddDays(IIf(txtVactiondays.Text < 1, 0, txtVactiondays.Text))
            Return True
        Else
            'lblTotalDays.Text = 0
            'lbTotalVal.Text = 0
            ''txtVactiondays.Text = 0
            'lbRemainVal.Text = 0
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
                If Not IsNothing(ClsEmployees.NationalityID) Then
                    ClsNationality.Find("Id=" & ClsEmployees.NationalityID)
                    SetScreenInformation("N")

                    FillEmployeeVacations()
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
            If DdlVacationType.SelectedValue = 0 Then

                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Please Select Vacation Type / عفوا لابد من اختيار نوع الاجازة "))
                Exit Function

            End If
            If Not String.IsNullOrEmpty(WebDateChooser2.Value) Then
                If CDate(WebDateChooser1.Value) >= CDate(WebDateChooser2.Value) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Vaction return date must be greater than start date /تاريخ الرجوع من الاجازة يجب أن يكون اكبر من تاريخ البداية "))
                    Exit Function
                End If
            End If


            'Check off days between two vacation
            '1- Get last raturndate request





            If SaveAnnualVacationRequest() Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Request Saved Successfully /تم حفظ الطلب بنجاح "))
                SetNew()
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Return False
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function SaveAnnualVacationRequest() As Boolean
        Dim InsertCommand As String
        Try
            Dim ClsEmployees As New Clshrs_Employees(Page)
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            Dim ClsEmployeeVacation As New Clshrs_EmployeesVacations(Page)
            ClsEmployeeVacation.Find("EmployeeID=" & ClsEmployees.ID)

            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
            Dim ClsEmployeesVacations2 = New Clshrs_EmployeesVacations(Page)

            If Not String.IsNullOrEmpty(WebDateChooser2.Value) Then
                If CDate(WebDateChooser1.Value) >= CDate(WebDateChooser2.Value) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Vaction return date must be greater than start date /تاريخ الرجوع من الاجازة يجب أن يكون اكبر من تاريخ البداية "))
                    Exit Function
                End If
            End If

            If ClsEmployeesVacations2.GetEmployeeLastVacation(ClsEmployees.ID) Then
                If IsDBNull(ClsEmployeesVacations2.ActualEndDate) Or ClsEmployeesVacations2.ActualEndDate = "01/01/0001" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a vacation without return date /يوجد اجازة سابقة من غير تاريخ رجوع  "))
                    Exit Function
                End If
            End If
            Dim DteVacationDate As Date = WebDateChooser1.Value
            Dim dsPrevVac As Data.DataSet = ClsEmployeeVacation.GetEmployeeVacationPerYear(CDate(WebDateChooser1.Value).Year, ClsEmployees.ID, -1)
            If dsPrevVac.Tables(0).Rows.Count > 0 Then
                For index = 0 To dsPrevVac.Tables(0).Rows.Count - 1
                    If (dsPrevVac.Tables(0).Rows(index).Item("ActualEndDate") >= WebDateChooser1.Value And dsPrevVac.Tables(0).Rows(index).Item("ActualStartDate") <= WebDateChooser1.Value) Or (dsPrevVac.Tables(0).Rows(index).Item("ActualEndDate") >= WebDateChooser2.Value And dsPrevVac.Tables(0).Rows(index).Item("ActualStartDate") <= WebDateChooser2.Value) Or (WebDateChooser2.Value >= dsPrevVac.Tables(0).Rows(index).Item("ActualStartDate") And WebDateChooser1.Value <= dsPrevVac.Tables(0).Rows(index).Item("ActualStartDate")) Or (WebDateChooser2.Value >= dsPrevVac.Tables(0).Rows(index).Item("ActualEndDate") And WebDateChooser1.Value <= dsPrevVac.Tables(0).Rows(index).Item("ActualEndDate")) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a vacation in this period / يوجد اجازة مسجلة بالفعل فى هذه الفترة  "))
                        Exit Function
                    End If
                Next
            End If
            'If ClsEmployeesVacations2.ActualEndDate > WebDateChooser1.Value Then
            '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a vacation with end date greater than the inserted date /يوجد اجازة سابقة بتاريخ رجوع أكبر من التاريخ المدخل  "))
            '    Exit Function
            'End If

            Dim SqlCommand As Data.SqlClient.SqlCommand
            'Dim ClsEmployees As New Clshrs_Employees(Page)
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            Dim ConnectionString As String
            ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()

            Dim strselect As String
            strselect = "select * from SS_Configuration where FormCode='SS_0011'and Rank=1"
            Dim DsFirstLevel As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
            If DsFirstLevel.Tables(0).Rows.Count > 0 Then

            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry Can not proceed your request because there are no levels for this request ...Please contact system admin  / عفوا لايمكن تسجيل الطلب لعدم وجود مراحل ... يرجي مراجعة مدير النظام"))
                Exit Function

            End If
            Dim VBalance As Decimal = Decimal.Parse(lbTotalVal.Text)
            Dim NoOfDays As Decimal = Decimal.Parse(lbRemainVal.Text)
            If NoOfDays > VBalance Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry Vacation Balance Should be More Than or Equal Required Vacation Days! / عفوا لابد ان يكون رصيد الاجازة اكبر من او يساوي عدد الايام المطلوبة"))
                Exit Function
            End If

            'strselect = "select SS_VacationRequest.ID as noActionsId from SS_VacationRequest inner join SS_RequestActions on SS_VacationRequest.VacationType=SS_RequestActions.FormCode and SS_VacationRequest.ID=SS_RequestActions.RequestSerial where SS_RequestActions.ActionID is null and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013' or SS_RequestActions.FormCode='SS_0018') and SS_VacationRequest.EmployeeID=" & ClsEmployees.ID
            'Dim DsRequestNoAction As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
            'If DsRequestNoAction.Tables(0).Rows.Count > 0 Then
            '    For i = 0 To DsRequestNoAction.Tables(0).Rows.Count - 1
            '        Dim str = " select count(Actionserial) as noActionCount from SS_RequestActions  where RequestSerial=" & DsRequestNoAction.Tables(0).Rows(i).Item("noActionsId") & " and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013' or SS_RequestActions.FormCode='SS_0018') and ActionID =4 "
            '        Dim canceled As Integer
            '        canceled = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str)
            '        If canceled < 1 Then
            '            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry there is another Vacation request stil need action! / عفوا لديك طلب اجازة اخر ينتظر الموافقه"))
            '            Return False
            '            Exit Function
            '        End If
            '    Next
            'End If

            If Not CheckOffdaysbetweenvacationosrequests() Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "It is not allowed to request two consecutive leaves separated by a weekend. Please adjust the dates according to the policy  /غير مسموح بتسجيل إجازتين متتاليتين بينهما عطلة نهاية الأسبوع. يرجى تعديل التواريخ وفقًا لسياسة النظام "))
                Exit Function
            End If

            If Not CheckOfficialvacationbetweenvacationosrequests() Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "It is not allowed to request two consecutive leaves separated by an officialvacation. Please adjust the dates according to the policy  /غير مسموح بتسجيل إجازتين متتاليتين بينهما اجازة رسمية. يرجى تعديل التواريخ وفقًا لسياسة النظام "))
                Exit Function
            End If
            If CheckRequestsOverlapping() Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "You cannot submit this vacation request because it overlaps with a previous Request  /عفوا لايمكن تسجيل طلبك لانه متداخل مع طلب اجازة اخري "))
                Exit Function
            End If
            If CheckVacationsOverlapping() Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "You cannot submit this vacation request because it overlaps with a previous vacation  /عفوا لايمكن تسجيل طلبك لانه متداخل مع  اجازة اخري "))
                Exit Function
            End If

            'ClsEmployees.Find("Code='" & txtEmployee.Text & "'")


            '''''''''''''''''''''''''''''عدد المرات
            Dim strNoOfTimes As String
            strNoOfTimes = "select NoOfTimes from SS_RequestTypes where RequestCode='SS_0011'"
            Dim NoOfTimes As Integer
            NoOfTimes = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, strNoOfTimes)
            ''''''''''''''''''''''''''''''عدد الشهور
            Dim strTimesPeriodPerMonth As String
            strTimesPeriodPerMonth = "select TimesPeriodPerMonth from SS_RequestTypes where RequestCode='SS_0011'"
            Dim TimesPeriodPerMonth As Integer
            TimesPeriodPerMonth = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, strTimesPeriodPerMonth)
            ''''''''''''''''''''
            If TimesPeriodPerMonth > 0 And NoOfTimes > 0 Then


                Dim strCOuntNoOfimes As String
                strCOuntNoOfimes = "Select count(id) from SS_VacationRequest where VacationType='SS_0013' and RequestDate<=GETDATE() and RequestDate>= DATEADD(month, -" & TimesPeriodPerMonth & ", getdate()) and EmployeeID='" & ClsEmployees.ID & "'"
                Dim COuntNoOfimes As Integer
                COuntNoOfimes = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, strCOuntNoOfimes)
                If COuntNoOfimes >= NoOfTimes Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Sorry...You Can't Exceed the allowed No Of Requests /عفوا لايمكن تجاوز عدد الطلبات المسموح به "))
                    Exit Function
                End If
            End If


            ''''''''''''''''''''''''''''''''''''''''''''''

            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            Dim AlternativeEmpID As String = ""
            If Not String.IsNullOrEmpty(txtAlternativeUser.Text) Then
                Dim ClsEmployees2 As New Clshrs_Employees(Page)
                ClsEmployees2.Find("Code='" & txtAlternativeUser.Text & "'")
                AlternativeEmpID = ClsEmployees2.ID
            End If

            WebHandler.GetCookies(Page, "UserID", User)
            InsertCommand = "Insert Into SS_VacationRequest  Values(''," & ClsEmployees.ID & "," & DdlVacationType.SelectedItem.Value & ",'SS_0011',convert(datetime, '" & ddlRequestDate.Value & "',103),convert(datetime, '" & WebDateChooser1.Value & "',103) ,convert(datetime, '" & WebDateChooser2.Value & "',103)," & lbTotalVal.Text & " , " & lbRemainVal.Text & " ,'" & txtContactNo.Text & "', '" & AlternativeEmpID & "','" & TxtRemarks.Text & "'," & User & " ,convert(datetime,  '" & ClsEmployeesVacations.GetHigriDate(Date.Now) & "' ,103),'" & txtRequesterUser.Text & "',3)"
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
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)

            Dim strselect As String
            strselect = "select * from SS_Configuration where FormCode='SS_0011'and Rank=1"
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
                        strMaxRequestSerial = "select isnull(Max (ID),1) from SS_VacationRequest where EmployeeID=" & ClsEmployees.ID & " and VacationType='SS_0011' "
                        Dim MaxSerial As Integer
                        SqlCommand = New SqlClient.SqlCommand
                        SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
                        SqlCommand.CommandType = CommandType.Text
                        SqlCommand.CommandText = strMaxRequestSerial
                        SqlCommand.Connection.Open()
                        MaxSerial = CInt(SqlCommand.ExecuteScalar())
                        SqlCommand.Connection.Close()



                        strinsert = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen,ConfigID)  values(" & MaxSerial & " , " & DirectManagerID & ",'SS_0011'," & ClsEmployees.ID & ",0," & Row("ID") & ")"
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
                            strMaxRequestSerial = "select isnull(Max (ID),1) from SS_VacationRequest where EmployeeID=" & ClsEmployees.ID & " and VacationType='SS_0011' "
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
                                strinsert = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen,ConfigID)  values(" & MaxSerial & " , " & RW("EmployeeID") & ",'SS_0011'," & ClsEmployees.ID & ",0," & Row("ID") & ")"
                                SqlCommand = New SqlClient.SqlCommand
                                SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
                                SqlCommand.CommandType = CommandType.Text
                                SqlCommand.CommandText = strinsert
                                SqlCommand.Connection.Open()
                                SqlCommand.ExecuteNonQuery()
                                SqlCommand.Connection.Close()

                            Next
                        Else
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
                        strinsert = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen,ConfigID)  values(" & MaxSerial & " , " & Row("EmployeeID") & ",'SS_0011'," & ClsEmployees.ID & ",0," & Row("ID") & ")"
                        SqlCommand = New SqlClient.SqlCommand
                        SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
                        SqlCommand.CommandType = CommandType.Text
                        SqlCommand.CommandText = strinsert
                        SqlCommand.Connection.Open()
                        SqlCommand.ExecuteNonQuery()
                        SqlCommand.Connection.Close()
                    End If
                Next
                ClsEmployees.SendEmail("SSRequests", Me.Page, 1, "SS_VacationRequest", PrintRequestSerial)
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry Can not proceed your request because there are no levels for this request ...Please contact system admin  / عفوا لايمكن تسجيل الطلب لعدم وجود مراحل ... يرجي مراجعة مدير النظام"))

            End If


        Catch ex As Exception

        End Try
    End Function

    Public Function CheckDelegationSchedule(EmpID As Integer) As Integer
        Dim StrGetDelegation As String
        StrGetDelegation = "select isnull(DelegatedEmployeeID,0) as DelegatedEmployeeID from SS_DelegationSChedule inner join SS_DelegationSCheduleRequests on SS_DelegationSCheduleRequests.ScheduleId=SS_DelegationSChedule.ID where DelegatorEmployeeID =" & EmpID & " and GetDate() >=FromDate and GetDate() <= Todate and SS_DelegationSCheduleRequests.RequestTypeId='SS_0011' and  (SS_DelegationSChedule.IsCanceled=0 OR SS_DelegationSChedule.IsCanceled is null Or SS_DelegationSChedule.CancelDate> GetDate() )union all select 0 as DelegatedEmployeeID "
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
        RequestCode = "V_ADM_ " & MaxSerial & ""
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
                    '.ExpectedEndDate = .ActualStartDate.AddDays(IIf(txtVactiondays.Text < 1, 0, txtVactiondays.Text))

                    If .ActualEndDate.Year = 1 Then
                        .ActualEndDate = Nothing
                    End If
                    .TotalDays = lbTotalVal.Text
                    '.RemainingDays = lbRemainVal.Text
                    '.RemainingDays = lblTotalDays.Text - txtVactiondays.Text
                    If lbRemainVal.Text < 0 Then
                        .RemainingDays = 0
                    End If

                    '.vactiondays = txtVactiondays.Text
                    .TotalBalance = lblTotalDays.Text
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
                End If
                lbVactionID.Text = .ID.ToString()
                WebDateChooser1.Value = Math.Round(.GetHigriDate(.ActualStartDate), 2)
                WebDateChooser2.Value = Math.Round(.GetHigriDate(.ActualEndDate), 2)
                If Not .ActualEndDate.Year = 1 Then
                    WebDateChooser1.Enabled = False
                    WebDateChooser2.Enabled = False
                    'txtVactiondays.Enabled = False
                Else
                    WebDateChooser1.Enabled = True
                    WebDateChooser2.Enabled = True
                    'txtVactiondays.Enabled = True
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

    Private Sub FillEmployeeVacations()
        Try
            'Dim User As String = String.Empty
            'Dim WebHandler As New Venus.Shared.Web.WebHandler
            'WebHandler.GetCookies(Page, "UserID", User)
            'Dim _sys_User As New Clssys_Users(Page)
            '_sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_Employees(Me)
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
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
            Dim EmpName As String
            Dim RequestName As String
            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " EmployeeArbName "
                RequestName = "RequestArbName"
            Else
                EmpName = "EmployeeEngName"
                RequestName = "RequestEngName"
            End If



            Dim str1 As String = "select ID,VacationType, RequestSerial ,EmployeeID ," & EmpName & " as EmployeeName,RequestDate , " & RequestName & " as RequestType, FormCode from SS_VFollowup where  EmployeeID= " & ClsEmployees.ID & "  and FormCode='SS_0011' Order By RequestDate desc"





            If str1 <> "" Then
                command = New Data.SqlClient.SqlCommand(str1, connection)
                adapter.SelectCommand = command
                adapter.Fill(DS1, "Table1")

                connection.Close()
                uwgEmployeeVacations.DataSource = Nothing
                uwgEmployeeVacations.DataBind()

                uwgEmployeeVacations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
                uwgEmployeeVacations.DataSource = DS1
                uwgEmployeeVacations.DataBind()

                If uwgEmployeeVacations.Rows.Count > 0 Then

                    FillRowColors()

                End If
            Else
                uwgEmployeeVacations.DataSource = Nothing
                uwgEmployeeVacations.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillRowColors()
        'Dim User As String = String.Empty
        'Dim WebHandler As New Venus.Shared.Web.WebHandler
        'WebHandler.GetCookies(Page, "UserID", User)
        'Dim _sys_User As New Clssys_Users(Page)
        '_sys_User.Find("ID = '" & User & "'")
        Dim ClsEmployees As New Clshrs_Employees(Me)
        ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
        Dim connetionString As String
        Dim connection As Data.SqlClient.SqlConnection
        Dim command As Data.SqlClient.SqlCommand
        Dim adapter As New Data.SqlClient.SqlDataAdapter
        connetionString = ClsEmployees.ConnectionString
        connection = New Data.SqlClient.SqlConnection(connetionString)

        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeeVacations.Rows
            Dim RequestSerial As String = row.Cells().FromKey("RequestSerial").Value
            Dim FormCode As String = row.Cells().FromKey("FormCode").Value
            Dim ID As Integer = row.Cells().FromKey("ID").Value


            Dim DS3 As New Data.DataSet()
            DS3.Clear()
            For x As Integer = 0 To DS3.Tables.Count - 1
                DS3.Tables(x).Constraints.Clear()
            Next
            DS3.Relations.Clear()
            DS3.Tables.Clear()
            Dim command3 As Data.SqlClient.SqlCommand
            Dim adapter3 As New Data.SqlClient.SqlDataAdapter
            connection = New Data.SqlClient.SqlConnection(connetionString)
            Dim str3 As String
            str3 = "Select * from SS_RequestActions where RequestSerial=" & ID & " and SS_RequestActions.EmployeeID=" & ClsEmployees.ID & " And FormCode='" & FormCode & "' and (IsHidden<>1 or IsHidden is null) and ActionID Is Null"

            command3 = New Data.SqlClient.SqlCommand(str3, connection)
            adapter3.SelectCommand = command3
            adapter3.Fill(DS3, "Table1")
            If DS3.Tables(0).Rows.Count = 0 Then
                'Accepted Request
                row.Style.BackColor = System.Drawing.Color.FromArgb(223, 240, 216)
            End If

            Dim DS4 As New Data.DataSet()
            DS4.Clear()
            For x As Integer = 0 To DS4.Tables.Count - 1
                DS4.Tables(x).Constraints.Clear()
            Next
            DS4.Relations.Clear()
            DS4.Tables.Clear()
            Dim command4 As Data.SqlClient.SqlCommand
            Dim adapter4 As New Data.SqlClient.SqlDataAdapter
            connection = New Data.SqlClient.SqlConnection(connetionString)
            Dim str4 As String
            str4 = "Select * from SS_RequestActions where RequestSerial=" & ID & " and SS_RequestActions.EmployeeID=" & ClsEmployees.ID & " And FormCode='" & FormCode & "' and (IsHidden<>1 or IsHidden is null) and ActionID=2"

            command4 = New Data.SqlClient.SqlCommand(str4, connection)
            adapter4.SelectCommand = command4
            adapter4.Fill(DS4, "Table1")
            If DS4.Tables(0).Rows.Count > 0 Then
                'Rejrcted Request
                row.Style.BackColor = System.Drawing.Color.FromArgb(242, 222, 222)

            End If
            Dim DS5 As New Data.DataSet()
            DS5.Clear()
            For x As Integer = 0 To DS5.Tables.Count - 1
                DS5.Tables(x).Constraints.Clear()
            Next
            DS5.Relations.Clear()
            DS5.Tables.Clear()
            Dim command5 As Data.SqlClient.SqlCommand
            Dim adapter5 As New Data.SqlClient.SqlDataAdapter
            connection = New Data.SqlClient.SqlConnection(connetionString)
            Dim str5 As String
            str5 = "Select * from SS_RequestActions where RequestSerial=" & ID & " and SS_RequestActions.EmployeeID=" & ClsEmployees.ID & " And FormCode='" & FormCode & "' And (IsHidden<>1 or IsHidden is null) and ActionID Is Null"

            command5 = New Data.SqlClient.SqlCommand(str5, connection)
            adapter5.SelectCommand = command5
            adapter5.Fill(DS5, "Table1")
            If DS5.Tables(0).Rows.Count > 0 Then
                'row.Style.BackColor = System.Drawing.Color.FromArgb(254, 216, 93)
                'Action token but not yet finish
                row.Style.BackColor = System.Drawing.Color.FromArgb(217, 237, 247)


            End If

            Dim DS6 As New Data.DataSet()
            DS6.Clear()
            For x As Integer = 0 To DS6.Tables.Count - 1
                DS6.Tables(x).Constraints.Clear()
            Next
            DS6.Relations.Clear()
            DS6.Tables.Clear()
            Dim command6 As Data.SqlClient.SqlCommand
            Dim adapter6 As New Data.SqlClient.SqlDataAdapter
            connection = New Data.SqlClient.SqlConnection(connetionString)
            Dim str6 As String
            str6 = "select hrs_Employees.ArbName4S +' ' +FatherArbName+' '+ FamilyArbName as EmployeeName,( case when SS_UserActions.ActionAraName is not null then SS_UserActions.ActionAraName else 'Pending ...' end) As Action,ConfirmedNoOfdays ,convert(varchar, ActionDate,101) as ActionDate,ActionRemarks  from  SS_RequestActions  join hrs_Employees on hrs_Employees.ID= SS_RequestActions.SS_EmployeeID left join SS_UserActions on SS_RequestActions.ActionID=SS_UserActions.ID where RequestSerial=" & ID & " And FormCode='" & FormCode & "' and (IsHidden<>1 or IsHidden is null) and  SS_RequestActions.EmployeeID=" & ClsEmployees.ID & " "

            command6 = New Data.SqlClient.SqlCommand(str6, connection)
            adapter6.SelectCommand = command6
            adapter6.Fill(DS6, "Table1")
            If DS6.Tables(0).Rows.Count = 1 Then
                For Each row6 In DS6.Tables(0).Rows
                    If CStr(row6("Action")) = "Pending ..." Then
                        'No Action yet
                        row.Style.BackColor = System.Drawing.Color.FromArgb(252, 248, 227)

                    End If
                Next


            End If


        Next

    End Sub
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




    'Protected Sub WebDateChooser3_ValueChanged(sender As Object, e As Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs) Handles WebDateChooser3.ValueChanged
    '    If lbVactionID.Text = "" Then
    '        txtVactiondays.Text = CDate(WebDateChooser3.Value).Subtract(CDate(WebDateChooser1.Value)).Days
    '        txtVactiondays_ValueChange(Nothing, Nothing)
    '    End If

    'End Sub

    Public Function RoundVacationBalance(Balance As Decimal) As String
        Dim OldBalance As Decimal = Balance
        Dim IntBalance As Integer
        Dim DecimalBalance As Decimal

        IntBalance = Decimal.Truncate(OldBalance)
        DecimalBalance = OldBalance - IntBalance
        If DecimalBalance <= 0.499 Then
            DecimalBalance = 0
        Else
            DecimalBalance = 1

        End If
        OldBalance = IntBalance + DecimalBalance
        Return OldBalance.ToString()
    End Function
    Private Sub LinkButton_Remarks_Load(sender As Object, e As EventArgs) Handles LinkButton_Remarks.Load

    End Sub
    Public Function CheckOfficialvacationbetweenvacationosrequests() As Boolean
        Dim ClsEmployees As New Clshrs_Employees(Page)
        ClsEmployees.Find("Code='" & txtEmployee.Text & "'")

        ' Retrieve last vacation end date
        Dim LastReturnDate As DateTime
        Dim strSelect As String = "SELECT MAX(EndDate) FROM SS_VacationRequest " &
                              "WHERE VacationTypeID=1 AND EmployeeID=" & ClsEmployees.ID &
                              " AND ID NOT IN (SELECT RequestSerial FROM SS_RequestActions " &
                              "WHERE FormCode='SS_0011' AND (ActionID=4 OR ActionID=2))"

        Dim result As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strSelect)

        If result Is DBNull.Value OrElse result Is Nothing Then
            Return True
        Else
            LastReturnDate = Convert.ToDateTime(result)
        End If

        Dim strSelectOfficialVacs As String = "SELECT FromDate, ToDate FROM Hrs_OfficialVacations"
        Dim dsOfficialVacations As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, strSelectOfficialVacs)

        If dsOfficialVacations.Tables(0).Rows.Count = 0 Then
            Return True
        End If

        Dim NewStartDate As DateTime = WebDateChooser1.Value
        Dim currentDate As DateTime = LastReturnDate

        While currentDate < NewStartDate
            For Each row As DataRow In dsOfficialVacations.Tables(0).Rows
                Dim offStart As DateTime = Convert.ToDateTime(row("FromDate"))
                Dim offEnd As DateTime = Convert.ToDateTime(row("ToDate"))

                If currentDate >= offStart AndAlso currentDate <= offEnd AndAlso NewStartDate = offEnd.AddDays(1) AndAlso result = offStart.AddDays(-1) Then
                    Return False
                End If
            Next
            currentDate = currentDate.AddDays(1)
        End While

        Return True
    End Function
    Public Function CheckOffdaysbetweenvacationosrequests() As Boolean

        Dim ClsEmployees As New Clshrs_Employees(Page)
        ClsEmployees.Find("Code='" & txtEmployee.Text & "'")

        ' Retrieve last vacation end date
        Dim LastReturnDate As DateTime
        Dim strSelect As String = "SELECT MAX(EndDate) FROM SS_VacationRequest " &
                              "WHERE VacationTypeID=1 AND EmployeeID=" & ClsEmployees.ID &
                              " AND ID NOT IN (SELECT RequestSerial FROM SS_RequestActions " &
                              "WHERE FormCode='SS_0011' AND (ActionID=4 OR ActionID=2))"

        Dim result As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strSelect)

        ' If no previous vacation exists, allow the new vacation
        If result Is DBNull.Value OrElse result Is Nothing Then
            Return True
        Else
            LastReturnDate = Convert.ToDateTime(result).AddDays(-1)
        End If

        ' Get the new vacation start date
        Dim NewStartDate As DateTime = WebDateChooser1.Value

        ' Check if the new vacation request is within 3 days of the last return date
        If DateDiff(DateInterval.Day, LastReturnDate, NewStartDate) <= 3 Then
            ' Retrieve employee's day-off schedule
            Dim strSelectDayOff As String = "SELECT DayNo FROM dbo.Att_AttendShiftDays " &
                                        "WHERE AttendShiftID IN (" &
                                        "    SELECT sh.ID FROM dbo.Att_AttendShifts AS sh " &
                                        "    INNER JOIN dbo.Att_AttendAppointment AS P ON sh.ID = P.AttendaceShiftID " &
                                        "    INNER JOIN dbo.Att_AttendAppointmentMembers AS M ON P.ID = M.AppointID " &
                                        "    WHERE M.EmployeeID = " & ClsEmployees.ID & " ) " &
                                        "AND IsDayOff = 1 And CancelDate is null"

            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, strSelectDayOff)

            ' If dataset has no records, return true (no day offs configured)
            If ds.Tables(0).Rows.Count = 0 Then
                Return True
            End If

            ' Loop through each date between the last vacation end date and the new vacation start date
            Dim currentDate As DateTime = LastReturnDate.AddDays(1) ' Start checking from the next day
            While currentDate < NewStartDate
                Dim currentDayNumber As Integer = RetDayNumber(currentDate) ' Get the day number of this date

                ' Check if this day number is a "Day Off" in the dataset
                For Each row As DataRow In ds.Tables(0).Rows
                    If currentDayNumber = Convert.ToInt32(row("DayNo")) Then
                        Return False ' Found a day off, reject vacation request
                    End If
                Next

                ' Move to the next day
                currentDate = currentDate.AddDays(1)
            End While

            Return True ' No day offs found between the vacations, allow the request
        Else
            Return True ' More than 3 days gap, allow vacation
        End If




    End Function
    Private Function RetDayNumber(ByVal TrnsDate As DateTime) As Integer
        Dim DayNumber As Integer = 0
        Dim Dayidx As Integer = TrnsDate.DayOfWeek
        If Dayidx = 0 Then
            DayNumber = 2
        ElseIf Dayidx = 1 Then
            DayNumber = 3
        ElseIf Dayidx = 2 Then
            DayNumber = 4
        ElseIf Dayidx = 3 Then
            DayNumber = 5
        ElseIf Dayidx = 4 Then
            DayNumber = 6
        ElseIf Dayidx = 5 Then
            DayNumber = 7
        ElseIf Dayidx = 6 Then
            DayNumber = 1
        End If
        Return DayNumber
    End Function

    Public Function CheckRequestsOverlapping() As Boolean
        ClsEmployees = New Clshrs_Employees(Page)
        ClsEmployees.Find("Code='" & txtEmployee.Text & "'")


        Dim strpreviousPeriods As String = "SELECT * FROM     SS_VacationRequest where EmployeeID= " & ClsEmployees.ID & " and id not in (select RequestSerial from SS_RequestActions where (FormCode='SS_0011' OR FormCode='SS_0013' OR FormCode='SS_0012' OR FormCode='SS_0018' )and (ActionID=4 or ActionID=2)And IsHidden Is null) "
        Dim previousPeriods As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, strpreviousPeriods)

        Dim newStart As DateTime = Convert.ToDateTime(WebDateChooser1.Value)
        Dim newEnd As DateTime = Convert.ToDateTime(WebDateChooser2.Value)
        For Each row As DataRow In previousPeriods.Tables(0).Rows
            Dim periodStart As DateTime = Convert.ToDateTime(row("StartDate"))
            Dim periodEnd As DateTime = Convert.ToDateTime(row("EndDate"))

            ' التحقق من وجود أي تداخل
            If (newStart <= periodEnd.AddDays(-1) AndAlso newEnd.AddDays(-1) >= periodStart) Then
                Return True ' يوجد تداخل
            End If
        Next

        Return False ' لا يوجد تداخل
    End Function
    Public Function CheckVacationsOverlapping() As Boolean
        Dim strpreviousPeriods As String = "select * from hrs_EmployeesVacations where canceldate is null And EmployeeID= " & ClsEmployees.ID & " "
        Dim previousPeriods As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, strpreviousPeriods)

        Dim newStart As DateTime = Convert.ToDateTime(WebDateChooser1.Value)
        Dim newEnd As DateTime = Convert.ToDateTime(WebDateChooser2.Value)
        For Each row As DataRow In previousPeriods.Tables(0).Rows
            Dim periodStart As DateTime = Convert.ToDateTime(row("ActualStartDate"))
            Dim periodEnd As DateTime = Convert.ToDateTime(row("ActualEndDate"))

            ' التحقق من وجود أي تداخل
            If (newStart <= periodEnd.AddDays(-1) AndAlso newEnd.AddDays(-1) >= periodStart) Then
                Return True ' يوجد تداخل
            End If
        Next

        Return False ' لا يوجد تداخل
    End Function

    Public Function GetOverlappingOfficialVacationDays(startDate As Date, endDate As Date) As Integer
        Dim totalDays As Integer = 0
        ClsEmployees = New Clshrs_Employees(Page)
        Dim OfficialVacsSTR As String = "set dateformat  dmy;SELECT FromDate, ToDate FROM hrs_OfficialVacations WHERE (FromDate <= '" & endDate & "' AND ToDate >= '" & startDate & "')"
        Dim OfficiaVacationsDS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, OfficialVacsSTR)
        If OfficiaVacationsDS.Tables(0).Rows.Count > 0 Then
            For Each Row In OfficiaVacationsDS.Tables(0).Rows

                Dim overlapStart As Date = If(startDate > Row("FromDate"), startDate, Row("FromDate"))
                Dim overlapEnd As Date = If(endDate < Row("ToDate"), endDate, Row("ToDate"))
                totalDays += (overlapEnd - overlapStart).Days + 1
            Next
        End If

        Return totalDays
    End Function


End Class
