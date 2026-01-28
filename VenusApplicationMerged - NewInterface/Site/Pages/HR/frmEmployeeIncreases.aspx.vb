Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeeIncreases
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
                btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
        End If
        If Not IsPostBack Then
            UwgSearchEmployees.Columns.FromKey("EmployeeName").CellStyle.HorizontalAlign = CInt(ClsNavigationHandler.SetLanguage(Page, "1/3"))

            Dim ClsTransactionsTypes As New Clshrs_TransactionsTypes(Page)
            ClsTransactionsTypes.GetList(UwgSearchEmployees.DisplayLayout.Bands(0).Columns(5).ValueList, True, "Code")
            lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
            Page.Session.Add("Lage", lblLage.Text)
            Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetData()
    End Sub

    Protected Sub Button_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Save.Command, ImageButton_Save.Command, LinkButton_Delete.Command, ImageButton_Delete.Command, LinkButton_Transfeer.Command, ImageButton_Transfeer.Command, ImageButton_Refund.Command, LinkButton_Refund.Command
        Try
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ClsContract As New Clshrs_Contracts(Page)
            Dim ClsTransactionsType As New Clshrs_TransactionsTypes(Page)
            Dim ClsContractsTransaction As New Clshrs_ContractsTransactions(Page)
            Dim clsFormula As Clshrs_FormulaSolver

            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Select Case DirectCast(e, System.Web.UI.WebControls.CommandEventArgs).CommandArgument
                Case "Transfeer"
                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        ClsContractsTransaction = New Clshrs_ContractsTransactions(Page)
                        If row.Cells.FromKey("EmployeeCode").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                            Continue For
                        End If
                        If row.Cells(1).Value = True Then
                            If row.Cells.FromKey("RelEmployeeID").Value = 0 Or row.Cells.FromKey("RelTransactionID").Value = 0 Then
                                Continue For
                            Else
                                If ClsEmployee.Find("Code='" & row.Cells.FromKey("EmployeeCode").Value & "'") Then
                                    If ClsContract.Find(" EmployeeId = " & ClsEmployee.ID & " And Isnull(CancelDate,'')= '' and Isnull(EndDate,'')= '' Order By StartDate DESC") Then
                                        If ClsTransactionsType.Find("ID='" & row.Cells.FromKey("RelTransactionID").Value & "'") Then
                                            If ClsContractsTransaction.Find("ContractID=" & ClsContract.ID & " and TransactionTypeID = " & ClsTransactionsType.ID) Then
                                                ClsContractsTransaction.Amount = row.Cells.FromKey("Amount").Value
                                                ClsContractsTransaction.Active = True
                                                ClsContractsTransaction.ActiveDate = ClsContractsTransaction.SetHigriDate2(row.Cells.FromKey("ActiveDate").Value, ClsContractsTransaction.ActiveDate_D)
                                                ClsContractsTransaction.RegComputerID = row.Cells.FromKey("ID").Value
                                                ClsContractsTransaction.Save()
                                                'Dim DSAllTransactions As Data.DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, System.Data.CommandType.Text, "select * from hrs_ContractsTransactions where canceldate is null and TransactionTypeID <> '" & ClsContractsTransaction.TransactionTypeID & "' and ContractID = " & ClsContract.ID)
                                                'For Each dr As Data.DataRow In DSAllTransactions.Tables(0).Rows
                                                '    ClsContractsTransaction = New Clshrs_ContractsTransactions(Page)
                                                '    ClsContractsTransaction.Find("ID=" & dr("ID"))
                                                '    ClsTransactionsType.Find("ID=" & ClsContractsTransaction.TransactionTypeID)
                                                '    Dim amt As Object = 0
                                                '    If Not ClsTransactionsType.IsBasicSalary Then
                                                '        If ClsTransactionsType.TransactionGroupID <> 3 Then
                                                '            If ClsTransactionsType.Formula.Length > 0 Then
                                                '                If IsNumeric(ClsTransactionsType.Formula) Then
                                                '                    amt = ClsTransactionsType.Formula
                                                '                Else
                                                '                    clsFormula = New Clshrs_FormulaSolver(ClsEmployee.ConnectionString, Me)
                                                '                    clsFormula.EmployeeID = ClsEmployee.ID
                                                '                    clsFormula.BolBeginOfContract = True
                                                '                    clsFormula.NoOfWorkingDays = 30
                                                '                    clsFormula.NoOfDaysPerPeriod = 30
                                                '                    clsFormula.EvaluateExpression(ClsTransactionsType.Formula)
                                                '                    amt = clsFormula.Output
                                                '                End If
                                                '            Else
                                                '                amt = ClsContractsTransaction.Amount
                                                '            End If
                                                '        End If

                                                '        If amt <> ClsContractsTransaction.Amount Then
                                                '            ClsContractsTransaction.Amount = amt
                                                '            ClsContractsTransaction.Active = True
                                                '            ClsContractsTransaction.ActiveDate = ClsContractsTransaction.SetHigriDate2(row.Cells.FromKey("ActiveDate").Value, ClsContractsTransaction.ActiveDate_D)
                                                '            ClsContractsTransaction.RegComputerID = row.Cells.FromKey("ID").Value
                                                '            ClsContractsTransaction.Save()
                                                '        End If
                                                '    End If
                                                'Next
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                            Dim strcammand As String = "update hrs_EmployeeIncreases set Status = 1 where ID =" & row.Cells(0).Value
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, strcammand)
                        End If
                    Next
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Transfeer Done!/تم التحويل"))
                Case "Refund"
                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        If row.Cells.FromKey("EmployeeCode").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                            Continue For
                        End If
                        If row.Cells(1).Value = True Then
                            ClsContractsTransaction = New Clshrs_ContractsTransactions(Page)
                            ClsContractsTransaction.Find("RegComputerID = " & row.Cells(0).Value & " and TransactionTypeID in (select TransactionTypeID from hrs_EmployeesTransactionsDetails where EmpTransProjID in (select ID from hrs_EmployeesTransactionsProjects where EmployeeTransactionID in (select ID from hrs_EmployeesTransactions where FiscalYearPeriodID in (select ID from sys_FiscalYearsPeriods where hrs_ContractsTransactions.ActiveDate between sys_FiscalYearsPeriods.FromDate and sys_FiscalYearsPeriods.ToDate))))")
                            If ClsContractsTransaction.DataSet.Tables(0).Rows.Count = 0 Then
                                Dim strcammand As String = "delete from hrs_ContractsTransactions where RegComputerID =" & row.Cells(0).Value
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, strcammand)
                                strcammand = "update hrs_EmployeeIncreases set Status = 0 where ID =" & row.Cells(0).Value
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, strcammand)
                            End If
                        End If
                    Next
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Cancel Done!/تم الإلغاء"))
                Case "Save"
                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        If row.Cells.FromKey("EmployeeCode").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                            Continue For
                        End If
                        Dim strcammand As String = "set dateformat dmy; update hrs_EmployeeIncreases set EmployeeCode = '" & row.Cells.FromKey("EmployeeCode").Value & "',TransactionCode = '" & row.Cells.FromKey("TransactionCode").Value & "',Amount = '" & row.Cells.FromKey("Amount").Value & "',ActiveDate = '" & row.Cells.FromKey("ActiveDate").Value & "'  where ID =" & row.Cells(0).Value
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, strcammand)
                    Next
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Save Done!/تم الحفظ"))
                Case "Delete"
                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        If row.Cells.FromKey("EmployeeCode").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                            Continue For
                        End If
                        If row.Cells(1).Value = True Then
                            Dim strcammand As String = "delete from hrs_EmployeeIncreases where ID =" & row.Cells(0).Value
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, strcammand)
                        End If
                    Next
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Delete Done!/تم الحذف"))
            End Select
            GetData()
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Private Function"

    Private Function GetData() As Boolean
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
        Dim strcommand As String = "select ID,EmployeeCode,(isnull((select top 1 ID from hrs_Employees where code = hrs_EmployeeIncreases.EmployeeCode),0)) as RelEmployeeID,(isnull((select dbo.fn_GetEmpName(hrs_Employees.Code, " & ClsNavigationHandler.SetLanguage(Me, "1/0") & ") from hrs_Employees where code = hrs_EmployeeIncreases.EmployeeCode),hrs_EmployeeIncreases.EmployeeName)) as EmployeeName,TransactionCode,(select ID from hrs_TransactionsTypes where code = hrs_EmployeeIncreases.TransactionCode) as RelTransactionID,(isnull((select top 1 " & ClsNavigationHandler.SetLanguage(Me, "EngName/ArbName") & " from hrs_TransactionsTypes where code = hrs_EmployeeIncreases.TransactionCode)," & ClsNavigationHandler.SetLanguage(Me, "'Un Defined'/'غير محدد'") & ")) as TransactionName,Amount,ActiveDate,Status,UploadDate from hrs_EmployeeIncreases"
        Dim strFilter As String = " where EmployeeCode like '%" & txtCode.Text & "%' and Status =" & ddlFilter.SelectedValue

        Dim dsEmployee As New Data.DataSet
        dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, Data.CommandType.Text, strcommand & strFilter & " order by UploadDate,EmployeeCode")

        UwgSearchEmployees.DataSource = Nothing
        UwgSearchEmployees.DataBind()
        If dsEmployee.Tables(0).Rows.Count > 0 Then
            Dim ds As New Data.DataSet
            ds.Tables.Add()
            ds.Tables(0).Columns.Add("EmployeeCode")
            ds.Tables(0).Rows.Add(ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل"))
            ds.Tables(0).Merge(dsEmployee.Tables(0))

            UwgSearchEmployees.DataSource = ds.Tables(0)
            UwgSearchEmployees.DataBind()

            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                If row.Cells.FromKey("EmployeeCode").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                    Continue For
                End If
                If row.Cells(3).Value = 0 Or row.Cells(6).Value = 0 Then
                    row.Style.BackColor = Drawing.Color.Bisque
                End If
            Next
        End If
        If ddlFilter.SelectedValue = 1 Then
            ImageButton_Delete.Visible = False
            LinkButton_Delete.Visible = False

            ImageButton_Save.Visible = False
            LinkButton_Save.Visible = False

            ImageButton_Transfeer.Visible = False
            LinkButton_Transfeer.Visible = False

            ImageButton_Refund.Visible = True
            LinkButton_Refund.Visible = True
        Else
            ImageButton_Delete.Visible = True
            LinkButton_Delete.Visible = True

            ImageButton_Save.Visible = True
            LinkButton_Save.Visible = True

            ImageButton_Transfeer.Visible = True
            LinkButton_Transfeer.Visible = True

            ImageButton_Refund.Visible = False
            LinkButton_Refund.Visible = False
        End If
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

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        GetData()
    End Sub
End Class
