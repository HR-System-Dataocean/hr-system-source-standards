

'=========================================================================
'Created by : DataOcean
'Date : 10/07/2007
'                   Class: Companies
'                   Table: sys_Companies
'                   Relations:
'                               sys_Companies.ID ->> sys_Branches.CompanyID
'                               sys_Companies.ID ->> sys_Departments.CompanyID
'                               sys_Companies.ID ->> hrs_Projects.CompanyID
'=========================================================================
Imports Venus
Public Class Clssys_Companies
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " sys_Companies "
        mInsertParameter = " Code,EngName,ArbName,ArbName4S,IsHigry,VacationFromPrepareDay,IncludeAbsencDays,PrepareDay,Remarks,HasSequence,SequenceLength,RegUserID,RegComputerID,EmpFirstName,EmpSecondName,EmpThirdName,EmpFourthName,EmpNameSeparator,DefaultAttend,SalaryCalculation,Prefix,Separator,CountEmployeeVacationDaysTotal,ZeroBalAfterVac,VacSettlement,AllowOverVacation,ExecuseRequestHoursallowed,EmployeeDocumentsAutoSerial,UserDepartmentsPermissions "
        mInsertParameterValues = " @Code,@EngName,@ArbName,@ArbName4S,@IsHigry,@VacationFromPrepareDay,@IncludeAbsencDays,@PrepareDay,@Remarks,@HasSequence,@SequenceLength,@RegUserID,@RegComputerID ,@EmpFirstName,@EmpSecondName,@EmpThirdName,@EmpFourthName,@EmpNameSeparator,@DefaultAttend,@SalaryCalculation,@Prefix,@Separator,@CountEmployeeVacationDaysTotal,@ZeroBalAfterVac,@VacSettlement,@AllowOverVacation,@ExecuseRequestHoursallowed,@EmployeeDocumentsAutoSerial,@UserDepartmentsPermissions"
        mUpdateParameter = " Code=@Code,EngName=@EngName,ArbName=@ArbName,ArbName4S=@ArbName4S,IsHigry=@IsHigry,VacationFromPrepareDay=@VacationFromPrepareDay,IncludeAbsencDays=@IncludeAbsencDays,PrepareDay=@PrepareDay,Remarks=@Remarks ,HasSequence=@HasSequence,SequenceLength=@SequenceLength,EmpFirstName=@EmpFirstName,EmpSecondName=@EmpSecondName,EmpThirdName=@EmpThirdName,EmpFourthName=@EmpFourthName,EmpNameSeparator=@EmpNameSeparator,DefaultAttend=@DefaultAttend,SalaryCalculation=@SalaryCalculation,Prefix = @Prefix,Separator = @Separator,CountEmployeeVacationDaysTotal=@CountEmployeeVacationDaysTotal,ZeroBalAfterVac=@ZeroBalAfterVac,VacSettlement=@VacSettlement,AllowOverVacation=@AllowOverVacation,ExecuseRequestHoursallowed=@ExecuseRequestHoursallowed,EmployeeDocumentsAutoSerial=@EmployeeDocumentsAutoSerial,UserDepartmentsPermissions=@UserDepartmentsPermissions"
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

#End Region

