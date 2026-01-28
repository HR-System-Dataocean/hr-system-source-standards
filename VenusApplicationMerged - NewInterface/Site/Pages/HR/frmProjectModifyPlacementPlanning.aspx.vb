Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmProjectModifyPlacementPlanning
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HiddenField_ChangeID.Value = 0
            txtDate.Value = DateTime.Now.ToString("ddMMyyyy")
            Dim ClsProjects As New Clshrs_Projects(Me, "hrs_Projects")
            ClsProjects.GetDropDownList(ddlProject, True, "IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and convert(Datetime,'" & txtDate.Text & "') between StartDate and EndDate")

            ddlLocation.Items.Clear()
            ddlPosition.Items.Clear()

            Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
            If clsprojectchange.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtDate.Text & "') order by ID DESC") Then
                Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
                cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & clsprojectchange.ID)

                Dim clsPositions As New Clshrs_Positions(Me)
                clsPositions.GetDropDownListForProjects(ddlPosition, True, "B.LocationID = " & ddlLocation.SelectedValue)

                HiddenField_ChangeID.Value = clsprojectchange.ID
            End If
        End If
    End Sub
    Private Function SetData() As Boolean
        Try
            Dim ClsProjectLocation As New Clshrs_ProjectLocations(Me)
            ClsProjectLocation.Find("ID = '" & ddlLocation.SelectedValue & "'")
            Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(ClsProjectLocation.ConnectionString)

            Dim str As String = "select distinct A.PlacementID," & _
                                "(select PlacementCode from hrs_ProjectPlacements where ID = A.PlacementID) AS PlacementCode," & _
                                "A.AttendanceTableShiftID AS ShiftID,(select TimeIn from hrs_AttendanceTableShifts where ID = A.AttendanceTableShiftID) AS TimeIn," & _
                                "(select TimeOut from hrs_AttendanceTableShifts where ID = A.AttendanceTableShiftID) AS TimeOut," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 1 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Sat," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 2 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Sun," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 3 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Mon," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 4 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Tue," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 5 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Wed," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 6 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Thu," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 7 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Fri" & _
                                " from hrs_ProjectPlacementPlanning A inner join hrs_ProjectPlacements C on A.PlacementID = C.ID "
            str = str & " where isnull(A.ReferenceTo,0) <> -1 and A.AttendanceTableShiftID is not null and isnull(C.RegComputerID,0) = 0 and C.LocationDetailID = " & ddlPosition.SelectedValue
            Dim ClsProjectLocationShifts As New Clshrs_ProjectLocationShifts(Me)
            uwglocationshift.Rows.Clear()
            uwglocationshift.DataSource = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsProjectLocationShifts.ConnectionString, Data.CommandType.Text, str).Tables(0)
            uwglocationshift.DataBind()

            str = "select distinct A.PlacementID," & _
                                "(select PlacementCode from hrs_ProjectPlacements where ID = A.PlacementID) AS PlacementCode," & _
                                "A.AttendanceTableShiftID AS ShiftID,(select TimeIn from hrs_AttendanceTableShifts where ID = A.AttendanceTableShiftID) AS TimeIn," & _
                                "(select TimeOut from hrs_AttendanceTableShifts where ID = A.AttendanceTableShiftID) AS TimeOut," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 1 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Sat," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 2 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Sun," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 3 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Mon," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 4 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Tue," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 5 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Wed," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 6 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Thu," & _
                                "(select COUNT(ID) from hrs_ProjectPlacementPlanning B where DayID = 7 and B.PlacementID = A.PlacementID and B.AttendanceTableShiftID = A.AttendanceTableShiftID) AS Fri" & _
                                " from hrs_ProjectPlacementPlanning A inner join hrs_ProjectPlacements C on A.PlacementID = C.ID "
            str = str & " where isnull(A.ReferenceTo,0) <> -2 and A.AttendanceTableShiftID is not null and isnull(C.RegComputerID,0) = 1 and C.LocationDetailID = " & ddlPosition.SelectedValue
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
            str = str & " where isnull(A.ReferenceTo,0) <> -1 and A.AttendanceTableShiftID is null and isnull(C.RegComputerID,0) = 0 and C.LocationDetailID = " & ddlPosition.SelectedValue
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
        Catch ex As Exception
        End Try
    End Function
    Protected Sub ImageButton_Save_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_Save.Click
        Dim ClsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
        If ClsProjectLocationDetails.Find("ID = " & ddlPosition.SelectedValue) Then
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwglocationshift.Rows
                Dim strcommand As String = "delete from hrs_ProjectPlacementPlanning where PlacementID in (select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "');"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                If Convert.ToBoolean(DGRow.Cells.FromKey("Sat").Value) = True Then
                    strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),1,null)"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                Else
                    If ClsProjectLocationDetails.IsAlternative = True Then
                        strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),1,-1)"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    End If
                End If
                If Convert.ToBoolean(DGRow.Cells.FromKey("Sun").Value) = True Then
                    strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),2,null)"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                Else
                    If ClsProjectLocationDetails.IsAlternative = True Then
                        strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),2,-1)"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    End If
                End If
                If Convert.ToBoolean(DGRow.Cells.FromKey("Mon").Value) = True Then
                    strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),3,null)"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                Else
                    If ClsProjectLocationDetails.IsAlternative = True Then
                        strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),3,-1)"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    End If
                End If
                If Convert.ToBoolean(DGRow.Cells.FromKey("Tue").Value) = True Then
                    strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),4,null)"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                Else
                    If ClsProjectLocationDetails.IsAlternative = True Then
                        strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),4,-1)"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    End If
                End If
                If Convert.ToBoolean(DGRow.Cells.FromKey("Wed").Value) = True Then
                    strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),5,null)"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                Else
                    If ClsProjectLocationDetails.IsAlternative = True Then
                        strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),5,-1)"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    End If
                End If
                If Convert.ToBoolean(DGRow.Cells.FromKey("Thu").Value) = True Then
                    strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),6,null)"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                Else
                    If ClsProjectLocationDetails.IsAlternative = True Then
                        strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),6,-1)"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    End If
                End If
                If Convert.ToBoolean(DGRow.Cells.FromKey("Fri").Value) = True Then
                    strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),7,null)"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                Else
                    If ClsProjectLocationDetails.IsAlternative = True Then
                        strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),7,-1)"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    End If
                End If
            Next
            Dim CPlacementCode As String = ""
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgalt.Rows
                Dim strcommand As String = ""
                If CPlacementCode <> DGRow.Cells.FromKey("PlacementCode").Value Then
                    strcommand = "delete from hrs_ProjectPlacementPlanning where PlacementID in (select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "');"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    CPlacementCode = DGRow.Cells.FromKey("PlacementCode").Value
                End If
                If Convert.ToBoolean(DGRow.Cells.FromKey("Sat").Value) = True Then
                    strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),1,1)"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where ID in (select ID from hrs_ProjectPlacementPlanning where ReferenceTo = -1 and DayID = 1 and AttendanceTableShiftID = " & DGRow.Cells.FromKey("ShiftID").Value & " and PlacementID in (select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & "))")
                End If
                If Convert.ToBoolean(DGRow.Cells.FromKey("Sun").Value) = True Then
                    strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),2,1)"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where ID in (select ID from hrs_ProjectPlacementPlanning where ReferenceTo = -1 and DayID = 2 and AttendanceTableShiftID = " & DGRow.Cells.FromKey("ShiftID").Value & " and PlacementID in (select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & "))")
                End If
                If Convert.ToBoolean(DGRow.Cells.FromKey("Mon").Value) = True Then
                    strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),3,1)"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where ID in (select ID from hrs_ProjectPlacementPlanning where ReferenceTo = -1 and DayID = 3 and AttendanceTableShiftID = " & DGRow.Cells.FromKey("ShiftID").Value & " and PlacementID in (select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & "))")
                End If
                If Convert.ToBoolean(DGRow.Cells.FromKey("Tue").Value) = True Then
                    strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),4,1)"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where ID in (select ID from hrs_ProjectPlacementPlanning where ReferenceTo = -1 and DayID = 4 and AttendanceTableShiftID = " & DGRow.Cells.FromKey("ShiftID").Value & " and PlacementID in (select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & "))")
                End If
                If Convert.ToBoolean(DGRow.Cells.FromKey("Wed").Value) = True Then
                    strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),5,1)"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where ID in (select ID from hrs_ProjectPlacementPlanning where ReferenceTo = -1 and DayID = 5 and AttendanceTableShiftID = " & DGRow.Cells.FromKey("ShiftID").Value & " and PlacementID in (select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & "))")
                End If
                If Convert.ToBoolean(DGRow.Cells.FromKey("Thu").Value) = True Then
                    strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),6,1)"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where ID in (select ID from hrs_ProjectPlacementPlanning where ReferenceTo = -1 and DayID = 6 and AttendanceTableShiftID = " & DGRow.Cells.FromKey("ShiftID").Value & " and PlacementID in (select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & "))")
                End If
                If Convert.ToBoolean(DGRow.Cells.FromKey("Fri").Value) = True Then
                    strcommand = "insert into hrs_ProjectPlacementPlanning values (" & DGRow.Cells.FromKey("ShiftID").Value & ",(select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & " and PlacementCode = '" & DGRow.Cells.FromKey("PlacementCode").Value & "'),7,1)"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, strcommand)
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where ID in (select ID from hrs_ProjectPlacementPlanning where ReferenceTo = -1 and DayID = 7 and AttendanceTableShiftID = " & DGRow.Cells.FromKey("ShiftID").Value & " and PlacementID in (select ID from hrs_ProjectPlacements where LocationID = " & ddlLocation.SelectedValue & "))")
                End If
            Next
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "update hrs_ProjectPlacementPlanning set AttendanceTableShiftID = null,ReferenceTo = null where ReferenceTo = -1")
            SetData()
            Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsProjectLocationDetails.ConnectionString)
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Update Done!/تم التحديث"))
        End If
    End Sub
    Protected Sub ddlProject_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProject.SelectedIndexChanged
        HiddenField_ChangeID.Value = 0
        ddlLocation.Items.Clear()
        ddlPosition.Items.Clear()

        Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
        If clsprojectchange.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtDate.Text & "') order by ID DESC") Then
            Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
            cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & clsprojectchange.ID)

            Dim clsPositions As New Clshrs_Positions(Me)
            clsPositions.GetDropDownListForProjects(ddlPosition, True, "B.LocationID = " & ddlLocation.SelectedValue)

            HiddenField_ChangeID.Value = clsprojectchange.ID
        End If
        SetData()
    End Sub
    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlLocation.SelectedIndexChanged
        Dim clsPositions As New Clshrs_Positions(Me)
        clsPositions.GetDropDownListForProjects(ddlPosition, True, "B.LocationID = " & ddlLocation.SelectedValue)
        SetData()
    End Sub
    Protected Sub txtDate_ValueChange(sender As Object, e As Infragistics.WebUI.WebDataInput.ValueChangeEventArgs) Handles txtDate.ValueChange
        Try
            HiddenField_ChangeID.Value = 0
            ddlLocation.Items.Clear()
            ddlPosition.Items.Clear()

            Dim ClsProjects As New Clshrs_Projects(Me, "hrs_Projects")
            ClsProjects.GetDropDownList(ddlProject, True, "IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and convert(Datetime,'" & txtDate.Text & "') between StartDate and EndDate")

            Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
            If clsprojectchange.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtDate.Text & "') order by ID DESC") Then
                Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
                cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & clsprojectchange.ID)

                Dim clsPositions As New Clshrs_Positions(Me)
                clsPositions.GetDropDownListForProjects(ddlPosition, True, "B.LocationID = " & ddlLocation.SelectedValue)

                HiddenField_ChangeID.Value = clsprojectchange.ID
            End If
            SetData()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlPosition_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPosition.SelectedIndexChanged
        SetData()
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        SetData()
    End Sub

    Protected Sub ImageButtonRefund_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonRefund.Click
        Dim hrsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(hrsProjectLocationDetails.ConnectionString)
        If hrsProjectLocationDetails.Find("ID = " & ddlPosition.SelectedValue) Then
            If hrsProjectLocationDetails.IsAlternative = False Then
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & ")")
                Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID)
                For Each PlaceDatarow As DataRow In hrsProjectPlacements.DataSet.Tables(0).Rows
                    Dim cinday As Integer = 0
                    For indys As Integer = 1 To 7
                        cinday = cinday + 1
                        Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                        hrsProjectPlacementPlanning.DayID = cinday
                        hrsProjectPlacementPlanning.PlacementID = PlaceDatarow("ID")
                        hrsProjectPlacementPlanning.Save()
                    Next indys
                Next

                Dim strarrDayoff As String = "0"
                strarrDayoff = hrsProjectLocationDetails.Remarks

                Dim strAr As String() = strarrDayoff.Split(",")

                Dim hrsProjectLocationShifts As New Clshrs_ProjectLocationShifts(Me)
                If hrsProjectLocationShifts.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and Qty > 0") Then
                    For Each Datar As DataRow In hrsProjectLocationShifts.DataSet.Tables(0).Rows
                        For i As Integer = 1 To Datar("Qty")
                            hrsProjectPlacements = New Clshrs_ProjectPlacements(Me)
                            If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                                For dy As Integer = 1 To 7
                                    Select Case dy
                                        Case 1
                                            Dim srl As Integer = 0
                                            For aridx As Integer = 0 To strAr.Length - 1
                                                If strAr(srl) = 1 Then
                                                    GoTo L1
                                                End If
                                                srl = srl + 1
                                            Next aridx
                                        Case 2
                                            Dim srl As Integer = 0
                                            For aridx As Integer = 0 To strAr.Length - 1
                                                If strAr(srl) = 2 Then
                                                    GoTo L1
                                                End If
                                                srl = srl + 1
                                            Next aridx
                                        Case 3
                                            Dim srl As Integer = 0
                                            For aridx As Integer = 0 To strAr.Length - 1
                                                If strAr(srl) = 3 Then
                                                    GoTo L1
                                                End If
                                                srl = srl + 1
                                            Next aridx
                                        Case 4
                                            Dim srl As Integer = 0
                                            For aridx As Integer = 0 To strAr.Length - 1
                                                If strAr(srl) = 4 Then
                                                    GoTo L1
                                                End If
                                                srl = srl + 1
                                            Next aridx
                                        Case 5
                                            Dim srl As Integer = 0
                                            For aridx As Integer = 0 To strAr.Length - 1
                                                If strAr(srl) = 5 Then
                                                    GoTo L1
                                                End If
                                                srl = srl + 1
                                            Next aridx
                                        Case 6
                                            Dim srl As Integer = 0
                                            For aridx As Integer = 0 To strAr.Length - 1
                                                If strAr(srl) = 6 Then
                                                    GoTo L1
                                                End If
                                                srl = srl + 1
                                            Next aridx
                                        Case 7
                                            Dim srl As Integer = 0
                                            For aridx As Integer = 0 To strAr.Length - 1
                                                If strAr(srl) = 7 Then
                                                    GoTo L1
                                                End If
                                                srl = srl + 1
                                            Next aridx
                                    End Select
                                    Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                    hrsProjectPlacementPlanning.Find("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & dy)
                                    hrsProjectPlacementPlanning.AttendanceTableShiftID = Datar("AttendanceTableShiftID")
                                    hrsProjectPlacementPlanning.Update("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & dy)
L1:
                                Next
                            End If
                        Next
                    Next
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & ")")
                End If
            ElseIf hrsProjectLocationDetails.IsAlternative = True And hrsProjectLocationDetails.IsInvoiced = True Then
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & ")")
                Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID)
                For Each PlaceDatarow As DataRow In hrsProjectPlacements.DataSet.Tables(0).Rows
                    Dim cinday As Integer = 0
                    For indys As Integer = 1 To 7
                        cinday = cinday + 1
                        Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                        hrsProjectPlacementPlanning.DayID = cinday
                        hrsProjectPlacementPlanning.PlacementID = PlaceDatarow("ID")
                        hrsProjectPlacementPlanning.Save()
                    Next indys
                Next

                Dim hrsProjectLocationShifts As New Clshrs_ProjectLocationShifts(Me)
                If hrsProjectLocationShifts.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and Qty > 0") Then
                    Dim seq As Integer = 1
                    Dim serial As Integer = 0
                    For Each Datar As DataRow In hrsProjectLocationShifts.DataSet.Tables(0).Rows
                        For i As Integer = 1 To Datar("Qty")
                            serial = serial + 1
                            seq = serial - (Math.Floor(Convert.ToDecimal(serial) / Convert.ToDecimal(7)) * 7)
                            seq = IIf(seq = 0, 7, seq)
                            If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                                For dy As Integer = 1 To hrsProjectLocationDetails.WeekDays
                                    Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                    hrsProjectPlacementPlanning.Find("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & seq)
                                    hrsProjectPlacementPlanning.AttendanceTableShiftID = Datar("AttendanceTableShiftID")
                                    hrsProjectPlacementPlanning.Update("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & seq)
                                    seq = seq + 1
                                    If seq > 7 Then seq = 1
                                Next
                            End If
                        Next i
                    Next

                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "update hrs_ProjectPlacements set RegComputerID = null where LocationDetailID = " & hrsProjectLocationDetails.ID & "; update hrs_ProjectPlacements set RegComputerID = 1 where LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)")
                    If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                        Dim strcommand As String = "select ID,PlacementID,AttendanceTableShiftID,DayID,(select MAX(B.AttendanceTableShiftID) from hrs_ProjectPlacementPlanning B where B.PlacementID = hrs_ProjectPlacementPlanning.PlacementID) AS RelShift from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & " and isnull(RegComputerID,0)= 0)"
                        Dim DTRest As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectPlacements.ConnectionString, Data.CommandType.Text, strcommand).Tables(0)

                        Dim totalcount As Integer = hrsProjectLocationDetails.WeekDays * DTRest.Rows.Count
                        Dim Fullcount As Integer = 7 * DTRest.Rows.Count
                        Dim GabEmployees As Integer = Math.Floor((Convert.ToDecimal(Fullcount) - Convert.ToDecimal(totalcount)) / Convert.ToDecimal(hrsProjectLocationDetails.WeekDays))
                        Dim altrcount = GabEmployees
                        Dim diminsions As Integer = 0
                        If altrcount > 0 Then
                            diminsions = Math.Ceiling(Convert.ToDecimal(DTRest.Rows.Count) / Convert.ToDecimal(altrcount))
                        End If

                        Dim array As Integer = 0

                        Dim plcDT As DataTable = hrsProjectPlacements.DataSet.Tables(0)
                        For i As Integer = 0 To plcDT.Rows.Count
                            If array + 1 > altrcount Then
                                Continue For
                            End If
                            Dim selected As Integer = array
                            For cnt1 As Integer = 1 To IIf(diminsions > hrsProjectLocationDetails.WeekDays, hrsProjectLocationDetails.WeekDays, diminsions)
                                Try
                                    Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                    hrsProjectPlacementPlanning.Find("PlacementID = " & plcDT.Rows(i)("ID") & " and DayID = " & DTRest.Rows(selected)("DayID"))
                                    hrsProjectPlacementPlanning.AttendanceTableShiftID = DTRest.Rows(selected)("RelShift")
                                    hrsProjectPlacementPlanning.ReferenceTo = DTRest.Rows(selected)("ID")
                                    hrsProjectPlacementPlanning.Update("PlacementID = " & plcDT.Rows(i)("ID") & " and DayID = " & DTRest.Rows(selected)("DayID"))
                                    selected = selected + altrcount
                                Catch ex As Exception
                                End Try
                            Next
                            array = array + 1
                        Next
                    End If
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and ID in (select ReferenceTo from hrs_ProjectPlacementPlanning); delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where isnull(RegComputerID,0) = 1); update hrs_ProjectPlacementPlanning set ReferenceTo = 1 where isnull(ReferenceTo,0) > 0 ;")
                End If
            ElseIf hrsProjectLocationDetails.IsAlternative = True And hrsProjectLocationDetails.IsInvoiced = False Then
                Dim totalcount As Integer = hrsProjectLocationDetails.WeekDays * hrsProjectLocationDetails.Qty
                Dim Fullcount As Integer = 7 * hrsProjectLocationDetails.Qty
                Dim GabEmployees As Integer = Math.Floor((Convert.ToDecimal(Fullcount) - Convert.ToDecimal(totalcount)) / Convert.ToDecimal(hrsProjectLocationDetails.WeekDays))
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & ")")
                Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID)
                If hrsProjectPlacements.DataSet.Tables(0).Rows.Count <> (hrsProjectLocationDetails.Qty + GabEmployees) Then
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacements where RegComputerID = 1 and LocationDetailID = " & hrsProjectLocationDetails.ID)
                    Dim listarray As Integer = 1
                    Dim CPlacementArr As Integer = hrsProjectPlacements.DataSet.Tables(0).Rows.Count
                    For ins As Integer = 1 To GabEmployees
                        CPlacementArr = CPlacementArr + 1
                        hrsProjectPlacements = New Clshrs_ProjectPlacements(Me)
                        hrsProjectPlacements.PlacementCode = "AltLoc:" & hrsProjectLocationDetails.LocationID.ToString() & "-PlaceNo:" & CPlacementArr.ToString()
                        hrsProjectPlacements.LocationID = hrsProjectLocationDetails.LocationID.ToString()
                        hrsProjectPlacements.LocationDetailID = hrsProjectLocationDetails.ID
                        hrsProjectPlacements.ProjectID = Request.QueryString.Item("ProjID")
                        hrsProjectPlacements.ProjectChangeID = HiddenField_ChangeID.Value
                        hrsProjectPlacements.RegComputerID = 1
                        hrsProjectPlacements.Save()
                        listarray = listarray + 1
                    Next ins
                End If

                hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID)
                For Each PlaceDatarow As DataRow In hrsProjectPlacements.DataSet.Tables(0).Rows
                    Dim cinday As Integer = 0
                    For indys As Integer = 1 To 7
                        cinday = cinday + 1
                        Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                        hrsProjectPlacementPlanning.DayID = cinday
                        hrsProjectPlacementPlanning.PlacementID = PlaceDatarow("ID")
                        hrsProjectPlacementPlanning.Save()
                    Next indys
                Next

                Dim hrsProjectLocationShifts As New Clshrs_ProjectLocationShifts(Me)
                If hrsProjectLocationShifts.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and Qty > 0") Then
                    Dim seq As Integer = 1
                    Dim serial As Integer = 0
                    For Each Datar As DataRow In hrsProjectLocationShifts.DataSet.Tables(0).Rows
                        For i As Integer = 1 To Datar("Qty")
                            serial = serial + 1
                            seq = serial - (Math.Floor(Convert.ToDecimal(serial) / Convert.ToDecimal(7)) * 7)
                            seq = IIf(seq = 0, 7, seq)
                            If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                                For dy As Integer = 1 To hrsProjectLocationDetails.WeekDays
                                    Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                    hrsProjectPlacementPlanning.Find("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & seq)
                                    hrsProjectPlacementPlanning.AttendanceTableShiftID = Datar("AttendanceTableShiftID")
                                    hrsProjectPlacementPlanning.Update("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & seq)
                                    seq = seq + 1
                                    If seq > 7 Then seq = 1
                                Next
                            End If
                        Next i
                    Next

                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "update hrs_ProjectPlacements set RegComputerID = null where LocationDetailID = " & hrsProjectLocationDetails.ID & "; update hrs_ProjectPlacements set RegComputerID = 1 where LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)")
                    If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                        Dim strcommand As String = "select ID,PlacementID,AttendanceTableShiftID,DayID,(select MAX(B.AttendanceTableShiftID) from hrs_ProjectPlacementPlanning B where B.PlacementID = hrs_ProjectPlacementPlanning.PlacementID) AS RelShift from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & " and isnull(RegComputerID,0)= 0) order by DayID,PlacementID"
                        Dim DTRest As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectPlacements.ConnectionString, Data.CommandType.Text, strcommand).Tables(0)

                        Dim altrcount = hrsProjectPlacements.DataSet.Tables(0).Rows.Count
                        Dim diminsions As Integer = Math.Ceiling(Convert.ToDecimal(DTRest.Rows.Count) / Convert.ToDecimal(altrcount))
                        Dim array As Integer = 0

                        For Each drplacements As DataRow In hrsProjectPlacements.DataSet.Tables(0).Rows
                            Dim selected As Integer = array
                            For cnt1 As Integer = 1 To IIf(diminsions > hrsProjectLocationDetails.WeekDays, hrsProjectLocationDetails.WeekDays, diminsions)
                                Try
                                    Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                    hrsProjectPlacementPlanning.Find("PlacementID = " & drplacements("ID") & " and DayID = " & DTRest.Rows(selected)("DayID"))
                                    hrsProjectPlacementPlanning.AttendanceTableShiftID = DTRest.Rows(selected)("RelShift")
                                    hrsProjectPlacementPlanning.ReferenceTo = DTRest.Rows(selected)("ID")
                                    hrsProjectPlacementPlanning.Update("PlacementID = " & drplacements("ID") & " and DayID = " & DTRest.Rows(selected)("DayID"))
                                    selected = selected + altrcount
                                Catch ex As Exception

                                End Try
                            Next
                            array = array + 1
                        Next
                    End If
                End If
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and ID in (select ReferenceTo from hrs_ProjectPlacementPlanning); delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where PlacementCode like '%Alt%'); update hrs_ProjectPlacementPlanning set ReferenceTo = 1 where isnull(ReferenceTo,0) > 0 ;")
            End If
            SetData()
            Dim objNav As New Venus.Shared.Web.NavigationHandler(hrsProjectLocationDetails.ConnectionString)
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Update Done!/تم التحديث"))
        End If
    End Sub
End Class
