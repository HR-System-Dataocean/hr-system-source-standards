<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeesDocuments.aspx.vb"
    Inherits="frmEmployeesDocuments" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>


<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Expired Documents</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
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
        $(function () {
            try {
                $(".Validators").Float();
            }
            catch (err) { }
        });
    </script>
    <script type="text/javascript">
        function validateFileUpload(input) {
            var filePath = input.value;
            if (filePath.lastIndexOf('.') === -1) {
                alert("Please select a valid file");
                input.value = ''; // مسح الملف المختار
                return false;
            }
            return true;
        }
</script>
</head>
<body style="height: 389px; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmEmployeesDocuments" runat="server">
        e<div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
        <asp:HiddenField ID="txtFormPermission" runat="server" />
        <asp:HiddenField ID="txtID" runat="server" />
        <asp:HiddenField ID="txtEmpID" runat="server" />
        <asp:HiddenField ID="txtDocumentID" runat="server" />
        <asp:HiddenField ID="txtObjectId" runat="server" />
        <asp:HiddenField ID="txtRecordID" runat="server" />
        <asp:Label ID="lblArbName" runat="server" meta:resourcekey="lblArbNameResource1"
            SkinID="Label_DefaultNormal" Text="Att. Arabic Name" Width="95px"></asp:Label>
        <asp:TextBox ID="txtDocumentArbName" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalrtl"
            meta:resourcekey="txtDocumentArbNameResource1"></asp:TextBox>
        <asp:Label ID="lblEngName" runat="server" meta:resourcekey="lblEngNameResource1"
            SkinID="Label_DefaultNormal" Text="Att. English Name" Width="95px"></asp:Label>
        <asp:TextBox ID="txtDocumentEngName" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalltr"
            meta:resourcekey="txtDocumentEngNameResource1"></asp:TextBox>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td class="Details">
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                        meta:resourcekey="UltraWebTab1Resource1" Height="90px">
                        <Tabs>
                            <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px">
                                                <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                    <tr>
                                                        <td style="width: 5px">
                                                        </td>
                                                        <td style="width: 40px; text-align: center;">
                                                            <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                                                SkinID="HrSave_Command" CommandArgument="Save" meta:resourcekey="btnSaveResource1"
                                                                ValidationGroup="G" OnClientClick="SaveOtherFieldsData();" />
                                                        </td>
                                                        <td style="width: 5px">
                                                            <asp:Label ID="Label_TSP3" runat="server" meta:resourcekey="Label_TSP1Resource1"
                                                                Text="|"></asp:Label>
                                                        </td>
                                                        <td style="width: 40px; text-align: center;">
                                                            <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                                                CommandArgument="New" meta:resourcekey="btnNewResource1" />
                                                        </td>
                                                        <td style="width: 5px">
                                                            <asp:Label ID="Label7" runat="server" meta:resourcekey="Label_TSP1Resource1" Text="|"></asp:Label>
                                                        </td>
                                                        <td style="width: 40px; text-align: center;">
                                                            <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server"
                                                                SkinID="HrDelete_Command" CommandArgument="Delete" meta:resourcekey="btnDeleteResource1" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 18px">
                                                <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                    <tr>
                                                        <td style="width: 5px">
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblUser" runat="server" Width="40px" SkinID="Label_CopyRightsBold"
                                                                Text="Code" meta:resourcekey="lblUserResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="lblDescEmployeeCode" runat="server" AutoPostBack="True" BackColor="#DCE8F6"
                                                                BorderColor="#DCE8F6" BorderStyle="Solid" BorderWidth="1px" Height="16px" Style="font-family: Tahoma;
                                                                font-size: 7pt; font-weight: normal; color: Gray; vertical-align: middle; text-align: center"
                                                                Width="80px" Enabled="False"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblName" runat="server" SkinID="Label_CopyRightsBold" Text="Name"
                                                                Width="80px" meta:resourcekey="lblNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 40%;">
                                                            <asp:Label ID="lblDescEnglishName" runat="server" Width="100%" SkinID="Label_CopyRightsNormal"
                                                                meta:resourcekey="lblDescEnglishNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 60%">
                                                            <asp:Label ID="lblMSG" runat="server" Width="100%" SkinID="Label_WarningBold" meta:resourcekey="lblMSGResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; height: 83%; vertical-align: top" cellspacing="0">
                                        <tr>
    <td style="width: 47%; height: 16px; vertical-align: top">
        <table style="width: 100%; vertical-align: top" cellspacing="0">
            <tr>
                <td class="SeparArea">
                </td>
                <td class="LabelArea">
                     <asp:Label ID="lblDocumentTypesGroup" runat="server"  Width="95px" SkinID="Label_DefaultNormal"
     Text="DocumentTypesGroup" meta:resourcekey="lblDocumentTypesGroupResource1"></asp:Label>
                </td>
                <td class="DataArea">
                   <asp:DropDownList ID="ddlDocumentTypesGroup"  runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="ddlDocumentTypesGroupResource1" AutoPostBack="True">
