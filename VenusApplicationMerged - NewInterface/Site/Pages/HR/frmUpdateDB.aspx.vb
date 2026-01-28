Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmUpdateDB
    Inherits MainPage
#Region "Public Decleration"
    Private ObjClssys_Groups As Clssys_Groups
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ObjClssys_Groups = New Clssys_Groups(Me)
        Dim SearchID As Integer = 0
        Try

            If Not IsPostBack Then
                'Page.Session.Add("ConnectionString", ObjClssys_Groups.ConnectionString)
                'ObjClssys_Groups.AddOnChangeEventToControls("frmUpdateDB", Page, UltraWebTab1)

                '================================= Exit & Navigation Notification [ End ]

            End If


            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ObjClssys_Groups.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub


#End Region

#Region "Private Functions"

    Protected Sub btnHR_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnHR.Command
        Dim clsUpdate As ClsSys_UpdateDB
        clsUpdate = New ClsSys_UpdateDB(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsUpdate.ConnectionString)
        clsUpdate.UpdateHR()
        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "HR Update Done/تم تحديث بيانات الموارد البشرية"))
    End Sub
    Protected Sub btnSS_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnSS.Command
        ' This code runs when the button is clicked
        Dim clsUpdate As ClsSys_UpdateDB
        clsUpdate = New ClsSys_UpdateDB(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsUpdate.ConnectionString)
        clsUpdate.UpdateSS()
        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Self Services Update Done/تم تحديث بيانات الخدمة الذاتية"))
    End Sub



#End Region
End Class
