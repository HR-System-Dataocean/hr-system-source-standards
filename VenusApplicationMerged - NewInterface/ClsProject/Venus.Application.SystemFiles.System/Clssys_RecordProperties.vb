'=========================================================================
'Created by : [0259]
'Date : 23/08/2007
'Modifications   :  
'                : E#001 Change Screen Style 
'                : B#g0010 [0256] 19-06-2008 Add _Forgin arrgument to data_ValueIn method in SetParameters Function 
'                :                           And _Forgin arrgument to data_value_Out Method in GetParameters Function 
'                :                           To avoid saving non-existing forign key in any table 
'                :                           And convert the DBnull values to zero in case of forign key fields only 
'=========================================================================

Public Class Clssys_RecordProperties
    Inherits ClsDataAcessLayer

#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page, ByVal TableName As String)
        MyBase.New(Page)
        With Me
            .Table = TableName
            .mSelectCommand = " Select ID,RegUserID,RegComputerID,RegDate,CancelDate From  " & mTable
            .mUpdateCommand = " Update " & mTable

        End With
    End Sub
#End Region

#Region "Private Members"

    '=====================================================================
    'Date : 09/07/2007
    'Description: Private members declration
    '=====================================================================

    Private mID As Object
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object


#End Region

#Region "Public property"
    Public ReadOnly Property ID() As Object
        Get
            Return mID
        End Get
    End Property
    Public ReadOnly Property RegUserID() As Object
        Get
            Return mRegUserID
        End Get
    End Property
    Public ReadOnly Property RegComputerID() As Object
        Get
            Return mRegComputerID
        End Get
    End Property
    Public ReadOnly Property RegDate() As Object
        Get
            Return mRegDate
        End Get
    End Property
    Public Property CancelDate() As Object
        Get
            Return mCancelDate
        End Get
        Set(ByVal value As Object)
            mCancelDate = value
        End Set
    End Property
#End Region


#Region "Public Fuctions"
    '==================================================================
    'Created by : [0259]
    'Date : 22/08/2007
    'Description: Get the Record properties(RegUserID,RegComputerID,RegDate,CancelDate)
    '==================================================================
    Public Function GetRecordProperties(ByVal IntRecordID As Integer, ByVal StrTableName As String) As Boolean

        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = " Select ID,RegUserID,RegDate,CancelDate From  " & StrTableName & " Where ID = " & IntRecordID
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                With mDataSet.Tables(0).Rows(0)
                    mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                    mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                    'mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                    mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                    mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                End With
            Else
                Clear()
            End If
            If mID > 0 Then
                Return True
            End If
        Catch ex As Exception

        End Try

    End Function

    '==================================================================
    'Created by : [0259]
    'Date : 22/08/2007
    'Description: Set the Record CancelDate (the only property which can be changed)
    '==================================================================
    Public Function SetRecordPropertiesCancelDate(ByVal IntRecordID As Integer, ByVal NewDate As DateTime, ByVal StrTableName As String) As Boolean

        Dim UpdateCommand As String = String.Empty
        Try
            If IsNothing(NewDate) Or NewDate.Year = 1 Then
                UpdateCommand = CONFIG_DATEFORMAT & " Update " & StrTableName & " Set CancelDate=null  Where ID = " & IntRecordID
            Else
                UpdateCommand = CONFIG_DATEFORMAT & " Update " & StrTableName & " Set CancelDate='" & NewDate & "'  Where ID = " & IntRecordID
            End If

            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = UpdateCommand

            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function Clear() As Boolean
        Try
            mID = 0
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function GetRecordHistory(ByVal RecordID As Integer, ByVal ObjectID As Integer, ByVal columnname As String, ByVal tablename As String) As DataTable
        Dim StrSelectCommand As String = String.Empty
        Dim DT As New DataTable
        Try
            Dim myUser As String = ""
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            Dim StrLanguage As String = String.Empty
            WebHandler.GetCookies(mPage, "Lang", StrLanguage)


            If StrLanguage = "ar-EG" Then
                myUser = " sys_users.ArbName "
            Else
                myUser = " sys_users.EngName "
            End If
            If tablename.Trim() = "hrs_Employees" Then
                StrSelectCommand = " Select sys_Fields.FieldName As ColumnName,sys_History.OldValue  As OldData,(Select Top 1 OldValue from sys_history his Where his.RecordID =" & RecordID & " and his.Fieldid = Fieldid and his.RegDate > sys_history.RegDate) as ProviosData," & myUser & "  As UserEngName,sys_history.RegDate	As RegDate From  sys_history Inner Join sys_fields	On sys_Fields.id	= sys_history.fieldid  Inner Join sys_Objects	On sys_objects.id	= sys_Fields.Objectid Inner Join sys_users	On sys_users.id		= sys_history.RegUserID  Where IsNull(sys_History.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(sys_History.ID,'" & tablename & "',43,1),0) <> 1 And sys_history.RecordID=@RecordID And sys_Objects.id in(" & ObjectID & ",130,344) Order by sys_Fields.SysColumns_OrderID,  sys_History.RegDate  "
            Else
                StrSelectCommand = " Select sys_Fields.FieldName As ColumnName,sys_History.OldValue  As OldData,(Select Top 1 OldValue from sys_history his Where his.RecordID =" & RecordID & " and his.Fieldid = Fieldid and his.RegDate > sys_history.RegDate) as ProviosData," & myUser & " As UserEngName,sys_history.RegDate	As RegDate From  sys_history Inner Join sys_fields	On sys_Fields.id	= sys_history.fieldid  Inner Join sys_Objects	On sys_objects.id	= sys_Fields.Objectid Inner Join sys_users	On sys_users.id		= sys_history.RegUserID  Where IsNull(sys_History.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(sys_History.ID,'" & tablename & "',43,1),0) <> 1 And sys_history.RecordID=@RecordID And sys_Objects.id =" & ObjectID & " Order by sys_Fields.SysColumns_OrderID,  sys_History.RegDate  "
            End If

            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            'mSqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            mSqlDataAdapter.SelectCommand.Parameters.AddWithValue("@RecordID", RecordID)
            mSqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Objectid", ObjectID)
            mSqlDataAdapter.Fill(DT)
            Return DT
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, Me.DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")

        End Try

    End Function

#End Region


End Class
