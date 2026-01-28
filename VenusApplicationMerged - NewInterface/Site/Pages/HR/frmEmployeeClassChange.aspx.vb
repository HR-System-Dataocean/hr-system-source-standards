Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Runtime.CompilerServices.RuntimeHelpers
Imports System.ComponentModel.Design
Imports System.Data

Partial Class frmEmployeeClassChange
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


                ClsEmployeeClass.GetDropDownList(ddlEmployeeClass, True)

                DisableControlsInTd()


                ClsEmployeeClass.GetDropDownList(ddlOLdEmployeeClass, True)

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
                    If JoinDate.Text = "  /  /    " Or JoinDate.Text = "" Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " you have to insert effective date /برجاء تسجيل تاريخ التأثير"))
                        Exit Sub
                    End If
                    If Convert.ToInt32(txtAllBalance.Text) = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " you have to insert Balance /برجاء ادخال الرصيد"))
                        Exit Sub
                    End If
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
        ClsEmployeesDecisions = New Clshrs_EmployeesDecisions(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Try
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            If ClsEmployees.ID > 0 Then

                ClsEmployeesDecisions.CompanyID = ClsEmployees.CompanyID
                ClsEmployeesDecisions.PreviousCompanyID = ClsEmployees.CompanyID

                ClsEmployeesDecisions.PreviousNationalityID = ClsEmployees.NationalityID

                ClsEmployeesDecisions.PreviousMaritalStatusID = ClsEmployees.MaritalStatusID


                ClsEmployeesDecisions.PreviousE_Mail = ClsEmployees.E_Mail

                ClsEmployeesDecisions.PreviousMobile = ClsEmployees.Mobile

                ClsEmployeesDecisions.PreviousWorkE_Mail = ClsEmployees.WorkE_Mail

                ClsEmployeesDecisions.PreviousBranchID = ClsEmployees.BranchID

                ClsEmployeesDecisions.PreviousDepartmentID = ClsEmployees.DepartmentID

                ClsEmployeesDecisions.PreviousLocationID = ClsEmployees.LocationID

                ClsEmployeesDecisions.PreviousSectorID = ClsEmployees.SectorID

                ClsEmployeesDecisions.PreviousCost1 = ClsEmployees.Cost1

                ClsEmployeesDecisions.PreviousCost2 = ClsEmployees.Cost2

                ClsEmployeesDecisions.PreviousCost3 = ClsEmployees.Cost3

                ClsEmployeesDecisions.PreviousCost4 = ClsEmployees.Cost4

                ClsEmployeesDecisions.PreviousManagerID = ClsEmployees.ManagerID

                ClsEmployeesDecisions.PreviousSponsorID = ClsEmployees.SponsorID

                ClsEmployeesDecisions.PreviousBankID = ClsEmployees.BankID

                ClsEmployeesDecisions.PreviousBankAccountNumber = ClsEmployees.BankAccountNumber

                ClsEmployeesDecisions.PreviousPassPortNo = ClsEmployees.PassPortNo
                ClsEmployeesDecisions.PreviousSSnNo = ClsEmployees.SSnNo

                Dim clsEmpEducations As New Clshrs_EmployeesEducations(Me)
                If clsEmpEducations.Find("EmployeeID=" & ClsEmployees.ID) Then

                    ClsEmployeesDecisions.PreviousLastEducations = clsEmpEducations.EducationTypeID

                    ClsEmployeesDecisions.PreviousGraduationDate = clsEmpEducations.GraduationDate


                    Dim clsContracts = New Clshrs_Contracts(Me.Page)
                    Dim ObjDataHandler As New Venus.Shared.DataHandler
                    Dim StrSerial As String = String.Empty

                    clsContracts.Find(" EmployeeId = " & ClsEmployees.ID & " And Isnull(CancelDate,'')= '' and (EndDate is null or EndDate > getdate()) order by ID desc")


                    ClsEmployeesDecisions.PreviousContractType = clsContracts.ContractTypeId

                    ClsEmployeesDecisions.PreviousProfessions = clsContracts.ProfessionID

                    ClsEmployeesDecisions.PreviousPosition = clsContracts.PositionID

                    ClsEmployeesDecisions.PreviousEmployeeClass = clsContracts.EmployeeClassID


                End If

                'New Vacation Balance
                Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Me)
                ClsEmployeeClasses.Find("ID=" & ddlEmployeeClass.SelectedValue)
                If ClsEmployeeClasses.AdvanceBalance And ClsEmployeeClasses.AddBalanceInAddEmp Then

                    Dim endOfYear As DateTime = New DateTime(CDate(JoinDate.Text).Year, 12, 31)
                    Dim myBalance = Convert.ToDecimal(txtAllBalance.Text)
                    Dim expireDate As Date = endOfYear
                    Dim moreOneYear As Boolean = False
                    Dim consumed = Convert.ToDecimal(txtAllConsumed.Text)
                    Dim Remain = Convert.ToDecimal(txtRemain.Text)
                    moreOneYear = ClsEmployeeClasses.AccumulatedBalance
                    If moreOneYear Then
                        expireDate = CDate(JoinDate.Text).Date.AddYears(2).AddDays(-1)
                    End If
                    Dim checkCnt = "UPDATE [dbo].[hrs_VacationsBalance] SET [CancelDate] = '" & CDate(JoinDate.Text).Date.ToString("yyyy-MM-dd") & "' WHERE [EmployeeID] = " & ClsEmployees.ID & " ; "
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, CommandType.Text, checkCnt)
                    checkCnt = "INSERT INTO [dbo].[hrs_VacationsBalance] ([EmployeeID],[Year],[Balance],[Consumed],[Remaining],[BalanceTypeID],[ExpireDate],[Src],[Remarks],[Reguser],[RegDate],DueDate) VALUES (" & ClsEmployees.ID & "," & CDate(JoinDate.Text).Year & "," & myBalance & "," & consumed & "," & Remain & ",1,'" & expireDate.ToString("yyyy-MM-dd") & "'," & "'frmEmployeeClassChange'" & "," & "''" & ",'" & ClsEmployees.RegUserID & "',getdate(),'" & CDate(JoinDate.Text).Date.ToString("yyyy-MM-dd") & "')"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, CommandType.Text, checkCnt)

                End If
            End If
        Catch ex As Exception
            Return False
        End Try

        Dim EmployeeID As Integer = Convert.ToInt32(txtEmpID.Value)
        ClsEmployees = New Clshrs_Employees(Me)
        ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
        If txtID.Value = String.Empty Then txtID.Value = 0

        Try

            ClsEmployeesDecisions.EmpId = ClsEmployees.ID
            ClsEmployeesDecisions.Code = ClsEmployees.Code


            'ddlEmployeeClass.SelectedValue = clsContracts.EmployeeClassID
            If ddlEmployeeClass.SelectedValue > 0 Then
                ClsEmployeesDecisions.EmployeeClass = ddlEmployeeClass.SelectedValue
            Else
                ClsEmployeesDecisions.EmployeeClass = DBNull.Value
            End If

            ClsEmployeesDecisions.Save()

            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")

            ClsEmployees.Update("Code='" & txtEmployee.Text & "'")

            Dim clsEmpEducations As New Clshrs_EmployeesEducations(Me)
            If clsEmpEducations.Find("EmployeeID=" & ClsEmployees.ID) Then
                'ddlLastEducations.SelectedValue = clsEmpEducations.EducationTypeID

                clsEmpEducations.Update("EmployeeID=" & ClsEmployees.ID)

            End If

            Dim clsContracts = New Clshrs_Contracts(Me.Page)
            Dim ObjDataHandler As New Venus.Shared.DataHandler
            '    Dim StrSerial As String = String.Empty

            '    ObjDataHandler.GetLastSerial(clsContracts.Table, "Number", StrSerial, clsContracts.ConnectionString)
            clsContracts.Find(" EmployeeId = " & ClsEmployees.ID & " And Isnull(CancelDate,'')= '' and (EndDate is null or EndDate > getdate()) order by ID desc")

            'ddlContractType.SelectedValue = clsContracts.ContractTypeId




            'ddlEmployeeClass.SelectedValue = clsContracts.EmployeeClassID
            If ddlEmployeeClass.SelectedValue > 0 Then
                clsContracts.EmployeeClassID = ddlEmployeeClass.SelectedValue
            End If

            clsContracts.Update("Number='" & clsContracts.Number & "'")


            Return True
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
        ClsEmployeesDecisions = New Clshrs_EmployeesDecisions(Me)
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
                'DdlNationality.SelectedValue = ClsEmployees.NationalityID
                ClsEmployeesDecisions.PreviousNationalityID = ClsEmployees.NationalityID
                'DdlMaritalStatus.SelectedValue = ClsEmployees.MaritalStatusID
                ClsEmployeesDecisions.PreviousMaritalStatusID = ClsEmployees.MaritalStatusID

                'txtEmail.Text = ClsEmployees.E_Mail
                ClsEmployeesDecisions.PreviousE_Mail = ClsEmployees.E_Mail
                'txtMobile.Text = ClsEmployees.Mobile
                ClsEmployeesDecisions.PreviousMobile = ClsEmployees.Mobile
                'txtWorkE_Mail.Text = ClsEmployees.WorkE_Mail
                ClsEmployeesDecisions.PreviousWorkE_Mail = ClsEmployees.WorkE_Mail
                'ddlBranch.SelectedValue = ClsEmployees.BranchID
                ClsEmployeesDecisions.PreviousBranchID = ClsEmployees.BranchID
                'ddlDepartment.SelectedValue = ClsEmployees.DepartmentID
                ClsEmployeesDecisions.PreviousDepartmentID = ClsEmployees.DepartmentID
                'ddlLocation.SelectedValue = ClsEmployees.LocationID
                ClsEmployeesDecisions.PreviousLocationID = ClsEmployees.LocationID
                'ddlSectors.SelectedValue = ClsEmployees.SectorID
                ClsEmployeesDecisions.PreviousSectorID = ClsEmployees.SectorID
                'TxtCostCode1.Text = ClsEmployees.Cost1
                ClsEmployeesDecisions.PreviousCost1 = ClsEmployees.Cost1
                'TxtCostCode2.Text = ClsEmployees.Cost2
                ClsEmployeesDecisions.PreviousCost2 = ClsEmployees.Cost2
                'TxtCostCode3.Text = ClsEmployees.Cost3
                ClsEmployeesDecisions.PreviousCost3 = ClsEmployees.Cost3
                'TxtCostCode4.Text = ClsEmployees.Cost4
                ClsEmployeesDecisions.PreviousCost4 = ClsEmployees.Cost4
                'txtManager.Text = ClsEmployees.ManagerID
                ClsEmployeesDecisions.PreviousManagerID = ClsEmployees.ManagerID
                'ddlSponsor.SelectedValue = ClsEmployees.SponsorID
                ClsEmployeesDecisions.PreviousSponsorID = ClsEmployees.SponsorID
                'ddlBank.SelectedValue = ClsEmployees.BankID
                ClsEmployeesDecisions.PreviousBankID = ClsEmployees.BankID
                'txtBankAccount.Text = ClsEmployees.BankAccountNumber
                ClsEmployeesDecisions.PreviousBankAccountNumber = ClsEmployees.BankAccountNumber
                'txtPassport.Text = ClsEmployees.PassPortNo
                ClsEmployeesDecisions.PreviousPassPortNo = ClsEmployees.PassPortNo
                ClsEmployeesDecisions.PreviousSSnNo = ClsEmployees.SSnNo



                Dim clsEmpEducations As New Clshrs_EmployeesEducations(Me)
                If clsEmpEducations.Find("EmployeeID=" & ClsEmployees.ID) Then
                    'ddlLastEducations.SelectedValue = clsEmpEducations.EducationTypeID
                    ClsEmployeesDecisions.PreviousLastEducations = clsEmpEducations.EducationTypeID
                    'txtGraduationDate.Text = clsEmpEducations.GraduationDate
                    ClsEmployeesDecisions.PreviousGraduationDate = clsEmpEducations.GraduationDate


                    Dim clsContracts = New Clshrs_Contracts(Me.Page)
                    Dim ObjDataHandler As New Venus.Shared.DataHandler
                    Dim StrSerial As String = String.Empty

                    clsContracts.Find(" EmployeeId = " & ClsEmployees.ID & " And Isnull(CancelDate,'')= '' and (EndDate is null or EndDate > getdate()) order by ID desc")

                    'ddlContractType.SelectedValue = clsContracts.ContractTypeId
                    ClsEmployeesDecisions.PreviousContractType = clsContracts.ContractTypeId
                    'ddlProfessions.SelectedValue = clsContracts.ProfessionID
                    ClsEmployeesDecisions.PreviousProfessions = clsContracts.ProfessionID
                    'ddlPosition.SelectedValue = clsContracts.PositionID
                    ClsEmployeesDecisions.PreviousPosition = clsContracts.PositionID
                    'ddlEmployeeClass.SelectedValue = clsContracts.EmployeeClassID
                    ClsEmployeesDecisions.PreviousEmployeeClass = clsContracts.EmployeeClassID


                    ddlOLdEmployeeClass.SelectedValue = clsContracts.EmployeeClassID
                    'Clear()
                    'ClsEmployeesDecisions.Find(" EmployeeID = " & ClsEmployees.ID)
                    'If ClsEmployeesDecisions.ID > 0 Then
                    'Else
                    '    txtEmployee.Text = ""
                    '    txtEngName.Text = ""
                    '    txtEmpID.Value = "0"
                    '    Clear()
                    '    SetToolBarDefaults()
                    '    AfterOperation()
                    '    SetToolBarPermission(Me, ClsEmployeesDecisions.ConnectionString, ClsEmployeesDecisions.DataBaseUserRelatedID, ClsEmployeesDecisions.GroupID, "N")
                    '    ImageButton_Delete.Enabled = False
                    '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Me, "This Employee Not Found! /هذا الموظف غير موجود"))
                    '    txtEmployee.Focus()
                    'End If

                    'NewMode()
                    GetEmpContractVac()
                End If

            Else
                txtEmployee.Text = ""
                txtEngName.Text = ""
                txtAraName.Text = ""
                txtEmpID.Value = "0"
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
        ClsEmployeesDecisions.Clear()
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


        ddlEmployeeClass.SelectedIndex = -1



        txtEngName.Text = ""
        txtAraName.Text = ""


        ddlOLdEmployeeClass.SelectedIndex = -1




        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function

    Protected Sub DisableControlsInTd()

        For Each ctrl As Control In myTd4.Controls
            If TypeOf ctrl Is WebControl Then
                CType(ctrl, WebControl).Enabled = False
            End If
        Next
    End Sub
    Private Function NewMode() As Boolean
        Clear()

        SetToolBarDefaults()
        ImageButton_Delete.Enabled = False
        ImageButton_SaveN.Enabled = False
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
        DteVacationStartDate = DateTime.Now.Date
        ClsContracts.ContractValidatoinId(ClsEmployees.ID, DteVacationStartDate)
        Dim dblVacationDays As Double = 0
        If ClsContracts.ID > 0 Then
            Dim isconract As Boolean = False
            Dim ClsEmployeeClass = New Clshrs_EmployeeClasses(Page)
            ClsEmployeeClass.Find("ID=" & ClsContracts.EmployeeClassID & "")
            If ClsEmployeeClass.AdvanceBalance Then
                Dim str = "SELECT    Remaining FROM hrs_VacationsBalance where hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID=2 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & DteVacationStartDate.ToString("yyyy-MM-dd") & "' and DueDate<='" & DteVacationStartDate.ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DteVacationStartDate.ToString("yyyy-MM-dd") & "') and ISNULL(Posted,0)=0"
                Dim remain As Decimal
                remain = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContracts.ConnectionString, CommandType.Text, str)

                str = "SELECT    Remaining FROM hrs_VacationsBalance where hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID<>2 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & DteVacationStartDate.ToString("yyyy-MM-dd") & "'  and DueDate<='" & DteVacationStartDate.ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DteVacationStartDate.ToString("yyyy-MM-dd") & "') and ISNULL(Posted,0)=0"
                Dim Balance As Decimal
                Balance = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContracts.ConnectionString, CommandType.Text, str)


                str = "SELECT    Balance FROM hrs_VacationsBalance where hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID=2 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & DteVacationStartDate.ToString("yyyy-MM-dd") & "' and DueDate<='" & DteVacationStartDate.ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DteVacationStartDate.ToString("yyyy-MM-dd") & "') and ISNULL(Posted,0)=0"
                Dim TransferBalance As Decimal
                TransferBalance = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContracts.ConnectionString, CommandType.Text, str)

                str = "SELECT    Balance FROM hrs_VacationsBalance where hrs_VacationsBalance.EndServiceDate IS NULL and BalanceTypeID<>2 and EmployeeID=" & ClsEmployees.ID & " and ExpireDate >='" & DteVacationStartDate.ToString("yyyy-MM-dd") & "'  and DueDate<='" & DteVacationStartDate.ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DteVacationStartDate.ToString("yyyy-MM-dd") & "') and ISNULL(Posted,0)=0"
                Dim NewBalance As Decimal
                NewBalance = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContracts.ConnectionString, CommandType.Text, str)


                dblVacationDays = Balance + remain
                If ClsVacations.RoundAnnualVacBalance = True Then
                    lblNewRemain.Text = (RoundVacationBalance(Math.Round(Balance, 2))).ToString()
                    lblTransferBalance.Text = (RoundVacationBalance(Math.Round(TransferBalance, 2))).ToString()
                    lblNewBalance.Text = (RoundVacationBalance(Math.Round(NewBalance, 2))).ToString()
                    lblNewConsumed.Text = (RoundVacationBalance(Math.Round(NewBalance - Balance, 2))).ToString()

                    lblTransferRemain.Text = (RoundVacationBalance(Math.Round(remain, 2))).ToString()
                    lblTransferConsumed.Text = (RoundVacationBalance(Math.Round(TransferBalance - remain, 2))).ToString()
                Else
                    lblNewRemain.Text = Math.Round(Balance, 2)
                    lblTransferBalance.Text = Math.Round(TransferBalance, 2)
                    lblNewBalance.Text = Math.Round(NewBalance, 2)
                    lblNewConsumed.Text = Math.Round(NewBalance - Balance, 2)

                    lblTransferRemain.Text = Math.Round(remain, 2)
                    lblTransferConsumed.Text = Math.Round(TransferBalance - remain, 2)

                End If
            End If

            Dim OpenBalanceAmount As Double = 0
            Dim VacTypeID As Integer = 1
            Dim Diffe As Single = 0



            If ProfileCls.CurrentLanguage = "Ar" Then
                lblNewYear.Text = "جديد" & " :"
                lblRemain.Text = "باقي" & " :"
                lblConsumed.Text = "م" & " :"

                lblNewYear.Text = "جديد" & " :"
                lblTransferYear.Text = "مرحل" & " :"
                lblRemainT.Text = "باقي" & " :"
                lblConsumedT.Text = "م" & " :"
            Else
                lblNewYear.Text = "New" & " :"
                lblTransferYear.Text = "Transfered" & " :"

                lblRemain.Text = "Remain" & " :"
                lblConsumed.Text = "C" & " :"
                lblRemainT.Text = "Remain" & " :"
                lblConsumedT.Text = "C" & " :"
            End If

            If dblVacationDays < 0 Then dblVacationDays = 0


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

    Protected Sub ddlEmployeeClass_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlEmployeeClass.SelectedIndexChanged, JoinDate.ValueChange
        If ddlEmployeeClass.SelectedValue <> 0 And JoinDate.Text <> "  /  /    " Then
            'New Vacation Balance
            Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Me)
            ClsEmployeeClasses.Find("ID=" & ddlEmployeeClass.SelectedValue)
            If ClsEmployeeClasses.AdvanceBalance And ClsEmployeeClasses.AddBalanceInAddEmp Then
                Dim ds As DataSet = ClsEmployeeClasses.GetEmployeeClassAnnualVacations(ClsEmployeeClasses.ID)
                Dim allDays As Decimal = 1
                Dim allVacDays As Decimal = 0
                Dim moreOneYear As Boolean = False
                If ds.Tables(0).Rows.Count > 0 Then
                    With ds.Tables(0).Rows(0)
                        allDays = .Item("RequiredWorkingMonths")
                        allVacDays = .Item("DurationDays")

                    End With
                End If
                moreOneYear = ClsEmployeeClasses.AccumulatedBalance

                Dim dayValue = allVacDays / allDays
                Dim currentYear As Integer = SetDate(JoinDate.Text, JoinDate.Text).Year
                Dim endOfYear As DateTime = New DateTime(currentYear, 12, 31)
                Dim myDays As Integer = DateDiff(DateInterval.Day, SetDate(JoinDate.Text, JoinDate.Text), endOfYear.Date)
                Dim myBalance = myDays * dayValue
                Dim expireDate As Date = endOfYear
                If moreOneYear Then
                    myDays = DateDiff(DateInterval.Day, SetDate(JoinDate.Text, JoinDate.Text), SetDate(JoinDate.Text, JoinDate.Text).AddDays(allDays))
                    myBalance = myDays * dayValue
                    expireDate = SetDate(JoinDate.Text, JoinDate.Text).AddYears(2).AddDays(-1)
                End If
                txtAllBalance.Text = Math.Round(myBalance, 0)
                txtAllConsumed.Text = 0
                txtRemain.Text = Math.Round(myBalance, 0)
            Else
                txtAllBalance.Text = 0
                txtAllConsumed.Text = 0
                txtRemain.Text = 0
            End If
        Else
            txtAllBalance.Text = 0
            txtAllConsumed.Text = 0
            txtRemain.Text = 0
        End If
    End Sub
    Private Function SetDate(gData As Object, hDate As Object) As Date
        Try

            If gData <> "  /  /    " Then
                If ClsDataAcessLayer.IsGreg(gData) Then
                    Return ClsDataAcessLayer.FormatGreg(gData, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.HijriToGreg(gData, "dd/MM/yyyy")
                End If
            ElseIf hDate <> "  /  /    " Then
                If ClsDataAcessLayer.IsHijri(hDate) Then
                    Return ClsDataAcessLayer.HijriToGreg(hDate, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.FormatGreg(hDate, "dd/MM/yyyy")
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Sub txtConsum_TextChanged(sender As Object, e As System.EventArgs) Handles txtAllConsumed.TextChanged, txtAllBalance.TextChanged
        If txtAllBalance.Text <> "" And txtAllConsumed.Text <> "" Then
            txtRemain.Text = Math.Round((Convert.ToDecimal(txtAllBalance.Text) - Convert.ToDecimal(txtAllConsumed.Text)), 0)
        End If
    End Sub
#End Region



End Class
