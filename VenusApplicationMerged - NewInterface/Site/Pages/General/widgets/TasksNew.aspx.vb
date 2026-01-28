Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class TasksNew
    Inherits MainPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HiddenField_CID.Value = DateTime.Now.Ticks
            Dim HrsEmployees As New Clshrs_Employees(Me)
            HrsEmployees.Find1("ID = '" & ProfileCls.RetRefPeople & "' or ManagerID = '" & ProfileCls.RetRefPeople & "' and ID in (select RelEmployee from sys_Users)")
            For Each rw As Data.DataRow In HrsEmployees.DataSet.Tables(0).Rows
                Dim SysUsers As New Clssys_Users(Me.Page)
                SysUsers.Find("RelEmployee = " & rw("ID"))
                Dim tn As New TreeNode(rw("FullName"), SysUsers.ID)
                TreeView_Subordinates.Nodes.Add(tn)
            Next
        End If
        TreeView_Subordinates.Attributes.Add("OnClick", "client_OnTreeNodeChecked(event)")
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsTasks As New Clssys_Tasks(Me.Page)
        If ClsObjects.Find(" Code='" & ClsTasks.Table.ToString.Trim() & "'") Then
            iframe1.Attributes("Src") = "../../HR/frmAttachDocuments.aspx?OId=" & ClsObjects.ID & "&RId=" & HiddenField_CID.Value
        End If
        txtExpiryDate.Focus()
    End Sub
    Protected Sub ImageButton_Save_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_Save.Click
        Dim ClsTasks As New Clssys_Tasks(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsTasks.ConnectionString)
        Dim empcnt As Integer = 0
        For Each tn As TreeNode In TreeView_Subordinates.Nodes
            If tn.Checked = True Then
                empcnt = empcnt + 1
            End If
        Next
        If empcnt = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Subordinates Required /يجب إختيار مرؤوسين "))
            Exit Sub
        End If
        Dim Str As String = "Set Dateformat DMY; Declare @CID Int; Insert Into sys_Tasks values (" & HiddenField_CID.Value & ",'" & HiddenField_CID.Value & "','" & SetDate(txtExpiryDate.Text, txtExpiryDate.Text) & "','" & IIf(txtExpiryTime.Text = "", "23:59", txtExpiryTime.Text) & "','" & txtEngName.Text & "','" & txtArbName.Text & "','" & txtArbName.Text & "','" & TextBox_Details.Text & "',1,''," & ClsTasks.DataBaseUserRelatedID & ",NULL,getdate(),NULL)"
        For Each tn As TreeNode In TreeView_Subordinates.Nodes
            If tn.Checked = True Then
                Str = Str & Environment.NewLine & "insert into sys_TasksInvolve (TaskID,UserID) values (" & HiddenField_CID.Value & "," & tn.Value & "); set @CID = (Select SCOPE_IDENTITY());"
                Str = Str & Environment.NewLine & "insert into sys_TasksInvolveSteps (TaskInvolveID,TrnsDatetime,Details,StatusID) values (@CID,getdate(),'',1);"
            End If
        Next
        If Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsTasks.ConnectionString, Data.CommandType.Text, Str) > 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Done /تمت العملية "))
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Failed /فشل العملية "))
        End If
    End Sub
    Protected Sub ImageButton_New_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_New.Click
        HiddenField_CID.Value = DateTime.Now.Ticks
    End Sub
    Private Function SetDate(gData As Object, hDate As Object) As Date
        Try
            If gData <> "  /  /    " Then
                If ClsDataAcessLayer.IsGreg(gData) Then
                    Return ClsDataAcessLayer.FormatGreg(gData, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.HijriToGreg(gData, "dd/MM/yyyy")
                End If
            ElseIf hDate <> "  /  /    " Then
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
End Class
