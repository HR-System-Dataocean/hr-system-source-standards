Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports Microsoft.VisualBasic
Imports System.Data

Partial Class frmProjectEmployeePenalities
    Inherits MainPage

#Region "Protected Sub"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)

        ClsEmployee.Find("ID = " & Request.QueryString.Item("ID"))
        lblProjectCode.Text = ClsEmployee.Code
        lblProjectName.Text = ClsEmployee.FullName

        If Not IsPostBack Then
            lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
            Page.Session.Add("Lage", lblLage.Text)
            Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)
            txtStartDate.Value = DateTime.Now.ToString("ddMMyyyy")
            Dim clsEmployeevacations As New Clshrs_EmployeesVacations(Me)
            If (clsEmployeevacations.FindEmployeeVacations(" hrs_EmployeesVacations.EmployeeID=" & Request.QueryString.Item("ID") & " And Convert(smalldatetime,Convert(varchar,ActualStartDate ,103)) <= Convert(smalldatetime,Convert(varchar,'" & txtStartDate.Text & "' ,103))	And	(ActualEndDate Is Null Or  Convert(smalldatetime,Convert(varchar,ActualEndDate ,103)) > Convert(smalldatetime,Convert(varchar,'" & txtStartDate.Text & "',103)))")) Then
                LinkButton_Absent.Visible = False
                Msg.Text = "هذا الموظف فى إجازة"
            Else
                LinkButton_Absent.Visible = True
                Msg.Text = ""
            End If
            Dim ClsPenality As New Clshrs_Penalties(Me)
            ClsPenality.GetDropDownList(ddlPenality, True)
        End If
    End Sub
