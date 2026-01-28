
''=========================================================================
''Created by : Mohammed Gad
''Date : 09/07/2007
''                   Class: Projects
''                   Table: hrs_Projects
''                   Relations:
''                               hrs_Projects.CompanyID ->> sys_Companies.ID
''=========================================================================

'Public Class Clshrs_Projects
'    Inherits ClsDataAcessLayer


'#Region "Class Constructors"

'    '=====================================================================
'    'Created by : DataOcean
'    'Date : 09/07/2007
'    'Description:   In the constructor of the class set
'    '                                       -Table name 
'    '                                       -Sqlstatment(s) of (Insert,Update,Delete,select) row(s) dealing with the database
'    ' change constructor Signature
'    '=====================================================================

'    Public Sub New(ByVal Page As Web.UI.Page, ByVal TableName As String)
'        MyBase.New(Page)
'        With Me
'            .Table = TAbleName
'            .mInsertParameter = " Code,EngName,ArbName,ArbName4S,Remarks,RegUserID,RegComputerID,CompanyID "
'            .mInsertParameterValues = " @Code,@EngName,@ArbName,@ArbName4S,@Remarks,@RegUserID,@RegComputerID,@CompanyID "
'            .mUpdateParameter = " Code=@Code,EngName=@EngName,ArbName=@ArbName,ArbName4S=@ArbName4S,Remarks=@Remarks " ',CompanyID=@CompanyID
'            .mSelectCommand = " Select * From  " & mTable
'            .mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
'            .mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
'            .mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
'        End With
'    End Sub

'#End Region

'#Region "Private Members"

'    '=====================================================================
'    'Date : 09/07/2007
'    'Description: Private members declration
'    '=====================================================================

'    Private mID As Object
'    Private mCode As String
'    Private mEngName As String
'    Private mArbName As String
'    Private mArbName4S As String
'    Private mRemarks As String
'    Private mRegUserID As Object
'    Private mRegComputerID As Object
'    Private mRegDate As Object
'    Private mCancelDate As Object
'    Private mCompanyID As Object

'#End Region

'#Region "Public property"

'    '=====================================================================
'    'Date : 09/07/2007
'    'Description: Public property declration
'    '=====================================================================

'    Public Property ID() As Object
'        Get
'            Return mID
'        End Get
'        Set(ByVal Value As Object)
'            mID = Value
'        End Set
'    End Property

'    Public Property Code() As String
'        Get
'            Return mCode
'        End Get
'        Set(ByVal Value As String)
'            mCode = Value
'        End Set
'    End Property

'    Public Property EngName() As String
'        Get
'            Return mEngName
'        End Get
'        Set(ByVal Value As String)
'            mEngName = Value
'        End Set
'    End Property

'    Public Property ArbName() As String
'        Get
'            Return mArbName
'        End Get
'        Set(ByVal Value As String)
'            mArbName = Value
'            mArbName4S = mStringHandler.ReplaceHamza(Value)
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

'    Public Property CompanyID() As Object
'        Get
'            Return mCompanyID
'        End Get
'        Set(ByVal Value As Object)
'            mCompanyID = Value
'        End Set
'    End Property


'#End Region

'#Region "Public Function"

'    '=====================================================================
'    'Created by : DataOcean
'    'Date : 10/07/2007
'    'Input : Filter as string (ex. ID=2)
'    'Steps: 
'    '       1-Fill Dataset with the results of sqldataAdapter
'    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
'    '       3-Clear all private members of the class
'    '       4-Return true if ID of Filteration >0 (Is Found)
'    '
'    'Description: Find all columns from Clshrs_Projects table where filter and canceldate = null 
'    '=====================================================================

'    Public Function Find(ByVal Filter As String) As Boolean
'        Dim StrSelectCommand As String
'        Try
'            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

'    '=====================================================================
'    'Created by : DataOcean
'    'Date : 10/07/2007
'    'Input : Filter as string (ex. ID=2)
'    'Description: Save or Update row 
'    'Steps: 
'    '       1-Execute sql statment to get ID from Clshrs_Projects where filter 
'    '       2-Check if ID > 0 this mean that row is already exist in Clshrs_Projects  table 
'    '       the make Update to this row
'    '           IF ID =0 this mean that row is new row Then Insert the row in Clshrs_Projects  table
'    '=====================================================================

