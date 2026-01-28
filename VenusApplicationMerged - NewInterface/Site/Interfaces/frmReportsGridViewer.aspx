<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmReportsGridViewer.aspx.vb"
    Inherits="Interfaces_frmReportsGridViewer" %>

<%@ Register TagPrefix="igcalc" Namespace="Infragistics.WebUI.UltraWebCalcManager"
    Assembly="Infragistics35.WebUI.UltraWebCalcManager.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbar" Namespace="Infragistics.WebUI.UltraWebToolbar" Assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Viewer</title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <link href="app_styles.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="app_rpwscripts_rviewer.js"></script>
    <script language="javascript" src="App_RpwScripts.js"></script>
    <script language="javascript" type="text/javascript" src="App_JScript_frmReportsGridViewer.js"></script>
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" style="background-color: white"
    bgcolor="white" onload="LoadGridColumnsStyles()">
    <form id="frmReportsSettings" runat="server">
    <div>
        &nbsp;
        <div align="left" style="border-right: 1px outset; border-top: 1px outset; z-index: 101;
            left: 0px; border-left: 1px outset; width: 100%; border-bottom: 1px outset; position: absolute;
            top: 0px; height: 736px; background-color: whitesmoke" id="DIV1">
            &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
            &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <igtbl:UltraWebGrid   ID="uwgViewColumns" runat="server" meta:resourcekey="uwgcriteriaResource1"
                Style="z-index: 106; left: 10px; position: absolute; top: 321px; height: 347px;"
                Width="98%" Browser="Xml">
                <Bands>
                    <igtbl:UltraGridBand>
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
                            <igtbl:UltraGridColumn HeaderText="Column 0">
                                <Header Caption="Column 0">
                                </Header>
                            </igtbl:UltraGridColumn>
                        </Columns>
                    </igtbl:UltraGridBand>
                </Bands>
                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnClient" AllowSortingDefault="OnClient"
                    AutoGenerateColumns="False" BorderCollapseDefault="Separate" CellClickActionDefault="NotSet"
                    HeaderClickActionDefault="SortMulti" Name="uwgViewColumns" RowHeightDefault="20px"
                    RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="HeaderAndFooter"
                    StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ColFootersVisibleDefault="Yes"
                    SelectTypeColDefault="Single">
                    <GroupByBox>
                        <style backcolor="ActiveBorder" bordercolor="Window"></style>
                    </GroupByBox>
                    <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                    </GroupByRowStyleDefault>
                    <ActivationObject BorderColor="" BorderStyle="None" BorderWidth="">
                    </ActivationObject>
                    <FooterStyleDefault BackColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px">
                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                    </FooterStyleDefault>
                    <RowStyleDefault BackColor="White" BorderColor="InactiveCaptionText" BorderStyle="None"
                        BorderWidth="1px">
                        <BorderDetails ColorLeft="Window" ColorTop="Window" />
                        <Padding Left="3px" />
                    </RowStyleDefault>
                    <FilterOptionsDefault>
                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                        </FilterHighlightRowStyle>
                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                            CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                            Font-Size="11px" Height="300px" Width="200px">
                            <Padding Left="2px" />
                        </FilterDropDownStyle>
                    </FilterOptionsDefault>
                    <SelectedRowStyleDefault BackColor="#FF8000">
                    </SelectedRowStyleDefault>
                    <HeaderStyleDefault BackColor="#FF8000" BorderStyle="Outset" BorderWidth="2px" Font-Bold="True"
                        ForeColor="White" HorizontalAlign="Center" Cursor="Hand" Height="27px">
                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                    </HeaderStyleDefault>
                    <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                    </EditCellStyleDefault>
                    <FrameStyle BackColor="White" BorderColor="Silver" BorderStyle="Ridge" Font-Names="Microsoft Sans Serif"
                        Font-Size="8.25pt" Height="445px" Width="98%" BorderWidth="2px">
                    </FrameStyle>
                    <Pager MinimumPagesForDisplay="1" PagerAppearance="Top" PageSize="25" StyleMode="PrevNext"
                        ChangeLinksColor="True">
                        <style backcolor="LightGray" borderstyle="Solid" borderwidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</style>
                    </Pager>
                    <AddNewBox Hidden="False">
                        <style backcolor="Window" bordercolor="InactiveCaption" borderstyle="Solid" borderwidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</style>
                    </AddNewBox>
                    <ClientSideEvents AfterColumnMoveHandler="uwgViewColumns_AfterColumnMoveHandler"
                        AfterColumnSizeChangeHandler="uwgFormParameters_AfterColumnSizeChangeHandler"
                        AfterSortColumnHandler="uwgViewColumns_AfterSortColumnHandler" />
                </DisplayLayout>
            </igtbl:UltraWebGrid>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <igtbar:UltraWebToolbar ID="TlbMainToolbar" runat="server" BackColor="PowderBlue"
                Font-Bold="False" Font-Names="Times New Roman" Font-Size="Small" ForeColor="White"
                Height="32px" ImageDirectory="/ig_common/images/" JavaScriptFileName="" JavaScriptFileNameCommon=""
                meta:resourcekey="TlbMainToolbarResource1" MovableImage="ig_tb_move00.gif" Style="z-index: 101;
                left: 0px; position: absolute; top: 0px" Width="100%" BorderStyle="Outset" BorderWidth="1px">
                <HoverStyle BackColor="#64799C" BorderStyle="Solid" ForeColor="White" BorderColor="Blue">
                    <BorderDetails WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                </HoverStyle>
                <DefaultStyle BorderStyle="Solid" ForeColor="White" BackColor="#7288AC">
                    <BorderDetails ColorBottom="114, 136, 172" ColorLeft="114, 136, 172" ColorRight="114, 136, 172"
                        ColorTop="114, 136, 172" WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                </DefaultStyle>
                <Items>
                    <igtbar:TBSeparator />
                    <igtbar:TBarButton DisabledImage="" HoverImage="" Image="./print_16.gif" ImageAlign="NotSet"
                        Key="HTML" SelectedImage="" Text="HTML">
                        <Images>
                            <DefaultImage Url="./print_16.gif" />
                        </Images>
                        <DefaultStyle BackColor="Transparent" CssClass="DefultToolBarButton" Width="80px">
                        </DefaultStyle>
                        <HoverStyle CssClass="HoverToolBarButton" Width="72px">
                        </HoverStyle>
                        <SelectedStyle CssClass="OppositeMainApplicationToolbar" Width="72px">
                        </SelectedStyle>
                    </igtbar:TBarButton>
                    <igtbar:TBSeparator />
                    <igtbar:TBarButton DisabledImage="./acrobat.gif" HoverImage="./acrobat.gif" Image="./acrobat.gif"
                        Key="PDF" SelectedImage="./acrobat.gif" Text="PDF">
                        <Images>
                            <SelectedImage Url="./acrobat.gif" />
                            <HoverImage Url="./acrobat.gif" />
                            <DisabledImage Url="./acrobat.gif" />
                            <DefaultImage Url="./acrobat.gif" />
                        </Images>
                        <DefaultStyle BackColor="Transparent" CssClass="DefultToolBarButton" Width="80px">
                        </DefaultStyle>
                        <HoverStyle CssClass="HoverToolBarButton" Width="72px">
                        </HoverStyle>
                    </igtbar:TBarButton>
                    <igtbar:TBSeparator />
                    <igtbar:TBButtonGroup>
                        <Buttons>
                            <igtbar:TBarButton DisabledImage="" HoverImage="" Image="./blog.gif" Key="Portrait"
                                SelectedImage="" Text="Portrait">
                                <Images>
                                    <DefaultImage Url="./blog.gif" />
                                </Images>
                                <DefaultStyle CssClass="DefultToolBarButton" Width="80px">
                                </DefaultStyle>
                                <HoverStyle CssClass="HoverToolBarButton" Width="72px">
                                </HoverStyle>
                            </igtbar:TBarButton>
                            <igtbar:TBarButton DisabledImage="" HoverImage="" Image="./abook_rdex_1.gif" Key="Landscape"
                                SelectedImage="" Text="Landscape">
                                <Images>
                                    <DefaultImage Url="./abook_rdex_1.gif" />
                                </Images>
                                <DefaultStyle CssClass="DefultToolBarButton" Width="80px">
                                </DefaultStyle>
                                <HoverStyle CssClass="HoverToolBarButton" Width="72px">
                                </HoverStyle>
                            </igtbar:TBarButton>
                        </Buttons>
                        <DefaultStyle BackColor="Transparent" CssClass="DefultToolBarButton" Width="80px">
                        </DefaultStyle>
                        <HoverStyle CssClass="HoverToolBarButton" Cursor="Hand" Width="72px">
                        </HoverStyle>
                    </igtbar:TBButtonGroup>
                    <igtbar:TBSeparator />
                    <igtbar:TBarButton DisabledImage="" HoverImage="./logoff_small.gif" Image="./logoff_small.gif"
                        Key="Close" SelectedImage="./logoff_small.gif" Text="Close">
                        <Images>
                            <SelectedImage Url="./logoff_small.gif" />
                            <HoverImage Url="./logoff_small.gif" />
                            <DefaultImage Url="./logoff_small.gif" />
                        </Images>
                        <DefaultStyle BackColor="Transparent" CssClass="DefultToolBarButton" Width="80px">
                        </DefaultStyle>
                        <HoverStyle CssClass="HoverToolBarButton" Cursor="Hand" Width="72px">
                        </HoverStyle>
                        <SelectedStyle CssClass="OppositeMainApplicationToolbar" Width="72px">
                        </SelectedStyle>
                    </igtbar:TBarButton>
                </Items>
            </igtbar:UltraWebToolbar>
            <asp:Panel ID="pnlCriterias" runat="server" BorderColor="Silver" BorderStyle="Groove"
                BorderWidth="2px" Style="z-index: 99; left: 10px; position: absolute; top: 43px;
                height: 271px;" Width="98%">
                &nbsp;
            </asp:Panel>
            <textarea id="txtColumnsStyles" runat="server" rows="2" style="left: 159px; width: 606px;
                position: absolute; top: 514px" tabindex="-1">                </textarea>
            &nbsp;&nbsp;&nbsp;
            <asp:HiddenField ID="hfRank" runat="server" />
        </div>
        &nbsp;&nbsp;
    </div>
    </form>
</body>
</html>
