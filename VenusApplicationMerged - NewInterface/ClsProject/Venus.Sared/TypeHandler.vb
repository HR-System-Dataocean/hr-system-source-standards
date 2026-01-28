Public Interface IvTypes
    Property Value() As Object
    ReadOnly Property SQLString(Optional ByVal Format As String = "") As String
    ReadOnly Property ToDB() As Object
    WriteOnly Property FromDB() As Object
End Interface

Public Class vDate
    Implements ComponentModel.INotifyPropertyChanged
    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#Region "Public Enum"
    Public Enum CalendarType
        E_Calendar = 0
        G_Calendar = 1
        H_Calendar = 2
    End Enum
    Private mConnectionString As String
#End Region

#Region "Public Constractor"
    Public Sub New(Optional ByVal ConnectionString As String = "")
        mConnectionString = ConnectionString
    End Sub
#End Region

#Region "Private Members"
    Private mvValue As Date = Nothing
#End Region

#Region "Public property"
    Public Property Value(Optional ByVal DateType As CalendarType = CalendarType.G_Calendar) As Date
        Get
            Select Case DateType
                Case CalendarType.E_Calendar, CalendarType.G_Calendar
                    Return mvValue
                Case CalendarType.H_Calendar
                    Return HijriDate
            End Select
        End Get
        Set(ByVal value As Date)
            mvValue = value
        End Set
    End Property
    Public ReadOnly Property FormatedValue(Optional ByVal DateType As CalendarType = CalendarType.G_Calendar, _
                                           Optional ByVal Format As String = "dd/MM/yyyy") As String
        Get
            If mvValue = Nothing Then
                Return ""
            Else
                Select Case DateType
                    Case CalendarType.E_Calendar, CalendarType.G_Calendar
                        Return mvValue.ToString(Format)
                    Case CalendarType.H_Calendar
                        Return HijriDate
                    Case Else
                        Return mvValue.ToString(Format)
                End Select
            End If
        End Get
    End Property
    Public Property HijriDate() As String
        Get
            If String.IsNullOrEmpty(mConnectionString) Then
                MsgBox("Connection String For Hijri Date should be supplied", MsgBoxStyle.Critical, "Venus")
                Return ""
            Else
                Return RelativeDate(mvValue, mConnectionString, CalendarType.H_Calendar)
            End If
        End Get
        Set(ByVal value As String)
            If String.IsNullOrEmpty(mConnectionString) Then
                MsgBox("Connection String For Hijri Date should be supplied", MsgBoxStyle.Critical, "Venus")
                mvValue = Nothing
            Else
                mvValue = RelativeDate(mvValue, mConnectionString, CalendarType.G_Calendar)
            End If
        End Set
    End Property
    Public ReadOnly Property ToNumber(Optional ByVal DaysofYear As Integer = 265 _
                                      , Optional ByVal DaysofMonth As Integer = 30 _
                                      , Optional ByVal MonthofYear As Integer = 12) As Integer
        Get
            Return DateToNumber(mvValue, DaysofYear, DaysofMonth, MonthofYear)
        End Get
    End Property
    Public ReadOnly Property SQLString(Optional ByVal Format As String = "dd/MM/yyyy") As String
        Get
            If mvValue = #12:00:00 AM# Then
                Return "'Null'"
            Else
                Return "'" & mvValue.ToString(Format) & "'"
            End If
        End Get
    End Property
    Public ReadOnly Property ToDB() As Object
        Get
            If mvValue = #12:00:00 AM# Then
                Return DBNull.Value
            Else
                Return mvValue
            End If
        End Get
    End Property
    Public WriteOnly Property FromDB() As Object
        Set(ByVal value As Object)
            If Not IsDBNull(value) AndAlso IsDate(value) Then
                mvValue = value
            Else
                mvValue = Nothing
            End If
        End Set
    End Property
#End Region

