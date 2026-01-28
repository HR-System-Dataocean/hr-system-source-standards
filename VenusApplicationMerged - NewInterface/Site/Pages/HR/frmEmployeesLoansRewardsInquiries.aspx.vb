Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmEmployeesLoansRewardsInquiries
    Inherits MainPage
#Region "Public Decleration"
    Dim ClsEmployeesPayability As Clshrs_EmployeesPayability
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0

        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim ClsCompanies As New Clssys_Companies(Page)
        Dim ClsGHCalender As New Clssys_GHCalendar(Page)
        Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Dim ClsCurrencies As New ClsSys_Currencies(Page)
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim StrSerial As String = String.Empty
        Dim currDate As Date = Date.Now
        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsTransactionTypes.ConnectionString)
        ClsEmployeesPayability = New Clshrs_EmployeesPayability(Page)
        ClsCompanies.Find("ID = " & ClsCompanies.MainCompanyID)
        ClsEmployeesPayability.AddNotificationOnChange(Page)

        Try

            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsEmployeesPayability.ConnectionString)
                ClsEmployeesPayability.AddOnChangeEventToControls("frmEmployeesLoansRewardsInquiries", Page, UltraWebTab1)
                ClsTransactionTypes.GetDropDownList(txtTransactionType, True, " IsDistributable =1  ")

                If txtTransactionType.Items.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "You must enter at least one distributed transactions/يجب أن تدخل بند واحد قابل للتقسيم على الاقل"))
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If

                Clear()
                currDate = ClsEmployeesPayability.GetHigriDate(currDate)

                ObjDataHandler.GetLastSerial(ClsEmployeesPayability.Table, "Number", lblDescLoanCode.Text, ClsEmployeesPayability.ConnectionString, "00000")
                SetScreenInformation("N")
                txtTransactionDate1.Text = Format(currDate, "dd/MM/yyyy")
                txtTransactionDate.Text = Format(currDate, "dd/MM/yyyy HH:mm")
                txtStartdate.Value = Format(currDate, "dd/MM/yyyy")

                ClsEmployeesPayability.LastRecord()
                ClsEmployees.Find("ID=" & ClsEmployeesPayability.EmployeeID)
                TextChanged(ClsEmployees.Code)
                lblDescLoanCode.Text = ClsEmployeesPayability.Number
                txtTransactionType.SelectedValue = ClsEmployeesPayability.TransactionTypeID
                txtTransactionDate.Text = ClsEmployeesPayability.SetHigriDate(ClsEmployeesPayability.RegDate)
                txtTransactionAmount.Text = ClsEmployeesPayability.GetTransactionAmount(ClsEmployeesPayability.ID)
                txtStartdate.Value = ClsEmployeesPayability.SetHigriDate(ClsEmployeesPayability.GetInstalmentDate(ClsEmployeesPayability.ID))
                Dim ClsUser As New Clssys_Users(Page)
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesPayability.ConnectionString)
                ClsUser.Find("ID=" & ClsEmployeesPayability.RegUserID)
                txtRegUser.Text = ObjNavigationHandler.SetLanguage(Page, ClsUser.ArbName & "/" & ClsUser.EngName)

                txtTransactionType.Enabled = False

            Else
                SetScreenInformation("E")

            End If



            If (lblDescLoanCode.Text <> "" And hdnSettelment.Value = "1") Then
                If ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'") Then
                    GetValues()
                    Dim tempClsEmployeesPayability As New Clshrs_EmployeesPayability(Page)
                    tempClsEmployeesPayability.Find(" EmployeeID=" & ClsEmployeesPayability.EmployeeID & " and (select top 1 Sign from [dbo].[hrs_TransactionsTypes] where hrs_EmployeesPayabilities.TransactionTypeID=hrs_TransactionsTypes.ID)=1 ")

                End If
                hdnSettelment.Value = "0"
            End If

            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'")
                IntrecordID = ClsEmployeesPayability.ID
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

            If Not IsPostBack Then
                If Request.QueryString.Item("EmpCode") <> Nothing And Request.QueryString.Item("TAmt") <> Nothing And Request.QueryString.Item("NOS") <> Nothing And Request.QueryString.Item("SDate") <> Nothing Then
                    txtCode.Text = Request.QueryString.Item("EmpCode")
                    TextChanged()
                    txtTransactionType.SelectedValue = Request.QueryString.Item("TrnsType")
                    txtStartdate.Value = ClsTransactionTypes.GetHigriDate(Request.QueryString.Item("SDate"))
                    txtTransactionAmount.Text = Convert.ToDecimal(Request.QueryString.Item("TAmt"))



                End If
            End If

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesPayability.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command
        ClsEmployeesPayability = New Clshrs_EmployeesPayability(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesPayability.ConnectionString)
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ClsEmployees.Find("Code = '" & txtCode.Text & "'")
                Select Case ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'")
                    Case True
                        If Not AssignValues() Then
                            Exit Sub
                        End If
                        ClsEmployeesPayability.Update("Number='" & lblDescLoanCode.Text & "'")
                        SaveInstalment(ClsEmployeesPayability.ID)

                    Case False
                        Dim boolDone As Boolean = False
                        If lblError.Text = "" Then
                            Dim ClsContract As New Clshrs_Contracts(Page)
                            Dim ValidContractChek As Integer = ClsContract.ContractValidatoinId(ClsEmployees.ID, Convert.ToDateTime(ClsEmployees.SetHigriDate(txtStartdate.Value)))
                            If ValidContractChek > 0 Then
                                ClsContract.Find("ID = " & ValidContractChek)
                                Dim ClsClasses As New Clshrs_EmployeeClasses(Page)
                                If ClsClasses.Find("ID = " & ClsContract.EmployeeClassID) Then
                                    Dim ClsEmployeesContracts As New Clshrs_Contracts(Me.Page)
                                    Dim dbBasicSalary As Double = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, "set dateformat dmy; select dbo.fn_GetBasicSalary(" & ClsEmployeesContracts.ContractValidatoinId(ClsEmployees.ID, ClsEmployees.SetHigriDate(txtStartdate.Value)) & ",'" & Convert.ToDateTime(ClsEmployees.SetHigriDate(txtStartdate.Value)).ToString("dd/MM/yyyy") & "')")
                                    Dim remAmt As Double = 0

                                    If ClsClasses.MinServiceMonth > 0 Then
                                        Dim WMonths As Integer = Convert.ToDateTime(ClsEmployees.SetHigriDate(txtStartdate.Value)).Subtract(ClsEmployees.JoinDate).Days / 30
                                        If WMonths < ClsClasses.MinServiceMonth Then
                                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "The Working Period Less Than Required Period /مدة العمل لا تسمح بإضافة سلف"))
                                            Exit Sub
                                        End If
                                    End If
                                    If ClsClasses.MaxLoanAmtPCT > 0 Then
                                        Dim maxLoan As Double = dbBasicSalary * (ClsClasses.MaxLoanAmtPCT / 100)
                                        If maxLoan < (remAmt + txtTransactionAmount.Text) Then
                                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "The Maximum Limit Reached /تم الوصول للحد الأقصى للسلف المسموح به"))
                                            Exit Sub
                                        End If
                                    End If
                                    If ClsClasses.MaxInstallementPCT > 0 Then
                                        Dim maxIns As Double = dbBasicSalary * (ClsClasses.MaxInstallementPCT / 100)

                                    End If
                                    If Not AssignValues() Then
                                        Exit Sub
                                    End If
                                    Dim PayID As Integer = ClsEmployeesPayability.Save()
                                    SaveInstalment(PayID)
                                    boolDone = True
                                End If
                            End If
                        End If
                        If boolDone = False Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Failled /فشل الحفظ"))
                            Exit Sub
                        End If
                End Select
                Clear()
                'ClsEmployees.MaxLoanDedution = txtMaxLoanDeduction.Text
                ClsEmployees.Update("Code='" & txtCode.Text)
                ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsEmployeesPayability.Table, ClsEmployeesPayability.ID)
                value.Text = ""
                TextChanged("")
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If txtTransactionType.SelectedIndex = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please select transaction type /برجاء إختيار نوع الحركة"))
                    Exit Sub
                End If
                ClsEmployees.Find("Code = '" & txtCode.Text & "'")
                Select Case ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'")
                    Case True
                        If Not AssignValues() Then
                            Exit Sub
                        End If
                        ClsEmployeesPayability.Update("Number='" & lblDescLoanCode.Text & "'")
                        SaveInstalment(ClsEmployeesPayability.ID)
                    Case False
                        Dim boolDone As Boolean = False
                        If lblError.Text = "" Then
                            Dim ClsContract As New Clshrs_Contracts(Page)
                            Dim ValidContractChek As Integer = ClsContract.ContractValidatoinId(ClsEmployees.ID, Convert.ToDateTime(ClsEmployees.SetHigriDate(txtStartdate.Value)))
                            If ValidContractChek > 0 Then
                                ClsContract.Find("ID = " & ValidContractChek)
                                Dim ClsClasses As New Clshrs_EmployeeClasses(Page)
                                If ClsClasses.Find("ID = " & ClsContract.EmployeeClassID) Then
                                    Dim ClsEmployeesContracts As New Clshrs_Contracts(Me.Page)
                                    Dim dbBasicSalary As Double = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, "set dateformat dmy; select dbo.fn_GetBasicSalary(" & ClsEmployeesContracts.ContractValidatoinId(ClsEmployees.ID, ClsEmployees.SetHigriDate(txtStartdate.Value)) & ",'" & Convert.ToDateTime(ClsEmployees.SetHigriDate(txtStartdate.Value)).ToString("dd/MM/yyyy") & "')")
                                    Dim remAmt As Double = 0

                                    If ClsClasses.MinServiceMonth > 0 Then
                                        Dim WMonths As Integer = Convert.ToDateTime(ClsEmployees.SetHigriDate(txtStartdate.Value)).Subtract(ClsEmployees.JoinDate).Days / 30
                                        If WMonths < ClsClasses.MinServiceMonth Then
                                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "The Working Period Less Than Required Period /مدة العمل لا تسمح بإضافة سلف"))
                                            Exit Sub
                                        End If
                                    End If
                                    If ClsClasses.MaxLoanAmtPCT > 0 Then
                                        Dim maxLoan As Double = dbBasicSalary * (ClsClasses.MaxLoanAmtPCT / 100)
                                        If maxLoan < (remAmt + txtTransactionAmount.Text) Then
                                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "The Maximum Limit Reached /تم الوصول للحد الأقصى للسلف المسموح به"))
                                            Exit Sub
                                        End If
                                    End If
                                    If ClsClasses.MaxInstallementPCT > 0 Then
                                        Dim maxIns As Double = dbBasicSalary * (ClsClasses.MaxInstallementPCT / 100)

                                    End If
                                    If Not AssignValues() Then
                                        Exit Sub
                                    End If
                                    Dim PayID As Integer = ClsEmployeesPayability.Save()
                                    SaveInstalment(PayID)
                                    boolDone = True
                                End If
                            End If
                        End If
                        If boolDone = False Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Failled /فشل الحفظ"))
                            Exit Sub
                        End If
                End Select
                'If Not String.IsNullOrEmpty(txtMaxLoanDeduction.Text) Then
                '    ClsEmployees.MaxLoanDedution = txtMaxLoanDeduction.Text
                'End If

                ClsEmployees.Update("Code='" & txtCode.Text & "'")
                ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsEmployeesPayability.Table, ClsEmployeesPayability.ID)
                value.Text = ""
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                Clear()
                'txtMaxLoanDeduction.Text = ClsEmployees.MaxLoanDedution
            Case "New"
                Clear()

            Case "Delete"
                If ClsEmployees.Find("Code = '" & txtCode.Text & "'") Then
                    ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'")
                    Dim hrsemployeeTransactions As New Clshrs_EmployeesTransactions(Me)
                    If hrsemployeeTransactions.Find("PostDate is not null and ID = " & ClsEmployeesPayability.RegComputerID) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Locked Record /الملف مغلق"))
                        Return
                    End If
                    Dim str As String = "delete from hrs_EmployeesTransactions where ID =" & ClsEmployeesPayability.RegComputerID
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeesPayability.ConnectionString, System.Data.CommandType.Text, str)
                    ClsEmployeesPayability.Delete("Number='" & lblDescLoanCode.Text & "'")
                End If
                Clear()
            Case "Property"
                ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsEmployeesPayability.ID & "&TableName=" & ClsEmployeesPayability.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsEmployeesPayability.ID & "&TableName=" & ClsEmployeesPayability.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsEmployeesPayability.Table
                ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'")
                Dim recordID As Integer = ClsEmployeesPayability.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsEmployeesPayability.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'")
                If ClsEmployeesPayability.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsEmployeesPayability.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsEmployeesPayability.CheckDiff(ClsEmployeesPayability, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsEmployeesPayability.FirstRecord()
                ClsEmployees.Find("ID=" & ClsEmployeesPayability.EmployeeID)
                TextChanged(ClsEmployees.Code)
                lblDescLoanCode.Text = ClsEmployeesPayability.Number
                txtTransactionType.SelectedValue = ClsEmployeesPayability.TransactionTypeID
                txtTransactionDate.Text = ClsEmployeesPayability.SetHigriDate(ClsEmployeesPayability.RegDate)
                txtTransactionAmount.Text = ClsEmployeesPayability.GetTransactionAmount(ClsEmployeesPayability.ID)
                txtStartdate.Value = ClsEmployeesPayability.SetHigriDate(ClsEmployeesPayability.GetInstalmentDate(ClsEmployeesPayability.ID))
                Dim ClsUser As New Clssys_Users(Page)
                'Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesPayability.ConnectionString)
                ClsUser.Find("ID=" & ClsEmployeesPayability.RegUserID)
                txtRegUser.Text = ObjNavigationHandler.SetLanguage(Page, ClsUser.ArbName & "/" & ClsUser.EngName)


            Case "Previous"
                ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'")
                If Not ClsEmployeesPayability.previousRecord() Then
                    ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If

                ClsEmployees.Find("ID=" & ClsEmployeesPayability.EmployeeID)
                TextChanged(ClsEmployees.Code)
                lblDescLoanCode.Text = ClsEmployeesPayability.Number
                txtTransactionType.SelectedValue = ClsEmployeesPayability.TransactionTypeID
                txtTransactionDate.Text = ClsEmployeesPayability.SetHigriDate(ClsEmployeesPayability.RegDate)
                txtTransactionAmount.Text = ClsEmployeesPayability.GetTransactionAmount(ClsEmployeesPayability.ID)
                txtStartdate.Value = ClsEmployeesPayability.SetHigriDate(ClsEmployeesPayability.GetInstalmentDate(ClsEmployeesPayability.ID))
                Dim ClsUser As New Clssys_Users(Page)
                'Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesPayability.ConnectionString)
                ClsUser.Find("ID=" & ClsEmployeesPayability.RegUserID)
                txtRegUser.Text = ObjNavigationHandler.SetLanguage(Page, ClsUser.ArbName & "/" & ClsUser.EngName)


            Case "Next"
                ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'")
                If Not ClsEmployeesPayability.NextRecord() Then
                    ClsEmployeesPayability.Find("Number='" & lblDescLoanCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                ClsEmployees.Find("ID=" & ClsEmployeesPayability.EmployeeID)
                TextChanged(ClsEmployees.Code)
                lblDescLoanCode.Text = ClsEmployeesPayability.Number
                txtTransactionType.SelectedValue = ClsEmployeesPayability.TransactionTypeID
                txtTransactionDate.Text = ClsEmployeesPayability.SetHigriDate(ClsEmployeesPayability.RegDate)
                txtTransactionAmount.Text = ClsEmployeesPayability.GetTransactionAmount(ClsEmployeesPayability.ID)
                txtStartdate.Value = ClsEmployeesPayability.SetHigriDate(ClsEmployeesPayability.GetInstalmentDate(ClsEmployeesPayability.ID))
                Dim ClsUser As New Clssys_Users(Page)
                'Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesPayability.ConnectionString)
                ClsUser.Find("ID=" & ClsEmployeesPayability.RegUserID)
                txtRegUser.Text = ObjNavigationHandler.SetLanguage(Page, ClsUser.ArbName & "/" & ClsUser.EngName)

            Case "Last"
                ClsEmployeesPayability.LastRecord()
                ClsEmployees.Find("ID=" & ClsEmployeesPayability.EmployeeID)
                TextChanged(ClsEmployees.Code)
                lblDescLoanCode.Text = ClsEmployeesPayability.Number
                txtTransactionType.SelectedValue = ClsEmployeesPayability.TransactionTypeID
                txtTransactionDate.Text = ClsEmployeesPayability.SetHigriDate(ClsEmployeesPayability.RegDate)
                txtTransactionAmount.Text = ClsEmployeesPayability.GetTransactionAmount(ClsEmployeesPayability.ID)
                txtStartdate.Value = ClsEmployeesPayability.SetHigriDate(ClsEmployeesPayability.GetInstalmentDate(ClsEmployeesPayability.ID))
                Dim ClsUser As New Clssys_Users(Page)
                'Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesPayability.ConnectionString)
                ClsUser.Find("ID=" & ClsEmployeesPayability.RegUserID)
                txtRegUser.Text = ObjNavigationHandler.SetLanguage(Page, ClsUser.ArbName & "/" & ClsUser.EngName)

        End Select
    End Sub





#End Region

#Region "Private Functions"

    Private Sub TextChanged(Optional ByVal strCode As String = "txtCode")
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ClsEmployeesPayability1 As New Clshrs_EmployeesPayability(Page)
        Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        ClsTransactionTypes = New Clshrs_TransactionsTypes(Page)
        ClsEmployeesPayability1 = New Clshrs_EmployeesPayability(Page)

        lblError.Text = ""
        txtCode.Text = IIf(strCode = "txtCode", txtCode.Text, strCode)
        If ClsEmployees.Find(" Code='" & txtCode.Text & "'") Then
            If CheckValid(ClsEmployees.ID) Then

                lblDescEnglishName.Text = ClsEmployees.EnglishName
                ClsEmployeesPayability1.AddDataUpdateSchedules(Page, UltraWebTab1, "frmEmployeeLoans", ClsEmployees.ID)
                ClsEmployeesPayability1.Find(" EmployeeID=" & ClsEmployees.ID & " and (select top 1 Sign from [dbo].[hrs_TransactionsTypes] where hrs_EmployeesPayabilities.TransactionTypeID=hrs_TransactionsTypes.ID)=1 ")


                'Clear()
                'txtMaxLoanDeduction.Text = ClsEmployees.MaxLoanDedution
            Else
                ClearEmployee()
                lblError.Text = ObjNavigationHandler.SetLanguage(Page, "This Employee has no valid contract/هذا الموظف ليس لة عقد")
            End If
        Else
            ClearEmployee()
            If txtCode.Text.Length > 0 Then
                lblError.Text = ObjNavigationHandler.SetLanguage(Page, "The Employee is not found/ الموظف غير موجود")
            Else
                lblError.Text = ""
            End If
        End If
    End Sub
    Private Function CheckValid(ByVal EmployeeID As Integer) As Boolean
        Dim ClsContract As New Clshrs_Contracts(Page)
        If ClsContract.ContractValidatoinId(EmployeeID, Date.Now) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function ClearEmployee() As Boolean
        lblDescEnglishName.Text = ""
        lblDescLoanCode.Text = ""

        txtCode.Focus()
        Clear()
    End Function
    Private Function AssignValues() As Boolean
        Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(Page)
        Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)

        Dim ClsEmployee As New Clshrs_Employees(Page)
        Try
            With ClsEmployeesPayability

                If ClsEmployee.Find("Code='" & txtCode.Text & "'") Then
                    .EmployeeID = ClsEmployee.ID
                End If

                .TransactionTypeID = txtTransactionType.SelectedItem.Value
                .Number = lblDescLoanCode.Text
                'Edit By Talaat At 28-02-2011
                .TransactionDate = ClsEmployee.SetHigriDate(txtTransactionDate.Text)

            End With
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function SaveInstalment(ByVal IntId As Integer) As Boolean
        Dim ObjRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
        Dim StrSqlCommand As String
        Try
            Dim clsEmployeesPayability As New Clshrs_EmployeesPayability(Me.Page)
            clsEmployeesPayability.Find("ID = " & IntId)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployeesPayability.ConnectionString)
            '1- find total amount


            StrSqlCommand = "Set DateFormat DMY; Delete From hrs_EmployeesPayabilitiesSchedules Where ID not in(Select EmployeePayabilityScheduleID from hrs_EmployeesPayabilitiesSchedulesSettlement) and EmployeePayabilityId=" & IntId & ";" & vbNewLine
            StrSqlCommand &= " update hrs_EmployeesPayabilitiesSchedules set DueAmount =s.amount from hrs_EmployeesPayabilitiesSchedules p inner join (select EmployeePayabilityScheduleID,sum(Amount)amount  from hrs_EmployeesPayabilitiesSchedulesSettlement group by EmployeePayabilityScheduleID ) s on p.id=s.EmployeePayabilityScheduleID where p.DueAmount-s.Amount >0 and p.EmployeePayabilityID=" & clsEmployeesPayability.ID & ";" & vbNewLine
            Dim LoanAmount As Double = clsEmployeesPayability.GetTransactionAmount(clsEmployeesPayability.ID) - clsEmployeesPayability.GetSettlementAmount(clsEmployeesPayability.ID)

            'Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployeesPayability.ConnectionString, System.Data.CommandType.Text, StrSqlCommand)
            Dim OriginalLaonAmount As Double = LoanAmount
            If LoanAmount > 0 Then
                Dim StrSqlFirstDueDate As String = "set dateformat dmy ;select isnull((select top 1 dateadd(m,1,date)   from hrs_EmployeesPayabilitiesSchedulesSettlement where EmployeePayabilityScheduleID in(select id from hrs_EmployeesPayabilitiesSchedules where EmployeePayabilityID=" & clsEmployeesPayability.ID & ") order by date desc),(select top 1 DueDate   from hrs_EmployeesPayabilitiesSchedules where EmployeePayabilityID =" & clsEmployeesPayability.ID & " ))"
                Dim FirsNonPaidSettelment As Date = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployeesPayability.ConnectionString, System.Data.CommandType.Text, StrSqlFirstDueDate)




                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployeesPayability.ConnectionString, System.Data.CommandType.Text, StrSqlCommand)

                Dim settlemntAmount As Double
                If LoanAmount > settlemntAmount Then
                    For index = 1 To LoanAmount / settlemntAmount
                        StrSqlCommand &= "Insert Into hrs_EmployeesPayabilitiesSchedules(" &
                                                                     "EmployeePayabilityId," &
                                                                     "DueDate," &
                                                                     "DueAmount," &
                                                                     "RegUserID," &
                                                                     "CompanyId" &
                                                                     ")values(" &
                                                                     IntId & ",'" &
                                                                     FirsNonPaidSettelment.ToString("dd/MM/yyyy") & "'," &
                                                                     IIf(settlemntAmount = Nothing, 0, settlemntAmount) & "," &
                                                                     clsEmployeesPayability.DataBaseUserRelatedID & "," &
                                                                     clsEmployeesPayability.MainCompanyID &
                                                                     ");" & vbNewLine

                        LoanAmount = LoanAmount - settlemntAmount
                        FirsNonPaidSettelment = FirsNonPaidSettelment.AddMonths(1)
                    Next
                End If
                If LoanAmount > 0 Then
                    StrSqlCommand &= "Insert Into hrs_EmployeesPayabilitiesSchedules(" &
                                                                      "EmployeePayabilityId," &
                                                                      "DueDate," &
                                                                      "DueAmount," &
                                                                      "RegUserID," &
                                                                      "CompanyId" &
                                                                      ")values(" &
                                                                      IntId & ",'" &
                                                                      FirsNonPaidSettelment.ToString("dd/MM/yyyy") & "'," &
                                                                      IIf(LoanAmount = Nothing, 0, LoanAmount) & "," &
                                                                      clsEmployeesPayability.DataBaseUserRelatedID & "," &
                                                                      clsEmployeesPayability.MainCompanyID &
                                                                      ");" & vbNewLine
                End If

                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployeesPayability.ConnectionString, System.Data.CommandType.Text, StrSqlCommand)
            End If


            If OriginalLaonAmount = 0 Then
                Dim currDate As Date

            End If


            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployeesPayability.ConnectionString, System.Data.CommandType.Text, StrSqlCommand)
            'append to Transactions Table By Tal3at
            Dim cmdString As String = ""
            If clsEmployeesPayability.RegComputerID = Nothing Then
                cmdString &= "Set DateFormat DMY insert into hrs_EmployeesTransactions(EmployeeID,FinancialWorkingUnits,FiscalYearPeriodID,PrepareType,Applyed)Values(" & clsEmployeesPayability.EmployeeID & "," & 0 & ",(select ID from sys_FiscalYearsPeriods where '" & clsEmployeesPayability.TransactionDate & "' between FromDate and todate),'L',0);" & vbNewLine
                cmdString &= "Select IDENT_CURRENT('hrs_EmployeesTransactions');"
                Dim TrnsID As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployeesPayability.ConnectionString, System.Data.CommandType.Text, cmdString)

                cmdString = "Set DateFormat DMY insert into hrs_EmployeesTransactionsProjects(EmployeeTransactionID,ProjectID,WorkingUnits,RegUserID)Values(" & TrnsID & ",(select DefaultProjectID from hrs_EmployeesClasses where ID in (select top 1 employeeclassid from hrs_Contracts where CancelDate is null and EndDate is null and EmployeeID=" & clsEmployeesPayability.EmployeeID & " order by StartDate desc))," & 0 & "," & clsEmployeesPayability.DataBaseUserRelatedID & ");" & vbNewLine
                cmdString &= "Select IDENT_CURRENT('hrs_EmployeesTransactionsProjects');"
                Dim ProID As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployeesPayability.ConnectionString, System.Data.CommandType.Text, cmdString)

                cmdString = "Set DateFormat DMY insert into hrs_EmployeesTransactionsDetails(EmpTransProjID,TransactionTypeID,NumericValue)Values(" & ProID & "," & clsEmployeesPayability.TransactionTypeID & "," & clsEmployeesPayability.GetTransactionAmount(clsEmployeesPayability.ID) & ");" & vbNewLine
                cmdString &= "update hrs_EmployeesPayabilities set RegComputerID =" & TrnsID & " where ID = " & clsEmployeesPayability.ID & vbNewLine
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployeesPayability.ConnectionString, System.Data.CommandType.Text, cmdString)
            End If
        Catch ex As Exception
        End Try
    End Function


    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(Page)
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsNationality As New ClsBasicFiles(Page, "sys_Nationalities")
        Dim ClsEmploueesPayabilitySchedules As New Clshrs_EmployeesPayabilitySchedules(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try

            With ClsEmployeesPayability
                hdnID.Value = .ID
                Dim item As New System.Web.UI.WebControls.ListItem()
                ClsTransactionTypes.GetDropDownList(txtTransactionType, True, " IsDistributable =1  ")

                Dim ClsTransType As New Clshrs_TransactionsTypes(Page)
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(.ConnectionString)

                ClsTransType.Find(" ID= " & IIf(IsNothing(.TransactionTypeID), 0, .TransactionTypeID))
                If ClsTransType.ID > 0 Then

                    item.Value = .TransactionTypeID
                    item.Text = ObjNavigationHandler.SetLanguage(Page, ClsTransType.EngName & "/" & ClsTransType.ArbName)

                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Page, ClsTransType.ArbName & "/" & ClsTransType.EngName)

                    End If

                    If Not txtTransactionType.Items.Contains(item) Then
                        txtTransactionType.Items.Add(item)
                        txtTransactionType.SelectedValue = item.Value
                    Else
                        txtTransactionType.SelectedValue = .TransactionTypeID
                    End If
                End If

                If ClsEmployee.Find("ID=" & IIf(.EmployeeID Is Nothing, 0, .EmployeeID)) Then
                    txtCode.Text = ClsEmployee.Code
                    lblDescEnglishName.Text = ClsEmployee.EnglishName
                    If ClsNationality.Find("ID=" & IIf(ClsEmployee.NationalityID Is Nothing, 0, ClsEmployee.NationalityID)) Then

                    End If
                End If

                lblDescLoanCode.Text = .Number.ToString
                txtTransactionDate1.Text = IIf(.TransactionDate = Nothing, "", .TransactionDate)
                Dim totalTrans As Double = 0
                totalTrans = .GetTransactionAmount(.ID) - .GetSettlementAmount(.ID)
                txtTransactionAmount.Text = .GetTransactionAmount(.ID)
                lblCurrentInstalmentvalue.Text = totalTrans.ToString()
                lbltotalTransactionvalue.Text = "0"
                txtTransactionDate.Text = .GetHigriDate(.TransactionDate)

                Dim instDate As Date = .GetInstalmentDate(.ID)
                Dim mDataHandler As New Venus.Shared.DataHandler
                txtStartdate.Value = .GetHigriDate(instDate)

                'If ClsEmploueesPayabilitySchedules.FindPayments("EmployeePayabilityId=" & IIf(.ID Is Nothing, 0, .ID)) Then
                '    uwgScheduleTemplet.DataSource = ClsEmploueesPayabilitySchedules.DataSet
                '    uwgScheduleTemplet.DataBind()
                'End If


                If ClsEmploueesPayabilitySchedules.FindPayments(.ID) Then

                End If
            End With


            If Not ClsEmployeesPayability.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsEmployeesPayability.RegUserID)
            End If



            If (ClsEmployeesPayability.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsEmployeesPayability.ConnectionString, ClsEmployeesPayability.DataBaseUserRelatedID, ClsEmployeesPayability.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEmployeesPayability.ConnectionString, ClsEmployeesPayability.DataBaseUserRelatedID, ClsEmployeesPayability.GroupID, ClsEmployeesPayability.Table, ClsEmployeesPayability.ID)

            If Page.IsPostBack Then
                CreateOtherFields(ClsEmployeesPayability.ID)
            End If
            Return True
        Catch ex As Exception
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


                Case "D"
                    ClsEmployeesPayability.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True

                Case "E"
                    ClsEmployeesPayability.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True

            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsEmployeesPayability
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)

                End If
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation() As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEmployeesPayability = New Clshrs_EmployeesPayability(Me)
        Try
            With ClsEmployeesPayability
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
        ClsEmployeesPayability = New Clshrs_EmployeesPayability(Me)
        If IntId > 0 Then
            ClsEmployeesPayability.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function

    Private Function Clear() As Boolean
        hdnID.Value = 0
        Dim StrSerial As String = ""
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim ClsEmployees As New Clshrs_Employees(Page)

        ClsEmployeesPayability = New Clshrs_EmployeesPayability(Page)
        txtTransactionDate.Text = Nothing

        txtTransactionDate1.Text = ""
        txtTransactionAmount.Text = ""
        txtTransactionType.Enabled = True
        txtTransactionType.SelectedIndex = 0
        txtTransactionAmount.Enabled = True
        lblDescLoanCode.Text = ""
        lblCurrentInstalmentvalue.Text = ""
        lbltotalTransactionvalue.Text = ""


        ObjDataHandler.GetLastSerial(ClsEmployeesPayability.Table, "Number", StrSerial, ClsEmployeesPayability.ConnectionString, "00000")
        lblDescLoanCode.Text = StrSerial
        txtTransactionType.Enabled = True
        txtTransactionAmount.Enabled = True

        txtTransactionDate1.Text = Format(DateTime.Now, "dd/MM/yyyy")
        txtTransactionDate.Text = Format(DateTime.Now, "dd/MM/yyyy HH:mm")
        txtStartdate.Value = Format(DateTime.Now, "dd/MM/yyyy")

        ClsEmployees.Find("Code = '" & txtCode.Text & "'")
        ClsEmployeesPayability.Find(" EmployeeID=" & ClsEmployees.ID & " and (select top 1 Sign from [dbo].[hrs_TransactionsTypes] where hrs_EmployeesPayabilities.TransactionTypeID=hrs_TransactionsTypes.ID)=1 ")



        'txtMaxLoanDeduction.Text = ClsEmployees.MaxLoanDedution
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsEmployeesPayability = New Clshrs_EmployeesPayability(Page)
        ClsEmployeesPayability.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsEmployeesPayability.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEmployeesPayability.Table) = True Then
            Dim StrTablename As String
            ClsEmployeesPayability = New Clshrs_EmployeesPayability(Me)
            StrTablename = ClsEmployeesPayability.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID &
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID &
                                    " And sys_OtherFields.CancelDate is Null ")
            objDS = clsOtherFieldsData.DataSet
            name.Text = ""
            realname.Text = ""
            If objDS.Tables(0).Rows.Count > 0 Then
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "U", objDS, "Interfaces_frmDocumentsTypes")
            Else
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "A", objDS, "Interfaces_frmDocumentsTypes")
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

