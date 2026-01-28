Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmUsers
    Inherits MainPage
#Region "Public Decleration"
    Private ObjClsSys_Users As Clssys_Users
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ObjClsSys_Users = New Clssys_Users(Me)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ObjClsSys_Users.Table.Trim.Replace(" ", "") & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearch.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            If ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim.Replace(" ", "") & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmpCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtEmpCode.ClientID & "'"
                    btnEmpCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ObjClsSys_Users.ConnectionString)
                ObjClsSys_Users.AddOnChangeEventToControls("frmUsers", Page, UltraWebTab1)
                Validator.Text = ""
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            End If
            '================================== Add DateUpdateSchedules [Start]

            'Dim ClsCurrUser As New Clssys_Users(Page)
            'If ClsCurrUser.Find(" ID =" & ObjClsSys_Users.DataBaseUserRelatedID) Then
            '    If ClsCurrUser.IsAdmin Then
            '        txtIsAdmin.Value = 1
            '        btnSearch.Visible = True
            '        TlbMainNavigation.Visible = True
            '        txtCode.ReadOnly = False
            '    Else
            '        txtIsAdmin.Value = 0
            '        btnSearch.Visible = False
            '        TlbMainNavigation.Visible = False
            '        txtCode.ReadOnly = True
            '    End If
            'End If

            chkChangePassword.Attributes.Add("OnClick", "ChangePasswordFn()")
            txtOldPass.Attributes.Add("OnChange", "txtPasswordChange()")

            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ObjClsSys_Users.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ObjClsSys_Users.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ObjClsSys_Users.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ObjClsSys_Users = New Clssys_Users(Me.Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ObjClsSys_Users.ConnectionString)

        Dim ObjSecurity As New Venus.Shared.Security.SQLSecurityHandler(ObjClsSys_Users.ConnectionString)
        Dim ObjEncode As New Venus.Shared.Security.ClientCrypt
        Dim StrNewPassword As String = String.Empty
        Dim strOldPassword As String = String.Empty
        Dim ClsCompaniesUsers As New Clssys_CompaniesUsers(Page)

        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")

                If Not AssignValues(strOldPassword, StrNewPassword) Then
                    Exit Sub
                End If

                If ObjClsSys_Users.ID > 0 Then
                    ObjClsSys_Users.Update("Code='" & txtCode.Text & "'")
                    If StrNewPassword <> "" Then
                        'ObjSecurity.ChangeSQLUserPassword(txtCode.Text, strOldPassword, StrNewPassword)
                    End If
                Else
                    If ObjClsSys_Users.Save() Then
                        'ObjSecurity.CreateSQLUser(ObjClsSys_Users.Code, ObjEncode.Decrypt(ObjClsSys_Users.Password, "DataOcean"), ObjClsSys_Users.DataBaseName)
                    End If
                End If

                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgUsersCompanies.Rows
                    ClsCompaniesUsers = New Clssys_CompaniesUsers(Page)
                    If row.Cells(2).Value <> 0 Then
                        ClsCompaniesUsers.UserID = ObjClsSys_Users.ID
                        ClsCompaniesUsers.CompanyID = row.Cells(3).Value
                        ClsCompaniesUsers.CanView = row.Cells(1).Value
                        ClsCompaniesUsers.Update(" ID =" & row.Cells(2).Value)
                    Else
                        ClsCompaniesUsers.UserID = ObjClsSys_Users.ID
                        ClsCompaniesUsers.CompanyID = row.Cells(3).Value
                        ClsCompaniesUsers.CanView = row.Cells(1).Value
                        ClsCompaniesUsers.Save()
                    End If


                Next

                For Each rowdt As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgUsersBranches.Rows
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ObjClsSys_Users.ConnectionString, "sys_SaveCompaniesBranches", rowdt.Cells.FromKey("ID").Value _
                                                                                , rowdt.Cells.FromKey("BrancheID").Value, rowdt.Cells.FromKey("CompanyID").Value _
                                                                                , ObjClsSys_Users.ID, rowdt.Cells.FromKey("CanView").Value _
                                                                                , ObjClsSys_Users.DataBaseUserRelatedID)
                Next

                SetReturnback()

                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ObjClsSys_Users.Table, ObjClsSys_Users.ID)
                value.Text = ""
                AfterOperation()
                txtEmpCode.Text = ""
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")

                If Not AssignValues(strOldPassword, StrNewPassword) Then
                    Exit Sub
                End If
                If ObjClsSys_Users.ID > 0 Then
                    ObjClsSys_Users.Update("Code='" & txtCode.Text & "'")
                    If StrNewPassword <> "" Then
                        'ObjSecurity.ChangeSQLUserPassword(txtCode.Text, strOldPassword, StrNewPassword)
                    End If
                Else
                    If ObjClsSys_Users.Save() Then
                        'ObjSecurity.CreateSQLUser(ObjClsSys_Users.Code, ObjEncode.Decrypt(ObjClsSys_Users.Password, "DataOcean"), ObjClsSys_Users.DataBaseName)
                    End If
                End If

                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgUsersCompanies.Rows
                    ClsCompaniesUsers = New Clssys_CompaniesUsers(Page)
                    If row.Cells(2).Value <> 0 Then
                        ClsCompaniesUsers.UserID = ObjClsSys_Users.ID
                        ClsCompaniesUsers.CompanyID = row.Cells(3).Value
                        ClsCompaniesUsers.CanView = row.Cells(1).Value
                        ClsCompaniesUsers.Update(" ID =" & row.Cells(2).Value)
                    Else
                        ClsCompaniesUsers.UserID = ObjClsSys_Users.ID
                        ClsCompaniesUsers.CompanyID = row.Cells(3).Value
                        ClsCompaniesUsers.CanView = row.Cells(1).Value
                        ClsCompaniesUsers.Save()
                    End If


                Next

                For Each rowdt As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgUsersBranches.Rows
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ObjClsSys_Users.ConnectionString, "sys_SaveCompaniesBranches", rowdt.Cells.FromKey("ID").Value _
                                                                                , rowdt.Cells.FromKey("BrancheID").Value, rowdt.Cells.FromKey("CompanyID").Value _
                                                                                , ObjClsSys_Users.ID, rowdt.Cells.FromKey("CanView").Value _
                                                                                , ObjClsSys_Users.DataBaseUserRelatedID)
                Next

                SetReturnback()

                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ObjClsSys_Users.Table, ObjClsSys_Users.ID)
                value.Text = ""
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
                txtEmpCode.Text = ""
            Case "Delete"
                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                Dim intUID = ObjClsSys_Users.ID
                ClsCompaniesUsers.Delete(" UserID = " & intUID)
                ObjClsSys_Users.Delete("Code='" & txtCode.Text & "'")

                SetReturnback()
                txtEmpCode.Text = ""
            Case "Property"
                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ObjClsSys_Users.ID & "&TableName=" & ObjClsSys_Users.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ObjClsSys_Users.ID & "&TableName=" & ObjClsSys_Users.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ObjClsSys_Users.Table
                ObjClsSys_Users.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ObjClsSys_Users.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ObjClsSys_Users.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ObjClsSys_Users.Find(" Code= '" & txtCode.Text & "'")
                If ObjClsSys_Users.ID > 0 Then
                    Dim Ds As Data.DataSet = ObjClsSys_Users.DataSet
                    'Update By Abdulrahman
                    'If Not AssignValues() Then
                    '    Exit Sub
                    'End If
                    If ObjClsSys_Users.CheckDiff(ObjClsSys_Users, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ObjClsSys_Users.FirstRecord()
                txtEmpCode.Text = ""
                GetValues()
            Case "Previous"
                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                If Not ObjClsSys_Users.previousRecord() Then
                    ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                txtEmpCode.Text = ""
                GetValues()
            Case "Next"
                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                If Not ObjClsSys_Users.NextRecord() Then
                    ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                txtEmpCode.Text = ""
                GetValues()
            Case "Last"
                ObjClsSys_Users.LastRecord()
                txtEmpCode.Text = ""
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub
    Protected Sub uwgUsersCompanies_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgUsersCompanies.InitializeRow
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        Dim _culture As String = StrLanguage

        If _culture = "ar-EG" Then
            e.Row.Cells(0).Value = e.Row.Cells(5).Value
        Else
            e.Row.Cells(0).Value = e.Row.Cells(4).Value
        End If
    End Sub
    Protected Sub uwgUsersBranches_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgUsersBranches.InitializeRow
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        Dim _culture As String = StrLanguage

        If _culture = "ar-EG" Then
            uwgUsersBranches.DisplayLayout.Bands(0).Columns.FromKey("ArbName").Hidden = False
        Else
            uwgUsersBranches.DisplayLayout.Bands(0).Columns.FromKey("EngName").Hidden = False
        End If

        Dim CompanyID As Integer = uwgUsersCompanies.Rows(0).Cells.FromKey("CompanyID").Value

        HideDetailsRows(CompanyID)
    End Sub
    Protected Sub uwgUsersCompanies_ActiveRowChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgUsersCompanies.ActiveRowChange

        Dim CompanyID As Integer = uwgUsersCompanies.Rows(e.Row.Index).Cells.FromKey("CompanyID").Value

        HideDetailsRows(CompanyID)
    End Sub

#End Region

#Region "Private Functions"
    Private Sub HideDetailsRows(ByVal CompanyID As Integer)
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgUsersBranches.Rows()
            If row.Cells.FromKey("CompanyID").Value = CompanyID Then
                row.Hidden = False
            Else
                row.Hidden = True
            End If
        Next
    End Sub
    Private Function SetReturnback() As Boolean
        'Venus.Shared.Web.ClientSideActions.DoRefresh(Page)
    End Function
    Private Function AssignValues(ByRef OldPassword As String, ByRef NewPassword As String) As Boolean
        Dim ObjEncode As New Venus.Shared.Security.ClientCrypt
        Try
            With ObjClsSys_Users


                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text


                If txtEmpCode.Text <> "" Then
                    Dim clsEmp As New Clshrs_Employees(Page)
                    clsEmp.Find("Code='" & txtEmpCode.Text & "'")
                    .RelEmployee = clsEmp.ID
                Else
                    .RelEmployee = 0
                End If


                If chkChangePassword.Checked Then
                    .Password = ObjEncode.Encrypt(txtPassword.Text, "DataOcean")
                    NewPassword = txtPassword.Text
                    OldPassword = txtOldPassword.Text
                Else
                    .Password = ObjEncode.Encrypt(txtOldPassword.Text, "DataOcean")
                    NewPassword = ""
                    OldPassword = ""
                End If


                .IsAdmin = ChkIsAdmin.Checked
                .IsArabic = ChkisArabic.Checked
                .CanChangePassword = ChkCanChangePassword.Checked
                .DenyAccessforall = ChkDenyAccessforall.Checked
                .WorkasIndividualuser = ChkWorkasIndividualuser.Checked
                .ChangeFormPermission = ChkFormPermission.Checked
                .ChangeControlPermission = ChkControlPermission.Checked
                .ChangeRecordPermission = ChkRecordPermiddion.Checked
                .ChangeReportPermission = ChkReportPermission.Checked
                .ChangeModulePermission = ChkModulePermission.Checked
                .CanChangeGroups = ChkCanChangeGroups.Checked
                .PasswordExpiry = txtPasswordExpiryAt.Text
                .PasswordChangedOn = txtPasswordChangedOn.Text
                .RegDate = Now.Date
                .SessionIdleTime = 60

            End With
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim Encode As New Venus.Shared.Security.ClientCrypt
        Dim ClsCompaniesUsers As New Clssys_CompaniesUsers(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ObjClsSys_Users

                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName

                If .RelEmployee <> Nothing Then
                    Dim clsEmp As New Clshrs_Employees(Page)
                    clsEmp.Find("ID=" & .RelEmployee)
                    txtEmpCode.Text = clsEmp.Code
                Else
                    txtEmpCode.Text = ""
                End If


                txtPassword.Text = .Password
                txtOldPassword.Text = Encode.Decrypt(.Password, "DataOcean")
                ChkIsAdmin.Checked = IIf(.IsAdmin = 0, False, True)
                ChkisArabic.Checked = IIf(.IsArabic = 0, False, True)
                txtPasswordExpiryAt.Value = .PasswordExpiry
                txtPasswordChangedOn.Value = .PasswordChangedOn

                ChkCanChangePassword.Checked = .CanChangePassword
                ChkDenyAccessforall.Checked = .DenyAccessforall
                ChkWorkasIndividualuser.Checked = .WorkasIndividualuser

                ChkFormPermission.Checked = .ChangeFormPermission
                ChkControlPermission.Checked = .ChangeControlPermission
                ChkRecordPermiddion.Checked = .ChangeRecordPermission
                ChkReportPermission.Checked = .ChangeReportPermission
                ChkModulePermission.Checked = .ChangeModulePermission
                ChkCanChangeGroups.Checked = .CanChangeGroups

                uwgUsersCompanies.DataSource = Nothing
                uwgUsersBranches.DataSource = Nothing

                Dim dsCompanies As New Data.DataSet()
                ClsCompaniesUsers.GetAllCompaniesByUser(.ID, dsCompanies)
                uwgUsersCompanies.DataSource = dsCompanies
                uwgUsersCompanies.DataBind()

                Dim CompanyID As Integer = dsCompanies.Tables(0).Rows(0).Item("CompanyID")

                Dim ds As New Data.DataSet
                ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ObjClsSys_Users.ConnectionString, "sys_GetCompaniesBranches", ObjClsSys_Users.ID)

                uwgUsersBranches.DataSource = ds.Tables(0).DefaultView
                uwgUsersBranches.DataBind()

            End With

            If Not ObjClsSys_Users.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ObjClsSys_Users.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ObjClsSys_Users.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ObjClsSys_Users.RegDate).Date
            End If
            If ObjClsSys_Users.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ObjClsSys_Users.CancelDate).Date
            End If
            If Not ObjClsSys_Users.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()

            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ObjClsSys_Users.ConnectionString)
            If (ObjClsSys_Users.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ObjClsSys_Users.ConnectionString, ObjClsSys_Users.DataBaseUserRelatedID, ObjClsSys_Users.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ObjClsSys_Users.ConnectionString, ObjClsSys_Users.DataBaseUserRelatedID, ObjClsSys_Users.GroupID, ObjClsSys_Users.Table, ObjClsSys_Users.ID)
            If Not ObjClsSys_Users.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ObjClsSys_Users.ID)
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
                    ObjClsSys_Users.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ObjClsSys_Users.Find("ID=" & intID)
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
            With ObjClsSys_Users
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
        ObjClsSys_Users = New Clssys_Users(Me)
        Try
            With ObjClsSys_Users
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
        ObjClsSys_Users = New Clssys_Users(Me)
        If IntId > 0 Then
            ObjClsSys_Users.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Try
            ObjClsSys_Users = New Clssys_Users(Page)
            ObjClsSys_Users.Find("Code='" & txtCode.Text & "'", False)
            Dim prevCode = txtCode.Text

            If ObjClsSys_Users.ID > 0 Then
                GetValues()
            Else
                GetValues()
                txtCode.Text = prevCode
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
        ObjClsSys_Users.Clear()
        GetValues()
        ImageButton_Delete.Enabled = False

        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        txtCode.Text = ""
        txtEngName.Text = ""
        txtArbName.Text = ""
        txtPassword.Text = ""
        txtOldPassword.Text = ""
        ChkIsAdmin.Checked = ""
        ChkisArabic.Checked = ""
        txtPasswordExpiryAt.Value = ""
        txtPasswordChangedOn.Value = ""
        ChkCanChangePassword.Checked = False
        ChkDenyAccessforall.Checked = False
        ChkWorkasIndividualuser.Checked = False
        ChkFormPermission.Checked = False
        ChkControlPermission.Checked = False
        ChkRecordPermiddion.Checked = False
        ChkReportPermission.Checked = False
        ChkModulePermission.Checked = False
        ChkCanChangeGroups.Checked = False
        uwgUsersCompanies.DataSource = Nothing
        uwgUsersCompanies.DataBind()
        uwgUsersBranches.DataSource = Nothing
        uwgUsersBranches.DataBind()

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
        Validator.Text = ""
        txtEmpCode.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ObjClsSys_Users = New Clssys_Users(Page)
        ObjClsSys_Users.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ObjClsSys_Users.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ObjClsSys_Users.Table) = True Then
            Dim StrTablename As String
            ObjClsSys_Users = New Clssys_Users(Me)
            StrTablename = ObjClsSys_Users.Table
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

    Protected Sub chkCheckAll_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkCheckAll.CheckedChanged
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgUsersBranches.Rows()
            row.Cells.FromKey("CanView").Value = chkCheckAll.Checked
        Next
    End Sub
End Class
