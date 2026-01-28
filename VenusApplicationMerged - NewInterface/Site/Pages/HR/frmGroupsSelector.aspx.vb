Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmGroupsSelector
    Inherits MainPage

#Region "       Constant"

    Const uwgCheck = 3
    Const uwgCode = 0

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim ClsUsers As New Clssys_Users(Page)

        Dim Arr(3) As String
        Arr(0) = "txtCode"
        Arr(1) = "txtEngName"
        Arr(2) = "txtArbName"

        Venus.Shared.Web.ClientSideActions.GridSelection(Page, UwgSearchUsers)
        Venus.Shared.Web.ClientSideActions.SetupTabOrder(Page, Arr, Drawing.Color.White, False)
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, False)

        If Not IsPostBack Then
            Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            SetData()
        End If

        chkCheckAll.Attributes.Add("onclick", "UwgSearchCheckColumns(3)")

        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)

        ClsUsers.Find("ID=" & IntID)
        lblUserCode.Text = ClsUsers.Code
        lblUserName.Text = IIf(StrLanguage = "en-US", ClsUsers.EngName, ClsUsers.ArbName)
        lblUser.Text = IIf(StrLanguage = "en-US", "User Code :", "كود المستخدم :")
        lblName.Text = IIf(StrLanguage = "en-US", "User Name :", "اسم المستخدم :")

    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_Delete.Command

        Select Case e.CommandArgument
            Case "Save"
         Dim IntID As Integer = Request.QueryString.Item("ID")
                Dim ObjRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
                Dim ClsUsersGroup As New Clssys_GroupsUsers(Page)
                Dim ClsGroup As New Clssys_Groups(Page)
                Dim intGroupId As Integer = 0
                For Each ObjRow In UwgSearchUsers.Rows
                    If ClsGroup.Find("Code='" & ObjRow.Cells(uwgCode).Value & "'") Then
                        intGroupId = ClsGroup.ID
                    Else
                        intGroupId = 0
                        Continue For
                    End If

                    With ClsUsersGroup
                        If .FindWithoutCancelDate("UserID=" & IntID & " And GroupID=" & intGroupId) Then
                            If ObjRow.Cells(uwgCheck).Value = 0 Then
                                .Delete("UserID=" & IntID & " And GroupID=" & intGroupId)
                            Else
                                .RegDate = Date.Now
                                .CancelDate = Nothing
                                .Update("UserID=" & IntID & " And GroupID=" & intGroupId)
                            End If
                        Else
                            If ObjRow.Cells(uwgCheck).Value = 1 Then
                                .UserID = IntID
                                .GroupID = intGroupId
                                .RegDate = Date.Now
                                .Save()
                            End If
                        End If
                    End With
                Next

                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsGroup.ConnectionString)
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
        Dim ObjClsGroups As New Clssys_Groups(Page)
        Dim ObjDataSet As New System.Data.DataSet
        ObjClsGroups.FindUserGroups(IntId, txtCode.Text, txtEngName.Text, txtArbName.Text, ObjDataSet)
        UwgSearchUsers.DataSource = ObjDataSet.Tables(0).DefaultView
        UwgSearchUsers.DataBind()
        '-------------------------------0257 MODIFIED-----------------------------------------
        For Each Row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchUsers.Rows
            If (Not IsNothing(Row.Cells(4).Value)) Then
                'Row.Cells(0).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                Row.Hidden = True
            End If
        Next
    End Sub
End Class
