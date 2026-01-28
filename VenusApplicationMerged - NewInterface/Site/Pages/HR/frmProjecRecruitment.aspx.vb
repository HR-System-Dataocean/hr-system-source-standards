Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmProjecRecruitment
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
                If clsprojectchange.Find("ID = " & Request.QueryString.Item("ProjChangeID")) Then
                    Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                    clsProjects.Find(" ID in (select ProjectID from hrs_ProjectChanges where ID = " & Request.QueryString.Item("ProjChangeID") & ")")
                    Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsProjects.ConnectionString)
                    lblProjectCode.Text = clsProjects.Code
                    lblProjectName.Text = IIf(ClsNavHandler.SetLanguage(Me, "0/1") = 0, clsProjects.EngName, clsProjects.ArbName)

                    Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
                    cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & Request.QueryString.Item("ProjChangeID") & " and MainLocationID is null")
                    cls_ProjectLocations.GetDropDownList(ddlSubLocation, True, "ProjectChangeID = " & Request.QueryString.Item("ProjChangeID") & " and MainLocationID = " & ddlLocation.SelectedValue)

                    uwgLocationPositions.Rows.Clear()
                    GetData()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetData() As Boolean
        Try
            uwgLocationPositions.Rows.Clear()
            Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
            Dim strcommand As String = "select A.ID,A.PlacementCode,A.LocationDetailID,(select ArbName from hrs_Positions where ID = B.PositionID) AS Position,B.InternalAmt,B.WeekDays,CONVERT(nvarchar(10), isnull((select top 1 ToDate from hrs_ProjectPlacementEmployees where PlacementCode = A.PlacementCode order by ToDate Desc),(select FromDate from hrs_ProjectChanges where ID = A.ProjectChangeID)),103) as LastDate," & _
                                       "case when (select COUNT(distinct AttendanceTableShiftID) from  hrs_ProjectPlacementPlanning where hrs_ProjectPlacementPlanning.PlacementID = A.ID group by PlacementID) > 1 then '' ELSE (select top 1 TimeIn from hrs_AttendanceTableShifts where ID in (select AttendanceTableShiftID from hrs_ProjectPlacementPlanning where hrs_ProjectPlacementPlanning.PlacementID = A.ID)) end AS TimeIn," & _
                                       "case when (select COUNT(distinct AttendanceTableShiftID) from  hrs_ProjectPlacementPlanning where hrs_ProjectPlacementPlanning.PlacementID = A.ID group by PlacementID) > 1 then '' ELSE (select top 1 TimeOut from hrs_AttendanceTableShifts where ID in (select AttendanceTableShiftID from hrs_ProjectPlacementPlanning where hrs_ProjectPlacementPlanning.PlacementID = A.ID)) end AS TimeOut " & _
                                       " from hrs_ProjectPlacements A inner join hrs_ProjectLocationDetails B on A.LocationDetailID = B.ID" & _
                                       " where A.ID in (select PlacementID from hrs_ProjectPlacementPlanning where isnull(ReferenceTo,0) in (0,1)) and A.ID in (" & Request.QueryString.Item("PlacementList") & ") and A.PlacementCode not in (select PlacementCode from hrs_ProjectPlacementEmployees where ToDate is null or ToDate >= (select FromDate from hrs_ProjectChanges where ID = A.ProjectChangeID))"
            Dim Locfilter As String = ""
            If ddlLocation.SelectedValue > 0 Then
                Locfilter = " and A.LocationID = " & ddlLocation.SelectedValue
            End If
            If ddlSubLocation.SelectedValue > 0 Then
                Locfilter = " and A.LocationID = " & ddlSubLocation.SelectedValue
            End If

            uwgLocationPositions.DataSource = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsProjects.ConnectionString, System.Data.CommandType.Text, strcommand & Locfilter)
            uwgLocationPositions.DataBind()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlLocation.SelectedIndexChanged
        Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(cls_ProjectLocations.ConnectionString)
        cls_ProjectLocations.GetDropDownList(ddlSubLocation, True, "ProjectChangeID = " & Request.QueryString.Item("ProjChangeID") & " and MainLocationID = " & ddlLocation.SelectedValue)

        GetData()
    End Sub

    Protected Sub ddlSubLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSubLocation.SelectedIndexChanged
        GetData()
    End Sub
End Class
