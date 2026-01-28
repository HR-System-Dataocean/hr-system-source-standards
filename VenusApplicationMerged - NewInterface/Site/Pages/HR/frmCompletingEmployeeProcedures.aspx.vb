Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Venus.Shared.Web
Imports System.Security.Cryptography
Imports System.Collections.ObjectModel
Imports System.Activities.Expressions

Partial Class frmCompletingEmployeeProcedures
    Inherits MainPage
#Region "Public Decleration"
    Private ClsEmployeesVacations As Clshrs_EmployeesVacations
    Private ClsEmployees As Clshrs_NewEmployee
    Dim ClsUsers As Clssys_Users
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Const csOtherFields = 11
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim recordID As Integer
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)

        If Not IsPostBack Then


            Try

                Dim WebHandler As New Venus.Shared.Web.WebHandler
                Dim User As String = String.Empty
                ClsEmployees = New Clshrs_NewEmployee(Page)
                WebHandler.GetCookies(Page, "UserID", User)
                Dim _sys_User As New Clssys_Users(Page)
                _sys_User.Find("ID = '" & User & "'")
                ddlFromDate.Value = ClsEmployeesVacations.GetHigriDate("01/01/" & DateTime.Now.Year.ToString())
                ddlToDate.Value = ClsEmployeesVacations.GetHigriDate(DateTime.Now.ToString("dd/MM/yyyy"))

                Dim filter As String
                filter = " and RegisterDate>='" & Convert.ToDateTime(ddlFromDate.Value.ToString()).ToString("yyyy-MM-dd") & "' and cast( RegisterDate as date)<='" & Convert.ToDateTime(ddlToDate.Value).ToString("yyyy-MM-dd") & "'"
                filter += " and isnull(IsTransfered,0)=0 and CancelDate is null"
                GetAllEmployeeRequests(filter)
                Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
                ClsEmployeesVacations.AddNotificationOnChange(Page)
                'Dim csSearchID As Integer
                'Dim ClsLevels As New Clshrs_LevelTypes(Page)
                'Dim ClsDataHandler As New Venus.Shared.DataHandler
                'Dim StrSerial As String = String.Empty
                'Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
                'Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)
                'Dim ClsObjects As New Clssys_Objects(Page)
                'Dim ClsSearchs As New Clssys_Searchs(Page)
                'Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
                'lblLage.Text = ObjNavigationHandler.SetLanguage(Page, "0/1")
                'Page.Session.Add("Lage", lblLage.Text)
                'Dim IntDimension As Integer = 510
                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
                Dim ClsObjects As New Clssys_Objects(Page)
                Dim ClsSearchs As New Clssys_Searchs(Page)
                Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language = ""javascript"">IntializeDataChanged()</script>")
                ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'")
                ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
                Dim csSearchID As Integer
                csSearchID = ClsSearchs.ID
                Dim IntDimension As Integer = 510
                'Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmpCode.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtEmpCode.ClientID & "'"
                'btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
            Catch ex As Exception
                mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
                Page.Session.Add("ErrorValue", ex)
                mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
                Page.Response.Redirect("ErrorPage.aspx")
            End Try

        Else

            Dim eventTarget As String = Request("__EVENTTARGET")
            Dim eventArgument As String = Request("__EVENTARGUMENT")

            If Request("__EVENTTARGET") = Me.UniqueID Then
                HandleGridButtonClick(eventArgument)
            End If

        End If



    End Sub
    Private Sub HandleGridButtonClick(eventArgument As String)
        Dim parts As String() = eventArgument.Split("|"c)
        Dim id As String = parts(0)
        Dim formCode As String = If(parts.Length > 1, parts(1), "")

        ' 👉 Do your logic here
        ' For example, open modal, load data, etc.
        ' This example just shows a message:
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Dim ObjNav As New Venus.Shared.Web.NavigationHandler(ClsEmployeesVacations.ConnectionString)

        Dim sqlstr = "UPDATE [dbo].[Hrs_NewEmployee] SET [CancelDate] = Getdate() WHERE ID=" & id
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeesVacations.ConnectionString, CommandType.Text, sqlstr)

        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNav.SetLanguage(Page, "employee Canceled !/!تم الغاء الموظف"))
        Dim filter As String
        filter = " and RegisterDate>='" & Convert.ToDateTime(ddlFromDate.Value.ToString()).ToString("yyyy-MM-dd") & "' and cast( RegisterDate as date)<='" & Convert.ToDateTime(ddlToDate.Value).ToString("yyyy-MM-dd") & "'"


        filter += " and isnull(IsTransfered,0)=0 and CancelDate is null"
        GetAllEmployeeRequests(filter)
    End Sub

    'Protected Sub txtEmpCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmpCode.TextChanged
    '    Try
    '        If Not String.IsNullOrEmpty(txtEmpCode.Text) Then
    '            ClsEmployees = New Clshrs_NewEmployee(Page)
    '            Dim EmpName As String
    '            If ProfileCls.CurrentLanguage = "Ar" Then
    '                EmpName = " isnull( hrs_NewEmployee.arbname ,' ')+' '+ isnull(hrs_NewEmployee.FatherArbName, ' ')+' '+ isnull(hrs_NewEmployee.GrandArbName,' ')+' '+isnull(hrs_NewEmployee.FamilyArbName,' ') "

    '            Else
    '                EmpName = " isnull(hrs_NewEmployee.EngName,' ')+' '+isnull(hrs_NewEmployee.FatherEngName,' ')+' '+isnull(hrs_NewEmployee.GrandEngName ,' ')+' '+isnull(hrs_NewEmployee.FamilyEngName,' ')"

    '            End If

    '            Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

    '            Dim DS2 As New Data.DataSet()
    '            Dim connetionString2 As String
    '            Dim connection2 As Data.SqlClient.SqlConnection
    '            Dim command2 As Data.SqlClient.SqlCommand
    '            Dim adapter2 As New Data.SqlClient.SqlDataAdapter
    '            connetionString2 = ClsEmployees.ConnectionString
    '            connection2 = New Data.SqlClient.SqlConnection(connetionString2)
    '            Dim strselect As String
    '            strselect = "select " & EmpName & "  FROM  hrs_NewEmployee where Code='" & txtEmpCode.Text & "'"
    '            'command2 = New Data.SqlClient.SqlCommand(strselect, connection2)

    '            Dim EmployeeName As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, strselect)
    '            ClsEmployees.Find("Code='" & txtEmpCode.Text & "'")
    '            If ClsEmployees.ID > 0 Then

    '                TxtEmpName.Text = EmployeeName

    '            Else
    '                TxtEmpName.Text = ""
    '                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Sorry there is no employee with this code !/!عفوا لا يوجد موظف مسجل بهذا الكود"))

    '            End If
    '        Else
    '            TxtEmpName.Text = ""
    '        End If

    '        Dim filter As String
    '        filter = " and RegisterDate>='" & Convert.ToDateTime(ddlFromDate.Value.ToString()).ToString("yyyy-MM-dd") & "' and RegisterDate<='" & Convert.ToDateTime(ddlToDate.Value).ToString("yyyy-MM-dd") & "'"
    '        If Not String.IsNullOrEmpty(txtEmpCode.Text) Then
    '            filter += " and Code='" & txtEmpCode.Text & "'"
    '        End If
    '        filter += " and isnull(IsTransfered,0)=0"
    '        GetAllEmployeeRequests(filter)

    '    Catch ex As Exception

    '    End Try
    'End Sub
    Protected Sub txtFromDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFromDate.ValueChanged
        Dim filter As String
        filter = " and RegisterDate>='" & Convert.ToDateTime(ddlFromDate.Value.ToString()).ToString("yyyy-MM-dd") & "' and cast( RegisterDate as date)<='" & Convert.ToDateTime(ddlToDate.Value).ToString("yyyy-MM-dd") & "'"


        filter += " and isnull(IsTransfered,0)=0 and CancelDate is null"
        GetAllEmployeeRequests(filter)

    End Sub

    Protected Sub txtToDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlToDate.ValueChanged
        Dim filter As String
        filter = " and RegisterDate>='" & Convert.ToDateTime(ddlFromDate.Value.ToString()).ToString("yyyy-MM-dd") & "' and cast( RegisterDate as date)<='" & Convert.ToDateTime(ddlToDate.Value).ToString("yyyy-MM-dd") & "'"

        filter += " and isnull(IsTransfered,0)=0 and CancelDate is null"
        GetAllEmployeeRequests(filter)

    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Delete.Command
        Dim IntId As Integer
        Dim strMode As String
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        ClsEmployees = New Clshrs_NewEmployee(Page)
        Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesVacations.ConnectionString)
        'If uwgEmployeeVacations.Rows.Count > 0 Then
        '    IntId = uwgEmployeeVacations.Rows(0).Cells.FromKey("ID").Value
        '    strMode = "E"
        'Else
        '    strMode = "N"
        '    IntId = 0
        'End If

    End Sub
