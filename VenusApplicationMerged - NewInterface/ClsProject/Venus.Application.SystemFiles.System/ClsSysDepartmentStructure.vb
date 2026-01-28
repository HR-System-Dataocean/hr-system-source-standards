Public Class ClsSysDepartmentStructure
    Inherits ClsDataAcessLayer

    Public Sub New(ByVal MyPage As Global.System.Web.UI.Page)
        MyBase.new(MyPage)
    End Sub

    'Modification :  [0256] 5-12-2007 Add SetLanguage Function to Switch Between EngName , ArbName Fields 
    '             :                  According to Page Language 

    Public Function CreateDepartments(ByRef Tree As Infragistics.WebUI.UltraWebNavigator.UltraWebTree) As Boolean
        Dim ObjDepartments As New DataSet
        Dim ObjDr As DataRow
        Dim ObjDepartmentNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            ObjDepartments = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, "    Select * from sys_Departments          Where IsNull(CancelDate,'')='' And IsNull(ParentID,'')=''")
            Tree.WebTreeTarget = Infragistics.WebUI.UltraWebNavigator.WebTreeTarget.ClassicTree
            If mDataHandler.CheckValidDataObject(ObjDepartments) Then
                Tree.Nodes.Clear()
                For Each ObjDr In ObjDepartments.Tables(0).Rows
                    ObjDepartmentNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    'ObjDepartmentNode.Text = ObjDr("EngName")
                    ObjDepartmentNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName"))
                    If (ObjDepartmentNode.Text.Trim = String.Empty) Then
                        ObjDepartmentNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName"))
                    End If
                    ObjDepartmentNode.Tag = ObjDr("ID")
                    ObjDepartmentNode.ImageUrl = "~/Pages/HR/Img/icon_messenger0.gif"
                    CheckSubDepartment(Me.MainCompanyID, ObjDr("ID"), ObjDepartmentNode)
                    Tree.Nodes.Add(ObjDepartmentNode)
                Next
            End If

        Catch ex As Exception

        End Try
    End Function


    'Modification :  [0256] 5-12-2007 Add SetLanguage Function to Switch Between EngName , ArbName Fields 
    '             :                  According to Page Language 

    Private Function CheckSubDepartment(ByVal CompanyID As Integer, ByVal DepartmentID As Integer, ByVal Node As Infragistics.WebUI.UltraWebNavigator.Node) As Boolean
        Dim ObjDepartments As New DataSet
        Dim ObjDr As DataRow
        Dim ObjDepartmentNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            ObjDepartments = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, " Select * from sys_Departments       Where IsNull(CancelDate,'')='' And ParentID=" & DepartmentID & "      And CompanyID=" & CompanyID)

            If mDataHandler.CheckValidDataObject(ObjDepartments) Then
                For Each ObjDr In ObjDepartments.Tables(0).Rows
                    ObjDepartmentNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    'ObjDepartmentNode.Text = ObjDr("EngName")
                    ObjDepartmentNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName"))
                    If (ObjDepartmentNode.Text.Trim = String.Empty) Then
                        ObjDepartmentNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName"))
                    End If
                    ObjDepartmentNode.Tag = ObjDr("ID")
                    ObjDepartmentNode.ImageUrl = "~/Pages/HR/Img/forums.gif"
                    CheckSubDepartment(CompanyID, ObjDr("ID"), ObjDepartmentNode)
                    Node.Nodes.Add(ObjDepartmentNode)
                Next
            End If

        Catch ex As Exception

        End Try
    End Function

End Class
