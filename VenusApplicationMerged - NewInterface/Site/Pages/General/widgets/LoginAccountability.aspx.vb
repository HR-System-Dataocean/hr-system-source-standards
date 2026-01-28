Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class LoginAccountability
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
            Dim ClsPositionAccountabilities = New Clshrs_PositionAccountabilities(Page)
            ClsPositionAccountabilities.Find("canceldate is null and PositionID=" & posid)
            Dim Dt0 As Data.DataTable = ClsPositionAccountabilities.DataSet.Tables(0)
            Dim DTMNode As New Infragistics.Web.UI.NavigationControls.DataTreeNode
            Dim DTCNode As New Infragistics.Web.UI.NavigationControls.DataTreeNode
            For i As Integer = 0 To Dt0.Rows.Count - 1
                DTMNode = New Infragistics.Web.UI.NavigationControls.DataTreeNode
                DTMNode.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", Dt0.Rows(i)(3).ToString(), Dt0.Rows(i)(2).ToString())
                Dim ClsPositionAccountabilitiesDtl = New Clshrs_PositionAccountabilitiesDtl(Page)
                ClsPositionAccountabilitiesDtl.Find("canceldate is null and PosAccountabilityID = " & Dt0.Rows(i)(0).ToString())
                Dim Dt As Data.DataTable = ClsPositionAccountabilitiesDtl.DataSet.Tables(0)
                For x As Integer = 0 To Dt.Rows.Count - 1
                    DTCNode = New Infragistics.Web.UI.NavigationControls.DataTreeNode
                    DTCNode.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", Dt.Rows(x)(3).ToString(), Dt.Rows(x)(2).ToString())
                    DTMNode.Nodes.Add(DTCNode)
                Next
                WebDataTree_DocumentsAlerts.Nodes.Add(DTMNode)
            Next
        End If
    End Sub
End Class
