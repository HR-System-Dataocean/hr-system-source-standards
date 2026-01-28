Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class LoginContacts
    Inherits MainPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim hrs_employee As New Clshrs_Employees(Page)
        hrs_employee.Find1("ID = " & ProfileCls.RetRefPeople())
        Dim datasetvalidcontracts As New Data.DataSet
        hrs_employee.GetAllEmployeeValidContract(datasetvalidcontracts, hrs_employee.ID)
        If datasetvalidcontracts.Tables(0).Rows.Count > 0 Then
            Dim clscontracts As New Clshrs_Contracts(Page)
            clscontracts.Find("ID = " & datasetvalidcontracts.Tables(0).Rows(0)(1))
            Dim posid As Integer = clscontracts.PositionID
            Dim ClsPositioncontact = New Clshrs_PositionContacts(Page)
            ClsPositioncontact.Find("canceldate is null and PositionID=" & posid)
            Dim Dt0 As Data.DataTable = ClsPositioncontact.DataSet.Tables(0)
            Dim DTMNode As New Infragistics.Web.UI.NavigationControls.DataTreeNode
            For i As Integer = 0 To Dt0.Rows.Count - 1
                DTMNode = New Infragistics.Web.UI.NavigationControls.DataTreeNode
                Dim clsInterval As New Clshrs_Intervals(Page)
                clsInterval.Find("ID = " & Dt0.Rows(i)(5).ToString())
                DTMNode.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "الإتصال مع : " & Dt0.Rows(i)(3).ToString() & " كل : " & clsInterval.ArbName & " حالة الإتصال : " & IIf(Dt0.Rows(i)(4).ToString() = "True", "خارجى", "داخلى"), "Contact With : " & Dt0.Rows(i)(2).ToString() & " Every : " & clsInterval.EngName & " Direction : " & IIf(Dt0.Rows(i)(4).ToString() = "True", "External", "Internal"))
                WebDataTree_DocumentsAlerts.Nodes.Add(DTMNode)
            Next
        End If
    End Sub
End Class
