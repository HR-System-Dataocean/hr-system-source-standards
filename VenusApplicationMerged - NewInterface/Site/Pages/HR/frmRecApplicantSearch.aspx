<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRecApplicantSearch.aspx.vb"
    Inherits="frmRecApplicantSearch" Culture="auto" meta:resourcekey="PageResource1"
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
    <title>* Venus Payroll * ~Rec Applicant Search</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
     <script type="text/javascript" id="Infragistics">
<!--

         function btnVacationTransaction_Click(oButton, oEvent) {
             var grid = igtbl_getGridById("UltraWebTab1xxctl0xuwgInterviewsDetail0")
             var Aray = "0"
             for (i = 0; i < grid.Rows.length; i++) {
                 row1 = grid.Rows.getRow(i);
                 currCodeCell = row1.getCell(4);
                 if (currCodeCell.getValue() == true) {
                     Aray = Aray + "|" + row1.getCell(0).getValue()
                 }
             }
             window.opener.document.forms[0]["txtReturned"].value = Aray;
             window.opener.focus();
             //            window.opener.document.forms[0]["txtReturned"].focus();
             window.opener.document.forms[0].submit();
             window.close();
         }
// -->
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;" >
    <form id="frmRecApplicantSearch" runat="server">
    <div style="display: none">

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
                                <table style="width: 100%;  vertical-align: top"
                                        cellspacing="0">
                                            <tr>
                                                <td style="height: 18px">
                                                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                        <tr>
                                                            <td style="width: 5px">
                                                            </td>
                                                            <td style="width: 40px; text-align: center;">
                                                                <igtxt:WebImageButton ID="btnVacationTransaction" runat="server" AutoSubmit="False" 
                                                                    Height="18px" meta:resourcekey="btnSaveResource1" Overflow="NoWordWrap" 
                                                                    Style="cursor: pointer;" UseBrowserDefaults="False" Width="24px">
                                                                    <Alignments TextImage="ImageBottom" />
                                                                    <Appearance>
                                                                        <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/SaveN.png" />
                                                                    </Appearance>
                                                                    <ClientSideEvents Click="btnVacationTransaction_Click" />
                                                                </igtxt:WebImageButton>
                                                            </td>
                                                            <td style="width: 5px">
                                                                <asp:Label ID="Label_TSP3" runat="server" meta:resourcekey="Label_TSP1Resource1"
                                                                    Text="|"></asp:Label>
                                                            </td>
                                                            <td style="width: 40px; text-align: center;">
                                                                <asp:ImageButton ID="ImageButton_Print" runat="server" CommandArgument="Print" 
                                                                    Height="16px" meta:resourcekey="ImageButton_PrintResource1" 
                                                                    SkinID="HrPrint_Command" Width="16px" />
                                                            </td>
                                                            <td style="width: 5px">
                                                                &nbsp;</td>
                                                            <td style="width: 40px; text-align: center;">
                                                                &nbsp;</td>
                                                  
                                                            
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            
                                            </table>
                                    <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                        cellspacing="0">
                                        <tr>
                                            <td style="height: 10px" colspan="3">
                                            </td>
                                        </tr>
                                <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label2" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Nationality" Width="90px" meta:resourcekey="Label2Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlNationality" runat="server" 
                                                                SkinID="DropDownList_LargNormal" 
                                                                meta:resourcekey="ddlNationalityResource1">
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="Label6" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Gender" Width="90px" meta:resourcekey="Label6Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlGender" runat="server" 
                                                                SkinID="DropDownList_LargNormal" meta:resourcekey="ddlGenderResource1">
                                                                <asp:ListItem Value="M" Text="Male" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                                <asp:ListItem Value="F" Text="Female" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="Label3" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Salary From" Width="90px" meta:resourcekey="Label3Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtLastSalaryFrom" runat="server" DataMode="Decimal" MaxLength="10" 
                                                                MaxValue="9999999" MinDecimalPlaces="One" MinValue="0" Nullable="False" 
                                                                NullText="0" SkinID="WebNumericEdit_Default" 
                                                                meta:resourcekey="txtLastSalaryFromResource1" >
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
                                                            <asp:Label ID="Label7" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Salary To" Width="90px" meta:resourcekey="Label7Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtLastSalaryTo" runat="server" DataMode="Decimal" 
                                                                MaxLength="10" MaxValue="9999999" MinDecimalPlaces="One" MinValue="0" 
                                                                Nullable="False" NullText="0" SkinID="WebNumericEdit_Default" 
                                                                meta:resourcekey="txtLastSalaryToResource1">
                                                            </igtxt:WebNumericEdit>
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
                                                            <asp:Label ID="Label4" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Top Degree" Width="90px" meta:resourcekey="Label4Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlEDegree" runat="server" 
                                                                SkinID="DropDownList_LargNormal" meta:resourcekey="ddlEDegreeResource1">
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="Label8" runat="server" SkinID="Label_DefaultNormal" Width="90px" 
                                                                Text="Experiences Years" Height="16px" meta:resourcekey="Label8Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtYOExp" runat="server" DataMode="Int" 
                                                                MaxLength="10" MaxValue="9999" MinValue="0" Nullable="False" NullText="0" 
                                                                SkinID="WebNumericEdit_Default" meta:resourcekey="txtYOExpResource1">
                                                            </igtxt:WebNumericEdit>
                                                        </td>
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
                                                            <asp:Label ID="Label_Title1" runat="server" Text="Please Select Applicant"
                                                                SkinID="Label_DefaultBold" meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: top;">
                                            </td>
                                            <td style="width: 47%; vertical-align: middle ;">
                                                
                                                <igtxt:WebImageButton ID="btnGetVacationDefault" runat="server" Height="5px" 
                                                     Overflow="NoWordWrap" 
                                                    style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;color:Black" 
                                                    Text=" Search " UseBrowserDefaults="False" Width="80px" 
                                                    meta:resourcekey="btnGetVacationDefaultResource1">
                                                    <Alignments TextImage="TextRightImageLeft" VerticalImage="Middle" />
                                                    <Appearance>
                                                        <Image Url="./img/forum_search.gif" />
                                                        <InnerBorder ColorBottom="160, 160, 160" ColorLeft="White" 
                                                            ColorRight="160, 160, 160" ColorTop="White" StyleBottom="Solid" 
                                                            StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid" WidthBottom="1px" 
                                                            WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                                    </Appearance>
                                                </igtxt:WebImageButton>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 16px; vertical-align: top" colspan="3">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgInterviewsDetail0" runat="server" EnableAppStyling="False"
                                                                Height="280px" Width="99%" SkinID="Default" 
                                                                meta:resourcekey="uwgInterviewsDetail0Resource1" >
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                                                    HeaderClickActionDefault="SortSingle" Name="uwgForNationality" RowHeightDefault="18px"
                                                                    RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                                    AutoGenerateColumns="False" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                    Version="4.00" ViewType="OutlookGroupBy">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="280px"
                                                                        Width="99%">
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
                                        <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="140px" AllowResize="Fixed"
                                            DataType="System.Int32" meta:resourcekey="UltraGridColumnResource1">
                                            <Header Caption="">
                                            </Header>
                                            <CellButtonStyle HorizontalAlign="Left">
                                            </CellButtonStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <SelectedCellStyle HorizontalAlign="Center">
                                            </SelectedCellStyle>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Applicant_Name" Key="Applicant_Name" 
                                            AllowResize="Fixed" AllowUpdate="No"
                                            Width="100%" meta:resourcekey="UltraGridColumnResource2">
                                            <Header Caption="Applicant Name">
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Header>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                            <SelectedCellStyle HorizontalAlign="Center">
                                            </SelectedCellStyle>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Experiounces"
                                            Key="Experiounces" Width="100px" AllowResize="Fixed" AllowUpdate="No" 
                                            meta:resourcekey="UltraGridColumnResource3">
                                            <Header Caption="Experionces">
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Header>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                            <SelectedCellStyle HorizontalAlign="Right">
                                            </SelectedCellStyle>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EXSalary"
                                            Key="EXSalary" Width="100px" AllowResize="Fixed" AllowUpdate="No" 
                                            meta:resourcekey="UltraGridColumnResource4">
                                            <Header Caption="Exp Salary">
                                                <RowLayoutColumnInfo OriginX="3" />
                                            </Header>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                            <SelectedCellStyle HorizontalAlign="Right">
                                            </SelectedCellStyle>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="3" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Apply"
                                            Key="Apply" Width="100px" AllowResize="Fixed" AllowUpdate="Yes" 
                                            DataType="System.Boolean" Type="CheckBox" 
                                            meta:resourcekey="UltraGridColumnResource5">
                                            <Header Caption="Is Apply">
                                                <RowLayoutColumnInfo OriginX="4" />
                                            </Header>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                            <SelectedCellStyle HorizontalAlign="Center">
                                            </SelectedCellStyle>
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
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
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
    </div>
    </form>
</body>
</html>
