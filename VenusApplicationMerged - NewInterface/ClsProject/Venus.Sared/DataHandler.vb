Public Class DataHandler

#Region "Public Declarations"

    Private PD_ObjErrorHandler As New ErrorsHandler("Data Source=salah;Initial Catalog=VENUSNET_20110403;User ID=sa;Password=")
    Public Enum DataOutType
        _CommandParameter
        _String
    End Enum

#End Region

#Region "Public Constructor"

    Public Sub New()
        Try
            'PD_ErrorLog = New Venus.Shared.Errors.ErrorsLog(

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Public Functions"
    Public Function GetLastSerial(ByVal Table As String, ByVal Field As String, ByRef Serial As String, ByVal strConnectionString As String, Optional ByVal SerialFormat As String = "") As Boolean
        Dim StrCommandString As String
        Dim StrSerial As String
        Dim IntSerial As Integer
        Dim ObjSqlCommand As New SqlClient.SqlCommand
        Dim ObjDataReader As Data.SqlClient.SqlDataReader

        Try
            StrCommandString = " Select Max(" & Field & ") as Serial From " & Table & " Where IsNumeric(" & Field & ")=1"
            With ObjSqlCommand
                .Connection = New SqlClient.SqlConnection(strConnectionString)
                .CommandText = StrCommandString
                .CommandType = CommandType.Text
                .Connection.Open()
                ObjDataReader = .ExecuteReader()
                If ObjDataReader.Read() Then
                    StrSerial = IIf(ObjDataReader("Serial") Is DBNull.Value, 0, ObjDataReader("Serial"))
                    If IsNumeric(StrSerial) Then
                        IntSerial = CType(StrSerial, Integer)
                        IntSerial += 1
                        If SerialFormat <> "" Then
                            Serial = Format(IntSerial, SerialFormat)
                        Else
                            Serial = CStr(IntSerial)
                        End If
                    Else
                        Serial = ""
                    End If
                Else
                    Serial = ""
                End If
                .Connection.Close()
            End With
            Return True
        Catch ex As Exception
            Return False
            PD_ObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, ErrorsHandler.eRecordingType.System_ErrorsLog)
        End Try
    End Function

    Public Function BeginTransactions(ByVal Connection As SqlClient.SqlConnection, ByVal IsolationLevel As System.Data.IsolationLevel, ByRef Transactions As SqlClient.SqlTransaction) As Boolean
        Try
            Transactions = Connection.BeginTransaction(IsolationLevel)
            Return True
        Catch ex As Exception
            Return False
            PD_ObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, ErrorsHandler.eRecordingType.System_ErrorsLog)
        End Try
    End Function

    Public Function CommitTransactions(ByVal Transaction As SqlClient.SqlTransaction) As Boolean
        Try
            Transaction.Commit()
            Return True
        Catch ex As Exception
            Return True
            PD_ObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, ErrorsHandler.eRecordingType.System_ErrorsLog)
        End Try
    End Function

    Public Function RollbackTransactions(ByVal Transaction As SqlClient.SqlTransaction) As Boolean
        Try
            Transaction.Rollback()
            Return True
        Catch ex As Exception
            Return False
            PD_ObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, ErrorsHandler.eRecordingType.System_ErrorsLog)
        End Try
    End Function

    Public Function CheckPoint(ByVal Transaction As SqlClient.SqlTransaction, ByVal CheckPointName As String) As Boolean
        Try
            Transaction.Save(CheckPointName)
            Return True
        Catch ex As Exception
            Return False
            PD_ObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, ErrorsHandler.eRecordingType.System_ErrorsLog)
        End Try
    End Function
#End Region

