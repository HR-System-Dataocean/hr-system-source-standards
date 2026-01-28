Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Venus.Shared.Web
Imports System.Security.Cryptography
Imports System.Collections.ObjectModel
Imports System.Activities.Expressions

Partial Class frmEmployeeDecisionsHistoryPopUp
    Inherits MainPage
#Region "Public Decleration"
    Private ClsEmployeesVacations As Clshrs_EmployeesVacations
    Private ClsEmployees As Clshrs_Employees
    Dim ClsUsers As Clssys_Users
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Const csOtherFields = 11
    Dim dtDecisions As DataTable
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim recordID As Integer
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)

        If Not IsPostBack Then
            Try

                Dim WebHandler As New Venus.Shared.Web.WebHandler
                Dim User As String = String.Empty
                ClsEmployees = New Clshrs_Employees(Page)
                WebHandler.GetCookies(Page, "UserID", User)
                Dim _sys_User As New Clssys_Users(Page)
                _sys_User.Find("ID = '" & User & "'")
                fillDDl(False)
                ddlFromDate.Value = ClsEmployeesVacations.GetHigriDate("01/01/" & DateTime.Now.Year - 1.ToString())
                ddlToDate.Value = ClsEmployeesVacations.GetHigriDate(DateTime.Now.ToString("dd/MM/yyyy"))

                'Dim filter As String
                'filter = " and hrs_EmployeesDecisions.RegDate>='" & Convert.ToDateTime(ddlFromDate.Value.ToString()).ToString("yyyy-MM-dd") & "' and cast( hrs_EmployeesDecisions.RegDate as date)<='" & Convert.ToDateTime(ddlToDate.Value).ToString("yyyy-MM-dd") & "'"

                ''filter += " and isnull(IsTransfered,0)=0"
                'GetAllEmployeeRequests(filter)
                Dim IntRecordID As Int64 = Request.QueryString.Item("ID")
                txtEmployee.Text = IntRecordID.ToString()
                txtEmployee.Enabled = False
                btnSearchCode.Enabled = False
                txtEngName.Enabled = False
                txtEmployee_TextChanged(Nothing, Nothing)

                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
                Dim ClsObjects As New Clssys_Objects(Page)
                Dim ClsSearchs As New Clssys_Searchs(Page)
                Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language = ""javascript"">IntializeDataChanged()</script>")
                If ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'") Then
                    If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                        Dim SearchID = ClsSearchs.ID
                        Dim IntDimension As Integer = 510
                        Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmployee.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtEmployee.ClientID & "'"
                        btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                    End If
                End If

                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
            Catch ex As Exception
                mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
                Page.Session.Add("ErrorValue", ex)
                mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
                Page.Response.Redirect("ErrorPage.aspx")
            End Try

        End If



    End Sub
    Protected Sub txtEmployee_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmployee.TextChanged
        ClsEmployees = New Clshrs_Employees(Me)
        Dim ClsEmployeesDecisions = New Clshrs_EmployeesDecisions(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Try
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            If ClsEmployees.ID > 0 Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    txtEngName.Text = ClsEmployees.ArbName & " " &
                                 ClsEmployees.FatherArbName & " " &
                                 ClsEmployees.GrandArbName & " " &
                                 ClsEmployees.FamilyArbName
                Else
                    txtEngName.Text = ClsEmployees.EngName & " " &
                                 ClsEmployees.FatherEngName & " " &
                                 ClsEmployees.GrandEngName & " " &
                                 ClsEmployees.FamilyEngName
                End If
            Else
                txtEngName.Text = ""
            End If

            Dim filter As String
            filter = " and hrs_EmployeesDecisions.RegDate>='" & Convert.ToDateTime(ddlFromDate.Value.ToString()).ToString("yyyy-MM-dd") & "' and cast( hrs_EmployeesDecisions.RegDate as date)<='" & Convert.ToDateTime(ddlToDate.Value).ToString("yyyy-MM-dd") & "'"
            If txtEmployee.Text <> "" Then
                filter += " and hrs_employeesDecisions.Code='" & txtEmployee.Text & "'"
            End If
            If ddlDecisions.SelectedValue <> "" Then
                filter += " and hrs_employeesDecisions." & ddlDecisions.SelectedValue & " <> ''"
            End If
            'filter += " and isnull(IsTransfered,0)=0"
            GetAllEmployeeRequests(filter)

        Catch ex As Exception
        End Try

    End Sub
    Protected Sub ddlDecisions_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDecisions.TextChanged
        Dim filter As String
        filter = " and hrs_EmployeesDecisions.RegDate>='" & Convert.ToDateTime(ddlFromDate.Value.ToString()).ToString("yyyy-MM-dd") & "' and cast( hrs_EmployeesDecisions.RegDate as date)<='" & Convert.ToDateTime(ddlToDate.Value).ToString("yyyy-MM-dd") & "'"
        If txtEmployee.Text <> "" Then
            filter += " and hrs_employeesDecisions.Code='" & txtEmployee.Text & "'"
        End If
        If ddlDecisions.SelectedValue <> "" Then
            filter += " and hrs_employeesDecisions." & ddlDecisions.SelectedValue & " <> ''"
        End If
        'filter += " and isnull(IsTransfered,0)=0"
        GetAllEmployeeRequests(filter)
    End Sub
    Protected Sub txtFromDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFromDate.ValueChanged
        Dim filter As String
        filter = " and hrs_EmployeesDecisions.RegDate>='" & Convert.ToDateTime(ddlFromDate.Value.ToString()).ToString("yyyy-MM-dd") & "' and cast( hrs_EmployeesDecisions.RegDate as date)<='" & Convert.ToDateTime(ddlToDate.Value).ToString("yyyy-MM-dd") & "'"
        If txtEmployee.Text <> "" Then
            filter += " and hrs_employeesDecisions.Code='" & txtEmployee.Text & "'"
        End If
        If ddlDecisions.SelectedValue <> "" Then
            filter += " and hrs_employeesDecisions." & ddlDecisions.SelectedValue & " <> ''"
        End If
        'filter += " and isnull(IsTransfered,0)=0"
        GetAllEmployeeRequests(filter)

    End Sub

    Protected Sub txtToDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlToDate.ValueChanged
        Dim filter As String
        filter = " and hrs_EmployeesDecisions.RegDate>='" & Convert.ToDateTime(ddlFromDate.Value.ToString()).ToString("yyyy-MM-dd") & "' and cast( hrs_EmployeesDecisions.RegDate as date)<='" & Convert.ToDateTime(ddlToDate.Value).ToString("yyyy-MM-dd") & "'"
        If txtEmployee.Text <> "" Then
            filter += " and hrs_employeesDecisions.Code='" & txtEmployee.Text & "'"
        End If
        If ddlDecisions.SelectedValue <> "" Then
            filter += " and hrs_employeesDecisions." & ddlDecisions.SelectedValue & " <> ''"
        End If
        GetAllEmployeeRequests(filter)

    End Sub


    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Delete.Command
        Dim IntId As Integer
        Dim strMode As String
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesVacations.ConnectionString)
        'If uwgEmployeeVacations.Rows.Count > 0 Then
        '    IntId = uwgEmployeeVacations.Rows(0).Cells.FromKey("ID").Value
        '    strMode = "E"
        'Else
        '    strMode = "N"
        '    IntId = 0
        'End If

    End Sub
