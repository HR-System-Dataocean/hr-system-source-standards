'                  : B#g0010 [0256] 23-06-2008 Add _Forgin arrgument to data_ValueIn method in SetParameters Function 
'                  :                           And _Forgin arrgument to data_value_Out Method in GetParameters Function 
'                  :                           To avoid saving non-existing forign key in any table 
'                  :                           And convert the DBnull values to zero in case of forign key fields only 
'                  : [0256] 22-07-2008         Change Number Filed data type  from String to integer 
'=========================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_ContractsBase2
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
        mTable = " hrs_Contracts "
        mInsertParameter = " Number,EmployeeID,StartDate,EndDate,ProfessionID,ContractTypeId,PositionID,EmployeeClassID,GradeStepId,CurrencyId,Remarks,RegUserID,RegComputerID,ContractPeriod "
        mInsertParameterValues = " @Number,@EmployeeID,@StartDate,@EndDate,@ProfessionID,@ContractTypeId,@PositionID,@EmployeeClassID,@GradeStepId,@CurrencyId,@Remarks,@RegUserID,@RegComputerID,@ContractPeriod "
        mUpdateParameter = " Number=@Number,EmployeeID=@EmployeeID,StartDate=@StartDate,EndDate=@EndDate,ProfessionID=@ProfessionID,ContractTypeId=@ContractTypeId,PositionID=@PositionID,EmployeeClassID=@EmployeeClassID,GradeStepId=@GradeStepId,CurrencyId=@CurrencyId,Remarks=@Remarks,ContractPeriod=@ContractPeriod "
        mSelectCommand = "set dateformat dmy; Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region

#Region "Private Members"
    Protected mID As Object
    Private mNumber As Integer
    Private mEmployeeID As Object
    Private mStartDate As Object
    Private mEndDate As Object
    Private mProfessionID As Object
    Private mContractTypeId As Object
    Private mPositionID As Object
    Private mEmployeeClassID As Object
    Private mGradeStepId As Object
    Private mCurrencyId As Object
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Private mContractPeriod As Integer

    Const xVacationType = 0
    Const xRequiredExperiences = 2
    Const xDuration = 3

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
    Public Property Number() As Integer
        Get
            Return mNumber
        End Get
        Set(ByVal Value As Integer)
            mNumber = Value
        End Set
    End Property
    Public Property EmployeeID() As Object
        Get
            Return mEmployeeID
        End Get
        Set(ByVal Value As Object)
            mEmployeeID = Value
        End Set
    End Property
    Public Property StartDate() As Object
        Get
            Return mStartDate
        End Get
        Set(ByVal Value As Object)
            mStartDate = Value
        End Set
    End Property
    Public Property EndDate() As Object
        Get
            Return mEndDate
        End Get
        Set(ByVal Value As Object)
            mEndDate = Value
        End Set
    End Property
    Public Property ProfessionID() As Object
        Get
            Return mProfessionID
        End Get
        Set(ByVal Value As Object)
            mProfessionID = Value
        End Set
    End Property
    Public Property ContractTypeId() As Object
        Get
            Return mContractTypeId
        End Get
        Set(ByVal Value As Object)
            mContractTypeId = Value
        End Set
    End Property
    Public Property PositionID() As Object
        Get
            Return mPositionID
        End Get
        Set(ByVal Value As Object)
            mPositionID = Value
        End Set
    End Property
    Public Property EmployeeClassID() As Object
        Get
            Return mEmployeeClassID
        End Get
        Set(ByVal Value As Object)
            mEmployeeClassID = Value
        End Set
    End Property
    Public Property GradeStepId() As Object
        Get
            Return mGradeStepId
        End Get
        Set(ByVal Value As Object)
            mGradeStepId = Value
        End Set
    End Property
    Public Property CurrencyId() As Object
        Get
            Return mCurrencyId
        End Get
        Set(ByVal Value As Object)
            mCurrencyId = Value
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
    Public Property ContractPeriod() As Integer
        Get
            Return mContractPeriod
        End Get
        Set(ByVal value As Integer)
            mContractPeriod = value
        End Set
    End Property

#End Region

