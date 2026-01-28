<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSystemUserSearch.aspx.vb"
    Inherits="frmSystemUserSearch" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~System Users Srearch </title>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
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
        var ODialoge;
        var OSender;
        function OpenModal1(pageurl, height, width, CheckID, SenderCtrl) {
            if (CheckID == false) {
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
        }
        function CloseIt() {
            var $dialog = ODialoge;
            $dialog.dialog('close');
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmSystemUserSearch" runat="server">
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display: none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N" />
                            </td>
                            <td style="width: 24px">
                            </td>
                            <td style="width: 120px">
                                <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                    meta:resourcekey="ImageButton_NewResource1" CommandArgument="New" />
                            </td>
                            <td style="width: 24px">
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Next" Width="16px" Height="16px" runat="server"
                                    SkinID="HrNext_Command" meta:resourcekey="ImageButton_NextResource1" CommandArgument="Next" />
                            </td>
                            <td style="width: 40px">
                                <asp:ImageButton ID="ImageButton_Back" Width="16px" Height="16px" runat="server"
                                    SkinID="HrBack_Command" meta:resourcekey="ImageButton_BackResource1" CommandArgument="Previous" />
                            </td>
                            <td style="width: 24px">
                                <asp:Label ID="Label_TSP2" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                            </td>
                            <td style="width: 80px">
                                <asp:Label ID="lblP" runat="server" Text="الصفحة" SkinID="Label_CopyRightsBold" meta:resourcekey="lblPResource1"></asp:Label>
                            </td>
                            <td style="width: 80px">
                                <asp:Label ID="lblCount" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblCountResource1"></asp:Label>
                            </td>
                            <td style="width: 40px">
                                <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Refresh" Width="14px" Height="12px" runat="server"
                                    CommandArgument="Refresh" ImageUrl="~/Pages/HR/Img/cal_recur.gif" meta:resourcekey="ImageButton_RefreshResource1" />
                            </td>
                            <td style="width: 24px">
                            </td>
                            <td style="width: 10px">
                            </td>
                            <td style="width: 24px">
                            </td>
                            <td style="width: 24px">
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
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; vertical-align: auto;">
                                                <igtbl:UltraWebGrid   Browser="UpLevel"  ID="UwgSearchUsers" runat="server" Height="100%"
                                                    meta:resourcekey="uwgEndOfServiceRulesResource1" SkinID="Default" 
                                                    Width="90%">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                    BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                                    RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                    Version="4.00" ViewType="OutlookGroupBy">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                        Width="90%">
                                                                    </FrameStyle>
                                                                    <ClientSideEvents AfterCellUpdateHandler="uwg_AfterCellUpdateHandler"  />
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
                                                                     <FilterOptionsDefault AllowRowFiltering="OnClient" ApplyOnAdd="True" 
                                                            FilterIcon="True" FilterRowView="Top" FilterUIType="FilterRow">
                                                        </FilterOptionsDefault>

                                                                </DisplayLayout>
                                                    <Bands>
                                                        <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="Code" Key="Code" Width="10%" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource1">
                                                                    <Header Caption="User Code">
                                                                    </Header>
                                                                    <CellStyle ForeColor="Black">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="EngName" Key="EngName" Width="15%" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource2">
                                                                    <Header Caption="English Name">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                    <CellStyle ForeColor="Black">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ArbName" Key="ArbName" Width="15%" AllowUpdate="No"
                                                                    meta:resourcekey="UltraGridColumnResource3">
                                                                    <Header Caption="Arabic Name">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                    <CellStyle ForeColor="Black">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowRowFiltering="False" BaseColumnName="Forms Permission"
                                                                    CellButtonDisplay="Always" Key="Forms Permission" Type="Button" Width="7.5%"
                                                                    meta:resourcekey="UltraGridColumnResource4">
                                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                                    <Header Caption="Forms Permission">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/Form.bmp" BorderStyle="None" Cursor="Hand"
                                                                        Height="14px" HorizontalAlign="Center" Width="14px">
                                                                    </CellButtonStyle>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowRowFiltering="False" CellButtonDisplay="Always" Key="ControlPermission"
                                                                    Type="Button" Width="7.5%" meta:resourcekey="UltraGridColumnResource5">
                                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                                    <Header Caption="Control Permission">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/Controls.bmp" BorderStyle="None"
                                                                        Cursor="Hand" Height="13px" HorizontalAlign="Center" Width="15px">
                                                                    </CellButtonStyle>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowRowFiltering="False" CellButtonDisplay="Always" Key="RecordPermission"
                                                                    Type="Button" Width="7.5%" meta:resourcekey="UltraGridColumnResource6">
                                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                                    <Header Caption="Records Permission">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/Table.bmp" BorderStyle="None" Cursor="Hand"
                                                                        Height="11px" HorizontalAlign="Center" Width="12px">
                                                                    </CellButtonStyle>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowRowFiltering="False" CellButtonDisplay="Always" Key="Groups"
                                                                    Type="Button" Width="7.5%" meta:resourcekey="UltraGridColumnResource7">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <Header Caption="Groups">
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Footer>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/i.p.putingrp.gif" BorderStyle="None"
                                                                        Cursor="Hand" Height="12px" HorizontalAlign="Center" Width="16px">
                                                                    </CellButtonStyle>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowRowFiltering="False" CellButtonDisplay="Always" Key="Report Permission"
                                                                    Type="Button" Width="7.5%" meta:resourcekey="UltraGridColumnResource8">
                                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                                    <Header Caption="Report Permission">
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Footer>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/Form.bmp" BorderStyle="None" Cursor="Hand"
                                                                        Height="14px" HorizontalAlign="Center" Width="14px">
                                                                    </CellButtonStyle>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowRowFiltering="False" CellButtonDisplay="Always" Key="ModulePermission"
                                                                    Type="Button" Width="7.5%" meta:resourcekey="UltraGridColumnResource9">
                                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                                    <Header Caption="Module Permission">
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Footer>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/Form.bmp" BorderStyle="None" Cursor="Hand"
                                                                        Height="14px" HorizontalAlign="Center" Width="14px">
                                                                    </CellButtonStyle>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowRowFiltering="False" CellButtonDisplay="Always" Key="Update"
                                                                    Hidden="true" Type="Button" Width="7.5%" meta:resourcekey="UltraGridColumnResource10">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <Header Caption="Update">
                                                                        <RowLayoutColumnInfo OriginX="9" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="9" />
                                                                    </Footer>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/i.p.edit.gif" BorderStyle="None"
                                                                        Cursor="Hand" Height="13px" HorizontalAlign="Center" Width="15px">
                                                                    </CellButtonStyle>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowRowFiltering="False" CellButtonDisplay="Always" Key="Delete"
                                                                    Hidden="true" Type="Button" Width="7.5%" meta:resourcekey="UltraGridColumnResource11">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <Header Caption="Delete">
                                                                        <RowLayoutColumnInfo OriginX="10" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="10" />
                                                                    </Footer>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/logoff_small.gif" BorderStyle="None"
                                                                        Cursor="Hand" Height="14px" Width="14px">
                                                                    </CellButtonStyle>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="0px">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="11" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="11" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowRowFiltering="False" CellButtonDisplay="Always" Key="WidgetsPermission"
                                                                    Type="Button" Width="7.5%" meta:resourcekey="UltraGridColumnResource12">
                                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                                    <Header Caption="Module Permission">
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Footer>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/Form.bmp" BorderStyle="None" Cursor="Hand"
                                                                        Height="14px" HorizontalAlign="Center" Width="14px">
                                                                    </CellButtonStyle>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
                                            </td>
                                            <tr>
                                                <td style="height: 100%">
                                                </td>
                                            </tr>
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
