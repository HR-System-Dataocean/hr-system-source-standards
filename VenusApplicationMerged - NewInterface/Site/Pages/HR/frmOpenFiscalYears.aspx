<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmOpenFiscalYears.aspx.vb"
    Inherits="frmOpenFiscalYears" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Fiscal Years Periods</title>
    <script language="javascript" src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
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
    <form id="frmOpenFiscalYears" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
        <asp:HiddenField ID="txtCode" runat="server" />
        <asp:HiddenField ID="txtFormPermission" runat="server" />
        <asp:HiddenField ID="txtRowIndex" runat="server" />
        <asp:HiddenField ID="txtRowID" runat="server" />
        <igtxt:WebDateTimeEdit ID="WebDateTimeEdit1" runat="server" meta:resourcekey="WebDateTimeEdit1Resource1">
        </igtxt:WebDateTimeEdit>
        <igtxt:WebDateTimeEdit ID="WebDateTimeEdit2" runat="server" meta:resourcekey="WebDateTimeEdit2Resource1">
        </igtxt:WebDateTimeEdit>
        <asp:TextBox ID="txtCode2" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
            AutoPostBack="True" meta:resourcekey="txtCodeResource1"></asp:TextBox>
        <asp:RadioButton ID="RadioButton_IsFormal" runat="server" Checked="True" GroupName="Ch1"
            Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black"
            Text="إعدادات عادية" meta:resourcekey="RadioButton_IsFormalResource1" />
        <asp:RadioButton ID="RadioButton_UnFormal" runat="server" GroupName="Ch1" meta:resourcekey="RadioButton_UnFormalResource1"
            Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black"
            Text="إعدادات مخصصة" />
        <asp:Label ID="lblGuidFisical" runat="server" meta:resourcekey="lblGuidFisicalResource1"
            SkinID="Label_DefaultNormal" Text="السنة المالية النموذج"></asp:Label>
        <asp:DropDownList ID="ddlGuidFisical" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal">
        </asp:DropDownList>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" meta:resourcekey="WebAsyncRefreshPanel1Resource1">
            <table align="center" style="width: 100%;">
                <tr>
                    <td style="width: 100%; height: 60px; vertical-align: top">
                        <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                            <tr>
                                <td style="display: none">
                                    <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                        SkinID="HrSave_Command" meta:resourcekey="ImageButton_SaveResource1" CommandArgument="Save"
                                        Visible="False" OnClientClick="SaveOtherFieldsData();" />
                                </td>
                                <td style="width: 120px">
                                    <asp:ImageButton ID="ImageButton_SaveN" Width="16px" Height="16px" runat="server"
                                        CommandArgument="SaveNew" SkinID="HrSaveN_Command" meta:resourcekey="ImageButton_SaveNResource1"
                                        Visible="False" OnClientClick="SaveOtherFieldsData();" />
                                    <asp:LinkButton ID="LinkButton_SaveN" runat="server" Text="حفظ مع جديد" meta:resourcekey="LinkButton_SaveNResource1"
                                        CommandArgument="SaveNew" Visible="False" OnClientClick="SaveOtherFieldsData();"></asp:LinkButton>
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                        meta:resourcekey="ImageButton_NewResource1" CommandArgument="New" Visible="false" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server"
                                        SkinID="HrDelete_Command" meta:resourcekey="ImageButton_DeleteResource1" CommandArgument="Delete" />
                                </td>
                                <td style="width: 40px">
                                    <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                        SkinID="HrPrint_Command" meta:resourcekey="ImageButton_PrintResource1" CommandArgument="Print" />
                                </td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Properties" Width="16px" Height="16px" runat="server"
                                        SkinID="HrProperties_Command" meta:resourcekey="ImageButton_PropertiesResource1"
                                        CommandArgument="Property" />
                                    <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="خصائص" meta:resourcekey="LinkButton_PropertiesResource1"
                                        CommandArgument="Property"></asp:LinkButton>
                                </td>
                                <td style="width: 80px">
                                    <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                        SkinID="HrRemarks_Command" meta:resourcekey="ImageButton_RemarksResource1" CommandArgument="Remarks" />
                                    <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="ملاحظات" meta:resourcekey="LinkButton_RemarksResource1"
                                        CommandArgument="Remarks"></asp:LinkButton>
                                </td>
                                <td style="width: 40px">
                                    <asp:Label ID="Label_TSP2" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Last" Width="16px" Height="16px" runat="server"
                                        SkinID="HrLast_Command" meta:resourcekey="ImageButton_LastResource1" CommandArgument="Last" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Next" Width="16px" Height="16px" runat="server"
                                        SkinID="HrNext_Command" meta:resourcekey="ImageButton_NextResource1" CommandArgument="Next" />
                                </td>
                                <td style="width: 10px">
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_Back" Width="16px" Height="16px" runat="server"
                                        SkinID="HrBack_Command" meta:resourcekey="ImageButton_BackResource1" CommandArgument="Previous" />
                                </td>
                                <td style="width: 24px">
                                    <asp:ImageButton ID="ImageButton_First" Width="16px" Height="16px" runat="server"
                                        SkinID="HrFirest_Command" meta:resourcekey="ImageButton_FirstResource1" CommandArgument="First" />
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
                                                            <asp:Label ID="lblRegDate" runat="server" Text="سجل فى" SkinID="Label_CopyRightsBold"
                                                                meta:resourcekey="lblRegDateResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                            <asp:Label ID="lblRegDateValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegDateValueResource1"></asp:Label>
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
                                                            <asp:Label ID="lblRegUser" runat="server" Text="سجل بواسطة" SkinID="Label_CopyRightsBold"
                                                                meta:resourcekey="lblRegUserResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                            <asp:Label ID="lblRegUserValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegUserValueResource1"></asp:Label>
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
                                                            <asp:Label ID="lblCancelDate" runat="server" Text="تاريخ الالغاء" SkinID="Label_CopyRightsBold"
                                                                meta:resourcekey="lblCancelDateResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                            <asp:Label ID="lblCancelDateValue" runat="server" SkinID="Label_CopyRightsNormal"
                                                                meta:resourcekey="lblCancelDateValueResource1"></asp:Label>
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
                                                <td style="height: 10px;" colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 16px; width: 47%; vertical-align: top;">
                                                    <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td class="SeparArea">
                                                            </td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblFiscalYear" runat="server" meta:resourcekey="lblFiscalYearResource1"
                                                                    SkinID="Label_DefaultNormal" Text="السنة المالية"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:DropDownList ID="ddlFiscalYear" runat="server" AutoPostBack="True" meta:resourcekey="ddlFiscalYearResource1"
                                                                    SkinID="DropDownList_LargNormal">
                                                                </asp:DropDownList>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <asp:Label ID="lblGabNotify" runat="server" meta:resourcekey="lblGabNotifyResource1"
                                                        SkinID="Label_WarningBold"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table cellspacing="6" style="border-bottom: 1px solid black; width: 100%; vertical-align: bottom;
                                                        height: 30px;">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Label ID="Label_Title1" runat="server" meta:resourceKey="Label_Title1Resource1"
                                                                    SkinID="Label_DefaultBold" Text="الفترات المالية المختلفة للسنة المالية"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table cellspacing="6" style="width: 100%; vertical-align: bottom; height: 30px;">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <igtxt:WebImageButton ID="btnAddPeriod" runat="server" ClickOnEnterKey="False" ClickOnSpaceKey="False"
                                                                    Height="22px" ImageDirectory="/ig_common/20071CLR20/Styles/Office2007Blue/WebImageButton/"
                                                                    meta:resourcekey="btnAddPeriodResource1" Text="فتح السنوات المالية" UseBrowserDefaults="False">
                                                                    <RoundedCorners HeightOfBottomEdge="0" MaxHeight="22" MaxWidth="200" RenderingType="FileImages"
                                                                        WidthOfRightEdge="5" />
                                                                    <ClientSideEvents Click="btnAddPeriod_Click" />
                                                                    <Appearance>
                                                                        <Image Url="./Img/abook_add_1.gif" />
                                                                    </Appearance>
                                                                </igtxt:WebImageButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%; vertical-align: top" colspan="3">
                                                    <igtbl:UltraWebGrid    Browser="UpLevel"  ID="uwgFiscalYearsPeriods" runat="server" EnableAppStyling="False"
                                                        Height="280px" Width="325px" SkinID="Default" meta:resourcekey="uwgEndOfServiceRulesResource1">
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowAddNewDefault="Yes" AllowColumnMovingDefault="OnServer"
                                                            AllowDeleteDefault="Yes" AllowSortingDefault="OnClient" AllowUpdateDefault="Yes"
                                                            BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgEndOfServiceRules"
                                                            RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                            StationaryMargins="Header" AutoGenerateColumns="False" StationaryMarginsOutlookGroupBy="True"
                                                            TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                            <FilterOptionsDefault>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px" Height="300px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                    BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px">
                                                                    <Padding Left="2px" />
                                                                </FilterOperandDropDownStyle>
                                                            </FilterOptionsDefault>
                                                            <AddNewBox>
                                                                <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                </BoxStyle>
                                                            </AddNewBox>
                                                            <Pager MinimumPagesForDisplay="2">
                                                                <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                </PagerStyle>
                                                            </Pager>
                                                            <GroupByBox Hidden="True">
                                                                <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                                </BoxStyle>
                                                            </GroupByBox>
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="280px"
                                                                Width="325px">
                                                            </FrameStyle>
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
                                                            <ActivationObject BorderColor="" BorderWidth="">
                                                            </ActivationObject>
                                                        </DisplayLayout>
                                                        <Bands>
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" meta:resourcekey="UltraGridColumnResource1">
                                                                        <Header Caption="ID">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="FiscalYearID" Hidden="True" Key="FiscalYearID"
                                                                        meta:resourcekey="UltraGridColumnResource2">
                                                                        <Header Caption="FiscalYearID">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="EngName" Key="EngName" meta:resourcekey="UltraGridColumnResource3"
                                                                        Width="20%">
                                                                        <Header Caption="English Name">
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ArbName" Key="ArbName" meta:resourcekey="UltraGridColumnResource4"
                                                                        Width="20%">
                                                                        <Header Caption="Arabic Name">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="FromDate" DataType="System.DateTime" EditorControlID="WebDateTimeEdit1"
                                                                        Key="FromDate" meta:resourcekey="UltraGridColumnResource5" Type="Custom" Width="15%">
                                                                        <Header Caption="From Date">
                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="5" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ToDate" DataType="System.DateTime" EditorControlID="WebDateTimeEdit2"
                                                                        Key="ToDate" meta:resourcekey="UltraGridColumnResource6" Type="Custom" Width="15%">
                                                                        <Header Caption="To Date">
                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>

                                                                    <igtbl:UltraGridColumn BaseColumnName="FiscalYearsName" Hidden="True" Key="FiscalYearsName"
                                                                        meta:resourcekey="UltraGridColumnResource7">
                                                                        <Header Caption="FiscalYearsName">
                                                                            <RowLayoutColumnInfo OriginX="7" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="7" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="RegUserID" Hidden="True" Key="RegUserID" meta:resourcekey="UltraGridColumnResource8">
                                                                        <Header Caption="RegUserID">
                                                                            <RowLayoutColumnInfo OriginX="6" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="6" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="RegComputerID" Hidden="True" Key="RegComputerID"
                                                                        meta:resourcekey="UltraGridColumnResource9">
                                                                        <Header Caption="RegComputerID">
                                                                            <RowLayoutColumnInfo OriginX="9" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="9" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="RegDate" Hidden="True" Key="RegDate" meta:resourcekey="UltraGridColumnResource10">
                                                                        <Header Caption="RegDate">
                                                                            <RowLayoutColumnInfo OriginX="8" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="8" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="CancelDate" Hidden="True" Key="CancelDate"
                                                                        meta:resourcekey="UltraGridColumnResource11">
                                                                        <Header Caption="CancelDate">
                                                                            <RowLayoutColumnInfo OriginX="10" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="10" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="HFromDate" Key="HFromDate" meta:resourcekey="UltraGridColumnResource12"
                                                                        Width="15%">
                                                                        <Header Caption="From Date">
                                                                            <RowLayoutColumnInfo OriginX="12" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="12" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="HToDate" Key="HToDate" meta:resourcekey="UltraGridColumnResource13"
                                                                        Width="15%">
                                                                        <Header Caption="To Date">
                                                                            <RowLayoutColumnInfo OriginX="11" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="11" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    
                                                                     <igtbl:UltraGridColumn BaseColumnName="PrepareFromDate" DataType="System.DateTime" EditorControlID="WebDateTimeEdit2"
                                                                        Key="PrepareFromDate" meta:resourcekey="PrepareFromDateResource6" Type="Custom" Width="15%">
                                                                        <Header Caption="Prepare FromDate">
                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="4" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="PrepareToDate" DataType="System.DateTime" EditorControlID="WebDateTimeEdit2"
                                                                               Key="PrepareToDate" meta:resourcekey="PPrepareToDateResource6" Type="Custom" Width="15%">
                                                                               <Header Caption="Prepare ToDate">
                                                                                   <RowLayoutColumnInfo OriginX="4" />
                                                                               </Header>
                                                                               <Footer>
                                                                                   <RowLayoutColumnInfo OriginX="4" />
                                                                               </Footer>
                                                                           </igtbl:UltraGridColumn>
                                                                </Columns>
                                                                <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                    </FilterHighlightRowStyle>
                                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                        Font-Size="11px" Width="200px">
                                                                        <Padding Left="2px" />
                                                                    </FilterDropDownStyle>
                                                                </FilterOptions>
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                            </igtbl:UltraGridBand>
                                                        </Bands>
                                                    </igtbl:UltraWebGrid>
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
