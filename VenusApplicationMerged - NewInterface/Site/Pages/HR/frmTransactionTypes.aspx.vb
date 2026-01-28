Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmTransactionTypes
    Inherits MainPage
#Region "Public Decleration"
    Private ClsTransactions As Clshrs_TransactionsTypes
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsTransactions = New Clshrs_TransactionsTypes(Me.Page)
        Dim ClsTransactionGroups As New Clshrs_TransactionGroups(Page)
        Dim objNavigation As New Venus.Shared.Web.NavigationHandler(ClsTransactionGroups.ConnectionString)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsTransactions.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("Lang", objNavigation.SetLanguage(Page, "Eng/Arb"))
                Page.Session.Add("ConnectionString", ClsTransactions.ConnectionString)
                ClsTransactions.AddOnChangeEventToControls("frmTransactionTypes", Page, UltraWebTab1)
                ClsTransactionGroups.GetDropDownList(DdlTransactionCategory, True)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtShortArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

                txtFormula.Attributes.Add("onblur", "txtFormula_TextChanged();")
                txtBeginFormula.Attributes.Add("onblur", "txtBeginFormula_TextChanged();")
                txtEndFormula.Attributes.Add("onblur", "txtEndFormula_TextChanged();")
                txtCode.Attributes.Add("onKeyUp", "Open_Search_KeyDown(" & " " & SearchID & " " & ",""txtCode"")")
                btnFormula.Attributes.Add("onclick", "Open_Formula_Screen('" & txtFormula.ID & "')")
                btnBeginFormula.Attributes.Add("onclick", "Open_Formula_Screen('" & txtBeginFormula.ID & "')")
                btnEndFormula.Attributes.Add("onclick", "Open_Formula_Screen('" & txtEndFormula.ID & "')")
                UwgSearchEmployees.Visible = False
                AddNewRow()
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsTransactions.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsTransactions.ID
                If (IntrecordID > 0) Then
                    SetScreenInformation("E")
                Else
                    SetScreenInformation("N")
                End If
            Else
                SetScreenInformation("N")
            End If
            CreateOtherFields(IntrecordID)
            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsTransactions.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsTransactions = New Clshrs_TransactionsTypes(Me.Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsTransactions.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If chkIsBasicSalary.Checked And chkIsEndOfService.Checked Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Transaction Can not be BasicSalary And End of Service / لا يمكن للبند أن يكون راتب أساسي ونهاية خدمة"))
                Else
                    SavePart()
                    AfterOperation()
                End If
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If chkIsBasicSalary.Checked And chkIsEndOfService.Checked Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Transaction Can not be BasicSalary And End of Service / لا يمكن للبند أن يكون راتب أساسي ونهاية خدمة"))
                Else
                    If SavePart() Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                    End If
                End If
            Case "New"

                AfterOperation()
            Case "Delete"
                ClsTransactions.Find("Code='" & txtCode.Text & "'")
                If (ClsTransactions.ID > 0) Then
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Transaction Type Not Found/نوع الحركة غير موجود"))
                    Exit Sub
                End If
                ClsTransactions.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                If ClsTransactions.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsTransactions.ID & "&TableName=" & ClsTransactions.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                End If
            Case "Remarks"
                If ClsTransactions.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsTransactions.ID & "&TableName=" & ClsTransactions.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                End If

            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"

            Case "Exit"

            Case "First"
                ClsTransactions.Find("Code='" & txtCode.Text & "'")
                ClsTransactions.FirstRecord()
                GetValues(ClsTransactions)
            Case "Previous"
                ClsTransactions.Find("Code='" & txtCode.Text & "'")
                If Not ClsTransactions.previousRecord() Then
                    ClsTransactions.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))

                End If
                GetValues(ClsTransactions)
            Case "Next"
                ClsTransactions.Find("Code='" & txtCode.Text & "'")
                If Not ClsTransactions.NextRecord() Then
                    ClsTransactions.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))

                End If
                GetValues(ClsTransactions)
            Case "Last"
                ClsTransactions.Find("Code='" & txtCode.Text & "'")
                ClsTransactions.LastRecord()
                GetValues(ClsTransactions)
        End Select
        chkHasInsuranceTiers_TextChanged(Nothing, Nothing)
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        UwgSearchEmployees.DataSource = Nothing
        UwgSearchEmployees.DataBind()
        UwgSearchEmployees.Rows.Add()
        CheckCode()
    End Sub
    Protected Sub chkHasInsuranceTiers_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHasInsuranceTiers.CheckedChanged
        If chkHasInsuranceTiers.Checked Then
            UwgSearchEmployees.Visible = True
        Else
            UwgSearchEmployees.Visible = False
        End If
    End Sub
    Protected Sub uwgFiscalYearsPeriods_ActiveRowChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles UwgSearchEmployees.ActiveRowChange
        e.Row.Cells.FromKey("SerialNumberTiers").Value = (e.Row.Index + 1).ToString()
    End Sub



