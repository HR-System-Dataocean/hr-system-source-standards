Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.IO
Imports System.Data

Partial Class frmAttachDocuments
    Inherits MainPage

#Region "Public Decaration"
    Private ClsObjectsAttachments As Clssys_ObjectsAttachments
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Private ClsDocumentsInformations As Clssys_DocumentsInformations
    Private ClsDocumentAttachment As Clssys_DocumentsInformationAttachments
    Private ClsDocuments As Clssys_DocumentsTypes
    Private ClsEmployees As Clshrs_Employees
    Private ClsEmployeesDependants As Clshrs_EmployeesDependants
    Private ClsCompanies As Clssys_Companies

    Private clsMainOtherFields As clsSys_MainOtherFields

#End Region


#Region "Protected Subs"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            HiddenField1.Value = 0
            Dim ObjectID As String = Request.QueryString.Item("OId")
            Dim RecordID As Object = Request.QueryString.Item("RId")
            If Request.QueryString.Count > 3 Then
                lblPhotoPath.Visible = False
                txtAttached.Visible = False
                btnSave.Visible = False
                btnCancel.Visible = False
                HiddenField1.Value = 0
            ElseIf Request.QueryString.Count > 2 Then
                lblPhotoPath.Visible = False
                txtAttached.Visible = False
                btnSave.Visible = False
                btnCancel.Visible = False
                HiddenField1.Value = 1
            End If

            txtObject.Value = ObjectID
            txtRecord.Value = RecordID

            ClsObjectsAttachments = New Clssys_ObjectsAttachments(Me.Page)
            ClsObjectsAttachments.AddNotificationOnChange(Page)
            If Not IsPostBack Then
                ClsObjectsAttachments.AddOnChangeEventToControls("frmAttachDocuments", Page)
                GetAllImages()
            End If
            uwgDocumentPictures.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect
            uwgDocumentPictures.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            uwgDocumentPictures.DisplayLayout.AllowAddNewDefault = Infragistics.WebUI.UltraWebGrid.AllowAddNew.No
            uwgDocumentPictures.BorderWidth = 0
            Laod_data_Grid()
            Ajax.Utility.RegisterTypeForAjax(GetType(frmAttachDocuments))
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnCancel.Click
        Dim ObjectID As String = Request.QueryString.Item("OId")
        Dim RecordID As Object = Request.QueryString.Item("RId")
        ClsObjectsAttachments = New Clssys_ObjectsAttachments(Me.Page)
        If txtID.Value <> "" Then
            Dim str As String = "delete From sys_ObjectsAttachments where ID=" & Val(txtID.Value)
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsObjectsAttachments.ConnectionString, Data.CommandType.Text, str)
            AfterOperation()
        End If
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click
        SavePart()
        AfterOperation()
    End Sub
#End Region

