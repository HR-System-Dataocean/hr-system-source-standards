Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeesDependantsBase
    Inherits ClsDataAcessLayer

#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EmployeesDependants "
        mInsertParameter = " EmployeeID,EngName,ArbName,ArbName4S,DependantTypeID,BirthDate,BirthCityID,Sex,NationalityId,InsuranceCovered,Remarks,RegUserID,RegComputerID,InsurancePercentage,TicketCovered,TicketPercentage,NationalIDORIqamano"
        mInsertParameterValues = " @EmployeeID,@EngName,@ArbName,@ArbName4S,@DependantTypeID,@BirthDate,@BirthCityID,@Sex,@NationalityId,@InsuranceCovered,@Remarks,@RegUserID,@RegComputerID,@InsurancePercentage,@TicketCovered,@TicketPercentage,@NationalIDORIqamano"
        mUpdateParameter = " EmployeeId= @EmployeeId , EngName=@EngName,ArbName=@ArbName,ArbName4S=@ArbName4S,DependantTypeID=@DependantTypeID,BirthDate=@BirthDate,BirthCityID=@BirthCityID,Sex=@Sex,NationalityId=@NationalityId,InsuranceCovered=@InsuranceCovered,Remarks=@Remarks,RegUserID=@RegUserID,RegComputerID=@RegComputerID,InsurancePercentage=@InsurancePercentage,TicketCovered=@TicketCovered,TicketPercentage=@TicketPercentage,NationalIDORIqamano=@NationalIDORIqamano"
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ") ;Select @@IDENTITY"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"

    End Sub
#End Region

#Region "Private Members"
    Private mID As Object
    Private mEmployeeID As Object
    Private mDependentID As Object
    Private mEngName As String
    Private mArbName As String
    Private mArbName4S As String
    Private mDependantTypeID As Object
    Private mBirthDate As Object
    Private mBirthCityID As Object
    Private mSex As Object
    Private mNationalityId As Object
    Private mInsuranceCovered As Object

    Private mInsurancePercentage As Object
    Private mTicketCovered As Object
    Private mTicketPercentage As Object
    Private mNationalIDORIqamano As Object

    Private mPicture As Global.System.Web.HttpPostedFile
    Private mPysicalPath As String
    Private mPicturePath As String
    Private mImageName As String

    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Private mCompanyId As Integer

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
    Public Property EmployeeID() As Object
        Get
            Return mEmployeeID
        End Get
        Set(ByVal Value As Object)
            mEmployeeID = Value
        End Set
    End Property
    Public Property DependentID() As Object
        Get
            Return mDependentID
        End Get
        Set(ByVal Value As Object)
            mDependentID = Value
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
    Public Property DependantTypeID() As Object
        Get
            Return mDependantTypeID
        End Get
        Set(ByVal Value As Object)
            mDependantTypeID = Value
        End Set
    End Property
    Public Property BirthDate() As Object
        Get
            Return mBirthDate
        End Get
        Set(ByVal Value As Object)
            mBirthDate = Value
        End Set
    End Property
    Public Property BirthCityID() As Object
        Get
            Return mBirthCityID
        End Get
        Set(ByVal Value As Object)
            mBirthCityID = Value
        End Set
    End Property
    Public Property Sex() As Object
        Get
            Return mSex
        End Get
        Set(ByVal Value As Object)
            mSex = Value
        End Set
    End Property
    Public Property NationalityId() As Object
        Get
            Return mNationalityId
        End Get
        Set(ByVal Value As Object)
            mNationalityId = Value
        End Set
    End Property
    Public Property InsuranceCovered() As Object
        Get
            Return mInsuranceCovered
        End Get
        Set(ByVal Value As Object)
            mInsuranceCovered = Value
        End Set
    End Property
    Public Property InsurancePercentage() As Object
        Get
            Return mInsurancePercentage
        End Get
        Set(ByVal Value As Object)
            mInsurancePercentage = Value
        End Set
    End Property
    Public Property TicketCovered() As Object
        Get
            Return mTicketCovered
        End Get
        Set(ByVal Value As Object)
            mTicketCovered = Value
        End Set
    End Property
    Public Property TicketPercentage() As Object
        Get
            Return mTicketPercentage
        End Get
        Set(ByVal Value As Object)
            mTicketPercentage = Value
        End Set
    End Property
    Public Property NationalIDORIqamano() As Object
        Get
            Return mNationalIDORIqamano
        End Get
        Set(ByVal Value As Object)
            mNationalIDORIqamano = Value
        End Set
    End Property
    Public Property Picture() As Global.System.Web.HttpPostedFile
        Get
            Return mPicture
        End Get
        Set(ByVal value As Global.System.Web.HttpPostedFile)
            mPicture = value
        End Set
    End Property
    Public Property PysicalPath() As String
        Get
            Return mPysicalPath
        End Get
        Set(ByVal value As String)
            mPysicalPath = value
        End Set
    End Property
    Public Property PicturePath() As String
        Get
            Return mPicturePath
        End Get
        Set(ByVal value As String)
            mPicturePath = value
        End Set
    End Property
    Public Property ImageName() As String
        Get
            Return mImageName
        End Get
        Set(ByVal value As String)
            mImageName = value
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
    Public Property CompanyId() As Integer
        Get
            Return mCompanyId
        End Get
        Set(ByVal value As Integer)
            mCompanyId = value
        End Set
    End Property

