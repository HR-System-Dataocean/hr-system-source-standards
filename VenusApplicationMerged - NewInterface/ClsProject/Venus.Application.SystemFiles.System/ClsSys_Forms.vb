Public Class ClsFormsBasic
    Inherits ClsMain
#Region "Constant"
    Const CID = 0
    Const CName = 1
    Const CField = 2
    Const CEngDesc = 3
    Const CArbDesc = 4
    Const CCompuslary = 5
    Const CIsArabic = 6
    Const CFormat = 7
    Const CArabicFormat = 8
    Const CTooltip = 9
    Const CArabicTooltip = 10
    Const CMaxLenght = 11
    Const CIsNumeric = 12
    Const CIsHide = 13
    Const CFocusOnStart = 14
    Const CRank = 15
    Const CMinValue = 16
    Const CMaxValue = 17
    Const CSearchID = 18
#End Region
#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " sys_Forms "
        mInsertParameter = "" & _
          "Code," & _
          "EngName," & _
          "ArbName," & _
          "ArbName4S," & _
          "EngDescription," & _
          "ArbDescription," & _
          "Rank," & _
          "ModuleID," & _
          "SearchFormID," & _
          "Height," & _
          "Width," & _
          "Remarks," & _
          "RegUserID," & _
          "RegComputerID," & _
          "Layout," & _
          "LinkTarget," & _
          "LinkUrl," & _
          "ImageUrl," & _
          "MainID"
        mInsertParameterValues = "" & _
          " @Code," & _
          " @EngName," & _
          " @ArbName," & _
          " @ArbName4S," & _
          " @EngDescription," & _
          " @ArbDescription," & _
          " @Rank," & _
          " @ModuleID," & _
          " @SearchFormID," & _
          " @Height," & _
          " @Width," & _
          " @Remarks," & _
          " @RegUserID," & _
          " @RegComputerID," & _
          " @Layout," & _
          " @LinkTarget," & _
          " @LinkUrl," & _
          " @ImageUrl," & _
          " @MainID"
        mUpdateParameter = "" & _
          "Code=@Code," & _
          "EngName=@EngName," & _
          "ArbName=@ArbName," & _
          "ArbName4S=@ArbName4S," & _
          "EngDescription=@EngDescription," & _
          "ArbDescription=@ArbDescription," & _
          "Rank=@Rank," & _
          "ModuleID=@ModuleID," & _
          "SearchFormID=@SearchFormID," & _
          "Height=@Height," & _
          "Width=@Width," & _
          "Remarks=@Remarks," & _
          "Layout=@Layout," & _
          "LinkTarget=@LinkTarget," & _
          "LinkUrl=@LinkUrl," & _
          "ImageUrl=@ImageUrl," & _
          "MainID=@MainID"
        mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Integer
    Private mCode As String
    Private mEngName As String
    Private mArbName As String
    Private mArbName4S As String
    Private mEngDescription As String
    Private mArbDescription As String
    Private mRank As Integer
    Private mModuleID As Integer
    Private mSearchFormID As Integer
    Private mHeight As Integer
    Private mWidth As Integer
    Private mRemarks As String
    Private mRegUserID As Integer
    Private mRegComputerID As Integer
    Private mRegDate As DateTime
    Private mCancelDate As DateTime
    Private mLayout As Integer
    Private mLinkTarget As String
    Private mLinkUrl As String
    Private mImageUrl As String
    Private mMainID As Integer

