Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEvalRondomEvaluators
    Inherits MainPage

#Region "Public Decleration"

    Private clsMainOtherFields As clsSys_MainOtherFields

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim ClsDepartments As New Clssys_Departments(Page)
        Dim ClsPositions As New Clshrs_Positions(Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
        Dim SearchID As Integer = 0
        If ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'") Then
            If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                SearchID = ClsSearchs.ID
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                btnSearchCode1.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
        End If
        If Not IsPostBack Then
            txtLang.Value = ObjNavigationHandler.SetLanguage(Page, "Eng/Arb")
            Dim ds As New Data.DataSet()
            Dim strEmpName As String = ""
            Dim strEngName As String = ""
            Dim strArbName As String = ""
            Dim sqlstr As String = "select ID,Code,EngName,ArbName,FamilyEngName,FamilyArbName,FatherEngName,FatherArbName,GrandEngName,GrandArbName,E_Mail from hrs_Employees where CancelDate is null and ExcludeDate is null"
            ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, Data.CommandType.Text, sqlstr)
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                strEngName = ds.Tables(0).Rows(i)("EngName").ToString() & "," & ds.Tables(0).Rows(i)("FatherEngName").ToString() & "," & ds.Tables(0).Rows(i)("GrandEngName").ToString() & "," & ds.Tables(0).Rows(i)("FamilyEngName").ToString()
                strArbName = ds.Tables(0).Rows(i)("ArbName").ToString() & "," & ds.Tables(0).Rows(i)("FatherArbName").ToString() & "," & ds.Tables(0).Rows(i)("GrandArbName").ToString() & "," & ds.Tables(0).Rows(i)("FamilyArbName").ToString()
                If strArbName = ",,," Then
                    strArbName = strEngName
                End If
                If strEngName = ",,," Then
                    strEngName = strArbName
                End If
                If txtLang.Value = "Arb" Then
                    strEmpName = strArbName
                Else
                    strEmpName = strEngName
                End If
                uwgInterviewsDetail0.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {ds.Tables(0).Rows(i)("ID").ToString(), ds.Tables(0).Rows(i)("Code").ToString(), strEmpName, False}))
            Next i
            ClsDepartments.GetDropDownList(DDLFromDepartment, True)
            ClsDepartments.GetDropDownList(DDLToDepartment, True)
            ClsPositions.GetDropDownList(DDLFromPosition, True)
            ClsPositions.GetDropDownList(DDLToPosition, True)

            Dim Stringvalue As String = ObjNavigationHandler.SetLanguage(Page, "[Select Your Choice]/ [ إختر أحد الإختيارات ]")
            Dim IDvalue As Int32 = 0
            DdlEmployee.Items.Add(New ListItem(Stringvalue, IDvalue))
            Dim DS1 As New Data.DataSet
            clsEmployees.Find("IsNull(CancelDate,'')=''")
            DS1 = ClsEmployees.DataSet
            For i As Integer = 0 To DS1.Tables(0).Rows.Count - 1
                IDvalue = ClsEmployees.DataSet.Tables(0).Rows(i)("ID")
                Stringvalue = DS1.Tables(0).Rows(i)(ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")).ToString() & " " & DS1.Tables(0).Rows(i)(ObjNavigationHandler.SetLanguage(Page, "FatherEngName/FatherArbName")).ToString() & " " & DS1.Tables(0).Rows(i)(ObjNavigationHandler.SetLanguage(Page, "GrandEngName/GrandArbName")).ToString() & " " & DS1.Tables(0).Rows(i)(ObjNavigationHandler.SetLanguage(Page, "FamilyEngName/FamilyArbName")).ToString()
                DdlEmployee.Items.Add(New ListItem(Stringvalue, IDvalue))
            Next i
        End If
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        Dim clsEmployees As New Clshrs_Employees(Page)
        clsEmployees.Find(" Code='" & txtCode.Text & "'")
        DdlEmployee.SelectedValue = Convert.ToString(clsEmployees.ID)
    End Sub
    Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFilter.Click
        For Each row1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgInterviewsDetail0.Rows
            If CheckEmployeeByFilter(row1.Cells(0).Value.ToString()) = False Then
                row1.Hidden = True
                row1.Cells(3).Value = False
            Else
                row1.Hidden = False
                row1.Cells(3).Value = False
            End If
        Next
        DDLFromPosition.SelectedValue = 0
        DDLFromDepartment.SelectedValue = 0
        DDLToDepartment.SelectedValue = 0
        DDLToPosition.SelectedValue = 0
    End Sub
    Protected Sub ImageButton_Print_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_Print.Click
        Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
    End Sub

#End Region

#Region "Public Function"

    Public Function CheckEmployeeByFilter(ByVal EmployeeeID As String) As Boolean
        Dim ClsEvaluationCompainTypes As New ClsEval_EvaluationCompainTypes(Page)
        Dim Whr As String = "hrs_Employees.CancelDate Is Null And hrs_contracts.CancelDate Is Null And hrs_contracts.ID = (Select Top 1 Cont.ID  From hrs_Contracts Cont Where Cont.EmployeeID = hrs_Contracts.EmployeeID Order by IsNull(Cont.EndDate,30/12/2070) Desc) and hrs_Employees.ID = " & EmployeeeID
        If DDLFromDepartment.SelectedValue <> 0 And DDLToDepartment.SelectedValue <> 0 Then
            Whr = Whr & " and hrs_Employees.DepartmentID between " & DDLFromDepartment.SelectedValue & " and " & DDLToDepartment.SelectedValue
        End If
        If DDLFromPosition.SelectedValue <> 0 And DDLToPosition.SelectedValue <> 0 Then
            Whr = Whr & " and hrs_Contracts.PositionID between " & DDLFromPosition.SelectedValue & " and " & DDLToPosition.SelectedValue
        End If
        Dim sqlstr As String = "select hrs_Employees.ID As ID,hrs_contracts.ID As ContractID from hrs_Employees Inner Join hrs_contracts On hrs_contracts.EmployeeID = hrs_Employees.ID where " & Whr
        Dim DS As Data.DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEvaluationCompainTypes.ConnectionString, Data.CommandType.Text, sqlstr)
        If DS.Tables(0).Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function

#End Region

End Class
