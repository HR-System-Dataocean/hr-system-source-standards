Imports System.Data
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class _WFTracing
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadDocumentTracing()
    End Sub
    Protected Sub Timer2_Tick(sender As Object, e As System.EventArgs) Handles Timer2.Tick
        LoadDocumentTracing()
    End Sub
    Protected Sub LoadDocumentTracing()
        'Get Dist User Documents
        Label1.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "أخر تحديث : ", "Last Update : ") & DateTime.Now.ToString()
        RadioButton_Month.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "أخر شهر", "Last Month")
        RadioButton_All.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "كل البيانات", "All Data")
        RadioButton_Week.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "أخر أسبوع", "Last Week")
        RadioButton_Today.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "اليوم", "Today")

        Dim _Clshrs_Positions As New Clshrs_Positions(Page)
        Dim strDatefilter As String = ""
        If RadioButton_Month.Checked = True Then
            strDatefilter = "and RegDate >= CONVERT(Date,'" & DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy") & "')"
        ElseIf RadioButton_Week.Checked = True Then
            strDatefilter = "and RegDate >= CONVERT(Date,'" & DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy") & "')"
        ElseIf RadioButton_Today.Checked = True Then
            strDatefilter = "and RegDate >= CONVERT(Date,'" & DateTime.Now.ToString("dd/MM/yyyy") & "')"
        End If
        Dim Strcommand1 As String = "set dateformat dmy; select Distinct TrnsEntryCode,(select Min(C.RegDate) from Dwf_DocumentWorkFow C where C.TrnsEntryCode = A.TrnsEntryCode) As LastUpdate,(select Max(B.RegDate) from Dwf_DocumentWorkFow B where B.TrnsEntryCode = A.TrnsEntryCode) As LastUpdate,DocumentID from Dwf_DocumentWorkFow A where InitPeople = '" & ProfileCls.RetRefPeople() & "' " & strDatefilter & " order by TrnsEntryCode Desc"
        Dim DT As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Clshrs_Positions.ConnectionString, System.Data.CommandType.Text, Strcommand1).Tables(0)
        WebDataTree_DocumentsTracing.Nodes.Clear()
        Dim DTMNode As New Infragistics.Web.UI.NavigationControls.DataTreeNode
        Dim DTCNode As New Infragistics.Web.UI.NavigationControls.DataTreeNode
        For x As Integer = 0 To DT.Rows.Count - 1
            DTMNode = New Infragistics.Web.UI.NavigationControls.DataTreeNode

            Dim _Dwf_Documents As New ClsDwf_Documents(Me.Page)
            _Dwf_Documents.Find("ID = " & DT.Rows(x)(3).ToString())

            DTMNode.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", _Dwf_Documents.ArbName & "</br>رقم الطلب : " & DT.Rows(x)(0).ToString() & "</br>الإنشاء : " & DT.Rows(x)(1).ToString().Substring(0, 10) & "</br>أخر تعديل : " & DT.Rows(x)(2).ToString().Substring(0, 10), _Dwf_Documents.EngName & "</br>Request No : " & DT.Rows(x)(0).ToString() & "</br>Init Date : " & DT.Rows(x)(1).ToString().Substring(0, 10) & "</br>Last Change Date : " & DT.Rows(x)(2).ToString().Substring(0, 10))
            DTMNode.ImageUrl = "../../../Common/Images/MsgL.png"
            Dim _Dwf_DocumentWorkFow As New ClsDwf_DocumentWorkFow(Me.Page)
            If _Dwf_DocumentWorkFow.Find("TrnsEntryCode = '" & DT.Rows(x)(0).ToString() & "' and InitPeople = '" & ProfileCls.RetRefPeople() & "' and Status <> 0 Order By ID DESC") Then
                Dim DT03 As DataTable = _Dwf_DocumentWorkFow.DataSet.Tables(0)
                For i As Integer = 0 To DT03.Rows.Count - 1
                    _Dwf_DocumentWorkFow.Find("ID = " & DT03.Rows(i)("ID"))
                    DTCNode = New Infragistics.Web.UI.NavigationControls.DataTreeNode
                    Dim ActionString = IIf(ProfileCls.CurrentLanguage() = "Ar", "معلقة", "Pending")
                    If _Dwf_DocumentWorkFow.ActionID <> 0 And _Dwf_DocumentWorkFow.Status = 1 Then
                        Dim _Dwf_ActionType As New ClsDwf_ActionTypes(Me.Page)
                        _Dwf_ActionType.Find("ID = " & _Dwf_DocumentWorkFow.ActionID)
                        ActionString = IIf(ProfileCls.CurrentLanguage() = "Ar", _Dwf_ActionType.ArbName, _Dwf_ActionType.EngName)
                    ElseIf _Dwf_DocumentWorkFow.ActionID <> 0 And _Dwf_DocumentWorkFow.Status = 2 Then
                        ActionString = IIf(ProfileCls.CurrentLanguage() = "Ar", "تجاوز", "Passed")
                    End If
                    Dim StrCommand As String = "select isnull((isnull(ArbName,'') + ' ' + isnull(FamilyArbName,'') + ' ' + isnull(FatherArbName,'') + ' ' + isnull(GrandArbName,'')) , (isnull(EngName,'') + ' ' + isnull(FamilyEngName,'') + ' ' + isnull(FatherEngName,'') + ' ' + isnull(GrandEngName,''))) AS ArbName ," & _
                                               "isnull((isnull(EngName,'') + ' ' + isnull(FamilyEngName,'') + ' ' + isnull(FatherEngName,'') + ' ' + isnull(GrandEngName,'')),(isnull(ArbName,'') + ' ' + isnull(FamilyArbName,'') + ' ' + isnull(FatherArbName,'') + ' ' + isnull(GrandArbName,''))) AS EngName " & _
                                               "from hrs_Employees where ID = '" & _Dwf_DocumentWorkFow.PeopleID & "'"
                    Dim EmployeeDataset As New Data.DataSet
                    EmployeeDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Clshrs_Positions.ConnectionString, System.Data.CommandType.Text, StrCommand)
                    Dim ArPeopString As String = ""
                    Dim EnPeopString As String = ""
                    If EmployeeDataset.Tables(0).Rows.Count > 0 Then
                        ArPeopString = EmployeeDataset.Tables(0).Rows(0)(0).ToString()
                        EnPeopString = EmployeeDataset.Tables(0).Rows(0)(1).ToString()
                    End If

                    Dim ArExtraTextString As String = "من : " & ArPeopString & " حالة الطلب : " & ActionString
                    Dim EnExtraTextString As String = "From : " & EnPeopString & "Request Status : " & ActionString
                    DTCNode.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", ArExtraTextString, EnExtraTextString)
                    DTCNode.ImageUrl = "../../../Common/Images/Msgs.png"
                    Dim _sys_Forms As New ClsSys_Forms(Me.Page)
                    _sys_Forms.Find("Code = 'Cus_" & _Dwf_DocumentWorkFow.DocumentID & "'")
                    Dim DT01 As DataTable = _sys_Forms.DataSet.Tables(0)
                    If DT.Rows.Count > 0 Then
                        Dim _Dwf_DocumentStage As New ClsDwf_DocumentStages(Me.Page)
                        _Dwf_DocumentStage.Find("ID = " & _Dwf_DocumentWorkFow.StageID)
                        _sys_Forms.Find("ID = " & DT01.Rows(0)("ID"))
                        Dim LinkString As String = "../../../" & _sys_Forms.LinkUrl & "&Stage=" & _Dwf_DocumentStage.Code & "&TrnsID=" & _Dwf_DocumentWorkFow.TrnsEntryCode & "&WF=" & _Dwf_DocumentWorkFow.ID & "&St=0"
                        DTCNode.Value = LinkString
                    End If
                    DTMNode.Nodes.Add(DTCNode)
                Next
                WebDataTree_DocumentsTracing.Nodes.Add(DTMNode)
            End If
        Next
        WebDataTree_DocumentsTracing.DataBind()
    End Sub
End Class
