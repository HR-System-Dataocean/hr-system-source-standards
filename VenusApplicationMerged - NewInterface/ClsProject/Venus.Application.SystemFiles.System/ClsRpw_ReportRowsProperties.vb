
'=========================================================================
'Created by : 0258
'Date : 30/12/2007
'                   Class: RowsProperties
'                   Table:
'=========================================================================

Public Class ClsRpw_ReportRowsProperties
    Inherits ClsDataAcessLayer

#Region "Class Constructor"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " rpw_ReportRowsProperties "
        mInsertParameter = " ReportID,ForeColor,BackColor,Font,FontIsItalic,FontIsBold,FontIsUnderLine,FontSize,RowHieght," & _
        "BorderColor,TopBorderColor,BottomBorderColor,LeftBorderColor,RightBorderColor,BorderStyle,TopBorderStyle," & _
        "BottomBorderStyle,LeftBorderStyle,RightBorderStyle,BorderWidth,TopBorderWidth,BottomBorderWidth,LeftBorderWidth," & _
        "RightBorderWidth "
        mInsertParameterValues = " @ReportID,@ForeColor,@BackColor,@Font,@FontIsItalic,@FontIsBold,@FontIsUnderLine,@FontSize,@RowHieght," & _
        "@BorderColor,@TopBorderColor,@BottomBorderColor,@LeftBorderColor,@RightBorderColor,@BorderStyle,@TopBorderStyle," & _
        "@BottomBorderStyle,@LeftBorderStyle,@RightBorderStyle,@BorderWidth,@TopBorderWidth,@BottomBorderWidth,@LeftBorderWidth," & _
        "@RightBorderWidth "
        mUpdateParameter = " ReportID=@ReportID, ForeColor=@ForeColor, BackColor=@BackColor, Font=@Font, FontIsItalic=@FontIsItalic, " & _
        "FontIsBold=@FontIsBold , FontIsUnderLine=@FontIsUnderLine, FontSize=@FontSize, RowHieght=@RowHieght, BorderColor=@BorderColor, " & _
        "TopBorderColor=@TopBorderColor, BottomBorderColor=@BottomBorderColor, LeftBorderColor=@LeftBorderColor, RightBorderColor=@RightBorderColor, " & _
        "BorderStyle=@BorderStyle, TopBorderStyle=@TopBorderStyle, BottomBorderStyle=@BottomBorderStyle, LeftBorderStyle=@LeftBorderStyle, " & _
        "RightBorderStyle=@RightBorderStyle, BorderWidth=@BorderWidth, TopBorderWidth=@TopBorderWidth, BottomBorderWidth=@BottomBorderWidth, " & _
        "LeftBorderWidth=@LeftBorderWidth, RightBorderWidth=@RightBorderWidth "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

#End Region

#Region "Private Members"
    Private mID As Integer
    Private mReportID As Integer
    Private mForeColor As String
    Private mBackColor As String
    Private mFont As String
    Private mFontIsItalic As Boolean
    Private mFontIsBold As Boolean
    Private mFontIsUnderLine As Boolean
    Private mFontSize As Integer
    Private mRowHieght As Integer
    Private mBorderColor As String
    Private mBorderStyle As Integer
    Private mBorderWidth As Integer
    Private mTopBorderColor As String
    Private mBottomBorderColor As String
    Private mLeftBorderColor As String
    Private mRightBorderColor As String
    Private mTopBorderStyle As Integer
    Private mBottomBorderStyle As Integer
    Private mLeftBorderStyle As Integer
    Private mRightBorderStyle As Integer
    Private mTopBorderWidth As Integer
    Private mBottomBorderWidth As Integer
    Private mLeftBorderWidth As Integer
    Private mRightBorderWidth As Integer
#End Region

