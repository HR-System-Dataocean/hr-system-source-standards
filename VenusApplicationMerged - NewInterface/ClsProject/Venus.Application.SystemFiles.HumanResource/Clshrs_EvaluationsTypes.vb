'==========================================================================
'Program File Name : Clshrs_EvaluationMainTypes.vb 
'Project           : Venus V.
'Module            : Hrs (Human Resource Module)
'Developer      :  [MAE] 
'Date Created   :  [22-07-2007]
'Description    :    1-Implement Data Acess Layer evaluation types (hrs_EvaluationTypes table)
'                    2-Every type has code,English name,
'                       Arabic name,Search Arab Name,Employee Dependent(show that evaluation enterd in employee
'                       total evaluation),Remarks
'                    3-Allow search in Evaluation Types using critra
'                    4-Get list with all codes of Evaluation Types records
'                    5-Implement functions save(), update() and delete() to allow DML with some critera
'                    6-Implement functions first(),last(),next() and previous() to allow navigation between 
'                       records

'==========================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EvaluationTypesBases
    Inherits ClsDataAcessLayer

#Region "Class Constructors"
    '========================================================================
    'ProcedureName  :  Constractor 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Initialize table name,table fields,parametrs and select,delete and update statments
    'Developer      :  DataOcean
    'Date Created   :  [22-07-2007]
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EvaluationsTypes "
        mInsertParameter = " Code,EngName,ArbName,ArbName4S,EmployeeDependent,IsAbsolute,MeasureValue,Remarks,RegUserID,RegComputerID "
        mInsertParameterValues = " @Code,@EngName,@ArbName,@SearchArbName,@EmployeeDependent,@IsAbsolute,@MeasureValue,@Remarks,@RegUserID,@RegComputerID"
        mUpdateParameter = " Code=@Code,EngName=@EngName,ArbName=@ArbName,ArbName4S=@SearchArbName,EmployeeDependent=@EmployeeDependent,IsAbsolute=@IsAbsolute,MeasureValue=@MeasureValue,Remarks=@Remarks "
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"


        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"


    End Sub
#End Region

#Region "Private Members"
    Private mID As Object
    Private mCode As String
    Private mEngName As String
    Private mArbName As String
    Private mArbName4S As String
    Private mEmployeeDependent As Boolean
    Private mIsAbsolute As Boolean
    Private mMeasureValue As Double
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
            mArbName4S = mStringHandler.ReplaceHamza(Value)
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
    Public Property EmployeeDependent() As Boolean
        Get
            Return mEmployeeDependent
        End Get
        Set(ByVal Value As Boolean)
            mEmployeeDependent = Value
        End Set
    End Property
    Public Property IsAbsolute() As Boolean
        Get
            Return mIsAbsolute
        End Get
        Set(ByVal Value As Boolean)
            mIsAbsolute = Value
        End Set
    End Property
    Public Property MeasureValue() As Double
        Get
            Return mMeasureValue
        End Get
        Set(ByVal Value As Double)
            mMeasureValue = Value
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
    'Public Property CompanyId() As Integer
    '    Get
    '        Return mCompanyId
    '    End Get
    '    Set(ByVal value As Integer)
    '        mCompanyId = value
    '    End Set
    'End Property

#End Region

