Imports Venus.Application.SystemFiles.System
Public Class Clshrs_FormControls
    Inherits ClsDataAcessLayer
    Dim ObjConnection As New SqlClient.SqlConnection("Data Source=(local);Initial Catalog=Venus;User ID=sa")

#Region "Class Constructors"
    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Input : 
    'Description: In the constructor of the class set the table name and 
    '           sqlstatment of (Insert,Update,Delete,select) row from the table

    '==================================================================
    Public Sub New(ByVal Page As Web.UI.Page, ByVal TableName As String)

        MyBase.New(Page)
        Me.Table = TableName
        Me.mInsertParameter = " FormID,Name,EngCaption,ArbCaption,Compulsory,Format,ArbFormat,ToolTip,ArbToolTip,MaxLength,[IsNumeric],IsHide,FocusOnStartUp,Rank,MinValue,MaxValue,RegUserID,RegComputerID "
        Me.mInsertParameterValues = " @FormID,@Name,@EngCaption,@ArbCaption,@Compulsory,@Format,@ArbFormat,@ToolTip,@ArbToolTip,@MaxLength,@IsNumeric,@IsHide,@FocusOnStartUp,@Rank,@MinValue,@MaxValue,@RegUserID,@RegComputerID "
        Me.mUpdateParameter = " Name=@Name,EngCaption=@EngCaption,ArbCaption=@ArbCaption,Compulsory=@Compulsory,Format=@Format,ArbFormat=@ArbFormat,ToolTip=@ToolTip,ArbToolTip=@ArbToolTip,MaxLength=@MaxLength,IsNumeric=@IsNumeric,IsHide=@IsHide,FocusOnStartUp=@FocusOnStartUp,Rank=@Rank,MinValue=@MinValue,MaxValue=@MaxValue,CancelDate=null "
        Me.mSelectCommand = " Select * From  " & mTable
        Me.mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        Me.mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        Me.mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"

    End Sub
#End Region

#Region "Private Members"

    Private mID As Object
    Private mFormID As Integer
    Private mName As String
    Private mEngCaption As String
    Private mArbCaption As String
    Private mCompulsory As String
    Private mFormat As String
    Private mArbFormat As String
    Private mToolTip As String
    Private mArbToolTip As String
    Private mMaxLength As Integer
    Private mIsNumeric As String
    Private mIsHide As String
    Private mFocusOnStartUp As String
    Private mRank As Integer
    Private mMinValue As Integer
    Private mMaxValue As Integer
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object

#End Region
    '==================================================================
    '
    '==================================================================
#Region "Public property"

    Public Property ID() As Object
        Get
            Return mID
        End Get
        Set(ByVal Value As Object)
            mID = Value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal Value As String)
            mName = Value
        End Set
    End Property

    Public Property EngCaption() As String
        Get
            Return mEngCaption
        End Get
        Set(ByVal Value As String)
            mEngCaption = Value
        End Set
    End Property

    Public Property ArbCaption() As String
        Get
            Return mArbCaption
        End Get
        Set(ByVal Value As String)
            mArbCaption = Value
        End Set
    End Property

    Public Property Compulsory() As String
        Get
            Return mCompulsory
        End Get
        Set(ByVal Value As String)

            mCompulsory = Value
        End Set
    End Property


    Public Property MaxValue() As Integer
        Get
            Return mMaxValue
        End Get
        Set(ByVal Value As Integer)
            mMaxValue = Value
        End Set
    End Property
    Public Property MinValue() As Integer
        Get
            Return mMinValue
        End Get
        Set(ByVal Value As Integer)
            mMinValue = Value
        End Set
    End Property
    Public Property Rank() As Integer
        Get
            Return mRank
        End Get
        Set(ByVal Value As Integer)
            mRank = Value
        End Set
    End Property
    Public Property FocusOnStartUp() As String
        Get
            Return mFocusOnStartUp
        End Get
        Set(ByVal Value As String)
            mFocusOnStartUp = Value
        End Set
    End Property
    Public Property IsHide() As String
        Get
            Return mIsHide
        End Get
        Set(ByVal Value As String)
            mIsHide = Value
        End Set
    End Property
    Public Property IsNumeric() As String
        Get
            Return mIsNumeric
        End Get
        Set(ByVal Value As String)
            mIsNumeric = Value
        End Set
    End Property
    Public Property MaxLength() As Integer
        Get
            Return mMaxLength
        End Get
        Set(ByVal Value As Integer)
            mMaxLength = Value
        End Set
    End Property
    Public Property ToolTip() As String
        Get
            Return mToolTip
        End Get
        Set(ByVal Value As String)
            mToolTip = Value
        End Set
    End Property
    Public Property ArbFormat() As String
        Get
            Return mArbFormat
        End Get
        Set(ByVal Value As String)
            mArbFormat = Value
        End Set
    End Property
    Public Property ArbToolTip() As String
        Get
            Return mArbToolTip
        End Get
        Set(ByVal Value As String)
            mArbToolTip = Value
        End Set
    End Property

    Public Property Format() As String
        Get
            Return mFormat
        End Get
        Set(ByVal Value As String)
            mFormat = Value
        End Set
    End Property

    Public Property RegUserID() As Object
        Get
            Return mRegUserID
        End Get
        Set(ByVal Value As Object)
            mRegUserID = Value
        End Set
    End Property

    Public Property RegComputerID() As Object
        Get
            Return mRegComputerID
        End Get
        Set(ByVal Value As Object)
            mRegComputerID = Value
        End Set
    End Property

    Public Property RegDate() As Object
        Get
            Return mRegDate
        End Get
        Set(ByVal Value As Object)
            mRegDate = Value
        End Set
    End Property

    Public Property CancelDate() As Object
        Get
            Return mCancelDate
        End Get
        Set(ByVal Value As Object)
            mCancelDate = Value
        End Set
    End Property