#Region "Shared Function"
    Public Shared Function RelativeDate(ByVal vDate As Object, ByVal strConnectionString As String, Optional ByVal CalType As CalendarType = CalendarType.E_Calendar) As String
        Dim DefultCalType As CalendarType
        Try
            If vDate.ToString = String.Empty Or vDate.ToString = "01/01/0001 12:00:00 AM" Then
                Return String.Empty
                Exit Function
            End If
            vDate = Format(CDate(vDate), "dd/MM/yyyy")
            If Val(Right$(vDate, 4)) < 1900 Then
                DefultCalType = CalendarType.H_Calendar
            Else
                DefultCalType = CalendarType.G_Calendar
            End If
            If CalType = CalendarType.E_Calendar Then
                If DefultCalType = CalendarType.G_Calendar Then
                    Return vDate
                Else
                    Return GetRelativeGregorianDate(vDate, CalendarType.G_Calendar, strConnectionString)
                End If
            Else
                If CalType = DefultCalType Then
                    Return vDate
                Else
                    Return GetRelativeGregorianDate(vDate, CalType, strConnectionString)
                End If
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Shared Function DateToNumber(ByVal DteDate As Date, ByVal DaysofYear As Integer, ByVal DaysofMonth As Integer, ByVal MonthofYear As Integer) As Integer
        Dim IntDay As Integer
        Dim IntMonth As Integer
        Dim IntYear As Integer
        Dim DateInNumber As Integer
        Try
            IntDay = DteDate.Day
            IntMonth = DteDate.Month
            IntYear = DteDate.Year
            If IntDay = 31 Then IntDay = DaysofMonth
            If IntMonth = 2 And IntDay = 28 Or IntDay = 29 Then IntDay = DaysofMonth
            DateInNumber = IntYear * DaysofYear + (IntMonth - 1) * DaysofMonth + IntDay
            Return DateInNumber
        Catch ex As Exception

        End Try
    End Function

    Private Shared Function GetRelativeGregorianDate(ByVal srchDate As String, ByVal srchCalType As CalendarType, ByVal strConnectionString As String) As String
        Dim StrCommandString As String
        Dim ObjSqlCommand As New SqlClient.SqlCommand
        Dim ObjDataReader As Data.SqlClient.SqlDataReader
        Try
            If srchCalType = CalendarType.G_Calendar Then
                StrCommandString = "Set DateFormat DMy  Select GDate As RelativeDate From sys_GHCalendar Where HDate='" & srchDate & "'"
            Else
                StrCommandString = "Set DateFormat DMy  Select HDate As RelativeDate  From sys_GHCalendar Where GDate='" & srchDate & "'"
            End If
            With ObjSqlCommand
                .Connection = New SqlClient.SqlConnection(strConnectionString)
                .CommandText = StrCommandString
                .CommandType = CommandType.Text
                .Connection.Open()
                ObjDataReader = .ExecuteReader()
                If ObjDataReader.Read() Then
                    GetRelativeGregorianDate = ObjDataReader("RelativeDate")
                Else
                    GetRelativeGregorianDate = String.Empty
                End If
                .Connection.Close()
            End With
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
#End Region

End Class

Public Class vIDate
    Implements IvTypes

#Region "Public Enum"
    Public Enum CalendarType
        E_Calendar = 0
        G_Calendar = 1
        H_Calendar = 2
    End Enum
    Private mConnectionString As String
#End Region

#Region "Public Constractor"
    Public Sub New(Optional ByVal ConnectionString As String = "")
        mConnectionString = ConnectionString
    End Sub
#End Region

#Region "Private Members"
    Private mvValue As Date = Nothing
#End Region

