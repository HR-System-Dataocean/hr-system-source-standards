Imports Resources
Imports OfficeWebUI
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class Login
    Inherits MainPage

#Region "Theme & Language"
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Me.Manager1.DirectionMode = IIf(ProfileCls.CurrentLanguage() = "Ar", DocumentDirection.RTL, DocumentDirection.LTR)
        Me.Manager1.UITheme = IIf(ProfileCls.CurrentTheme().Contains("Blue") = True, OfficeWebUI.Theme.Office2010Blue, OfficeWebUI.Theme.Office2010Silver)

        Me.Manager1.Resources.Text_Cancel = MessageSetting.Cancel
        Me.Manager1.Resources.Text_Yes = MessageSetting.Yes
        Me.Manager1.Resources.Text_No = MessageSetting.No
        Me.Manager1.Resources.Text_Ok = MessageSetting.OK
    End Sub
#End Region

#Region "Login Button Functions"
    Dim ObjEncode As New Venus.Shared.Security.ClientCrypt
    Public Function IsAuthenticated(ByVal username As String, ByVal pwd As String) As Int64
        If String.IsNullOrEmpty(username) OrElse String.IsNullOrEmpty(pwd) Then
            Return 0
        End If
        Dim _Sys_Users As New Clssys_Users(Me.Page)
        If Not _Sys_Users.Find("Code = '" & username & "'") Then
            Return -1
        Else
            If _Sys_Users.PasswordExpiry <> Nothing And _Sys_Users.PasswordExpiry < DateTime.Now Then
                Return -1
            End If
            If _Sys_Users.Password <> ObjEncode.Encrypt(pwd, "DataOcean") Then
                Return -2
            End If
            Return Convert.ToInt64(_Sys_Users.ID)
        End If
    End Function
    Protected Sub LinkButton_Login_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton_Login.Click
        If Me.OfficePopup_SignUp.Visible = True Then
            Return
        End If
        Me.OfficePopup_Companies.Hide()
        Me.OfficePopup_SignUp.Hide()
        Me.OfficePopup_Help.Hide()
        If TextBox_Username.Text = "" Or TextBox_Password.Text = "" Then
            Me.OkMessage.Show(MessageBoxType.Error, Message.UserPasswordRequired, MessageSetting.FailledMessage)
        ElseIf TextBox_Username.Text <> "" Or TextBox_Password.Text <> "" Then
            Dim RetUserID As Int64 = IsAuthenticated(TextBox_Username.Text, TextBox_Password.Text)
            Page.Session.Add("UserID", RetUserID)
            If RetUserID = -1 Then
                Me.OkMessage.Show(MessageBoxType.Error, Message.UserIncorrect, MessageSetting.FailledMessage)
            ElseIf RetUserID = -2 Then
                Me.OkMessage.Show(MessageBoxType.Error, Message.PasswordIncorrect, MessageSetting.FailledMessage)
            Else
                Me.OfficePopup_Companies.Title = MessageSetting.SelectCompany
                Me.HiddenField_UserID.Value = RetUserID

                Dim _Sys_Companies As New Clssys_Companies(Me.Page)
                _Sys_Companies.Find("ID in (select CompanyID from sys_CompaniesUsers where UserID = " & RetUserID & " and CanView = 1)")
                RetDropDownList(Me.DropDownList_Company, _Sys_Companies.DataSet.Tables(0), "ID", IIf(ProfileCls.CurrentLanguage() = "Ar", "ArbName", "EngName")).DataBind()

                Dim _Sys_Groups As New Clssys_Groups(Me.Page)
                _Sys_Groups.Find("ID in (select GroupID from sys_GroupsUsers where UserID = " & RetUserID & ")")
                RetDropDownList(Me.DropDownList_Groups, _Sys_Groups.DataSet.Tables(0), "ID", IIf(ProfileCls.CurrentLanguage() = "Ar", "ArbName", "EngName")).DataBind()

                If Me.DropDownList_Company.Items.Count = 0 Then
                    Me.OkMessage.Show(MessageBoxType.Error, Message.NoAuthCompnaies, MessageSetting.FailledMessage)
                Else
                    Me.OfficePopup_Companies.Show()
                End If
            End If
        End If
    End Sub
    Public Shared Function RetDropDownList(ByRef DDL As DropDownList, ByRef DT As System.Data.DataTable, ByVal valueMember As String, ByVal DisMember As String) As DropDownList
        DDL.DataSource = DT
        DDL.DataValueField = valueMember
        DDL.DataTextField = DisMember
        Return (DDL)
    End Function
    Protected Sub OfficePopup_Companies_ClickOk(ByVal sender As Object, ByVal e As System.EventArgs) Handles OfficePopup_Companies.ClickOk
        Dim _Sys_User As New Clssys_Users(New Page)
        _Sys_User.Find("ID = " & HiddenField_UserID.Value)
        Dim authTicket As New FormsAuthenticationTicket(1, _Sys_User.ID.ToString(), DateTime.Now, DateTime.Now.AddHours(_Sys_User.SessionIdleTime), False, String.Empty)
        Dim encryptedTicket As String = FormsAuthentication.Encrypt(authTicket)
        Dim authCookie As New HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
        Response.Cookies.Add(authCookie)
        Dim Cookie As New HttpCookie("LoginCookies")
        Cookie.Values(MetaData.SessionCurrentLanguageKey) = HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey).ToString()
        Cookie.Values(MetaData.SessionCurrentThemeKey) = HttpContext.Current.Session(MetaData.SessionCurrentThemeKey).ToString()
        Cookie.Expires = DateTime.Now.AddYears(1)
        Response.Cookies.Add(Cookie)

        Dim Sys_Companie As New Clssys_Companies(Me.Page)
        Sys_Companie.Find("ID = " & Convert.ToInt32(DropDownList_Company.SelectedValue))
        HttpContext.Current.Session.Add(MetaData.CurrentCompanyInfo, Sys_Companie.DataSet.Tables(0))

        ' Old System Modifi
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Page.Session.Add("CompanyID", Me.DropDownList_Company.SelectedValue)
        Page.Session.Add("GroupID", Me.DropDownList_Groups.SelectedValue)

        ObjWebHandler.SetCookies(Page, "CompanyID", Me.DropDownList_Company.SelectedValue, True)
        ObjWebHandler.SetCookies(Page, "GroupID", Me.DropDownList_Groups.SelectedValue, True)
        ObjWebHandler.SetCookies(Page, "UserID", _Sys_User.ID, True)

        Dim CurLanguage As String = IIf(ProfileCls.CurrentLanguage() = "Ar", "ar-EG", "en-US")
        ObjWebHandler.SetCookies(Page, "Lang", CurLanguage, True)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Response.Redirect(FormsAuthentication.GetRedirectUrl(_Sys_User.Code, False))
    End Sub
