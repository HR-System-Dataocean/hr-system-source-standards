Imports System.Data.SqlClient
Imports Venus.Application.SystemFiles.System

Public Class ClsHrs_AppraisalCreate
    Inherits ClsDataAcessLayer
    Public Sub New(page As Web.UI.Page)
        MyBase.New(page)
        mTable = "APP_Appraisals"

        mInsertParameter = "Code, ArabName, EngName, AppraisalTypeID, FromDate, ToDate, ByDepartment, ByPosition, BranchID, SectorID, DepartmentID, UnitID, PositionID, EmployeeID,Notes,RegUserID, RegDate, CancelDate"
        mInsertParameterValues = "@Code, @ArabName, @EngName, @AppraisalTypeID, @FromDate, @ToDate, @ByDepartment, @ByPosition, @BranchID, @SectorID, @DepartmentID, @UnitID, @PositionID, @EmployeeID,@Notes, @RegUserID, @RegDate, @CancelDate"

        mUpdateParameter = "Code=@Code, ArabName=@ArabName, EngName=@EngName, AppraisalTypeID=@AppraisalTypeID, " &
                      "FromDate=@FromDate, ToDate=@ToDate, ByDepartment=@ByDepartment, ByPosition=@ByPosition, " &
                      "BranchID=@BranchID, SectorID=@SectorID, DepartmentID=@DepartmentID, UnitID=@UnitID, " &
                      "PositionID=@PositionID, EmployeeID=@EmployeeID,Notes=@Notes, RegUserID=@RegUserID, RegDate=@RegDate"

        mSelectCommand = "SELECT * FROM " & mTable & " "
        mInsertCommand = "INSERT INTO " & mTable & "(" & mInsertParameter & ") VALUES(" & mInsertParameterValues & ")"
        mUpdateCommand = "UPDATE " & mTable & " SET " & mUpdateParameter & " "

        mDeleteCommand = "UPDATE " & mTable & " SET CancelDate=GETDATE()  "
    End Sub
#Region "Private Members"
    Private mID As Integer
    Private mCode As String
    Private mArabName As String
    Private mEngName As String
    Private mAppraisalTypeID As Integer
    Private mFromDate As DateTime
    Private mToDate As DateTime
    Private mByDepartment As Boolean
    Private mByPosition As Boolean
    Private mBranchID As Nullable(Of Integer)
    Private mSectorID As Nullable(Of Integer)
    Private mDepartmentID As Nullable(Of Integer)
    Private mUnitID As Nullable(Of Integer)
    Private mPositionID As Nullable(Of Integer)
    Private mEmployeeID As Nullable(Of Integer)
    Private mNotes As String
    Private mRegUserID As Integer
    Private mRegDate As DateTime
    Private mCancelDate As Nullable(Of DateTime)
#End Region
#Region "Public Properties"
    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal Value As Integer)
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

    Public Property ArabName() As String
        Get
            Return mArabName
        End Get
        Set(ByVal Value As String)
            mArabName = Value
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

    Public Property AppraisalTypeID() As Integer
        Get
            Return mAppraisalTypeID
        End Get
        Set(ByVal Value As Integer)
            mAppraisalTypeID = Value
        End Set
    End Property

    Public Property FromDate() As DateTime
        Get
            Return mFromDate
        End Get
        Set(ByVal Value As DateTime)
            mFromDate = Value
        End Set
    End Property

    Public Property ToDate() As DateTime
        Get
            Return mToDate
        End Get
        Set(ByVal Value As DateTime)
            mToDate = Value
        End Set
    End Property

    Public Property ByDepartment() As Boolean
        Get
            Return mByDepartment
        End Get
        Set(ByVal Value As Boolean)
            mByDepartment = Value
        End Set
    End Property

    Public Property ByPosition() As Boolean
        Get
            Return mByPosition
        End Get
        Set(ByVal Value As Boolean)
            mByPosition = Value
        End Set
    End Property

    Public Property BranchID() As Integer?
        Get
            Return mBranchID
        End Get
        Set(ByVal Value As Integer?)
            mBranchID = Value
        End Set
    End Property

    Public Property SectorID() As Integer?
        Get
            Return mSectorID
        End Get
        Set(ByVal Value As Integer?)
            mSectorID = Value
        End Set
    End Property

    Public Property DepartmentID() As Integer?
        Get
            Return mDepartmentID
        End Get
        Set(ByVal Value As Integer?)
            mDepartmentID = Value
        End Set
    End Property

    Public Property UnitID() As Integer?
        Get
            Return mUnitID
        End Get
        Set(ByVal Value As Integer?)
            mUnitID = Value
        End Set
    End Property

    Public Property PositionID() As Integer?
        Get
            Return mPositionID
        End Get
        Set(ByVal Value As Integer?)
            mPositionID = Value
        End Set
    End Property

    Public Property EmployeeID() As Integer?
        Get
            Return mEmployeeID
        End Get
        Set(ByVal Value As Integer?)
            mEmployeeID = Value
        End Set
    End Property
    Public Property Notes() As String
        Get
            Return mNotes
        End Get
        Set(ByVal Value As String)
            mNotes = Value
        End Set
    End Property

    Public Property RegUserID() As Integer
        Get
            Return mRegUserID
        End Get
        Set(ByVal Value As Integer)
            mRegUserID = Value
        End Set
    End Property

    Public Property RegDate() As DateTime
        Get
            Return mRegDate
        End Get
        Set(ByVal Value As DateTime)
            mRegDate = Value
        End Set
    End Property

    Public Property CancelDate() As DateTime?
        Get
            Return mCancelDate
        End Get
        Set(ByVal Value As DateTime?)
            mCancelDate = Value
        End Set
    End Property
