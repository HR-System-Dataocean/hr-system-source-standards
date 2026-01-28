
'================================================
'Project :
'Module : 
'Screen :
'Developer :
'Date Created : 
'Description :
'Modifications:
'                : [0256] E#0001 21-04-2008 Change the documentation code of the page 
'                : [0260] B#0001 21-04-2008 Ignore saving null values into max,,,,,,Fields 
'
'=================================================

Public Class Clssys_FormsControls
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
        mTable = " sys_FormsControls "
        mInsertParameter = " Name,EngCaption,ArbCaption,Compulsory,Format,ToolTip,MaxLength,IsNumeric,IsHide,FocusOnStartUp,Rank,FormID,FieldID,RegUserID,RegComputerID,IsArabic,ArbFormat,ArbToolTip,MinValue ,MaxValue,SearchID "
        mInsertParameterValues = " @Name,@EngCaption,@ArbCaption,@Compulsory,@Format,@ToolTip,@MaxLength,@IsNumeric,@IsHide,@FocusOnStartUp,@Rank,@FormID,@FieldID,@RegUserID,@RegComputerID,@IsArabic,@ArbFormat,@ArbToolTip,@MinValue ,@MaxValue,@SearchID  "
        mUpdateParameter = " Name=@Name,EngCaption=@EngCaption,ArbCaption=@ArbCaption,Compulsory=@Compulsory,Format=@Format,ToolTip=@ToolTip,MaxLength=@MaxLength,IsNumeric=@IsNumeric,IsHide=@IsHide,FocusOnStartUp=@FocusOnStartUp,Rank=@Rank,FormID=@FormID,FieldID=@FieldID,RegUserID=@RegUserID,RegComputerID=@RegComputerID,IsArabic=@IsArabic,ArbFormat=@ArbFormat,ArbToolTip=@ArbToolTip,MinValue=@MinValue ,MaxValue=@MaxValue,SearchID=@SearchID"
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

#End Region

#Region "Private Members"

    Private mArbFormat As String
    Private mArbToolTip As String
    Private mMinValue As Double
    Private mMaxValue As Double
    Private mSearchID As Integer
    Private mID As Integer
    Private mName As String
    Private mEngCaption As String
    Private mArbCaption As String
    Private mCompulsory As Boolean
    Private mFormat As String
    Private mToolTip As String
    Private mMaxLength As Integer
    Private mIsNumeric As Boolean
    Private mIsHide As Boolean
    Private mFocusOnStartUp As Boolean
    Private mRank As Integer
    Private mFormID As Integer
    Private mFieldID As Integer
    Private mRegUserID As Integer
    Private mRegComputerID As Integer
    Private mRegDate As Date
    Private mCancelDate As Date
    Private mIsArabic As Boolean

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
    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal Value As String)
            mName = Value
        End Set
    End Property
    Public Property EngCaption() As String
        Get
            Return mEngCaption
        End Get
        Set(ByVal Value As String)
            mEngCaption = Value
        End Set
    End Property
    Public Property ArbCaption() As String
        Get
            Return mArbCaption
        End Get
        Set(ByVal Value As String)
            mArbCaption = Value
        End Set
    End Property
    Public Property Compulsory() As Boolean
        Get
            Return mCompulsory
        End Get
        Set(ByVal Value As Boolean)
            mCompulsory = Value
        End Set
    End Property
    Public Property Format() As String
        Get
            Return mFormat
        End Get
        Set(ByVal Value As String)
            mFormat = Value
        End Set
    End Property
    Public Property ToolTip() As String
        Get
            Return mToolTip
        End Get
        Set(ByVal Value As String)
            mToolTip = Value
        End Set
    End Property
    Public Property MaxLength() As Integer
        Get
            Return mMaxLength
        End Get
        Set(ByVal Value As Integer)
            mMaxLength = Value
        End Set
    End Property
    Public Property IsNumeric() As Boolean
        Get
            Return mIsNumeric
        End Get
        Set(ByVal Value As Boolean)
            mIsNumeric = Value
        End Set
    End Property
    Public Property IsHide() As Boolean
        Get
            Return mIsHide
        End Get
        Set(ByVal Value As Boolean)
            mIsHide = Value
        End Set
    End Property
    Public Property FocusOnStartUp() As Boolean
        Get
            Return mFocusOnStartUp
        End Get
        Set(ByVal Value As Boolean)
            mFocusOnStartUp = Value
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
    Public Property FormID() As Integer
        Get
            Return mFormID
        End Get
        Set(ByVal Value As Integer)
            mFormID = Value
        End Set
    End Property
    Public Property FieldID() As Integer
        Get
            Return mFieldID
        End Get
        Set(ByVal Value As Integer)
            mFieldID = Value
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
    Public Property RegDate() As Date
        Get
            Return mRegDate
        End Get
        Set(ByVal Value As Date)
            mRegDate = Value
        End Set
    End Property
    Public Property CancelDate() As Date
        Get
            Return mCancelDate
        End Get
        Set(ByVal Value As Date)
            mCancelDate = Value
        End Set
    End Property

    Public Property IsArabic() As Boolean
        Get
            Return mIsArabic
        End Get
        Set(ByVal value As Boolean)
            mIsArabic = value
        End Set
    End Property



    Public Property ArbFormat() As String
        Get
            Return mArbFormat

        End Get
        Set(ByVal value As String)
            mArbFormat = value
        End Set
    End Property




    Public Property ArbToolTip() As String
        Get
            Return mArbToolTip
        End Get
        Set(ByVal value As String)
            mArbToolTip = value
        End Set
    End Property

    Public Property MinValue() As Double
        Get
            Return mMinValue
        End Get
        Set(ByVal value As Double)
            mMinValue = value
        End Set
    End Property

    Public Property MaxValue() As Double
        Get
            Return mMaxValue
        End Get
        Set(ByVal value As Double)
            mMaxValue = value
        End Set
    End Property

    Public Property SearchID() As Integer
        Get
            Return mSearchID
        End Get
        Set(ByVal value As Integer)
            mSearchID = value
        End Set
    End Property

