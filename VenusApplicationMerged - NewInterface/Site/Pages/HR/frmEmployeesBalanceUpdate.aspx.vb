Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Runtime.CompilerServices.RuntimeHelpers
Imports System.ComponentModel.Design
Imports System.Data

Partial Class frmEmployeesBalanceUpdate
    Inherits MainPage
#Region "Public Decleration"
    Dim ClsEmployeesDecisions As Clshrs_EmployeesDecisions
    Private ClsEmployees As Clshrs_Employees

    Dim Item As New System.Web.UI.WebControls.ListItem()

    Const intEmpSearchID = 90
    Const intItemSearchID = 82

    Private ObjNavigationHandler As Venus.Shared.Web.NavigationHandler

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEmployees = New Clshrs_Employees(Me)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Dim clsItems As New Clshrs_Items(Me)
        Try
            Dim ClsMaretalStatus As New ClsBasicFiles(Me.Page, "hrs_MaritalStatus")
            Dim ClsBloodGroup As New ClsBasicFiles(Me.Page, "hrs_BloodGroups")
            Dim ClsNationality As New ClsBasicFiles(Me.Page, "sys_Nationalities")
            Dim ClsBanks As New ClsBasicFiles(Me.Page, "sys_Banks")
            Dim ClsCities As New Clssys_Cities(Me.Page)
            Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
            Dim Clslocation As New ClsBasicFiles(Me.Page, "sys_Locations")
            Dim clsBranch As New Clssys_Branches(Page)
            Dim ClsContractsTypes As New Clshrs_ContractTypes(Me.Page)
            Dim ClsProfission As New Clshrs_Professions(Me.Page)
            Dim ClsPosition As New Clshrs_Positions(Me.Page)
            Dim ClsEmployeeClass As New Clshrs_EmployeeClasses(Me.Page)
            Dim ClsGradeStep As New Clshrs_GradesSteps(Me.Page)
            Dim ClsCurrencies As New ClsSys_Currencies(Me.Page)
            Dim ClsSponsor As New Clshrs_Sponsors(Page)
            Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsSponsor.ConnectionString)
            Dim ClsSectors As New ClsSys_Sectors(Page)

            Dim ClsDependantType As New ClsBasicFiles(Me.Page, "hrs_DependantsTypes")
            Dim ClsBirthCities As New ClsBasicFiles(Me.Page, "sys_Cities")
            Dim ClsLevels As New Clshrs_LevelTypes(Me.Page)
            Dim clsDependantsTypes As New Clshrs_DependantsTypes(Me.Page)
            ClsEmployees = New Clshrs_Employees(Me.Page)
            Dim Intrecordid As Integer
            Dim clsEducations As New Clshrs_Educations(Me, "hrs_Educations")

            txtEmployee.Focus()
            If ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmployee.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtEmployee.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then





            End If

            '================================== Add DateUpdateSchedules [Start]


            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try


    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_SaveN.Command, ImageButton_Save.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsEmployees = New Clshrs_Employees(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ClsEmployeesDecisions As New Clshrs_EmployeesDecisions(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesDecisions.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"


                If Not txtEmpID.Value = "" Then

                    If SavePart() = True Then
                        AfterOperation()

                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                    Else
                        'txtItem.ReadOnly = False
                        'txtItem.Enabled = True
                        'btnSearchItem.Enabled = True
                        'txtReceivedDate.Focus()
                        ImageButton_Delete.Enabled = False
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Me, "Please choose Employee/الرجاء إختيار الموظف"))
                    'txtItem.ReadOnly = False
                    'txtItem.Enabled = True
                    'btnSearchItem.Enabled = True
                    'txtItem.Focus()
                End If
            Case "Save"


                If Not txtEmpID.Value = "" Then

                    If SavePart() = True Then

                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                    Else
                        'txtItem.ReadOnly = False
                        'txtItem.Enabled = True
                        'btnSearchItem.Enabled = True
                        'txtReceivedDate.Focus()
                        ImageButton_Delete.Enabled = False
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Me, "Please choose Employee/الرجاء إختيار الموظف"))
                    'txtItem.ReadOnly = False
                    'txtItem.Enabled = True
                    'btnSearchItem.Enabled = True
                    'txtItem.Focus()
                End If
            Case "New"
                NewMode()
                ImageButton_New.Enabled = False
            Case "Delete"
                If txtID.Value <> String.Empty Then
                    ClsEmployeesDecisions.Delete("ID=" & Val(txtID.Value))
                    CheckCode()
                End If
            Case "Property"
                If txtID.Value <> String.Empty Then
                    If ClsEmployeesDecisions.Find("ID=" & Val(txtID.Value)) Then
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsEmployeesDecisions.ID & "&TableName=" & ClsEmployeesDecisions.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                    End If
                End If
            Case "Remarks"
                If txtID.Value <> String.Empty Then
                    If ClsEmployeesDecisions.Find("ID=" & Val(txtID.Value)) Then
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsEmployeesDecisions.ID & "&TableName=" & ClsEmployeesDecisions.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                    End If
                End If
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsEmployeesDecisions.Table
                ClsEmployeesDecisions.Find("ID=" & Val(txtID.Value))
                Dim recordID As Integer = ClsEmployeesDecisions.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsEmployeesDecisions.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsEmployeesDecisions.Find("ID=" & Val(txtID.Value))

                Venus.Shared.Web.ClientSideActions.ClosePage(Page)

            Case "First"
                ClsEmployees.Find(" ID = " & Val(txtEmpID.Value))
                ClsEmployees.FirstRecord("", Date.Now)
                txtEmployee.Text = ClsEmployees.Code
                txtEngName.Text = ClsEmployees.EngName & " " &
                                 ClsEmployees.FatherEngName & " " &
                                 ClsEmployees.GrandEngName & " " &
                                 ClsEmployees.FamilyEngName
                txtAraName.Text = ClsEmployees.ArbName & " " &
                                 ClsEmployees.FatherArbName & " " &
                                 ClsEmployees.GrandArbName & " " &
                                 ClsEmployees.FamilyArbName
                txtEmpID.Value = ClsEmployees.ID
                CheckCode()
            Case "Previous"
                ClsEmployees.Find(" Code ='" & txtEmployee.Text & "'")
                If Not ClsEmployees.previousRecord(" AND hrs_employees.Code < '" & txtEmployee.Text & "'", Date.Now) Then
                    ClsEmployees.Find(" Code ='" & txtEmployee.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                txtEmployee.Text = ClsEmployees.Code
                txtEngName.Text = ClsEmployees.EngName & " " &
                                 ClsEmployees.FatherEngName & " " &
                                 ClsEmployees.GrandEngName & " " &
                                 ClsEmployees.FamilyEngName
                txtAraName.Text = ClsEmployees.ArbName & " " &
                                 ClsEmployees.FatherArbName & " " &
                                 ClsEmployees.GrandArbName & " " &
                                 ClsEmployees.FamilyArbName
                txtEmpID.Value = ClsEmployees.ID
                CheckCode()
            Case "Next"
                ClsEmployees.Find(" Code ='" & txtEmployee.Text & "'")
                If Not ClsEmployees.NextRecord(" AND hrs_employees.Code > '" & txtEmployee.Text & "'", Date.Now) Then
                    ClsEmployees.Find(" Code ='" & txtEmployee.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                txtEmployee.Text = ClsEmployees.Code
                txtEngName.Text = ClsEmployees.EngName & " " &
                                 ClsEmployees.FatherEngName & " " &
                                 ClsEmployees.GrandEngName & " " &
                                 ClsEmployees.FamilyEngName
                txtAraName.Text = ClsEmployees.ArbName & " " &
                                 ClsEmployees.FatherArbName & " " &
                                 ClsEmployees.GrandArbName & " " &
                                 ClsEmployees.FamilyArbName
                txtEmpID.Value = ClsEmployees.ID
                CheckCode()
            Case "Last"
                ClsEmployees.Find(" Code ='" & txtEmployee.Text & "'")
                ClsEmployees.LastRecord("", Date.Now)
                txtEmployee.Text = ClsEmployees.Code
                txtEngName.Text = ClsEmployees.EngName & " " &
                                 ClsEmployees.FatherEngName & " " &
                                 ClsEmployees.GrandEngName & " " &
                                 ClsEmployees.FamilyEngName
                txtAraName.Text = ClsEmployees.ArbName & " " &
                                 ClsEmployees.FatherArbName & " " &
                                 ClsEmployees.GrandArbName & " " &
                                 ClsEmployees.FamilyArbName
                txtEmpID.Value = ClsEmployees.ID
                CheckCode()
        End Select
    End Sub
    Protected Sub txtEmployee_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmployee.TextChanged
        Dim ClsItems As New Clshrs_Items(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsItems.ConnectionString)

        CheckCode()

    End Sub

    Private Function CheckEmpContract() As Boolean
        Dim clsContracts As New Clshrs_Contracts(Me.Page)
        ClsEmployees = New Clshrs_Employees(Me.Page)
        ClsEmployees.Find(" Code = '" & txtEmployee.Text.Trim & "'")
        Dim EmployeeID As Integer = ClsEmployees.ID
        If Not ClsEmployees.ID > 0 Then
            Return True
        End If
        clsContracts.ContractValidatoinId(EmployeeID, Date.Now)
        If clsContracts.ID > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


#End Region

#Region "Private Functions"

    Private Function SavePart() As Boolean
        ClsEmployees = New Clshrs_Employees(Me)
        Dim ClsEmployeesVacations = New Clshrs_EmployeesVacations(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim IntUserId As Integer = ProfileCls.RetUserID()
        'Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        'IntUserId = ObjWebHandler.GetCookies(Page, "CurrentUserID", IntUserId)
        Try
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            If ClsEmployees.ID > 0 Then
                If txtNewExpireNew.Text <> txtExpireDate.Text Then
                    If CDate(txtNewExpireNew.Text) < CDate(txtExpireDate.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "New Expire date should be after old expire date/تاريخ الصلاحيه الجديد يجب ان يكون بعد التاريخ القديم!"))
                        Return False
                    End If
                    If (ClsEmployeesVacations.FindEmployeeVacations("hrs_EmployeesVacations.EmployeeID=" & ClsEmployees.ID & " AND ExpectedEndDate > '" & txtExpireDate.Text & "'")) Then

                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a Vacation that expires after the current Expire date/توجد اجازة تنتهي بعد تاريخ الصلاحية الحالي"))
                        Return False

                    End If
                    Dim UpdateCommand = "Update  hrs_VacationsBalance  set ExpireDate ='" & CDate(txtNewExpireNew.Text).ToString("yyyy-MM-dd") & "' where hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID=1 and EmployeeID=" & ClsEmployees.ID & "  and DueDate<='" & DateTime.Parse(DateTime.Now).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(DateTime.Now).ToString("yyyy-MM-dd") & "') ; "
                    Dim isertHistory = " INSERT INTO [dbo].[hrs_BalanceExpireHistory] ([EmpId],[BalanceType],[OldExpireDate],[NewExpireDate],[RegUserID],[RegDate]) VALUES (" & ClsEmployees.ID & ",1,'" & CDate(txtExpireDate.Text).ToString("yyyy-MM-dd") & "','" & CDate(txtNewExpireNew.Text).ToString("yyyy-MM-dd") & "'," & IntUserId & ",'" & DateTime.Now.ToString("yyyy-MM-dd HH:mm") & "') ;"
                    UpdateCommand = UpdateCommand & isertHistory
                    Dim SqlCommand As System.Data.SqlClient.SqlCommand
                    SqlCommand = New SqlClient.SqlCommand
                    SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                    SqlCommand.CommandType = CommandType.Text
                    SqlCommand.CommandText = UpdateCommand
                    SqlCommand.Connection.Open()
                    SqlCommand.ExecuteNonQuery()
                    SqlCommand.Connection.Close()
                End If

                If txtTransferExpireNew.Text <> txtTransferBalanceExpire.Text Then

                    If (ClsEmployeesVacations.FindEmployeeVacations("hrs_EmployeesVacations.EmployeeID=" & ClsEmployees.ID & " AND ExpectedEndDate > '" & txtTransferBalanceExpire.Text & "'")) Then

                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a Vacation that expires after the current Expire date/توجد اجازة تنتهي بعد تاريخ الصلاحية الحالي"))
                        Return False

                    End If
                    If CDate(txtTransferExpireNew.Text) < CDate(txtTransferBalanceExpire.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "New Expire date should be after old expire date/تاريخ الصلاحيه الجديد يجب ان يكون بعد التاريخ القديم!"))
                        Return False
                    End If
                    Dim UpdateCommand = "Update  hrs_VacationsBalance  set ExpireDate ='" & CDate(txtTransferExpireNew.Text).ToString("yyyy-MM-dd") & "' where hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID=2 and EmployeeID=" & ClsEmployees.ID & "  and DueDate<='" & DateTime.Parse(DateTime.Now).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(DateTime.Now).ToString("yyyy-MM-dd") & "')"
                    Dim isertHistory = " INSERT INTO [dbo].[hrs_BalanceExpireHistory] ([EmpId],[BalanceType],[OldExpireDate],[NewExpireDate],[RegUserID],[RegDate]) VALUES (" & ClsEmployees.ID & ",2,'" & CDate(txtTransferBalanceExpire.Text).ToString("yyyy-MM-dd") & "','" & CDate(txtTransferExpireNew.Text).ToString("yyyy-MM-dd") & "'," & ClsEmployees.DataBaseUserRelatedID & ",'" & DateTime.Now.ToString("yyyy-MM-dd HH:mm") & "') ;"
                    UpdateCommand = UpdateCommand & isertHistory
                    Dim SqlCommand As System.Data.SqlClient.SqlCommand
                    SqlCommand = New SqlClient.SqlCommand
                    SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                    SqlCommand.CommandType = CommandType.Text
                    SqlCommand.CommandText = UpdateCommand
                    SqlCommand.Connection.Open()
                    SqlCommand.ExecuteNonQuery()
                    SqlCommand.Connection.Close()
                End If

                If txtOverlapExpireNew.Text <> txtOverlapExpire.Text Then
                    If CDate(txtOverlapExpireNew.Text) < CDate(txtOverlapExpire.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "New Expire date should be after old expire date/تاريخ الصلاحيه الجديد يجب ان يكون بعد التاريخ القديم!"))
                        Return False
                    End If
                    If (ClsEmployeesVacations.FindEmployeeVacations("hrs_EmployeesVacations.EmployeeID=" & ClsEmployees.ID & " AND ExpectedEndDate > '" & txtOverlapExpire.Text & "'")) Then

                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There is a Vacation that expires after the current Expire date/توجد اجازة تنتهي بعد تاريخ الصلاحية الحالي"))
                        Return False

                    End If
                    Dim UpdateCommand = "Update  hrs_VacationsBalance  set ExpireDate ='" & CDate(txtOverlapExpireNew.Text).ToString("yyyy-MM-dd") & "' where hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID=3 and EmployeeID=" & ClsEmployees.ID & "  and DueDate<='" & DateTime.Parse(DateTime.Now).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(DateTime.Now).ToString("yyyy-MM-dd") & "')"
                    Dim isertHistory = " INSERT INTO [dbo].[hrs_BalanceExpireHistory] ([EmpId],[BalanceType],[OldExpireDate],[NewExpireDate],[RegUserID],[RegDate]) VALUES (" & ClsEmployees.ID & ",3,'" & CDate(txtOverlapExpire.Text).ToString("yyyy-MM-dd") & "','" & CDate(txtOverlapExpireNew.Text).ToString("yyyy-MM-dd") & "'," & ClsEmployees.DataBaseUserRelatedID & ",'" & DateTime.Now.ToString("yyyy-MM-dd HH:mm") & "') ;"
                    UpdateCommand = UpdateCommand & isertHistory
                    Dim SqlCommand As System.Data.SqlClient.SqlCommand
                    SqlCommand = New SqlClient.SqlCommand
                    SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                    SqlCommand.CommandType = CommandType.Text
                    SqlCommand.CommandText = UpdateCommand
                    SqlCommand.Connection.Open()
                    SqlCommand.ExecuteNonQuery()
                    SqlCommand.Connection.Close()
                End If
                CheckCode()
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function SetToolbarSetting(ByVal ptrType As String, ByVal ClsClass As Object, ByVal intID As Integer) As Boolean
        Try
            Select Case ptrType
                Case "N", "R"
                    txtID.Value = String.Empty
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
                    ClsEmployeesDecisions.Find("ID=" & intID)
                    CheckCode()
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsEmployeesDecisions.Find("ID=" & intID)
                    CheckCode()
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsEmployeesDecisions
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    ImageButton_Delete.Enabled = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsEmployeesDecisions = New Clshrs_EmployeesDecisions(Me)
        If IntId > 0 Then
            ClsEmployeesDecisions.Find("ID=" & IntId)
            CheckCode()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean

        ClsEmployees = New Clshrs_Employees(Me)
        'ClsEmployeesDecisions = New Clshrs_EmployeesDecisions(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Try
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            If ClsEmployees.ID > 0 Then
                txtEmpID.Value = ClsEmployees.ID

                txtEngName.Text = ClsEmployees.EngName & " " &
                                 ClsEmployees.FatherEngName & " " &
                                 ClsEmployees.GrandEngName & " " &
                                 ClsEmployees.FamilyEngName
                txtAraName.Text = ClsEmployees.ArbName & " " &
                                 ClsEmployees.FatherArbName & " " &
                                 ClsEmployees.GrandArbName & " " &
                                 ClsEmployees.FamilyArbName

                Dim ConnectionString As String
                ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()

                Dim strselect As String
                strselect = "SELECT      Balance, Consumed, Remaining,ExpireDate FROM     hrs_VacationsBalance  where hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID=1 and EmployeeID=" & ClsEmployees.ID & " and DueDate<='" & DateTime.Parse(DateTime.Now).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(DateTime.Now).ToString("yyyy-MM-dd") & "') and ISNULL(Posted,0)=0"
                Dim DsFirstLevel As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
                If DsFirstLevel.Tables(0).Rows.Count > 0 Then
                    With DsFirstLevel.Tables(0).Rows(0)
                        lblNewBalanceNew.Text = .Item("Balance")
                        lblNewConsumed.Text = .Item("Consumed")
                        lblNewRemain.Text = .Item("Remaining")
                        txtNewExpireNew.Text = DateTime.Parse(.Item("ExpireDate")).ToString("dd/MM/yyyy")
                        txtExpireDate.Text = DateTime.Parse(.Item("ExpireDate")).ToString("dd/MM/yyyy")
                    End With
                Else
                    txtExpireDate.Text = ""
                    txtNewExpireNew.Text = ""
                End If

                strselect = "SELECT      Balance, Consumed, Remaining,ExpireDate FROM     hrs_VacationsBalance  where hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID=2 and EmployeeID=" & ClsEmployees.ID & "  and DueDate<='" & DateTime.Parse(DateTime.Now).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(DateTime.Now).ToString("yyyy-MM-dd") & "') and ISNULL(Posted,0)=0"
                DsFirstLevel = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
                If DsFirstLevel.Tables(0).Rows.Count > 0 Then
                    With DsFirstLevel.Tables(0).Rows(0)
                        lblTransferValue.Text = .Item("Balance")
                        lblTransferConsumedValue.Text = .Item("Consumed")
                        lblTransferRemainValue.Text = .Item("Remaining")
                        txtTransferBalanceExpire.Text = DateTime.Parse(.Item("ExpireDate")).ToString("dd/MM/yyyy")
                        txtTransferExpireNew.Text = DateTime.Parse(.Item("ExpireDate")).ToString("dd/MM/yyyy")
                    End With
                Else
                    txtTransferBalanceExpire.Text = ""
                    txtTransferExpireNew.Text = ""
                End If

                strselect = "SELECT      Balance, Consumed, Remaining,ExpireDate FROM     hrs_VacationsBalance  where hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID=3 and EmployeeID=" & ClsEmployees.ID & "  and DueDate<='" & DateTime.Parse(DateTime.Now).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(DateTime.Now).ToString("yyyy-MM-dd") & "') and ISNULL(Posted,0)=0"
                DsFirstLevel = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
                If DsFirstLevel.Tables(0).Rows.Count > 0 Then
                    With DsFirstLevel.Tables(0).Rows(0)
                        lblOverlapValue.Text = .Item("Balance")
                        lblOverlapConsumedValue.Text = .Item("Consumed")
                        lblOverlapRemainValue.Text = .Item("Remaining")
                        txtOverlapExpire.Text = DateTime.Parse(.Item("ExpireDate")).ToString("dd/MM/yyyy")
                        txtOverlapExpireNew.Text = DateTime.Parse(.Item("ExpireDate")).ToString("dd/MM/yyyy")
                    End With
                Else
                    txtOverlapExpire.Text = ""
                    txtOverlapExpireNew.Text = ""

                End If

                Dim EmpName As String
                Dim newType As String
                Dim transType As String
                Dim overlapType As String
                If ProfileCls.CurrentLanguage = "Ar" Then
                    EmpName = " sys_Users.ArbName "
                    newType = " جديد "
                    transType = " مرحل "
                    overlapType = " تعويضي "
                Else
                    EmpName = "sys_Users.EngName"
                    newType = " New "
                    transType = " Transfered "
                    overlapType = " Overlap "
                End If
                Dim str1 As String = "SELECT        hrs_BalanceExpireHistory.OldExpireDate, hrs_BalanceExpireHistory.NewExpireDate,  hrs_BalanceExpireHistory.RegDate,case when hrs_BalanceExpireHistory.BalanceType =1 then '" & newType & "' else case when hrs_BalanceExpireHistory.BalanceType =2 then '" & transType & "' else '" & overlapType & "' end end as BalanceType ," & EmpName & " as RegUser FROM    hrs_BalanceExpireHistory LEFT OUTER JOIN sys_Users ON hrs_BalanceExpireHistory.RegUserID = sys_Users.ID where hrs_BalanceExpireHistory.EmpId=" & ClsEmployees.ID



                If str1 <> "" Then
                    Dim DS1 As New System.Data.DataSet()
                    DS1.Clear()
                    For x As Integer = 0 To DS1.Tables.Count - 1
                        DS1.Tables(x).Constraints.Clear()
                    Next
                    Dim connetionString As String
                    Dim connection As System.Data.SqlClient.SqlConnection
                    Dim command As System.Data.SqlClient.SqlCommand
                    Dim adapter As New System.Data.SqlClient.SqlDataAdapter
                    connetionString = ClsEmployees.ConnectionString
                    connection = New System.Data.SqlClient.SqlConnection(connetionString)

                    command = New System.Data.SqlClient.SqlCommand(str1, connection)
                    adapter.SelectCommand = command
                    adapter.Fill(DS1, "Table1")

                    connection.Close()
                    uwgEmployeeVacations.DataSource = Nothing
                    uwgEmployeeVacations.DataBind()

                    uwgEmployeeVacations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
                    uwgEmployeeVacations.DataSource = DS1
                    uwgEmployeeVacations.DataBind()
                End If

            Else
                txtEmployee.Text = ""
                txtEngName.Text = ""
                txtAraName.Text = ""
                txtEmpID.Value = "0"
                txtExpireDate.Text = txtNewExpireNew.Text = txtTransferBalanceExpire.Text = txtTransferExpireNew.Text = txtOverlapExpire.Text = txtOverlapExpireNew.Text = ""
                uwgEmployeeVacations.DataSource = Nothing
                uwgEmployeeVacations.DataBind()
                Clear()
                SetToolBarDefaults()
                AfterOperation()
                ImageButton_Delete.Enabled = False
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Me, "This Employee Not Found! /هذا الموظف غير موجود"))
                txtEmployee.Focus()
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        txtEmployee.Text = ""
        Clear()
        'CheckCode()
        'Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtItem, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean

        txtID.Value = String.Empty
        txtEmployee.Text = String.Empty


        txtEngName.Text = ""
        txtAraName.Text = ""

        txtExpireDate.Text = ""
        txtNewExpireNew.Text = ""
        txtTransferBalanceExpire.Text = ""
        txtTransferExpireNew.Text = ""
        txtOverlapExpire.Text = ""
        txtOverlapExpireNew.Text = ""

        lblNewBalanceNew.Text = "0"
        lblNewConsumed.Text = "0"
        lblNewRemain.Text = "0"

        lblTransferValue.Text = "0"
        lblTransferConsumedValue.Text = "0"
        lblTransferRemainValue.Text = "0"

        lblOverlapValue.Text = "0"
        lblOverlapConsumedValue.Text = "0"
        lblOverlapRemainValue.Text = "0"

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function


    Private Function NewMode() As Boolean
        Clear()

        SetToolBarDefaults()
        ImageButton_Delete.Enabled = False
        ImageButton_SaveN.Enabled = False
    End Function

#End Region




End Class