#Region "Private Members"
    Private mID As Object
    Private mCode As String
    Private mEngName As String
    Private mArbName As String
    Private mArbName4S As String
    Private mIsHigry As Boolean
    Private mVacationFromPrepareDay As Boolean
    Private mIncludeAbsencDays As Boolean
    Private mPrepareDay As Integer
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object

    Private mHasSequence As Boolean
    Private mSequenceLength As Integer

    Private mSalaryCalculation As Integer

    Private mRegDate As Object
    Private mCancelDate As Object

    Private mEmpFirstName As String
    Private mEmpSecondName As String
    Private mEmpThirdName As String
    Private mEmpFourthName As String
    Private mEmpNameSeparator As Char

    Private mDefaultAttend As Boolean
    Private mPrefix As Integer
    Private mSeparator As String
    Private mCountEmployeeVacationDaysTotal As Boolean
    Private mZeroBalAfterVac As Boolean
    Private mVacSettlement As Boolean

    Private mAllowOverVacation As Boolean
    Private mExecuseRequestHoursallowed As Integer
    Private mEmployeeDocumentsAutoSerial As Boolean
    Private mUserDepartmentsPermissions As Boolean

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

    Public Property IncludeAbsencDays() As Boolean
        Get
            Return mIncludeAbsencDays
        End Get
        Set(ByVal value As Boolean)
            mIncludeAbsencDays = value
        End Set
    End Property

    Public Property IsHigry() As Boolean
        Get
            Return mIsHigry
        End Get
        Set(ByVal Value As Boolean)
            mIsHigry = Value
        End Set
    End Property
    Public Property VacationFromPrepareDay() As Boolean
        Get
            Return mVacationFromPrepareDay
        End Get
        Set(ByVal Value As Boolean)
            mVacationFromPrepareDay = Value
        End Set
    End Property
    Public Property SalaryCalculation() As Integer
        Get
            Return mSalaryCalculation
        End Get
        Set(ByVal Value As Integer)
            mSalaryCalculation = Value
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

    Public Property EmpFirstName() As String
        Get
            Return mEmpFirstName
        End Get
        Set(ByVal value As String)
            mEmpFirstName = value
        End Set
    End Property

    Public Property PrepareDay() As Integer
        Get
            Return mPrepareDay
        End Get
        Set(ByVal value As Integer)
            mPrepareDay = value
        End Set
    End Property

    Public Property EmpSecondName() As String
        Get
            Return mEmpSecondName
        End Get
        Set(ByVal value As String)
            mEmpSecondName = value
        End Set
    End Property

    Public Property EmpThirdName() As String
        Get
            Return mEmpThirdName
        End Get
        Set(ByVal value As String)
            mEmpThirdName = value
        End Set
    End Property

    Public Property EmpFourthName() As String
        Get
            Return mEmpFourthName
        End Get
        Set(ByVal value As String)
            mEmpFourthName = value
        End Set
    End Property

    Public Property EmpNameSeparator() As Char
        Get
            Return mEmpNameSeparator
        End Get
        Set(ByVal value As Char)
            mEmpNameSeparator = value
        End Set
    End Property

    Public Property HasSequence() As Boolean
        Get
            Return mHasSequence
        End Get
        Set(ByVal value As Boolean)
            mHasSequence = value

        End Set
    End Property
    Public Property SequenceLength() As Integer
        Get
            Return mSequenceLength
        End Get
        Set(ByVal value As Integer)
            mSequenceLength = value
        End Set
    End Property
    Public Property DefaultAttend() As Boolean
        Get
            Return mDefaultAttend
        End Get
        Set(ByVal value As Boolean)
            mDefaultAttend = value
        End Set
    End Property
    Public Property Prefix() As Integer
        Get
            Return mPrefix
        End Get
        Set(ByVal value As Integer)
            mPrefix = value
        End Set
    End Property
    Public Property Separator() As String
        Get
            Return mSeparator
        End Get
        Set(ByVal value As String)
            mSeparator = value
        End Set
    End Property
    Public Property CountEmployeeVacationDaysTotal() As Boolean
        Get
            Return mCountEmployeeVacationDaysTotal
        End Get
        Set(ByVal value As Boolean)
            mCountEmployeeVacationDaysTotal = value
        End Set
    End Property
    Public Property ZeroBalAfterVac() As Boolean
        Get
            Return mZeroBalAfterVac
        End Get
        Set(value As Boolean)
            mZeroBalAfterVac = value
        End Set
    End Property
    Public Property VacSettlement() As Boolean
        Get
            Return mVacSettlement
        End Get
        Set(value As Boolean)
            mVacSettlement = value
        End Set
    End Property

    Public Property AllowOverVacation() As Boolean
        Get
            Return mAllowOverVacation
        End Get
        Set(ByVal value As Boolean)
            mAllowOverVacation = value
        End Set
    End Property

    Public Property ExecuseRequestHoursallowed() As Integer
        Get
            Return mExecuseRequestHoursallowed
        End Get
        Set(ByVal value As Integer)
            mExecuseRequestHoursallowed = value
        End Set
    End Property
    Public Property EmployeeDocumentsAutoSerial() As Boolean
        Get
            Return mEmployeeDocumentsAutoSerial
        End Get
        Set(ByVal value As Boolean)
            mEmployeeDocumentsAutoSerial = value
        End Set
    End Property

    Public Property UserDepartmentsPermissions() As Boolean
        Get
            Return mUserDepartmentsPermissions
        End Get
        Set(ByVal value As Boolean)
            mUserDepartmentsPermissions = value
        End Set
    End Property

