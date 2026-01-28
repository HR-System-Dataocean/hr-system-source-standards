Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeePeriodicalTransactions
    Inherits MainPage

    Private clsContracts As Clshrs_Contracts
    Private ClsContractTransactions As Clshrs_ContractsTransactions
    Private ClsEmployees As Clshrs_Employees

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim IntEmpID As Integer = Request.QueryString.Item("EmpID")
        Dim IntContID As Integer = Request.QueryString.Item("ContID")
        clsContracts = New Clshrs_Contracts(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsContracts.ConnectionString)
        If Not IsPostBack Then

            If clsContracts.Find("ID=" & IntContID) Then
                Load_ContractTrans(IntEmpID, IntContID, clsContracts.GradeStepId)

            End If
            SetFormPermission_ContractTrans("frmEmployeePeriodicalTransactions")
            uwgContractsTransactoions.Bands(0).Columns.FromKey("TransactionName").CellStyle.HorizontalAlign = IIf(ObjNavigationHandler.SetLanguage(Page, "Eng/Arb") = "Eng", HorizontalAlign.Left, HorizontalAlign.Right)
            '  WebDateTimeEdit1.NullText = Date.Now.ToString("dd/MM/yyyy")
        End If
        lblLage.Value = ObjNavigationHandler.SetLanguage(Page, "0/1")
    End Sub
    Protected Sub btnPrint_Click1(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnPrint.Click
        Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
    End Sub
    Protected Sub btnSaveAll_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click
        Dim IntContID As Integer = Request.QueryString.Item("ContID")
        ClsContractTransactions = New Clshrs_ContractsTransactions(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsContractTransactions.ConnectionString)
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgContractsTransactoions.Rows
            If (IsNothing(row.Cells.FromKey("ActiveDate").Value) And IsNothing(row.Cells.FromKey("TransactionName").Value) = False) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Active Date Required / تاريخ التفعيل مطلوب"))
                Return
            End If
        Next
        clsContracts.Find("ID=" & IntContID)
        ClsEmployees = New Clshrs_Employees(Me)
        ClsEmployees.Find1("ID=" & clsContracts.EmployeeID)
        If ClsContractTransactions.SaveContractTransacions(uwgContractsTransactoions, IntContID, False, ClsEmployees.IsSocialInsuranceIncluded) = -1 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Basic Salary Required / الراتب الأساسى مطلوب"))
            Return
        End If


        If ClsEmployees.Find1("ID=" & clsContracts.EmployeeID) Then
            If ClsEmployees.IsProjectRelated Then
                Load_ContractTrans(clsContracts.EmployeeID, IntContID, clsContracts.GradeStepId)
                Dim ClsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
                Dim ClsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
                If ClsProjectPlacementEmployees.Find("EmployeeID = " & ClsEmployees.ID & " and FromDate <= getdate() and (ToDate is null or ToDate >=  getdate())") Then
                    For Each rows As DataRow In ClsProjectPlacementEmployees.DataSet.Tables(0).Rows
                        If ClsProjectLocationDetails.Find("ID in (select LocationDetailID from hrs_ProjectPlacements where PlacementCode = '" & rows("PlacementCode") & "')") Then
                            Dim amt As Decimal = IIf(ClsProjectLocationDetails.InternalAmt > 0, ClsProjectLocationDetails.InternalAmt, ClsProjectLocationDetails.ExternalAmt)
                            If amt < Label_TAddtionsAmt.Text Then
                                Dim strCmd As String = " update hrs_ContractsTransactions set Amount = isnull(Remarks,0) where ContractID = " & IntContID & "; update hrs_ContractsTransactions set Remarks = null;"
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsContractTransactions.ConnectionString, CommandType.Text, strCmd)
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Invalid Contract Amount not More Than : / تعاقد مخالف الحد الأقصى :" & Math.Round(amt, 2)))
                                Exit Sub
                            End If
                        End If
                    Next
                End If
                If ClsProjectPlacementEmployees.Find("FromDate > getdate() and (ToDate is null or ToDate >= FromDate) and EmployeeID = " & ClsEmployees.ID & " order by FromDate ASC") Then
                    For Each rows As DataRow In ClsProjectPlacementEmployees.DataSet.Tables(0).Rows
                        If ClsProjectLocationDetails.Find("ID in (select LocationDetailID from hrs_ProjectPlacements where PlacementCode = '" & rows("PlacementCode") & "')") Then
                            Dim amt As Decimal = IIf(ClsProjectLocationDetails.InternalAmt > 0, ClsProjectLocationDetails.InternalAmt, ClsProjectLocationDetails.ExternalAmt)
                            If amt < Label_TAddtionsAmt.Text Then
                                Dim strCmd As String = " update hrs_ContractsTransactions set Amount = isnull(Remarks,0) where ContractID = " & IntContID & "; update hrs_ContractsTransactions set Remarks = null;"
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsContractTransactions.ConnectionString, CommandType.Text, strCmd)
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Invalid Next Contract Amount not More Than : / تعاقد لاحق مخالف الحد الأقصى :" & Math.Round(amt, 2)))
                                Exit Sub
                            End If
                        End If
                    Next
                End If
                Dim strCmd1 As String = " update hrs_ContractsTransactions set Remarks = null;"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsContractTransactions.ConnectionString, CommandType.Text, strCmd1)
                Venus.Shared.Web.ClientSideActions.ClosePage(Page, ObjNavigationHandler.SetLanguage(Page, "Save Complete Successfully / تم الحفظ بنجاح"))
            Else
                Dim strCmd1 As String = " update hrs_ContractsTransactions set Remarks = null;"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsContractTransactions.ConnectionString, CommandType.Text, strCmd1)
                Venus.Shared.Web.ClientSideActions.ClosePage(Page, ObjNavigationHandler.SetLanguage(Page, "Save Complete Successfully / تم الحفظ بنجاح"))
            End If
        End If
    End Sub
    Protected Sub btnGetDefault_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnGetDefault.Click
        Dim IntContID As Integer = Request.QueryString.Item("ContID")
        clsContracts = New Clshrs_Contracts(Me.Page)
        clsContracts.Find("ID=" & IntContID)
        Dim IntGradeStepId As Integer = clsContracts.GradeStepId
        Dim StrContractNo As String = clsContracts.Number
        Dim ClsGradesStepTransaction As New Clshrs_GradesStepsTransactions(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsContracts.ConnectionString)
        Dim DS As New DataSet()
        Dim intMaxID As Integer = 0
        clsContracts = New Clshrs_Contracts(Me.Page)
        clsContracts.Find(" Number = '" & StrContractNo & "'")
        intMaxID = ClsGradesStepTransaction.GetLastIDFromContractTransaction() + 1
        ClsGradesStepTransaction.GetGradesStepsTransactions(IntGradeStepId, ObjNavigationHandler.SetLanguage(Page, "0/1"), DS)
        For Each dr As Data.DataRow In DS.Tables(0).Rows
            dr("ID") = intMaxID
            intMaxID += 1
            If IsDBNull(dr("ActiveDate")) Then
                If clsContracts.ID > 0 Then
                    dr("ActiveDate") = clsContracts.StartDate
                    dr("ActiveDate_D") = "0,0"
                End If
            End If
        Next

        uwgContractsTransactoions.DataSource = DS.Tables(0).DefaultView
        uwgContractsTransactoions.DataBind()

        If lblAllowAdd.Value = 1 Then uwgContractsTransactoions.Rows.Add()
    End Sub
    Protected Sub uwgContractsTransactoions_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgContractsTransactoions.InitializeRow
        Dim IntEmpID As Integer = Request.QueryString.Item("EmpID")
        Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(Page)
        Dim ClsFrrmualResolver As New Clshrs_FormulaSolver(ClsTransactionTypes.ConnectionString, Page)
        ClsEmployees = New Clshrs_Employees(Me)
        ClsEmployees.Find1("ID=" & IntEmpID)

        If (e.Row.Cells.FromKey("ActiveDate_D").Value Is Nothing) Then
            e.Row.Cells.FromKey("ActiveDate_D").Value = "0,0"
        End If

        e.Row.Cells.FromKey("ActiveDate").Value = ClsTransactionTypes.GetHigriDate2(e.Row.Cells.FromKey("ActiveDate").Value, e.Row.Cells.FromKey("ActiveDate_D").Value)

    End Sub
    Public Function GetList_Data(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal ObjDataset As DataSet) As Boolean
        Dim ObjDataRow As DataRow
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsContracts.ConnectionString)
        Try

            DdlValues.ValueListItems.Clear()

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
    Public Function GetList_YesNo(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList) As Boolean
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsContracts.ConnectionString)
        Try
            DdlValues.ValueListItems.Clear()

            Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
            Item.DisplayText = ObjNavigationHandler.SetLanguage(Page, "No/لا")
            Item.DataValue = False
            DdlValues.ValueListItems.Add(Item)

            Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
            Item.DisplayText = ObjNavigationHandler.SetLanguage(Page, "Yes/نعم")
            Item.DataValue = True
            DdlValues.ValueListItems.Add(Item)

            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub AddValueListGrid(ByVal GridName As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal Columns As Object, ByVal ParamArray ValueText() As String)
        If IsNumeric(Columns) Then
            GridName.DisplayLayout.Bands(0).Columns(Columns).ValueList.ValueListItems.Clear()
        Else
            GridName.DisplayLayout.Bands(0).Columns.FromKey(Columns).ValueList.ValueListItems.Clear()
        End If

        For I As Integer = 0 To ValueText.Length - 1
            If IsNumeric(Columns) Then
                GridName.DisplayLayout.Bands(0).Columns(Columns).ValueList.ValueListItems.Add(I, ValueText(I))
            Else
                GridName.DisplayLayout.Bands(0).Columns.FromKey(Columns).ValueList.ValueListItems.Add(I, ValueText(I))
            End If
        Next
    End Sub

