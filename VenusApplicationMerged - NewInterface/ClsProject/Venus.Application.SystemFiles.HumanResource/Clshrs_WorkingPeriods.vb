'==========================================================================
'Program File Name : Payroll.Net
'Project           : Venus V.
'Module            : Hrs (Human Resource Module)
'Date Created      : 11-07-2007
'Developer         : DataOcean
'Description       : 1-Implement Data Acess Layer of hrs_WorkingPeriods table with fields code,English name,
'                       Arabic name,Employee class ,from date and end date
'
'                    2-Allow search in working periods using critra
'                    3-Get list with all codes of working periods records
'                    4-Implement functions save(), update() and delete() to allow DML with some critera
'                    5-Implement functions first(),last(),next() and previous() to allow navigation between 
'                       records
'
'
'==========================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_WorkingPeriodsBase
    Inherits ClsDataAcessLayer

#Region "Class Constructors"
    '========================================================================
    'ProcedureName  :  Constractor 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  To create new object from class
    'Developer      :   
    'Date Created   :  
    'Modifacations  : [0257] [11-07-2007]
    '                   1-delete Company Id field from mInsertParameter,mInsertParameterValues and
    '                   mUpdateParameter
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_WorkingPeriods "
        'mInsertParameter = " Code,EngName,ArbName,SearchArbName,EmployeeClassID,Remarks,FromDate,ToDate,RegUserID,RegComputerID,CompanyID "
        'mInsertParameterValues = " @Code,@EngName,@ArbName,@SearchArbName,@EmployeeClassID,@Remarks,@FromDate,@ToDate,@RegUserID,@RegComputerID,@CompanyID "
        'mUpdateParameter = " Code=@Code,EngName=@EngName,ArbName=@ArbName,SearchArbName=@SearchArbName,EmployeeClassID=@EmployeeClassID,Remarks=@Remarks,FromDate=@FromDate,ToDate=@ToDate,RegUserID=@RegUserID,RegComputerID=@RegComputerID,CompanyID=@CompanyID "
        mInsertParameter = " Code,EngName,ArbName,SearchArbName,EmployeeClassID,Remarks,FromDate,ToDate,RegUserID,RegComputerID "
        mInsertParameterValues = " @Code,@EngName,@ArbName,@SearchArbName,@EmployeeClassID,@Remarks,@FromDate,@ToDate,@RegUserID,@RegComputerID "
        mUpdateParameter = " Code=@Code,EngName=@EngName,ArbName=@ArbName,SearchArbName=@SearchArbName,EmployeeClassID=@EmployeeClassID,Remarks=@Remarks,FromDate=@FromDate,ToDate=@ToDate,RegUserID=@RegUserID,RegComputerID=@RegComputerID "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region

