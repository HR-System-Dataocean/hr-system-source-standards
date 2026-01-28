Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class TasksMonitor
    Inherits MainPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim ClsTasks As New Clssys_Tasks(Me.Page)
            ClsTasks.Find("ID = " & Request.QueryString("ID"))
            txtExpiryDate.Value = ClsTasks.ExpiryDate.ToString("ddMMyyyy")
            txtExpiryTime.Text = ClsTasks.ExpiryTime
            txtArbName.Text = ClsTasks.ArbName
            txtEngName.Text = ClsTasks.EngName
            TextBox_Details.Text = ClsTasks.Details
            Dim ClsObjects As New Clssys_Objects(Page)
            If ClsObjects.Find(" Code='" & ClsTasks.Table.ToString.Trim() & "'") Then
                iframe1.Attributes("Src") = "../../HR/frmAttachDocuments.aspx?OId=" & ClsObjects.ID & "&RId=" & ClsTasks.ID & "&Par1=0&Par2=0"
            End If
            If ClsTasks.IsOpen = False Then
                Label_Warning.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "المهمة موقوفة", "Stopped Task")
                LinkButton2.Visible = False
            End If

            Dim HrsEmployees As New Clshrs_Employees(Me)
            If HrsEmployees.Find1("ID in (select RelEmployee from sys_Users where ID in (select UserID from sys_TasksInvolve where TaskID = " & ClsTasks.ID & "))") Then
                Label_Sent.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "المهمة أرسلت الى : ", "The Mission Sent To : ") & HrsEmployees.FullName
            End If

            TreeView_Achievement.Nodes.Clear()
            Dim strCommand1 As String = "select TrnsDatetime,Details,AchievePCT,StatusID,(select ArbName from hrs_Enum where Flag = 5 and Code = StatusID) StArb,(select EngName from hrs_Enum where Flag = 5 and Code = StatusID) StEng from sys_TasksInvolveSteps where TaskInvolveID in (select ID from sys_TasksInvolve where TaskID = " & ClsTasks.ID & ")"
            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsTasks.ConnectionString, CommandType.Text, strCommand1)
            For Each dr As DataRow In ds.Tables(0).Rows
                Dim tn As New Infragistics.Web.UI.NavigationControls.DataTreeNode
                If dr("StatusID") = 4 Then
                    tn.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "المرحلة : " & dr("StArb") & " التاريخ : '" & Convert.ToDateTime(dr("TrnsDatetime")).ToString("dd/MM/yyyy HH:mm:ss") & "' - " & dr("Details") & " نسبة الإنجاز = " & dr("AchievePCT") & "%", "Stage : " & dr("StEng") & " Date : '" & Convert.ToDateTime(dr("TrnsDatetime")).ToString("dd/MM/yyyy HH:mm:ss") & "' - " & dr("Details") & " Achievement PCT = " & dr("AchievePCT") & "%")
                Else
                    tn.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "المرحلة : " & dr("StArb") & " التاريخ : '" & Convert.ToDateTime(dr("TrnsDatetime")).ToString("dd/MM/yyyy HH:mm:ss") & "' - " & dr("Details"), "Stage : " & dr("StEng") & " Date : '" & Convert.ToDateTime(dr("TrnsDatetime")).ToString("dd/MM/yyyy HH:mm:ss") & "' - " & dr("Details"))
                End If
                TreeView_Achievement.Nodes.Add(tn)
            Next

            WebDataTree_Discussion.Nodes.Clear()
            strCommand1 = "select Details,RegDate,isnull(ToOwner,0) AS ToOwner,isnull(ToClient,0) AS ToClient from sys_TasksChat where TaskInvolveID in (select ID from sys_TasksInvolve where TaskID = " & ClsTasks.ID & ")"
            ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsTasks.ConnectionString, CommandType.Text, strCommand1)
            For Each dr As DataRow In ds.Tables(0).Rows
                Dim tn As New Infragistics.Web.UI.NavigationControls.DataTreeNode
                If dr("ToOwner") = "True" Then
                    tn.Text = "'" & Convert.ToDateTime(dr("RegDate")).ToString("dd/MM/yyyy HH:mm:ss") & "' - " & IIf(ProfileCls.CurrentLanguage() = "Ar", "<u><font color='red'>منفذ المهمة</font></u> : " & dr("Details"), "<u><font color='red'>Mission Reciever</font></u> : " & dr("Details"))
                End If
                If dr("ToClient") = "True" Then
                    tn.Text = "'" & Convert.ToDateTime(dr("RegDate")).ToString("dd/MM/yyyy HH:mm:ss") & "' - " & IIf(ProfileCls.CurrentLanguage() = "Ar", "أنا : " & dr("Details"), "Me : " & dr("Details"))
                End If
                WebDataTree_Discussion.Nodes.Add(tn)
            Next

            Dim Str As String = "update sys_TasksChat set IsRecieved = 1,RecieveDate = getdate() where RecieveDate is null and ToOwner = 1 and TaskInvolveID in (select ID from sys_TasksInvolve where TaskID = " & ClsTasks.ID & ")"
            Str &= "update sys_TasksInvolveSteps set IsRecieved = 1,RecieveDate = getdate() where RecieveDate is null and TaskInvolveID in (select ID from sys_TasksInvolve where TaskID = " & ClsTasks.ID & ")"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsTasks.ConnectionString, Data.CommandType.Text, Str)
        End If
    End Sub
    Protected Sub LinkButton1_Click(sender As Object, e As System.EventArgs) Handles LinkButton1.Click
        Dim ClsTasks As New Clssys_Tasks(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsTasks.ConnectionString)
        Dim Str As String = "delete from sys_Tasks where ID = " & Request.QueryString("ID")
        If Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsTasks.ConnectionString, Data.CommandType.Text, Str) > 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Done /تمت العملية "))
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Failed /فشل العملية "))
        End If
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As System.EventArgs) Handles LinkButton2.Click
        Dim ClsTasks As New Clssys_Tasks(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsTasks.ConnectionString)
        Dim Str As String = "update sys_Tasks set IsOpen = 0 where ID = " & Request.QueryString("ID")
        If Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsTasks.ConnectionString, Data.CommandType.Text, Str) > 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Done /تمت العملية "))
            Label_Warning.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "المهمة موقوفة", "Stopped Task")
            LinkButton2.Visible = False
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Failed /فشل العملية "))
        End If
    End Sub

    Protected Sub LinkButton_Send_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Send.Click
        Dim ClsTasks As New Clssys_Tasks(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsTasks.ConnectionString)
        Dim Str As String = "insert into sys_TasksChat (TaskInvolveID,ToOwner,ToClient,Details,RegDate) values ((select top 1 ID from sys_TasksInvolve where TaskID = " & Request.QueryString(0) & "),0,1,'" & TextBox_Msg.Text & "',getdate())"
        If Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsTasks.ConnectionString, Data.CommandType.Text, Str) > 0 Then
            TextBox_Msg.Text = ""
            TextBox_Msg.Focus()
            WebDataTree_Discussion.Nodes.Clear()
            Dim strCommand1 As String = "select Details,RegDate,isnull(ToOwner,0) AS ToOwner,isnull(ToClient,0) AS ToClient from sys_TasksChat where TaskInvolveID in (select ID from sys_TasksInvolve where TaskID = " & Request.QueryString(0) & ")"
            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsTasks.ConnectionString, CommandType.Text, strCommand1)
            For Each dr As DataRow In ds.Tables(0).Rows
                Dim tn As New Infragistics.Web.UI.NavigationControls.DataTreeNode
                If dr("ToOwner") = "True" Then
                    tn.Text = "'" & Convert.ToDateTime(dr("RegDate")).ToString("dd/MM/yyyy HH:mm:ss") & "' - " & IIf(ProfileCls.CurrentLanguage() = "Ar", "<u><font color='red'>منفذ المهمة</font></u> : " & dr("Details"), "<u><font color='red'>Mission Reciever</font></u> : " & dr("Details"))
                End If
                If dr("ToClient") = "True" Then
                    tn.Text = "'" & Convert.ToDateTime(dr("RegDate")).ToString("dd/MM/yyyy HH:mm:ss") & "' - " & IIf(ProfileCls.CurrentLanguage() = "Ar", "أنا : " & dr("Details"), "Me : " & dr("Details"))
                End If
                WebDataTree_Discussion.Nodes.Add(tn)
            Next
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Failed /فشل العملية "))
        End If
    End Sub
End Class
