Imports Venus.Shared.DataHandler
Public Class NavigationHandler

#Region "Public Declarations"

    Private mObjErrorHandler As Venus.Shared.ErrorsHandler
    Private mObjDataHandler As New Venus.Shared.DataHandler
    Private mStrCurrentSql As String
    Private mstrConnectionString As String
    Private mObjClassError As String
    Private mBolHasError As Boolean

    Private mIntID As Integer
    Private mStrCode As String
    Private mStrEngName As String
    Private mStrArbName As String
    Private mIntParentID As Integer
    Private mIntRank As Integer
    Private mBolIsHide As Boolean
    Private mImgImage As System.Drawing.Image
    Private mIntViewType As Int16
    Private mStrShortcut As String

    Private mIntFormID As Integer
    Private mIntObjectID As Integer
    Private mIntViewFormID As Integer



#End Region

#Region "Public Property"

    Public Property ID() As Integer
        Get
            Return mIntID
        End Get
        Set(ByVal value As Integer)
            mIntID = value
        End Set
    End Property

    Public Property Code() As String
        Get
            Return mStrCode
        End Get
        Set(ByVal value As String)
            mStrCode = value
        End Set
    End Property

    Public Property EngName() As String
        Get
            Return mStrEngName
        End Get
        Set(ByVal value As String)
            mStrEngName = value
        End Set
    End Property

    Public Property ArbName() As String
        Get
            Return mStrArbName
        End Get
        Set(ByVal value As String)
            mStrArbName = value
        End Set
    End Property

    Public Property ParentID() As Integer
        Get
            Return mIntParentID
        End Get
        Set(ByVal value As Integer)
            mIntParentID = value
        End Set
    End Property

    Public Property Rank() As Integer
        Get
            Return mIntRank
        End Get
        Set(ByVal value As Integer)
            mIntRank = value
        End Set
    End Property

    Public Property FormID() As Integer
        Get
            Return mIntFormID
        End Get
        Set(ByVal value As Integer)
            mIntFormID = value
        End Set
    End Property

    Public Property IsHide() As Boolean
        Get
            Return mBolIsHide
        End Get
        Set(ByVal value As Boolean)
            mBolIsHide = value
        End Set
    End Property

    Public Property Image() As System.Drawing.Image
        Get
            Return mImgImage
        End Get
        Set(ByVal value As System.Drawing.Image)
            mImgImage = value
        End Set
    End Property

    Public Property ViewType() As Int16
        Get
            Return mIntViewType
        End Get
        Set(ByVal value As Int16)
            mIntViewType = value
        End Set
    End Property

    Public Property Shortcut() As String
        Get
            Return mStrShortcut
        End Get
        Set(ByVal value As String)
            mStrShortcut = value
        End Set
    End Property

    Public Property ObjectID() As Integer
        Get
            Return mIntObjectID
        End Get
        Set(ByVal value As Integer)
            mIntObjectID = value
        End Set
    End Property

    Public Property ViewFormID() As Integer
        Get
            Return mIntViewFormID
        End Get
        Set(ByVal value As Integer)
            mIntViewFormID = value
        End Set
    End Property


#End Region

#Region "Constant Definitions"

    Const CD_INTINITIALVALUE = 0
    Const CD_STRINITIALVALUE = ""
    Const CD_DBLINITIALVALUE = 0.0
    Const CD_PARENTID = "ParentID"
    Const CD_ENGNAME = "EngName"
    Const CD_ARBNAME = "ArbName"
    Const CD_IsHide = "IsHide"
    Const CD_ID = "ID"
    Const CD_SQLSTATMENTMENU1 = " Select * From Sys_Menus Where IsHide=0 "
    Const CD_SQLSTATMENTTREE1 = " Select * From Sys_Menus "
    Const CD_SQLSTATMENT2 = " Select * From sys_Menus Where ID= "
    Const CD_SQLSTATMENT3 = " Select * from sys_Menus "
    Const CD_SQLSTATMENT4 = " Select * from sys_Menus Where ID= "
    Const CD_SQLSTATMENT5 = " Set dateformat DMY Insert into sys_Menus(Code,EngName,ArbName,ParentID,Rank,FormID,IsHide,ViewType,Shortcut,ObjectID,ViewFormID)values(@Code,@EngName,@ArbName,@ParentID,@Rank,@FormID,@IsHide,@ViewType,@Shortcut,@ObjectID,@ViewFormID)"
    Const CD_SQLSTATMENT6 = " Set dateformat DMY Update sys_Menus set EngName=@EngName,ArbName=@ArbName,ParentID=@ParentID,Rank=@Rank,FormID=@FormID,IsHide=@IsHide,ViewType=@ViewType,Shortcut=@Shortcut,ObjectID=@ObjectID,ViewFormID=@ViewFormID where ID="
    Const CD_SQLSTATMENT7 = " Select * from sys_Forms Where isNull(CancelDate,'')=''"


#End Region

