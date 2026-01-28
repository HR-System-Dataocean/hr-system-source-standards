'Project           : Venus V.
'Module            : Sys (Report Writer Module)
'Date Created      : 18-06-2009
'Developer         : [Mohammed Al-Janazrh]
'Description       : 1-Implement Data Acess Layer of Sys_RreportsControlsProperties table with fields code,English Caption,
'                       Arabic Caption
'                    2-Allow searching
'                    3-Get list with all codes
'                    4-Implement functions save(), update() and delete() to allow DML with some critera
'                    5-Implement functions first(),last(),next() and previous() to allow navigation between records
'                  : B#g0010 [0256] 18-06-2009 Add _Forgin arrgument to data_ValueIn method in SetParameters Function 
'                  :                           And _Forgin arrgument to data_value_Out Method in GetParameters Function 
'                  :                           To avoid saving non-existing forign key in any table 
'                  :                           And convert the DBnull values to zero in case of forign key fields only 
'=========================================================================

Imports Venus.Shared

Public Class ClsReportsControlsProperties
    Inherits ClsDataAcessLayer
#Region "private members"
    Public Sub New(ByVal page As Global.System.Web.UI.Page)
        MyBase.New(page)
        mTable = " sys_ReportsControlsProperties"
        mInsertParameter = "FieldName,IsHidden,EngCaption,ArbCaption,BackColor,ForeColor,FontName,FontSize,IsBold,IsItalic,IsUnderLine,IsBoarded,ControlLeft,ControlTop,ControlHight,ControlWidth,Section,ImagePath"
        mInsertParameterValues = " @FieldName,@IsHidden,@EngCaption,@ArbCaption,@BackColor,@ForeColor,@FontName,@FontSize,@IsBold,@IsItalic,@IsUnderLine,@IsBoarded,@ControlLeft,@ControlTop,@ControlHight,@ControlWidth,@Section,@ImagePath"
        mUpdateParameter = " FieldName=@FieldName,IsHidden=@IsHidden,EngCaption=@EngCaption,ArbCaption=@ArbCaption,BackColor=@BackColor,ForeColor=@ForeColor,FontName=@FontName,FontSize=@FontSize,IsBold=@IsBold,IsItalic=@IsItalic,IsUnderLine=@IsUnderLine,IsBoarded=@IsBoarded,ControlLeft=@ControlLeft,ControlTop=@ControlTop,ControlHight=@ControlHight,ControlWidth=@ControlWidth,Section=@Section,ImagePath=@ImagePath "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
    End Sub

    Private mId As Integer
    Private mFieldName As String
    Private mIsHidden As Boolean
    Private mEngCaption As String
    Private mArbCaption As String
    Private mBackColor As String
    Private mForeColor As String
    Private mFontName As String
    Private mFontSize As Integer
    Private mIsBold As Boolean
    Private mIsItalic As Boolean
    Private mIsUnderLine As Boolean
    Private mIsBoarded As Boolean
    Private mControlLeft As Double
    Private mControlTop As Double
    Private mControlWidth As Double
    Private mControlHight As Double
    Private mSection As Int16
    Private mImage As String

