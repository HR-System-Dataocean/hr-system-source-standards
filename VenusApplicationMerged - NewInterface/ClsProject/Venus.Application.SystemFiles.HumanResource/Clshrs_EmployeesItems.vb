Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeesItemsBase
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Description:   In the constructor of the class set
    '                                       -Table name 
    '                                       -Sqlstatment(s) of (Insert,Update,Delete,select) row(s) dealing with the database
    '=====================================================================

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EmployeesItems "
        mInsertParameter = " EmployeeID,ItemID,ReceivedDate,ReturnedDate,ReceivingItemstatus,ReturningItemstatus,Remarks,RegUserID,RegComputerID,IsFromAssets,IsConfirmed "
        mInsertParameterValues = " @EmployeeID,@ItemID,@ReceivedDate,@ReturnedDate,@ReceivingItemstatus,@ReturningItemstatus,@Remarks,@RegUserID,@RegComputerID,@IsFromAssets,@IsConfirmed "
        mUpdateParameter = " EmployeeID=@EmployeeID,ItemID=@ItemID,ReceivedDate=@ReceivedDate,ReturnedDate=@ReturnedDate,ReceivingItemstatus=@ReceivingItemstatus,ReturningItemstatus=@ReturningItemstatus,Remarks=@Remarks,IsFromAssets=@IsFromAssets,IsConfirmed=@IsConfirmed "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

#End Region

#Region "Private Members"

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Description :  Private members declration
    '=====================================================================

    Protected mID As Object
    Private mEmployeeID As Object
    Private mItemID As Object
    Private mReceivedDate As Object
    Private mReturnedDate As Object
    Private mReceivingItemstatus As String
    Private mReturningItemstatus As String
    Private mIsFromAssets As Object
    Private mIsConfirmed As Object
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object

#End Region

