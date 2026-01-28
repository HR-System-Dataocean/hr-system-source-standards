Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmSalaryProtectionSystem
    Inherits MainPage

#Region "Protected Sub"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(Page)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
        Dim IntSelectedPeriod As Integer = 0
        Dim IntModuleId As Integer = GetModuleID("frmPrepareSalaries")
        Dim clsBranch As New Clssys_Branches(Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
        If ClsObjects.Find(" Code='" & ClsEmployee.Table.Trim & "'") Then
            If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
            End If
            Dim clsSponsor As New Clshrs_Sponsors(Page)
            If ClsObjects.Find(" Code='" & clsSponsor.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TextBox_Sponsor.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & TextBox_Sponsor.ClientID & "'"
                    WebImageButton_Sponsor.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
        End If
        If Not IsPostBack Then
            ddlDepartment.Attributes.Add("OnChange", "ddlDepartment_Change()")
            Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
            Dim ClsBanks As New ClsBasicFiles(Me.Page, "sys_Banks")
            ClsBanks.GetDropDownList(DropDownList_Bank, True)
            DropDownList_Bank.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Banks]/ [ جميع البنوك]")
            ClsDepartment.GetDropDownList(ddlDepartment, True)
            ddlDepartment.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Departments]/ [ جميع الإدارات]")
            clsBranch.GetDropDownList(ddlBranche, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
            ddlBranche.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Branches]/ [ جميع الفروع]")
           

            ClsFisicalYearsPeriods.GetDropDownList(DdlPeriods, IntModuleId, True, "")
            IntSelectedPeriod = ClsFisicalYearsPeriods.GetLastOpenedFiscalPieriod(IntModuleId)
            UwgSearchEmployees.Columns.FromKey("FullName").CellStyle.HorizontalAlign = CInt(ClsNavigationHandler.SetLanguage(Page, "1/3"))
            DdlPeriods.SelectedIndex = 0
            lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
            Page.Session.Add("Lage", lblLage.Text)
            Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)
        End If
    End Sub
    Protected Sub DdlPeriods_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlPeriods.SelectedIndexChanged
        GetData()
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetData()
    End Sub
    Protected Sub Button_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Prepare.Command, ImageButton_Prepare.Command
        Try
            Select Case DirectCast(e, System.Web.UI.WebControls.CommandEventArgs).CommandArgument
                Case "Prepare"
                    Dim dt As New DataTable()
                    If LoadData(dt) Then
                        Response.Clear()
                        Response.AddHeader("content-disposition", "attachment;filename=MyFiles.xls")
                        Response.ContentType = "application/vnd.ms-excel"
                        Response.ContentEncoding = System.Text.Encoding.Unicode
                        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())
                        Dim tw As New System.IO.StringWriter()
                        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
                        Dim dgGrid As New DataGrid()
                        For Each row As DataRow In dt.Rows
                            For i As Integer = 0 To dt.Columns.Count - 1
                                If dt.Columns(i).ColumnName = "Column1" Then
                                    row(i) = "إيداع راتب"
                                ElseIf dt.Columns(i).ColumnName = "Column2" Then
                                    row(i) = "0"
                                ElseIf dt.Columns(i).ColumnName = "Column3" Then
                                    row(i) = "0"
                                End If
                            Next
                        Next
                        For Each column As DataColumn In dt.Columns
                            If column.ColumnName = "Column1" Then
                                column.ColumnName = "وصف المدفوعات"
                            ElseIf column.ColumnName = "Column2" Then
                                column.ColumnName = "مرجع العملية من البنك"
                            ElseIf column.ColumnName = "Column3" Then
                                column.ColumnName = "حالة العملية من البنك"
                            End If
                        Next
                        dgGrid.DataSource = dt
                        dgGrid.DataBind()
                        dgGrid.RenderControl(hw)
                        Response.Write(tw.ToString())
                        Response.End()
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Private Function"

    Private Function LoadData(ByRef dt As DataTable, Optional ByVal command As String = "") As Boolean
        Try
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)
            Dim BranchID As Integer = ddlBranche.SelectedValue
            Dim DepartmentID As Integer = ddlDepartment.SelectedValue
            Dim BankID As Integer = DropDownList_Bank.SelectedValue
            Dim BankAccountType As String = ddlBankAccountType.SelectedValue
            Dim clsbanks As New Clssys_Banks(Page)
            If DdlPeriods.SelectedIndex = 0 Then
                Return False
            End If
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


            Dim strFilter As String = " And isnull(e.LaborOfficeNo,'') like '%" & txtCode.Text & "%' And e.SponsorID in (select ID from hrs_Sponsors where Code like '%" & TextBox_Sponsor.Text & "%') "
            If BranchID > 0 Then
                strFilter &= " And e.BranchID = " & BranchID
            Else
                Dim mUserID As Integer = ClsEmployee.DataBaseUserRelatedID
                Dim strbrnches As String
                Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, Data.CommandType.Text, "select BrancheID  FROM sys_CompaniesBranches where sys_CompaniesBranches.CanView = 1 and UserID =" & mUserID)
                For index = 0 To ds.Tables(0).Rows.Count - 1
                    strbrnches &= ds.Tables(0).Rows(index)("BrancheID").ToString() + ","
                Next
                If strbrnches.EndsWith(",") Then
                    strbrnches = strbrnches.Remove(strbrnches.Length - 1)
                End If
                strFilter &= " And e.BranchID in(" & strbrnches & ")"
            End If



            If DepartmentID > 0 Then
                strFilter &= " And e.DepartmentID = " & DepartmentID
            End If
            If BankID > 0 Then
                clsbanks.Find("ID = " & BankID)

                strFilter &= " And e.BankID = " & BankID
            End If
            If BankAccountType <> "0" Then
                strFilter &= " And e.BankAccountType = '" & BankAccountType & "'"
            End If
            Dim strCommand As String = ""
            If ConfigurationManager.AppSettings("WPSystem") = 0 Then
                strCommand = "Set DateFormat DMY; select B.Code AS 'رمز البنك',Char(39) + isnull(e.BankAccountNumber,'') AS 'رقم الحســـاب',dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & ") AS 'الصافي',e.Code as 'الرقم الوظيفي',dbo.fn_GetEmpName(e.Code,0) AS 'اســــم الموظف',isnull(e.SSnNo,'') AS 'الهوية',dbo.fn_GetTotalBasic(e.ID," & ClsFisicalPeriods.ID & ") AS 'الأساسي',dbo.fn_GetTotalHousing(e.ID," & ClsFisicalPeriods.ID & ") AS 'بدل السكن الشهري',dbo.fn_GetTotalOthers(e.ID," & ClsFisicalPeriods.ID & ") AS 'مدفوعات اخرى للعامل',dbo.fn_GetTotalDeductions(e.ID," & ClsFisicalPeriods.ID & ") AS 'الخصومات علي العامل',isnull((select ArbName from sys_Branches where ID = e.BranchID),'') AS 'الفرع' from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID))"
            ElseIf ConfigurationManager.AppSettings("WPSystem") = 1 Then
                strCommand = "Set DateFormat DMY; select isnull(e.SSnNo,'') AS 'الهوية',dbo.fn_GetEmpName(e.Code,0) AS 'اســــم الموظف',isnull(e.BankAccountNumber,'') AS 'رقم الحســـاب',B.Code AS 'رمز البنك',dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & ") AS 'الصافي',dbo.fn_GetTotalBasic(e.ID," & ClsFisicalPeriods.ID & ") AS 'الأساسي',dbo.fn_GetTotalHousing(e.ID," & ClsFisicalPeriods.ID & ") AS 'بدل السكن الشهري',dbo.fn_GetTotalOthers(e.ID," & ClsFisicalPeriods.ID & ") AS 'مدفوعات اخرى للعامل',dbo.fn_GetTotalDeductions(e.ID," & ClsFisicalPeriods.ID & ") AS 'الخصومات علي العامل','' العنوان,'وصف المدفوعات','مرجع العملية من البنك','حالة العملية من البنك',e.Code as 'الرقم الوظيفي',isnull((select ArbName from sys_Branches where ID = e.BranchID),'') AS 'الفرع',isnull((select ArbName from fcs_CostCenters where ID = e.Cost1),'') AS 'مركز تكلفة 1',isnull((select ArbName from fcs_CostCenters where ID = e.Cost2),'') AS 'مركز تكلفة 2',isnull(e.LaborOfficeNo,'') AS 'رقم المنشأة' from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID))"
            ElseIf ConfigurationManager.AppSettings("WPSystem") = 3 Then
                strCommand = "Set DateFormat DMY; select e.Code as 'كود الموظف',dbo.fn_GetEmpName(e.Code,0) AS 'اسم الموظف',isnull(e.SSnNo,'') AS 'رقم الهوية',Char(39) + isnull(e.BankAccountNumber,'') AS 'رقم الحســـاب',B.Code AS 'رمز البنك',dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & ") AS 'إجمالى الراتب',dbo.fn_GetTotalBasic(e.ID," & ClsFisicalPeriods.ID & ") AS 'الأساسي',dbo.fn_GetTotalHousing(e.ID," & ClsFisicalPeriods.ID & ") AS 'بدل السكن ',dbo.fn_GetTotalOthers(e.ID," & ClsFisicalPeriods.ID & ") AS 'مستحقات أخرى',dbo.fn_GetTotalDeductions(e.ID," & ClsFisicalPeriods.ID & ") AS 'خصومات','Salary' as 'تفاصيل الدفع' from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID))"
            ElseIf ConfigurationManager.AppSettings("WPSystem") = 4 Then
                strCommand = "Set DateFormat DMY;select isnull(e.SSnNo,'') AS 'Employee Iqama',isnull(e.BankAccountNumber,'') AS 'Employee Account No' ,dbo.fn_GetEmpName(e.Code,0) AS 'Employee Name',B.Code AS 'Bank Code',dbo.fn_GetBasicSalary2(e.ID," & ClsFisicalPeriods.ID & ")  'Basic Salary',dbo.fn_GetTotalHousing(e.ID," & ClsFisicalPeriods.ID & ") AS 'Housing Allowance ',dbo.fn_GetTotalOthers(e.ID," & ClsFisicalPeriods.ID & ") AS 'Other Allowance',dbo.fn_GetTotalDeductions(e.ID," & ClsFisicalPeriods.ID & ") AS 'Deduction',dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & ") AS 'Total Amount' from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID))"
            ElseIf ConfigurationManager.AppSettings("WPSystem") = 5 Then
                strCommand = "Set DateFormat DMY; select e.code as 'Employee Code' , B.Code AS 'Bank', isnull(e.BankAccountNumber,'') AS 'Account Number',dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & ") AS 'Total Salary','' as 'Comments',dbo.fn_GetEmpName(e.Code,0) AS 'EmployeeName',isnull(e.SSnNo,'') AS 'National ID /Iqama ID','Jeddah' As 'Employee Address' ,dbo.fn_GetTotalBasic(e.ID," & ClsFisicalPeriods.ID & ") AS 'Basic Salary',dbo.fn_GetTotalHousing(e.ID," & ClsFisicalPeriods.ID & ") AS 'Housing Allowance',dbo.fn_GetTotalOthers(e.ID," & ClsFisicalPeriods.ID & ") AS 'Other Earning',dbo.fn_GetTotalDeductions(e.ID," & ClsFisicalPeriods.ID & ") AS 'Deductions ' from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID))"
            ElseIf ConfigurationManager.AppSettings("WPSystem") = 6 Then
                strCommand = "Set DateFormat DMY;select e.code as 'Employee Code',isnull(e.SSnNo,'') AS 'Employee Iqama',B.Code AS 'Bank Code',isnull(e.BankAccountNumber,'') AS 'Employee Account No' ,dbo.fn_GetEmpName(e.Code,0) AS 'Employee Name',dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & ") AS 'Total Amount',dbo.fn_GetTotalBasic(e.ID," & ClsFisicalPeriods.ID & ") AS 'Basic Salary',dbo.fn_GetTotalHousing(e.ID," & ClsFisicalPeriods.ID & ") AS 'Housing Allowance ',dbo.fn_GetTotalOthers(e.ID," & ClsFisicalPeriods.ID & ") as 'Other Allowance',dbo.fn_GetTotalDeductions(e.ID," & ClsFisicalPeriods.ID & ") AS 'Deduction' from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID))"
            ElseIf ConfigurationManager.AppSettings("WPSystem") = 7 Then
                strCommand = "Set DateFormat DMY; select isnull(e.SSnNo,'') AS 'الهوية',dbo.fn_GetEmpName(e.Code,0) AS 'اســــم الموظف',isnull(e.BankAccountNumber,'') AS 'رقم الحســـاب',B.Code AS 'رمز البنك','' As 'وصف الراتب',dbo.fn_GetTotalBasic(e.ID," & ClsFisicalPeriods.ID & ") AS 'الأساسي',dbo.fn_GetTotalHousing(e.ID," & ClsFisicalPeriods.ID & ") AS 'بدل السكن الشهري',dbo.fn_GetTotalOthers(e.ID," & ClsFisicalPeriods.ID & ") AS 'مستحقات أخرى',dbo.fn_GetTotalDeductions(e.ID," & ClsFisicalPeriods.ID & ") AS 'خصومات ',dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & ") AS 'الصافي' from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID))"
            ElseIf ConfigurationManager.AppSettings("WPSystem") = 8 Then
                'strCommand = "Set DateFormat DMY; select B.Code AS 'Bank', isnull(e.BankAccountNumber,'') AS 'Account Number',dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & ") AS 'TotalSalary',e.Code as 'Comments',dbo.fn_GetEmpName(e.Code,0) AS 'Employee Name',isnull(e.SSnNo,'') AS 'National ID','Jeddah' 'Employee Address',dbo.fn_GetTotalBasic(e.ID," & ClsFisicalPeriods.ID & ") AS 'Basic salary',dbo.fn_GetTotalHousing(e.ID," & ClsFisicalPeriods.ID & ") AS 'Housing Allownace ',dbo.fn_GetTotalOthers(e.ID," & ClsFisicalPeriods.ID & ") AS 'Other Earning',dbo.fn_GetTotalDeductions(e.ID," & ClsFisicalPeriods.ID & ") AS 'Deductions' from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID))"
                strCommand = "Set DateFormat DMY; select B.Code AS 'Bank', isnull(e.BankAccountNumber,'') AS 'Account Number',dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & ") AS 'TotalSalary',e.Code as 'Comments',dbo.fn_GetEmpName(e.Code,0) AS 'Employee Name',isnull(e.SSnNo,'') AS 'National ID','Jeddah' 'Employee Address',dbo.fn_GetBasicSalary2(e.ID," & ClsFisicalPeriods.ID & ") AS 'Basic salary',dbo.fn_GetTotalHousing(e.ID," & ClsFisicalPeriods.ID & ") AS 'Housing Allownace ',dbo.fn_GetTotalOthers2(e.ID," & ClsFisicalPeriods.ID & ") AS 'Other Earning',dbo.fn_GetTotalDeductions(e.ID," & ClsFisicalPeriods.ID & ") + isnull(dbo.fn_GetabsentValue(e.ID," & ClsFisicalPeriods.ID & "),0) AS 'Deductions' from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID))"
            ElseIf ConfigurationManager.AppSettings("WPSystem") = 9 Then
                strCommand = "Set DateFormat DMY; select e.Code as 'الرقم الوظيفي',dbo.fn_GetEmpName(e.Code,0) AS 'اســــم الموظف',Char(39) + isnull(e.BankAccountNumber,'') AS 'رقم الحساب',B.Code AS ' البنك','' AS ' طريقة الصرف',dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & ") AS 'المبلغ',isnull(e.SSnNo,'') AS 'الهوية',dbo.fn_GetTotalBasic(e.ID," & ClsFisicalPeriods.ID & ") AS 'الأساسي',dbo.fn_GetTotalHousing(e.ID," & ClsFisicalPeriods.ID & ") AS 'بدل السكن ',dbo.fn_GetTotalOthers(e.ID," & ClsFisicalPeriods.ID & ") AS 'دخل أخر',dbo.fn_GetTotalDeductions(e.ID," & ClsFisicalPeriods.ID & ") AS ' الخصومات' from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID))"
            ElseIf ConfigurationManager.AppSettings("WPSystem") = 10 Then

                strCommand = "Set DateFormat DMY;select B.Engname AS 'Bank name',Char(39) + isnull(e.BankAccountNumber,'') AS 'Account Number',dbo.fn_GetEmpName(e.Code,0) AS 'Employee name ',e.code as ' Employee Number ',  isnull(e.SSnNo,'') AS 'National ID Number ',dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & ") AS 'Salary',dbo.fn_GetTotalBasic(e.ID," & ClsFisicalPeriods.ID & ") AS 'Basic salary',dbo.fn_GetTotalHousing(e.ID," & ClsFisicalPeriods.ID & ") AS 'Housing',dbo.fn_GetTotalOthers(e.ID," & ClsFisicalPeriods.ID & ") AS 'Others earning',dbo.fn_GetTotalDeductions(e.ID," & ClsFisicalPeriods.ID & ") AS 'Deduction' from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID))"
                If BankID > 0 Then
                    If clsbanks.RegComputerID = 101 Then
                        strCommand = "Set DateFormat DMY;select B.Engname AS 'Bank name',Char(39) + isnull(e.BankAccountNumber,'') AS 'Account Number',dbo.fn_GetEmpName(e.Code,0) AS 'Employee name ',e.code as ' Employee Number ',  isnull(e.SSnNo,'') AS 'National ID Number ',dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & ") AS 'Salary',dbo.fn_GetTotalBasic(e.ID," & ClsFisicalPeriods.ID & ") AS 'Basic salary',dbo.fn_GetTotalHousing(e.ID," & ClsFisicalPeriods.ID & ") AS 'Housing',dbo.fn_GetTotalOthers(e.ID," & ClsFisicalPeriods.ID & ") AS 'Others earning',dbo.fn_GetTotalDeductions(e.ID," & ClsFisicalPeriods.ID & ") AS 'Deduction' from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID)) and left(e.BankAccountNumber,2)='sa'"
                    ElseIf clsbanks.RegComputerID = 102 Then
                        strCommand = "Set DateFormat DMY;select e.code as ' Employee ID',Char(39) + isnull(e.BankAccountNumber,'') AS 'Payroll card id',dbo.fn_GetEmpName(e.Code,0) AS 'Employee name ', isnull(e.SSnNo,'') AS 'National ID Number ', dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & ") AS 'Salary',dbo.fn_GetTotalBasic(e.ID," & ClsFisicalPeriods.ID & ") AS 'Basic salary',dbo.fn_GetTotalHousing(e.ID," & ClsFisicalPeriods.ID & ") AS 'Housing',dbo.fn_GetTotalOthers(e.ID," & ClsFisicalPeriods.ID & ") AS 'Others earning',dbo.fn_GetTotalDeductions(e.ID," & ClsFisicalPeriods.ID & ") AS 'Deduction',e.Mobile as 'Mobil Number' from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID)) and left(e.BankAccountNumber,2)<>'sa'"
                    End If

                End If
            ElseIf ConfigurationManager.AppSettings("WPSystem") = 2 Then

                If command = "Special" Then
                    strCommand = "Set DateFormat DMY; select * from (select distinct REPLACE(e.Code,'cop','') as 'الرقم الوظيفي',(select Code from sys_Banks where ID in (select top 1 BankID from hrs_Employees where Code = REPLACE(e.Code,'cop',''))) AS 'رمز البنك',(select ArbName from sys_Banks where ID in (select top 1 BankID from hrs_Employees where Code = REPLACE(e.Code,'cop',''))) AS 'إسم البنك عربى',(select EngName from sys_Banks where ID in (select top 1 BankID from hrs_Employees where Code = REPLACE(e.Code,'cop',''))) AS 'إسم البنك إنجليزى',(select top 1 BankAccountNumber from hrs_Employees where Code = REPLACE(e.Code,'cop','')) AS 'رقم الحســـاب',dbo.fn_GetEmpName(REPLACE(e.Code,'cop',''),1) AS 'اســــم الموظف',(select top 1 SSnNo from hrs_Employees where Code = REPLACE(e.Code,'cop','')) AS 'الهوية',dbo.fn_GetNetSalarySPS(REPLACE(e.Code,'cop','')," & ClsFisicalPeriods.ID & ") as 'صافى الراتب',(select ToDate from sys_FiscalYearsPeriods where ID = et.FiscalYearPeriodID) AS 'تاريخ الراتب','' AS الحالة,dbo.fn_GetTotalBasicSPS(REPLACE(e.Code,'cop','')," & ClsFisicalPeriods.ID & ") as 'الراتب الأساسى',dbo.fn_GetTotalHousingSPS(REPLACE(e.Code,'cop','')," & ClsFisicalPeriods.ID & ") as 'بدل السكن',dbo.fn_GetTotalOthersSPS(REPLACE(e.Code,'cop','')," & ClsFisicalPeriods.ID & ") as 'بدلات أخرى',dbo.fn_GetTotalDeductionsSPS(REPLACE(e.Code,'cop','')," & ClsFisicalPeriods.ID & ") as 'خصومات أخرى' from hrs_Employees e inner join hrs_EmployeesTransactions et on e.ID = et.EmployeeID Inner Join (Select EmployeeID,ID,EndDate,ContractTypeID,ProfessionID From hrs_Contracts Where CancelDate Is Null And ID = (Select Top 1 Cont.ID  From hrs_Contracts Cont Where cont.StartDate <= '" & ToDate & "' and IsNull(cont.EndDate, '30/12/2070') >= '" & ToDate & "' and Cont.EmployeeID = hrs_Contracts.EmployeeID And cont.CancelDate Is Null Order by IsNull(Cont.EndDate,'30/12/2070') Desc)) Contracts On e.ID = Contracts.EmployeeID Left Join  (Select EmployeeID, Max(IsNull(EndOfServiceDate, '31/12/2070')) EndOfServiceDate From hrs_EmployeesJoins Where CancelDate Is Null Group By EmployeeID) EmployeesJoins On e.ID = EmployeesJoins.EmployeeID Left Join  (Select EmployeeID, Max(ID) ID From hrs_EmployeesVacations Group By EmployeeID) EmpVacs On EmpVacs.EmployeeID = e.ID Left Join  hrs_EmployeesVacations On hrs_EmployeesVacations.ID = EmpVacs.ID where et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N'and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA'," & ClsFisicalPeriods.ID & ")) "
                    strCommand &= strFilter & ") A where A.[رقم الحســـاب] like '%SA%' and A.[صافى الراتب]>0"
                    Dim dsEmployee1 As New Data.DataSet
                    Dim SqlConn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings(0).ToString())
                    Dim adapter As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
                    Dim objCMD As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand(strCommand, SqlConn)
                    objCMD.CommandTimeout = 180
                    adapter.SelectCommand = objCMD
                    adapter.ContinueUpdateOnError = True
                    adapter.Fill(dsEmployee1, "SalaryList")
                    If dsEmployee1.Tables(0).Rows.Count > 0 Then
                        dt = dsEmployee1.Tables(0)
                        Return True
                    End If
                Else
                    strCommand = "Set DateFormat DMY; select * from (select distinct REPLACE(e.Code,'cop','') as 'الرقم الوظيفي',Char(39) + (select top 1 BankAccountNumber from hrs_Employees where Code = REPLACE(e.Code,'cop','')) AS 'رقم الحســـاب',dbo.fn_GetEmpName(REPLACE(e.Code,'cop',''),1) AS 'اســــم الموظف',(select top 1 SSnNo from hrs_Employees where Code = REPLACE(e.Code,'cop','')) AS 'الهوية',dbo.fn_GetNetSalarySPS(REPLACE(e.Code,'cop','')," & ClsFisicalPeriods.ID & ") * 100 as 'صافى الراتب',(select ToDate from sys_FiscalYearsPeriods where ID = et.FiscalYearPeriodID) AS 'تاريخ الراتب','' AS الحالة,dbo.fn_GetTotalBasicSPS(REPLACE(e.Code,'cop','')," & ClsFisicalPeriods.ID & ") * 100 as 'الراتب الأساسى',dbo.fn_GetTotalHousingSPS(REPLACE(e.Code,'cop','')," & ClsFisicalPeriods.ID & ") * 100 as 'بدل السكن',dbo.fn_GetTotalOthersSPS(REPLACE(e.Code,'cop','')," & ClsFisicalPeriods.ID & ") * 100 as 'بدلات أخرى',dbo.fn_GetTotalDeductionsSPS(REPLACE(e.Code,'cop','')," & ClsFisicalPeriods.ID & ") * 100 as 'خصومات أخرى' from hrs_Employees e inner join hrs_EmployeesTransactions et on e.ID = et.EmployeeID Inner Join (Select EmployeeID,ID,EndDate,ContractTypeID,ProfessionID From hrs_Contracts Where CancelDate Is Null And ID = (Select Top 1 Cont.ID  From hrs_Contracts Cont Where cont.StartDate <= '" & ToDate & "' and IsNull(cont.EndDate, '30/12/2070') >= '" & ToDate & "' and Cont.EmployeeID = hrs_Contracts.EmployeeID And cont.CancelDate Is Null Order by IsNull(Cont.EndDate,'30/12/2070') Desc)) Contracts On e.ID = Contracts.EmployeeID Left Join  (Select EmployeeID, Max(IsNull(EndOfServiceDate, '31/12/2070')) EndOfServiceDate From hrs_EmployeesJoins Where CancelDate Is Null Group By EmployeeID) EmployeesJoins On e.ID = EmployeesJoins.EmployeeID Left Join  (Select EmployeeID, Max(ID) ID From hrs_EmployeesVacations Group By EmployeeID) EmpVacs On EmpVacs.EmployeeID = e.ID Left Join  hrs_EmployeesVacations On hrs_EmployeesVacations.ID = EmpVacs.ID where et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N'and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA'," & ClsFisicalPeriods.ID & ")) "
                    strCommand &= strFilter & ") A where A.[رقم الحســـاب] Not like '%SA%' and A.[صافى الراتب]>0"
                    Dim dsEmployee1 As New Data.DataSet
                    Dim SqlConn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings(0).ToString())
                    Dim adapter As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
                    Dim objCMD As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand(strCommand, SqlConn)
                    objCMD.CommandTimeout = 180
                    adapter.SelectCommand = objCMD
                    adapter.ContinueUpdateOnError = True
                    adapter.Fill(dsEmployee1, "SalaryList")
                    If dsEmployee1.Tables(0).Rows.Count > 0 Then
                        dt = dsEmployee1.Tables(0)
                        Return True
                    End If
                End If
            End If

            If command = "Special" Then
                strCommand = "Set DateFormat DMY; select B.Code AS 'Bank', isnull(e.BankAccountNumber,'') AS 'Account Number',dbo.fn_GetOverTime(e.ID," & ClsFisicalPeriods.ID & ") AS 'TotalSalary',e.Code as 'Comments',dbo.fn_GetEmpName(e.Code,0) AS 'Employee Name',isnull(e.SSnNo,'') AS 'National ID','Jeddah' 'Employee Address',dbo.fn_GetTotalBasic(e.ID," & ClsFisicalPeriods.ID & ") AS 'Basic salary','0' AS 'Housing Allownace ','0' AS 'Other Earning','0' 'Deductions' from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID)) AND dbo.fn_GetOverTime(e.ID," & ClsFisicalPeriods.ID & ")>0"

                'Dim dsEmployee1 As New Data.DataSet
                'Dim SqlConn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings(0).ToString())
                'Dim adapter As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
                'Dim objCMD As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand(strCommand, SqlConn)
                'objCMD.CommandTimeout = 180
                'adapter.SelectCommand = objCMD
                'adapter.ContinueUpdateOnError = True
                'adapter.Fill(dsEmployee1, "SalaryList")
                'If dsEmployee1.Tables(0).Rows.Count > 0 Then
                '    dt = dsEmployee1.Tables(0)
                '    Return True
                'End If
            End If

            strCommand &= strFilter
            Dim dsEmployee As New Data.DataSet
            dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, Data.CommandType.Text, strCommand)
            If dsEmployee.Tables(0).Rows.Count > 0 Then
                dt = dsEmployee.Tables(0)
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetData(Optional ByRef dt As DataTable = Nothing) As Boolean
        Try
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)
            Dim BranchID As Integer = ddlBranche.SelectedValue
            Dim DepartmentID As Integer = ddlDepartment.SelectedValue
            Dim BankID As Integer = DropDownList_Bank.SelectedValue
            Dim BankAccountType As String = ddlBankAccountType.SelectedValue
            UwgSearchEmployees.DataSource = Nothing
            UwgSearchEmployees.DataBind()
          
            If DdlPeriods.SelectedIndex = 0 Then
                Return False
            End If
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


            Dim strFilter As String = " And isnull(e.LaborOfficeNo,'') like '%" & txtCode.Text & "%' And e.SponsorID in (select ID from hrs_Sponsors where Code like '%" & TextBox_Sponsor.Text & "%') "
            If BranchID > 0 Then
                strFilter &= " And e.BranchID = " & BranchID
            Else
                Dim mUserID As Integer = ClsEmployee.DataBaseUserRelatedID
                Dim strbrnches As String
                Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, Data.CommandType.Text, "select BrancheID  FROM sys_CompaniesBranches where sys_CompaniesBranches.CanView = 1 and UserID =" & mUserID)
                For index = 0 To ds.Tables(0).Rows.Count - 1
                    strbrnches &= ds.Tables(0).Rows(index)("BrancheID").ToString() + ","
                Next
                If strbrnches.EndsWith(",") Then
                    strbrnches = strbrnches.Remove(strbrnches.Length - 1)
                End If
                strFilter &= " And e.BranchID in(" & strbrnches & ")"
            End If



            If DepartmentID > 0 Then
                strFilter &= " And e.DepartmentID = " & DepartmentID
            End If
            If BankID > 0 Then
                strFilter &= " And e.BankID = " & BankID
            End If
            If BankAccountType <> "0" Then
                strFilter &= " And e.BankAccountType = '" & BankAccountType & "'"
            End If

            Dim strCommand As String
            'If ConfigurationManager.AppSettings("WPSystem") = 1 Then
            strCommand = "Set DateFormat DMY; select et.ID,et.EmployeeID,e.Code as Code," & "dbo.fn_GetEmpName(e.Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS FullName, round(dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & "),0) AS NetSalary from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and round(dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & "),0) > 0 and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID))"
            strCommand &= strFilter
            'Else
            ' strCommand = "Set DateFormat DMY; select * from (select et.ID,et.EmployeeID,e.Code as Code," & "dbo.fn_GetEmpName(e.Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS FullName,dbo.fn_GetNetSalarySPS(REPLACE(e.Code,'cop','')," & ClsFisicalPeriods.ID & ") AS NetSalary from hrs_Employees e inner join hrs_EmployeesTransactions et on e.ID = et.EmployeeID Inner Join (Select EmployeeID,ID,EndDate,ContractTypeID,ProfessionID From hrs_Contracts Where CancelDate Is Null And ID = (Select Top 1 Cont.ID  From hrs_Contracts Cont Where cont.StartDate <= '" & ToDate & "' and IsNull(cont.EndDate, '30/12/2070') >= '" & ToDate & "' and Cont.EmployeeID = hrs_Contracts.EmployeeID And cont.CancelDate Is Null Order by IsNull(Cont.EndDate,'30/12/2070') Desc)) Contracts On e.ID = Contracts.EmployeeID Left Join  (Select EmployeeID, Max(IsNull(EndOfServiceDate, '31/12/2070')) EndOfServiceDate From hrs_EmployeesJoins Where CancelDate Is Null Group By EmployeeID) EmployeesJoins On e.ID = EmployeesJoins.EmployeeID Left Join  (Select EmployeeID, Max(ID) ID From hrs_EmployeesVacations Group By EmployeeID) EmpVacs On EmpVacs.EmployeeID = e.ID Left Join  hrs_EmployeesVacations On hrs_EmployeesVacations.ID = EmpVacs.ID where et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N'and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA'," & ClsFisicalPeriods.ID & ")) "
            ' strCommand &= strFilter & " ) A where A.NetSalary > 0 order by A.Code"
            'End If

            Dim dsEmployee As New Data.DataSet
            Dim SqlConn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings(0).ToString())
            'Dim adapter As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
            'Dim objCMD As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand(strCommand, SqlConn)


            'objCMD.CommandTimeout = 180

            'adapter.SelectCommand = objCMD

            'adapter.ContinueUpdateOnError = True

            'adapter.Fill(dsEmployee, "SalaryList")
            dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, CommandType.Text, strCommand)

            ' Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " 0/0" & dsEmployee.Tables(0).Rows.Count()))

            If dsEmployee.Tables(0).Rows.Count > 0 Then
                UwgSearchEmployees.DataSource = dsEmployee.Tables(0)
                UwgSearchEmployees.DataBind()
                dt = dsEmployee.Tables(0)
            End If
            ddlDepartment.SelectedValue = DepartmentID
            ddlBranche.SelectedValue = BranchID

            Return True


        Catch ex As Exception


            Return False
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
    Protected Sub LinkButton1_Click(sender As Object, e As System.EventArgs) Handles LinkButton1.Click
        Dim dt As New DataTable()
        If LoadData(dt, "Special") Then
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=MyFiles.xls")
            Response.ContentType = "application/vnd.ms-excel"
            Response.ContentEncoding = System.Text.Encoding.Unicode
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())
            Dim tw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim dgGrid As New DataGrid()
            For Each row As DataRow In dt.Rows
                For i As Integer = 0 To dt.Columns.Count - 1
                    If dt.Columns(i).ColumnName = "Column1" Then
                        row(i) = "إيداع راتب"
                    ElseIf dt.Columns(i).ColumnName = "Column2" Then
                        row(i) = "0"
                    ElseIf dt.Columns(i).ColumnName = "Column3" Then
                        row(i) = "0"
                    End If
                Next
            Next
            For Each column As DataColumn In dt.Columns
                If column.ColumnName = "Column1" Then
                    column.ColumnName = "وصف المدفوعات"
                ElseIf column.ColumnName = "Column2" Then
                    column.ColumnName = "مرجع العملية من البنك"
                ElseIf column.ColumnName = "Column3" Then
                    column.ColumnName = "حالة العملية من البنك"
                End If
            Next
            dgGrid.DataSource = dt
            dgGrid.DataBind()
            dgGrid.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        End If
    End Sub