#Region "Private Function Contract Trans"
    Private Function Load_ContractTrans(ByVal IntEmployeeId As Integer, ByVal StrContractNo As String, ByVal IntGradeStepId As Integer) As Boolean
        Try
            Dim IntContractId As Integer
            Dim ClsTransaction As New Clshrs_TransactionsTypes(Me.Page)
            Dim ClsIntervals As New Clshrs_Intervals(Me.Page)
            Dim ClsCurrencies As New ClsBasicFiles(Me.Page, "sys_Currencies")
            Dim ClsEmployee As New Clshrs_Employees(Me.Page)
            Dim clsSearchsColumns = New Clssys_SearchsColumns(Me.Page)
            Dim ClsUser As New Clssys_Users(Page)

            clsContracts = New Clshrs_Contracts(Me.Page)
            ClsContractTransactions = New Clshrs_ContractsTransactions(Me.Page)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)

            Dim DsContractsTransations As New Data.DataSet
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            Dim IntLanguage As Integer = ObjNavigationHandler.SetLanguage(Page, "0/1")
            Dim IntrecordID As Integer = 0

            clsContracts.Find(" ID = '" & StrContractNo & "'")
            IntContractId = clsContracts.ID
            ClsUser.Find("ID=" & clsContracts.RegUserID)
            LblRegistereByValue.Text = ClsUser.EngName & "" & clsContracts.RegDate
            ClsUser.Find("ID=" & clsContracts.UpdatedUserID)

            LblLastUpdateValue.Text = ClsUser.EngName & "" & clsContracts.UpdateDate
            Dim clsMainCountry As New Clssys_Countries(Me.Page)
            Dim clsMainCurrency As New ClsSys_Currencies(Me.Page)
            clsMainCountry.Find(" IsMainCountries = 1 ")
            If clsMainCountry.ID > 0 Then
                clsMainCurrency.Find(" ID=" & clsMainCountry.CurrencyID)
                If Not IsNothing(clsMainCurrency.NoDecimalPlaces) Then
                    uwgContractsTransactoions.Columns.FromKey("Amount").Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgContractsTransactoions.Columns.FromKey("Amount").Format, clsMainCurrency.NoDecimalPlaces)
                End If
            End If
            ClsTransaction.Find("CancelDate IS NULL")
            ClsIntervals.Find("")

            GetList_Data(uwgContractsTransactoions.Bands(0).Columns.FromKey("TransactionName").ValueList, ClsTransaction.DataSet)
            GetList_Data(uwgContractsTransactoions.Bands(0).Columns.FromKey("IntervalID").ValueList, ClsIntervals.DataSet)
            GetList_YesNo(uwgContractsTransactoions.Bands(0).Columns.FromKey("Active").ValueList)
            GetList_YesNo(uwgContractsTransactoions.Bands(0).Columns.FromKey("OnceAtPeriod").ValueList)
            AddValueListGrid(uwgContractsTransactoions, "PaidAtVacation", ObjNavigationHandler.SetLanguage(Me, "No/لا"), ObjNavigationHandler.SetLanguage(Me, "Yes/نعم"), ObjNavigationHandler.SetLanguage(Me, "Only in vacation/فقط في الاجازة"))

            If IntEmployeeId > 0 Then
                CheckEmployeeBasicSalaryTransaction_ContractTrans(IntEmployeeId, IntContractId)
                ClsEmployee.Find1("ID=" & IntEmployeeId)
                lblDescEmployeeCode.Text = ClsEmployee.Code
                lblDescEnglishName.Text = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployee.ConnectionString, CommandType.Text, "Select dbo.fn_GetEmpName('" & ClsEmployee.Code & "'," & ObjNavigationHandler.SetLanguage(Page, "0/1") & ")")  'ClsEmployee.Name
            Else
                ClsEmployee.Find1("ID=" & IntEmployeeId)
                lblDescEmployeeCode.Text = ClsEmployee.Code
                lblDescEnglishName.Text = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployee.ConnectionString, CommandType.Text, "Select dbo.fn_GetEmpName('" & ClsEmployee.Code & "'," & ObjNavigationHandler.SetLanguage(Page, "0/1") & ")")  'ClsEmployee.Name
            End If

            LoadData_ContractTrans(IntContractId, IntLanguage)

            AddValueListGrid(uwgContractsTransactoions, "PaidAtVacation", ObjNavigationHandler.SetLanguage(Me, "No/لا"), ObjNavigationHandler.SetLanguage(Me, "Yes/نعم"), ObjNavigationHandler.SetLanguage(Me, "Only in vacation/فقط في الاجازة"))

        Catch ex As Exception

        End Try
    End Function
    Private Function CheckEmployeeBasicSalaryTransaction_ContractTrans(ByVal EmpID As Integer, ByVal intContractId As Integer) As Boolean
        Dim ClsContractsTransactions As New Clshrs_ContractsTransactions(Me.Page)
        Dim ClsTransctionTypes As New Clshrs_TransactionsTypes(Me.Page)
        Dim blnBasicSalary As Boolean = False
        Dim objNavigation As New Venus.Shared.Web.NavigationHandler(ClsTransctionTypes.ConnectionString)
        Dim objRow As Data.DataRow
        If ClsContractsTransactions.Find(" ContractID=" & intContractId & " And CancelDate Is Null") Then
            For Each objRow In ClsContractsTransactions.DataSet.Tables(0).Rows
                If ClsTransctionTypes.Find(" ID=" & objRow.Item("TransactionTypeID")) Then
                    If ClsTransctionTypes.IsBasicSalary Then
                        blnBasicSalary = True
                        Exit For
                    End If
                End If
            Next
        End If
        If blnBasicSalary = False Then
            lblNoBasicSalary.Text = objNavigation.SetLanguage(Me.Page, "This employee has no Basic Salary Transaction/لم يتم عمل بند المرتب الأساسي لهذا الموظف ")
        End If
    End Function
    Private Function LoadData_ContractTrans(ByVal IntContractId As Integer, ByVal IntLanguage As Integer) As Boolean
        Dim DsContractsTransations As New Data.DataSet

        clsContracts = New Clshrs_Contracts(Me.Page)
        clsContracts.Find("ID=" & IntContractId)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsContracts.ConnectionString)

        Label_TAddtions.Text = ObjNavigationHandler.SetLanguage(Me, "Total Addtions/إجمالى الإضافات")
        Label_TDeductions.Text = ObjNavigationHandler.SetLanguage(Me, "Total Deductions/إجمالى الخصومات")
        Label_Total.Text = ObjNavigationHandler.SetLanguage(Me, "Net Salary/صافى الراتب")
        Dim AddAmount As Decimal = 0
        Dim DedAmount As Decimal = 0
        Dim TotAmount As Decimal = 0
        Label_TAddtionsAmt.Text = AddAmount
        Label_TDeductionsAmt.Text = DedAmount
        Label_TotalAmt.Text = TotAmount

        Dim IntGradeStepId As Integer = clsContracts.GradeStepId
        ClsContractTransactions.GetContractsLastTransactions(IntContractId, IntLanguage, DsContractsTransations)
        If DsContractsTransations.Tables(0).Rows.Count = 0 Then
        Else
            uwgContractsTransactoions.DataSource = DsContractsTransations.Tables(0)
            uwgContractsTransactoions.DataBind()

            For Each dr As DataRow In DsContractsTransations.Tables(0).Rows
                Dim cls As New Clshrs_TransactionsTypes(Me.Page)
                cls.Find("ID = " & dr("TransactionTypeID"))
                If cls.IsPaid = True And dr("PaidAtVacation") <> 2 And dr("Active") <> False Then
                    If cls.Sign > 0 Then
                        AddAmount = AddAmount + dr("Amount")
                    ElseIf cls.Sign < 0 Then
                        DedAmount = DedAmount + dr("Amount")
                    End If
                End If
            Next
            Label_TAddtionsAmt.Text = Math.Round(AddAmount, 2)
            Label_TDeductionsAmt.Text = Math.Round(DedAmount, 2)
            Label_TotalAmt.Text = Math.Round(AddAmount - DedAmount, 2)
            Return True
        End If
        If IsNothing(uwgContractsTransactoions.DataSource) Then
            ClsContractTransactions.GetContractsLastTransactions(IntContractId, IntLanguage, DsContractsTransations)
            uwgContractsTransactoions.DataSource = DsContractsTransations.Tables(0)
            uwgContractsTransactoions.DataBind()

            For Each dr As DataRow In DsContractsTransations.Tables(0).Rows
                Dim cls As New Clshrs_TransactionsTypes(Me.Page)
                cls.Find("ID = " & dr("TransactionTypeID"))
                If cls.IsPaid = True And dr("PaidAtVacation") <> 2 And dr("Active") <> False Then
                    If cls.Sign > 0 Then
                        AddAmount = AddAmount + dr("Amount")
                    ElseIf cls.Sign < 0 Then
                        DedAmount = DedAmount + dr("Amount")
                    End If
                End If
            Next
            Label_TAddtionsAmt.Text = Math.Round(AddAmount, 2)
            Label_TDeductionsAmt.Text = Math.Round(DedAmount, 2)
            Label_TotalAmt.Text = Math.Round(AddAmount - DedAmount, 2)
        End If

        If DsContractsTransations.Tables(0).Rows.Count > 0 Then
            txtId.Value = DsContractsTransations.Tables(0).Rows(0).Item("ID")
        End If
    End Function
    Private Sub SetFormPermission_ContractTrans(ByVal frmCode As String)
        Dim ClsForms As New ClsSys_Forms(Me.Page)
        Dim ClsFormPermission As New ClsSys_FormsPermissions(Me.Page)
        If ClsForms.Find(" Code='" & frmCode & "'") Then
            ClsFormPermission.Find("FormID=" & ClsForms.ID)
            With ClsFormPermission
                If .ID > 0 Then
                    uwgContractsTransactoions.DisplayLayout.AllowDeleteDefault = IIf(.AllowDelete, Infragistics.WebUI.UltraWebGrid.AllowDelete.Yes, Infragistics.WebUI.UltraWebGrid.AllowDelete.No)
                    uwgContractsTransactoions.Bands(0).Columns.FromKey("btnDel").Hidden = IIf(.AllowDelete, False, True)
                    btnSave.Enabled = .AllowEdit
                    btnGetDefault.Enabled = .AllowEdit

                    If .AllowAdd And .AllowEdit Then
                        uwgContractsTransactoions.DisplayLayout.AllowAddNewDefault = IIf(.AllowAdd, Infragistics.WebUI.UltraWebGrid.AllowDelete.Yes, Infragistics.WebUI.UltraWebGrid.AllowDelete.No)
                        uwgContractsTransactoions.Rows.Add()
                        lblAllowAdd.Value = 1
                    Else
                        lblAllowAdd.Value = 0
                    End If

                    btnPrint.Enabled = .AllowPrint
                End If
            End With
        End If
    End Sub

#End Region

    Protected Sub uwgContractsTransactoions_ClickCellButton(sender As Object, e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles uwgContractsTransactoions.ClickCellButton
        clsContracts = New Clshrs_Contracts(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsContracts.ConnectionString)

        Dim id As Object = e.Cell.Row.Cells(0).Value
        If id IsNot Nothing Then
            Dim strCmd As String = " Update hrs_ContractsTransactions set CancelDate =getdate() where id=" & id
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsContracts.ConnectionString, CommandType.Text, strCmd)
        End If

        Dim IntEmpID As Integer = Request.QueryString.Item("EmpID")
        Dim IntContID As Integer = Request.QueryString.Item("ContID")
        clsContracts = New Clshrs_Contracts(Me.Page)
        If clsContracts.Find("ID=" & IntContID) Then
            Load_ContractTrans(IntEmpID, IntContID, clsContracts.GradeStepId)

        End If
        Venus.Shared.Web.ClientSideActions.ClosePage(Page, ObjNavigationHandler.SetLanguage(Page, "delete Complete Successfully / تم الحذف"))
    End Sub
End Class
