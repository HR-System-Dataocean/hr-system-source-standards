Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmFormsPermission
    Inherits MainPage

#Region "Constant"

    Const uwgAdd = 4
    Const uwgEdit = 5
    Const uwgDelete = 6
    Const uwgView = 7
    Const uwgPrint = 8
    Const uwgId = 9

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Arr(3) As String
        Dim ClsUsers As New Clssys_Users(Page)
        Dim ClsGroups As New Clssys_Groups(Page)
        Dim IntID As Integer = Request.QueryString.Item("ID")
        Dim StrMode As String = Request.QueryString.Item("Mode")

        Dim ClsDataAccessLayer As New ClsDataAcessLayer(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDataAccessLayer.ConnectionString)

        Arr(0) = "txtCode"
        Arr(1) = "txtEngName"
        Arr(2) = "txtArbName"

        Venus.Shared.Web.ClientSideActions.GridSelection(Page, UwgSearchUsers)
        Venus.Shared.Web.ClientSideActions.SetupTabOrder(Page, Arr, Drawing.Color.White, False)
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, False)

        chkAllowAdd.Attributes.Add("onclick", "UwgSearchCheckColumns(4)")
        chkAllowEdit.Attributes.Add("onclick", "UwgSearchCheckColumns(5)")
        chkAllowDelete.Attributes.Add("onclick", "UwgSearchCheckColumns(6)")
        chkAllowView.Attributes.Add("onclick", "UwgSearchCheckColumns(7)")
        chkAllowPrint.Attributes.Add("onclick", "UwgSearchCheckColumns(8)")
        chkCheckAll.Attributes.Add("onclick", "UwgSearchCheckColumns(10)")

        If Not IsPostBack Then
            Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

            Dim WebHandler As New Venus.Shared.Web.WebHandler
            Dim StrLanguage As String = String.Empty
            WebHandler.GetCookies(Page, "Lang", StrLanguage)
            Dim _culture As String = StrLanguage
            'Use this
            If (_culture <> "en-US") Then
                UwgSearchUsers.Columns.Item(3).BaseColumnName = "ParentArbName"
            Else
                UwgSearchUsers.Columns.Item(3).BaseColumnName = "ParentEngName"
            End If
            SetData()


            Select Case StrMode
                Case "U"
                    ClsUsers.Find("ID=" & IntID)
                    lblUserCode.Text = ClsUsers.Code
                    lblUserName.Text = IIf(StrLanguage = "en-US", ClsUsers.EngName, ClsUsers.ArbName)
                    lblUser.Text = IIf(StrLanguage = "en-US", "User Code :", "كود المستخدم :")
                    lblName.Text = IIf(StrLanguage = "en-US", "User Name :", "اسم المستخدم :")
                Case "G"
                    ClsGroups.Find("ID=" & IntID)
                    lblUserCode.Text = ClsGroups.Code
                    lblUserName.Text = IIf(StrLanguage = "en-US", ClsGroups.EngName, ClsGroups.ArbName)
                    lblUser.Text = IIf(StrLanguage = "en-US", "Group Code :", "كود المجموعة :")
                    lblName.Text = IIf(StrLanguage = "en-US", "Group Name :", "اسم المجموعة :")
            End Select
        End If
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_Delete.Command

        Select Case e.CommandArgument
            Case "Save"
                Dim IntID As Integer = Request.QueryString.Item("ID")
                Dim StrMode As String = Request.QueryString.Item("Mode")
                Dim StrFieldName As String = String.Empty

                Dim ObjRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
                Dim ClsFormPermission As New ClsSys_FormsPermissions(Page)


                Select Case StrMode
                    Case "U"
                        StrFieldName = "UserID"
                    Case "G"
                        StrFieldName = "GroupID"
                    Case Else
                        StrFieldName = "UserID"
                End Select

                For Each ObjRow In UwgSearchUsers.Rows

                    With ClsFormPermission
                        ClsFormPermission.DeleteAll(StrFieldName & "=" & IntID & " And FormID =" & ObjRow.Cells(uwgId).Value)

                        .FormID = ObjRow.Cells(uwgId).Value
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

                        .AllowAdd = IIf(ObjRow.Cells(uwgAdd).Value = 0, False, True)
                        .AllowDelete = IIf(ObjRow.Cells(uwgDelete).Value = 0, False, True)
                        .AllowEdit = IIf(ObjRow.Cells(uwgEdit).Value = 0, False, True)
                        .AllowPrint = IIf(ObjRow.Cells(uwgPrint).Value = 0, False, True)
                        .AllowView = IIf(ObjRow.Cells(uwgView).Value = 0, False, True)

                        .RegDate = Date.Now
                        .Save()
                    End With
                Next
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsFormPermission.ConnectionString)
                Venus.Shared.Web.ClientSideActions.ClosePage(Page, ObjNavigationHandler.SetLanguage(Page, "Save Complete Successfully / تم الحفظ بنجاح"))

            Case "Delete"
                Venus.Shared.Web.ClientSideActions.ClosePage(Page)
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        SetData()
    End Sub
    Protected Sub btnSearchCode_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSearchCode.Click
        SetData()
    End Sub
    Private Sub SetData()

        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim StrMode As String = Request.QueryString.Item("Mode")

        Dim ObjClsGroups As New Clssys_Forms(Page)
        Dim ObjDataSet As New System.Data.DataSet
        Dim ObjTempDataSet As Data.DataSet

        Select Case StrMode
            Case "U"
                ObjClsGroups.FindUsersFormPermission(IntId, txtCode.Text, txtEngName.Text, txtArbName.Text, ObjDataSet)
                ObjTempDataSet = ObjClsGroups.GetUsersFormsPermissionsCounts(IntId, txtCode.Text, txtEngName.Text, txtArbName.Text)

            Case "G"
                ObjClsGroups.FindGroupsFormPermission(IntId, txtCode.Text, txtEngName.Text, txtArbName.Text, ObjDataSet)
                ObjTempDataSet = ObjClsGroups.GetGroupsFormsPermissionsCounts(IntId, txtCode.Text, txtEngName.Text, txtArbName.Text)
            Case Else
                ObjClsGroups.FindUsersFormPermission(IntId, txtCode.Text, txtEngName.Text, txtArbName.Text, ObjDataSet)
                ObjTempDataSet = ObjClsGroups.GetUsersFormsPermissionsCounts(IntId, txtCode.Text, txtEngName.Text, txtArbName.Text)
        End Select

        chkAllowAdd.Checked = False
        chkAllowDelete.Checked = False
        chkAllowEdit.Checked = False
        chkAllowPrint.Checked = False
        chkAllowView.Checked = False
        chkCheckAll.Checked = False


        If ObjTempDataSet.Tables(0).Rows.Count > 0 Then
            txtAddC.Value = ObjTempDataSet.Tables(0).Rows(0).Item(0)
            txtEditC.Value = ObjTempDataSet.Tables(0).Rows(1).Item(0)
            txtDeleteC.Value = ObjTempDataSet.Tables(0).Rows(2).Item(0)
            txtViewC.Value = ObjTempDataSet.Tables(0).Rows(3).Item(0)
            txtPrintC.Value = ObjTempDataSet.Tables(0).Rows(4).Item(0)
            txtCheckAllC.Value = ObjTempDataSet.Tables(0).Rows(5).Item(0)
            txtGridCount.Value = ObjTempDataSet.Tables(0).Rows(6).Item(0)
            txtGridCountAll.Value = ObjDataSet.Tables(0).Rows.Count
            If txtAddC.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkAllowAdd.Checked = True
            If txtEditC.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkAllowEdit.Checked = True
            If txtDeleteC.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkAllowDelete.Checked = True
            If txtViewC.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkAllowView.Checked = True
            If txtPrintC.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkAllowPrint.Checked = True
            If txtCheckAllC.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkCheckAll.Checked = True
        End If

        UwgSearchUsers.DataSource = ObjDataSet.Tables(0).DefaultView
        UwgSearchUsers.DataBind()

    End Sub
End Class
