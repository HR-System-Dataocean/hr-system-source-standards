'Module            : Hrs (Human Resource Module)
'Date Created      : 23-08-2007
'Developer         : [0257]
'Description       : 1-Implement Data Acess Layer of hrs_EmployeesPayabilitiesSchedulesSettlement table with fields 
'                    2-Allow searching
'                    3-Get list with all codes
'                    4-Implement functions save(), update() and delete() to allow DML with some critera
'                    5-Implement functions first(),last(),next() and previous() to allow navigation between 
'                       records
'==========================================================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeesPayabilitySchedulesSettlement
    Inherits ClsDataAcessLayer

#Region "Class Constructors"
    '========================================================================
    'ProcedureName  :  Constractor 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    : Initialize insert ,update and delete commands
    'Developer      :[0257]   
    'Date Created   :23-08-2007
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EmployeesPayabilitiesSchedulesSettlement "
        mInsertParameter = " EmployeePayabilityScheduleID,Amount,Date,EmployeeTransactionID,RegUserID,RegComputerID,CompanyId,DocumentNO "
        mInsertParameterValues = " @EmployeePayabilityScheduleID,@Amount,@Date,@EmployeeTransactionID,@RegUserID,@RegComputerID,@CompanyId,@DocumentNO "
        mUpdateParameter = " EmployeePayabilityScheduleID=@EmployeePayabilityScheduleID,Amount=@Amount,Date=@Date,EmployeeTransactionID=@EmployeeTransactionID,DocumentNO=@DocumentNO"
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region

#Region "Private Members"
    Private mID As Object
    Private mEmployeePayabilityScheduleID As Integer
    Private mAmount As Object
    Private mDate As Object
    Private mEmployeeTransactionID As Integer
    'Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Private mCompanyId As Integer
    Private mDocumentNo As String
    Public mGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid

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
    Public Property EmployeePayabilityScheduleID() As Integer
        Get
            Return mEmployeePayabilityScheduleID
        End Get
        Set(ByVal Value As Integer)
            mEmployeePayabilityScheduleID = Value
        End Set
    End Property
    Public Property Amount() As Object
        Get
            Return mAmount
        End Get
        Set(ByVal Value As Object)
            mAmount = Value
        End Set
    End Property

    Public Property DDate() As Object
        Get
            Return mDate
        End Get
        Set(ByVal Value As Object)
            mDate = Value
        End Set
    End Property

    Public Property EmployeeTransactionID() As Integer
        Get
            Return mEmployeeTransactionID
        End Get
        Set(ByVal value As Integer)
            mEmployeeTransactionID = value
        End Set
    End Property

    'Public Property Remarks() As String
    '    Get
    '        Return mRemarks
    '    End Get
    '    Set(ByVal Value As String)
    '        mRemarks = Value
    '    End Set
    'End Property
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

    Public Property CompanyId() As Integer
        Get
            Return mCompanyId
        End Get
        Set(ByVal value As Integer)
            mCompanyId = value
        End Set
    End Property

    Public Property DocumentNO() As String
        Get
            Return mDocumentNo

        End Get
        Set(ByVal value As String)
            mDocumentNo = value
        End Set
    End Property

#End Region

