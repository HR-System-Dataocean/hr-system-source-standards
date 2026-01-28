
'=========================================================================
'Created by    : [0256] 
'Date Created  : 07/01/2008
'Class         : rwp_ReportsCriterias
'Table         :
'=========================================================================

Public Class ClsRpw_ReportsCriterias
    Inherits ClsDataAcessLayer

#Region "Class Constructor"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " rpw_ReportsCriteriasProperties  "
        mInsertParameter = " ReportId , SearchID , EngDescription,ArbDescription,FieldName,DataType,DefaultValue,Status,Operation,Length,FieldLanguage,Rank,MaximumValue,MinimumValue,SQLname ,FieldEngDefaultSet, FieldArbDefaultSet , FieldDefaultSetValues,DefinedTablesId,DefinedColumnsId,DefinedValueId "
        mInsertParameterValues = " @ReportID , @SearchID , @EngDescription,@ArbDescription,@FieldName,@DataType,@DefaultValue,@Status,@Operation,@Length,@FieldLanguage,@Rank,@MaximumValue,@MinimumValue ,@SQLname ,@FieldEngDefaultSet , @FieldArbDefaultSet , @FieldDefaultSetValues ,@DefinedTablesId,@DefinedColumnsId,@DefinedValueId "
        mUpdateParameter = " ReportID = @ReportID , SearchID=@SearchID , EngDescription=@EngDescription, ArbDescription=@ArbDescription, FieldName=@FieldName, DataType=@DataType, DefaultValue=@DefaultValue, Status=@Status, Operation=@Operation, Length=@Length, [FieldLanguage]=@FieldLanguage, IsCriteria=@IsCriteria  ,Rank =@Rank , MaximumValue =@MaximumValue ,MinimumValue = @MinimumValue ,SQLname =@SQLname , FieldEngDefaultSet=@FieldEngDefaultSet , FieldArbDefaultSet =@FieldArbDefaultSet  , FieldDefaultSetValues =@FieldDefaultSetValues ,DefinedTablesId=@DefinedTablesId,DefinedColumnsId=@DefinedColumnsId,DefinedValueId=@DefinedValueId "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Delete " & mTable
    End Sub
#End Region

#Region "Private Members"

    Private mID As Integer
    Private mReportID As Integer
    Private mRank As Integer
    Private mEngDescription As String
    Private mArbDescription As String
    Private mFieldName As String
    Private mDataType As String
    Private mDefaultValue As String
    Private mStatus As Boolean
    Private mOperation As String
    Private mLength As Integer
    Private mFieldLangauge As Integer
    Private mMaximumValue As String
    Private mMinimumValue As String
    Private mSearchID As Integer
    Private mSQLname As String
    Private mFieldEngDefaultSet As String
    Private mFieldArbDefaultSet As String
    Private mFieldDefaultSetValues As String

    Private mDefinedTablesId As Integer
    Private mDefinedColumnsId As Integer
    Private mDefinedValueId As Integer

#End Region

#Region "Public Enum"

    Public Enum DatesConstants
        Begin_Of_Month
        Begin_Of_Year
        Begin_Of_Period
        End_Of_Month
        End_Of_Year
        End_Of_Period
        Begin_Of_Week
        End_Of_Week
    End Enum

    'Public Enum NumericConstants
    '    Begin_Of_Month
    '    Begin_Of_Year
    '    Begin_Of_Period
    '    End_Of_Month
    '    End_Of_Year
    '    End_Of_Period
    '    Begin_Of_Week
    '    End_Of_Week
    'End Enum


#End Region

