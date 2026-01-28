<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmOtherFieldsSettings.aspx.vb"
    Inherits="frmOtherFieldsSettings" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ Other Fields Settingss</title>
    <script language="javascript" type="text/javascript" src="Scripts/App_JScript_OtherFieldsSettings.js"></script>
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
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmOtherFieldsSettings" runat="server">
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
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
            <table align="center" style="width: 100%;">
                <tr>
                    <td style="width: 100%; height: 60px; vertical-align: top">
                        <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                            <tr>
                                <td style="display:none">
                                    <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N" />
                                </td>
                                <td style="width: 24px">
                                </td>
                                <td style="width: 120px">
                                </td>
                                <td style="width: 24px">
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Refresh" Width="14px" Height="12px" runat="server"
                                        CommandArgument="Refresh" ImageUrl="~/Pages/HR/Img/cal_recur.gif" meta:resourcekey="ImageButton_RefreshResource1" />
                                </td>
                                <td style="width: 40px">
                                </td>
                                <td style="width: 24px">
                                </td>
                                <td style="width: 80px">
                                </td>
                                <td style="width: 80px">
                                </td>
                                <td style="width: 40px">
                                </td>
                                <td style="width: 24px">
                                </td>
                                <td style="width: 24px">
                                </td>
                                <td style="width: 10px">
                                </td>
                                <td style="width: 24px">
                                </td>
                                <td style="width: 24px">
                                </td>
                                <td style="width: 20%">
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
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                            &nbsp;
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
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                            &nbsp;
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
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                            &nbsp;
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
                            meta:resourcekey="UltraWebTab1Resource1">
                            <Tabs>
                                <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                    <ContentTemplate>
                                        <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                            cellspacing="0">
                                            <tr>
                                                <td style="height: 10px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;">
                                                    <asp:Label ID="Label_Title1" runat="server" SkinID="Label_DefaultBold" Text="برجاء تحديد خصائص الحقول"
                                                        meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                </td>
                                                <td style="width: 40%;">
                                                    <igtxt:WebImageButton ID="btnAddField" runat="server" Height="5px" meta:resourcekey="WebImageButton1Resource1"
                                                        Overflow="NoWordWrap" Text="اضافة حقل" UseBrowserDefaults="False" Width="150px"
                                                        AutoSubmit="False">
                                                        <ClientSideEvents Click="btnAddField()" />
                                                        <Alignments TextImage="TextRightImageLeft" VerticalImage="Middle" />
                                                        <Appearance>
                                                            <Image Url="./img/forum_newmsg.gif" />
                                                            <InnerBorder ColorBottom="160, 160, 160" ColorLeft="White" ColorRight="160, 160, 160"
                                                                ColorTop="White" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid"
                                                                WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                                        </Appearance>
                                                    </igtxt:WebImageButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">
                                                    <igtbl:UltraWebGrid   Browser="UpLevel"   ID="uwgOtherFields" runat="server" EnableAppStyling="False" Height="100%"
                                                        SkinID="Default" Width="100%" meta:resourcekey="uwgOtherFieldsResource1">
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                            BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgOnlyForProfession"
                                                            RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                            StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                            Version="4.00" ViewType="OutlookGroupBy">
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
                                                            <ClientSideEvents CellClickHandler="uwgOtherFields_CellClickHandler" />
                                                        </DisplayLayout>
                                                        <Bands>
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                                <FilterOptions NonEmptyString="" AllString="" EmptyString="">
                                                                    <FilterHighlightRowStyle ForeColor="White" BackColor="#151C55">
                                                                    </FilterHighlightRowStyle>
                                                                    <FilterDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px"
                                                                        Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White" Width="200px"
                                                                        CustomRules="overflow:auto;">
                                                                        <Padding Left="2px"></Padding>
                                                                    </FilterDropDownStyle>
                                                                </FilterOptions>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" Width="0px" meta:resourcekey="UltraGridColumnResource1">
                                                                        <CellButtonStyle Height="12px">
                                                                        </CellButtonStyle>
                                                                        <Header Caption="Field Name">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowResize="Fixed" AllowUpdate="No" BaseColumnName="VwEngName"
                                                                        Key="VwEngName" Width="15%" Type="DropDownList" meta:resourcekey="UltraGridColumnResource2">
                                                                        <Header Caption="V. Eng. Name">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                        <ValueList DisplayStyle="DataValue">
                                                                            <ValueListItems>
                                                                                <igtbl:ValueListItem DataValue="English" DisplayText="English" Key="&quot;0&quot;" />
                                                                                <igtbl:ValueListItem DataValue="Arabic" DisplayText="Arabic" Key="&quot;1&quot;" />
                                                                                <igtbl:ValueListItem DataValue="Both" DisplayText="Both" Key="&quot;2&quot;" />
                                                                            </ValueListItems>
                                                                        </ValueList>
                                                                        <CellStyle Cursor="Hand">
                                                                        </CellStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowResize="Fixed" AllowUpdate="No" BaseColumnName="VwArbName"
                                                                        Key="VwArbName" Width="15%" FooterText="View ArbName" meta:resourcekey="UltraGridColumnResource3">
                                                                        <Header Caption="V. Arb. Name">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer Caption="View ArbName">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                        <CellStyle Cursor="Hand">
                                                                        </CellStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowResize="Fixed" AllowUpdate="No" BaseColumnName="ViewObjectName"
                                                                        Key="ViewObjectName" Width="15%" meta:resourcekey="UltraGridColumnResource4">
                                                                        <Header Caption="V. Obj. Name">
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Footer>
                                                                        <CellStyle Cursor="Hand">
                                                                        </CellStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowResize="Fixed" AllowUpdate="No" BaseColumnName="DataLength"
                                                                        Key="DataLength" Width="10%" DataType="System.Int32" meta:resourcekey="UltraGridColumnResource5">
                                                                        <Header Caption="Data Len.">
                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                        </Footer>
                                                                        <CellStyle Cursor="Hand">
                                                                        </CellStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowResize="Fixed" AllowUpdate="No" BaseColumnName="DataTypeName"
                                                                        Key="DataTypeName" Width="10%" meta:resourcekey="UltraGridColumnResource6">
                                                                        <Header Caption="Data Type">
                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                        </Footer>
                                                                        <CellStyle Cursor="Hand">
                                                                        </CellStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowResize="Fixed" AllowUpdate="No" BaseColumnName="Rank"
                                                                        Key="Rank" Width="5%" meta:resourcekey="UltraGridColumnResource7">
                                                                        <Header Caption="Rank">
                                                                            <RowLayoutColumnInfo OriginX="6" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="6" />
                                                                        </Footer>
                                                                        <CellStyle Cursor="Hand">
                                                                        </CellStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowResize="Fixed" AllowUpdate="No" BaseColumnName="FieldTypeName"
                                                                        Key="FieldTypeName" Width="10%" meta:resourcekey="UltraGridColumnResource8">
                                                                        <Header Caption="Field Type">
                                                                            <RowLayoutColumnInfo OriginX="7" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="7" />
                                                                        </Footer>
                                                                        <CellStyle Cursor="Hand">
                                                                        </CellStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowResize="Fixed" AllowUpdate="No" BaseColumnName="OtherFieldsGroupName"
                                                                        Key="OtherFieldsGroupName" Width="10%" meta:resourcekey="UltraGridColumnResource9">
                                                                        <Header Caption="Group name">
                                                                            <RowLayoutColumnInfo OriginX="8" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="8" />
                                                                        </Footer>
                                                                        <CellStyle Cursor="Hand">
                                                                        </CellStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="EngName" Key="EngName" Width="10%" AllowResize="Fixed"
                                                                        AllowUpdate="No" meta:resourcekey="UltraGridColumnResource10">
                                                                        <CellStyle Cursor="Hand">
                                                                        </CellStyle>
                                                                        <Header Caption="Field Name">
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
                                                <td style="width: 40%;">
                                                    <igtbl:UltraWebGrid   Browser="UpLevel"   ID="uwgNavigation" runat="server" EnableAppStyling="False" Height="280px"
                                                        SkinID="Default" Width="98%" meta:resourcekey="uwgNavigationResource1">
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                            BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgOnlyForProfession"
                                                            RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                            StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                            Version="4.00" ViewType="OutlookGroupBy">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="280px"
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
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource2">
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn AllowResize="Fixed" AllowUpdate="No" BaseColumnName="Image"
                                                                        CellButtonDisplay="Always" Key="Image" Type="Button" Width="10%" meta:resourcekey="UltraGridColumnResource11">
                                                                        <Header Caption="">
                                                                        </Header>
                                                                        <CellStyle HorizontalAlign="Center">
                                                                        </CellStyle>
                                                                        <CellButtonStyle BackColor="LightSteelBlue" BackgroundImage="~/Pages/HR/Img/Table.bmp"
                                                                            BorderStyle="None" Height="11px" Width="12px">
                                                                        </CellButtonStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="Code" Key="TableName" Width="80%" meta:resourcekey="UltraGridColumnResource12">
                                                                        <Header Caption="Table Name">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                        <CellStyle HorizontalAlign="Left">
                                                                        </CellStyle>
                                                                        <CellButtonStyle HorizontalAlign="Center">
                                                                        </CellButtonStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="0px" meta:resourcekey="UltraGridColumnResource13">
                                                                        <Header>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ObjectID" Key="ObjectID" Width="0px" meta:resourcekey="UltraGridColumnResource14">
                                                                        <Header>
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
                                            <tr>
                                                <td style="height: 100%" colspan="2">
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
        </igmisc:WebAsyncRefreshPanel>
    </div>
    </form>
</body>
</html>
