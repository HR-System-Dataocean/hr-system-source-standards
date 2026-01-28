Imports Venus.Application.SystemFiles.System
Public Class Clshrs_TrainingBase
    Inherits ClsDataAcessLayer
#Region "Class Constructors"

    '==================================================================
    'Created by : [0258]
    'Date : 8/8/2007
    'Input : 
    'Description: In the constructor of the class set the table name and 
    '           sqlstatment of (Insert,Update,Delete,select) row from the table

    '==================================================================

    Public Sub New(ByVal Page As Web.UI.Page, ByVal TableName As String)

        MyBase.New(Page)
        With Me
            .Table = TableName
            .mInsertParameter = " CourseID,LocationID,StartDate,EndDate,TrainingHours,MaximumAttendats,EmployeeCost,Remarks,RegUserID,RegComputerID "
            .mInsertParameterValues = " @CourseID,@LocationID,@StartDate,@EndDate,@TrainingHours,@MaximumAttendats,@EmployeeCost,@Remarks,@RegUserID,@RegComputerID "
            .mUpdateParameter = " CourseID=@CourseID,LocationID=@LocationID,StartDate=@StartDate,EndDate=@EndDate,TrainingHours=@TrainingHours,MaximumAttendats=@MaximumAttendats,EmployeeCost=@EmployeeCost,Remarks=@Remarks "
            .mSelectCommand = " Select * From  " & mTable
            .mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
            .mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
            .mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
        End With
    End Sub

#End Region

#Region "Private Members"
    Private mID As Object
    Private mCourseID As Integer
    Private mLocationID As Integer
    Private mStartDate As Object
    Private mEndDate As Object
    Private mTrainingHours As String
    Private mMaximumAttendats As String
    Private mEmployeeCost As Object
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object

#End Region

#Region "Public property"

    Public Property ID() As Object
        Get
            Return mID
        End Get
        Set(ByVal Value As Object)
            mID = Value
        End Set
    End Property

    Public Property CourseID() As Integer
        Get
            Return mCourseID
        End Get
        Set(ByVal Value As Integer)
            mCourseID = Value
        End Set
    End Property

    Public Property LocationID() As Integer
        Get
            Return mLocationID
        End Get
        Set(ByVal Value As Integer)
            mLocationID = Value
        End Set
    End Property

    Public Property StartDate() As Object
        Get
            Return mStartDate
        End Get
        Set(ByVal Value As Object)
            mStartDate = Value

        End Set
    End Property

    Public Property EndDate() As Object
        Get
            Return mEndDate
        End Get
        Set(ByVal Value As Object)

            mEndDate = Value
        End Set
    End Property
    Public Property TrainingHours() As String
        Get
            Return mTrainingHours
        End Get
        Set(ByVal value As String)
            mTrainingHours = value
        End Set
    End Property

    Public Property EmployeeCost() As Object
        Get
            Return mEmployeeCost
        End Get
        Set(ByVal value As Object)
            mEmployeeCost = value
        End Set
    End Property

    Public Property MaximumAttendats() As String
        Get
            Return mMaximumAttendats
        End Get
        Set(ByVal value As String)
            mMaximumAttendats = value
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
    '==================================================================
    'Created by : [0258]
    'Date : 8/8/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Find all columns from hrs_Training table where filter and canceldate = null  
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class
    '       4-Return true if ID of Filteration >0 (Is Found)
    '==================================================================

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = CONFIG_DATEFORMAT & mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
    'Date : 8/8/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Save or Update row 
    'Steps: 
    '       1-Execute sql statment to get ID from hrs_Training where filter 
    '       2-Check if ID > 0 this mean that row is already exist in hrs_Training  table 
    '       the make Update to this row
    '           IF ID =0 this mean that row is new row Then Insert the row in hrs_Training  table
    '==================================================================

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Try
            Dim strSQL As String
            Dim Value As Integer
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '==================================================================
    'Created by : [0258]
    'Date : 8/8/2007

    'Description: Save New Row in hrs_Training  table
    'Steps: 
    '       1-execute sqlstatment to insert new row in hrs_Training  table

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
    'Date : 8/8/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update existing Row in hrs_Training  table where filter
    'Steps: 
    '       1-execute sqlstatment to Update existing row in hrs_Training  table

    '==================================================================

    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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
    '==================================================================
    'Created by : [0258]
    'Date : 8/8/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Delete existing Row in hrs_Training  table where filter
    'Steps: 
    '       1-execute sqlstatment to Delete existing row in hrs_Training  table

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
    'Date : 8/8/2007
    'Description: Clear all private members  of the class

    '==================================================================

    Public Function Clear() As Boolean
        Try
            mID = 0
            mCourseID = 0
            mLocationID = 0
            mStartDate = Nothing
            mEndDate = Nothing
            mTrainingHours = String.Empty
            mMaximumAttendats = 0
            mEmployeeCost = String.Empty
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
    'Date : 8/8/2007
    'Description:  find first row in sys_FormsControls table
    'Steps: 
    '       1-execute sqlstatment to find first row in hrs_Training  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================

    Public Function FirstRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
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

    '==================================================================
    'Created by : [0258]
    'Date : 8/8/2007
    'Description:  find Last row in hrs_Training  table
    'Steps: 
    '       1-execute sqlstatment to find last row in hrs_Training  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================

    Public Function LastRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
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

    '==================================================================
    'Created by : [0258]
    'Date : 8/8/2007
    'Description:  find Next row in hrs_Training  table
    'Steps: 
    '       1-execute sqlstatment to find Next row in hrs_Training  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================

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

    '==================================================================
    'Created by : [0258]
    'Date : 8/8/2007
    'Description:  find previous row in hrs_Training  table
    'Steps: 
    '       1-execute sqlstatment to find previous row in hrs_Training  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================

    Public Function previousRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And " & Filter & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
    'Date : 8/8/2007
    'Description:  Check Duplicated Row in hrs_training Table
    '===================================================================
    Public Function ChecckTrainingDate(ByVal CourseID As Integer, ByVal LocationID As Integer, ByVal StartDate As Date, ByVal EndDate As Date, ByVal ID As Integer) As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.StoredProcedure
            mSqlCommand.CommandText = "hrs_CheckTrainingDate"
            mSqlCommand.Parameters.AddWithValue("@CourseID", CourseID)
            mSqlCommand.Parameters.AddWithValue("@LocationID", LocationID)
            mSqlCommand.Parameters.AddWithValue("@StartDate", StartDate)
            mSqlCommand.Parameters.AddWithValue("@EndDate", EndDate)
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