#Region "Public property"
    Public Property Value() As Object Implements IvTypes.Value
        Get
            Return mvValue
        End Get
        Set(ByVal value As Object)
            mvValue = value
        End Set
    End Property
    Public ReadOnly Property FormatedValue(Optional ByVal Format As String = "dd/MM/yyyy") As String
        Get
            If mvValue = Nothing Then
                Return ""
            Else
                Return mvValue.ToString(Format)
            End If
        End Get
    End Property
    Public Property HijriDate() As String
        Get
            If String.IsNullOrEmpty(mConnectionString) Then
                MsgBox("Connection String For Hijri Date should be supplied", MsgBoxStyle.Critical, "Venus")
                Return ""
            Else
                Return RelativeDate(mvValue, mConnectionString, CalendarType.H_Calendar)
            End If
        End Get
        Set(ByVal value As String)
            If String.IsNullOrEmpty(mConnectionString) Then
                MsgBox("Connection String For Hijri Date should be supplied", MsgBoxStyle.Critical, "Venus")
                mvValue = Nothing
            Else
                mvValue = RelativeDate(mvValue, mConnectionString, CalendarType.G_Calendar)
            End If
        End Set
    End Property
    Public ReadOnly Property ToNumber(Optional ByVal DaysofYear As Integer = 265 _
                                      , Optional ByVal DaysofMonth As Integer = 30 _
                                      , Optional ByVal MonthofYear As Integer = 12) As Integer
        Get
            Return DateToNumber(mvValue, DaysofYear, DaysofMonth, MonthofYear)
        End Get
    End Property
    Public ReadOnly Property SQLString(Optional ByVal Format As String = "") As String Implements IvTypes.SQLString
        Get
            If mvValue = #12:00:00 AM# Then
                Return "'Null'"
            Else
                Return "'" & mvValue.ToString(Format) & "'"
            End If
        End Get
    End Property
    Public WriteOnly Property FromDB() As Object Implements IvTypes.FromDB
        Set(ByVal value As Object)
            If Not IsDBNull(value) AndAlso IsDate(value) Then
                mvValue = value
            Else
                mvValue = Nothing
            End If
        End Set
    End Property
    Public ReadOnly Property ToDB() As Object Implements IvTypes.ToDB
        Get
            If mvValue = #12:00:00 AM# Then
                Return DBNull.Value
            Else
                Return mvValue
            End If
        End Get
    End Property
#End Region

#Region "Shared Function"
    Public Shared Function RelativeDate(ByVal vDate As Object, ByVal strConnectionString As String, Optional ByVal CalType As CalendarType = CalendarType.E_Calendar) As String
        Dim DefultCalType As CalendarType
        Try
            If vDate.ToString = String.Empty Or vDate.ToString = "01/01/0001 12:00:00 AM" Then
                Return String.Empty
                Exit Function
            End If
            vDate = Format(CDate(vDate), "dd/MM/yyyy")
            If Val(Right$(vDate, 4)) < 1900 Then
                DefultCalType = CalendarType.H_Calendar
            Else
                DefultCalType = CalendarType.G_Calendar
            End If
            If CalType = CalendarType.E_Calendar Then
                If DefultCalType = CalendarType.G_Calendar Then
                    Return vDate
                Else
                    Return GetRelativeGregorianDate(vDate, CalendarType.G_Calendar, strConnectionString)
                End If
            Else
                If CalType = DefultCalType Then
                    Return vDate
                Else
                    Return GetRelativeGregorianDate(vDate, CalType, strConnectionString)
                End If
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Shared Function DateToNumber(ByVal DteDate As Date, ByVal DaysofYear As Integer, ByVal DaysofMonth As Integer, ByVal MonthofYear As Integer) As Integer
        Dim IntDay As Integer
        Dim IntMonth As Integer
        Dim IntYear As Integer
        Dim DateInNumber As Integer
        Try
            IntDay = DteDate.Day
            IntMonth = DteDate.Month
            IntYear = DteDate.Year
            If IntDay = 31 Then IntDay = DaysofMonth
            If IntMonth = 2 And IntDay = 28 Or IntDay = 29 Then IntDay = DaysofMonth
            DateInNumber = IntYear * DaysofYear + (IntMonth - 1) * DaysofMonth + IntDay
            Return DateInNumber
        Catch ex As Exception

        End Try
    End Function

    Private Shared Function GetRelativeGregorianDate(ByVal srchDate As String, ByVal srchCalType As CalendarType, ByVal strConnectionString As String) As String
        Dim StrCommandString As String
        Dim ObjSqlCommand As New SqlClient.SqlCommand
        Dim ObjDataReader As Data.SqlClient.SqlDataReader
        Try
            If srchCalType = CalendarType.G_Calendar Then
                StrCommandString = "Set DateFormat DMy  Select GDate As RelativeDate From sys_GHCalendar Where HDate='" & srchDate & "'"
            Else
                StrCommandString = "Set DateFormat DMy  Select HDate As RelativeDate  From sys_GHCalendar Where GDate='" & srchDate & "'"
            End If
            With ObjSqlCommand
                .Connection = New SqlClient.SqlConnection(strConnectionString)
                .CommandText = StrCommandString
                .CommandType = CommandType.Text
                .Connection.Open()
                ObjDataReader = .ExecuteReader()
                If ObjDataReader.Read() Then
                    GetRelativeGregorianDate = ObjDataReader("RelativeDate")
                Else
                    GetRelativeGregorianDate = String.Empty
                End If
                .Connection.Close()
            End With
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
#End Region