#Region "Public Property"

    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal Value As Integer)
            mID = Value
        End Set
    End Property
    Public Property ReportID() As Integer
        Get
            Return mReportID
        End Get
        Set(ByVal value As Integer)
            mReportID = value
        End Set
    End Property
    Public Property Rank() As Integer
        Get
            Return mRank
        End Get
        Set(ByVal value As Integer)
            mRank = value
        End Set
    End Property

    Public Property EngDescription() As String
        Get
            Return mEngDescription
        End Get
        Set(ByVal Value As String)
            mEngDescription = Value
        End Set
    End Property

    Public Property ArbDescription() As String
        Get
            Return mArbDescription
        End Get
        Set(ByVal Value As String)
            mArbDescription = Value
        End Set
    End Property

    Public Property FieldName() As String
        Get
            Return mFieldName
        End Get
        Set(ByVal Value As String)
            mFieldName = Value
        End Set
    End Property

    Public Property DataType() As String
        Get
            Return mDataType
        End Get
        Set(ByVal Value As String)
            mDataType = Value
        End Set
    End Property

    Public Property DefaultValue() As Object
        Get
            Return mDefaultValue
        End Get
        Set(ByVal Value As Object)
            mDefaultValue = Value
        End Set
    End Property

    Public Property Status() As Boolean
        Get
            Return mStatus
        End Get
        Set(ByVal Value As Boolean)
            mStatus = Value
        End Set
    End Property

    Public Property Operation() As String
        Get
            Return mOperation
        End Get
        Set(ByVal Value As String)
            mOperation = Value
        End Set
    End Property

    Public Property Length() As Integer
        Get
            Return mLength
        End Get
        Set(ByVal Value As Integer)
            mLength = Value
        End Set
    End Property

    Public Property FieldLangauge() As Integer
        Get
            Return mFieldLangauge
        End Get
        Set(ByVal Value As Integer)
            mFieldLangauge = Value
        End Set
    End Property

    Public Property MaximumValue() As String
        Get
            Return mMaximumValue
        End Get
        Set(ByVal value As String)
            mMaximumValue = value
        End Set
    End Property

    Public Property MinimumValue() As String
        Get
            Return mMinimumValue
        End Get
        Set(ByVal value As String)
            mMinimumValue = value
        End Set
    End Property

    Public Property SearchID() As Integer
        Get
            Return mSearchID
        End Get
        Set(ByVal value As Integer)
            mSearchID = value
        End Set
    End Property

    Public Property SQLname() As String
        Get
            Return mSQLname
        End Get
        Set(ByVal value As String)
            mSQLname = value
        End Set
    End Property
    Public Property FieldEngDefaultSet() As String
        Get
            Return mFieldEngDefaultSet
        End Get
        Set(ByVal value As String)
            mFieldEngDefaultSet = value
        End Set
    End Property
    Public Property FieldArbDefaultSet() As String
        Get
            Return mFieldArbDefaultSet
        End Get
        Set(ByVal value As String)
            mFieldArbDefaultSet = value
        End Set
    End Property
    Public Property FieldDefaultSetValues() As String
        Get
            Return mFieldDefaultSetValues
        End Get
        Set(ByVal value As String)
            mFieldDefaultSetValues = value
        End Set
    End Property


    Public Property DefinedTablesId() As Integer
        Get
            Return mDefinedTablesId
        End Get
        Set(ByVal value As Integer)
            mDefinedTablesId = value
        End Set
    End Property
    Public Property DefinedColumnsId() As Integer
        Get
            Return mDefinedColumnsId
        End Get
        Set(ByVal value As Integer)
            mDefinedColumnsId = value
        End Set
    End Property
    Public Property DefinedValueId() As Integer
        Get
            Return mDefinedValueId
        End Get
        Set(ByVal value As Integer)
            mDefinedValueId = value
        End Set
    End Property


#End Region

