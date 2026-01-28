'Project           : Venus V.
'Module            : Hrs (Human Resource Module)
'Date Created      : 09-08-2007
'Developer         : [0257]
'Description       : 1-Implement Data Acess Layer of hrs_Grades table with fields code,English name,
'                       Arabic name,Grade level and Regular hours
'                    2-Allow searching
'                    3-Get list with all codes
'                    4-Implement functions save(), update() and delete() to allow DML with some critera
'                    5-Implement functions first(),last(),next() and previous() to allow navigation between 
'                       records
'Modifications     :  11-09-2007 Add paidatvacation & OnceatPeriod Properties to class 
'                  : and add them to getparameters,setparameters and clear methods.  
'                  :  18-09-2007 aDD IntervalID To hrs_GradesTransactions    
'==========================================================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_GradesTransactions
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    '========================================================================
    'ProcedureName  :  Constractor 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    : Initialize insert ,update and delete commands
    'Developer      :[0257]   
    'Date Created   :09-08-2007
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_GradesTransactions "
        mInsertParameter = " GradeID,TransactionTypeID,MinValue,MaxValue,Remarks,RegUserID,RegComputerID,Paidatvacation,onceatperiod ,IntervalID "
        mInsertParameterValues = " @GradeID,@TransactionTypeID,@MinValue,@MaxValue,@Remarks,@RegUserID,@RegComputerID ,@paidatvacation , @onceatperiod ,@IntervalID"
        mUpdateParameter = " GradeID=@GradeID,TransactionTypeID=@TransactionTypeID,MinValue=@MinValue,MaxValue=@MaxValue,Remarks=@Remarks,RegUserID=@RegUserID,RegComputerID=@RegComputerID ,paidatvacation = @paidatvacation , onceatperiod=@onceatperiod ,IntervalID=@IntervalID "
        mSelectCommand = " Select hrs_GradesTransactions.ID,hrs_GradesTransactions.GradeID,hrs_GradesTransactions.TransactionTypeID,hrs_GradesTransactions.MinValue,hrs_GradesTransactions.MaxValue,hrs_GradesTransactions.Remarks,hrs_GradesTransactions.RegUserID,hrs_GradesTransactions.RegComputerID,hrs_GradesTransactions.RegDate,hrs_GradesTransactions.CancelDate,hrs_GradesTransactions.IntervalID ,hrs_TransactionsTypes.EngName,hrs_GradesTransactions.paidatvacation , hrs_GradesTransactions.onceatperiod  From  " & mTable & " INNER JOIN hrs_Grades ON hrs_GradesTransactions.GradeID=hrs_Grades.ID INNER JOIN hrs_TransactionsTypes ON hrs_GradesTransactions.TransactionTypeID=hrs_TransactionsTypes.ID "
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Object
    Private mGradeID As Integer
    Private mTransactionTypeID As Integer
    Private mMinValue As Double
    Private mMaxValue As Double



    Private mRemarks As String
    Private mCompanyID As Integer
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Private mPaidatVacation As Integer
    Private mOnceatPeriod As Boolean
    Private mIntervalID As Integer

#End Region
#Region "Public property"
    Public Property ID() As Object
        Get
            Return mID
        End Get
        Set(ByVal Value As Object)
            mID = Value
        End Set
    End Property
    Public Property GradeID() As Integer
        Get
            Return mGradeID
        End Get
        Set(ByVal Value As Integer)
            mGradeID = Value
        End Set
    End Property
    Public Property TransactionTypeID() As Integer
        Get
            Return mTransactionTypeID
        End Get
        Set(ByVal Value As Integer)
            mTransactionTypeID = Value
        End Set
    End Property
    Public Property MinValue() As Double
        Get
            Return mMinValue
        End Get
        Set(ByVal Value As Double)
            mMinValue = Value
        End Set
    End Property
    Public Property MaxValue() As Double
        Get
            Return mMaxValue
        End Get
        Set(ByVal Value As Double)
            mMaxValue = Value
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

    Public Property CompanyID() As Integer
        Get
            Return mCompanyID
        End Get
        Set(ByVal value As Integer)
            mCompanyID = value
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

    Public Property PaidatVacation() As Integer
        Get
            Return mPaidatVacation
        End Get
        Set(ByVal value As Integer)
            mPaidatVacation = value
        End Set
    End Property
    Public Property OnceatPeriod() As Boolean
        Get
            Return mOnceatPeriod
        End Get
        Set(ByVal value As Boolean)
            mOnceatPeriod = value
        End Set
    End Property
    Public Property IntervalID() As Integer
        Get
            Return mIntervalID
        End Get
        Set(ByVal value As Integer)
            mIntervalID = value
        End Set
    End Property