'    Public Function SaveUpdate(ByVal Filter As String) As Boolean
'        Try
'            Dim StrSqlCommand As String
'            Dim Value As Integer
'            StrSqlCommand = "Select ID From hrs_Projects Where " & Filter
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = StrSqlCommand
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
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    '=====================================================================
'    'Created by : DataOcean
'    'Date : 10/07/2007

'    'Description: Save New Row in Clshrs_Projects  table
'    'Steps: 
'    '       1-execute sqlstatment to insert new row in Clshrs_Projects  table

'    '=====================================================================

'    Public Function Save() As Boolean
'        Try
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = mInsertCommand
'            SetParameter(mSqlCommand, OperationType.Save)
'            mSqlCommand.Connection.Open()
'            mSqlCommand.ExecuteNonQuery()
'            mSqlCommand.Connection.Close()
'            Return True

'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    '=====================================================================
'    'Created by : DataOcean
'    'Date : 10/07/2007
'    'Inptu : Filter as string (ex.ID=2)
'    'Description: Update existing Row in Clshrs_Projects  table where filter
'    'Steps: 
'    '       1-execute sqlstatment to Update existing row in Clshrs_Projects  table

'    '=====================================================================

'    Public Function Update(ByVal Filter As String) As Boolean
'        Dim StrUpdateCommand As String
'        Try
'            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = StrUpdateCommand
'            SetParameter(mSqlCommand, OperationType.Update)
'            mSqlCommand.Connection.Open()
'            mSqlCommand.ExecuteNonQuery()
'            mSqlCommand.Connection.Close()
'            Return True
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    '=====================================================================
'    'Created by : DataOcean
'    'Date : 10/07/2007
'    'Inptu : Filter as string (ex.ID=2)
'    'Description: Delete existing Row in Clshrs_Projects  table where filter
'    'Steps: 
'    '       1-execute sqlstatment to Delete existing row in Clshrs_Projects  table

'    '=====================================================================

'    Public Function Delete(ByVal Filter As String) As Boolean
'        Dim StrDeleteCommand As String
'        Try
'            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = StrDeleteCommand
'            mSqlCommand.Connection.Open()
'            mSqlCommand.ExecuteNonQuery()
'            mSqlCommand.Connection.Close()
'            Return True
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    '=====================================================================
'    'Created by : DataOcean
'    'Date : 10/07/2007
'    'Description: Clear all private members  of the class
'    '=====================================================================

'    Public Function Clear() As Boolean
'        Try
'            mID = 0
'            mCode = String.Empty
'            mEngName = String.Empty
'            mArbName = String.Empty
'            mArbName4S = String.Empty
'            mRemarks = String.Empty
'            mRegUserID = 0
'            mRegComputerID = 0
'            mRegDate = Nothing
'            mCancelDate = Nothing
'            mCompanyID = 0

'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    '=====================================================================
'    'Created by : DataOcean
'    'Date : 10/07/2007
'    'Description:  find first row in sys_FormsControls table
'    'Steps: 
'    '       1-execute sqlstatment to find first row in Clshrs_Projects  table
'    '       2-Fill dataset with result of sqlstatment
'    '       3-call Getparameter function to insert the result of dataset into private members of the class
'    '=====================================================================

'    Public Function FirstRecord() As Boolean
'        Dim StrSelectCommand As String = String.Empty
'        Try
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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

'    '=====================================================================
'    'Created by : DataOcean
'    'Date : 10/07/2007
'    'Description:  find Last row in Clshrs_Projects  table
'    'Steps: 
'    '       1-execute sqlstatment to find last row in Clshrs_Projects  table
'    '       2-Fill dataset with result of sqlstatment
'    '       3-call Getparameter function to insert the result of dataset into private members of the class
'    '=====================================================================

'    Public Function LastRecord() As Boolean
'        Dim StrSelectCommand As String = String.Empty
'        Try
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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

'    '=====================================================================
'    'Created by : DataOcean
'    'Date : 10/07/2007
'    'Description:  find Next row in Clshrs_Projects  table
'    'Steps: 
'    '       1-execute sqlstatment to find Next row in Clshrs_Projects  table
'    '       2-Fill dataset with result of sqlstatment
'    '       3-call Getparameter function to insert the result of dataset into private members of the class
'    '=====================================================================

