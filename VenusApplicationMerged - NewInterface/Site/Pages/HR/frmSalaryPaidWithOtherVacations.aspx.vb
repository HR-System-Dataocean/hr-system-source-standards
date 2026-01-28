Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports Venus.Application.SystemFiles.System.ClsDataAcessLayer

Partial Class frmEmployeesVacationTransactions
    Inherits MainPage

#Region "Protected Methods"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ClsEmployees As New Clshrs_Employees(Me.Page)
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
        Dim DteVacationDate As Date = Date.Now
        Dim IntEmployeeId = 0
        Dim Dys = 0
        If Request.QueryString.Item("ID") <> Nothing Then
            IntEmployeeId = IIf(Request.QueryString.Item("ID") = "", 0, Request.QueryString.Item("ID"))
        End If
        If Request.QueryString.Item("EmpCode") <> Nothing Then
            ClsEmployees = New Clshrs_Employees(Me.Page)
            ClsEmployees.Find1("Code = '" & Request.QueryString.Item("EmpCode") & "'")
            IntEmployeeId = ClsEmployees.ID
        End If
        Dim ClsTransTypes As New Clshrs_TransactionsTypes(Page)
        Dim ClsEmpVacation As New Clshrs_EmployeesVacations(Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

        If Not IsPostBack Then
            Page.Session.Add("Log", objNav.SetLanguage(Page, "Eng/Arb"))
            Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
            Call ClsTransTypes.GetList(uwgEmployeeTransaction.Columns.FromKey("TransactionTypeID").ValueList)
            Call ClsTransTypes.GetList(uwgPayabilities.Columns.FromKey("TransactionTypeID").ValueList)

            Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(Page)


            Dim intContractId As Integer = CheckEmployee1(IntEmployeeId, Date.Now)
            If IntEmployeeId = 0 Then
                DteVacationDate = Date.Now.ToString("dd/MM/yyyy")
            Else

                If Request.QueryString.Item("Fisical") <> "" Then
                    DteVacationDate = Request.QueryString.Item("Fisical")
                Else
                    Dim str As String = "Select Top 1 ID From hrs_VacationsTypes Where IsAnnual= 1 And CancelDate Is Null"
                    Dim VacTypeID As Integer = Convert.ToInt32(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, System.Data.CommandType.Text, str))
                    Dim resetDate As Date? = Nothing
                    Dim Cls_EmployeeVacationOpenBalance As New Clshrs_EmployeeVacationOpenBalance(Me.Page)
                    Cls_EmployeeVacationOpenBalance.Find("EmployeeID=" & IntEmployeeId & " and VacationTypeID = " & VacTypeID)
                    If Cls_EmployeeVacationOpenBalance.GBalanceDate <> Nothing Then
                        resetDate = Cls_EmployeeVacationOpenBalance.GBalanceDate
                    End If
                    DteVacationDate = ClsEmpVacation.GetDefaultVacationPayment(IntEmployeeId, intContractId, resetDate, False)
                End If
            End If
            CheckBox_SalaryPayment.Visible = False
            If Request.QueryString.Item("TrnsID") <> 0 Then
                CheckBox_SalaryPayment.Enabled = True
                CheckBox_SalaryPayment.Checked = True
                Dim fiscID As Integer = 0
                Dim fiscfrom As DateTime
                Dim fiscto As DateTime
                ClsFisicalYearsPeriods.GetFisicalperiodInfo(Request.QueryString.Item("Fisical"), fiscID, fiscfrom, fiscto)

            Else

                CheckBox_SalaryPayment.Enabled = False
                CheckBox_SalaryPayment.Checked = False


            End If

            If Request.QueryString("TrnsID") Then
                Dim IntVacID As Integer = Request.QueryString("TrnsID")
                ClsEmpVacation.Find("ID= " & IntVacID)
                SetData(IntEmployeeId, ClsEmpVacation.PaymentTrnsID)
            Else
                SetData(IntEmployeeId)
            End If



        End If
    End Sub
    Private Function GetModuleID(ByVal TableName As String) As Integer
        Dim ClsForms As New ClsSys_Forms(Me.Page)
        Dim IntModuleID As Integer
        ClsForms.Find(" Code = '" & TableName & "'")
        If ClsForms.ID > 0 Then
            IntModuleID = ClsForms.ModuleID
        End If
        Return IntModuleID
    End Function
    Protected Sub txtCode_TextChanged()
        Dim ClsEmployees As New Clshrs_Employees(Me.Page)
        Dim ClsEmpVacation As New Clshrs_EmployeesVacations(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        If txtCode.Text.Trim <> String.Empty Then
            If ClsEmployees.Find("Code='" & txtCode.Text.Trim & "'") Then
                Dim intContractId As Integer = CheckEmployee1(ClsEmployees.ID, Date.Now)
                Dim str As String = "Select Top 1 ID From hrs_VacationsTypes Where IsAnnual= 1 And CancelDate Is Null"
                Dim VacTypeID As Integer = Convert.ToInt32(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, System.Data.CommandType.Text, str))

                Dim resetDate As Date? = Nothing
                Dim Cls_EmployeeVacationOpenBalance As New Clshrs_EmployeeVacationOpenBalance(Me.Page)
                Cls_EmployeeVacationOpenBalance.Find("EmployeeID=" & ClsEmployees.ID & " and VacationTypeID = " & VacTypeID)
                If Cls_EmployeeVacationOpenBalance.GBalanceDate <> Nothing Then
                    resetDate = Cls_EmployeeVacationOpenBalance.GBalanceDate
                End If


                SetData(ClsEmployees.ID)
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This Employee Not found / هذا الموظف غير موجود"))
                Clear(True)
            End If
        Else
            Clear(True)
        End If
    End Sub


    Protected Sub uwgVacationHistory_SelectedRowsChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs) Handles uwgVacationHistory.SelectedRowsChange
        Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Page)
        If (Not IsNothing(e.SelectedRows(0).Cells.FromKey("ID")) AndAlso Val(e.SelectedRows(0).Cells.FromKey("ID").Value) > 0) Then
            If ClsEmployeesTransactions.Find("ID=" & Val(e.SelectedRows(0).Cells.FromKey("ID").Value)) Then

                SetData(ClsEmployeesTransactions.EmployeeID, ClsEmployeesTransactions.ID)
                hdnEmpTrans.Value = ClsEmployeesTransactions.ID
                btnSave.Enabled = False
                ClsEmployeesTransactions = New Clshrs_EmployeesTransactions(Page)


            End If
        End If
    End Sub
    Protected Sub uwgEmployeeTransaction_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgEmployeeTransaction.InitializeRow
        Dim clsTransType As New Clshrs_TransactionsTypes(Page)
        'If clsTransType.Find("ID=" & e.Row.Cells(0).Value) Then
        '    If clsTransType.Formula.Trim <> String.Empty Or clsTransType.BeginContractFormula.Trim <> String.Empty Or clsTransType.EndContractFormula.Trim <> String.Empty Then
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
        'If clsTransType.Find("ID=" & e.Row.Cells(0).Value) Then
        '    If clsTransType.Formula.Trim <> String.Empty Or clsTransType.BeginContractFormula.Trim <> String.Empty Or clsTransType.EndContractFormula.Trim <> String.Empty Then
        '        e.Row.Cells(1).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
        '    End If
        'End If
        If clsTransType.Find("ID=" & e.Row.Cells(0).Value) Then
            'If e.Row.Cells(5).Value = "By Project" Or clsTransType.InputIsNumeric = False Or clsTransType.Formula.Trim <> String.Empty Or clsTransType.BeginContractFormula.Trim <> String.Empty Or clsTransType.EndContractFormula.Trim <> String.Empty Then
            If clsTransType.InputIsNumeric = False Or clsTransType.Formula.Trim <> String.Empty Or clsTransType.BeginContractFormula.Trim <> String.Empty Or clsTransType.EndContractFormula.Trim <> String.Empty Then
                e.Row.Cells(1).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            End If
        End If
    End Sub
    Protected Sub uwgEmployeeTransaction_UpdateCell(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles uwgEmployeeTransaction.UpdateCell
        If CheckBox_SalaryPayment.Checked Then
            Dim TotVal As Double = 0
            For Each ObjRowDet As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeeTransaction.Rows
                If ObjRowDet.Cells.FromKey("Description").Value <> "Vac" And (ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid" Or ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid By Days") Then
                    If Not ObjRowDet.Cells(1).Value Is DBNull.Value AndAlso Val(ObjRowDet.Cells(1).Value) > 0 Then
                        TotVal += ObjRowDet.Cells(1).Value
                    End If
                End If
            Next
            Dim SumAdd As Double = TotVal

            TotVal = 0
            For Each ObjRowDet In uwgPayabilities.Rows
                If ObjRowDet.Cells.FromKey("Description").Value <> "Vac" And (ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid" Or ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid By Days") Then
                    If Not ObjRowDet.Cells(1).Value Is DBNull.Value AndAlso Val(ObjRowDet.Cells(1).Value) > 0 Then
                        TotVal += ObjRowDet.Cells(1).Value
                    End If
                End If
            Next
            Dim SumDed As Double = TotVal

            TextBox_SalaryAmount.Text = Math.Round((SumAdd - SumDed), 2)
        Else
            Dim TotVal As Double = 0
            For Each ObjRowDet As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeeTransaction.Rows
                If (ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid" Or ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid By Days") Then
                    If Not ObjRowDet.Cells(1).Value Is DBNull.Value AndAlso Val(ObjRowDet.Cells(1).Value) > 0 Then
                        TotVal += ObjRowDet.Cells(1).Value
                    End If
                End If
            Next
            Dim SumAdd As Double = TotVal

            TotVal = 0
            For Each ObjRowDet In uwgPayabilities.Rows
                If (ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid" Or ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid By Days") Then
                    If Not ObjRowDet.Cells(1).Value Is DBNull.Value AndAlso Val(ObjRowDet.Cells(1).Value) > 0 Then
                        TotVal += ObjRowDet.Cells(1).Value
                    End If
                End If
            Next
            Dim SumDed As Double = TotVal

        End If
    End Sub
    Protected Sub uwgPayabilities_UpdateCell(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles uwgPayabilities.UpdateCell
        If CheckBox_SalaryPayment.Checked Then
            Dim TotVal As Double = 0
            For Each ObjRowDet As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeeTransaction.Rows
                If ObjRowDet.Cells.FromKey("Description").Value <> "Vac" And (ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid" Or ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid By Days") Then
                    If Not ObjRowDet.Cells(1).Value Is DBNull.Value AndAlso Val(ObjRowDet.Cells(1).Value) > 0 Then
                        TotVal += ObjRowDet.Cells(1).Value
                    End If
                End If
            Next
            Dim SumAdd As Double = TotVal

            TotVal = 0
            For Each ObjRowDet In uwgPayabilities.Rows
                If ObjRowDet.Cells.FromKey("Description").Value <> "Vac" And (ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid" Or ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid By Days") Then
                    If Not ObjRowDet.Cells(1).Value Is DBNull.Value AndAlso Val(ObjRowDet.Cells(1).Value) > 0 Then
                        TotVal += ObjRowDet.Cells(1).Value
                    End If
                End If
            Next
            Dim SumDed As Double = TotVal

            TextBox_SalaryAmount.Text = Math.Round((SumAdd - SumDed), 2)
        Else
            Dim TotVal As Double = 0
            For Each ObjRowDet As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeeTransaction.Rows
                If (ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid" Or ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid By Days") Then
                    If Not ObjRowDet.Cells(1).Value Is DBNull.Value AndAlso Val(ObjRowDet.Cells(1).Value) > 0 Then
                        TotVal += ObjRowDet.Cells(1).Value
                    End If
                End If
            Next
            Dim SumAdd As Double = TotVal

            TotVal = 0
            For Each ObjRowDet In uwgPayabilities.Rows
                If (ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid" Or ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid By Days") Then
                    If Not ObjRowDet.Cells(1).Value Is DBNull.Value AndAlso Val(ObjRowDet.Cells(1).Value) > 0 Then
                        TotVal += ObjRowDet.Cells(1).Value
                    End If
                End If
            Next
            Dim SumDed As Double = TotVal

        End If
    End Sub

    Protected Sub btnSave_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles btnSave.Command, btnDelete.Command
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim clsVacType As New Clshrs_VacationsTypes(Page)
        Dim ClsEmpPaySchSettlments As New Clshrs_EmployeesPayabilitySchedulesSettlement(Page)
        Dim ClsEmployeeVacation As New Clshrs_EmployeesVacations(Page)
        Dim IntEmployeeTransactionID As Integer
        Dim ClsEmployeesTransactionsProjects As New Clshrs_EmployeesTransactionsProjects(Page)
        Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)
        Dim ClsEmployeeTransactionsDet As New Clshrs_EmployeesTransactionsDetails(Me.Page)
        Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim ClsEmployeesTrancationsProjects As New Clshrs_EmployeesTransactionsProjects(Page)
        Dim ClsEmpTransDet As New Clshrs_EmployeesTransactionsDetails(Page)
        Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(Page)
        Dim IntEmployeeID As Integer = txtEmployeeId.Value
        Dim DteFromDate As Date = Nothing
        Dim DteToDate As Date = Nothing
        Dim IntPeriodId As Integer = 0
        'If DdlPeriodsForSalary.SelectedValue > 0 Then
        ' ClsFisicalPeriods.Find("ID=" & DdlPeriodsForSalary.SelectedValue)
        IntPeriodId = ClsFisicalPeriods.ID
            DteFromDate = ClsFisicalPeriods.FromDate
            DteToDate = ClsFisicalPeriods.ToDate
        ' Else
        Dim DteVacationDate2 As Date = Nothing
        DteVacationDate2 = Request.QueryString.Item("Fisical")
        ClsFisicalPeriods.GetFisicalperiodInfo(DteVacationDate2, IntPeriodId, DteFromDate, DteToDate)
        '  End If






        Select Case e.CommandName
            Case "Save"
                Dim intContractId As Integer = CheckEmployee(IntEmployeeID, ClsFisicalPeriods.ID)
                Dim clscontracts As New Clshrs_Contracts(Me.Page)
                Dim clsemployeeclass As New Clshrs_EmployeeClasses(Me.Page)
                ClsEmployees = New Clshrs_Employees(Me.Page)
                clscontracts.Find("ID = " & intContractId)
                clsemployeeclass.Find("ID =" & clscontracts.EmployeeClassID)
                ClsEmployees.Find("ID= " & IntEmployeeID)
                Dim CurrrentSalTransaction As Integer = 0
                Dim IntProjectHeadID As Integer = 0
                If CheckBox_SalaryPayment.Checked Then

                    If ClsEmployeesTransactions.Find("EmployeeID=" & IntEmployeeID & " And FiscalYearPeriodID=" & IntFisicalPeriod & " And PrepareType ='NV'") Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Done !/!لا يمكن دفع مستحقات الراتب مرتين"))
                        Exit Sub

                    End If
                    If Val(lblWorkingDays.Text) > 0 Or uwgEmployeeTransaction.Rows.Count > 0 Or uwgPayabilities.Rows.Count > 0 Then
                        ClsEmployeesTransactions = New Clshrs_EmployeesTransactions(Me.Page)
                        With ClsEmployeesTransactions
                            .EmployeeID = IntEmployeeID
                            .FiscalYearPeriodID = IntPeriodId
                            .PaidDate = Date.Now
                            .FinancialWorkingUnits = Val(lblWorkingDays.Text)
                            .PrepareType = "NV"
                            .CBranchID = ClsEmployees.BranchID
                            .CDepartmetnID = ClsEmployees.DepartmentID
                            .Applyed = True
                            CurrrentSalTransaction = .Save()
                        End With

                        ClsEmployeesTrancationsProjects = New Clshrs_EmployeesTransactionsProjects(Page)
                        With ClsEmployeesTrancationsProjects
                            .EmployeeTransactionID = CurrrentSalTransaction
                            .ProjectID = clsemployeeclass.DefaultProjectID
                            .WorkingDate = DateTime.Now
                            .WorkingUnits = Val(lblWorkingDays.Text)
                            IntProjectHeadID = .Save1()
                        End With
                        For Each ObjTransaction In uwgEmployeeTransaction.Rows
                            If ObjTransaction.Cells.FromKey("Description").Value <> "Vac" Then
                                ClsEmpTransDet = New Clshrs_EmployeesTransactionsDetails(Page)
                                With ClsEmpTransDet
                                    .EmpTransProjID = IntProjectHeadID
                                    .NumericValue = ObjTransaction.Cells(1).Value
                                    .TextValue = ObjTransaction.Cells(2).Value
                                    .TransactionTypeID = ObjTransaction.Cells(0).Value
                                    .Save()
                                End With
                            End If
                        Next

                        If CurrrentSalTransaction > 0 Then
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeesTransactions.ConnectionString, Data.CommandType.Text, "Update hrs_EmployeesVacations set PaymentTrnID =" & CurrrentSalTransaction & " where id=" & Convert.ToInt32(Request.QueryString.Item("TrnsID")))
                        End If

                        For Each ObjLoans In uwgPayabilities.Rows
                            If ObjLoans.Cells.FromKey("Description").Value <> "Vac" Then
                                ClsEmpTransDet = New Clshrs_EmployeesTransactionsDetails(Page)
                                With ClsEmpTransDet
                                    .EmpTransProjID = IntProjectHeadID
                                    .NumericValue = ObjLoans.Cells(1).Value
                                    .TextValue = ObjLoans.Cells(2).Value
                                    .TransactionTypeID = ObjLoans.Cells(0).Value


                                    Dim n As Integer = .Save()

                                    If ObjLoans.Cells.FromKey("EmpSchID").Value > 0 Then
                                        Dim ClsEmployeesPayabilityScheduleSttlments As New Clshrs_EmployeesPayabilitySchedulesSettlement(Me.Page)
                                        With ClsEmployeesPayabilityScheduleSttlments
                                            .Amount = ObjLoans.Cells(1).Value
                                            .DDate = DteToDate
                                            .EmployeeTransactionID = IntProjectHeadID
                                            .EmployeePayabilityScheduleID = ObjLoans.Cells.FromKey("EmpSchID").Value
                                            .Save()
                                        End With
                                    End If
                                End With
                            End If
                        Next
                    End If
                End If





                ClsTransactionTypes = New Clshrs_TransactionsTypes(Page)
                    ClsTransactionTypes.Find("Code='24'")

                    ClsEmpTransDet = New Clshrs_EmployeesTransactionsDetails(Page)

                With ClsEmpTransDet
                    .EmpTransProjID = IntProjectHeadID
                    If txtExtraDudectionAmount.Text <> "" Then
                        .NumericValue = Convert.ToDouble(txtExtraDudectionAmount.Text.Trim())
                        .TextValue = IIf(txtExtraDeductionRemarks.Text.Trim() = "", "تسوية خصومات", txtExtraDeductionRemarks.Text.Trim())
                        .TransactionTypeID = ClsTransactionTypes.ID
                        Dim n As Integer = .Save()
                    End If

                    ' .NumericValue = IIf(txtExtraDudectionAmount.Text.Trim() = "", 0, Convert.ToDouble(txtExtraDudectionAmount.Text.Trim()))

                End With
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">OpenPrintedScreen(" & IntEmployeeTransactionID & ");</script>")
                Clear()

            Case "Refund"
                Dim intTransID = Request.QueryString.Item("PaymentTrnsID")
                If intTransID > 0 Then

                    If CInt(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployeesTransactions.ConnectionString, Data.CommandType.Text, "select isnull(count(ID),0) from hrs_EmployeesTransactions where PrepareType='V' and PostDate is not null and ID = " & intTransID)) > 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Unable to delete this transaction, The Transaction Locked / لا يمكن حذف هذه الحركة لانهاء مغلقة"))
                        Exit Sub
                    End If
                    If CInt(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployeesTransactions.ConnectionString, Data.CommandType.Text, "select isnull(count(ID),0) from hrs_EmployeesTransactions where PrepareType='N' and PostDate is not null and ID in (select RegComputerID from hrs_EmployeesTransactions where PrepareType='V' and ID = " & intTransID & ")")) > 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Unable to delete this transaction, The Related Prepared Salary Locked / لا يمكن حذف هذه الحركة لانهاء حركة الرواتب المرتبطة مغلقة"))
                        Exit Sub
                    End If
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeesTransactions.ConnectionString, Data.CommandType.Text, "delete from hrs_EmployeesTransactions where ID in (select Mstr.RegComputerID from hrs_EmployeesTransactions Mstr where Mstr.ID = " & intTransID & ");  delete from hrs_EmployeeExtraItems where Src = 5 and TransactionNo = " & intTransID)
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeesTransactions.ConnectionString, Data.CommandType.Text, "update hrs_EmployeesVacations set PaymentTrnID =Null,PaidFromBalance=0,RemainingBalance=0 where PaymentTrnID = " & intTransID)
                    ClsEmployeesTransactions.DeleteAll("ID=" & intTransID)
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Done / تم حذف المستحقات"))
                    txtCode_TextChanged()


                End If
        End Select
    End Sub