#End Region
#Region "Public Function"
    '========================================================================
    'ProcedureName  :  GetList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill Value List with English name  of TransactionsTypes column and its value with ID 
    '                  hrs_GradesTransactions 
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :14-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column
    '========================================================================
    Public Function GetList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)

        Dim intNoDecimalPlaces As Integer = 2
        Dim clsMainCountry As New Clssys_Countries(mPage)
        Dim clsMainCurrency As New ClsSys_Currencies(mPage)
        clsMainCountry.Find(" IsMainCountries = 1 ")
        If clsMainCountry.ID > 0 Then
            clsMainCurrency.Find(" ID=" & clsMainCountry.CurrencyID)
        End If
        If Not IsNothing(clsMainCurrency.NoDecimalPlaces) Then
            If clsMainCurrency.NoDecimalPlaces > 0 Then
                intNoDecimalPlaces = clsMainCurrency.NoDecimalPlaces
            End If
        End If

        Try

            'StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID
            If Filter = "" Then
                StrCommandString = "Select  hrs_GradesTransactions.ID,hrs_TransactionsTypes.EngName,hrs_TransactionsTypes.ArbName,hrs_GradesTransactions.MinValue,hrs_GradesTransactions.MaxValue From " & Me.mTable & " INNER JOIN hrs_Grades ON hrs_GradesTransactions.GradeID=hrs_Grades.ID INNER JOIN hrs_TransactionsTypes ON hrs_GradesTransactions.TransactionTypeID=hrs_TransactionsTypes.ID Where IsNull(hrs_GradesTransactions.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_GradesTransactions.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 Group BY hrs_TransactionsTypes.EngName"
            Else
                StrCommandString = "Select  hrs_GradesTransactions.ID,hrs_TransactionsTypes.EngName,hrs_TransactionsTypes.ArbName,hrs_GradesTransactions.MinValue,hrs_GradesTransactions.MaxValue From " & Me.mTable & " INNER JOIN hrs_Grades ON hrs_GradesTransactions.GradeID=hrs_Grades.ID INNER JOIN hrs_TransactionsTypes ON hrs_GradesTransactions.TransactionTypeID=hrs_TransactionsTypes.ID Where " & Filter & " AND IsNull(hrs_GradesTransactions.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_GradesTransactions.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " 'Group BY hrs_TransactionsTypes.EngName"
            End If
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                'Item.DisplayText = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar) & " --Between (" & mDataHandler.DataValue(ObjDataRow("MinValue"), SqlDbType.Money) & "," & mDataHandler.DataValue(ObjDataRow("MaxValue"), SqlDbType.Money) & ")"
                Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar) & "  " & ObjNavigationHandler.SetLanguage(mPage, " Between / Èíä ") & " # " & FormatNumber(mDataHandler.DataValue(ObjDataRow("MinValue"), SqlDbType.Money), intNoDecimalPlaces) & " * " & FormatNumber(mDataHandler.DataValue(ObjDataRow("MaxValue"), SqlDbType.Money), intNoDecimalPlaces) & " "
                If (mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar).ToString.Trim = "") Then
                    Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar) & "  " & ObjNavigationHandler.SetLanguage(mPage, " Between / Èíä ") & " # " & FormatNumber(mDataHandler.DataValue(ObjDataRow("MinValue"), intNoDecimalPlaces), SqlDbType.Money) & " * " & FormatNumber(mDataHandler.DataValue(ObjDataRow("MaxValue"), SqlDbType.Money), intNoDecimalPlaces) & " "
                End If
                Item.DataValue = ObjDataRow("ID")
                'Item.Tag = ObjDataRow("MinValue").ToString & "," & ObjDataRow("MaxValue").ToString
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

    '========================================================================
    'ProcedureName  :  GetDropDown
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill DropDown with English name  of TransactionsTypes column and its value with ID 
    '                  hrs_GradesTransactions 
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :14-08-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :DropDownList     :used to fill it with English name
    '========================================================================
    Public Function GetDropDownWithMaxAndMin(ByRef DdlValues As Global.System.Web.UI.WebControls.DropDownList, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem

        Try

            If Filter = "" Then
                StrCommandString = "Select  hrs_GradesTransactions.ID,hrs_TransactionsTypes.EngName,hrs_GradesTransactions.MinValue,hrs_GradesTransactions.MaxValue From " & Me.mTable & " INNER JOIN hrs_Grades ON hrs_GradesTransactions.GradeID=hrs_Grades.ID INNER JOIN hrs_TransactionsTypes ON hrs_GradesTransactions.TransactionTypeID=hrs_TransactionsTypes.ID Where IsNull(hrs_GradesTransactions.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_GradesTransactions.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 Group BY hrs_TransactionsTypes.EngName"
            Else
                StrCommandString = "Select  hrs_GradesTransactions.ID,hrs_TransactionsTypes.EngName,hrs_GradesTransactions.MinValue,hrs_GradesTransactions.MaxValue From " & Me.mTable & " INNER JOIN hrs_Grades ON hrs_GradesTransactions.GradeID=hrs_Grades.ID INNER JOIN hrs_TransactionsTypes ON hrs_GradesTransactions.TransactionTypeID=hrs_TransactionsTypes.ID Where " & Filter & " AND IsNull(hrs_GradesTransactions.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_GradesTransactions.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " 'Group BY hrs_TransactionsTypes.EngName"
            End If
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.Items.Clear()

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = mDataHandler.DataValue(ObjDataRow("MinValue"), SqlDbType.VarChar) & "," & mDataHandler.DataValue(ObjDataRow("MaxValue"), SqlDbType.VarChar)
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

    '========================================================================
    'ProcedureName  :  GetDropDown
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill DropDown with English name  of TransactionsTypes column and its value with ID 
    '                  hrs_GradesTransactions 
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :14-08-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :DropDownList     :used to fill it with English name
    '========================================================================
    Public Function GetDropDown(ByRef DdlValues As Global.System.Web.UI.WebControls.DropDownList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            'StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1"
            StrCommandString = "Select  hrs_GradesTransactions.ID,hrs_TransactionsTypes.EngName,hrs_TransactionsTypes.ArbName From " & Me.mTable & " INNER JOIN hrs_TransactionsTypes ON hrs_GradesTransactions.TransactionTypeID=hrs_TransactionsTypes.ID Where IsNull(hrs_GradesTransactions.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_GradesTransactions.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 Group BY hrs_TransactionsTypes.EngName"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.Items.Clear()

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

    '========================================================================
    'ProcedureName  :  Find 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  [0257]   
    'Date Created   :  09-08-2007
    'Modifacations  :
    'Calls          :
    'From           :(frmGrades.aspx.vb)TlbMainToolbar_ButtonClicked()
    '               :(frmGrades.aspx.vb)TlbMainNavigation_ButtonClicked()
    '               :(frmGrades.aspx.vb)GetValues()
    '               :(frmGrades.aspx.vb)SavePart()
    '               :(frmGrades.aspx.vb)SetToolbarSetting()
    '               :(frmGrades.aspx.vb)Setsetting()
    '               :(frmGrades.aspx.vb)CheckCode()
    'To             :GetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(hrs_GradesTransactions.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  And IsNull(dbo.hrs_GetRecordViewStatus(hrs_GradesTransactions.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    '========================================================================
    'ProcedureName  :  SaveUpdate
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save Or Update Row that match with critera
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [0257]   
    'Date Created   :  09-08-2007
    'Modifacations  :
    'Calls          :
    'From           :
    'To             :Update()
    '               :Save()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '    Filter         : String    : used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select hrs_GradesTransactions.ID From hrs_GradesTransactions Where " & Filter
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

    '========================================================================
    'ProcedureName  :  Save
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save new record
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :09-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmGrades.aspx.vb)SavePart()
    'To             :SetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
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

    '========================================================================
    'ProcedureName  :  Update
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  update row that match with critera
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :09-08-2007
    'Modifacations  :
    'Calls          :
    'From           :(frmGrades.aspx.vb)SavePart()
    'To             :SetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
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
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Delete
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Delete record that match critera
    '                  and return true if operation done otherwise report errors in ErrorPage                    
    'Developer      :[0257]   
    'Date Created   :09-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmGrades.aspx.vb)TlbMainToolbar_ButtonClicked()
    'To             :SetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
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

    '========================================================================
    'ProcedureName  :  DeleteAll
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Delete record that match critera without set cancel date =null
    '                  and return true if operation done otherwise report errors in ErrorPage                    
    'Developer      :[0257]   
    'Date Created   :14-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmGrades.aspx.vb)TlbMainToolbar_ButtonClicked()
    'To             :SetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function DeleteAll(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = "Delete from " & mTable & " Where " & Filter & ""
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

    '========================================================================
    'ProcedureName  :  Clear
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Clear all private attributes in class
    'Developer      :[0257]   
    'Date Created   :09-08-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mGradeID = -1
            mTransactionTypeID = -1
            mMinValue = 0
            mMaxValue = 0

            mRemarks = String.Empty
            mCompanyID = Nothing
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mPaidatVacation = 0
            mOnceatPeriod = False
            mIntervalID = 0
            '
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#Region "Navigation Functions"
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :[0257]   
    'Date Created   :09-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmGrades.aspx.vb)TlbMainNavigation_ButtonClicked()
    'To             :GetParameter()
    '               :Clear()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
#End Region

#End Region
#Region "Class Private Function"
    '========================================================================
    'ProcedureName  :  GetParameter
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :09-08-2007
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
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mGradeID = mDataHandler.DataValue_Out(.Item("GradeID"), SqlDbType.Int, True)
                mTransactionTypeID = mDataHandler.DataValue_Out(.Item("TransactionTypeID"), SqlDbType.Int, True)
                mMinValue = mDataHandler.DataValue_Out(.Item("MinValue"), SqlDbType.Money)
                mMaxValue = mDataHandler.DataValue_Out(.Item("MaxValue"), SqlDbType.Money)
                mPaidatVacation = mDataHandler.DataValue_Out(.Item("PaidatVacation"), SqlDbType.Int)
                mOnceatPeriod = mDataHandler.DataValue_Out(.Item("OnceatPeriod"), SqlDbType.Bit)
                mIntervalID = mDataHandler.DataValue_Out(.Item("IntervalID"), SqlDbType.Int, True)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
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
    'Description    :  Assign parameters of sql command  with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :09-08-2007
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
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GradeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mGradeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransactionTypeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTransactionTypeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MinValue", SqlDbType.Money)).Value = mDataHandler.DataValue_In(mMinValue, SqlDbType.Money)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaxValue", SqlDbType.Money)).Value = mDataHandler.DataValue_In(mMaxValue, SqlDbType.Money)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PaidatVacation", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPaidatVacation, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OnceatPeriod", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mOnceatPeriod, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IntervalID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mIntervalID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)

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