#End Region

#Region "Public Function"

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            '==================== Order By Modification [Start]
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By Code "
            End If
            'Filter = Filter.Replace("Õ", " AM ").Replace("ã", " PM ")
            '==================== Order By Modification [End]
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            '==================== Order By Modification [Start]
            StrSelectCommand = StrSelectCommand & orderByStr
            '==================== Order By Modification [End]
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
            strSQL = "Select ID From sys_Companies Where " & Filter
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
            mCode = String.Empty
            mEngName = String.Empty
            mArbName = String.Empty
            mArbName4S = String.Empty
            mIsHigry = False
            mVacationFromPrepareDay = False
            mIncludeAbsencDays = False
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mHasSequence = False
            mSequenceLength = 0
            mDefaultAttend = False
            mSeparator = String.Empty
            mPrefix = 0
            mCountEmployeeVacationDaysTotal = 0
            mAllowOverVacation = False
            mExecuseRequestHoursallowed = 0
            mEmployeeDocumentsAutoSerial = False
            mUserDepartmentsPermissions = False
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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

    Public Function GetEX() As Boolean
        Dim ObjEncode As New Venus.Shared.Security.ClientCrypt
        Dim dtEX As DateTime
        Dim strEXEnc As String
        Dim strExDate As Date
        Dim strCmd As String
        Dim strDtEx As String
        If mConnectionString.IndexOf("venusDB") <> -1 Then
            strCmd = " Select Top(1) EXENC from " & mTable
            strDtEx = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strCmd)
           
            If strDtEx.Trim = "" Then
                strCmd = "Set Date format dmy ;" & _
                        " Update sys_Companies set EXENC = '" & ObjEncode.Encrypt(Format(Date.Now, "dd/MM/yyyy"), "DataOcean") & "' where ID = (select top(1) ID from sys_Companies );"

                dtEX = Date.Now
            Else
                dtEX = ObjEncode.Decrypt(strDtEx, "DataOcean")

            End If
            strEXEnc = Global.System.Configuration.ConfigurationManager.AppSettings("EX")
            strExDate = CDate(ObjEncode.Decrypt(strEXEnc, "DataOcean"))
            If DateDiff(DateInterval.Day, dtEX, strExDate) < 0 Then
                Return False
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function

#End Region