#End Region
#Region "Private Functions"
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
    Public Shared Function GetNDate_Shared(ByVal date1 As Object, ByVal dteDefault As Date) As Date
        If IsDBNull(date1) Then
            Return dteDefault
        Else
            Return CDate(date1)
        End If
    End Function



    Public Sub GetAllEmployeeRequests(ByVal filter As String)
        Try
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_NewEmployee(Me)
            'ClsEmployees.Find("Code='" & _sys_User.Code & "'")
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
            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " [dbo].[fn_GetNewEmpName](hrs_NewEmployee.ID,1) "
            Else
                EmpName = "[dbo].[fn_GetNewEmpName](hrs_NewEmployee.ID,0)"
            End If

            Dim str1 As String = ""

            str1 = "SELECT [ID]," & EmpName & " as EmployeeName,[RegisterDate] as JoinDate,[PersonalEmail] as E_Mail,[Mobile] FROM [dbo].[hrs_NewEmployee] where 1=1 " + filter


            command = New Data.SqlClient.SqlCommand(str1, connection)
            adapter.SelectCommand = command
            adapter.Fill(DS1, "Table1")

            connection.Close()
            uwgEmployeeVacations.DataSource = Nothing
            uwgEmployeeVacations.DataBind()

            uwgEmployeeVacations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
            uwgEmployeeVacations.DataSource = DS1
            uwgEmployeeVacations.DataBind()


        Catch ex As Exception

        End Try
    End Sub





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

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim RequestSerial As Integer

        'RequestSerial = uwgEmployeeVacations.DisplayLayout.SelectedRows.Item(0).GetCellValue()



        'Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPostJournalPreview.aspx?IDs=" & StrIDArray & "", 1500, 1200, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "wWindO", False, True, False, False, False, False, False, False, False)
    End Sub

    Private Sub LinkButton_Remarks_Load(sender As Object, e As EventArgs) Handles LinkButton_Remarks.Load

    End Sub
    Protected Sub ImageButton_Refresh_Click(sender As Object, e As System.EventArgs) Handles ImageButton_Refresh.Click
        Dim filter As String
        filter = " and RegisterDate>='" & Convert.ToDateTime(ddlFromDate.Value.ToString()).ToString("yyyy-MM-dd") & "' and cast( RegisterDate as date)<='" & Convert.ToDateTime(ddlToDate.Value).ToString("yyyy-MM-dd") & "'"

        filter += " and isnull(IsTransfered,0)=0 and CancelDate is null"
        GetAllEmployeeRequests(filter)
    End Sub
End Class
