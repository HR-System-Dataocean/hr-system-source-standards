Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports Venus.Shared.Web
Imports Infragistics.WebUI.WebSchedule
Imports System.Data.SqlClient
Imports System.Collections.ObjectModel
Imports System.Data.Common
Imports System.Security.Cryptography

Partial Class frmOccurrenceVarianceReportingLettersAction
    Inherits MainPage

#Region "Public Decleration"
    Dim mErrorHandler As Venus.Shared.ErrorsHandler
    Dim clsMainOtherFields As clsSys_MainOtherFields
    Private dbOTSalary As Double = 0
    Private dbHOTSalary As Double = 0
    Private ClsClasses As Clshrs_EmployeeClasses
    Private ClsEmployeesExcuses As Clshrs_EmployeesExcuses
    Private ClsEmployees As Clshrs_Employees


#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim RequestSerial As Integer = Request.QueryString.Item("RequestSerial")
        Dim RequestType As Integer = Request.QueryString.Item("RequestType")
        Dim FormCode As String = Request.QueryString.Item("FormCode")
        Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
        Dim clsEmployees As New Clshrs_Employees(Page)
        Dim ClsVacationsTypes As New Clshrs_VacationsTypes(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler

        If Not IsPostBack Then
            Dim strselectAction As String = "select ActionCode as ID,ActionAraName,ActionEngName from SS_UserActions where ID<4 "
            Dim Item As Global.System.Web.UI.WebControls.ListItem
            Item = New Global.System.Web.UI.WebControls.ListItem
            Item.Value = 0
            Item.Text = ObjNavigationHandler.SetLanguage(Page, "[Select Your Action]/[ برجاء الاختيار ]")
            ddlAction.Items.Add(Item)

            Dim dsActions As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsEmployees.ConnectionString, CommandType.Text, strselectAction)
            For Each dr In dsActions.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Value = dr("ID")
                If ProfileCls.CurrentLanguage = "Ar" Then
                    Item.Text = dr("ActionAraName")
                Else
                    Item.Text = dr("ActionEngName")
                End If
                ddlAction.Items.Add(Item)
            Next
            Dim ConfigID As Integer = Request.QueryString.Item("ConfigID")

            Dim CanEdit As Boolean = False
            Dim ConfigCommand As String = "select * from SS_Configuration where ID=" & ConfigID & ""
            Dim adapter As New Data.SqlClient.SqlDataAdapter
            Dim dsconfig As New Data.DataSet()
            Dim connection As Data.SqlClient.SqlConnection
            connection = New Data.SqlClient.SqlConnection(clsEmployees.ConnectionString)
            Dim command As Data.SqlClient.SqlCommand
            command = New Data.SqlClient.SqlCommand(ConfigCommand, connection)
            adapter.SelectCommand = command
            adapter.Fill(dsconfig)
            connection.Close()
            If dsconfig.Tables(0).Rows.Count > 0 Then
                CanEdit = dsconfig.Tables(0).Rows(0)("CanEdit")
            End If

            txtEmployee.Enabled = False
            TxtRequestSerial.Enabled = False
            txtRequestDate.Enabled = False
            txtDescEnglishName.Enabled = False

            TxtRequestRemarks.Enabled = False
            FillEmployeeVacations()


            btnDelete.Visible = False
            Dim ClsSearchs As New Clssys_Searchs(Page)
            'Dim SearchID As Integer = 0
            'clsEmployees As New Clshrs_Employees(Page)
            Dim ClsObjects As New Clssys_Objects(Page)
            ClsObjects.Find(" Code='" & clsEmployees.Table.Trim & "'")
            ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
            Dim csSearchID As Integer
            csSearchID = ClsSearchs.ID
            Dim IntDimension As Integer = 510

            Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtDelegated.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtDelegated.ClientID & "'"
            btnDelegatedSearch.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            lblDelegated.Visible = False
            txtDelegated.Visible = False
            btnDelegatedSearch.Visible = False
            txtDelegatedName.Visible = False
            txtDelegatedName.Enabled = False
        End If
        Dim ClsCountries As New Clssys_Countries(Me.Page)
        Dim clsMainCurrency As New ClsSys_Currencies(Me.Page)

    End Sub
    Private Sub FillEmployeeVacations()
        Try
            Dim RequestSerial As Integer = Request.QueryString.Item("RequestSerial")
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
            Dim ActionName As String
            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "
                Position = " dbo.hrs_Positions.ArbName "
                Department = " sys_Departments.ArbName "
                ActionName = " SS_UserActions.ActionAraName "
            Else

                ActionName = " SS_UserActions.ActionEngName "
                EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,0) "
                Position = " dbo.hrs_Positions.EngName "
                Department = " sys_Departments.EngName "
            End If

            'lll
            Dim str1 As String
            str1 = "select " & EmpName & " as EmployeeName,( case when " & ActionName & " is not null then " & ActionName & " else 'Pending ...' end) As Action ,convert(varchar, ActionDate,103) as ActionDate,ActionRemarks  from SS_OccurrenceVarianceReportingLetters join SS_RequestActions on SS_OccurrenceVarianceReportingLetters.ID=SS_RequestActions.RequestSerial and SS_OccurrenceVarianceReportingLetters.EmployeeID=SS_RequestActions.EmployeeID join hrs_Employees on hrs_Employees.ID= SS_RequestActions.SS_EmployeeID left join SS_UserActions on SS_RequestActions.ActionID=SS_UserActions.ID where RequestSerial=" & RequestSerial & " and ( SS_RequestActions.IsHidden is null or SS_RequestActions.IsHidden=0 ) and FormCode='SS_001918' "

            command = New Data.SqlClient.SqlCommand(str1, connection)
            adapter.SelectCommand = command
            adapter.Fill(DS1, "Table1")
            adapter.Dispose()
            command.Dispose()
            connection.Close()
            Dim DataCol1 As Data.DataColumn
            DataCol1 = DS1.Tables(0).Columns(0)
            uwgEmployeeVacations.DataSource = Nothing
            uwgEmployeeVacations.DataBind()

            uwgEmployeeVacations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
            uwgEmployeeVacations.DataSource = DS1
            uwgEmployeeVacations.DataBind()

            'lll

            Dim DS2 As New Data.DataSet()
            'Dim connetionString As String
            Dim connection2 As Data.SqlClient.SqlConnection
            Dim command2 As Data.SqlClient.SqlCommand
            Dim adapter2 As New Data.SqlClient.SqlDataAdapter
            connetionString = ClsEmployees.ConnectionString
            connection2 = New Data.SqlClient.SqlConnection(connetionString)
            Dim strselect As String



            strselect = "select SS_OccurrenceVarianceReportingLetters.ID as RequestSerial,SS_OccurrenceVarianceReportingLetters.EmployeeID,hrs_employees.Code as EmpCode," & EmpName & " as EmployeeName ,Convert(date,RequestDate) as RequestDate,SS_OccurrenceVarianceReportingLetters.Remarks, " & Position & " As Position , " & Department & " As Department FROM            dbo.SS_OccurrenceVarianceReportingLetters INNER JOIN dbo.hrs_Employees ON dbo.SS_OccurrenceVarianceReportingLetters.EmployeeID = dbo.hrs_Employees.ID INNER JOIN dbo.hrs_Contracts ON dbo.hrs_Employees.ID = dbo.hrs_Contracts.EmployeeID and( hrs_Contracts.EndDate>=getdate() or hrs_Contracts.EndDate is null)  INNER JOIN dbo.hrs_Positions ON dbo.hrs_Contracts.PositionID = dbo.hrs_Positions.ID INNER JOIN dbo.sys_Departments ON dbo.hrs_Employees.DepartmentID = dbo.sys_Departments.ID   where  SS_OccurrenceVarianceReportingLetters.ID=" & RequestSerial & ""
            command2 = New Data.SqlClient.SqlCommand(strselect, connection2)
            adapter2.SelectCommand = command2
            adapter2.Fill(DS2, "Table1")
            adapter2.Dispose()
            command2.Dispose()
            connection2.Close()

            Dim RequesterID As Integer = Request.QueryString.Item("EmployeeID")
            If DS2.Tables(0).Rows.Count <= 0 Then

                Dim OntractEndDatestr As String = "Select EndDate from hrs_contracts where EmployeeID=" & RequesterID & ""

                Dim EndDate As DateTime = CDate(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, OntractEndDatestr))

                If EndDate <= DateTime.Now Then

                    Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)



                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry Can not proceed your Action because this Employee contract has been ended / عفوا لايمكن تسجيل الاجراء بسبب انتهاء عقد الموظف   "))

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)





                End If

            Else
                txtEmployee.Text = DS2.Tables(0).Rows(0)("EmpCode").ToString()
            txtDescEnglishName.Text = DS2.Tables(0).Rows(0)("EmployeeName").ToString()
            TxtPosition.Text = DS2.Tables(0).Rows(0)("Position").ToString()
            TxtDepartment.Text = DS2.Tables(0).Rows(0)("Department").ToString()
            TxtRequestSerial.Text = DS2.Tables(0).Rows(0)("RequestSerial").ToString()
            txtRequestDate.Text = CDate(DS2.Tables(0).Rows(0)("RequestDate").ToString()).ToShortDateString()
            TxtRequestRemarks.Text = DS2.Tables(0).Rows(0)("Remarks").ToString()
            End If
        Catch ex As Exception
            Page.Session.Add("ErrorValue", ex)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click

        Dim ClsEmployees As New Clshrs_Employees(Page)

        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        If ddlAction.SelectedValue > 0 Then



            Dim RequestSerial As Integer = Request.QueryString.Item("RequestSerial")
            Dim ConfigID As Integer = Request.QueryString.Item("ConfigID")
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")
            WebHandler.GetCookies(Page, "UserID", User)
            Dim SqlCommand As Data.SqlClient.SqlCommand
            Dim UpdateCommand As String = ""
            UpdateCommand = "update SS_RequestActions set  seen=1 , ActionID=" & ddlAction.SelectedValue & " ,ActionDate= GETDATE(),ActionRemarks='" + TxtRemarks.Text + "'  where ConfigID=" & ConfigID & " And FormCode='SS_001918' and RequestSerial=" & RequestSerial & " and SS_EmployeeID=" & ClsEmployees.ID & ""
            SqlCommand = New SqlClient.SqlCommand
            SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
            SqlCommand.CommandType = CommandType.Text
            SqlCommand.CommandText = UpdateCommand
            SqlCommand.Connection.Open()
            SqlCommand.ExecuteNonQuery()
            SqlCommand.Connection.Close()

            Dim clsEmp As New Clshrs_Employees(Page)
            clsEmp.Find("Code='" & _sys_User.Code & "'")
            Dim actionIdSql As String
            actionIdSql = "SELECT [ActionSerial] FROM [dbo].[SS_RequestActions]  where ConfigID=" & ConfigID & " and RequestSerial=" & RequestSerial & " and SS_EmployeeID=" & clsEmp.ID & ""
            Dim actionSerial As String
            actionSerial = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, actionIdSql)

            '============Get ConfigData======================
            If ddlAction.SelectedValue = 2 Then   'رفض

                Dim SqlCommandRank As Data.SqlClient.SqlCommand
                Dim UpdateCommandRank As String = ""
                UpdateCommandRank = "UPDATE SS_OccurrenceVarianceReportingLetters SET [RequestStautsTypeID] = 2 WHERE ID=" & RequestSerial & ""
                SqlCommandRank = New SqlClient.SqlCommand
                SqlCommandRank.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                SqlCommandRank.CommandType = CommandType.Text
                SqlCommandRank.CommandText = UpdateCommandRank
                SqlCommandRank.Connection.Open()
                SqlCommandRank.ExecuteNonQuery()
                SqlCommandRank.Connection.Close()

                Dim ConfigCommand As String = "select * from SS_Configuration where ID=" & ConfigID & ""
                Dim adapter As New Data.SqlClient.SqlDataAdapter
                Dim dsconfig As New Data.DataSet()
                Dim connection As Data.SqlClient.SqlConnection
                connection = New Data.SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                Dim command As Data.SqlClient.SqlCommand
                command = New Data.SqlClient.SqlCommand(ConfigCommand, connection)
                adapter.SelectCommand = command
                adapter.Fill(dsconfig)
                connection.Close()

                Dim dsRank As New Data.DataSet()
                If dsconfig.Tables(0).Rows.Count > 0 Then
                    If CBool(dsconfig.Tables(0).Rows(0)("ApplyForAll")) Then
                        _sys_User.Find("ID = '" & User & "'")
                        ClsEmployees.Find("Code='" & _sys_User.Code & "'")

                        Dim SqlCommand2 As Data.SqlClient.SqlCommand
                        Dim UpdateCommand2 As String = "update SS_RequestActions set  seen=1 , IsHidden=1 where ConfigID=" & ConfigID & " and RequestSerial=" & RequestSerial & " and SS_EmployeeID <>" & ClsEmployees.ID & ""
                        SqlCommand2 = New SqlClient.SqlCommand
                        SqlCommand2.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                        SqlCommand2.CommandType = CommandType.Text
                        SqlCommand2.CommandText = UpdateCommand2
                        SqlCommand2.Connection.Open()
                        SqlCommand2.ExecuteNonQuery()
                        SqlCommand2.Connection.Close()



                    End If


                    If CBool(dsconfig.Tables(0).Rows(0)("IsFinal")) Then
                        '===Insert Vacation Tranaction
                        'If SaveVacation() Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Save Done !/!تم الحفظ"))
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseWindow()", True)

                    End If



                End If

                ClsEmployees.SendEmail("frmOccurrenceVarianceReportingLettersAction", Me.Page, 1, "SS_RequestActions", actionSerial)
                ClsEmployees.SendEmail("SSRequestActions", Me.Page, 1, "SS_RequestActions", actionSerial)
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Save Done !/!تم الحفظ"))
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseWindow()", True)
            End If

            If ddlAction.SelectedValue = 1 Then

                Dim SqlCommandRank1 As Data.SqlClient.SqlCommand
                Dim UpdateCommandRank1 As String = ""
                UpdateCommandRank1 = "UPDATE SS_OccurrenceVarianceReportingLetters SET [RequestStautsTypeID] = 4 WHERE ID=" & RequestSerial & ""
                SqlCommandRank1 = New SqlClient.SqlCommand
                SqlCommandRank1.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                SqlCommandRank1.CommandType = CommandType.Text
                SqlCommandRank1.CommandText = UpdateCommandRank1
                SqlCommandRank1.Connection.Open()
                SqlCommandRank1.ExecuteNonQuery()
                SqlCommandRank1.Connection.Close()

                Dim ConfigCommand As String = "select * from SS_Configuration where ID=" & ConfigID & ""
                Dim adapter As New Data.SqlClient.SqlDataAdapter
                Dim dsconfig As New Data.DataSet()
                Dim connection As Data.SqlClient.SqlConnection
                connection = New Data.SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                Dim command2 As Data.SqlClient.SqlCommand
                Dim command As Data.SqlClient.SqlCommand
                Dim NextRank As Integer
                command = New Data.SqlClient.SqlCommand(ConfigCommand, connection)
                adapter.SelectCommand = command
                adapter.Fill(dsconfig)
                connection.Close()
                Dim dsRank As New Data.DataSet()
                If dsconfig.Tables(0).Rows.Count > 0 Then

                    If CBool(dsconfig.Tables(0).Rows(0)("ApplyForAll")) Then
                        _sys_User.Find("ID = '" & User & "'")
                        ClsEmployees.Find("Code='" & _sys_User.Code & "'")

                        Dim SqlCommand2 As Data.SqlClient.SqlCommand
                        Dim UpdateCommand2 As String = "update SS_RequestActions set  seen=1 , IsHidden=1 where ConfigID=" & ConfigID & " and RequestSerial=" & RequestSerial & " and SS_EmployeeID <> " & ClsEmployees.ID & " "
                        SqlCommand2 = New SqlClient.SqlCommand
                        SqlCommand2.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                        SqlCommand2.CommandType = CommandType.Text
                        SqlCommand2.CommandText = UpdateCommand2
                        SqlCommand2.Connection.Open()
                        SqlCommand2.ExecuteNonQuery()
                        SqlCommand2.Connection.Close()

                    Else
                        Dim NeededactionIdSql As String
                        NeededactionIdSql = "SELECT [ActionSerial] FROM [dbo].[SS_RequestActions]  where ConfigID=" & ConfigID & " And FormCode='" & dsconfig.Tables(0).Rows(0)("FormCode") & "' and RequestSerial=" & RequestSerial & " and ActionID is null and SS_EmployeeID<>" & ClsEmployees.ID & ""
                        Dim NeededactionSerial As String
                        NeededactionSerial = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, NeededactionIdSql)

                        If Not String.IsNullOrWhiteSpace(NeededactionSerial) Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Save Done !/!تم الحفظ"))
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
                            Return
                        End If

                    End If
                    If CBool(dsconfig.Tables(0).Rows(0)("IsFinal")) Then

                        Dim SqlCommandRank As Data.SqlClient.SqlCommand
                        Dim UpdateCommandRank As String = ""
                        UpdateCommandRank = "UPDATE SS_OccurrenceVarianceReportingLetters SET [RequestStautsTypeID] = 1 WHERE ID=" & RequestSerial & ""
                        SqlCommandRank = New SqlClient.SqlCommand
                        SqlCommandRank.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                        SqlCommandRank.CommandType = CommandType.Text
                        SqlCommandRank.CommandText = UpdateCommandRank
                        SqlCommandRank.Connection.Open()
                        SqlCommandRank.ExecuteNonQuery()
                        SqlCommandRank.Connection.Close()
                        '===Insert Execuse Tranaction
                        If SaveExecuseTransAction() Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Save Done !/!تم الحفظ"))
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
                            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseWindow()", True)

                        End If

                    Else
                        NextRank = CInt(dsconfig.Tables(0).Rows(0)("Rank")) + 1
                        Dim STRNextID As String
                        STRNextID = "select * from SS_Configuration where FormCode='" & dsconfig.Tables(0).Rows(0)("FormCode") & "' and Rank=" & NextRank & ""
                        command2 = New Data.SqlClient.SqlCommand(STRNextID, connection)
                        adapter.SelectCommand = command2
                        adapter.Fill(dsRank)
                        ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                        If dsRank.Tables(0).Rows.Count > 0 Then
                            For Each Row In dsRank.Tables(0).Rows
                                'Direct Manager
                                If Row("UserTypeID") = 1 Then
                                    Dim strdirectmanager As String
                                    strdirectmanager = "select ManagerID from hrs_Employees where Code='" & txtEmployee.Text & "'"
                                    Dim DirectManagerID As String
                                    DirectManagerID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, strdirectmanager)
                                    '==================CheckDelegation===========
                                    Dim DelegatedEmpID As Integer
                                    DelegatedEmpID = CheckDelegationSchedule(DirectManagerID)
                                    If DelegatedEmpID > 0 Then

                                        DirectManagerID = DelegatedEmpID

                                    End If
                                    Dim strinsert As String
                                    strinsert = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen,ConfigID)  values(" & TxtRequestSerial.Text & " , " & DirectManagerID & ",'" & dsconfig.Tables(0).Rows(0)("FormCode") & "'," & ClsEmployees.ID & ",0," & Row("ID") & ")"
                                    SqlCommand = New SqlClient.SqlCommand
                                    SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
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
                                    Dim DsPositionEmployees As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, strempinposition)
                                    If DsPositionEmployees.Tables(0).Rows.Count > 0 Then
                                        For Each RW In DsPositionEmployees.Tables(0).Rows
                                            Dim DelegatedEmpID As Integer
                                            DelegatedEmpID = CheckDelegationSchedule(RW("EmployeeID"))
                                            If DelegatedEmpID > 0 Then

                                                RW("EmployeeID") = DelegatedEmpID

                                            End If
                                            Dim strinsert As String
                                            strinsert = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen,ConfigID)  values(" & TxtRequestSerial.Text & " , " & RW("EmployeeID") & ",'" & dsconfig.Tables(0).Rows(0)("FormCode") & "'," & ClsEmployees.ID & ",0," & Row("ID") & ")"
                                            SqlCommand = New SqlClient.SqlCommand
                                            SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
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
                                'Employee
                                If Row("UserTypeID") = 3 Then
                                    Dim DelegatedEmpID As Integer
                                    DelegatedEmpID = CheckDelegationSchedule(Row("EmployeeID"))
                                    If DelegatedEmpID > 0 Then

                                        Row("EmployeeID") = DelegatedEmpID

                                    End If
                                    Dim strinsert As String
                                    strinsert = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen,ConfigID)  values(" & TxtRequestSerial.Text & " , " & Row("EmployeeID") & ",'" & dsconfig.Tables(0).Rows(0)("FormCode") & "'," & ClsEmployees.ID & ",0," & Row("ID") & ")"
                                    SqlCommand = New SqlClient.SqlCommand
                                    SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                                    SqlCommand.CommandType = CommandType.Text
                                    SqlCommand.CommandText = strinsert
                                    SqlCommand.Connection.Open()
                                    SqlCommand.ExecuteNonQuery()
                                    SqlCommand.Connection.Close()
                                End If
                            Next
                        Else

                            Dim SqlCommandRank As Data.SqlClient.SqlCommand
                            Dim UpdateCommandRank As String = ""
                            UpdateCommandRank = "UPDATE SS_OccurrenceVarianceReportingLetters SET [RequestStautsTypeID] = 1 WHERE ID=" & RequestSerial & ""
                            SqlCommandRank = New SqlClient.SqlCommand
                            SqlCommandRank.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                            SqlCommandRank.CommandType = CommandType.Text
                            SqlCommandRank.CommandText = UpdateCommandRank
                            SqlCommandRank.Connection.Open()
                            SqlCommandRank.ExecuteNonQuery()
                            SqlCommandRank.Connection.Close()
                        End If
                    End If


                End If

                ClsEmployees.SendEmail("frmOccurrenceVarianceReportingLettersAction", Me.Page, 1, "SS_RequestActions", actionSerial)
                ClsEmployees.SendEmail("SSRequestActions", Me.Page, 1, "SS_RequestActions", actionSerial)
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Save Done !/!تم الحفظ"))
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseWindow()", True)



            End If
            If ddlAction.SelectedValue = 3 Then
                If txtDelegated.Text = "" Then

                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Please Select Delegated Employee / عفوا لابد من تحديد الموظف المفوض اليه "))
                    txtDelegated.Focus()
                    Return

                End If

                Dim SqlCommandRank1 As Data.SqlClient.SqlCommand
                Dim UpdateCommandRank1 As String = ""
                UpdateCommandRank1 = "UPDATE SS_OccurrenceVarianceReportingLetters SET [RequestStautsTypeID] = 4 WHERE ID=" & RequestSerial & ""
                SqlCommandRank1 = New SqlClient.SqlCommand
                SqlCommandRank1.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                SqlCommandRank1.CommandType = CommandType.Text
                SqlCommandRank1.CommandText = UpdateCommandRank1
                SqlCommandRank1.Connection.Open()
                SqlCommandRank1.ExecuteNonQuery()
                SqlCommandRank1.Connection.Close()

                Dim ConfigCommand As String = "select * from SS_Configuration where ID=" & ConfigID & ""
                Dim adapter As New Data.SqlClient.SqlDataAdapter
                Dim dsconfig As New Data.DataSet()
                Dim connection As Data.SqlClient.SqlConnection
                connection = New Data.SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                Dim command2 As Data.SqlClient.SqlCommand
                Dim command As Data.SqlClient.SqlCommand

                ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                command = New Data.SqlClient.SqlCommand(ConfigCommand, connection)
                adapter.SelectCommand = command
                adapter.Fill(dsconfig)
                connection.Close()
                Dim dsRank As New Data.DataSet()
                If dsconfig.Tables(0).Rows.Count > 0 Then

                    If CBool(dsconfig.Tables(0).Rows(0)("ApplyForAll")) Then

                        _sys_User.Find("ID = '" & User & "'")
                        ClsEmployees.Find("Code='" & _sys_User.Code & "'")
                        Dim SqlCommand2 As Data.SqlClient.SqlCommand
                        Dim UpdateCommand2 As String = "update SS_RequestActions set  seen=1 , IsHidden=1 where ConfigID=" & ConfigID & " and RequestSerial=" & RequestSerial & " and SS_EmployeeID <> " & ClsEmployees.ID & " "
                        SqlCommand2 = New SqlClient.SqlCommand
                        SqlCommand2.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                        SqlCommand2.CommandType = CommandType.Text
                        SqlCommand2.CommandText = UpdateCommand2
                        SqlCommand2.Connection.Open()
                        SqlCommand2.ExecuteNonQuery()
                        SqlCommand2.Connection.Close()



                    End If

                    ClsEmployees.Find("Code='" & txtEmployee.Text & "'")

                    Dim strinsert As String
                    Dim clsDelegatedEmp As New Clshrs_Employees(Page)
                    clsDelegatedEmp.Find("Code='" & txtDelegated.Text & "'")
                    strinsert = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen,ConfigID)  values(" & TxtRequestSerial.Text & " , " & clsDelegatedEmp.ID & ",'" & dsconfig.Tables(0).Rows(0)("FormCode") & "'," & ClsEmployees.ID & ",0," & dsconfig.Tables(0).Rows(0)("ID") & ")"
                    SqlCommand = New SqlClient.SqlCommand
                    SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                    SqlCommand.CommandType = CommandType.Text
                    SqlCommand.CommandText = strinsert
                    SqlCommand.Connection.Open()
                    SqlCommand.ExecuteNonQuery()
                    SqlCommand.Connection.Close()


                End If

                ClsEmployees.SendEmail("frmOccurrenceVarianceReportingLettersAction", Me.Page, 1, "SS_RequestActions", actionSerial)
                ClsEmployees.SendEmail("SSRequestActions", Me.Page, 1, "SS_RequestActions", actionSerial)
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Save Done !/!تم الحفظ"))
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseWindow()", True)



            End If
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Save Done !/!تم الحفظ"))
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Sorry...You Have to Select Action Or Close Window  !/!عفوا لابد من اختيار اجراء او اغلاق النافذة"))

        End If



    End Sub

    Public Function CheckDelegationSchedule(EmpID As Integer) As Integer
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim StrGetDelegation As String
        StrGetDelegation = "select isnull(DelegatedEmployeeID,0) as DelegatedEmployeeID from SS_DelegationSChedule inner join SS_DelegationSCheduleRequests on SS_DelegationSCheduleRequests.ScheduleId=SS_DelegationSChedule.ID where DelegatorEmployeeID =" & EmpID & " and GetDate() >=FromDate and GetDate() <= Todate and SS_DelegationSCheduleRequests.RequestTypeId='SS_001918' and  (SS_DelegationSChedule.IsCanceled=0 OR SS_DelegationSChedule.IsCanceled is null Or SS_DelegationSChedule.CancelDate> GetDate() )union all select 0 as DelegatedEmployeeID "
        Dim DelegatedEmpID As Integer
        DelegatedEmpID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, StrGetDelegation)

        Return DelegatedEmpID

    End Function

