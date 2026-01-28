<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSearchScreen.aspx.vb"
    Inherits="Interfaces_frmSearchScreen" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbar" Namespace="Infragistics.WebUI.UltraWebToolbar" Assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search</title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <link href="app_styles.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <link href="~/ig_common/20071CLR20/Styles/Office2007Blue/ig_WebGrid.css " rel="Stylesheet"
        type="text/css" />
    <link href="~/ig_common/20071CLR20/Styles/Office2007Blue/ig_WebPanel.css " rel="Stylesheet"
        type="text/css" />
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div>
        &nbsp;
        <div align="left" style="border-right: 1px outset; border-top: 1px outset; z-index: 104;
            left: 0px; border-left: 1px outset; width: 724px; border-bottom: 1px outset;
            position: absolute; top: 0px; height: 615px; background-color: #bfdbff" id="DIV1"
            runat="server">
            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="UwgSearch" runat="server"  Height="250px" Style="z-index: 100;
                left: 6px; position: absolute; top: 238px; height: 250px;" Width="711px" meta:resourcekey="UwgSearchResource1">
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
                    </igtbl:UltraGridBand>
                </Bands>
                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                    BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect" GridLinesDefault="NotSet"
                    HeaderClickActionDefault="SortMulti" Name="UwgSearch" RowHeightDefault="20px"
                    RowSelectorsDefault="No" SelectTypeRowDefault="Extended" Version="4.00"  
                    ViewType="Hierarchical" LoadOnDemand="Automatic" TableLayout="Fixed" StationaryMargins="Header">
                    <GroupByBox>
                        <style backcolor="ActiveBorder" bordercolor="Window"></style>
                        <BandLabelStyle CssClass="igwgGrpBoxBandLblBlue2k7">
                        </BandLabelStyle>
                        <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                        </BoxStyle>
                    </GroupByBox>
                    <GroupByRowStyleDefault CssClass="igwgGrpRowBlue2k7">
                    </GroupByRowStyleDefault>
                    <ActivationObject BorderColor="181, 196, 223" BorderWidth="">
                        <BorderDetails WidthLeft="0px" WidthRight="0px" />
                    </ActivationObject>
                    <FooterStyleDefault CssClass="igwgFooterBlue2k7">
                    </FooterStyleDefault>
                    <RowStyleDefault CssClass="igwgRowBlue2k7">
                        <BorderDetails WidthBottom="1px" WidthTop="1px" />
                        <Padding Left="3px" />
                    </RowStyleDefault>
                    <FilterOptionsDefault DropDownRowCount="15">
                        <FilterDropDownStyle CssClass="igwgFltrDrpDwnBlue2k7">
                        </FilterDropDownStyle>
                        <FilterHighlightRowStyle CssClass="igwgFltrRowHiLtBlue2k7">
                        </FilterHighlightRowStyle>
                    </FilterOptionsDefault>
                    <ClientSideEvents DblClickHandler="MainSearch_DblClickHandler" KeyDownHandler="MainSearch_KeyDownHandler"
                        AfterRowActivateHandler="UwgSearch_AfterRowActivateHandler" XmlVirtualScrollHandler="UwgSearch_XmlVirtualScrollHandler" />
                    <SelectedRowStyleDefault CssClass="igwgRowSelBlue2k7">
                    </SelectedRowStyleDefault>
                    <HeaderStyleDefault CssClass="SearchHeadColors" Height="23px">
                    </HeaderStyleDefault>
                    <RowAlternateStyleDefault CssClass="igwgRowAltBlue2k7">
                    </RowAlternateStyleDefault>
                    <EditCellStyleDefault CssClass="igwgCellEdtBlue2k7">
                    </EditCellStyleDefault>
                    <FrameStyle BorderStyle="Outset" Height="250px" Width="711px" CssClass="igwgFrameBlue2k7"
                        BorderColor="#FFC080" BorderWidth="1px">
                    </FrameStyle>
                    <Pager PagerAppearance="Both" PageSize="10" StyleMode="CustomLabels">
                        <style backcolor="LightGray" borderstyle="Solid" borderwidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</style>
                        <PagerStyle BackColor="LightGray" BorderWidth="1px" BorderStyle="Solid">
                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px">
                            </BorderDetails>
                        </PagerStyle>
                    </Pager>
                    <AddNewBox>
                        <style backcolor="Window" bordercolor="InactiveCaption" borderstyle="Solid" borderwidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</style>
                        <ButtonStyle CssClass="igwgAddNewBtnBlue2k7">
                        </ButtonStyle>
                        <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" BorderStyle="Solid">
                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px">
                            </BorderDetails>
                        </BoxStyle>
                    </AddNewBox>
                    <RowExpAreaStyleDefault CssClass="igwgRowExpBlue2k7">
                    </RowExpAreaStyleDefault>
                    <SelectedGroupByRowStyleDefault CssClass="igwgGrpRowSelBlue2k7">
                    </SelectedGroupByRowStyleDefault>
                    <RowSelectorStyleDefault CssClass="igwgRowSlctrBlue2k7">
                    </RowSelectorStyleDefault>
                    <FixedHeaderStyleDefault CssClass="igwgHdrFxdBlue2k7">
                    </FixedHeaderStyleDefault>
                    <Images ImageDirectory="~/ig_common/20071CLR20/Styles/Office2007Blue/WebGrid/">
                    </Images>
                    <SelectedHeaderStyleDefault CssClass="igwgHdrSelBlue2k7">
                    </SelectedHeaderStyleDefault>
                    <FormulaErrorStyleDefault CssClass="igwgFormulaErrBlue2k7">
                    </FormulaErrorStyleDefault>
                    <FixedCellStyleDefault CssClass="igwgCellFxdBlue2k7">
                    </FixedCellStyleDefault>
                    <FixedFooterStyleDefault CssClass="igwgFtrFxdBlue2k7">
                    </FixedFooterStyleDefault>
                </DisplayLayout>
            </igtbl:UltraWebGrid>
            <asp:Label ID="lblMainHeader" runat="server" Font-Size="Small" Height="27px" Style="z-index: 101;
                left: 6px; position: absolute; top: 7px" Width="711px" CssClass="SearchHeadColors"
                meta:resourcekey="lblMainHeaderResource1" BorderColor="#8080FF" BorderStyle="Solid"
                BorderWidth="1px" ForeColor="Black"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <igtxt:WebImageButton ID="btnSearch" runat="server" Style="z-index: 106; left: 687px;
                position: absolute; top: 13px" UseBrowserDefaults="False" Width="27px" Height="21px"
                meta:resourcekey="btnSearchResource1">
                <Appearance>
                    <Image Url="./Img/forum_search.gif" />
                    <style font-bold="True"></style>
                </Appearance>
                <ClientSideEvents Click="MainSearch_btn_Click" />
                <FocusAppearance>
                    <style font-bold="False" font-italic="False" font-overline="False" font-strikeout="False"
                        font-underline="False"></style>
                </FocusAppearance>
                <HoverAppearance>
                    <style font-bold="False" font-italic="False" font-overline="False" font-strikeout="False"
                        font-underline="False"></style>
                </HoverAppearance>
                <DisabledAppearance>
                    <style font-bold="False" font-italic="False" font-overline="False" font-strikeout="False"
                        font-underline="False"></style>
                </DisabledAppearance>
                <PressedAppearance ContentShift="None">
                    <style font-bold="False" font-italic="False" font-overline="False" font-strikeout="False"
                        font-underline="False"></style>
                </PressedAppearance>
            </igtxt:WebImageButton>
            <asp:Label ID="name" runat="server" Style="z-index: 103; left: 609px; position: absolute;
                top: 101px" Width="99px" ForeColor="White" TabIndex="-1" meta:resourcekey="nameResource1"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:Panel ID="pnlCriterias" runat="server" BackColor="#FFC080" Height="192px" Style="left: 6px;
                position: absolute; top: 35px; z-index: 104;" Width="711px" BorderColor="#8080FF"
                BorderWidth="1px" meta:resourcekey="pnlCriteriasResource1" BorderStyle="Solid">
            </asp:Panel>
            <asp:TextBox ID="txtSearchAll" runat="server" Height="16px" Style="z-index: 105;
                left: 584px; position: absolute; top: 11px" Width="124px"></asp:TextBox>
        </div>
        <asp:Label ID="realname" runat="server" Style="z-index: 100; left: 610px; position: absolute;
            top: 128px" Width="99px" ForeColor="White" TabIndex="-1" meta:resourcekey="realnameResource1"></asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="value" runat="server" Style="z-index: 101; left: 612px; position: absolute;
            top: 155px" Width="91px" BorderStyle="None" ForeColor="White" TabIndex="-1" meta:resourcekey="valueResource1"></asp:TextBox>
        &nbsp;
        <asp:Label ID="TargetControl" runat="server" Style="z-index: 102; left: 613px; position: absolute;
            top: 184px" Width="99px" ForeColor="White" TabIndex="-1" meta:resourcekey="TargetControlResource1"></asp:Label>
        <asp:HiddenField ID="txtRankofCodeCell" runat="server" />
        <asp:Image ID="Image1" runat="server" Height="16px" ImageUrl="~/Interfaces/Form.bmp"
            Style="z-index: 105; left: 119px; position: absolute; top: 14px" Width="14px" />
    </div>
    </form>
</body>
</html>