#Region "Public Function"
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Find all columns from hrs_Contracts table where filter and canceldate = null  
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class
    '       4-Return true if ID of Filteration >0 (Is Found)
    '==================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            '==================== Order By Modification [Start]
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By Number "
            End If
            Filter = Filter.Replace("Õ", " AM ").Replace("ã", " PM ")
            '==================== Order By Modification [ End ]

            StrSelectCommand = CONFIG_DATEFORMAT & mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And IsNull(CancelDate,'')='' And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And IsNull(CancelDate,'')='' ")
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
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Save or Update row 
    'Steps: 
    '       1-Execute sql statment to get ID from hrs_Contracts where filter 
    '       2-Check if ID > 0 this mean that row is already exist in hrs_Contracts  table 
    '       the make Update to this row
    '           IF ID =0 this mean that row is new row Then Insert the row in hrs_Contracts  table
    '==================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String = String.Empty
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_Contracts Where " & Filter
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
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007

    'Description: Save New Row in hrs_Contracts  table
    'Steps: 
    '       1-execute sqlstatment to insert new row in hrs_Contracts  table

    '==================================================================
    Public Function Save() As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, OperationType.Save)
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
    Public Function SaveWithID() As Integer
        Dim intID As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand & " ; Select SCOPE_IDENTITY()"
            SetParameter(mSqlCommand, OperationType.Save)
            mSqlCommand.Connection.Open()
            intID = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            Return intID
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update existing Row in hrs_Contracts  table where filter
    'Steps: 
    '       1-execute sqlstatment to Update existing row in hrs_Contracts  table

    '==================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String = String.Empty
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand, OperationType.Update)
            'mSqlCommand.Connection.Open()
            'mSqlCommand.ExecuteNonQuery()
            'mSqlCommand.Connection.Close()
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            'Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Delete existing Row in hrs_Contracts  table where filter
    'Steps: 
    '       1-execute sqlstatment to Delete existing row in hrs_Contracts  table

    '==================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String = String.Empty
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            SetParameter(mSqlCommand, OperationType.Update)
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
    'Date : 16/07/2007
    'Description: Clear all private members  of the class

    '==================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mNumber = 0
            mEmployeeID = 0
            mStartDate = Nothing
            mEndDate = Nothing
            mProfessionID = 0
            mContractTypeId = 0
            mPositionID = 0
            mEmployeeClassID = 0
            mGradeStepId = 0
            mCurrencyId = 0
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mContractPeriod = 0
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Description:  find first row in hrs_Contracts table for selected Employee
    'Steps: 
    '       1-execute sqlstatment to find first row in hrs_Contracts table for selected Employee
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Overloads Function FirstRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where " & Filter & " and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim <> "", " And " & Filter, "") & " ORDER BY Number ASC"
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
    'Date : 16/07/2007
    'Description:  find Last row in hrs_Contracts  table for selected Employee
    'Steps: 
    '       1-execute sqlstatment to find last row in hrs_Contracts  table for selected Employee
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Overloads Function LastRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where " & Filter & "  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  " & IIf(Filter.Trim <> "", " And " & Filter, "") & "  ORDER BY Number DESC"
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
    'Date : 16/07/2007
    'Description:  find Next row in hrs_Contracts  table for selected Employee
    'Steps: 
    '       1-execute sqlstatment to find Next row in hrs_Contracts  table for selected Employee
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Overloads Function NextRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And " & Filter & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Number >'" & mNumber & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  " & IIf(Filter.Trim <> "", " And " & Filter, "") & "  ORDER BY Number ASC"
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
    'Date : 16/07/2007
    'Description:  find previous row in hrs_Contracts  table for selected Employee
    'Steps: 
    '       1-execute sqlstatment to find previous row in hrs_Contracts  table for selected Employee
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Overloads Function previousRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And " & Filter & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Number < '" & mNumber & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  " & IIf(Filter.Trim <> "", " And " & Filter, "") & "  ORDER BY Number DESC"
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
    'Date : 16/07/2007
    'Description: execute stored procedure to get vacation types (RequiredExperience , Duration of each vacation) for each Contract

    '==================================================================
    Public Function GetContractVacation(ByVal ContractId As Integer, ByVal DToCheck As Date, ByVal AllRows As Integer, ByRef ObjDs As DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            ObjDs = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, "hrs_GetContractsVacations", ContractId, DToCheck, AllRows)
            If mDataHandler.CheckValidDataObject(ObjDs) And ObjDs.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function GetDurationDaysForPeriod(ByVal intContractID As Integer, ByVal intVacType As Integer, ByVal dteStartDate As DateTime, ByVal UseAddDays As Boolean) As DataSet
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, "GetDurationDaysForPeriod", intContractID, intVacType, dteStartDate, UseAddDays)
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Description: execute sqlstatement to insert into hrs_ContractsVacations table (DatawebGrid Control) 
    '       Insert into hrs_ContractsVacations table RequiredExperience,Duration for all vacationtypes of Current Contract
    '==================================================================
    Public Function SetContractVacation(ByVal ContractId As Integer, ByVal ObjGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid) As Boolean

        Dim StrSelectCommand As String = String.Empty
        Dim ObjDataRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
        Try
            StrSelectCommand = " Delete From hrs_ContractsVacations Where ContractID = " & ContractId & ";" & vbNewLine
            For Each ObjDataRow In ObjGrid.Rows
                If (Not IsNothing(ObjDataRow.Cells.FromKey("FromMonth").Value)) And (Not IsNothing(ObjDataRow.Cells.FromKey("VacationTypeID").Value)) Then
                    StrSelectCommand &= " Insert Into hrs_ContractsVacations(vacationtypeid,ContractId,FromMonth,ToMonth,DurationDays,RequiredWorkingMonths,TicketsRnd,DependantTicketRnd,MaxKeepDays)" &
                    " Values(" & ObjDataRow.Cells.FromKey("VacationTypeID").Value &
                    "," & ContractId &
                    "," & IIf(ObjDataRow.Cells.FromKey("FromMonth").Value Is Nothing, "Null", ObjDataRow.Cells.FromKey("FromMonth").Value) &
                    "," & IIf(ObjDataRow.Cells.FromKey("ToMonth").Value Is Nothing, "Null", ObjDataRow.Cells.FromKey("ToMonth").Value) &
                    "," & IIf(ObjDataRow.Cells.FromKey("DurationDays").Value Is Nothing, "Null", ObjDataRow.Cells.FromKey("DurationDays").Value) &
                    "," & IIf(ObjDataRow.Cells.FromKey("RequiredWorkingMonths").Value Is Nothing, "Null", ObjDataRow.Cells.FromKey("RequiredWorkingMonths").Value) &
                    "," & IIf(ObjDataRow.Cells.FromKey("TicketsRnd").Value Is Nothing, "Null", ObjDataRow.Cells.FromKey("TicketsRnd").Value) &
                    "," & IIf(ObjDataRow.Cells.FromKey("DependantTicketRnd").Value Is Nothing, "Null", ObjDataRow.Cells.FromKey("DependantTicketRnd").Value) &
                    "," & IIf(ObjDataRow.Cells.FromKey("MaxKeepDays").Value Is Nothing, "Null", ObjDataRow.Cells.FromKey("MaxKeepDays").Value) & ")" &
                    ";" & vbNewLine
                End If


            Next
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, StrSelectCommand)
            Return True

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function SetContractVacation(ByVal ContractId As Integer, ByVal objDS As DataSet) As Boolean

        Dim StrSelectCommand As String = String.Empty
        Dim ObjDataRow As DataRow
        Try
            StrSelectCommand = " Delete From hrs_ContractsVacations Where ContractID = " & ContractId & ";" & vbNewLine
            For Each ObjDataRow In objDS.Tables(0).Rows
                If (Not IsNothing(ObjDataRow("FromMonth"))) And (Not IsNothing(ObjDataRow("VacationTypeID"))) Then
                    StrSelectCommand &= " Insert Into hrs_ContractsVacations(vacationtypeid,ContractId,FromMonth,ToMonth,DurationDays,RequiredWorkingMonths)" &
                    " Values(" & ObjDataRow("VacationTypeID") &
                    "," & ContractId &
                    "," & IIf(Convert.ToString(ObjDataRow("FromMonth")) = "", "Null", ObjDataRow("FromMonth")) &
                    "," & IIf(Convert.ToString(ObjDataRow("ToMonth")) = "", "Null", ObjDataRow("ToMonth")) &
                    "," & IIf(Convert.ToString(ObjDataRow("DurationDays")) = "", "Null", ObjDataRow("DurationDays")) &
                    "," & IIf(Convert.ToString(ObjDataRow("RequiredWorkingMonths")) = "", "Null", ObjDataRow("RequiredWorkingMonths")) & ")" &
                    ";" & vbNewLine
                End If


            Next
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, StrSelectCommand)
            Return True

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region

