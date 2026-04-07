Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Application.SystemFiles.System
Imports Venus.Shared.Web
Partial Class frmUsers
    Inherits MainPage
#Region "Public Decleration"
    Private ObjClsSys_Users As Clssys_Users
    Private ClsUsers As Clssys_Users
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ObjClsSys_Users = New Clssys_Users(Me)
        ClsUsers = New Clssys_Users(Me)
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

            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ObjClsSys_Users.ConnectionString)
                ObjClsSys_Users.AddOnChangeEventToControls("frmUsers", Page, UltraWebTab1)

                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            End If


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
        ClsUsers = New Clssys_Users(Me.Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ObjClsSys_Users.ConnectionString)

        Dim ObjSecurity As New Venus.Shared.Security.SQLSecurityHandler(ObjClsSys_Users.ConnectionString)


        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                Dim str As String = "Delete From SS_SelfServiceTransactionUserPermissions where UserID=" & ObjClsSys_Users.ID & ""


                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ObjClsSys_Users.ConnectionString, Data.CommandType.Text, str)

                Dim User As String = String.Empty
                Dim WebHandler As New Venus.Shared.Web.WebHandler

                WebHandler.GetCookies(Page, "UserID", User)
                Dim _sys_User As New Clssys_Users(Page)
                _sys_User.Find("ID = '" & User & "'")



                Dim strinsert = "Insert Into SS_SelfServiceTransactionUserPermissions values(" & ObjClsSys_Users.ID & ",'" & ChkCanDeleteSelfServiceTransactions.Checked & "',GetDate()," & _sys_User.ID & ")"

                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ObjClsSys_Users.ConnectionString, Data.CommandType.Text, strinsert)
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))



                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ObjClsSys_Users.Table, ObjClsSys_Users.ID)
                value.Text = ""
                Clear()
            Case "Save"

                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                Dim str As String = "Delete From SS_SelfServiceTransactionUserPermissions where UserID=" & ObjClsSys_Users.ID & ""


                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ObjClsSys_Users.ConnectionString, Data.CommandType.Text, str)

                Dim User As String = String.Empty
                Dim WebHandler As New Venus.Shared.Web.WebHandler

                WebHandler.GetCookies(Page, "UserID", User)
                Dim _sys_User As New Clssys_Users(Page)
                _sys_User.Find("ID = '" & User & "'")



                Dim strinsert = "Insert Into SS_SelfServiceTransactionUserPermissions values(" & ObjClsSys_Users.ID & ",'" & ChkCanDeleteSelfServiceTransactions.Checked & "',GetDate()," & _sys_User.ID & ")"

                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ObjClsSys_Users.ConnectionString, Data.CommandType.Text, strinsert)
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))




            Case "New"
                Clear()
            Case "Delete"
                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                Dim intUID = ObjClsSys_Users.ID
                ObjClsSys_Users.Delete("Code='" & txtCode.Text & "'")


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

                GetValues()
            Case "Previous"
                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                If Not ObjClsSys_Users.previousRecord() Then
                    ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If

                GetValues()
            Case "Next"
                ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                If Not ObjClsSys_Users.NextRecord() Then
                    ObjClsSys_Users.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If

                GetValues()
            Case "Last"
                ObjClsSys_Users.LastRecord()

                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"

    Private Function AssignValues(ByRef OldPassword As String, ByRef NewPassword As String) As Boolean
        Dim ObjEncode As New Venus.Shared.Security.ClientCrypt
        Try
            With ObjClsSys_Users


                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
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
                Else

                End If


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

    Private Function Clear() As Boolean
        txtCode.Text = ""
        txtEngName.Text = ""
        txtArbName.Text = ""
        ChkCanDeleteSelfServiceTransactions.Checked = False
        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""

    End Function
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

#End Region


End Class
