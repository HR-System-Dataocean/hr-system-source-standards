Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports Venus.Shared.Web
Imports Infragistics.WebUI.WebSchedule
Imports System.Data.SqlClient
Imports System.Collections.ObjectModel
Imports System.Data.Common
Imports System.Security.Cryptography
Imports System.ServiceModel.Activities.Configuration
Imports System.Activities.Statements

Partial Class FrmAppraisalDetailsAllEmployee
    Inherits MainPage

#Region "Public Decleration"
    Dim mErrorHandler As Venus.Shared.ErrorsHandler
    Dim clsMainOtherFields As clsSys_MainOtherFields
    Private dbOTSalary As Double = 0
    Private dbHOTSalary As Double = 0
    Private ClsClasses As Clshrs_EmployeeClasses
    Private ClsEmployeesVacations As Clshrs_EmployeesVacations
    Private ClsEmployees As Clshrs_Employees


#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim EmployeeID As Integer = Request.QueryString.Item("EmployeeID")
        Dim AppraisalID As Integer = Request.QueryString.Item("AppraisalID")
        'Dim NotificationID As Integer = Request.QueryString.Item("NotificationID")

        Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)

        Dim clsEmployees As New Clshrs_Employees(Page)





        Dim ClsVacationsTypes As New Clshrs_VacationsTypes(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler

        If Not IsPostBack Then

            txtEmployee.Enabled = False

            txtDescEnglishName.Enabled = False

            TxtPosition.Enabled = False
            TxtDepartment.Enabled = False
            TxtFinalScore.Enabled = False

            FillEmployeeData()





        End If
        Dim ClsCountries As New Clssys_Countries(Me.Page)
        Dim clsMainCurrency As New ClsSys_Currencies(Me.Page)

    End Sub


    Private Sub FillEmployeeData()
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Try
            Dim EmployeeID As Integer = Request.QueryString.Item("EmployeeID")
            Dim AppraisalID As Integer = Request.QueryString.Item("AppraisalID")
            'Dim NotificationID As Integer = Request.QueryString.Item("NotificationID")
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_Employees(Page)
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")
            Dim DS1 As New Data.DataSet()
            Dim connetionString As String
            Dim connection As Data.SqlClient.SqlConnection
            Dim command As Data.SqlClient.SqlCommand
            Dim adapter As New Data.SqlClient.SqlDataAdapter
            connetionString = ClsEmployees.ConnectionString
            connection = New Data.SqlClient.SqlConnection(connetionString)

            Dim EmpName As String
            Dim Position As String
            Dim Department As String
            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "
                Position = " dbo.hrs_Positions.ArbName "
                Department = " sys_Departments.ArbName "
            Else

                EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,0) "
                Position = " dbo.hrs_Positions.EngName "
                Department = " sys_Departments.EngName "
            End If



            Dim DS2 As New Data.DataSet()
            Dim connetionString2 As String
            Dim connection2 As Data.SqlClient.SqlConnection
            Dim command2 As Data.SqlClient.SqlCommand
            Dim adapter2 As New Data.SqlClient.SqlDataAdapter
            connetionString2 = ClsEmployees.ConnectionString
            connection2 = New Data.SqlClient.SqlConnection(connetionString2)
            Dim strselect As String
            strselect = "select hrs_Employees.id,hrs_Employees.Code as EmployeeCode, " & EmpName & " as EmployeeName," & Department & " as DepartmentName, " & Position & " as PositionName from  hrs_Employees join sys_Departments on hrs_Employees.DepartmentID=sys_Departments.ID join hrs_Contracts on hrs_Contracts.EmployeeID =hrs_Employees.ID and (hrs_Contracts.EndDate is null or hrs_Contracts.EndDate > getdate()) join hrs_Positions on hrs_contracts.PositionID=hrs_Positions.ID where hrs_Employees.ID=" & EmployeeID & ""
            command2 = New Data.SqlClient.SqlCommand(strselect, connection2)
            adapter2.SelectCommand = command2
            adapter2.Fill(DS2, "Table1")
            adapter2.Dispose()
            command2.Dispose()
            connection2.Close()
            txtEmployee.Text = DS2.Tables(0).Rows(0)("EmployeeCode").ToString()
            txtDescEnglishName.Text = DS2.Tables(0).Rows(0)("EmployeeName").ToString()
            TxtPosition.Text = DS2.Tables(0).Rows(0)("PositionName").ToString()
            TxtDepartment.Text = DS2.Tables(0).Rows(0)("DepartmentName").ToString()
            FillAppraisalCriteria()
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Private Sub FillAppraisalCriteria()
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Try
            Dim EmployeeID As Integer = Request.QueryString.Item("EmployeeID")
            Dim AppraisalID As Integer = Request.QueryString.Item("AppraisalID")
            'Dim NotificationID As Integer = Request.QueryString.Item("NotificationID")
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_Employees(Page)
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")
            Dim DS1 As New Data.DataSet()
            Dim connetionString As String
            Dim connection As Data.SqlClient.SqlConnection
            Dim command As Data.SqlClient.SqlCommand
            Dim adapter As New Data.SqlClient.SqlDataAdapter
            connetionString = ClsEmployees.ConnectionString
            connection = New Data.SqlClient.SqlConnection(connetionString)

            Dim CriteriaName As String
            Dim Position As String
            Dim Department As String
            Dim EmpName As String
            If ProfileCls.CurrentLanguage = "Ar" Then
                CriteriaName = " App_Criteria.ArbName "
                Position = " dbo.hrs_Positions.ArbName "
                Department = " sys_Departments.ArbName "
                EmpName = " dbo.fn_GetEmpName(hrs_Employees.Code,1)"
            Else

                CriteriaName = " App_Criteria.EngName "
                Position = " dbo.hrs_Positions.EngName "
                Department = " sys_Departments.EngName "
                EmpName = " dbo.fn_GetEmpName(hrs_Employees.Code,0)"
            End If



            Dim DS2 As New Data.DataSet()
            Dim connetionString2 As String
            Dim connection2 As Data.SqlClient.SqlConnection
            Dim command2 As Data.SqlClient.SqlCommand
            Dim adapter2 As New Data.SqlClient.SqlDataAdapter
            connetionString2 = ClsEmployees.ConnectionString
            connection2 = New Data.SqlClient.SqlConnection(connetionString2)
            Dim strselect As String
            'strselect = "SELECT        AppraisalID, CriteriaID, CriteriaName, MinimumScore, MaximumScore,avg(Result) as AppraisalScore, sum(AppCriteriaScore) as AppraisalScorePercent from(select App_AppraisalResult.AppraisalID,App_AppraisalResult.CriteriaID," & CriteriaName & " As CriteriaName ,APP_AppraisalCriterias.MinimumScore,APP_AppraisalCriterias.MaximumScore, SUM(cast(dbo.App_AppraisalResult.Score AS FLOAT))/count ([App_AppraisalConfigurations].ID) AS Result, dbo.APP_AppraisalCriterias.Weight, round( (SUM(CAST(dbo.App_AppraisalResult.Score AS FLOAT))/ dbo.APP_AppraisalCriterias.MaximumScore )*  dbo.APP_AppraisalCriterias.Weight*[App_AppraisalConfigurations].StageWeight/100,2) As AppCriteriaScore FROM     dbo.App_AppraisalResult INNER JOIN dbo.App_Criteria ON dbo.App_AppraisalResult.CriteriaID = dbo.App_Criteria.ID INNER JOIN dbo.APP_AppraisalCriterias ON dbo.APP_AppraisalCriterias.CriteriaID = dbo.App_Criteria.ID and APP_AppraisalCriterias.AppraisalID=App_AppraisalResult.AppraisalID Inner join [dbo].[App_AppraisalNotifications] on App_AppraisalResult.NotificationID= App_AppraisalNotifications.ID Inner Join [dbo].[App_AppraisalConfigurations] on App_AppraisalNotifications.ConfigurationID=App_AppraisalConfigurations.ID INNER JOIN dbo.hrs_Employees ON dbo.App_AppraisalResult.App_EmployeeID = dbo.hrs_Employees.ID where App_AppraisalResult.AppraisalID=" & AppraisalID & " GROUP BY dbo.App_Criteria.ArbName, dbo.APP_AppraisalCriterias.MinimumScore, dbo.APP_AppraisalCriterias.MaximumScore, dbo.APP_AppraisalCriterias.Weight, dbo.App_AppraisalResult.AppraisalID, dbo.App_AppraisalResult.CriteriaID,dbo.App_AppraisalResult.EmployeeID, dbo.App_AppraisalResult.CriteriaID, dbo.hrs_Employees.Code, dbo.hrs_Employees.EngName, dbo.App_AppraisalResult.AppraisalID,dbo.App_Criteria.EngName,[App_AppraisalConfigurations].StageWeight) as t group by  AppraisalID, CriteriaID, CriteriaName, MinimumScore, MaximumScore "
            strselect = "select AppraisalID,CriteriaGroupID, CriteriaID, CriteriaName, MinimumScore, MaximumScore,avg(AppraisalScore) as AppraisalScore ,AppraisalScorePercent from V_AppraisalResult Where AppraisalID=" & AppraisalID & "  group by  AppraisalID, CriteriaGroupID,CriteriaID, CriteriaName, MinimumScore, MaximumScore ,AppraisalScorePercent order by CriteriaGroupID "
            'command2 = New Data.SqlClient.SqlCommand(strselect, connection2)
            'adapter2.SelectCommand = command2
            'adapter2.Fill(DS2, "Table1")
            'adapter2.Dispose()
            'command2.Dispose()
            'connection2.Close()
            'uwgEmployeeAppraisal.DataSource = DS2.Tables(0)
            'uwgEmployeeAppraisal.DataBind()

            Dim str2 As String = "select App_AppraisalResult.AppraisalID,App_AppraisalResult.CriteriaID," & EmpName & " as AppEmployee, SUM(cast(dbo.App_AppraisalResult.Score AS FLOAT))/count ([App_AppraisalConfigurations].ID) AS Result, round( (SUM(CAST(dbo.App_AppraisalResult.Score AS FLOAT))/ dbo.APP_AppraisalCriterias.MaximumScore )*  dbo.APP_AppraisalCriterias.Weight*[App_AppraisalConfigurations].StageWeight/100,2) As AppCriteriaScore,[App_AppraisalConfigurations].StageWeight FROM     dbo.App_AppraisalResult INNER JOIN dbo.App_Criteria ON dbo.App_AppraisalResult.CriteriaID = dbo.App_Criteria.ID INNER JOIN dbo.APP_AppraisalCriterias ON dbo.APP_AppraisalCriterias.CriteriaID = dbo.App_Criteria.ID and APP_AppraisalCriterias.AppraisalID=App_AppraisalResult.AppraisalID Inner join [dbo].[App_AppraisalNotifications] on App_AppraisalResult.NotificationID= App_AppraisalNotifications.ID Inner Join [dbo].[App_AppraisalConfigurations] on App_AppraisalNotifications.ConfigurationID=App_AppraisalConfigurations.ID INNER JOIN dbo.hrs_Employees ON dbo.App_AppraisalResult.App_EmployeeID = dbo.hrs_Employees.ID where App_AppraisalResult.AppraisalID=" & AppraisalID & "  GROUP BY  dbo.App_AppraisalResult.AppraisalID,dbo.App_AppraisalResult.CriteriaID,dbo.App_AppraisalResult.EmployeeID, dbo.hrs_Employees.Code,[App_AppraisalConfigurations].StageWeight,dbo.APP_AppraisalCriterias.MaximumScore,dbo.APP_AppraisalCriterias.Weight"
            ' Dim str2 As String = " select App_AppraisalResult.AppraisalID,App_AppraisalResult.CriteriaID,dbo.fn_GetEmpName(hrs_Employees.Code,0) as AppEmployee, dbo.App_AppraisalResult.Score   AS Result, round( (SUM(CAST(dbo.App_AppraisalResult.Score AS FLOAT))/ dbo.APP_AppraisalCriterias.MaximumScore )*  dbo.APP_AppraisalCriterias.Weight*[App_AppraisalConfigurations].StageWeight/100,2) As AppCriteriaScore,[App_AppraisalConfigurations].StageWeight FROM     dbo.App_AppraisalResult INNER JOIN dbo.App_Criteria ON dbo.App_AppraisalResult.CriteriaID = dbo.App_Criteria.ID INNER JOIN dbo.APP_AppraisalCriterias ON dbo.APP_AppraisalCriterias.CriteriaID = dbo.App_Criteria.ID and APP_AppraisalCriterias.AppraisalID=App_AppraisalResult.AppraisalID Inner join [dbo].[App_AppraisalNotifications] on App_AppraisalResult.NotificationID= App_AppraisalNotifications.ID Inner Join [dbo].[App_AppraisalConfigurations] on App_AppraisalNotifications.ConfigurationID=App_AppraisalConfigurations.ID INNER JOIN dbo.hrs_Employees ON dbo.App_AppraisalResult.App_EmployeeID = dbo.hrs_Employees.ID where App_AppraisalResult.AppraisalID=1 GROUP BY App_AppraisalResult.Score, dbo.App_AppraisalResult.AppraisalID,dbo.App_AppraisalResult.CriteriaID,dbo.App_AppraisalResult.EmployeeID, dbo.hrs_Employees.Code,[App_AppraisalConfigurations].StageWeight,dbo.APP_AppraisalCriterias.MaximumScore,dbo.APP_AppraisalCriterias.Weight "

            command = New Data.SqlClient.SqlCommand(strselect, connection)
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
            DataCol1 = DS1.Tables(0).Columns(2)
            DataCol2 = DS1.Tables(1).Columns(1)
            Dim Rel1 As Data.DataRelation = New Data.DataRelation("Rel1", DataCol1, DataCol2, False)
            DS1.Relations.Add(Rel1)

            uwgEmployeeAppraisal.DataSource = Nothing
            uwgEmployeeAppraisal.DataBind()

            uwgEmployeeAppraisal.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
            uwgEmployeeAppraisal.DataSource = DS1
            uwgEmployeeAppraisal.DataBind()
            Dim finalscorePercentage As Decimal = 0
            Dim strscore As String = "select isnull(Score,0) from V_AppraisalFinalResult where AppraisalID= " & AppraisalID & ""
            finalscorePercentage = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, strscore)
            TxtFinalScore.Text = finalscorePercentage.ToString() + "%"
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub


    Private Function SendNextLevelNotification()
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim connection As New SqlConnection(ClsEmployees.ConnectionString)
        Dim EmployeeID As Integer = Request.QueryString.Item("EmployeeID")
        Dim AppraisalID As Integer = Request.QueryString.Item("AppraisalID")
        Dim NotificationID As Integer = Request.QueryString.Item("NotificationID")
        Dim _sys_User As New Clssys_Users(Page)
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim User As String = String.Empty

        WebHandler.GetCookies(Page, "UserID", User)

        _sys_User.Find("ID = '" & User & "'")

        ClsEmployees.Find("Code='" & _sys_User.Code & "'")
        Try
            Dim ConnectionString As String
            ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
            Dim str As String
            str = "select ConfigurationLevel from App_AppraisalNotifications where id=" & NotificationID & " "
            Dim CurrentLevele As Integer
            CurrentLevele = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, str)
            'Get Nxt Level Configurations
            Dim strNextConfig As String
            str = "select * from App_AppraisalConfigurations where AppraisalID= " & AppraisalID & "	and Rank=" & CurrentLevele & " + 1 "
            Dim DSConfig As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, str)
            If DSConfig.Tables(0).Rows.Count > 0 Then
                Dim strNotification As String = ""
                Dim DsAppraisalEmployees As DataTable

                For Each row In DSConfig.Tables(0).Rows
                    If row("UserTypeID") = 1 Then  'مدير مباشر
                        Dim DsEmployees As DataTable = (GetAppraisalEmployeesIthDirectManager(AppraisalID))
                        If DsEmployees.Rows.Count > 0 Then
                            For Each Erow In DsEmployees.Rows
                                strNotification = "INSERT INTO [dbo].[App_AppraisalNotifications] ([AppraisalID],[APP_EmployeeID],[EmployeeID],[ConfigurationLevel],ConfigurationID,RegDate , RegUserID )Values(" & AppraisalID & "," & Erow("ManagerID") & "," & Erow("EmployeeID") & "," & row("Rank") & "," & row("ID") & ",GetDate()," & _sys_User.ID & ")"
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, Data.CommandType.Text, strNotification)

                            Next
                        End If
                    End If
                    If row("UserTypeID") = 2 Then  'درجة وظيفية
                        Dim DsEmployeesInPosition As DataTable = (GetAppraisalEmployeesInPosition(row("PositionID")))
                        If DsEmployeesInPosition.Rows.Count > 0 Then
                            For Each Erow In DsEmployeesInPosition.Rows

                                strNotification = "INSERT INTO [dbo].[App_AppraisalNotifications] ([AppraisalID],[APP_EmployeeID],[EmployeeID],[ConfigurationLevel],ConfigurationID ,RegDate , RegUserID)Values(" & AppraisalID & "," & Erow("EmployeeID") & "," & EmployeeID & "," & row("Rank") & "," & row("ID") & ",GetDate()," & _sys_User.ID & ")"

                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, Data.CommandType.Text, strNotification)


                            Next
                        End If
                    End If
                    If row("UserTypeID") = 3 Then   'موظف


                        strNotification = "INSERT INTO [dbo].[App_AppraisalNotifications] ([AppraisalID],[APP_EmployeeID],[EmployeeID],[ConfigurationLevel],ConfigurationID ,RegDate , RegUserID)Values(" & AppraisalID & "," & row("EmployeeID") & "," & EmployeeID & "," & row("Rank") & "," & row("ID") & ",GetDate()," & _sys_User.ID & ")"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, Data.CommandType.Text, strNotification)

                    End If
                Next
            End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployees.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        Finally
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

