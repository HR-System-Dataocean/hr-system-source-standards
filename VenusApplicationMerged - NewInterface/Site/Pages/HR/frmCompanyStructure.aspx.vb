Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class Pages_HR_frmCompanyStructure
    Inherits MainPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ClsCompanies As New ClsSysCompanyStructure(Page)
        ClsCompanies.ReadCompanyStructure(UltraWebTree1)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsCompanies.ConnectionString)
        If ObjNavigationHandler.SetLanguage(Page, "Eng/Arb") = "Arb" Then
            Dim mLeft As New System.Web.UI.WebControls.Unit(100, UnitType.Percentage)
            UltraWebTree1.Padding.Left = mLeft
        End If
    End Sub
End Class