#Region "Public Constructor"

    Public Sub New(ByVal ConnectionString As String)
        Try
            mstrConnectionString = ConnectionString
            mObjErrorHandler = New Venus.Shared.ErrorsHandler(mstrConnectionString)
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Public Functions"

    Public Function LoadMenu(ByVal page As System.Web.UI.Page, ByVal Menu As Infragistics.WebUI.UltraWebNavigator.UltraWebMenu, ByVal UserID As Integer, ByVal GroupID As Integer) As Boolean
        Dim ObjDataRow As DataRow
        Dim ObjDataSetMinItems As New DataSet
        Dim ObjMenuItem As Infragistics.WebUI.UltraWebNavigator.Item
        Dim StrSQLCommand As String = String.Empty
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim strLang As String = String.Empty
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            StrSQLCommand = "Sys_GetMenuPermissions"
            ObjDataSetMinItems = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, StrSQLCommand, UserID, GroupID)

            If mObjDataHandler.CheckValidDataObject(ObjDataSetMinItems) Then
                Menu.Items.Clear()
                For Each ObjDataRow In ObjDataSetMinItems.Tables(CD_INTINITIALVALUE).Rows
                    If ObjDataRow.Item(CD_PARENTID) Is DBNull.Value And Not ObjDataRow.Item(CD_IsHide) Then

                        ObjMenuItem = New Infragistics.WebUI.UltraWebNavigator.Item
                        ObjMenuItem.Text = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                        ObjMenuItem.ToolTip = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                        ObjMenuItem.Tag = ObjDataRow.Item("shortcut")
                        ObjMenuItem.TargetUrl = ObjDataRow.Item("ViewForm") & "?TableName=" & ObjDataRow.Item("TableName") & "&RelatedForm=" & ObjDataRow.Item("Tag")
                        ObjMenuItem.TargetFrame = "contents"

                        LoadSubItem(page, ObjMenuItem, ObjDataRow.Item(CD_ID), ObjDataSetMinItems)
                        If ObjMenuItem.Items.Count > 0 Then
                            Menu.Items.Add(ObjMenuItem)
                        End If
                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRow = Nothing
            ObjDataSetMinItems.Dispose()
        End Try
    End Function

    Public Function LoadMenu(ByVal page As System.Web.UI.Page, ByVal Menu As Infragistics.WebUI.UltraWebNavigator.UltraWebMenu, ByVal UserID As Integer, ByVal GroupID As Integer, ByVal ModuleID As Integer) As Boolean
        Dim ObjDataRow As DataRow
        Dim ObjDataSetMinItems As New DataSet
        Dim ObjMenuItem As Infragistics.WebUI.UltraWebNavigator.Item
        Dim StrSQLCommand As String = String.Empty
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim strLang As String = String.Empty
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim IntHeight As Integer
        Dim IntWidth As Integer
        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            StrSQLCommand = "Sys_GetMenuPermissionsAll"
            ObjDataSetMinItems = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, StrSQLCommand, UserID, GroupID, ModuleID)

            If mObjDataHandler.CheckValidDataObject(ObjDataSetMinItems) Then
                Menu.Items.Clear()
                For Each ObjDataRow In ObjDataSetMinItems.Tables(CD_INTINITIALVALUE).Rows
                    If ObjDataRow.Item(CD_PARENTID) Is DBNull.Value And Not ObjDataRow.Item(CD_IsHide) Then
                        IntHeight = DataValue_Out(ObjDataRow.Item("Height"), SqlDbType.Int)
                        IntWidth = DataValue_Out(ObjDataRow.Item("Width"), SqlDbType.Int)

                        ObjMenuItem = New Infragistics.WebUI.UltraWebNavigator.Item
                        ObjMenuItem.Text = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                        ObjMenuItem.ToolTip = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                        ObjMenuItem.Tag = ObjDataRow.Item("shortcut")
                        ObjMenuItem.TargetUrl = ObjDataRow.Item("ViewForm") & "?TableName=" & ObjDataRow.Item("TableName") & "&RelatedForm=" & ObjDataRow.Item("Tag") & "&Height=" & IntHeight & "&Width=" & IntWidth
                        ObjMenuItem.TargetFrame = "contents"

                        LoadSubItem(page, ObjMenuItem, ObjDataRow.Item(CD_ID), ObjDataSetMinItems)
                        If ObjMenuItem.Items.Count > 0 Then
                            Menu.Items.Add(ObjMenuItem)
                        End If


                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRow = Nothing
            ObjDataSetMinItems.Dispose()
        End Try
    End Function

    Public Function LoadMenu(ByVal page As System.Web.UI.Page, ByRef wlbModules As Infragistics.WebUI.UltraWebListbar.UltraWebListbar, ByVal UserID As Integer, ByVal GroupID As Integer) As Boolean
        Dim ObjDataRow As DataRow
        Dim ObjDataSetMinItems As New DataSet
        Dim ObjTreeNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim StrSQLCommand As String = String.Empty
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim strLang As String = String.Empty
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim IntHeight As Integer
        Dim IntWidth As Integer
        Dim ObjGroup As New Infragistics.WebUI.UltraWebListbar.Group
        Dim ObjTree As New Infragistics.WebUI.UltraWebNavigator.UltraWebTree
        Dim ObjGroupTemplate As GroupTemplate
        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            StrSQLCommand = "hrs_GetMenuPermissionsAll"
            For Each ObjGroup In wlbModules.Groups
                ObjTree = New Infragistics.WebUI.UltraWebNavigator.UltraWebTree
                If ObjGroup.Tag.ToString() <> "10101" Then
                    ObjDataSetMinItems = New DataSet
                    ObjDataSetMinItems = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, StrSQLCommand, UserID, GroupID, ObjGroup.Tag)
                    If DataHandler.CheckValidDataObject(ObjDataSetMinItems) Then
                        ObjTree.Nodes.Clear()
                        For Each ObjDataRow In ObjDataSetMinItems.Tables(CD_INTINITIALVALUE).Rows
                            If ObjDataRow.Item(CD_PARENTID) Is DBNull.Value And Not ObjDataRow.Item(CD_IsHide) Then
                                IntHeight = DataHandler.DataValue_Out(ObjDataRow.Item("Height"), SqlDbType.Int)
                                IntWidth = DataHandler.DataValue_Out(ObjDataRow.Item("Width"), SqlDbType.Int)
                                ObjTreeNode = New Infragistics.WebUI.UltraWebNavigator.Node
                                ObjTreeNode.Text = DataHandler.DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                                ObjTreeNode.ToolTip = DataHandler.DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                                ObjTreeNode.Tag = "RelatedForm=" & ObjDataRow.Item("Tag") & ";Height=" & IntHeight & ";Width=" & IntWidth & ";LinkTarget=" & ObjDataRow.Item("LinkTarget") & ";LinkUrl=" & ObjDataRow.Item("LinkUrl") & ";FrmID=" & ObjDataRow.Item("TargetFormID") & ";Header=" & ObjTreeNode.Text & ";MainID=" & ObjDataRow.Item("MainID")
                                LoadSubNode(page, ObjTreeNode, ObjDataRow.Item(CD_ID), ObjDataSetMinItems)
                                If ObjTreeNode.Nodes.Count > 0 Then
                                    ObjTreeNode.Tag = ""
                                    ObjTree.Nodes.Add(ObjTreeNode)
                                End If
                            End If
                        Next
                    End If
                    ObjTree.WebTreeTarget = Infragistics.WebUI.UltraWebNavigator.WebTreeTarget.ClassicTree
                    ObjTree.DefaultImage = "../../Common/Images/ig_treeFolder.gif"
                    ObjTree.LeafNodeImageUrl = "../../Common/Images/board_sn.gif"
                    ObjTree.Images.SelectedImage.Url = "../../Common/Images/board_sh.gif"
                    ObjTree.Images.ParentNodeImage.Url = "../../Common/Images/MainFolder_sn.gif"
                    ObjGroup.GroupStyle.Height = ObjTree.Height
                    ObjTree.ClientSideEvents.NodeClick = "MainMenuTree_NodeClick"
                    ObjGroupTemplate = New GroupTemplate
                    ObjGroupTemplate.ObjTree = ObjTree
                    ObjGroup.Template = ObjGroupTemplate
                Else
                    LoadGroupsTree(page, ObjTree, UserID, GroupID)
                    LoadReportTree(page, ObjTree, UserID, GroupID)
                    ObjTree.WebTreeTarget = Infragistics.WebUI.UltraWebNavigator.WebTreeTarget.ClassicTree
                    ObjTree.LeafNodeImageUrl = "../../Common/Images/icon_mini_memberlist.gif"
                    ObjTree.Images.ParentNodeImage.Url = "../../Common/Images/forum_faq.gif"
                    ObjGroup.GroupStyle.Height = ObjTree.Height
                    ObjTree.ClientSideEvents.NodeClick = "UltraWebTree1ReportTree_NodeClick"
                    ObjGroupTemplate = New GroupTemplate
                    ObjGroupTemplate.ObjTree = ObjTree
                    ObjGroup.Template = ObjGroupTemplate
                End If

            Next
            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRow = Nothing
            ObjDataSetMinItems.Dispose()
        End Try
    End Function
    Public Function LoadMenu(ByVal page As System.Web.UI.Page, ByVal ModuleID As Integer, ByVal UserID As Integer, ByVal GroupID As Integer, ByVal Rpt As Boolean) As System.Web.UI.WebControls.TreeView
        Dim ObjDataRow As DataRow
        Dim ObjDataSetMinItems As New DataSet
        Dim ObjTreeNode As System.Web.UI.WebControls.TreeNode
        Dim StrSQLCommand As String = String.Empty
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim strLang As String = String.Empty
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim IntHeight As Integer
        Dim IntWidth As Integer
        Dim ObjTree As New System.Web.UI.WebControls.TreeView
        ObjTree.ID = ModuleID
        ObjTree.Attributes.Add("onclick", "return OnTreeClick(event)")
        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            StrSQLCommand = "hrs_GetMenuPermissionsAll"
            If Rpt = False Then
                ObjDataSetMinItems = New DataSet
                ObjDataSetMinItems = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, StrSQLCommand, UserID, GroupID, ModuleID)
                If DataHandler.CheckValidDataObject(ObjDataSetMinItems) Then
                    ObjTree.Nodes.Clear()
                    For Each ObjDataRow In ObjDataSetMinItems.Tables(CD_INTINITIALVALUE).Rows
                        If ObjDataRow.Item(CD_PARENTID) Is DBNull.Value And Not ObjDataRow.Item(CD_IsHide) Then
                            IntHeight = DataHandler.DataValue_Out(ObjDataRow.Item("Height"), SqlDbType.Int)
                            IntWidth = DataHandler.DataValue_Out(ObjDataRow.Item("Width"), SqlDbType.Int)
                            ObjTreeNode = New System.Web.UI.WebControls.TreeNode
                            ObjTreeNode.Text = DataHandler.DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                            ObjTreeNode.ToolTip = DataHandler.DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                            ObjTreeNode.Value = "RelatedForm=" & ObjDataRow.Item("Tag") & ";Height=" & IntHeight & ";Width=" & IntWidth & ";LinkTarget=" & ObjDataRow.Item("LinkTarget") & ";LinkUrl=" & ObjDataRow.Item("LinkUrl") & ";FrmID=" & ObjDataRow.Item("TargetFormID") & ";Header=" & ObjTreeNode.Text & ";MainID=" & ObjDataRow.Item("MainID")
                            LoadSubNode(page, ObjTreeNode, ObjDataRow.Item(CD_ID), ObjDataSetMinItems)
                            If ObjTreeNode.ChildNodes.Count > 0 Then
                                ObjTreeNode.Value = ""
                                ObjTreeNode.SelectAction = System.Web.UI.WebControls.TreeNodeSelectAction.Expand
                                ObjTreeNode.CollapseAll()
                                ObjTreeNode.ImageUrl = "Common/Images/26.png"
                                ObjTree.Nodes.Add(ObjTreeNode)
                            End If
                        End If
                    Next
                End If
                Return ObjTree
            Else
                LoadGroupsTree(page, ObjTree, UserID, GroupID)
                LoadReportTree(page, ObjTree, UserID, GroupID)
                Return ObjTree
            End If
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return Nothing
        Finally
            ObjDataRow = Nothing
            ObjDataSetMinItems.Dispose()
        End Try
    End Function
    Public Function LoadReportTree(ByVal page As System.Web.UI.Page, ByRef Tree As System.Web.UI.WebControls.TreeView, ByVal UserID As Integer, ByVal GroupID As Integer) As Boolean
        Dim ObjDataRow As DataRow
        Dim ObjDataSetMinItems As New DataSet
        Dim ObjMenuNode As System.Web.UI.WebControls.TreeNode
        Dim StrSQLCommand As String = String.Empty
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim strLang As String = String.Empty
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim GroupNode As System.Web.UI.WebControls.TreeNode
        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            StrSQLCommand = "hrs_GetReportPermissions"
            ObjDataSetMinItems = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, StrSQLCommand, UserID, GroupID)
            If mObjDataHandler.CheckValidDataObject(ObjDataSetMinItems) Then
                For Each ObjDataRow In ObjDataSetMinItems.Tables(CD_INTINITIALVALUE).Rows
                    Try

                        GroupNode = New System.Web.UI.WebControls.TreeNode
                        ObjMenuNode = New System.Web.UI.WebControls.TreeNode
                        ObjMenuNode.Text = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                        ObjMenuNode.ToolTip = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                        ObjMenuNode.Value = "r=" & DataValue_Out(ObjDataRow.Item("Code"), SqlDbType.Int)
                        ObjMenuNode.SelectAction = System.Web.UI.WebControls.TreeNodeSelectAction.Select
                        ObjMenuNode.ImageUrl = "Common/Images/report.png"
                        GroupNode = Tree.FindNode(DataValue_Out(ObjDataRow.Item("ReportGroupID"), SqlDbType.Int))

                        If ObjDataRow.Item("ReportGroupID") > 0 And GroupNode IsNot Nothing Then
                            GroupNode.ChildNodes.Add(ObjMenuNode)
                        End If

                    Catch ex As Exception
                    End Try
                Next
            End If
            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRow = Nothing
            ObjDataSetMinItems.Dispose()
        End Try
    End Function
    Public Function LoadGroupsTree(ByVal page As System.Web.UI.Page, ByVal Tree As System.Web.UI.WebControls.TreeView, ByVal UserID As Integer, ByVal GroupID As Integer) As Boolean
        Dim ObjDataRow As DataRow
        Dim ObjDataSetMinItems As New DataSet
        Dim ObjMenuNode As System.Web.UI.WebControls.TreeNode
        Dim StrSQLCommand As String = String.Empty
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim strLang As String = String.Empty
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            StrSQLCommand = "hrs_GetReportsGroups"
            ObjDataSetMinItems = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, StrSQLCommand)
            If mObjDataHandler.CheckValidDataObject(ObjDataSetMinItems) Then
                Tree.Nodes.Clear()
                For Each ObjDataRow In ObjDataSetMinItems.Tables(CD_INTINITIALVALUE).Rows
                    If ObjDataRow.Item(CD_PARENTID) Is DBNull.Value Then
                        ObjMenuNode = New System.Web.UI.WebControls.TreeNode
                        ObjMenuNode.Text = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                        ObjMenuNode.ToolTip = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                        ObjMenuNode.Value = DataValue_Out(ObjDataRow.Item("ID"), SqlDbType.VarChar)
                        ObjMenuNode.SelectAction = System.Web.UI.WebControls.TreeNodeSelectAction.Expand
                        ObjMenuNode.CollapseAll()
                        ObjMenuNode.ImageUrl = "Common/Images/26.png"

                        LoadGroupsSubNode(page, ObjMenuNode, ObjDataRow.Item(CD_ID), ObjDataSetMinItems)
                        Tree.Nodes.Add(ObjMenuNode)
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRow = Nothing
            ObjDataSetMinItems.Dispose()
        End Try
    End Function
    Private Function LoadGroupsSubNode(ByVal page As System.Web.UI.Page, ByRef Item As System.Web.UI.WebControls.TreeNode, ByVal ParentID As Integer, ByVal Data As DataSet) As Boolean
        Dim ObjDataRows() As DataRow
        Dim ObjDataRow As DataRow
        Dim ObjMenuNode As System.Web.UI.WebControls.TreeNode
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim strLang As String = String.Empty
        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            ObjDataRows = Data.Tables(CD_INTINITIALVALUE).Select(CD_PARENTID & "=" & ParentID)
            For Each ObjDataRow In ObjDataRows
                ObjMenuNode = New System.Web.UI.WebControls.TreeNode
                ObjMenuNode.Text = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                ObjMenuNode.ToolTip = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                ObjMenuNode.Value = DataValue_Out(ObjDataRow.Item("ID"), SqlDbType.VarChar)
                ObjMenuNode.SelectAction = System.Web.UI.WebControls.TreeNodeSelectAction.Expand
                ObjMenuNode.CollapseAll()
                ObjMenuNode.ImageUrl = "Common/Images/26.png"
                LoadSubNode(page, ObjMenuNode, ObjDataRow.Item(CD_ID), Data)
                Item.ChildNodes.Add(ObjMenuNode)
            Next
            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRows = Nothing
            ObjDataRow = Nothing
        End Try
    End Function
    Private Function LoadSubNode(ByVal page As System.Web.UI.Page, ByRef Node As System.Web.UI.WebControls.TreeNode, ByVal ParentID As Integer, ByVal Data As DataSet) As Boolean
        Dim ObjDataRows() As DataRow
        Dim ObjDataRow As DataRow
        Dim ObjTreeNode As System.Web.UI.WebControls.TreeNode
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim ObjStringHandler As New Venus.Shared.StringHandler
        Dim strLang As String = String.Empty
        Dim IntHeight As Integer
        Dim IntWidth As Integer
        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            ObjDataRows = Data.Tables(CD_INTINITIALVALUE).Select(CD_PARENTID & "=" & ParentID)
            For Each ObjDataRow In ObjDataRows
                IntHeight = DataValue_Out(ObjDataRow.Item("Height"), SqlDbType.Int)
                IntWidth = DataValue_Out(ObjDataRow.Item("Width"), SqlDbType.Int)
                ObjTreeNode = New System.Web.UI.WebControls.TreeNode
                ObjTreeNode.Text = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                ObjTreeNode.Value = "RelatedForm=" & ObjDataRow.Item("Tag") & ";Height=" & IntHeight & ";Width=" & IntWidth & ";LinkTarget=" & ObjDataRow.Item("LinkTarget") & ";LinkUrl=" & ObjDataRow.Item("LinkUrl") & ";FrmID=" & ObjDataRow.Item("TargetFormID") & ";Header=" & ObjTreeNode.Text & ";MainID=" & ObjDataRow.Item("MainID")
                LoadSubNode(page, ObjTreeNode, ObjDataRow.Item(CD_ID), Data)
                If ObjTreeNode.ChildNodes.Count > 0 Then
                    ObjTreeNode.Value = ""
                    ObjTreeNode.SelectAction = System.Web.UI.WebControls.TreeNodeSelectAction.Expand
                    ObjTreeNode.CollapseAll()
                    ObjTreeNode.ImageUrl = "Common/Images/26.png"
                    Node.ChildNodes.Add(ObjTreeNode)
                ElseIf Not ObjDataRow.Item("Tag") Is DBNull.Value AndAlso Not ObjDataRow.Item("Tag") = "" Then
                    ObjTreeNode.SelectAction = System.Web.UI.WebControls.TreeNodeSelectAction.Select
                    ObjTreeNode.ImageUrl = "Common/Images/application2.png"
                    Node.ChildNodes.Add(ObjTreeNode)
                End If
            Next
            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRow = Nothing
            ObjDataRows = Nothing
        End Try
    End Function
    Public Function LoadReportMenu(ByVal page As System.Web.UI.Page, ByVal Menu As Infragistics.WebUI.UltraWebNavigator.UltraWebMenu, ByVal UserID As Integer, ByVal GroupID As Integer) As Boolean
        Dim ObjDataRow As DataRow
        Dim ObjDataSetMinItems As New DataSet
        Dim ObjMenuItem As Infragistics.WebUI.UltraWebNavigator.Item
        Dim StrSQLCommand As String = String.Empty
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim strLang As String = String.Empty
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            StrSQLCommand = "Sys_GetReportPermissions"
            ObjDataSetMinItems = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, StrSQLCommand, UserID, GroupID)
            If DataHandler.CheckValidDataObject(ObjDataSetMinItems) Then
                Menu.Items.Clear()
                For Each ObjDataRow In ObjDataSetMinItems.Tables(CD_INTINITIALVALUE).Rows
                    If ObjDataRow.Item(CD_PARENTID) Is DBNull.Value Then
                        ObjMenuItem = New Infragistics.WebUI.UltraWebNavigator.Item
                        ObjMenuItem.Text = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                        ObjMenuItem.ToolTip = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                        ObjMenuItem.Tag = ObjDataRow.Item("shortcut")
                        ObjMenuItem.TargetUrl = ObjDataRow.Item("ViewForm") & "?ReportName=" & ObjDataRow.Item("Code") & "&SPName=" & ObjDataRow.Item("DataSource")
                        ObjMenuItem.TargetFrame = "contents"
                        LoadReportSubItem(page, ObjMenuItem, ObjDataRow.Item(CD_ID), ObjDataSetMinItems)
                        Menu.Items.Add(ObjMenuItem)
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRow = Nothing
            ObjDataSetMinItems.Dispose()
        End Try
    End Function
    Public Function LoadGroupsTree(ByVal page As System.Web.UI.Page, ByVal Tree As Infragistics.WebUI.UltraWebNavigator.UltraWebTree, ByVal UserID As Integer, ByVal GroupID As Integer) As Boolean
        Dim ObjDataRow As DataRow
        Dim ObjDataSetMinItems As New DataSet
        Dim ObjMenuNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim StrSQLCommand As String = String.Empty
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim strLang As String = String.Empty
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            StrSQLCommand = "hrs_GetReportsGroups"
            ObjDataSetMinItems = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, StrSQLCommand)

            If mObjDataHandler.CheckValidDataObject(ObjDataSetMinItems) Then
                Tree.Nodes.Clear()
                For Each ObjDataRow In ObjDataSetMinItems.Tables(CD_INTINITIALVALUE).Rows
                    If ObjDataRow.Item(CD_PARENTID) Is DBNull.Value Then
                        ObjMenuNode = New Infragistics.WebUI.UltraWebNavigator.Node
                        ObjMenuNode.Text = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                        ObjMenuNode.ToolTip = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                        ObjMenuNode.Tag = DataValue_Out(ObjDataRow.Item("ID"), SqlDbType.VarChar)
                        LoadGroupsSubNode(page, ObjMenuNode, ObjDataRow.Item(CD_ID), ObjDataSetMinItems)
                        Tree.Nodes.Add(ObjMenuNode)
                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRow = Nothing
            ObjDataSetMinItems.Dispose()
        End Try
    End Function

    Public Function LoadReportTree(ByVal page As System.Web.UI.Page, ByRef Tree As Infragistics.WebUI.UltraWebNavigator.UltraWebTree, ByVal UserID As Integer, ByVal GroupID As Integer) As Boolean
        Dim ObjDataRow As DataRow
        Dim ObjDataSetMinItems As New DataSet
        Dim ObjMenuNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim StrSQLCommand As String = String.Empty
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim strLang As String = String.Empty
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim GroupNode As Infragistics.WebUI.UltraWebNavigator.Node
        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            StrSQLCommand = "hrs_GetReportPermissions"
            ObjDataSetMinItems = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, StrSQLCommand, UserID, GroupID)

            If mObjDataHandler.CheckValidDataObject(ObjDataSetMinItems) Then
                For Each ObjDataRow In ObjDataSetMinItems.Tables(CD_INTINITIALVALUE).Rows
                    GroupNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjMenuNode = New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjMenuNode.Text = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                    ObjMenuNode.ToolTip = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                    ObjMenuNode.Tag = "r=" & DataValue_Out(ObjDataRow.Item("Code"), SqlDbType.Int)
                    GroupNode = Tree.Find(DataValue_Out(ObjDataRow.Item("ReportGroupID"), SqlDbType.Int))
                    GroupNode.Nodes.Add(ObjMenuNode)
                Next
            End If

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRow = Nothing
            ObjDataSetMinItems.Dispose()
        End Try
    End Function

    Public Function LoadTree(ByVal Tree As Infragistics.WebUI.UltraWebNavigator.UltraWebTree, ByVal UserID As Integer) As Boolean
        Dim ObjDataRow As DataRow
        Dim ObjDataSetMinNodes As New DataSet
        Dim ObjTreeNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim StrSQLCommand As String = String.Empty

        Try
            StrSQLCommand = CD_SQLSTATMENTTREE1 & " Order by Rank "
            ObjDataSetMinNodes = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, CommandType.Text, StrSQLCommand)
            Tree.Nodes.Clear()
            If mObjDataHandler.CheckValidDataObject(ObjDataSetMinNodes) Then
                For Each ObjDataRow In ObjDataSetMinNodes.Tables(CD_INTINITIALVALUE).Rows
                    If ObjDataRow.Item(CD_PARENTID) Is DBNull.Value Then

                        ObjTreeNode = New Infragistics.WebUI.UltraWebNavigator.Node
                        ObjTreeNode.Text = ObjDataRow.Item(CD_ENGNAME)
                        ObjTreeNode.Tag = ObjDataRow.Item(CD_ID)

                        LoadSubNode(ObjTreeNode, ObjDataRow.Item(CD_ID), ObjDataSetMinNodes)
                        Tree.Nodes.Add(ObjTreeNode)

                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRow = Nothing
            ObjDataSetMinNodes.Dispose()
        End Try
    End Function

    Public Function LoadTreeMenus(ByVal Tree As Infragistics.WebUI.UltraWebNavigator.UltraWebTree, ByVal UserID As Integer, ByVal mLage As Integer) As Boolean
        Dim ObjDataRow As DataRow
        Dim ObjDataSetMinNodes As New DataSet
        Dim ObjTreeNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim StrSQLCommand As String = String.Empty

        Try
            StrSQLCommand = CD_SQLSTATMENTTREE1 & " Order by Rank "
            ObjDataSetMinNodes = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, CommandType.Text, StrSQLCommand)
            Tree.Nodes.Clear()
            If mObjDataHandler.CheckValidDataObject(ObjDataSetMinNodes) Then
                For Each ObjDataRow In ObjDataSetMinNodes.Tables(CD_INTINITIALVALUE).Rows
                    If ObjDataRow.Item(CD_PARENTID) Is DBNull.Value Then

                        ObjTreeNode = New Infragistics.WebUI.UltraWebNavigator.Node
                        ObjTreeNode.Text = ObjDataRow.Item(IIf(mLage = 0, CD_ENGNAME, CD_ARBNAME))
                        ObjTreeNode.Tag = ObjDataRow.Item(CD_ID)

                        LoadSubNodeMenus(ObjTreeNode, ObjDataRow.Item(CD_ID), ObjDataSetMinNodes, IIf(mLage = 0, CD_ENGNAME, CD_ARBNAME))
                        Tree.Nodes.Add(ObjTreeNode)

                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRow = Nothing
            ObjDataSetMinNodes.Dispose()
        End Try
    End Function

    Public Function LoadMainModule(ByVal Ddl As System.Web.UI.WebControls.DropDownList) As Boolean
        Dim StrSqlCommand As String = String.Empty
        Dim ObjDataSet As New DataSet
        Dim ObjDataRow As DataRow
        Dim ObjItem As System.Web.UI.WebControls.ListItem

        Try
            StrSqlCommand = "Select * From sys_modules where isnull(canceldate,'')=''"
            ObjDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, CommandType.Text, StrSqlCommand)
            If mObjDataHandler.CheckValidDataObject(ObjDataSet) Then
                Ddl.Items.Clear()
                For Each ObjDataRow In ObjDataSet.Tables(CD_INTINITIALVALUE).Rows

                    ObjItem = New System.Web.UI.WebControls.ListItem
                    ObjItem.Text = mObjDataHandler.DataValue(ObjDataRow(CD_ENGNAME), SqlDbType.VarChar)
                    ObjItem.Value = mObjDataHandler.DataValue(ObjDataRow(CD_ID), SqlDbType.Int)
                    Ddl.Items.Add(ObjItem)

                Next

            End If

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrSqlCommand, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        End Try
    End Function

    Public Function MarkSelection(ByRef Menu As Infragistics.WebUI.UltraWebNavigator.UltraWebMenu, ByVal Shortcut As String, ByRef TargetForm As String) As Boolean
        Dim ObjItem As New Infragistics.WebUI.UltraWebNavigator.Item

        Try
            For Each ObjItem In Menu.Items
                If GetSelectedItem(ObjItem, Shortcut, TargetForm) Then
                    Return True
                End If
            Next


        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        End Try
    End Function

    Private Function GetSelectedItem(ByRef item As Infragistics.WebUI.UltraWebNavigator.Item, ByVal Shortcut As String, ByRef TargetForm As String) As Boolean
        Dim ObjItem As New Infragistics.WebUI.UltraWebNavigator.Item
        Try
            If item.Items.Count > 0 Then
                For Each ObjItem In item.Items
                    If GetSelectedItem(ObjItem, Shortcut, TargetForm) Then
                        Return True
                    End If
                Next
            Else
                If item.Tag = Shortcut Then
                    TargetForm = item.TargetUrl
                    Return True
                End If
            End If

            Return False
        Catch ex As Exception
            Return False
            mObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
        End Try
    End Function

    Public Function ViewNodeInformation(ByVal Node As Infragistics.WebUI.UltraWebNavigator.Node) As Boolean
        Dim strSqlCommandString As String = String.Empty
        Dim IntIndex As Integer
        Dim ObjDataSet As New DataSet

        Try
            IntIndex = CInt(Node.Tag)
            strSqlCommandString = CD_SQLSTATMENT2 & IntIndex
            ObjDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, CommandType.Text, strSqlCommandString)
            If mObjDataHandler.CheckValidDataObject(ObjDataSet) Then
                Assign_Values(ObjDataSet)
            End If
            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(strSqlCommandString, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataSet.Dispose()
        End Try
    End Function

    Public Function GetPagesList(ByRef Ddl As System.Web.UI.WebControls.DropDownList, ByVal mLage As Integer, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String = String.Empty
        Dim ObjDataset As New DataSet
        Dim Item As System.Web.UI.WebControls.ListItem
        Dim StrFilterStatment As String

        Try
            If Len(Filter) > 0 Then
                StrFilterStatment = " Where " & Filter & " Order by " & IIf(mLage = 0, CD_ENGNAME, CD_ARBNAME)
            Else
                StrFilterStatment = " Order by " & IIf(mLage = 0, CD_ENGNAME, CD_ARBNAME)
            End If

            StrCommandString = CD_SQLSTATMENTTREE1 & StrFilterStatment
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, CommandType.Text, StrCommandString)
            Ddl.Items.Clear()

            For Each ObjDataRow In ObjDataset.Tables(CD_INTINITIALVALUE).Rows
                Item = New System.Web.UI.WebControls.ListItem
                Item.Text = mObjDataHandler.DataValue(ObjDataRow(IIf(mLage = 0, CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                Item.Value = ObjDataRow(CD_ID)
                Ddl.Items.Add(Item)
            Next

            Item = New System.Web.UI.WebControls.ListItem
            Item.Text = "Root"
            Item.Value = 0
            Ddl.Items.Add(Item)

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrCommandString, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    Public Function GetPagesList(ByRef Ddl As System.Web.UI.WebControls.DropDownList, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String = String.Empty
        Dim ObjDataset As New DataSet
        Dim Item As System.Web.UI.WebControls.ListItem
        Dim StrFilterStatment As String

        Try
            If Len(Filter) > 0 Then
                StrFilterStatment = " Where " & Filter & " Order by EngName "
            Else
                StrFilterStatment = " Order by EngName "
            End If

            StrCommandString = CD_SQLSTATMENTTREE1 & StrFilterStatment
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, CommandType.Text, StrCommandString)
            Ddl.Items.Clear()

            For Each ObjDataRow In ObjDataset.Tables(CD_INTINITIALVALUE).Rows
                Item = New System.Web.UI.WebControls.ListItem
                Item.Text = mObjDataHandler.DataValue(ObjDataRow(CD_ENGNAME), SqlDbType.VarChar)
                Item.Value = ObjDataRow(CD_ID)
                Ddl.Items.Add(Item)
            Next

            Item = New System.Web.UI.WebControls.ListItem
            Item.Text = "Root"
            Item.Value = 0
            Ddl.Items.Add(Item)

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrCommandString, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataset.Dispose()
        End Try
    End Function
    Public Function GetDropDown(ByRef DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal Lang As String, Optional ByVal NullNode As Boolean = False) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mstrConnectionString)
        Try
            StrCommandString = "Select * From sys_Menus"
            StrCommandString &= " Order By EngName"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, CommandType.Text, StrCommandString)
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Lang = "Ar", "[ برجاء الاختيار ]", "[Select Your Choice]")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = DataHandler.DataValue(ObjDataRow(IIf(Lang = "Ar", "ArbName", "EngName")), SqlDbType.VarChar)
                If (Item.Text.Trim = "") Then
                    Item.Text = DataHandler.DataValue(ObjDataRow(IIf(Lang = "Ar", "ArbName", "EngName")), SqlDbType.VarChar)
                End If
                Item.Value = ObjDataRow("ID")
                DdlValues.Items.Add(Item)
            Next
            If DdlValues.Items.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
        Finally
            ObjDataset.Dispose()
        End Try
    End Function
    Public Function GetFormsList(ByRef Ddl As System.Web.UI.WebControls.DropDownList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String = String.Empty
        Dim ObjDataset As New DataSet
        Dim Item As System.Web.UI.WebControls.ListItem

        Try

            StrCommandString = CD_SQLSTATMENT7
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, CommandType.Text, StrCommandString)
            Ddl.Items.Clear()

            For Each ObjDataRow In ObjDataset.Tables(CD_INTINITIALVALUE).Rows
                Item = New System.Web.UI.WebControls.ListItem
                Item.Text = mObjDataHandler.DataValue(ObjDataRow(CD_ENGNAME), SqlDbType.VarChar)
                Item.Value = ObjDataRow(CD_ID)
                Ddl.Items.Add(Item)
            Next

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrCommandString, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    Public Function GetPagesList_HardDrive(ByVal Ddl As System.Web.UI.WebControls.DropDownList, ByVal DestinationFolder As String) As Boolean
        Dim Files() As String
        Dim File As String
        Dim FileName() As String
        Dim Item As System.Web.UI.WebControls.ListItem

        Try
            Files = IO.Directory.GetFiles(DestinationFolder)
            Ddl.Items.Clear()
            For Each File In Files
                If File.Substring(Len(File) - 5, 5).ToUpper = ".ASPX" Then
                    Item = New System.Web.UI.WebControls.ListItem
                    FileName = File.Split("\")
                    Item.Text = FileName(FileName.GetUpperBound(0))
                    Item.Value = FileName(FileName.GetUpperBound(0))
                    Ddl.Items.Add(Item)
                End If
            Next
            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally

        End Try
    End Function

    Public Function Save() As Boolean
        Dim StrCommandString As String = String.Empty
        Dim ObjCommand As New SqlClient.SqlCommand

        Try

            StrCommandString = CD_SQLSTATMENT5
            ObjCommand.Connection = New SqlClient.SqlConnection(mstrConnectionString)
            ObjCommand.CommandText = StrCommandString
            ObjCommand.CommandType = CommandType.Text
            AssignCommandParameter(ObjCommand)
            ObjCommand.Connection.Open()
            ObjCommand.ExecuteNonQuery()
            ObjCommand.Connection.Close()

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrCommandString, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjCommand.Dispose()
        End Try
    End Function

    Public Function Update(ByVal index As Integer) As Boolean
        Dim StrCommandString As String = String.Empty
        Dim ObjCommand As New SqlClient.SqlCommand

        Try

            StrCommandString = CD_SQLSTATMENT6 & index
            ObjCommand.Connection = New SqlClient.SqlConnection(mstrConnectionString)
            ObjCommand.CommandType = CommandType.Text
            ObjCommand.CommandText = StrCommandString
            AssignCommandParameter(ObjCommand)
            ObjCommand.Connection.Open()
            ObjCommand.ExecuteNonQuery()
            ObjCommand.Connection.Close()

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrCommandString, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjCommand.Dispose()
        End Try
    End Function

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrCommandString As String = String.Empty
        Dim ObjDataSet As New DataSet

        Try
            If Len(Filter) > 0 Then
                Filter = " Where " & Filter
            End If
            StrCommandString = CD_SQLSTATMENT3 & Filter
            ObjDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, CommandType.Text, StrCommandString)
            If mObjDataHandler.CheckValidDataObject(ObjDataSet) Then
                Assign_Values(ObjDataSet)
            Else
                'clear()
            End If


            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrCommandString, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataSet.Dispose()
        End Try
    End Function

    Public Function Find(ByVal index As Integer) As Boolean
        Dim StrCommandString As String = String.Empty
        Dim ObjDataset As DataSet

        Try
            StrCommandString = CD_SQLSTATMENT4 & index
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, CommandType.Text, StrCommandString)
            If mObjDataHandler.CheckValidDataObject(ObjDataset) Then
                Assign_Values(ObjDataset)
            Else
                'Clear
            End If

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrCommandString, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        End Try
    End Function

    Public Function Clear() As Boolean
        Try

            mStrCode = String.Empty
            mStrEngName = String.Empty
            mStrArbName = String.Empty
            mIntParentID = 0
            mIntRank = 0
            mBolIsHide = False
            mIntViewType = 0
            mIntFormID = 0
            mIntObjectID = 0
            mIntViewFormID = 0
            mStrShortcut = String.Empty

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        End Try
    End Function

    Public Function SetLanguage(ByVal page As System.Web.UI.Page, ByVal key As String) As String
        Dim ObjStringHandler As New Venus.Shared.StringHandler
        Dim ObjWebhandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        Dim IntLanguage As Integer
        Dim StrReturnValue As String = String.Empty
        Try
            ObjWebhandler.GetCookies(page, "Lang", StrLanguage)
            IntLanguage = IIf(StrLanguage = "en-US", 0, 1)
            StrReturnValue = ObjStringHandler.SwitchParts(key, "/", IntLanguage)
            Return StrReturnValue
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase(StrLanguage, ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return ""
        End Try
    End Function

    Public Function ReadParameters(ByVal IntFormID As Integer) As String
        Dim ObjDS As New DataSet()
        Dim StrSelectCommand As String = String.Empty
        Dim strQueryString As String = String.Empty
        Dim intIndex As Integer
        Try
            StrSelectCommand = " SELECT Name As ParamName ,Value As ParamValue  FROM sys_FormsParameters Where FormID = " & IntFormID
            ObjDS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mstrConnectionString, CommandType.Text, StrSelectCommand)
            If ObjDS.Tables(0).Rows.Count > 0 Then
                For Each row As Data.DataRow In ObjDS.Tables(0).Rows
                    If IsNumeric(row.Item("ParamValue")) Then
                        strQueryString &= row.Item("ParamName") & "=" & IIf(IsDBNull(row.Item("ParamValue")), "", row.Item("ParamValue")) & "&"
                    Else
                        strQueryString &= row.Item("ParamName") & "='" & IIf(IsDBNull(row.Item("ParamValue")), "''", row.Item("ParamValue")) & "'&"
                    End If
                Next
                intIndex = strQueryString.LastIndexOf("&")
                strQueryString = strQueryString.Remove(intIndex, 1)
            End If
            Return strQueryString
        Catch ex As Exception
            Return ""
        End Try
    End Function

