Imports System
Imports System.Web
Imports System.Data
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class CustomFormsWs
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)> _
    Public Function SaveControls(ByVal Elements As Object(), ByVal DocumentID As String) As Integer
        Try
            Dim Str As String = ""
            ClearData(DocumentID)
            For i As Integer = 0 To Elements.Length - 1
                If Elements(i) IsNot Nothing Then
                    If Elements(i)(16).ToString() = "0" Then
                        Dim _Dwf_DocumentElement As New ClsDwf_DocumentElements(New System.Web.UI.Page)
                        _Dwf_DocumentElement.Code = Elements(i)(0).ToString()
                        _Dwf_DocumentElement.DocumentID = Convert.ToInt32(DocumentID)
                        _Dwf_DocumentElement.ElementType = Elements(i)(1).ToString()
                        _Dwf_DocumentElement.FColor = Elements(i)(2).ToString()
                        _Dwf_DocumentElement.BColor = Elements(i)(3).ToString()
                        _Dwf_DocumentElement.Loc_Top = Elements(i)(4).ToString()
                        _Dwf_DocumentElement.Loc_Left = Elements(i)(5).ToString()
                        _Dwf_DocumentElement.Size_Width = Elements(i)(6).ToString()
                        _Dwf_DocumentElement.Size_Hight = Elements(i)(7).ToString()
                        _Dwf_DocumentElement.TabIndex = Elements(i)(8).ToString()
                        _Dwf_DocumentElement.Image_Src = Elements(i)(9).ToString()
                        _Dwf_DocumentElement.MaxLength = Elements(i)(10).ToString()
                        _Dwf_DocumentElement.Title = Elements(i)(11).ToString()
                        _Dwf_DocumentElement.Align = Elements(i)(12).ToString()
                        _Dwf_DocumentElement.Dir = Elements(i)(13).ToString()
                        _Dwf_DocumentElement.Value = Elements(i)(14).ToString()
                        If Elements(i)(15).ToString() = "" Then
                            _Dwf_DocumentElement.ListID = Nothing
                        Else
                            _Dwf_DocumentElement.ListID = Convert.ToInt32(Elements(i)(15).ToString())
                        End If
                        _Dwf_DocumentElement.FontSize = Convert.ToInt32(Elements(i)(17).ToString())
                        _Dwf_DocumentElement.IsMainCode = Convert.ToBoolean(Elements(i)(18).ToString())
                        _Dwf_DocumentElement.IsRequired = Convert.ToBoolean(Elements(i)(19).ToString())
                        If Elements(i)(20).ToString() = "" Then
                            _Dwf_DocumentElement.SearchID = Nothing
                        Else
                            _Dwf_DocumentElement.SearchID = Convert.ToInt32(Elements(i)(20).ToString())
                        End If
                        _Dwf_DocumentElement.KeyColumn = Elements(i)(21).ToString()
                        _Dwf_DocumentElement.Remarks = Elements(i)(22).ToString()
                        _Dwf_DocumentElement.KeyTable = Elements(i)(23).ToString()
                        _Dwf_DocumentElement.KeyRelated = Elements(i)(24).ToString()
                        _Dwf_DocumentElement.CtrlFormat = Elements(i)(25).ToString()
                        _Dwf_DocumentElement.Date_Calendar = Elements(i)(26).ToString()
                        _Dwf_DocumentElement.ZoomingForm = Elements(i)(27).ToString()
                        _Dwf_DocumentElement.IsEnabled = Convert.ToBoolean(Elements(i)(28).ToString())
                        _Dwf_DocumentElement.FriendlyName = Elements(i)(29).ToString()
                        _Dwf_DocumentElement.RegUserID = ProfileCls.RetUserID()
                        _Dwf_DocumentElement.RegComputerID = 0
                        _Dwf_DocumentElement.RegDate = DateTime.Now
                        _Dwf_DocumentElement.CancelDate = Nothing
                        _Dwf_DocumentElement.Save()
                        Dim StrMCode As String = IIf(_Dwf_DocumentElement.IsMainCode = True, "1", "0")
                        Str = Str & "|" & Elements(i)(0).ToString() & "," & Elements(i)(1).ToString() & "," & Elements(i)(11).ToString() & "," & StrMCode
                    End If
                End If
            Next
            BuildDataTable(Str, DocumentID)
        Catch ex As Exception
            Return 0
        End Try
        Return 1
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Sub BuildDataTable(ByVal Col As String, ByVal DocumentID As String)
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        _Dwf_Document.find("ID = " & DocumentID)
        Dim ColumnsData As String() = Col.Split("|")
        Dim DTOldData As New DataTable()
        Dim strcommand As String = "select table_name from INFORMATION_SCHEMA.Tables where TABLE_TYPE = 'BASE TABLE' and table_name = 'CF_" & _Dwf_Document.Code & "'"
        If Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Dwf_Document.ConnectionString, CommandType.Text, strcommand).Tables(0).Rows.Count = 1 Then
            strcommand = "drop table CF_" & _Dwf_Document.Code
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(_Dwf_Document.ConnectionString, CommandType.Text, strcommand)
        End If
        Dim Buildcommand As String = ""
        Buildcommand = Buildcommand & "CREATE TABLE [CF_" & _Dwf_Document.Code & "](" & Environment.NewLine & _
                                      "[ID] [bigint] primary key IDENTITY(1,1) NOT NULL," & Environment.NewLine
        For i As Integer = 0 To ColumnsData.Length - 1
            If ColumnsData(i) <> "" Then
                Dim column As String() = ColumnsData(i).Split(",")
                If column(3).ToString() = 1 Then
                    Dim columnname = ""
                    Dim columntype = ""
                    columnname = "[" & column(0).ToString() & "]"
                    If column(1).ToString = "TextBox" Then
                        columntype = "[nvarchar](255) NULL,"
                        Buildcommand = Buildcommand & columnname & " " & columntype & Environment.NewLine
                    ElseIf column(1).ToString = "TextArea" Then
                        columntype = "[nvarchar](1024) NULL,"
                        Buildcommand = Buildcommand & columnname & " " & columntype & Environment.NewLine
                    ElseIf column(1).ToString = "ComboBox" Then
                        columntype = "[int] NULL,"
                        Buildcommand = Buildcommand & columnname & " " & columntype & Environment.NewLine
                    ElseIf column(1).ToString = "CheckBox" Then
                        columntype = "[bit] NULL,"
                        Buildcommand = Buildcommand & columnname & " " & columntype & Environment.NewLine
                    ElseIf column(1).ToString = "Radio" Then
                        columntype = "[bit] NULL,"
                        Buildcommand = Buildcommand & columnname & " " & columntype & Environment.NewLine
                    ElseIf column(1).ToString = "Image" Then
                        columntype = "[image] NULL,"
                        Buildcommand = Buildcommand & columnname & " " & columntype & Environment.NewLine
                        Buildcommand = Buildcommand & "[" & column(0).ToString() & "_Url]" & " " & "[nvarchar](255) NULL," & Environment.NewLine
                    End If
                End If
            End If
        Next i
        For i As Integer = 0 To ColumnsData.Length - 1
            If ColumnsData(i) <> "" Then
                Dim column As String() = ColumnsData(i).Split(",")
                If column(3).ToString() = 0 Then
                    Dim columnname = ""
                    Dim columntype = ""
                    columnname = "[" & column(0).ToString() & "]"
                    If column(1).ToString = "TextBox" Then
                        columntype = "[nvarchar](255) NULL,"
                        Buildcommand = Buildcommand & columnname & " " & columntype & Environment.NewLine
                    ElseIf column(1).ToString = "TextArea" Then
                        columntype = "[nvarchar](1024) NULL,"
                        Buildcommand = Buildcommand & columnname & " " & columntype & Environment.NewLine
                    ElseIf column(1).ToString = "ComboBox" Then
                        columntype = "[int] NULL,"
                        Buildcommand = Buildcommand & columnname & " " & columntype & Environment.NewLine
                    ElseIf column(1).ToString = "CheckBox" Then
                        columntype = "[bit] NULL,"
                        Buildcommand = Buildcommand & columnname & " " & columntype & Environment.NewLine
                    ElseIf column(1).ToString = "Radio" Then
                        columntype = "[bit] NULL,"
                        Buildcommand = Buildcommand & columnname & " " & columntype & Environment.NewLine
                    ElseIf column(1).ToString = "Image" Then
                        columntype = "[image] NULL,"
                        Buildcommand = Buildcommand & columnname & " " & columntype & Environment.NewLine
                        Buildcommand = Buildcommand & "[" & column(0).ToString() & "_Url]" & " " & "[nvarchar](255) NULL," & Environment.NewLine
                    End If
                End If
            End If
        Next i
        Buildcommand = Buildcommand & "[Stage] [nvarchar](255) NULL," & Environment.NewLine & _
                                      "[Remarks] [nvarchar](1024) NULL," & Environment.NewLine & _
                                      "[RegUserID] [int] NULL," & Environment.NewLine & _
                                      "[RegDate] [smalldatetime] NOT NULL," & Environment.NewLine & _
                                      "[CancelDate] [smalldatetime] NULL)" & Environment.NewLine & _
                                      "ALTER TABLE [CF_" & _Dwf_Document.Code & "] ADD  CONSTRAINT [DF_CF_" & _Dwf_Document.Code & "_RegDate_" & DateTime.Now.ToString("ddMMyyyyHHmmss") & "]  DEFAULT (getdate()) FOR [RegDate]"
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(_Dwf_Document.ConnectionString, CommandType.Text, Buildcommand)
        If _Dwf_Document.RegComputerID > 0 Then
            Dim _Sys_Form01 As New ClsSys_Forms(New System.Web.UI.Page)
            _Sys_Form01.Find("Code = 'Cus_" & _Dwf_Document.ID & "'")
            Dim DT As DataTable = _Sys_Form01.DataSet.Tables(0)
            If DT.Rows.Count > 0 Then
                _Sys_Form01.Find("ID = " & DT.Rows(0)("ID"))
                _Sys_Form01.Code = "Cus_" & _Dwf_Document.ID
                _Sys_Form01.EngName = _Dwf_Document.EngName
                _Sys_Form01.ArbName = _Dwf_Document.ArbName
                _Sys_Form01.ModuleID = Convert.ToInt32(ConfigurationManager.AppSettings("WFModule").ToString())
                _Sys_Form01.MainID = Convert.ToInt32(ConfigurationManager.AppSettings("WFModule").ToString())
                _Sys_Form01.ImageUrl = "../../../Common/Images/Pages.png"
                _Sys_Form01.LinkUrl = "Pages/DocumentWF/DocumentView.aspx?DocumentID=" & _Dwf_Document.ID & "&FrmID=" & _Sys_Form01.ID
                _Sys_Form01.LinkTarget = "_Blank"
                _Sys_Form01.RegDate = DateTime.Now
                _Sys_Form01.Update("ID = " & DT.Rows(0)("ID"))

                Dim _Sys_Menu As New Clssys_Menus(New System.Web.UI.Page)
                Dim Strcommand1 As String = "delete from sys_Menus where FormID = " & _Sys_Form01.ID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(_Sys_Menu.ConnectionString, System.Data.CommandType.Text, Strcommand1)

                _Sys_Menu.Code = "Cus_" & _Dwf_Document.ID
                _Sys_Menu.EngName = _Dwf_Document.EngName
                _Sys_Menu.ArbName = _Dwf_Document.ArbName
                _Sys_Menu.ParentID = _Dwf_Document.RegComputerID
                _Sys_Menu.Shortcut = "CF"
                _Sys_Menu.Rank = 0
                _Sys_Menu.FormID = _Sys_Form01.ID
                _Sys_Menu.ViewFormID = _Sys_Form01.ID
                _Sys_Menu.IsHide = False
                _Sys_Menu.ViewType = 1
                _Sys_Menu.RegDate = DateTime.Now
                _Sys_Menu.Save()
            Else
                _Sys_Form01.Code = "Cus_" & _Dwf_Document.ID
                _Sys_Form01.EngName = _Dwf_Document.EngName
                _Sys_Form01.ArbName = _Dwf_Document.ArbName
                _Sys_Form01.ModuleID = Convert.ToInt32(ConfigurationManager.AppSettings("WFModule").ToString())
                _Sys_Form01.MainID = Convert.ToInt32(ConfigurationManager.AppSettings("WFModule").ToString())
                _Sys_Form01.ImageUrl = "../../../Common/Images/Pages.png"
                _Sys_Form01.LinkTarget = "_Blank"
                _Sys_Form01.RegDate = DateTime.Now
                _Sys_Form01.Save()

                Dim _Sys_Menu As New Clssys_Menus(New System.Web.UI.Page)
                _Sys_Menu.Code = "Cus_" & _Dwf_Document.ID
                _Sys_Menu.EngName = _Dwf_Document.EngName
                _Sys_Menu.ArbName = _Dwf_Document.ArbName
                _Sys_Menu.ParentID = _Dwf_Document.RegComputerID
                _Sys_Menu.Shortcut = "CF"
                _Sys_Menu.Rank = 0
                _Sys_Menu.FormID = _Sys_Form01.ID
                _Sys_Menu.ViewFormID = _Sys_Form01.ID
                _Sys_Menu.IsHide = False
                _Sys_Menu.ViewType = 1
                _Sys_Menu.RegDate = DateTime.Now
                _Sys_Menu.Save()

                _Sys_Form01.LinkUrl = "Pages/DocumentWF/DocumentView.aspx?DocumentID=" & _Dwf_Document.ID & "&FrmID=" & _Sys_Form01.ID
                _Sys_Form01.Update("code = 'Cus_" & _Dwf_Document.ID & "'")
            End If
            Dim _Sys_FormsPermission As New ClsSys_FormsPermissions(New System.Web.UI.Page)
            Dim Strdel As String = "delete from sys_FormsPermissions where FormID = " & _Sys_Form01.ID
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(_Sys_FormsPermission.ConnectionString, System.Data.CommandType.Text, Strdel)

            _Sys_FormsPermission = New ClsSys_FormsPermissions(New System.Web.UI.Page)
            _Sys_FormsPermission.FormID = _Sys_Form01.ID
            _Sys_FormsPermission.UserID = ProfileCls.RetUserID()
            _Sys_FormsPermission.AllowView = True
            _Sys_FormsPermission.AllowAdd = True
            _Sys_FormsPermission.AllowEdit = True
            _Sys_FormsPermission.AllowDelete = True
            _Sys_FormsPermission.AllowPrint = True
            _Sys_FormsPermission.RegDate = DateTime.Now
            _Sys_FormsPermission.RegUserID = ProfileCls.RetUserID()
            _Sys_FormsPermission.Save()
        End If
    End Sub
    <WebMethod(EnableSession:=True)> _
    Public Function ClearData(ByVal DocumentID As String) As Integer
        Dim Strcommand1 As String = "delete from Dwf_DocumentElements where DocumentID = " & DocumentID
        Dim Cls As New Clssys_Announces(New System.Web.UI.Page)
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(Cls.ConnectionString, System.Data.CommandType.Text, Strcommand1)
        Return 0
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function SaveStages(ByVal Elements As Object(), ByVal DocumentID As String, ByVal StageElements As Object(), ByVal StagePeople As Object(), ByVal StageActions As Object(), ByVal InitInfo As Object(), ByVal StageActionNotify As Object(), ByVal StageActionPlugin As Object(), ByVal StageActionPluginPar As Object()) As Integer
        Try
            Dim Str As String = ""
            ClearStages(DocumentID)
            For i As Integer = 0 To Elements.Length - 1
                If Elements(i) IsNot Nothing Then
                    If Elements(i)(8).ToString() = "0" Then
                        Dim _Dwf_DocumentStage As New ClsDwf_DocumentStages(New System.Web.UI.Page)
                        _Dwf_DocumentStage.Code = Elements(i)(0).ToString()
                        _Dwf_DocumentStage.DocumentID = Convert.ToInt32(DocumentID)
                        _Dwf_DocumentStage.ElementType = Elements(i)(1).ToString()
                        _Dwf_DocumentStage.Loc_Top = Elements(i)(2).ToString()
                        _Dwf_DocumentStage.Loc_Left = Elements(i)(3).ToString()
                        _Dwf_DocumentStage.Rank = Elements(i)(4).ToString()
                        _Dwf_DocumentStage.ArComment = Elements(i)(5).ToString()
                        _Dwf_DocumentStage.EnComment = Elements(i)(11).ToString()
                        _Dwf_DocumentStage.ConnectorIn = Elements(i)(6).ToString()
                        _Dwf_DocumentStage.ConnectorOut = Elements(i)(7).ToString()
                        _Dwf_DocumentStage.WaitForAll = Convert.ToBoolean(Elements(i)(9).ToString())
                        _Dwf_DocumentStage.EscalationTime = Convert.ToInt32(Elements(i)(10).ToString())
                        _Dwf_DocumentStage.RegUserID = ProfileCls.RetUserID()
                        _Dwf_DocumentStage.RegComputerID = 0
                        _Dwf_DocumentStage.RegDate = DateTime.Now
                        _Dwf_DocumentStage.CancelDate = Nothing
                        _Dwf_DocumentStage.Save()
                        For x1 As Integer = 0 To StageElements.Length - 1
                            If StageElements(x1) IsNot Nothing Then
                                If StageElements(x1)(0).ToString() = _Dwf_DocumentStage.Code Then
                                    Dim _Dwf_DocumentStageElement As New ClsDwf_DocumentStageElements(New System.Web.UI.Page)
                                    _Dwf_DocumentStageElement.DocumentID = _Dwf_DocumentStage.DocumentID
                                    _Dwf_DocumentStageElement.StageCode = _Dwf_DocumentStage.Code
                                    _Dwf_DocumentStageElement.ElementCode = StageElements(x1)(1).ToString()
                                    _Dwf_DocumentStageElement.IsHide = IIf(StageElements(x1)(2) = 1, 1, 0)
                                    _Dwf_DocumentStageElement.IsDisabled = IIf(StageElements(x1)(3) = 1, 1, 0)
                                    _Dwf_DocumentStageElement.RegUserID = ProfileCls.RetUserID()
                                    _Dwf_DocumentStageElement.RegComputerID = 0
                                    _Dwf_DocumentStageElement.RegDate = DateTime.Now
                                    _Dwf_DocumentStageElement.CancelDate = Nothing
                                    _Dwf_DocumentStageElement.Save()
                                End If
                            End If
                        Next

                        For x2 As Integer = 0 To StagePeople.Length - 1
                            If StagePeople(x2) IsNot Nothing Then
                                If StagePeople(x2)(4).ToString() = 1 And StagePeople(x2)(0).ToString() = _Dwf_DocumentStage.Code Then
                                    Dim _Dwf_DocumentStagePeople As New ClsDwf_DocumentStagePeoples(New System.Web.UI.Page)
                                    _Dwf_DocumentStagePeople.DocumentID = _Dwf_DocumentStage.DocumentID
                                    _Dwf_DocumentStagePeople.StageCode = _Dwf_DocumentStage.Code
                                    _Dwf_DocumentStagePeople.PeopleType = StagePeople(x2)(1)
                                    _Dwf_DocumentStagePeople.PositionID = StagePeople(x2)(2)
                                    _Dwf_DocumentStagePeople.EmployeeID = StagePeople(x2)(3)
                                    _Dwf_DocumentStagePeople.RegUserID = ProfileCls.RetUserID()
                                    _Dwf_DocumentStagePeople.RegComputerID = 0
                                    _Dwf_DocumentStagePeople.RegDate = DateTime.Now
                                    _Dwf_DocumentStagePeople.CancelDate = Nothing
                                    _Dwf_DocumentStagePeople.Save()
                                End If
                            End If
                        Next
                        For x3 As Integer = 0 To StageActions.Length - 1
                            If StageActions(x3) IsNot Nothing Then
                                If StageActions(x3)(2).ToString() = 1 And StageActions(x3)(0).ToString() = _Dwf_DocumentStage.Code Then
                                    Dim _Dwf_DocumentStageAction As New ClsDwf_DocumentStageActions(New System.Web.UI.Page)
                                    _Dwf_DocumentStageAction.DocumentID = _Dwf_DocumentStage.DocumentID
                                    _Dwf_DocumentStageAction.StageCode = _Dwf_DocumentStage.Code
                                    _Dwf_DocumentStageAction.ActionID = StageActions(x3)(1).ToString()
                                    _Dwf_DocumentStageAction.RegUserID = ProfileCls.RetUserID()
                                    _Dwf_DocumentStageAction.RegComputerID = 0
                                    _Dwf_DocumentStageAction.RegDate = DateTime.Now
                                    _Dwf_DocumentStageAction.CancelDate = Nothing
                                    _Dwf_DocumentStageAction.Save()
                                End If
                            End If
                        Next
                        For x5 As Integer = 0 To StageActionNotify.Length - 1
                            If StageActionNotify(x5) IsNot Nothing Then
                                If StageActionNotify(x5)(7).ToString() = 1 And StageActionNotify(x5)(0).ToString() = _Dwf_DocumentStage.Code Then
                                    Dim _Dwf_DocumentStageActionNotify As New ClsDwf_DocumentStageActionNotify(New System.Web.UI.Page)
                                    _Dwf_DocumentStageActionNotify.DocumentID = _Dwf_DocumentStage.DocumentID
                                    _Dwf_DocumentStageActionNotify.StageCode = _Dwf_DocumentStage.Code
                                    _Dwf_DocumentStageActionNotify.ActionID = StageActionNotify(x5)(1).ToString()
                                    _Dwf_DocumentStageActionNotify.RegComputerID = StageActionNotify(x5)(2).ToString()
                                    _Dwf_DocumentStageActionNotify.NotifStage = StageActionNotify(x5)(3)
                                    _Dwf_DocumentStageActionNotify.NotifyPosition = StageActionNotify(x5)(4)
                                    _Dwf_DocumentStageActionNotify.NotifyEmployee = StageActionNotify(x5)(5)
                                    _Dwf_DocumentStageActionNotify.Notification = StageActionNotify(x5)(6)
                                    _Dwf_DocumentStageActionNotify.RegUserID = ProfileCls.RetUserID()
                                    _Dwf_DocumentStageActionNotify.RegDate = DateTime.Now
                                    _Dwf_DocumentStageActionNotify.CancelDate = Nothing
                                    _Dwf_DocumentStageActionNotify.Save()
                                End If
                            End If
                        Next
                        Dim StageActionPluginID As Integer = 0
                        For x6 As Integer = 0 To StageActionPlugin.Length - 1
                            If StageActionPlugin(x6) IsNot Nothing Then
                                If StageActionPlugin(x6)(3).ToString() = 1 And StageActionPlugin(x6)(0).ToString() = _Dwf_DocumentStage.Code Then
                                    Dim _Dwf_DocumentStageActionPlugin As New ClsDwf_DocumentStageActionPlugin(New System.Web.UI.Page)
                                    _Dwf_DocumentStageActionPlugin.DocumentID = _Dwf_DocumentStage.DocumentID
                                    _Dwf_DocumentStageActionPlugin.StageCode = _Dwf_DocumentStage.Code
                                    _Dwf_DocumentStageActionPlugin.ActionID = StageActionPlugin(x6)(1).ToString()
                                    _Dwf_DocumentStageActionPlugin.EventPluginID = StageActionPlugin(x6)(2)
                                    _Dwf_DocumentStageActionPlugin.RegUserID = ProfileCls.RetUserID()
                                    _Dwf_DocumentStageActionPlugin.RegComputerID = 0
                                    _Dwf_DocumentStageActionPlugin.RegDate = DateTime.Now
                                    _Dwf_DocumentStageActionPlugin.CancelDate = Nothing
                                    StageActionPluginID = _Dwf_DocumentStageActionPlugin.Save()
                                End If
                            End If
                        Next
                        For x7 As Integer = 0 To StageActionPluginPar.Length - 1
                            If StageActionPluginPar(x7) IsNot Nothing Then
                                If StageActionPluginPar(x7)(5).ToString() = 1 And StageActionPluginPar(x7)(0).ToString() = _Dwf_DocumentStage.Code Then
                                    Dim _Dwf_DocumentStageActionPluginPar As New ClsDwf_DocumentStageActionPluginPar(New System.Web.UI.Page)
                                    _Dwf_DocumentStageActionPluginPar.ActionPluginID = StageActionPluginID
                                    _Dwf_DocumentStageActionPluginPar.DocumentID = _Dwf_DocumentStage.DocumentID
                                    _Dwf_DocumentStageActionPluginPar.StageCode = _Dwf_DocumentStage.Code
                                    _Dwf_DocumentStageActionPluginPar.ActionID = StageActionPluginPar(x7)(1).ToString()
                                    _Dwf_DocumentStageActionPluginPar.EventPluginID = StageActionPluginPar(x7)(2)
                                    _Dwf_DocumentStageActionPluginPar.EventPluginParameterID = StageActionPluginPar(x7)(3)
                                    _Dwf_DocumentStageActionPluginPar.ElementCode = StageActionPluginPar(x7)(4)
                                    _Dwf_DocumentStageActionPluginPar.RegUserID = ProfileCls.RetUserID()
                                    _Dwf_DocumentStageActionPluginPar.RegComputerID = 0
                                    _Dwf_DocumentStageActionPluginPar.RegDate = DateTime.Now
                                    _Dwf_DocumentStageActionPluginPar.CancelDate = Nothing
                                    _Dwf_DocumentStageActionPluginPar.Save()
                                End If
                            End If
                        Next
                    End If
                End If
            Next
            For x4 As Integer = 0 To InitInfo.Length - 1
                If InitInfo(x4) IsNot Nothing Then
                    Dim _Dwf_DocumentStageInitInfo As New ClsDwf_DocumentStageInitInfo(New System.Web.UI.Page)
                    _Dwf_DocumentStageInitInfo.DocumentID = Convert.ToInt32(DocumentID)
                    _Dwf_DocumentStageInitInfo.ElementCode = InitInfo(x4)(0).ToString()
                    _Dwf_DocumentStageInitInfo.RelColumn = InitInfo(x4)(1).ToString()
                    _Dwf_DocumentStageInitInfo.RegUserID = ProfileCls.RetUserID()
                    _Dwf_DocumentStageInitInfo.RegComputerID = 0
                    _Dwf_DocumentStageInitInfo.RegDate = DateTime.Now
                    _Dwf_DocumentStageInitInfo.CancelDate = Nothing
                    _Dwf_DocumentStageInitInfo.Save()
                End If
            Next
        Catch ex As Exception
            Return 0
        End Try
        Return 1
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function LoadControls(ByVal DocumentID As String) As String
        Dim _Dwf_DocumentElement As New ClsDwf_DocumentElements(New System.Web.UI.Page)
        _Dwf_DocumentElement.Find("DocumentID = " & Convert.ToInt32(DocumentID))
        Dim DT As New DataTable()
        Dim Str As String = ""
        DT = _Dwf_DocumentElement.DataSet.Tables(0)
        For i As Integer = 0 To DT.Rows.Count - 1
            _Dwf_DocumentElement.Find("ID = " & DT.Rows(i)("ID"))
            Str = Str & _Dwf_DocumentElement.Code & "," & _
                        _Dwf_DocumentElement.ElementType & "," & _
                        _Dwf_DocumentElement.FColor & "," & _
                        _Dwf_DocumentElement.BColor & "," & _
                        _Dwf_DocumentElement.Loc_Top & "," & _
                        _Dwf_DocumentElement.Loc_Left & "," & _
                        _Dwf_DocumentElement.Size_Width & "," & _
                        _Dwf_DocumentElement.Size_Hight & "," & _
                        _Dwf_DocumentElement.TabIndex & "," & _
                        _Dwf_DocumentElement.Image_Src & "," & _
                        _Dwf_DocumentElement.MaxLength & "," & _
                        _Dwf_DocumentElement.Title & "," & _
                        _Dwf_DocumentElement.Align & "," & _
                        _Dwf_DocumentElement.Dir & "," & _
                        _Dwf_DocumentElement.Value & "," & _
                        _Dwf_DocumentElement.ListID & "," & _
                        0 & "," & _
                        _Dwf_DocumentElement.FontSize & "," & _
                        _Dwf_DocumentElement.IsMainCode & "," & _
                        _Dwf_DocumentElement.IsRequired & "," & _
                        _Dwf_DocumentElement.SearchID & "," & _
                        _Dwf_DocumentElement.KeyColumn & "," & _
                        _Dwf_DocumentElement.Remarks & "," & _
                        _Dwf_DocumentElement.ID & "," & _
                        _Dwf_DocumentElement.KeyTable & "," & _
                        _Dwf_DocumentElement.KeyRelated & "," & _
                        _Dwf_DocumentElement.CtrlFormat & "," & _
                        _Dwf_DocumentElement.Date_Calendar & "," & _
                        _Dwf_DocumentElement.ZoomingForm & "," & _
                        _Dwf_DocumentElement.IsEnabled & "," & _
                        _Dwf_DocumentElement.FriendlyName & ","
            If _Dwf_DocumentElement.ElementType = "ComboBox" Then
                Dim _Dwf_ListItem As New ClsDwf_ListItems(New System.Web.UI.Page)
                _Dwf_ListItem.Find("ID = " & _Dwf_DocumentElement.ListID)
                'todo to add function to select from list
                If _Dwf_ListItem.ID <> Nothing Then
                    Dim ListDatatable As New DataTable
                    Dim ListString As String = ""

                    If _Dwf_ListItem.Type = 1 Then
                        Dim StrCommand As String = "select * FROM " & _Dwf_ListItem.DataSource & " ORDER BY " & _Dwf_ListItem.ValueMember & " ASC"
                        ListDatatable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Dwf_DocumentElement.ConnectionString, System.Data.CommandType.Text, StrCommand).Tables(0)
                    ElseIf _Dwf_ListItem.Type = 2 Then
                        ListDatatable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Dwf_DocumentElement.ConnectionString, System.Data.CommandType.StoredProcedure, _Dwf_ListItem.DataSource).Tables(0)
                    ElseIf _Dwf_ListItem.Type = 3 Then
                        Dim _Dwf_ListItemsValue As New ClsDwf_ListItemsValues(New System.Web.UI.Page)
                        _Dwf_ListItemsValue.Find("ListItemID = " & _Dwf_ListItem.ID)
                        Dim LisValueDatatable As New DataTable
                        LisValueDatatable = _Dwf_ListItemsValue.DataSet.Tables(0)
                        For x As Integer = 0 To LisValueDatatable.Rows.Count - 1
                            _Dwf_ListItemsValue = New ClsDwf_ListItemsValues(New System.Web.UI.Page)
                            _Dwf_ListItemsValue.Find("ID = " & LisValueDatatable.Rows(x)("ID"))
                            ListString = ListString & _Dwf_ListItemsValue.ID.ToString() & ";" & _Dwf_ListItemsValue.ArbName.ToString() & ";" & _Dwf_ListItemsValue.EngName.ToString()
                            If x < LisValueDatatable.Rows.Count - 1 Then
                                ListString = ListString & "$"
                            End If
                        Next x
                    End If
                    For x As Integer = 0 To ListDatatable.Rows.Count - 1
                        ListString = ListString & ListDatatable.Rows(x)(_Dwf_ListItem.ValueMember) & ";" & ListDatatable.Rows(x)(_Dwf_ListItem.Ar_DisplayMember) & ";" & ListDatatable.Rows(x)(_Dwf_ListItem.En_DisplayMember)
                        If x < ListDatatable.Rows.Count - 1 Then
                            ListString = ListString & "$"
                        End If
                    Next x
                    Str = Str & "*" & ListString
                End If
            End If
            If i < DT.Rows.Count - 1 Then
                Str = Str & "|"
            End If
        Next i
        Dim ln As Integer = Str.Length
        Return Str
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function LoadStages(ByVal DocumentID As String) As String
        Dim _Dwf_DocumentStage As New ClsDwf_DocumentStages(New System.Web.UI.Page)
        _Dwf_DocumentStage.Find("DocumentID = " & Convert.ToInt32(DocumentID))
        Dim DT As New DataTable()
        Dim Str As String = ""
        DT = _Dwf_DocumentStage.DataSet.Tables(0)
        For i As Integer = 0 To DT.Rows.Count - 1
            _Dwf_DocumentStage = New ClsDwf_DocumentStages(New System.Web.UI.Page)
            _Dwf_DocumentStage.Find("ID = " & DT.Rows(i)("ID"))
            Str = Str & _Dwf_DocumentStage.Code & "," & _
                        _Dwf_DocumentStage.ElementType & "," & _
                        _Dwf_DocumentStage.Loc_Top & "," & _
                        _Dwf_DocumentStage.Loc_Left & "," & _
                        _Dwf_DocumentStage.Rank & "," & _
                        _Dwf_DocumentStage.ArComment & "," & _
                        _Dwf_DocumentStage.ConnectorIn & "," & _
                        _Dwf_DocumentStage.ConnectorOut & "," & _
                        0 & "," & _
                        _Dwf_DocumentStage.WaitForAll & "," & _
                        _Dwf_DocumentStage.EscalationTime & "," & _
                        _Dwf_DocumentStage.EnComment & "," & _
                        _Dwf_DocumentStage.ID & ","
            If i < DT.Rows.Count - 1 Then
                Str = Str & "|"
            End If
        Next i
        Return Str
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function ClearStages(ByVal DocumentID As String) As Integer
        Dim Strcommand1 As String = "delete from Dwf_DocumentStages where DocumentID = " & DocumentID
        Strcommand1 = Strcommand1 & Environment.NewLine & "delete from Dwf_DocumentStageElements where DocumentID = " & DocumentID
        Strcommand1 = Strcommand1 & Environment.NewLine & "delete from Dwf_DocumentStagePeoples where DocumentID = " & DocumentID
        Strcommand1 = Strcommand1 & Environment.NewLine & "delete from Dwf_DocumentStageActionNotify where DocumentID = " & DocumentID
        Strcommand1 = Strcommand1 & Environment.NewLine & "delete from Dwf_DocumentStageActionPluginPar where DocumentID = " & DocumentID
        Strcommand1 = Strcommand1 & Environment.NewLine & "delete from Dwf_DocumentStageActionPlugin where DocumentID = " & DocumentID
        Strcommand1 = Strcommand1 & Environment.NewLine & "delete from Dwf_DocumentStageActions where DocumentID = " & DocumentID
        Strcommand1 = Strcommand1 & Environment.NewLine & "delete from Dwf_DocumentStageInitInfo where DocumentID = " & DocumentID
        Dim Cls As New Clssys_Announces(New System.Web.UI.Page)
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(Cls.ConnectionString, System.Data.CommandType.Text, Strcommand1)
        Return 1
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function SaveData(ByVal DocumentID As String, ByVal StrArray As String) As Integer
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        _Dwf_Document.Find("ID = " & Convert.ToInt32(DocumentID))
        Dim ID As Int32 = 0
        Dim ColString As String = ""
        Dim ValueString As String = ""
        ColString = ColString & "insert into " & "CF_" & _Dwf_Document.Code & "("
        ValueString = ValueString & " values ("
        Dim ColumnsData As String() = StrArray.Split("|")
        For i As Integer = 0 To ColumnsData.Length - 1
            If ColumnsData(i) <> "" Then
                Dim column As String() = ColumnsData(i).Split(";")
                Dim columnname As String = ""
                Dim ColumnValue As String = ""
                columnname = column(0).ToString()
                ColumnValue = column(2).ToString()
                If column(1).ToString = "TextBox" Then
                    ColString = ColString & "[" & columnname & "]"
                    ValueString = ValueString & "'" & ColumnValue & "'"
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                        ValueString = ValueString & ","
                    End If
                ElseIf column(1).ToString = "TextArea" Then
                    ColString = ColString & "[" & columnname & "]"
                    ValueString = ValueString & "'" & ColumnValue & "'"
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                        ValueString = ValueString & ","
                    End If
                ElseIf column(1).ToString = "ComboBox" Then
                    If (ColumnValue = "") Then
                        ColumnValue = "null"
                    End If
                    ColString = ColString & "[" & columnname & "]"
                    ValueString = ValueString & ColumnValue
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                        ValueString = ValueString & ","
                    End If
                ElseIf column(1).ToString = "CheckBox" Then
                    If (ColumnValue.ToUpper() = "TRUE") Then
                        ColumnValue = "1"
                    Else
                        ColumnValue = "0"
                    End If

                    ColString = ColString & "[" & columnname & "]"
                    ValueString = ValueString & ColumnValue
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                        ValueString = ValueString & ","
                    End If
                ElseIf column(1).ToString = "Radio" Then
                    If (ColumnValue.ToUpper() = "TRUE") Then
                        ColumnValue = "1"
                    Else
                        ColumnValue = "0"
                    End If
                    ColString = ColString & "[" & columnname & "]"
                    ValueString = ValueString & ColumnValue
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                        ValueString = ValueString & ","
                    End If
                End If
            End If
        Next i
        ColString = ColString & ",[RegUserID],[RegDate])"
        ValueString = ValueString & "," & ProfileCls.RetUserID() & ",getdate())"
        ID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(_Dwf_Document.ConnectionString, CommandType.Text, ColString.Replace(",,", ",") & ValueString.Replace(",,", ",") & " " & " select SCOPE_IDENTITY()")
        Return ID
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function CheckStage(ByVal DocumentID As String, ByVal Stage As String, ByVal Action As String, ByVal Code As String, ByVal ForwordTo As String) As String


        Dim PeopleList As String = "0" & ";" & DocumentID & ";" & Action & ";"
        Dim _Dwf_DocumentStages As New ClsDwf_DocumentStages(New System.Web.UI.Page)
        _Dwf_DocumentStages.Find("ID = 0")
        Dim DTDocumentStartStages As DataTable = _Dwf_DocumentStages.DataSet.Tables(0)
        If Stage = "" Then
            _Dwf_DocumentStages = New ClsDwf_DocumentStages(New System.Web.UI.Page)
            _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and ElementType = 'Start'")
            DTDocumentStartStages = _Dwf_DocumentStages.DataSet.Tables(0)
            If DTDocumentStartStages.Rows.Count > 0 Then
                _Dwf_DocumentStages = New ClsDwf_DocumentStages(New System.Web.UI.Page)
                _Dwf_DocumentStages.Find("ID = " & DTDocumentStartStages.Rows(0)("ID"))
                Stage = _Dwf_DocumentStages.ConnectorOut.Replace("cont_", "")
            End If
        End If
        _Dwf_DocumentStages = New ClsDwf_DocumentStages(New System.Web.UI.Page)
        _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and Code = '" & Stage & "'")
        DTDocumentStartStages = _Dwf_DocumentStages.DataSet.Tables(0)
        _Dwf_DocumentStages = New ClsDwf_DocumentStages(New System.Web.UI.Page)
        _Dwf_DocumentStages.Find("ID = " & DTDocumentStartStages.Rows(0)("ID"))
        If RetActionDirection(Action) = Convert.ToInt32(HelperCls.GlobalEnum.ActionMovement.StepForward) Then
            ''If Forward 
            Dim NextStage As String = _Dwf_DocumentStages.ConnectorOut.Replace("cont_", "")
            If NextStage.Contains("menu_sg") = True Then
                Dim _Dwf_DocumentStagePeople As New ClsDwf_DocumentStagePeoples(New System.Web.UI.Page)
                _Dwf_DocumentStagePeople.Find("StageCode = '" & NextStage & "' and DocumentID = " & Convert.ToInt32(DocumentID) & " and PeopleType = " & Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.Employee))
                Dim DocumentStagePeopleDT As DataTable = _Dwf_DocumentStagePeople.DataSet.Tables(0)
                PeopleList = DocumentStagePeopleDT.Rows.Count.ToString() & ";" & DocumentID & ";" & Action & ";"
                Dim _Dwf_DocumentWorkFow As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                _Dwf_DocumentWorkFow.Find("TrnsEntryCode = '" & Code & "' and DocumentID = '" & DocumentID & "' and StageID = '" & RetStageID(DocumentID, NextStage) & "' and Status > 0 and RegComputerID =  '" & Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.Employee) & "' order by RegDate Desc")
                Dim StagePeopleListDT As DataTable = _Dwf_DocumentWorkFow.DataSet.Tables(0)
                If (StagePeopleListDT.Rows.Count >= DocumentStagePeopleDT.Rows.Count) Then
                    For i As Integer = 0 To DocumentStagePeopleDT.Rows.Count - 1
                        PeopleList = PeopleList & StagePeopleListDT.Rows(i)("PeopleID").ToString() & "|"
                    Next
                End If
            End If
        ElseIf RetActionDirection(Action) = Convert.ToInt32(HelperCls.GlobalEnum.ActionMovement.StepBackward) Then
            ''If Backward

            Dim BackStage As String = _Dwf_DocumentStages.ConnectorIn.Replace("cont_", "")
            If BackStage.Contains("menu_sg") = True Then
                Dim _Dwf_DocumentStagePeople As New ClsDwf_DocumentStagePeoples(New System.Web.UI.Page)
                _Dwf_DocumentStagePeople.Find("StageCode = '" & BackStage & "' and DocumentID = " & Convert.ToInt32(DocumentID) & " and PeopleType = " & Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.Employee))
                Dim DocumentStagePeopleDT As DataTable = _Dwf_DocumentStagePeople.DataSet.Tables(0)
                PeopleList = DocumentStagePeopleDT.Rows.Count.ToString() & ";" & DocumentID & ";" & Action & ";"
                Dim _Dwf_DocumentWorkFow As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                _Dwf_DocumentWorkFow.Find("TrnsEntryCode = '" & Code & "' and DocumentID = '" & DocumentID & "' and StageID = '" & RetStageID(DocumentID, BackStage) & "' and Status > 0 and RegComputerID =  '" & Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.Employee) & "' order by RegDate Desc")
                Dim StagePeopleListDT As DataTable = _Dwf_DocumentWorkFow.DataSet.Tables(0)
                If (StagePeopleListDT.Rows.Count >= DocumentStagePeopleDT.Rows.Count) Then
                    For i As Integer = 0 To DocumentStagePeopleDT.Rows.Count - 1
                        PeopleList = PeopleList & StagePeopleListDT.Rows(i)("PeopleID").ToString() & "|"
                    Next
                End If
            End If
        ElseIf RetActionDirection(Action) = Convert.ToInt32(HelperCls.GlobalEnum.ActionMovement.ForwordTo) Then
            ''If ForwordTo
            'Next Stage will be forword
            ' _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and ConnectorIn = 'ForwardFrom'")
            'Dim NextStage As String = _Dwf_DocumentStages.ConnectorIn


            Dim StrCommand As String = "select Code from hrs_employees where Code = '" & ForwordTo & "'"
            Dim EmployeeCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(_Dwf_DocumentStages.ConnectionString, CommandType.Text, StrCommand)
            PeopleList = "0" & ";" & DocumentID & ";" & Action & ";|" & EmployeeCode

        End If
        Return PeopleList
    End Function
    <WebMethod(EnableSession:=True)>Public Function CheckStageNotify(ByVal DocumentID As String, ByVal Stage As String, ByVal Action As String, ByVal People As String) As String
        Dim PeopleList As String = "0" & ";" & DocumentID & ";" & Action & "|" & People
        Dim _Dwf_DocumentStages As New ClsDwf_DocumentStages(New System.Web.UI.Page)
        _Dwf_DocumentStages.Find("ID = 0")
        Dim DTDocumentStartStages As DataTable = _Dwf_DocumentStages.DataSet.Tables(0)
        If Stage = "" Then
            _Dwf_DocumentStages = New ClsDwf_DocumentStages(New System.Web.UI.Page)
            _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and ElementType = 'Start'")
            DTDocumentStartStages = _Dwf_DocumentStages.DataSet.Tables(0)
            If DTDocumentStartStages.Rows.Count > 0 Then
                _Dwf_DocumentStages = New ClsDwf_DocumentStages(New System.Web.UI.Page)
                _Dwf_DocumentStages.Find("ID = " & DTDocumentStartStages.Rows(0)("ID"))
                Stage = _Dwf_DocumentStages.ConnectorOut.Replace("cont_", "")
            End If
        End If
        Dim _Dwf_DocumentStageActionNotify = New ClsDwf_DocumentStageActionNotify(New System.Web.UI.Page)
        _Dwf_DocumentStageActionNotify.Find("DocumentID = " & DocumentID & " and StageCode = '" & Stage & "' and ActionID = " & Action & " and RegComputerID = " & Convert.ToInt32(HelperCls.GlobalEnum.StageActionNotificationTarget.Employee))
        Dim DTDocumentStageActionNotify As DataTable = _Dwf_DocumentStageActionNotify.DataSet.Tables(0)
        If DTDocumentStageActionNotify.Rows.Count > 0 Then
            PeopleList = DTDocumentStageActionNotify.Rows.Count.ToString() & ";" & DocumentID & ";" & Action & "|" & People
        End If
        Return PeopleList
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function SendData(ByVal DocumentID As String, ByVal Stage As String, ByVal Action As String, ByVal StrArray As String, ByVal Code As String, ByVal WF As String, ByVal PeopleList As String, ByVal PeopleNotiList As String) As String

        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        _Dwf_Document.Find("ID = " & Convert.ToInt32(DocumentID))
        Dim ID As Int32 = 0
        Dim ColString As String = ""
        Dim ValueString As String = ""
        ColString = ColString & "insert into " & "CF_" & _Dwf_Document.Code & "("
        ValueString = ValueString & " values ("
        Dim ColumnsData As String() = StrArray.Split("|")
        Dim ForwordToValue As String = ""
        For i As Integer = 0 To ColumnsData.Length - 1
            If ColumnsData(i) <> "" Then
                Dim column As String() = ColumnsData(i).Split(";")
                Dim columnname As String = ""
                Dim ColumnValue As String = ""
                columnname = column(0).ToString()

                ColumnValue = column(2).ToString()
                If columnname = "ForwordTo" Then
                    ForwordToValue = ColumnValue
                End If
                If column(1).ToString = "TextBox" Then
                    ColString = ColString & "[" & columnname & "]"
                    ValueString = ValueString & "'" & ColumnValue & "'"
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                        ValueString = ValueString & ","
                    End If
                ElseIf column(1).ToString = "TextArea" Then
                    ColString = ColString & "[" & columnname & "]"
                    ValueString = ValueString & "'" & ColumnValue & "'"
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                        ValueString = ValueString & ","
                    End If
                ElseIf column(1).ToString = "ComboBox" Then
                    If (ColumnValue = "") Then
                        ColumnValue = "null"
                    End If
                    ColString = ColString & "[" & columnname & "]"
                    ValueString = ValueString & ColumnValue
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                        ValueString = ValueString & ","
                    End If
                ElseIf column(1).ToString = "CheckBox" Then
                    If (ColumnValue.ToUpper() = "TRUE") Then
                        ColumnValue = "1"
                    Else
                        ColumnValue = "0"
                    End If

                    ColString = ColString & "[" & columnname & "]"
                    ValueString = ValueString & ColumnValue
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                        ValueString = ValueString & ","
                    End If
                ElseIf column(1).ToString = "Radio" Then
                    If (ColumnValue.ToUpper() = "TRUE") Then
                        ColumnValue = "1"
                    Else
                        ColumnValue = "0"
                    End If
                    ColString = ColString & "[" & columnname & "]"
                    ValueString = ValueString & ColumnValue
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                        ValueString = ValueString & ","
                    End If
                End If
            End If
        Next i
        If Stage = "" Then
            ColString = ColString & ",[RegUserID],[RegDate])"
            ValueString = ValueString & "," & ProfileCls.RetUserID() & ",getdate())"
        Else
            ColString = ColString & ",[Stage],[RegUserID],[RegDate])"
            ValueString = ValueString & ",'" & Stage & "'," & ProfileCls.RetUserID() & ",getdate())"
        End If
        ID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(_Dwf_Document.ConnectionString, CommandType.Text, ColString.Replace(",,", ",") & ValueString.Replace(",,", ",") & " " & " select SCOPE_IDENTITY()")

        Dim _Dwf_DocumentStages As New ClsDwf_DocumentStages(New System.Web.UI.Page)
        Dim _Dwf_DocumentWorkFow As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
        _Dwf_DocumentStages.Find("ID = 0")
        Dim DTDocumentStartStages As DataTable = _Dwf_DocumentStages.DataSet.Tables(0)
        If ID > 0 Then
            If Stage = "" Then
                _Dwf_DocumentStages = New ClsDwf_DocumentStages(New System.Web.UI.Page)
                _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and ElementType = 'Start'")
                DTDocumentStartStages = _Dwf_DocumentStages.DataSet.Tables(0)
                If DTDocumentStartStages.Rows.Count > 0 Then
                    _Dwf_DocumentStages = New ClsDwf_DocumentStages(New System.Web.UI.Page)
                    _Dwf_DocumentStages.Find("ID = " & DTDocumentStartStages.Rows(0)("ID"))
                    _Dwf_DocumentWorkFow = New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                    _Dwf_DocumentWorkFow.DocumentID = Convert.ToInt32(DocumentID)
                    _Dwf_DocumentWorkFow.StageID = _Dwf_DocumentStages.ID
                    _Dwf_DocumentWorkFow.TrnsEntryCode = Code
                    _Dwf_DocumentWorkFow.RegUserID = ProfileCls.RetUserID()
                    _Dwf_DocumentWorkFow.Status = 1
                    _Dwf_DocumentWorkFow.RegDate = DateTime.Now
                    _Dwf_DocumentWorkFow.CancelDate = Nothing
                    _Dwf_DocumentWorkFow.Save()
                    Stage = _Dwf_DocumentStages.ConnectorOut.Replace("cont_", "")

                    ID = 0
                    ColString = ""
                    ValueString = ""
                    ColString = ColString & "insert into " & "CF_" & _Dwf_Document.Code & "("
                    ValueString = ValueString & " values ("
                    ColumnsData = StrArray.Split("|")
                    For i As Integer = 0 To ColumnsData.Length - 1
                        If ColumnsData(i) <> "" Then
                            Dim column As String() = ColumnsData(i).Split(";")
                            Dim columnname As String = ""
                            Dim ColumnValue As String = ""
                            columnname = column(0).ToString()
                            ColumnValue = column(2).ToString()
                            If column(1).ToString = "TextBox" Then
                                ColString = ColString & "[" & columnname & "]"
                                ValueString = ValueString & "'" & ColumnValue & "'"
                                If i < ColumnsData.Length - 1 Then
                                    ColString = ColString & ","
                                    ValueString = ValueString & ","
                                End If
                            ElseIf column(1).ToString = "TextArea" Then
                                ColString = ColString & "[" & columnname & "]"
                                ValueString = ValueString & "'" & ColumnValue & "'"
                                If i < ColumnsData.Length - 1 Then
                                    ColString = ColString & ","
                                    ValueString = ValueString & ","
                                End If
                            ElseIf column(1).ToString = "ComboBox" Then
                                If (ColumnValue = "") Then
                                    ColumnValue = "null"
                                End If
                                ColString = ColString & "[" & columnname & "]"
                                ValueString = ValueString & ColumnValue
                                If i < ColumnsData.Length - 1 Then
                                    ColString = ColString & ","
                                    ValueString = ValueString & ","
                                End If
                            ElseIf column(1).ToString = "CheckBox" Then
                                If (ColumnValue.ToUpper() = "TRUE") Then
                                    ColumnValue = "1"
                                Else
                                    ColumnValue = "0"
                                End If

                                ColString = ColString & "[" & columnname & "]"
                                ValueString = ValueString & ColumnValue
                                If i < ColumnsData.Length - 1 Then
                                    ColString = ColString & ","
                                    ValueString = ValueString & ","
                                End If
                            ElseIf column(1).ToString = "Radio" Then
                                If (ColumnValue.ToUpper() = "TRUE") Then
                                    ColumnValue = "1"
                                Else
                                    ColumnValue = "0"
                                End If
                                ColString = ColString & "[" & columnname & "]"
                                ValueString = ValueString & ColumnValue
                                If i < ColumnsData.Length - 1 Then
                                    ColString = ColString & ","
                                    ValueString = ValueString & ","
                                End If
                            End If
                        End If
                    Next i
                    ColString = ColString & ",[Stage],[RegUserID],[RegDate])"
                    ValueString = ValueString & ",'" & Stage & "'," & ProfileCls.RetUserID() & ",getdate())"
                    ID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(_Dwf_Document.ConnectionString, CommandType.Text, ColString.Replace(",,", ",") & ValueString.Replace(",,", ",") & " " & " select SCOPE_IDENTITY()")
                End If
                _Dwf_DocumentWorkFow = New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                _Dwf_DocumentWorkFow.DocumentID = Convert.ToInt32(DocumentID)
                _Dwf_DocumentWorkFow.StageID = RetStageID(DocumentID, Stage)
                _Dwf_DocumentWorkFow.PeopleID = RetStagePeople(DocumentID, ProfileCls.RetUserID().ToString())
                _Dwf_DocumentWorkFow.TrnsEntryCode = Code
                _Dwf_DocumentWorkFow.InitPeople = RetStagePeople(DocumentID, ProfileCls.RetUserID().ToString())
                _Dwf_DocumentWorkFow.MessageArabic = RetArabicMessage(_Dwf_DocumentWorkFow.DocumentID, _Dwf_DocumentWorkFow.StageID)
                _Dwf_DocumentWorkFow.MessageEnglish = RetEnglishMessage(_Dwf_DocumentWorkFow.DocumentID, _Dwf_DocumentWorkFow.StageID)
                _Dwf_DocumentWorkFow.RegUserID = ProfileCls.RetUserID()
                _Dwf_DocumentWorkFow.Status = 0
                _Dwf_DocumentWorkFow.RegDate = DateTime.Now
                _Dwf_DocumentWorkFow.CancelDate = Nothing
                WF = _Dwf_DocumentWorkFow.Save()
                If WF > 0 Then
                Else
                    Return Nothing
                End If
            End If
            Dim _Dwf_DocumentStageActionPlugin = New ClsDwf_DocumentStageActionPlugin(New System.Web.UI.Page)
            _Dwf_DocumentStageActionPlugin.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and StageCode = '" & Stage & "' and ActionID = " & Action)
            Dim DTDocumentStageActionPlugin As New DataTable
            DTDocumentStageActionPlugin = _Dwf_DocumentStageActionPlugin.DataSet.Tables(0)
            For plg As Integer = 0 To DTDocumentStageActionPlugin.Rows.Count - 1
                _Dwf_DocumentStageActionPlugin = New ClsDwf_DocumentStageActionPlugin(New System.Web.UI.Page)
                _Dwf_DocumentStageActionPlugin.Find("ID = " & DTDocumentStageActionPlugin.Rows(plg)("ID"))
                Dim _Dwf_DocumentStageActionPluginPar = New ClsDwf_DocumentStageActionPluginPar(New System.Web.UI.Page)
                _Dwf_DocumentStageActionPluginPar.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and StageCode = '" & Stage & "' and ActionID = " & Action & " and EventPluginID = " & _Dwf_DocumentStageActionPlugin.EventPluginID)
                Dim DTDocumentStageActionPluginPar As New DataTable
                DTDocumentStageActionPluginPar = _Dwf_DocumentStageActionPluginPar.DataSet.Tables(0)
                Dim _Dwf_EventPlugins As New ClsDwf_EventPlugins(New System.Web.UI.Page)
                _Dwf_EventPlugins.Find("ID = " & _Dwf_DocumentStageActionPlugin.EventPluginID)

                Dim connection As New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings(0).ToString())
                Dim command As New System.Data.SqlClient.SqlCommand(_Dwf_EventPlugins.Code, connection)
                For plgPar As Integer = 0 To DTDocumentStageActionPluginPar.Rows.Count - 1
                    _Dwf_DocumentStageActionPluginPar = New ClsDwf_DocumentStageActionPluginPar(New System.Web.UI.Page)
                    _Dwf_DocumentStageActionPluginPar.Find("ID = " & DTDocumentStageActionPluginPar.Rows(plgPar)("ID"))
                    For i As Integer = 0 To ColumnsData.Length - 1
                        If ColumnsData(i) <> "" Then
                            Dim column As String() = ColumnsData(i).Split(";")
                            Dim columnname As String = ""
                            Dim ColumnValue As String = ""
                            columnname = column(0).ToString()
                            ColumnValue = column(2).ToString()
                            If _Dwf_DocumentStageActionPluginPar.ElementCode = columnname Then
                                Dim _Dwf_EventPluginParameters As New ClsDwf_EventPluginParameters(New System.Web.UI.Page)
                                _Dwf_EventPluginParameters.Find("ID = " & _Dwf_DocumentStageActionPluginPar.EventPluginParameterID)
                                command.Parameters.Add("@" & _Dwf_EventPluginParameters.Code, SqlDbType.BigInt)
                                command.Parameters("@ID").Value = ColumnValue
                            End If
                        End If
                    Next
                Next
                Try
                    connection.Open()
                    command.CommandType = CommandType.StoredProcedure
                    command.ExecuteNonQuery()
                Catch
                End Try
            Next
            'hhh
            _Dwf_DocumentStages = New ClsDwf_DocumentStages(New System.Web.UI.Page)
            _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and Code = '" & Stage & "'")

            DTDocumentStartStages = _Dwf_DocumentStages.DataSet.Tables(0)
            If DTDocumentStartStages.Rows.Count > 0 Then
                _Dwf_DocumentStages = New ClsDwf_DocumentStages(New System.Web.UI.Page)
                _Dwf_DocumentStages.Find("ID = " & DTDocumentStartStages.Rows(0)("ID"))
                _Dwf_DocumentWorkFow = New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                _Dwf_DocumentWorkFow.Find("ID = " & Convert.ToInt32(WF))
                _Dwf_DocumentWorkFow.ActionID = Convert.ToInt32(Action)
                _Dwf_DocumentWorkFow.Status = 1
                _Dwf_DocumentWorkFow.Update("ID = " & Convert.ToInt32(WF))
                If RetActionDirection(Action) = Convert.ToInt32(HelperCls.GlobalEnum.ActionMovement.StepForward) Then
                    ''If Forward
                    If _Dwf_DocumentStages.WaitForAll = True Then
                        Dim _Dwf_DocumentWorkFowCheck As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                        If _Dwf_DocumentWorkFowCheck.Find("StageID = " & RetStageID(DocumentID, Stage) & " and TrnsEntryCode = '" & Code & "' and Status = 0") Then
                            ''Transfeer To Next Stage
                            Dim NextStage As String = _Dwf_DocumentStages.ConnectorOut.Replace("cont_", "")
                            If NextStage.Contains("menu_sg") = True Then
                                Dim _Dwf_DocumentStagePeople As New ClsDwf_DocumentStagePeoples(New System.Web.UI.Page)
                                _Dwf_DocumentStagePeople.Find("StageCode = '" & NextStage & "' and DocumentID = " & Convert.ToInt32(DocumentID))
                                Dim DocumentStagePeopleDT As DataTable = _Dwf_DocumentStagePeople.DataSet.Tables(0)
                                For Peo As Integer = 0 To DocumentStagePeopleDT.Rows.Count - 1
                                    _Dwf_DocumentStagePeople = New ClsDwf_DocumentStagePeoples(New System.Web.UI.Page)
                                    _Dwf_DocumentStagePeople.Find("ID = " & DocumentStagePeopleDT.Rows(Peo)("ID"))
                                    If _Dwf_DocumentStagePeople.PeopleType = Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.DirectManager) Then
                                        If RetPeopleDirectManager(_Dwf_DocumentWorkFow.InitPeople) <> Nothing Then
                                            Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                            _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                                            _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, NextStage)
                                            _Dwf_DocumentWorkFowNew.PeopleID = RetPeopleDirectManager(_Dwf_DocumentWorkFow.InitPeople)
                                            _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                                            _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                                            _Dwf_DocumentWorkFowNew.MessageArabic = RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                            _Dwf_DocumentWorkFowNew.MessageEnglish = RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                            _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                                            _Dwf_DocumentWorkFowNew.Status = 0
                                            _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                                            _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                                            _Dwf_DocumentWorkFowNew.Save()
                                        End If
                                    ElseIf _Dwf_DocumentStagePeople.PeopleType = Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.Position) Then
                                        For Pos As Integer = 0 To RetPeoplePosition(_Dwf_DocumentStagePeople.PositionID).Rows.Count - 1
                                            Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                            _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                                            _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, NextStage)
                                            _Dwf_DocumentWorkFowNew.PeopleID = RetPeoplePosition(_Dwf_DocumentStagePeople.PositionID).Rows(Pos)(0).ToString()
                                            _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                                            _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                                            _Dwf_DocumentWorkFowNew.MessageArabic = RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                            _Dwf_DocumentWorkFowNew.MessageEnglish = RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                            _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                                            _Dwf_DocumentWorkFowNew.Status = 0
                                            _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                                            _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                                            _Dwf_DocumentWorkFowNew.Save()
                                        Next
                                    End If
                                Next
                                Dim StrPeopleList As String() = PeopleList.Split(";")
                                If StrPeopleList.Length > 1 Then
                                    For i As Integer = 1 To StrPeopleList.Length - 1
                                        Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                        _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                                        _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, NextStage)
                                        _Dwf_DocumentWorkFowNew.PeopleID = Convert.ToInt32(StrPeopleList(i))
                                        _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                                        _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                                        _Dwf_DocumentWorkFowNew.MessageArabic = RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                        _Dwf_DocumentWorkFowNew.MessageEnglish = RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                        _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                                        _Dwf_DocumentWorkFowNew.Status = 0
                                        _Dwf_DocumentWorkFowNew.RegComputerID = Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.Employee)
                                        _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                                        _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                                        _Dwf_DocumentWorkFowNew.Save()
                                    Next
                                End If
                            ElseIf NextStage.Contains("menu_en") = True Then
                                _Dwf_DocumentWorkFow = New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                _Dwf_DocumentWorkFow.DocumentID = Convert.ToInt32(DocumentID)
                                _Dwf_DocumentWorkFow.StageID = RetStageID(DocumentID, NextStage)
                                _Dwf_DocumentWorkFow.TrnsEntryCode = Code
                                _Dwf_DocumentWorkFow.RegUserID = ProfileCls.RetUserID()
                                _Dwf_DocumentWorkFow.Status = 1
                                _Dwf_DocumentWorkFow.RegDate = DateTime.Now
                                _Dwf_DocumentWorkFow.CancelDate = Nothing
                                _Dwf_DocumentWorkFow.Save()
                            End If
                        End If
                    Else
                        Dim _Dwf_DocumentWorkFowCheck As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                        _Dwf_DocumentWorkFowCheck.Find("StageID = " & RetStageID(DocumentID, Stage) & " and TrnsEntryCode = '" & Code & "' and Status = 0")
                        Dim DocumentWorkFowCheckDT As DataTable = _Dwf_DocumentWorkFowCheck.DataSet.Tables(0)
                        For FCH As Integer = 0 To DocumentWorkFowCheckDT.Rows.Count - 1
                            _Dwf_DocumentWorkFowCheck = New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                            _Dwf_DocumentWorkFowCheck.Find("ID = " & DocumentWorkFowCheckDT.Rows(FCH)("ID"))
                            _Dwf_DocumentWorkFowCheck.Status = 2
                            _Dwf_DocumentWorkFowCheck.Update("ID = " & DocumentWorkFowCheckDT.Rows(FCH)("ID"))
                        Next
                        Dim NextStage As String = _Dwf_DocumentStages.ConnectorOut.Replace("cont_", "")
                        If NextStage.Contains("menu_sg") = True Then
                            Dim _Dwf_DocumentStagePeople As New ClsDwf_DocumentStagePeoples(New System.Web.UI.Page)
                            _Dwf_DocumentStagePeople.Find("StageCode = '" & NextStage & "' and DocumentID = " & Convert.ToInt32(DocumentID))
                            Dim DocumentStagePeopleDT As DataTable = _Dwf_DocumentStagePeople.DataSet.Tables(0)
                            For Peo As Integer = 0 To DocumentStagePeopleDT.Rows.Count - 1
                                _Dwf_DocumentStagePeople = New ClsDwf_DocumentStagePeoples(New System.Web.UI.Page)
                                _Dwf_DocumentStagePeople.Find("ID = " & DocumentStagePeopleDT.Rows(Peo)("ID"))
                                If _Dwf_DocumentStagePeople.PeopleType = Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.DirectManager) Then
                                    If RetPeopleDirectManager(_Dwf_DocumentWorkFow.InitPeople) <> Nothing Then
                                        Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                        _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                                        _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, NextStage)
                                        _Dwf_DocumentWorkFowNew.PeopleID = RetPeopleDirectManager(_Dwf_DocumentWorkFow.InitPeople)
                                        _Dwf_DocumentWorkFowNew.MessageArabic = RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                        _Dwf_DocumentWorkFowNew.MessageEnglish = RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                        _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                                        _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                                        _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                                        _Dwf_DocumentWorkFowNew.Status = 0
                                        _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                                        _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                                        _Dwf_DocumentWorkFowNew.Save()
                                    End If
                                ElseIf _Dwf_DocumentStagePeople.PeopleType = Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.Position) Then
                                    For Pos As Integer = 0 To RetPeoplePosition(_Dwf_DocumentStagePeople.PositionID).Rows.Count - 1
                                        Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                        _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                                        _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, NextStage)
                                        _Dwf_DocumentWorkFowNew.PeopleID = RetPeoplePosition(_Dwf_DocumentStagePeople.PositionID).Rows(Pos)(0).ToString()
                                        _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                                        _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                                        _Dwf_DocumentWorkFowNew.MessageArabic = RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                        _Dwf_DocumentWorkFowNew.MessageEnglish = RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                        _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                                        _Dwf_DocumentWorkFowNew.Status = 0
                                        _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                                        _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                                        _Dwf_DocumentWorkFowNew.Save()
                                    Next
                                End If
                            Next
                            Dim StrPeopleList As String() = PeopleList.Split(";")
                            If StrPeopleList.Length > 1 Then
                                For i As Integer = 1 To StrPeopleList.Length - 1
                                    Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                    _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                                    _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, NextStage)
                                    _Dwf_DocumentWorkFowNew.PeopleID = Convert.ToInt32(StrPeopleList(i))
                                    _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                                    _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                                    _Dwf_DocumentWorkFowNew.MessageArabic = RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                    _Dwf_DocumentWorkFowNew.MessageEnglish = RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                    _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                                    _Dwf_DocumentWorkFowNew.Status = 0
                                    _Dwf_DocumentWorkFowNew.RegComputerID = Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.Employee)
                                    _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                                    _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                                    _Dwf_DocumentWorkFowNew.Save()
                                Next
                            End If
                        ElseIf NextStage.Contains("menu_en") = True Then
                            _Dwf_DocumentWorkFow = New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                            _Dwf_DocumentWorkFow.DocumentID = Convert.ToInt32(DocumentID)
                            _Dwf_DocumentWorkFow.StageID = RetStageID(DocumentID, NextStage)
                            _Dwf_DocumentWorkFow.TrnsEntryCode = Code
                            _Dwf_DocumentWorkFow.RegUserID = ProfileCls.RetUserID()
                            _Dwf_DocumentWorkFow.Status = 1
                            _Dwf_DocumentWorkFow.RegDate = DateTime.Now
                            _Dwf_DocumentWorkFow.CancelDate = Nothing
                            _Dwf_DocumentWorkFow.Save()
                        End If
                    End If
                ElseIf RetActionDirection(Action) = Convert.ToInt32(HelperCls.GlobalEnum.ActionMovement.StepBackward) Then
                    ''If Backward
                    Dim IsSave As Integer = 0
                    If _Dwf_DocumentStages.WaitForAll = True Then
                        Dim _Dwf_DocumentWorkFowCheck As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                        _Dwf_DocumentWorkFowCheck.Find("StageID = " & RetStageID(DocumentID, Stage) & " and TrnsEntryCode = '" & Code & "'")
                        Dim DocumentWorkFowCheckDT As DataTable = _Dwf_DocumentWorkFowCheck.DataSet.Tables(0)
                        For FCH As Integer = 0 To DocumentWorkFowCheckDT.Rows.Count - 1
                            _Dwf_DocumentWorkFowCheck = New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                            _Dwf_DocumentWorkFowCheck.Find("ID = " & DocumentWorkFowCheckDT.Rows(FCH)("ID"))
                            _Dwf_DocumentWorkFowCheck.Status = 2
                            _Dwf_DocumentWorkFowCheck.Update("ID = " & DocumentWorkFowCheckDT.Rows(FCH)("ID"))
                        Next
                        Dim BackStage As String = _Dwf_DocumentStages.ConnectorIn.Replace("cont_", "")
                        If BackStage.Contains("menu_sg") = True Then
                            Dim _Dwf_DocumentStagePeople As New ClsDwf_DocumentStagePeoples(New System.Web.UI.Page)
                            _Dwf_DocumentStagePeople.Find("StageCode = '" & BackStage & "' and DocumentID = " & Convert.ToInt32(DocumentID))
                            Dim DocumentStagePeopleDT As DataTable = _Dwf_DocumentStagePeople.DataSet.Tables(0)
                            For Peo As Integer = 0 To DocumentStagePeopleDT.Rows.Count - 1
                                _Dwf_DocumentStagePeople = New ClsDwf_DocumentStagePeoples(New System.Web.UI.Page)
                                _Dwf_DocumentStagePeople.Find("ID = " & DocumentStagePeopleDT.Rows(Peo)("ID"))
                                If _Dwf_DocumentStagePeople.PeopleType = Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.DirectManager) Then
                                    If RetPeopleDirectManager(_Dwf_DocumentWorkFow.InitPeople) <> Nothing Then
                                        Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                        _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                                        _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, BackStage)
                                        _Dwf_DocumentWorkFowNew.PeopleID = RetPeopleDirectManager(_Dwf_DocumentWorkFow.InitPeople)
                                        _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                                        _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                                        _Dwf_DocumentWorkFowNew.MessageArabic = "تصحيح : " & RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                        _Dwf_DocumentWorkFowNew.MessageEnglish = "Correction : " & RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                        _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                                        _Dwf_DocumentWorkFowNew.Status = 0
                                        _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                                        _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                                        _Dwf_DocumentWorkFowNew.Save()
                                        IsSave = 1
                                    End If
                                ElseIf _Dwf_DocumentStagePeople.PeopleType = Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.Position) Then
                                    For Pos As Integer = 0 To RetPeoplePosition(_Dwf_DocumentStagePeople.PositionID).Rows.Count - 1
                                        Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                        _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                                        _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, BackStage)
                                        _Dwf_DocumentWorkFowNew.PeopleID = RetPeoplePosition(_Dwf_DocumentStagePeople.PositionID).Rows(Pos)(0).ToString()
                                        _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                                        _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                                        _Dwf_DocumentWorkFowNew.MessageArabic = "تصحيح : " & RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                        _Dwf_DocumentWorkFowNew.MessageEnglish = "Correction : " & RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                        _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                                        _Dwf_DocumentWorkFowNew.Status = 0
                                        _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                                        _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                                        _Dwf_DocumentWorkFowNew.Save()
                                        IsSave = 1
                                    Next
                                End If
                            Next
                            Dim StrPeopleList As String() = PeopleList.Split(";")
                            If StrPeopleList.Length > 1 Then
                                For i As Integer = 1 To StrPeopleList.Length - 1
                                    Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                    _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                                    _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, BackStage)
                                    _Dwf_DocumentWorkFowNew.PeopleID = Convert.ToInt32(StrPeopleList(i))
                                    _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                                    _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                                    _Dwf_DocumentWorkFowNew.MessageArabic = "تصحيح : " & RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                    _Dwf_DocumentWorkFowNew.MessageEnglish = "Correction : " & RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                    _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                                    _Dwf_DocumentWorkFowNew.Status = 0
                                    _Dwf_DocumentWorkFowNew.RegComputerID = Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.Employee)
                                    _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                                    _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                                    _Dwf_DocumentWorkFowNew.Save()
                                Next
                            End If
                            If IsSave <> 1 Then
                                'check If BackStage Is FirestStage
                                Dim _Dwf_DocumentStagesCheckIsInitiator = New ClsDwf_DocumentStages(New System.Web.UI.Page)
                                _Dwf_DocumentStagesCheckIsInitiator.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and Code = '" & BackStage & "'")
                                Dim DTDwf_DocumentStagesCheckIsInitiator As DataTable = _Dwf_DocumentStagesCheckIsInitiator.DataSet.Tables(0)
                                If DTDwf_DocumentStagesCheckIsInitiator.Rows.Count > 0 Then
                                    _Dwf_DocumentStagesCheckIsInitiator = New ClsDwf_DocumentStages(New System.Web.UI.Page)
                                    _Dwf_DocumentStagesCheckIsInitiator.Find("ID = " & DTDwf_DocumentStagesCheckIsInitiator.Rows(0)("ID"))
                                    If _Dwf_DocumentStagesCheckIsInitiator.ConnectorIn.Replace("cont_", "").Contains("menu_st") = True Then
                                        Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                        _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                                        _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, BackStage)
                                        _Dwf_DocumentWorkFowNew.PeopleID = Convert.ToInt32(_Dwf_DocumentWorkFow.InitPeople)
                                        _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                                        _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                                        _Dwf_DocumentWorkFowNew.MessageArabic = "تصحيح : " & RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                        _Dwf_DocumentWorkFowNew.MessageEnglish = "Correction : " & RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                        _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                                        _Dwf_DocumentWorkFowNew.Status = 0
                                        _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                                        _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                                        _Dwf_DocumentWorkFowNew.Save()
                                    End If
                                End If
                            End If
                        End If
                    Else
                        Dim _Dwf_DocumentWorkFowCheck As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                        If _Dwf_DocumentWorkFowCheck.Find("StageID = " & RetStageID(DocumentID, Stage) & " and TrnsEntryCode = '" & Code & "'") Then
                            ''Transfeer To Next Stage
                            Dim BackStage As String = _Dwf_DocumentStages.ConnectorIn.Replace("cont_", "")
                            If BackStage.Contains("menu_sg") = True Then

                                Dim _Dwf_DocumentStagePeople As New ClsDwf_DocumentStagePeoples(New System.Web.UI.Page)
                                _Dwf_DocumentStagePeople.Find("StageCode = '" & BackStage & "' and DocumentID = " & Convert.ToInt32(DocumentID))
                                Dim DocumentStagePeopleDT As DataTable = _Dwf_DocumentStagePeople.DataSet.Tables(0)
                                For Peo As Integer = 0 To DocumentStagePeopleDT.Rows.Count - 1
                                    _Dwf_DocumentStagePeople = New ClsDwf_DocumentStagePeoples(New System.Web.UI.Page)
                                    _Dwf_DocumentStagePeople.Find("ID = " & DocumentStagePeopleDT.Rows(Peo)("ID"))
                                    If _Dwf_DocumentStagePeople.PeopleType = Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.DirectManager) Then
                                        If RetPeopleDirectManager(_Dwf_DocumentWorkFow.InitPeople) <> Nothing Then
                                            Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                            _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                                            _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, BackStage)
                                            _Dwf_DocumentWorkFowNew.PeopleID = RetPeopleDirectManager(_Dwf_DocumentWorkFow.InitPeople)
                                            _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                                            _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                                            _Dwf_DocumentWorkFowNew.MessageArabic = "تصحيح : " & RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                            _Dwf_DocumentWorkFowNew.MessageEnglish = "Correction : " & RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                            _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                                            _Dwf_DocumentWorkFowNew.Status = 0
                                            _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                                            _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                                            _Dwf_DocumentWorkFowNew.Save()
                                            IsSave = 1
                                        End If
                                    ElseIf _Dwf_DocumentStagePeople.PeopleType = Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.Position) Then
                                        For Pos As Integer = 0 To RetPeoplePosition(_Dwf_DocumentStagePeople.PositionID).Rows.Count - 1
                                            Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                            _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                                            _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, BackStage)
                                            _Dwf_DocumentWorkFowNew.PeopleID = RetPeoplePosition(_Dwf_DocumentStagePeople.PositionID).Rows(Pos)(0).ToString()
                                            _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                                            _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                                            _Dwf_DocumentWorkFowNew.MessageArabic = "تصحيح : " & RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                            _Dwf_DocumentWorkFowNew.MessageEnglish = "Correction : " & RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                            _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                                            _Dwf_DocumentWorkFowNew.Status = 0
                                            _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                                            _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                                            _Dwf_DocumentWorkFowNew.Save()
                                            IsSave = 1
                                        Next
                                    End If
                                Next
                                Dim StrPeopleList As String() = PeopleList.Split(";")
                                If StrPeopleList.Length > 1 Then
                                    For i As Integer = 1 To StrPeopleList.Length - 1
                                        Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                        _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                                        _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, BackStage)
                                        _Dwf_DocumentWorkFowNew.PeopleID = Convert.ToInt32(StrPeopleList(i))
                                        _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                                        _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                                        _Dwf_DocumentWorkFowNew.MessageArabic = "تصحيح : " & RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                        _Dwf_DocumentWorkFowNew.MessageEnglish = "Correction : " & RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                        _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                                        _Dwf_DocumentWorkFowNew.Status = 0
                                        _Dwf_DocumentWorkFowNew.RegComputerID = Convert.ToInt32(HelperCls.GlobalEnum.StagePeople.Employee)
                                        _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                                        _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                                        _Dwf_DocumentWorkFowNew.Save()
                                    Next
                                End If
                                If IsSave <> 1 Then
                                    'check If BackStage Is FirestStage
                                    Dim _Dwf_DocumentStagesCheckIsInitiator = New ClsDwf_DocumentStages(New System.Web.UI.Page)
                                    _Dwf_DocumentStagesCheckIsInitiator.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and Code = '" & BackStage & "'")
                                    Dim DTDwf_DocumentStagesCheckIsInitiator As DataTable = _Dwf_DocumentStagesCheckIsInitiator.DataSet.Tables(0)
                                    If DTDwf_DocumentStagesCheckIsInitiator.Rows.Count > 0 Then
                                        _Dwf_DocumentStagesCheckIsInitiator = New ClsDwf_DocumentStages(New System.Web.UI.Page)
                                        _Dwf_DocumentStagesCheckIsInitiator.Find("ID = " & DTDwf_DocumentStagesCheckIsInitiator.Rows(0)("ID"))
                                        If _Dwf_DocumentStagesCheckIsInitiator.ConnectorIn.Replace("cont_", "").Contains("menu_st") = True Then
                                            Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                                            _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                                            _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, BackStage)
                                            _Dwf_DocumentWorkFowNew.PeopleID = Convert.ToInt32(_Dwf_DocumentWorkFow.InitPeople)
                                            _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                                            _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                                            _Dwf_DocumentWorkFowNew.MessageArabic = "تصحيح : " & RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                            _Dwf_DocumentWorkFowNew.MessageEnglish = "Correction : " & RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                                            _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                                            _Dwf_DocumentWorkFowNew.Status = 0
                                            _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                                            _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                                            _Dwf_DocumentWorkFowNew.Save()
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                ElseIf RetActionDirection(Action) = Convert.ToInt32(HelperCls.GlobalEnum.ActionMovement.StepFirest) Then
                    Dim _Dwf_DocumentWorkFowCheck As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                    _Dwf_DocumentWorkFowCheck.Find("StageID = " & RetStageID(DocumentID, Stage) & " and TrnsEntryCode = '" & Code & "'")
                    Dim DocumentWorkFowCheckDT As DataTable = _Dwf_DocumentWorkFowCheck.DataSet.Tables(0)
                    For FCH As Integer = 0 To DocumentWorkFowCheckDT.Rows.Count - 1
                        _Dwf_DocumentWorkFowCheck = New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                        _Dwf_DocumentWorkFowCheck.Find("ID = " & DocumentWorkFowCheckDT.Rows(FCH)("ID"))
                        _Dwf_DocumentWorkFowCheck.Status = 2
                        _Dwf_DocumentWorkFowCheck.Update("ID = " & DocumentWorkFowCheckDT.Rows(FCH)("ID"))
                    Next
                    Dim _Dwf_DocumentStagesGetFirest = New ClsDwf_DocumentStages(New System.Web.UI.Page)
                    _Dwf_DocumentStagesGetFirest.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and ElementType = 'Start'")
                    Dim DTDocumentGetFirest As DataTable = _Dwf_DocumentStages.DataSet.Tables(0)
                    If DTDocumentGetFirest.Rows.Count > 0 Then
                        _Dwf_DocumentStagesGetFirest = New ClsDwf_DocumentStages(New System.Web.UI.Page)
                        _Dwf_DocumentStagesGetFirest.Find("ID = " & DTDocumentGetFirest.Rows(0)("ID"))
                        Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                        _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                        _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, "menu_st1")
                        _Dwf_DocumentWorkFowNew.PeopleID = Convert.ToInt32(_Dwf_DocumentWorkFow.InitPeople)
                        _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                        _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                        _Dwf_DocumentWorkFowNew.MessageArabic = "** رد ** " & RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                        _Dwf_DocumentWorkFowNew.MessageEnglish = "** Return ** " & RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                        _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                        _Dwf_DocumentWorkFowNew.Status = 1
                        _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                        _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                        _Dwf_DocumentWorkFowNew.Save()
                    End If
                ElseIf RetActionDirection(Action) = Convert.ToInt32(HelperCls.GlobalEnum.ActionMovement.ForwordTo) Then


                    _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and Code = '" & Stage & "'")
                    Dim NextStage As String = _Dwf_DocumentStages.ConnectorOut.Replace("cont_", "")
                    'Dim NextStage As String = "ForwordTo"

                    Dim _Dwf_DocumentWorkFowCheck As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                    _Dwf_DocumentWorkFowCheck.Find("StageID = " & RetStageID(DocumentID, Stage) & " and TrnsEntryCode = '" & Code & "' and Status = 0")
                    Dim str As String = "select id from hrs_employees where code='" & ForwordToValue & "'"
                    Dim Employeeid As Integer = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(_Dwf_DocumentStages.ConnectionString, CommandType.Text, str)


                    Dim _Dwf_DocumentStagePeople As New ClsDwf_DocumentStagePeoples(New System.Web.UI.Page)
                    _Dwf_DocumentStagePeople.Find("StageCode = '" & NextStage & "' and DocumentID = " & Convert.ToInt32(DocumentID))
                    Dim DocumentStagePeopleDT As DataTable = _Dwf_DocumentStagePeople.DataSet.Tables(0)
                    Dim _Dwf_DocumentWorkFowNew As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                    _Dwf_DocumentWorkFowNew.DocumentID = Convert.ToInt32(DocumentID)
                    _Dwf_DocumentWorkFowNew.StageID = RetStageID(DocumentID, NextStage)
                    _Dwf_DocumentWorkFowNew.PeopleID = Employeeid 'RetPeopleDirectManager(_Dwf_DocumentWorkFow.InitPeople)
                    _Dwf_DocumentWorkFowNew.MessageArabic = RetArabicMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                    _Dwf_DocumentWorkFowNew.MessageEnglish = RetEnglishMessage(_Dwf_DocumentWorkFowNew.DocumentID, _Dwf_DocumentWorkFowNew.StageID)
                    _Dwf_DocumentWorkFowNew.TrnsEntryCode = Code
                    _Dwf_DocumentWorkFowNew.InitPeople = _Dwf_DocumentWorkFow.InitPeople
                    _Dwf_DocumentWorkFowNew.RegUserID = ProfileCls.RetUserID()
                    _Dwf_DocumentWorkFowNew.Status = 0
                    _Dwf_DocumentWorkFowNew.RegDate = DateTime.Now
                    _Dwf_DocumentWorkFowNew.CancelDate = Nothing
                    _Dwf_DocumentWorkFowNew.Save()

                End If

                Dim _Dwf_DocumentStageActionNotify = New ClsDwf_DocumentStageActionNotify(New System.Web.UI.Page)
                _Dwf_DocumentStageActionNotify.Find("DocumentID = " & DocumentID & " and StageCode = '" & Stage & "' and ActionID = " & Action)
                Dim DTDocumentStageActionNotify As DataTable = _Dwf_DocumentStageActionNotify.DataSet.Tables(0)
                For i As Integer = 0 To DTDocumentStageActionNotify.Rows.Count - 1
                    _Dwf_DocumentStageActionNotify = New ClsDwf_DocumentStageActionNotify(New System.Web.UI.Page)
                    _Dwf_DocumentStageActionNotify.Find("ID = " & DTDocumentStageActionNotify.Rows(i)("ID"))
                    If _Dwf_DocumentStageActionNotify.Notification = Convert.ToInt32(HelperCls.GlobalEnum.StageActionNotificationWays.ByMail) Then
                        If _Dwf_DocumentStageActionNotify.RegComputerID = Convert.ToInt32(HelperCls.GlobalEnum.StageActionNotificationTarget.Stage) Then
                            If RetPeopleDirectManager(_Dwf_DocumentWorkFow.InitPeople) <> Nothing Then
                                SendMail(RetPeopleMail(RetPeopleDirectManager(_Dwf_DocumentWorkFow.InitPeople)), WF, Code, Action)
                            End If
                        ElseIf _Dwf_DocumentStageActionNotify.RegComputerID = Convert.ToInt32(HelperCls.GlobalEnum.StageActionNotificationTarget.Position) Then
                            For Pos As Integer = 0 To RetPeoplePosition(_Dwf_DocumentStageActionNotify.NotifyPosition).Rows.Count - 1
                                SendMail(RetPeopleMail(RetPeoplePosition(_Dwf_DocumentStageActionNotify.NotifyPosition).Rows(Pos)(0).ToString()), WF, Code, Action)
                            Next
                        ElseIf _Dwf_DocumentStageActionNotify.RegComputerID = Convert.ToInt32(HelperCls.GlobalEnum.StageActionNotificationTarget.Employee) Then
                            Dim StrPeopleNotiList As String() = PeopleNotiList.Split(";")
                            If StrPeopleNotiList.Length > 1 Then
                                For x As Integer = 1 To StrPeopleNotiList.Length - 1
                                    SendMail(RetPeopleMail(StrPeopleNotiList(x)), WF, Code, Action)
                                Next
                            End If
                        ElseIf _Dwf_DocumentStageActionNotify.RegComputerID = Convert.ToInt32(HelperCls.GlobalEnum.StageActionNotificationTarget.Initiator) Then
                            SendMail(RetPeopleMail(_Dwf_DocumentWorkFow.InitPeople), WF, Code, Action)
                        End If

                    End If
                Next
            End If
            Return Code & "|" & Action
        Else
            Return Nothing
        End If
    End Function
    Public Sub SendMail(ByVal ToMail As String, ByVal WF As String, ByVal Code As String, ByVal Action As String)
        Try
            If ToMail <> Nothing Then
                Dim _Dwf_DocumentWorkFow As New ClsDwf_DocumentWorkFow(New System.Web.UI.Page)
                _Dwf_DocumentWorkFow.Find("ID = " & Convert.ToInt32(WF))
                Dim SMTPHost As String = ConfigurationManager.AppSettings("SMTPHost").ToString()
                Dim SMTPPort As String = ConfigurationManager.AppSettings("SMTPPort").ToString()
                Dim SMTPUsername As String = ConfigurationManager.AppSettings("SMTPUsername").ToString()
                Dim SMTPPassword As String = ConfigurationManager.AppSettings("SMTPPassword").ToString()
                Dim SMTPFrom As String = ConfigurationManager.AppSettings("SMTPFrom").ToString()
                Dim smtp As New System.Net.Mail.SmtpClient()
                Dim message As New System.Net.Mail.MailMessage()
                Dim FromAddress As New System.Net.Mail.MailAddress(SMTPFrom, "WorkFlow Notification" & "  " & "تنبيهات نظام تدفق المستندات")
                smtp.Host = SMTPHost
                smtp.Port = SMTPPort
                Dim cred As New System.Net.NetworkCredential(SMTPUsername, SMTPPassword)
                message.From = FromAddress
                message.To.Clear()
                message.To.Add(ToMail)
                message.Subject = "رسالة تنبيه    Notification Message"


                Dim TextStringAr As String = "رقم الطلب : " & Code
                Dim TextStringEn As String = "Request No : " & Code
                Dim _Dwf_ActionType As New ClsDwf_ActionTypes(New System.Web.UI.Page)
                _Dwf_ActionType.Find("ID = " & Convert.ToInt32(Action))
                Dim ActionStringAr As String = _Dwf_ActionType.ArbName
                Dim ActionStringEn As String = _Dwf_ActionType.EngName

                Dim StrCommand As String = "select isnull((isnull(ArbName,'') + ' ' + isnull(FamilyArbName,'') + ' ' + isnull(FatherArbName,'') + ' ' + isnull(GrandArbName,'')) , (isnull(EngName,'') + ' ' + isnull(FamilyEngName,'') + ' ' + isnull(FatherEngName,'') + ' ' + isnull(GrandEngName,''))) AS ArbName ," & _
                                           "isnull((isnull(EngName,'') + ' ' + isnull(FamilyEngName,'') + ' ' + isnull(FatherEngName,'') + ' ' + isnull(GrandEngName,'')),(isnull(ArbName,'') + ' ' + isnull(FamilyArbName,'') + ' ' + isnull(FatherArbName,'') + ' ' + isnull(GrandArbName,''))) AS EngName " & _
                                           "from hrs_Employees where ID = '" & _Dwf_DocumentWorkFow.PeopleID & "'"
                Dim EmployeeDatatable As New Data.DataTable
                EmployeeDatatable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Dwf_ActionType.ConnectionString, CommandType.Text, StrCommand).Tables(0)
                Dim ArPeopString As String = ""
                Dim EnPeopString As String = ""
                If EmployeeDatatable.Rows.Count > 0 Then
                    ArPeopString = EmployeeDatatable.Rows(0)(0).ToString()
                    EnPeopString = EmployeeDatatable.Rows(0)(1).ToString()
                End If
                Dim ArExtraTextString As String = TextStringAr & " من " & ArPeopString & " حالة الطلب " & ActionStringAr
                Dim EnExtraTextString As String = TextStringEn & " From " & EnPeopString & " Request Status " & ActionStringEn

                message.Body = "<p>حالة الطلب :" & ActionStringAr & "</p>"
                message.Body += "<p>من :" & ArPeopString & "</p>"
                message.Body += "<p>المرحلة :" & _Dwf_DocumentWorkFow.MessageArabic & "</p>"
                message.Body += "<p>رقم الطلب : " & Code & "</p>"
                message.Body += "<p></p>"
                message.Body += "<p>Request Status :" & ActionStringEn & "</p>"
                message.Body += "<p>From :" & EnPeopString & "</p>"
                message.Body += "<p>Stage :" & _Dwf_DocumentWorkFow.MessageEnglish & "</p>"
                message.Body += "<p>Request No : " & Code & "</p>"

                message.IsBodyHtml = True
                smtp.UseDefaultCredentials = False
                smtp.EnableSsl = False
                smtp.Credentials = cred
                smtp.Send(message)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Function RetStageID(ByVal DocumentID As String, ByVal Stage As String) As Integer
        Dim _Dwf_DocumentStages As New ClsDwf_DocumentStages(New System.Web.UI.Page)
        _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and Code = '" & Stage & "'")
        Dim DTDocumentStartStages As DataTable = _Dwf_DocumentStages.DataSet.Tables(0)
        If DTDocumentStartStages.Rows.Count > 0 Then
            _Dwf_DocumentStages.Find("ID = " & DTDocumentStartStages.Rows(0)("ID"))
            Return _Dwf_DocumentStages.ID
        End If
        Return Nothing
    End Function
    Public Function RetArabicMessage(ByVal DocumentID As String, ByVal StageID As Integer) As String
        Dim Message As String = "رسالة تنبيه : بتاريخ (" & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & ") "
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        If _Dwf_Document.Find("ID = " & Convert.ToInt32(DocumentID)) Then
            Message = Message & "<br/>" & "يوجد مستند " & _Dwf_Document.ArbName & " "
            Dim _Dwf_DocumentStages As New ClsDwf_DocumentStages(New System.Web.UI.Page)
            _Dwf_DocumentStages.Find("ID = " & StageID)
            Message = Message & "ينتظر " & _Dwf_DocumentStages.ArComment & " "
        End If
        Return Message
    End Function
    Public Function RetEnglishMessage(ByVal DocumentID As String, ByVal StageID As Integer) As String
        Dim Message As String = "رسالة تنبيه : بتاريخ (" & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & ") "
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        If _Dwf_Document.Find("ID = " & Convert.ToInt32(DocumentID)) Then
            Message = Message & "<br/>" & "يوجد مستند " & _Dwf_Document.EngName & " "
            Dim _Dwf_DocumentStages As New ClsDwf_DocumentStages(New System.Web.UI.Page)
            _Dwf_DocumentStages.Find("ID = " & StageID)
            Message = Message & "ينتظر " & _Dwf_DocumentStages.EnComment & " "
        End If
        Return Message
    End Function
    Public Function RetStagePeople(ByVal DocumentID As String, ByVal People As String) As Integer
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        Dim StrCommand As String = "select RelEmployee from sys_Users where ID = " & People
        Dim ID As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(_Dwf_Document.ConnectionString, CommandType.Text, StrCommand)
        Try
            Return Convert.ToInt32(ID)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function RetPeopleDirectManager(ByVal People As String) As Integer
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        Dim StrCommand As String = "select ManagerID from hrs_Employees where ID = " & People
        Dim ID As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(_Dwf_Document.ConnectionString, CommandType.Text, StrCommand)
        Try
            Return Convert.ToInt32(ID)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function RetStepEmployees(ByVal TrnsEntryCode As String) As String
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        Dim StrCommand As String = "select dbo.fn_GetEmployeeName(EngName, FatherEngName, GrandEngName, FamilyEngName, ArbName, FatherArbName, GrandArbName, FamilyArbName, 1) " & _
                           "from hrs_Employees where ID in(select PeopleID from Dwf_DocumentWorkFow where DocumentID in (select DocumentID from Dwf_DocumentWorkFow " & _
                           "where TrnsEntryCode='" + TrnsEntryCode + "') and TrnsEntryCode='" + TrnsEntryCode + "' and RegComputerID is not null)"
        Dim Dt As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Dwf_Document.ConnectionString, CommandType.Text, StrCommand).Tables(0)
        Dim StepList As String = ""
        For i As Integer = 0 To Dt.Rows.Count - 1
            StepList = StepList & " , " & Dt.Rows(i)(0).ToString()
        Next
        Return StepList
    End Function
    Public Function RetPeopleMail(ByVal People As String) As String
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        Dim StrCommand As String = "select E_Mail from hrs_Employees where ID = " & People
        Dim ID As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(_Dwf_Document.ConnectionString, CommandType.Text, StrCommand)
        Try
            Return Convert.ToString(ID)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function RetPeoplePosition(ByVal Position As String) As DataTable
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        Dim StrCommand As String = "select EmployeeID from hrs_Contracts where ID in (select Max(Dtl.ID) from hrs_contracts as Dtl where Dtl.EmployeeID = hrs_Contracts.EmployeeID) and PositionID = '" & Position & "'"
        Dim Dt As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Dwf_Document.ConnectionString, CommandType.Text, StrCommand).Tables(0)
        Return Dt
    End Function
    Public Function RetActionDirection(ByVal Action As String) As Integer
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        Dim StrCommand As String = "select Movement from Dwf_ActionTypes where ID = " & Action
        Dim Movement As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(_Dwf_Document.ConnectionString, CommandType.Text, StrCommand)
        Try
            Return Convert.ToInt32(Movement)
        Catch ex As Exception
            Return Convert.ToInt32(HelperCls.GlobalEnum.ActionMovement.StepForward)
        End Try
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function UpdateData(ByVal DocumentID As String, ByVal StrArray As String, ByVal ID As String) As Integer
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        _Dwf_Document.Find("ID = " & Convert.ToInt32(DocumentID))
        Dim Result As Int32 = 0
        Dim ColString As String = ""
        ColString = ColString & "update " & "CF_" & _Dwf_Document.Code & " set "
        Dim ColumnsData As String() = StrArray.Split("|")
        For i As Integer = 0 To ColumnsData.Length - 1
            If ColumnsData(i) <> "" Then
                Dim column As String() = ColumnsData(i).Split(";")
                Dim columnname As String = ""
                Dim columntype As String = ""
                Dim ColumnValue As String = ""
                columnname = column(0).ToString()
                ColumnValue = column(2).ToString()
                If column(1).ToString = "TextBox" Then
                    ColString = ColString & "[" & columnname & "] = '" & ColumnValue & "'"
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                    End If
                ElseIf column(1).ToString = "TextArea" Then
                    ColString = ColString & "[" & columnname & "] = '" & ColumnValue & "'"
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                    End If
                ElseIf column(1).ToString = "ComboBox" Then
                    If (ColumnValue = "") Then
                        ColumnValue = "null"
                    End If
                    ColString = ColString & "[" & columnname & "] = " & ColumnValue
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                    End If
                ElseIf column(1).ToString = "CheckBox" Then
                    If (ColumnValue.ToUpper() = "TRUE") Then
                        ColumnValue = "1"
                    Else
                        ColumnValue = "0"
                    End If
                    ColString = ColString & "[" & columnname & "] = " & ColumnValue
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                    End If
                ElseIf column(1).ToString = "Radio" Then
                    If (ColumnValue.ToUpper() = "TRUE") Then
                        ColumnValue = "1"
                    Else
                        ColumnValue = "0"
                    End If
                    ColString = ColString & "[" & columnname & "] = " & ColumnValue
                    If i < ColumnsData.Length - 1 Then
                        ColString = ColString & ","
                    End If
                End If
            End If
        Next i
        ColString = ColString & ",[RegUserID] = " & ProfileCls.RetUserID() & ",[RegDate] = getdate()"
        ColString = ColString & " where ID = " & ID
        Result = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(_Dwf_Document.ConnectionString, CommandType.Text, ColString.Replace(",,", ","))
        Return Result
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function DeleteData(ByVal DocumentID As String, ByVal ID As String) As Integer
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        _Dwf_Document.Find("ID = " & Convert.ToInt32(DocumentID))
        Dim Result As Int32 = 0
        Dim ColString As String = ""
        ColString = ColString & "update " & "CF_" & _Dwf_Document.Code & " set "
        ColString = ColString & "[CancelDate] = getdate()"
        ColString = ColString & " where ID = " & ID
        Result = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(_Dwf_Document.ConnectionString, CommandType.Text, ColString)
        Return Result
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function PrintData(ByVal DocumentID As String, ByVal ID As String) As String
        Dim Val As String = ""
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        _Dwf_Document.Find("ID = " & Convert.ToInt32(DocumentID))
        Dim _Sys_Report As New Clssys_Reports(New System.Web.UI.Page)
        If _Sys_Report.Find("ID = " & _Dwf_Document.ReportID) Then
            If _Sys_Report.DataSource <> "" Then
                If _Sys_Report.ReportSource = 1 Then
                    Dim StrCommand As String = "select PARAMETER_NAME from information_schema.parameters where PARAMETER_MODE = 'IN' and specific_name='" & _Sys_Report.DataSource & "'"
                    Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Dwf_Document.ConnectionString, CommandType.Text, StrCommand).Tables(0)
                    If DT.Rows.Count > 0 Then
                        Val = DT.Rows(0)(0).ToString().Replace("@", "")
                    Else
                        Return ""
                    End If
                End If
                Return "../../Interfaces/frmReportsGridViewer.aspx?Language=true&Criteria=" & Val & "&preview=1&ReportCode=" & _Sys_Report.Code & "&sq0=''&v=" & ID
            Else
                Return ""
            End If
        Else
            Return ""
        End If
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function SearchForm(ByVal CtrlID As String, ByVal CurLang As String) As String
        Dim _Dwf_DocumentElement As New ClsDwf_DocumentElements(New System.Web.UI.Page)
        _Dwf_DocumentElement.Find("ID = " & Convert.ToInt32(CtrlID))
        Dim _Sys_Search As New Clssys_Searchs(New System.Web.UI.Page)
        If _Sys_Search.Find("ID = " & _Dwf_DocumentElement.SearchID) Then
            If _Dwf_DocumentElement.SearchID > 0 And _Dwf_DocumentElement.KeyColumn <> "" Then
                Dim lang As String = IIf(CurLang = "Ar", "ar-EG", "en-US")
                Return "../../Interfaces/frmSearchScreen.aspx?Language=" & lang & "&SearchID=" & _Dwf_DocumentElement.SearchID & "&TargetControl=" & _Dwf_DocumentElement.KeyColumn
            Else
                Return ""
            End If
        End If
        Return ""
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function ZoomForm(ByVal CtrlID As String, ByVal CurLang As String) As String
        Dim Url As String = ""
        Dim _Dwf_DocumentElement As New ClsDwf_DocumentElements(New System.Web.UI.Page)
        _Dwf_DocumentElement.Find("ID = " & Convert.ToInt32(CtrlID))
        Dim _Sys_Form As New ClsSys_Forms(New System.Web.UI.Page)
        _Sys_Form.Find("Code = '" & _Dwf_DocumentElement.ZoomingForm & "'")
        Dim DT As DataTable = _Sys_Form.DataSet.Tables(0)
        If DT.Rows.Count > 0 Then
            _Sys_Form.Find("ID = " & DT.Rows(0)(0))
            If _Sys_Form.LinkUrl = "" Then
                Url = "../../Interfaces/" & _Sys_Form.Code & ".aspx" & "|" & _Sys_Form.Height.ToString() & "|" & _Sys_Form.Width.ToString()
            Else
                Url = _Sys_Form.LinkUrl & "|" & _Sys_Form.Height.ToString() & "|" & _Sys_Form.Width.ToString()
            End If
        End If
        Return Url
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function LinkForm(ByVal CtrlID As String, ByVal CurLang As String) As String
        Dim Url As String = ""
        Dim _Dwf_DocumentElement As New ClsDwf_DocumentElements(New System.Web.UI.Page)
        _Dwf_DocumentElement.Find("ID = " & Convert.ToInt32(CtrlID))
        Dim FrmURL As String = _Dwf_DocumentElement.ZoomingForm.Split("?")(0)
        Url = Url & FrmURL
        For i As Integer = 0 To _Dwf_DocumentElement.ZoomingForm.Split("?")(1).Split("&").Length - 1
            Dim cstring As String = _Dwf_DocumentElement.ZoomingForm.Split("?")(1).Split("&")(i)
            For x As Integer = 0 To cstring.Split("=").Length - 1
                Url = Url & "|" & cstring.Split("=")(x)
            Next x
        Next i
        Return Url
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function RetCode(ByVal DocumentID As String, ByVal Stage As String, ByVal MainElement As String, ByVal MainValue As String) As Object(,)
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        _Dwf_Document.Find("ID = " & Convert.ToInt32(DocumentID))
        Dim StrCommand As String = ""
        StrCommand = "select TOP 1 * FROM " & "CF_" & _Dwf_Document.Code & " WHERE " & MainElement & " = '" & MainValue & "' order By ID DESC"
        Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Dwf_Document.ConnectionString, CommandType.Text, StrCommand).Tables(0)
        If DT.Rows.Count > 0 Then
            Return FillObject(DT.Rows(0), 5)
        End If
        Return FillObject(Nothing, 5)
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function SelectNext(ByVal DocumentID As String, ByVal Stage As String, ByVal ID As String) As Object(,)
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        _Dwf_Document.Find("ID = " & Convert.ToInt32(DocumentID))
        Dim _Dwf_DocumentStages As New ClsDwf_DocumentStages(New System.Web.UI.Page)
        _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and Code = '" & Stage & "'")
        Dim DTStage As DataTable = _Dwf_DocumentStages.DataSet.Tables(0)
        If DTStage.Rows.Count > 0 Then
            _Dwf_DocumentStages.Find("ID = " & DTStage.Rows(0)("ID"))
            Stage = _Dwf_DocumentStages.ConnectorIn.Replace("cont_", "")
        End If
        Dim StrCommand As String = ""
        If Stage = "" Then
            StrCommand = "select TOP 1 * FROM " & "CF_" & _Dwf_Document.Code & " WHERE ID > " & ID & " ORDER BY ID ASC"
        Else
            StrCommand = "select TOP 1 * FROM " & "CF_" & _Dwf_Document.Code & " WHERE ID > " & ID & " and Stage='" & Stage & "' ORDER BY ID ASC"
        End If
        Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Dwf_Document.ConnectionString, CommandType.Text, StrCommand).Tables(0)
        If DT.Rows.Count > 0 Then
            Return FillObject(DT.Rows(0), 3)
        End If
        Return FillObject(Nothing, 3)
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function SelectBack(ByVal DocumentID As String, ByVal Stage As String, ByVal ID As String) As Object(,)
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        _Dwf_Document.Find("ID = " & Convert.ToInt32(DocumentID))
        Dim _Dwf_DocumentStages As New ClsDwf_DocumentStages(New System.Web.UI.Page)
        _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and Code = '" & Stage & "'")
        Dim DTStage As DataTable = _Dwf_DocumentStages.DataSet.Tables(0)
        If DTStage.Rows.Count > 0 Then
            _Dwf_DocumentStages.Find("ID = " & DTStage.Rows(0)("ID"))
            Stage = _Dwf_DocumentStages.ConnectorIn.Replace("cont_", "")
        End If
        Dim StrCommand As String = ""
        If Stage = "" Then
            StrCommand = "select TOP 1 * FROM " & "CF_" & _Dwf_Document.Code & " WHERE ID < " & ID & " ORDER BY ID DESC"
        Else
            StrCommand = "select TOP 1 * FROM " & "CF_" & _Dwf_Document.Code & " WHERE ID < " & ID & " and Stage='" & Stage & "' ORDER BY ID DESC"
        End If
        Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Dwf_Document.ConnectionString, CommandType.Text, StrCommand).Tables(0)
        If DT.Rows.Count > 0 Then
            Return FillObject(DT.Rows(0), 2)
        End If
        Return FillObject(Nothing, 2)
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function SelectFirst(ByVal DocumentID As String, ByVal Stage As String) As Object(,)
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        _Dwf_Document.Find("ID = " & Convert.ToInt32(DocumentID))
        Dim _Dwf_DocumentStages As New ClsDwf_DocumentStages(New System.Web.UI.Page)
        _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and Code = '" & Stage & "'")
        Dim DTStage As DataTable = _Dwf_DocumentStages.DataSet.Tables(0)
        If DTStage.Rows.Count > 0 Then
            _Dwf_DocumentStages.Find("ID = " & DTStage.Rows(0)("ID"))
            Stage = _Dwf_DocumentStages.ConnectorIn.Replace("cont_", "")
        End If
        Dim StrCommand As String = ""
        If Stage = "" Then
            StrCommand = "select TOP 1 * FROM " & "CF_" & _Dwf_Document.Code & " ORDER BY ID Asc"
        Else
            StrCommand = "select TOP 1 * FROM " & "CF_" & _Dwf_Document.Code & " WHERE Stage='" & Stage & "' ORDER BY ID Asc"
        End If
        Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Dwf_Document.ConnectionString, CommandType.Text, StrCommand).Tables(0)
        If DT.Rows.Count > 0 Then
            Return FillObject(DT.Rows(0), 1)
        End If
        Return FillObject(Nothing, 1)
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function SelectLast(ByVal DocumentID As String, ByVal Stage As String) As Object(,)
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        _Dwf_Document.Find("ID = " & Convert.ToInt32(DocumentID))
        Dim _Dwf_DocumentStages As New ClsDwf_DocumentStages(New System.Web.UI.Page)
        _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and Code = '" & Stage & "'")
        Dim DTStage As DataTable = _Dwf_DocumentStages.DataSet.Tables(0)
        If DTStage.Rows.Count > 0 Then
            _Dwf_DocumentStages.Find("ID = " & DTStage.Rows(0)("ID"))
            Stage = _Dwf_DocumentStages.ConnectorIn.Replace("cont_", "")
        End If
        Dim StrCommand As String = ""
        If Stage = "" Then
            StrCommand = "select TOP 1 * FROM " & "CF_" & _Dwf_Document.Code & " ORDER BY ID DESC"
        Else
            StrCommand = "select TOP 1 * FROM " & "CF_" & _Dwf_Document.Code & " WHERE Stage='" & Stage & "' ORDER BY ID DESC"
        End If
        Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Dwf_Document.ConnectionString, CommandType.Text, StrCommand).Tables(0)
        If DT.Rows.Count > 0 Then
            Return FillObject(DT.Rows(0), 4)
        End If
        Return FillObject(Nothing, 4)
    End Function
    Public Function FillObject(ByRef _Dr As DataRow, ByVal Ref As Integer) As Object(,)
        Dim myobject(1, 50) As Object
        If _Dr Is Nothing Then
            myobject(0, 0) = Ref
            myobject(0, 1) = 0
        Else
            myobject(0, 0) = Ref
            myobject(0, 1) = 1

            myobject(0, 2) = _Dr("ID") & ";" & _Dr.Table.Columns("ID").DataType.ToString() & ";" & _Dr.Table.Columns("ID").ColumnName.ToString()
            myobject(0, 3) = _Dr("Remarks") & ";" & _Dr.Table.Columns("Remarks").DataType.ToString()
            myobject(0, 4) = _Dr("RegUserID") & ";" & _Dr.Table.Columns("RegUserID").DataType.ToString()
            myobject(0, 5) = _Dr("RegDate") & ";" & _Dr.Table.Columns("RegDate").DataType.ToString()
            myobject(0, 6) = _Dr("CancelDate") & ";" & _Dr.Table.Columns("CancelDate").DataType.ToString()
            For i As Integer = 1 To _Dr.Table.Columns.Count - 5
                myobject(0, i + 6) = _Dr(i) & ";" & _Dr.Table.Columns(i).DataType.ToString() & ";" & _Dr.Table.Columns(i).ColumnName.ToString()
            Next i
        End If
        Return myobject
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function ReturnObjectFields(ByVal ObjectName As String) As Object()
        Dim _Sys_Object As New Clssys_Objects(New System.Web.UI.Page)
        If _Sys_Object.Find("Code = '" & ObjectName & "'") Then
            Dim _Sys_Field As New Clssys_Fields(New System.Web.UI.Page)
            _Sys_Field.Find("ObjectID = " & _Sys_Object.ID)
            Dim DT1 As DataTable = _Sys_Field.DataSet.Tables(0)
            Dim myobject(DT1.Rows.Count - 1) As Object
            For i As Integer = 0 To DT1.Rows.Count - 1
                myobject(i) = DT1.Rows(i)("FieldName")
            Next i
            Return myobject
        End If
        Return Nothing
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function ReturnStageElements(ByVal DocumentID As String) As String
        Dim _Dwf_DocumentStageElement As New ClsDwf_DocumentStageElements(New System.Web.UI.Page)
        _Dwf_DocumentStageElement.Find("DocumentID = " & Convert.ToInt32(DocumentID))
        Dim DT As New DataTable
        Dim RetElements = ""
        DT = _Dwf_DocumentStageElement.DataSet.Tables(0)
        For i As Integer = 0 To DT.Rows.Count - 1
            RetElements = RetElements & DT.Rows(i)("StageCode").ToString() & ";" & DT.Rows(i)("ElementCode").ToString() & ";" & DT.Rows(i)("IsHide").ToString() & ";" & DT.Rows(i)("IsDisabled").ToString()
            If (i <> DT.Rows.Count - 1) Then
                RetElements = RetElements & "|"
            End If
        Next i
        Return RetElements
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function ReturnStagePeoples(ByVal DocumentID As String) As String
        Dim _Dwf_DocumentStagePeople As New ClsDwf_DocumentStagePeoples(New System.Web.UI.Page)
        _Dwf_DocumentStagePeople.Find("DocumentID = " & Convert.ToInt32(DocumentID))
        Dim DT As New DataTable
        Dim RetElements = ""
        DT = _Dwf_DocumentStagePeople.DataSet.Tables(0)
        For i As Integer = 0 To DT.Rows.Count - 1
            RetElements = RetElements & DT.Rows(i)("StageCode").ToString() & ";" & DT.Rows(i)("PeopleType").ToString() & ";" & DT.Rows(i)("PositionID").ToString() & ";" & DT.Rows(i)("EmployeeID").ToString()
            If (i <> DT.Rows.Count - 1) Then
                RetElements = RetElements & "|"
            End If
        Next i
        Return RetElements
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function ReturnStageActions(ByVal DocumentID As String) As String
        Dim _Dwf_DocumentStageAction As New ClsDwf_DocumentStageActions(New System.Web.UI.Page)
        _Dwf_DocumentStageAction.Find("DocumentID = " & Convert.ToInt32(DocumentID))
        Dim DT As New DataTable
        Dim RetElements = ""
        DT = _Dwf_DocumentStageAction.DataSet.Tables(0)
        For i As Integer = 0 To DT.Rows.Count - 1
            RetElements = RetElements & DT.Rows(i)("StageCode").ToString() & ";" & DT.Rows(i)("ActionID").ToString()
            If (i <> DT.Rows.Count - 1) Then
                RetElements = RetElements & "|"
            End If
        Next i
        Return RetElements
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function ReturnStageActionsNotify(ByVal DocumentID As String) As String
        Dim _Dwf_DocumentStageActionNotify As New ClsDwf_DocumentStageActionNotify(New System.Web.UI.Page)
        _Dwf_DocumentStageActionNotify.Find("DocumentID = " & Convert.ToInt32(DocumentID))
        Dim DT As New DataTable
        Dim RetElements = ""
        DT = _Dwf_DocumentStageActionNotify.DataSet.Tables(0)
        For i As Integer = 0 To DT.Rows.Count - 1
            RetElements = RetElements & DT.Rows(i)("StageCode").ToString() & ";" & DT.Rows(i)("ActionID").ToString() & ";" & DT.Rows(i)("RegComputerID").ToString() & ";" & DT.Rows(i)("NotifStage").ToString() & ";" & DT.Rows(i)("NotifyPosition").ToString() & ";" & DT.Rows(i)("NotifyEmployee").ToString() & ";" & DT.Rows(i)("Notification").ToString()
            If (i <> DT.Rows.Count - 1) Then
                RetElements = RetElements & "|"
            End If
        Next i
        Return RetElements
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function ReturnStageActionsPlugin(ByVal DocumentID As String) As String
        Dim _Dwf_DocumentStageActionPlugin As New ClsDwf_DocumentStageActionPlugin(New System.Web.UI.Page)
        _Dwf_DocumentStageActionPlugin.Find("DocumentID = " & Convert.ToInt32(DocumentID))
        Dim DT As New DataTable
        Dim RetElements = ""
        DT = _Dwf_DocumentStageActionPlugin.DataSet.Tables(0)
        For i As Integer = 0 To DT.Rows.Count - 1
            RetElements = RetElements & DT.Rows(i)("StageCode").ToString() & ";" & DT.Rows(i)("ActionID").ToString() & ";" & DT.Rows(i)("EventPluginID").ToString()
            If (i <> DT.Rows.Count - 1) Then
                RetElements = RetElements & "|"
            End If
        Next i
        Return RetElements
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function ReturnStageActionsPluginParam(ByVal DocumentID As String) As String
        Dim _Dwf_DocumentStageActionPluginPar As New ClsDwf_DocumentStageActionPluginPar(New System.Web.UI.Page)
        _Dwf_DocumentStageActionPluginPar.Find("DocumentID = " & Convert.ToInt32(DocumentID))
        Dim DT As New DataTable
        Dim RetElements = ""
        DT = _Dwf_DocumentStageActionPluginPar.DataSet.Tables(0)
        For i As Integer = 0 To DT.Rows.Count - 1
            RetElements = RetElements & DT.Rows(i)("StageCode").ToString() & ";" & DT.Rows(i)("ActionID").ToString() & ";" & DT.Rows(i)("EventPluginID").ToString() & ";" & DT.Rows(i)("EventPluginParameterID").ToString() & ";" & DT.Rows(i)("ElementCode").ToString()
            If (i <> DT.Rows.Count - 1) Then
                RetElements = RetElements & "|"
            End If
        Next i
        Return RetElements
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function ReturnDocumentInitInfo(ByVal DocumentID As String) As String
        Dim InitInfoString = Nothing
        Try
            Dim _Dwf_DocumentStageInitInfo As New ClsDwf_DocumentStageInitInfo(New System.Web.UI.Page)
            _Dwf_DocumentStageInitInfo.Find("DocumentID = " & Convert.ToInt32(DocumentID))
            Dim DT As DataTable = _Dwf_DocumentStageInitInfo.DataSet.Tables(0)
            If DT.Rows.Count > 0 Then
                Dim StrCommand As String = "set Dateformat DMY; select "
                For i As Integer = 0 To DT.Rows.Count - 1
                    _Dwf_DocumentStageInitInfo.Find("ID = " & DT.Rows(i)("ID"))
                    StrCommand = StrCommand & _Dwf_DocumentStageInitInfo.RelColumn
                    If i < DT.Rows.Count - 1 Then
                        StrCommand = StrCommand & ","
                    End If
                Next
                StrCommand = StrCommand & " from EmployeeFullDetails where ID = '" & RetStagePeople(DocumentID, ProfileCls.RetUserID().ToString()) & "'"
                Dim ResltDT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Dwf_DocumentStageInitInfo.ConnectionString, CommandType.Text, StrCommand).Tables(0)
                If ResltDT.Rows.Count > 0 Then
                    For i As Integer = 0 To DT.Rows.Count - 1
                        _Dwf_DocumentStageInitInfo.Find("ID = " & DT.Rows(i)("ID"))
                        InitInfoString = InitInfoString & _Dwf_DocumentStageInitInfo.ElementCode & ";" & ResltDT.Rows(0)(_Dwf_DocumentStageInitInfo.RelColumn).ToString()
                        If i < DT.Rows.Count - 1 Then
                            InitInfoString = InitInfoString & "|"
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Return Nothing
        End Try
        Return InitInfoString
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function FillSearchAtt(ByVal CtrlID As String, ByVal CtrlValue As String) As String
        Dim MainCol As String = ""
        Dim _Dwf_DocumentElement As New ClsDwf_DocumentElements(New System.Web.UI.Page)
        _Dwf_DocumentElement.Find("ID = " & Convert.ToInt32(CtrlID))
        Dim _Sys_Search As New Clssys_Searchs(New System.Web.UI.Page)
        _Sys_Search.Find("ID = " & _Dwf_DocumentElement.SearchID)
        Dim _Sys_Object As New Clssys_Objects(New System.Web.UI.Page)
        _Sys_Object.Find("ID = " & _Sys_Search.ObjectID)
        If _Sys_Object.Code.Contains("CF_") = False Then
            MainCol = "Code"
        Else
            Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
            _Dwf_Document.Find("Code = '" & _Sys_Object.Code.Replace("CF_", "") & "'")
            Dim DT01 As DataTable = _Dwf_Document.DataSet.Tables(0)
            If DT01.Rows.Count > 0 Then
                _Dwf_Document.Find("ID = " & DT01.Rows(0)("ID"))
                Dim _Dwf_DocumentElement01 As New ClsDwf_DocumentElements(New System.Web.UI.Page)
                _Dwf_DocumentElement01.Find("DocumentID = " & _Dwf_Document.ID & " and IsMainCode = 1")
                Dim DT02 As DataTable = _Dwf_DocumentElement01.DataSet.Tables(0)
                If DT02.Rows.Count > 0 Then
                    _Dwf_DocumentElement01.Find("ID = " & DT02.Rows(0)("ID"))
                    MainCol = _Dwf_DocumentElement01.Code
                End If
            End If
        End If
        If MainCol <> "" And _Dwf_DocumentElement.KeyTable <> "" Then
            Dim StrCommand As String = "select * from " & _Dwf_DocumentElement.KeyTable & " where " & MainCol & " = '" & CtrlValue & "'"
            Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Dwf_DocumentElement.ConnectionString, CommandType.Text, StrCommand).Tables(0)
            Dim strarray As String = ""
            If DT.Rows.Count > 0 Then
                Dim ElementsRelatedArr As String = _Dwf_DocumentElement.KeyRelated
                Dim ElementRelated As String() = ElementsRelatedArr.Split("^")
                For i As Integer = 0 To ElementRelated.Length - 1
                    Dim ElementRe As String() = ElementRelated(i).Split("$")
                    Dim ColName As String = ElementRe(0)
                    strarray = strarray + ElementRe(1) + "," + DT.Columns(ColName).DataType.ToString() + "," + DT.Rows(0)(ColName).ToString()
                    If i < ElementRelated.Length - 1 Then
                        strarray = strarray + "|"
                    End If
                Next i
                Return strarray
            Else
                Dim ElementsRelatedArr As String = _Dwf_DocumentElement.KeyRelated
                Dim ElementRelated As String() = ElementsRelatedArr.Split("^")
                For i As Integer = 0 To ElementRelated.Length - 1
                    Dim ElementRe As String() = ElementRelated(i).Split("$")
                    Dim ColName As String = ElementRe(0)
                    strarray = strarray + ElementRe(1) + "," + "" + "," + ""
                    If i < ElementRelated.Length - 1 Then
                        strarray = strarray + "|"
                    End If
                Next i
                Return strarray
            End If

        End If
        Return Nothing
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function CheckDate(ByVal Dat As String, ByVal Cal As String, ByVal Ctl As String) As String
        Dim DtCls As New HelperCls.DateCls
        If Cal = "H" Then
            If DtCls.IsHijri(Dat) = False Then
                Return Ctl
            End If
        End If
        If Cal = "G" Then
            If DtCls.IsGreg(Dat) = False Then
                Return Ctl
            End If
        End If
        If Cal = "B" Then
            If DtCls.IsGreg(Dat) = False And DtCls.IsHijri(Dat) = False Then
                Return Ctl
            End If
        End If
        Return Nothing
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function ReturnDocumentStageActions(ByVal DocumentID As String, ByVal Stage As String) As Object()
        'check workflow valid
        Dim _Dwf_DocumentStages As New ClsDwf_DocumentStages(New System.Web.UI.Page)
        _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID))
        Dim DTDocumentStages As DataTable = _Dwf_DocumentStages.DataSet.Tables(0)
        If DTDocumentStages.Rows.Count = 0 Then
            Return Nothing
        End If
        For i As Integer = 0 To DTDocumentStages.Rows.Count - 1
            _Dwf_DocumentStages = New ClsDwf_DocumentStages(New System.Web.UI.Page)
            _Dwf_DocumentStages.Find("ID = " & DTDocumentStages.Rows(i)("ID"))
            If _Dwf_DocumentStages.ElementType = "Start" And _Dwf_DocumentStages.ConnectorOut = "" Then
                Return Nothing
            ElseIf _Dwf_DocumentStages.ElementType = "End" And _Dwf_DocumentStages.ConnectorIn = "" Then
                Return Nothing
            ElseIf _Dwf_DocumentStages.ElementType = "Stage" Then
                If _Dwf_DocumentStages.ConnectorIn <> "" And _Dwf_DocumentStages.ConnectorOut <> "" Then
                    If _Dwf_DocumentStages.ConnectorIn.Replace("cont_", "") = _Dwf_DocumentStages.Code Or _Dwf_DocumentStages.ConnectorOut.Replace("cont_", "") = _Dwf_DocumentStages.Code Then
                        Return Nothing
                    End If
                Else
                    Return Nothing
                End If
            End If
        Next
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim _Dwf_DocumentStageAction As New ClsDwf_DocumentStageActions(New System.Web.UI.Page)
        If Stage <> "" Then
            _Dwf_DocumentStageAction.Find("StageCode = '" & Stage & "' and DocumentID = " & Convert.ToInt32(DocumentID))
        Else
            _Dwf_DocumentStages = New ClsDwf_DocumentStages(New System.Web.UI.Page)
            _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and ElementType = 'Start'")
            Dim DTDocumentStartStages As DataTable = _Dwf_DocumentStages.DataSet.Tables(0)
            If DTDocumentStartStages.Rows.Count > 0 Then
                _Dwf_DocumentStages = New ClsDwf_DocumentStages(New System.Web.UI.Page)
                _Dwf_DocumentStages.Find("ID = " & DTDocumentStartStages.Rows(0)("ID"))
                _Dwf_DocumentStageAction.Find("StageCode = '" & _Dwf_DocumentStages.ConnectorOut.Replace("cont_", "") & "' and DocumentID = " & Convert.ToInt32(DocumentID))

            End If
        End If
        Dim DTDocumentStagesActions As DataTable = _Dwf_DocumentStageAction.DataSet.Tables(0)
        Dim myobject(DTDocumentStagesActions.Rows.Count - 1) As Object
        For i As Integer = 0 To DTDocumentStagesActions.Rows.Count - 1
            _Dwf_DocumentStageAction = New ClsDwf_DocumentStageActions(New System.Web.UI.Page)
            _Dwf_DocumentStageAction.Find("ID = " & DTDocumentStagesActions.Rows(i)("ID"))
            myobject(i) = _Dwf_DocumentStageAction.ActionID
        Next i
        Return myobject
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function ReturnDocumentStageElements(ByVal DocumentID As String, ByVal Stage As String) As Object()
        Dim _Dwf_DocumentStageElement As New ClsDwf_DocumentStageElements(New System.Web.UI.Page)
        If Stage <> "" Then
            _Dwf_DocumentStageElement.Find("IsHide = 1 and DocumentID = " & Convert.ToInt32(DocumentID) & " and StageCode = '" & Stage & "'")
        Else
            Dim _Dwf_DocumentStages As New ClsDwf_DocumentStages(New System.Web.UI.Page)
            _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and ElementType = 'Start'")
            Dim DTDocumentStartStages As DataTable = _Dwf_DocumentStages.DataSet.Tables(0)
            If DTDocumentStartStages.Rows.Count > 0 Then
                _Dwf_DocumentStages.Find("ID = " & DTDocumentStartStages.Rows(0)("ID"))
                _Dwf_DocumentStageElement.Find("IsHide = 1 and DocumentID = " & Convert.ToInt32(DocumentID) & " and StageCode = '" & _Dwf_DocumentStages.ConnectorOut.Replace("cont_", "") & "'")
            End If
        End If
        Dim DTDocumentStagesElements As DataTable = _Dwf_DocumentStageElement.DataSet.Tables(0)
        Dim myobject(DTDocumentStagesElements.Rows.Count - 1) As Object
        For i As Integer = 0 To DTDocumentStagesElements.Rows.Count - 1
            _Dwf_DocumentStageElement.Find("ID = " & DTDocumentStagesElements.Rows(i)("ID"))
            myobject(i) = _Dwf_DocumentStageElement.ElementCode
        Next i
        Return myobject
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function ReturnDocumentStageElementsEna(ByVal DocumentID As String, ByVal Stage As String) As Object()
        Dim _Dwf_DocumentStageElement As New ClsDwf_DocumentStageElements(New System.Web.UI.Page)
        If Stage <> "" Then
            _Dwf_DocumentStageElement.Find("IsDisabled = 1 and DocumentID = " & Convert.ToInt32(DocumentID) & " and StageCode = '" & Stage & "'")
        Else
            Dim _Dwf_DocumentStages As New ClsDwf_DocumentStages(New System.Web.UI.Page)
            _Dwf_DocumentStages.Find("DocumentID = " & Convert.ToInt32(DocumentID) & " and ElementType = 'Start'")
            Dim DTDocumentStartStages As DataTable = _Dwf_DocumentStages.DataSet.Tables(0)
            If DTDocumentStartStages.Rows.Count > 0 Then
                _Dwf_DocumentStages.Find("ID = " & DTDocumentStartStages.Rows(0)("ID"))
                _Dwf_DocumentStageElement.Find("IsDisabled = 1 and DocumentID = " & Convert.ToInt32(DocumentID) & " and StageCode = '" & _Dwf_DocumentStages.ConnectorOut.Replace("cont_", "") & "'")
            End If
        End If
        Dim DTDocumentStagesElements As DataTable = _Dwf_DocumentStageElement.DataSet.Tables(0)
        Dim myobject(DTDocumentStagesElements.Rows.Count - 1) As Object
        For i As Integer = 0 To DTDocumentStagesElements.Rows.Count - 1
            _Dwf_DocumentStageElement.Find("ID = " & DTDocumentStagesElements.Rows(i)("ID"))
            myobject(i) = _Dwf_DocumentStageElement.ElementCode
        Next i
        Return myobject
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function ExecFunction(ByVal fnstring As String, ByVal CtrlName As String) As String
        Dim _Dwf_Document As New ClsDwf_Documents(New System.Web.UI.Page)
        Dim StrCommand As String = "set dateformat DMY; select " & fnstring
        Try
            Dim RetValue As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(_Dwf_Document.ConnectionString, CommandType.Text, StrCommand)
            Return CtrlName & "|" & RetValue
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class