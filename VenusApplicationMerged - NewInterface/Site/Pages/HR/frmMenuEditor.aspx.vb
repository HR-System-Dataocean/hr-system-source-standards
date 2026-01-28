Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmMenuEditor
    Inherits MainPage

#Region "Private Decleration"
    Private ObjMenuHandler As Venus.Shared.Web.NavigationHandler
    Private ObjErrorHandler As Venus.Shared.ErrorsHandler
    Private ObjAddinHandler As Venus.Shared.Web.ClientSideActions
    Private ObjWebHandler As Venus.Shared.Web.WebHandler
#End Region

#Region "Constant Decleration"
    Const TlbbtnNew = 1
    Const TlbbtnSave = 3
    Const TlbbtnRef = 5
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsForms As New ClsSys_Forms(Page)
        Dim clsusers As New Clssys_Users(Page)
        clsusers.IsAuthenticated()
        ObjMenuHandler = New Venus.Shared.Web.NavigationHandler(ClsObjects.ConnectionString)
        Dim Cls_DataAccessLayer As New ClsDataAcessLayer(Page)
        Dim Lage As Integer = ObjMenuHandler.SetLanguage(Page, "0/1")

        ObjMenuHandler = New Venus.Shared.Web.NavigationHandler(Cls_DataAccessLayer.ConnectionString)
        ObjErrorHandler = New Venus.Shared.ErrorsHandler(Cls_DataAccessLayer.ConnectionString)
        ObjAddinHandler = New Venus.Shared.Web.ClientSideActions
        ObjWebHandler = New Venus.Shared.Web.WebHandler

        If Lage = 1 Then
            Dim mLeft As New System.Web.UI.WebControls.Unit(100, UnitType.Percentage)
            UwtNavigation.Padding.Left = mLeft

        End If
 
        If Not Page.IsPostBack Then            

            ObjMenuHandler.GetPagesList(DdlMenu, Lage)
            ClsForms.GetDropDownList(DdlTargetForm, True, "CancelDate is Null ")
            ClsForms.GetDropDownList(DdlViewForm, True, "CancelDate is Null ")
            ClsObjects.GetDropDownList(DdlObjects, True, "CancelDate is Null ")

        End If

        SetScreenInformation()
        ImageButton_Save.Enabled = False
        ObjMenuHandler.LoadTreeMenus(UwtNavigation, Cls_DataAccessLayer.DataBaseUserRelatedID, Lage)
        Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

    End Sub
    Protected Sub UwtNavigation_NodeClicked(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs) Handles UwtNavigation.NodeClicked
        ObjMenuHandler.ViewNodeInformation(e.Node)
        ObjMenuHandler.GetPagesList(DdlMenu, ObjMenuHandler.SetLanguage(Page, "0/1"), "ID <> " & CInt(e.Node.Tag))
        GetData()
        ImageButton_Save.Enabled = True
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_Refresh.Command, ImageButton_New.Command
        Dim IntIndex As Integer
        Dim Cls_DataAccessLayer As New ClsDataAcessLayer(Page)
        Dim Lage As Integer = ObjMenuHandler.SetLanguage(Page, "0/1")
        Select Case e.CommandArgument
            Case "Save"
                ObjMenuHandler.Find("code='" & txtCode.Text & "'")
                IntIndex = ObjMenuHandler.ID
                If SetData() Then
                    If ObjMenuHandler.Update(IntIndex) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Me, "Save complete successfully")
                        With ObjMenuHandler
                            .LoadTree(UwtNavigation, Cls_DataAccessLayer.DataBaseUserRelatedID)
                            .GetPagesList(DdlMenu, Lage)
                            .Clear()
                            ImageButton_Save.Enabled = False
                        End With
                        GetData()
                    End If
                End If
            Case "Refresh"
                With ObjMenuHandler
                    .LoadTree(UwtNavigation, Cls_DataAccessLayer.DataBaseUserRelatedID)
                    .GetPagesList(DdlMenu, Lage)
                    .Clear()
                    ImageButton_Save.Enabled = False
                End With
                GetData()
            Case "New"
                Response.Redirect("frmMenuEditorNew.aspx")
        End Select
    End Sub
#End Region

#Region "Private Functions"
    Private Function GetData() As Boolean
        Try

            With ObjMenuHandler

                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                txtItemRank.Text = .Rank
                txtShortcut.Text = .Shortcut
                DdlTargetForm.SelectedValue = .FormID
                ChkHide.Checked = .IsHide
                DdlMenu.SelectedValue = .ParentID
                DdlObjects.SelectedValue = .ObjectID
                DdlViewType.SelectedValue = .ViewType
                DdlViewForm.SelectedValue = .ViewFormID

            End With

            Return True
        Catch ex As Exception
            ObjErrorHandler.RecordExceptions_DataBase("Error while geting the value from class", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Return False
        End Try
    End Function
    Private Function SetData() As Boolean
        Try

            With ObjMenuHandler

                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .Rank = txtItemRank.Text
                .Shortcut = txtShortcut.Text
                .FormID = DdlTargetForm.SelectedItem.Value
                .ObjectID = DdlObjects.SelectedItem.Value
                .ViewType = DdlViewType.SelectedItem.Value
                .ViewFormID = DdlViewForm.SelectedItem.Value
                .IsHide = ChkHide.Checked
                .ParentID = DdlMenu.SelectedItem.Value

            End With

            Return True
        Catch ex As Exception
            ObjErrorHandler.RecordExceptions_DataBase("Error while assign the value to class", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Return False
        End Try

    End Function
    Private Function SetScreenInformation() As Boolean
        Dim Cls_DataAccessLayer As New ClsDataAcessLayer(Page)
        Try

            Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, Cls_DataAccessLayer.ConnectionString, DIV)
            Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, Cls_DataAccessLayer.ConnectionString)
            Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, Cls_DataAccessLayer.ConnectionString)
            Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, Cls_DataAccessLayer.ConnectionString)
            Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, Cls_DataAccessLayer.ConnectionString)
            Venus.Shared.Web.ClientSideActions.SetPageToolTips(Me, Cls_DataAccessLayer.ConnectionString)
            Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, Cls_DataAccessLayer.ConnectionString)

        Catch ex As Exception

        End Try

    End Function
#End Region

End Class
