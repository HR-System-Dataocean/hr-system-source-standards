'==========================================================================
'Program File Name : Clshrs_EvaluationSTransactionDetail.vb
'Project           : Venus V.
'Module            : Hrs (Human Resource Module)
'Developer         : [0256]    
'                  : [0257]
'Date Created      : 26-07-2007
'Description       : 
'==========================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EvaluationsTransactionDetail
    Inherits ClsDataAcessLayer

#Region "Class Constructors"
    '========================================================================
    'ProcedureName  :  Constractor 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  
    'Developer      :  [MAE] 
    'Date Created   :  [26-07-2007]
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EvaluationSTransactionDetail "
        mInsertParameter = " EvaluationTransactionHeadID,EvaluationSubElementID,Degree,Remarks,RegUserID,RegComputerID "
        mInsertParameterValues = " @EvaluationTransactionHeadID,@EvaluationSubElementID,@Degree,@Remarks,@RegUserID,@RegComputerID"
        mUpdateParameter = "EvaluationTransactionHeadID=@EvaluationTransactionHeadID,EvaluationSubElementID=@EvaluationSubElementID,Degree=@Degree,Remarks=@Remarks,RegUserID=@RegUserID,RegComputerID=@RegComputerID"

        'mSelectCommand = " Select EvaluationTransactionHeadID,EvaluationSubElementID,Degree,Remarks,RegUserID,RegComputerID,RegDate,CancelDate From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"

        'mSelectCommand = " Select hrs_EvaluationSTransactionDetail.ID ,hrs_EvaluationSTransactionDetail.EvaluationTransactionHeadID,hrs_EvaluationSTransactionDetail.EvaluationSubElementID,hrs_EvaluationSTransactionDetail.Degree,hrs_EvaluationSTransactionDetail.Remarks,hrs_EvaluationSTransactionDetail.RegUserID,hrs_EvaluationSTransactionDetail.RegComputerID,hrs_EvaluationSTransactionDetail.RegDate,hrs_EvaluationSTransactionDetail.CancelDate ,hrs_Evaluationssubelements.EvaluationMainElementID ,hrs_Evaluationssubelements.Code AS ESEcode ,hrs_Evaluationssubelements.ID as SubElementID , hrs_Evaluationssubelements.EngName as ESEengName , hrs_Evaluationssubelements.MeasureValue as ESEMeasureValue From hrs_Evaluationssubelements  Left Join hrs_EvaluationSTransactionDetail on hrs_EvaluationSTransactionDetail.EvaluationTransactionHeadID = hrs_Evaluationssubelements.id inner join hrs_EvaluationMainElements on  hrs_Evaluationssubelements.EvaluationMainElementID = hrs_EvaluationMainElements.id  "
        mSelectCommand = " Select hrs_EvaluationSTransactionDetail.ID ,hrs_EvaluationSTransactionDetail.EvaluationTransactionHeadID,hrs_EvaluationSTransactionDetail.EvaluationSubElementID,hrs_EvaluationSTransactionDetail.Degree,hrs_EvaluationSTransactionDetail.Remarks,hrs_EvaluationSTransactionDetail.RegUserID,hrs_EvaluationSTransactionDetail.RegComputerID,hrs_EvaluationSTransactionDetail.RegDate,hrs_EvaluationSTransactionDetail.CancelDate ,hrs_Evaluationssubelements.EvaluationMainElementID ,hrs_Evaluationssubelements.Code AS ESEcode ,hrs_Evaluationssubelements.ID as SubElementID , hrs_Evaluationssubelements.EngName as ESEengName , hrs_Evaluationssubelements.MeasureValue as ESEMeasureValue From (hrs_Evaluationssubelements  Left Join hrs_EvaluationSTransactionDetail on hrs_Evaluationssubelements.id=hrs_EvaluationSTransactionDetail.EvaluationSubElementID) Full join hrs_EvaluationsTransactionHead ON hrs_EvaluationSTransactionDetail.EvaluationTransactionHeadID= hrs_EvaluationsTransactionHead.ID "
    End Sub
#End Region

#Region "Private Members"
    Private mID As Object
    Private mEvaluationSubElementID As Int32
    Private mEvaluationTransactionHeadID As Int32
    Private mDegree As Double
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object

    '[0256] 240707 
    Private mESEMeasureValue As Object
    Private mESECode As Object
    Private mEvaluationMainElementID As Object
    Private mEngName As Object
#End Region

