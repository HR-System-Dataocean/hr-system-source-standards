Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class Pages_HR_frmPositionsStructure
    Inherits MainPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ClsPositionsStructure As New ClsPositionsStructure(Page)
        ClsPositionsStructure.CreatePositions(UltraWebTree1)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsPositionsStructure.ConnectionString)
        If ObjNavigationHandler.SetLanguage(Page, "Eng/Arb") = "Arb" Then
            Dim mLeft As New System.Web.UI.WebControls.Unit(100, UnitType.Percentage)
            UltraWebTree1.Padding.Left = mLeft
        End If
    End Sub
End Class