#End Region

#Region "Class Private Function"
    '===================================================================
    'Created by : [0258]
    'Date : 8/8/2007
    'Input : Ds as Dataset 
    'Description:   Asign the Result of Ds to the private members of the class

    '===================================================================

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCourseID = mDataHandler.DataValue_Out(.Item("CourseID"), SqlDbType.Int, True)
                mLocationID = mDataHandler.DataValue_Out(.Item("LocationID"), SqlDbType.Int, True)
                mStartDate = mDataHandler.DataValue_Out(.Item("StartDate"), SqlDbType.DateTime)
                mEndDate = mDataHandler.DataValue_Out(.Item("EndDate"), SqlDbType.DateTime)
                mTrainingHours = mDataHandler.DataValue_Out(.Item("TrainingHours"), SqlDbType.Real)
                mMaximumAttendats = mDataHandler.DataValue_Out(.Item("MaximumAttendats"), SqlDbType.Int)
                mEmployeeCost = mDataHandler.DataValue_Out(.Item("EmployeeCost"), SqlDbType.Money)
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

    '===================================================================
    'Created by : [0258]
    'Date : 8/8/2007
    'Description:   Make the values of parameter equal values of private member  of the class

    '===================================================================

    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal ptrOperationType As OperationType) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CourseID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCourseID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LocationID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mLocationID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@StartDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mStartDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EndDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mEndDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TrainingHours", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mTrainingHours, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaximumAttendats", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mMaximumAttendats, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeCost", SqlDbType.Money)).Value = mDataHandler.DataValue_In(mEmployeeCost, SqlDbType.Real)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeCost", SqlDbType.Money)).Value = mEmployeeCost
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
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
Public Class Clshrs_Training
    Inherits Clshrs_TrainingBase

    '==================================================================
    'Created by : [0258]
    'Date : 8/8/2007
    'Input : 
    'Description: In the constructor of the class set the table name and 
    '           sqlstatment of (Insert,Update,Delete,select) row from the table

    '==================================================================

    Public Sub New(ByVal Page As Web.UI.Page, ByVal TableName As String)

        MyBase.New(Page, "hrs_Training")

    End Sub
    '==================================================================
    'Created by :DataOcean
    'Date : 09/07/2007
    'Input : DdlValues as DropDownList
    '       Condition to fill dropdownlist (if Wanted)
    '       NullNode to make attention to user by make first item ="Please Select"    
    'Description:  Fill DropDownList with Items 
    '==================================================================
    'Modification :  [0256] 5-12-2007 Add SetLanguage Function to Switch Between EngName , ArbName Fields 
    '             :                  According to Page Language 
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By EngName ", "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & " Order By EngName ")
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

    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList) As Boolean
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
                'Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
                Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                If (Item.DisplayText.Trim = "") Then
                    Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                'Item.DisplayText = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
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
End Class