#End Region
#Region "Public property"
    Public Property ID() As Integer
        Get
            Return mId
        End Get
        Set(ByVal value As Integer)
            mId = value
        End Set
    End Property
    Public Property FieldName() As String
        Get
            Return mFieldName
        End Get
        Set(ByVal value As String)
            mFieldName = value
        End Set
    End Property
    Public Property IsHidden() As Boolean
        Get
            Return mIsHidden
        End Get
        Set(ByVal value As Boolean)
            mIsHidden = value

        End Set
    End Property
    Public Property EngCaption() As String
        Get
            Return mEngCaption
        End Get
        Set(ByVal value As String)
            mEngCaption = value
        End Set
    End Property
    Public Property ArbCaption() As String
        Get
            Return mArbCaption
        End Get
        Set(ByVal value As String)
            mArbCaption = value

        End Set
    End Property
    Public Property BackColor() As String
        Get
            Return mBackColor

        End Get
        Set(ByVal value As String)
            mBackColor = value

        End Set
    End Property
    Public Property ForeColor() As String
        Get
            Return mForeColor

        End Get
        Set(ByVal value As String)
            mForeColor = value

        End Set
    End Property
    Public Property FontName() As String
        Get
            Return mFontName

        End Get
        Set(ByVal value As String)
            mFontName = value
        End Set
    End Property
    Public Property FontSize() As Integer
        Get
            Return mFontSize

        End Get
        Set(ByVal value As Integer)
            mFontSize = value
        End Set
    End Property
    Public Property IsBold() As Boolean
        Get
            Return mIsBold
        End Get
        Set(ByVal value As Boolean)
            mIsBold = value
        End Set
    End Property
    Public Property IsItalic() As Boolean
        Get
            Return mIsItalic

        End Get
        Set(ByVal value As Boolean)
            mIsItalic = value
        End Set
    End Property
    Public Property IsUnderLine() As Boolean
        Get
            Return mIsUnderLine

        End Get
        Set(ByVal value As Boolean)
            mIsUnderLine = value

        End Set
    End Property
    Public Property IsBoarded() As Boolean
        Get
            Return mIsBoarded
        End Get
        Set(ByVal value As Boolean)
            mIsBoarded = value
        End Set
    End Property
    Public Property ControlLeft() As Double
        Get
            Return mControlLeft
        End Get
        Set(ByVal value As Double)
            mControlLeft = value
        End Set
    End Property
    Public Property ControlTop() As Double
        Get
            Return mControlTop
        End Get
        Set(ByVal value As Double)
            mControlTop = value
        End Set
    End Property
    Public Property ControlHight() As Double
        Get
            Return mControlHight
        End Get
        Set(ByVal value As Double)
            mControlHight = value
        End Set
    End Property
    Public Property ControlWidth() As Double
        Get
            Return mControlWidth
        End Get
        Set(ByVal value As Double)
            mControlWidth = value
        End Set
    End Property
    Public Property Section() As Int16
        Get
            Return mSection
        End Get
        Set(ByVal value As Int16)
            mSection = value
        End Set
    End Property
    Public Property Image() As String
        Get
            Return mImage
        End Get
        Set(ByVal value As String)
            mImage = value
        End Set
    End Property
