<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeesOthersVacations.aspx.vb"
    Inherits="frmEmployeesOthersVacations" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Employee Vacations</title>
    <script language="javascript" src="Scripts/App_JScript.js"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js"></script>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js"></script>
    <script language="javascript" src="Scripts/App_EmpVacations.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
        function TlbMainToolbar_Click(oToolbar, oButton, oEvent) {
            var tlbControl = igtbar_getToolbarById("TlbMainToolbar");
            if (tlbControl.Items.fromKey("Payments").Selected == true) {
                btnVacationTransactionOn_Click();
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
        function OpenModal3(pageurl, height, width, CheckID, SenderCtrl) {
            var ctrId = window.document.getElementById("lblRecordID");
            if (CheckID == false || (ctrId.value != "" && ctrId.value != null && ctrId.value != "0")) {
                var page = pageurl + "RId=" + ctrId.value;
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
        function uwgEmployeeVacations_ClickCellButtonHandler(gridName, cellId) {

            var ultraTab = igtab_getTabById("UltraWebTab1");

            var Row = igtbl_getActiveRow(gridName);
            empVacationId = Row.getCellFromKey("ID").getValue();

            OpenModalNew('frmAnnualVacationDocuments.aspx?TB=hrs_EmployeesVacations&SV=' + empVacationId + '&', 495, 800)
            //var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=EmployeeTransactionID&preview=1&ReportCode=VacSlip&sq0=''&v=" + intPaymentTransID, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            //win.focus();
        }
        function OpenModalNew(pageurl, height, width) {

            var page = pageurl + "ItemId=" + empVacationId

            var $dialog = $('<div></div>')
                .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: height,
                    width: width
                });
            ODialoge = $dialog;
            $dialog.dialog('open');
        }
    </script>
    <style type="text/css">
        .igWebDateChooserMainBlue2k7 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            text-align: center;
            border: 1px solid #9BB7E0;
        }

        .igWebDateChooserMainBlue2k7 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            text-align: center;
            border: 1px solid #9BB7E0;
        }
    </style>