#End Region

#Region "Public Function"

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And " & Filter, "  Where IsNull(CancelDate,'')=''  ")
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From sys_FormsControls Where " & Filter
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
            mErrorHandler.RecordExceptions_DataBase(strSQL, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
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
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
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
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
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
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function DeleteAllRows(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = "DELETE FROM " & mTable & " WHERE " & Filter
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
            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function SaveFormsControls(ByRef DG As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal intFormID As Integer, ByVal DeleteAll As Boolean) As Boolean
        Try
            Dim strCmd As String = CONFIG_DATEFORMAT
            Dim strFormat As String
            Dim strArbFormat As String
            If DeleteAll Then

                strCmd &= " delete from " & mTable & " where FormID = " & intFormID & ";"

            End If

            Dim bFirst As Boolean = True
            '[0260] B#0001  ================ [Start]
            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In DG.Rows

                If Not IsNothing(row.Cells(CFormat).Value) Then
                    If row.Cells(CFormat).Value.ToString.Trim = "" Then
                        strFormat = " NULL "
                    Else
                        strFormat = "'" & row.Cells(CFormat).Value & "'"
                    End If
                Else
                    strFormat = " NULL "
                End If


                If Not IsNothing(row.Cells(CArabicFormat).Value) Then
                    If row.Cells(CArabicFormat).Value.ToString.Trim = "" Then
                        strArbFormat = " NULL "
                    Else
                        strArbFormat = "'" & row.Cells(CArabicFormat).Value & "'"
                    End If
                Else
                    strArbFormat = " NULL "
                End If


                If (row.Cells(1).Value <> Nothing) Then
                    If (bFirst = True) Then
                        strCmd &= " insert into " & mTable & " ([FormID],[Name],[EngCaption],[ArbCaption],[Compulsory],[Format],[ArbFormat],[ToolTip],[ArbToolTip],[MaxLength],[IsNumeric],[IsHide],[FocusOnStartUp],[Rank],[MinValue],[MaxValue],[FieldID],[SearchID],[RegUserID],[RegComputerID],[IsArabic]) " & vbNewLine
                    End If
                    strCmd &= " " & IIf(bFirst, " select ", " union all select ") & _
                    " " & intFormID & ",'" & row.Cells(CName).Value & "','" & _
                    row.Cells(CEngDesc).Value & "','" & _
                    row.Cells(CArbDesc).Value & "','" & _
                    row.Cells(CCompuslary).Value & "'," & _
                    strFormat & "," & _
                    strArbFormat & ",'" & _
                    row.Cells(CTooltip).Value & "','" & _
                    row.Cells(CArabicTooltip).Value & "'," & _
                    IIf(row.Cells(CMaxLenght).Value = Nothing, "0", mDataHandler.DataValue_Out(row.Cells(CMaxLenght).Value, SqlDbType.Int)) & ",'" & _
                     row.Cells(CIsNumeric).Value & "','" & row.Cells(CIsHide).Value & "','" & _
                     row.Cells(CFocusOnStart).Value & "'," & _
                     IIf(row.Cells(CRank).Value = Nothing, "0", mDataHandler.DataValue_Out(row.Cells(CRank).Value, SqlDbType.Int)) & "," & _
                     IIf(row.Cells(CMinValue).Value = Nothing, "0", mDataHandler.DataValue_Out(row.Cells(CMinValue).Value, SqlDbType.Int)) & "," & _
                     IIf(row.Cells(CMaxValue).Value = Nothing, "0", mDataHandler.DataValue_Out(row.Cells(CMaxValue).Value, SqlDbType.Int)) & "," & _
                     IIf(row.Cells(CField).Value = Nothing Or row.Cells(CField).Value = 0, "0", mDataHandler.DataValue_Out(row.Cells(CField).Value, SqlDbType.Int)) & "," & _
                     IIf(row.Cells(CSearchID).Value = Nothing, "0", mDataHandler.DataValue_Out(row.Cells(CSearchID).Value, SqlDbType.Int)) & "," & _
                     mRegUserID & "," & mRegComputerID & ",'" & row.Cells(CIsArabic).Value & "'" & vbNewLine
                    bFirst = False
                End If
            Next
            '[0260] B#0001  ================ [End]

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

    Public Function Clear() As Boolean
        Try
            mID = 0
            mName = String.Empty
            mEngCaption = String.Empty
            mArbCaption = String.Empty
            mCompulsory = False
            mFormat = String.Empty
            mToolTip = String.Empty
            mMaxLength = 0
            mIsNumeric = False
            mIsHide = False
            mFocusOnStartUp = False
            mRank = 0
            mFormID = 0
            mFieldID = 0
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing

            mArbFormat = String.Empty
            mArbToolTip = String.Empty
            mMinValue = 0
            mMaxValue = 0
            mSearchID = 0

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' ORDER BY ID ASC"
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

    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')=''  ORDER BY ID DESC"
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

    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' ORDER BY ID ASC"
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

    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID <" & mID & " And isNull(CancelDate,'')='' ORDER BY ID DESC"
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

#Region "Public Functions"

    Public Function ParsText(ByVal StrString As String) As ArrayList
        Dim StrArray As New ArrayList
        Dim StrCurrentString As String = String.Empty
        Dim BolArrgmentStarted As Boolean
        For Each chrCharacter As Char In StrString
            If chrCharacter = "<" Then
                StrCurrentString = String.Empty
                BolArrgmentStarted = True
                StrCurrentString &= chrCharacter
            ElseIf chrCharacter = ">" And BolArrgmentStarted Then
                StrCurrentString &= chrCharacter
                StrArray.Add(StrCurrentString)
                BolArrgmentStarted = False
                StrCurrentString = String.Empty
            Else
                StrCurrentString &= chrCharacter
            End If
        Next
        Return StrArray
    End Function

    Public Function GetParameters(ByVal Array As ArrayList) As DataTable
        Try
            Dim DtTable As New DataTable
            Dim DtRow As Data.DataRow
            Dim StrCurrentString As String
            Dim StrValue As String = String.Empty
            Dim StrType As String = String.Empty
            CreateColumns(DtTable)
            For Each ObjItem As Object In Array
                StrCurrentString = ObjItem
                StrValue = GetValue(StrCurrentString, "ID=")
                If StrCurrentString.Contains("igtxt") Then
                    StrType = GetValueType(StrCurrentString, "<igtxt:")
                Else
                    StrType = GetValueType(StrCurrentString, "<asp:")
                End If
                If StrValue <> "" Then
                    If StrValue = "name" Or StrValue = "realname" Or StrValue = "value" Or StrValue = "TargetControl" Then
                        Continue For
                    End If
                    If StrType = "Label" Or StrType = "TextBox" Or StrType = "DropDownList" Or StrType = "WebDateTimeEdit" Or StrType = "WebMaskEdit" Then
                        DtRow = DtTable.NewRow
                        DtRow("Name") = StrValue
                        DtRow("Format") = StrType
                        DtTable.Rows.Add(DtRow)
                    End If
                End If
            Next
            Return DtTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CreateColumns(ByRef DtTable As DataTable) As DataTable
        Dim ClsObject As New Clssys_Objects(mPage)
        Dim ClsFields As New Clssys_Fields(mPage)
        Dim DtColumn As DataColumn
        ClsObject.Find(" Code = 'sys_FormsControls'")
        ClsFields.Find(" ObjectID = " & ClsObject.ID & " Order by ID")
        Dim ObjRow As Data.DataRow
        If ClsFields.DataSet.Tables(0).Rows.Count > 0 Then
            For Each ObjRow In ClsFields.DataSet.Tables(0).Rows
                DtColumn = New DataColumn
                DtColumn.DataType = Global.System.Type.GetType("System.String")
                DtColumn.ColumnName = ObjRow.Item("FieldName")
                DtTable.Columns.Add(DtColumn)
            Next
        End If

    End Function


    Public Function GetValue(ByVal StrString As String, ByVal Parameter As String) As String
        Dim StartIndex As Integer = 0
        Dim EndIndex As Integer = 0
        Dim IntNoofQut As Integer = 0
        Dim StrCurrentString As String = String.Empty
        Try
            StartIndex = StrString.IndexOf(Parameter)
            If StartIndex = -1 Then Return ""
            StrCurrentString = StrString.Substring(StartIndex + Len(Parameter) + 1)
            EndIndex = StrCurrentString.IndexOf("""")
            StrCurrentString = StrCurrentString.Substring(0, EndIndex)
            Return StrCurrentString
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function GetValueType(ByVal StrString As String, ByVal Parameter As String) As String
        Dim StartIndex As Integer = 0
        Dim EndIndex As Integer = 0
        Dim IntNoofQut As Integer = 0
        Dim StrCurrentString As String = String.Empty
        Try
            StartIndex = StrString.IndexOf(Parameter)
            If StartIndex = -1 Then Return ""
            StrCurrentString = StrString.Substring(StartIndex + Len(Parameter))
            EndIndex = StrCurrentString.IndexOf(" ")
            StrCurrentString = StrCurrentString.Substring(0, EndIndex)
            Return StrCurrentString
        Catch ex As Exception
            Return ""
        End Try
    End Function

#End Region

#End Region

#Region "Class Private Function"

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mName = mDataHandler.DataValue_Out(.Item("Name"), SqlDbType.VarChar)
                mEngCaption = mDataHandler.DataValue_Out(.Item("EngCaption"), SqlDbType.VarChar)
                mArbCaption = mDataHandler.DataValue_Out(.Item("ArbCaption"), SqlDbType.VarChar)
                mCompulsory = mDataHandler.DataValue_Out(.Item("Compulsory"), SqlDbType.Bit)
                mFormat = mDataHandler.DataValue_Out(.Item("Format"), SqlDbType.VarChar)
                mToolTip = mDataHandler.DataValue_Out(.Item("ToolTip"), SqlDbType.VarChar)
                mMaxLength = mDataHandler.DataValue_Out(.Item("MaxLength"), SqlDbType.Int)
                mIsNumeric = mDataHandler.DataValue_Out(.Item("IsNumeric"), SqlDbType.Bit)
                mIsHide = mDataHandler.DataValue_Out(.Item("IsHide"), SqlDbType.Bit)
                mFocusOnStartUp = mDataHandler.DataValue_Out(.Item("FocusOnStartUp"), SqlDbType.Bit)
                mRank = mDataHandler.DataValue_Out(.Item("Rank"), SqlDbType.Int)
                mFormID = mDataHandler.DataValue_Out(.Item("FormID"), SqlDbType.Int, True)
                mFieldID = mDataHandler.DataValue_Out(.Item("FieldID"), SqlDbType.Int, True)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mIsArabic = mDataHandler.DataValue_Out(.Item("IsArabic"), SqlDbType.Bit)

                mArbFormat = mDataHandler.DataValue_Out(.Item("ArbFormat"), SqlDbType.VarChar)
                mArbToolTip = mDataHandler.DataValue_Out(.Item("ArbToolTip"), SqlDbType.VarChar)
                mMinValue = mDataHandler.DataValue_Out(.Item("MinValue"), SqlDbType.Float)
                mMaxValue = mDataHandler.DataValue_Out(.Item("MaxValue"), SqlDbType.Float)
                mSearchID = mDataHandler.DataValue_Out(.Item("SearchID"), SqlDbType.Int, True)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Name", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngCaption", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbCaption", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Compulsory", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCompulsory, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Format", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFormat, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ToolTip", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mToolTip, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaxLength", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mMaxLength, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsNumeric", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsNumeric, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsHide", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsHide, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FocusOnStartUp", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mFocusOnStartUp, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Rank", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRank, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FormID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFormID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FieldID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFieldID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mRegDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CancelDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mCancelDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsArabic", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsArabic, SqlDbType.Bit)


            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbFormat", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbFormat, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbToolTip", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbToolTip, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MinValue", SqlDbType.Float)).Value = mDataHandler.DataValue_In(mMinValue, SqlDbType.Float)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaxValue", SqlDbType.Float)).Value = mDataHandler.DataValue_In(mMaxValue, SqlDbType.Float)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SearchID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mSearchID, SqlDbType.Int, True)

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
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

