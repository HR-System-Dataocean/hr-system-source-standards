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
        Dim RequestSerial As Integer = Request.QueryString.Item("RequestSerial")
        Dim RequestType As Integer = Request.QueryString.Item("RequestType")
        Dim ConfigID As Integer = Request.QueryString.Item("ConfigID")

        Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)

        Dim clsEmployees As New Clshrs_Employees(Page)

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



        Dim ClsVacationsTypes As New Clshrs_VacationsTypes(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler

        If Not IsPostBack Then
            ClsVacationTypes.GetDropDownList(DdlVacationType, False)
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
            txtEmployee.Enabled = False
            TxtRequestSerial.Enabled = False
            txtContactNo.Enabled = False
            txtRequestDate.Enabled = False
            txtDescEnglishName.Enabled = False
            DdlVacationType.Enabled = False
            txtStartDate.Enabled = False
            txtEndDate.Enabled = False
            txtTotalVal.Enabled = False
            txtRemainVal.Enabled = False
            TxtPosition.Enabled = False
            TxtDepartment.Enabled = False
            TxtAlternativeEmpName.Enabled = False
            TxtRemarks.Enabled = False
            txtRequestType.Enabled = False
            If CanEdit Then

                txtAlternativeUser.Enabled = True
            Else

                txtAlternativeUser.Enabled = False

            End If
            FillEmployeeVacations()

            Dim ClsObjects As New Clssys_Objects(Page)
            Dim ClsSearchs As New Clssys_Searchs(Page)
            Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
            ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language = ""javascript"">IntializeDataChanged()</script>")
            ClsObjects.Find(" Code='" & clsEmployees.Table.Trim & "'")
            ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
            Dim csSearchID As Integer
            csSearchID = ClsSearchs.ID
            Dim IntDimension As Integer = 510
            Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtAlternativeUser.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtAlternativeUser.ClientID & "'"
            btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            btnDelete.Visible = False

            UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtDelegated.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtDelegated.ClientID & "'"
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
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Try

            Dim RequestCode As String = Request.QueryString.Item("FormCode")
            Dim Type As String = Request.QueryString.Item("Type")
            Dim RequestSerial As Integer = Request.QueryString.Item("RequestSerial")
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler

            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_Employees(Page)
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")
            Dim SelectRequestType As String = ""
            Dim RequestType As String = ""
            If ProfileCls.CurrentLanguage = "Ar" Then
                SelectRequestType = "RequestArbName"
            Else
                SelectRequestType = "RequestEngName"

            End If
            'Fill Request type
            Dim requesttypestr As String
            requesttypestr = "Select " & SelectRequestType & " from SS_RequestTypes where RequestCode='" & RequestCode & "'"
            RequestType = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, requesttypestr)
            txtRequestType.Text = RequestType


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

            ' 
            Dim str1 As String
            str1 = "select " & EmpName & " as EmployeeName,( case when " & ActionName & " is not null then " & ActionName & " else 'Pending ...' end) As Action,ConfirmedNoOfdays ,convert(varchar, ActionDate,103) as ActionDate,ActionRemarks  from SS_VacationRequest join SS_RequestActions on SS_VacationRequest.ID=SS_RequestActions.RequestSerial and SS_VacationRequest.EmployeeID=SS_RequestActions.EmployeeID join hrs_Employees on hrs_Employees.ID= SS_RequestActions.SS_EmployeeID left join SS_UserActions on SS_RequestActions.ActionID=SS_UserActions.ID where RequestSerial=" & RequestSerial & " and SS_RequestActions.FormCode='" & RequestCode & "' And ( SS_RequestActions.IsHidden is null or SS_RequestActions.IsHidden=0 ) "

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
            Dim connetionString2 As String
            Dim connection2 As Data.SqlClient.SqlConnection
            Dim command2 As Data.SqlClient.SqlCommand
            Dim adapter2 As New Data.SqlClient.SqlDataAdapter
            connetionString2 = ClsEmployees.ConnectionString
            connection2 = New Data.SqlClient.SqlConnection(connetionString2)
            Dim strselect As String
            strselect = "select SS_VacationRequest.ID as RequestSerial,SS_VacationRequest.EmployeeID ,hrs_Employees.Code as EmployeeCode ," & EmpName & " as EmployeeName,alt.Code as alternativecode,alt.arbname+' '+alt.FatherArbName+' '+alt.GrandArbName+' '+alt.FamilyArbName as AlternativeEmployeeName ,hrs_VacationsTypes.ID as VacationID,hrs_VacationsTypes.EngName as VacationEngName , hrs_VacationsTypes.ArbName as VacationArabName,Convert(date,RequestDate) as RequestDate,Convert(date,SS_VacationRequest.StartDate) as StartDate,Convert(date,SS_VacationRequest.EndDate) as EndDate,TotalBalance,NoOfDays,ContactNo,AlternativeUser,SS_VacationRequest.Remarks , " & Position & " As Position ,  " & Department & " As Department FROM            dbo.SS_VacationRequest INNER JOIN dbo.hrs_VacationsTypes ON dbo.SS_VacationRequest.VacationTypeID = dbo.hrs_VacationsTypes.ID LEFT OUTER JOIN dbo.hrs_Employees AS alt ON dbo.SS_VacationRequest.AlternativeUser = alt.ID INNER JOIN dbo.hrs_Employees ON dbo.hrs_Employees.ID = dbo.SS_VacationRequest.EmployeeID INNER JOIN  dbo.hrs_Contracts ON dbo.hrs_Employees.ID = dbo.hrs_Contracts.EmployeeID and( hrs_Contracts.EndDate>=getdate() or hrs_Contracts.EndDate is null) INNER JOIN dbo.hrs_Positions ON dbo.hrs_Contracts.PositionID = dbo.hrs_Positions.ID INNER JOIN dbo.sys_Departments ON dbo.hrs_Employees.DepartmentID = dbo.sys_Departments.ID  where SS_VacationRequest.ID=" & RequestSerial & ""
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

                TxtRequestSerial.Text = DS2.Tables(0).Rows(0)("RequestSerial").ToString()
            txtEmployee.Text = DS2.Tables(0).Rows(0)("EmployeeCode").ToString()
            txtDescEnglishName.Text = DS2.Tables(0).Rows(0)("EmployeeName").ToString()
            TxtPosition.Text = DS2.Tables(0).Rows(0)("Position").ToString()
            TxtDepartment.Text = DS2.Tables(0).Rows(0)("Department").ToString()
            txtAlternativeUser.Text = DS2.Tables(0).Rows(0)("alternativecode").ToString()
            txtContactNo.Text = DS2.Tables(0).Rows(0)("ContactNo").ToString()
            txtRequestDate.Text = CDate(DS2.Tables(0).Rows(0)("RequestDate").ToString()).ToShortDateString()
            txtStartDate.Text = CDate(DS2.Tables(0).Rows(0)("StartDate").ToString()).ToShortDateString()
            txtEndDate.Text = CDate(DS2.Tables(0).Rows(0)("EndDate").ToString()).ToShortDateString()
            txtTotalVal.Text = DS2.Tables(0).Rows(0)("TotalBalance").ToString()
            txtRemainVal.Text = DS2.Tables(0).Rows(0)("NoOfDays").ToString()
            txtContactNo.Text = DS2.Tables(0).Rows(0)("ContactNo").ToString()
            TxtAlternativeEmpName.Text = DS2.Tables(0).Rows(0)("AlternativeEmployeeName").ToString()
            txtConfirmedDays.Text = DS2.Tables(0).Rows(0)("NoOfDays").ToString()
            DdlVacationType.SelectedValue = DS2.Tables(0).Rows(0)("VacationID").ToString()
            TxtRemarks.Text = DS2.Tables(0).Rows(0)("Remarks").ToString()
            If CInt(DS2.Tables(0).Rows(0)("VacationID").ToString()) > 1 Then
                txtAlternativeUser.Visible = False
                lblWebDateChooser3.Visible = False
                btnSearchCode.Visible = False
                TxtAlternativeEmpName.Visible = False
            End If
            End If
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    'Protected Sub btnShowAttachment_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles BtnShowAttachment.Click
    '    'Dim ClsObjects As New Clssys_Objects(Page)
    '    'Dim ClsSearchs As New Clssys_Searchs(Page)
    '    'Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
    '    'ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language = ""javascript"">IntializeDataChanged()</script>")
    '    'ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'")
    '    'ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
    '    'Dim csSearchID As Integer
    '    'csSearchID = ClsSearchs.ID
    '    'Dim IntDimension As Integer = 510
    '    Dim RequestSerial As Integer = Request.QueryString.Item("RequestSerial")
    '    Dim UrlString As String = "frmAnnualVacationDocuments.aspx?TB=hrs_EmployeesVacations&SV=" & RequestSerial & " , 495, 800"

    '    BtnShowAttachment.ClientSideEvents.Click = "OpenModal(" & UrlString & ")"
    '    btnDelete.Visible = False

    'End Sub
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
            'ClsEmployees.Find("Code='" & _sys_User.Code & "'")
            WebHandler.GetCookies(Page, "UserID", User)

            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")

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
                UpdateCommandRank = "UPDATE SS_VacationRequest SET [RequestStautsTypeID] = 2 WHERE ID=" & RequestSerial & ""
                SqlCommandRank = New SqlClient.SqlCommand
                SqlCommandRank.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                SqlCommandRank.CommandType = CommandType.Text
                SqlCommandRank.CommandText = UpdateCommandRank
                SqlCommandRank.Connection.Open()
                SqlCommandRank.ExecuteNonQuery()
                SqlCommandRank.Connection.Close()

                Dim SqlCommand As Data.SqlClient.SqlCommand
                Dim UpdateCommand As String = "update SS_RequestActions set  seen=1 , ActionID=" & ddlAction.SelectedValue & " , ConfirmedNoOfDays=" & txtConfirmedDays.Text & ",ActionDate= GETDATE() , ActionRemarks='" & txtActionRemarks.Text & "' where ConfigID=" & ConfigID & " and RequestSerial=" & RequestSerial & " and SS_EmployeeID=" & clsEmp.ID & ""
                SqlCommand = New SqlClient.SqlCommand
                SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                SqlCommand.CommandType = CommandType.Text
                SqlCommand.CommandText = UpdateCommand
                SqlCommand.Connection.Open()
                SqlCommand.ExecuteNonQuery()
                SqlCommand.Connection.Close()


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

                ClsEmployees.SendEmail("FrmAnnualVacationRequestAction", Me.Page, 1, "V_AnnualvacationAction_EmialToRequester", actionSerial)
                ClsEmployees.SendEmail("SSRequestActions", Me.Page, 1, "V_AnnualvacationAction_EmialToRequester", actionSerial)
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Save Done !/!تم الحفظ"))
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseWindow()", True)
            End If



            If ddlAction.SelectedValue = 1 Then

                Dim SqlCommandRank1 As Data.SqlClient.SqlCommand
                Dim UpdateCommandRank1 As String = ""
                UpdateCommandRank1 = "UPDATE SS_VacationRequest SET [RequestStautsTypeID] = 4 WHERE ID=" & RequestSerial & ""
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

                Dim dat_ACTUALL_RETRUN As Date = CDate(txtEndDate.Text)
                Dim dat_NEW_RETURN As Date = CDate(txtEndDate.Text)
                Dim ClsEmployeeVacation As New Clshrs_EmployeesVacations(Page)
                ClsEmployeeVacation.Find("EmployeeID=" & ClsEmployees.ID)

                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
                Dim ClsEmployeesVacations2 = New Clshrs_EmployeesVacations(Page)
                ClsEmployees.Find("Code='" & txtEmployee.Text & "'")

                If ClsEmployeesVacations2.GetEmployeeLastVacation(ClsEmployees.ID) Then
                    If IsDBNull(ClsEmployeesVacations2.ActualEndDate) Or ClsEmployeesVacations2.ActualEndDate = "01/01/0001" Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a vacation without return date /يوجد اجازة سابقة من غير تاريخ رجوع  "))
                        Return
                    End If
                End If
                Dim DteVacationDate As Date = txtStartDate.Text
                Dim dsPrevVac As Data.DataSet = ClsEmployeeVacation.GetEmployeeVacationPerYear(CDate(txtStartDate.Text).Year, ClsEmployees.ID, -1)
                If dsPrevVac.Tables(0).Rows.Count > 0 Then
                    For index = 0 To dsPrevVac.Tables(0).Rows.Count - 1
                        If (dsPrevVac.Tables(0).Rows(index).Item("ActualEndDate") >= txtStartDate.Text And dsPrevVac.Tables(0).Rows(index).Item("ActualStartDate") <= txtStartDate.Text) Or (dsPrevVac.Tables(0).Rows(index).Item("ActualEndDate") >= dat_NEW_RETURN And dsPrevVac.Tables(0).Rows(index).Item("ActualStartDate") <= dat_NEW_RETURN) Or (dat_NEW_RETURN >= dsPrevVac.Tables(0).Rows(index).Item("ActualStartDate") And txtStartDate.Text <= dsPrevVac.Tables(0).Rows(index).Item("ActualStartDate")) Or (dat_NEW_RETURN >= dsPrevVac.Tables(0).Rows(index).Item("ActualEndDate") And txtStartDate.Text <= dsPrevVac.Tables(0).Rows(index).Item("ActualEndDate")) Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a vacation in this period / يوجد اجازة مسجلة بالفعل فى هذه الفترة  "))
                            Return
                        End If
                    Next
                End If
                'If ClsEmployeesVacations2.ActualEndDate > txtStartDate.Text Then
                '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a vacation with end date greater than the inserted date /يوجد اجازة سابقة بتاريخ رجوع أكبر من التاريخ المدخل  "))
                '    Return

                'End If
                Dim ClsEmployees2 As New Clshrs_Employees(Page)
                ClsEmployees2.Find("Code='" & _sys_User.Code & "'")

                'old update here
                Dim dsRank As New Data.DataSet()



                If dsconfig.Tables(0).Rows.Count > 0 Then
                    If CBool(dsconfig.Tables(0).Rows(0)("ApplyForAll")) Then

                        '_sys_User.Find("ID = '" & User & "'")
                        'ClsEmployees.Find("Code='" & _sys_User.Code & "'")
                        Dim SqlCommand2 As Data.SqlClient.SqlCommand
                        Dim UpdateCommand2 As String = "update SS_RequestActions set  seen=1 , IsHidden=1 where ConfigID=" & ConfigID & " and RequestSerial=" & RequestSerial & " and SS_EmployeeID <>" & ClsEmployees2.ID & ""
                        SqlCommand2 = New SqlClient.SqlCommand
                        SqlCommand2.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                        SqlCommand2.CommandType = CommandType.Text
                        SqlCommand2.CommandText = UpdateCommand2
                        SqlCommand2.Connection.Open()
                        SqlCommand2.ExecuteNonQuery()
                        SqlCommand2.Connection.Close()
                        Dim SqlCommandU As Data.SqlClient.SqlCommand
                        Dim UpdateCommand As String = "update SS_RequestActions set  seen=1 , ActionID=" & ddlAction.SelectedValue & " , ConfirmedNoOfDays=" & txtConfirmedDays.Text & ",ActionDate= GETDATE() , ActionRemarks='" & txtActionRemarks.Text & "' where ConfigID=" & ConfigID & " and RequestSerial=" & RequestSerial & " and SS_EmployeeID=" & ClsEmployees2.ID & " And ActionID is null"
                        SqlCommandU = New SqlClient.SqlCommand
                        SqlCommandU.Connection = New SqlClient.SqlConnection(ClsEmployees2.ConnectionString)
                        SqlCommandU.CommandType = CommandType.Text
                        SqlCommandU.CommandText = UpdateCommand
                        SqlCommandU.Connection.Open()
                        SqlCommandU.ExecuteNonQuery()
                        SqlCommandU.Connection.Close()
                    Else
                        Dim NeededactionIdSql As String
                        NeededactionIdSql = "SELECT count(ActionSerial) as myCount FROM [dbo].[SS_RequestActions]  where ConfigID=" & ConfigID & " And FormCode='" & dsconfig.Tables(0).Rows(0)("FormCode") & "' and RequestSerial=" & RequestSerial & " and ActionID is null and SS_EmployeeID<>" & ClsEmployees.ID & ""
                        Dim NeededactionSerial As String
                        NeededactionSerial = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, NeededactionIdSql)
                        Dim SqlCommandU As Data.SqlClient.SqlCommand
                        Dim UpdateCommand As String = "update SS_RequestActions set  seen=1 , ActionID=" & ddlAction.SelectedValue & " , ConfirmedNoOfDays=" & txtConfirmedDays.Text & ",ActionDate= GETDATE() , ActionRemarks='" & txtActionRemarks.Text & "' where ConfigID=" & ConfigID & " and RequestSerial=" & RequestSerial & " and SS_EmployeeID=" & ClsEmployees2.ID & " And ActionID is null"
                        SqlCommandU = New SqlClient.SqlCommand
                        SqlCommandU.Connection = New SqlClient.SqlConnection(ClsEmployees2.ConnectionString)
                        SqlCommandU.CommandType = CommandType.Text
                        SqlCommandU.CommandText = UpdateCommand
                        SqlCommandU.Connection.Open()
                        SqlCommandU.ExecuteNonQuery()
                        SqlCommandU.Connection.Close()
                        If Not String.IsNullOrWhiteSpace(NeededactionSerial) And NeededactionSerial > 1 Then

                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Save Done !/!تم الحفظ"))
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
                            Return
                        End If

                    End If


                    If CBool(dsconfig.Tables(0).Rows(0)("IsFinal")) Then

                        Dim SqlCommandRank As Data.SqlClient.SqlCommand
                        Dim UpdateCommandRank As String = ""
                        UpdateCommandRank = "UPDATE SS_VacationRequest SET [RequestStautsTypeID] = 1 WHERE ID=" & RequestSerial & ""
                        SqlCommandRank = New SqlClient.SqlCommand
                        SqlCommandRank.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                        SqlCommandRank.CommandType = CommandType.Text
                        SqlCommandRank.CommandText = UpdateCommandRank
                        SqlCommandRank.Connection.Open()
                        SqlCommandRank.ExecuteNonQuery()
                        SqlCommandRank.Connection.Close()
                        Dim SqlCommandU As Data.SqlClient.SqlCommand
                        Dim UpdateCommand As String = "update SS_RequestActions set  seen=1 , ActionID=" & ddlAction.SelectedValue & " , ConfirmedNoOfDays=" & txtConfirmedDays.Text & ",ActionDate= GETDATE() , ActionRemarks='" & txtActionRemarks.Text & "' where ConfigID=" & ConfigID & " and RequestSerial=" & RequestSerial & " and SS_EmployeeID=" & ClsEmployees2.ID & " And ActionID is null"
                        SqlCommandU = New SqlClient.SqlCommand
                        SqlCommandU.Connection = New SqlClient.SqlConnection(ClsEmployees2.ConnectionString)
                        SqlCommandU.CommandType = CommandType.Text
                        SqlCommandU.CommandText = UpdateCommand
                        SqlCommandU.Connection.Open()
                        SqlCommandU.ExecuteNonQuery()
                        SqlCommandU.Connection.Close()
                        '===Insert Vacation Tranaction
                        If Not SaveVacation() Then
                            Return
                            'Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Save Done !/!تم الحفظ"))
                            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
                            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseWindow()", True)

                        End If

                    Else
                        NextRank = CInt(dsconfig.Tables(0).Rows(0)("Rank")) + 1
                        Dim STRNextID As String
                        STRNextID = "select * from SS_Configuration where FormCode='" & dsconfig.Tables(0).Rows(0)("FormCode") & "' and Rank=" & NextRank & ""
                        command2 = New Data.SqlClient.SqlCommand(STRNextID, connection)
                        adapter.SelectCommand = command2
                        adapter.Fill(dsRank)
                        ClsEmployees = New Clshrs_Employees(Page)
                        ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                        If ClsEmployees.ID = 0 Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry, you do not have permission for the applicant's branch...Please contact system admin  / عفوا ليس لديك صلاحية على فرع مقدم الطلب ... يرجي مراجعة مدير النظام"))
                            Return
                        End If
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
                                    DelegatedEmpID = CheckDelegationSchedule(DirectManagerID, dsconfig.Tables(0).Rows(0)("FormCode"))
                                    If DelegatedEmpID > 0 Then

                                        DirectManagerID = DelegatedEmpID

                                    End If
                                    Dim strinsert As String
                                    strinsert = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen,ConfigID)  values(" & TxtRequestSerial.Text & " , " & DirectManagerID & ",'" & dsconfig.Tables(0).Rows(0)("FormCode") & "'," & ClsEmployees.ID & ",0," & Row("ID") & ")"
                                    Dim SqlCommand = New SqlClient.SqlCommand
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
                                            DelegatedEmpID = CheckDelegationSchedule(RW("EmployeeID"), dsconfig.Tables(0).Rows(0)("FormCode"))
                                            If DelegatedEmpID > 0 Then

                                                RW("EmployeeID") = DelegatedEmpID

                                            End If
                                            Dim strinsert As String
                                            strinsert = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen,ConfigID)  values(" & TxtRequestSerial.Text & " , " & RW("EmployeeID") & ",'" & dsconfig.Tables(0).Rows(0)("FormCode") & "'," & ClsEmployees.ID & ",0," & Row("ID") & ")"
                                            Dim SqlCommand = New SqlClient.SqlCommand
                                            SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                                            SqlCommand.CommandType = CommandType.Text
                                            SqlCommand.CommandText = strinsert
                                            SqlCommand.Connection.Open()
                                            SqlCommand.ExecuteNonQuery()
                                            SqlCommand.Connection.Close()
                                        Next
                                    Else
                                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry Can not proceed your request because there are no employees in the next level ...Please contact system admin  / عفوا لايمكن تسجيل الطلب لعدم وجود موظفين في المرحلة التالية ... يرجي مراجعة مدير النظام"))
                                        Return
                                    End If
                                End If
                                'Employee
                                If Row("UserTypeID") = 3 Then
                                    Dim DelegatedEmpID As Integer
                                    DelegatedEmpID = CheckDelegationSchedule(Row("EmployeeID"), dsconfig.Tables(0).Rows(0)("FormCode"))
                                    If DelegatedEmpID > 0 Then

                                        Row("EmployeeID") = DelegatedEmpID

                                    End If
                                    Dim strinsert As String
                                    strinsert = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen,ConfigID)  values(" & TxtRequestSerial.Text & " , " & Row("EmployeeID") & ",'" & dsconfig.Tables(0).Rows(0)("FormCode") & "'," & ClsEmployees.ID & ",0," & Row("ID") & ")"
                                    Dim SqlCommand = New SqlClient.SqlCommand
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
                            UpdateCommandRank = "UPDATE SS_VacationRequest SET [RequestStautsTypeID] = 1 WHERE ID=" & RequestSerial & ""
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



                ClsEmployees.SendEmail("FrmAnnualVacationRequestAction", Me.Page, 1, "V_AnnualvacationAction_EmialToRequester", actionSerial)
                ClsEmployees.SendEmail("SSRequestActions", Me.Page, 1, "V_AnnualvacationAction_EmialToRequester", actionSerial)
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
                UpdateCommandRank1 = "UPDATE SS_VacationRequest SET [RequestStautsTypeID] = 4 WHERE ID=" & RequestSerial & ""
                SqlCommandRank1 = New SqlClient.SqlCommand
                SqlCommandRank1.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                SqlCommandRank1.CommandType = CommandType.Text
                SqlCommandRank1.CommandText = UpdateCommandRank1
                SqlCommandRank1.Connection.Open()
                SqlCommandRank1.ExecuteNonQuery()
                SqlCommandRank1.Connection.Close()

                Dim ClsEmployees2 As New Clshrs_Employees(Page)
                ClsEmployees2.Find("Code='" & _sys_User.Code & "'")
                Dim SqlCommand As Data.SqlClient.SqlCommand
                Dim UpdateCommand As String = "update SS_RequestActions set  seen=1 , ActionID=" & ddlAction.SelectedValue & " , ConfirmedNoOfDays=" & txtConfirmedDays.Text & ",ActionDate= GETDATE() , ActionRemarks='" & txtActionRemarks.Text & "' where ConfigID=" & ConfigID & " and RequestSerial=" & RequestSerial & " and SS_EmployeeID=" & ClsEmployees2.ID & ""
                SqlCommand = New SqlClient.SqlCommand
                SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                SqlCommand.CommandType = CommandType.Text
                SqlCommand.CommandText = UpdateCommand
                SqlCommand.Connection.Open()
                SqlCommand.ExecuteNonQuery()
                SqlCommand.Connection.Close()
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
                        ClsEmployees2.Find("Code='" & _sys_User.Code & "'")
                        Dim SqlCommand2 As Data.SqlClient.SqlCommand
                        Dim UpdateCommand2 As String = "update SS_RequestActions set  seen=1 , IsHidden=1 where ConfigID=" & ConfigID & " and RequestSerial=" & RequestSerial & " and SS_EmployeeID <> " & ClsEmployees2.ID & " "
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
                ClsEmployees.SendEmail("FrmAnnualVacationRequestAction", Me.Page, 1, "V_AnnualvacationAction_EmialToRequester", actionSerial)
                ClsEmployees.SendEmail("SSRequestActions", Me.Page, 1, "V_AnnualvacationAction_EmialToRequester", actionSerial)
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

    Public Function CheckDelegationSchedule(EmpID As Integer, FormCode As String) As Integer
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim StrGetDelegation As String
        StrGetDelegation = "select isnull(DelegatedEmployeeID,0) as DelegatedEmployeeID from SS_DelegationSChedule inner join SS_DelegationSCheduleRequests on SS_DelegationSCheduleRequests.ScheduleId=SS_DelegationSChedule.ID where DelegatorEmployeeID =" & EmpID & " and GetDate() >=FromDate and GetDate() <= Todate and SS_DelegationSCheduleRequests.RequestTypeId='" & FormCode & "' and  (SS_DelegationSChedule.IsCanceled=0 OR SS_DelegationSChedule.IsCanceled is null Or SS_DelegationSChedule.CancelDate> GetDate() )union all select 0 as DelegatedEmployeeID "
        Dim DelegatedEmpID As Integer
        DelegatedEmpID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, StrGetDelegation)

        Return DelegatedEmpID

    End Function
    Protected Sub txtAlternative_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAlternativeUser.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtAlternativeUser.Text) Then
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
                strselect = "select " & EmpName & "  FROM  Hrs_Employees where Code='" & txtAlternativeUser.Text & "'"
                'command2 = New Data.SqlClient.SqlCommand(strselect, connection2)

                Dim AlternativeName As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, strselect)
                ClsEmployees.Find("Code='" & txtAlternativeUser.Text & "'")
                If ClsEmployees.ID > 0 Then

                    TxtAlternativeEmpName.Text = AlternativeName
                Else
                    TxtAlternativeEmpName.Text = ""
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Sorry there is no employee with this code !/!عفوا لا يوجد موظف مسجل بهذا الكود"))

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Private Function"
    Private Function SaveVacation() As Boolean
        Try

            ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
            Dim ClsEmployeesVacations2 = New Clshrs_EmployeesVacations(Page)
            Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesVacations.ConnectionString)
            Dim ClsEmployees As New Clshrs_Employees(Page)
            Dim Cls_Contracts As New Clshrs_Contracts(Page)
            Dim FormCode As String = Request.QueryString.Item("FormCode")
            Dim dat_ACTUALL_RETRUN As Date = CDate(txtEndDate.Text)
            Dim dat_NEW_RETURN As Date = CDate(txtStartDate.Text).AddDays(CInt(txtConfirmedDays.Text))
            Dim ClsEmployeeVacation As New Clshrs_EmployeesVacations(Page)

            ClsClasses = New Clshrs_EmployeeClasses(Page)
            'Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
            'Dim ClsEmployeesVacations2 = New Clshrs_EmployeesVacations(Page)
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            ClsEmployeeVacation.Find("EmployeeID=" & ClsEmployees.ID)

            Dim intContractID As Integer = Cls_Contracts.ContractValidatoinId(ClsEmployees.ID, dat_NEW_RETURN)
                Cls_Contracts.Find(" ID =" & intContractID)
                ClsClasses.Find("ID=" & IIf(Cls_Contracts.EmployeeClassID > 0, Cls_Contracts.EmployeeClassID, 0))
            If ClsEmployeesVacations2.GetEmployeeLastVacation(ClsEmployees.ID) Then
                If IsDBNull(ClsEmployeesVacations2.ActualEndDate) Or ClsEmployeesVacations2.ActualEndDate = "01/01/0001" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a vacation without return date /يوجد اجازة سابقة من غير تاريخ رجوع  "))
                    Return False
                    Exit Function
                End If
            End If

            Dim ClsVacations As New Clshrs_VacationsTypes(Page)
            ClsVacations.Find(" ID=" & DdlVacationType.SelectedItem.Value)
            Dim strNoOfTimes As String
            strNoOfTimes = "select count(ID) from hrs_EmployeesVacations where YEAR(ActualEndDate)=" & (dat_NEW_RETURN).Year & " and VacationTypeID=" & DdlVacationType.SelectedValue & " and EmployeeID=" & ClsEmployees.ID & " and CancelDate is null"
            Dim NoOfTimes As Integer
            NoOfTimes = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsVacations.ConnectionString, Data.CommandType.Text, strNoOfTimes)
            If NoOfTimes >= ClsVacations.TimesNoInYear And ClsVacations.TimesNoInYear > 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Sorry, you have exceeded the maximum number of allowed requests for this leave type in the current calendar year. /عفوًا، تم تجاوز الحد الأقصى لعدد مرات طلب هذه الإجازة خلال السنة الميلادية "))
                Exit Function
            End If
            Dim DteVacationDate As Date = txtStartDate.Text
            Dim dsPrevVac As Data.DataSet = ClsEmployeeVacation.GetEmployeeVacationPerYear(CDate(txtStartDate.Text).Year, ClsEmployees.ID, -1)
            If dsPrevVac.Tables(0).Rows.Count > 0 Then
                For index = 0 To dsPrevVac.Tables(0).Rows.Count - 1
                    If (dsPrevVac.Tables(0).Rows(index).Item("ActualEndDate") >= txtStartDate.Text And dsPrevVac.Tables(0).Rows(index).Item("ActualStartDate") <= txtStartDate.Text) Or (dsPrevVac.Tables(0).Rows(index).Item("ActualEndDate") >= dat_NEW_RETURN And dsPrevVac.Tables(0).Rows(index).Item("ActualStartDate") <= dat_NEW_RETURN) Or (dat_NEW_RETURN >= dsPrevVac.Tables(0).Rows(index).Item("ActualStartDate") And txtStartDate.Text <= dsPrevVac.Tables(0).Rows(index).Item("ActualStartDate")) Or (dat_NEW_RETURN >= dsPrevVac.Tables(0).Rows(index).Item("ActualEndDate") And txtStartDate.Text <= dsPrevVac.Tables(0).Rows(index).Item("ActualEndDate")) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a vacation in this period / يوجد اجازة مسجلة بالفعل فى هذه الفترة  "))
                        Return False
                        Exit Function
                    End If
                Next
            End If
            'If ClsEmployeesVacations2.ActualEndDate > txtStartDate.Text Then
            '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a vacation with end date greater than the inserted date /يوجد اجازة سابقة بتاريخ رجوع أكبر من التاريخ المدخل  "))
            '    Return False
            '    Exit Function
            'End If
            'setOverDueDays(ClsVacationTypes, dat_NEW_RETURN)


            ClsEmployeesVacations.EmployeeID = ClsEmployees.ID
            ClsEmployeesVacations.VacationTypeID = DdlVacationType.SelectedValue
            ClsEmployeesVacations.ExpectedStartDate = txtStartDate.Text
            ClsEmployeesVacations.ExpectedEndDate = dat_NEW_RETURN
            ClsEmployeesVacations.ActualStartDate = txtStartDate.Text
            ClsEmployeesVacations.ActualEndDate = dat_NEW_RETURN

            ClsEmployeesVacations.ConsumDays = txtConfirmedDays.Text
            'MOusa too update 12-03-2025
            '================= get the remaining
            If ClsClasses.AdvanceBalance Then


                Dim strSql = "SELECT    ROUND( Remaining,0) FROM hrs_VacationsBalance where hrs_VacationsBalance.EndServiceDate IS NULL and ISNULL(Posted,0)=0 and BalanceTypeID=2 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & DateTime.Parse(txtStartDate.Text).ToString("yyyy-MM-dd") & "' and DueDate<='" & DateTime.Parse(txtStartDate.Text).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(txtStartDate.Text).ToString("yyyy-MM-dd") & "')"
                Dim remain As Decimal
                remain = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, strSql)

                strSql = "SELECT    ROUND( Remaining,0) FROM hrs_VacationsBalance where hrs_VacationsBalance.EndServiceDate IS NULL and ISNULL(Posted,0)=0 and BalanceTypeID<>2 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & DateTime.Parse(txtStartDate.Text).ToString("yyyy-MM-dd") & "' and DueDate<='" & DateTime.Parse(txtStartDate.Text).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(txtStartDate.Text).ToString("yyyy-MM-dd") & "')"
                Dim Balance As Decimal
                Balance = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, strSql)
                If ClsEmployeesVacations.VacationTypeID = 1 Then

                    Dim TotBalance = Balance + remain
                    ClsEmployeesVacations.TotalBalance = TotBalance
                    ClsEmployeesVacations.TotalDays = TotBalance
                    If CInt(TotBalance) > 0 Then
                        ClsEmployeesVacations.RemainingDays = Convert.ToDouble(TotBalance) - Convert.ToDouble(txtConfirmedDays.Text)
                    End If
                    '=================
                    If CInt(TotBalance) > 0 Then
                        ClsEmployeesVacations.RemainingBalance = Convert.ToDouble(TotBalance) - Convert.ToDouble(txtConfirmedDays.Text)
                    End If
                    ClsEmployeesVacations.vactiondays = txtConfirmedDays.Text
                End If
                ClsEmployeesVacations.Src = FormCode
                ClsEmployeesVacations.VacationRequestID = Convert.ToInt32(TxtRequestSerial.Text)
                If ClsEmployeesVacations.VacationTypeID = 1 Then
                    ClsVacationTypes.Find("ID=" & DdlVacationType.SelectedValue)
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
                End If
                Dim Diffe As Single = 0
                    Dim OfficialVacations As Integer = 0
                    OfficialVacations = GetOverlappingOfficialVacationDays(CDate(txtStartDate.Text), CDate(txtEndDate.Text).AddDays(-1))

                    Try

                        Diffe = (DateDiff(DateInterval.Day, Convert.ToDateTime(txtStartDate.Text), dat_NEW_RETURN))
                        'Diffe = Diffe - OfficialVacations
                    Catch ex As Exception
                        Diffe = 0
                    End Try

                If ClsEmployeesVacations.VacationTypeID = 1 Then
                    Dim str = "SELECT    ROUND( Remaining,0) FROM hrs_VacationsBalance where BalanceTypeID=2 and ISNULL(Posted,0)=0 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & ClsEmployeesVacations.ExpectedEndDate.ToString("yyyy-MM-dd") & "' and DueDate<='" & ClsEmployeesVacations.ExpectedStartDate.ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & ClsEmployeesVacations.ExpectedEndDate.ToString("yyyy-MM-dd") & "')"
                    Dim remainTrans As Decimal
                    remainTrans = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str)
                    If remainTrans = 0 Then
                        str = "SELECT    ROUND( Remaining,0) FROM hrs_VacationsBalance where BalanceTypeID=2 and ISNULL(Posted,0)=0 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & ClsEmployeesVacations.ExpectedStartDate.ToString("yyyy-MM-dd") & "' and DueDate<='" & ClsEmployeesVacations.ExpectedStartDate.ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & ClsEmployeesVacations.ExpectedEndDate.ToString("yyyy-MM-dd") & "')"
                        remainTrans = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployeesVacations.ConnectionString, Data.CommandType.Text, str)
                        If remainTrans > 0 Then
                            str = "SELECT    ExpireDate FROM hrs_VacationsBalance where BalanceTypeID=2 and ISNULL(Posted,0)=0 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & ClsEmployeesVacations.ExpectedStartDate.ToString("yyyy-MM-dd") & "' and DueDate<='" & ClsEmployeesVacations.ExpectedStartDate.ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & ClsEmployeesVacations.ExpectedEndDate.ToString("yyyy-MM-dd") & "')"
                            Dim expire As Date = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployeesVacations.ConnectionString, Data.CommandType.Text, str)
                            Dim AllowDays = (DateDiff(DateInterval.Day, ClsEmployeesVacations.ExpectedStartDate, expire))
                            If AllowDays < remainTrans Then
                                remainTrans = AllowDays
                            End If
                        End If
                    End If
                    If remainTrans > Diffe Then
                        Dim strUpdateTrans As String = "UPDATE [dbo].[hrs_VacationsBalance]  SET Consumed = Consumed+" & Diffe & " ,Remaining = Remaining - " & Diffe & " where BalanceTypeID=2 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & ClsEmployeesVacations.ExpectedStartDate.ToString("yyyy-MM-dd") & "' and DueDate<='" & ClsEmployeesVacations.ExpectedStartDate.ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & ClsEmployeesVacations.ExpectedEndDate.ToString("yyyy-MM-dd") & "')"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeesVacations.ConnectionString, Data.CommandType.Text, strUpdateTrans)
                    Else
                        If remainTrans + Balance < Diffe Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Vaction days more the current balance-review Balance Expire Date /  أيام الإجازة أكثر من الرصيد المستحق - برجاء مراجعة تاريخ صلاحية الرصيد"))
                            Return False
                            Exit Function
                        End If
                        If remainTrans > 0 Then
                            Dim strUpdateTrans As String = "UPDATE [dbo].[hrs_VacationsBalance]  SET Consumed = Consumed+" & remainTrans & " ,Remaining = 0 where BalanceTypeID=2 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & ClsEmployeesVacations.ExpectedStartDate.ToString("yyyy-MM-dd") & "' and DueDate<='" & ClsEmployeesVacations.ExpectedStartDate.ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & ClsEmployeesVacations.ExpectedEndDate.ToString("yyyy-MM-dd") & "')"
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeesVacations.ConnectionString, Data.CommandType.Text, strUpdateTrans)
                        End If
                        Dim strUpdateNew As String = "UPDATE [dbo].[hrs_VacationsBalance]  SET Consumed = Consumed+" & (Diffe - remainTrans) & " ,Remaining = Remaining-" & (Diffe - remainTrans) & " where BalanceTypeID<>2 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & ClsEmployeesVacations.ExpectedEndDate.ToString("yyyy-MM-dd") & "' and DueDate<='" & ClsEmployeesVacations.ExpectedStartDate.ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & ClsEmployeesVacations.ExpectedEndDate.ToString("yyyy-MM-dd") & "')"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeesVacations.ConnectionString, Data.CommandType.Text, strUpdateNew)
                    End If
                End If

                If ClsEmployeesVacations.VacationTypeID <> 1 Then
                        CheckVacationsOverlapping()
                    End If
                Else
                    ClsEmployeesVacations.TotalBalance = txtTotalVal.Text
                ClsEmployeesVacations.TotalDays = txtTotalVal.Text
                If CInt(txtTotalVal.Text) > 0 Then
                    ClsEmployeesVacations.RemainingDays = Convert.ToDouble(txtTotalVal.Text) - Convert.ToDouble(txtConfirmedDays.Text)
                End If

                ClsEmployeesVacations.vactiondays = txtConfirmedDays.Text
                ClsEmployeesVacations.Src = FormCode
                ClsEmployeesVacations.VacationRequestID = Convert.ToInt32(TxtRequestSerial.Text)

                ClsVacationTypes.Find("ID=" & DdlVacationType.SelectedValue)
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

            End If

            Dim recordId As Integer
            recordId = ClsEmployeesVacations.SaveVacation()
            Return True
        Catch ex As Exception
            ' Handle exception
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
    Public Function GetOverlappingOfficialVacationDays(startDate As Date, endDate As Date) As Integer
        Dim totalDays As Integer = 0
        Dim clsEmployees As New Clshrs_Employees(Page)

        Dim OfficialVacsSTR As String = "set dateformat  DMY;SELECT FromDate, ToDate FROM Hrs_OfficialVacations WHERE (FromDate <= '" & endDate & "' AND ToDate >= '" & startDate & "')"
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
    Public Function CheckVacationsOverlappingOld() As Boolean
        Dim strpreviousPeriods As String = "select * from hrs_EmployeesVacations where canceldate is null And EmployeeID= " & ClsEmployees.ID & " "
        Dim previousPeriods As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, strpreviousPeriods)

        Dim newStart As DateTime = CDate(txtStartDate.Text)
        Dim newEnd As DateTime = CDate(txtEndDate.Text).AddDays(-1)

        Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
        ClsVacationTypes.Find(" ID=" & DdlVacationType.SelectedItem.Value)

        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler

        WebHandler.GetCookies(Page, "UserID", User)

        Dim Cls_Contracts As New Clshrs_Contracts(Page)
        ClsClasses = New Clshrs_EmployeeClasses(Page)
        'Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        'Dim ClsEmployeesVacations2 = New Clshrs_EmployeesVacations(Page)

        Dim dat_NEW_RETURN As Date = CDate(txtStartDate.Text).AddDays(CInt(txtConfirmedDays.Text))
        Dim intContractID As Integer = Cls_Contracts.ContractValidatoinId(ClsEmployees.ID, dat_NEW_RETURN)
        Cls_Contracts.Find(" ID =" & intContractID)
        ClsClasses.Find("ID=" & IIf(Cls_Contracts.EmployeeClassID > 0, Cls_Contracts.EmployeeClassID, 0))

        For Each row As DataRow In previousPeriods.Tables(0).Rows
            Dim periodStart As DateTime = Convert.ToDateTime(row("ActualStartDate"))
            Dim periodEnd As DateTime = Convert.ToDateTime(row("ActualEndDate")).AddDays(-1)

            ' التحقق من وجود أي تداخل
            If (newStart <= periodEnd AndAlso newEnd >= periodStart) Then
                If ClsVacationTypes.OverlapWithAnotherVac Then

                    If CInt(row("VacationTypeID")) = 1 Then
                        If ClsClasses.AdvanceBalance Then

                            'insert t3weed in vacationbalance
                            Dim ExpireStr As String = "select top 1 ExpireDate from hrs_VacationsBalance where canceldate is null and ISNULL(Posted,0)=0 And EmployeeID= " & ClsEmployees.ID & " ExpireDate > '" & newEnd & "' order by BalanceTypeID desc"
                            Dim CompExpireDate = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, ExpireStr)

                            Dim compStr = "INSERT INTO [dbo].[hrs_VacationsBalance]([EmployeeID],[Year],[Balance],[Consumed],[Remaining],[BalanceTypeID],[ExpireDate],[Src],[Reguser],[RegDate],[DueDate]) VALUES (" & ClsEmployees.ID & "," & newEnd.Year & "," & txtConfirmedDays.Text & ",0," & txtConfirmedDays.Text & ",3,'" & CompExpireDate & "','FrmAnnualVacationRequestAction'," & User & ",'" & DateTime.Now.Date.ToString("yyyy-MM-dd") & "','" & newEnd & "')"
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, Data.CommandType.Text, compStr)

                        End If

                    End If
                    Return False

                Else
                    Return True ' يوجد تداخل
                End If
            End If
        Next

        Return False ' لا يوجد تداخل
    End Function

    Public Function CheckVacationsOverlapping() As Boolean
        ' Validate input dates and confirmed days
        Dim newStart As DateTime
        Dim newEnd As DateTime
        Dim confirmedDays As Integer

        If Not Date.TryParse(txtStartDate.Text, newStart) Then Return False
        If Not Date.TryParse(txtEndDate.Text, newEnd) Then Return False
        If Not Integer.TryParse(txtConfirmedDays.Text, confirmedDays) Then Return False
        Dim ClsEmployees As New Clshrs_Employees(Page)
        ClsEmployees.Find("Code='" & txtEmployee.Text & "'")

        newEnd = newEnd.AddDays(-1)

        ' Prepare query to get previous vacations
        Dim query As String = "SELECT * FROM hrs_EmployeesVacations WHERE canceldate IS NULL AND EmployeeID = @EmployeeID"
        Dim previousPeriods As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(
        ClsEmployees.ConnectionString,
        CommandType.Text,
        query,
        New SqlClient.SqlParameter("@EmployeeID", ClsEmployees.ID)
    )

        ' Load selected vacation type
        Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
        ClsVacationTypes.Find("ID=" & DdlVacationType.SelectedItem.Value)

        ' Get UserID from cookie
        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        WebHandler.GetCookies(Page, "UserID", User)

        ' Get employee class & contract
        Dim Cls_Contracts As New Clshrs_Contracts(Page)
        ClsClasses = New Clshrs_EmployeeClasses(Page)

        Dim dat_NEW_RETURN As DateTime = newStart.AddDays(confirmedDays)
        Dim intContractID As Integer = Cls_Contracts.ContractValidatoinId(ClsEmployees.ID, dat_NEW_RETURN)
        Cls_Contracts.Find("ID=" & intContractID)
        ClsClasses.Find("ID=" & If(Cls_Contracts.EmployeeClassID > 0, Cls_Contracts.EmployeeClassID, 0))

        ' Check each previous vacation for overlap
        For Each row As DataRow In previousPeriods.Tables(0).Rows
            Dim periodStart As DateTime = Convert.ToDateTime(row("ActualStartDate"))
            Dim periodEnd As DateTime = Convert.ToDateTime(row("ActualEndDate")).AddDays(-1)

            If (newStart <= periodEnd AndAlso newEnd >= periodStart) Then
                If ClsVacationTypes.OverlapWithAnotherVac Then
                    If CInt(row("VacationTypeID")) = 1 AndAlso ClsClasses.AdvanceBalance Then
                        ' Get nearest expire date for balance
                        Dim expireQuery As String = "SELECT TOP 1 ExpireDate FROM hrs_VacationsBalance WHERE canceldate IS NULL and ISNULL(Posted,0)=0 AND EmployeeID = @EmployeeID AND ExpireDate > @NewEnd ORDER BY BalanceTypeID asc"
                        Dim expireDate As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(
                        ClsEmployees.ConnectionString,
                        CommandType.Text,
                        expireQuery,
                        New SqlClient.SqlParameter("@EmployeeID", ClsEmployees.ID),
                        New SqlClient.SqlParameter("@NewEnd", newEnd)
                    )

                        If expireDate IsNot Nothing Then
                            ' Insert compensation balance
                            Dim insertQuery As String = "INSERT INTO [dbo].[hrs_VacationsBalance] " &
                            "([EmployeeID], [Year], [Balance], [Consumed], [Remaining], [BalanceTypeID], [ExpireDate], [Src], [Reguser], [RegDate], [DueDate]) " &
                            "VALUES (@EmployeeID, @Year, @Balance, 0, @Balance, 3, @ExpireDate, 'FrmAnnualVacationRequestAction', @User, @RegDate, @DueDate)"

                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(
                            ClsEmployees.ConnectionString,
                            CommandType.Text,
                            insertQuery,
                            New SqlClient.SqlParameter("@EmployeeID", ClsEmployees.ID),
                            New SqlClient.SqlParameter("@Year", newEnd.Year),
                            New SqlClient.SqlParameter("@Balance", confirmedDays),
                            New SqlClient.SqlParameter("@ExpireDate", Convert.ToDateTime(expireDate)),
                            New SqlClient.SqlParameter("@User", User),
                            New SqlClient.SqlParameter("@RegDate", DateTime.Now.Date),
                            New SqlClient.SqlParameter("@DueDate", newEnd)
                        )
                        End If
                    End If

                    Return False ' Overlap allowed and handled
                Else
                    Return True ' Overlap exists and not allowed
                End If
            End If
        Next

        Return False ' No overlap found
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
                    EmpName = " isnull( hrs_Employees.arbname ,' ')+' '+ isnull(hrs_Employees.FatherArbName, ' ')+' '+ isnull(hrs_Employees.GrandArbName,' ')+' '+isnull(hrs_Employees.FamilyArbName,' ') "

                Else
                    EmpName = " isnull(hrs_Employees.EngName,' ')+' '+isnull(hrs_Employees.FatherEngName,' ')+' '+isnull(hrs_Employees.GrandEngName ,' ')+' '+isnull(hrs_Employees.FamilyEngName,' ')"

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