#Region "Class Private Function"
    '===================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Input : Ds as Dataset 
    'Description:   Asign the Result of Ds to the private members of the class

    '===================================================================
    Protected Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mNumber = mDataHandler.DataValue_Out(.Item("Number"), SqlDbType.Int)
                mEmployeeID = mDataHandler.DataValue_Out(.Item("EmployeeID"), SqlDbType.Int, True)
                mStartDate = mDataHandler.DataValue_Out(.Item("StartDate"), SqlDbType.DateTime)
                mEndDate = mDataHandler.DataValue_Out(.Item("EndDate"), SqlDbType.DateTime)
                mProfessionID = mDataHandler.DataValue_Out(.Item("ProfessionID"), SqlDbType.Int, True)
                mContractTypeId = mDataHandler.DataValue_Out(.Item("ContractTypeID"), SqlDbType.Int, True)
                mPositionID = mDataHandler.DataValue_Out(.Item("PositionID"), SqlDbType.Int, True)
                mEmployeeClassID = mDataHandler.DataValue_Out(.Item("EmployeeClassID"), SqlDbType.Int, True)
                mGradeStepId = mDataHandler.DataValue_Out(.Item("GradeStepID"), SqlDbType.Int, True)
                mCurrencyId = mDataHandler.DataValue_Out(.Item("CurrencyID"), SqlDbType.Int, True)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mContractPeriod = mDataHandler.DataValue_Out(.Item("ContractPeriod"), SqlDbType.Int)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '===================================================================
    'Created by : [0258]
    'Date : 16/07/2007
    'Description:   Make the values of parameter equal values of private member  of the class

    '===================================================================
    Protected Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal operationtype As OperationType) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Number", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mNumber, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@StartDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mStartDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EndDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mEndDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ProfessionID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mProfessionID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ContractTypeId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mContractTypeId, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PositionID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPositionID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeClassID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeClassID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GradeStepId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mGradeStepId, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CurrencyId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCurrencyId, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ContractPeriod", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mContractPeriod, SqlDbType.Int)
            Select Case operationtype
                Case ClsDataAcessLayer.OperationType.Save
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            End Select
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

