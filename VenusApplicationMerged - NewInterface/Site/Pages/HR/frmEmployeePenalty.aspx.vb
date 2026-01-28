Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data.SqlClient

Partial Class frmEmployeePenalty
    Inherits MainPage
#Region "Public Decleration"
    Private ClsDelegationSChedule As ClsSS_DelegationSChedule
    Private ClsSCheduleRequests As ClsSS_DelegationSCheduleRequests

    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Private ClsEmployees As Clshrs_Employees
    Private ClsPositions As Clshrs_Positions
    Private dbOTSalary As Double = 0

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(Page)

        ClsDelegationSChedule = New ClsSS_DelegationSChedule(Me.Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)

            'If ClsObjects.Find(" Code='hrs_Employees'") Then
            '    If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
            '        SearchID = ClsSearchs.ID

            '        Dim IntDimension As Integer = 510
            '        Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
            '        btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            '    End If
            'End If


            txtDelegatedName.Enabled = False

            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                ClsTransactionTypes.GetDropDownList(txtTransactionType, True, " IsDistributable =1 and Sign=-1 and Code=242 ")
                txtCode.Focus()


                Page.Session.Add("ConnectionString", ClsDelegationSChedule.ConnectionString)
                ClsDelegationSChedule.AddOnChangeEventToControls("frmEmployeePenalty", Page, UltraWebTab1)
                'AddNewRow()


                'Dim 
                Dim IntDimension As Integer = 510
                Dim UrlString = ""
                If ClsObjects.Find(" Code='hrs_EmployeePenalty'") Then
                    If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                        SearchID = ClsSearchs.ID
                        IntDimension = 510

                        UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                        btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                    End If
                End If

                ClsObjects.Find(" Code='hrs_Employees'")
                ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
                Dim csSearchID As Integer
                csSearchID = ClsSearchs.ID
                IntDimension = 510
                UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtDelegated.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtDelegated.ClientID & "'"

                btnDelegatedSearch.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                '================================= Exit & Navigation Notification [ End ]

                'Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Dim clsEmployees = New Clshrs_Employees(Page)
                Dim MaxAppIDStr As String = "SELECT ISNULL(MAX(Code), 0) + 1 FROM hrs_EmployeePenalty"
                Dim MaxAppointCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, MaxAppIDStr)

                ' Ensure it's an integer and assign to txtCode.Text
                txtCode.Text = If(IsDBNull(MaxAppointCode), "1", MaxAppointCode.ToString())
                ' Store the ClientID and UniqueID in hidden fields

                txtTransactionDate.Value = DateTime.Now.Date
            End If

            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, "", Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsDelegationSChedule = New ClsSS_DelegationSChedule(Me.Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDelegationSChedule.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If

                If String.IsNullOrWhiteSpace(txtDaysNo.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Days Number /برجاء إدخال عدد الايام"))
                    Exit Sub
                End If

                If txtTransactionType.SelectedIndex <= 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Transaction Type /برجاء إختيار نوع الحركة "))
                    Exit Sub
                End If

                If txtDelegated.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Employee /برجاء اختيار الموظف"))
                    Exit Sub
                End If

                Dim ds As Data.DataSet = GetExistRecord()

                If ds.Tables(0).Rows.Count > 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " The record cannot be modified because it is linked to a discount transaction /لا يمكن التعديل على السجل لارتباطه بحركة خصومات"))
                    Exit Sub
                End If

                SavePart()
                Clear()
                txtCode_TextChanged(Nothing, Nothing)
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If

                If String.IsNullOrWhiteSpace(txtDaysNo.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Days Number /برجاء إدخال عدد الايام"))
                    Exit Sub
                End If

                If txtTransactionType.SelectedIndex <= 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Transaction Type /برجاء إختيار نوع الحركة "))
                    Exit Sub
                End If

                If txtDelegated.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Employee /برجاء اختيار الموظف"))
                    Exit Sub
                End If

                Dim ds As Data.DataSet = GetExistRecord()

                If ds.Tables(0).Rows.Count > 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " The record cannot be modified because it is linked to a discount transaction /لا يمكن التعديل على السجل لارتباطه بحركة خصومات"))
                    Exit Sub
                End If

                If SavePart() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                End If
            Case "New"


                'AfterOperation()
                Dim clsEmployees As New Clshrs_Employees(Page)
                Dim MaxAppIDStr As String = "SELECT ISNULL(MAX(Code), 0) + 1 FROM hrs_EmployeePenalty"
                Dim MaxAppointCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, MaxAppIDStr)

                ' Ensure it's an integer and assign to txtCode.Text
                txtCode.Text = If(IsDBNull(MaxAppointCode), "1", MaxAppointCode.ToString())
                txtCode_TextChanged(Nothing, Nothing)
            Case "Delete"

                Dim clsEmployees As New Clshrs_Employees(Page)
                clsEmployees.Find("Code='" & txtDelegated.Text & "'")
                Dim ClsForms As New ClsSys_Forms(Page)
                ClsForms.Find("EngName = 'frmEmployees.aspx'")
                Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(Me)
                ClsFisicalYearsPeriods.Find("FromDate <= '" & clsEmployees.SetHigriDate(txtTransactionDate.Value) & "' and ToDate >='" & clsEmployees.SetHigriDate(txtTransactionDate.Value) & "'")

                Dim clsFiscalYearsPeriodsModules As New Clssys_FiscalYearsPeriodsModules(Page)
                clsFiscalYearsPeriodsModules.Find(" FiscalYearPeriodID=" & ClsFisicalYearsPeriods.ID & " and ModuleID=" & ClsForms.ModuleID)
                'Dim IntSelectedPeriod = ClsFisicalYearsPeriods.GetLastOpenedFiscalPieriod(ClsForms.ModuleID)
                If Not String.IsNullOrWhiteSpace(Convert.ToString(clsFiscalYearsPeriodsModules.CloseDate)) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Penalty Date in Closed Period !/!تاريخ الجزاء في فترة مغلقة"))
                    Return
                End If

                Dim periodSql As String
                periodSql = "select count(hrs_EmployeesTransactions.ID) from hrs_EmployeesTransactions where hrs_EmployeesTransactions.PrepareType='N' and hrs_EmployeesTransactions.FiscalYearPeriodID=" & ClsFisicalYearsPeriods.ID & " and hrs_EmployeesTransactions.EmployeeID=" & clsEmployees.ID
                Dim periodPrepared As Integer
                periodPrepared = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, periodSql)
                If periodPrepared > 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Penalty Date in Prepared Period !/!تاريخ الجزاء في فترة مجهزة"))
                    Return
                End If

                Dim RequestSerial As Integer = txtCode.Text

                Dim notes As String = "penality Code= " & RequestSerial
                Dim CancelPayabilities = " UPDATE [dbo].[hrs_EmployeesPayabilities] SET [CancelDate] = getdate() WHERE [EmployeeID] = " & clsEmployees.ID & " and [TransactionComment] like '" & notes & "%'"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployees.ConnectionString, Data.CommandType.Text, CancelPayabilities)
                Dim currentIdQuery As String = ""

                currentIdQuery = "DELETE FROM [dbo].[hrs_EmployeePenalty] where Code=" & txtCode.Text
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployees.ConnectionString, Data.CommandType.Text, currentIdQuery)

                'Dim clsEmployees As New Clshrs_Employees(Page)
                Dim MaxAppIDStr As String = "SELECT ISNULL(MAX(Code), 0) + 1 FROM hrs_EmployeePenalty"
                Dim MaxAppointCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, MaxAppIDStr)

                ' Ensure it's an integer and assign to txtCode.Text
                txtCode.Text = If(IsDBNull(MaxAppointCode), "1", MaxAppointCode.ToString())
                txtCode_TextChanged(Nothing, Nothing)
            Case "Property"
                If ClsDelegationSChedule.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsDelegationSChedule.ID & "&TableName=" & ClsDelegationSChedule.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                End If
            Case "Remarks"
                If ClsDelegationSChedule.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsDelegationSChedule.ID & "&TableName=" & ClsDelegationSChedule.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                End If

            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"

            Case "Exit"

            Case "First"
                Dim clsEmployees As New Clshrs_Employees(Page)
                Dim StrSelectCommand = "SELECT TOP 1 Code FROM hrs_EmployeePenalty ORDER BY Code ASC"
                Dim FirstCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, StrSelectCommand)
                txtCode.Text = FirstCode
                txtCode_TextChanged(Nothing, Nothing)
            Case "Previous"
                Dim clsEmployees As New Clshrs_Employees(Page)
                Dim StrSelectCommand = "SELECT TOP 1 Code FROM hrs_EmployeePenalty where Code<" & txtCode.Text & " ORDER BY Code Desc"
                Dim FirstCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, StrSelectCommand)
                If FirstCode = Nothing Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                Else
                    txtCode.Text = FirstCode
                    txtCode_TextChanged(Nothing, Nothing)
                End If

            Case "Next"
                Dim clsEmployees As New Clshrs_Employees(Page)
                Dim StrSelectCommand = "SELECT TOP 1 Code FROM hrs_EmployeePenalty where Code>" & txtCode.Text & " ORDER BY Code ASC"
                Dim FirstCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, StrSelectCommand)
                If FirstCode = Nothing Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                Else
                    txtCode.Text = FirstCode
                    txtCode_TextChanged(Nothing, Nothing)
                End If

            Case "Last"
                Dim clsEmployees As New Clshrs_Employees(Page)
                Dim StrSelectCommand = "SELECT TOP 1 Code FROM hrs_EmployeePenalty  ORDER BY Code Desc"
                Dim FirstCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, StrSelectCommand)

                txtCode.Text = FirstCode
                txtCode_TextChanged(Nothing, Nothing)
        End Select
    End Sub

    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()

    End Sub

