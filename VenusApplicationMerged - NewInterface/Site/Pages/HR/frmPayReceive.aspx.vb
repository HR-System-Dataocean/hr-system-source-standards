Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.IO

Partial Class frmPayReceive
    Inherits MainPage

#Region "Public Decleration"

    Private ClsEmployeesPayability As Clshrs_EmployeesPayability

#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim StrSerial As String = String.Empty
        ClsEmployeesPayability = New Clshrs_EmployeesPayability(Me.Page)
        If Not IsPostBack Then
            Dim clsMainCountry As New Clssys_Countries(Page)
            Dim clsMainCurrency As New ClsSys_Currencies(Page)
            clsMainCountry.Find(" IsMainCountries = 1 ")
            If clsMainCountry.ID > 0 Then
                clsMainCurrency.Find(" ID=" & clsMainCountry.CurrencyID)
                If Not IsNothing(clsMainCurrency.NoDecimalPlaces) Then
                    uwgBenetitTemplet.Columns(2).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgBenetitTemplet.Columns(2).Format, clsMainCurrency.NoDecimalPlaces)
                    uwgBenetitTemplet.Columns(3).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgBenetitTemplet.Columns(3).Format, clsMainCurrency.NoDecimalPlaces)
                    uwgBenetitTemplet.Columns(4).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgBenetitTemplet.Columns(4).Format, clsMainCurrency.NoDecimalPlaces)
                    ' txtSettlementAmont.MinDecimalPlaces = clsMainCurrency.NoDecimalPlaces
                    lblTransVal.MinDecimalPlaces = clsMainCurrency.NoDecimalPlaces
                End If
            End If

            '    uwgBenetitTemplet.DisplayLayout.Bands(0).Columns(4).AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes

            ClsEmployeesPayability.Find("ID=" & IntId)
            GetValues(ClsEmployeesPayability)
        End If
        '  txtSettlementAmont.Attributes.Add("onBlur", "CloseSettlementAmont();")
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtSettlementAmont)
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click
        Try
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesPayability.ConnectionString)
            Dim ObjRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
            Dim Ischecked As Integer = 0
            For Each ObjRow In uwgBenetitTemplet.Rows
                If (ObjRow.Cells(3).Value > 0) Then
                    Ischecked += 1
                End If
            Next
            If Ischecked > 0 And (WebNumericDocumentDo.Text = "" Or WebNumericDocumentDo.Text = String.Empty) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " please add Document Number /يرجى إدخال رقم السند"))
                Return

            End If
            Dim settlementAmount As Integer = Convert.ToInt32(txtSettlementAmont.Value)
            Dim TotalAmount As Double = lblTransVal.Text
            Dim NewFirstSetlAmount As Integer = Convert.ToInt32(uwgBenetitTemplet.Rows(0).Cells(4).Value)
            Dim Trnsdate As DateTime = Convert.ToDateTime(uwgBenetitTemplet.Rows(0).Cells(1).Value)

            Dim str As String = ""
            Dim IntId As Integer = Request.QueryString.Item("ID")
            ClsEmployeesPayability = New Clshrs_EmployeesPayability(Page)
            ClsEmployeesPayability.Find("ID=" & IntId)


            Dim ClsEmployeesPayabilitySattelment As New Clshrs_EmployeesPayabilitySchedulesSettlement(Page)
            ClsEmployeesPayabilitySattelment.mGrid = uwgBenetitTemplet
            If Ischecked > 0 Then
                ClsEmployeesPayabilitySattelment.Find("DocumentNO ='" & WebNumericDocumentDo.Text & "'")
                If ClsEmployeesPayabilitySattelment.DataSet.Tables(0).Rows.Count > 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Document Number is already saved to other loan  /رقم السند مسجل لقسط آخر"))
                    Return
                End If
            End If


            'If settlementAmount <> NewFirstSetlAmount And settlementAmount > 0 Then
            '    If ClsEmployeesPayability.SalaryLink = True Then
            '        'If settlementAmount = 0 Or settlementAmount = Nothing Then
            '        '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " please set settlement Amount /برجاء ادخال مبلغ القسط"))
            '        'End If


            '        If NewFirstSetlAmount > TotalAmount Then
            '            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " The first Settlment amount is greater than Total amount /قيمة القسط الاول أكبر من قيمة السلفة"))
            '        Else
            '            str = "delete from hrs_EmployeesPayabilitiesSchedules where id not in (select EmployeePayabilityScheduleID from hrs_EmployeesPayabilitiesSchedulesSettlement) and EmployeePayabilityID=" & IntId & ";"
            '            ' Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeesPayabilitySattelment.ConnectionString, System.Data.CommandType.Text, strdelete)

            '            TotalAmount = TotalAmount - NewFirstSetlAmount
            '            str &= "set dateformat dmy insert into hrs_EmployeesPayabilitiesSchedules(EmployeePayabilityID,DueDate,DueAmount,CompanyID,RegDate,RegUserID)values(" & IntId & ",'" & Trnsdate & "'," & NewFirstSetlAmount & "," & 1 & ",'" & DateTime.Now.Date & "'," & ClsEmployeesPayability.DataBaseUserRelatedID & ");"
            '            While TotalAmount > 0
            '                If TotalAmount < settlementAmount Then
            '                    settlementAmount = TotalAmount
            '                End If
            '                Trnsdate = Trnsdate.AddMonths(1)
            '                str &= "insert into hrs_EmployeesPayabilitiesSchedules(EmployeePayabilityID,DueDate,DueAmount,CompanyID,RegDate,RegUserID)values(" & IntId & ",'" & Trnsdate & "'," & settlementAmount & "," & 1 & ",'" & DateTime.Now.Date & "'," & ClsEmployeesPayability.DataBaseUserRelatedID & ");"

            '                TotalAmount = TotalAmount - settlementAmount

            '            End While

            '            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeesPayabilitySattelment.ConnectionString, System.Data.CommandType.Text, str)



            '        End If

            '    End If
            'End If
            Dim ss As Date
            If Trim(lblDescTransD.Text) = "" Or Date.TryParse(lblDescTransD.Text, ss) = False Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " please add Paid Date /يرجى إدخال تاريخ سداد القسط"))
                Return
            End If
            ClsEmployeesPayabilitySattelment.SaveSatellment(CDate(lblDescTransD.Text), WebNumericDocumentDo.Text)


            ' upload the attchmnet
            Dim RowCount As Integer = 0
            Dim Clsobjects As New Clssys_Objects(Me.Page)
            Dim ClsDocumentAttachment = New Clssys_ObjectsAttachments(Me.Page)

            Clsobjects.Find(" Code = REPLACE('" & ClsEmployeesPayabilitySattelment.Table.Trim() & "',' ' ,'')")
            Dim StrRealPath As String = Request.PhysicalApplicationPath
            StrRealPath = StrRealPath + "Uploads\"

            If txtAttachedFile.HasFile Then
                If Not System.IO.Directory.Exists(StrRealPath) Then
                    System.IO.Directory.CreateDirectory(StrRealPath)
                End If
                Dim arrTempFileName As String() = txtAttachedFile.FileName.Split(".")
                Dim strFileName = arrTempFileName(0) & "_" & DateTime.Now.ToString("ddMMyyyyHHmmsss") & "." & arrTempFileName(1)
                If Not System.IO.File.Exists(StrRealPath & strFileName) Then

                    If txtAttachedFile.FileName <> "" Then
                        ClsEmployeesPayabilitySattelment = New Clshrs_EmployeesPayabilitySchedulesSettlement(Page)

                        ClsEmployeesPayabilitySattelment.Find("DocumentNO ='" & WebNumericDocumentDo.Text & "'")
                        For Each row In ClsEmployeesPayabilitySattelment.DataSet.Tables(0).Rows

                            ClsDocumentAttachment.ObjectID = Clsobjects.ID
                            ClsDocumentAttachment.RecordID = row("ID")
                            ClsDocumentAttachment.FileName = strFileName
                            ClsDocumentAttachment.ExpiryDate = Nothing
                            ClsDocumentAttachment.FolderName = "Uploads"
                            ClsDocumentAttachment.Save()
                            If RowCount < 1 Then
                                SaveIMage(ClsDocumentAttachment, row("ID"), Clsobjects.ID)
                            End If

                            RowCount = RowCount + 1
                        Next
                    End If

                End If
            End If


            'SetReturnback()
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            GetValues(ClsEmployeesPayability)
        Catch ex As Exception

        End Try
    End Sub
    Private Function SaveIMage(ByRef ClsObjectsAttachments As Clssys_ObjectsAttachments, RecordID As Integer, ObjectID As Integer) As Boolean


        '=================
        Dim fileOK As Boolean = False
        Dim fileExtension As String
        fileExtension = System.IO.Path.GetExtension(txtAttachedFile.FileName).ToLower()
        Dim allowedExtensions As String() = {".jpg", ".jpeg", ".png", ".gif", ".bmp"}
        For i As Integer = 0 To allowedExtensions.Length - 1
            If fileExtension = allowedExtensions(i) Then
                fileOK = True
                Exit For
            End If
        Next
        If txtAttachedFile.FileName = Nothing Then
            fileOK = True
        End If
        '=================
        Try
            Dim StrRealPath As String = Request.PhysicalApplicationPath & "Uploads\"
            Dim strPath As String = StrRealPath & ObjectID & "_" & RecordID
            If Not Directory.Exists(strPath) Then
                Directory.CreateDirectory(strPath)
            End If
            If fileOK Then
                txtAttachedFile.SaveAs(strPath & "\" & ClsObjectsAttachments.FileName)
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Protected Sub btnDelete_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnDelete.Click
        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
    End Sub

    Protected Sub uwgBenetitTemplet_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgBenetitTemplet.InitializeRow
        '-------------------------------0257 MODIFIED-----------------------------------------

        uwgBenetitTemplet.DisplayLayout.Rows(0).Cells(4).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes
        Dim clsCompanies As New Clssys_Companies(Page)
        clsCompanies.Find("ID = " & clsCompanies.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(Page)
        '-------------------------------=============-----------------------------------------
        Dim startDate As Date = e.Row.Cells(1).Value

        '-------------------------------0257 MODIFIED-----------------------------------------
        Dim mDataHandler As New Venus.Shared.DataHandler
        If clsCompanies.IsHigry Then
            startDate = mDataHandler.DataValue_In(ClsGHCalender.GetRelativeDate(startDate, ClsGHCalender.DateType.Hijri, ClsGHCalender.Directions.Output), System.Data.SqlDbType.DateTime)
        Else
            startDate = mDataHandler.DataValue_In(ClsGHCalender.GetRelativeDate(startDate, ClsGHCalender.DateType.Gregorian, ClsGHCalender.Directions.Output), System.Data.SqlDbType.DateTime)
        End If
        '-------------------------------=============-----------------------------------------
        e.Row.Cells(1).Value = startDate

    End Sub
    Protected Sub WebImageButton_Up_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles WebImageButton_Up.Click
        ShiftLoans(-1)
    End Sub
    Protected Sub WebImageButton_Down_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles WebImageButton_Down.Click
        ShiftLoans(1)
    End Sub
#End Region

#Region "Private Function"
    Public Function GetValues(ByRef ClsEmployeesPayability As Clshrs_EmployeesPayability) As Boolean
        Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(Page)
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsNationality As New ClsBasicFiles(Page, "sys_Nationalities")
        Dim ClsEmploueesPayabilitySchedules As New Clshrs_EmployeesPayabilitySchedules(Page)
        Try
            With ClsEmployeesPayability
                If ClsEmployee.Find("ID=" & IIf(.EmployeeID Is Nothing, 0, .EmployeeID)) Then
                    lblDescEnglishName.Text = ClsEmployee.Name
                    lblDescEmployeeCode.Text = ClsEmployee.Code
                End If
                lblDescLoanCode.Text = .Number
                'lblDescTransD.Text = .TransactionDate
                lblTransVal.Text = .GetTransactionAmount(.ID) - .GetSettlementsAmount(.ID)
                txtSettlementAmont.Text = .GetInstalmentAmountNotPaid(.ID)
                If ClsEmploueesPayabilitySchedules.FindPayments("EmployeePayabilityId=" & IIf(.ID Is Nothing, 0, .ID) & " and hrs_EmployeesPayabilitiesSchedules.dueAmount - IsNull((Select Sum(Amount) From hrs_EmployeesPayabilitiesSchedulesSettlement Where hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID=hrs_EmployeesPayabilitiesSchedules.id),0) > 0") Then
                    uwgBenetitTemplet.DataSource = ClsEmploueesPayabilitySchedules.DataSet.Tables(0)
                    uwgBenetitTemplet.DataBind()
                End If
            End With
            WebNumericDocumentDo.Text = ""
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub ShiftLoans(ByVal Add As Integer)
        Dim ClsEmploueesPayabilitySchedules As New Clshrs_EmployeesPayabilitySchedules(Page)
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEmployeesPayability.Find("ID=" & IntId)
        ClsEmploueesPayabilitySchedules.FindPayments("EmployeePayabilityId=" & IIf(ClsEmployeesPayability.ID Is Nothing, 0, ClsEmployeesPayability.ID) & " and hrs_EmployeesPayabilitiesSchedules.dueAmount - IsNull((Select Sum(Amount) From hrs_EmployeesPayabilitiesSchedulesSettlement Where hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID=hrs_EmployeesPayabilitiesSchedules.id),0) > 0")
        Dim Dt1 As Data.DataTable = ClsEmploueesPayabilitySchedules.DataSet.Tables(0)
        For i As Integer = 0 To Dt1.Rows.Count - 1
            ClsEmploueesPayabilitySchedules.Find("ID = " & Dt1.Rows(i)("ID").ToString())
            ClsEmploueesPayabilitySchedules.DueDate = Convert.ToDateTime(ClsEmploueesPayabilitySchedules.DueDate).AddMonths(Add)
            ClsEmploueesPayabilitySchedules.Update("ID = " & Dt1.Rows(i)("ID").ToString())
        Next i
        uwgBenetitTemplet.DataSource = Nothing
        uwgBenetitTemplet.DataBind()
        GetValues(ClsEmployeesPayability)
    End Sub
#End Region


End Class
