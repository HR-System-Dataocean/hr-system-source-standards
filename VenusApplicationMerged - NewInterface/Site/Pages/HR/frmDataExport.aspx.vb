Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Drawing

Partial Class frmDataExport
    Inherits MainPage

#Region "Protected Sub"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim clsuser As New Clssys_Users(Me.Page)

        Dim clsnationality As New Clssys_Nationality(Me.Page)
        Dim clsbranch As New Clssys_Branches(Me.Page)
        Dim clsdepartments As New Clssys_Departments(Me.Page)
        Dim clslocationr As New Clssys_Locations(Me.Page)
        Dim clseducations As New Clshrs_Educations(Me.Page, "hrs_Educations")
        Dim clsClss As New Clshrs_EmployeeClasses(Me.Page)

        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsObjects.ConnectionString)
        If ClsNavigationHandler.SetLanguage(Page, "1/0") = "0" Then
            Dim mLeft As New System.Web.UI.WebControls.Unit(96, UnitType.Percentage)
            UltraWebTree1.Padding.Left = mLeft
        End If

        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
        Dim clsSponsor As New Clshrs_Sponsors(Page)
        If ClsObjects.Find(" Code='" & clsSponsor.Table.Trim & "'") Then
            If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TextBox_Sponsor.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & TextBox_Sponsor.ClientID & "'"
                WebImageButton_Sponsor.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
        End If
        Dim clsctype As New Clshrs_ContractTypes(Page)
        If ClsObjects.Find(" Code='" & clsctype.Table.Trim & "'") Then
            If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TextBox_Ctype.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & TextBox_Ctype.ClientID & "'"
                WebImageButton1.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
        End If

        If Not IsPostBack Then
            ClsObjects.GetDropDownList(DropDownList_Object, True, "ID in (344,414,373,281,185,276,269,264,392,401,396,336,437,14,310,329,121,194,81,515,1909,89)")
            clsuser.GetDropDownList(DropDownList_User, True, "")

            clsnationality.GetDropDownList(DropDownList_Nationality, True, "")
            clsbranch.GetDropDownList(ddlBranche, True, "")
            ddlBranche.SelectedIndex = 1
            clsdepartments.GetDropDownList(ddlDepartment, True, "")
            clsClss.GetDropDownList(ddlClass, True, "")
            clslocationr.GetDropDownList(DropDownList_Location, True, "")
            clseducations.GetDropDownList(DropDownList_Education, True, "")

            lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
            Page.Session.Add("Lage", lblLage.Text)
            Page.Session.Add("ConnectionString", ClsObjects.ConnectionString)

            Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
           
            clsProjects.GetDropDownList(DropDownList_Project, True, "IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null ")
            DropDownList_Project.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[select Project]/ [ أختار المشروع]")


            UltraWebTree1.Nodes.Clear()
            Dim clsenum As New Clshrs_Enum(Me.Page)
            If clsenum.Find("Flag = 2 order by ID") Then
                Dim ObjTreeNode As New Infragistics.WebUI.UltraWebNavigator.Node
                ObjTreeNode.Text = ClsNavigationHandler.SetLanguage(Page, "Selected Columns/أعمدة الإختيار")
                ObjTreeNode.Tag = "0"
                For Each row As Data.DataRow In clsenum.DataSet.Tables(0).Rows
                    Dim ObjTreeNodeSub As New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjTreeNodeSub.Text = row(ClsNavigationHandler.SetLanguage(Page, "EngName/ArbName"))
                    ObjTreeNodeSub.Tag = row("Code")
                    ObjTreeNode.Nodes.Add(ObjTreeNodeSub)
                Next
                UltraWebTree1.Nodes.Add(ObjTreeNode)
                UltraWebTree1.ExpandAll()
            End If
        End If
    End Sub
    Protected Sub Button_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Prepare.Command, ImageButton_Prepare.Command
        Try
            Select Case DirectCast(e, System.Web.UI.WebControls.CommandEventArgs).CommandArgument
                Case "Prepare"
                    Dim dt As New DataTable()
                    If LoadData(dt) Then
                        If UltraWebTab1.SelectedTab = 0 Then
                            For Each ObjTreeNode As Infragistics.WebUI.UltraWebNavigator.Node In UltraWebTree1.Nodes
                                For Each ObjTreeNodeSub As Infragistics.WebUI.UltraWebNavigator.Node In ObjTreeNode.Nodes
                                    If Not ObjTreeNodeSub.Checked Then
                                        dt.Columns.Remove(ObjTreeNodeSub.Tag)
                                    Else
                                        Dim Idx As Integer = dt.Columns(ObjTreeNodeSub.Tag).Ordinal
                                        dt.Columns(Idx).ColumnName = ObjTreeNodeSub.Text
                                    End If
                                Next
                            Next
                        End If
                        If dt.Columns.Count > 0 Then
                            Dim dgGrid As New GridView()
                            dgGrid.DataSource = dt
                            dgGrid.DataBind()
                            Response.ClearContent()
                            Response.AddHeader("content-disposition", "attachment;filename=Data" & DateTime.Now.Ticks.ToString() & ".xls")
                            Response.ContentType = "application/vnd.ms-excel"
                            Response.ContentEncoding = System.Text.Encoding.Unicode
                            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())
                            Dim tw As New System.IO.StringWriter()
                            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
                            dgGrid.RenderControl(hw)
                            Response.Write(tw.ToString())
                            Response.End()
                        End If
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
    End Sub
    Private Function LoadData(ByRef dt As DataTable) As Boolean
        Try
            If String.IsNullOrEmpty(txtFromSalary.Text.Trim()) Or String.IsNullOrEmpty(txtToSalary.Text.Trim()) Then
                txtFromSalary.Text = 0
                txtToSalary.Text = 0
            End If

            Dim clsenum As New Clshrs_Enum(Me.Page)
            If UltraWebTab1.SelectedTab = 0 Then
                If Convert.ToString(WebMaskEdit_FromDate.Value).Trim <> "" And Convert.ToString(WebMaskEdit_ToDate.Value).Trim <> "" Then
                    dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsenum.ConnectionString, "SP_AllEmployeesInfo", DropDownList_Nationality.SelectedValue, ddlBranche.SelectedValue, ddlDepartment.SelectedValue, DropDownList_Location.SelectedValue, TextBox_Sponsor.Text, DropDownList_Education.SelectedValue, TextBox_Ctype.Text, ddlClass.SelectedValue, IIf(CheckBox1.Checked, 1, 0), clsenum.SetHigriDate(WebMaskEdit_FromDate.Text), clsenum.SetHigriDate(WebMaskEdit_ToDate.Text), DropDownList_Project.SelectedValue, Convert.ToInt32(txtFromSalary.Text.Trim()), Convert.ToInt32(txtToSalary.Text.Trim())).Tables(0)
                Else
                    dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsenum.ConnectionString, "SP_AllEmployeesInfo", DropDownList_Nationality.SelectedValue, ddlBranche.SelectedValue, ddlDepartment.SelectedValue, DropDownList_Location.SelectedValue, TextBox_Sponsor.Text, DropDownList_Education.SelectedValue, TextBox_Ctype.Text, ddlClass.SelectedValue, IIf(CheckBox1.Checked, 1, 0), DateTime.Now.AddYears(-99), DateTime.Now.AddYears(99), DropDownList_Project.SelectedValue, Convert.ToInt32(txtFromSalary.Text.Trim()), Convert.ToInt32(txtToSalary.Text.Trim())).Tables(0)
                End If
                If dt.Rows.Count > 0 Then
                    Return True
                End If
            Else
                If Convert.ToString(WebMaskEdit_LogFrom.Value).Trim <> "" And Convert.ToString(WebMaskEdit_LogTo.Value).Trim <> "" Then
                    dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsenum.ConnectionString, "SP_AllUsersLog", DropDownList_User.SelectedValue, DropDownList_Object.SelectedValue, clsenum.SetHigriDate(WebMaskEdit_LogFrom.Text), clsenum.SetHigriDate(WebMaskEdit_LogTo.Text)).Tables(0)
                Else
                    dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsenum.ConnectionString, "SP_AllUsersLog", DropDownList_User.SelectedValue, DropDownList_Object.SelectedValue, DateTime.Now.AddYears(-20), DateTime.Now.AddYears(10)).Tables(0)
                End If
                If dt.Rows.Count > 0 Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            Response.Write(ex)
            Return False
        End Try
    End Function

#End Region

End Class