#End Region

#Region "Help Button Functions"
    Protected Sub LinkButton_Help_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton_Help.Click
        Me.OfficePopup_Companies.Hide()
        Me.OfficePopup_SignUp.Hide()
        Me.OfficePopup_Help.Hide()
        Me.OfficePopup_Help.Title = MessageSetting.HelpTitle
        Me.OfficePopup_Help.Show()
    End Sub

    Protected Sub OfficePopup_Help_ClickOk(ByVal sender As Object, ByVal e As System.EventArgs) Handles OfficePopup_Help.ClickOk
        Me.OfficePopup_Help.Hide()
    End Sub
    Protected Sub LinkButton_SignUp_Click(sender As Object, e As System.EventArgs) Handles LinkButton_SignUp.Click
        Label1.Text = ""
        Me.OfficePopup_SignUp.Dispose()
        Me.OfficePopup_Companies.Hide()
        Me.OfficePopup_SignUp.Hide()
        Me.OfficePopup_Help.Hide()
        Me.OfficePopup_SignUp.Title = MessageSetting.SignUpTitle
        Me.OfficePopup_SignUp.Show()
    End Sub
    Protected Sub OfficePopup_SignUp_ClickOk(sender As Object, e As System.EventArgs) Handles OfficePopup_SignUp.ClickOk
        Label1.Text = ""
        Dim ObjEncode As New Venus.Shared.Security.ClientCrypt
        If TextBox_EmployeeCode.Text <> String.Empty Or TextBox_RegUsername.Text <> String.Empty Then
            Dim EmployeeID As Object = ProfileCls.CheckEmployeeByCode(TextBox_EmployeeCode.Text)
            If EmployeeID = Nothing Then
                Label1.Text = Message.NotValidEmpCode
            Else
                Dim UserID As Object = ProfileCls.RetUserByRefPeople(EmployeeID)
                If UserID <> Nothing Then
                    Label1.Text = Message.ECAlreadyRegister
                Else
                    If ProfileCls.CheckUserByCode(TextBox_RegUsername.Text) <> Nothing Then
                        Label1.Text = Message.AlreadyUNRegister
                    Else
                        Try
                            Dim _sys_User As New Clssys_Users(Page)
                            _sys_User.Code = TextBox_RegUsername.Text
                            _sys_User.Password = ObjEncode.Encrypt(TextBox_RegPassword.Text, "DataOcean")
                            _sys_User.ArbName = ProfileCls.RetEmployeeArb(EmployeeID)
                            _sys_User.EngName = ProfileCls.RetEmployeeEng(EmployeeID)
                            _sys_User.RegDate = DateTime.Now
                            _sys_User.InterfaceLang = ProfileCls.CurrentLanguage
                            _sys_User.InterfaceStyle = ProfileCls.CurrentTheme
                            _sys_User.RelEmployee = EmployeeID
                            _sys_User.SessionIdleTime = 60
                            _sys_User.CanChangePassword = True

                            If (_sys_User.Save()) Then
                                _sys_User.Find("Code = '" & TextBox_RegUsername.Text & "'")
                                Dim _Clssys_CompaniesUsers As New Clssys_CompaniesUsers(Me)
                                _Clssys_CompaniesUsers.CompanyID = ProfileCls.RetEmployeeComp(EmployeeID)
                                _Clssys_CompaniesUsers.UserID = _sys_User.ID
                                _Clssys_CompaniesUsers.CanView = True
                                _Clssys_CompaniesUsers.RegDate = DateTime.Now
                                _Clssys_CompaniesUsers.Save()

                                Dim _Clssys_GroupsUsers As New Clssys_GroupsUsers(Me)
                                _Clssys_GroupsUsers.UserID = _sys_User.ID
                                _Clssys_GroupsUsers.GroupID = Convert.ToInt32(ConfigurationManager.AppSettings("GuestGroup"))
                                _Clssys_GroupsUsers.RegDate = DateTime.Now
                                _Clssys_GroupsUsers.Save()
                            End If
                            Me.OfficePopup_SignUp.Hide()
                            TextBox_Username.Text = TextBox_RegUsername.Text
                            TextBox_Password.Focus()
                        Catch ex As Exception
                            Label1.Text = "Registration Failled. Try Again"
                        End Try
                    End If
                End If
            End If
        End If
    End Sub
#End Region

End Class