#End Region

    Protected Sub txtStartDate_ValueChange(sender As Object, e As Infragistics.WebUI.WebDataInput.ValueChangeEventArgs) Handles txtStartDate.ValueChange
        Dim clsEmployeevacations As New Clshrs_EmployeesVacations(Me)
        If (clsEmployeevacations.FindEmployeeVacations(" hrs_EmployeesVacations.EmployeeID=" & Request.QueryString.Item("ID") & " And Convert(smalldatetime,Convert(varchar,ActualStartDate ,103)) <= Convert(smalldatetime,Convert(varchar,'" & txtStartDate.Text & "' ,103))	And	(ActualEndDate Is Null Or  Convert(smalldatetime,Convert(varchar,ActualEndDate ,103)) > Convert(smalldatetime,Convert(varchar,'" & txtStartDate.Text & "',103)))")) Then
            LinkButton_Absent.Visible = False
            Msg.Text = "هذا الموظف فى إجازة"
        Else
            LinkButton_Absent.Visible = True
            Msg.Text = ""
        End If
    End Sub
    Private dbBasicSalary As Double = 0
    Private FromDate As DateTime
    Private ToDate As DateTime
    Private Function Calculate_SalaryPerHour(ByRef Amount As Double, EmpID As Integer, fisca As Integer, fiscDays As Integer) As Boolean
        Try
            Dim ClsContract As New Clshrs_Contracts(Page)
            Dim ClsClass As New Clshrs_EmployeeClasses(Page)
            Dim ContID As Integer = ClsContract.ContractValidatoinId(EmpID, fisca)
            ClsContract.Find("ID = " & ContID)
            Dim clsCompanies As New Clssys_Companies(Me)
            clsCompanies.Find("ID=" & clsCompanies.MainCompanyID)
            If clsCompanies.SalaryCalculation = 0 Then            'Get Basic Salary
                dbBasicSalary = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsCompanies.ConnectionString, Data.CommandType.Text, "set dateformat dmy; select dbo.fn_GetBasicSalary(" & ClsContract.ContractValidatoinId(EmpID, ClsClass.SetHigriDate(txtStartDate.Text)) & ",'" & ClsClass.SetHigriDate(txtStartDate.Text) & "')")
                Amount = dbBasicSalary
            ElseIf clsCompanies.SalaryCalculation = 1 Then        'Get Total Salary By Days
                dbBasicSalary = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsCompanies.ConnectionString, Data.CommandType.Text, "set dateformat dmy; select dbo.fn_GetTotalAdditions(" & ClsContract.ContractValidatoinId(EmpID, ClsClass.SetHigriDate(txtStartDate.Text)) & ",'" & ClsClass.SetHigriDate(txtStartDate.Text) & "')")
                Amount = dbBasicSalary
            End If
            ClsClass.Find("ID =" & ClsContract.EmployeeClassID)
            If ClsClass.NoOfDaysPerPeriod > 0 Then
                Amount = Amount / ClsClass.NoOfDaysPerPeriod
            Else
                Amount = Amount / fiscDays
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Protected Sub LinkButton_Absent_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Absent.Click
        Dim clsProjectEmployeesPen As New Clshrs_ProjectEmployeePenalities(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsProjectEmployeesPen.ConnectionString)
        If ddlPenality.SelectedValue > 0 Then
            With clsProjectEmployeesPen
                .TrnsDate = .SetHigriDate(txtStartDate.Text)
                .PenalityID = ddlPenality.SelectedValue
                .EmployeeID = Request.QueryString.Item("ID")
                .Switch = IIf(Fixed.Checked = True, 1, 0)
                .Amount = txtAmount.Value
                .Remarks = txtrefno.Text
                .Save()

                Dim clsCompanies As New Clssys_Companies(Page)
                Dim clsBranch As New Clssys_Branches(Page)
                Dim clsemployee As New Clshrs_Employees(Page)
                clsemployee.Find("ID = " & Request.QueryString.Item("ID"))
                clsCompanies.Find("ID = " & .MainCompanyID)
                clsBranch.Find("ID=" & clsemployee.BranchID)
                Dim fiscalperiood As New Clssys_FiscalYearsPeriods(Me)
                fiscalperiood.Find("sys_FiscalYearsPeriods.CancelDate Is null And convert(datetime,'" & .TrnsDate & "') >= sys_FiscalYearsPeriods.FromDate and convert(datetime,'" & .TrnsDate & "') <= sys_FiscalYearsPeriods.ToDate")

                Dim fperiodid As Int32 = fiscalperiood.ID
                fiscalperiood.Find("ID = " & fperiodid)
                FromDate = fiscalperiood.FromDate
                ToDate = fiscalperiood.ToDate
                If clsBranch.PrepareDay > 0 Then
                    If .TrnsDate >= New DateTime(fiscalperiood.FromDate.Year, fiscalperiood.FromDate.Month, clsBranch.PrepareDay) Then
                        fperiodid = fperiodid + 1
                    End If
                    fiscalperiood.Find("ID = " & fperiodid)
                    FromDate = fiscalperiood.FromDate
                    ToDate = fiscalperiood.ToDate
                    FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsBranch.PrepareDay)
                    ToDate = FromDate.AddMonths(1).AddDays(-1)
                Else
                    If clsCompanies.PrepareDay > 0 Then
                        If .TrnsDate >= New DateTime(fiscalperiood.FromDate.Year, fiscalperiood.FromDate.Month, clsCompanies.PrepareDay) Then
                            fperiodid = fperiodid + 1
                        End If
                        fiscalperiood.Find("ID = " & fperiodid)
                        FromDate = fiscalperiood.FromDate
                        ToDate = fiscalperiood.ToDate
                        FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsCompanies.PrepareDay)
                        ToDate = FromDate.AddMonths(1).AddDays(-1)
                    End If
                End If

                Dim ClsPenality As New Clshrs_Penalties(Me)
                ClsPenality.Find("ID =" & ddlPenality.SelectedValue)
                Dim hrstrans As New Clshrs_TransactionsTypes(Page)
                hrstrans.Find("ID = " & ClsPenality.TransactionTypeID)

                Dim PenalityVal As Double = 0
                If Fixed.Checked Then
                    PenalityVal = txtAmount.Value
                Else
                    Dim SPH As Double = 0
                    Calculate_SalaryPerHour(SPH, Request.QueryString.Item("ID"), fperiodid, (ToDate.Subtract(FromDate).Days + 1))
                    PenalityVal = txtAmount.Value * SPH
                End If
                Dim strcommand As String = "set dateformat dmy; insert into hrs_EmployeeExtraItems values ((select Code from hrs_Employees where ID = " & Request.QueryString.Item("ID") & "),''," & hrstrans.Code & "," & PenalityVal & "," & fperiodid & ",1,'" & .TrnsDate.ToString("dd/MM/yyyy") & "',4,(select top 1 ID from hrs_ProjectEmployeePenalities where EmployeeID = " & Request.QueryString.Item("ID") & " order by ID desc),'','')"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(.ConnectionString, Data.CommandType.Text, strcommand)
            End With
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe();", True)
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "No Employees Selected/لا يوجد موظفين تم إختيارهم"))
        End If
    End Sub
End Class
