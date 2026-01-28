Imports System.Web
Imports System.Configuration
Public Class WebSecurity

#Region "Public Decliration"

    Public Sub New()
        Me.mConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        AssignConnectionParameter()
    End Sub

    Protected Function AssignConnectionParameter() As Boolean
        Dim conInfo As String() = New String(4) {}
        conInfo = Me.ConnectionString.Split(";"c)
        For x As Integer = 0 To conInfo.Length - 1
            Dim str As String() = New String(1) {}
            str = conInfo(x).Split("="c)
            Select Case str(0)
                Case "data source"
                    Me.mDataBaseServerName = str(1)
                    Exit Select
                Case "initial catalog"
                    Me.mDataBaseName = str(1)
                    Exit Select
            End Select
        Next
    End Function

#End Region

#Region "Protected Decleration"

    Protected mDataBaseUserID As String
    Protected mDataBasePassword As String
    Protected mDataBaseServerName As String
    Protected mDataBaseName As String
    Protected mConnectionString As String
    Protected mSqlConnection As SqlClient.SqlConnection
    Protected mAllowAdd As Boolean
    Protected mAllowDelete As Boolean
    Protected mAllowEdit As Boolean
    Protected mAllowPrint As Boolean

#End Region

#Region "Public property"

    Public ReadOnly Property ConnectionString() As String
        Get
            Return mConnectionString
        End Get
    End Property

    Public Property DataBaseUserID() As String
        Get
            Return mDataBaseUserID
        End Get
        Set(ByVal value As String)
            mDataBaseUserID = value
        End Set
    End Property

    Public Property DataBasePassword() As String
        Get
            Return mDataBasePassword
        End Get
        Set(ByVal value As String)
            mDataBasePassword = value
        End Set
    End Property

    Public ReadOnly Property DataBaseServerName() As String
        Get
            Return mDataBaseServerName
        End Get
    End Property

    Public ReadOnly Property DataBaseName() As String
        Get
            Return mDataBaseName
        End Get
    End Property

    Public Property AllowAdd() As Boolean
        Get
            Return mAllowAdd
        End Get
        Set(ByVal value As Boolean)
            mAllowAdd = value
        End Set
    End Property

    Public Property AllowDelete() As Boolean
        Get
            Return mAllowDelete
        End Get
        Set(ByVal value As Boolean)
            mAllowDelete = value
        End Set
    End Property

    Public Property AllowEdit() As Boolean
        Get
            Return mAllowEdit
        End Get
        Set(ByVal value As Boolean)
            mAllowEdit = value
        End Set
    End Property

    Public Property AllowPrint() As Boolean
        Get
            Return mAllowPrint
        End Get
        Set(ByVal value As Boolean)
            mAllowPrint = value
        End Set
    End Property

#End Region

