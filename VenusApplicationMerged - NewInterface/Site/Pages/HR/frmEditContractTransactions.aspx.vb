Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Partial Class frmEditContractTransactions
    Inherits MainPage
#Region "Public Decleration"
    Private ClsContractTransactions As Clshrs_ContractsTransactions
    Private clsMainOtherFields As clsSys_MainOtherFields

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim ClsEmployee As New Clshrs_Employees(Me.Page)
        Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(Me.Page)
        Dim ClsIntervals As New Clshrs_Intervals(Me.Page)
        ClsContractTransactions = New Clshrs_ContractsTransactions(Page)
        Dim clsContracts As New Clshrs_Contracts(Me.Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsEmployee.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            'DdlTransactionType.Attributes.Add("onchange", "ddlTrans_IndexChanged('" & DdlTransactionType.ID & "');")
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsContractTransactions.ConnectionString)
                ClsEmployee.AddOnChangeEventToControls("frmEditContractTransactions", Page, UltraWebTab1)

                '================================= Exit & Navigation Notification [ End ]

                Dim clsMainCountry As New Clssys_Countries(Me.Page)
                Dim clsMainCurrency As New ClsSys_Currencies(Me.Page)
                clsMainCountry.Find(" IsMainCountries = 1 ")
                If clsMainCountry.ID > 0 Then
                    clsMainCurrency.Find(" ID=" & clsMainCountry.CurrencyID)
                    If Not IsNothing(clsMainCurrency.NoDecimalPlaces) Then
                        uwgContractsTransactoions.Columns(3).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgContractsTransactoions.Columns(3).Format, clsMainCurrency.NoDecimalPlaces)
                        txtAmount.MinDecimalPlaces = clsMainCurrency.NoDecimalPlaces
                    End If
                End If

                ClsTransactionTypes.GetDropDownList(DdlTransactionType, True)
                ClsIntervals.GetDropDownList(ddlIntervalType, True)
                SelectMonthlyInterval()
                ClearFormulaDIV()
            End If
            '================================== Add DateUpdateSchedules [Start]

            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsContractTransactions.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Delete.Command
        ClsContractTransactions = New Clshrs_ContractsTransactions(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ClsContract As New Clshrs_Contracts(Page)
        Dim clsNav As New Venus.Shared.Web.NavigationHandler(ClsContractTransactions.ConnectionString)
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ClsFiscalYears As New Clssys_FiscalYearsPeriods(Page)
        Dim DtBenefits As New Data.DataTable
        Dim DtDeductions As New Data.DataTable
        Dim dblBenefits As Double = 0
        Dim dblDeduct As Double = 0
        ClsEmployees.Find("ID=" & hdnEmployeeID.Value)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, clsNav.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If ClsContractTransactions.Find("ID=" & hdnID.Value) Then
                Else
                    If AssginValues(ClsContractTransactions) Then
                        ClsContractTransactions.Save()
                    Else
                        Exit Sub
                    End If


                    Dim fisPeriodID As Integer
                    Dim fisFrom As DateTime
                    Dim fisTo As DateTime

                    Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Me)
                    ClsFisicalPeriods.GetFisicalperiodInfo(ClsContractTransactions.SetHigriDate2(wdcActiveDate.Text, wdcActiveDate.Text), fisPeriodID, fisFrom, fisTo)

                    Dim ClstransactionType As New Clshrs_TransactionsTypes(Me)
                    Dim clsFormula As Clshrs_FormulaSolver
                    Dim DSAllTransactions As Data.DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, System.Data.CommandType.Text, "select * from hrs_ContractsTransactions A where A.canceldate is null and A.TransactionTypeID <> '" & DdlTransactionType.SelectedValue & "' and A.ContractID = " & hdnContractID.Value & " and A.ActiveDate in (select top 1 B.ActiveDate from hrs_ContractsTransactions B where B.TransactionTypeID = A.TransactionTypeID and A.ContractID = B.ContractID and B.CancelDate is null order by B.ActiveDate DESC)")
                    For Each dr As Data.DataRow In DSAllTransactions.Tables(0).Rows
                        ClsContractTransactions.Find("ID=" & dr("ID"))
                        ClstransactionType.Find("ID=" & ClsContractTransactions.TransactionTypeID)
                        Dim baseFormula As String = ""
                        If ClstransactionType.ID > 0 And ClstransactionType.Formula <> "" Then
                            baseFormula = ClstransactionType.Formula
                            If ClsEmployees.IsSocialInsuranceIncluded Then
                                If ClstransactionType.HasInsuranceTiers Then
                                    Dim sTSql = "SELECT  TOP (1) BaseFormulaTiers FROM     hrs_TransactionsTypesTiers WHERE    (TransactionsTypesId = " & ClstransactionType.ID & ") AND ((MONTH(FinancialPeriodTiers) <= " & DateTime.Now.Month & " AND YEAR(FinancialPeriodTiers) = " & DateTime.Now.Year & ") or YEAR(FinancialPeriodTiers) < " & DateTime.Now.Year & " ) order by FinancialPeriodTiers desc"
                                    Dim strFormula = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, sTSql)
                                    If strFormula <> "" Then
                                        baseFormula = strFormula
                                    Else
                                        baseFormula = ClstransactionType.Formula
                                    End If
                                Else
                                    baseFormula = ClstransactionType.Formula
                                End If
                            Else
                                baseFormula = ClstransactionType.Formula
                            End If
                        End If

                        Dim amt As Object = 0
                        If Not ClstransactionType.IsBasicSalary Then
                            If ClstransactionType.TransactionGroupID <> 3 Then
                                If baseFormula.Length > 0 Then
                                    If IsNumeric(baseFormula) Then
                                        amt = baseFormula
                                    Else
                                        clsFormula = New Clshrs_FormulaSolver(ClsEmployees.ConnectionString, Me)
                                        clsFormula.Executedate = ClsContractTransactions.SetHigriDate2(wdcActiveDate.Text, ClsContractTransactions.ActiveDate_D)
                                        clsFormula.FormulaCalculated = "N"
                                        clsFormula.EmployeeID = hdnEmployeeID.Value
                                        clsFormula.BolBeginOfContract = True
                                        clsFormula.NoOfWorkingDays = 30
                                        clsFormula.NoOfDaysPerPeriod = 30
                                        clsFormula.FiscalPeriodID = fisPeriodID
                                        clsFormula.EvaluateExpression(baseFormula, 0)
                                        amt = clsFormula.Output
                                        amt = IIf(amt < 0, 0, amt)
                                    End If
                                Else
                                    amt = ClsContractTransactions.Amount
                                End If
                            Else
                                If baseFormula.Length > 0 Then
                                    If IsNumeric(baseFormula) Then
                                        amt = baseFormula
                                    Else
                                        clsFormula = New Clshrs_FormulaSolver(ClsEmployees.ConnectionString, Me)
                                        clsFormula.Executedate = ClsContractTransactions.SetHigriDate2(wdcActiveDate.Text, ClsContractTransactions.ActiveDate_D)
                                        clsFormula.FormulaCalculated = "N"
                                        clsFormula.EmployeeID = hdnEmployeeID.Value
                                        clsFormula.BolBeginOfContract = False
                                        clsFormula.NoOfWorkingDays = 30
                                        clsFormula.NoOfDaysPerPeriod = 30
                                        clsFormula.FiscalPeriodID = fisPeriodID
                                        clsFormula.EvaluateExpression(baseFormula, 0)
                                        amt = clsFormula.Output
                                        amt = IIf(amt < 0, 0, amt)
                                    End If
                                Else
                                    amt = ClsContractTransactions.Amount
                                End If
                            End If

                            If amt <> ClsContractTransactions.Amount And Not Double.IsNegativeInfinity(amt) Then
                                ClsContractTransactions.Amount = amt
                                ClsContractTransactions.Active = True
                                ClsContractTransactions.ActiveDate = ClsContractTransactions.SetHigriDate2(wdcActiveDate.Text, ClsContractTransactions.ActiveDate_D)
                                ClsContractTransactions.Save()
                            End If
                        End If
                    Next
                    Dim ObjTAmout As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContractTransactions.ConnectionString, CommandType.Text, "select [dbo].[fn_TotalPackageProject](" & hdnContractID.Value & "," & fisPeriodID & ")")
                    Dim ClsContracts As New Clshrs_Contracts(Page)
                    ClsContracts.Find("ID = " & hdnContractID.Value)
                    ClsEmployees = New Clshrs_Employees(Me)
                    If ClsEmployees.Find("ID=" & hdnEmployeeID.Value) Then
                        If ClsEmployees.IsProjectRelated Then
                            Dim ClsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
                            Dim ClsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
                            If ClsProjectPlacementEmployees.Find("EmployeeID = " & ClsEmployees.ID & " and FromDate <= convert(Datetime,'" & ClsContractTransactions.SetHigriDate2(wdcActiveDate.Text, ClsContractTransactions.ActiveDate_D) & "') and (ToDate is null or ToDate >=  convert(Datetime,'" & ClsContractTransactions.SetHigriDate2(wdcActiveDate.Text, ClsContractTransactions.ActiveDate_D) & "'))") Then
                                For Each rows As DataRow In ClsProjectPlacementEmployees.DataSet.Tables(0).Rows
                                    If ClsProjectLocationDetails.Find("ID in (select LocationDetailID from hrs_ProjectPlacements where PlacementCode = '" & rows("PlacementCode") & "')") Then
                                        Dim amt As Decimal = IIf(ClsProjectLocationDetails.InternalAmt > 0, ClsProjectLocationDetails.InternalAmt, ClsProjectLocationDetails.ExternalAmt)
                                        If amt < ObjTAmout Then
                                            Dim strCmd As String = " set dateformat dmy; delete from hrs_ContractsTransactions where ContractID = " & hdnContractID.Value & " and CONVERT(VARCHAR(10),ActiveDate,103) =  CONVERT(VARCHAR(10),convert(Datetime,'" & ClsContractTransactions.SetHigriDate2(wdcActiveDate.Text, ClsContractTransactions.ActiveDate_D) & "'),103);"
                                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsContractTransactions.ConnectionString, CommandType.Text, strCmd)
                                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, clsNav.SetLanguage(Page, "Invalid Contract Amount not More Than : / تعاقد مخالف الحد الأقصى :" & Math.Round(amt, 2)))
                                            Exit Sub
                                        End If
                                    End If
                                Next
                            End If
                            If ClsProjectPlacementEmployees.Find("FromDate > convert(Datetime,'" & ClsContractTransactions.SetHigriDate2(wdcActiveDate.Text, ClsContractTransactions.ActiveDate_D) & "') and (ToDate is null or ToDate >= FromDate) and EmployeeID = " & ClsEmployees.ID & " order by FromDate ASC") Then
                                For Each rows As DataRow In ClsProjectPlacementEmployees.DataSet.Tables(0).Rows
                                    If ClsProjectLocationDetails.Find("ID in (select LocationDetailID from hrs_ProjectPlacements where PlacementCode = '" & rows("PlacementCode") & "')") Then
                                        Dim amt As Decimal = IIf(ClsProjectLocationDetails.InternalAmt > 0, ClsProjectLocationDetails.InternalAmt, ClsProjectLocationDetails.ExternalAmt)
                                        If amt < ObjTAmout Then
                                            Dim strCmd As String = " set dateformat dmy;  delete from hrs_ContractsTransactions where ContractID = " & hdnContractID.Value & " and CONVERT(VARCHAR(10),ActiveDate,103) =  CONVERT(VARCHAR(10),convert(Datetime,'" & ClsContractTransactions.SetHigriDate2(wdcActiveDate.Text, ClsContractTransactions.ActiveDate_D) & "'),103);"
                                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsContractTransactions.ConnectionString, CommandType.Text, strCmd)
                                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, clsNav.SetLanguage(Page, "Invalid Next Contract Amount not More Than : / تعاقد لاحق مخالف الحد الأقصى :" & Math.Round(amt, 2)))
                                            Exit Sub
                                        End If
                                    End If
                                Next
                            End If
                            DdlTransactionType_SelectedIndexChanged(Nothing, Nothing)
                            Venus.Shared.Web.ClientSideActions.ClosePage(Page, clsNav.SetLanguage(Page, "Save Complete Successfully / تم الحفظ بنجاح"))
                        Else
                            DdlTransactionType_SelectedIndexChanged(Nothing, Nothing)
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, clsNav.SetLanguage(Page, " Save Done /تم الحفظ"))
                        End If
                    End If
                End If
            Case "New"
                SetToolBarDefaults()
                SetToolBarPermission(Me, ClsContractTransactions.ConnectionString, ClsContractTransactions.DataBaseUserRelatedID, ClsContractTransactions.GroupID, "N")
                ClearContractTransaction()
            Case "Delete"
                If hdnID.Value > 0 Then
                    ClsContractTransactions.Delete("ID=" & hdnID.Value)
                    DdlTransactionType_SelectedIndexChanged(Nothing, Nothing)
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, _
                                                           clsNav.SetLanguage(Page, "No Found Transaction/لايوجد بند"))
                End If
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsContractTransactions.Table
                ClsContractTransactions.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsContractTransactions.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsContractTransactions.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        Dim clsEmployee As New Clshrs_Employees(Page)
        Dim ClsContracts As New Clshrs_Contracts(Page)

        If txtCode.Text.Trim <> String.Empty Then
            If clsEmployee.Find("Code='" & txtCode.Text.Trim & "'") Then
                'Set Employee Full Name
                lblEmpName.Text = clsEmployee.FullName
                'Set Hidden Field with Employee ID
                hdnEmployeeID.Value = clsEmployee.ID
                'Get Employee Contracts
                ClsContracts.Find(" EmployeeId = " & clsEmployee.ID & " And Isnull(CancelDate,'')= '' Order By StartDate ")
                uwgContracts.DataSource = ClsContracts.DataSet.Tables(0).DefaultView
                uwgContracts.DataBind()
                'Select Last Contract
                If ClsContracts.DataSet.Tables(0).Rows.Count > 0 Then
                    uwgContracts.Rows(ClsContracts.DataSet.Tables(0).Rows.Count - 1).Activate()
                    uwgContracts.Rows(ClsContracts.DataSet.Tables(0).Rows.Count - 1).Selected = True
                    hdnContractID.Value = _
                        uwgContracts.Rows(ClsContracts.DataSet.Tables(0).Rows.Count - 1).Cells.FromKey("ID").Value
                End If
                'Clear Transaction Controls And Grid Of Active Dates
                ClearContractTransaction()
            Else
                Clear()
            End If
        Else
            Clear()
        End If

    End Sub
    Protected Sub uwgContracts_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgContracts.InitializeRow
        Dim ClsContracts As New Clshrs_Contracts(Page)
        e.Row.Cells.FromKey("StartDate").Value = ClsContracts.GetHigriDate(e.Row.Cells.FromKey("StartDate").Value)
        e.Row.Cells.FromKey("EndDate").Value = ClsContracts.GetHigriDate(e.Row.Cells.FromKey("EndDate").Value)
    End Sub

    Protected Sub uwgContractsTransactoions_ActiveCellChange(sender As Object, e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles uwgContractsTransactoions.ActiveCellChange
        Try
            If e.Cell.Row.Cells.FromKey("ID").Value <> Nothing Then
                hdnID.Value = e.Cell.Row.Cells.FromKey("ID").Value
                If ClsContractTransactions.Find("ID=" & hdnID.Value) Then
                    With ClsContractTransactions
                        txtAmount.Text = .Amount
                        wdcActiveDate.Value = .SetHigriDate(.ActiveDate)
                        DdlActive.SelectedValue = .Active
                        DdlTransactionType.SelectedValue = .TransactionTypeID
                        ddlPaidAtVacation.SelectedValue = .PaidAtVacation
                        ddlOncePerPeriod.SelectedValue = .OnceAtPeriod
                        ddlIntervalType.SelectedValue = .IntervalID

                        ImageButton_Delete.Enabled = True
                    End With
                End If
            Else
                hdnID.Value = 0
                ImageButton_Delete.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DdlTransactionType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlTransactionType.SelectedIndexChanged
        Dim ClsContractTransaction As New Clshrs_ContractsTransactions(Page)

        GetFormulaDesc(DdlTransactionType.SelectedValue)

        If ClsContractTransaction.Find("ContractID=" & hdnContractID.Value & _
                                       " And TransactionTypeID=" & DdlTransactionType.SelectedValue & _
                                       " And CancelDate Is Null ") Then

            uwgContractsTransactoions.DataSource = ClsContractTransaction.DataSet
            uwgContractsTransactoions.DataBind()
        Else
            uwgContractsTransactoions.DataSource = Nothing
            uwgContractsTransactoions.DataBind()
        End If

        ClearContractTransaction(ClearTransactionType.No)
    End Sub
    Protected Sub uwgContractsTransactoions_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgContractsTransactoions.InitializeRow
        Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(Page)
        Dim ClsFrrmualResolver As New Clshrs_FormulaSolver(ClsTransactionTypes.ConnectionString, Page)
        Dim IntEmployeeId As Integer = hdnEmployeeID.Value
        If (e.Row.Cells.FromKey("ActiveDate_D").Value Is Nothing) Then
            e.Row.Cells.FromKey("ActiveDate_D").Value = "0,0"
        End If

        e.Row.Cells.FromKey("ActiveDate").Value = Convert.ToString(ClsTransactionTypes.GetHigriDate2 _
                                            (e.Row.Cells.FromKey("ActiveDateT").Value,
                                             e.Row.Cells.FromKey("ActiveDate_D").Value)).Replace("/", "")
    End Sub


#End Region

#Region "Private Functions"

    Private Function SetNew() As Boolean
        UltraWebTab1.SelectedTab = 0

        txtAmount.Text = 1
        lblFormula.Text = ""
        lblFormulaDesc.Text = ""
        DdlActive.SelectedIndex = 0
        ddlIntervalType.SelectedIndex = 0
        ddlOncePerPeriod.SelectedIndex = 0
        ddlPaidAtVacation.SelectedIndex = 0
        DdlTransactionType.SelectedIndex = 0
        DdlTransactionType.Enabled = True

        lblRegUserValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
        ImageButton_Delete.Enabled = False
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
    Private Function AssginValues(ByVal ClsContractTransactions As Clshrs_ContractsTransactions) As Boolean
        Dim clsNav As New Venus.Shared.Web.NavigationHandler(ClsContractTransactions.ConnectionString)
        Dim strCmd As String = "Set DateFormat DMY; Select * from " & ClsContractTransactions.Table & " Where ID <> " & hdnID.Value & _
                               " And ActiveDate = '" & ClsContractTransactions.SetHigriDate2(wdcActiveDate.Text, "") & "' " & _
                               " And TransactionTypeID = " & DdlTransactionType.SelectedValue & _
                               " And CancelDate Is Null" & _
                               " And ContractID = " & hdnContractID.Value
        If Not hdnContractID.Value > 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, _
                                                            clsNav.SetLanguage(Page, "No Contract Selected/لم يتم اختيار عقد"))
            Return False
        End If
        Dim dsResult As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset( _
                                ClsContractTransactions.ConnectionString, CommandType.Text, strCmd)
        If dsResult.Tables(0).Rows.Count > 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, _
                                                            clsNav.SetLanguage(Page, "Found Transaction With Same Active Date/يوجد بند بنفس تاريخ التفعيل"))
            Return False
        End If

        With ClsContractTransactions
            .ContractID = hdnContractID.Value
            .TransactionTypeID = DdlTransactionType.SelectedValue
            .Amount = txtAmount.Value
            .Active = DdlActive.SelectedValue
            .IntervalID = ddlIntervalType.SelectedValue
            .PaidAtVacation = ddlPaidAtVacation.SelectedValue
            .OnceAtPeriod = ddlOncePerPeriod.SelectedValue
            .ActiveDate = ClsContractTransactions.SetHigriDate2(wdcActiveDate.Text, .ActiveDate_D)
        End With
        Return True
    End Function
    Private Function SelectMonthlyInterval() As Boolean
        Dim ClsInterval As New Clshrs_Intervals(Me.Page)
        Dim item As New ListItem()
        If (ddlIntervalType.Items.Count > 1) Then
            For Each item In ddlIntervalType.Items
                ClsInterval.Find(" ID=" & Val(item.Value))
                If ClsInterval.Number = 1 Then
                    ddlIntervalType.SelectedValue = item.Value
                    Exit For
                End If
            Next
        End If
    End Function
    Enum ClearTransactionType
        No = 0
        Yes = 1
    End Enum
    Private Sub ClearContractTransaction(Optional ByVal ClearTransType As ClearTransactionType = ClearTransactionType.Yes)
        Dim clsDAL As New ClsDataAcessLayer(Page)
        hdnID.Value = 0
        If ClearTransType = ClearTransactionType.Yes Then
            DdlTransactionType.SelectedIndex = 0
            uwgContractsTransactoions.DataSource = Nothing
            uwgContractsTransactoions.DataBind()
            ClearFormulaDIV()
        End If
        txtAmount.Value = 0
        SelectMonthlyInterval()
        wdcActiveDate.Value = clsDAL.GetHigriDate2(Date.Now, "")
        ddlOncePerPeriod.SelectedValue = 0
        ddlPaidAtVacation.SelectedValue = 0
        DdlActive.SelectedValue = 1
        lblRegUserValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
        ImageButton_Delete.Enabled = False
    End Sub
    Private Sub ClearFormulaDIV()
        lbFormula.Text = String.Empty
        lblFormula.Text = String.Empty
        lbFormulaDesc.Text = String.Empty
        lblFormulaDesc.Text = String.Empty

    End Sub
    Private Sub GetFormulaDesc(ByVal intTransTypeID As Integer)
        Dim clsDAL As New ClsDataAcessLayer(Page)
        Dim clsNav As New Venus.Shared.Web.NavigationHandler(clsDAL.ConnectionString)
        Dim strLang As String = clsNav.SetLanguage(Page, "Eng/Arb")

        Dim ClsEmployees As New Clshrs_Employees(Page)
        ClsEmployees = New Clshrs_Employees(Page)
        ClsEmployees.Find("ID=" & hdnEmployeeID.Value)
        Dim strRet As String = GetFormulaVlaue(intTransTypeID, strLang, ClsEmployees.IsSocialInsuranceIncluded)
        If (Not IsNothing(strRet)) AndAlso strRet <> "=" AndAlso strRet <> "" Then
            Dim strArr As String() = strRet.Split("=")
            If strLang = "Arb" Then
                lbFormula.Text = "المعادلة"
                lbFormulaDesc.Text = "الوصف"
            Else
                lbFormula.Text = "Formula"
                lbFormulaDesc.Text = "Description"
            End If
            lblFormula.Text = strArr(0)
            lblFormulaDesc.Text = strArr(1)

        Else
            ClearFormulaDIV()
        End If
    End Sub
    Public Function SetToolBarPermission(ByVal pgSender As System.Web.UI.Page, ByVal ConnectionString As String, ByVal UserID As Integer, ByVal GroupID As Integer, ByVal Mode As String) As Boolean
        Dim StrCommandStored As String
        Dim StrFormName As String
        Dim ObjDataSet As New Data.DataSet
        Try
            StrFormName = pgSender.Form.ID
            StrCommandStored = "hrs_GetFormsPermissions"
            ObjDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, StrCommandStored, UserID, GroupID, StrFormName)
            If Venus.Shared.DataHandler.CheckValidDataObject(ObjDataSet) Then
                With ObjDataSet.Tables(0).Rows(0)
                    ImageButton_Delete.Enabled = .Item("AllowDelete")
                    ImageButton_Print.Enabled = .Item("AllowPrint")
                    Select Case Mode
                        Case "N", "R"
                            ImageButton_Save.Enabled = .Item("AllowAdd")
                            ImageButton_SaveN.Enabled = .Item("AllowAdd")
                            LinkButton_SaveN.Enabled = .Item("AllowAdd")
                        Case "E"
                            ImageButton_Save.Enabled = .Item("AllowEdit")
                            ImageButton_SaveN.Enabled = .Item("AllowEdit")
                            LinkButton_SaveN.Enabled = .Item("AllowAdd")
                    End Select
                End With
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function SetToolBarRecordPermission(ByVal pgSender As System.Web.UI.Page, ByVal ConnectionString As String, ByVal UserID As Integer, ByVal GroupID As Integer, ByVal StrTableName As String, ByVal RecordID As Integer) As Boolean
        Dim StrCommandStored As String
        Dim StrFormName As String
        Dim ObjDataSet As New Data.DataSet
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Try
            StrFormName = pgSender.Form.ID
            StrCommandStored = "hrs_GetRecordsPermissions"
            ObjDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, StrCommandStored, UserID, GroupID, Replace(StrTableName, " ", ""), RecordID)
            If Venus.Shared.DataHandler.CheckValidDataObject(ObjDataSet) Then
                With ObjDataSet.Tables(0).Rows(0)

                    If ImageButton_Save.Enabled = True And .Item("CanEdit") = True Then
                        ImageButton_Save.Enabled = Not .Item("CanEdit")
                        ImageButton_SaveN.Enabled = Not .Item("CanEdit")
                        LinkButton_SaveN.Enabled = Not .Item("CanEdit")
                    End If

                    If ImageButton_Delete.Enabled = True And .Item("CanDelete") = True Then
                        ImageButton_Delete.Enabled = Not .Item("CanDelete")
                    End If

                    If ImageButton_Print.Enabled = True And .Item("CanPrint") = True Then
                        ImageButton_Print.Enabled = Not .Item("CanPrint")
                    End If
                End With
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SetToolbarSetting(ByVal ptrType As String, ByVal ClsClass As Object, ByVal intID As Integer) As Boolean
        Try
            Select Case ptrType
                Case "N", "R"
                    txtCode.Text = String.Empty
                    ImageButton_Delete.Enabled = False

                Case "D"
                    ClsContractTransactions.Find("ID=" & intID)
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsContractTransactions.Find("ID=" & intID)
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsContractTransactions
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                    ImageButton_Delete.Enabled = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation() As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsContractTransactions = New Clshrs_ContractsTransactions(Me)
        Try
            With ClsContractTransactions
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Page, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsContractTransactions = New Clshrs_ContractsTransactions(Me)
        If IntId > 0 Then
            ClsContractTransactions.Find("ID=" & IntId)

        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Sub Clear()
        txtCode.Text = String.Empty
        lblEmpName.Text = String.Empty
        uwgContracts.DataSource = Nothing
        uwgContracts.DataBind()
        hdnEmployeeID.Value = 0
        hdnContractID.Value = 0
        ClearContractTransaction()
    End Sub
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsContractTransactions = New Clshrs_ContractsTransactions(Page)
        ClsContractTransactions.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsContractTransactions.ID
        If (recordID > 0) Then
            Dim clsForms As New ClsSys_Forms(Page)
            clsForms.Find(" code = REPLACE('" & formName & "',' ','')")
            Dim clsFormsControls As New Clssys_FormsControls(Page)
            clsFormsControls.Find(" FormID=" & clsForms.ID)
            Dim tab As Data.DataTable = clsFormsControls.DataSet.Tables(0).Copy()
            For Each row As Data.DataRow In tab.Rows
                clsFormsControls.Find(" FormID=" & clsForms.ID & " And Name='" & row("Name") & "'")
                Dim sys_Fields As New Clssys_Fields(Page)
                sys_Fields.Find(" ID=" & clsFormsControls.FieldID)
                If (sys_Fields.FieldName.Trim() = "Code" Or sys_Fields.FieldName.Trim() = "Number" Or sys_Fields.FieldName.Trim() = "ID") Then
                    Continue For
                End If
                Dim currCtrl As Control = Me.FindControl(row("Name"))
                Dim bIsArabic As Boolean = IIf(IsDBNull(row("IsArabic")), False, row("IsArabic"))
                If (bIsArabic Or row("Name").ToString.ToLower.IndexOf("arb") > -1) And (TypeOf (currCtrl) Is TextBox) Then
                    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedulesForArabicText(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                ElseIf (TypeOf (currCtrl) Is TextBox) Then
                    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebSchedule.WebDateChooser) Then
                    CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                End If
            Next
        End If
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
    Public Shared Function GetFormulaVlaue(ByVal strID As String, ByVal lang As String, ByVal IsSocialInsuranceIncluded As Boolean) As String
        Dim IntIndex As Integer = 0
        Dim StrCode As String = String.Empty
        Dim StrFormulaValue As String = ""
        Dim Strformula As String = ""
        Dim objkey As Object
        Dim ds As New DataSet

        Dim arrEngCode As New System.Collections.SortedList()
        Dim arrArbCode As New System.Collections.SortedList()
        arrArbCode("<@NDPP@>") = "	عدد الأيام في الفترة المالية"
        arrArbCode("<@NHPP@>") = "	عدد الساعات في الفترة المالية"
        arrArbCode("<@WHPD@>") = "	عدد ساعات العمل في اليوم"
        arrArbCode("<@OVF@>") = "	عامل الوقت الاضافي"
        arrArbCode("<@HOF@>") = "	عامل الأجازة"
        arrArbCode("<@WUPP@>") = "  وحدات العمل في الفترة المالية"
        arrArbCode("<@OHPP@>") = "	 ساعات العمل الاضافي في الفترة المالية "
        arrArbCode("<@OHPH@>") = " ساعات العمل الاضافي خلال الساعة"
        arrArbCode("<@HUPP@>") = "	وحدات الأحازة في الفترة المالية"
        arrArbCode("<@SPPH@>") = "	المرتب في الساعة"
        arrArbCode("<@SPPD@>") = "	المرتب في اليوم"
        arrArbCode("<@BEOC@>") = "	بداية / نهاية العقد"
        arrArbCode("<@PWU@>") = "	وحدات العمل للمشروع"
        arrArbCode("<@TPWU@>") = "	وحدات العمل للمشاريع"
        arrArbCode("<@RUPP@>") = "	الفارق بين التعاقد والفترة المالية"


        arrEngCode("<@NDPP@>") = "	No. of Days Per Period"
        arrEngCode("<@NHPP@>") = "	No. of Hours Per Period"
        arrEngCode("<@WHPD@>") = "	No. of Work Hours Per Day"
        arrEngCode("<@OVF@>") = "	Overtime Factor"
        arrEngCode("<@HOF@>") = "	Holiday Factor"
        arrEngCode("<@WUPP@>") = "	Working units per period"
        arrEngCode("<@OHPP@>") = "	Overtime hours per period"
        arrEngCode("<@OHPH@>") = "	Overtime hours per Hour"
        arrEngCode("<@HUPP@>") = "	Holidays units per period"
        arrEngCode("<@SPPH@>") = "	Salary Price Per Hour"
        arrEngCode("<@SPPD@>") = "	Salary Price Per Day"
        arrEngCode("<@BEOC@>") = "	Begin / End of Contract"
        arrArbCode("<@PWU@>") = " Project working Units"
        arrArbCode("<@TPWU@>") = "	Projects Working Units"
        arrArbCode("<@RUPP@>") = "	Ration Start Contract And Prepare"
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        If Find("hrs_TransactionsTypes", " ID=" & strID, ds) Then
            StrFormulaValue = IIf(IsDBNull(ds.Tables(0).Rows(0).Item("Formula")), "", ds.Tables(0).Rows(0).Item("Formula"))

            If ds.Tables(0).Rows(0).Item("ID") > 0 Then
                If IsSocialInsuranceIncluded Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("HasInsuranceTiers")) AndAlso
   CBool(ds.Tables(0).Rows(0).Item("HasInsuranceTiers")) Then
                        Dim sTSql = "SELECT  TOP (1) BaseFormulaTiers FROM     hrs_TransactionsTypesTiers WHERE    (TransactionsTypesId = " & ds.Tables(0).Rows(0).Item("ID") & ") AND ((MONTH(FinancialPeriodTiers) <= " & DateTime.Now.Month & " AND YEAR(FinancialPeriodTiers) = " & DateTime.Now.Year & ") or YEAR(FinancialPeriodTiers) < " & DateTime.Now.Year & " ) order by FinancialPeriodTiers desc"
                        Dim myFormula = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sTSql)
                        If myFormula <> "" Then
                            StrFormulaValue = myFormula

                        End If

                    End If

                End If
            End If


            Strformula = StrFormulaValue
            If (StrFormulaValue <> "") Then
                For Each objkey In arrEngCode.Keys
                    If StrFormulaValue.Contains(objkey.ToString) Then
                        If (lang = "Eng") Then
                            StrFormulaValue = StrFormulaValue.Replace(objkey.ToString(), arrEngCode(objkey))
                        Else
                            StrFormulaValue = StrFormulaValue.Replace(objkey.ToString(), arrArbCode(objkey))
                        End If
                    End If
                Next
            End If
            While True
                IntIndex = StrFormulaValue.IndexOf("$")
                If IntIndex > -1 Then
                    StrFormulaValue = StrFormulaValue.Remove(IntIndex - 1, 2)
                    StrCode = StrFormulaValue.Substring(IntIndex - 1, StrFormulaValue.IndexOf("$") - (IntIndex - 1))
                    StrFormulaValue = StrFormulaValue.Remove(StrFormulaValue.IndexOf("$"), 2)
                    StrFormulaValue = StrFormulaValue.Replace(StrCode, GetDesc(StrCode, lang))
                Else
                    Exit While
                End If
            End While

            Return Strformula & "=" & StrFormulaValue
        End If
        Return ""
    End Function
    Public Shared Function GetDesc(ByVal StrCode As String, ByVal lang As String) As String
        Dim StrDesc As String = ""
        Dim ds As New DataSet

        If Find("hrs_TransactionsTypes", " Code='" & StrCode & "'", ds) Then
            With ds.Tables(0).Rows(0)
                StrDesc = IIf(lang = "Eng", .Item("EngName"), .Item("ArbName"))

                If (StrDesc = "") Then
                    StrDesc = IIf(lang = "Eng", .Item("ArbName"), .Item("EngName"))
                End If

            End With
        End If

        Return StrDesc
    End Function

#End Region

End Class
