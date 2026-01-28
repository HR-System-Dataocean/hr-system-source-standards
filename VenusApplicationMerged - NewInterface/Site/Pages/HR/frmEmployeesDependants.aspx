<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeesDependants.aspx.vb"
    Inherits="frmEmployeesDependants" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

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
    <title>* Venus Payroll * ~Employees Dependants</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="Scripts/App_Emp_Documents.js" type="text/javascript"></script>
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
        $(function () {
            try {
                $(".Validators").Float();
            }
            catch (err) { }
        });
        function DisplayImageScreen(IntObjectId) {
            var webTab = igtab_getTabById("UltraWebTab1");
            if (webTab == null)
                return;

            var ctrId = window.document.getElementById("txtID");
            if (ctrId.value != "" && ctrId.value != null && ctrId.value != "0")
                window.open("frmPictures.aspx?OId=" + IntObjectId + "&RId=" + ctrId.value, "_blank", "height=435,width=447,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");
        }
    </script>
</head>
<body style="height: 415px; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmEmployeesDependants" runat="server">
    <div style="display: none">
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
        <asp:HiddenField ID="txtObjectId" runat="server" />
        <asp:HiddenField ID="txtRecordID" runat="server" />
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
                                                            <asp:Label ID="lblUser" runat="server" Width="60px" SkinID="Label_CopyRightsBold"
                                                                Text="Code" meta:resourcekey="lblUserResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="lblDescEmployeeCode" runat="server" BorderColor="#DCE8F6" BorderStyle="Solid"
                                                                BorderWidth="1px" BackColor="#DCE8F6" Style="font-family: Tahoma; font-size: 7pt;
                                                                font-weight: normal; color: Gray; vertical-align: middle; text-align: center"
                                                                Width="80px" Height="16px" AutoPostBack="True" Enabled="False"></asp:TextBox>
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
                                                            <table style="width: 100%; height: 18px; vertical-align: top;">
                                                                <tr>
                                                                    <td style="width: 5px">
                                                                    </td>
                                                                    <td style="width: 40px; text-align: center;">
                                                                    </td>
                                                                    <td style="width: 5px">
                                                                    </td>
                                                                    <td style="width: 40px; text-align: center;">
                                                                    </td>
                                                                    <td style="width: 5px">
                                                                    </td>
                                                                    <td style="width: 40px; text-align: center;">
                                                                    </td>
                                                                    <td style="width: 5px">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="width: 40px; text-align: center;">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; height: 83%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 120px;">
                                                <asp:Image ID="ImgDependantImage" runat="server" meta:resourcekey="Image1Resource1"
                                                    Width="120px" Height="120px" />
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 5px;">
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="Label1" runat="server" meta:resourcekey="lblEngNameResource1" SkinID="Label_DefaultNormal"
                                                                    Text="English name" Width="80px"></asp:Label>
                                                            </td>
                                                            <td style="width: 210px;">
                                                                <asp:TextBox ID="txtEngName" runat="server" meta:resourcekey="txtEngNameResource1"
                                                                    SkinID="TextBox_DefaultNormalltr"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                   <asp:Label ID="lblNationalIDORIqamaNo" runat="server" meta:resourcekey="lblNationalIDORIqamaNoResource1" SkinID="Label_DefaultNormal"
       Text="English name" Width="80px"></asp:Label>
                                                            </td>
                                                            <td style="width: 210px;">
                                                                    <asp:TextBox ID="TxtNationalIDORIqamaNo" runat="server" meta:resourcekey="txtNationalIDORIqamaNoResource1"
        SkinID="TextBox_DefaultNormalltr"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 5px;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 5px;">
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="Label2" runat="server" meta:resourcekey="lblArbNameResource1" SkinID="Label_DefaultNormal"
                                                                    Text="Arabic name"></asp:Label>
                                                            </td>
                                                            <td style="width: 210px;">
                                                                <asp:TextBox ID="txtArbName" runat="server" meta:resourcekey="txtArbNameResource1"
                                                                    SkinID="TextBox_DefaultNormalrtl"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 20px;">
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblSex" runat="server" SkinID="Label_DefaultNormal" Text="Sex" meta:resourcekey="lblSexResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 210px;">
                                                                <asp:DropDownList ID="DdlSex" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="DdlSexResource1">
                                                                    <asp:ListItem meta:resourcekey="ListItemResource3" Selected="True" Text="Male" Value="M"></asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="ListItemResource4" Text="Female" Value="F"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 5px;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 5px;">
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblDependantTypeID" runat="server" SkinID="Label_DefaultNormal" Text="Dependant Type"
                                                                    meta:resourcekey="lblDependantTypeIDResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 210px;">
                                                                <asp:DropDownList ID="DdlDependantTypeID" runat="server" SkinID="DropDownList_LargNormal"
                                                                    meta:resourcekey="DdlDependantTypeIDResource1">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblNationalityId" runat="server" SkinID="Label_DefaultNormal" Text="Nationality"
                                                                    meta:resourcekey="lblNationalityIdResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 210px;">
                                                                <asp:DropDownList ID="DdlNationalityId" runat="server" SkinID="DropDownList_LargNormal"
                                                                    meta:resourcekey="DdlNationalityIdResource1">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 5px;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 5px;">
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblBirthCity2" runat="server" SkinID="Label_DefaultNormal" Text="Birth City"
                                                                    meta:resourcekey="lblBirthCity2Resource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 210px;">
                                                                <asp:DropDownList ID="DdlBirthCity2" runat="server" SkinID="DropDownList_LargNormal"
                                                                    meta:resourcekey="DdlBirthCity2Resource1">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblBirthDate" runat="server" SkinID="Label_DefaultNormal" Text="Birth Date"
                                                                    meta:resourcekey="lblBirthDateResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 210px;">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebMaskEdit ID="txtBirthDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default">
                                                                            </igtxt:WebMaskEdit>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblHijri2" runat="server" meta:resourcekey="lblGergResource1" SkinID="Label_CopyRightsBold"
                                                                                Text="G"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebMaskEdit ID="txtBirthDateH" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default">
                                                                            </igtxt:WebMaskEdit>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblHijri" runat="server" meta:resourcekey="lblHijriResource1" SkinID="Label_CopyRightsBold"
                                                                                Text="H"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 5px;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 5px;">
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblInsuranceCovered" runat="server" SkinID="Label_DefaultNormal" Text="Insurance Covered"
                                                                    Width="100px" meta:resourcekey="lblInsuranceCoveredResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 210px;">
                                                                <asp:DropDownList ID="DdlInsuranceCovered" runat="server" SkinID="DropDownList_LargNormal"
                                                                    meta:resourcekey="DdlInsuranceCoveredResource1">
                                                                    <asp:ListItem meta:resourcekey="ListItemResource5" Text="Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="ListItemResource6" Text="No" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblPercentageInsurance" runat="server" SkinID="Label_DefaultNormal"
                                                                    Text="Insurance PCT" meta:resourcekey="lblPercentageInsuranceResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 210px;">
                                                                <igtxt:WebNumericEdit ID="txtPercentageInsurance" runat="server" MaxValue="100" MinValue="0"
                                                                    NullText="0" SkinID="WebNumericEdit_Default" ValueText="0" meta:resourcekey="txtPercentageInsuranceResource1">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                            <td style="width: 5px;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 5px;">
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblTicketCovered" runat="server" SkinID="Label_DefaultNormal" Text="Ticket Covered"
                                                                    meta:resourcekey="lblTicketCoveredResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 210px;">
                                                                <asp:DropDownList ID="DdlTicketCovered" runat="server" SkinID="DropDownList_LargNormal"
                                                                    meta:resourcekey="DdlTicketCoveredResource1">
                                                                    <asp:ListItem meta:resourcekey="ListItemResource5" Text="Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="ListItemResource6" Text="No" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblPercentageTicket" runat="server" SkinID="Label_DefaultNormal" Text="Ticket PCT"
                                                                    meta:resourcekey="lblPercentageTicketResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 210px;">
                                                                <igtxt:WebNumericEdit ID="txtPercentageTicket" runat="server" MaxValue="100" MinValue="0"
                                                                    NullText="0" SkinID="WebNumericEdit_Default" ValueText="0" meta:resourcekey="txtPercentageTicketResource1">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                            <td style="width: 5px;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                    border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label6" runat="server" meta:resourcekey="Label6Resource1" SkinID="Label_DefaultBold"
                                                                Text="Current contract information"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgDependents" runat="server" EnableAppStyling="True" Height="100%"
                                                    SkinID="Default" Width="100%" meta:resourcekey="uwgDependentsResource1">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                        RowHeightDefault="18px" RowSelectorsDefault="NotSet" SelectTypeRowDefault="Extended"
                                                        StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                        Version="4.00" ViewType="OutlookGroupBy">
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
                                                        <igtbl:UltraGridBand AllowAdd="Yes" meta:resourcekey="UltraGridBandResource1">
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                            </FilterOptions>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" NullText="0" meta:resourcekey="UltraGridColumnResource1">
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="EngName" Key="EnglishName" Width="25%" meta:resourcekey="UltraGridColumnResource2">
                                                                    <Header Caption="English Name">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ArbName" Key="ArbName" Width="25%" meta:resourcekey="UltraGridColumnResource3">
                                                                    <Header Caption="Arabic Name">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="BirthDate" Format="dd/MM/yyyy" Key="BirthDate"
                                                                    Width="22%" meta:resourcekey="UltraGridColumnResource5">
                                                                    <Header Caption="Birth Date">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                                       <igtbl:UltraGridColumn BaseColumnName="NationalIDORIqamano" Format="dd/MM/yyyy" Key="BirthDate"
                           Width="22%" meta:resourcekey="UltraGridColumnNationalIDORIqamanoResource">
                           <Header Caption="Birth Date">
                               <RowLayoutColumnInfo OriginX="4" />
                           </Header>
                           <Footer>
                               <RowLayoutColumnInfo OriginX="4" />
                           </Footer>
                       </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="RegDate" Hidden="false" Key="RegDate" Width="28%"
                                                                    meta:resourcekey="UltraGridColumnResource11">
                                                                    <Header Caption="RegDate">
                                                                        <RowLayoutColumnInfo OriginX="10" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="10" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
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
</body>
</html>