#End Region

#Region "Private Functions"

    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim ClsEmployee = New Clshrs_Employees(Page)
        ClsEmployee.Find(" Code='" & txtDelegated.Text & "'")

        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim User As String = String.Empty
        WebHandler.GetCookies(Page, "UserID", User)
        Dim _sys_User As New Clssys_Users(Page)
        Dim NewRec As Boolean = True
        Dim ds As Data.DataSet = GetExistRecord()

        If ds.Tables(0).Rows.Count > 0 Then
            NewRec = False
        Else
            NewRec = True
        End If

        ' Convert inputs to correct types
        Dim code As Integer = Convert.ToInt32(txtCode.Text)
        Dim employeeId As Integer = ClsEmployee.ID
        Dim penaltyDate As Date = Convert.ToDateTime(txtTransactionDate.Text)
        Dim transactionTypeId As Integer = Convert.ToInt32(txtTransactionType.SelectedValue)
        Dim daysNo As Double = Convert.ToDouble(txtDaysNo.Text)
        Dim notes As String = TxtRemarks.Text
        Dim regUserId As Integer = Convert.ToInt32(User)
        SaveEmployeePenalty(code, employeeId, penaltyDate, transactionTypeId, daysNo, notes, regUserId)



        If NewRec Then
            SavePenaltyPayabilities(employeeId)
        End If

        Return True
    End Function


    Public Function SaveEmployeePenalty(code As Integer,
                                    employeeId As Integer,
                                    penaltyDate As Date,
                                    transactionTypeId As Integer,
                                    daysNo As Double,
                                    notes As String,
                                    regUserId As Integer) As Integer

        Dim ClsEmployee = New Clshrs_Employees(Page)
        Dim connString As String = ClsEmployee.ConnectionString
        Dim newId As Integer

        Using conn As New SqlConnection(connString)
            conn.Open()

            Dim sql As String =
