Imports System.Data
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class _EDAlerts
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadExpiryDocuments()
    End Sub
    Protected Sub Timer4_Tick(sender As Object, e As System.EventArgs) Handles Timer4.Tick
        LoadExpiryDocuments()
    End Sub
    Protected Sub LoadExpiryDocuments()
        Label1.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "أخر تحديث : ", "Last Update : ") & DateTime.Now.ToString()
        WebDataTree2.Nodes.Clear()
        Dim DTMNode As New Infragistics.Web.UI.NavigationControls.DataTreeNode
        Dim DTCNode As New Infragistics.Web.UI.NavigationControls.DataTreeNode

        Dim ObjClsDocumentDetails As New Clssys_DocumentsInformations(Me.Page)
        Dim DT As New DataTable

        Dim clsForms As New ClsSys_Forms(Page)
        If clsForms.Find("Code='frmExpiredDocuments'") Then
            Dim clsFormsPermissions As New ClsSys_FormsPermissions(Page)
            clsFormsPermissions.Find1("FormID=" & clsForms.ID & " And UserID=" & clsForms.DataBaseUserRelatedID)
            If clsFormsPermissions.AllowView Then
                Dim doctype As New Clssys_DocumentsTypes(Me)
                doctype.Find("RegComputerID > 0")
                Dim DTdoctype As New DataTable
                DTdoctype = doctype.DataSet.Tables(0)
                For doc As Integer = 0 To DTdoctype.Rows.Count - 1
                    DT = ObjClsDocumentDetails.GetExpireDocuments("", "", "", DateTime.Now.AddYears(-50), DateTime.Now.AddDays(DTdoctype.Rows(doc)("RegComputerID")), 0, DTdoctype.Rows(doc)("Code")).Tables(0)
                    For i As Integer = 0 To DT.Rows.Count - 1
                        DTMNode = New Infragistics.Web.UI.NavigationControls.DataTreeNode
                        Dim Cont As String = ""
                        If ProfileCls.CurrentLanguage() = "Ar" Then
                            Cont = "وثيقة رقم " & DT.Rows(i)("DocumentNumber") & " من نوع " & DT.Rows(i)("DocumentType") & "<br/>" & " تخص الموظف " & DT.Rows(i)("EmpCode") & " " & DT.Rows(i)("EmployeeName") & IIf(DT.Rows(i)("DependantName") = "", "", " تخص المرافق " & DT.Rows(i)("DependantName")) & " تنتهى فى " & DT.Rows(i)("ExpiryDate")
                        Else
                            Cont = "Document No " & DT.Rows(i)("DocumentNumber") & " Of Type " & DT.Rows(i)("DocumentType") & "<br/>" & " Belong To Employee " & DT.Rows(i)("EmpCode") & " " & DT.Rows(i)("EmployeeName") & IIf(DT.Rows(i)("DependantName") = "", "", " Belong To Dependant " & DT.Rows(i)("DependantName")) & " Will Expired At " & DT.Rows(i)("ExpiryDate")
                        End If
                        DTMNode.Text = Cont
                        DTMNode.ImageUrl = "../../../Common/Images/calendar.png"
                        DTMNode.Value = "../../../pages/hr/frmEmployeesDocuments.aspx?TB=" & IIf(DT.Rows(i)("DependantID").ToString() = "0", "hrs_Employees", "hrs_EmployeesDependants") & "&SV=" & IIf(DT.Rows(i)("DependantID").ToString() = "0", DT.Rows(i)("EmpID").ToString(), DT.Rows(i)("DependantID").ToString()) & "&DN=" & DT.Rows(i)("DocumentNumber").ToString()
                        WebDataTree2.Nodes.Add(DTMNode)
                    Next
                Next
            End If
        End If

        Dim hrs_employees As New Clshrs_Employees(Page)
        hrs_employees.Find("ID = " & ProfileCls.RetRefPeople())
        If hrs_employees.Code <> "" Then
            Dim doctype As New Clssys_DocumentsTypes(Me)
            doctype.Find("RegComputerID > 0")
            Dim DTdoctype As New DataTable
            DTdoctype = doctype.DataSet.Tables(0)
            For doc As Integer = 0 To DTdoctype.Rows.Count - 1
                DT = ObjClsDocumentDetails.GetExpireDocuments(hrs_employees.Code, "", "", DateTime.Now.AddYears(-50), DateTime.Now.AddDays(DTdoctype.Rows(doc)("RegComputerID")), 0, DTdoctype.Rows(doc)("Code")).Tables(0)
                If DT.Rows.Count > 0 Then
                    DTMNode = New Infragistics.Web.UI.NavigationControls.DataTreeNode
                    DTMNode.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "وثائقى المنتهية", "My Expiry Documents")
                    DTMNode.ImageUrl = "../../../Common/Images/SubMdl.png"
                    For i As Integer = 0 To DT.Rows.Count - 1
                        DTCNode = New Infragistics.Web.UI.NavigationControls.DataTreeNode
                        Dim Cont As String = ""
                        If ProfileCls.CurrentLanguage() = "Ar" Then
                            Cont = "نوع الوثيقة " & DT.Rows(i)("DocumentType") & " تنتهى فى " & DT.Rows(i)("ExpiryDate")
                        Else
                            Cont = "Document Type " & DT.Rows(i)("DocumentType") & " Will Expired At " & DT.Rows(i)("ExpiryDate")
                        End If
                        DTCNode.Text = Cont
                        DTCNode.ImageUrl = "../../../Common/Images/calendar.png"
                        DTMNode.Nodes.Add(DTCNode)
                    Next
                    WebDataTree2.Nodes.Add(DTMNode)
                End If
            Next
        End If
        WebDataTree2.DataBind()
    End Sub
End Class
