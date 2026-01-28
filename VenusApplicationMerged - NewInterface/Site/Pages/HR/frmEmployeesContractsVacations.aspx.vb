Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeesContractsVacations
    Inherits MainPage

    Private clsContracts As Clshrs_Contracts
    Private ClsEmployees As Clshrs_Employees

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim IntEmpID As Integer = Request.QueryString.Item("EmpID")
        Dim IntContID As Integer = Request.QueryString.Item("ContID")
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

        ClsEmployees.Find("ID =" & IntEmpID)
        lblDescEmployeeCode.Text = ClsEmployees.Code
        lblDescEnglishName.Text = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, "Select dbo.fn_GetEmpName('" & ClsEmployees.Code & "'," & ObjNavigationHandler.SetLanguage(Page, "0/1") & ")")

        hdnLang.Value = ObjNavigationHandler.SetLanguage(Page, "0/1")

        If Not IsPostBack Then
            Dim IntValidContract As Integer
            Dim blnFound As Boolean = False
            clsContracts = New Clshrs_Contracts(Me.Page)

            clsContracts.Find(" Employeeid =" & IntEmpID)
            uwgContracts.DataSource = clsContracts.DataSet.Tables(0).DefaultView
            uwgContracts.DataBind()

            Dim StrSelectCommand = " Select TOP 1 EndofServiceDate from hrs_employeesJoins where employeeid = " & IntEmpID & " Order By EndofServiceDate Desc "
            Dim endServ = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, StrSelectCommand)

            For i As Integer = 0 To uwgContracts.Rows.Count - 1
                If i < uwgContracts.Rows.Count - 1 Then
                    uwgContracts.Rows(i).Cells.FromKey("EndDate").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                End If
                If Not String.IsNullOrWhiteSpace(Convert.ToString(endServ)) Then
                    uwgContracts.Rows(i).Cells.FromKey("EndDate").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                End If
            Next i

            IntValidContract = clsContracts.ContractValidatoinId(IntEmpID, Date.Now)
            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgContracts.Rows
                If (row.Cells(0).Value = IntValidContract) Then
                    row.Activate()
                    row.Activated = True
                    row.Selected = True
                    txtActivatedRowIndex.Value = row.Index
                    blnFound = True

                    Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Page)
                    Dim ObjDs As New Data.DataSet

                    uwgVacationTypes.DataSource = ClsEmployeeClasses.GetAllVacationTypes()
                    uwgVacationTypes.DataBind()
                    If clsContracts.GetContractVacation(clsContracts.ID, Nothing, 1, ObjDs) Then
                        uwgVacationDetails.DataSource = ObjDs.Tables(0).DefaultView
                        uwgVacationDetails.DataBind()
                    Else
                        uwgVacationDetails.DataSource = ClsEmployeeClasses.GetEmployeeClassVacations(clsContracts.EmployeeClassID)
                        uwgVacationDetails.DataBind()
                    End If
                    uwgVacationTypes.DisplayLayout.ActiveRow = uwgVacationTypes.Rows(0)
                    uwgVacationTypes.Rows(0).Selected = True
                    hdnVacationTypeID.Value = uwgVacationTypes.Rows(0).Cells.FromKey("VacationTypeID").Value
                    HideDetailsRows(uwgVacationTypes.Rows(0).Cells.FromKey("VacationTypeID").Value)
                End If
                '  Exit For
            Next
        End If
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click
        Dim IntContID As Integer = Request.QueryString.Item("ContID")
        Dim IntEmpID As Integer = Request.QueryString.Item("EmpID")

        clsContracts = New Clshrs_Contracts(Me.Page)
        Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Page)

        clsContracts = New Clshrs_Contracts(Me.Page)
        clsContracts.Find(" ID = " & IntContID)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsContracts.ConnectionString)
        clsContracts.SetContractVacation(IntContID, uwgVacationDetails)
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgContracts.Rows
            clsContracts = New Clshrs_Contracts(Me.Page)
            clsContracts.Find("ID = " & row.Cells.FromKey("ID").Value)
            If row.Cells.FromKey("EndDate").Text <> "" And clsContracts.StartDate > SetDate(row.Cells.FromKey("EndDate").Value, row.Cells.FromKey("EndDate").Value) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Me.Page, "Data Can't be saved ,  Invalid Contract Dates ! Check Entered Dates ! ")
                Exit Sub
            End If
            clsContracts.EndDate = SetDate(row.Cells.FromKey("EndDate").Value, row.Cells.FromKey("EndDate").Value)
            clsContracts.Update("ID = " & row.Cells.FromKey("ID").Value)

        Next
        ClsEmployeeClasses.Find("ID = " & clsContracts.EmployeeClassID & "")
        If ClsEmployeeClasses.AdvanceBalance Then
            Dim DurationDays As Decimal = 0
            Dim NewDurationDays As Decimal = 0

            Dim ds As DataSet = ClsEmployeeClasses.GetEmployeeClassAnnualVacations(clsContracts.EmployeeClassID)
            If ds.Tables(0).Rows.Count > 0 Then
                '1-Get Duration Days for new class
                DurationDays = ds.Tables(0).Rows(0)("DurationDays")
                '2- Get current Vacation balance
                Dim str As String = "select * from hrs_VacationsBalance where EmployeeID=" & IntEmpID & " and Year=" & DateTime.Now.Year & " and BalanceTypeID=1"
                Dim dsbalance As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, str)
                If dsbalance.Tables(0).Rows.Count > 0 Then
                    '3- If Vacation Token
                    Dim consumed As Decimal = dsbalance.Tables(0).Rows(0)("Consumed")
                    NewDurationDays = DurationDays - consumed
                    '4- Update Hrs_vacationsBalance
                    Dim SqlCommand As Data.SqlClient.SqlCommand
                    Dim UpdateCommand As String = ""
                    UpdateCommand = "update hrs_VacationsBalance set Balance=" & DurationDays & " , Remaining=" & NewDurationDays & " where EmployeeID=" & IntEmpID & " and Year=" & DateTime.Now.Year & " and BalanceTypeID=1 "
                    SqlCommand = New SqlClient.SqlCommand
                    SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                    SqlCommand.CommandType = CommandType.Text
                    SqlCommand.CommandText = UpdateCommand
                    SqlCommand.Connection.Open()
                    SqlCommand.ExecuteNonQuery()
                    SqlCommand.Connection.Close()

                End If



            Else
                '2- Get current Vacation balance
                Dim str As String = "select * from hrs_VacationsBalance where EmployeeID=" & IntEmpID & " and Year=" & DateTime.Now.Year & " and BalanceTypeID=1"
                Dim dsbalance As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, str)
                If dsbalance.Tables(0).Rows.Count > 0 Then
                    '3- If Vacation Token
                    Dim consumed As Decimal = dsbalance.Tables(0).Rows(0)("Consumed")
                    NewDurationDays = DurationDays - consumed
                    Dim SqlCommand As Data.SqlClient.SqlCommand
                    Dim UpdateCommand As String = ""
                    UpdateCommand = "update hrs_VacationsBalance set Balance=0 , Remaining=" & NewDurationDays & " where EmployeeID=" & IntEmpID & " and Year=" & DateTime.Now.Year & " and BalanceTypeID=1 "
                    SqlCommand = New SqlClient.SqlCommand
                    SqlCommand.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                    SqlCommand.CommandType = CommandType.Text
                    SqlCommand.CommandText = UpdateCommand
                    SqlCommand.Connection.Open()
                    SqlCommand.ExecuteNonQuery()
                    SqlCommand.Connection.Close()
                End If

            End If
        End If
        Venus.Shared.Web.ClientSideActions.ClosePage(Page, ObjNavigationHandler.SetLanguage(Page, "Save Complete Successfully / تم الحفظ بنجاح"))

        End Sub
    Private Function SetDate(gData As Object, hDate As Object) As Date
        Try

            If Convert.ToString(gData) <> "" Then
                If ClsDataAcessLayer.IsGreg(gData) Then
                    Return ClsDataAcessLayer.FormatGreg(gData, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.HijriToGreg(gData, "dd/MM/yyyy")
                End If
            ElseIf Convert.ToString(hDate) <> "" Then
                If ClsDataAcessLayer.IsHijri(hDate) Then
                    Return ClsDataAcessLayer.HijriToGreg(hDate, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.FormatGreg(hDate, "dd/MM/yyyy")
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Protected Sub btnGetVacationDefault_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnGetVacationDefault.Click
        Dim IntContID As Integer = Request.QueryString.Item("ContID")
        Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Page)

        clsContracts = New Clshrs_Contracts(Me.Page)
        clsContracts.Find(" ID = " & IntContID)

        uwgVacationDetails.DataSource = Nothing
        uwgVacationDetails.DataBind()
        uwgVacationDetails.DataSource = ClsEmployeeClasses.GetEmployeeClassAnnualVacations(clsContracts.EmployeeClassID)
        uwgVacationDetails.DataBind()
    End Sub
    Private Sub HideDetailsRows(ByVal intVacationTypeID As Integer)
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgVacationDetails.Rows
            If row.Cells.FromKey("VacationTypeID").Value = intVacationTypeID Then
                row.Hidden = False
            Else
                row.Hidden = True
            End If
        Next
    End Sub
    Private Sub CreateEmptyRows(ByVal intNoOfrows As Integer)
        For i As Integer = 0 To intNoOfrows
            uwgVacationDetails.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow())
        Next
    End Sub

End Class
