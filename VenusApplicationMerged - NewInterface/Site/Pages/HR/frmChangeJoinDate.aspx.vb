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
    Private ClsEmployeeClass As Clshrs_EmployeeClasses
    Private ClsVacationsTypes As Clshrs_VacationsTypes
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ClsEmployees = New Clshrs_Employees(Page)
            ClsContract = New Clshrs_Contracts(Page)
            ClsEmployeeClass = New Clshrs_EmployeeClasses(Page)
            ClsVacationsTypes = New Clshrs_VacationsTypes(Page)
            ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

            Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)

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
            If Session IsNot Nothing Then Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0,
                Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Private Sub SetupSearchButton()
        Try
            Dim csSearchID As Integer = 0
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

            If csSearchID = 0 Then
                If ClsSearchs.Find("ObjectID IN (SELECT ID FROM sys_Objects WHERE Code='" & tableCode & "')") Then
                    csSearchID = ClsSearchs.ID
                End If
            End If

            Dim intDimension As Integer = 510
            Dim urlString As String = "'frmModalSearchScreen.aspx" &
                                      "?TargetControl=" & txtEmployeeCode.ID &
                                      "&SearchID=" & csSearchID &
                                      "&'," & intDimension & ",720,false,'" &
                                      txtEmployeeCode.ClientID & "'"

            btnSearchEmployee.ClientSideEvents.Click = "OpenModal1(" & urlString & ")"
            btnSearchEmployee.Attributes.Add("onclick", "OpenModal1(" & urlString & "); return false;")

        Catch ex As Exception
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
                            " Done Successfully / تم بنجاح"))
                    ClearForm()
                End If

            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)

            Case "Property"
                If ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page,
                        "frmPropertyScreen.aspx?ID=" & ClsEmployees.ID & "&TableName=" & ClsEmployees.Table,
                        477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                End If

            Case "Remarks"
                If ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page,
                        "frmRemarks.aspx?ID=" & ClsEmployees.ID & "&TableName=" & ClsEmployees.Table,
                        410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                End If

            Case "First", "Previous", "Next", "Last"
                NavigateEmployee(e.CommandArgument.ToString())

        End Select
    End Sub

    Protected Sub txtEmployeeCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmployeeCode.TextChanged
        LoadEmployeeData()
    End Sub

    Protected Sub ddlNewClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlNewClass.SelectedIndexChanged
        ' JavaScript يتعامل مع إظهار/إخفاء القسم
    End Sub

#End Region

