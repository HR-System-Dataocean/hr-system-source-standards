Imports System.Data
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class _Main
    Inherits MainPage

    Protected Sub Page_Init1(sender As Object, e As System.EventArgs) Handles Me.Init
        Label1.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "الملف الشخصى", "Login Profile")
        Dim _polic As New PoliciesWs()
        Dim dt As DataTable = _polic.AuthorizedPages()
        Dim Dr As DataRow() = dt.Select("LinkTarget = '_Blank' and LinkUrl <> ''")
        If Dr.Length > 0 Then
            Label2.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "الخدمة الذاتية", "Self Services")
        Else
            Label2.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "", "")
        End If
        Label3.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "تنبيهات المستندات", "WorkFlow Alerts")
        Label4.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "تتبع المستندات", "WorkFlow Tracking")
        '  Label6.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "متابعة المهام", "Tasks Management")
        Dim clsForms As New ClsSys_Forms(Page)
        If clsForms.Find("Code='frmExpiredDocuments'") Then
            Dim clsFormsPermissions As New ClsSys_FormsPermissions(Page)
            clsFormsPermissions.Find1("FormID=" & clsForms.ID & " And UserID=" & clsForms.DataBaseUserRelatedID)
            If clsFormsPermissions.AllowView Then
                Label5.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "تنبيهات الوثائق المنتهية", "Expiry Documents Alerts")
            Else
                Label5.Text = ""
            End If
        End If
    End Sub
End Class