#Region "Public Function"
    '==========================================================================
    'ProcedureName  :  Find 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  1-Find all rows in hrs_EvaluationTypes that match criteria
    '                  2-return true if operation done
    'Developer      :  [MAE]Mah Abdel-aziz
    'Date Created   :  [22-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : (frmEvaluationTypes.aspx.vb) TlbMainNavigation_ButtonClicked() 
    '               : (frmEvaluationTypes.aspx.vb) SetToolbarSetting()
    '               : (frmEvaluationTypes.aspx.vb) Page_Load()
    '               : (frmEvaluationTypes.aspx.vb) TlbMainToolbar_ButtonClicked()
    '               : (frmEvaluationTypes.aspx.vb) btnRefCode_Click()
    '               : (frmEvaluationTypes.aspx.vb) SavePart()
    '               : (frmEvaluationTypes.aspx.vb) CheckMode()
    'To             : GetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as criteria to filter rows  ex:'ID=2'
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
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    '==========================================================================
    'ProcedureName  :  SaveUpdate 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  1-Used to update record if found(by checking its ID in hrs_EvaluationTypes table) 
    '                   otherwise insert it as new record
    '                  2-return true if operation done
    'Developer      :  [MAE]Mah Abdel-aziz
    'Date Created   :  [22-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : 
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as criteria to filter rows  ex:'ID=2'
    '========================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_EvaluationTypes Where " & Filter
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

    '==========================================================================
    'ProcedureName  :  Save 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  1-Used to insert new record in hrs_EvaluationTypes table
    '                  2-return true if operation done
    'Developer      :  [MAE]Mah Abdel-aziz
    'Date Created   :  [22-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : (frmEvaluationTypes.aspx.vb)SavePart()
    '               :
    'To             : SetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
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

    '==========================================================================
    'ProcedureName  :  Update 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  1-Used to update record if found(by checking its ID in hrs_EvaluationTypes) 
    '                   otherwise insert it as new record
    '                  2-return true if operation done
    'Developer      :  [MAE]Mah Abdel-aziz
    'Date Created   :  [22-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : (frmEvaluationTypes.aspx.vb)SavePart() 
    'To             : SetParameter()
    '               : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera to filter rows  ex:'ID=2'
    '========================================================================
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


    '==========================================================================
    'ProcedureName  :  Delete 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  1-Used to delete record that match criteria from hrs_EvaluationTypes table 
    '                  2-return true if operation done
    'Developer      :  [MAE]Mah Abdel-aziz
    'Date Created   :  [22-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : (frmEvaluationTypes.aspx.vb) TlbMainToolbar_ButtonClicked() 
    'To             : SetParameter()
    '               : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as criteria to filter rows  ex:'EmployeeID=2'
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

    '==========================================================================
    'ProcedureName  :  Clear 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Clear all object attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz
    'Date Created   :  [22-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : 
    'To             : 
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
            mArbName4S = String.Empty
            mIsAbsolute = False
            mMeasureValue = 0
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

#Region "Navigation Functions"

    '==========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Used to navigate between all record in hrs_EvaluationTypes table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz
    'Date Created   :  [22-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : (frmEvaluationTypes.aspx.vb) TlbMainNavigation_ButtonClicked()
    'To             : 
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

#End Region

#End Region

#Region "Class Private Function"
    '==========================================================================
    'ProcedureName  :  GetParameter 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz
    'Date Created   :  [22-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : 
    'To             : Find()
    '               : FirstRecord()
    '               : NextRecord()
    '               : PreviousRecord()
    '               : LastRecord()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Ds                 :DataSet    :used its attributes to assign them to object attributes
    '========================================================================
    Protected Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = mDataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mEmployeeDependent = mDataHandler.DataValue_Out(.Item("EmployeeDependent"), SqlDbType.Bit)
                mIsAbsolute = mDataHandler.DataValue_Out(.Item("IsAbsolute"), SqlDbType.Bit)
                mMeasureValue = mDataHandler.DataValue_Out(.Item("MeasureValue"), SqlDbType.Real)

                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int)
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

    '==========================================================================
    'ProcedureName  :  SetParameter 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Assign parameters of sqlcommand values with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz
    'Date Created   :  [22-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : 
    'To             : Save()
    '               : Update()
    '               : Delete()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand         :SqlCommand :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SearchArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeDependent", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mEmployeeDependent, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsAbsolute", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsAbsolute, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MeasureValue", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mMeasureValue, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int)
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

Public Class Clshrs_EvaluationsTypes
    Inherits Clshrs_EvaluationTypesBases

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
    End Sub

    '==========================================================================
    'ProcedureName  :  GetList 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill Datagrid Value List with English name column from hrs_EvaluationTypes table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz
    'Date Created   :  [22-07-2007]
    'Modifacations  : 
    'Modified by    : MAz
    'Date           : 05-12-2007
    'Description    : Update method to match with Language
    'Calls          :
    'From           : 
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column from hrs_EvaluationTypes table
    '========================================================================
    Public Function GetList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1"
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
    'Created by : DataOcean
    'Date : 09/07/2007
    'Input : DdlValues as DropDownList
    '       Condition to fill dropdownlist (if Wanted)
    '       NullNode to make attention to user by make first item ="Please Select"    
    'Description:  Fill DropDownList with Courses 
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

            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
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