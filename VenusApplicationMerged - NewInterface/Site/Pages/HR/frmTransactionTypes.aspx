<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTransactionTypes.aspx.vb"
    Inherits="frmTransactionTypes" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Transaction Types</title>
    <script src="../HR/Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="../HR/Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="../HR/Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="../HR/Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="../HR/Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>

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
    <form id="frmTransactionTypes" runat="server">
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
         <igtxt:WebDateTimeEdit ID="txtDate" runat="server" meta:resourcekey="txtDateResource1"
            DisplayModeFormat="dd/MM/yyyy" EditModeFormat="dd/MM/yyyy">
        </igtxt:WebDateTimeEdit>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display: none">
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
                                                        <td class="search">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
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
                                                            &nbsp;
                                                        </td>
                                                        <td class="DataArea">
                                                            &nbsp;
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
                                                            <asp:Label ID="lblEngName" runat="server" Text="التوصيف الإنجليزى" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalltr" MaxLength="255"
                                                                meta:resourcekey="txtEngNameResource1"></asp:TextBox>
                                                        </td>
                                                        <td class="search">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                                        </td>
                                                    </tr>
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
                                                    &nbsp;
                                                </td>
                                                <td class="DataArea">
                                                    &nbsp;
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
                                                        <asp:Label ID="lblShortEngName" runat="server" SkinID="Label_DefaultNormal" Text="الرمز الإنجليزى"
                                                            meta:resourcekey="lblShortEngNameResource1"></asp:Label>
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:TextBox ID="txtShortEngName" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalltr"
                                                            meta:resourcekey="txtShortEngNameResource1"></asp:TextBox>
                                                    </td>
                                                    <td class="search">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                                    </td>
                                                </tr>
                                    </tr>
                                    </table> </td>
                                    <td style="width: 6%; height: 16px; vertical-align: top">
                                    </td>
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
                                    </tr>
                                    <tr>
                                        <td style="width: 47%; height: 16px; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td class="SeparArea">
                                                    </td>
                                                    <td class="LabelArea">
                                                        <asp:Label ID="lblArbName" runat="server" meta:resourcekey="lblArbNameResource1"
                                                            SkinID="Label_DefaultNormal" Text="التوصيف العربى"></asp:Label>
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:TextBox ID="txtArbName" runat="server" MaxLength="255" meta:resourcekey="txtArbNameResource1"
                                                            SkinID="TextBox_LargeNormalrtl"></asp:TextBox>
                                                    </td>
                                                    <td class="search">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                                    </td>
                                                </tr>
                                    </tr>
                                    </table> </td>
                                    <td style="width: 6%; height: 16px; vertical-align: top">
                                    </td>
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
                                    </tr>
                                    <tr>
                                        <td style="width: 47%; height: 16px; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td class="SeparArea">
                                                    </td>
                                                    <td class="LabelArea">
                                                        <asp:Label ID="lblShortArbName" runat="server" SkinID="Label_DefaultNormal" Text="الرمز العربي "
                                                            meta:resourcekey="lblShortArbNameResource1"></asp:Label>
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:TextBox ID="txtShortArbName" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalltr"
                                                            meta:resourcekey="txtShortArbNameResource1"></asp:TextBox>
                                                    </td>
                                                    <td class="search">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                                    </td>
                                                </tr>
                                    </tr>
                                    </table> </td>
                                    <td style="width: 6%; height: 16px; vertical-align: top">
                                    </td>
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
                                    </tr>
                                    <tr>
                                        <td style="width: 47%; height: 16px; vertical-align: top">
                                            <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                border-bottom: 1px solid black">
                                                <tr>
                                                    <td style="vertical-align: bottom">
                                                        <asp:Label ID="Label2" runat="server" SkinID="Label_DefaultBold" Text="إعدادات المعادلة"
                                                            meta:resourcekey="Label2Resource1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 6%; height: 16px; vertical-align: top">
                                        </td>
                                        <td style="width: 47%; height: 16px; vertical-align: top">
                                            <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                border-bottom: 1px solid black">
                                                <tr>
                                                    <td style="vertical-align: bottom">
                                                        <asp:Label ID="Label4" runat="server" SkinID="Label_DefaultBold" Text="الإعدادات"
                                                            meta:resourcekey="Label4Resource1"></asp:Label>
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
                                                        <asp:Label ID="lblSign" runat="server" SkinID="Label_DefaultNormal" Text="الاشارة"
                                                            meta:resourcekey="lblSignResource1"></asp:Label>
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:DropDownList ID="ddlSign" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="ddlSignResource1">
                                                            <asp:ListItem Value="1" meta:resourcekey="ListItemResource1" Text="Plus"></asp:ListItem>
                                                            <asp:ListItem Value="-1" meta:resourcekey="ListItemResource2" Text="Minus"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <br />
                                                    </td>
                                                    <td class="search">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
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
                                                        <asp:CheckBox ID="chkIsDistributable" runat="server" meta:resourcekey="chkIsDistributableResource1" />
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:Label ID="lblIsDistributable" runat="server" SkinID="Label_DefaultNormal" Text="قابل للتوزيع"
                                                            meta:resourcekey="lblIsDistributableResource1"></asp:Label>
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
                                                        <asp:Label ID="lblTransactionCategory" runat="server" SkinID="Label_DefaultNormal"
                                                            Text="نوع الحركة" meta:resourcekey="lblTransactionCategoryResource1"></asp:Label>
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:DropDownList ID="DdlTransactionCategory" runat="server" SkinID="DropDownList_LargNormal"
                                                            meta:resourcekey="DdlTransactionCategoryResource1">
                                                            <asp:ListItem Value="True" Text="Include" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="False" Text="Not Include" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="search">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                                    </td>
                                                </tr>
                                    </tr>
                                    </table> </td>
                                    <td style="width: 6%; height: 16px; vertical-align: top">
                                    </td>
                                    <td style="width: 47%; height: 16px; vertical-align: top">
                                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                                            <tr>
                                                <td class="SeparArea">
                                                </td>
                                                <td class="LabelArea">
                                                    <asp:CheckBox ID="chkIsPaid" runat="server" meta:resourcekey="chkIsPaidResource1" />
                                                </td>
                                                <td class="DataArea">
                                                    <asp:Label ID="lblIsPaid" runat="server" SkinID="Label_DefaultNormal" Text="داخل في الرواتب"
                                                        meta:resourcekey="lblIsPaidResource1"></asp:Label>
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
                                                        <asp:Label ID="lblDebitAccountCode" runat="server" SkinID="Label_DefaultNormal" Text="كود حساب المدين"
                                                            meta:resourcekey="lblDebitAccountCodeResource1"></asp:Label>
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:TextBox ID="txtDebitAccountCode" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalltr"
                                                            meta:resourcekey="txtDebitAccountCodeResource1"></asp:TextBox>
                                                    </td>
                                                    <td class="search">
                                                    </td>
                                                </tr>
                                    </tr>
                                    </table> </td>
                                    <td style="width: 6%; height: 16px; vertical-align: top">
                                    </td>
                                    <td style="width: 47%; height: 16px; vertical-align: top">
                                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                                            <tr>
                                                <td class="SeparArea">
                                                </td>
                                                <td class="LabelArea">
                                                    <asp:CheckBox ID="chkInputNumeric" runat="server" meta:resourcekey="chkInputNumericResource1" />
                                                </td>
                                                <td class="DataArea">
                                                    <asp:Label ID="lblInputNumeric" runat="server" SkinID="Label_DefaultNormal" Text="المدخل رقمي"
                                                        meta:resourcekey="lblInputNumericResource1"></asp:Label>
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
                                                        <asp:Label ID="lblCreditAccountCode" runat="server" SkinID="Label_DefaultNormal"
                                                            Text="كود حساب الدائن" meta:resourcekey="lblCreditAccountCodeResource1"></asp:Label>
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:TextBox ID="txtCreditAccountCode" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalltr"
                                                            meta:resourcekey="txtCreditAccountCodeResource1"></asp:TextBox>
                                                    </td>
                                                    <td class="search">
                                                    </td>
                                                </tr>
                                    </tr>
                                    </table> </td>
                                    <td style="width: 6%; height: 16px; vertical-align: top">
                                    </td>
                                    <td style="width: 47%; height: 16px; vertical-align: top">
                                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                                            <tr>
                                                <td class="SeparArea">
                                                </td>
                                                <td class="LabelArea">
                                                    <asp:CheckBox ID="chkIsEndOfService" runat="server" meta:resourcekey="chkIsEndOfServiceResource1" />
                                                </td>
                                                <td class="DataArea">
                                                    <asp:Label ID="lblIsEndOfService" runat="server" SkinID="Label_DefaultNormal" Text="داخل في نهاية الخدمة"
                                                        meta:resourcekey="lblIsEndOfServiceResource1"></asp:Label>
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
                                                        <asp:Label ID="lblFormula" runat="server" Text="الصيغة" SkinID="Label_DefaultNormal"
                                                            meta:resourcekey="lblFormulaResource1"></asp:Label>
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:TextBox ID="txtFormula" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalltr"
                                                            meta:resourcekey="txtFormulaResource1"></asp:TextBox>
                                                        <br />
                                                        <asp:Label ID="lblFormulaDesc" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="lblFormulaDescResource1"></asp:Label>
                                                    </td>
                                                    <td class="search">
                                                        <igtxt:WebImageButton ID="btnFormula" runat="server" AutoSubmit="False" Height="18px"
                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnFormulaResource1">
                                                            <Alignments TextImage="ImageBottom" />
                                                            <Appearance>
                                                                <Image Url="./Img/rtg_rate.gif" />
                                                            </Appearance>
                                                        </igtxt:WebImageButton>
                                                        <br />
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
                                                        <asp:CheckBox ID="chkIsBasicSalary" runat="server" meta:resourcekey="chkIsBasicSalaryResource1" />
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:Label ID="lblIsBasicSalary" runat="server" SkinID="Label_DefaultNormal" Text="راتب الأساسي"
                                                            meta:resourcekey="lblIsBasicSalaryResource1"></asp:Label>
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
                                                        <asp:Label ID="lbltxtBeginFormula" runat="server" Text="صيغة البداية" SkinID="Label_DefaultNormal"
                                                            meta:resourcekey="lbltxtBeginFormulaResource1"></asp:Label>
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:TextBox ID="txtBeginFormula" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalltr"
                                                            meta:resourcekey="txtBeginFormulaResource2"></asp:TextBox>
                                                        <br />
                                                        <asp:Label ID="lblBeginFormulaDesc" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="lblBeginFormulaDescResource1"></asp:Label>
                                                    </td>
                                                    <td class="search">
                                                        <igtxt:WebImageButton ID="btnBeginFormula" runat="server" AutoSubmit="False" Height="18px"
                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnBeginFormulaResource1">
                                                            <Alignments TextImage="ImageBottom" />
                                                            <Appearance>
                                                                <Image Url="./Img/rtg_rate.gif" />
                                                            </Appearance>
                                                        </igtxt:WebImageButton>
                                                        <br />
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
                                                        <asp:CheckBox ID="CheckBox_IsAllowPosting" runat="server" meta:resourcekey="chkIsAllowPostingResource1" />
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:Label ID="Label1" runat="server" SkinID="Label_DefaultNormal" Text="مستبعد من عملية الترحيل"
                                                            meta:resourcekey="lblIsPostingResource1"></asp:Label>
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
                                                        <asp:Label ID="lblEndFormula" runat="server" Text="صيغة النهاية" SkinID="Label_DefaultNormal"
                                                            meta:resourcekey="lblEndFormulaResource1"></asp:Label>
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:TextBox ID="txtEndFormula" runat="server" SkinID="TextBox_LargeNormalltr" MaxLength="255"
                                                            meta:resourcekey="txtEndFormulaResource1"></asp:TextBox>
                                                        <br />
                                                        <asp:Label ID="lblEndFormulaDesc" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="lblEndFormulaDescResource1"></asp:Label>
                                                    </td>
                                                    <td class="search">
                                                        <igtxt:WebImageButton ID="btnEndFormula" runat="server" AutoSubmit="False" Height="18px"
                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnEndFormulaResource1">
                                                            <Alignments TextImage="ImageBottom" />
                                                            <Appearance>
                                                                <Image Url="./Img/rtg_rate.gif" />
                                                            </Appearance>
                                                        </igtxt:WebImageButton>
                                                        <br />
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
                                                        <asp:CheckBox ID="CheckBox_IsSalaryEOSExeclude" runat="server" />
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:Label ID="Label_IsSalaryEOSExeclude" runat="server" SkinID="Label_DefaultNormal"
                                                            Text="مستبعد من مستحقات الراتب لنهاية الخدمة" meta:resourcekey="lblIsSalaryEOSExecludeResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 47%; height: 16px; vertical-align: top">
                                        </td>
                                        <td style="width: 6%; height: 16px; vertical-align: top">
                                        </td>
                                        <td style="width: 47%; height: 16px; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td class="SeparArea">
                                                    </td>
                                                    <td class="LabelArea">
                                                        <asp:CheckBox ID="CheckBox_IsProjectRelatedItem" runat="server" />
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:Label ID="Label_IsProjectRelatedItem" runat="server" SkinID="Label_DefaultNormal"
                                                            Text="يوزع على مستوى المشاريع" meta:resourcekey="lblIsProjectRelatedItemResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 47%; height: 16px; vertical-align: top">
                                        </td>
                                        <td style="width: 6%; height: 16px; vertical-align: top">
                                        </td>
                                        <td style="width: 47%; height: 16px; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td class="SeparArea">
                                                    </td>
                                                    <td class="LabelArea">
                                                        <asp:CheckBox ID="chkHasInsuranceTiers"  AutoPostBack="True" runat="server" />
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:Label ID="lblHasInsuranceTiers" runat="server" SkinID="Label_DefaultNormal"
                                                            Text="HasInsuranceTiers" meta:resourcekey="lblHasInsuranceTiersResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                            <td style="height: 16px; vertical-align: top" colspan="3">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                          <igtbl:UltraWebGrid  Browser="UpLevel"   ID="UwgSearchEmployees" runat="server" EnableAppStyling="False"
                                                                Height="100%" meta:resourcekey="uwgForNationalityResource1" SkinID="Default"
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
                                                                             <igtbl:UltraGridColumn BaseColumnName="TransactionsTypesId" Hidden="True" Key="TransactionsTypesId">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="SerialNumberTiers" Key="SerialNumberTiers"
                                                                                meta:resourcekey="UltraGridColumnSerial" Width="5%">
                                                                                <Header Caption="Serial">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                 <CellStyle HorizontalAlign="Center" BackColor="LightGray" />
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="FinancialPeriodTiers" Key="FinancialPeriodTiers" Type="Custom" EditorControlID="txtDate"
                                                                    Format="dd/MM/yyyy"  meta:resourcekey="UltraGridColumnFinancialPeriodTiers" Width="25%">
                                                                                <Header Caption="FinancialPeriodTiers">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="BaseFormulaTiers" Key="BaseFormulaTiers"
                                                                                meta:resourcekey="UltraGridColumnBaseFormulaTiers" Width="25%">
                                                                                <Header Caption="BaseFormulaTiers">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="BeginFormulaTiers" Key="BeginFormulaTiers"
                                                                                meta:resourcekey="UltraGridColumnBeginFormulaTiers" Width="25%">
                                                                                <Header Caption="BeginFormulaTiers">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="EndFormulaTiers" Key="EndFormulaTiers"
                                                                                meta:resourcekey="UltraGridColumnEndFormulaTiers" Width="25%">
                                                                                <Header Caption="EndFormulaTiers">
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