#End Region

#Region "Private Methods"
    Private Function SaveDetails(ByVal TransactionHeadID As Integer, ByVal EmployeeID As Integer, ByVal FiscalPeriodID As Integer) As Boolean
        Dim ObjRowDet As New Infragistics.WebUI.UltraWebGrid.UltraGridRow
        Dim ClsEmployeesPayabilityScheduleSttlments As New Clshrs_EmployeesPayabilitySchedulesSettlement(Me.Page)
        Dim ObjErrorHandler As New Venus.Shared.Errors.ErrorsHandler(ClsEmployeesPayabilityScheduleSttlments.ConnectionString)
        Dim ClsEmployeeTransactionsDet As New Clshrs_EmployeesTransactionsDetails(Page)
        Dim ClsEmployeesTransactionsProjects As New Clshrs_EmployeesTransactionsProjects(Page)
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ClsEmployeeesPayablitySetelment As New Clshrs_EmployeesPayabilitySchedulesSettlement(Page)
        Dim IntTransactionDetailID As Integer = 0
        Dim ClsProjects As New Clshrs_Projects(Page, "hrs_projects")
        Dim intprojectID As Integer
        Try
            ClsEmployeeesPayablitySetelment.Delete(TransactionHeadID)
            ClsEmployeeTransactionsDet.DeleteAll("EmployeeTransactionID=" & TransactionHeadID)
            ClsEmployeesTransactionsProjects.DeleteAll("EmployeeTransactionID=" & TransactionHeadID)
            ClsEmployees.DeleteEmployeesPenalties(TransactionHeadID)

            Dim StrSaveCommand As String = "Declare @ProjectTransID Int; "
            If ClsProjects.Find(" CancelDate Is Null") Then
                intprojectID = ClsProjects.ID
            Else
                Exit Function
            End If
            StrSaveCommand &= " Insert Into hrs_EmployeesTransactionsProjects([EmployeeTransactionID],[ProjectID],[WorkingUnits],[RegUserID]) " & _
                                          " Select " & _
                                          TransactionHeadID & _
                                          "," & intprojectID & _
                                          "," & Convert.ToDouble(lblWorkingDays.Text) & _
                                          "," & ClsEmployees.DataBaseUserRelatedID & ";" & "  Select IDENT_CURRENT('hrs_EmployeesTransactionsProjects'); "
            Dim intProjTrans As Integer = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, StrSaveCommand)
            For Each ObjRowDet In uwgEmployeeTransaction.Rows
                If ObjRowDet.Cells.FromKey("Description").Value = "Vac" And (ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid" Or ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid By Days") Then
                    ClsEmployeeTransactionsDet = New Clshrs_EmployeesTransactionsDetails(Page)
                    ClsEmployeeTransactionsDet.TransactionTypeID = ObjRowDet.Cells(0).Value
                    ClsEmployeeTransactionsDet.EmpTransProjID = intProjTrans
                    ClsEmployeeTransactionsDet.TextValue = ObjRowDet.Cells(2).Value
                    If Not ObjRowDet.Cells(1).Value Is DBNull.Value AndAlso Val(ObjRowDet.Cells(1).Value) > 0 Then
                        ClsEmployeeTransactionsDet.NumericValue = ObjRowDet.Cells(1).Value
                        ClsEmployeeTransactionsDet.Save()
                        If ObjRowDet.Cells(4).Value > 0 Then
                            Dim Comstring = "insert into hrs_EmployeeVacationOpenBalanceSettlement (OpenBalanceID,EmployeeTransactionID,Amount,Date,RegDate) values (" & ObjRowDet.Cells(4).Value & "," & intProjTrans & "," & ObjRowDet.Cells(1).Value & ",getdate(),getdate())"
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, Comstring)
                        End If
                    End If
                End If
            Next

            If CheckBox_SalaryPayment.Checked = False Then
                For Each ObjLoans In uwgPayabilities.Rows
                    If ObjLoans.Cells.FromKey("Description").Value = "Vac" And ObjLoans.Cells.FromKey("EmpSchID").Value <= 0 Then
                        Dim ClsEmpTransDet As New Clshrs_EmployeesTransactionsDetails(Page)
                        With ClsEmpTransDet
                            .EmpTransProjID = intProjTrans
                            .NumericValue = ObjLoans.Cells(1).Value
                            .TextValue = ObjLoans.Cells(2).Value
                            .TransactionTypeID = ObjLoans.Cells(0).Value
                            Dim n As Integer = .Save()
                        End With
                    End If
                Next
            End If







            If CheckBox_SalaryPayment.Checked = True Then
                For Each ObjRowDet In uwgPayabilities.Rows
                    If ObjRowDet.Cells.FromKey("Description").Value = "Vac" And (ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid" Or ObjRowDet.Cells.FromKey("DescriptionSign").Value = "Paid By Days") Then
                        ClsEmployeeTransactionsDet = New Clshrs_EmployeesTransactionsDetails(Page)
                        ClsEmployeeTransactionsDet.TransactionTypeID = ObjRowDet.Cells(0).Value
                        ClsEmployeeTransactionsDet.EmpTransProjID = intProjTrans
                        ClsEmployeeTransactionsDet.TextValue = ObjRowDet.Cells(2).Value
                        If Not ObjRowDet.Cells(1).Value Is DBNull.Value AndAlso Val(ObjRowDet.Cells(1).Value) > 0 Then
                            ClsEmployeeTransactionsDet.NumericValue = ObjRowDet.Cells(1).Value
                            IntTransactionDetailID = ClsEmployeeTransactionsDet.Save()
                            If ObjRowDet.Cells.FromKey("EmpSchID").Value > 0 Then
                                ClsEmployeesPayabilityScheduleSttlments = New Clshrs_EmployeesPayabilitySchedulesSettlement(Me.Page)
                                With ClsEmployeesPayabilityScheduleSttlments
                                    .Amount = ObjRowDet.Cells(1).Value
                                    .DDate = Now.Date
                                    .EmployeeTransactionID = intProjTrans
                                    .EmployeePayabilityScheduleID = ObjRowDet.Cells.FromKey("EmpSchID").Value
                                    .Save()
                                End With
                            End If
                        End If
                    End If
                Next
            End If







        Catch ex As Exception
            Page.Session.Add("ErrorValue", ex)
            ObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesPayabilityScheduleSttlments.DataBaseUserRelatedID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
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

    Dim ClsFisicalPeriod As Clssys_FiscalYearsPeriods
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

            ClsFisicalPeriod.GetFisicalperiodInfo(ClsEmployees.SetHigriDate(Request.QueryString.Item("Fisical")), IntFisicalPeriod, FromDate, ToDate)

            ClsEmployees.Find("Code = '" & txtCode.Text & "'")
            clsCompanies.Find("ID = " & clsBranch.MainCompanyID)
            clsBranch.Find("ID=" & ClsEmployees.BranchID)
            Dim fiscalperiood As New Clssys_FiscalYearsPeriods(Me)
            fiscalperiood.Find("ID = " & IntFisicalPeriod)
            FromDate = fiscalperiood.FromDate
            ToDate = fiscalperiood.ToDate
            If clsBranch.PrepareDay > 0 Then
                If ClsEmployees.SetHigriDate(Request.QueryString.Item("Fisical")) >= New DateTime(fiscalperiood.FromDate.Year, fiscalperiood.FromDate.Month, clsBranch.PrepareDay) Then
                    'IntFisicalPeriod = IntFisicalPeriod + 1
                End If
                fiscalperiood.Find("ID = " & IntFisicalPeriod)
                FromDate = fiscalperiood.FromDate
                ToDate = fiscalperiood.ToDate
                FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsBranch.PrepareDay)
                ToDate = FromDate.AddMonths(1).AddDays(-1)
            Else
                If clsCompanies.PrepareDay > 0 Then
                    If ClsEmployees.SetHigriDate(Request.QueryString.Item("Fisical")) >= New DateTime(fiscalperiood.FromDate.Year, fiscalperiood.FromDate.Month, clsCompanies.PrepareDay) Then
                        '  IntFisicalPeriod = IntFisicalPeriod + 1
                    End If
                    fiscalperiood.Find("ID = " & IntFisicalPeriod)
                    FromDate = fiscalperiood.FromDate
                    ToDate = fiscalperiood.ToDate
                    FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsCompanies.PrepareDay)
                    ToDate = FromDate.AddMonths(1).AddDays(-1)
                End If
            End If

            ClsFisicalPeriods.Find("ID = " & IntFisicalPeriod)
            clsCompanies.Find("ID=" & ClsFisicalPeriods.MainCompanyID)
            ClsEmployees.Find("Code = '" & txtCode.Text & "'")
            clsBranch.Find("ID=" & ClsEmployees.BranchID)
            Get_FromToDate()

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
    Private Function Get_FromToDate() As Boolean
        Try
            FromDate = ClsFisicalPeriods.FromDate
            ToDate = IIf(ClsFisicalPeriods.ToDate > ClsEmployees.SetHigriDate(Request.QueryString.Item("Fisical")), ClsEmployees.SetHigriDate(Request.QueryString.Item("Fisical")), ClsFisicalPeriods.ToDate)
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

            If (clsEmployeevacations.FindEmployeeVacations(" hrs_EmployeesVacations.VacationTypeID in (select ID from hrs_VacationsTypes where CancelDate is null and IsAnnual <> 1 and HasPayment=1) and hrs_EmployeesVacations.EmployeeID=" & ClsEmployees.ID & " And Convert(smalldatetime,Convert(varchar,ActualStartDate ,103)) <= Convert(smalldatetime,Convert(varchar,'" & FiscFromDate & "' ,103))	And	(ActualEndDate Is Null Or  Convert(smalldatetime,Convert(varchar,ActualEndDate ,103)) > Convert(smalldatetime,Convert(varchar,'" & FiscToDate & "',103)))")) Then
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
            ObjPrepaerdData = ClsEmployees.GetPreparedEmployessForSalariesByEmployeeID(IntFisicalPeriod, EmpID, FromDate.ToString("dd/MM/yyyy"), ToDate.ToString("dd/MM/yyyy"), FiscFromDate.ToString("dd/MM/yyyy"), FiscToDate.ToString("dd/MM/yyyy"))

            Dim Amount As Double = 0
            Calculate_SalaryPerHour(Amount, EmpID, ClsEmployees.SetHigriDate(Request.QueryString.Item("Fisical")))

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
    Private ClsEmployeesTrancations As Clshrs_EmployeesTransactions
    Private Function Get_Attendance(ByVal EmpID As Integer, Optional ByVal IsNew As Boolean = True) As Boolean
       

        Try


            Dim intAbsent As Integer = 0
            Dim Total_OT As Double = 0
            Dim Total_HD As Double = 0
            Dim Total_Absent As Double = 0
            ObjTotalLate = 0
            NotPermitLat = 0
            Dim fromdate2 As DateTime
            Dim ToDate2 As DateTime
            Dim StartFiscal As DateTime
            Dim EndFiscal As DateTime
            Dim ClsFisicalPeriod2 As New Clssys_FiscalYearsPeriods(Page)
            Dim IntFisicalPeriod2 As Integer
            Dim DteVacationDate2 As Date = Nothing


            If Request.QueryString.Item("Fisical") <> "" Then
                DteVacationDate2 = Request.QueryString.Item("Fisical")
                ClsFisicalPeriod2.GetFisicalperiodInfo(ClsEmployees.SetHigriDate(DteVacationDate2), IntFisicalPeriod2, StartFiscal, EndFiscal)
            End If
            If clsBranch.PrepareDay > 0 Then
               
                ClsFisicalPeriod2.Find("ID = " & IntFisicalPeriod2)
                fromdate2 = ClsFisicalPeriod2.FromDate
                ToDate2 = ClsFisicalPeriod2.ToDate
                fromdate2 = New DateTime(IIf(fromdate2.Month = 1, fromdate2.Year - 1, fromdate2.Year), fromdate2.AddMonths(-1).Month, clsBranch.PrepareDay)
                ToDate2 = fromdate2.AddMonths(1).AddDays(-1)
            Else
                If clsCompanies.PrepareDay > 0 Then
                    ClsFisicalPeriod2.Find("ID = " & IntFisicalPeriod2)
                    fromdate2 = ClsFisicalPeriod2.FromDate
                    ToDate2 = ClsFisicalPeriod2.ToDate
                    fromdate2 = New DateTime(IIf(fromdate2.Month = 1, fromdate2.Year - 1, fromdate2.Year), fromdate2.AddMonths(-1).Month, clsCompanies.PrepareDay)
                    ToDate2 = fromdate2.AddMonths(1).AddDays(-1)
                End If

            End If
            If clsCompanies.PrepareDay > 0 = False And clsBranch.PrepareDay = False Then
                fromdate2 = ClsFisicalPeriod2.FromDate
                ToDate2 = ClsFisicalPeriod2.ToDate
            End If



           


            Dim VacDays As Integer = 0

            Dim dsAttandance As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, "set dateformat dmy  select * from dbo.AttendanceEffects(" & ClsEmployees.ID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & DteVacationDate2.ToString("dd/MM/yyy") & "'," & IntFisicalPeriod & ",1,'" & DteVacationDate2 & "',0 ,'" & FiscFromDate.ToString("dd/MM/yyy") & "','" & FiscToDate.ToString("dd/MM/yyy") & "' )")
            'Dim dsAttandance As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, "set dateformat dmy  select * from dbo.AttendanceEffects(" & ClsEmployees.ID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & FiscToDate.ToString("dd/MM/yyy") & "'," & IntFisicalPeriod & ",0,NULL,0 ,'" & FiscFromDate.ToString("dd/MM/yyy") & "','" & FiscToDate.ToString("dd/MM/yyy") & "' )")
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
    Private Sub SetData(Optional ByVal EmployeeID As Integer = 0, Optional ByVal intEmpTransID As Integer = 0)
        Clear()
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ClsEmpVacation As New Clshrs_EmployeesVacations(Page)
        Dim intEmployeeID As Integer = 0
        Dim ClsContract As New Clshrs_Contracts(Page)
        Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
        Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Page)
        Dim ClsEmployeesTransactionsDet As New Clshrs_EmployeesTransactionsDetails(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim mErrorHandler As New Venus.Shared.ErrorsHandler(ClsEmployees.ConnectionString)
        Dim DtBenefits As New Data.DataTable
        Dim DtDeductions As New Data.DataTable
        Dim dblBenefits As Double = 0
        Dim dblDeduct As Double = 0
        Dim intContractId As Integer = 0
        Dim DteFromDate As Date = Nothing
        Dim DteToDate As Date = Nothing
        Dim IntPeriodId As Integer = 0
        Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)
        Dim clsEmpClass As New Clshrs_EmployeeClasses(Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(clsEmpClass.ConnectionString)
        Dim totalDays As Single = 0

        Dim totalBenfits As Single = 0
        Dim totalDeducation As Single = 0

        Dim totalBenfitsSal As Single = 0
        Dim totalDeducationSal As Single = 0

        ClsEmployeesTrancations = New Clshrs_EmployeesTransactions(Page)
        ClsFisicalPeriod = New Clssys_FiscalYearsPeriods(Page)

        Load_ClsLayers()
        If Not SetScreenSetting(EmployeeID) Then Return
        New_Value(True)
        If Not Get_Attendance(EmployeeID) Then Return




        If EmployeeID = 0 Then
            If txtCode.Text.Trim <> "" Then
                If ClsEmployees.Find("Code ='" & txtCode.Text & "'") Then
                    intEmployeeID = ClsEmployees.ID
                End If
            End If
        Else
            intEmployeeID = EmployeeID
            ClsEmployees.Find("ID ='" & intEmployeeID & "'")
        End If
        txtEmployeeId.Value = intEmployeeID
        lblEmployeeName.Text = ClsEmployees.FullName
        Dim clscompany As New Clssys_Companies(Page)
        clscompany.Find("ID=" & ClsContract.MainCompanyID)
        Dim DteVacationDate As DateTime = Request.QueryString("Fisical")



        ClsContract.Find("ID=" & intContractId)
        clsEmpClass.Find("ID=" & ClsContract.EmployeeClassID)

        Load_ClsLayers()
        If Not SetScreenSetting(intEmployeeID) Then Return
        New_Value(True)
        If Not Get_Attendance(ClsEmployees.ID) Then Return

        Dim ClsContracts As New Clshrs_Contracts(Page)
        Dim DteVacationStartDate As DateTime
        DteVacationStartDate = ClsEmployees.SetHigriDate(DteVacationDate)
        ClsContracts.ContractValidatoinId(intEmployeeID, DteVacationStartDate)



        DteFromDate = FiscFromDate


        Dim dblVacationDays As Double = 0
        If ClsContracts.ID > 0 Then





            '=======================================================


            Dim DtBenefitsCurr As New Data.DataTable
            Dim DtDeductionsCurr As New Data.DataTable
            ' ClsEmployees.CollectEmployeesTransactions("", ToDate, intEmployeeID, IntPeriodId, DtBenefitsCurr, DtDeductionsCurr, dblBenefits, dblDeduct, dblVacationDays, 0, dblVacationDays, 0, 0, 0, 0, 0, 0, Clshrs_EmployeesBase.ePrepareType.EndOfContract, Clshrs_EmployeesBase.ePrepareStage.Vacation)



            If Request.QueryString.Item("Fisical") <> "" Then
                DteVacationDate = Request.QueryString.Item("Fisical")
            End If
            lblWorkingDays.Text = 0
            totalBenfits = 0
            totalDeducation = 0
            If CheckBox_SalaryPayment.Checked Then
                Dim IntPaid As Integer
                If (IntNoOfWorkDays > 0) Then
                    IntPaid = 1
                Else
                    IntPaid = 0
                End If

                If IntPaid = 1 Then
                    'Rabie 19-08-2024
                    'ClsEmployees.CollectEmployeesTransactions("", ToDate, intEmployeeID, IntFisicalPeriod, DtBenefits, DtDeductions, dblBenefits, dblDeduct, IntNoOfWorkDays, 0, IntNoOfWorkDays, dbOvertimeSalary, dbHolidayHoursSalary, ObjSalaryPerHour, ObjSalaryPerDay, NotPermitLat, 0, Clshrs_EmployeesBase.ePrepareType.Normal, Clshrs_EmployeesBase.ePrepareStage.Normal, 0, 1, DteVacationDate)

                    ClsEmployees.CollectEmployeesTransactions("", DteVacationDate, intEmployeeID, IntFisicalPeriod, DtBenefits, DtDeductions, dblBenefits, dblDeduct, IntNoOfWorkDays, 0, IntNoOfWorkDays, dbOvertimeSalary, dbHolidayHoursSalary, ObjSalaryPerHour, ObjSalaryPerDay, NotPermitLat, 0, Clshrs_EmployeesBase.ePrepareType.EndOfContract, Clshrs_EmployeesBase.ePrepareStage.Vacation, 0, 1, DteVacationDate)
                    'ClsEmployees.CollectEmployeesTransactionsForSettlement("", ToDate, intEmployeeID, IntFisicalPeriod, DtBenefitsCurr, DtDeductionsCurr, dblBenefits, dblDeduct, dblVacationDays, 0, dblVacationDays, 0, 0, 0, 0, 0, 0, Clshrs_EmployeesBase.ePrepareType.EndOfContract, Clshrs_EmployeesBase.ePrepareStage.Vacation)
                    ClsEmployees.CollectEmployeesPayablities(ClsEmployees.ID, IntFisicalPeriod, DtBenefits, DtDeductions, dblBenefits, dblDeduct, "N")
                    If ClsEmployees.MaxLoanDedution > 0 Then
                        DtDeductions = maxloandeduction(DtDeductions, ClsEmployees.MaxLoanDedution)
                    End If
                    Dim Extrastrcommand As String = "select (isnull((select top 1 ID from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode),0)) as RelTransactionID,(isnull((select top 1 Sign from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode),0)) as Sign,Amount from hrs_EmployeeExtraItems"
                    Dim strFilter As String = " where EmployeeCode = '" & ClsEmployees.Code & "' and Status = 1 and FiscalPeriodID =" & IntFisicalPeriod
                    Dim dsEmployee As New Data.DataSet
                    dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, Data.CommandType.Text, Extrastrcommand & strFilter)
                    For EXB As Integer = 0 To dsEmployee.Tables(0).Rows.Count - 1
                        If dsEmployee.Tables(0).Rows(EXB)(1) > 0 Then
                            'DtBenefits.Rows.Add(New Object() {dsEmployee.Tables(0).Rows(EXB)(0), dsEmployee.Tables(0).Rows(EXB)(2), "Paid", Nothing, "Paid", "N"})
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

                    Dim AbsDys As Decimal = 0
                    Dim ObjAbsent As Double = 0
                    If ClsEmployeeClass.RegComputerID > 0 Then
                        Dim strcommand As String = "select isnull(sum(SalaryPerDay),0) from Att_AttendancePreparationProjects where isnull(IsAbsent,0) = 1 and isnull(IsVacation,0) = 0 and TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)) "
                        Dim Absvalue As Decimal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, strcommand)
                        strcommand = "Select isnull(count(ID),0) from Att_AttendancePreparationProjects where isnull(IsAbsent,0) = 1 And isnull(IsVacation, 0) = 0 And TrnsID In (Select ID from Att_AttendancePreparationDetails where EmployeeID = " & ClsEmployees.ID & " And CONVERT(Date,GAttendDate,103) >= CONVERT(Date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
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
                            DtDeductions.Rows.Add(New Object() {ClsEmployeeClass.RegComputerID, Absvalue, "Paid", Nothing, "Paid"})
                        End If

                        'If Absvalue > 0 Then
                        '    cmdString &= " Insert Into hrs_EmployeesTransactionsDetails (EmpTransProjID,TransactionTypeID,NumericValue,TextValue,EmployeePayabilityScheduleID)" & _
                        '             " Values (@ProjectTransID," & ClsEmployeeClass.RegComputerID & "," & Absvalue & ", 'Paid',Null); "
                        '    totalDeducationsum += Absvalue
                        'End If
                    End If

                    lblWorkingDays.Text = IIf(IntNoOfWorkDays < 0, 0, IntNoOfWorkDays)
                Else
                    CheckBox_SalaryPayment.Checked = False
                    DtBenefits = DtBenefitsCurr.Clone()
                    DtDeductions = DtDeductionsCurr.Clone()
                    ClsEmployees.CollectEmployeesPayablities(intEmployeeID, IntFisicalPeriod, DtBenefitsCurr, DtDeductionsCurr, dblBenefits, dblDeduct, "N")
                End If
            Else
                DtBenefits = DtBenefitsCurr.Clone()
                DtDeductions = DtDeductionsCurr.Clone()
                ' ClsEmployees.CollectEmployeesPayablities(intEmployeeID, IntFisicalPeriod, DtBenefitsCurr, DtDeductionsCurr, dblBenefits, dblDeduct, "N")
            End If


            For Each bRow As Data.DataRow In DtBenefits.Rows
                If bRow.Item("DescriptionSign") = "Paid" Or bRow.Item("DescriptionSign") = "Paid By Days" Then
                    totalBenfits += bRow("Amount")
                End If
            Next

            For Each bRow As Data.DataRow In DtDeductions.Rows

                If bRow.Item("DescriptionSign") = "Paid" Or bRow.Item("DescriptionSign") = "Paid By Days" Then
                    totalDeducation += bRow("Amount")
                End If
            Next

            'Get Vacations Details
            If intEmpTransID > 0 Then
                hdnEmpTrans.Value = intEmpTransID
                If ClsEmployeesTransactions.Find("ID=" & intEmpTransID) Then
                    dblVacationDays = ClsEmployeesTransactions.FinancialWorkingUnits
                    totalBenfits = 0
                    totalDeducation = 0
                    DtBenefits.Rows.Clear()
                    DtDeductions.Rows.Clear()

                    ClsEmployeesTransactionsDet.FindRelatedToProjects("EmployeeTransactionID = " & ClsEmployeesTransactions.ID & " And (Select sign From hrs_TransactionsTypes Where ID=hrs_EmployeesTransactionsDetails.TransactionTypeID)=1 ")
                    ClsEmployeesTransactionsDet.DataSet.Tables(0).Columns("NumericValue").ColumnName = "Amount"
                    ClsEmployeesTransactionsDet.DataSet.Tables(0).Columns("TextValue").ColumnName = "Description"
                    DtBenefits = ClsEmployeesTransactionsDet.DataSet.Tables(0).Copy()

                    If ClsEmployeesTransactionsDet.DataSet.Tables(0).Rows.Count > 0 Then
                        For Each drDataRow As Data.DataRow In ClsEmployeesTransactionsDet.DataSet.Tables(0).Rows
                            totalBenfits += drDataRow.Item("Amount")
                        Next
                    End If

                    ClsEmployeesTransactionsDet.FindRelatedToProjects("EmployeeTransactionID = " & ClsEmployeesTransactions.ID & " And (Select sign From hrs_TransactionsTypes Where ID=hrs_EmployeesTransactionsDetails.TransactionTypeID)=-1 And (Select ispaid From hrs_TransactionsTypes Where ID=hrs_EmployeesTransactionsDetails.TransactionTypeID)=1")
                    ClsEmployeesTransactionsDet.DataSet.Tables(0).Columns("NumericValue").ColumnName = "Amount"
                    ClsEmployeesTransactionsDet.DataSet.Tables(0).Columns("TextValue").ColumnName = "Description"
                    DtDeductions = ClsEmployeesTransactionsDet.DataSet.Tables(0).Copy()

                    If ClsEmployeesTransactionsDet.DataSet.Tables(0).Rows.Count > 0 Then
                        For Each drDataRow As Data.DataRow In ClsEmployeesTransactionsDet.DataSet.Tables(0).Rows
                            totalDeducation += drDataRow.Item("Amount")
                        Next
                    End If

                    TextBox_SalaryAmount.Text = Math.Round(Val(totalBenfits - totalDeducation), 2)
                    btnSave.Enabled = False
                    lblWorkingDays.Enabled = False
                    TextBox_SalaryAmount.Enabled = False
                End If
            End If


            uwgEmployeeTransaction.DataSource = DtBenefits
            uwgEmployeeTransaction.DataBind()
            If DtDeductions.Rows.Count > 0 Then
                uwgPayabilities.DataSource = DtDeductions '.Select("DescriptionSign = 'Paid' or DescriptionSign = ''").CopyToDataTable
                uwgPayabilities.DataBind()
            End If




            If intEmpTransID > 0 Then
                uwgEmployeeTransaction.DisplayLayout.Bands(0).Columns(2).AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgPayabilities.DisplayLayout.Bands(0).Columns(2).AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            Else
                uwgEmployeeTransaction.DisplayLayout.Bands(0).Columns(2).AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes
                uwgPayabilities.DisplayLayout.Bands(0).Columns(2).AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes
                btnSave.Enabled = True
            End If

            TextBox_SalaryAmount.Text = Math.Round(Val(totalBenfits - totalDeducation), 2)




        End If
    End Sub
    Private Function maxloandeduction(dtDeduct As DataTable, MaxLoanDedution As Double) As DataTable

        Dim drows() As DataRow = dtDeduct.Select("EmpSchID >0")
        If drows.Length > 0 Then
            Dim dt As DataTable = dtDeduct.Select("EmpSchID >0").CopyToDataTable
            Dim tempdt As DataTable = dtDeduct.Select("isnull(EmpSchID,0)=0").CopyToDataTable
            dtDeduct.Clear()
            dtDeduct = tempdt.Select().CopyToDataTable




            Dim totalLoan = dt.Compute("Sum(amount)", "")
            If totalLoan >= MaxLoanDedution Then
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
                For Each row As DataRow In dt.Rows

                    dtDeduct.Rows.Add(New Object() {row(0), row(1), row(2), row(3), row(4), row(5)})
                    MaxLoanDedution = MaxLoanDedution - row("amount")


                Next
            End If
        End If
        Return dtDeduct

    End Function
    Private Function CheckEmployee1(ByVal intEmployeeID As Integer, ByVal DteVacationDate As Date) As Integer
        Dim ClsContract As New Clshrs_Contracts(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsContract.ConnectionString)
        Dim clsEmployeevacations As New Clshrs_EmployeesVacations(Page)
        Dim clsEmployee As New Clshrs_Employees(Page)
        Dim clsVacType As New Clshrs_VacationsTypes(Page)
        Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)
        Dim intValidContract As Integer = ClsContract.ContractValidatoinId(intEmployeeID, DteVacationDate)

        If clsEmployee.Find("ID=" & intEmployeeID) Then
            txtCode.Text = clsEmployee.Code
            lblEmployeeName.Text = clsEmployee.FullName
            txtEmployeeId.Value = intEmployeeID
        End If
        If intValidContract <= 0 Then
            Clear()
            Return intValidContract
        End If


        Return intValidContract
    End Function
    Private Sub Clear(Optional ByVal ClearCode As Boolean = False)
        If ClearCode Then
            txtCode.Text = ""
            uwgVacationHistory.DataSource = Nothing
            uwgVacationHistory.DataBind()
        End If
        txtEmployeeId.Value = 0
        lblEmployeeName.Text = ""
        lblWorkingDays.Text = ""
        TextBox_SalaryAmount.Text = ""
        uwgEmployeeTransaction.DataSource = Nothing
        uwgEmployeeTransaction.DataBind()

        uwgPayabilities.DataSource = Nothing
        uwgPayabilities.DataBind()

    End Sub
