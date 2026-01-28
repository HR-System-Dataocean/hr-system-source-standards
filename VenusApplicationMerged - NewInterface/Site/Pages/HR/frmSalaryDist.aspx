<%@ page language="VB" autoeventwireup="false" codefile="frmSalaryDist.aspx.vb"
    inherits="frmSalaryDist" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ register assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    namespace="Infragistics.WebUI.UltraWebTab" tagprefix="igtab" %>
<%@ register assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ register assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ Salary Distribution</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
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
        function isNumberKey(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                //Check if the text already contains the . character
                if (txt.value.indexOf('.') === -1) {
                    return true;
                } else {
                    return false;
                }
            } else {
                if (charCode > 31 &&
                    (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
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
        .style1 {
            height: 23px;
        }

        .hidden {
            display: none;
        }
    </style>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmAttendShifts" runat="server">
        <div style="display: none">
            <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
                meta:resourcekey="nameResource1"></asp:Label>
            <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
                TabIndex="-1" Width="99px"></asp:Label>
            <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
                Width="91px" meta:resourcekey="valueResource1">
            </asp:TextBox>
            <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
                meta:resourcekey="TargetControlResource1"></asp:Label>
            <igtxt:webtextedit meta:resourcekey="WebTextEdit1Recourcekey" id="WebTextEdit1" runat="server"
                height="22px" style="z-index: 1; left: 383px; top: 206px; position: absolute; width: 14%;">
            </igtxt:webtextedit>
            <igtxt:webdatetimeedit id="WebDateTimeEdit1" runat="server" displaymodeformat="HH:mm"
                editmodeformat="HH:mm" enableappstyling="False" skinid="WebDateTimeEdit_Default"
                datamode="Text">
            </igtxt:webdatetimeedit>
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
                                        meta:resourcekey="LinkButton_SaveNResource1" OnClientClick="SaveOtherFieldsData();">
                                    </asp:LinkButton>
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
                                        meta:resourcekey="LinkButton_PropertiesResource1">
                                    </asp:LinkButton>
                                </td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                        SkinID="HrRemarks_Command" CommandArgument="Remarks" meta:resourcekey="ImageButton_RemarksResource1" />
                                    <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="ملاحظات" CommandArgument="Remarks"
                                        meta:resourcekey="LinkButton_RemarksResource1">
                                    </asp:LinkButton>
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
                        <igtab:ultrawebtab id="UltraWebTab1" runat="server" enableappstyling="True" skinid="Default"
                            meta:resourcekey="UltraWebTab1Resource1">
                            <tabs>
                                <igtab:tab text="عام" meta:resourcekey="TabResource1">
                                    <contenttemplate>
                                        <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
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
                                                                <asp:Label ID="lblCode" runat="server" Text="الكود" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblCodeResource1"></asp:Label>
                                                                <asp:Label ID="Label_Star1" runat="server" Text="*" Style="color: #FF0000" meta:resourcekey="Label_Star1Resource1"></asp:Label>
                                                            </td>
                                                            <td class="DataAreawithsearch">
                                                                <asp:TextBox ID="txtCode" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                    AutoPostBack="True" meta:resourcekey="txtCodeResource1">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td class="search">
                                                                <igtxt:webimagebutton id="btnSearchCode" runat="server" autosubmit="False" height="18px"
                                                                    overflow="NoWordWrap" usebrowserdefaults="False" width="24px" meta:resourcekey="btnSearchCodeResource1">
                                                                    <alignments textimage="ImageBottom" />
                                                                    <appearance>
                                                                        <image url="./Img/forum_search.gif" />
                                                                    </appearance>
                                                                </igtxt:webimagebutton>
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
                                                                <asp:Label ID="lblEngName" runat="server" Text="التوصيف إنجليزى" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalltr" MaxLength="255"
                                                                    meta:resourcekey="txtEngNameResource1">
                                                                </asp:TextBox>
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
                                                                <asp:Label ID="lblArbName" runat="server" Text="التوصيف عربى" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblArbNameResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                                                                    meta:resourcekey="txtArbNameResource1">
                                                                </asp:TextBox>
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
                                                <td style="width: 47%; height: 30px; vertical-align: top;">
                                                    <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                        cellspacing="6">
                                                        <tr>
                                                            <td style="vertical-align: bottom" class="style1">
                                                                <asp:Label ID="Label_Title1" runat="server" Text="برجاء توزيع الراتب على المشاريع"
                                                                    SkinID="Label_DefaultBold" meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; vertical-align: top;"></td>
                                                <td style="width: 47%; vertical-align: top;">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblProjectName" runat="server" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblProjectName"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlProjects" runat="server" SkinID="DropDownList_LargNormal"
                                                                    meta:resourcekey="ddlHICompanyResource1" AutoPostBack="false">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="Label1" runat="server" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblProjectPercentage"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtProjectPercentage" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="5"
                                                                    meta:resourcekey="txtArbNameResource1" onkeypress="return isNumberKey(this, event);">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Button ID="btnAddProject" runat="server" SkinID="Button_LargeNormalrtl" meta:resourcekey="btnAddProject"
                                                                    OnClick="btnAddProject_Click" />
                                                            </td>
                                                            <td class="DataArea"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 16px; vertical-align: top" colspan="3">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <%--<td>
                                                                <igtbl:UltraWebGrid   ID="uwgAttendShiftsDays" runat="server" EnableAppStyling="True"
                                                                    Height="200px" meta:resourcekey="uwgForNationalityResource1" SkinID="Default"
                                                                    Width="100%">
                                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer"
                                                                        AllowUpdateDefault="Yes" AutoGenerateColumns="False" BorderCollapseDefault="Separate"
                                                                        Name="uwgAttendShiftsDays" RowHeightDefault="18px"
                                                                        SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                                        TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" CellClickActionDefault="Edit"
                                                                        TabDirection="LeftToRight">
                                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="200px"
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
                                                                    </DisplayLayout>
                                                                    <Bands>
                                                                        <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1" AllowAdd="Yes">
                                                                            <AddNewRow View="NotSet" Visible="No">
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
                                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID">
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn BaseColumnName="AttendShiftID" Hidden="True" Key="AttendShiftID">
                                                                                    <Header>
                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn BaseColumnName="DayNo" Key="DayNo" DataType="System.Int32"
                                                                                    Type="DropDownList" Width="30%" AllowUpdate="No">
                                                                                    <Header Caption="يوم الأسبوع">
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Header>
                                                                                    <ValueList>
                                                                                        <ValueListItems>
                                                                                            <igtbl:ValueListItem DataValue="1" DisplayText="السبت" Key="7" />
                                                                                            <igtbl:ValueListItem DataValue="2" DisplayText="الأحد" Key="1" />
                                                                                            <igtbl:ValueListItem DataValue="3" DisplayText="الإثنين" Key="2" />
                                                                                            <igtbl:ValueListItem DataValue="4" DisplayText="الثلاثاء" Key="3" />
                                                                                            <igtbl:ValueListItem DataValue="5" DisplayText="الأربعاء" Key="4" />
                                                                                            <igtbl:ValueListItem DataValue="6" DisplayText="الخميس" Key="5" />
                                                                                            <igtbl:ValueListItem DataValue="7" DisplayText="الجمعة" Key="6" />
                                                                                        </ValueListItems>
                                                                                    </ValueList>
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn BaseColumnName="TimeIn" Key="TimeIn" Width="15%" EditorControlID="WebDateTimeEdit1"
                                                                                    Type="Custom">
                                                                                    <Header Caption="الدوام من">
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Header>
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn Width="15%" BaseColumnName="TimeOut" Key="TimeOut" EditorControlID="WebDateTimeEdit1"
                                                                                    Type="Custom">
                                                                                    <Header Caption="الدوام إلى">
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Header>
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn BaseColumnName="TimeIn2nd" Key="TimeIn2nd" Width="15%" EditorControlID="WebDateTimeEdit1"
                                                                                    Type="Custom">
                                                                                    <Header Caption="الدوام من ">
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Header>
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn Width="15%" BaseColumnName="TimeOut2nd" Key="TimeOut2nd" EditorControlID="WebDateTimeEdit1"
                                                                                    Type="Custom">
                                                                                    <Header Caption="الدوام الى">
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Header>
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn BaseColumnName="IsDayOff" DataType="System.Boolean" Key="IsDayOff"
                                                                                    Width="10%" Type="CheckBox" AllowUpdate="Yes">
                                                                                    <Header Caption="عطلة أسبوعية">
                                                                                        <RowLayoutColumnInfo OriginX="10" />
                                                                                    </Header>
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                                    </SelectedCellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="10" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                            </Columns>
                                                                        </igtbl:UltraGridBand>
                                                                    </Bands>
                                                                </igtbl:UltraWebGrid>
                                                            </td>--%>
                                                            <td>
                                                                <asp:GridView ID="grdProjects" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="4" ForeColor="#333333" Width="100%" OnRowCommand="grdProjects_RowCommand">
                                                                    <footerstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" />
                                                                    <headerstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" />
                                                                    <pagerstyle backcolor="#284775" forecolor="White" horizontalalign="Center" />
                                                                    <rowstyle backcolor="#F7F6F3" forecolor="#333333" />
                                                                    <selectedrowstyle backcolor="#E2DED6" font-bold="True" forecolor="#333333" />
                                                                    <columns>
                                                                        <asp:BoundField DataField="ProjectID" HeaderText="Project ID" InsertVisible="False" SortExpression="ProjectID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName" />
                                                                        <asp:BoundField DataField="ProjectPercentage" HeaderText="Project Percentage" SortExpression="ProjectPercentage" />
                                                                        <asp:TemplateField HeaderText="Actions">
                                                                            <itemtemplate>
                                                                                <asp:ImageButton ID="imgProject" runat="server" ImageURL="~/Pages/HR/Img/i.p.edit.gif" CommandName="EdirRow" CommandArgument='<%# Eval("ProjectID")%>'></asp:ImageButton>
                                                                                <asp:ImageButton ID="imgDelete" runat="server" ImageURL="~/Pages/HR/Img/logoff_small.gif" CommandName="Remove" CommandArgument='<%# Eval("ProjectID")%>'></asp:ImageButton>
                                                                            </itemtemplate>
                                                                        </asp:TemplateField>
                                                                    </columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 100%" colspan="3"></td>
                                            </tr>
                                        </table>
                                    </contenttemplate>
                                </igtab:tab>
                            </tabs>
                        </igtab:ultrawebtab>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