</head>
<body style="margin: 0; padding: 0;">
    <form id="frmEmployeesOthersVacations" runat="server" defaultbutton="Button1">
        <div style="display: none">
            <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
                meta:resourcekey="nameResource1"></asp:Label>
            <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
                TabIndex="-1" Width="99px"></asp:Label>
            <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
                Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
            <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
                meta:resourcekey="TargetControlResource1"></asp:Label>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
            </asp:ScriptManager>
            <asp:HiddenField ID="hdnWorkingHoursPerDay" runat="server" />
            <asp:HiddenField ID="hdnRequiredMonths" runat="server" />
            <asp:HiddenField ID="hdnAnnualVacId" runat="server" />
            <asp:HiddenField ID="hdnDurationDays" runat="server" />
            <asp:LinkButton ID="LinkButton_OverDueMessage" runat="server" Visible="False" meta:resourcekey="LinkButton_OverDueMessageResource1">LinkButton</asp:LinkButton>
            <asp:TextBox ID="lbVactionID" runat="server" meta:resourcekey="lbVactionIDRecordIDResource1"></asp:TextBox>
        </div>
        <div class="Div_MasterContainer" runat="server" id="DIV">
            <table align="center" style="width: 100%;">
                <tr>
                    <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                        <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                            <tr>
                                <td style="width: 10px; display: none">
                                    <asp:Button ID="Button1" runat="server" Text="" OnClientClick="return false;" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                        SkinID="HrSave_Command" meta:resourcekey="ImageButton_SaveResource1" CommandArgument="Save" />
                                </td>
                                <td style="width: 10px">
                                    <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                        meta:resourcekey="ImageButton_NewResource1" CommandArgument="New" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server"
                                        SkinID="HrDelete_Command" meta:resourcekey="ImageButton_DeleteResource1" CommandArgument="Delete" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                        SkinID="HrPrint_Command" meta:resourcekey="ImageButton_PrintResource1" CommandArgument="Print" />
                                </td>
                                <td style="width: 40px">
                                    <asp:Label ID="Label_TSP3" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                                </td>
                                <td style="width: 90px">
                                    <asp:ImageButton ID="ImageButton_Properties" Width="16px" Height="16px" runat="server"
                                        SkinID="HrProperties_Command" meta:resourcekey="ImageButton_PropertiesResource1"
                                        CommandArgument="Property" />
                                    <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="خصائص" meta:resourcekey="LinkButton_PropertiesResource1"
                                        CommandArgument="Property"></asp:LinkButton>
                                </td>
                                <td style="width: 90px">
                                    <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                        SkinID="HrRemarks_Command" meta:resourcekey="ImageButton_RemarksResource1" CommandArgument="Remarks" />
                                    <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="ملاحظات" meta:resourcekey="LinkButton_RemarksResource1"
                                        CommandArgument="Remarks"></asp:LinkButton>
                                </td>
                                     <td style="width: 20px">
         <asp:Label ID="Label_TSP2" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
     </td>
     <td style="width: 90px">
         <asp:ImageButton ID="ImageButtonPayment" Width="14px" Height="12px" runat="server"
             meta:resourcekey="ImageButton_PaymentsResource1" CommandArgument="Payments" ImageUrl="~/Pages/HR/Img/cal_year.gif" />
         <asp:LinkButton ID="LinkButtonPayment" runat="server" Text="مستحقات" meta:resourcekey="LinkButton_PaymentsResource1"></asp:LinkButton>
     </td>
                                <td style="width: 20px">&nbsp;
                                </td>
                                <td>

                                    <asp:ImageButton ID="ImageButton_Payments" Width="14px" Height="12px" runat="server"
                                        meta:resourcekey="ImageButton_PaymentsResource1" CommandArgument="Payments" ImageUrl="~/Pages/HR/Img/cal_year.gif" Visible="False" />
                                    <asp:LinkButton ID="LinkButton_Payments" runat="server" Text="مستحقات" meta:resourcekey="LinkButton_PaymentsResource1" Visible="False"></asp:LinkButton>
                                </td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Documents" runat="server" meta:resourcekey="ImageButton_DocumentsRec"
                                        Height="16px" ImageUrl="./img/abook_add_1.gif" Width="16px" />
                                </td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                        SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                    <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
                                </td>
                                <td style="width: 50px"></td>
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
                                                    
                                                    <tr>
        <td style="width: 40%; height: 16px; vertical-align: middle;">
            <asp:Label ID="LblRequestID" runat="server" Text="RequestID" SkinID="Label_CopyRightsBold"
                meta:resourcekey="LblRequestIDResource1"></asp:Label>
        </td>
        <td style="width: 60%; height: 16px; vertical-align: middle;">
            <asp:Label ID="LblRequestIDValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegUserValueResource1"></asp:Label>
        </td>
    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 49%; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 40%; height: 16px; vertical-align: middle;">&nbsp;
                                                        </td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">&nbsp;
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
                                                <td style="height: 10px" colspan="3"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea" style="min-width: 90px;">
                                                                <asp:Label ID="lblCode" runat="server" Text="الكود" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblCodeResource1"></asp:Label>
                                                                <asp:Label ID="Label_Star1" runat="server" Text="*" Style="color: #FF0000" meta:resourcekey="Label_Star1Resource1"></asp:Label>
                                                            </td>
                                                            <td class="DataAreawithsearch">
                                                                <asp:TextBox ID="txtEmployee" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                    AutoPostBack="True" meta:resourcekey="txtEmployeeResource1"></asp:TextBox>
                                                            </td>
                                                            <td class="search">
                                                                <igtxt:WebImageButton ID="btnEmployee" runat="server" AutoSubmit="False" Height="18px"
                                                                    meta:resourcekey="btnEmployeeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                    Width="24px">
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
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                        <td class="DataArea">
                                                              <asp:ImageButton ID="ImageButton_Refresh" runat="server" ImageUrl="~/Common/Images/refresh.png" />
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
                                                            <td class="LabelArea">
                                                                <asp:Label ID="labelDescEnglishName" Width="90px" runat="server" Text="اسم الموظف"
                                                                    SkinID="Label_DefaultNormal" meta:resourcekey="labelDescEnglishNameResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="lblDescEnglishName" runat="server" SkinID="TextBox_LargeNormalC"
                                                                    ReadOnly="True" MaxLength="255" meta:resourcekey="lblDescEnglishNameResource1"
                                                                    TabIndex="1"></asp:TextBox>
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
                                                                <asp:Label ID="labelDescNationality" Width="90px" runat="server" Text="الجنسية" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="labelDescNationalityResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="lblDescNationality" runat="server" ReadOnly="true" SkinID="TextBox_LargeNormalc"
                                                                    meta:resourcekey="lblDescNationalityResource1" TabIndex="2"></asp:TextBox>
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
                                                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_Title1Resource2" SkinID="Label_DefaultBold"
                                                                    Text="الإعدادات الاجازة"></asp:Label>
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
                                                                <asp:Label ID="lblVacationType" runat="server" Width="90px" SkinID="Label_DefaultNormal"
                                                                    Text="نوع الاجازة" meta:resourcekey="lblVacationTypeResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="DdlVacationType" runat="server" SkinID="DropDownList_LargNormal"
                                                                    meta:resourcekey="DdlVacationTypeResource1" TabIndex="3" AutoPostBack="true">
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
                                                                <asp:Label ID="lblWebDateChooser1" runat="server" meta:resourcekey="lblWebDateChooser1Resource1"
                                                                    SkinID="Label_DefaultNormal" Text="تاريخ البدء" Width="90px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igsch:WebDateChooser ID="WebDateChooser1" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                    BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                    Width="130px" EnableAppStyling="True">
                                                                </igsch:WebDateChooser>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">&nbsp;
                                                            </td>
                                                            <td class="DataArea">&nbsp;
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
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblWebDateChooser2" runat="server" meta:resourcekey="lblWebDateChooser2Resource1"
                                                                    SkinID="Label_DefaultNormal" Text="تاريخ الرجوع" Width="90px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igsch:WebDateChooser ID="WebDateChooser2" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                    BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma; 
                                                                    font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                    Width="130px" EnableAppStyling="True">
                                                                    <AutoPostBack ValueChanged="True" />
                                                                </igsch:WebDateChooser>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">&nbsp;
                                                            </td>
                                                            <td class="DataArea">&nbsp;
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
                                                                <asp:Label ID="lblvactiondays" runat="server" Width="90px" SkinID="Label_DefaultNormal"
                                                                    Text="أيام الإجازة" meta:resourcekey="lblvactiondaysResource"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtVactiondays" runat="server" SkinID="TextBox_SmalltNormalc" meta:resourcekey="lbConsumeValResource1"
                                                                    TabIndex="6" AutoPostBack="True" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_Title2Resource2" SkinID="Label_DefaultBold"
                                                                    Text="الاجازات السابقة"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 100%" colspan="3">
                                                    <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgEmployeeVacations" runat="server"
                                                        Height="100%" meta:resourcekey="uwgEmployeeSubjectsResource1" SkinID="Default"
                                                        Width="100%">
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                                            AutoGenerateColumns="False" BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect"
                                                            CellPaddingDefault="1" CellSpacingDefault="1"   GridLinesDefault="NotSet"
                                                            HeaderClickActionDefault="SortMulti" Name="uwgEmployeeSubjects"
                                                            RowHeightDefault="15px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00"
                                                            ViewType="OutlookGroupBy" AllowRowNumberingDefault="Continuous">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                Width="99%">
                                                            </FrameStyle>
                                                            <ClientSideEvents AfterCellUpdateHandler="UwgSearchEmployees_AfterCellUpdateHandler"
                                                                ClickCellButtonHandler="uwgEmployeeVacations_ClickCellButtonHandler" />
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
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="VacationTypeID" DataType="System.Int32" Hidden="True"
                                                                        Key="VacationTypeID" meta:resourcekey="UltraGridColumnResource1" Type="DropDownList"
                                                                        Width="30%">
                                                                        <Header Caption="Vacation Type">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ExpectedStartDate" DataType="System.DateTime"
                                                                        Format="dd/MM/yyyy hh:mm tt" Hidden="True" Key="ExpectedStartDate" meta:resourcekey="UltraGridColumnResource2"
                                                                        Width="20%">
                                                                        <Header Caption="E Start Date">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ExpectedEndDate" DataType="System.DateTime"
                                                                        Format="dd/MM/yyyy hh:mm tt" Hidden="True" Key="ExpectedEndDate" meta:resourcekey="UltraGridColumnResource3"
                                                                        Width="20%">
                                                                        <Header Caption="E End Date">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ActualStartDate" DataType="System.DateTime"
                                                                        Format="dd/MM/yyyy hh:mm tt" Key="ActualStartDate" meta:resourcekey="UltraGridColumnResource4"
                                                                        Width="50%">
                                                                        <Header Caption="Start Date">
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ActualEndDate" DataType="System.DateTime"
                                                                        Format="dd/MM/yyyy hh:mm tt" Key="ActualEndDate" meta:resourcekey="UltraGridColumnResource5"
                                                                        Width="50%">
                                                                        <Header Caption="End Date">
                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ConsumDays" DataType="System.Int32" 
                                                                        Key="ConsumDays" meta:resourcekey="UltraGridColumnResource11" Width="10%">
                                                                        <Header Caption="ConsumDays">
                                                                            <RowLayoutColumnInfo OriginX="11" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="11" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn Type="Button" Width="10%" AllowRowFiltering="False" CellButtonDisplay="Always"
                                                                            meta:resourcekey="UltraGridColumnResource17">
                                                                            <Header Caption="">
                                                                                <RowLayoutColumnInfo OriginX="10" />
                                                                            </Header>
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="10" />
                                                                            </Footer>
                                                                            <CellButtonStyle BackgroundImage="./img/abook_add_1.gif" BorderStyle="None"
                                                                                Cursor="Hand" Height="12px" Width="13px">
                                                                            </CellButtonStyle>
                                                                        </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="EmployeeRequestRemarks" Hidden="True" Key="EmployeeRequestRemarks"
                                                                        meta:resourcekey="UltraGridColumnResource6" Width="0px">
                                                                        <Header Caption="Request Remarks">
                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="DepartmentApproval" DataType="System.Boolean"
                                                                        Hidden="True" Key="DepartmentApproval" meta:resourcekey="UltraGridColumnResource7"
                                                                        Type="CheckBox" Width="10%">
                                                                        <Header Caption="Dep Approval">
                                                                            <RowLayoutColumnInfo OriginX="6" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="6" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="VacationApproval" DataType="System.Boolean"
                                                                        Hidden="True" Key="VacationApproval" meta:resourcekey="UltraGridColumnResource8"
                                                                        Type="CheckBox" Width="10%">
                                                                        <Header Caption="Vacation Approval">
                                                                            <RowLayoutColumnInfo OriginX="7" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="7" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="PaperProcessed" DataType="System.Boolean"
                                                                        Hidden="True" Key="PaperProcessed" meta:resourcekey="UltraGridColumnResource9"
                                                                        Type="CheckBox" Width="10%">
                                                                        <Header Caption="Paper Processed">
                                                                            <RowLayoutColumnInfo OriginX="8" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="8" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Int32" Hidden="True"
                                                                        Key="ID" meta:resourcekey="UltraGridColumnResource10">
                                                                        <Header>
                                                                            <RowLayoutColumnInfo OriginX="9" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="9" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                </Columns>
                                                            </igtbl:UltraGridBand>
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource2">
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="VacationTypeID" DataType="System.Int32" Key="VacationTypeID"
                                                                        meta:resourcekey="UltraGridColumnResource1" Type="DropDownList" Width="200px">
                                                                        <Header Caption="Vacation Type">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ActualStartDate" DataType="System.DateTime"
                                                                        Format="dd/MM/yyyy hh:mm tt" Key="ActualStartDate" meta:resourcekey="UltraGridColumnResource4"
                                                                        Width="15%">
                                                                        <Header Caption="Start Date">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ActualEndDate" DataType="System.DateTime"
                                                                        Format="dd/MM/yyyy hh:mm tt" Key="ActualEndDate" meta:resourcekey="UltraGridColumnResource5"
                                                                        Width="15%">
                                                                        <Header Caption="End Date">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                     
                                                                </Columns>

                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
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
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script type="text/javascript" id="igClientScript">
        $(document).ready(function () {

               // var Deletebtn = $("#<%=ImageButton_Delete.ClientID%>")
            //  Deletebtn.click(function () {
            //   if (confirm("هل انت متأكد من الحذف؟") == false)
            // {

            //       return false;
            // } 

            //}
            // )


        });

    </script>
</body>
</html>