#Region "Public property"
    Public Property ESEMeasureValue()
        Get
            Return mESEMeasureValue
        End Get
        Set(ByVal value)
            mESEMeasureValue = value
        End Set
    End Property

    Public Property ESECode()
        Get
            Return mESECode
        End Get
        Set(ByVal value)
            mESECode = value
        End Set
    End Property
    Public Property EvaluationMainElementID()
        Get
            Return mEvaluationMainElementID
        End Get
        Set(ByVal value)
            mEvaluationMainElementID = value
        End Set
    End Property
    Public Property EngName()
        Get
            Return mEngName
        End Get
        Set(ByVal value)
            mEngName = value
        End Set
    End Property
    Public Property ID() As Object
        Get
            Return Mid()
        End Get
        Set(ByVal Value As Object)
            Mid = Value
        End Set
    End Property
    Public Property EvaluationTransactionHeadID() As Int32
        Get
            Return mEvaluationTransactionHeadID
        End Get
        Set(ByVal Value As Int32)
            mEvaluationTransactionHeadID = Value
        End Set
    End Property
    Public Property EvaluationSubElementID() As Int32
        Get
            Return mEvaluationSubElementID
        End Get
        Set(ByVal Value As Int32)
            mEvaluationSubElementID = Value
        End Set
    End Property
    Public Property Degree() As Double
        Get
            Return mDegree
        End Get
        Set(ByVal Value As Double)
            mDegree = Value
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
    '========================================================================
    'ProcedureName  :  Find 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  
    'Developer      :  [MAE] 
    'Date Created   :  [26-07-2007]
    'Modifications  : 
    'Calls          :
    'From           : (frmEmployeeEvaluation.aspx.vb) fnFillGrid() 
    'To             : GetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as criteria to filter rows  ex:'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String, Optional ByVal newStatus As Boolean = False) As Boolean
        Dim StrSelectCommand As String
        Dim StrSelect As String = " Select hrs_EvaluationSTransactionDetail.ID ,hrs_EvaluationSTransactionDetail.EvaluationTransactionHeadID,hrs_EvaluationSTransactionDetail.EvaluationSubElementID,hrs_EvaluationSTransactionDetail.Remarks,hrs_EvaluationSTransactionDetail.RegUserID,hrs_EvaluationSTransactionDetail.RegComputerID,hrs_EvaluationSTransactionDetail.RegDate,hrs_EvaluationSTransactionDetail.CancelDate ,hrs_Evaluationssubelements.EvaluationMainElementID ,hrs_Evaluationssubelements.Code AS ESEcode ,hrs_Evaluationssubelements.ID as SubElementID , hrs_Evaluationssubelements.EngName as ESEengName , hrs_Evaluationssubelements.MeasureValue as ESEMeasureValue From hrs_Evaluationssubelements  Left Join hrs_EvaluationSTransactionDetail on hrs_Evaluationssubelements.id = hrs_EvaluationSTransactionDetail.EvaluationSubElementID "
        Try
            'StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            If newStatus Then
                StrSelectCommand = StrSelect & IIf(Len(Filter) > 0, " Where IsNull(hrs_EvaluationSTransactionDetail.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EvaluationSTransactionDetail.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(hrs_EvaluationSTransactionDetail.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EvaluationSTransactionDetail.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            Else
                StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(hrs_EvaluationSTransactionDetail.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EvaluationSTransactionDetail.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(hrs_EvaluationSTransactionDetail.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EvaluationSTransactionDetail.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            End If

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
    'ProcedureName  :  Save
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  
    'Developer      :  [MAE] 
    'Date Created   :  [26-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : (frmEmployeeEvaluation.aspx.vb)fnPrepareToSave()
    'To             : SetParameter()
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
    'Description    :  
    'Developer      :  [MAE] 
    'Date Created   :  [26-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : (frmEmployeeEvaluation.aspx.vb)fnPrepareToSave()
    'To             : SetParameter()
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
    'Description    :                   
    'Developer      :  [MAE] 
    'Date Created   :  [26-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : (frmEmployeeEvaluation.aspx.vb)fnDelete() 
    'To             : SetParameter()
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
    'Developer      :  [MAE] 
    'Date Created   :  [26-07-2007]
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function Clear() As Boolean
        Try
            Mid = 0
            mEvaluationTransactionHeadID = Nothing
            mEvaluationSubElementID = Nothing
            mDegree = Nothing
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
    'Description    :  Used to navigate between all record hrs_EvaluationSTransactionDetail table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE] 
    'Date Created   :  [26-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : 
    'To             : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================

    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String
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


    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String
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


    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID() & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID() & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
    '========================================================================
    'ProcedureName  :  GetParameter
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :   Assign Result of Dataset to private attributes
    'Developer      :  [MAE] 
    'Date Created   :  [26-07-2007]
    'Modifacations  : 
    'From           :Find()
    '               :FirstRecord()
    '               :LastRecord()
    '               :NextRecord()
    '               :PreviousRecord()
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Ds             :DataSet     :used its attributes to assign them to private attributes
    '========================================================================
    Protected Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                Mid = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                mEvaluationTransactionHeadID = mDataHandler.DataValue_Out(.Item("EvaluationTransactionHeadID"), SqlDbType.Int)
                mEvaluationSubElementID = mDataHandler.DataValue_Out(.Item("EvaluationSubElementID"), SqlDbType.Int)
                mDegree = mDataHandler.DataValue_Out(.Item("Degree"), SqlDbType.Real)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                '[0256] 240707 
                mEngName = mDataHandler.DataValue_Out(.Item("ESEEngName"), SqlDbType.VarChar)
                mESEMeasureValue = mDataHandler.DataValue_Out(.Item("ESEMeasureValue"), SqlDbType.Real)
                mESECode = mDataHandler.DataValue_Out(.Item("ESEcode"), SqlDbType.VarChar)
                mEvaluationMainElementID = mDataHandler.DataValue_Out(.Item("EvaluationMainElementID"), SqlDbType.Int)
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
    'Developer      :  [MAE] 
    'Date Created   :  [26-07-2007]
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
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EvaluationTransactionHeadID", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEvaluationTransactionHeadID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EvaluationSubElementID", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEvaluationSubElementID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Degree", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mDegree, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int)

            '[0256]
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(Me.mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ESEMeasureValue", SqlDbType.Real)).Value = mDataHandler.DataValue_In(Me.mEngName, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ESEcode", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(Me.mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EvaluationMainElementID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mEngName, SqlDbType.Int)

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