#End Region

#Region "Public Function"
    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Find all columns from FormControl table where filter and canceldate = null  
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class
    '       4-Return true if ID of Filteration >0 (Is Found)
    '==================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Save or Update row 
    'Steps: 
    '       1-Execute sql statment to get ID from sys_FormsControls where filter 
    '       2-Check if ID > 0 this mean that row is already exist in sys_FormsControls table 
    '       the make Update to this row
    '           IF ID =0 this mean that row is new row Then Insert the row in sys_FormsControls table
    '==================================================================

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Try
            Dim strSQL As String
            Dim Value As Integer
            strSQL = "Select ID From sys_FormsControls Where " & Filter
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Input : ID as integer (FormID from sys_Forms table)
    'Description : Execute hrs_SelectFormName Stored procedure to Get information about the Form (Code of Form, English Caption of Form,Description of Form)
    '              
    '==================================================================
    Public Function GetFormName(ByVal ID As Integer) As DataTable
        Dim ObjSqldataadapter As New SqlClient.SqlDataAdapter("hrs_SelectFormName", ObjConnection)
        ObjSqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure
        ObjSqldataadapter.SelectCommand.Parameters.AddWithValue("@ID", ID)
        Dim ObjTable As New DataTable()
        Try
            ObjSqldataadapter.Fill(ObjTable)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
        Return ObjTable
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007

    'Description: Save New Row in sys_FormsControls table
    'Steps: 
    '       1-execute sqlstatment to insert new row in sys_FormsControls table

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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update existing Row in sys_FormsControls table where filter
    'Steps: 
    '       1-execute sqlstatment to Update existing row in sys_FormsControls table

    '==================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand, OperationType.Update)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Delete existing Row in sys_FormsControls table where filter
    'Steps: 
    '       1-execute sqlstatment to Delete existing row in sys_FormsControls table

    '==================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Description: Clear all private members  of the class

    '==================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mRemarks = String.Empty
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
    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Description:  find first row in sys_FormsControls table
    'Steps: 
    '       1-execute sqlstatment to find first row in sys_FormsControls table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
    'Date : 26/6/2007
    'Description:  find Last row in sys_FormsControls table
    'Steps: 
    '       1-execute sqlstatment to find last row in sys_FormsControls table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
    'Date : 26/6/2007
    'Description:  find Next row in sys_FormsControls table
    'Steps: 
    '       1-execute sqlstatment to find Next row in sys_FormsControls table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
    'Date : 26/6/2007
    'Description:  find previous row in sys_FormsControls table
    'Steps: 
    '       1-execute sqlstatment to find previous row in sys_FormsControls table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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

#End Region

#Region "Class Private Function"
    '===================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Input : Ds as Dataset 
    'Description:   Asign the Result of Ds to the private members of the class

    '===================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mName = mDataHandler.DataValue_Out(.Item("Name"), SqlDbType.VarChar)
                mEngCaption = mDataHandler.DataValue_Out(.Item("EngCaption"), SqlDbType.VarChar)
                mArbCaption = mDataHandler.DataValue_Out(.Item("ArbCaption"), SqlDbType.VarChar)
                mCompulsory = mDataHandler.DataValue_Out(.Item("Compulsory"), SqlDbType.Bit)
                mFormat = mDataHandler.DataValue_Out(.Item("Format"), SqlDbType.VarChar)
                mArbFormat = mDataHandler.DataValue_Out(.Item("ArbFormat"), SqlDbType.VarChar)
                mToolTip = mDataHandler.DataValue_Out(.Item("ToolTip"), SqlDbType.VarChar)
                mArbToolTip = mDataHandler.DataValue_Out(.Item("ArbToolTip"), SqlDbType.VarChar)
                mMaxLength = mDataHandler.DataValue_Out(.Item("MaxLength"), SqlDbType.Int)
                mIsNumeric = mDataHandler.DataValue_Out(.Item("IsNumeric"), SqlDbType.Bit)
                mIsHide = mDataHandler.DataValue_Out(.Item("IsHide"), SqlDbType.Bit)
                mFocusOnStartUp = mDataHandler.DataValue_Out(.Item("FocusOnStartUp"), SqlDbType.VarChar)
                mRank = mDataHandler.DataValue_Out(.Item("Rank"), SqlDbType.VarChar)
                mMinValue = mDataHandler.DataValue_Out(.Item("MinValue"), SqlDbType.Int)
                mMaxValue = mDataHandler.DataValue_Out(.Item("MaxValue"), SqlDbType.Int)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '===================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Description:   Make the values of parameter equal values of private member  of the class

    '===================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal ptrOperationType As OperationType) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FormID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mID, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Name", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngCaption", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbCaption", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Compulsory", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCompulsory, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Format", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFormat, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbFormat", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbFormat, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ToolTip", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mToolTip, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbToolTip", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbToolTip, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaxLength", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mMaxLength, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsNumeric", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsNumeric, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsHide", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsHide, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FocusOnStartUp", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mFocusOnStartUp, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Rank", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRank, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MinValue", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mMinValue, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaxValue", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mMaxValue, SqlDbType.VarChar)
            Select Case ptrOperationType
                Case OperationType.Save
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            End Select
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region

#Region "Class Destructors"

    Protected Overloads Sub finalized()
        mDataSet.Dispose()
    End Sub

#End Region

End Class
