
'Public Class Clshrs_EvaluationsMainElements
'    Inherits ClsDataAcessLayer


'#Region "Class Constructors"

'    Public Sub New(ByVal Page As Web.UI.Page)
'        MyBase.New(Page)
'        mTable = " hrs_EvaluationsMainElements "
'        mInsertParameter = " Code,EngName,ArbName,ArbName4S,Remarks,RegUserID,EvaluationTotal,EvaluationTypeID "
'        mInsertParameterValues = " @Code,@EngName,@ArbName,@ArbName4S,@Remarks,@RegUserID ,@EvaluationTotal,@EvaluationTypeID"
'        mUpdateParameter = " Code=@Code,EngName=@EngName,ArbName=@ArbName,ArbName4S=@ArbName4S,Remarks=@Remarks,EvaluationTotal=@EvaluationTotal,EvaluationTypeID=@EvaluationTypeID "
'        mSelectCommand = " Select * from " & mTable
'        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ") ;"
'        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
'        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
'    End Sub

'#End Region

'#Region "Private Members"

'    Private mID As Integer
'    Private mCode As String
'    Private mEngName As String
'    Private mArbName As String
'    Private mArbName4S As String
'    Private mRemarks As String
'    Private mCompanyID As Integer
'    Private mRegUserID As Integer
'    Private mRegDate As Date
'    Private mCancelDate As Date
'    Private mEvaluationTotal As Double
'    Private mEvaluationTypeID As Integer

'#End Region

'#Region "Public property"

'    Public Property ID() As Integer
'        Get
'            Return mID
'        End Get
'        Set(ByVal Value As Integer)
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

'    Public Property ArbName4S() As String
'        Get
'            Return mArbName4S
'        End Get
'        Set(ByVal Value As String)
'            mArbName4S = Value
'        End Set
'    End Property

'    Public Property CompanyID() As Integer
'        Get
'            Return mCompanyID
'        End Get
'        Set(ByVal Value As Integer)
'            mCompanyID = Value
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

'    Public Property RegUserID() As Integer
'        Get
'            Return mRegUserID
'        End Get
'        Set(ByVal Value As Integer)
'            mRegUserID = Value
'        End Set
'    End Property

'    Public Property RegDate() As Date
'        Get
'            Return mRegDate
'        End Get
'        Set(ByVal Value As Date)
'            mRegDate = Value
'        End Set
'    End Property

'    Public Property CancelDate() As Date
'        Get
'            Return mCancelDate
'        End Get
'        Set(ByVal Value As Date)
'            mCancelDate = Value
'        End Set
'    End Property

'    Public Property EvaluationTotal() As Double
'        Get
'            Return mEvaluationTotal
'        End Get
'        Set(ByVal value As Double)
'            mEvaluationTotal = value
'        End Set
'    End Property
'    Public Property EvaluationTypeID() As Integer
'        Get
'            Return mEvaluationTypeID
'        End Get
'        Set(ByVal value As Integer)
'            mEvaluationTypeID = value
'        End Set
'    End Property

'#End Region

'#Region "Public Function"


'    Public Function Find(ByVal Filter As String) As Boolean
'        Dim StrSelectCommand As String
'        Try
'            '==================== Order By Modification [Start]
'            Dim orderByStr As String = ""
'            If Filter.ToLower.IndexOf("order by") = -1 Then
'                orderByStr = " Order By Code "
'            End If
'            'Filter = Filter.Replace("Õ", " AM ").Replace("ã", " PM ")
'            '==================== Order By Modification [ End ]
'            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
'            '==================== Order By Modification [Start]
'            StrSelectCommand = StrSelectCommand & orderByStr
'            '==================== Order By Modification [ End ]
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


'    Public Function Save() As Integer
'        Dim IntNewID As Integer
'        Try
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = mInsertCommand & " Select SCOPE_IDENTITY();"
'            SetParameter(mSqlCommand, OperationType.Save)
'            mSqlCommand.Connection.Open()
'            IntNewID = mSqlCommand.ExecuteScalar()
'            mSqlCommand.Connection.Close()
'            Return IntNewID

'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function


'    Public Function Update(ByVal Filter As String) As Boolean
'        Dim StrUpdateCommand As String
'        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
'        Try
'            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = StrUpdateCommand
'            SetParameter(mSqlCommand, OperationType.Update)
'            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")

'            Return True

'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function


'    Public Function Delete(ByVal Filter As String) As Boolean
'        Dim StrDeleteCommand As String = String.Empty
'        Try
'            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = StrDeleteCommand
'            SetParameter(mSqlCommand, OperationType.Update)
'            mSqlCommand.Connection.Open()
'            mSqlCommand.ExecuteNonQuery()
'            mSqlCommand.Connection.Close()
'            Return True

'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function


