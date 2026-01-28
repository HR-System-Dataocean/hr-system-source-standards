'Project           : Venus V.
'Module            : Hrs (Human Resource Module)
'Date Created      : 05-08-20007
'Developer         : [0257]
'Description       : 1-Implement Data Acess Layer of hrs_EmployeesClassesCalenderSet table 
'                    2-Allow searching
'                    3-Implement functions save(), update() and delete() to allow DML with some critera
'                    4-Implement functions first(),last(),next() and previous() to allow navigation between 
'                       records
'==========================================================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeesClassesCalenderSet
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EmployeesClassesCalenderSet "
        mInsertParameter = " DayNumber,EmployeeClassID,FromTime,ToTime,UseDefaultTime,NonWorkingTime,Remarks,RegUserID,RegComputerID "
        mInsertParameterValues = " @DayNumber,@EmployeeClassID,@FromTime,@ToTime,@UseDefaultTime,@NonWorkingTime,@Remarks,@RegUserID,@RegComputerID "
        mUpdateParameter = "DayNumber=@DayNumber,EmployeeClassID=@EmployeeClassID,FromTime=@FromTime,ToTime=@ToTime,UseDefaultTime=@UseDefaultTime,NonWorkingTime=@NonWorkingTime,Remarks=@Remarks"
        mSelectCommand = " Select e.ID,e.DayNumber,e.EmployeeClassID,e.FromTime,e.ToTime,e.UseDefaultTime,e.NonWorkingTime,e.Remarks,e.RegUserID,e.RegComputerID,e.RegDate,e.CancelDate From  " & mTable & " as e INNER JOIN hrs_EmployeesClasses as c ON e.EmployeeClassID = c.ID"
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Object

    Private mDayNumber As Integer
    Private mEmployeeClassID As Integer
    Private mFromTime As String
    Private mToTime As String
    Private mUseDefaultTime As Boolean
    Private mNonWorkingTime As Boolean

    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
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

    Public Property DayNumber() As Integer
        Get
            Return mDayNumber
        End Get
        Set(ByVal Value As Integer)
            mDayNumber = Value
        End Set
    End Property

    Public Property EmployeeClassID() As Integer
        Get
            Return mEmployeeClassID
        End Get
        Set(ByVal Value As Integer)
            mEmployeeClassID = Value
        End Set
    End Property

    Public Property FromTime() As String
        Get
            Return mFromTime
        End Get
        Set(ByVal Value As String)
            mFromTime = Value
        End Set
    End Property
    Public Property ToTime() As String
        Get
            Return mToTime
        End Get
        Set(ByVal Value As String)
            mToTime = Value
        End Set
    End Property

    Public Property UseDefaultTime() As Boolean
        Get
            Return mUseDefaultTime
        End Get
        Set(ByVal value As Boolean)
            mUseDefaultTime = value
        End Set
    End Property

    Public Property NonWorkingTime() As Boolean
        Get
            Return mNonWorkingTime
        End Get
        Set(ByVal value As Boolean)
            mNonWorkingTime = value
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
#End Region
#Region "Public Function"

    '========================================================================
    'ProcedureName  :  Find 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  [0257]   
    'Date Created   :  09-09-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(e.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(e.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(e.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(e.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            'StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
    'ProcedureName  :  Save
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save new record
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [0257]   
    'Date Created   :  09-09-2007
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
            SetParameter(mSqlCommand, "S")
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
    'Developer      :  [0257]   
    'Date Created   :  09-09-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand, "U")
            'mSqlCommand.Connection.Open()
            'mSqlCommand.ExecuteNonQuery()
            'mSqlCommand.Connection.Close()
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
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
    'Date Created   :09-09-2007
    'Modifacations  : 
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
            SetParameter(mSqlCommand, "D")
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
    'Description    :  Delete record that match critera without set CancelDate with value 
    '                  and return true if operation done otherwise report errors in ErrorPage                    
    'Developer      :  [0257]   
    'Date Created   :  09-09-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function DeleteAll(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = "Delete from  " & mTable & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            SetParameter(mSqlCommand, "D")
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
    'Date Created   :09-09-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mDayNumber = Nothing
            mEmployeeClassID = Nothing
            mFromTime = String.Empty
            mToTime = String.Empty
            mUseDefaultTime = Nothing
            mNonWorkingTime = Nothing


            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing

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


    '========================================================================
    'ProcedureName  :  GetShiftsDays
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get All non deafult days with summation  of working hours to specific employee class                   
    'Developer      :  [0257]   
    'Date Created   :  09-09-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'classID             :int     :class id
    '========================================================================

    Public Function GetShiftsDays(ByVal classID As Integer) As Hashtable

        Dim result As New Hashtable()
        Dim Filter As String = " e.EmployeeClassID = " & classID & "AND NonWorkingTime = 0 "

        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(e.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(e.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(e.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(e.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
            Else
                Clear()
            End If
            Dim addList As New ArrayList()
            If mDataSet.Tables(0).Rows.Count > 0 Then
                Dim i As Integer
                Dim hcount As Double = 0
                For i = 0 To mDataSet.Tables(0).Rows.Count - 1
                    Dim currRow As Data.DataRow = mDataSet.Tables(0).Rows(i)
                    Dim strF As String = currRow("FromTime")
                    Dim strT As String = currRow("ToTime")
                    Dim hF As Integer = strF.Split(":")(0)
                    Dim mF As Integer = strF.Split(":")(1).Split(" ")(0)
                    If (strF.Split(":")(1).Split(" ")(1).Trim.ToUpper = "PM" And hF <> 12) Then
                        hF = hF + 12
                    ElseIf (strF.Split(":")(1).Split(" ")(1).Trim.ToUpper = "AM" And hF = 12) Then
                        hF = 0
                    End If

                    Dim hT As Integer = strT.Split(":")(0)
                    Dim mT As Integer = strT.Split(":")(1).Split(" ")(0)

                    If (strT.Split(":")(1).Split(" ")(1).Trim.ToUpper = "PM" And hT <> 12) Then
                        hT = hT + 12
                    ElseIf (strT.Split(":")(1).Split(" ")(1).Trim.ToUpper = "AM" And hT = 12) Then
                        hT = 0
                    End If

                    If ((Not currRow("UseDefaultTime")) And (addList.IndexOf(currRow("DayNumber")) = -1)) Then
                        hcount = 0
                        addList.Add(currRow("DayNumber"))
                        hcount = hcount + Math.Abs((hT - hF) + ((mT - mF) / 60))
                        result.Add(currRow("DayNumber"), hcount)
                    ElseIf (Not currRow("UseDefaultTime")) Then
                        hcount = result(currRow("DayNumber"))
                        hcount = hcount + Math.Abs((hT - hF) + ((mT - mF) / 60))
                        result(currRow("DayNumber")) = hcount

                    End If
                Next

                Return result
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function

    '========================================================================
    'ProcedureName  :  GetDayNumber
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  This function recieve date and return day number in the week                   
    'Developer      :  [0257]   
    'Date Created   :  09-09-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'currDate             :Date     :input date
    '========================================================================
    Public Function GetDayNumber(ByVal currDate As Date) As Integer
        Dim glob As New Globalization.DateTimeFormatInfo()
        Dim dayArr As String() = glob.DayNames()
        Dim strDay As String = currDate.DayOfWeek.ToString()
        Dim dayNumber As Integer = Array.IndexOf(dayArr, strDay)
        Return dayNumber

    End Function

    '========================================================================
    'ProcedureName  :  IsVacationDay
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  This function recieve date and true if this day is vacation in specific employee class                   
    'Developer      :  [0257]   
    'Date Created   :  09-09-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'currDate             :Date     :input date
    'classID              :int      :class id
    '========================================================================
    Public Function IsVacationDay(ByVal currDate As Date, ByVal classID As Integer) As Boolean

        Dim glob As New Globalization.DateTimeFormatInfo()
        Dim dayArr As String() = glob.DayNames()

        Dim strDay As String = currDate.DayOfWeek.ToString()

        Dim dayNumber As Integer = Array.IndexOf(dayArr, strDay)

        Dim Filter As String = " e.DayNumber = " & dayNumber & " AND e.EmployeeClassID = " & classID & " "

        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(e.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(e.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(e.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(e.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
            Else
                Clear()
            End If
            If mID > 0 And mNonWorkingTime Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function

    '========================================================================
    'ProcedureName  :  GetVacationDays
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  This function recieve employee class id and get all vacation days to it in specific 
    '                  fiscal period (enterd or get period of current date)
    'Developer      :  [0257]   
    'Date Created   :  09-09-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'ClassID             :int     :class id 
    'FiscalPeriodID      :int     :optional fiscal period id
    '========================================================================
    Public Function GetVacationDays(ByVal ClassID As Integer, Optional ByVal FiscalPeriodID As Integer = 0) As ArrayList
        Dim datesList As New ArrayList()

        Dim Filter As String = String.Empty
        If (FiscalPeriodID = 0) Then
            Filter = " getDate() between FromDate And ToDate"
        Else
            Filter = " ID=" & FiscalPeriodID
        End If

        Dim fromDate As Date
        Dim toDate As Date

        Dim StrSelectCommand As String
        Try
            StrSelectCommand = " Select * from sys_FiscalYearsPeriods where " & Filter
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then

            Else
                Return datesList
            End If
            If (mDataSet.Tables(0).Rows.Count <= 0) Then
                Return datesList
            Else
                fromDate = mDataHandler.DataValue_Out(mDataSet.Tables(0).Rows(0)("FromDate"), SqlDbType.SmallDateTime)
                toDate = mDataHandler.DataValue_Out(mDataSet.Tables(0).Rows(0)("ToDate"), SqlDbType.SmallDateTime)
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

        Dim noOfDays As Integer = Math.Abs(DateDiff(DateInterval.Day, fromDate, toDate))

        Dim index As Integer
        Dim currDate As Date = fromDate
        For index = 0 To noOfDays
            If (IsVacationDay(currDate, ClassID)) Then
                datesList.Add(currDate)
            End If
            currDate = currDate.AddDays(1)
        Next

        Return datesList




    End Function

    '========================================================================
    'ProcedureName  :  GetAllWorkingDays
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  This function recieve employee class id and get all working days with actual working
    '                   hours  to it in specific fiscal period (enterd or get period of current date)
    'Developer      :  [0257]   
    'Date Created   :  09-09-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'ClassID             :int     :class id 
    'FiscalPeriodID      :int     :optional fiscal period id
    '========================================================================
    Public Function GetAllWorkingDays(ByVal ClassID As Integer, Optional ByVal FiscalPeriodID As Integer = 0) As Object(,)
        Dim datesList As New ArrayList()
        Dim objectsList(365, 1) 'As Object

        Dim Filter As String = String.Empty
        If (FiscalPeriodID = 0) Then
            Filter = " getDate() between FromDate And ToDate"
        Else
            Filter = " ID=" & FiscalPeriodID
        End If

        Dim fromDate As Date
        Dim toDate As Date

        Dim StrSelectCommand As String
        Try
            StrSelectCommand = " Select * from sys_FiscalYearsPeriods where " & Filter
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then

            Else
                Return objectsList
            End If
            If (mDataSet.Tables(0).Rows.Count <= 0) Then
                Return objectsList
            Else
                fromDate = mDataHandler.DataValue_Out(mDataSet.Tables(0).Rows(0)("FromDate"), SqlDbType.SmallDateTime)
                toDate = mDataHandler.DataValue_Out(mDataSet.Tables(0).Rows(0)("ToDate"), SqlDbType.SmallDateTime)

            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try


        Dim DefaultWorkingHours As Double = 0

        Try
            StrSelectCommand = " Select WorkHoursPerDay from hrs_EmployeesClasses where ID=" & ClassID

            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrSelectCommand
            mSqlCommand.Connection.Open()
            DefaultWorkingHours = mDataHandler.DataValue_Out(mSqlCommand.ExecuteScalar(), SqlDbType.Real)
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try


        Dim noOfDays As Integer = Math.Abs(DateDiff(DateInterval.Day, fromDate, toDate))
        Dim hashShifts As Hashtable = GetShiftsDays(ClassID)
        Dim index As Integer
        Dim currDate As Date = fromDate

        For index = 0 To noOfDays
            If (Not IsVacationDay(currDate, ClassID)) Then
                If (hashShifts.ContainsKey(GetDayNumber(currDate))) Then
                    Dim shiftHours As Double = hashShifts(GetDayNumber(currDate))
                    objectsList(index, 0) = currDate
                    objectsList(index, 1) = shiftHours
                Else
                    objectsList(index, 0) = currDate
                    objectsList(index, 1) = DefaultWorkingHours
                End If
            End If


            currDate = currDate.AddDays(1)
        Next
        Return objectsList
    End Function


#Region "Navigation Functions"
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :[0257]   
    'Date Created   :09-09-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmBloodGroups.aspx.vb)TlbMainNavigation_ButtonClicked()
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
    'Date Created   :09-09-2007
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

                mDayNumber = mDataHandler.DataValue_Out(.Item("DayNumber"), SqlDbType.Int)
                mEmployeeClassID = mDataHandler.DataValue_Out(.Item("EmployeeClassID"), SqlDbType.Int, True)
                mFromTime = mDataHandler.DataValue_Out(.Item("FromTime"), SqlDbType.VarChar)
                mToTime = mDataHandler.DataValue_Out(.Item("ToTime"), SqlDbType.VarChar)
                mUseDefaultTime = mDataHandler.DataValue_Out(.Item("UseDefaultTime"), SqlDbType.Bit)
                mNonWorkingTime = mDataHandler.DataValue_Out(.Item("NonWorkingTime"), SqlDbType.Bit)


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
    'Date Created   :09-09-2007
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
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strStatus As String) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DayNumber", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mDayNumber, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeClassID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeClassID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FromTime", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFromTime, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ToTime", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mToTime, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@UseDefaultTime", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mUseDefaultTime, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NonWorkingTime", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mNonWorkingTime, SqlDbType.Bit)


            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)

            If (strStatus = "S") Then
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            End If
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CancelDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mCancelDate, SqlDbType.DateTime)

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

