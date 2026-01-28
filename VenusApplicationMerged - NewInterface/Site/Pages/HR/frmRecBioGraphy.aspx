<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRecBioGraphy.aspx.vb"
    Inherits="frmRecBioGraphy" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~BioGraphy</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_frmRecBioGraphy.js" type="text/javascript"></script>

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
    </script>
    <style type="text/css">
        .igwtMainBlue2k7
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
            color: #15428B;
            padding-right: 12px;
            padding-left: 12px; /*border: 1px solid red;*/
            background-color: #BFDBFF;
        }
    </style>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmRecBioGraphy" runat="server">
    <div style="display: none">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <asp:HiddenField ID="txtLang" runat="server" Value="Eng" />
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" 
            Width="99px" meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" 
            Width="99px" meta:resourcekey="TargetControlResource1"></asp:Label>
        <igtxt:WebTextEdit meta:resourcekey="WebTextEdit1Recourcekey" ID="WebTextEdit1" runat="server"
            Height="22px" Style="z-index: 1; left: 383px; top: 206px; position: absolute;
            width: 14%;">
        </igtxt:WebTextEdit>
        <igtxt:WebDateTimeEdit meta:resourcekey="WebDateTimeEdit1Recourcekey" ID="WebDateTimeEdit1"
            runat="server" EditModeFormat="">
        </igtxt:WebDateTimeEdit>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display:none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" 
                                    CommandArgument="N" meta:resourcekey="ImageButton1Resource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                    SkinID="HrSave_Command" CommandArgument="Save" meta:resourcekey="ImageButton_SaveResource1"
                                    OnClientClick="SaveOtherFieldsData();" />
                            </td>
                            <td style="width: 120px">
                                <asp:ImageButton ID="ImageButton_SaveN" Width="16px" Height="16px" runat="server"
                                    CommandArgument="SaveNew" SkinID="HrSaveN_Command" meta:resourcekey="ImageButton_SaveNResource1"
                                    OnClientClick="SaveOtherFieldsData();" />
                                <asp:LinkButton ID="LinkButton_SaveN" runat="server" Text="حفظ مع جديد" CommandArgument="SaveNew"
                                    meta:resourcekey="LinkButton_SaveNResource1" OnClientClick="SaveOtherFieldsData();"></asp:LinkButton>
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
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" 
                        SkinID="Default" meta:resourcekey="UltraWebTab1Resource1"
                        >
                        <Tabs>
                            <igtab:Tab Text="عام" meta:resourcekey="TabMain">
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
                                                            <asp:Label ID="lblCode" runat="server" Text="Code " SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                                            <asp:Label ID="Label_Star1" runat="server" Text="*" Style="color: #FF0000" meta:resourcekey="Label_Star1Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtCode" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                AutoPostBack="True" meta:resourcekey="txtCodeResource1"></asp:TextBox>
                                                            &nbsp;<igtxt:WebImageButton ID="btnSearchCode" runat="server" AutoSubmit="False"
                                                                Height="18px" Overflow="NoWordWrap"
                                                                UseBrowserDefaults="False" Width="24px" 
                                                                meta:resourcekey="btnSearchCodeResource1">
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
                                                &nbsp;
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" SkinID="Label_WarningBold" 
                                                                meta:resourcekey="Label1Resource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 16px; vertical-align: top" colspan="3">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 11%;">
                                                        </td>
                                                        <td style="width: 21%; text-align: center;">
                                                            <asp:Label ID="lblName" runat="server" Text="Name" SkinID="Label_DefaultNormal" 
                                                                meta:resourcekey="lblNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 21%; text-align: center;">
                                                            <asp:Label ID="lblFatherName" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Father Name" meta:resourcekey="lblFatherNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 21%; text-align: center;">
                                                            <asp:Label ID="lblGrandName" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Grand Name" meta:resourcekey="lblGrandNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 21%; text-align: center;">
                                                            <asp:Label ID="lblFamilyName" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Family Name" meta:resourcekey="lblFamilyNameResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 16px; vertical-align: top" colspan="3">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 1%;">
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:Label ID="lblEngName" runat="server"
                                                                SkinID="Label_DefaultNormal" Text="English name" 
                                                                meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtEngNameResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;">
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtEngFathername" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtEngFathernameResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;">
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtEngGrandName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtEngGrandNameResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;">
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtEngFamilyName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtEngFamilyNameResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 16px; vertical-align: top" colspan="3">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 1%;">
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:Label ID="lblArbName" runat="server"
                                                                SkinID="Label_DefaultNormal" Text="Arabic name" 
                                                                meta:resourcekey="lblArbNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtArbNameResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;">
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtArbFatherName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtArbFatherNameResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;">
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtArbGrandName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtArbGrandNameResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;">
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtArbFamilyName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtArbFamilyNameResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;">
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
                                                            <asp:Label ID="lblPhone" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Phone No" meta:resourcekey="lblPhoneResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtPhone" runat="server" SkinID="TextBox_DefaultNormalC" 
                                                                meta:resourcekey="txtPhoneResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lblGender" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Gender" meta:resourcekey="lblGenderResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlGender" runat="server" 
                                                                SkinID="DropDownList_DefaultNormal" meta:resourcekey="ddlGenderResource1">
                                                                <asp:ListItem Value="M" meta:resourcekey="ListItemResource1" Text="Male"></asp:ListItem>
                                                                <asp:ListItem Value="F" meta:resourcekey="ListItemResource2" Text="Female"></asp:ListItem>
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="lblMobile" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Mobile No" meta:resourcekey="lblMobileResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtMobile" runat="server" SkinID="TextBox_DefaultNormalC" 
                                                                meta:resourcekey="txtMobileResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lblMaritalStatus" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Marital Status" meta:resourcekey="lblMaritalStatusResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlMaritalStatus" runat="server" 
                                                                SkinID="DropDownList_DefaultNormal" 
                                                                meta:resourcekey="ddlMaritalStatusResource1">
                                                                <asp:ListItem Value="S" meta:resourcekey="ListItemResource3" Text ="Single"></asp:ListItem>
                                                                <asp:ListItem Value="M" meta:resourcekey="ListItemResource4" Text="Married"></asp:ListItem>
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="lblEmail" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="E Mail" meta:resourcekey="lblEmailResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtEmail" runat="server" SkinID="TextBox_DefaultNormalC" 
                                                                meta:resourcekey="txtEmailResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lblRelegion" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Religion" meta:resourcekey="lblRelegionResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlRelegion" runat="server" 
                                                                SkinID="DropDownList_DefaultNormal" meta:resourcekey="ddlRelegionResource1">
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="lblAddress" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Address" meta:resourcekey="lblAddressResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtAddress" runat="server" SkinID="TextBox_DefaultNormalC" 
                                                                meta:resourcekey="txtAddressResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lblNationality" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Nationality" meta:resourcekey="lblNationalityResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlNationality" runat="server" 
                                                                SkinID="DropDownList_DefaultNormal" meta:resourcekey="ddlNationalityResource1">
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="lblLastJob" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Last Job" meta:resourcekey="lblLastJobResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtLastJob" runat="server" SkinID="TextBox_DefaultNormalC" 
                                                                meta:resourcekey="txtLastJobResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lblPosition" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Suit Position" meta:resourcekey="lblPositionResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlPosition" runat="server" 
                                                                SkinID="DropDownList_DefaultNormal" meta:resourcekey="ddlPositionResource1">
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="lbliqamano" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="IQama No/SSN" meta:resourcekey="lbliqamanoResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtiqamano" runat="server" InputMask="################" 
                                                                SkinID="WebMaskEdit_Fix" meta:resourcekey="txtiqamanoResource1">
                                                            </igtxt:WebMaskEdit>
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
                                                            <asp:Label ID="lblNODependancies" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="NO Dependancies" meta:resourcekey="lblNODependanciesResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtNODependancies" runat="server" DataMode="Int" SkinID="WebNumericEdit_Fix"
                                                                ValueText="0" meta:resourcekey="txtNODependanciesResource1">
                                                            </igtxt:WebNumericEdit>
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
                                                            <asp:Label ID="lblpassportno" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Passport No" meta:resourcekey="lblpassportnoResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtpassportno" runat="server" InputMask="################"
                                                                SkinID="WebMaskEdit_Fix" meta:resourcekey="txtpassportnoResource1">
                                                            </igtxt:WebMaskEdit>
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
                                                            <asp:Label ID="lblLastSalary" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Last Salary" meta:resourcekey="lblLastSalaryResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtLastSalary" runat="server" DataMode="Int" SkinID="WebNumericEdit_Fix"
                                                                ValueText="0" meta:resourcekey="txtLastSalaryResource1">
                                                            </igtxt:WebNumericEdit>
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
                                                            <asp:Label ID="lblGBirthDate" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Greg Birth Date" meta:resourcekey="lblGBirthDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebDateTimeEdit ID="txtGBirthDate" runat="server" AutoPostBack="True" 
                                                                SkinID="WebDateTimeEdit_Fix" meta:resourcekey="txtGBirthDateResource1">
                                                            </igtxt:WebDateTimeEdit>
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
                                                            <asp:Label ID="lblExpSalary" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Expected Salary" meta:resourcekey="lblExpSalaryResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtExpSalary" runat="server" DataMode="Int" SkinID="WebNumericEdit_Fix"
                                                                ValueText="0" meta:resourcekey="txtExpSalaryResource1">
                                                            </igtxt:WebNumericEdit>
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
                                                            <asp:Label ID="lblHBirthDate" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Hijri Birth Date" meta:resourcekey="lblHBirthDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebDateTimeEdit ID="txtHBirthDate" runat="server" AutoPostBack="True" 
                                                                SkinID="WebDateTimeEdit_Fix" meta:resourcekey="txtHBirthDateResource1">
                                                            </igtxt:WebDateTimeEdit>
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
                                                            <asp:Label ID="lblHasDriverLic" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Driver Lic" meta:resourcekey="lblHasDriverLicResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="HasDriverLic" runat="server" 
                                                                meta:resourcekey="HasDriverLicResource2" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 180px; vertical-align: top" colspan="3">
                                                <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" 
                                                    meta:resourcekey="WebAsyncRefreshPanel1Resource1">
                                                    <igtab:UltraWebTab ID="UltraWebTab2" runat="server" EnableAppStyling="True" Height="100%"
                                                         SkinID="Default" meta:resourcekey="UltraWebTab2Resource1">
                                                        <Tabs>
                                                            <igtab:Tab meta:resourcekey="TabNanSaudi" Text="Nan Saudi" Key="">
                                                                <ContentTemplate>
                                                                    <table style="width: 100%; height: 100%; vertical-align: top" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                                                    border-bottom: 1px solid black">
                                                                                    <tr>
                                                                                        <td style="vertical-align: bottom">
                                                                                            <asp:Label ID="Label8" runat="server" SkinID="Label_DefaultBold" 
                                                                                                Text="Please Setect Setting For Non Saudi" meta:resourcekey="Label8Resource1"></asp:Label>
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
                                                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                                                    <tr>
                                                                                        <td class="SeparArea">
                                                                                        </td>
                                                                                        <td class="DataArea">
                                                                                            <asp:Label ID="lblHasDriverLic0" runat="server" SkinID="Label_DefaultNormal" 
                                                                                                Text="Has Transfeerable Iqama" meta:resourcekey="lblHasDriverLic0Resource1"></asp:Label>
                                                                                        </td>
                                                                                        <td class="LabelArea">
                                                                                            <asp:CheckBox ID="HasTransIqama" runat="server" 
                                                                                                meta:resourcekey="HasTransIqamaResource1" />
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
                                                                                        <td class="DataArea">
                                                                                            <asp:Label ID="lblGBirthDate0" runat="server" SkinID="Label_DefaultNormal" 
                                                                                                Text="Has No Objection From Pre Sponsor" 
                                                                                                meta:resourcekey="lblGBirthDate0Resource1"></asp:Label>
                                                                                        </td>
                                                                                        <td class="LabelArea">
                                                                                            <asp:CheckBox ID="HasNOSponsor" runat="server" 
                                                                                                meta:resourcekey="HasNOSponsorResource1" />
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
                                                                                        </td>
                                                                                        <td class="DataArea">
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
                                                                                        </td>
                                                                                        <td class="DataArea">
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
                                                                                        </td>
                                                                                        <td class="DataArea">
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
                                                                                        </td>
                                                                                        <td class="DataArea">
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
                                                                                        </td>
                                                                                        <td class="DataArea">
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
                                                                    </table>
                                                                </ContentTemplate>
                                                            </igtab:Tab>
                                                            <igtab:Tab Text="Special Condition" meta:resourcekey="TabResource1">
                                                                <ContentTemplate>
                                                                    <table style="width: 100%; height: 100%; vertical-align: top" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                                                    border-bottom: 1px solid black">
                                                                                    <tr>
                                                                                        <td style="vertical-align: bottom">
                                                                                            <asp:Label ID="Label2" runat="server" SkinID="Label_DefaultBold" 
                                                                                                Text="Please Select Special Condition" meta:resourcekey="Label2Resource1"></asp:Label>
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
                                                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                                                    <tr>
                                                                                        <td class="SeparArea">
                                                                                        </td>
                                                                                        <td class="DataArea">
                                                                                            <asp:Label ID="lblHasSConditions" runat="server" SkinID="Label_DefaultNormal" 
                                                                                                Text="Special Condition Affect Work" 
                                                                                                meta:resourcekey="lblHasSConditionsResource1"></asp:Label>
                                                                                        </td>
                                                                                        <td class="LabelArea">
                                                                                            <asp:CheckBox ID="HasSConditions" runat="server" 
                                                                                                meta:resourcekey="HasSConditionsResource2" />
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
                                                                                        <td class="LabelArea" style="vertical-align: top">
                                                                                            <asp:Label ID="lbltxtSConditions" runat="server" SkinID="Label_DefaultNormal" 
                                                                                                Text="Special Conditions" meta:resourcekey="lbltxtSConditionsResource1"></asp:Label>
                                                                                        </td>
                                                                                        <td class="DataArea">
                                                                                            <asp:TextBox ID="txtSConditions" runat="server" Style="font-family: Tahoma; font-size: 8pt;
                                                                                                font-weight: Normal; color: black; vertical-align: middle; text-align: Left"
                                                                                                BorderStyle="Solid" BorderWidth="1px" Width="100%" Height="60px" TextMode="MultiLine"
                                                                                                BorderColor="#CCCCCC" meta:resourcekey="txtSConditionsResource2"></asp:TextBox>
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
                                                                                        </td>
                                                                                        <td class="DataArea">
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
                                                                                        </td>
                                                                                        <td class="DataArea">
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
                                                                                        </td>
                                                                                        <td class="DataArea">
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
                                                                    </table>
                                                                </ContentTemplate>
                                                            </igtab:Tab>
                                                            <igtab:Tab Text="References" meta:resourcekey="TabResource2">
                                                                <ContentTemplate>
                                                                    <table cellspacing="0" style="width: 100%; height: 100%; vertical-align: top">
                                                                        <tr>
                                                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                                                    border-bottom: 1px solid black">
                                                                                    <tr>
                                                                                        <td style="vertical-align: bottom">
                                                                                            <asp:Label ID="Label3" runat="server" SkinID="Label_DefaultBold" 
                                                                                                Text="Please Add References" meta:resourcekey="Label3Resource1"></asp:Label>
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
                                                                            <td colspan="3" style="height: 140px; width: 100%; vertical-align: top">
                                                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgReferences" runat="server" EnableAppStyling="False" Height="100%"
                                                                                    SkinID="Default" Width="100%" meta:resourcekey="uwgReferencesResource1">
                                                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                                                        RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                                        StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                                        Version="4.00" ViewType="OutlookGroupBy">
                                                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                                            Width="100%">
                                                                                        </FrameStyle>
                                                                                        <ClientSideEvents AfterCellUpdateHandler="AddRow" />
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
                                                                                        <igtbl:UltraGridBand AllowAdd="Yes" meta:resourcekey="UltraGridBandResource1">
                                                                                            <RowEditTemplate>
                                                                                                <p align="right">
                                                                                                    <br />
                                                                                                </p>
                                                                                                <br />
                                                                                                <p align="center">
                                                                                                </p>
                                                                                            </RowEditTemplate>
                                                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                                                            </AddNewRow>
                                                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                                    Font-Size="11px" Width="200px">
                                                                                                    <Padding Left="2px" />
                                                                                                </FilterDropDownStyle>
                                                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                                                </FilterHighlightRowStyle>
                                                                                            </FilterOptions>
                                                                                            <Columns>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID"
                                                                                                    Width="0px" meta:resourcekey="UltraGridColumnResource1">
                                                                                                    <Header Caption="ID">
                                                                                                    </Header>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="EngName" Key="EngName"
                                                                                                    Width="30%" meta:resourcekey="UltraGridColumnResource2">
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Header Caption="Reference English" Title="EngName">
                                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                                    </Header>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="ArbName" EditorControlID="WebTextEdit1" Key="ArbName"
                                                                                                    Type="Custom" Width="30%" meta:resourcekey="UltraGridColumnResource3">
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Header Caption="Reference Arabic" Title="ArbName">
                                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                                    </Header>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="Phone" Key="Phone" Width="10%" 
                                                                                                    meta:resourcekey="UltraGridColumnResource4">
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Header Caption="Phone" Title="Phone">
                                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                                    </Header>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="Fax" Key="Fax" Width="10%" 
                                                                                                    meta:resourcekey="UltraGridColumnResource5">
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Header Caption="Fax" Title="Fax">
                                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                                    </Header>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="E_Mail" Key="E_Mail" Width="20%" 
                                                                                                    meta:resourcekey="UltraGridColumnResource6">
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Header Caption="E_Mail" Title="E_Mail">
                                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                                    </Header>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                            </Columns>
                                                                                            <RowTemplateStyle BackColor="Transparent" BorderColor="Transparent" BorderStyle="Ridge">
                                                                                                <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                                                            </RowTemplateStyle>
                                                                                        </igtbl:UltraGridBand>
                                                                                    </Bands>
                                                                                </igtbl:UltraWebGrid>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </igtab:Tab>
                                                            <igtab:Tab Text="Language" meta:resourcekey="TabResource3">
                                                                <ContentTemplate>
                                                                    <table cellspacing="0" style="width: 100%; height: 100%; vertical-align: top">
                                                                        <tr>
                                                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                                                    border-bottom: 1px solid black">
                                                                                    <tr>
                                                                                        <td style="vertical-align: bottom">
                                                                                            <asp:Label ID="Label4" runat="server" SkinID="Label_DefaultBold" 
                                                                                                Text="Please Add Language" meta:resourcekey="Label4Resource1"></asp:Label>
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
                                                                            <td colspan="3" style="height: 140px; width: 100%; vertical-align: top">
                                                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgLanguage" runat="server" EnableAppStyling="False" Height="100%"
                                                                                    SkinID="Default" Width="100%" meta:resourcekey="uwgLanguageResource1">
                                                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                                                        RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                                        StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                                        Version="4.00" ViewType="OutlookGroupBy">
                                                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                                            Width="100%">
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
                                                                                        <ClientSideEvents AfterCellUpdateHandler="uwgLanguage_AfterCellUpdateHandler" AfterEnterEditModeHandler="uwg_AfterEnterEditModeHandler" />
                                                                                    </DisplayLayout>
                                                                                    <Bands>
                                                                                        <igtbl:UltraGridBand AllowAdd="Yes" meta:resourcekey="UltraGridBandResource2">
                                                                                            <RowEditTemplate>
                                                                                                <p align="right">
                                                                                                    <br />
                                                                                                </p>
                                                                                                <br />
                                                                                                <p align="center">
                                                                                                </p>
                                                                                            </RowEditTemplate>
                                                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                                                            </AddNewRow>
                                                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                                    Font-Size="11px" Width="200px">
                                                                                                    <Padding Left="2px" />
                                                                                                </FilterDropDownStyle>
                                                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                                                </FilterHighlightRowStyle>
                                                                                            </FilterOptions>
                                                                                            <Columns>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID"
                                                                                                    Width="0px" meta:resourcekey="UltraGridColumnResource7">
                                                                                                    <Header Caption="ID">
                                                                                                    </Header>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="Language_ID"
                                                                                                    Key="Language_ID" Width="50%" Type="DropDownList" DataType="System.Int32" 
                                                                                                    meta:resourcekey="UltraGridColumnResource8">
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Header Caption="Language" Title="">
                                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                                    </Header>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="SLevel_ID" Key="SLevel_ID" Type="DropDownList"
                                                                                                    Width="25%" DataType="System.Int32" 
                                                                                                    meta:resourcekey="UltraGridColumnResource9">
                                                                                                    <Header Caption="Write Level">
                                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                                    </Header>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="WLevel_ID" Key="WLevel_ID" Type="DropDownList"
                                                                                                    Width="25%" DataType="System.Int32" 
                                                                                                    meta:resourcekey="UltraGridColumnResource10">
                                                                                                    <Header Caption="Read Level">
                                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                                    </Header>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                            </Columns>
                                                                                            <RowTemplateStyle BackColor="Transparent" BorderColor="Transparent" BorderStyle="Ridge">
                                                                                                <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                                                            </RowTemplateStyle>
                                                                                        </igtbl:UltraGridBand>
                                                                                    </Bands>
                                                                                </igtbl:UltraWebGrid>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </igtab:Tab>
                                                            <igtab:Tab Text="Educational Degree" meta:resourcekey="TabResource4">
                                                                <ContentTemplate>
                                                                    <table cellspacing="0" style="width: 100%; height: 100%; vertical-align: top">
                                                                        <tr>
                                                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                                                    border-bottom: 1px solid black">
                                                                                    <tr>
                                                                                        <td style="vertical-align: bottom">
                                                                                            <asp:Label ID="Label5" runat="server" SkinID="Label_DefaultBold" 
                                                                                                Text="Please Add Educational Degree" meta:resourcekey="Label5Resource1"></asp:Label>
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
                                                                            <td colspan="3" style="height: 140px; width: 100%; vertical-align: top">
                                                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgCertifications" runat="server" EnableAppStyling="False"
                                                                                    Height="100%" SkinID="Default"
                                                                                    Width="100%" meta:resourcekey="uwgCertificationsResource1">
                                                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                                                        RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                                        StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                                        Version="4.00" ViewType="OutlookGroupBy">
                                                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                                            Width="100%">
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
                                                                                        <ClientSideEvents AfterCellUpdateHandler="uwgCertifications_AfterCellUpdateHandler"
                                                                                            AfterEnterEditModeHandler="uwg_AfterEnterEditModeHandler" />
                                                                                    </DisplayLayout>
                                                                                    <Bands>
                                                                                        <igtbl:UltraGridBand AllowAdd="Yes" meta:resourcekey="UltraGridBandResource3">
                                                                                            <RowEditTemplate>
                                                                                                <p align="right">
                                                                                                    <br />
                                                                                                </p>
                                                                                                <br />
                                                                                                <p align="center">
                                                                                                </p>
                                                                                            </RowEditTemplate>
                                                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                                                            </AddNewRow>
                                                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                                    Font-Size="11px" Width="300px">
                                                                                                    <Padding Left="2px" />
                                                                                                </FilterDropDownStyle>
                                                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                                                </FilterHighlightRowStyle>
                                                                                            </FilterOptions>
                                                                                            <Columns>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="0px" 
                                                                                                    meta:resourcekey="UltraGridColumnResource11">
                                                                                                    <Header Caption="ID">
                                                                                                    </Header>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="EDegree_ID" Key="EDegree_ID" Width="40%" DataType="System.Int32"
                                                                                                    Type="DropDownList" meta:resourcekey="UltraGridColumnResource12">
                                                                                                    <Header Caption="Education Degree">
                                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                                    </Header>
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="GDateFrom" Key="GDateFrom" Width="15%" EditorControlID="WebDateTimeEdit1"
                                                                                                    Type="Custom" Format="" meta:resourcekey="UltraGridColumnResource13">
                                                                                                    <Header Caption="From Gerg">
                                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                                    </Header>
                                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="HDateFrom" Key="HDateFrom" Width="15%" Format="dd/MM/yyyy"
                                                                                                    EditorControlID="WebDateTimeEdit1" Type="Custom" 
                                                                                                    meta:resourcekey="UltraGridColumnResource14">
                                                                                                    <Header Caption="From Hijri">
                                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                                    </Header>
                                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="GDateTo" Key="GDateTo" Width="15%" Format=""
                                                                                                    EditorControlID="WebDateTimeEdit1" Type="Custom" 
                                                                                                    meta:resourcekey="UltraGridColumnResource15">
                                                                                                    <Header Caption="To Gerg">
                                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                                    </Header>
                                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="HDateTo" Key="HDateTo" Width="15%" Format="dd/MM/yyyy"
                                                                                                    EditorControlID="WebDateTimeEdit1" Type="Custom" 
                                                                                                    meta:resourcekey="UltraGridColumnResource16">
                                                                                                    <Header Caption="To Hijri">
                                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                                    </Header>
                                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn Width="1px" AllowUpdate="No" 
                                                                                                    meta:resourcekey="UltraGridColumnResource17">
                                                                                                    <Header>
                                                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                                                    </Header>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                            </Columns>
                                                                                            <RowTemplateStyle BackColor="Transparent" BorderColor="Transparent" BorderStyle="Ridge">
                                                                                                <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                                                            </RowTemplateStyle>
                                                                                        </igtbl:UltraGridBand>
                                                                                    </Bands>
                                                                                </igtbl:UltraWebGrid>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </igtab:Tab>
                                                            <igtab:Tab Text="Employment History" meta:resourcekey="TabResource5">
                                                                <ContentTemplate>
                                                                    <table cellspacing="0" style="width: 100%; height: 100%; vertical-align: top">
                                                                        <tr>
                                                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                                                    border-bottom: 1px solid black">
                                                                                    <tr>
                                                                                        <td style="vertical-align: bottom">
                                                                                            <asp:Label ID="Label6" runat="server" SkinID="Label_DefaultBold" 
                                                                                                Text="Please Add Employment History" meta:resourcekey="Label6Resource1"></asp:Label>
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
                                                                            <td colspan="3" style="height: 140px; width: 100%; vertical-align: top">
                                                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgHistory" runat="server" EnableAppStyling="False" Height="100%"
                                                                                    SkinID="Default" Width="100%" meta:resourcekey="uwgHistoryResource1">
                                                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                                                        RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                                        StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                                        Version="4.00" ViewType="OutlookGroupBy">
                                                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                                            Width="100%">
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
                                                                                        <ClientSideEvents AfterCellUpdateHandler="uwgHistory_AfterCellUpdateHandler" AfterEnterEditModeHandler="uwg_AfterEnterEditModeHandler" />
                                                                                    </DisplayLayout>
                                                                                    <Bands>
                                                                                        <igtbl:UltraGridBand AllowAdd="Yes" meta:resourcekey="UltraGridBandResource4">
                                                                                            <RowEditTemplate>
                                                                                                <p align="right">
                                                                                                    <br />
                                                                                                </p>
                                                                                                <br />
                                                                                                <p align="center">
                                                                                                </p>
                                                                                            </RowEditTemplate>
                                                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                                                            </AddNewRow>
                                                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                                    Font-Size="11px" Width="200px">
                                                                                                    <Padding Left="2px" />
                                                                                                </FilterDropDownStyle>
                                                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                                                </FilterHighlightRowStyle>
                                                                                            </FilterOptions>
                                                                                            <Columns>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID"
                                                                                                    Width="0px" meta:resourcekey="UltraGridColumnResource18">
                                                                                                    <Header Caption="ID">
                                                                                                    </Header>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="EngName" Key="EngName"
                                                                                                    Width="20%" meta:resourcekey="UltraGridColumnResource19">
                                                                                                    <Header Caption="English Name">
                                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                                    </Header>
                                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="ArbName" Key="ArbName" Width="20%" EditorControlID="WebTextEdit1"
                                                                                                    Type="Custom" meta:resourcekey="UltraGridColumnResource20">
                                                                                                    <Header Caption="Arabic Name">
                                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                                    </Header>
                                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="GDateFrom" Key="GDateFrom" Width="10%" Format="dd/MM/yyyy"
                                                                                                    EditorControlID="WebDateTimeEdit1" Type="Custom" 
                                                                                                    DataType="System.DateTime" meta:resourcekey="UltraGridColumnResource21">
                                                                                                    <Header Caption="From Gerg">
                                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                                    </Header>
                                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="HDateFrom" Key="HDateFrom" Width="10%" Format="dd/MM/yyyy"
                                                                                                    EditorControlID="WebDateTimeEdit1" Type="Custom" 
                                                                                                    meta:resourcekey="UltraGridColumnResource22">
                                                                                                    <Header Caption="From Hijri">
                                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                                    </Header>
                                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="GDateTo" Key="GDateTo" Width="10%" Format="dd/MM/yyyy"
                                                                                                    EditorControlID="WebDateTimeEdit1" Type="Custom" 
                                                                                                    DataType="System.DateTime" meta:resourcekey="UltraGridColumnResource23">
                                                                                                    <Header Caption="To Gerg">
                                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                                    </Header>
                                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="HDateTo" Key="HDateTo" Width="10%" Format="dd/MM/yyyy"
                                                                                                    EditorControlID="WebDateTimeEdit1" Type="Custom" 
                                                                                                    meta:resourcekey="UltraGridColumnResource24">
                                                                                                    <Header Caption="To Hijri">
                                                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                                                    </Header>
                                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="Years" Key="Years" Format="" Width="7%" 
                                                                                                    meta:resourcekey="UltraGridColumnResource25">
                                                                                                    <Header Caption="Years">
                                                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                                                    </Header>
                                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                                <igtbl:UltraGridColumn BaseColumnName="Position_ID" Key="Position_ID" EditorControlID=""
                                                                                                    Type="DropDownList" Width="13%" 
                                                                                                    meta:resourcekey="UltraGridColumnResource26">
                                                                                                    <Header Caption="Position">
                                                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                                                    </Header>
                                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                    <Footer>
                                                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                                                    </Footer>
                                                                                                </igtbl:UltraGridColumn>
                                                                                            </Columns>
                                                                                            <RowTemplateStyle BackColor="Transparent" BorderColor="Transparent" BorderStyle="Ridge">
                                                                                                <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                                                            </RowTemplateStyle>
                                                                                        </igtbl:UltraGridBand>
                                                                                    </Bands>
                                                                                </igtbl:UltraWebGrid>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </igtab:Tab>
                                                        </Tabs>
                                                    </igtab:UltraWebTab>
                                                </igmisc:WebAsyncRefreshPanel>
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
