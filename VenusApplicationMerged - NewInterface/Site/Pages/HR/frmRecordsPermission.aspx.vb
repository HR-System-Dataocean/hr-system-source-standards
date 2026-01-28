Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmRecordsPermission
    Inherits MainPage

#Region "Constant"

    Const uwgCantEdit = 4
    Const uwgCantDelete = 5
    Const uwgCantView = 6
    Const uwgCantPrint = 7
    Const uwgIsSpacific = 8
    Const uwgId = 0
    Const cIntialTable = 0
    Const uwgCheckAll = 9

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim IntID As Integer = Request.QueryString.Item("ID")
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim ClsUsers As New Clssys_Users(Page)
        Dim ClsGroups As New Clssys_Groups(Page)
        Dim ClsObjects As New Clssys_Objects(Page)

        Dim ClsDataAccessLayer As New ClsDataAcessLayer(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDataAccessLayer.ConnectionString)

        Dim Arr(3) As String
        Arr(0) = "txtCode"
        Arr(1) = "txtEngName"
        Arr(2) = "txtArbName"

        Venus.Shared.Web.ClientSideActions.GridSelection(Page, UwgSearchUsers)
        Venus.Shared.Web.ClientSideActions.SetupTabOrder(Page, Arr, Drawing.Color.White, False)
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, False)

        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)

        Select Case StrMode
            Case "U"
                ClsUsers.Find("ID=" & IntID)
                lblUserCode.Text = ClsUsers.Code
                lblUserName.Text = IIf(StrLanguage = "en-US", ClsUsers.EngName, ClsUsers.ArbName)
                lblUser.Text = IIf(StrLanguage = "en-US", "User Code :", "كود المستخدم :")
                lblName.Text = IIf(StrLanguage = "en-US", "User Name :", "اسم المستخدم :")
                If ClsUsers.DenyAccessforall Then
                    UwgSearchUsers.Columns(uwgCantEdit).Header.Caption = IIf(StrLanguage = "en-US", "Can Edit", "تعديل")
                    UwgSearchUsers.Columns(uwgCantDelete).Header.Caption = IIf(StrLanguage = "en-US", "Can Delete", "حذف")
                    UwgSearchUsers.Columns(uwgCantPrint).Header.Caption = IIf(StrLanguage = "en-US", "Can Print", "طباعة")
                    UwgSearchUsers.Columns(uwgCantView).Header.Caption = IIf(StrLanguage = "en-US", "Can View ", "عرض")
                End If

            Case "G"
                ClsGroups.Find("ID=" & IntID)
                lblUserCode.Text = ClsGroups.Code
                lblUserName.Text = IIf(StrLanguage = "en-US", ClsGroups.EngName, ClsGroups.ArbName)
                lblUser.Text = IIf(StrLanguage = "en-US", "Group Code :", "كود المجموعة :")
                lblName.Text = IIf(StrLanguage = "en-US", "Group Name :", "اسم المجموعة :")

        End Select
        chkAllowEdit.Attributes.Add("onclick", "UwgSearchCheckColumns(" & uwgCantEdit & ")")
        chkIsSpecific.Attributes.Add("onclick", "UwgSearchCheckColumns(" & uwgIsSpacific & ")")
        chkAllowDelete.Attributes.Add("onclick", "UwgSearchCheckColumns(" & uwgCantDelete & ")")
        chkAllowView.Attributes.Add("onclick", "UwgSearchCheckColumns(" & uwgCantView & ")")
        chkAllowPrint.Attributes.Add("onclick", "UwgSearchCheckColumns(" & uwgCantPrint & ")")
        chkCheckAll.Attributes.Add("onclick", "UwgSearchCheckColumns(" & uwgCheckAll & ")")

        If Not IsPostBack Then
            Dim ClsRecordsPermission As New ClsSys_RecordsPermissions(Page)
            ClsObjects.GetDropDownList2(DdlTables, False, " CancelDate is Null ")

        End If
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_Delete.Command

        Select Case e.CommandArgument
            Case "Save"
                Dim IntID As Integer = Request.QueryString.Item("ID")
                Dim StrMode As String = Request.QueryString.Item("Mode")
                Dim StrFieldName As String = String.Empty
                Dim ObjRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
                Dim ClsRecordsPermission As New ClsSys_RecordsPermissions(Page)

                Select Case StrMode
                    Case "U"
                        StrFieldName = "UserID"
                    Case "G"
                        StrFieldName = "GroupID"
                    Case Else
                        StrFieldName = "UserID"
                End Select

                For Each ObjRow In UwgSearchUsers.Rows

                    With ClsRecordsPermission

                        If .Find(StrFieldName & "=" & IntID & " And RecordID =" & ObjRow.Cells(uwgId).Value & " And ObjectID =" & DdlTables.SelectedItem.Value.ToString) Then

                            .CanView = IIf(ObjRow.Cells(uwgCantView).Value = 0, False, True)
                            .CanEdit = IIf(ObjRow.Cells(uwgCantEdit).Value = 0, False, True)
                            .CanDelete = IIf(ObjRow.Cells(uwgCantDelete).Value = 0, False, True)
                            .CanPrint = IIf(ObjRow.Cells(uwgCantPrint).Value = 0, False, True)
                            .IsSpacific = IIf(ObjRow.Cells(uwgIsSpacific).Value = 0, False, True)
                            .Update(StrFieldName & "=" & IntID & " And RecordID =" & ObjRow.Cells(uwgId).Value & " And ObjectID =" & DdlTables.SelectedItem.Value.ToString)

                        Else
                            Select Case StrMode
                                Case "U"
                                    .UserID = IntID
                                    .GroupID = Nothing
                                Case "G"
                                    .UserID = Nothing
                                    .GroupID = IntID
                                Case Else
                                    .UserID = IntID
                                    .GroupID = Nothing
                            End Select

                            .RecordID = ObjRow.Cells(uwgId).Value
                            .ObjectID = DdlTables.SelectedItem.Value

                            .CanView = IIf(ObjRow.Cells(uwgCantView).Value = 0, False, True)
                            .CanEdit = IIf(ObjRow.Cells(uwgCantEdit).Value = 0, False, True)
                            .CanDelete = IIf(ObjRow.Cells(uwgCantDelete).Value = 0, False, True)
                            .CanPrint = IIf(ObjRow.Cells(uwgCantPrint).Value = 0, False, True)
                            .IsSpacific = IIf(ObjRow.Cells(uwgIsSpacific).Value = 0, False, True)

                            .RegDate = Date.Now
                            .Save()

                        End If

                    End With
                Next
                Venus.Shared.Web.ClientSideActions.ClosePage(Page, "Save Complete Successfully")
            Case "Delete"
                Venus.Shared.Web.ClientSideActions.ClosePage(Page)
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        SetData(DdlTables.SelectedItem.Value)
    End Sub
    Protected Sub btnSearchCode_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSearchCode.Click
        SetData(DdlTables.SelectedItem.Value)
    End Sub
    Private Sub SetData(ByVal TableID As Integer)
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim BolOperation As Boolean = False

        UwgSearchUsers.Rows.Clear(True)

        Dim ObjClsRecordsPermission As New ClsSys_RecordsPermissions(Page)
        Dim ObjDataSet As New System.Data.DataSet

        Select Case StrMode
            Case "U"
                If ObjClsRecordsPermission.FindUsersRecordsPermission(IntId, TableID, txtCode.Text, ObjDataSet) Then
                    BolOperation = True
                Else
                    BolOperation = False
                End If
            Case "G"
                If ObjClsRecordsPermission.FindGroupsRecordsPermission(IntId, TableID, txtCode.Text, ObjDataSet) Then
                    BolOperation = True
                Else
                    BolOperation = False
                End If
            Case Else
                If ObjClsRecordsPermission.FindUsersRecordsPermission(IntId, TableID, txtCode.Text, ObjDataSet) Then
                    BolOperation = True
                Else
                    BolOperation = False
                End If
        End Select

        txtEditC.Value = ObjDataSet.Tables(0).Select(" CantEdit = 1 ").Length
        txtDeleteC.Value = ObjDataSet.Tables(0).Select(" CantDelete = 1 ").Length
        txtViewC.Value = ObjDataSet.Tables(0).Select(" CantView = 1 ").Length
        txtPrintC.Value = ObjDataSet.Tables(0).Select(" CantPrint = 1 ").Length
        txtIsSpecificC.Value = ObjDataSet.Tables(0).Select(" IsSpacific = 1 ").Length
        txtGridCount.Value = ObjDataSet.Tables(0).Select(" CheckAll=1 ").Length
        txtGridCountAll.Value = ObjDataSet.Tables(0).Rows.Count


        chkAllowDelete.Checked = False
        chkAllowEdit.Checked = False
        chkAllowPrint.Checked = False
        chkAllowView.Checked = False
        chkCheckAll.Checked = False
        chkIsSpecific.Checked = False

        If txtIsSpecificC.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkIsSpecific.Checked = True
        If txtEditC.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkAllowEdit.Checked = True
        If txtDeleteC.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkAllowDelete.Checked = True
        If txtViewC.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkAllowView.Checked = True
        If txtPrintC.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkAllowPrint.Checked = True
        If txtGridCount.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkCheckAll.Checked = True
        If BolOperation Then
            UwgSearchUsers.DataSource = ObjDataSet.Tables(cIntialTable).DefaultView
            UwgSearchUsers.DataBind()
            UwgSearchUsers.Focus()
            If UwgSearchUsers.Rows.Count > 0 Then
                UwgSearchUsers.Rows(0).Activate()
                UwgSearchUsers.Rows(0).Cells(1).Activate()

            End If
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "This Table is system Table please select another table")
        End If
    End Sub
End Class
