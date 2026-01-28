<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProjecWizard.aspx.vb"
    Inherits="frmProjecWizard" Culture="auto" UICulture="auto" %>

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
    <title>* Venus Payroll * ~ frmProjecWizard</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmProjecWizard" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%= LinkButton_Generate.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= LinkButton_GenerateOne.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
        });
        function OpenScreen() {
            var LocDetails = document.getElementById('<%=ddlPosition.ClientID%>');
            var LocDetailsValue = LocDetails.options[LocDetails.selectedIndex].value;

            if (LocDetailsValue > 0) {
                var hight = 600;
                var width = 1000;
                var win = window.open("frmProjectPlacementPlanning.aspx?LocationDetailID=" + LocDetailsValue, "_NEW", "height=" + hight + ",width=" + width + ",resizable=1,menubar=0,toolbar=0,location=0,directories=0,scrollbars=1,status=0,center=0");
                win.focus();
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="display: none">
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td class="Details">
                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                        <tr>
                            <td style="height: 18px">
                                <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCode" runat="server" Width="80px" SkinID="Label_CopyRightsBold"
                                                Text="الكود"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblProjectCode" runat="server" Width="80px" SkinID="Label_CopyRightsNormal"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblName" runat="server" SkinID="Label_CopyRightsBold" Text="التوصيف"
                                                Width="80px"></asp:Label>
                                        </td>
                                        <td style="width: 40%;">
                                            <asp:Label ID="lblProjectName" runat="server" Width="100%" SkinID="Label_CopyRightsNormal"></asp:Label>
                                        </td>
                                        <td style="width: 60%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <igtab:UltraWebTab ID="UltraWebTab1" TabOrientation="LeftTop" runat="server" EnableAppStyling="True"
                        SkinID="Default" SelectedTab="5">
                        <Tabs>
                            <igtab:Tab Enabled="true" Text="معلومات التعديل">
                                <ContentTemplate>
                                    <asp:HiddenField ID="CCHangeID" runat="server" />
                                    <asp:HiddenField ID="NewChangeID" runat="server" />
                                    <asp:HiddenField ID="ToNewChangeID" runat="server" />
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: left; width: 50%">
                                                <asp:LinkButton ID="LinkButton2" runat="server" SkinID="LinkButton_DefaultBold" Text="التالى"
                                                    ValidationGroup="G1"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 2%">
                                            </td>
                                            <td style="width: 96%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label1" runat="server" Text="تاريخ بداية التفعيل" SkinID="Label_DefaultNormal"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtStartDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix">
                                                            </igtxt:WebMaskEdit>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtStartDate" SetFocusOnError="True" ValidationGroup="G1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label31" runat="server" Text="تاريخ نهاية التفعيل" SkinID="Label_DefaultNormal"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtEndDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix">
                                                            </igtxt:WebMaskEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 2%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%">
                                            </td>
                                            <td style="width: 96%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label2" runat="server" Text="سبب التعديل" SkinID="Label_DefaultNormal"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtCompanyConditions" runat="server" Height="60px" MaxLength="8000"
                                                                TextMode="MultiLine" Width="80%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtCompanyConditions" SetFocusOnError="True" ValidationGroup="G1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 2%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; width: 100%; vertical-align: bottom; border-bottom: 1px solid black">
                                                <asp:Label ID="Label_SubTitle1" runat="server" SkinID="Label_DefaultBold" Text="تعديلات سابقة فى نفس العقد"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <igtbl:UltraWebGrid   Browser="UpLevel"  ID="uwgChanges" runat="server" EnableAppStyling="True" Height="100px"
                                                    SkinID="Default" Width="100%" EnableTheming="True">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgSuperVisors"
                                                        RowHeightDefault="18px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                                                        AllowAddNewDefault="Yes" CellClickActionDefault="Edit">
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
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="0px">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="FromDate" Key="FromDate" Width="40%" Format="dd/MM/yyyy">
                                                                    <Header Caption="من تاريخ">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Remarks" Key="Remarks" Width="60%">
                                                                    <Header Caption="أسباب التعديل">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
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
                            <igtab:Tab Enabled="false" Text="الإضافى">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: left; width: 50%">
                                                <asp:LinkButton ID="LinkButton4" runat="server" SkinID="LinkButton_DefaultBold" Text="التالى"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_InternalOvertimeFactor" runat="server" Text="معامل الإضافى للرواتب"
                                                                SkinID="Label_DefaultNormal"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtInternalOvertimeFactor" runat="server" DataMode="Decimal"
                                                                MinValue="0" NullText="0" SkinID="WebNumericEdit_Default" ValueText="0">
                                                            </igtxt:WebNumericEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_ExternalOvertimeFactor" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="معامل الإضافى للفواتير"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtExternalOvertimeFactor" runat="server" DataMode="Decimal"
                                                                MinValue="0" NullText="0" SkinID="WebNumericEdit_Default" ValueText="0">
                                                            </igtxt:WebNumericEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_InternalHOvertimeFactor" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="معامل العطلات للرواتب"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtInternalHolidayFactor" runat="server" DataMode="Decimal"
                                                                MinValue="0" NullText="0" SkinID="WebNumericEdit_Default" ValueText="0">
                                                            </igtxt:WebNumericEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_ExternalOvertimeFactor0" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="معامل العطلات للفواتير"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtExternalHolidayFactor" runat="server" DataMode="Decimal"
                                                                MinValue="0" NullText="0" SkinID="WebNumericEdit_Default" ValueText="0">
                                                            </igtxt:WebNumericEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_InternalExtension" runat="server" Text="قيمة التطبيق للرواتب"
                                                                SkinID="Label_DefaultNormal"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="WebNumericEdit_InternalExtension" runat="server" DataMode="Decimal"
                                                                MinValue="0" NullText="0" SkinID="WebNumericEdit_Default" ValueText="0">
                                                            </igtxt:WebNumericEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_ExternalExtension" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="قيمة التطبيق للفواتير"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="WebNumericEdit_ExternalExtension" runat="server" DataMode="Decimal"
                                                                MinValue="0" NullText="0" SkinID="WebNumericEdit_Default" ValueText="0">
                                                            </igtxt:WebNumericEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Enabled="false" Text="الخصومات">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: right; width: 50%">
                                                <asp:LinkButton ID="LinkButton5" runat="server" SkinID="LinkButton_DefaultBold" Text="السابق"></asp:LinkButton>
                                            </td>
                                            <td style="height: 18px; text-align: left; width: 50%">
                                                <asp:LinkButton ID="LinkButton6" runat="server" ValidationGroup="G2" SkinID="LinkButton_DefaultBold"
                                                    Text="التالى"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label3" runat="server" Text="معامل الغياب للرواتب" SkinID="Label_DefaultNormal"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtInternalAbsentFactor" Text="0" runat="server" SkinID="TextBox_LargeBoldltr"
                                                                MaxLength="255"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtInternalAbsentFactor" SetFocusOnError="True" ValidationGroup="G2">*</asp:RequiredFieldValidator>
                                                            <br />
                                                            <asp:Label ID="Label610" runat="server" Text="like : 1,2,3,4" SkinID="Label_CopyRightsNormal"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label4" runat="server" SkinID="Label_DefaultNormal" Text="معامل الغياب للفواتير"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtExternalAbsentFactor" Text="0" runat="server" SkinID="TextBox_LargeBoldltr"
                                                                MaxLength="255"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtExternalAbsentFactor" SetFocusOnError="True" ValidationGroup="G2">*</asp:RequiredFieldValidator>
                                                            <br />
                                                            <asp:Label ID="Label11" runat="server" Text="like : 1,2,3,4" SkinID="Label_CopyRightsNormal"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea" style="height: 16px">
                                                        </td>
                                                        <td class="style1">
                                                            <asp:Label ID="Label5" runat="server" Text="معامل المرضى للرواتب" SkinID="Label_DefaultNormal"></asp:Label>
                                                        </td>
                                                        <td class="DataArea" style="height: 16px">
                                                            <asp:TextBox ID="txtInternalSickFactor" Text="0" runat="server" SkinID="TextBox_LargeBoldltr"
                                                                MaxLength="255"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtInternalSickFactor" SetFocusOnError="True" ValidationGroup="G2">*</asp:RequiredFieldValidator>
                                                            <br />
                                                            <asp:Label ID="Label12" runat="server" Text="like : 1,2,3,4" SkinID="Label_CopyRightsNormal"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label10" runat="server" SkinID="Label_DefaultNormal" Text="معامل المرضى للفواتير"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtExternalSickFactor" Text="0" runat="server" SkinID="TextBox_LargeBoldltr"
                                                                MaxLength="255"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtExternalSickFactor" SetFocusOnError="True" ValidationGroup="G2">*</asp:RequiredFieldValidator>
                                                            <br />
                                                            <asp:Label ID="Label13" runat="server" Text="like : 1,2,3,4" SkinID="Label_CopyRightsNormal"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label6" runat="server" Text="معامل الإنسحاب للرواتب" SkinID="Label_DefaultNormal"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtInternalLeavFactor" Text="0" runat="server" SkinID="TextBox_LargeBoldltr"
                                                                MaxLength="255"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtInternalLeavFactor" SetFocusOnError="True" ValidationGroup="G2">*</asp:RequiredFieldValidator>
                                                            <br />
                                                            <asp:Label ID="Label17" runat="server" Text="like : 1,2,3,4" SkinID="Label_CopyRightsNormal"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label14" runat="server" SkinID="Label_DefaultNormal" Text="معامل الانسحاب للفواتير"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtExternalLeavFactor" Text="0" runat="server" SkinID="TextBox_LargeBoldltr"
                                                                MaxLength="255"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtExternalLeavFactor" SetFocusOnError="True" ValidationGroup="G2">*</asp:RequiredFieldValidator>
                                                            <br />
                                                            <asp:Label ID="Label18" runat="server" Text="like : 1,2,3,4" SkinID="Label_CopyRightsNormal"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label8" runat="server" Text="التأخير المسموح به للرواتب-دقيقة" SkinID="Label_DefaultNormal"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtInternalPermitDelayFactor" Text="0" runat="server" SkinID="TextBox_LargeBoldltr"
                                                                MaxLength="255"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtInternalPermitDelayFactor" SetFocusOnError="True" ValidationGroup="G2">*</asp:RequiredFieldValidator>
                                                            <br />
                                                            <asp:Label ID="Label19" runat="server" Text="like : 1,2,3,4" SkinID="Label_CopyRightsNormal"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label15" runat="server" SkinID="Label_DefaultNormal" Text="التأخير المسموح به للفواتير-دقيقة"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtExternalPermitDelayFactor" Text="0" runat="server" SkinID="TextBox_LargeBoldltr"
                                                                MaxLength="255"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtExternalPermitDelayFactor" SetFocusOnError="True" ValidationGroup="G2">*</asp:RequiredFieldValidator>
                                                            <br />
                                                            <asp:Label ID="Label20" runat="server" Text="like : 1,2,3,4" SkinID="Label_CopyRightsNormal"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label9" runat="server" Text="قيمة تجاوز التأخير للرواتب-يوم" SkinID="Label_DefaultNormal"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtInternalDelayPunishFactor" Text="0" runat="server" SkinID="TextBox_LargeBoldltr"
                                                                MaxLength="255"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtInternalDelayPunishFactor" SetFocusOnError="True" ValidationGroup="G2">*</asp:RequiredFieldValidator>
                                                            <br />
                                                            <asp:Label ID="Label21" runat="server" Text="like : 1,2,3,4" SkinID="Label_CopyRightsNormal"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label16" runat="server" SkinID="Label_DefaultNormal" Text="قيمة تجاوز التأخير للفواتير-يوم"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtExternalDelayPunishFactor" Text="0" runat="server" SkinID="TextBox_LargeBoldltr"
                                                                MaxLength="255"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtExternalDelayPunishFactor" SetFocusOnError="True" ValidationGroup="G2">*</asp:RequiredFieldValidator>
                                                            <br />
                                                            <asp:Label ID="Label22" runat="server" Text="like : 1,2,3,4" SkinID="Label_CopyRightsNormal"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Enabled="false" Text="المكافئات">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: right; width: 50%">
                                                <asp:LinkButton ID="LinkButton7" runat="server" SkinID="LinkButton_DefaultBold" Text="السابق"></asp:LinkButton>
                                            </td>
                                            <td style="height: 18px; text-align: left; width: 50%">
                                                <asp:LinkButton ID="LinkButton8" runat="server" SkinID="LinkButton_DefaultBold" Text="التالى"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 2%; vertical-align: top">
                                            </td>
                                            <td style="width: 96%; vertical-align: top">
                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgBenfits" runat="server" EnableAppStyling="True" Height="220px"
                                                    SkinID="Default" Width="100%" EnableTheming="True">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgSuperVisors"
                                                        RowHeightDefault="18px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                                                        AllowAddNewDefault="Yes" CellClickActionDefault="Edit">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="220px"
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
                                                        <igtbl:UltraGridBand>
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
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="0px">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="RewardID" Width="30%" EditorControlID="" Type="DropDownList"
                                                                    Key="RewardID" DataType="System.Int32" AllowUpdate="Yes">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="التوصيف">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Occurance" Key="Occurance" Width="10%" DataType="System.Int32">
                                                                    <Header Caption="التكرار">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ExternalValue" Key="ExternalValue" Width="15%"
                                                                    DataType="System.Decimal" Format="##.##">
                                                                    <Header Caption="القيمة للفواتير">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ExternalFactor" Width="15%" EditorControlID=""
                                                                    Type="DropDownList" Key="ExternalFactor" DataType="System.Int32" AllowUpdate="Yes">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="المعيار">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="InternalValue" Key="InternalValue" Width="15%"
                                                                    DataType="System.Decimal" Format="##.##">
                                                                    <Header Caption="القيمة للرواتب">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="InternalFactor" Width="15%" EditorControlID=""
                                                                    Type="DropDownList" Key="InternalFactor" DataType="System.Int32" AllowUpdate="Yes">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="المعيار">
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
                                            </td>
                                            <td style="width: 2%; vertical-align: top">
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Enabled="false" Text="المخالفات">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: right; width: 50%">
                                                <asp:LinkButton ID="LinkButton9" runat="server" SkinID="LinkButton_DefaultBold" Text="السابق"></asp:LinkButton>
                                            </td>
                                            <td style="height: 18px; text-align: left; width: 50%">
                                                <asp:LinkButton ID="LinkButton10" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="التالى"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 2%; vertical-align: top">
                                            </td>
                                            <td style="width: 96%; vertical-align: top">
                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgPenalities" runat="server" EnableAppStyling="True" Height="220px"
                                                    SkinID="Default" Width="100%" EnableTheming="True">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgSuperVisors"
                                                        RowHeightDefault="18px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                                                        AllowAddNewDefault="Yes" CellClickActionDefault="Edit">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="220px"
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
                                                        <igtbl:UltraGridBand>
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
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="0px">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="PenaltyID" Width="30%" EditorControlID=""
                                                                    Type="DropDownList" Key="PenaltyID" DataType="System.Int32" AllowUpdate="Yes">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="التوصيف">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Occurance" Key="Occurance" Width="10%" DataType="System.Int32">
                                                                    <Header Caption="التكرار">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ExternalValue" Key="ExternalValue" Width="15%"
                                                                    DataType="System.Decimal" Format="##.##">
                                                                    <Header Caption="القيمة للفواتير">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ExternalFactor" Width="15%" EditorControlID=""
                                                                    Type="DropDownList" Key="ExternalFactor" DataType="System.Int32" AllowUpdate="Yes">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="المعيار">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="InternalValue" Key="InternalValue" Width="15%"
                                                                    DataType="System.Decimal" Format="##.##">
                                                                    <Header Caption="القيمة للرواتب">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="InternalFactor" Width="15%" EditorControlID=""
                                                                    Type="DropDownList" Key="InternalFactor" DataType="System.Int32" AllowUpdate="Yes">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="المعيار">
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
                                            </td>
                                            <td style="width: 2%; vertical-align: top">
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Enabled="false" Text="تفاصيل المواقع">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: right; width: 50%">
                                                <asp:LinkButton ID="LinkButton11" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="السابق"></asp:LinkButton>
                                            </td>
                                            <td style="height: 18px; text-align: left; width: 50%">
                                                <asp:LinkButton ID="LinkButton12" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="التالى"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 2%; vertical-align: top">
                                            </td>
                                            <td style="width: 96%; vertical-align: top">
                                                <asp:HiddenField ID="HiddenField_LocationID" runat="server" />
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <table style="width: 100%; height: 100%">
                                                            <tr>
                                                                <td style="width: 100%; vertical-align: bottom; border-bottom: 1px solid black">
                                                                    <asp:Label ID="Label7" runat="server" SkinID="Label_DefaultBold" Text="تسجيل المواقع فى المشروع"></asp:Label>
                                                                    <asp:Label ID="Label23" runat="server" SkinID="Label_CopyRightsNormal" Text="حذف الموقع أو الموقع الفرعى سيلغى جميع الوظائف الملحقة به"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%; vertical-align: top">
                                                                    <igtbl:UltraWebGrid   Browser="UpLevel"  ID="uwgLocations" runat="server" EnableAppStyling="True" Height="220px"
                                                                        SkinID="Default" Width="100%" EnableTheming="True">
                                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                                                            AllowUpdateDefault="Yes" AutoGenerateColumns="False" BorderCollapseDefault="Separate"
                                                                            HeaderClickActionDefault="SortSingle" Name="uwgSuperVisors" RowHeightDefault="18px"
                                                                            SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                                            TableLayout="Fixed" Version="4.00" ViewType="Hierarchical" AllowAddNewDefault="Yes"
                                                                            CellClickActionDefault="Edit" AllowRowNumberingDefault="ByBandLevel">
                                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="220px"
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
                                                                            <igtbl:UltraGridBand>
                                                                                <AddNewRow Visible="Yes" View="NotSet">
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
                                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="0px">
                                                                                        <Header Caption="ID">
                                                                                        </Header>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="LocationDescription" Key="LocationDescription"
                                                                                        Width="35%">
                                                                                        <Header Caption="بيانات الموقع">
                                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                                        </Header>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <CellStyle HorizontalAlign="Center">
                                                                                        </CellStyle>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="LocationAddress" Key="LocationAddress" Width="35%">
                                                                                        <Header Caption="عنوان الموقع">
                                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                                        </Header>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <CellStyle HorizontalAlign="Center">
                                                                                        </CellStyle>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="Required" Key="Required" Width="20%" DataType="System.Int32">
                                                                                        <Header Caption="عدد الوظائف">
                                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                                        </Header>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <CellStyle HorizontalAlign="Center">
                                                                                        </CellStyle>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="LinkedCS" Key="LinkedCS" Width="200px" Type="DropDownList"
                                                                                        AllowUpdate="Yes">
                                                                                        <CellStyle HorizontalAlign="Center">
                                                                                        </CellStyle>
                                                                                        <Header Caption="حساب الربط">
                                                                                            <RowLayoutColumnInfo OriginX="6" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="6" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn Key="IsDel" DataType="System.Boolean" Type="CheckBox" Width="10%">
                                                                                        <Header Caption="حذف">
                                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="OrgID" Width="0px">
                                                                                        <Header Caption="OrgID">
                                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                </Columns>
                                                                            </igtbl:UltraGridBand>
                                                                            <igtbl:UltraGridBand>
                                                                                <AddNewRow Visible="Yes" View="NotSet">
                                                                                </AddNewRow>
                                                                                <Columns>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="0px">
                                                                                        <Header Caption="ID">
                                                                                        </Header>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="MainLocationID" Hidden="True" Key="MainLocationID"
                                                                                        Width="0px">
                                                                                        <Header Caption="MainLocationID">
                                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="LocationDescription" Key="LocationDescription"
                                                                                        Width="200px">
                                                                                        <Header Caption="بيانات الموقع الفرعى">
                                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                                        </Header>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <CellStyle HorizontalAlign="Center">
                                                                                        </CellStyle>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="LocationAddress" Key="LocationAddress" Width="200px">
                                                                                        <Header Caption="عنوان الموقع الفرعى">
                                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                                        </Header>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <CellStyle HorizontalAlign="Center">
                                                                                        </CellStyle>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="Required" Key="Required" Width="80px" DataType="System.Int32">
                                                                                        <Header Caption="عدد الوظائف">
                                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                                        </Header>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <CellStyle HorizontalAlign="Center">
                                                                                        </CellStyle>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="LinkedCS" Key="LinkedCS" Width="200px" Type="DropDownList"
                                                                                        AllowUpdate="Yes">
                                                                                        <CellStyle HorizontalAlign="Center">
                                                                                        </CellStyle>
                                                                                        <Header Caption="حساب الربط">
                                                                                            <RowLayoutColumnInfo OriginX="7" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="7" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn Key="IsDel" DataType="System.Boolean" Type="CheckBox" Width="80px">
                                                                                        <Header Caption="حذف">
                                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                                        </Footer>
                                                                                    </igtbl:UltraGridColumn>
                                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="OrgID" Width="0px">
                                                                                        <Header Caption="OrgID">
                                                                                            <RowLayoutColumnInfo OriginX="6" />
                                                                                        </Header>
                                                                                        <Footer>
                                                                                            <RowLayoutColumnInfo OriginX="6" />
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
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="width: 2%; vertical-align: top">
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Enabled="false" Text="تخطيط الوظائف">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: right; width: 50%">
                                                <asp:LinkButton ID="LinkButton13" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="السابق"></asp:LinkButton>
                                            </td>
                                            <td style="height: 18px; text-align: left; width: 50%">
                                                <asp:LinkButton ID="LinkButton14" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="التالى"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 100%; vertical-align: top" colspan="3">
                                                <div>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 10px;">
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lblLocation" runat="server" SkinID="Label_DefaultNormal" Text="إختر الموقع"
                                                                    Width="80px"></asp:Label>
                                                            </td>
                                                            <td style="width: 39%;">
                                                                <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lblSubLocation" runat="server" SkinID="Label_DefaultNormal" Text="إختر الموقع الفرعى"
                                                                    Width="80px"></asp:Label>
                                                            </td>
                                                            <td style="width: 39%;">
                                                                <asp:DropDownList ID="ddlSubLocation" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 5px;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; vertical-align: top; text-align: center" colspan="3">
                                                <asp:Label ID="Label_Cnt" runat="server" Font-Bold="True" Font-Size="12pt" Font-Underline="True"
                                                    ForeColor="#FF3300"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%; vertical-align: top">
                                            </td>
                                            <td style="width: 96%; vertical-align: top">
                                                <asp:Label ID="Label25" runat="server" SkinID="Label_CopyRightsNormal" Text="حذف الوظيفة سيلغى جميع الحركات المرتبطة به"></asp:Label>
                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgLocationPositions" runat="server" EnableAppStyling="True"
                                                    Height="200px" SkinID="Default" Width="100%" EnableTheming="True">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                                        AllowUpdateDefault="Yes" AutoGenerateColumns="False" BorderCollapseDefault="Separate"
                                                        HeaderClickActionDefault="SortSingle" Name="uwgSuperVisors" RowHeightDefault="18px"
                                                        SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                        TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" AllowAddNewDefault="Yes"
                                                        CellClickActionDefault="Edit">
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
                                                        <igtbl:UltraGridBand>
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
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="0px">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Qty" Key="Qty" Width="10%" DataType="System.Int32">
                                                                    <Header Caption="العدد">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="PositionID" Width="20%" EditorControlID=""
                                                                    Type="DropDownList" Key="PositionID" DataType="System.Int32" AllowUpdate="Yes">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="المهنة">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ExternalAmt" Key="ExternalAmt" Width="15%"
                                                                    DataType="System.Decimal" Format="##.##">
                                                                    <Header Caption="قيمة التعاقد">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="InternalAmt" Key="InternalAmt" Width="15%"
                                                                    DataType="System.Decimal" Format="##.##">
                                                                    <Header Caption="سقف الراتب">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="WeekDays" Key="WeekDays" Width="10%" DataType="System.Int32">
                                                                    <Header Caption="أيام العمل">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="IsAlternative" Key="IsAlternative" DataType="System.Boolean"
                                                                    Type="CheckBox" Width="15%">
                                                                    <Header Caption="بديل راحات">
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="IsInvoiced" Key="IsInvoiced" DataType="System.Boolean"
                                                                    Type="CheckBox" Width="15%">
                                                                    <Header Caption="على العميل">
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn Key="IsDel" DataType="System.Boolean" Type="CheckBox" Width="80px">
                                                                    <Header Caption="حذف">
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="OrgID" Width="0px">
                                                                    <Header Caption="ID">
                                                                        <RowLayoutColumnInfo OriginX="9" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="9" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Remarks" Hidden="True" Key="Remarks" Width="0px">
                                                                    <Header Caption="Remarks">
                                                                        <RowLayoutColumnInfo OriginX="9" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="9" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
                                            </td>
                                            <td style="width: 2%; vertical-align: top">
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                        <tr>
                                            <td style="width: 5px">
                                            </td>
                                            <td style="width: 40px; text-align: center;">
                                                <asp:ImageButton ID="ImageButton1" runat="server" Height="18px" Width="24px" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/SaveN.png" />
                                            </td>
                                            <td style="width: 5px">
                                            </td>
                                            <td style="width: 40px; text-align: center;">
                                            </td>
                                            <td style="width: 5px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 40px; text-align: center;">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Enabled="false" Text="تخطيط الدوامات">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: right; width: 50%">
                                                <asp:LinkButton ID="LinkButton15" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="السابق"></asp:LinkButton>
                                            </td>
                                            <td style="height: 18px; text-align: left; width: 50%">
                                                <asp:LinkButton ID="LinkButton16" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="إنهاء"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 100%; vertical-align: top" colspan="3">
                                                <div>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 10px;">
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="Label24" runat="server" SkinID="Label_DefaultNormal" Text="إختر الموقع"
                                                                    Width="80px"></asp:Label>
                                                            </td>
                                                            <td style="width: 39%;">
                                                                <asp:DropDownList ID="ddlLocation1" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="Label26" runat="server" SkinID="Label_DefaultNormal" Text="إختر الموقع الفرعى"
                                                                    Width="80px"></asp:Label>
                                                            </td>
                                                            <td style="width: 39%;">
                                                                <asp:DropDownList ID="ddlsublocation1" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 5px;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 10px;">
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="Label27" runat="server" SkinID="Label_DefaultNormal" Text="إختر الوظيفة"
                                                                    Width="80px"></asp:Label>
                                                            </td>
                                                            <td style="width: 39%;">
                                                                <asp:DropDownList ID="ddlPosition" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="Label29" runat="server" SkinID="Label_DefaultNormal" Text="خطة الدوام"
                                                                    Width="80px"></asp:Label>
                                                            </td>
                                                            <td style="width: 39%;">
                                                                <asp:DropDownList ID="ddlAttendancetable" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 5px;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; vertical-align: top; text-align: center" colspan="3">
                                                <asp:Label ID="LblCnt1" runat="server" Font-Bold="True" Font-Size="12pt" Font-Underline="True"
                                                    ForeColor="#FF3300"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%; vertical-align: top">
                                            </td>
                                            <td style="width: 96%; vertical-align: top">
                                                <asp:Label ID="Label28" runat="server" SkinID="Label_CopyRightsNormal" Text="تعديل مخطط الدوام أو الاعدادا فى الورديات سوف يستتبعة تعديل فى جداول الدوامات"></asp:Label>
                                                <br />
                                                <asp:Label ID="LabelAltNote" runat="server" SkinID="Label_CopyRightsBold" Text="يتم توزيع فقط الموظفين الاساسيين على الورديات دون بدلاء الراحات"></asp:Label>
                                                <igtbl:UltraWebGrid   Browser="UpLevel"  ID="uwglocationshift" runat="server" EnableAppStyling="True"
                                                    Height="150px" SkinID="Default" Width="100%" EnableTheming="True">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                                        AllowUpdateDefault="Yes" AutoGenerateColumns="False" BorderCollapseDefault="Separate"
                                                        HeaderClickActionDefault="SortSingle" Name="uwglocationshift" RowHeightDefault="18px"
                                                        SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                        TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" AllowAddNewDefault="Yes"
                                                        CellClickActionDefault="Edit">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="150px"
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
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="0px">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ArbName" Key="ArbName" Width="50%" AllowUpdate="No">
                                                                    <Header Caption="توصيف الدوام">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="TimeIn" Key="TimeIn" Width="10%" AllowUpdate="No">
                                                                    <Header Caption="من دوام">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="TimeOut" Key="TimeOut" Width="10%" AllowUpdate="No">
                                                                    <Header Caption="الى دوام">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Qty" Key="Qty" Width="30%" DataType="System.Int32">
                                                                    <Header Caption="العدد">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="OrgID" Width="0px">
                                                                    <Header Caption="OrgID">
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
                                            <td style="width: 2%; vertical-align: top">
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="T1" visible="False" cellspacing="6" style="width: 100%;
                                        vertical-align: bottom; border-bottom: 1px solid black">
                                        <tr runat="server">
                                            <td colspan="7" runat="server">
                                                <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Size="12pt" Font-Underline="True"
                                                    ForeColor="#FF3300" Text="إختر أيام العطلة الأسبوعية" SkinID="Label_WarningBold"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr runat="server">
                                            <td style="vertical-align: bottom" class="style1" runat="server">
                                                <asp:CheckBox ID="Sat" runat="server" Text="السبت" />
                                            </td>
                                            <td style="vertical-align: bottom" class="style1" runat="server">
                                                <asp:CheckBox ID="Sun" runat="server" Text="الأحد" />
                                            </td>
                                            <td style="vertical-align: bottom" class="style1" runat="server">
                                                <asp:CheckBox ID="Mon" runat="server" Text="الإثنين" />
                                            </td>
                                            <td style="vertical-align: bottom" class="style1" runat="server">
                                                <asp:CheckBox ID="Tue" runat="server" Text="الثلاثاء" />
                                            </td>
                                            <td style="vertical-align: bottom" class="style1" runat="server">
                                                <asp:CheckBox ID="Wed" runat="server" Text="الأربعاء" />
                                            </td>
                                            <td style="vertical-align: bottom" class="style1" runat="server">
                                                <asp:CheckBox ID="Thu" runat="server" Text="الخميس" />
                                            </td>
                                            <td style="vertical-align: bottom" class="style1" runat="server">
                                                <asp:CheckBox ID="Fri" runat="server" Text="الجمعة" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                        <tr>
                                            <td style="width: 5px">
                                            </td>
                                            <td style="width: 40px; text-align: center;">
                                                <asp:ImageButton ID="btnsave1" runat="server" Height="18px" Width="24px" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/SaveN.png" />
                                            </td>
                                            <td style="width: 5px">
                                            </td>
                                            <td style="width: 40px; text-align: center;">
                                            </td>
                                            <td style="width: 5px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 40px; text-align: center;">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton_Show" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="عرض جدول الدوامات الحالى" OnClientClick="OpenScreen(); return false;"></asp:LinkButton>
                                                &nbsp; &nbsp; &nbsp;
                                                <asp:LinkButton ID="LinkButton_GenerateOne" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="توليد جدول الدوامات الحالى"></asp:LinkButton>
                                                &nbsp; &nbsp; &nbsp;
                                                <asp:LinkButton ID="LinkButton_Generate" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="توليد جدول دوامات كامل المشروع"></asp:LinkButton>
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