'    Public Function NextRecord() As Boolean
'        Dim StrSelectCommand As String = String.Empty
'        Try
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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

'    '=====================================================================
'    'Created by : DataOcean
'    'Date : 10/07/2007
'    'Steps: 
'    '               - Execute proc sqlstatment to find previous row in Clshrs_Projects  table
'    '               - Fill dataset with result of the proc
'    '               - Call Getparameter function to insert the results of dataset into the private members of the class
'    'Description:   Find previous row in Clshrs_Projects  table
'    '=====================================================================

'    Public Function previousRecord() As Boolean
'        Dim StrSelectCommand As String = String.Empty
'        Try
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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

'    '==================================================================
'    'Created by : Mohammed Gad
'    'Date : 16/07/2007
'    'Steps: 
'    '               - Clear the DropDownList Values
'    '               - Check NullNode()
'    '               - If Null screen [Select Your Choice] choice
'    '               - If not screen the EngName for the selected Item value
'    'Description:   Fill in the DropDownList with DB records
'    '==================================================================

'    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
'        Dim ObjDataRow As DataRow
'        Dim StrSelectCommand As String
'        Dim ObjDataset As New DataSet
'        Dim Item As Global.System.Web.UI.WebControls.ListItem

'        Try

'            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
'            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
'            DdlValues.Items.Clear()

'            If NullNode Then
'                Item = New Global.System.Web.UI.WebControls.ListItem
'                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
'                Item.Value = 0
'                DdlValues.Items.Add(Item)
'            End If

'            For Each ObjDataRow In ObjDataset.Tables(0).Rows
'                Item = New Global.System.Web.UI.WebControls.ListItem
'                Item.Text = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
'                Item.Value = ObjDataRow("ID")
'                DdlValues.SelectedValue = Item.Value
'                DdlValues.Items.Add(Item)
'            Next

'            If DdlValues.Items.Count > 0 Then
'                Return True
'            End If

'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        Finally
'            ObjDataset.Dispose()
'        End Try
'    End Function

'    '=====================================================================
'    'Created by : Mohammed Gad
'    'Date : 16/07/2007
'    'Input : Ds as Dataset 
'    'Description:   Get all the records data from the DataBase Table(Me.mTable)
'    '=====================================================================

'    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList) As Boolean
'        Dim ObjDataRow As DataRow
'        Dim StrCommandString As String
'        Dim ObjDataset As New DataSet
'        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem

'        Try

'            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=1" ' & Me.MainCompanyID
'            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
'            DdlValues.ValueListItems.Clear()

'            For Each ObjDataRow In ObjDataset.Tables(0).Rows
'                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
'                Item.DisplayText = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
'                Item.DataValue = ObjDataRow("ID")
'                DdlValues.ValueListItems.Add(Item)
'            Next

'            If DdlValues.ValueListItems.Count > 0 Then
'                Return True
'            End If

'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")

'        Finally
'            ObjDataset.Dispose()
'        End Try

'    End Function

'#End Region

'#Region "Class Private Function"

'    '=====================================================================
'    'Created by : DataOcean
'    'Date : 10/07/2007
'    'Input : Ds as Dataset 
'    'Description:   Asign the Result of Ds to the private members of the class
'    '=====================================================================

'    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
'        Try
'            With Ds.Tables(0).Rows(0)
'                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
'                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
'                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
'                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
'                mArbName4S = mDataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
'                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
'                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int)
'                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int)
'                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
'                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
'                mCompanyID = mDataHandler.DataValue_Out(.Item("CompanyID"), SqlDbType.Int)
'            End With
'            Return True
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    '=====================================================================
'    'Created by : DataOcean
'    'Date : 10/07/2007
'    'Description:   Make the values of parameter equal values of private member of the class
'    '=====================================================================

'    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal ptrOperationType As OperationType) As Boolean
'        Try
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
'            Select Case ptrOperationType
'                Case OperationType.Save
'                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int)
'                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int)
'                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int)
'            End Select
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'#End Region

'#Region "Class Destructors"

'    '=====================================================================
'    'Created by : DataOcean
'    'Date : 10/07/2007
'    'Description :  Dispose dataset from the stack
'    '=====================================================================

'    Protected Overloads Sub finalized()
'        mDataSet.Dispose()
'    End Sub

'#End Region

'End Class

