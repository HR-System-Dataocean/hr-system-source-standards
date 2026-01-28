Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmAttendancePreparation
    Inherits MainPage

#Region "Public Decleration"
    Dim mErrorHandler As Venus.Shared.ErrorsHandler
    Dim clsMainOtherFields As clsSys_MainOtherFields
    Private dbOTSalary As Double = 0
    Private dbHOTSalary As Double = 0
    Private ClsClasses As Clshrs_EmployeeClasses
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim EmpID As Integer = Request.QueryString.Item("EmpID")
        Dim PeriodID As Integer = Request.QueryString.Item("PeriodID")
        Dim clsEmployees As New Clshrs_Employees(Page)
        Dim ClsVacationsTypes As New Clshrs_VacationsTypes(Page)
        Dim ClsProjects As New Clshrs_Projects(Page, "hrs_Projects")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler

        If Not IsPostBack Then

            Dim Clscompanies As New Clssys_Companies(Page)
            Clscompanies.Find("ID = " & Clscompanies.MainCompanyID)
            If (Clscompanies.IsHigry = True) Then
                UWGEmployeesAttend.Bands(0).Columns(2).Hidden = True
                UWGEmployeesAttend.Bands(0).Columns(3).Hidden = False
            Else
                UWGEmployeesAttend.Bands(0).Columns(2).Hidden = False
                UWGEmployeesAttend.Bands(0).Columns(3).Hidden = True
            End If

            ClsProjects.Find("")
            GetList_Data(UWGEmployeesAttend.Bands(1).Columns.FromKey("ProjectID").ValueList, ClsProjects.DataSet)

            ClsVacationsTypes.Find("")
            GetList_Data(UWGEmployeesAttend.Bands(0).Columns.FromKey("LeavingType").ValueList, ClsVacationsTypes.DataSet)
            ViewAttend(EmpID, PeriodID)

            Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Me)
            If ClsEmployeesTransactions.Find("EmployeeID=" & EmpID & " And FiscalYearPeriodID=" & Request.QueryString.Item("PeriodID") & " And PrepareType ='N'") Then
                btnSave.Enabled = False
            End If
            btnDelete.Visible = False
        End If
        Dim ClsCountries As New Clssys_Countries(Me.Page)
        Dim clsMainCurrency As New ClsSys_Currencies(Me.Page)
        If ClsCountries.Find(" IsMainCountries = 1 ") Then
            clsMainCurrency.Find(" ID=" & ClsCountries.CurrencyID)
            If Not IsNothing(clsMainCurrency.NoDecimalPlaces) Then
                UWGEmployeesAttend.Bands(0).Columns.FromKey("TotalLate").Format = clsMainCurrency.GetFormatOfDecimalPlaces(UWGEmployeesAttend.Bands(0).Columns.FromKey("TotalLate").Format, 1)
                UWGEmployeesAttend.Bands(0).Columns.FromKey("PermitLate").Format = clsMainCurrency.GetFormatOfDecimalPlaces(UWGEmployeesAttend.Bands(0).Columns.FromKey("PermitLate").Format, 1)
                UWGEmployeesAttend.Bands(0).Columns.FromKey("NotpermitLate").Format = clsMainCurrency.GetFormatOfDecimalPlaces(UWGEmployeesAttend.Bands(0).Columns.FromKey("NotpermitLate").Format, 1)
                UWGEmployeesAttend.Bands(0).Columns.FromKey("Overtime").Format = clsMainCurrency.GetFormatOfDecimalPlaces(UWGEmployeesAttend.Bands(0).Columns.FromKey("Overtime").Format, 1)
                UWGEmployeesAttend.Bands(0).Columns.FromKey("HolidayHours").Format = clsMainCurrency.GetFormatOfDecimalPlaces(UWGEmployeesAttend.Bands(0).Columns.FromKey("HolidayHours").Format, 1)


                UWGEmployeesAttend.Bands(1).Columns.FromKey("TotalTime").Format = clsMainCurrency.GetFormatOfDecimalPlaces(UWGEmployeesAttend.Bands(1).Columns.FromKey("TotalTime").Format, 1)
                UWGEmployeesAttend.Bands(1).Columns.FromKey("Overtime").Format = clsMainCurrency.GetFormatOfDecimalPlaces(UWGEmployeesAttend.Bands(1).Columns.FromKey("Overtime").Format, 1)
                UWGEmployeesAttend.Bands(1).Columns.FromKey("HolidayHours").Format = clsMainCurrency.GetFormatOfDecimalPlaces(UWGEmployeesAttend.Bands(1).Columns.FromKey("HolidayHours").Format, 1)
            End If
        End If
    End Sub
    Public Function GetList_Data(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal ObjDataset As DataSet) As Boolean
        Dim ObjDataRow As DataRow
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim clsEmployees As New Clshrs_Employees(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
        Try
            DdlValues.ValueListItems.Clear()
            Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
            Item.DisplayText = ObjNavigationHandler.SetLanguage(Page, "[Select your choise]/[إختر أحد الأختيارات]")
            Item.DataValue = 0
            DdlValues.ValueListItems.Add(Item)
            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = IIf(IsDBNull(ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName"))), _
                                       IIf(IsDBNull(ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "ArbName/EngName"))), _
                                       "", _
                                       ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "ArbName/EngName"))), _
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
    Protected Sub btnSave_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim EmpID As Integer = Request.QueryString.Item("EmpID")

        If SaveAttend(EmpID) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Save Done !/!تم الحفظ"))
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
        End If
    End Sub

