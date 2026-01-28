Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeesMonthlyTransactions
    Inherits MainPage

#Region "Public Decleration"

    Private ClsEmployeesTransactions As Clshrs_EmployeesTransactions
    Private ClsEmployeeTransactionsDet As Clshrs_EmployeesTransactionsDetails
    Private ClsContractTransactions As Clshrs_ContractsTransactions
    Private ClsEmployeesTransactionsProjects As Clshrs_EmployeesTransactionsProjects
    Private ClsFisicalPeriods As Clssys_FiscalYearsPeriods
    Private ClsEmployeeClassCalendar As Clshrs_EmployeesClassCalander
    Private ClsVacations As Clshrs_EmployeesVacations

    Private ClsProjects As Clshrs_Projects
    Private ClsEmployees As Clshrs_Employees
    Private ClsEmployeesContracts As Clshrs_Contracts
    Private ClsVacationsTypes As Clshrs_VacationsTypes
    Private ClsEmployeeClass As Clshrs_EmployeeClasses
    Private ObjNavigationHandler As Venus.Shared.Web.NavigationHandler
    Private clsTransType As Clshrs_TransactionsTypes
    Private ClsAttendancePreparationDetails As ClsAtt_AttendancePreparationDetails
    Private clsAttendancePreparationProjects As ClsAtt_AttendancePreparationProjects
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
    Private ObjAbsent As Double

    Private dbOTSalary As Double = 0
    Private dbHOTSalary As Double = 0

    Private IntNoOfDays As Integer
    Private IntNoOfWorkDays As Integer
    Private IntFisicalPeriod As Integer
    Private intNoDecimalPlaces As Integer
    Private intEmployeeContarctID As Integer
    Private ObjSalaryPerDay As Double
    Private ObjSalaryPerHour As Double
    Private ObjOverTime As Double
    Private ObjTotalLate As String

    Private BolPrepared As Boolean = False
    Private penaltyDays As Single = 0
    Private ObjPrepaerdData As ArrayList
    Private totalBenefits As Double = 0
    Private totalDeducation As Double = 0
    Private NotPermitLat As Double = 0
    Private dbTotalAbsent As Double = 0

    Const CPreparedData_calctype As Int16 = 9
    Const CPreparedData_AvaliableDays As Int16 = 8
    Const CPreparedData_Prepared As Int16 = 4
    Const CPreparedData_TotalPenalty As Int16 = 7
    Const CPreparedData_FirstPrepare As Int16 = 5
    Public Enum PaidStatus
        PaidOnly
        NotPaid
        All
    End Enum