#End Region
#Region "Public Function"
    '========================================================================
    'ProcedureName  :  Update
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  update row that match with critera
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :  04-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By ID "
            End If
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            StrSelectCommand = StrSelectCommand & orderByStr
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                Getparameter(mDataSet)
            Else
                Clear()
            End If
            If mID > 0 Then
                Return True
            End If
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  Update
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  update row that match with critera
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :  04-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Save() As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, "Save")
            mSqlCommand.Connection.Open()
            Return mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Return 0
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  Update
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  update row that match with critera
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :  04-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Update(ByVal Filter As String) As Integer
        Dim StrUpdateCommand As String = String.Empty

        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand, "Update")
            'CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  Delete
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Delete record that match critera
    '                  and return true if operation done otherwise report errors in ErrorPage                    
    'Developer      :  DataOcean  
    'Date Created   :  04-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String = String.Empty
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            SetParameter(mSqlCommand, "Delete")
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   :  04-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function FirstRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & mGroupID & "),0) <> 1  " & IIf(Filter.Trim <> "", " And " & Filter, "") & "  ORDER BY ID ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                Getparameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   :  04-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function LastRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & mGroupID & "),0) <> 1  " & IIf(Filter.Trim <> "", " And " & Filter, "") & "  ORDER BY ID DESC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                Getparameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   :  04-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function NextRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >'" & mId & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & mGroupID & "),0) <> 1  " & IIf(Filter.Trim <> "", " And " & Filter, "") & "  ORDER BY ID ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                Getparameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   :  04-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function previousRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < '" & mId & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & mGroupID & "),0) <> 1  " & IIf(Filter.Trim <> "", " And " & Filter, "") & "  ORDER BY ID DESC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                Getparameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function
    Public Sub Clear()
        Try
            mFieldName = String.Empty
            mIsHidden = False
            mEngCaption = String.Empty
            mArbCaption = String.Empty
            mBackColor = String.Empty
            mForeColor = String.Empty
            mFontName = String.Empty
            mFontSize = 0
            mIsBold = False
            mIsItalic = False
            mIsUnderLine = False
            mIsBoarded = False
            mControlLeft = 0
            mControlTop = 0
            mControlWidth = 0
            mControlHight = 0
            mSection = 0
            mImage = Nothing
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserRelatedID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Sub
    Public Function ChcekDiffrance(ByVal OldCriteria As DataTable, ByVal NewCriteria As DataTable) As DataTable
        Dim ObjFinalCriteria As New DataTable
        Dim ObjFinalRow As DataRow
        Try
            ObjFinalCriteria = OldCriteria.Clone
            For Each ObjOldRow As DataRow In OldCriteria.Rows
                ObjFinalRow = ObjFinalCriteria.NewRow
                ObjFinalRow("FieldName") = ObjOldRow("FieldName")
                ObjFinalRow("IsHidden") = ObjOldRow("IsHidden")
                ObjFinalRow("EngCaption") = ObjOldRow("EngCaption")
                ObjFinalRow("ArbCaption") = ObjOldRow("ArbCaption")
                ObjFinalRow("BackColor") = ObjOldRow("BackColor")
                ObjFinalRow("ForeColor") = ObjOldRow("ForeColor")
                ObjFinalRow("FontName") = ObjOldRow("FontName")
                ObjFinalRow("FontSize") = ObjOldRow("FontSize")
                ObjFinalRow("IsBold") = ObjOldRow("IsBold")
                ObjFinalRow("IsItalic") = ObjOldRow("IsItalic")
                ObjFinalRow("IsBoarded") = ObjOldRow("IsBoarded")
                ObjFinalRow("ControlLeft") = ObjOldRow("ControlLeft")
                ObjFinalRow("ControlTop") = ObjOldRow("ControlTop")
                ObjFinalRow("ControlHight") = ObjOldRow("ControlHight")
                ObjFinalRow("ControlWidth") = ObjOldRow("ControlWidth")
                ObjFinalRow("Section") = ObjOldRow("Section")
                ObjFinalRow("ImagePath") = ObjOldRow("ImagePath")
                ObjFinalRow("DependOnPaperSize") = ObjOldRow("DependOnPaperSize")
                ObjFinalRow("Alignment") = ObjOldRow("Alignment")
                ObjFinalCriteria.Rows.Add(ObjFinalRow)
            Next
            Return ObjFinalCriteria
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserRelatedID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
            Return Nothing
        End Try
    End Function
    'Public Function SaveMultiControls(ByVal Controls As ArrayList) As Boolean
    '    Dim StrCommandString As String = "Delete From sys_ReportsControlsProperties;" & vbNewLine
    '    Try
    '        For Each control As Object In Controls
    '            StrCommandString &= "Insert Into sys_ReportsControlsProperties(FieldName,EngCaption,ArbCaption,IsHidden,BackColor,ForeColor,FontName,FontSize,IsBold,IsItalic,IsUnderLine,IsBoarded,ControlLeft,ControlTop,ControlHight,ControlWidth,Section,ImagePath,DependOnPaperSize,Alignment)Select " & _
    '            "FiledName ='" & CType(control, ClsReportsControlField).FieldName & "'," & _
    '            "EngCaption ='" & CType(control, ClsReportsControlField).EnglishCaption.Replace("'", "''") & "'," & _
    '            "ArbCaption ='" & CType(control, ClsReportsControlField).ArabicCaption.Replace("'", "''") & "'," & _
    '            "IsHidden ='" & CType(control, ClsReportsControlField).Hidden & "'," & _
    '            "BackColor ='" & CType(control, ClsReportsControlField).BackColor.Name & "'," & _
    '            "ForeColor ='" & CType(control, ClsReportsControlField).ForeColor.Name & "'," & _
    '            "FontName ='" & CType(control, ClsReportsControlField).Font.Name & "'," & _
    '            "FontSize =" & CType(control, ClsReportsControlField).Font.Size & "," & _
    '            "IsBold ='" & CType(control, ClsReportsControlField).Font.Bold & "'," & _
    '            "IsItalic ='" & CType(control, ClsReportsControlField).Font.Italic & "'," & _
    '            "IsUnderLine ='" & CType(control, ClsReportsControlField).Font.Underline & "'," & _
    '            "IsBoarded ='" & CType(control, ClsReportsControlField).Boarderd & "'," & _
    '            "ControlLeft =" & CType(control, ClsReportsControlField).Left & "," & _
    '            "ControlTop =" & CType(control, ClsReportsControlField).Top & "," & _
    '            "ControlHight =" & CType(control, ClsReportsControlField).Hight & "," & _
    '            "ControlWidth =" & CType(control, ClsReportsControlField).Width & "," & _
    '            "Section =" & CType(control, ClsReportsControlField).Section & "," & _
    '            "ImagePath ='" & CType(control, ClsReportsControlField).ImagePath & "'," & _
    '            "DependOnPaperSize ='" & IIf(CType(control, ClsReportsControlField).SizeType = ClsReportsControlField.eSizeType.DependOnPaperSize, True, False) & "'," & _
    '            "Alignment =" & CType(control, ClsReportsControlField).Alignment & ";" & vbNewLine
    '        Next
    '        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionSting, CommandType.Text, StrCommandString)
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function
#End Region
#Region "Private Function"
    '========================================================================
    'ProcedureName  :  GetParameter
    'Module         :  Sys (Report Writer Module)
    'Project        :  Venus V.
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[Mohammed Al-Janazrh]   
    'Date Created   :20-06-2009
    'Modifacations  :
    'Calls          :
    'From           :Find()
    '               :FirstRecord()
    '               :LastRecord()
    '               :PreviousRecord()
    '               :NextRecord()
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Ds             :DataSet     :used its attributes to assign them to private attributes
    '========================================================================
    Private Sub GetParameter(ByVal Ds As DataSet)
        Try
            With Ds.Tables(0).Rows(0)
                ID = DataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                mFieldName = DataHandler.DataValue_Out(.Item("FieldName"), SqlDbType.VarChar)
                mIsHidden = DataHandler.DataValue_Out(.Item("IsHidden"), SqlDbType.Bit)
                mEngCaption = DataHandler.DataValue_Out(.Item("EngCaption"), SqlDbType.VarChar)
                mArbCaption = DataHandler.DataValue_Out(.Item("ArbCaption"), SqlDbType.VarChar)
                mBackColor = DataHandler.DataValue_Out(.Item("BackColor"), SqlDbType.VarChar)
                mForeColor = DataHandler.DataValue_Out(.Item("ForeColor"), SqlDbType.VarChar)
                mFontName = DataHandler.DataValue_Out(.Item("FontName"), SqlDbType.VarChar)
                mFontSize = DataHandler.DataValue_Out(.Item("FontSize"), SqlDbType.Int)
                mIsBold = DataHandler.DataValue_Out(.Item("IsBold"), SqlDbType.Bit)
                mIsItalic = DataHandler.DataValue_Out(.Item("IsItalic"), SqlDbType.Bit)
                mIsUnderLine = DataHandler.DataValue_Out(.Item("IsUnderLine"), SqlDbType.Bit)
                mIsBoarded = DataHandler.DataValue_Out(.Item("IsBoarded"), SqlDbType.Bit)
                mControlLeft = DataHandler.DataValue_Out(.Item("ControlLeft"), SqlDbType.Float)
                mControlTop = DataHandler.DataValue_Out(.Item("ControlTop"), SqlDbType.Float)
                mControlHight = DataHandler.DataValue_Out(.Item("ControlHight"), SqlDbType.Float)
                mControlWidth = DataHandler.DataValue_Out(.Item("ControlWidth"), SqlDbType.Float)
                mSection = DataHandler.DataValue_Out(.Item("Section"), SqlDbType.Int)
                mImage = DataHandler.DataValue_Out(.Item("ImagePath"), SqlDbType.VarChar)
            End With
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserRelatedID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Sub
    '========================================================================
    'ProcedureName  :  SetParameter
    'Module         :  Sys (Report Writer Module)
    'Project        :  Venus V.
    'Description    :  Assign parameters of sql command  with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[Mohammed Al-Janazrh]   
    'Date Created   :20-06-2009
    'Modifacations  :
    'Calls          :
    'From           :Save()
    '               :Update()
    '               :Delete()
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Sub SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String)
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FieldName", SqlDbType.VarChar)).Value = DataHandler.DataValue_In(mFieldName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsHidden", SqlDbType.Bit)).Value = DataHandler.DataValue_In(mIsHidden, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngCaption", SqlDbType.VarChar)).Value = DataHandler.DataValue_In(mEngCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbCaption", SqlDbType.VarChar)).Value = DataHandler.DataValue_In(mArbCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BackColor", SqlDbType.VarChar)).Value = DataHandler.DataValue_In(mBackColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ForeColor", SqlDbType.VarChar)).Value = DataHandler.DataValue_In(mForeColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FontName", SqlDbType.VarChar)).Value = DataHandler.DataValue_In(mFontName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FontSize", SqlDbType.Int)).Value = DataHandler.DataValue_In(mFontSize, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsBold", SqlDbType.Bit)).Value = DataHandler.DataValue_In(mIsBold, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsItalic", SqlDbType.Bit)).Value = DataHandler.DataValue_In(mIsItalic, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsUnderLine", SqlDbType.Bit)).Value = DataHandler.DataValue_In(mIsUnderLine, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsBoarded", SqlDbType.Bit)).Value = DataHandler.DataValue_In(mIsBoarded, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ControlLeft", SqlDbType.Float)).Value = DataHandler.DataValue_In(mControlLeft, SqlDbType.Float)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ControlTop", SqlDbType.Float)).Value = DataHandler.DataValue_In(mControlTop, SqlDbType.Float)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ControlHight", SqlDbType.Float)).Value = DataHandler.DataValue_In(mControlHight, SqlDbType.Float)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ControlWidth", SqlDbType.Float)).Value = DataHandler.DataValue_In(mControlWidth, SqlDbType.Float)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Section", SqlDbType.Int)).Value = DataHandler.DataValue_In(mSection, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Image", SqlDbType.VarChar)).Value = DataHandler.DataValue_In(mImage, SqlDbType.VarChar)

        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserRelatedID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Sub
#End Region
#Region "Class Destructors"
    Public Sub finalized()
        mDataSet.Dispose()
    End Sub
#End Region

End Class