#End Region

#Region "Private Function"


    Private Function GetAppraisalEmployeesIthDirectManager(AppraisalID As Integer) As DataTable
        Dim EmployeeID As Integer = Request.QueryString.Item("EmployeeID")
        Dim ConnectionString As String
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strselect As String
        strselect = "select EmployeeID,ManagerID from hrs_Employees join APP_AppraisalEmployees on APP_AppraisalEmployees.EmployeeID=hrs_Employees.id where  hrs_Employees.id= " & EmployeeID & " And  AppraisalID=" & AppraisalID & "  "
        Dim DSApp_Employees As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
        Return DSApp_Employees.Tables(0)
    End Function

    Private Function GetAppraisalEmployeesInPosition(PositionID As Integer) As DataTable

        Dim ConnectionString As String
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strselect As String
        strselect = "select EmployeeID from Hrs_Contracts where CancelDate is null and( EndDate is null or EndDate> GetDate() )and PositionID =" & PositionID & "  "
        Dim DSApp_EmployeesInPosition As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
        Return DSApp_EmployeesInPosition.Tables(0)
    End Function
    Private Function GetAppraisalEmployees(AppraisalID As Integer) As DataTable

        Dim ConnectionString As String
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strselect As String
        strselect = "select EmployeeID from APP_AppraisalEmployees where AppraisalID =" & AppraisalID & "  "
        Dim DSApp_Employees As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
        Return DSApp_Employees.Tables(0)
    End Function
#End Region

End Class
