<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRecOpenVacancy.aspx.vb" Inherits="Pages_HR_frmRecOpenVacancy" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
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
    <form id="frmRecOpenVacancy" runat="server">
        <div style="display: none">
            <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
            <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
                TabIndex="-1" Width="99px"></asp:Label>
            <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
                Width="91px"></asp:TextBox>
            <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
        </div>
        <div class="Div_MasterContainer" runat="server" id="DIV">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                        <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver;">
                            <tr>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                        SkinID="HrSave_Command" CommandArgument="Save" meta:resourcekey="ImageButton_SaveResource1" />
                                </td>
                                <td style="width: 120px">
                                    <asp:ImageButton ID="ImageButton_SaveN" Width="16px" Height="16px" runat="server"
                                        CommandArgument="SaveNew" SkinID="HrSaveN_Command" meta:resourcekey="ImageButton_SaveNResource1" />
                                    <asp:LinkButton ID="LinkButton_SaveN" runat="server" Text="حفظ مع جديد"
                                        CommandArgument="SaveNew" meta:resourcekey="LinkButton_SaveNResource1"></asp:LinkButton>
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
                                        SkinID="HrProperties_Command"
                                        CommandArgument="Property" meta:resourcekey="ImageButton_PropertiesResource1" />
                                    <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="خصائص"
                                        CommandArgument="Property" meta:resourcekey="LinkButton_PropertiesResource1"></asp:LinkButton>
                                </td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                        SkinID="HrRemarks_Command" CommandArgument="Remarks" meta:resourcekey="ImageButton_RemarksResource1" />
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
                        <table style="width: 100%; height: 42px; vertical-align: top;">
                            <tr>
                                <td style="width: 32px; vertical-align: top">
                                    <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle"
                                        ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png" meta:resourcekey="Image_LogoResource1" />
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
                                                            <asp:Label ID="lblRegDate" runat="server" Text="سجل فى"
                                                                SkinID="Label_CopyRightsBold" meta:resourcekey="lblRegDateResource1"></asp:Label>
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
                                                            <asp:Label ID="lblRegUser" runat="server" Text="سجل بواسطة"
                                                                SkinID="Label_CopyRightsBold" meta:resourcekey="lblRegUserResource1"></asp:Label>
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
                                                            <asp:Label ID="lblCancelDate" runat="server" Text="تاريخ الالغاء"
                                                                SkinID="Label_CopyRightsBold" meta:resourcekey="lblCancelDateResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                            <asp:Label ID="lblCancelDateValue" runat="server"
                                                                SkinID="Label_CopyRightsNormal" meta:resourcekey="lblCancelDateValueResource1"></asp:Label>
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
                        <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True"
                            SkinID="Default" meta:resourcekey="UltraWebTab1Resource1">
                            <Tabs>
                                <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                    <ContentTemplate>
                                        <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top;"
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
                                                                <asp:Label ID="lblCode" runat="server" Text="الكود"
                                                                    SkinID="Label_DefaultNormal" meta:resourcekey="lblCodeResource1" Width="80px"></asp:Label>
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
                                                                <asp:Label Width="86px" ID="lblEngName" runat="server" Text="التوصيف إنجليزى"
                                                                    SkinID="Label_DefaultNormal" meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalltr"
                                                                    MaxLength="255" meta:resourcekey="txtEngNameResource1"></asp:TextBox>
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
                                                                <asp:Label Width="86px" ID="lblArbName" runat="server" Text="التوصيف عربى"
                                                                    SkinID="Label_DefaultNormal" meta:resourcekey="lblArbNameResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalrtl"
                                                                    MaxLength="255" meta:resourcekey="txtArbNameResource1"></asp:TextBox>
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
                                                                <asp:Label Width="86px" ID="lblDepartment" runat="server"
                                                                    SkinID="Label_DefaultNormal" Text="مركز الوظيفة" meta:resourcekey="lblDepartmentResource2"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlPosition" runat="server"
                                                                    SkinID="DropDownList_LargNormal" meta:resourcekey="ddlPositionResource2">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label Width="86px" ID="lblRefCode" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="درجة التعليم" meta:resourcekey="lblRefCodeResource2"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlEDegree" runat="server"
                                                                    SkinID="DropDownList_LargNormal" meta:resourcekey="ddlEDegreeResource2">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; vertical-align: top;">
                                                    <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label Width="86px" ID="lblBranch" runat="server"
                                                                    SkinID="Label_DefaultNormal"
                                                                    Text="المكان" meta:resourcekey="lblBranchResource2"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlLocations" runat="server"
                                                                    SkinID="DropDownList_LargNormal" meta:resourcekey="ddlLocationsResource2">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; vertical-align: top;"></td>
                                                <td style="width: 47%; vertical-align: top;">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label Width="86px" ID="lblPositionQTY" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="المستوى" meta:resourcekey="lblPositionQTYResource2"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlGrade" runat="server"
                                                                    SkinID="DropDownList_LargNormal" meta:resourcekey="ddlGradeResource2">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; vertical-align: top;">
                                                    <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label Width="86px" ID="lblBranch0" runat="server"
                                                                    SkinID="Label_DefaultNormal"
                                                                    Text="نوع العقد " meta:resourcekey="lblBranch0Resource2"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlConTypes" runat="server"
                                                                    SkinID="DropDownList_LargNormal" meta:resourcekey="ddlConTypesResource2">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; vertical-align: top;">&nbsp;</td>
                                                <td style="width: 47%; vertical-align: top;">
                                                    <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label Width="86px" ID="lblStatus0" runat="server"
                                                                    SkinID="Label_DefaultNormal" Text="درجة المتسوى" meta:resourcekey="lblStatus0Resource2"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlGeadeStep" runat="server"
                                                                    SkinID="DropDownList_LargNormal" meta:resourcekey="ddlGeadeStepResource2">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; vertical-align: top;">
                                                    <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label Width="86px" ID="lblPosition" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="عدد سنوات الخبرة" meta:resourcekey="lblPositionResource2"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="txtExperionces" runat="server" MaxLength="10" MaxValue="9999"
                                                                    MinValue="0" NullText="0" SkinID="WebNumericEdit_Default"
                                                                    meta:resourcekey="txtExperioncesResource2">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; vertical-align: top;">&nbsp;</td>
                                                <td style="width: 47%; vertical-align: top;">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; vertical-align: top;">
                                                    <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label Width="86px" ID="lblPosition0" runat="server"
                                                                    SkinID="Label_DefaultNormal"
                                                                    Text="حالة الوظيفة" meta:resourcekey="lblPosition0Resource2"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:CheckBox ID="VStatus" runat="server" meta:resourcekey="VStatusResource2" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; vertical-align: top;">&nbsp;</td>
                                                <td style="width: 47%; vertical-align: top;">
                                                    <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label Width="86px" ID="lblPosition1" runat="server"
                                                                    SkinID="Label_DefaultNormal"
                                                                    Text="خارجية" meta:resourcekey="lblPosition1Resource2"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:CheckBox ID="IsExternal" runat="server"
                                                                    TabIndex="5" meta:resourcekey="IsExternalResource2" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="style1" style="vertical-align: top;">
                                                    <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label Width="86px" ID="lblPosition2" runat="server"
                                                                    SkinID="Label_DefaultNormal"
                                                                    Text="المهارات المطلوبة" meta:resourcekey="lblPosition2Resource2"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtskills" runat="server" Height="40px" MaxLength="255"
                                                                    BorderColor="#CCCCCC" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: black; vertical-align: middle; text-align: center" BorderStyle="Solid" BorderWidth="1px" Width="100%"
                                                                    TextMode="MultiLine" meta:resourcekey="txtskillsResource2"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="style2" style="vertical-align: top;">&nbsp;</td>
                                                <td class="style1" style="vertical-align: top;">&nbsp;</td>
                                            </tr>

                                            <tr>
                                                <td style="width: 47%; vertical-align: top;">
                                                    <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label Width="86px" ID="lblGStartDate" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="تاريخ البداية" meta:resourcekey="lblGStartDateResource2"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebDateTimeEdit ID="txtGStartDate" runat="server" AutoPostBack="True"
                                                                    Height="18px" SkinID="WebDateTimeEdit_Fix"
                                                                    meta:resourcekey="txtGStartDateResource2">
                                                                </igtxt:WebDateTimeEdit>
                                                                &nbsp;<asp:Label ID="lblG0" runat="server"
                                                                    SkinID="Label_CopyRightsBold" Text="(ميلادي)"
                                                                    meta:resourcekey="lblG0Resource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; vertical-align: top;">&nbsp;</td>
                                                <td style="width: 47%; vertical-align: top;">
                                                    <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label Width="86px" ID="lblHStartDate" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="تاريخ البداية" meta:resourcekey="lblHStartDateResource2"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebDateTimeEdit ID="txtHStartDate" runat="server" AutoPostBack="True"
                                                                    Height="18px" SkinID="WebDateTimeEdit_Fix"
                                                                    meta:resourcekey="txtHStartDateResource2">
                                                                </igtxt:WebDateTimeEdit>
                                                                &nbsp;<asp:Label ID="lblH0" runat="server" SkinID="Label_CopyRightsBold"
                                                                    Text="(هجري)" meta:resourcekey="lblH0Resource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; vertical-align: top;">
                                                    <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label Width="86px" ID="lblGStartDate0" runat="server"
                                                                    SkinID="Label_DefaultNormal"
                                                                    Text="تاريخ النهاية" meta:resourcekey="lblGStartDate0Resource2"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebDateTimeEdit ID="txtGEndDate" runat="server" AutoPostBack="True"
                                                                    Height="18px" SkinID="WebDateTimeEdit_Fix" meta:resourcekey="txtGEndDateResource2">
                                                                </igtxt:WebDateTimeEdit>
                                                                &nbsp;<asp:Label ID="lblG1" runat="server" SkinID="Label_CopyRightsBold"
                                                                    Text="(ميلادي)" meta:resourcekey="lblG1Resource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; vertical-align: top;">&nbsp;</td>
                                                <td style="width: 47%; vertical-align: top;">
                                                    <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label Width="86px" ID="lblHStartDate0" runat="server"
                                                                    SkinID="Label_DefaultNormal"
                                                                    Text="تاريخ النهاية" meta:resourcekey="lblHStartDate0Resource2"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebDateTimeEdit ID="txtHEndDate" runat="server" AutoPostBack="True"
                                                                    Height="18px" SkinID="WebDateTimeEdit_Fix"
                                                                    meta:resourcekey="txtHEndDateResource2">
                                                                </igtxt:WebDateTimeEdit>
                                                                &nbsp;<asp:Label ID="lblH1" runat="server" SkinID="Label_CopyRightsBold"
                                                                    Text="(هجري)" meta:resourcekey="lblH1Resource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 100%" colspan="3"></td>
                                                        </tr>
                                                    </table>
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
