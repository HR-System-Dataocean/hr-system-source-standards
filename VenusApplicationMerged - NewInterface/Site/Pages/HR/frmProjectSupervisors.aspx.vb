Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmProjectSupervisors
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                txtDate.Value = DateTime.Now.ToString("ddMMyyyy")

                Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                Dim clsUsers As New Clssys_Users(Me)
                Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsProjects.ConnectionString)

                Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
                clsProjects.GetDropDownList(ddlProject, True, "IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and convert(Datetime,'" & txtDate.Text & "') between StartDate and EndDate")
                clsUsers.GetDropDownList(ddlSupervisor, True, "")
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetData(ByVal ProjectID As Integer, ByVal Supervisor As Integer) As Boolean
        Try
            Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
            If clsprojectchange.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtDate.Text & "') order by ID DESC") Then
                Dim ClsProjectLocationDtls As New Clshrs_ProjectLocationDetails(Me)
                Dim Str As String = "select hrs_ProjectLocations.ID,hrs_ProjectLocations.LocationDescription AS LocationDescription,hrs_ProjectLocations.LocationAddress AS LocationAddress,hrs_ProjectLocations.Required AS Required," & _
                                    " (select COUNT(hrs_ProjectLocations01.ID) from hrs_ProjectLocations AS hrs_ProjectLocations01 where hrs_ProjectLocations01.ID = hrs_ProjectLocations.ID and hrs_ProjectLocations01.Supervisor = " & Supervisor & ") AS UserStatus, " & _
                                    " (select COUNT(ID) from hrs_ProjectLocationUsers where hrs_ProjectLocationUsers.ProjectLocationID = hrs_ProjectLocations.ID and hrs_ProjectLocationUsers.UserID = " & Supervisor & ") AS EntryStatus " & _
                                    " from hrs_ProjectLocations where ProjectChangeID = " & clsprojectchange.ID
                Dim Ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsProjectLocationDtls.ConnectionString, System.Data.CommandType.Text, Str)
                uwgLocationPositions.DataSource = Ds.Tables(0)
                uwgLocationPositions.DataBind()
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Protected Sub btnPrint_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnPrint.Click
        Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click
        Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsProjects.ConnectionString)
        Try
            If ddlProject.SelectedValue > 0 And ddlSupervisor.SelectedValue > 0 Then
                Dim UpdCommand As String = ""
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgLocationPositions.Rows
                    If DGRow.Cells(4).Value = 1 Then
                        UpdCommand = UpdCommand & Environment.NewLine & " update hrs_ProjectLocations set Supervisor = " & ddlSupervisor.SelectedValue & " where ID = '" & DGRow.Cells(0).Value & "'"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsProjects.ConnectionString, Data.CommandType.Text, UpdCommand)
                    End If
                    Dim delString As String = "delete from hrs_ProjectLocationUsers where UserID =" & ddlSupervisor.SelectedValue & " and ProjectLocationID = " & DGRow.Cells(0).Value
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsProjects.ConnectionString, Data.CommandType.Text, delString)
                    If DGRow.Cells(5).Value = 1 Then
                        Dim InsString As String = "insert into hrs_ProjectLocationUsers (ProjectLocationID,UserID) values (" & DGRow.Cells(0).Value & "," & ddlSupervisor.SelectedValue & ")"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsProjects.ConnectionString, Data.CommandType.Text, InsString)
                    End If
                Next
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Save Complete Successfully/تم الحفظ بنجاح"))
                GetData(ddlProject.SelectedValue, ddlSupervisor.SelectedValue)
            End If
        Catch ex As Exception
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Save Failled/فشل الحفظ"))
        End Try
    End Sub
    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProject.SelectedIndexChanged
        Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        GetData(ddlProject.SelectedValue, ddlSupervisor.SelectedValue)
    End Sub
    Protected Sub ddlSubLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSupervisor.SelectedIndexChanged
        Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        GetData(ddlProject.SelectedValue, ddlSupervisor.SelectedValue)
    End Sub

    Protected Sub txtDate_ValueChange(sender As Object, e As Infragistics.WebUI.WebDataInput.ValueChangeEventArgs) Handles txtDate.ValueChange
        Try
            Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
            clsProjects.GetDropDownList(ddlProject, True, "IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and convert(Datetime,'" & txtDate.Text & "') between StartDate and EndDate")
            GetData(ddlProject.SelectedValue, ddlSupervisor.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub
End Class
