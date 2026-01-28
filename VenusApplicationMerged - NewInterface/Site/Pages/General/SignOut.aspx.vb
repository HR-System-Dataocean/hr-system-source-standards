
Partial Class SignOut
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cookies(FormsAuthentication.FormsCookieName).Expires = DateTime.Now.AddDays(-1)
        Response.Redirect(FormsAuthentication.LoginUrl)
    End Sub
End Class
