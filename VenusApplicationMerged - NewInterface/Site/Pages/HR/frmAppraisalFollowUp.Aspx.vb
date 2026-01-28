Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Venus.Shared.Web
Imports System.Security.Cryptography
Imports System.Collections.ObjectModel

Partial Class frmAppraisalFollowUp
    Inherits MainPage
#Region "Public Decleration"
    Private ClsEmployeesVacations As Clshrs_EmployeesVacations
    Private ClsEmployees As Clshrs_Employees
    Private ClsAppraisalTypes As Clshrs_AppraisalTypes
    Private ClsAppraisal As ClsHrs_AppraisalCreate
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
                ClsEmployees = New Clshrs_Employees(Page)
                ClsAppraisalTypes = New Clshrs_AppraisalTypes(Page)
                ClsAppraisal = New ClsHrs_AppraisalCreate(Page)
                WebHandler.GetCookies(Page, "UserID", User)
                Dim _sys_User As New Clssys_Users(Page)
                _sys_User.Find("ID = '" & User & "'")


                Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
                ClsEmployeesVacations.AddNotificationOnChange(Page)
                Dim ClsLevels As New Clshrs_LevelTypes(Page)
                Dim ClsDataHandler As New Venus.Shared.DataHandler
                Dim StrSerial As String = String.Empty
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
                Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)

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

                lblLage.Text = ObjNavigationHandler.SetLanguage(Page, "0/1")
                Page.Session.Add("Lage", lblLage.Text)

                ClsAppraisalTypes.GetDropDownList(DdlAppraisalType, True)
                ClsAppraisal.GetDropDownList(ddlAppraisals, True)
                GetAppraisalNotifications()

            Catch ex As Exception
                mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
                Page.Session.Add("ErrorValue", ex)
                mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
                Page.Response.Redirect("ErrorPage.aspx")
            End Try

        End If
    End Sub

    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Delete.Command
        Dim IntId As Integer
        Dim strMode As String
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesVacations.ConnectionString)
        'If uwgAppraisalNotifications.Rows.Count > 0 Then
        '    IntId = uwgAppraisalNotifications.Rows(0).Cells.FromKey("ID").Value
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

    Private Sub GetAppraisalNotifications()
        Try
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_Employees(Me)
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")
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
            Dim AppraisalName As String
            Dim AppraisalType As String
            Dim result As String
            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " dbo.fn_GetEmpName(E.Code,1)"
                AppraisalName = "A.ArabName "
                AppraisalType = " T.ArbName "
                result = " case when sum(AppraisalResult/100)<50/100 then 'ضعيف' else case when sum(AppraisalResult)<65 then 'متوسط' else case when sum(AppraisalResult)<75 then 'جيد' else case when sum(AppraisalResult)<85 then 'جيد جداً' else case when sum(AppraisalResult)>85 then 'ممتاز' else 'في انتظار التقييم' end end end end end  "
            Else
                EmpName = " dbo.fn_GetEmpName(E.Code,0)"
                AppraisalName = "A.EngName "
                AppraisalType = " T.EngName "
                result = "  case when sum(AppraisalResult/100)<50/100 then 'weak' else case when sum(AppraisalResult)<65 then 'Medium' else case when sum(AppraisalResult)<75 then 'Good' else case when sum(AppraisalResult)<85 then 'Very Good' else case when sum(AppraisalResult)>85 then 'Excellent' else 'Awaiting Evaluation' end end end end end  "
            End If

            Dim str1 As String
            Dim Criteria As String = ""
            'str1 = "SELECT AppraisalID, EmployeeID, AppraisalCode, AppraisalType, AppraisalName, EmployeeName, " & result & " AS AppraisalResult FROM (SELECT N.AppraisalID, N.EmployeeID, A.Code AS AppraisalCode, " & AppraisalType & " AS AppraisalType, " & AppraisalName & " AS AppraisalName, " & EmpName & " AS EmployeeName, sum(CR.CriteriaScore)*App_AppraisalConfigurations.StageWeight/100 AS AppraisalResult, N.ConfigurationID FROM App_AppraisalNotifications N JOIN APP_Appraisals A ON N.AppraisalID = A.ID JOIN App_AppraisalTypes T ON A.AppraisalTypeID = T.ID JOIN Hrs_Employees E ON E.ID = N.EmployeeID join App_AppraisalConfigurations on N.ConfigurationID=App_AppraisalConfigurations.ID LEFT OUTER JOIN (SELECT AC.ID AS CriteriaID, SUM(CAST(R.Score AS FLOAT)) / COUNT(appC.ID) / CASE WHEN ISNULL(AC.MaximumScore, 0) > 0 THEN AC.MaximumScore ELSE 1 END * ISNULL(AC.Weight, 0) AS CriteriaScore, AC.AppraisalID, R.EmployeeID, R.NotificationID FROM APP_AppraisalCriterias AC JOIN App_AppraisalResult R ON R.AppraisalID = AC.AppraisalID AND R.CriteriaID = AC.CriteriaID INNER JOIN [dbo].[App_AppraisalConfigurations] appC ON AC.AppraisalID = appC.AppraisalTypeID GROUP BY AC.ID, AC.MaximumScore, AC.Weight, AC.AppraisalID, R.EmployeeID, R.NotificationID) CR ON CR.NotificationID = N.ID AND CR.EmployeeID = N.EmployeeID WHERE 1=1 "
            str1 = "Select * from V_AppraisalFinalResult Where EmployeeID= " & ClsEmployees.ID & " "
            If DdlAppraisalType.SelectedValue > 0 Then
                Criteria &= " And   AppraisalTypeID = " & DdlAppraisalType.SelectedValue & " "
            End If
            If ddlAppraisals.SelectedValue > 0 Then
                Criteria &= " And   AppraisalID = " & ddlAppraisals.SelectedValue & " "

            End If

            str1 &= Criteria
            '            str1 &= "  GROUP BY N.AppraisalID, N.EmployeeID, A.Code, T.ArbName,T.EngName,A.EngName, A.ArabName, E.Code, N.ConfigurationID,App_AppraisalConfigurations.StageWeight) AS AllData GROUP BY AppraisalID, EmployeeID, AppraisalCode, AppraisalType, AppraisalName, EmployeeName"
            command = New Data.SqlClient.SqlCommand(str1, connection)
            adapter.SelectCommand = command
            adapter.Fill(DS1, "Table1")

            connection.Close()
            uwgAppraisalNotifications.DataSource = Nothing
            uwgAppraisalNotifications.DataBind()

            uwgAppraisalNotifications.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
            uwgAppraisalNotifications.DataSource = DS1
            uwgAppraisalNotifications.DataBind()


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GetAppraisalNotifications()
    End Sub

    Protected Sub ddl_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlAppraisalType.SelectedIndexChanged
        ClsAppraisal = New ClsHrs_AppraisalCreate(Page)

        If DdlAppraisalType.SelectedValue > 0 Then

            ClsAppraisal.GetDropDownList(ddlAppraisals, True, " AppraisalTypeID=" & DdlAppraisalType.SelectedValue & " ")
        Else
            ClsAppraisal.GetDropDownList(ddlAppraisals, True)
        End If
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




    Private Sub LinkButton_Remarks_Load(sender As Object, e As EventArgs) Handles LinkButton_Remarks.Load

    End Sub
End Class
