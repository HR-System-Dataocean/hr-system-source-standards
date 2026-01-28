<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSendEmailSetting.aspx.vb"
    Inherits="frmSendEmailSetting" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ SendEmail Setting</title>
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

        var Row;
        var IsEdit = true;
        function uwg_AfterCellUpdateHandler(gridName, cellId) {
            if (IsEdit == true) {
                var cell = igtbl_getCellById(cellId);
                Row = igtbl_getRowById(cellId);
               
                var count = igtbl_getGridById(gridName).Rows.length - 1;
                var rowIndex = igtbl_getRowById(cellId).Id.split("_")[2];

                if (rowIndex == count) {

                    igtbl_addNew(gridName, 0, true, false);

                }
            }
        }

        var cell;
        function uwgEnterCellEdit(gridName, cellId) {
            cell = cellId;

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
    <form id="frmContractTypes" runat="server">
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display:none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N" />
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
                                                            <asp:Label ID="lblCode" runat="server" Text="الكود" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
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
                                                            <asp:Label ID="lblFormName" runat="server" Text="اسم الشاشة" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblFormNameResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtFormName" runat="server" SkinID="TextBox_LargeNormalltr" MaxLength="255"
                                                                meta:resourcekey="txtFormNameResource1"></asp:TextBox>
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
                                                           <asp:Label ID="lblSaveSend" runat="server" Style="margin-right: 20px; width: 250%; font-size: small" Text="Send With Save" SkinID="TextBox_LargeNormalC"
                                                                                meta:resourcekey="lblSaveSendResource1"></asp:Label>
                                                                            <asp:CheckBox ID="chkSaveSend" runat="server" />
                                                        </td>
                                                     
                                                        <td class="DataArea">
                                                               <table style="width: 100%;" cellspacing="0">
                                                    <tr>
                                                         <td class="LabelArea" style="width: 60px;" >
                                                           
                                                        </td>
                                                         <td colspan="4">
                                                            <asp:Label ID="lblUpdateSend" runat="server" Style="margin-right: 20px; width: 25%; font-size: small" Text="Send With Update" SkinID="TextBox_LargeNormalC"
                                                                                meta:resourcekey="lblUpdateSendResource1"></asp:Label>
                                                                            <asp:CheckBox ID="chkUpdateSend" runat="server" />
                                                             </td>
                                                              <td colspan="4">
                                                                              <asp:Label ID="lblDeleteSend" runat="server" Style="margin-right: 20px; width: 25%; font-size: small" Text="Send With Delete" SkinID="TextBox_LargeNormalC"
                                                                                meta:resourcekey="lblDeleteSendResource1"></asp:Label>
                                                                            <asp:CheckBox ID="chkDeleteSend" runat="server" />
                                                                        </td>
                                                        </tr></table>
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
                    <asp:Label ID="lblFromEmail" runat="server" Text="من إيميل" SkinID="Label_DefaultNormal"
                        meta:resourcekey="lblFromEmailResource1"></asp:Label>
                </td>
                <td class="DataArea">
                    <asp:TextBox ID="txtFromEmail" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                        meta:resourcekey="txtFromMobileResource1"></asp:TextBox>
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
                    <asp:Label ID="lblToEmail" runat="server" Text="الى إيميل" SkinID="Label_DefaultNormal"
                        meta:resourcekey="lblToEmailResource1"></asp:Label>
                </td>
                <td class="DataArea">
                    <asp:TextBox ID="txtToEmail" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                        meta:resourcekey="txtToEmailResource1"></asp:TextBox>
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
                    <asp:Label ID="lblToTable" runat="server" Text="الى جدول" SkinID="Label_DefaultNormal"
                        meta:resourcekey="lblToTableResource1"></asp:Label>
                </td>
                <td class="DataArea">
                    <asp:TextBox ID="txtToTable" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                        meta:resourcekey="txtToTableResource1"></asp:TextBox>
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
                    <asp:Label ID="lblToCondition" runat="server" Text="شرط ثابت" SkinID="Label_DefaultNormal"
                        meta:resourcekey="lblToConditionResource1"></asp:Label>
                </td>
                <td class="DataArea">
                    <asp:TextBox ID="txtToCondition" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                        meta:resourcekey="txtToConditionResource1"></asp:TextBox>
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
                    <asp:Label ID="lblFormCondition" runat="server" Text="الشرط من الشاشة" SkinID="Label_DefaultNormal"
                        meta:resourcekey="lblFormConditionResource1"></asp:Label>
                </td>
                <td class="DataArea">
                    <asp:TextBox ID="txtFormCondition" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                        meta:resourcekey="txtFormConditionResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lblTitle" runat="server" Text="العنوان" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblTitleResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtTitle" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                                                                meta:resourcekey="txtTitleResource1"></asp:TextBox>
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
                <td style="width: 50px;">
                    <asp:Label ID="lblSubject" runat="server" Text="الموضوع" SkinID="Label_DefaultNormal"
                        meta:resourcekey="lblSubjectResource1"></asp:Label>
                </td>
                <td class="DataArea">
                         <asp:TextBox ID="txtSubject" runat="server" Height="50px" MaxLength="8000"
    TextMode="MultiLine" Width="100%"></asp:TextBox>
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
                                            
                                        </tr>

                                        <tr>
                                            <td style="height: 16px; vertical-align: top" colspan="3">
                                                <table style="width: 47%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="UwgSearchEmployees" runat="server" EnableAppStyling="False"
                                                                Height="250px" meta:resourcekey="uwgForNationalityResource1" SkinID="Default"
                                                                Width="100%">
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                    BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                                    RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                    Version="4.00" ViewType="OutlookGroupBy">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="250px"
                                                                        Width="100%">
                                                                    </FrameStyle>
                                                                    <ClientSideEvents AfterCellUpdateHandler="uwg_AfterCellUpdateHandler" AfterEnterEditModeHandler="uwgEnterCellEdit" />
                                                                    <Pager MinimumPagesForDisplay="2">
                                                                        <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                        </PagerStyle>
                                                                    </Pager>
                                                                    <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                                    </EditCellStyleDefault>
                                                                    <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </FooterStyleDefault>
                                                                    <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                                                                        Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </HeaderStyleDefault>
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
                                                                    <igtbl:UltraGridBand AllowSorting="No" meta:resourcekey="UltraGridBandResource1"
                                                                        AllowAdd="Yes">
                                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                                        </AddNewRow>
                                                                        <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                            <FilterDropDownStyle BackColor="SteelBlue" BorderColor="Silver" BorderStyle="Solid"
                                                                                BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                Font-Size="11px" Width="200px">
                                                                                <Padding Left="2px" />
                                                                            </FilterDropDownStyle>
                                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                            </FilterHighlightRowStyle>
                                                                        </FilterOptions>
                                                                        <Columns>
                                                                           
                                                                            <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" Hidden="True">
                                                                                <Header Caption="">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                             <igtbl:UltraGridColumn BaseColumnName="SendEmailSettingId" Hidden="True" Key="SendEmailSettingId">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="FixedEmail" Key="FixedEmail"
                                                                                meta:resourcekey="UltraGridColumnEmail" Width="100%">
                                                                                <Header Caption="Fixed Emails">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
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