#Region "Public Functions"

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String = String.Empty
        Dim Value As Integer
        Try
            strSQL = " Select ID From " & Me.mTable & " Where " & Filter
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
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand)
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
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
            mEngDescription = String.Empty
            mArbDescription = String.Empty
            mFieldName = String.Empty
            mDataType = String.Empty
            mDefaultValue = Nothing
            mStatus = False
            mOperation = 0
            mLength = 0
            mFieldLangauge = 3
            mSearchID = 0
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function CheckDiff(ByVal ClassObj As Object, ByVal DSData As DataSet, ByVal Filter As String) As Boolean

        Dim myPropertyInfo As Reflection.PropertyInfo() = CType(ClassObj.GetType, Type).GetProperties()
        Dim PropertyCounter As Integer
        Dim DSCounter As Integer
        Dim Index() As Object

        For PropertyCounter = 1 To myPropertyInfo.Length - 1
            For DSCounter = 1 To DSData.Tables(0).Columns.Count - 1

                With DSData.Tables(0).Columns(DSCounter)
                    Dim myPropInfo As Reflection.PropertyInfo = CType(myPropertyInfo(PropertyCounter), Reflection.PropertyInfo)

                    'Check column name (ex: CODE=CODE)
                    If myPropInfo.Name.ToString.ToUpper = .ColumnName.ToUpper Then

                        'Check column value (ex: 002=002)
                        If Not myPropInfo.GetValue(ClassObj, Index) = FixNull(DSData.Tables(0).Rows(0)(DSCounter), DSData.Tables(0).Columns(DSCounter)) Then
                            Return True

                            Exit For
                        End If

                        Exit For
                    End If

                End With

            Next
        Next

    End Function
    Public Function FixNull(ByVal obj As Object, ByVal DataColumn As Global.System.Data.DataColumn) As Object
        Try

            Select Case DataColumn.DataType.Name.ToUpper
                Case "DECIMAL", "DOUBLE"
                    If obj Is DBNull.Value Then
                        Return 0.0
                    Else
                        Return obj
                    End If
                Case "INT32"
                    If obj Is DBNull.Value Then
                        Return 0
                    Else
                        Return obj
                    End If
                Case "DATETIME"
                    If obj Is DBNull.Value Then
                        Return Nothing
                    Else
                        Return obj
                    End If
                Case "STRING"
                    If obj Is DBNull.Value Then
                        Return ""
                    Else
                        Return obj
                    End If
                Case "BIT"
                    If obj Is DBNull.Value Then
                        Return ""
                    Else
                        Return obj
                    End If
                Case Else
                    If obj Is DBNull.Value Then
                        Return ""
                    Else
                        Return obj
                    End If
            End Select

        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function GetRepCriteriasProperties(ByVal IntReportID As Integer, ByRef dtTargetTable As Data.DataTable) As Boolean

        Dim mDataHandler As New Venus.Shared.DataHandler
        Try
            Find(" ReportID= " & IntReportID & " Order By Rank ")

            Dim dsColumnsDS As DataSet = mDataSet
            Dim drColDataRow As Data.DataRow
            With dsColumnsDS.Tables(0)
                If .Rows.Count > 0 Then
                    For IntCounter As Integer = 0 To .DataSet.Tables(0).Rows.Count - 1
                        drColDataRow = dtTargetTable.NewRow
                        drColDataRow.Item("ID") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ID"), SqlDbType.Int)
                        drColDataRow.Item("ReportID") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ReportID"), SqlDbType.Int)
                        drColDataRow.Item("Rank") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("Rank"), SqlDbType.Int)
                        drColDataRow.Item("MinimumValue") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("MinimumValue"), SqlDbType.Int)
                        drColDataRow.Item("MaximumValue") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("MaximumValue"), SqlDbType.VarChar)
                        drColDataRow.Item("FieldName") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("FieldName"), SqlDbType.VarChar)
                        drColDataRow.Item("EngDescription") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("EngDescription"), SqlDbType.VarChar)
                        drColDataRow.Item("ArbDescription") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ArbDescription"), SqlDbType.VarChar)
                        drColDataRow.Item("DataType") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("DataType"), SqlDbType.VarChar)
                        drColDataRow.Item("DefaultValue") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("DefaultValue"), SqlDbType.VarChar)
                        drColDataRow.Item("Status") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("Status"), SqlDbType.Bit)
                        drColDataRow.Item("Operation") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("Operation"), SqlDbType.VarChar)
                        drColDataRow.Item("Length") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("Length"), SqlDbType.Int)
                        drColDataRow.Item("FieldLanguage") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("FieldLanguage"), SqlDbType.Int)
                        drColDataRow.Item("ColumnBackColor") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColumnBackColor"), SqlDbType.VarChar)
                        drColDataRow.Item("ColumnFont") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColumnFont"), SqlDbType.VarChar)
                        drColDataRow.Item("ColumnFontIsBold") = .Rows(IntCounter).Item("ColumnFontIsBold")
                        drColDataRow.Item("ColumnFontIsItalic") = .Rows(IntCounter).Item("ColumnFontIsItalic")
                        drColDataRow.Item("ColumnFontIsUnderLine") = .Rows(IntCounter).Item("ColumnFontIsUnderLine")
                        drColDataRow.Item("ColumnFontSize") = IIf(.Rows(IntCounter).Item("ColumnFontSize") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColumnFontSize"))
                        drColDataRow.Item("SQLname") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("SQLname"), SqlDbType.VarChar)
                        drColDataRow.Item("FieldEngDefaultSet") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("FieldEngDefaultSet"), SqlDbType.VarChar)
                        drColDataRow.Item("FieldArbDefaultSet") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("FieldArbDefaultSet"), SqlDbType.VarChar)
                        drColDataRow.Item("FieldDefaultSetValues") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("FieldDefaultSetValues"), SqlDbType.VarChar)

                        drColDataRow.Item("DefinedTablesId") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("DefinedTablesId"), SqlDbType.Int)
                        drColDataRow.Item("DefinedColumnsId") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("DefinedColumnsId"), SqlDbType.Int)
                        drColDataRow.Item("DefinedValueId") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("DefinedValueId"), SqlDbType.Int)
                        dtTargetTable.Rows.Add(drColDataRow)
                    Next
                End If
            End With
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function SetRepCriteriasProperties(ByVal IntReportID As Integer, ByVal dtCriteriasProperties As Data.DataTable) As Boolean
        Try
            Dim IntColumnIndex As Integer = 0
            If dtCriteriasProperties.Rows.Count > 0 Then
                For Each drDatRow As DataRow In dtCriteriasProperties.Rows
                    FieldName = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("FieldName"), SqlDbType.VarChar)
                    ReportID = IntReportID
                    Rank = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("Rank"), SqlDbType.Int)
                    EngDescription = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("EngDescription"), SqlDbType.VarChar)
                    ArbDescription = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("ArbDescription"), SqlDbType.VarChar)
                    DataType = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("DataType"), SqlDbType.VarChar)
                    DefaultValue = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("DefaultValue"), SqlDbType.VarChar)
                    Status = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("Status"), SqlDbType.Bit)
                    Operation = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("Operation"), SqlDbType.VarChar)
                    Length = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("Length"), SqlDbType.Int)
                    FieldLangauge = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("FieldLanguage"), SqlDbType.Int)
                    MaximumValue = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("MaximumValue"), SqlDbType.Int)
                    MinimumValue = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("MinimumValue"), SqlDbType.Int)
                    DefaultValue = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("DefaultValue"), SqlDbType.VarChar)
                    SearchID = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("SearchID"), SqlDbType.Int)
                    SQLname = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("SQLname"), SqlDbType.VarChar)

                    FieldEngDefaultSet = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("FieldEngDefaultSet"), SqlDbType.VarChar)
                    FieldArbDefaultSet = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("FieldArbDefaultSet"), SqlDbType.VarChar)
                    FieldDefaultSetValues = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("FieldDefaultSetValues"), SqlDbType.VarChar)

                    DefinedTablesId = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("DefinedTablesId"), SqlDbType.Int)
                    DefinedColumnsId = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("DefinedColumnsId"), SqlDbType.Int)
                    DefinedValueId = mDataHandler.DataValue_Out(dtCriteriasProperties.Rows(IntColumnIndex).Item("DefinedValueId"), SqlDbType.Int)
                    Save()
                    IntColumnIndex += 1
                Next

            End If


        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function


