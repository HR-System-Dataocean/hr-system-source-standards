Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmDepartments
    Inherits MainPage
#Region "Public Decleration"
    Private ClsDepartments As Clssys_Departments
    Private ClsSectors As ClsSys_Sectors
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsDepartments = New Clssys_Departments(Me.Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsDepartments.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsDepartments.ConnectionString)
                ClsDepartments.AddOnChangeEventToControls("frmDepartments", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]

                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                'ClsDepartments.GetDropDownList(ddl_MainDepartment, True)
                ClsSectors = New ClsSys_Sectors(Me.Page)

                ClsSectors.GetDropDownList(ddl_Sectors, True)
                uwgBranches.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsDepartments.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsDepartments.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsDepartments.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsDepartments = New Clssys_Departments(Me.Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDepartments.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                SavePart()
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If SavePart() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                End If
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsDepartments.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsDepartments.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsDepartments.ID & "&TableName=" & ClsDepartments.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsDepartments.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsDepartments.ID & "&TableName=" & ClsDepartments.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"

            Case "Exit"

            Case "First"
                ClsDepartments.Find("Code='" & txtCode.Text & "'")
                ClsDepartments.FirstRecord()
                GetValues(ClsDepartments)
            Case "Previous"
                ClsDepartments.Find("Code='" & txtCode.Text & "'")
                If Not ClsDepartments.previousRecord() Then
                    ClsDepartments.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))

                End If
                GetValues(ClsDepartments)
            Case "Next"
                ClsDepartments.Find("Code='" & txtCode.Text & "'")
                If Not ClsDepartments.NextRecord() Then
                    ClsDepartments.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))

                End If
                GetValues(ClsDepartments)
            Case "Last"
                ClsDepartments.Find("Code='" & txtCode.Text & "'")
                ClsDepartments.LastRecord()
                GetValues(ClsDepartments)
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        ClsDepartments = New Clssys_Departments(Page)

        Try
            ClsDepartments.Find("Code='" & txtCode.Text & "'")

            If ClsDepartments.ID > 0 Then
                If Not AssignValue(ClsDepartments) Then
                    Exit Function
                End If
                ClsDepartments.Update("Code='" & txtCode.Text & "'")
                UpdateSector()

            Else
                If Not AssignValue(ClsDepartments) Then
                    Exit Function
                End If
                ClsDepartments.Save()
                UpdateSector()


            End If

            ClsDepartments.Find("Code='" & txtCode.Text & "'")
            ClsDepartments.SaveAllBranches(ClsDepartments.ID, uwgBranches)
            clsMainOtherFields.CollectDataAndSave(value.Text, ClsDepartments.Table, ClsDepartments.ID)
            value.Text = ""
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function AssignValue(ByRef ClsDepartments As Clssys_Departments) As Boolean
        Try
            With ClsDepartments
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .ArbName4S = txtArbName.Text
                .CostCenterCode = TxtCostCenterCode.Text
                '.ParentID = IIf(ddl_MainDepartment.SelectedItem.Value = 0, Nothing, ddl_MainDepartment.SelectedItem.Value)
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues(ByVal ClsDepartments As Clssys_Departments) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsDepartments
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                TxtCostCenterCode.Text = .CostCenterCode
                uwgBranches.DataSource = ClsDepartments.GetAllBranches(.ID).Tables(0).DefaultView
                uwgBranches.DataBind()

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
                ddl_Sectors.Items.Clear()
                ClsSectors.GetDropDownList(ddl_Sectors, True)
                ddl_Sectors.SelectedValue = GetSector(.ID)

                'ddl_MainDepartment.Items.Clear()
                'Dim str As String = ""
                'GetChildDepartments(.ID, .ConnectionString, str)
                'str = str.Trim(",")
                'If str.Trim <> String.Empty Then
                '    .GetDropDownList(ddl_MainDepartment, True, " ID <> " & .ID & " And ID  not in ( " & str & " )")
                'Else
                '    .GetDropDownList(ddl_MainDepartment, True, " ID <> " & .ID)
                'End If
                'ddl_MainDepartment.SelectedValue = IIf(.ParentID Is Nothing, 0, .ParentID)


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
                    CreateOtherFields(ClsDepartments.ID)
                End If
            End With
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function GetChildDepartments(ByVal DepartmentID As Integer, ByVal Connection As String, ByRef RetStr As String) As Boolean
        Dim strSelect As String = " Select ID from sys_Departments where ParentID = " & DepartmentID
        Dim DS As New Data.DataSet
        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Connection, Data.CommandType.Text, strSelect)
        If (DS.Tables(0).Rows.Count > 0) Then
            For Each row As Data.DataRow In DS.Tables(0).Rows
                RetStr = RetStr & row("ID") & ","
                GetChildDepartments(row("ID"), Connection, RetStr)
            Next
        Else
            Return True
        End If
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
                    ClsDepartments.Find("ID=" & intID)
                    GetValues(ClsDepartments)
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsDepartments.Find("ID=" & intID)
                    GetValues(ClsDepartments)
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsDepartments
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
        ClsDepartments = New Clssys_Departments(Me.Page)
        Try
            With ClsDepartments
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
        ClsDepartments = New Clssys_Departments(Me.Page)
        If IntId > 0 Then
            ClsDepartments.Find("ID=" & IntId)
            GetValues(ClsDepartments)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsDepartments = New Clssys_Departments(Me.Page)
        ClsSectors = New ClsSys_Sectors(Me.Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Try
            ClsDepartments.Find("Code='" & txtCode.Text & "'")
            IntId = ClsDepartments.ID
            txtEngName.Focus()
            If ClsDepartments.ID > 0 Then
                GetValues(ClsDepartments)
                StrMode = "E"
            Else
                If ClsDepartments.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                uwgBranches.DataSource = ClsDepartments.GetAllBranches(ClsDepartments.ID).Tables(0).DefaultView
                uwgBranches.DataBind()
                Clear()
                ImageButton_Delete.Enabled = False
                ClsSectors.GetDropDownList(ddl_Sectors, True)
                ' ClsDepartments.GetDropDownList(ddl_MainDepartment, True)
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsDepartments.ConnectionString, ClsDepartments.DataBaseUserRelatedID, ClsDepartments.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsDepartments.ConnectionString, ClsDepartments.DataBaseUserRelatedID, ClsDepartments.GroupID, ClsDepartments.Table, IntId)
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
    Private Function AfterOperation() As Boolean
        ClsDepartments.Clear()
        GetValues(ClsDepartments)
        uwgBranches.Clear()
        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        TxtCostCenterCode.Text = String.Empty
        'ddl_MainDepartment.SelectedIndex = 0
        ddl_Sectors.SelectedIndex = 0
        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsDepartments = New Clssys_Departments(Me.Page)
        ClsDepartments.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsDepartments.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsDepartments.Table) = True Then
            Dim StrTablename As String
            ClsDepartments = New Clssys_Departments(Me)
            StrTablename = ClsDepartments.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID &
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID &
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

    Private Function UpdateSector()
        Dim strDepartmentID As String
        strDepartmentID = "select ID from sys_Departments where Code='" & txtCode.Text & "'"
        Dim DepartmentID As Integer
        DepartmentID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, strDepartmentID)
        Dim UpdateDepartmentSector As String
        If DepartmentID > 0 Then
            UpdateDepartmentSector = "Update sys_SectorsDepartments set SectorID=" & ddl_Sectors.SelectedValue & " where DepartmentID=" & DepartmentID & " and Checked =1"
        Else
            UpdateDepartmentSector = "Update sys_SectorsDepartments set Checked =1 where SectorID=" & ddl_Sectors.SelectedValue & " AND DepartmentID=" & DepartmentID & ""
        End If
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, UpdateDepartmentSector)



    End Function
    Private Function GetSector(DepartmentID As Integer) As Integer
        Dim strSectorID As String

        strSectorID = "select SectorID from sys_SectorsDepartments where  DepartmentID=" & ClsDepartments.ID & " and Checked =1"
        Dim SectorID As Integer
        SectorID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, strSectorID)

        Return SectorID

    End Function

#End Region
End Class
