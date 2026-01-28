Imports System.IO
Public Class Clssys_ObjectsAttachments
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " sys_ObjectsAttachments "
        mInsertParameter = " ObjectID,RecordID,EngName,ArbName,ArbName4S,FolderName,FileName,ExpiryDate,Remarks,RegUserID,RegComputerID,IsProfilePicture ,IsDGSignature, IsImageView,MyImage "
        mInsertParameterValues = " @ObjectID,@RecordID,@EngName,@ArbName,@ArbName4S,@FolderName,@FileName,@ExpiryDate,@Remarks,@RegUserID,@RegComputerID , @IsProfilePicture ,@IsDGSignature ,@IsImageView,@MyImage "
        mUpdateParameter = " ObjectID=@ObjectID,RecordID=@RecordID,EngName=@EngName,ArbName=@ArbName,ArbName4S=@ArbName4S,FolderName=@FolderName,FileName=@FileName,ExpiryDate=@ExpiryDate,Remarks=@Remarks , IsProfilePicture=@IsProfilePicture ,IsDGSignature=@IsDGSignature , IsImageView=@IsImageView,MyImage=@MyImage "
        mSelectCommand = " set dateformat DMY Select sys_Documents.EngName , * From  " & mTable & " INNER join sys_DocumentsDetails on dbo.sys_DocumentsDetails.ID = dbo.sys_ObjectsAttachments.RecordID INNER JOIN sys_Documents ON dbo.sys_DocumentsDetails.DocumentID = dbo.sys_Documents.ID  "
        mInsertCommand = " set dateformat DMY insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " set dateformat DMY Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " set dateformat DMY Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

#End Region

#Region "Private Members"

    Private mID As Integer
    Private mObjectID As Integer
    Private mRecordID As Object
    Private mEngName As String
    Private mArbName As String
    Private mArbName4S As String
    Private mFolderName As String
    Private mFileName As String
    Private mExpiryDate As Date
    Private mRemarks As String
    Private mRegUserID As Integer
    Private mRegComputerID As Integer
    Private mRegDate As Date
    Private mCancelDate As Date
    Private mMyImage As Byte()
    Private mIsImageView As Boolean
    Private mIsProfilePicture As Boolean
    Private mIsDGSignature As Boolean

#End Region

#Region "Public property"

    Public Property ID() As String
        Get
            Return mID
        End Get
        Set(ByVal Value As String)
            mID = Value
        End Set
    End Property

    Public Property ObjectID() As Integer
        Get
            Return mObjectID
        End Get
        Set(ByVal Value As Integer)
            mObjectID = Value
        End Set
    End Property

    Public Property RecordID() As Object
        Get
            Return mRecordID
        End Get
        Set(ByVal Value As Object)
            mRecordID = Value
        End Set
    End Property

    Public Property EngName() As String
        Get
            Return mEngName
        End Get
        Set(ByVal Value As String)
            mEngName = Value
        End Set
    End Property

    Public Property ArbName() As String
        Get
            Return mArbName
        End Get
        Set(ByVal Value As String)
            mArbName = Value
        End Set
    End Property

    Public Property ArbName4S() As String
        Get
            Return mArbName4S
        End Get
        Set(ByVal Value As String)
            mArbName4S = Value
        End Set
    End Property

    Public Property FolderName() As String
        Get
            Return mFolderName
        End Get
        Set(ByVal Value As String)
            mFolderName = Value
        End Set
    End Property

    Public Property FileName() As String
        Get
            Return mFileName
        End Get
        Set(ByVal Value As String)
            mFileName = Value
        End Set
    End Property

    Public Property ExpiryDate() As Date
        Get
            Return mExpiryDate
        End Get
        Set(ByVal Value As Date)
            mExpiryDate = Value
        End Set
    End Property

    Public Property Remarks() As String
        Get
            Return mRemarks
        End Get
        Set(ByVal Value As String)
            mRemarks = Value
        End Set
    End Property

    Public Property RegUserID() As Integer
        Get
            Return mRegUserID
        End Get
        Set(ByVal Value As Integer)
            mRegUserID = Value
        End Set
    End Property

    Public Property RegComputerID() As Integer
        Get
            Return mRegComputerID
        End Get
        Set(ByVal Value As Integer)
            mRegComputerID = Value
        End Set
    End Property

    Public Property RegDate() As Date
        Get
            Return mRegDate
        End Get
        Set(ByVal Value As Date)
            mRegDate = Value
        End Set
    End Property

    Public Property CancelDate() As Date
        Get
            Return mCancelDate
        End Get
        Set(ByVal Value As Date)
            mCancelDate = Value
        End Set
    End Property

    Public Property IsImageView() As Boolean
        Get
            Return mIsImageView
        End Get
        Set(ByVal Value As Boolean)
            mIsImageView = Value
        End Set
    End Property

    Public Property IsProfilePicture() As Boolean
        Get
            Return mIsProfilePicture
        End Get
        Set(ByVal Value As Boolean)
            mIsProfilePicture = Value
        End Set
    End Property
    Public Property IsDGSignature() As Boolean
        Get
            Return mIsDGSignature
        End Get
        Set(ByVal Value As Boolean)
            mIsDGSignature = Value
        End Set
    End Property

    Public Property MyImage() As Byte()
        Get
            Return mMyImage
        End Get
        Set(ByVal value As Byte())
            mMyImage = value
        End Set
    End Property

