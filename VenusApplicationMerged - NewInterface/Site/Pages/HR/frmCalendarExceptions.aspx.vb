Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports Microsoft.VisualBasic
Imports System.Data

Partial Class frmCalendarExceptions
    Inherits MainPage

#Region "Protected Sub"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)

        If Not IsPostBack Then
            lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
            Page.Session.Add("Lage", lblLage.Text)
            Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)
        End If
    End Sub
    Protected Sub Button_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Prepare.Command, ImageButton_Prepare.Command
        Try
            Dim StrMode As String = Request.QueryString.Item("Cls")
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Select Case DirectCast(e, System.Web.UI.WebControls.CommandEventArgs).CommandArgument
                Case "Prepare"
                    Dim dte As Date
                    If Date.TryParse(txtFromDate.Text, dte) Or Date.TryParse(txtToDate.Text, dte) Then
                        Dim frmDate As String = ClsDataAcessLayer.HijriToGreg(txtFromDate.Text, "dd/MM/yyyy")
                        Dim ToDate As String = ClsDataAcessLayer.HijriToGreg(txtToDate.Text, "dd/MM/yyyy")

                        Dim CntDays As Integer = Convert.ToDateTime(ToDate).Subtract(Convert.ToDateTime(frmDate)).Days
                        For CounDays As Integer = 0 To CntDays
                            Dim OperDate As DateTime = Convert.ToDateTime(frmDate).AddDays(CounDays)

                            clsEmployeesClassCalander = New Clshrs_EmployeesClassCalander(Page)
                            Dim bUseDefault As Boolean = False
                            Dim bNonWorking As Boolean = False
                            Dim dDateFrom As Date = Nothing
                            Dim dDateTo As Date = Nothing
                            Dim ClsDAL As New ClsDataAcessLayer(Page)
                            Dim dCalanderDate As Date = ClsDAL.SetHigriDate(OperDate)

                            If Weekday(OperDate) = FirstDayOfWeek.Saturday Then
                                If CheckBox1.Checked Then
                                    clsEmployeesClassCalander.fnDelete(OperDate.ToString("MM/dd/yyyy"), Request.QueryString.Item("Cls"))
                                    If rbtnSelectedDateType.SelectedValue = 0 Then
                                        bUseDefault = True
                                        bNonWorking = False
                                        dDateFrom = dCalanderDate
                                        dDateTo = dCalanderDate
                                        If fnAssignvalue(clsEmployeesClassCalander, OperDate, OperDate, bUseDefault, bNonWorking) Then
                                            clsEmployeesClassCalander.fnSaveToDB()
                                        End If
                                    ElseIf rbtnSelectedDateType.SelectedValue = 1 Then
                                        bUseDefault = False
                                        bNonWorking = True
                                        dDateFrom = dCalanderDate
                                        dDateTo = dCalanderDate
                                        If fnAssignvalue(clsEmployeesClassCalander, OperDate, OperDate, bUseDefault, bNonWorking) Then
                                            clsEmployeesClassCalander.fnSaveToDB()
                                        End If
                                    ElseIf rbtnSelectedDateType.SelectedValue = 2 Then
                                        Dim arrList As ArrayList
                                        Select Case fnCheckShifts()
                                            Case 1
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 2
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 3
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)
                                            Case 4
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 5
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit9.Text, WebDateTimeEdit10.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)
                                        End Select
                                    End If
                                End If
                            ElseIf Weekday(OperDate) = FirstDayOfWeek.Sunday Then
                                If CheckBox2.Checked Then
                                    clsEmployeesClassCalander.fnDelete(OperDate.ToString("MM/dd/yyyy"), Request.QueryString.Item("Cls"))
                                    If rbtnSelectedDateType.SelectedValue = 0 Then
                                        bUseDefault = True
                                        bNonWorking = False
                                        dDateFrom = dCalanderDate
                                        dDateTo = dCalanderDate
                                        If fnAssignvalue(clsEmployeesClassCalander, OperDate, OperDate, bUseDefault, bNonWorking) Then
                                            clsEmployeesClassCalander.fnSaveToDB()
                                        End If
                                    ElseIf rbtnSelectedDateType.SelectedValue = 1 Then
                                        bUseDefault = False
                                        bNonWorking = True
                                        dDateFrom = dCalanderDate
                                        dDateTo = dCalanderDate
                                        If fnAssignvalue(clsEmployeesClassCalander, OperDate, OperDate, bUseDefault, bNonWorking) Then
                                            clsEmployeesClassCalander.fnSaveToDB()
                                        End If
                                    ElseIf rbtnSelectedDateType.SelectedValue = 2 Then
                                        Dim arrList As ArrayList
                                        Select Case fnCheckShifts()
                                            Case 1
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 2
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 3
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)
                                            Case 4
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 5
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit9.Text, WebDateTimeEdit10.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)
                                        End Select
                                    End If
                                End If
                            ElseIf Weekday(OperDate) = FirstDayOfWeek.Monday Then
                                If CheckBox3.Checked Then
                                    clsEmployeesClassCalander.fnDelete(OperDate.ToString("MM/dd/yyyy"), Request.QueryString.Item("Cls"))
                                    If rbtnSelectedDateType.SelectedValue = 0 Then
                                        bUseDefault = True
                                        bNonWorking = False
                                        dDateFrom = dCalanderDate
                                        dDateTo = dCalanderDate
                                        If fnAssignvalue(clsEmployeesClassCalander, OperDate, OperDate, bUseDefault, bNonWorking) Then
                                            clsEmployeesClassCalander.fnSaveToDB()
                                        End If
                                    ElseIf rbtnSelectedDateType.SelectedValue = 1 Then
                                        bUseDefault = False
                                        bNonWorking = True
                                        dDateFrom = dCalanderDate
                                        dDateTo = dCalanderDate
                                        If fnAssignvalue(clsEmployeesClassCalander, OperDate, OperDate, bUseDefault, bNonWorking) Then
                                            clsEmployeesClassCalander.fnSaveToDB()
                                        End If
                                    ElseIf rbtnSelectedDateType.SelectedValue = 2 Then
                                        Dim arrList As ArrayList
                                        Select Case fnCheckShifts()
                                            Case 1
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 2
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 3
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)
                                            Case 4
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 5
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit9.Text, WebDateTimeEdit10.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)
                                        End Select
                                    End If
                                End If
                            ElseIf Weekday(OperDate) = FirstDayOfWeek.Tuesday Then
                                If CheckBox4.Checked Then
                                    clsEmployeesClassCalander.fnDelete(OperDate.ToString("MM/dd/yyyy"), Request.QueryString.Item("Cls"))
                                    If rbtnSelectedDateType.SelectedValue = 0 Then
                                        bUseDefault = True
                                        bNonWorking = False
                                        dDateFrom = dCalanderDate
                                        dDateTo = dCalanderDate
                                        If fnAssignvalue(clsEmployeesClassCalander, OperDate, OperDate, bUseDefault, bNonWorking) Then
                                            clsEmployeesClassCalander.fnSaveToDB()
                                        End If
                                    ElseIf rbtnSelectedDateType.SelectedValue = 1 Then
                                        bUseDefault = False
                                        bNonWorking = True
                                        dDateFrom = dCalanderDate
                                        dDateTo = dCalanderDate
                                        If fnAssignvalue(clsEmployeesClassCalander, OperDate, OperDate, bUseDefault, bNonWorking) Then
                                            clsEmployeesClassCalander.fnSaveToDB()
                                        End If
                                    ElseIf rbtnSelectedDateType.SelectedValue = 2 Then
                                        Dim arrList As ArrayList
                                        Select Case fnCheckShifts()
                                            Case 1
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 2
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 3
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)
                                            Case 4
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 5
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit9.Text, WebDateTimeEdit10.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)
                                        End Select
                                    End If
                                End If
                            ElseIf Weekday(OperDate) = FirstDayOfWeek.Wednesday Then
                                If CheckBox5.Checked Then
                                    clsEmployeesClassCalander.fnDelete(OperDate.ToString("MM/dd/yyyy"), Request.QueryString.Item("Cls"))
                                    If rbtnSelectedDateType.SelectedValue = 0 Then
                                        bUseDefault = True
                                        bNonWorking = False
                                        dDateFrom = dCalanderDate
                                        dDateTo = dCalanderDate
                                        If fnAssignvalue(clsEmployeesClassCalander, OperDate, OperDate, bUseDefault, bNonWorking) Then
                                            clsEmployeesClassCalander.fnSaveToDB()
                                        End If
                                    ElseIf rbtnSelectedDateType.SelectedValue = 1 Then
                                        bUseDefault = False
                                        bNonWorking = True
                                        dDateFrom = dCalanderDate
                                        dDateTo = dCalanderDate
                                        If fnAssignvalue(clsEmployeesClassCalander, OperDate, OperDate, bUseDefault, bNonWorking) Then
                                            clsEmployeesClassCalander.fnSaveToDB()
                                        End If
                                    ElseIf rbtnSelectedDateType.SelectedValue = 2 Then
                                        Dim arrList As ArrayList
                                        Select Case fnCheckShifts()
                                            Case 1
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 2
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 3
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)
                                            Case 4
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 5
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit9.Text, WebDateTimeEdit10.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)
                                        End Select
                                    End If
                                End If
                            ElseIf Weekday(OperDate) = FirstDayOfWeek.Thursday Then
                                If CheckBox6.Checked Then
                                    clsEmployeesClassCalander.fnDelete(OperDate.ToString("MM/dd/yyyy"), Request.QueryString.Item("Cls"))
                                    If rbtnSelectedDateType.SelectedValue = 0 Then
                                        bUseDefault = True
                                        bNonWorking = False
                                        dDateFrom = dCalanderDate
                                        dDateTo = dCalanderDate
                                        If fnAssignvalue(clsEmployeesClassCalander, OperDate, OperDate, bUseDefault, bNonWorking) Then
                                            clsEmployeesClassCalander.fnSaveToDB()
                                        End If
                                    ElseIf rbtnSelectedDateType.SelectedValue = 1 Then
                                        bUseDefault = False
                                        bNonWorking = True
                                        dDateFrom = dCalanderDate
                                        dDateTo = dCalanderDate
                                        If fnAssignvalue(clsEmployeesClassCalander, OperDate, OperDate, bUseDefault, bNonWorking) Then
                                            clsEmployeesClassCalander.fnSaveToDB()
                                        End If
                                    ElseIf rbtnSelectedDateType.SelectedValue = 2 Then
                                        Dim arrList As ArrayList
                                        Select Case fnCheckShifts()
                                            Case 1
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 2
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 3
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)
                                            Case 4
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 5
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit9.Text, WebDateTimeEdit10.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)
                                        End Select
                                    End If
                                End If
                            ElseIf Weekday(OperDate) = FirstDayOfWeek.Friday Then
                                If CheckBox7.Checked Then
                                    clsEmployeesClassCalander.fnDelete(OperDate.ToString("MM/dd/yyyy"), Request.QueryString.Item("Cls"))
                                    If rbtnSelectedDateType.SelectedValue = 0 Then
                                        bUseDefault = True
                                        bNonWorking = False
                                        dDateFrom = dCalanderDate
                                        dDateTo = dCalanderDate
                                        If fnAssignvalue(clsEmployeesClassCalander, OperDate, OperDate, bUseDefault, bNonWorking) Then
                                            clsEmployeesClassCalander.fnSaveToDB()
                                        End If
                                    ElseIf rbtnSelectedDateType.SelectedValue = 1 Then
                                        bUseDefault = False
                                        bNonWorking = True
                                        dDateFrom = dCalanderDate
                                        dDateTo = dCalanderDate
                                        If fnAssignvalue(clsEmployeesClassCalander, OperDate, OperDate, bUseDefault, bNonWorking) Then
                                            clsEmployeesClassCalander.fnSaveToDB()
                                        End If
                                    ElseIf rbtnSelectedDateType.SelectedValue = 2 Then
                                        Dim arrList As ArrayList
                                        Select Case fnCheckShifts()
                                            Case 1
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 2
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 3
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)
                                            Case 4
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                            Case 5
                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)

                                                arrList = fnGetDates(dCalanderDate, WebDateTimeEdit9.Text, WebDateTimeEdit10.Text)
                                                fnBelongTofnPrepareToSave(arrList, bUseDefault, bNonWorking)
                                        End Select
                                    End If
                                End If
                            End If
                        Next
                        txtFromDate.Text = ""
                        txtToDate.Text = ""
                        WebDateTimeEdit1.Text = ""
                        WebDateTimeEdit2.Text = ""
                        WebDateTimeEdit3.Text = ""
                        WebDateTimeEdit4.Text = ""
                        WebDateTimeEdit5.Text = ""
                        WebDateTimeEdit6.Text = ""
                        WebDateTimeEdit7.Text = ""
                        WebDateTimeEdit8.Text = ""
                        WebDateTimeEdit9.Text = ""
                        WebDateTimeEdit10.Text = ""
                        CheckBox1.Checked = False
                        CheckBox2.Checked = False
                        CheckBox3.Checked = False
                        CheckBox4.Checked = False
                        CheckBox5.Checked = False
                        CheckBox6.Checked = False
                        CheckBox7.Checked = False
                        rbtnSelectedDateType.SelectedIndex = -1

                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Operation Done /تمت العملية"))
                    Else
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "From - To Date Is Invalid /التاريخ من - الى غير صحيح"))
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub
    Private Function fnGetDates(ByVal dDate As Date, ByRef TxtShiftFrom As String, ByRef txtShiftTo As String) As ArrayList
        Dim arrListDates As New ArrayList
        Dim dDay As Integer = dDate.Day
        Dim dMonth As Integer = dDate.Month
        Dim dYear As Integer = dDate.Year

        Dim dHourFrom As Integer = TxtShiftFrom.Split(":")(0) 'Math.Truncate(CDbl(TxtShiftFrom))
        Dim dMinutesFrom As Integer = TxtShiftFrom.Split(":")(1).Split(" ")(0) 'Math.Round((CDbl(TxtShiftFrom) - Math.Truncate(CDbl(TxtShiftFrom))) * 100)

        Dim strShiftFrom As String = TxtShiftFrom.Split(":")(1).Split(" ")(1).Trim.ToUpper()
        Dim strShiftTo As String = txtShiftTo.Split(":")(1).Split(" ")(1).Trim.ToUpper()

        If (strShiftFrom = "PM" Or strShiftFrom = "م") Then
            If (dHourFrom <> 12) Then
                dHourFrom = dHourFrom + 12
            End If
        ElseIf ((strShiftFrom = "AM" Or strShiftFrom = "ص") And dHourFrom = 12) Then
            dHourFrom = 0
        End If

        Dim dHourTo As Integer = txtShiftTo.Split(":")(0) 'Math.Truncate(CDbl(txtShiftTo))
        Dim dMinutesTo As Integer = txtShiftTo.Split(":")(1).Split(" ")(0) 'Math.Round((CDbl(txtShiftTo) - Math.Truncate(CDbl(txtShiftTo))) * 100)

        If (strShiftTo = "PM" Or strShiftTo = "م") Then
            If (dHourTo <> 12) Then
                dHourTo = dHourTo + 12
            End If
        ElseIf ((strShiftTo = "AM" Or strShiftTo = "ص") And dHourTo = 12) Then
            dHourTo = 0

        End If
        '
        arrListDates.Add(fnMakeDateTime(dYear, dMonth, dDay, dHourFrom, dMinutesFrom, 0))
        arrListDates.Add(fnMakeDateTime(dYear, dMonth, dDay, dHourTo, dMinutesTo, 0))
        Return arrListDates
    End Function
    Private Function fnMakeDateTime(ByVal dYear As Integer, ByVal dMonth As Integer, ByVal dDay As Integer, ByVal dHours As Integer, ByVal dMinutes As Integer, Optional ByVal dSecounds As Integer = 0) As DateTime
        Dim dateConverted As New System.DateTime(dYear, dMonth, dDay, dHours, dMinutes, dSecounds)
        Return dateConverted
    End Function
    Private Function fnAssignvalue(ByRef clsEmployeesClassCalander As Clshrs_EmployeesClassCalander, ByVal dTimeFrom As Date, ByVal dTimeTo As Date, ByVal bUseDefault As Boolean, ByVal bNonWorking As Boolean) As Boolean
        Try
            With clsEmployeesClassCalander
                .EmployeeClassID = Request.QueryString.Item("Cls")
                .FromTime = dTimeFrom
                .ToTime = dTimeTo
                .useDefaultTime = bUseDefault
                .nonWorkingTime = bNonWorking
            End With
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Dim clsEmployeesClassCalander As Clshrs_EmployeesClassCalander
    Private Sub fnBelongTofnPrepareToSave(ByRef arrlist As ArrayList, ByVal bUseDefault As Boolean, ByVal bNonWorking As Boolean)
        If fnAssignvalue(clsEmployeesClassCalander, CDate(arrlist.Item(0)), CDate(arrlist.Item(1)), bUseDefault, bNonWorking) Then                      'Save 
            clsEmployeesClassCalander.fnSaveToDB()
        End If
        arrlist.Clear()
    End Sub
    Private Function fnCheckShifts() As Integer
        Dim nShifts As Integer = 0
        If Val(WebDateTimeEdit1.Text) > 0 And Val(WebDateTimeEdit2.Text) > 0 Then
            nShifts += 1
            If Val(WebDateTimeEdit3.Text) > 0 And Val(WebDateTimeEdit4.Text) > 0 Then
                nShifts += 1
                If Val(WebDateTimeEdit5.Text) > 0 And Val(WebDateTimeEdit6.Text) > 0 Then
                    nShifts += 1
                    If Val(WebDateTimeEdit7.Text) > 0 And Val(WebDateTimeEdit8.Text) > 0 Then
                        nShifts += 1
                        If Val(WebDateTimeEdit9.Text) > 0 And Val(WebDateTimeEdit10.Text) > 0 Then
                            nShifts += 1
                        End If
                    End If
                End If
            End If
        End If
        Return nShifts
    End Function
#End Region
End Class
