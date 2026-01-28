<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAppraisalPositionCriteria.aspx.vb"
    Inherits="frmEmployeesVacations" Culture="auto" meta:resourcekey="PageResource1"
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
    <title>* Venus Payroll * ~Employee Vacation Request</title>
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
        var empVacationId;

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

        // New method for vacation module
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

        function CloseIt(retvalue) {
            if (retvalue != "") {
                var Sender = window.document.getElementById(OSender);
                Sender.value = retvalue;
                Sender.focus();
            }
            var $dialog = ODialoge;
            $dialog.dialog('close');
        }
        function OpenModal3(pageurl, height, width, CheckID, SenderCtrl) {
            var ctrId = window.document.getElementById("lbVactionID");
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

        function OpenPrintedScreen(v) {
            var hight = window.screen.availHeight - 35;
            var width = window.screen.availWidth - 10;
            var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=EmployeeID&preview=1&ReportCode=CF_101&sq0=''&v=" + v, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            win.focus();
        }
    </script>
    <style type="text/css">
        .igWebDateChooserMainBlue2k7 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            text-align: center;
            border: 1px solid #9BB7E0;
        }
        
        /* محاذاة الكنترولات مع دعم اللغتين */
        .form-container {
            width: 100%;
            margin: 0 auto;
        }
        
        .form-row {
            margin-bottom: 15px;
            width: 100%;
            overflow: hidden;
        }
        
        .arabic-layout .form-label {
            float: right;
            width: 180px;
            text-align: right;
            padding: 5px 10px 5px 0;
            direction: rtl;
        }
        
        .english-layout .form-label {
            float: left;
            width: 180px;
            text-align: left;
            padding: 5px 0 5px 10px;
            direction: ltr;
        }
        
        .arabic-layout .form-control {
            float: right;
            width: 200px;
            text-align: right;
            direction: rtl;
        }
        
        .english-layout .form-control {
            float: left;
            width: 200px;
            text-align: left;
            direction: ltr;
        }
        
        .arabic-layout .form-checkbox {
            float: right;
            width: 200px;
            padding: 5px 0 5px 0;
            text-align: right;
            direction: rtl;
        }
        
        .english-layout .form-checkbox {
            float: left;
            width: 200px;
            padding: 5px 0 5px 0;
            text-align: left;
            direction: ltr;
        }
        
        .grid-container {
            clear: both;
            margin-top: 20px;
            width: 100%;
        }
        
        .clear {
            clear: both;
        }
        
        /* محاذاة الجداول حسب اللغة */
        .arabic-layout table {
            direction: rtl;
        }
        
        .english-layout table {
            direction: ltr;
        }
        
        .arabic-layout .LabelArea {
            text-align: right;
            padding-right: 10px;
        }
        
        .english-layout .LabelArea {
            text-align: left;
            padding-left: 10px;
        }
        
        .arabic-layout .DataArea {
            text-align: right;
        }
        
        .english-layout .DataArea {
            text-align: left;
        }
        
        /* محاذاة شريط الأدوات */
        .arabic-layout .toolbar-item {
            float: right;
            margin-left: 10px;
        }
        
        .english-layout .toolbar-item {
            float: left;
            margin-right: 10px;
        }
    </style>