#End Region
#Region "Public property"
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
    Public Property EngDescription() As String
        Get
            Return mEngDescription
        End Get
        Set(ByVal Value As String)
            mEngDescription = Value
        End Set
    End Property
    Public Property ArbDescription() As String
        Get
            Return mArbDescription
        End Get
        Set(ByVal Value As String)
            mArbDescription = Value
        End Set
    End Property
    Public Property Rank() As Integer
        Get
            Return mRank
        End Get
        Set(ByVal Value As Integer)
            mRank = Value
        End Set
    End Property
    Public Property ModuleID() As Integer
        Get
            Return mModuleID
        End Get
        Set(ByVal Value As Integer)
            mModuleID = Value
        End Set
    End Property
    Public Property SearchFormID() As Integer
        Get
            Return mSearchFormID
        End Get
        Set(ByVal Value As Integer)
            mSearchFormID = Value
        End Set
    End Property
    Public Property Height() As Integer
        Get
            Return mHeight
        End Get
        Set(ByVal Value As Integer)
            mHeight = Value
        End Set
    End Property
    Public Property Width() As Integer
        Get
            Return mWidth
        End Get
        Set(ByVal Value As Integer)
            mWidth = Value
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
    Public Property RegUserID() As Integer
        Get
            Return mRegUserID
        End Get
        Set(ByVal Value As Integer)
            mRegUserID = Value
        End Set
    End Property
    Public Property RegComputerID() As Integer
        Get
            Return mRegComputerID
        End Get
        Set(ByVal Value As Integer)
            mRegComputerID = Value
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
    Public Property CancelDate() As DateTime
        Get
            Return mCancelDate
        End Get
        Set(ByVal Value As DateTime)
            mCancelDate = Value
        End Set
    End Property
    Public Property Layout() As Integer
        Get
            Return mLayout
        End Get
        Set(ByVal Value As Integer)
            mLayout = Value
        End Set
    End Property
    Public Property LinkTarget() As String
        Get
            Return mLinkTarget
        End Get
        Set(ByVal Value As String)
            mLinkTarget = Value
        End Set
    End Property
    Public Property LinkUrl() As String
        Get
            Return mLinkUrl
        End Get
        Set(ByVal Value As String)
            mLinkUrl = Value
        End Set
    End Property
    Public Property ImageUrl() As String
        Get
            Return mImageUrl
        End Get
        Set(ByVal Value As String)
            mImageUrl = Value
        End Set
    End Property
    Public Property MainID() As Integer
        Get
            Return mMainID
        End Get
        Set(ByVal Value As Integer)
            mMainID = Value
        End Set
    End Property
