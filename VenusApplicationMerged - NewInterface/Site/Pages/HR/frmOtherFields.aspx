<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmOtherFields.aspx.vb"
    Inherits="frmOtherFields" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Other Fields</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_frmOtherFields.js" type="text/javascript"></script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmOtherFields" runat="server">
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
        <asp:HiddenField ID="txtObjectID" runat="server" />
        <asp:HiddenField ID="txtRowIndex" runat="server" />
        <asp:HiddenField ID="txtFormPermission" runat="server" />
        <asp:HiddenField ID="txtOtherFieldID" runat="server" />
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
                                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                                        <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                            cellspacing="0">
                                            <tr>
                                                <td style="height: 10px" colspan="3">
                                                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                        <tr>
                                                            <td style="width: 19px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 80px">
                                                                <asp:ImageButton ID="ImageButton_New" runat="server" CommandArgument="New" Height="16px"
                                                                    meta:resourcekey="ImageButton_NewResource1" SkinID="HrNew_Command" Width="16px" />
                                                            </td>
                                                            <td style="width: 10px">
                                                                <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_TSP1Resource1" Text="|"></asp:Label>
                                                            </td>
                                                            <td style="width: 80px">
                                                                <asp:ImageButton ID="ImageButton_Save" runat="server" CommandArgument="Save" Height="16px"
                                                                    meta:resourcekey="ImageButton_SaveResource1" SkinID="HrSave_Command" Width="16px" />
                                                            </td>
                                                            <td style="width: 10px">
                                                                <asp:Label ID="Label_TSP1" runat="server" meta:resourcekey="Label_TSP1Resource1"
                                                                    Text="|"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ImageButton_Delete" runat="server" CommandArgument="Delete"
                                                                    Height="16px" meta:resourcekey="ImageButton_DeleteResource1" SkinID="HrDelete_Command"
                                                                    Width="16px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="3">
                                                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                        <tr>
                                                            <td style="width: 8px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 90px">
                                                                <asp:Label ID="lblTableName" runat="server" SkinID="Label_CopyRightsBold" Text="اسم الجدول"></asp:Label>
                                                            </td>
                                                            <td style="width: 270px">
                                                                <asp:Label ID="lblTableNameDesc" runat="server" SkinID="Label_CopyRightsNormal"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblMSG" runat="server" 
                                                                    SkinID="Label_WarningBold"></asp:Label>
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
                                                                <asp:Label ID="lblOtherFieldsGroups" runat="server" Text="إختيار مجموعة" SkinID="Label_DefaultNormal"
                                                                    Width="90px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlOtherFieldsGroups" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
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
                                                                <asp:Label ID="lblViewObject" runat="server" Width="100px" SkinID="Label_DefaultNormal"
                                                                    Text="الكائن"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlViewObject" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
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
                                                                <asp:Label ID="lblEngName" runat="server" Text="التوصيف إنجليزى" SkinID="Label_DefaultNormal"
                                                                    Width="90px" meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
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
                                                                <asp:Label ID="lblEngFieldView" runat="server" SkinID="Label_DefaultNormal" Text="اسم الانجليزي للحقل"
                                                                    Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlEngFieldView" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
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
                                                                <asp:Label ID="lblArbName" runat="server" Text="التوصيف عربى" SkinID="Label_DefaultNormal"
                                                                    meta:resourcekey="lblArbNameResource1" Width="90px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
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
                                                                <asp:Label ID="lblArbFieldView" runat="server" SkinID="Label_DefaultNormal" Text="اسم العربي للحقل"
                                                                    Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlArbFieldView" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
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
                                                                <asp:Label ID="lblFieldType" runat="server" SkinID="Label_DefaultNormal" Text="نوع الحقل"
                                                                    Width="90px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlFieldType" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
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
                                                                <asp:Label ID="lblDataLength" runat="server" SkinID="Label_DefaultNormal" Text="طول البيانات"
                                                                    Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="txtDataLength" runat="server" MinValue="0" SkinID="WebNumericEdit_Default">
                                                                    <ClientSideEvents KeyDown="Set_MAx_Value" />
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
                                                                <asp:Label ID="lblDataTypes" runat="server" SkinID="Label_DefaultNormal" Text="نوع البيانات"
                                                                    Width="90px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlDataTypes" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
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
                                                                <asp:Label ID="lblRank" runat="server" SkinID="Label_DefaultNormal" Text="الترتيب"
                                                                    Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <igtxt:WebNumericEdit ID="txtRank" runat="server" MaxValue="255" MinValue="0" SkinID="WebNumericEdit_Default">
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
                                                                <asp:Label ID="Label_Title1" runat="server" Text="بيانات الحقول الإضافية للجدول"
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
                                                <td style="height: 100%; vertical-align: top" colspan="3">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <igtbl:UltraWebGrid    Browser="UpLevel"  ID="uwgOtherFields" runat="server" EnableAppStyling="False" Height="100%"
                                                                    Width="100%" SkinID="Default" meta:resourcekey="uwgForNationalityResource1">
                                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                                                        HeaderClickActionDefault="SortSingle" Name="uwgForNationality" RowHeightDefault="18px"
                                                                        RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                                        AutoGenerateColumns="False" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
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
                                                                        <ClientSideEvents AfterRowActivateHandler="uwgOtherFields_AfterRowActivateHandlerNew"
                                                                            BeforeRowActivateHandler="uwgOtherFields_BeforeRowActivateHandlerNew" />
                                                                    </DisplayLayout>
                                                                    <Bands>
                                                                        <igtbl:UltraGridBand>
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
                                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="ID" HeaderText="ID" Hidden="True"
                                                                                    Key="ID" Width="0px">
                                                                                    <Header Caption="ID">
                                                                                    </Header>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="EngName" HeaderText="English name"
                                                                                    Key="EngName" Width="23%">
                                                                                    <Header Caption="English name">
                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="ArbName" HeaderText="Arabic name"
                                                                                    Key="ArbName" Width="23%">
                                                                                    <Header Caption="Arabic name">
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                                    </Footer>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="FieldType" HeaderText="Field Type"
                                                                                    Key="FieldType" Width="17%">
                                                                                    <Header Caption="Field Type">
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                                    </Footer>
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Rank" HeaderText="Rank" Key="Rank"
                                                                                    Width="8%" NullText="0">
                                                                                    <Header Caption="Rank">
                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                                    </Footer>
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="DataType" HeaderText="Data Type"
                                                                                    Key="DataType" Width="17%" Type="DropDownList">
                                                                                    <Header Caption="Data Type">
                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                                    </Footer>
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                </igtbl:UltraGridColumn>
                                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="DataLength" HeaderText="Data Length"
                                                                                    Key="DataLength" Width="12%">
                                                                                    <Header Caption="Data Length">
                                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                                    </Header>
                                                                                    <Footer>
                                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                                    </Footer>
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                </igtbl:UltraGridColumn>
                                                                            </Columns>
                                                                        </igtbl:UltraGridBand>
                                                                    </Bands>
                                                                </igtbl:UltraWebGrid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 100%" colspan="3">
                                                </td>
                                            </tr>
                                        </table>
                                    </igmisc:WebAsyncRefreshPanel>
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
