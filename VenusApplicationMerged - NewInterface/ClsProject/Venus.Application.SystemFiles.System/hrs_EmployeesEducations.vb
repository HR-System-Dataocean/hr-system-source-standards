'Public Class Clshrs_EmployeesEducations
'    Inherits ClsDataAcessLayer

'#Region "Class Constructors"

'    Public Sub New(ByVal Page As Web.UI.Page)
'        MyBase.New(Page)
'        mTable = " hrs_EmployeesEducations "
'        mInsertParameter = " EmployeeID,EducationID,GraduationDate,GraduationDegree,Description,Remarks,RegUserID,RegComputerID "
'        mInsertParameterValues = " @EmployeeID,@EducationID,@GraduationDate,@GraduationDegree,@Description,@Remarks,@RegUserID,@RegComputerID "
'        mUpdateParameter = " EmployeeID=@EmployeeID,EducationID=@EducationID,GraduationDate=@GraduationDate,GraduationDegree=@GraduationDegree,Description=@Description,Remarks=@Remarks,RegUserID=@RegUserID,RegComputerID=@RegComputerID "
'        mSelectCommand = " Select * From  " & mTable
'        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
'        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
'        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
'    End Sub

'#End Region

'#Region "Private Members"

'    Private mID As Object
'    Private mEmployeeID As Object
'    Private mEducationID As Object
'    Private mGraduationDate As Object
'    Private mGraduationDegree As Object
'    Private mDescription As String
'    Private mRemarks As String
'    Private mRegUserID As Object
'    Private mRegComputerID As Object
'    Private mRegDate As Object
'    Private mCancelDate As Object

'    Const uwgEducationId = 0
'    Const uwgDate = 1
'    Const uwgDegree = 2
'    Const uwgDescriptions = 3


'#End Region

'#Region "Public property"

'    Public Property ID() As Object
'        Get
'            Return mID
'        End Get
'        Set(ByVal Value As Object)
'            mID = Value
'        End Set
'    End Property

'    Public Property EmployeeID() As Object
'        Get
'            Return mEmployeeID
'        End Get
'        Set(ByVal Value As Object)
'            mEmployeeID = Value
'        End Set
'    End Property

'    Public Property EducationID() As Object
'        Get
'            Return mEducationID
'        End Get
'        Set(ByVal Value As Object)
'            mEducationID = Value
'        End Set
'    End Property

'    Public Property GraduationDate() As Object
'        Get
'            Return mGraduationDate
'        End Get
'        Set(ByVal Value As Object)
'            mGraduationDate = Value
'        End Set
'    End Property

'    Public Property GraduationDegree() As Object
'        Get
'            Return mGraduationDegree
'        End Get
'        Set(ByVal Value As Object)
'            mGraduationDegree = Value
'        End Set
'    End Property

'    Public Property Description() As String
'        Get
'            Return mDescription
'        End Get
'        Set(ByVal Value As String)
'            mDescription = Value
'        End Set
'    End Property

'    Public Property Remarks() As String
'        Get
'            Return mRemarks
'        End Get
'        Set(ByVal Value As String)
'            mRemarks = Value
'        End Set
'    End Property

'    Public Property RegUserID() As Object
'        Get
'            Return mRegUserID
'        End Get
'        Set(ByVal Value As Object)
'            mRegUserID = Value
'        End Set
'    End Property

'    Public Property RegComputerID() As Object
'        Get
'            Return mRegComputerID
'        End Get
'        Set(ByVal Value As Object)
'            mRegComputerID = Value
'        End Set
'    End Property

'    Public Property RegDate() As Object
'        Get
'            Return mRegDate
'        End Get
'        Set(ByVal Value As Object)
'            mRegDate = Value
'        End Set
'    End Property

'    Public Property CancelDate() As Object
'        Get
'            Return mCancelDate
'        End Get
'        Set(ByVal Value As Object)
'            mCancelDate = Value
'        End Set
'    End Property

'#End Region

'#Region "Public Function"

