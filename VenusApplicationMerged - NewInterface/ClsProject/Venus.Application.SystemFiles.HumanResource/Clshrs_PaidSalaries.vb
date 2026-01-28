Imports Venus.Application.SystemFiles.System
Public Class Clshrs_PaidSalaries
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_PaidSalaries "
        mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
    End Sub
#End Region
#Region "Private Members"
    Private mCode As String
    Private mID As Integer
    Private mEmpEngName As String
    Private mFullName As String
    Private mPeriodCode As String
    Private mPeriodID As Integer
    Private mPaidSalary As Decimal

#End Region
#Region "Public property"
    Public Property Code() As String
        Get
            Return mCode
        End Get
        Set(ByVal Value As String)
            mCode = Value
        End Set
    End Property
    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal Value As Integer)
            mID = Value
        End Set
    End Property
    Public Property EmpEngName() As String
        Get
            Return mEmpEngName
        End Get
        Set(ByVal Value As String)
            mEmpEngName = Value
        End Set
    End Property
    Public Property FullName() As String
        Get
            Return mFullName
        End Get
        Set(ByVal Value As String)
            mFullName = Value
        End Set
    End Property
    Public Property mPeriodCod() As String
        Get
            Return mPeriodCode
        End Get
        Set(ByVal Value As String)
            mPeriodCode = Value
        End Set
    End Property
    Public Property PeriodID() As Integer
        Get
            Return mPeriodID
        End Get
        Set(ByVal Value As Integer)
            mPeriodID = Value
        End Set
    End Property
    Public Property PaidSalary() As Decimal
        Get
            Return mPaidSalary
        End Get
        Set(ByVal Value As Decimal)
            mPaidSalary = Value
        End Set
    End Property

#End Region
#Region "Public Function"
    '========================================================================
    'ProcedureName  :  GetList
    'Project        :  Fisalia Module
    'Description    :  Fill Value List with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean  
    'Date Created   : 08/02/2015
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column
    '========================================================================
    Public Function GetList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' AND IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()
            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                Item.DisplayText = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/ [ إختر أحد الإختيارات ] ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If
            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Find 
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  DataOcean   
    'Date Created   :08/02/2015
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' AND IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
            Else
                'Clear()
            End If
            If mCode > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region
#Region "Class Private Function"
    '========================================================================
    'ProcedureName  :  GetParameter 
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :08/02/2015
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = [Shared].DataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCode = [Shared].DataHandler.DataValue_Out(.Item("Code"), SqlDbType.Int, True)
                mEmpEngName = [Shared].DataHandler.DataValue_Out(.Item("EmpEngName"), SqlDbType.VarChar)
                mFullName = [Shared].DataHandler.DataValue_Out(.Item("FullName"), SqlDbType.VarChar)
                mPeriodCode = [Shared].DataHandler.DataValue_Out(.Item("PeriodCode"), SqlDbType.VarChar)
                mPeriodID = [Shared].DataHandler.DataValue_Out(.Item("PeriodID"), SqlDbType.Int, True)
                mPaidSalary = [Shared].DataHandler.DataValue_Out(.Item("PaidSalary"), SqlDbType.Money)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  SetParameter
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Assign parameters of sql command  with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   : 08/02/2015 12:21:39
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mCode, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmpEngName", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mEmpEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FullName", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mFullName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PeriodCode", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mPeriodCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PeriodID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mPeriodID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PaidSalary", SqlDbType.Money)).Value = [Shared].DataHandler.DataValue_In(mPaidSalary, SqlDbType.Money)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region
#Region "Navigation Functions"
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   : 08/02/2015 12:21:39
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    'Public Function FirstRecord(Optional ByVal Filter As String = "") As Boolean
    '    Dim StrSelectCommand As String
    '    Try
    '        StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code ASC"
    '        mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
    '        mDataSet = New DataSet
    '        mSqlDataAdapter.Fill(mDataSet)
    '        If mDataHandler.CheckValidDataObject(mDataSet) Then
    '            GetParameter(mDataSet)
    '            Return True
    '        Else
    '            'Clear()
    '        End If
    '    Catch ex As Exception
    '        mPage.Session.Add("ErrorValue", ex)
    '        mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
    '        mPage.Response.Redirect("ErrorPage.aspx")
    '    End Try
    'End Function

    'Public Function LastRecord(Optional ByVal Filter As String = "") As Boolean
    '    Dim StrSelectCommand As String
    '    Try
    '        StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code DESC"
    '        mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
    '        mDataSet = New DataSet
    '        mSqlDataAdapter.Fill(mDataSet)
    '        If mDataHandler.CheckValidDataObject(mDataSet) Then
    '            GetParameter(mDataSet)
    '            Return True
    '        Else
    '            'Clear()
    '        End If
    '    Catch ex As Exception
    '        mPage.Session.Add("ErrorValue", ex)
    '        mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
    '        mPage.Response.Redirect("ErrorPage.aspx")
    '    End Try
    'End Function

    'Public Function NextRecord(Optional ByVal Filter As String = "") As Boolean
    '    Dim StrSelectCommand As String
    '    Try
    '        StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code >'" & mCode & "' And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code ASC"
    '        mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
    '        mDataSet = New DataSet
    '        mSqlDataAdapter.Fill(mDataSet)
    '        If mDataHandler.CheckValidDataObject(mDataSet) Then
    '            GetParameter(mDataSet)
    '            Return True
    '        Else
    '            'Clear()
    '        End If
    '    Catch ex As Exception
    '        mPage.Session.Add("ErrorValue", ex)
    '        mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
    '        mPage.Response.Redirect("ErrorPage.aspx")
    '    End Try
    'End Function

    'Public Function previousRecord(Optional ByVal Filter As String = "") As Boolean
    '    Dim StrSelectCommand As String
    '    Try
    '        StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code <'" & mCode & "' And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code DESC"
    '        mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
    '        mDataSet = New DataSet
    '        mSqlDataAdapter.Fill(mDataSet)
    '        If mDataHandler.CheckValidDataObject(mDataSet) Then
    '            GetParameter(mDataSet)
    '            Return True
    '        Else
    '            'Clear()
    '        End If
    '    Catch ex As Exception
    '        mPage.Session.Add("ErrorValue", ex)
    '        mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
    '        mPage.Response.Redirect("ErrorPage.aspx")
    '    End Try
    'End Function
#End Region
#Region "Class Destructors"
    Public Sub finalized()
        mDataSet.Dispose()
    End Sub
#End Region
End Class
