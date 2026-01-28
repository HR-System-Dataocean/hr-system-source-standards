Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmOtherFields
    Inherits MainPage
#Region "Public Decleration"
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private clsObjects As Clssys_Objects
    Private clsSysFields As Clssys_Fields
    Private clsOtherFields As ClsSys_OtherFields

    Private arr() As String = {"Addresses", "Contacts", "Other Types"}
    Private arrDataTypes() As String = {"Characters", "Numeric", "Boolean", "DateTime"}
    Private arrArb() As String = {"عناوين", "معلومات", "غير ذلك"}
    Private arrDataTypesArb() As String = {"نصية", "رقمية", "اختيارية", "تاريخية"}
    Private arrCodes() As String = {"A", "B", "C"}
    Private arrDataTypesCodes() As Integer = {1, 2, 3, 4}
    Private clsOtherFieldsGroups As ClsSys_OtherFieldsGroups
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ObjectID As String = IIf(Request.QueryString.Item("ObjID") = "", 0, Request.QueryString.Item("ObjID"))
        Dim OtherFieldID As Integer = IIf(Request.QueryString.Item("OtherFieldID") = "", 0, Request.QueryString.Item("OtherFieldID"))
        clsOtherFields = New ClsSys_OtherFields(Me.Page)
        Dim clsOtherFieldsGroups As New ClsSys_OtherFieldsGroups(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsOtherFields.ConnectionString)
        lblMSG.Text = ""
        ClsObjects = New Clssys_Objects(Me.Page)
        Dim GroupID As Integer
        If (ObjectID > 0) Then
            ClsObjects.Find(" ID = " & ObjectID)
            lblTableNameDesc.Text = ClsObjects.Code
        End If
        txtObjectID.Value = ObjectID
        Try
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", clsOtherFields.ConnectionString)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

                uwgOtherFields.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect
                uwgOtherFields.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgOtherFields.DisplayLayout.AllowAddNewDefault = Infragistics.WebUI.UltraWebGrid.AllowAddNew.No

                clsOtherFieldsGroups.GetDropDownList(ddlOtherFieldsGroups, False)
                If ddlOtherFieldsGroups.Items.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "You must enter at least one other fields group/يجب أن تدخل على الأقل مجموعة واحدة للحقول الأخرى"))
                    lblMSG.Text = ObjNavigationHandler.SetLanguage(Page, "You must enter at least one other fields group/يجب أن تدخل على الأقل مجموعة واحدة للحقول الأخرى")
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    Exit Sub
                End If
                clsObjects.GetDropDownList(ddlViewObject, True, " ID <> 0 ")
                If (OtherFieldID > 0) Then
                    If clsOtherFields.Find(" sys_OtherFields.ID=" & OtherFieldID) Then
                        GroupID = clsOtherFields.OtherFieldGroupID
                    End If
                End If
                ddlOtherFieldsGroups.SelectedValue = GroupID
                If ObjectID > 0 Then
                    LoadNavigationGrid()
                    If uwgOtherFields.Rows.Count > 0 And OtherFieldID > 0 Then
                        uwgOtherFields.Rows(0).Activate()
                        uwgOtherFields.Rows(0).Selected = True
                        txtOtherFieldID.Value = clsOtherFields.ID
                        Session("OtherFieldID") = clsOtherFields.ID
                        GetValues(clsOtherFields)
                    Else
                        txtOtherFieldID.Value = 0
                        Session("OtherFieldID") = 0
                    End If
                End If

                txtRowIndex.Value = 0

                ddlDataTypes.DataSource = IIf(ObjNavigationHandler.SetLanguage(Page, "0/1") = 0, arrDataTypes, arrDataTypesArb)
                ddlDataTypes.DataBind()
                ddlFieldType.DataSource = IIf(ObjNavigationHandler.SetLanguage(Page, "0/1") = 0, arr, arrArb)
                ddlFieldType.DataBind()
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                GetFormPermission("frmOtherFields")
                If Val(Session("OtherFieldID")) <= 0 Then
                    txtDataLength.Text = "8000"
                End If
            End If

            If Val(Session("OtherFieldID")) > 0 Then
                SetScreenInformation("E")
                ddlDataTypes.Enabled = False
                txtDataLength.Enabled = False
            Else
                SetScreenInformation("N")
            End If

            ddlOtherFieldsGroups.Attributes.Add("onchange", "")
            txtEngName.Attributes.Add("onchange", "")
            ddlDataTypes.Attributes.Add("onchange", "Change_Data_Lenght()")
            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, clsOtherFields.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_New.Command, ImageButton_Delete.Command
        Dim mode As String = Request.QueryString.Item("Mode")
        clsOtherFields = New ClsSys_OtherFields(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsOtherFields.ConnectionString)
        Dim mOtherFieldID = Session("OtherFieldID")
        Select Case e.CommandArgument
            Case "Save"

                clsOtherFields.Find(" sys_OtherFields.ID = " & mOtherFieldID)
                If clsOtherFields.ID > 0 Then
                    'If (clsOtherFields.Find(" sys_OtherFields.EngName ='" & txtEngName.Text & "' And sys_OtherFields.ArbName='" & txtArbName.Text & "' And sys_OtherFields.ObjectID=" & clsOtherFields.ObjectID)) Then
                    '    lblMSG.Text = ObjNavigationHandler.SetLanguage(Page, "This data is already exist/هذه البيانات موجودة سابقاً")
                    '    Return
                    'Else
                    'clsOtherFields.Find(" sys_OtherFields.ID = " & mOtherFieldID)
                    If Not AssignValue(clsOtherFields) Then
                        Exit Sub
                    End If
                    clsOtherFields.Update(" sys_OtherFields.ID = " & mOtherFieldID)
                    ' End If
                Else
                    If Not AssignValue(clsOtherFields) Then
                        Exit Sub
                    End If
                    clsOtherFields.Save()
                End If
                Clear()
                ddlDataTypes.Enabled = True
                txtDataLength.Enabled = True
                LoadNavigationGrid()

            Case "New"
                Clear()
                ddlDataTypes.Enabled = True
                txtDataLength.Enabled = True
                txtOtherFieldID.Value = 0
                Session("OtherFieldID") = 0
                SetToolBarPermission(Me, clsOtherFields.ConnectionString, clsOtherFields.DataBaseUserRelatedID, clsOtherFields.GroupID, "N")
                ImageButton_Delete.Enabled = False

            Case "Delete"
                If mOtherFieldID > 0 Then
                    clsOtherFields.Delete(" sys_OtherFields.ID = " & mOtherFieldID)
                    LoadNavigationGrid()
                End If
        End Select
    End Sub
    Protected Sub ddlViewObject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlViewObject.SelectedIndexChanged
        clsSysFields = New Clssys_Fields(Page)
        clsSysFields.GetDropDownList(ddlEngFieldView, False, " ObjectID = " & ddlViewObject.SelectedValue)
        clsSysFields.GetDropDownList(ddlArbFieldView, False, " ObjectID = " & ddlViewObject.SelectedValue)
    End Sub
    Protected Sub ddlOtherFieldsGroups_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOtherFieldsGroups.SelectedIndexChanged
        If LoadNavigationGrid() Then
            uwgOtherFields.Rows(0).Activate()
            uwgOtherFields.Rows(0).Selected = True
            txtOtherFieldID.Value = clsOtherFields.ID
            Session("OtherFieldID") = clsOtherFields.ID
            GetValues(clsOtherFields)
        Else
            txtOtherFieldID.Value = 0
            Session("OtherFieldID") = 0
            clsOtherFields.Clear()
            GetValues(clsOtherFields)
            uwgOtherFields.DataSource = Nothing
            uwgOtherFields.DataBind()
        End If
        txtRowIndex.Value = 0
    End Sub
    Protected Sub uwgOtherFields_ActiveRowChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgOtherFields.ActiveRowChange
        If Not (e.Row.Cells.FromKey("ID").Text = Nothing) Then
            clsOtherFields = New ClsSys_OtherFields(Page)
            If clsOtherFields.Find(" sys_OtherFields.ID=" & e.Row.Cells.FromKey("ID").Text) Then
                With clsOtherFields
                    txtOtherFieldID.Value = .ID
                    Session("OtherFieldID") = e.Row.Cells.FromKey("ID").Text
                    txtRank.Text = .Rank
                    txtEngName.Text = .EngName
                    txtArbName.Text = .ArbName
                    ddlFieldType.SelectedIndex = Array.IndexOf(arrCodes, .FieldType.ToString)
                    ddlViewObject.SelectedValue = .ViewObjectID
                    If .ViewObjectID > 0 Then
                        clsSysFields = New Clssys_Fields(Page)
                        clsSysFields.GetDropDownList(ddlEngFieldView, False, " ObjectID = " & .ViewObjectID)
                        clsSysFields.GetDropDownList(ddlArbFieldView, False, " ObjectID = " & .ViewObjectID)
                        ddlEngFieldView.SelectedValue = .ViewEngFieldID
                        ddlArbFieldView.SelectedValue = .ViewArbFieldID
                    Else
                        ddlEngFieldView.Items.Clear()
                        ddlArbFieldView.Items.Clear()
                        ddlEngFieldView.SelectedIndex = -1
                        ddlArbFieldView.SelectedIndex = -1
                    End If
                    ddlDataTypes.SelectedIndex = Array.IndexOf(arrDataTypesCodes, .DataType)
                    txtDataLength.Text = .DataLength
                End With
            Else
                txtOtherFieldID.Value = 0
                Session("OtherFieldID") = 0
            End If
        End If
    End Sub
    'Protected Sub txtEngName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEngName.TextChanged
    '    clsOtherFields = New ClsSys_OtherFields(Page)
    '    If (txtObjectID.Value > 0 And txtEngName.Text <> "") Then
    '        If clsOtherFields.Find(" sys_OtherFields.EngName='" & txtEngName.Text & "' And sys_OtherFields.ObjectID=" & txtObjectID.Value, True) Then
    '            With clsOtherFields
    '                txtOtherFieldID.Value = .ID
    '                Session("OtherFieldID") = .ID
    '                txtRank.Text = .Rank
    '                txtEngName.Text = .EngName
    '                txtArbName.Text = .ArbName
    '                ddlFieldType.SelectedIndex = Array.IndexOf(arrCodes, .FieldType.ToString)
    '                ddlViewObject.SelectedValue = .ViewObjectID
    '                clsSysFields = New Clssys_Fields(Page)
    '                clsSysFields.GetDropDownList(ddlEngFieldView, False, " ObjectID = " & .ViewObjectID)
    '                clsSysFields.GetDropDownList(ddlArbFieldView, False, " ObjectID = " & .ViewObjectID)
    '                ddlEngFieldView.SelectedValue = .ViewEngFieldID
    '                ddlArbFieldView.SelectedValue = .ViewArbFieldID
    '                ddlDataTypes.SelectedIndex = Array.IndexOf(arrDataTypesCodes, .DataType)
    '                txtDataLength.Text = .DataLength
    '            End With
    '        Else
    '            txtOtherFieldID.Value = 0
    '            Session("OtherFieldID") = 0
    '        End If
    '    End If
    'End Sub

