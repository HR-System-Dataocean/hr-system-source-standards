Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeesHealthInsurance
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim StrEmpID As String = Request.QueryString.Item("EmpID")
                Dim clsTicketsClasses As New Clshrs_TicketsClasses(Me)
                Dim clsTicketsRoutes As New Clshrs_TicketsRoutes(Me)
                clsTicketsClasses.GetDropDownList(DropDownList1, True, "CompanyID=" & clsTicketsClasses.MainCompanyID)
                clsTicketsRoutes.GetDropDownList(DropDownList2, True, "CompanyID=" & clsTicketsClasses.MainCompanyID)
                GetData(StrEmpID)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetData(ByVal EmpID As Integer) As Boolean
        Try
            Dim clsEmployees As New Clshrs_Employees(Me)
            Dim clsContracts As New Clshrs_Contracts(Me)
            Dim clsTicketsContarct As New Clshrs_TicketsContarct(Me)
            Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsContracts.ConnectionString)
            Dim FieldName As String = ClsNavHandler.SetLanguage(Me, "EngName/ArbName")
            Dim CommandStr As String = String.Empty
            Dim IntValidContract As Integer
            clsEmployees.Find(" ID = " & EmpID)
            IntValidContract = clsContracts.ContractValidatoinId(EmpID, Date.Now)
            clsContracts.Find("ID = " & IntValidContract)
            lblDescEmployeeCode.Text = clsEmployees.Code
            lblDescEnglishName.Text = clsEmployees.Name
            Try
                CommandStr = " Select " & vbNewLine
                CommandStr &= "(Select Top 1 HIC.[FieldName] From hrs_HIPolicyContract HIPC Inner Join hrs_HICompanyClasses HICC ON HICC.ID=HIPC.HICompanyClasses Inner Join hrs_HICompanies HIC On HIC.ID=HICC.HICompanyID Where ContractID= [ContractID] And HIPC.CancelDate Is null And HIPC.ActiveDate <= GETDATE() Order By HIPC.ActiveDate Desc) As CompanyName, " & vbNewLine
                CommandStr &= "(Select Top 1 HICC.[FieldName] From hrs_HIPolicyContract HIPC Inner Join hrs_HICompanyClasses HICC ON HICC.ID=HIPC.HICompanyClasses Inner Join hrs_HICompanies HIC On HIC.ID=HICC.HICompanyID Where ContractID= [ContractID] And HIPC.CancelDate Is null And HIPC.ActiveDate <= GETDATE() Order By HIPC.ActiveDate Desc) As ClassName, " & vbNewLine
                CommandStr &= "(Select Sum(HIPC.EmployeeAmt) From hrs_HIPolicyContract HIPC Inner Join hrs_HICompanyClasses HICC ON HICC.ID=HIPC.HICompanyClasses Inner Join hrs_HICompanies HIC On HIC.ID=HICC.HICompanyID Where ContractID= [ContractID] And HIPC.CancelDate Is null ) As EmployeeAmt, " & vbNewLine
                CommandStr &= "(Select Sum(HIPC.CompanyAmt) From hrs_HIPolicyContract HIPC Inner Join hrs_HICompanyClasses HICC ON HICC.ID=HIPC.HICompanyClasses Inner Join hrs_HICompanies HIC On HIC.ID=HICC.HICompanyID Where ContractID= [ContractID] And HIPC.CancelDate Is null ) As CompanyAmt, " & vbNewLine
                CommandStr &= "(Select Top 1 CONVERT(Date,HIPC.ActiveDate) From hrs_HIPolicyContract HIPC Inner Join hrs_HICompanyClasses HICC ON HICC.ID=HIPC.HICompanyClasses Inner Join hrs_HICompanies HIC On HIC.ID=HICC.HICompanyID Where ContractID= [ContractID] And HIPC.CancelDate Is null And HIPC.ActiveDate <= GETDATE() Order By HIPC.ActiveDate Desc) As ActiveDate" & vbNewLine
                CommandStr = CommandStr.Replace("[FieldName]", FieldName)
                CommandStr = CommandStr.Replace("[ContractID]", clsContracts.ID)
                Dim ds As New Data.DataSet
                ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsContracts.ConnectionString, Data.CommandType.Text, CommandStr)
                If ds.Tables(0).Rows.Count > 0 Then
                    With ds.Tables(0).Rows(0)
                        TextBox1.Text = .Item("CompanyName")
                        TextBox2.Text = .Item("ClassName")
                        TextBox3.Text = .Item("EmployeeAmt")
                        TextBox4.Text = .Item("CompanyAmt")
                        TextBox5.Text = .Item("ActiveDate")
                    End With
                End If
            Catch ex As Exception
            End Try
            If clsTicketsContarct.Find("ContractID=" & clsContracts.ID & " And CompanyID =" & clsTicketsContarct.MainCompanyID) Then
                DropDownList1.SelectedValue = clsTicketsContarct.TicketsClassID
                DropDownList2.SelectedValue = clsTicketsContarct.TicketsRouteID
                txtTotalCost.Text = clsTicketsContarct.TotalCost
                CheckBox_IsPaid.Checked = clsTicketsContarct.IsPaid
            End If
            Try
                CommandStr = " Select isnull(WHours,0) WHours,IsProjectRelated,IsSpecialForce from hrs_Employees where ID = " & EmpID
                Dim ds As New Data.DataSet
                ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsContracts.ConnectionString, Data.CommandType.Text, CommandStr)
                If ds.Tables(0).Rows.Count > 0 Then
                    TextBox6.Text = ds.Tables(0).Rows(0)("WHours")
                    CheckBox_IsProjectRelated.Checked = ds.Tables(0).Rows(0)("IsProjectRelated")
                    CheckBox_IsSpecial.Checked = ds.Tables(0).Rows(0)("IsSpecialForce")
                End If
            Catch ex As Exception
            End Try
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Protected Sub btnPrint_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnPrint.Click
        Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click
        Try
            Dim clsContracts As New Clshrs_Contracts(Me)
            Dim StrEmpID As String = Request.QueryString.Item("EmpID")
            Dim IntValidContract As Integer
            IntValidContract = clsContracts.ContractValidatoinId(StrEmpID, Date.Now)
            clsContracts.Find("ID = " & IntValidContract)
            Dim clsTicketsContarct As New Clshrs_TicketsContarct(Me)
            Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsContracts.ConnectionString)
            Try
                If clsTicketsContarct.Find("ContractID=" & clsContracts.ID) Then
                    clsTicketsContarct.TicketsClassID = DropDownList1.SelectedValue
                    clsTicketsContarct.TicketsRouteID = DropDownList2.SelectedValue
                    clsTicketsContarct.TotalCost = txtTotalCost.Text
                    clsTicketsContarct.IsPaid = CheckBox_IsPaid.Checked
                    clsTicketsContarct.CompanyID=clsTicketsContarct.MainCompanyID 
                    clsTicketsContarct.Update("ContractID=" & clsContracts.ID)
                Else
                    clsTicketsContarct.TicketsClassID = DropDownList1.SelectedValue
                    clsTicketsContarct.TicketsRouteID = DropDownList2.SelectedValue
                    clsTicketsContarct.TotalCost = txtTotalCost.Text
                    clsTicketsContarct.ContractID = clsContracts.ID
                    clsTicketsContarct.IsPaid = CheckBox_IsPaid.Checked
                    clsTicketsContarct.CompanyID=clsTicketsContarct.MainCompanyID 
                    
                    clsTicketsContarct.Save()
                End If
            Catch ex As Exception
            End Try
            Try
                Dim CommandStr As String = String.Empty
                CommandStr = " update hrs_Employees set WHours = " & IIf(TextBox6.Text = "", 0, TextBox6.Text) & ",IsProjectRelated= " & IIf(CheckBox_IsProjectRelated.Checked, 1, 0) & ",IsSpecialForce= " & IIf(CheckBox_IsSpecial.Checked, 1, 0) & " where ID = " & StrEmpID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsContracts.ConnectionString, Data.CommandType.Text, CommandStr)
            Catch ex As Exception
            End Try
            Venus.Shared.Web.ClientSideActions.ClosePage(Page, ClsNavHandler.SetLanguage(Page, "Save Complete Successfully/تم الحفظ بنجاح"))
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownList2.SelectedIndexChanged
        UpdateTicketsPrice()
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        UpdateTicketsPrice()
    End Sub
    Private Function UpdateTicketsPrice() As Boolean
        Try
            txtTotalCost.Text = "0"
            Dim clsContracts As New Clshrs_Contracts(Me)
            Dim Str As String = "select * from hrs_TicketsPrices where canceldate is null and TicketsRoutesID = " & DropDownList2.SelectedValue & " and TicketsClassesID = " & DropDownList1.SelectedValue
            Dim dsPrices As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsContracts.ConnectionString, System.Data.CommandType.Text, Str)
            If dsPrices.Tables(0).Rows.Count > 0 Then
                txtTotalCost.Text = dsPrices.Tables(0).Rows(0)(3)
            End If
            Return True
        Catch ex As Exception
            txtTotalCost.Text = "0"
            Return False
        End Try
    End Function
End Class