#End Region

#Region "Private Function"

    Private Function Assign_Values(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(CD_INTINITIALVALUE).Rows(CD_INTINITIALVALUE)
                mIntID = DataValue_Out(.Item("ID"), SqlDbType.Int)
                mStrCode = DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mStrEngName = DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mStrArbName = DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mIntParentID = DataValue_Out(.Item("ParentID"), SqlDbType.Int)
                mIntRank = DataValue_Out(.Item("Rank"), SqlDbType.Int)
                mIntFormID = DataValue_Out(.Item("FormID"), SqlDbType.Int, True)
                mBolIsHide = DataValue_Out(.Item("IsHide"), SqlDbType.SmallInt)
                mStrShortcut = DataValue_Out(.Item("Shortcut"), SqlDbType.VarChar)
                mIntViewType = DataValue_Out(.Item("ViewType"), SqlDbType.SmallInt)
                mIntObjectID = DataValue_Out(.Item("ObjectID"), SqlDbType.Int, True)
                mIntViewFormID = DataValue_Out(.Item("ViewFormID"), SqlDbType.Int, True)
            End With

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        End Try
    End Function

    Private Function AssignCommandParameter(ByVal Command As System.Data.SqlClient.SqlCommand) As Boolean
        Dim ObjSqlParameter As New SqlClient.SqlParameter
        Try

            With Command.Parameters
                .Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar, 100)).Value = DataValue_In(mStrCode, SqlDbType.VarChar)
                .Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar, 100)).Value = DataValue_In(mStrEngName, SqlDbType.VarChar)
                .Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar, 100)).Value = DataValue_In(mStrArbName, SqlDbType.VarChar)
                .Add(New SqlClient.SqlParameter("@ParentID", SqlDbType.Int, 4)).Value = DataValue_In(mIntParentID, SqlDbType.Int, True)
                .Add(New SqlClient.SqlParameter("@Rank", SqlDbType.Int, 4)).Value = DataValue_In(mIntRank, SqlDbType.Int)
                .Add(New SqlClient.SqlParameter("@FormID", SqlDbType.Int)).Value = DataValue_In(mIntFormID, SqlDbType.Int, True)
                .Add(New SqlClient.SqlParameter("@IsHide", SqlDbType.Bit, 1)).Value = DataValue_In(mBolIsHide, SqlDbType.Bit)
                .Add(New SqlClient.SqlParameter("@ViewType", SqlDbType.TinyInt, 1)).Value = DataValue_In(mIntViewType, SqlDbType.Int)
                .Add(New SqlClient.SqlParameter("@Shortcut", SqlDbType.VarChar, 100)).Value = DataValue_In(mStrShortcut, SqlDbType.VarChar)
                .Add(New SqlClient.SqlParameter("@ObjectID", SqlDbType.Int)).Value = DataValue_In(mIntObjectID, SqlDbType.Int, True)
                .Add(New SqlClient.SqlParameter("@ViewFormID", SqlDbType.Int)).Value = DataValue_In(mIntViewFormID, SqlDbType.Int, True)
            End With

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        End Try
    End Function

    Private Function LoadSubItem(ByVal page As System.Web.UI.Page, ByRef Item As Infragistics.WebUI.UltraWebNavigator.Item, ByVal ParentID As Integer, ByVal Data As DataSet) As Boolean
        Dim ObjDataRows() As DataRow
        Dim ObjDataRow As DataRow
        Dim ObjMenuItem As Infragistics.WebUI.UltraWebNavigator.Item
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim strLang As String = String.Empty
        Dim IntHeight As Integer
        Dim IntWidth As Integer

        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            ObjDataRows = Data.Tables(CD_INTINITIALVALUE).Select(CD_PARENTID & "=" & ParentID)
            For Each ObjDataRow In ObjDataRows
                If Not ObjDataRow.Item(CD_IsHide) Then
                    IntHeight = DataValue_Out(ObjDataRow.Item("Height"), SqlDbType.Int)
                    IntWidth = DataValue_Out(ObjDataRow.Item("Width"), SqlDbType.Int)

                    ObjMenuItem = New Infragistics.WebUI.UltraWebNavigator.Item
                    ObjMenuItem.Text = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                    ObjMenuItem.ToolTip = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                    ObjMenuItem.Tag = ObjDataRow.Item("shortcut")
                    ObjMenuItem.TargetUrl = ObjDataRow.Item("ViewForm") & "?TableName=" & ObjDataRow.Item("TableName") & "&RelatedForm=" & ObjDataRow.Item("Tag") & "&Height=" & IntHeight & "&Width=" & IntWidth
                    ObjMenuItem.TargetFrame = "contents"

                    LoadSubItem(page, ObjMenuItem, ObjDataRow.Item(CD_ID), Data)
                    Item.Items.Add(ObjMenuItem)
                End If
            Next

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRows = Nothing
            ObjDataRow = Nothing
        End Try
    End Function

    Private Function LoadReportSubItem(ByVal page As System.Web.UI.Page, ByRef Item As Infragistics.WebUI.UltraWebNavigator.Item, ByVal ParentID As Integer, ByVal Data As DataSet) As Boolean
        Dim ObjDataRows() As DataRow
        Dim ObjDataRow As DataRow
        Dim ObjMenuItem As Infragistics.WebUI.UltraWebNavigator.Item
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim strLang As String = String.Empty
        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            ObjDataRows = Data.Tables(CD_INTINITIALVALUE).Select(CD_PARENTID & "=" & ParentID)
            For Each ObjDataRow In ObjDataRows
                If Not ObjDataRow.Item(CD_IsHide) Then

                    ObjMenuItem = New Infragistics.WebUI.UltraWebNavigator.Item
                    ObjMenuItem.Text = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                    ObjMenuItem.ToolTip = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                    ObjMenuItem.Tag = ObjDataRow.Item("shortcut")
                    ObjMenuItem.TargetUrl = ObjDataRow.Item("ViewForm") & "?ReportName=" & ObjDataRow.Item("Code") & "&SPName=" & ObjDataRow.Item("DataSource")
                    ObjMenuItem.TargetFrame = "contents"


                    LoadSubItem(page, ObjMenuItem, ObjDataRow.Item(CD_ID), Data)
                    Item.Items.Add(ObjMenuItem)
                End If
            Next

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRows = Nothing
            ObjDataRow = Nothing
        End Try
    End Function

    Private Function LoadGroupsSubNode(ByVal page As System.Web.UI.Page, ByRef Item As Infragistics.WebUI.UltraWebNavigator.Node, ByVal ParentID As Integer, ByVal Data As DataSet) As Boolean
        Dim ObjDataRows() As DataRow
        Dim ObjDataRow As DataRow
        Dim ObjMenuNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim strLang As String = String.Empty
        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            ObjDataRows = Data.Tables(CD_INTINITIALVALUE).Select(CD_PARENTID & "=" & ParentID)
            For Each ObjDataRow In ObjDataRows

                ObjMenuNode = New Infragistics.WebUI.UltraWebNavigator.Node
                ObjMenuNode.Text = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                ObjMenuNode.ToolTip = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                ObjMenuNode.Tag = DataValue_Out(ObjDataRow.Item("ID"), SqlDbType.VarChar)

                LoadSubNode(page, ObjMenuNode, ObjDataRow.Item(CD_ID), Data)
                Item.Nodes.Add(ObjMenuNode)
            Next

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRows = Nothing
            ObjDataRow = Nothing
        End Try
    End Function

    Private Function LoadSubNodeMenus(ByRef Node As Infragistics.WebUI.UltraWebNavigator.Node, ByVal ParentID As Integer, ByVal Data As DataSet, ByVal mName As String) As Boolean
        Dim ObjDataRows() As DataRow
        Dim ObjDataRow As DataRow
        Dim ObjTreeNode As Infragistics.WebUI.UltraWebNavigator.Node

        Try

            ObjDataRows = Data.Tables(CD_INTINITIALVALUE).Select(CD_PARENTID & "=" & ParentID)
            For Each ObjDataRow In ObjDataRows

                ObjTreeNode = New Infragistics.WebUI.UltraWebNavigator.Node
                ObjTreeNode.Text = ObjDataRow.Item(mName)
                ObjTreeNode.Tag = ObjDataRow.Item(CD_ID)
                LoadSubNodeMenus(ObjTreeNode, ObjDataRow.Item(CD_ID), Data, mName)
                Node.Nodes.Add(ObjTreeNode)

            Next

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRow = Nothing
            ObjDataRows = Nothing
        End Try
    End Function

    Private Function LoadSubNode(ByRef Node As Infragistics.WebUI.UltraWebNavigator.Node, ByVal ParentID As Integer, ByVal Data As DataSet) As Boolean
        Dim ObjDataRows() As DataRow
        Dim ObjDataRow As DataRow
        Dim ObjTreeNode As Infragistics.WebUI.UltraWebNavigator.Node

        Try

            ObjDataRows = Data.Tables(CD_INTINITIALVALUE).Select(CD_PARENTID & "=" & ParentID)
            For Each ObjDataRow In ObjDataRows

                ObjTreeNode = New Infragistics.WebUI.UltraWebNavigator.Node
                ObjTreeNode.Text = ObjDataRow.Item(CD_ENGNAME)
                ObjTreeNode.Tag = ObjDataRow.Item(CD_ID)
                LoadSubNode(ObjTreeNode, ObjDataRow.Item(CD_ID), Data)
                Node.Nodes.Add(ObjTreeNode)

            Next

            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRow = Nothing
            ObjDataRows = Nothing
        End Try
    End Function

    Private Function LoadSubNode(ByVal page As System.Web.UI.Page, ByRef Node As Infragistics.WebUI.UltraWebNavigator.Node, ByVal ParentID As Integer, ByVal Data As DataSet) As Boolean
        Dim ObjDataRows() As DataRow
        Dim ObjDataRow As DataRow
        Dim ObjTreeNode As Infragistics.WebUI.UltraWebNavigator.Node
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim ObjStringHandler As New Venus.Shared.StringHandler
        Dim strLang As String = String.Empty
        Dim IntHeight As Integer
        Dim IntWidth As Integer
        Dim StrModulePerfix As String
        Try
            ObjWebHandler.GetCookies(page, "Lang", strLang)
            ObjDataRows = Data.Tables(CD_INTINITIALVALUE).Select(CD_PARENTID & "=" & ParentID)
            For Each ObjDataRow In ObjDataRows
                IntHeight = DataValue_Out(ObjDataRow.Item("Height"), SqlDbType.Int)
                IntWidth = DataValue_Out(ObjDataRow.Item("Width"), SqlDbType.Int)
                ObjTreeNode = New Infragistics.WebUI.UltraWebNavigator.Node
                ObjTreeNode.Text = DataValue_Out(ObjDataRow.Item(IIf(strLang = "en-US", CD_ENGNAME, CD_ARBNAME)), SqlDbType.VarChar)
                ObjTreeNode.Tag = "RelatedForm=" & ObjDataRow.Item("Tag") & ";Height=" & IntHeight & ";Width=" & IntWidth & ";LinkTarget=" & ObjDataRow.Item("LinkTarget") & ";LinkUrl=" & ObjDataRow.Item("LinkUrl") & ";FrmID=" & ObjDataRow.Item("TargetFormID") & ";Header=" & ObjTreeNode.Text & ";MainID=" & ObjDataRow.Item("MainID")
                LoadSubNode(page, ObjTreeNode, ObjDataRow.Item(CD_ID), Data)
                If ObjTreeNode.Nodes.Count > 0 Then
                    ObjTreeNode.Tag = ""
                    Node.Nodes.Add(ObjTreeNode)
                ElseIf Not ObjDataRow.Item("Tag") Is DBNull.Value AndAlso Not ObjDataRow.Item("Tag") = "" Then
                    Node.Nodes.Add(ObjTreeNode)
                End If
            Next
            Return True
        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        Finally
            ObjDataRow = Nothing
            ObjDataRows = Nothing
        End Try
    End Function

#End Region

#Region "Public Destructor"

    Public Sub finlize()
        Try

            mObjErrorHandler = Nothing
            mObjDataHandler = Nothing

        Catch ex As Exception
            mObjErrorHandler.RecordExceptions_DataBase("Error while finalizing class", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
        End Try
    End Sub


#End Region

End Class


Public Class GroupTemplate
    Implements System.Web.UI.ITemplate

    Private mObjTree As Infragistics.WebUI.UltraWebNavigator.UltraWebTree

    Public Property ObjTree() As Infragistics.WebUI.UltraWebNavigator.UltraWebTree
        Get
            Return mObjTree
        End Get
        Set(ByVal value As Infragistics.WebUI.UltraWebNavigator.UltraWebTree)
            mObjTree = value
        End Set
    End Property

    Public Sub InstantiateIn(ByVal container As System.Web.UI.Control) Implements System.Web.UI.ITemplate.InstantiateIn
        container.Controls.Add(mObjTree)
    End Sub

End Class