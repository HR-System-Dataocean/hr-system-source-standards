
Partial Class Interfaces_ErrorPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ObjExeptions As New System.Exception
        ObjExeptions = CType(Session.Item("ErrorValue"), System.Exception)

        txtErrorHandler.InnerText = ObjExeptions.Message
        txtPageName.InnerText = ObjExeptions.TargetSite.ReflectedType.Assembly.FullName
        txtModuleName.InnerText = ObjExeptions.TargetSite.DeclaringType.Name

    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Try
            Dim StrMailMessage As String = String.Empty
            Dim ObjExeptions As New System.Exception
            ObjExeptions = CType(Session.Item("ErrorValue"), System.Exception)

            StrMailMessage = "================================================" & vbCrLf & _
                     "Title: " & " Error Message coming " & vbCrLf & _
                     "ModuleName: " & ObjExeptions.TargetSite.DeclaringType.Name & vbCrLf & _
                     "ProcedureName: " & ObjExeptions.TargetSite.Name & vbCrLf & _
                     "ErrorDescription: " & ObjExeptions.Message & vbCrLf & _
                     "ErrorSource: " & ObjExeptions.StackTrace & vbCrLf & _
                     "AppVersion: " & ObjExeptions.TargetSite.ReflectedType.Assembly.FullName & vbCrLf & _
                     "Date/Time: " & Date.Now & vbCrLf & _
                     "================================================" & vbCrLf

            Dim ObjMail As New System.Net.Mail.MailMessage
            Dim ObjMailServer As New System.Net.Mail.SmtpClient("najtech3")
            Dim ObjMailAddress As New System.Net.Mail.MailAddress("ahmede@DataOcean.com")


            ObjMail.Subject = "Error Messege"
            ObjMail.To.Add("ahmedehssan@msn.com")
            ObjMail.From = ObjMailAddress
            ObjMail.Body = StrMailMessage
            ObjMail.Priority = Mail.MailPriority.High
            ObjMailServer.Send(ObjMail)
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Messege Sent Successfully")
        Catch ex As Exception
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Messege Sending Faild")
        End Try

    End Sub
End Class
