Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmProjectPlacementPlanning
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetData()
            If Request.QueryString.Item("PlacementsID") <> "" Then
                uwglocationshift.Bands(0).Columns(0).Hidden = True
                uwgalt.Bands(0).Columns(0).Hidden = True
                ImageButtonRefund.Visible = False
                ImageButton_Save.Visible = False
                Label28.Visible = False
                LabelAltNote.Visible = False
            End If
        End If
    End Sub
    Private Function SetData() As Boolean
        Try
            Dim ClsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
            Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(ClsProjectLocationDetails.ConnectionString)
            ClsProjectLocationDetails.Find("ID = " & Request.QueryString.Item("LocationDetailID"))
            Dim ClsProjectLocation As New Clshrs_ProjectLocations(Me)
            ClsProjectLocation.Find("ID = " & ClsProjectLocationDetails.LocationID)
            Dim ClsPositions As New Clshrs_Positions(Me)
            ClsPositions.Find("ID = " & ClsProjectLocationDetails.PositionID)
            Dim hrsProjectLocationShifts As New Clshrs_ProjectLocationShifts(Me)
            hrsProjectLocationShifts.Find("LocationDetailID = " & ClsProjectLocationDetails.ID)
            Dim hrsAttendanceTableShifts As New Clshrs_AttendanceTableShifts(Me)
            hrsAttendanceTableShifts.Find("ID = " & hrsProjectLocationShifts.AttendanceTableShiftID)
            Dim hrsAttendanceTable As New Clshrs_AttendanceTable(Me)
            hrsAttendanceTable.Find("ID = " & hrsAttendanceTableShifts.AttendanceTableID)
            lblProjectCode.Text = ClsProjectLocation.LocationDescription
            lblProjectName.Text = IIf(ClsNavHandler.SetLanguage(Me, "0/1") = 0, ClsPositions.EngName, ClsPositions.ArbName)
            Label_PlanName.Text = IIf(ClsNavHandler.SetLanguage(Me, "0/1") = 0, hrsAttendanceTable.EngName, hrsAttendanceTable.ArbName)

            'If ClsProjectLocationDetails.IsAlternative = True Then
            '    T1.Visible = True
            'Else
            '    T1.Visible = False
            'End If

            Dim str As String = "select distinct A.PlacementID," & _
                                "(select PlacementCode from hrs_ProjectPlacements where ID = A.PlacementID) AS PlacementCode," & _
                                "(select TimeIn from hrs_AttendanceTableShifts where ID = A.AttendanceTableShiftID) AS TimeIn," & _
                                "(select TimeOut from hrs_AttendanceTableShifts where ID = A.AttendanceTableShiftID) AS TimeOut," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 1 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Sat," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 2 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Sun," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 3 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Mon," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 4 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Tue," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 5 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Wed," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 6 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Thu," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 7 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Fri" & _
                                " from hrs_ProjectPlacementPlanning A inner join hrs_ProjectPlacements C on A.PlacementID = C.ID "
            If Request.QueryString.Item("LocationDetailID") = "" And Request.QueryString.Item("PlacementsID") = "" Then
            Else
                str = str & " where isnull(A.ReferenceTo,0) <> -1 and A.AttendanceTableShiftID is not null and isnull(C.RegComputerID,0) = 0 and " & IIf(Request.QueryString.Item("LocationDetailID") <> "", "C.LocationDetailID = " & Request.QueryString.Item("LocationDetailID"), "") & IIf(Request.QueryString.Item("PlacementsID") <> "", " and C.ID = " & Request.QueryString.Item("PlacementsID"), "")
            End If
            Dim ClsProjectLocationShifts As New Clshrs_ProjectLocationShifts(Me)
            uwglocationshift.Rows.Clear()
            uwglocationshift.DataSource = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsProjectLocationShifts.ConnectionString, Data.CommandType.Text, str).Tables(0)
            uwglocationshift.DataBind()

            str = "select distinct A.PlacementID," & _
                                "(select PlacementCode from hrs_ProjectPlacements where ID = A.PlacementID) AS PlacementCode," & _
                                "(select TimeIn from hrs_AttendanceTableShifts where ID = A.AttendanceTableShiftID) AS TimeIn," & _
                                "(select TimeOut from hrs_AttendanceTableShifts where ID = A.AttendanceTableShiftID) AS TimeOut," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 1 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Sat," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 2 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Sun," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 3 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Mon," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 4 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Tue," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 5 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Wed," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 6 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Thu," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 7 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Fri" & _
                                " from hrs_ProjectPlacementPlanning A inner join hrs_ProjectPlacements C on A.PlacementID = C.ID "
            If Request.QueryString.Item("LocationDetailID") = "" And Request.QueryString.Item("PlacementsID") = "" Then
            Else
                str = str & " where isnull(A.ReferenceTo,0) <> -2 and A.AttendanceTableShiftID is not null and isnull(C.RegComputerID,0) = 1 and " & IIf(Request.QueryString.Item("LocationDetailID") <> "", "C.LocationDetailID = " & Request.QueryString.Item("LocationDetailID"), "") & IIf(Request.QueryString.Item("PlacementsID") <> "", " and C.ID = " & Request.QueryString.Item("PlacementsID"), "")
            End If
            uwgalt.Rows.Clear()
            uwgalt.DataSource = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsProjectLocationShifts.ConnectionString, Data.CommandType.Text, str).Tables(0)
            uwgalt.DataBind()

            str = "select distinct A.PlacementID," & _
                "(select PlacementCode from hrs_ProjectPlacements where ID = A.PlacementID) AS PlacementCode," & _
                "(select TimeIn from hrs_AttendanceTableShifts where ID = A.AttendanceTableShiftID) AS TimeIn," & _
                "(select TimeOut from hrs_AttendanceTableShifts where ID = A.AttendanceTableShiftID) AS TimeOut," & _
                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 1 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID is null) AS Sat," & _
                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 2 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID is null) AS Sun," & _
                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 3 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID is null) AS Mon," & _
                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 4 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID is null) AS Tue," & _
                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 5 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID is null) AS Wed," & _
                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 6 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID is null) AS Thu," & _
                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 7 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID is null) AS Fri" & _
                " from hrs_ProjectPlacementPlanning A inner join hrs_ProjectPlacements C on A.PlacementID = C.ID "
            If Request.QueryString.Item("LocationDetailID") = "" And Request.QueryString.Item("PlacementsID") = "" Then
            Else
                str = str & " where isnull(A.ReferenceTo,0) <> -1 and A.AttendanceTableShiftID is null and isnull(C.RegComputerID,0) = 0 and " & IIf(Request.QueryString.Item("LocationDetailID") <> "", "C.LocationDetailID = " & Request.QueryString.Item("LocationDetailID"), "") & IIf(Request.QueryString.Item("PlacementsID") <> "", " and C.ID = " & Request.QueryString.Item("PlacementsID"), "")
            End If
            ClsProjectLocationShifts = New Clshrs_ProjectLocationShifts(Me)
            Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsProjectLocationShifts.ConnectionString, Data.CommandType.Text, str).Tables(0)
            Dim Tatbiq As Integer = 0
            For Each dr As DataRow In DT.Rows
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwglocationshift.Rows
                    If DGRow.Cells.FromKey("PlacementCode").Value = dr("PlacementCode") Then
                        If dr("Sat") = "1" Then
                            DGRow.Cells.FromKey("Sat").Style.BackColor = Drawing.Color.Red
                            Tatbiq = Tatbiq + 1
                        ElseIf dr("Sun") = "1" Then
                            DGRow.Cells.FromKey("Sun").Style.BackColor = Drawing.Color.Red
                            Tatbiq = Tatbiq + 1
                        ElseIf dr("Mon") = "1" Then
                            DGRow.Cells.FromKey("Mon").Style.BackColor = Drawing.Color.Red
                            Tatbiq = Tatbiq + 1
                        ElseIf dr("Tue") = "1" Then
                            DGRow.Cells.FromKey("Tue").Style.BackColor = Drawing.Color.Red
                            Tatbiq = Tatbiq + 1
                        ElseIf dr("Wed") = "1" Then
                            DGRow.Cells.FromKey("Wed").Style.BackColor = Drawing.Color.Red
                            Tatbiq = Tatbiq + 1
                        ElseIf dr("Thu") = "1" Then
                            DGRow.Cells.FromKey("Thu").Style.BackColor = Drawing.Color.Red
                            Tatbiq = Tatbiq + 1
                        ElseIf dr("Fri") = "1" Then
                            DGRow.Cells.FromKey("Fri").Style.BackColor = Drawing.Color.Red
                            Tatbiq = Tatbiq + 1
                        End If
                    End If
                Next
            Next
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwglocationshift.Rows
                If DGRow.Cells.FromKey("Sat").Style.BackColor <> Drawing.Color.Red Then
                    DGRow.Cells.FromKey("Sat").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                End If
                If DGRow.Cells.FromKey("Sun").Style.BackColor <> Drawing.Color.Red Then
                    DGRow.Cells.FromKey("Sun").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                End If
                If DGRow.Cells.FromKey("Mon").Style.BackColor <> Drawing.Color.Red Then
                    DGRow.Cells.FromKey("Mon").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                End If
                If DGRow.Cells.FromKey("Tue").Style.BackColor <> Drawing.Color.Red Then
                    DGRow.Cells.FromKey("Tue").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                End If
                If DGRow.Cells.FromKey("Wed").Style.BackColor <> Drawing.Color.Red Then
                    DGRow.Cells.FromKey("Wed").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                End If
                If DGRow.Cells.FromKey("Thu").Style.BackColor <> Drawing.Color.Red Then
                    DGRow.Cells.FromKey("Thu").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                End If
                If DGRow.Cells.FromKey("Fri").Style.BackColor <> Drawing.Color.Red Then
                    DGRow.Cells.FromKey("Fri").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                End If
            Next
            Dim CPlaceCode As String = ""
            Dim AlColr As Integer = 0
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgalt.Rows
                If CPlaceCode = "" Then
                    CPlaceCode = DGRow.Cells.FromKey("PlacementCode").Value
                End If
                If CPlaceCode = DGRow.Cells.FromKey("PlacementCode").Value Then
                    If AlColr = 0 Then
                        DGRow.Style.BackColor = Drawing.Color.White
                    Else
                        DGRow.Style.BackColor = Drawing.Color.Beige
                    End If
                Else
                    CPlaceCode = DGRow.Cells.FromKey("PlacementCode").Value
                    AlColr = IIf(AlColr = 0, 1, 0)
                    If AlColr = 0 Then
                        DGRow.Style.BackColor = Drawing.Color.White
                    Else
                        DGRow.Style.BackColor = Drawing.Color.Beige
                    End If
                End If
            Next
            If Tatbiq > 0 Then
                LblCnt1.Text = "عدد التطبيقات المقترحة هو " & Tatbiq.ToString() & " تطبيق مرمزين باللون الأحمر لجدول الدوامات المقترح للموظفين"
            Else
                LblCnt1.Text = ""
            End If
        Catch ex As Exception
        End Try
    End Function
    Protected Sub btnPrint_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnPrint.Click
        Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
    End Sub
    Protected Sub ImageButton_Save_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_Save.Click
        Dim ClsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwglocationshift.Rows
            If Convert.ToBoolean(DGRow.Cells.FromKey("IsActive").Value) = True Then
                Dim strcommand As String = "update hrs_ProjectPlacementPlanning set ReferenceTo = -1 where PlacementID in (select ID from hrs_ProjectPlacements where PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "' and LocationDetailID = " & Request.QueryString.Item("LocationDetailID") & ")"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
            End If
            If DGRow.Cells.FromKey("Sat").Style.BackColor = Drawing.Color.Red And Convert.ToBoolean(DGRow.Cells.FromKey("Sat").Value) = True Then
                Dim strcommand As String = "update hrs_ProjectPlacementPlanning set ReferenceTo = -1 where DayID = 1 and PlacementID in (select ID from hrs_ProjectPlacements where PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "' and LocationDetailID = " & Request.QueryString.Item("LocationDetailID") & ")"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
            End If
            If DGRow.Cells.FromKey("Sun").Style.BackColor = Drawing.Color.Red And Convert.ToBoolean(DGRow.Cells.FromKey("Sun").Value) = True Then
                Dim strcommand As String = "update hrs_ProjectPlacementPlanning set ReferenceTo = -1 where DayID = 2 and PlacementID in (select ID from hrs_ProjectPlacements where PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "' and LocationDetailID = " & Request.QueryString.Item("LocationDetailID") & ")"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
            End If
            If DGRow.Cells.FromKey("Mon").Style.BackColor = Drawing.Color.Red And Convert.ToBoolean(DGRow.Cells.FromKey("Mon").Value) = True Then
                Dim strcommand As String = "update hrs_ProjectPlacementPlanning set ReferenceTo = -1 where DayID = 3 and PlacementID in (select ID from hrs_ProjectPlacements where PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "' and LocationDetailID = " & Request.QueryString.Item("LocationDetailID") & ")"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
            End If
            If DGRow.Cells.FromKey("Tue").Style.BackColor = Drawing.Color.Red And Convert.ToBoolean(DGRow.Cells.FromKey("Tue").Value) = True Then
                Dim strcommand As String = "update hrs_ProjectPlacementPlanning set ReferenceTo = -1 where DayID = 4 and PlacementID in (select ID from hrs_ProjectPlacements where PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "' and LocationDetailID = " & Request.QueryString.Item("LocationDetailID") & ")"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
            End If
            If DGRow.Cells.FromKey("Wed").Style.BackColor = Drawing.Color.Red And Convert.ToBoolean(DGRow.Cells.FromKey("Wed").Value) = True Then
                Dim strcommand As String = "update hrs_ProjectPlacementPlanning set ReferenceTo = -1 where DayID = 5 and PlacementID in (select ID from hrs_ProjectPlacements where PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "' and LocationDetailID = " & Request.QueryString.Item("LocationDetailID") & ")"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
            End If
            If DGRow.Cells.FromKey("Thu").Style.BackColor = Drawing.Color.Red And Convert.ToBoolean(DGRow.Cells.FromKey("Thu").Value) = True Then
                Dim strcommand As String = "update hrs_ProjectPlacementPlanning set ReferenceTo = -1 where DayID = 6 and PlacementID in (select ID from hrs_ProjectPlacements where PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "' and LocationDetailID = " & Request.QueryString.Item("LocationDetailID") & ")"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
            End If
            If DGRow.Cells.FromKey("Fri").Style.BackColor = Drawing.Color.Red And Convert.ToBoolean(DGRow.Cells.FromKey("Fri").Value) = True Then
                Dim strcommand As String = "update hrs_ProjectPlacementPlanning set ReferenceTo = -1 where DayID = 7 and PlacementID in (select ID from hrs_ProjectPlacements where PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "' and LocationDetailID = " & Request.QueryString.Item("LocationDetailID") & ")"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
            End If
        Next
        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgalt.Rows
            If Convert.ToBoolean(DGRow.Cells.FromKey("IsActive").Value) = True Then
                Dim strcommand As String = "update hrs_ProjectPlacementPlanning set ReferenceTo = -2 where PlacementID in (select ID from hrs_ProjectPlacements where PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "' and LocationDetailID = " & Request.QueryString.Item("LocationDetailID") & ")"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
            End If
        Next
        SetData()
    End Sub
    Protected Sub ImageButtonRefund_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonRefund.Click
        Dim ClsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
        Dim strcommand As String = "update hrs_ProjectPlacementPlanning set ReferenceTo = null where ReferenceTo = -1 and PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & Request.QueryString.Item("LocationDetailID") & ")"
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
        strcommand = "update hrs_ProjectPlacementPlanning set ReferenceTo = 1 where ReferenceTo = -2 and PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & Request.QueryString.Item("LocationDetailID") & ")"
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
        SetData()
    End Sub
End Class
