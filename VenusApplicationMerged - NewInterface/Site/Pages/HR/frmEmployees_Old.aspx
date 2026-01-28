<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployees_Old.aspx.vb" Inherits="frmEmployees"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Employees</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
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


        function OpenPrintedScreen(v) {

            var hight = window.screen.availHeight - 35;
            var width = window.screen.availWidth - 10;
            var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=EmployeeCode&preview=1&ReportCode=EmployeeDetails&sq0=''&v=" + v, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            win.focus();
        }
        function DisplayImageScreenEmp(IntObjectId) {
            var webTab = igtab_getTabById("UltraWebTab1");
            if (webTab == null)
                return;
            var ctrId = window.document.getElementById("txtEmpId");

            if (ctrId.value != "" && ctrId.value != null && ctrId.value != "0")
                window.open("frmPictures.aspx?OId=" + IntObjectId + "&RId=" + ctrId.value, "_blank", "height=435,width=447,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");
        }
        function DisplayImageScreenEmp2(IntObjectId) {
            var webTab = igtab_getTabById("UltraWebTab1");
            if (webTab == null)
                return;
            var ctrId = window.document.getElementById("txtEmpId");

            if (ctrId.value != "" && ctrId.value != null && ctrId.value != "0")
                window.open("frmSignature.aspx?OId=" + IntObjectId + "&RId=" + ctrId.value, "_blank", "height=435,width=447,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");
        }

        var ODialoge;
        var OSender;
        function OpenModal1(pageurl, height, width, CheckID, CheckContract, SenderCtrl) {
            var ctrId = window.document.getElementById("txtEmpId");
            var ContraId = window.document.getElementById("txtContractId");
            if (CheckContract == true) {
                if (ContraId.value == "" || ContraId.value == null || ContraId.value == "0") {
                    return;
                }
            }

            if (CheckID == false || (ctrId.value != "" && ctrId.value != null && ctrId.value != "0")) {

                var page = pageurl + "EmpID=" + ctrId.value + "&ContID=" + ContraId.value;
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

        function OpenModal12(pageurl, height, width) {
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
            $dialog.dialog('open');
        }
        $(function () {
            var icons = {
                header: "",
                activeHeader: ""
            };
            $("#accordion").accordion({ icons: icons, autoHeight: false });
            $("#accordion").accordion("option", "icons", "");
        });
        $(function () {
            try {
                $(".Validators").Float();
            }
            catch (err) { }
        });







        function CheckNumeric(e) {

            if (window.event) // IE 
            {
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) {
                    event.returnValue = false;
                    return false;

                }

            }
            else { // Fire Fox
                if ((e.which < 48 || e.which > 57) & e.which != 8) {
                    e.preventDefault();
                    return false;

                }
            }
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 50%;
            height: 21px;
        }
    </style>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmEmployees" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div style="display: none">
            <table>
                <tr>
                    <td>
                        <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                            <tr>
                                <td style="width: 10px;"></td>
                                <td style="width: 20%;">
                                    <asp:LinkButton ID="LinkButton_Contracts" runat="server" CommandArgument="Contracts"
                                        Style="font-family: Tahoma; font-size: 8pt;" meta:resourcekey="LinkButton_ContractsResource1"
                                        Text="View Contracts &amp; Vacation"></asp:LinkButton>
                                </td>
                                <td style="width: 25%;">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="Contracts" Style="font-family: Tahoma; font-size: 8pt;"
                                        meta:resourcekey="LinkButton_HTResource1" Text="View Health Insurance & Tickets"></asp:LinkButton>
                                </td>
                                <td style="width: 20%;">
                                    <asp:LinkButton ID="LinkButton_Transaction" runat="server" CommandArgument="Transaction"
                                        meta:resourcekey="LinkButton_TransactionResource1" Style="font-family: Tahoma; font-size: 8pt;"
                                        Text="Set Contract Transaction"></asp:LinkButton>
                                </td>
                                <td style="width: 20%;">
                                    <asp:LinkButton ID="LinkButton_Dependants" runat="server" Style="font-family: Tahoma; font-size: 8pt;"
                                        CommandArgument="Dependants" meta:resourcekey="LinkButton_DependantsResource1"
                                        Text="Add Dependants"></asp:LinkButton>
                                </td>
                                <td style="width: 15%;">
                                    <asp:LinkButton ID="LinkButton_Documents" runat="server" Style="font-family: Tahoma; font-size: 8pt;"
                                        CommandArgument="Documents" meta:resourcekey="LinkButton_DocumentsResource1"
                                        Text="Add Documents"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="txtFormPermission" runat="server" />
            <asp:HiddenField ID="txtCurrentRowIndex" runat="server" />
            <asp:HiddenField ID="hdnTransactionTypeID" runat="server" Value="0" />
            <asp:HiddenField ID="txtId" runat="server" />
            <asp:HiddenField ID="txtLanguage" runat="server" />
            <asp:HiddenField ID="hdnCurrDate" runat="server" />
            <asp:HiddenField ID="hdnLang" runat="server" Value="0" />
            <asp:HiddenField ID="hdnVacationTypeID" runat="server" Value="0" />
            <asp:HiddenField ID="txtEmpId" runat="server" />
            <asp:HiddenField ID="txtContractsTransactionsPermissions" runat="server" />
            <asp:HiddenField ID="txtContractStatus" runat="server" />
            <asp:TextBox ID="txtCId" runat="server" Style="position: absolute; top: 0" TabIndex="-1"
                meta:resourcekey="txtCIdResource1"></asp:TextBox>
            <asp:HiddenField ID="txtEmpDependantId" runat="server" />
            <asp:HiddenField ID="txtDocumentsPermission" runat="server" />
            <asp:HiddenField ID="txtContractsF" runat="server" />
            <asp:HiddenField ID="txtContractsTransF" runat="server" EnableViewState="False" />
            <asp:HiddenField ID="txtDependantsF" runat="server" />
            <asp:HiddenField ID="txtDocumentsF" runat="server" />
            <asp:HiddenField ID="txtDependentDeletedID" runat="server" />
            <asp:HiddenField ID="txtActivatedRowIndex" runat="server" />
            <asp:HiddenField ID="txtContractId" runat="server" />
            <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
                meta:resourcekey="nameResource1"></asp:Label>
            <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
                TabIndex="-1" Width="99px"></asp:Label>
            <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
                Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
            <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
                meta:resourcekey="TargetControlResource1"></asp:Label>
            <asp:Label ID="Label212" runat="server" ForeColor="Red" Height="11px" Style="left: 652px; position: absolute; top: 70px"
                Width="134px" meta:resourcekey="Label21Resource1"></asp:Label>
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
                                <td style="width: 10px"></td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Back" Width="16px" Height="16px" runat="server"
                                        SkinID="HrBack_Command" CommandArgument="Previous" meta:resourcekey="ImageButton_BackResource1" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_First" Width="16px" Height="16px" runat="server"
                                        SkinID="HrFirest_Command" CommandArgument="First" meta:resourcekey="ImageButton_FirstResource1" />
                                </td>
                                <td style="width: 40px">
                                    <asp:Label ID="Label18" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Contracts" runat="server" meta:resourcekey="ImageButton_ContractsRes"
                                        Height="14px" ImageUrl="./img/discuss_16.gif" Width="14px" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_HealthInsurance" runat="server" meta:resourcekey="ImageButton_HealthInsuranceRes"
                                        Height="14px" ImageUrl="./img/Controls.bmp" Width="14px" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Transaction" runat="server" meta:resourcekey="ImageButton_TransactionRes"
                                        Height="14px" ImageUrl="./img/forum_newmsg.gif" Width="14px" />
                                </td>
                                <td style="width: 24px"></td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Dependants" runat="server" CommandArgument="Dependants"
                                        meta:resourcekey="ImageButton_DependantsRec" Height="14px" ImageUrl="./img/i.p.putingrp.gif"
                                        Width="14px" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Documents" runat="server" CommandArgument="Documents"
                                        meta:resourcekey="ImageButton_DocumentsRec" Height="16px" ImageUrl="./img/abook_add_1.gif"
                                        Width="16px" />
                                </td>
                                <td style="width: 40px">
                                    <asp:Label ID="Label213" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                                    <asp:LinkButton ID="LinkButton_Import" runat="server" CommandArgument="Import" meta:resourcekey="LinkButton_ImportResource1" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;"
                                        Text="التحقق من رقم الهوية" Visible="True"
                                        OnClientClick="OpenModal12('frmIqamaValid.aspx?FrmID=1',200,850); return false;"></asp:LinkButton>
                                </td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                        SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                    <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
                                </td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Role" Width="16px" Height="16px" runat="server"
                                        SkinID="HrRole_Command" OnClientClick="OpenModal12('CompanyRoles.aspx?FrmID=1',450,844); return false;" />
                                    <asp:LinkButton ID="LinkButton_Role" runat="server" Text="الإجراءات" meta:resourcekey="LinkButton_RoleResource1"
                                        OnClientClick="OpenModal12('CompanyRoles.aspx?FrmID=1',450,844); return false;"></asp:LinkButton>
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
                                                <asp:DropDownList ID="DropDownList1" runat="server">
                                                </asp:DropDownList>
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
                                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                                            <tr>
                                                <td style="height: 16px; vertical-align: top">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 85%; vertical-align: top;">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="height: 16px" colspan="5"></td>
                                                                        <td style="height: 16px" colspan="5">
                                                                            <asp:Label ID="lblNoContractNotify" runat="server" SkinID="Label_WarningBold" meta:resourcekey="lblNoContractNotifyResource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td style="width: 11%;">
                                                                            <asp:Label ID="lblCode" runat="server" meta:resourcekey="lblCodeResource1" SkinID="Label_DefaultNormal"
                                                                                Text="Code "></asp:Label>
                                                                        </td>
                                                                        <td style="width: 21%; text-align: center;">
                                                                            <asp:TextBox ID="txtCode" dir="ltr" runat="server" AutoPostBack="True" MaxLength="30"
                                                                                meta:resourcekey="txtCodeResource1" SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                                        </td>
                                                                        <td></td>
                                                                        <td style="width: 21%; text-align: center;">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="width: 20%;">
                                                                                        <igtxt:WebImageButton ID="btnSearchCode" runat="server" AutoSubmit="False" Height="18px"
                                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                                            Width="24px">
                                                                                            <Alignments TextImage="ImageBottom" />
                                                                                            <Appearance>
                                                                                                <Image Url="./Img/forum_search.gif" />
                                                                                            </Appearance>
                                                                                        </igtxt:WebImageButton>
                                                                                    </td>
                                                                                    <td style="width: 80%;"></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="Label7" runat="server" Text="الكود المحاسبى" SkinID="Label_CopyRightsBold"
                                                                                meta:resourcekey="lblProjectResource1"></asp:Label>
                                                                        </td>
                                                                        <td colspan="2">
                                                                            <asp:TextBox ID="txtLedgerCode" dir="ltr" runat="server" MaxLength="30"
                                                                                meta:resourcekey="txtCodeResource1" SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                                        </td>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="Label8" runat="server" Style="margin-right: 20px; width: 21%; font-size: small" Text="Supported by Taqat" SkinID="TextBox_LargeNormalC"
                                                                                meta:resourcekey="lblHasTaqatResource1"></asp:Label>
                                                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                        </td>
                                                                       
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5">
                                                                            <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                                                cellspacing="6">
                                                                                <tr>
                                                                                    <td style="vertical-align: bottom">
                                                                                        <asp:Label ID="Label_Title1" runat="server" Text="Personal information" SkinID="Label_DefaultBold"
                                                                                            meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                                                    </td>
                                                                                      
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="Label_LocationTitle" runat="server" Text="المشروع الحالى" SkinID="Label_CopyRightsBold"
                                                                                meta:resourcekey="Label_LocationResource1"></asp:Label>
                                                                        </td>
                                                                        
                                                                        <td colspan="4">
                                                                            <asp:Label ID="Label_LocationDescription" runat="server" SkinID="Label_CopyRightsNormal"></asp:Label>
                                                                             <asp:Label ID="lblflexiblesalarydist" runat="server" Style="margin-right: 20px; width: 21%; font-size: small" Text="Supported by Taqat" SkinID="TextBox_LargeNormalC"
                                                                                meta:resourcekey="lblflexiblesalarydistResource1"></asp:Label>
                                                                            <asp:CheckBox ID="chkflexiblesalarydist" runat="server" />
                                                                        </td>
                                                                        <td colspan="1">
                                                                           
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 1%; height: 16px">
                                                                            <asp:Label ID="td1" runat="server" Text=" " Width="5px" meta:resourcekey="td1Resource1"></asp:Label>
                                                                        </td>

                                                                        <td></td>
                                                                        <td style="width: 21%; text-align: center;">
                                                                            <asp:Label ID="Label1" runat="server" Text="Name" SkinID="Label_DefaultNormal" meta:resourcekey="lblNameResource1"></asp:Label>
                                                                        </td>
                                                                        <td></td>
                                                                        <td style="width: 21%; text-align: center;">
                                                                            <asp:Label ID="Label2" runat="server" SkinID="Label_DefaultNormal" Text="Father Name"
                                                                                meta:resourcekey="lblFatherNameResource1"></asp:Label>
                                                                        </td>
                                                                        <td></td>
                                                                        <td style="width: 21%; text-align: center;">
                                                                            <asp:Label ID="Label3" runat="server" SkinID="Label_DefaultNormal" Text="Grand Name"
                                                                                meta:resourcekey="lblGrandNameResource1"></asp:Label>
                                                                        </td>
                                                                        <td></td>
                                                                        <td style="width: 21%; text-align: center;">
                                                                            <asp:Label ID="Label4" runat="server" SkinID="Label_DefaultNormal" Text="Family Name"
                                                                                meta:resourcekey="lblFamilyNameResource1"></asp:Label>
                                                                        </td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 1%; height: 16px">&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEngName" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                                Text="English name" meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 20%;">
                                                                            <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEngNameResource1"></asp:TextBox>
                                                                        </td>
                                                                        <td></td>
                                                                        <td style="width: 20%;">
                                                                            <asp:TextBox ID="txtEngFathername" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEngFathernameResource1"></asp:TextBox>
                                                                        </td>
                                                                        <td></td>
                                                                        <td style="width: 20%;">
                                                                            <asp:TextBox ID="txtEngGrandName" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEngGrandNameResource1"></asp:TextBox>
                                                                        </td>
                                                                        <td></td>
                                                                        <td style="width: 20%;">
                                                                            <asp:TextBox ID="txtEngFamilyName" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEngFamilyNameResource1"></asp:TextBox>
                                                                        </td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 1%; height: 16px"></td>
                                                                        <td>
                                                                            <asp:Label ID="lblArbName" runat="server" SkinID="Label_DefaultNormal" Text="Arabic name"
                                                                                meta:resourcekey="lblArbNameResource1"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 20%;">
                                                                            <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtArbNameResource1"></asp:TextBox>
                                                                        </td>
                                                                        <td></td>
                                                                        <td style="width: 20%;">
                                                                            <asp:TextBox ID="txtArbFatherName" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtArbFatherNameResource1"></asp:TextBox>
                                                                        </td>
                                                                        <td></td>
                                                                        <td style="width: 20%;">
                                                                            <asp:TextBox ID="txtArbGrandName" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtArbGrandNameResource1"></asp:TextBox>
                                                                        </td>
                                                                        <td></td>
                                                                        <td style="width: 20%;">
                                                                            <asp:TextBox ID="txtArbFamilyName" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtArbFamilyNameResource1"></asp:TextBox>
                                                                        </td>
                                                                        <td></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 15%">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Image ID="Image1" runat="server" Height="135px" Width="135px" meta:resourcekey="Image1Resource1" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="accordion">
                                            <h3></h3>
                                            <div id="DivMainInfo">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                <td style="height: 16px; vertical-align: top">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>

                                                    <tr>
                                                        <td></td>
                                                        <td style="width: 10%;">
                                                            <asp:Label ID="lblBirthDate" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="Birth Date" meta:resourcekey="lblBirthDateResource1" Style="height: 13px"></asp:Label>
                                                        </td>
                                                        <td style="width: 39%;">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 40%;">
                                                                        <igtxt:WebMaskEdit ID="txtBirthDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default">
                                                                        </igtxt:WebMaskEdit>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblHijri2" runat="server" meta:resourcekey="lblGergResource1" SkinID="Label_CopyRightsBold"
                                                                            Text="G"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 40%;">
                                                                        <igtxt:WebMaskEdit ID="txtBirthDateH" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default">
                                                                        </igtxt:WebMaskEdit>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblHijri" runat="server" meta:resourcekey="lblHijriResource1" SkinID="Label_CopyRightsBold"
                                                                            Text="H"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 5%;"></td>
                                                                    <td style="width: 15%;">
                                                                        <asp:Label ID="Label_Age" runat="server" meta:resourcekey="lblHijriResource1" SkinID="Label_CopyRightsBold"
                                                                            Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:Label ID="td2" runat="server" Text=" " Width="24px" meta:resourcekey="td2Resource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:Label ID="lblBirthCitys" runat="server" SkinID="Label_DefaultNormal" Text="Birth City"
                                                                Width="80px" meta:resourcekey="lblBirthCitysResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 39%;">
                                                            <asp:DropDownList ID="DdlBirthCity" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="DdlBirthCityResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                   
<%--                                                        <td class="style1">  
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="211px" />  
 </td>  --%>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                      <%--  <td class="style1">  
                    <asp:Button ID="BTN_Signature" runat="server"  Text="Upload Signature" />  
                </td> --%> 
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblNationality" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="Nationality" meta:resourcekey="lblNationalityResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DdlNationality" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="DdlNationalityResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblReligion" runat="server" SkinID="Label_DefaultNormal" Text="Religion"
                                                                Width="80px" meta:resourcekey="lblReligionResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DdlReligion" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="DdlReligionResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblGender" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="Gender" meta:resourcekey="lblGenderResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DdlGender" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="DdlGenderResource1">
                                                                <asp:ListItem meta:resourcekey="ListItemResource1" Selected="True" Text="Male" Value="M"></asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="ListItemResource2" Text="Female" Value="F"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblMaritalStatus" runat="server" SkinID="Label_DefaultNormal" Text="Marital Status"
                                                                Width="80px" meta:resourcekey="lblMaritalStatusResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DdlMaritalStatus" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="DdlMaritalStatusResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblBloodGroups" runat="server" SkinID="Label_DefaultNormal" Text="Blood Group"
                                                                Width="80px" meta:resourcekey="lblBloodGroupsResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DdlBloodGroups" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="DdlBloodGroupsResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblEmail" runat="server" SkinID="Label_DefaultNormal" Text="Email"
                                                                Width="80px" meta:resourcekey="lblEmailResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmail" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMobile" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="Mobile No" meta:resourcekey="lblMobileResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMobile" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtMobileResource1"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td>
    <asp:Label ID="LblWorkEmail" runat="server" SkinID="Label_DefaultNormal" Text="Work E-Mail"
        Width="80px" meta:resourcekey="LblWorkEmailResource1"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtWorkE_Mail" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEmailResource1"></asp:TextBox>
</td>
                                                        <%--<td>
                                                            <asp:Label ID="lblPhone" runat="server" SkinID="Label_DefaultNormal" Text="Phone No"
                                                                Width="80px" meta:resourcekey="lblPhoneResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPhone" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtPhoneResource1"></asp:TextBox>
                                                        </td>--%>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                                                                                <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                       <td>
      <asp:Label ID="lblPhone" runat="server" SkinID="Label_DefaultNormal" Text="Phone No"
          Width="80px" meta:resourcekey="lblPhoneResource1"></asp:Label>
  </td>
  <td>
      <asp:TextBox ID="txtPhone" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtPhoneResource1"></asp:TextBox>
  </td>
                                                        <td></td>

                                                        <%--<td>
                                                            <asp:Label ID="lblPhone" runat="server" SkinID="Label_DefaultNormal" Text="Phone No"
                                                                Width="80px" meta:resourcekey="lblPhoneResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPhone" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtPhoneResource1"></asp:TextBox>
                                                        </td>--%>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                            </td>
                                               
                                                        </tr>
                                                </table>
                                                    </td>
                                                              <td style="width: 15%">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Image ID="Image2" runat="server" Height="135px" Width="135px" meta:resourcekey="Image1Resource1" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                    </tr>
                                                    </table>
                                            </div>
                                            <h3>
                                                <asp:Label ID="Label17" runat="server" Text="Organization information" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="Label17Resource1"></asp:Label>
                                            </h3>
                                            <div>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 10px;"></td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="lblBranch" runat="server" meta:resourcekey="lblBranchResource1" SkinID="Label_DefaultNormal"
                                                                        Text="Branch" Width="80px"></asp:Label>
                                                                </td>
                                                                <td style="width: 39%;">
                                                                    <asp:DropDownList ID="ddlBranch" runat="server" SkinID="DropDownList_LargNormal"
                                                                        AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td></td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="lblDepartment" runat="server" meta:resourcekey="lblDepartmentResource1"
                                                                        SkinID="Label_DefaultNormal" Text="Department" Width="80px"></asp:Label>
                                                                </td>
                                                                <td style="width: 39%;">
                                                                    <asp:DropDownList ID="ddlDepartment" runat="server" meta:resourcekey="ddlDepartmentResource1"
                                                                        SkinID="DropDownList_LargNormal" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 5px;"></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="lblSectors" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                        Text="Sectors" meta:resourcekey="lblSectorsResource1"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlSectors" runat="server" SkinID="DropDownList_LargNormal">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 20px;"></td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="Label_Location" runat="server" meta:resourcekey="lblLocationResource1"
                                                                        SkinID="Label_DefaultNormal" Text="Location" Width="80px"></asp:Label>
                                                                </td>
                                                                <td style="width: 39%;">
                                                                    <asp:DropDownList ID="DropDownList_Location" runat="server" meta:resourcekey="ddlLocationResource1"
                                                                        SkinID="DropDownList_LargNormal" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 5px;"></td>
                                                            </tr>

                                                             <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="LblCost1" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                        Text="Cost 1" meta:resourcekey="lblCost1Resource1"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:textbox ID="TxtCostCode1" runat="server" autopostback="true"  OnTextChanged="TxtCostCode1_TextChanged">
                                                                    </asp:textbox>
                                                                    <igtxt:WebImageButton ID="WebImageButton1" runat="server" AutoSubmit="False" Height="10px"
                                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                                            Width="20px">
                                                                                            <Alignments TextImage="ImageBottom" />
                                                                                            <Appearance>
                                                                                                <Image Url="./Img/forum_search.gif" />
                                                                                            </Appearance>
                                                                                        </igtxt:WebImageButton>
                                                                     <asp:textbox ID="TxtCostName1" runat="server"  Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
                                                                color: Black; border: solid 1px #CCCCCC" Value="" >
                                                                    </asp:textbox>
                                                                </td>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="lblCostCode2" runat="server" SkinID="Label_DefaultNormal" Text="Cost 2"
                                                                        Width="80px" meta:resourcekey="lblCost2Resource1"></asp:Label>
                                                                </td>
                                                               <td>
                                                                    <asp:textbox ID="TxtCostCode2" runat="server" autopostback="true">
                                                                    </asp:textbox>
                                                                    <igtxt:WebImageButton ID="WebImageButton2" runat="server" AutoSubmit="False" Height="10px"
                                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                                            Width="20px">
                                                                                            <Alignments TextImage="ImageBottom" />
                                                                                            <Appearance>
                                                                                                <Image Url="./Img/forum_search.gif" />
                                                                                            </Appearance>
                                                                                        </igtxt:WebImageButton>
                                                                     <asp:textbox ID="TxtCostName2" runat="server"  Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
                                                                color: Black; border: solid 1px #CCCCCC" Value="" >
                                                                    </asp:textbox>
                                                                </td>

                                                                <td></td>
                                                            </tr>
                                                         <%--   <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="lblCost11" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                        Text="Cost 1" meta:resourcekey="lblCost1Resource1"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlCost1" runat="server" SkinID="DropDownList_LargNormal">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="lblCost22" runat="server" SkinID="Label_DefaultNormal" Text="Cost 2"
                                                                        Width="80px" meta:resourcekey="lblCost2Resource1"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlCost2" runat="server" SkinID="DropDownList_LargNormal">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td></td>
                                                            </tr>--%>


                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="lblCostCode3" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                        Text="Cost 3" meta:resourcekey="lblCost3Resource1"></asp:Label>
                                                                </td>
                                                               <td>
                                                                    <asp:textbox ID="TxtCostCode3" runat="server" autopostback="true">
                                                                    </asp:textbox>
                                                                    <igtxt:WebImageButton ID="WebImageButton3" runat="server" AutoSubmit="False" Height="10px"
                                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                                            Width="20px">
                                                                                            <Alignments TextImage="ImageBottom" />
                                                                                            <Appearance>
                                                                                                <Image Url="./Img/forum_search.gif" />
                                                                                            </Appearance>
                                                                                        </igtxt:WebImageButton>
                                                                     <asp:textbox ID="TxtCostName3" runat="server" autopostback="true" Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
                                                                color: Black; border: solid 1px #CCCCCC" Value="" >
                                                                    </asp:textbox>
                                                                </td>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="lblCostCode4" runat="server" SkinID="Label_DefaultNormal" Text="Cost 4"
                                                                        Width="80px" meta:resourcekey="lblCost4Resource1"></asp:Label>
                                                                </td>
                                                         <td>
                                                                    <asp:textbox ID="TxtCostCode4" runat="server" autopostback="true">
                                                                    </asp:textbox>
                                                                    <igtxt:WebImageButton ID="WebImageButton4" runat="server" AutoSubmit="False" Height="10px"
                                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                                            Width="20px">
                                                                                            <Alignments TextImage="ImageBottom" />
                                                                                            <Appearance>
                                                                                                <Image Url="./Img/forum_search.gif" />
                                                                                            </Appearance>
                                                                                        </igtxt:WebImageButton>
                                                                     <asp:textbox ID="TxtCostName4" runat="server" autopostback="true" Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
                                                                color: Black; border: solid 1px #CCCCCC" Value="" >
                                                                    </asp:textbox>
                                                                </td>
                                                                <td></td>
                                                            </tr>


                                                          <%--  <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="lblCost33" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                        Text="Cost 3" meta:resourcekey="lblCost3Resource1"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlCost3" runat="server" SkinID="DropDownList_LargNormal">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="lblCost44" runat="server" SkinID="Label_DefaultNormal" Text="Cost 4"
                                                                        Width="80px" meta:resourcekey="lblCost4Resource1"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlCost4" runat="server" SkinID="DropDownList_LargNormal">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td></td>
                                                            </tr>--%>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <h3>
                                                <asp:Label ID="Label5" runat="server" Text="Others information" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="Label15Resource1"></asp:Label>
                                            </h3>
                                            <div>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td></td>
                                                        <td style="width: 10%;">
                                                            <asp:Label ID="lblManager" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="Manager" meta:resourcekey="lblManagerResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 39%;">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 50%;">
                                                                        <asp:TextBox ID="txtManager" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtManagerResource1"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 50%;">
                                                                        <igtxt:WebImageButton ID="btnManager" runat="server" AutoSubmit="False" Height="18px"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnManagerResource1">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/forum_search.gif" />
                                                                            </Appearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 2%;">&nbsp;
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:Label ID="lblEntry" runat="server" meta:resourcekey="lblEntryResource1" SkinID="Label_DefaultNormal"
                                                                Text="Entry No." Width="80px"></asp:Label>
                                                        </td>
                                                        <td style="width: 39%;">
                                                            <asp:TextBox ID="txtEntry" runat="server" SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblJoinDate" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="Join Date" meta:resourcekey="lblJoinDateResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <igtxt:WebMaskEdit ID="JoinDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default">
                                                            </igtxt:WebMaskEdit>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblSponsor" runat="server" SkinID="Label_DefaultNormal" Text="Sponsor"
                                                                Width="80px" meta:resourcekey="lblSponsorResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlSponsor" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="ddlSponsorResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblBank" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="Bank" meta:resourcekey="lblBankResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlBank" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="ddlBankResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblBankAccount" runat="server" SkinID="Label_DefaultNormal" Text="Bank Account"
                                                                Width="80px" meta:resourcekey="lblBankAccountResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBankAccount" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtBankAccountResource1"></asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="BankAccountTypeLabel" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="Bank Account Type" meta:resourcekey="BankAccountTypeLabelResource"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlBankAccountType" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="ddlBankResource1">
                                                                <asp:ListItem Value="0">حدد الخيار</asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="RegularAccountLabel" Value="Regular">بطاقة بنك</asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="SalaryAccountLabel" Value="Salary">بطاقة راتب</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                         <td>
     <asp:Label ID="LblPaymentType" runat="server" Width="80px" SkinID="Label_DefaultNormal"
         Text="Payment Type" meta:resourcekey="LblPaymentTypeResource"></asp:Label>
 </td>
 <td>
     <asp:DropDownList ID="ddlLblPaymentType" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="ddlBankResource1">
         <%--<asp:ListItem Value="0">حدد الخيار</asp:ListItem>
         <asp:ListItem meta:resourcekey="RegularAccountLabel" Value="Regular">بطاقة بنك</asp:ListItem>
         <asp:ListItem meta:resourcekey="SalaryAccountLabel" Value="Salary">بطاقة راتب</asp:ListItem>--%>
     </asp:DropDownList>
 </td>
                                                        <%-- <td>
                                                        <asp:Label ID="Label10" runat="server" SkinID="Label_DefaultNormal" Text="Bank Account"
                                                            Width="80px" meta:resourcekey="lblBankAccountResource1"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox1" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtBankAccountResource1"></asp:TextBox>
                                                    </td>--%>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMachineCode" runat="server" meta:resourcekey="lblMachineCodeResource1"
                                                                SkinID="Label_DefaultNormal" Text="Machine Code" Width="80px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox_MachineCode" runat="server" meta:resourcekey="TextBox_MachineCodeResource1"
                                                                SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblGosiNumber" runat="server" meta:resourcekey="lblGosiNumberResource1"
                                                                SkinID="Label_DefaultNormal" Text="GOSI Number" Width="80px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGosiNumber" runat="server" meta:resourcekey="txtGosiNumberResource1"
                                                                SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%;">&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGOSIJoinDate" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="GOSI Join Date" meta:resourcekey="lblGOSIJoinDateResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <igtxt:WebMaskEdit ID="GOSIJoinDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default">
                                                            </igtxt:WebMaskEdit>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblGOSIExcludeDate" runat="server" SkinID="Label_DefaultNormal" Text="Exclude Date"
                                                                Width="80px" meta:resourcekey="lblGOSIExcludeDateResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <igtxt:WebMaskEdit ID="GOSIExcludeDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default">
                                                            </igtxt:WebMaskEdit>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPassport" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="Passport No." meta:resourcekey="lblPassportResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPassport" runat="server" SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblIdentity" runat="server" SkinID="Label_DefaultNormal" Text="Identity No."
                                                                Width="80px" meta:resourcekey="lblIdentityResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIdentity" runat="server" SkinID="TextBox_LargeNormalC" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%;">&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblLastEducations" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="Last Educations" meta:resourcekey="lblLastEducationsResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlLastEducations" runat="server" SkinID="DropDownList_LargNormal">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblGraduationDate" runat="server" SkinID="Label_DefaultNormal" Text="Graduation Date"
                                                                Width="80px" meta:resourcekey="lblGraduationDateResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <igtxt:WebMaskEdit ID="txtGraduationDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default">
                                                            </igtxt:WebMaskEdit>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <h3>
                                                <asp:Label ID="Label6" runat="server" Text="Current contract information" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="Label6Resource1"></asp:Label>
                                            </h3>
                                            <div id="DivContract" runat="server">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:Label ID="lblCode2" runat="server" SkinID="Label_DefaultNormal" Text="Contract No"
                                                                Width="80px" meta:resourcekey="lblCode2Resource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 39%;">
                                                            <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel3" runat="server" meta:resourcekey="WebAsyncRefreshPanel3Resource1">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td class="auto-style1">
                                                                            <asp:Label ID="lblContractNo" runat="server" meta:resourcekey="lblNoContractNotifyResource1"
                                                                                SkinID="Label_WarningBold"></asp:Label>
                                                                        </td>
                                                                        <td class="auto-style1">
                                                                            <igtxt:WebImageButton ID="btnAddContract" runat="server" Height="18px" Overflow="NoWordWrap"
                                                                                UseBrowserDefaults="False" Width="24px" ToolTip="Add New Contract" meta:resourcekey="btnAddContractResource1">
                                                                                <Alignments TextImage="ImageBottom" />
                                                                                <Appearance>
                                                                                    <Image Url="./img/abook_add_1.gif" />
                                                                                </Appearance>
                                                                            </igtxt:WebImageButton>


                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </igmisc:WebAsyncRefreshPanel>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="td3" runat="server" Text=" " Width="10px" meta:resourcekey="td3Resource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:Label ID="lblStartDate" runat="server" SkinID="Label_DefaultNormal" Text="Start Date"
                                                                Width="80px" meta:resourcekey="lblStartDateResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 39%;">
                                                            <igtxt:WebMaskEdit ID="txtStartDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default">
                                                            </igtxt:WebMaskEdit>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblContractType" runat="server" SkinID="Label_DefaultNormal" Text="Contract Type"
                                                                Width="80px" meta:resourcekey="lblContractTypeResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlContractType" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="ddlContractTypeResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblEndDate" runat="server" SkinID="Label_DefaultNormal" Text="End Date"
                                                                Width="80px" meta:resourcekey="lblEndDateResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <igtxt:WebMaskEdit ID="txtEndDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default">
                                                            </igtxt:WebMaskEdit>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblProfessions" runat="server" SkinID="Label_DefaultNormal" Text="Profession"
                                                                Width="80px" meta:resourcekey="lblProfessionsResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlProfessions" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="ddlProfessionsResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblGradeStep" runat="server" SkinID="Label_DefaultNormal" Text="Grade Steps"
                                                                Width="80px" meta:resourcekey="lblGradeStepResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlGradeStep" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="ddlGradeStepResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblPosition" runat="server" SkinID="Label_DefaultNormal" Text="Position"
                                                                Width="80px" meta:resourcekey="lblPositionResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlPosition" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="ddlPositionResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblCurrency" runat="server" SkinID="Label_DefaultNormal" Text="Currency"
                                                                Width="80px" meta:resourcekey="lblCurrencyResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCurrency" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="ddlCurrencyResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblEmployeeClass" runat="server" SkinID="Label_DefaultNormal" Text="Employee Class"
                                                                Width="80px" meta:resourcekey="lblEmployeeClassResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlEmployeeClass" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="ddlEmployeeClassResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblContractDuration" runat="server" SkinID="Label_DefaultNormal" Text="Period"
                                                                Width="80px" meta:resourcekey="lblContractDurationResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <igtxt:WebNumericEdit ID="wneContractDuration" runat="server" SkinID="WebNumericEdit_Default"
                                                                meta:resourceKey="wneContractDurationResource" MinValue="1" NullText="1" ValueText="1">
                                                            </igtxt:WebNumericEdit>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
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
    <script type="text/javascript" id="igClientScript">
        $(document).ready(function () {
            var Deletebtn = $("#<%=ImageButton_Delete.ClientID%>")

            Deletebtn.click(function () {
                if (confirm("هل انت متأكد من الحذف؟") == false) {

                    return false;
                }

            })


        });

    </script>
    </form>
    </body>
</html>
