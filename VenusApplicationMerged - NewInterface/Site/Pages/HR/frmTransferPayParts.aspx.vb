Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports Infragistics.Documents.Reports.Report.Grid
Imports OfficeOpenXml
Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Application.SystemFiles.System

Partial Class frmTransferPayParts
    Inherits MainPage
#Region "Public Decleration"
    Private ObjClssys_Groups As Clssys_Groups
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private ClsEmployees As Clshrs_Employees
    Private ClsUsers As Clssys_Users
    Private _Sys_Report As Clssys_Reports
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ObjClssys_Groups = New Clssys_Groups(Me)
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim StrSerial As String = String.Empty
        Dim ClsEmployeesPayability As Clshrs_EmployeesPayability
        ClsEmployeesPayability = New Clshrs_EmployeesPayability(Me.Page)
        Try

            If Not IsPostBack Then

                ClsEmployeesPayability.Find("ID=" & IntId)
                GetValues(ClsEmployeesPayability)
            End If


            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ObjClssys_Groups.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub


#End Region

#Region "Private Functions"

    Public Function GetValues(ByRef ClsEmployeesPayability As Clshrs_EmployeesPayability) As Boolean
        Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(Page)
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsNationality As New ClsBasicFiles(Page, "sys_Nationalities")
        Dim ClsEmploueesPayabilitySchedules As New Clshrs_EmployeesPayabilitySchedules(Page)
        Try
            With ClsEmployeesPayability
                'If ClsEmployee.Find("ID=" & IIf(.EmployeeID Is Nothing, 0, .EmployeeID)) Then
                '    lblDescEnglishName.Text = ClsEmployee.Name
                '    lblDescEmployeeCode.Text = ClsEmployee.Code
                'End If
                'lblDescLoanCode.Text = .Number
                'lblDescTransD.Text = .TransactionDate
                'lblTransVal.Text = .GetTransactionAmount(.ID) - .GetSettlementsAmount(.ID)
                txtCurrentInstallment.Text = .GetInstalmentAmountNotPaid(.ID)

            End With
            'WebNumericDocumentDo.Text = ""
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Protected Sub btnHR_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnHR.Command

        Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(Page)
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsNationality As New ClsBasicFiles(Page, "sys_Nationalities")
        Dim ClsEmploueesPayabilitySchedules As New Clshrs_EmployeesPayabilitySchedules(Page)
        Dim clsContracts As Clshrs_Contracts = New Clshrs_Contracts(Page)

        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        WebHandler.GetCookies(Page, "UserID", User)
        Dim _sys_User As New Clssys_Users(Page)
        _sys_User.Find("ID = '" & User & "'")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsContracts.ConnectionString)
        If CDec(txtTransValue.Text) >= CDec(txtCurrentInstallment.Text) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "The Trans Value must be less than the installment amount/القيمة المرحلة يجب ان تكون اقل من قيمة القسط"))
            Return
        End If
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim StrSerial As String = String.Empty
        Dim ClsEmployeesPayability As Clshrs_EmployeesPayability
        ClsEmployeesPayability = New Clshrs_EmployeesPayability(Me.Page)
        ClsEmployees = New Clshrs_Employees(Page)
        ClsEmployeesPayability.Find("ID=" & IntId)
        GetValues(ClsEmployeesPayability)
        With ClsEmployeesPayability
            If Not ClsEmploueesPayabilitySchedules.FindPayments("EmployeePayabilityId=" & IIf(.ID Is Nothing, 0, .ID) & " and hrs_EmployeesPayabilitiesSchedules.dueAmount - IsNull((Select Sum(Amount) From hrs_EmployeesPayabilitiesSchedulesSettlement Where hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID=hrs_EmployeesPayabilitiesSchedules.id),0) > 0") Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "No due schedules found / لا يوجد أقساط مستحقة"))
                Return
            End If

            Dim dt As DataTable = ClsEmploueesPayabilitySchedules.DataSet.Tables(0)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "No due schedules found / لا يوجد أقساط مستحقة"))
                Return
            End If

            Dim transValue As Decimal = CDec(txtTransValue.Text)
            Dim currentScheduleId As Integer = CInt(dt.Rows(0)("ID"))
            Dim currentRemaining As Decimal = CDec(dt.Rows(0)("dueAmount"))
            Dim hasNextSchedule As Boolean = (dt.Rows.Count >= 2)
            Dim nextScheduleId As Integer = 0
            If hasNextSchedule Then
                nextScheduleId = CInt(dt.Rows(1)("ID"))
            End If

            If transValue <= 0D Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Invalid transfer value / قيمة الترحيل غير صحيحة"))
                Return
            End If

            If transValue >= currentRemaining Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "The Trans Value must be less than the installment amount/القيمة المرحلة يجب ان تكون اقل من قيمة القسط"))
                Return
            End If

            Dim con As New Data.SqlClient.SqlConnection(clsContracts.ConnectionString)
            con.Open()
            Dim tran As Data.SqlClient.SqlTransaction = con.BeginTransaction()
            Try
                Dim cmdCurrent As New Data.SqlClient.SqlCommand("Update hrs_EmployeesPayabilitiesSchedules Set DueAmount = DueAmount - @Value Where ID = @ID", con, tran)
                cmdCurrent.Parameters.Add(New Data.SqlClient.SqlParameter("@Value", SqlDbType.Money)).Value = transValue
                cmdCurrent.Parameters.Add(New Data.SqlClient.SqlParameter("@ID", SqlDbType.Int)).Value = currentScheduleId
                cmdCurrent.ExecuteNonQuery()

                If hasNextSchedule Then
                    Dim cmdNext As New Data.SqlClient.SqlCommand("Update hrs_EmployeesPayabilitiesSchedules Set DueAmount = DueAmount + @Value Where ID = @ID", con, tran)
                    cmdNext.Parameters.Add(New Data.SqlClient.SqlParameter("@Value", SqlDbType.Money)).Value = transValue
                    cmdNext.Parameters.Add(New Data.SqlClient.SqlParameter("@ID", SqlDbType.Int)).Value = nextScheduleId
                    cmdNext.ExecuteNonQuery()
                Else
                    Dim cmdLast As New Data.SqlClient.SqlCommand("Select Top 1 DueDate, Rank From hrs_EmployeesPayabilitiesSchedules Where EmployeePayabilityId = @EmployeePayabilityId And IsNull(CancelDate,'')='' Order By DueDate Desc, Rank Desc, ID Desc", con, tran)
                    cmdLast.Parameters.Add(New Data.SqlClient.SqlParameter("@EmployeePayabilityId", SqlDbType.Int)).Value = CInt(IIf(.ID Is Nothing, 0, .ID))
                    Dim lastDueDate As Date = Now.Date
                    Dim lastRank As Integer = 0
                    Using rdr As Data.SqlClient.SqlDataReader = cmdLast.ExecuteReader()
                        If rdr.Read() Then
                            If Not IsDBNull(rdr("DueDate")) Then
                                lastDueDate = CDate(rdr("DueDate"))
                            End If
                            If Not IsDBNull(rdr("Rank")) Then
                                lastRank = CInt(rdr("Rank"))
                            End If
                        End If
                    End Using

                    Dim newDueDate As Date = DateAdd(DateInterval.Month, 1, lastDueDate)
                    Dim newRank As Integer = lastRank + 1
                    Dim companyId As Integer = CInt(Page.Session.Item("CompanyID"))
                    Dim regUserId As Integer = CInt(User)

                    Dim cmdInsert As New Data.SqlClient.SqlCommand("Insert Into hrs_EmployeesPayabilitiesSchedules (EmployeePayabilityId, DueDate, DueAmount, Rank, RegDate, RegUserId, RegComputerId, CancelDate, CompanyId) Values (@EmployeePayabilityId, @DueDate, @DueAmount, @Rank, GetDate(), @RegUserId, @RegComputerId, Null, @CompanyId)", con, tran)
                    cmdInsert.Parameters.Add(New Data.SqlClient.SqlParameter("@EmployeePayabilityId", SqlDbType.Int)).Value = CInt(IIf(.ID Is Nothing, 0, .ID))
                    cmdInsert.Parameters.Add(New Data.SqlClient.SqlParameter("@DueDate", SqlDbType.DateTime)).Value = newDueDate
                    cmdInsert.Parameters.Add(New Data.SqlClient.SqlParameter("@DueAmount", SqlDbType.Money)).Value = transValue
                    cmdInsert.Parameters.Add(New Data.SqlClient.SqlParameter("@Rank", SqlDbType.Int)).Value = newRank
                    cmdInsert.Parameters.Add(New Data.SqlClient.SqlParameter("@RegUserId", SqlDbType.Int)).Value = regUserId
                    cmdInsert.Parameters.Add(New Data.SqlClient.SqlParameter("@RegComputerId", SqlDbType.Int)).Value = 0
                    cmdInsert.Parameters.Add(New Data.SqlClient.SqlParameter("@CompanyId", SqlDbType.Int)).Value = companyId
                    cmdInsert.ExecuteNonQuery()
                End If

                tran.Commit()
            Catch
                Try
                    tran.Rollback()
                Catch
                End Try
                Throw
            Finally
                con.Close()
            End Try

            GetValues(ClsEmployeesPayability)
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Transfer completed / تم الترحيل"))
            txtTransValue.Text = "0"
        End With
    End Sub

#End Region
End Class
