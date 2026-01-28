Public Class ErrorsHandler

#Region "Public Declarations"

    Private PD_StrConnectionString As String
    Private PD_StrSqlStatment As String
    Private PD_StrModuleName As String
    Private PD_StrProceduresName As String
    Private PD_StrErrorDescription As String
    Private PD_StrErrorName As String
    Private PD_IntErrorNumber As Integer
    Private PD_StrErrorSource As String
    Private PD_StrApplicationVersion As String
    Private PD_IntUserId As String
    Private PD_ObjSqlCommand As Data.SqlClient.SqlCommand
    Private PD_ObjSqlParameter As SqlClient.SqlParameter

#End Region

#Region "Public Constructor"

    Public Sub New(ByVal ConnectionString As String)
        Try
            PD_StrConnectionString = ConnectionString
        Catch ex As Exception

        End Try
    End Sub

    Public Sub New()

    End Sub

#End Region

#Region "Public Enumeration"

    Public Enum eRecordingType
        System_DataBase
        System_Folder
        System_ErrorsLog
    End Enum

    Public Enum eSavingType
        Text
        Xml
    End Enum

#End Region

#Region "Public Functions"

    '*************************************************************
    'NAME:          RecordExceptions_DataBase
    'PURPOSE:       Manage all the Exception Which will save to the Data Base 
    'PARAMETERS:    Excep - ErrorNumber
    '               This function is managing all the exception which
    '               will be save to the databese at first you assign 
    '               the exception to diffrant variables in order to 
    '               easy to concatinate it later second you have to 
    '               check about which kind of divice you will use 
    '               database or system log or in spacific folder 
    'RETURNS:       True if successful
    '*************************************************************

    Public Function RecordExceptions_DataBase(ByVal SqlStatment As String, ByVal Excep As System.Exception, ByVal ErrorNumber As Integer, _
    Optional ByVal UserID As String = "", _
    Optional ByVal RecordingType As eRecordingType = eRecordingType.System_DataBase, _
    Optional ByVal SavingType As eSavingType = eSavingType.Text) As Boolean
        Try

            '==================================Comments================================
            'Assigning the Exceptions to the Deffrant Parts which is allowing us form 
            'storing the Exception in the clear way
            '==========================================================================

            AssignErrorValues(Excep, ErrorNumber)
            PD_StrSqlStatment = SqlStatment
            PD_IntUserId = UserID

            '==================================Comments================================
            'Spacify the storing way of the Exception if the storing way is the DataBase 
            'so  we  will  store the Exception in the Database and if the storing way is 
            'the  system_errorlogs then the exception will store in the error log of the 
            'system  and  if  the  selection in the last way the exception will be store 
            'in a spacific folder in the server.
            '==========================================================================

            Select Case RecordingType
                Case eRecordingType.System_DataBase
                    Call ExecuteErrorDatabaseRecording()
                Case eRecordingType.System_ErrorsLog
                    Call ExecuteErrorSystemlogRecording()
                Case eRecordingType.System_Folder
                    Call ExecuteErrorSystemFolderRecording()
            End Select

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function



#End Region