'    Public Function Find(ByVal Filter As String) As Boolean
'        Dim StrSelectCommand As String
'        Try
'            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
'            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
'            mDataSet = New DataSet
'            mSqlDataAdapter.Fill(mDataSet)
'            If mDataHandler.CheckValidDataObject(mDataSet) Then
'                GetParameter(mDataSet)
'            Else
'                Clear()
'            End If
'            If mID > 0 Then
'                Return True
'            End If
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    Public Function SaveUpdate(ByVal Filter As String) As Boolean
'        Dim strSQL As String
'        Dim Value As Integer
'        Try
'            strSQL = "Select ID From hrs_EmployeesEducations Where " & Filter
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = strSQL
'            mSqlCommand.Connection.Open()
'            Value = mSqlCommand.ExecuteScalar()
'            mSqlCommand.Connection.Close()
'            If Value > 0 Then
'                Update(Filter)
'            Else
'                Save()
'            End If
'            Return True
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(strSQL, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    Public Function Save() As Boolean
'        Try
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = mInsertCommand
'            SetParameter(mSqlCommand)
'            mSqlCommand.Connection.Open()
'            mSqlCommand.ExecuteNonQuery()
'            mSqlCommand.Connection.Close()
'            Return True
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    Public Function Update(ByVal Filter As String) As Boolean
'        Dim StrUpdateCommand As String
'        Try
'            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = StrUpdateCommand
'            SetParameter(mSqlCommand)
'            mSqlCommand.Connection.Open()
'            mSqlCommand.ExecuteNonQuery()
'            mSqlCommand.Connection.Close()
'            Return True
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    Public Function Delete(ByVal Filter As String) As Boolean
'        Dim StrDeleteCommand As String
'        Try
'            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = StrDeleteCommand
'            SetParameter(mSqlCommand)
'            mSqlCommand.Connection.Open()
'            mSqlCommand.ExecuteNonQuery()
'            mSqlCommand.Connection.Close()
'            Return True
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    Public Function Clear() As Boolean
'        Try
'            mID = 0
'            mEmployeeID = 0
'            mEducationID = 0
'            mGraduationDate = Nothing
'            mGraduationDegree = 0
'            mDescription = String.Empty
'            mRemarks = String.Empty
'            mRegUserID = 0
'            mRegComputerID = 0
'            mRegDate = Nothing
'            mCancelDate = Nothing

'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    Public Function FirstRecord() As Boolean
'        Dim StrSelectCommand As String
'        Try
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
'            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
'            mDataSet = New DataSet
'            mSqlDataAdapter.Fill(mDataSet)
'            If mDataHandler.CheckValidDataObject(mDataSet) Then
'                GetParameter(mDataSet)
'                Return True
'            Else
'                Clear()
'            End If
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    Public Function LastRecord() As Boolean
'        Dim StrSelectCommand As String
'        Try
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
'            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
'            mDataSet = New DataSet
'            mSqlDataAdapter.Fill(mDataSet)
'            If mDataHandler.CheckValidDataObject(mDataSet) Then
'                GetParameter(mDataSet)
'                Return True
'            Else
'                Clear()
'            End If
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    Public Function NextRecord() As Boolean
'        Dim StrSelectCommand As String
'        Try
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
'            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
'            mDataSet = New DataSet
'            mSqlDataAdapter.Fill(mDataSet)
'            If mDataHandler.CheckValidDataObject(mDataSet) Then
'                GetParameter(mDataSet)
'                Return True
'            Else
'                Clear()
'            End If
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    Public Function previousRecord() As Boolean
'        Dim StrSelectCommand As String
'        Try
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
'            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
'            mDataSet = New DataSet
'            mSqlDataAdapter.Fill(mDataSet)
'            If mDataHandler.CheckValidDataObject(mDataSet) Then
'                GetParameter(mDataSet)
'                Return True
'            Else
'                Clear()
'            End If
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    Public Function SaveEmployeesEducations(ByVal ObjGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal IntEmployeeId As Integer) As Boolean
'        Dim ObjRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
'        Dim StrSqlCommand As String = String.Empty
'        Dim ObjSqlCommand As New SqlClient.SqlCommand
'        Try
'            StrSqlCommand = "Delete from hrs_EmployeesEducations Where EmployeeID=" & IntEmployeeId & ";" & vbNewLine
'            For Each ObjRow In ObjGrid.Rows
'                If Not ObjRow.Cells(uwgEducationId).Value Is Nothing Then
'                    StrSqlCommand &= "Insert Into hrs_EmployeesEducations(EmployeeID,EducationID,GraduationDate,GraduationDegree,Description)Values" & _
'                                   "(" & IntEmployeeId & _
'                                   "," & ObjRow.Cells(uwgEducationId).Value & _
'                                   ",'" & IIf(ObjRow.Cells(uwgDate).Value Is Nothing, "", ObjRow.Cells(uwgDate).Value) & _
'                                   "','" & ObjRow.Cells(uwgDegree).Value & _
'                                   "','" & ObjRow.Cells(uwgDescriptions).Value & _
'                                   "');" & vbNewLine
'                End If
'            Next

'            ObjSqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
'            ObjSqlCommand.CommandText = CONFIG_DATEFORMAT & StrSqlCommand
'            ObjSqlCommand.CommandType = CommandType.Text
'            ObjSqlCommand.Connection.Open()
'            ObjSqlCommand.ExecuteNonQuery()
'            ObjSqlCommand.Connection.Close()

'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrSqlCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        Finally
'            ObjSqlCommand.Connection.Close()
'            ObjSqlCommand.Connection = Nothing
'            ObjSqlCommand.Dispose()
'        End Try
'    End Function

'#End Region

'#Region "Class Private Function"

'    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
'        Try
'            With Ds.Tables(0).Rows(0)
'                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
'                mEmployeeID = mDataHandler.DataValue_Out(.Item("EmployeeID"), SqlDbType.Int)
'                mEducationID = mDataHandler.DataValue_Out(.Item("EducationID"), SqlDbType.Int)
'                mGraduationDate = mDataHandler.DataValue_Out(.Item("GraduationDate"), SqlDbType.DateTime)
'                mGraduationDegree = mDataHandler.DataValue_Out(.Item("GraduationDegree"), SqlDbType.Real)
'                mDescription = mDataHandler.DataValue_Out(.Item("Description"), SqlDbType.VarChar)
'                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
'                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int)
'                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int)
'                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
'                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
'            End With
'            Return True
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
'        Try
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EducationID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEducationID, SqlDbType.Int)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GraduationDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mGraduationDate, SqlDbType.DateTime)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GraduationDegree", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mGraduationDegree, SqlDbType.Real)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Description", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mDescription, SqlDbType.VarChar)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegUserID, SqlDbType.Int)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int)
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'#End Region

'#Region "Class Destructors"

'    Public Sub finalized()
'        mDataSet.Dispose()
'    End Sub

'#End Region

'End Class