#Region "Private Functions"

    ' =============================================
    ' التحقق من البيانات قبل الحفظ
    ' =============================================
    Private Function ValidateBeforeSave() As Boolean

        ' كود الموظف إجباري
        If txtEmployeeCode.Text.Trim() = "" Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                ObjNavigationHandler.SetLanguage(Page,
                    " Please Enter Employee Code /برجاء إدخال كود الموظف"))
            Return False
        End If

        ' لازم يكون فيه تغيير في تاريخ المباشرة أو الفئة
        Dim newClassSelected As Boolean = (ddlNewClass.SelectedIndex > 0)
        Dim currentClassIDText As String = hdnCurrentClassID.Value
        Dim classChanged As Boolean = newClassSelected AndAlso
            (currentClassIDText = "" OrElse ddlNewClass.SelectedValue <> currentClassIDText)

        Dim joinDateChanged As Boolean = False
        If Not String.IsNullOrEmpty(txtNewJoinDate.Text) Then
            Try
                Dim newJoin As DateTime = Convert.ToDateTime(txtNewJoinDate.Value)
                Dim oldJoin As DateTime = Convert.ToDateTime(txtCurrentJoinDate.Text)
                joinDateChanged = (newJoin <> oldJoin)
            Catch
            End Try
        End If

        If Not classChanged AndAlso Not joinDateChanged Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                ObjNavigationHandler.SetLanguage(Page,
                    " Please change Join Date or Class / برجاء تغيير تاريخ المباشرة أو الفئة"))
            Return False
        End If

        ' سبب التغيير إجباري
        If txtReasonJoinDate.Text.Trim() = "" Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                ObjNavigationHandler.SetLanguage(Page,
                    " Please Enter Reason /برجاء إدخال سبب التغيير"))
            Return False
        End If

        ' لو الفئة اتغيرت واختار ترحيل الرصيد، تاريخ الانتهاء إجباري
        If classChanged Then
            If chkTransferBalance.Checked Then
                If String.IsNullOrEmpty(txtTransferExpireDate.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                        ObjNavigationHandler.SetLanguage(Page,
                            " Please enter the transfer expire date /برجاء إدخال تاريخ انتهاء الرصيد المرحل"))
                    Return False
                End If
            End If
        End If

        Return True
    End Function

    ' =============================================
    ' رسائل اللغة
    ' =============================================
    Private Sub SetLanguageMessages()
        lblEmployeeCodeMsg.Text = ObjNavigationHandler.SetLanguage(Page, "Employee Code is required / كود الموظف مطلوب")
        lblNewJoinDateMsg.Text = ObjNavigationHandler.SetLanguage(Page, "New Join Date is required / تاريخ المباشرة الجديد مطلوب")
        lblReasonJoinDateMsg.Text = ObjNavigationHandler.SetLanguage(Page, "Reason is required / سبب التغيير مطلوب")
        lblValidationTitle.Text = ObjNavigationHandler.SetLanguage(Page, "Please complete the following data / الرجاء إكمال البيانات التالية")
        lblConfirmMsg.Text = ObjNavigationHandler.SetLanguage(Page, "Are you sure? / هل أنت متأكد؟")
        lblTransferExpireDateMsg.Text = ObjNavigationHandler.SetLanguage(Page, "Please enter the transfer expire date / برجاء إدخال تاريخ انتهاء الرصيد المرحل")
    End Sub

    ' =============================================
    ' تحميل الفئات
    ' =============================================
    Private Sub LoadClasses()
        Try
            Dim clsClasses As New Clshrs_EmployeeClasses(Page)
            clsClasses.GetDropDownList(ddlNewClass, True)
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
                    hdnEmployeeID.Value = ClsEmployees.ID.ToString()
                    txtEmployeeName.Text = ClsEmployees.EnglishName
                    txtCurrentJoinDate.Text = ClsEmployees.JoinDate
                    txtLastSalary.Text = GetLastSalary(ClsEmployees.ID)

                    LoadCurrentClass(ClsEmployees.ID)
                    LoadVacationBalances(ClsEmployees.ID)
                    CalculateDueBalance(ClsEmployees.ID)

                    ' إعادة تعيين قسم تغيير الفئة
                    chkTransferBalance.Checked = False
                    txtTransferExpireDate.Value = ""
                Else
                    ClearForm()
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                        ObjNavigationHandler.SetLanguage(Page, " Employee Not Found / الموظف غير موجود"))
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ' =============================================
    ' تحميل الفئة الحالية من العقد
    ' =============================================
    Private Sub LoadCurrentClass(ByVal EmployeeID As Integer)
        Try
            hdnCurrentClassID.Value = ""
            Dim validContractID As Integer = ClsContract.ContractValidatoinId(EmployeeID, DateTime.Now)

            If validContractID > 0 Then
                Dim dt As DataTable = GetContractDataRead(validContractID)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim employeeClassID As Integer = Convert.ToInt32(dt.Rows(0)("EmployeeClassID"))
                    hdnCurrentClassID.Value = employeeClassID.ToString()
                    If ClsEmployeeClass.Find("ID=" & employeeClassID) Then
                        txtCurrentClass.Text = ClsEmployeeClass.EngName
                    Else
                        txtCurrentClass.Text = "غير محدد"
                    End If
                Else
                    txtCurrentClass.Text = "لا يوجد عقد صحيح"
                End If
            Else
                txtCurrentClass.Text = "لا يوجد عقد صحيح"
            End If
        Catch ex As Exception
            txtCurrentClass.Text = "خطأ في جلب الفئة"
        End Try
    End Sub

    ' =============================================
    ' جلب بيانات العقد (للقراءة - بره الـ Transaction)
    ' =============================================
    Private Function GetContractDataRead(ByVal ContractID As Integer) As DataTable
        Try
            Dim sql As String = "SELECT EmployeeClassID, EmployeeID, StartDate, EndDate " &
                                "FROM hrs_Contracts WHERE ID = " & ContractID
            Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(
                ClsEmployees.ConnectionString, CommandType.Text, sql).Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' =============================================
    ' تحميل أرصدة الإجازات
    ' =============================================
    Private Sub LoadVacationBalances(ByVal EmployeeID As Integer)
        Try
            Dim dt As DataTable = GetVacationBalances(EmployeeID)

            Dim annualBalance As Decimal = 0
            Dim transferredBalance As Decimal = 0
            Dim annualExpireDate As String = ""
            Dim transferredExpDate As String = ""

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    Dim balTypeID As Integer = Convert.ToInt32(row("BalanceTypeID"))
                    Dim bal As Decimal = Convert.ToDecimal(row("Remaining"))
                    Dim expDate As DateTime = Convert.ToDateTime(row("ExpireDate"))

                    If balTypeID = 1 Then
                        annualBalance = bal
                        annualExpireDate = expDate.ToString("dd/MM/yyyy")
                    ElseIf balTypeID = 2 Then
                        transferredBalance = bal
                        transferredExpDate = expDate.ToString("dd/MM/yyyy")
                    End If
                Next
            End If

            txtAnnualVacation.Text = annualBalance.ToString("N2")
            txtAnnualExpireDate.Text = annualExpireDate
            txtTransferredVacation.Text = transferredBalance.ToString("N2")
            txtTransferredExpireDate.Text = transferredExpDate
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
    ' جلب أرصدة الإجازات - أحدث سجل لكل نوع
    ' =============================================
    Private Function GetVacationBalances(ByVal EmployeeID As Integer) As DataTable
        Try
            Dim sql As String =
                "SELECT BalanceTypeID, Remaining, ExpireDate " &
                "FROM hrs_VacationsBalance vb " &
                "WHERE vb.EmployeeID = " & EmployeeID &
                "  AND vb.ExpireDate > GETDATE() AND vb.CancelDate IS NULL " &
                "  AND vb.ID = (" &
                "      SELECT TOP 1 vb2.ID FROM hrs_VacationsBalance vb2 " &
                "      WHERE vb2.EmployeeID = vb.EmployeeID " &
                "        AND vb2.BalanceTypeID = vb.BalanceTypeID " &
                "        AND vb2.ExpireDate > GETDATE() AND vb2.CancelDate IS NULL " &
                "      ORDER BY vb2.ID DESC)"

            Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(
                ClsEmployees.ConnectionString, CommandType.Text, sql).Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' =============================================
    ' حساب الرصيد المستحق حتى اليوم
    ' =============================================
    Private Sub CalculateDueBalance(ByVal EmployeeID As Integer)
        Try
            Dim myTotalBalance As Decimal = 0
            Dim validContractID As Integer = ClsContract.ContractValidatoinId(EmployeeID, DateTime.Now)

            If validContractID > 0 Then
                Dim dtContract As DataTable = GetContractDataRead(validContractID)
                If dtContract IsNot Nothing AndAlso dtContract.Rows.Count > 0 Then
                    Dim employeeClassID As Integer = Convert.ToInt32(dtContract.Rows(0)("EmployeeClassID"))
                    If ClsEmployeeClass.Find("ID=" & employeeClassID) Then
                        If ClsEmployeeClass.AdvanceBalance Then
                            Dim strSQL As String =
                                "SELECT Balance, Consumed, Remaining, BalanceTypeID, ExpireDate, DueDate " &
                                "FROM hrs_VacationsBalance " &
                                "WHERE EndServiceDate IS NULL AND EmployeeID = " & EmployeeID &
                                "  AND ExpireDate >= '" & DateTime.Now.ToString("yyyy-MM-dd") & "'" &
                                "  AND DueDate <= '" & DateTime.Now.ToString("yyyy-MM-dd") & "'" &
                                "  AND (CancelDate IS NULL OR CancelDate > '" & DateTime.Now.ToString("yyyy-MM-dd") & "')" &
                                "  AND ISNULL(Posted,0) = 0"

                            Dim da As New SqlDataAdapter(strSQL, ClsEmployees.ConnectionString)
                            Dim ds As New DataSet()
                            da.Fill(ds)

                            If ds.Tables(0).Rows.Count > 0 Then
                                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                                    Dim dueDate As DateTime = CDate(ds.Tables(0).Rows(i)("DueDate"))
                                    Dim expireDate As DateTime = CDate(ds.Tables(0).Rows(i)("ExpireDate"))
                                    Dim balTypeID As Integer = Convert.ToInt32(ds.Tables(0).Rows(i)("BalanceTypeID"))
                                    Dim allBalance As Decimal = Convert.ToDecimal(ds.Tables(0).Rows(i)("Balance"))
                                    Dim consumed As Decimal = Convert.ToDecimal(ds.Tables(0).Rows(i)("Consumed"))
                                    Dim myBalance As Decimal = 0

                                    If balTypeID = 2 Then
                                        myBalance = Convert.ToDecimal(ds.Tables(0).Rows(i)("Remaining"))
                                    ElseIf balTypeID = 1 Then
                                        Dim allDays As Decimal = DateDiff(DateInterval.Day, dueDate, expireDate)
                                        If allDays = 0 Then allDays = 360
                                        Dim myDays As Integer = DateDiff(DateInterval.Day, dueDate, DateTime.Now)
                                        If myDays < 0 Then myDays = 0
                                        Dim dayValue As Decimal = allBalance / allDays
                                        myBalance = Math.Round((myDays * dayValue) - consumed, 2)
                                    Else
                                        myBalance = Convert.ToDecimal(ds.Tables(0).Rows(i)("Remaining"))
                                    End If

                                    myTotalBalance += myBalance
                                Next
                            End If
                        End If
                    End If
                End If
            End If

            hdnDueBalance.Value = myTotalBalance.ToString()
            lblDueBalance.Text = myTotalBalance.ToString("N2")

        Catch ex As Exception
            lblDueBalance.Text = "0.00"
            hdnDueBalance.Value = "0"
        End Try
    End Sub

    ' =============================================
    ' آخر راتب
    ' =============================================
    Private Function GetLastSalary(ByVal EmployeeID As Integer) As String
        Return "0.00"
    End Function

    ' =============================================
    ' الحفظ الرئيسي - داخل Transaction
    ' =============================================
    Private Function SaveChanges() As Boolean
        Dim conn As SqlConnection = Nothing
        Dim trans As SqlTransaction = Nothing

        Try
            ' ==== جلب كل البيانات قبل فتح الـ Transaction ====
            If Not ClsEmployees.Find("Code='" & txtEmployeeCode.Text.Trim() & "'") Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                    ObjNavigationHandler.SetLanguage(Page, " Employee Not Found / الموظف غير موجود"))
                Return False
            End If

            Dim employeeID As Integer = ClsEmployees.ID
            Dim oldJoinDate As DateTime = Convert.ToDateTime(ClsEmployees.JoinDate)
            Dim newJoinDate As DateTime = Convert.ToDateTime(txtNewJoinDate.Value)
            Dim connStr As String = ClsEmployees.ConnectionString
            Dim regUserID As Integer = ClsEmployees.RegUserID

            ' الرصيد المستحق حتى اليوم (محسوب مسبقاً)
            Dim dueBalance As Decimal = 0
            Decimal.TryParse(hdnDueBalance.Value, dueBalance)

            ' الفئة الجديدة
            Dim newClassID As Object = DBNull.Value
            If ddlNewClass.SelectedIndex > 0 Then
                newClassID = Convert.ToInt32(ddlNewClass.SelectedValue)
            End If

            ' هل تغير تاريخ المباشرة؟
            Dim joinDateChanged As Boolean = (newJoinDate <> oldJoinDate)

            ' هل تغيرت الفئة؟
            Dim currentClassIDText As String = hdnCurrentClassID.Value
            Dim classChanged As Boolean = Not IsDBNull(newClassID) AndAlso
                (currentClassIDText = "" OrElse Convert.ToString(newClassID) <> currentClassIDText)

            ' ==== فتح Connection و Transaction ====
            conn = New SqlConnection(connStr)
            conn.Open()
            trans = conn.BeginTransaction()

            Try
                ' ========== 1. جلب الفئة القديمة والـ ContractID ======
                Dim validContractID As Integer = ClsContract.ContractValidatoinId(employeeID, DateTime.Now)
                Dim oldClassID As Object = DBNull.Value

                If validContractID > 0 Then
                    Dim dtOld As DataTable = GetContractDataInTrans(validContractID, conn, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        oldClassID = Convert.ToInt32(dtOld.Rows(0)("EmployeeClassID"))
                    End If
                End If

                ' ========== 2. تحديث تاريخ المباشرة ==========
                If joinDateChanged Then
                    UpdateEmployeeJoinDate(conn, trans, employeeID, newJoinDate)
                End If

                ' ========== 3. تحديث الفئة في العقد ==========
                If classChanged AndAlso Not IsDBNull(newClassID) AndAlso validContractID > 0 Then
                    UpdateContractClass(conn, trans, validContractID, newClassID)
                End If

                ' ========== 4. معالجة الرصيد عند تغيير الفئة ==========
                If classChanged Then
                    If chkTransferBalance.Checked AndAlso dueBalance > 0 Then
                        ' ترحيل الرصيد المستحق
                        Dim newExpireDate As DateTime = Convert.ToDateTime(txtTransferExpireDate.Value)
                        UpdateAnnualBalanceExpireDate(conn, trans, employeeID,
                                                      newExpireDate, newJoinDate,
                                                      dueBalance,
                                                      If(IsDBNull(oldClassID), 0, Convert.ToInt32(oldClassID)),
                                                      If(IsDBNull(newClassID), 0, Convert.ToInt32(newClassID)),
                                                      regUserID)
                    Else
                        ' تصفير الرصيد
                        ZeroAnnualBalance(conn, trans, employeeID)
                    End If

                    ' ========== 5. إضافة رصيد جديد للفئة الجديدة ==========
                    If Not IsDBNull(newClassID) Then
                        InsertNewClassBalance(conn, trans, employeeID, newClassID, newJoinDate, regUserID)
                    End If
                End If

                ' ========== 6. حفظ سجل التغيير ==========
                InsertChangeLog(conn, trans,
                                employeeID,
                                oldJoinDate, newJoinDate,
                                oldClassID, newClassID,
                                dueBalance,
                                chkTransferBalance.Checked,
                                txtReasonJoinDate.Text.Trim(),
                                regUserID)

                trans.Commit()
                Return True

            Catch ex As Exception
                Try : trans.Rollback() : Catch : End Try
                Throw ex
            End Try

        Catch ex As Exception
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                ObjNavigationHandler.SetLanguage(Page,
                    " Error: " & ex.Message & " / خطأ: " & ex.Message))
            Return False

        Finally
            If conn IsNot Nothing Then
                If conn.State = ConnectionState.Open Then conn.Close()
                conn.Dispose()
            End If
        End Try
    End Function

    ' =============================================
    ' جلب بيانات العقد (جوه الـ Transaction)
    ' =============================================
    Private Function GetContractDataInTrans(ByVal ContractID As Integer,
                                            ByVal conn As SqlConnection,
                                            ByVal trans As SqlTransaction) As DataTable
        Try
            Dim sql As String = "SELECT EmployeeClassID, EmployeeID, StartDate, EndDate " &
                                "FROM hrs_Contracts WHERE ID = @ContractID"
            Using cmd As New SqlCommand(sql, conn, trans)
                cmd.Parameters.AddWithValue("@ContractID", ContractID)
                Using da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    Return dt
                End Using
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' =============================================
    ' 2. تحديث تاريخ المباشرة (مع إيقاف الـ Trigger)
    ' =============================================
    Private Sub UpdateEmployeeJoinDate(ByVal conn As SqlConnection,
                                       ByVal trans As SqlTransaction,
                                       ByVal employeeID As Integer,
                                       ByVal newJoinDate As DateTime)
        Dim triggerName As String = ""
        Try
            Using cmd As New SqlCommand(
                "SELECT TOP 1 name FROM sys.triggers WHERE parent_id = OBJECT_ID('hrs_Employees')",
                conn, trans)
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing Then triggerName = result.ToString()
            End Using
        Catch
        End Try

        If triggerName <> "" Then
            Using cmd As New SqlCommand("DISABLE TRIGGER [" & triggerName & "] ON hrs_Employees", conn, trans)
                cmd.ExecuteNonQuery()
            End Using
        End If

        Try
            Using cmd As New SqlCommand(
                "UPDATE hrs_Employees SET JoinDate = @NewJoinDate WHERE ID = @EmployeeID",
                conn, trans)
                cmd.CommandTimeout = 120
                cmd.Parameters.AddWithValue("@NewJoinDate", newJoinDate)
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID)
                cmd.ExecuteNonQuery()
            End Using
        Finally
            If triggerName <> "" Then
                Using cmd As New SqlCommand("ENABLE TRIGGER [" & triggerName & "] ON hrs_Employees", conn, trans)
                    cmd.ExecuteNonQuery()
                End Using
            End If
        End Try
    End Sub

    ' =============================================
    ' 3. تحديث الفئة في العقد
    ' =============================================
    Private Sub UpdateContractClass(ByVal conn As SqlConnection,
                                    ByVal trans As SqlTransaction,
                                    ByVal contractID As Integer,
                                    ByVal newClassID As Object)
        Using cmd As New SqlCommand(
            "UPDATE hrs_Contracts SET EmployeeClassID = @NewClassID WHERE ID = @ContractID",
            conn, trans)
            cmd.Parameters.AddWithValue("@NewClassID", newClassID)
            cmd.Parameters.AddWithValue("@ContractID", contractID)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' =============================================
    ' 4.أ إلغاء الرصيد السنوي الحالي وإضافة رصيد مرحل
    ' =============================================
    Private Sub UpdateAnnualBalanceExpireDate(ByVal conn As SqlConnection,
                                              ByVal trans As SqlTransaction,
                                              ByVal employeeID As Integer,
                                              ByVal newExpireDate As DateTime,
                                              ByVal newDueDate As DateTime,
                                              ByVal currentBalance As Decimal,
                                              ByVal oldClassID As Integer,
                                              ByVal newClassID As Integer,
                                              ByVal regUserID As Integer)
        ' إلغاء الرصيد السنوي الحالي
        Dim cancelSql As String =
            "UPDATE hrs_VacationsBalance SET CancelDate = GETDATE(), LastUpdateDate = GETDATE() " &
            "WHERE ID = (SELECT TOP 1 ID FROM hrs_VacationsBalance " &
            "            WHERE EmployeeID = @EmployeeID AND BalanceTypeID = 1 " &
            "              AND ExpireDate > GETDATE() AND CancelDate IS NULL " &
            "            ORDER BY ID DESC)"

        Using cmd As New SqlCommand(cancelSql, conn, trans)
            cmd.CommandTimeout = 120
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID)
            cmd.ExecuteNonQuery()
        End Using

        ' إضافة سجل الرصيد المرحل
        If currentBalance > 0 Then
            Dim oldClassName As String = GetClassNameFromDB(conn, trans, oldClassID)
            Dim newClassName As String = GetClassNameFromDB(conn, trans, newClassID)
            Dim remarks As String = String.Format(
                "تم ترحيل الرصيد من حركة تغيير الفئة - الفئة القديمة: {0} (ID: {1}) -> الفئة الجديدة: {2} (ID: {3}) - تاريخ المباشرة الجديد: {4}",
                oldClassName, oldClassID, newClassName, newClassID, newDueDate.ToString("dd/MM/yyyy"))

            Dim insertSql As String =
                "INSERT INTO hrs_VacationsBalance " &
                "(EmployeeID, [Year], Balance, Consumed, Remaining, BalanceTypeID, " &
                " ExpireDate, DueDate, Src, Remarks, Reguser, RegDate) " &
                "VALUES (@EmployeeID, @Year, @Balance, 0, @Remaining, 2, " &
                "        @ExpireDate, @DueDate, @Src, @Remarks, @RegUserID, GETDATE())"

            Using cmd As New SqlCommand(insertSql, conn, trans)
                cmd.CommandTimeout = 120
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID)
                cmd.Parameters.AddWithValue("@Year", newDueDate.Year)
                cmd.Parameters.AddWithValue("@Balance", currentBalance)
                cmd.Parameters.AddWithValue("@Remaining", currentBalance)
                cmd.Parameters.AddWithValue("@ExpireDate", newExpireDate)
                cmd.Parameters.AddWithValue("@DueDate", newDueDate)
                cmd.Parameters.AddWithValue("@Src", "frmChangeJoinDate")
                cmd.Parameters.AddWithValue("@Remarks", remarks)
                cmd.Parameters.AddWithValue("@RegUserID", regUserID)
                cmd.ExecuteNonQuery()
            End Using
        End If
    End Sub

    ' =============================================
    ' جلب اسم الفئة من الداتابيز
    ' =============================================
    Private Function GetClassNameFromDB(ByVal conn As SqlConnection,
                                        ByVal trans As SqlTransaction,
                                        ByVal classID As Integer) As String
        Try
            Using cmd As New SqlCommand(
                "SELECT EnglishName FROM hrs_EmployeeClasses WHERE ID = @ClassID",
                conn, trans)
                cmd.Parameters.AddWithValue("@ClassID", classID)
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing Then Return result.ToString()
            End Using
        Catch
        End Try
        Return "غير محدد (ID: " & classID & ")"
    End Function

    ' =============================================
    ' 4.ب تصفير الرصيد السنوي (عدم الترحيل)
    ' =============================================
    Private Sub ZeroAnnualBalance(ByVal conn As SqlConnection,
                                   ByVal trans As SqlTransaction,
                                   ByVal employeeID As Integer)
        Using cmd As New SqlCommand(
            "UPDATE hrs_VacationsBalance SET Remaining = 0, LastUpdateDate = GETDATE() " &
            "WHERE ID = (SELECT TOP 1 ID FROM hrs_VacationsBalance " &
            "            WHERE EmployeeID = @EmployeeID AND BalanceTypeID = 1 " &
            "              AND ExpireDate > GETDATE() AND CancelDate IS NULL " &
            "            ORDER BY ID DESC)",
            conn, trans)
            cmd.CommandTimeout = 120
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' =============================================
    ' 5. إضافة رصيد جديد للفئة الجديدة
    ' =============================================
    Private Sub InsertNewClassBalance(ByVal conn As SqlConnection,
                                      ByVal trans As SqlTransaction,
                                      ByVal employeeID As Integer,
                                      ByVal newClassID As Object,
                                      ByVal startDate As DateTime,
                                      ByVal regUserID As Integer)
        Dim balanceDays As Decimal = GetClassVacationDays(newClassID, startDate)
        If balanceDays <= 0 Then Exit Sub

        Dim expireDate As DateTime = startDate.AddYears(1) ' TODO: تعديل القاعدة لاحقاً

        Using cmd As New SqlCommand(
            "INSERT INTO hrs_VacationsBalance " &
            "(EmployeeID, [Year], Balance, Consumed, Remaining, BalanceTypeID, " &
            " ExpireDate, DueDate, Src, Remarks, Reguser, RegDate) " &
            "VALUES (@EmployeeID, @Year, @Balance, 0, @Remaining, 1, " &
            "        @ExpireDate, @DueDate, @Src, '', @RegUserID, GETDATE())",
            conn, trans)
            cmd.CommandTimeout = 120
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID)
            cmd.Parameters.AddWithValue("@Year", startDate.Year)
            cmd.Parameters.AddWithValue("@Balance", balanceDays)
            cmd.Parameters.AddWithValue("@Remaining", balanceDays)
            cmd.Parameters.AddWithValue("@ExpireDate", expireDate)
            cmd.Parameters.AddWithValue("@DueDate", startDate)
            cmd.Parameters.AddWithValue("@Src", "frmChangeJoinDate")
            cmd.Parameters.AddWithValue("@RegUserID", regUserID)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' =============================================
    ' جلب عدد أيام الإجازة للفئة الجديدة
    ' =============================================
    Private Function GetClassVacationDays(ByVal classID As Object, ByVal joinDate As DateTime) As Decimal
        Try
            If IsDBNull(classID) Then Return 0

            Dim clsClass As New Clshrs_EmployeeClasses(Page)
            If Not clsClass.Find("ID=" & Convert.ToInt32(classID)) Then Return 0
            If Not (clsClass.AdvanceBalance AndAlso clsClass.AddBalanceInAddEmp) Then Return 0

            Dim ds As DataSet = clsClass.GetEmployeeClassAnnualVacations(clsClass.ID)

            Dim requiredWorkingMonths As Decimal = 1
            Dim durationDays As Decimal = 0
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                With ds.Tables(0).Rows(0)
                    requiredWorkingMonths = .Item("RequiredWorkingMonths")
                    durationDays = .Item("DurationDays")
                End With
            End If

            If requiredWorkingMonths = 0 Then requiredWorkingMonths = 1
            Dim dayValue As Decimal = durationDays / requiredWorkingMonths
            Dim myBalance As Decimal = 0

            If clsClass.AccumulatedBalance Then
                Dim myDays As Integer = DateDiff(DateInterval.Day, joinDate.Date,
                                                 joinDate.Date.AddDays(CDbl(requiredWorkingMonths)))
                myBalance = myDays * dayValue
            Else
                Dim endOfYear As DateTime = New DateTime(joinDate.Year, 12, 31)
                Dim myDays As Integer = DateDiff(DateInterval.Day, joinDate.Date, endOfYear.Date)
                If myDays < 0 Then myDays = 0
                myBalance = myDays * dayValue
            End If

            Return Math.Round(myBalance, 2)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ' =============================================
    ' 6. إدراج سجل التغيير في hrs_ChangeJoinDate
    ' =============================================
    Private Sub InsertChangeLog(ByVal conn As SqlConnection,
                                ByVal trans As SqlTransaction,
                                ByVal employeeID As Integer,
                                ByVal oldJoinDate As DateTime,
                                ByVal newJoinDate As DateTime,
                                ByVal oldClassID As Object,
                                ByVal newClassID As Object,
                                ByVal dueBalance As Decimal,
                                ByVal isTransferred As Boolean,
                                ByVal changeReason As String,
                                ByVal regUserID As Integer)

        Dim sql As String =
            "INSERT INTO hrs_ChangeJoinDate " &
            "(EmployeeID, OldJoinDate, NewJoinDate, OldClassID, NewClassID, " &
            " DueBalance, IsBalanceTransfered, ChangeReason, RegDate, RegUserID) " &
            "VALUES " &
            "(@EmployeeID, @OldJoinDate, @NewJoinDate, @OldClassID, @NewClassID, " &
            " @DueBalance, @IsBalanceTransfered, @ChangeReason, GETDATE(), @RegUserID)"

        Using cmd As New SqlCommand(sql, conn, trans)
            cmd.CommandTimeout = 120
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID)
            cmd.Parameters.AddWithValue("@OldJoinDate", oldJoinDate)
            cmd.Parameters.AddWithValue("@NewJoinDate", newJoinDate)
            cmd.Parameters.AddWithValue("@OldClassID", If(IsDBNull(oldClassID), DBNull.Value, oldClassID))
            cmd.Parameters.AddWithValue("@NewClassID", If(IsDBNull(newClassID), DBNull.Value, newClassID))
            cmd.Parameters.AddWithValue("@DueBalance", dueBalance)
            cmd.Parameters.AddWithValue("@IsBalanceTransfered", If(isTransferred, 1, 0))
            cmd.Parameters.AddWithValue("@ChangeReason", changeReason)
            cmd.Parameters.AddWithValue("@RegUserID", regUserID)
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
        txtCurrentClass.Text = ""
        txtAnnualVacation.Text = ""
        txtAnnualExpireDate.Text = ""
        txtTransferredVacation.Text = ""
        txtTransferredExpireDate.Text = ""
        txtCurrentBalance.Text = ""
        txtNewJoinDate.Value = ""
        txtReasonJoinDate.Text = ""
        ddlNewClass.SelectedIndex = 0
        hdnCurrentClassID.Value = ""
        hdnEmployeeID.Value = ""
        hdnDueBalance.Value = ""
        lblDueBalance.Text = "0.00"
        chkTransferBalance.Checked = False
        txtTransferExpireDate.Value = ""
        lblMessage.Text = ""
        lblErrorMessage.Text = ""
    End Sub

    ' =============================================
    ' التنقل بين الموظفين
    ' =============================================
    Private Sub NavigateEmployee(ByVal direction As String)
        Try
            Select Case direction
                Case "First" : ClsEmployees.FirstRecord()
                Case "Previous"
                    ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'")
                    If Not ClsEmployees.previousRecord() Then
                        ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'")
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                            ObjNavigationHandler.SetLanguage(Page, " This is the first record /هذه أول صفحة"))
                    End If
                Case "Next"
                    ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'")
                    If Not ClsEmployees.NextRecord() Then
                        ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'")
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                            ObjNavigationHandler.SetLanguage(Page, " This is the last record /هذه أخر صفحة"))
                    End If
                Case "Last" : ClsEmployees.LastRecord()
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