#Region "Private Functions"
    Private Function AssignValue(ByRef ClsObjectsAttachments As Clssys_ObjectsAttachments) As Boolean
        Dim ObjectID As String = Request.QueryString.Item("OId")
        Dim RecordID As Object = Request.QueryString.Item("RId")
        ClsObjectsAttachments = New Clssys_ObjectsAttachments(Me.Page)
        Dim strFileName As String = txtAttached.FileName
        Try
            If strFileName <> "" Then
                Dim arrTempFileName As String() = strFileName.Split(".")
                strFileName = arrTempFileName(0) & "_" & DateTime.Now.ToString("ddMMyyyyHHmmsss") & "." & arrTempFileName(1)
            End If
            With ClsObjectsAttachments
                .ObjectID = ObjectID
                .RecordID = RecordID
                .FileName = strFileName
                .IsImageView = False
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetValues(ByRef ClsObjectsAttachments As Clssys_ObjectsAttachments) As Boolean
        Dim ObjectID As String = Request.QueryString.Item("OId")
        Dim RecordID As Object = Request.QueryString.Item("RId")
        Try
            With ClsObjectsAttachments
                txtID.Value = .ID
                txtPhotoName.Value = .FileName
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function AfterOperation() As Boolean
        ClsObjectsAttachments.Clear()
        GetValues(ClsObjectsAttachments)
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtAttached, True)
        Laod_data_Grid()
        GetAllImages()
    End Function
    Private Function SavePart() As Boolean
        Dim ObjectID As String = Request.QueryString.Item("OId")
        Dim RecordID As Object = Request.QueryString.Item("RId")
        ClsObjectsAttachments = New Clssys_ObjectsAttachments(Me.Page)
        If txtAttached.FileName <> "" Then
            ClsObjectsAttachments.Find("  ObjectID = " & ObjectID & " AND RecordID = " & RecordID & " AND FileName = '" & txtAttached.FileName & "'")
            If Not AssignValue(ClsObjectsAttachments) Then
                Exit Function
            End If
            If ClsObjectsAttachments.ID > 0 Then
            Else
                If ClsObjectsAttachments.Save() Then
                    SaveIMage(ClsObjectsAttachments)
                End If
            End If
        Else
            ClsObjectsAttachments = New Clssys_ObjectsAttachments(Me.Page)
            ClsObjectsAttachments.Find(" ID = " & Val(txtID.Value))
            With ClsObjectsAttachments
                .ObjectID = ObjectID
                .RecordID = RecordID
                .FileName = txtPhotoName.Value
            End With
            ClsObjectsAttachments.Update(" ID = " & Val(txtID.Value))
        End If
    End Function
    Private Function SaveIMage(ByRef ClsObjectsAttachments As Clssys_ObjectsAttachments) As Boolean
        Dim ObjectID As String = Request.QueryString.Item("OId")
        Dim RecordID As Object = Request.QueryString.Item("RId")
        Try
            Dim StrRealPath As String = Request.PhysicalApplicationPath & "Uploads\"
            Dim strPath As String = StrRealPath & ObjectID & "_" & RecordID
            If Not Directory.Exists(strPath) Then
                Directory.CreateDirectory(strPath)
            End If
            txtAttached.SaveAs(strPath & "\" & ClsObjectsAttachments.FileName)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetAllImages() As Boolean
        Dim ObjectID As String = Request.QueryString.Item("OId")
        Dim RecordID As Object = Request.QueryString.Item("RId")
        ClsObjectsAttachments = New Clssys_ObjectsAttachments(Me.Page)
        If Request.QueryString.Count > 3 Then
            ClsObjectsAttachments.FirstRecord(" AND ObjectID = " & ObjectID & " AND RecordID = " & RecordID & " AND (ExpiryDate >= '" & Date.Now.Date & "' OR ExpiryDate is null) ")
        ElseIf Request.QueryString.Count > 2 Then
            ClsObjectsAttachments.FirstRecord(" AND ObjectID = " & ObjectID & " AND RecordID in (select ID from sys_DocumentsDetails where RecordID = " & RecordID & " and ObjectID=281) AND (ExpiryDate >= '" & Date.Now.Date & "' OR ExpiryDate is null) ")
        Else
            ClsObjectsAttachments.FirstRecord(" AND ObjectID = " & ObjectID & " AND RecordID = " & RecordID & " AND (ExpiryDate >= '" & Date.Now.Date & "' OR ExpiryDate is null) ")
        End If
        GetValues(ClsObjectsAttachments)
    End Function
    Private Function Laod_data_Grid() As Boolean
        Dim ObjectID As String = Request.QueryString.Item("OId")
        Dim RecordID As Object = Request.QueryString.Item("RId")
        ClsObjectsAttachments = New Clssys_ObjectsAttachments(Me.Page)
        If Request.QueryString.Count > 3 Then
            ClsObjectsAttachments.Find(" isNull(sys_ObjectsAttachments.CancelDate,'')='' AND sys_ObjectsAttachments.ObjectID = " & ObjectID & " AND sys_ObjectsAttachments.RecordID = " & RecordID & " AND (ExpiryDate >= '" & Date.Now.Date & "' OR ExpiryDate is null)  ")
        ElseIf Request.QueryString.Count > 2 Then
            ClsObjectsAttachments.Find(" isNull(sys_ObjectsAttachments.CancelDate,'')='' AND sys_ObjectsAttachments.ObjectID = " & ObjectID & " AND sys_ObjectsAttachments.RecordID in (select ID from sys_DocumentsDetails where RecordID = " & RecordID & "  and ObjectID=281) AND (sys_ObjectsAttachments.ExpiryDate >= '" & Date.Now.Date & "' OR sys_ObjectsAttachments.ExpiryDate is null)  ")
        Else
            ClsObjectsAttachments.Find(" isNull(sys_ObjectsAttachments.CancelDate,'')='' AND sys_ObjectsAttachments.ObjectID = " & ObjectID & " AND sys_ObjectsAttachments.RecordID = " & RecordID & " AND (sys_ObjectsAttachments.ExpiryDate >= '" & Date.Now.Date & "' OR sys_ObjectsAttachments.ExpiryDate is null)  ")
        End If
        uwgDocumentPictures.DataSource = ClsObjectsAttachments.DataSet.Tables(0).DefaultView
        uwgDocumentPictures.DataBind()
        Try
            If uwgDocumentPictures.Rows.Count > 0 Then
                Dim rowEmpItem As Object = uwgDocumentPictures.Rows.GetItem(0)
                uwgDocumentPictures.Rows(0).Activate()
                uwgDocumentPictures.Rows(0).Selected = True
            End If
        Catch ex As Exception
        End Try
    End Function



#End Region
End Class
