Imports System.Data
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class _Tasks
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadEmployeeProfile()
    End Sub
    Protected Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
        LoadEmployeeProfile()
    End Sub
    Protected Sub LoadEmployeeProfile()
        Label1.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "أخر تحديث : ", "Last Update : ") & DateTime.Now.ToString()
        RadioButton_Month.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "أخر شهر", "Last Month")
        RadioButton_All.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "كل البيانات", "All Data")
        RadioButton_Week.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "أخر أسبوع", "Last Week")
        RadioButton_Today.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "اليوم", "Today")

        Dim strDatefilter As String = ""
        If RadioButton_Month.Checked = True Then
            strDatefilter = " and RegDate >= CONVERT(Date,'" & DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy") & "')"
        ElseIf RadioButton_Week.Checked = True Then
            strDatefilter = " and RegDate >= CONVERT(Date,'" & DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy") & "')"
        ElseIf RadioButton_Today.Checked = True Then
            strDatefilter = " and RegDate >= CONVERT(Date,'" & DateTime.Now.ToString("dd/MM/yyyy") & "')"
        End If

        Dim ClsTasks As New Clssys_Tasks(Me.Page)
        ClsTasks.Find("RegUserID = " & ClsTasks.DataBaseUserRelatedID & strDatefilter & " order by RegDate DESC")
        TreeView_Tasks.Nodes.Clear()
        For Each rw As Data.DataRow In ClsTasks.DataSet.Tables(0).Rows
            Dim isnew As Boolean = False
            Dim tn As New Infragistics.Web.UI.NavigationControls.DataTreeNode
            tn.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "المهمة : ", "Task : ")
            tn.Text &= IIf(ProfileCls.CurrentLanguage() = "Ar", rw("ArbName"), rw("EngName"))
            Dim strCommand As String = "select * from sys_TasksInvolve where TaskID = " & ClsTasks.ID
            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsTasks.ConnectionString, CommandType.Text, strCommand)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim HrsEmployees As New Clshrs_Employees(Me)
                If HrsEmployees.Find1("ID in (select RelEmployee from sys_Users where ID = '" & ds.Tables(0).Rows(0)("UserID") & "')") Then
                    tn.Text &= " - " & IIf(ProfileCls.CurrentLanguage() = "Ar", " أرسلت إلى : ", " Sent To : ") & HrsEmployees.FullName
                End If
                If Convert.ToBoolean(IIf(String.IsNullOrEmpty(Convert.ToString(ds.Tables(0).Rows(0)("IsRecieved"))), False, True)) = True Then
                    tn.Text &= " - " & IIf(ProfileCls.CurrentLanguage() = "Ar", "تم الإستلام فى : '" & Convert.ToDateTime(ds.Tables(0).Rows(0)("RecieveDate")).ToString("dd/MM/yyyy HH:mm:ss") & "'", "Recieved At : '" & Convert.ToDateTime(ds.Tables(0).Rows(0)("RecieveDate")).ToString("dd/MM/yyyy HH:mm:ss") & "'")
                Else
                    tn.Text &= " - " & IIf(ProfileCls.CurrentLanguage() = "Ar", "لم تستلم الى الأن", "Not Recieved Yet")
                End If
                Dim strCommand1 As String = "select * from sys_TasksInvolveSteps where isnull(IsRecieved,0) = 0 and StatusID in (4,5,6) and TaskInvolveID = " & ds.Tables(0).Rows(0)("ID")
                Dim ds1 As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsTasks.ConnectionString, CommandType.Text, strCommand1)
                If ds1.Tables(0).Rows.Count > 0 Then
                    tn.Text &= " - " & IIf(ProfileCls.CurrentLanguage() = "Ar", "توجد تحديثات فى الأمر المرسل", "There is Updates To Sent Task")
                    isnew = True
                End If
                Dim strCommand2 As String = "select * from sys_TasksChat where isnull(ToOwner,0) = 1 and isnull(IsRecieved,0) = 0 and TaskInvolveID = " & ds.Tables(0).Rows(0)("ID")
                Dim ds2 As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsTasks.ConnectionString, CommandType.Text, strCommand2)
                If ds2.Tables(0).Rows.Count > 0 Then
                    isnew = True
                End If
            End If
            If isnew = True Then
                tn.Text &= " - " & "<img src=""New.gif"" />"
            End If
            tn.ImageUrl = "../../../Common/Images/SServices/Task.png"
            tn.Value = rw("ID")
            TreeView_Tasks.Nodes.Add(tn)
        Next

        ClsTasks = New Clssys_Tasks(Me.Page)
        ClsTasks.Find("IsOpen = 1 and ExpiryDate > getdate() and ID in (select TaskID from sys_TasksInvolve where UserID = " & ClsTasks.DataBaseUserRelatedID & ") " & strDatefilter & " order by RegDate DESC")
        TreeView_Missions.Nodes.Clear()
        For Each rw As Data.DataRow In ClsTasks.DataSet.Tables(0).Rows
            Dim isnew As Boolean = False
            Dim tn As New Infragistics.Web.UI.NavigationControls.DataTreeNode
            tn.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "المهمة : " & rw("ArbName") & " - مطلوب إنجازها قبل '" & ClsTasks.ExpiryDate.ToString("dd/MM/yyyy") & " " & ClsTasks.ExpiryTime & "' إضغط للاطلاع على التفاصيل و لإضافة التطورات", "Mission : " & rw("EngName") & " - Required accomplished Before '" & ClsTasks.ExpiryDate.ToString("dd/MM/yyyy") & " " & ClsTasks.ExpiryTime & "' Click To Preview Details and Register Progress")
            Dim strCommand As String = "select * from sys_TasksInvolve where isnull(IsRecieved,0) = 0 and TaskID = " & ClsTasks.ID
            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsTasks.ConnectionString, CommandType.Text, strCommand)
            If ds.Tables(0).Rows.Count > 0 Then
                isnew = True
            End If
            Dim strCommand1 As String = "select * from sys_TasksChat where isnull(ToClient,0) = 1 and isnull(IsRecieved,0) = 0 and TaskInvolveID in (select ID from sys_TasksInvolve where UserID = " & ClsTasks.DataBaseUserRelatedID & " and TaskID = " & ClsTasks.ID & ")"
            Dim ds1 As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsTasks.ConnectionString, CommandType.Text, strCommand1)
            If ds1.Tables(0).Rows.Count > 0 Then
                isnew = True
            End If
            If isnew = True Then
                tn.Text &= " - " & "<img src=""New.gif"" />"
            End If
            tn.ImageUrl = "../../../Common/Images/SServices/Mission.png"
            tn.Value = rw("ID")
            TreeView_Missions.Nodes.Add(tn)
        Next
    End Sub
End Class
