Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmSendEmailSetting
    Inherits MainPage
#Region "Public Decleration"
    Private ClsSendEmailSetting As Clssys_SendEmailSetting
    Private clsFixedEmails As Clssys_SendEmailSettingFixedEmails
    Private mErrorHandler As Venus.Shared.ErrorsHandler
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsSendEmailSetting = New Clssys_SendEmailSetting(Me.Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            txtCode.Focus()
            If ClsObjects.Find(" Code='" & ClsSendEmailSetting.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID

                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsSendEmailSetting.ConnectionString)
                ClsSendEmailSetting.AddOnChangeEventToControls("frmSendEmailSetting", Page, UltraWebTab1)
                GetData(0)
                AddNewRow()
                '================================= Exit & Navigation Notification [ End ]

                'Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsSendEmailSetting.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsSendEmailSetting.ID
                If (IntrecordID > 0) Then
                    SetScreenInformation("E")
                Else
                    SetScreenInformation("N")
                End If
            Else
                SetScreenInformation("N")
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
        ClsSendEmailSetting = New Clssys_SendEmailSetting(Me.Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsSendEmailSetting.ConnectionString)
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
                ClsSendEmailSetting.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                If ClsSendEmailSetting.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsSendEmailSetting.ID & "&TableName=" & ClsSendEmailSetting.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                End If
            Case "Remarks"
                If ClsSendEmailSetting.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsSendEmailSetting.ID & "&TableName=" & ClsSendEmailSetting.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                End If

            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"

            Case "Exit"

            Case "First"
                ClsSendEmailSetting.Find("Code='" & txtCode.Text & "'")
                ClsSendEmailSetting.FirstRecord()
                GetValues(ClsSendEmailSetting)
            Case "Previous"
                ClsSendEmailSetting.Find("Code='" & txtCode.Text & "'")
                If Not ClsSendEmailSetting.previousRecord() Then
                    ClsSendEmailSetting.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))

                End If
                GetValues(ClsSendEmailSetting)
            Case "Next"
                ClsSendEmailSetting.Find("Code='" & txtCode.Text & "'")
                If Not ClsSendEmailSetting.NextRecord() Then
                    ClsSendEmailSetting.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))

                End If
                GetValues(ClsSendEmailSetting)
            Case "Last"
                ClsSendEmailSetting.Find("Code='" & txtCode.Text & "'")
                ClsSendEmailSetting.LastRecord()
                GetValues(ClsSendEmailSetting)
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
        GetData(txtCode.Text)
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
    Private Function GetData(SendEmailSettingID As Integer) As Boolean
        Try
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsSendEmailSetting.ConnectionString)

            Dim str As String = " SELECT        ID, SendEmailSettingId, FixedEmail  FROM  sys_SendEmailSettingFixedEmails Where SendEmailSettingId = " & SendEmailSettingID

            Dim ds As New Data.DataSet
            ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsSendEmailSetting.ConnectionString, Data.CommandType.Text, str)

            UwgSearchEmployees.DataSource = Nothing
            UwgSearchEmployees.DataBind()
            UwgSearchEmployees.DataSource = ds.Tables(0)
            UwgSearchEmployees.DataBind()

            UwgSearchEmployees.Rows.Add()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SaveGrid(SendEmailSettingID As Integer) As Boolean
        Try
            Dim ds As Data.DataSet
            Dim str As String = String.Empty
            Dim str1 As String = String.Empty
            Dim ClsSendEmailSettingFixed = New Clssys_SendEmailSettingFixedEmails(Me)

            'str = "set dateformat DMY; UPDATE hrs_HIPolicyContract SET CancelDate = GetDate() Where [PolicyID]=" & HIPolicyID & ";" & vbNewLine
            'Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsSendEmailSetting.ConnectionString, Data.CommandType.Text, str)

            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                If row.Cells.FromKey("ID").Value <> Nothing Then
                    If row.Cells.FromKey("SendEmailSettingId").Value <> Nothing Then
                        ClsSendEmailSettingFixed = New Clssys_SendEmailSettingFixedEmails(Me)

                        str1 = " SELECT        ID, SendEmailSettingId, FixedEmail  FROM  sys_SendEmailSettingFixedEmails Where SendEmailSettingId = " & SendEmailSettingID & " And ID = " & row.Cells.FromKey("ID").Value
                        ds = New Data.DataSet
                        ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsSendEmailSettingFixed.ConnectionString, Data.CommandType.Text, str1)
                        str = "set dateformat DMY; "
                        If ds.Tables(0).Rows.Count > 0 Then
                            str &=
                                    " UPDATE sys_SendEmailSettingFixedEmails SET " &
                                    " [FixedEmail] = " & row.Cells.FromKey("FixedEmail").Value & " WHERE [ID] = " & ds.Tables(0).Rows(0).Item("ID") & " ;" & vbNewLine
                        Else
                            str &= " INSERT INTO [dbo].[sys_SendEmailSettingFixedEmails] ([SendEmailSettingId],[FixedEmail]) VALUES " &
                                    "(" & SendEmailSettingID &
                                    ",'" & row.Cells.FromKey("FixedEmail").Value &
                                    "');" & vbNewLine
                        End If
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsSendEmailSettingFixed.ConnectionString, Data.CommandType.Text, str)


                    End If
                Else
                    str = " INSERT INTO [dbo].[sys_SendEmailSettingFixedEmails] ([SendEmailSettingId],[FixedEmail]) VALUES " &
                                    "(" & SendEmailSettingID &
                                    ",'" & row.Cells.FromKey("FixedEmail").Value &
                                    "');" & vbNewLine
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsSendEmailSettingFixed.ConnectionString, Data.CommandType.Text, str)
                End If


            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        ClsSendEmailSetting = New Clssys_SendEmailSetting(Page)

        Try
            ClsSendEmailSetting.Find("Code='" & txtCode.Text & "'")

            If ClsSendEmailSetting.ID > 0 Then
                If Not AssignValues(ClsSendEmailSetting) Then
                    Exit Function
                End If
                ClsSendEmailSetting.Update("Code='" & txtCode.Text & "'")

            Else
                If Not AssignValues(ClsSendEmailSetting) Then
                    Exit Function
                End If
                ClsSendEmailSetting.Save()

            End If

            SaveGrid(Convert.ToInt32(txtCode.Text))
            ClsSendEmailSetting.Find("Code='" & txtCode.Text & "'")
            value.Text = ""
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function AssignValues(ByRef ClsBasicFiles As Object) As Boolean
        Try
            With ClsSendEmailSetting
                .Code = txtCode.Text
                .FormName = txtFormName.Text
                .SendAfterSave = chkSaveSend.Checked
                .SendAfterDelete = chkDeleteSend.Checked
                .SendAfterUpdate = chkUpdateSend.Checked
                .FromEmail = txtFromEmail.Text
                .ToEmailField = txtToEmail.Text
                .ToTable = txtToTable.Text
                .ToFixedCondition = txtToCondition.Text
                .ToFormCondition = txtFormCondition.Text
                .EmailTitle = txtTitle.Text
                .EmailBody = txtSubject.Text


                '.EngName = txtEngName.Text
                '.ArbName = txtArbName.Text

            End With
            Return True
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsSendEmailSetting.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, "", Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
            Return False
        End Try
    End Function
    Private Function GetValues(ByVal ClsSendEmailSetting As Clssys_SendEmailSetting) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsSendEmailSetting
                txtCode.Text = .Code
                txtFormName.Text = .FormName
                chkSaveSend.Checked = .SendAfterSave
                chkDeleteSend.Checked = .SendAfterDelete
                chkUpdateSend.Checked = .SendAfterUpdate
                txtFromEmail.Text = .FromEmail
                txtToEmail.Text = .ToEmailField
                txtToTable.Text = .ToTable
                txtToCondition.Text = .ToFixedCondition
                txtFormCondition.Text = .ToFormCondition
                txtTitle.Text = .EmailTitle
                txtSubject.Text = .EmailBody


                If Not ClsSendEmailSetting.RegUserId = Nothing Then
                    ClsUser.Find("ID=" & ClsSendEmailSetting.RegUserId)
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
                    ClsSendEmailSetting.Find("ID=" & intID)
                    GetValues(ClsSendEmailSetting)
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsSendEmailSetting.Find("ID=" & intID)
                    GetValues(ClsSendEmailSetting)
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsSendEmailSetting
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
        ClsSendEmailSetting = New Clssys_SendEmailSetting(Me.Page)
        Try
            With ClsSendEmailSetting
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
        ClsSendEmailSetting = New Clssys_SendEmailSetting(Me.Page)
        If IntId > 0 Then
            ClsSendEmailSetting.Find("ID=" & IntId)
            GetValues(ClsSendEmailSetting)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsSendEmailSetting = New Clssys_SendEmailSetting(Me.Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Try
            ClsSendEmailSetting.Find("Code='" & txtCode.Text & "'")
            IntId = ClsSendEmailSetting.ID
            'txtEngName.Focus()
            If ClsSendEmailSetting.ID > 0 Then
                GetValues(ClsSendEmailSetting)
                StrMode = "E"
            Else
                If ClsSendEmailSetting.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If

                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"

            End If
            SetToolBarDefaults()
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
        ClsSendEmailSetting.Clear()
        GetValues(ClsSendEmailSetting)
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


        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsSendEmailSetting = New Clssys_SendEmailSetting(Me.Page)
        ClsSendEmailSetting.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsSendEmailSetting.ID
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


#End Region
End Class
