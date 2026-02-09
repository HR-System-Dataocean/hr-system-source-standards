
'=========================================================================
'Program Name  : Clshrs_EndOfServices.vb
'Created by    : DataOcean
'Modified By   : [0256]
'Date Created  : DataOcean
'Date Modified : 23-08-2007 
'Modifications : 
'              :  Add Method GetEmployeeWorkingDays()
'              :  Add Method GetEmployeeNonPaiedDays()
'              :  Add Method GetEmployeeNonPaiedDays()
'              :  Add Method CalculateEndofService()
'              :  Add Method GetVacationsDays()
'                   Class: EndOfServices
'                   Table: hrs_EndOfServices
'                   Relations:
'                               hrs_EndOfServices.ID ->> hrs_EndOfServiceRules.EndOfServiceID
'                               hrs_EndOfServices.CompanyId ->> sys_Companies.ID
'              : B#g0010 23-06-2008 Add _Forgin arrgument to data_ValueIn method in SetParameters Function 
'              :                           And _Forgin arrgument to data_value_Out Method in GetParameters Function 
'              :                           To avoid saving non-existing forign key in any table 
'              :                           And convert the DBnull values to zero in case of forign key fields only 
'=========================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EndOfServices
    Inherits ClsDataAcessLayer


#Region "Class Constructors"

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Description:   In the constructor of the class set
    '                                       -Table name 
    '                                       -Sqlstatment(s) of (Insert,Update,Delete,select) row(s) dealing with the database
    '=====================================================================

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EndOfServices "
        mInsertParameter = " Code,EngName,ArbName,ArbName4S,Remarks,RegUserID,RegComputerID,CompanyID,ExcludedFromSSRequests "
        mInsertParameterValues = " @Code,@EngName,@ArbName,@ArbName4S,@Remarks,@RegUserID,@RegComputerID,@CompanyID,@ExcludedFromSSRequests "
        mUpdateParameter = " Code=@Code,EngName=@EngName,ArbName=@ArbName,ArbName4S=@ArbName4S,Remarks=@Remarks,ExcludedFromSSRequests=@ExcludedFromSSRequests"
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

#End Region

#Region "Private Members"

    '=====================================================================
    'Date : 08/08/2007
    'Description :  Private members declration
    '=====================================================================

    Private mID As Object
    Private mCode As String
    Private mEngName As String
    Private mArbName As String
    Private mArbName4S As String
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Private mCompanyID As Object
    Private mExtraTransactionID As Object
    Private mExcludedFromSSRequests As Boolean


    Const xFromWorkingMonths = 1
    Const xToWorkingMonths = 2
    Const xAmountPercent = 4
    Const xFormula = 3
    Const xExtraFormula = 5


    'Const xFromWorkingMonths = 1
    'Const xToWorkingMonths = 2
    ''Const xAmountPercent = 3
    'Const xFormula = 3
    Const CsIntYearDays = 360
    Const CsIntMonthDays = 30
    Const CsIntMonths = 12



#End Region

#Region "Public property"

    '=====================================================================
    'Date : 08/08/2007
    'Description :  Public property declration
    '=====================================================================

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
    Public Property ExcludedFromSSRequests() As Boolean
        Get
            Return mExcludedFromSSRequests
        End Get
        Set(ByVal Value As Boolean)
            mExcludedFromSSRequests = Value
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
    Public Property CompanyID() As Object
        Get
            Return mCompanyID
        End Get
        Set(ByVal Value As Object)
            mCompanyID = Value
        End Set
    End Property


    Public Property ExtraTransactionID() As Object
        Get
            Return mExtraTransactionID
        End Get
        Set(ByVal Value As Object)
            mExtraTransactionID = Value
        End Set
    End Property

#End Region

