<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCalendar.aspx.vb" Inherits="frmCalendar"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Calendar</title>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_Calendar.js" type="text/javascript"></script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmCalendar" runat="server">
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display: none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N"
                                    meta:resourcekey="ImageButton1Resource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:Label ID="Label_TSP3" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                            </td>
                            <td style="width: 120px">
                                <asp:ImageButton ID="ImageButton_SaveN" Width="16px" Height="16px" runat="server"
                                    CommandArgument="SaveNew" SkinID="HrSaveN_Command" meta:resourcekey="ImageButton_SaveNResource1" />
                                <asp:LinkButton ID="LinkButton_SaveN" CommandArgument="SaveNew" runat="server" Text="حفظ"
                                    meta:resourcekey="LinkButton_SaveN"></asp:LinkButton>
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 40px">
                                &nbsp;
                            </td>
                            <td style="width: 24px">
                                <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server"
                                    SkinID="HrDelete_Command" CommandArgument="Delete" meta:resourcekey="ImageButton_DeleteResource1" />
                                <asp:LinkButton ID="LinkButton_Delete" runat="server" CommandArgument="Delete" Text="حذف"
                                    meta:resourcekey="LinkButton_Delete"></asp:LinkButton>
                            </td>
                            <td style="width: 80px">
                                &nbsp;
                            </td>
                            <td style="width: 40px">
                                &nbsp;
                            </td>
                            <td style="width: 24px">
                                <asp:Label ID="Label_TSP2" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                    SkinID="HrPrint_Command" CommandArgument="Print" meta:resourcekey="ImageButton_PrintResource1" />
                            </td>
                            <td>
                                <asp:LinkButton ID="LinkButton_Print" runat="server" CommandArgument="Print" Text="طباعة"
                                    meta:resourcekey="LinkButton_Print"></asp:LinkButton>
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
                                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" meta:resourcekey="WebAsyncRefreshPanel1Resource1">
                                        <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                            cellspacing="0">
                                            <tr>
                                                <td style="height: 10px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea">
                                                            </td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblEngName" runat="server" Text="تحديد فئة موظفين" SkinID="Label_DefaultNormal"
                                                                    Width="100px" meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlEmployeeClass" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal"
                                                                    meta:resourcekey="ddlEmployeeClassResource1">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 16px; vertical-align: top">
                                                    <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                        cellspacing="6">
                                                        <tr>
                                                            <td style="vertical-align: bottom" class="style1">
                                                                <asp:Label ID="Label_Title1" runat="server" Text="طريقة إحتساب الدوام" SkinID="Label_DefaultBold"
                                                                    meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 16px; vertical-align: top">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 16px; vertical-align: top" colspan="2">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td colspan="3">
                                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                                    cellspacing="6">
                                                                    <tr>
                                                                        <td style="vertical-align: bottom">
                                                                            <asp:Label ID="Label1" runat="server" Text="التقويم أو أيام الأسبوع" SkinID="Label_DefaultBold"
                                                                                meta:resourcekey="Label1Resource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 1%;">
                                                            </td>
                                                            <td style="width: 32%;">
                                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                                    cellspacing="6">
                                                                    <tr>
                                                                        <td style="vertical-align: bottom">
                                                                            <asp:Label ID="Label2" runat="server" Text="الرجاء تحديد الدوام" SkinID="Label_DefaultBold"
                                                                                meta:resourcekey="Label2Resource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 1%;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%;">
                                                            </td>
                                                            <td style="width: 25%;">
                                                                <igsch:WebCalendar ID="cldDateSelector" runat="server" Height="16px" Width="64%"
                                                                    Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black;
                                                                    background-color: White;" meta:resourcekey="cldDateSelectorResource1">
                                                                    <Layout>
                                                                        <CalendarStyle Height="16px" Width="100%">
                                                                        </CalendarStyle>
                                                                    </Layout>
                                                                    <AutoPostBack ValueChanged="True" VisibleMonthChanged="True" />
                                                                </igsch:WebCalendar>
                                                            </td>
                                                            <td style="width: 15%; text-align: center;">
                                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgDays" runat="server" EnableAppStyling="False" Height="180px"
                                                                    meta:resourcekey="uwgBranchesResource1" SkinID="Default" Width="95%">
                                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgBranches"
                                                                        RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                        StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                        Version="4.00" ViewType="OutlookGroupBy">
                                                                        <ClientSideEvents RowSelectorClickHandler="uwgDays_RowSelectorClickHandler" AfterRowActivateHandler="uwgDays_AfterRowActivateHandler" />
                                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="180px"
                                                                            Width="95%">
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
                                                                        <igtbl:UltraGridBand AllowSorting="No" meta:resourcekey="UltraGridBandResource1">
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
                                                                                <igtbl:UltraGridColumn Width="100%" meta:resourcekey="UltraGridColumnResource1">
                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                    </CellStyle>
                                                                                    <Header Caption="Column 0">
                                                                                    </Header>
                                                                                </igtbl:UltraGridColumn>
                                                                            </Columns>
                                                                        </igtbl:UltraGridBand>
                                                                    </Bands>
                                                                </igtbl:UltraWebGrid>
                                                            </td>
                                                            <td style="width: 1%;">
                                                            </td>
                                                            <td style="width: 32%;">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:RadioButtonList ID="rbtnSelectedDateType" runat="server" Width="100%" Style="font-family: Tahoma;
                                                                                font-size: 8pt; font-weight: Normal; color: Black" meta:resourcekey="rbtnSelectedDateTypeResource1">
                                                                                <asp:ListItem meta:resourcekey="ListItemResource1" Text="الوقت الإفتراضي" Value="0"></asp:ListItem>
                                                                                <asp:ListItem meta:resourcekey="ListItemResource2" Text="لا يوجد عمل" Value="1"></asp:ListItem>
                                                                                <asp:ListItem meta:resourcekey="ListItemResource3" Text="تحديد الوقت" Value="2"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <asp:Label ID="lbl0" runat="server" SkinID="Label_DefaultNormal" Text="من :" meta:resourcekey="lbl0Resource1"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 50%;">
                                                                            <asp:Label ID="lbl1" runat="server" SkinID="Label_DefaultNormal" Text="إلى :" meta:resourcekey="lbl1Resource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit1" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" Enabled="False" SkinID="WebDateTimeEdit_Default" meta:resourcekey="WebDateTimeEdit1Resource1">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit1_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit2" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" Enabled="False" SkinID="WebDateTimeEdit_Default" meta:resourcekey="WebDateTimeEdit2Resource1">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit3" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" Enabled="False" SkinID="WebDateTimeEdit_Default" meta:resourcekey="WebDateTimeEdit3Resource1">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit4" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" Enabled="False" SkinID="WebDateTimeEdit_Default" meta:resourcekey="WebDateTimeEdit4Resource1">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit5" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" Enabled="False" SkinID="WebDateTimeEdit_Default" meta:resourcekey="WebDateTimeEdit5Resource1">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit6" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" Enabled="False" SkinID="WebDateTimeEdit_Default" meta:resourcekey="WebDateTimeEdit6Resource1">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit7" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" Enabled="False" SkinID="WebDateTimeEdit_Default" meta:resourcekey="WebDateTimeEdit7Resource1">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit8" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" Enabled="False" SkinID="WebDateTimeEdit_Default" meta:resourcekey="WebDateTimeEdit8Resource1">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit9" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" Enabled="False" SkinID="WebDateTimeEdit_Default" meta:resourcekey="WebDateTimeEdit9Resource1">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                        <td style="width: 50%;">
                                                                            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit10" runat="server" DisplayModeFormat="hh:mm tt"
                                                                                EditModeFormat="hh:mm tt" Enabled="False" SkinID="WebDateTimeEdit_Default" meta:resourcekey="WebDateTimeEdit10Resource1">
                                                                                <ClientSideEvents ValueChange="frmCalWebDateTimeEdit2_ValueChange" />
                                                                            </igtxt:WebDateTimeEdit>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 1%;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 100%" colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                    </igmisc:WebAsyncRefreshPanel>
                                </ContentTemplate>
                            </igtab:Tab>
                        </Tabs>
                    </igtab:UltraWebTab>
                    <asp:LinkButton ID="LinkButton1" meta:resourcekey="LinkButton1Resource1" runat="server" SkinID="LinkButton_DefaultBold">تخصيص التقويم</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