#End Region

#Region "PageMethods"

    '========================================================================
    'ProcedureName  :GetRecordPermissinAjax  
    'Module         :Hrs (Human Resource Module)
    'Project        :Venus V.
    'Description    :this function get the toolbar permssion By Ajax 
    'Developer      : 
    'Date Created   :23-08-2007
    'Calls          :Java Script  
    'From           :
    'To             :
    'fn. Arguments  :nothing 
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'ClsEmployeesPayability    :Clshrs_EmployeesPayability  :set its values                                                                                                   
    '========================================================================

    <System.Web.Services.WebMethod()>
    Public Shared Function GetRecordPermissionAjax(ByVal recordID As Integer) As String
        Dim StrRetStr As String = "1,1,1"
        Dim dsObjects As New Data.DataSet
        Dim dsRecordsPermissions As New Data.DataSet
        Dim ObjectsID As Int64

        Find("sys_Objects", "Code='hrs_ContractsTransactions'", dsObjects)

        ObjectsID = dsObjects.Tables(0).Rows(0).Item("ID")

        If ObjectsID > 0 And recordID > 0 Then

            Find("sys_RecordsPermissions", "ObjectID=" & ObjectsID & " And RecordID=" & recordID & " And UserID=" & HttpContext.Current.Session("UserID"), dsRecordsPermissions)
            If dsRecordsPermissions.Tables.Count > 0 Then

                If dsRecordsPermissions.Tables(0).Rows.Count > 0 Then

                    With dsRecordsPermissions.Tables(0).Rows(0)
                        If .Item("ID") > 0 Then
                            StrRetStr = ""
                            StrRetStr = IIf(.Item("CanEdit"), "0", "1")
                            StrRetStr &= IIf(.Item("CanDelete"), ",0", ",1")
                            StrRetStr &= IIf(.Item("CanPrint"), ",0", ",1")
                        End If
                    End With
                End If

            End If
        End If
        Return StrRetStr
    End Function

#End Region

End Class
