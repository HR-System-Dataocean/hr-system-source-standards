Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.SessionState
Imports Microsoft.VisualBasic.ApplicationServices
Imports Stimulsoft.Base
Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Application.SystemFiles.System

Partial Class frmChangeJoinDate
    Inherits MainPage
    Implements IRequiresSessionState

#Region "Public Decleration"
    Private mErrorHandler As New Venus.Shared.ErrorsHandler
    Private ClsEmployees As Clshrs_Employees
    Private ClsContract As Clshrs_Contracts
    Private ObjNavigationHandler As Venus.Shared.Web.NavigationHandler
    Private ClsEmployeesVacations As Clshrs_EmployeesVacations
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ClsEmployees = New Clshrs_Employees(Page)
            ClsContract = New Clshrs_Contracts(Page)
            ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

            Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)

            ' ---- إعداد زرار البحث - لازم يتعمل في كل Load مش بس IsPostBack ----
            SetupSearchButton()

            txtEmployeeCode.Attributes.Add("onchange", "ChangeIsDataChanged()")
            ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load",
                "<script language=""javascript"">IntializeDataChanged()</script>")

            If Not IsPostBack Then
                SetLanguageMessages()
                LoadClasses()
                SetScreenInformation()
            End If

        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler()
            If Session IsNot Nothing Then
                Page.Session.Add("ErrorValue", ex)
            End If
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0,
                Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    ' =============================================
    ' إعداد زرار البحث بشكل صحيح
    ' =============================================
    Private Sub SetupSearchButton()
        Try
            Dim csSearchID As Integer = 0

            ' جلب SearchID من الداتابيز
            Dim ClsObjects As New Clssys_Objects(Page)
            Dim ClsSearchs As New Clssys_Searchs(Page)

            Dim tableCode As String = ClsEmployees.Table.Trim()

            If ClsObjects.Find("Code='" & tableCode & "'") Then
                If ClsObjects.ID > 0 Then
                    If ClsSearchs.Find("ObjectID=" & ClsObjects.ID) Then
                        csSearchID = ClsSearchs.ID
                    End If
                End If
            End If

            ' لو مش لاقي SearchID يحاول بطريقة تانية
            If csSearchID = 0 Then
                If ClsSearchs.Find("ObjectID IN (SELECT ID FROM sys_Objects WHERE Code='" & tableCode & "')") Then
                    csSearchID = ClsSearchs.ID
                End If
            End If

            ' بناء الـ URL
            Dim intDimension As Integer = 510
            Dim urlString As String = "'frmModalSearchScreen.aspx" &
                                      "?TargetControl=" & txtEmployeeCode.ID &
                                      "&SearchID=" & csSearchID &
                                      "&'," & intDimension & ",720,false,'" &
                                      txtEmployeeCode.ClientID & "'"

            ' تعيين الـ Click Event على زرار البحث
            btnSearchEmployee.ClientSideEvents.Click = "OpenModal1(" & urlString & ")"

            ' أيضاً أضف onclick مباشر كـ fallback
            btnSearchEmployee.Attributes.Add("onclick",
                "OpenModal1(" & urlString & "); return false;")

        Catch ex As Exception
            ' لو حصل خطأ في إعداد البحث مش هيوقف الشاشة
            Try
                btnSearchEmployee.ClientSideEvents.Click =
                    "alert('Search setup error: " & ex.Message.Replace("'", "") & "'); return false;"
            Catch
            End Try
        End Try
    End Sub

    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles _
        ImageButton_Save.Command, ImageButton_New.Command, ImageButton_Print.Command,
        ImageButton_Properties.Command, LinkButton_Properties.Command,
        ImageButton_Remarks.Command, LinkButton_Remarks.Command,
        ImageButton_Last.Command, ImageButton_Next.Command,
        ImageButton_Back.Command, ImageButton_First.Command

        Select Case e.CommandArgument

            Case "New"
                ClearForm()

            Case "Save"
                If Not ValidateBeforeSave() Then Exit Sub

                If SaveChanges() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                        ObjNavigationHandler.SetLanguage(Page,
                            " Join Date Changed Successfully /تم تغيير تاريخ المباشرة بنجاح"))
                    ClearForm()
                End If

            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)

            Case "Property"
                If ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page,
                        "frmPropertyScreen.aspx?ID=" & ClsEmployees.ID &
                        "&TableName=" & ClsEmployees.Table,
                        477, 313, False,
                        Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                End If

            Case "Remarks"
                If ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page,
                        "frmRemarks.aspx?ID=" & ClsEmployees.ID &
                        "&TableName=" & ClsEmployees.Table,
                        410, 210, False,
                        Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                End If

            Case "First", "Previous", "Next", "Last"
                NavigateEmployee(e.CommandArgument.ToString())

        End Select
    End Sub

    Protected Sub txtEmployeeCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmployeeCode.TextChanged
        LoadEmployeeData()
    End Sub