#End Region

#Region "Private Functions"
    Private Function AddNewRow() As Boolean
        Try


            UwgSearchEmployees.DataSource = Nothing
            UwgSearchEmployees.DataBind()

            UwgSearchEmployees.Rows.Add()


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SavePart() As Boolean
        Try
            Dim StrMode As String '= Request.QueryString.Item("Mode")
            ClsTransactions = New Clshrs_TransactionsTypes(Page)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsTransactions.ConnectionString)
            ClsTransactions.Find("Code='" & txtCode.Text & "'")
            If Not DdlTransactionCategory.SelectedIndex = 0 Then
                If Not AssignValue(ClsTransactions) Then
                    Exit Function
                End If
                If chkIsBasicSalary.Checked Then
                    If Not ClsTransactions.IsEndOfService = True Then
                        ClsTransactions.IsBasicSalary = True
                        If ClsTransactions.ID > 0 Then
                            ClsTransactions.Update("Code='" & txtCode.Text & "'")
                            ClsTransactions.UpdateIsBasicSalary(" Code <> '" & txtCode.Text & "'")
                        Else
                            ClsTransactions.Save()
                            ClsTransactions.UpdateIsBasicSalary(" Code <> '" & txtCode.Text & "'")
                        End If
                        'E#001
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        ClsTransactions.Find(" Code='" & txtCode.Text & "'")
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsTransactions.Table, ClsTransactions.ID)
                        value.Text = ""
                    Else
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Transaction Can not be BasicSalary And End of Service / لا يمكن للبند أن يكون راتب أساسي ونهاية خدمة"))
                        Exit Function
                    End If
                ElseIf chkIsEndOfService.Checked Then
                    If Not ClsTransactions.IsBasicSalary = True Then
                        ClsTransactions.IsEndOfService = True

                        If ClsTransactions.ID > 0 Then
                            ClsTransactions.Update("Code='" & txtCode.Text & "'")
                        Else
                            ClsTransactions.Save()
                        End If
                        'E#001
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        ClsTransactions.Find("Code='" & txtCode.Text & "'")
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsTransactions.Table, ClsTransactions.ID)
                        value.Text = ""
                    Else
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Transaction Can not be BasicSalary And End of Service / لا يمكن للبند أن يكون راتب أساسي ونهاية خدمة"))
                        Exit Function
                    End If
                Else
                    If ClsTransactions.ID > 0 Then
                        ClsTransactions.Update("Code='" & txtCode.Text & "'")
                    Else
                        ClsTransactions.Save()
                    End If
                    'E#001
                    clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                    ClsTransactions.Find("Code='" & txtCode.Text & "'")
                    clsMainOtherFields.CollectDataAndSave(value.Text, ClsTransactions.Table, ClsTransactions.ID)
                    value.Text = ""
                End If
                SaveGrid()
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Please Select Transactions Category / برجاء اختيار نوع الحركة المالية")
                Venus.Shared.Web.ClientSideActions.SetFocus(Page, DdlTransactionCategory, True)
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetGrid() As Boolean
        UwgSearchEmployees.DataSource = Nothing
        UwgSearchEmployees.DataBind()
        UwgSearchEmployees.Rows.Add()
        'Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        ClsTransactions = New Clshrs_TransactionsTypes(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsTransactions.ConnectionString)
        ClsTransactions.Find("Code='" & txtCode.Text & "'")
        If ClsTransactions.ID > 0 Then
            Dim strselect2 As String
            strselect2 = "select [SerialNumberTiers],[FinancialPeriodTiers],[BaseFormulaTiers],[BeginFormulaTiers],[EndFormulaTiers] from hrs_TransactionsTypesTiers where TransactionsTypesId=" & ClsTransactions.ID
            Dim DSOfficialvacations As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect2)
            If DSOfficialvacations.Tables(0).Rows.Count > 0 Then
                UwgSearchEmployees.DataSource = DSOfficialvacations.Tables(0)
                UwgSearchEmployees.DataBind()
                UwgSearchEmployees.Rows.Add()
            End If
        End If


    End Function
    Private Function SaveGrid() As Boolean
        ClsTransactions = New Clshrs_TransactionsTypes(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsTransactions.ConnectionString)
        ClsTransactions.Find("Code='" & txtCode.Text & "'")
        If ClsTransactions.ID > 0 Then

            Dim SqlCommand As String = ""
            Dim Deletecommand As String = "Delete from hrs_TransactionsTypesTiers where TransactionsTypesId='" & ClsTransactions.ID & "' "
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsTransactions.ConnectionString, Data.CommandType.Text, Deletecommand)
            'clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
            Dim LineNumber = 0
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                If Not IsNothing(DGRow.Cells("3").Value) Then
                    LineNumber = LineNumber + 1
                    SqlCommand &= " Set DateFormat DMY Insert Into hrs_TransactionsTypesTiers " &
                 "([TransactionsTypesId],[SerialNumberTiers],[FinancialPeriodTiers],[BaseFormulaTiers],[BeginFormulaTiers],[EndFormulaTiers]) VALUES (" &
                 ClsTransactions.ID & ", " &
                 "" & DGRow.Cells("2").Value & ", " &
                 "'" & CDate(DGRow.Cells("3").Text).ToString("yyyy-MM-dd") & "', " &
                 "'" & DGRow.Cells("4").Text & "', " &
                 "'" & DGRow.Cells("5").Text & "', " &
                 "'" & DGRow.Cells("6").Text & "') ; " & vbNewLine
                End If





            Next
            If SqlCommand <> "" Then
                Dim cmd As New SqlClient.SqlCommand
                cmd.CommandText = SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.Connection = New SqlClient.SqlConnection(ClsTransactions.ConnectionString)
                cmd.Connection.Open()
                cmd.ExecuteNonQuery()
                cmd.Connection.Close()
            End If
        End If

    End Function
    Private Function AssignValue(ByRef ClsTransactions As Clshrs_TransactionsTypes) As Boolean
        Try
            With ClsTransactions
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ShortEngName = txtShortEngName.Text
                .ArbName = txtArbName.Text
                .ShortArbName = txtShortArbName.Text
                .Sign = ddlSign.SelectedItem.Value
                .Formula = txtFormula.Text
                .BeginContractFormula = txtBeginFormula.Text
                .EndContractFormula = txtEndFormula.Text
                .TransactionGroupID = DdlTransactionCategory.SelectedItem.Value
                .DebitAccountCode = txtDebitAccountCode.Text
                .CreditAccountCode = txtCreditAccountCode.Text
                .IsPaid = chkIsPaid.Checked
                .InputIsNumeric = chkInputNumeric.Checked
                .IsDistributable = chkIsDistributable.Checked
                .IsBasicSalary = chkIsBasicSalary.Checked
                .IsEndOfService = chkIsEndOfService.Checked
                .IsAllowPosting = CheckBox_IsAllowPosting.Checked
                .IsSalaryEOSExeclude = CheckBox_IsSalaryEOSExeclude.Checked
                .IsProjectRelatedItem = CheckBox_IsProjectRelatedItem.Checked
                .HasInsuranceTiers = chkHasInsuranceTiers.Checked
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues(ByVal ClsTransactions As Clshrs_TransactionsTypes) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = ClsTransactions.ID
        Dim ClsTransactionGroups As New Clshrs_TransactionGroups(Page)
        Dim objNavigation As New Venus.Shared.Web.NavigationHandler(ClsTransactionGroups.ConnectionString)

        Try
            SetToolBarDefaults()
            With ClsTransactions
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtShortEngName.Text = .ShortEngName
                txtArbName.Text = .ArbName
                txtShortArbName.Text = .ShortArbName
                txtFormula.Text = .Formula
                txtBeginFormula.Text = .BeginContractFormula
                txtEndFormula.Text = .EndContractFormula
                txtDebitAccountCode.Text = .DebitAccountCode
                txtCreditAccountCode.Text = .CreditAccountCode
                chkIsPaid.Checked = .IsPaid
                chkInputNumeric.Checked = .InputIsNumeric
                chkIsBasicSalary.Checked = .IsBasicSalary
                chkIsEndOfService.Checked = .IsEndOfService
                chkIsDistributable.Checked = .IsDistributable
                CheckBox_IsAllowPosting.Checked = .IsAllowPosting
                CheckBox_IsSalaryEOSExeclude.Checked = .IsSalaryEOSExeclude
                CheckBox_IsProjectRelatedItem.Checked = .IsProjectRelatedItem
                chkHasInsuranceTiers.Checked = .HasInsuranceTiers
                ddlSign.SelectedValue = .Sign

                lblFormulaDesc.Text = GetFormulaValue(txtFormula.Text, objNavigation.SetLanguage(Page, "Eng/Arb"))
                If (lblFormulaDesc.Text <> "") Then
                    lblFormulaDesc.BorderStyle = BorderStyle.Solid
                    lblFormulaDesc.BorderWidth = "1"
                    lblFormulaDesc.BorderColor = Drawing.Color.White
                Else
                    lblFormulaDesc.BorderStyle = BorderStyle.None
                    lblFormulaDesc.BorderWidth = "0"
                    lblFormulaDesc.BorderColor = Drawing.Color.White
                End If

                lblBeginFormulaDesc.Text = GetFormulaValue(txtBeginFormula.Text, objNavigation.SetLanguage(Page, "Eng/Arb"))
                If (lblBeginFormulaDesc.Text <> "") Then
                    lblBeginFormulaDesc.BorderStyle = BorderStyle.Solid
                    lblBeginFormulaDesc.BorderWidth = "1"
                    lblBeginFormulaDesc.BorderColor = Drawing.Color.White
                Else
                    lblBeginFormulaDesc.BorderStyle = BorderStyle.None
                    lblBeginFormulaDesc.BorderWidth = "0"
                    lblBeginFormulaDesc.BorderColor = Drawing.Color.White
                End If

                lblEndFormulaDesc.Text = GetFormulaValue(txtEndFormula.Text, objNavigation.SetLanguage(Page, "Eng/Arb"))
                If (lblEndFormulaDesc.Text <> "") Then
                    lblEndFormulaDesc.BorderStyle = BorderStyle.Solid
                    lblEndFormulaDesc.BorderWidth = "1"
                    lblEndFormulaDesc.BorderColor = Drawing.Color.White
                Else
                    lblEndFormulaDesc.BorderStyle = BorderStyle.None
                    lblEndFormulaDesc.BorderWidth = "0"
                    lblEndFormulaDesc.BorderColor = Drawing.Color.White
                End If

                Dim item As New System.Web.UI.WebControls.ListItem()
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(.ConnectionString)
                ClsTransactionGroups.GetDropDownList(DdlTransactionCategory, True)
                ClsTransactionGroups.Find(" ID= " & IIf(IsNothing(.TransactionGroupID), 0, .TransactionGroupID))
                If ClsTransactionGroups.ID > 0 Then
                    item.Value = .TransactionGroupID
                    item.Text = ObjNavigationHandler.SetLanguage(Page, ClsTransactionGroups.EngName & "/" & ClsTransactionGroups.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Page, ClsTransactionGroups.ArbName & "/" & ClsTransactionGroups.EngName)
                    End If
                    If Not DdlTransactionCategory.Items.Contains(item) Then
                        DdlTransactionCategory.Items.Add(item)
                        DdlTransactionCategory.SelectedValue = item.Value
                    Else
                        DdlTransactionCategory.SelectedValue = .TransactionGroupID
                    End If

                End If

                If Not ClsTransactions.RegUserID = Nothing Then
                    ClsUser.Find("ID=" & ClsTransactions.RegUserID)
                End If
                If ClsUser.EngName = Nothing Then
                    lblRegUserValue.Text = ""
                Else
                    lblRegUserValue.Text = ClsUser.EngName
                End If
                If Convert.ToDateTime(.RegDate).Date = Nothing Then
                    lblRegDateValue.Text = ""
                Else
                    lblRegDateValue.Text = Convert.ToDateTime(.RegDate).Date
                End If
                If .CancelDate = Nothing Then
                    lblCancelDateValue.Text = ""
                Else
                    lblCancelDateValue.Text = Convert.ToDateTime(.CancelDate).Date
                End If
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                Else
                    ImageButton_Delete.Enabled = True
                End If

                If (.ID > 0) Then
                    StrMode = "E"
                Else
                    StrMode = "N"
                End If
                SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                SetToolBarRecordPermission(Me, ClsTransactions.ConnectionString, ClsTransactions.DataBaseUserRelatedID, ClsTransactions.GroupID, ClsTransactions.Table, IntId)
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                End If
                If Page.IsPostBack Then
                    CreateOtherFields(ClsTransactions.ID)
                End If
            End With
            GetGrid()
            Return True
        Catch ex As Exception
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
    Private Function SetToolbarSetting(ByVal ptrType As String, ByVal ClsClass As Object, ByVal intID As Integer) As Boolean
        Try
            Select Case ptrType
                Case "N", "R"
                    txtCode.Text = String.Empty
                    ImageButton_First.Visible = False
                    ImageButton_Back.Visible = False
                    ImageButton_Next.Visible = False
                    ImageButton_Last.Visible = False
                    ImageButton_Delete.Enabled = False
                    ImageButton_Properties.Visible = False
                    LinkButton_Properties.Visible = False
                    ImageButton_Remarks.Visible = False
                    LinkButton_Remarks.Visible = False

                Case "D"
                    ClsTransactions.Find("ID=" & intID)
                    GetValues(ClsTransactions)
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsTransactions.Find("ID=" & intID)
                    GetValues(ClsTransactions)
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsTransactions
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
        ClsTransactions = New Clshrs_TransactionsTypes(Me.Page)
        Try
            With ClsTransactions
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
        ClsTransactions = New Clshrs_TransactionsTypes(Me.Page)
        If IntId > 0 Then
            ClsTransactions.Find("ID=" & IntId)
            GetValues(ClsTransactions)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsTransactions = New Clshrs_TransactionsTypes(Me.Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Try
            ClsTransactions.Find("Code='" & txtCode.Text & "'")
            IntId = ClsTransactions.ID
            txtEngName.Focus()
            If ClsTransactions.ID > 0 Then
                GetValues(ClsTransactions)
                GetGrid()
                StrMode = "E"

            Else
                If ClsTransactions.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If

                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsTransactions.ConnectionString, ClsTransactions.DataBaseUserRelatedID, ClsTransactions.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsTransactions.ConnectionString, ClsTransactions.DataBaseUserRelatedID, ClsTransactions.GroupID, ClsTransactions.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                ImageButton_Delete.Enabled = False
            End If
        Catch ex As Exception
        End Try
        chkHasInsuranceTiers_TextChanged(Nothing, Nothing)
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        ClsTransactions.Clear()
        UwgSearchEmployees.DataSource = Nothing
        UwgSearchEmployees.DataBind()
        UwgSearchEmployees.Rows.Add()
        GetValues(ClsTransactions)
        chkIsBasicSalary.Checked = False
        chkIsEndOfService.Checked = False
        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        txtShortArbName.Text = String.Empty
        txtShortEngName.Text = String.Empty
        ddlSign.SelectedValue = 1
        txtFormula.Text = String.Empty
        txtBeginFormula.Text = String.Empty
        txtEndFormula.Text = String.Empty
        txtCreditAccountCode.Text = String.Empty
        txtDebitAccountCode.Text = String.Empty
        chkIsPaid.Checked = True
        chkInputNumeric.Checked = True
        DdlTransactionCategory.SelectedIndex = 0
        chkIsBasicSalary.Checked = False
        chkIsEndOfService.Checked = False
        chkIsDistributable.Checked = False
        CheckBox_IsAllowPosting.Checked = False
        CheckBox_IsSalaryEOSExeclude.Checked = False
        CheckBox_IsProjectRelatedItem.Checked = False
        chkHasInsuranceTiers.Checked = False
        lblFormulaDesc.Text = ""
        lblFormulaDesc.BorderWidth = 0

        lblFormulaDesc.Text = ""
        lblFormulaDesc.BorderWidth = 0

        lblBeginFormulaDesc.Text = ""
        lblBeginFormulaDesc.BorderWidth = 0

        lblEndFormulaDesc.Text = ""
        lblEndFormulaDesc.BorderWidth = 0

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsTransactions = New Clshrs_TransactionsTypes(Me.Page)
        ClsTransactions.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsTransactions.ID
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
    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsTransactions.Table) = True Then
            Dim StrTablename As String
            ClsTransactions = New Clshrs_TransactionsTypes(Page)
            StrTablename = ClsTransactions.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID &
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID &
                                    " And sys_OtherFields.CancelDate is Null ")
            objDS = clsOtherFieldsData.DataSet
            name.Text = ""
            realname.Text = ""
            If objDS.Tables(0).Rows.Count > 0 Then
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "U", objDS, "Interfaces_frmRegions")
            Else
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "A", objDS, "Interfaces_frmRegions")
            End If
        End If
    End Function

