Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
Public Class DocumentsWs
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)> _
    Public Function Save_Documents(ByVal _ID As Int32, ByVal _Code As String, ByVal _ArbName As String, ByVal _EngName As String, ByVal _DocTypeID As Int32, ByVal _ReportID As Int32, ByVal _MenuParent As Int32, ByVal _AllowWorkFlow As Boolean) As Boolean
        If _ReportID <> 0 Then
            Dim _Sys_Report As New Clssys_Reports(New System.Web.UI.Page)
            _Sys_Report.Find("ID = " & _ReportID)
            If _Sys_Report.ReportSource = 1 Then
                If _Sys_Report.DataSource <> "" Then
                    Dim StrCommand As String = "select PARAMETER_NAME from information_schema.parameters where PARAMETER_MODE = 'IN' and specific_name='" & _Sys_Report.DataSource & "'"
                    Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Sys_Report.ConnectionString, System.Data.CommandType.Text, StrCommand).Tables(0)
                    If DT.Rows.Count > 1 Then
                        Return -1
                    End If
                Else
                    Return -1
                End If
            End If
        End If
        Dim _Dwf_Documents As New ClsDwf_Documents(New System.Web.UI.Page)
        SetObjectValues(_Dwf_Documents, _Code, _ArbName, _EngName, _DocTypeID, _ReportID, _MenuParent, _AllowWorkFlow)
        If _ID = 0 Then
            If _Dwf_Documents.Save() Then
                Return True
            Else
                Return False
            End If
        Else
            If _Dwf_Documents.Update("ID = " & _ID) Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
    Private Sub SetObjectValues(ByRef _Dwf_Documents As ClsDwf_Documents, ByVal _Code As String, ByVal _ArbName As String, ByVal _EngName As String, ByVal _DocTypeID As Int32, ByVal _ReportID As Int32, ByVal _MenuParent As Int32, ByVal _AllowWorkFlow As Boolean)
        _Dwf_Documents.Code = _Code
        _Dwf_Documents.ArbName = _ArbName
        _Dwf_Documents.EngName = _EngName
        _Dwf_Documents.DocTypeID = IIf(_DocTypeID = 0, Nothing, _DocTypeID)
        _Dwf_Documents.ReportID = IIf(_ReportID = 0, Nothing, _ReportID)
        _Dwf_Documents.RegComputerID = IIf(_MenuParent = 0, Nothing, _MenuParent)
        _Dwf_Documents.RegDate = DateTime.Now
        _Dwf_Documents.RegUserID = ProfileCls.RetUserID()
        _Dwf_Documents.ApplyWorkFlow = _AllowWorkFlow
    End Sub
    <WebMethod(EnableSession:=True)> _
    Public Function CreateObject(ByVal DocumentID As String) As Integer
        Try
            Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
            _Dwf_Document.Find("ID = " & DocumentID)
            Dim _Dwf_DocumentElement As New ClsDwf_DocumentElements(New System.Web.UI.Page)
            _Dwf_DocumentElement.Find("DocumentID = " & _Dwf_Document.ID)
            Dim _Sys_Form As New ClsSys_Forms(New System.Web.UI.Page)
            _Sys_Form.Find("Code = 'Cus_" & _Dwf_Document.ID & "'")
            Dim DT As DataTable = _Sys_Form.DataSet.Tables(0)
            If DT.Rows.Count > 0 Then
                Dim _Sys_Object As New Clssys_Objects(New System.Web.UI.Page)
                If _Sys_Object.Find("Code = 'CF_" & _Dwf_Document.Code & "'") Then
                Else
                    _Sys_Object.Code = "CF_" & _Dwf_Document.Code
                    _Sys_Object.ArbName = "CF_" & _Dwf_Document.Code
                    _Sys_Object.EngName = "CF_" & _Dwf_Document.Code
                    _Sys_Object.RegDate = DateTime.Now
                    _Sys_Object.Save()
                End If
                Dim _Sys_Searchs As New Clssys_Searchs(New System.Web.UI.Page)
                If _Sys_Searchs.Find("Code = 'CF_" & _Dwf_Document.Code & "'") Then
                Else
                    _Sys_Searchs.Code = "CF_" & _Dwf_Document.Code
                    _Sys_Searchs.ArbName = "CF_" & _Dwf_Document.Code
                    _Sys_Searchs.EngName = "CF_" & _Dwf_Document.Code
                    _Sys_Searchs.ObjectID = _Sys_Object.ID
                    _Sys_Searchs.RegDate = DateTime.Now
                    _Sys_Searchs.Save()
                End If
                Dim stringcmd As String = "Delete from sys_FormsControls where FormID = " & _Sys_Form.ID
                stringcmd = stringcmd & Environment.NewLine & "Delete from sys_SearchsColumns where SearchID = " & _Sys_Searchs.ID
                stringcmd = stringcmd & Environment.NewLine & "Delete from sys_Fields where ObjectID = " & _Sys_Object.ID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(_Sys_Searchs.ConnectionString, System.Data.CommandType.Text, stringcmd)

                Dim _Sys_FormsControl As New Clssys_FormsControls(New System.Web.UI.Page)
                Dim _Sys_SearchsColumn As New Clssys_SearchsColumns(New System.Web.UI.Page)
                Dim _Sys_Field As New Clssys_Fields(New System.Web.UI.Page)
                Dim DTElements As DataTable = _Dwf_DocumentElement.DataSet.Tables(0)
                For i As Integer = 0 To DTElements.Rows.Count - 1
                    If DTElements.Rows(i)("ElementType") = "TextBox" Or DTElements.Rows(i)("ElementType") = "TextArea" Or DTElements.Rows(i)("ElementType") = "ComboBox" Or DTElements.Rows(i)("ElementType") = "CheckBox" Or DTElements.Rows(i)("ElementType") = "Radio" Then
                        _Sys_Field = New Clssys_Fields(New System.Web.UI.Page)
                        _Sys_Field.Find("ObjectID = " & _Sys_Object.ID & " and FieldName = '" & DTElements.Rows(i)("Code") & "'")
                        If DTElements.Rows(i)("ElementType") = "TextBox" Then
                            _Sys_Field.FieldType = 167
                            _Sys_Field.FieldLength = 2048
                        ElseIf DTElements.Rows(i)("ElementType") = "TextArea" Then
                            _Sys_Field.FieldType = 167
                            _Sys_Field.FieldLength = 2048
                        ElseIf DTElements.Rows(i)("ElementType") = "ComboBox" Then
                            _Sys_Field.FieldType = 56
                            _Sys_Field.FieldLength = 4
                        ElseIf DTElements.Rows(i)("ElementType") = "CheckBox" Then
                            _Sys_Field.FieldType = 104
                            _Sys_Field.FieldLength = 1
                        ElseIf DTElements.Rows(i)("ElementType") = "Radio" Then
                            _Sys_Field.FieldType = 104
                            _Sys_Field.FieldLength = 1
                        End If
                        _Sys_Field.ObjectID = _Sys_Object.ID
                        _Sys_Field.FieldName = DTElements.Rows(i)("Code")
                        _Sys_Field.RegDate = DateTime.Now
                        _Sys_Field.Save()

                        _Sys_FormsControl = New Clssys_FormsControls(New System.Web.UI.Page)
                        _Sys_FormsControl.FormID = _Sys_Form.ID
                        _Sys_FormsControl.Name = DTElements.Rows(i)("Code")
                        _Sys_FormsControl.EngCaption = DTElements.Rows(i)("Title")
                        _Sys_FormsControl.ArbCaption = DTElements.Rows(i)("Title")
                        _Sys_FormsControl.FieldID = _Sys_Field.ID
                        _Sys_FormsControl.RegDate = DateTime.Now
                        _Sys_FormsControl.Save()

                        _Sys_SearchsColumn = New Clssys_SearchsColumns(New System.Web.UI.Page)
                        _Sys_SearchsColumn.SearchID = _Sys_Searchs.ID
                        _Sys_SearchsColumn.FieldID = _Sys_Field.ID
                        _Sys_SearchsColumn.EngName = DTElements.Rows(i)("Title")
                        _Sys_SearchsColumn.ArbName = DTElements.Rows(i)("Title")
                        _Sys_SearchsColumn.IsCriteria = True
                        _Sys_SearchsColumn.IsView = True
                        _Sys_SearchsColumn.IsArabic = False
                        _Sys_SearchsColumn.RegDate = DateTime.Now
                        _Sys_SearchsColumn.Save()
                    End If
                Next i
            End If
        Catch ex As Exception
            Return 0
        End Try
        Return 1
    End Function
End Class