#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntFiscalPeriod As Integer = IIf(Request.QueryString.Item("Fisical") <> "", CInt(Request.QueryString.Item("Fisical")), 0)
        Dim IntIncomingEmpId As Integer = IIf(Request.QueryString.Item("ID") <> "", CInt(Request.QueryString.Item("ID")), 0)

        ClsEmployeesTransactions = New Clshrs_EmployeesTransactions(Page)
        If Not IsPostBack Then
            ClsEmployees = New Clshrs_Employees(Page)
            Dim ClsEmployeesLoans As New Clshrs_EmployeesPayabilitySchedules(Page)
            Dim ClsTransTypes As New Clshrs_TransactionsTypes(Page)
            ClsFisicalPeriods = New Clssys_FiscalYearsPeriods(Page)
            ClsProjects = New Clshrs_Projects(Page, " hrs_Projects ")
            ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
            clsMainCurrency = New ClsSys_Currencies(Page)
            ClsCountries = New Clssys_Countries(Page)
            Dim ClsVacationsTypes As New Clshrs_VacationsTypes(Me.Page)
            Dim ClsForms As New ClsSys_Forms(Page)
            Dim clsDepartments As New Clssys_Departments(Page)
            ClsVacations = New Clshrs_EmployeesVacations(Page)

            ClsEmployeesTransactionsProjects = New Clshrs_EmployeesTransactionsProjects(Page)
            Dim ClsCompanies As New Clssys_Companies(Me.Page)
            Dim arrList As New ArrayList

            Dim IntCurrentFiscalPeriod As Integer = 0

            Page.Session.Add("ConnectionString", ClsEmployeesTransactions.ConnectionString)
            ClsCompanies.Find(" ID = " & ClsCompanies.MainCompanyID)

            hdnLang.Value = ObjNavigationHandler.SetLanguage(Page, "Eng/Arb")
            Page.Session.Add("Lang", ObjNavigationHandler.SetLanguage(Page, "Eng/Arb"))
            ClsTransTypes.GetList(uwgEmployeeTransaction.Columns(0).ValueList)
            ClsTransTypes.GetList(uwgPayabilities.Columns(0).ValueList)
            ClsForms.Find("EngName = 'frmEmployees.aspx'")
            ClsFisicalPeriods.GetDropDownList(ddlPeriod, ClsForms.ModuleID, False)
            IntCurrentFiscalPeriod = ClsFisicalPeriods.GetLastOpenedFiscalPieriod(ClsForms.ModuleID)
            ddlPeriod.SelectedIndex = IntCurrentFiscalPeriod
            If IntFiscalPeriod <= 0 Then
                IntFiscalPeriod = IntCurrentFiscalPeriod
            End If
            ddlPeriod.SelectedValue = IntFiscalPeriod
            'Adjust the discimal placess accourding to the main countriy currency
            If ClsCountries.Find(" IsMainCountries = 1 ") Then
                clsMainCurrency.Find(" ID=" & ClsCountries.CurrencyID)
                If Not IsNothing(clsMainCurrency.NoDecimalPlaces) Then
                    uwgEmployeeTransaction.Columns(1).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgEmployeeTransaction.Columns(1).Format, clsMainCurrency.NoDecimalPlaces)
                    uwgPayabilities.Columns(1).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgPayabilities.Columns(1).Format, clsMainCurrency.NoDecimalPlaces)
                End If
            End If
            arrList = GetEmployeeGeneralInfromation(IntIncomingEmpId)
            'Load Prepared Data or load data for preparation
            btnRefund.Enabled = False
            If Not FillPreparedORNewData(True, True, PaidStatus.PaidOnly) Then
                Exit Sub
            End If

            Dim CntDays As Double = 0
            Dim ClsEmpTrans As New Clshrs_EmployeesTransactions(Page)
            Dim clsNav As New Venus.Shared.Web.NavigationHandler(ClsEmpTrans.ConnectionString)
            Dim dsResult As New DataSet
            If ClsEmpTrans.Find("EmployeeID=" & IntIncomingEmpId & " And FiscalYearPeriodID=" & IntFiscalPeriod & " And PrepareType ='N' ") Then
                dsResult = ClsEmpTrans.GetAllDepartmentEmployeesTransactions(ClsEmpTrans.ID, clsNav.SetLanguage(Page, "Eng/Arb"))
            End If

            If dsResult.Tables.Count > 0 Then
                For i As Integer = 0 To dsResult.Tables(0).Rows.Count - 1
                    CntDays = CntDays + dsResult.Tables(0).Rows(i)("NumberofWorkingDays")
                Next

                If Math.Round(CntDays, 1) <> IIf(txtNoOfWorkingUnits.Text = "", 0, txtNoOfWorkingUnits.Text) Or CntDays = 0 Then
                    lbError.Text = ObjNavigationHandler.SetLanguage(Page, "Must be prepare attendance first/يجب تجهيز الحضور والانصراف أولا")
                    lbError.Visible = True
                    btnSave.Enabled = False
                Else
                    lbError.Text = ""
                End If
            Else
                If IIf(txtNoOfWorkingUnits.Text = "", 0, txtNoOfWorkingUnits.Text) = 0 Then
                    lbError.Text = ObjNavigationHandler.SetLanguage(Page, "Must be prepare attendance first/يجب تجهيز الحضور والانصراف أولا")
                    lbError.Visible = True
                    btnSave.Enabled = False
                Else
                    lbError.Text = ""
                End If
            End If
        End If ' End of not post back
        Dim BolPrepared As Boolean = False
        BolPrepared = ClsEmployeesTransactions.Find("EmployeeID=" & txtEmployeeID.Value & " And FiscalYearPeriodID = " & ddlPeriod.SelectedValue & " And PrepareType='N'")
        If BolPrepared Then
            btnSave.Enabled = False
        End If
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click
        Dim IntEmployeeID As Integer = txtEmployeeID.Value
        Dim IntFisicalYearPeriod As Integer = ddlPeriod.SelectedValue
        Dim BolPrepared As Boolean = False
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Me)
        Dim ClsEmployeeTransactionsDet As New Clshrs_EmployeesTransactionsDetails(Me)
        Dim ClsEmployeeesPayablitySetelment As New Clshrs_EmployeesPayabilitySchedulesSettlement(Me)
        Dim ClsEmployeesTransactionsProjects As New Clshrs_EmployeesTransactionsProjects(Me)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployeeesPayablitySetelment.ConnectionString)
        BolPrepared = ClsEmployeesTransactions.Find("EmployeeID=" & IntEmployeeID & " And FiscalYearPeriodID = " & IntFisicalYearPeriod & " And PrepareType='N'")
        Dim arrList As New ArrayList
        If Not Load_ClsLayers() Then Exit Sub
        arrList = GetEmployeeGeneralInfromation(IntEmployeeID)
        Try
            If txtNoOfWorkingUnits.Text > 0 Then
                Dim BenTotVal As Double = 0
                For Each ObjRowDet As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeeTransaction.Rows
                    If ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid" Or ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid By Days" Or ObjRowDet.Cells.FromKey("DescriptionSign").Value = "By Project" Then
                        If Not ObjRowDet.Cells(1).Value Is DBNull.Value AndAlso Val(ObjRowDet.Cells(1).Value) > 0 Then
                            BenTotVal += ObjRowDet.Cells(1).Value
                        End If
                    End If
                Next
                lblTotalBenefits.Text = Math.Round(BenTotVal, Get_NoDecimalPlaces())
                Dim DedTotVal As Double = 0
                For Each ObjRowDet In uwgPayabilities.Rows
                    If ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid" Or ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid By Days" Or ObjRowDet.Cells.FromKey("DescriptionSign").Value = "By Project" Then

                        If Not ObjRowDet.Cells(1).Value Is DBNull.Value AndAlso Val(ObjRowDet.Cells(1).Value) > 0 Then
                            DedTotVal += ObjRowDet.Cells(1).Value
                        End If
                    End If
                Next
                lblTotalDeductions.Text = Math.Round(DedTotVal, Get_NoDecimalPlaces())
                lblNetSalary.Text = Math.Round(BenTotVal - DedTotVal, Get_NoDecimalPlaces())

                Dim comparevalue As Integer = 0
                Try
                    comparevalue = ConfigurationManager.AppSettings("AllowZero")
                Catch ex As Exception
                End Try

                Dim PrepareData As Boolean = False
                If comparevalue = -1 Then
                    Select Case BolPrepared
                        Case False
                            PrepareData = True
                    End Select
                ElseIf comparevalue = 1 Then
                    If BenTotVal - DedTotVal >= 0 Then
                        Select Case BolPrepared
                            Case False
                                PrepareData = True
                        End Select
                    End If
                Else
                    If BenTotVal - DedTotVal > 0 Then
                        Select Case BolPrepared
                            Case False
                                PrepareData = True
                        End Select
                    End If
                End If
                If PrepareData = True Then
                    ClsEmployees.Find("ID = " & IntEmployeeID)
                    Dim cmdString As String = ""
                    Dim clsProject As New Clshrs_Projects(Page, "hrs_Projects")
                    Dim Str As String = "select ProjectID,isnull(LinkedCS,'') LinkedCS,COUNT(ID) PTotal,(select COUNT(ID) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where LeavingType not in (select ID from hrs_VacationsTypes where IsAnnual = 1) and EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))) ATotal from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where LeavingType not in (select ID from hrs_VacationsTypes where IsAnnual = 1) and EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) group by ProjectID,isnull(LinkedCS,'')"
                    Dim DTProjects As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, Str).Tables(0)
                    Dim AllWorkingUnits As Double = 0
                    If ClsEmployees.JoinDate > ToDate And ClsEmployees.JoinDate <= FiscToDate And DTProjects.Rows.Count = 0 Then
                        IntNoOfWorkDays = FiscToDate.Subtract(ClsEmployees.JoinDate).Days + 1
                        If clsProject.Find("ID=" & ClsEmployeeClass.DefaultProjectID) Then
                            cmdString &= "insert into hrs_EmployeesTransactions(EmployeeID,FinancialWorkingUnits,FiscalYearPeriodID,PrepareType,Applyed,CBranchID,CDepartmetnID,CSectorID,CCost1,CCost2,CCost3,CCost4,CMainProjectID)Values(" & IntEmployeeID & "," & txtNoOfWorkingUnits.Text & "," & IntFisicalYearPeriod & ",'N',0," & ClsEmployees.BranchID & "," & ClsEmployees.DepartmentID & "," & ClsEmployees.SectorID & "," & ClsEmployees.Cost1 & "," & ClsEmployees.Cost2 & "," & ClsEmployees.Cost3 & "," & ClsEmployees.Cost4 & "," & ClsEmployeeClass.DefaultProjectID & ");" & vbNewLine
                            cmdString &= "Set @TransID = (Select IDENT_CURRENT('hrs_EmployeesTransactions'));" & vbNewLine
                            cmdString &= " Insert Into hrs_EmployeesTransactionsProjects([EmployeeTransactionID],[ProjectID],[WorkingUnits],[RegUserID]) " & _
                                         " Select @TransID," & clsProject.ID & "," & IntNoOfWorkDays & "," & ClsEmployees.DataBaseUserRelatedID & ";" & "  Set @ProjectTransID = (Select IDENT_CURRENT('hrs_EmployeesTransactionsProjects')); "
                            For Each ObjRowDet In uwgEmployeeTransaction.Rows
                                If ObjRowDet.Cells.FromKey("DescriptionSign").Value <> "By Project" Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                                         " Values (@ProjectTransID," & ObjRowDet.Cells.FromKey("TransactionTypeID").Value & "," & ObjRowDet.Cells.FromKey("Value").Value & ", '" & ObjRowDet.Cells.FromKey("DescriptionSign").Value & "','" & IIf(IsDBNull(ObjRowDet.Cells.FromKey("EmpSchId")), "Null", ObjRowDet.Cells.FromKey("EmpSchId").Value) & "'); "
                                    If ObjRowDet.Cells.FromKey("EmpSchId").Value <> "" Then
                                        cmdString &= " Insert Into hrs_EmployeesPayabilitiesSchedulesSettlement (EmployeePayabilityScheduleID,EmployeeTransactionID,Amount)" & _
                                                                                                            " Values (" & ObjRowDet.Cells.FromKey("EmpSchId").Value & ",@ProjectTransID," & ObjRowDet.Cells.FromKey("Value").Value & "); "
                                    End If
                                End If
                            Next
                            For Each ObjRowDet In uwgPayabilities.Rows
                                If ObjRowDet.Cells.FromKey("DescriptionSign").Value <> "By Project" Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                                         " Values (@ProjectTransID," & ObjRowDet.Cells.FromKey("TransactionTypeID").Value & "," & ObjRowDet.Cells.FromKey("Value").Value & ", '" & ObjRowDet.Cells.FromKey("DescriptionSign").Value & "','" & IIf(IsDBNull(ObjRowDet.Cells.FromKey("EmpSchId")), "Null", ObjRowDet.Cells.FromKey("EmpSchId").Value) & "'); "
                                    If ObjRowDet.Cells.FromKey("EmpSchId").Value <> "" Then
                                        cmdString &= " Insert Into hrs_EmployeesPayabilitiesSchedulesSettlement (EmployeePayabilityScheduleID,EmployeeTransactionID,Amount)" & _
                                                                                                            " Values (" & ObjRowDet.Cells.FromKey("EmpSchId").Value & ",@ProjectTransID," & ObjRowDet.Cells.FromKey("Value").Value & "); "
                                    End If
                                End If
                            Next
                        End If
                    ElseIf txtNoOfWorkingUnits.Text > 0 And DTProjects.Rows.Count = 0 Then
                        IntNoOfWorkDays = txtNoOfWorkingUnits.Text
                        If clsProject.Find("ID=" & ClsEmployeeClass.DefaultProjectID) Then
                            cmdString &= "insert into hrs_EmployeesTransactions(EmployeeID,FinancialWorkingUnits,FiscalYearPeriodID,PrepareType,Applyed,CBranchID,CDepartmetnID,CSectorID,CCost1,CCost2,CCost3,CCost4,CMainProjectID)Values(" & IntEmployeeID & "," & txtNoOfWorkingUnits.Text & "," & IntFisicalYearPeriod & ",'N',0," & ClsEmployees.BranchID & "," & ClsEmployees.DepartmentID & "," & ClsEmployees.SectorID & "," & ClsEmployees.Cost1 & "," & ClsEmployees.Cost2 & "," & ClsEmployees.Cost3 & "," & ClsEmployees.Cost4 & "," & ClsEmployeeClass.DefaultProjectID & ");" & vbNewLine
                            cmdString &= "Set @TransID = (Select IDENT_CURRENT('hrs_EmployeesTransactions'));" & vbNewLine
                            cmdString &= " Insert Into hrs_EmployeesTransactionsProjects([EmployeeTransactionID],[ProjectID],[WorkingUnits],[RegUserID]) " & _
                                         " Select @TransID," & clsProject.ID & "," & IntNoOfWorkDays & "," & ClsEmployees.DataBaseUserRelatedID & ";" & "  Set @ProjectTransID = (Select IDENT_CURRENT('hrs_EmployeesTransactionsProjects')); "
                            For Each ObjRowDet In uwgEmployeeTransaction.Rows
                                If ObjRowDet.Cells.FromKey("DescriptionSign").Value <> "By Project" Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                                         " Values (@ProjectTransID," & ObjRowDet.Cells.FromKey("TransactionTypeID").Value & "," & ObjRowDet.Cells.FromKey("Value").Value & ", '" & ObjRowDet.Cells.FromKey("DescriptionSign").Value & "','" & IIf(IsDBNull(ObjRowDet.Cells.FromKey("EmpSchId")), "Null", ObjRowDet.Cells.FromKey("EmpSchId").Value) & "'); "
                                    If ObjRowDet.Cells.FromKey("EmpSchId").Value <> "" Then
                                        cmdString &= " Insert Into hrs_EmployeesPayabilitiesSchedulesSettlement (EmployeePayabilityScheduleID,EmployeeTransactionID,Amount)" & _
                                                                                                            " Values (" & ObjRowDet.Cells.FromKey("EmpSchId").Value & ",@ProjectTransID," & ObjRowDet.Cells.FromKey("Value").Value & "); "
                                    End If
                                End If
                            Next
                            For Each ObjRowDet In uwgPayabilities.Rows
                                If ObjRowDet.Cells.FromKey("DescriptionSign").Value <> "By Project" Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                                         " Values (@ProjectTransID," & ObjRowDet.Cells.FromKey("TransactionTypeID").Value & "," & ObjRowDet.Cells.FromKey("Value").Value & ", '" & ObjRowDet.Cells.FromKey("DescriptionSign").Value & "','" & IIf(IsDBNull(ObjRowDet.Cells.FromKey("EmpSchId")), "Null", ObjRowDet.Cells.FromKey("EmpSchId").Value) & "'); "
                                    If ObjRowDet.Cells.FromKey("EmpSchId").Value <> "" Then
                                        cmdString &= " Insert Into hrs_EmployeesPayabilitiesSchedulesSettlement (EmployeePayabilityScheduleID,EmployeeTransactionID,Amount)" & _
                                                                                                            " Values (" & ObjRowDet.Cells.FromKey("EmpSchId").Value & ",@ProjectTransID," & ObjRowDet.Cells.FromKey("Value").Value & "); "
                                    End If
                                End If
                            Next
                        End If
                    End If
                    If DTProjects.Rows.Count > 0 Then
                        cmdString &= "insert into hrs_EmployeesTransactions(EmployeeID,FinancialWorkingUnits,FiscalYearPeriodID,PrepareType,Applyed,CBranchID,CDepartmetnID,CSectorID,CCost1,CCost2,CCost3,CCost4,CMainProjectID)Values(" & IntEmployeeID & "," & txtNoOfWorkingUnits.Text & "," & IntFisicalYearPeriod & ",'N',0," & ClsEmployees.BranchID & "," & ClsEmployees.DepartmentID & "," & ClsEmployees.SectorID & "," & ClsEmployees.Cost1 & "," & ClsEmployees.Cost2 & "," & ClsEmployees.Cost3 & "," & ClsEmployees.Cost4 & "," & ClsEmployeeClass.DefaultProjectID & ");" & vbNewLine
                        cmdString &= "Set @TransID = (Select IDENT_CURRENT('hrs_EmployeesTransactions'));" & vbNewLine
                    End If
                    For i As Integer = 0 To DTProjects.Rows.Count - 1
                        If clsProject.Find("ID=" & DTProjects.Rows(i)("ProjectID").ToString()) Then
                            cmdString &= " Insert Into hrs_EmployeesTransactionsProjects([EmployeeTransactionID],[ProjectID],[WorkingUnits],[RegUserID],[LinkedCS]) " & _
                                         " Select @TransID," & clsProject.ID & "," & (txtNoOfWorkingUnits.Text * DTProjects.Rows(i)("PTotal") / DTProjects.Rows(i)("ATotal")) & "," & ClsEmployees.DataBaseUserRelatedID & ",'" & DTProjects.Rows(i)("LinkedCS") & "';" & "  Set @ProjectTransID = (Select IDENT_CURRENT('hrs_EmployeesTransactionsProjects')); "
                            For Each ObjRowDet In uwgEmployeeTransaction.Rows
                                If ObjRowDet.Cells.FromKey("DescriptionSign").Value <> "By Project" Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                                         " Values (@ProjectTransID," & ObjRowDet.Cells.FromKey("TransactionTypeID").Value & "," & (ObjRowDet.Cells.FromKey("Value").Value * DTProjects.Rows(i)("PTotal") / DTProjects.Rows(i)("ATotal")) & ", '" & ObjRowDet.Cells.FromKey("DescriptionSign").Value & "'," & IIf(String.IsNullOrEmpty(ObjRowDet.Cells.FromKey("EmpSchId").Value), "Null", ObjRowDet.Cells.FromKey("EmpSchId").Value) & "); "
                                    If Convert.ToString(ObjRowDet.Cells.FromKey("EmpSchId").Value) <> "" Then
                                        cmdString &= " Insert Into hrs_EmployeesPayabilitiesSchedulesSettlement (EmployeePayabilityScheduleID,EmployeeTransactionID,Amount)" & _
                                                                                                            " Values (" & ObjRowDet.Cells.FromKey("EmpSchId").Value & ",@ProjectTransID," & (ObjRowDet.Cells.FromKey("Value").Value * DTProjects.Rows(i)("PTotal") / DTProjects.Rows(i)("ATotal")) & "); "
                                    End If
                                End If
                            Next
                            For Each ObjRowDet In uwgPayabilities.Rows
                                If ObjRowDet.Cells.FromKey("DescriptionSign").Value <> "By Project" Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                                         " Values (@ProjectTransID," & ObjRowDet.Cells.FromKey("TransactionTypeID").Value & "," & (ObjRowDet.Cells.FromKey("Value").Value * DTProjects.Rows(i)("PTotal") / DTProjects.Rows(i)("ATotal")) & ", '" & ObjRowDet.Cells.FromKey("DescriptionSign").Value & "'," & IIf(String.IsNullOrEmpty(ObjRowDet.Cells.FromKey("EmpSchId").Value), "Null", ObjRowDet.Cells.FromKey("EmpSchId").Value) & "); "
                                    If Convert.ToString(ObjRowDet.Cells.FromKey("EmpSchId").Value) <> "" Then
                                        cmdString &= " Insert Into hrs_EmployeesPayabilitiesSchedulesSettlement (EmployeePayabilityScheduleID,EmployeeTransactionID,Amount)" & _
                                                                                                            " Values (" & ObjRowDet.Cells.FromKey("EmpSchId").Value & ",@ProjectTransID," & (ObjRowDet.Cells.FromKey("Value").Value * DTProjects.Rows(i)("PTotal") / DTProjects.Rows(i)("ATotal")) & "); "
                                    End If
                                End If
                            Next
                            Dim Extrastrcommand As String = "select (isnull((select top 1 ID from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode),0)) as RelTransactionID,(isnull((select top 1 Sign from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode),0)) as Sign,Amount from hrs_EmployeeExtraItems"
                            Dim strFilter As String = " where EmployeeCode = '" & ClsEmployees.Code & "' and Status = 1 and isnull(LinkedCS,'') = '' and FiscalPeriodID =" & ClsFisicalPeriods.ID
                            Dim dsEmployee As New Data.DataSet
                            dsEmployee = New Data.DataSet
                            dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, Data.CommandType.Text, Extrastrcommand & strFilter)
                            For EXB As Integer = 0 To dsEmployee.Tables(0).Rows.Count - 1
                                cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                             " Values (@ProjectTransID," & dsEmployee.Tables(0).Rows(EXB)(0) & "," & (dsEmployee.Tables(0).Rows(EXB)(2) * DTProjects.Rows(i)("PTotal") / DTProjects.Rows(i)("ATotal")) & ", 'Paid',Null); "
                            Next

                            Dim latHrs As Decimal = 0
                            'If ClsEmployeeClass.NonPermiLatTransaction > 0 Then
                            'Dim strcommand As String = "select isnull(sum(A.LatPunishment),0) from Att_AttendancePreparationDetails A inner join Att_AttendancePreparationProjects B on A.ID = B.TrnsID where A.EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,A.GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,A.GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103) and isnull(B.LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and B.ProjectID = " & clsProject.ID
                            'Dim latvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                            'strcommand = "select isnull(sum(A.NotpermitLate),0) from Att_AttendancePreparationDetails A inner join Att_AttendancePreparationProjects B on A.ID = B.TrnsID where A.EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,A.GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,A.GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103) and isnull(B.LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and B.ProjectID = " & clsProject.ID
                            'latHrs = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                            'If latvalue > 0 Then
                            '    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                            '                 " Values (@ProjectTransID," & ClsEmployeeClass.NonPermiLatTransaction & "," & latvalue & ", 'Paid',Null); "
                            'End If
                            'End If
                            Dim OTHrs As Decimal = 0
                            'If ClsEmployeeClass.OvertimeTransaction > 0 Then
                            '    Dim strcommand As String = "select isnull(sum(Overtime * OTSalary),0) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                            '    Dim OTvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                            '    strcommand = "select isnull(sum(Overtime),0) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                            '    OTHrs = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                            '    If OTvalue > 0 Then
                            '        cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                            '                     " Values (@ProjectTransID," & ClsEmployeeClass.OvertimeTransaction & "," & OTvalue & ", 'Paid',Null); "
                            '    End If
                            'End If
                            Dim HOTHrs As Decimal = 0
                            If ClsEmployeeClass.HOvertimeTransaction > 0 Then
                                Dim strcommand As String = "select isnull(sum(HolidayHours * HOTSalary),0) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                Dim HOTvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                strcommand = "select isnull(sum(HolidayHours),0) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                HOTHrs = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                If HOTvalue > 0 Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                                 " Values (@ProjectTransID," & ClsEmployeeClass.HOvertimeTransaction & "," & HOTvalue & ", 'Paid',Null); "
                                End If
                            End If
                            Dim AbsDys As Decimal = 0
                            If ClsEmployeeClass.RegComputerID > 0 Then
                                Dim strcommand As String = "select isnull(sum(SalaryPerDay),0) from Att_AttendancePreparationProjects where isnull(IsAbsent,0) = 1 and isnull(IsVacation,0) = 0 and TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                Dim Absvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                strcommand = "select isnull(count(ID),0) from Att_AttendancePreparationProjects where isnull(IsAbsent,0) = 1 and isnull(IsVacation,0) = 0 and TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                AbsDys = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                If Absvalue > 0 Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                                 " Values (@ProjectTransID," & ClsEmployeeClass.RegComputerID & "," & Absvalue & ", 'Paid',Null); "
                                End If
                            End If

                            cmdString &= "update hrs_EmployeesTransactionsProjects set OvertimeHours = " & OTHrs & ",HoliDayOvertimeHours = " & HOTHrs & ",AbsentDays = " & AbsDys & ",LatHours = " & latHrs & " where ID = @ProjectTransID;"

                            Extrastrcommand = "select (isnull((select top 1 ID from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode),0)) as RelTransactionID,(isnull((select top 1 Sign from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode),0)) as Sign,Amount from hrs_EmployeeExtraItems"
                            strFilter = " where EmployeeCode = '" & ClsEmployees.Code & "' and Status = 1 and LTRIM(RTRIM(ProjectID)) = '" & clsProject.ID & "' and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and FiscalPeriodID =" & ClsFisicalPeriods.ID
                            dsEmployee = New Data.DataSet
                            dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, Data.CommandType.Text, Extrastrcommand & strFilter)
                            For EXB As Integer = 0 To dsEmployee.Tables(0).Rows.Count - 1
                                cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                             " Values (@ProjectTransID," & dsEmployee.Tables(0).Rows(EXB)(0) & "," & dsEmployee.Tables(0).Rows(EXB)(2) & ", 'Paid',Null); "
                            Next

                            If clsProject.LateTransaction > 0 Then
                                Dim strcommand As String = "select sum(LatPunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                Dim PLatvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                If PLatvalue > 0 Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                                 " Values (@ProjectTransID," & clsProject.LateTransaction & "," & PLatvalue & ", 'Paid',Null); "
                                End If
                            End If
                            If clsProject.AbsentTransaction > 0 Then
                                Dim strcommand As String = "select sum(AbsentPunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                Dim PAbsvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                If PAbsvalue > 0 Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                                 " Values (@ProjectTransID," & clsProject.AbsentTransaction & "," & PAbsvalue & ", 'Paid',Null); "
                                End If
                            End If
                            If clsProject.SickTransaction > 0 Then
                                Dim strcommand As String = "select sum(SickPunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                Dim Psikvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                If Psikvalue > 0 Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                                 " Values (@ProjectTransID," & clsProject.SickTransaction & "," & Psikvalue & ", 'Paid',Null); "
                                End If
                            End If
                            If clsProject.LeaveTransaction > 0 Then
                                Dim strcommand As String = "select sum(LeavePunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                Dim Pleavvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                If Pleavvalue > 0 Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                                 " Values (@ProjectTransID," & clsProject.LeaveTransaction & "," & Pleavvalue & ", 'Paid',Null); "
                                End If
                            End If
                            If clsProject.OTTransaction > 0 Then
                                Dim strcommand As String = "select sum(OTFactor * Overtime * SalaryPerHour) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                Dim Potvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                If Potvalue > 0 Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                                 " Values (@ProjectTransID," & clsProject.OTTransaction & "," & Potvalue & ", 'Paid',Null); "
                                End If
                            End If
                            If clsProject.HOTTransaction > 0 Then
                                Dim strcommand As String = "select sum(HOTFactor * HolidayHours * SalaryPerHour) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
                                Dim Photvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                                If Photvalue > 0 Then
                                    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                                                 " Values (@ProjectTransID," & clsProject.HOTTransaction & "," & Photvalue & ", 'Paid',Null); "
                                End If
                            End If
                        End If
                    Next
                    If cmdString <> "" Then
                        Dim mSqlCommand As New SqlClient.SqlCommand
                        mSqlCommand.Connection = New Data.SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                        mSqlCommand.CommandType = Data.CommandType.Text
                        mSqlCommand.CommandText = "BEGIN Transaction SalaryTrns; Declare @ProjectTransID Int; Declare @TransID Int;" & vbNewLine & cmdString & " Commit Transaction SalaryTrns;"
                        mSqlCommand.Connection.Open()
                        mSqlCommand.ExecuteNonQuery()
                        mSqlCommand.Connection.Close()
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Invalid Save/لم يتم الحفظ"))
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnRefund_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnRefund.Click
        Try
            Dim IntEmployeeID As Integer = txtEmployeeID.Value
            Dim IntFisicalYearPeriod As Integer = ddlPeriod.SelectedValue
            Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Me)
            Dim ClsEmployeeTransactionsDet As New Clshrs_EmployeesTransactionsDetails(Me)
            Dim ClsEmployeeesPayablitySetelment As New Clshrs_EmployeesPayabilitySchedulesSettlement(Me)
            Dim ClsEmployeesTransactionsProjects As New Clshrs_EmployeesTransactionsProjects(Me)
            ClsEmployees = New Clshrs_Employees(Page)
            Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployeeesPayablitySetelment.ConnectionString)

            uwgEmployeeTransaction.DataSource = Nothing
            uwgEmployeeTransaction.DataBind()
            uwgPayabilities.DataSource = Nothing
            uwgPayabilities.DataBind()
            ClsEmployeesTransactions.Find("EmployeeID=" & IntEmployeeID & " And FiscalYearPeriodID = " & IntFisicalYearPeriod & " And PrepareType='N'")
            ClsEmployeeTransactionsDet.DeleteAll("EmployeeTransactionID=" & ClsEmployeesTransactions.ID)
            ClsEmployeesTransactionsProjects.DeleteAll("EmployeeTransactionID=" & ClsEmployeesTransactions.ID)
            ClsEmployees.DeleteEmployeesPenalties(ClsEmployeesTransactions.ID)
            ClsEmployeesTransactions.DeleteAll("ID=" & ClsEmployeesTransactions.ID)

            FillPreparedORNewData(True)
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Refund Done/تم الاستعادة"))
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ChkPaid_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPaid.CheckedChanged
        If ChkPaid.Checked Then
            FillPreparedORNewData(True, True, PaidStatus.PaidOnly)
        ElseIf Not ChkPaid.Checked Then
            FillPreparedORNewData(True, True, PaidStatus.All)
        End If
    End Sub
    Protected Sub uwgEmployeeTransaction_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgEmployeeTransaction.InitializeRow
        Dim clsTransType As New Clshrs_TransactionsTypes(Page)
        'If clsTransType.Find("ID=" & e.Row.Cells(0).Value) Then
        '    If e.Row.Cells(6).Value = "By Project" Or clsTransType.InputIsNumeric = False Or clsTransType.Formula.Trim <> String.Empty Or clsTransType.BeginContractFormula.Trim <> String.Empty Or clsTransType.EndContractFormula.Trim <> String.Empty Then
        '        e.Row.Cells(1).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
        '    End If
        'End If

        If clsTransType.Find("ID=" & e.Row.Cells(0).Value) Then
            If clsTransType.InputIsNumeric = False Then

                e.Row.Cells(1).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            End If
        End If
    End Sub
    Protected Sub uwgPayabilities_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgPayabilities.InitializeRow
        Dim clsTransType As New Clshrs_TransactionsTypes(Page)
        If clsTransType.Find("ID=" & e.Row.Cells(0).Value) Then
            'If e.Row.Cells(5).Value = "By Project" Or clsTransType.InputIsNumeric = False Or clsTransType.Formula.Trim <> String.Empty Or clsTransType.BeginContractFormula.Trim <> String.Empty Or clsTransType.EndContractFormula.Trim <> String.Empty Then
            If clsTransType.InputIsNumeric = False Or clsTransType.Formula.Trim <> String.Empty Or clsTransType.BeginContractFormula.Trim <> String.Empty Or clsTransType.EndContractFormula.Trim <> String.Empty Then
                e.Row.Cells(1).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            End If
        End If
    End Sub
