Public Class ClsSysCompanyStructure
    Inherits ClsDataAcessLayer

    Public Sub New(ByVal MyPage As Global.System.Web.UI.Page)
        MyBase.new(MyPage)
    End Sub

    Public Function ReadCompanyStructure(ByRef Tree As Infragistics.WebUI.UltraWebNavigator.UltraWebTree) As Boolean
        Dim StrSqlcommand As String = String.Empty
        Dim ObjCompanies As New DataSet
        Try
            Tree.Nodes.Clear()
            ObjCompanies = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, "   Select * from sys_Companies         where  ID=" & mPage.Session("CompanyID"))
            CreateCompany(Tree, ObjCompanies)

        Catch ex As Exception

        End Try
    End Function
    Private Function CreateCompany(ByRef ObjTree As Infragistics.WebUI.UltraWebNavigator.UltraWebTree, ByVal Ds As DataSet) As Boolean

        Dim ObjTreeNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjDr As DataRow
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            ObjTree.WebTreeTarget = Infragistics.WebUI.UltraWebNavigator.WebTreeTarget.ClassicTree
            If mDataHandler.CheckValidDataObject(Ds) Then
                For Each ObjDr In Ds.Tables(0).Rows
                    ObjTreeNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjTreeNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName"))
                    If (ObjTreeNode.Text.Trim = String.Empty) Then
                        ObjTreeNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName"))
                    End If
                    ObjTreeNode.Tag = ObjDr("ID")
                    ObjTreeNode.ImageUrl = "~/Pages/HR/Img/home.gif"
                    CreateBranches(ObjDr("ID"), ObjTreeNode)
                    ObjTree.Nodes.Add(ObjTreeNode)

                Next
            End If

            Return True
        Catch ex As Exception

        End Try
    End Function
    Private Function CreateBranches(ByVal CompanyID As Integer, ByRef Node As Infragistics.WebUI.UltraWebNavigator.Node) As Boolean
        Dim ObjBranches As New DataSet
        Dim ObjDepartments As New DataSet
        Dim ObjLocations As New DataSet
        Dim ObjDr As DataRow
        Dim ObjBranchesNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjDepartmentNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjLocationNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)

        Try
            ObjBranches = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, "    Select * from sys_Branches          Where IsNull(CancelDate,'')='' And IsNull(ParentID,'')=''        And CompanyID=" & CompanyID)
            If mDataHandler.CheckValidDataObject(ObjBranches) Then
                For Each ObjDr In ObjBranches.Tables(0).Rows
                    ObjBranchesNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjBranchesNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName"))
                    If (ObjBranchesNode.Text.Trim = String.Empty) Then
                        ObjBranchesNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName"))
                    End If
                    ObjBranchesNode.Tag = ObjDr("ID")
                    ObjBranchesNode.ImageUrl = "~/Pages/HR/Img/icon_mini_home.gif"
                    CheckSubBranches(CompanyID, ObjDr("ID"), ObjBranchesNode)
                    CreateSectores(CompanyID, ObjDr("ID"), ObjBranchesNode)
                    'Rabie 5-9-2024
                    'CreateDepartments(CompanyID, ObjDr("ID"), ObjBranchesNode)
                    Node.Nodes.Add(ObjBranchesNode)
                Next
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function CreateDepartments(ByVal Companyid As Integer, ByVal BranchID As Integer, ByRef Node As Infragistics.WebUI.UltraWebNavigator.Node) As Boolean
        Dim ObjDepartments As New DataSet
        Dim ObjDr As DataRow
        Dim ObjDepartmentNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            'ObjDepartments = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, " Select * from sys_Departments       Where IsNull(CancelDate,'')='' And ID in (select DepartmentID from sys_DepartmentsBranches where BranchID = " & BranchID & " and Checked = 1) And IsNull(ParentID,'')=''      And CompanyID=" & Companyid)
            ObjDepartments = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, " Select * from sys_Departments       Where IsNull(CancelDate,'')='' And ID in (select DepartmentID from sys_SectorsDepartments where SectorID = " & BranchID & " and Checked = 1) And IsNull(ParentID,'')=''      And CompanyID=" & Companyid)
            If mDataHandler.CheckValidDataObject(ObjDepartments) Then
                For Each ObjDr In ObjDepartments.Tables(0).Rows
                    ObjDepartmentNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjDepartmentNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName"))
                    If (ObjDepartmentNode.Text.Trim = String.Empty) Then
                        ObjDepartmentNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName"))
                    End If
                    ObjDepartmentNode.Tag = ObjDr("ID")
                    ObjDepartmentNode.ImageUrl = "~/Pages/HR/Img/forums.gif"
                    CheckSubDepartment(Companyid, ObjDr("ID"), ObjDepartmentNode)
                    CreateProjects(Companyid, ObjDr("ID"), ObjDepartmentNode)
                    Node.Nodes.Add(ObjDepartmentNode)
                Next
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function CreateSectores(ByVal Companyid As Integer, ByVal DepartmentID As Integer, ByRef Node As Infragistics.WebUI.UltraWebNavigator.Node) As Boolean
        Dim ObjSectors As New DataSet
        Dim ObjDr As DataRow
        Dim ObjSectortNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            'ObjSectors = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, " Select * from sys_Sectors       Where IsNull(CancelDate,'')='' And ID in (select SectorID from sys_SectorsDepartments where DepartmentID = " & DepartmentID & " and Checked = 1) And IsNull(ParentID,'')=''      And CompanyID=" & Companyid)
            ObjSectors = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, " Select * from sys_Sectors       Where IsNull(CancelDate,'')='' And ID in (select distinct(SectorID) from sys_SectorsDepartments join sys_DepartmentsBranches on sys_SectorsDepartments.DepartmentID= sys_DepartmentsBranches.DepartmentID) And IsNull(ParentID,'')=''      And CompanyID=" & Companyid)

            If mDataHandler.CheckValidDataObject(ObjSectors) Then
                For Each ObjDr In ObjSectors.Tables(0).Rows
                    ObjSectortNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjSectortNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName"))
                    If (ObjSectortNode.Text.Trim = String.Empty) Then
                        ObjSectortNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName"))
                    End If
                    ObjSectortNode.Tag = ObjDr("ID")
                    ObjSectortNode.ImageUrl = "~/Pages/HR/Img/forums.gif"
                    CheckSubSector(Companyid, ObjDr("ID"), ObjSectortNode)
                    CreateDepartments(Companyid, ObjDr("ID"), ObjSectortNode)

                    Node.Nodes.Add(ObjSectortNode)
                Next
            End If
        Catch ex As Exception
        End Try
    End Function

    Private Function CreateProjects(ByVal Companyid As Integer, ByVal DepartmentID As Integer, ByRef Node As Infragistics.WebUI.UltraWebNavigator.Node) As Boolean
        Dim ObjProjects As New DataSet
        Dim ObjDr As DataRow
        Dim ObjSectortNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            'ObjSectors = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, " Select * from sys_Sectors       Where IsNull(CancelDate,'')='' And ID in (select SectorID from sys_SectorsDepartments where DepartmentID = " & DepartmentID & " and Checked = 1) And IsNull(ParentID,'')=''      And CompanyID=" & Companyid)
            ObjProjects = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, " Select * from hrs_Projects       Where departmentid=" & DepartmentID)

            If mDataHandler.CheckValidDataObject(ObjProjects) Then
                For Each ObjDr In ObjProjects.Tables(0).Rows
                    ObjSectortNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjSectortNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName"))
                    If (ObjSectortNode.Text.Trim = String.Empty) Then
                        ObjSectortNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName"))
                    End If
                    ObjSectortNode.Tag = ObjDr("ID")
                    ObjSectortNode.ImageUrl = "~/Pages/HR/Img/forums.gif"
                    ' CheckSubSector(Companyid, ObjDr("ID"), ObjSectortNode)
                    'CreateDepartments(Companyid, ObjDr("ID"), ObjSectortNode)

                    Node.Nodes.Add(ObjSectortNode)
                Next
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function CreateLocations(ByVal Companyid As Integer, ByVal Departmentid As Integer, ByVal Node As Infragistics.WebUI.UltraWebNavigator.Node) As Boolean
        Dim ObjLocations As New DataSet
        Dim ObjDr As DataRow
        Dim ObjLocationNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            ObjLocations = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, "   Select * from sys_Locations         Where IsNull(CancelDate,'')='' And DepartmentID=" & Departmentid & " And CompanyID=" & Companyid)
            If mDataHandler.CheckValidDataObject(ObjLocations) Then
                For Each ObjDr In ObjLocations.Tables(0).Rows
                    ObjLocationNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjLocationNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName"))
                    If (ObjLocationNode.Text.Trim = String.Empty) Then
                        ObjLocationNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName"))
                    End If
                    ObjLocationNode.Tag = ObjDr("ID")
                    ObjLocationNode.ImageUrl = "~/Pages/HR/Img/samples_icon.gif"
                    Node.Nodes.Add(ObjLocationNode)
                Next
            End If
        Catch ex As Exception

        End Try
    End Function
    Private Function CheckSubBranches(ByVal Companyid As Integer, ByVal BranchID As Integer, ByRef Node As Infragistics.WebUI.UltraWebNavigator.Node) As Boolean
        Dim ObjBranches As New DataSet
        Dim ObjDepartments As New DataSet
        Dim ObjLocations As New DataSet
        Dim ObjDr As DataRow
        Dim ObjBranchesNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjDepartmentNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjLocationNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            ObjBranches = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, "    Select * from sys_Branches          Where IsNull(CancelDate,'')='' And ParentID=" & BranchID & "                                 And CompanyID=" & Companyid)
            If mDataHandler.CheckValidDataObject(ObjBranches) Then
                For Each ObjDr In ObjBranches.Tables(0).Rows
                    ObjBranchesNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjBranchesNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName"))
                    If (ObjBranchesNode.Text.Trim = String.Empty) Then
                        ObjBranchesNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName"))
                    End If
                    ObjBranchesNode.Tag = ObjDr("ID")
                    ObjBranchesNode.ImageUrl = "~/Pages/HR/Img/icon_mini_home.gif"
                    CheckSubBranches(Companyid, ObjDr("ID"), ObjBranchesNode)
                    Node.Nodes.Add(ObjBranchesNode)
                Next
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function CheckSubDepartment(ByVal CompanyID As Integer, ByVal DepartmentID As Integer, ByVal Node As Infragistics.WebUI.UltraWebNavigator.Node) As Boolean
        Dim ObjDepartments As New DataSet
        Dim ObjDr As DataRow
        Dim ObjDepartmentNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            Dim str As String
            str = " Select * from sys_Departments       Where IsNull(CancelDate,'')='' And ParentID=" & DepartmentID & "      And CompanyID=" & CompanyID
            ObjDepartments = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, str)
            If mDataHandler.CheckValidDataObject(ObjDepartments) Then
                For Each ObjDr In ObjDepartments.Tables(0).Rows
                    ObjDepartmentNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjDepartmentNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName"))
                    If (ObjDepartmentNode.Text.Trim = String.Empty) Then
                        ObjDepartmentNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName"))
                    End If
                    ObjDepartmentNode.Tag = ObjDr("ID")
                    ObjDepartmentNode.ImageUrl = "~/Pages/HR/Img/forums.gif"
                    'CheckSubDepartment(CompanyID, ObjDr("ID"), ObjDepartmentNode)
                    'CreateSectores(CompanyID, ObjDr("ID"), ObjDepartmentNode)
                    'Node.Nodes.Add(ObjDepartmentNode)
                Next
            End If
        Catch ex As Exception

        End Try
    End Function
    Private Function CheckSubSector(ByVal CompanyID As Integer, ByVal SectorID As Integer, ByVal Node As Infragistics.WebUI.UltraWebNavigator.Node) As Boolean
        Dim ObjSectors As New DataSet
        Dim ObjDr As DataRow
        Dim ObjSectortNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            ObjSectors = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, " Select * from sys_Sectors       Where IsNull(CancelDate,'')='' And ParentID=" & SectorID & "      And CompanyID=" & CompanyID)
            If mDataHandler.CheckValidDataObject(ObjSectors) Then
                For Each ObjDr In ObjSectors.Tables(0).Rows
                    ObjSectortNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjSectortNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName"))
                    If (ObjSectortNode.Text.Trim = String.Empty) Then
                        ObjSectortNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName"))
                    End If
                    ObjSectortNode.Tag = ObjDr("ID")
                    ObjSectortNode.ImageUrl = "~/Pages/HR/Img/forums.gif"
                    'CheckSubSector(CompanyID, ObjDr("ID"), ObjSectortNode)
                    Node.Nodes.Add(ObjSectortNode)
                Next
            End If
        Catch ex As Exception

        End Try
    End Function

End Class