#End Region

#Region "Private Function"
    Private Function SaveExecuseTransAction() As Boolean
        'AssignValues()
        'Dim ClsEmployeesExcuses As New Clshrs_EmployeesExcuses(Page)

        'ClsEmployeesExcuses.Save()
        Return True
    End Function

    Private Function AssignValues() As Boolean
        Try
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_Employees(Page)

            ClsEmployees.Find("Code='" & _sys_User.Code & "'")
            Dim EmpID As String = ClsEmployees.ID
            'Dim ClsEmployeesExcuses As New Clshrs_EmployeesExcuses(Page)
            'Dim ExecuesShift As Integer
            'If TxtExecuseShift.Text = "وردية اولي" Then
            '    ExecuesShift = 1
            'Else
            '    ExecuesShift = 2
            'End If
            'With ClsEmployeesExcuses
            '    .EmployeeID = EmpID
            '    .ExcuseDate = txtExecuseDate.Text
            '    .ExcuseHours = TxtExecuseTime.Text
            '    .ExcuseTarget = TxtExecuseReason.Text
            '    .ExcuseType = txtExecuseType.Text
            '    .Shift = ExecuesShift

            'End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub ddlAction_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlAction.SelectedIndexChanged
        ' Cast the sender to a DropDownList
        'Dim ddl As DropDownList = CType(sender, DropDownList)

        '' Get the selected value
        'Dim selectedValue As String = ddl.SelectedValue

        '' Perform your logic here
        'lblMessage.Text = "You selected: " & selectedValue
        If ddlAction.SelectedValue = 3 Then
            lblDelegated.Visible = True
            txtDelegated.Visible = True
            btnDelegatedSearch.Visible = True
            txtDelegatedName.Visible = True
        Else
            lblDelegated.Visible = False
            txtDelegated.Visible = False
            btnDelegatedSearch.Visible = False
            txtDelegatedName.Visible = False
        End If
    End Sub
    Protected Sub txtDelegated_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDelegated.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtDelegated.Text) Then
                Dim ClsEmployees As Clshrs_Employees
                ClsEmployees = New Clshrs_Employees(Page)
                Dim EmpName As String
                If ProfileCls.CurrentLanguage = "Ar" Then
                    EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "

                Else
                    EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,0) "

                End If
                Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

                Dim DS2 As New Data.DataSet()
                Dim connetionString2 As String
                Dim connection2 As Data.SqlClient.SqlConnection
                Dim command2 As Data.SqlClient.SqlCommand
                Dim adapter2 As New Data.SqlClient.SqlDataAdapter
                connetionString2 = ClsEmployees.ConnectionString
                connection2 = New Data.SqlClient.SqlConnection(connetionString2)
                Dim strselect As String
                strselect = "select " & EmpName & "  FROM  Hrs_Employees where Code='" & txtDelegated.Text & "'"
                'command2 = New Data.SqlClient.SqlCommand(strselect, connection2)

                Dim AlternativeName As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, strselect)
                ClsEmployees.Find("Code='" & txtDelegated.Text & "'")
                If ClsEmployees.ID > 0 Then

                    txtDelegatedName.Text = AlternativeName
                Else
                    txtDelegatedName.Text = ""
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Sorry there is no employee with this code !/!عفوا لا يوجد موظف مسجل بهذا الكود"))

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

End Class