#Region "Public Shared Functions"
    Public Shared Function CheckValidDataObject(ByVal DataObject As Object) As Boolean
        Try
            If TypeOf DataObject Is DataSet Then
                If Not DataObject Is Nothing AndAlso CType(DataObject, DataSet).Tables.Count > CD_INTINITIALVALUE AndAlso _
                CType(DataObject, DataSet).Tables(CD_INTINITIALVALUE).Rows.Count > CD_INTINITIALVALUE Then
                    Return True
                End If
            ElseIf TypeOf DataObject Is DataTable Then
                If Not DataObject Is Nothing AndAlso CType(DataObject, DataTable).Rows.Count > CD_INTINITIALVALUE Then
                    Return True
                End If
            ElseIf TypeOf DataObject Is DataView Then
                If Not DataObject Is Nothing AndAlso CType(DataObject, DataView).Count > CD_INTINITIALVALUE Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function DataValue(ByVal Value As Object, ByVal Type As SqlDbType) As Object
        Try
            If Value Is DBNull.Value Then
                Select Case Type
                    Case SqlDbType.Char _
                        , SqlDbType.NChar _
                        , SqlDbType.NVarChar _
                        , SqlDbType.VarChar _
                        , SqlDbType.Text
                        Return String.Empty
                    Case SqlDbType.Int _
                         , SqlDbType.TinyInt, _
                         SqlDbType.SmallInt
                        Return 0
                    Case SqlDbType.SmallMoney, _
                        SqlDbType.Variant, _
                        SqlDbType.Float
                        Return 0.0
                    Case SqlDbType.Bit
                        Return False
                    Case SqlDbType.DateTime, _
                          SqlDbType.SmallDateTime
                        Return Nothing
                    Case Else

                End Select
            Else
                Return Value
            End If
            Return Value
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function DataValue_Out(ByVal Value As Object, ByVal Type As SqlDbType, Optional ByVal _ForignKey As Boolean = False) As Object
        Try
            If Value Is DBNull.Value Then
                Select Case Type
                    Case SqlDbType.Char _
                        , SqlDbType.NChar _
                        , SqlDbType.NVarChar _
                        , SqlDbType.VarChar _
                        , SqlDbType.Text
                        Return String.Empty
                    Case SqlDbType.Int _
                         , SqlDbType.TinyInt, _
                         SqlDbType.SmallInt
                        If _ForignKey Then
                            Return 0
                        Else
                            Return vbNullString
                        End If
                    Case SqlDbType.SmallMoney, _
                        SqlDbType.Variant, _
                        SqlDbType.Money, _
                        SqlDbType.Real, _
                        SqlDbType.Float
                        Return 0.0
                    Case SqlDbType.Bit
                        Return False
                    Case SqlDbType.DateTime, _
                          SqlDbType.SmallDateTime
                        Return Nothing
                    Case Else
                        Return String.Empty
                End Select
            Else
                Return Value
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function DataValue_In(ByVal Value As Object, ByVal Type As SqlDbType, Optional ByVal _ForignKey As Boolean = False) As Object
        Try

            Select Case Type
                Case SqlDbType.Char _
                    , SqlDbType.NChar _
                    , SqlDbType.NVarChar _
                    , SqlDbType.VarChar _
                    , SqlDbType.Text
                    If Len(Value) = 0 Then
                        Return DBNull.Value
                    Else
                        Return Value
                    End If

                Case SqlDbType.Int _
                     , SqlDbType.TinyInt, _
                     SqlDbType.SmallInt, _
                    SqlDbType.SmallMoney, _
                    SqlDbType.Variant, _
                    SqlDbType.Float

                    If Not IsNumeric(Value) Then
                        Return DBNull.Value
                    ElseIf _ForignKey And Val(Value) = 0 Then
                        Return DBNull.Value
                    Else
                        Return Value
                    End If

                Case SqlDbType.Bit
                    Return Value

                Case SqlDbType.DateTime, _
                      SqlDbType.SmallDateTime
                    If Not IsDate(Value) OrElse Value = #12:00:00 AM# Then
                        Return DBNull.Value
                    Else
                        Return Value
                    End If
                Case SqlDbType.Real
                    If Not IsNumeric(Value) Or Val(Value) = 0 Then
                        Return DBNull.Value
                    Else
                        Return Value
                    End If
                Case SqlDbType.Xml
                    If Len(Value) = 0 Then
                        Return DBNull.Value
                    Else
                        Return Value
                    End If
                Case Else
                    Return Value
            End Select

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function DataValue_InS(ByVal Value As Object, ByVal Type As SqlDbType, Optional ByVal _ForignKey As Boolean = False, _
                                        Optional ByVal ParameterType As DataOutType = DataOutType._CommandParameter, _
                                        Optional ByVal DateFormat As String = "dd/MM/yyyy") As Object
        Try
            Select Case Type
                Case SqlDbType.Char _
                    , SqlDbType.NChar _
                    , SqlDbType.NVarChar _
                    , SqlDbType.VarChar _
                    , SqlDbType.Text _
                    , SqlDbType.Xml
                    If Len(Value) = 0 Then
                        Select Case ParameterType
                            Case DataOutType._CommandParameter
                                Return DBNull.Value
                            Case DataOutType._String
                                Return "Null"
                        End Select
                    Else
                        Select Case ParameterType
                            Case DataOutType._CommandParameter
                                Return Value
                            Case DataOutType._String
                                Return "'" & Value.ToString.Replace("'", "''") & "'"
                        End Select
                    End If
                Case SqlDbType.Int _
                     , SqlDbType.TinyInt, _
                     SqlDbType.SmallInt, _
                    SqlDbType.SmallMoney, _
                    SqlDbType.Variant, _
                    SqlDbType.Real
                    If Not IsNumeric(Value) Then
                        Select Case ParameterType
                            Case DataOutType._CommandParameter
                                Return DBNull.Value
                            Case DataOutType._String
                                Return "Null"
                        End Select
                    ElseIf _ForignKey And Val(Value) = 0 Then
                        Select Case ParameterType
                            Case DataOutType._CommandParameter
                                Return DBNull.Value
                            Case DataOutType._String
                                Return "Null"
                        End Select
                    Else
                        Return Value
                    End If
                Case SqlDbType.Bit
                    Select Case ParameterType
                        Case DataOutType._CommandParameter
                            Return Value
                        Case DataOutType._String
                            If Value Then
                                Return 1
                            Else
                                Return 0
                            End If
                    End Select
                Case SqlDbType.DateTime, _
                     SqlDbType.SmallDateTime
                    If Not IsDate(Value) OrElse Value = #12:00:00 AM# Then
                        Select Case ParameterType
                            Case DataOutType._CommandParameter
                                Return DBNull.Value
                            Case DataOutType._String
                                Return "Null"
                        End Select
                    Else
                        Select Case ParameterType
                            Case DataOutType._CommandParameter
                                Return Value
                            Case DataOutType._String
                                Return "'" & CDate(Value).ToString(DateFormat) & "'"
                        End Select
                    End If
                Case Else
                    Return Value
            End Select
            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Shared Function CountRelated(ByVal StrConnection As String, ByVal IntID As Integer, ByVal strTableName As String) As Integer
        Dim RelationDs As New DataSet
        Dim IntCounter As Integer = 0
        Dim ObjDataRow As DataRow
        RelationDs = GetRelatedTables(StrConnection, strTableName)
        For Each ObjDataRow In RelationDs.Tables(0).Rows
            IntCounter = IntCounter + CountTable(StrConnection, ObjDataRow.Item(6), IntID, ObjDataRow.Item(7))
        Next
        Return IntCounter
    End Function

    Private Shared Function CountTable(ByVal StrConnection As String, ByVal strTable As String, ByVal IntID As Integer, ByVal strColumnName As String) As Integer
        Dim StrCmd As String = "Select * from " & strTable & " Where " & strColumnName & "= " & IntID
        Dim SqlDa As New SqlClient.SqlDataAdapter(StrCmd, StrConnection)
        Dim dt As New DataTable
        Try
            SqlDa.Fill(dt)
            Return dt.Rows.Count
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Shared Function GetRelatedTables(ByVal StrConnection As String, ByVal strTableName As String)
        Dim StrCmd As String = "exec sp_fkeys '" & strTableName.Trim & "'"
        Dim SqlDa As New SqlClient.SqlDataAdapter(StrCmd, StrConnection)
        Dim RelationDs As New DataSet
        Try
            SqlDa.Fill(RelationDs)
            Return RelationDs
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Public Destructor"

    Public Sub finlize()
        Try




        Catch ex As Exception

        End Try
    End Sub


#End Region

End Class
