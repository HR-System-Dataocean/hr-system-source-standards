Imports System.Data
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class _WFAlerts
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadDocumentAlerts()
    End Sub
    Protected Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
        LoadDocumentAlerts()
    End Sub
    Protected Sub LoadDocumentAlerts()
        Label1.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "أخر تحديث : ", "Last Update : ") & DateTime.Now.ToString()
        RadioButton_Month.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "أخر شهر", "Last Month")
        RadioButton_All.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "كل البيانات", "All Data")
        RadioButton_Week.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "أخر أسبوع", "Last Week")
        RadioButton_Today.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "اليوم", "Today")

        WebDataTree_DocumentsAlerts.Nodes.Clear()
        Dim DTMNode As New Infragistics.Web.UI.NavigationControls.DataTreeNode
        Dim _Dwf_DocumentWorkFow As New ClsDwf_DocumentWorkFow(Me.Page)
        Dim strDatefilter As String = ""
        If RadioButton_Month.Checked = True Then
            strDatefilter = "and RegDate >= CONVERT(Date,'" & DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy") & "')"
        ElseIf RadioButton_Week.Checked = True Then
            strDatefilter = "and RegDate >= CONVERT(Date,'" & DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy") & "')"
        ElseIf RadioButton_Today.Checked = True Then
            strDatefilter = "and RegDate >= CONVERT(Date,'" & DateTime.Now.ToString("dd/MM/yyyy") & "')"
        End If

        If _Dwf_DocumentWorkFow.Find("PeopleID = " & ProfileCls.RetRefPeople() & "  " & strDatefilter & " order by RegDate Desc") Then
            Dim DT As DataTable = _Dwf_DocumentWorkFow.DataSet.Tables(0)
            For i As Integer = 0 To DT.Rows.Count - 1
                _Dwf_DocumentWorkFow.Find("ID = " & DT.Rows(i)("ID"))
                Dim _sys_Forms As New ClsSys_Forms(Me.Page)
                _sys_Forms.Find("Code = 'Cus_" & _Dwf_DocumentWorkFow.DocumentID & "'")
                Dim DT01 As DataTable = _sys_Forms.DataSet.Tables(0)
                Dim _Dwf_DocumentStage As New ClsDwf_DocumentStages(Me.Page)
                _Dwf_DocumentStage.Find("ID = " & _Dwf_DocumentWorkFow.StageID)
                If DT.Rows.Count > 0 Then
                    _sys_Forms.Find("ID = " & DT01.Rows(0)("ID"))
                    DTMNode = New Infragistics.Web.UI.NavigationControls.DataTreeNode
                    Dim HrsEmployees As New Clshrs_Employees(Me)
                    HrsEmployees.Find1("ID = " & _Dwf_DocumentWorkFow.InitPeople)

                    DTMNode.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", _Dwf_DocumentWorkFow.MessageArabic & "</br> تخص الموظف : ", _Dwf_DocumentWorkFow.MessageEnglish & "</br>Belong To : ") & HrsEmployees.FullName
                    If _Dwf_DocumentWorkFow.ActionID = Nothing Then
                        DTMNode.ImageUrl = "../../../Common/Images/Alarms.png"
                    ElseIf _Dwf_DocumentWorkFow.ActionID = 2 Then
                        DTMNode.ImageUrl = "../../../Common/Images/Alerts/Success.png"
                    Else _Dwf_DocumentWorkFow.ActionID = 3
                        DTMNode.ImageUrl = "../../../Common/Images/Alerts/Failled.png"
                    End If
                    Dim LinkString As String = "../../../" & _sys_Forms.LinkUrl & "&Stage=" & _Dwf_DocumentStage.Code & "&TrnsID=" & _Dwf_DocumentWorkFow.TrnsEntryCode & "&WF=" & _Dwf_DocumentWorkFow.ID
                    DTMNode.Value = LinkString
                    WebDataTree_DocumentsAlerts.Nodes.Add(DTMNode)
                End If
            Next
        End If
        WebDataTree_DocumentsAlerts.DataBind()
    End Sub
End Class
