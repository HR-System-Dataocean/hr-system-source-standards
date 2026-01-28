<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAppraisalCriterias_new.aspx.vb" Inherits="frmProfession"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Professions</title>
    <script language="javascript" src="Scripts/App_JScript.js"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js"></script>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js"></script>

    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        // flag to add listener for resize events
        var _onloadFlag = true;
        function adjustHeight() {
            var myHeight = 0;
            if (typeof (window.innerWidth) == 'number') {
                myHeight = window.innerHeight;
            } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
                myHeight = document.documentElement.clientHeight;
            } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
                myHeight = document.body.clientHeight;
            }
            var tab = igtab_getTabById('UltraWebTab1');
            // <td> which is used as content pane
            var cp = document.getElementById(tab.ID + '_cp');
            // <table> of tab
            var table = tab.element;
            // <div> container of tab
            var container = table.parentNode;
            // height available for tab
            var height = container.clientHeight;
            height = (myHeight - 85);
            if (!height) return;
            // difference between heights of tab and content pane
            var heightShift = tab._myHeightShift;
            // 4 - is adjustment for top/bottom borders of tab
            if (!heightShift)
                heightShift = tab._myHeightShift = (table.offsetHeight - cp.offsetHeight + 4);
            // calculate height for content pane (can be improved for different browsers)
            height -= heightShift;
            if (height < 0) return;
            // set height of content pane to make height of tab to fit with container
            if (table.offsetHeight < (myHeight - 85)) {
                cp.style.height = height + 'px';
            }
            if (!_onloadFlag)
                return;
            _onloadFlag = false;
            // process onresize events
            ig_shared.addEventListener(window, 'resize', adjustHeight);
        }
        var ODialoge;
        var OSender;
        var IsEdit = true;
        function UwgSearchEmployees_AfterCellUpdateHandler(gridName, cellId) {
            var grid = igtbl_getGridById(gridName);
            var gridLength = grid.Rows.length;
            var cell = igtbl_getCellById(cellId);
            var row = cell.getRow();
            var blSign = cell.getValue();
            var CellToChange;
            if (IsEdit) {
                if (row.Id == gridName + "_r_0") {
                    IsEdit = false;
                    for (i = 0; i < gridLength; i++) {
                        var currRow = grid.Rows.rows[i];
                        var currRow = igtbl_getCellById(gridName + "_r" + i);
                        CellToChange = igtbl_getCellById(gridName + "_rc_" + i + "_1");
                        CellToChange.setValue(blSign)
                    }
                }
                else {
                    IsEdit = false;
                    CellToChange = igtbl_getCellById(gridName + "_rc_0_1");
                    CellToChange.setValue(false)
                }
                IsEdit = true;
            }
        }

        var ODialoge;
        var OSender;
        function OpenModal1(pageurl, height, width, CheckID, SenderCtrl) {
            if (CheckID == false) {
                var page = pageurl;
                var $dialog = $('<div></div>')
               .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
               .dialog({
                   autoOpen: false,
                   modal: true,
                   height: height,
                   width: width
               });
                ODialoge = $dialog;
                OSender = SenderCtrl;
                $dialog.dialog('open');
            }
        }
        function CloseIt(retvalue) {
            if (retvalue != "") {
                var Sender = window.document.getElementById(OSender);
                Sender.value = retvalue;
                Sender.focus();
            }
            var $dialog = ODialoge;
            $dialog.dialog('close');
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmProfession" runat="server">
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px" meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px" meta:resourcekey="TargetControlResource1"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display:none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N" meta:resourcekey="ImageButton1Resource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                    SkinID="HrSave_Command" meta:resourcekey="ImageButton_SaveResource1" CommandArgument="Save"
                                    OnClientClick="SaveOtherFieldsData();" />
                            </td>
                            <td style="width: 120px">
                                <asp:ImageButton ID="ImageButton_SaveN" Width="16px" Height="16px" runat="server"
                                    CommandArgument="SaveNew" SkinID="HrSaveN_Command" meta:resourcekey="ImageButton_SaveNResource1"
                                    OnClientClick="SaveOtherFieldsData();" />
                                <asp:LinkButton ID="LinkButton_SaveN" runat="server" Text="حفظ مع جديد" meta:resourcekey="LinkButton_SaveNResource1"
                                    CommandArgument="SaveNew" OnClientClick="SaveOtherFieldsData();"></asp:LinkButton>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                    meta:resourcekey="ImageButton_NewResource1" CommandArgument="New" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server"
                                    SkinID="HrDelete_Command" meta:resourcekey="ImageButton_DeleteResource1" CommandArgument="Delete" />
                            </td>
                            <td style="width: 40px">
                                <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                    SkinID="HrPrint_Command" meta:resourcekey="ImageButton_PrintResource1" CommandArgument="Print" />
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Properties" Width="16px" Height="16px" runat="server"
                                    SkinID="HrProperties_Command" meta:resourcekey="ImageButton_PropertiesResource1"
                                    CommandArgument="Property" />
                                <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="خصائص" meta:resourcekey="LinkButton_PropertiesResource1"
                                    CommandArgument="Property"></asp:LinkButton>
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                    SkinID="HrRemarks_Command" meta:resourcekey="ImageButton_RemarksResource1" CommandArgument="Remarks" />
                                <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="ملاحظات" meta:resourcekey="LinkButton_RemarksResource1"
                                    CommandArgument="Remarks"></asp:LinkButton>
                            </td>
                            <td style="width: 40px">
                                <asp:Label ID="Label_TSP2" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Last" Width="16px" Height="16px" runat="server"
                                    SkinID="HrLast_Command" meta:resourcekey="ImageButton_LastResource1" CommandArgument="Last" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Next" Width="16px" Height="16px" runat="server"
                                    SkinID="HrNext_Command" meta:resourcekey="ImageButton_NextResource1" CommandArgument="Next" />
                            </td>
                            <td style="width: 10px">
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Back" Width="16px" Height="16px" runat="server"
                                    SkinID="HrBack_Command" meta:resourcekey="ImageButton_BackResource1" CommandArgument="Previous" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_First" Width="16px" Height="16px" runat="server"
                                    SkinID="HrFirest_Command" meta:resourcekey="ImageButton_FirstResource1" CommandArgument="First" />
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 5%">
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; height: 42px; vertical-align: top">
                        <tr>
                            <td style="width: 32px; vertical-align: top">
                                <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png"
                                    meta:resourcekey="Image_LogoResource1" />
                            </td>
                            <td style="width: 50%; vertical-align: middle">
                                <asp:Label ID="Label_Header" runat="server" meta:resourcekey="Label_HeaderResource1"></asp:Label>
                            </td>
                            <td style="width: 50%; vertical-align: middle">
                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                    <tr>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegDate" runat="server" Text="سجل فى" SkinID="Label_CopyRightsBold"
                                                            meta:resourcekey="lblRegDateResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegDateValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegDateValueResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 2%; vertical-align: top">
                                        </td>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegUser" runat="server" Text="سجل بواسطة" SkinID="Label_CopyRightsBold"
                                                            meta:resourcekey="lblRegUserResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegUserValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegUserValueResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblCancelDate" runat="server" Text="تاريخ الالغاء" SkinID="Label_CopyRightsBold"
                                                            meta:resourcekey="lblCancelDateResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblCancelDateValue" runat="server" SkinID="Label_CopyRightsNormal"
                                                            meta:resourcekey="lblCancelDateValueResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 2%; vertical-align: top">
                                        </td>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="Details">
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                        meta:resourcekey="UltraWebTab1Resource1">
                        <Tabs>
                            <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                    <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                        cellspacing="0">
                                        <tr>
                                            <td style="height: 10px" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblCode" runat="server" Text="الكود" SkinID="Label_DefaultNormal"    meta:resourcekey="lblCodeResource1"></asp:Label>
                                                            <asp:Label ID="Label_Star1" runat="server" Text="*" Style="color: #FF0000" meta:resourcekey="Label_Star1Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataAreawithsearch">
                                                            <asp:TextBox ID="txtCode" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                AutoPostBack="True" meta:resourcekey="txtCodeResource1"></asp:TextBox>
                                                        </td>
                                                        <td class="search">
                                                            <igtxt:WebImageButton ID="btnSearchCode" runat="server" AutoSubmit="False" Height="18px"
                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnSearchCodeResource1">
                                                                <Alignments TextImage="ImageBottom" />
                                                                <Appearance>
                                                                    <Image Url="./Img/forum_search.gif" />
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblEngName" runat="server" Text="التوصيف إنجليزى" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                            <%--                <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalltr" MaxLength="700"
                                                                  height="30px" TextMode="MultiLine" Rows="5" Columns="50" meta:resourcekey="txtEngNameResource1"></asp:TextBox>--%>
                                                <asp:TextBox ID="txtEngName" runat="server" TextMode="MultiLine" Rows="2" Width="500px"  />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                       
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblArbName" runat="server" Text="التوصيف عربى" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblArbNameResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                      <%--      <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                                                              TextMode="MultiLine" Rows="5" Columns="50"  meta:resourcekey="txtArbNameResource1"></asp:TextBox>--%>
                                                              <asp:TextBox ID="txtArbName" runat="server" TextMode="MultiLine" Rows="2" Width="500px"  />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                       
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                                                                                                                            <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="LblCriteriaGroipType" runat="server" Text="Group Type" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="LblCriteriaGroipTypeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                          <asp:DropDownList ID="ddlCriteriaGroupType" Width="200px" runat="server" AutoPostBack="true" SkinID="DropDownList_smallNormal"
    meta:resourcekey="ddlCriteriaGroipTypeResource1" TabIndex="3">
</asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="LblCriteriaGroup" runat="server" Text="المجموعة" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="LblCriteriaGroupResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                          <asp:DropDownList ID="ddlCriteriaGroup" Width="200px" runat="server" AutoPostBack="true" SkinID="DropDownList_smallNormal"
    meta:resourcekey="ddlFormCodeResource1" TabIndex="3">
</asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

<%--                                            <tr>
        <td style="width: 20%; height: 16px; vertical-align: top">
            <table style="width: 100%; vertical-align: top" cellspacing="0">
                <tr>
                    <td class="SeparArea">
                    </td>
                    <td class="LabelArea">
                        <asp:Label ID="LblMinimumScore" runat="server" Text="Default Minimum Score" SkinID="Label_DefaultNormal"
                            meta:resourcekey="LblMinimumScoreResource1"></asp:Label>
                    </td>
                    <td class="DataArea">
                        <asp:TextBox ID="txtminimumscore" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                            meta:resourcekey="txtArbNameResource1"></asp:TextBox>
                    </td>
 
                </tr>
                                <tr>
                    <td class="SeparArea">
                     

                    <td class="LabelArea">
    <asp:Label ID="Lblmaximumscore" runat="server" Text="Default Maximum Score" SkinID="Label_DefaultNormal"
        meta:resourcekey="LblmaximumscoreResource1"></asp:Label>
</td>
<td class="DataArea">
    <asp:TextBox ID="TxtMaximumScore" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
        meta:resourcekey="txtArbNameResource1"></asp:TextBox>
</td>
                </tr>
                                </tr>
                                <tr>
                    <td class="SeparArea">
                     

                    <td class="LabelArea">
    <asp:Label ID="lblDefaultWeight" runat="server" Text="Default Weight" SkinID="Label_DefaultNormal"
        meta:resourcekey="lblDefaultWeightResource1"></asp:Label>
</td>
<td class="DataArea">
    <asp:TextBox ID="TxtDefaultWeight" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
        meta:resourcekey="txtArbNameResource1"></asp:TextBox>
</td>
                </tr>

                                                                        <tr>
                    <td class="SeparArea">
                     

                    <td class="LabelArea">
    <asp:Label ID="lblApplyDefault" runat="server" Text=" Apply Default  " SkinID="Label_DefaultNormal"
        meta:resourcekey="lblDefaultWeightResource1"></asp:Label>
</td>
<td class="DataArea">
      <asp:CheckBox ID="ChkApplyDefault" runat="server" meta:resourcekey="ChkApplyDefaultResource1"
AutoPostBack="true" />
</td>
                </tr>--%>
            </table>
        </td>
        <td style="width: 6%; height: 16px; vertical-align: top">
        </td>
        <td style="width: 20%; height: 16px; vertical-align: top">
            <table style="width: 100%; vertical-align: top" cellspacing="0">
                <tr>
                    <td class="SeparArea">
                    </td>
                    <td class="LabelArea">
                    </td>
                    <td class="DataArea">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
<%--
                                          <tr>
      <td style="vertical-align: top;" colspan="3">
          <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgPositions" runat="server" meta:resourcekey="uwgPositionsResource1"
              SkinID="Default" Width="325px" EnableTheming="True">
              <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="None" AllowDeleteDefault="No"
                  AllowSortingDefault="Yes"  AllowUpdateDefault="No" AutoGenerateColumns="False"
                 
                   BorderCollapseDefault="Separate" HeaderClickActionDefault="Select" Name="uwgForNationality"
                  RowHeightDefault="18px" SelectTypeRowDefault="None" StationaryMargins="No" StationaryMarginsOutlookGroupBy="False"
                  TableLayout="Auto" Version="4.00" ViewType="Flat" AllowRowNumberingDefault="Continuous"
                  LoadOnDemand="Automatic">
                  <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                      BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Width="325px">
                  </FrameStyle>
                  <ClientSideEvents AfterCellUpdateHandler="UwgSearchEmployees_AfterCellUpdateHandler"
                      ClickCellButtonHandler="UwgSearchEmployees_ClickCellButtonHandler" />
                  <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                  </EditCellStyleDefault>
                  <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                      <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                  </FooterStyleDefault>
                  <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                      Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                      <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                  </HeaderStyleDefault>
                  <RowSelectorStyleDefault Width="40px" Font-Names="Arial" Font-Size="7pt">
                  </RowSelectorStyleDefault>
                  <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                      Font-Names="tahoma" Font-Size="8pt" Height="18px">
                      <Padding Left="3px" />
                      <BorderDetails ColorLeft="Window" ColorTop="Window" />
                  </RowStyleDefault>
                  <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                  </GroupByRowStyleDefault>
                  <GroupByBox Hidden="True">
                      <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                      </BoxStyle>
                  </GroupByBox>
                  <AddNewBox>
                      <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                          <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                      </BoxStyle>
                  </AddNewBox>
                  <ActivationObject BorderColor="" BorderWidth="">
                  </ActivationObject>
                  <FilterOptionsDefault>
                      <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                          CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                          Font-Size="11px" Height="300px" Width="200px">
                          <Padding Left="2px" />
                      </FilterDropDownStyle>
                      <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                      </FilterHighlightRowStyle>
                      <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                          BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                          Font-Size="11px">
                          <Padding Left="2px" />
                      </FilterOperandDropDownStyle>
                  </FilterOptionsDefault>
              </DisplayLayout>
              <Bands>
                  <igtbl:UltraGridBand AllowSorting="No" meta:resourcekey="UltraGridBandResource1">
                      <AddNewRow View="NotSet" Visible="NotSet">
                      </AddNewRow>
                      <FilterOptions EmptyString="" AllString="" NonEmptyString="">
                          <FilterDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px"
                              Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="SteelBlue" Width="200px"
                              CustomRules="overflow:auto;">
                              <Padding Left="2px"></Padding>            
                          </FilterDropDownStyle>
                          <FilterHighlightRowStyle ForeColor="White" BackColor="#151C55">
                          </FilterHighlightRowStyle>
                      </FilterOptions>
                      <Columns>
                          <igtbl:UltraGridColumn BaseColumnName="PositionID" Key="PositionID" hidden="true"  meta:resourcekey="PositionIDResource1">
                              <Header Caption="PositionID">
                              </Header>
                          </igtbl:UltraGridColumn>
                          <igtbl:UltraGridColumn Width="23px" BaseColumnName="Select" key="Select" AllowUpdate="Yes" Type="CheckBox" meta:resourcekey="UltraGridColumnResource2">
                              <Header Caption="√">
                                  <RowLayoutColumnInfo OriginX="1" />
                              </Header>
                              <HeaderStyle HorizontalAlign="Center" />
                              <CellStyle HorizontalAlign="Center">
                              </CellStyle>
                              <Footer>
                                  <RowLayoutColumnInfo OriginX="1" />
                              </Footer>
                          </igtbl:UltraGridColumn>
                          <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Code" Key="Code" Width="90px"
                              meta:resourcekey="UltraGridColumnResource3">
                              <Header Caption="Position Code">
                                  <RowLayoutColumnInfo OriginX="2" />
                              </Header>
                              <CellStyle HorizontalAlign="Center">
                              </CellStyle>
                              <Footer>
                                  <RowLayoutColumnInfo OriginX="2" />
                              </Footer>
                          </igtbl:UltraGridColumn>
                          <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="PositionName" Key="FullName"
                              Width="50%" meta:resourcekey="UltraGridColumnResource4">
                              <Header Caption="Position Name">
                                  <RowLayoutColumnInfo OriginX="3" />
                              </Header>
                              <Footer>
                                  <RowLayoutColumnInfo OriginX="3" />
                              </Footer>
                          </igtbl:UltraGridColumn>
                          <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="MinimumScore" Key="MinimumScore" 
                              Width="10%" meta:resourcekey="PaidColumn">
                              <Header Caption="Minimum Score">
                                  <RowLayoutColumnInfo OriginX="3" />
                              </Header>
                              <Footer>
                                  <RowLayoutColumnInfo OriginX="3" />
                              </Footer>
                          </igtbl:UltraGridColumn>


                          <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="MaximumScore" Key="MaximumScore"
    Width="10%" meta:resourcekey="MaximumScore">
    <Header Caption="Maximum Score">
        <RowLayoutColumnInfo OriginX="3" />
    </Header>
    <Footer>
        <RowLayoutColumnInfo OriginX="3" />
    </Footer>
</igtbl:UltraGridColumn>
                          <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Weight" Key="Weight"
    Width="10%" meta:resourcekey="PaidColumn">
    <Header Caption="Weight">
        <RowLayoutColumnInfo OriginX="3" />
    </Header>
    <Footer>
        <RowLayoutColumnInfo OriginX="3" />
    </Footer>
</igtbl:UltraGridColumn>

                         
                      </Columns>
                  </igtbl:UltraGridBand>
              </Bands>
          </igtbl:UltraWebGrid>
      </td>
  </tr>--%>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            &nbsp;
                                                        </td>
                                                        <td class="DataArea">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%" colspan="3">
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                        </Tabs>
                    </igtab:UltraWebTab>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