#Region "Class Private Function"

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = mDataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mIsHigry = mDataHandler.DataValue_Out(.Item("IsHigry"), SqlDbType.Bit)
                mVacationFromPrepareDay = mDataHandler.DataValue_Out(.Item("VacationFromPrepareDay"), SqlDbType.Bit)
                mIncludeAbsencDays = mDataHandler.DataValue_Out(.Item("IncludeAbsencDays"), SqlDbType.Bit)
                mPrepareDay = mDataHandler.DataValue_Out(.Item("PrepareDay"), SqlDbType.Int)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mEmpFirstName = mDataHandler.DataValue_Out(.Item("EmpFirstName"), SqlDbType.VarChar)
                mEmpSecondName = mDataHandler.DataValue_Out(.Item("EmpSecondName"), SqlDbType.VarChar)
                mEmpThirdName = mDataHandler.DataValue_Out(.Item("EmpThirdName"), SqlDbType.VarChar)
                mEmpFourthName = mDataHandler.DataValue_Out(.Item("EmpFourthName"), SqlDbType.VarChar)
                mEmpNameSeparator = mDataHandler.DataValue_Out(.Item("EmpNameSeparator"), SqlDbType.Char)
                mHasSequence = mDataHandler.DataValue_Out(.Item("HasSequence"), SqlDbType.Bit)
                mSequenceLength = mDataHandler.DataValue_Out(.Item("SequenceLength"), SqlDbType.Int, True)
                mDefaultAttend = mDataHandler.DataValue_Out(.Item("DefaultAttend"), SqlDbType.Bit)
                mSalaryCalculation = mDataHandler.DataValue_Out(.Item("SalaryCalculation"), SqlDbType.Int)
                mPrefix = mDataHandler.DataValue_Out(.Item("Prefix"), SqlDbType.Int)
                mSeparator = mDataHandler.DataValue_Out(.Item("Separator"), SqlDbType.VarChar)
                mCountEmployeeVacationDaysTotal = mDataHandler.DataValue_Out(.Item("CountEmployeeVacationDaysTotal"), SqlDbType.Bit)
                mZeroBalAfterVac = mDataHandler.DataValue_Out(.Item("ZeroBalAfterVac"), SqlDbType.Bit)
                mVacSettlement = mDataHandler.DataValue_Out(.Item("VacSettlement"), SqlDbType.Bit)
                mAllowOverVacation = mDataHandler.DataValue_Out(.Item("AllowOverVacation"), SqlDbType.Bit)
                mExecuseRequestHoursallowed = mDataHandler.DataValue_Out(.Item("ExecuseRequestHoursallowed"), SqlDbType.Int)
                mEmployeeDocumentsAutoSerial = mDataHandler.DataValue_Out(.Item("EmployeeDocumentsAutoSerial"), SqlDbType.Bit)
                mUserDepartmentsPermissions = mDataHandler.DataValue_Out(.Item("UserDepartmentsPermissions"), SqlDbType.Bit)

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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsHigry", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsHigry, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@VacationFromPrepareDay", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mVacationFromPrepareDay, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IncludeAbsencDays", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIncludeAbsencDays, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PrepareDay", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mPrepareDay, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int, True)


            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmpFirstName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEmpFirstName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmpSecondName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEmpSecondName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmpThirdName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEmpThirdName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmpFourthName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEmpFourthName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmpNameSeparator", SqlDbType.Char)).Value = mDataHandler.DataValue_In(mEmpNameSeparator, SqlDbType.Char)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HasSequence", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHasSequence, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SequenceLength", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mSequenceLength, SqlDbType.Int, True)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DefaultAttend", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mDefaultAttend, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SalaryCalculation", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mSalaryCalculation, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Prefix", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mPrefix, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Separator", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mSeparator, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CountEmployeeVacationDaysTotal", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCountEmployeeVacationDaysTotal, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ZeroBalAfterVac", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mZeroBalAfterVac, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@VacSettlement", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mVacSettlement, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AllowOverVacation", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mAllowOverVacation, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExecuseRequestHoursallowed", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mExecuseRequestHoursallowed, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeDocumentsAutoSerial", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mEmployeeDocumentsAutoSerial, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@UserDepartmentsPermissions", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mUserDepartmentsPermissions, SqlDbType.Bit)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
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
                Case "BOOLEAN"
                    If obj Is DBNull.Value Then
                        Return False
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

    Public Sub finalized()
        mDataSet.Dispose()
    End Sub

#End Region

#Region "Additional Functions"

    Public Function GetCompanies(ByVal DdlGroups As Global.System.Web.UI.WebControls.DropDownList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = "Select * from sys_Companies Where isNull(CancelDate,'')=''"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlGroups.Items.Clear()

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                'Item.Text = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                Item.Value = ObjDataRow("ID")
                DdlGroups.Items.Add(Item)
            Next

            Item = New Global.System.Web.UI.WebControls.ListItem
            Item.Text = "Non"
            Item.Value = 0
            Item.Selected = True
            DdlGroups.Items.Add(Item)

            If DdlGroups.Items.Count > 0 Then
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

    Public Function GetCompaniesWithUser(ByVal DdlGroups As Global.System.Web.UI.WebControls.DropDownList, ByVal intUserID As Integer) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Dim ClscompaniesUsers As New Clssys_CompaniesUsers(mPage)
        Try

            StrCommandString = "select  c.EngName,c.ArbName,c.id  from sys_companies as c inner join sys_companiesusers  as cu on cu.companyid = c.id where(userid = " & intUserID & ") and canView = 1 "
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlGroups.Items.Clear()

            ClscompaniesUsers.Find(" UserID = " & intUserID)

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                'Item.Text = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                Item.Value = ObjDataRow("ID")
                DdlGroups.Items.Add(Item)
            Next


            If DdlGroups.Items.Count > 0 Then
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
                DdlValues.SelectedValue = Item.Value
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

            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " ' & Me.MainCompanyID
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


#End Region

End Class