#End Region

#Region "Public Function"
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where  IsNull(dbo.hrs_GetRecordViewStatus(sys_ObjectsAttachments.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
            Else
                Clear()
            End If
            If mID > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String = String.Empty
        Dim Value As Integer
        Try
            strSQL = "set dateformat DMY Select ID From sys_ObjectsAttachments Where " & Filter
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = strSQL
            mSqlCommand.Connection.Open()
            Value = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            If Value > 0 Then
                Update(Filter)
            Else
                Save()
            End If
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(strSQL, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function Save() As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String = String.Empty
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String = String.Empty
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function Clear() As Boolean
        Try
            mID = 0
            mObjectID = 0
            mRecordID = 0
            mEngName = String.Empty
            mArbName = String.Empty
            mArbName4S = String.Empty
            mFolderName = String.Empty
            mFileName = String.Empty
            mExpiryDate = Nothing
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mIsImageView = False
            mIsProfilePicture = False
            mIsDGSignature = False
            mMyImage = Nothing

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function FirstRecord(ByVal strFilter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = " set dateformat DMY SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  "
            StrSelectCommand &= strFilter & " ORDER BY IsProfilePicture DESC "
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function FirstRecordSignature(ByVal strFilter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = " set dateformat DMY SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  "
            StrSelectCommand &= strFilter & " ORDER BY IsDGSignature DESC "
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function LastRecord(ByVal strFilter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = " set dateformat DMY SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  "
            StrSelectCommand &= strFilter & " ORDER BY ID DESC "
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function NextRecord(ByVal strFilter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = " set dateformat DMY SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  "
            StrSelectCommand &= strFilter & " ORDER BY ID ASC "
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function previousRecord(ByVal strFilter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = " set dateformat DMY SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  "
            StrSelectCommand &= strFilter & " ORDER BY ID DESC "
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function


    'Modification :  [AGL] 5-12-2007 Add SetLanguage Function to Switch Between EngName , ArbName Fields 
    '             :                  According to Page Language 
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            '-------------------------------0257 MODIFIED-----------------------------------------
            'StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By EngName ", "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & " Order By EngName ")
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By FieldName ", "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & " Order By FieldName ")
            '------------------------------=============-----------------------------------------
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                'Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")

                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            'For Each ObjDataRow In ObjDataset.Tables(0).Rows
            '    Item = New Global.System.Web.UI.WebControls.ListItem
            '    'Item.Text = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
            '    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
            '    If (Item.Text.Trim = "") Then
            '        Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
            '    End If
            '    Item.Value = ObjDataRow("ID")
            '    DdlValues.Items.Add(Item)
            'Next
            'B# 0001 ==================================[Start]

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = mDataHandler.DataValue(ObjDataRow("FieldName"), SqlDbType.VarChar)
                Item.Value = ObjDataRow("ID")
                DdlValues.Items.Add(Item)
            Next
            'B# 0001 ==================================[End]
            If DdlValues.Items.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function
    Public Function GetList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            If Filter.Trim <> "" Then
                StrCommandString = " set dateformat DMY Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter
            Else
                StrCommandString = " set dateformat DMY Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1"
            End If

            'StrCommandString = Me.mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And FormID=" & Me.mObjectID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And FormID=" & Me.MainCompanyID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                Item.DisplayText = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")
                Item.DataValue = 0

                DdlValues.ValueListItems.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = mDataHandler.DataValue(ObjDataRow("FieldName"), SqlDbType.VarChar)
                Item.DataValue = ObjDataRow("ID")
                DdlValues.ValueListItems.Add(Item)
            Next
            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function


    Public Function CLearDefaultPhoto(ByVal ObjectID As Integer, ByVal RecordID As Integer)
        Dim StrUpdateCommand As String = String.Empty
        Try
            StrUpdateCommand = " set dateformat DMY Update " & mTable & " Set IsProfilePicture= 'False' Where ObjectID = " & ObjectID & " And  RecordID = " & RecordID
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function
    Public Function CLearDefaultPhotoSignature(ByVal ObjectID As Integer, ByVal RecordID As Integer)
        Dim StrUpdateCommand As String = String.Empty
        Try
            StrUpdateCommand = " set dateformat DMY Update " & mTable & " Set IsDGSignature= 'False' Where ObjectID = " & ObjectID & " And  RecordID = " & RecordID
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function

#End Region

#Region "Class Private Function"
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)

                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mObjectID = mDataHandler.DataValue_Out(.Item("ObjectID"), SqlDbType.Int, True)
                mRecordID = mDataHandler.DataValue_Out(.Item("RecordID"), SqlDbType.BigInt, True)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = mDataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mFolderName = mDataHandler.DataValue_Out(.Item("FolderName"), SqlDbType.VarChar)
                mFileName = mDataHandler.DataValue_Out(.Item("FileName"), SqlDbType.VarChar)
                mExpiryDate = mDataHandler.DataValue_Out(.Item("ExpiryDate"), SqlDbType.DateTime)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mIsProfilePicture = mDataHandler.DataValue_Out(.Item("IsProfilePicture"), SqlDbType.Bit)
                mIsDGSignature = mDataHandler.DataValue_Out(.Item("IsDGSignature"), SqlDbType.Bit)
                mIsImageView = mDataHandler.DataValue_Out(.Item("IsImageView"), SqlDbType.Bit)
                'mMyImage = mDataHandler.DataValue_Out(.Item("MyImage"), SqlDbType.Image)
                mMyImage = IIf(.Item("MyImage") Is DBNull.Value, Nothing, .Item("MyImage"))

            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ObjectID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mObjectID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RecordID", SqlDbType.BigInt)).Value = mDataHandler.DataValue_In(mRecordID, SqlDbType.BigInt, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FolderName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFolderName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FileName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFileName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExpiryDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mExpiryDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegUserID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mRegDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CancelDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mCancelDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsProfilePicture", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsProfilePicture, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsDGSignature", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsDGSignature, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsImageView", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsImageView, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MyImage", SqlDbType.Image)).Value = IIf(mMyImage Is Nothing, DBNull.Value, mMyImage)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MyImage", SqlDbType.Image)).Value = mDataHandler.DataValue_In(mMyImage, SqlDbType.Image)

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region

#Region "Class Destructors"
    Public Sub finalized()
        mDataSet.Dispose()
    End Sub
#End Region
End Class
