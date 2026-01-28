<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGroupsSelector.aspx.vb"
    Inherits="frmGroupsSelector" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Groups Selector</title>
    <script language="javascript" src="Scripts/App_JScript.js"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js"></script>
    <script language="javascript" src="Scripts/App_frmGroupsSelector.js" type="text/javascript"></script>
</head>
<body style="margin: 0; padding: 0;">
    <form id="frmGroupsSelector" runat="server">
    <div runat="server" id="DIV" style="background-color: #BEDCFF;">
        <table align="center" style="width: 100%;">
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
                                            <td style="height: 10px" colspan="3">
                                                <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                    <tr>
                                                        <td style="width: 24px">
                                                        </td>
                                                        <td style="width: 80px">
                                                            <asp:ImageButton ID="ImageButton_Save" runat="server" CommandArgument="Save" Height="16px"
                                                                meta:resourcekey="ImageButton_SaveResource1" SkinID="HrSave_Command" Width="16px" />
                                                        </td>
                                                        <td style="width: 10px">
                                                            <asp:Label ID="Label_TSP1" runat="server" meta:resourcekey="Label_TSP1Resource1"
                                                                Text="|"></asp:Label>
                                                        </td>
                                                        <td style="width: 24px">
                                                            <asp:ImageButton ID="ImageButton_Delete" runat="server" CommandArgument="Delete"
                                                                Height="16px" meta:resourcekey="ImageButton_DeleteResource1" SkinID="HrDelete_Command"
                                                                Width="16px" />
                                                        </td>
                                                        <td style="width: 40px">
                                                        </td>
                                                        <td style="width: 60px">
                                                        </td>
                                                        <td style="width: 60px">
                                                        </td>
                                                        <td style="width: 60px">
                                                        </td>
                                                        <td style="width: 100px">
                                                        </td>
                                                        <td style="width: 200px">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px" colspan="3">
                                                <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                    <tr>
                                                        <td class="SeparArea">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 70px">
                                                            <asp:Label ID="lblUser" runat="server" meta:resourcekey="lblUserResource1" SkinID="Label_CopyRightsBold"
                                                                Text="الكود"></asp:Label>
                                                        </td>
                                                        <td style="width: 60px">
                                                            <asp:Label ID="lblUserCode" runat="server" meta:resourcekey="lblRegDateValueResource1"
                                                                SkinID="Label_CopyRightsNormal"></asp:Label>
                                                        </td>
                                                        <td style="width: 70px">
                                                            <asp:Label ID="lblName" runat="server" meta:resourcekey="lblNameResource1" SkinID="Label_CopyRightsBold"
                                                                Text="الاسم"></asp:Label>
                                                        </td>
                                                        <td style="width: 120px">
                                                            <asp:Label ID="lblUserName" runat="server" meta:resourcekey="lblRegUserValueResource1"
                                                                SkinID="Label_CopyRightsNormal"></asp:Label>
                                                        </td>
                                                        <td style="width: 200Px">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 3%;">
                                                        </td>
                                                        <td style="width: 30%;">
                                                            <asp:Label ID="lblCode" runat="server" Text="الكود" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 40%;">
                                                            <asp:TextBox ID="txtCode" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                AutoPostBack="True" meta:resourcekey="txtCodeResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 27%;">
                                                            <igtxt:WebImageButton ID="btnSearchCode" runat="server" Height="18px" meta:resourcekey="btnSearchCodeResource1"
                                                                UseBrowserDefaults="False" Width="32px">
                                                                <Alignments TextImage="ImageBottom" />
                                                                <Appearance>
                                                                    <Image Url="./Img/forum_search.gif" />
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
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
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 3%;">
                                                        </td>
                                                        <td style="width: 30%;">
                                                            <asp:Label ID="lblEngName" runat="server" Text="التوصيف إنجليزى" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 67%;">
                                                            <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalltr" MaxLength="255"
                                                                meta:resourcekey="txtEngNameResource1"></asp:TextBox>
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
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 3%;">
                                                        </td>
                                                        <td style="width: 30%;">
                                                            <asp:Label ID="lblArbName" runat="server" Text="التوصيف عربى" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblArbNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 67%;">
                                                            <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                                                                meta:resourcekey="txtArbNameResource1"></asp:TextBox>
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
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; vertical-align: top;">
                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                    cellspacing="6">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label_Title1" runat="server" Text="برجاء تحديد مجموعات لهذا المستخدم"
                                                                SkinID="Label_DefaultBold" meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: top;">
                                            </td>
                                            <td style="width: 47%; vertical-align: top;">
                                                <table style="width: 100%; height: 30px;" cellspacing="6">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkCheckAll" runat="server" meta:resourcekey="chkCheckAllResource1"
                                                                Text=" " />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; vertical-align: auto" colspan="3">
                                                <igtbl:UltraWebGrid   Browser="UpLevel"   ID="UwgSearchUsers" runat="server" EnableAppStyling="False" Height="100%"
                                                    Width="100%" SkinID="Default" meta:resourcekey="uwgEndOfServiceRulesResource1">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowAddNewDefault="Yes" AllowColumnMovingDefault="OnServer"
                                                        AllowDeleteDefault="Yes" AllowSortingDefault="OnClient" AllowUpdateDefault="Yes"
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgEndOfServiceRules"
                                                        RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                        StationaryMargins="Header" AutoGenerateColumns="False" StationaryMarginsOutlookGroupBy="True"
                                                        TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                            Width="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents ClickCellButtonHandler="UwgSearchUsers_ClickCellButtonHandler"
                                                            AfterCellUpdateHandler="UwgSearchUsers_AfterCellUpdateHandler" />
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
                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Code" Width="123px" meta:resourcekey="UltraGridColumnResource1">
                                                                    <Header Caption="Code">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="EngName" Width="250px" meta:resourcekey="UltraGridColumnResource2">
                                                                    <Header Caption="English Description">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="ArbName" Width="250px" meta:resourcekey="UltraGridColumnResource3">
                                                                    <Header Caption="Arabic Description">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Selected" CellButtonDisplay="Always"
                                                                    DataType="System.Boolean" Key="Selected" Type="CheckBox" meta:resourcekey="UltraGridColumnResource4">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Selected">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="CancelDate" DataType="System.DateTime" Hidden="True"
                                                                    Width="100px" meta:resourcekey="UltraGridColumnResource5">
                                                                    <Header Caption="Cancel Date">
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
                                            <td style="height: 100%" colspan="3">
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
        <asp:HiddenField ID="txtCanViewC" runat="server" />
        <asp:HiddenField ID="txtGridCount" runat="server" />
    </div>
    </form>
</body>
</html>