#Region "Private Members"
    Private mID As Object
    Private mCode As String
    Private mEngName As String
    Private mArbName As String
    Private mSearchArbName As String
    Private mEmployeeClassID As Object
    Private mRemarks As String
    Private mFromDate As Object
    Private mToDate As Object
    Private mRegUserID As Object
    Private mRegComputerID As Object
    'Private mCompanyID As Object
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
    Public Property Code() As String
        Get
            Return mCode
        End Get
        Set(ByVal Value As String)
            mCode = Value
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
            mStringHandler.ReplaceHamza(Value)
        End Set
    End Property
    Public Property SearchArbName() As String
        Get
            Return mSearchArbName
        End Get
        Set(ByVal Value As String)
            mSearchArbName = Value
        End Set
    End Property
    Public Property EmployeeClassID() As Object
        Get
            Return mEmployeeClassID
        End Get
        Set(ByVal Value As Object)
            mEmployeeClassID = Value
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
    Public Property FromDate() As Object
        Get
            Return mFromDate
        End Get
        Set(ByVal Value As Object)
            mFromDate = Value
        End Set
    End Property
    Public Property ToDate() As Object
        Get
            Return mToDate
        End Get
        Set(ByVal Value As Object)
            mToDate = Value
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
    'Public Property CompanyID() As Object
    '    Get
    '        Return mCompanyID
    '    End Get
    '    Set(ByVal Value As Object)
    '        mCompanyID = Value
    '    End Set
    'End Property
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
    '========================================================================
    'ProcedureName  :  Find 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find all rows in hrs_WorkingPeriods that match criteria or filter and canceldate = null
    '               Steps:
    '                   1-Set Select Statment
    '                   2-Intialize DataSet And Adapter with Select statment and connection
    '                   3-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '                   4-Clear all private members of the class if data not valid
    '                   5-Return true if ID of Filteration >0 (Is Found)
    'Developer      : DataOcean 
    'Date Created   : 11-07-07 
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter:String:used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            '==================== Order By Modification [Start]
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By Code "
            End If
            'Filter = Filter.Replace("Õ", " AM ").Replace("ã", " PM ")
            '==================== Order By Modification [ End ]
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    '========================================================================
    'ProcedureName  :  SaveUpdate
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save Or Update Row that match with critera in hrs_WorkingPeriods table 
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean 
    'Date Created   :  11-07-07
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '    Filter         : String    : used as critera in select statment like 'ID=2'
    ' 
    '========================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_WorkingPeriods Where " & Filter
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

    '========================================================================
    'ProcedureName  :  Save
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save new row into hrs_WorkingPeriods table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   : 11-07-07 
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
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

    '========================================================================
    'ProcedureName  :  Update
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  update row that match with critera into hrs_WorkingPeriods table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  11-07-07
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
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

    '========================================================================
    'ProcedureName  :  Delete
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Delete new row that match critera into hrs_WorkingPeriods table
    '                  and return true if operation done otherwise report errors in ErrorPage                    
    'Developer      :  DataOcean
    'Date Created   :  11-07-07
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
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

    '========================================================================
    'ProcedureName  :  Clear
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Clear all private attributes in class
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  11-07-07
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mCode = String.Empty
            mEngName = String.Empty
            mArbName = String.Empty
            mSearchArbName = String.Empty
            mEmployeeClassID = 0
            mRemarks = String.Empty
            mFromDate = Nothing
            mToDate = Nothing
            mRegUserID = 0
            mRegComputerID = 0
            'mCompanyID = 0
            mRegDate = Nothing
            mCancelDate = Nothing

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  FirstRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get first record in hrs_WorkingPeriods table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  11-07-07
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
    '========================================================================
    'ProcedureName  :  LastRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get last record in hrs_WorkingPeriods table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  11-07-07
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
    '========================================================================
    'ProcedureName  :  NextRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get next record  from hrs_WorkingPeriods table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  11-07-07
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code >'" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code ASC"
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

    '========================================================================
    'ProcedureName  :  previousRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get previous record from hrs_WorkingPeriods table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  11-07-07
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code < '" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code DESC"
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

    Public Function UpdatePostDate(ByVal IntPeriodId As Integer, ByVal PostDate As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "Set Dateformat DMY Update hrs_WorkingPeriods Set PostDate ='" & PostDate & "' Where Id= " & IntPeriodId
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrSelectCommand
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region

#Region "Class Private Function"
    '========================================================================
    'ProcedureName  :  GetParameter
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  11-07-07
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Ds             :DataSet     :used its attributes to assign them to private attributes
    '========================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & Me.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mSearchArbName = mDataHandler.DataValue_Out(.Item("SearchArbName"), SqlDbType.VarChar)
                mEmployeeClassID = mDataHandler.DataValue_Out(.Item("EmployeeClassID"), SqlDbType.Int, True)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                'mFromDate = mDataHandler.DataValue_Out(.Item("FromDate"), SqlDbType.DateTime)
                'mToDate = mDataHandler.DataValue_Out(.Item("ToDate"), SqlDbType.DateTime)
                If clsCompanies.IsHigry Then
                    mFromDate = mDataHandler.DataValue_Out(ClsGHCalender.GetRelativeDate(.Item("FromDate"), Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Output), SqlDbType.DateTime)
                    mToDate = mDataHandler.DataValue_Out(ClsGHCalender.GetRelativeDate(.Item("ToDate"), Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Output), SqlDbType.DateTime)
                Else
                    mFromDate = mDataHandler.DataValue_Out(ClsGHCalender.GetRelativeDate(.Item("FromDate"), Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Output), SqlDbType.DateTime)
                    mToDate = mDataHandler.DataValue_Out(ClsGHCalender.GetRelativeDate(.Item("ToDate"), Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Output), SqlDbType.DateTime)
                End If
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                'mCompanyID = mDataHandler.DataValue_Out(.Item("CompanyID"), SqlDbType.Int,true )
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

    '========================================================================
    'ProcedureName  :  SetParameter
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Assign parameters of sqlcommand values with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  11-07-07
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & Me.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SearchArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mSearchArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeClassID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeClassID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FromDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mFromDate, SqlDbType.DateTime)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ToDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mToDate, SqlDbType.DateTime)
            If clsCompanies.IsHigry Then
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FromDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(ClsGHCalender.GetRelativeDate(mFromDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input), SqlDbType.DateTime)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ToDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(ClsGHCalender.GetRelativeDate(mToDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input), SqlDbType.DateTime)
            Else
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FromDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(ClsGHCalender.GetRelativeDate(mFromDate, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input), SqlDbType.DateTime)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ToDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(ClsGHCalender.GetRelativeDate(mToDate, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input), SqlDbType.DateTime)
            End If
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int, True)
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

Public Class Clshrs_WorkingPeriods
    Inherits Clshrs_WorkingPeriodsBase

    Public Sub New(ByVal page As Global.System.Web.UI.Page)
        MyBase.New(page)
    End Sub

    '========================================================================
    'ProcedureName  :  GetList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill DropDownList with English name column from hrs_WorkingPeriods table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  11-07-2007
    'Modifacations  : [0257] [11-07-2007]
    '                   1-remove Company ID Column from Select statment
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column from hrs_TimeIntervals table
    '========================================================================
    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String = String.Empty
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)

        Try

            'StrCommandString = Me.mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And CompanyId=" & Me.MainCompanyID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And CompanyId=" & Me.MainCompanyID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            StrCommandString = Me.mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' " & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And " & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")

                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

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

    '========================================================================
    'ProcedureName  :  GetDropDown
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill DropDownList with English name column from hrs_WorkingPeriods table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      : DataOcean 
    'Date Created   : 11-07-07 
    'Modifacations  : [0257] [11-07-2007]
    '                   1-remove Company ID Column from Select statment
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :DropDownList     :used to fill it with English name column from hrs_TimeIntervals table
    '========================================================================
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



End Class