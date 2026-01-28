<%@ Application Language="VB" %>
<%@ Import Namespace="Resources" %>
<%@ Import Namespace="Stimulsoft.Base" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        Try
            Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHnGrI7U31pf7LINGNdAxG7ryQYWZ5VI3IcibKpAwcRj7GIS8V" +
                                    "DqFkBBbmYvTu5Z8MvU6M6A/k/6taQpYrq/3JaLqScHOPSJC/TnKxxUYtqBwgghbI1V8RD2NKJpGhyxIhw8pi76SSec" +
                                    "I52qCZciJzuloc/AIN6bD8gRmxxEfmUhmEB34p2D3VpY29QVS02hORdBm0jPNYlFwsWr8eGViG01ujDdcvfcUGK7Vh" +
                                    "OUeRFL/PKhhEEifu3FzXEVF7CevfOeyq0liA3o7Xg4SxuvEcdIc+aPrnXZTM4DtnEvxcjNo7veqnhzPYKhP5UEuQMQ" +
                                    "xDrnoCm3U6siTcjVhfiKbDk0i23Ne312Ha3+D1svtBhJnnfmSNRWd/kLhGbwBjVJcXKxmlC0LSIjhte60/cZJBjcK9" +
                                    "menVnAPCDFB0Mw8Rwv4lt+BjQjhWcRfee7bxWmmdXw+Dqk+z6AZk0b8iIajY8XY4x1Rb+t0KmLuNSd27269H9x+pZu" +
                                    "5+Iho6488xpfXxGwUe9WtxYSbuxsIsQuOzKF"
        Catch ex As Exception
            ' ignore license init errors to avoid blocking app start
        End Try
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Dim Cookie As HttpCookie = Request.Cookies("LoginCookies")
        If Cookie IsNot Nothing Then
            HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey) = Cookie.Values(MetaData.SessionCurrentLanguageKey)
            HttpContext.Current.Session(MetaData.SessionCurrentThemeKey) = Cookie.Values(MetaData.SessionCurrentThemeKey)
        Else
            HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey) = Localization.Default_lang
            HttpContext.Current.Session(MetaData.SessionCurrentThemeKey) = Localization.Default_Theme

            Cookie = New HttpCookie("LoginCookies")
            Cookie.Values(MetaData.SessionCurrentLanguageKey) = Localization.Default_lang.ToString()
            Cookie.Values(MetaData.SessionCurrentThemeKey) = Localization.Default_Theme.ToString()
            Cookie.Expires = DateTime.Now.AddYears(1)
            Response.Cookies.Add(Cookie)
        End If
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        Session.RemoveAll()
    End Sub
</script>