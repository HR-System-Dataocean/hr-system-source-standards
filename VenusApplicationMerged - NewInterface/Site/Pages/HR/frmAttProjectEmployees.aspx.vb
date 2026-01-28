Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmAttProjectEmployees
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ClsObjects As New Clssys_Objects(Page)
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            Dim ClsSearchs As New Clssys_Searchs(Page)
            If ClsObjects.Find(" Code='" & ClsEmployee.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

            If Not IsPostBack Then
                txtTrnsDate.Value = DateTime.Now.ToString("ddMMyyyy")
                Dim ClsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                ClsProjects.GetDropDownList(ddlProject, True, "CancelDate is null")
                Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)


                GetData()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetData() As Boolean
        Try
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Dim strCommand As String
            Dim Ds As DataSet
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Dim ClsProjects As New Clshrs_Projects(Me, "hrs_Projects")
            If ddlProject.SelectedValue > 0 Then
                ClsProjects.Find("CancelDate is null and ID=" & ddlProject.SelectedValue)
                strCommand = "Set DateFormat DMY; Select emp.ID as ID ,Code, dbo.fn_GetEmpName(Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS FullName from Att_ProjectEmployees Attp inner join hrs_employees emp on emp.id=attp.EmployeeID   inner join(Select EmployeeID From hrs_Contracts Where CancelDate Is Null And ID = (Select Top 1 Cont.ID  From hrs_Contracts Cont Where cont.StartDate <= '" & txtTrnsDate.Text & "' and IsNull(cont.EndDate, '30/12/2070') >= '" & txtTrnsDate.Text & "' and Cont.EmployeeID = hrs_Contracts.EmployeeID And cont.CancelDate Is Null Order by IsNull(Cont.EndDate,'30/12/2070') Desc) ) Contracts On emp.ID = Contracts.EmployeeID where  Attp.FromDate <=  '" & txtTrnsDate.Text & "' and (Attp.ToDate is null or Attp.ToDate >= '" & txtTrnsDate.Text & "')  And  attp.ProjectID =" & ddlProject.SelectedValue & "  and attp.canceldate is null order by emp.Code"

                Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, System.Data.CommandType.Text, strCommand)
                UwgEmployeesProject.DataSource = Ds
                UwgEmployeesProject.DataBind()


            End If
            If txtCode.Text <> "" Then
                ClsProjects.Find("CancelDate is null and ID=" & ddlProject.SelectedValue)
                strCommand = "Set DateFormat DMY; Select emp.ID as ID ,Code, dbo.fn_GetEmpName(Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS FullName,Attp.ProjectID as ProjectID from Att_ProjectEmployees Attp inner join hrs_employees emp on emp.id=attp.EmployeeID   inner join(Select EmployeeID From hrs_Contracts Where CancelDate Is Null And ID = (Select Top 1 Cont.ID  From hrs_Contracts Cont Where cont.StartDate <= '" & txtTrnsDate.Text & "' and IsNull(cont.EndDate, '30/12/2070') >= '" & txtTrnsDate.Text & "' and Cont.EmployeeID = hrs_Contracts.EmployeeID And cont.CancelDate Is Null Order by IsNull(Cont.EndDate,'30/12/2070') Desc) ) Contracts On emp.ID = Contracts.EmployeeID where  Attp.FromDate <=  '" & txtTrnsDate.Text & "' and (Attp.ToDate is null or Attp.ToDate >= '" & txtTrnsDate.Text & "')  And  emp.code ='" & txtCode.Text & "'  and attp.canceldate is null order by emp.Code"
                Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, System.Data.CommandType.Text, strCommand)
                If Ds.Tables(0).Rows.Count > 0 Then
                    UwgEmployeesProject.DataSource = Ds
                    UwgEmployeesProject.DataBind()
                    ddlProject.SelectedValue = Ds.Tables(0).Rows(0)("ProjectID")
                End If

            End If
            If ddlProjectTransfer.SelectedValue > 0 Then

                strCommand = "Set DateFormat DMY; Select Attp.ID as AttProjectID, emp.ID as ID, Code, dbo.fn_GetEmpName(Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS FullName,dbo.fn_GetPreviousProject(FromDate,attp.EmployeeID," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ")PreviousProject from Att_ProjectEmployees Attp inner join hrs_employees emp on emp.id=attp.EmployeeID   inner join(Select EmployeeID From hrs_Contracts Where CancelDate Is Null And ID = (Select Top 1 Cont.ID  From hrs_Contracts Cont Where cont.StartDate <= '" & txtTrnsDate.Text & "' and IsNull(cont.EndDate, '30/12/2070') >= '" & txtTrnsDate.Text & "' and Cont.EmployeeID = hrs_Contracts.EmployeeID And cont.CancelDate Is Null Order by IsNull(Cont.EndDate,'30/12/2070') Desc) ) Contracts On emp.ID = Contracts.EmployeeID where  Attp.FromDate <=  '" & txtTrnsDate.Text & "' and (Attp.ToDate is null or Attp.ToDate >= '" & txtTrnsDate.Text & "')  And  attp.ProjectID =" & ddlProjectTransfer.SelectedValue & "  and attp.canceldate is null order by emp.Code"
                Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, System.Data.CommandType.Text, strCommand)
                uwgEmployeesprojectTransfer.DataSource = Ds
                uwgEmployeesprojectTransfer.DataBind()


            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Protected Sub btnPrint_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnPrint.Click
        Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
    End Sub

    'Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
    '    Try

    '        Dim ClsEmployee As New Clshrs_Employees(Page)
    '        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
    '        Dim str As String = "Set DateFormat DMY; "

    '        If ddlProjectTransfer.SelectedValue > 0 Then
    '            For Each ObjRow In UwgEmployeesProject.Rows
    '                If ObjRow.Cells(1).Value Then

    '                    str &= " update Att_ProjectEmployees set ToDate = '" & Convert.ToDateTime(txtTrnsDate.Text).AddDays(-1).ToString("dd/MM/yyyy") & "' where Todate is null and EmployeeID = " & ObjRow.Cells(0).Value & ";"
    '                    str &= " INSERT INTO Att_ProjectEmployees (EmployeeID,ProjectID,FromDate,RegDate,RegUserID) VALUES (" & ObjRow.Cells(0).Value & "," & ddlProjectTransfer.SelectedValue & ",'" & txtTrnsDate.Text & "',getdate()," & ClsEmployee.DataBaseUserRelatedID & ") ;"

    '                End If

    '            Next

    '            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, str)
    '        Else
    '            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "please select project  !/برجاء اختيار المشروع الذى سيتم نقل الموظف عليه"))
    '        End If


    '    Catch ex As Exception
    '    End Try
    '    GetData()
    'End Sub

    Protected Sub ddlProject_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProject.SelectedIndexChanged
        GetData()
        Dim ClsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        ClsProjects.GetDropDownList(ddlProjectTransfer, True, "CancelDate is null and ID <>" & ddlProject.SelectedValue)
    End Sub

    Protected Sub txtTrnsDate_ValueChange(sender As Object, e As Infragistics.WebUI.WebDataInput.ValueChangeEventArgs) Handles txtTrnsDate.ValueChange
        GetData()
    End Sub

    Protected Sub txtCode_TextChanged(sender As Object, e As System.EventArgs) Handles txtCode.TextChanged
        GetData()
    End Sub
    Protected Sub ddlProjectTransfer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProjectTransfer.SelectedIndexChanged
        Try
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Dim strCommand As String
            If ddlProjectTransfer.SelectedValue > 0 Then
                Dim ClsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
                strCommand = "Set DateFormat DMY; Select Attp.ID as AttProjectID, emp.ID as ID, Code, dbo.fn_GetEmpName(Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS FullName,dbo.fn_GetPreviousProjectID(FromDate,attp.EmployeeID) PreviousProjectID,dbo.fn_GetPreviousProject(FromDate,attp.EmployeeID," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ")PreviousProject,attp.fromdate  as fromdate from Att_ProjectEmployees Attp inner join hrs_employees emp on emp.id=attp.EmployeeID   inner join(Select EmployeeID From hrs_Contracts Where CancelDate Is Null And ID = (Select Top 1 Cont.ID  From hrs_Contracts Cont Where cont.StartDate <= '" & txtTrnsDate.Text & "' and IsNull(cont.EndDate, '30/12/2070') >= '" & txtTrnsDate.Text & "' and Cont.EmployeeID = hrs_Contracts.EmployeeID And cont.CancelDate Is Null Order by IsNull(Cont.EndDate,'30/12/2070') Desc) ) Contracts On emp.ID = Contracts.EmployeeID where  Attp.FromDate <=  '" & txtTrnsDate.Text & "' and (Attp.ToDate is null or Attp.ToDate >= '" & txtTrnsDate.Text & "')  And  attp.ProjectID =" & ddlProjectTransfer.SelectedValue & "  and attp.canceldate is null order by emp.Code"
                Dim Ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, System.Data.CommandType.Text, strCommand)
                uwgEmployeesprojectTransfer.DataSource = Ds
                uwgEmployeesprojectTransfer.DataBind()


            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ImageButton_Save_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton_Save.Click
        Try

            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Dim str As String = "Set DateFormat DMY; "

            If ddlProjectTransfer.SelectedValue > 0 Then
                For Each ObjRow In UwgEmployeesProject.Rows
                    If ObjRow.Cells(1).Value Then

                        str &= " update Att_ProjectEmployees set ToDate = '" & Convert.ToDateTime(txtTrnsDate.Text).AddDays(-1).ToString("dd/MM/yyyy") & "' where Todate is null and EmployeeID = " & ObjRow.Cells(0).Value & ";"
                        str &= " INSERT INTO Att_ProjectEmployees (EmployeeID,ProjectID,FromDate,RegDate,RegUserID) VALUES (" & ObjRow.Cells(0).Value & "," & ddlProjectTransfer.SelectedValue & ",'" & txtTrnsDate.Text & "',getdate()," & ClsEmployee.DataBaseUserRelatedID & ") ;"

                    End If

                Next

                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, str)
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "please select project  !/برجاء اختيار المشروع الذى سيتم نقل الموظف عليه"))
            End If


        Catch ex As Exception
        End Try
        GetData()
    End Sub
    Protected Sub uwgEmployeesprojectTransfer_ClickCellButton(sender As Object, e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles uwgEmployeesprojectTransfer.ClickCellButton
        'delete the employee from project and return to prevouis one
        Dim str As String
        Dim ClsEmployee As New Clshrs_Employees(Page)

        Dim AttID As Integer = e.Cell.Row.Cells(7).Value
        Dim Employeeid As Integer = e.Cell.Row.Cells(0).Value
        Dim PreviousProjectID As Integer = e.Cell.Row.Cells(8).Value
        str = "set datefromat dmy; update Att_ProjectEmployees set canceldate=getdate() where id=" & AttID
        str &= ";  update Att_ProjectEmployees set Todate =NULL where employeeid=" & Employeeid & "  and FromDate <="
        'Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, str)

    End Sub
End Class
