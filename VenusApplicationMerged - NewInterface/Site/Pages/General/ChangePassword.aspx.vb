Imports Venus.Application.SystemFiles.System

Partial Class ChangePassword
    Inherits MainPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub
    Dim ObjEncode As New Venus.Shared.Security.ClientCrypt
    Protected Sub LinkButton_Change_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Change.Click
        Dim _Sys_User As New Clssys_Users(New Page)
        If _Sys_User.Find("ID = " & Convert.ToInt64(Context.User.Identity.Name.ToString())) Then
            If _Sys_User.CanChangePassword = True Then
                If _Sys_User.Password = ObjEncode.Encrypt(TextBox_OldPassword.Text, "DataOcean") Then
                    _Sys_User.Password = ObjEncode.Encrypt(TextBox_NewPassword.Text, "DataOcean")
                    _Sys_User.Update("ID = " & Convert.ToInt64(Context.User.Identity.Name.ToString()))
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
                End If
            End If
        End If
    End Sub
End Class
