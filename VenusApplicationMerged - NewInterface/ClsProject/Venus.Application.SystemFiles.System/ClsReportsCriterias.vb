Imports Venus.Shared

'Project           : Venus V.
'Module            : Sys (Report Writer Module)
'Date Created      : 21-06-2009
'Developer         : [Mohammed Al-Janazrh]
'Description       : 1-Implement Data Acess Layer of Sys_RreportsControlsProperties table with fields code,English Caption,
'                       Arabic Caption
'                    2-Allow searching
'                    3-Get list with all codes
'                    4-Implement functions save(), update() and delete() to allow DML with some critera
'                    5-Implement functions first(),last(),next() and previous() to allow navigation between records
'                  : B#g0010 [0256] 18-06-2009 Add _Forgin arrgument to data_ValueIn method in SetParameters Function 
'                  :                           And _Forgin arrgument to data_value_Out Method in GetParameters Function 
'                  :                           To avoid saving non-existing forign key in any table 
'                  :                           And convert the DBnull values to zero in case of forign key fields only 
'=========================================================================


Public Class ClsReportsCriterias
    Inherits ClsDataAcessLayer

    Public Sub New(ByVal page As Global.System.Web.UI.Page)
        MyBase.New(page)
        mTable = " sys_ReportsCriterias"
        mInsertParameter = "ReportID,Rank,FieldName,EngCaption,ArbCaption,DataType,Length,DefaultValue,IsArabic,IsActive,MaximumValue,MinimumValue,SearchID"
        mInsertParameterValues = " @ReportID,@Rank,@FieldName,@EngCaption,@ArbCaption,@DataType,@Length,@DefaultValue,@IsArabic,@IsActive,@MaximumValue,@MinimumValue,@SearchID"
        mUpdateParameter = "ReportID=@ReportID,Rank=@Rank,FieldName=@FieldName,EngCaption=@EngCaption,ArbCaption=@ArbCaption,DataType=@DataType,Length=@Length,DefaultValue=@DefaultValue,IsArabic=@IsArabic,IsActive=@IsActive,MaximumValue=@MaximumValue,MinimumValue=@MinimumValue,SearchID=@SearchID"
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
    End Sub

#Region "private members"
    Private mID As Integer
    Private mReportID As Integer
    Private mRank As Integer
    Private mFieldName As String
    Private mEngCaption As String
    Private mArbCaption As String
    Private mDataType As String
    Private mLength As Integer
    Private mDefaultValue As String
    Private mIsArabic As Boolean
    Private mIsActive As Boolean
    Private mMaximumValue As String
    Private mMinimumValue As String
    Private mSearchID As Integer
    Private mRegDate As Date
#End Region

#Region "Public property"
    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal value As Integer)
            mID = value
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
    Public Property FieldName() As String
        Get
            Return mFieldName
        End Get
        Set(ByVal value As String)
            mFieldName = value

        End Set
    End Property
    Public Property EngCaption() As String
        Get
            Return mEngCaption
        End Get
        Set(ByVal value As String)
            mEngCaption = value
        End Set
    End Property
    Public Property ArbCaption() As String
        Get
            Return mArbCaption
        End Get
        Set(ByVal value As String)
            mArbCaption = value

        End Set
    End Property
    Public Property DataType() As String
        Get
            Return mDataType

        End Get
        Set(ByVal value As String)
            mDataType = value

        End Set
    End Property
    Public Property Length() As Integer
        Get
            Return mLength

        End Get
        Set(ByVal value As Integer)
            mLength = value

        End Set
    End Property
    Public Property DefaultValue() As String
        Get
            Return mDefaultValue

        End Get
        Set(ByVal value As String)
            mDefaultValue = value
        End Set
    End Property
    Public Property IsArabic() As Boolean
        Get
            Return mIsArabic

        End Get
        Set(ByVal value As Boolean)
            mIsArabic = value
        End Set
    End Property
    Public Property IsActive() As Boolean
        Get
            Return mIsActive
        End Get
        Set(ByVal value As Boolean)
            mIsActive = value
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
#End Region