</head>
<body style="margin: 0; padding: 0;">
    <form id="frmAnnualVacationsRequest" runat="server" defaultbutton="Button1">
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
        <asp:HiddenField ID="EmpVacationId" runat="server" />
        <asp:HiddenField ID="hdnWorkingHoursPerDay" runat="server" />
        <asp:HiddenField ID="hdnRequiredMonths" runat="server" />
        <asp:HiddenField ID="hdnAnnualVacId" runat="server" />
        <asp:HiddenField ID="hdnDurationDays" runat="server" />
        <asp:HiddenField ID="RemaningOPenBalanceDays" runat="server" Value="0" />
        <asp:HiddenField ID="OpenBalanceId" runat="server" Value="0" />
        <asp:HiddenField ID="hdDeletedStr" runat="server" />
        <asp:LinkButton ID="LinkButton_OverDueMessage" runat="server" Visible="False" meta:resourcekey="LinkButton_OverDueMessageResource1">LinkButton</asp:LinkButton>
        <asp:TextBox ID="lbVactionID" runat="server" meta:resourcekey="lbVactionIDResource1"></asp:TextBox>
         <asp:Label ID="lblLage" runat="server" meta:resourcekey="lblLageResource1"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="width: 10px; display: none">
                                <asp:Button ID="Button1" runat="server" OnClientClick="return false;" meta:resourcekey="Button1Resource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                    SkinID="HrSave_Command" meta:resourcekey="ImageButton_SaveResource1" CommandArgument="Save"
                                    TabIndex="-1" />
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
                                <asp:ImageButton ID="ImageButton_Print" Width="15px" Height="12px" runat="server"
                                    ImageUrl="~/Pages/HR/Img/i.p.edit.gif" meta:resourcekey="ImageButton_PrintResource1"
                                    CommandArgument="Print" />
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
                            <td>
                                <asp:ImageButton ID="textbtn" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command" 
                                  meta:resourcekey="ImageButton_NewResource1" CommandArgument="New" Visible="False" />
                            <asp:LinkButton ID="LinkButton1" runat="server" Visible="False" Text="مستحقات" CommandArgument="Payments2" meta:resourcekey="LinkButton_PaymentsResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Documents" runat="server" CommandArgument="Documents" Visible="False"
                                    meta:resourcekey="ImageButton_DocumentsRec" Height="16px" ImageUrl="./img/abook_add_1.gif"
                                    Width="16px" />
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Role" Width="16px" Height="16px" runat="server"
                                    SkinID="HrRole_Command" OnClientClick="OpenModal1('CompanyRoles.aspx?FrmID=2',450,844,false,''); return false;" meta:resourcekey="ImageButton_RoleResource1" />
                                <asp:LinkButton ID="LinkButton_Role" runat="server" Text="الإجراءات" meta:resourcekey="LinkButton_RoleResource1"
                                    OnClientClick="OpenModal1('CompanyRoles.aspx?FrmID=2',450,844,false,''); return false;"></asp:LinkButton>
                            </td>
                            <td style="width: 50px">
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
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        &nbsp;
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
                            <igtab:Tab Text="معايير التقييم للوظائف" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                    <div class="form-container" id="formContainer" runat="server">
                                        <div class="form-row">
                                            <div class="form-label">
                                                <asp:Label ID="LblPosition" runat="server" SkinID="Label_DefaultNormal"
                                                    Text="الوظيفة" meta:resourcekey="LblPositionResource1"></asp:Label>
                                            </div>
                                            <div class="form-control">
                                                <asp:DropDownList ID="ddlPosition" Width="200px" runat="server" AutoPostBack="True" SkinID="DropDownList_smallNormal"
                                                    meta:resourcekey="ddlFormCodeResource1" TabIndex="3">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                        <div class="form-row">
                                            <div class="form-label">
                                                <asp:Label ID="lblCritriaGroupType" runat="server" SkinID="Label_DefaultNormal"
                                                    Text="نوع مجموعة المعايير" meta:resourcekey="lblCritriaGroupTypeResource1"></asp:Label>
                                            </div>
                                            <div class="form-control">
                                                <asp:DropDownList ID="ddlCritriaGroupType" Width="200px" runat="server" AutoPostBack="True" SkinID="DropDownList_smallNormal"
                                                    meta:resourcekey="ddlFormCodeResource1" TabIndex="3">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                        <div class="form-row">
                                            <div class="form-label">
                                                <asp:Label ID="lblCriteriaGroup" runat="server" SkinID="Label_DefaultNormal"
                                                    Text="مجموعة المعايير" meta:resourcekey="lblCriteriaGroupResource1"></asp:Label>
                                            </div>
                                            <div class="form-control">
                                                <asp:DropDownList ID="ddlCriteriaGroup" Width="200px" runat="server" AutoPostBack="True" SkinID="DropDownList_smallNormal"
                                                    meta:resourcekey="ddlFormCodeResource1" TabIndex="3">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        
        <%--                                <div class="form-row">
                                            <div class="form-label">
                                                <asp:Label ID="LblMinimumScore" runat="server" Text="Default Minimum Score" SkinID="Label_DefaultNormal"
                                                    meta:resourcekey="LblMinimumScoreResource1"></asp:Label>
                                            </div>
                                            <div class="form-control">
                                                <asp:TextBox ID="txtminimumscore" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                                                    meta:resourcekey="txtArbNameResource1"></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                        <div class="form-row">
                                            <div class="form-label">
                                                <asp:Label ID="Lblmaximumscore" runat="server" Text="Default Maximum Score" SkinID="Label_DefaultNormal"
                                                    meta:resourcekey="LblmaximumscoreResource1"></asp:Label>
                                            </div>
                                            <div class="form-control">
                                                <asp:TextBox ID="TxtMaximumScore" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                                                    meta:resourcekey="txtArbNameResource1"></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                        <div class="form-row">
                                            <div class="form-label">
                                                <asp:Label ID="lblDefaultWeight" runat="server" Text="Default Weight" SkinID="Label_DefaultNormal"
                                                    meta:resourcekey="lblDefaultWeightResource1"></asp:Label>
                                            </div>
                                            <div class="form-control">
                                                <asp:TextBox ID="TxtDefaultWeight" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                                                    meta:resourcekey="txtArbNameResource1"></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                        <div class="form-row">
                                            <div class="form-label">
                                                <asp:Label ID="lblApplyDefault" runat="server" Text=" Apply Default  " SkinID="Label_DefaultNormal"
                                                    meta:resourcekey="lblDefaultWeightResource1"></asp:Label>
                                            </div>
                                            <div class="form-checkbox">
                                                <asp:CheckBox ID="ChkApplyDefault" runat="server" meta:resourcekey="ChkApplyDefaultResource1"
                                                    AutoPostBack="true" />
                                            </div>
                                        </div>--%>
                                        
                                        <div class="clear"></div>
                                        
                                        <div class="grid-container">
                                            <igtbl:UltraWebGrid Browser="UpLevel" ID="uwgPositioncriteria" runat="server" EnableAppStyling="True"
                                                Height="200px" meta:resourcekey="uwgForNationalityResource1" SkinID="Default"
                                                Width="100%">
                                                <DisplayLayout AllowColSizingDefault="Free" AllowDeleteDefault="Yes"  AllowAddNewDefault="Yes" AllowColumnMovingDefault="OnServer"
                                                    AllowUpdateDefault="Yes" AutoGenerateColumns="False" BorderCollapseDefault="Separate"
                                                    Name="uwgPositioncriteria" RowHeightDefault="18px"
                                                    SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                    TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" CellClickActionDefault="Edit"
                                                    TabDirection="LeftToRight">
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
                                                </DisplayLayout>
                                                <Bands>
                                                    <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1" AllowAdd="Yes">
                                                        <AddNewRow View="NotSet" Visible="Yes">
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
                                                            <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="true" Key="ID">
                                                            </igtbl:UltraGridColumn>

                                                            <igtbl:UltraGridColumn BaseColumnName="CriteriaName" width="70%" Key="CriteriaName" Type="DropDownList"  DataType="System.Int32" meta:resourcekey="CriteriaNameResource1" >
                                                                <Header Caption="CriteriaName">
                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                </Header>
                                                                <CellStyle HorizontalAlign="Justify">
                                                                </CellStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                                
                                                          
                                                            <igtbl:UltraGridColumn BaseColumnName="MinimumScore"  Key="MinimumScore" DataType="System.string" meta:resourcekey="MinimumScoreResource1" >
                                                                <Header Caption="أقل قيمة">
                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                </Header>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            
                                                            <igtbl:UltraGridColumn BaseColumnName="MaximumScore"  Key="MaximumScore" DataType="System.string" meta:resourcekey="MaximumScoreResource1" >
                                                                <Header Caption="أكبر قيمة">
                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                </Header>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                       <igtbl:UltraGridColumn BaseColumnName="Weight"  Key="Weight" DataType="System.string" meta:resourcekey="WeightResource1" >
                                               <Header Caption="الوزن النسبي">
                                                   <RowLayoutColumnInfo OriginX="5" />
                                               </Header>
                                               <CellStyle HorizontalAlign="Center">
                                               </CellStyle>
                                               <Footer>
                                                   <RowLayoutColumnInfo OriginX="3" />
                                               </Footer>
                                           </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="Select" DataType="System.Boolean" Key="Select" meta:resourcekey="IncludeResource1"
                                                                Width="20%" Type="CheckBox"  >
                                                                <Header Caption="اضافة">
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
                                        </div>
                                    </div>
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
            var Deletebtn = $("#<%=ImageButton_Delete.ClientID%>")

            Deletebtn.click(function () {
                if (confirm("هل انت متأكد من الحذف؟") == false) {
                    return false;
                }
            });

            // تحديد اتجاه الصفحة بناءً على اللغة
            function setPageDirection() {
                var isArabic = $('body').attr('dir') === 'rtl' ||
                    $('body').hasClass('arabic-layout') ||
                    document.documentElement.lang === 'ar';

                if (isArabic) {
                    $('body').addClass('arabic-layout').removeClass('english-layout');
                    $('.form-container').addClass('arabic-layout').removeClass('english-layout');
                } else {
                    $('body').addClass('english-layout').removeClass('arabic-layout');
                    $('.form-container').addClass('english-layout').removeClass('arabic-layout');
                }
            }

            // تطبيق الاتجاه عند تحميل الصفحة
            setPageDirection();

            // إعادة تطبيق الاتجاه عند تغيير اللغة (إذا كان هناك زر تغيير لغة)
            $('.language-switcher').on('click', function () {
                setTimeout(setPageDirection, 100);
            });

        });
    </script>
</body>
</html>