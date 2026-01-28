Imports Venus.Application.SystemFiles.System
Imports Resources

Partial Class ThemeLangManagerLogin
    Inherits System.Web.UI.UserControl
    Protected Sub ImageButton_Theme_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Blue.Command, ImageButton_Silver.Command
        Dim _Sys_User As New Clssys_Users(New Page)
        If _Sys_User.Find("ID = " & ProfileCls.RetUserID()) Then
            _Sys_User.InterfaceStyle = Convert.ToString(e.CommandName) & "_" & _Sys_User.InterfaceLang
            _Sys_User.Update("ID = " & ProfileCls.RetUserID())
            ProfileCls.LoadProfile(_Sys_User.ID)
        Else
            HttpContext.Current.Session(MetaData.SessionCurrentThemeKey) = Convert.ToString(e.CommandName) & "_" & HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey).ToString()
        End If

        Dim Cookie As New HttpCookie("LoginCookies")
        Cookie.Values(MetaData.SessionCurrentLanguageKey) = HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey).ToString()
        Cookie.Values(MetaData.SessionCurrentThemeKey) = HttpContext.Current.Session(MetaData.SessionCurrentThemeKey).ToString()
        Cookie.Expires = DateTime.Now.AddYears(1)
        Response.Cookies.Add(Cookie)

        Response.Redirect(Request.Url.ToString())
    End Sub
    Protected Sub ImageButton_Lang_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_English.Command, ImageButton_Arabic.Command
        Dim _Sys_User As New Clssys_Users(New Page)
        If _Sys_User.Find("ID = " & ProfileCls.RetUserID()) Then
            _Sys_User.InterfaceLang = Convert.ToString(e.CommandName)
            _Sys_User.InterfaceStyle = Page.Theme.Substring(0, Page.Theme.LastIndexOf("_")) & "_" & e.CommandName
            _Sys_User.Update("ID = " & ProfileCls.RetUserID())
            ProfileCls.LoadProfile(_Sys_User.ID)
        Else
            HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey) = Convert.ToString(e.CommandName)
            HttpContext.Current.Session(MetaData.SessionCurrentThemeKey) = Page.Theme.Substring(0, Page.Theme.LastIndexOf("_")) & "_" & e.CommandName
        End If

        Dim Cookie As New HttpCookie("LoginCookies")
        Cookie.Values(MetaData.SessionCurrentLanguageKey) = HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey).ToString()
        Cookie.Values(MetaData.SessionCurrentThemeKey) = HttpContext.Current.Session(MetaData.SessionCurrentThemeKey).ToString()
        Cookie.Expires = DateTime.Now.AddYears(1)
        Response.Cookies.Add(Cookie)

        ' Old System Modifi
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim CurLanguage As String = IIf(ProfileCls.CurrentLanguage() = "Ar", "ar-EG", "en-US")
        ObjWebHandler.SetCookies(Page, "Lang", CurLanguage, True)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Response.Redirect(Request.Url.ToString())
    End Sub
End Class


