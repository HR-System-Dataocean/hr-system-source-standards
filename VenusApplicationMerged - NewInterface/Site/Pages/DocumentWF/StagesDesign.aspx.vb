Imports System.Data
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class StagesDesign
    Inherits MainPage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        TBHEADER.Style("background-image") = IIf(ProfileCls.CurrentTheme().Contains("Blue") = True, "url('../../Common/Images/BlueHeader.png');", "url('../../Common/Images/SilverHeader.png');")
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Page.Request.QueryString("DocumentID") <> Nothing Then
                Dim _Dwf_DocumentElement As New ClsDwf_DocumentElements(Me.Page)
                _Dwf_DocumentElement.Find("DocumentID = " & Page.Request.QueryString("DocumentID"))
                Dim DTElements As DataTable = _Dwf_DocumentElement.DataSet.Tables(0)
                UWG_Elements.DataSource = DTElements
                UWG_Elements.DataBind()

                'add people type to grid
                DropDownList_PeopleType.Items.Add(New ListItem(IIf(ProfileCls.CurrentLanguage() = "En", "Direct Manager", "المدير المباشر"), 1))
                DropDownList_PeopleType.Items.Add(New ListItem(IIf(ProfileCls.CurrentLanguage() = "En", "Position", "درجة وظيفية"), 2))
                DropDownList_PeopleType.Items.Add(New ListItem(IIf(ProfileCls.CurrentLanguage() = "En", "Employee", "موظف"), 3))
                Dim _Clshrs_Positions As New Clshrs_Positions(Page)
                _Clshrs_Positions.Find("ID > 0")
                HelperCls.RetDropDownList(Me.DropDownList_Positions, _Clshrs_Positions.DataSet.Tables(0), "ID", IIf(ProfileCls.CurrentLanguage() = "Ar", "ArbName", "EngName")).DataBind()

                'add Action Notify Way to grid

                DropDownList_NotifTarget.Items.Add(New ListItem(IIf(ProfileCls.CurrentLanguage() = "En", "Stage", "مرحلة"), 1))
                DropDownList_NotifTarget.Items.Add(New ListItem(IIf(ProfileCls.CurrentLanguage() = "En", "Position", "درجة وظيفية"), 2))
                DropDownList_NotifTarget.Items.Add(New ListItem(IIf(ProfileCls.CurrentLanguage() = "En", "Employee", "موظف"), 3))
                DropDownList_NotifTarget.Items.Add(New ListItem(IIf(ProfileCls.CurrentLanguage() = "En", "Initiator", "منشئ الطلب"), 4))
                UWG_ActionNotify.DisplayLayout.Bands(0).Columns(0).ValueList.ValueListItems.Add(1, IIf(ProfileCls.CurrentLanguage() = "En", "Stage", "مرحلة"))
                UWG_ActionNotify.DisplayLayout.Bands(0).Columns(0).ValueList.ValueListItems.Add(2, IIf(ProfileCls.CurrentLanguage() = "En", "Position", "درجة وظيفية"))
                UWG_ActionNotify.DisplayLayout.Bands(0).Columns(0).ValueList.ValueListItems.Add(3, IIf(ProfileCls.CurrentLanguage() = "En", "Employee", "موظف"))
                UWG_ActionNotify.DisplayLayout.Bands(0).Columns(0).ValueList.ValueListItems.Add(4, IIf(ProfileCls.CurrentLanguage() = "En", "Initiator", "منشئ الطلب"))

                DropDownList_NotifWays.Items.Add(New ListItem(IIf(ProfileCls.CurrentLanguage() = "En", "By E-Mail", "البريد الإلكترونى"), 1))
                DropDownList_NotifWays.Items.Add(New ListItem(IIf(ProfileCls.CurrentLanguage() = "En", "By SMS", "رسائل جوال"), 2))
                DropDownList_NotifWays.Items.Add(New ListItem(IIf(ProfileCls.CurrentLanguage() = "En", "By Notification System", "برنامج التنبيهات"), 3))
                UWG_ActionNotify.DisplayLayout.Bands(0).Columns(4).ValueList.ValueListItems.Add(1, IIf(ProfileCls.CurrentLanguage() = "En", "By E-Mail", "البريد الإلكترونى"))
                UWG_ActionNotify.DisplayLayout.Bands(0).Columns(4).ValueList.ValueListItems.Add(2, IIf(ProfileCls.CurrentLanguage() = "En", "By SMS", "رسائل جوال"))
                UWG_ActionNotify.DisplayLayout.Bands(0).Columns(4).ValueList.ValueListItems.Add(3, IIf(ProfileCls.CurrentLanguage() = "En", "By Notification System", "برنامج التنبيهات"))

                HelperCls.RetDropDownList(Me.DropDownList_NotiPositions, _Clshrs_Positions.DataSet.Tables(0), "ID", IIf(ProfileCls.CurrentLanguage() = "Ar", "ArbName", "EngName")).DataBind()

                'add people type to grid
                UWG_Peoples.DisplayLayout.Bands(0).Columns(0).ValueList.ValueListItems.Add(1, IIf(ProfileCls.CurrentLanguage() = "En", "Direct Manager", "المدير المباشر"))
                UWG_Peoples.DisplayLayout.Bands(0).Columns(0).ValueList.ValueListItems.Add(2, IIf(ProfileCls.CurrentLanguage() = "En", "Position", "درجة وظيفية"))
                UWG_Peoples.DisplayLayout.Bands(0).Columns(0).ValueList.ValueListItems.Add(3, IIf(ProfileCls.CurrentLanguage() = "En", "Employee", "موظف"))
                'add Positions to grid
                For i As Integer = 0 To _Clshrs_Positions.DataSet.Tables(0).Rows.Count - 1
                    Try
                        UWG_Peoples.DisplayLayout.Bands(0).Columns(1).ValueList.ValueListItems.Add(_Clshrs_Positions.DataSet.Tables(0).Rows(i)("ID"), _Clshrs_Positions.DataSet.Tables(0).Rows(i)(IIf(ProfileCls.CurrentLanguage() = "Ar", "ArbName", "EngName")))
                        UWG_ActionNotify.DisplayLayout.Bands(0).Columns(2).ValueList.ValueListItems.Add(_Clshrs_Positions.DataSet.Tables(0).Rows(i)("ID"), _Clshrs_Positions.DataSet.Tables(0).Rows(i)(IIf(ProfileCls.CurrentLanguage() = "Ar", "ArbName", "EngName")))
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
                'add UWG Actions
                Dim _Dwf_ActionType As New ClsDwf_ActionTypes(Me.Page)
                _Dwf_ActionType.Find("ID > 0")
                Dim ActionsDT As New Data.DataTable
                ActionsDT = _Dwf_ActionType.DataSet.Tables(0)
                For i As Integer = 0 To ActionsDT.Rows.Count - 1
                    UWG_Actions.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {False, ActionsDT.Rows(i)("ID").ToString(), IIf(ProfileCls.CurrentLanguage() = "En", ActionsDT.Rows(i)("EngName").ToString(), ActionsDT.Rows(i)("ArbName").ToString())}))
                Next
                'add UWG InitInfo
                Dim StrCommand As String = "select column_name from information_schema.columns where table_name =  'EmployeeFullDetails'"
                Dim InitInfoDataset As New Data.DataSet
                InitInfoDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Clshrs_Positions.ConnectionString, System.Data.CommandType.Text, StrCommand)
                For i As Integer = 0 To InitInfoDataset.Tables(0).Rows.Count - 1
                    Try
                        UWG_InitInfo.DisplayLayout.Bands(0).Columns(1).ValueList.ValueListItems.Add(InitInfoDataset.Tables(0).Rows(i)("column_name"), InitInfoDataset.Tables(0).Rows(i)("column_name"))
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
                Dim DocumentElementDT As Data.DataTable = _Dwf_DocumentElement.DataSet.Tables(0)
                For i As Integer = 0 To DocumentElementDT.Rows.Count - 1
                    Try
                        If DocumentElementDT.Rows(i)("ElementType") = "TextBox" Or DocumentElementDT.Rows(i)("ElementType") = "TextArea" Or DocumentElementDT.Rows(i)("ElementType") = "ComboBox" Then
                            UWG_InitInfo.DisplayLayout.Bands(0).Columns(0).ValueList.ValueListItems.Add(DocumentElementDT.Rows(i)("Code"), DocumentElementDT.Rows(i)("FriendlyName"))
                        End If
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
                StrCommand = "select A.Code,B.RelColumn from Dwf_DocumentElements A left outer join Dwf_DocumentStageInitInfo B on A.Code = B.ElementCode and A.DocumentID = B.DocumentID where A.ElementType in ('TextBox','TextArea','ComboBox') and A.DocumentID = " & _Dwf_DocumentElement.DocumentID
                InitInfoDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Clshrs_Positions.ConnectionString, System.Data.CommandType.Text, StrCommand)
                For i As Integer = 0 To InitInfoDataset.Tables(0).Rows.Count - 1
                    Try
                        UWG_InitInfo.DisplayLayout.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {InitInfoDataset.Tables(0).Rows(i)("Code"), InitInfoDataset.Tables(0).Rows(i)("RelColumn")}))
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
                'add UWG ActionEvent
                UWG_ActionPlugin.DataSource = Nothing
                UWG_ActionPlugin.DataBind()
                UWG_ActionPlugin.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
                For i As Integer = 0 To DTElements.Rows.Count - 1
                    Try
                        UWG_ActionPlugin.DisplayLayout.Bands(1).Columns(3).ValueList.ValueListItems.Add(DTElements.Rows(i)("Code"), DTElements.Rows(i)("FriendlyName"))
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
                Dim _Dwf_EventPlugin As New ClsDwf_EventPlugins(Me.Page)
                _Dwf_EventPlugin.Find("ID > 0")
                Dim DTEventPlugin As DataTable = _Dwf_EventPlugin.DataSet.Tables(0)
                DTEventPlugin.TableName = "DTEventPlugin"
                Dim _Dwf_EventPluginParameter As New ClsDwf_EventPluginParameters(Me.Page)
                _Dwf_EventPluginParameter.Find("ID > 0")
                Dim DTPluginParameter As DataTable = _Dwf_EventPluginParameter.DataSet.Tables(0)
                DTPluginParameter.TableName = "DTPluginParameter"
                Dim dt1 As New DataTable("Table1")
                dt1 = DTEventPlugin.Copy()

                Dim dt2 As New DataTable("Table2")
                dt2 = DTPluginParameter.Copy()

                Dim DSAll As New DataSet()
                DSAll.Tables.Add(dt1)
                DSAll.Tables.Add(dt2)
                Dim DataCol1 As Data.DataColumn
                Dim DataCol2 As Data.DataColumn
                DataCol1 = DSAll.Tables(0).Columns("ID")
                DataCol2 = DSAll.Tables(1).Columns("PluginID")
                Dim Rel1 As Data.DataRelation = New Data.DataRelation("Rel1", DataCol1, DataCol2, False)
                DSAll.Relations.Add(Rel1)
                UWG_ActionPlugin.DataSource = DSAll
                UWG_ActionPlugin.DataBind()
            End If
        End If
    End Sub
End Class