#End Region

#Region "Private Functions"
    Private Function LoadNavigationGrid() As Boolean
        clsOtherFields = New ClsSys_OtherFields(Page)
        If (clsOtherFields.Find(" sys_OtherFields.OtherFieldGroupID=" & ddlOtherFieldsGroups.SelectedValue & IIf(txtObjectID.Value = "", "", " And sys_OtherFields.ObjectID=" & txtObjectID.Value))) Then
            uwgOtherFields.Columns.FromKey("DataType").ValueList.ValueListItems.Add(1, "Charachters")
            uwgOtherFields.Columns.FromKey("DataType").ValueList.ValueListItems.Add(2, "Numeric")
            uwgOtherFields.Columns.FromKey("DataType").ValueList.ValueListItems.Add(3, "Boolean")
            uwgOtherFields.Columns.FromKey("DataType").ValueList.ValueListItems.Add(4, "DateTime")
            Dim RowOtherField As Data.DataRow
            For Each RowOtherField In clsOtherFields.DataSet.Tables(0).Rows
                If RowOtherField("FieldType") = "A" Then
                    RowOtherField("FieldType") = "Addresses"
                End If
                If RowOtherField("FieldType") = "B" Then
                    RowOtherField("FieldType") = "Contacts"
                End If
                If RowOtherField("FieldType") = "C" Then
                    RowOtherField("FieldType") = "Other Types"
                End If
            Next
            uwgOtherFields.DataSource = Nothing
            uwgOtherFields.DataBind()

            uwgOtherFields.DataSource = clsOtherFields.DataSet.Tables(0).DefaultView
            uwgOtherFields.DataBind()
            Return True
        Else
            Return False
        End If
    End Function
    Private Function Clear() As Boolean
        txtOtherFieldID.Value = 0
        Session("OtherFieldID") = 0
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        txtRank.Text = "0"
        txtDataLength.Text = "8000"
        ddlArbFieldView.SelectedIndex = -1
        ddlEngFieldView.SelectedIndex = -1
        ddlFieldType.SelectedIndex = 0
        ddlViewObject.SelectedIndex = 0
        ddlDataTypes.SelectedIndex = 0
    End Function
    Private Function AssignValue(ByRef ClsOtherFields As ClsSys_OtherFields) As Boolean
        Dim Mode As String = Request.QueryString.Item("Mode")
        Try
            With ClsOtherFields
                .ObjectID = txtObjectID.Value
                .OtherFieldGroupID = ddlOtherFieldsGroups.SelectedItem.Value
                .Rank = Val(txtRank.Text)
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .FieldType = arrCodes(ddlFieldType.SelectedIndex)
                .ViewObjectID = ddlViewObject.SelectedItem.Value
                If ddlViewObject.SelectedItem.Value > 0 Then
                    .ViewEngFieldID = ddlEngFieldView.SelectedItem.Value
                    .ViewArbFieldID = ddlArbFieldView.SelectedItem.Value
                Else
                    .ViewEngFieldID = 0
                    .ViewArbFieldID = 0
                End If


                .DataType = arrDataTypesCodes(ddlDataTypes.SelectedIndex)
                .DataLength = IIf(txtDataLength.Text.Trim = String.Empty, 0, txtDataLength.Text)
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetValues(ByRef ClsOtherFields As ClsSys_OtherFields) As Boolean
        Dim StrMode As String = ""
        Try
            SetToolBarDefaults()
            With ClsOtherFields
                If (.OtherFieldGroupID > 0) Then
                    ddlOtherFieldsGroups.SelectedValue = .OtherFieldGroupID
                End If
                txtRank.Text = .Rank
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                ddlFieldType.SelectedIndex = Array.IndexOf(arrCodes, .FieldType.ToString)
                ddlViewObject.SelectedValue = .ViewObjectID
                If .ViewObjectID > 0 Then
                    clsSysFields = New Clssys_Fields(Page)
                    clsSysFields.GetDropDownList(ddlEngFieldView, False, " ObjectID = " & .ViewObjectID)
                    clsSysFields.GetDropDownList(ddlArbFieldView, False, " ObjectID = " & .ViewObjectID)
                    ddlEngFieldView.SelectedValue = .ViewEngFieldID
                    ddlArbFieldView.SelectedValue = .ViewArbFieldID
                Else
                    ddlEngFieldView.Items.Clear()
                    ddlArbFieldView.Items.Clear()
                    ddlEngFieldView.SelectedIndex = -1
                    ddlArbFieldView.SelectedIndex = -1
                End If
                ddlEngFieldView.SelectedValue = .ViewEngFieldID
                ddlArbFieldView.SelectedValue = .ViewArbFieldID
                ddlDataTypes.SelectedIndex = Array.IndexOf(arrDataTypesCodes, .DataType)
                If Session("OtherFieldID") = 0 Then
                    txtDataLength.Text = "8000"
                Else
                    txtDataLength.Text = .DataLength
                End If



            End With
            If (ClsOtherFields.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsOtherFields.ConnectionString, ClsOtherFields.DataBaseUserRelatedID, ClsOtherFields.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsOtherFields.ConnectionString, ClsOtherFields.DataBaseUserRelatedID, ClsOtherFields.GroupID, ClsOtherFields.Table, ClsOtherFields.ID)
            If Not ClsOtherFields.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
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

                    If ImageButton_Delete.Enabled = True And .Item("CanDelete") = True Then
                        ImageButton_Delete.Enabled = Not .Item("CanDelete")
                    End If

                End With
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With clsOtherFields
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
        clsOtherFields = New ClsSys_OtherFields(Me)
        If IntId > 0 Then
            clsOtherFields.Find("ID=" & IntId)
            GetValues(clsOtherFields)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_Delete.Enabled = True
    End Function
    Private Function GetFormPermission(ByVal frmCode As String) As Boolean
        Dim ClsForms As New ClsSys_Forms(Page)
        Dim ClsFormPermission As New ClsSys_FormsPermissions(Page)
        Dim StrFormPermission As String = "1,1,1"
        If ClsForms.Find(" Code='" & frmCode & "'") Then
            ClsFormPermission.Find("FormID=" & ClsForms.ID)
            With ClsFormPermission
                If .ID > 0 Then
                    StrFormPermission = ""
                    If .AllowEdit Then
                        StrFormPermission = "0"
                    Else
                        StrFormPermission = "1"
                    End If
                    If .AllowDelete Then
                        StrFormPermission &= ",0"
                    Else
                        StrFormPermission &= ",1"
                    End If

                    If .AllowPrint Then
                        StrFormPermission &= ",0"
                    Else
                        StrFormPermission &= ",1"
                    End If
                End If
            End With
        End If
        txtFormPermission.Value = StrFormPermission
    End Function


#End Region


End Class
