
'=========================================================================
'Created by : DataOcean
'Date : 04/04/2026
'                   Class: RequestTypes
'                   Table: SS_RequestTypes
'=========================================================================
Imports Venus.Application.SystemFiles.System
Public Class ClsSS_RequestTypes
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        With Me
            mTable = " SS_RequestTypes "
            .mInsertParameter = " RequestID,RequestCode,RequestArbName,RequestEngName,NotActive,NoOfTimes,TimesPeriodPerMonth,AutoSerialAttach,RequiredAttach,IsPaidWithSalary"
            .mInsertParameterValues = " @RequestID,@RequestCode,@RequestArbName,@RequestEngName,@NotActive,@NoOfTimes,@TimesPeriodPerMonth,@AutoSerialAttach,@RequiredAttach,@IsPaidWithSalary"
            .mUpdateParameter = " RequestID=@RequestID,RequestCode=@RequestCode,RequestArbName=@RequestArbName,RequestEngName=@RequestEngName,NotActive=@NotActive,NoOfTimes=@NoOfTimes,TimesPeriodPerMonth=@TimesPeriodPerMonth,AutoSerialAttach=@AutoSerialAttach,RequiredAttach=@RequiredAttach,IsPaidWithSalary=@IsPaidWithSalary "
            .mSelectCommand = " Select * From  " & mTable
            .mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
            .mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
            .mDeleteCommand = " Update " & mTable & " Set NotActive=1"
        End With
    End Sub

#End Region

#Region "Private Members"

    Private mRequestID As Object
    Private mRequestCode As String
    Private mRequestArbName As String
    Private mRequestEngName As String
    Private mNotActive As Object
    Private mNoOfTimes As Object
    Private mTimesPeriodPerMonth As Object
    Private mAutoSerialAttach As Object
    Private mRequiredAttach As Object
    Private mIsPaidWithSalary As Object

#End Region

#Region "Public property"

    Public Property RequestID() As Object
        Get
            Return mRequestID
        End Get
        Set(ByVal Value As Object)
            mRequestID = Value
        End Set
    End Property

    Public Property RequestCode() As String
        Get
            Return mRequestCode
        End Get
        Set(ByVal Value As String)
            mRequestCode = Value
        End Set
    End Property

    Public Property RequestArbName() As String
        Get
            Return mRequestArbName
        End Get
        Set(ByVal Value As String)
            mRequestArbName = Value
        End Set
    End Property

    Public Property RequestEngName() As String
        Get
            Return mRequestEngName
        End Get
        Set(ByVal Value As String)
            mRequestEngName = Value
        End Set
    End Property

    Public Property NotActive() As Object
        Get
            Return mNotActive
        End Get
        Set(ByVal Value As Object)
            mNotActive = Value
        End Set
    End Property

    Public Property NoOfTimes() As Object
        Get
            Return mNoOfTimes
        End Get
        Set(ByVal Value As Object)
            mNoOfTimes = Value
        End Set
    End Property

    Public Property TimesPeriodPerMonth() As Object
        Get
            Return mTimesPeriodPerMonth
        End Get
        Set(ByVal Value As Object)
            mTimesPeriodPerMonth = Value
        End Set
    End Property

    Public Property AutoSerialAttach() As Object
        Get
            Return mAutoSerialAttach
        End Get
        Set(ByVal Value As Object)
            mAutoSerialAttach = Value
        End Set
    End Property

    Public Property RequiredAttach() As Object
        Get
            Return mRequiredAttach
        End Get
        Set(ByVal Value As Object)
            mRequiredAttach = Value
        End Set
    End Property

    Public Property IsPaidWithSalary() As Object
        Get
            Return mIsPaidWithSalary
        End Get
        Set(ByVal Value As Object)
            mIsPaidWithSalary = Value
        End Set
    End Property

#End Region

#Region "Public Function"

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By RequestCode "
            End If

            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(RequestID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(RequestID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            StrSelectCommand = StrSelectCommand & orderByStr

            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
            Else
                Clear()
            End If
            If mRequestID > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Try
            Dim StrSqlCommand As String
            Dim Value As Integer
            StrSqlCommand = "Select RequestID From SS_RequestTypes Where " & Filter
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrSqlCommand
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            CLsWebHandlar.Add2History(mConnectionString, mRequestID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function Clear() As Boolean
        Try
            mRequestID = 0
            mRequestCode = String.Empty
            mRequestArbName = String.Empty
            mRequestEngName = String.Empty
            mNotActive = Nothing
            mNoOfTimes = Nothing
            mTimesPeriodPerMonth = Nothing
            mAutoSerialAttach = Nothing
            mRequiredAttach = Nothing
            mIsPaidWithSalary = Nothing

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(RequestID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY RequestCode ASC"
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
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(RequestID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY RequestCode DESC"
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
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE RequestCode >'" & mRequestCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(RequestID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY RequestCode ASC"
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
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE RequestCode < '" & mRequestCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(RequestID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY RequestCode DESC"
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

    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(NotActive,0)=0 And IsNull(dbo.hrs_GetRecordViewStatus(RequestID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By RequestEngName ", "  Where IsNull(NotActive,0)=0 And IsNull(dbo.hrs_GetRecordViewStatus(RequestID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  Order By RequestEngName ")
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
                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "RequestEngName/RequestArbName")), SqlDbType.VarChar)
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "RequestArbName/RequestEngName")), SqlDbType.VarChar)
                End If
                Item.Value = ObjDataRow("RequestID")
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

            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(NotActive,0)=0 And IsNull(dbo.hrs_GetRecordViewStatus(RequestID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 "
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "RequestEngName/RequestArbName")), SqlDbType.VarChar)
                If (Item.DisplayText.Trim = "") Then
                    Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "RequestArbName/RequestEngName")), SqlDbType.VarChar)
                End If
                Item.DataValue = ObjDataRow("RequestID")
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

#Region "Class Private Function"

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mRequestID = mDataHandler.DataValue_Out(.Item("RequestID"), SqlDbType.Int, True)
                mRequestCode = mDataHandler.DataValue_Out(.Item("RequestCode"), SqlDbType.VarChar)
                mRequestArbName = mDataHandler.DataValue_Out(.Item("RequestArbName"), SqlDbType.VarChar)
                mRequestEngName = mDataHandler.DataValue_Out(.Item("RequestEngName"), SqlDbType.VarChar)
                mNotActive = mDataHandler.DataValue_Out(.Item("NotActive"), SqlDbType.Bit)
                mNoOfTimes = mDataHandler.DataValue_Out(.Item("NoOfTimes"), SqlDbType.Int)
                mTimesPeriodPerMonth = mDataHandler.DataValue_Out(.Item("TimesPeriodPerMonth"), SqlDbType.Int)
                mAutoSerialAttach = mDataHandler.DataValue_Out(.Item("AutoSerialAttach"), SqlDbType.Bit)
                mRequiredAttach = mDataHandler.DataValue_Out(.Item("RequiredAttach"), SqlDbType.Bit)
                mIsPaidWithSalary = mDataHandler.DataValue_Out(.Item("IsPaidWithSalary"), SqlDbType.Bit)

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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RequestID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRequestID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RequestCode", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRequestCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RequestArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRequestArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RequestEngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRequestEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NotActive", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mNotActive, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NoOfTimes", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mNoOfTimes, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TimesPeriodPerMonth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTimesPeriodPerMonth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AutoSerialAttach", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mAutoSerialAttach, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RequiredAttach", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mRequiredAttach, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsPaidWithSalary", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsPaidWithSalary, SqlDbType.Bit)

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
