Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeesSelector_old
    Inherits MainPage

#Region "Protected Sub"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("SM")
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(Page)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
        Dim IntSelectedPeriod As Integer = 0
        Dim IntModuleId As Integer = GetModuleID("frmPrepareSalaries")
        Dim clsBranch As New Clssys_Branches(Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim clsContracttype As New Clshrs_ContractTypes(Page)
        Dim clsSponsor As New Clshrs_Sponsors(Page)

        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
        If ClsObjects.Find(" Code='" & ClsEmployee.Table.Trim & "'") Then
            If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
        End If

        If ClsObjects.Find(" Code='" & clsContracttype.Table.Trim & "'") Then
            If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TextBox_Contract.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & TextBox_Contract.ClientID & "'"
                WebImageButton_Cont.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
        End If

        If ClsObjects.Find(" Code='" & clsSponsor.Table.Trim & "'") Then
            If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TextBox_Sponsor.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & TextBox_Sponsor.ClientID & "'"
                WebImageButton_Sponsor.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
        End If

        If Not IsPostBack Then
            Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
            Dim ClsNationalities As New ClsBasicFiles(Me.Page, "sys_Nationalities")
            ClsDepartment.GetDropDownList(ddlDepartment, True)
            ddlDepartment.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Departments]/ [ جميع الإدارات]")

            ClsNationalities.GetDropDownList(ddlNationality, True)
            ddlNationality.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Nationalities]/ [ جميع الجنسيات]")

            clsBranch.GetDropDownList(ddlBranche, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")

            ClsFisicalYearsPeriods.GetDropDownList(DdlPeriods, IntModuleId, True, "")

            Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
            Dim strbranchfilter As String = ""
            strbranchfilter = " and BranchID = " & ddlBranche.SelectedValue
            clsProjects.GetDropDownList(DropDownList_Project, True, "isnull(BranchID,0) = 0 or (IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and EndDate >= (select ToDate from sys_FiscalYearsPeriods where ID = " & DdlPeriods.SelectedValue & ")" & strbranchfilter & ")")
            DropDownList_Project.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Employee Has Not Attendance Transactions]/ [ جميع الموظفين بدون إدخالات حضور وانصراف]")
            DropDownList_Project.Items.Insert(0, New System.Web.UI.WebControls.ListItem(ClsNavigationHandler.SetLanguage(Page, "[All Employee Has Attendance Transactions]/ [ جميع الموظفين مع إدخالات حضور وانصراف]"), -1))
            DropDownList_Project.Items.Insert(0, New System.Web.UI.WebControls.ListItem(ClsNavigationHandler.SetLanguage(Page, "[All Employees]/ [ جميع الموظفين]"), -2))
            IntSelectedPeriod = ClsFisicalYearsPeriods.GetLastOpenedFiscalPieriod(IntModuleId)

            UwgSearchEmployees.Columns.FromKey("FullName").CellStyle.HorizontalAlign = CInt(ClsNavigationHandler.SetLanguage(Page, "1/3"))
            DdlPeriods.SelectedIndex = 0

            lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
            Page.Session.Add("Lage", lblLage.Text)
            Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)

            If StrMode = "Att" Then
                Label_Header.Text = IIf(lblLage.Text = 0, "<b> Attendance Preparation : </b>(This form is designed to prepare or refund for attendance)", "<b>شاشة تجهيز الدوام </b>(تم تصميم هذا النموذج لتجهيز أو إلغاء التجهيز للدوام)")
                LinkButton_Import.Visible = True
                ImageButton_Import.Visible = True
                LinkButton_Fingerprint.Visible = True
                ImageButton_Fingerprint.Visible = True
            End If

            If StrMode = "Sal" Then
                Label_Header.Text = IIf(lblLage.Text = 0, "<b> Salary Preparation : </b>(This form is designed to prepare or refund for salaries)", "<b>شاشة تجهيز الرواتب </b>(تم تصميم هذا النموذج لتجهيز أو إلغاء التجهيز للرواتب)")
            End If
            ddlFilter.SelectedValue = ConfigurationManager.AppSettings("AttendaceFilterDefaultValue")
        End If
    End Sub

    Protected Sub DdlPeriods_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlPeriods.SelectedIndexChanged
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
        Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        Dim strbranchfilter As String = ""
        strbranchfilter = " and BranchID = " & ddlBranche.SelectedValue
        clsProjects.GetDropDownList(DropDownList_Project, True, "isnull(BranchID,0) = 0 or (IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and EndDate >= (select ToDate from sys_FiscalYearsPeriods where ID = " & DdlPeriods.SelectedValue & ")" & strbranchfilter & ")")
        DropDownList_Project.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Employee Has Not Attendance Transactions]/ [ جميع الموظفين بدون إدخالات حضور وانصراف]")
        DropDownList_Project.Items.Insert(0, New System.Web.UI.WebControls.ListItem(ClsNavigationHandler.SetLanguage(Page, "[All Employee Has Attendance Transactions]/ [ جميع الموظفين مع إدخالات حضور وانصراف]"), -1))
        DropDownList_Project.Items.Insert(0, New System.Web.UI.WebControls.ListItem(ClsNavigationHandler.SetLanguage(Page, "[All Employees]/ [ جميع الموظفين]"), -2))
        GetData(True)
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetData(True)
    End Sub
    Protected Sub btnFingerprint_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Fingerprint.Click, ImageButton_Fingerprint.Click
        Dim ClsFingerprintSettings As New clsFingerprintSettings(Page)
        Try
            ClsFingerprintSettings.Find("IsDefault=1")

            If ClsFingerprintSettings.DataSet.Tables(0).Rows.Count > 0 Then
                For Each row As System.Data.DataRow In ClsFingerprintSettings.DataSet.Tables(0).Rows
                    ClsFingerprintSettings.GetFingerPrints(row("ID").ToString())
                Next

                'Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Done !/!تمت العملية"))
            End If
        Catch ex As Exception
            ' Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Fail !/!فشلت العملية"))
        End Try
    End Sub

    Protected Sub Button_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Prepare.Command, LinkButton_Refund.Command, ImageButton_Prepare.Command, ImageButton_Refund.Command, ImageButton_Import.Click, LinkButton_Import.Command
        Try
            Dim StrMode As String = Request.QueryString.Item("SM")

            Select Case DirectCast(e, System.Web.UI.WebControls.CommandEventArgs).CommandArgument
                Case "Prepare"
                    If StrMode = "Att" Then
                        Get_FromToDate()
                        SetDataAtt()
                        GetData(True)
                    End If

                    If StrMode = "Sal" Then
                        Get_FromToDate()
                        SetData_Salary()
                    End If

                Case "Refund"
                    If StrMode = "Att" Then
                        Get_FromToDate()
                        SetDataAtt(False)
                        GetData(True)
                    End If

                    If StrMode = "Sal" Then
                        Get_FromToDate()
                        Dim ClsEmployee As New Clshrs_Employees(Page)
                        ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
                        Try
                            Dim EmpIDs As String = String.Empty
                            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                                If row.Cells(1).Value = True Then
                                    If row.Cells(0).Value > 0 Then
                                        EmpIDs &= row.Cells(0).Value & ","
                                    End If
                                End If
                            Next
                            EmpIDs = EmpIDs.Remove(EmpIDs.Length - 1)

                            Dim strwhr As String = ""
                            strwhr = " and EmployeeID in(" & EmpIDs & ") "
                            Dim str As String = "Set DateFormat DMY delete from hrs_EmployeesTransactions where isnull(Applyed,0) = 0 and PostDate is null and FiscalYearPeriodID = '" & DdlPeriods.SelectedValue & "' and PrepareType = 'N' and ID not in (select isnull(mstr.RegComputerID,0) from hrs_EmployeesTransactions mstr)" & strwhr
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, str)
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Done !/!تمت العملية"))
                            GetData(True)
                        Catch ex As Exception
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Fail !/!فشلت العملية"))
                        End Try
                    End If
                Case "Import"
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmAttendanceLoad.aspx", 800, 170, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "wWindO", False, True, False, False, False, False, False, False, False)
            End Select
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Private Function"

    Private Function GetData(ByVal isfill As Boolean) As DataSet
        Try

            Dim StrMode As String = Request.QueryString.Item("SM")
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)


            Dim BranchID As Integer = ddlBranche.SelectedValue
            Dim DepartmentID As Integer = ddlDepartment.SelectedValue
            Dim NationalityID As Integer = ddlNationality.SelectedValue
            Dim strFilter As String = String.Empty

            UwgSearchEmployees.DataSource = Nothing
            UwgSearchEmployees.DataBind()

            If DdlPeriods.SelectedIndex = 0 Then
                Return Nothing
            End If

            Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)
            ClsFisicalPeriods.Find("ID=" & DdlPeriods.SelectedValue)
            Dim FromDate As DateTime = ClsFisicalPeriods.FromDate
            Dim ToDate As DateTime = ClsFisicalPeriods.ToDate
            Dim FiscFromDate As DateTime = ClsFisicalPeriods.FromDate
            Dim FiscToDate As DateTime = ClsFisicalPeriods.ToDate

            Dim clsCompanies As New Clssys_Companies(Page)
            clsCompanies.Find("ID=" & ClsFisicalPeriods.MainCompanyID)
            Dim clsBranch As New Clssys_Branches(Page)
            clsBranch.Find("ID=" & ddlBranche.SelectedValue)
            If clsBranch.PrepareDay > 0 Then
                If clsCompanies.IsHigry = True Then
                    Dim strarr As String() = ClsFisicalPeriods.HFromDate.Split("/")
                    Dim FrmHDate As String = clsBranch.PrepareDay & "/" & IIf(strarr(1) = "01", "12", strarr(1) - 1) & "/" & IIf(strarr(1) = "01", strarr(2) - 1, strarr(2))
                    ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy")
                    FromDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy"), "dd/MM/yyyy")

                    Dim strarr1 As String() = FrmHDate.Split("/")
                    Dim ToHDate As String = clsBranch.PrepareDay - 1 & "/" & IIf(strarr1(1) = "12", "01", strarr1(1) + 1) & "/" & IIf(strarr1(1) = "12", strarr1(2) + 1, strarr1(2))
                    ToDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(ToHDate, "dd/MM/yyyy"), "dd/MM/yyyy")
                Else
                    FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsBranch.PrepareDay)
                    ToDate = FromDate.AddMonths(1).AddDays(-1)
                End If
                If clsBranch.AffectPeriod Then
                    FiscFromDate = FromDate
                    FiscToDate = ToDate
                End If
            Else
                If clsCompanies.PrepareDay > 0 Then
                    If clsCompanies.IsHigry = True Then
                        Dim strarr As String() = ClsFisicalPeriods.HFromDate.Split("/")
                        Dim FrmHDate As String = clsCompanies.PrepareDay & "/" & IIf(strarr(1) = "01", "12", strarr(1) - 1) & "/" & IIf(strarr(1) = "01", strarr(2) - 1, strarr(2))
                        ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy")
                        FromDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy"), "dd/MM/yyyy")

                        Dim strarr1 As String() = FrmHDate.Split("/")
                        Dim ToHDate As String = clsCompanies.PrepareDay - 1 & "/" & IIf(strarr1(1) = "12", "01", strarr1(1) + 1) & "/" & IIf(strarr1(1) = "12", strarr1(2) + 1, strarr1(2))
                        ToDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(ToHDate, "dd/MM/yyyy"), "dd/MM/yyyy")
                    Else
                        FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsCompanies.PrepareDay)
                        ToDate = FromDate.AddMonths(1).AddDays(-1)
                    End If
                End If
            End If

            Dim strCommand As String = "Set DateFormat DMY; Select hrs_Employees.ID, Code, dbo.fn_GetEmpName(Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS FullName "
            Dim extrafltr As String = ""
            If DropDownList_Project.SelectedValue = 0 Then
                extrafltr = extrafltr & " isnull(hrs_Employees.IsProjectRelated,0) = 0 And hrs_Employees.ID not in (select distinct Att1.EmployeeID from Att_AttendTransactions Att1 where CONVERT(Datetime,Att1.TrnsDatetime,103)>= CONVERT(Datetime,'" & FromDate & "',103) and CONVERT(Datetime,Att1.TrnsDatetime,103) <= CONVERT(Datetime,'" & ToDate & "',103)) And "
            ElseIf DropDownList_Project.SelectedValue = -1 Then
                extrafltr = extrafltr & " hrs_Employees.ID in (select distinct Att1.EmployeeID from Att_AttendTransactions Att1 where CONVERT(Datetime,Att1.TrnsDatetime,103)>= CONVERT(Datetime,'" & FromDate & "',103) and CONVERT(Datetime,Att1.TrnsDatetime,103) <= CONVERT(Datetime,'" & ToDate & "',103)) And "
            ElseIf DropDownList_Project.SelectedValue = -2 Then
                extrafltr = extrafltr & " ID > 0 and "
            Else
                extrafltr = extrafltr & " hrs_Employees.ID in (select distinct Att1.EmployeeID from Att_AttendTransactions Att1 where CONVERT(Datetime,Att1.TrnsDatetime,103)>= CONVERT(Datetime,'" & FromDate & "',103) and CONVERT(Datetime,Att1.TrnsDatetime,103) <= CONVERT(Datetime,'" & ToDate & "',103) and  (select top 1 Att2.ProjectID from Att_AttendTransactions Att2 where Att2.EmployeeID = Att1.EmployeeID and CONVERT(Datetime,Att2.TrnsDatetime,103)>= CONVERT(Datetime,'" & FromDate & "',103) and CONVERT(Datetime,Att2.TrnsDatetime,103) <= CONVERT(Datetime,'" & ToDate & "',103) order by Att2.TrnsDatetime DESC) = " & DropDownList_Project.SelectedValue & ") And "
            End If

            If StrMode = "Att" Then
                strCommand &= ",(Select COUNT(ID) from Att_AttendancePreparationDetails where EmployeeID = hrs_Employees.ID and CONVERT(Datetime,GAttendDate,103)>= CONVERT(Datetime,'" & FromDate & "',103) and CONVERT(Datetime,GAttendDate,103) <= CONVERT(Datetime,'" & ToDate & "',103))  as Prepared"

                strFilter &= " From hrs_Employees where dbo.fn_CheckEndOfServiceByPeriod(hrs_Employees.ID," & ClsFisicalPeriods.ID & ")>0 AND "

                If ddlFilter.SelectedValue = 1 Then
                    strFilter &= " (Select COUNT(ID) from Att_AttendancePreparationDetails where EmployeeID = hrs_Employees.ID and CONVERT(Datetime,GAttendDate,103)>= CONVERT(Datetime,'" & FromDate & "',103) and CONVERT(Datetime,GAttendDate,103) <= CONVERT(Datetime,'" & ToDate & "',103))>0 And "
                ElseIf ddlFilter.SelectedValue = 2 Then
                    strFilter &= " (Select COUNT(ID) from Att_AttendancePreparationDetails where EmployeeID = hrs_Employees.ID and CONVERT(Datetime,GAttendDate,103)>= CONVERT(Datetime,'" & FromDate & "',103) and CONVERT(Datetime,GAttendDate,103) <= CONVERT(Datetime,'" & ToDate & "',103))=0 And "
                ElseIf ddlFilter.SelectedValue = 3 Then
                    strFilter &= " hrs_employees.id in (select employeeid from Att_AttendTransactions where TrnsDatetime>= CONVERT(Datetime,'" & FromDate & "',103) and TrnsDatetime<= CONVERT(Datetime,'" & ToDate & "',103)) And "


                End If
            End If

            If StrMode = "Sal" Then
                strCommand &= ",(Select COUNT(ID) From hrs_EmployeesTransactions Where EmployeeID =hrs_Employees.ID And FiscalYearPeriodID =" & ClsFisicalPeriods.ID & " And PrepareType ='N')  as Prepared"

                If Convert.ToString(clsCompanies.Remarks) = "1" Then
                    strFilter &= " From hrs_Employees where dbo.fn_CheckEndOfServiceByPeriodWEnd(hrs_Employees.ID," & ClsFisicalPeriods.ID & ")>0 AND "
                Else
                    strFilter &= " From hrs_Employees where dbo.fn_CheckEndOfServiceByPeriod(hrs_Employees.ID," & ClsFisicalPeriods.ID & ")>0 AND "
                End If

                If ddlFilter.SelectedValue = 1 Then
                    strFilter &= " (Select COUNT(ID) From hrs_EmployeesTransactions Where EmployeeID =hrs_Employees.ID And FiscalYearPeriodID =" & ClsFisicalPeriods.ID & " And PrepareType ='N')>0  And "
                ElseIf ddlFilter.SelectedValue = 2 Then
                    strFilter &= " (Select COUNT(ID) From hrs_EmployeesTransactions Where EmployeeID =hrs_Employees.ID And FiscalYearPeriodID =" & ClsFisicalPeriods.ID & " And PrepareType ='N')=0  And "
                ElseIf ddlFilter.SelectedValue = 3 Then
                    strFilter &= "hrs_employees.id in (select employeeid from Att_AttendTransactions where TrnsDatetime>= CONVERT(Datetime,'" & FromDate & "',103) and TrnsDatetime<= CONVERT(Datetime,'" & ToDate & "',103)) And "
                End If
            End If
            strFilter &= extrafltr
            strFilter &= " SponsorID in (select ID from hrs_Sponsors where Code like '%" & TextBox_Sponsor.Text & "%') and ID in (select EmployeeID from hrs_Contracts where ContractTypeID in (select ID from hrs_ContractsTypes where Code like '%" & TextBox_Contract.Text & "%'))  and Code like '%" & txtCode.Text & "%' "





            strFilter &= " And isnull(hrs_Employees.RegComputerID,0) = 0 "


            If BranchID > 0 Then
                strFilter &= "  And hrs_Employees.BranchID = " & BranchID

            End If


            If DepartmentID > 0 Then
                strFilter &= " And hrs_Employees.DepartmentID = " & DepartmentID
            End If

            If NationalityID > 0 Then
                strFilter &= " And hrs_Employees.NationalityID = " & NationalityID
            End If

            Dim ClsUsers As New Clssys_Users(Page)
            ClsUsers.Find("ID=" & ClsFisicalPeriods.DataBaseUserRelatedID)
            Dim Filcommand As String = "select * from Sys_UsersViewDomain where UserCode = '" & ClsUsers.Code & "'"
            Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsFisicalPeriods.ConnectionString, CommandType.Text, Filcommand).Tables(0)
            If (DT.Rows.Count > 0) Then
                If Convert.ToString(DT.Rows(0)("Departments")) <> "" Then
                    strFilter &= " And hrs_Employees.DepartmentID in (" & Convert.ToString(DT.Rows(0)("Departments")) & ")"
                End If
                If Convert.ToString(DT.Rows(0)("Cost1")) <> "" Then
                    strFilter &= " And hrs_Employees.Cost1 in (" & Convert.ToString(DT.Rows(0)("Cost1")) & ")"
                End If
                If Convert.ToString(DT.Rows(0)("Cost2")) <> "" Then
                    strFilter &= " And hrs_Employees.Cost2 in (" & Convert.ToString(DT.Rows(0)("Cost2")) & ")"
                End If
                If Convert.ToString(DT.Rows(0)("Cost3")) <> "" Then
                    strFilter &= " And hrs_Employees.Cost3 in (" & Convert.ToString(DT.Rows(0)("Cost3")) & ")"
                End If
                If Convert.ToString(DT.Rows(0)("Cost4")) <> "" Then
                    strFilter &= " And hrs_Employees.Cost4 in (" & Convert.ToString(DT.Rows(0)("Cost4")) & ")"
                End If
            End If

            strCommand &= strFilter & " order by Case When IsNumeric(Code) = 1 then Right(Replicate('0',51) + Code, 50) When IsNumeric(Code) = 0 then Left(Code + Replicate('',51), 50) Else Code End"

            Dim dsEmployee As New Data.DataSet
            dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, Data.CommandType.Text, strCommand)

            If isfill Then
                If dsEmployee.Tables(0).Rows.Count > 0 Then
                    Dim ds As New Data.DataSet
                    ds.Tables.Add()
                    ds.Tables(0).Columns.Add("Code")
                    ds.Tables(0).Rows.Add(ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل"))
                    ds.Tables(0).Merge(dsEmployee.Tables(0))

                    UwgSearchEmployees.DataSource = ds.Tables(0)
                    UwgSearchEmployees.DataBind()
                End If

                ddlDepartment.SelectedValue = DepartmentID
                ddlNationality.SelectedValue = NationalityID
                ddlBranche.SelectedValue = BranchID
            End If
            Return dsEmployee
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Function GetModuleID(ByVal TableName As String) As Integer
        Dim ClsForms As New ClsSys_Forms(Me.Page)
        Dim IntModuleID As Integer
        ClsForms.Find(" Code = '" & TableName & "'")
        If ClsForms.ID > 0 Then
            IntModuleID = ClsForms.ModuleID
        End If
        Return IntModuleID
    End Function

#End Region

#Region "Salary Function"

    Private ClsEmployeesTransactions As Clshrs_EmployeesTransactions
    Private ClsEmployeeTransactionsDet As Clshrs_EmployeesTransactionsDetails
    Private ClsContractTransactions As Clshrs_ContractsTransactions
    Private ClsEmpTrancProjects As Clshrs_EmployeesTransactionsProjects
    Private ClsFisicalPeriods As Clssys_FiscalYearsPeriods

    Private ClsProjects As Clshrs_Projects
    Private ClsEmployees As Clshrs_Employees
    Private ClsEmployeesContracts As Clshrs_Contracts
    Private ClsVacationsTypes As Clshrs_VacationsTypes
    Private ClsEmployeeClass As Clshrs_EmployeeClasses
    Private ObjNavigationHandler As Venus.Shared.Web.NavigationHandler
    Private clsTransType As Clshrs_TransactionsTypes
    Private ClsAttendancePreparationDetails As ClsAtt_AttendancePreparationDetails
    Private clsAttendancePreparationProjects As ClsAtt_AttendancePreparationProjects
    Private ClsEmployeesTransactionsProjects As Clshrs_EmployeesTransactionsProjects
    Private clsMainCurrency As ClsSys_Currencies
    Private ClsCountries As Clssys_Countries
    Private clsCompanies As Clssys_Companies
    Private clsBranch As Clssys_Branches

    Private FromDate As DateTime
    Private ToDate As DateTime
    Private FiscFromDate As DateTime
    Private FiscToDate As DateTime

    Private arrList As New ArrayList
    Private DtBenefits As New Data.DataTable
    Private DtDeductions As New Data.DataTable
    Private dbOvertimeSalary As Double
    Private dbHolidayHoursSalary As Double
    Private dblBenefits As Double
    Private dblDeduct As Double
    Private dbBasicSalary As Double

    Private IntNoOfDays As Integer
    Private IntNoOfWorkDays As Integer
    Private IntFisicalPeriod As Integer
    Private intNoDecimalPlaces As Integer
    Private intEmployeeContarctID As Integer
    Private ObjSalaryPerDay As Double
    Private ObjSalaryPerHour As Double
    Private ObjOverTime As Double
    Private ObjAbsent As Double
    Private ObjTotalLate As String

    Private BolPrepared As Boolean = False
    Private penaltyDays As Single = 0
    Private ObjPrepaerdData As ArrayList
    Private totalBenefits As Double = 0
    Private totalDeducation As Double = 0
    Private NotPermitLat As Double = 0
    Private dbTotalAbsent As Double = 0


    Private dbOTSalary As Double = 0
    Private dbHOTSalary As Double = 0

    Const CPreparedData_calctype As Int16 = 9
    Const CPreparedData_AvaliableDays As Int16 = 8
    Const CPreparedData_Prepared As Int16 = 4
    Const CPreparedData_TotalPenalty As Int16 = 7
    Const CPreparedData_FirstPrepare As Int16 = 5

    Private Function Refund(EmpID As Integer) As Boolean
        Try
            Dim IntEmployeeID As Integer = EmpID
            Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Me)
            Dim ClsEmployeeTransactionsDet As New Clshrs_EmployeesTransactionsDetails(Me)
            Dim ClsEmployeeesPayablitySetelment As New Clshrs_EmployeesPayabilitySchedulesSettlement(Me)
            Dim ClsEmployeesTransactionsProjects As New Clshrs_EmployeesTransactionsProjects(Me)

            If ClsEmployeesTransactions.Find("EmployeeID=" & EmpID & " And FiscalYearPeriodID=" & IntFisicalPeriod & " And PrepareType ='N'") Then
                If ClsEmployeesTransactions.Find("EmployeeID=" & EmpID & " And FiscalYearPeriodID = " & IntFisicalPeriod & " And PrepareType='N' and PostDate is null and isnull(Applyed,0) = 0") Then
                    If ClsEmployeesTransactions.Find("EmployeeID=" & EmpID & " And FiscalYearPeriodID = " & IntFisicalPeriod & " And PrepareType='N' and PostDate is null and ID not in (select isnull(mstr.RegComputerID,0) from hrs_EmployeesTransactions mstr)") Then
                        ClsEmployeeTransactionsDet.DeleteAll("EmployeeTransactionID=" & ClsEmployeesTransactions.ID)
                        ClsEmployeesTransactionsProjects.DeleteAll("EmployeeTransactionID=" & ClsEmployeesTransactions.ID)
                        ClsEmployees.DeleteEmployeesPenalties(ClsEmployeesTransactions.ID)
                        ClsEmployeesTransactions.DeleteAll("ID=" & ClsEmployeesTransactions.ID)
                    End If
                Else
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function CheckEmployee(ByVal IntInComingEmployeeID As Integer, ByVal FiscalPeriodID As Integer) As Boolean
        Dim ClsContract As New Clshrs_Contracts(Page)
        Dim clsEmployeevacations As New Clshrs_EmployeesVacations(Page)
        Dim ClsFiscalPeriods As New Clssys_FiscalYearsPeriods(Page)
        ClsFiscalPeriods.Find("ID=" & FiscalPeriodID)
        Dim intValidContract As Integer = 0
        Try
            intValidContract = ClsContract.ContractValidatoinId(IntInComingEmployeeID, FiscalPeriodID)
            If intValidContract <= 0 Then
                intEmployeeContarctID = 0
                Return False
            End If
            If (clsEmployeevacations.FindEmployeeVacations(" hrs_EmployeesVacations.VacationTypeID = (select top 1 ID from hrs_VacationsTypes where IsAnnual = 1 and CancelDate is null) and hrs_EmployeesVacations.EmployeeID=" & IntInComingEmployeeID & " And Convert(smalldatetime,Convert(varchar,ActualStartDate ,103)) <= Convert(smalldatetime,Convert(varchar,'" & FiscFromDate & "' ,103))	And	(ActualEndDate Is Null Or  Convert(smalldatetime,Convert(varchar,ActualEndDate ,103)) > Convert(smalldatetime,Convert(varchar,'" & FiscToDate & "',103)))")) Then
                intEmployeeContarctID = 0
            End If
            intEmployeeContarctID = intValidContract

            ClsEmployeesContracts.Find("ID = " & intEmployeeContarctID)
            ClsEmployeeClass.Find("ID =" & ClsEmployeesContracts.EmployeeClassID)
            IntNoOfDays = Get_NoOfDays()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function Get_NoDecimalPlaces() As Integer
        Try
            clsMainCurrency = New ClsSys_Currencies(Page)
            ClsCountries = New Clssys_Countries(Page)

            If ClsCountries.Find(" IsMainCountries = 1 ") Then
                clsMainCurrency.Find(" ID=" & ClsCountries.CurrencyID)
                If Not IsNothing(clsMainCurrency.NoDecimalPlaces) Then
                    Return clsMainCurrency.NoDecimalPlaces
                Else
                    Return 2
                End If
            End If
        Catch ex As Exception
            Return 2
        End Try
    End Function
    Private Function Get_NoOfDays() As Integer
        Try
            If ClsEmployeeClass.WorkingUnitsIsHours Then
                Return ClsEmployeeClass.NoOfDaysPerPeriod * ClsEmployeeClass.WorkHoursPerDay
            Else
                If ClsEmployeeClass.NoOfDaysPerPeriod > 0 Then
                    Return ClsEmployeeClass.NoOfDaysPerPeriod
                Else
                    Return FiscToDate.Day ' DateDiff(DateInterval.Day, FromDate, ToDate.AddDays(1))
                End If
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Function Get_FromToDate() As Boolean
        Try
            ClsFisicalPeriods = New Clssys_FiscalYearsPeriods(Page)
            clsCompanies = New Clssys_Companies(Page)
            clsBranch = New Clssys_Branches(Page)
            ClsFisicalPeriods.Find("ID = " & DdlPeriods.SelectedValue)
            clsCompanies.Find("ID=" & ClsFisicalPeriods.MainCompanyID)
            clsBranch.Find("ID=" & ddlBranche.SelectedValue)

            FromDate = ClsFisicalPeriods.FromDate
            ToDate = ClsFisicalPeriods.ToDate
            FiscFromDate = ClsFisicalPeriods.FromDate
            FiscToDate = ClsFisicalPeriods.ToDate

            If clsBranch.PrepareDay > 0 Then
                If clsCompanies.IsHigry = True Then
                    Dim strarr As String() = ClsFisicalPeriods.HFromDate.Split("/")
                    Dim FrmHDate As String = clsBranch.PrepareDay & "/" & IIf(strarr(1) = "01", "12", strarr(1) - 1) & "/" & IIf(strarr(1) = "01", strarr(2) - 1, strarr(2))
                    ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy")
                    FromDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy"), "dd/MM/yyyy")

                    Dim strarr1 As String() = FrmHDate.Split("/")
                    Dim ToHDate As String = clsBranch.PrepareDay - 1 & "/" & IIf(strarr1(1) = "12", "01", strarr1(1) + 1) & "/" & IIf(strarr1(1) = "12", strarr1(2) + 1, strarr1(2))
                    ToDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(ToHDate, "dd/MM/yyyy"), "dd/MM/yyyy")
                Else
                    FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsBranch.PrepareDay)
                    ToDate = FromDate.AddMonths(1).AddDays(-1)
                End If
                If clsBranch.AffectPeriod Then
                    FiscFromDate = FromDate
                    FiscToDate = ToDate
                End If
            Else
                If clsCompanies.PrepareDay > 0 Then
                    If clsCompanies.IsHigry = True Then
                        Dim strarr As String() = ClsFisicalPeriods.HFromDate.Split("/")
                        Dim FrmHDate As String = clsCompanies.PrepareDay & "/" & IIf(strarr(1) = "01", "12", strarr(1) - 1) & "/" & IIf(strarr(1) = "01", strarr(2) - 1, strarr(2))
                        ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy")
                        FromDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy"), "dd/MM/yyyy")

                        Dim strarr1 As String() = FrmHDate.Split("/")
                        Dim ToHDate As String = clsCompanies.PrepareDay - 1 & "/" & IIf(strarr1(1) = "12", "01", strarr1(1) + 1) & "/" & IIf(strarr1(1) = "12", strarr1(2) + 1, strarr1(2))
                        ToDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(ToHDate, "dd/MM/yyyy"), "dd/MM/yyyy")
                    Else
                        FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsCompanies.PrepareDay)
                        ToDate = FromDate.AddMonths(1).AddDays(-1)
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function Calculate_SalaryPerHour(ByRef Amount As Double, empID As Integer, ByRef TrnsDate As DateTime) As Boolean
        Try
            Dim clsCompanies As New Clssys_Companies(Me)
            ClsEmployeesContracts = New Clshrs_Contracts(Me.Page)
            clsCompanies.Find("ID=" & clsCompanies.MainCompanyID)
            If clsCompanies.SalaryCalculation = 0 Then            'Get Basic Salary
                dbBasicSalary = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployeeClass.ConnectionString, Data.CommandType.Text, "set dateformat dmy; select dbo.fn_GetBasicSalary(" & ClsEmployeesContracts.ContractValidatoinId(empID, TrnsDate) & ",'" & TrnsDate.ToString("dd/MM/yyyy") & "')")
                Amount = dbBasicSalary
            ElseIf clsCompanies.SalaryCalculation = 1 Then        'Get Total Salary By Days
                dbBasicSalary = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployeeClass.ConnectionString, Data.CommandType.Text, "set dateformat dmy; select dbo.fn_GetTotalAdditions(" & ClsEmployeesContracts.ContractValidatoinId(empID, TrnsDate) & ",'" & TrnsDate.ToString("dd/MM/yyyy") & "')")
                Amount = dbBasicSalary
            End If

            Dim ClsSolver As New Clshrs_FormulaSolver(clsCompanies.ConnectionString, Me.Page)
            ClsSolver.EmployeeID = empID
            ClsSolver.NoOfDaysPerPeriod = 30
            ClsSolver.NoOfWorkingDays = 30

            ClsSolver.EvaluateExpression(ClsEmployeeClass.OvertimeFormula, 0)
            dbOTSalary = ClsSolver.Output

            ClsSolver.EvaluateExpression(ClsEmployeeClass.HolidayFormula, 0)
            dbHOTSalary = ClsSolver.Output

            'absent value
            Dim ClsTransactionsTypes = New Clshrs_TransactionsTypes(Me.Page)

            ClsTransactionsTypes.Find("ID=" & ClsEmployeeClass.RegComputerID)
            If ClsTransactionsTypes.ID > 0 And ClsTransactionsTypes.Formula <> "" Then
                ClsSolver.NoOfDaysPerPeriod = ClsEmployeeClass.NoOfDaysPerPeriod
                ClsSolver.NoOfWorkingDays = ClsEmployeeClass.NoOfDaysPerPeriod
                ClsSolver.EvaluateExpression(ClsTransactionsTypes.Formula, 0)
                ObjAbsent = ClsSolver.Output
            End If





            Dim id As Integer = ClsEmployeeClass.ID

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SetScreenSetting(empID As Integer, ByRef Amount As Double) As Boolean
        Try
            ObjPrepaerdData = ClsEmployees.GetPreparedEmployessForSalariesByEmployeeID(IntFisicalPeriod, empID, FromDate, ToDate, FiscFromDate, FiscToDate)

            Calculate_SalaryPerHour(Amount, empID, ToDate)
            ObjSalaryPerDay = Math.Round(Amount / IntNoOfDays, intNoDecimalPlaces)
            ObjSalaryPerHour = Math.Round(ObjSalaryPerDay / ClsEmployeeClass.WorkHoursPerDay, intNoDecimalPlaces)
            ObjOverTime = Math.Round(ObjSalaryPerHour * ClsEmployeeClass.OvertimeFactor, intNoDecimalPlaces)

            If (ObjPrepaerdData.Count > 1) Then
                If ObjPrepaerdData(CPreparedData_calctype) = 1 Then
                    IntNoOfDays = ObjPrepaerdData(CPreparedData_AvaliableDays)
                ElseIf ObjPrepaerdData(CPreparedData_calctype) > 1 Then
                    IntNoOfDays = ObjPrepaerdData(CPreparedData_calctype)
                End If
            End If


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function Load_ClsLayers() As Boolean
        Try
            ClsEmployees = New Clshrs_Employees(Page)
            ClsProjects = New Clshrs_Projects(Page, " hrs_Projects ")
            ClsEmployeesContracts = New Clshrs_Contracts(Page)
            ClsVacationsTypes = New Clshrs_VacationsTypes(Page)
            ClsEmployeeClass = New Clshrs_EmployeeClasses(Page)
            ClsEmployeesTransactions = New Clshrs_EmployeesTransactions(Page)
            ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsFisicalPeriods.ConnectionString)
            ClsEmployeesTransactionsProjects = New Clshrs_EmployeesTransactionsProjects(Page)
            clsTransType = New Clshrs_TransactionsTypes(Page)

            ClsAttendancePreparationDetails = New ClsAtt_AttendancePreparationDetails(Page)

            IntFisicalPeriod = ClsFisicalPeriods.ID
            intNoDecimalPlaces = Get_NoDecimalPlaces()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function Get_Attendance(ByVal EmpID As Integer) As Boolean
        Try
            Dim clsEmpClass As New Clshrs_EmployeeClasses(Page)
            Dim intAbsent As Integer = 0
            Dim Total_OT As Double = 0
            Dim Total_HD As Double = 0
            Dim Total_Absent As Double = 0
            ObjTotalLate = 0
            NotPermitLat = 0

            ClsAttendancePreparationDetails = New ClsAtt_AttendancePreparationDetails(Page)
            ClsAttendancePreparationDetails.Find("EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)")
            Dim DT As Data.DataTable = ClsAttendancePreparationDetails.DataSet.Tables(0)
            Dim cnt As Integer = 0
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To IntNoOfDays
                    Try
                        ClsAttendancePreparationDetails.Find("ID = " & DT.Rows(i)("ID"))
                        If ClsAttendancePreparationDetails.IsAbsent = True And ClsAttendancePreparationDetails.IsVacation = False And ClsAttendancePreparationDetails.LeavingType = 0 Then
                            intAbsent = intAbsent + 1
                        ElseIf ClsAttendancePreparationDetails.LeavingType <> 0 Then
                            If ClsVacationsTypes.Find("ID = " & ClsAttendancePreparationDetails.LeavingType) Then
                                If ClsVacationsTypes.IsPaid = -1 Then
                                    If ClsVacationsTypes.IsAnnual <> True Then
                                        intAbsent = intAbsent + 1
                                    End If
                                End If
                            End If
                        End If
                        cnt = cnt + 1
                    Catch ex As Exception
                    End Try
                Next
            End If
            NotPermitLat = Math.Round(NotPermitLat, intNoDecimalPlaces)
            dbTotalAbsent = Math.Round(Total_Absent, intNoDecimalPlaces)
            dbOvertimeSalary = Math.Round(Total_OT, intNoDecimalPlaces)
            dbHolidayHoursSalary = Math.Round(Total_HD, intNoDecimalPlaces)
            If IntNoOfDays = 28 Or IntNoOfDays = 29 Then
                IntNoOfDays = 30
            End If
            If IntNoOfDays = 31 Then
                IntNoOfDays = 30
            End If

            If cnt = intAbsent Then
                IntNoOfWorkDays = 0
            Else
                IntNoOfWorkDays = Convert.ToInt32(IntNoOfDays) - IIf(intAbsent > Convert.ToInt32(IntNoOfDays), Convert.ToInt32(IntNoOfDays), intAbsent)
            End If

            Dim CntDays As Integer = FiscToDate.Subtract(FiscFromDate).Days + 2
            Dim VacDays As Integer = 0
            Dim IsToEnd As Boolean = False
            For CounDays As Integer = 0 To CntDays
                Dim OperDate As DateTime = FiscFromDate.AddDays(CounDays)
                Dim hrsEmployeesVacations As New Clshrs_EmployeesVacations(Me)
                hrsEmployeesVacations.Find("EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103) >= CONVERT(date,ActualStartDate,103) and CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103) < CONVERT(date,isnull(ActualEndDate,'01/01/2050'),103) and VacationTypeID in (select ID from hrs_VacationsTypes where IsAnnual = 1)")
                If (hrsEmployeesVacations.DataSet.Tables(0).Rows.Count > 0) Then
                    If OperDate > FiscToDate Then
                        IsToEnd = True
                        Continue For
                    Else
                        VacDays = VacDays + 1
                    End If
                End If
            Next
            If IsToEnd Then
                If FiscToDate.Subtract(FiscFromDate).Days <> ToDate.Subtract(FromDate).Days Then
                    If FiscToDate.Subtract(FiscFromDate).Days > ToDate.Subtract(FromDate).Days Then
                        VacDays = VacDays - 1
                    ElseIf FiscToDate.Subtract(FiscFromDate).Days < ToDate.Subtract(FromDate).Days Then
                        If ClsEmployeeClass.NoOfDaysPerPeriod > 0 Then
                        Else
                            VacDays = VacDays + 1
                        End If
                    End If
                Else
                    If ClsEmployeeClass.NoOfDaysPerPeriod > 0 And FiscToDate.Subtract(FiscFromDate).Days + 1 > ClsEmployeeClass.NoOfDaysPerPeriod Then
                        VacDays = VacDays - 1
                    End If
                End If
            End If

            If ClsEmployees.JoinDate > ToDate And ClsEmployees.JoinDate <= FiscToDate And IntNoOfWorkDays = 0 Then
                IntNoOfWorkDays = IIf(FiscToDate.Day = 31, FiscToDate.AddDays(-1).Subtract(ClsEmployees.JoinDate).Days + 1, FiscToDate.Subtract(ClsEmployees.JoinDate).Days + 1)
            End If



            IntNoOfWorkDays = IntNoOfWorkDays - IIf(VacDays < 0, 0, VacDays)
            Dim dsAttandance As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, "set dateformat dmy  select * from dbo.AttendanceEffects(" & ClsEmployees.ID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & ToDate.ToString("dd/MM/yyy") & "'," & IntFisicalPeriod & ",0,NULL,0 ,'" & FiscFromDate.ToString("dd/MM/yyy") & "','" & FiscToDate.ToString("dd/MM/yyy") & "' )")
            ' Dim dsAttandance As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsEmpClass.ConnectionString, CommandType.Text, "set dateformat dmy  select * from dbo.AttendanceEffects(" & ClsEmployees.ID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & ToDate.ToString("dd/MM/yyy") & "'," & IntFisicalPeriod & ",0 )")
            If dsAttandance.Tables(0).Rows.Count > 0 Then
                IntNoOfWorkDays = dsAttandance.Tables(0).Rows(0)("WorkingDays")
                intAbsent = dsAttandance.Tables(0).Rows(0)("AbsentDays")
                VacDays = dsAttandance.Tables(0).Rows(0)("vactiondays")
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function Clear() As Boolean
        Try
            dbOvertimeSalary = 0
            dbHolidayHoursSalary = 0
            dblBenefits = 0
            dblDeduct = 0
            dbBasicSalary = 0

            IntNoOfDays = 0
            IntNoOfWorkDays = 0
            IntFisicalPeriod = 0
            intNoDecimalPlaces = 0
            intEmployeeContarctID = 0
            ObjSalaryPerDay = 0
            ObjSalaryPerHour = 0
            ObjOverTime = 0
            ObjAbsent = 0
            totalBenefits = 0
            totalDeducation = 0
            NotPermitLat = 0
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SetData_Salary(Optional ByVal IsSave As Boolean = True) As Boolean
        Try
            Dim clsProject As New Clshrs_Projects(Page, "hrs_Projects")
            If Not Load_ClsLayers() Then Return False

            For Each ObjRow In UwgSearchEmployees.Rows
                Try
                    If ObjRow.Cells(1).Value Then
                        Dim cmdString As String = ""
                        Dim EmployeeID As Integer
                        EmployeeID = ObjRow.Cells.FromKey("ID").Value
                        If EmployeeID = 0 Then Continue For
                        ClsEmployees = New Clshrs_Employees(Page)
                        ClsEmployees.Find("ID=" & EmployeeID)

                        If Not CheckEmployee(ClsEmployees.ID, IntFisicalPeriod) Then Continue For
                        Dim Amount As Double = 0
                        If Not SetScreenSetting(ClsEmployees.ID, Amount) Then Continue For
                        If IsSave And Refund(ClsEmployees.ID) Then

                            Dim AttBol = Get_Attendance(EmployeeID)
                            '===============================================================
                            ' For employees returns from vaction in the same period
                            Dim clsEmpTransaction As New Clshrs_EmployeesTransactions(Page)
                            Dim clsEmpVaction As New Clshrs_EmployeesVacations(Page)
                            Dim clsFiscalperio As New Clssys_FiscalYearsPeriods(Page)
                            clsEmpTransaction.Find("(prepareType='NV' or prepareType='NP')  and EmployeeID=" & EmployeeID & " and FiscalYearPeriodID=" & IntFisicalPeriod)
                            If clsEmpTransaction.ID > 0 Then
                                clsFiscalperio.Find("ID=" & IntFisicalPeriod)
                                clsEmpVaction.Find("Employeeid=" & EmployeeID & "  and CONVERT(datetime ,ActualEndDate,103 )>CONVERT(datetime ,'" & ClsFisicalPeriods.FromDate & "',103 ) and CONVERT(datetime ,ActualEndDate,103 )<=CONVERT(datetime ,'" & ClsFisicalPeriods.ToDate & "',103 )")
                                If clsEmpVaction.ID > 0 Then
                                    Dim WorkingDays As Integer = clsFiscalperio.ToDate.Subtract(clsEmpVaction.ActualEndDate).Days + 1
                                    AttBol = True
                                    IntNoOfWorkDays = IntNoOfWorkDays - clsEmpTransaction.FinancialWorkingUnits
                                Else
                                    Continue For


                                End If


                            End If

                            '==============================================================
                            If Not AttBol Then Continue For
                            If IntNoOfWorkDays = 0 Then Continue For
                            ClsEmployees.CollectEmployeesTransactions("T", ClsFisicalPeriods.ToDate, ClsEmployees.ID, ClsFisicalPeriods.ID, DtBenefits, DtDeductions, dblBenefits, dblDeduct,
                                                                      IntNoOfWorkDays, IntNoOfWorkDays, IntNoOfWorkDays, dbOvertimeSalary, dbHolidayHoursSalary, ObjSalaryPerHour, ObjSalaryPerDay, NotPermitLat, dbTotalAbsent, ObjPrepaerdData(CPreparedData_FirstPrepare), Clshrs_EmployeesBase.ePrepareStage.Normal, IntNoOfDays)




                            Dim clshrsemployeesvaction As New Clshrs_EmployeesVacations(Me.Page)
                            Dim returnvation As Integer = 0
                            Dim strupdate As String = ""
                            If returnvation > 0 Then
                                Dim ClsEmployeesPayability As New Clshrs_EmployeesPayability(Me.Page)
                                Dim ObjDs As New DataSet
                                'ClsEmployeesPayability.GetEmployeesPayabilities(EmployeeID, ClsFisicalPeriods.ID, ObjDs, "N")
                                'If ObjDs.Tables(0).Rows.Count > 0 Then
                                '    For Each row In ObjDs.Tables(0).DefaultView.ToTable(True, "EmployeesPayabilitiesID").Rows
                                '        strupdate &= "set dateformat dmy declare @months  as int =datediff(month,(select min(duedate) from hrs_EmployeesPayabilitiesSchedules where EmployeePayabilityID=" & Convert.ToInt32(row("EmployeesPayabilitiesID")) & " and duedate <= Convert(Date," & ClsFisicalPeriods.ToDate & ",103)),Convert(Date," & ClsFisicalPeriods.ToDate & ",103))"
                                '        strupdate &= " update ps set ps.DueDate=DATEADD(month,@months,DueDate) from hrs_EmployeesPayabilitiesSchedules ps where EmployeePayabilityID in(" & Convert.ToInt32(row("EmployeesPayabilitiesID")) & ") and convert(date,ps.DueDate,103) <=convert(date," & ClsFisicalPeriods.ToDate & ",103) and (dueAmount - IsNull((Select Sum(Amount) From hrs_EmployeesPayabilitiesSchedulesSettlement  where  ps.ID = hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID),0)) > 0;"
                                '    Next

                                'End If

                                For index = 1 To 100
                                    ClsEmployeesPayability.GetEmployeesPayabilities(EmployeeID, ClsFisicalPeriods.ID, ObjDs, "N")
                                    If ObjDs.Tables(0).Rows.Count > 0 Then
                                        strupdate = "update ps set ps.DueDate =DATEADD(month,1,DueDate)from hrs_EmployeesPayabilitiesSchedules ps where EmployeePayabilityID in(" & Convert.ToInt32(ObjDs.Tables(0).Rows(0)("EmployeesPayabilitiesID")) & ") and (dueAmount - IsNull((Select Sum(Amount) From hrs_EmployeesPayabilitiesSchedulesSettlement  where  ps.ID = hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID),0)) > 0;"
                                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeesPayability.ConnectionString, CommandType.Text, strupdate)
                                    Else
                                        Exit For

                                    End If
                                Next


                            Else
                                ClsEmployees.CollectEmployeesPayablities(ClsEmployees.ID, ClsFisicalPeriods.ID, DtBenefits, DtDeductions, dblBenefits, dblDeduct, "N")
                                If ClsEmployees.MaxLoanDedution > 0 Then
                                    DtDeductions = maxloandeduction(DtDeductions, ClsEmployees.MaxLoanDedution)
                                End If

                            End If








                            ''Add Extra Finance Items
                            Dim Extrastrcommand As String = "select (isnull((select top 1 ID from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode),0)) as RelTransactionID,(isnull((select top 1 Sign from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode),0)) as Sign,Amount from hrs_EmployeeExtraItems"
                            Dim strFilter As String = " where EmployeeCode = '" & ClsEmployees.Code & "' and Status = 1 and isnull(LinkedCS,'') = '' and FiscalPeriodID =" & ClsFisicalPeriods.ID
                            Dim dsEmployee As New Data.DataSet
                            dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, Data.CommandType.Text, Extrastrcommand & strFilter)
                            For EXB As Integer = 0 To dsEmployee.Tables(0).Rows.Count - 1
                                If dsEmployee.Tables(0).Rows(EXB)(1) > 0 Then
                                    DtBenefits.Rows.Add(New Object() {dsEmployee.Tables(0).Rows(EXB)(0), dsEmployee.Tables(0).Rows(EXB)(2), "Paid", Nothing, "Paid"})
                                ElseIf dsEmployee.Tables(0).Rows(EXB)(1) < 0 Then
                                    DtDeductions.Rows.Add(New Object() {dsEmployee.Tables(0).Rows(EXB)(0), dsEmployee.Tables(0).Rows(EXB)(2), "Paid", Nothing, "Paid"})
                                End If
                            Next
                            ''End Add Extra Finance Items


                            ''Add Medical Commission 
                            Try

                                Extrastrcommand = "select tt.id RelTransactionID,Ct.employeeid,ct.dueamount  Amount from hrs_HmsCommissionTransfer Ct inner join hrs_HmsCommissionCategories CC on cc.id=Ct.CommissionCatageryid inner join hrs_TransactionsTypes tt on tt.id=cc.salarytransactionID"
                                strFilter = " where employeeid = " & ClsEmployees.ID & " and fiscalperidid =" & ClsFisicalPeriods.ID
                                'strFilter = " where EmployeeCode = '" & ClsEmployees.Code & "' and Status = 1 and isnull(LinkedCS,'') = '' and FiscalPeriodID =" & ClsFisicalPeriods.ID
                                dsEmployee = New Data.DataSet
                                dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, Data.CommandType.Text, Extrastrcommand & strFilter)
                                For EXB As Integer = 0 To dsEmployee.Tables(0).Rows.Count - 1

                                    DtBenefits.Rows.Add(New Object() {dsEmployee.Tables(0).Rows(EXB)(0), dsEmployee.Tables(0).Rows(EXB)(2), "Paid", Nothing, "Paid"})


                                Next
                            Catch ex As Exception

                            End Try

                            ''End Add Medical Commission 





                            Dim totalBenefitssum As Double = 0
                            Dim totalDeducationsum As Double = 0
                            For Each row As System.Data.DataRow In DtBenefits.Rows
                                If IsTransPaid(row("TransactionTypeID")) = "1" Then
                                    totalBenefitssum += IIf(IsNothing(row("Amount")), 0, row("Amount"))
                                End If
                            Next
                            For Each row As System.Data.DataRow In DtDeductions.Rows
                                If IsTransPaid(row("TransactionTypeID")) = "1" Then
                                    totalDeducationsum += IIf(IsNothing(row("Amount")), 0, row("Amount"))
                                End If
                            Next

                            Dim Str As String = "select  ProjectID,isnull(LinkedCS,'') LinkedCS,COUNT(ID) PTotal,(select COUNT(ID) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where LeavingType not in (select ID from hrs_VacationsTypes where IsAnnual = 1) and EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))) ATotal from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where LeavingType not in (select ID from hrs_VacationsTypes where IsAnnual = 1) and EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) group by ProjectID,isnull(LinkedCS,'')"
                            Dim DTProjects As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, Str).Tables(0)
                            Dim AllWorkingUnits As Double = 0
                            If ClsEmployees.JoinDate > ToDate And ClsEmployees.JoinDate <= FiscToDate And DTProjects.Rows.Count = 0 Then
                                IntNoOfWorkDays = FiscToDate.Subtract(ClsEmployees.JoinDate).Days + 1
                                If clsProject.Find("ID=" & ClsEmployeeClass.DefaultProjectID) Then
                                    cmdString &= "insert into hrs_EmployeesTransactions(EmployeeID,FinancialWorkingUnits,FiscalYearPeriodID,PrepareType,Applyed,CBranchID,CDepartmetnID,CSectorID,CCost1,CCost2,CCost3,CCost4,CMainProjectID)Values(" & EmployeeID & "," & IntNoOfWorkDays & "," & IntFisicalPeriod & ",'N',0," & ClsEmployees.BranchID & "," & ClsEmployees.DepartmentID & "," & ClsEmployees.SectorID & "," & ClsEmployees.Cost1 & "," & ClsEmployees.Cost2 & "," & ClsEmployees.Cost3 & "," & ClsEmployees.Cost4 & "," & ClsEmployeeClass.DefaultProjectID & ");" & vbNewLine
                                    cmdString &= "Set @TransID = (Select IDENT_CURRENT('hrs_EmployeesTransactions'));" & vbNewLine
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsProjects([EmployeeTransactionID],[ProjectID],[WorkingUnits],[RegUserID]) " &
                                                 " Select @TransID," & clsProject.ID & "," & IntNoOfWorkDays & "," & ClsEmployees.DataBaseUserRelatedID & ";" & "  Set @ProjectTransID = (Select IDENT_CURRENT('hrs_EmployeesTransactionsProjects')); "

                                    For Each BenfitRow As DataRow In DtBenefits.Rows
                                        cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                               " Values (@ProjectTransID," & BenfitRow("TransactionTypeID") & "," & BenfitRow("Amount") & ", '" & BenfitRow("DescriptionSign") & "'," & IIf(IsDBNull(BenfitRow("EmpSchID")), "Null", BenfitRow("EmpSchID")) & "); "
                                        If BenfitRow(3).ToString() <> "" Then
                                            cmdString &= " Insert Into hrs_EmployeesPayabilitiesSchedulesSettlement (EmployeePayabilityScheduleID,EmployeeTransactionID,Amount,date)" &
                                                                                                                " Values (" & BenfitRow(3) & ",@ProjectTransID," & BenfitRow("Amount") & ",convert(date,'" & FiscToDate.ToString("dd/MM/yyyy") & "',103)); "
                                        End If
                                    Next

                                    For Each DeducationRow As DataRow In DtDeductions.Rows
                                        cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                     " Values (@ProjectTransID," & DeducationRow("TransactionTypeID") & "," & DeducationRow("Amount") & ", '" & DeducationRow("DescriptionSign") & "'," & IIf(IsDBNull(DeducationRow("EmpSchID")), "Null", DeducationRow("EmpSchID")) & "); "
                                        If DeducationRow(3).ToString() <> "" Then
                                            cmdString &= " Insert Into hrs_EmployeesPayabilitiesSchedulesSettlement (EmployeePayabilityScheduleID,EmployeeTransactionID,Amount,date)" &
                                                                                                                " Values (" & DeducationRow(3) & ",@ProjectTransID," & DeducationRow("Amount") & ",convert(date,'" & FiscToDate.ToString("dd/MM/yyyy") & "',103)); "
                                        End If
                                    Next
                                End If
                            ElseIf IntNoOfWorkDays > 0 And DTProjects.Rows.Count = 0 Then
                                IntNoOfWorkDays = IntNoOfWorkDays
                                If clsProject.Find("ID=" & ClsEmployeeClass.DefaultProjectID) Then
                                    cmdString &= "insert into hrs_EmployeesTransactions(EmployeeID,FinancialWorkingUnits,FiscalYearPeriodID,PrepareType,Applyed,CBranchID,CDepartmetnID,CSectorID,CCost1,CCost2,CCost3,CCost4,CMainProjectID)Values(" & EmployeeID & "," & IntNoOfWorkDays & "," & IntFisicalPeriod & ",'N',0," & ClsEmployees.BranchID & "," & ClsEmployees.DepartmentID & "," & ClsEmployees.SectorID & "," & ClsEmployees.Cost1 & "," & ClsEmployees.Cost2 & "," & ClsEmployees.Cost3 & "," & ClsEmployees.Cost4 & "," & ClsEmployeeClass.DefaultProjectID & ");" & vbNewLine
                                    cmdString &= "Set @TransID = (Select IDENT_CURRENT('hrs_EmployeesTransactions'));" & vbNewLine
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsProjects([EmployeeTransactionID],[ProjectID],[WorkingUnits],[RegUserID]) " &
                                                 " Select @TransID," & clsProject.ID & "," & IntNoOfWorkDays & "," & ClsEmployees.DataBaseUserRelatedID & ";" & "  Set @ProjectTransID = (Select IDENT_CURRENT('hrs_EmployeesTransactionsProjects')); "

                                    For Each BenfitRow As DataRow In DtBenefits.Rows
                                        cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                               " Values (@ProjectTransID," & BenfitRow("TransactionTypeID") & "," & BenfitRow("Amount") & ", '" & BenfitRow("DescriptionSign") & "'," & IIf(IsDBNull(BenfitRow("EmpSchID")), "Null", BenfitRow("EmpSchID")) & "); "
                                        If BenfitRow(3).ToString() <> "" Then
                                            cmdString &= " Insert Into hrs_EmployeesPayabilitiesSchedulesSettlement (EmployeePayabilityScheduleID,EmployeeTransactionID,Amount,date)" &
                                                                                                                " Values (" & BenfitRow(3) & ",@ProjectTransID," & BenfitRow("Amount") & ",convert(date,'" & FiscToDate.ToString("dd/MM/yyyy") & "',103)); "
                                        End If
                                    Next

                                    For Each DeducationRow As DataRow In DtDeductions.Rows
                                        cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                     " Values (@ProjectTransID," & DeducationRow("TransactionTypeID") & "," & DeducationRow("Amount") & ", '" & DeducationRow("DescriptionSign") & "'," & IIf(IsDBNull(DeducationRow("EmpSchID")), "Null", DeducationRow("EmpSchID")) & "); "
                                        If DeducationRow(3).ToString() <> "" Then
                                            cmdString &= " Insert Into hrs_EmployeesPayabilitiesSchedulesSettlement (EmployeePayabilityScheduleID,EmployeeTransactionID,Amount,date)" &
                                                                                                                " Values (" & DeducationRow(3) & ",@ProjectTransID," & DeducationRow("Amount") & ",convert(date,'" & FiscToDate.ToString("dd/MM/yyyy") & "',103)); "
                                        End If
                                    Next
                                End If
                            End If
                            If DTProjects.Rows.Count > 0 Then
                                cmdString &= "insert into hrs_EmployeesTransactions(EmployeeID,FinancialWorkingUnits,FiscalYearPeriodID,PrepareType,Applyed,CBranchID,CDepartmetnID,CSectorID,CCost1,CCost2,CCost3,CCost4,CMainProjectID)Values(" & EmployeeID & "," & IntNoOfWorkDays & "," & IntFisicalPeriod & ",'N',0," & ClsEmployees.BranchID & "," & ClsEmployees.DepartmentID & "," & ClsEmployees.SectorID & "," & ClsEmployees.Cost1 & "," & ClsEmployees.Cost2 & "," & ClsEmployees.Cost3 & "," & ClsEmployees.Cost4 & "," & ClsEmployeeClass.DefaultProjectID & ");" & vbNewLine
                                cmdString &= "Set @TransID = (Select IDENT_CURRENT('hrs_EmployeesTransactions'));" & vbNewLine
                            End If
                            For i As Integer = 0 To DTProjects.Rows.Count - 1
                                If clsProject.Find("ID=" & DTProjects.Rows(i)("ProjectID").ToString()) Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsProjects([EmployeeTransactionID],[ProjectID],[WorkingUnits],[RegUserID],[LinkedCS]) " &
                                                 " Select @TransID," & clsProject.ID & "," & (IntNoOfWorkDays * DTProjects.Rows(i)("PTotal") / DTProjects.Rows(i)("ATotal")) & "," & ClsEmployees.DataBaseUserRelatedID & ",'" & DTProjects.Rows(i)("LinkedCS") & "';" & "  Set @ProjectTransID = (Select IDENT_CURRENT('hrs_EmployeesTransactionsProjects')); "
                                    For Each BenfitRow As DataRow In DtBenefits.Rows
                                        cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                               " Values (@ProjectTransID," & BenfitRow("TransactionTypeID") & "," & (BenfitRow("Amount") * DTProjects.Rows(i)("PTotal") / DTProjects.Rows(i)("ATotal")) & ", '" & BenfitRow("DescriptionSign") & "'," & IIf(IsDBNull(BenfitRow("EmpSchID")), "Null", BenfitRow("EmpSchID")) & "); "
                                        If BenfitRow(3).ToString() <> "" Then
                                            cmdString &= " Insert Into hrs_EmployeesPayabilitiesSchedulesSettlement (EmployeePayabilityScheduleID,EmployeeTransactionID,Amount,date)" &
                                                                                                                " Values (" & BenfitRow(3) & ",@ProjectTransID," & (BenfitRow("Amount") * DTProjects.Rows(i)("PTotal") / DTProjects.Rows(i)("ATotal")) & ",convert(date,'" & FiscToDate.ToString("dd/MM/yyyy") & "',103)); "
                                        End If
                                    Next
                                    For Each DeducationRow As DataRow In DtDeductions.Rows
                                        cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                     " Values (@ProjectTransID," & DeducationRow("TransactionTypeID") & "," & (DeducationRow("Amount") * DTProjects.Rows(i)("PTotal") / DTProjects.Rows(i)("ATotal")) & ", '" & DeducationRow("DescriptionSign") & "'," & IIf(IsDBNull(DeducationRow("EmpSchID")), "Null", DeducationRow("EmpSchID")) & "); "
                                        If DeducationRow(3).ToString() <> "" Then
                                            cmdString &= " Insert Into hrs_EmployeesPayabilitiesSchedulesSettlement (EmployeePayabilityScheduleID,EmployeeTransactionID,Amount,date)" &
                                                                                                                " Values (" & DeducationRow(3) & ",@ProjectTransID," & (DeducationRow("Amount") * DTProjects.Rows(i)("PTotal") / DTProjects.Rows(i)("ATotal")) & ",convert(date,'" & FiscToDate.ToString("dd/MM/yyyy") & "',103)); "
                                        End If
                                    Next

                                    'Edited By Hassan Kurdi
                                    'Date: 2021-02-17
                                    'Add if condition to calculate late hours based on PunishementCalc from employee classes

                                    Dim ClsContract As New Clshrs_Contracts(Page)
                                    Dim ClsClasses As New Clshrs_EmployeeClasses(Page)

                                    ClsContract.Find("EmployeeID = " & ClsEmployees.ID)
                                    ClsClasses.Find("ID = " & ClsContract.EmployeeClassID)

                                    Dim latHrs As Decimal = 0
                                    If ClsEmployeeClass.NonPermiLatTransaction > 0 Then
                                        'Dim strcommand As String = "Select isnull(sum(A.LatPunishment),0) from Att_AttendancePreparationDetails A inner join Att_AttendancePreparationProjects B On A.ID = B.TrnsID where A.EmployeeID = " & ClsEmployees.ID & " And CONVERT(Date,A.GAttendDate,103) >= CONVERT(Date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,A.GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103) and isnull(B.LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and B.ProjectID = " & clsProject.ID
                                        Dim strcommand As String = "Select isnull(sum(A.LatPunishment),0) from Att_AttendancePreparationDetails A  where A.EmployeeID = " & ClsEmployees.ID & " And CONVERT(Date,A.GAttendDate,103) >= CONVERT(Date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,A.GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103) "
                                        Dim latvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)

                                        strcommand = "Select isnull(sum(A.TotalLate),0) from Att_AttendancePreparationDetails A  where A.EmployeeID = " & ClsEmployees.ID & " And CONVERT(Date,A.GAttendDate,103) >= CONVERT(Date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,A.GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103) AND A.NotpermitLate > 0"
                                        Dim TotalLate As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)

                                        'strcommand = "select isnull(sum(A.NotpermitLate),0) from Att_AttendancePreparationDetails A inner join Att_AttendancePreparationProjects B on A.ID = B.TrnsID where A.EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,A.GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,A.GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103) and isnull(B.LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and B.ProjectID = " & clsProject.ID
                                        strcommand = "select isnull(sum(A.NotpermitLate),0) from Att_AttendancePreparationDetails A inner join Att_AttendancePreparationProjects B on A.ID = B.TrnsID where A.EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,A.GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,A.GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103) "

                                        If (ClsClasses.PunishementCalc = 1) Then
                                            latHrs = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                        Else
                                            latHrs = TotalLate
                                        End If

                                        'End of edit

                                        If i = 0 Then
                                            If latvalue > 0 Then
                                                cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                             " Values (@ProjectTransID," & ClsEmployeeClass.NonPermiLatTransaction & "," & latvalue & ", 'Paid',Null); "
                                                totalDeducationsum += latvalue
                                            End If
                                        End If

                                    End If
                                    Dim OTHrs As Decimal = 0
                                    If ClsEmployeeClass.OvertimeTransaction > 0 Then
                                        Dim strcommand As String = "select isnull(sum(Overtime * OTSalary),0) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                        Dim OTvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                        strcommand = "select isnull(sum(Overtime),0) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                        OTHrs = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                        If OTvalue > 0 Then
                                            cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                         " Values (@ProjectTransID," & ClsEmployeeClass.OvertimeTransaction & "," & OTvalue & ", 'Paid',Null); "
                                            totalBenefitssum += OTvalue
                                        End If
                                    End If
                                    Dim HOTHrs As Decimal = 0
                                    If ClsEmployeeClass.HOvertimeTransaction > 0 Then
                                        Dim strcommand As String = "select isnull(sum(HolidayHours * HOTSalary),0) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                        Dim HOTvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                        strcommand = "select isnull(sum(HolidayHours),0) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                        HOTHrs = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                        If HOTvalue > 0 Then
                                            cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                         " Values (@ProjectTransID," & ClsEmployeeClass.HOvertimeTransaction & "," & HOTvalue & ", 'Paid',Null); "
                                            totalBenefitssum += HOTvalue
                                        End If
                                    End If
                                    Dim AbsDys As Decimal = 0
                                    If ClsEmployeeClass.RegComputerID > 0 Then
                                        Dim strcommand As String = "select isnull(sum(SalaryPerDay),0) from Att_AttendancePreparationProjects where isnull(IsAbsent,0) = 1  and TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                        Dim Absvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                        strcommand = "select isnull(count(ID),0) from Att_AttendancePreparationProjects where isnull(IsAbsent,0) = 1  and TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                        AbsDys = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                        'absent formaula

                                        clsTransType.Find("ID= " & ClsEmployeeClass.RegComputerID)
                                        If clsTransType.ID > 0 And clsTransType.Formula <> "" Then

                                            Dim ClsSolver = New Clshrs_FormulaSolver(clsTransType.ConnectionString, Page)
                                            ClsSolver.EmployeeID = ClsEmployees.ID
                                            ClsSolver.FiscalPeriodID = ClsFisicalPeriods.ID
                                            ClsSolver.NoOfDaysPerPeriod = ClsEmployeeClass.NoOfDaysPerPeriod
                                            ClsSolver.Executedate = ToDate
                                            ClsSolver.EvaluateExpression(clsTransType.Formula)
                                            ObjAbsent = IIf(IsNumeric(ClsSolver.Output), ClsSolver.Output, 0)

                                        End If

                                        If ObjAbsent > 0 Then
                                            Absvalue = ObjAbsent * AbsDys
                                        End If

                                        If Absvalue > 0 Then
                                            cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                         " Values (@ProjectTransID," & ClsEmployeeClass.RegComputerID & "," & Absvalue & ",'" & IIf(clsTransType.IsPaid, "Piaid", "Not Paid") & "',Null); "

                                            If clsTransType.IsPaid = True Then
                                                totalDeducationsum += Absvalue
                                            End If

                                        End If


                                    End If
                                    cmdString &= "update hrs_EmployeesTransactionsProjects set OvertimeHours = " & OTHrs & ",HoliDayOvertimeHours = " & HOTHrs & ",AbsentDays = " & AbsDys & ",LatHours = " & latHrs & " where ID = @ProjectTransID;"

                                    Extrastrcommand = "select (isnull((select top 1 ID from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode),0)) as RelTransactionID,(isnull((select top 1 Sign from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode),0)) as Sign,Amount from hrs_EmployeeExtraItems"
                                    strFilter = " where EmployeeCode = '" & ClsEmployees.Code & "' and Status = 1 and LTRIM(RTRIM(ProjectID)) = '" & clsProject.ID & "' and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and FiscalPeriodID =" & ClsFisicalPeriods.ID
                                    dsEmployee = New Data.DataSet
                                    dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, Data.CommandType.Text, Extrastrcommand & strFilter)
                                    For EXB As Integer = 0 To dsEmployee.Tables(0).Rows.Count - 1
                                        cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                     " Values (@ProjectTransID," & dsEmployee.Tables(0).Rows(EXB)(0) & "," & dsEmployee.Tables(0).Rows(EXB)(2) & ", 'Paid',Null); "
                                        Dim clsTransactionsTypes As New Clshrs_TransactionsTypes(Me)
                                        If clsTransactionsTypes.Find("ID=" & dsEmployee.Tables(0).Rows(EXB)(0)) Then
                                            If clsTransactionsTypes.Sign = 1 Then
                                                totalBenefitssum += dsEmployee.Tables(0).Rows(EXB)(2)
                                            Else
                                                totalDeducationsum += dsEmployee.Tables(0).Rows(EXB)(2)
                                            End If
                                        End If
                                    Next

                                    If clsProject.LateTransaction > 0 Then
                                        Dim strcommand As String = "select sum(LatPunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                        Dim PLatvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                        If PLatvalue > 0 Then
                                            cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                         " Values (@ProjectTransID," & clsProject.LateTransaction & "," & PLatvalue & ", 'Paid',Null); "
                                            totalDeducationsum += PLatvalue
                                        End If
                                    End If
                                    If clsProject.AbsentTransaction > 0 Then
                                        Dim strcommand As String = "select sum(AbsentPunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                        Dim PAbsvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                        If PAbsvalue > 0 Then
                                            cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                         " Values (@ProjectTransID," & clsProject.AbsentTransaction & "," & PAbsvalue & ", 'Paid',Null); "
                                            totalDeducationsum += PAbsvalue
                                        End If
                                    End If
                                    If clsProject.SickTransaction > 0 Then
                                        Dim strcommand As String = "select sum(SickPunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                        Dim Psikvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                        If Psikvalue > 0 Then
                                            cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                         " Values (@ProjectTransID," & clsProject.SickTransaction & "," & Psikvalue & ", 'Paid',Null); "
                                            totalDeducationsum += Psikvalue
                                        End If
                                    End If
                                    If clsProject.LeaveTransaction > 0 Then
                                        Dim strcommand As String = "select sum(LeavePunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                        Dim Pleavvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                        If Pleavvalue > 0 Then
                                            cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                         " Values (@ProjectTransID," & clsProject.LeaveTransaction & "," & Pleavvalue & ", 'Paid',Null); "
                                            totalDeducationsum += Pleavvalue
                                        End If
                                    End If
                                    If clsProject.OTTransaction > 0 Then
                                        Dim strcommand As String = "select sum(OTFactor * Overtime * SalaryPerHour) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                        Dim Potvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                        If Potvalue > 0 Then
                                            cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                         " Values (@ProjectTransID," & clsProject.OTTransaction & "," & Potvalue & ", 'Paid',Null); "
                                            totalBenefitssum += Potvalue
                                        End If
                                    End If
                                    If clsProject.HOTTransaction > 0 Then
                                        Dim strcommand As String = "select sum(HOTFactor * HolidayHours * SalaryPerHour) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                        Dim Photvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                        If Photvalue > 0 Then
                                            cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                                                         " Values (@ProjectTransID," & clsProject.HOTTransaction & "," & Photvalue & ", 'Paid',Null); "
                                            totalBenefitssum += Photvalue
                                        End If
                                    End If
                                End If
                            Next

                            If totalBenefitssum.ToString.Split(".").Length = 2 Then
                                If totalBenefitssum.ToString.Split(".")(1) = 5 Then
                                    totalBenefitssum = CDbl(totalBenefitssum.ToString.Split(".")(0) & ".6")
                                End If
                            End If
                            If totalDeducationsum.ToString.Split(".").Length = 2 Then
                                If totalDeducationsum.ToString.Split(".")(1) = 5 Then
                                    totalDeducationsum = CDbl(totalDeducationsum.ToString.Split(".")(0) & ".6")
                                End If
                            End If
                            Dim NetSalary As Double = Math.Round(totalBenefitssum - totalDeducationsum, intNoDecimalPlaces)
                            Dim comparevalue As Integer = 0
                            Try
                                comparevalue = ConfigurationManager.AppSettings("AllowZero")
                            Catch ex As Exception
                            End Try
                            If comparevalue = -1 Then
                                If Not (IntNoOfWorkDays > 0) Then
                                    Continue For
                                End If
                            ElseIf comparevalue = 1 Then
                                If Not (IntNoOfWorkDays > 0 And NetSalary >= 0) Then
                                    Continue For
                                End If
                            Else
                                If Not (IntNoOfWorkDays > 0 And NetSalary > 0) Then
                                    Continue For
                                End If
                            End If
                        End If
                        If cmdString <> "" Then
                            Dim mSqlCommand As New SqlClient.SqlCommand
                            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                            mSqlCommand.CommandType = Data.CommandType.Text
                            mSqlCommand.CommandText = "BEGIN Transaction SalaryTrns; Declare @ProjectTransID Int; Declare @TransID Int;" & vbNewLine & cmdString & " Commit Transaction SalaryTrns;"
                            mSqlCommand.Connection.Open()
                            mSqlCommand.ExecuteNonQuery()
                            mSqlCommand.Connection.Close()
                        End If
                    End If
                Catch ex As Exception
                    Continue For
                End Try
            Next
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Done !/!تمت العملية"))
            GetData(True)
            Return True
        Catch ex As Exception
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Fail !/!فشلت العملية"))
            Return False
        End Try
    End Function

    Private Function maxloandeduction(dtDeduct As DataTable, MaxLoanDedution As Double) As DataTable

        Dim drows() As DataRow = dtDeduct.Select("EmpSchID >0")
        If drows.Length > 0 Then
            Dim dt As DataTable = dtDeduct.Select("EmpSchID >0").CopyToDataTable
            Dim tempdt As DataTable = dtDeduct.Select("isnull(EmpSchID,0)=0").CopyToDataTable




            Dim totalLoan = dt.Compute("Sum(amount)", "")
            If totalLoan >= MaxLoanDedution Then
                dtDeduct.Clear()
                dtDeduct = tempdt.Select().CopyToDataTable
                '  Dim maxloandedcution As Double = ClsEmployees.MaxLoanDedution
                For Each row As DataRow In dt.Rows
                    If row("amount") < MaxLoanDedution Then
                        dtDeduct.Rows.Add(New Object() {row(0), row(1), row(2), row(3), row(4), row(5)})
                        MaxLoanDedution = MaxLoanDedution - row("amount")
                    ElseIf MaxLoanDedution > 0 Then
                        dtDeduct.Rows.Add(New Object() {row(0), MaxLoanDedution, row(2), row(3), row(4), MaxLoanDedution})
                        MaxLoanDedution = 0
                    End If
                Next
            Else
                'For Each row As DataRow In dt.Rows

                '    dtDeduct.Rows.Add(New Object() {row(0), row(1), row(2), row(3), row(4), row(5)})
                '        MaxLoanDedution = MaxLoanDedution - row("amount")


                'Next
            End If
        End If
        Return dtDeduct

    End Function

    Public Function IsTransPaid(ByVal intTransTypeID As Integer) As String
        Dim dsTransType As New DataSet
        Dim clsTransactionsTypes As New Clshrs_TransactionsTypes(Me)
        If clsTransactionsTypes.Find("ID=" & intTransTypeID) Then
            If clsTransactionsTypes.DataSet.Tables(0).Rows(0).Item("IsPaid") = True Then
                Return "1"
            Else
                Return "0"
            End If
        Else
            Return "0"
        End If

    End Function
    Private Function CreateDataTable(ByVal DtTable As Data.DataTable, ByVal PtrTableName As String) As Boolean
        Dim ObjDataColumn As New Data.DataColumn
        Try
            DtTable.Columns.Clear()
            DtTable.TableName = PtrTableName

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "TransactionTypeID"
            ObjDataColumn.DataType = System.Type.GetType("System.Int32")
            DtTable.Columns.Add(ObjDataColumn)

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "Amount"
            ObjDataColumn.DataType = System.Type.GetType("System.Double")
            DtTable.Columns.Add(ObjDataColumn)

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "Description"
            ObjDataColumn.DataType = System.Type.GetType("System.String")
            DtTable.Columns.Add(ObjDataColumn)

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "EmpSchID"
            ObjDataColumn.DataType = System.Type.GetType("System.String")
            DtTable.Columns.Add(ObjDataColumn)
        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "Attend Function"

    Private Function SetDataAtt(Optional ByVal IsSave As Boolean = True) As Boolean
        Try
            Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)
            Dim clsCompanies As New Clssys_Companies(Page)
            clsCompanies.Find("ID=" & ClsFisicalPeriods.MainCompanyID)
            clsBranch = New Clssys_Branches(Page)
            clsBranch.Find("ID=" & ddlBranche.SelectedValue)

            Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsFisicalPeriods.ConnectionString)
            Dim ClsEmployees As New Clshrs_Employees(Page)
            Dim EmployeeDS As New Data.DataSet()
            Dim EmpIDs As String = String.Empty
            Dim mAllBranches As String = String.Empty
            If DdlPeriods.SelectedValue <> Nothing Then
                ClsFisicalPeriods.Find("ID=" & DdlPeriods.SelectedValue)
                mAllBranches = ddlBranche.SelectedValue
                For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                    If row.Cells(1).Value = True Then
                        If row.Cells(0).Value > 0 Then
                            Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Me)
                            If Not ClsEmployeesTransactions.Find("EmployeeID=" & row.Cells(0).Value & " And FiscalYearPeriodID=" & DdlPeriods.SelectedValue & " And PrepareType ='N'") Then
                                EmpIDs &= row.Cells(0).Value & ","
                            End If
                        End If
                    End If
                Next
                EmpIDs = EmpIDs.Remove(EmpIDs.Length - 1)

                FromDate = ClsFisicalPeriods.FromDate
                ToDate = ClsFisicalPeriods.ToDate
                FiscFromDate = ClsFisicalPeriods.FromDate
                FiscToDate = ClsFisicalPeriods.ToDate
                If clsBranch.PrepareDay > 0 Then
                    If clsCompanies.IsHigry = True Then
                        Dim strarr As String() = ClsFisicalPeriods.HFromDate.Split("/")
                        Dim FrmHDate As String = clsBranch.PrepareDay & "/" & IIf(strarr(1) = "01", "12", strarr(1) - 1) & "/" & IIf(strarr(1) = "01", strarr(2) - 1, strarr(2))
                        ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy")
                        FromDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy"), "dd/MM/yyyy")

                        Dim strarr1 As String() = FrmHDate.Split("/")
                        Dim ToHDate As String = clsBranch.PrepareDay - 1 & "/" & IIf(strarr1(1) = "12", "01", strarr1(1) + 1) & "/" & IIf(strarr1(1) = "12", strarr1(2) + 1, strarr1(2))
                        ToDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(ToHDate, "dd/MM/yyyy"), "dd/MM/yyyy")
                    Else
                        FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsBranch.PrepareDay)
                        ToDate = FromDate.AddMonths(1).AddDays(-1)
                    End If
                    If clsBranch.AffectPeriod Then
                        FiscFromDate = FromDate
                        FiscToDate = ToDate
                    End If
                Else
                    If clsCompanies.PrepareDay > 0 Then
                        If clsCompanies.IsHigry = True Then
                            Dim strarr As String() = ClsFisicalPeriods.HFromDate.Split("/")
                            Dim FrmHDate As String = clsCompanies.PrepareDay & "/" & IIf(strarr(1) = "01", "12", strarr(1) - 1) & "/" & IIf(strarr(1) = "01", strarr(2) - 1, strarr(2))
                            ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy")
                            FromDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy"), "dd/MM/yyyy")

                            Dim strarr1 As String() = FrmHDate.Split("/")
                            Dim ToHDate As String = clsCompanies.PrepareDay - 1 & "/" & IIf(strarr1(1) = "12", "01", strarr1(1) + 1) & "/" & IIf(strarr1(1) = "12", strarr1(2) + 1, strarr1(2))
                            ToDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(ToHDate, "dd/MM/yyyy"), "dd/MM/yyyy")
                        Else
                            FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsCompanies.PrepareDay)
                            ToDate = FromDate.AddMonths(1).AddDays(-1)
                        End If
                    End If
                End If

                Dim strwhr As String = ""
                strwhr = " and EmployeeID in(" & EmpIDs & ") "
                Dim str As String = "Set DateFormat DMY delete from Att_AttendancePreparationDetails where CONVERT(Datetime,GAttendDate,103)>= CONVERT(Datetime,'" & FromDate & "',103) and CONVERT(Datetime,GAttendDate,103) <= CONVERT(Datetime,'" & ToDate & "',103)" & strwhr
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsFisicalPeriods.ConnectionString, Data.CommandType.Text, str)
                If IsSave Then
                    If FillAttend(mAllBranches, EmpIDs) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                            objNav.SetLanguage(Page, "Operation Done !/!تمت العملية"))
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                            objNav.SetLanguage(Page, "Operation Done !/!تمت العملية"))
                End If
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                    objNav.SetLanguage(Page, "Please select Period/الرجاء إختيار الفترة المالية"))
            End If
        Catch ex As Exception

        End Try
    End Function
    Private Function FillAttend(ByVal AllBranches As String, ByVal AllEmp As String) As Boolean
        Try
            Dim clsDAL As New ClsDataAcessLayer(Page)
            Dim objNav As New Venus.Shared.Web.NavigationHandler(clsDAL.ConnectionString)
            Dim ClsAttendancePreparationDetails As New ClsAtt_AttendancePreparationDetails(Page)
            Dim ClsAttendancePreparationProjects As New ClsAtt_AttendancePreparationProjects(Page)
            Dim ClsAttendTransactions As New ClsAtt_AttendTransactions(Page)
            Dim ClsContract As New Clshrs_Contracts(Page)
            Dim ClsClasses As New Clshrs_EmployeeClasses(Page)
            Dim ClsClassCalander As New Clshrs_EmployeesClassCalander(Page)
            Dim ClsClassCalenderSet As New Clshrs_EmployeesClassesCalenderSet(Page)
            Dim ClsClassDelay As New Clshrs_EmployeesClassesDelay(Page)
            Dim ClsEmployees As New Clshrs_Employees(Page)
            Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)
            ClsFisicalPeriods.Find("ID=" & DdlPeriods.SelectedValue)
            Dim clsCompanies As New Clssys_Companies(Page)
            clsCompanies.Find("ID=" & ClsFisicalPeriods.MainCompanyID)
            Dim ClassCalanderSetDS As New Data.DataSet
            Dim EmployeetrnsDt As New Data.DataTable
            Dim EmployeeDS As New Data.DataSet()
            Dim NumberOfDays As Integer = 0
            Dim ClsOvertimeReviewDetails As New ClsAtt_OvertimeReviewDetail(Page)


            ClsEmployees.GetAllEmployeeValidContract(EmployeeDS, AllEmp, FromDate, ToDate)
            If Not Load_ClsLayers() Then Return False
            For EmpCnt As Integer = 0 To EmployeeDS.Tables(0).Rows.Count - 1
                Dim ValidContract As Integer = EmployeeDS.Tables(0).Rows(EmpCnt)("ContractID")
                ClsContract.Find("ID = " & ValidContract)
                If (ClsContract.DataSet.Tables(0).Rows.Count < 1) Then
                    Continue For
                End If
                If Not ClsClasses.Find("ID = " & EmployeeDS.Tables(0).Rows(EmpCnt)("ClassID")) Then
                    Continue For
                End If
                ClsEmployeeClass = ClsClasses
                Dim DailyPermit As Integer = ClsClasses.PerDailyDelaying
                Dim MonthlyPermit As Integer = ClsClasses.PerMonthlyDelaying
                ClsEmployees.Find("ID = " & EmployeeDS.Tables(0).Rows(EmpCnt)("ID"))
                NumberOfDays = Get_NoOfDays()

                Dim whours As Integer = ClsEmployees.WHours
                Dim isrelated As Object = ClsEmployees.IsProjectRelated

                Dim CntDays As Integer = ToDate.Subtract(FromDate).Days

                '------------------ Related Project ------------- 
                If isrelated = "True" Then
                    Dim EmployeeID As Int32 = ClsEmployees.ID

                    ClsEmployees = New Clshrs_Employees(Page)
                    ClsEmployees.Find("ID=" & EmployeeID)
                    Dim Amount As Double = 0
                    If Not SetScreenSetting(ClsEmployees.ID, Amount) Then Continue For
                    IntFisicalPeriod = DdlPeriods.SelectedValue
                    Dim cntsick As Integer = 0
                    Dim cntLeave As Integer = 0
                    Dim cntAbsent As Integer = 0
                    Dim strgetChanges As String = "set dateformat dmy; select Distinct convert(varchar(10),ActiveDate,103) AS Dte from hrs_ContractsTransactions where ActiveDate >= " & FromDate.ToString("dd/MM/yyyy") & " and canceldate is null and Active = 1 and contractID in (select ID from hrs_contracts where EmployeeID = " & ClsEmployees.ID & " and canceldate is null)"
                    Dim dtChanges As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, System.Data.CommandType.Text, strgetChanges).Tables(0)

                    For CounDays As Integer = 0 To CntDays
                        Dim OperDate As DateTime = FromDate.AddDays(CounDays)

                        If ClsEmployees.JoinDate > OperDate Then
                            Continue For
                        End If
                        If ClsContract.StartDate > OperDate Then
                            Continue For
                        End If
                        If ClsContract.EndDate <> Nothing And ClsContract.EndDate < OperDate Then
                            Dim ValidContractChek As Integer = ClsContract.ContractValidatoinId(Convert.ToInt32(EmployeeDS.Tables(0).Select("BranchID=" & ddlBranche.SelectedValue)(EmpCnt).Item("ID").ToString()), OperDate)
                            If ValidContractChek > 0 Then
                                ClsContract.Find("ID = " & ValidContractChek)
                                If Not ClsClasses.Find("ID = " & ClsContract.EmployeeClassID) Then
                                    Continue For
                                End If
                                ClsEmployeeClass = ClsClasses
                                DailyPermit = ClsClasses.PerDailyDelaying
                                MonthlyPermit = ClsClasses.PerMonthlyDelaying
                                NumberOfDays = Get_NoOfDays()
                                Calculate_SalaryPerHour(Amount, ClsEmployees.ID, OperDate)
                            Else
                                Continue For
                            End If
                        End If

                        If OperDate > ToDate Then
                            Continue For
                        Else
                            If dtChanges.Select("Dte = '" & OperDate.ToString("dd/MM/yyyy") & "'").Length > 0 Then
                                Calculate_SalaryPerHour(Amount, ClsEmployees.ID, OperDate)
                            End If
                            ClsAttendancePreparationDetails = New ClsAtt_AttendancePreparationDetails(Page)
                            ClsAttendancePreparationDetails.GAttendDate = OperDate
                            ClsAttendancePreparationDetails.HAttendDate = ClsDataAcessLayer.GregToHijri(OperDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
                            ClsAttendancePreparationDetails.EmployeeID = ClsEmployees.ID
                            Dim hrsEmployeesVacations As New Clshrs_EmployeesVacations(Page)
                            hrsEmployeesVacations.Find("EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103) >= CONVERT(date,ActualStartDate,103) and CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103) < CONVERT(date,isnull(ActualEndDate,'01/01/2050'),103)")
                            If (hrsEmployeesVacations.DataSet.Tables(0).Rows.Count > 0) Then
                                Dim ClsVacationsTypes As New Clshrs_VacationsTypes(Me.Page)
                                ClsVacationsTypes.Find("ID =  " & hrsEmployeesVacations.VacationTypeID)
                                If ClsVacationsTypes.IsPaid = -1 Then
                                    Dim CProject As Integer = 0
                                    Dim CRefNumber As String = "-1"
                                    Dim ClsAttendTransactionsLast As New ClsAtt_AttendTransactions(Page)
                                    If ClsAttendTransactionsLast.Find("EmployeeID = '" & ClsEmployees.ID & "' and CONVERT(date,TrnsDatetime,103) <= CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103)") Then
                                        CProject = ClsAttendTransactionsLast.ProjectID
                                        CRefNumber = ClsAttendTransactionsLast.RegComputerID
                                    End If
                                    If Not CProject > 0 Then
                                        CProject = EmployeeDS.Tables(0).Select("BranchID=" & ddlBranche.SelectedValue)(EmpCnt).Item("CProject").ToString()
                                        CRefNumber = EmployeeDS.Tables(0).Select("BranchID=" & ddlBranche.SelectedValue)(EmpCnt).Item("CRef").ToString()
                                    End If
                                    Dim clsplacements As New Clshrs_ProjectPlacements(Me.Page)
                                    If clsplacements.Find("ID = " & CRefNumber) Then
                                        Dim locations As New Clshrs_ProjectLocations(Me.Page)
                                        If locations.Find("ID = " & clsplacements.LocationID) Then
                                            CRefNumber = IIf(locations.LinkedCS = "", 0, locations.LinkedCS)
                                        End If
                                    End If

                                    ClsAttendancePreparationProjects = New ClsAtt_AttendancePreparationProjects(Page)
                                    ClsAttendancePreparationDetails.LeavingType = hrsEmployeesVacations.VacationTypeID
                                    'ClsAttendancePreparationDetails.IsAbsent = True
                                    ClsAttendancePreparationDetails.FiscalYearPeriodID = IntFisicalPeriod
                                    ClsAttendancePreparationProjects.TrnsID = ClsAttendancePreparationDetails.Save()
                                    ClsAttendancePreparationProjects.ProjectID = CProject
                                    ClsAttendancePreparationProjects.LinkedCS = CRefNumber
                                    ClsAttendancePreparationProjects.SalaryPerDay = Amount / Get_NoOfDays()
                                    ClsAttendancePreparationProjects.SalaryPerHour = 0
                                    ClsAttendancePreparationProjects.IsAbsent = True
                                    ClsAttendancePreparationProjects.Save()
                                Else
                                    ClsAttendancePreparationProjects = New ClsAtt_AttendancePreparationProjects(Page)
                                    ClsAttendancePreparationDetails.LeavingType = hrsEmployeesVacations.VacationTypeID
                                    ClsAttendancePreparationDetails.FiscalYearPeriodID = IntFisicalPeriod
                                    ClsAttendancePreparationProjects.TrnsID = ClsAttendancePreparationDetails.Save()
                                    ClsAttendancePreparationProjects.ProjectID = ClsClasses.DefaultProjectID
                                    ClsAttendancePreparationProjects.LinkedCS = ""
                                    ClsAttendancePreparationProjects.Save()
                                End If
                            Else
                                If (ClsClassCalander.Find("EmployeeClassID = " & ClsClasses.ID & " and CONVERT(date,FromTime,103) = CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103)") = True) Then
                                    If ClsClassCalander.nonWorkingTime = True Then
                                        ClsAttendancePreparationDetails.IsVacation = True
                                    Else
                                        ClsAttendancePreparationDetails.IsVacation = False
                                    End If
                                End If
                                ClsAttendTransactions.Find("EmployeeID = '" & ClsEmployees.ID & "' and CONVERT(date,TrnsDatetime,103) = CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103)")
                                Dim thours As Double = 0
                                For att As Integer = 0 To ClsAttendTransactions.DataSet.Tables(0).Rows.Count - 1
                                    thours = thours + ClsAttendTransactions.DataSet.Tables(0).Rows(att)("TotalHours")
                                Next att
                                If ClsAttendTransactions.DataSet.Tables(0).Rows.Count = 0 Then
                                    Dim CProject As Integer = 0
                                    Dim CRefNumber As String = "-1"
                                    Dim ClsAttendTransactionsLast As New ClsAtt_AttendTransactions(Page)
                                    If ClsAttendTransactionsLast.Find("EmployeeID = '" & ClsEmployees.ID & "' and CONVERT(date,TrnsDatetime,103) <= CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103)") Then
                                        CProject = ClsAttendTransactionsLast.ProjectID
                                        CRefNumber = ClsAttendTransactionsLast.RegComputerID
                                    End If
                                    If Not CProject > 0 Then
                                        CProject = EmployeeDS.Tables(0).Select("BranchID=" & ddlBranche.SelectedValue)(EmpCnt).Item("CProject").ToString()
                                        CRefNumber = EmployeeDS.Tables(0).Select("BranchID=" & ddlBranche.SelectedValue)(EmpCnt).Item("CRef").ToString()
                                    End If
                                    Dim clsplacements As New Clshrs_ProjectPlacements(Me.Page)
                                    If clsplacements.Find("ID = " & CRefNumber) Then
                                        Dim locations As New Clshrs_ProjectLocations(Me.Page)
                                        If locations.Find("ID = " & clsplacements.LocationID) Then
                                            CRefNumber = IIf(locations.LinkedCS = "", 0, locations.LinkedCS)
                                        End If
                                    End If

                                    ClsAttendancePreparationProjects = New ClsAtt_AttendancePreparationProjects(Page)
                                    If clsBranch.DefaultAbsent Then
                                        ClsAttendancePreparationDetails.IsAbsent = True
                                        ClsAttendancePreparationProjects.IsAbsent = True
                                    Else
                                        If clsCompanies.DefaultAttend = True Then
                                            ClsAttendancePreparationDetails.IsAbsent = True
                                            ClsAttendancePreparationProjects.IsAbsent = True
                                        Else
                                            ClsAttendancePreparationDetails.IsAbsent = False
                                            ClsAttendancePreparationProjects.IsAbsent = False
                                        End If
                                    End If
                                    ClsAttendancePreparationDetails.FiscalYearPeriodID = IntFisicalPeriod
                                    ClsAttendancePreparationProjects.TrnsID = ClsAttendancePreparationDetails.Save()
                                    ClsAttendancePreparationProjects.ProjectID = CProject
                                    ClsAttendancePreparationProjects.LinkedCS = CRefNumber
                                    ClsAttendancePreparationProjects.SalaryPerDay = Amount / Get_NoOfDays()
                                    ClsAttendancePreparationProjects.SalaryPerHour = 0
                                    ClsAttendancePreparationProjects.Save()
                                Else
                                    If ClsAttendTransactions.Status <> 0 Then
                                        If ClsAttendTransactions.Status = 1 Then
                                            ClsAttendancePreparationDetails.IsVacation = True
                                        ElseIf ClsAttendTransactions.Status = -1 Then
                                            ClsAttendancePreparationDetails.IsVacation = False
                                        End If
                                    End If
                                    Dim LastTrnsID As Integer = 0
                                    Dim totallat As Double = 0

                                    If thours = 0 Then
                                        ClsAttendancePreparationDetails.IsAbsent = True
                                    End If
                                    ClsAttendancePreparationDetails.IsAbsent = True
                                    ClsAttendancePreparationDetails.FiscalYearPeriodID = IntFisicalPeriod
                                    LastTrnsID = ClsAttendancePreparationDetails.Save()
                                    EmployeetrnsDt = DailyAttendanceTrnsSlice(ClsAttendTransactions.DataSet)
                                    ClsAttendancePreparationDetails.Find("ID = " & LastTrnsID)
                                    For i As Integer = 0 To EmployeetrnsDt.Rows.Count - 1
                                        Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                                        If hrsProjectPlacements.Find("ID = " & EmployeetrnsDt.Rows(i)("RefTrnsID").ToString()) Then
                                            Dim hrsProjectSetting As New Clshrs_ProjectSetting(Me)
                                            If hrsProjectSetting.Find("ProjectChangeID = " & hrsProjectPlacements.ProjectChangeID) Then
                                                ClsAttendancePreparationProjects = New ClsAtt_AttendancePreparationProjects(Page)
                                                ClsAttendancePreparationProjects.ProjectID = Convert.ToInt32(EmployeetrnsDt.Rows(i)("ProjectID").ToString())
                                                ClsAttendancePreparationProjects.TrnsID = LastTrnsID
                                                If EmployeetrnsDt.Rows(i)("FromHoure").ToString() <> "" Then
                                                    ClsAttendancePreparationProjects.Checkin = Convert.ToDateTime(OperDate.ToString("dd/MM/yyyy") & " " & CheckTime(EmployeetrnsDt.Rows(i)("FromHoure").ToString()))
                                                End If
                                                If EmployeetrnsDt.Rows(i)("ToHoure").ToString() <> "" Then
                                                    ClsAttendancePreparationProjects.Checkout = Convert.ToDateTime(OperDate.ToString("dd/MM/yyyy") & " " & CheckTime(EmployeetrnsDt.Rows(i)("ToHoure").ToString()))
                                                End If
                                                ClsAttendancePreparationProjects.TotalTime = Convert.ToDouble(EmployeetrnsDt.Rows(i)("TotalHours"))
                                                'Late
                                                Dim TotalLattime As Double = 0
                                                Dim PermitLattime As Double = 0
                                                Dim NotPermitLattime As Double = 0
                                                If hrsProjectSetting.InternalPermitDelayFactor <> "" Then
                                                    Dim latslice As String() = hrsProjectSetting.InternalPermitDelayFactor.Split(",")
                                                    Dim latPunslice As String() = hrsProjectSetting.InternalDelayPunishFactor.Split(",")
                                                    Dim charIdx As Integer = 0
                                                    TotalLattime = Convert.ToDouble(EmployeetrnsDt.Rows(i)("TotalLat"))
                                                    For chsrarray As Integer = 0 To latslice.Length - 1
                                                        If (TotalLattime - Convert.ToDouble(latslice(chsrarray).ToString())) <= 0 Then
                                                            Exit For
                                                        End If
                                                        charIdx = IIf(charIdx + 1 > latslice.Length - 1, latslice.Length - 1, charIdx + 1)
                                                    Next
                                                    NotPermitLattime = IIf((IIf(TotalLattime > 0, (TotalLattime - Convert.ToDouble(latslice(IIf(charIdx - 1 < 0, 0, charIdx - 1)).ToString())), 0)) < 0, 0, IIf(TotalLattime > 0, (TotalLattime - Convert.ToDouble(latslice(IIf(charIdx - 1 < 0, 0, charIdx - 1)).ToString())), 0))
                                                    ClsAttendancePreparationProjects.NotpermitLate = NotPermitLattime / 60
                                                    ClsAttendancePreparationProjects.LatPunishment = Convert.ToDouble(latPunslice(charIdx).ToString())
                                                    totallat = totallat + ClsAttendancePreparationProjects.NotpermitLate
                                                Else
                                                    TotalLattime = Convert.ToDouble(EmployeetrnsDt.Rows(i)("TotalLat"))
                                                    ClsAttendancePreparationProjects.NotpermitLate = 0
                                                    ClsAttendancePreparationProjects.LatPunishment = 0
                                                    totallat = totallat + ClsAttendancePreparationProjects.NotpermitLate
                                                End If
                                                Dim sts As Integer = Convert.ToInt32(EmployeetrnsDt.Rows(i)("Status"))
                                                If sts = 1 Then
                                                    ClsAttendancePreparationProjects.IsVacation = True
                                                    ClsAttendancePreparationProjects.IsAbsent = False
                                                    ClsAttendancePreparationDetails.IsAbsent = False

                                                    If ClsAttendancePreparationProjects.TotalTime > 0 Then
                                                        ClsAttendancePreparationProjects.HolidayHours = (IIf(ClsEmployees.WHours > 0, ClsEmployees.WHours, Convert.ToDouble(EmployeetrnsDt.Rows(i)("TotalHours")))) + (Convert.ToDouble(EmployeetrnsDt.Rows(i)("Overtime")) / 60)
                                                        If ClsAttendancePreparationProjects.HolidayHours > 0 Then
                                                            ClsAttendancePreparationProjects.HOTFactor = hrsProjectSetting.InternalDayOffOvertimeFactor
                                                        End If
                                                    End If
                                                ElseIf sts = 0 Then
                                                    cntAbsent = cntAbsent + 1
                                                    ClsAttendancePreparationProjects.IsAbsent = True
                                                    ClsAttendancePreparationDetails.IsAbsent = IIf(ClsAttendancePreparationDetails.IsAbsent = False, False, True)

                                                    Dim Absentslice As String() = hrsProjectSetting.InternalAbsentFactor.Split(",")
                                                    If Absentslice.Length >= cntAbsent Then
                                                        ClsAttendancePreparationProjects.AbsentPunishment = Absentslice(cntAbsent - 1)
                                                    Else
                                                        ClsAttendancePreparationProjects.AbsentPunishment = Absentslice(IIf(Absentslice.Length - 1 < 0, 0, Absentslice.Length - 1))
                                                    End If
                                                ElseIf sts = -1 Then
                                                    ClsAttendancePreparationProjects.IsVacation = False
                                                    ClsAttendancePreparationProjects.IsAbsent = False
                                                    ClsAttendancePreparationDetails.IsAbsent = False
                                                ElseIf sts = 2 Then
                                                    cntLeave = cntLeave + 1
                                                    ClsAttendancePreparationProjects.IsLeave = True
                                                    ClsAttendancePreparationProjects.IsAbsent = False
                                                    ClsAttendancePreparationDetails.IsAbsent = False

                                                    Dim Absentslice As String() = hrsProjectSetting.InternalLeavFactor.Split(",")
                                                    If Absentslice.Length >= cntLeave Then
                                                        ClsAttendancePreparationProjects.LeavePunishment = Absentslice(cntLeave - 1)
                                                    Else
                                                        ClsAttendancePreparationProjects.LeavePunishment = Absentslice(IIf(Absentslice.Length - 1 < 0, 0, Absentslice.Length - 1))
                                                    End If
                                                ElseIf sts = 3 Then
                                                    cntsick = cntsick + 1
                                                    ClsAttendancePreparationProjects.IsSick = True
                                                    ClsAttendancePreparationProjects.IsAbsent = False
                                                    ClsAttendancePreparationDetails.IsAbsent = False

                                                    Dim Absentslice As String() = hrsProjectSetting.InternalSickFactor.Split(",")
                                                    If Absentslice.Length >= cntsick Then
                                                        ClsAttendancePreparationProjects.SickPunishment = Absentslice(cntsick - 1)
                                                    Else
                                                        ClsAttendancePreparationProjects.SickPunishment = Absentslice(IIf(Absentslice.Length - 1 < 0, 0, Absentslice.Length - 1))
                                                    End If
                                                End If
                                                If sts <> 1 Then
                                                    ClsAttendancePreparationProjects.Overtime = Convert.ToDouble(EmployeetrnsDt.Rows(i)("Overtime")) / 60
                                                    If ClsAttendancePreparationProjects.Overtime > 0 Then
                                                        ClsAttendancePreparationProjects.OTFactor = hrsProjectSetting.InternalOvertimeFactor
                                                    End If
                                                End If
                                                ClsAttendancePreparationProjects.SalaryPerDay = Amount / Get_NoOfDays()
                                                If ClsAttendancePreparationProjects.TotalTime > 0 Then
                                                    If whours = 0 Then
                                                        ClsAttendancePreparationProjects.SalaryPerHour = (ClsAttendancePreparationProjects.SalaryPerDay / ClsAttendancePreparationProjects.TotalTime)
                                                    Else
                                                        ClsAttendancePreparationProjects.SalaryPerHour = (ClsAttendancePreparationProjects.SalaryPerDay / whours)
                                                    End If
                                                Else
                                                    ClsAttendancePreparationProjects.SalaryPerHour = 0
                                                End If
                                                Dim clsplacements As New Clshrs_ProjectPlacements(Me.Page)
                                                If clsplacements.Find("ID = " & ClsAttendTransactions.RegComputerID) Then
                                                    Dim locations As New Clshrs_ProjectLocations(Me.Page)
                                                    If locations.Find("ID = " & clsplacements.LocationID) Then
                                                        ClsAttendancePreparationProjects.LinkedCS = locations.LinkedCS
                                                    End If
                                                End If
                                                ClsAttendancePreparationProjects.Save()
                                            Else
                                            End If
                                        End If
                                    Next i
                                    ClsAttendancePreparationDetails.NotpermitLate = totallat
                                    ClsAttendancePreparationDetails.Update("ID = " & LastTrnsID)
                                End If
                            End If
                        End If
                    Next CounDays
                    For CounDays As Integer = 0 To CntDays
                        ClsAttendancePreparationDetails = New ClsAtt_AttendancePreparationDetails(Page)
                        Dim OperDate As DateTime = FromDate.AddDays(CounDays)
                        ClsAttendancePreparationDetails.Find("EmployeeID = '" & ClsEmployees.ID & "' and CONVERT(date,GAttendDate,103) = CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103)")
                        If ClsAttendancePreparationDetails.DataSet.Tables(0).Rows.Count > 0 Then
                            Dim IsOff As Boolean = False
                            If ClsAttendancePreparationDetails.IsVacation = True Then
                                For CounDays1 As Integer = 1 To CounDays
                                    Dim str1 As String = CheckDay(CounDays1 * -1, ClsClasses.ID, ClsEmployees.ID, OperDate)
                                    If str1.Split(",")(0) = "0" Then
                                        If str1.Split(",")(1) = "1" Then
                                            IsOff = True
                                            Exit For
                                        Else
                                            IsOff = False
                                            Exit For
                                        End If
                                    End If
                                Next
                                If IsOff = False Then
                                    For CounDays1 As Integer = 1 To CntDays
                                        Dim str1 As String = CheckDay(CounDays1, ClsClasses.ID, ClsEmployees.ID, OperDate)
                                        If str1.Split(",")(0) = "0" Then
                                            If str1.Split(",")(1) = "1" Then
                                                IsOff = True
                                                Exit For
                                            Else
                                                IsOff = False
                                                Exit For
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                            ClsAttendancePreparationDetails.IsVacation = IsOff
                            ClsAttendancePreparationDetails.IsAbsent = IIf(IsOff = True, False, ClsAttendancePreparationDetails.IsAbsent)
                            ClsAttendancePreparationDetails.Update("ID = " & ClsAttendancePreparationDetails.ID)
                        End If
                    Next CounDays

                    '---------------------- Not Related Project ------------------------
                Else
                    Dim cmdString As String = ""
                    Dim EmployeeID As Int32 = ClsEmployees.ID
                    Dim Amount As Double = 0
                    If Not SetScreenSetting(ClsEmployees.ID, Amount) Then Continue For
                    IntFisicalPeriod = DdlPeriods.SelectedValue
                    Dim strgetChanges As String = "set dateformat dmy; select Distinct convert(varchar(10),ActiveDate,103) AS Dte from hrs_ContractsTransactions where ActiveDate >= " & FromDate.ToString("dd/MM/yyyy") & " and canceldate is null and Active = 1 and contractID in (select ID from hrs_contracts where EmployeeID = " & ClsEmployees.ID & " and canceldate is null)"
                    Dim dtChanges As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, System.Data.CommandType.Text, strgetChanges).Tables(0)
                    Dim CurrentProject As Integer = 0
                    Dim ClsAttendTransactionsLast As New ClsAtt_AttendTransactions(Page)
                    If ClsAttendTransactionsLast.Find("EmployeeID = '" & ClsEmployees.ID & "' and CONVERT(date,TrnsDatetime,103) <= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) order by TrnsDatetime DESC") Then
                        CurrentProject = ClsAttendTransactionsLast.ProjectID
                    Else
                        CurrentProject = ClsClasses.DefaultProjectID
                    End If
                    '------------- loop on attendance days --------------
                    For CounDays As Integer = 0 To CntDays
                        Dim OperDate As DateTime = FromDate.AddDays(CounDays)
                        If ClsEmployees.JoinDate > OperDate Then
                            Continue For
                        ElseIf ClsContract.StartDate > OperDate Then
                            Continue For
                        End If
                        If ClsContract.EndDate <> Nothing And ClsContract.EndDate < OperDate Then
                            Dim ValidContractChek As Integer = ClsContract.ContractValidatoinId(Convert.ToInt32(EmployeeDS.Tables(0).Select("BranchID=" & ddlBranche.SelectedValue)(EmpCnt).Item("ID").ToString()), OperDate)
                            If ValidContractChek > 0 Then
                                ClsContract.Find("ID = " & ValidContractChek)
                                If Not ClsClasses.Find("ID = " & ClsContract.EmployeeClassID) Then
                                    Continue For
                                End If
                                ClsEmployeeClass = ClsClasses
                                DailyPermit = ClsClasses.PerDailyDelaying
                                MonthlyPermit = ClsClasses.PerMonthlyDelaying
                                NumberOfDays = Get_NoOfDays()
                                Calculate_SalaryPerHour(Amount, ClsEmployees.ID, OperDate)
                            Else
                                Continue For
                            End If
                        End If
                        If OperDate > ToDate Then
                            Continue For
                        Else
                            If dtChanges.Select("Dte = '" & OperDate.ToString("dd/MM/yyyy") & "'").Length > 0 Then
                                Calculate_SalaryPerHour(Amount, ClsEmployees.ID, OperDate)
                            End If
                            ClsAttendancePreparationDetails = New ClsAtt_AttendancePreparationDetails(Page)
                            ClsAttendancePreparationDetails.GAttendDate = OperDate
                            ClsAttendancePreparationDetails.HAttendDate = ClsDataAcessLayer.GregToHijri(OperDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
                            ClsAttendancePreparationDetails.EmployeeID = ClsEmployees.ID
                            Dim hrsEmployeesVacations As New Clshrs_EmployeesVacations(Page)
                            hrsEmployeesVacations.Find("EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103) >= CONVERT(date,ActualStartDate,103) and CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103) < CONVERT(date,isnull(ActualEndDate,'01/01/2050'),103)")
                            If (hrsEmployeesVacations.DataSet.Tables(0).Rows.Count > 0) Then
                                Dim ClsVacationsTypes As New Clshrs_VacationsTypes(Me.Page)
                                ClsVacationsTypes.Find("ID =  " & hrsEmployeesVacations.VacationTypeID)
                                If ClsVacationsTypes.IsPaid = -1 Then
                                    ClsAttendancePreparationProjects = New ClsAtt_AttendancePreparationProjects(Page)
                                    ClsAttendancePreparationDetails.LeavingType = hrsEmployeesVacations.VacationTypeID
                                    'ClsAttendancePreparationDetails.IsAbsent = True
                                    ClsAttendancePreparationDetails.FiscalYearPeriodID = IntFisicalPeriod

                                    cmdString &= " INSERT INTO Att_AttendancePreparationDetails (EmployeeID,GAttendDate,HAttendDate,TotalLate,PermitLate,NotpermitLate,LatPunishment,LeavingType,IsVacation,IsAbsent,Notes,Remarks,RegUserID,RegComputerID,RegDate,CancelDate,FiscalYearPeriodID)"
                                    cmdString &= " values(" & ClsAttendancePreparationDetails.EmployeeID & ",'" & ClsAttendancePreparationDetails.GAttendDate.ToString("yyyy-MM-dd") & "','" & ClsDataAcessLayer.GregToHijri(OperDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy") & "',0,0,0,0," & hrsEmployeesVacations.VacationTypeID & ",0,0,'',''," & ClsAttendancePreparationDetails.DataBaseUserRelatedID & ",NULL,getdate(),NULL," & ClsAttendancePreparationDetails.FiscalYearPeriodID & ")"
                                    cmdString &= " set @TransID = (select SCOPE_IDENTITY())"


                                    ClsAttendancePreparationProjects.ProjectID = CurrentProject
                                    ClsAttendancePreparationProjects.SalaryPerDay = Amount / NumberOfDays
                                    ClsAttendancePreparationProjects.SalaryPerHour = (Amount / NumberOfDays) / ClsClasses.WorkHoursPerDay
                                    ClsAttendancePreparationProjects.OTSalary = dbOTSalary
                                    ClsAttendancePreparationProjects.HOTSalary = dbHOTSalary
                                    ClsAttendancePreparationProjects.IsAbsent = True

                                    cmdString &= " INSERT INTO Att_AttendancePreparationProjects (TrnsID,ProjectID,Checkin,Checkout,TotalTime,Overtime,HolidayHours,IsVacation,IsAbsent,IsSick,IsLeave,NotpermitLate,AbsentPunishment,SickPunishment,OTFactor,HOTFactor,LeavePunishment,LatPunishment,SalaryPerDay,SalaryPerHour,OTSalary,HOTSalary,Remarks,RegUserID,RegComputerID,RegDate,CancelDate,OOvertime,OHolidayHours,LinkedCS)"
                                    cmdString &= " values (@TransID," & ClsAttendancePreparationProjects.ProjectID & ",NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,0,0,0," & ClsAttendancePreparationProjects.SalaryPerDay & "," & ClsAttendancePreparationProjects.SalaryPerHour & "," & ClsAttendancePreparationProjects.OTSalary & "," & ClsAttendancePreparationProjects.HOTSalary & ",''," & ClsAttendancePreparationDetails.DataBaseUserRelatedID & ",NULL,getdate(),NULL,0,0,NULL)"
                                Else
                                    ClsAttendancePreparationProjects = New ClsAtt_AttendancePreparationProjects(Page)
                                    ClsAttendancePreparationDetails.LeavingType = hrsEmployeesVacations.VacationTypeID
                                    ClsAttendancePreparationDetails.FiscalYearPeriodID = IntFisicalPeriod

                                    cmdString &= " INSERT INTO Att_AttendancePreparationDetails (EmployeeID,GAttendDate,HAttendDate,TotalLate,PermitLate,NotpermitLate,LatPunishment,LeavingType,IsVacation,IsAbsent,Notes,Remarks,RegUserID,RegComputerID,RegDate,CancelDate,FiscalYearPeriodID)"
                                    cmdString &= " values(" & ClsAttendancePreparationDetails.EmployeeID & ",'" & ClsAttendancePreparationDetails.GAttendDate.ToString("yyyy-MM-dd") & "','" & ClsDataAcessLayer.GregToHijri(OperDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy") & "',0,0,0,0," & hrsEmployeesVacations.VacationTypeID & ",0,0,'',''," & ClsAttendancePreparationDetails.DataBaseUserRelatedID & ",NULL,getdate(),NULL," & ClsAttendancePreparationDetails.FiscalYearPeriodID & ")"
                                    cmdString &= " set @TransID = (select SCOPE_IDENTITY())"

                                    ClsAttendancePreparationProjects.ProjectID = CurrentProject
                                    ClsAttendancePreparationProjects.SalaryPerDay = Amount / NumberOfDays
                                    ClsAttendancePreparationProjects.SalaryPerHour = (Amount / NumberOfDays) / ClsClasses.WorkHoursPerDay
                                    ClsAttendancePreparationProjects.OTSalary = dbOTSalary
                                    ClsAttendancePreparationProjects.HOTSalary = dbHOTSalary

                                    cmdString &= " INSERT INTO Att_AttendancePreparationProjects (TrnsID,ProjectID,Checkin,Checkout,TotalTime,Overtime,HolidayHours,IsVacation,IsAbsent,IsSick,IsLeave,NotpermitLate,AbsentPunishment,SickPunishment,OTFactor,HOTFactor,LeavePunishment,LatPunishment,SalaryPerDay,SalaryPerHour,OTSalary,HOTSalary,Remarks,RegUserID,RegComputerID,RegDate,CancelDate,OOvertime,OHolidayHours,LinkedCS)"
                                    cmdString &= " values (@TransID," & ClsAttendancePreparationProjects.ProjectID & ",NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,0,0,0," & ClsAttendancePreparationProjects.SalaryPerDay & "," & ClsAttendancePreparationProjects.SalaryPerHour & "," & ClsAttendancePreparationProjects.OTSalary & "," & ClsAttendancePreparationProjects.HOTSalary & ",''," & ClsAttendancePreparationDetails.DataBaseUserRelatedID & ",NULL,getdate(),NULL,0,0,NULL)"
                                End If
                            Else
                                If (ClsClassCalander.Find("EmployeeClassID = " & ClsClasses.ID & " and CONVERT(date,FromTime,103) = CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103)") = True) Then
                                    If ClsClassCalander.nonWorkingTime = True Then
                                        ClsAttendancePreparationDetails.IsVacation = True
                                    Else
                                        ClsAttendancePreparationDetails.IsVacation = False
                                    End If
                                ElseIf (ClsClassCalenderSet.Find("EmployeeClassID = " & ClsClasses.ID & " and DayNumber = " & RetDayNumber(OperDate)) = True) Then
                                    If ClsClassCalenderSet.NonWorkingTime = True Then
                                        ClsAttendancePreparationDetails.IsVacation = True
                                    Else
                                        ClsAttendancePreparationDetails.IsVacation = False
                                    End If
                                End If
                                If Convert.ToBoolean(ClsClasses.HasAttendance) = False Then
                                    ClsAttendancePreparationProjects = New ClsAtt_AttendancePreparationProjects(Page)
                                    ClsAttendancePreparationDetails.IsAbsent = False
                                    ClsAttendancePreparationDetails.FiscalYearPeriodID = IntFisicalPeriod
                                    cmdString &= " INSERT INTO Att_AttendancePreparationDetails (EmployeeID,GAttendDate,HAttendDate,TotalLate,PermitLate,NotpermitLate,LatPunishment,LeavingType,IsVacation,IsAbsent,Notes,Remarks,RegUserID,RegComputerID,RegDate,CancelDate,FiscalYearPeriodID)"
                                    cmdString &= " values(" & ClsAttendancePreparationDetails.EmployeeID & ",'" & ClsAttendancePreparationDetails.GAttendDate.ToString("yyyy-MM-dd") & "','" & ClsDataAcessLayer.GregToHijri(OperDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy") & "',0,0,0,0,0,0,0,'',''," & ClsAttendancePreparationDetails.DataBaseUserRelatedID & ",NULL,getdate(),NULL," & ClsAttendancePreparationDetails.FiscalYearPeriodID & ")"
                                    cmdString &= " set @TransID = (select SCOPE_IDENTITY())"

                                    ClsAttendancePreparationProjects.ProjectID = CurrentProject
                                    ClsAttendancePreparationProjects.SalaryPerDay = Amount / NumberOfDays
                                    ClsAttendancePreparationProjects.SalaryPerHour = (Amount / NumberOfDays) / ClsClasses.WorkHoursPerDay
                                    ClsAttendancePreparationProjects.OTSalary = dbOTSalary
                                    ClsAttendancePreparationProjects.HOTSalary = dbHOTSalary
                                    ClsAttendancePreparationProjects.IsAbsent = False

                                    cmdString &= " INSERT INTO Att_AttendancePreparationProjects (TrnsID,ProjectID,Checkin,Checkout,TotalTime,Overtime,HolidayHours,IsVacation,IsAbsent,IsSick,IsLeave,NotpermitLate,AbsentPunishment,SickPunishment,OTFactor,HOTFactor,LeavePunishment,LatPunishment,SalaryPerDay,SalaryPerHour,OTSalary,HOTSalary,Remarks,RegUserID,RegComputerID,RegDate,CancelDate,OOvertime,OHolidayHours,LinkedCS)"
                                    cmdString &= " values (@TransID," & ClsAttendancePreparationProjects.ProjectID & ",NULL,NULL,0,0,0,0,0,0,0,0,0,0,0,0,0,0," & ClsAttendancePreparationProjects.SalaryPerDay & "," & ClsAttendancePreparationProjects.SalaryPerHour & "," & ClsAttendancePreparationProjects.OTSalary & "," & ClsAttendancePreparationProjects.HOTSalary & ",''," & ClsAttendancePreparationDetails.DataBaseUserRelatedID & ",NULL,getdate(),NULL,0,0,NULL)"
                                    Continue For
                                End If


                                '------------- Absent -------------------
                                If Not ClsAttendTransactions.Find("EmployeeID = '" & ClsEmployees.ID & "' and CONVERT(date,TrnsDatetime,103) = CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103)") Then
                                    ClsAttendancePreparationProjects = New ClsAtt_AttendancePreparationProjects(Page)
                                    If ClsAttendancePreparationDetails.IsVacation = False Then
                                        If clsBranch.DefaultAbsent Then
                                            ClsAttendancePreparationDetails.IsAbsent = True
                                            ClsAttendancePreparationProjects.IsAbsent = True
                                        Else
                                            If clsCompanies.DefaultAttend = True Then
                                                ClsAttendancePreparationDetails.IsAbsent = True
                                                ClsAttendancePreparationProjects.IsAbsent = True
                                            Else
                                                ClsAttendancePreparationDetails.IsAbsent = False
                                                ClsAttendancePreparationProjects.IsAbsent = False
                                            End If
                                        End If
                                    Else
                                        ClsAttendancePreparationDetails.IsAbsent = False
                                        ClsAttendancePreparationProjects.IsAbsent = False
                                    End If
                                    ClsAttendancePreparationDetails.FiscalYearPeriodID = IntFisicalPeriod


                                    cmdString &= " INSERT INTO Att_AttendancePreparationDetails (EmployeeID,GAttendDate,HAttendDate,TotalLate,PermitLate,NotpermitLate,LatPunishment,LeavingType,IsVacation,IsAbsent,Notes,Remarks,RegUserID,RegComputerID,RegDate,CancelDate,FiscalYearPeriodID)"
                                    cmdString &= " values(" & ClsAttendancePreparationDetails.EmployeeID & ",'" & ClsAttendancePreparationDetails.GAttendDate.ToString("yyyy-MM-dd") & "','" & ClsDataAcessLayer.GregToHijri(OperDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy") & "',0,0,0,0,0," & IIf(ClsAttendancePreparationDetails.IsVacation, 1, 0) & "," & IIf(ClsAttendancePreparationDetails.IsAbsent, 1, 0) & ",'',''," & ClsAttendancePreparationDetails.DataBaseUserRelatedID & ",NULL,getdate(),NULL," & ClsAttendancePreparationDetails.FiscalYearPeriodID & ")"
                                    cmdString &= " set @TransID = (select SCOPE_IDENTITY())"


                                    ' overtime new method


                                    If ConfigurationManager.AppSettings("Overtime") = 1 Then
                                        ClsOvertimeReviewDetails.Find("Employeeid = '" & ClsEmployees.ID & "' and CONVERT(date,TransDate,103) = CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103) and canceldate is null order by TransDate")
                                        If ClsOvertimeReviewDetails.ID > 0 Then
                                            ClsAttendancePreparationProjects.Overtime = ClsOvertimeReviewDetails.Overtime
                                        End If
                                    End If


                                    ClsAttendancePreparationProjects.ProjectID = CurrentProject
                                    ClsAttendancePreparationProjects.SalaryPerDay = Amount / NumberOfDays
                                    ClsAttendancePreparationProjects.SalaryPerHour = (Amount / NumberOfDays) / ClsClasses.WorkHoursPerDay
                                    ClsAttendancePreparationProjects.OTSalary = dbOTSalary
                                    ClsAttendancePreparationProjects.HOTSalary = dbHOTSalary

                                    cmdString &= " INSERT INTO Att_AttendancePreparationProjects (TrnsID,ProjectID,Checkin,Checkout,TotalTime,Overtime,HolidayHours,IsVacation,IsAbsent,IsSick,IsLeave,NotpermitLate,AbsentPunishment,SickPunishment,OTFactor,HOTFactor,LeavePunishment,LatPunishment,SalaryPerDay,SalaryPerHour,OTSalary,HOTSalary,Remarks,RegUserID,RegComputerID,RegDate,CancelDate,OOvertime,OHolidayHours,LinkedCS)"
                                    cmdString &= " values (@TransID," & ClsAttendancePreparationProjects.ProjectID & ",NULL,NULL,0," & ClsAttendancePreparationProjects.Overtime & ",0," & IIf(ClsAttendancePreparationProjects.IsVacation, 1, 0) & "," & IIf(ClsAttendancePreparationProjects.IsAbsent, 1, 0) & ",0,0,0,0,0,0,0,0,0," & ClsAttendancePreparationProjects.SalaryPerDay & "," & ClsAttendancePreparationProjects.SalaryPerHour & "," & ClsAttendancePreparationProjects.OTSalary & "," & ClsAttendancePreparationProjects.HOTSalary & ",''," & ClsAttendancePreparationDetails.DataBaseUserRelatedID & ",NULL,getdate(),NULL,0,0,NULL)"
                                Else
                                    '--------------- if has attendance --------------
                                    CurrentProject = ClsAttendTransactions.ProjectID
                                    If ClsAttendTransactions.Status <> 0 Then
                                        If ClsAttendTransactions.Status = 1 Then
                                            ClsAttendancePreparationDetails.IsVacation = True
                                        ElseIf ClsAttendTransactions.Status = -1 Then
                                            ClsAttendancePreparationDetails.IsVacation = False
                                        End If
                                    End If
                                    '===========
                                    If ClsAttendTransactions.TotalHours = 0 And ClsAttendTransactions.TimeIn = Nothing And ClsAttendTransactions.TimeOut = Nothing Then

                                        ClsAttendancePreparationProjects = New ClsAtt_AttendancePreparationProjects(Page)
                                        ClsAttendancePreparationDetails.IsAbsent = IIf(ClsAttendancePreparationDetails.IsVacation = True, False, True)
                                        ClsAttendancePreparationDetails.FiscalYearPeriodID = IntFisicalPeriod
                                        If ClsEmployeeClass.OnNoExit = 1 Then
                                            ClsAttendancePreparationDetails.IsAbsent = False
                                        End If
                                        '=============================
                                        'Edited by: Hassan Kurdi  Date"2021-01-28'
                                        'Start edit
                                        ' ------------- Full Day Excuses ------------------------
                                        Dim ClsEmployeesExcuses As New Clshrs_EmployeesExcuses(Page)
                                        Dim shiftNo As Integer
                                        shiftNo = ClsAttendTransactions.ShiftNo

                                        If (ClsAttendancePreparationDetails.IsAbsent = True) Then
                                            If ClsEmployeesExcuses.Find("EmployeeID = " & ClsEmployees.ID & " AND ExcuseDate = '" & OperDate.ToString("dd/MM/yyyy") & "' AND Shift = " & shiftNo & " AND ExcuseType = 'Full'") Then
                                                ClsAttendancePreparationDetails.IsAbsent = False
                                                ClsAttendancePreparationDetails.Notes = ClsAttendancePreparationDetails.Notes + "Excuse Full Day: " & ClsEmployeesExcuses.Notes
                                            End If
                                        End If

                                        If Not String.IsNullOrEmpty(ClsAttendTransactions.TimeIn) Then
                                            If String.IsNullOrEmpty(ClsAttendTransactions.TimeOut) Then
                                                If ClsEmployeesExcuses.Find("EmployeeID = " & ClsEmployees.ID & " AND ExcuseDate = '" & OperDate.ToString("dd/MM/yyyy") & "' AND Shift = " & shiftNo & " AND ExcuseType = 'OUT'") Then
                                                    'اذن الدخول
                                                    ClsAttendancePreparationDetails.IsAbsent = False
                                                    ClsAttendancePreparationDetails.TotalLate = 0
                                                    ClsAttendancePreparationDetails.Notes = ClsAttendancePreparationDetails.Notes + "Excuse Out: " & ClsEmployeesExcuses.Notes

                                                End If
                                            End If

                                        End If

                                        'End of edit
                                        cmdString &= " INSERT INTO Att_AttendancePreparationDetails (EmployeeID,GAttendDate,HAttendDate,TotalLate,PermitLate,NotpermitLate,LatPunishment,LeavingType,IsVacation,IsAbsent,Notes,Remarks,RegUserID,RegComputerID,RegDate,CancelDate,FiscalYearPeriodID)"
                                        cmdString &= " values(" & ClsAttendancePreparationDetails.EmployeeID & ",'" & ClsAttendancePreparationDetails.GAttendDate.ToString("yyyy-MM-dd") & "','" & ClsDataAcessLayer.GregToHijri(OperDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy") & "',0,0,0,0,0," & IIf(ClsAttendancePreparationDetails.IsVacation, 1, 0) & "," & IIf(ClsAttendancePreparationDetails.IsAbsent, 1, 0) & ",'" & ClsAttendancePreparationDetails.Notes & "',''," & ClsAttendancePreparationDetails.DataBaseUserRelatedID & ",NULL,getdate(),NULL," & ClsAttendancePreparationDetails.FiscalYearPeriodID & ")"
                                        cmdString &= " set @TransID = (select SCOPE_IDENTITY())"

                                        ClsAttendancePreparationProjects.ProjectID = ClsAttendTransactions.ProjectID
                                        ClsAttendancePreparationProjects.SalaryPerDay = Amount / NumberOfDays
                                        ClsAttendancePreparationProjects.SalaryPerHour = (Amount / NumberOfDays) / ClsClasses.WorkHoursPerDay
                                        ClsAttendancePreparationProjects.OTSalary = dbOTSalary
                                        ClsAttendancePreparationProjects.HOTSalary = dbHOTSalary
                                        ClsAttendancePreparationProjects.IsAbsent = ClsAttendancePreparationDetails.IsAbsent

                                        cmdString &= " INSERT INTO Att_AttendancePreparationProjects (TrnsID,ProjectID,Checkin,Checkout,TotalTime,Overtime,HolidayHours,IsVacation,IsAbsent,IsSick,IsLeave,NotpermitLate,AbsentPunishment,SickPunishment,OTFactor,HOTFactor,LeavePunishment,LatPunishment,SalaryPerDay,SalaryPerHour,OTSalary,HOTSalary,Remarks,RegUserID,RegComputerID,RegDate,CancelDate,OOvertime,OHolidayHours,LinkedCS)"
                                        cmdString &= " values (@TransID," & ClsAttendancePreparationProjects.ProjectID & ",NULL,NULL,0,0,0," & IIf(ClsAttendancePreparationProjects.IsVacation, 1, 0) & "," & IIf(ClsAttendancePreparationProjects.IsAbsent, 1, 0) & ",0,0,0,0,0,0,0,0,0," & ClsAttendancePreparationProjects.SalaryPerDay & "," & ClsAttendancePreparationProjects.SalaryPerHour & "," & ClsAttendancePreparationProjects.OTSalary & "," & ClsAttendancePreparationProjects.HOTSalary & ",''," & ClsAttendancePreparationDetails.DataBaseUserRelatedID & ",NULL,getdate(),NULL,0,0,NULL)"
                                    Else

                                        ClsAttendancePreparationDetails.IsAbsent = False
                                        Dim totalLat As Double = 0
                                        If ClsAttendTransactions.DataSet.Tables(0).Rows.Count > 0 Then
                                            For Each Row In ClsAttendTransactions.DataSet.Tables(0).Rows
                                                totalLat += Row("TotalLate")
                                                Dim shiftLate As Double = Row("TotalLate")

                                                '=============================
                                                'Edited by: Hassan Kurdi  Date:"2021-01-28"'
                                                'Start edit
                                                '-------- In Excuses -------------
                                                Dim ClsEmployeesExcuses As New Clshrs_EmployeesExcuses(Page)
                                                Dim shiftNo As Integer = 0
                                                shiftNo = Row("ShiftNo")

                                                If String.IsNullOrEmpty(ClsAttendTransactions.TimeIn) Or shiftLate > 0 Then
                                                    If Not String.IsNullOrEmpty(ClsAttendTransactions.TimeOut) Then
                                                        If ClsEmployeesExcuses.Find("EmployeeID = " & ClsEmployees.ID & " AND ExcuseDate = '" & OperDate.ToString("dd/MM/yyyy") & "' AND Shift = " & shiftNo & " AND ExcuseType = 'IN'") Then
                                                            'اذن الدخول
                                                            totalLat = 0
                                                            ClsAttendancePreparationDetails.Notes = ClsAttendancePreparationDetails.Notes + "Excuse In: " & ClsEmployeesExcuses.Notes

                                                        End If
                                                    End If
                                                Else
                                                    If String.IsNullOrEmpty(ClsAttendTransactions.TimeOut) Then
                                                        If ClsEmployeesExcuses.Find("EmployeeID = " & ClsEmployees.ID & " AND ExcuseDate = '" & OperDate.ToString("dd/MM/yyyy") & "' AND Shift = " & shiftNo & " AND ExcuseType = 'OUT'") Then
                                                            'اذن الخروج
                                                            totalLat = 0
                                                            ClsAttendancePreparationDetails.Notes = ClsAttendancePreparationDetails.Notes + " Excuse Out: " & ClsEmployeesExcuses.Notes

                                                            'End of edit
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If

                                        'ClsAttendancePreparationDetails.TotalLate = ClsAttendTransactions.TotalLate
                                        ClsAttendancePreparationDetails.TotalLate = totalLat
                                        If ClsAttendancePreparationDetails.IsVacation = True Then
                                            ClsAttendancePreparationDetails.TotalLate = 0
                                            ClsAttendancePreparationDetails.PermitLate = 0
                                            ClsAttendancePreparationDetails.NotpermitLate = 0
                                            ClsAttendancePreparationDetails.LatPunishment = 0
                                        Else
                                            '==========================================================
                                            If ClsAttendTransactions.TotalHours = 0 Then

                                                ClsAttendancePreparationDetails.TotalLate = totalLat
                                            End If
                                            '==============================================================





                                            If DailyPermit / 60 >= ClsAttendancePreparationDetails.TotalLate Then
                                                If MonthlyPermit / 60 >= ClsAttendancePreparationDetails.TotalLate Then
                                                    ClsAttendancePreparationDetails.PermitLate = ClsAttendancePreparationDetails.TotalLate
                                                    MonthlyPermit = MonthlyPermit - (ClsAttendancePreparationDetails.PermitLate * 60)
                                                Else
                                                    ClsAttendancePreparationDetails.PermitLate = MonthlyPermit / 60
                                                    MonthlyPermit = 0
                                                End If
                                            Else
                                                If MonthlyPermit >= DailyPermit Then
                                                    ClsAttendancePreparationDetails.PermitLate = DailyPermit / 60
                                                    MonthlyPermit = MonthlyPermit - (ClsAttendancePreparationDetails.PermitLate * 60)
                                                Else
                                                    ClsAttendancePreparationDetails.PermitLate = MonthlyPermit / 60
                                                    MonthlyPermit = 0
                                                End If
                                            End If
                                            ClsAttendancePreparationDetails.NotpermitLate = ClsAttendancePreparationDetails.TotalLate - ClsAttendancePreparationDetails.PermitLate
                                            If ClsAttendancePreparationDetails.NotpermitLate * 60 > 0 And ClsAttendancePreparationDetails.IsVacation = False Then
                                                If ClsClasses.DeductionMethod = 0 Then
                                                    ClsClassDelay.Find("ClassID = " & ClsClasses.ID & " and " & ClsAttendancePreparationDetails.NotpermitLate * 60 & " between FromMin and ToMin")
                                                    Dim DT As Data.DataTable = ClsClassDelay.DataSet.Tables(0)
                                                    If DT.Rows.Count > 0 Then
                                                        If ClsClasses.PunishementCalc = 1 Then
                                                            ClsAttendancePreparationDetails.LatPunishment += (Amount / NumberOfDays) * ClsClassDelay.PunishPCT / 100
                                                        Else
                                                            ClsAttendancePreparationDetails.LatPunishment += ((Amount / NumberOfDays / ClsClasses.WorkHoursPerDay) * ClsAttendancePreparationDetails.NotpermitLate) * ClsClassDelay.PunishPCT / 100
                                                        End If
                                                    End If
                                                Else
                                                    ClsClassDelay.Find("ClassID = " & ClsClasses.ID & " and " & ClsAttendancePreparationDetails.TotalLate * 60 & " between FromMin and ToMin")
                                                    Dim DT As Data.DataTable = ClsClassDelay.DataSet.Tables(0)
                                                    If DT.Rows.Count > 0 Then
                                                        If ClsClasses.PunishementCalc = 1 Then
                                                            ClsAttendancePreparationDetails.LatPunishment += (Amount / NumberOfDays) * ClsClassDelay.PunishPCT / 100
                                                        Else
                                                            ClsAttendancePreparationDetails.LatPunishment += ((Amount / NumberOfDays / ClsClasses.WorkHoursPerDay) * ClsAttendancePreparationDetails.TotalLate) * ClsClassDelay.PunishPCT / 100
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                        '=================

                                        ClsAttendancePreparationDetails.FiscalYearPeriodID = IntFisicalPeriod

                                        cmdString &= " INSERT INTO Att_AttendancePreparationDetails (EmployeeID,GAttendDate,HAttendDate,TotalLate,PermitLate,NotpermitLate,LatPunishment,LeavingType,IsVacation,IsAbsent,Notes,Remarks,RegUserID,RegComputerID,RegDate,CancelDate,FiscalYearPeriodID)"
                                        cmdString &= " values(" & ClsAttendancePreparationDetails.EmployeeID & ",'" & ClsAttendancePreparationDetails.GAttendDate.ToString("yyyy-MM-dd") & "','" & ClsDataAcessLayer.GregToHijri(OperDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy") & "'," & ClsAttendancePreparationDetails.TotalLate & "," & ClsAttendancePreparationDetails.PermitLate & "," & ClsAttendancePreparationDetails.NotpermitLate & "," & ClsAttendancePreparationDetails.LatPunishment & ",0," & IIf(ClsAttendancePreparationDetails.IsVacation, 1, 0) & "," & IIf(ClsAttendancePreparationDetails.IsAbsent, 1, 0) & ",'" & ClsAttendancePreparationDetails.Notes & "',''," & ClsAttendancePreparationDetails.DataBaseUserRelatedID & ",NULL,getdate(),NULL," & ClsAttendancePreparationDetails.FiscalYearPeriodID & ")"
                                        cmdString &= " set @TransID = (select SCOPE_IDENTITY())"

                                        EmployeetrnsDt = DailyAttendanceTrnsSlice(ClsAttendTransactions.DataSet)
                                        For i As Integer = 0 To EmployeetrnsDt.Rows.Count - 1
                                            ClsAttendancePreparationProjects = New ClsAtt_AttendancePreparationProjects(Page)
                                            ClsAttendancePreparationProjects.ProjectID = Convert.ToInt32(EmployeetrnsDt.Rows(i)("ProjectID").ToString())
                                            ClsAttendancePreparationProjects.TotalTime = Convert.ToDouble(EmployeetrnsDt.Rows(i)("TotalHours"))

                                            'Edit By: Hassan Kurdi
                                            'Date: 2021-03-14
                                            'Before editing the code insert Checkin and Checkout fingerprint only if two of them has a value
                                            'Now if one of the fingerprint has a value it will be inserted

                                            Dim chechIn As String
                                            If EmployeetrnsDt.Rows(i)("FromHoure").ToString() <> "" Then
                                                ClsAttendancePreparationProjects.Checkin = Convert.ToDateTime(OperDate.ToString("dd/MM/yyyy") & " " & CheckTime(EmployeetrnsDt.Rows(i)("FromHoure").ToString()))
                                                chechIn = "'" & ClsAttendancePreparationProjects.Checkin.ToString("yyyy-MM-dd HH:mm:ss") & "'"
                                            Else
                                                chechIn = "Null"
                                            End If

                                            Dim chechOut As String
                                            If EmployeetrnsDt.Rows(i)("ToHoure").ToString() <> "" Then
                                                ClsAttendancePreparationProjects.Checkout = Convert.ToDateTime(OperDate.ToString("dd/MM/yyyy") & " " & CheckTime(EmployeetrnsDt.Rows(i)("ToHoure").ToString()))
                                                chechOut = "'" & ClsAttendancePreparationProjects.Checkout.ToString("yyyy-MM-dd HH:mm:ss") & "'"
                                            Else
                                                chechOut = "Null"
                                            End If
                                            If EmployeetrnsDt.Rows(i)("FromHoure").ToString() <> "" And EmployeetrnsDt.Rows(i)("ToHoure").ToString() <> "" Then
                                                Dim SFin As New DateTime
                                                Dim SFout As New DateTime
                                                SFin = Convert.ToDateTime(OperDate.ToString("dd/MM/yyyy") & " " & Convert.ToDateTime(CheckTime(EmployeetrnsDt.Rows(i)("FromHoure").ToString())).ToString("HH:mm"))
                                                SFout = Convert.ToDateTime(OperDate.ToString("dd/MM/yyyy") & " " & Convert.ToDateTime(CheckTime(EmployeetrnsDt.Rows(i)("ToHoure").ToString())).ToString("HH:mm"))
                                                If SFin >= SFout Then
                                                    SFout = SFout.AddDays(1)
                                                    ClsAttendancePreparationProjects.Checkout = ClsAttendancePreparationProjects.Checkout.AddDays(1)
                                                End If
                                            End If

                                            If (ClsClasses.PunishementCalc = 0 And (chechIn = "Null" Or chechOut = "Null")) Then
                                                ClsAttendancePreparationProjects.IsAbsent = 1
                                                cmdString &= "UPDATE Att_AttendancePreparationDetails SET IsAbsent = 1 WHERE ID = @TransID"
                                            End If

                                            If ClsAttendancePreparationDetails.IsVacation = True Then
                                                ClsAttendancePreparationProjects.Overtime = 0
                                                ClsAttendancePreparationProjects.HolidayHours = Convert.ToDouble(EmployeetrnsDt.Rows(i)("TotalHours"))
                                            Else
                                                ClsAttendancePreparationProjects.HolidayHours = 0
                                                ClsAttendancePreparationProjects.Overtime = Convert.ToDouble(EmployeetrnsDt.Rows(i)("Overtime"))
                                            End If
                                            ClsAttendancePreparationProjects.SalaryPerDay = Amount / NumberOfDays
                                            ClsAttendancePreparationProjects.SalaryPerHour = (Amount / NumberOfDays) / ClsClasses.WorkHoursPerDay
                                            ClsAttendancePreparationProjects.OTSalary = dbOTSalary
                                            ClsAttendancePreparationProjects.HOTSalary = dbHOTSalary

                                            cmdString &= " INSERT INTO Att_AttendancePreparationProjects (TrnsID,ProjectID,Checkin,Checkout,TotalTime,Overtime,HolidayHours,IsVacation,IsAbsent,IsSick,IsLeave,NotpermitLate,AbsentPunishment,SickPunishment,OTFactor,HOTFactor,LeavePunishment,LatPunishment,SalaryPerDay,SalaryPerHour,OTSalary,HOTSalary,Remarks,RegUserID,RegComputerID,RegDate,CancelDate,OOvertime,OHolidayHours,LinkedCS)"
                                            'cmdString &= " values (@TransID," & ClsAttendancePreparationProjects.ProjectID & ",'" & ClsAttendancePreparationProjects.Checkin.ToString("yyyy-MM-dd HH:mm:ss") & "','" & ClsAttendancePreparationProjects.Checkout.ToString("yyyy-MM-dd HH:mm:ss") & "'," & ClsAttendancePreparationProjects.TotalTime & "," & ClsAttendancePreparationProjects.Overtime & "," & ClsAttendancePreparationProjects.HolidayHours & "," & IIf(ClsAttendancePreparationProjects.IsVacation, 1, 0) & "," & IIf(ClsAttendancePreparationProjects.IsAbsent, 1, 0) & ",0,0,0,0,0,0,0,0,0," & ClsAttendancePreparationProjects.SalaryPerDay & "," & ClsAttendancePreparationProjects.SalaryPerHour & "," & ClsAttendancePreparationProjects.OTSalary & "," & ClsAttendancePreparationProjects.HOTSalary & ",''," & ClsAttendancePreparationDetails.DataBaseUserRelatedID & ",NULL,getdate(),NULL," & ClsAttendancePreparationProjects.Overtime & "," & ClsAttendancePreparationProjects.HolidayHours & ",NULL)"
                                            cmdString &= " values (@TransID," & ClsAttendancePreparationProjects.ProjectID & "," & chechIn & "," & chechOut & "," & ClsAttendancePreparationProjects.TotalTime & "," & ClsAttendancePreparationProjects.Overtime & "," & ClsAttendancePreparationProjects.HolidayHours & "," & IIf(ClsAttendancePreparationProjects.IsVacation, 1, 0) & "," & IIf(ClsAttendancePreparationProjects.IsAbsent, 1, 0) & ",0,0,0,0,0,0,0,0,0," & ClsAttendancePreparationProjects.SalaryPerDay & "," & ClsAttendancePreparationProjects.SalaryPerHour & "," & ClsAttendancePreparationProjects.OTSalary & "," & ClsAttendancePreparationProjects.HOTSalary & ",''," & ClsAttendancePreparationDetails.DataBaseUserRelatedID & ",NULL,getdate(),NULL," & ClsAttendancePreparationProjects.Overtime & "," & ClsAttendancePreparationProjects.HolidayHours & ",NULL)"

                                            'End of date
                                        Next i
                                    End If
                                End If
                            End If
                        End If
                    Next CounDays


                    If cmdString <> "" Then

                        Dim mSqlCommand As New SqlClient.SqlCommand
                        mSqlCommand.Connection = New Data.SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                        mSqlCommand.CommandType = Data.CommandType.Text
                        mSqlCommand.CommandText = "Declare @TransID Int; " & vbNewLine & cmdString & "; set dateformat dmy ;exec setWeekEndAbsent " & EmployeeID & ",'" & FromDate.ToString("dd/MM/yyyy") & "','" & ToDate.ToString("dd/MM/yyyy") & "'"
                        mSqlCommand.Connection.Open()
                        mSqlCommand.ExecuteNonQuery()
                        mSqlCommand.Connection.Close()
                    End If
                End If


            Next EmpCnt
            Return True
        Catch ex As Exception
            Return True
        End Try
    End Function
    Private Function CheckDay(ByVal Day As Int32, ByVal ClassID As Int32, ByVal EmployeeID As Int32, ByVal OperDate As DateTime) As String
        Dim rslt As String
        Dim ClsClassCalander As New Clshrs_EmployeesClassCalander(Page)
        Dim ClsClassCalenderSet As New Clshrs_EmployeesClassesCalenderSet(Page)
        If (ClsClassCalander.Find("EmployeeClassID = " & ClassID & " and CONVERT(date,FromTime,103) = CONVERT(date,'" & OperDate.AddDays(Day).ToString("dd/MM/yyyy") & "',103)") = True) Then
            If ClsClassCalander.nonWorkingTime = True Then
                rslt = "1"
            Else
                rslt = "0"
            End If
        ElseIf (ClsClassCalenderSet.Find("EmployeeClassID = " & ClassID & " and DayNumber = " & RetDayNumber(OperDate.AddDays(Day))) = True) Then
            If ClsClassCalenderSet.NonWorkingTime = True Then
                rslt = "1"
            Else
                rslt = "0"
            End If
        Else
            rslt = "-1"
        End If
        Dim ClsAttendTransactions As New ClsAtt_AttendTransactions(Page)
        If ClsAttendTransactions.Find("EmployeeID = '" & EmployeeID & "' and CONVERT(date,TrnsDatetime,103) = CONVERT(date,'" & OperDate.AddDays(Day).ToString("dd/MM/yyyy") & "',103)") Then
            Dim ClsClasses As New Clshrs_EmployeeClasses(Page)
            ClsClasses.Find("ID = " & ClassID)
            If ClsAttendTransactions.TotalHours > 0 Then
                rslt = rslt & ",1"
            Else
                If Not String.IsNullOrEmpty(ClsAttendTransactions.TimeIn) And ClsClasses.OnNoExit = 1 Then
                    rslt = rslt & ",1"
                Else
                    rslt = rslt & ",0"
                End If
            End If
        Else
            rslt = rslt & ",-1"
        End If
        Return rslt
    End Function
    Private Function CheckTime(ByVal mTime As String) As String
        If mTime.Contains("ص") Then
            mTime = mTime.Replace("ص", "AM")
        ElseIf mTime.Contains("م") Then
            mTime = mTime.Replace("م", "PM")
        End If
        Return mTime
    End Function
    Private Function RetDayNumber(ByVal TrnsDate As DateTime) As Integer
        Dim DayNumber As Integer = 0
        Dim Dayidx As Integer = TrnsDate.DayOfWeek
        If Dayidx = 0 Then
            DayNumber = 1
        ElseIf Dayidx = 1 Then
            DayNumber = 2
        ElseIf Dayidx = 2 Then
            DayNumber = 3
        ElseIf Dayidx = 3 Then
            DayNumber = 4
        ElseIf Dayidx = 4 Then
            DayNumber = 5
        ElseIf Dayidx = 5 Then
            DayNumber = 6
        ElseIf Dayidx = 6 Then
            DayNumber = 7
        End If
        Return DayNumber
    End Function
    Private Function DailyAttendanceTrnsSlice(ByVal TrnDataset As Data.DataSet) As Data.DataTable
        Dim dt As New Data.DataTable()
        dt.Columns.Add("FromHoure", GetType(String))
        dt.Columns.Add("ToHoure", GetType(String))
        dt.Columns.Add("TotalHours", GetType(Double))
        dt.Columns.Add("ProjectID", GetType(Int32))
        dt.Columns.Add("Overtime", GetType(Double))
        dt.Columns.Add("TotalLat", GetType(Double))
        dt.Columns.Add("Status", GetType(Int32))
        dt.Columns.Add("RefTrnsID", GetType(Int32))
        Dim FromHoure As String = ""
        Dim ToHoure As String = ""
        Dim TotalHoure As Double = 0
        Dim OvertimeHours As Double = 0
        Dim LatHours As Double = 0
        Dim ProjectID As Int32 = 0
        Dim Status As Int32 = 0
        Dim RefTrns As Int32 = 0
        Dim PrevProjectID As Int32 = 0
        For i As Integer = 0 To TrnDataset.Tables(0).Rows.Count - 1
            If Not String.IsNullOrEmpty(TrnDataset.Tables(0).Rows(i)("TimeIn").ToString()) Then
                FromHoure = Convert.ToDateTime(TrnDataset.Tables(0).Rows(i)("TimeIn")).TimeOfDay.ToString()
            Else
                FromHoure = Nothing
            End If
            If Not String.IsNullOrEmpty(TrnDataset.Tables(0).Rows(i)("TimeOut").ToString()) Then
                ToHoure = Convert.ToDateTime(TrnDataset.Tables(0).Rows(i)("TimeOut")).TimeOfDay.ToString()
            Else
                ToHoure = Nothing
            End If
            TotalHoure = Convert.ToDouble(TrnDataset.Tables(0).Rows(i)("TotalHours"))
            ProjectID = Convert.ToInt32(TrnDataset.Tables(0).Rows(i)("ProjectID"))
            Status = Convert.ToInt32(TrnDataset.Tables(0).Rows(i)("Status"))
            Try
                RefTrns = Convert.ToInt32(TrnDataset.Tables(0).Rows(i)("RegComputerID"))
            Catch ex As Exception
                RefTrns = 0
            End Try

            Try
                OvertimeHours = Convert.ToDouble(TrnDataset.Tables(0).Rows(i)("Overtime"))
            Catch ex As Exception
                OvertimeHours = 0
            End Try
            Try
                LatHours = Convert.ToDouble(TrnDataset.Tables(0).Rows(i)("TotalLate"))
            Catch ex As Exception
                LatHours = 0
            End Try
            If PrevProjectID <> ProjectID Then
                dt.Rows.Add(New Object() {FromHoure, ToHoure, TotalHoure, ProjectID, OvertimeHours, LatHours, Status, RefTrns})
                PrevProjectID = ProjectID
            End If
        Next i
        Return dt
    End Function
    Private Function TotalDailyAttendance(ByVal TrnDataset As Data.DataSet) As Double
        Dim TotalHoure As Double = 0
        Dim ProjectID As Int32 = 0
        Dim PrevProjectID As Int32 = 0
        For i As Integer = 0 To TrnDataset.Tables(0).Rows.Count - 1
            ProjectID = Convert.ToInt32(TrnDataset.Tables(0).Rows(i)("ProjectID"))
            If PrevProjectID <> ProjectID Then
                TotalHoure += Convert.ToDouble(TrnDataset.Tables(0).Rows(i)("TotalHours"))
                PrevProjectID = ProjectID
            End If
        Next i
        Return TotalHoure
    End Function
#End Region

#Region "Public Shared Function"

    Public Sub Create_Wait(ByVal page As System.Web.UI.Page)
        page.Response.Write("<div id='div_Wait' align='center' style='font-size: 16pt; text-decoration: blink; color: #666666; position: absolute; top: 0px; width: 100%;'>")
        page.Response.Write("   <br />")
        page.Response.Write("   <br />")
        page.Response.Write("   <asp:Label ID='lblmsg'></asp:Label>")
        page.Response.Write("   <br />")
        page.Response.Write("   <asp:Label ID='lblmsg2'></asp:Label>")
        page.Response.Write("   <br />")
        page.Response.Write("   <img src='" & "../../Pages/HR/Img/waiting.gif" & "'>")
        page.Response.Write("</div>")
        page.Response.Write("<script language=javascript>")
        page.Response.Write("   var div_Wait= window.document.all.item(""div_Wait"");")
        page.Response.Write("   var lbl_msg= window.document.all.item(""lblmsg"");")
        page.Response.Write("   var lbl_msg2= window.document.all.item(""lblmsg2"");")
        page.Response.Write("   function Stop_Wait(){div_Wait.style.visibility = ""hidden"";}")
        page.Response.Write("</script>")
        page.Response.Flush()
    End Sub

    Public Sub Start_Wait(ByVal page As System.Web.UI.Page, ByVal Id As String, ByVal msg As String, ByVal msg2 As String)
        page.Response.Write("<script language=javascript>")
        page.Response.Write("   function Start_Wait_" & Id & "(){lbl_msg.innerText=""" & msg & """; lbl_msg2.innerText=""" & msg2 & """; div_Wait.style.visibility = ""visible"";}")
        page.Response.Write("   Start_Wait_" & Id & "();")
        page.Response.Write("</script>")
        page.Response.Flush()
    End Sub

    Public Sub Stop_Wait(ByVal page As System.Web.UI.Page)
        page.Response.Write("<script language=javascript>")
        page.Response.Write("   Stop_Wait();")
        page.Response.Write("</script>")
        page.Response.Flush()
    End Sub

    Public Shared Function CheckBranchPermission(ByVal intDeptID As Integer) As String
        Try
            Dim str As String = ""
            Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
            Dim mCompanyID As Integer = CType(HttpContext.Current.Session("CompanyID"), Integer)
            Dim mUserID As Integer = CType(HttpContext.Current.Session("UserID"), Integer)
            Dim BranchesIDs As String = GetRelatedDept(intDeptID)
            BranchesIDs = IIf(BranchesIDs = "", "0", BranchesIDs)

            Dim StrSelectCommand As String =
                    "Declare @Branches as nvarchar(max)='';" &
                    "Select  @Branches = @Branches + N',' + Cast(B.ID As varchar(200)) " &
                    "From sys_Branches B Inner Join sys_CompaniesBranches CB ON CB.BrancheID=B.ID Where B.ID IN (" & BranchesIDs & ")  And CB.CompanyID=" & mCompanyID & " And CB.UserID=" & mUserID & " AND CanView= 1" &
                    "Select @Branches  = STUFF(@Branches,1,1,''); " &
                    "Select IsNull(@Branches,'')"

            str = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, Data.CommandType.Text, StrSelectCommand)

            Return IIf(str = "", "0", str)

        Catch ex As Exception
            Return "0"
        End Try
    End Function
    Public Shared Function GetRelatedDept(ByVal intDeptID As Integer) As String
        Dim StrSelectCommand As String
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        Dim dsBranches As New Data.DataSet

        Dim str As String = IIf(intDeptID = 0, "", " And DB.DepartmentID = " & intDeptID)
        Try
            StrSelectCommand = " Declare @Branches as nvarchar(max)=''; " &
                                "Select  @Branches = @Branches + N',' + Cast(B.ID As varchar(200)) " &
                                "From sys_DepartmentsBranches DB Inner Join sys_Branches B On DB.BranchID = B.ID Where  DB.Checked  = 1 " & str & " And B.CancelDate Is Null; " &
                                "Select @Branches  = STUFF(@Branches,1,1,''); " &
                                "Select IsNull(@Branches,'')"

            Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, Data.CommandType.Text, StrSelectCommand)

        Catch ex As Exception

        End Try
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function GetRelatedDepartment(ByVal strDeptID As String) As String
        Try

            Dim dsBranches As New Data.DataSet
            Dim StrSelectCommand As String
            Dim strResultBranches As String = CheckBranchPermission(strDeptID)
            Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
            Dim Lage As String = CType(HttpContext.Current.Session("Lage"), String)
            Dim mCompanyID As Integer = CType(HttpContext.Current.Session("CompanyID"), Integer)
            Dim mUserID As Integer = CType(HttpContext.Current.Session("UserID"), Integer)
            Dim strFieldName As String = IIf(Lage = "0", "EngName", "ArbName")
            Dim str As String = String.Empty

            StrSelectCommand = " Select B.ID, B." & strFieldName & " From sys_Branches B Inner Join sys_CompaniesBranches CB ON CB.BrancheID=B.ID Where B.ID IN (" & IIf(strResultBranches = "", 0, strResultBranches) & ") And CB.CompanyID=" & mCompanyID & " And CB.UserID=" & mUserID & " AND CanView= 1"

            dsBranches = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnStr, Data.CommandType.Text, StrSelectCommand)

            If dsBranches.Tables(0).Rows.Count > 0 Then

                str = "<select style='border: 1px solid rgb(204, 204, 204); width: 100%; height: 20px; color: black; font-family: Tahoma; font-size: 8pt; font-weight: normal;' id='UltraWebTab1__ctl0_ddlBranche' name='UltraWebTab1$_ctl0$ddlBranche'>"

                For I As Integer = 0 To dsBranches.Tables(0).Rows.Count - 1
                    str &= "<option value=" & dsBranches.Tables(0).Rows(I).Item("ID") & ">" & dsBranches.Tables(0).Rows(I).Item(strFieldName) & "</option>"
                Next

                str &= "</select>"

                Return str
            Else
                Return "<select style='border: 1px solid rgb(204, 204, 204); width: 100%; height: 20px; color: black; font-family: Tahoma; font-size: 8pt; font-weight: normal;' id='UltraWebTab1__ctl0_ddlBranche' name='UltraWebTab1$_ctl0$ddlBranche'></select>"
            End If

        Catch ex As Exception

        End Try
    End Function

#End Region

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        GetData(True)
    End Sub

    Protected Sub DropDownList_Project_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownList_Project.SelectedIndexChanged
        GetData(True)
    End Sub

    Protected Sub ddlBranche_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBranche.SelectedIndexChanged
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
        Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        Dim strbranchfilter As String = ""
        strbranchfilter = " and BranchID = " & ddlBranche.SelectedValue
        clsProjects.GetDropDownList(DropDownList_Project, True, "isnull(BranchID,0) = 0 or (IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and EndDate >= (select ToDate from sys_FiscalYearsPeriods where ID = " & DdlPeriods.SelectedValue & ")" & strbranchfilter & ")")
        DropDownList_Project.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Employee Has Not Attendance Transactions]/ [ جميع الموظفين بدون إدخالات حضور وانصراف]")
        DropDownList_Project.Items.Insert(0, New System.Web.UI.WebControls.ListItem(ClsNavigationHandler.SetLanguage(Page, "[All Employee Has Attendance Transactions]/ [ جميع الموظفين مع إدخالات حضور وانصراف]"), -1))
        DropDownList_Project.Items.Insert(0, New System.Web.UI.WebControls.ListItem(ClsNavigationHandler.SetLanguage(Page, "[All Employees]/ [ جميع الموظفين]"), -2))
        GetData(True)
    End Sub
    Protected Sub ImageButton_Export_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_Export.Click
        Dim DT As New DataTable
        DT = GetData(False).Tables(0)
        If DT.Columns.Count > 0 Then
            DT.Columns.RemoveAt(0)
            Dim dgGrid As New GridView()
            dgGrid.DataSource = DT
            dgGrid.DataBind()
            Response.ClearContent()
            Response.AddHeader("content-disposition", "attachment;filename=Data" & DateTime.Now.Ticks.ToString() & ".xls")
            Response.ContentType = "application/vnd.ms-excel"
            Response.ContentEncoding = System.Text.Encoding.Unicode
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())
            Dim tw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            dgGrid.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        End If
    End Sub
End Class