#Region "Public Function"

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Input : Filter AS String
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class if dataset doesn't return rows
    '       4-Return true if ID of Filteration >0 (Is Found)
    '
    'Description: Find all columns from hrs_EndOfServices table where filter
    '=====================================================================

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
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Input : Filter AS String
    'Steps: 
    '               - Execute sql statment to get ID from hrs_Grades where filter 
    '               - Check if ID > 0 this mean that row is already exist in hrs_EndOfServices  table 
    '
    'Description:   Determine mode (Save/Update?)
    '=====================================================================

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_EndOfServices Where " & Filter
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Steps: 
    '               - Execute sqlstatment to insert new row in hrs_EndOfServices table
    '
    'Description:   Save New Row in hrs_EndOfServices table
    '=====================================================================

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

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Input : Filter AS String
    'Steps: 
    '               - Execute sqlstatment to Update existing row in hrs_EndOfServices table
    '
    'Description:   Update existing Row in hrs_EndOfServices table WHERE filter
    '=====================================================================

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

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Input : 
    'Steps: 
    '             
    '
    'Description: 
    '=====================================================================
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Input : Filter AS String
    'Steps: 
    '               - Execute sqlstatment to Delete existing row in hrs_EndOfServices table
    '
    'Description: Delete existing Row in hrs_EndOfServices table where filter
    '=====================================================================

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

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Description: Clear all private members  of the class
    '=====================================================================

    Public Function Clear() As Boolean
        Try
            mID = 0
            mCode = String.Empty
            mEngName = String.Empty
            mArbName = String.Empty
            mArbName4S = String.Empty
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mCompanyID = 0
            mExcludedFromSSRequests = False

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Description:  find first row in hrs_EndOfServices table
    'Steps: 
    '       1-execute sqlstatment to find first row in the table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '=====================================================================

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

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Description:  find Last row in hrs_EndOfServices table
    'Steps: 
    '       1-execute sqlstatment to find last row in the table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '=====================================================================

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

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Description:  find Next row in hrs_EndOfServices table
    'Steps: 
    '       1-execute sqlstatment to find Next row in the table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '=====================================================================

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

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Steps: 
    '               - Execute proc sqlstatment to find previous row in the table
    '               - Fill dataset with result of the proc
    '               - Call Getparameter function to insert the results of dataset into the private members of the class
    '
    'Description:   Find previous row in hrs_EndOfServices table
    '=====================================================================

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

    '=====================================================================
    'Created by : [0259]
    'Date : 09/08/2007
    'Steps: 
    '               - Execute StrSelectCommand to determine the required recoed data from the table
    '               - Excute CheckValidDataObject
    '               - Return true
    '
    'Description:   Get the rules equivelant to the record
    '=====================================================================

    Public Function GetEndOfServicesRules(ByVal EndOfServiceID As Integer, ByRef ObjDs As DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            ObjDs = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, "hrs_GetEndOfServiceRules", EndOfServiceID)
            If mDataHandler.CheckValidDataObject(ObjDs) Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Description:   Fill in the dropdownlist with records of hrs_EndOfServices
    'Modified by    : MAz
    'Date           : 05-12-2007
    'Description    : Update method to match with Language
    '=====================================================================

    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String = String.Empty
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = Me.mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And CompanyId=" & Me.MainCompanyID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And CompanyId=" & Me.MainCompanyID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

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
    'Modified by    : MAz
    'Date           : 05-12-2007
    'Description    : Update method to match with Language
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String = String.Empty
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = Me.mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')=''  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                'Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار]")
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    '=====================================================================
    'Created by : [0259]
    'Date : 09/08/2007
    'Steps: 
    '               - Execute StrSelectCommand to determine the required recoed to insert the data
    '               - Return true
    '
    'Description:   Set the rules equivelant to the record
    '=====================================================================

    Public Function SetEndOfServicesRules(ByVal EndOfServiceID As Integer, ByVal ObjGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid) As Boolean

        Dim StrSelectCommand As String = String.Empty
        Dim ObjDataRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
        Try
            StrSelectCommand = " Delete From hrs_EndOfServicesRules Where EndOfServiceID = " & EndOfServiceID & ";" & vbNewLine
            For Each ObjDataRow In ObjGrid.Rows
                StrSelectCommand &= " Insert Into hrs_EndOfServicesRules(EndOfServiceID,FromWorkingMonths,ToWorkingMonths,Formula,ExtraDedFormula,AmountPercent)" &
                "values(" & EndOfServiceID &
                "," & IIf(ObjDataRow.Cells(xFromWorkingMonths).Value Is Nothing, 0, ObjDataRow.Cells(xFromWorkingMonths).Value) &
                "," & IIf(ObjDataRow.Cells(xToWorkingMonths).Value Is Nothing, 0, ObjDataRow.Cells(xToWorkingMonths).Value) &
                ",'" & IIf(ObjDataRow.Cells(xFormula).Value Is Nothing, 0, ObjDataRow.Cells(xFormula).Value) & "'" &
                ",'" & IIf(ObjDataRow.Cells(xExtraFormula).Value Is Nothing, 0, ObjDataRow.Cells(xExtraFormula).Value) & "'" &
                ",'" & IIf(ObjDataRow.Cells(xAmountPercent).Value Is Nothing, 0, ObjDataRow.Cells(xAmountPercent).Value) & "'" &
                ");" & vbNewLine
                '"," & IIf(ObjDataRow.Cells(xAmountPercent).Value Is Nothing, 0, ObjDataRow.Cells(xAmountPercent).Value) & _
            Next
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, StrSelectCommand)
            Return True

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '!=!=======================================================================================
    'Procedure Name      : GetEmployeeWorkingDays()
    'Date Created        : 
    'Developer           :[0256]   
    'Description         :
    'Modifications       :
    '
    '
    '
    'Calling 
    'Called From         :
    'Calles              :
    'Function Arrguments :intEmployeeID,ContractId
    '                    :
    '!=!=======================================================================================
    Public Function GetEmployeeWorkingDays(ByVal intEmployeeID As Integer, ByVal ContractId As Integer, ByRef IntYears As Integer, ByRef IntMonthes As Integer, ByRef IntDays As Integer, ByRef IntTotal As Integer, ByRef IntUnPaid As Integer) As Boolean
        Dim StrSelectCommand As String
        Dim DteContractStartDate As Date
        Dim DteContractEndDate As Date
        Dim IntContractStartDate As Integer
        Dim IntContractEndDate As Integer
        Dim IntContractActualDate As Integer
        Dim IntYearDays As Integer
        Dim IntYearRemainingDays As Integer


        'Dim ClsYearClassification As New Clshrs_YearClassification(mPage)
        Dim LastEndOfServiceDate As Date = Nothing

        Dim IntContarctTotalDays As Integer = 0
        Dim IntContractPaidVactions As Integer = 0
        Dim IntContractUnPaidVactions As Integer = 0
        Dim IntMonthDays As Integer
        Const CsIntYearDays = 360
        Const CsIntMonthDays = 30
        Const CsIntMonths = 12

        'Dim IntYears As UInt16
        'Dim IntMonthes As UInt32
        ' IntDays As UInt32
        'Dim IntMonthDaysRemainingD As Integer



        Try

            '=================================================================================
            'Get the start Date of the contract 
            '=================================================================================

            'StrSelectCommand = "Select Top 1 hrs_contractsRenewals.StartDate  From hrs_contracts inner join hrs_contractsRenewals On hrs_contracts.id = hrs_contractsRenewals.contractid Where hrs_Contracts.id = " & ContractId & " Order by hrs_ContractsRenewals.StartDate"

            StrSelectCommand = " Select TOP 1 EndofServiceDate from hrs_employeesJoins where employeeid = " & intEmployeeID & " Order By EndofServiceDate Desc "
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mDataSet.Clear()
            mSqlDataAdapter.Fill(mDataSet)

            If mDataSet.Tables(0).Rows.Count > 0 Then ' Employee Has Previous End Of Services 
                If mDataSet.Tables(0).Rows(0)("EndofServiceDate") Is DBNull.Value Or LastEndOfServiceDate = #12:00:00 AM# Then
                    StrSelectCommand = " Select *  From hrs_contracts  Where EmployeeID = " & intEmployeeID & " Order By StartDate  "
                    LastEndOfServiceDate = Now.Date.ToShortDateString
                Else
                    StrSelectCommand = " Select *  From hrs_contracts  Where EmployeeID = " & intEmployeeID & " And StartDate >= " & LastEndOfServiceDate & " Order By StartDate "
                    LastEndOfServiceDate = mDataHandler.DataValue_Out(mDataSet.Tables(0).Rows(0)("EndofServiceDate"), SqlDbType.DateTime)
                End If
            Else
                '!=!' Get Contract Infromation 
                LastEndOfServiceDate = Now.Date.ToShortDateString
                'If mDataSet.Tables(0).Rows(0)("EndofServiceDate") Is DBNull.Value Or LastEndOfServiceDate = #12:00:00 AM# Then
                StrSelectCommand = " Select *  From hrs_contracts  Where EmployeeID = " & intEmployeeID & " Order By StartDate  "
                'Else

                'End If
            End If



            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mDataSet.Clear()
            mSqlDataAdapter.Fill(mDataSet)

            'Get Start Date Of First Contract 
            'Get End Date Of Last Contract and if this contract has no end date then use getdate()
            Dim IntCount As Integer = mDataSet.Tables(0).Rows.Count
            If IntCount > 0 Then
                Dim FirstContractStartDate = mDataSet.Tables(0).Rows(0).Item("StartDate")
                Dim LastContractEndDate
                If mDataSet.Tables(0).Rows(IntCount - 1).Item("EndDate") Is DBNull.Value Then
                    LastContractEndDate = Now.Date.ToShortDateString
                Else
                    LastContractEndDate = mDataSet.Tables(0).Rows(IntCount - 1).Item("EndDate")
                End If
                '!=!' Get Contract Total Days 
                IntContarctTotalDays = GetDateDiffrance(FirstContractStartDate, LastContractEndDate)
                '!=!' Get Paid Vacations 
                IntContractPaidVactions = GetVacationsDays(intEmployeeID, FirstContractStartDate, LastContractEndDate, True)
                '!=!' Get UnPaid Vacations 
                IntContractUnPaidVactions = GetVacationsDays(intEmployeeID, FirstContractStartDate, LastContractEndDate, False)
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(mPage, " Current Employee Has No Vaild Contracts ! ")
                Return False
            End If

            For Each ROW As Data.DataRow In mDataSet.Tables(0).Rows
            Next
            IntTotal = IntContarctTotalDays





            IntContarctTotalDays = IntContarctTotalDays - IntContractUnPaidVactions

            '!=! Years Calculations 
            IntYears = IntContarctTotalDays \ CsIntYearDays
            IntYearDays = IntYears * CsIntYearDays
            IntYearRemainingDays = IntContarctTotalDays - IntYearDays

            '!=! Monthes Calculations 
            IntMonthes = IntYearRemainingDays \ CsIntMonthDays
            IntMonthDays = IntMonthes * CsIntMonthDays
            IntDays = IntYearRemainingDays - IntMonthDays

            IntUnPaid = IntContractUnPaidVactions

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '!=!=======================================================================================
    'Procedure Name      : GetEmployeeWorkingDays()
    'Date Created        : 
    'Developer           :[0256]   
    'Description         :
    'Modifications       :
    '
    '
    '
    'Calling 
    'Called From         :
    'Calles              :
    'Function Arrguments :intEmployeeID,ContractId
    '                    :
    '!=!=======================================================================================
    Public Function GetEmployeeWorkingDays(ByVal intEmployeeID As Integer, ByVal ContractId As Integer, ByRef IntYears As Integer,
                                           ByRef IntMonthes As Integer, ByRef IntDays As Integer,
                                           ByRef IntTotal As Integer, ByRef IntUnPaid As Integer, ByVal DteToDate As Date, ByVal StartDate As Date, Optional ByRef IntTotalServicedays As Integer = 0) As Boolean

        Dim StrSelectCommand As String = String.Empty
        Dim DteFirstContractStartDate As Date
        Dim IntYearDays As Integer
        Dim IntYearRemainingDays As Integer
        Dim LastEndOfServiceDate As Date = Nothing
        Dim IntContarctTotalDays As Integer = 0
        Dim IntMonthDays As Integer
        Dim IntPenaltyDays As Integer = 0
        Dim clsCompanies As New Clssys_Companies(mPage)
        Dim clshrsEmployees As New Clshrs_Employees(mPage)

        Try

            '======================================================================================
            'Get the start Date of the contract eather if there is end of service or not 
            '======================================================================================
            StrSelectCommand = " Select TOP 1 EndofServiceDate from hrs_employeesJoins where employeeid = " & intEmployeeID & " Order By EndofServiceDate Desc "
            mDataSet = New DataSet
            mDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)

            If mDataHandler.CheckValidDataObject(mDataSet) Then
                LastEndOfServiceDate = mDataHandler.DataValue_Out(mDataSet.Tables(0).Rows(0)("EndofServiceDate"), SqlDbType.DateTime)
                If mDataSet.Tables(0).Rows(0)("EndofServiceDate") Is DBNull.Value Or LastEndOfServiceDate = #12:00:00 AM# Then
                    StrSelectCommand = CONFIG_DATEFORMAT & " Select *  From hrs_contracts  Where EmployeeID = " & intEmployeeID & " Order By StartDate "
                Else
                    StrSelectCommand = CONFIG_DATEFORMAT & " Select *  From hrs_contracts  Where EmployeeID = " & intEmployeeID & " And StartDate >= '" & Format(LastEndOfServiceDate, "dd/MM/yyyy") & "' Order By StartDate "
                End If
            Else
                StrSelectCommand = CONFIG_DATEFORMAT & " Select *  From hrs_contracts  Where EmployeeID = " & intEmployeeID & " Order By StartDate  "
            End If

            mDataSet = New DataSet
            mDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)

            '======================================================================================
            'Get the diffrance between the startDate and the end of service date by years,months,Days 
            '======================================================================================

            If mDataHandler.CheckValidDataObject(mDataSet) Then
                DteFirstContractStartDate = mDataHandler.DataValue_Out(mDataSet.Tables(0).Rows(0)("StartDate"), SqlDbType.DateTime)


                DteFirstContractStartDate = StartDate




                clshrsEmployees.Find("ID=" & intEmployeeID)
                clsCompanies.Find("ID=" & clshrsEmployees.CompanyID)
                If clsCompanies.RegComputerID = 360 Then
                    Dim start As DateTime = DteFirstContractStartDate
                    Dim finish = DteToDate
                    Dim d1 = If(start.Day = 31, 30, start.Day)
                    Dim d2 = If(finish.Day = 31 AndAlso (start.Day = 30 OrElse start.Day = 31), 30, finish.Day)

                    'Edit By: Hassan Kurdi
                    'Edit Date: 2022-01-18
                    'Purpose: Find the correct value of subtract two dates 
                    'Dim diff1 = (finish.Subtract(start))
                    'IntContarctTotalDays = diff1.TotalDays
                    IntContarctTotalDays = 1 + ((360 * (finish.Year - start.Year)) + (30 * (finish.Month - start.Month)) + (d2 - d1))
                    'End
                Else
                    IntContarctTotalDays = DteToDate.Subtract(DteFirstContractStartDate).Days
                End If






                IntTotalServicedays = IntContarctTotalDays
                '  IntContarctTotalDays = DteToDate.Subtract(DteFirstContractStartDate).Days
                IntPenaltyDays = GetEmployeeTotalPenaltyVacation(intEmployeeID, DteToDate)

                Dim CureYearDays = 0
                CureYearDays = CsIntYearDays
                Dim Sys_Companies As New Clssys_Companies(mPage)
                Sys_Companies.Find("ID = " & Sys_Companies.MainCompanyID)
                If Sys_Companies.RegComputerID <> Nothing Then
                    CureYearDays = Sys_Companies.RegComputerID
                End If

                Dim HeavyYearDays As Integer = GetLeapDays(DteFirstContractStartDate, DteToDate)
                IntContarctTotalDays -= IntPenaltyDays
                IntTotal = IntContarctTotalDays '- HeavyYearDays


                IntYears = IntTotal \ CureYearDays
                IntYearDays = IntYears * CureYearDays

                IntYearRemainingDays = IntTotal - IntYearDays

                IntMonthes = (IntYearRemainingDays * 360 \ CureYearDays) \ CsIntMonthDays
                IntMonthDays = IntMonthes * CsIntMonthDays
                'IntDays = (IntYearRemainingDays * 360 \ CureYearDays) - IntMonthDays
                IntDays = IntYearRemainingDays - IntMonthDays
                IntUnPaid = IntPenaltyDays


            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function GetLeapDays(ByVal StartDate As Date, ByVal EndDate As Date) As Single
        Dim CntDays As Single = 0
        If StartDate.Year = EndDate.Year Then
            If EndDate.Subtract(StartDate).Days + 1 > 365 Then
                CntDays = CntDays + 1
            End If
        Else
            For i As Integer = StartDate.Year To EndDate.Year
                If i = StartDate.Year And StartDate.Month < 2 Then
                    CntDays += IIf(DateTime.IsLeapYear(i), 1, 0)
                End If
                If i = EndDate.Year And EndDate.Month > 2 Then
                    CntDays += IIf(DateTime.IsLeapYear(i), 1, 0)
                End If
                If i <> EndDate.Year And i <> StartDate.Year Then
                    CntDays += IIf(DateTime.IsLeapYear(i), 1, 0)
                End If
            Next
        End If
        Return CntDays
    End Function

    Public Function GetDateDiffrance(ByVal StartDate As Date, ByVal EndDate As Date) As Double
        Dim IntStartYear As Integer
        Dim IntEndYear As Integer
        Dim IntStartMonth As Integer
        Dim IntEndMonth As Integer
        Dim IntStartDay As Integer
        Dim IntEndDay As Integer
        Dim IntStartTotalDays As Integer
        Dim IntEndTotalDays As Integer
        Dim IntDifference As Integer

        Try
            IntStartYear = DatePart(DateInterval.Year, StartDate)
            IntEndYear = DatePart(DateInterval.Year, EndDate)

            IntStartMonth = DatePart(DateInterval.Month, StartDate)
            IntEndMonth = DatePart(DateInterval.Month, EndDate)

            IntStartDay = DatePart(DateInterval.Day, StartDate)
            If DatePart(DateInterval.Day, EndDate) = 31 Or DatePart(DateInterval.Day, EndDate) = 28 Then
                IntEndDay = 30
            Else
                IntEndDay = DatePart(DateInterval.Day, EndDate)
            End If


            IntStartTotalDays = IntStartYear * 360 + IntStartMonth * 30 + IntStartDay
            IntEndTotalDays = IntEndYear * 360 + IntEndMonth * 30 + IntEndDay + 1

            IntDifference = IntEndTotalDays - IntStartTotalDays

            Return IntDifference

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '!=!=======================================================================================
    'Procedure Name      :  GetEmployeeNonPaiedDays
    'Date Created        :  20-08-2007
    'Developer           : [0256]    
    'Description         : 
    'Modifications       :
    '
    '
    '
    'Calling 
    'Called From         :
    'Calles              :
    'Function Arrguments :
    '                    :
    '!=!=======================================================================================
    Public Function GetEmployeeNonPaiedDays(ByRef IntUnPaid As Integer, ByRef IntYear As Integer, ByRef IntMonth As Integer, ByRef intDays As Integer) As Boolean

        Dim IntYearDays As Integer
        Dim IntYearRemaining As Integer
        Dim IntMonthDays As Integer

        Const CsIntYearDays = 360
        Const CsIntMonthDays = 30
        Const CsIntMonths = 12
        Try
            IntYear = IntUnPaid \ CsIntYearDays
            IntYearDays = IntYear * CsIntYearDays
            IntYearRemaining = IntUnPaid - IntYearDays

            IntMonth = IntYearRemaining \ CsIntMonthDays
            IntMonthDays = IntMonth * CsIntMonthDays
            intDays = IntYearRemaining - IntMonthDays

            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function GetYearsMonthsDays(ByVal clsCompaniesDays As Integer, ByRef Totaldays As Integer, ByRef IntYear As Integer, ByRef IntMonth As Integer, ByRef intDays As Integer) As Boolean

        Dim IntYearDays As Integer
        Dim IntYearRemaining As Integer
        Dim IntMonthDays As Integer

        Dim CsIntYearDays As Integer = clsCompaniesDays
        Const CsIntMonthDays = 30
        Const CsIntMonths = 12
        Try

            IntYear = Totaldays \ CsIntYearDays
            IntYearDays = IntYear * CsIntYearDays
            IntYearRemaining = Totaldays - IntYearDays

            ' IntMonth = (IntYearRemaining * 360 \ CsIntYearDays) \ CsIntMonthDays
            'Rabie 09-02-2026
            IntMonth = (IntYearRemaining * clsCompaniesDays \ CsIntYearDays) \ CsIntMonthDays
            IntMonthDays = IntMonth * CsIntMonthDays
            intDays = IntYearRemaining - IntMonthDays

            'New Rabie 09-02-2026
            If IntMonth = 0 And (Totaldays - IntYearDays) > 0 And intDays = 0 Then
                intDays = Totaldays - IntYearDays
            End If
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function CalculateEndofService(ByVal InEmployeeId As Integer, ByVal IntDays As Single, ByVal EndOfServiceTypeId As Integer, ByRef Amount As Double, ByVal EndDate As Date) As Boolean
        Dim ClsEndOfServicesRules As New Clshrs_EndOfServicesRules(mPage)
        Dim ClsSolver As New Clshrs_FormulaSolver(ConnectionString, mPage)
        Dim ObjDataRow As Data.DataRow
        Dim IntFromNumberofDays As Integer
        Dim IntFromNumberofDays1 As Integer
        Dim IntToNumberOfDays As Integer
        Dim IntCurrentDays As Integer
        Dim StrFormulaExpression As String = String.Empty
        Dim StrFormulaExtraExpression As String = String.Empty
        Try
            Dim CureYearDays = 0
            CureYearDays = CsIntYearDays
            Dim Sys_Companies As New Clssys_Companies(mPage)
            Sys_Companies.Find("ID = " & Sys_Companies.MainCompanyID)
            If Sys_Companies.RegComputerID <> Nothing Then
                CureYearDays = Sys_Companies.RegComputerID
            End If
            Dim IntYears = IntDays \ CureYearDays
            Dim IntYearDays = IntYears * CureYearDays
            ''!=! Years Calculations 
            Dim IntYearRemainingDays = IntDays - IntYearDays
            ''!=! Monthes Calculations 
            Dim IntMonthes = (IntYearRemainingDays * 360 \ CureYearDays) \ CsIntMonthDays
            Dim IntMonthDays = IntMonthes * CsIntMonthDays
            Dim IntDays1 = (IntYearRemainingDays * 360 \ CureYearDays) - IntMonthDays
            ClsEndOfServicesRules.Find(" EndOfServiceID = " & EndOfServiceTypeId)
            IntCurrentDays = IntYears
            For Each ObjDataRow In ClsEndOfServicesRules.DataSet.Tables(0).Rows
                If ObjDataRow("FromWorkingMonths") > (IntDays / 30) Then
                    Continue For
                End If

                StrFormulaExpression = ObjDataRow("Formula")
                StrFormulaExtraExpression = ObjDataRow("ExtraDedFormula")
                If IntCurrentDays < ((ObjDataRow("ToWorkingMonths") / 12) - (IntYears - IntCurrentDays)) Then
                    ClsSolver.EmployeeID = InEmployeeId
                    ClsSolver.NoOfDaysPerPeriod = 30
                    ClsSolver.NoOfWorkingDays = 30
                    ClsSolver.Executedate = EndDate
                    ClsSolver.EvaluateExpression(StrFormulaExpression)
                    Amount += ClsSolver.Output * IntCurrentDays
                    If Sys_Companies.IsHigry Then
                        Amount += (ClsSolver.Output * (IntYearRemainingDays * 30 / 354)) / 30
                    Else
                        Amount += (ClsSolver.Output * (IntYearRemainingDays * 30 / CureYearDays)) / 30
                    End If
                    'Amount += ClsSolver.Output * (IntDays1 / 30 / 12)
                    Amount = Amount * ObjDataRow("AmountPercent") / 100
                    ClsSolver.Executedate = EndDate
                    ClsSolver.EvaluateExpression(StrFormulaExtraExpression)
                    Amount = Amount + (ClsSolver.Output)
                    Exit For
                ElseIf IntCurrentDays >= ((ObjDataRow("ToWorkingMonths") / 12) - (IntYears - IntCurrentDays)) Then
                    ClsSolver.EmployeeID = InEmployeeId
                    ClsSolver.NoOfDaysPerPeriod = 30
                    ClsSolver.NoOfWorkingDays = 30
                    ClsSolver.Executedate = EndDate
                    ClsSolver.EvaluateExpression(StrFormulaExpression)
                    Amount += ClsSolver.Output * ((ObjDataRow("ToWorkingMonths") / 12) - (IntYears - IntCurrentDays))
                    IntCurrentDays = IntYears - (ObjDataRow("ToWorkingMonths") / 12)
                End If

            Next
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function CalculateEndofServiceCost(ByVal InEmployeeId As Integer, ByVal IntDays As Single, ByVal EndOfServiceTypeId As Integer, ByRef Amount As Double) As Boolean
        Dim ClsEndOfServicesRules As New Clshrs_EndOfServicesRules(mPage)
        Dim ClsSolver As New Clshrs_FormulaSolver(ConnectionString, mPage)
        Dim ObjDataRow As Data.DataRow
        Dim IntFromNumberofDays As Integer
        Dim IntFromNumberofDays1 As Integer
        Dim IntToNumberOfDays As Integer
        Dim IntCurrentDays As Integer
        Dim StrFormulaExpression As String = String.Empty
        Try

            Dim CureYearDays = 0
            CureYearDays = CsIntYearDays
            Dim Sys_Companies As New Clssys_Companies(mPage)
            Sys_Companies.Find("ID = " & Sys_Companies.MainCompanyID)
            If Sys_Companies.RegComputerID <> Nothing Then
                CureYearDays = Sys_Companies.RegComputerID
            End If
            ClsEndOfServicesRules.Find(" EndOfServiceID = " & EndOfServiceTypeId)
            IntCurrentDays = IntDays * 360 \ (IIf(CureYearDays > 360, 360, CureYearDays))
            Dim Cnt As Int16 = 0
            For Each ObjDataRow In ClsEndOfServicesRules.DataSet.Tables(0).Rows
                If Cnt > 0 Then
                    IntFromNumberofDays = (ObjDataRow("FromWorkingMonths") - 1) * 30
                End If
                IntFromNumberofDays1 = (ObjDataRow("FromWorkingMonths") - 1) * 30
                IntToNumberOfDays = ObjDataRow("ToWorkingMonths") * 30
                StrFormulaExpression = ObjDataRow("Formula")
                If Cnt > 0 Then
                    IntCurrentDays += IntFromNumberofDays
                End If
                Cnt = Cnt + 1
                If IntFromNumberofDays1 < IntDays Then
                    If IntFromNumberofDays < IntDays And IntToNumberOfDays >= IntDays Then
                        ClsSolver.EmployeeID = InEmployeeId
                        ClsSolver.NoOfDaysPerPeriod = 30
                        ClsSolver.NoOfWorkingDays = 30
                        ClsSolver.EvaluateExpression(StrFormulaExpression)
                        Amount = ClsSolver.Output ' * ((IntCurrentDays / 360) - (IntFromNumberofDays / 360))
                        ' Amount = Amount * ObjDataRow("AmountPercent") / 100
                    ElseIf IntDays > IntToNumberOfDays Then
                        ClsSolver.EmployeeID = InEmployeeId
                        ClsSolver.NoOfDaysPerPeriod = 30
                        ClsSolver.NoOfWorkingDays = 30
                        ClsSolver.EvaluateExpression(StrFormulaExpression)
                        Amount = ClsSolver.Output '* ((IntToNumberOfDays / 360) - (IntFromNumberofDays / 360))
                        IntCurrentDays = IntCurrentDays - IntToNumberOfDays
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region