#End Region

#Region "Public Function"

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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function Find(ByVal Filter As String, ByVal NonCanceledOnly As Boolean) As Boolean
        Dim StrSelectCommand As String
        Try
            If NonCanceledOnly Then
                StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            Else
                StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where   IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "   And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    Public Function FindWithImage(ByVal Filter As String, ByVal NonCanceledOnly As Boolean) As Boolean
        Dim StrSelectCommand As String
        Try
            If NonCanceledOnly Then
                StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            Else
                StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where   IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "   And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_EmployeesDependants Where " & Filter
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
        Dim intID As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameterAdd(mSqlCommand)
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

    '-------------------------------0257 MODIFIED-----------------------------------------
    Public Function SaveDependant() As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameterAdd(mSqlCommand)
            mSqlCommand.Connection.Open()
            Return mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '-------------------------------=============-----------------------------------------

    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameterEdit(mSqlCommand)
            'mSqlCommand.Connection.Open()
            'mSqlCommand.ExecuteNonQuery()
            'mSqlCommand.Connection.Close()
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
            StrDeleteCommand = CONFIG_DATEFORMAT & " Delete from hrs_EmployeesDependants " & IIf(Len(Filter) > 0, " Where " & Filter, "")
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
    Public Function Clear() As Boolean
        Try
            mID = 0
            mEmployeeID = 0
            mDependentID = 0
            mEngName = String.Empty
            mArbName = String.Empty
            mArbName4S = String.Empty
            mDependantTypeID = 0
            mBirthDate = Nothing
            mBirthCityID = 0
            mSex = String.Empty
            mNationalityId = 0
            mInsuranceCovered = 0
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
    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String
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


