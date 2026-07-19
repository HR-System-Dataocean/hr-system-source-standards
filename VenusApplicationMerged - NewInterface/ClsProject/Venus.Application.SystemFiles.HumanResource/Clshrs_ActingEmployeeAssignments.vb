Imports Venus.Application.SystemFiles.System

Public Class Clshrs_ActingEmployeeAssignments
    Inherits ClsDataAcessLayer

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_ActingEmployeeAssignments "
        mSelectCommand = CONFIG_DATEFORMAT & " SELECT * FROM " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " INSERT INTO " & mTable &
            " (Code, OriginalEmployeeID, ActingEmployeeID, EffectiveFrom, EffectiveTo, Reason, Remarks, RegUserID, RegDate)" &
            " VALUES (@Code,@OriginalEmployeeID,@ActingEmployeeID,@EffectiveFrom,@EffectiveTo,@Reason,@Remarks,@RegUserID,GETDATE())"
        mUpdateCommand = CONFIG_DATEFORMAT & " UPDATE " & mTable &
            " SET OriginalEmployeeID=@OriginalEmployeeID, ActingEmployeeID=@ActingEmployeeID," &
            " EffectiveFrom=@EffectiveFrom, EffectiveTo=@EffectiveTo, Reason=@Reason, Remarks=@Remarks"
    End Sub

    Public Property ID As Integer
    Public Property Code As String
    Public Property OriginalEmployeeID As Integer
    Public Property ActingEmployeeID As Integer
    Public Property EffectiveFrom As Date
    Public Property EffectiveTo As Date
    Public Property Reason As String
    Public Property Remarks As String
    Public Property RegUserID As Integer
    Public Property RegDate As DateTime
    Public Property CancelUserID As Integer
    Public Property CancelDate As DateTime
    Public Property CancelReason As String

    Public Function Find(ByVal filter As String) As Boolean
        Dim sql As String = mSelectCommand & If(filter.Trim() = "", "", " WHERE " & filter)
        Try
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(sql, mConnectionString)
            mDataset = New DataSet
            mSqlDataAdapter.Fill(mDataset)
            If mDataHandler.CheckValidDataObject(mDataset) Then
                LoadRow(mDataset.Tables(0).Rows(0))
                Return True
            End If
            Clear()
        Catch ex As Exception
            HandleError(sql, ex)
        End Try
        Return False
    End Function

    Public Function Save() As Boolean
        Return ExecuteCommand(mInsertCommand, False)
    End Function

    Public Function Update(ByVal filter As String) As Boolean
        Dim sql As String = mUpdateCommand & " WHERE " & filter
        Try
            mSqlCommand = New SqlClient.SqlCommand(sql, New SqlClient.SqlConnection(mConnectionString))
            SetParameters(mSqlCommand, False)
            Dim handler As New Venus.Shared.Web.WebHandler()
            handler.Add2History(mConnectionString, ID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            Return True
        Catch ex As Exception
            HandleError(sql, ex)
        End Try
        Return False
    End Function

    Public Function Cancel(ByVal filter As String, Optional ByVal reason As String = "") As Boolean
        Dim sql As String = CONFIG_DATEFORMAT & " UPDATE " & mTable &
            " SET CancelDate=GETDATE(), CancelUserID=@CancelUserID, CancelReason=@CancelReason WHERE " & filter
        Try
            Using connection As New SqlClient.SqlConnection(mConnectionString)
                Using command As New SqlClient.SqlCommand(sql, connection)
                    command.Parameters.Add("@CancelUserID", SqlDbType.Int).Value = Me.mDataBaseUserRelatedID
                    command.Parameters.Add("@CancelReason", SqlDbType.NVarChar, 500).Value = reason
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            HandleError(sql, ex)
        End Try
        Return False
    End Function

    Public Function FirstRecord() As Boolean
        Return Find("ID=(SELECT MIN(ID) FROM hrs_ActingEmployeeAssignments)")
    End Function

    Public Function LastRecord() As Boolean
        Return Find("ID=(SELECT MAX(ID) FROM hrs_ActingEmployeeAssignments)")
    End Function

    Public Function NextRecord() As Boolean
        Return Find("ID=(SELECT MIN(ID) FROM hrs_ActingEmployeeAssignments WHERE ID>" & ID & ")")
    End Function

    Public Function PreviousRecord() As Boolean
        Return Find("ID=(SELECT MAX(ID) FROM hrs_ActingEmployeeAssignments WHERE ID<" & ID & ")")
    End Function

    Public Function Clear() As Boolean
        ID = 0
        Code = ""
        OriginalEmployeeID = 0
        ActingEmployeeID = 0
        EffectiveFrom = Nothing
        EffectiveTo = Nothing
        Reason = ""
        Remarks = ""
        RegUserID = 0
        RegDate = Nothing
        CancelUserID = 0
        CancelDate = Nothing
        CancelReason = ""
        Return True
    End Function

    Private Function ExecuteCommand(ByVal sql As String, ByVal isUpdate As Boolean) As Boolean
        Try
            Using connection As New SqlClient.SqlConnection(mConnectionString)
                Using command As New SqlClient.SqlCommand(sql, connection)
                    SetParameters(command, Not isUpdate)
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            HandleError(sql, ex)
        End Try
        Return False
    End Function

    Private Sub SetParameters(ByVal command As SqlClient.SqlCommand, ByVal includeCode As Boolean)
        If includeCode Then command.Parameters.Add("@Code", SqlDbType.VarChar, 30).Value = Code
        command.Parameters.Add("@OriginalEmployeeID", SqlDbType.Int).Value = OriginalEmployeeID
        command.Parameters.Add("@ActingEmployeeID", SqlDbType.Int).Value = ActingEmployeeID
        command.Parameters.Add("@EffectiveFrom", SqlDbType.DateTime).Value = EffectiveFrom
        command.Parameters.Add("@EffectiveTo", SqlDbType.DateTime).Value = EffectiveTo
        command.Parameters.Add("@Reason", SqlDbType.NVarChar, 500).Value = If(Reason, "")
        command.Parameters.Add("@Remarks", SqlDbType.NVarChar, 1000).Value = If(Remarks, "")
        If includeCode Then command.Parameters.Add("@RegUserID", SqlDbType.Int).Value = Me.mDataBaseUserRelatedID
    End Sub

    Private Sub LoadRow(ByVal row As DataRow)
        ID = Convert.ToInt32(row("ID"))
        Code = Convert.ToString(row("Code"))
        OriginalEmployeeID = Convert.ToInt32(row("OriginalEmployeeID"))
        ActingEmployeeID = Convert.ToInt32(row("ActingEmployeeID"))
        EffectiveFrom = Convert.ToDateTime(row("EffectiveFrom"))
        EffectiveTo = Convert.ToDateTime(row("EffectiveTo"))
        Reason = If(IsDBNull(row("Reason")), "", Convert.ToString(row("Reason")))
        Remarks = If(IsDBNull(row("Remarks")), "", Convert.ToString(row("Remarks")))
        RegUserID = If(IsDBNull(row("RegUserID")), 0, Convert.ToInt32(row("RegUserID")))
        RegDate = If(IsDBNull(row("RegDate")), Nothing, Convert.ToDateTime(row("RegDate")))
        CancelUserID = If(IsDBNull(row("CancelUserID")), 0, Convert.ToInt32(row("CancelUserID")))
        CancelDate = If(IsDBNull(row("CancelDate")), Nothing, Convert.ToDateTime(row("CancelDate")))
        CancelReason = If(IsDBNull(row("CancelReason")), "", Convert.ToString(row("CancelReason")))
    End Sub

    Private Sub HandleError(ByVal sql As String, ByVal ex As Exception)
        mPage.Session.Add("ErrorValue", ex)
        mErrorHandler.RecordExceptions_DataBase(sql, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
        mPage.Response.Redirect("ErrorPage.aspx")
    End Sub
End Class