#Region "Public Property"

    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal Value As Integer)
            mID = Value
        End Set
    End Property

    Public Property ReportID() As Integer
        Get
            Return mReportID
        End Get
        Set(ByVal Value As Integer)
            mReportID = Value
        End Set
    End Property

    Public Property ForeColor() As String
        Get
            Return mForeColor
        End Get
        Set(ByVal Value As String)
            mForeColor = Value
        End Set
    End Property

    Public Property BackColor() As String
        Get
            Return mBackColor
        End Get
        Set(ByVal Value As String)
            mBackColor = Value
        End Set
    End Property

    Public Property Font() As String
        Get
            Return mFont
        End Get
        Set(ByVal Value As String)
            mFont = Value
        End Set
    End Property

    Public Property FontIsItalic() As Boolean
        Get
            Return mFontIsItalic
        End Get
        Set(ByVal Value As Boolean)
            mFontIsItalic = Value
        End Set
    End Property

    Public Property FontIsBold() As Boolean
        Get
            Return mFontIsBold
        End Get
        Set(ByVal Value As Boolean)
            mFontIsBold = Value
        End Set
    End Property

    Public Property FontIsUnderLine() As Boolean
        Get
            Return mFontIsUnderLine
        End Get
        Set(ByVal Value As Boolean)
            mFontIsUnderLine = Value
        End Set
    End Property

    Public Property FontSize() As Integer
        Get
            Return mFontSize
        End Get
        Set(ByVal Value As Integer)
            mFontSize = Value
        End Set
    End Property

    Public Property RowHieght() As Integer
        Get
            Return mRowHieght
        End Get
        Set(ByVal Value As Integer)
            mRowHieght = Value
        End Set
    End Property

    Public Property BorderColor() As String
        Get
            Return mBorderColor
        End Get
        Set(ByVal Value As String)
            mBorderColor = Value
        End Set
    End Property

    Public Property BorderStyle() As Integer
        Get
            Return mBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mBorderStyle = Value
        End Set
    End Property

    Public Property BorderWidth() As Integer
        Get
            Return mBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mBorderWidth = Value
        End Set
    End Property

    Public Property TopBorderColor() As String
        Get
            Return mTopBorderColor
        End Get
        Set(ByVal Value As String)
            mTopBorderColor = Value
        End Set
    End Property

    Public Property BottomBorderColor() As String
        Get
            Return mBottomBorderColor
        End Get
        Set(ByVal Value As String)
            mBottomBorderColor = Value
        End Set
    End Property

    Public Property LeftBorderColor() As String
        Get
            Return mLeftBorderColor
        End Get
        Set(ByVal Value As String)
            mLeftBorderColor = Value
        End Set
    End Property

    Public Property RightBorderColor() As String
        Get
            Return mRightBorderColor
        End Get
        Set(ByVal Value As String)
            mRightBorderColor = Value
        End Set
    End Property

    Public Property TopBorderStyle() As Integer
        Get
            Return mTopBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mTopBorderStyle = Value
        End Set
    End Property

    Public Property BottomBorderStyle() As Integer
        Get
            Return mBottomBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mBottomBorderStyle = Value
        End Set
    End Property

    Public Property LeftBorderStyle() As Integer
        Get
            Return mLeftBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mLeftBorderStyle = Value
        End Set
    End Property

    Public Property RightBorderStyle() As Integer
        Get
            Return mRightBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mRightBorderStyle = Value
        End Set
    End Property

    Public Property TopBorderWidth() As Integer
        Get
            Return mTopBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mTopBorderWidth = Value
        End Set
    End Property

    Public Property BottomBorderWidth() As Integer
        Get
            Return mBottomBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mBottomBorderWidth = Value
        End Set
    End Property

    Public Property LeftBorderWidth() As Integer
        Get
            Return mLeftBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mLeftBorderWidth = Value
        End Set
    End Property

    Public Property RightBorderWidth() As Integer
        Get
            Return mRightBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mRightBorderWidth = Value
        End Set
    End Property

#End Region

#Region "Class Public Functions"

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = " Select ID From " & Me.mTable & " Where " & Filter
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

    Public Function Clear() As Boolean
        Try
            mID = 0
            mReportID = 0
            mForeColor = String.Empty
            mBackColor = String.Empty
            mFont = String.Empty
            mFontIsItalic = False
            mFontIsBold = False
            mFontIsUnderLine = False
            mFontSize = 0
            mRowHieght = 0
            mBorderColor = String.Empty
            mBorderStyle = String.Empty
            mBorderWidth = 0
            mTopBorderColor = String.Empty
            mBottomBorderColor = String.Empty
            mLeftBorderColor = String.Empty
            mRightBorderColor = String.Empty
            mTopBorderStyle = String.Empty
            mBottomBorderStyle = String.Empty
            mLeftBorderStyle = String.Empty
            mRightBorderStyle = String.Empty
            mTopBorderWidth = String.Empty
            mBottomBorderWidth = String.Empty
            mLeftBorderWidth = String.Empty
            mRightBorderWidth = String.Empty
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

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
                Case "BIT"
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

