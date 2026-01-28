Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class DocumentsListItems
    Inherits MainPage
#Region "Public Decleration"
    Dim ClsDwfListItems As ClsDwf_ListItems
    Private clsMainOtherFields As clsSys_MainOtherFields

    'Private ClsDwfListItemsRules As ClsDwf_ListItemsRules

    Const CsPaidatVacation = 4
    Const CsOnceatPeriod = 5
    Const CsMaxValue = 3
    Const CsMinValue = 2
    Const CsIntervalID = 6
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsDwfListItems = New ClsDwf_ListItems(Me)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsDwfListItems.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsDwfListItems.ConnectionString)
                ClsDwfListItems.AddOnChangeEventToControls("DocumentsListItems", Page, UltraWebTab1)

                '================================= Exit & Navigation Notification [ End ]
                Setsetting(0)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, WebTextEdit1, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

                uwgEndOfServiceRules.Rows.Clear()
                uwgEndOfServiceRules.Rows.Add()
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsDwfListItems.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsDwfListItems.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsDwfListItems.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsDwfListItems = New ClsDwf_ListItems(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDwfListItems.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                SavePart()
                AfterOperation()
            Case "Save"
                SavePart()
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsDwfListItems.Find("Code='" & txtCode.Text & "'")
                If (ClsDwfListItems.ID > 0) Then
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, " List Item Not Found ")
                    Exit Sub
                End If
                ClsDwfListItems.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsDwfListItems.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsDwfListItems.ID & "&TableName=" & ClsDwfListItems.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsDwfListItems.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsDwfListItems.ID & "&TableName=" & ClsDwfListItems.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsDwfListItems.Table
                ClsDwfListItems.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsDwfListItems.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsDwfListItems.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsDwfListItems.Find(" Code= '" & txtCode.Text & "'")
                If ClsDwfListItems.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsDwfListItems.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsDwfListItems.CheckDiff(ClsDwfListItems, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsDwfListItems.FirstRecord()
                GetValues()
            Case "Previous"
                ClsDwfListItems.Find("Code='" & txtCode.Text & "'")
                If Not ClsDwfListItems.previousRecord() Then
                    ClsDwfListItems.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsDwfListItems.Find("Code='" & txtCode.Text & "'")
                If Not ClsDwfListItems.NextRecord() Then
                    ClsDwfListItems.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsDwfListItems.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function AssignValues() As Boolean
        Try
            With ClsDwfListItems
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .Type = DropDownList_Type.SelectedValue
                .DataSource = TextBox_DataSource.Text
                .ValueMember = TextBox_ValueMember.Text
                .Ar_DisplayMember = TextBox_Ar_DisplayMember.Text
                .En_DisplayMember = TextBox_En_DisplayMember.Text
            End With

            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        ClsDwfListItems = New ClsDwf_ListItems(Page)
        'ClsDwfListItemsRules = New ClsDwf_ListItemsRules(Page)
        Try
            ClsDwfListItems.Find("Code='" & txtCode.Text & "'")
            If ClsDwfListItems.ID > 0 Then
                If Not AssignValues() Then
                    Exit Function
                End If
                ClsDwfListItems.Update("Code='" & txtCode.Text & "'")
                Dim str As String = "delete from Dwf_ListItemsValues where ListItemID = " & ClsDwfListItems.ID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsDwfListItems.ConnectionString, Data.CommandType.Text, str)
                If (SaveDG(ClsDwfListItems.ID)) Then
                    ClsDwfListItems = New ClsDwf_ListItems(Page)
                    clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                    ClsDwfListItems.Find("Code='" & txtCode.Text & "'")
                    clsMainOtherFields.CollectDataAndSave(value.Text, ClsDwfListItems.Table, ClsDwfListItems.ID)
                    value.Text = ""

                End If
            Else
                If Not AssignValues() Then
                    Exit Function
                End If
                ClsDwfListItems.Save()
                ClsDwfListItems.Find("Code='" & txtCode.Text & "'")
                If ClsDwfListItems.ID > 0 Then
                    If (SaveDG(ClsDwfListItems.ID)) Then
                        ClsDwfListItems = New ClsDwf_ListItems(Page)
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        ClsDwfListItems.Find("Code='" & txtCode.Text & "'")
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsDwfListItems.Table, ClsDwfListItems.ID)
                        value.Text = ""
                    End If
                End If
                End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsDwfListItems.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function SaveDG(ByVal ListItemID As Integer) As Boolean
        ClsDwfListItems = New ClsDwf_ListItems(Page)
        Try
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEndOfServiceRules.Rows
                If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                    Continue For
                End If
                Dim str As String = "insert into Dwf_ListItemsValues (Code,EngName,ArbName,ListItemID,RegUserID,RegDate) values ('" & DGRow.Index.ToString() + ListItemID.ToString() & "','" & DGRow.Cells(1).Value & "','" & DGRow.Cells(2).Value & "'," & ListItemID & "," & ClsDwfListItems.DataBaseUserRelatedID & ",getdate())"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsDwfListItems.ConnectionString, Data.CommandType.Text, str)
            Next
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDwfListItems.ConnectionString)
        Try
            SetToolBarDefaults()

            With ClsDwfListItems
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName

                DropDownList_Type.SelectedValue = .Type
                TextBox_DataSource.Text = .DataSource
                TextBox_ValueMember.Text = .ValueMember
                TextBox_Ar_DisplayMember.Text = .Ar_DisplayMember
                TextBox_En_DisplayMember.Text = .En_DisplayMember

                Dim str As String = "select * from Dwf_ListItemsValues where ListItemID = " & ClsDwfListItems.ID
                uwgEndOfServiceRules.DataSource = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsDwfListItems.ConnectionString, Data.CommandType.Text, str).Tables(0).DefaultView
                uwgEndOfServiceRules.DataBind()

            End With
            If Not ClsDwfListItems.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsDwfListItems.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsDwfListItems.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsDwfListItems.RegDate).Date
            End If
            If ClsDwfListItems.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsDwfListItems.CancelDate).Date
            End If

            Dim item As New System.Web.UI.WebControls.ListItem()


            If (ClsDwfListItems.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsDwfListItems.ConnectionString, ClsDwfListItems.DataBaseUserRelatedID, ClsDwfListItems.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsDwfListItems.ConnectionString, ClsDwfListItems.DataBaseUserRelatedID, ClsDwfListItems.GroupID, ClsDwfListItems.Table, ClsDwfListItems.ID)
            If Not ClsDwfListItems.CancelDate = Nothing Then

            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsDwfListItems.ID)
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

                Case "D"
                    ClsDwfListItems.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False

                Case "E"
                    ClsDwfListItems.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True

            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsDwfListItems
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
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
        ClsDwfListItems = New ClsDwf_ListItems(Me)
        If IntId > 0 Then
            ClsDwfListItems.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsDwfListItems = New ClsDwf_ListItems(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDwfListItems.ConnectionString)
        Try
            ClsDwfListItems.Find("Code='" & txtCode.Text & "'")
            IntId = ClsDwfListItems.ID
            txtEngName.Focus()
            If ClsDwfListItems.ID > 0 Then
                GetValues()
                StrMode = "E"
                uwgEndOfServiceRules.Rows.Add()
            Else
                If ClsDwfListItems.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsDwfListItems.ConnectionString, ClsDwfListItems.DataBaseUserRelatedID, ClsDwfListItems.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsDwfListItems.ConnectionString, ClsDwfListItems.DataBaseUserRelatedID, ClsDwfListItems.GroupID, ClsDwfListItems.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then

            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        ClsDwfListItems.Clear()
        GetValues()
        DropDownList_Type.SelectedValue = 1
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        uwgEndOfServiceRules.Rows.Clear()
        uwgEndOfServiceRules.Rows.Add()
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""

        DropDownList_Type.SelectedValue = 1
        TextBox_DataSource.Text = ""
        TextBox_ValueMember.Text = ""
        TextBox_Ar_DisplayMember.Text = ""
        TextBox_En_DisplayMember.Text = ""
    End Function
    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsDwfListItems.Table) = True Then
            Dim StrTablename As String
            ClsDwfListItems = New ClsDwf_ListItems(Me)
            StrTablename = ClsDwfListItems.Table
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

End Class
