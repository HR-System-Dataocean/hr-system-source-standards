Public Class Clssys_DocumentsInformationsBase
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Input : 
    'Description: In the constructor of the class set the table name and 
    '           sqlstatment of (Insert,Update,Delete,select) row from the table

    '==================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " sys_DocumentsDetails "
        mInsertParameter = " DocumentID,ObjectID,RecordID,DocumentNumber,IssueDate,IssuedCityID,LastRenewalDate,ExpiryDate,Remarks,RegUserID,RegComputerID,IssueDate_D,LastRenewalDate_D,ExpiryDate_D,CombID,ReferenceNumber"
        mInsertParameterValues = " @DocumentID,@ObjectID,@RecordID,@DocumentNumber,@IssueDate,@IssuedCityID,@LastRenewalDate,@ExpiryDate,@Remarks,@RegUserID,@RegComputerID,@IssueDate_D,@LastRenewalDate_D,@ExpiryDate_D,@CombID,@ReferenceNumber"
        mUpdateParameter = " DocumentID=@DocumentID,ObjectID=@ObjectID,RecordID=@RecordID,DocumentNumber=@DocumentNumber,IssueDate=@IssueDate,IssuedCityID=@IssuedCityID,LastRenewalDate=@LastRenewalDate,ExpiryDate=@ExpiryDate,Remarks=@Remarks,IssueDate_D=@IssueDate_D,LastRenewalDate_D=@LastRenewalDate_D,ExpiryDate_D=@ExpiryDate_D,CombID=@CombID,ReferenceNumber=@ReferenceNumber"
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region

#Region "Private Members"
    Protected mID As Object
    Private mDocumentID As Object
    Private mObjectID As Object
    Private mRecordID As Object
    Private mDocumentNumber As String
    Private mIssueDate As Object
    Private mIssuedCityID As Object
    Private mExpiryDate As Object
    Private mLastRenewalDate As Object
    Private mPostedFile As Global.System.Web.HttpPostedFile
    Private mFileName As String
    Private mPhysicalPath As String
    Private mActuallyPhysicalPath As String
    Private mEmployeeCode As String
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Private mIssueDate_D As String
    Private mLastRenewalDate_D As String
    Private mExpiryDate_D As String
    Private mCombID As String
    Private mReferenceNumber As String

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

    Public Property DbObject() As String
        Get
            Return mObjectID
        End Get
        Set(ByVal Value As String)
            mObjectID = Value
        End Set
    End Property

    Public Property Record() As Object
        Get
            Return mRecordID
        End Get
        Set(ByVal Value As Object)
            mRecordID = Value
        End Set
    End Property

    Public Property Document() As String
        Get
            Return mDocumentID
        End Get
        Set(ByVal Value As String)
            mDocumentID = Value
        End Set
    End Property

    Public Property DocumentNumber() As String
        Get
            Return mDocumentNumber
        End Get
        Set(ByVal Value As String)
            mDocumentNumber = Value
        End Set
    End Property

    Public Property IssueDate() As Object
        Get
            Return mIssueDate
        End Get
        Set(ByVal Value As Object)
            mIssueDate = Value
        End Set
    End Property

    Public Property IssuedCityID() As Object
        Get
            Return mIssuedCityID
        End Get
        Set(ByVal Value As Object)
            mIssuedCityID = Value
        End Set
    End Property

    Public Property ExpiryDate() As Object
        Get
            Return mExpiryDate
        End Get
        Set(ByVal Value As Object)
            mExpiryDate = Value
        End Set
    End Property

    Public Property LastRenewalDate() As Object
        Get
            Return mLastRenewalDate
        End Get
        Set(ByVal Value As Object)
            mLastRenewalDate = Value
        End Set
    End Property

    Public Property PostedFile() As Global.System.Web.HttpPostedFile
        Get
            Return mPostedFile
        End Get
        Set(ByVal value As Global.System.Web.HttpPostedFile)
            mPostedFile = value
        End Set
    End Property

    Public Property FileName() As String
        Get
            Return mFileName
        End Get
        Set(ByVal value As String)
            mFileName = value
        End Set
    End Property

    Public Property PhysicalPath() As String
        Get
            Return mPhysicalPath
        End Get
        Set(ByVal value As String)
            mPhysicalPath = value
        End Set
    End Property

    Public Property ActuallyPhysicalPath() As String
        Get
            Return mActuallyPhysicalPath
        End Get
        Set(ByVal value As String)
            mActuallyPhysicalPath = value
        End Set
    End Property

    Public Property EmployeeCode() As String
        Get
            Return mEmployeeCode
        End Get
        Set(ByVal value As String)
            mEmployeeCode = value
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

    Public Property IssueDate_D() As String
        Get
            Return mIssueDate_D
        End Get
        Set(ByVal value As String)
            mIssueDate_D = value
        End Set
    End Property

    Public Property LastRenewalDate_D() As String
        Get
            Return mLastRenewalDate_D
        End Get
        Set(ByVal value As String)
            mLastRenewalDate_D = value
        End Set
    End Property
    Public Property ExpiryDate_D() As String
        Get
            Return mExpiryDate_D
        End Get
        Set(ByVal value As String)
            mExpiryDate_D = value
        End Set
    End Property
    Public Property CombID() As String
        Get
            Return mCombID
        End Get
        Set(ByVal Value As String)
            mCombID = Value
        End Set
    End Property
    'ReferenceNumber
    Public Property ReferenceNumber() As String
        Get
            Return mReferenceNumber
        End Get
        Set(ByVal Value As String)
            mReferenceNumber = Value
        End Set
    End Property
