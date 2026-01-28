Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmEmployeesExcuses
    Inherits MainPage
#Region "Public Decleration"
    Private ClsEmployees As Clshrs_Employees
    Private ClsEmployeesExcuses As Clshrs_EmployeesExcuses
    Private clsMainOtherFields As clsSys_MainOtherFields

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEmployeesExcuses = New Clshrs_EmployeesExcuses(Me)
        ClsEmployees = New Clshrs_Employees(Me)
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
                Page.Session.Add("ConnectionString", ClsEmployeesExcuses.ConnectionString)
                ClsEmployeesExcuses.AddOnChangeEventToControls("frmEmployeesExcuses", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, WebTextEdit1, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)


            End If
            If Not IsPostBack Then
                If Request.QueryString.Item("EmpCode") <> Nothing Then
                    txtCode.Text = Request.QueryString.Item("EmpCode")
                    ClsEmployees.Find1("Code= " & txtCode.Text)
                    GetValues()
                End If

                If Request.QueryString.Item("NoButton") <> Nothing Then
                    ButtonDiv.Visible = False
                    ButtonDiv2.Visible = False
                End If

            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsEmployees.Find1(" Code='" & txtCode.Text & "'")
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesExcuses.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsEmployeesExcuses = New Clshrs_EmployeesExcuses(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesExcuses.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Code /يجب تحديد الكود"))
                    Exit Sub
                End If
                If DdlExcuseType.SelectedIndex = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Excuse Time /يجب تحديد وقت الإذن "))
                    Exit Sub
                End If
                If (DdlExcuseType.SelectedValue <> "Full") Then
                    If Not TimeValidation() Then
                        Exit Sub
                    End If
                End If
                If DdlShift.SelectedIndex = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Shift /يجب تحديد  الوردية "))
                    Exit Sub
                End If
                'If DdlExcuseHours.SelectedIndex = 0 Then
                '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Excuse Hours /يجب تحديد عدد الساعات"))
                '    Exit Sub
                'End If
                If WebDateChooser1.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Excuse Date /يجب إدخال تاريخ الإذن "))
                    Exit Sub
                End If
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                ClsEmployeesExcuses.Save()
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                GetValues()

                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsEmployeesExcuses.Table, ClsEmployeesExcuses.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Code /يجب تحديد الكود"))
                    Exit Sub
                End If
                If DdlExcuseType.SelectedIndex = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Excuse Type /يجب تحديد نوع الإذن "))
                    Exit Sub
                End If
                If (DdlExcuseType.SelectedValue <> "Full") Then
                    If Not TimeValidation() Then
                        Exit Sub
                    End If
                End If
                If DdlShift.SelectedIndex = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Shift /يجب تحديد  الوردية "))
                    Exit Sub
                End If
                'If DdlExcuseHours.SelectedIndex = 0 Then
                '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Excuse Hours /يجب تحديد عدد الساعات"))
                '    Exit Sub
                'End If
                If WebDateChooser1.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Excuse Date /يجب إدخال تاريخ الإذن "))
                    Exit Sub
                End If
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                ClsEmployeesExcuses.Save()
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                GetValues()
                AfterOperation()
            Case "New"
                AfterOperation()
            Case "Delete"

                ClsEmployeesExcuses.Find("ID=" & lbExcuseID.Text)
                If Not String.IsNullOrWhiteSpace(ClsEmployeesExcuses.SRC) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This Transaction was automatically generated from Self-Service and cannot be deleted from the system !  /هذه الحركة مُنشأة تلقائيًا من الخدمة الذاتية، لذلك لا يمكن حذفها من النظام. "))
                    Exit Sub
                Else
                    ClsEmployeesExcuses.Delete("ID=" & lbExcuseID.Text)
                    GetValues()
                    AfterOperation()
                End If

            Case "Property"
                ClsEmployeesExcuses.Find("ID=" & lbExcuseID.Text)
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsEmployeesExcuses.ID & "&TableName=" & ClsEmployeesExcuses.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsEmployeesExcuses.Find("ID=" & lbExcuseID.Text)
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsEmployeesExcuses.ID & "&TableName=" & ClsEmployeesExcuses.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsEmployeesExcuses.Table
                ClsEmployeesExcuses.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsEmployeesExcuses.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsEmployeesExcuses.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsEmployeesExcuses.Find("ID=" & lbExcuseID.Text)
                If ClsEmployeesExcuses.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsEmployeesExcuses.DataSet
                    If ClsEmployeesExcuses.CheckDiff(ClsEmployeesExcuses, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsEmployees.FirstRecord()
                GetValues()
            Case "Previous"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                If Not ClsEmployees.previousRecord() Then
                    ClsEmployees.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                If Not ClsEmployees.NextRecord() Then
                    ClsEmployees.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsEmployees.LastRecord()
                GetValues()
        End Select
    End Sub
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEmployees = New Clshrs_Employees(Me)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Try
            ClsEmployees.Find("Code='" & txtCode.Text & "'")
            IntId = ClsEmployees.ID
            txtArbName.Focus()
            If ClsEmployees.ID > 0 Then
                GetValues()
                StrMode = "E"
            Else
                If ClsEmployees.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If

                ImageButton_Delete.Enabled = False
                StrMode = "N"
                txtArbName.Focus()
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsEmployeesExcuses.ConnectionString, ClsEmployeesExcuses.DataBaseUserRelatedID, ClsEmployeesExcuses.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEmployeesExcuses.ConnectionString, ClsEmployeesExcuses.DataBaseUserRelatedID, ClsEmployees.GroupID, ClsEmployeesExcuses.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                ImageButton_Delete.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Function
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub
    Protected Sub DdlExcuseType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlExcuseType.SelectedIndexChanged
        If (DdlExcuseType.SelectedValue = "Full") Then
            ExcuseDurationTextBox.Enabled = False
        Else
            ExcuseDurationTextBox.Enabled = True
        End If
    End Sub
#End Region
#Region "Private Functions"
    Private Function AssignValues() As Boolean
        Try
            With ClsEmployeesExcuses
                .EmployeeID = ClsEmployees.ID
                .Notes = txtNotes.Text
                .ExcuseDate = WebDateChooser1.Value
                .ExcuseHours = ExcuseDurationTextBox.Text

                If (DdlExcuseTarget.SelectedValue.Length > 0) Then
                    .ExcuseTarget = DdlExcuseTarget.SelectedValue
                End If

                If (DdlExcuseType.SelectedValue.Length > 0) Then
                    .ExcuseType = DdlExcuseType.SelectedValue
                End If

                If (DdlExcuseCalcType.SelectedValue.Length > 0) Then
                    .ExcuseCalcType = DdlExcuseCalcType.SelectedValue
                End If

                If (DdlShift.SelectedValue.Length > 0) Then
                    .Shift = DdlShift.SelectedValue
                End If
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsUser.ConnectionString)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            txtCode.Text = ClsEmployees.Code
            txtArbName.Text = ClsEmployees.FullName
            ClsEmployees.Find1("Code ='" & txtCode.Text & "'")
            ClsEmployeesExcuses.Find("EmployeeID = " & ClsEmployees.ID)
            Dim Dt As Data.DataTable = ClsEmployeesExcuses.DataSet.Tables(0)
            uwgEmployeeExcuse.DataSource = Dt
            uwgEmployeeExcuse.DataBind()

            If Not ClsEmployeesExcuses.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsEmployeesExcuses.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsEmployeesExcuses.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsEmployeesExcuses.RegDate).Date
            End If
            If ClsEmployeesExcuses.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsEmployeesExcuses.CancelDate).Date
            End If
            If Not ClsEmployeesExcuses.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            If (ClsEmployees.ID > 0) Then
                StrMode = "E"

            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsEmployeesExcuses.ConnectionString, ClsEmployeesExcuses.DataBaseUserRelatedID, ClsEmployeesExcuses.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEmployeesExcuses.ConnectionString, ClsEmployeesExcuses.DataBaseUserRelatedID, ClsEmployeesExcuses.GroupID, ClsEmployeesExcuses.Table, ClsEmployeesExcuses.ID)
            If Not ClsEmployeesExcuses.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsEmployeesExcuses.ID)
            End If
            Return True
        Catch ex As Exception
        End Try
    End Function
    'Added By: Hassan Kurdi
    'Date: 2021-12-14
    'Purpose: Check if the time formate is correct
    Private Function TimeValidation() As Boolean
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesExcuses.ConnectionString)
        Dim regex As Regex = New Regex("^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$")
        regex.IsMatch(ExcuseDurationTextBox.Text)

        If ExcuseDurationTextBox.Text <> "" Then
            If regex.IsMatch(ExcuseDurationTextBox.Text) = False Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Excuse Duration is not in vaild Format /مدة الاستئذان بصيغة غير صحيحة"))
                Return False
            End If
        End If

        Return True
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
                    ClsEmployeesExcuses.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsEmployeesExcuses.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsEmployeesExcuses
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
        ClsEmployeesExcuses = New Clshrs_EmployeesExcuses(Me)
        Try
            With ClsEmployeesExcuses
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
        ClsEmployeesExcuses = New Clshrs_EmployeesExcuses(Me)
        If IntId > 0 Then
            ClsEmployeesExcuses.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function

    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        ClsEmployeesExcuses.Clear()
        DdlExcuseType.SelectedIndex = 0
        DdlExcuseCalcType.SelectedIndex = 0
        ExcuseDurationTextBox.Text = ""
        DdlShift.SelectedIndex = 0
        DdlExcuseTarget.SelectedIndex = 0
        txtNotes.Text = ""
        WebDateChooser1.Value = ""
        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function

    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsEmployeesExcuses = New Clshrs_EmployeesExcuses(Page)
        ClsEmployeesExcuses.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsEmployeesExcuses.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEmployeesExcuses.Table) = True Then
            Dim StrTablename As String
            ClsEmployeesExcuses = New Clshrs_EmployeesExcuses(Me)
            StrTablename = ClsEmployeesExcuses.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID & _
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID & _
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



    Protected Sub uwgEmployeeExcuse_SelectedRowsChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs) Handles uwgEmployeeExcuse.SelectedRowsChange
        lbExcuseID.Text = e.SelectedRows.Item(0).Cells.FromKey("ID").Value
        ImageButton_Delete.Enabled = True
    End Sub
End Class
