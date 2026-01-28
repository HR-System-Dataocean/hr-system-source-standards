Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class Pages_HR_frmDepartmentsStructure
    Inherits MainPage
    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ClsDepartments As New ClsSysDepartmentStructure(Page)
        ClsDepartments.CreateDepartments(UltraWebTree1)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDepartments.ConnectionString)
        If ObjNavigationHandler.SetLanguage(Page, "Eng/Arb") = "Arb" Then
            Dim mLeft As New System.Web.UI.WebControls.Unit(100, UnitType.Percentage)
            UltraWebTree1.Padding.Left = mLeft
        End If
    End Sub
End Class
