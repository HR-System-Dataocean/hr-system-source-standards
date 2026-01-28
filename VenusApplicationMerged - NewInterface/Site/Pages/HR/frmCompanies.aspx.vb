Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmCompanies
    Inherits MainPage

#Region "Public Decleration"
    Private ClsCompanies As Clssys_Companies
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As New Venus.Shared.ErrorsHandler
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsCompanies = New Clssys_Companies(Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsCompanies.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsCompanies.ConnectionString)
                ClsCompanies.AddOnChangeEventToControls("frmCompanies", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsCompanies.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsCompanies.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsCompanies.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsCompanies = New Clssys_Companies(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsCompanies.ConnectionString)

        Select Case e.CommandArgument
            Case "New"
                AfterOperation()

            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If SavePart() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done /تم الحفظ"))
                End If

            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                SavePart()
                AfterOperation()

            Case "Delete"
                ClsCompanies.Find("Code='" & txtCode.Text & "'")
                If ClsCompanies.ID <> Session("CompanyID") Then
                    ClsCompanies.Delete("Code='" & txtCode.Text & "'")
                    AfterOperation()
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Can't delete the current company /لا يمكن حذف الشركة الحالية"))
                End If
            Case "Property"
                If ClsCompanies.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsCompanies.ID & "&TableName=" & ClsCompanies.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                End If
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Remarks"
                If ClsCompanies.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsCompanies.ID & "&TableName=" & ClsCompanies.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                End If

            Case "Other Fields"

            Case "Exit"

            Case "First"
                ClsCompanies.FirstRecord()
                GetValues(ClsCompanies)

            Case "Previous"
                ClsCompanies.Find("Code='" & txtCode.Text & "'")
                If Not ClsCompanies.previousRecord() Then
                    ClsCompanies.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues(ClsCompanies)

            Case "Next"
                ClsCompanies.Find("Code='" & txtCode.Text & "'")
                If Not ClsCompanies.NextRecord() Then
                    ClsCompanies.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues(ClsCompanies)

            Case "Last"
                ClsCompanies.LastRecord()
                GetValues(ClsCompanies)

        End Select
    End Sub

    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function AssignValue(ByRef ClsCompanies As Clssys_Companies) As Boolean
        Dim ClsCities As New Clssys_Cities(Page)
        Try
            With ClsCompanies
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text

                .IsHigry = chkIsHegri.Checked
                .IncludeAbsencDays = chkAbsents.Checked
                .PrepareDay = txtPrepareDay.Value
                .HasSequence = chkHasSequence.Checked()
                .CountEmployeeVacationDaysTotal = CountEmployeeVacationCheckBox.Checked

                .EmpFirstName = ddlFirst.SelectedValue
                .EmpSecondName = ddlSecond.SelectedValue
                .EmpThirdName = ddlThird.SelectedValue
                .EmpFourthName = ddlFourth.SelectedValue
                .EmpNameSeparator = txtSeparator.Text
                .DefaultAttend = IIf(chkDefaultAttend.Checked = True, 1, 0)
                .ZeroBalAfterVac = ZeroBalAfterVacCheckBox.Checked
                .VacSettlement = VacSettlementCheckBox.Checked
                If txtExecuseRequestHoursallowed.Text <> "" Then
                    .ExecuseRequestHoursallowed = Convert.ToInt16(txtExecuseRequestHoursallowed.Text)
                End If
                .EmployeeDocumentsAutoSerial = ChKEmployeeDocumentsAutoSerial.Checked

                If Not IsNumeric(txtSequenceLength.Text) Then txtSequenceLength.Text = 0
                .SequenceLength = txtSequenceLength.Text

                ''#0001 [Abdulrahman] [03/05/2012]
                .SalaryCalculation = ddlSalary.SelectedIndex
                .Separator = TextBox_Separator.Text
                .Prefix = ddlprefix.SelectedValue

                .AllowOverVacation = AllowOverVacationCheckBox.Checked
                .VacationFromPrepareDay = ChkVacationFromPrepareDay.Checked
                .UserDepartmentsPermissions = chkUserDepartmentsPermissions.Checked


            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetValues(ByRef ClsCompanies As Clssys_Companies) As Boolean

        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsCompanies
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                chkHasSequence.Checked = .HasSequence
                txtSequenceLength.Text = .SequenceLength

                chkIsHegri.Checked = .IsHigry
                chkAbsents.Checked = .IncludeAbsencDays
                txtPrepareDay.Value = .PrepareDay

                ddlFirst.SelectedValue = IIf(Not IsDBNull(.EmpFirstName), .EmpFirstName, "FR")
                ddlSecond.SelectedValue = IIf(Not IsDBNull(.EmpSecondName), .EmpSecondName, "FA")
                ddlThird.SelectedValue = IIf(Not IsDBNull(.EmpThirdName), .EmpThirdName, "GR")
                ddlFourth.SelectedValue = IIf(Not IsDBNull(.EmpFourthName), .EmpFourthName, "FM")
                txtSeparator.Text = .EmpNameSeparator
                chkDefaultAttend.Checked = .DefaultAttend
                TextBox_Separator.Text = .Separator
                ddlprefix.SelectedValue = .Prefix
                ZeroBalAfterVacCheckBox.Checked = .ZeroBalAfterVac
                VacSettlementCheckBox.Checked = .VacSettlement

                txtExecuseRequestHoursallowed.Text = .ExecuseRequestHoursallowed
                ChKEmployeeDocumentsAutoSerial.Checked = .EmployeeDocumentsAutoSerial

                If txtSeparator.Text = "" Then
                    txtSeparator.Text = ","
                End If
                ''#0001 [Abdulrahman] [03/05/2012]
                ddlSalary.SelectedIndex = .SalaryCalculation

                If Not ClsCompanies.RegUserID = Nothing Then
                    ClsUser.Find("ID=" & ClsCompanies.RegUserID)
                End If

                If ClsUser.EngName = Nothing Then
                    lblRegUserValue.Text = ""
                Else
                    lblRegUserValue.Text = ClsUser.EngName
                End If

                If Convert.ToDateTime(ClsCompanies.RegDate).Date = Nothing Then
                    lblRegDateValue.Text = ""
                Else
                    lblRegDateValue.Text = Convert.ToDateTime(ClsCompanies.RegDate).Date
                End If

                If ClsCompanies.CancelDate = Nothing Then
                    lblCancelDateValue.Text = ""
                    ImageButton_Delete.Enabled = True
                Else
                    lblCancelDateValue.Text = Convert.ToDateTime(ClsCompanies.CancelDate).Date
                    ImageButton_Delete.Enabled = False
                End If
                CountEmployeeVacationCheckBox.Checked = .CountEmployeeVacationDaysTotal
                AllowOverVacationCheckBox.Checked = .AllowOverVacation
                ChkVacationFromPrepareDay.Checked = .VacationFromPrepareDay
                chkUserDepartmentsPermissions.Checked = .UserDepartmentsPermissions

            End With
            'E#001 [0261][27-05-2008]
            If (ClsCompanies.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsCompanies.ConnectionString, ClsCompanies.DataBaseUserRelatedID, ClsCompanies.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsCompanies.ConnectionString, ClsCompanies.DataBaseUserRelatedID, ClsCompanies.GroupID, ClsCompanies.Table, ClsCompanies.ID)
            If Not ClsCompanies.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsCompanies.ID)
            End If
            Return True
        Catch ex As Exception
            If Page.IsPostBack Then CreateOtherFields(ClsCompanies.ID)
            Return False
        End Try
    End Function
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")

        Try

            ClsCompanies = New Clssys_Companies(Page)
            ClsCompanies.Find("Code='" & txtCode.Text & "'")
            If Not AssignValue(ClsCompanies) Then
                Exit Function
            End If
            If ClsCompanies.ID > 0 Then
                ClsCompanies.Update("Code='" & txtCode.Text & "'")
            Else
                ClsCompanies.Save()
            End If
            clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
            ClsCompanies.Find("Code='" & txtCode.Text & "'")
            clsMainOtherFields.CollectDataAndSave(value.Text, ClsCompanies.Table, ClsCompanies.ID)
            value.Text = ""

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
                    ClsCompanies.Find("ID=" & intID)
                    GetValues(ClsCompanies)
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsCompanies.Find("ID=" & intID)
                    GetValues(ClsCompanies)
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsCompanies
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
        ClsCompanies = New Clssys_Companies(Page)
        Try
            With ClsCompanies
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
        ClsCompanies = New Clssys_Companies(Page)
        If IntId > 0 Then
            ClsCompanies.Find("ID=" & IntId)
            GetValues(ClsCompanies)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsCompanies = New Clssys_Companies(Page)
        Try
            ClsCompanies.Find("Code='" & txtCode.Text & "'")
            IntId = ClsCompanies.ID
            txtEngName.Focus()
            If ClsCompanies.ID > 0 Then
                GetValues(ClsCompanies)
                StrMode = "E"
            Else
                If ClsCompanies.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsCompanies.ConnectionString, ClsCompanies.DataBaseUserRelatedID, ClsCompanies.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsCompanies.ConnectionString, ClsCompanies.DataBaseUserRelatedID, ClsCompanies.GroupID, ClsCompanies.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                ImageButton_Delete.Enabled = False
            End If

        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsCompanies.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
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
        ClsCompanies.Clear()
        GetValues(ClsCompanies)
        ddlFirst.SelectedIndex = 0
        ddlSecond.SelectedIndex = 1
        ddlThird.SelectedIndex = 2
        ddlFourth.SelectedIndex = 3
        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If

    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        txtSeparator.Text = ","
        chkIsHegri.Checked = False
        chkHasSequence.Checked = False
        chkAbsents.Checked = False
        ddlFirst.SelectedIndex = 0
        ddlSecond.SelectedIndex = 1
        ddlThird.SelectedIndex = 2
        ddlFourth.SelectedIndex = 3
        chkDefaultAttend.Checked = False
        ImageButton_Delete.Enabled = False
        txtPrepareDay.Value = 0
        lblRegUserValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""

        chkHasSequence.Checked = False
        txtSequenceLength.Text = ""
        TextBox_Separator.Text = ""
        ddlprefix.SelectedValue = 0
        ddlSalary.SelectedIndex = 0
        VacSettlementCheckBox.Checked = False
        AllowOverVacationCheckBox.Checked = False
        ChkVacationFromPrepareDay.Checked = False
        chkUserDepartmentsPermissions.Checked = False

        txtExecuseRequestHoursallowed.Text = ""
        ChKEmployeeDocumentsAutoSerial.Checked = False
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsCompanies = New Clssys_Companies(Page)
        ClsCompanies.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsCompanies.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsCompanies.Table) = True Then
            Dim StrTablename As String
            ClsCompanies = New Clssys_Companies(Me)
            StrTablename = ClsCompanies.Table
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

#End Region
End Class
