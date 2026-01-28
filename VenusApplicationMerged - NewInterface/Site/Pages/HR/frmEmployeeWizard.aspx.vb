Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports Infragistics.Documents.Reports.Report.Grid
Imports System.Runtime.CompilerServices.RuntimeHelpers
Imports C1.C1Rdl.Rdl2008

Partial Class frmEmployeeWizard
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim StrEmployee As String = Request.QueryString.Item("ID")

                Dim clsEmployees As New Clshrs_NewEmployee(Me)
                Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
                Dim ClsObjects As New Clssys_Objects(Me.Page)
                Dim ClsSearchs As New Clssys_Searchs(Me.Page)
                Dim csSearchID As Integer
                clsEmployees.Find(" ID = " & StrEmployee)

                lblProjectCode.Text = clsEmployees.ID
                lblProjectName.Text = IIf(ClsNavHandler.SetLanguage(Me, "0/1") = 0, clsEmployees.EngName, clsEmployees.ArbName)

                'If clsEmployees.IsLocked Then
                'CType(Me.UltraWebTab1.Tabs(0), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
                'Else
                CType(Me.UltraWebTab1.Tabs(0), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
                CType(Me.UltraWebTab1.Tabs(1), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
                'End If

                ToNewChangeID.Value = "0"
                Dim clsprojectchange As New Clshrs_NewEmployee(Me)
                If clsprojectchange.Find("ID = " & Request.QueryString.Item("ID") & " order by ID Desc") Then
                    CCHangeID.Value = clsprojectchange.ID
                    NewChangeID.Value = clsprojectchange.ID
                    GetData()
                End If

                Dim clsOrgEmployee As New Clshrs_Employees(Me)
                If ClsObjects.Find(" Code='" & clsOrgEmployee.Table.Trim() & "'") Then
                    If ClsSearchs.Find(" ObjectID=" & ClsObjects.ID) Then
                        csSearchID = ClsSearchs.ID

                        '' add by tal3at for search
                        Dim IntDimension As Integer = 510
                        Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,false,'" & txtCode.ClientID & "'"
                        'btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                        UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtManager.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,false,'" & txtManager.ClientID & "'"
                        btnManager.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                    End If
                End If

                If ClsObjects.Find(" Code='fcs_CostCenters1'") Then
                    If ClsSearchs.Find(" ObjectID=" & ClsObjects.ID) Then
                        csSearchID = ClsSearchs.ID

                        '' add by tal3at for search
                        Dim IntDimension As Integer = 510
                        Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtCostCode1.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,false,'" & TxtCostCode1.ClientID & "'"
                        WebImageButton1.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                    End If
                End If
                If ClsObjects.Find(" Code='fcs_CostCenters2'") Then
                    If ClsSearchs.Find(" ObjectID=" & ClsObjects.ID) Then
                        csSearchID = ClsSearchs.ID

                        '' add by tal3at for search
                        Dim IntDimension As Integer = 510
                        Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtCostCode2.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,false,'" & TxtCostCode2.ClientID & "'"
                        WebImageButton2.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                    End If
                End If

                If ClsObjects.Find(" Code='fcs_CostCenters3'") Then
                    If ClsSearchs.Find(" ObjectID=" & ClsObjects.ID) Then
                        csSearchID = ClsSearchs.ID

                        '' add by tal3at for search
                        Dim IntDimension As Integer = 510
                        Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtCostCode3.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,false,'" & TxtCostCode3.ClientID & "'"
                        WebImageButton3.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                    End If
                End If
                If ClsObjects.Find(" Code='fcs_CostCenters4'") Then
                    If ClsSearchs.Find(" ObjectID=" & ClsObjects.ID) Then
                        csSearchID = ClsSearchs.ID

                        '' add by tal3at for search
                        Dim IntDimension As Integer = 510
                        Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtCostCode4.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,false,'" & TxtCostCode4.ClientID & "'"
                        WebImageButton4.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                    End If
                End If
                'If ProfileCls.CurrentLanguage = "Ar" Then

                'Else

                'End If
                'Dim clsrewards As New Clshrs_Rewards(Me)
                'clsrewards.GetList(uwgBenfits.DisplayLayout.Bands(0).Columns(1).ValueList, True)

                'Dim clsenum As New Clshrs_Enum(Me)
                'clsenum.GetList(uwgBenfits.DisplayLayout.Bands(0).Columns(4).ValueList, True, "Flag = '1'")
                'clsenum.GetList(uwgBenfits.DisplayLayout.Bands(0).Columns(6).ValueList, True, "Flag = '1'")
                'clsenum.GetList(uwgPenalities.DisplayLayout.Bands(0).Columns(4).ValueList, True, "Flag = '1'")
                'clsenum.GetList(uwgPenalities.DisplayLayout.Bands(0).Columns(6).ValueList, True, "Flag = '1'")
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
                Dim Cls_Companies As New Clssys_Companies(Page)
                If Cls_Companies.Find("ID=" & Cls_Companies.MainCompanyID) = True Then
                    If Cls_Companies.HasSequence = True Then
                        txtEmployeeCode.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Auto/تلقائى")
                    End If
                End If

            End If
            'AddEventToControls()
        Catch ex As Exception
        End Try
    End Sub
    Public Function GetList_Data(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal ObjDataset As Data.DataSet) As Boolean
        Dim ObjDataRow As DataRow
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim clsEmployees As New Clshrs_NewEmployee(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
        Try
            DdlValues.ValueListItems.Clear()
            Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
            Item.DisplayText = ObjNavigationHandler.SetLanguage(Page, "[Select your choise]/[إختر أحد الأختيارات]")
            Item.DataValue = 0
            DdlValues.ValueListItems.Add(Item)
            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = IIf(IsDBNull(ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName"))),
                                       IIf(IsDBNull(ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "ArbName/EngName"))),
                                       "",
                                       ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "ArbName/EngName"))),
                                       ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")))
                Item.DataValue = ObjDataRow("ID")
                DdlValues.ValueListItems.Add(Item)
            Next
            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    Private Function GetData() As Boolean
        Try
            Dim clsprojectchange As New Clshrs_NewEmployee(Me)
            clsprojectchange.Find("ID = " & Request.QueryString.Item("ID") & " order by ID Desc")
            uwgChanges.DataSource = clsprojectchange.DataSet.Tables(0)
            uwgChanges.DataBind()

            Dim ClsNationality As New ClsBasicFiles(Me.Page, "sys_Nationalities")
            ClsNationality.GetDropDownList(DdlNationality, True)

            Dim clsBranch As New Clssys_Branches(Page)
            clsBranch.GetDropDownList(ddlBranch, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")

            Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
            ClsDepartment.GetDropDownList(ddlDepartment, True)

            Dim ClsSectors As New ClsSys_Sectors(Page)
            ClsSectors.GetDropDownList(ddlSectors, True)

            Dim Clslocation As New ClsBasicFiles(Me.Page, "sys_Locations")
            Clslocation.GetDropDownList(DropDownList_Location, True)

            Dim ClsContractsTypes As New Clshrs_ContractTypes(Me.Page)
            ClsContractsTypes.GetDropDownList(ddlContractType, True)

            Dim ClsProfission As New Clshrs_Professions(Me.Page)
            ClsProfission.GetDropDownList(ddlProfessions, True)

            Dim ClsPosition As New Clshrs_Positions(Me.Page)
            ClsPosition.GetDropDownList(ddlPosition, True)

            Dim ClsEmployeeClass As New Clshrs_EmployeeClasses(Me.Page)
            ClsEmployeeClass.GetDropDownList(ddlEmployeeClass, True)

            Dim ClsGradeStep As New Clshrs_GradesSteps(Me.Page)
            ClsGradeStep.GetDropDownList(ddlGradeStep, True)

            Dim ClsSponsor As New Clshrs_Sponsors(Page)
            'Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsSponsor.ConnectionString)
            ClsSponsor.GetDropDownList(ddlSponsor, True)

            Dim ClsEmployees = New Clshrs_Employees(Me.Page)
            ClsEmployees.GetDropDownList(ddlLblPaymentType, "Hrs_PaymentTypes", True, "")

            Dim ClsBirthCities As New ClsBasicFiles(Me.Page, "sys_Cities")
            Dim clsEducations As New Clshrs_Educations(Me, "hrs_Educations")
            Dim ClsRelegion As New ClsBasicFiles(Me.Page, "hrs_Religions")
            Dim ClsMaretalStatus As New ClsBasicFiles(Me.Page, "hrs_MaritalStatus")
            Dim ClsBloodGroup As New ClsBasicFiles(Me.Page, "hrs_BloodGroups")
            Dim ClsBanks As New ClsBasicFiles(Me.Page, "sys_Banks")
            Dim ClsCities As New Clssys_Cities(Me.Page)

            clsEducations.GetDropDownList(ddlLastEducations, True)
            ClsRelegion.GetDropDownList(DdlReligion, True)
            ClsMaretalStatus.GetDropDownList(DdlMaritalStatus, True)
            ClsBloodGroup.GetDropDownList(DdlBloodGroups, True)
            ClsNationality.GetDropDownList(DdlNationality, True)
            ClsBanks.GetDropDownList(ddlBank, True)
            ClsCities.GetDropDownList(DdlBirthCity, True)

            Dim clsprojectsetting As New Clshrs_NewEmployee(Me)
            If clsprojectsetting.Find("ID = " & NewChangeID.Value) Then
                txtArbName.Value = clsprojectsetting.ArbName
                txtEngName.Value = clsprojectsetting.EngName
                txtFamilyArbName.Value = clsprojectsetting.FamilyArbName
                txtFamilyEngName.Value = clsprojectsetting.FamilyEngName
                txtFatherArbName.Value = clsprojectsetting.FatherArbName
                txtFatherEngName.Value = clsprojectsetting.FatherEngName
                txtGrandArbName.Value = clsprojectsetting.GrandArbName
                txtGrandEngName.Value = clsprojectsetting.GrandEngName
                txtBirthDate.Value = Convert.ToDateTime(clsprojectsetting.BirthDate).ToString("yyyy/MM/dd")
                DdlNationality.SelectedValue = Convert.ToInt32(clsprojectsetting.NationalityID)
                DdlBirthCity.SelectedValue = clsprojectsetting.BirthCityID
                DdlReligion.SelectedValue = clsprojectsetting.ReligionID
                If clsprojectsetting.GenderID = 1 Then
                    DdlGender.SelectedValue = "F"
                Else
                    DdlGender.SelectedValue = "M"
                End If
                'DdlGender.SelectedValue = clsprojectsetting.GenderID 
                DdlMaritalStatus.SelectedValue = clsprojectsetting.MaritalStatusID
                DdlBloodGroups.SelectedValue = clsprojectsetting.BloodGroupID
                ddlBank.SelectedValue = clsprojectsetting.BankID
                ddlLastEducations.SelectedValue = clsprojectsetting.LastEducationCertificateID

                txtRemarks.Text = clsprojectsetting.Remarks
                'ddlBranch.SelectedValue = clsprojectchange.BranchID
                'ddlDepartment.SelectedValue = clsprojectchange.DepartmentID
                txtJoinDate.Text = Convert.ToDateTime(DateTime.Now).ToString("yyyy/MM/dd")
                txtStartWorkDate.Text = Convert.ToDateTime(DateTime.Now).ToString("yyyy/MM/dd")

                txtEmail.Text = clsprojectsetting.PersonalEmail
                txtMobile.Text = clsprojectsetting.Mobile
                txtPassport.Text = clsprojectsetting.PassPortNo
                txtBankAccount.Text = clsprojectsetting.IBANNo
                txtGraduationDate.Text = Convert.ToDateTime(clsprojectsetting.GraduationDate).ToString("yyyy/MM/dd")
                'ddlSponsor.SelectedValue = clsprojectchange.SponsorID
                txtIdentity.Text = clsprojectsetting.SSNO

                If clsprojectsetting.PassportExpireDate <> Nothing Then
                    txtPassportExpireDate.Text = clsprojectsetting.PassportExpireDate
                End If

                If clsprojectsetting.PassportIssueDate <> Nothing Then
                    txtPassportIssueDate.Text = Convert.ToDateTime(clsprojectsetting.PassportIssueDate).ToString("yyyy/MM/dd")
                End If

                If clsprojectsetting.SSNOExpireDate <> Nothing Then
                    txtSSNOExpireDate.Text = Convert.ToDateTime(clsprojectsetting.SSNOExpireDate).ToString("yyyy/MM/dd")
                End If

                If clsprojectsetting.SSNOIssueDate <> Nothing Then
                    txtSSNOIssueDate.Text = Convert.ToDateTime(clsprojectsetting.SSNOIssueDate).ToString("yyyy/MM/dd")
                End If


                If clsprojectsetting.AddressAsPerContract <> String.Empty Then
                    txtAddressAsPerContract.Text = clsprojectsetting.AddressAsPerContract
                End If
                'txtStartDate.Value = Convert.ToDateTime(clsprojectsetting.RegDate).ToString("yyyy/MM/dd")
                GetContractsData(clsprojectsetting.ID)
                If clsprojectsetting.ProfissionID <> Nothing Then
                    ddlProfessions.SelectedValue = clsprojectsetting.ProfissionID
                End If
                'ddlContractType.SelectedValue = clsprojectchange.ContractTypeId
                'ddlProfessions.SelectedValue = clsprojectchange.pro



                '    txtExternalOvertimeFactor.Value = clsprojectsetting.ExternalOvertimeFactor
                '    txtInternalHolidayFactor.Value = clsprojectsetting.InternalDayOffOvertimeFactor
                '    txtExternalHolidayFactor.Value = clsprojectsetting.ExternalDayOffOvertimeFactor
                '    WebNumericEdit_InternalExtension.Value = clsprojectsetting.InternalExtensionValue
                '    WebNumericEdit_ExternalExtension.Value = clsprojectsetting.ExternalExtensionValue
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetAlignmentPre() As String
        ' Example condition for dynamic alignment

        If ProfileCls.CurrentLanguage = "Ar" Then
            Return "right"
        End If
        Return "left"
    End Function
    Public Function GetAlignmentNext() As String
        ' Example condition for dynamic alignment

        If ProfileCls.CurrentLanguage = "Ar" Then
            Return "left"
        End If
        Return "right"
    End Function
    Private Function GetContractsData(ByVal IntEmployeeId As Integer) As Boolean
        Dim ClsVacationsType As New Clshrs_VacationsTypes(Me.Page)
        'ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsVacationsType.ConnectionString)
        Dim IntValidContract As Integer
        Dim blnFound As Boolean = False
        Dim ObjDs As Data.DataSet = Nothing
        Dim ClsContracts As New Clshrs_Contracts(Me.Page)
        ClsContracts.Find(" EmployeeId = " & IntEmployeeId & " And Isnull(CancelDate,'')= '' and (EndDate is null or EndDate > getdate()) order by ID desc")


        If IntEmployeeId > 0 Then
            IntValidContract = ClsContracts.ID
            GetValidContractData(IntValidContract)
        End If
        Return True
    End Function
    Private Function GetValidContractData(ByVal intValidContract As Integer) As Boolean
        Dim IntContractId As Integer
        Dim ObjDs As New Data.DataSet
        Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Page)

        Dim ClsContracts As New Clshrs_Contracts(Me.Page)

        IntContractId = intValidContract
        ClsContracts.Find(" ID = " & IntContractId)
        GetValues_Contracts(ClsContracts)


    End Function

    Private Function GetValues_Contracts(ByRef ClsContracts As Clshrs_Contracts) As Boolean
        Dim ClsUser As New Clssys_Users(Me.Page)
        Try

            LoadEmp_ContractCanceledItems(ClsContracts)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub AddEventToControls()
        btnManager.Attributes.Add("onclick", "OpenModal1('frmModalSearchScreen.aspx?TargetControl=" & txtManager.ID & "&SearchID=" & 95 & "&'," & 510 & ",720,false,false,'" & txtManager.ClientID & "')")

    End Sub
    Public Function LoadEmp_ContractCanceledItems(ByRef ClsContracts As Clshrs_Contracts) As Boolean
        Dim ClsContractsTypes As New Clshrs_ContractTypes(Me.Page)
        Dim ClsProfission As New ClsBasicFiles(Me.Page, "hrs_Professions")
        Dim ClsPosition As New ClsBasicFiles(Me.Page, "hrs_Positions")
        Dim ClsEmployeeClass As New ClsBasicFiles(Me.Page, "hrs_EmployeesClasses")
        Dim ClsGradeStep As New Clshrs_GradesSteps(Me.Page)
        Dim ClsEmployees As New Clshrs_NewEmployee(Me)

        'txtStartDate.Value = ClsContracts.StartDate

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Try
            With ClsContracts

                Dim datestart As String
                datestart = .StartDate

                If .StartDate <> Nothing Then
                    txtStartWorkDate.Text = DateTime.Now.Date.ToString("yyyy/MM/dd")
                End If

                Dim item As New System.Web.UI.WebControls.ListItem()

                ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(.ConnectionString)
                ClsContractsTypes.Find(" ID= " & IIf(IsNothing(.ContractTypeId), 0, .ContractTypeId))
                If ClsContractsTypes.ID > 0 Then
                    item.Value = .ContractTypeId
                    item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsContractsTypes.EngName & "/" & ClsContractsTypes.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsContractsTypes.ArbName & "/" & ClsContractsTypes.EngName)
                    End If
                    If Not ddlContractType.Items.Contains(item) Then
                        ddlContractType.Items.Add(item)
                        ddlContractType.SelectedValue = item.Value
                    Else
                        ddlContractType.SelectedValue = .ContractTypeId
                    End If
                End If
                item = New System.Web.UI.WebControls.ListItem()
                ClsProfission.Find(" ID= " & IIf(IsNothing(.ProfessionID), 0, .ProfessionID))
                If ClsProfission.ID > 0 Then
                    item.Value = .ProfessionID

                    If Not ddlProfessions.Items.Contains(item) Then
                        ddlProfessions.Items.Add(item)
                        ddlProfessions.SelectedValue = item.Value
                    Else
                        ddlProfessions.SelectedValue = .ProfessionID
                    End If
                End If
                item = New System.Web.UI.WebControls.ListItem()
                ClsPosition.Find(" ID= " & IIf(IsNothing(.PositionID), 0, .PositionID))
                If ClsPosition.ID > 0 Then
                    item.Value = .PositionID
                    If (item.Text.Trim = "") Then
                    End If
                    If Not ddlPosition.Items.Contains(item) Then
                        ddlPosition.Items.Add(item)
                        ddlPosition.SelectedValue = item.Value
                    Else
                        ddlPosition.SelectedValue = .PositionID
                    End If
                End If
                item = New System.Web.UI.WebControls.ListItem()
                ClsEmployeeClass.Find(" ID= " & IIf(IsNothing(.EmployeeClassID), 0, .EmployeeClassID))
                If ClsEmployeeClass.ID > 0 Then
                    item.Value = .EmployeeClassID
                    'item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsEmployeeClass.EngName & "/" & ClsEmployeeClass.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsEmployeeClass.ArbName & "/" & ClsEmployeeClass.EngName)
                    End If
                    If Not ddlEmployeeClass.Items.Contains(item) Then
                        ddlEmployeeClass.Items.Add(item)
                        ddlEmployeeClass.SelectedValue = item.Value
                    Else
                        ddlEmployeeClass.SelectedValue = .EmployeeClassID
                    End If
                End If
                item = New System.Web.UI.WebControls.ListItem()
                ClsGradeStep.Find(" hrs_GradesSteps.ID= " & IIf(IsNothing(.GradeStepId), 0, .GradeStepId))
                If ClsGradeStep.ID > 0 Then
                    item.Value = .GradeStepId
                    item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsGradeStep.EngName & "/" & ClsGradeStep.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsGradeStep.ArbName & "/" & ClsGradeStep.EngName)
                    End If
                    If Not ddlGradeStep.Items.Contains(item) Then
                        ddlGradeStep.Items.Add(item)
                        ddlGradeStep.SelectedValue = item.Value
                    Else
                        ddlGradeStep.SelectedValue = .GradeStepId
                    End If
                End If


                item = New System.Web.UI.WebControls.ListItem()

            End With
        Catch ex As Exception

        End Try
    End Function
    Private Function SetDate(gData As Object, hDate As Object) As Date
        Try

            If gData <> "  /  /    " Then
                If ClsDataAcessLayer.IsGreg(gData) Then
                    Return ClsDataAcessLayer.FormatGreg(gData, "yyyy/MM/dd")
                Else
                    Return ClsDataAcessLayer.HijriToGreg(gData, "yyyy/MM/dd")
                End If
            ElseIf hDate <> "  /  /    " Then
                If ClsDataAcessLayer.IsHijri(hDate) Then
                    Return ClsDataAcessLayer.HijriToGreg(hDate, "yyyy/MM/dd")
                Else
                    Return ClsDataAcessLayer.FormatGreg(hDate, "yyyy/MM/dd")
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function SetToDate(NChange As Integer) As Boolean
        'Modification Details
        Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
        clsprojectchange.ProjectID = Request.QueryString.Item("ProjID")
        clsprojectchange.FromDate = SetDate(txtEndDate.Text, txtEndDate.Text)
        clsprojectchange.Remarks = txtCompanyConditions.Text
        NewChangeID.Value = clsprojectchange.Save()

        'Add hrs_ProjectRewards
        Dim clsprojectrewards As New Clshrs_ProjectRewards(Me)
        If clsprojectrewards.Find("ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectrewards.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectrewards = New Clshrs_ProjectRewards(Me)
                clsprojectrewards.ProjectChangeID = NewChangeID.Value
                clsprojectrewards.RewardID = row("RewardID")
                clsprojectrewards.Occurance = row("Occurance")
                clsprojectrewards.ExternalValue = row("ExternalValue")
                clsprojectrewards.ExternalFactor = row("ExternalFactor")
                clsprojectrewards.InternalValue = row("InternalValue")
                clsprojectrewards.InternalFactor = row("InternalFactor")
                clsprojectrewards.Save()
            Next
        End If

        'Add hrs_ProjectPenalities
        Dim clsprojectpenalities As New Clshrs_ProjectPenalities(Me)
        If clsprojectpenalities.Find("ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectpenalities.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectpenalities = New Clshrs_ProjectPenalities(Me)
                clsprojectpenalities.ProjectChangeID = NewChangeID.Value
                clsprojectpenalities.PenaltyID = row("PenaltyID")
                clsprojectpenalities.Occurance = row("Occurance")
                clsprojectpenalities.ExternalValue = row("ExternalValue")
                clsprojectpenalities.ExternalFactor = row("ExternalFactor")
                clsprojectpenalities.InternalValue = row("InternalValue")
                clsprojectpenalities.InternalFactor = row("InternalFactor")
                clsprojectpenalities.Save()
            Next
        End If

        'Add hrs_ProjectSetting
        Dim clsprojectsetting As New Clshrs_ProjectSetting(Me)
        If clsprojectsetting.Find("ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectsetting.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectsetting = New Clshrs_ProjectSetting(Me)
                clsprojectsetting.ProjectChangeID = NewChangeID.Value
                clsprojectsetting.InternalOvertimeFactor = row("InternalOvertimeFactor")
                clsprojectsetting.ExternalOvertimeFactor = row("ExternalOvertimeFactor")
                clsprojectsetting.InternalDayOffOvertimeFactor = row("InternalDayOffOvertimeFactor")
                clsprojectsetting.ExternalDayOffOvertimeFactor = row("ExternalDayOffOvertimeFactor")
                clsprojectsetting.InternalExtensionValue = row("InternalExtensionValue")
                clsprojectsetting.ExternalExtensionValue = row("ExternalExtensionValue")
                clsprojectsetting.InternalAbsentFactor = row("InternalAbsentFactor")
                clsprojectsetting.ExternalAbsentFactor = row("ExternalAbsentFactor")
                clsprojectsetting.InternalSickFactor = row("InternalSickFactor")
                clsprojectsetting.ExternalSickFactor = row("ExternalSickFactor")
                clsprojectsetting.InternalLeavFactor = row("InternalLeavFactor")
                clsprojectsetting.ExternalLeavFactor = row("ExternalLeavFactor")
                clsprojectsetting.InternalPermitDelayFactor = row("InternalPermitDelayFactor")
                clsprojectsetting.ExternalPermitDelayFactor = row("ExternalPermitDelayFactor")
                clsprojectsetting.InternalDelayPunishFactor = row("InternalDelayPunishFactor")
                clsprojectsetting.ExternalDelayPunishFactor = row("ExternalDelayPunishFactor")
                clsprojectsetting.Save()
            Next
        End If

        'Add hrs_ProjectLocations
        Dim clsprojectlocation As New Clshrs_ProjectLocations(Me)
        If clsprojectlocation.Find("MainLocationID is null and ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectlocation.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectlocation = New Clshrs_ProjectLocations(Me)
                clsprojectlocation.ProjectChangeID = NewChangeID.Value
                If Convert.ToString(row("MainLocationID")) <> "" Then
                    clsprojectlocation.MainLocationID = row("MainLocationID")
                End If
                clsprojectlocation.LocationDescription = row("LocationDescription")
                clsprojectlocation.LocationAddress = row("LocationAddress")
                clsprojectlocation.Required = row("Required")
                clsprojectlocation.LinkedCS = Convert.ToString(row("LinkedCS"))
                If Convert.ToString(row("Supervisor")) <> "" Then
                    clsprojectlocation.Supervisor = row("Supervisor")
                End If
                Dim CLocation As Integer = clsprojectlocation.Save()

                Dim MaininsUserEntry As String = "insert into hrs_ProjectLocationUsers (ProjectLocationID,UserID) select " & CLocation & ",UserID from hrs_ProjectLocationUsers where ProjectLocationID = " & row("ID")
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectlocation.ConnectionString, Data.CommandType.Text, MaininsUserEntry)

                'Add hrs_ProjectLocationDetails
                Dim clsprojectlocationdetails As New Clshrs_ProjectLocationDetails(Me)
                If clsprojectlocationdetails.Find("LocationID = " & row("ID")) Then
                    Dim dt01 As DataTable = clsprojectlocationdetails.DataSet.Tables(0)
                    For Each row01 As DataRow In dt01.Rows
                        clsprojectlocationdetails = New Clshrs_ProjectLocationDetails(Me)
                        clsprojectlocationdetails.LocationID = CLocation
                        clsprojectlocationdetails.PositionID = row01("PositionID")
                        clsprojectlocationdetails.Qty = row01("Qty")
                        clsprojectlocationdetails.InternalAmt = row01("InternalAmt")
                        clsprojectlocationdetails.ExternalAmt = row01("ExternalAmt")
                        clsprojectlocationdetails.WeekDays = row01("WeekDays")
                        clsprojectlocationdetails.IsAlternative = row01("IsAlternative")
                        clsprojectlocationdetails.IsInvoiced = row01("IsInvoiced")
                        clsprojectlocationdetails.Remarks = row01("Remarks")
                        Dim CLocationDetails As Integer = clsprojectlocationdetails.Save()

                        'Add hrs_ProjectLocationShifts
                        Dim clsprojectlocationshift As New Clshrs_ProjectLocationShifts(Me)
                        If clsprojectlocationshift.Find("LocationDetailID = " & row01("ID")) Then
                            Dim dt02 As DataTable = clsprojectlocationshift.DataSet.Tables(0)
                            For Each row02 As DataRow In dt02.Rows
                                clsprojectlocationshift = New Clshrs_ProjectLocationShifts(Me)
                                clsprojectlocationshift.LocationDetailID = CLocationDetails
                                clsprojectlocationshift.AttendanceTableShiftID = row02("AttendanceTableShiftID")
                                clsprojectlocationshift.Qty = row02("Qty")
                                Dim CLocationShift As Integer = clsprojectlocationshift.Save()
                            Next
                        End If

                        'Add hrs_ProjectPlacements
                        Dim clsprojectplacement As New Clshrs_ProjectPlacements(Me)
                        If clsprojectplacement.Find("ProjectChangeID = " & CCHangeID.Value & " and LocationDetailID = " & row01("ID")) Then
                            Dim dt03 As DataTable = clsprojectplacement.DataSet.Tables(0)
                            For Each row03 As DataRow In dt03.Rows
                                clsprojectplacement = New Clshrs_ProjectPlacements(Me)
                                clsprojectplacement.PlacementCode = row03("PlacementCode")
                                clsprojectplacement.ProjectID = row03("ProjectID")
                                clsprojectplacement.ProjectChangeID = NewChangeID.Value
                                clsprojectplacement.LocationID = CLocation
                                clsprojectplacement.LocationDetailID = CLocationDetails
                                If Convert.ToString(row03("LoadPCT")) <> "" Then
                                    clsprojectplacement.LoadPCT = row03("LoadPCT")
                                End If
                                If Convert.ToString(row03("RegComputerID")) <> "" Then
                                    clsprojectplacement.RegComputerID = row03("RegComputerID")
                                End If
                                Dim CPlacementID As Integer = clsprojectplacement.Save()

                                'Add hrs_ProjectPlacementPlanning
                                Dim clsprojectplacementplanning As New Clshrs_ProjectPlacementPlanning(Me)
                                If clsprojectplacementplanning.Find("PlacementID = " & row03("ID")) Then
                                    Dim dt04 As DataTable = clsprojectplacementplanning.DataSet.Tables(0)
                                    For Each row04 As DataRow In dt04.Rows
                                        clsprojectplacementplanning = New Clshrs_ProjectPlacementPlanning(Me)
                                        If Convert.ToString(row04("AttendanceTableShiftID")) <> "" Then
                                            clsprojectplacementplanning.AttendanceTableShiftID = row04("AttendanceTableShiftID")
                                        End If
                                        clsprojectplacementplanning.PlacementID = CPlacementID
                                        clsprojectplacementplanning.DayID = row04("DayID")
                                        If Convert.ToString(row04("ReferenceTo")) <> "" Then
                                            clsprojectplacementplanning.ReferenceTo = row04("ReferenceTo")
                                        End If
                                        clsprojectplacementplanning.Save()
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If

                'Add hrs_ProjectLocationsDetails
                Dim clsprojectlocation3 As New Clshrs_ProjectLocations(Me)
                If clsprojectlocation3.Find("MainLocationID = " & row("ID") & " and ProjectChangeID = " & CCHangeID.Value) Then
                    Dim dt3 As DataTable = clsprojectlocation3.DataSet.Tables(0)
                    For Each row3 As DataRow In dt3.Rows
                        clsprojectlocation3 = New Clshrs_ProjectLocations(Me)
                        clsprojectlocation3.ProjectChangeID = NewChangeID.Value
                        If Convert.ToString(row3("MainLocationID")) <> "" Then
                            clsprojectlocation3.MainLocationID = CLocation
                        End If
                        clsprojectlocation3.LocationDescription = row3("LocationDescription")
                        clsprojectlocation3.LocationAddress = row3("LocationAddress")
                        clsprojectlocation3.Required = row3("Required")
                        clsprojectlocation3.LinkedCS = Convert.ToString(row3("LinkedCS"))
                        If Convert.ToString(row3("Supervisor")) <> "" Then
                            clsprojectlocation3.Supervisor = row3("Supervisor")
                        End If
                        Dim CLocation1 As Integer = clsprojectlocation3.Save()

                        Dim SubinsUserEntry As String = "insert into hrs_ProjectLocationUsers (ProjectLocationID,UserID) select " & CLocation1 & ",UserID from hrs_ProjectLocationUsers where ProjectLocationID = " & row3("ID")
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectlocation.ConnectionString, Data.CommandType.Text, SubinsUserEntry)

                        'Add hrs_ProjectLocationDetails
                        Dim clsprojectlocation3details As New Clshrs_ProjectLocationDetails(Me)
                        If clsprojectlocation3details.Find("LocationID = " & row3("ID")) Then
                            Dim dt301 As DataTable = clsprojectlocation3details.DataSet.Tables(0)
                            For Each row301 As DataRow In dt301.Rows
                                clsprojectlocation3details = New Clshrs_ProjectLocationDetails(Me)
                                clsprojectlocation3details.LocationID = CLocation1
                                clsprojectlocation3details.PositionID = row301("PositionID")
                                clsprojectlocation3details.Qty = row301("Qty")
                                clsprojectlocation3details.InternalAmt = row301("InternalAmt")
                                clsprojectlocation3details.ExternalAmt = row301("ExternalAmt")
                                clsprojectlocation3details.WeekDays = row301("WeekDays")
                                clsprojectlocation3details.IsAlternative = row301("IsAlternative")
                                clsprojectlocation3details.IsInvoiced = row301("IsInvoiced")
                                clsprojectlocation3details.Remarks = row301("Remarks")
                                Dim CLocationDetails As Integer = clsprojectlocation3details.Save()

                                'Add hrs_ProjectLocationShifts
                                Dim clsprojectlocation3shift As New Clshrs_ProjectLocationShifts(Me)
                                If clsprojectlocation3shift.Find("LocationDetailID = " & row301("ID")) Then
                                    Dim dt302 As DataTable = clsprojectlocation3shift.DataSet.Tables(0)
                                    For Each row302 As DataRow In dt302.Rows
                                        clsprojectlocation3shift = New Clshrs_ProjectLocationShifts(Me)
                                        clsprojectlocation3shift.LocationDetailID = CLocationDetails
                                        clsprojectlocation3shift.AttendanceTableShiftID = row302("AttendanceTableShiftID")
                                        clsprojectlocation3shift.Qty = row302("Qty")
                                        Dim CLocationShift As Integer = clsprojectlocation3shift.Save()
                                    Next
                                End If

                                'Add hrs_ProjectPlacements
                                Dim clsprojectplacement As New Clshrs_ProjectPlacements(Me)
                                If clsprojectplacement.Find("ProjectChangeID = " & CCHangeID.Value & " and LocationDetailID = " & row301("ID")) Then
                                    Dim dt303 As DataTable = clsprojectplacement.DataSet.Tables(0)
                                    For Each row303 As DataRow In dt303.Rows
                                        clsprojectplacement = New Clshrs_ProjectPlacements(Me)
                                        clsprojectplacement.PlacementCode = row303("PlacementCode")
                                        clsprojectplacement.ProjectID = row303("ProjectID")
                                        clsprojectplacement.ProjectChangeID = NewChangeID.Value
                                        clsprojectplacement.LocationID = CLocation1
                                        clsprojectplacement.LocationDetailID = CLocationDetails
                                        If Convert.ToString(row303("LoadPCT")) <> "" Then
                                            clsprojectplacement.LoadPCT = row303("LoadPCT")
                                        End If
                                        If Convert.ToString(row303("RegComputerID")) <> "" Then
                                            clsprojectplacement.RegComputerID = row303("RegComputerID")
                                        End If
                                        Dim CPlacementID As Integer = clsprojectplacement.Save()

                                        'Add hrs_ProjectPlacementPlanning
                                        Dim clsprojectplacementplanning As New Clshrs_ProjectPlacementPlanning(Me)
                                        If clsprojectplacementplanning.Find("PlacementID = " & row303("ID")) Then
                                            Dim dt304 As DataTable = clsprojectplacementplanning.DataSet.Tables(0)
                                            For Each row304 As DataRow In dt304.Rows
                                                clsprojectplacementplanning = New Clshrs_ProjectPlacementPlanning(Me)
                                                If Convert.ToString(row304("AttendanceTableShiftID")) <> "" Then
                                                    clsprojectplacementplanning.AttendanceTableShiftID = row304("AttendanceTableShiftID")
                                                End If
                                                clsprojectplacementplanning.PlacementID = CPlacementID
                                                clsprojectplacementplanning.DayID = row304("DayID")
                                                If Convert.ToString(row304("ReferenceTo")) <> "" Then
                                                    clsprojectplacementplanning.ReferenceTo = row304("ReferenceTo")
                                                End If
                                                clsprojectplacementplanning.Save()
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        End If
        ToNewChangeID.Value = NewChangeID.Value
        NewChangeID.Value = NChange
        Return True
    End Function

    Protected Sub LinkButton2_Click(sender As Object, e As System.EventArgs) Handles LinkButton2.Click
        'Modification Details
        Dim hrsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(hrsProjectLocationDetails.ConnectionString)

        Dim clsprojectchange123 As New Clshrs_ProjectChanges(Me)
        If clsprojectchange123.Find("ProjectID = " & Request.QueryString.Item("ProjID") & " and RegComputerID = 1 order by ID Desc") Then
            If SetDate(txtStartDate.Text, txtStartDate.Text) <= clsprojectchange123.FromDate Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Start Date Must Be More Than Last Change/تاريخ بداية التفعيل لابد وان يكون اكبر من تاريخ أخر تعديل "))
                Return
            End If
        End If
        If txtEndDate.Text <> "  /  /    " Then
            If SetDate(txtEndDate.Text, txtEndDate.Text) <= SetDate(txtStartDate.Text, txtStartDate.Text) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Please Select Department !/عفوا ... لابد من تحديد القسم !"))
                Return
            End If
        End If
        If ddlBranch.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Please Select Branch !/عفوا ... لابد من تحديد الفرع !"))
            Return
        End If
        If String.IsNullOrEmpty(txtManager.Text) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Please Select Direct Manager !/عفوا ... لابد من تحديد المدير المباشر !"))
            Return
        End If
        If DropDownList_Location.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Please Select Unit !/عفوا ... لابد من تحديد الوحدة !"))
            Return
        End If
        If ddlSectors.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Please Select Sector !/عفوا ... لابد من تحديدالقطاع !"))
            Return
        End If
        If ddlDepartment.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Invalid Dates/التواريخ غير منضبطة"))
            Return
        End If

        Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
        clsprojectchange.ProjectID = Request.QueryString.Item("ProjID")
        clsprojectchange.FromDate = SetDate(txtStartDate.Text, txtStartDate.Text)
        clsprojectchange.Remarks = txtCompanyConditions.Text
        NewChangeID.Value = clsprojectchange.Save()

        'Add hrs_ProjectRewards
        Dim clsprojectrewards As New Clshrs_ProjectRewards(Me)
        If clsprojectrewards.Find("ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectrewards.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectrewards = New Clshrs_ProjectRewards(Me)
                clsprojectrewards.ProjectChangeID = NewChangeID.Value
                clsprojectrewards.RewardID = row("RewardID")
                clsprojectrewards.Occurance = row("Occurance")
                clsprojectrewards.ExternalValue = row("ExternalValue")
                clsprojectrewards.ExternalFactor = row("ExternalFactor")
                clsprojectrewards.InternalValue = row("InternalValue")
                clsprojectrewards.InternalFactor = row("InternalFactor")
                clsprojectrewards.Save()
            Next
        End If

        'Add hrs_ProjectPenalities
        Dim clsprojectpenalities As New Clshrs_ProjectPenalities(Me)
        If clsprojectpenalities.Find("ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectpenalities.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectpenalities = New Clshrs_ProjectPenalities(Me)
                clsprojectpenalities.ProjectChangeID = NewChangeID.Value
                clsprojectpenalities.PenaltyID = row("PenaltyID")
                clsprojectpenalities.Occurance = row("Occurance")
                clsprojectpenalities.ExternalValue = row("ExternalValue")
                clsprojectpenalities.ExternalFactor = row("ExternalFactor")
                clsprojectpenalities.InternalValue = row("InternalValue")
                clsprojectpenalities.InternalFactor = row("InternalFactor")
                clsprojectpenalities.Save()
            Next
        End If

        'Add hrs_ProjectSetting
        Dim clsprojectsetting As New Clshrs_ProjectSetting(Me)
        If clsprojectsetting.Find("ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectsetting.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectsetting = New Clshrs_ProjectSetting(Me)
                clsprojectsetting.ProjectChangeID = NewChangeID.Value
                clsprojectsetting.InternalOvertimeFactor = row("InternalOvertimeFactor")
                clsprojectsetting.ExternalOvertimeFactor = row("ExternalOvertimeFactor")
                clsprojectsetting.InternalDayOffOvertimeFactor = row("InternalDayOffOvertimeFactor")
                clsprojectsetting.ExternalDayOffOvertimeFactor = row("ExternalDayOffOvertimeFactor")
                clsprojectsetting.InternalExtensionValue = row("InternalExtensionValue")
                clsprojectsetting.ExternalExtensionValue = row("ExternalExtensionValue")
                clsprojectsetting.InternalAbsentFactor = row("InternalAbsentFactor")
                clsprojectsetting.ExternalAbsentFactor = row("ExternalAbsentFactor")
                clsprojectsetting.InternalSickFactor = row("InternalSickFactor")
                clsprojectsetting.ExternalSickFactor = row("ExternalSickFactor")
                clsprojectsetting.InternalLeavFactor = row("InternalLeavFactor")
                clsprojectsetting.ExternalLeavFactor = row("ExternalLeavFactor")
                clsprojectsetting.InternalPermitDelayFactor = row("InternalPermitDelayFactor")
                clsprojectsetting.ExternalPermitDelayFactor = row("ExternalPermitDelayFactor")
                clsprojectsetting.InternalDelayPunishFactor = row("InternalDelayPunishFactor")
                clsprojectsetting.ExternalDelayPunishFactor = row("ExternalDelayPunishFactor")
                clsprojectsetting.Save()
            Next
        End If

        'Add hrs_ProjectLocations
        Dim clsprojectlocation As New Clshrs_ProjectLocations(Me)
        If clsprojectlocation.Find("MainLocationID is null and ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectlocation.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectlocation = New Clshrs_ProjectLocations(Me)
                clsprojectlocation.ProjectChangeID = NewChangeID.Value
                If Convert.ToString(row("MainLocationID")) <> "" Then
                    clsprojectlocation.MainLocationID = row("MainLocationID")
                End If
                clsprojectlocation.LocationDescription = row("LocationDescription")
                clsprojectlocation.LocationAddress = row("LocationAddress")
                clsprojectlocation.Required = row("Required")
                clsprojectlocation.LinkedCS = Convert.ToString(row("LinkedCS"))
                If Convert.ToString(row("Supervisor")) <> "" Then
                    clsprojectlocation.Supervisor = row("Supervisor")
                End If
                Dim CLocation As Integer = clsprojectlocation.Save()

                Dim MaininsUserEntry As String = "insert into hrs_ProjectLocationUsers (ProjectLocationID,UserID) select " & CLocation & ",UserID from hrs_ProjectLocationUsers where ProjectLocationID = " & row("ID")
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectlocation.ConnectionString, Data.CommandType.Text, MaininsUserEntry)

                'Add hrs_ProjectLocationDetails
                Dim clsprojectlocationdetails As New Clshrs_ProjectLocationDetails(Me)
                If clsprojectlocationdetails.Find("LocationID = " & row("ID")) Then
                    Dim dt01 As DataTable = clsprojectlocationdetails.DataSet.Tables(0)
                    For Each row01 As DataRow In dt01.Rows
                        clsprojectlocationdetails = New Clshrs_ProjectLocationDetails(Me)
                        clsprojectlocationdetails.LocationID = CLocation
                        clsprojectlocationdetails.PositionID = row01("PositionID")
                        clsprojectlocationdetails.Qty = row01("Qty")
                        clsprojectlocationdetails.InternalAmt = row01("InternalAmt")
                        clsprojectlocationdetails.ExternalAmt = row01("ExternalAmt")
                        clsprojectlocationdetails.WeekDays = row01("WeekDays")
                        clsprojectlocationdetails.IsAlternative = row01("IsAlternative")
                        clsprojectlocationdetails.IsInvoiced = row01("IsInvoiced")
                        clsprojectlocationdetails.Remarks = row01("Remarks")
                        Dim CLocationDetails As Integer = clsprojectlocationdetails.Save()

                        'Add hrs_ProjectLocationShifts
                        Dim clsprojectlocationshift As New Clshrs_ProjectLocationShifts(Me)
                        If clsprojectlocationshift.Find("LocationDetailID = " & row01("ID")) Then
                            Dim dt02 As DataTable = clsprojectlocationshift.DataSet.Tables(0)
                            For Each row02 As DataRow In dt02.Rows
                                clsprojectlocationshift = New Clshrs_ProjectLocationShifts(Me)
                                clsprojectlocationshift.LocationDetailID = CLocationDetails
                                clsprojectlocationshift.AttendanceTableShiftID = row02("AttendanceTableShiftID")
                                clsprojectlocationshift.Qty = row02("Qty")
                                Dim CLocationShift As Integer = clsprojectlocationshift.Save()
                            Next
                        End If

                        'Add hrs_ProjectPlacements
                        Dim clsprojectplacement As New Clshrs_ProjectPlacements(Me)
                        If clsprojectplacement.Find("ProjectChangeID = " & CCHangeID.Value & " and LocationDetailID = " & row01("ID")) Then
                            Dim dt03 As DataTable = clsprojectplacement.DataSet.Tables(0)
                            For Each row03 As DataRow In dt03.Rows
                                clsprojectplacement = New Clshrs_ProjectPlacements(Me)
                                clsprojectplacement.PlacementCode = row03("PlacementCode")
                                clsprojectplacement.ProjectID = row03("ProjectID")
                                clsprojectplacement.ProjectChangeID = NewChangeID.Value
                                clsprojectplacement.LocationID = CLocation
                                clsprojectplacement.LocationDetailID = CLocationDetails
                                If Convert.ToString(row03("LoadPCT")) <> "" Then
                                    clsprojectplacement.LoadPCT = row03("LoadPCT")
                                End If
                                If Convert.ToString(row03("RegComputerID")) <> "" Then
                                    clsprojectplacement.RegComputerID = row03("RegComputerID")
                                End If
                                Dim CPlacementID As Integer = clsprojectplacement.Save()

                                'Add hrs_ProjectPlacementPlanning
                                Dim clsprojectplacementplanning As New Clshrs_ProjectPlacementPlanning(Me)
                                If clsprojectplacementplanning.Find("PlacementID = " & row03("ID")) Then
                                    Dim dt04 As DataTable = clsprojectplacementplanning.DataSet.Tables(0)
                                    For Each row04 As DataRow In dt04.Rows
                                        clsprojectplacementplanning = New Clshrs_ProjectPlacementPlanning(Me)
                                        If Convert.ToString(row04("AttendanceTableShiftID")) <> "" Then
                                            clsprojectplacementplanning.AttendanceTableShiftID = row04("AttendanceTableShiftID")
                                        End If
                                        clsprojectplacementplanning.PlacementID = CPlacementID
                                        clsprojectplacementplanning.DayID = row04("DayID")
                                        If Convert.ToString(row04("ReferenceTo")) <> "" Then
                                            clsprojectplacementplanning.ReferenceTo = row04("ReferenceTo")
                                        End If
                                        clsprojectplacementplanning.Save()
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If

                'Add hrs_ProjectLocationsDetails
                Dim clsprojectlocation3 As New Clshrs_ProjectLocations(Me)
                If clsprojectlocation3.Find("MainLocationID = " & row("ID") & " and ProjectChangeID = " & CCHangeID.Value) Then
                    Dim dt3 As DataTable = clsprojectlocation3.DataSet.Tables(0)
                    For Each row3 As DataRow In dt3.Rows
                        clsprojectlocation3 = New Clshrs_ProjectLocations(Me)
                        clsprojectlocation3.ProjectChangeID = NewChangeID.Value
                        If Convert.ToString(row3("MainLocationID")) <> "" Then
                            clsprojectlocation3.MainLocationID = CLocation
                        End If
                        clsprojectlocation3.LocationDescription = row3("LocationDescription")
                        clsprojectlocation3.LocationAddress = row3("LocationAddress")
                        clsprojectlocation3.Required = row3("Required")
                        clsprojectlocation3.LinkedCS = Convert.ToString(row3("LinkedCS"))
                        If Convert.ToString(row3("Supervisor")) <> "" Then
                            clsprojectlocation3.Supervisor = row3("Supervisor")
                        End If
                        Dim CLocation1 As Integer = clsprojectlocation3.Save()

                        Dim SubinsUserEntry As String = "insert into hrs_ProjectLocationUsers (ProjectLocationID,UserID) select " & CLocation1 & ",UserID from hrs_ProjectLocationUsers where ProjectLocationID = " & row3("ID")
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectlocation.ConnectionString, Data.CommandType.Text, SubinsUserEntry)

                        'Add hrs_ProjectLocationDetails
                        Dim clsprojectlocation3details As New Clshrs_ProjectLocationDetails(Me)
                        If clsprojectlocation3details.Find("LocationID = " & row3("ID")) Then
                            Dim dt301 As DataTable = clsprojectlocation3details.DataSet.Tables(0)
                            For Each row301 As DataRow In dt301.Rows
                                clsprojectlocation3details = New Clshrs_ProjectLocationDetails(Me)
                                clsprojectlocation3details.LocationID = CLocation1
                                clsprojectlocation3details.PositionID = row301("PositionID")
                                clsprojectlocation3details.Qty = row301("Qty")
                                clsprojectlocation3details.InternalAmt = row301("InternalAmt")
                                clsprojectlocation3details.ExternalAmt = row301("ExternalAmt")
                                clsprojectlocation3details.WeekDays = row301("WeekDays")
                                clsprojectlocation3details.IsAlternative = row301("IsAlternative")
                                clsprojectlocation3details.IsInvoiced = row301("IsInvoiced")
                                clsprojectlocation3details.Remarks = row301("Remarks")
                                Dim CLocationDetails As Integer = clsprojectlocation3details.Save()

                                'Add hrs_ProjectLocationShifts
                                Dim clsprojectlocation3shift As New Clshrs_ProjectLocationShifts(Me)
                                If clsprojectlocation3shift.Find("LocationDetailID = " & row301("ID")) Then
                                    Dim dt302 As DataTable = clsprojectlocation3shift.DataSet.Tables(0)
                                    For Each row302 As DataRow In dt302.Rows
                                        clsprojectlocation3shift = New Clshrs_ProjectLocationShifts(Me)
                                        clsprojectlocation3shift.LocationDetailID = CLocationDetails
                                        clsprojectlocation3shift.AttendanceTableShiftID = row302("AttendanceTableShiftID")
                                        clsprojectlocation3shift.Qty = row302("Qty")
                                        Dim CLocationShift As Integer = clsprojectlocation3shift.Save()
                                    Next
                                End If

                                'Add hrs_ProjectPlacements
                                Dim clsprojectplacement As New Clshrs_ProjectPlacements(Me)
                                If clsprojectplacement.Find("ProjectChangeID = " & CCHangeID.Value & " and LocationDetailID = " & row301("ID")) Then
                                    Dim dt303 As DataTable = clsprojectplacement.DataSet.Tables(0)
                                    For Each row303 As DataRow In dt303.Rows
                                        clsprojectplacement = New Clshrs_ProjectPlacements(Me)
                                        clsprojectplacement.PlacementCode = row303("PlacementCode")
                                        clsprojectplacement.ProjectID = row303("ProjectID")
                                        clsprojectplacement.ProjectChangeID = NewChangeID.Value
                                        clsprojectplacement.LocationID = CLocation1
                                        clsprojectplacement.LocationDetailID = CLocationDetails
                                        If Convert.ToString(row303("LoadPCT")) <> "" Then
                                            clsprojectplacement.LoadPCT = row303("LoadPCT")
                                        End If
                                        If Convert.ToString(row303("RegComputerID")) <> "" Then
                                            clsprojectplacement.RegComputerID = row303("RegComputerID")
                                        End If
                                        Dim CPlacementID As Integer = clsprojectplacement.Save()

                                        'Add hrs_ProjectPlacementPlanning
                                        Dim clsprojectplacementplanning As New Clshrs_ProjectPlacementPlanning(Me)
                                        If clsprojectplacementplanning.Find("PlacementID = " & row303("ID")) Then
                                            Dim dt304 As DataTable = clsprojectplacementplanning.DataSet.Tables(0)
                                            For Each row304 As DataRow In dt304.Rows
                                                clsprojectplacementplanning = New Clshrs_ProjectPlacementPlanning(Me)
                                                If Convert.ToString(row304("AttendanceTableShiftID")) <> "" Then
                                                    clsprojectplacementplanning.AttendanceTableShiftID = row304("AttendanceTableShiftID")
                                                End If
                                                clsprojectplacementplanning.PlacementID = CPlacementID
                                                clsprojectplacementplanning.DayID = row304("DayID")
                                                If Convert.ToString(row304("ReferenceTo")) <> "" Then
                                                    clsprojectplacementplanning.ReferenceTo = row304("ReferenceTo")
                                                End If
                                                clsprojectplacementplanning.Save()
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        End If
        If txtEndDate.Text <> "  /  /    " Then
            SetToDate(NewChangeID.Value)
        End If

        clsprojectsetting = New Clshrs_ProjectSetting(Me)
        If clsprojectsetting.Find("ProjectChangeID = " & NewChangeID.Value) Then
            'txtInternalOvertimeFactor.Value = clsprojectsetting.InternalOvertimeFactor
            'txtExternalOvertimeFactor.Value = clsprojectsetting.ExternalOvertimeFactor
            'txtInternalHolidayFactor.Value = clsprojectsetting.InternalDayOffOvertimeFactor
            'txtExternalHolidayFactor.Value = clsprojectsetting.ExternalDayOffOvertimeFactor
            'WebNumericEdit_InternalExtension.Value = clsprojectsetting.InternalExtensionValue
            'WebNumericEdit_ExternalExtension.Value = clsprojectsetting.ExternalExtensionValue
        End If

        CType(Me.UltraWebTab1.Tabs(0), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(1), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton4_Click(sender As Object, e As System.EventArgs) Handles LinkButton4.Click
        'Overtime Forward
        Dim clsprojectsetting As New Clshrs_NewEmployee(Me)
        If clsprojectsetting.Find("ID = " & NewChangeID.Value) Then
            'clsprojectsetting.InternalOvertimeFactor = txtInternalOvertimeFactor.Value
            'clsprojectsetting.ExternalOvertimeFactor = txtExternalOvertimeFactor.Value
            'clsprojectsetting.InternalDayOffOvertimeFactor = txtInternalHolidayFactor.Value
            'clsprojectsetting.ExternalDayOffOvertimeFactor = txtExternalHolidayFactor.Value
            'clsprojectsetting.InternalExtensionValue = WebNumericEdit_InternalExtension.Value
            'clsprojectsetting.ExternalExtensionValue = WebNumericEdit_ExternalExtension.Value
            clsprojectsetting.Update("ID = " & NewChangeID.Value)
        Else
            clsprojectsetting = New Clshrs_NewEmployee(Me)
            clsprojectsetting.ID = NewChangeID.Value
            'clsprojectsetting.InternalOvertimeFactor = txtInternalOvertimeFactor.Value
            'clsprojectsetting.ExternalOvertimeFactor = txtExternalOvertimeFactor.Value
            'clsprojectsetting.InternalDayOffOvertimeFactor = txtInternalHolidayFactor.Value
            'clsprojectsetting.ExternalDayOffOvertimeFactor = txtExternalHolidayFactor.Value
            'clsprojectsetting.InternalExtensionValue = WebNumericEdit_InternalExtension.Value
            'clsprojectsetting.ExternalExtensionValue = WebNumericEdit_ExternalExtension.Value
            clsprojectsetting.Save()
        End If

        clsprojectsetting = New Clshrs_NewEmployee(Me)
        If clsprojectsetting.Find("ID = " & NewChangeID.Value) Then
            'txtInternalAbsentFactor.Text = clsprojectsetting.InternalAbsentFactor
            'txtExternalAbsentFactor.Text = clsprojectsetting.ExternalAbsentFactor
            'txtInternalSickFactor.Text = clsprojectsetting.InternalSickFactor
            'txtExternalSickFactor.Text = clsprojectsetting.ExternalSickFactor
            'txtInternalLeavFactor.Text = clsprojectsetting.InternalLeavFactor
            'txtExternalLeavFactor.Text = clsprojectsetting.ExternalLeavFactor
            'txtInternalPermitDelayFactor.Text = clsprojectsetting.InternalPermitDelayFactor
            'txtExternalPermitDelayFactor.Text = clsprojectsetting.ExternalPermitDelayFactor
            'txtInternalDelayPunishFactor.Text = clsprojectsetting.InternalDelayPunishFactor
            'txtExternalDelayPunishFactor.Text = clsprojectsetting.ExternalDelayPunishFactor
        End If
        CType(Me.UltraWebTab1.Tabs(1), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(2), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton5_Click(sender As Object, e As System.EventArgs) Handles LinkButton5.Click
        'Punishment Back
        Dim clsprojectsetting As New Clshrs_NewEmployee(Me)
        If clsprojectsetting.Find("ID = " & NewChangeID.Value) Then
            'txtInternalOvertimeFactor.Value = clsprojectsetting.InternalOvertimeFactor
            'txtExternalOvertimeFactor.Value = clsprojectsetting.ExternalOvertimeFactor
            'txtInternalHolidayFactor.Value = clsprojectsetting.InternalDayOffOvertimeFactor
            'txtExternalHolidayFactor.Value = clsprojectsetting.ExternalDayOffOvertimeFactor
            'WebNumericEdit_InternalExtension.Value = clsprojectsetting.InternalExtensionValue
            'WebNumericEdit_ExternalExtension.Value = clsprojectsetting.ExternalExtensionValue
        End If
        CType(Me.UltraWebTab1.Tabs(2), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(1), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton6_Click(sender As Object, e As System.EventArgs) Handles LinkButton6.Click
        'Punishment Forward
        Dim clsprojectsetting As New Clshrs_NewEmployee(Me)
        If clsprojectsetting.Find("ID = " & NewChangeID.Value) Then
            'clsprojectsetting.InternalAbsentFactor = txtInternalAbsentFactor.Text
            'clsprojectsetting.ExternalAbsentFactor = txtExternalAbsentFactor.Text
            'clsprojectsetting.InternalSickFactor = txtInternalSickFactor.Text
            'clsprojectsetting.ExternalSickFactor = txtExternalSickFactor.Text
            'clsprojectsetting.InternalLeavFactor = txtInternalLeavFactor.Text
            'clsprojectsetting.ExternalLeavFactor = txtExternalLeavFactor.Text
            'clsprojectsetting.InternalPermitDelayFactor = txtInternalPermitDelayFactor.Text
            'clsprojectsetting.ExternalPermitDelayFactor = txtExternalPermitDelayFactor.Text
            'clsprojectsetting.InternalDelayPunishFactor = txtInternalDelayPunishFactor.Text
            'clsprojectsetting.ExternalDelayPunishFactor = txtExternalDelayPunishFactor.Text
            clsprojectsetting.Update("ID = " & NewChangeID.Value)
        Else
            clsprojectsetting = New Clshrs_NewEmployee(Me)
            'clsprojectsetting.ProjectChangeID = NewChangeID.Value
            'clsprojectsetting.InternalAbsentFactor = txtInternalAbsentFactor.Text
            'clsprojectsetting.ExternalAbsentFactor = txtExternalAbsentFactor.Text
            'clsprojectsetting.InternalSickFactor = txtInternalSickFactor.Text
            'clsprojectsetting.ExternalSickFactor = txtExternalSickFactor.Text
            'clsprojectsetting.InternalLeavFactor = txtInternalLeavFactor.Text
            'clsprojectsetting.ExternalLeavFactor = txtExternalLeavFactor.Text
            'clsprojectsetting.InternalPermitDelayFactor = txtInternalPermitDelayFactor.Text
            'clsprojectsetting.ExternalPermitDelayFactor = txtExternalPermitDelayFactor.Text
            'clsprojectsetting.InternalDelayPunishFactor = txtInternalDelayPunishFactor.Text
            'clsprojectsetting.ExternalDelayPunishFactor = txtExternalDelayPunishFactor.Text
            clsprojectsetting.Save()
        End If

        'uwgBenfits.Rows.Clear()
        'uwgBenfits.Rows.Add()
        'Dim clsprojectrewards As New Clshrs_NewEmployee(Me)
        'If clsprojectrewards.Find("ID = " & NewChangeID.Value) Then
        '    uwgBenfits.DataSource = clsprojectrewards.DataSet.Tables(0)
        '    uwgBenfits.DataBind()
        'End If

        CType(Me.UltraWebTab1.Tabs(2), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(3), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
        'LinkButton10_Click(Nothing, Nothing)
    End Sub

    Protected Sub LinkButton7_Click(sender As Object, e As System.EventArgs) Handles LinkButton7.Click
        'Rewards Back
        Dim clsprojectsetting As New Clshrs_NewEmployee(Me)
        If clsprojectsetting.Find("ID = " & NewChangeID.Value) Then
            'txtInternalAbsentFactor.Text = clsprojectsetting.InternalAbsentFactor
            'txtExternalAbsentFactor.Text = clsprojectsetting.ExternalAbsentFactor
            'txtInternalSickFactor.Text = clsprojectsetting.InternalSickFactor
            'txtExternalSickFactor.Text = clsprojectsetting.ExternalSickFactor
            'txtInternalLeavFactor.Text = clsprojectsetting.InternalLeavFactor
            'txtExternalLeavFactor.Text = clsprojectsetting.ExternalLeavFactor
            'txtInternalPermitDelayFactor.Text = clsprojectsetting.InternalPermitDelayFactor
            'txtExternalPermitDelayFactor.Text = clsprojectsetting.ExternalPermitDelayFactor
            'txtInternalDelayPunishFactor.Text = clsprojectsetting.InternalDelayPunishFactor
            'txtExternalDelayPunishFactor.Text = clsprojectsetting.ExternalDelayPunishFactor
        End If

        CType(Me.UltraWebTab1.Tabs(3), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(2), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton8_Click(sender As Object, e As System.EventArgs) Handles LinkButton8.Click
        'Rewards Forward
        'Dim clsprojectrewards As New Clshrs_ProjectRewards(Me)


        'uwgPenalities.Rows.Clear()
        'uwgPenalities.Rows.Add()
        'Dim clsprojectpenalities As New Clshrs_ProjectPenalities(Me)
        'If clsprojectpenalities.Find("ProjectChangeID = " & NewChangeID.Value) Then
        '    uwgPenalities.DataSource = clsprojectpenalities.DataSet.Tables(0)
        '    uwgPenalities.DataBind()
        'End If

        CType(Me.UltraWebTab1.Tabs(3), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(4), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub



    Protected Sub LinkButton15_Click(sender As Object, e As System.EventArgs) Handles LinkButton15.Click
        'Attendance Planning Back
        'Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
        'cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & NewChangeID.Value & " and MainLocationID is null")
        'cls_ProjectLocations.GetDropDownList(ddlSubLocation, True, "ProjectChangeID = " & NewChangeID.Value & " and MainLocationID = " & ddlLocation.SelectedValue)
        'Dim clsPositions As New Clshrs_Positions(Me)
        'clsPositions.GetList(uwgLocationPositions.DisplayLayout.Bands(0).Columns(2).ValueList)

        'uwgLocationPositions.Rows.Clear()
        'uwgLocationPositions.Rows.Add()
        'Label_Cnt.Text = ""

        CType(Me.UltraWebTab1.Tabs(4), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(3), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
        'LinkButton16_Click(sender, e)
    End Sub

    Private Function GetCostCenter1ID(CostCenter1Code As String) As Integer
        If Not String.IsNullOrEmpty(CostCenter1Code) Then
            Dim str As String
            Dim CostCode As String = CostCenter1Code
            Dim CostCenterID As Integer
            Dim clsEmployees As New Clshrs_NewEmployee(Me)
            Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)

            str = "select ID From fcs_CostCenters1 where Code='" & CostCode & "' "
            Try
                CostCenterID = CInt(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, str))
                Return CostCenterID
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try

        End If
    End Function
    Private Function GetCostCenter2ID(CostCenter1Code As String) As Integer
        If Not String.IsNullOrEmpty(CostCenter1Code) Then
            Dim str As String
            Dim CostCode As String = CostCenter1Code
            Dim CostCenterID As Integer
            Dim clsEmployees As New Clshrs_NewEmployee(Me)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)

            str = "select ID From fcs_CostCenters2 where Code='" & CostCode & "' "
            Try
                CostCenterID = CInt(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, str))
                Return CostCenterID
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try

        End If
    End Function

    Private Function GetCostCenter3ID(CostCenter1Code As String) As Integer
        If Not String.IsNullOrEmpty(CostCenter1Code) Then
            Dim str As String
            Dim CostCode As String = CostCenter1Code
            Dim CostCenterID As Integer
            Dim clsEmployees As New Clshrs_NewEmployee(Me)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)

            str = "select ID From fcs_CostCenters3 where Code='" & CostCode & "' "
            Try
                CostCenterID = CInt(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, str))
                Return CostCenterID
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try

        End If
    End Function
    Private Function GetCostCenter4ID(CostCenter1Code As String) As Integer
        If Not String.IsNullOrEmpty(CostCenter1Code) Then
            Dim str As String
            Dim CostCode As String = CostCenter1Code
            Dim CostCenterID As Integer
            Dim clsEmployees As New Clshrs_NewEmployee(Me)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)

            str = "select ID From fcs_CostCenters4 where Code='" & CostCode & "' "
            Try
                CostCenterID = CInt(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, str))
                Return CostCenterID
            Catch ex As Exception
            End Try

        End If
    End Function

    'add contract
    Protected Sub btnAddContract_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnAddContract.Click

        Dim ClsEmployees As New Clshrs_Employees(Me)
        Dim ClsEmployees2 As New Clshrs_Employees(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        ClsEmployees.ArabicName = txtArbName.Text
        ClsEmployees.EnglishName = txtEngName.Text
        ClsEmployees.FatherArbName = txtFatherArbName.Text
        ClsEmployees.FatherEngName = txtFatherEngName.Text
        ClsEmployees.GrandArbName = txtGrandArbName.Text
        ClsEmployees.GrandEngName = txtGrandEngName.Text
        ClsEmployees.FamilyArbName = txtFamilyArbName.Text
        ClsEmployees.FamilyEngName = txtFamilyEngName.Text
        ClsEmployees.BirthDate = txtBirthDate.Text
        ClsEmployees.NationalityID = DdlNationality.SelectedValue
        ClsEmployees.BranchID = ddlBranch.SelectedValue
        ClsEmployees.DepartmentID = ddlDepartment.SelectedValue
        ClsEmployees.SectorID = ddlSectors.SelectedValue
        ClsEmployees.LocationID = DropDownList_Location.SelectedValue
        ClsEmployees.JoinDate = txtJoinDate.Text
        ClsEmployees.SponsorID = ddlSponsor.SelectedValue
        ClsEmployees.SSnNo = txtIdentity.Text
        ClsEmployees.Cost1 = GetCostCenter1ID(TxtCostCode1.Text)
        ClsEmployees.Cost2 = GetCostCenter2ID(TxtCostCode2.Text)
        ClsEmployees.Cost3 = GetCostCenter3ID(TxtCostCode3.Text)
        ClsEmployees.Cost4 = GetCostCenter4ID(TxtCostCode4.Text)
        ClsEmployees.paymenttype = ddlLblPaymentType.SelectedValue
        ClsEmployees.MachineCode = TextBox_MachineCode.Text
        ClsEmployees.GosiNumber = txtGosiNumber.Text
        ClsEmployees.GOSIJoinDate = GOSIJoinDate.Text
        ClsEmployees2.Find("Code= " & txtManager.Text & "")
        ClsEmployees.ManagerID = ClsEmployees2.ID
        ClsEmployees.BankAccountType = ddlBankAccountType.SelectedValue

        If DdlNationality.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select nationality /عفوا...لابد من تحديد الجنسية "))
            Exit Sub
        End If
        If ddlBranch.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Branch /عفوا...لابد من تحديد الفرع "))
            Exit Sub
        End If
        If ddlDepartment.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Department /عفوا...لابد من تحديد القسم "))
            Exit Sub
        End If
        If ddlSponsor.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Sponsor /عفوا...لابد من تحديد الكفيل "))
            Exit Sub
        End If
        If ddlContractType.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Contract Type /عفوا...لابد من تحديد نوع العقد "))
            Exit Sub
        End If
        If ddlProfessions.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Profession   /عفوا...لابد من تحديد المهنة "))
            Exit Sub
        End If
        If ddlGradeStep.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Grade step   /عفوا...لابد من تحديد الرتبة "))
            Exit Sub
        End If
        If ddlPosition.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Position   /عفوا...لابد من تحديد الوظيفة "))
            Exit Sub
        End If
        If ddlEmployeeClass.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Employee Class   /عفوا...لابد من تحديد الفئة "))
            Exit Sub
        End If
        If String.IsNullOrEmpty(txtJoinDate.Text) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Join Date /عفوا...لابد من ادخال تاريخ المباشرة "))
            Exit Sub
        End If
        If String.IsNullOrEmpty(txtIdentity.Text) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Identity No  /عفوا...لابد من ادخال رقم الهوية  "))
            Exit Sub
        End If
        If String.IsNullOrEmpty(txtStartDate.Text) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Start Date   /عفوا...لابد من ادخال تاريخ البدء  "))
            Exit Sub
        End If

        If ClsEmployees.ID > 0 Then


            If txtIdentity.Text <> "" Then
                Dim checkCnt = "select top 1 Code from hrs_employees where SSnNo = '" & txtIdentity.Text & "' and Code <> '" & txtCode.Text & "' and branchID = '" & ddlBranch.SelectedValue & "'"
                Dim cnt As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, checkCnt)
                If cnt <> "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " ID NO Used Before Employee No. /رقم الهوية مستخدم سابقا للموظف رقم") & cnt)
                    Exit Sub
                End If
            End If
            txtEmpId.Value = ClsEmployees.ID
            ClsEmployees.Update("code='" & txtCode.Text & "'")


        Else
            'Added By Mohannad
            '2022-06-15
            '-----------------------------------------------
            If ClsEmployees.Find("SSnNo LIKE '" & txtIdentity.Text.Trim() & "'") Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " The SSNO is used /رقم الهوية مستخدم لموظف آخر"))
                Exit Sub
            End If
            'If String.IsNullOrEmpty(txtWorkE_Mail.Text) Then
            '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to enter Work E-mail /عفوا...لابد من اخال بريد العمل"))
            '    Exit Sub
            'End If
            If DdlNationality.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select nationality /عفوا...لابد من تحديد الجنسية "))
                Exit Sub
            End If
            If ddlBranch.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Branch /عفوا...لابد من تحديد الفرع "))
                Exit Sub
            End If
            If ddlDepartment.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Department /عفوا...لابد من تحديد القسم "))
                Exit Sub
            End If
            If ddlSponsor.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Sponsor /عفوا...لابد من تحديد الكفيل "))
                Exit Sub
            End If
            If ddlContractType.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Contract Type /عفوا...لابد من تحديد نوع العقد "))
                Exit Sub
            End If
            If ddlProfessions.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Profession   /عفوا...لابد من تحديد المهنة "))
                Exit Sub
            End If
            If ddlGradeStep.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Grade step   /عفوا...لابد من تحديد الرتبة "))
                Exit Sub
            End If
            If ddlPosition.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Position   /عفوا...لابد من تحديد الوظيفة "))
                Exit Sub
            End If
            If ddlEmployeeClass.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Employee Class   /عفوا...لابد من تحديد الفئة "))
                Exit Sub
            End If
            If String.IsNullOrEmpty(txtJoinDate.Text) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Join Date /عفوا...لابد من ادخال تاريخ المباشرة "))
                Exit Sub
            End If
            If String.IsNullOrEmpty(txtIdentity.Text) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Identity No  /عفوا...لابد من ادخال رقم الهوية  "))
                Exit Sub
            End If
            If String.IsNullOrEmpty(txtStartDate.Text) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Start Date   /عفوا...لابد من ادخال تاريخ البدء  "))
                Exit Sub
            End If

            '-----------------------------------------------
            'Get Next Code
            Dim Cls_Companies As New Clssys_Companies(Page)
            Dim IntSeqLength As Integer = 0
            Dim Retcode As String = ""
            If Cls_Companies.Find("ID=" & Cls_Companies.MainCompanyID) = True Then
                If Cls_Companies.HasSequence = True Then
                    'If txtCode.Text = "" Or txtCode.Text = "Auto" Or txtCode.Text = "تلقائى" Then
                    IntSeqLength = Cls_Companies.SequenceLength
                    Dim strcommand As String = "select Max(isnull(convert(int,Substring(Code,CHARINDEX('" & Cls_Companies.Separator & "',Code)+1,LEN(Code))),0)) + 1 from Hrs_Employees where Substring(Code,CHARINDEX('" & Cls_Companies.Separator & "',Code)+1,LEN(Code)) NOT LIKE '%[a-z]%' and CompanyID = " & Cls_Companies.MainCompanyID
                    Dim strnextcode As String = ""
                    Try
                        strnextcode = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Cls_Companies.ConnectionString, Data.CommandType.Text, strcommand)
                    Catch ex As Exception
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
                        Exit Sub
                    End Try
                    Retcode = strnextcode
                    For I As Integer = 0 To IntSeqLength - strnextcode.Length - 1
                        Retcode = "0" + Retcode
                    Next
                    '' Prefix
                    Dim prfx As String = ""
                    If (Cls_Companies.Prefix = 0) Then
                        prfx = ""
                    ElseIf (Cls_Companies.Prefix = 1) Then
                        Dim cls As New Clssys_Branches(Me)
                        If cls.Find("ID = " & ddlBranch.SelectedValue) Then
                            prfx = cls.Code
                        End If
                    ElseIf (Cls_Companies.Prefix = 2) Then
                        Dim cls As New Clssys_Departments(Me)
                        If cls.Find("ID = " & ddlDepartment.SelectedValue) Then
                            prfx = cls.Code
                        End If
                    ElseIf (Cls_Companies.Prefix = 3) Then
                        Dim cls As New Clshrs_Positions(Me)
                        If cls.Find("ID = " & ddlPosition.SelectedValue) Then
                            prfx = cls.Code
                        End If
                    ElseIf (Cls_Companies.Prefix = 4) Then
                        Dim cls As New Clshrs_ContractTypes(Me)
                        If cls.Find("ID = " & ddlContractType.SelectedValue) Then
                            prfx = cls.Code
                        End If
                    End If

                    txtCode.Text = prfx & IIf(prfx = "", "", Cls_Companies.Separator) & Retcode

                    ClsEmployees.Code = txtCode.Text
                    ClsEmployees.MachineCode = txtCode.Text
                    If txtCode.Text = "" Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                        Exit Sub
                    End If
                    ' End If
                End If
            End If
            'End Get Code


            If txtIdentity.Text <> "" Then
                Dim checkCnt = "select top 1 Code from hrs_employees where SSnNo = '" & txtIdentity.Text & "' and Code <> '" & txtCode.Text & "' and branchID = '" & ddlBranch.SelectedValue & "'"
                Dim cnt As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, checkCnt)
                If cnt <> "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " ID NO Used Before Employee No. /رقم الهوية مستخدم سابقا للموظف رقم") & cnt)
                    Exit Sub
                End If
            End If

            ClsEmployees.Save()
            ClsEmployees.Find("Code='" & txtCode.Text & "'")
            txtEmpId.Value = ClsEmployees.ID

            Dim Transcommand As String = "update Hrs_NewEmployee set IsTransfered = 1,TransfereDate=GetDate() where ID = " & txtEmpId.Value
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, Data.CommandType.Text, Transcommand)


        End If

        'If ddlLastEducations.SelectedIndex > 0 Then
        '    Dim clsEmpEducations As New Clshrs_EmployeesEducations(Me)
        '    If clsEmpEducations.Find("EmployeeID=" & ClsEmployees.ID) Then
        '        clsEmpEducations.EducationTypeID = ddlLastEducations.SelectedValue
        '        clsEmpEducations.GraduationDate = SetDate(txtGraduationDate.Text, txtGraduationDate.Text)
        '        clsEmpEducations.Update("EmployeeID=" & ClsEmployees.ID)
        '    Else
        '        clsEmpEducations.EmployeeID = ClsEmployees.ID
        '        clsEmpEducations.EducationTypeID = ddlLastEducations.SelectedValue
        '        clsEmpEducations.GraduationDate = SetDate(txtGraduationDate.Text, txtGraduationDate.Text)
        '        clsEmpEducations.Save()
        '    End If
        'End If

        Dim clsContracts = New Clshrs_Contracts(Page)
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim StrSerial As String = String.Empty
        If lblContractNo.Text = String.Empty Or lblContractNo.Text = "0" Or lblContractNo.Text = "Auto" Or lblContractNo.Text = "تلقائى" Then
            ObjDataHandler.GetLastSerial(clsContracts.Table, "Number", StrSerial, clsContracts.ConnectionString)
            lblContractNo.Text = StrSerial
        End If
        If lblContractNo.Text <> String.Empty Or lblContractNo.Text <> "0" Then
            clsContracts = New Clshrs_Contracts(Page)
            Dim ClsContractTransactions = New Clshrs_ContractsTransactions(Page)
            If Not txtStartWorkDate.Value Is Nothing And lblContractNo.Text.Length > 0 Then
                If SavePart_Contracts() Then
                    clsContracts.Find("Number=" & lblContractNo.Text)
                    txtContractId.Value = clsContracts.ID
                Else
                    txtStartWorkDate.Focus()
                    Exit Sub
                End If
            End If
        End If

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CloseWindowScript", "window.close();", True)
    End Sub


    Private Function SavePart_Contracts() As Boolean
        Dim IntEmployeeId As Integer = txtEmpId.Value
        Dim StrMode As String = String.Empty
        Dim ClsEmployeeJoin = New Clshrs_EmployeesJoins(Me.Page)
        Dim ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsEmployeeJoin.ConnectionString)
        Dim ClsContracts As New Clshrs_Contracts(Me.Page)
        ClsContracts.Find("Number='" & lblContractNo.Text & "'")

        If Not txtEndDate.Text = "  /  /    " Then
            If SetDate(txtStartDate.Text, txtStartDate.Text) > SetDate(txtEndDate.Text, txtEndDate.Text) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Me.Page, "Data Can't be saved ,  Invalid Contract Dates ! Check Entered Dates ! ")
                Return False
            End If
        End If
        If ClsContracts.ID > 0 Then
            If CheckUpdateContract_New() = 0 Then
                If ClsEmployeeJoin.Find("EmployeeID=" & IntEmployeeId & " And JoinDate ='" & Format(ClsContracts.StartDate, "yyyy/MM/dd") & "'") Then
                    ClsEmployeeJoin.EmployeeId = IntEmployeeId
                    ClsEmployeeJoin.JoinDate = SetDate(txtStartDate.Text, txtStartDate.Text) 'IIf(txtStartDate.Text.Trim = "", Nothing, ClsContracts.SetHigriDate(txtStartDate.Value))
                    ClsEmployeeJoin.Update("ID=" & ClsEmployeeJoin.ID)
                End If
                If Not AssignValues_Contracts(ClsContracts) Then
                    Exit Function
                End If
                ClsContracts.Update("Number='" & lblContractNo.Text & "'")
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Me.Page, "Data Can't be saved ,  Invalid Contract Dates ! Check Entered Dates ! ")
                txtStartDate.Value = Nothing
                txtEndDate.Value = Nothing
                txtStartDate.Focus()
                Return False
            End If
        Else
            '-------------------------------0257 MODIFIED-----------------------------------------
            If CheckUpdateContract_New() = 0 Then
                '-------------------------------=============-----------------------------------------
                If Not AssignValues_Contracts(ClsContracts) Then
                    Exit Function
                End If
                ClsContracts.StartDate = txtStartWorkDate.Text
                ClsContracts.ContractTypeId = ddlContractType.SelectedValue
                ClsContracts.ProfessionID = ddlProfessions.SelectedValue
                ClsContracts.GradeStepId = ddlGradeStep.SelectedValue
                ClsContracts.PositionID = ddlPosition.SelectedValue
                ClsContracts.EmployeeClassID = ddlEmployeeClass.SelectedValue

                ClsContracts.Save()
                ClsContracts.Find("Number='" & lblContractNo.Text & "'")
                Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Me)
                Dim ds As Data.DataSet = ClsEmployeeClasses.GetEmployeeClassVacations(ClsContracts.EmployeeClassID)
                ClsContracts.SetContractVacation(ClsContracts.ID, ds)

                '#Region' "Add Vacations Balance"
                ClsEmployeeClasses.Find(" ID= " & ClsContracts.EmployeeClassID)
                If ClsEmployeeClasses.AdvanceBalance And ClsEmployeeClasses.AddBalanceInAddEmp Then
                    Dim dsNew As Data.DataSet = ClsEmployeeClasses.GetEmployeeClassAnnualVacations(ClsEmployeeClasses.ID)
                    Dim allDays As Decimal = 1
                    Dim allVacDays As Decimal = 0
                    Dim moreOneYear As Boolean = False
                    If dsNew.Tables(0).Rows.Count > 0 Then
                        With dsNew.Tables(0).Rows(0)
                            allDays = .Item("RequiredWorkingMonths")
                            allVacDays = .Item("DurationDays")

                        End With
                    End If
                    moreOneYear = ClsEmployeeClasses.AccumulatedBalance
                    'Rabie 20-07-2025
                    Dim dayValue = allVacDays / allDays
                    'Dim currentYear As Integer = SetDate(txtStartDate.Text, txtStartDate.Text).Year
                    Dim currentYear As Integer = SetDate(txtJoinDate.Text, txtJoinDate.Text).Year
                    Dim endOfYear As DateTime = New DateTime(currentYear, 12, 31)
                    'Dim myDays As Integer = DateDiff(DateInterval.Day, SetDate(txtStartDate.Text, txtStartDate.Text), endOfYear.Date)
                    Dim myDays As Integer = DateDiff(DateInterval.Day, SetDate(txtJoinDate.Text, txtJoinDate.Text), endOfYear.Date)
                    Dim myBalance = myDays * dayValue
                    Dim expireDate As Date = endOfYear
                    If moreOneYear Then
                        'myDays = DateDiff(DateInterval.Day, SetDate(txtStartDate.Text, txtStartDate.Text), SetDate(txtStartDate.Text, txtStartDate.Text).AddDays(allDays))
                        myDays = DateDiff(DateInterval.Day, SetDate(txtJoinDate.Text, txtJoinDate.Text), SetDate(txtJoinDate.Text, txtJoinDate.Text).AddDays(allDays))
                        myBalance = myDays * dayValue
                        'expireDate = SetDate(txtStartDate.Text, txtStartDate.Text).AddDays(allDays)
                        expireDate = SetDate(txtJoinDate.Text, txtJoinDate.Text).AddDays(allDays)
                    End If
                    Dim checkCnt = "INSERT INTO [dbo].[hrs_VacationsBalance] ([EmployeeID],[Year],[Balance],[Consumed],[Remaining],[BalanceTypeID],[ExpireDate],[Src],[Remarks],[Reguser],[RegDate],DueDate) VALUES (" & IntEmployeeId & "," & currentYear & "," & myBalance & ",0," & myBalance & ",1,'" & expireDate.ToString("yyyy-MM-dd") & "'," & "'frmEmployeeWizard'" & "," & "''" & ",'" & ClsContracts.RegUserID & "',getdate(),'" & SetDate(txtJoinDate.Text, txtJoinDate.Text).ToString("yyyy-MM-dd") & "')"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeeClasses.ConnectionString, System.Data.CommandType.Text, checkCnt)

                End If

                '#End Region

                Dim clsnationality As New Clssys_Nationality(Me.Page)
                If clsnationality.Find("ID = " & DdlNationality.SelectedValue) Then
                    Dim Str As String = "select * from hrs_TicketsPrices where canceldate is null and TicketsRoutesID = " & clsnationality.TravelRoute & " and TicketsClassesID = " & clsnationality.TravelClass
                    Dim dsPrices As Data.DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsContracts.ConnectionString, System.Data.CommandType.Text, Str)
                    If dsPrices.Tables(0).Rows.Count > 0 Then
                        Str = "delete from hrs_TicketsContarct where ContractID = " & ClsContracts.ID
                        Str &= Environment.NewLine & "insert into hrs_TicketsContarct (ContractID,TicketsRouteID,TicketsClassID,TotalCost,IsPaid,RegUserID,RegDate) values (" & ClsContracts.ID & "," & dsPrices.Tables(0).Rows(0)(2) & "," & dsPrices.Tables(0).Rows(0)(1) & "," & dsPrices.Tables(0).Rows(0)(3) & ",0," & ClsContracts.DataBaseUserRelatedID & ",getdate())"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsContracts.ConnectionString, System.Data.CommandType.Text, Str)
                    End If
                End If

                If ClsEmployeeJoin.Find("EmployeeID=" & IntEmployeeId & " And JoinDate ='" & CDate(ClsContracts.StartDate).Date & "'") Then
                    ClsEmployeeJoin.EmployeeId = IntEmployeeId
                    ClsEmployeeJoin.JoinDate = SetDate(txtJoinDate.Text, txtJoinDate.Text)
                    ClsEmployeeJoin.Update("ID=" & ClsEmployeeJoin.ID)
                Else
                    ClsEmployeeJoin.EmployeeId = IntEmployeeId
                    ClsEmployeeJoin.JoinDate = SetDate(txtJoinDate.Text, txtJoinDate.Text)
                    ClsEmployeeJoin.Save()
                End If
                Dim ClsEmployees As New Clshrs_Employees(Me)
                ClsEmployees.Find("ID = " & txtEmpId.Value)
                Dim Mangcommand As String = "update hrs_employees set ExcludeDate = null where ID = " & txtEmpId.Value
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, Data.CommandType.Text, Mangcommand)
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Me.Page, "Invalid Contract Date! ")
                txtStartWorkDate.Value = Nothing
                'txtEndDate.Value = Nothing
                txtStartWorkDate.Focus()
                Return False
            End If
        End If
        Return True
    End Function

    Private Function AssignValues_Contracts(ByRef ClsContracts As Clshrs_Contracts) As Boolean
        Dim IntEmployeeId As Integer = txtEmpId.Value
        Dim clsCompanies As New Clssys_Companies(Me.Page)
        Try
            With ClsContracts
                .Number = lblContractNo.Text
                .EmployeeID = IntEmployeeId
                Try
                    .ProfessionID = IIf(ddlProfessions.Items.Count = 0, 0, ddlProfessions.SelectedItem.Value)
                Catch ex As Exception
                    .ProfessionID = 0
                End Try
                Try
                    .ContractTypeId = IIf(ddlContractType.Items.Count = 0, 0, ddlContractType.SelectedItem.Value) 'DdlContractType.SelectedItem.Value

                Catch ex As Exception
                    .ContractTypeId = 0
                End Try
                Try
                    .PositionID = IIf(ddlPosition.Items.Count = 0, 0, ddlPosition.SelectedItem.Value) 'DdlPosition.SelectedItem.Value

                Catch ex As Exception
                    .PositionID = 0
                End Try
                Try
                    .EmployeeClassID = IIf(ddlEmployeeClass.Items.Count = 0, 0, ddlEmployeeClass.SelectedItem.Value) 'DdlEmployeeClass.SelectedItem.Value

                Catch ex As Exception
                    .EmployeeClassID = 0
                End Try

                Try
                    .GradeStepId = IIf(ddlGradeStep.Items.Count = 0, 0, ddlGradeStep.SelectedItem.Value) 'DdlGradeStep.SelectedItem.Value
                Catch ex As Exception
                    .GradeStepId = 0
                End Try
                .StartDate = SetDate(txtStartDate.Text, txtStartDate.Text) 'IIf(txtStartDate.Value.Trim = "", Nothing, .SetHigriDate(txtStartDate.Text))
                .EndDate = SetDate(txtEndDate.Text, txtEndDate.Text) 'IIf(txtEndDate.Value.Trim = "", Nothing, .SetHigriDate(txtEndDate.Text))


            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function CheckUpdateContract_New() As Integer
        Dim IntEmployeeId As Integer = txtEmpId.Value
        Dim ObjRow As Data.DataRow
        Dim Int As Integer = 0

        Dim mDataHandler As New Venus.Shared.DataHandler
        Dim clsCompanies As New Clssys_Companies(Me.Page)
        clsCompanies.Find("ID = " & clsCompanies.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(Me.Page)
        Dim GHStartDate As Date
        Dim GHEndDate As Date
        Dim ClsContracts As New Clshrs_Contracts(Page)

        GHStartDate = SetDate(txtStartDate.Text, txtStartDate.Text) ' IIf(txtStartDate.Text.Trim = "", Nothing, ClsGHCalender.SetHigriDate(txtStartDate.Value))
        GHEndDate = SetDate(txtEndDate.Text, txtEndDate.Text) ' IIf(txtEndDate.Text.Trim = "", Nothing, ClsGHCalender.SetHigriDate(txtEndDate.Value))

        ClsContracts.Find("employeeid=" & IntEmployeeId & "  And canceldate is null ")
        For Each ObjRow In ClsContracts.DataSet.Tables(0).Rows

            If (CInt(lblContractNo.Text) = CInt(ObjRow("Number"))) Then
                Continue For
            End If

            If IsDBNull(ObjRow.Item("EndDate")) Then
                If (GHEndDate.Year = 1) Then
                    Return 1
                End If
                If GHEndDate >= ObjRow.Item("StartDate") Then
                    Return 1
                End If
            Else
                If (GHEndDate.Year = 1) Then
                    If GHStartDate <= ObjRow.Item("EndDate") Then
                        Return 1
                    End If
                Else
                    If GHStartDate = ObjRow.Item("StartDate") Or GHEndDate = ObjRow.Item("EndDate") Then
                        Return 1
                    End If
                    If GHStartDate <= ObjRow.Item("EndDate") And GHEndDate > ObjRow.Item("EndDate") Then
                        Return 1
                    End If
                    If GHEndDate >= ObjRow.Item("StartDate") And GHStartDate < ObjRow.Item("StartDate") Then
                        Return 1
                    End If
                End If
            End If
        Next
        Return 0
    End Function


    Protected Sub LinkButton16_Click(sender As Object, e As System.EventArgs) Handles LinkButton16.Click
        'Attendance Planning Finish
        Dim ClsEmployees As New Clshrs_Employees(Me)
        Dim ClsEmployees2 As New Clshrs_Employees(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)


        If DdlNationality.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select nationality /عفوا...لابد من تحديد الجنسية "))
            Exit Sub
        End If
        If ddlBranch.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Branch /عفوا...لابد من تحديد الفرع "))
            Exit Sub
        End If
        If ddlDepartment.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Department /عفوا...لابد من تحديد القسم "))
            Exit Sub
        End If
        If ddlSponsor.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Sponsor /عفوا...لابد من تحديد الكفيل "))
            Exit Sub
        End If
        If ddlContractType.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Contract Type /عفوا...لابد من تحديد نوع العقد "))
            Exit Sub
        End If
        If ddlProfessions.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Profession   /عفوا...لابد من تحديد المهنة "))
            Exit Sub
        End If
        If ddlGradeStep.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Grade step   /عفوا...لابد من تحديد الرتبة "))
            Exit Sub
        End If
        If ddlPosition.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Position   /عفوا...لابد من تحديد الوظيفة "))
            Exit Sub
        End If
        If ddlEmployeeClass.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Employee Class   /عفوا...لابد من تحديد الفئة "))
            Exit Sub
        End If
        If String.IsNullOrEmpty(txtJoinDate.Text) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Join Date /عفوا...لابد من ادخال تاريخ المباشرة "))
            Exit Sub
        End If
        If String.IsNullOrEmpty(txtIdentity.Text) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Identity No  /عفوا...لابد من ادخال رقم الهوية  "))
            Exit Sub
        End If
        If String.IsNullOrEmpty(txtStartDate.Text) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Start Date   /عفوا...لابد من ادخال تاريخ البدء  "))
            Exit Sub
        End If

        If ClsEmployees.ID > 0 Then


            If txtIdentity.Text <> "" Then
                Dim checkCnt = "select top 1 Code from hrs_employees where SSnNo = '" & txtIdentity.Text & "' and Code <> '" & txtCode.Text & "' and branchID = '" & ddlBranch.SelectedValue & "'"
                Dim cnt As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, checkCnt)
                If cnt <> "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " ID NO Used Before Employee No. /رقم الهوية مستخدم سابقا للموظف رقم") & cnt)
                    Exit Sub
                End If
            End If
            txtEmpId.Value = ClsEmployees.ID
            ClsEmployees.Update("code='" & txtCode.Text & "'")


        Else
            'Added By Mohannad
            '2022-06-15
            '-----------------------------------------------
            Try
                If ClsEmployees.Find("SSnNo = '" & txtIdentity.Text.Trim() & "'") Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " The SSNO is used /رقم الهوية مستخدم لموظف آخر"))
                    Exit Sub
                End If
            Catch

            End Try

            'If String.IsNullOrEmpty(txtWorkE_Mail.Text) Then
            '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to enter Work E-mail /عفوا...لابد من اخال بريد العمل"))
            '    Exit Sub
            'End If
            If DdlNationality.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select nationality /عفوا...لابد من تحديد الجنسية "))
                Exit Sub
            End If
            If ddlBranch.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Branch /عفوا...لابد من تحديد الفرع "))
                Exit Sub
            End If
            If ddlDepartment.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Department /عفوا...لابد من تحديد القسم "))
                Exit Sub
            End If
            If ddlSponsor.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Sponsor /عفوا...لابد من تحديد الكفيل "))
                Exit Sub
            End If
            If ddlContractType.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Contract Type /عفوا...لابد من تحديد نوع العقد "))
                Exit Sub
            End If
            If ddlProfessions.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Profession   /عفوا...لابد من تحديد المهنة "))
                Exit Sub
            End If
            If ddlGradeStep.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Grade step   /عفوا...لابد من تحديد الرتبة "))
                Exit Sub
            End If
            If ddlPosition.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Position   /عفوا...لابد من تحديد الوظيفة "))
                Exit Sub
            End If
            If ddlEmployeeClass.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Employee Class   /عفوا...لابد من تحديد الفئة "))
                Exit Sub
            End If
            If String.IsNullOrEmpty(txtJoinDate.Text) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Join Date /عفوا...لابد من ادخال تاريخ المباشرة "))
                Exit Sub
            End If
            If String.IsNullOrEmpty(txtIdentity.Text) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Identity No  /عفوا...لابد من ادخال رقم الهوية  "))
                Exit Sub
            End If
            If String.IsNullOrEmpty(txtStartDate.Text) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Start Date   /عفوا...لابد من ادخال تاريخ البدء  "))
                Exit Sub
            End If

            '-----------------------------------------------
            'Get Next Code
            Dim Cls_Companies As New Clssys_Companies(Page)
            Dim IntSeqLength As Integer = 0
            Dim Retcode As String = ""
            If Cls_Companies.Find("ID=" & Cls_Companies.MainCompanyID) = True Then
                If Cls_Companies.HasSequence = True Then
                    'If txtCode.Text = "" Or txtCode.Text = "Auto" Or txtCode.Text = "تلقائى" Then
                    IntSeqLength = Cls_Companies.SequenceLength
                    Dim strcommand As String = "select Max(isnull(convert(int,Substring(Code,CHARINDEX('" & Cls_Companies.Separator & "',Code)+1,LEN(Code))),0)) + 1 from Hrs_Employees where Substring(Code,CHARINDEX('" & Cls_Companies.Separator & "',Code)+1,LEN(Code)) NOT LIKE '%[a-z]%' and CompanyID = " & Cls_Companies.MainCompanyID
                    Dim strnextcode As String = ""
                    Try
                        strnextcode = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Cls_Companies.ConnectionString, Data.CommandType.Text, strcommand)
                    Catch ex As Exception
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
                        Exit Sub
                    End Try
                    Retcode = strnextcode
                    For I As Integer = 0 To IntSeqLength - strnextcode.Length - 1
                        Retcode = "0" + Retcode
                    Next
                    '' Prefix
                    Dim prfx As String = ""
                    If (Cls_Companies.Prefix = 0) Then
                        prfx = ""
                    ElseIf (Cls_Companies.Prefix = 1) Then
                        Dim cls As New Clssys_Branches(Me)
                        If cls.Find("ID = " & ddlBranch.SelectedValue) Then
                            prfx = cls.Code
                        End If
                    ElseIf (Cls_Companies.Prefix = 2) Then
                        Dim cls As New Clssys_Departments(Me)
                        If cls.Find("ID = " & ddlDepartment.SelectedValue) Then
                            prfx = cls.Code
                        End If
                    ElseIf (Cls_Companies.Prefix = 3) Then
                        Dim cls As New Clshrs_Positions(Me)
                        If cls.Find("ID = " & ddlPosition.SelectedValue) Then
                            prfx = cls.Code
                        End If
                    ElseIf (Cls_Companies.Prefix = 4) Then
                        Dim cls As New Clshrs_ContractTypes(Me)
                        If cls.Find("ID = " & ddlContractType.SelectedValue) Then
                            prfx = cls.Code
                        End If
                    End If

                    txtCode.Text = prfx & IIf(prfx = "", "", Cls_Companies.Separator) & Retcode

                    ClsEmployees.Code = txtCode.Text
                    ClsEmployees.MachineCode = txtCode.Text
                    If txtCode.Text = "" Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                        Exit Sub
                    End If
                    ' End If
                Else
                    If txtEmployeeCode.Text <> "" Then
                        If ClsEmployees.Find("Code = '" & txtEmployeeCode.Text.Trim() & "'") Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " The Employee Code is used /كود الموظف مستخدم لموظف آخر"))
                            Exit Sub
                        Else
                            txtCode.Text = txtEmployeeCode.Text
                            ClsEmployees.Code = txtCode.Text
                            ClsEmployees.MachineCode = txtCode.Text
                        End If
                    Else
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter The Employee Code / برجاء ادخال كود الموظف"))
                        Exit Sub
                    End If
                End If

            End If
            'End Get Code


            If txtIdentity.Text <> "" Then
                Dim checkCnt = "select top 1 Code from hrs_employees where SSnNo = '" & txtIdentity.Text & "' and Code <> '" & txtCode.Text & "' and branchID = '" & ddlBranch.SelectedValue & "'"
                Dim cnt As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, checkCnt)
                If cnt <> "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " ID NO Used Before Employee No. /رقم الهوية مستخدم سابقا للموظف رقم") & cnt)
                    Exit Sub
                End If
            End If

            ClsEmployees.ArbName = txtArbName.Text
            ClsEmployees.EngName = txtEngName.Text
            ClsEmployees.FatherArbName = txtFatherArbName.Text
            ClsEmployees.FatherEngName = txtFatherEngName.Text
            ClsEmployees.GrandArbName = txtGrandArbName.Text
            ClsEmployees.GrandEngName = txtGrandEngName.Text
            ClsEmployees.FamilyArbName = txtFamilyArbName.Text
            ClsEmployees.FamilyEngName = txtFamilyEngName.Text
            ClsEmployees.BirthDate = txtBirthDate.Text
            ClsEmployees.NationalityID = DdlNationality.SelectedValue
            ClsEmployees.BranchID = ddlBranch.SelectedValue
            ClsEmployees.DepartmentID = ddlDepartment.SelectedValue
            ClsEmployees.SectorID = ddlSectors.SelectedValue
            ClsEmployees.LocationID = DropDownList_Location.SelectedValue
            ClsEmployees.JoinDate = txtJoinDate.Text
            ClsEmployees.SponsorID = ddlSponsor.SelectedValue
            ClsEmployees.SSnNo = txtIdentity.Text
            ClsEmployees.BirthCityID = DdlBirthCity.SelectedValue
            ClsEmployees.ReligionID = DdlReligion.SelectedValue
            ClsEmployees.Sex = DdlGender.SelectedValue
            ClsEmployees.MaritalStatusID = DdlMaritalStatus.SelectedValue
            ClsEmployees.BloodGroupID = DdlBloodGroups.SelectedValue
            ClsEmployees.E_Mail = txtEmail.Text
            ClsEmployees.Mobile = txtMobile.Text
            ClsEmployees.PassPortNo = txtPassport.Text
            ClsEmployees.BankID = ddlBank.SelectedValue
            ClsEmployees.BankAccountNumber = txtBankAccount.Text

            ClsEmployees.IsProjectRelated = False
            ClsEmployees.IsSpecialForce = False
            ClsEmployees.Cost1 = GetCostCenter1ID(TxtCostCode1.Text)
            ClsEmployees.Cost2 = GetCostCenter2ID(TxtCostCode2.Text)
            ClsEmployees.Cost3 = GetCostCenter3ID(TxtCostCode3.Text)
            ClsEmployees.Cost4 = GetCostCenter4ID(TxtCostCode4.Text)
            ClsEmployees.paymenttype = ddlLblPaymentType.SelectedValue
            If TextBox_MachineCode.Text <> "" Then
                ClsEmployees.MachineCode = TextBox_MachineCode.Text
            End If

            If txtGosiNumber.Text <> "" Then
                ClsEmployees.GosiNumber = txtGosiNumber.Text
            End If

            If GOSIJoinDate.Text <> "" And GOSIJoinDate.Text <> "  /  /    " Then
                ClsEmployees.GOSIJoinDate = GOSIJoinDate.Text
            End If

            If txtManager.Text <> "" Then
                ClsEmployees2.Find("Code=" & txtManager.Text & "")
                ClsEmployees.ManagerID = ClsEmployees2.ID
            End If

            If ddlBankAccountType.SelectedValue <> "" Then
                ClsEmployees.BankAccountType = ddlBankAccountType.SelectedValue
            End If

            If txtPassportExpireDate.Text <> "" Then
                ClsEmployees.PassportExpireDate = txtPassportExpireDate.Text
            End If

            If txtPassportIssueDate.Text <> "" And txtPassportIssueDate.Text <> "  /  /    " Then
                ClsEmployees.PassportIssueDate = txtPassportIssueDate.Text
            End If

            If txtSSNOExpireDate.Text <> "" And txtSSNOExpireDate.Text <> "  /  /    " Then
                ClsEmployees.SSNOExpireDate = txtSSNOExpireDate.Text
            End If

            If txtSSNOIssueDate.Text <> "" And txtSSNOIssueDate.Text <> "  /  /    " Then
                ClsEmployees.SSNOIssueDate = txtSSNOIssueDate.Text
            End If


            If txtAddressAsPerContract.Text <> "" Then
                ClsEmployees.AddressAsPerContract = txtAddressAsPerContract.Text
            End If

            ClsEmployees.Save()

            ClsEmployees.Find("Code='" & txtCode.Text & "'")


            txtEmpId.Value = ClsEmployees.ID



            Dim Transcommand As String = "update Hrs_NewEmployee set IsTransfered = 1,TransfereDate=GetDate() where ID = " & lblProjectCode.Text
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, Data.CommandType.Text, Transcommand)

        End If

        If ddlLastEducations.SelectedIndex > 0 Then
            Dim clsEmpEducations As New Clshrs_EmployeesEducations(Me)
            If clsEmpEducations.Find("EmployeeID=" & ClsEmployees.ID) Then
                clsEmpEducations.EducationTypeID = ddlLastEducations.SelectedValue
                clsEmpEducations.GraduationDate = SetDate(txtGraduationDate.Text, txtGraduationDate.Text)
                clsEmpEducations.Update("EmployeeID=" & ClsEmployees.ID)
            Else
                clsEmpEducations.EmployeeID = ClsEmployees.ID
                clsEmpEducations.EducationTypeID = ddlLastEducations.SelectedValue
                clsEmpEducations.GraduationDate = SetDate(txtGraduationDate.Text, txtGraduationDate.Text)
                clsEmpEducations.Save()
            End If
        End If

        Dim clsContracts = New Clshrs_Contracts(Page)
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim StrSerial As String = String.Empty
        If lblContractNo.Text = String.Empty Or lblContractNo.Text = "0" Or lblContractNo.Text = "Auto" Or lblContractNo.Text = "تلقائى" Then
            ObjDataHandler.GetLastSerial(clsContracts.Table, "Number", StrSerial, clsContracts.ConnectionString)
            lblContractNo.Text = StrSerial
        End If
        If lblContractNo.Text <> String.Empty Or lblContractNo.Text <> "0" Then
            clsContracts = New Clshrs_Contracts(Page)
            Dim ClsContractTransactions = New Clshrs_ContractsTransactions(Page)
            If Not txtStartWorkDate.Value Is Nothing And lblContractNo.Text.Length > 0 Then
                If SavePart_Contracts() Then
                    clsContracts.Find("Number=" & lblContractNo.Text)
                    txtContractId.Value = clsContracts.ID
                Else
                    txtStartWorkDate.Focus()
                    Exit Sub
                End If
            End If
        End If

        ClsEmployees.SendEmail("frmEmployeeWizard", Me.Page, 1, "V_Hrs_NewEmployee", ClsEmployees.ID)

        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This Employee Saved With Code No : / هذا الموظف تم حفظه برقم كود : ") & ClsEmployees.Code)
        'ClientScript.RegisterStartupScript(Page.GetType(), "winclose", "window.close();", True)
        LinkButton16.Enabled = False
        ClientScript.RegisterStartupScript(Me.GetType(), "CloseWindow", "closeme();", True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> closeme();</script>")
        Venus.Shared.Web.ClientSideActions.ClosePage(Page)


    End Sub

    Protected Sub TxtCostCode1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCostCode1.TextChanged, TxtCostCode1.DataBinding

        If Not String.IsNullOrEmpty(TxtCostCode1.Text) Then
            Dim str As String
            Dim CostCode As String = TxtCostCode1.Text
            Dim CostCenterName As String
            Dim clsEmployees As New Clshrs_NewEmployee(Me)
            Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)

            If ProfileCls.CurrentLanguage = "Ar" Then
                CostCenterName = "ArbName"
            Else
                CostCenterName = "EngName"
            End If
            str = "select " & CostCenterName & " From fcs_CostCenters1 where Code='" & CostCode & "' "
            Dim Cost1Name As String = ""
            Try
                Cost1Name = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str)
                TxtCostName1.Text = Cost1Name
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try
        Else

            TxtCostName1.Text = String.Empty

        End If



    End Sub

    Protected Sub TxtCostCode2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCostCode2.TextChanged
        If Not String.IsNullOrEmpty(TxtCostCode2.Text) Then
            Dim str As String
            Dim CostCode As String = TxtCostCode2.Text
            Dim CostCenterName As String
            Dim clsEmployees As New Clshrs_NewEmployee(Me)
            Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
            If ProfileCls.CurrentLanguage = "Ar" Then
                CostCenterName = "ArbName"
            Else
                CostCenterName = "EngName"
            End If
            str = "select " & CostCenterName & " From fcs_CostCenters2 where Code='" & CostCode & "' "
            Dim Cost2Name As String = ""
            Try
                Cost2Name = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str)
                TxtCostName2.Text = Cost2Name
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
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
            Dim clsEmployees As New Clshrs_NewEmployee(Me)
            Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
            If ProfileCls.CurrentLanguage = "Ar" Then
                CostCenterName = "ArbName"
            Else
                CostCenterName = "EngName"
            End If
            str = "select " & CostCenterName & " From fcs_CostCenters3 where Code='" & CostCode & "' "
            Dim Cost3Name As String = ""
            Try
                Cost3Name = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str)
                TxtCostName3.Text = Cost3Name
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
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
            Dim clsEmployees As New Clshrs_NewEmployee(Me)
            Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
            If ProfileCls.CurrentLanguage = "Ar" Then
                CostCenterName = "ArbName"
            Else
                CostCenterName = "EngName"
            End If
            str = "select " & CostCenterName & " From fcs_CostCenters4 where Code='" & CostCode & "' "
            Dim Cost4Name As String = ""
            Try
                Cost4Name = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str)
                TxtCostName4.Text = Cost4Name
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try
        Else
            TxtCostName4.Text = String.Empty
        End If

    End Sub

End Class
