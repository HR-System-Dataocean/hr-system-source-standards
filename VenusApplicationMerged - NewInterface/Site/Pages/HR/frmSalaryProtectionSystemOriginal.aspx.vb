Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.IO

Partial Class frmSalaryProtectionSystem2
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
        Dim ClsSalaryProductionFiles As New Clshrs_SalaryProductionFiles(Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
        Dim ClsGradeStep As New Clshrs_GradesSteps(Me.Page)

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
            ClsSalaryProductionFiles.GetDropDownList(dllFileFormat, True)
            ddlDepartment.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Departments]/ [ جميع الإدارات]")
            clsBranch.GetDropDownList(ddlBranche, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
            ddlBranche.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Branches]/ [ جميع الفروع]")
            ClsEmployee.GetDropDownList(ddlLblPaymentType, "Hrs_PaymentTypes", True, "")
            ClsGradeStep.GetDropDownList(ddlGradeStep, True, "")


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

                        Dim ClsSalaryProductionFiles As New Clshrs_SalaryProductionFiles(Page)

                        Dim IsTextfile As Boolean
                        Dim str As String = "select IsTextfile from hrs_SalaryProductionFiles where id =" & dllFileFormat.SelectedValue & ""
                        IsTextfile = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsSalaryProductionFiles.ConnectionString, Data.CommandType.Text, str)
                        If Not IsTextfile Then
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

                        Else



                            Dim ms As New MemoryStream()

                            Dim tw As New StreamWriter(ms)
                            'Dim hw As New System.Web.UI.HtmlTextWriter(tw)
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

                            'For Each Row In dt.Rows
                            '    str = String.Empty
                            '    For b As Integer = 0 To dt.Columns.Count - 1
                            '        str = str & Row.(b).Value.ToString & "|"
                            '    Next
                            '    str = str.Substring(0, str.Length - 1)
                            '    tw.WriteLine(str)
                            'Next
                            str = String.Empty
                            For Each column As DataColumn In dt.Columns
                                str = str & column.ColumnName.ToString & "|"


                            Next
                            str = str.Substring(0, str.Length - 1)
                            tw.WriteLine(str)

                            For a As Integer = 0 To dt.Rows.Count - 1
                                str = String.Empty

                                For b As Integer = 0 To dt.Columns.Count - 1
                                    str = str & dt.Rows(a)(b).ToString & "|"
                                Next
                                str = str.Substring(0, str.Length - 1)
                                tw.WriteLine(str)
                            Next
                            tw.Flush()
                            Dim bytes As Byte() = ms.ToArray()
                            ms.Close()

                            Response.Clear()
                            Response.ContentType = "application/force-download"
                            Response.AddHeader("content-disposition", "attachment;    filename=SalaryProtectionFile" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".txt")

                            Response.ContentEncoding = System.Text.Encoding.Unicode
                            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble())

                            Response.BinaryWrite(bytes)
                            HttpContext.Current.Response.Flush()
                            HttpContext.Current.Response.SuppressContent = True
                            HttpContext.Current.ApplicationInstance.CompleteRequest()
                        End If
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Private Function"

    Private Function getFileFiledsAsString(ByVal FileId As Integer) As String
        Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)
        ClsFisicalPeriods.Find("ID=" & DdlPeriods.SelectedValue)
        Dim strDeclartion = "Declare @FisicalPeriodId int =" & ClsFisicalPeriods.ID
        Dim strFileFileds As String = strDeclartion & " ;Set DateFormat DMY;select "
        Dim ClsSalaryProductionFiles As New Clshrs_SalaryProductionFiles(Page)
        Dim str As String = "select case when SPFC.Name='' then spc.Name else SPFC.Name end as AliasName,SPC.DataSource DataSource FROM hrs_SalaryProductionFilesColumns SPFC inner join hrs_SalaryProductionColumns SPC on SPC.Id=SPFC.SalaryProductionColumnId where spfc.SalaryProductionFileId =" & FileId
        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsSalaryProductionFiles.ConnectionString, Data.CommandType.Text, str)

        If ds.Tables(0).Rows.Count > 0 Then
            For Each Row In ds.Tables(0).Rows
                strFileFileds = strFileFileds & Row("DataSource") & " As '" & Row("AliasName") + "',"
            Next
            strFileFileds = strFileFileds.Remove(strFileFileds.Length - 1, 1)
        End If


        Return strFileFileds
    End Function
    Private Function LoadData(ByRef dt As DataTable, Optional ByVal command As String = "") As Boolean
        Try
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)

            Dim BranchID As Integer = ddlBranche.SelectedValue
            Dim DepartmentID As Integer = ddlDepartment.SelectedValue
            Dim BankID As Integer = DropDownList_Bank.SelectedValue
            Dim BankAccountType As String = ddlBankAccountType.SelectedValue
            Dim PaymentType As Integer = ddlLblPaymentType.SelectedValue
            Dim GradeStep As Integer = ddlGradeStep.SelectedValue
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
            If PaymentType > 0 Then
                strFilter &= " And e.PaymentType = '" & PaymentType & "'"

            End If
            If GradeStep > 0 Then
                strFilter &= " And C.GradeStepID = '" & GradeStep & "'"

            End If
            Dim strFileds As String = getFileFiledsAsString(dllFileFormat.SelectedValue)

            Dim strTables As String = " from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID INNER JOIN sys_Banks B on B.ID = e.BankID join hrs_Contracts C on e.ID=C.EmployeeID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID))"

            Dim strCommand As String = ""


            strCommand &= strFileds & strTables & strFilter
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
            If ddlLblPaymentType.SelectedValue > 0 Then
                strFilter &= " And e.PaymentType = '" & ddlLblPaymentType.SelectedValue & "'"

            End If
            If ddlGradeStep.SelectedValue > 0 Then
                strFilter &= " And C.GradeStepID = '" & ddlGradeStep.SelectedValue & "'"

            End If
            Dim strCommand As String
            'If ConfigurationManager.AppSettings("WPSystem") = 1 Then
            strCommand = "Set DateFormat DMY; select et.ID,et.EmployeeID,e.Code as Code," & "dbo.fn_GetEmpName(e.Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS FullName, round(dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & "),0) AS NetSalary from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID inner join hrs_Contracts C on e.ID=C.EmployeeID  where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' and round(dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & "),0) > 0 and e.ID In (select * from dbo.GetEmpOnBankTransfeer('B','EWA',et.FiscalYearPeriodID)) and C.CancelDate is null and (C.EndDate>GETDATE()or C.EndDate is null)"
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

        Try
            StrSelectCommand = " Declare @Branches as nvarchar(max)=''; " &
                                "Select  @Branches = @Branches + N',' + Cast(B.ID As varchar(200)) " &
                                "From sys_DepartmentsBranches DB Inner Join sys_Branches B On DB.BranchID = B.ID Where DB.DepartmentID = " & intDeptID & " And DB.Checked  = 1 And B.CancelDate Is Null; " &
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

    Protected Sub ddlLblPaymentType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlLblPaymentType.SelectedIndexChanged
        GetData()
    End Sub
    Protected Sub ddlGradeStep_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlGradeStep.SelectedIndexChanged
        GetData()
    End Sub
End Class
