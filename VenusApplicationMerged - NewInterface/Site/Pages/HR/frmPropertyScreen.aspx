<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPropertyScreen.aspx.vb"
    Inherits="frmPropertyScreen" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Property Screen</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmPropertyScreen" runat="server">
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
    <div class="Div_MasterContainer" runat="server" id="DIV" style="background-color: #BEDCFF;
        width: 800px; height: 600px;">
        <table style="width: 800px;">
            <tr>
                <td style="width: 100%;">
                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                        <tr>
                            <td>
                                &nbsp;&nbsp;
                                <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png"
                                    meta:resourcekey="Image_LogoResource1" />
                                <asp:Label ID="Label_Header" runat="server" meta:resourcekey="Label_HeaderResource1"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; height: 500px; vertical-align: top;" cellspacing="0">
                        <tr>
                            <td style="width: 2%;">
                            </td>
                            <td style="width: 96%; height: 500px">
                                <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                                    meta:resourcekey="UltraWebTab1Resource1" Height="212px" Width="100%">
                                    <Tabs>
                                        <igtab:Tab Text="خصائص السجل" meta:resourcekey="TabResource1">
                                            <ContentTemplate>
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 2%; height: 20px">
                                                        </td>
                                                        <td style="width: 35%;">
                                                        </td>
                                                        <td style="width: 56%;">
                                                        </td>
                                                        <td style="width: 2%;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2%; height: 25px">
                                                        </td>
                                                        <td style="width: 35%;">
                                                            <asp:Label ID="lblEngName" runat="server" Text="تاريخ إلغاء السجل" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 56%;">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 130px;">
                                                                        <igsch:WebDateChooser ID="WebDateChooser1" runat="server" meta:resourcekey="txtPasswordChangedOnResource1"
                                                                            Value="06/23/2012 10:15:58" Width="130px">
                                                                        </igsch:WebDateChooser>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="CheckBox1" runat="server" Text=" " meta:resourcekey="CheckBox1Resource1" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 2%;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2%; height: 25px">
                                                        </td>
                                                        <td style="width: 35%;">
                                                            <asp:Label ID="Label6" runat="server" Text="تاريخ التسجيل" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="Label6Resource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 56%;">
                                                            <asp:Label ID="lblRegistrationDate" runat="server" Width="155px" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblRegistrationDateResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 2%;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2%; height: 25px">
                                                        </td>
                                                        <td style="width: 35%;">
                                                            <asp:Label ID="Label7" runat="server" Text="المسجل" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="Label7Resource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 56%;">
                                                            <asp:Label ID="lblRegestreduser" runat="server" Width="155px" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblRegestreduserResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 2%;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2%; height: 25px">
                                                        </td>
                                                        <td style="width: 35%;">
                                                            <asp:Label ID="Label8" runat="server" Text="رقم السجل" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="Label8Resource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 56%;">
                                                            <asp:Label ID="lblRecordId" runat="server" Width="155px" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblRecordIdResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 2%;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                        </td>
                                                        <td class="style2">
                                                            <asp:Label ID="Label9" runat="server" Text="تاريخ إلغاء السجل" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="Label9Resource1"></asp:Label>
                                                        </td>
                                                        <td class="style3">
                                                            <asp:Label ID="lblRecordCancelDate" runat="server" Width="155px" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblRecordCancelDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="style1">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2%; height: 25px">
                                                        </td>
                                                        <td style="width: 35%;">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 56%;">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 2%;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2%; height: 25px">
                                                        </td>
                                                        <td style="width: 35%;">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 56%;">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 2%;">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </igtab:Tab>
                                        <igtab:Tab Text="المحفوظات" meta:resourceKey="TabResource2">
                                            <ContentTemplate>
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 100%; vertical-align: top">
                                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="UwgPropertyPage" runat="server" EnableAppStyling="False"
                                                                Height="100%" SkinID="Default" Width="100%" meta:resourcekey="UwgPropertyPageResource1">
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                    BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgOnlyForProfession"
                                                                    RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
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
                                                                    <ClientSideEvents ClickCellButtonHandler="UwgSearchUsers_ClickCellButtonHandler" />
                                                                </DisplayLayout>
                                                                <Bands>
                                                                    <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
                                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                                        </AddNewRow>
                                                                        <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                                CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                Font-Size="11px" Width="200px">
                                                                                <Padding Left="2px" />
                                                                            </FilterDropDownStyle>
                                                                            <FilterHighlightRowStyle ForeColor="White" BackColor="#151C55" />
                                                                        </FilterOptions>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ColumnName" Key="Field Name" Width="190px"
                                                                                meta:resourcekey="UltraGridColumnResource1">
                                                                                <Header Caption="Field Name">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="OldData" Key="Old Data" Width="150px" meta:resourcekey="UltraGridColumnResource2">
                                                                                <Header Caption="Old Data">
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="UserEngName" Key="User" Width="190px" meta:resourcekey="UltraGridColumnResource3">
                                                                                <Header Caption="User">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="RegDate" Key="Date" Width="200px" meta:resourcekey="UltraGridColumnResource4">
                                                                                <Header Caption="Date">
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
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
                            <td style="width: 2%;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <table style="width: 100%; height: 30px; vertical-align: top" cellspacing="0">
                <tr>
                    <td style="width: 100%; text-align: center; vertical-align: middle;">
                        <igtxt:WebImageButton ID="btnAddField" runat="server" Height="10px" meta:resourcekey="WebImageButton1Resource1"
                            Overflow="NoWordWrap" Text="موافق" UseBrowserDefaults="False" Width="100px">
                            <Alignments TextImage="TextRightImageLeft" VerticalImage="Middle" />
                            <Appearance>
                                <InnerBorder ColorBottom="160, 160, 160" ColorLeft="White" ColorRight="160, 160, 160"
                                    ColorTop="White" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid"
                                    WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                            </Appearance>
                        </igtxt:WebImageButton>
                    </td>
                </tr>
            </table>
        </table>
    </div>
    </form>
</body>
</html>
