Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmProjectNewEmployee
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim clsplacement As New Clshrs_ProjectPlacements(Me)
            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsplacement.ConnectionString)
            If ClsNavigationHandler.SetLanguage(Page, "1/0") = "0" Then
                Dim mLeft As New System.Web.UI.WebControls.Unit(96, UnitType.Percentage)
                UltraWebTree1.Padding.Left = mLeft
            End If
            If Not IsPostBack Then
                If Request.QueryString.Count > 2 Then
                    CType(Me.UltraWebTab1.Tabs(0), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
                    CType(Me.UltraWebTab1.Tabs(1), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
                    LinkButton5.Visible = False
                    LinkButton6.Visible = False

                    Dim clsprojectemployees As New Clshrs_ProjectPlacementEmployees(Me.Page)
                    clsprojectemployees.Find("EmployeeID = " & Request.QueryString.Item("EmpID"))

                    clsplacement = New Clshrs_ProjectPlacements(Me)
                    clsplacement.Find("PlacementCode = '" & clsprojectemployees.PlacementCode & "'")

                    Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                    Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsProjects.ConnectionString)
                    clsProjects.Find(" ID = " & clsplacement.ProjectID)
                    HiddenField_ProjectID.Value = clsplacement.ProjectID

                    lblProjectCode.Text = clsProjects.Code
                    lblProjectName.Text = IIf(ClsNavHandler.SetLanguage(Me, "0/1") = 0, clsProjects.EngName, clsProjects.ArbName)

                    Dim clsEducations As New Clshrs_Educations(Me, "hrs_Educations")
                    Dim ClsRelegion As New ClsBasicFiles(Me.Page, "hrs_Religions")
                    Dim ClsMaretalStatus As New ClsBasicFiles(Me.Page, "hrs_MaritalStatus")
                    Dim ClsBloodGroup As New ClsBasicFiles(Me.Page, "hrs_BloodGroups")
                    Dim ClsNationality As New ClsBasicFiles(Me.Page, "sys_Nationalities")
                    Dim ClsCities As New Clssys_Cities(Me.Page)
                    Dim ClsSponsor As New Clshrs_Sponsors(Me.Page)

                    clsEducations.GetDropDownList(ddlLastEducations, True)
                    ClsRelegion.GetDropDownList(DdlReligion, True)
                    ClsMaretalStatus.GetDropDownList(DdlMaritalStatus, True)
                    ClsBloodGroup.GetDropDownList(DdlBloodGroups, True)
                    ClsNationality.GetDropDownList(DdlNationality, True)
                    ClsCities.GetDropDownList(DdlBirthCity, True)
                    ClsSponsor.GetDropDownList(ddlSponsor, True)

                    Dim ClsEmployees As New Clshrs_Employees(Me)
                    If ClsEmployees.Find1("ID=" & Request.QueryString.Item("EmpID")) Then
                        With ClsEmployees
                            HiddenField_EmpNewCode.Value = .Code
                            txtEngName.Text = .EngName
                            txtArbName.Text = .ArbName
                            txtEngFamilyName.Text = .FamilyEngName
                            txtArbFamilyName.Text = .FamilyArbName
                            txtEngFathername.Text = .FatherEngName
                            txtArbFatherName.Text = .FatherArbName
                            txtEngGrandName.Text = .GrandEngName
                            txtArbGrandName.Text = .GrandArbName
                            txtEmail.Text = .E_Mail
                            txtPhone.Text = .Phone
                            txtMobile.Text = .Mobile
                            txtBirthDate.Value = Nothing
                            txtBirthDateH.Value = Nothing
                            If .BirthDate <> Nothing Then
                                txtBirthDate.Value = ClsDataAcessLayer.FormatGreg(.BirthDate, "dd/MM/yyyy").ToString("ddMMyyyy")
                                txtBirthDateH.Value = ClsDataAcessLayer.GregToHijri(.BirthDate, "ddMMyyyy")
                            End If
                            DdlBirthCity.SelectedValue = .BirthCityID
                            DdlReligion.SelectedValue = .ReligionID
                            DdlMaritalStatus.SelectedValue = .MaritalStatusID
                            If .Sex.ToString = "M" Then
                                DdlGender.SelectedIndex = 0
                            ElseIf .Sex.ToString = "F" Then
                                DdlGender.SelectedIndex = 1
                            End If

                            DdlBloodGroups.SelectedValue = .BloodGroupID
                            ddlSponsor.SelectedValue = .SponsorID
                            DdlNationality.SelectedValue = .NationalityID
                            WebMaskEdit_ExpectedJoinDate.Value = Nothing
                            If .JoinDate <> Nothing Then
                                WebMaskEdit_ExpectedJoinDate.Value = CDate(.GetHigriDate(.JoinDate)).ToString("ddMMyyyy")
                            End If
                            txtCode.Text = .SSnNo
                        End With
                    End If
                Else
                    HiddenField_DetailsPrint.Value = 0
                    HiddenField_ContPrint.Value = 0
                    HiddenField_ContID.Value = 0

                    LinkButton4.Visible = False
                    LinkButton11.Visible = False
                    LinkButton12.Visible = False

                    Dim StrProject As String = Request.QueryString.Item("ID")

                    clsplacement = New Clshrs_ProjectPlacements(Me)
                    clsplacement.Find("ID = " & StrProject)

                    Dim clsprojectslocationdetails As New Clshrs_ProjectLocationDetails(Me)
                    clsprojectslocationdetails.Find("ID = " & clsplacement.LocationDetailID)
                    HiddenField_MaxSalary.Value = clsprojectslocationdetails.InternalAmt

                    Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                    Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsProjects.ConnectionString)
                    clsProjects.Find(" ID = " & clsplacement.ProjectID)
                    HiddenField_ProjectID.Value = clsplacement.ProjectID

                    lblProjectCode.Text = clsProjects.Code
                    lblProjectName.Text = IIf(ClsNavHandler.SetLanguage(Me, "0/1") = 0, clsProjects.EngName, clsProjects.ArbName)

                    Dim clsEducations As New Clshrs_Educations(Me, "hrs_Educations")
                    Dim ClsRelegion As New ClsBasicFiles(Me.Page, "hrs_Religions")
                    Dim ClsMaretalStatus As New ClsBasicFiles(Me.Page, "hrs_MaritalStatus")
                    Dim ClsBloodGroup As New ClsBasicFiles(Me.Page, "hrs_BloodGroups")
                    Dim ClsNationality As New ClsBasicFiles(Me.Page, "sys_Nationalities")
                    Dim ClsCities As New Clssys_Cities(Me.Page)
                    Dim ClsSponsor As New Clshrs_Sponsors(Me.Page)

                    clsEducations.GetDropDownList(ddlLastEducations, True)
                    ClsRelegion.GetDropDownList(DdlReligion, True)
                    ClsMaretalStatus.GetDropDownList(DdlMaritalStatus, True)
                    ClsBloodGroup.GetDropDownList(DdlBloodGroups, True)
                    ClsNationality.GetDropDownList(DdlNationality, True)
                    ClsCities.GetDropDownList(DdlBirthCity, True)
                    ClsSponsor.GetDropDownList(ddlSponsor, True)

                    WebMaskEdit_ExpectedJoinDate.Value = IIf(CDate(Request.QueryString.Item("LastDate")) < DateTime.Now, DateTime.Now.ToString("ddMMyyyy"), CDate(Request.QueryString.Item("LastDate")).ToString("ddMMyyyy"))
                    WebMaskEdit1.Value = IIf(CDate(Request.QueryString.Item("LastDate")) < DateTime.Now, DateTime.Now.ToString("ddMMyyyy"), CDate(Request.QueryString.Item("LastDate")).ToString("ddMMyyyy"))
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As System.EventArgs) Handles LinkButton2.Click
        Dim hrs_employees As New Clshrs_Employees(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(hrs_employees.ConnectionString)
        If hrs_employees.Find("SSnNo = '" & txtCode.Text & "' and canceldate is not null") Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "This Already Has Locked Account/هذا الشخص بالفعل موقوف التعامل معه"))
            Exit Sub
        End If

        Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        clsProjects.Find(" ID = " & HiddenField_ProjectID.Value)
        If hrs_employees.Find("SSnNo = '" & txtCode.Text & "' and branchID = " & clsProjects.BranchID) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "This Identity Already Registered on the System/هذه الهوية مسجلة بالفعل فى النظام"))
            Exit Sub
        End If

        CType(Me.UltraWebTab1.Tabs(0), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(1), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton5_Click(sender As Object, e As System.EventArgs) Handles LinkButton5.Click
        CType(Me.UltraWebTab1.Tabs(1), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(0), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton6_Click(sender As Object, e As System.EventArgs) Handles LinkButton6.Click
        Dim clsenum As New Clshrs_Enum(Me.Page)
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsenum.ConnectionString)
        Try
            If CDate(SetDate(WebMaskEdit_ExpectedJoinDate.Text, WebMaskEdit_ExpectedJoinDate.Text)) < CDate(SetDate(WebMaskEdit1.Text, WebMaskEdit1.Text)) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavigationHandler.SetLanguage(Page, " Invalid Join Date /تاريخ الإلتحاق غير صالح"))
                Exit Sub
            End If
        Catch ex As Exception
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavigationHandler.SetLanguage(Page, " Error  /خطأ "))
            Exit Sub
        End Try

        txtSalary.Value = HiddenField_MaxSalary.Value
        txtSalary.MaxValue = HiddenField_MaxSalary.Value
        UltraWebTree1.Nodes.Clear()
        If clsenum.Find("Flag = 3 order by ID") Then
            For Each row As Data.DataRow In clsenum.DataSet.Tables(0).Rows
                Dim ObjTreeNode As New Infragistics.WebUI.UltraWebNavigator.Node
                ObjTreeNode.Text = row(ClsNavigationHandler.SetLanguage(Page, "EngName/ArbName"))
                ObjTreeNode.Tag = row("ID")
                UltraWebTree1.Nodes.Add(ObjTreeNode)
            Next
            UltraWebTree1.ExpandAll()
        End If

        Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        clsProjects.Find(" ID = " & HiddenField_ProjectID.Value)
        Dim StrProject As String = Request.QueryString.Item("ID")
        Dim clsplacement As New Clshrs_ProjectPlacements(Me)
        clsplacement.Find("ID = " & StrProject)
        Dim clsprojectslocationdetails As New Clshrs_ProjectLocationDetails(Me)
        clsprojectslocationdetails.Find("ID = " & clsplacement.LocationDetailID)
        Dim hrsprojectattendanceshift As New Clshrs_AttendanceTableShifts(Me)
        Dim nohours As Decimal = 0
        If hrsprojectattendanceshift.Find("ID in (select Max(AttendanceTableShiftID) from hrs_ProjectPlacementPlanning where PlacementID = " & clsplacement.ID & ")") Then
            Dim Datefrom As DateTime = DateTime.Now.ToString("dd/MM/yyyy") & " " & hrsprojectattendanceshift.TimeIn
            Dim DateTo As DateTime = DateTime.Now.ToString("dd/MM/yyyy") & " " & hrsprojectattendanceshift.TimeOut
            If DateTo < Datefrom Then
                DateTo = DateTo.AddDays(1)
            End If
            nohours = (DateTo - Datefrom).TotalHours
        End If

        Dim Cls_Companies As New Clssys_Companies(Page)
        Dim IntSeqLength As Integer = 0
        Dim Retcode As String = ""
        If String.IsNullOrEmpty(HiddenField_EmpNewCode.Value) Then
            If Cls_Companies.Find("ID=" & Cls_Companies.MainCompanyID) = True Then
                IntSeqLength = Cls_Companies.SequenceLength
                Dim strcommand As String = "select Max(isnull(convert(int,Substring(Code,CHARINDEX('" & Cls_Companies.Separator & "',Code)+1,LEN(Code))),0)) + 1 from Hrs_Employees where Substring(Code,CHARINDEX('" & Cls_Companies.Separator & "',Code)+1,LEN(Code)) NOT LIKE '%[a-z]%' and CompanyID = " & Cls_Companies.MainCompanyID
                Dim strnextcode As String = ""
                Try
                    strnextcode = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Cls_Companies.ConnectionString, Data.CommandType.Text, strcommand)
                Catch ex As Exception
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
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
                    If cls.Find("ID = " & clsProjects.BranchID) Then
                        prfx = cls.Code
                    End If
                ElseIf (Cls_Companies.Prefix = 2) Then
                    Dim cls As New Clssys_Departments(Me)
                    If cls.Find("ID = " & ConfigurationManager.AppSettings("ResDefaultsValues").Split("|")(0).ToString()) Then
                        prfx = cls.Code
                    End If
                ElseIf (Cls_Companies.Prefix = 3) Then
                    Dim cls As New Clshrs_Positions(Me)
                    If cls.Find("ID = " & clsprojectslocationdetails.PositionID) Then
                        prfx = cls.Code
                    End If
                ElseIf (Cls_Companies.Prefix = 4) Then
                    Dim cls As New Clshrs_ContractTypes(Me)
                    If cls.Find("ID = " & ConfigurationManager.AppSettings("ResDefaultsValues").Split("|")(1).ToString()) Then
                        prfx = cls.Code
                    End If
                End If
                HiddenField_EmpNewCode.Value = prfx & IIf(prfx = "", "", Cls_Companies.Separator) & Retcode
            End If
        End If
        Dim EmpID As Integer = 0
        Dim ClsEmployees As New Clshrs_Employees(Me)
        If ClsEmployees.Find1("Code='" & HiddenField_EmpNewCode.Value & "'") Then
            EmpID = ClsEmployees.ID
        End If
        With ClsEmployees
            .Code = HiddenField_EmpNewCode.Value
            .EngName = txtEngName.Text
            .ArbName = txtArbName.Text
            .FamilyEngName = txtEngFamilyName.Text
            .FamilyArbName = txtArbFamilyName.Text
            .FatherEngName = txtEngFathername.Text
            .FatherArbName = txtArbFatherName.Text
            .GrandEngName = txtEngGrandName.Text
            .GrandArbName = txtArbGrandName.Text
            .E_Mail = txtEmail.Text
            .Phone = txtPhone.Text
            .Mobile = txtMobile.Text
            .MachineCode = HiddenField_EmpNewCode.Value
            .BirthDate = SetDate(txtBirthDate.Text, txtBirthDateH.Text)
            .BirthCityID = DdlBirthCity.SelectedItem.Value
            .ReligionID = DdlReligion.SelectedItem.Value
            .MaritalStatusID = DdlMaritalStatus.SelectedItem.Value
            '-------------------------------0257 MODIFIED-----------------------------------------
            If (DdlGender.SelectedValue = "M" Or DdlGender.SelectedValue = "1") Then
                .Sex = "M"
            ElseIf (DdlGender.SelectedValue = "F" Or DdlGender.SelectedValue = "2") Then
                .Sex = "F"
            End If

            .BloodGroupID = DdlBloodGroups.SelectedItem.Value
            .SponsorID = ddlSponsor.SelectedItem.Value
            .NationalityID = DdlNationality.SelectedItem.Value
            .DepartmentID = ConfigurationManager.AppSettings("ResDefaultsValues").Split("|")(0).ToString()
            .LocationID = clsProjects.LocationID
            .BranchID = clsProjects.BranchID
            .JoinDate = SetDate(WebMaskEdit_ExpectedJoinDate.Text, WebMaskEdit_ExpectedJoinDate.Text)
            .SSnNo = txtCode.Text
            .RegComputerID = 1
            .IsProjectRelated = True
            .IsSpecialForce = False
            .WHours = nohours
        End With
        If EmpID = 0 Then
            ClsEmployees.Save()
            ClsEmployees.Find1("Code='" & HiddenField_EmpNewCode.Value & "'")
            EmpID = ClsEmployees.ID
        Else
            ClsEmployees.Update("code='" & HiddenField_EmpNewCode.Value & "'")
        End If

        Dim clscontracts As New Clshrs_Contracts(Me.Page)
        Dim stscommand As String = "delete from hrs_Contracts where employeeID = " & EmpID
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clscontracts.ConnectionString, CommandType.Text, stscommand)

        clscontracts.EmployeeID = EmpID
        clscontracts.ContractTypeId = ConfigurationManager.AppSettings("ResDefaultsValues").Split("|")(1).ToString()
        clscontracts.EmployeeClassID = ConfigurationManager.AppSettings("ResDefaultsValues").Split("|")(2).ToString()
        clscontracts.ContractPeriod = 12
        clscontracts.ProfessionID = ConfigurationManager.AppSettings("ResDefaultsValues").Split("|")(4).ToString()
        clscontracts.PositionID = clsprojectslocationdetails.PositionID
        clscontracts.GradeStepId = ConfigurationManager.AppSettings("ResDefaultsValues").Split("|")(3).ToString()
        clscontracts.CurrencyId = ConfigurationManager.AppSettings("ResDefaultsValues").Split("|")(5).ToString()
        clscontracts.StartDate = SetDate(WebMaskEdit_ExpectedJoinDate.Text, WebMaskEdit_ExpectedJoinDate.Text)
        clscontracts.Number = clscontracts.GetLastContractNumber() + 1
        Dim intNewcontractID As Integer = clscontracts.SaveWithID()
        HiddenField_ContID.Value = intNewcontractID
        Dim stscommandTrns As String = "set dateformat dmy; Insert Into hrs_ContractsTransactions (ContractID,TransactionTypeID,Active,Amount,IntervalID,PaidAtVacation,OnceAtPeriod,ActiveDate,ActiveDate_D) select " & _
                                       intNewcontractID & ",B.TransactionTypeID,A.Active,A.Amount,B.IntervalID,B.PaidAtVacation,B.OnceAtPeriod,'" & SetDate(WebMaskEdit_ExpectedJoinDate.Text, WebMaskEdit_ExpectedJoinDate.Text).ToString("dd/MM/yyyy") & "','0,0' from hrs_GradesStepsTransactions A inner join hrs_GradesTransactions B on A.GradeTransactionID = B.ID where A.GradeStepID = " & ConfigurationManager.AppSettings("ResDefaultsValues").Split("|")(3).ToString() & _
                                       "; Insert Into hrs_EmployeesJoins(EmployeeID,JoinDate,RegUserID,RegDate) values (" & EmpID & ",'" & SetDate(WebMaskEdit_ExpectedJoinDate.Text, WebMaskEdit_ExpectedJoinDate.Text).ToString("dd/MM/yyyy") & "'," & clscontracts.DataBaseUserRelatedID & ",getdate())" & _
                                       "; Insert Into hrs_ContractsVacations(vacationtypeid,ContractId,FromMonth,ToMonth,DurationDays) select VacationTypeID," & intNewcontractID & ",FromMonth,ToMonth,DurationDays from hrs_EmployeesClassesVacations where EmployeeClassID = " & ConfigurationManager.AppSettings("ResDefaultsValues").Split("|")(2).ToString()
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clscontracts.ConnectionString, CommandType.Text, stscommandTrns)

        If ddlLastEducations.SelectedIndex > 0 Then
            Dim clsEmpEducations As New Clshrs_EmployeesEducations(Me)
            If clsEmpEducations.Find("EmployeeID=" & EmpID) Then
                clsEmpEducations.EducationTypeID = ddlLastEducations.SelectedValue
                clsEmpEducations.GraduationDate = SetDate(txtGraduationDate.Text, txtGraduationDate.Text)
                clsEmpEducations.Update("EmployeeID=" & EmpID)
            Else
                clsEmpEducations.EmployeeID = EmpID
                clsEmpEducations.EducationTypeID = ddlLastEducations.SelectedValue
                clsEmpEducations.GraduationDate = SetDate(txtGraduationDate.Text, txtGraduationDate.Text)
                clsEmpEducations.Save()
            End If
        End If

        CType(Me.UltraWebTab1.Tabs(1), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(2), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
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

    Protected Sub LinkButton7_Click(sender As Object, e As System.EventArgs) Handles LinkButton7.Click
        CType(Me.UltraWebTab1.Tabs(2), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(1), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton8_Click(sender As Object, e As System.EventArgs) Handles LinkButton8.Click
        Dim ClsEmployees As New Clshrs_Employees(Page)
        If ClsEmployees.Find1("Code='" & HiddenField_EmpNewCode.Value & "'") Then
            Dim stscommandTrns As String = "set dateformat dmy; update hrs_ContractsTransactions set Amount = " & txtSalary.Value & " where TransactionTypeID in (select ID from hrs_TransactionsTypes where isnull(IsBasicSalary,0) = 1) and ContractID = " & HiddenField_ContID.Value
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, CommandType.Text, stscommandTrns)
            For Each ObjTreeNode As Infragistics.WebUI.UltraWebNavigator.Node In UltraWebTree1.Nodes
                If ObjTreeNode.Checked Then
                    Dim clsenum As New Clshrs_Enum(Me.Page)
                    If clsenum.Find("ID = " & ObjTreeNode.Tag) Then
                        Dim stscommandloan As String = "set dateformat dmy; insert into hrs_EmployeesPayabilities (Number,EmployeeID,TransactionTypeID,TransactionDate,TransactionComment,SalaryLink,RegUserID,RegDate) values ((select Max(Number) + 1 from hrs_EmployeesPayabilities)," & ClsEmployees.ID & "," & clsenum.Remarks & ",getdate(),'From Employement Form',1," & ClsEmployees.RegUserID & ",getdate());" & _
                                                       " insert into hrs_EmployeesPayabilitiesSchedules (EmployeePayabilityID,DueDate,DueAmount,Rank,CompanyID,RegDate,RegUserID) values ((select max(ID) from hrs_EmployeesPayabilities),'" & SetDate(WebMaskEdit_ExpectedJoinDate.Text, WebMaskEdit_ExpectedJoinDate.Text).ToString("dd/MM/yyyy") & "'," & clsenum.Code & ",0," & ClsEmployees.CompanyID & ",getdate()," & ClsEmployees.RegUserID & ");"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, CommandType.Text, stscommandloan)
                    End If
                End If
            Next
            frame1.Attributes("src") = "frmEmployeesDocuments.aspx?TB=" & ClsEmployees.Table.ToString.Trim() & "&SV=" & ClsEmployees.ID & "&St=0"
        End If
        CType(Me.UltraWebTab1.Tabs(2), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(3), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton9_Click(sender As Object, e As System.EventArgs) Handles LinkButton9.Click
        CType(Me.UltraWebTab1.Tabs(3), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(2), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton10_Click(sender As Object, e As System.EventArgs) Handles LinkButton10.Click
        Dim ClsEmployees As New Clshrs_Employees(Page)
        ClsEmployees.Find1("Code='" & HiddenField_EmpNewCode.Value & "'")
        Dim TableName As String = Request.QueryString.Item("TB")
        Dim Clsobjects As New Clssys_Objects(Me.Page)
        Clsobjects.Find(" Code = REPLACE('" & ClsEmployees.Table.ToString.Trim() & "',' ' ,'')")

        Dim ObjClsDocumentDetails As New Clssys_DocumentsInformations(Me.Page)
        Dim ClsDocumentType As New Clssys_DocumentsTypes(Me.Page)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        If ObjClsDocumentDetails.Find("ObjectId = " & Clsobjects.ID & " And  RecordID=" & ClsEmployees.ID & " And CancelDate Is Null") Then
            If ObjClsDocumentDetails.DataSet.Tables(0).Rows.Count = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "No Documents Added Yet/لا توجد مستندات مضافة"))
                Exit Sub
            End If
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "No Documents Added Yet/لا توجد مستندات مضافة"))
            Exit Sub
        End If
        CType(Me.UltraWebTab1.Tabs(3), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(4), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub
    Protected Sub LinkButton15_Click(sender As Object, e As System.EventArgs) Handles LinkButton15.Click
        CType(Me.UltraWebTab1.Tabs(4), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(3), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton16_Click(sender As Object, e As System.EventArgs) Handles LinkButton16.Click
        If Request.QueryString.Count = 2 Then
            Dim hrsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
            Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(hrsProjectLocationDetails.ConnectionString)
            If HiddenField_ContPrint.Value > 0 And HiddenField_DetailsPrint.Value > 0 Then
                Dim clsplacement As New Clshrs_ProjectPlacements(Me)
                Dim ClsEmployees As New Clshrs_Employees(Page)
                clsplacement.Find("ID = " & Request.QueryString.Item("ID"))
                ClsEmployees.Find1("Code='" & HiddenField_EmpNewCode.Value & "'")
                Dim clsplacementemployees As New Clshrs_ProjectPlacementEmployees(Me)
                clsplacementemployees.PlacementCode = clsplacement.PlacementCode
                clsplacementemployees.EmployeeID = ClsEmployees.ID
                clsplacementemployees.FromDate = SetDate(WebMaskEdit_ExpectedJoinDate.Text, WebMaskEdit_ExpectedJoinDate.Text)
                clsplacementemployees.Status = 0
                clsplacementemployees.Save()
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "The Process Done/تم تنفيذ العملية بنجاح"))
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "You Don't Print The Data Yet/لم تتم الطباعة الى الأن"))
                Exit Sub
            End If
        End If
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe();", True)
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As System.EventArgs) Handles LinkButton1.Click
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">PrintContract(" & HiddenField_EmpNewCode.Value & ");</script>")
        HiddenField_ContPrint.Value = HiddenField_ContPrint.Value + 1
    End Sub

    Protected Sub LinkButton3_Click(sender As Object, e As System.EventArgs) Handles LinkButton3.Click
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">PrintAttend(" & HiddenField_ProjectID.Value & "," & Request.QueryString.Item("ID") & ");</script>")
        HiddenField_DetailsPrint.Value = HiddenField_DetailsPrint.Value + 1
    End Sub

    Protected Sub LinkButton4_Click(sender As Object, e As System.EventArgs) Handles LinkButton4.Click
        Dim clscontracts As New Clshrs_Contracts(Me.Page)
        clscontracts.Find("EmployeeID = " & Request.QueryString.Item("EmpID"))
        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmEmployeePeriodicalTransactions.aspx?EmpID=" & Request.QueryString.Item("EmpID") & "&ContID=" & clscontracts.ID, 700, 490, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "wWindO", False, True, False, False, False, False, False, False, False)
    End Sub

    Protected Sub LinkButton11_Click(sender As Object, e As System.EventArgs) Handles LinkButton11.Click
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmEmployeesDocuments.aspx?TB=" & ClsEmployees.Table.ToString.Trim() & "&SV=" & Request.QueryString.Item("EmpID"), 700, 450, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "wWindO", False, True, False, False, False, False, False, False, False)
    End Sub

    Protected Sub LinkButton12_Click(sender As Object, e As System.EventArgs) Handles LinkButton12.Click
        Dim ClsEmployees As New Clshrs_Employees(Me)
        If ClsEmployees.Find1("ID=" & Request.QueryString.Item("EmpID")) Then
            ClsEmployees.RegComputerID = 0
            ClsEmployees.Update("ID=" & Request.QueryString.Item("EmpID"))
        End If
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe();", True)
    End Sub
End Class
