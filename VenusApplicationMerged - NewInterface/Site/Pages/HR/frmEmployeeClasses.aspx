<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeeClasses.aspx.vb"
    Inherits="frmEmployeeClasses" Culture="auto" meta:resourcekey="PageResource1"
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
    <title>* Venus Payroll * ~Employee Classes</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_frmEmployeeClasses.js" type="text/javascript"></script>
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

        function Open_Formula_Screen_NEW(controlId) {
            var webTab = igtab_getTabById("UltraWebTab1");
            if (webTab == null)
                return;
            control = webTab.findControl(controlId);
            var queryString = "?ControlName=" + control.name + "&ControlValue=" + control.value + "&ControlType=T";
            window.open("frmFormulaDesigner.aspx" + queryString, "_blank", "height=320px,width=787px,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");

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
    <form id="frmEmployeeClasses" runat="server">
        <div style="display: none">
            <asp:HiddenField ID="hdnLang" runat="server" Value="0" />
            <asp:HiddenField ID="hdnVacationTypeID" runat="server" Value="0" />
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
                                            <td style="width: 2%; vertical-align: top"></td>
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
                        <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                            meta:resourcekey="UltraWebTab1Resource1">
                            <Tabs>
                                <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                    <ContentTemplate>
                                        <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                            cellspacing="0">
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td></td>
                                                            <td class="DataArea"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 1%;"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <table style="width: 100px;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblCode" runat="server" Text="Code" SkinID="Label_DefaultNormal" meta:resourcekey="lblCodeResource1"></asp:Label>
                                                                            <asp:Label ID="Label_Star1" runat="server" Text="*" Style="color: #FF0000" meta:resourcekey="Label_Star1Resource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                                                           
                                                            <td>
                                                                <asp:Label ID="HasFingerPrintLabel" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Has Finger Print" meta:resourcekey="HasFingerPrintLabelResource" Width="50px"></asp:Label>
                                                                <asp:CheckBox ID="HasFingerPrintCheckBox" runat="server" />
                                                            </td>
                                                              
                                                              
                                                            <td>
                                                                <asp:Label ID="AttendanceFromTimeSheetLabel" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Attendance From Time Sheet" meta:resourcekey="AttendanceFromTimeSheetLabelResource" Width="100px"></asp:Label>
                                                                <asp:CheckBox ID="AttendanceFromTimeSheetCheckBox" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="hasOvertimeList" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Has Overtime List" meta:resourcekey="hasOvertimeListLabelResource" Width="100px"></asp:Label>
                                                                <asp:CheckBox ID="hasOvertimeListCheckBox" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
 <asp:Label ID="HasFlexableFingerPrint" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Has Flexable Finger Print" meta:resourcekey="HasFlexableFingerPrintLabelResource" Width="50px"></asp:Label>
                                                                <asp:CheckBox ID="HasFlexableFingerPrintCheckBox" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblEngName" runat="server" Text="English Name" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblEngNameResource1" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalltr" MaxLength="255"
                                                                    meta:resourcekey="txtEngNameResource1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td></td>
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
                                                            <td>
                                                                <asp:Label ID="lblArbName" runat="server" Text="Arabic Name" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblArbNameResource1" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                                                                    meta:resourcekey="txtArbNameResource1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td></td>
                                                            <td class="DataArea"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 16px; vertical-align: top" colspan="3">
                                                    <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Label ID="Label1" runat="server" SkinID="Label_DefaultBold" Text="Attendance Settings"
                                                                    meta:resourcekey="Label1Resource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblHasAttendance" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="lblHasAttendanceRec"
                                                                    Text="Has Attendance" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="DdlHasAttendance" runat="server" SkinID="DropDownList_LargNormal">
                                                                    <asp:ListItem meta:resourcekey="ListItemResourceYes" Text="Yes" Value="True"></asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="ListItemResourceNo" Selected="True" Text="No" Value="False"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblPolicyCheckMachine" runat="server" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblPolicyCheckMachineRec" Text="Check Machine" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="DdlPolicyCheckMachine" runat="server" SkinID="DropDownList_LargNormal">
                                                                    <asp:ListItem meta:resourcekey="ListItemResourceYes" Text="Yes" Value="True"></asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="ListItemResourceNo" Selected="True" Text="No" Value="False"></asp:ListItem>
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
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblWorkhoursperday" runat="server" meta:resourcekey="lblWorkhoursperdayResource1"
                                                                    SkinID="Label_DefaultNormal" Text="Work Per Day" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="txtWorkhoursperday" runat="server" meta:resourcekey="txtWorkhoursperdayResource1"
                                                                    MinValue="0" NullText="0" SkinID="WebNumericEdit_Default" ValueText="0">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label11" runat="server" meta:resourcekey="lblHourResource1" SkinID="Label_CopyRightsNormal"
                                                                    Text="(Hour)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblNoofdaysperperiod" runat="server" meta:resourcekey="lblNoofdaysperperiodResource1"
                                                                    SkinID="Label_DefaultNormal" Text="No of Days Period" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="txtNoofdaysperperiod" runat="server" meta:resourcekey="txtNoofdaysperperiodResource1"
                                                                    MinValue="0" NullText="0" SkinID="WebNumericEdit_Default" ValueText="0">
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
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblNoofhoursperweek" runat="server" meta:resourcekey="lblNoofhoursperweekResource1"
                                                                    SkinID="Label_DefaultNormal" Text="Work Per Week" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="txtNoofhoursperweek" runat="server" MinValue="0" NullText="0"
                                                                    SkinID="WebNumericEdit_Default" ValueText="0" meta:resourcekey="txtNoofhoursperweekResource1">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" meta:resourcekey="lblHourResource1" SkinID="Label_CopyRightsNormal"
                                                                    Text="(Hour)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblFirstdayofweek" runat="server" meta:resourcekey="lblFirstdayofweekResource1"
                                                                    SkinID="Label_DefaultNormal" Text="First Day of Week" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="DdlFirstdayofweek" runat="server" meta:resourcekey="DdlFirstdayofweekResource1"
                                                                    SkinID="DropDownList_LargNormal">
                                                                    <asp:ListItem meta:resourcekey="ListItemResource1" Text="Saturday" Value="1"></asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="ListItemResource2" Text="Sunday" Value="2"></asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="ListItemResource3" Text="Monday" Value="3"></asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="ListItemResource4" Text="Tuesday" Value="4"></asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="ListItemResource5" Text="Wednesday" Value="5"></asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="ListItemResource6" Text="Thursday" Value="6"></asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="ListItemResource7" Text="Friday" Value="7"></asp:ListItem>
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
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblNoofhoursperperiod" runat="server" meta:resourcekey="lblNoofhoursperperiodResource1"
                                                                    SkinID="Label_DefaultNormal" Text="Work Per Period" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="txtNoofhoursperperiod" runat="server" meta:resourcekey="txtNoofhoursperperiodResource1"
                                                                    MinValue="0" NullText="0" SkinID="WebNumericEdit_Default" ValueText="0">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label10" runat="server" meta:resourcekey="lblHourResource1" SkinID="Label_CopyRightsNormal"
                                                                    Text="(Hour)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblProjects" runat="server" meta:resourcekey="lblProjectsResource1"
                                                                    SkinID="Label_DefaultNormal" Text="Default Project" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="DdlProjects" runat="server" meta:resourcekey="DdlProjectsResource1"
                                                                    SkinID="DropDownList_LargNormal">
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
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblStartTime" runat="server" meta:resourcekey="lblStartTimeResource1"
                                                                    SkinID="Label_DefaultNormal" Text="Default Start Time" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebDateTimeEdit ID="txtStartTime" runat="server" EditModeFormat="hh:mm tt"
                                                                    meta:resourcekey="txtStartTimeResource1" SkinID="WebDateTimeEdit_Default">
                                                                </igtxt:WebDateTimeEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblDefultendtime" runat="server" meta:resourcekey="lblDefultendtimeResource1"
                                                                    SkinID="Label_DefaultNormal" Text="Default End Time" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebDateTimeEdit ID="txtEndTime" runat="server" EditModeFormat="hh:mm tt" meta:resourcekey="txtEndTimeResource1"
                                                                    SkinID="WebDateTimeEdit_Default">
                                                                </igtxt:WebDateTimeEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <%--<td style="width: 47%; height: 16px; vertical-align: top" >
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>--%>
                                                <%-- <asp:Label ID="lblWorkingUnitsIsHours" runat="server" meta:resourcekey="lblWorkingUnitsIsHoursResource1"
                                                                    SkinID="Label_DefaultNormal" Text="Work Units Is Hours" Width="100px" Visible="False"></asp:Label>
                                                --%> <%-- </td>
                                                            <td class="DataArea">--%>
                                                <asp:DropDownList ID="DdlWorkingUnitsIsHours" runat="server" meta:resourcekey="DdlWorkingUnitsIsHoursResource1"
                                                    SkinID="DropDownList_LargNormal" Visible="False">
                                                    <asp:ListItem meta:resourcekey="ListItemResourceYes" Text="Yes" Value="True"></asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResourceNo" Selected="True" Text="No" Value="False"></asp:ListItem>
                                                </asp:DropDownList>
                                                <%--</td>
                                                        </tr>
                                                    </table>
                                                </td>--%>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="VacCostFormulaLabel" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="VacCostFormulaLabelResource"
                                                                    Text="Vacation Cost Formula" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="VacCostFormulaTextBox" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalC"
                                                                                meta:resourcekey="txtFormulaResource"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 24px;">
                                                                            <igtxt:WebImageButton ID="VacCostButton" runat="server" AutoSubmit="False" Height="18px"
                                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnFormulaResource1">
                                                                                <Alignments TextImage="ImageBottom" />
                                                                                <Appearance>
                                                                                    <Image Url="./Img/rtg_rate.gif" />
                                                                                </Appearance>
                                                                            </igtxt:WebImageButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblFormula" runat="server" meta:resourcekey="lblFormulaResource1"
                                                                    SkinID="Label_DefaultNormal" Text="EOS Formula" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtFormula" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalC"
                                                                                meta:resourcekey="txtFormulaResource1"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 24px;">
                                                                            <igtxt:WebImageButton ID="btnFormula" runat="server" AutoSubmit="False" Height="18px"
                                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnFormulaResource1">
                                                                                <Alignments TextImage="ImageBottom" />
                                                                                <Appearance>
                                                                                    <Image Url="./Img/rtg_rate.gif" />
                                                                                </Appearance>
                                                                            </igtxt:WebImageButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblOvertimefactor" runat="server" meta:resourcekey="lblOvertimefactorResource1"
                                                                    SkinID="Label_DefaultNormal" Text="Overtime Factor" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtOvertimefactor" runat="server" MaxLength="255" meta:resourcekey="txtOvertimefactorResource1"
                                                                    SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblOvertimeFormula" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="lblOvertimeFormulaRec"
                                                                    Text="Overtime Formula" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtOvertimeFormula" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalC"
                                                                                meta:resourcekey="txtFormulaResource1"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 24px;">
                                                                            <igtxt:WebImageButton ID="btnOvertimeFormula" runat="server" AutoSubmit="False" Height="18px"
                                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnFormulaResource1">
                                                                                <Alignments TextImage="ImageBottom" />
                                                                                <Appearance>
                                                                                    <Image Url="./Img/rtg_rate.gif" />
                                                                                </Appearance>
                                                                            </igtxt:WebImageButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblHolidayfactor" runat="server" meta:resourcekey="lblHolidayfactorResource1"
                                                                    SkinID="Label_DefaultNormal" Text="Holiday Factor" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtHolidayfactor" runat="server" MaxLength="255" meta:resourcekey="txtHolidayfactorResource1"
                                                                    SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblHolidayFormula" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="lblHolidayFormulaRec"
                                                                    Text="Holiday Formula" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtHolidayFormula" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalC"
                                                                                meta:resourcekey="txtFormulaResource1"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 24px;">
                                                                            <igtxt:WebImageButton ID="btnHolidayFormula" runat="server" AutoSubmit="False" Height="18px"
                                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnFormulaResource1">
                                                                                <Alignments TextImage="ImageBottom" />
                                                                                <Appearance>
                                                                                    <Image Url="./Img/rtg_rate.gif" />
                                                                                </Appearance>
                                                                            </igtxt:WebImageButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="LateFormulaLabel" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="LateFormulaLabelResource"
                                                                    Text="Late Formula" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="LateFormulaTextBox" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalC"
                                                                                meta:resourcekey="txtFormulaResource1"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 24px;">
                                                                            <igtxt:WebImageButton ID="LateButton" runat="server" AutoSubmit="False" Height="18px"
                                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnFormulaResource1">
                                                                                <Alignments TextImage="ImageBottom" />
                                                                                <Appearance>
                                                                                    <Image Url="./Img/rtg_rate.gif" />
                                                                                </Appearance>
                                                                            </igtxt:WebImageButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="AbsentFormulaLabel" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="AbsentFormulaLabelResource"
                                                                    Text="Absent Formula" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="AbsentFormulaTextBox" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalC"
                                                                                meta:resourcekey="txtFormulaResource1"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 24px;">
                                                                            <igtxt:WebImageButton ID="AbsentButton" runat="server" AutoSubmit="False" Height="18px"
                                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnFormulaResource1">
                                                                                <Alignments TextImage="ImageBottom" />
                                                                                <Appearance>
                                                                                    <Image Url="./Img/rtg_rate.gif" />
                                                                                </Appearance>
                                                                            </igtxt:WebImageButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="Labelottransaction" runat="server" meta:resourcekey="LabelottransactionResource1"
                                                                    SkinID="Label_DefaultNormal" Text="Holiday Factor" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="DropDownList6" runat="server" SkinID="DropDownList_LargNormal">
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
                                                            <td>
                                                                <asp:Label ID="Labelhottransaction" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="Labelhottransactionresorce"
                                                                    Text="Holiday Formula" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="DropDownList7" runat="server" SkinID="DropDownList_LargNormal">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 16px; vertical-align: top" colspan="3">
                                                    <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Label ID="Label2" runat="server" SkinID="Label_DefaultBold" Text="Late Settings"
                                                                    meta:resourcekey="Label2Resource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblDDelayingfactor" runat="server" SkinID="Label_DefaultNormal" Text="Permit Daily Delay"
                                                                    Width="100px" meta:resourcekey="lblDDelayingfactorResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="txtDDelayingfactor" runat="server" MinValue="0" NullText="0"
                                                                    SkinID="WebNumericEdit_Default" ValueText="0" meta:resourcekey="txtDDelayingfactorResource1">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblMinute0" runat="server" meta:resourcekey="lblMinuteResource1" SkinID="Label_CopyRightsNormal"
                                                                    Text="(Mint)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 47%; vertical-align: top" rowspan="4">
                                                    <asp:Label ID="Label13" runat="server" SkinID="Label_DefaultBold" Text=" Delaying Punishment Slices"
                                                        meta:resourcekey="Label13Resource1"></asp:Label>
                                                    <igtbl:UltraWebGrid   Browser="UpLevel"  ID="uwgDelayingSlice" runat="server" EnableAppStyling="False"
                                                        Height="100%" SkinID="Default" Width="100%" meta:resourcekey="uwgDelayingSliceResource1">
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                            BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                            RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                            StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                            Version="4.00" ViewType="OutlookGroupBy">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="80px"
                                                                Width="100%">
                                                            </FrameStyle>
                                                            <ClientSideEvents AfterEnterEditModeHandler="uwg_AfterEnterEditModeHandler" />
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
                                                            <igtbl:UltraGridBand>
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
                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="0px" meta:resourcekey="UltraGridColumnResource1">
                                                                        <Header Caption="ID">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="FromMin" DataType="System.Int32" Key="FromMin"
                                                                        Width="25%" meta:resourcekey="UltraGridColumnResource2">
                                                                        <Header Caption="From Minutes" Title="Answer_Eng">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ToMin" DataType="System.Int32" Key="ToMin"
                                                                        Width="25%" meta:resourcekey="UltraGridColumnResource3">
                                                                        <Header Caption="To Minutes" Title="Answer_Arb">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="PunishPCT" DataType="System.Int32" Key="PunishPCT"
                                                                        Width="50%" meta:resourcekey="UltraGridColumnResource4">
                                                                        <Header Caption="Daily Salary Punishment PCT" Title="APower">
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
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblMDelayingfactor" runat="server" SkinID="Label_DefaultNormal" Text="Permit Monthly"
                                                                    Width="100px" meta:resourcekey="lblMDelayingfactorResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="txtMDelayingfactor" runat="server" MinValue="0" NullText="0"
                                                                    SkinID="WebNumericEdit_Default" ValueText="0" meta:resourcekey="txtMDelayingfactorResource1">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblMinute1" runat="server" meta:resourcekey="lblMinuteResource1" SkinID="Label_CopyRightsNormal"
                                                                    Text="(Mint)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblNonProfitOverTimeH" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Non Profit Overtime" Width="100px" meta:resourcekey="lblNonProfitOverTimeHResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="txtNonProfitOverTimeH" runat="server" MinValue="0" NullText="0"
                                                                    SkinID="WebNumericEdit_Default" ValueText="0" meta:resourcekey="txtNonProfitOverTimeHResource1">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label12" runat="server" meta:resourcekey="lblHourResource1" SkinID="Label_CopyRightsNormal"
                                                                    Text="(Hour)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblPermitDelayTrans" runat="server" SkinID="Label_DefaultNormal" Text="Permit Delay Trans"
                                                                    Width="100px" meta:resourcekey="lblPermitDelayTransResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="DropDownList1" runat="server" SkinID="DropDownList_LargNormal"
                                                                    meta:resourcekey="DropDownList1Resource1">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="Label_AbsentTransaction" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Absent deduction Transaction" Width="100px" meta:resourcekey="Label_AbsentTransactionResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="DropDownList4" runat="server" SkinID="DropDownList_LargNormal"
                                                                    meta:resourcekey="DropDownList1Resource1">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" SkinID="Label_DefaultNormal" Text="Punishment Calculation"
                                                                    Width="100%" meta:resourcekey="Label4Resource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="DropDownList2" runat="server" SkinID="DropDownList_LargNormal">
                                                                    <asp:ListItem meta:resourcekey="DropDownList2Resource1" Selected="True" Text="Hour Salary"
                                                                        Value="0"></asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="DropDownList2Resource2" Text="Day Salary" Value="1"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="Label_DeductionMethod" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Deduction Method" Width="100%" meta:resourcekey="Label_DeductionMethodResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="DropDownList5" runat="server" SkinID="DropDownList_LargNormal">
                                                                    <asp:ListItem meta:resourcekey="DropDownList5Resource1" Selected="True" Text="Overtaking"
                                                                        Value="0"></asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="DropDownList5Resource2" Text="Full Period" Value="1"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" SkinID="Label_DefaultNormal" Text="On No Exit"
                                                                    Width="100px" meta:resourcekey="Label5Resource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="DropDownList3" runat="server" SkinID="DropDownList_LargNormal">
                                                                    <asp:ListItem meta:resourcekey="DropDownList3Resource1" Selected="True" Text="Absent"
                                                                        Value="0"></asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="DropDownList3Resource2" Text="Default Exit" Value="1"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td></td>
                                                            <td class="DataArea"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 16px; vertical-align: top" colspan="3">
                                                    <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Label ID="Label3" runat="server" SkinID="Label_DefaultBold" Text="Loans Settings"
                                                                    meta:resourcekey="Label3Resource1" Visible="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="lblGosiCompPCT" runat="server" meta:resourcekey="lblGosiCompPCTResource1"
                                                                    SkinID="Label_DefaultNormal" Text="Max Loan PCT" Width="100px" Visible="true"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 40px;">
                                                                            <igtxt:WebNumericEdit ID="txtMaxLoanAmtPCT" runat="server" MinValue="0" MaxValue="9999"
                                                                                NullText="0" SkinID="WebNumericEdit_Default" ValueText="0" meta:resourcekey="txtGosiCompPCTResource1" Visible="true">
                                                                            </igtxt:WebNumericEdit>
                                                                        </td>
                                                                        <td style="width: 10px;">
                                                                            <asp:Label ID="lbl20" runat="server" SkinID="Label_DefaultBold" Text="%" meta:resourcekey="lbl20Resource1" Visible="true"></asp:Label>
                                                                        </td>
                                                                        <td></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td></td>
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
                                                            <td>
                                                                <asp:Label ID="lblDeductionTranstion" runat="server" meta:resourcekey="lblDeductionTranstionResource1"
                                                                    SkinID="Label_DefaultNormal" Text="Min Working Period" Width="100px" Visible="true"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 40px;">
                                                                            <igtxt:WebNumericEdit ID="txtMinServiceMonth" runat="server" MinValue="0" MaxValue="9999"
                                                                                NullText="0" SkinID="WebNumericEdit_Default" ValueText="0" meta:resourcekey="txtGosiEmpPCTResource1" Visible="true">
                                                                            </igtxt:WebNumericEdit>
                                                                        </td>
                                                                        <td style="width: 10px;"></td>
                                                                        <td></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td></td>
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
                                                            <td>
                                                                <asp:Label ID="lblGosiEmpPCT" runat="server" meta:resourcekey="lblGosiEmpPCTResource1"
                                                                    SkinID="Label_DefaultNormal" Text="Max Installement" Width="100px" Visible="true"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 40px;">
                                                                            <igtxt:WebNumericEdit ID="txtMaxInstallementPCT" runat="server" MinValue="0" MaxValue="9999"
                                                                                NullText="0" SkinID="WebNumericEdit_Default" ValueText="0" meta:resourcekey="txtGosiEmpPCTResource1" Visible="true">
                                                                            </igtxt:WebNumericEdit>
                                                                        </td>
                                                                        <td style="width: 10px;">
                                                                            <asp:Label ID="lbl21" runat="server" SkinID="Label_DefaultBold" Text="%" meta:resourcekey="lbl21Resource1" Visible="true"></asp:Label>
                                                                        </td>
                                                                        <td></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td></td>
                                                            <td class="DataArea"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 16px; vertical-align: top" colspan="3">
                                                    <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Label ID="Label6" runat="server" SkinID="Label_DefaultBold" Text="Costing Settings"
                                                                    meta:resourcekey="Label6Resource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" meta:resourcekey="Label7Resource1" SkinID="Label_DefaultNormal"
                                                                    Text="EOS Transaction" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlEOSCosting" runat="server" SkinID="DropDownList_LargNormal">
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
                                                            <td></td>
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
                                                            <td>
                                                                <asp:Label ID="Label15" runat="server" meta:resourcekey="Label15Resource1" SkinID="Label_DefaultNormal"
                                                                    Text="Vacation Transaction" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlVacCosting" runat="server" SkinID="DropDownList_LargNormal">
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
                                                            <td></td>
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
                                                            <td>
                                                                <asp:Label ID="Label16" runat="server" meta:resourcekey="Label16Resource1" SkinID="Label_DefaultNormal"
                                                                    Text="Tickets Transaction" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlTicketsCosting" runat="server" SkinID="DropDownList_LargNormal">
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
                                                            <td></td>
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
                                                            <td>
                                                                <asp:Label ID="Label14" runat="server" meta:resourcekey="Label14Resource1" SkinID="Label_DefaultNormal"
                                                                    Text="HI Transaction" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlHI" runat="server" SkinID="DropDownList_LargNormal">
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
                                                            <td></td>
                                                            <td class="DataArea"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 16px; vertical-align: top" colspan="3">
                                                    <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Label ID="Label8" runat="server" SkinID="Label_DefaultBold" Text="Vacations Slices"
                                                                    meta:resourcekey="Label8Resource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                        <td style="width: 22%; height: 16px; vertical-align: top">
                                                                <asp:Label ID="lblAdvanceBalance" runat="server" meta:resourcekey="AdvanceBalanceResource1" SkinID="Label_DefaultNormal"
                                                                    Text="AdvanceBalance" Width="100px"></asp:Label>
                                                            </td>
                                                <td>
                                                                <asp:CheckBox ID="chkAdvanceBalance" runat="server" />
                                                </td>
                                                            <td style="width: 30%; height: 16px; vertical-align: top">
                                                                <asp:Label ID="lblVacationTrans" runat="server" meta:resourcekey="VacationTransResource1" SkinID="Label_DefaultNormal"
                                                                    Text="VacationTrans" Width="100px"></asp:Label>
                                                       
                                                                <asp:CheckBox ID="chkVacationTrans" runat="server" />
                                                </td> 
                                                            </tr>
                                                </Table>
                                                    </td>
                                                
                                           
                                                
                                               <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top"> </td>
                                            </tr>

                                            <tr style="height: 6px; vertical-align: top" ></tr>
                                             <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                        <td style="width: 22%; height: 16px; vertical-align: top">
                                                                <asp:Label ID="lblVactionTransType" runat="server" meta:resourcekey="VactionTransTypeResource1" SkinID="Label_DefaultNormal"
                                                                    Text="VactionTransType" Width="100px"></asp:Label>
                                                            </td>
                                                <td>
                                                                <asp:DropDownList ID="ddlVactionTransType" Width="200px" runat="server" SkinID="DropDownList_smallNormal"
                                                                meta:resourcekey="ddlVactionTransTypeResource1" TabIndex="3">
                                                                <asp:ListItem meta:resourcekey="lblSelectResource1" Value="0">حدد الخيار</asp:ListItem>
                                                                                    <asp:ListItem meta:resourcekey="lblDaysTypeResource1" Value="1">ايام</asp:ListItem>
                                                                                    <asp:ListItem meta:resourcekey="lblPrecentResource1" Value="2">نسبة</asp:ListItem>
                                                            </asp:DropDownList>
                                                </td>
                                                            <td style="width: 30%; height: 16px; vertical-align: top">
                                                                <asp:Label ID="lblTransValue" runat="server" meta:resourcekey="TransValueResource1" SkinID="Label_DefaultNormal"
                                                                    Text="TransValue" Width="100px"></asp:Label>
                                                       
                                                                 <asp:TextBox ID="txtTransValue" runat="server"  SkinID="TextBox_SmalltNormalC"
                                                                                meta:resourcekey="txtTransValueResource"></asp:TextBox>
                                                </td> 
                                                            </tr>
                                                </Table>
                                                    </td>
                                                
                                           
                                                
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top"> </td>
                                            </tr>
                                            <tr style="height: 6px; vertical-align: top" ></tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                        <td style="width: 22%; height: 16px; vertical-align: top">
                                                                <asp:Label ID="lblAddBalanceInAddEmp" runat="server" meta:resourcekey="AddBalanceInAddEmpResource1" SkinID="Label_DefaultNormal"
                                                                    Text="AddBalanceInAddEmp" Width="100px"></asp:Label>
                                                            </td>
                                                <td>
                                                                <asp:CheckBox ID="chkAddBalanceInAddEmp" runat="server" />
                                                </td>
                                                            <td style="width: 33%; height: 16px; vertical-align: top"></td>
                                                             <td style="width: 16%; height: 16px; vertical-align: top">
                                                                <asp:Label ID="lblAccumulatedBalance" runat="server" meta:resourcekey="AccumulatedBalanceResource1" SkinID="Label_DefaultNormal"
                                                                    Text="AccumulatedBalance" Width="100px"></asp:Label>
                                                            </td>
                                                <td>
                                                                <asp:CheckBox ID="chkAccumulatedBalance" runat="server" />
                                                </td>
                                                            </tr>
                                                </Table>
                                                    </td>
                                               <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top"> </td>
                                            </tr>

                                            <tr>
                                                <td style="height: 16px; vertical-align: top" colspan="3">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgVacationTypes" runat="server" EnableAppStyling="False"
                                                                    Height="100%" meta:resourcekey="uwgVacationTypesResource1" SkinID="Default"
                                                                    Width="100%">
                                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                                        RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                        StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                        Version="4.00" ViewType="OutlookGroupBy">
                                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100px"
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
                                                                        <ClientSideEvents AfterRowActivateHandler="uwgVacationTypes_AfterRowActivateHandler" />
                                                                    </DisplayLayout>
                                                                    <Bands>
                                                                        <igtbl:UltraGridBand AllowAdd="No" AllowDelete="No" AllowUpdate="No" Key="VacationHead"
                                                                            meta:resourcekey="UltraGridBandResource3">
                                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                                            </AddNewRow>
                                                                            <Columns>
                                                                                <igtbl:UltraGridColumn BaseColumnName="VacationTypeID" DataType="System.Int32" Hidden="True"
                                                                                    Key="VacationTypeID" meta:resourcekey="UltraGridColumnResource21">
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="VacationType" Key="VacationType"
                                                                                    meta:resourcekey="UltraGridColumnResource22" Width="99%">
                                                                                    <Header Caption="Vacation Type">
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
                                                            </td>
                                                            <td style="width: 1%;"></td>
                                                            <td style="width: 69%;">
                                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgVacationDetails" runat="server" EnableAppStyling="False"
                                                                    Height="100%" meta:resourcekey="uwgVacationDetailsResource1" SkinID="Default"
                                                                    Width="100%">
                                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                                        RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                        StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                        Version="4.00" ViewType="OutlookGroupBy">
                                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100px"
                                                                            Width="100%">
                                                                        </FrameStyle>
                                                                        <ClientSideEvents AfterExitEditModeHandler="uwgVacationDetails_AfterExitEditModeHandler"
                                                                            AfterEnterEditModeHandler="uwgVacationDetails_AfterEnterEditModeHandler" />
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
                                                                        <igtbl:UltraGridBand AllowAdd="Yes" AllowDelete="Yes" AllowUpdate="Yes" Key="VacationDetails">
                                                                            <AddNewRow View="NotSet" Visible="Yes">
                                                                            </AddNewRow>
                                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                                </FilterHighlightRowStyle>
                                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                    Font-Size="11px" Width="190px">
                                                                                    <Padding Left="2px" />
                                                                                </FilterDropDownStyle>
                                                                            </FilterOptions>
                                                                            <Columns>
                                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="ID" Hidden="True" Key="ID">
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="VacationTypeID" Hidden="True"
                                                                                    Key="VacationTypeID" meta:resourcekey="UltraGridColumnResource16">
                                                                                    <Header>
                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="FromMonth" DataType="System.Double"
                                                                                    FieldLen="6" Key="FromMonth" Width="10%" Type="Custom" EditorControlID="wneFromMonth"
                                                                                    meta:resourcekey="UltraGridColumnResource17">
                                                                                    <Header Caption="From Month">
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Header>
                                                                                    <CellStyle HorizontalAlign="Right">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="ToMonth" DataType="System.Double"
                                                                                    FieldLen="6" Key="ToMonth" Width="10%" Type="Custom" EditorControlID="wneToMonth"
                                                                                    meta:resourcekey="UltraGridColumnResource18">
                                                                                    <Header Caption="To Month">
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Header>
                                                                                    <CellStyle HorizontalAlign="Right">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="RequiredWorkingMonths" DataType="System.Int32"
                                                                                    FieldLen="6" Key="RequiredWorkingMonths" Width="20%" Type="Custom" EditorControlID="wneRequiredWorkingMonths"
                                                                                    meta:resourcekey="UltraGridColumnResource19">
                                                                                    <Header Caption="Required Working Months">
                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                    </Header>
                                                                                    <CellStyle HorizontalAlign="Right">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="DurationDays" DataType="System.Int32"
                                                                                    FieldLen="6" Key="DurationDays" Width="15%" Type="Custom" EditorControlID="wneDurationDays"
                                                                                    meta:resourcekey="UltraGridColumnResource20">
                                                                                    <Header Caption="Duration Days">
                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                    </Header>
                                                                                    <CellStyle HorizontalAlign="Right">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="Yes" Hidden="true" BaseColumnName="TicketsRnd" DataType="System.Int32"
                                                                                    FieldLen="6" Key="TicketsRnd" Width="15%" meta:resourcekey="UltraGridColumnResource50">
                                                                                    <Header Caption="Tickets Rownds">
                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                    </Header>
                                                                                    <CellStyle HorizontalAlign="Right">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="Yes" Hidden="true" BaseColumnName="DependantTicketRnd" DataType="System.Int32"
                                                                                    FieldLen="6" Key="DependantTicketRnd" Width="20%" meta:resourcekey="UltraGridColumnResource51">
                                                                                    <Header Caption="Dep. Tickets Rownds">
                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                    </Header>
                                                                                    <CellStyle HorizontalAlign="Right">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="Yes" Hidden="true" BaseColumnName="MaxKeepDays" DataType="System.Int32"
                                                                                    FieldLen="6" Key="MaxKeepDays" Width="10%" meta:resourcekey="UltraGridColumnResource52">
                                                                                    <Header Caption="Max Balance">
                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                    </Header>
                                                                                    <CellStyle HorizontalAlign="Right">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="5" />
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
                                                <td style="height: 100%" colspan="3"></td>
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
