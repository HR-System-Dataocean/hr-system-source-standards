'======================================================================
'Project name  : Venus V. 
'Program name  : ClsFileUpload.vb
'Date Created  : 29-06-2021
'Issue #       :       
'Developer     : Hassan Kurdi
'Description   : For document attachment popup
'              : 
'              : 
'              : 
'Modifacations :
'======================================================================
Public Class ClsFileUpload
    Inherits ClsDataAcessLayer

#Region "Class Constructor"
    '========================================================================
    'ProcedureName  :  new()
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Initialize Class Sql Related Members 
    'Developer      :   
    'Date Created   :  29-06-2021
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
    End Sub
#End Region
#Region "Private Members"
    Private mID As Integer
    Private mCode As String
    Private mEngName As String
    Private mArbName As String
#End Region
#Region "Public Properties"
    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal value As Integer)
            mID = value
        End Set
    End Property
    Public Property Code() As String
        Get
            Return mCode
        End Get
        Set(ByVal value As String)
            mCode = value
        End Set
    End Property
    Public Property EngName() As String
        Get
            Return mEngName
        End Get
        Set(ByVal value As String)
            mEngName = value
        End Set
    End Property
    Public Property ArbName() As String
        Get
            Return mArbName
        End Get
        Set(ByVal value As String)
            mArbName = value
        End Set
    End Property
#End Region
#Region "Public Methods"
    Public Function Find(ByVal TableName As String, ByVal RecordID As Integer) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT * FROM " & TableName & " WHERE ID = " & RecordID 
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
     Public Function Find(ByVal TableName As String, ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT * FROM " & TableName & IIf(Len(Filter) > 0, " Where " & Filter, "")
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
    Public Function Clear() As Boolean
        Try
            mID = 0
            mCode = String.Empty
            mEngName = String.Empty
            mArbName = String.Empty

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region
#Region "Private Methods"
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                If(Ds.Tables(0).Columns.Contains("ID"))Then
                    mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                End If
                If(Ds.Tables(0).Columns.Contains("Code"))Then
                    mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                End If
                If(Ds.Tables(0).Columns.Contains("EngName"))Then
                    mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                End If
                If(Ds.Tables(0).Columns.Contains("ArbName"))Then
                    mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                End If
                
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region

End Class