#Region "Private Functions"

    '*************************************************************
    'NAME:          AssignErrorValues
    'PURPOSE:       Assigning the value of the Error to the separat Parts
    'PARAMETERS:    Exception - ErrorNumber
    '               this function is take the Exception and and the Errro 
    '               Number and separate it in to differant Parts
    'RETURNS:       True if successful
    '*************************************************************

    Private Function AssignErrorValues(ByVal Exception As System.Exception, ByVal ErrorNumber As Integer) As Boolean
        Try

            PD_StrModuleName = Exception.TargetSite.DeclaringType.Name
            PD_StrProceduresName = Exception.TargetSite.Name
            PD_StrErrorDescription = Exception.Message
            PD_IntErrorNumber = ErrorNumber
            PD_StrErrorSource = Exception.StackTrace
            PD_StrApplicationVersion = Exception.TargetSite.ReflectedType.Assembly.FullName

            Return True
        Catch ex As Exception
            AssignErrorValues(ex, Err.Number)
            Call ExecuteErrorSystemlogRecording()
            Return False
        End Try
    End Function

    '*************************************************************
    'NAME:          ExecuteErrorDatabaseRecording
    'PURPOSE:       Saving Errors into the Database 
    'PARAMETERS:    Nothing 
    '               Thing function is taking all the parts of 
    '               Error and save it to the database 
    'RETURNS:       True if successful
    '*************************************************************

    Private Function ExecuteErrorDatabaseRecording() As Boolean
        Dim StrSqlStatment As String
        Try
            StrSqlStatment = "Insert into Sys_ErrorsLogs" & _
            " (ModuleName,ProcedureName,ErrorNumber,ErrorDescription,ErrorSource,AppVersion,SqlStatment)Values" & _
            "(@ModuleName,@ProcedureName,@ErrorNumber,@ErrorDescription,@ErrorSource,@AppVersion,@SqlStatment)"

            PD_ObjSqlCommand = New SqlClient.SqlCommand
            PD_ObjSqlCommand.CommandType = CommandType.Text
            PD_ObjSqlCommand.CommandText = StrSqlStatment
            PD_ObjSqlCommand.Connection = New Data.SqlClient.SqlConnection(PD_StrConnectionString)
            PD_ObjSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@ModuleName", SqlDbType.VarChar, 100)).Value = PD_StrModuleName
            PD_ObjSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@ProcedureName", SqlDbType.VarChar, 100)).Value = PD_StrProceduresName
            PD_ObjSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@ErrorNumber", SqlDbType.Int, 4)).Value = PD_IntErrorNumber
            PD_ObjSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@ErrorDescription", SqlDbType.VarChar, 1024)).Value = PD_StrErrorDescription
            PD_ObjSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@ErrorSource", SqlDbType.VarChar, 4096)).Value = PD_StrErrorSource
            PD_ObjSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@AppVersion", SqlDbType.VarChar, 32)).Value = PD_StrApplicationVersion
            PD_ObjSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@SqlStatment", SqlDbType.VarChar, 1024)).Value = PD_StrSqlStatment
            PD_ObjSqlCommand.Connection.Open()
            PD_ObjSqlCommand.ExecuteNonQuery()
            PD_ObjSqlCommand.Connection.Close()

            Return True
        Catch ex As Exception
            AssignErrorValues(ex, Err.Number)
            ExecuteErrorSystemlogRecording()
            Return False
        Finally
            PD_ObjSqlCommand.Connection.Close()
        End Try
    End Function

    '*************************************************************
    'NAME:          ExecuteErrorSystemlogRecording
    'PURPOSE:       Saving Errors into the System Error Log 
    'PARAMETERS:    Nothing 
    '               Thing function is taking all the parts of 
    '               Error and save it to the System error log
    'RETURNS:       True if successful
    '*************************************************************

    Private Function ExecuteErrorSystemlogRecording() As Boolean

        Dim ObjEventlog As New EventLog
        Dim StrEntry As String
        Dim StrAppName As String
        Dim StrLogName As String
        Dim StrMessage As String


        Try

            StrMessage = "================================================" & vbCrLf & _
                   "Title: " & " Error Message coming " & vbCrLf & _
                   "ModuleName: " & PD_StrModuleName & vbCrLf & _
                   "ProcedureName: " & PD_StrProceduresName & vbCrLf & _
                   "ErrorNumber: " & PD_IntErrorNumber & vbCrLf & _
                   "ErrorDescription: " & PD_StrErrorDescription & vbCrLf & _
                   "ErrorSource: " & PD_StrErrorSource & vbCrLf & _
                   "AppVersion: " & PD_StrApplicationVersion & vbCrLf & _
                   "SqlStatment: " & PD_StrSqlStatment & vbCrLf & _
                   "User Id: " & PD_IntUserId & vbCrLf & _
                   "Date/Time: " & Date.Now & vbCrLf & _
                   "================================================" & vbCrLf

            '1)Fill the log entry 
            StrAppName = PD_StrModuleName
            StrLogName = PD_StrProceduresName
            StrEntry = StrMessage

            '2)Check about the log Entry 
            If Not EventLog.SourceExists(StrAppName) Then
                EventLog.CreateEventSource(StrAppName, StrLogName)
            End If

            '3)Record the log entry
            ObjEventlog.Source = StrAppName
            ObjEventlog.WriteEntry(StrEntry, EventLogEntryType.Error)

            Return True
        Catch ex As Exception
            AssignErrorValues(ex, Err.Number)
            ExecuteErrorSystemFolderRecording()
            Return False
        Finally
            ObjEventlog.Dispose()
        End Try
    End Function

    '*************************************************************
    'NAME:          ExecuteErrorSystemFolderRecording
    'PURPOSE:       Saving Errors into Spacific folder 
    'PARAMETERS:    Nothing 
    '               Thing function is taking all the parts of 
    '               Error and save it to a spacific Folder 
    'RETURNS:       True if successful
    '*************************************************************

    Private Function ExecuteErrorSystemFolderRecording() As Boolean
        Dim StrMessage As String
        Dim BolErrorLogExist As Boolean
        Dim StrFolderDistination As String


        Try

            StrMessage = "================================================" & vbCrLf & _
                    "Title: " & " Error Message coming " & vbCrLf & _
                    "ModuleName: " & PD_StrModuleName & vbCrLf & _
                    "ProcedureName: " & PD_StrProceduresName & vbCrLf & _
                    "ErrorNumber: " & PD_IntErrorNumber & vbCrLf & _
                    "ErrorDescription: " & PD_StrErrorDescription & vbCrLf & _
                    "ErrorSource: " & PD_StrErrorSource & vbCrLf & _
                    "AppVersion: " & PD_StrApplicationVersion & vbCrLf & _
                    "SqlStatment: " & PD_StrSqlStatment & vbCrLf & _
                    "User Id :    " & PD_IntUserId & vbCrLf & _
                    "Date/Time: " & Date.Now & vbCrLf & _
                    "================================================" & vbCrLf

            StrFolderDistination = My.Application.Info.DirectoryPath & "\ErrorsLog.txt"


            BolErrorLogExist = System.IO.File.Exists(StrFolderDistination)

            If Not BolErrorLogExist Then
                System.IO.File.Create(StrFolderDistination).Close()
            End If

            Dim ObjStreamWriter As New System.IO.StreamWriter(StrFolderDistination, True)
            ObjStreamWriter.WriteLine(StrMessage)
            ObjStreamWriter.Close()


            Return True
        Catch ex As Exception
            AssignErrorValues(ex, Err.Number)
            'ExecuteErrorSystemlogRecording()
            Return False
        End Try

    End Function

#End Region

#Region "Public Destructor"

    '*************************************************************
    'NAME:          Finalize
    'PURPOSE:       Reset all the value of the class 
    'PARAMETERS:    Nothing 
    '               this function Reset all the value of the class 
    'RETURNS:       Nothing 
    '*************************************************************

    Protected Overrides Sub finalize()

        PD_StrConnectionString = String.Empty
        PD_StrSqlStatment = String.Empty
        PD_StrModuleName = String.Empty
        PD_StrProceduresName = String.Empty
        PD_StrErrorDescription = String.Empty
        PD_StrErrorName = String.Empty
        PD_IntErrorNumber = 0
        PD_StrErrorSource = String.Empty
        PD_StrApplicationVersion = String.Empty


    End Sub

#End Region

End Class
