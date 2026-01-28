Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Web.UI.WebControls

Partial Class frmDistributedSalary
    Inherits MainPage

#Region "Protected Sub"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim IntFiscalPeriod As Integer = IIf(Request.QueryString.Item("Fisical") <> "", CInt(Request.QueryString.Item("Fisical")), 0)
            Dim IntIncomingEmpId As Integer = IIf(Request.QueryString.Item("ID") <> "", CInt(Request.QueryString.Item("ID")), 0)

            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim strCommand As String = "SELECT PRO.ArbName, SDE.ProjectPercentage, SDE.ProjectAmount,TTY.ArbName as TransactionType,case when TTY.Sign=1 then 'استحقاق' else   'استقطاع'end as Sign  FROM hrs_SalartDistExec SDE INNER JOIN hrs_Projects PRO ON SDE.ProjectID = PRO.ID  join hrs_TransactionsTypes TTY on TTY.code = SDE.TransactionCode WHERE FiscalPeriodID = " & IntFiscalPeriod & " AND EmployeeID = " & IntIncomingEmpId
            Dim dsProjects As New Data.DataSet
            dsProjects = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, Data.CommandType.Text, strCommand)

            grdProjects.DataSource = dsProjects.Tables(0)
            grdProjects.DataBind()
        End If
    End Sub

#End Region

#Region "Private Function"


#End Region

End Class