#End Region

#Region "Shared Function"

    Public Shared Function Find(ByVal Table As String, ByVal Filter As String, ByRef DataSet As DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Dim mSelectCommand = " Select * From " & Table

        Dim mSqlDataAdapter As New SqlClient.SqlDataAdapter
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)

        Try
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

    Public Shared Function CheckMyDate(ByVal strDate As String) As String
        Try

            Dim strRet As String = IIf(CType(HttpContext.Current.Session("Log"), String) = "Eng", "0", "1") & ","
            Dim strDiff As String = String.Empty
            If strDate = "__/__/____" Or strDate = "  /  /    " Then
                strRet &= "1"
                Return strRet
            End If
            If SetHigriDate2(strDate, strDiff) = Nothing Then
                strRet &= "0"
            Else
                strRet &= "1"
            End If
            Return strRet

        Catch ex As Exception

        End Try
    End Function

    Public Shared Function SetHigriDate2(ByVal dteDate As Object, ByRef strDetails As String) As Object
        Try

            Dim isHijri As Int16
            Dim intDiff As Int16 = 0
            Dim dteDateOut As Object

            If Not CheckValidDate(dteDate) Then
                strDetails = String.Empty
                Return Nothing
            End If

            Dim strArr() As String = dteDate.ToString.Split(" ")(0).Split("/")

            If CInt(strArr(2)) < 1900 Then
                isHijri = 1
                If CInt(strArr(1)) = 2 Then ' Month is 2
                    If CInt(strArr(0)) > 28 Then
                        intDiff = CInt(strArr(0)) - 28
                        dteDate = "28/" & Format(CInt(strArr(1)), "00") & "/" & strArr(2)
                    End If
                End If
            Else
                isHijri = 0
            End If

            strDetails = isHijri.ToString & "," & intDiff

            If isHijri = 1 Then
                dteDateOut = GetRelativeDate(dteDate, DateType.Hijri, Directions.Input)
            Else
                dteDateOut = GetRelativeDate(dteDate, DateType.Gregorian, Directions.Input)
            End If

            Return CDate(dteDateOut).AddDays(intDiff)

        Catch ex As Exception

        End Try
    End Function

    Public Shared Function GetHigriDate2(ByVal dteDate As Object, ByVal strDetails As String) As Object
        Try

            Dim isHijri As Int16
            Dim intDiff As Int16 = 0
            Dim mDataHandler As New Venus.Shared.DataHandler
            Dim strFormatDate As String = ConfigurationManager.AppSettings("DATEFORMAT")
            Dim dteDateOut As Object
            Dim dsCompanies As New DataSet
            Dim MainCompanyID As String = CType(HttpContext.Current.Session("CompanyID"), String)

            Find("sys_Companies", "ID = " & MainCompanyID, dsCompanies)

            If Not CheckValidDate(dteDate) Then
                Return Nothing
            End If
            If strDetails.Trim = String.Empty Then
                If dsCompanies.Tables(0).Rows(0).Item("IsHigry") = True Then
                    isHijri = 1
                Else
                    isHijri = 0
                End If
            Else
                Dim strDetailsArr As String() = strDetails.Split(",")
                isHijri = CInt(strDetailsArr(0))
                intDiff = CInt(strDetailsArr(1))
            End If

            If isHijri = 1 Then
                dteDateOut = Venus.Shared.DataHandler.DataValue_Out(GetRelativeDate(CDate(dteDate).AddDays(IIf(intDiff > 0, -intDiff, 0)), DateType.Hijri, Directions.Output), Global.System.Data.SqlDbType.DateTime)
            Else
                dteDateOut = Venus.Shared.DataHandler.DataValue_Out(GetRelativeDate(dteDate, DateType.Gregorian, Directions.Output), Global.System.Data.SqlDbType.DateTime)
            End If

            dteDateOut = String.Format(dteDateOut, strFormatDate)
            If intDiff > 0 Then
                Dim strArr As String() = dteDateOut.ToString.Split(" ")(0).Split("/")
                Dim intDay As Int16 = CInt(strArr(0)) + intDiff
                dteDateOut = Format(intDay, "00") & "/" & Format(CInt(strArr(1)), "00") & "/" & strArr(2)
            End If

            Return dteDateOut

        Catch ex As Exception

        End Try
    End Function

    Public Shared Function CheckValidDate(ByVal dteDateIn As Object) As Boolean
        Try

            If IsDBNull(dteDateIn) Then
                Return False
            End If
            If IsNothing(dteDateIn) Then
                Return False
            End If
            If dteDateIn.ToString.Trim = "" Then
                Return False
            End If
            If TypeOf (dteDateIn) Is Date Then
                If dteDateIn.Year = 1 Then
                    Return False
                End If
            ElseIf TypeOf (dteDateIn) Is String Then
                If dteDateIn.ToString = "  /  /    " Or dteDateIn.ToString = "__/__/____" Then
                    Return False
                End If
                Dim strArr() As String = dteDateIn.ToString.Split(" ")(0).Split("/")
                If strArr.Length < 3 Then
                    Return False
                End If
                Dim intDay As Integer = CInt(IIf(strArr(0).Trim = "" Or strArr(0).Trim = "__", 0, strArr(0).Trim("_").Trim))
                Dim intMonth As Integer = CInt(IIf(strArr(1).Trim = "" Or strArr(1).Trim = "__", 0, strArr(1).Trim("_").Trim))
                Dim intYear As Integer = CInt(IIf(strArr(2).Trim = "" Or strArr(2).Trim = "____", 0, strArr(2).Trim("_").Trim))
                If intDay <= 0 Or intDay > 31 Then
                    Return False
                End If
                If intMonth <= 0 Or intMonth > 12 Then
                    Return False
                End If
                If intYear <= 0 Or intYear < 1300 Or intYear > 2070 Then
                    Return False
                End If
                If intYear > 1900 Then
                    Try
                        Dim dte As Date = New Date(intYear, intMonth, intDay)
                    Catch ex As Exception
                        Return False
                    End Try
                Else
                    If intDay > 30 Then
                        Return False
                    End If
                End If
            End If

            Return True

        Catch ex As Exception

        End Try
    End Function

    Public Shared Function GetRelativeDate(ByVal iDate As Object, ByVal DateDisplayType As DateType, ByVal oDirection As Directions) As Object
        Try

            If IsNothing(iDate) And oDirection = Directions.Output Then
                Return Nothing
            ElseIf IsNothing(iDate) And oDirection = Directions.Input Then
                Return DBNull.Value
            End If

            If (oDirection = Directions.Output And (IsDBNull(iDate) Or IsNothing(iDate) Or iDate.ToString.Trim.ToLower = "null" Or iDate.ToString.Trim = "")) Then
                Return Nothing
            Else
                If (oDirection = Directions.Input And (IsDBNull(iDate) Or IsNothing(iDate) Or iDate.ToString.Trim.ToLower = "null" Or iDate.ToString.Trim = "")) Then
                    Return DBNull.Value
                End If
            End If
            If (CDate(iDate).Year < 1300) Then
                Return Nothing
            End If

            If iDate = "1/1/1900" Then
                Return Nothing
            End If

            Dim oDate As Date = iDate

            Dim strFormatDate As String = "dd/MM/yyyy"
            Dim StrSQLCommand As String = String.Empty
            Dim UmAlQuraCalender As New Global.System.Globalization.UmAlQuraCalendar
            Dim GregorianCalender As New Global.System.Globalization.GregorianCalendar
            Dim IntYear As Integer
            Dim IntMonth As Integer
            Dim IntDay As Integer

            Dim IntTempGYear As Integer
            Dim IntTempGMonth As Integer
            Dim IntTempGDay As Integer
            Dim StrTempGDate As String

            Dim IntTempHYear As Integer
            Dim IntTempHMonth As Integer
            Dim IntTempHDay As Integer
            Dim StrTempHDate As String

            Dim StrDate As String = String.Empty
            Dim StrReturnDate As String
            Dim CurrentDate As Date
            Dim GDate As Date
            Dim HDate As Date

            Dim mSqlCommand As SqlClient.SqlCommand
            Dim mConnectionString As String = CType(HttpContext.Current.Session("ConnectionString"), String)
            Dim mLog As String = CType(HttpContext.Current.Session("Log"), String)
            Dim HostPage As System.Web.UI.Page


            Dim nav As New Venus.Shared.Web.NavigationHandler(mConnectionString)

            '---------------------------------------------------------------------------'
            'This part is to prepare the data to get the relative date from the databese' 
            '---------------------------------------------------------------------------'
            IntYear = DatePart(DateInterval.Year, oDate)
            IntMonth = DatePart(DateInterval.Month, oDate)
            IntDay = DatePart(DateInterval.Day, oDate)
            StrDate = Format(IntDay, "00") & "/" & Format(IntMonth, "00") & "/" & IntYear.ToString

            '---------------------------------------------------------------------------'
            'This part to get the relative date according to microsoft standerd calender' 
            '---------------------------------------------------------------------------'
            If IntYear <= 1600 Then
                IntTempGYear = GregorianCalender.GetYear(oDate)
                IntTempGMonth = GregorianCalender.GetMonth(oDate)
                IntTempGDay = GregorianCalender.GetDayOfMonth(oDate)
                StrTempGDate = Format(IntTempGDay, "00") + "/" + Format(IntTempGMonth, "00") + "/" + IntTempGYear.ToString
            Else
                IntTempHYear = UmAlQuraCalender.GetYear(oDate)
                IntTempHMonth = UmAlQuraCalender.GetMonth(oDate)
                IntTempHDay = UmAlQuraCalender.GetDayOfMonth(oDate)
                StrTempHDate = Format(IntTempHDay, "00") + "/" + Format(IntTempHMonth, "00") + "/" + IntTempHYear.ToString
            End If


            '---------------------------------------------------------------------------'
            'Get the Relative Date according to the input paramters and depending on the'
            'display language                                                           '
            '---------------------------------------------------------------------------'
            Select Case DateDisplayType
                Case DateType.Hijri

                    Select Case oDirection
                        Case Directions.Input
                            If IntYear < 1600 Then
                                StrSQLCommand = "Set Dateformat dmy Select GDate from sys_GHCalendar Where HDate ='" & StrDate & "'"
                                mSqlCommand = New SqlClient.SqlCommand
                                mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                                mSqlCommand.CommandType = CommandType.Text
                                mSqlCommand.CommandText = StrSQLCommand
                                mSqlCommand.Connection.Open()
                                StrReturnDate = mSqlCommand.ExecuteScalar
                                mSqlCommand.Connection.Close()
                                If StrReturnDate Is Nothing Then
                                    StrReturnDate = HijriToGreg(StrDate, strFormatDate)
                                    If Not IsNothing(StrReturnDate) Then
                                        '============================== Insert GH Date if not found [Start]
                                        Dim strInser As String = "Set Dateformat dmy  Insert Into sys_GHCalendar Values ('" & StrReturnDate & "','" & StrDate & "');"
                                        mSqlCommand.CommandText = strInser
                                        mSqlCommand.Connection.Open()
                                        mSqlCommand.ExecuteNonQuery()
                                        mSqlCommand.Connection.Close()
                                        '============================== Insert GH Date if not found [ END ]
                                    Else
                                        StrReturnDate = StrTempGDate
                                    End If

                                End If
                            Else
                                Return oDate
                            End If
                        Case Directions.Output
                            StrSQLCommand = "Set Dateformat dmy Select HDate from sys_GHCalendar Where GDate between '" & StrDate & " 00:00:00 " & "' And '" & StrDate & " 23:00:00 '"
                            mSqlCommand = New SqlClient.SqlCommand
                            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                            mSqlCommand.CommandType = CommandType.Text
                            mSqlCommand.CommandText = StrSQLCommand
                            mSqlCommand.Connection.Open()
                            StrReturnDate = mSqlCommand.ExecuteScalar
                            mSqlCommand.Connection.Close()
                            If StrReturnDate Is Nothing Then
                                Try
                                    '============================== Insert GH Date if not found [Start]
                                    Dim strInser As String = "Set Dateformat dmy  Insert Into sys_GHCalendar Values ('" & StrDate & "','" & StrTempHDate & "');"
                                    mSqlCommand.CommandText = strInser
                                    mSqlCommand.Connection.Open()
                                    mSqlCommand.ExecuteNonQuery()
                                    mSqlCommand.Connection.Close()
                                    '============================== Insert GH Date if not found [ END ]
                                    StrReturnDate = StrTempHDate
                                Catch ex As SqlClient.SqlException
                                    If ex.Number = 2601 Then

                                        HostPage.ClientScript.RegisterClientScriptBlock(HostPage.GetType(), "", "<script language=""javascript"">alert('" & IIf(mLog = "Eng", "Found invalid saved Dates", "يوجد تواريخ محفوظة خطأ") & "');</script>")

                                        Return Nothing
                                    End If

                                End Try
                            End If
                    End Select

                Case DateType.Gregorian
                    Select Case oDirection
                        Case Directions.Input
                            If IntYear < 1600 Then

                                StrSQLCommand = "Set Dateformat dmy Select GDate from sys_GHCalendar Where HDate ='" & StrDate & "'"
                                mSqlCommand = New SqlClient.SqlCommand
                                mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                                mSqlCommand.CommandType = CommandType.Text
                                mSqlCommand.CommandText = StrSQLCommand
                                mSqlCommand.Connection.Open()
                                StrReturnDate = mSqlCommand.ExecuteScalar
                                mSqlCommand.Connection.Close()
                                If StrReturnDate Is Nothing Then
                                    StrReturnDate = HijriToGreg(StrDate, strFormatDate)
                                    If Not IsNothing(StrReturnDate) Then
                                        '============================== Insert GH Date if not found [Start]
                                        Dim strInser As String = "Set Dateformat dmy  Insert Into sys_GHCalendar Values ('" & StrReturnDate & "','" & StrDate & "');"
                                        mSqlCommand.CommandText = strInser
                                        mSqlCommand.Connection.Open()
                                        mSqlCommand.ExecuteNonQuery()
                                        mSqlCommand.Connection.Close()
                                        '============================== Insert GH Date if not found [ END ]
                                    Else
                                        StrReturnDate = StrTempGDate
                                    End If
                                End If

                            Else
                                Return oDate
                            End If
                        Case Directions.Output
                            Return oDate
                    End Select
            End Select
            Return StrReturnDate
        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "PageMethods"

    <System.Web.Services.WebMethod()> _
    Public Shared Function CheckDate(ByVal strDate As String) As String
        'Return "0,0" 

        CheckMyDate(strDate)

        If ClsDataAcessLayer.IsGreg(strDate) Then
            Return ClsDataAcessLayer.GregToHijri(strDate, "dd/MM/yyyy")
        ElseIf ClsDataAcessLayer.IsHijri(strDate) Then
            Return ClsDataAcessLayer.HijriToGreg(strDate, "dd/MM/yyyy")
        Else
            Return ""
        End If
    End Function

#End Region







End Class