#End Region

#Region "Class Private Functions"

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                mReportID = mDataHandler.DataValue_Out(.Item("ReportID"), SqlDbType.Int)
                mForeColor = mDataHandler.DataValue_Out(.Item("ForeColor"), SqlDbType.VarChar)
                mBackColor = mDataHandler.DataValue_Out(.Item("BackColor"), SqlDbType.VarChar)
                mFont = mDataHandler.DataValue_Out(.Item("Font"), SqlDbType.VarChar)
                mFontIsItalic = mDataHandler.DataValue_Out(.Item("FontIsItalic"), SqlDbType.Bit)
                mFontIsBold = mDataHandler.DataValue_Out(.Item("FontIsBold"), SqlDbType.Bit)
                mFontIsUnderLine = mDataHandler.DataValue_Out(.Item("FontIsUnderLine"), SqlDbType.Bit)
                mFontSize = mDataHandler.DataValue_Out(.Item("FontSize"), SqlDbType.Int)
                mRowHieght = mDataHandler.DataValue_Out(.Item("RowHieght"), SqlDbType.Int)
                mBorderColor = mDataHandler.DataValue_Out(.Item("BorderColor"), SqlDbType.VarChar)
                mBorderStyle = mDataHandler.DataValue_Out(.Item("BorderStyle"), SqlDbType.VarChar)
                mBorderWidth = mDataHandler.DataValue_Out(.Item("BorderWidth"), SqlDbType.Int)
                mTopBorderColor = mDataHandler.DataValue_Out(.Item("TopBorderColor"), SqlDbType.VarChar)
                mBottomBorderColor = mDataHandler.DataValue_Out(.Item("BottomBorderColor"), SqlDbType.VarChar)
                mLeftBorderColor = mDataHandler.DataValue_Out(.Item("LeftBorderColor"), SqlDbType.VarChar)
                mRightBorderColor = mDataHandler.DataValue_Out(.Item("RightBorderColor"), SqlDbType.VarChar)
                mTopBorderStyle = mDataHandler.DataValue_Out(.Item("TopBorderStyle"), SqlDbType.VarChar)
                mBottomBorderStyle = mDataHandler.DataValue_Out(.Item("BottomBorderStyle"), SqlDbType.VarChar)
                mLeftBorderStyle = mDataHandler.DataValue_Out(.Item("LeftBorderStyle"), SqlDbType.Bit)
                mRightBorderStyle = mDataHandler.DataValue_Out(.Item("RightBorderStyle"), SqlDbType.Bit)
                mTopBorderWidth = mDataHandler.DataValue_Out(.Item("TopBorderWidth"), SqlDbType.Bit)
                mBottomBorderWidth = mDataHandler.DataValue_Out(.Item("BottomBorderWidth"), SqlDbType.Bit)
                mLeftBorderWidth = mDataHandler.DataValue_Out(.Item("LeftBorderWidth"), SqlDbType.Bit)
                mRightBorderWidth = mDataHandler.DataValue_Out(.Item("RightBorderWidth"), SqlDbType.Bit)

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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ForeColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mForeColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BackColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mBackColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Font", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFont, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FontIsItalic", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mFontIsItalic, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FontIsBold", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mFontIsBold, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FontIsUnderLine", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mFontIsUnderLine, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FontSize", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFontSize, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowHieght", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowHieght, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TopBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mTopBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BottomBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mBottomBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LeftBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mLeftBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RightBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRightBorderColor, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BorderStyle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mBorderStyle, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TopBorderStyle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mTopBorderStyle, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BottomBorderStyle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mBottomBorderStyle, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LeftBorderStyle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mLeftBorderStyle, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RightBorderStyle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRightBorderStyle, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BorderWidth", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mBorderWidth, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TopBorderWidth", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mTopBorderWidth, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BottomBorderWidth", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mBottomBorderWidth, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LeftBorderWidth", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mLeftBorderWidth, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RightBorderWidth", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRightBorderWidth, SqlDbType.VarChar)

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region

#Region "Class Destructor"

    Protected Overloads Sub finalized()
        mDataSet.Dispose()
    End Sub

#End Region

End Class
