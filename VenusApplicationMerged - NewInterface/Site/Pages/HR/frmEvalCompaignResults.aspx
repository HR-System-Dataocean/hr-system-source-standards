<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEvalCompaignResults.aspx.vb"
    Inherits="frmEvalCompaignResults" Culture="auto" meta:resourcekey="PageResource1"
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
    <title>* Venus Payroll * ~Eval Compaign Results</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmEvalCompaignResults" runat="server">
        <div style="display: none">
            <asp:TextBox ID="txtCode" runat="server" AutoPostBack="True"
                meta:resourcekey="txtCodeResource1"></asp:TextBox>
        </div>
        <div class="Div_MasterContainer" runat="server" id="DIV">
            <table align="center" style="width: 100%;">
                <tr>
                    <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                        <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                            <tr>
                                <td style="display:none">
                                    <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server"
                                        CommandArgument="N" meta:resourcekey="ImageButton1Resource1" />
                                </td>

                                <td style="width: 120px">&nbsp;</td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                        SkinID="HrPrint_Command" CommandArgument="Print" meta:resourcekey="ImageButton_PrintResource1" />
                                </td>
                                <td></td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                        SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                    <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
                                </td>
                                <td style="width: 5%"></td>
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
                                                        <td style="width: 40%; height: 16px; vertical-align: middle;">&nbsp;</td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 2%; vertical-align: top"></td>
                                            <td style="width: 49%; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 40%; height: 16px; vertical-align: middle;">&nbsp;</td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 49%; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 40%; height: 16px; vertical-align: middle;">&nbsp;</td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 2%; vertical-align: top"></td>
                                            <td style="width: 49%; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 40%; height: 16px; vertical-align: middle;"></td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;"></td>
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
                                                <td style="height: 10px" colspan="3"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 5px;"></td>
                                                            <td style="width: 90px;">

                                                                <asp:Label ID="Label1" runat="server" Text="Select Compaign"
                                                                    SkinID="Label_DefaultNormal" meta:resourcekey="Label1Resource1"></asp:Label>

                                                            </td>
                                                            <td>

                                                                <asp:DropDownList ID="ddlCompaign" runat="server" AutoPostBack="true"
                                                                    meta:resourcekey="ddl_MainDepartmentResource1" SkinID="DropDownList_LargNormal">
                                                                </asp:DropDownList>

                                                            </td>
                                                            <td style="width: 24px;">

                                                                <igtxt:WebImageButton ID="btnSearchCode" runat="server" AutoSubmit="False"
                                                                    Height="18px" meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap"
                                                                    UseBrowserDefaults="False" Width="24px">
                                                                    <Alignments TextImage="ImageBottom" />
                                                                    <Appearance>
                                                                        <Image Url="./Img/forum_search.gif" />
                                                                    </Appearance>
                                                                </igtxt:WebImageButton>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">&nbsp;</td>
                                                <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                        cellspacing="6">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_Title1Resource1"
                                                                    SkinID="Label_DefaultBold" Text="Evaluation Employee Compaign Powers"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">&nbsp;</td>
                                                <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 16px; vertical-align: top" colspan="3">

                                                    <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgInterviewsDetail0" runat="server"
                                                        EnableAppStyling="False" Height="280px"
                                                        SkinID="Default" Width="100%"
                                                        meta:resourcekey="uwgInterviewsDetail0Resource1">
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer"
                                                            AllowDeleteDefault="Yes" AllowSortingDefault="OnClient"
                                                            AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                            BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle"
                                                            Name="uwgOnlyForProfession" RowHeightDefault="18px" RowSelectorsDefault="No"
                                                            SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00"
                                                            ViewType="OutlookGroupBy">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption"
                                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                                                Font-Size="8.25pt" Height="280px" Width="100%">
                                                            </FrameStyle>
                                                            <Pager MinimumPagesForDisplay="2">
                                                                <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                        WidthTop="1px" />
                                                                </PagerStyle>
                                                            </Pager>
                                                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                            </EditCellStyleDefault>
                                                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                    WidthTop="1px" />
                                                            </FooterStyleDefault>
                                                            <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma"
                                                                Font-Size="9pt" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                    WidthTop="1px" />
                                                            </HeaderStyleDefault>
                                                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                                <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                    BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px"
                                                                        WidthTop="1px" />
                                                                </BoxStyle>
                                                            </AddNewBox>
                                                            <ActivationObject BorderColor="" BorderWidth="">
                                                            </ActivationObject>
                                                            <FilterOptionsDefault>
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                    BorderWidth="1px" CustomRules="overflow:auto;"
                                                                    Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px"
                                                                    Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver"
                                                                    BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;"
                                                                    Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
                                                                    <Padding Left="2px" />
                                                                </FilterOperandDropDownStyle>
                                                            </FilterOptionsDefault>
                                                            <ClientSideEvents AfterCellUpdateHandler="uwg_AfterCellUpdateHandler" />
                                                        </DisplayLayout>
                                                        <Bands>
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="Evaluated_ID"
                                                                        DataType="System.Int32" Hidden="True" Key="Evaluated_ID"
                                                                        meta:resourcekey="UltraGridColumnResource1">
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="EmpName"
                                                                        Key="EmpName" Width="70%" meta:resourcekey="UltraGridColumnResource2">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <CellStyle HorizontalAlign="Center">
                                                                        </CellStyle>
                                                                        <SelectedCellStyle HorizontalAlign="Center">
                                                                        </SelectedCellStyle>
                                                                        <Header Caption="Evaluated Name">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="Grad" DataType="System.Double"
                                                                        Key="Grad" Width="30%" meta:resourcekey="UltraGridColumnResource3">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <CellStyle HorizontalAlign="Center">
                                                                        </CellStyle>
                                                                        <SelectedCellStyle HorizontalAlign="Center">
                                                                        </SelectedCellStyle>
                                                                        <Header Caption="Evaluation Grade">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                </Columns>
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                            </igtbl:UltraGridBand>
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource2">
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="Evaluated_ID"
                                                                        DataType="System.Int32" Hidden="True" Key="Evaluated_ID">
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="Evaluator_ID"
                                                                        DataType="System.Int32" Hidden="True" Key="Evaluator_ID">
                                                                        <Header>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn meta:resourcekey="EmpNameRecourcekey" BaseColumnName="EmpName"
                                                                        Key="EmpName" Width="300px" AllowUpdate="No">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <CellStyle HorizontalAlign="Center">
                                                                        </CellStyle>
                                                                        <SelectedCellStyle HorizontalAlign="Center">
                                                                        </SelectedCellStyle>
                                                                        <Header Caption="Evaluator Name">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn meta:resourcekey="GardeRecourcekey" BaseColumnName="Garde"
                                                                        DataType="System.Double" Key="Garde" Width="150px" AllowUpdate="No">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <CellStyle HorizontalAlign="Center">
                                                                        </CellStyle>
                                                                        <SelectedCellStyle HorizontalAlign="Center">
                                                                        </SelectedCellStyle>
                                                                        <Header Caption="Evaluation Grade">
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
                                                <td style="height: 100%;" colspan="3"></td>
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
