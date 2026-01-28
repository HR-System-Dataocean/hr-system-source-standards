Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Shared.Web
Imports OfficeWebUI
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports Infragistics.WebUI.UltraWebGrid
Imports Resources
Partial Class frmSalaryEmployeeDistribution
    Inherits MainPage
#Region "Public Decleration"
    Private ClsAttendAppointmentMembers As Clshrs_SalaryDistPlanMember
    Private ClsAttendAppointment As Clshrs_SalaryDistPlan
    Private ClsLocations As Clssys_Locations
    Private ClsEmployees As Clshrs_Employees
    Private ClsWorkPlan As ClsAtt_AttendWorkPlan
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private ClsFiscalPeriod As Clssys_FiscalYearsPeriods


#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEmployees = New Clshrs_Employees(Me.Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID

                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
                ClsEmployees.AddOnChangeEventToControls("frmSalaryEmployeeDistribution", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]

                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtEngName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ENGLISH)


            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsEmployees.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsEmployees.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployees.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsEmployees = New Clshrs_Employees(Me.Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim ClsAttendAppointmentMembers As New Clshrs_SalaryDistPlanMember(Page)
        Select Case e.CommandArgument
            Case "SaveNew"

            Case "Save"

            Case "New"

            Case "Delete"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                ClsAttendAppointmentMembers.Delete("SalaryDistPlanID=" & lbAppointmentID.Text & "and Employeeid=" & ClsEmployees.ID)
                GetValues(ClsEmployees)
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "  Delete is done/تم الحذف"))
            Case "Property"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsEmployees.ID & "&TableName=" & ClsEmployees.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsEmployees.ID & "&TableName=" & ClsEmployees.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"

            Case "Exit"

            Case "First"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                ClsEmployees.FirstRecord()
                GetValues(ClsEmployees)
            Case "Previous"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                If Not ClsEmployees.previousRecord() Then
                    ClsEmployees.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))

                End If
                GetValues(ClsEmployees)
            Case "Next"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                If Not ClsEmployees.NextRecord() Then
                    ClsEmployees.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))

                End If
                GetValues(ClsEmployees)
            Case "Last"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                ClsEmployees.LastRecord()
                GetValues(ClsEmployees)

        End Select
    End Sub




    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty

        uwgWorkPlans.Clear()
        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function




    Private Function GetValues(ByVal ClsEmployees As Clshrs_Employees) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim clsDAL As New ClsDataAcessLayer(Page)
        Dim ClsAttendAppointmentMembers As New Clshrs_SalaryDistPlanMember(Page)
        Dim ClsAttendAppointment As New Clshrs_SalaryDistPlan(Page)
        Dim ClsAttendWorkPlan As New ClsAtt_AttendWorkPlan(Page)
        Dim tbAttendWorkPlan As New DataTable
        Dim tbShiftDays As New DataTable
        Dim dtCopy As New DataTable
        Dim dtCopy2 As New DataTable
        Dim DStemp As New DataSet
        Dim DS1 As New DataSet
        Dim ClsFiscalPeriod As New Clssys_FiscalYearsPeriods(Page)
        Dim dsCurrAttendWorkPlan As New Data.DataSet
        Dim param As SqlParameter
        param = New SqlParameter("@employeeID", SqlDbType.Int)
        param.Value = ClsEmployees.ID

        Dim param2(2) As SqlParameter
        param2(0) = New SqlParameter("@employeeID", SqlDbType.Int)
        param2(0).Value = ClsEmployees.ID
        param2(1) = New SqlParameter("@DayDate", SqlDbType.Date)
        param2(1).Value = DateTime.Now.Date

        'dsCurrAttendWorkPlan = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsDAL.ConnectionString, Data.CommandType.StoredProcedure, "dbo.shift_FindCurrAttendWorkPlan", param2)
        'If (dsCurrAttendWorkPlan.Tables(0).Rows.Count > 0) Then
        '    ClsAttendWorkPlan.GetDropDownList(ddlCurrWorkPlan, False, "ID =" & Convert.ToInt16(dsCurrAttendWorkPlan.Tables(0).Rows(0).Item("ID").ToString()))
        'End If
        DS1 = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsDAL.ConnectionString, Data.CommandType.StoredProcedure, "dbo.FindAllSalaryDist", param)
        uwgWorkPlans.DataSource = DS1
        uwgWorkPlans.DataBind()

        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsEmployees
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
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
                SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                SetToolBarRecordPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, .Table, .ID)
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                End If
                If Page.IsPostBack Then
                    CreateOtherFields(ClsEmployees.ID)
                End If
            End With
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Try
            ClsEmployees.Find("Code='" & txtCode.Text & "'")
            IntId = ClsEmployees.ID
            txtEngName.Focus()
            If ClsEmployees.ID > 0 Then
                GetValues(ClsEmployees)
                StrMode = "E"
            Else
                If ClsEmployees.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If

                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsEmployees.ConnectionString, ClsEmployees.DataBaseUserRelatedID, ClsEmployees.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEmployees.ConnectionString, ClsEmployees.DataBaseUserRelatedID, ClsEmployees.GroupID, ClsEmployees.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                ImageButton_Delete.Enabled = False
            End If
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

                        Case "E"
                            ImageButton_Save.Enabled = .Item("AllowEdit")

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
                    ImageButton_Delete.Enabled = False
                    ImageButton_Properties.Visible = False
                    LinkButton_Properties.Visible = False
                    ImageButton_Remarks.Visible = False
                    LinkButton_Remarks.Visible = False
                Case "D"
                    ClsEmployees.Find("ID=" & intID)
                    GetValues(ClsEmployees)

                    ImageButton_Save.Visible = False
                Case "E"
                    ClsEmployees.Find("ID=" & intID)
                    GetValues(ClsEmployees)

                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsEmployees
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

    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsEmployees = New Clshrs_Employees(Me.Page)
        If IntId > 0 Then
            ClsEmployees.Find("ID=" & IntId)
            GetValues(ClsEmployees)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function


    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEmployees.Table) = True Then
            Dim StrTablename As String
            ClsEmployees = New Clshrs_Employees(Me)
            StrTablename = ClsEmployees.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID & _
                                                                                                                                                                                                                                                                                                                                " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID & _
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
    Private Function CheckDateBetween2Dates(ByVal d As Date, ByVal d1 As Date, ByVal d2 As Date) As Boolean
        If (d1 = Nothing Or d2 = Nothing) Then
            Return False
        End If
        If (Date.Compare(d, d1) >= 0 And Date.Compare(d, d2) <= 0) Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Sub txtCode_TextChanged(sender As Object, e As EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub





    Protected Sub uwgWorkPlans_SelectedRowsChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs) Handles uwgWorkPlans.SelectedRowsChange


        Try
            lbAppointmentID.Text = e.SelectedRows.Item(0).Cells.FromKey("AppointID").Value
        Catch ex As Exception
            Exit Sub
        End Try


    End Sub

End Class
