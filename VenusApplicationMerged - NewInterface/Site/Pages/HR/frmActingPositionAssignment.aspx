<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmActingPositionAssignment.aspx.vb"
    Inherits="frmActingPositionAssignment" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>
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
    <title>Position Acting Assignment</title>
    <script language="javascript" src="Scripts/App_JScript.js"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var actingDialog, actingSender;
        function OpenModal1(pageurl, height, width, checkID, senderCtrl) {
            actingSender = senderCtrl;
            actingDialog = $('<div></div>').html('<iframe style="border:0" src="' + pageurl + '" width="100%" height="100%"></iframe>')
                .dialog({ autoOpen: true, modal: true, height: height, width: width });
        }
        function CloseIt(value) {
            if (value && actingSender) {
                var sender = document.getElementById(actingSender);
                sender.value = value;
                sender.focus();
                if (sender.name) __doPostBack(sender.name, '');
            }
            if (actingDialog) actingDialog.dialog('close');
        }
    </script>
    <style type="text/css">
        .assignment { width:100%; border-collapse:collapse; }
        .assignment td { padding:4px; vertical-align:middle; }
        .fieldLabel { width:175px; white-space:nowrap; }
        .required { color:#d00; }
        .readonly { background:#f1f1f1; }
        .gridArea {
            width:100%;
            height:140px;
            overflow:auto;
            border:1px solid #808080;
            background:#fff;
        }
        .gridArea table {
            width:100% !important;
            height:auto !important;
        }
        .gridArea tr.igtbl_Header,
        .gridArea .igtbl_Header,
        .gridArea .igtbl_HeaderCaption,
        .gridArea td.igtbl_Header,
        .gridArea th {
            height:22px !important;
            max-height:22px !important;
            line-height:18px !important;
            vertical-align:middle !important;
            padding-top:2px !important;
            padding-bottom:2px !important;
        }
    </style>
</head>
<body style="height:100%; margin:0; padding:0">
<form id="frmActingPositionAssignment" runat="server">
<div id="DIV" runat="server" class="Div_MasterContainer">
    <table style="width:100%">
        <tr><td>
            <table style="width:100%; border-bottom:1px solid silver">
                <tr>
                    <td style="width:24px" runat="server" visible="false"><asp:ImageButton ID="ImageButton_Save" runat="server" Width="16px" Height="16px" SkinID="HrSave_Command" CommandArgument="Save" meta:resourcekey="ImageButton_SaveResource1" /></td>
                    <td style="width:120px" runat="server" visible="false"><asp:ImageButton ID="ImageButton_SaveN" runat="server" Width="16px" Height="16px" SkinID="HrSaveN_Command" CommandArgument="SaveNew" meta:resourcekey="ImageButton_SaveNResource1" /><asp:LinkButton ID="LinkButton_SaveN" runat="server" Text="Save With New" CommandArgument="SaveNew" meta:resourcekey="LinkButton_SaveNResource1" /></td>
                    <td style="width:24px" runat="server" visible="false"><asp:ImageButton ID="ImageButton_New" runat="server" Width="16px" Height="16px" SkinID="HrNew_Command" CommandArgument="New" meta:resourcekey="ImageButton_NewResource1" /></td>
                    <td style="width:75px" runat="server" visible="false"><asp:ImageButton ID="ImageButton_Delete" runat="server" Width="16px" Height="16px" SkinID="HrDelete_Command" CommandArgument="Delete" meta:resourcekey="ImageButton_DeleteResource1" /><asp:LinkButton ID="LinkButton_Delete" runat="server" Text="Cancel" CommandArgument="Delete" meta:resourcekey="LinkButton_DeleteResource1" /></td>
                    <td style="width:24px"><asp:ImageButton ID="ImageButton_Print" runat="server" Width="16px" Height="16px" SkinID="HrPrint_Command" CommandArgument="Print" meta:resourcekey="ImageButton_PrintResource1" /></td>
                    <td style="width:85px"><asp:ImageButton ID="ImageButton_Properties" runat="server" Width="16px" Height="16px" SkinID="HrProperties_Command" CommandArgument="Property" meta:resourcekey="ImageButton_PropertiesResource1" /><asp:LinkButton ID="LinkButton_Properties" runat="server" Text="Properties" CommandArgument="Property" meta:resourcekey="LinkButton_PropertiesResource1" /></td>
                    <td style="width:85px"><asp:ImageButton ID="ImageButton_Remarks" runat="server" Width="16px" Height="16px" SkinID="HrRemarks_Command" CommandArgument="Remarks" meta:resourcekey="ImageButton_RemarksResource1" /><asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="Remarks" CommandArgument="Remarks" meta:resourcekey="LinkButton_RemarksResource1" /></td>
                    <td style="width:24px"><asp:ImageButton ID="ImageButton_First" runat="server" Width="16px" Height="16px" SkinID="HrFirest_Command" CommandArgument="First" /></td>
                    <td style="width:24px"><asp:ImageButton ID="ImageButton_Back" runat="server" Width="16px" Height="16px" SkinID="HrBack_Command" CommandArgument="Previous" /></td>
                    <td style="width:24px"><asp:ImageButton ID="ImageButton_Next" runat="server" Width="16px" Height="16px" SkinID="HrNext_Command" CommandArgument="Next" /></td>
                    <td style="width:24px"><asp:ImageButton ID="ImageButton_Last" runat="server" Width="16px" Height="16px" SkinID="HrLast_Command" CommandArgument="Last" /></td>
                    <td></td>
                </tr>
            </table>
        </td></tr>
        <tr><td>
            <table style="width:100%; height:58px">
                <tr>
                    <td style="width:40px"><asp:Image ID="Image_Logo" runat="server" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png" /></td>
                    <td style="width:45%"><asp:Label ID="Label_Header" runat="server" Text="Position Acting Assignment" Font-Bold="True" meta:resourcekey="Label_HeaderResource1" /></td>
                    <td>
                        <table style="width:100%">
                            <tr><td><asp:Label ID="lblRegDate" runat="server" Text="Registered In" meta:resourcekey="lblRegDateResource1" /></td><td><asp:Label ID="lblRegDateValue" runat="server" /></td><td><asp:Label ID="lblRegUser" runat="server" Text="Registered By" meta:resourcekey="lblRegUserResource1" /></td><td><asp:Label ID="lblRegUserValue" runat="server" /></td></tr>
                            <tr><td><asp:Label ID="lblCancelDate" runat="server" Text="Cancel Date" meta:resourcekey="lblCancelDateResource1" /></td><td><asp:Label ID="lblCancelDateValue" runat="server" /></td><td></td><td></td></tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td></tr>
        <tr><td class="Details">
            <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default">
                <Tabs><igtab:Tab Text="General" meta:resourcekey="TabResource1"><ContentTemplate>
                    <table class="assignment">
                        <tr><td class="fieldLabel"><asp:Label ID="lblCode" runat="server" Text="Code" meta:resourcekey="lblCodeResource1" /> <span class="required">*</span></td><td><asp:TextBox ID="txtCode" runat="server" Width="145px" AutoPostBack="True" MaxLength="30" Enabled="False" /><igtxt:WebImageButton ID="btnSearchCode" runat="server" AutoSubmit="False" Width="24px" Height="18px" Enabled="False"><Appearance><Image Url="./Img/forum_search.gif" /></Appearance></igtxt:WebImageButton></td></tr>
                        <tr><td class="fieldLabel"><asp:Label ID="lblOriginalPosition" runat="server" Text="Assigned Position" meta:resourcekey="lblOriginalPositionResource1" /> <span class="required">*</span></td><td><asp:TextBox ID="txtOriginalPosition" runat="server" Width="145px" AutoPostBack="True" MaxLength="30" Enabled="False" /><igtxt:WebImageButton ID="btnOriginalPositionSearch" runat="server" AutoSubmit="False" Width="24px" Height="18px" Enabled="False"><Appearance><Image Url="./Img/forum_search.gif" /></Appearance></igtxt:WebImageButton> <asp:TextBox ID="txtOriginalPositionName" runat="server" Width="255px" CssClass="readonly" ReadOnly="True" Enabled="False" /></td></tr>
                        <tr><td class="fieldLabel"><asp:Label ID="lblActingEmployee" runat="server" Text="Acting Employee" meta:resourcekey="lblActingEmployeeResource1" /> <span class="required">*</span></td><td><asp:TextBox ID="txtActingEmployee" runat="server" Width="145px" AutoPostBack="True" MaxLength="30" Enabled="False" /><igtxt:WebImageButton ID="btnActingEmployeeSearch" runat="server" AutoSubmit="False" Width="24px" Height="18px" Enabled="False"><Appearance><Image Url="./Img/forum_search.gif" /></Appearance></igtxt:WebImageButton> <asp:TextBox ID="txtActingEmployeeName" runat="server" Width="255px" CssClass="readonly" ReadOnly="True" Enabled="False" /></td></tr>
                        <tr><td class="fieldLabel"><asp:Label ID="lblEffectiveFrom" runat="server" Text="Acting Start Date" meta:resourcekey="lblEffectiveFromResource1" /> <span class="required">*</span></td><td><igsch:WebDateChooser ID="txtEffectiveFrom" runat="server" Width="145px" Height="18px" Enabled="False" /></td></tr>
                        <tr><td class="fieldLabel"><asp:Label ID="lblEffectiveTo" runat="server" Text="Acting End Date" meta:resourcekey="lblEffectiveToResource1" /> <span class="required">*</span></td><td><igsch:WebDateChooser ID="txtEffectiveTo" runat="server" Width="145px" Height="18px" Enabled="False" /></td></tr>
                        <tr><td class="fieldLabel"><asp:Label ID="lblReason" runat="server" Text="Acting Reason" meta:resourcekey="lblReasonResource1" /></td><td><asp:TextBox ID="txtReason" runat="server" Width="400px" MaxLength="500" Enabled="False" /></td></tr>
                        <tr><td class="fieldLabel"><asp:Label ID="lblRemarks" runat="server" Text="Remarks" meta:resourcekey="lblRemarksResource1" /></td><td><asp:TextBox ID="txtRemarks" runat="server" Width="400px" Height="50px" TextMode="MultiLine" MaxLength="1000" Enabled="False" /></td></tr>
                        <tr><td colspan="2">
                            <div class="gridArea">
                            <igtbl:UltraWebGrid ID="uwgAssignments" runat="server" Width="100%" EnableAppStyling="False" AutoPostBack="True" Browser="UpLevel">
                                <DisplayLayout Name="uwgAssignments" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
                                    CellClickActionDefault="RowSelect" SelectTypeRowDefault="Single" RowSelectorsDefault="No"
                                    TableLayout="Fixed" StationaryMargins="No" RowHeightDefault="18px"
                                    BorderCollapseDefault="Separate" Version="4.00">
                                    <FrameStyle BackColor="Window" BorderStyle="None" Font-Names="Tahoma" Font-Size="8pt" Width="100%">
                                    </FrameStyle>
                                    <HeaderStyleDefault BackColor="#DFDFDF" Font-Names="Tahoma" Font-Size="9pt" Height="22px"
                                        VerticalAlign="Middle" />
                                    <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="Tahoma" Font-Size="8pt" Height="18px" />
                                </DisplayLayout>
                                <Bands><igtbl:UltraGridBand>
                                    <Columns>
                                        <igtbl:UltraGridColumn BaseColumnName="Code" Key="Code" Width="14%" meta:resourcekey="GridCodeResource1"><Header Caption="Transaction No." /></igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="OriginalPosition" Key="OriginalPosition" Width="28%" meta:resourcekey="GridOriginalPositionResource1"><Header Caption="Assigned Position" /></igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="ActingEmployee" Key="ActingEmployee" Width="28%" meta:resourcekey="GridActingEmployeeResource1"><Header Caption="Acting Employee" /></igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EffectiveFrom" Key="EffectiveFrom" DataType="System.DateTime" Format="dd/MM/yyyy" Width="15%" meta:resourcekey="GridEffectiveFromResource1"><Header Caption="Acting Start Date" /></igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EffectiveTo" Key="EffectiveTo" DataType="System.DateTime" Format="dd/MM/yyyy" Width="15%" meta:resourcekey="GridEffectiveToResource1"><Header Caption="Acting End Date" /></igtbl:UltraGridColumn>
                                    </Columns>
                                </igtbl:UltraGridBand></Bands>
                            </igtbl:UltraWebGrid>
                            </div>
                        </td></tr>
                    </table>
                </ContentTemplate></igtab:Tab></Tabs>
            </igtab:UltraWebTab>
        </td></tr>
    </table>
</div>
</form>
</body>
</html>