#Region "Navigation Functions"
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :[0257]   
    'Date Created   :09-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmGradesSteps.aspx.vb)TlbMainNavigation_ButtonClicked()
    'To             :GetParameter()
    '               :Clear()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function FirstRecord(ByVal Filter) As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"

            If (Filter = "") Then
                StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            Else
                StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where " & Filter & " AND IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC "
            End If
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
    Public Function LastRecord(ByVal Filter) As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            If (Filter = "") Then
                StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            Else
                StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where " & Filter & " AND IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            End If

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
    Public Function NextRecord(ByVal Filter) As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            If (Filter = "") Then
                StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            Else
                StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE " & Filter & " AND ID >" & mID & " And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            End If

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
    Public Function previousRecord(ByVal Filter) As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"

            If (Filter = "") Then
                StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            Else
                StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE " & Filter & " AND ID < " & mID & " And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            End If


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

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mEmployeeID = mDataHandler.DataValue_Out(.Item("EmployeeID"), SqlDbType.Int, True)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = mDataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mDependantTypeID = mDataHandler.DataValue_Out(.Item("DependantTypeID"), SqlDbType.Int, True)
                mBirthDate = mDataHandler.DataValue_Out(.Item("BirthDate"), SqlDbType.DateTime)
                mBirthCityID = mDataHandler.DataValue_Out(.Item("BirthCityID"), SqlDbType.Int, True)
                mSex = mDataHandler.DataValue_Out(.Item("Sex"), SqlDbType.Char)
                mNationalityId = mDataHandler.DataValue_Out(.Item("NationalityID"), SqlDbType.Int, True)
                mInsuranceCovered = mDataHandler.DataValue_Out(.Item("InsuranceCovered"), SqlDbType.Int)
                mInsurancePercentage = mDataHandler.DataValue_Out(.Item("InsurancePercentage"), SqlDbType.Real)
                mTicketCovered = mDataHandler.DataValue_Out(.Item("TicketCovered"), SqlDbType.Int)
                mTicketPercentage = mDataHandler.DataValue_Out(.Item("TicketPercentage"), SqlDbType.Real)
                mNationalIDORIqamano = mDataHandler.DataValue_Out(.Item("NationalIDORIqamano"), SqlDbType.VarChar)
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
    Private Function SetParameterAdd(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Dim StrFilePath As String = String.Empty
        Dim ObjHandler As New Venus.Shared.Web.WebHandler
        Dim StrErrorMsg As String = String.Empty
        'Dim ClsEmployees As New Clshrs_Employees(mPage)
        Try
            'ClsEmployees.Find("ID=" & mEmployeeID)
            'StrFilePath = "~\" & ClsDataAcessLayer.CONFIG_DEFULTIMAGEFOLDER & "\" & ClsEmployees.Code & "\" & mEngName & "\" & ImageName

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DependantTypeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mDependantTypeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BirthDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mBirthDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BirthCityID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBirthCityID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Sex", SqlDbType.Char)).Value = mDataHandler.DataValue_In(mSex, SqlDbType.Char)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NationalityId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mNationalityId, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InsuranceCovered", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mInsuranceCovered, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InsurancePercentage", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mInsurancePercentage, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TicketCovered", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTicketCovered, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TicketPercentage", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mTicketPercentage, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NationalIDORIqamano", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mNationalIDORIqamano, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function SetParameterEdit(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & Me.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)

        Dim StrFilePath As String = String.Empty
        Dim ObjHandler As New Venus.Shared.Web.WebHandler
        Dim StrErrorMsg As String = String.Empty
        'Dim ClsEmployees As New Clshrs_Employees(mPage)
        Try
            'ClsEmployees.Find("ID=" & mEmployeeID)
            'StrFilePath = "~\" & ClsDataAcessLayer.CONFIG_DEFULTIMAGEFOLDER & "\" & ClsEmployees.Code & "\" & mEngName & "\" & ImageName

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DependantTypeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mDependantTypeID, SqlDbType.Int, True)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BirthDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mBirthDate, SqlDbType.DateTime)
            If clsCompanies.IsHigry Then
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BirthDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(ClsGHCalender.GetRelativeDate(mBirthDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input), SqlDbType.DateTime)
            Else
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BirthDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(ClsGHCalender.GetRelativeDate(mBirthDate, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input), SqlDbType.DateTime)
            End If
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BirthCityID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBirthCityID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Sex", SqlDbType.Char)).Value = mDataHandler.DataValue_In(mSex, SqlDbType.Char)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NationalityId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mNationalityId, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InsuranceCovered", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mInsuranceCovered, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InsurancePercentage", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mInsurancePercentage, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TicketCovered", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTicketCovered, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TicketPercentage", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mTicketPercentage, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NationalIDORIqamano", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mNationalIDORIqamano, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
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

Public Class Clshrs_EmployeesDependants
    Inherits Clshrs_EmployeesDependantsBase

    Public Sub New(ByVal page As Global.System.Web.UI.Page)
        MyBase.New(page)
    End Sub
    'Created By     : DataOcean
    'Modified by    : [0258]
    'Date           : 05-12-2007
    'Description    : Update method to match with Language
    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String = String.Empty
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = Me.mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')=''  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')=''  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    'Created By     : DataOcean
    'Modified by    : [0258]
    'Date           : 05-12-2007
    'Description    : Update method to match with Language
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String = String.Empty
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = Me.mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')=''  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
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


