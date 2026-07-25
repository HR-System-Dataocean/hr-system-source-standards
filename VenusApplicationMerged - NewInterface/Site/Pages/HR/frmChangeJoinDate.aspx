<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmChangeJoinDate.aspx.vb" Inherits="frmChangeJoinDate"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" 
    EnableSessionState="True" %>

<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Change Join Date</title>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <style type="text/css">
        body {
            font-family: Tahoma, Arial, sans-serif;
            font-size: 12px;
        }
        [dir="rtl"] body {
            direction: rtl;
        }
        [dir="ltr"] body {
            direction: ltr;
        }
        .MainContainer {
            width: 100%;
            padding: 5px;
        }
        .Div_MasterContainer {
            width: 100%;
            padding: 5px;
        }
        [dir="rtl"] .Div_MasterContainer {
            direction: rtl;
        }
        [dir="ltr"] .Div_MasterContainer {
            direction: ltr;
        }
        .SectionHeader {
            border-bottom: 2px solid #333;
            padding: 8px 0 5px 0;
            margin: 10px 0 8px 0;
            font-weight: bold;
            font-size: 14px;
            color: #1a1a1a;
        }
        table {
            width: 100%;
        }
        [dir="rtl"] table {
            direction: rtl;
        }
        [dir="ltr"] table {
            direction: ltr;
        }
        .LabelRight {
            padding: 3px 8px 3px 5px;
            white-space: nowrap;
            font-weight: normal;
            font-size: 12px;
        }
        [dir="rtl"] .LabelRight {
            text-align: right;
        }
        [dir="ltr"] .LabelRight {
            text-align: left;
        }
        .DataRight {
            padding: 3px 5px;
        }
        [dir="rtl"] .DataRight {
            text-align: right;
        }
        [dir="ltr"] .DataRight {
            text-align: left;
        }
        .ReadOnlyBox {
            background-color: #F0F0F0;
            border: 1px solid #CCCCCC;
            padding: 2px 4px;
            font-family: Tahoma;
            font-size: 11px;
            height: 20px;
        }
        .TextBoxNormal {
            border: 1px solid #CCCCCC;
            padding: 2px 4px;
            font-family: Tahoma;
            font-size: 11px;
            height: 20px;
        }
        .ClassChangeBox {
            background-color: #E8F4FD;
            border: 1px solid #4A90D9;
            border-radius: 5px;
            padding: 15px;
            margin: 5px 0;
        }
        .DueBalanceBox {
            background-color: #E8F5E9;
            border: 1px solid #4CAF50;
            border-radius: 5px;
            padding: 10px 15px;
            margin: 5px 0;
            font-weight: bold;
            font-size: 13px;
            color: #1a5a96;
            text-align: center;
        }
        .RequiredStar {
            color: #FF0000;
            font-weight: bold;
            font-size: 14px;
        }
        .ReasonTextArea {
            width: 95%;
            max-width: 450px;
            height: 55px;
            font-family: Tahoma;
            font-size: 11px;
            border: 1px solid #CCCCCC;
            padding: 3px;
        }
        .MessageSuccess {
            color: Green;
            font-weight: bold;
            padding: 5px;
        }
        .MessageError {
            color: Red;
            font-weight: bold;
            padding: 5px;
        }
        .SearchButton {
            width: 30px;
            text-align: center;
        }
        .EmptyCell {
            width: 20px;
        }
        .MainTable {
            width: 100%;
            border-collapse: collapse;
        }
        .MainTable td {
            padding: 2px;
            vertical-align: middle;
        }
        .SeparArea {
            width: 5px;
        }
        .LabelArea {
            padding-left: 5px;
            padding-right: 5px;
            white-space: nowrap;
        }
        [dir="rtl"] .LabelArea {
            text-align: right;
        }
        [dir="ltr"] .LabelArea {
            text-align: left;
        }
        .DataArea {
            padding-left: 5px;
            padding-right: 5px;
        }
        [dir="rtl"] .DataArea {
            text-align: right;
        }
        [dir="ltr"] .DataArea {
            text-align: left;
        }
        .DataAreawithsearch {
            padding-left: 2px;
            padding-right: 2px;
        }
        [dir="rtl"] .DataAreawithsearch {
            text-align: right;
        }
        [dir="ltr"] .DataAreawithsearch {
            text-align: left;
        }
        .search {
            width: 30px;
            text-align: center;
        }
        .ClassChangeOption {
            margin: 5px 0;
        }
        [dir="rtl"] .ClassChangeOption {
            text-align: right;
        }
        [dir="ltr"] .ClassChangeOption {
            text-align: left;
        }
        .SeparArea {
            width: 5px;
        }
    </style>

    <script type="text/javascript">
        // =============================================
        // Modal Search Dialog
        // =============================================
        var ODialoge;
        var OSender;

        function OpenModal1(pageurl, height, width, CheckID, CheckContract, SenderCtrl) {
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

        function CloseIt(retvalue) {
            if (retvalue != "") {
                var Sender = window.document.getElementById(OSender);
                if (Sender) {
                    Sender.value = retvalue;
                    Sender.focus();
                    if (Sender.onchange) {
                        Sender.onchange();
                    } else {
                        __doPostBack(Sender.name, '');
                    }
                }
            }
            if (ODialoge) {
                ODialoge.dialog('close');
            }
        }

        // =============================================
        // تعريف ClientID للعناصر المهمة (سيتم تعبئتها من Server)
        // =============================================
        var ids = {};

        // =============================================
        // قسم تغيير الفئة - ترحيل الرصيد
        // =============================================
        function CheckClassChange() {
            var ddlClass = document.getElementById(ids.ddlNewClass);
            var hdnCurrentClass = document.getElementById(ids.hdnCurrentClassID);
            var divSection = document.getElementById('divClassChangeVacation');

            if (!ddlClass || !divSection) return;

            var selectedClassID = ddlClass.value;
            var currentClassID = hdnCurrentClass ? hdnCurrentClass.value : '';

            var classChanged = (selectedClassID != '' && selectedClassID != '0' && selectedClassID != currentClassID);

            if (classChanged) {
                divSection.style.display = 'block';
            } else {
                divSection.style.display = 'none';
                var chkTransfer = document.getElementById(ids.chkTransferBalance);
                if (chkTransfer) chkTransfer.checked = false;
                document.getElementById('divTransferExpireDate').style.display = 'none';
            }
        }

        function ToggleTransferExpireDate() {
            var chkTransfer = document.getElementById(ids.chkTransferBalance);
            var divExpireDate = document.getElementById('divTransferExpireDate');

            if (chkTransfer && chkTransfer.checked) {
                divExpireDate.style.display = 'block';
            } else {
                divExpireDate.style.display = 'none';
                var txtDate = document.getElementById(ids.txtTransferExpireDate);
                if (txtDate) txtDate.value = '';
            }
        }

        // =============================================
        // Validate Form
        // =============================================
        function ValidateForm() {
            var isValid = true;
            var errorMsg = '';

            // كود الموظف إجباري
            var empCode = document.getElementById(ids.txtEmployeeCode).value;
            if (empCode == '') {
                errorMsg += '- ' + document.getElementById(ids.lblEmployeeCodeMsg).innerHTML + '\n';
                isValid = false;
            }

            // لازم يكون فيه تغيير في تاريخ المباشرة أو الفئة
            var currentJoinDate = document.getElementById(ids.txtCurrentJoinDate).value;
            var newJoinDateVal = document.getElementById(ids.txtNewJoinDate).value;
            var hdnCurrentClass = document.getElementById(ids.hdnCurrentClassID);
            var ddlClass = document.getElementById(ids.ddlNewClass);
            var selectedClassID = ddlClass ? ddlClass.value : '';
            var currentClassID = hdnCurrentClass ? hdnCurrentClass.value : '';

            var classChanged = (selectedClassID != '' && selectedClassID != '0' && selectedClassID != currentClassID);
            var joinDateChanged = (newJoinDateVal != '' && newJoinDateVal != currentJoinDate);

            if (!classChanged && !joinDateChanged) {
                errorMsg += '- برجاء تغيير تاريخ المباشرة أو الفئة / Please change Join Date or Class\n';
                isValid = false;
            }

            // سبب التغيير إجباري دايماً
            var reasonJoin = document.getElementById(ids.txtReasonJoinDate).value;
            if (reasonJoin == '') {
                errorMsg += '- ' + document.getElementById(ids.lblReasonJoinDateMsg).innerHTML + '\n';
                isValid = false;
            }

            // لو الفئة اتغيرت واختار ترحيل الرصيد، تاريخ الانتهاء إجباري
            if (classChanged) {
                var chkTransfer = document.getElementById(ids.chkTransferBalance);
                if (chkTransfer && chkTransfer.checked) {
                    var expireDate = document.getElementById(ids.txtTransferExpireDate).value;
                    if (expireDate == '') {
                        errorMsg += '- ' + document.getElementById(ids.lblTransferExpireDateMsg).innerHTML + '\n';
                        isValid = false;
                    }
                }
            }

            if (!isValid) {
                var title = document.getElementById(ids.lblValidationTitle).innerHTML;
                alert(title + ':\n' + errorMsg);
                return false;
            }

            var confirmMsg = document.getElementById(ids.lblConfirmMsg).innerHTML;
            return confirm(confirmMsg);
        }

        // =============================================
        // تحديث اتجاه الصفحة
        // =============================================
        function SetPageDirection(dir) {
            document.documentElement.dir = dir;
            document.body.dir = dir;
            var div = document.getElementById('DIV');
            if (div) div.dir = dir;
            $('table').each(function () {
                $(this).attr('dir', dir);
            });
        }

        // =============================================
        // تهيئة الصفحة
        // =============================================
        $(document).ready(function () {
            // تعيين الاتجاه من القيمة المرسلة من Server
            SetPageDirection(pageDirection);

            // تعيين Client IDs من القيم المرسلة من Server
            ids.txtEmployeeCode = txtEmployeeCodeID;
            ids.txtEmployeeName = txtEmployeeNameID;
            ids.txtCurrentJoinDate = txtCurrentJoinDateID;
            ids.txtLastSalary = txtLastSalaryID;
            ids.txtCurrentClass = txtCurrentClassID;
            ids.txtCurrentBalance = txtCurrentBalanceID;
            ids.txtAnnualVacation = txtAnnualVacationID;
            ids.txtAnnualExpireDate = txtAnnualExpireDateID;
            ids.txtTransferredVacation = txtTransferredVacationID;
            ids.txtTransferredExpireDate = txtTransferredExpireDateID;
            ids.txtNewJoinDate = txtNewJoinDateID;
            ids.txtReasonJoinDate = txtReasonJoinDateID;
            ids.txtTransferExpireDate = txtTransferExpireDateID;
            ids.ddlNewClass = ddlNewClassID;
            ids.chkTransferBalance = chkTransferBalanceID;
            ids.hdnCurrentClassID = hdnCurrentClassIDID;
            ids.lblEmployeeCodeMsg = lblEmployeeCodeMsgID;
            ids.lblNewJoinDateMsg = lblNewJoinDateMsgID;
            ids.lblReasonJoinDateMsg = lblReasonJoinDateMsgID;
            ids.lblTransferExpireDateMsg = lblTransferExpireDateMsgID;
            ids.lblValidationTitle = lblValidationTitleID;
            ids.lblConfirmMsg = lblConfirmMsgID;

            $('#' + ids.ddlNewClass).change(function () {
                CheckClassChange();
            });
            $('#' + ids.chkTransferBalance).click(function () {
                ToggleTransferExpireDate();
            });
            CheckClassChange();
        });
    </script>

</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmChangeJoinDate" runat="server">

        <!-- رسائل للـ JavaScript (مخبأة) -->
        <div style="display: none">
            <asp:Label ID="lblEmployeeCodeMsg" runat="server" Text="Employee Code is required / كود الموظف مطلوب"></asp:Label>
            <asp:Label ID="lblNewJoinDateMsg" runat="server" Text="New Join Date is required / تاريخ المباشرة الجديد مطلوب"></asp:Label>
            <asp:Label ID="lblReasonJoinDateMsg" runat="server" Text="Reason is required / سبب التغيير مطلوب"></asp:Label>
            <asp:Label ID="lblValidationTitle" runat="server" Text="Please complete the following data / الرجاء إكمال البيانات التالية"></asp:Label>
            <asp:Label ID="lblConfirmMsg" runat="server" Text="Are you sure? / هل أنت متأكد؟"></asp:Label>
            <asp:Label ID="lblTransferExpireDateMsg" runat="server" Text="Please enter the transfer expire date / برجاء إدخال تاريخ انتهاء الرصيد المرحل"></asp:Label>

            <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
            <asp:Label ID="realname" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
            <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1" Width="91px"></asp:TextBox>
            <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
            <asp:HiddenField ID="hdnCurrentClassID" runat="server" />
            <asp:HiddenField ID="hdnEmployeeID" runat="server" />
            <asp:HiddenField ID="hdnDueBalance" runat="server" />
            <asp:HiddenField ID="TxtHDJoinDate" runat="server" />
            <asp:HiddenField ID="TxtHDClassID" runat="server" />

        </div>

        <div class="Div_MasterContainer" runat="server" id="DIV">
            <table align="center" style="width: 100%;">
                <tr>
                    <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                        <!-- شريط الأدوات -->
                        <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                            <tr>
                                <td style="display: none">
                                    <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                        SkinID="HrSave_Command" meta:resourcekey="ImageButton_SaveResource1" CommandArgument="Save"
                                        OnClientClick="return ValidateForm();" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                        meta:resourcekey="ImageButton_NewResource1" CommandArgument="New" />
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

                        <!-- Header -->
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
                                <igtab:Tab Text="تغيير تاريخ المباشرة" meta:resourcekey="TabResource2">
                                    <ContentTemplate>
                                        <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top" cellspacing="0">
                                            <tr>
                                                <td style="height: 10px" colspan="4"></td>
                                            </tr>

                                            <!-- كود الموظف -->
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblEmployeeCode" runat="server" Text="كود الموظف" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblEmployeeCodeResource1"></asp:Label>
                                                                <asp:Label ID="Label_Star1" runat="server" Text="*" Style="color: #FF0000"></asp:Label>
                                                            </td>
                                                            <td class="DataAreawithsearch">
                                                                <asp:TextBox ID="txtEmployeeCode" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                    AutoPostBack="True" meta:resourcekey="txtEmployeeCodeResource1"></asp:TextBox>
                                                            </td>
                                                            <td class="search">
                                                               <%-- <igtxt:WebImageButton ID="btnSearchEmployee" runat="server" AutoSubmit="False" Height="18px"
                                                                    Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnSearchEmployeeResource1">
                                                                    <Alignments TextImage="ImageBottom" />
                                                                    <Appearance>
                                                                        <Image Url="./Img/forum_search.gif" />
                                                                    </Appearance>
                                                                </igtxt:WebImageButton>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                            </tr>

                                            <!-- اسم الموظف -->
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblEmployeeName" runat="server" Text="اسم الموظف" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblEmployeeNameResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtEmployeeName" runat="server" SkinID="TextBox_LargeNormalltr" 
                                                                    ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtEmployeeNameResource1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                            </tr>

                                            <!-- معلومات الموظف الحالية -->
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Label ID="lblCurrentInfo" runat="server" meta:resourcekey="lblCurrentInfoResource1"
                                                                    SkinID="Label_DefaultBold" Text="معلومات الموظف الحالية"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                            </tr>

                                            <!-- تاريخ المباشرة الحالي + آخر راتب -->
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblCurrentJoinDate" runat="server" Text="تاريخ المباشرة الحالي" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblCurrentJoinDateResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtCurrentJoinDate" runat="server" SkinID="TextBox_LargeNormalltr" 
                                                                    ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtCurrentJoinDateResource1"></asp:TextBox>
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
                                                                <asp:Label ID="lblLastSalary" runat="server" Text="آخر راتب تم صرفه" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblLastSalaryResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtLastSalary" runat="server" SkinID="TextBox_LargeNormalltr" 
                                                                    ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtLastSalaryResource1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <!-- الفئة الحالية + الرصيد الحالي -->
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblCurrentClass" runat="server" Text="الفئة الحالية" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblCurrentClassResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtCurrentClass" runat="server" SkinID="TextBox_LargeNormalltr" 
                                                                    ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtCurrentClassResource1"></asp:TextBox>
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
                                                                <asp:Label ID="lblCurrentBalance" runat="server" Text="الرصيد الحالي" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblCurrentBalanceResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtCurrentBalance" runat="server" SkinID="TextBox_LargeNormalltr" 
                                                                    ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtCurrentBalanceResource1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <!-- الرصيد السنوي -->
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea" style="width: 22.5%;">
                                                                <asp:Label ID="lblAnnualVacation" runat="server" Text="الرصيد السنوي" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblAnnualVacationResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea" style="width: 30%;">
                                                                <asp:TextBox ID="txtAnnualVacation" runat="server" SkinID="TextBox_SmalltNormalC" 
                                                                    ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtAnnualVacationResource1"></asp:TextBox>
                                                            </td>
                                                            <td class="LabelArea" style="width: 20%;">
                                                                <asp:Label ID="lblAnnualExpireDate" runat="server" Text="تاريخ الانتهاء" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblAnnualExpireDateResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea" style="width: 30%;">
                                                                <asp:TextBox ID="txtAnnualExpireDate" runat="server" SkinID="TextBox_SmalltNormalC" 
                                                                    ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtAnnualExpireDateResource1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea" style="width: 22.5%;">
                                                                <asp:Label ID="lblTransferredVacation" runat="server" Text="الرصيد المرحل" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblTransferredVacationResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea" style="width: 30%;">
                                                                <asp:TextBox ID="txtTransferredVacation" runat="server" SkinID="TextBox_SmalltNormalC" 
                                                                    ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtTransferredVacationResource1"></asp:TextBox>
                                                            </td>
                                                            <td class="LabelArea" style="width: 20%;">
                                                                <asp:Label ID="lblTransferredExpireDate" runat="server" Text="تاريخ الانتهاء" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblTransferredExpireDateResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea" style="width: 30%;">
                                                                <asp:TextBox ID="txtTransferredExpireDate" runat="server" SkinID="TextBox_SmalltNormalC" 
                                                                    ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtTransferredExpireDateResource1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <!-- الرصيد المستحق حتى اليوم -->
                                            <tr>
                                                <td style="width: 100%;" colspan="4">
                                                    <div class="DueBalanceBox">
                                                        <asp:Label ID="lblDueBalanceTitle" runat="server" Text="الرصيد المستحق حتى اليوم: " 
                                                            meta:resourcekey="lblDueBalanceTitleResource1"></asp:Label>
                                                        <asp:Label ID="lblDueBalance" runat="server" Text="0.00" 
                                                            meta:resourcekey="lblDueBalanceResource1"></asp:Label>
                                                        <asp:Label ID="lblDueBalanceUnit" runat="server" Text=" يوم" 
                                                            meta:resourcekey="lblDueBalanceUnitResource1"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>

                                            <!-- البيانات الجديدة -->
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Label ID="lblNewData" runat="server" meta:resourcekey="lblNewDataResource1"
                                                                    SkinID="Label_DefaultBold" Text="البيانات الجديدة"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                            </tr>

                                            <!-- تاريخ المباشرة الجديد + الفئة الجديدة -->
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea"></td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblNewJoinDate" runat="server" Text="تاريخ المباشرة الجديد" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblNewJoinDateResource1"></asp:Label>
                                                                <asp:Label ID="Label_Star2" runat="server" Text="*" Style="color: #FF0000"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igsch:WebDateChooser ID="txtNewJoinDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                    BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma;
                                                                    font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                    Width="130px">
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
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblNewClass" runat="server" Text="الفئة الجديدة" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblNewClassResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlNewClass" runat="server" SkinID="DropDownList_LargNormal" 
                                                                    Width="100%" AutoPostBack="True" meta:resourcekey="ddlNewClassResource1">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <!-- قسم ترحيل الرصيد -->
                                            <tr>
                                                <td style="width: 100%;" colspan="4">
                                                    <div id="divClassChangeVacation" class="ClassChangeBox" style="display: none;">
                                                        <table style="width: 100%;" cellspacing="5">
                                                            <tr>
                                                                <td colspan="2" style="font-weight: bold; color: #1a5a96;">
                                                                    <asp:Label ID="lblClassChangeMessage" runat="server"
                                                                        Text="سيتم تغيير الفئة. هل تريد ترحيل الرصيد المستحق حتى اليوم؟"
                                                                        meta:resourcekey="lblClassChangeMessageResource1"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:CheckBox ID="chkTransferBalance" runat="server"
                                                                        Text="ترحيل الرصيد المستحق حتى اليوم"
                                                                        onclick="ToggleTransferExpireDate();"
                                                                        meta:resourcekey="chkTransferBalanceResource1" />
                                                                </td>
                                                            </tr>
                                                            <!-- تاريخ انتهاء الرصيد المرحل -->
                                                            <tr>
                                                                <td colspan="2" id="divTransferExpireDate" style="display: none; margin-top: 8px;">
                                                                    <table style="width: 100%;" cellspacing="5">
                                                                        <tr>
                                                                            <td style="width: 25%; font-weight: bold; vertical-align: top; text-align: right;">
                                                                                <asp:Label ID="lblTransferExpireDate" runat="server"
                                                                                    Text="تاريخ انتهاء الرصيد المرحل:"
                                                                                    meta:resourcekey="lblTransferExpireDateResource1"></asp:Label>
                                                                                <asp:Label ID="Label_Star5" runat="server" Text="*" Style="color: #FF0000"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 75%; text-align: right;">
                                                                                <igsch:WebDateChooser ID="txtTransferExpireDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                                    BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma;
                                                                                    font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                                    Width="130px"
                                                                                    meta:resourcekey="txtTransferExpireDateResource1">
                                                                                </igsch:WebDateChooser>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>

                                            <!-- سبب التغيير -->
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Label ID="lblReasonSection" runat="server" meta:resourcekey="lblReasonSectionResource1"
                                                                    SkinID="Label_DefaultBold" Text="سبب التغيير"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                            </tr>

                                            <tr>
                                                <td style="width: 100%;" colspan="4">
                                                    <table style="width: 100%;" cellspacing="5">
                                                        <tr>
                                                            <td style="width: 15%; font-weight: bold; vertical-align: top; padding: 5px; text-align: right;">
                                                                <asp:Label ID="lblReasonJoinDate" runat="server" 
                                                                    Text="سبب التغيير:"
                                                                    SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblReasonJoinDateResource1"></asp:Label>
                                                                <asp:Label ID="Label_Star4" runat="server" Text="*" Style="color: #FF0000"></asp:Label>
                                                            </td>
                                                            <td style="width: 85%; padding: 5px; text-align: right;">
                                                                <asp:TextBox ID="txtReasonJoinDate" runat="server" 
                                                                    TextMode="MultiLine" SkinID="TextBox_LargeNormalrtl" 
                                                                    Rows="3" Width="100%"
                                                                    meta:resourcekey="txtReasonJoinDateResource1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <!-- رسائل النتائج -->
                                            <tr>
                                                <td style="width: 100%;" colspan="4">
                                                    <asp:Label ID="lblMessage" runat="server" SkinID="Label_DefaultBold" ForeColor="Green" />
                                                    <asp:Label ID="lblErrorMessage" runat="server" SkinID="Label_DefaultBold" ForeColor="Red" />
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

 