</asp:DropDownList>
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
    <td style="width: 5px;">
    </td>
</tr>
                                        
                                        
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblCode" runat="server" Width="95px" Text="Document Type" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlDocumentType" runat="server" SkinID="DropDownList_LargNormal">
                                                            </asp:DropDownList>
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
                                                           <asp:Label ID="lblReferenceNumber" runat="server" Width="95px" SkinID="Label_DefaultNormal"
                                                                Text="Reference Number" meta:resourcekey="lblReferenceNumberResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                             <asp:TextBox ID="txtReferenceNumber" runat="server" meta:resourcekey="TextBox_MachineCodeResource1"
                                                                        SkinID="TextBox_LargeNormalC" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 5px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblDocumentNumber0" runat="server" Width="95px" SkinID="Label_DefaultNormal"
                                                                Text="Document Number" meta:resourcekey="lblDocumentNumber0Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                                                            </asp:ScriptManager>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="txtDocumentNumber0" runat="server" meta:resourcekey="TextBox_MachineCodeResource1"
                                                                        SkinID="TextBox_LargeNormalC" AutoPostBack="True"></asp:TextBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
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
                                                            <asp:Label ID="lblAttachedFile" runat="server" meta:resourcekey="lblAttachedFileResource1"
                                                                SkinID="Label_DefaultNormal" Text="Attached File" Width="95px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">

                                                            <asp:FileUpload ID="txtAttachedFile" accept=".doc, .docx, .pdf, .xls, .xlsx, .jpg, .jpeg, .png, .tiff, .ppt, .pptx, .txt, .rtf" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="18px" meta:resourcekey="txtAttachedFileResource1" Style="font-family: Tahoma;
                                                                font-size: 8pt; font-weight: Normal; color: black;" Width="100%"  onchange="validateFileUpload(this)" />
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
                                                            <asp:Label ID="lblIssuedCity" runat="server" meta:resourcekey="lblIssuedCityResource1"
                                                                SkinID="Label_DefaultNormal" Text="Issued City" Width="95px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DdlIssuedCity" runat="server" meta:resourcekey="DdlIssuedCityResource1"
                                                                SkinID="DropDownList_LargNormal">
                                                            </asp:DropDownList>
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
                                                            <asp:HyperLink ID="lnkDownload" runat="server" meta:resourcekey="lnkDownloadResource1"
                                                                Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black"
                                                                Target="_blank" Text="Download Attached" Width="95px"></asp:HyperLink>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="DdlAttachedFiles" runat="server" meta:resourcekey="DdlAttachedFilesResource1"
                                                                            SkinID="DropDownList_LargNormal">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 24px;">
                                                                        <igtxt:WebImageButton ID="btnDeleteAttachment" runat="server" Height="16px" meta:resourcekey="btnDeleteResource1"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="~/Pages/HR/Img/logoff_small.gif" />
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
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblIssueDate" runat="server" meta:resourcekey="lblIssueDateResource1"
                                                                SkinID="Label_DefaultNormal" Text="Issue Date" Width="95px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtIssueDate" runat="server" InputMask="##/##/####" meta:resourcekey="txtIssueDateResource1"
                                                                SkinID="WebMaskEdit_Fix">
                                                            </igtxt:WebMaskEdit>
                                                            &nbsp;<asp:Label ID="lblHijri2" runat="server" meta:resourcekey="lblGergResource1"
                                                                SkinID="Label_CopyRightsBold" Text="(Gerg)"></asp:Label>
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
                                                            <asp:Label ID="lblIssueDate0" runat="server" meta:resourcekey="lblIssueDateResource1"
                                                                SkinID="Label_DefaultNormal" Text="Issue Date" Width="95px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtIssueDateH" runat="server" InputMask="##/##/####" meta:resourcekey="txtIssueDateHResource1"
                                                                SkinID="WebMaskEdit_Fix">
                                                            </igtxt:WebMaskEdit>
                                                            &nbsp;<asp:Label ID="lblHijri" runat="server" meta:resourcekey="lblHijriResource1"
                                                                SkinID="Label_CopyRightsBold" Text="(Hijri)"></asp:Label>
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
                                                            <asp:Label ID="lblExpiryDate" runat="server" Width="95px" SkinID="Label_DefaultNormal"
                                                                Text="Expiry Date" meta:resourcekey="lblExpiryDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtExpiryDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix"
                                                                meta:resourcekey="txtExpiryDateResource1">
                                                            </igtxt:WebMaskEdit>
                                                            &nbsp;<asp:Label ID="lblHijri3" runat="server" meta:resourcekey="lblGergResource1"
                                                                SkinID="Label_CopyRightsBold" Text="(Gerg)"></asp:Label>
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
                                                            <asp:Label ID="lblExpiryDate0" runat="server" meta:resourcekey="lblExpiryDateResource1"
                                                                SkinID="Label_DefaultNormal" Text="Expiry Date" Width="95px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtExpiryDateH" runat="server" InputMask="##/##/####" meta:resourcekey="txtExpiryDateHResource1"
                                                                SkinID="WebMaskEdit_Fix">
                                                            </igtxt:WebMaskEdit>
                                                            &nbsp;<asp:Label ID="lblHijri0" runat="server" meta:resourcekey="lblHijriResource1"
                                                                SkinID="Label_CopyRightsBold" Text="(Hijri)"></asp:Label>
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
                                                            <asp:Label ID="lblLastRenewalDate" runat="server" Width="95px" SkinID="Label_DefaultNormal"
                                                                Text="Last Renewal" meta:resourcekey="lblLastRenewalDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtLastRenewalDate" runat="server" InputMask="##/##/####"
                                                                SkinID="WebMaskEdit_Fix" meta:resourcekey="txtLastRenewalDateResource1">
                                                            </igtxt:WebMaskEdit>
                                                            &nbsp;<asp:Label ID="lblHijri4" runat="server" meta:resourcekey="lblGergResource1"
                                                                SkinID="Label_CopyRightsBold" Text="(Gerg)"></asp:Label>
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
                                                            <asp:Label ID="lblLastRenewalDate0" runat="server" meta:resourcekey="lblLastRenewalDateResource1"
                                                                SkinID="Label_DefaultNormal" Text="Last Renewal" Width="95px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtLastRenewalDateH" runat="server" InputMask="##/##/####"
                                                                meta:resourcekey="txtLastRenewalDateHResource1" SkinID="WebMaskEdit_Fix">
                                                            </igtxt:WebMaskEdit>
                                                            &nbsp;<asp:Label ID="lblHijri1" runat="server" meta:resourcekey="lblHijriResource1"
                                                                SkinID="Label_CopyRightsBold" Text="(Hijri)"></asp:Label>
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
                                                            <asp:Label ID="Label_Title1" runat="server" Text="برجاء تحديد الجنسيات المرتبطة"
                                                                SkinID="Label_DefaultBold" meta:resourcekey="Label_Title1Resource1"></asp:Label>
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
                                            <td style="height: 100%; vertical-align: top" colspan="3">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="height: 100%; vertical-align: top" colspan="3">
                                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="UwgEmployeeDocuments" runat="server" EnableAppStyling="True"
                                                                Height="100%" Width="100%" SkinID="Default" meta:resourcekey="uwgForNationalityResource1">
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                                                    HeaderClickActionDefault="SortSingle" Name="uwgForNationality" RowHeightDefault="18px"
                                                                    SelectTypeRowDefault="Extended" StationaryMargins="Header" AutoGenerateColumns="False"
                                                                    StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                        Width="98%">
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
                                                                    <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
                                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                                        </AddNewRow>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Decimal" Hidden="True" Key="ID" meta:resourcekey="UltraGridColumnResource1">
                                                                                <Header Caption="ID">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="DocumentID" DataType="System.Decimal" Key="DocumentID" Type="DropDownList"
                                                                                Width="30%" meta:resourcekey="UltraGridColumnResource2">
                                                                                <Header Caption="Document">
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ObjectID" Hidden="True" DataType="System.Decimal" Key="ObjectID" meta:resourcekey="UltraGridColumnResource3">
                                                                                <Header Caption="ObjectID">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="RecordID" Hidden="True" Key="RecordID" DataType="System.Decimal" meta:resourcekey="UltraGridColumnResource4">
                                                                                <Header Caption="RecordID">
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="DocumentNumber" DataType="System.Decimal" Key="DocumentNumber" Width="25%"
                                                                                meta:resourcekey="UltraGridColumnResource5">
                                                                                <Header Caption="DocumentNumber">
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="IssueDate" Hidden="False" DataType="System.Date"
                                                                                Format="dd/MM/yyyy" Key="IssueDate" Width="15%" meta:resourcekey="UltraGridColumnResource6">
                                                                                <Header Caption="IssueDate">
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="IssuedCityID" DataType="System.Decimal" Hidden="True" Key="IssuedCityID"
                                                                                meta:resourcekey="UltraGridColumnResource7">
                                                                                <Header Caption="IssuedCityID">
                                                                                    <RowLayoutColumnInfo OriginX="6" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="6" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="LastRenewalDate" Hidden="True" DataType="System.Date"
                                                                                Format="dd/MM/yyyy" Key="LastRenewalDate" Width="10%" meta:resourcekey="UltraGridColumnResource8">
                                                                                <Header Caption="LastRenewalDate">
                                                                                    <RowLayoutColumnInfo OriginX="7" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="7" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ExpiryDate" Hidden="False" DataType="System.Date"
                                                                                Format="dd/MM/yyyy" Key="ExpiryDate" Width="15%" meta:resourcekey="UltraGridColumnResource9">
                                                                                <Header Caption="ExpiryDate">
                                                                                    <RowLayoutColumnInfo OriginX="8" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="8" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="RegDate" Hidden="False" DataType="System.Date"
    Format="dd/MM/yyyy" Key="RegDate" Width="15%" meta:resourcekey="UltraGridColumnResource10">
    <Header Caption="RegDate">
        <RowLayoutColumnInfo OriginX="8" />
    </Header>
    <Footer>
        <RowLayoutColumnInfo OriginX="8" />
    </Footer>
</igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <RowEditTemplate>
                                                                            <p align="right">
                                                                                Document
                                                                                <input id="igtbl_TextBox_0_1" columnkey="DocumentID" style="width: 150px" type="text" /><br />
                                                                                DocumentNumber
                                                                                <input id="igtbl_TextBox_0_4" columnkey="DocumentNumber" style="width: 150px" type="text" /><br />
                                                                                IssueDate
                                                                                <input id="igtbl_TextBox_0_5" columnkey="IssueDate" style="width: 150px" type="text" /><br />
                                                                                LastRenewalDate
                                                                                <input id="igtbl_TextBox_0_7" columnkey="LastRenewalDate" style="width: 150px" type="text" /><br />
                                                                                ExpiryDate
                                                                                <input id="igtbl_TextBox_0_8" columnkey="ExpiryDate" style="width: 150px" type="text" /><br />
                                                                            </p>
                                                                            <br />
                                                                            <p align="center">
                                                                                <input id="igtbl_reOkBtn" onclick="igtbl_gRowEditButtonClick(event);" style="width: 50px"
                                                                                    type="button" value="OK" />&nbsp;
                                                                                <input id="igtbl_reCancelBtn" onclick="igtbl_gRowEditButtonClick(event);" style="width: 50px"
                                                                                    type="button" value="Cancel" /></p>
                                                                        </RowEditTemplate>
                                                                        <RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge">
                                                                            <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                                        </RowTemplateStyle>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                            </igtbl:UltraWebGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
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
