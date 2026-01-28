<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPayReceive.aspx.vb" Inherits="frmPayReceive"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Pay Receive</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_frmPayReceive.js" type="text/javascript"></script>
    <script>


       
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmPayReceive" runat="server">
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit1" runat="server" BackColor="#FFE0C0" BorderColor="White"
                                BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" DisplayModeFormat="dd/MM/yyyy"
                                EditModeFormat="dd/MM/yyyy" Fields="2007-1-13-0-0-0-0" Style="left: 48px; position: absolute;
                                top: 142px" UseBrowserDefaults="False" Width="103px" meta:resourcekey="WebDateTimeEdit1Resource1">
                                <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow" CustomButtonDisabledImageUrl="[ig_edit_01b.gif]"
                                    CustomButtonImageUrl="[ig_edit_0b.gif]">
                                    <ButtonStyle BackColor="#C5D5FC" BorderColor="#ABC1F4" BorderStyle="Solid" BorderWidth="1px"
                                        Width="13px">
                                    </ButtonStyle>
                                    <ButtonDisabledStyle BackColor="#F1F1ED" BorderColor="#E4E4E4">
                                    </ButtonDisabledStyle>
                                    <ButtonHoverStyle BackColor="#DCEDFD">
                                    </ButtonHoverStyle>
                                    <ButtonPressedStyle BackColor="#83A6F4">
                                    </ButtonPressedStyle>
                                </ButtonsAppearance>
                                <SpinButtons DefaultTriangleImages="ArrowSmall" LowerButtonDisabledImageUrl="[ig_edit_21b.gif]"
                                    LowerButtonImageUrl="[ig_edit_2b.gif]" UpperButtonDisabledImageUrl="[ig_edit_11b.gif]"
                                    UpperButtonImageUrl="[ig_edit_1b.gif]" Width="15px" />
                            </igtxt:WebDateTimeEdit>
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
                                    <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                        cellspacing="0">
                                        <tr>
                                            <td style="height: 18px" colspan="3">
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
                                                            <asp:Label ID="Label_TSP3" runat="server" meta:resourcekey="Label_TSP1Resource1"
                                                                Text="|"></asp:Label>
                                                        </td>
                                                        <td style="width: 40px; text-align: center;">
                                                            <igtxt:WebImageButton ID="btnDelete" runat="server" Height="18px" meta:resourcekey="btnDeleteResource1"
                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px">
                                                                <Alignments TextImage="ImageBottom" />
                                                                <Appearance>
                                                                    <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/Delete.png" />
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
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
                                            <td style="height: 18px" colspan="3">
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
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblLoanCode" runat="server" Width="95px" SkinID="Label_DefaultNormal"
                                                                Text="Loan Number" meta:resourcekey="lblLoanCodeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblDescLoanCode" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="lblDescLoanCodeResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lblTransD" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Transaction Date" Width="95px" meta:resourcekey="lblTransDResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblDescTransD" runat="server" 
                                                                SkinID="TextBox_LargeNormalC" meta:resourcekey="lblDescTransDResource1"></asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblTransValue" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Transaction Value" Width="95px" 
                                                                meta:resourcekey="lblTransValueResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="lblTransVal" runat="server" MinDecimalPlaces="Five" 
                                                                MinValue="0" ReadOnly="True" SkinID="WebNumericEdit_Default" 
                                                                meta:resourcekey="lblTransValResource1">
                                                            </igtxt:WebNumericEdit>
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
                                                            <asp:Label ID="lblSettlementAmont" runat="server" 
                                                                meta:resourcekey="lblLastRenewalDateResource1" SkinID="Label_DefaultNormal" 
                                                                Text="Settlement Amount" Width="95px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtSettlementAmont" runat="server" MaxLength="10" 
                                                                 MinValue="0" SkinID="WebNumericEdit_Default" 
                                                                meta:resourcekey="txtSettlementAmontResource1" DataMode="Int">
                                                            </igtxt:WebNumericEdit>

                                                           <%-- <asp:TextBox ID="txtSettlementAmont" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtSettlementAmontResource1"></asp:TextBox>--%>


                                                        </td>
                                                        <td>&nbsp;&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblDocumentNo" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Document Number" Width="95px" 
                                                                meta:resourcekey="lblDocumentNoResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                           <%-- <igtxt:WebNumericEdit ID="WebNumericDocumentDo" runat="server" 
                                                                 SkinID="WebNumericEdit_Default" 
                                                                meta:resourcekey="lblTransValResource1" >
                                                            </igtxt:WebNumericEdit>--%>

                                                            <asp:TextBox ID="WebNumericDocumentDo" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="lblTransValResource1" onkeypress="CheckNumeric(event);"></asp:TextBox>
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
                                                            <asp:Label ID="lblAttache" runat="server" 
                                                                meta:resourcekey="lblAttacheResource1" SkinID="Label_DefaultNormal" 
                                                                Text="Attached file" Width="95px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:FileUpload ID="txtAttachedFile" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="18px" meta:resourcekey="txtAttachedFileResource1" Style="font-family: Tahoma;
                                                                font-size: 8pt; font-weight: Normal; color: black;" Width="100%" />

                                                           <%-- <asp:TextBox ID="txtSettlementAmont" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtSettlementAmontResource1"></asp:TextBox>--%>


                                                        </td>
                                                        <td>&nbsp;&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                    cellspacing="6">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label_Title1" runat="server" Text="Installments of the loan or rewards"
                                                                SkinID="Label_DefaultBold" meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: top;">
                                            </td>
                                            <td style="width: 47%; vertical-align: top;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%; vertical-align: auto" colspan="3">

                                                <table cellspacing="0" style="width: 100%; vertical-align: auto">
                                                    <tr>
                                                        
                                                        <td>
                                                            <igtbl:UltraWebGrid    Browser="UpLevel"  ID="uwgBenetitTemplet" runat="server" EnableAppStyling="False"
                                                                Height="100%" SkinID="Default" Width="100%" 
                                                                meta:resourcekey="uwgBenetitTempletResource1">
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                    BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
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
                                                                    <ClientSideEvents AfterCellUpdateHandler="uwgBenetitTemplet_AfterCellUpdateHandler" />
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
                                                                            <igtbl:UltraGridColumn BaseColumnName="Id" Hidden="True" 
                                                                                Key="ID" meta:resourcekey="UltraGridColumnResource1">
                                                                                <Header Caption="ID">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="dueDate" 
                                                                                Format="dd/MM/yyyy" 
                                                                                Width="100%" EditorControlID="WebDateTimeEdit1" Key="DueDate" 
                                                                                Type="Custom" meta:resourcekey="UltraGridColumnResource2">
                                                                                <Header Caption="Due Date">
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="dueAmount" DataType="System.Double"
                                                                                Format="###,###,###.##" Key="Remain" NullText="0" 
                                                                                Width="80px" meta:resourcekey="UltraGridColumnResource3">
                                                                                <Header Caption="Remain">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Payed" DataType="System.Double"
                                                                                Format="###,###,###.##" Key="Payed" NullText="0" 
                                                                                Width="80px" meta:resourcekey="UltraGridColumnResource4">
                                                                                <Header Caption="Paid">
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="dueAmount"
                                                                                Format="###,###,###.##" 
                                                                                Width="80px" DataType="System.Double" FooterTotal="Sum" NullText="0" 
                                                                                Key="DueAmount" meta:resourcekey="UltraGridColumnResource5">
                                                                                <Header Caption="Amount">
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer Total="Sum">
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn CellButtonDisplay="Always" 
                                                                                Type="CheckBox" Width="80px" meta:resourcekey="UltraGridColumnResource6">
                                                                                <Header Caption="Settle">
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Header>
                                                                                <CellButtonStyle HorizontalAlign="Center">
                                                                                </CellButtonStyle>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                            </igtbl:UltraWebGrid>
                                                        </td>
                                                        <td style="width: 25px;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <igtxt:WebImageButton ID="WebImageButton_Up" runat="server" Height="18px"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" 
                                                                            meta:resourcekey="WebImageButton_UpResource1">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/Loan_Up.png" />
                                                                            </Appearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <igtxt:WebImageButton ID="WebImageButton_Down" runat="server"
                                                                            Height="18px" Overflow="NoWordWrap"  UseBrowserDefaults="False" 
                                                                            Width="24px" meta:resourcekey="WebImageButton_DownResource1">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/Loan_Down.png" />
                                                                            </Appearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%" colspan="3">
                                                &nbsp;&nbsp;</td>
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