#End Region

#Region "Private Functions"

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                mReportID = mDataHandler.DataValue_Out(.Item("ReportID"), SqlDbType.Int)
                mRank = mDataHandler.DataValue_Out(.Item("Rank"), SqlDbType.Int)
                mEngDescription = mDataHandler.DataValue_Out(.Item("EngDescription"), SqlDbType.VarChar)
                mArbDescription = mDataHandler.DataValue_Out(.Item("ArbDescription"), SqlDbType.VarChar)
                mFieldName = mDataHandler.DataValue_Out(.Item("FieldName"), SqlDbType.VarChar)
                mDataType = mDataHandler.DataValue_Out(.Item("DataType"), SqlDbType.VarChar)
                mDefaultValue = mDataHandler.DataValue_Out(.Item("DefaultValue"), SqlDbType.Variant)
                mStatus = mDataHandler.DataValue_Out(.Item("Status"), SqlDbType.Bit)
                mOperation = mDataHandler.DataValue_Out(.Item("Operation"), SqlDbType.Int)
                mLength = mDataHandler.DataValue_Out(.Item("Length"), SqlDbType.Int)
                mFieldLangauge = mDataHandler.DataValue_Out(.Item("FieldLanguage"), SqlDbType.Int)
                mMaximumValue = mDataHandler.DataValue_Out(.Item("MaximumValue"), SqlDbType.VarChar)
                mMinimumValue = mDataHandler.DataValue_Out(.Item("MinimumValue"), SqlDbType.VarChar)
                mSearchID = mDataHandler.DataValue_Out(.Item("SearchID"), SqlDbType.Int)
                mSQLname = mDataHandler.DataValue_Out(.Item("SQLname"), SqlDbType.VarChar)
                FieldEngDefaultSet = mDataHandler.DataValue_Out(.Item("FieldEngDefaultSet"), SqlDbType.VarChar)
                FieldArbDefaultSet = mDataHandler.DataValue_Out(.Item("FieldArbDefaultSet"), SqlDbType.VarChar)
                FieldDefaultSetValues = mDataHandler.DataValue_Out(.Item("FieldDefaultSetValues"), SqlDbType.VarChar)

                mDefinedTablesId = mDataHandler.DataValue_Out(.Item("DefinedTablesId"), SqlDbType.Int)
                mDefinedColumnsId = mDataHandler.DataValue_Out(.Item("DefinedColumnsId"), SqlDbType.Int)
                mDefinedValueId = mDataHandler.DataValue_Out(.Item("DefinedValueId"), SqlDbType.Int)
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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Rank", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRank, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngDescription", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngDescription, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbDescription", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbDescription, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FieldName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFieldName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DataType", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mDataType, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DefaultValue", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mDefaultValue, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Status", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mStatus, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Operation", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mOperation, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Length", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mLength, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FieldLanguage", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFieldLangauge, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MinimumValue", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mMinimumValue, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaximumValue", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mMaximumValue, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SearchID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mSearchID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SqlName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mSQLname, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FieldEngDefaultSet", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFieldEngDefaultSet, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FieldArbDefaultSet", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFieldArbDefaultSet, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FieldDefaultSetValues", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFieldDefaultSetValues, SqlDbType.VarChar)


            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DefinedTablesId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mDefinedTablesId, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DefinedColumnsId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mDefinedColumnsId, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DefinedValueId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mDefinedValueId, SqlDbType.Int)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function ReadReportCriteria(ByRef Div1 As Web.UI.WebControls.Panel, _
                                       ByRef dtCriteriasTable As Data.DataTable, _
                                       Optional ByVal STRCriteriaData As String = "") As Boolean

        Dim mDataHandler As New Venus.Shared.DataHandler
        'Dim dtCriteriasTable As DataTable
        Dim ObjDr As DataRow = Nothing
        Dim ValueCounter As Integer
        Dim TempCounter As Integer
        Dim str As String = String.Empty
        Dim litral As New Web.UI.LiteralControl

        '==== 0256 22-01-2008 
        Dim Y_Pos As Integer
        Dim X_Pos As Integer
        Dim Width As Integer = 170
        Dim CurrentTextBox As String = String.Empty
        Dim Arr() As String
        Try
            If dtCriteriasTable.Rows.Count > 0 Then
                ''If mObjReportsCriteria.Find(" ReportID =" & ReportID) Then
                ''ReDim Arr(mObjReportsCriteria.DataSet.Tables(0).Rows.Count)
                ReDim Arr(dtCriteriasTable.Rows.Count)
                For Each ObjDr In dtCriteriasTable.Rows

                    If ValueCounter > 8 Then
                        Y_Pos = 20
                        X_Pos = 370
                    Else
                        Y_Pos = 20
                        X_Pos = 20
                        Width = 170
                    End If
                    If mDataHandler.DataValue_Out(ObjDr.Item("Status"), SqlDbType.Bit) = True Then
                        CurrentTextBox = " "
                        Select Case ObjDr.Item("datatype")
                            Case "String", "VarChar" ' VarChar 
                                Dim ASPTXT As New Web.UI.WebControls.TextBox
                                ASPTXT.ID = "WV_" & ObjDr.Item("FieldName")
                                CurrentTextBox = ObjDr.Item("FieldName")
                                ASPTXT.Style.Item("POSITION") = " absolute"
                                ASPTXT.Style.Item("LEFT") = CStr(X_Pos + 125) & "px"
                                ASPTXT.Style.Item("TOP") = CStr(IIf(ValueCounter > 8, TempCounter, ValueCounter) * 25 + Y_Pos) & "px"
                                ASPTXT.Style.Item("WIDTH") = CStr(Width) & "px"
                                ASPTXT.BackColor = Drawing.Color.White

                                ASPTXT.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                                ASPTXT.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                                ASPTXT.BorderColor = Drawing.Color.White
                                ASPTXT.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("length"), SqlDbType.Int)

                                If mDataHandler.DataValue_Out(ObjDr.Item("FieldLanguage"), SqlDbType.Int) = 2 Then
                                    Venus.Shared.Web.ClientSideActions.SetLanguage(mPage, ASPTXT, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                                End If

                                '==== [256] 22-01-2008 
                                ASPTXT.Text = mDataHandler.DataValue_Out(ObjDr.Item("DefaultValue"), SqlDbType.VarChar)
                                ASPTXT.Enabled = False
                                '==== [256] 22-01-2008 
                                Div1.Controls.Add(ASPTXT)

                            Case "Int32", "Integer", "Int16", "Decimal", "Double", "Single", "Numeric" ' Numeric Values (TinyInt-Int-SmallInt-Real-Money)
                                Dim ASPWN As New Web.UI.WebControls.TextBox

                                ASPWN.ID = "WN_" & ObjDr.Item("FieldName")
                                CurrentTextBox = "WN_" & ObjDr.Item("FieldName")
                                ASPWN.Style.Item("POSITION") = " absolute"
                                ASPWN.Style.Item("LEFT") = CStr(X_Pos + 125) & "px"
                                ASPWN.Style.Item("TOP") = CStr(IIf(ValueCounter > 8, TempCounter, ValueCounter) * 25 + Y_Pos) & "px"
                                ASPWN.Style.Item("WIDTH") = CStr(Width) & "px"
                                ASPWN.BackColor = Drawing.Color.Silver

                                ASPWN.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                                ASPWN.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                                ASPWN.BorderColor = Drawing.Color.White

                                ASPWN.BackColor = Drawing.Color.White

                                '==== [256]
                                ASPWN.Text = mDataHandler.DataValue_Out(ObjDr.Item("DefaultValue"), SqlDbType.Decimal)
                                ASPWN.Enabled = False
                                '==== [256]

                                Div1.Controls.Add(ASPWN)

                            Case "Boolean", "Bit" 'Bit -Boolean Values 
                                Dim ASPWB As New Web.UI.WebControls.DropDownList
                                Dim ASPItem1 As New Web.UI.WebControls.ListItem
                                Dim ASPItem2 As New Web.UI.WebControls.ListItem
                                ASPItem1.Text = "True"
                                ASPItem1.Value = 0
                                ASPItem2.Text = "False"
                                ASPItem2.Value = 1
                                ASPWB.Items.Add(ASPItem1)
                                ASPWB.Items.Add(ASPItem2)

                                ASPWB.ID = "WB_" & ObjDr.Item("FieldName")
                                CurrentTextBox = "WB_" & ObjDr.Item("FieldName")
                                ASPWB.Style.Item("POSITION") = " absolute"
                                ASPWB.Style.Item("LEFT") = CStr(X_Pos + 125) & "px"
                                ASPWB.Style.Item("TOP") = CStr(IIf(ValueCounter > 8, TempCounter, ValueCounter) * 25 + Y_Pos) & "px"
                                ASPWB.Style.Item("WIDTH") = CStr(Width) & "px"
                                ASPWB.BackColor = Drawing.Color.White

                                ASPWB.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                                ASPWB.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                                ASPWB.BorderColor = Drawing.Color.White


                                '==== [256]
                                ASPWB.Text = mDataHandler.DataValue_Out(ObjDr.Item("DefaultValue"), SqlDbType.Bit)
                                ASPWB.Enabled = False
                                '==== [256]
                                Div1.Controls.Add(ASPWB)

                            Case "Gdate", "Adate", "Date", "DateTime" ' DateTime  
                                Dim ASPWC As New Web.UI.WebControls.TextBox
                                ASPWC.ID = "WD_" & ObjDr.Item("FieldName")
                                CurrentTextBox = "WD_" & ObjDr.Item("FieldName")
                                ASPWC.Style.Item("POSITION") = " absolute"
                                ASPWC.Style.Item("LEFT") = CStr(X_Pos + 125) & "px"
                                ASPWC.Style.Item("TOP") = CStr(IIf(ValueCounter > 8, TempCounter, ValueCounter) * 25 + Y_Pos) & "px"
                                ASPWC.Style.Item("WIDTH") = CStr(Width) & "px"
                                ASPWC.BackColor = Drawing.Color.White

                                ASPWC.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                                ASPWC.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                                ASPWC.BorderColor = Drawing.Color.White

                                '==== [256]
                                ASPWC.Text = mDataHandler.DataValue_Out(ObjDr.Item("DefaultValue"), SqlDbType.DateTime)
                                ASPWC.Enabled = False
                                '==== [256]

                                Div1.Controls.Add(ASPWC)

                        End Select

                        'RENDER NAMEING==============================
                        Dim literalName As String = String.Empty
                        If ObjDr.Item("EngDescription") Is DBNull.Value Then
                            literalName = ObjDr.Item("FieldName")
                        Else
                            literalName = ObjDr.Item("EngDescription")
                        End If
                        'border-right: white 0px outset; border-top: white 0px outset; border-left: white 0px outset; border-bottom: white 0px outset
                        str = "<DIV style='DISPLAY: inline; font-size: small; Z-INDEX: 102; LEFT: " & CStr(X_Pos) & "px; WIDTH: 123px; POSITION: absolute; TOP: " & CStr(IIf(ValueCounter > 8, TempCounter, ValueCounter) * 25 + Y_Pos) & "px; HEIGHT: 20px' ms_positioning='FlowLayout'>" & literalName & "</DIV>"
                        litral = New Web.UI.LiteralControl(str)
                        Div1.Controls.Add(litral)

                        ValueCounter += 1
                        If ValueCounter > 9 Then
                            TempCounter += 1
                        End If

                        If ValueCounter > 16 Then
                            Exit For
                        End If
                    End If
                Next

                'If ParameterNames.Text <> "" Then
                '    ParameterNames.Text = ParameterNames.Text.Substring(1)
                '    ParameterRealName.Text = ParameterRealName.Text.Substring(1)
                'End If
                Venus.Shared.Web.ClientSideActions.SetupTabOrder(mPage, Arr, Drawing.Color.LightBlue, False)
            End If


        Catch ex As Exception

        End Try
    End Function

    Public Function CreateParametersForView(ByRef pnlCriterias As Web.UI.WebControls.Panel, ByRef dtParameters As DataTable, ByVal StrparmN() As String, ByVal StrParVal() As String) As Boolean

        Dim mDataHandler As New Venus.Shared.DataHandler
        Dim ObjRow As Data.DataRow
        Dim IntY_Pos As Integer = 5
        Dim StrX_Pos As String = "26%"
        Dim StrlblX_Pos As String = "20px"
        Dim IntCounterSectionOne As Integer = 0
        Dim IntCounterSectionTwo As Integer = 0
        Dim IntCounterSectionThree As Integer = 0
        Dim IntCounter As Integer = 0
        Dim SectionID As Integer = 1
        Dim Intx As Integer = -1

        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(mPage, "Lang", StrLanguage)
        Dim _culture As String = StrLanguage
        Dim blArCulture As Boolean
        Dim StrLangLbl As String = String.Empty
        Dim StrFontName As String = String.Empty

        If (_culture <> "en-US") Then
            blArCulture = True
            StrLangLbl = "ArbDescription"
            StrFontName = "Tahoma"
        Else
            StrLangLbl = "EngDescription"
            StrFontName = "Arail"
        End If

        Try
            For Each ObjRow In dtParameters.Rows

                Intx = Array.IndexOf(StrparmN, ObjRow.Item("Fieldname"))
                If Intx >= 0 Then
                    ObjRow.Item("DefaultValue") = StrParVal(Intx).Replace(",", "")
                Else
                    ObjRow.Item("DefaultValue") = ""
                End If

                If ObjRow.Item("Status") = True Then
                    If blArCulture Then
                        'Prepare For Arabic Layout
                        If mDataHandler.DataValue_Out(ObjRow.Item("FieldLanguage"), SqlDbType.Int) = 1 Then
                            Continue For
                        Else
                            If IntCounterSectionTwo > 5 Then SectionID = 2
                            If IntCounterSectionOne > 5 Then SectionID = 3
                            If SectionID = 2 Then
                                StrX_Pos = "36%"
                                StrlblX_Pos = "40%"
                                IntCounter = IntCounterSectionOne
                            ElseIf SectionID = 3 Then
                                StrX_Pos = "5%"
                                StrlblX_Pos = "8%"
                                IntCounter = IntCounterSectionThree
                            Else
                                StrX_Pos = "70%"
                                StrlblX_Pos = "74%"
                                IntCounter = IntCounterSectionTwo
                            End If
                        End If
                    Else
                        'Prepare For English Layout
                        If mDataHandler.DataValue_Out(ObjRow.Item("FieldLanguage"), SqlDbType.Int) <> 2 Then
                            If IntCounterSectionTwo > 10 Then SectionID = 2
                            If IntCounterSectionOne > 10 Then SectionID = 3
                            If SectionID = 2 Then
                                StrX_Pos = "53%"
                                StrlblX_Pos = "38%"
                                IntCounter = IntCounterSectionOne
                            ElseIf SectionID = 3 Then
                                StrX_Pos = "82%"
                                StrlblX_Pos = "72%"
                                IntCounter = IntCounterSectionThree
                            Else
                                StrX_Pos = "18%"
                                StrlblX_Pos = "10px"
                                IntCounter = IntCounterSectionTwo
                            End If
                        Else
                            Continue For
                        End If
                    End If

                    Dim literalName As String = String.Empty
                    If ObjRow.Item(StrLangLbl) Is DBNull.Value Then
                        literalName = ObjRow.Item("FieldName")
                    Else
                        literalName = ObjRow.Item(StrLangLbl)
                    End If

                    Dim ASPLBL As New Global.System.Web.UI.WebControls.Label
                    ASPLBL.ID = "lbl" & ObjRow.Item("FieldName")
                    ASPLBL.Style.Item("POSITION") = " absolute"
                    ASPLBL.Style.Item("LEFT") = StrlblX_Pos
                    ASPLBL.Style.Item("TOP") = CStr(IntY_Pos + (25 * IntCounter)) & "px"
                    ASPLBL.Width = Global.System.Web.UI.WebControls.Unit.Percentage(21)
                    ASPLBL.Text = literalName

                    If blArCulture Then
                        ASPLBL.Style.Item("TEXT-ALIGN") = "right"
                        ASPLBL.Style.Item("DIRECTION") = "rtl"
                    End If

                    ASPLBL.Font.Name = StrFontName
                    ASPLBL.Font.Size = Global.System.Web.UI.WebControls.FontSize.Large

                    pnlCriterias.Controls.Add(ASPLBL)

                    Dim ASPTXT As New Global.System.Web.UI.WebControls.TextBox
                    ASPTXT.ID = "txt" & mDataHandler.DataValue_Out(ObjRow.Item("FieldName"), Data.SqlDbType.VarChar)
                    ASPTXT.Style.Item("POSITION") = " absolute"
                    ASPTXT.Style.Item("LEFT") = StrX_Pos
                    ASPTXT.Style.Item("TOP") = CStr(IntY_Pos + (25 * IntCounter)) & "px"
                    ASPTXT.Text = mDataHandler.DataValue_Out(ObjRow.Item("Defaultvalue"), Data.SqlDbType.VarChar)
                    ASPTXT.Width = Global.System.Web.UI.WebControls.Unit.Pixel(100)
                    ASPTXT.Height = Global.System.Web.UI.WebControls.Unit.Pixel(18)
                    ASPTXT.CssClass = "TextBoxSearchCriteria"
                    ASPTXT.BackColor = Drawing.Color.White
                    ASPTXT.BorderColor = Drawing.Color.LightGray

                    ASPTXT.Font.Size = Global.System.Web.UI.WebControls.FontSize.Medium

                    If blArCulture Then
                        ASPLBL.Style.Item("TEXT-ALIGN") = "right"
                        ASPLBL.Style.Item("DIRECTION") = "rtl"
                    End If

                    ASPTXT.ReadOnly = True
                    pnlCriterias.Controls.Add(ASPTXT)

                    If SectionID = 2 Then
                        IntCounterSectionOne += 1
                    ElseIf SectionID = 3 Then
                        IntCounterSectionThree += 1
                    Else
                        IntCounterSectionTwo += 1
                    End If
                End If
            Next
        Catch ex As Exception

        End Try

    End Function

    '==================================================================
    'Created by  : DataOcean 
    'Date        : 26/08/2007
    'Description : Fill all the defults in the DropDownlist 
    '==================================================================

    Public Function FillDefualts(ByRef PtrDdl As Global.System.Web.UI.WebControls.DropDownList, ByVal defualts As String, ByVal StrValues As String) As Boolean
        Dim StrDefualts() As String
        Dim StrArrValues() As String
        Dim ObjListItem As Global.System.Web.UI.WebControls.ListItem
        Dim IntCounter As Integer
        Try
            StrDefualts = defualts.Split(",")
            StrArrValues = StrValues.Split(",")
            For IntCounter = 0 To StrDefualts.GetUpperBound(0)
                ObjListItem = New Global.System.Web.UI.WebControls.ListItem
                ObjListItem.Text = StrDefualts(IntCounter)
                ObjListItem.Value = StrArrValues(IntCounter)
                PtrDdl.Items.Add(ObjListItem)
            Next
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
        Return True
    End Function
#End Region

#Region "Class Destructor"

    Protected Overloads Sub finalized()
        mDataSet.Dispose()
    End Sub

#End Region

End Class
