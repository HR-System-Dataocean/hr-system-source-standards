<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPositions.aspx.vb" Inherits="frmPositions" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Positions</title>
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
        $(function () {
            var icons = {
                header: "",
                activeHeader: ""
            };
            $("#accordion").accordion({ icons: icons, autoHeight: false });
            $("#accordion").accordion("option", "icons", "");
        });
    </script>
    <script type="text/javascript" id="igClientScript">
        function uwg_AfterExitEditModeHandler(gridName, cellId) {
            var count = igtbl_getGridById(gridName).Rows.length - 1;
            var rowIndex = igtbl_getRowById(cellId).Id.split("_")[2];
            if (rowIndex == count) {
                igtbl_addNew(gridName, 0, true, false);
            }
        }
        function uwgAccountability_AfterRowActivateHandler(gridName, rowId) {
            var Row = igtbl_getRowById(rowId);
            var ultraTab = igtab_getTabById("UltraWebTab1");
            var _ID = igtab_getElementById("HiddenField_Accountability", ultraTab.element);
            var AccountabilityID = Row.getCellFromKey("ID").getValue()
            if (AccountabilityID == null) {
                var date = new Date();
                Row.getCellFromKey("ID").setValue(date.getTime());
                _ID.value = Row.getCellFromKey("ID").getValue();
            }
            else {
                _ID.value = AccountabilityID;
            }

        }
        function uwgAccountability1_AfterRowActivateHandler(gridName, rowId) {
            var Row = igtbl_getRowById(rowId);
            var ultraTab = igtab_getTabById("UltraWebTab1");
            var _ID = igtab_getElementById("HiddenField_Accountability", ultraTab.element);
            Row.getCellFromKey("PosAccountabilityID").setValue(_ID.value);
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmPositions" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="display: none">
            <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px" meta:resourcekey="nameResource1"></asp:Label>
            <asp:Label ID="realname" runat="server" ForeColor="White"
                TabIndex="-1" Width="99px" meta:resourcekey="realnameResource1"></asp:Label>
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
                                        SkinID="HrSave_Command" CommandArgument="Save"
                                        OnClientClick="SaveOtherFieldsData();" meta:resourcekey="ImageButton_SaveResource1" />
                                </td>
                                <td style="width: 120px">
                                    <asp:ImageButton ID="ImageButton_SaveN" Width="16px" Height="16px" runat="server"
                                        CommandArgument="SaveNew" SkinID="HrSaveN_Command"
                                        OnClientClick="SaveOtherFieldsData();" meta:resourcekey="ImageButton_SaveNResource1" />
                                    <asp:LinkButton ID="LinkButton_SaveN" runat="server" Text="حفظ مع جديد" CommandArgument="SaveNew"
                                        OnClientClick="SaveOtherFieldsData();" meta:resourcekey="LinkButton_SaveNResource1"></asp:LinkButton>
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                        CommandArgument="New" meta:resourcekey="ImageButton_NewResource1" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server"
                                        SkinID="HrDelete_Command" CommandArgument="Delete" meta:resourcekey="ImageButton_DeleteResource1" />
                                </td>
                                <td style="width: 40px">
                                    <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                        SkinID="HrPrint_Command" CommandArgument="Print" meta:resourcekey="ImageButton_PrintResource1" />
                                </td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Properties" Width="16px" Height="16px" runat="server"
                                        SkinID="HrProperties_Command" meta:resourcekey="ImageButton_PropertiesResource1"
                                        CommandArgument="Property" />
                                    <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="خصائص"
                                        CommandArgument="Property" meta:resourcekey="LinkButton_PropertiesResource1"></asp:LinkButton>
                                </td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                        SkinID="HrRemarks_Command" meta:resourcekey="ImageButton_RemarksResource1" CommandArgument="Remarks" />
                                    <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="ملاحظات"
                                        CommandArgument="Remarks" meta:resourcekey="LinkButton_RemarksResource1"></asp:LinkButton>
                                </td>
                                <td style="width: 40px">
                                    <asp:Label ID="Label_TSP2" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Last" Width="16px" Height="16px" runat="server"
                                        SkinID="HrLast_Command" CommandArgument="Last" meta:resourcekey="ImageButton_LastResource1" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Next" Width="16px" Height="16px" runat="server"
                                        SkinID="HrNext_Command" CommandArgument="Next" meta:resourcekey="ImageButton_NextResource1" />
                                </td>
                                <td style="width: 10px"></td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Back" Width="16px" Height="16px" runat="server"
                                        SkinID="HrBack_Command" CommandArgument="Previous" meta:resourcekey="ImageButton_BackResource1" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_First" Width="16px" Height="16px" runat="server"
                                        SkinID="HrFirest_Command" CommandArgument="First" meta:resourcekey="ImageButton_FirstResource1" />
                                </td>
                                <td style="width: 30%"></td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                        SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                    <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
                                </td>
                                <td style="width: 5%"></td>
                            </tr>
                        </table>
                        <table style="width: 100%; height: 42px; vertical-align: top">
                            <tr>
                                <td style="width: 32px; vertical-align: top">
                                    <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png" meta:resourcekey="Image_LogoResource1" />
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
                                                            <asp:Label ID="lblRegDate" runat="server" Text="سجل فى" SkinID="Label_CopyRightsBold" meta:resourcekey="lblRegDateResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                            <asp:Label ID="lblRegDateValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegDateValueResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 2%; vertical-align: top"></td>
                                            <td style="width: 49%; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                            <asp:Label ID="lblRegUser" runat="server" Text="سجل بواسطة" SkinID="Label_CopyRightsBold" meta:resourcekey="lblRegUserResource1"></asp:Label>
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
                                                            <asp:Label ID="lblCancelDate" runat="server" Text="تاريخ الالغاء" SkinID="Label_CopyRightsBold" meta:resourcekey="lblCancelDateResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                            <asp:Label ID="lblCancelDateValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblCancelDateValueResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 2%; vertical-align: top"></td>
                                            <td style="width: 49%; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 40%; height: 16px; vertical-align: middle;"></td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;"></td>
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
                        <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default" meta:resourcekey="UltraWebTab1Resource1">
                            <Tabs>
                                <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                    <ContentTemplate>
                                        <table style="width: 65%; vertical-align: top"
                                            cellspacing="0">
                                            <tr>
                                                <td style="height: 10px" colspan="3"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblCode" runat="server" Text="الكود" SkinID="Label_DefaultNormal" meta:resourcekey="lblCodeResource1"></asp:Label>
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
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea"></td>
                                                            <td class="DataArea"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblEngName" runat="server" Text="التوصيف إنجليزى" SkinID="Label_DefaultNormal" meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalltr" MaxLength="255" meta:resourcekey="txtEngNameResource1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea"></td>
                                                            <td class="DataArea"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblArbName" runat="server" Text="التوصيف عربى" SkinID="Label_DefaultNormal" meta:resourcekey="lblArbNameResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255" meta:resourcekey="txtArbNameResource1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea"></td>
                                                            <td class="DataArea"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Label ID="Label1" runat="server" SkinID="Label_DefaultBold"
                                                                    Text="الإعدادات" meta:resourcekey="Label1Resource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea"></td>
                                                            <td class="DataArea"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblParentPosition" runat="server" SkinID="Label_DefaultNormal" Text="المستوى الرئيسي" meta:resourcekey="lblParentPositionResource1" ></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="DdlParentPosition" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="DdlParentPositionResource1" >
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblappraisaltypegroup" runat="server" SkinID="Label_DefaultNormal" Text="مجموعة التقييم" meta:resourcekey="lblappraisaltypegroupResource1" ></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlappraisaltypegroup" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="lblappraisaltypegroupResource1" >
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
    <td class="SeparArea"></td>
    <td class="LabelArea">
        <asp:Label ID="lblPositionLevel" runat="server" SkinID="Label_DefaultNormal" Text="درجة المستوى" meta:resourcekey="lblPositionLevelResource1" ></asp:Label>
    </td>
    <td class="DataArea">
        <asp:DropDownList ID="DdlPositionLevel" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="DdlPositionLevelResource1">
        </asp:DropDownList>
    </td>
     <td class="LabelArea">

</tr>
                                                        <tr>
   <td class="SeparArea"></td>

    <td class="LabelArea">
          <asp:Label ID="lblNoOfEmployees" runat="server" SkinID="Label_DefaultNormal" Text="No.Of Employees" meta:resourcekey="lblNoOfEmployeesResource1" ></asp:Label>
     </td>
      <td class="DataArea">
            <asp:TextBox ID="TxtNoOfEmployees"  runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="255" meta:resourcekey="txtArbNameResource1"></asp:TextBox>
                   <asp:CheckBox ID="chkApplyValidation" Width="100" runat="server" meta:resourcekey="CheckBox_ExternalLevelRecourcekey" Text="Apply Validation" />

       </td>
    <td>


    </td>
</tr>
                                                               <tr>
           <td class="SeparArea"></td>
           <td class="LabelArea">
               <asp:Label ID="LblPositionBudget" runat="server" SkinID="Label_DefaultNormal" Text="Position Budget" meta:resourcekey="lblPositionBudgetResource1" ></asp:Label>
           </td>
            <td class="DataArea">
     <asp:TextBox ID="TxtPositionBudget"  runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="255" meta:resourcekey="txtPositionBudgetResource1"></asp:TextBox>

</td>
            <td class="LabelArea">

                                                      </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea"></td>
                                                            <td class="DataArea"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            </table>
                                          <table style="width: 75%; vertical-align: top"
      cellspacing="0">
                                            <tr>
                                                <td style="width: 75%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        
                                                        
                                                        <tr>
 <td style="width: 47%; height: 16px; vertical-align: top">
     <table style="width: 100%; vertical-align: top" cellspacing="0">
    
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea"></td>
                                                            <td class="DataArea"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                               
                                            <tr>
                                                <td style="height: 100%" colspan="3"></td>
                                            </tr>
                                        </table>
                                        <div id="accordion">
                                            <h3>
                                                <asp:Label ID="Label2" runat="server" Text="SuperVisors" SkinID="Label_DefaultBold" meta:resourcekey="Label2Resource1" ></asp:Label></h3>
                                            <div >
                                                <igtbl:UltraWebGrid   Browser="UpLevel"   ID="uwgSuperVisors" runat="server" EnableAppStyling="False"
                                                    Height="120px" SkinID="Default"
                                                    Width="100%" meta:resourcekey="uwgSuperVisorsResource1" >
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer"
                                                        AllowDeleteDefault="Yes" AllowSortingDefault="OnClient"
                                                        AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle"
                                                        Name="uwgSuperVisors" RowHeightDefault="18px" RowSelectorsDefault="No"
                                                        SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00"
                                                        ViewType="OutlookGroupBy" AllowAddNewDefault="Yes">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption"
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                                            Font-Size="8.25pt" Height="120px" Width="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents AfterExitEditModeHandler="uwg_AfterExitEditModeHandler" />
                                                        <Pager MinimumPagesForDisplay="2">
                                                            <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                    WidthTop="1px" />
                                                            </PagerStyle>
                                                        </Pager>
                                                        <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                        </EditCellStyleDefault>
                                                        <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                WidthTop="1px" />
                                                        </FooterStyleDefault>
                                                        <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma"
                                                            Font-Size="9pt" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                WidthTop="1px" />
                                                        </HeaderStyleDefault>
                                                        <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                            <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                    WidthTop="1px" />
                                                            </BoxStyle>
                                                        </AddNewBox>
                                                        <ActivationObject BorderColor="" BorderWidth="">
                                                        </ActivationObject>
                                                        <FilterOptionsDefault>
                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                BorderWidth="1px" CustomRules="overflow:auto;"
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px"
                                                                Width="200px">
                                                                <Padding Left="2px" />
                                                            </FilterDropDownStyle>
                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                            </FilterHighlightRowStyle>
                                                            <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver"
                                                                BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;"
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
                                                                <Padding Left="2px" />
                                                            </FilterOperandDropDownStyle>
                                                        </FilterOptionsDefault>
                                                    </DisplayLayout>
                                                    <Bands>
                                                        <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                    BorderWidth="1px" CustomRules="overflow:auto;"
                                                                    Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                            </FilterOptions>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID"
                                                                    Hidden="True" Key="ID" Width="0px" meta:resourcekey="UltraGridColumnResource1">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="SuperVisorID" Width="100%" EditorControlID=""
                                                                    Type="DropDownList" Key="SuperVisorID" DataType="System.Int32"
                                                                    AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource2">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="SuperVisor">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
                                            </div>

                                            <h3>
                                                <asp:Label ID="Label4" runat="server" Text="Position Contacts" SkinID="Label_DefaultBold" meta:resourcekey="Label4Resource1" ></asp:Label></h3>
                                            <div>
                                                <igtbl:UltraWebGrid    Browser="UpLevel"  ID="uwgContact" runat="server" EnableAppStyling="False"
                                                    Height="120px" SkinID="Default"
                                                    Width="100%" meta:resourcekey="uwgContactResource1" >
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer"
                                                        AllowDeleteDefault="Yes" AllowSortingDefault="OnClient"
                                                        AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle"
                                                        Name="uwgContact" RowHeightDefault="18px" RowSelectorsDefault="No"
                                                        SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00"
                                                        ViewType="OutlookGroupBy" AllowAddNewDefault="Yes">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption"
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                                            Font-Size="8.25pt" Height="120px" Width="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents AfterExitEditModeHandler="uwg_AfterExitEditModeHandler" />
                                                        <Pager MinimumPagesForDisplay="2">
                                                            <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                    WidthTop="1px" />
                                                            </PagerStyle>
                                                        </Pager>
                                                        <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                        </EditCellStyleDefault>
                                                        <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                WidthTop="1px" />
                                                        </FooterStyleDefault>
                                                        <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma"
                                                            Font-Size="9pt" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                WidthTop="1px" />
                                                        </HeaderStyleDefault>
                                                        <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                            <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                    WidthTop="1px" />
                                                            </BoxStyle>
                                                        </AddNewBox>
                                                        <ActivationObject BorderColor="" BorderWidth="">
                                                        </ActivationObject>
                                                        <FilterOptionsDefault>
                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                BorderWidth="1px" CustomRules="overflow:auto;"
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px"
                                                                Width="200px">
                                                                <Padding Left="2px" />
                                                            </FilterDropDownStyle>
                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                            </FilterHighlightRowStyle>
                                                            <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver"
                                                                BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;"
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
                                                                <Padding Left="2px" />
                                                            </FilterOperandDropDownStyle>
                                                        </FilterOptionsDefault>
                                                    </DisplayLayout>
                                                    <Bands>
                                                        <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource2">
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                    BorderWidth="1px" CustomRules="overflow:auto;"
                                                                    Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                            </FilterOptions>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID"
                                                                    Hidden="True" Key="ID" Width="0px" meta:resourcekey="UltraGridColumnResource3">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="EngName" Width="50%" meta:resourcekey="UltraGridColumnResource4">
                                                                    <Header Caption="Contact English" Title="EngName">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ArbName" Width="50%"
                                                                    EditorControlID="WebTextEdit1" Type="Custom" meta:resourcekey="UltraGridColumnResource5">
                                                                    <Header Caption="Contact Arabic" Title="ArbName">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="IntervalID" Width="120px" EditorControlID=""
                                                                    Type="DropDownList" Key="IntervalID" DataType="System.Int32"
                                                                    AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource6">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Interval">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="IsExternal" CellButtonDisplay="Always"
                                                                    DataType="System.Boolean" Type="CheckBox" Width="70px" meta:resourcekey="UltraGridColumnResource7">
                                                                    <Header Caption="Is External">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
                                            </div>
                                            <h3>
                                                <asp:Label ID="Label3" runat="server" Text="Position Accountability" SkinID="Label_DefaultBold" meta:resourcekey="Label3Resource1" ></asp:Label></h3>
                                            <div>
                                                <asp:HiddenField ID="HiddenField_Accountability" runat="server" />
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>

                                                        <igtbl:UltraWebGrid   Browser="UpLevel"   ID="uwgAccountability1" runat="server" EnableAppStyling="False"
                                                            Height="120px" SkinID="Default"
                                                            Width="100%" meta:resourcekey="uwgAccountability1Resource1" >
                                                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer"
                                                                AllowDeleteDefault="Yes" AllowSortingDefault="OnClient"
                                                                AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle"
                                                                Name="uwgAccountability1" RowHeightDefault="18px" RowSelectorsDefault="No"
                                                                SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                                StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00"
                                                                ViewType="OutlookGroupBy" AllowAddNewDefault="Yes">
                                                                <FrameStyle BackColor="Window" BorderColor="InactiveCaption"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                                                    Font-Size="8.25pt" Height="100%" Width="100%">
                                                                </FrameStyle>
                                                                <ClientSideEvents AfterExitEditModeHandler="uwg_AfterExitEditModeHandler" AfterRowActivateHandler="uwgAccountability_AfterRowActivateHandler" />
                                                                <Pager MinimumPagesForDisplay="2">
                                                                    <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                            WidthTop="1px" />
                                                                    </PagerStyle>
                                                                </Pager>
                                                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                                </EditCellStyleDefault>
                                                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                        WidthTop="1px" />
                                                                </FooterStyleDefault>
                                                                <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma"
                                                                    Font-Size="9pt" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                        WidthTop="1px" />
                                                                </HeaderStyleDefault>
                                                                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid"
                                                                    BorderWidth="1px" Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                                    <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                            WidthTop="1px" />
                                                                    </BoxStyle>
                                                                </AddNewBox>
                                                                <ActivationObject BorderColor="" BorderWidth="">
                                                                </ActivationObject>
                                                                <FilterOptionsDefault>
                                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                        BorderWidth="1px" CustomRules="overflow:auto;"
                                                                        Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px"
                                                                        Width="200px">
                                                                        <Padding Left="2px" />
                                                                    </FilterDropDownStyle>
                                                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                    </FilterHighlightRowStyle>
                                                                    <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver"
                                                                        BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;"
                                                                        Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
                                                                        <Padding Left="2px" />
                                                                    </FilterOperandDropDownStyle>
                                                                </FilterOptionsDefault>
                                                            </DisplayLayout>
                                                            <Bands>
                                                                <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource3">
                                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                                    </AddNewRow>
                                                                    <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                            BorderWidth="1px" CustomRules="overflow:auto;"
                                                                            Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
                                                                            <Padding Left="2px" />
                                                                        </FilterDropDownStyle>
                                                                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                        </FilterHighlightRowStyle>
                                                                    </FilterOptions>
                                                                    <Columns>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ID"
                                                                            Hidden="True" Key="ID" Width="100px" meta:resourcekey="UltraGridColumnResource8">
                                                                            <Header Caption="ID">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="CriteriaGroupID" Width="30%" Key="CriteriaGroupID" meta:resourcekey="CriteriaGroupIDResource9"
                                                                            Type="DropDownList" AllowUpdate="Yes">
                                                                            <Header Caption="Criteria Groups" Title="CriteriaGroupID">
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Header>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>

                                                                           <igtbl:UltraGridColumn BaseColumnName="CriteriaID" Width="25%" meta:resourcekey="CriteriaIDResource13"
      Type="DropDownList" AllowUpdate="Yes">
      <Header Caption="CriteriaID" Title="CriteriaID">
          <RowLayoutColumnInfo OriginX="2" />
      </Header>
      <HeaderStyle HorizontalAlign="Center" />
      <CellStyle HorizontalAlign="Center">
      </CellStyle>
      <Footer>
          <RowLayoutColumnInfo OriginX="2" />
      </Footer>
  </igtbl:UltraGridColumn>
  <igtbl:UltraGridColumn BaseColumnName="MinimumScore" Width="15%"
              EditorControlID="WebTextEdit1" Type="Custom" meta:resourcekey="MMinimumScoreScoreResource14">
              <Header Caption="MinimumScore Score" Title="MinimumScore">
                  <RowLayoutColumnInfo OriginX="3" />
              </Header>
              <HeaderStyle HorizontalAlign="Center" />
              <CellStyle HorizontalAlign="Center">
              </CellStyle>
              <Footer>
                  <RowLayoutColumnInfo OriginX="3" />
              </Footer>
          </igtbl:UltraGridColumn>

            <igtbl:UltraGridColumn BaseColumnName="MaximumScore" Width="15%"
              EditorControlID="WebTextEdit1" Type="Custom" meta:resourcekey="MaximumScoreResource14">
              <Header Caption="Maximum Score" Title="MaximumScore">
                  <RowLayoutColumnInfo OriginX="3" />
              </Header>
              <HeaderStyle HorizontalAlign="Center" />
              <CellStyle HorizontalAlign="Center">
              </CellStyle>
              <Footer>
                  <RowLayoutColumnInfo OriginX="3" />
              </Footer>
          </igtbl:UltraGridColumn>
            
          <igtbl:UltraGridColumn BaseColumnName="Weight" Width="15%"
                EditorControlID="WebTextEdit1" Type="Custom" meta:resourcekey="WeightResource14">
                <Header Caption="Weight" Title="Weight">
                    <RowLayoutColumnInfo OriginX="3" />
                </Header>
                <HeaderStyle HorizontalAlign="Center" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <Footer>
                    <RowLayoutColumnInfo OriginX="3" />
                </Footer>
            </igtbl:UltraGridColumn>
                                                                    </Columns>
                                                                </igtbl:UltraGridBand>
                                                            </Bands>
                                                        </igtbl:UltraWebGrid>

<%--                                                        <igtbl:UltraWebGrid   Browser="UpLevel"   ID="uwgAccountability2" runat="server" EnableAppStyling="False"
                                                            Height="120px" SkinID="Default"
                                                            Width="100%" meta:resourcekey="uwgAccountability2Resource1">
                                                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer"
                                                                AllowDeleteDefault="Yes" AllowSortingDefault="OnClient"
                                                                AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle"
                                                                Name="uwgAccountability2" RowHeightDefault="18px" RowSelectorsDefault="No"
                                                                SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                                StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00"
                                                                ViewType="OutlookGroupBy" AllowAddNewDefault="Yes">
                                                                <FrameStyle BackColor="Window" BorderColor="InactiveCaption"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                                                    Font-Size="8.25pt" Height="120px" Width="100%">
                                                                </FrameStyle>
                                                                <ClientSideEvents AfterExitEditModeHandler="uwg_AfterExitEditModeHandler" AfterRowActivateHandler="uwgAccountability1_AfterRowActivateHandler" />
                                                                <Pager MinimumPagesForDisplay="2">
                                                                    <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                            WidthTop="1px" />
                                                                    </PagerStyle>
                                                                </Pager>
                                                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                                </EditCellStyleDefault>
                                                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                        WidthTop="1px" />
                                                                </FooterStyleDefault>
                                                                <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma"
                                                                    Font-Size="9pt" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                        WidthTop="1px" />
                                                                </HeaderStyleDefault>
                                                                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid"
                                                                    BorderWidth="1px" Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                                    <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                            WidthTop="1px" />
                                                                    </BoxStyle>
                                                                </AddNewBox>
                                                                <ActivationObject BorderColor="" BorderWidth="">
                                                                </ActivationObject>
                                                                <FilterOptionsDefault>
                                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                        BorderWidth="1px" CustomRules="overflow:auto;"
                                                                        Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px"
                                                                        Width="200px">
                                                                        <Padding Left="2px" />
                                                                    </FilterDropDownStyle>
                                                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                    </FilterHighlightRowStyle>
                                                                    <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver"
                                                                        BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;"
                                                                        Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
                                                                        <Padding Left="2px" />
                                                                    </FilterOperandDropDownStyle>
                                                                </FilterOptionsDefault>
                                                            </DisplayLayout>
                                                            <Bands>
                                                                <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource4">
                                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                                    </AddNewRow>
                                                                    <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                            BorderWidth="1px" CustomRules="overflow:auto;"
                                                                            Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
                                                                            <Padding Left="2px" />
                                                                        </FilterDropDownStyle>
                                                                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                        </FilterHighlightRowStyle>
                                                                    </FilterOptions>
                                                                    <Columns>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ID"
                                                                            Hidden="True" Key="ID" Width="100px" meta:resourcekey="UltraGridColumnResource11">
                                                                            <Header Caption="ID">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="PosAccountabilityID"
                                                                            Hidden="True" Key="PosAccountabilityID" Width="100px" meta:resourcekey="UltraGridColumnResource12">
                                                                            <Header Caption="PosAccountabilityID">
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="CriteriaID" Width="25%" meta:resourcekey="CriteriaIDResource13"
                                                                            Type="DropDownList" AllowUpdate="Yes">
                                                                            <Header Caption="CriteriaID" Title="CriteriaID">
                                                                                <RowLayoutColumnInfo OriginX="2" />
                                                                            </Header>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="2" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="MinimumScore" Width="15%"
                                                                                    EditorControlID="WebTextEdit1" Type="Custom" meta:resourcekey="MMinimumScoreScoreResource14">
                                                                                    <Header Caption="MinimumScore Score" Title="MinimumScore">
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Header>
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>

                                                                                  <igtbl:UltraGridColumn BaseColumnName="MaximumScore" Width="15%"
                                                                                    EditorControlID="WebTextEdit1" Type="Custom" meta:resourcekey="MaximumScoreResource14">
                                                                                    <Header Caption="Maximum Score" Title="MaximumScore">
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Header>
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                  
                                                                                <igtbl:UltraGridColumn BaseColumnName="Weight" Width="15%"
                                                                                      EditorControlID="WebTextEdit1" Type="Custom" meta:resourcekey="WeightResource14">
                                                                                      <Header Caption="Weight" Title="Weight">
                                                                                          <RowLayoutColumnInfo OriginX="3" />
                                                                                      </Header>
                                                                                      <HeaderStyle HorizontalAlign="Center" />
                                                                                      <CellStyle HorizontalAlign="Center">
                                                                                      </CellStyle>
                                                                                      <Footer>
                                                                                          <RowLayoutColumnInfo OriginX="3" />
                                                                                      </Footer>
                                                                                  </igtbl:UltraGridColumn>
                          
                                                                    </Columns>
                                                                </igtbl:UltraGridBand>
                                                            </Bands>
                                                        </igtbl:UltraWebGrid>--%>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <h3>
                                                <asp:Label ID="Label6" runat="server" Text="Position Qualifications" SkinID="Label_DefaultBold" meta:resourcekey="Label6Resource1" ></asp:Label></h3>
                                            <div>
                                                <igtbl:UltraWebGrid   Browser="UpLevel"   ID="uwgQualifications" runat="server" EnableAppStyling="False"
                                                    Height="120px" SkinID="Default"
                                                    Width="100%" meta:resourcekey="uwgQualificationsResource1" >
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer"
                                                        AllowDeleteDefault="Yes" AllowSortingDefault="OnClient"
                                                        AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle"
                                                        Name="uwgQualifications" RowHeightDefault="18px" RowSelectorsDefault="No"
                                                        SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00"
                                                        ViewType="OutlookGroupBy" AllowAddNewDefault="Yes">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption"
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                                            Font-Size="8.25pt" Height="100%" Width="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents AfterExitEditModeHandler="uwg_AfterExitEditModeHandler" />
                                                        <Pager MinimumPagesForDisplay="2">
                                                            <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                    WidthTop="1px" />
                                                            </PagerStyle>
                                                        </Pager>
                                                        <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                        </EditCellStyleDefault>
                                                        <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                WidthTop="1px" />
                                                        </FooterStyleDefault>
                                                        <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma"
                                                            Font-Size="9pt" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                WidthTop="1px" />
                                                        </HeaderStyleDefault>
                                                        <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                            <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                    WidthTop="1px" />
                                                            </BoxStyle>
                                                        </AddNewBox>
                                                        <ActivationObject BorderColor="" BorderWidth="">
                                                        </ActivationObject>
                                                        <FilterOptionsDefault>
                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                BorderWidth="1px" CustomRules="overflow:auto;"
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px"
                                                                Width="200px">
                                                                <Padding Left="2px" />
                                                            </FilterDropDownStyle>
                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                            </FilterHighlightRowStyle>
                                                            <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver"
                                                                BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;"
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
                                                                <Padding Left="2px" />
                                                            </FilterOperandDropDownStyle>
                                                        </FilterOptionsDefault>
                                                    </DisplayLayout>
                                                    <Bands>
                                                        <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource5">
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                    BorderWidth="1px" CustomRules="overflow:auto;"
                                                                    Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                            </FilterOptions>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID"
                                                                    Hidden="True" Key="ID" Width="0px" meta:resourcekey="UltraGridColumnResource15">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="EngName" Width="50%" meta:resourcekey="UltraGridColumnResource16">
                                                                    <Header Caption="Qualification English" Title="EngName">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ArbName" Width="50%"
                                                                    EditorControlID="WebTextEdit1" Type="Custom" meta:resourcekey="UltraGridColumnResource17">
                                                                    <Header Caption="Qualification Arabic" Title="ArbName">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
                                            </div>
                                            <h3>
                                                <asp:Label ID="Label5" runat="server" Text="Position Competences" SkinID="Label_DefaultBold" meta:resourcekey="Label5Resource1" ></asp:Label></h3>
                                            <div>
                                                <igtbl:UltraWebGrid   Browser="UpLevel"   ID="uwgCompetences" runat="server" EnableAppStyling="False"
                                                    Height="120px" SkinID="Default"
                                                    Width="100%" meta:resourcekey="uwgCompetencesResource1" >
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer"
                                                        AllowDeleteDefault="Yes" AllowSortingDefault="OnClient"
                                                        AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle"
                                                        Name="uwgCompetences" RowHeightDefault="18px" RowSelectorsDefault="No"
                                                        SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00"
                                                        ViewType="OutlookGroupBy" AllowAddNewDefault="Yes">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption"
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                                            Font-Size="8.25pt" Height="120px" Width="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents AfterExitEditModeHandler="uwg_AfterExitEditModeHandler" />
                                                        <Pager MinimumPagesForDisplay="2">
                                                            <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                    WidthTop="1px" />
                                                            </PagerStyle>
                                                        </Pager>
                                                        <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                        </EditCellStyleDefault>
                                                        <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                WidthTop="1px" />
                                                        </FooterStyleDefault>
                                                        <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma"
                                                            Font-Size="9pt" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                WidthTop="1px" />
                                                        </HeaderStyleDefault>
                                                        <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                            <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                    WidthTop="1px" />
                                                            </BoxStyle>
                                                        </AddNewBox>
                                                        <ActivationObject BorderColor="" BorderWidth="">
                                                        </ActivationObject>
                                                        <FilterOptionsDefault>
                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                BorderWidth="1px" CustomRules="overflow:auto;"
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px"
                                                                Width="200px">
                                                                <Padding Left="2px" />
                                                            </FilterDropDownStyle>
                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                            </FilterHighlightRowStyle>
                                                            <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver"
                                                                BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;"
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
                                                                <Padding Left="2px" />
                                                            </FilterOperandDropDownStyle>
                                                        </FilterOptionsDefault>
                                                    </DisplayLayout>
                                                    <Bands>
                                                        <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource6">
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                    BorderWidth="1px" CustomRules="overflow:auto;"
                                                                    Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                            </FilterOptions>
                                                     <%--       <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID"
                                                                    Hidden="True" Key="ID" Width="0px" meta:resourcekey="UltraGridColumnResource18">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="EngName" Width="50%" meta:resourcekey="UltraGridColumnResource19">
                                                                    <Header Caption="Competences English" Title="EngName">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ArbName" Width="50%"
                                                                    EditorControlID="WebTextEdit1" Type="Custom" meta:resourcekey="UltraGridColumnResource20">
                                                                    <Header Caption="Competences Arabic" Title="ArbName">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>--%>
                                                                                                                              <Columns>
                                                                      <igtbl:UltraGridColumn BaseColumnName="ID"
                                                                          Hidden="True" Key="ID" Width="100px" meta:resourcekey="UltraGridColumnResource8">
                                                                          <Header Caption="ID">
                                                                          </Header>
                                                                      </igtbl:UltraGridColumn>

                                                                      <igtbl:UltraGridColumn BaseColumnName="CriteriaGroupID" Width="30%" Key="CriteriaGroupID" meta:resourcekey="CriteriaGroupIDResource9"
                                                                          Type="DropDownList" AllowUpdate="Yes">
                                                                          <Header Caption="Criteria Groups" Title="CriteriaGroupID">
                                                                              <RowLayoutColumnInfo OriginX="1" />
                                                                          </Header>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <CellStyle HorizontalAlign="Center">
                                                                          </CellStyle>
                                                                          <Footer>
                                                                              <RowLayoutColumnInfo OriginX="1" />
                                                                          </Footer>
                                                                      </igtbl:UltraGridColumn>

                                                                         <igtbl:UltraGridColumn BaseColumnName="CriteriaID" Width="40%" meta:resourcekey="CriteriaIDResource13"
    Type="DropDownList" AllowUpdate="Yes">
    <Header Caption="CriteriaID" Title="CriteriaID">
        <RowLayoutColumnInfo OriginX="2" />
    </Header>
    <HeaderStyle HorizontalAlign="Center" />
    <CellStyle HorizontalAlign="Center">
    </CellStyle>
    <Footer>
        <RowLayoutColumnInfo OriginX="2" />
    </Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn BaseColumnName="MinimumScore" Width="10%"
            EditorControlID="WebTextEdit1" Type="Custom" meta:resourcekey="MMinimumScoreScoreResource14">
            <Header Caption="MinimumScore Score" Title="MinimumScore">
                <RowLayoutColumnInfo OriginX="3" />
            </Header>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
            <Footer>
                <RowLayoutColumnInfo OriginX="3" />
            </Footer>
        </igtbl:UltraGridColumn>

          <igtbl:UltraGridColumn BaseColumnName="MaximumScore" Width="10%"
            EditorControlID="WebTextEdit1" Type="Custom" meta:resourcekey="MaximumScoreResource14">
            <Header Caption="Maximum Score" Title="MaximumScore">
                <RowLayoutColumnInfo OriginX="3" />
            </Header>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
            <Footer>
                <RowLayoutColumnInfo OriginX="3" />
            </Footer>
        </igtbl:UltraGridColumn>
          
        <igtbl:UltraGridColumn BaseColumnName="Weight" Width="10%"
              EditorControlID="WebTextEdit1" Type="Custom" meta:resourcekey="WeightResource14">
              <Header Caption="Weight" Title="Weight">
                  <RowLayoutColumnInfo OriginX="3" />
              </Header>
              <HeaderStyle HorizontalAlign="Center" />
              <CellStyle HorizontalAlign="Center">
              </CellStyle>
              <Footer>
                  <RowLayoutColumnInfo OriginX="3" />
              </Footer>
          </igtbl:UltraGridColumn>
  <igtbl:UltraGridColumn BaseColumnName="EngName" Hidden="True"  Width="50%" meta:resourcekey="UltraGridColumnResource19">
     <Header Caption="Competences English" Title="EngName">
         <RowLayoutColumnInfo OriginX="1" />
     </Header>
     <HeaderStyle HorizontalAlign="Center" />
     <CellStyle HorizontalAlign="Center">
     </CellStyle>
     <Footer>
         <RowLayoutColumnInfo OriginX="1" />
     </Footer>
 </igtbl:UltraGridColumn>
 <igtbl:UltraGridColumn BaseColumnName="ArbName" Width="50%" Hidden="True"
     EditorControlID="WebTextEdit1" Type="Custom" meta:resourcekey="UltraGridColumnResource20">
     <Header Caption="Competences Arabic" Title="ArbName">
         <RowLayoutColumnInfo OriginX="2" />
     </Header>
     <HeaderStyle HorizontalAlign="Center" />
     <CellStyle HorizontalAlign="Center">
     </CellStyle>
     <Footer>
         <RowLayoutColumnInfo OriginX="2" />
     </Footer>
 </igtbl:UltraGridColumn>
 <igtbl:UltraGridColumn BaseColumnName="Remarks" Width="50%" Hidden="True"
     EditorControlID="WebTextEdit1" Type="Custom" meta:resourcekey="UltraGridColumnResource20">
     <Header Caption="Competences Arabic" Title="ArbName">
         <RowLayoutColumnInfo OriginX="2" />
     </Header>
     <HeaderStyle HorizontalAlign="Center" />
     <CellStyle HorizontalAlign="Center">
     </CellStyle>
     <Footer>
         <RowLayoutColumnInfo OriginX="2" />
     </Footer>
 </igtbl:UltraGridColumn>
                                                                  </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
                                                <igtxt:WebTextEdit ID="WebTextEdit1"
                                                    runat="server" Style="display: none;" meta:resourcekey="WebTextEdit1Resource1">
                                                </igtxt:WebTextEdit>
                                            </div>
                                        </div>

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
