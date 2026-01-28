Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmReportPermission
    Inherits MainPage

#Region "Constant"

    Const uwgCode = 0
    Const uwgEngName = 1
    Const uwgArbName = 2
    Const uwgView = 3
    Const uwgPrint = 4
    Const uwgExport = 5
    Const uwgId = 6

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

        chkAllowView.Attributes.Add("onclick", "UwgSearchCheckColumns(3)")
        chkAllowPrint.Attributes.Add("onclick", "UwgSearchCheckColumns(4)")
        chkAllowExport.Attributes.Add("onclick", "UwgSearchCheckColumns(5)")
        chkCheckAll.Attributes.Add("onclick", "UwgSearchCheckColumns(7)")

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
                Dim ClsReportPermission As New ClsSys_ReportsPermissions(Page)

                Select Case StrMode
                    Case "U"
                        StrFieldName = "UserID"
                    Case "G"
                        StrFieldName = "GroupID"
                    Case Else
                        StrFieldName = "UserID"
                End Select

                For Each ObjRow In UwgSearchUsers.Rows

                    With ClsReportPermission

                        If .Find(StrFieldName & "=" & IntID & " And ReportID =" & ObjRow.Cells(uwgId).Value) Then
                            .CanView = IIf(ObjRow.Cells(uwgView).Value = 0, False, True)
                            .CanPrint = IIf(ObjRow.Cells(uwgPrint).Value = 0, False, True)
                            .CanExport = IIf(ObjRow.Cells(uwgExport).Value = 0, False, True)
                            .Update(StrFieldName & "=" & IntID & " And ReportID =" & ObjRow.Cells(uwgId).Value)

                        Else
                            .ReportID = ObjRow.Cells(uwgId).Value
                            Select Case StrMode
                                Case "U"
                                    .UserID = IntID
                                    .SysGroupID = Nothing
                                Case "G"
                                    .UserID = Nothing
                                    .SysGroupID = IntID
                                Case Else
                                    .UserID = IntID
                                    .SysGroupID = Nothing
                            End Select
                            .CanView = IIf(ObjRow.Cells(uwgView).Value = 0, False, True)
                            .CanPrint = IIf(ObjRow.Cells(uwgPrint).Value = 0, False, True)
                            .CanExport = IIf(ObjRow.Cells(uwgExport).Value = 0, False, True)
                            .RegDate = Date.Now
                            .Save()
                        End If
                    End With
                Next
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsReportPermission.ConnectionString)
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

        Dim ObjClsGroups As New ClsSys_Reports(Page)
        Dim ObjDataSet As New System.Data.DataSet
        Dim ObjTempDataSet As Data.DataSet

        Select Case StrMode
            Case "U"
                ObjClsGroups.FindUsersReportsPermission(IntId, txtCode.Text, txtEngName.Text, txtArbName.Text, ObjDataSet)
                ObjTempDataSet = ObjClsGroups.GetUsersReportsPermissionsCounts(IntId, txtCode.Text, txtEngName.Text, txtArbName.Text)
            Case "G"
                ObjClsGroups.FindGroupsReportsPermission(IntId, txtCode.Text, txtEngName.Text, txtArbName.Text, ObjDataSet)
                ObjTempDataSet = ObjClsGroups.GetGroupsReportsPermissionsCounts(IntId, txtCode.Text, txtEngName.Text, txtArbName.Text)
            Case Else
                ObjClsGroups.FindUsersReportsPermission(IntId, txtCode.Text, txtEngName.Text, txtArbName.Text, ObjDataSet)
                ObjTempDataSet = ObjClsGroups.GetUsersReportsPermissionsCounts(IntId, txtCode.Text, txtEngName.Text, txtArbName.Text)
        End Select
        chkAllowView.Checked = False
        chkAllowExport.Checked = False
        chkAllowPrint.Checked = False
        chkCheckAll.Checked = False

        If ObjTempDataSet.Tables(0).Rows.Count > 0 Then
            txtCanViewC.Value = ObjTempDataSet.Tables(0).Rows(0).Item(0)
            txtCanPrintC.Value = ObjTempDataSet.Tables(0).Rows(1).Item(0)
            txtCanExportC.Value = ObjTempDataSet.Tables(0).Rows(2).Item(0)
            txtGridCount.Value = ObjTempDataSet.Tables(0).Rows(3).Item(0)
            txtGridCountAll.Value = ObjDataSet.Tables(0).Rows.Count
            If txtCanViewC.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkAllowView.Checked = True
            If txtCanPrintC.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkAllowPrint.Checked = True
            If txtCanExportC.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkAllowExport.Checked = True
            If txtGridCount.Value = txtGridCountAll.Value And Val(txtGridCountAll.Value) <> 0 Then chkCheckAll.Checked = True
        End If
        UwgSearchUsers.DataSource = ObjDataSet.Tables(0).DefaultView
        UwgSearchUsers.DataBind()
    End Sub
End Class