#Region "Public Functions"
    '========================================================================
    'ProcedureName  :  SaveSatellment 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save All Satellments related to input employee payabilites
    'Developer      :  [0257]   
    'Date Created   :  23-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function SaveSatellment(PaidDate As Date, Optional DocumentNo As String = "") As Boolean
        Dim ObjRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
        Dim StrSqlCommand As String
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            StrSqlCommand = " Set DateFormat DMY " & vbNewLine
            For Each ObjRow In mGrid.Rows

                If Not ObjRow.Cells(3).Value Is DBNull.Value AndAlso ObjRow.Cells(3).Value > 0 Then

                    'StrSqlCommand &= "Insert Into hrs_EmployeesPayabilitiesSchedulesSettlement(" & _
                    '"EmployeePayabilityScheduleID," & _
                    '"Amount," & _
                    '"Date," & _
                    '"RegUserID," & _
                    '"CompanyId" & _
                    '")values(" & _
                    'ObjRow.Cells(5).Value & "," & _
                    'ObjRow.Cells(2).Value & ",'" & _
                    'Format(Date.Now, "dd/MM/yyyy") & "'," & _
                    'Me.mDataBaseUserRelatedID & "," & _
                    'Me.MainCompanyID & _
                    '");" & vbNewLine

                    StrSqlCommand &= "Insert Into hrs_EmployeesPayabilitiesSchedulesSettlement(" &
                    "EmployeePayabilityScheduleID," &
                    "Amount," &
                    "Date," &
                    "RegUserID," &
                    "CompanyId," &
                    "DocumentNo" &
                    ")values(" &
                    ObjRow.Cells(0).Value & "," &
                    ObjRow.Cells(3).Value & ",'" &
                    Format(PaidDate, "dd/MM/yyyy") & "'," &
                    Me.mDataBaseUserRelatedID & "," &
                    1 & "," &
                    DocumentNo &
                    ");" & vbNewLine

                End If

            Next
            mSqlCommand.CommandText = StrSqlCommand
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Save
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find All Satellments that match criteria
    'Developer      :  Added By  
    'Date Created   :  25-08-2007
    'Modifacations  :
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
            SetParameter(mSqlCommand, OperationType.Save)

            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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
            SetParameter(mSqlCommand, OperationType.Update)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function GetAllSettelmentsOfEmpTrans(ByVal intEmpTrans As Integer, ByRef ds As Data.DataSet) As Boolean
        Try
            Dim strCmd As String = CONFIG_DATEFORMAT & " Select * From  " & mTable & " as s Inner join hrs_EmployeesTransactions as t on s.EmployeeTransactionID = " & _
            " t.ID where t.ID = " & intEmpTrans
            ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, strCmd)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function DeleteAllSettelmentsOfEmpTrans(ByVal intEmpTrans As Integer) As Boolean
        Try
            Dim strCmd As String = CONFIG_DATEFORMAT & " Delete From  " & mTable & " where EmployeeTransactionID  = " & intEmpTrans & " "
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(mConnectionString, CommandType.Text, strCmd)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Find 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find All Satellments that match criteria
    'Developer      :  [0257]   
    'Date Created   :  23-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             : String    :used as criteria in select statment like 'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
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

    Public Function Delete(ByVal EmployeeTransactionid As Integer) As Boolean
        Dim StrSelectCommand As String
        Try
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(mConnectionString, "hrs_DeleteTransactionSettlements", EmployeeTransactionid)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function DeleteSettlments(ByVal EmployeePayabilityScheduleID As Integer) As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mDeleteCommand & " Where EmployeePayabilityScheduleID = " & EmployeePayabilityScheduleID
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Clear 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Clear all private attributes
    'Developer      :  [0257]   
    'Date Created   :  23-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mEmployeePayabilityScheduleID = 0
            mAmount = String.Empty
            mDate = Nothing
            ' mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mDocumentNo = String.Empty


        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region


#Region "Private Function"

    '========================================================================
    'ProcedureName  :  GetParameter 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Set private attributes with  parameters values
    'Developer      :  [0257]   
    'Date Created   :  23-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Ds                 :DataSet     :used its attributes to assign them to private attributes
    '========================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & Me.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mEmployeePayabilityScheduleID = mDataHandler.DataValue_Out(.Item("EmployeePayabilityScheduleID"), SqlDbType.Int, True)

                'mDate = mDataHandler.DataValue_Out(.Item("Date"), SqlDbType.DateTime)
                mAmount = mDataHandler.DataValue_Out(.Item("Amount"), SqlDbType.Money)
                If clsCompanies.IsHigry Then
                    mDate = mDataHandler.DataValue_Out(ClsGHCalender.GetRelativeDate(.Item("Date"), Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Output), SqlDbType.DateTime)
                Else
                    mDate = mDataHandler.DataValue_Out(ClsGHCalender.GetRelativeDate(.Item("Date"), Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Output), SqlDbType.DateTime)
                End If
                mEmployeeTransactionID = mDataHandler.DataValue_Out(.Item("EmployeeTransactionID"), SqlDbType.Int, True)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mCompanyId = mDataHandler.DataValue_Out(.Item("CompanyID"), SqlDbType.Int, True)
                mDocumentNo = mDataHandler.DataValue_Out(.Item("DocumentNO"), SqlDbType.VarChar)
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
    'Date Created   :23-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal operationtype As OperationType) As Boolean
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & Me.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeePayabilityScheduleID", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEmployeePayabilityScheduleID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Amount", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mAmount, SqlDbType.Money)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Date", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mDate, SqlDbType.DateTime)
            If clsCompanies.IsHigry Then
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Date", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(ClsGHCalender.GetRelativeDate(mDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input), SqlDbType.DateTime)
            Else
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Date", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(ClsGHCalender.GetRelativeDate(mDate, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input), SqlDbType.DateTime)
            End If
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeTransactionID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeTransactionID, SqlDbType.Int, True)
            Select Case operationtype
                Case ClsDataAcessLayer.OperationType.Save
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            End Select
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DocumentNO", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mDocumentNo, SqlDbType.VarChar)

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region

End Class