#End Region

#Region "Public Shared Function"

    Public Shared Function CheckBranchPermission(ByVal intDeptID As Integer) As String
        Try
            Dim str As String = ""
            Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
            Dim mCompanyID As Integer = CType(HttpContext.Current.Session("CompanyID"), Integer)
            Dim mUserID As Integer = CType(HttpContext.Current.Session("UserID"), Integer)
            Dim BranchesIDs As String = GetRelatedDept(intDeptID)
            BranchesIDs = IIf(BranchesIDs = "", "0", BranchesIDs)

            Dim StrSelectCommand As String = _
                    "Declare @Branches as nvarchar(max)='';" & _
                    "Select  @Branches = @Branches + N',' + Cast(B.ID As varchar(200)) " & _
                    "From sys_Branches B Inner Join sys_CompaniesBranches CB ON CB.BrancheID=B.ID Where B.ID IN (" & BranchesIDs & ")  And CB.CompanyID=" & mCompanyID & " And CB.UserID=" & mUserID & " AND CanView= 1" & _
                    "Select @Branches  = STUFF(@Branches,1,1,''); " & _
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

        Try
            StrSelectCommand = " Declare @Branches as nvarchar(max)=''; " & _
                                "Select  @Branches = @Branches + N',' + Cast(B.ID As varchar(200)) " & _
                                "From sys_DepartmentsBranches DB Inner Join sys_Branches B On DB.BranchID = B.ID Where DB.DepartmentID = " & intDeptID & " And DB.Checked  = 1 And B.CancelDate Is Null; " & _
                                "Select @Branches  = STUFF(@Branches,1,1,''); " & _
                                "Select IsNull(@Branches,'')"

            Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, Data.CommandType.Text, StrSelectCommand)

        Catch ex As Exception

        End Try
    End Function
    <System.Web.Services.WebMethod()> _
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
            Dim strAllName As String = IIf(Lage = "0", "[All branches]", "[ جميع الفروع]")
            Dim str As String = String.Empty

            StrSelectCommand = " Select B.ID, B." & strFieldName & " From sys_Branches B Inner Join sys_CompaniesBranches CB ON CB.BrancheID=B.ID Where B.ID IN (" & IIf(strResultBranches = "", 0, strResultBranches) & ") And CB.CompanyID=" & mCompanyID & " And CB.UserID=" & mUserID & " AND CanView= 1"

            dsBranches = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnStr, Data.CommandType.Text, StrSelectCommand)

            If dsBranches.Tables(0).Rows.Count > 0 Then

                str = "<select style='border: 1px solid rgb(204, 204, 204); width: 100%; height: 20px; color: black; font-family: Tahoma; font-size: 8pt; font-weight: normal;' id='UltraWebTab1__ctl0_ddlBranche' name='UltraWebTab1$_ctl0$ddlBranche'><option value=0>" & strAllName & "</option>"

                For I As Integer = 0 To dsBranches.Tables(0).Rows.Count - 1
                    str &= "<option value=" & dsBranches.Tables(0).Rows(I).Item("ID") & ">" & dsBranches.Tables(0).Rows(I).Item(strFieldName) & "</option>"
                Next

                str &= "</select>"

                Return str
            Else
                Return "<select style='border: 1px solid rgb(204, 204, 204); width: 100%; height: 20px; color: black; font-family: Tahoma; font-size: 8pt; font-weight: normal;' id='UltraWebTab1__ctl0_ddlBranche' name='UltraWebTab1$_ctl0$ddlBranche'><option value=0>" & strAllName & "</option></select>"
            End If

        Catch ex As Exception

        End Try
    End Function

#End Region

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        GetData()
    End Sub

    Protected Sub ddlBranche_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBranche.SelectedIndexChanged
        GetData()
    End Sub
End Class