'    Public Function Clear() As Boolean
'        Try
'            mID = 0
'            mCode = String.Empty
'            mEngName = String.Empty
'            mArbName = String.Empty
'            mArbName4S = String.Empty
'            mRemarks = String.Empty
'            mCompanyID = 0
'            mRegUserID = 0
'            mRegDate = Nothing
'            mCancelDate = Nothing

'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function


'    Public Function FirstRecord() As Boolean
'        Dim StrSelectCommand As String
'        Try
'            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
'            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code ASC"
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code ASC"
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


'    Public Function LastRecord() As Boolean
'        Dim StrSelectCommand As String
'        Try
'            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
'            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code DESC"
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code DESC"
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


'    Public Function NextRecord() As Boolean
'        Dim StrSelectCommand As String
'        Try
'            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
'            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code >'" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code ASC"
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code >'" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code ASC"
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


'    Public Function previousRecord() As Boolean
'        Dim StrSelectCommand As String
'        Try
'            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
'            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code < '" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code DESC"
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code < '" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code DESC"
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


'    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
'        Dim ObjDataRow As DataRow
'        Dim StrSelectCommand As String
'        Dim ObjDataset As New DataSet
'        Dim Item As Global.System.Web.UI.WebControls.ListItem
'        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
'        Try

'            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
'            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
'            DdlValues.Items.Clear()

'            If NullNode Then
'                Item = New Global.System.Web.UI.WebControls.ListItem
'                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ]")
'                Item.Value = 0
'                DdlValues.Items.Add(Item)
'            End If

'            For Each ObjDataRow In ObjDataset.Tables(0).Rows
'                Item = New Global.System.Web.UI.WebControls.ListItem
'                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
'                If (Item.Text.Trim = "") Then
'                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
'                End If
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

'    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList) As Boolean
'        Dim ObjDataRow As DataRow
'        Dim StrCommandString As String
'        Dim ObjDataset As New DataSet
'        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
'        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
'        Try

'            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID
'            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
'            DdlValues.ValueListItems.Clear()

'            For Each ObjDataRow In ObjDataset.Tables(0).Rows
'                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
'                Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
'                If (Item.DisplayText.Trim = "") Then
'                    Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
'                End If
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

'    Public Function GetEvaluationDetailsByIDOrEvalutionType(ByVal IntMainEvaluationID As Integer, ByVal IntEvaluationTypeID As Integer) As DataSet
'        Try
'            Dim retDS As DataSet
'            retDS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "GetEvaluationDetailsByIDOrEvalutionType", IntMainEvaluationID, IntEvaluationTypeID)
'            Return retDS
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'            Return Nothing
'        End Try
'    End Function

'    Public Function SaveEvaluationDetails(ByVal uwgDetails As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal IntEvalMainID As Integer) As Boolean
'        Dim strCmd As String = "Set DateFormat dmy ;"
'        Try
'            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgDetails.Rows

'                If row.Cells.FromKey("ID").Value > 0 Then
'                    strCmd &= " Update hrs_EvaluationMainElementSubElements " & _
'                              " Set Value = " & IIf(IsNothing(row.Cells.FromKey("Value").Value), 0, row.Cells.FromKey("Value").Value) & " " & _
'                              " Where ID = " & row.Cells.FromKey("ID").Value & ";"

'                Else
'                    strCmd &= " Insert Into hrs_EvaluationMainElementSubElements (EvaluationMainElementID,EvaluationSubElementID,Value,RegUserID)" & _
'                              " Values (" & IntEvalMainID & "," & _
'                              row.Cells.FromKey("EvaluationSubElementID").Value & "," & _
'                              IIf(IsNothing(row.Cells.FromKey("Value").Value), 0, row.Cells.FromKey("Value").Value) & "," & _
'                              Me.DataBaseUserRelatedID & ");"
'                End If

'            Next

'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = strCmd
'            mSqlCommand.Connection.Open()
'            mSqlCommand.ExecuteNonQuery()
'            mSqlCommand.Connection.Close()
'            Return True

'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function



'#End Region

'#Region "Class Private Function"

'    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
'        Try
'            With Ds.Tables(0).Rows(0)
'                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
'                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
'                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
'                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
'                mArbName4S = mDataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
'                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
'                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
'                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
'                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
'                mEvaluationTotal = mDataHandler.DataValue_Out(.Item("EvaluationTotal"), SqlDbType.Real)
'                mEvaluationTypeID = mDataHandler.DataValue_Out(.Item("EvaluationTypeID"), SqlDbType.Int, True)

'            End With
'            Return True
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal OperationType As OperationType) As Boolean
'        Try

'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
'            Select Case OperationType
'                Case ClsDataAcessLayer.OperationType.Save
'                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
'            End Select
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EvaluationTotal", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mEvaluationTotal, SqlDbType.Real)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EvaluationTypeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEvaluationTypeID, SqlDbType.Int, True)

'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function


'#End Region

'#Region "Class Destructors"

'    Public Sub finalized()
'        mDataSet.Dispose()
'    End Sub

'#End Region

'End Class