End Class

Public Class vString
    Implements IvTypes

#Region "Private Members"
    Private mvValue As String = String.Empty
#End Region

#Region "Public property"
    Public Property Value() As Object Implements IvTypes.Value
        Get
            Return mvValue
        End Get
        Set(ByVal value As Object)
            mvValue = value
        End Set
    End Property
    Public ReadOnly Property SQLString(Optional ByVal Format As String = "") As String Implements IvTypes.SQLString
        Get
            Return "'" & mvValue.Replace("'", "''") & "'"
        End Get
    End Property
    Public WriteOnly Property FromDB() As Object Implements IvTypes.FromDB
        Set(ByVal value As Object)
            If Not IsDBNull(value) AndAlso Not String.IsNullOrEmpty(value) Then
                mvValue = value
            Else
                mvValue = String.Empty
            End If
        End Set
    End Property
    Public ReadOnly Property ToDB() As Object Implements IvTypes.ToDB
        Get
            If String.IsNullOrEmpty(mvValue) Then
                Return DBNull.Value
            Else
                Return mvValue
            End If
        End Get
    End Property
#End Region

End Class

Public Class vInteger
    Implements IvTypes

#Region "Private Members"
    Private mvValue As Double = 0.0
#End Region

    Public Property Value() As Object Implements IvTypes.Value
        Get

        End Get
        Set(ByVal value As Object)

        End Set
    End Property
    Public ReadOnly Property SQLString(Optional ByVal Format As String = "") As String Implements IvTypes.SQLString
        Get

        End Get
    End Property
    Public WriteOnly Property FromDB() As Object Implements IvTypes.FromDB
        Set(ByVal value As Object)

        End Set
    End Property
    Public ReadOnly Property ToDB() As Object Implements IvTypes.ToDB
        Get

        End Get
    End Property

End Class

Public Class vBoolean
    Implements IvTypes

    Public Property Value() As Object Implements IvTypes.Value
        Get

        End Get
        Set(ByVal value As Object)

        End Set
    End Property
    Public ReadOnly Property SQLString(Optional ByVal Format As String = "") As String Implements IvTypes.SQLString
        Get

        End Get
    End Property
    Public WriteOnly Property FromDB() As Object Implements IvTypes.FromDB
        Set(ByVal value As Object)

        End Set
    End Property
    Public ReadOnly Property ToDB() As Object Implements IvTypes.ToDB
        Get

        End Get
    End Property

End Class

Public Class vDouble
    Implements IvTypes

    Public WriteOnly Property FromDB() As Object Implements IvTypes.FromDB
        Set(ByVal value As Object)

        End Set
    End Property
    Public ReadOnly Property SQLString(Optional ByVal Format As String = "") As String Implements IvTypes.SQLString
        Get

        End Get
    End Property
    Public ReadOnly Property ToDB() As Object Implements IvTypes.ToDB
        Get

        End Get
    End Property
    Public Property Value() As Object Implements IvTypes.Value
        Get

        End Get
        Set(ByVal value As Object)

        End Set
    End Property

End Class

