Public Class ClsPositionsStructure
    Inherits ClsDataAcessLayer

    Public Sub New(ByVal MyPage As Global.System.Web.UI.Page)
        MyBase.new(MyPage)
    End Sub
    'Modification :  [0256] 5-12-2007 Add SetLanguage Function to Switch Between EngName , ArbName Fields 
    '             :                  According to Page Language 
    Public Function CreatePositions(ByRef Tree As Infragistics.WebUI.UltraWebNavigator.UltraWebTree) As Boolean
        Dim ObjPositions As New DataSet
        Dim ObjDr As DataRow
        Dim ObjPositionNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            Tree.WebTreeTarget = Infragistics.WebUI.UltraWebNavigator.WebTreeTarget.ClassicTree
            ObjPositions = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, "    Select * from hrs_Positions          Where IsNull(CancelDate,'')='' And IsNull(ParentID,'')=''")

            If mDataHandler.CheckValidDataObject(ObjPositions) Then
                Tree.Nodes.Clear()
                For Each ObjDr In ObjPositions.Tables(0).Rows
                    ObjPositionNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    'ObjPositionNode.Text = ObjDr("EngName")
                    ObjPositionNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName"))
                    If (ObjPositionNode.Text.Trim = "") Then
                        ObjPositionNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName"))
                    End If
                    ObjPositionNode.Tag = ObjDr("ID")
                    ObjPositionNode.ImageUrl = "~/Pages/HR/Img/icon_messenger0.gif"
                    CheckSubPostion(ObjDr("ID"), ObjPositionNode)
                    Tree.Nodes.Add(ObjPositionNode)
                Next
            End If

        Catch ex As Exception

        End Try
    End Function

    'Modification :  [0256] 5-12-2007 Add SetLanguage Function to Switch Between EngName , ArbName Fields 
    '             :                  According to Page Language 
    Private Function CheckSubPostion(ByVal PositionsID As Integer, ByRef Node As Infragistics.WebUI.UltraWebNavigator.Node) As Boolean
        Dim ObjPostions As New DataSet
        Dim ObjLocations As New DataSet
        Dim ObjDr As DataRow
        Dim ObjPostionsNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            ObjPostions = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, "    Select * from hrs_Positions          Where IsNull(CancelDate,'')='' And ParentID=" & PositionsID)
            If mDataHandler.CheckValidDataObject(ObjPostions) Then
                For Each ObjDr In ObjPostions.Tables(0).Rows
                    ObjPostionsNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    'ObjPostionsNode.Text = ObjDr("EngName")
                    ObjPostionsNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName"))
                    If (ObjPostionsNode.Text.Trim = "") Then
                        ObjPostionsNode.Text = ObjDr(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName"))
                    End If
                    ObjPostionsNode.Tag = ObjDr("ID")
                    ObjPostionsNode.ImageUrl = "~/Pages/HR/Img/forums.gif"
                    CheckSubPostion(ObjDr("ID"), ObjPostionsNode)
                    Node.Nodes.Add(ObjPostionsNode)
                Next
            End If
        Catch ex As Exception

        End Try
    End Function
End Class