#Region "Private Function"

    Private Function CheckConnection() As Boolean
        Try
            mSqlConnection = New SqlClient.SqlConnection(Me.mConnectionString)
            mSqlConnection.Open()
            mSqlConnection.Close()
            Return True
        Catch ex As Exception
            Return False
        Finally
            mSqlConnection.Close()
        End Try
    End Function

    Private Function ClearPermission() As Boolean
        Try

            mAllowAdd = False
            mAllowDelete = False
            mAllowEdit = False
            mAllowPrint = False

        Catch ex As Exception

        End Try
    End Function

    Private Function AssignPermission(ByVal Reader As SqlClient.SqlDataReader) As Boolean
        Try

            mAllowAdd = Reader("AllowAdd")
            mAllowDelete = Reader("AllowDelete")
            mAllowEdit = Reader("AllowEdit")
            mAllowPrint = Reader("AllowPrint")

        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "Public Function"

    Public Function CreateConnection(ByRef page As System.Web.UI.Page) As Boolean
        Dim StrConnectionString As String = "Data source=[COMPUTERNAME]|Initial Catalog=[DATABASENAME]|User ID=[USERID]|Password=[PASSWORD]"
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler

        Try
            StrConnectionString = Replace(StrConnectionString, "[COMPUTERNAME]", Me.DataBaseServerName)
            StrConnectionString = Replace(StrConnectionString, "[DATABASENAME]", Me.DataBaseName)
            StrConnectionString = Replace(StrConnectionString, "[USERID]", Me.DataBaseUserID)
            StrConnectionString = Replace(StrConnectionString, "[PASSWORD]", Me.DataBasePassword)

            Me.mConnectionString = Replace(StrConnectionString, "|", ";")
            If CheckConnection() Then
                'Save the connection for overall the application
                page.Session.Add("CurrentConnectionString", StrConnectionString)
                page.Session.Add("CurrentUserID", Me.DataBaseUserID)
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

        End Try
    End Function

    Public Function CreateUserLogEntry(ByVal Page As System.Web.UI.Page) As Boolean
        Dim IntUserId As Integer
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim StrCommandString As String = String.Empty
        Dim ObjSqlCommand As New System.Data.SqlClient.SqlCommand
        Try

            IntUserId = ObjWebHandler.GetCookies(Page, "CurrentUserID", IntUserId)
            StrCommandString = "Insert into sys_UsersLog(UserId,Logindate)values(@UserID,GetDate())"
            ObjSqlCommand.CommandText = StrCommandString
            ObjSqlCommand.CommandType = CommandType.Text
            ObjSqlCommand.Connection = New SqlClient.SqlConnection(GetCurrentConnection(Page))
            ObjSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@UserID", SqlDbType.Int)).Value = IntUserId
            ObjSqlCommand.Connection.Open()
            ObjSqlCommand.ExecuteNonQuery()
            ObjSqlCommand.Connection.Close()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetSavedUser(ByRef Page As System.Web.UI.Page) As Boolean
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim StrConnectionString As String = String.Empty

        Try
            ObjWebHandler.GetCookies(Page, "CurrentConnectionString", StrConnectionString)
            If StrConnectionString <> "" Then
                Me.mConnectionString = Replace(StrConnectionString, "|", ";")
                If CheckConnection() Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Function GetCurrentConnection(ByVal page As System.Web.UI.Page) As String
        Dim StrConnectionString As String = String.Empty
        Try

            If Not page.Session.Item("CurrentConnectionString") Is Nothing Then
                StrConnectionString = page.Session.Item("CurrentConnectionstring")
                StrConnectionString = Replace(StrConnectionString, "|", ";")
            Else
                Return ""
            End If
            Return StrConnectionString
        Catch ex As Exception
            page.Response.Redirect("frmLoginPage.aspx")
            Return ""
        End Try
    End Function

    Public Function GetUserPermetion(ByVal Page As System.Web.UI.Page, ByVal UserID As Integer, ByVal FormID As Integer) As Boolean

        Dim ObjSqlCommand As New SqlClient.SqlCommand
        Dim ObjSqlReader As SqlClient.SqlDataReader

        Try

            ObjSqlCommand.Connection = New SqlClient.SqlConnection(GetCurrentConnection(Page))
            ObjSqlCommand.CommandType = CommandType.StoredProcedure
            ObjSqlCommand.CommandText = "hrs_UserPermission"
            ObjSqlCommand.Parameters.Add(New SqlClient.SqlParameter("UserID", SqlDbType.Int)).Value = UserID
            ObjSqlCommand.Parameters.Add(New SqlClient.SqlParameter("FormID", SqlDbType.Int)).Value = FormID
            ObjSqlCommand.Connection.Open()
            ObjSqlReader = ObjSqlCommand.ExecuteReader()

            If ObjSqlReader.Read() Then
                AssignPermission(ObjSqlReader)
            Else
                ClearPermission()
            End If
            ObjSqlCommand.Connection.Close()

        Catch ex As Exception

        End Try
    End Function

    Public Function GetUserInformation(ByVal page As System.Web.UI.Page, ByVal UserID As Integer) As Boolean
        Dim IntUserId As Integer
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler

        Try
            IntUserId = ObjWebHandler.GetCookies(page, "CurrentUserID", IntUserId)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetUserLogEntry(ByVal page As System.Web.UI.Page, ByVal UserID As Integer, ByVal LastLogin As String) As Boolean
        Dim StrCommandString As String = String.Empty
        Dim ObjSqlCommand As New SqlClient.SqlCommand
        Dim DteUserLastLoginDate As Date = Nothing
        Try
            StrCommandString = "Select Max(LoginDate) as LastLoginDate From sys_usersLog where UserID=@UserID"
            ObjSqlCommand.Connection = New SqlClient.SqlConnection(GetCurrentConnection(page))
            ObjSqlCommand.CommandType = CommandType.Text
            ObjSqlCommand.CommandText = StrCommandString
            ObjSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@UserID", SqlDbType.Int)).Value = UserID
            ObjSqlCommand.Connection.Open()
            DteUserLastLoginDate = ObjSqlCommand.ExecuteScalar
            If DteUserLastLoginDate = Nothing Then
                LastLogin = "Welcome to venus application"
            Else
                LastLogin = DteUserLastLoginDate
            End If
            ObjSqlCommand.Connection.Close()

            Return True
        Catch ex As Exception
            Return False
        Finally
            ObjSqlCommand.Connection.Close()
        End Try
    End Function

#End Region

End Class
