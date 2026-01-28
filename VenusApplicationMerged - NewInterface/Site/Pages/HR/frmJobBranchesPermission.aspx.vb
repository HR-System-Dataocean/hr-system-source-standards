Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmJobBranchesPermission
    Inherits MainPage
#Region "Public Decleration"
    Private ClsDelegationSChedule As ClsSS_DelegationSChedule
    Private ClsSCheduleRequests As ClsSS_DelegationSCheduleRequests

    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Private ClsEmployees As Clshrs_Employees
    Private ClsPositions As Clshrs_Positions

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")

        ClsDelegationSChedule = New ClsSS_DelegationSChedule(Me.Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)

            If ClsObjects.Find(" Code='hrs_JobBranchesPermission'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID

                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

            If ClsObjects.Find(" Code='hrs_Positions'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID

                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtPositionCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & TxtPositionCode.ClientID & "'"
                    WebImageButton5.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                End If
            End If
            TxtPositionName.Enabled = False
            txtDelegatedName.Enabled = False

            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                txtCode.Focus()


                Page.Session.Add("ConnectionString", ClsDelegationSChedule.ConnectionString)
                ClsDelegationSChedule.AddOnChangeEventToControls("frmJobBranchesPermission", Page, UltraWebTab1)
                GetData(0)
                'AddNewRow()


                'Dim 
                Dim IntDimension As Integer = 510
                Dim UrlString = ""
                If ClsObjects.Find(" Code='hrs_JobBranchesPermission'") Then
                    If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                        SearchID = ClsSearchs.ID
                        IntDimension = 510

                        UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                        btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                    End If
                End If
                '================================= Exit & Navigation Notification [ End ]

                'Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Dim clsEmployees = New Clshrs_Employees(Page)
                Dim MaxAppIDStr As String = "SELECT ISNULL(MAX(Code), 0) + 1 FROM hrs_JobBranchesPermission"
                Dim MaxAppointCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, MaxAppIDStr)

                ' Ensure it's an integer and assign to txtCode.Text
                txtCode.Text = If(IsDBNull(MaxAppointCode), "1", MaxAppointCode.ToString())
                ' Store the ClientID and UniqueID in hidden fields

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
                If TxtPositionCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Position Code /برجاء إدخال الوظيفة"))
                    Exit Sub
                End If
                If txtDelegated.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Employee Code /برجاء إدخال الموظف"))
                    Exit Sub
                End If
                Dim clsEmployees As New Clshrs_Employees(Page)
                clsEmployees.Find("Code='" & txtDelegated.Text & "'")

                Dim ClsPosition As New ClsBasicFiles(Me.Page, "hrs_Positions")
                ClsPosition.Find("Code = " & TxtPositionCode.Text & "")

                Dim existSql = "select Code from hrs_JobBranchesPermission where PositionID=" & ClsPosition.ID & " and EmployeeId=" & clsEmployees.ID & " and Code <>" & txtCode.Text
                Dim existEmp As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, existSql)

                If Not IsDBNull(existEmp) And existEmp <> Nothing Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " The employee with the job is registered by code /الموظف مع الوظيفة مسجل من قبل بكود رقم " & existEmp))
                    Exit Sub
                End If

                SavePart()
                Clear()
                txtCode_TextChanged(Nothing, Nothing)
                GetData(0)
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If TxtPositionCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Position Code /برجاء إدخال الوظيفة"))
                    Exit Sub
                End If
                If txtDelegated.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Employee Code /برجاء إدخال الموظف"))
                    Exit Sub
                End If
                Dim clsEmployees As New Clshrs_Employees(Page)
                clsEmployees.Find("Code='" & txtDelegated.Text & "'")

                Dim ClsPosition As New ClsBasicFiles(Me.Page, "hrs_Positions")
                ClsPosition.Find("Code = " & TxtPositionCode.Text & "")

                Dim existSql = "select Code from hrs_JobBranchesPermission where PositionID=" & ClsPosition.ID & " and EmployeeId=" & clsEmployees.ID & " and Code <>" & txtCode.Text
                Dim existEmp As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, existSql)

                If Not IsDBNull(existEmp) And existEmp <> Nothing Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " The employee with the job is registered by code /الموظف مع الوظيفة مسجل من قبل بكود رقم " & existEmp))
                    Exit Sub
                End If

                If SavePart() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                End If
            Case "New"


                'AfterOperation()
                Dim clsEmployees As New Clshrs_Employees(Page)
                Dim MaxAppIDStr As String = "SELECT ISNULL(MAX(Code), 0) + 1 FROM hrs_JobBranchesPermission"
                Dim MaxAppointCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, MaxAppIDStr)

                ' Ensure it's an integer and assign to txtCode.Text
                txtCode.Text = If(IsDBNull(MaxAppointCode), "1", MaxAppointCode.ToString())
                txtCode_TextChanged(Nothing, Nothing)
                GetData(0)
            Case "Delete"
                Dim clsEmployees As New Clshrs_Employees(Page)
                Dim currentIdQuery As String = "Delete hrs_JobBranchesPermissionDetails FROM     hrs_JobBranchesPermissionDetails INNER JOIN  hrs_JobBranchesPermission ON hrs_JobBranchesPermissionDetails.JobBranchesPermissionId =  hrs_JobBranchesPermission.ID WHERE  hrs_JobBranchesPermission.Code =" & txtCode.Text
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployees.ConnectionString, Data.CommandType.Text, currentIdQuery)

                currentIdQuery = "DELETE FROM [dbo].[hrs_JobBranchesPermission] where Code=" & txtCode.Text
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployees.ConnectionString, Data.CommandType.Text, currentIdQuery)

                'Dim clsEmployees As New Clshrs_Employees(Page)
                Dim MaxAppIDStr As String = "SELECT ISNULL(MAX(Code), 0) + 1 FROM hrs_JobBranchesPermission"
                Dim MaxAppointCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, MaxAppIDStr)

                ' Ensure it's an integer and assign to txtCode.Text
                txtCode.Text = If(IsDBNull(MaxAppointCode), "1", MaxAppointCode.ToString())
                txtCode_TextChanged(Nothing, Nothing)
                GetData(0)
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
                Dim StrSelectCommand = "SELECT TOP 1 Code FROM hrs_JobBranchesPermission ORDER BY Code ASC"
                Dim FirstCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, StrSelectCommand)
                txtCode.Text = FirstCode
                txtCode_TextChanged(Nothing, Nothing)
            Case "Previous"
                Dim clsEmployees As New Clshrs_Employees(Page)
                Dim StrSelectCommand = "SELECT TOP 1 Code FROM hrs_JobBranchesPermission where Code<" & txtCode.Text & " ORDER BY Code Desc"
                Dim FirstCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, StrSelectCommand)
                If FirstCode = Nothing Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                Else
                    txtCode.Text = FirstCode
                    txtCode_TextChanged(Nothing, Nothing)
                End If

            Case "Next"
                Dim clsEmployees As New Clshrs_Employees(Page)
                Dim StrSelectCommand = "SELECT TOP 1 Code FROM hrs_JobBranchesPermission where Code>" & txtCode.Text & " ORDER BY Code ASC"
                Dim FirstCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, StrSelectCommand)
                If FirstCode = Nothing Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                Else
                    txtCode.Text = FirstCode
                    txtCode_TextChanged(Nothing, Nothing)
                End If

            Case "Last"
                Dim clsEmployees As New Clshrs_Employees(Page)
                Dim StrSelectCommand = "SELECT TOP 1 Code FROM hrs_JobBranchesPermission  ORDER BY Code Desc"
                Dim FirstCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, StrSelectCommand)

                txtCode.Text = FirstCode
                txtCode_TextChanged(Nothing, Nothing)
        End Select
    End Sub
    Protected Sub TxtPositionCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPositionCode.TextChanged
        Dim cond = ""
        If Not String.IsNullOrEmpty(TxtPositionCode.Text) Then
            Dim str As String
            Dim PositionCode As String = TxtPositionCode.Text
            Dim PositionName As String
            Dim ClsPosition As New ClsBasicFiles(Me.Page, "hrs_Positions")
            ClsPosition.Find("Code = " & PositionCode & "")
            If ClsPosition.ID > 0 Then
                TxtPositionID.Value = ClsPosition.ID
                If ProfileCls.CurrentLanguage = "Ar" Then
                    PositionName = ClsPosition.ArbName
                Else
                    PositionName = ClsPosition.EngName
                End If

                TxtPositionName.Text = PositionName

                cond = " and Id in (SELECT [EmployeeID] FROM [dbo].[hrs_Contracts] where PositionID=" & ClsPosition.ID & ")"
            End If


        Else

            TxtPositionName.Text = String.Empty
            TxtPositionID.Value = 0
        End If
        If TxtPositionID.Value > 0 Then

            Dim PositionName As String
            Dim ClsPosition As New ClsBasicFiles(Me.Page, "hrs_Positions")
            ClsPosition.Find("ID = " & TxtPositionID.Value & "")
            TxtPositionCode.Text = ClsPosition.Code
            If ProfileCls.CurrentLanguage = "Ar" Then
                PositionName = ClsPosition.ArbName
            Else
                PositionName = ClsPosition.EngName
            End If

            TxtPositionName.Text = PositionName

        Else

            TxtPositionName.Text = String.Empty

        End If

        Dim clsEmployees As New Clshrs_Employees(Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0

        ClsObjects.Find(" Code='" & clsEmployees.Table.Trim & "'")
        ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
        Dim csSearchID As Integer
        csSearchID = ClsSearchs.ID
        Dim IntDimension As Integer = 510
        Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtDelegated.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtDelegated.ClientID & "'"
        If cond <> "" Then
            UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtDelegated.ID & "&SearchID=" & csSearchID & "&Cond=" & cond & "&'," & IntDimension & ",720,false,'" & txtDelegated.ClientID & "'"
        End If
        btnDelegatedSearch.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"


    End Sub

    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()

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
    Private Function GetData(ScheduleId As Integer) As Boolean
        Try
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDelegationSChedule.ConnectionString)
            Dim requestName As String
            If ProfileCls.CurrentLanguage = "Ar" Then
                requestName = " sys_Branches.ArbName "
            Else
                requestName = " sys_Branches.EngName "
            End If
            Dim str As String = " select sys_Branches.ID as BranchId,sys_Branches.Code," & requestName & " as BranchName,case when hrs_JobBranchesPermissionDetails.Id is null then 0 else 1 end as 'Select' from sys_Branches left outer join hrs_JobBranchesPermissionDetails on hrs_JobBranchesPermissionDetails.BranchId=sys_Branches.Id and hrs_JobBranchesPermissionDetails.JobBranchesPermissionId=" & ScheduleId & " Where hrs_JobBranchesPermissionDetails.JobBranchesPermissionId is null or hrs_JobBranchesPermissionDetails.JobBranchesPermissionId = " & ScheduleId

            Dim ds As New Data.DataSet
            ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsDelegationSChedule.ConnectionString, Data.CommandType.Text, str)

            UwgSearchEmployees.DataSource = Nothing
            UwgSearchEmployees.DataBind()
            UwgSearchEmployees.DataSource = ds.Tables(0)
            UwgSearchEmployees.DataBind()

            'UwgSearchEmployees.Rows.Add()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SaveGrid(ScheduleId As Integer) As Boolean
        Try
            Dim ds As Data.DataSet
            Dim str As String = String.Empty
            Dim str1 As String = String.Empty
            Dim clsEmployee = New Clshrs_Employees(Me)

            'str = "set dateformat DMY; UPDATE hrs_HIPolicyContract SET CancelDate = GetDate() Where [PolicyID]=" & HIPolicyID & ";" & vbNewLine
            'Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsDelegationSChedule.ConnectionString, Data.CommandType.Text, str)

            If ScheduleId > 0 Then
                str = "set dateformat DMY; "
                str &=
                                    " DELETE FROM [dbo].[hrs_JobBranchesPermissionDetails] where " &
                                    " JobBranchesPermissionId = " & ScheduleId & " ;"

                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployee.ConnectionString, Data.CommandType.Text, str)

            End If

            ds = GetExistRecord()

            If ds.Tables(0).Rows.Count > 0 Then
                For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows

                    If row.Cells.FromKey("Select").Value <> Nothing Then
                        clsEmployee = New Clshrs_Employees(Me)

                        str = "set dateformat DMY; "
                        str &= " INSERT INTO [dbo].[hrs_JobBranchesPermissionDetails] ([JobBranchesPermissionId],[BranchId]) VALUES " &
                                    "(" & ds.Tables(0).Rows(0).Item(0) &
                                    ",'" & row.Cells.FromKey("BranchId").Value &
                                    "');" & vbNewLine

                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployee.ConnectionString, Data.CommandType.Text, str)


                    End If



                Next
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim ClsEmployee = New Clshrs_Employees(Page)
        ClsEmployee.Find(" Code='" & txtDelegated.Text & "'")
        Try
            Dim ds As Data.DataSet = GetExistRecord()

            If ds.Tables(0).Rows.Count > 0 Then

                Dim updateSql = "UPDATE hrs_JobBranchesPermission SET [Code] = " & txtCode.Text & ",[PositionID] = " & TxtPositionID.Value & ",[EmployeeId] = " & ClsEmployee.ID & " WHERE ID=" & ds.Tables(0).Rows(0).Item("ID") & ""
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, updateSql)

                SaveGrid(Convert.ToInt32(ds.Tables(0).Rows(0).Item("ID")))
            Else
                Dim InsertSql = "INSERT INTO hrs_JobBranchesPermission(Code,PositionID,EmployeeId) VALUES (" & txtCode.Text & "," & TxtPositionID.Value & "," & ClsEmployee.ID & ")"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, InsertSql)

                SaveGrid(0)
            End If



            value.Text = ""
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
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
                TxtPositionID.Value = ds.Tables(0).Rows(0).Item("PositionID")
                Dim ClsPosition As New ClsBasicFiles(Me.Page, "hrs_Positions")
                ClsPosition.Find("ID = " & TxtPositionID.Value & "")
                If ClsPosition.ID > 0 Then
                    TxtPositionCode.Text = ClsPosition.Code
                    TxtPositionCode_TextChanged(Nothing, Nothing)
                End If
                Dim empId = ds.Tables(0).Rows(0).Item("EmployeeId")
                    ClsEmployee.Find(" Id=" & empId)
                    If ClsEmployee.Code > 0 Then
                    txtDelegated.Text = ClsEmployee.Code
                    txtDelegated_TextChanged(Nothing, Nothing)
                End If

                    Dim perId = ds.Tables(0).Rows(0).Item("ID")
                    GetData(perId)

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
        Dim sql = "SELECT [ID],[PositionID],[EmployeeId] FROM [dbo].[hrs_JobBranchesPermission] where Code='" & txtCode.Text & "'"
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
        TxtPositionCode.Text = String.Empty
        TxtPositionName.Text = String.Empty
        TxtPositionID.Value = String.Empty

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim User As String = String.Empty
        Dim ClsEmployees = New Clshrs_Employees(Page)
        txtDelegated_TextChanged(Nothing, Nothing)
        UwgSearchEmployees.Rows.Clear()
        Dim MaxAppIDStr As String = "SELECT ISNULL(MAX(Code), 0) + 1 FROM hrs_JobBranchesPermission"
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

    Protected Sub chkCheckAll_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkCheckAll.CheckedChanged
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows()
            row.Cells.FromKey("Select").Value = chkCheckAll.Checked
        Next
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
                If TxtPositionID.Value > 0 Then
                    cond = " and Id in (SELECT [EmployeeID] FROM [dbo].[hrs_Contracts] where PositionID=" & TxtPositionID.Value & ")"
                End If
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
