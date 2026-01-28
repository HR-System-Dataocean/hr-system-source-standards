Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class Interfaces_frmFormulaDesigner
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim StrTargetControl As String = Request.QueryString("ControlName")
        Dim StrTargetControlValue As String = Request.QueryString("ControlValue")
        Dim StrTargetControlType As String = Request.QueryString("ControlType")
        Dim ClsTransaction As New Clshrs_TransactionsTypes(Page)
        ClsTransaction.Find("")
        uwgBenetitTemplet.Rows.Clear(True)
        uwgBenetitTemplet.DataSource = ClsTransaction.DataSet
        uwgBenetitTemplet.DataBind()
        txtFormula.Text = IIf(StrTargetControlValue = "null", "", StrTargetControlValue)
        'Button1.Attributes.Add("onClick", "btnSave_Click();")
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">targetControl('" & StrTargetControl & "','" & StrTargetControlType & "');</script>")

    End Sub

End Class
