Imports System.Data
Imports Infragistics.WebUI.UltraWebNavigator
Imports Microsoft.ApplicationBlocks.Data
Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Application.SystemFiles.System

Partial Class frmAppraisalEmployeesKPIs
    Inherits MainPage
#Region "Public Decleration"
    Dim clsHrsEmployeeOfficialVacations As Clshrs_OfficialVacations
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private ClsFiscalYears As Clssys_FiscalYears
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)

        ClsFiscalYears = New Clssys_FiscalYears(Me.Page)
        Dim clsNavigation = New Venus.Shared.Web.NavigationHandler(ClsFiscalYears.ConnectionString)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)

            If ClsObjects.Find(" Code='App_AppraisalEmployeesKPIs_H'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID

                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

            Dim clsEmployees As New Clshrs_Employees(Page)

            ClsObjects.Find(" Code='" & clsEmployees.Table.Trim & "'")
            ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
            Dim csSearchID As Integer
            csSearchID = ClsSearchs.ID
            Dim IntDimensionEmp As Integer = 510
            Dim UrlStringEmp = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmployee.ID & "&SearchID=" & csSearchID & "&'," & IntDimensionEmp & ",720,false,'" & txtEmployee.ClientID & "'"

            btnEmployeeSearch.ClientSideEvents.Click = "OpenModal1(" & UrlStringEmp & ")"
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Dim clsAppraisalTypes = New Clshrs_AppraisalTypes(Me)
                clsAppraisalTypes.GetDropDownList(DdlAppraisalType, True)

                Page.Session.Add("ConnectionString", clsHrsEmployeeOfficialVacations.ConnectionString)
                clsHrsEmployeeOfficialVacations.AddOnChangeEventToControls("frmAppraisalEmployeesKPIs", Page, UltraWebTab1)

                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-GB")
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-GB")

                'ClsFiscalYears.GetDropDown(ddlFiscalYear)
                'If (ddlFiscalYear.SelectedItem.Text <> "") Then
                '    GetValues()

                AddNewRow()
                SetNextHeaderCode()
            End If
            Dim IntrecordID As Integer
            GetDropDownListGrid()

            CreateOtherFields(IntrecordID)
            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, clsHrsEmployeeOfficialVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsHrsEmployeeOfficialVacations.ConnectionString)
        Dim SqlCommand As String = String.Empty
        Select Case e.CommandArgument
            Case "SaveNew"
                Dim empCode As String = txtEmployee.Text.Trim()
                If String.IsNullOrEmpty(empCode) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Employee /برجاء إدخال الموظف"))
                    Exit Sub
                End If

                Dim appraisalTypeId As Integer
                Integer.TryParse(If(DdlAppraisalType.SelectedValue, "0"), appraisalTypeId)
                If appraisalTypeId <= 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please select Appraisal Type / برجاء اختيار نوع التقييم"))
                    Exit Sub
                End If

                Dim fDate As Date, tDate As Date
                If Not Date.TryParse(Convert.ToString(txtFromDate.Value), fDate) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please enter From Date / برجاء إدخال من تاريخ"))
                    Exit Sub
                End If
                If Not Date.TryParse(Convert.ToString(txtToDate.Value), tDate) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please enter To Date / برجاء إدخال إلى تاريخ"))
                    Exit Sub
                End If

                ' Use user-entered Total Weight from txtTotalWeight, not grid sum
                Dim totalWeight As Decimal
                If Not Decimal.TryParse(Convert.ToString(txtTotalWeight.Text), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, totalWeight) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please enter a valid Total Weight / برجاء إدخال إجمالي وزن صحيح"))
                    Exit Sub
                End If
                Dim TotalCriteriasWeight As Integer = 0
                For Each r As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows

                    TotalCriteriasWeight += r.Cells.FromKey("Weight").Value
                Next

                If TotalCriteriasWeight <> txtTotalWeight.Text Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please enter a valid Total Weight / برجاء إدخال إجمالي وزن صحيح "))
                    Exit Sub

                End If
                Dim HId = SaveHeaderAndGetId()
                InsertDetailsForHeader(HId)

                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done / تم الحفظ"))
                ClearFormValuesToNothing()
                SetNextHeaderCode()
            Case "Save"

                Dim empCode As String = txtEmployee.Text.Trim()
                If String.IsNullOrEmpty(empCode) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Employee /برجاء إدخال الموظف"))
                    Exit Sub
                End If

                Dim appraisalTypeId As Integer
                Integer.TryParse(If(DdlAppraisalType.SelectedValue, "0"), appraisalTypeId)
                If appraisalTypeId <= 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please select Appraisal Type / برجاء اختيار نوع التقييم"))
                    Exit Sub
                End If

                Dim fDate As Date, tDate As Date
                If Not Date.TryParse(Convert.ToString(txtFromDate.Value), fDate) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please enter From Date / برجاء إدخال من تاريخ"))
                    Exit Sub
                End If
                If Not Date.TryParse(Convert.ToString(txtToDate.Value), tDate) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please enter To Date / برجاء إدخال إلى تاريخ"))
                    Exit Sub
                End If

                ' Use user-entered Total Weight from txtTotalWeight, not grid sum
                Dim totalWeight As Decimal
                If Not Decimal.TryParse(Convert.ToString(txtTotalWeight.Text), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, totalWeight) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please enter a valid Total Weight / برجاء إدخال إجمالي وزن صحيح"))
                    Exit Sub
                End If
                Dim TotalCriteriasWeight As Integer = 0
                For Each r As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows

                    TotalCriteriasWeight += r.Cells.FromKey("Weight").Value
                Next

                If TotalCriteriasWeight <> txtTotalWeight.Text Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please enter a valid Total Weight / برجاء إدخال إجمالي وزن صحيح "))
                    Exit Sub

                End If
                Dim HId = SaveHeaderAndGetId()
                InsertDetailsForHeader(HId)

                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done / تم الحفظ"))


            Case "New"
                ClearFormValuesToNothing()
                SetNextHeaderCode()
                AfterOperation()
            Case "Delete"
                'clsHrsEmployeeOfficialVacations.Delete("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                ClearFormValuesToNothing()
                SetNextHeaderCode()
                AfterOperation()
            Case "Property"
                'clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & clsHrsEmployeeOfficialVacations.ID & "&TableName=" & clsHrsEmployeeOfficialVacations.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                'clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & clsHrsEmployeeOfficialVacations.ID & "&TableName=" & clsHrsEmployeeOfficialVacations.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = "App_AppraisalEmployeesKPIs_H"
                'clsHrsEmployeeOfficialVacations.Find(" year = '" & ddlFiscalYear.SelectedItem.Text & "'")
                Dim recordID As Integer = clsHrsEmployeeOfficialVacations.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & clsHrsEmployeeOfficialVacations.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                'clsHrsEmployeeOfficialVacations.Find(" Year= '" & ddlFiscalYear.SelectedItem.Text & "'")
                If clsHrsEmployeeOfficialVacations.ID > 0 Then
                    Dim Ds As Data.DataSet = clsHrsEmployeeOfficialVacations.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If clsHrsEmployeeOfficialVacations.CheckDiff(clsHrsEmployeeOfficialVacations, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                Try
                    If clsHrsEmployeeOfficialVacations Is Nothing Then
                        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
                    End If
                    Dim connStr As String = clsHrsEmployeeOfficialVacations.ConnectionString
                    Dim sqlMin As String = "SELECT MIN(CONVERT(int, Code)) FROM dbo.App_AppraisalEmployeesKPIs_H WHERE ISNUMERIC(Code)=1 AND CancelDate IS NULL"
                    Dim objMin = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(connStr, CommandType.Text, sqlMin)
                    If objMin IsNot Nothing AndAlso objMin IsNot DBNull.Value Then
                        txtCode.Text = Convert.ToInt32(objMin).ToString()
                        ' Trigger load by code
                        txtCode_TextChanged(txtCode, EventArgs.Empty)
                    End If
                Catch ex As Exception
                End Try
            Case "Previous"
                Try
                    Dim curCodeInt As Integer
                    If Not Integer.TryParse(If(txtCode.Text, String.Empty).Trim(), curCodeInt) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                        Exit Select
                    End If

                    If clsHrsEmployeeOfficialVacations Is Nothing Then
                        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
                    End If
                    Dim connStr As String = clsHrsEmployeeOfficialVacations.ConnectionString
                    Dim sqlPrev As String = "SELECT MAX(CONVERT(int, Code)) FROM dbo.App_AppraisalEmployeesKPIs_H WHERE ISNUMERIC(Code)=1 AND CancelDate IS NULL AND CONVERT(int, Code) < @Cur"
                    Dim objPrev = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(connStr, CommandType.Text, sqlPrev, New SqlClient.SqlParameter("@Cur", curCodeInt))
                    If objPrev IsNot Nothing AndAlso objPrev IsNot DBNull.Value Then
                        txtCode.Text = Convert.ToInt32(objPrev).ToString()
                        txtCode_TextChanged(txtCode, EventArgs.Empty)
                    Else
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                    End If
                Catch ex As Exception
                End Try
            Case "Next"
                Try
                    Dim curCodeInt As Integer
                    If Not Integer.TryParse(If(txtCode.Text, String.Empty).Trim(), curCodeInt) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                        Exit Select
                    End If

                    If clsHrsEmployeeOfficialVacations Is Nothing Then
                        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
                    End If
                    Dim connStr As String = clsHrsEmployeeOfficialVacations.ConnectionString
                    Dim sqlNext As String = "SELECT MIN(CONVERT(int, Code)) FROM dbo.App_AppraisalEmployeesKPIs_H WHERE ISNUMERIC(Code)=1 AND CancelDate IS NULL AND CONVERT(int, Code) > @Cur"
                    Dim objNext = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(connStr, CommandType.Text, sqlNext, New SqlClient.SqlParameter("@Cur", curCodeInt))
                    If objNext IsNot Nothing AndAlso objNext IsNot DBNull.Value Then
                        txtCode.Text = Convert.ToInt32(objNext).ToString()
                        txtCode_TextChanged(txtCode, EventArgs.Empty)
                    Else
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                    End If
                Catch ex As Exception
                End Try
            Case "Last"
                Try
                    If clsHrsEmployeeOfficialVacations Is Nothing Then
                        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
                    End If
                    Dim connStr As String = clsHrsEmployeeOfficialVacations.ConnectionString
                    Dim sqlMax As String = "SELECT MAX(CONVERT(int, Code)) FROM dbo.App_AppraisalEmployeesKPIs_H WHERE ISNUMERIC(Code)=1 AND CancelDate IS NULL"
                    Dim objMax = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(connStr, CommandType.Text, sqlMax)
                    If objMax IsNot Nothing AndAlso objMax IsNot DBNull.Value Then
                        txtCode.Text = Convert.ToInt32(objMax).ToString()
                        txtCode_TextChanged(txtCode, EventArgs.Empty)
                    End If
                Catch ex As Exception
                End Try
        End Select
    End Sub
    'Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFiscalYear.TextChanged
    '    CheckCode()
    'End Sub
    Protected Sub ddlAppraisalType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAppraisalType.SelectedIndexChanged
        Try
            CalculateAppraisalDates()
            If DdlAppraisalType.SelectedValue > 0 Then
                Dim ConnectionString As String = clsHrsEmployeeOfficialVacations.ConnectionString

                Dim str As String = " select KPIsWeight from App_AppraisalTypes where id=" & DdlAppraisalType.SelectedValue & " "
                Dim obj As String = SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, str)
                If Not String.IsNullOrEmpty(obj) Then
                    txtTotalWeight.Text = obj
                    txtTotalWeight.Enabled = False
                End If
                Dim str1 As String = " select * from App_AppraisalTypes where id=" & DdlAppraisalType.SelectedValue & " "
                Dim ds As DataSet = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, str1)
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim IsOneTimeOnly As Boolean = ds.Tables(0).Rows(0)("IsOneTimeOnly")
                    If IsOneTimeOnly Then
                        Dim OneTimePeriod As Integer = ds.Tables(0).Rows(0)("OneTimePeriod")

                        txtToDate.Value = CDate(txtFromDate.Value).AddDays(OneTimePeriod)
                    Else
                        Dim AppraisalFrequency As Boolean = ds.Tables(0).Rows(0)("AppraisalFrequency")

                        txtToDate.Value = CDate(txtFromDate.Value).AddDays(AppraisalFrequency)

                    End If

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub TxtEmpCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmployee.TextChanged
        If Not String.IsNullOrEmpty(txtEmployee.Text) Then
            Dim clsEmployees = New Clshrs_Employees(Me)
            clsEmployees.Find("Code='" & txtEmployee.Text & "'")
            CheckDepartmentPermission()
        Else
            txtEmployeeName.Text = ""

        End If

    End Sub
    ' When Code changes, load header and detail rows and populate controls
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        Try
            Dim codeVal As String = If(txtCode.Text, String.Empty).Trim()
            If String.IsNullOrEmpty(codeVal) Then Exit Sub
            LoadHeaderAndDetailsByCode(txtCode.Text)

        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Private Functions"
    Private Sub CheckDepartmentPermission()

        Dim ClsEmployees = New Clshrs_Employees(Me)
        ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
        If Not String.IsNullOrEmpty(ClsEmployees.ArabicName.ToString()) Then
            Dim clsCompanies = New Clssys_Companies(Me.Page)
            Dim Cls_Employeees As New Clshrs_Employees(Me)

            clsCompanies.Find("ID = " & Cls_Employeees.MainCompanyID)
            If clsCompanies.UserDepartmentsPermissions Then
                Dim User As String = String.Empty
                Dim WebHandler As New Venus.Shared.Web.WebHandler

                WebHandler.GetCookies(Page, "UserID", User)
                Dim _sys_User As New Clssys_Users(Page)
                _sys_User.Find("ID = '" & User & "'")
                Cls_Employeees.Find("Code='" & _sys_User.Code & "'")
                Dim dsLocPermission As DataSet
                Dim LocsPermission = ""

                Dim strLoc As String = "SELECT [LocationId] FROM [dbo].[hrs_EmployeeLocations] where [EmpId]='" & Cls_Employeees.ID & "'"
                dsLocPermission = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, strLoc)
                LocsPermission = "(-1"
                For h As Integer = 0 To dsLocPermission.Tables(0).Rows.Count - 1
                    LocsPermission = LocsPermission & "," & Convert.ToString(dsLocPermission.Tables(0).Rows(h)("LocationId"))
                Next
                LocsPermission = LocsPermission & ")"
                Dim strchecklocationEMP As String
                Dim DSAuthEmployees As DataSet
                strchecklocationEMP = "select * from hrs_Employees where code='" & txtEmployee.Text & "' and hrs_Employees.LocationID in " & LocsPermission & ""
                DSAuthEmployees = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, strchecklocationEMP)
                If DSAuthEmployees.Tables(0).Rows.Count > 0 Then

                    If ProfileCls.CurrentLanguage = "Ar" Then

                        txtEmployeeName.Text = ClsEmployees.FullName
                    Else
                        txtEmployeeName.Text = ClsEmployees.FullName
                    End If

                Else
                        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry you don't have a permission for this employee   / عفوا ليس لديك صلاحية علي هذا الموظف"))
                    txtEmployeeName.Text = ""

                End If


            Else


                If ProfileCls.CurrentLanguage = "Ar" Then

                    txtEmployeeName.Text = ClsEmployees.ArabicName
                Else
                    txtEmployeeName.Text = ClsEmployees.EnglishName
                End If

                GetEmployeeData()

            End If


        Else
            txtEmployeeName.Text = ""
        End If

    End Sub

    Private Sub GetEmployeeData()
        Dim Cls_Employees As New Clshrs_Employees(Me)
        Cls_Employees.Find("Code = '" & txtCode.Text & "'")
        Dim ObjNavigationHandle As New Venus.Shared.Web.NavigationHandler(Cls_Employees.ConnectionString)
        Dim clsDepartments As New Clssys_Departments(Me)

        Dim clsCompanies = New Clssys_Companies(Me.Page)
        clsCompanies.Find("ID = " & Cls_Employees.MainCompanyID)

        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler

        WebHandler.GetCookies(Page, "UserID", User)
        Dim _sys_User As New Clssys_Users(Page)
        _sys_User.Find("ID = '" & User & "'")
        Dim ClsEmployees As New Clshrs_Employees(Page)
        ClsEmployees.Find("Code='" & _sys_User.Code & "'")

        Dim departsPermission = ""
        If clsCompanies.UserDepartmentsPermissions Then
            Dim dsDepPermission As DataSet
            Dim str As String = "SELECT [DepartmentId] FROM [dbo].[hrs_EmployeeDepartments] where [EmpId]='" & ClsEmployees.ID & "'"
            dsDepPermission = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, str)
            departsPermission = "(-1"
            For i As Integer = 0 To dsDepPermission.Tables(0).Rows.Count - 1
                departsPermission = departsPermission & "," & Convert.ToString(dsDepPermission.Tables(0).Rows(i)("DepartmentId"))
            Next
            departsPermission = departsPermission & ")"
        End If
        Dim deprtPermFilter = ""
        If departsPermission <> "" Then
            deprtPermFilter = " and ID in " & departsPermission
        End If

        clsDepartments.Find("ID=" & Cls_Employees.DepartmentID & "" & deprtPermFilter)
        If clsDepartments.DataSet.Tables(0).Rows.Count > 0 Then


            Dim ObjTreeNode As New Infragistics.WebUI.UltraWebNavigator.Node
            ObjTreeNode.Text = clsDepartments.DataSet.Tables(0).Rows(0)((ObjNavigationHandle.SetLanguage(Me, "EngName/ArbName")))
            If (ObjTreeNode.Text.Trim = String.Empty) Then
                ObjTreeNode.Text = clsDepartments.DataSet.Tables(0).Rows(0)((ObjNavigationHandle.SetLanguage(Me, "ArbName/EngName")))
            End If
            '

            Dim ObjTreeNodeSub As New Infragistics.WebUI.UltraWebNavigator.Node
            ObjTreeNodeSub.Text = Cls_Employees.ArabicName
            ObjTreeNodeSub.Tag = Cls_Employees.ID
            ObjTreeNode.Nodes.Add(ObjTreeNodeSub)


        End If
    End Sub

    Private Sub CalculateAppraisalDates()
        Try
            ' Preconditions

            If String.IsNullOrWhiteSpace(txtEmployee.Text) Then Exit Sub
            If DdlAppraisalType Is Nothing OrElse DdlAppraisalType.SelectedIndex < 0 Then Exit Sub

            Dim empCode As String = txtEmployee.Text.Trim()
            Dim appraisalTypeId As Integer = 0
            Integer.TryParse(If(DdlAppraisalType.SelectedValue, "0"), appraisalTypeId)
            If appraisalTypeId <= 0 Then Exit Sub

            ' 1) Prefer last appraisal ToDate for this employee and appraisal type
            Dim lastTo As DateTime? = GetLastAppraisalToDate(empCode, appraisalTypeId)
            If lastTo.HasValue Then
                txtFromDate.Value = lastTo.Value.AddDays(1)
                Return
            End If

            ' 2) Fallback to employee join date
            Dim joinDate As DateTime? = GetEmployeeJoinDate(empCode)
            If joinDate.HasValue Then
                txtFromDate.Value = joinDate.Value
            Else
                ' If not found, clear FromDate to avoid stale values
                txtFromDate.Value = Nothing
            End If
        Catch ex As Exception
            ' Fail-safe: do not throw in UI; clear to avoid stale values
            'If dtFromDate IsNot Nothing Then dtFromDate.Value = Nothing
        End Try
    End Sub

    ' Returns the last saved ToDate for the employee and appraisal type from header table, or Nothing if none
    Private Function GetLastAppraisalToDate(empCode As String, appraisalTypeId As Integer) As DateTime?
        Try
            ' Resolve EmployeeID from code
            Dim emp As New Clshrs_Employees(Me)
            If Not emp.Find(" Code='" & empCode.Replace("'", "''") & "'") Then Return Nothing
            If emp.ID Is Nothing OrElse Convert.ToInt32(emp.ID) <= 0 Then Return Nothing

            ' Lookup last ToDate from header table if it exists
            Dim connStr As String = ConfigurationManager.AppSettings("Connstring").ToString()
            Dim sql As String = "SET DATEFORMAT DMY; SELECT TOP 1 ToDate FROM dbo.App_AppraisalEmployeesKPIs_H  WHERE EmployeeID=@EmpID AND AppraisalTypeID=@TypeID AND CancelDate IS NULL ORDER BY ToDate DESC"
            Dim obj = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(connStr, CommandType.Text, sql, New SqlClient.SqlParameter("@EmpID", Convert.ToInt32(emp.ID)), New SqlClient.SqlParameter("@TypeID", appraisalTypeId))
            If obj IsNot Nothing AndAlso obj IsNot DBNull.Value Then
                Dim dt As DateTime
                If DateTime.TryParse(obj.ToString(), dt) Then Return dt
            End If
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' Loads header from App_AppraisalEmployeesKPIs_H and details from App_AppraisalEmployeesKPIs_D by Code
    ' Populates controls and grid. Returns True if found.
    Private Function LoadHeaderAndDetailsByCode(codeVal As String) As Boolean
        Try
            If clsHrsEmployeeOfficialVacations Is Nothing Then
                clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
            End If
            Dim connStr As String = clsHrsEmployeeOfficialVacations.ConnectionString

            ' Header
            Dim hdrSql As String = "SET DATEFORMAT DMY; SELECT TOP 1 ID, Code, EmployeeID, AppraisalTypeID, FromDate, ToDate, TotalWeight, Description, RegUserID, RegDate FROM dbo.App_AppraisalEmployeesKPIs_H WHERE Code=@Code AND CancelDate IS NULL ORDER BY ID DESC"
            Dim headerId As Integer = 0
            Using con As New SqlClient.SqlConnection(connStr)
                Using cmd As New SqlClient.SqlCommand(hdrSql, con)
                    cmd.Parameters.AddWithValue("@Code", codeVal)
                    con.Open()
                    Using rd = cmd.ExecuteReader()
                        If rd.Read() Then
                            headerId = Convert.ToInt32(rd("ID"))
                            ' Set controls
                            txtCode.Text = Convert.ToString(rd("Code"))
                            Dim empId As Integer = If(IsDBNull(rd("EmployeeID")), 0, Convert.ToInt32(rd("EmployeeID")))
                            Dim apprTypeId As Integer = If(IsDBNull(rd("AppraisalTypeID")), 0, Convert.ToInt32(rd("AppraisalTypeID")))
                            Dim fromDate As DateTime = If(IsDBNull(rd("FromDate")), Nothing, Convert.ToDateTime(rd("FromDate")))
                            Dim toDate As DateTime = If(IsDBNull(rd("ToDate")), Nothing, Convert.ToDateTime(rd("ToDate")))
                            Dim totalW As Decimal = If(IsDBNull(rd("TotalWeight")), 0D, Convert.ToDecimal(rd("TotalWeight")))
                            Dim descr As String = If(IsDBNull(rd("Description")), Nothing, Convert.ToString(rd("Description")))

                            ' Employee info
                            Try
                                If empId > 0 Then
                                    Dim emp As New Clshrs_Employees(Me)
                                    If emp.Find(" ID=" & empId) Then
                                        txtEmployee.Text = Convert.ToString(emp.Code)
                                        txtEmployeeName.Text = Convert.ToString(emp.FullName)
                                    End If
                                End If
                            Catch
                            End Try

                            ' Appraisal type
                            Try
                                If apprTypeId > 0 AndAlso DdlAppraisalType IsNot Nothing Then
                                    Dim li = DdlAppraisalType.Items.FindByValue(apprTypeId.ToString())
                                    If li IsNot Nothing Then
                                        DdlAppraisalType.ClearSelection()
                                        li.Selected = True
                                    End If
                                End If
                            Catch
                            End Try

                            ' Dates and other fields
                            txtFromDate.Value = If(fromDate = DateTime.MinValue, Nothing, fromDate)
                            txtToDate.Value = If(toDate = DateTime.MinValue, Nothing, toDate)
                            txtTotalWeight.Text = totalW.ToString(System.Globalization.CultureInfo.InvariantCulture)
                            txtDescription.Text = descr
                        Else
                            Return False
                        End If
                    End Using
                End Using

                ' Details (bound grid pattern)
                Dim detSql As String = "SELECT LineNum, KPIName, MinScore, MaxScore, Weight FROM dbo.App_AppraisalEmployeesKPIs_D  WHERE KPIsHeaderID=@HID ORDER BY LineNum"
                Using cmdDet As New SqlClient.SqlCommand(detSql, con)
                    cmdDet.Parameters.AddWithValue("@HID", headerId)
                    Using rdDet = cmdDet.ExecuteReader()
                        Dim dt As New DataTable()
                        dt.Columns.Add("Serial", GetType(Integer))
                        dt.Columns.Add("KPI", GetType(String))
                        dt.Columns.Add("MinimumScore", GetType(Decimal))
                        dt.Columns.Add("MaximumScore", GetType(Decimal))
                        dt.Columns.Add("Weight", GetType(Decimal))

                        While rdDet.Read()
                            Dim dr = dt.NewRow()
                            dr("Serial") = If(IsDBNull(rdDet("LineNum")), DBNull.Value, rdDet("LineNum"))
                            dr("KPI") = If(IsDBNull(rdDet("KPIName")), DBNull.Value, rdDet("KPIName"))
                            dr("MinimumScore") = If(IsDBNull(rdDet("MinScore")), DBNull.Value, rdDet("MinScore"))
                            dr("MaximumScore") = If(IsDBNull(rdDet("MaxScore")), DBNull.Value, rdDet("MaxScore"))
                            dr("Weight") = If(IsDBNull(rdDet("Weight")), DBNull.Value, rdDet("Weight"))
                            dt.Rows.Add(dr)
                        End While

                        If dt.Rows.Count = 0 Then
                            dt.Rows.Add(dt.NewRow())
                        End If

                        UwgSearchEmployees.DataSource = dt
                        UwgSearchEmployees.DataBind()
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' Returns next numeric Code as string from App_AppraisalEmployeesKPIs_H (MAX(TRY_CONVERT(int, Code)) + 1)
    Private Function GetNextHeaderCode() As String
        Try
            If clsHrsEmployeeOfficialVacations Is Nothing Then
                clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
            End If
            Dim connStr As String = clsHrsEmployeeOfficialVacations.ConnectionString

            Dim sql As String = "SELECT ISNULL(MAX(CONVERT(int, Code)), 0) + 1 FROM dbo.App_AppraisalEmployeesKPIs_H"
            Using con As New SqlClient.SqlConnection(connStr)
                Using cmd As New SqlClient.SqlCommand(sql, con)
                    cmd.CommandType = CommandType.Text
                    con.Open()
                    Dim obj As Object = cmd.ExecuteScalar()
                    If obj IsNot Nothing AndAlso obj IsNot DBNull.Value Then
                        Return obj.ToString()
                    End If
                End Using
            End Using
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' Sets txtCode to the next available numeric code
    Private Sub SetNextHeaderCode()
        Try
            Dim nxt As String = GetNextHeaderCode()
            If Not String.IsNullOrEmpty(nxt) AndAlso txtCode IsNot Nothing Then
                txtCode.Text = nxt
            End If
        Catch ex As Exception
        End Try
    End Sub

    ' Returns the employee join date by employee code, or Nothing if not found
    Private Function GetEmployeeJoinDate(empCode As String) As DateTime?
        Try
            Dim emp As New Clshrs_Employees(Me)
            If emp.Find(" Code='" & empCode.Replace("'", "''") & "'") Then
                If emp.ID IsNot Nothing AndAlso Convert.ToInt32(emp.ID) > 0 Then
                    Return emp.JoinDate
                End If
            End If
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    ' Inserts a new header row in App_AppraisalEmployeesKPIs_H using current control values and returns the new ID
    ' Returns 0 on failure
    Private Function SaveHeaderAndGetId() As Integer
        Try
            ' Resolve connection string
            Dim connStr As String
            If clsHrsEmployeeOfficialVacations Is Nothing Then
                clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
            End If
            connStr = clsHrsEmployeeOfficialVacations.ConnectionString

            ' Resolve EmployeeID from employee code
            Dim empCode As String = If(txtEmployee.Text, String.Empty).Trim()
            If String.IsNullOrEmpty(empCode) Then Return 0
            Dim emp As New Clshrs_Employees(Me)
            If Not emp.Find(" Code='" & empCode.Replace("'", "''") & "'") Then Return 0
            If emp.ID Is Nothing OrElse Convert.ToInt32(emp.ID) <= 0 Then Return 0
            Dim employeeId As Integer = Convert.ToInt32(emp.ID)

            ' Collect other values
            Dim codeVal As String = If(txtCode.Text, String.Empty).Trim()
            Dim appraisalTypeId As Integer = 0
            Integer.TryParse(Convert.ToString(DdlAppraisalType.SelectedValue), appraisalTypeId)
            If appraisalTypeId <= 0 Then Return 0

            Dim fromDate As Date
            Dim toDate As Date
            If Not Date.TryParse(Convert.ToString(txtFromDate.Value), fromDate) Then Return 0
            If Not Date.TryParse(Convert.ToString(txtToDate.Value), toDate) Then Return 0

            Dim totalWeight As Decimal
            If Not Decimal.TryParse(Convert.ToString(txtTotalWeight.Text), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, totalWeight) Then
                Return 0
            End If

            Dim descriptionVal As String = If(txtDescription.Text, String.Empty).Trim()

            Dim regUserId As Integer = 0
            Try
                regUserId = clsHrsEmployeeOfficialVacations.RegUserID
            Catch
            End Try
            Dim regDate As DateTime = DateTime.Now

            ' Insert and return new identity
            Dim sql As String = "SET DATEFORMAT DMY; " &
                                "INSERT INTO [dbo].[App_AppraisalEmployeesKPIs_H] " &
                                "([Code],[EmployeeID],[AppraisalTypeID],[FromDate],[ToDate],[TotalWeight],[Description],[RegUserID],[RegDate]) " &
                                "VALUES (@Code,@EmployeeID,@AppraisalTypeID,@FromDate,@ToDate,@TotalWeight,@Description,@RegUserID,@RegDate); " &
                                "SELECT CAST(SCOPE_IDENTITY() AS int);"

            Using con As New SqlClient.SqlConnection(connStr)
                Using cmd As New SqlClient.SqlCommand(sql, con)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.AddWithValue("@Code", If(String.IsNullOrEmpty(codeVal), CType(DBNull.Value, Object), codeVal))
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId)
                    cmd.Parameters.AddWithValue("@AppraisalTypeID", appraisalTypeId)
                    cmd.Parameters.AddWithValue("@FromDate", fromDate)
                    cmd.Parameters.AddWithValue("@ToDate", toDate)
                    cmd.Parameters.AddWithValue("@TotalWeight", totalWeight)
                    cmd.Parameters.AddWithValue("@Description", If(String.IsNullOrEmpty(descriptionVal), CType(DBNull.Value, Object), descriptionVal))
                    cmd.Parameters.AddWithValue("@RegUserID", regUserId)
                    cmd.Parameters.AddWithValue("@RegDate", regDate)

                    con.Open()
                    Dim newIdObj As Object = cmd.ExecuteScalar()
                    If newIdObj IsNot Nothing AndAlso newIdObj IsNot DBNull.Value Then
                        Return Convert.ToInt32(newIdObj)
                    End If
                End Using
            End Using

            Return 0
        Catch ex As Exception
            ' Optional: log error using existing error handler pattern
            Return 0
        End Try
    End Function

    ' Inserts detail rows for the given header using rows from UwgSearchEmployees
    ' Returns number of inserted rows
    Private Function InsertDetailsForHeader(headerId As Integer) As Integer
        If headerId <= 0 Then Return 0
        If UwgSearchEmployees Is Nothing OrElse UwgSearchEmployees.Rows Is Nothing Then Return 0

        Try
            If clsHrsEmployeeOfficialVacations Is Nothing Then
                clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
            End If
            Dim connStr As String = clsHrsEmployeeOfficialVacations.ConnectionString
            Dim regUserId As Integer = 0
            Try
                regUserId = clsHrsEmployeeOfficialVacations.RegUserID
            Catch
            End Try

            Dim inserted As Integer = 0
            Using con As New SqlClient.SqlConnection(connStr)
                con.Open()
                Using trn As SqlClient.SqlTransaction = con.BeginTransaction()
                    Try
                        Dim sql As String = "INSERT INTO [dbo].[App_AppraisalEmployeesKPIs_D]" &
                                            "([KPIsHeaderID],[LineNum],[KPIName],[MinScore],[MaxScore],[Weight],[RegUserID],[RegDate]) " &
                                            "VALUES (@HeaderID,@LineNum,@KPIName,@MinScore,@MaxScore,@Weight,@RegUserID,@RegDate)"

                        Dim lineNum As Integer = 0
                        For Each r As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                            ' Read values safely


                            Dim kpiName As String = String.Empty
                            If r.Cells.FromKey("KPI") IsNot Nothing AndAlso r.Cells.FromKey("KPI").Value IsNot Nothing Then
                                kpiName = Convert.ToString(r.Cells.FromKey("KPI").Value).Trim()
                            End If

                            ' Skip empty rows
                            If String.IsNullOrEmpty(kpiName) Then
                                Continue For
                            End If

                            Dim minScore As Decimal = 0D
                            If r.Cells.FromKey("MinimumScore") IsNot Nothing Then
                                Decimal.TryParse(Convert.ToString(r.Cells.FromKey("MinimumScore").Value), minScore)
                            End If
                            Dim maxScore As Decimal = 0D
                            If r.Cells.FromKey("MaximumScore") IsNot Nothing Then
                                Decimal.TryParse(Convert.ToString(r.Cells.FromKey("MaximumScore").Value), maxScore)
                            End If

                            Dim weight As Decimal = 0D
                            If r.Cells.FromKey("Weight") IsNot Nothing Then
                                Decimal.TryParse(Convert.ToString(r.Cells.FromKey("Weight").Value), weight)
                            End If

                            ' Determine line number: prefer Serial column if present, else increment
                            Dim rowLine As Integer = 0
                            If r.Cells.FromKey("Serial") IsNot Nothing AndAlso Integer.TryParse(Convert.ToString(r.Cells.FromKey("Serial").Value), rowLine) AndAlso rowLine > 0 Then
                                ' use rowLine
                            Else
                                lineNum += 1
                                rowLine = lineNum
                            End If

                            Using cmd As New SqlClient.SqlCommand(sql, con, trn)
                                cmd.CommandType = CommandType.Text
                                cmd.Parameters.AddWithValue("@HeaderID", headerId)
                                cmd.Parameters.AddWithValue("@LineNum", rowLine)
                                cmd.Parameters.AddWithValue("@KPIName", If(String.IsNullOrEmpty(kpiName), CType(DBNull.Value, Object), kpiName))
                                cmd.Parameters.AddWithValue("@MinScore", minScore)
                                cmd.Parameters.AddWithValue("@MaxScore", maxScore)
                                cmd.Parameters.AddWithValue("@Weight", weight)
                                cmd.Parameters.AddWithValue("@RegUserID", regUserId)
                                cmd.Parameters.AddWithValue("@RegDate", DateTime.Now)
                                inserted += cmd.ExecuteNonQuery()
                            End Using
                        Next

                        trn.Commit()
                    Catch ex As Exception
                        trn.Rollback()
                        Throw
                    End Try
                End Using
            End Using

            Return inserted
        Catch ex As Exception
            Return 0
        End Try
    End Function
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
    Private Function AssignValues() As Boolean
        Try

            Return True
        Catch ex As Exception
        End Try
    End Function

    Private Function ValidateFromToDateValues() As Boolean
        Try
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsHrsEmployeeOfficialVacations.ConnectionString)
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                If Not IsNothing(DGRow.Cells("3").Value) Then
                    If IsNothing(DGRow.Cells("4").Value) Or CStr(DGRow.Cells("4").Value) = "  /  /    " Then
                        Return False
                    End If
                    If IsNothing(DGRow.Cells("5").Value) Or CStr(DGRow.Cells("5").Value) = "  /  /    " Then
                        Return False
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            Dim str = ex.ToString()
        End Try
    End Function
    Private Function GetValues() As Boolean

        Dim ClsUser As New Clshrs_OfficialVacations(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            'ddlFiscalYear.SelectedItem.Text = clsHrsEmployeeOfficialVacations.Year
            'txtEngName.Text = clsHrsEmployeeOfficialVacations.EngName
            'txtArbName.Text = clsHrsEmployeeOfficialVacations.ArbName
            Dim mFindDataset As New DataSet
            'mFindDataset = ClsUser.Find("Year=" & ddlFiscalYear.SelectedItem.Text)

            UwgSearchEmployees.DataSource = mFindDataset.Tables(0)
            UwgSearchEmployees.DataBind()
            UwgSearchEmployees.Rows.Add()
            Dim item As New System.Web.UI.WebControls.ListItem()
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                If IsNothing(DGRow.Cells(1).Value) Then
                    Continue For
                End If

            Next
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsHrsEmployeeOfficialVacations.ConnectionString)
            If (UwgSearchEmployees.Rows.Count > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
                'UwgSearchEmployees.Rows.Add()
                'UwgSearchEmployees.Rows(0).Cells("LineNum").Value = 1

            End If
            SetToolBarPermission(Me, clsHrsEmployeeOfficialVacations.ConnectionString, clsHrsEmployeeOfficialVacations.DataBaseUserRelatedID, clsHrsEmployeeOfficialVacations.GroupID, StrMode)
            SetToolBarRecordPermission(Me, clsHrsEmployeeOfficialVacations.ConnectionString, clsHrsEmployeeOfficialVacations.DataBaseUserRelatedID, clsHrsEmployeeOfficialVacations.GroupID, clsHrsEmployeeOfficialVacations.Table, clsHrsEmployeeOfficialVacations.ID)
            If Not clsHrsEmployeeOfficialVacations.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(clsHrsEmployeeOfficialVacations.ID)
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
                    'ddlFiscalYear.SelectedItem.Text = String.Empty
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
                    clsHrsEmployeeOfficialVacations.Find("ID=" & intID)
                    GetValues()
                    'ddlFiscalYear.Enabled = False
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    clsHrsEmployeeOfficialVacations.Find("ID=" & intID)
                    GetValues()
                    'ddlFiscalYear.Enabled = False
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With clsHrsEmployeeOfficialVacations
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
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
        Try
            With clsHrsEmployeeOfficialVacations
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
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
        If IntId > 0 Then
            clsHrsEmployeeOfficialVacations.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
        Try

            GetValues()

        Catch ex As Exception
        End Try
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        LoadHeaderAndDetailsByCode(txtCode.Text)
        'Venus.Shared.Web.ClientSideActions.SetFocus(Page, ddlFiscalYear, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean


        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    ' Sets all control values to Nothing (null) and clears selections
    Private Sub ClearFormValuesToNothing()
        Try
            ' Text boxes
            txtCode.Text = Nothing
            txtEmployee.Text = Nothing
            txtEmployeeName.Text = Nothing
            txtTotalWeight.Text = Nothing
            txtDescription.Text = Nothing

            ' Date inputs
            txtFromDate.Value = Nothing
            txtToDate.Value = Nothing

            ' Dropdowns

            DdlAppraisalType.ClearSelection()
            DdlAppraisalType.SelectedIndex = -1



            ' Grid - reset to empty
            If UwgSearchEmployees IsNot Nothing Then
                UwgSearchEmployees.DataSource = Nothing
                UwgSearchEmployees.DataBind()
                UwgSearchEmployees.Rows.Add()
            End If

            ' Buttons / tab state
            ImageButton_Delete.Enabled = False
            If UltraWebTab1 IsNot Nothing Then UltraWebTab1.SelectedTabIndex = 0

            SetNextHeaderCode()

        Catch ex As Exception
        End Try
    End Sub
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Page)
        'clsHrsEmployeeOfficialVacations.Find(" Year = '" & ddlFiscalYear.SelectedItem.Text & "'")
        Dim recordID As Integer = clsHrsEmployeeOfficialVacations.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, clsHrsEmployeeOfficialVacations.Table) = True Then
            Dim StrTablename As String
            clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
            StrTablename = clsHrsEmployeeOfficialVacations.Table
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

    Protected Sub UwgSearchEmployees_DeleteRow(sender As Object, e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles UwgSearchEmployees.DeleteRow
        If e.Row.Cells.FromKey("ID").Value <> Nothing Then
            clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsHrsEmployeeOfficialVacations.ConnectionString)
            Dim SqlCommand As String = String.Empty

            SqlCommand = "DELETE FROM [dbo].[hrs_OfficialVacations] WHERE ID=" & e.Row.Cells.FromKey("ID").Value
            Dim cmd As New SqlClient.SqlCommand
            cmd.CommandText = SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.Connection = New SqlClient.SqlConnection(clsHrsEmployeeOfficialVacations.ConnectionString)
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
            cmd.Connection.Close()
        End If
    End Sub

    Public Function GetDropDownListGrid() As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)

        Dim strselect2 As String
        strselect2 = "select ID, Code,EngName,ArbName from hrs_VacationsTypes where IsOfficial=1"
        Dim DSOfficialvacations As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect2)
        If DSOfficialvacations.Tables(0).Rows.Count > 0 Then
            UwgSearchEmployees.DisplayLayout.Bands(0).Columns(3).ValueList.ValueListItems.Clear()
            For Each Row As Data.DataRow In DSOfficialvacations.Tables(0).Rows
                UwgSearchEmployees.DisplayLayout.Bands(0).Columns(3).ValueList.ValueListItems.Add(Row("ID"), Row("Code") & " - " & ObjNavigationHandler.SetLanguage(Page, "" & Row("EngName") & "/ " & Row("ArbName") & ""))
            Next

        End If



    End Function

#End Region
End Class
