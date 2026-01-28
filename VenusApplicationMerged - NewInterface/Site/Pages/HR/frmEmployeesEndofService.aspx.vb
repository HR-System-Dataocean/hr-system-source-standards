Imports System.Data
Imports System.Data.SqlClient
Imports System.Runtime.CompilerServices.RuntimeHelpers
Imports C1.C1Rdl
Imports C1.Win.Localization.Design
Imports Infragistics.WebUI.UltraWebGrid
Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Application.SystemFiles.System

Partial Class frmEmployeesEndofService
    Inherits MainPage

#Region "Public Decleration"

    Private ClsEndOfService As Clshrs_EndOfServices
    Private Clshrs_EmployeesJoins As Clshrs_EmployeesJoins
    Private ClsEmployeesTrancations As Clshrs_EmployeesTransactions
    Dim ClsFisicalPeriod As Clssys_FiscalYearsPeriods
    Private blCheckTwice As Boolean = False

    Const CIntInitValue = 0
    Const csSearchID = 90
    Const CsIntYearDays = 360
    Const CsIntMonthDays = 30
    Const CsIntMonths = 12

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        blCheckTwice = False
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsContract As New Clshrs_Contracts(Page)
        Dim ClsTransactionsTypes As New Clshrs_TransactionsTypes(Page)

        Dim clsNavigation = New Venus.Shared.Web.NavigationHandler(ClsTransactionsTypes.ConnectionString)
        ClsEndOfService = New Clshrs_EndOfServices(Page)

        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0

        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
        If ClsObjects.Find(" Code='" & ClsEmployee.Table.Trim & "'") Then
            If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                SearchID = ClsSearchs.ID

                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & lblDescEmployeeCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & lblDescEmployeeCode.ClientID & "'"
                btnEmployee.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
        End If

        If Not IsPostBack Then
            Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)

            ClsTransactionsTypes.GetList(uwgPayabilities.Columns(0).ValueList)
            ClsTransactionsTypes.GetList(uwgTransactions.Columns(0).ValueList)

            ClsTransactionsTypes.GetList(uwgVacationsTransactions.Columns(0).ValueList)
            Dim clsMainCountry As New Clssys_Countries(Page)
            Dim clsMainCurrency As New ClsSys_Currencies(Page)
            clsMainCountry.Find(" IsMainCountries = 1 ")
            If clsMainCountry.ID > 0 Then
                clsMainCurrency.Find(" ID=" & clsMainCountry.CurrencyID)
                If Not IsNothing(clsMainCurrency.NoDecimalPlaces) Then
                    uwgPayabilities.Columns(1).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgPayabilities.Columns(1).Format, clsMainCurrency.NoDecimalPlaces)
                    uwgPayabilities.Columns(2).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgPayabilities.Columns(2).Format, clsMainCurrency.NoDecimalPlaces)
                    uwgTransactions.Columns(1).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgTransactions.Columns(1).Format, clsMainCurrency.NoDecimalPlaces)
                    uwgTransactions.Columns(2).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgTransactions.Columns(2).Format, clsMainCurrency.NoDecimalPlaces)
                    uwgVacationsTransactions.Columns(1).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgVacationsTransactions.Columns(1).Format, clsMainCurrency.NoDecimalPlaces)
                    uwgVacationsTransactions.Columns(2).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgVacationsTransactions.Columns(2).Format, clsMainCurrency.NoDecimalPlaces)
                End If
            End If

            ClsEndOfService.GetDropDownList(DdlEndofService, False)
            If DdlEndofService.Items.Count = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, clsNavigation.SetLanguage(Page, "You must enter at least one End Of Service Type / يجب أن تدخل نوع انهاء خدمة على الأقل "))
                Exit Sub
            End If

            Dim DteEOSDate As Date = Date.Now
            If Request.QueryString.Count > 0 Then
                If Request.QueryString.Item("EmpCode") <> Nothing Then
                    lblDescEmployeeCode.Text = Request.QueryString.Item("EmpCode")
                End If
                If Request.QueryString.Item("EOSDate") <> Nothing Then
                    DteEOSDate = Request.QueryString.Item("EOSDate")
                End If
            End If
            DdlEndofService.SelectedIndex = 0
            wdcEndOfServiceDate.Value = ClsContract.GetHigriDate(DteEOSDate)
            CheckCode()

            uwgExtraBenfits.Rows.Add()
            uwgExtraDeduction.Rows.Add()
        End If



    End Sub
    Protected Sub ImageButton_Save_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_Save.Click
        Try
            If lblDescEmployeeCode.Text.ToString.Trim.Length = 0 Then Exit Sub
            Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(Me.Page)
            Dim ClsSolver As New Clshrs_FormulaSolver(ClsTransactionTypes.ConnectionString, Me.Page)
            Dim ClsEmpContracts As New Clshrs_Contracts(Page)
            Dim ClsEmployeesTrancations As New Clshrs_EmployeesTransactions(Page)
            Dim ClsEmployeesTrancationsProjects As New Clshrs_EmployeesTransactionsProjects(Page)
            Dim ClsEmpTransDet As New Clshrs_EmployeesTransactionsDetails(Page)
            Dim ClsFisicalPeriod As New Clssys_FiscalYearsPeriods(Page)
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ClsEmployeesItems As New Clshrs_EmployeesItems(Page)
            Dim ClsObjNav As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Dim ObjTransaction As Infragistics.WebUI.UltraWebGrid.UltraGridRow
            Dim ObjLoans As Infragistics.WebUI.UltraWebGrid.UltraGridRow
            Dim ObjVacation As Infragistics.WebUI.UltraWebGrid.UltraGridRow
            Dim IntContractId As Integer = CIntInitValue
            Dim IntEmployeeId As Integer = CheckEmployee(True)
            If IntEmployeeId <= 0 Then Return

            Dim IntPeriodId As Integer = CIntInitValue
            Dim DtePeriodFrom As Date = Nothing
            Dim DtePeriodTo As Date = Nothing
            Dim dtNowDate As Date
            Dim DteEndOfServiceDate As Date
            Dim ContractStartDate As Date = Nothing
            Dim IntTransactionHeadID As Integer = 0

            Dim IntProjectHeadID As Integer = 0
            Dim DefaultProject As Integer = 0
            DteEndOfServiceDate = ClsEmployee.SetHigriDate(wdcEndOfServiceDate.Value)

            Clshrs_EmployeesJoins = New Clshrs_EmployeesJoins(Me.Page)
            ClsEndOfService = New Clshrs_EndOfServices(Page)
            clsBranch = New Clssys_Branches(Page)

            IntContractId = ClsEmpContracts.ContractValidatoinId(IntEmployeeId, DteEndOfServiceDate)
            ClsEmpContracts.Find("ID = " & IntContractId)
            ClsEmployee.Find("ID = " & IntEmployeeId)
            ClsTransactionTypes.Find(" IsEndOfService = 1 ")
            Dim clsEmployeeClass As New Clshrs_EmployeeClasses(Page)
            clsEmployeeClass.Find("ID =" & ClsEmpContracts.EmployeeClassID)
            DefaultProject = clsEmployeeClass.DefaultProjectID
            clsBranch.Find("ID=" & ClsEmployee.BranchID & "")
            '==========================================================================
            '1- check if the Contract is valid 
            '2- Get the Contract StartDate 
            '==========================================================================
            If IntContractId = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsObjNav.SetLanguage(Page, " Current Employee Has No Valid Contract !/ هذا الموظف ليس له عقد"))
                Exit Sub
            End If
            ContractStartDate = ClsEmpContracts.StartDate

            'Check Employee Items
            If ClsEmployeesItems.Find("EmployeeID = '" & ClsEmployee.ID & "' and canceldate is null and ReturnedDate is null and isnull(IsConfirmed,0) = 1") Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsObjNav.SetLanguage(Page, "There is an employee items must be recieved / توجد عهد تخص الموظف لابد من تسليمها "))
                Dim cs As ClientScriptManager = Page.ClientScript
                cs.RegisterStartupScript(Me.GetType(), "PopupScript", "OpenModal12('frmEmployeesItemsClearance.aspx?EmpCode=" & ClsEmployee.Code & "',400,700);", True)
                Return
            End If

            '==========================================================================
            '4- Update Employees Joins by the endofservce Date and reson
            '5- Update the contract by the End date of the Endof service date 
            '==========================================================================
            Clshrs_EmployeesJoins.Find("EndofServiceDate is null and EmployeeID  = " & IntEmployeeId)
            With Clshrs_EmployeesJoins
                .EmployeeId = IntEmployeeId
                .JoinDate = ContractStartDate
                .EndofServiceDate = DteEndOfServiceDate
                .EndofServiceReson = DdlEndofService.SelectedItem.Text
                .EndofServiceType = DdlEndofService.SelectedItem.Value
                .EndOfServiceDateText = wdcEndOfServiceDate.Text
                .EOSDays = IIf(lblDescPaidDays.Text <> "", lblDescPaidDays.Text, 0)
                .EOSMonths = IIf(lblDescPaidMonth.Text <> "", lblDescPaidMonth.Text, 0)
                .EOSYears = IIf(lblDescPaidYear.Text <> "", lblDescPaidYear.Text, 0)


                .EosTotalDays = IIf(lblservicedays.Text <> "", lblservicedays.Text, 0)
                .EosTotalMonths = IIf(lblservicemonths.Text <> "", lblservicemonths.Text, 0)
                .EosTotalYears = IIf(lblserviceYears.Text <> "", lblserviceYears.Text, 0)


                .EosOverDueDays = IIf(lblNotPaidDays.Text <> "", lblNotPaidDays.Text, 0)
                .EosOverDueMonths = IIf(lblNotPaidMonth.Text <> "", lblNotPaidMonth.Text, 0)
                .EosOverDueYears = IIf(lblDescNotPaidYear.Text <> "", lblDescNotPaidYear.Text, 0)



                If Clshrs_EmployeesJoins.ID > 0 Then
                    .Update("EndofServiceDate is null and EmployeeID  = " & IntEmployeeId)
                Else
                    .Save()
                End If
            End With
            ClsEmpContracts.EndContract(" ID = " & IntContractId, DteEndOfServiceDate)

            ClsEmployee.Update("ID = " & IntEmployeeId)
            Dim cmdstring As String = "set dateformat dmy; update hrs_employees set ExcludeDate = '" & DteEndOfServiceDate & "' where ID = " & IntEmployeeId
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, CommandType.Text, cmdstring)

            '==========================================================================
            '6- Get the FiscalPeriod Details From FiscalPeriod Class 
            '7- Save the Head of the Employee Transaction 
            '==========================================================================

            Get_FromToDate(DteEndOfServiceDate)
            Load_ClsLayers(DteEndOfServiceDate)

            dtNowDate = wdcEndOfServiceDate.Value
            If clsBranch.AffectPeriod Then
                ClsFisicalPeriod.GetFisicalperiodInfoByPrepareDay(ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value), IntFisicalPeriod, FromDate, ToDate)
            Else
                ClsFisicalPeriod.GetFisicalperiodInfo(ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value), IntFisicalPeriod, FromDate, ToDate)


            End If
            'Rabie 3-12-2025
            ' ClsFisicalPeriod.GetFisicalperiodInfo(dtNowDate, IntPeriodId, DtePeriodFrom, DtePeriodTo)
            '------------------------------=============-----------------------------------------
            With ClsEmployeesTrancations
                .EmployeeID = IntEmployeeId
                .FiscalYearPeriodID = IntPeriodId
                .PaidDate = DateAdd(DateInterval.Hour, 23, DteEndOfServiceDate)
                .FinancialWorkingUnits = lblDescworkingDays.Text
                .PrepareType = "E"
                .CBranchID = ClsEmployee.BranchID
                IntTransactionHeadID = .Save()

            End With

            ClsEmployeesTrancationsProjects = New Clshrs_EmployeesTransactionsProjects(Page)
            With ClsEmployeesTrancationsProjects
                .EmployeeTransactionID = IntTransactionHeadID
                .ProjectID = DefaultProject
                .WorkingDate = DateTime.Now
                .WorkingUnits = lblDescworkingDays.Text
                IntProjectHeadID = .Save1()
            End With
            ClsEmpTransDet = New Clshrs_EmployeesTransactionsDetails(Page)
            With ClsEmpTransDet
                .EmpTransProjID = IntProjectHeadID
                .NumericValue = CDbl(lblDescAmount.Text)  ' * IIf(Val(lblDescPaidYear.Text) > 0, Val(lblDescPaidYear.Text), 1)
                .TextValue = "End Of Service : " & DdlEndofService.SelectedItem.Text
                .TransactionTypeID = ClsTransactionTypes.ID
                .Save()
            End With
            Dim EosTransactionid As Integer = IntTransactionHeadID
            '------------------------------=============-----------------------------------------
            ' Saving Transaction Head Details For Normal Transaction
            '------------------------------=============-----------------------------------------
            ClsEmployeesTrancations = New Clshrs_EmployeesTransactions(Page)
            With ClsEmployeesTrancations
                .EmployeeID = IntEmployeeId
                .FiscalYearPeriodID = IntPeriodId
                .PaidDate = DateAdd(DateInterval.Hour, 23, DteEndOfServiceDate)
                .FinancialWorkingUnits = lblWorkingDays.Text
                .CBranchID = ClsEmployee.BranchID
                .PrepareType = "EN"
                IntTransactionHeadID = .Save()
            End With

            ClsEmployeesTrancationsProjects = New Clshrs_EmployeesTransactionsProjects(Page)
            With ClsEmployeesTrancationsProjects
                .EmployeeTransactionID = IntTransactionHeadID
                .ProjectID = DefaultProject
                .WorkingDate = DateTime.Now
                .WorkingUnits = lblWorkingDays.Text
                IntProjectHeadID = .Save1()
            End With

            For Each ObjTransaction In uwgTransactions.Rows
                ClsEmpTransDet = New Clshrs_EmployeesTransactionsDetails(Page)
                With ClsEmpTransDet
                    .EmpTransProjID = IntProjectHeadID
                    .NumericValue = Math.Abs(ObjTransaction.Cells(1).Value - ObjTransaction.Cells(2).Value)
                    .TextValue = ObjTransaction.Cells(3).Value
                    .TransactionTypeID = ObjTransaction.Cells(0).Value
                    .Save()
                End With
            Next

            'Salary Loans
            For Each ObjLoans In uwgPayabilities.Rows
                If ObjLoans.Cells(2).Value = 0 Then
                    ClsEmpTransDet = New Clshrs_EmployeesTransactionsDetails(Page)
                    With ClsEmpTransDet
                        .EmpTransProjID = IntProjectHeadID
                        .NumericValue = ObjLoans.Cells(1).Value
                        .TextValue = ObjLoans.Cells(3).Value
                        .TransactionTypeID = ObjLoans.Cells(0).Value
                        Dim n As Integer = .Save()
                        Dim strcommand As String = "insert into hrs_EmployeesPayabilitiesSchedulesSettlement (EmployeePayabilityScheduleID,EmployeeTransactionID,Amount,[Date],RegDate,RegUserID)"
                        strcommand = strcommand & " select ID," & IntProjectHeadID & ",DueAmount, " & DteEndOfServiceDate & ",GETDATE()," & ClsEmpTransDet.DataBaseUserRelatedID & " from hrs_EmployeesPayabilitiesSchedules where EmployeePayabilityID in (select ID from hrs_EmployeesPayabilities where EmployeeID = " & IntEmployeeId & " and TransactionTypeID = " & ObjLoans.Cells(0).Value & ")"
                        strcommand = strcommand & " and ID not in(select EmployeePayabilityScheduleID from hrs_EmployeesPayabilitiesSchedulesSettlement where hrs_EmployeesPayabilitiesSchedulesSettlement.Amount = hrs_EmployeesPayabilitiesSchedules.DueAmount)"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmpTransDet.ConnectionString, CommandType.Text, strcommand)
                    End With
                End If
            Next




            '------------------------------=============-----------------------------------------
            ' Saving Transaction Head Details For extra Benfit
            '------------------------------=============-----------------------------------------
            ClsEmployeesTrancations = New Clshrs_EmployeesTransactions(Page)
            With ClsEmployeesTrancations
                .EmployeeID = IntEmployeeId
                .FiscalYearPeriodID = IntPeriodId
                .PaidDate = DateAdd(DateInterval.Hour, 23, DteEndOfServiceDate)
                .FinancialWorkingUnits = 0
                .PrepareType = "ET"
                .RegComputerID = EosTransactionid
                IntTransactionHeadID = .Save()
            End With
            ClsEmployeesTrancationsProjects = New Clshrs_EmployeesTransactionsProjects(Page)
            With ClsEmployeesTrancationsProjects
                .EmployeeTransactionID = IntTransactionHeadID
                .ProjectID = DefaultProject
                .WorkingDate = DateTime.Now
                .WorkingUnits = 0
                IntProjectHeadID = .Save1()
            End With

            ClsTransactionTypes = New Clshrs_TransactionsTypes(Page)
            ClsTransactionTypes.Find("Code='17'")

            For Each ObjRowDet In uwgExtraBenfits.Rows
                If ObjRowDet.Cells.FromKey("Amount").Value > 0 Then

                    ClsEmployeeTransactionsDet = New Clshrs_EmployeesTransactionsDetails(Page)
                    ClsEmployeeTransactionsDet.TransactionTypeID = ClsTransactionTypes.ID
                    ClsEmployeeTransactionsDet.EmpTransProjID = IntProjectHeadID
                    ClsEmployeeTransactionsDet.TextValue = ObjRowDet.Cells().FromKey("description").Value
                    ClsEmployeeTransactionsDet.NumericValue = ObjRowDet.Cells().FromKey("Amount").Value
                    ClsEmployeeTransactionsDet.Save()


                End If
            Next





            'ClsEmpTransDet = New Clshrs_EmployeesTransactionsDetails(Page)

            'With ClsEmpTransDet
            '    .EmpTransProjID = IntProjectHeadID
            '    If txtExtraBenfitsAmount.Text <> "" Then
            '        .NumericValue = Convert.ToDouble(txtExtraBenfitsAmount.Text.Trim())
            '        .TextValue = IIf(txtExtraBenfitsRemarks.Text.Trim() = "", "تسوية مستحقات", txtExtraBenfitsRemarks.Text.Trim())
            '        .TransactionTypeID = ClsTransactionTypes.ID
            '        Dim n As Integer = .Save()
            '    End If


            'End With



            ClsTransactionTypes = New Clshrs_TransactionsTypes(Page)
            ClsTransactionTypes.Find("Code='24'")

            For Each ObjRowDet In uwgExtraDeduction.Rows
                If ObjRowDet.Cells.FromKey("Amount").Value > 0 Then

                    ClsEmployeeTransactionsDet = New Clshrs_EmployeesTransactionsDetails(Page)
                    ClsEmployeeTransactionsDet.TransactionTypeID = ClsTransactionTypes.ID
                    ClsEmployeeTransactionsDet.EmpTransProjID = IntProjectHeadID
                    ClsEmployeeTransactionsDet.TextValue = ObjRowDet.Cells().FromKey("description").Value
                    ClsEmployeeTransactionsDet.NumericValue = ObjRowDet.Cells().FromKey("Amount").Value
                    ClsEmployeeTransactionsDet.Save()


                End If
            Next



            'ClsEmpTransDet = New Clshrs_EmployeesTransactionsDetails(Page)

            'With ClsEmpTransDet
            '    .EmpTransProjID = IntProjectHeadID
            '    If txtExtraDudectionAmount.Text <> "" Then
            '        .NumericValue = Convert.ToDouble(txtExtraDudectionAmount.Text.Trim())
            '        .TextValue = IIf(txtExtraDeductionRemarks.Text.Trim() = "", "تسوية خصومات", txtExtraDeductionRemarks.Text.Trim())
            '        .TransactionTypeID = ClsTransactionTypes.ID
            '        Dim n As Integer = .Save()
            '    End If

            '    ' .NumericValue = IIf(txtExtraDudectionAmount.Text.Trim() = "", 0, Convert.ToDouble(txtExtraDudectionAmount.Text.Trim()))

            'End With














            '------------------------------=============-----------------------------------------
            ' Saving Transaction Head Details For Loans
            '------------------------------=============-----------------------------------------
            ClsEmployeesTrancations = New Clshrs_EmployeesTransactions(Page)
            With ClsEmployeesTrancations
                .EmployeeID = IntEmployeeId
                .FiscalYearPeriodID = IntPeriodId
                .PaidDate = DateAdd(DateInterval.Hour, 23, DteEndOfServiceDate)
                .FinancialWorkingUnits = lblDescworkingDays.Text
                .CBranchID = ClsEmployee.BranchID
                .PrepareType = "EL"
                IntTransactionHeadID = .Save()
            End With
            ClsEmployeesTrancationsProjects = New Clshrs_EmployeesTransactionsProjects(Page)
            With ClsEmployeesTrancationsProjects
                .EmployeeTransactionID = IntTransactionHeadID
                .ProjectID = DefaultProject
                .WorkingDate = DateTime.Now
                .WorkingUnits = lblDescworkingDays.Text
                IntProjectHeadID = .Save1()
            End With
            For Each ObjLoans In uwgPayabilities.Rows
                If ObjLoans.Cells(2).Value > 0 Then
                    ClsEmpTransDet = New Clshrs_EmployeesTransactionsDetails(Page)
                    With ClsEmpTransDet
                        .EmpTransProjID = IntProjectHeadID
                        .NumericValue = ObjLoans.Cells(2).Value
                        .TextValue = "Loan Balance"
                        .TransactionTypeID = ObjLoans.Cells(0).Value
                        Dim n As Integer = .Save()
                        Dim strcommand As String = "set dateformat dmy; insert into hrs_EmployeesPayabilitiesSchedulesSettlement (EmployeePayabilityScheduleID,EmployeeTransactionID,Amount,[Date],RegDate,RegUserID)"
                        strcommand = strcommand & " select ID," & IntProjectHeadID & ",DueAmount, '" & DteEndOfServiceDate & "',GETDATE()," & ClsEmpTransDet.DataBaseUserRelatedID & " from hrs_EmployeesPayabilitiesSchedules where EmployeePayabilityID in (select ID from hrs_EmployeesPayabilities where EmployeeID = " & IntEmployeeId & " and TransactionTypeID = " & ObjLoans.Cells(0).Value & ")"
                        strcommand = strcommand & " and ID not in(select EmployeePayabilityScheduleID from hrs_EmployeesPayabilitiesSchedulesSettlement group by EmployeePayabilityScheduleID having sum(hrs_EmployeesPayabilitiesSchedulesSettlement.Amount) = hrs_EmployeesPayabilitiesSchedules.DueAmount)"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmpTransDet.ConnectionString, CommandType.Text, strcommand)
                    End With
                End If
            Next











            '------------------------------=============-----------------------------------------
            ' Saving Transaction Head Details For Vacation
            '------------------------------=============-----------------------------------------
            ClsEmployeesTrancations = New Clshrs_EmployeesTransactions(Page)
            With ClsEmployeesTrancations
                .EmployeeID = IntEmployeeId
                .FiscalYearPeriodID = IntPeriodId
                .PaidDate = DateAdd(DateInterval.Hour, 23, DteEndOfServiceDate)
                .FinancialWorkingUnits = lblVacationDue.Text
                .CBranchID = ClsEmployee.BranchID
                .PrepareType = "EV"
                IntTransactionHeadID = .Save()
            End With
            ClsEmployeesTrancationsProjects = New Clshrs_EmployeesTransactionsProjects(Page)
            With ClsEmployeesTrancationsProjects
                .EmployeeTransactionID = IntTransactionHeadID
                .ProjectID = DefaultProject
                .WorkingDate = DateTime.Now
                .WorkingUnits = lblVacationDue.Text
                IntProjectHeadID = .Save1()
            End With
            For Each ObjVacation In uwgVacationsTransactions.Rows
                ClsEmpTransDet = New Clshrs_EmployeesTransactionsDetails(Page)
                With ClsEmpTransDet
                    .EmpTransProjID = IntProjectHeadID
                    .NumericValue = Math.Abs(ObjVacation.Cells(1).Value - ObjVacation.Cells(2).Value)
                    .TextValue = ObjVacation.Cells(3).Value
                    .TransactionTypeID = ObjVacation.Cells(0).Value
                    .Save()
                End With
            Next

            If ClsEmployee.IsProjectRelated Then
                Dim hrsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
                If hrsProjectPlacementEmployees.Find("EmployeeID = " & IntEmployeeId & " and (ToDate is null or ToDate >= '" & DteEndOfServiceDate.ToString("dd/MM/yyyy") & "')") Then
                    Dim DTPlacement As DataTable = hrsProjectPlacementEmployees.DataSet.Tables(0)
                    For Each DrPlacement As DataRow In DTPlacement.Rows
                        hrsProjectPlacementEmployees = New Clshrs_ProjectPlacementEmployees(Me)
                        hrsProjectPlacementEmployees.Find("ID = " & DrPlacement("ID"))
                        hrsProjectPlacementEmployees.ToDate = DteEndOfServiceDate
                        hrsProjectPlacementEmployees.Update("ID = " & DrPlacement("ID"))
                    Next
                End If
                Dim strDelcommand As String = "set dateformat dmy; delete from Att_AttendTransactions where EmployeeID = " & IntEmployeeId & " and TrnsDatetime > '" & DteEndOfServiceDate.ToString("dd/MM/yyyy") & "'"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmpContracts.ConnectionString, Data.CommandType.Text, strDelcommand)
            End If

            Dim clsEmp As New Clshrs_Employees(Me)
            If clsEmp.Find("Code='" & lblDescEmployeeCode.Text & "'") Then
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">OpenPrintedScreen(" & clsEmp.ID & ");</script>")
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsObjNav.SetLanguage(Page, "Please Select Employee/الرجاء احتيار الموظف"))
            End If

            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsObjNav.SetLanguage(Page, "Save Done!/تم الحفظ"))
            lblDescEmployeeCode.Text = ""
            CheckCode()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub ImageButton2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        If lblDescEmployeeCode.Text.ToString.Trim.Length = 0 Then Exit Sub
        Dim IntEmployeeId As Integer = CheckEmployee(True)
        Dim ClsEmployeesTrancations As New Clshrs_EmployeesTransactions(Page)
        Dim ClsObjNav As New Venus.Shared.Web.NavigationHandler(ClsEmployeesTrancations.ConnectionString)

        Dim lasteosdate As Date
        Clshrs_EmployeesJoins = New Clshrs_EmployeesJoins(Me.Page)
        If Clshrs_EmployeesJoins.Find("EmployeeID = " & IntEmployeeId & " and canceldate is null order by JoinDate DESC") Then
            lasteosdate = Clshrs_EmployeesJoins.EndofServiceDate
            ClsEmployeesTrancations.Find("PrepareType in ('E','EN','EV','EL') and hrs_EmployeesTransactions.PostDate is not null and CONVERT(varchar(10),PaidDate,103) = '" & lasteosdate.ToString("dd/MM/yyyy") & "' and EmployeeID = " & IntEmployeeId)
            If (ClsEmployeesTrancations.DataSet.Tables(0).Rows.Count > 0) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsObjNav.SetLanguage(Page, "This Transaction Was Posted To Gl /هذه الحركة تم ترحيلها للحسابات"))
                Exit Sub
            End If

            ClsEmployeesTrancations.Find("PrepareType in ('E')  and CONVERT(varchar(10),PaidDate,103) = '" & lasteosdate.ToString("dd/MM/yyyy") & "' and EmployeeID = " & IntEmployeeId)

            Dim cmdstring As String = "delete from hrs_EmployeesTransactions where PrepareType in ('E','EN','EV','EL','ET') and hrs_EmployeesTransactions.PostDate is null and CONVERT(varchar(10),PaidDate,103) = '" & lasteosdate.ToString("dd/MM/yyyy") & "' and EmployeeID = " & IntEmployeeId
            If (ClsEmployeesTrancations.ID > 0) Then
                cmdstring = cmdstring & Environment.NewLine & "delete from hrs_EmployeesTransactions where PrepareType in ('ET') and regComputerId=" & ClsEmployeesTrancations.ID
            End If
            cmdstring = cmdstring & Environment.NewLine & "update hrs_Contracts set EndDate = null where ID in (select top 1 ID from hrs_Contracts where CancelDate is null and EmployeeID = " & IntEmployeeId & " order by ID desc)"
            cmdstring = cmdstring & Environment.NewLine & "update hrs_employees set ExcludeDate = null where ID = " & IntEmployeeId
            cmdstring = cmdstring & Environment.NewLine & "update hrs_EmployeesJoins set EndOfServiceID = null,EndOfServiceDate = null,EndOfServiceReson = null,EndOfServiceDateText = null,EOSDays = null,EOSMonths = null,EOSYears = null where CONVERT(varchar(10),EndOfServiceDate,103) = '" & lasteosdate.ToString("dd/MM/yyyy") & "' and EmployeeID = " & IntEmployeeId
            Dim rws As Integer = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeesTrancations.ConnectionString, CommandType.Text, "Set DateFormat DMY; " & cmdstring)
            If rws > 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsObjNav.SetLanguage(Page, "Refund Done /الإستعادة تمت "))
                CheckCode(False)
            End If
        End If
    End Sub
    Protected Sub ImageButton_Print_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_Print.Click
        Dim clsEmp As New Clshrs_Employees(Me)
        If clsEmp.Find("Code='" & lblDescEmployeeCode.Text & "'") Then
            ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">OpenPrintedScreen(" & clsEmp.ID & ");</script>")
        End If
        CheckCode(False)
    End Sub
    Protected Sub ImageButton_Print0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_Print0.Click
        Dim clsEmp As New Clshrs_Employees(Me)
        Dim ClsObjNav As New Venus.Shared.Web.NavigationHandler(clsEmp.ConnectionString)

        Dim d, m, y, t As Integer
        d = IIf(lblDescPaidDays.Text < 31, lblDescPaidDays.Text + 1, lblDescPaidDays.Text)
        m = IIf(lblDescPaidMonth.Text < 12, lblDescPaidMonth.Text + 1, lblDescPaidMonth.Text)
        y = lblDescPaidYear.Text + 2000
        t = lblVacationDue.Text

        Dim dmy As Date = CStr(d & "/" & m & "/" & y)

        If clsEmp.Find("Code='" & lblDescEmployeeCode.Text & "'") Then
            Venus.Shared.Web.ClientSideActions.OpenWindow(Page, "../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=EmpCode|EOSDate|EOSType|DMY|TotalDays&ReportCode=CF_TerminationLetter&sq0=''&v=" & clsEmp.Code & "|" & CDate(wdcEndOfServiceDate.Value).ToString("dd/MM/yyyy") & "|" & DdlEndofService.SelectedValue & "|" & dmy.ToString("MM/dd/yyyy") & "|" & t & "&preview=1", 700, 490, False, "wWindO", False)
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsObjNav.SetLanguage(Page, "Please Select Employee/الرجاء احتيار الموظف"))
        End If
    End Sub
    Protected Sub wdcEndOfServiceDate_ValueChanged(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs) Handles wdcEndOfServiceDate.ValueChanged
        CheckCode()
    End Sub
    Protected Sub lblDescEmployeeCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblDescEmployeeCode.TextChanged, DdlEndofService.SelectedIndexChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Function"

    Private Function CheckCode(Optional ByVal Hasmessage As Boolean = True)
        If blCheckTwice Then Return 0
        Dim ClsEmployee = New Clshrs_Employees(Page)
        Dim IntEmployeeId As Integer = CheckEmployee(True)
        Dim ClsEmpContracts As New Clshrs_Contracts(Page)
        Dim IntContractId As Integer = CIntInitValue
        Dim ClsObjNav As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
        Dim ContractStartDate As Date = Nothing
        ImageButton_Save.Enabled = True
        ImageButton_Print.Enabled = False
        ImageButton2.Enabled = False
        ImageButton_Print0.Enabled = True

        Clshrs_EmployeesJoins = New Clshrs_EmployeesJoins(Me.Page)
        If IntEmployeeId <= 0 Then
            lblDescEmployeeCode.Text = ""
            lblDescEnglishName.Text = ""

            ImageButton_Save.Enabled = False
            ImageButton_Print.Enabled = False
            ImageButton2.Enabled = False
            ImageButton_Print0.Enabled = False
        Else
            ClsEmployee.Find("ID=" & IntEmployeeId)
            Dim ClsEmployeeVacation = New Clshrs_EmployeesVacations(Page)
            If ClsEmployeeVacation.Find("ActualEndDate is null and EmployeeID = " & IntEmployeeId) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsObjNav.SetLanguage(Page, " Check This Employee Vacations /برجاء مراجعة حركة إجازات الموظف الحالى "))

                lblDescEmployeeCode.Text = ""
                lblDescEnglishName.Text = ""
                lblDescEmployeeCode.Focus()
                SetData(0, DateTime.Now.Date)
                Return 0

                ImageButton_Save.Enabled = False
                ImageButton_Print.Enabled = False
                ImageButton2.Enabled = False
                ImageButton_Print0.Enabled = False
            End If

            Clshrs_EmployeesJoins.Find("EmployeeID = " & IntEmployeeId & " and EndOfServiceDate is null and canceldate is null order by JoinDate ASC")
            If Clshrs_EmployeesJoins.DataSet.Tables(0).Rows.Count = 0 Then
                If Hasmessage = True Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsObjNav.SetLanguage(Page, " Current employee Already Has End Of Service !/هذا الموظف تم انهاء خدماته"))
                End If
                lblDescEmployeeCode.Text = ClsEmployee.Code
                lblDescEnglishName.Text = ClsEmployee.FullName
                ImageButton_Save.Enabled = False
                ImageButton_Print.Enabled = True
                ImageButton2.Enabled = True
                ImageButton_Print0.Enabled = False
                SetData(0, DateTime.Now.Date)
                GetEosData(IntEmployeeId)
                Return 0
            Else

                wdcEndOfServiceDate.Enabled = True
                DdlEndofService.Enabled = True

                'txtExtraBenfitsAmount.Enabled = True
                'txtExtraBenfitsRemarks.Enabled = True
                'txtExtraDeductionRemarks.Enabled = True
                'txtExtraDudectionAmount.Enabled = True
                Dim DteEndOfServiceDate As Date

                'txtExtraBenfitsAmount.Text = ""
                'txtExtraBenfitsRemarks.Text = ""
                'txtExtraDeductionRemarks.Text = ""
                'txtExtraDudectionAmount.Text = ""
                DteEndOfServiceDate = ClsEmployee.SetHigriDate(wdcEndOfServiceDate.Value)
                If Clshrs_EmployeesJoins.JoinDate > DteEndOfServiceDate Then
                    If Hasmessage = True Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsObjNav.SetLanguage(Page, " Current employee Has No Values At This Date !/هذا لا توجد له مستحقات فى هذا التاريخ"))
                    End If
                    lblDescEmployeeCode.Text = ClsEmployee.Code
                    lblDescEnglishName.Text = ClsEmployee.FullName
                    ImageButton_Save.Enabled = False
                    ImageButton_Print.Enabled = True
                    ImageButton2.Enabled = True
                    ImageButton_Print0.Enabled = False
                    SetData(0, DateTime.Now.Date)
                    Return 0
                End If
                IntContractId = ClsEmpContracts.ContractValidatoinId(IntEmployeeId, DteEndOfServiceDate)
                If IntContractId = 0 Then
                    If Hasmessage = True Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsObjNav.SetLanguage(Page, " Current employee hasn't any valid contracts !/هذا الموظف لا يوجد لديه عقد"))
                    End If

                    lblDescEmployeeCode.Text = ""
                    lblDescEnglishName.Text = ""
                    lblDescEmployeeCode.Focus()
                    SetData(0, DateTime.Now.Date)
                    Return 0

                    ImageButton_Save.Enabled = False
                    ImageButton_Print.Enabled = False
                    ImageButton2.Enabled = False
                    ImageButton_Print0.Enabled = False
                End If
                ClsEmpContracts.Find("ID = " & IntContractId)
                ContractStartDate = ClsEmpContracts.StartDate
            End If

            lblDescEmployeeCode.Text = ClsEmployee.Code
            lblDescEnglishName.Text = ClsEmployee.FullName
            SetData(IntEmployeeId, Clshrs_EmployeesJoins.JoinDate)

            Dim ClsEmployeeRelated = New Clshrs_Employees(Page)
            If (ClsEmployee.IsProjectRelated = True) Then
                If ClsEmployeeRelated.Find("Code = '" & ClsEmployee.Code & "cop'") Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsObjNav.SetLanguage(Page, " Kindly Review The Cop Employee and Do The Same Procedure To Him /برجاء مراجعة الموظف الرتبط وعمل نفس الاجراء له COP"))
                End If
            End If
        End If
        blCheckTwice = True
    End Function
    Private Sub GetEosData(IntEmployeeId As Integer)
        Dim DsEosDetails As New DataSet()
        Clshrs_EmployeesJoins = New Clshrs_EmployeesJoins(Me.Page)
        Clshrs_EmployeesJoins.Find("EmployeeID = " & IntEmployeeId & " and EndOfServiceDate is not null and canceldate is null order by JoinDate desc")

        If Clshrs_EmployeesJoins.ID > 0 Then

            'ImageButton_Save.Enabled = False
            'ImageButton_Print.Enabled = True
            'ImageButton2.Enabled = True
            'ImageButton_Print0.Enabled = False


            DdlEndofService.SelectedValue = Clshrs_EmployeesJoins.EndofServiceType
            DdlEndofService.Enabled = False

            wdcEndOfServiceDate.Value = Clshrs_EmployeesJoins.EndofServiceDate
            wdcEndOfServiceDate.Enabled = False

            'txtExtraBenfitsAmount.Enabled = False
            'txtExtraBenfitsRemarks.Enabled = False
            'txtExtraDeductionRemarks.Enabled = False
            'txtExtraDudectionAmount.Enabled = False



            lblLoansAmount.Text = ""
            lblRemainAmount.Text = ""
            lblLoansBalance.Text = ""
            lblVacationAmount.Text = ""
            lblWorkingDays.Text = ""
            lblBenifitsAmount.Text = ""
            lblVacationDue.Text = ""
            lblDescworkingDays.Text = ""


            lblDescPaidDays.Text = Clshrs_EmployeesJoins.EOSDays
            lblDescPaidMonth.Text = Clshrs_EmployeesJoins.EOSMonths
            lblDescPaidYear.Text = Clshrs_EmployeesJoins.EOSYears
            lblDescPaidTotal.Text = Clshrs_EmployeesJoins.EOSDays + (Clshrs_EmployeesJoins.EOSMonths * 30) + (Clshrs_EmployeesJoins.EOSYears * 360)
            lblNotPaidDays.Text = Clshrs_EmployeesJoins.EosOverDueDays
            lblNotPaidMonth.Text = Clshrs_EmployeesJoins.EosOverDueMonths
            lblDescNotPaidYear.Text = Clshrs_EmployeesJoins.EosOverDueYears
            lblNotPaidTotal.Text = Clshrs_EmployeesJoins.EosOverDueDays + (Clshrs_EmployeesJoins.EosOverDueMonths * 30) + (Clshrs_EmployeesJoins.EosOverDueYears * 360)
            lblservicedays.Text = Clshrs_EmployeesJoins.EosTotalDays
            lblservicemonths.Text = Clshrs_EmployeesJoins.EosTotalMonths
            lblserviceYears.Text = Clshrs_EmployeesJoins.EosTotalYears
            lblTotalservicedays.Text = Clshrs_EmployeesJoins.EosTotalDays + (Clshrs_EmployeesJoins.EosTotalMonths * 30) + (Clshrs_EmployeesJoins.EosTotalYears * 360)



            DsEosDetails = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Clshrs_EmployeesJoins.ConnectionString, Data.CommandType.Text, "exec GetEosDetails " & IntEmployeeId)


            If DsEosDetails.Tables(0).Select("PrepareType='E'").Length > 0 Then

                lblDescAmount.Text = Math.Round(DsEosDetails.Tables(0).Select("PrepareType='E'").CopyToDataTable.Rows(0)("Amount"), 0)
                'lblTotalBenefit1.Text = Math.Round(DsEosDetails.Tables(0).Compute("sum(amount)", "PrepareType='EN'"), 0)
            End If

            If DsEosDetails.Tables(0).Select("PrepareType='EN'").Length > 0 Then
                uwgTransactions.DataSource = DsEosDetails.Tables(0).Select("PrepareType='EN'").CopyToDataTable
                uwgTransactions.DataBind()
                lblWorkingDays.Text = DsEosDetails.Tables(0).Select("PrepareType='EN'").CopyToDataTable.Rows(0)("FinancialWorkingUnits")
                lblTotalBenefit1.Text = Math.Round(DsEosDetails.Tables(0).Compute("sum(amount)", "PrepareType='EN'"), 0)
            End If


            If DsEosDetails.Tables(0).Select("PrepareType='EV'").Length > 0 Then
                uwgVacationsTransactions.DataSource = DsEosDetails.Tables(0).Select("PrepareType='EV'").CopyToDataTable
                uwgVacationsTransactions.DataBind()

                lblVacationDue.Text = DsEosDetails.Tables(0).Select("PrepareType='EV'").CopyToDataTable.Rows(0)("FinancialWorkingUnits")
                lblTotalBenefits2.Text = Math.Round(DsEosDetails.Tables(0).Compute("sum(amount)", "PrepareType='EV'"), 0)
            End If


            If DsEosDetails.Tables(0).Select("PrepareType='ET' and sign='1'").Length > 0 Then
                'txtExtraBenfitsAmount.Text = DsEosDetails.Tables(0).Select("PrepareType='ET' and sign='1'").CopyToDataTable.Rows(0)("Amount")
                'txtExtraBenfitsRemarks.Text = DsEosDetails.Tables(0).Select("PrepareType='ET' and sign='1'").CopyToDataTable.Rows(0)("Description")

                uwgExtraBenfits.DataSource = DsEosDetails.Tables(0).Select("PrepareType='ET' and sign='1'").CopyToDataTable
                uwgExtraBenfits.DataBind()

            End If

            If DsEosDetails.Tables(0).Select("PrepareType='ET' and sign='-1'").Length > 0 Then
                'txtExtraDudectionAmount.Text = DsEosDetails.Tables(0).Select("PrepareType='ET' and sign='-1'").CopyToDataTable.Rows(0)("Amount")
                'txtExtraDeductionRemarks.Text = DsEosDetails.Tables(0).Select("PrepareType='ET' and sign='-1'").CopyToDataTable.Rows(0)("Description")
                uwgExtraDeduction.DataSource = DsEosDetails.Tables(0).Select("PrepareType='ET' and sign='-1'").CopyToDataTable
                uwgExtraDeduction.DataBind()

            End If

            If DsEosDetails.Tables(0).Select("PrepareType='EL'").Length > 0 Then
                DsEosDetails.Tables(0).Columns("Amount").ColumnName = "Remain"
                uwgPayabilities.DataSource = DsEosDetails.Tables(0).Select("PrepareType='El'").CopyToDataTable
                uwgPayabilities.DataBind()
                lblRemainAmount.Text = Math.Round(DsEosDetails.Tables(0).Compute("sum(Remain)", "PrepareType='EL'"), 0)

                DsEosDetails.Tables(0).Columns("Remain").ColumnName = "Amount"
            End If

            Dim TotalBenfits As Double
            If DsEosDetails.Tables(0).Select("sign='1'").Length > 0 Then
                TotalBenfits = Math.Round(DsEosDetails.Tables(0).Compute("sum(Amount)", "sign=1"), 0)
            End If

            Dim TotalDeduction As Double
            If DsEosDetails.Tables(0).Select("sign='-1'").Length > 0 Then
                TotalDeduction = Math.Round(DsEosDetails.Tables(0).Compute("sum(Amount)", "sign=-1"), 0)
            End If

            lblDescTotalDue.Text = TotalBenfits - TotalDeduction




        End If





    End Sub

    Private Function Get_NoDecimalPlaces() As Integer
        Try
            Dim clsMainCurrency As New ClsSys_Currencies(Page)
            Dim ClsCountries As New Clssys_Countries(Page)

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

    ''For Salary
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
    Private dbAbsentDays As Double
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

    Private Function Load_ClsLayers(ByVal BasDate As Date) As Boolean
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
            clsCompanies.Find("ID = " & clsBranch.MainCompanyID)
            clsBranch.Find("ID=" & ClsEmployees.BranchID)

            'Rabie 23-11-2025

            ''''ClsFisicalPeriod.GetFisicalperiodInfo(ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value), IntFisicalPeriod, FromDate, ToDate)

            ClsEmployees.Find("Code = '" & lblDescEmployeeCode.Text & "'")
            clsCompanies.Find("ID = " & clsBranch.MainCompanyID)
            clsBranch.Find("ID=" & ClsEmployees.BranchID)
            Dim fiscalperiood As New Clssys_FiscalYearsPeriods(Me)
            fiscalperiood.Find("ID = " & IntFisicalPeriod)

            FromDate = fiscalperiood.FromDate
            ToDate = fiscalperiood.ToDate
            If clsBranch.AffectPeriod Then
                ClsFisicalPeriod.GetFisicalperiodInfoByPrepareDay(ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value), IntFisicalPeriod, FromDate, ToDate)
            Else
                ClsFisicalPeriod.GetFisicalperiodInfo(ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value), IntFisicalPeriod, FromDate, ToDate)


            End If
            'If clsBranch.PrepareDay > 0 Then
            '    If ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value) >= New DateTime(fiscalperiood.FromDate.Year, fiscalperiood.FromDate.Month, clsBranch.PrepareDay) Then
            '        IntFisicalPeriod = IntFisicalPeriod + 1
            '    End If
            '    fiscalperiood.Find("ID = " & IntFisicalPeriod)
            '    FromDate = fiscalperiood.FromDate
            '    ToDate = fiscalperiood.ToDate
            '    FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsBranch.PrepareDay)
            '    ToDate = FromDate.AddMonths(1).AddDays(-1)
            'Else
            '    If clsCompanies.PrepareDay > 0 Then
            '        If ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value) >= New DateTime(fiscalperiood.FromDate.Year, fiscalperiood.FromDate.Month, clsCompanies.PrepareDay) Then
            '            IntFisicalPeriod = IntFisicalPeriod + 1
            '        End If
            '        fiscalperiood.Find("ID = " & IntFisicalPeriod)
            '        FromDate = fiscalperiood.FromDate
            '        ToDate = fiscalperiood.ToDate
            '        FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsCompanies.PrepareDay)
            '        ToDate = FromDate.AddMonths(1).AddDays(-1)
            '    End If
            'End If

            ClsFisicalPeriods.Find("ID = " & IntFisicalPeriod)
            clsCompanies.Find("ID=" & ClsFisicalPeriods.MainCompanyID)
            ClsEmployees.Find("Code = '" & lblDescEmployeeCode.Text & "'")
            clsBranch.Find("ID=" & ClsEmployees.BranchID)
            Get_FromToDate(BasDate)

            IntFisicalPeriod = ClsFisicalPeriods.ID
            intNoDecimalPlaces = Get_NoDecimalPlaces()
            intEmployeeContarctID = CheckEmployee(ClsEmployees.ID, IntFisicalPeriod)

            If intEmployeeContarctID = 0 Then Exit Function

            ClsEmployeesContracts.Find("ID = " & intEmployeeContarctID)
            ClsEmployeeClass.Find("ID =" & ClsEmployeesContracts.EmployeeClassID)

            Return True
        Catch ex As Exception
            Return False
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
    Private Function Get_FromToDate(ByVal Basedate As Date) As Boolean
        Try
            ClsEmployees = New Clshrs_Employees(Page)


            If clsBranch.AffectPeriod Then
                ClsFisicalPeriod.GetFisicalperiodInfoByPrepareDay(ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value), IntFisicalPeriod, FromDate, ToDate)
            Else
                ClsFisicalPeriod.GetFisicalperiodInfo(ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value), IntFisicalPeriod, FromDate, ToDate)


            End If
            FiscFromDate = FromDate
            FiscToDate = ToDate
            ClsEmployees.Find("Code = '" & lblDescEmployeeCode.Text & "'")
            'Rabie 01-12-2025
            'FromDate = IIf(Basedate > ClsFisicalPeriods.FromDate, Basedate, ClsFisicalPeriods.FromDate)
            'ToDate = IIf(ClsFisicalPeriods.ToDate > ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value), ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value), ClsFisicalPeriods.ToDate)
            'FiscFromDate = ClsFisicalPeriods.FromDate
            'FiscToDate = ClsFisicalPeriods.ToDate

            If clsBranch.PrepareDay > 0 And Not clsBranch.AffectPeriod Then
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
                If clsCompanies.PrepareDay > 0 And Not clsBranch.AffectPeriod Then
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
    Private Function CheckEmployee(ByVal IntInComingEmployeeID As Integer, ByVal FiscalPeriodID As Integer) As Integer
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsContract As New Clshrs_Contracts(Page)
        Dim clsEmployeevacations As New Clshrs_EmployeesVacations(Page)
        ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim ClsFiscalPeriods As New Clssys_FiscalYearsPeriods(Page)
        Dim intValidContract As Integer = 0
        Try
            ClsFiscalPeriods.Find("ID=" & FiscalPeriodID)
            ClsEmployees.Find("CancelDate is Null And ID = " & IntInComingEmployeeID)
            intValidContract = ClsContract.ContractValidatoinId(ClsEmployees.ID, FiscalPeriodID)
            If intValidContract <= 0 Then
                Return 0
                Exit Function
            End If

            If (clsEmployeevacations.FindEmployeeVacations(" hrs_EmployeesVacations.VacationTypeID in (select ID from hrs_VacationsTypes where CancelDate is null and IsAnnual = 1) and hrs_EmployeesVacations.EmployeeID=" & ClsEmployees.ID & " And Convert(smalldatetime,Convert(varchar,ActualStartDate ,103)) <= Convert(smalldatetime,Convert(varchar,'" & FiscFromDate & "' ,103))	And	(ActualEndDate Is Null Or  Convert(smalldatetime,Convert(varchar,ActualEndDate ,103)) > Convert(smalldatetime,Convert(varchar,'" & FiscToDate & "',103)))")) Then
                Return 0
            End If
            Return intValidContract
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Function SetScreenSetting(EmpID As Integer) As Boolean
        Try
            arrList = GetEmployeeGeneralInfromation(ClsEmployees.ID)
            ObjPrepaerdData = ClsEmployees.GetPreparedEmployessForSalariesByEmployeeID(IntFisicalPeriod, EmpID, FromDate, ToDate, FiscFromDate, FiscToDate)

            Dim Amount As Double = 0
            Calculate_SalaryPerHour(Amount, EmpID, ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value))

            ObjSalaryPerDay = Math.Round(Amount / IntNoOfDays, intNoDecimalPlaces)
            ObjSalaryPerHour = Math.Round(ObjSalaryPerDay / ClsEmployeeClass.WorkHoursPerDay, intNoDecimalPlaces)
            ObjOverTime = Math.Round(ObjSalaryPerHour * ClsEmployeeClass.OvertimeFactor, intNoDecimalPlaces)
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
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetEmployeeGeneralInfromation(ByVal intEmployeeID As Object) As ArrayList
        Dim EmpInfoarrList As New ArrayList
        Dim IntFiscalPeriod As Integer = IntFisicalPeriod
        Dim IntContractID As Integer
        Dim IntEmpClassID As Integer
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsContractTransaction As New Clshrs_ContractsTransactions(Page)
        Dim ClsContract As New Clshrs_Contracts(Page)
        Dim clsTransactionsTypes As New Clshrs_TransactionsTypes(Page)
        ClsEmployeeClass = New Clshrs_EmployeeClasses(Page)

        ClsEmployees.Find("ID=" & intEmployeeID)
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
    Private Function Get_Attendance(ByVal EmpID As Integer, Optional ByVal IsNew As Boolean = True) As Boolean
        'Try
        '    Dim intAbsent As Integer = 0
        '    Dim Total_OT As Double = 0
        '    Dim Total_HD As Double = 0
        '    Dim Total_Absent As Double = 0
        '    ObjTotalLate = 0
        '    NotPermitLat = 0

        '    clsAttendancePreparationProjects = New ClsAtt_AttendancePreparationProjects(Page)
        '    ClsAttendancePreparationDetails.Find("EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)")
        '    Dim DT As Data.DataTable = ClsAttendancePreparationDetails.DataSet.Tables(0)
        '    Dim cnt As Integer = 0
        '    If DT.Rows.Count > 0 Then
        '        For i As Integer = 0 To Get_NoOfDays() - 1
        '            Try
        '                If FromDate.AddDays(i) > ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value) Then
        '                    intAbsent = intAbsent + 1
        '                    Continue For
        '                End If
        '                If ClsAttendancePreparationDetails.Find("EmployeeID = " & ClsEmployees.ID & " and GAttendDate = '" & FromDate.AddDays(i).ToString("dd/MM/yyyy") & "'") Then
        '                    If ClsAttendancePreparationDetails.IsAbsent = True And ClsAttendancePreparationDetails.IsVacation = False And ClsAttendancePreparationDetails.LeavingType = 0 Then
        '                        intAbsent = intAbsent + 1
        '                    ElseIf ClsAttendancePreparationDetails.LeavingType <> 0 Then
        '                        If ClsVacationsTypes.Find("ID = " & ClsAttendancePreparationDetails.LeavingType) Then
        '                            If ClsVacationsTypes.IsPaid = -1 Then
        '                                If ClsVacationsTypes.IsAnnual <> True Then
        '                                    intAbsent = intAbsent + 1
        '                                End If
        '                            End If
        '                        End If
        '                    End If
        '                Else
        '                    intAbsent = intAbsent + 1
        '                    Continue For
        '                End If
        '                cnt = cnt + 1
        '            Catch ex As Exception
        '            End Try
        '        Next
        '    End If
        '    NotPermitLat = Math.Round(NotPermitLat, intNoDecimalPlaces)
        '    dbTotalAbsent = Math.Round(Total_Absent, intNoDecimalPlaces)
        '    dbOvertimeSalary = Math.Round(Total_OT, intNoDecimalPlaces)
        '    dbHolidayHoursSalary = Math.Round(Total_HD, intNoDecimalPlaces)
        '    If cnt = intAbsent Then
        '        IntNoOfWorkDays = 0
        '    Else
        '        IntNoOfWorkDays = Convert.ToInt32(Get_NoOfDays()) - IIf(intAbsent > Convert.ToInt32(Get_NoOfDays()), Convert.ToInt32(Get_NoOfDays()), intAbsent)
        '    End If

        '    Dim CntDays As Integer = IIf(FiscToDate > ToDate, ToDate, FiscToDate).Subtract(FiscFromDate).Days + 2
        '    Dim VacDays As Integer = 0
        '    Dim IsToEnd As Boolean = False
        '    For CounDays As Integer = 0 To CntDays
        '        Dim OperDate As DateTime = FiscFromDate.AddDays(CounDays)
        '        Dim hrsEmployeesVacations As New Clshrs_EmployeesVacations(Me)
        '        hrsEmployeesVacations.Find("EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103) >= CONVERT(date,ActualStartDate,103) and CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103) < CONVERT(date,isnull(ActualEndDate,'01/01/2050'),103) and VacationTypeID in (select ID from hrs_VacationsTypes where IsAnnual = 1)")
        '        If (hrsEmployeesVacations.DataSet.Tables(0).Rows.Count > 0) Then
        '            If OperDate < ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value) Then
        '                If OperDate >= IIf(FiscToDate > ToDate, ToDate, FiscToDate) Then
        '                    IsToEnd = True
        '                    Continue For
        '                Else
        '                    VacDays = VacDays + 1
        '                End If
        '            End If
        '        End If
        '    Next
        '    If IsToEnd Then
        '        If IIf(FiscToDate > ToDate, ToDate, FiscToDate).Subtract(FiscFromDate).Days <> ToDate.Subtract(FromDate).Days Then
        '            If IIf(FiscToDate > ToDate, ToDate, FiscToDate).Subtract(FiscFromDate).Days > ToDate.Subtract(FromDate).Days Then
        '                VacDays = VacDays - 1
        '            ElseIf IIf(FiscToDate > ToDate, ToDate, FiscToDate).Subtract(FiscFromDate).Days < ToDate.Subtract(FromDate).Days Then
        '                If ClsEmployeeClass.NoOfDaysPerPeriod > 0 Then
        '                Else
        '                    VacDays = VacDays + 1
        '                End If
        '            End If
        '        Else
        '            If ClsEmployeeClass.NoOfDaysPerPeriod > 0 And IIf(FiscToDate > ToDate, ToDate, FiscToDate).Subtract(FiscFromDate).Days + 1 > ClsEmployeeClass.NoOfDaysPerPeriod Then
        '                VacDays = VacDays - 1
        '            End If
        '        End If
        '    End If

        '    If ClsEmployees.JoinDate > ToDate And ClsEmployees.JoinDate <= IIf(FiscToDate > ToDate, ToDate, FiscToDate) And IntNoOfWorkDays = 0 Then
        '        IntNoOfWorkDays = IIf(FiscToDate > ToDate, ToDate, FiscToDate).Subtract(ClsEmployees.JoinDate).Days + 1
        '    End If
        '    IntNoOfWorkDays = IntNoOfWorkDays - IIf(VacDays < 0, 0, VacDays)
        '    Dim clsemployeetransaction As New Clshrs_EmployeesTransactions(Page)
        '    If ClsEmployeesTrancations.Find("EmployeeID = " & ClsEmployees.ID & " and FiscalYearPeriodID = " & IntFisicalPeriod & " and PrepareType = 'N'") Then
        '        IntNoOfWorkDays = 0
        '    End If
        '    Return True
        'Catch ex As Exception
        '    Return False
        'End Try

        Try
            Dim intAbsent As Integer = 0
            Dim Total_OT As Double = 0
            Dim Total_HD As Double = 0
            Dim Total_Absent As Double = 0
            ObjTotalLate = 0
            NotPermitLat = 0
            Dim fromdate2 As DateTime = FromDate
            Dim StartFiscal As DateTime
            Dim EndFiscal As DateTime
            Dim ClsFisicalPeriod2 As New Clssys_FiscalYearsPeriods(Page)
            Dim IntFisicalPeriod2 As Integer
            'Rabie 3-12-2025
            'ClsFisicalPeriod2.GetFisicalperiodInfo(ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value), IntFisicalPeriod2, StartFiscal, EndFiscal)

            If clsBranch.AffectPeriod Then
                ClsFisicalPeriod2.GetFisicalperiodInfoByPrepareDay(ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value), IntFisicalPeriod2, FromDate, ToDate)
            Else
                ClsFisicalPeriod2.GetFisicalperiodInfo(ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value), IntFisicalPeriod2, FromDate, ToDate)


            End If
            If Convert.ToDateTime(wdcEndOfServiceDate.Value).Day > clsCompanies.PrepareDay Then
                ' fromdate2 = fromdate2.AddMonths(-1)
            End If


            clsAttendancePreparationProjects = New ClsAtt_AttendancePreparationProjects(Page)
            ClsAttendancePreparationDetails.Find("EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & fromdate2.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & Convert.ToDateTime(wdcEndOfServiceDate.Value).ToString("dd/MM/yyyy") & "',103)")
            Dim DT As Data.DataTable = ClsAttendancePreparationDetails.DataSet.Tables(0)
            Dim cnt As Integer = 0
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To Get_NoOfDays() - 1
                    Try
                        If fromdate2.AddDays(i) > ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value) Then
                            ' intAbsent = intAbsent + 1
                            Continue For
                        End If
                        If ClsAttendancePreparationDetails.Find("EmployeeID = " & ClsEmployees.ID & " and GAttendDate = '" & fromdate2.AddDays(i).ToString("dd/MM/yyyy") & "'") Then
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
                        Else
                            intAbsent = intAbsent + 1
                            Continue For
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
            If cnt = intAbsent Then
                IntNoOfWorkDays = 0
            Else
                IntNoOfWorkDays = Convert.ToDateTime(wdcEndOfServiceDate.Value).Day - intAbsent 'Convert.ToInt32(Get_NoOfDays()) - IIf(intAbsent > Convert.ToInt32(Get_NoOfDays()), Convert.ToInt32(Get_NoOfDays()), intAbsent)
            End If

            Dim CntDays As Integer = IIf(FiscToDate > ToDate, ToDate, FiscToDate).Subtract(FiscFromDate).Days + 2
            Dim VacDays As Integer = 0
            Dim IsToEnd As Boolean = False
            For CounDays As Integer = 0 To CntDays
                Dim OperDate As DateTime = FiscFromDate.AddDays(CounDays)
                Dim hrsEmployeesVacations As New Clshrs_EmployeesVacations(Me)
                hrsEmployeesVacations.Find("EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103) >= CONVERT(date,ActualStartDate,103) and CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103) < CONVERT(date,isnull(ActualEndDate,'01/01/2050'),103) and VacationTypeID in (select ID from hrs_VacationsTypes where IsAnnual = 1)")
                If (hrsEmployeesVacations.DataSet.Tables(0).Rows.Count > 0) Then
                    If OperDate < ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value) Then
                        If OperDate >= IIf(FiscToDate > ToDate, ToDate, FiscToDate) Then
                            IsToEnd = True
                            Continue For
                        Else
                            VacDays = VacDays + 1
                        End If
                    End If
                End If
            Next
            'If IsToEnd Then
            '    If IIf(FiscToDate > ToDate, ToDate, FiscToDate).Subtract(FiscFromDate).Days <> ToDate.Subtract(FromDate).Days Then
            '        If IIf(FiscToDate > ToDate, ToDate, FiscToDate).Subtract(FiscFromDate).Days > ToDate.Subtract(FromDate).Days Then
            '            VacDays = VacDays - 1
            '        ElseIf IIf(FiscToDate > ToDate, ToDate, FiscToDate).Subtract(FiscFromDate).Days < ToDate.Subtract(FromDate).Days Then
            '            If ClsEmployeeClass.NoOfDaysPerPeriod > 0 Then
            '            Else
            '                VacDays = VacDays + 1
            '            End If
            '        End If
            '    Else
            '        If ClsEmployeeClass.NoOfDaysPerPeriod > 0 And IIf(FiscToDate > ToDate, ToDate, FiscToDate).Subtract(FiscFromDate).Days + 1 > ClsEmployeeClass.NoOfDaysPerPeriod Then
            '            VacDays = VacDays - 1
            '        End If
            '    End If
            'End If

            If ClsEmployees.JoinDate > ToDate And ClsEmployees.JoinDate <= IIf(FiscToDate > ToDate, ToDate, FiscToDate) And IntNoOfWorkDays = 0 Then
                IntNoOfWorkDays = IIf(FiscToDate > ToDate, ToDate, FiscToDate).Subtract(ClsEmployees.JoinDate).Days + 1
            End If

            IntNoOfWorkDays = IntNoOfWorkDays - IIf(VacDays < 0, 0, VacDays)


            Dim endOfserviceDate As Date = CDate(wdcEndOfServiceDate.Value)
            'Dim dsAttandance As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, "set dateformat dmy  select * from dbo.AttendanceEffects(" & ClsEmployees.ID & ",'" & StartFiscal.ToString("dd/MM/yyy") & "','" & endOfserviceDate.ToString("dd/MM/yyy") & "'," & IntFisicalPeriod2 & ",1 )")
            'Dim dsAttandance As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, "set dateformat dmy  select * from dbo.AttendanceEffects(" & ClsEmployees.ID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & endOfserviceDate.ToString("dd/MM/yyy") & "'," & IntFisicalPeriod & ",0,NULL,1 )")
            'Dim dsAttandance As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, "set dateformat dmy  select * from dbo.AttendanceEffects(" & ClsEmployees.ID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & endOfserviceDate.ToString("dd/MM/yyy") & "'," & ClsFisicalPeriod2.ID & ",0,NULL,1 )")
            Dim dsAttandance As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, "set dateformat dmy  select * from dbo.AttendanceEffects(" & ClsEmployees.ID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & endOfserviceDate.ToString("dd/MM/yyy") & "'," & ClsFisicalPeriod2.ID & ",0,NULL,1 ,'" & FiscFromDate.ToString("dd/MM/yyy") & "','" & FiscToDate.ToString("dd/MM/yyy") & "' )")
            If dsAttandance.Tables(0).Rows.Count > 0 Then
                IntNoOfWorkDays = dsAttandance.Tables(0).Rows(0)("WorkingDays")
                intAbsent = dsAttandance.Tables(0).Rows(0)("AbsentDays")
                VacDays = dsAttandance.Tables(0).Rows(0)("vactiondays")
            End If
            Dim clsemployeetransaction As New Clshrs_EmployeesTransactions(Page)
            If ClsEmployeesTrancations.Find("EmployeeID = " & ClsEmployees.ID & " and FiscalYearPeriodID = " & IntFisicalPeriod2 & " and PrepareType = 'N'") Then
                IntNoOfWorkDays = 0
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function New_Value(ByVal blnDefaultWorkingUnits As Boolean) As Boolean
        Try
            If blnDefaultWorkingUnits Then
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
    ''' '''''''''''''''''''''End OF Salary Items Settings''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub SetData(ByVal IntEmployeeId As Integer, ByVal BaseDate As Date)
        If IntEmployeeId = 0 Then
            Clear()
            Exit Sub
        End If
        Dim ClsContract As New Clshrs_Contracts(Page)
        Dim ClsPayabilities As New Clshrs_EmployeesPayability(Page)
        Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(Page)
        Dim ClsSolver = New Clshrs_FormulaSolver(ClsContract.ConnectionString, Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsContract.ConnectionString)
        Dim Clssys_GHCalendar As New Clssys_GHCalendar(Page)
        Dim IntContractId As Integer = CIntInitValue
        Dim IntTotalDays As Integer = CIntInitValue
        Dim IntYear As Integer = CIntInitValue
        Dim IntMonth As Integer = CIntInitValue
        Dim IntDays As Integer = CIntInitValue
        Dim IntNotYear As Integer = CIntInitValue
        Dim IntNotMonth As Integer = CIntInitValue
        Dim IntNotDays As Integer = CIntInitValue
        Dim IntUnPaid As Integer = CIntInitValue
        Dim IntTransId As Integer = CIntInitValue
        Dim IntPeriodWorkingUint As Double = CIntInitValue
        Dim IntPaid As Integer
        Dim dblVacationDays As Double = CIntInitValue
        Dim DblAmount As Double = CIntInitValue
        Dim DblPayabilityBalance As Double = CIntInitValue
        Dim DblPayabilities As Double = CIntInitValue
        Dim DblFinalizedPayabilities As Double = CIntInitValue
        Dim dblBenefits As Double = CIntInitValue
        Dim dblDeduct As Double = CIntInitValue
        Dim DteFromDate As Date = Nothing
        Dim DteToDate As Date = Nothing
        Dim dtContractStartDate As Date
        Dim DteEndOfServiceDate As DateTime
        Dim DtBenefits As New Data.DataTable
        Dim DtDeductions As New Data.DataTable
        Dim DtBenefitsDeduction As New Data.DataTable
        Dim DtBenefitsDeductionVac As New Data.DataTable
        Dim ClsEmployeesVacations As New Clshrs_EmployeesVacations(Page)
        Dim ClsVacations As New Clshrs_VacationsTypes(Page)
        Dim ClsContracts As New Clshrs_Contracts(Page)
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim extrabenfit As Double
        Dim dsPayability As New Data.DataSet
        Dim dblTotalReamin As Double = 0
        Dim IntserviceYear As Integer = CIntInitValue
        Dim IntserviceMonth As Integer = CIntInitValue
        Dim IntserviceDays As Integer = CIntInitValue
        Dim IntTotalServicesDays As Integer = CIntInitValue
        Dim VactotalDays As Single = 0
        Dim VacunpaidDays As Single = 0
        Dim VacNetDays As Single = 0

        ClsEndOfService = New Clshrs_EndOfServices(Page)
        ClsEmployeesTrancations = New Clshrs_EmployeesTransactions(Page)
        ClsFisicalPeriod = New Clssys_FiscalYearsPeriods(Page)

        CreateDataTable(DtBenefits, "Benefits")
        CreateDataTable(DtDeductions, "Deductions")
        CreateBenefitDeductionDataTable(DtBenefitsDeduction, "BenefitsDeductions")
        CreateBenefitDeductionDataTable(DtBenefitsDeductionVac, "BenefitsDeductionsVac")

        Load_ClsLayers(BaseDate)
        If Not SetScreenSetting(IntEmployeeId) Then Return
        New_Value(True)
        If Not Get_Attendance(IntEmployeeId) Then Return

        '==========================================================================
        '2- Get the valid Contract ID if the employee have more than one contract 
        '3- Get the Employee Working Days From the last Endofserivce or from the first Contract 
        '4- Get the Employee UnPaid Days 
        '==========================================================================
        DteEndOfServiceDate = ClsEmployees.SetHigriDate(wdcEndOfServiceDate.Value)
        IntContractId = ClsContract.ContractValidatoinId(IntEmployeeId, DteEndOfServiceDate)


        ClsEmployees.Find("ID=" & IntEmployeeId)
        ClsEndOfService.GetEmployeeWorkingDays(IntEmployeeId, ClsContract.ID, IntYear, IntMonth, IntDays, IntTotalDays, IntUnPaid, DteEndOfServiceDate, BaseDate, IntTotalServicesDays)
        ClsEndOfService.GetEmployeeNonPaiedDays(IntUnPaid, IntNotYear, IntNotMonth, IntNotDays)
        ClsEndOfService.GetYearsMonthsDays(clsCompanies.RegComputerID, IntTotalServicesDays, IntserviceYear, IntserviceMonth, IntserviceDays)

        '==========================================================================
        '5- Fill the Employees working Days 
        '==========================================================================
        lblDescworkingDays.Text = IntTotalDays
        lblDescPaidYear.Text = IntYear
        lblDescPaidMonth.Text = IntMonth
        lblDescPaidDays.Text = IntDays
        lblDescPaidTotal.Text = IntTotalDays


        lblDescNotPaidYear.Text = IntNotYear
        lblNotPaidMonth.Text = IntNotMonth
        lblNotPaidDays.Text = IntNotDays
        lblNotPaidTotal.Text = IntUnPaid


        lblserviceYears.Text = IntserviceYear
        lblservicemonths.Text = IntserviceMonth
        lblservicedays.Text = IntserviceDays
        lblTotalservicedays.Text = IntTotalServicesDays

        '==========================================================================
        '6- Calculate the Endofservice Amount 
        '7- Calculate the Total Amount should be paid or received 
        '==========================================================================

        ClsEndOfService.CalculateEndofService(IntEmployeeId, IntTotalDays, DdlEndofService.SelectedItem.Value, DblAmount, DteEndOfServiceDate)


        'Extra End of service transaction
        ClsEndOfService.Find("ID=" & DdlEndofService.SelectedItem.Value)
        If ClsEndOfService.ID > 0 Then
            ClsTransactionTypes.Find("ID=" & ClsEndOfService.ExtraTransactionID)
            ClsSolver.EmployeeID = IntEmployeeId
            ClsSolver.NoOfDaysPerPeriod = 30
            ClsSolver.NoOfWorkingDays = 30
            ClsSolver.Executedate = DteEndOfServiceDate

            Dim baseFormula = ClsTransactionTypes.Formula
            If ClsTransactionTypes.ID > 0 Then
                If ClsEmployees.IsSocialInsuranceIncluded Then
                    If ClsTransactionTypes.HasInsuranceTiers Then
                        Dim sTSql = "SELECT  TOP (1) BaseFormulaTiers FROM     hrs_TransactionsTypesTiers WHERE    (TransactionsTypesId = " & ClsTransactionTypes.ID & ") AND ((MONTH(FinancialPeriodTiers) <= " & DteEndOfServiceDate.Month & " AND YEAR(FinancialPeriodTiers) = " & DteEndOfServiceDate.Year & ") or YEAR(FinancialPeriodTiers) < " & DteEndOfServiceDate.Year & " ) order by FinancialPeriodTiers desc"
                        Dim strFormula = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, sTSql)
                        If strFormula <> "" Then
                            baseFormula = strFormula
                        Else
                            baseFormula = ClsTransactionTypes.Formula
                        End If
                    Else
                        baseFormula = ClsTransactionTypes.Formula
                    End If
                Else
                    baseFormula = ClsTransactionTypes.Formula
                End If
            End If

            ClsSolver.EvaluateExpression(baseFormula)
            extrabenfit += ClsSolver.Output



        End If




        lblDescAmount.Text = Math.Round(DblAmount, Get_NoDecimalPlaces())
        '==========================================================================
        '8- Fill the Employees payability 
        '==========================================================================
        DblPayabilityBalance = ClsPayabilities.GetEmployeePayabilityBalance(IntEmployeeId)
        ClsPayabilities.GetAllPayabilitiesDetails(IntEmployeeId, dsPayability)

        For Each row As Data.DataRow In dsPayability.Tables(0).Rows
            ClsTransactionTypes.Find("ID=" & row("TransactionTypeID"))
            If ClsTransactionTypes.Sign = -1 And row("Remain") > 0 Then
                dblTotalReamin += IIf(IsDBNull(row("Remain")), 0, row("Remain"))
            Else
                row.Delete()
            End If
        Next
        uwgPayabilities.DataSource = dsPayability
        uwgPayabilities.DataBind()

        '==========================================================================
        '9- Get all the due Benefits & Deductions according to the working days until now 
        '========================================================================== 

        DteFromDate = FiscFromDate
        If IntFisicalPeriod = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, " No found Fiscal Period/لايوجد فترة مالية فى هذا التاريخ"))
            Exit Sub
        End If

        If (IntNoOfWorkDays > 0) Then
            IntPaid = 1
        Else
            IntPaid = 0
        End If

        If ObjPrepaerdData(CPreparedData_FirstPrepare) = Clshrs_EmployeesBase.ePrepareType.BeginOfContract Then
            ClsEmployees.CollectEmployeesTransactions("TE", ToDate, IntEmployeeId, IntFisicalPeriod, DtBenefits, DtDeductions, dblBenefits, dblDeduct, _
            IntNoOfWorkDays, 0, IntNoOfWorkDays, dbOvertimeSalary, dbHolidayHoursSalary, ObjSalaryPerHour, ObjSalaryPerDay, NotPermitLat, 0, Clshrs_EmployeesBase.ePrepareType.EndOfContract, Clshrs_EmployeesBase.ePrepareStage.Normal)
        Else
            ClsEmployees.CollectEmployeesTransactions("T", ToDate, IntEmployeeId, IntFisicalPeriod, DtBenefits, DtDeductions, dblBenefits, dblDeduct, _
            IntNoOfWorkDays, 0, IntNoOfWorkDays, dbOvertimeSalary, dbHolidayHoursSalary, ObjSalaryPerHour, ObjSalaryPerDay, NotPermitLat, 0, Clshrs_EmployeesBase.ePrepareType.EndOfContract, Clshrs_EmployeesBase.ePrepareStage.Normal)
        End If

        Dim Extrastrcommand As String = "select (isnull((select top 1 ID from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode),0)) as RelTransactionID,(isnull((select top 1 Sign from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode),0)) as Sign,Amount from hrs_EmployeeExtraItems"
        Dim strFilter As String = " where EmployeeCode = '" & ClsEmployees.Code & "' and Status = 1 and FiscalPeriodID =" & IntFisicalPeriod
        Dim dsEmployee As New Data.DataSet
        dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, Data.CommandType.Text, Extrastrcommand & strFilter)
        For EXB As Integer = 0 To dsEmployee.Tables(0).Rows.Count - 1
            If dsEmployee.Tables(0).Rows(EXB)(1) > 0 Then
                DtBenefits.Rows.Add(New Object() {dsEmployee.Tables(0).Rows(EXB)(0), dsEmployee.Tables(0).Rows(EXB)(2), "Paid", Nothing, "Paid"})
            ElseIf dsEmployee.Tables(0).Rows(EXB)(1) < 0 Then
                DtDeductions.Rows.Add(New Object() {dsEmployee.Tables(0).Rows(EXB)(0), dsEmployee.Tables(0).Rows(EXB)(2), "Paid", Nothing, "Paid"})
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
                DtBenefits.Rows.Add(New Object() {ClsEmployeeClass.HOvertimeTransaction, HOTvalue, "Paid", Nothing, "Paid"})
            End If
        End If

        '===========Absent Claculation Rabie 23-11-2025========================================
        Dim AbsDys As Decimal = 0
        Dim ObjAbsent As Double = 0
        Dim baseFormulas As String = ""

        If ClsEmployeeClass.RegComputerID > 0 Then
            Dim strcommand As String = "select isnull(sum(SalaryPerDay),0) from Att_AttendancePreparationProjects where isnull(IsAbsent,0) = 1  and TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) " 'and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
            Dim Absvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
            ' strcommand = "select isnull(count(ID),0) from Att_AttendancePreparationProjects where isnull(IsAbsent,0) = 1  and TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) and isnull(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' and ProjectID = " & clsProject.ID
            strcommand = "SELECT sum(b) FROM ( SELECT ISNULL(COUNT(pp.ID),0) AS b FROM Att_AttendancePreparationProjects pp INNER JOIN Att_AttendancePreparationDetails pd ON pp.TrnsID = pd.ID WHERE ISNULL(pp.IsAbsent,0) = 1  AND TrnsID IN (select ID from Att_AttendancePreparationDetails WHERE EmployeeID = " & ClsEmployees.ID & " AND CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) AND CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) )as x" 'AND ISNULL(LinkedCS,'') = '" & DTProjects.Rows(i)("LinkedCS") & "' AND ProjectID = " & clsProject.ID & " GROUP BY pd.GAttendDate	) AS Count"
            AbsDys = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
            'absent formaula

            clsTransType.Find("ID= " & ClsEmployeeClass.RegComputerID)
            If clsTransType.ID > 0 And clsTransType.Formula <> "" Then
                If ClsEmployees.IsSocialInsuranceIncluded Then
                    If clsTransType.HasInsuranceTiers Then
                        Dim sTSql = "SELECT  TOP (1) BaseFormulaTiers FROM     hrs_TransactionsTypesTiers WHERE    (TransactionsTypesId = " & clsTransType.ID & ") AND ((MONTH(FinancialPeriodTiers) <= " & FromDate.Month & " AND YEAR(FinancialPeriodTiers) = " & FromDate.Year & ") or YEAR(FinancialPeriodTiers) < " & FromDate.Year & " ) order by FinancialPeriodTiers desc"
                        Dim strFormula = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, sTSql)
                        If strFormula <> "" Then
                            baseFormulas = strFormula
                        Else
                            baseFormulas = clsTransType.Formula
                        End If
                    Else
                        baseFormulas = clsTransType.Formula
                    End If
                Else
                    baseFormulas = clsTransType.Formula
                End If
                ClsSolver.EmployeeID = ClsEmployees.ID
                ClsSolver.FiscalPeriodID = ClsFisicalPeriods.ID
                ClsSolver.NoOfDaysPerPeriod = ClsEmployeeClass.NoOfDaysPerPeriod
                ClsSolver.Executedate = ToDate
                ClsSolver.EvaluateExpression(baseFormulas)
                ObjAbsent = IIf(IsNumeric(ClsSolver.Output), ClsSolver.Output, 0)

            End If

            If clsTransType.ID > 0 And ClsEmployeeClass.AbsentFormula <> "" Then

                ClsSolver.EmployeeID = ClsEmployees.ID
                ClsSolver.FiscalPeriodID = ClsFisicalPeriods.ID
                ClsSolver.NoOfDaysPerPeriod = ClsEmployeeClass.NoOfDaysPerPeriod
                ClsSolver.Executedate = ToDate
                ClsSolver.EvaluateExpression(ClsEmployeeClass.AbsentFormula)
                ObjAbsent = IIf(IsNumeric(ClsSolver.Output), ClsSolver.Output, 0)

            End If

            If ObjAbsent > 0 Then
                Absvalue = ObjAbsent * AbsDys
            End If

            If Absvalue > 0 Then
                DtDeductions.Rows.Add(New Object() {ClsEmployeeClass.RegComputerID, Absvalue, "Paid", Nothing, "Paid"})

                'Dim cmdString As String = ""
                'cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" &
                '                                         " Values (@ProjectTransID," & ClsEmployeeClass.RegComputerID & "," & Absvalue & ",'" & IIf(clsTransType.IsPaid, "Piaid", "Not Paid") & "',Null); "

                'If clsTransType.IsPaid = True Then
                'End If

            End If


        End If

        '=======================End Od Absent Aclculation ==========================================

        lblWorkingDays.Text = IntNoOfWorkDays
        Dim Rows As Data.DataRow() = DtBenefits.Select("Amount >0 And DescriptionSign='Paid' or Amount >0 And DescriptionSign='Paid By Days'")
        Dim RowsDet As Data.DataRow() = DtDeductions.Select("Amount >0 And DescriptionSign='Paid' or Amount >0 And DescriptionSign='Paid By Days'")
        Dim dtTemp As Data.DataTable = DtBenefits.Clone()
        dblBenefits = 0

        If IntPaid <> 0 Then
            For Each row As Data.DataRow In Rows
                If row(1).ToString.Contains("Infinity") Then
                ElseIf row(1).ToString.Contains("لا نهاية") Then
                Else
                    Dim nRow As Data.DataRow = dtTemp.NewRow()
                    nRow(0) = row(0)
                    nRow(1) = row(1)
                    nRow(2) = row(2)
                    dtTemp.Rows.Add(nRow)
                    dblBenefits += row(1)

                End If
            Next
        End If

        Dim dblTotalDed As Double = 0
        If IntPaid <> 0 Then
            For Each row As Data.DataRow In RowsDet
                If row(1).ToString.Contains("Infinity") Then
                ElseIf row(1).ToString.Contains("لا نهاية") Then
                Else
                    If row(1) > 0 Or row(1).ToString <> "" Then
                        'uwgPayabilities.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {row(0), row(1), 0, row(2)}))
                        dblTotalDed += row(1)
                    End If
                End If
            Next
        End If

        If dblBenefits > dblTotalDed Then
            uwgTransactions.DataSource = dtTemp
            uwgTransactions.DataBind()

            If IntPaid <> 0 Then
                For Each row As Data.DataRow In RowsDet
                    If row(1).ToString.Contains("Infinity") Then
                    ElseIf row(1).ToString.Contains("لا نهاية") Then
                    Else
                        If row(1) > 0 Or row(1).ToString <> "" Then
                            uwgPayabilities.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {row(0), row(1), 0, row(2)}))
                        End If
                    End If
                Next
            End If

            lblTotalBenefit1.Text = Math.Round((dblBenefits * IntPaid), Get_NoDecimalPlaces())
            lblRemainAmount.Text = Math.Round(dblTotalReamin + dblTotalDed, Get_NoDecimalPlaces())
        Else
            'uwgTransactions.DataSource = Nothing
            'uwgTransactions.DataBind()

            uwgTransactions.DataSource = dtTemp
            uwgTransactions.DataBind()

            If IntPaid <> 0 Then
                For Each row As Data.DataRow In RowsDet
                    If row(1).ToString.Contains("Infinity") Then
                    ElseIf row(1).ToString.Contains("لا نهاية") Then
                    Else
                        If row(1) > 0 Or row(1).ToString <> "" Then
                            uwgPayabilities.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {row(0), row(1), 0, row(2)}))
                        End If
                    End If
                Next
            End If

            lblTotalBenefit1.Text = Math.Round((dblBenefits * IntPaid), Get_NoDecimalPlaces())
            lblRemainAmount.Text = Math.Round(dblTotalReamin + dblTotalDed, Get_NoDecimalPlaces())

            'lblTotalBenefit1.Text = 0
            'lblRemainAmount.Text = Math.Round(dblTotalReamin, Get_NoDecimalPlaces())
        End If

        '==========================================================================
        '10- Get all the due Vacation Benefits 
        '==========================================================================
        ClsVacations.Find(" IsAnnual=1 ")
        ClsContracts.ContractValidatoinId(IntEmployeeId, DteEndOfServiceDate)
        dtContractStartDate = ClsContracts.StartDate
        ClsContracts.StartDate = dtContractStartDate

        'Edit By: Hassan Kurdi
        'Edit Date: 2022-01-18
        'Purpose: Use the new method to get annual vacation days for settlement

        'ClsContracts.CheckAnnualVacationDays(ClsContracts.ID, ClsVacations.ID, True, dblVacationDays, VactotalDays, VacunpaidDays, VacNetDays, DteEndOfServiceDate)
        Dim lastDays As Double = ClsContracts.GetAnnualVacationDaysForSettlement(ClsContracts.ID, ClsVacations.ID, True, dblVacationDays, VactotalDays, VacunpaidDays, VacNetDays, 0, DteEndOfServiceDate.AddDays(1))
        'Rabie Calculation Mistake
        'dblVacationDays = ClsContract.GET_EMPLOYEE_VACATION_BALANCE_TO_DATE(ClsEmployees.Code, DteEndOfServiceDate.AddDays(1))
        If lastDays > 0 Then
            ' dblVacationDays = lastDays
        End If
        txtVactionTotalDays.Text = VactotalDays
        txtVactionExceededDays.Text = VacunpaidDays
        txtVactionNetDays.Text = VacNetDays

        DtBenefits = New Data.DataTable
        DtDeductions = New Data.DataTable
        dblBenefits = CIntInitValue
        dblDeduct = CIntInitValue
        CreateDataTable(DtBenefits, "Benefits")
        CreateDataTable(DtDeductions, "Deductions")
        DtBenefits.Clear()
        dblBenefits = 0

        
          dim RemaningOPenBalanceDays As double=0
            Dim Cls_EmployeeVacationOpenBalance As New Clshrs_EmployeeVacationOpenBalance(Me.Page)
            Cls_EmployeeVacationOpenBalance.Find("EmployeeID=" & intEmployeeID & " and VacationTypeID = 1" )
            If Cls_EmployeeVacationOpenBalance.GBalanceDate <> Nothing Then
               dim  str as string = "Select isnull(sum(Amount),0) From hrs_EmployeeVacationOpenBalanceSettlement Where CancelDate Is Null and OpenBalanceID = " & Cls_EmployeeVacationOpenBalance.ID
                Dim PaidAmt As Double = Convert.ToDouble(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContract.ConnectionString, System.Data.CommandType.Text, str))

                str = "Select isnull(sum(PaidDays),0) From hrs_EmployeeVacationOpenBalanceSettlement Where CancelDate Is Null and OpenBalanceID = " & Cls_EmployeeVacationOpenBalance.ID
            Dim PaidDays As Double = Convert.ToDouble(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContract.ConnectionString, System.Data.CommandType.Text, str))

            If PaidDays = 0 Then
                RemaningOPenBalanceDays = Cls_EmployeeVacationOpenBalance.Days - PaidDays
            End If

            'OpenBalanceId.Value =Cls_EmployeeVacationOpenBalance.ID 
        End If
        dblVacationDays = dblVacationDays '+ RemaningOPenBalanceDays
        ClsEmployees.CollectEmployeesTransactions("", ToDate, IntEmployeeId, IntFisicalPeriod, DtBenefits, DtDeductions, dblBenefits, dblDeduct, dblVacationDays, 0, dblVacationDays, 0, 0, 0, 0, 0, 0, Clshrs_EmployeesBase.ePrepareType.EndOfContract, Clshrs_EmployeesBase.ePrepareStage.Vacation)
        lblVacationDue.Text = dblVacationDays

        Dim clsTrans As New Clshrs_TransactionsTypes(Me)

        Dim VRows As Data.DataRow() = DtBenefits.Select("Amount >0 And DescriptionSign='Paid' or Amount >0 And DescriptionSign='Paid By Days'")
        Dim dtVTemp As Data.DataTable = DtBenefits.Clone()
        dblBenefits = 0
        For Each row As Data.DataRow In VRows
            If row(1).ToString.Contains("Infinity") Then
            ElseIf row(1).ToString.Contains("لا نهاية") Then
            Else
                clsTrans.Find("ID=" & row(0))
                If clsTrans.IsEndOfService Or clsTrans.IsBasicSalary = True Then
                    Dim nRow As Data.DataRow = dtVTemp.NewRow()
                    nRow(0) = row(0)
                    nRow(1) = row(1)
                    nRow(2) = row(2)
                    dtVTemp.Rows.Add(nRow)
                    dblBenefits += row(1)
                End If
            End If
        Next

        Dim OpenBalanceAmount As Double = 0
        'Dim Cls_EmployeeVacationOpenBalance As New Clshrs_EmployeeVacationOpenBalance(Me.Page)
        Cls_EmployeeVacationOpenBalance.Find("EmployeeID=" & IntEmployeeId & " and VacationTypeID = " & ClsVacations.ID)
        If Cls_EmployeeVacationOpenBalance.GBalanceDate <> Nothing Then
            Dim StrcMD As String = "Select isnull(sum(Amount),0) From hrs_EmployeeVacationOpenBalanceSettlement Where CancelDate Is Null and OpenBalanceID = " & Cls_EmployeeVacationOpenBalance.ID
            Dim PaidAmt As Double = Convert.ToDouble(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContract.ConnectionString, System.Data.CommandType.Text, StrcMD))
            OpenBalanceAmount = Cls_EmployeeVacationOpenBalance.VacationBalance - PaidAmt
        End If

        If ClsVacations.OBalanceTransactionID <> 0 Or ClsVacations.OBalanceTransactionID <> Nothing Then
            If OpenBalanceAmount <> 0 Then
                Dim nRow As Data.DataRow = dtVTemp.NewRow()
                nRow("TransactionTypeID") = ClsVacations.OBalanceTransactionID
                nRow("Amount") = OpenBalanceAmount
                nRow("Description") = "Paid"
                nRow("DescriptionSign") = "Paid"
                dtVTemp.Rows.Add(nRow)
                dblBenefits = dblBenefits + OpenBalanceAmount
            End If
        End If
        uwgVacationsTransactions.DataSource = dtVTemp
        uwgVacationsTransactions.DataBind()

        lblTotalBenefits2.Text = Math.Round(dblBenefits, Get_NoDecimalPlaces())





        '==========================================================================
        '11- sum of all Benefits 
        '==========================================================================

        lblDescTotalDue.Text = Double.Parse(IIf(IsNumeric(lblTotalBenefits2.Text), lblTotalBenefits2.Text, 0)) _
                             + Double.Parse(IIf(IsNumeric(lblTotalBenefit1.Text), lblTotalBenefit1.Text, 0)) _
                             + Double.Parse(IIf(IsNumeric(lblDescAmount.Text), lblDescAmount.Text, 0)) _
                             - Double.Parse(IIf(IsNumeric(lblRemainAmount.Text), lblRemainAmount.Text, 0))

        lblBenifitsAmount.Text = Double.Parse(IIf(IsNumeric(lblTotalBenefits2.Text), lblTotalBenefits2.Text, 0)) _
                             + Double.Parse(IIf(IsNumeric(lblTotalBenefit1.Text), lblTotalBenefit1.Text, 0)) _
                             + Double.Parse(IIf(IsNumeric(lblDescAmount.Text), lblDescAmount.Text, 0)) _
                             - Double.Parse(IIf(IsNumeric(lblRemainAmount.Text), lblRemainAmount.Text, 0))

        
        
        
        'lblDescTotalDue.Text += Double.Parse(IIf(IsNumeric(txtExtraBenfitsAmount.Text), txtExtraBenfitsAmount.Text, 0)) _
        '                         - Double.Parse(IIf(IsNumeric(txtExtraDudectionAmount.Text), txtExtraDudectionAmount.Text, 0))
    End Sub
    Private Function CreateDataTable(ByVal DtTable As Data.DataTable, ByVal PtrTableName As String) As Boolean
        Dim ObjDataColumn As New Data.DataColumn
        Try
            DtTable.TableName = PtrTableName

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "TransactionTypeID"
            ObjDataColumn.DataType = System.Type.GetType("System.Int32")

            If Not DtTable.Columns.Contains(ObjDataColumn.ColumnName) Then
                DtTable.Columns.Add(ObjDataColumn)
            End If

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "Amount"
            ObjDataColumn.DataType = System.Type.GetType("System.Double")

            If Not DtTable.Columns.Contains(ObjDataColumn.ColumnName) Then
                DtTable.Columns.Add(ObjDataColumn)
            End If

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "Description"
            ObjDataColumn.DataType = System.Type.GetType("System.String")

            If Not DtTable.Columns.Contains(ObjDataColumn.ColumnName) Then
                DtTable.Columns.Add(ObjDataColumn)
            End If

        Catch ex As Exception

        End Try
    End Function
    Private Function CreateBenefitDeductionDataTable(ByVal DtTable As Data.DataTable, ByVal PtrTableName As String) As Boolean
        Dim ObjDataColumn As New Data.DataColumn
        Try
            DtTable.TableName = PtrTableName

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "TransactionTypeID"
            ObjDataColumn.DataType = System.Type.GetType("System.Int32")

            If Not DtTable.Columns.Contains(ObjDataColumn.ColumnName) Then
                DtTable.Columns.Add(ObjDataColumn)
            End If

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "BAmount"
            ObjDataColumn.DataType = System.Type.GetType("System.Double")

            If Not DtTable.Columns.Contains(ObjDataColumn.ColumnName) Then
                DtTable.Columns.Add(ObjDataColumn)
            End If

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "DAmount"
            ObjDataColumn.DataType = System.Type.GetType("System.Double")

            If Not DtTable.Columns.Contains(ObjDataColumn.ColumnName) Then
                DtTable.Columns.Add(ObjDataColumn)
            End If


            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "Description"
            ObjDataColumn.DataType = System.Type.GetType("System.String")

            If Not DtTable.Columns.Contains(ObjDataColumn.ColumnName) Then
                DtTable.Columns.Add(ObjDataColumn)
            End If

        Catch ex As Exception

        End Try
    End Function
    Private Function CheckEmployee(Optional ByVal withMsg As Boolean = False) As Integer
        Dim BolExist As Integer = 0
        Dim ClsEmployees = New Clshrs_Employees(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        If lblDescEmployeeCode.Text <> "" Then
            ClsEmployees.Find("Code='" & lblDescEmployeeCode.Text & "'")
            If (ClsEmployees.ID > 0) Then
                BolExist = ClsEmployees.ID
                ImageButton_Save.Enabled = True
                ImageButton_Print.Enabled = False
                ImageButton2.Enabled = False
                ImageButton_Print0.Enabled = True
            Else
                BolExist = 0
                Clear()
                lblDescEmployeeCode.Focus()
                lblDescEmployeeCode.Text = ""
                lblDescEnglishName.Text = ""
            End If
        Else
            BolExist = 0
        End If
        If (BolExist <= 0) Then
            Clear()
        End If
        Return BolExist
    End Function
    Private Sub Clear()
        lblLoansAmount.Text = ""
        lblRemainAmount.Text = ""
        lblLoansBalance.Text = ""
        lblVacationAmount.Text = ""
        lblWorkingDays.Text = ""
        lblBenifitsAmount.Text = ""
        lblVacationDue.Text = ""
        lblDescworkingDays.Text = ""
        lblDescPaidTotal.Text = ""
        lblDescPaidDays.Text = ""
        lblDescPaidMonth.Text = ""
        lblDescPaidYear.Text = ""
        lblNotPaidTotal.Text = ""
        lblNotPaidDays.Text = ""
        lblNotPaidMonth.Text = ""
        lblDescNotPaidYear.Text = ""
        lblTotalservicedays.Text = ""
        lblserviceYears.Text = ""
        lblservicemonths.Text = ""
        lblservicedays.Text = ""


        lblDescAmount.Text = ""
        lblDescTotalDue.Text = ""





        lblTotalBenefit1.Text = ""
        lblTotalDeduction1.Text = "0"
        lblBenifitsAmount.Text = ""




        lblTotalBenefits2.Text = ""
        lblTotalDeduction2.Text = ""
        lblVacationAmount.Text = ""


        lblLoansAmount.Text = ""
        lblRemainAmount.Text = ""
        lblLoansBalance.Text = ""


        'txtExtraBenfitsAmount.Text = ""
        'txtExtraBenfitsRemarks.Text = ""
        'txtExtraDudectionAmount.Text = ""
        'txtExtraDeductionRemarks.Text = ""


        uwgPayabilities.DataSource = Nothing
        uwgTransactions.DataSource = Nothing
        uwgVacationsTransactions.DataSource = Nothing

        uwgPayabilities.DataBind()
        uwgTransactions.DataBind()
        uwgVacationsTransactions.DataBind()




    End Sub

#End Region

#Region "Shared Function"

    Public Shared Function Find(ByVal Table As String, ByVal Filter As String, ByRef DataSet As DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Dim mSelectCommand = " Select * From " & Table

        Dim mSqlDataAdapter As New SqlClient.SqlDataAdapter
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)

        Try
            'Dim orderByStr As String = ""
            'If Filter.ToLower.IndexOf("order by") = -1 Then
            '    orderByStr = " Order By Code "
            'End If

            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where " & Filter & " And CancelDate IS Null", " Where CancelDate IS Null")

            StrSelectCommand = StrSelectCommand '& orderByStr

            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, ConnStr)
            DataSet = New DataSet
            mSqlDataAdapter.Fill(DataSet)

            If DataSet.Tables(0).Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Shared Function EmpContrcts_ContractValidatoinId(ByVal EmployeeID As Integer) As Integer
        Try
            Dim dsEmp As New Data.DataSet
            Dim DateNow As String

            If Not ClsDataAcessLayer.IsGreg(DateTime.Now.ToString("dd/MM/yyyy")) Then
                DateNow = ClsDataAcessLayer.FormatGregString(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            Else
                DateNow = DateTime.Now.ToString("dd/MM/yyyy")
            End If

            If Find("hrs_Contracts", "Employeeid=" & EmployeeID & "  And CONVERT(Datetime,StartDate,103) <= convert(datetime,'" & DateNow & "',103) And (convert(datetime,enddate,103) is null or convert(datetime,'" & DateNow & "',103) Between StartDate and EndDate)", dsEmp) Then
                Return dsEmp.Tables(0).Rows(0).Item("ID")
            Else
                Return 0
            End If
        Catch ex As Exception

        End Try
    End Function
#End Region

#Region "PageMethods"

    <System.Web.Services.WebMethod()> _
    Public Shared Function CheckEmplyeeContracts(ByVal StrEmployeeCode As String) As String
        'InitializeCulture()
        Dim dsEmployees As New DataSet
        Try

            If Find("hrs_Employees", " Code = '" & StrEmployeeCode & "'", dsEmployees) Then
                If EmpContrcts_ContractValidatoinId(dsEmployees.Tables(0).Rows(0).Item("ID")) > 0 Then
                    Return "1"
                Else
                    Return "0"
                End If
            Else
                Return "2"
            End If
        Catch ex As Exception
            Return "0"
        End Try
        Return "1"
    End Function

#End Region

    Protected Sub BtExcute_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles BtExcute.Click
        CheckCode()
    End Sub
    'Protected Sub txtExtraBenfitsAmount_TextChanged(sender As Object, e As EventArgs) Handles txtExtraBenfitsAmount.TextChanged, txtExtraDudectionAmount.TextChanged


    '    lblDescTotalDue.Text = lblBenifitsAmount.Text + Double.Parse(IIf(IsNumeric(txtExtraBenfitsAmount.Text), txtExtraBenfitsAmount.Text, 0)) _
    '        - Double.Parse(IIf(IsNumeric(txtExtraDudectionAmount.Text), txtExtraDudectionAmount.Text, 0))
    'End Sub
    Protected Sub uwgExtraBenfits_UpdateRow(sender As Object, e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgExtraBenfits.UpdateRow
        If e.Row.DataChanged = DataChanged.Modified Then
            For Each ObjRowDet In uwgExtraBenfits.Rows
                If ObjRowDet.Cells(0).Value = 0 Then
                    ObjRowDet.Delete()
                End If

            Next

            uwgExtraBenfits.Rows.Add()
        End If
    End Sub
    Protected Sub uwgExtraDeduction_UpdateRow(sender As Object, e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgExtraDeduction.UpdateRow
        If e.Row.DataChanged = DataChanged.Modified Then


            uwgExtraDeduction.Rows.Add()
        End If
    End Sub
End Class