#Region "Public Function"
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Find all columns from sys_Cities table where filter and canceldate = null  
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class
    '       4-Return true if ID of Filteration >0 (Is Found)
    '==================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By FieldName "
            End If
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            StrSelectCommand = StrSelectCommand & orderByStr
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
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Save or Update row 
    'Steps: 
    '       1-Execute sql statment to get ID from sys_Cities where filter 
    '       2-Check if ID > 0 this mean that row is already exist in sys_Cities  table 
    '       the make Update to this row
    '           IF ID =0 this mean that row is new row Then Insert the row in sys_Cities  table
    '==================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String = String.Empty
        Dim Value As Integer
        Try
            strSQL = "Select ID From " & mTable & " Where " & Filter
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
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007

    'Description: Save New Row in sys_Cities  table
    'Steps: 
    '       1-execute sqlstatment to insert new row in sys_Cities  table

    '==================================================================
    Public Function Save() As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, OperationType.Save)
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
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update existing Row in sys_Cities  table where filter
    'Steps: 
    '       1-execute sqlstatment to Update existing row in sys_Cities  table

    '==================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String = String.Empty
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()

        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand, OperationType.Update)
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Delete existing Row in sys_Cities  table where filter
    'Steps: 
    '       1-execute sqlstatment to Delete existing row in sys_Cities  table

    '==================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String = String.Empty
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
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
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Description: Clear all private members  of the class

    '==================================================================
  
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Description:  find first row in sys_Cities table
    'Steps: 
    '       1-execute sqlstatment to find first row in sys_Cities  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code ASC"
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
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Description:  find Last row in sys_Cities  table
    'Steps: 
    '       1-execute sqlstatment to find last row in sys_Cities  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code DESC"
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
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Description:  find Next row in sys_Cities  table
    'Steps: 
    '       1-execute sqlstatment to find Next row in sys_Cities  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID > " & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >'" & mID & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Description:  find previous row in sys_Cities  table
    'Steps: 
    '       1-execute sqlstatment to find previous row in sys_Cities  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < '" & mID & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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

    Public Function GetParameter4SPDT(ByVal StrSP As String) As DataTable
        Dim objparam(0) As SqlClient.SqlParameter
        Dim objreader As SqlClient.SqlDataReader
        Dim objColumn As DataColumn
        Dim objdr As DataRow
        Dim DT As New DataTable
        Try
            DT = New DataTable
            objparam(0) = New SqlClient.SqlParameter("@sp_Name", SqlDbType.VarChar)

            objparam(0).Value = StrSP
            objreader = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "sp_GetParaInfo", objparam)

            objColumn = New DataColumn("FieldName", Type.GetType("System.String"))
            DT.Columns.Add(objColumn)

            objColumn = New DataColumn("EngCaption", Type.GetType("System.String"))
            DT.Columns.Add(objColumn)

            objColumn = New DataColumn("ArbCaption", Type.GetType("System.String"))
            DT.Columns.Add(objColumn)

            objColumn = New DataColumn("DataType", Type.GetType("System.String"))
            DT.Columns.Add(objColumn)

            objColumn = New DataColumn("length", Type.GetType("System.Int64"))
            DT.Columns.Add(objColumn)

            objColumn = New DataColumn("IsActive", Type.GetType("System.Boolean"))
            DT.Columns.Add(objColumn)

            While objreader.Read
                objdr = DT.NewRow()
                objdr("FieldName") = objreader("Name")
                objdr("EngCaption") = objreader("Name")
                objdr("ArbCaption") = objreader("Name")
                objdr("DataType") = objreader("DataType")
                objdr("length") = objreader("length")
                objdr("IsActive") = True
                DT.Rows.Add(objdr)
            End While

            Return DT
        Catch ex As Exception
            Return Nothing
        Finally

        End Try
    End Function

    Public Function GetParameter4TSQL(ByVal TSQL As String) As Data.DataTable 'ToDo 
        Dim objColumn As DataColumn
        Dim objdr As DataRow
        Dim DT As New DataTable
        Dim SelectPart As String
        Dim WherePart As String
        Dim WherePosition As Integer = TSQL.IndexOf("Where")
        Dim Parameters As ArrayList
        Dim StrParameters As String = ""
        Dim ParametersTypes As ArrayList
        Dim CurrentParameter As String
        Dim CurrentParameterStart As Boolean
        Try
            WherePart = TSQL.Substring(WherePosition, Len(TSQL) - WherePosition)
            For Each character As Char In WherePart.ToCharArray
                If character = "@" Then
                    CurrentParameterStart = True
                ElseIf " " And CurrentParameterStart Then
                    Parameters.Add(CurrentParameter)
                    CurrentParameter = String.Empty
                    CurrentParameterStart = False
                ElseIf CurrentParameterStart Then
                    CurrentParameter &= character
                End If
            Next




            DT = New DataTable

            objColumn = New DataColumn("FieldName", Type.GetType("System.String"))
            DT.Columns.Add(objColumn)

            objColumn = New DataColumn("EngCaption", Type.GetType("System.String"))
            DT.Columns.Add(objColumn)

            objColumn = New DataColumn("ArbCaption", Type.GetType("System.String"))
            DT.Columns.Add(objColumn)

            objColumn = New DataColumn("DataType", Type.GetType("System.String"))
            DT.Columns.Add(objColumn)

            objColumn = New DataColumn("length", Type.GetType("System.Int64"))
            DT.Columns.Add(objColumn)

            objColumn = New DataColumn("IsActive", Type.GetType("System.Boolean"))
            DT.Columns.Add(objColumn)
        Catch ex As Exception

        End Try
    End Function

    Public Function SaveCriteria(ByVal ReportID As Integer, ByVal ObjCriteriaDt As DataTable) As Boolean
        Dim StrSqlcommand As String = "Insert Into sys_ReportsCriterias(ReportID,Rank,FieldName,EngCaption,ArbCaption,DataType,Length,DefaultValue,IsArabic,IsActive,MaximumValue,MinimumValue,SearchID)Values" & _
        "([ReportID],[Rank],'[FieldName]','[EngCaption]','[ArbCaption]','[DataType]',[length],'[DefaultValue]',[IsArabic],[IsActive],[MaximumValue],[MinimumValue],[SearchID]);"
        Dim StrInsertCommand As String = String.Empty
        Dim StrFinalInsert As String = String.Empty
        Try
            For Each ObjRow As DataRow In ObjCriteriaDt.Rows
                StrInsertCommand = StrSqlcommand.Replace("[ReportID]", ReportID)
                StrInsertCommand = StrInsertCommand.Replace("[Rank]", ObjRow("Rank"))
                StrInsertCommand = StrInsertCommand.Replace("[FieldName]", ObjRow("FieldName"))
                StrInsertCommand = StrInsertCommand.Replace("[EngCaption]", [Shared].DataHandler.DataValue_Out(ObjRow("EngCaption"), SqlDbType.VarChar))
                StrInsertCommand = StrInsertCommand.Replace("[ArbCaption]", [Shared].DataHandler.DataValue_Out(ObjRow("ArbCaption"), SqlDbType.VarChar))
                StrInsertCommand = StrInsertCommand.Replace("[DataType]", [Shared].DataHandler.DataValue_Out(ObjRow("DataType"), SqlDbType.VarChar))
                StrInsertCommand = StrInsertCommand.Replace("[length]", [Shared].DataHandler.DataValue_Out(ObjRow("length"), SqlDbType.VarChar))
                StrInsertCommand = StrInsertCommand.Replace("[DefaultValue]", [Shared].DataHandler.DataValue_Out(ObjRow("DefaultValue"), SqlDbType.VarChar))
                StrInsertCommand = StrInsertCommand.Replace("[IsArabic]", IIf(ObjRow("IsArabic") = True, 1, 0))
                StrInsertCommand = StrInsertCommand.Replace("[IsActive]", IIf(ObjRow("IsActive") = True, 1, 0))
                StrInsertCommand = StrInsertCommand.Replace("[MaximumValue]", IIf(IsDBNull(ObjRow("MaximumValue")), "null", ObjRow("MaximumValue")))
                StrInsertCommand = StrInsertCommand.Replace("[MinimumValue]", IIf(IsDBNull(ObjRow("MinimumValue")), "null", ObjRow("MinimumValue")))
                StrInsertCommand = StrInsertCommand.Replace("[SearchID]", IIf(IsDBNull(ObjRow("SearchID")), "null", ObjRow("SearchID")))
                StrFinalInsert &= StrInsertCommand & vbNewLine
            Next
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, "Delete From sys_ReportsCriterias Where Reportid = " & ReportID & ";" & vbNewLine & StrFinalInsert)
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(StrFinalInsert, ex, Err.Number, mDataBaseUserRelatedID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function

    Public Function CheckDiffrance(ByVal OldCriteria As DataTable, ByVal NewCriteria As DataTable) As DataTable
        Dim ObjFinalCriteria As New DataTable
        Dim BolFound As Boolean
        Dim ObjFinalRow As DataRow
        Try
            ObjFinalCriteria = OldCriteria.Clone
            For Each ObjNewRow As DataRow In NewCriteria.Rows
                For Each ObjOldRow As DataRow In OldCriteria.Rows
                    If ObjOldRow("FieldName") = ObjNewRow("FieldName") Then
                        ObjFinalRow = ObjFinalCriteria.NewRow
                        ObjFinalRow("FieldName") = ObjNewRow("FieldName")
                        ObjFinalRow("EngCaption") = ObjOldRow("EngCaption")
                        ObjFinalRow("ArbCaption") = ObjOldRow("ArbCaption")
                        ObjFinalRow("DataType") = ObjNewRow("DataType")
                        ObjFinalRow("Length") = ObjNewRow("Length")
                        ObjFinalRow("DefaultValue") = ObjOldRow("DefaultValue")
                        ObjFinalRow("IsArabic") = ObjOldRow("IsArabic")
                        ObjFinalRow("IsActive") = ObjOldRow("IsActive")
                        ObjFinalRow("MaximumValue") = ObjOldRow("MaximumValue")
                        ObjFinalRow("MinimumValue") = ObjOldRow("MinimumValue")
                        ObjFinalRow("SearchID") = ObjOldRow("SearchID")
                        ObjFinalCriteria.Rows.Add(ObjFinalRow)
                        BolFound = True
                        Exit For
                    End If
                Next
                If Not BolFound Then
                    ObjFinalRow = ObjFinalCriteria.NewRow
                    ObjFinalRow("FieldName") = ObjNewRow("FieldName")
                    ObjFinalRow("EngCaption") = ObjNewRow("FieldName")
                    ObjFinalRow("ArbCaption") = ObjNewRow("FieldName")
                    ObjFinalRow("DataType") = ObjNewRow("DataType")
                    ObjFinalRow("Length") = ObjNewRow("Length")
                    ObjFinalRow("DefaultValue") = ""
                    ObjFinalRow("IsArabic") = False
                    ObjFinalRow("IsActive") = True
                    ObjFinalRow("MaximumValue") = DBNull.Value
                    ObjFinalRow("MinimumValue") = DBNull.Value
                    ObjFinalRow("SearchID") = DBNull.Value
                    ObjFinalCriteria.Rows.Add(ObjFinalRow)
                Else
                    BolFound = False
                End If
            Next
            Return ObjFinalCriteria
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserRelatedID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
            Return Nothing
        End Try
    End Function

    Public Function CreateParametersForView(ByRef pnlCriterias As Global.System.Web.UI.WebControls.Panel, ByRef dtParameters As DataTable, ByVal StrparmN() As String, ByVal StrParVal() As String) As Boolean

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
            StrLangLbl = "ArbCaption"
            StrFontName = "Tahoma"
        Else
            StrLangLbl = "EngCaption"
            StrFontName = "Arail"
        End If

        Try
            For Each ObjRow In dtParameters.Rows

                Intx = Array.IndexOf(StrparmN, ObjRow.Item("Fieldname"))
                If Intx >= 0 Then
                    ObjRow.Item("DefaultValue") = StrParVal(Intx).Replace(",", "")
                Else
                    ObjRow.Item("DefaultValue") = ObjRow.Item("DefaultValue")
                End If

                If ObjRow.Item("IsActive") = True Then
                    If blArCulture Then
                        'Prepare For Arabic Layout
                        If mDataHandler.DataValue_Out(ObjRow.Item("IsArabic"), SqlDbType.Bit) = True Then
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
                        If mDataHandler.DataValue_Out(ObjRow.Item("IsArabic"), SqlDbType.Bit) = False Then
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

#End Region

#Region "Private Function"
    '========================================================================
    'ProcedureName  :  GetParameter
    'Module         :  Sys (Report Writer Module)
    'Project        :  Venus V.
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[Mohammed Al-Janazrh]   
    'Date Created   :21-06-2009
    'Modifacations  :
    'Calls          :
    'From           :Find()
    '               :FirstRecord()
    '               :LastRecord()
    '               :PreviousRecord()
    '               :NextRecord()
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Ds             :DataSet     :used its attributes to assign them to private attributes
    '========================================================================
    Protected Sub Getparameter(ByVal Ds As DataSet)
        Try
            With Ds.Tables(0).Rows(0)
                mID = DataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                mReportID = DataHandler.DataValue_Out(.Item("ReportID"), SqlDbType.Int, True)
                mRank = DataHandler.DataValue_Out(.Item("Rank"), SqlDbType.Int)
                mFieldName = DataHandler.DataValue_Out(.Item("FieldName"), SqlDbType.VarChar)
                mEngCaption = DataHandler.DataValue_Out(.Item("EngCaption"), SqlDbType.VarChar)
                mArbCaption = DataHandler.DataValue_Out(.Item("ArbCaption"), SqlDbType.VarChar)
                mDataType = DataHandler.DataValue_Out(.Item("DataType"), SqlDbType.VarChar)
                mLength = DataHandler.DataValue_Out(.Item("Length"), SqlDbType.Int)
                mDefaultValue = DataHandler.DataValue_Out(.Item("DefaultValue"), SqlDbType.VarChar)
                mIsArabic = DataHandler.DataValue_Out(.Item("IsArabic"), SqlDbType.Bit)
                mIsActive = DataHandler.DataValue_Out(.Item("IsActive"), SqlDbType.Bit)
                mMaximumValue = DataHandler.DataValue_Out(.Item("MaximumValue"), SqlDbType.VarChar)
                mMinimumValue = DataHandler.DataValue_Out(.Item("MinimumValue"), SqlDbType.VarChar)
                mSearchID = DataHandler.DataValue_Out(.Item("SearchID"), SqlDbType.Int, True)
            End With
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserRelatedID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Sub
    '========================================================================
    'ProcedureName  :  SetParameter
    'Module         :  Sys (Report Writer Module)
    'Project        :  Venus V.
    'Description    :  Assign parameters of sql command  with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[Mohammed Al-Janazrh]   
    'Date Created   :21-06-2009
    'Modifacations  :
    'Calls          :
    'From           :Save()
    '               :Update()
    '               :Delete()
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Protected Sub SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String)
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportID", SqlDbType.Int)).Value = DataHandler.DataValue_In(mReportID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Rank", SqlDbType.Int)).Value = DataHandler.DataValue_In(mRank, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FieldName", SqlDbType.VarChar)).Value = DataHandler.DataValue_In(mFieldName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngCaption", SqlDbType.VarChar)).Value = DataHandler.DataValue_In(mEngCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbCaption", SqlDbType.VarChar)).Value = DataHandler.DataValue_In(mArbCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DataType", SqlDbType.VarChar)).Value = DataHandler.DataValue_In(mDataType, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Length", SqlDbType.Int)).Value = DataHandler.DataValue_In(mLength, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DefaultValue", SqlDbType.VarChar)).Value = DataHandler.DataValue_In(mDefaultValue, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsArabic", SqlDbType.Bit)).Value = DataHandler.DataValue_In(mIsArabic, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsActive", SqlDbType.Bit)).Value = DataHandler.DataValue_In(mIsActive, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaximumValue", SqlDbType.VarChar)).Value = DataHandler.DataValue_In(mMaximumValue, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MinimumValue", SqlDbType.VarChar)).Value = DataHandler.DataValue_In(mMinimumValue, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SearchID", SqlDbType.Int)).Value = DataHandler.DataValue_In(mSearchID, SqlDbType.Int, True)
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserRelatedID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Sub
    '========================================================================
    'ProcedureName  :  Clear
    'Module         :  Sys (Report Writer Module)
    'Project        :  Venus V.
    'Description    :  Clear all private attributes in class
    'Developer      :[Mohammed Al-Janazrh]   
    'Date Created   :21-06-2009
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Sub Clear()
        Try
            mID = 0
            mReportID = 0
            mRank = 0
            mFieldName = String.Empty
            mEngCaption = String.Empty
            mArbCaption = String.Empty
            mDataType = String.Empty
            mLength = 0
            mDefaultValue = String.Empty
            mIsArabic = False
            mIsActive = False
            mMaximumValue = Nothing
            mMinimumValue = Nothing
            mSearchID = 0
            mRegDate = Nothing
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Sub
#End Region
#Region "Class Destructors"
    Public Sub finalized()
        mDataSet.Dispose()
    End Sub
#End Region

End Class