#End Region
#Region "Public Functions"
    '========================================================================
    'ProcedureName  :  GetList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill Value List with English name column and its value with ID column
    '========================================================================
    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrCommandString = "Select * From " & Me.mTable & " Where CancelDate IS NULL And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArabName")), SqlDbType.VarChar)
                If (Item.DisplayText.Trim = "") Then
                    Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArabName/EngName")), SqlDbType.VarChar)
                End If
                Item.DataValue = ObjDataRow("ID")
                DdlValues.ValueListItems.Add(Item)
            Next

            Return DdlValues.ValueListItems.Count > 0

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetDropDown
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill DropDownList with English name column and its value with ID column
    '========================================================================
    Public Function GetDropDown(ByRef DdlValues As Global.System.Web.UI.WebControls.DropDownList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrCommandString = "Select * From " & Me.mTable & " Where CancelDate IS NULL And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.Items.Clear()

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArabName")), SqlDbType.VarChar)
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArabName/EngName")), SqlDbType.VarChar)
                End If
                Item.Value = ObjDataRow("ID")
                DdlValues.Items.Add(Item)
            Next

            Return DdlValues.Items.Count > 0

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetDropDownList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill DropDownList with optional null item
    '========================================================================
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where CancelDate IS NULL And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By EngName ", " Where CancelDate IS NULL And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 Order By EngName")

            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")
                Item.Value = "0"
                DdlValues.Items.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArabName")), SqlDbType.VarChar)
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArabName/EngName")), SqlDbType.VarChar)
                End If
                Item.Value = ObjDataRow("ID")
                DdlValues.Items.Add(Item)
            Next

            Return DdlValues.Items.Count > 0

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Find 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find all rows that match criteria or filter
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            Dim orderByStr As String = If(Filter.ToLower.IndexOf("order by") = -1, " Order By Code ", "")

            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, "   Where CancelDate IS NULL And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1")
            StrSelectCommand &= orderByStr

            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)

            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
                Return False
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  SaveUpdate
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save Or Update Row that match with critera
    '========================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "Select ID From APP_Appraisals Where " & Filter
            Dim Value As Integer = CInt(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strSQL))

            If Value > 0 Then
                Return Update(Filter)
            Else
                Return Save()
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Save
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save new record
    '========================================================================
    Public Function Save() As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand(mInsertCommand, New SqlClient.SqlConnection(mConnectionString))
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

    '========================================================================
    'ProcedureName  :  Update
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  update row that match with critera
    '========================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Try
            StrUpdateCommand = mUpdateCommand & " Where " & Filter
            mSqlCommand = New SqlClient.SqlCommand(StrUpdateCommand, New SqlClient.SqlConnection(mConnectionString))
            SetParameter(mSqlCommand)

            ' Add to history if needed
            Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")

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

    '========================================================================
    'ProcedureName  :  Delete
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Delete record that match critera
    '========================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        'Dim StrDeleteCommand As String
        'Try
        '    StrDeleteCommand = mDeleteCommand & " Where " & Filter
        '    mSqlCommand = New SqlClient.SqlCommand(StrDeleteCommand, New SqlClient.SqlConnection(mConnectionString))
        '    mSqlCommand.Connection.Open()
        '    mSqlCommand.ExecuteNonQuery()
        '    mSqlCommand.Connection.Close()
        '    Return True

        'Catch ex As Exception
        '    mPage.Session.Add("ErrorValue", ex)
        '    mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
        '    mPage.Response.Redirect("ErrorPage.aspx")
        'End Try

        Dim StrDeleteCommand As String = String.Empty
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            ' SetParameter(mSqlCommand)
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
    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mCode = String.Empty
            mArabName = String.Empty
            mEngName = String.Empty
            mAppraisalTypeID = 0
            mFromDate = DateTime.MinValue
            mToDate = DateTime.MinValue
            mByDepartment = False
            mByPosition = False
            mBranchID = Nothing
            mSectorID = Nothing
            mDepartmentID = Nothing
            mUnitID = Nothing
            mPositionID = Nothing
            mEmployeeID = Nothing
            mNotes = String.Empty
            mRegUserID = 0
            mRegDate = DateTime.MinValue
            mCancelDate = Nothing
            Return True

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function



