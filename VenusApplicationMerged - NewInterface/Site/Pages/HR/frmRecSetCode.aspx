<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRecSetCode.aspx.vb" Inherits="frmRecSetCode"
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
    <title>* Venus Payroll * ~Set Code</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
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
    <style type="text/css">
        .style1
        {
            width: 77px;
        }
    </style>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmRecSetCode" runat="server">
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
        <table align="right" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display:none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N"
                                    meta:resourcekey="ImageButton1Resource1" />
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 120px">
                                <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                    SkinID="HrSave_Command" CommandArgument="Save" meta:resourcekey="ImageButton_SaveResource1"
                                    OnClientClick="SaveOtherFieldsData();" />
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
                                &nbsp;
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                    SkinID="HrPrint_Command" CommandArgument="Print" meta:resourcekey="ImageButton_PrintResource1" />
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td style="width: 40px">
                                &nbsp;
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 10px">
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 30%">
                                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 80px">
                                &nbsp;
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
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblVacancy" runat="server" meta:resourcekey="lblVacancy" SkinID="Label_DefaultNormal"
                                                                Text="فرص التوظيف"></asp:Label>
                                                            <asp:Label ID="Label_Star2" runat="server" meta:resourcekey="Label_Star1Resource1"
                                                                Style="color: #FF0000" Text="*"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlVacancy" runat="server" meta:resourcekey="ddl_MainDepartmentResource1"
                                                                SkinID="DropDownList_LargNormal">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <igtxt:WebImageButton ID="btnGetVacationDefault" runat="server" AutoSubmit="true"
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
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 30px; vertical-align: top;">
                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                    cellspacing="6">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_Title1Resource1" SkinID="Label_DefaultBold"
                                                                Text="الرجاء تحديد الاكواد للمتقدمين"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgInterviewsDetail" runat="server" EnableAppStyling="False"
                                                                Height="250px" Width="100%" SkinID="Default" meta:resourcekey="uwgForNationalityResource1">
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                                                    HeaderClickActionDefault="SortSingle" Name="uwgForNationality" RowHeightDefault="18px"
                                                                    RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                                    AutoGenerateColumns="False" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                    Version="4.00" ViewType="OutlookGroupBy">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="250px"
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
                                                                    <ClientSideEvents />
                                                                </DisplayLayout>
                                                                <Bands>
                                                                    <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
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
                                                                            <igtbl:UltraGridColumn BaseColumnName="Applicant_ID" Hidden="True" Key="Applicant_ID"
                                                                                AllowResize="Fixed" DataType="System.Int32" Width="140px" meta:resourcekey="UltraGridColumnResource5">
                                                                                <Header Caption="">
                                                                                </Header>
                                                                                <CellButtonStyle HorizontalAlign="Left">
                                                                                </CellButtonStyle>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <SelectedCellStyle HorizontalAlign="Center">
                                                                                </SelectedCellStyle>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Applicant_Name" Key="Applicant_Name"
                                                                                Width="50%" AllowResize="Fixed" meta:resourcekey="UltraGridColumnResource2">
                                                                                <Header Caption="اسم المتقدم">
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
                                                                            <igtbl:UltraGridColumn BaseColumnName="ExpectedCode" Width="30%" meta:resourcekey="UltraGridColumnResource3">
                                                                                <Header Caption="الكود المتوقع">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn Type="Button" Width="20%" NullText="Return" meta:resourcekey="UltraGridColumnResource4">
                                                                                <Header Caption="تراجع">
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <SelectedCellStyle HorizontalAlign="Center">
                                                                                </SelectedCellStyle>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="InterView_ID" DataType="System.Int32" Hidden="True"
                                                                                Key="InterView_ID" meta:resourcekey="UltraGridColumnResource9">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn DataType="System.Int32" Hidden="True" Key="OpenVacancy_ID"
                                                                                meta:resourcekey="UltraGridColumnResource10">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
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
                                                                        <RowTemplateStyle BackColor="Transparent" BorderColor="Transparent" BorderStyle="Ridge">
                                                                            <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                                        </RowTemplateStyle>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                            </igtbl:UltraWebGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%">
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