'========================================================================
'Program Name  : Clshrs_Contracts
'Project       : Venus 
'Module        : 
'Date Created  : 
'Developer     :
'Date Modified : 
'Modifications :
'               : 02-09-2007 Add Method ContractValidatoinId
'               : 02-09-2007 Add Method EndContract
'               : 05-09-2007 Add Method CheckAnnualVacationDays
'========================================================================

Public Class Clshrs_Contracts2
    Inherits Clshrs_ContractsBase

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
    End Sub

    '==================================================================
    'Created by : DataOcean
    'Date : 09/07/2007
    'Input : DdlValues as DropDownList
    '       Condition to fill dropdownlist (if Wanted)
    '       NullNode to make attention to user by make first item ="Please Select"    
    'Description:  Fill DropDownList with Contracts 
    'Modified by    : MAz
    'Date           : 05-12-2007
    'Description    : Update GetDropDownList And GetList to match with Language 
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

    '==================================================================
    'Created by : DataOcean
    'Date : 09/07/2007
    'Input : value list in the datagrid 
    '       Condition to fill dropdownlist (if Wanted)
    '       NullNode to make attention to user by make first item ="Please Select"    
    'Description:  Fill DropDownList with Contracts 
    'Modified by    : MAz
    'Date           : 05-12-2007
    'Description    : Update GetDropDownList And GetList to match with Language 
    '==================================================================
    Public Function GetList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID
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

    '=========================================================================
    'Added By : AGL on 22-8-2007 
    '=========================================================================
    Public Function ContractValidatoinId(ByVal EmployeeID As Integer, ByVal IntFisicalYearPeriod As Integer) As Integer
        'Get Current Period 
        'Get Module 
        'Get Start And EndDate of Contracts

        Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(mPage)
        'Get Current Period 
        Dim FisicalFromDate As Date = Nothing
        Dim fisicalToDate As Date = Nothing
        If IntFisicalYearPeriod = 0 Then
            ClsFisicalYearsPeriods.Find(" GetDate() Between sys_FiscalYearsPeriods.FromDate and sys_FiscalYearsPeriods.ToDate ")
        Else
            ClsFisicalYearsPeriods.Find(" sys_FiscalYearsPeriods.Id = " & IntFisicalYearPeriod)
        End If
        If ClsFisicalYearsPeriods.ID <= 0 Then
            Return 0
        End If

        '-------------------------------0257 MODIFIED-----------------------------------------
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & Me.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        '------------------------------=============-----------------------------------------
        '=============================================================
        FisicalFromDate = ClsFisicalYearsPeriods.FromDate
        fisicalToDate = ClsFisicalYearsPeriods.ToDate
        '=============================================================
        '-------------------------------0257 MODIFIED-----------------------------------------
        If clsCompanies.IsHigry Then
            FisicalFromDate = ClsGHCalender.GetRelativeDate(FisicalFromDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input)
            fisicalToDate = ClsGHCalender.GetRelativeDate(fisicalToDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input)
        End If
        '------------------------------=============-----------------------------------------



        If Find("Employeeid=" & EmployeeID & "  And StartDate <= '" & Format(fisicalToDate, "dd/MM/yyyy") & "' And (enddate is null or EndDate >= '" & Format(FisicalFromDate, "dd/MM/yyyy") & "') order by StartDate DESC") Then
            Return ID
        Else
            Return 0
        End If
    End Function

    Public Function GetContractVacationDeserved(ByVal ContractID As Integer) As Double
        Try
            Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Me.ConnectionString, "hrs_GetContractsVacations", ContractID)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    '=========================================================================
    'Added By : DataOcean on 24-12-2007 
    '=========================================================================
    Public Function ContractValidatoinId(ByVal EmployeeID As Integer, ByVal CurrentDate As Date) As Integer
        'Get Current Period 
        'Get Module 
        'Get Start And EndDate of Contracts
        'If Find("Employeeid=" & EmployeeID & "  And StartDate <= '" & FisicalFromDate & "' And (enddate is null or '" & fisicalToDate & "' Between StartDate and EndDate)") Then
        If Find("Employeeid=" & EmployeeID & "  And StartDate <= '" & Format(CurrentDate, "dd/MM/yyyy") & "' And (enddate is null or '" & Format(CurrentDate, "dd/MM/yyyy") & "' Between StartDate and EndDate)  order by StartDate DESC") Then
            Return ID
        Else
            Return 0
        End If
    End Function

    Public Function EndContract(ByVal Filter As String) As Boolean
        Dim sqlStr As String = " Update " & mTable & " Set EndDate = GetDate() " & IIf(Len(Filter) > 0, " WHERE " & Filter, "")
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(mConnectionString, CommandType.Text, sqlStr)
    End Function

    Public Function EndContract(ByVal Filter As String, ByVal oDate As Date) As Boolean
        Dim sqlStr As String = CONFIG_DATEFORMAT & " Update " & mTable & " Set EndDate = '" & Format(oDate, "dd/MM/yyyy") & "' " & IIf(Len(Filter) > 0, " WHERE " & Filter, "")
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(mConnectionString, CommandType.Text, sqlStr)
    End Function

    '=======================================================================================================
    'Added By    :  05-09-2007
    'Description : Returns No Of Annual vacation Days in a Sepecified Contract and checks if contract overdue Annual Vacations or not
    'Calls       : GetContractVacation()  Fills DataSet With Contract Vacations And Returns It 
    '            : Find Method Of Hrs_contracts Class 
    '=======================================================================================================
    Public Function CheckAnnualVacationDays(ByVal IntContractId As Integer, ByVal AnnualVacationTypeId As Integer, ByRef IsForPayment As Boolean, ByRef AnnualVacationDays As Object, ByRef TotalDays As Integer, ByRef UnpaidDays As Integer, ByRef NetDays As Integer, ByRef dtVacInfo As DataTable, Optional ByVal DateToCheck As Date = Nothing, Optional ByRef IsContractial As Boolean = False) As Integer



        If DateToCheck = Nothing Then DateToCheck = Now.Date
        Dim Ds As New DataSet
        Dim IntNoofworkingDays As Integer = 0
        Dim IntAllWorkinDays As Integer = 0
        Dim IntAllWorkinDaysToLastVac As Integer = 0
        Dim DblHolidayFactor As Double = 0
        Dim ContractStartDate As Date = Nothing
        Dim ClsEmployeeTransaction As New Clshrs_EmployeesTransactions(mPage)
        Dim ClsEndofService As New Clshrs_EndOfServices(mPage)
        Dim DteLastPaidVacation As Nullable(Of Date) = Nothing
        Dim DteOpenBalanceVacation As Nullable(Of Date) = Nothing
        Dim DteLastReturnVacation As Date
        Dim DteCheckDate As Date
        Dim clscontracts As New Clshrs_Contracts(mPage)
        Dim paiddays As Double
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & Me.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        Dim clshrs_VactionTypeCalculation As New Clshrs_VactionTypeCalculation(mPage)
        Dim RemainingBalance As Double = 0
        Dim IsbenfitsAndDaysAreSperated As Boolean = False
        Dim FinancialWorkingUnits As Double
        Dim DurationDays As Integer
        Dim relatedVactionDays As Integer
        dtVacInfo = New DataTable
        dtVacInfo.Columns.Add("RequiredWorkingDays")
        dtVacInfo.Columns.Add("Duration")
        dtVacInfo.Columns.Add("FromDate")
        dtVacInfo.Columns.Add("ToDate")
        dtVacInfo.Columns.Add("TotalDays")
        dtVacInfo.Columns.Add("DeductionDays")
        dtVacInfo.Columns.Add("NetDays")
        dtVacInfo.Columns.Add("VactionBalance", GetType(Decimal))
        dtVacInfo.Columns.Add("Amount", GetType(Decimal))

        GetContractVacation(IntContractId, DateToCheck, 1, Ds)

        DteOpenBalanceVacation = ClsEmployeeTransaction.GetVacationOpenBalanceDate(EmployeeID, AnnualVacationTypeId)
        Dim nStartDate As Date = StartDate

        Dim duedate As Nullable(Of Date) = Nothing
        duedate = DteOpenBalanceVacation
        If clsCompanies.IsHigry Then
            nStartDate = ClsGHCalender.GetRelativeDate(nStartDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input)
        End If
        Find(" ID =" & IntContractId)

        clshrs_VactionTypeCalculation.FindVactionCalculation()

        If clshrs_VactionTypeCalculation.Code = "001" Then
            DteLastPaidVacation = ClsEmployeeTransaction.GetPreviousVacationPaidDate(EmployeeID, FinancialWorkingUnits, relatedVactionDays)
            If Not IsNothing(DteLastPaidVacation) Then
                duedate = DteLastPaidVacation
            End If


        ElseIf clshrs_VactionTypeCalculation.Code = "002" Then
            DteLastPaidVacation = ClsEmployeeTransaction.GetPreviousVacationPaidDate(EmployeeID, FinancialWorkingUnits, relatedVactionDays)
            If Not IsNothing(DteLastPaidVacation) Then
                'add related vactiondaye to duedate
                duedate = Convert.ToDateTime(DteLastPaidVacation).AddDays(relatedVactionDays)
            End If


        ElseIf clshrs_VactionTypeCalculation.Code = "003" Then
            DteLastReturnVacation = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEndofService.ConnectionString, CommandType.Text, "set dateformat dmy select top 1  isnull(ActualEndDate,01/01/1950) as LastReturnDate from hrs_EmployeesVacations  Inner Join hrs_VacationsTypes On hrs_VacationsTypes.id = hrs_EmployeesVacations.VacationTypeID Where hrs_VacationsTypes.isAnnual = 1 and employeeid=" & EmployeeID & " and   hrs_EmployeesVacations.CancelDate is null   Order by ActualStartDate desc ")
            If Not IsNothing(DteLastPaidVacation) Then
                duedate = DteLastPaidVacation
            End If
        ElseIf clshrs_VactionTypeCalculation.Code = "004" And IsForPayment = True Then
            DteLastPaidVacation = ClsEmployeeTransaction.GetPreviousVacationPaidDate(EmployeeID)
            If Not IsNothing(DteLastPaidVacation) Then
                duedate = DteLastPaidVacation
            End If
        ElseIf clshrs_VactionTypeCalculation.Code = "004" And IsForPayment = False Then
            DteLastReturnVacation = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEndofService.ConnectionString, CommandType.Text, "set dateformat dmy select top 1  isnull(ActualEndDate,01/01/1950) as LastReturnDate from hrs_EmployeesVacations  Inner Join hrs_VacationsTypes On hrs_VacationsTypes.id = hrs_EmployeesVacations.VacationTypeID Where hrs_VacationsTypes.isAnnual = 1 and employeeid=" & EmployeeID & " and   hrs_EmployeesVacations.CancelDate is null and  Order by ActualStartDate desc ")
            If Not IsNothing(DteLastPaidVacation) Then
                duedate = DteLastPaidVacation
            End If
        End If





        If DteOpenBalanceVacation >= duedate Then
            duedate = DteOpenBalanceVacation
        End If
        If IsNothing(duedate) Then
            duedate = StartDate
        End If
        clscontracts.Find("ID=" & IntContractId)
        Dim strSelect As String = "set dateformat dmy; select * from hrs_Contracts where EmployeeID =" & EmployeeID & " and (EndDate is null or EndDate >'" & duedate & "')"
        Dim dsContracts As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEndofService.ConnectionString, CommandType.Text, strSelect)
        Dim newDateCheck As Date = DateToCheck
        Dim newduedate As Date = duedate
        Dim WorkingDayFromstart As Integer = 0
        Dim workingDaysToDueDate As Integer = 0
        Dim TDays As Integer
        Dim duedays As Double = 0
        Dim RequiredWorkingMonths As Integer

        Dim ToMonths As Integer
        'Loop Through employee Contracts (in case there is more than one contract during vaction period)
        For Each row In dsContracts.Tables(0).Rows
            Dim R As DataRow = dtVacInfo.NewRow()
            If Not IsDBNull(row("EndDate")) Then
                'reset the datecheck to end contract if is there ended contract
                newDateCheck = row("EndDate")
            Else
                newDateCheck = DateToCheck
            End If
            'Get All Contraction Vaction Slice in the employee contract
            GetContractVacation(row("ID"), newDateCheck, 1, Ds)
            'Get No Of Working days from last payment date to (Contract End date or Current due date) 
            If clsCompanies.RegComputerID = 360 Then
                IntNoofworkingDays = days360(newduedate, newDateCheck)
            Else
                IntNoofworkingDays = (newDateCheck - newduedate).TotalDays
            End If
            IntNoofworkingDays = IIf(IntNoofworkingDays < 0, 0, IntNoofworkingDays)
            'Get Deduction Days during the due vaction period 
            UnpaidDays = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEndofService.ConnectionString, "hrs_GetEmployeeVacationPenalty", Format(newduedate, "dd/MM/yyyy"), Format(newDateCheck, "dd/MM/yyyy"), clscontracts.EmployeeID)
            R("TotalDays") = IntNoofworkingDays
            R("DeductionDays") = UnpaidDays
            IntNoofworkingDays = IntNoofworkingDays - UnpaidDays
            R("NetDays") = IntNoofworkingDays
            NetDays = IntNoofworkingDays
            TDays = IntNoofworkingDays
            TotalDays = IntNoofworkingDays


            'Get TotalDays from contract start date to (to current date or  contract end date  )
            If clsCompanies.RegComputerID = 360 Then
                WorkingDayFromstart = days360(nStartDate, newDateCheck)
            Else
                WorkingDayFromstart = (newDateCheck - nStartDate).TotalDays
            End If
            'Get TotalDays from contract start date to (Lat payment date or contract end date)
            If clsCompanies.RegComputerID = 360 Then
                workingDaysToDueDate = days360(nStartDate, newduedate)
            Else
                workingDaysToDueDate = (newduedate - nStartDate).TotalDays
            End If

            R("Fromdate") = newduedate.ToString("dd/MM/yyyy")
            R("Todate") = newDateCheck.ToString("dd/MM/yyyy")
            duedays = 0
            For index = 0 To Ds.Tables(0).Rows.Count - 1
                RequiredWorkingMonths = Convert.ToInt32(Ds.Tables(0).Rows(index)("RequiredWorkingMonths"))
                DurationDays = Convert.ToInt32(Ds.Tables(0).Rows(index)("DurationDays"))
                ToMonths = IIf(IsDBNull(Ds.Tables(0).Rows(index)("ToMonth")) = True, 99999, Ds.Tables(0).Rows(index)("ToMonth"))
                R("RequiredWorkingDays") = RequiredWorkingMonths
                R("Duration") = DurationDays

                If index = Ds.Tables(0).Rows.Count - 1 Then
                    If TDays > 0 Then
                        duedays += (TDays / RequiredWorkingMonths) * DurationDays
                    End If
                Else
                    If WorkingDayFromstart > ToMonths Then

                        If workingDaysToDueDate > ToMonths Then
                            Continue For
                        ElseIf workingDaysToDueDate + TDays > ToMonths Then
                            Dim remaingduedays As Integer
                            remaingduedays = ToMonths - workingDaysToDueDate
                            duedays += (remaingduedays / RequiredWorkingMonths) * DurationDays
                            TDays = TDays - remaingduedays
                            Continue For
                        End If

                        If WorkingDayFromstart = TDays + UnpaidDays Then
                            TDays = TDays - RequiredWorkingMonths
                            duedays = DurationDays


                        ElseIf WorkingDayFromstart > ToMonths Then

                            duedays += DurationDays
                            TDays = TDays - RequiredWorkingMonths
                            Continue For
                        Else
                            Continue For
                        End If
                    Else

                        duedays += (TDays / RequiredWorkingMonths) * DurationDays

                        TDays = 0
                    End If
                End If


            Next
            R("VactionBalance") = Math.Round(duedays, 2)
            dtVacInfo.Rows.Add(R)
            newduedate = newDateCheck.AddDays(1)
        Next














        AnnualVacationDays = dtVacInfo.Compute("sum(VactionBalance)", String.Empty) ' Math.Round(duedays, 2)
        Return AnnualVacationDays
    End Function
    Private Function days360(ByVal stDate As Date, ByVal enddate As Date) As Integer
        Dim days As Integer
        Dim fromdate As DateTime = stDate
        '  Dim todate = enddate.AddDays(1)
        Dim todate = enddate
        Dim d1 = If(fromdate.Day = 31, 30, fromdate.Day)
        Dim d2 = If(todate.Day = 31 AndAlso (fromdate.Day = 30 OrElse fromdate.Day = 31), 30, todate.Day)
        days = ((360 * (todate.Year - fromdate.Year)) + (30 * (todate.Month - fromdate.Month)) + (d2 - d1))
        Return days
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
    Private Function GetPenaltyDays(ByVal StartDate As Date, ByVal EndDate As Date) As Single
        Dim strSelect As String = "set Dateformat DMY  Select IsNull(Sum (DateDiff(hh,ActualStartDate ,IsNull(ActualEndDate,getDate()))),0) From hrs_employeesvacations Inner Join hrs_vacationsTypes On hrs_EmployeesVacations.VacationTypeId = hrs_VacationsTypes.Id Where hrs_VacationsTypes.IsPaid = -1 And EmployeeID = " & EmployeeID & " And IsNull(hrs_employeesvacations.CancelDate,'')='' And  ActualStartDate Between '" & StartDate.ToString("dd/MM/yyyy") & "' And '" & Format(EndDate, "dd/MM/yyyy") & "' And  IsNull(ActualEndDate,GetDate()) Between '" & StartDate.ToString("dd/MM/yyyy") & "' And '" & Format(EndDate, "dd/MM/yyyy") & "'"
        Dim PenltyDays As Single = 0
        PenltyDays = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strSelect)
        Return PenltyDays
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
                        '-------------------------------0257 MODIFIED-----------------------------------------
                        If (IsDBNull(myPropInfo.GetValue(ClassObj, Index)) Or IsNothing(myPropInfo.GetValue(ClassObj, Index))) Then
                            If (IsDBNull(FixNull(DSData.Tables(0).Rows(0)(DSCounter), DSData.Tables(0).Columns(DSCounter))) Or IsNothing(FixNull(DSData.Tables(0).Rows(0)(DSCounter), DSData.Tables(0).Columns(DSCounter)))) Then
                                Exit For
                            End If
                        End If
                        '-------------------------------=============-----------------------------------------

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

    '==================================================================
    'Created by : [0259]
    'Date : 26/08/2007
    'Description: Check the null value coming form the database 
    '==================================================================
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

    Public Function GetLastContractNumber() As Integer
        Dim strCmd As String = " Select Max(hrs_Contracts.Number) From hrs_Contracts "
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strCmd)
    End Function

    Public Function GetLastContractID(ByVal intEmployeeID As Integer) As Integer
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, "hrs_GetLastContractID", intEmployeeID)
    End Function

    Public Function GetExpiredContracts(ByVal strEmpCode As String, ByVal strDeptCode As String, ByVal strBranchCode As String, ByVal dteFromDate As Date, ByVal dteToDate As Date, ByVal intContractTypeID As String) As DataSet
        Dim ObjDataset As New DataSet
        Dim clsDAL As New ClsDataAcessLayer(mPage)
        Dim ObjNav As New Venus.Shared.Web.NavigationHandler(clsDAL.ConnectionString)
        ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetAllExpiredContracts", strEmpCode.Trim, IIf(dteFromDate.Year = 1, Nothing, dteFromDate), IIf(dteToDate.Year = 1, Nothing, dteToDate), strDeptCode.Trim, strBranchCode.Trim, intContractTypeID, ObjNav.SetLanguage(mPage, "0/1"))
        Return ObjDataset
    End Function

    Public Function RenewContract(ByVal strEmpcode As String, Optional ByVal intContractID As Integer = 0) As Boolean
        Dim clsContractTransaction As New Clshrs_ContractsTransactions(mPage)
        Dim clsEmployee As New Clshrs_Employees(mPage)
        Dim clsContract As New Clshrs_Contracts(mPage)
        Dim clsNewContract As New Clshrs_Contracts(mPage)
        Dim intValidContract As Integer = intContractID
        Dim dsContractTransactions As DataSet
        Dim dsContractVacation As DataSet

        If clsContract.Find("ID=" & intValidContract) Then
            Dim intNewcontractID As Integer
            clsContractTransaction.GetContractsLastTransactions(intValidContract, 0, dsContractTransactions)
            clsContract.GetContractVacation(intValidContract, Nothing, 1, dsContractVacation)

            If IsNothing(clsContract.EndDate) Then
                clsContract.EndDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, IIf(clsContract.ContractPeriod = 0, 12, clsContract.ContractPeriod), clsContract.StartDate))
                clsContract.Update("ID=" & intValidContract)
            Else
                Dim dayscontracts As Integer = CDate(clsContract.EndDate).AddDays(1).Subtract(CDate(clsContract.StartDate)).Days
                clsNewContract.EndDate = DateAdd(DateInterval.Day, dayscontracts, CDate(clsContract.EndDate).AddDays(1))
            End If

            clsNewContract.EmployeeID = clsContract.EmployeeID
            clsNewContract.ContractTypeId = clsContract.ContractTypeId
            clsNewContract.EmployeeClassID = clsContract.EmployeeClassID
            clsNewContract.ContractPeriod = clsContract.ContractPeriod
            clsNewContract.ProfessionID = clsContract.ProfessionID
            clsNewContract.PositionID = clsContract.PositionID
            clsNewContract.GradeStepId = clsContract.GradeStepId
            clsNewContract.CurrencyId = clsContract.CurrencyId
            clsNewContract.StartDate = DateAdd(DateInterval.Day, 1, clsContract.EndDate)

            clsNewContract.Number = clsNewContract.GetLastContractNumber() + 1
            intNewcontractID = clsNewContract.SaveWithID()

            Dim strInsertCmd As String = "Set DateFormat dmy ;"

            For Each row As Data.DataRow In dsContractTransactions.Tables(0).Rows

                Dim strActiveDate_D As String = "0,0"
                Dim strActiveDate As Object = SetHigriDate2(GetHigriDate2(clsNewContract.StartDate, ""), strActiveDate_D)

                strInsertCmd &= " Insert Into hrs_ContractsTransactions (ContractId,TransactionTypeID,Active,Amount,IntervalID,PaidAtVacation,OnceAtPeriod,ActiveDate,ActiveDate_D) Values (" &
                                intNewcontractID & "," &
                                row("TransactionTypeID") & "," &
                                IIf(row("Active"), "1", "0") & "," &
                                row("Amount") & "," &
                                row("IntervalID") & "," &
                                row("PaidAtVacation") & "," &
                                IIf(row("OnceAtPeriod"), "1", "0") & "," &
                                "'" & strActiveDate & "'," &
                                "'" & strActiveDate_D & "');"
            Next

            For Each row As Data.DataRow In dsContractVacation.Tables(0).Rows
                strInsertCmd &= " Insert Into hrs_ContractsVacations(vacationtypeid,ContractId,FromMonth,ToMonth,DurationDays,TicketsRnd,DependantTicketRnd,MaxKeepDays)" &
                      " Values(" & row("VacationTypeID") &
                      "," & intNewcontractID &
                      "," & IIf(IsDBNull(row("FromMonth")), "Null", row("FromMonth")) &
                      "," & IIf(IsDBNull(row("ToMonth")), "Null", row("ToMonth")) &
                      "," & IIf(IsDBNull(row("DurationDays")), "Null", row("DurationDays")) &
                      "," & IIf(IsDBNull(row("TicketsRnd")), "Null", row("TicketsRnd")) &
                      "," & IIf(IsDBNull(row("DependantTicketRnd")), "Null", row("DependantTicketRnd")) &
                      "," & IIf(IsDBNull(row("MaxKeepDays")), "Null", row("MaxKeepDays")) & ")" &
                      ";" & vbNewLine
            Next
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(mConnectionString, CommandType.Text, strInsertCmd)
            Dim Mangcommand As String = "update hrs_employees set ExcludeDate = null where Code = '" & strEmpcode & "'"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(mConnectionString, Data.CommandType.Text, Mangcommand)
            Return True
        Else
            Return False
        End If

    End Function
End Class
