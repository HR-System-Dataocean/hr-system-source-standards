<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmUsers.aspx.vb" Inherits="frmUsers"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ System Users</title>
    <script language="javascript" src="Scripts/App_JScript.js"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js"></script>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js"></script>
    <script language="javascript" src="Scripts/App_JScript_SystemUser.js"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" id="Infragistics">

        function uwgGroups_AfterRowActivateHandler(gridName, rowId) {
            var Row = igtbl_getRowById(rowId);
            var intCompanyID = Row.getCellFromKey("CompanyID").getValue()
            //HideDetailsRows(intCompanyID, null)
        }

        function HideDetailsRows(CompanyID, GridCtrl) {
            debugger;
            var grid;
            var activeRowsCount = 0;
            if (GridCtrl == null)
                grid = igtbl_getGridById("UltraWebTab1xxctl0xuwgUsersBranches");
            if (grid == null) {
                grid = igtbl_getGridById("UltraWebTab1xctl0xuwgUsersBranches");
            }
            else
                grid = GridCtrl
            var rowCount = grid.Rows.length;
            for (i = 0; i < rowCount; i++) {
                var currRow = grid.Rows.rows[i];
                if (currRow != null)
                    if (ConvertToNumber(currRow.getCellFromKey("CompanyID").getValue()) == CompanyID) {
                        currRow.setHidden(false)
                        activeRowsCount = activeRowsCount + 1
                    }
                    else {
                        currRow.setHidden(true)
                    }
            }
        }

    </script>
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
    <form id="frmUsers" runat="server">
    <div style="display: none">
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
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                    SkinID="HrSave_Command" meta:resourcekey="ImageButton_SaveResource1" CommandArgument="Save" />
                            </td>
                            <td style="width: 120px">
                                <asp:ImageButton ID="ImageButton_SaveN" Width="16px" Height="16px" runat="server"
                                    CommandArgument="SaveNew" SkinID="HrSaveN_Command" meta:resourcekey="ImageButton_SaveNResource1" />
                                <asp:LinkButton ID="LinkButton_SaveN" runat="server" Text="حفظ مع جديد" meta:resourcekey="LinkButton_SaveNResource1"
                                    CommandArgument="SaveNew"></asp:LinkButton>
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
                            <td style="width: 20%">
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
                                        cellspacing="0" id="ttt">
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
                                                        <td style="width: 100%">
                                                            <asp:TextBox ID="txtCode" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                AutoPostBack="True" meta:resourcekey="txtCodeResource1"></asp:TextBox>
                                                            &nbsp;
                                                            <igtxt:WebImageButton ID="btnSearch" runat="server" AutoSubmit="False" Height="18px"
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
                                                            <asp:Label ID="lblEngName" runat="server" Text="التوصيف إنجليزى" SkinID="Label_DefaultNormal"
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
                                                                Text="الإعدادات كلمة السر"></asp:Label>
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
                                                            <asp:Label ID="lblHint" runat="server" SkinID="Label_DefaultNormal" Text="كلمة السر القديمة"
                                                                meta:resourcekey="lblHintResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtOldPass" runat="server" MaxLength="255" SkinID="TextBox_SmalltNormalC"
                                                                TextMode="Password" meta:resourcekey="txtOldPassResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lblCanChangePassword" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="يستطيع تغيير الكلمة" meta:resourcekey="lblCanChangePasswordResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="ChkCanChangePassword" runat="server" meta:resourcekey="ChkCanChangePasswordResource1" />
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
                                                        <td class="LabelArea" style="width: 23%">
                                                            <asp:Label ID="lblPassword" runat="server" SkinID="Label_DefaultNormal" Text="كلمة السر"
                                                                meta:resourcekey="lblPasswordResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtPassword" runat="server" MaxLength="255" SkinID="TextBox_SmalltNormalC"
                                                                TextMode="Password" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:CheckBox ID="chkChangePassword" runat="server" Text="تغيير كلمة السر" meta:resourcekey="chkChangePasswordResource1" />
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
                                                            <asp:Label ID="lblPasswordChangedOn" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="تاريخ تغيير كلمة السر" meta:resourcekey="lblPasswordChangedOnResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igsch:WebDateChooser ID="txtPasswordChangedOn" runat="server" Value="06/23/2012 10:15:58"
                                                                Width="130px" meta:resourcekey="txtPasswordChangedOnResource1">
                                                                <ClientSideEvents ValueChanged="frmUsertxtPasswordChangedOn_ValueChanged" />
                                                            </igsch:WebDateChooser>
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
                                                            <asp:Label ID="lblConfirmPassword" runat="server" SkinID="Label_DefaultNormal" Text="اعادة كلمة السر"
                                                                meta:resourcekey="lblConfirmPasswordResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtConfirmPassword" runat="server" MaxLength="255" SkinID="TextBox_SmalltNormalC"
                                                                TextMode="Password" meta:resourcekey="txtConfirmPasswordResource1"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:Label ID="Validator" runat="server" SkinID="Label_WarningBold" ToolTip="The password shoud match"
                                                                Text="غير متطابقة" meta:resourcekey="ValidatorResource1"></asp:Label>
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
                                                            <asp:Label ID="lblPasswordExpiryAt" runat="server" SkinID="Label_DefaultNormal" Text="تاريخ انتهاء الكلمة السر"
                                                                meta:resourcekey="lblPasswordExpiryAtResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igsch:WebDateChooser ID="txtPasswordExpiryAt" runat="server" Value="06/23/2012 10:15:58"
                                                                Width="130px" meta:resourcekey="txtPasswordExpiryAtResource1">
                                                                <ClientSideEvents ValueChanged="frmUsertxtPasswordExpiryAt_ValueChanged" />
                                                            </igsch:WebDateChooser>
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
                                                            <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_Title1Resource22" SkinID="Label_DefaultBold"
                                                                Text="الإعدادات المستخدم"></asp:Label>
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
                                                            <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_Title1Resource2" SkinID="Label_DefaultBold"
                                                                Text="الإعدادات التحكم بصلاحيات النظام"></asp:Label>
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
                                                            <asp:Label ID="lblEmpCode" runat="server" Text="الكود الموظف" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblEmpCodeResource"></asp:Label>
                                                        </td>
                                                        <td style="width: 100%">
                                                            <asp:TextBox ID="txtEmpCode" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                AutoPostBack="True"></asp:TextBox>
                                                            &nbsp;
                                                            <igtxt:WebImageButton ID="btnEmpCode" runat="server" AutoSubmit="False" Height="18px"
                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px">
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
                                                        <td style="width: 80px;">
                                                            <asp:Label ID="lblModulePermission" runat="server" SkinID="Label_DefaultNormal" Text="صلاحيات البرامج"
                                                                meta:resourcekey="lblModulePermissionResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 20px;">
                                                            <asp:CheckBox ID="ChkModulePermission" runat="server" meta:resourcekey="ChkModulePermissionResource1" />
                                                        </td>
                                                        <td style="width: 100px;">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 80px;">
                                                            <asp:Label ID="lblCanChangeGroups" runat="server" SkinID="Label_DefaultNormal" Text="صلاحيات المجموعات"
                                                                meta:resourcekey="lblCanChangeGroupsResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 20px;">
                                                            <asp:CheckBox ID="ChkCanChangeGroups" runat="server" meta:resourcekey="ChkCanChangeGroupsResource1" />
                                                        </td>
                                                        <td style="width: 40px;">
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
                                                        <td style="width: 80px;">
                                                            <asp:Label ID="lblIsAdmin" runat="server" SkinID="Label_DefaultNormal" Text="مدير النظام"
                                                                meta:resourcekey="lblIsAdminResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 20px;">
                                                            <asp:CheckBox ID="ChkIsAdmin" runat="server" meta:resourcekey="ChkIsAdminResource1" />
                                                        </td>
                                                        <td style="width: 100px;">
                                                        </td>
                                                        <td style="width: 80px;">
                                                            <asp:Label ID="lblisArabic" runat="server" SkinID="Label_DefaultNormal" Text="النظام بالعربي"
                                                                meta:resourcekey="lblisArabicResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 20px;">
                                                            <asp:CheckBox ID="ChkisArabic" runat="server" meta:resourcekey="ChkisArabicResource1" />
                                                        </td>
                                                        <td style="width: 40px;">
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
                                                        <td style="width: 80px;">
                                                            <asp:Label ID="lblFormPermission" runat="server" SkinID="Label_DefaultNormal" Text="صلاحيات الشاشات"
                                                                meta:resourcekey="lblFormPermissionResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 20px;">
                                                            <asp:CheckBox ID="ChkFormPermission" runat="server" meta:resourcekey="ChkFormPermissionResource1" />
                                                        </td>
                                                        <td style="width: 100px;">
                                                        </td>
                                                        <td style="width: 80px;">
                                                            <asp:Label ID="lblControlPermission" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="صلاحيات الحقول" meta:resourcekey="lblControlPermissionResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 20px;">
                                                            <asp:CheckBox ID="ChkControlPermission" runat="server" meta:resourcekey="ChkControlPermissionResource1" />
                                                        </td>
                                                        <td style="width: 40px;">
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
                                                        <td style="width: 80px;">
                                                            <asp:Label ID="lblDenyAccessforall" runat="server" SkinID="Label_DefaultNormal" Text="تعطيل المستخدم"
                                                                meta:resourcekey="lblDenyAccessforallResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 20px;">
                                                            <asp:CheckBox ID="ChkDenyAccessforall" runat="server" meta:resourcekey="ChkDenyAccessforallResource1" />
                                                        </td>
                                                        <td style="width: 100px;">
                                                        </td>
                                                        <td style="width: 80px;">
                                                            <asp:Label ID="lblWorkasIndividualuser" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="مستخدم مستقر" meta:resourcekey="lblWorkasIndividualuserResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 20px;">
                                                            <asp:CheckBox ID="ChkWorkasIndividualuser" runat="server" meta:resourcekey="ChkWorkasIndividualuserResource1" />
                                                        </td>
                                                        <td style="width: 40px;">
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
                                                        <td style="width: 80px;">
                                                            <asp:Label ID="lblRecordPermiddion" runat="server" SkinID="Label_DefaultNormal" Text="صلاحيات المعلموات"
                                                                meta:resourcekey="lblRecordPermiddionResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 20px;">
                                                            <asp:CheckBox ID="ChkRecordPermiddion" runat="server" meta:resourcekey="ChkRecordPermiddionResource1" />
                                                        </td>
                                                        <td style="width: 100px;">
                                                        </td>
                                                        <td style="width: 80px;">
                                                            <asp:Label ID="lblReportPermission" runat="server" SkinID="Label_DefaultNormal" Text="صلاحيات التقارير"
                                                                meta:resourcekey="lblReportPermissionResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 20px;">
                                                            <asp:CheckBox ID="ChkReportPermission" runat="server" meta:resourcekey="ChkReportPermissionResource1" />
                                                        </td>
                                                        <td style="width: 40px;">
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
                                                            <asp:Label ID="Label_Title1" runat="server" Text="برجاء تحديد الشركات" SkinID="Label_DefaultBold"
                                                                meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: top;">
                                            </td>
                                            <td style="width: 47%; vertical-align: top;">
                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                    cellspacing="6">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label_Title2" runat="server" Text="برجاء تحديد الأفرع" SkinID="Label_DefaultBold"
                                                                meta:resourcekey="Label_Title2Resource1"></asp:Label>
                                                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                      
                                                        </td>
                                                         <td style="vertical-align: bottom">  
                                                             <asp:ScriptManager ID="ScriptManager1" runat="server">
                                                            </asp:ScriptManager>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:CheckBox ID="chkCheckAll" runat="server" Text=" " AutoPostBack="True" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                         </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgUsersCompanies" runat="server" EnableAppStyling="False"
                                                                Height="100%" Width="100%" SkinID="Default" meta:resourcekey="uwgForNationalityResource1">
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                                                    HeaderClickActionDefault="SortSingle" Name="uwgForNationality" RowHeightDefault="18px"
                                                                    RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                                    AutoGenerateColumns="False" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                    Version="4.00" ViewType="OutlookGroupBy">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
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
                                                                    <ClientSideEvents AfterRowActivateHandler="uwgGroups_AfterRowActivateHandler" />
                                                                </DisplayLayout>
                                                                <Bands>
                                                                    <igtbl:UltraGridBand meta:resourceKey="UltraGridBandResource1">
                                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                                        </AddNewRow>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="CompanyName" Width="70%"
                                                                                meta:resourcekey="UltraGridColumnResource1">
                                                                                <Header Caption="Company">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="CanView" DataType="System.Boolean"
                                                                                Type="CheckBox" Width="30%" meta:resourcekey="UltraGridColumnResource2">
                                                                                <Header Caption="Can View">
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Footer>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="ID" DataType="System.Int32"
                                                                                Hidden="True" Key="ID" meta:resourcekey="UltraGridColumnResource3">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="CompanyID" DataType="System.Int32"
                                                                                Hidden="True" Key="CompanyID" meta:resourcekey="UltraGridColumnResource4">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="EngName" Hidden="True" meta:resourcekey="UltraGridColumnResource5">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ArbName" Hidden="True" meta:resourcekey="UltraGridColumnResource6">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Header>
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
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgUsersBranches" runat="server" EnableAppStyling="False"
                                                                        Height="100%" Width="100%" SkinID="Default" meta:resourcekey="uwgOnlyForProfessionResource1">
                                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                                                            HeaderClickActionDefault="SortSingle" Name="uwgOnlyForProfession" RowHeightDefault="18px"
                                                                            RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                                            AutoGenerateColumns="False" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                            Version="4.00" ViewType="OutlookGroupBy">
                                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
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
                                                                            <igtbl:UltraGridBand meta:resourceKey="UltraGridBandResource1">
                                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                                </AddNewRow>
                                                                                <Columns>
                                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="ID" DataType="System.Int32"
                                                                                        Hidden="True" Key="ID">
                                                                                        <Header Caption="ID">
                                                                                        </Header>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="CompanyID" Key="CompanyID" Hidden="True">
                                                                                        <Header>
                                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="BrancheID" Hidden="True" Key="BrancheID">
                                                                                        <Header Caption="BrancheID">
                                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn meta:resourcekey="EngNameRecourcekey" AllowUpdate="No" BaseColumnName="EngName"
                                                                                        Width="70%" Hidden="True" Key="EngName">
                                                                                        <Header Caption="BrancheID">
                                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn meta:resourcekey="ArbNameRecourcekey" BaseColumnName="ArbName"
                                                                                        Hidden="True" Key="ArbName">
                                                                                        <Header Caption="Branche">
                                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn meta:resourcekey="CanViewRecourcekey" AllowUpdate="Yes" BaseColumnName="CanView"
                                                                                        DataType="System.Boolean" Key="CanView" Type="CheckBox" Width="30%">
                                                                                        <Header Caption="Branche">
                                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                                        </Footer>
                                                                                        <CellStyle HorizontalAlign="Center">
                                                                                        </CellStyle>
                                                                                    </igtbl:UltraGridColumn>
                                                                                </Columns>
                                                                            </igtbl:UltraGridBand>
                                                                        </Bands>
                                                                    </igtbl:UltraWebGrid>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%" colspan="3">
                                                <asp:HiddenField ID="txtIsAdmin" runat="server" />
                                                <asp:TextBox ID="txtOldPassword" runat="server" Visible="False" meta:resourcekey="txtOldPasswordResource1"></asp:TextBox>
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
