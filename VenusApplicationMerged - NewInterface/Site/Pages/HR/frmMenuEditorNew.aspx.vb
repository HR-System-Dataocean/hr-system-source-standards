Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmMenuEditorNew
    Inherits MainPage
#Region "Private Decleration"

    Private ObjMenuHandler As Venus.Shared.Web.NavigationHandler
    Private ObjErrorHandler As Venus.Shared.ErrorsHandler
    Private ObjAddinHandler As New Venus.Shared.Web.ClientSideActions
    Private ObjWebHandler As New Venus.Shared.Web.WebHandler
    Private ObjDataHandler As New Venus.Shared.DataHandler

#End Region

#Region "Constant Decleration"

    Const TlbbtnSave = 0
    Const TlbbtnReturn = 1

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrLastSerial As String = String.Empty
        Dim StrApplicationFolder As String = String.Empty
        Dim ClsBaseClass As New ClsDataAcessLayer(Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsForms As New ClsSys_Forms(Page)
        ObjMenuHandler = New Venus.Shared.Web.NavigationHandler(ClsObjects.ConnectionString)
        StrApplicationFolder = ConfigurationManager.AppSettings("ApplicationFolder")
        ObjMenuHandler = New Venus.Shared.Web.NavigationHandler(ClsBaseClass.ConnectionString)
        ObjErrorHandler = New Venus.Shared.ErrorsHandler(ClsBaseClass.ConnectionString)

        If Not IsPostBack Then

            ClsForms.GetDropDownList(DdlTargetForm, True, "CancelDate is Null ")
            ClsForms.GetDropDownList(DdlViewForm, True, "CancelDate is Null ")
            ClsObjects.GetDropDownList(DdlObjectID, True, "CancelDate is Null ")
            ObjMenuHandler.GetPagesList(DdlMenus)

        End If

        ObjDataHandler.GetLastSerial("sys_Menus", "Code", StrLastSerial, ClsBaseClass.ConnectionString, "0000")
        txtCode.Text = StrLastSerial
        SetScreenInformation()

    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_Delete.Command
        Dim IntIndex As Integer
        ObjMenuHandler.Find("code='" & txtCode.Text & "'")
        IntIndex = ObjMenuHandler.ID
        Dim ClsForms As New ClsSys_Forms(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsForms.ConnectionString)
        Select Case e.CommandArgument
            Case "Save"
                If SetData() Then
                    If IntIndex = 0 Then
                        If ObjMenuHandler.Save() Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Me, ObjNavigationHandler.SetLanguage(Me, "Save complete successfully/تم الحفظ بنجاح"))
                            Response.Redirect("frmMenuEditor.aspx")
                        Else
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Me, ObjNavigationHandler.SetLanguage(Me, "Error while saving data, Please check the input data!/الرجاء التأكد من البيانات المدخلة"))
                        End If
                    Else
                        If ObjMenuHandler.Update(IntIndex) Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Me, ObjNavigationHandler.SetLanguage(Me, "Save complete successfully/تم الحفظ بنجاح"))
                            Response.Redirect("frmMenuEditor.aspx")
                        Else
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Me, ObjNavigationHandler.SetLanguage(Me, "Error while upating data, Please check the input data!/الرجاء التأكد من البيانات المدخلة"))
                        End If
                    End If
                End If
            Case "Delete"
                    Response.Redirect("frmMenuEditor.aspx")
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        Try
            ObjMenuHandler.Find("Code='" & txtCode.Text & "'")
            GetData()
        Catch ex As Exception
            ObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Sub

#End Region

#Region "Private Functions"

    Private Function GetData() As Boolean
        Try

            With ObjMenuHandler

                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                txtItemRank.Text = .Rank
                txtShortcut.Text = .Shortcut
                DdlTargetForm.SelectedValue = .FormID

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
                .Rank = Val(txtItemRank.Text)
                .Shortcut = txtShortcut.Text
                .FormID = DdlTargetForm.SelectedItem.Value
                .ObjectID = DdlObjectID.SelectedItem.Value
                .ViewFormID = DdlViewForm.SelectedItem.Value
                .ViewType = DdlViewType.SelectedItem.Value
                .ParentID = DdlMenus.SelectedItem.Value
                .IsHide = ChkHide.Checked

            End With

            Return True
        Catch ex As Exception
            ObjErrorHandler.RecordExceptions_DataBase("Error while assign the value to class", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Return False
        End Try

    End Function
    Private Function SetScreenInformation() As Boolean
        Dim ClsBaseClass As New ClsDataAcessLayer(Page)
        Try

            Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, ClsBaseClass.ConnectionString)
            Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, ClsBaseClass.ConnectionString)
            Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, ClsBaseClass.ConnectionString)
            Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, ClsBaseClass.ConnectionString)
            Venus.Shared.Web.ClientSideActions.SetPageToolTips(Me, ClsBaseClass.ConnectionString)
            Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, ClsBaseClass.ConnectionString, DIV)
            Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)


        Catch ex As Exception
            ObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function

#End Region
End Class