#End Region

#Region "Public Function"
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Find all columns from sys_Countries table where filter and canceldate = null  
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class
    '       4-Return true if ID of Filteration >0 (Is Found)
    '==================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
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
    'Modification 0260  21-10-2008 [Start]
    Public Function Find(ByVal Filter As String, ByVal blnCanceled As Boolean) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where CancelDate IS NULL and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
    'Modification              [End]
    Public Function FindAndView(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try

            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Save or Update row 
    'Steps: 
    '       1-Execute sql statment to get ID from sys_Countries where filter 
    '       2-Check if ID > 0 this mean that row is already exist in sys_Countries  table 
    '       the make Update to this row
    '           IF ID =0 this mean that row is new row Then Insert the row in sys_Countries  table
    '==================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String = String.Empty
        Dim Value As Integer
        Try
            strSQL = "Select ID From sys_DocumentsDetails Where " & Filter
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = strSQL
            mSqlCommand.Connection.Open()
            Value = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            If Value > 0 Then
                Update(Filter, "", "")
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
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007

    'Description: Save New Row in sys_Countries  table
    'Steps: 
    '       1-execute sqlstatment to insert new row in sys_Regions  table

    '==================================================================
    Public Function Save() As Integer
        Dim ClsDocumentAttachment As New Clssys_DocumentsInformationAttachments(mPage)
        Dim IntRecordId As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameterSave(mSqlCommand, OperationType.Save)
            mSqlCommand.Connection.Open()
            IntRecordId = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

            Return IntRecordId
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update existing Row in sys_Countries  table where filter
    'Steps: 
    '       1-execute sqlstatment to Update existing row in sys_Countries  table

    '==================================================================
    Public Function Update(ByVal Filter As String, ByVal Filename As String, ByVal Foldername As String) As Boolean
        Dim StrUpdateCommand As String = String.Empty
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Dim IntRecordID As Integer
        Dim ClsDocumentAttachment As New Clssys_DocumentsInformationAttachments(mPage)
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameterUpdate(mSqlCommand)
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            'CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")

            'If ActuallyPhysicalPath <> "" Then
            '    ClsDocumentAttachment.DbObject = 128
            '    ClsDocumentAttachment.RecordId = IntRecordID
            '    ClsDocumentAttachment.FolderName = Foldername
            '    ClsDocumentAttachment.FileName = Filename
            '    ClsDocumentAttachment.Save()
            'End If

            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Inptut : Filter as string (ex.ID=2)
    'Description: Delete existing Row in sys_Countries  table where filter
    'Steps: 
    '       1-execute sqlstatment to Delete existing row in sys_Countries  table

    '==================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String = String.Empty
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            'SetParameter(mSqlCommand)
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
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Description: Clear all private members  of the class

    '==================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mDocumentID = 0
            mObjectID = 0
            mRecordID = 0
            mDocumentNumber = String.Empty
            mIssueDate = Nothing
            mIssuedCityID = 0
            mExpiryDate = Nothing
            mLastRenewalDate = Nothing
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mIssueDate_D = String.Empty
            mLastRenewalDate_D = String.Empty
            mExpiryDate_D = String.Empty
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Description:  find first row in sys_Countries table
    'Steps: 
    '       1-execute sqlstatment to find first row in sys_Countries  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function FirstRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where " & Filter & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Description:  find Last row in sys_Countries  table
    'Steps: 
    '       1-execute sqlstatment to find last row in sys_Countries  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function LastRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where " & Filter & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Description:  find Next row in sys_Countries  table
    'Steps: 
    '       1-execute sqlstatment to find Next row in sys_Countries  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function NextRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And " & Filter & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Description:  find previous row in sys_Countries  table
    'Steps: 
    '       1-execute sqlstatment to find previous row in sys_Countries  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function previousRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And " & Filter & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
    Public Function ChecckDocumentDuplicate(ByVal RecordID As Integer, ByVal ObjectID As Integer, ByVal DocumentID As Integer, ByVal ID As Integer) As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.StoredProcedure
            mSqlCommand.CommandText = "hrs_CheckDocumentDuplicate"
            mSqlCommand.Parameters.AddWithValue("@RecordID", RecordID)
            mSqlCommand.Parameters.AddWithValue("@ObjectID", ObjectID)
            mSqlCommand.Parameters.AddWithValue("@DocumentID", DocumentID)
            mSqlCommand.Parameters.AddWithValue("@ID", ID)
            mSqlCommand.Connection.Open()
            Return mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function
    '==================================================================
    'Created by : [0259]
    'Date : 26/08/2007
    'Description: Getting if there are any difference between DataSet and Class's Properties
    '==================================================================

    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
                DdlValues.SelectedValue = Item.Value
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

    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 "
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()

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

    Public Function GetExpireDocuments(ByVal strEmpCode As String, ByVal strDeptCode As String, ByVal strBranchCode As String, ByVal dteFromDate As Date, ByVal dteToDate As Date, ByVal intFilterType As Integer, ByVal intDocumentTypeID As Integer) As DataSet
        Dim ObjDataset As New DataSet
        Dim clsDAL As New ClsDataAcessLayer(mPage)
        Dim ObjNav As New Venus.Shared.Web.NavigationHandler(clsDAL.ConnectionString)
        ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetAllExpiredDocuments", strEmpCode.Trim, IIf(dteFromDate.Year = 1, Nothing, dteFromDate), IIf(dteToDate.Year = 1, Nothing, dteToDate), strDeptCode.Trim, strBranchCode.Trim, intDocumentTypeID, 0, intFilterType, ObjNav.SetLanguage(mPage, "0/1"))
        Return ObjDataset
    End Function

#End Region

#Region "Class Private Function"

    Protected Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mDocumentID = mDataHandler.DataValue_Out(.Item("DocumentID"), SqlDbType.Int, True)
                mObjectID = mDataHandler.DataValue_Out(.Item("ObjectID"), SqlDbType.Int, True)
                mRecordID = mDataHandler.DataValue_Out(.Item("RecordID"), SqlDbType.Int, True)
                mDocumentNumber = mDataHandler.DataValue_Out(.Item("DocumentNumber"), SqlDbType.VarChar)
                mIssueDate = mDataHandler.DataValue_Out(.Item("IssueDate"), SqlDbType.DateTime)
                mIssuedCityID = mDataHandler.DataValue_Out(.Item("IssuedCityID"), SqlDbType.Int, True)
                mExpiryDate = mDataHandler.DataValue_Out(.Item("ExpiryDate"), SqlDbType.DateTime)
                mLastRenewalDate = mDataHandler.DataValue_Out(.Item("LastRenewalDate"), SqlDbType.DateTime)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mIssueDate_D = mDataHandler.DataValue_Out(.Item("IssueDate_D"), SqlDbType.VarChar)
                mLastRenewalDate_D = mDataHandler.DataValue_Out(.Item("LastRenewalDate_D"), SqlDbType.VarChar)
                mExpiryDate_D = mDataHandler.DataValue_Out(.Item("ExpiryDate_D"), SqlDbType.VarChar)
                mCombID = mDataHandler.DataValue_Out(.Item("CombID"), SqlDbType.VarChar)
                'ReferenceNumber
                mReferenceNumber = mDataHandler.DataValue_Out(.Item("ReferenceNumber"), SqlDbType.VarChar)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Private Function SetParameterSave(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal operationtype As OperationType) As Boolean
        Dim StrFilePath As String = String.Empty
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim StrErrorMsg As String = String.Empty
        Dim ClsDocument As New Clssys_DocumentsBase(mPage)
        Try

            'ClsDocument.Find("ID=" & mDocumentID)
            'StrFilePath = "..\" & ClsDataAcessLayer.CONFIG_DEFULTIMAGEFOLDER & "\" & mEmployeeCode & "\" & ClsDocument.Code & "\" & FileName

            'If Not ObjWebHandler.SaveFile(mPhysicalPath, PostedFile, ClsDataAcessLayer.CONFIG_DEFULTIMAGEFOLDER, mEmployeeCode, ClsDocument.Code, FileName, StrErrorMsg) And Not PostedFile.ContentLength = 0 Then
            '    Return False
            'ElseIf PostedFile.ContentLength = 0 Then
            '    StrFilePath = ""
            'End If

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DocumentID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mDocumentID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ObjectID", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mObjectID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RecordID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRecordID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DocumentNumber", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mDocumentNumber, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IssueDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mIssueDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IssuedCityID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mIssuedCityID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExpiryDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mExpiryDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LastRenewalDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mLastRenewalDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IssueDate_D", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mIssueDate_D, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LastRenewalDate_D", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mLastRenewalDate_D, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExpiryDate_D", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mExpiryDate_D, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CombID", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCombID, SqlDbType.VarChar)
            'ReferenceNumber
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReferenceNumber", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mReferenceNumber, SqlDbType.VarChar)

            Select Case operationtype
                Case ClsDataAcessLayer.OperationType.Save
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            End Select

            ActuallyPhysicalPath = StrFilePath

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Private Function SetParameterUpdate(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Dim StrFilePath As String = String.Empty
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim StrErrorMsg As String = String.Empty
        Dim ClsDocument As New Clssys_DocumentsBase(mPage)
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & Me.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        Try
            'Modification             [Start]
            ClsDocument.Find("ID=" & mDocumentID)
            StrFilePath = "..\" & ClsDataAcessLayer.CONFIG_DEFULTIMAGEFOLDER & "\" & mEmployeeCode & "\" & ClsDocument.Code & "\" & FileName
            If Not ObjWebHandler.SaveFile(mPhysicalPath, PostedFile, ClsDataAcessLayer.CONFIG_DEFULTIMAGEFOLDER, mEmployeeCode, ClsDocument.Code, FileName, StrErrorMsg) And Not PostedFile.ContentLength = 0 Then
                StrFilePath = ""
            ElseIf PostedFile.ContentLength = 0 Then
                StrFilePath = ""
            End If
            'Modification             [End]
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DocumentID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mDocumentID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ObjectID", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mObjectID, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RecordID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRecordID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DocumentNumber", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mDocumentNumber, SqlDbType.VarChar)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IssueDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mIssueDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IssuedCityID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mIssuedCityID, SqlDbType.Int, True)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExpiryDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mExpiryDate, SqlDbType.DateTime)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LastRenewalDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mLastRenewalDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IssueDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mIssueDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExpiryDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mExpiryDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LastRenewalDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mLastRenewalDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IssueDate_D", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mIssueDate_D, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LastRenewalDate_D", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mLastRenewalDate_D, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExpiryDate_D", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mExpiryDate_D, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CombID", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCombID, SqlDbType.VarChar)
            'ReferenceNumber
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReferenceNumber", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mReferenceNumber, SqlDbType.VarChar)

            ActuallyPhysicalPath = StrFilePath
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Private Function FixNull(ByVal obj As Object, ByVal DataColumn As Global.System.Data.DataColumn) As Object
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
#End Region

#Region "Class Destructors"
    Public Sub finalized()
        mDataSet.Dispose()
    End Sub
#End Region

End Class
Public Class Clssys_DocumentsInformations
    Inherits Clssys_DocumentsInformationsBase

#Region "Class Constructors"

    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Input : 
    'Description: In the constructor of the class set the table name and 
    '           sqlstatment of (Insert,Update,Delete,select) row from the table

    '==================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        
    End Sub
#End Region

    

End Class