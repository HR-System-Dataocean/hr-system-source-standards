<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DocumentsListItems.aspx.vb"
    Inherits="DocumentsListItems" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Document List Items</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="Scripts/App_EndOfServicesTypes.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/Float.js" type="text/javascript"></script>
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
            try {
                $(".Validators").Float();
            }
            catch (err) { }
        });
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="DocumentsListItems" runat="server">
    <div style="display: none">
        <igtxt:WebTextEdit meta:resourcekey="WebTextEdit1Recourcekey" ID="WebTextEdit1" runat="server"
            Style="display: none;">
        </igtxt:WebTextEdit>
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display: none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N"
                                    meta:resourcekey="ImageButton1Resource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                    SkinID="HrSave_Command" CommandArgument="Save" meta:resourcekey="ImageButton_SaveResource1"
                                    ValidationGroup="G" OnClientClick="SaveOtherFieldsData();" />
                            </td>
                            <td style="width: 120px">
                                <asp:ImageButton ID="ImageButton_SaveN" Width="16px" Height="16px" runat="server"
                                    CommandArgument="SaveNew" SkinID="HrSaveN_Command" meta:resourcekey="ImageButton_SaveNResource1"
                                    ValidationGroup="G" OnClientClick="SaveOtherFieldsData();" />
                                <asp:LinkButton ID="LinkButton_SaveN" runat="server" Text="حفظ مع جديد" CommandArgument="SaveNew"
                                    meta:resourcekey="LinkButton_SaveNResource1" ValidationGroup="G" OnClientClick="SaveOtherFieldsData();"></asp:LinkButton>
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
                                    SkinID="HrProperties_Command" CommandArgument="Property" meta:resourcekey="ImageButton_PropertiesResource1" />
                                <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="خصائص" CommandArgument="Property"
                                    meta:resourcekey="LinkButton_PropertiesResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                    SkinID="HrRemarks_Command" CommandArgument="Remarks" meta:resourcekey="ImageButton_RemarksResource1" />
                                <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="ملاحظات" CommandArgument="Remarks"
                                    meta:resourcekey="LinkButton_RemarksResource1"></asp:LinkButton>
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
                            <td style="width: 10px">
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Back" Width="16px" Height="16px" runat="server"
                                    SkinID="HrBack_Command" CommandArgument="Previous" meta:resourcekey="ImageButton_BackResource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_First" Width="16px" Height="16px" runat="server"
                                    SkinID="HrFirest_Command" CommandArgument="First" meta:resourcekey="ImageButton_FirstResource1" />
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
                                                <asp:HiddenField ID="HiddenField_ISCopy" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblCode" runat="server" Text="Code" SkinID="Label_DefaultNormal" meta:resourcekey="lblCodeResource1"></asp:Label>
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
                                                            <asp:Label ID="lblEngName" runat="server" Text="English Name" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalltr" MaxLength="255"
                                                                meta:resourcekey="txtEngNameResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lblArbName" runat="server" Text="Arabic Name" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblArbNameResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                                                                meta:resourcekey="txtArbNameResource1"></asp:TextBox>
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
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label_Type" runat="server" Text="نوع المصدر" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="Label_TypeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DropDownList_Type" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="DropDownList_TypeResource1">
                                                                <asp:ListItem Selected="True" Value="1" meta:resourcekey="ListItemResource1">جدول مباشر</asp:ListItem>
                                                                <asp:ListItem Value="2" meta:resourcekey="ListItemResource2">إجراء مخزن</asp:ListItem>
                                                                <asp:ListItem Value="3" meta:resourcekey="ListItemResource3">قائمة عناصر</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label_DataSource" runat="server" Text="مصدر القائمة" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="Label_DataSourceResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="TextBox_DataSource" runat="server" SkinID="TextBox_DefaultNormalltr"
                                                                MaxLength="255" meta:resourcekey="TextBox_DataSourceResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label_ValueMember" runat="server" Text="عنصر القيمة" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="Label_ValueMemberResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="TextBox_ValueMember" runat="server" SkinID="TextBox_DefaultNormalltr"
                                                                MaxLength="255" meta:resourcekey="TextBox_ValueMemberResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label_Ar_DisplayMember" runat="server" Text="عنصر العرض العربى" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="Label_Ar_DisplayMemberResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="TextBox_Ar_DisplayMember" runat="server" SkinID="TextBox_DefaultNormalrtl"
                                                                MaxLength="255" onchange="this.style.borderColor = 'silver';" meta:resourcekey="TextBox_Ar_DisplayMemberResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label_En_DisplayMember" runat="server" Text="عنصر العرض الانجليزى"
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label_En_DisplayMemberResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="TextBox_En_DisplayMember" runat="server" SkinID="TextBox_DefaultNormalltr"
                                                                MaxLength="255" onchange="this.style.borderColor = 'silver';" meta:resourcekey="TextBox_En_DisplayMemberResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                    cellspacing="6">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label_Title1" runat="server" Text="برجاء إضافة القيم التابعة للقائمة"
                                                                SkinID="Label_DefaultBold" meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: top;">
                                            </td>
                                            <td style="width: 47%; vertical-align: top;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 16px; vertical-align: top" colspan="3">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgEndOfServiceRules" runat="server"
                                                                Height="280px" Width="325px" SkinID="Default" 
                                                                meta:resourcekey="uwgEndOfServiceRulesResource1">
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowAddNewDefault="Yes" AllowColumnMovingDefault="OnServer"
                                                                    AllowDeleteDefault="Yes" AllowSortingDefault="OnClient" AllowUpdateDefault="Yes"
                                                                    BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgEndOfServiceRules"
                                                                    RowHeightDefault="18px" SelectTypeRowDefault="Extended"
                                                                    StationaryMargins="Header" AutoGenerateColumns="False" StationaryMarginsOutlookGroupBy="True"
                                                                    TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" 
                                                                    AllowRowNumberingDefault="Continuous">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="280px"
                                                                        Width="325px">
                                                                    </FrameStyle>
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
                                                                    <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" HorizontalAlign="Center"
                                                                        Height="20px" VerticalAlign="Middle" Font-Names="tahoma" Font-Size="9pt">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </HeaderStyleDefault>
                                                                    <RowSelectorStyleDefault Font-Size="7pt" Width="40px">
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
                                                                    <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ListItemID" DataType="System.Int32" FieldLen="4"
                                                                                Hidden="True" Key="ListItemID" meta:resourcekey="UltraGridColumnResource1">
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="EngName" Width="50%" meta:resourcekey="UltraGridColumnResource5">
                                                                                <Header Caption="English Value" Title="EngName">
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Header>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ArbName" Width="50%" EditorControlID="WebTextEdit1"
                                                                                Type="Custom" meta:resourcekey="UltraGridColumnResource6">
                                                                                <Header Caption="Arabic Value" Title="ArbName">
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
                                                                        <AddNewRow Visible="Yes">
                                                                        </AddNewRow>
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