#End Region
#Region "Private Functions"
    Private Sub AddOnChangeEventToControls(ByVal formName As String)
        Try
            Dim clsForms As New ClsSys_Forms(Page)
            clsForms.Find(" code = REPLACE('" & formName & "',' ','')")
            Dim clsFormsControls As New Clssys_FormsControls(Page)
            If clsForms.ID > 0 Then
                clsFormsControls.Find(" FormID=" & clsForms.ID)
                Dim tab As Data.DataTable = clsFormsControls.DataSet.Tables(0).Copy()
                For Each row As Data.DataRow In tab.Rows
                    Dim currCtrl As Control = Me.FindControl(row("Name"))
                    'If TypeOf (currCtrl) Is TextBox Then
                    'End If
                Next
            End If
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Public Shared Function GetNDate_Shared(ByVal date1 As Object, ByVal dteDefault As Date) As Date
        If IsDBNull(date1) Then
            Return dteDefault
        Else
            Return CDate(date1)
        End If
    End Function

    Public Sub fillDDl(fillTableOnly As Boolean)
        Dim DS1 As New Data.DataSet()
        DS1.Clear()
        For x As Integer = 0 To DS1.Tables.Count - 1
            DS1.Tables(x).Constraints.Clear()
        Next
        DS1.Relations.Clear()
        DS1.Tables.Clear()
        Dim connetionString As String
        Dim connection As Data.SqlClient.SqlConnection
        Dim command As Data.SqlClient.SqlCommand
        Dim adapter As New Data.SqlClient.SqlDataAdapter
        ClsEmployees = New Clshrs_Employees(Page)
        connetionString = ClsEmployees.ConnectionString
        connection = New Data.SqlClient.SqlConnection(connetionString)

        Dim str1 As String = ""

        str1 = "SELECT   DecisionField, DecisionAraName, DecisionEngName FROM hrs_employeesDecisionsFields "


        command = New Data.SqlClient.SqlCommand(str1, connection)
        adapter.SelectCommand = command
        adapter.Fill(DS1, "Table1")

        connection.Close()

        dtDecisions = DS1.Tables(0)
        If fillTableOnly Then
            Return
        End If
        ddlDecisions.DataSource = Nothing
        ddlDecisions.DataBind()

        Dim Display As String
        If ProfileCls.CurrentLanguage = "Ar" Then
            Display = "DecisionAraName"
        Else
            Display = "DecisionEngName"
        End If

        ddlDecisions.DataTextField = Display
        ddlDecisions.DataValueField = "DecisionField"
        ddlDecisions.DataSource = DS1

        ddlDecisions.DataSourceID = String.Empty
        ddlDecisions.DataBind()
    End Sub

    Public Sub GetAllEmployeeRequests(ByVal filter As String)
        Try
            fillDDl(True)
            Dim EmpName As String
            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " [dbo].[fn_GetEmpName](hrs_employeesDecisions.Code,1) "
            Else
                EmpName = "[dbo].[fn_GetEmpName](hrs_employeesDecisions.Code,0)"
            End If
            Dim sqlStr As String = ""
            If ddlDecisions.SelectedValue = "" Then

                For decision = 1 To dtDecisions.Rows.Count - 1
                    Dim colName As String
                    Dim current As String = "hrs_EmployeesDecisions." & dtDecisions.Rows(decision).Item(0)
                    Dim previous As String = "Previous" & dtDecisions.Rows(decision).Item(0)
                    Dim change As String = " and hrs_EmployeesDecisions." & dtDecisions.Rows(decision).Item(0) & " <> '' "
                    Dim regUser As String
                    Dim colId As String = dtDecisions.Rows(decision).Item(0)
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        colName = dtDecisions.Rows(decision).Item(1)
                        regUser = "sys_Users.ArbName"
                    Else
                        colName = dtDecisions.Rows(decision).Item(2)
                        regUser = "sys_Users.EngName"
                    End If

                    Dim name As String = ""
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        name = "ArbName"
                    Else
                        name = "EngName"
                    End If
                    Dim mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_MaritalStatus where ID=" & current
                    Dim PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_MaritalStatus where ID=" & previous
                    If colId <> "MaritalStatusID" Then
                        mSelectCommand = ""
                        PrevSelectCommand = ""
                    End If
                    If colId = "BankID" Then
                        If ProfileCls.CurrentLanguage = "Ar" Then
                            name = "ArbName"
                        Else
                            name = "EngName"
                        End If
                        mSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Banks where ID=" & current
                        PrevSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Banks where ID=" & previous
                    End If

                    If colId = "NationalityID" Then
                        If ProfileCls.CurrentLanguage = "Ar" Then
                            name = "ArbName"
                        Else
                            name = "EngName"
                        End If
                        mSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Nationalities where ID=" & current
                        PrevSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Nationalities where ID=" & previous
                    End If

                    If colId = "DepartmentID" Then
                        If ProfileCls.CurrentLanguage = "Ar" Then
                            name = "ArbName"
                        Else
                            name = "EngName"
                        End If
                        mSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Departments where ID=" & current
                        PrevSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Departments where ID=" & previous
                    End If

                    If colId = "BranchID" Then
                        If ProfileCls.CurrentLanguage = "Ar" Then
                            name = "ArbName"
                        Else
                            name = "EngName"
                        End If
                        mSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Branches where ID=" & current
                        PrevSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Branches where ID=" & previous
                    End If

                    If colId = "SponsorID" Then
                        If ProfileCls.CurrentLanguage = "Ar" Then
                            name = "ArbName"
                        Else
                            name = "EngName"
                        End If
                        mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Sponsors where ID=" & current
                        PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Sponsors where ID=" & previous
                    End If

                    If colId = "ManagerID" Then
                        If ProfileCls.CurrentLanguage = "Ar" Then
                            name = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "
                        Else
                            name = "[dbo].[fn_GetEmpName](hrs_Employees.Code,0)"
                        End If
                        mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Employees where ID=" & current
                        PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Employees where ID=" & previous
                    End If

                    If colId = "SectorID" Then
                        If ProfileCls.CurrentLanguage = "Ar" Then
                            name = "ArbName"
                        Else
                            name = "EngName"
                        End If
                        mSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Sectors where ID=" & current
                        PrevSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Sectors where ID=" & previous
                    End If

                    If colId = "LocationID" Then
                        If ProfileCls.CurrentLanguage = "Ar" Then
                            name = "ArbName"
                        Else
                            name = "EngName"
                        End If
                        mSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Locations where ID=" & current
                        PrevSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Locations where ID=" & previous
                    End If

                    If colId = "ContractType" Then
                        If ProfileCls.CurrentLanguage = "Ar" Then
                            name = "ArbName"
                        Else
                            name = "EngName"
                        End If
                        mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_ContractsTypes where ID=" & current
                        PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_ContractsTypes where ID=" & previous
                    End If

                    If colId = "Professions" Then
                        If ProfileCls.CurrentLanguage = "Ar" Then
                            name = "ArbName"
                        Else
                            name = "EngName"
                        End If
                        mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Professions where ID=" & current
                        PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Professions where ID=" & previous
                    End If

                    If colId = "Position" Then
                        If ProfileCls.CurrentLanguage = "Ar" Then
                            name = "ArbName"
                        Else
                            name = "EngName"
                        End If
                        mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Positions where ID=" & current
                        PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Positions where ID=" & previous
                    End If
                    If colId = "EmployeeClass" Then
                        If ProfileCls.CurrentLanguage = "Ar" Then
                            name = "ArbName"
                        Else
                            name = "EngName"
                        End If
                        mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_EmployeesClasses where ID=" & current
                        PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_EmployeesClasses where ID=" & previous
                    End If

                    If colId = "LastEducations" Then
                        If ProfileCls.CurrentLanguage = "Ar" Then
                            name = "ArbName"
                        Else
                            name = "EngName"
                        End If
                        mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Educations where ID=" & current
                        PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Educations where ID=" & previous
                    End If

                    If mSelectCommand <> "" Then
                        current = " ( " & mSelectCommand & ")"
                    End If
                    If PrevSelectCommand <> "" Then
                        previous = " ( " & PrevSelectCommand & ")"
                    End If

                    sqlStr += " Select " & EmpName & " as EmpName,'" & colId & "' as DecisionId, '" & colName & "' as DecisionName ,CONVERT(nvarchar(50)," & current & ") as CurrentDecision ,CONVERT(nvarchar(50)," & previous & ") as PreviousDecision,hrs_EmployeesDecisions.RegDate," & regUser & " as RegUser FROM hrs_EmployeesDecisions LEFT JOIN sys_Users ON hrs_EmployeesDecisions.RegUserID = sys_Users.ID where 1=1 " + change + filter
                    If decision < dtDecisions.Rows.Count - 1 Then
                        sqlStr += " union all "
                    End If
                Next
            Else
                Dim regUser As String
                If ProfileCls.CurrentLanguage = "Ar" Then
                    regUser = "sys_Users.ArbName"
                Else
                    regUser = "sys_Users.EngName"
                End If
                Dim colName As String = ddlDecisions.SelectedItem.Text
                Dim colId As String = ddlDecisions.SelectedItem.Value
                Dim current As String = "hrs_EmployeesDecisions." & ddlDecisions.SelectedValue
                Dim previous As String = "Previous" & ddlDecisions.SelectedValue


                Dim name As String = ""
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                Dim mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_MaritalStatus where ID=" & current
                Dim PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_MaritalStatus where ID=" & previous
                If colId <> "MaritalStatusID" Then
                    mSelectCommand = ""
                    PrevSelectCommand = ""
                End If
                If colId = "BankID" Then
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        name = "ArbName"
                    Else
                        name = "EngName"
                    End If
                    mSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Banks where ID=" & current
                    PrevSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Banks where ID=" & previous
                End If

                If colId = "NationalityID" Then
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        name = "ArbName"
                    Else
                        name = "EngName"
                    End If
                    mSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Nationalities where ID=" & current
                    PrevSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Nationalities where ID=" & previous
                End If

                If colId = "DepartmentID" Then
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        name = "ArbName"
                    Else
                        name = "EngName"
                    End If
                    mSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Departments where ID=" & current
                    PrevSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Departments where ID=" & previous
                End If

                If colId = "BranchID" Then
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        name = "ArbName"
                    Else
                        name = "EngName"
                    End If
                    mSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Branches where ID=" & current
                    PrevSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Branches where ID=" & previous
                End If

                If colId = "SponsorID" Then
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        name = "ArbName"
                    Else
                        name = "EngName"
                    End If
                    mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Sponsors where ID=" & current
                    PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Sponsors where ID=" & previous
                End If

                If colId = "ManagerID" Then
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        name = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "
                    Else
                        name = "[dbo].[fn_GetEmpName](hrs_Employees.Code,0)"
                    End If
                    mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Employees where ID=" & current
                    PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Employees where ID=" & previous
                End If

                If colId = "SectorID" Then
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        name = "ArbName"
                    Else
                        name = "EngName"
                    End If
                    mSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Sectors where ID=" & current
                    PrevSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Sectors where ID=" & previous
                End If

                If colId = "LocationID" Then
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        name = "ArbName"
                    Else
                        name = "EngName"
                    End If
                    mSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Locations where ID=" & current
                    PrevSelectCommand = " Select top 1 " & name & " as DecisionName From sys_Locations where ID=" & previous
                End If

                If colId = "ContractType" Then
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        name = "ArbName"
                    Else
                        name = "EngName"
                    End If
                    mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_ContractsTypes where ID=" & current
                    PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_ContractsTypes where ID=" & previous
                End If

                If colId = "Professions" Then
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        name = "ArbName"
                    Else
                        name = "EngName"
                    End If
                    mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Professions where ID=" & current
                    PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Professions where ID=" & previous
                End If

                If colId = "Position" Then
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        name = "ArbName"
                    Else
                        name = "EngName"
                    End If
                    mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Positions where ID=" & current
                    PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Positions where ID=" & previous
                End If
                If colId = "EmployeeClass" Then
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        name = "ArbName"
                    Else
                        name = "EngName"
                    End If
                    mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_EmployeesClasses where ID=" & current
                    PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_EmployeesClasses where ID=" & previous
                End If

                If colId = "LastEducations" Then
                    If ProfileCls.CurrentLanguage = "Ar" Then
                        name = "ArbName"
                    Else
                        name = "EngName"
                    End If
                    mSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Educations where ID=" & current
                    PrevSelectCommand = " Select top 1 " & name & " as DecisionName From hrs_Educations where ID=" & previous
                End If

                If mSelectCommand <> "" Then
                    current = " ( " & mSelectCommand & ")"
                End If
                If PrevSelectCommand <> "" Then
                    previous = " ( " & PrevSelectCommand & ")"
                End If

                sqlStr += " Select " & EmpName & " as EmpName,'" & colId & "' as DecisionId,'" & colName & "' as DecisionName ," & current & " as CurrentDecision ," & previous & " as PreviousDecision,hrs_EmployeesDecisions.RegDate," & regUser & " as RegUser FROM hrs_EmployeesDecisions LEFT JOIN sys_Users ON hrs_EmployeesDecisions.RegUserID = sys_Users.ID where 1=1 " + filter
            End If
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_NewEmployee(Me)
            'ClsEmployees.Find("Code=" & _sys_User.Code & "")
            Dim DS1 As New Data.DataSet()
            DS1.Clear()
            For x As Integer = 0 To DS1.Tables.Count - 1
                DS1.Tables(x).Constraints.Clear()
            Next
            DS1.Relations.Clear()
            DS1.Tables.Clear()
            Dim connetionString As String
            Dim connection As Data.SqlClient.SqlConnection
            Dim command As Data.SqlClient.SqlCommand
            Dim adapter As New Data.SqlClient.SqlDataAdapter
            connetionString = ClsEmployees.ConnectionString
            connection = New Data.SqlClient.SqlConnection(connetionString)
            'Dim Display As String
            'If ProfileCls.CurrentLanguage = "Ar" Then
            '    Display = "DecisionAraName"
            'Else
            '    Display = "DecisionEngName"
            'End If

            'Dim str1 As String = ""

            'str1 = "SELECT [ID]," & EmpName & " as EmployeeName,[RegisterDate] as JoinDate,[PersonalEmail] as E_Mail,[Mobile] FROM [dbo].[hrs_NewEmployee] where 1=1 " + filter

            'sqlStr = sqlStr.Replace("CONVERT(nvarchar(50),Remarks)", "CONVERT(nvarchar(50),hrs_employeesDecisions.Remarks)")
            command = New Data.SqlClient.SqlCommand(sqlStr, connection)
            adapter.SelectCommand = command
            adapter.Fill(DS1, "Table1")

            connection.Close()

            Dim tab As Data.DataTable = DS1.Tables(0).Copy()

            uwgEmployeeVacations.DataSource = Nothing
            uwgEmployeeVacations.DataBind()

            uwgEmployeeVacations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
            uwgEmployeeVacations.DataSource = DS1
            uwgEmployeeVacations.DataBind()



        Catch ex As Exception

        End Try
    End Sub





    Public Function GetRecordInfoAjax(ByVal recordID As Integer) As Boolean
        Try
            Dim dsContractsTransactions As New DataSet
            Dim dsUser As New DataSet
            Dim clsUser As New Clssys_Users(Page)
            Dim clsContr As New Clshrs_ContractsTransactions(Page)
            clsContr.Find("ID=" & recordID)
            If clsContr.ID > 0 Then
                If Not clsContr.RegUserID = Nothing Then
                    clsUser.Find("ID=" & clsContr.RegUserID)

                End If


            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Shared Function"
    Public Shared Function Find(ByVal Table As String, ByVal Filter As String, ByRef DataSet As DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Dim mSelectCommand = " Select * From " & Table
        Dim mSqlDataAdapter As New SqlClient.SqlDataAdapter
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        Try
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



    <System.Web.Services.WebMethod()>
    Public Shared Function RetTable(ByVal strTableName As String) As Object
        Dim tbl As New Data.DataTable(strTableName)
        tbl.Columns.Add(New Data.DataColumn("EmpID", GetType(Integer)))
        tbl.Columns.Add(New Data.DataColumn("EmpName", GetType(String)))
        For i As Int16 = 1 To 4
            Dim nRow As Data.DataRow = tbl.NewRow()
            nRow(0) = i
            nRow(1) = "Employee " + i.ToString()
            tbl.Rows.Add(nRow)
        Next
        Return tbl
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function Get_Searched_Description(ByVal IntSearchId As Integer, ByVal strCode As String) As String
        Dim dsSearchs As New Data.DataSet
        Find("sys_Searchs", " sys_Searchs.Id = " & IntSearchId, dsSearchs)
        Dim dsObjects As New Data.DataSet
        Find("sys_Objects", " sys_Objects.Id = " & dsSearchs.Tables(0).Rows(0).Item("ObjectID"), dsObjects)
        Return GetFieldDescription(strCode, dsObjects.Tables(0).Rows(0).Item("Code"))
    End Function
#End Region
    'Protected Sub WebDateChooser3_ValueChanged(sender As Object, e As Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs) Handles WebDateChooser3.ValueChanged
    '    If lbVactionID.Text = "" Then
    '        txtVactiondays.Text = CDate(WebDateChooser3.Value).Subtract(CDate(WebDateChooser1.Value)).Days
    '        txtVactiondays_ValueChange(Nothing, Nothing)
    '    End If
    'End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim RequestSerial As Integer

        'RequestSerial = uwgEmployeeVacations.DisplayLayout.SelectedRows.Item(0).GetCellValue()



        'Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPostJournalPreview.aspx?IDs=" & StrIDArray & "", 1500, 1200, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "wWindO", False, True, False, False, False, False, False, False, False)
    End Sub

    Private Sub LinkButton_Remarks_Load(sender As Object, e As EventArgs) Handles LinkButton_Remarks.Load

    End Sub
    Protected Sub ImageButton_Refresh_Click(sender As Object, e As System.EventArgs) Handles ImageButton_Refresh.Click
        Dim filter As String
        filter = " and hrs_EmployeesDecisions.RegDate>='" & Convert.ToDateTime(ddlFromDate.Value.ToString()).ToString("yyyy-MM-dd") & "' and cast( hrs_EmployeesDecisions.RegDate as date)<='" & Convert.ToDateTime(ddlToDate.Value).ToString("yyyy-MM-dd") & "'"
        If txtEmployee.Text <> "" Then
            filter += " and hrs_employeesDecisions.Code='" & txtEmployee.Text & "'"
        End If
        If ddlDecisions.SelectedValue <> "" Then
            filter += " and hrs_employeesDecisions." & ddlDecisions.SelectedValue & " <> ''"
        End If
        GetAllEmployeeRequests(filter)
    End Sub

End Class
