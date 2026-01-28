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

Partial Class frmAttendancePreparation
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
        Dim EmployeeID As Integer = 100 'Request.QueryString.Item("EmployeeID")
        Dim AppraisalID As Integer = 100 'Request.QueryString.Item("AppraisalID")
        Dim NotificationID As Integer = Request.QueryString.Item("NotificationID")
        Dim ConfigurationLevel As Integer = Request.QueryString.Item("ConfigurationLevel")
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

            lblObjectionDetais.Visible = False
            txtObjectionDetais.Visible = False
            FillEmployeeData()





        End If
        Dim ClsCountries As New Clssys_Countries(Me.Page)
        Dim clsMainCurrency As New ClsSys_Currencies(Me.Page)

    End Sub


    Private Sub FillEmployeeData()
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Try
            Dim EmployeeID As Integer = 100 ' Request.QueryString.Item("EmployeeID")
            Dim AppraisalID As Integer = 100 'Request.QueryString.Item("AppraisalID")
            Dim NotificationID As Integer = 100 'Request.QueryString.Item("NotificationID")
            Dim ConfigurationLevel As Integer = 100 ' Request.QueryString.Item("ConfigurationLevel")

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
            ''''''''''''''==================Get Objection Details======================================
            Dim str As String = ""
            str = "select * from App_AppraisalNotifications where AppraisalID=" & AppraisalID & " and EmployeeID=" & EmployeeID & "  and APP_EmployeeID=" & EmployeeID & " and isobjection=1 and ConfigurationLevel=" & ConfigurationLevel & " + 1"
            Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connetionString2, CommandType.Text, str)
            If DS.Tables(0).Rows.Count > 0 Then
                lblObjectionDetais.Visible = True
                txtObjectionDetais.Visible = True
                txtObjectionDetais.Text = DS.Tables(0).Rows(0)("ObjectionDetails").ToString()
                GetPreviousAppraisalEvaluation()
            Else
                FillAppraisalCriteria()

            End If


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
            Dim EmployeeID As Integer = 100 ' Request.QueryString.Item("EmployeeID")
            Dim AppraisalID As Integer = 100 ' Request.QueryString.Item("AppraisalID")
            Dim NotificationID As Integer = 100 ' Request.QueryString.Item("NotificationID")
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
            If ProfileCls.CurrentLanguage = "Ar" Then
                CriteriaName = " App_Criteria.ArbName "
                Position = " dbo.hrs_Positions.ArbName "
                Department = " sys_Departments.ArbName "
            Else

                CriteriaName = " App_Criteria.EngName "
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
            strselect = "select APP_AppraisalCriterias.AppraisalID,APP_AppraisalCriterias.CriteriaID," & CriteriaName & " As CriteriaName ,APP_AppraisalCriterias.MinimumScore,APP_AppraisalCriterias.MaximumScore from APP_AppraisalCriterias join App_Criteria on APP_AppraisalCriterias.CriteriaID=App_Criteria.ID where AppraisalID=" & AppraisalID & "  "
            command2 = New Data.SqlClient.SqlCommand(strselect, connection2)
            adapter2.SelectCommand = command2
            adapter2.Fill(DS2, "Table1")
            adapter2.Dispose()
            command2.Dispose()
            connection2.Close()
            uwgEmployeeAppraisal.DataSource = DS2.Tables(0)
            uwgEmployeeAppraisal.DataBind()

        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Private Sub GetPreviousAppraisalEvaluation()
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Try
            Dim EmployeeID As Integer = 100 ' Request.QueryString.Item("EmployeeID")
            Dim AppraisalID As Integer = 100 'Request.QueryString.Item("AppraisalID")
            Dim NotificationID As Integer = 100 ' Request.QueryString.Item("NotificationID")
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
            If ProfileCls.CurrentLanguage = "Ar" Then
                CriteriaName = " App_Criteria.ArbName "
                Position = " dbo.hrs_Positions.ArbName "
                Department = " sys_Departments.ArbName "
            Else

                CriteriaName = " App_Criteria.EngName "
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
            strselect = "select APP_AppraisalCriterias.AppraisalID,APP_AppraisalCriterias.CriteriaID," & CriteriaName & " As CriteriaName ,APP_AppraisalCriterias.MinimumScore,APP_AppraisalCriterias.MaximumScore ,App_AppraisalResult.Score As AppraisalScore from APP_AppraisalCriterias join App_Criteria on APP_AppraisalCriterias.CriteriaID=App_Criteria.ID join App_AppraisalResult on APP_AppraisalCriterias.AppraisalID=App_AppraisalResult.AppraisalID and App_AppraisalResult.CriteriaID=App_Criteria.ID  where APP_AppraisalCriterias.AppraisalID=" & AppraisalID & " and App_AppraisalResult.App_EmployeeID=" & ClsEmployees.ID & " and EmployeeID=" & EmployeeID & "  "
            command2 = New Data.SqlClient.SqlCommand(strselect, connection2)
            adapter2.SelectCommand = command2
            adapter2.Fill(DS2, "Table1")
            adapter2.Dispose()
            command2.Dispose()
            connection2.Close()
            uwgEmployeeAppraisal.DataSource = DS2.Tables(0)
            uwgEmployeeAppraisal.DataBind()

        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click
        Dim EmployeeID As Integer = 100 'Request.QueryString.Item("EmployeeID")
        Dim AppraisalID As Integer = 100 'Request.QueryString.Item("AppraisalID")
        Dim NotificationID As Integer = 100 ' Request.QueryString.Item("NotificationID")
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim WebHandler As New Venus.Shared.Web.WebHandler

        Dim User As String = String.Empty


        WebHandler.GetCookies(Page, "UserID", User)
        Dim _sys_User As New Clssys_Users(Page)
        _sys_User.Find("Code = '" & User & "'")




        WebHandler.GetCookies(Page, "UserID", User)

        _sys_User.Find("ID = '" & User & "'")

        ClsEmployees.Find("Code='" & _sys_User.Code & "'")


        Dim SqlCommand As Data.SqlClient.SqlCommand
        Dim UpdateCommand As String = ""



        Dim MinScore As Integer
        Dim MaxScore As Integer
        Dim AppraisalScore As Integer
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

        For Each DGRow In uwgEmployeeAppraisal.Rows
            MinScore = DGRow.Cells.FromKey("MinimumScore").Value
            MaxScore = DGRow.Cells.FromKey("MaximumScore").Value
            AppraisalScore = DGRow.Cells.FromKey("AppraisalScore").Value
            If AppraisalScore < MinScore Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...Appraisal Score Should be Greater Than Or Equal To  Minimum Score /  عفوا...لابد ان يكون درجة التقييم اكبر او يساوي الحد الادني للتقييم"))
                Exit Sub
            End If
            If AppraisalScore > MaxScore Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...Appraisal Score Should be Less Than Or Equal To Maximum Score /  عفوا...لابد ان يكون درجة التقييم اقل من او يساوي الحد الاقصي للتقييم"))
                Exit Sub
            End If
        Next

        If SaveDG() Then
            UpdateCommand = "update App_AppraisalNotifications set Completed=1,RegDate=GetDate(),CompleteDate=GetDate(),RegUserID=" & _sys_User.ID & " where id=" & NotificationID & "  and EmployeeID=" & EmployeeID & ""
            SqlCommand = New SqlClient.SqlCommand
            SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
            SqlCommand.CommandType = CommandType.Text
            SqlCommand.CommandText = UpdateCommand
            SqlCommand.Connection.Open()
            SqlCommand.ExecuteNonQuery()
            SqlCommand.Connection.Close()
            SendNextLevelNotification()
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done successfully /  تم الحفظ بنجاح)"))
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)


        End If
    End Sub
    Private Function SaveDG() As Boolean
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
            Dim insertCommand As String = ""
            Dim DeleteCommand As String = ""
            Dim SqlCommand As Data.SqlClient.SqlCommand

            DeleteCommand = "delete from App_AppraisalResult where AppraisalID=" & AppraisalID & " and App_EmployeeID=" & ClsEmployees.ID & " and EmployeeID=" & EmployeeID & ""
            SqlCommand = New SqlClient.SqlCommand
            SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
            SqlCommand.CommandType = CommandType.Text
            SqlCommand.CommandText = DeleteCommand
            SqlCommand.Connection.Open()
            SqlCommand.ExecuteNonQuery()
            SqlCommand.Connection.Close()
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeeAppraisal.Rows
                If Not String.IsNullOrEmpty(DGRow.Cells.FromKey("CriteriaID").Value) Then

                    insertCommand = "INSERT INTO App_AppraisalResult  (AppraisalID,NotificationID,EmployeeID, CriteriaID, App_EmployeeID,Score,Remarks, RegUserID, RegDate)  VALUES (" & AppraisalID & ", " & NotificationID & "," & EmployeeID & ", '" & DGRow.Cells.FromKey("CriteriaID").Value & "' , " & ClsEmployees.ID & " , '" & DGRow.Cells.FromKey("AppraisalScore").Value & "' , '" & DGRow.Cells.FromKey("AppraisalRemarks").Value & "', '" & _sys_User.ID & "', GETDATE())"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, Data.CommandType.Text, insertCommand)

                End If
            Next

            Return True

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
                    If row("UserTypeID") = 4 Then   'موظف


                        strNotification = "INSERT INTO [dbo].[App_AppraisalNotifications] ([AppraisalID],[APP_EmployeeID],[EmployeeID],[ConfigurationLevel],ConfigurationID ,RegDate , RegUserID)Values(" & AppraisalID & "," & EmployeeID & "," & EmployeeID & "," & row("Rank") & "," & row("ID") & ",GetDate()," & _sys_User.ID & ")"
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
