Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Runtime.CompilerServices.RuntimeHelpers
Imports System.ComponentModel.Design
Partial Class frmEmployeesDecisions
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
                clsEducations.GetDropDownList(ddlLastEducations, True)
                ClsMaretalStatus.GetDropDownList(DdlMaritalStatus, True)
                ClsNationality.GetDropDownList(DdlNationality, True)
                ClsBanks.GetDropDownList(ddlBank, True)
                ClsSponsor.GetDropDownList(ddlSponsor, True)
                ClsDepartment.GetDropDownList(ddlDepartment, True)
                clsBranch.GetDropDownList(ddlBranch, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
                ClsSectors.GetDropDownList(ddlSectors, True)
                Clslocation.GetDropDownList(ddlLocation, True)

                ClsContractsTypes.GetDropDownList(ddlContractType, True)
                ClsProfission.GetDropDownList(ddlProfessions, True)
                ClsPosition.GetDropDownList(ddlPosition, True)
                ClsEmployeeClass.GetDropDownList(ddlEmployeeClass, True)

                DisableControlsInTd()

                clsEducations.GetDropDownList(ddlOldLastEducations, True)
                ClsMaretalStatus.GetDropDownList(ddlOldMaritalStatus, True)
                ClsNationality.GetDropDownList(ddlOldNationality, True)
                ClsBanks.GetDropDownList(ddlOldBank, True)
                ClsSponsor.GetDropDownList(ddlOldSponsor, True)
                ClsDepartment.GetDropDownList(ddlOldDepartment, True)
                clsBranch.GetDropDownList(ddlOldBranch, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
                ClsSectors.GetDropDownList(ddlOldSectors, True)
                Clslocation.GetDropDownList(ddlOldLocation, True)

                ClsContractsTypes.GetDropDownList(ddlOLdContractType, True)
                ClsProfission.GetDropDownList(ddlOLdProfessions, True)
                ClsPosition.GetDropDownList(ddlOLdPosition, True)
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
            If DdlNationality.SelectedValue > 0 Then
                ClsEmployeesDecisions.NationalityID = DdlNationality.SelectedValue
            Else ClsEmployeesDecisions.NationalityID = DBNull.Value
            End If

            If DdlMaritalStatus.SelectedValue > 0 Then
                ClsEmployeesDecisions.MaritalStatusID = DdlMaritalStatus.SelectedValue
            Else
                ClsEmployeesDecisions.MaritalStatusID = DBNull.Value
            End If

            If txtEmail.Text <> "" Then
                ClsEmployeesDecisions.E_Mail = txtEmail.Text
            Else
                ClsEmployeesDecisions.E_Mail = String.Empty
            End If

            If txtMobile.Text <> "" Then
                ClsEmployeesDecisions.Mobile = txtMobile.Text
            Else
                ClsEmployeesDecisions.Mobile = String.Empty
            End If
            If txtWorkE_Mail.Text <> "" Then
                ClsEmployeesDecisions.WorkE_Mail = txtWorkE_Mail.Text
            Else
                ClsEmployeesDecisions.WorkE_Mail = String.Empty
            End If

            If ddlBranch.SelectedValue > 0 Then
                ClsEmployeesDecisions.BranchID = ddlBranch.SelectedValue
            Else
                ClsEmployeesDecisions.BranchID = DBNull.Value
            End If

            'ddlDepartment.SelectedValue = ClsEmployees.DepartmentID
            If ddlDepartment.SelectedValue > 0 Then
                ClsEmployeesDecisions.DepartmentID = ddlDepartment.SelectedValue
            Else
                ClsEmployeesDecisions.DepartmentID = DBNull.Value
            End If

            'ddlLocation.SelectedValue = ClsEmployees.LocationID
            If ddlLocation.SelectedValue > 0 Then
                ClsEmployeesDecisions.LocationID = ddlLocation.SelectedValue
            Else
                ClsEmployeesDecisions.LocationID = DBNull.Value
            End If

            'ddlSectors.SelectedValue = ClsEmployees.SectorID
            If ddlSectors.SelectedValue > 0 Then
                ClsEmployeesDecisions.SectorID = ddlSectors.SelectedValue
            Else
                ClsEmployeesDecisions.SectorID = DBNull.Value
            End If

            'TxtCostCode1.Text = ClsEmployees.Cost1
            If TxtCostCode1.Text <> "" Then
                ClsEmployeesDecisions.Cost1 = TxtCostCode1.Text
            Else
                ClsEmployeesDecisions.Cost1 = DBNull.Value
            End If

            If TxtCostCode2.Text <> "" Then
                ClsEmployeesDecisions.Cost2 = TxtCostCode2.Text
            Else
                ClsEmployeesDecisions.Cost2 = DBNull.Value
            End If

            If TxtCostCode3.Text <> "" Then
                ClsEmployeesDecisions.Cost3 = TxtCostCode3.Text
            Else
                ClsEmployeesDecisions.Cost3 = DBNull.Value
            End If
            If TxtCostCode4.Text <> "" Then
                ClsEmployeesDecisions.Cost4 = TxtCostCode4.Text
            Else
                ClsEmployeesDecisions.Cost4 = DBNull.Value
            End If

            'txtManager.Text = ClsEmployees.ManagerID
            If txtManager.Text <> "" Then
                ClsEmployeesDecisions.ManagerID = txtManager.Text
            Else
                ClsEmployeesDecisions.ManagerID = DBNull.Value
            End If

            'ddlSponsor.SelectedValue = ClsEmployees.SponsorID
            If ddlSponsor.SelectedValue > 0 Then
                ClsEmployeesDecisions.SponsorID = ddlSponsor.SelectedValue
            Else
                ClsEmployeesDecisions.SponsorID = DBNull.Value
            End If

            'ddlBank.SelectedValue = ClsEmployees.BankID
            If ddlBank.SelectedValue > 0 Then
                ClsEmployeesDecisions.BankID = ddlBank.SelectedValue
            Else
                ClsEmployeesDecisions.BankID = DBNull.Value
            End If

            'txtBankAccount.Text = ClsEmployees.BankAccountNumber
            If txtBankAccount.Text <> "" Then
                ClsEmployeesDecisions.BankAccountNumber = txtBankAccount.Text
            Else
                ClsEmployeesDecisions.BankAccountNumber = String.Empty

            End If

            'txtPassport.Text = ClsEmployees.PassPortNo
            If txtPassport.Text <> "" Then
                ClsEmployeesDecisions.PassPortNo = txtPassport.Text
            Else
                ClsEmployeesDecisions.PassPortNo = String.Empty
            End If
            If txtIdentity.Text <> "" Then
                ClsEmployeesDecisions.SSnNo = txtIdentity.Text
            Else
                ClsEmployeesDecisions.SSnNo = String.Empty
            End If

            'Dim clsEmpEducations As New Clshrs_EmployeesEducations(Me)
            'If clsEmpEducations.Find("EmployeeID=" & ClsEmployees.ID) Then
            'ddlLastEducations.SelectedValue = clsEmpEducations.EducationTypeID
            If ddlLastEducations.SelectedValue > 0 Then
                ClsEmployeesDecisions.LastEducations = ddlLastEducations.SelectedValue
            Else
                ClsEmployeesDecisions.LastEducations = DBNull.Value
            End If

            'txtGraduationDate.Text = clsEmpEducations.GraduationDate
            If txtGraduationDate.Text <> "  /  /    " And txtGraduationDate.Text <> "12/00/00  " Then
                ClsEmployeesDecisions.GraduationDate = txtGraduationDate.Text
            Else
                ClsEmployeesDecisions.GraduationDate = DBNull.Value
            End If


            'End If

            'Dim clsContracts = New Clshrs_Contracts(Me.Page)
            '    Dim ObjDataHandler As New Venus.Shared.DataHandler
            '    Dim StrSerial As String = String.Empty

            '    ObjDataHandler.GetLastSerial(clsContracts.Table, "Number", StrSerial, clsContracts.ConnectionString)
            '    clsContracts.Find("Number=" & StrSerial)

            'ddlContractType.SelectedValue = clsContracts.ContractTypeId
            If ddlContractType.SelectedValue > 0 Then
                ClsEmployeesDecisions.ContractType = ddlContractType.SelectedValue
            Else
                ClsEmployeesDecisions.ContractType = Nothing
            End If

            'ddlProfessions.SelectedValue = clsContracts.ProfessionID
            If ddlProfessions.SelectedValue > 0 Then
                ClsEmployeesDecisions.Professions = ddlProfessions.SelectedValue
            Else
                ClsEmployeesDecisions.Professions = DBNull.Value
            End If

            'ddlPosition.SelectedValue = clsContracts.PositionID
            If ddlPosition.SelectedValue > 0 Then
                ClsEmployeesDecisions.Position = ddlPosition.SelectedValue
            Else
                ClsEmployeesDecisions.Position = DBNull.Value
            End If

            'ddlEmployeeClass.SelectedValue = clsContracts.EmployeeClassID
            If ddlEmployeeClass.SelectedValue > 0 Then
                ClsEmployeesDecisions.EmployeeClass = ddlEmployeeClass.SelectedValue
            Else
                ClsEmployeesDecisions.EmployeeClass = DBNull.Value
            End If

            ClsEmployeesDecisions.Save()

            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            If DdlNationality.SelectedValue > 0 Then
                ClsEmployees.NationalityID = DdlNationality.SelectedValue
            End If
            If DdlMaritalStatus.SelectedValue > 0 Then
                ClsEmployees.MaritalStatusID = DdlMaritalStatus.SelectedValue
            End If
            If txtEmail.Text <> "" Then
                ClsEmployees.E_Mail = txtEmail.Text
            End If
            If txtMobile.Text <> "" Then
                ClsEmployees.Mobile = txtMobile.Text
            End If
            If txtWorkE_Mail.Text <> "" Then
                ClsEmployees.WorkE_Mail = txtWorkE_Mail.Text
            End If
            If ddlBranch.SelectedValue > 0 Then
                ClsEmployees.BranchID = ddlBranch.SelectedValue
            End If

            'ddlDepartment.SelectedValue = ClsEmployees.DepartmentID
            If ddlDepartment.SelectedValue > 0 Then
                ClsEmployees.DepartmentID = ddlDepartment.SelectedValue
            End If

            'ddlLocation.SelectedValue = ClsEmployees.LocationID
            If ddlLocation.SelectedValue > 0 Then
                ClsEmployees.LocationID = ddlLocation.SelectedValue
            End If

            'ddlSectors.SelectedValue = ClsEmployees.SectorID
            If ddlSectors.SelectedValue > 0 Then
                ClsEmployees.SectorID = ddlSectors.SelectedValue
            End If

            'TxtCostCode1.Text = ClsEmployees.Cost1
            If TxtCostCode1.Text <> "" Then
                ClsEmployees.Cost1 = TxtCostCode1.Text
            End If
            If TxtCostCode2.Text <> "" Then
                ClsEmployees.Cost2 = TxtCostCode2.Text
            End If
            If TxtCostCode3.Text <> "" Then
                ClsEmployees.Cost3 = TxtCostCode3.Text
            End If
            If TxtCostCode4.Text <> "" Then
                ClsEmployees.Cost4 = TxtCostCode4.Text
            End If

            'txtManager.Text = ClsEmployees.ManagerID
            If txtManager.Text <> "" Then
                ClsEmployees.ManagerID = txtManager.Text
            End If

            'ddlSponsor.SelectedValue = ClsEmployees.SponsorID
            If ddlSponsor.SelectedValue > 0 Then
                ClsEmployees.SponsorID = ddlSponsor.SelectedValue
            End If

            'ddlBank.SelectedValue = ClsEmployees.BankID
            If ddlBank.SelectedValue > 0 Then
                ClsEmployees.BankID = ddlBank.SelectedValue
            End If

            'txtBankAccount.Text = ClsEmployees.BankAccountNumber
            If txtBankAccount.Text <> "" Then
                ClsEmployees.BankAccountNumber = txtBankAccount.Text
            End If

            'txtPassport.Text = ClsEmployees.PassPortNo
            If txtPassport.Text <> "" Then
                ClsEmployees.PassPortNo = txtPassport.Text
            End If

            If txtIdentity.Text <> "" Then
                ClsEmployees.SSnNo = txtIdentity.Text
            End If

            ClsEmployees.Update("Code='" & txtEmployee.Text & "'")

            Dim clsEmpEducations As New Clshrs_EmployeesEducations(Me)
            If clsEmpEducations.Find("EmployeeID=" & ClsEmployees.ID) Then
                'ddlLastEducations.SelectedValue = clsEmpEducations.EducationTypeID
                If ddlLastEducations.SelectedValue > 0 Then
                    clsEmpEducations.EducationTypeID = ddlLastEducations.SelectedValue
                End If

                'txtGraduationDate.Text = clsEmpEducations.GraduationDate
                If txtGraduationDate.Text <> "  /  /    " And txtGraduationDate.Text <> "12/00/00  " Then
                    clsEmpEducations.GraduationDate = txtGraduationDate.Text
                End If
                clsEmpEducations.Update("EmployeeID=" & ClsEmployees.ID)

                End If

                Dim clsContracts = New Clshrs_Contracts(Me.Page)
            Dim ObjDataHandler As New Venus.Shared.DataHandler
            '    Dim StrSerial As String = String.Empty

            '    ObjDataHandler.GetLastSerial(clsContracts.Table, "Number", StrSerial, clsContracts.ConnectionString)
            clsContracts.Find(" EmployeeId = " & ClsEmployees.ID & " And Isnull(CancelDate,'')= '' and (EndDate is null or EndDate > getdate()) order by ID desc")

            'ddlContractType.SelectedValue = clsContracts.ContractTypeId
            If ddlContractType.SelectedValue > 0 Then
                clsContracts.ContractTypeId = ddlContractType.SelectedValue
            End If

            'ddlProfessions.SelectedValue = clsContracts.ProfessionID
            If ddlProfessions.SelectedValue > 0 Then
                clsContracts.ProfessionID = ddlProfessions.SelectedValue
            End If

            'ddlPosition.SelectedValue = clsContracts.PositionID
            If ddlPosition.SelectedValue > 0 Then
                clsContracts.PositionID = ddlPosition.SelectedValue
            End If

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

                ddlOldNationality.SelectedValue = ClsEmployees.NationalityID
                ddlOldMaritalStatus.SelectedValue = ClsEmployees.MaritalStatusID
                txtOldEmail.Text = ClsEmployees.E_Mail
                txtOldMobile.Text = ClsEmployees.Mobile
                txtOldWorkEmail.Text = ClsEmployees.WorkE_Mail
                ddlOldBranch.SelectedValue = ClsEmployees.BranchID
                ddlOldDepartment.SelectedValue = ClsEmployees.DepartmentID
                ddlOldLocation.SelectedValue = ClsEmployees.LocationID
                ddlOldSectors.SelectedValue = ClsEmployees.SectorID
                txtOldCost1.Text = ClsEmployees.Cost1
                txtOldCost2.Text = ClsEmployees.Cost2
                txtOldCost3.Text = ClsEmployees.Cost3
                txtOldCost4.Text = ClsEmployees.Cost4
                txtOldManager.Text = ClsEmployees.ManagerID
                ddlOldSponsor.SelectedValue = ClsEmployees.SponsorID
                ddlOldBank.SelectedValue = ClsEmployees.BankID
                txtOldBankAccount.Text = ClsEmployees.BankAccountNumber
                txtOldPassport.Text = ClsEmployees.PassPortNo
                txtOldIdentity.Text = ClsEmployees.SSnNo

                Dim clsEmpEducations As New Clshrs_EmployeesEducations(Me)
                If clsEmpEducations.Find("EmployeeID=" & ClsEmployees.ID) Then
                    'ddlLastEducations.SelectedValue = clsEmpEducations.EducationTypeID
                    ClsEmployeesDecisions.PreviousLastEducations = clsEmpEducations.EducationTypeID
                    'txtGraduationDate.Text = clsEmpEducations.GraduationDate
                    ClsEmployeesDecisions.PreviousGraduationDate = clsEmpEducations.GraduationDate

                    ddlOldLastEducations.SelectedValue = clsEmpEducations.EducationTypeID
                    txtOldGraduationDate.Text = clsEmpEducations.GraduationDate

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

                    ddlOLdContractType.SelectedValue = clsContracts.ContractTypeId
                    ddlOLdProfessions.SelectedValue = clsContracts.ProfessionID
                    ddlOLdPosition.SelectedValue = clsContracts.PositionID
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
        ddlBank.SelectedIndex = -1
        ddlBranch.SelectedIndex = -1
        ddlContractType.SelectedIndex = -1
        ddlDepartment.SelectedIndex = -1
        ddlEmployeeClass.SelectedIndex = -1
        ddlLastEducations.SelectedIndex = -1
        ddlLocation.SelectedIndex = -1
        ddlPosition.SelectedIndex = -1
        ddlProfessions.SelectedIndex = -1
        ddlSectors.SelectedIndex = -1
        ddlSponsor.SelectedIndex = -1
        DdlMaritalStatus.SelectedIndex = -1
        DdlNationality.SelectedIndex = -1
        txtBankAccount.Text = ""
        txtEmail.Text = ""
        txtEngName.Text = ""
        txtAraName.Text = ""
        txtGraduationDate.Text = ""
        txtManager.Text = ""
        txtMobile.Text = ""
        txtPassport.Text = ""
        txtIdentity.Text = ""
        txtWorkE_Mail.Text = ""
        TxtCostCode1.Text = ""
        TxtCostCode1.Text = ""
        TxtCostCode2.Text = ""
        TxtCostCode4.Text = ""

        ddlOldBank.SelectedIndex = -1
        ddlOldBranch.SelectedIndex = -1
        ddlOLdContractType.SelectedIndex = -1
        ddlOldDepartment.SelectedIndex = -1
        ddlOLdEmployeeClass.SelectedIndex = -1
        ddlOldLastEducations.SelectedIndex = -1
        ddlOldLocation.SelectedIndex = -1
        ddlOLdPosition.SelectedIndex = -1
        ddlOLdProfessions.SelectedIndex = -1
        ddlOldSectors.SelectedIndex = -1
        ddlOldSponsor.SelectedIndex = -1
        ddlOldMaritalStatus.SelectedIndex = -1
        ddlOldNationality.SelectedIndex = -1
        txtOldBankAccount.Text = ""
        txtOldEmail.Text = ""
        txtOldGraduationDate.Text = ""
        txtOldManager.Text = ""
        txtOldMobile.Text = ""
        txtOldPassport.Text = ""
        txtOldIdentity.Text = ""
        txtOldWorkEmail.Text = ""
        txtOldCost1.Text = ""
        txtOldCost1.Text = ""
        txtOldCost2.Text = ""
        txtOldCost4.Text = ""

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function

    Protected Sub DisableControlsInTd()
        For Each ctrl As Control In myTd1.Controls
            If TypeOf ctrl Is WebControl Then
                CType(ctrl, WebControl).Enabled = False
            End If
        Next
        For Each ctrl As Control In myTd2.Controls
            If TypeOf ctrl Is WebControl Then
                CType(ctrl, WebControl).Enabled = False
            End If
        Next
        For Each ctrl As Control In myTd3.Controls
            If TypeOf ctrl Is WebControl Then
                CType(ctrl, WebControl).Enabled = False
            End If
        Next
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

#End Region

    Protected Sub TxtCostCode1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCostCode1.TextChanged, TxtCostCode1.DataBinding

        If Not String.IsNullOrEmpty(TxtCostCode1.Text) Then
            Dim str As String
            Dim CostCode As String = TxtCostCode1.Text
            Dim CostCenterName As String

            If ProfileCls.CurrentLanguage = "Ar" Then
                CostCenterName = "ArbName"
            Else
                CostCenterName = "EngName"
            End If
            str = "select " & CostCenterName & " From fcs_CostCenters1 where Code='" & CostCode & "' "
            Dim Cost1Name As String = ""
            Try
                Cost1Name = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, System.Data.CommandType.Text, str)
                TxtCostName1.Text = Cost1Name
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try
        Else

            TxtCostName1.Text = String.Empty

        End If



    End Sub
    Protected Sub ddlBranch_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBranch.SelectedIndexChanged
        If ddlBranch.SelectedValue <> 0 Then
            Dim ClsDepartment As New Clssys_Departments(Me.Page)
            ClsDepartment.GetDropDownList(ddlDepartment, True, "ID in (select DepartmentID from sys_DepartmentsBranches where Checked = 1 and BranchID = " & ddlBranch.SelectedValue & ")")
            ddlDepartment_SelectedIndexChanged(Nothing, Nothing)
            ddlBranch.Focus()
        End If
    End Sub

    Protected Sub ddlDepartment_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDepartment.SelectedIndexChanged
        If ddlDepartment.SelectedValue <> 0 Then
            Dim ClsSector As New ClsSys_Sectors(Me.Page)
            ClsSector.GetDropDownList(ddlSectors, True, "ID in (select SectorID from sys_SectorsDepartments where Checked = 1 and DepartmentID = " & ddlDepartment.SelectedValue & ")")
            ddlDepartment.Focus()
        End If
    End Sub

    Protected Sub TxtCostCode2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCostCode2.TextChanged
        If Not String.IsNullOrEmpty(TxtCostCode2.Text) Then
            Dim str As String
            Dim CostCode As String = TxtCostCode2.Text
            Dim CostCenterName As String

            If ProfileCls.CurrentLanguage = "Ar" Then
                CostCenterName = "ArbName"
            Else
                CostCenterName = "EngName"
            End If
            str = "select " & CostCenterName & " From fcs_CostCenters2 where Code='" & CostCode & "' "
            Dim Cost2Name As String = ""
            Try
                Cost2Name = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, System.Data.CommandType.Text, str)
                TxtCostName2.Text = Cost2Name
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try
        Else

            TxtCostName2.Text = String.Empty
        End If


    End Sub
    Protected Sub TxtCostCode3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCostCode3.TextChanged
        If Not String.IsNullOrEmpty(TxtCostCode3.Text) Then
            Dim str As String
            Dim CostCode As String = TxtCostCode3.Text
            Dim CostCenterName As String

            If ProfileCls.CurrentLanguage = "Ar" Then
                CostCenterName = "ArbName"
            Else
                CostCenterName = "EngName"
            End If
            str = "select " & CostCenterName & " From fcs_CostCenters3 where Code='" & CostCode & "' "
            Dim Cost3Name As String = ""
            Try
                Cost3Name = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, System.Data.CommandType.Text, str)
                TxtCostName3.Text = Cost3Name
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try
        Else

            TxtCostName3.Text = String.Empty
        End If


    End Sub

    Protected Sub TxtCostCode4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCostCode4.TextChanged
        If Not String.IsNullOrEmpty(TxtCostCode4.Text) Then
            Dim str As String
            Dim CostCode As String = TxtCostCode4.Text
            Dim CostCenterName As String

            If ProfileCls.CurrentLanguage = "Ar" Then
                CostCenterName = "ArbName"
            Else
                CostCenterName = "EngName"
            End If
            str = "select " & CostCenterName & " From fcs_CostCenters4 where Code='" & CostCode & "' "
            Dim Cost4Name As String = ""
            Try
                Cost4Name = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, System.Data.CommandType.Text, str)
                TxtCostName4.Text = Cost4Name
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try
        Else
            TxtCostName4.Text = String.Empty
        End If

    End Sub

End Class