#End Region

#Region "Private Function"
    Private Function GetEmployeeGeneralInfromation(ByVal intEmployeeID As Object) As ArrayList
        Dim EmpInfoarrList As New ArrayList
        Dim IntFiscalPeriod As Integer = ddlPeriod.SelectedItem.Value
        Dim IntContractID As Integer
        Dim IntEmpClassID As Integer
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsContractTransaction As New Clshrs_ContractsTransactions(Page)
        Dim ClsContract As New Clshrs_Contracts(Page)
        Dim clsTransactionsTypes As New Clshrs_TransactionsTypes(Page)
        ClsEmployeeClass = New Clshrs_EmployeeClasses(Page)

        ClsEmployees.Find("ID=" & intEmployeeID)
        txtEmployeeCode.Text = ClsEmployees.Code
        lblDescEnglishName.Text = ClsEmployees.Name
        IntEmpClassID = ClsContract.EmployeeClassID

        IntContractID = ClsContract.ContractValidatoinId(intEmployeeID, IntFiscalPeriod)
        ClsContract.Find(" ID = " & IntContractID)
        IntEmpClassID = ClsContract.EmployeeClassID

        ClsEmployeeClass.Find("ID=" & IntEmpClassID)
        EmpInfoarrList.Add(intEmployeeID)
        EmpInfoarrList.Add(IntContractID)
        EmpInfoarrList.Add(ClsEmployeeClass.WorkHoursPerDay)

        CalculateEmployeeSalaryDetails(IntEmpClassID, EmpInfoarrList, IntFiscalPeriod)
        Return EmpInfoarrList

    End Function

    Private Sub CalculateEmployeeSalaryDetails(ByVal EmpClassID As Integer, ByRef arrList As ArrayList, ByVal intFiscalPeriodid As Integer)
        Dim SinWorkHours As Single
        Dim SinOverTimeFactor As Single
        ClsEmployeeClass = New Clshrs_EmployeeClasses(Page)
        Dim ClsFiscalPeriods As New Clssys_FiscalYearsPeriods(Page)

        ClsEmployeeClass.Find(" ID= " & EmpClassID)
        SinWorkHours = ClsEmployeeClass.WorkHoursPerDay
        SinOverTimeFactor = IIf(ClsEmployeeClass.OvertimeFactor.ToString = "", 0, ClsEmployeeClass.OvertimeFactor)
        arrList.Add(SinWorkHours)
    End Sub

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
                    Return DateDiff(DateInterval.Day, FromDate, ToDate.AddDays(1))
                End If
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function Get_FromToDate() As Boolean
        Try
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
                lbBasicSalary.Text = ObjNavigationHandler.SetLanguage(Me, "Basic Salary/الراتب الأساسي")
            ElseIf clsCompanies.SalaryCalculation = 1 Then        'Get Total Salary By Days
                dbBasicSalary = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployeeClass.ConnectionString, Data.CommandType.Text, "set dateformat dmy; select dbo.fn_GetTotalAdditions(" & ClsEmployeesContracts.ContractValidatoinId(empID, TrnsDate) & ",'" & TrnsDate.ToString("dd/MM/yyyy") & "')")
                Amount = dbBasicSalary
                lbBasicSalary.Text = ObjNavigationHandler.SetLanguage(Me, "Total Package/إجمالي الراتب")
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
            ClsSolver.NoOfDaysPerPeriod = ClsEmployeeClass.NoOfDaysPerPeriod
            ClsSolver.NoOfWorkingDays = ClsEmployeeClass.NoOfDaysPerPeriod
            ClsSolver.EvaluateExpression(ClsTransactionsTypes.Formula, 0)
            ObjAbsent = ClsSolver.Output
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SetScreenSetting(EmpID As Integer) As Boolean
        Try
            hdnContractID.Value = intEmployeeContarctID
            txtNoOfWorkingUnits.Text = IntNoOfDays
            lblCurrentPeriod.Text = ObjNavigationHandler.SetLanguage(Me, ClsFisicalPeriods.EngName & "/" & ClsFisicalPeriods.ArbName)
            arrList = GetEmployeeGeneralInfromation(ClsEmployees.ID)
            txtWorkingHoursPerDay.Text = arrList(3)
            ObjPrepaerdData = ClsEmployees.GetPreparedEmployessForSalariesByEmployeeID(IntFisicalPeriod, EmpID, FromDate, ToDate, FiscFromDate, FiscToDate)

            btnRefund.Enabled = ObjPrepaerdData(CPreparedData_Prepared)
            btnSave.Enabled = IIf(ObjPrepaerdData(CPreparedData_Prepared) = True, False, True)
            SwitchMsg(IIf(ObjPrepaerdData(CPreparedData_TotalPenalty) > 0, True, False), ObjPrepaerdData(CPreparedData_TotalPenalty))

            Dim Amount As Double = 0
            Calculate_SalaryPerHour(Amount, EmpID, ToDate)

            ObjSalaryPerDay = Math.Round(Amount / IntNoOfDays, intNoDecimalPlaces)
            ObjSalaryPerHour = Math.Round(ObjSalaryPerDay / ClsEmployeeClass.WorkHoursPerDay, intNoDecimalPlaces)
            ObjOverTime = Math.Round(ObjSalaryPerHour * ClsEmployeeClass.OvertimeFactor, intNoDecimalPlaces)

            lblBasicSalary.Text = Math.Round(Amount, intNoDecimalPlaces)
            
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValue(ByVal paidOption As PaidStatus) As Boolean
        Try
            ChkPaid.Visible = True
            hdnPrepared.Value = 1
            txtNoOfWorkingUnits.Text = ClsEmployeesTransactions.GetFinancialWorkingUnits(ClsEmployees.ID, ClsFisicalPeriods.ID)
            GetAllPreparedData(ClsEmployees.ID, ClsFisicalPeriods.ID, dblBenefits, dblDeduct, paidOption)
            txtNoOfWorkingUnits.Text = txtNoOfWorkingUnits.Text
            btnSave.Enabled = True
            btnRefund.Enabled = True
            If ClsEmployeesTransactions.Find("EmployeeID=" & ClsEmployees.ID & " And FiscalYearPeriodID = " & ClsFisicalPeriods.ID & " And PrepareType='N' and PostDate is not null") = True Then
                btnSave.Enabled = False
                btnRefund.Enabled = False
                uwgEmployeeTransaction.DisplayLayout.Bands(0).Columns.FromKey("Value").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgEmployeeTransaction.DisplayLayout.Bands(0).Columns.FromKey("Description").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgPayabilities.DisplayLayout.Bands(0).Columns.FromKey("Value").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgPayabilities.DisplayLayout.Bands(0).Columns.FromKey("Description").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            ElseIf ClsEmployeesTransactions.Find("EmployeeID=" & ClsEmployees.ID & " And FiscalYearPeriodID = " & ClsFisicalPeriods.ID & " And PrepareType='N' and ID in (select isnull(RegComputerID,0) from hrs_EmployeesTransactions)") = True Then
                btnSave.Enabled = False
                btnRefund.Enabled = False
                uwgEmployeeTransaction.DisplayLayout.Bands(0).Columns.FromKey("Value").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgEmployeeTransaction.DisplayLayout.Bands(0).Columns.FromKey("Description").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgPayabilities.DisplayLayout.Bands(0).Columns.FromKey("Value").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgPayabilities.DisplayLayout.Bands(0).Columns.FromKey("Description").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            ElseIf ClsEmployeesTransactions.Find("EmployeeID=" & ClsEmployees.ID & " And FiscalYearPeriodID = " & ClsFisicalPeriods.ID & " And PrepareType='N' and isnull(Applyed,0) = 1") = True Then
                btnSave.Enabled = False
                btnRefund.Enabled = False
                uwgEmployeeTransaction.DisplayLayout.Bands(0).Columns.FromKey("Value").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgEmployeeTransaction.DisplayLayout.Bands(0).Columns.FromKey("Description").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgPayabilities.DisplayLayout.Bands(0).Columns.FromKey("Value").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgPayabilities.DisplayLayout.Bands(0).Columns.FromKey("Description").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function Load_ClsLayers() As Boolean
        Try
            ClsFisicalPeriods = New Clssys_FiscalYearsPeriods(Page)
            ClsProjects = New Clshrs_Projects(Page, " hrs_Projects ")
            ClsEmployees = New Clshrs_Employees(Page)
            ClsEmployeesContracts = New Clshrs_Contracts(Page)
            ClsVacationsTypes = New Clshrs_VacationsTypes(Page)
            ClsEmployeeClass = New Clshrs_EmployeeClasses(Page)
            ClsEmployeesTransactions = New Clshrs_EmployeesTransactions(Page)
            ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
            ClsEmployeesTransactionsProjects = New Clshrs_EmployeesTransactionsProjects(Page)
            clsTransType = New Clshrs_TransactionsTypes(Page)
            clsCompanies = New Clssys_Companies(Page)
            clsBranch = New Clssys_Branches(Page)
            ClsAttendancePreparationDetails = New ClsAtt_AttendancePreparationDetails(Page)

            ClsFisicalPeriods.Find("ID = " & ddlPeriod.SelectedValue)
            clsCompanies.Find("ID=" & ClsFisicalPeriods.MainCompanyID)
            ClsEmployees.Find("Code = '" & txtEmployeeCode.Text & "'")
            clsBranch.Find("ID=" & ClsEmployees.BranchID)

            Get_FromToDate()


            IntFisicalPeriod = ClsFisicalPeriods.ID
            intNoDecimalPlaces = Get_NoDecimalPlaces()
            intEmployeeContarctID = CheckEmployee(ClsEmployees.ID, IntFisicalPeriod)

            If intEmployeeContarctID = 0 Then Exit Function

            ClsEmployeesContracts.Find("ID = " & intEmployeeContarctID)
            ClsEmployeeClass.Find("ID =" & ClsEmployeesContracts.EmployeeClassID)

            IntNoOfDays = Get_NoOfDays()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function New_Value(ByVal blnDefaultWorkingUnits As Boolean) As Boolean
        Try
            hdnPrepared.Value = 0
            btnSave.Enabled = True
            btnRefund.Enabled = False
            ChkPaid.Visible = False
            uwgEmployeeTransaction.DisplayLayout.Bands(0).Columns.FromKey("Value").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes
            uwgEmployeeTransaction.DisplayLayout.Bands(0).Columns.FromKey("Description").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes
            uwgPayabilities.DisplayLayout.Bands(0).Columns.FromKey("Value").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes
            uwgPayabilities.DisplayLayout.Bands(0).Columns.FromKey("Description").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes


            If blnDefaultWorkingUnits Then
                If ObjPrepaerdData(CPreparedData_calctype) = 1 Then
                    IntNoOfDays = ObjPrepaerdData(CPreparedData_AvaliableDays)
                ElseIf ObjPrepaerdData(CPreparedData_calctype) > 1 Then
                    IntNoOfDays = ObjPrepaerdData(CPreparedData_calctype)
                End If
            End If
            If IntNoOfDays = 29 And FiscToDate.Month = 3 Then
                IntNoOfDays += 1
            End If
            txtNoOfWorkingUnits.Text = IntNoOfDays

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function Get_Attendance(ByVal EmpID As Integer, Optional ByVal IsNew As Boolean = True) As Boolean
        Try
            Dim intAbsent As Integer = 0
            Dim Total_Lat As Double = 0
            Dim Total_OT As Double = 0
            Dim Total_HD As Double = 0
            Dim Total_Absent As Double = 0
            ObjTotalLate = 0
            NotPermitLat = 0

            If IntNoOfDays = 29 And FiscToDate.Month = 3 Then
                IntNoOfDays += 1
            End If
            ClsAttendancePreparationDetails = New ClsAtt_AttendancePreparationDetails(Page)
            ClsAttendancePreparationDetails.Find("EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)")
            Dim DT As Data.DataTable = ClsAttendancePreparationDetails.DataSet.Tables(0)
            Dim cnt As Integer = 0
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

                    'Edited by Hassan Kurdi
                    'Date 2021-02-18
                    'Add if condition to calculate late hours based on PunishementCalc from employee classes

                    Dim ClsContract As New Clshrs_Contracts(Page)
                    Dim ClsClasses As New Clshrs_EmployeeClasses(Page)

                    ClsContract.Find("EmployeeID = " & ClsEmployees.ID)
                    ClsClasses.Find("ID = " & ClsContract.EmployeeClassID)

                    If (ClsClasses.PunishementCalc = 1) Then
                        Total_Lat = Total_Lat + ClsAttendancePreparationDetails.NotpermitLate
                    ElseIf (ClsAttendancePreparationDetails.NotpermitLate > 0) Then
                        Total_Lat = Total_Lat + ClsAttendancePreparationDetails.TotalLate
                    End If

                    'End of edit

                    cnt = cnt + 1
                Catch ex As Exception
                End Try
            Next

            Dim strGetFromProjects = "Set Dateformat DMY; select isnull(SUM(NotpermitLate),0) AS Lat,isnull(SUM(Overtime),0) AS OT,isnull(sum(HolidayHours),0) AS HOT,isnull(Max(OTSalary),0) AS OTSal,isnull(MAX(HOTSalary),0) AS HOTSal,isnull(MAX(SalaryPerHour),0) AS SPerHr,isnull(MAX(SalaryPerDay),0) AS SPerDys from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
            Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsAttendancePreparationDetails.ConnectionString, Data.CommandType.Text, strGetFromProjects)

            lblTotalLate.Text = Math.Round(Total_Lat, 2)
            txtOvertimeWorkHours.Text = Math.Round(DS.Tables(0).Rows(0)("OT"), intNoDecimalPlaces)
            txtHolidayWorkHours.Text = Math.Round(DS.Tables(0).Rows(0)("HOT"), intNoDecimalPlaces)

            lblSalaryPerDay.Text = Math.Round(DS.Tables(0).Rows(0)("SPerDys"), intNoDecimalPlaces)
            lblSalaryPerHour.Text = Math.Round(DS.Tables(0).Rows(0)("SPerHr"), intNoDecimalPlaces)
            lblOverTimePerHour.Text = Math.Round(DS.Tables(0).Rows(0)("OTSal"), intNoDecimalPlaces)
            lblHolidayPerHour.Text = Math.Round(DS.Tables(0).Rows(0)("HOTSal"), intNoDecimalPlaces)

            If IsNew Then
                NotPermitLat = Math.Round(NotPermitLat, intNoDecimalPlaces)
                dbTotalAbsent = Math.Round(Total_Absent, intNoDecimalPlaces)
                Session("NotPermitLat_Value") = NotPermitLat
                Session("Absent_Value") = Total_Absent
                dbOvertimeSalary = Math.Round(DS.Tables(0).Rows(0)("OTSal"), intNoDecimalPlaces)
                dbHolidayHoursSalary = Math.Round(DS.Tables(0).Rows(0)("HOTSal"), intNoDecimalPlaces)
                If cnt = intAbsent Then
                    IntNoOfWorkDays = 0
                Else
                    IntNoOfWorkDays = Convert.ToInt32(txtNoOfWorkingUnits.Text) - IIf(intAbsent > Convert.ToInt32(txtNoOfWorkingUnits.Text), Convert.ToInt32(txtNoOfWorkingUnits.Text), intAbsent)
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
                    IntNoOfWorkDays = FiscToDate.Subtract(ClsEmployees.JoinDate).Days + 1
                End If
                IntNoOfWorkDays = IntNoOfWorkDays - IIf(VacDays < 0, 0, VacDays)
                txtNoOfWorkingUnits.Text = IntNoOfWorkDays
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function FillPreparedORNewData(Optional ByVal blnDefaultWorkingUnits As Boolean = True, Optional ByVal LoadProjectData As Boolean = True, Optional ByVal paidOption As PaidStatus = PaidStatus.All) As Boolean
        Try
            Clear()
            If Not Load_ClsLayers() Then Return False
            If Not SetScreenSetting(IIf(Request.QueryString.Item("ID") <> "", CInt(Request.QueryString.Item("ID")), 0)) Then Return False

            If ObjPrepaerdData(CPreparedData_Prepared) Then
                GetValue(paidOption)
                Get_Attendance(ClsEmployees.ID, False)
            Else
                New_Value(blnDefaultWorkingUnits)

                If Not Get_Attendance(ClsEmployees.ID) Then Return False
                If IntNoOfWorkDays = 0 Then Return False

                ClsEmployees.CollectEmployeesTransactions("T", ToDate, ClsEmployees.ID, ClsFisicalPeriods.ID, DtBenefits, DtDeductions, dblBenefits, dblDeduct, _
                        IntNoOfWorkDays, IntNoOfDays, IntNoOfWorkDays, dbOvertimeSalary, dbHolidayHoursSalary, ObjSalaryPerHour, ObjSalaryPerDay, NotPermitLat, dbTotalAbsent, ObjPrepaerdData(CPreparedData_FirstPrepare), Clshrs_EmployeesBase.ePrepareStage.Normal, IntNoOfDays)
                ClsEmployees.CollectEmployeesPayablities(ClsEmployees.ID, ClsFisicalPeriods.ID, DtBenefits, DtDeductions, dblBenefits, dblDeduct, "N")
                If ClsEmployees.MaxLoanDedution > 0 Then
                    DtDeductions = maxloandeduction(DtDeductions, ClsEmployees.MaxLoanDedution)
                End If

                Dim Extrastrcommand As String = "select (isnull((select top 1 ID from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode),0)) as RelTransactionID,(isnull((select top 1 Sign from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode),0)) as Sign,Amount from hrs_EmployeeExtraItems"
                Dim strFilter As String = " where EmployeeCode = '" & ClsEmployees.Code & "' and Status = 1 and FiscalPeriodID =" & ClsFisicalPeriods.ID
                Dim dsEmployee As New Data.DataSet
                dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, Data.CommandType.Text, Extrastrcommand & strFilter)
                For EXB As Integer = 0 To dsEmployee.Tables(0).Rows.Count - 1
                    If dsEmployee.Tables(0).Rows(EXB)(1) > 0 Then
                        DtBenefits.Rows.Add(New Object() {dsEmployee.Tables(0).Rows(EXB)(0), dsEmployee.Tables(0).Rows(EXB)(2), "Paid", Nothing, "By Project"})
                    ElseIf dsEmployee.Tables(0).Rows(EXB)(1) < 0 Then
                        DtDeductions.Rows.Add(New Object() {dsEmployee.Tables(0).Rows(EXB)(0), dsEmployee.Tables(0).Rows(EXB)(2), "Paid", Nothing, "By Project"})
                    End If
                Next

                If ClsEmployeeClass.NonPermiLatTransaction > 0 Then
                    Dim strcommand1 As String = "select isnull(sum(A.LatPunishment),0) from Att_AttendancePreparationDetails A inner join Att_AttendancePreparationProjects B on A.ID = B.TrnsID where A.EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,A.GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,A.GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)"
                    Dim latvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand1)
                    If latvalue > 0 Then
                        DtDeductions.Rows.Add(New Object() {ClsEmployeeClass.NonPermiLatTransaction, latvalue, "Paid", Nothing, "Paid"})
                    End If
                End If

                If ClsEmployeeClass.OvertimeTransaction > 0 Then
                    Dim strcommand1 As String = "select isnull(sum(Overtime * OTSalary),0) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    Dim OTvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand1)
                    If OTvalue > 0 Then
                        DtBenefits.Rows.Add(New Object() {ClsEmployeeClass.OvertimeTransaction, OTvalue, "Paid", Nothing, "Paid"})
                    End If
                End If

                If ClsEmployeeClass.HOvertimeTransaction > 0 Then
                    Dim strcommand1 As String = "select isnull(sum(HolidayHours * HOTSalary),0) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    Dim HOTvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand1)
                    If HOTvalue > 0 Then
                        DtBenefits.Rows.Add(New Object() {ClsEmployeeClass.HOvertimeTransaction, HOTvalue, "Paid", Nothing, ""})
                    End If
                End If
                Dim AbsDys As Decimal = 0
                If ClsEmployeeClass.RegComputerID > 0 Then
                    Dim strcommand1 As String = "select isnull(sum(SalaryPerDay),0) from Att_AttendancePreparationProjects where isnull(IsAbsent,0) = 1 and isnull(IsVacation,0) = 0 and TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    Dim Absvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand1)
                    strcommand1 = "select isnull(count(ID),0) from Att_AttendancePreparationProjects where isnull(IsAbsent,0) = 1 and isnull(IsVacation,0) = 0 and TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    AbsDys = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand1)
                    If ObjAbsent > 0 Then
                        Absvalue = ObjAbsent * AbsDys
                    End If
                    If Absvalue > 0 Then
                        DtDeductions.Rows.Add(New Object() {ClsEmployeeClass.RegComputerID, Absvalue, "Paid", Nothing, "By Project"})
                    End If
                End If

                Dim clsProject As New Clshrs_Projects(Page, "hrs_Projects")
                If clsProject.Find("LateTransaction > 0") Then
                    Dim strcommand As String = "select sum(LatPunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    Dim PLatvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                    If PLatvalue > 0 Then
                        DtDeductions.Rows.Add(New Object() {clsProject.LateTransaction, PLatvalue, "Paid", Nothing, "By Project"})
                    End If
                End If
                If clsProject.Find("AbsentTransaction > 0") Then
                    Dim strcommand As String = "select sum(AbsentPunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    Dim PAbsvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                    If PAbsvalue > 0 Then
                        DtDeductions.Rows.Add(New Object() {clsProject.AbsentTransaction, PAbsvalue, "Paid", Nothing, "By Project"})
                    End If
                End If
                If clsProject.Find("SickTransaction > 0") Then
                    Dim strcommand As String = "select sum(SickPunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    Dim Psikvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                    If Psikvalue > 0 Then
                        DtDeductions.Rows.Add(New Object() {clsProject.SickTransaction, Psikvalue, "Paid", Nothing, "By Project"})
                    End If
                End If
                If clsProject.Find("LeaveTransaction > 0") Then
                    Dim strcommand As String = "select sum(LeavePunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    Dim Pleavvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                    If Pleavvalue > 0 Then
                        DtDeductions.Rows.Add(New Object() {clsProject.LeaveTransaction, Pleavvalue, "Paid", Nothing, "By Project"})
                    End If
                End If
                If clsProject.Find("OTTransaction > 0") Then
                    Dim strcommand As String = "select sum(OTFactor * Overtime * SalaryPerHour) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    Dim Potvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                    If Potvalue > 0 Then
                        DtBenefits.Rows.Add(New Object() {clsProject.OTTransaction, Potvalue, "Paid", Nothing, "By Project"})
                    End If
                End If
                If clsProject.Find("HOTTransaction > 0") Then
                    Dim strcommand As String = "select sum(HOTFactor * HolidayHours * SalaryPerHour) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    Dim Photvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                    If Photvalue > 0 Then
                        DtBenefits.Rows.Add(New Object() {clsProject.HOTTransaction, Photvalue, "Paid", Nothing, "By Project"})
                    End If
                End If


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

                uwgEmployeeTransaction.DataSource = DtBenefits
                uwgEmployeeTransaction.DataBind()

                uwgPayabilities.DataSource = DtDeductions
                uwgPayabilities.DataBind()
            End If

            'Benefits and deduction summation
            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeeTransaction.Rows
                If IsTransPaid(row.Cells.FromKey("TransactionTypeID").Value) = "1" Then
                    totalBenefits += IIf(IsNothing(row.Cells.FromKey("Value").Value), 0, row.Cells.FromKey("Value").Value)
                End If
            Next
            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgPayabilities.Rows
                If IsTransPaid(row.Cells.FromKey("TransactionTypeID").Value) = "1" Then
                    totalDeducation += IIf(IsNothing(row.Cells.FromKey("Value").Value), 0, row.Cells.FromKey("Value").Value)
                End If
            Next
            If totalBenefits.ToString.Split(".").Length = 2 Then
                If totalBenefits.ToString.Split(".")(1) = 5 Then
                    totalBenefits = CDbl(totalBenefits.ToString.Split(".")(0) & ".6")
                End If
            End If
            If totalDeducation.ToString.Split(".").Length = 2 Then
                If totalDeducation.ToString.Split(".")(1) = 5 Then
                    totalDeducation = CDbl(totalDeducation.ToString.Split(".")(0) & ".6")
                End If
            End If

            lblTotalBenefits.Text = Math.Round(totalBenefits, intNoDecimalPlaces)
            lblTotalDeductions.Text = Math.Round(totalDeducation, intNoDecimalPlaces)
            lblNetSalary.Text = Math.Round(totalBenefits - totalDeducation, intNoDecimalPlaces)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function maxloandeduction(dtDeduct As DataTable, MaxLoanDedution As Double) As DataTable

        Dim drows() As DataRow = dtDeduct.Select("EmpSchID >0")
        If drows.Length > 0 Then
            Dim dt As DataTable = dtDeduct.Select("EmpSchID >0").CopyToDataTable
            Dim tempdt As DataTable = dtDeduct.Select("isnull(EmpSchID,0)=0").CopyToDataTable
            dtDeduct.Clear()
            dtDeduct = tempdt.Select().CopyToDataTable



            Dim totalLoan = dt.Compute("Sum(amount)", "")
            If totalLoan > MaxLoanDedution Then
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
            End If
        End If
        Return dtDeduct

    End Function
    Private Function CreateDataTable(ByVal DtTable As Data.DataTable, ByVal PtrTableName As String) As Boolean

        Dim ObjDataColumn As New Data.DataColumn
        Try
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

            'B#003 [0256]
            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "EmpSchID"
            ObjDataColumn.DataType = System.Type.GetType("System.Int32")
            DtTable.Columns.Add(ObjDataColumn)

        Catch ex As Exception

        End Try
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

    Enum AttendanceType
        Late
        Overtime
        Holiday
    End Enum
    Private Function GetTotalHours(AttType As AttendanceType) As Double
        Try
            Dim str As String = ""

            Select Case AttType
                Case AttendanceType.Late
                    str = "TotalLate"
                Case AttendanceType.Overtime
                    str = "Overtime"
                Case AttendanceType.Holiday
                    str = "HolidayHours"
            End Select

            Dim TotalHour As Integer = 0
            Dim TotalMint As Integer = 0
            Dim Hour As Integer = 0
            Dim Mint As Integer = 0

            Dim DS As New DataSet
            DS = GetAttendanceDataSet()

            For Each row As Data.DataRow In DS.Tables(0).Rows

                Hour = row.Item(str).ToString.Split(".")(0)
                Mint = row.Item(str).ToString.Split(".")(1).Substring(0, 1)

                TotalHour += Hour
                TotalMint += Mint
                If TotalMint >= 60 Then
                    TotalMint -= 60
                    TotalHour += 1
                End If
            Next

            Return CDbl(TotalHour & "." & TotalMint)

        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function GetAttendanceDataSet() As DataSet
        Try

            ClsEmployees = New Clshrs_Employees(Page)
            ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'")
            ClsFisicalPeriods = New Clssys_FiscalYearsPeriods(Page)
            ClsFisicalPeriods.Find("ID=" & ddlPeriod.SelectedValue)
            Dim DS1 As New DataSet
            Dim connetionString As String
            Dim connection As Data.SqlClient.SqlConnection
            Dim command As Data.SqlClient.SqlCommand
            Dim adapter As New Data.SqlClient.SqlDataAdapter
            connetionString = ClsEmployees.ConnectionString
            connection = New Data.SqlClient.SqlConnection(connetionString)
            Dim strwhr As String = " and EmployeeID = " & ClsEmployees.ID

            Dim str2 As String = "select ParentTable.ID,EmployeeID,GAttendDate as GAttendDate,HAttendDate,TotalLate as TotalLate," & _
                     "PermitLate as PermitLate," & _
                     "NotpermitLate as NotpermitLate," & _
                     "(select ISNULL(SUM(Overtime),0) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = ParentTable.EmployeeID and CONVERT(Datetime,GAttendDate,103)= CONVERT(Datetime,ParentTable.GAttendDate,103))) as Overtime," & _
                     "(select ISNULL(SUM(HolidayHours),0) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = ParentTable.EmployeeID and CONVERT(Datetime,GAttendDate,103)= CONVERT(Datetime,ParentTable.GAttendDate,103))) as HolidayHours," & _
                     "IsVacation,IsAbsent,LeavingType,Notes,ParentTable.RegComputerID " & _
                     "from Att_AttendancePreparationDetails AS ParentTable inner join hrs_Employees on hrs_Employees.ID = ParentTable.EmployeeID  where CONVERT(Datetime,ParentTable.GAttendDate,103)>= CONVERT(Datetime,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(Datetime,ParentTable.GAttendDate,103) <= CONVERT(Datetime,'" & ToDate.ToString("dd/MM/yyyy") & "',103) and ParentTable.CancelDate is null " & strwhr & " order by ParentTable.GAttendDate ASC"

            Dim str3 As String = "select A.ID,A.TrnsID,A.ProjectID,A.CheckIn,A.Checkout,A.TotalTime,A.Overtime,A.HolidayHours from Att_AttendancePreparationProjects A left outer join Att_AttendancePreparationDetails ParentTable on A.TrnsID=ParentTable.ID" & _
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

            Return DS1

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub Clear()
        lblTotalLate.Text = ""
        txtNoOfWorkingUnits.Text = ""
        txtOvertimeWorkHours.Text = ""
        txtHolidayWorkHours.Text = ""
        lblCurrentPeriod.Text = ""
        lblSalaryPerDay.Text = ""
        lblBasicSalary.Text = ""
        lblSalaryPerHour.Text = ""
        lblOverTimePerHour.Text = ""
        lblTotalBenefits.Text = ""
        lblTotalDeductions.Text = ""
        lblNetSalary.Text = ""
        hdnContractID.Value = "0"
        txtEmployeeID.Value = "0"
        ObjAbsent = 0
        uwgEmployeeTransaction.Rows.Clear()
        uwgPayabilities.Rows.Clear()

    End Sub

    Private Function CheckEmployee(ByVal IntInComingEmployeeID As Integer, ByVal FiscalPeriodID As Integer) As Integer
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsContract As New Clshrs_Contracts(Page)
        Dim clsEmployeevacations As New Clshrs_EmployeesVacations(Page)
        ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim ClsFiscalPeriods As New Clssys_FiscalYearsPeriods(Page)
        Dim intValidContract As Integer = 0

        Try
            ClsFiscalPeriods.Find("ID=" & FiscalPeriodID)
            lbError.Text = ""
            lbError.Visible = False
            ClsEmployees.Find("CancelDate is Null And ID = " & IntInComingEmployeeID)
            txtEmployeeID.Value = ClsEmployees.ID
            intValidContract = ClsContract.ContractValidatoinId(ClsEmployees.ID, FiscalPeriodID)
            If intValidContract <= 0 Then
                Clear()
                lbError.Text = ObjNavigationHandler.SetLanguage(Page, "Employee have no active contract/هذا الموظف غير موجود او موجود فى العقود الغير فعالة ")
                lbError.Visible = True
                txtEmployeeID.Value = 0
                txtEmployeeCode.Focus()
                btnSave.Enabled = False
                btnRefund.Enabled = False
                Return 0
                Exit Function
            End If

            If (clsEmployeevacations.FindEmployeeVacations(" hrs_EmployeesVacations.VacationTypeID in (select ID from hrs_VacationsTypes where CancelDate is null and IsAnnual = 1) and hrs_EmployeesVacations.EmployeeID=" & ClsEmployees.ID & " And Convert(smalldatetime,Convert(varchar,ActualStartDate ,103)) <= Convert(smalldatetime,Convert(varchar,'" & FiscFromDate & "' ,103))	And	(ActualEndDate Is Null Or  Convert(smalldatetime,Convert(varchar,ActualEndDate ,103)) > Convert(smalldatetime,Convert(varchar,'" & FiscToDate & "',103)))")) Then
                Clear()
                lbError.Text = ObjNavigationHandler.SetLanguage(Page, "Employee already in vacation/هذا الموظف فى اجازة")
                lbError.Visible = True
                txtEmployeeID.Value = 0
                txtEmployeeCode.Focus()
                btnSave.Enabled = False
                btnRefund.Enabled = False
                Return 0
            End If
            btnSave.Enabled = True
            Return intValidContract
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function SwitchMsg(ByVal Active As Boolean, ByVal Days As Integer) As Boolean
        Dim ClsBaseClass As New ClsDataAcessLayer(Page)
        ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsBaseClass.ConnectionString)
        Try
            lbError.Text = ObjNavigationHandler.SetLanguage(Page, " Penalty Days :/أيام مخصمة:") & Days
            lbError.Visible = Active
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetAllPreparedData(ByVal IntEmployeeID As Integer, ByVal intFiscalYearPeriod As Integer, ByRef dblBenfits As Double, ByRef dblDeduct As Double, ByVal paidOption As PaidStatus) As Boolean
        ClsEmployeesTransactions = New Clshrs_EmployeesTransactions(Page)
        Dim ClsEmployeesTransactionsDet As New Clshrs_EmployeesTransactionsDetails(Page)
        Dim ClsDataHandler As New Venus.Shared.DataHandler
        Dim strPaidOption As String = ""
        Try
            'Get the Transaction Head Id 
            ClsEmployeesTransactions.Find(" EmployeeID = " & IntEmployeeID & " And FiscalYearPeriodID = " & intFiscalYearPeriod & " And PrepareType ='N'")
            'Get the Transaction Details with plus sign 
            If paidOption = PaidStatus.PaidOnly Then
                strPaidOption = "(Select IsPaid From hrs_TransactionsTypes Where ID=hrs_EmployeesTransactionsDetails.TransactionTypeID)=1"
            ElseIf paidOption = PaidStatus.All Then
                strPaidOption = ""
            ElseIf paidOption = PaidStatus.NotPaid Then
                strPaidOption = "(Select IsPaid From hrs_TransactionsTypes Where ID=hrs_EmployeesTransactionsDetails.TransactionTypeID)=0"
            End If
            ClsEmployeesTransactionsDet.FindRelatedToProjects("EmployeeTransactionID = " & ClsEmployeesTransactions.ID & " And (Select sign From hrs_TransactionsTypes Where ID=hrs_EmployeesTransactionsDetails.TransactionTypeID)=1 " & IIf(strPaidOption.Trim = String.Empty, "", " And " & strPaidOption))
            If ClsDataHandler.CheckValidDataObject(ClsEmployeesTransactionsDet.DataSet) Then
                ClsEmployeesTransactionsDet.DataSet.Tables(0).Columns("NumericValue").ColumnName = "Amount"
                ClsEmployeesTransactionsDet.DataSet.Tables(0).Columns("TextValue").ColumnName = "Description"
                ClsEmployeesTransactionsDet.DataSet.Tables(0).Columns("EmployeePayabilityScheduleID").ColumnName = "ID"
                ClsEmployeesTransactionsDet.DataSet.Tables(0).Columns("OrgAmount").ColumnName = "OrgAmount"
                If ClsEmployeesTransactionsDet.DataSet.Tables(0).Rows.Count > 0 Then
                    For Each drDataRow As Data.DataRow In ClsEmployeesTransactionsDet.DataSet.Tables(0).Rows
                        dblBenfits += drDataRow.Item("Amount")
                        Dim ClsTransactionsTypes As New Clshrs_TransactionsTypes(Page)
                        ClsTransactionsTypes.Find("ID=" & drDataRow.Item(0))
                        If ClsTransactionsTypes.IsPaid Then
                            drDataRow("DescriptionSign") = "Paid"
                        Else
                            drDataRow("DescriptionSign") = "Not Paid"
                        End If
                    Next
                End If
            End If
            uwgEmployeeTransaction.DataSource = ClsEmployeesTransactionsDet.DataSet
            uwgEmployeeTransaction.DataBind()

            'Get the Transaction Details with minus sign 
            ClsEmployeesTransactionsDet.FindRelatedToProjects("EmployeeTransactionID = " & ClsEmployeesTransactions.ID & " And (Select sign From hrs_TransactionsTypes Where ID=hrs_EmployeesTransactionsDetails.TransactionTypeID)=-1 " & IIf(strPaidOption.Trim = String.Empty, "", " And " & strPaidOption))
            If ClsDataHandler.CheckValidDataObject(ClsEmployeesTransactionsDet.DataSet) Then
                ClsEmployeesTransactionsDet.DataSet.Tables(0).Columns("NumericValue").ColumnName = "Amount"
                ClsEmployeesTransactionsDet.DataSet.Tables(0).Columns("TextValue").ColumnName = "Description"
                ClsEmployeesTransactionsDet.DataSet.Tables(0).Columns("EmployeePayabilityScheduleID").ColumnName = "EmpSchId"
                ClsEmployeesTransactionsDet.DataSet.Tables(0).Columns("OrgAmount").ColumnName = "OrgAmount"

                If ClsEmployeesTransactionsDet.DataSet.Tables(0).Rows.Count > 0 Then
                    For Each drDataRow As Data.DataRow In ClsEmployeesTransactionsDet.DataSet.Tables(0).Rows
                        dblDeduct += drDataRow.Item("Amount")
                        Dim ClsTransactionsTypes As New Clshrs_TransactionsTypes(Page)
                        ClsTransactionsTypes.Find("ID=" & drDataRow.Item(0))
                        If ClsTransactionsTypes.IsPaid Then
                            drDataRow("DescriptionSign") = "Paid"
                        Else
                            drDataRow("DescriptionSign") = "Not Paid"
                        End If
                    Next
                End If
            End If
            uwgPayabilities.DataSource = ClsEmployeesTransactionsDet.DataSet
            uwgPayabilities.DataBind()

        Catch ex As Exception

        End Try
    End Function

#End Region

End Class
