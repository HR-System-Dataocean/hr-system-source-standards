<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEvalEvaluationTypes_Popup.aspx.vb"
    Inherits="frmEvalEvaluationTypes_Popup" Culture="auto" meta:resourcekey="PageResource1"
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
    <title>* Venus Payroll * ~Evaluation Types</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script type="text/javascript" id="Infragistics">
        function uwg_AfterExitEditModeHandler(gridName, cellId) {
            var count = igtbl_getGridById(gridName).Rows.length - 1;
            var rowIndex = igtbl_getRowById(cellId).Id.split("_")[2];
            if (rowIndex == count) {
                igtbl_addNew(gridName, 0, true, false);
            }
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmEmployeesDocuments" runat="server">
    <div style="display: none">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <igtxt:WebTextEdit meta:resourcekey="WebTextEdit1Recourcekey" ID="WebTextEdit1" runat="server">
        </igtxt:WebTextEdit>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td class="Details">
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                        meta:resourcekey="UltraWebTab1Resource1">
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
                                                            <igtxt:WebImageButton ID="btnSave" runat="server" Style="cursor: pointer;" Height="18px"
                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnSaveResource1">
                                                                <Alignments TextImage="ImageBottom" />
                                                                <Appearance>
                                                                    <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/SaveN.png" />
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
                                                        </td>
                                                        <td style="width: 5px">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 40px; text-align: center;">
                                                            &nbsp;
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 18px">
                                                <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                    <tr>
                                                        <td style="width: 5px">
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblUser" runat="server" Width="80px" SkinID="Label_CopyRightsBold"
                                                                Text="Code" meta:resourcekey="lblUserResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDescEmployeeCode" runat="server" Width="80px" SkinID="Label_CopyRightsNormal"
                                                                meta:resourcekey="lblDescEmployeeCodeResource1"></asp:Label>
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
                                        <tr>
                                            <td>
                                                <div id="Div1" runat="server" visible="False">
                                                    <table style="width: 100%; height: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                                    cellspacing="6">
                                                                    <tr>
                                                                        <td style="vertical-align: bottom">
                                                                            <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_Title3Resource1" SkinID="Label_DefaultBold"
                                                                                Text="Evaluations Scales"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 6%; vertical-align: top;">
                                                            </td>
                                                            <td style="width: 47%; vertical-align: top;">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top" colspan="3">
                                                                <igtbl:UltraWebGrid  Browser="UpLevel"  ID="uwgEvalScales" runat="server" EnableAppStyling="True" Height="350px"
                                                                    meta:resourcekey="uwgEvalScalesResource1" SkinID="Default" Width="100%">
                                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                                        RowHeightDefault="18px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                                                                        AllowRowNumberingDefault="Continuous">
                                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="350px"
                                                                            Width="100%">
                                                                        </FrameStyle>
                                                                        <ClientSideEvents AfterExitEditModeHandler="uwg_AfterExitEditModeHandler" />
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
                                                                        <igtbl:UltraGridBand AllowAdd="Yes" meta:resourcekey="UltraGridBandResource1">
                                                                            <RowEditTemplate>
                                                                                <p align="right">
                                                                                    <br />
                                                                                </p>
                                                                                <br />
                                                                                <p align="center">
                                                                                </p>
                                                                            </RowEditTemplate>
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
                                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" meta:resourcekey="UltraGridColumnResource1"
                                                                                    Width="0px">
                                                                                    <Header Caption="ID">
                                                                                    </Header>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn BaseColumnName="EngName" meta:resourcekey="UltraGridColumnResource2"
                                                                                    Width="50%">
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <Header Caption="Scale English" Title="Answer_Eng">
                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn BaseColumnName="ArbName" EditorControlID="WebTextEdit1" meta:resourcekey="UltraGridColumnResource3"
                                                                                    Type="Custom" Width="50%">
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <Header Caption="Scale Arabic" Title="Answer_Arb">
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn BaseColumnName="DFrom" DataType="System.Int32" Key="DFrom"
                                                                                    meta:resourcekey="UltraGridColumnResource4" Width="50px">
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <CellStyle HorizontalAlign="Right">
                                                                                    </CellStyle>
                                                                                    <Header Caption="From" Title="APower">
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn BaseColumnName="DTo" DataType="System.Int32" meta:resourcekey="UltraGridColumnResource5"
                                                                                    Width="50px">
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <CellStyle HorizontalAlign="Right">
                                                                                    </CellStyle>
                                                                                    <Header Caption="To">
                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                            </Columns>
                                                                            <RowTemplateStyle BackColor="Transparent" BorderColor="Transparent" BorderStyle="Ridge">
                                                                                <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                                            </RowTemplateStyle>
                                                                        </igtbl:UltraGridBand>
                                                                    </Bands>
                                                                </igtbl:UltraWebGrid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div id="Div2" runat="server" visible="False">
                                                    <table style="width: 100%; height: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                                    cellspacing="6">
                                                                    <tr>
                                                                        <td style="vertical-align: bottom">
                                                                            <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_Title1Resource1" SkinID="Label_DefaultBold"
                                                                                Text="Target Modules"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 6%; vertical-align: top;">
                                                            </td>
                                                            <td style="width: 47%; vertical-align: top;">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top" colspan="3">
                                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgModules" runat="server" EnableAppStyling="True" Height="350px"
                                                                    meta:resourcekey="uwgModulesResource1" SkinID="Default" Width="100%">
                                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                                        RowHeightDefault="18px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                                                                        AllowRowNumberingDefault="Continuous">
                                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="350px"
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
                                                                        <RowSelectorStyleDefault Font-Size="7pt" Font-Strikeout="False">
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
                                                                        <igtbl:UltraGridBand AllowAdd="No" meta:resourcekey="UltraGridBandResource2">
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
                                                                                <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Int32" Hidden="True"
                                                                                    meta:resourcekey="UltraGridColumnResource6">
                                                                                    <Header Caption="Column Name">
                                                                                    </Header>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowResize="Fixed" AllowUpdate="No" meta:resourcekey="UltraGridColumnResource7"
                                                                                    Width="80%">
                                                                                    <Header Caption="Module Name">
                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                    </Header>
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="IsCriteria" CellButtonDisplay="Always"
                                                                                    DataType="System.Boolean" meta:resourcekey="UltraGridColumnResource8" Type="CheckBox"
                                                                                    Width="20%">
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <Header Caption="Check">
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Header>
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
                                                </div>
                                                <div id="Div3" runat="server" visible="False">
                                                    <table style="width: 100%; height: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                                    cellspacing="6">
                                                                    <tr>
                                                                        <td style="vertical-align: bottom">
                                                                            <asp:Label ID="Label_Title1" runat="server" meta:resourcekey="Label_Title2Resource1"
                                                                                SkinID="Label_DefaultBold" Text="Evaluations Characteristics"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 6%; vertical-align: top;">
                                                            </td>
                                                            <td style="width: 47%; vertical-align: top;">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top" colspan="3">
                                                                <igtbl:UltraWebGrid   Browser="UpLevel"  ID="uwgGroups" runat="server" EnableAppStyling="True" Height="160px"
                                                                    meta:resourcekey="uwgGroupsResource1" SkinID="Default" Width="100%">
                                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                                        RowHeightDefault="18px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                                                                        AllowRowNumberingDefault="Continuous">
                                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="160px"
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
                                                                        <RowSelectorStyleDefault Font-Size="7pt" Font-Strikeout="False">
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
                                                                        <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource3">
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
                                                                                <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="IsCriteria" CellButtonDisplay="Always"
                                                                                    DataType="System.Boolean" meta:resourcekey="UltraGridColumnResource9" Type="CheckBox"
                                                                                    Width="60px">
                                                                                    <Header Caption="Check">
                                                                                    </Header>
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Int32" Hidden="True"
                                                                                    Key="ID" meta:resourcekey="UltraGridColumnResource10">
                                                                                    <Header Caption="ID">
                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowResize="Fixed" AllowUpdate="No" meta:resourcekey="UltraGridColumnResource11"
                                                                                    Width="100%">
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <Header Caption="Characteristics Name">
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn BaseColumnName="GPower" DataType="System.Int32" Key="GPower"
                                                                                    meta:resourcekey="UltraGridColumnResource12" Width="60px">
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <CellStyle HorizontalAlign="Right">
                                                                                    </CellStyle>
                                                                                    <Header Caption="Power">
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn BaseColumnName="RegComputerID" DataType="System.Int32" Key="RegComputerID"
                                                                                    meta:resourcekey="UltraGridColumnResource13" Width="60px">
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <CellStyle HorizontalAlign="Right">
                                                                                    </CellStyle>
                                                                                    <Header Caption="Rank">
                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                            </Columns>
                                                                        </igtbl:UltraGridBand>
                                                                    </Bands>
                                                                </igtbl:UltraWebGrid>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                                    cellspacing="6">
                                                                    <tr>
                                                                        <td style="vertical-align: bottom">
                                                                            <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_Title4Resource1" SkinID="Label_DefaultBold"
                                                                                Text="Evaluations Characteristics"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 6%; vertical-align: top;">
                                                            </td>
                                                            <td style="width: 47%; vertical-align: top;">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top" colspan="3">
                                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgGroupsQuestions" runat="server" EnableAppStyling="True"
                                                                    Height="160px" meta:resourcekey="uwgGroupsQuestionsResource1" SkinID="Default"
                                                                    Width="100%">
                                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgOnlyForProfession"
                                                                        RowHeightDefault="18px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                                                                        AllowRowNumberingDefault="Continuous">
                                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="160px"
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
                                                                        <RowSelectorStyleDefault Font-Size="7pt" Font-Strikeout="False">
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
                                                                        <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource4">
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
                                                                                <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Int32" Hidden="True"
                                                                                    Key="ID" meta:resourcekey="UltraGridColumnResource14">
                                                                                    <Header Caption="ID">
                                                                                    </Header>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowResize="Fixed" AllowUpdate="No" BaseColumnName="GroupID"
                                                                                    DataType="System.Int32" Hidden="True" Key="GroupID" meta:resourcekey="UltraGridColumnResource15">
                                                                                    <Header Caption="Group_ID">
                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn BaseColumnName="QuestionID" DataType="System.Int32" Hidden="True"
                                                                                    Key="QuestionID" meta:resourcekey="UltraGridColumnResource16">
                                                                                    <Header Caption="QuestionID">
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="No" meta:resourcekey="UltraGridColumnResource17"
                                                                                    Width="100%">
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <Header Caption="Element Content">
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="QPower" DataType="System.Int32"
                                                                                    Key="QPower" meta:resourcekey="UltraGridColumnResource18" Width="60px">
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <CellStyle HorizontalAlign="Right">
                                                                                    </CellStyle>
                                                                                    <Header Caption="Power">
                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn BaseColumnName="RegComputerID" DataType="System.Int32" Key="RegComputerID"
                                                                                    meta:resourcekey="UltraGridColumnResource19" Width="60px">
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <CellStyle HorizontalAlign="Right">
                                                                                    </CellStyle>
                                                                                    <Header Caption="Rank">
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
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%">
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
