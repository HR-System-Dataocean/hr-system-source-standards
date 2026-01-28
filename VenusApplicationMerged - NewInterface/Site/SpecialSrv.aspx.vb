Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class SpecialSrv
    Inherits System.Web.UI.Page
    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim clscontracts As New Clshrs_Contracts(Me.Page)
        Dim clsFormula As Clshrs_FormulaSolver
        clscontracts.Find("")
        Dim dt As Data.DataTable = clscontracts.DataSet.Tables(0)
        For Each dr As DataRow In dt.Rows
            Dim contransactions As New Clshrs_ContractsTransactions(Me.Page)
            If contransactions.Find("ContractID = " & dr("ID")) Then
                Dim dt1 As DataTable = contransactions.DataSet.Tables(0)
                For Each dr1 As DataRow In dt1.Rows
                    Dim clsTransType As New Clshrs_TransactionsTypes(Me.Page)
                    clsTransType.Find("ID = " & dr1("TransactionTypeID"))
                    Dim amount As Double = 0
                    If Not clsTransType.IsBasicSalary Then
                        If clsTransType.TransactionGroupID <> 3 Then
                        Else
                            If clsTransType.Formula.Length > 0 Then
                                If IsNumeric(clsTransType.Formula) Then
                                Else
                                    Dim fisPeriodID As Integer
                                    Dim fisFrom As DateTime
                                    Dim fisTo As DateTime

                                    Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Me.Page)
                                    ClsFisicalPeriods.GetFisicalperiodInfo(clscontracts.SetHigriDate2(DateTime.Now.ToString("dd/MM/yyyy"), dr1("ActiveDate_D")), fisPeriodID, fisFrom, fisTo)

                                    clsFormula = New Clshrs_FormulaSolver(clscontracts.ConnectionString, Me.Page)
                                    clsFormula.Executedate = clscontracts.SetHigriDate2(dr1("ActiveDate"), dr1("ActiveDate_D"))
                                    clsFormula.FormulaCalculated = "N"
                                    clsFormula.EmployeeID = dr("EmployeeID")
                                    clsFormula.BolBeginOfContract = False
                                    clsFormula.NoOfWorkingDays = 30
                                    clsFormula.NoOfDaysPerPeriod = 30
                                    clsFormula.FiscalPeriodID = fisPeriodID
                                    clsFormula.EvaluateExpression(clsTransType.Formula, 0)
                                    amount = clsFormula.Output
                                    amount = IIf(amount < 0, 0, amount)

                                    Dim contransactions1 As New Clshrs_ContractsTransactions(Me.Page)
                                    contransactions1.Find("ID = " & dr1("ID"))
                                    contransactions1.Amount = amount
                                    contransactions1.Update("ID = " & dr1("ID"))
                                End If
                            Else
                            End If
                        End If
                    End If
                Next
            End If
        Next
    End Sub
End Class
