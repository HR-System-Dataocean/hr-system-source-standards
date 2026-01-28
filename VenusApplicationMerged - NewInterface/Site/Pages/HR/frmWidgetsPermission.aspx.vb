Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmWidgetsPermission
    Inherits MainPage

#Region "Constant"

    Const uwgCode = 0
    Const uwgEngName = 1
    Const uwgArbName = 2
    Const uwgView = 3
    Const uwgId = 4

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

        If Not IsPostBack Then
            Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            SetData()
        End If

        chkCheckAll.Attributes.Add("onclick", "UwgSearchCheckColumns(3)")

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
            Case "G"
                ClsGroups.Find("ID=" & IntID)
                lblUserCode.Text = ClsGroups.Code
                lblUserName.Text = IIf(StrLanguage = "en-US", ClsGroups.EngName, ClsGroups.ArbName)
                lblUser.Text = IIf(StrLanguage = "en-US", "Group Code :", "كود المجموعة :")
                lblName.Text = IIf(StrLanguage = "en-US", "Group Name :", "اسم المجموعة :")
        End Select

    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_Delete.Command

        Select Case e.CommandArgument
            Case "Save"
                Dim IntID As Integer = Request.QueryString.Item("ID")
                Dim StrMode As String = Request.QueryString.Item("Mode")
                Dim StrFieldName As String = String.Empty

                Dim ObjRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
                Dim ClsWidgetPermission As New Clsdsh_WidgetsPermissions(Page)


                Select Case StrMode
                    Case "U"
                        StrFieldName = "UserID"
                End Select

                For Each ObjRow In UwgSearchUsers.Rows

                    With ClsWidgetPermission

                        If .Find(StrFieldName & "=" & IntID & " And WidgetsID =" & ObjRow.Cells(uwgId).Value) Then

                            .CanView = IIf(ObjRow.Cells(uwgView).Value = 0, False, True)
                            .Update(StrFieldName & "=" & IntID & " And WidgetsID =" & ObjRow.Cells(uwgId).Value)

                        Else

                            .WidgetsID = ObjRow.Cells(uwgId).Value
                            Select Case StrMode
                                Case "U"
                                    .UserID = IntID
                            End Select


                            .CanView = IIf(ObjRow.Cells(uwgView).Value = 0, False, True)
                            .RegDate = Date.Now
                            .Save()

                        End If

                    End With
                Next
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsWidgetPermission.ConnectionString)
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

        Dim ObjClsGroups As New Clsdsh_Widgets(Page)
        Dim ObjDataSet As New System.Data.DataSet

        Select Case StrMode
            Case "U"
                ObjClsGroups.FindUsersWidgetsPermission(IntId, txtCode.Text, txtEngName.Text, txtArbName.Text, ObjDataSet)
        End Select

        txtCanViewC.Value = ObjDataSet.Tables(0).Select(" AllowView = 1 ").Length
        txtGridCount.Value = ObjDataSet.Tables(0).Rows.Count

        If txtCanViewC.Value = txtGridCount.Value And Val(txtGridCount.Value) <> 0 Then
            chkCheckAll.Checked = True
        Else
            chkCheckAll.Checked = False
        End If
        UwgSearchUsers.DataSource = ObjDataSet.Tables(0).DefaultView
        UwgSearchUsers.DataBind()
    End Sub
End Class