#Region "Class Private Function"

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Input : Ds as Dataset 
    'Description:   Asign the Result of Ds to the private members of the class object
    '=====================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = mDataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mCompanyID = mDataHandler.DataValue_Out(.Item("CompanyID"), SqlDbType.Int, True)
                mExtraTransactionID = mDataHandler.DataValue_Out(.Item("ExtraTransactionID"), SqlDbType.Int, True)
                mExcludedFromSSRequests = mDataHandler.DataValue_Out(.Item("ExcludedFromSSRequests"), SqlDbType.Bit, True)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '=====================================================================
    'Created by : DataOcean
    'Date : 08/08/2007
    'Description:   Make the values of parameter equal values of private member of the class object
    '=====================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int, True)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExcludedFromSSRequests", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mExcludedFromSSRequests, SqlDbType.Bit, True)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    'Private Function SaveIntervals(ByVal RecordId As Integer, ByVal SqlCommand As SqlClient.SqlCommand) As Boolean


    '    Dim ObjRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
    '    Dim StrSqlCommand As String = String.Empty
    '    Try

    '        StrSqlCommand = "Delete From hrs_EndofServiceTypesRules Where EndOfServiceTypeId=" & RecordId & ";" & vbNewLine
    '        For Each ObjRow In mGrid.Rows
    '            If Not ObjRow.Cells(4).Value Is DBNull.Value And Not ObjRow.Cells(5).Value Is DBNull.Value Then
    '                StrSqlCommand &= "Insert Into hrs_EndofServiceTypesRules(EndOfServiceTypeId,FromTimeInterval,FromTimeIntervalTypeId,ToTimeInterval,ToTimeIntervalTypeId,Formula,RegUserId,CompanyId)Select " & _
    '                "EndOfServiceTypeId=" & RecordId & "," & _
    '                "FromTimeInterval=" & ObjRow.Cells(0).Value & "," & _
    '                "FromTimeIntervalTypeId=" & ObjRow.Cells(1).Value & "," & _
    '                "ToTimeInterval=" & ObjRow.Cells(2).Value & "," & _
    '                "ToTimeIntervalTypeId=" & ObjRow.Cells(3).Value & "," & _
    '                "Formula='" & ObjRow.Cells(4).Value & "'," & _
    '                "RegUserId=" & Me.mDataBaseUserRelatedID & "," & _
    '                "CompanyId=" & Me.MainCompanyID & ";" & vbNewLine
    '            End If
    '        Next
    '        mSqlCommand.CommandType = CommandType.Text
    '        mSqlCommand.CommandText = StrSqlCommand
    '        If Len(StrSqlCommand) > 0 Then
    '            mSqlCommand.ExecuteNonQuery()
    '        End If


    '    Catch ex As Exception
    '        mPage.Session.Add("ErrorValue", ex)
    '        mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
    '        mPage.Response.Redirect("ErrorPage.aspx")
    '    End Try
    'End Function
    Private Function GetVacationsDays(ByVal IntEmployeeId As Integer, ByVal startDate As Date, ByVal Enddate As Date, ByVal IsPaid As Boolean) As Integer

        Dim clsVacations As New Clshrs_EmployeesVacations(mPage)
        Dim ClsVacationsTypes As New Clshrs_VacationsTypes(mPage)
        Dim IntDaysInVacations As Integer
        Dim DteActualStartDate As Date
        Dim DteActualEndDate As Date

        If IsPaid = True Then
            ClsVacationsTypes.Find(" IsPaid = 1")
        Else
            ClsVacationsTypes.Find(" IsPaid = 0")
        End If

        If clsVacations.Find(" EmployeeID= " & IntEmployeeId & " And ActualStartDate Between '" & startDate & "' And '" & Enddate & "'") Then
            'Some Other Parameters Like Vacation Approval and PaperProcessed will be Included in The previous SQL statement

            For Each row As Data.DataRow In clsVacations.DataSet.Tables(0).Rows
                For Each rowVactionsTypes As Data.DataRow In ClsVacationsTypes.DataSet.Tables(0).Rows
                    If rowVactionsTypes.Item("ID") = row.Item("VacationTypeID") Then
                        DteActualStartDate = row.Item("ActualStartDate")
                        If row.Item("ActualEndDate") Is DBNull.Value Then
                            DteActualEndDate = Now.Date.ToShortDateString
                        Else
                            DteActualEndDate = row.Item("ActualEndDate").adddays(1)
                        End If
                        IntDaysInVacations += DateDiff(DateInterval.Day, DteActualEndDate, DteActualEndDate)
                    End If
                Next
            Next
            Return IntDaysInVacations
        Else
            Return 0
        End If
    End Function

    '!-!==========================================================================
    'Procedure Name : GetEmployeeTotalPenality Vacation  
    'Date Created   : 07-08-2007 
    'Date Modified  :
    'Developer      : 
    'Description    : 
    '               : 
    '               : 
    '               : 
    '               : 
    '               :  
    'Calling        :
    'Form           : 
    'Calls          : 
    '               : 
    '               :
    '               : 
    '               : 
    '!-!==========================================================================
    'ToDo Remove #001 and implement it in the database by Mahmoud
    Private Function GetEmployeeTotalPenaltyVacation(ByVal intEmployeeId As Integer, ByVal EndOfServiceDate As Date) As Single
        Dim ClsContract As New Clshrs_Contracts(mPage)
        Dim intValidContract As Integer = ClsContract.ContractValidatoinId(intEmployeeId, EndOfServiceDate)
        Dim retVal As Object
        ClsContract.Find(" ID = " & intValidContract)
        retVal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContract.ConnectionString, "hrs_GetEmployeeVacationPenaltyForEos", Format(ClsContract.StartDate, "dd/MM/yyyy"), Format(EndOfServiceDate, "dd/MM/yyyy"), intEmployeeId)
        '#001==============================================
        If IsDBNull(retVal) Then
            Return 0
        Else
            Return retVal
        End If
        '#001==============================================

    End Function

    '!-!==========================================================================
    'Procedure Name : GetDateDiffWith Period
    'Date Created   : 07-08-2007 
    'Date Modified  :
    'Developer      : 
    'Description    : 
    '               : 
    '               : 
    '               : 
    '               : 
    '               :  
    'Calling        :
    'Form           : 
    'Calls          : 
    '               : 
    '               :
    '               : 
    '               : 
    '!-!==========================================================================
    Private Function GetDateDiffWithPeriod(ByVal date1 As Object, ByVal date2 As Object, ByVal periodEndDate As Date) As Single

        Dim dateFrom As Date = CDate(date1)
        Dim dateTo As Date
        If IsDBNull(date2) Then

            dateTo = periodEndDate
        Else
            If CDate(date2) > periodEndDate Then
                dateTo = periodEndDate
            Else
                dateTo = CDate(date2)
            End If

        End If

        Return DateDiff(DateInterval.Hour, dateFrom, dateTo) / 24

    End Function

#End Region

#Region "Class Destructors"

    '=====================================================================
    'Date : 08/08/2007
    'Description :  Dispose dataset from the stack
    '=====================================================================

    Public Sub finalized()
        mDataSet.Dispose()
    End Sub

#End Region

End Class
