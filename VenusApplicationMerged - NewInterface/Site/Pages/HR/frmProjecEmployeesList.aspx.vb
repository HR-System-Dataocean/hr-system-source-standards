Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmProjecEmployeesList
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                If clsProjects.Find("ID = " & Request.QueryString.Item("ProjID")) Then
                    Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsProjects.ConnectionString)
                    lblProjectCode.Text = clsProjects.Code
                    lblProjectName.Text = IIf(ClsNavHandler.SetLanguage(Me, "0/1") = 0, clsProjects.EngName, clsProjects.ArbName)
                    Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
                    If clsprojectchange.Find("ProjectID = " & Request.QueryString.Item("ProjID") & " and RegComputerID = 1 and FromDate <= getdate() order by ID DESC") Then
                        Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
                        cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & clsprojectchange.ID & " and MainLocationID is null")
                        cls_ProjectLocations.GetDropDownList(ddlSubLocation, True, "ProjectChangeID = " & clsprojectchange.ID & " and MainLocationID = " & ddlLocation.SelectedValue)

                        uwgLocationPositions.Rows.Clear()
                        GetData()
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetData() As Boolean
        Try
            uwgLocationPositions.Rows.Clear()
            Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
            If clsprojectchange.Find("ProjectID = " & Request.QueryString.Item("ProjID") & " and RegComputerID = 1 and FromDate <= getdate() order by ID DESC") Then
                Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                Dim strcommand As String = "select A.ID,A.PlacementCode,(select ArbName from hrs_Positions where ID = B.PositionID) AS Position,B.InternalAmt,B.WeekDays,isnull((select top 1 (select dbo.fn_GetEmpName((select Code from hrs_employees where ID = EmployeeID), 1)) from hrs_ProjectPlacementEmployees where PlacementCode = A.PlacementCode and (ToDate is null or ToDate > getdate()) order by ToDate Desc),'') AS CEmployee" & _
                                           " from hrs_ProjectPlacements A inner join hrs_ProjectLocationDetails B on A.LocationDetailID = B.ID" & _
                                           " where A.ID in (select PlacementID from hrs_ProjectPlacementPlanning where isnull(ReferenceTo,0) in (0,1)) and A.ProjectChangeID = " & clsprojectchange.ID
                Dim Locfilter As String = ""
                If ddlLocation.SelectedValue > 0 Then
                    Locfilter = " and A.LocationID = " & ddlLocation.SelectedValue
                End If
                If ddlSubLocation.SelectedValue > 0 Then
                    Locfilter = " and A.LocationID = " & ddlSubLocation.SelectedValue
                End If

                uwgLocationPositions.DataSource = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsProjects.ConnectionString, System.Data.CommandType.Text, strcommand & Locfilter & " Order by A.ID")
                uwgLocationPositions.DataBind()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlLocation.SelectedIndexChanged
        Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
        If clsprojectchange.Find("ProjectID = " & Request.QueryString.Item("ProjID") & " and RegComputerID = 1 and FromDate <= getdate() order by ID DESC") Then
            Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
            Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(cls_ProjectLocations.ConnectionString)
            cls_ProjectLocations.GetDropDownList(ddlSubLocation, True, "ProjectChangeID = " & clsprojectchange.ID & " and MainLocationID = " & ddlLocation.SelectedValue)

            GetData()
        End If
    End Sub

    Protected Sub ddlSubLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSubLocation.SelectedIndexChanged
        GetData()
    End Sub
End Class