#End Region

#Region "Private Functions"

    ' =============================================
    ' التحقق من البيانات قبل الحفظ
    ' =============================================
    Private Function ValidateBeforeSave() As Boolean
        If txtEmployeeCode.Text.Trim() = "" Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                ObjNavigationHandler.SetLanguage(Page,
                    " Please Enter Employee Code /برجاء إدخال كود الموظف"))
            Return False
        End If

        If txtNewJoinDate.Value = "" Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                ObjNavigationHandler.SetLanguage(Page,
                    " Please Enter New Join Date /برجاء إدخال تاريخ المباشرة الجديد"))
            Return False
        End If

        If txtReasonJoinDate.Text.Trim() = "" Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                ObjNavigationHandler.SetLanguage(Page,
                    " Please Enter Reason for Changing Join Date /برجاء إدخال سبب تغيير تاريخ المباشرة"))
            Return False
        End If

        If rdoZeroVacation.Checked AndAlso txtReasonVacation.Text.Trim() = "" Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                ObjNavigationHandler.SetLanguage(Page,
                    " Please Enter Reason for Zeroing Vacation Balance /برجاء إدخال سبب تصفير رصيد الاجازات"))
            Return False
        End If

        Return True
    End Function

    ' =============================================
    ' رسائل اللغة
    ' =============================================
    Private Sub SetLanguageMessages()
        lblEmployeeCodeMsg.Text = ObjNavigationHandler.SetLanguage(Page,
            "Employee Code is required / كود الموظف مطلوب")
        lblNewJoinDateMsg.Text = ObjNavigationHandler.SetLanguage(Page,
            "New Join Date is required / تاريخ المباشرة الجديد مطلوب")
        lblReasonJoinDateMsg.Text = ObjNavigationHandler.SetLanguage(Page,
            "Reason for changing Join Date is required / سبب تغيير تاريخ المباشرة مطلوب")
        lblReasonVacationMsg.Text = ObjNavigationHandler.SetLanguage(Page,
            "Reason for changing Vacation Balance is required / سبب تغيير رصيد الاجازات مطلوب")
        lblValidationTitle.Text = ObjNavigationHandler.SetLanguage(Page,
            "Please complete the following data / الرجاء إكمال البيانات التالية")
        lblConfirmMsg.Text = ObjNavigationHandler.SetLanguage(Page,
            "Are you sure you want to change the Join Date? / هل أنت متأكد من رغبتك في تغيير تاريخ المباشرة؟")
    End Sub

    ' =============================================
    ' تحميل الفئات
    ' =============================================
    Private Sub LoadClasses()
        Try
            Dim clsClasses As New Clshrs_EmployeeClasses(Page)
            clsClasses.GetDropDownList(ddlNewCategory, False)
        Catch ex As Exception
        End Try
    End Sub

    ' =============================================
    ' تحميل بيانات الموظف
    ' =============================================
    Private Sub LoadEmployeeData()
        Try
            If txtEmployeeCode.Text.Trim() <> "" Then
                If ClsEmployees.Find("Code='" & txtEmployeeCode.Text.Trim() & "'") Then
                    txtEmployeeName.Text = ClsEmployees.EnglishName
                    txtCurrentJoinDate.Text = ClsEmployees.JoinDate
                    txtLastSalary.Text = GetLastSalary(ClsEmployees.ID)
                    LoadVacationBalances(ClsEmployees.ID)
                    Dim str As String = "select top(1) sys_FiscalYearsPeriods.EngName from  hrs_employeestransactions  join sys_FiscalYearsPeriods on hrs_employeestransactions.FiscalYearPeriodID =sys_FiscalYearsPeriods.ID where employeeid=" & ClsEmployees.ID & " order by sys_FiscalYearsPeriods.ID desc"

                    Dim LastSalary = Convert.ToString(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str))

                    txtLastSalary.Text = LastSalary


                    Dim validcontractid As Integer = ClsContract.ContractValidatoinId(ClsEmployees.ID, DateTime.Now)
                    If True Then

                    End If


                Else
                    ClearForm()
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                        ObjNavigationHandler.SetLanguage(Page,
                            " Employee Not Found / الموظف غير موجود"))
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ' =============================================
    ' تحميل أرصدة الإجازات
    ' =============================================
    Private Sub LoadVacationBalances(ByVal EmployeeID As Integer)
        Try
            Dim dt As DataTable = GetVacationBalances(EmployeeID)

            Dim annualBalance As Decimal = 0
            Dim transferredBalance As Decimal = 0
            Dim annualExpireDate As String = ""
            Dim transferredExpireDate As String = ""

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    Dim balanceTypeID As Integer = Convert.ToInt32(row("BalanceTypeID"))
                    Dim balance As Decimal = Convert.ToDecimal(row("Remaining"))
                    Dim expDate As DateTime = Convert.ToDateTime(row("ExpireDate"))

                    If balanceTypeID = 1 Then
                        annualBalance = balance
                        annualExpireDate = expDate.ToString("dd/MM/yyyy")
                    ElseIf balanceTypeID = 2 Then
                        transferredBalance = balance
                        transferredExpireDate = expDate.ToString("dd/MM/yyyy")
                    End If
                Next
            End If

            txtAnnualVacation.Text = annualBalance.ToString("N2")
            txtAnnualExpireDate.Text = annualExpireDate
            txtTransferredVacation.Text = transferredBalance.ToString("N2")
            txtTransferredExpireDate.Text = transferredExpireDate
            txtCurrentBalance.Text = (annualBalance + transferredBalance).ToString("N2")

        Catch ex As Exception
            txtAnnualVacation.Text = "0.00"
            txtAnnualExpireDate.Text = ""
            txtTransferredVacation.Text = "0.00"
            txtTransferredExpireDate.Text = ""
            txtCurrentBalance.Text = "0.00"
        End Try
    End Sub

    ' =============================================
    ' جلب أرصدة الإجازات من الداتابيز
    ' =============================================
    Private Function GetVacationBalances(ByVal EmployeeID As Integer) As DataTable
        Try
            Dim sql As String = "SELECT BalanceTypeID, Remaining, ExpireDate " &
                                "FROM hrs_VacationsBalance " &
                                "WHERE EmployeeID = " & EmployeeID &
                                "  AND ExpireDate > GETDATE() " &
                                "  AND CancelDate IS NULL"

            Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(
                ClsEmployees.ConnectionString, CommandType.Text, sql).Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' =============================================
    ' جلب آخر راتب
    ' =============================================
    Private Function GetLastSalary(ByVal EmployeeID As Integer) As String
        Try
            ' Dim clsSalary As New Clshrs_EmployeeSalaries(Page)
            ' If clsSalary.Find("EmployeeID=" & EmployeeID & " AND IsActive=1", "Date DESC") Then
            '     Return clsSalary.NetSalary.ToString("N2")
            ' End If
        Catch ex As Exception
        End Try
        Return "0.00"
    End Function

    ' =============================================
    ' الحفظ الرئيسي داخل Transaction
    ' =============================================
    Private Function SaveChanges() As Boolean
        Dim conn As SqlConnection = Nothing
        Dim trans As SqlTransaction = Nothing

        Try
            If Not ClsEmployees.Find("Code='" & txtEmployeeCode.Text.Trim() & "'") Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                    ObjNavigationHandler.SetLanguage(Page,
                        " Employee Not Found / الموظف غير موجود"))
                Return False
            End If

            Dim employeeID As Integer = ClsEmployees.ID
            Dim oldJoinDate As DateTime = Convert.ToDateTime(ClsEmployees.JoinDate)
            Dim newJoinDate As DateTime = Convert.ToDateTime(txtNewJoinDate.Value)
            Dim newExpireDate As DateTime = newJoinDate.AddYears(1)

            Dim lastSalary As Decimal = 0
            Dim totalBalance As Decimal = 0
            Dim annualBalance As Decimal = 0
            Dim transferBalance As Decimal = 0

            Decimal.TryParse(txtCurrentBalance.Text.Replace(",", ""), totalBalance)
            Decimal.TryParse(txtAnnualVacation.Text.Replace(",", ""), annualBalance)
            Decimal.TryParse(txtTransferredVacation.Text.Replace(",", ""), transferBalance)
            Decimal.TryParse(txtLastSalary.Text.Replace(",", ""), lastSalary)

            Dim annualExpire As Object = DBNull.Value
            Dim transferExpire As Object = DBNull.Value
            If txtAnnualExpireDate.Text <> "" Then
                annualExpire = Convert.ToDateTime(txtAnnualExpireDate.Text)
            End If
            If txtTransferredExpireDate.Text <> "" Then
                transferExpire = Convert.ToDateTime(txtTransferredExpireDate.Text)
            End If

            ' 1 = إبقاء الرصيد ، 2 = تصفير الرصيد
            Dim vacationAction As Integer = If(rdoZeroVacation.Checked, 2, 1)

            Dim newCategoryID As Object = DBNull.Value
            If ddlNewCategory.SelectedIndex > 0 Then
                newCategoryID = Convert.ToInt32(ddlNewCategory.SelectedValue)
            End If

            conn = New SqlConnection(ClsEmployees.ConnectionString)
            conn.Open()
            trans = conn.BeginTransaction()

            ' الخطوة 1 - تحديث تاريخ المباشرة في hrs_Employees
            UpdateEmployeeJoinDate(conn, trans, employeeID, newJoinDate, newCategoryID)

            ' الخطوة 2 - تحديث رصيد الاجازات في hrs_VacationsBalance
            UpdateVacationBalance(conn, trans, employeeID, newJoinDate, newExpireDate, vacationAction)

            ' الخطوة 3 - حفظ سجل التغيير في hrs_ChangeJoinDate
            InsertChangeLog(conn, trans,
                            employeeID, txtEmployeeCode.Text.Trim(),
                            oldJoinDate, newJoinDate,
                            DBNull.Value, newCategoryID,
                            annualBalance, annualExpire,
                            transferBalance, transferExpire,
                            totalBalance,
                            vacationAction, txtReasonVacation.Text.Trim(),
                            txtReasonJoinDate.Text.Trim(),
                            lastSalary)

            trans.Commit()
            Return True

        Catch ex As Exception
            If trans IsNot Nothing Then
                Try
                    trans.Rollback()
                Catch
                End Try
            End If
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                ObjNavigationHandler.SetLanguage(Page,
                    " Error Occurred: " & ex.Message & " / حدث خطأ: " & ex.Message))
            Return False

        Finally
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Function

    ' =============================================
    ' 1. تحديث تاريخ المباشرة في hrs_Employees
    ' =============================================
    Private Sub UpdateEmployeeJoinDate(ByVal conn As SqlConnection,
                                       ByVal trans As SqlTransaction,
                                       ByVal employeeID As Integer,
                                       ByVal newJoinDate As DateTime,
                                       ByVal newCategoryID As Object)

        Dim sql As String = "UPDATE hrs_Employees SET JoinDate = @NewJoinDate " &
                            IIf(Not IsDBNull(newCategoryID), ", CategoryID = @NewCategoryID ", "") &
                            "WHERE ID = @EmployeeID"

        Using cmd As New SqlCommand(sql, conn, trans)
            cmd.Parameters.AddWithValue("@NewJoinDate", newJoinDate)
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID)
            If Not IsDBNull(newCategoryID) Then
                cmd.Parameters.AddWithValue("@NewCategoryID", newCategoryID)
            End If
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' =============================================
    ' 2. تحديث رصيد الاجازات في hrs_VacationsBalance
    ' =============================================
    Private Sub UpdateVacationBalance(ByVal conn As SqlConnection,
                                      ByVal trans As SqlTransaction,
                                      ByVal employeeID As Integer,
                                      ByVal newJoinDate As DateTime,
                                      ByVal newExpireDate As DateTime,
                                      ByVal vacationAction As Integer)

        Dim sql As String
        If vacationAction = 2 Then
            sql = "UPDATE hrs_VacationsBalance SET " &
                  "    Remaining = 0, ExpireDate = @NewExpireDate, LastUpdateDate = GETDATE() " &
                  "WHERE EmployeeID = @EmployeeID " &
                  "  AND ExpireDate > @NewJoinDate AND CancelDate IS NULL"
        Else
            sql = "UPDATE hrs_VacationsBalance SET " &
                  "    ExpireDate = @NewExpireDate, LastUpdateDate = GETDATE() " &
                  "WHERE EmployeeID = @EmployeeID " &
                  "  AND ExpireDate > @NewJoinDate AND CancelDate IS NULL"
        End If

        Using cmd As New SqlCommand(sql, conn, trans)
            cmd.Parameters.AddWithValue("@NewExpireDate", newExpireDate)
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID)
            cmd.Parameters.AddWithValue("@NewJoinDate", newJoinDate)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' =============================================
    ' 3. إدراج سجل في hrs_ChangeJoinDate
    ' =============================================
    Private Sub InsertChangeLog(ByVal conn As SqlConnection,
                                ByVal trans As SqlTransaction,
                                ByVal employeeID As Integer,
                                ByVal employeeCode As String,
                                ByVal oldJoinDate As DateTime,
                                ByVal newJoinDate As DateTime,
                                ByVal oldCategoryID As Object,
                                ByVal newCategoryID As Object,
                                ByVal annualBalance As Decimal,
                                ByVal annualExpire As Object,
                                ByVal transferBalance As Decimal,
                                ByVal transferExpire As Object,
                                ByVal totalBalance As Decimal,
                                ByVal vacationAction As Integer,
                                ByVal vacationReason As String,
                                ByVal changeReason As String,
                                ByVal lastSalary As Decimal)

        Dim sql As String =
            "INSERT INTO hrs_ChangeJoinDate (" &
            "    EmployeeID, EmployeeCode, OldJoinDate, NewJoinDate," &
            "    OldCategoryID, NewCategoryID," &
            "    AnnualVacationBalance, AnnualVacationExpireDate," &
            "    TransferredVacationBalance, TransferredVacationExpireDate," &
            "    TotalVacationBalance, VacationAction, VacationActionReason," &
            "    ChangeReason, LastSalary, RegDate, RegUserID" &
            ") VALUES (" &
            "    @EmployeeID, @EmployeeCode, @OldJoinDate, @NewJoinDate," &
            "    @OldCategoryID, @NewCategoryID," &
            "    @AnnualVacationBalance, @AnnualVacationExpireDate," &
            "    @TransferredVacationBalance, @TransferredVacationExpireDate," &
            "    @TotalVacationBalance, @VacationAction, @VacationActionReason," &
            "    @ChangeReason, @LastSalary, GETDATE(), @RegUserID" &
            ")"

        Using cmd As New SqlCommand(sql, conn, trans)
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID)
            cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode)
            cmd.Parameters.AddWithValue("@OldJoinDate", oldJoinDate)
            cmd.Parameters.AddWithValue("@NewJoinDate", newJoinDate)
            cmd.Parameters.AddWithValue("@OldCategoryID", If(IsDBNull(oldCategoryID), DBNull.Value, oldCategoryID))
            cmd.Parameters.AddWithValue("@NewCategoryID", If(IsDBNull(newCategoryID), DBNull.Value, newCategoryID))
            cmd.Parameters.AddWithValue("@AnnualVacationBalance", annualBalance)
            cmd.Parameters.AddWithValue("@AnnualVacationExpireDate", If(IsDBNull(annualExpire), DBNull.Value, annualExpire))
            cmd.Parameters.AddWithValue("@TransferredVacationBalance", transferBalance)
            cmd.Parameters.AddWithValue("@TransferredVacationExpireDate", If(IsDBNull(transferExpire), DBNull.Value, transferExpire))
            cmd.Parameters.AddWithValue("@TotalVacationBalance", totalBalance)
            cmd.Parameters.AddWithValue("@VacationAction", vacationAction)
            cmd.Parameters.AddWithValue("@VacationActionReason", If(vacationReason = "", CObj(DBNull.Value), CObj(vacationReason)))
            cmd.Parameters.AddWithValue("@ChangeReason", changeReason)
            cmd.Parameters.AddWithValue("@LastSalary", lastSalary)
            cmd.Parameters.AddWithValue("@RegUserID", ClsEmployees.RegUserID)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' =============================================
    ' مسح الفورم
    ' =============================================
    Private Sub ClearForm()
        txtEmployeeCode.Text = ""
        txtEmployeeName.Text = ""
        txtCurrentJoinDate.Text = ""
        txtLastSalary.Text = ""
        txtCurrentCategory.Text = ""
        txtAnnualVacation.Text = ""
        txtAnnualExpireDate.Text = ""
        txtTransferredVacation.Text = ""
        txtTransferredExpireDate.Text = ""
        txtCurrentBalance.Text = ""
        txtNewJoinDate.Value = ""
        txtReasonJoinDate.Text = ""
        txtReasonVacation.Text = ""
        rdoZeroVacation.Checked = False
        rdoKeepVacation.Checked = False
        ddlNewCategory.SelectedIndex = 0
        lblMessage.Text = ""
        lblErrorMessage.Text = ""
    End Sub

    ' =============================================
    ' التنقل بين الموظفين
    ' =============================================
    Private Sub NavigateEmployee(ByVal direction As String)
        Try
            Select Case direction
                Case "First"
                    ClsEmployees.FirstRecord()
                Case "Previous"
                    ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'")
                    If Not ClsEmployees.previousRecord() Then
                        ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'")
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                            ObjNavigationHandler.SetLanguage(Page,
                                " This is the first record /هذه أول صفحة"))
                    End If
                Case "Next"
                    ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'")
                    If Not ClsEmployees.NextRecord() Then
                        ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'")
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                            ObjNavigationHandler.SetLanguage(Page,
                                " This is the last record /هذه أخر صفحة"))
                    End If
                Case "Last"
                    ClsEmployees.LastRecord()
            End Select
            txtEmployeeCode.Text = ClsEmployees.Code
            LoadEmployeeData()
        Catch ex As Exception
        End Try
    End Sub

    ' =============================================
    ' خصائص الشاشة
    ' =============================================
    Private Sub SetScreenInformation()
        Try
            Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, ClsEmployees.ConnectionString, "UltraWebTab1")
            Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, ClsEmployees.ConnectionString, "UltraWebTab1")
            Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, ClsEmployees.ConnectionString, "UltraWebTab1")
            Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Page, ClsEmployees.ConnectionString, "UltraWebTab1")
            Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, ClsEmployees.ConnectionString, DIV)
            Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, ClsEmployees.ConnectionString, "UltraWebTab1")
        Catch ex As Exception
        End Try
    End Sub

#End Region

End Class