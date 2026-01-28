Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmControlsPermission
    Inherits MainPage

#Region "Constant"

    Const uwgEnabled = 3
    Const uwgVisible = 4
    Const uwgReadOnly = 5
    Const uwgId = 6

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim IntID As Integer = Request.QueryString.Item("ID")
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim ClsUsers As New Clssys_Users(Page)
        Dim ClsGroups As New Clssys_Groups(Page)
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        ClsUsers.Find("ID=" & IntID)
        ClsGroups.Find("ID=" & IntID)
        Venus.Shared.Web.ClientSideActions.GridSelection(Page, UwgSearchUsers)
        If Not IsPostBack Then
            Dim ClsForms As New ClsSys_Forms(Page)
            Select Case StrMode
                Case "U"
                    ClsUsers.Find("ID=" & IntID)
                    lblUserCode.Text = ClsUsers.Code
                    lblUserName.Text = IIf(StrLanguage = "en-US", ClsUsers.EngName, ClsUsers.ArbName)
                    lblUser.Text = IIf(StrLanguage = "en-US", "User Code :", "كود المستخدم :")
                    lblName.Text = IIf(StrLanguage = "en-US", "User Name :", "اسم المستخدم :")
                    ClsForms.FindUsersAvailableForms(IntID, DdlForms)
                Case "G"
                    ClsGroups.Find("ID=" & IntID)
                    lblUserCode.Text = ClsGroups.Code
                    lblUserName.Text = IIf(StrLanguage = "en-US", ClsGroups.EngName, ClsGroups.ArbName)
                    lblUser.Text = IIf(StrLanguage = "en-US", "Group Code :", "كود المجموعة :")
                    lblName.Text = IIf(StrLanguage = "en-US", "Group Name :", "اسم المجموعة :")
                    ClsForms.FindGroupsAvailableForms(IntID, DdlForms)
            End Select
            DdlForms.SelectedIndex = 0
            If DdlForms.Items.Count > 0 Then
                SetData(DdlForms.SelectedItem.Value)
            End If

        End If
        chkNotVisible.Attributes.Add("onclick", "UwgSearchCheckColumns(4)")
        chkReadOnly.Attributes.Add("onclick", "UwgSearchCheckColumns(5)")
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_Delete.Command
        Select Case e.CommandArgument
            Case "Save"
      Dim IntID As Integer = Request.QueryString.Item("ID")
                Dim StrMode As String = Request.QueryString.Item("Mode")
                Dim StrFieldName As String = String.Empty
                Dim ObjRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
                Dim ClsFormPermission As New Clssys_FormsControlsPermissions(Page)
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsFormPermission.ConnectionString)
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
                        If .Find(StrFieldName & "=" & IntID & " And FormControlID =" & ObjRow.Cells(uwgId).Value) Then

                            .IsEnabled = IIf(ObjRow.Cells(uwgEnabled).Value = 0, False, True)
                            .IsVisible = IIf(ObjRow.Cells(uwgVisible).Value = 0, False, True)
                            .IsReadOnly = IIf(ObjRow.Cells(uwgReadOnly).Value = 0, False, True)
                            .Update(StrFieldName & "=" & IntID & " And FormControlID =" & ObjRow.Cells(uwgId).Value)

                        Else
                            .FormControlID = ObjRow.Cells(uwgId).Value
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
                            .IsEnabled = IIf(ObjRow.Cells(uwgEnabled).Value = 0, False, True)
                            .IsVisible = IIf(ObjRow.Cells(uwgVisible).Value = 0, False, True)
                            .IsReadOnly = IIf(ObjRow.Cells(uwgReadOnly).Value = 0, False, True)
                            .RegDate = Date.Now
                            .Save()
                        End If
                    End With
                Next
                Venus.Shared.Web.ClientSideActions.ClosePage(Page, ObjNavigationHandler.SetLanguage(Page, "Save Complete Successfully / تم الحفظ بنجاح"))
            Case "Delete"
                Venus.Shared.Web.ClientSideActions.ClosePage(Page)
        End Select
    End Sub
    Protected Sub btnSearchCode_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSearchCode.Click
        If (DdlForms.Items.Count > 0) Then
            SetData(DdlForms.SelectedItem.Value)
        End If
    End Sub
    Private Sub SetData(ByVal FormID As Integer)
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim ObjClsFormsControlsPermission As New Clssys_FormsControlsPermissions(Page)
        Dim ObjDataSet As New System.Data.DataSet
        Select Case StrMode
            Case "U"
                ObjClsFormsControlsPermission.FindUsersFormsControlsPermissions(IntId, FormID, ObjDataSet)
            Case "G"
                ObjClsFormsControlsPermission.FindGroupsFormsControlsPermissions(IntId, FormID, ObjDataSet)
            Case Else
                ObjClsFormsControlsPermission.FindUsersFormsControlsPermissions(IntId, FormID, ObjDataSet)
        End Select
        chkNotVisible.Checked = False
        chkReadOnly.Checked = False
        txtNotVisibleC.Value = ObjDataSet.Tables(0).Select(" IsVisible = 1 ").Length
        txtReadOnlyC.Value = ObjDataSet.Tables(0).Select(" IsReadOnly = 1 ").Length
        txtGridCount.Value = ObjDataSet.Tables(0).Rows.Count
        If txtNotVisibleC.Value = txtGridCount.Value And Val(txtGridCount.Value) <> 0 Then chkNotVisible.Checked = True
        If txtReadOnlyC.Value = txtGridCount.Value And Val(txtGridCount.Value) <> 0 Then chkReadOnly.Checked = True
        UwgSearchUsers.DataSource = ObjDataSet.Tables(0).DefaultView
        UwgSearchUsers.DataBind()
    End Sub
End Class