#Region "Navigation Functions"
    '========================================================================
    'ProcedureName  :  FirstRecord, LastRecord, NextRecord, PreviousRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Navigate between records
    '========================================================================
    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ORDER BY Code ASC"
        Return Navigate(StrSelectCommand)
    End Function

    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ORDER BY Code DESC"
        Return Navigate(StrSelectCommand)
    End Function

    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String = "SELECT TOP 1 * FROM " & mTable & " WHERE Code >'" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ORDER BY Code ASC"
        Return Navigate(StrSelectCommand)
    End Function

    Public Function PreviousRecord() As Boolean
        Dim StrSelectCommand As String = "SELECT TOP 1 * FROM " & mTable & " WHERE Code < '" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ORDER BY Code DESC"
        Return Navigate(StrSelectCommand)
    End Function

    Private Function Navigate(ByVal StrSelectCommand As String) As Boolean
        Try
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)

            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
                Return False
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  CheckDiff
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Check if there are any difference between DataSet and Class's Properties
    '========================================================================
    Public Function CheckDiff(ByVal ClassObj As Object, ByVal DSData As DataSet, ByVal Filter As String) As Boolean
        Try
            Dim myPropertyInfo As Reflection.PropertyInfo() = CType(ClassObj.GetType, Type).GetProperties()

            For Each prop As Reflection.PropertyInfo In myPropertyInfo
                For Each col As DataColumn In DSData.Tables(0).Columns
                    If prop.Name.Equals(col.ColumnName, StringComparison.OrdinalIgnoreCase) Then
                        Dim classValue = prop.GetValue(ClassObj, Nothing)
                        Dim dsValue = If(DSData.Tables(0).Rows(0)(col) Is DBNull.Value, Nothing, DSData.Tables(0).Rows(0)(col))

                        If Not Object.Equals(classValue, dsValue) Then
                            Return True
                        End If
                    End If
                Next
            Next

            Return False

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region

    '========================================================================
    'ProcedureName  :  HandleError
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Centralized error handling
    '========================================================================
    Private Sub HandleError(ByVal ex As Exception, ByVal commandText As String)
        mPage.Session.Add("ErrorValue", ex)
        mErrorHandler.RecordExceptions_DataBase(commandText, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
        mPage.Response.Redirect("ErrorPage.aspx")
    End Sub

#End Region
#Region "Class Private Functions"
    '========================================================================
    'ProcedureName  :  GetParameter
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Assign Result of Dataset to private attributes
    '========================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mArabName = mDataHandler.DataValue_Out(.Item("ArabName"), SqlDbType.VarChar)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mAppraisalTypeID = mDataHandler.DataValue_Out(.Item("AppraisalTypeID"), SqlDbType.Int, True)
                mFromDate = mDataHandler.DataValue_Out(.Item("FromDate"), SqlDbType.DateTime)
                mToDate = mDataHandler.DataValue_Out(.Item("ToDate"), SqlDbType.DateTime)

                mEmployeeID = mDataHandler.DataValue_Out(.Item("EmployeeID"), SqlDbType.Int, True)
                mNotes = mDataHandler.DataValue_Out(.Item("Notes"), SqlDbType.VarChar, True)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
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

    '========================================================================
    'ProcedureName  :  SetParameter
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Assign parameters of sql command with private attributes values
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            ' Basic Fields
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArabName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArabName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AppraisalTypeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mAppraisalTypeID, SqlDbType.Int, True)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FromDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mFromDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ToDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mToDate, SqlDbType.DateTime)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ByDepartment", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mByDepartment, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ByPosition", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mByPosition, SqlDbType.Bit)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BranchID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBranchID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SectorID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mSectorID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DepartmentID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mDepartmentID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@UnitID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mUnitID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PositionID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPositionID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Notes", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mNotes, SqlDbType.VarChar, True)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mRegDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CancelDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mCancelDate, SqlDbType.DateTime)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mID, SqlDbType.Int, True)

            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
            Return False
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  FixNull
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Handle null values from database
    '========================================================================
    Private Function FixNull(ByVal obj As Object, ByVal DataColumn As Global.System.Data.DataColumn) As Object
        Try
            If obj Is DBNull.Value OrElse obj Is Nothing Then
                Select Case DataColumn.DataType.Name.ToUpper
                    Case "DECIMAL", "DOUBLE"
                        Return 0.0
                    Case "INT32", "INT16", "INT64"
                        Return 0
                    Case "DATETIME"
                        Return Nothing
                    Case "BOOLEAN"
                        Return False
                    Case "STRING"
                        Return String.Empty
                    Case Else
                        Return Nothing
                End Select
            Else
                Return obj
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


#End Region
#Region "Class Destructors"
    Public Sub finalized()
        mDataSet.Dispose()
    End Sub


#End Region
End Class
