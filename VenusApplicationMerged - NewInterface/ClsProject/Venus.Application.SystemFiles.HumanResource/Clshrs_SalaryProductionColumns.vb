
'=========================================================================
'Created by : DataOcean
'Date : 06/08/2007
'                   Class: SalaryProductionfields
'                   Table: hrs_SalaryProductionfields
'Developer  : [0259]
'              : B#g0010 [0256] 23-06-2008 Add _Forgin arrgument to data_ValueIn method in SetParameters Function 
'              :                           And _Forgin arrgument to data_value_Out Method in GetParameters Function 
'              :                           To avoid saving non-existing forign key in any table 
'              :                           And convert the DBnull values to zero in case of forign key fields only 
'=========================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_SalaryProductionColumns
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    '=====================================================================
    'Created by : DataOcean
    'Date : 05/08/2007
    'Description:   In the constructor of the class set
    '                                       -Table name 
    '                                       -Sqlstatment(s) of (Insert,Update,Delete,select) row(s) dealing with the database
    '=====================================================================

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_SalaryProductionColumns "
        mInsertParameter = " Name "
        mInsertParameterValues = " @Name"
        mUpdateParameter = " Name=@Name"
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

#End Region

#Region "Private Members"

    Private mID As Object

    Private mName As String


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

    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal Value As String)
            mName = Value
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
    'Date : 10/07/2007
    'Input : Filter AS String
    'Steps: 
    '               - Fill Dataset with the results of sqldataAdapter
    '               - Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '               - Clear all private members of the class
    '               - Return true if ID of Filteration >0 (Condition Is Found)
    '
    'Description:   Find all columns from hrs_VacationsTypes table WHERE filter and canceldate = null 
    '=====================================================================

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            '==================== Order By Modification [Start]
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By Id "
            End If
            'Filter = Filter.Replace("�", " AM ").Replace("�", " PM ")
            '==================== Order By Modification [ End ]
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where CancelDate Is Null And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID = " & Me.MainCompanyID)
            '==================== Order By Modification [Start]
            StrSelectCommand = StrSelectCommand & orderByStr
            '==================== Order By Modification [ End ]
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
    'Date : 10/07/2007
    'Description: Clear all private members  of the class
    '=====================================================================

    Public Function Clear() As Boolean

        Try
            mID = 0

            mName = String.Empty


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

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Steps: 
    '               -Execute proc (StrSelectCommand) to find first row in hrs_VacationsTypes table
    '               -Fill dataset with result of the proc
    '               -Call Getparameter function to insert the results of dataset into the private members of the class
    'Description:   Find first row in hrs_VacationsTypes table
    '=====================================================================

    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  and CompanyID = " & Me.MainCompanyID & " ORDER BY Id ASC"
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
    'Date : 10/07/2007
    'Steps: 
    '               - Execute proc (StrSelectCommand) to find last row in hrs_VacationsTypes table
    '               - Fill dataset with result of sqlstatment
    '               - Call Getparameter function to insert the results of dataset into the private members of the class
    'Description:  Find Last row in hrs_VacationsTypes table
    '=====================================================================

    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  and CompanyID = " & Me.MainCompanyID & " ORDER BY Id DESC"
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
    'Date : 10/07/2007
    'Steps: 
    '               - Execute proc (StrSelectCommand) to find Next row in hrs_VacationsTypes table
    '               - Fill dataset with result of the proc
    '               - Call Getparameter function to insert the results of dataset into the private members of the class
    'Description:   Find Next row in hrs_VacationsTypes table
    '=====================================================================



    '==================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Steps: 
    '               - Execute proc (StrSelectCommand) to find previous row in hrs_VacationsTypes table
    '               - Fill dataset with result of the proc
    '               - Call Getparameter function to insert the results of dataset into the private members of the class
    'Description:   Find previous row in hrs_VacationsTypes table
    '==================================================================



#End Region

#Region "Class Private Function"

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Input : Ds as Dataset 
    'Description:   Asign the Result of Ds to the private members of the class
    '=====================================================================

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)

                mName = mDataHandler.DataValue_Out(.Item("Name"), SqlDbType.VarChar)


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
    'Date : 10/07/2007
    'Description:   Make the values of parameter equal values of private member of the class
    '=====================================================================

    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mName, SqlDbType.VarChar)



            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)




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