#End Region
#Region "Public Function"

    Public Function ReadParameters(ByVal IntFormID As Integer) As String
        Dim ObjDS As New DataSet()
        Dim StrSelectCommand As String = String.Empty
        Dim strQueryString As String = String.Empty
        Dim intIndex As Integer
        Try
            StrSelectCommand = " SELECT Name As ParamName ,Value As ParamValue  FROM sys_FormsParameters Where FormID = " & IntFormID
            ObjDS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, StrSelectCommand)
            If ObjDS.Tables(0).Rows.Count > 0 Then
                For Each row As Data.DataRow In ObjDS.Tables(0).Rows
                    strQueryString &= row.Item("ParamName") & "='" & IIf(IsDBNull(row.Item("ParamValue")), "''", row.Item("ParamValue")) & "'&"
                Next
                intIndex = strQueryString.LastIndexOf("&")
                strQueryString = strQueryString.Remove(intIndex, 1)
            End If
            Return strQueryString
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function GetFormParameters(ByVal IntFormID As Integer, ByRef ObjDS As Data.DataSet) As Boolean
        Dim StrSelectCommand As String
        Dim objNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrSelectCommand = " SELECT ID ,FormID,Name As ParamName ,Value As ParamValue  FROM sys_FormsParameters Where FormID = " & IntFormID
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            ObjDS = New DataSet
            mSqlDataAdapter.Fill(ObjDS)
            If mDataHandler.CheckValidDataObject(ObjDS) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Function SetFormParameters(ByRef DG As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal intFormID As Integer, ByVal DeleteAll As Boolean) As Boolean
        Try
            Dim strCmd As String = CONFIG_DATEFORMAT
            Dim strFormat As String
            Dim strArbFormat As String
            Dim IntRecID As Integer
            If DeleteAll Then
                strCmd &= " delete from sys_FormsParameters where FormID = " & intFormID & ";"
                ExecuteQuery(strCmd)
            End If
            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In DG.Rows
                If (row.Cells(2).Value <> Nothing) Then
                    strCmd = String.Empty
                    strCmd = " insert into sys_FormsParameters ([FormID],[Name],[Value]) VALUES( " & vbNewLine
                    strCmd &= intFormID & ",'" & _
                    row.Cells(2).Value & "','" & _
                    row.Cells(3).Value & "' )"
                    ExecuteQuery(strCmd)
                End If
            Next
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
            Return False
        End Try
    End Function
    Public Function SetFormParameter(ByVal intFormID As Integer, ByVal ParameterName As String, ByVal ParameterValue As String) As Boolean
        Try
            Dim strCmd As String = CONFIG_DATEFORMAT
            strCmd &= " insert into sys_FormsParameters ([FormID],[Name],[Value]) VALUES( " & _
                                    intFormID & ",'" & ParameterName & "','" & ParameterValue & "' ) "
            ExecuteQuery(strCmd)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
            Return False
        End Try
    End Function


    Private Function ExecuteQuery(ByVal strCmd As String) As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = strCmd
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
            Return False
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Find all columns from sys_forms table where filter and canceldate = null  
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class
    '       4-Return true if ID of Filteration >0 (Is Found)
    '==================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            '==================== Order By Modification [Start]
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By Code "
            End If
            'Filter = Filter.Replace("Õ", " AM ").Replace("ã", " PM ")
            '==================== Order By Modification [ End ]
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            '==================== Order By Modification [Start]
            StrSelectCommand = StrSelectCommand & orderByStr
            '==================== Order By Modification [ End ]
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Save or Update row 
    'Steps: 
    '       1-Execute sql statment to get ID from sys_forms where filter 
    '       2-Check if ID > 0 this mean that row is already exist in sys_forms  table 
    '       the make Update to this row
    '           IF ID =0 this mean that row is new row Then Insert the row in sys_forms  table
    '==================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Try
            Dim strSQL As String
            Dim Value As Integer
            strSQL = "Select ID From sys_forms Where " & Filter
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007

    'Description: Save New Row in sys_forms  table
    'Steps: 
    '       1-execute sqlstatment to insert new row in sys_forms  table

    '==================================================================
    Public Function Save() As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand & " ; Select SCOPE_IDENTITY()"
            SetParameter(mSqlCommand, "Save")
            mSqlCommand.Connection.Open()
            mID = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update existing Row in sys_forms  table where filter
    'Steps: 
    '       1-execute sqlstatment to Update existing row in sys_forms  table

    '==================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand, "Update")
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")

            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
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
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Delete existing Row in sys_forms  table where filter
    'Steps: 
    '       1-execute sqlstatment to Delete existing row in sys_forms  table

    '==================================================================
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Description: Clear all private members  of the class

    '==================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mCode = String.Empty
            mEngName = String.Empty
            mArbName = String.Empty
            mArbName4S = String.Empty
            mEngDescription = String.Empty
            mArbDescription = String.Empty
            mRank = 0
            mModuleID = 0
            mSearchFormID = 0
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mHeight = Nothing
            mWidth = Nothing

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Description:  find first row in sys_forms table
    'Steps: 
    '       1-execute sqlstatment to find first row in sys_forms  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Description:  find Last row in sys_forms  table
    'Steps: 
    '       1-execute sqlstatment to find last row in sys_forms  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Description:  find Next row in sys_forms  table
    'Steps: 
    '       1-execute sqlstatment to find Next row in sys_forms  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Description:  find previous row in sys_forms  table
    'Steps: 
    '       1-execute sqlstatment to find previous row in sys_forms  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : 
    'Date : 16/07/2007
    'Description:  
    'Steps: 
    '==================================================================
    Public Function FindUsersFormPermission(ByVal UserID As Integer, ByVal Code As String, ByVal EngName As String, ByVal ArbName As String, ByRef Ds As DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, "hrs_GetUsersFormsPermissions", UserID, Code, EngName, ArbName)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : 
    'Date : 16/07/2007
    'Description:  
    'Steps: 
    '==================================================================
    Public Function FindGroupsFormPermission(ByVal GroupID As Integer, ByVal Code As String, ByVal EngName As String, ByVal ArbName As String, ByRef Ds As DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, "hrs_GetGroupsFormsPermissions", GroupID, Code, EngName, ArbName)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : 
    'Date : 16/07/2007
    'Description:  
    'Steps: 
    '==================================================================
    Public Function FindUsersAvailableForms(ByVal UserID As String, ByRef Ddl As Web.UI.WebControls.DropDownList) As Boolean
        Dim ObjDs As New DataSet
        Dim ObjDr As DataRow
        Dim Dditem As Web.UI.WebControls.ListItem
        Try
            ObjDs = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, "hrs_GetUsersAvailableForms", UserID)
            If mDataHandler.CheckValidDataObject(ObjDs) Then
                For Each ObjDr In ObjDs.Tables(0).Rows
                    Dditem = New Web.UI.WebControls.ListItem
                    Dditem.Text = ObjDr.Item("Code")
                    Dditem.Value = ObjDr.Item("ID")
                    Ddl.Items.Add(Dditem)
                Next
            End If
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("hrs_GetAvailableUserForms,Parameter(UserID=" & UserID & ")", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : 
    'Date : 16/07/2007
    'Description:  
    'Steps: 
    '==================================================================
    Public Function FindGroupsAvailableForms(ByVal GroupID As String, ByRef Ddl As Web.UI.WebControls.DropDownList) As Boolean
        Dim ObjDs As New DataSet
        Dim ObjDr As DataRow
        Dim Dditem As Web.UI.WebControls.ListItem
        Try
            ObjDs = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, "hrs_GetGroupsAvailableForms", GroupID)
            If mDataHandler.CheckValidDataObject(ObjDs) Then
                For Each ObjDr In ObjDs.Tables(0).Rows
                    Dditem = New Web.UI.WebControls.ListItem
                    Dditem.Text = ObjDr.Item("Code")
                    Dditem.Value = ObjDr.Item("ID")
                    Ddl.Items.Add(Dditem)
                Next
            End If
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("hrs_GetGroupsAvailableForms,Parameter(UserID=" & GroupID & ")", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function SaveFormsControls(ByRef DG As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal intFormID As Integer, ByVal DeleteAll As Boolean) As Boolean
        Try
            Dim strCmd As String = CONFIG_DATEFORMAT
            If DeleteAll Then

                strCmd &= " delete from " & mTable & " where FormID = " & intFormID & ";"

            End If

            Dim bFirst As Boolean = True
            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In DG.Rows

                If (row.Cells(1).Value <> Nothing) Then
                    If (bFirst = True) Then
                        strCmd &= " insert into " & mTable & " ([FormID],[Name],[EngCaption],[ArbCaption],[Compulsory],[Format],[ArbFormat],[ToolTip],[ArbToolTip],[MaxLength],[IsNumeric],[IsHide],[FocusOnStartUp],[Rank],[MinValue],[MaxValue],[FieldID],[SearchID],[RegUserID],[RegComputerID],[IsArabic]) " & vbNewLine
                    End If
                    strCmd &= " " & IIf(bFirst, " select ", " union all select ") & _
                    " " & intFormID & ",'" & row.Cells(CName).Value & "','" & row.Cells(CEngDesc).Value & "','" & row.Cells(CArbDesc).Value & "','" & row.Cells(CCompuslary).Value & "','" & row.Cells(CFormat).Value & "','" & row.Cells(CArabicFormat).Value & "','" & row.Cells(CTooltip).Value & "','" & row.Cells(CArabicTooltip).Value & _
                     "'," & IIf(IsDBNull(row.Cells(CMaxLenght).Value), 0, row.Cells(CMaxLenght).Value) & ",'" & row.Cells(CIsNumeric).Value & "','" & row.Cells(CIsHide).Value & "','" & row.Cells(CFocusOnStart).Value & "'," & IIf(IsDBNull(row.Cells(CRank).Value), 0, row.Cells(CRank).Value) & "," & IIf(IsDBNull(row.Cells(CMinValue).Value), 0, row.Cells(CMinValue).Value) & "," & IIf(IsDBNull(row.Cells(CMaxValue).Value), 0, row.Cells(CMaxValue).Value) & "," & mDataHandler.DataValue_Out(row.Cells(CField).Value, SqlDbType.Int) & "," & mDataHandler.DataValue_Out(row.Cells(CSearchID).Value, SqlDbType.Int) & "," & mRegUserID & "," & mRegComputerID & "," & row.Cells(CIsArabic).Value & ")" & vbNewLine
                    bFirst = False
                End If
            Next

            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = strCmd
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()

            Return True

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
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
    'Date Created   :10/05/2014
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
                mCode = [Shared].DataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mEngName = [Shared].DataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = [Shared].DataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = [Shared].DataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mEngDescription = [Shared].DataHandler.DataValue_Out(.Item("EngDescription"), SqlDbType.VarChar)
                mArbDescription = [Shared].DataHandler.DataValue_Out(.Item("ArbDescription"), SqlDbType.VarChar)
                mRank = [Shared].DataHandler.DataValue_Out(.Item("Rank"), SqlDbType.Int, True)
                mModuleID = [Shared].DataHandler.DataValue_Out(.Item("ModuleID"), SqlDbType.Int, True)
                mSearchFormID = [Shared].DataHandler.DataValue_Out(.Item("SearchFormID"), SqlDbType.Int, True)
                mHeight = [Shared].DataHandler.DataValue_Out(.Item("Height"), SqlDbType.Int, True)
                mWidth = [Shared].DataHandler.DataValue_Out(.Item("Width"), SqlDbType.Int, True)
                mRemarks = [Shared].DataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = [Shared].DataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = [Shared].DataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = [Shared].DataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = [Shared].DataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mLayout = [Shared].DataHandler.DataValue_Out(.Item("Layout"), SqlDbType.Int, True)
                mLinkTarget = [Shared].DataHandler.DataValue_Out(.Item("LinkTarget"), SqlDbType.VarChar)
                mLinkUrl = [Shared].DataHandler.DataValue_Out(.Item("LinkUrl"), SqlDbType.VarChar)
                mImageUrl = [Shared].DataHandler.DataValue_Out(.Item("ImageUrl"), SqlDbType.VarChar)
                mMainID = [Shared].DataHandler.DataValue_Out(.Item("MainID"), SqlDbType.Int, True)
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
    'Date Created   : 10/05/2014 20:47:50
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngDescription", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngDescription, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbDescription", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbDescription, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Rank", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRank, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ModuleID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mModuleID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SearchFormID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mSearchFormID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Height", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mHeight, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Width", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Layout", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mLayout, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LinkTarget", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mLinkTarget, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LinkUrl", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mLinkUrl, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ImageUrl", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mImageUrl, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MainID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mMainID, SqlDbType.Int)
            If (strMode.Trim.ToUpper = "SAVE") Then
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region
#Region "Class Destructors"

    Protected Overloads Sub finalized()
        mDataSet.Dispose()
    End Sub

#End Region

End Class

Public Class ClsSys_Forms
    Inherits ClsFormsBasic
#Region "Class Constructors"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
    End Sub
#End Region
#Region "Public Function"
    '==================================================================
    'Created by     : [0258]
    'Date           : 17/07/2007
    'Input          : DdlValues as DropDownList
    '                 Condition to fill dropdownlist (if Wanted)
    '                 NullNode to make attention to user by make first item ="Please Select"    
    'Description    :  Fill DropDownList with Forms Name and ID 
    'Steps:            1- Exceute sql statement to get all columns from sys_forms table where filter parameter(if spcefied)
    '                  2- if NullNode is true --then make first text item is  "Please Select" and it's value is 0
    '                  3- fill DtatText of Dropdownlist with English Name and DataValue with ID 
    'Modification   :  [0256] 5-12-2007 Add SetLanguage Function to Switch Between EngName , ArbName Fields 
    '               :                  According to Page Language 
    '==================================================================

    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By EngName ", "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & " Order By EngName ")
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
                DdlValues.Items.Add(Item)
            Next

            If DdlValues.Items.Count > 0 Then
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

    Public Function GetUsersFormsPermissionsCounts(ByVal IntUserID As Integer, ByVal StrCode As String, ByVal StrEngName As String, ByVal StrArbName As String) As DataSet
        Dim DS As DataSet
        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetUsersFormsPermissionsCounter", IntUserID, StrCode, StrEngName, StrArbName)
        Return DS
    End Function

    Public Function GetGroupsFormsPermissionsCounts(ByVal IntGroupID As Integer, ByVal StrCode As String, ByVal StrEngName As String, ByVal StrArbName As String) As DataSet
        Dim DS As DataSet
        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetGroupsFormsPermissionsCounter", IntGroupID, StrCode, StrEngName, StrArbName)
        Return DS
    End Function
#End Region
End Class
