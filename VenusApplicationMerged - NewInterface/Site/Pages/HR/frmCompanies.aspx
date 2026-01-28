<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCompanies.aspx.vb" Inherits="frmCompanies"
    Culture="auto" meta:resourcekey="PageResource2" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Companies</title>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js" type="text/javascript"></script>
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
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmCompanies" runat="server">
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
                                    meta:resourcekey="Image_LogoResource2" />
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
                                                            meta:resourcekey="lblRegDateResource2"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegDateValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegDateValueResource2"></asp:Label>
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
                                                            meta:resourcekey="lblRegUserResource2"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegUserValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegUserValueResource2"></asp:Label>
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
                                                            meta:resourcekey="lblCancelDateResource2"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblCancelDateValue" runat="server" SkinID="Label_CopyRightsNormal"
                                                            meta:resourcekey="lblCancelDateValueResource2"></asp:Label>
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
                        meta:resourcekey="UltraWebTab1Resource2">
                        <Tabs>
                            <igtab:Tab Text="عام" meta:resourcekey="TabResource2">
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
                                                                meta:resourcekey="lblCodeResource2"></asp:Label>
                                                            <asp:Label ID="Label_Star1" runat="server" Text="*" Style="color: #FF0000" meta:resourcekey="Label_Star1Resource2"></asp:Label>
                                                        </td>
                                                        <td class="DataAreawithsearch">
                                                            <asp:TextBox ID="txtCode" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                AutoPostBack="True" meta:resourcekey="txtCodeResource2"></asp:TextBox>
                                                        </td>
                                                        <td class="search">
                                                            <igtxt:WebImageButton ID="btnSearchCode" runat="server" AutoSubmit="False" Height="18px"
                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnSearchCodeResource2">
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
                                                                meta:resourcekey="lblEngNameResource2"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalltr" MaxLength="255"
                                                                meta:resourcekey="txtEngNameResource2"></asp:TextBox>
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
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                    border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label_Title1" runat="server" meta:resourcekey="Label_Title1Resource1"
                                                                SkinID="Label_DefaultBold" Text="ترتيب الاسم"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
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
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lbEmployeeNameOrder" runat="server" meta:resourcekey="lbEmployeeNameOrderResource1"
                                                                SkinID="Label_DefaultNormal" Text="اسم الأول"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlFirst" runat="server" meta:resourcekey="ddlFirstResource1"
                                                                SkinID="DropDownList_LargNormal" Width="100%">
                                                                <asp:ListItem meta:resourceKey="FRResource1" Selected="True" Text="اسم الأول" Value="FR"></asp:ListItem>
                                                                <asp:ListItem meta:resourceKey="FAResource1" Text="اسم الأب" Value="FA"></asp:ListItem>
                                                                <asp:ListItem meta:resourceKey="GRResource1" Text="اسم الجد" Value="GR"></asp:ListItem>
                                                                <asp:ListItem meta:resourceKey="FMResource1" Text="اسم العائلة" Value="FM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lbEmployeeNameOrder1" runat="server" meta:resourcekey="lbEmployeeNameOrder1Resource1"
                                                                SkinID="Label_DefaultNormal" Text="اسم الجد"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlThird" runat="server" meta:resourcekey="ddlThirdResource1"
                                                                SkinID="DropDownList_LargNormal" Width="100%">
                                                                <asp:ListItem meta:resourceKey="FRResource1" Text="اسم الأول" Value="FR"></asp:ListItem>
                                                                <asp:ListItem meta:resourceKey="FAResource1" Text="اسم الأب" Value="FA"></asp:ListItem>
                                                                <asp:ListItem meta:resourceKey="GRResource1" Selected="True" Text="اسم الجد" Value="GR"></asp:ListItem>
                                                                <asp:ListItem meta:resourceKey="FMResource1" Text="اسم العائلة" Value="FM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lbEmployeeNameOrder0" runat="server" meta:resourcekey="lbEmployeeNameOrder0Resource1"
                                                                SkinID="Label_DefaultNormal" Text="اسم الأب"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlSecond" runat="server" meta:resourcekey="ddlSecondResource1"
                                                                SkinID="DropDownList_LargNormal" Width="100%">
                                                                <asp:ListItem meta:resourceKey="FRResource1" Text="اسم الأول" Value="FR"></asp:ListItem>
                                                                <asp:ListItem meta:resourceKey="FAResource1" Selected="True" Text="اسم الأب" Value="FA"></asp:ListItem>
                                                                <asp:ListItem meta:resourceKey="GRResource1" Text="اسم الجد" Value="GR"></asp:ListItem>
                                                                <asp:ListItem meta:resourceKey="FMResource1" Text="اسم العائلة" Value="FM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lbEmployeeNameOrder2" runat="server" meta:resourcekey="lbEmployeeNameOrder2Resource1"
                                                                SkinID="Label_DefaultNormal" Text="اسم العائلة"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlFourth" runat="server" meta:resourcekey="ddlFourthResource1"
                                                                SkinID="DropDownList_LargNormal" Width="100%">
                                                                <asp:ListItem meta:resourceKey="FRResource1" Text="اسم الأول" Value="FR"></asp:ListItem>
                                                                <asp:ListItem meta:resourceKey="FAResource1" Text="اسم الأب" Value="FA"></asp:ListItem>
                                                                <asp:ListItem meta:resourceKey="GRResource1" Text="اسم الجد" Value="GR"></asp:ListItem>
                                                                <asp:ListItem meta:resourceKey="FMResource1" Selected="True" Text="اسم العائلة" Value="FM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
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
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
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
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblSeparator" runat="server" meta:resourcekey="lblSeparatorResource1"
                                                                SkinID="Label_DefaultNormal" Text="الفاصلة بين الاسم"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtSeparator" runat="server" MaxLength="255" meta:resourcekey="txtSeparatorResource1"
                                                                SkinID="TextBox_LargeNormalrtl"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
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
                                                            <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_Title1Resource2" SkinID="Label_DefaultBold"
                                                                Text="الإعدادات"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
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
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblIsHegri" runat="server" meta:resourcekey="lblIsHegriResource1"
                                                                SkinID="Label_DefaultNormal" Text="التاريخ الهجري"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="chkIsHegri" runat="server" meta:resourcekey="chkIsHegriResource1" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lbEmployeeDefAttend" runat="server" meta:resourcekey="lbEmployeeDefAttendResource1"
                                                                SkinID="Label_DefaultNormal" Text="الإفتراضى غياب"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="chkDefaultAttend" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblAbsents" runat="server" meta:resourcekey="lblAbsentsResource1"
                                                                SkinID="Label_DefaultNormal" Text="لا تتبع طريقة إحتساب الراتب للاجازة"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="chkAbsents" runat="server" meta:resourcekey="chkAbsentsResource1" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label2" runat="server" meta:resourcekey="CountEmployeeVacationDaysResource"
                                                                SkinID="Label_DefaultNormal" Text="أيام الاجازة السنوية المستحقة متصلة"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="CountEmployeeVacationCheckBox" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblHasSequence" runat="server" meta:resourcekey="lblHasSequenceResource1"
                                                                SkinID="Label_DefaultNormal" Text="الترقيم التلقائي"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="chkHasSequence" runat="server" meta:resourcekey="chkHasSequenceResource1" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblPrepareDay" runat="server" meta:resourcekey="lblPrepareDayResource1"
                                                                SkinID="Label_DefaultNormal" Text="يوم التجهيز"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtPrepareDay" runat="server" DataMode="Int" MinValue="0"
                                                                NullText="0" SkinID="WebNumericEdit_Default" ValueText="0">
                                                            </igtxt:WebNumericEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
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
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblSequenceLength" runat="server" meta:resourcekey="lblSequenceLengthResource1"
                                                                SkinID="Label_DefaultNormal" Text="طول الترقيم"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtSequenceLength" runat="server" MaxLength="255" meta:resourcekey="txtSequenceLengthResource1"
                                                                SkinID="TextBox_LargeNormalrtl"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblPrefix" runat="server" meta:resourcekey="lblPrefixResource1" SkinID="Label_DefaultNormal"
                                                                Text="الترقيم حسب"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlprefix" runat="server" meta:resourcekey="ddlOvertimeResource1"
                                                                SkinID="DropDownList_LargNormal" Width="100%">
                                                                <asp:ListItem meta:resourcekey="ListItemResource5" Selected="True" Text="بدون محدد"
                                                                    Value="0"></asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="ListItemResource6" Text="الفرع" Value="1"></asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="ListItemResource7" Text="الإدارة" Value="2"></asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="ListItemResource8" Text="الوظيفة" Value="3"></asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="ListItemResource9" Text="نوع التعاقد" Value="4"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea" style="height: 16px">
                                                        </td>
                                                        <td class="LabelArea" style="height: 16px">
                                                            <asp:Label ID="Label_Separator" runat="server" meta:resourcekey="Label_SeparatorResource1"
                                                                SkinID="Label_DefaultNormal" Text="الفاصلة بين الترقيم"></asp:Label>
                                                        </td>
                                                        <td class="DataArea" style="height: 16px">
                                                            <asp:TextBox ID="TextBox_Separator" runat="server" MaxLength="1" SkinID="TextBox_LargeNormalrtl"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top; display:none">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="LabelZeroBalAfterVac" runat="server" meta:resourcekey="lblZeroBalAfterVacResource"
                                                                SkinID="Label_DefaultNormal" Text="تصفير رصيد الاجازة بعد العودة"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="ZeroBalAfterVacCheckBox" runat="server" meta:resourcekey="chkZeroBalAfterVacResource" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
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
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblOvertime" runat="server" meta:resourcekey="lblOvertimeResource1"
                                                                SkinID="Label_DefaultNormal" Text="أجر اليوم"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlSalary" runat="server" meta:resourcekey="ddlOvertimeResource1"
                                                                SkinID="DropDownList_LargNormal" Width="100%">
                                                                <asp:ListItem meta:resourcekey="ListItemResource1" Text="على راتب الأساسي" Value="0"></asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="ListItemResource2" Text="على جميع البدلات" Value="1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="VacSettlementLabel" runat="server" meta:resourcekey="lblVacSettlementResource"
                                                                SkinID="Label_DefaultNormal" Text="ربط شاشة المستحقات بالاجازات"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="VacSettlementCheckBox" runat="server" meta:resourcekey="chkVacSettlementResource" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
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
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblExecuseRequestHoursallowed" runat="server" meta:resourcekey="lblExecuseRequestHoursallowedsource1"
                                                                SkinID="Label_DefaultNormal" Text="ExecuseRequestHoursallowed"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtExecuseRequestHoursallowed" runat="server" MaxLength="2" SkinID="txtExecuseRequestHoursallowedResource"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="1" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="VacationFromPrepareDay" runat="server" meta:resourcekey="lblVacationFromPrepare"
                                                                SkinID="Label_DefaultNormal" Text="حساب الاجازات من يوم التجهيز"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
    <asp:CheckBox ID="ChkVacationFromPrepareDay" runat="server" meta:resourcekey="chkVacationFromPrepareDayResource" />
</td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="AllowOverVacationLabel" runat="server" meta:resourcekey="lblAllowOverVacation"
                                                                SkinID="Label_DefaultNormal" Text="هل يسمح بتجاوز الرصيد"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="AllowOverVacationCheckBox" runat="server" meta:resourcekey="chkVacSettlementResource" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
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
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblEmployeeDocumentsAutoSerial" runat="server" meta:resourcekey="lblEmployeeDocumentsAutoSerialResource1"
                                                                SkinID="Label_DefaultNormal" Text="Employee Documents Auto Serial"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="ChKEmployeeDocumentsAutoSerial" runat="server" meta:resourcekey="ChKEmployeeDocumentsAutoSerialResource" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                             <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblUserDepartmentsPermissions" runat="server" meta:resourcekey="lblUserDepartmentsPermissionsResource"
                                                                SkinID="Label_DefaultNormal" Text="UserDepartmentsPermissions"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="chkUserDepartmentsPermissions" runat="server" meta:resourcekey="chkUserDepartmentsPermissionsResource" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
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