#Region "Public property"

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Description :  Public property declration
    '=====================================================================

    Public Property ID() As Object
        Get
            Return mID
        End Get
        Set(ByVal Value As Object)
            mID = Value
        End Set
    End Property
    Public Property EmployeeID() As Object
        Get
            Return mEmployeeID
        End Get
        Set(ByVal Value As Object)
            mEmployeeID = Value
        End Set
    End Property
    Public Property ItemID() As Object
        Get
            Return mItemID
        End Get
        Set(ByVal Value As Object)
            mItemID = Value
        End Set
    End Property
    Public Property ReceivedDate() As Object
        Get
            Return mReceivedDate
        End Get
        Set(ByVal Value As Object)
            mReceivedDate = Value
        End Set
    End Property
    Public Property ReturnedDate() As Object
        Get
            Return mReturnedDate
        End Get
        Set(ByVal Value As Object)
            mReturnedDate = Value
        End Set
    End Property
    Public Property ReceivingItemstatus() As String
        Get
            Return mReceivingItemstatus
        End Get
        Set(ByVal Value As String)
            mReceivingItemstatus = Value
        End Set
    End Property
    Public Property ReturningItemstatus() As String
        Get
            Return mReturningItemstatus
        End Get
        Set(ByVal Value As String)
            mReturningItemstatus = Value
        End Set
    End Property
    Public Property IsFromAssets() As Object
        Get
            Return mIsFromAssets
        End Get
        Set(ByVal Value As Object)
            mIsFromAssets = Value
        End Set
    End Property
    Public Property IsConfirmed() As Object
        Get
            Return mIsConfirmed
        End Get
        Set(ByVal Value As Object)
            mIsConfirmed = Value
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Input : Filter AS String
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class if dataset doesn't return rows
    '       4-Return true if ID of Filteration >0 (Is Found)
    '
    'Description: Find all columns from hrs_EmployeeItems table where filter and canceldate = null 
    '=====================================================================

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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Input : Filter AS String
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class if dataset doesn't return rows
    '       4-Return true if ID of Filteration >0 (Is Found)
    '
    'Description:	Return The Search of the Employee 
    '               item Screen 	 
    '=====================================================================

    Public Function FindEmployeeItems(ByVal Filter As String) As Boolean
        Try
            mDataSet = New DataSet
            mDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, "hrs_GetEmployeesItems")
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("hrs_GetEmployeesItems", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_EmployeesItems Where " & Filter
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Steps: 
    '               - Execute sqlstatment to insert new row in hrs_EmployeeItems table
    '
    'Description:   Save New Row in hrs_EmployeeItems  table
    '=====================================================================

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

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Input : Filter AS String
    'Steps: 
    '               - Execute sqlstatment to Update existing row in hrs_EmployeeItems  table
    '
    'Description:   Update existing Row in hrs_EmployeeItems  table WHERE filter
    '=====================================================================

    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Input : Filter AS String
    'Steps: 
    '               - Execute sqlstatment to Delete existing row in hrs_EmployeeItems  table
    '
    'Description: Delete existing Row in hrs_EmployeeItems table where filter
    '=====================================================================

    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            'SetParameter(mSqlCommand)
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Description: Clear all private members  of the class
    '=====================================================================

    Public Function Clear() As Boolean
        Try
            mID = 0
            mEmployeeID = 0
            mItemID = 0
            mReceivedDate = Nothing
            mReturnedDate = Nothing
            mReceivingItemstatus = String.Empty
            mReturningItemstatus = String.Empty
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mIsConfirmed = False
            mIsFromAssets = False
            mRegDate = Nothing
            mCancelDate = Nothing

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Description:  find first row in hrs_EmployeeItems table for Selected Employee
    'Steps: 
    '       1-execute sqlstatment to find first row in hrs_EmployeeItems  table 
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '=====================================================================

    Public Function FirstRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where " & Filter & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Description:  find Last row in hrs_EmployeeItems  table for Selected Employee
    'Steps: 
    '       1-execute sqlstatment to find last row in hrs_EmployeeItems  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '=====================================================================

    Public Function LastRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where " & Filter & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Description:  find Next row in hrs_EmployeeItems  table for Selected Employee
    'Steps: 
    '       1-execute sqlstatment to find Next row in hrs_EmployeeItems  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '=====================================================================

    Public Function NextRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And " & Filter & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Steps: 
    '               - Execute proc sqlstatment to find previous row in hrs_EmployeeItems  table
    '               - Fill dataset with result of the proc
    '               - Call Getparameter function to insert the results of dataset into the private members of the class
    '
    'Description:   Find previous row in hrs_EmployeeItems table for Selected Employee
    '=====================================================================

    Public Function previousRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And " & Filter & " and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
    'Created by : [0259]
    'Date : 26/08/2007
    'Description: Getting if there are any difference between DataSet and Class's Properties
    '==================================================================

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

#End Region

#Region "Class Private Function"

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Input : Ds as Dataset 
    'Description:   Asign the Result of Ds to the private members of the class
    '=====================================================================

    Protected Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mEmployeeID = mDataHandler.DataValue_Out(.Item("EmployeeID"), SqlDbType.Int, True)
                mItemID = mDataHandler.DataValue_Out(.Item("ItemID"), SqlDbType.Int, True)
                mReceivedDate = mDataHandler.DataValue_Out(.Item("ReceivedDate"), SqlDbType.DateTime)
                mReturnedDate = mDataHandler.DataValue_Out(.Item("ReturnedDate"), SqlDbType.DateTime)
                mReceivingItemstatus = mDataHandler.DataValue_Out(.Item("ReceivingItemstatus"), SqlDbType.VarChar)
                mReturningItemstatus = mDataHandler.DataValue_Out(.Item("ReturningItemstatus"), SqlDbType.VarChar)
                mIsConfirmed = mDataHandler.DataValue_Out(.Item("IsConfirmed"), SqlDbType.Bit)
                mIsFromAssets = mDataHandler.DataValue_Out(.Item("IsFromAssets"), SqlDbType.Bit)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Description:   Make the values of parameter equal values of private member of the class
    '=====================================================================

    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ItemID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mItemID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReceivedDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mReceivedDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReturnedDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mReturnedDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReceivingItemstatus", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mReceivingItemstatus, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReturningItemstatus", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mReturningItemstatus, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsFromAssets", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsFromAssets, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsConfirmed", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsConfirmed, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function


    Private Function FixNull(ByVal obj As Object, ByVal DataColumn As Global.System.Data.DataColumn) As Object
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
#End Region

#Region "Class Destructors"

    '=====================================================================
    'Created by : DataOcean
    'Date : 19/07/2007
    'Description :  Dispose dataset from the stack
    '=====================================================================

    Public Sub finalized()
        mDataSet.Dispose()
    End Sub
#End Region

End Class

Public Class Clshrs_EmployeesItems
    Inherits Clshrs_EmployeesItemsBase

#Region "Class Constructors"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
    End Sub

#End Region



    '==================================================================
    'Created by : DataOcean
    'Date : 09/07/2007
    'Input : DdlValues as DropDownList
    '       Condition to fill dropdownlist (if Wanted)
    '       NullNode to make attention to user by make first item ="Please Select"    
    'Description:  Fill DropDownList With Items 
    'Modified by    : MAz
    'Date           : 05-12-2007
    'Description    : Update method to match with Language
    '==================================================================
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            '-------------------------------0257 MODIFIED-----------------------------------------
            'StrSelectCommand = "Select * from hrs_items where (SELECT [dbo].[ISItemValid](id))=1 Order By EngName "
            StrSelectCommand = "Select * from hrs_items where (SELECT [dbo].[ISItemValid](id))=1 and Isnull(CancelDate,'')='' Order By EngName "
            '-------------------------------=============-----------------------------------------


            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                'Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")

                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                'Item.Text = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                Item.Value = ObjDataRow("ID")
                DdlValues.Items.Add(Item)
            Next

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

    'Modified by    : MAz
    'Date           : 05-12-2007
    'Description    : Update method to match with Language
    Public Function GetList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                'Item.DisplayText = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
                Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                If (Item.DisplayText.Trim = "") Then
                    Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
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
    '==================================================================
    'Created by : [0258]
    'Date : 8/8/2007
    'Description:  Check Duplicated Row in hrs_EmployeesItem Table
    '===================================================================
    Public Function ChecckTrainingDate(ByVal EmployeeID As Integer, ByVal ItemID As Integer, ByVal ReceivedDate As Date, ByVal ID As Integer) As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.StoredProcedure
            mSqlCommand.CommandText = "hrs_CheckEmployeeItemDuplicate"
            mSqlCommand.Parameters.AddWithValue("@EmployeeID", EmployeeID)
            mSqlCommand.Parameters.AddWithValue("@ItemID", ItemID)
            mSqlCommand.Parameters.AddWithValue("@ReceivedDate", ReceivedDate)
            mSqlCommand.Parameters.AddWithValue("@ID", ID)
            mSqlCommand.Connection.Open()
            Return mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function
End Class
