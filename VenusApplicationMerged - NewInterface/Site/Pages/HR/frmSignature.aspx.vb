Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.IO

Partial Class frmPictures
    Inherits MainPage

#Region "Public Decaration"
    Private ClsObjectsAttachments As Clssys_ObjectsAttachments
    Private mErrorHandler As Venus.Shared.ErrorsHandler
#End Region

#Region "Protected Subs"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim ObjectID As String = Request.QueryString.Item("OId")
            Dim RecordID As Object = Request.QueryString.Item("RId")

            ClsObjectsAttachments = New Clssys_ObjectsAttachments(Me.Page)
            ClsObjectsAttachments.AddNotificationOnChange(Page)
            If Not IsPostBack Then
                ClsObjectsAttachments.AddOnChangeEventToControls("frmPictures", Page)
                GetPersonName()
                GetAllImages()
            End If
            uwgDocumentPictures.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect
            uwgDocumentPictures.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            uwgDocumentPictures.DisplayLayout.AllowAddNewDefault = Infragistics.WebUI.UltraWebGrid.AllowAddNew.No
            uwgDocumentPictures.BorderWidth = 0
            Laod_data_Grid()
            btnNext.Attributes.Add("onclick", "GetNextRow()")
            btnPrevious.Attributes.Add("onclick", "GetPreviousRow()")
            btnFirst.Attributes.Add("onclick", "GetFirstRow()")
            btnLast.Attributes.Add("onclick", "GetLastRow()")
            Ajax.Utility.RegisterTypeForAjax(GetType(frmPictures))
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
        Dim fileOK As Boolean = False
        Dim fileExtension As String
        fileExtension = System.IO.Path.GetExtension(txtAttached.FileName).ToLower()
        Dim allowedExtensions As String() = {".jpg", ".jpeg", ".png", ".gif", ".bmp"}
        For i As Integer = 0 To allowedExtensions.Length - 1
            If fileExtension = allowedExtensions(i) Then
                fileOK = True
                Exit For
            End If
        Next
        If txtAttached.FileName = Nothing Then
            fileOK = True
        End If
        If fileOK = True Then
            SavePart()
            AfterOperation()
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Please Check Your Photo Extension")
        End If
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
                .ExpiryDate = txtExpireDate.Value
                .IsImageView = True
                'If chkIsDefault.Checked Then
                '    ClsObjectsAttachments.CLearDefaultPhotoSignature(ObjectID, RecordID)
                .IsDGSignature = True
                'Else
                '  .IsDGSignature = False
                'End If
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetValues(ByRef ClsObjectsAttachments As Clssys_ObjectsAttachments) As Boolean
        Dim ObjectID As String = Request.QueryString.Item("OId")
        Dim RecordID As Object = Request.QueryString.Item("RId")
        Dim StrRealPath As String = "~\Photos\"
        Try
            With ClsObjectsAttachments
                txtID.Value = .ID
                txtExpireDate.Value = .ExpiryDate()
                ImgEmployee.ImageUrl = StrRealPath & .ObjectID & "_" & .RecordID & "\" & .FileName
                txtPhotoName.Value = .FileName
                chkIsDefault.Checked = .IsDGSignature
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
                ' ClsObjectsAttachments.Update("  ObjectID = " & ObjectID & " AND RecordID = " & RecordID & " AND FileName = '" & txtAttached.FileName & "'")
            Else
                If ClsObjectsAttachments.Save() Then
                    SaveIMage(ClsObjectsAttachments)
                End If
            End If
        Else
            If txtExpireDate.Value = Nothing Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Please Insert Photo First")
            Else
                ClsObjectsAttachments = New Clssys_ObjectsAttachments(Me.Page)
                ClsObjectsAttachments.Find(" ID = " & Val(txtID.Value))
                With ClsObjectsAttachments
                    .ObjectID = ObjectID
                    .RecordID = RecordID
                    .FileName = txtPhotoName.Value
                    .ExpiryDate = txtExpireDate.Value
                    .IsImageView = True
                    If chkIsDefault.Checked Then
                        ClsObjectsAttachments.CLearDefaultPhotoSignature(ObjectID, RecordID)
                        .IsDGSignature = True
                    Else
                        .IsDGSignature = False
                    End If
                End With
                ClsObjectsAttachments.Update(" ID = " & Val(txtID.Value))
            End If
        End If
    End Function
    Private Function SaveIMage(ByRef ClsObjectsAttachments As Clssys_ObjectsAttachments) As Boolean
        Dim ObjectID As String = Request.QueryString.Item("OId")
        Dim RecordID As Object = Request.QueryString.Item("RId")
        '=================
        Dim fileOK As Boolean = False
        Dim fileExtension As String
        fileExtension = System.IO.Path.GetExtension(txtAttached.FileName).ToLower()
        Dim allowedExtensions As String() = {".jpg", ".jpeg", ".png", ".gif", ".bmp"}
        For i As Integer = 0 To allowedExtensions.Length - 1
            If fileExtension = allowedExtensions(i) Then
                fileOK = True
                Exit For
            End If
        Next
        If txtAttached.FileName = Nothing Then
            fileOK = True
        End If
        '=================
        Try
            Dim StrRealPath As String = Request.PhysicalApplicationPath & "Photos\"
            Dim strPath As String = StrRealPath & ObjectID & "_" & RecordID
            If Not Directory.Exists(strPath) Then
                Directory.CreateDirectory(strPath)
            End If
            If fileOK Then
                txtAttached.SaveAs(strPath & "\" & ClsObjectsAttachments.FileName)
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetAllImages() As Boolean
        Dim ObjectID As String = Request.QueryString.Item("OId")
        Dim RecordID As Object = Request.QueryString.Item("RId")
        ClsObjectsAttachments = New Clssys_ObjectsAttachments(Me.Page)
        ClsObjectsAttachments.FirstRecordSignature(" AND ObjectID = " & ObjectID & " AND RecordID = " & RecordID & " AND (ExpiryDate >= '" & Date.Now.Date & "' OR ExpiryDate is null) ")
        GetValues(ClsObjectsAttachments)
    End Function
    Private Function Laod_data_Grid() As Boolean
        Dim ObjectID As String = Request.QueryString.Item("OId")
        Dim RecordID As Object = Request.QueryString.Item("RId")
        ClsObjectsAttachments = New Clssys_ObjectsAttachments(Me.Page)
        ClsObjectsAttachments.Find(" isNull(sys_ObjectsAttachments.CancelDate,'')='' AND sys_ObjectsAttachments.ObjectID = " & ObjectID & " AND sys_ObjectsAttachments.RecordID = " & RecordID & " AND (sys_ObjectsAttachments.ExpiryDate >= '" & Date.Now.Date & "' OR sys_ObjectsAttachments.ExpiryDate is null) ORDER BY sys_ObjectsAttachments.IsDGSignature DESC  ")
        uwgDocumentPictures.DataSource = ClsObjectsAttachments.DataSet.Tables(0).DefaultView
        uwgDocumentPictures.DataBind()
        Dim intID As Integer
        Try
            If uwgDocumentPictures.Rows.Count > 0 Then
                Dim rowEmpItem As Object = uwgDocumentPictures.Rows.GetItem(0)
                uwgDocumentPictures.Rows(0).Activate()
                uwgDocumentPictures.Rows(0).Selected = True
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function GetPersonName() As Boolean
        Dim ObjectID As String = Request.QueryString.Item("OId")
        Dim RecordID As Object = Request.QueryString.Item("RId")
        Dim strTableName As String = String.Empty
        Dim ClsObject As New Clssys_Objects(Me.Page)
        Dim ClsEmployees As New Clshrs_Employees(Me.Page)
        Dim ClsDependant As New Clshrs_EmployeesDependants(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsObject.ConnectionString)
        ClsObject.Find(" ID = " & ObjectID)
        If ClsObject.ID > 0 Then
            strTableName = ClsObject.EngName.Trim
        End If
        If strTableName = "hrs_Employees" Then
            ClsEmployees.Find("ID=" & RecordID)
            lblName.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsEmployees.Name & "/" & ClsEmployees.ArabicName)
        ElseIf strTableName = "hrs_EmployeesDependants" Then
            ClsDependant.Find("ID=" & RecordID)
            lblName.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsDependant.EngName & "/" & ClsDependant.ArbName)
        End If
    End Function
#End Region

End Class