"IF EXISTS (SELECT 1 FROM hrs_EmployeePenalty WHERE Code = @Code)" & vbCrLf &
"BEGIN" & vbCrLf &
"    UPDATE hrs_EmployeePenalty" & vbCrLf &
"    SET EmployeeId = @EmployeeId," & vbCrLf &
"        PenaltyDate = @PenaltyDate," & vbCrLf &
"        TransactionTypeId = @TransactionTypeId," & vbCrLf &
"        DaysNo = @DaysNo," & vbCrLf &
"        Notes = @Notes," & vbCrLf &
"        RegUserID = @RegUserID," & vbCrLf &
"        RegDate = GETDATE()" & vbCrLf &
"    WHERE Code = @Code;" & vbCrLf &
"" & vbCrLf &
"    SELECT Id FROM hrs_EmployeePenalty WHERE Code = @Code;" & vbCrLf &
"END" & vbCrLf &
"ELSE" & vbCrLf &
"BEGIN" & vbCrLf &
"    INSERT INTO hrs_EmployeePenalty" & vbCrLf &
"        (Code, EmployeeId, PenaltyDate, TransactionTypeId, DaysNo, Notes, RegUserID, RegDate)" & vbCrLf &
"    VALUES" & vbCrLf &
"        (@Code, @EmployeeId, @PenaltyDate, @TransactionTypeId, @DaysNo, @Notes, @RegUserID, GETDATE());" & vbCrLf &
"" & vbCrLf &
"    SELECT SCOPE_IDENTITY();" & vbCrLf &
"END"

            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@Code", code)
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId)
                cmd.Parameters.AddWithValue("@PenaltyDate", penaltyDate)
                cmd.Parameters.AddWithValue("@TransactionTypeId", transactionTypeId)
                cmd.Parameters.AddWithValue("@DaysNo", daysNo)
                cmd.Parameters.AddWithValue("@Notes", notes)
                cmd.Parameters.AddWithValue("@RegUserID", regUserId)

                newId = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using

        Return newId
    End Function

    Private Function SavePenaltyPayabilities(ByVal employeeId As Integer) As Boolean
        'AssignValues()
        Dim ClsEmployeesPayability As New Clshrs_EmployeesPayability(Page)
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim StrSerial As String = ""

        ClsEmployeesPayability.EmployeeID = employeeId

        ObjDataHandler.GetLastSerial(ClsEmployeesPayability.Table, "Number", StrSerial, ClsEmployeesPayability.ConnectionString, "00000")
        Dim ClsTransactionsType As New Clshrs_TransactionsTypes(Page)
        ClsTransactionsType.Find(" Code=242")

        ClsEmployeesPayability.TransactionTypeID = ClsTransactionsType.ID
        ClsEmployeesPayability.Number = StrSerial
        ClsEmployeesPayability.TransactionDate = ClsEmployeesPayability.SetHigriDate(txtTransactionDate.Value)
        ClsEmployeesPayability.TransactionComment = TxtRemarks.Text
        ClsEmployeesPayability.SalaryLink = "True"

        Dim RequestSerial As Integer = txtCode.Text

        Dim notes As String = "penality Code= " & RequestSerial & " .. Due date=" & ClsEmployeesPayability.SetHigriDate(txtTransactionDate.Value) & " .. Days=" & txtDaysNo.Text
        ClsEmployeesPayability.TransactionComment = notes

        Dim PayID As Integer = ClsEmployeesPayability.Save()
        SaveInstalment(PayID, employeeId)
        'ClsEmployeesExcuses.Save()
        Return True
    End Function
    Private Function SaveInstalment(ByVal IntId As Integer, ByVal empId As Integer) As Boolean
        Dim ObjRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
        Dim StrSqlCommand As String
        Try

            Dim clsEmployeesPayability As New Clshrs_EmployeesPayability(Me.Page)
            clsEmployeesPayability.Find("ID = " & IntId)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployeesPayability.ConnectionString)
            '1- find total amount
            ClsEmployees = New Clshrs_Employees(Me.Page)
            Dim fiscalperiood As New Clssys_FiscalYearsPeriods(Me.Page)
            fiscalperiood.Find("FromDate <= '" & ClsEmployees.SetHigriDate(txtTransactionDate.Value) & "' and ToDate >='" & ClsEmployees.SetHigriDate(txtTransactionDate.Value) & "'")
            Dim IntFisicalPeriod = fiscalperiood.ID

            Dim intContractId As Integer = CheckEmployee(empId, IntFisicalPeriod)
            Dim clscontracts As New Clshrs_Contracts(Me.Page)
            Dim clsemployeeclass As New Clshrs_EmployeeClasses(Me.Page)

            clscontracts.Find("ID = " & intContractId)
            clsemployeeclass.Find("ID =" & clscontracts.EmployeeClassID)
            Dim FromDate = fiscalperiood.FromDate
            Dim ToDate = fiscalperiood.ToDate
            Dim FiscFromDate = fiscalperiood.FromDate
            Dim FiscToDate = fiscalperiood.ToDate

            Dim arrList As New ArrayList
            arrList = GetEmployeeGeneralInfromation(empId, IntFisicalPeriod)
            Dim ObjPrepaerdData = ClsEmployees.GetPreparedEmployessForSalariesByEmployeeID(IntFisicalPeriod, empId, FromDate.ToString("dd/MM/yyyy"), ToDate.ToString("dd/MM/yyyy"), FiscFromDate.ToString("dd/MM/yyyy"), FiscToDate.ToString("dd/MM/yyyy"))

            Dim Amount As Double = 0
            Calculate_Salary(Amount, empId, ClsEmployees.SetHigriDate(txtTransactionDate.Value))

            Dim IntNoOfDays As Integer
            If ObjPrepaerdData(9) = 1 Then
                IntNoOfDays = ObjPrepaerdData(8)
            ElseIf ObjPrepaerdData(9) > 1 Then
                IntNoOfDays = ObjPrepaerdData(9)
            Else
                IntNoOfDays = ObjPrepaerdData(8)
            End If



            Dim ObjSalaryPerDay = Math.Round(Amount / IntNoOfDays, 2)
            'Dim ObjSalaryPerHour = Math.Round(ObjSalaryPerDay / clsemployeeclass.WorkHoursPerDay, 2)

            Dim ClsSolver As New Clshrs_FormulaSolver(clsemployeeclass.ConnectionString, Me.Page)
            ClsSolver.EmployeeID = empId
            ClsSolver.NoOfDaysPerPeriod = IntNoOfDays
            ClsSolver.NoOfWorkingDays = 30

            ClsSolver.EvaluateExpression(clsemployeeclass.AbsentFormula, 0)
            dbOTSalary = ClsSolver.Output


            'Dim ObjOverTime = Math.Round(ObjSalaryPerHour * clsemployeeclass.OvertimeFactor, 2)


            StrSqlCommand = "Set DateFormat DMY; Delete From hrs_EmployeesPayabilitiesSchedules Where ID not in(Select EmployeePayabilityScheduleID from hrs_EmployeesPayabilitiesSchedulesSettlement) and EmployeePayabilityId=" & IntId & ";" & vbNewLine
            StrSqlCommand &= " update hrs_EmployeesPayabilitiesSchedules set DueAmount =s.amount from hrs_EmployeesPayabilitiesSchedules p inner join (select EmployeePayabilityScheduleID,sum(Amount)amount  from hrs_EmployeesPayabilitiesSchedulesSettlement group by EmployeePayabilityScheduleID ) s on p.id=s.EmployeePayabilityScheduleID where p.DueAmount-s.Amount >0 and p.EmployeePayabilityID=" & clsEmployeesPayability.ID & ";" & vbNewLine
            Dim LoanAmount As Double = clsEmployeesPayability.GetTransactionAmount(clsEmployeesPayability.ID) - clsEmployeesPayability.GetSettlementAmount(clsEmployeesPayability.ID)

            'Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployeesPayability.ConnectionString, System.Data.CommandType.Text, StrSqlCommand)
            Dim OriginalLaonAmount As Double = LoanAmount

            Dim settlemntAmount As Double
            settlemntAmount = dbOTSalary * Convert.ToDouble(txtDaysNo.Text)

            If settlemntAmount > 0 Then
                StrSqlCommand &= "Insert Into hrs_EmployeesPayabilitiesSchedules(" &
                                                                      "EmployeePayabilityId," &
                                                                      "DueDate," &
                                                                      "DueAmount," &
                                                                      "RegUserID," &
                                                                      "CompanyId" &
                                                                      ")values(" &
                                                                      IntId & ",'" &
                                                                      txtTransactionDate.Value & "'," &
                                                                      IIf(settlemntAmount = Nothing, 0, settlemntAmount) & "," &
                                                                      clsEmployeesPayability.DataBaseUserRelatedID & "," &
                                                                      clsEmployeesPayability.MainCompanyID &
                                                                      ");" & vbNewLine
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployeesPayability.ConnectionString, System.Data.CommandType.Text, StrSqlCommand)
            End If





        Catch ex As Exception
            Dim x = ex.ToString()
        End Try
    End Function

    Private Function CheckEmployee(ByVal IntInComingEmployeeID As Integer, ByVal FiscalPeriodID As Integer) As Integer
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsContract As New Clshrs_Contracts(Page)
        Dim clsEmployeevacations As New Clshrs_EmployeesVacations(Page)
        Dim ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
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

            If (clsEmployeevacations.FindEmployeeVacations(" hrs_EmployeesVacations.VacationTypeID in (select ID from hrs_VacationsTypes where CancelDate is null and IsAnnual <> 1 and HasPayment=1) and hrs_EmployeesVacations.EmployeeID=" & ClsEmployees.ID & " And Convert(smalldatetime,Convert(varchar,ActualStartDate ,103)) <= Convert(smalldatetime,Convert(varchar,'" & ClsFiscalPeriods.FromDate & "' ,103))	And	(ActualEndDate Is Null Or  Convert(smalldatetime,Convert(varchar,ActualEndDate ,103)) > Convert(smalldatetime,Convert(varchar,'" & ClsFiscalPeriods.ToDate & "',103)))")) Then
                Return 0
            End If
            Return intValidContract
        Catch ex As Exception
            Return 0
        End Try
    End Function


    Private Function Calculate_Salary(ByRef Amount As Double, empID As Integer, ByRef TrnsDate As DateTime) As Boolean
        Try
            Dim clsCompanies As New Clssys_Companies(Me)
            Dim ClsEmployeesContracts = New Clshrs_Contracts(Me.Page)
            ClsEmployees = New Clshrs_Employees(Page)
            clsCompanies.Find("ID=" & clsCompanies.MainCompanyID)
            If clsCompanies.SalaryCalculation = 0 Then            'Get Basic Salary
                Dim dbBasicSalary = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, "set dateformat dmy; select dbo.fn_GetBasicSalary(" & ClsEmployeesContracts.ContractValidatoinId(empID, TrnsDate) & ",'" & TrnsDate.ToString("dd/MM/yyyy") & "')")
                Amount = dbBasicSalary
            ElseIf clsCompanies.SalaryCalculation = 1 Then        'Get Total Salary By Days
                Dim dbBasicSalary = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, "set dateformat dmy; select dbo.fn_GetTotalAdditions(" & ClsEmployeesContracts.ContractValidatoinId(empID, TrnsDate) & ",'" & TrnsDate.ToString("dd/MM/yyyy") & "')")
                Amount = dbBasicSalary
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetEmployeeGeneralInfromation(ByVal intEmployeeID As Object, ByVal FiscalPeriodID As Integer) As ArrayList
        Dim EmpInfoarrList As New ArrayList
        Dim IntFiscalPeriod As Integer = FiscalPeriodID
        Dim IntContractID As Integer
        Dim IntEmpClassID As Integer
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsContractTransaction As New Clshrs_ContractsTransactions(Page)
        Dim ClsContract As New Clshrs_Contracts(Page)
        Dim clsTransactionsTypes As New Clshrs_TransactionsTypes(Page)
        Dim ClsEmployeeClass = New Clshrs_EmployeeClasses(Page)

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
        Dim ClsEmployeeClass = New Clshrs_EmployeeClasses(Page)
        Dim ClsFiscalPeriods As New Clssys_FiscalYearsPeriods(Page)

        ClsEmployeeClass.Find(" ID= " & EmpClassID)
        SinWorkHours = ClsEmployeeClass.WorkHoursPerDay
        'SinOverTimeFactor = IIf(ClsEmployeeClass.OvertimeFactor.ToString = "", 0, ClsEmployeeClass.OvertimeFactor)
        arrList.Add(SinWorkHours)
    End Sub
    Private Function AssignValues(ByRef ClsBasicFiles As Object) As Boolean
        Try
            Dim CLSEMP As Clshrs_Employees = New Clshrs_Employees(Page)

            Dim ClsEmployees As Clshrs_Employees = New Clshrs_Employees(Page)
            ClsEmployees.Find("Code='" & txtDelegated.Text & "'")
            With ClsDelegationSChedule
                .Code = txtCode.Text
                .DelegatorEmployeeID = CLSEMP.ID
                .DelegatedEmployeeID = ClsEmployees.ID
                'If Convert.ToString(txtFromDate.Value) <> "" Then
                '    .FromDate = Convert.ToDateTime(txtFromDate.Value).Date
                'End If
                'If Convert.ToString(txtTodate.Value) <> "" Then
                '    .Todate = Convert.ToDateTime(txtTodate.Value).Date
                'End If





            End With
            Return True
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsDelegationSChedule.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, "", Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
            Return False
        End Try
    End Function
    Private Function GetValues(ByVal ClsDelegationSChedule As ClsSS_DelegationSChedule) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()

            Dim CLSEMP As Clshrs_Employees = New Clshrs_Employees(Page)
            CLSEMP.Find("ID='" & ClsDelegationSChedule.DelegatorEmployeeID & "'")

            Dim ClsEmployees As Clshrs_Employees = New Clshrs_Employees(Page)
            ClsEmployees.Find("ID='" & ClsDelegationSChedule.DelegatedEmployeeID & "'")
            With ClsDelegationSChedule
                txtCode.Text = .Code
                txtCode.Text = .Code
                txtDelegated.Text = ClsEmployees.Code



                If Not ClsDelegationSChedule.DelegatedEmployeeID = Nothing Then
                    txtDelegated_TextChanged(Nothing, Nothing)
                End If
                If Not ClsDelegationSChedule.RegUserID = Nothing Then
                    ClsUser.Find("ID=" & ClsDelegationSChedule.RegUserID)
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
                Dim item As New System.Web.UI.WebControls.ListItem()


                If (.ID > 0) Then
                    StrMode = "E"
                Else
                    StrMode = "N"
                End If
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                End If

            End With
            Return True
        Catch ex As Exception
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
                    ClsDelegationSChedule.Find("ID=" & intID)
                    GetValues(ClsDelegationSChedule)
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsDelegationSChedule.Find("ID=" & intID)
                    GetValues(ClsDelegationSChedule)
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsDelegationSChedule
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)

            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation() As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsDelegationSChedule = New ClsSS_DelegationSChedule(Me.Page)
        Try
            With ClsDelegationSChedule
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Page, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsDelegationSChedule = New ClsSS_DelegationSChedule(Me.Page)
        If IntId > 0 Then
            ClsDelegationSChedule.Find("ID=" & IntId)
            GetValues(ClsDelegationSChedule)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim ClsEmployee As New Clshrs_Employees(Page)

        Try
            Dim ds As Data.DataSet = GetExistRecord()

            If ds.Tables(0).Rows.Count > 0 Then


                Dim empId = ds.Tables(0).Rows(0).Item("EmployeeId")
                ClsEmployee.Find(" Id=" & empId)
                If ClsEmployee.Code > 0 Then
                    txtDelegated.Text = ClsEmployee.Code
                    txtDelegated_TextChanged(Nothing, Nothing)

                End If
                txtTransactionType.SelectedValue = Convert.ToInt32(ds.Tables(0).Rows(0).Item("TransactionTypeId"))
                txtTransactionDate.Value = ds.Tables(0).Rows(0).Item("PenaltyDate")
                txtDaysNo.Text = ds.Tables(0).Rows(0).Item("DaysNo")
                TxtRemarks.Text = ds.Tables(0).Rows(0).Item("Notes")

                StrMode = "E"
            Else
                'txtCode.Text = ""
                txtCode.Focus()

                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"

            End If
            SetToolBarDefaults()
            If Not lblCancelDateValue.Text = "" Then
                ImageButton_Delete.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Function

    Private Function GetExistRecord() As Data.DataSet
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim sql = "SELECT  Id, Code, EmployeeId, PenaltyDate, TransactionTypeId, DaysNo, Notes FROM [dbo].[hrs_EmployeePenalty] where Code='" & txtCode.Text & "'"
        Dim ds As New Data.DataSet
        ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, Data.CommandType.Text, sql)
        Return ds
    End Function

    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        ClsDelegationSChedule.Clear()
        GetValues(ClsDelegationSChedule)
        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        'txtEngName.Text = String.Empty
        'txtArbName.Text = String.Empty
        txtDelegated.Text = String.Empty
        txtDelegatedName.Text = String.Empty
        TxtPositionID.Value = String.Empty
        txtDaysNo.Text = ""
        TxtRemarks.Text = ""
        txtTransactionType.SelectedIndex = -1
        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
        txtTransactionDate.Value = DateTime.Now.Date

        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim User As String = String.Empty
        Dim ClsEmployees = New Clshrs_Employees(Page)
        txtDelegated_TextChanged(Nothing, Nothing)
        Dim MaxAppIDStr As String = "SELECT ISNULL(MAX(Code), 0) + 1 FROM hrs_EmployeePenalty"
        Dim MaxAppointCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, MaxAppIDStr)

        ' Ensure it's an integer and assign to txtCode.Text
        txtCode.Text = If(IsDBNull(MaxAppointCode), "1", MaxAppointCode.ToString())
        'txtCode_TextChanged(Nothing, Nothing)
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsDelegationSChedule = New ClsSS_DelegationSChedule(Me.Page)
        ClsDelegationSChedule.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsDelegationSChedule.ID
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




    Protected Sub txtDelegated_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDelegated.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtDelegated.Text) Then
                Dim ClsEmployees As Clshrs_Employees
                ClsEmployees = New Clshrs_Employees(Page)
                Dim EmpName As String
                If ProfileCls.CurrentLanguage = "Ar" Then
                    EmpName = " isnull( hrs_Employees.arbname ,' ')+' '+ isnull(hrs_Employees.FatherArbName, ' ')+' '+ isnull(hrs_Employees.GrandArbName,' ')+' '+isnull(hrs_Employees.FamilyArbName,' ') "

                Else
                    EmpName = " isnull(hrs_Employees.EngName,' ')+' '+isnull(hrs_Employees.FatherEngName,' ')+' '+isnull(hrs_Employees.GrandEngName ,' ')+' '+isnull(hrs_Employees.FamilyEngName,' ')"

                End If
                Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

                Dim DS2 As New Data.DataSet()
                Dim connetionString2 As String
                Dim connection2 As Data.SqlClient.SqlConnection
                Dim command2 As Data.SqlClient.SqlCommand
                Dim adapter2 As New Data.SqlClient.SqlDataAdapter
                connetionString2 = ClsEmployees.ConnectionString
                connection2 = New Data.SqlClient.SqlConnection(connetionString2)
                Dim strselect As String
                Dim cond = ""

                strselect = "select " & EmpName & "  FROM  Hrs_Employees where Code='" & txtDelegated.Text & "'" & cond
                'command2 = New Data.SqlClient.SqlCommand(strselect, connection2)

                Dim AlternativeName As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, strselect)
                ClsEmployees.Find("Code='" & txtDelegated.Text & "'" & cond)
                If ClsEmployees.ID > 0 Then

                    txtDelegatedName.Text = AlternativeName
                Else
                    txtDelegatedName.Text = ""
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Sorry there is no employee with this code !/!عفوا لا يوجد موظف مسجل بهذا الكود"))

                End If
            Else
                txtDelegatedName.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub




#End Region
End Class