#End Region

#Region "Shared Function"

    Public Shared Function Find(ByVal Table As String, ByVal Filter As String, ByRef DataSet As Data.DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Dim mSelectCommand = " Select * From " & Table

        Dim mSqlDataAdapter As New Data.SqlClient.SqlDataAdapter
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)

        Try
            'Dim orderByStr As String = ""
            'If Filter.ToLower.IndexOf("order by") = -1 Then
            '    orderByStr = " Order By Code "
            'End If

            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where " & Filter & " And CancelDate IS Null", " Where CancelDate IS Null")

            StrSelectCommand = StrSelectCommand '& orderByStr

            mSqlDataAdapter = New Data.SqlClient.SqlDataAdapter(StrSelectCommand, ConnStr)
            DataSet = New Data.DataSet
            mSqlDataAdapter.Fill(DataSet)

            If DataSet.Tables(0).Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Shared Function GetFormulaVlaue(ByVal strID As String, ByVal lang As String) As String
        Dim IntIndex As Integer = 0
        Dim StrCode As String = String.Empty
        Dim StrFormulaValue As String = ""
        Dim Strformula As String = ""
        Dim objkey As Object
        Dim ds As New Data.DataSet
        Dim arrEngCode As New System.Collections.SortedList()
        Dim arrArbCode As New System.Collections.SortedList()

        lang = CType(HttpContext.Current.Session("Lang"), String)

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

        'If Find("hrs_TransactionsTypes", " ID=" & strID, ds) Then
        'StrFormulaValue = IIf(IsDBNull(ds.Tables(0).Rows(0).Item("Formula")), "", ds.Tables(0).Rows(0).Item("Formula"))
        StrFormulaValue = strID
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

        'Return Strformula & "=" & StrFormulaValue
        Return StrFormulaValue

    End Function

    Public Shared Function GetDesc(ByVal StrCode As String, ByVal lang As String) As String
        Dim StrDesc As String = ""
        Dim ds As New Data.DataSet

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

    Public Shared Function GetFieldDescription(ByVal StrCode As String, ByVal StrTableName As String) As String
        Dim StrReturnData As Object
        Try
            StrReturnData = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, " Select EngName + '/' + ArbName From " & StrTableName & " Where Code = '" & StrCode.ToString.TrimStart.TrimEnd & "'")
            If IsNothing(StrReturnData) Then Return "/"
            If IsDBNull(StrReturnData) Then Return "/"
            Return StrReturnData
        Catch ex As Exception
            Return "/"
        End Try

    End Function

#End Region

#Region "PageMethods"
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetFormulaValue(ByVal StrFormula As String, ByVal StrLang As String) As String
        Return GetFormulaVlaue(StrFormula, StrLang)
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function Get_Searched_Description(ByVal IntSearchId As Integer, ByVal strCode As String) As String

        Dim dsSearchs As New Data.DataSet
        Find("sys_Searchs", " sys_Searchs.Id = " & IntSearchId, dsSearchs)

        Dim dsObjects As New Data.DataSet
        Find("sys_Objects", " sys_Objects.Id = " & dsSearchs.Tables(0).Rows(0).Item("ObjectID"), dsObjects)

        Return GetFieldDescription(strCode, dsObjects.Tables(0).Rows(0).Item("Code"))
    End Function
#End Region

End Class