#End Region

#Region "Private Function"
    Private FromDate As DateTime
    Private ToDate As DateTime
    Private FiscFromDate As DateTime
    Private FiscToDate As DateTime
    Private Function ViewAttend(ByVal EmpID As Integer, ByVal PeriodID As Integer) As Boolean
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)
        ClsFisicalPeriods.Find("ID=" & PeriodID)
        Dim DS1 As New Data.DataSet()
        DS1.Clear()
        For x As Integer = 0 To DS1.Tables.Count - 1
            DS1.Tables(x).Constraints.Clear()
        Next
        DS1.Relations.Clear()
        DS1.Tables.Clear()
        Dim connetionString As String
        Dim connection As Data.SqlClient.SqlConnection
        Dim command As Data.SqlClient.SqlCommand
        Dim adapter As New Data.SqlClient.SqlDataAdapter
        connetionString = ClsEmployees.ConnectionString
        connection = New Data.SqlClient.SqlConnection(connetionString)

        Try
            ClsEmployees.Find("ID=" & EmpID)

            FromDate = ClsFisicalPeriods.FromDate
            ToDate = ClsFisicalPeriods.ToDate
            FiscFromDate = ClsFisicalPeriods.FromDate
            FiscToDate = ClsFisicalPeriods.ToDate

            Dim clsCompanies As New Clssys_Companies(Page)
            clsCompanies.Find("ID=" & ClsFisicalPeriods.MainCompanyID)
            Dim clsBranch As New Clssys_Branches(Page)
            clsBranch.Find("ID=" & ClsEmployees.BranchID)
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

            lblDescEmployeeCode.Text = ClsEmployees.Code
            lblDescEnglishName.Text = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, "Select dbo.fn_GetEmpName('" & ClsEmployees.Code & "'," & objNav.SetLanguage(Page, "0/1") & ")")  'ClsEmployee.Name
            lblDescTotalVacation.Text = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, "(select ISNULL(SUM(convert(int,IsVacation)),0) from Att_AttendancePreparationDetails where EmployeeID = " & EmpID & " and CONVERT(Datetime,GAttendDate,103)>= CONVERT(Datetime,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(Datetime,GAttendDate,103) <= CONVERT(Datetime,'" & ToDate.ToString("dd/MM/yyyy") & "',103))  ")
            lblDescTotalAbsent.Text = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, "(select ISNULL(SUM(convert(int,IsAbsent)),0) from Att_AttendancePreparationDetails where EmployeeID = " & EmpID & " and CONVERT(Datetime,GAttendDate,103)>= CONVERT(Datetime,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(Datetime,GAttendDate,103) <= CONVERT(Datetime,'" & ToDate.ToString("dd/MM/yyyy") & "',103) and IsVacation = 0)  ")

            Dim strwhr As String = " and EmployeeID = " & EmpID

            Dim str2 As String = "select ParentTable.ID,EmployeeID,GAttendDate as GAttendDate,HAttendDate,(TotalLate * 60) TotalLate," & _
                                 "(PermitLate * 60) PermitLate," & _
                                 "(NotpermitLate * 60) NotpermitLate," & _
                                 "(select ISNULL(SUM(Overtime * 60),0) Overtime from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = ParentTable.EmployeeID and CONVERT(Datetime,GAttendDate,103)= CONVERT(Datetime,ParentTable.GAttendDate,103))) as Overtime," & _
                                 "(select ISNULL(SUM(HolidayHours * 60),0) HolidayHours from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = ParentTable.EmployeeID and CONVERT(Datetime,GAttendDate,103)= CONVERT(Datetime,ParentTable.GAttendDate,103))) as HolidayHours," & _
                                 "IsVacation,IsAbsent,LeavingType,Notes,ParentTable.RegComputerID " & _
                                 "from Att_AttendancePreparationDetails AS ParentTable inner join hrs_Employees on hrs_Employees.ID = ParentTable.EmployeeID  where CONVERT(Datetime,ParentTable.GAttendDate,103)>= CONVERT(Datetime,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(Datetime,ParentTable.GAttendDate,103) <= CONVERT(Datetime,'" & ToDate.ToString("dd/MM/yyyy") & "',103) and ParentTable.CancelDate is null " & strwhr & " order by ParentTable.GAttendDate ASC"

            Dim str3 As String = "select A.ID,A.TrnsID,A.ProjectID,A.CheckIn,A.Checkout,(A.TotalTime * 60) TotalTime,(A.Overtime * 60) Overtime,(A.HolidayHours * 60) HolidayHours from Att_AttendancePreparationProjects A left outer join Att_AttendancePreparationDetails ParentTable on A.TrnsID=ParentTable.ID" & _
                                 " where CONVERT(Datetime,ParentTable.GAttendDate,103)>= CONVERT(Datetime,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(Datetime,ParentTable.GAttendDate,103) <= CONVERT(Datetime,'" & ToDate.ToString("dd/MM/yyyy") & "',103) and ParentTable.CancelDate is null " & strwhr & " order by ParentTable.GAttendDate ASC"

            command = New Data.SqlClient.SqlCommand(str2, connection)
            adapter.SelectCommand = command
            adapter.Fill(DS1, "Table1")
            command = New Data.SqlClient.SqlCommand(str3, connection)
            adapter.SelectCommand = command
            adapter.Fill(DS1, "Table2")
            adapter.Dispose()
            command.Dispose()
            connection.Close()

            Dim DataCol3 As Data.DataColumn
            Dim DataCol4 As Data.DataColumn
            DataCol3 = DS1.Tables(0).Columns("ID")
            DataCol4 = DS1.Tables(1).Columns("TrnsID")
            Dim Rel2 As Data.DataRelation = New Data.DataRelation("Rel2", DataCol3, DataCol4, False)
            DS1.Relations.Add(Rel2)

            UWGEmployeesAttend.DataSource = Nothing
            UWGEmployeesAttend.DataBind()

            UWGEmployeesAttend.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
            UWGEmployeesAttend.DataSource = DS1
            UWGEmployeesAttend.DataBind()

            'Edited By Hassan Kurdi
            'Date 2021-02-17
            'Added Total Late

            Dim TotalLate As Double = 0
            Dim Total_OV As Double = 0
            Dim TotalH_OV As Double = 0
            Dim NotpermitLate As Double = 0

            For Each row As Data.DataRow In DS1.Tables(0).Rows
                Total_OV = Total_OV + row.Item("Overtime")
                TotalH_OV = TotalH_OV + row.Item("HolidayHours")
                NotpermitLate = NotpermitLate + row.Item("NotpermitLate")

                If row.Item("NotpermitLate") > 0 Then
                    TotalLate = TotalLate + row.Item("TotalLate")
                End If

            Next

            lblDescTotalNotPermitLate.Text = Math.Round(NotpermitLate, 2)
            lblDescTotalLate.Text = Math.Round(TotalLate, 2)
            lblDescTotalOvertime.Text = Math.Round(TotalH_OV + Total_OV, 2)

            'End of Edit

            CheckLeavingType()
            '        Dim col As Infragistics.WebUI.UltraWebGrid.UltraGridColumn =
            'UWGEmployeesAttend.DisplayLayout.Bands(0).Columns("LeavingType")
            '        col.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub CheckLeavingType()
        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UWGEmployeesAttend.Rows
            If DGRow.HasChildRows Then
                If DGRow.Cells.FromKey("LeavingType").Value > 0 And DGRow.Cells.FromKey("RegComputerID").Value = Nothing Then
                    DGRow.Cells.FromKey("LeavingType").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                Else
                    'DGRow.Cells.FromKey("LeavingType").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes
                End If
            End If
        Next
    End Sub
    Private ClsEmployeesContracts As Clshrs_Contracts
    Private dbBasicSalary As Double
    Private ObjSalaryPerDay As Double
    Private ObjSalaryPerHour As Double
    Private Function Calculate_SalaryPerHour(ByRef Amount As Double, empID As Integer, ByRef TrnsDate As DateTime) As Boolean
        Try
            Dim clsCompanies As New Clssys_Companies(Me)
            ClsEmployeesContracts = New Clshrs_Contracts(Me.Page)
            clsCompanies.Find("ID=" & clsCompanies.MainCompanyID)
            If clsCompanies.SalaryCalculation = 0 Then            'Get Basic Salary
                dbBasicSalary = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsCompanies.ConnectionString, Data.CommandType.Text, "set dateformat dmy; select dbo.fn_GetBasicSalary(" & ClsEmployeesContracts.ContractValidatoinId(empID, TrnsDate) & ",'" & TrnsDate.ToString("dd/MM/yyyy") & "')")
                Amount = dbBasicSalary
            ElseIf clsCompanies.SalaryCalculation = 1 Then        'Get Total Salary By Days
                dbBasicSalary = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsCompanies.ConnectionString, Data.CommandType.Text, "set dateformat dmy; select dbo.fn_GetTotalAdditions(" & ClsEmployeesContracts.ContractValidatoinId(empID, TrnsDate) & ",'" & TrnsDate.ToString("dd/MM/yyyy") & "')")
                Amount = dbBasicSalary
            End If


            Dim ClsSolver As New Clshrs_FormulaSolver(clsCompanies.ConnectionString, Me.Page)
            ClsSolver.EmployeeID = empID
            Dim ClsFisicalPeriods = New Clssys_FiscalYearsPeriods(Page)
            ClsFisicalPeriods.Find("ID=" & Request.QueryString.Item("PeriodID"))
            If ClsClasses.NoOfDaysPerPeriod = 0 Then
                ClsSolver.NoOfDaysPerPeriod = ClsFisicalPeriods.ToDate.Day
            Else
                ClsSolver.NoOfDaysPerPeriod = 30
            End If

            ClsSolver.NoOfWorkingDays = 30

            ClsSolver.EvaluateExpression(ClsClasses.OvertimeFormula, 0)
            dbOTSalary = ClsSolver.Output

            ClsSolver.EvaluateExpression(ClsClasses.HolidayFormula, 0)
            dbHOTSalary = ClsSolver.Output
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SaveAttend(ByVal EmpID As Integer) As Boolean
        Try
            Dim ClsEmployees As New Clshrs_Employees(Page)

            Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)
            ClsFisicalPeriods.Find("ID=" & Request.QueryString.Item("PeriodID"))

            FromDate = ClsFisicalPeriods.FromDate
            ToDate = ClsFisicalPeriods.ToDate
            FiscFromDate = ClsFisicalPeriods.FromDate
            FiscToDate = ClsFisicalPeriods.ToDate

            Dim clsCompanies As New Clssys_Companies(Page)
            clsCompanies.Find("ID=" & ClsFisicalPeriods.MainCompanyID)
            Dim clsBranch As New Clssys_Branches(Page)
            clsBranch.Find("ID=" & ClsEmployees.BranchID)
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

            Dim clsDAL As New ClsDataAcessLayer(Page)
            Dim ClsClassDelay As New Clshrs_EmployeesClassesDelay(Page)
            Dim ClsContract As New Clshrs_Contracts(Page)
            ClsClasses = New Clshrs_EmployeeClasses(Page)
            Dim ClsAttendancePreparationDetails As New ClsAtt_AttendancePreparationDetails(Page)
            If ClsAttendancePreparationDetails.Find("ID = " & UWGEmployeesAttend.Rows(0).Cells.FromKey("ID").Value) Then
                Dim strgetChanges As String = "set dateformat dmy; select Distinct convert(varchar(10),ActiveDate,103) AS Dte from hrs_ContractsTransactions where ActiveDate >= " & ClsAttendancePreparationDetails.GAttendDate.ToString("dd/MM/yyyy") & " and canceldate is null and Active = 1 and contractID in (select ID from hrs_contracts where EmployeeID = " & EmpID & " and canceldate is null)"
                Dim dtChanges As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, System.Data.CommandType.Text, strgetChanges).Tables(0)
                Dim Amount As Double = 0
                Dim EmployeeDS As New Data.DataSet()
                ClsEmployees.GetAllEmployeeValidContract(EmployeeDS, EmpID)
                Dim ValidContract As Integer = Convert.ToInt32(EmployeeDS.Tables(0).Rows(0)("ContractID").ToString())
                ClsContract.Find("ID = " & ValidContract)
                Dim ValidClass As Integer = ClsContract.EmployeeClassID
                ClsClasses.Find("ID = " & ValidClass)
                Calculate_SalaryPerHour(Amount, EmpID, ClsAttendancePreparationDetails.GAttendDate)
                For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UWGEmployeesAttend.Rows
                    If DGRow1.HasChildRows Then
                        ClsAttendancePreparationDetails = New ClsAtt_AttendancePreparationDetails(Page)
                        Dim ClsAttendancePreparationDetailsOld As New ClsAtt_AttendancePreparationDetails(Page)
                        ClsAttendancePreparationDetails.Find("ID = " & DGRow1.Cells.FromKey("ID").Value)
                        If dtChanges.Select("Dte = '" & ClsAttendancePreparationDetails.GAttendDate.ToString("dd/MM/yyyy") & "'").Length > 0 Then
                            Calculate_SalaryPerHour(Amount, EmpID, ClsAttendancePreparationDetails.GAttendDate)
                        End If
                        ClsAttendancePreparationDetailsOld.Find("ID = " & DGRow1.Cells.FromKey("ID").Value)

                        ClsAttendancePreparationDetails.TotalLate = DGRow1.Cells.FromKey("TotalLate").Value / 60
                        ClsAttendancePreparationDetails.PermitLate = DGRow1.Cells.FromKey("PermitLate").Value / 60
                        ClsAttendancePreparationDetails.NotpermitLate = DGRow1.Cells.FromKey("NotpermitLate").Value / 60

                        ClsAttendancePreparationDetails.IsVacation = DGRow1.Cells.FromKey("IsVacation").Value
                        'If ClsAttendancePreparationDetails.IsVacation = True Then
                        '    ClsAttendancePreparationDetails.IsAbsent = False
                        'Else
                        ClsAttendancePreparationDetails.IsAbsent = DGRow1.Cells.FromKey("IsAbsent").Value
                        'End If

                        ClsAttendancePreparationDetails.LatPunishment = 0



                        If ClsAttendancePreparationDetails.NotpermitLate > 0 And ClsAttendancePreparationDetails.IsVacation = False Then
                            If ClsClasses.DeductionMethod = 0 Then
                                ClsClassDelay.Find("ClassID = " & ClsClasses.ID & " and " & ClsAttendancePreparationDetails.NotpermitLate * 60 & " between FromMin and ToMin")
                                Dim DT As Data.DataTable = ClsClassDelay.DataSet.Tables(0)
                                If DT.Rows.Count > 0 Then
                                    ObjSalaryPerDay = 0
                                    ObjSalaryPerHour = 0
                                    If ClsClasses.NoOfDaysPerPeriod > 0 Then
                                        ObjSalaryPerDay = dbBasicSalary / ClsClasses.NoOfDaysPerPeriod
                                    Else
                                        ObjSalaryPerDay = dbBasicSalary / (ToDate.Subtract(FromDate).Days + 1)
                                    End If
                                    ObjSalaryPerHour = ObjSalaryPerDay / ClsClasses.WorkHoursPerDay
                                    If ClsClasses.PunishementCalc = 1 Then
                                        ClsAttendancePreparationDetails.LatPunishment += ObjSalaryPerDay * ClsClassDelay.PunishPCT / 100
                                    Else
                                        ClsAttendancePreparationDetails.LatPunishment += (ObjSalaryPerHour * ClsAttendancePreparationDetails.NotpermitLate) * ClsClassDelay.PunishPCT / 100
                                    End If
                                End If
                            Else
                                ClsClassDelay.Find("ClassID = " & ClsClasses.ID & " and " & ClsAttendancePreparationDetails.TotalLate * 60 & " between FromMin and ToMin")
                                Dim DT As Data.DataTable = ClsClassDelay.DataSet.Tables(0)
                                If DT.Rows.Count > 0 Then
                                    ObjSalaryPerDay = 0
                                    ObjSalaryPerHour = 0
                                    If ClsClasses.NoOfDaysPerPeriod > 0 Then
                                        ObjSalaryPerDay = dbBasicSalary / ClsClasses.NoOfDaysPerPeriod
                                    Else
                                        ObjSalaryPerDay = dbBasicSalary / (ToDate.Subtract(FromDate).Days + 1)
                                    End If
                                    ObjSalaryPerHour = ObjSalaryPerDay / ClsClasses.WorkHoursPerDay
                                    If ClsClasses.PunishementCalc = 1 Then
                                        ClsAttendancePreparationDetails.LatPunishment += ObjSalaryPerDay * ClsClassDelay.PunishPCT / 100
                                    Else
                                        ClsAttendancePreparationDetails.LatPunishment += (ObjSalaryPerHour * ClsAttendancePreparationDetails.TotalLate) * ClsClassDelay.PunishPCT / 100
                                    End If
                                End If
                            End If
                        End If

                        If ClsAttendancePreparationDetailsOld.LeavingType = 0 Then
                            ClsAttendancePreparationDetails.LeavingType = DGRow1.Cells.FromKey("LeavingType").Value
                            If DGRow1.Cells.FromKey("LeavingType").AllowEditing = 1 Then
                                If DGRow1.Cells.FromKey("LeavingType").Value <> 0 Then
                                    Dim hrsEmployeesVacations As New Clshrs_EmployeesVacations(Page)
                                    hrsEmployeesVacations.EmployeeID = EmpID
                                    hrsEmployeesVacations.VacationTypeID = DGRow1.Cells.FromKey("LeavingType").Value
                                    hrsEmployeesVacations.ExpectedStartDate = DGRow1.Cells.FromKey("GAttendDate").Value
                                    hrsEmployeesVacations.ExpectedEndDate = DGRow1.Cells.FromKey("GAttendDate").Value
                                    hrsEmployeesVacations.ActualStartDate = DGRow1.Cells.FromKey("GAttendDate").Value
                                    hrsEmployeesVacations.ActualEndDate = DGRow1.Cells.FromKey("GAttendDate").Value
                                    hrsEmployeesVacations.EmployeeRequestRemarks = "مضافة من حركة تجهيز الحضور والإنصراف    Added By Attendance Prepration Transaction"
                                    hrsEmployeesVacations.TotalDays = 1
                                    hrsEmployeesVacations.ConsumDays = 1
                                    hrsEmployeesVacations.RemainingDays = 0
                                    ClsAttendancePreparationDetails.RegComputerID = hrsEmployeesVacations.SaveVacation()
                                End If
                            End If
                        ElseIf ClsAttendancePreparationDetailsOld.LeavingType <> 0 Then
                            ClsAttendancePreparationDetails.LeavingType = DGRow1.Cells.FromKey("LeavingType").Value
                            If ClsAttendancePreparationDetails.LeavingType <> ClsAttendancePreparationDetailsOld.LeavingType Then
                                If ClsAttendancePreparationDetails.LeavingType = 0 And ClsAttendancePreparationDetails.RegComputerID <> Nothing Then
                                    Dim hrsEmployeesVacations As New Clshrs_EmployeesVacations(Page)
                                    hrsEmployeesVacations.Delete("ID = " & ClsAttendancePreparationDetails.RegComputerID)
                                ElseIf ClsAttendancePreparationDetails.LeavingType <> 0 And ClsAttendancePreparationDetails.RegComputerID <> Nothing Then
                                    Dim hrsEmployeesVacations As New Clshrs_EmployeesVacations(Page)
                                    hrsEmployeesVacations.Find("ID = " & ClsAttendancePreparationDetails.RegComputerID)
                                    hrsEmployeesVacations.VacationTypeID = DGRow1.Cells.FromKey("LeavingType").Value
                                    hrsEmployeesVacations.Update("ID = " & ClsAttendancePreparationDetails.RegComputerID)
                                End If
                            End If
                        End If
                        ClsAttendancePreparationDetails.Update("ID = " & DGRow1.Cells.FromKey("ID").Value)
                        If DGRow1.HasChildRows Then
                            For Each DGRow2 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In DGRow1.Rows
                                Dim ClsAttendancePreparationProjects As New ClsAtt_AttendancePreparationProjects(Page)
                                ClsAttendancePreparationProjects.Find("ID = " & DGRow2.Cells.FromKey("ID").Value)
                                ClsAttendancePreparationProjects.ProjectID = DGRow2.Cells.FromKey("ProjectID").Value
                                ClsAttendancePreparationProjects.Overtime = DGRow2.Cells.FromKey("Overtime").Value / 60
                                ClsAttendancePreparationProjects.HolidayHours = DGRow2.Cells.FromKey("HolidayHours").Value / 60
                                ClsAttendancePreparationProjects.IsVacation = DGRow1.Cells.FromKey("IsVacation").Value
                                ClsAttendancePreparationProjects.OTSalary = dbOTSalary
                                ClsAttendancePreparationProjects.HOTSalary = dbHOTSalary
                                'If ClsAttendancePreparationProjects.IsVacation = True Then
                                '    ClsAttendancePreparationProjects.IsAbsent = False
                                'Else
                                ClsAttendancePreparationProjects.IsAbsent = DGRow1.Cells.FromKey("IsAbsent").Value
                                '  End If
                                ClsAttendancePreparationProjects.Update("ID = " & DGRow2.Cells.FromKey("ID").Value)
                            Next
                        End If
                    End If
                Next



                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
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
#End Region

End Class
