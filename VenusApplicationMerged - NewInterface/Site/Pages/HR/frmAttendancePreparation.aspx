<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAttendancePreparation.aspx.vb"
    Inherits="frmAttendancePreparation" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics35.WebUI.WebCombo.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Attendance Preparation</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script type="text/javascript" id="Infragistics">
        var IsEdit = true;
        function UWGEmployeesAttend_AfterCellUpdateHandler(gridName, cellId) {
            var ultraTab = igtab_getTabById("UltraWebTab1");
            var grid = igtbl_getGridById(gridName);
            var cell = igtbl_getCellById(cellId);
            var Row = igtbl_getRowById(cellId);
            var cellkey = cell.Column.Key;
            if (cellkey == 'PermitLate' || cellkey == 'NotpermitLate') {
                var TotalLateSum = 0;
                var PermitLateSum = 0;
                var NotpermitLateSum = 0;
                var HTotalLateVal = Row.getCellFromKey('TotalLate').getValue();
                var HPermitLateVal = Row.getCellFromKey('PermitLate').getValue();
                var HNotpermitLateVal = Row.getCellFromKey('NotpermitLate').getValue();
                var lblDescTotalLate = igtab_getElementById("lblDescTotalLate", ultraTab.element);
                var cTotalLateVal = Row.getCellFromKey('TotalLate');
                TotalLateSum = (HNotpermitLateVal - HPermitLateVal);
                if (TotalLateSum < 0) {
                    TotalLateSum = 0;
                }
                cTotalLateVal.setValue(ConvertToNumber(TotalLateSum));
                TotalLateSum = (ConvertToNumber(lblDescTotalLate.innerText) - HTotalLateVal);
                if (TotalLateSum < 0) {
                    TotalLateSum = 0;
                }
                TotalLateSum = TotalLateSum + (HNotpermitLateVal - HPermitLateVal);
                if (TotalLateSum < 0) {
                    TotalLateSum = 0;
                }
                lblDescTotalLate.innerText = TotalLateSum;
            }
            else if (cellkey == 'IsAbsent' || cellkey == 'IsVacation') {
                var TotalVac = ConvertToNumber(igtab_getElementById("lblDescTotalVacation", ultraTab.element).innerText);
                var TotalAbsent = ConvertToNumber(igtab_getElementById("lblDescTotalAbsent", ultraTab.element).innerText);
                var HTotalVac = Row.getCellFromKey('IsVacation').getValue();
                var HTotalAbsent = Row.getCellFromKey('IsAbsent').getValue();
                if ( HTotalAbsent == true) {
                    ++TotalAbsent;
                    
                }
                else if ( HTotalAbsent == false) {
                    --TotalAbsent;
                    
                }
                igtab_getElementById("lblDescTotalAbsent", ultraTab.element).innerText = TotalAbsent;
            }
        }
        function CloseMe() {
            parent.CloseIt("");
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmAttendancePreparation" runat="server">
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
        <%--        <igcmbo:WebCombo ID="cmbVacationsTypesID" runat="server" BackColor="White" BorderColor="Silver"
            BorderStyle="None" BorderWidth="0px" ForeColor="Black" ParentBandIndex="0" ParentColumnIndex="-1"
            ParentWebGridId="uwgPaymentDetails" SelBackColor="DarkBlue" SelForeColor="White"
            ValueList="1" Version="4.00" Width="150px" meta:resourcekey="cmbVacationsTypesIDResource1">
            <ExpandEffects ShadowColor="LightGray" />
            <Rows>
                <igtbl:UltraGridRow Height="">
                    <Cells>
                        <igtbl:UltraGridCell Key="0" Text="Select your choise">
                        </igtbl:UltraGridCell>
                        <igtbl:UltraGridCell Key="EngName">
                        </igtbl:UltraGridCell>
                        <igtbl:UltraGridCell Key="ArbName">
                        </igtbl:UltraGridCell>
                    </Cells>
                </igtbl:UltraGridRow>
            </Rows>
            <Columns>
                <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Int32" Hidden="True"
                    Key="ID" meta:resourceKey="IDRecourcekey">
                    <Header Caption="ID">
                    </Header>
                </igtbl:UltraGridColumn>
                <igtbl:UltraGridColumn BaseColumnName="EngName" Key="EngName" meta:resourceKey="EngNameRecourcekey">
                    <Header>
                        <RowLayoutColumnInfo OriginX="1" />
                    </Header>
                    <Footer>
                        <RowLayoutColumnInfo OriginX="1" />
                    </Footer>
                </igtbl:UltraGridColumn>
                <igtbl:UltraGridColumn BaseColumnName="ArbName" Hidden="True" Key="ArbName" meta:resourceKey="ArbNameRecourcekey">
                    <Header>
                        <RowLayoutColumnInfo OriginX="2" />
                    </Header>
                    <Footer>
                        <RowLayoutColumnInfo OriginX="2" />
                    </Footer>
                </igtbl:UltraGridColumn>
            </Columns>
            <DropDownLayout AutoGenerateColumns="False" BorderCollapse="NotSet" ColHeadersVisible="No"
                ColWidthDefault="100%" DropdownWidth="250px" RowHeightDefault="20px" RowSelectors="No"
                Version="4.00">
                <FrameStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                    Cursor="Default" Font-Names="Tahoma" Font-Size="8pt" Height="130px" Width="250px">
                </FrameStyle>
                <HeaderStyle BackColor="Transparent" BorderStyle="None">
                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                </HeaderStyle>
                <RowStyle BackColor="Transparent" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                    <Padding Left="3px" />
                    <BorderDetails WidthLeft="0px" WidthTop="0px" />
                </RowStyle>
                <SelectedRowStyle BackColor="#3399FF" BorderColor="Silver" ForeColor="White" />
            </DropDownLayout>
        </igcmbo:WebCombo>
        <igcmbo:WebCombo ID="cmbProjectID" runat="server" BackColor="White" BorderColor="Silver"
            BorderStyle="None" BorderWidth="0px" ForeColor="Black" ParentBandIndex="0" ParentColumnIndex="-1"
            ParentWebGridId="uwgPaymentDetails" SelBackColor="DarkBlue" SelForeColor="White"
            ValueList="1" Version="4.00" Width="150px" meta:resourcekey="cmbProjectIDResource1">
            <ExpandEffects ShadowColor="LightGray" />
            <Rows>
                <igtbl:UltraGridRow Height="">
                    <Cells>
                        <igtbl:UltraGridCell Key="0" Text="Select your choise">
                        </igtbl:UltraGridCell>
                        <igtbl:UltraGridCell Key="EngName">
                        </igtbl:UltraGridCell>
                        <igtbl:UltraGridCell Key="ArbName">
                        </igtbl:UltraGridCell>
                    </Cells>
                </igtbl:UltraGridRow>
            </Rows>
            <Columns>
                <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Int32" Hidden="True"
                    Key="ID" meta:resourceKey="IDRecourcekey">
                    <Header Caption="ID">
                    </Header>
                </igtbl:UltraGridColumn>
                <igtbl:UltraGridColumn BaseColumnName="EngName" Key="EngName" meta:resourceKey="EngNameRecourcekey">
                    <Header>
                        <RowLayoutColumnInfo OriginX="1" />
                    </Header>
                    <Footer>
                        <RowLayoutColumnInfo OriginX="1" />
                    </Footer>
                </igtbl:UltraGridColumn>
                <igtbl:UltraGridColumn BaseColumnName="ArbName" Hidden="True" Key="ArbName" meta:resourceKey="ArbNameRecourcekey">
                    <Header>
                        <RowLayoutColumnInfo OriginX="2" />
                    </Header>
                    <Footer>
                        <RowLayoutColumnInfo OriginX="2" />
                    </Footer>
                </igtbl:UltraGridColumn>
            </Columns>
            <DropDownLayout AutoGenerateColumns="False" BorderCollapse="NotSet" ColHeadersVisible="No"
                ColWidthDefault="110px" DropdownWidth="110px" RowHeightDefault="20px" RowSelectors="No"
                Version="4.00">
                <FrameStyle BackColor="Transparent" BorderStyle="None" BorderWidth="2px" Cursor="Default"
                    Font-Names="Verdana" Font-Size="10pt" Height="130px" Width="110px">
                </FrameStyle>
                <HeaderStyle BackColor="Transparent" BorderStyle="None">
                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                </HeaderStyle>
                <RowStyle BackColor="White" BorderColor="Transparent" BorderStyle="Solid" BorderWidth="1px">
                    <BorderDetails WidthLeft="0px" WidthTop="0px" />
                </RowStyle>
                <SelectedRowStyle BackColor="DarkBlue" ForeColor="White" />
            </DropDownLayout>
        </igcmbo:WebCombo>--%>
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
                                            <td style="height: 18px">
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
                                                            <igtxt:WebImageButton ID="btnDelete" runat="server" AutoSubmit="False" Height="18px"
                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnDeleteResource1">
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
                                                            <asp:Label ID="lblMSG" runat="server" meta:resourcekey="lblMSGResource1" SkinID="Label_WarningBold"
                                                                Width="100%"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 18px">
                                                <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                    <tr>
                                                        <td style="width: 5px">
                                                        </td>
                                                        <td style="width: 40px;">
                                                            <asp:Label ID="lblUser" runat="server" SkinID="Label_CopyRightsBold" Text="Code"
                                                                meta:resourcekey="lblUserResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 50px;">
                                                            <asp:Label ID="lblDescEmployeeCode" runat="server" SkinID="Label_CopyRightsNormal"
                                                                meta:resourcekey="lblDescEmployeeCodeResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 40px;">
                                                            <asp:Label ID="lblName" runat="server" SkinID="Label_CopyRightsBold" Text="Name"
                                                                meta:resourcekey="lblNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 150px;">
                                                            <asp:Label ID="lblDescEnglishName" runat="server" SkinID="Label_CopyRightsNormal"
                                                                meta:resourcekey="lblDescEnglishNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 70px;">
                                                            <asp:Label ID="lblTotalLate" runat="server" SkinID="Label_CopyRightsBold" Text="Total Late"
                                                                meta:resourcekey="lblTotalLateResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 50px; text-align: center;">
                                                            <asp:Label ID="lblDescTotalLate" runat="server" SkinID="Label_CopyRightsNormal" Text="0"
                                                                meta:resourcekey="lblDescTotalLateResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 70px;">
                                                            <asp:Label ID="lblNotPermitLate" runat="server" SkinID="Label_CopyRightsBold" Text="Total Not Permit Late"
                                                                meta:resourcekey="lblTotalNotPermitLateResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 50px; text-align: center;">
                                                            <asp:Label ID="lblDescTotalNotPermitLate" runat="server" SkinID="Label_CopyRightsNormal" Text="0"
                                                                meta:resourcekey="lblDescNotPermitLateResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 80px;">
                                                            <asp:Label ID="lblTotalOvertime" runat="server" SkinID="Label_CopyRightsBold" Text="Total Overtime"
                                                                meta:resourcekey="lblTotalOvertimeResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 50px; text-align: center;">
                                                            <asp:Label ID="lblDescTotalOvertime" runat="server" SkinID="Label_CopyRightsNormal"
                                                                Text="0" meta:resourcekey="lblDescTotalOvertimeResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 80px;">
                                                            <asp:Label ID="lblTotalAbsent" runat="server" SkinID="Label_CopyRightsBold" Text="Total Absent"
                                                                meta:resourcekey="lblTotalAbsentResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 50px; text-align: center;">
                                                            <asp:Label ID="lblDescTotalAbsent" runat="server" Text="0" SkinID="Label_CopyRightsNormal"
                                                                meta:resourcekey="lblDescTotalAbsentResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="lblTotalVacation" runat="server" SkinID="Label_CopyRightsBold" Text="Total Vacation"
                                                                meta:resourcekey="lblTotalVacationResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 50px; text-align: center;">
                                                            <asp:Label ID="lblDescTotalVacation" runat="server" Text="0" SkinID="Label_CopyRightsNormal"
                                                                meta:resourcekey="lblDescTotalVacationResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: middle">
                                                            <asp:Label ID="Label6" runat="server" meta:resourcekey="Label6Resource1" SkinID="Label_DefaultBold"
                                                                Text="Current contract information"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="UWGEmployeesAttend" runat="server" EnableAppStyling="True"
                                                    Height="370px" SkinID="Default" Width="100%" meta:resourcekey="UWGEmployeesAttendResource1">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                                        AllowUpdateDefault="Yes" AutoGenerateColumns="False" BorderCollapseDefault="Separate"
                                                        HeaderClickActionDefault="SortSingle" Name="uwgForNationality" RowHeightDefault="24px"
                                                        RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                                                        CellClickActionDefault="RowSelect">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="Tahoma" Font-Size="7pt" Height="370px" Width="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents AfterCellUpdateHandler="UWGEmployeesAttend_AfterCellUpdateHandler" />
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
                                                        <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="7pt"
                                                            Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="False">
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
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Int32" Hidden="True"
                                                                    Key="ID" meta:resourcekey="UltraGridColumnResource1">
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="EmployeeID" DataType="System.Int32" Hidden="True"
                                                                    Key="EmployeeID" meta:resourcekey="UltraGridColumnResource2">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="GAttendDate" Key="GAttendDate" Width="10%"
                                                                    DataType="System.DateTime" AllowUpdate="No" Format="dd/MM/yyyy" Hidden="True"
                                                                    meta:resourcekey="UltraGridColumnResource3">
                                                                    <Header Caption="Greg Date">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="HAttendDate" Key="HAttendDate" Width="10%"
                                                                    AllowUpdate="No" Hidden="True" meta:resourcekey="UltraGridColumnResource4">
                                                                    <Header Caption="Hijri Date">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="TotalLate" DataType="System.Double" Key="TotalLate"
                                                                    Width="10%" AllowUpdate="No" meta:resourcekey="UltraGridColumnResource5" Format="'0'###,###,###.##">
                                                                    <Header Caption="Total Late">
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
                                                                </igtbl:UltraGridColumn>                                                                <igtbl:UltraGridColumn BaseColumnName="NotpermitLate" DataType="System.Double" Key="NotpermitLate"
                                                                    Width="8%" AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource6" Format="'0'###,###,##.##">
                                                                    <Header Caption="Late (H)">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="PermitLate" DataType="System.Double" Key="PermitLate"
                                                                    Width="12%" AllowUpdate="No" Hidden="true" meta:resourcekey="UltraGridColumnResource7"
                                                                    Format="###,###,###.##">
                                                                    <Header Caption="Permit Late (H)">
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Overtime" DataType="System.Double" Key="Overtime"
                                                                    Width="10%" AllowUpdate="No" meta:resourcekey="UltraGridColumnResource8" Format="'0'###,###,###.##">
                                                                    <Header Caption="Overtime (H)">
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="HolidayHours" DataType="System.Double" Key="HolidayHours"
                                                                    Width="10%" AllowUpdate="No" meta:resourcekey="UltraGridColumnResource9" Format="###,###,###.##">
                                                                    <Header Caption="Holiday (H)">
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="IsAbsent" DataType="System.Boolean" Key="IsAbsent"
                                                                    Width="8%" Type="CheckBox" AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource10">
                                                                    <Header Caption="Is Absent">
                                                                        <RowLayoutColumnInfo OriginX="9" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="9" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="IsVacation" DataType="System.Boolean" Key="IsVacation"
                                                                    Width="10%" Type="CheckBox" AllowUpdate="No" meta:resourcekey="UltraGridColumnResource11">
                                                                    <Header Caption="Is Vacation">
                                                                        <RowLayoutColumnInfo OriginX="10" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="10" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="LeavingType" Key="LeavingType" Width="15%"
                                                                    Type="DropDownList" AllowUpdate="No" meta:resourcekey="UltraGridColumnResource12">
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Vacation Type">
                                                                        <RowLayoutColumnInfo OriginX="11" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="11" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Notes" FieldLen="255" Key="Notes" Width="15%"
                                                                    AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource13">
                                                                    <Header Title="Notes" Caption="Notes">
                                                                        <RowLayoutColumnInfo OriginX="12" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="12" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="RegComputerID" DataType="System.Int32" Hidden="True"
                                                                    Key="RegComputerID" meta:resourcekey="UltraGridColumnResource14">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="13"></RowLayoutColumnInfo>
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="13"></RowLayoutColumnInfo>
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                            <FilterOptions EmptyString="" AllString="" NonEmptyString="">
                                                                <FilterDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px"
                                                                    Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="SteelBlue" Width="200px"
                                                                    CustomRules="overflow:auto;">
                                                                    <Padding Left="2px"></Padding>
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle ForeColor="White" BackColor="#151C55">
                                                                </FilterHighlightRowStyle>
                                                            </FilterOptions>
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                        </igtbl:UltraGridBand>
                                                        <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource2">
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Int32" Hidden="True"
                                                                    Key="ID" meta:resourcekey="UltraGridColumnResource16">
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="TrnsID" DataType="System.Int32" Hidden="True"
                                                                    Key="TrnsID" meta:resourcekey="UltraGridColumnResource17">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ProjectID" Key="ProjectID" Width="150px" Type="DropDownList"
                                                                    AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource18">
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Project">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Checkin" Key="Checkin" Width="100px" AllowUpdate="No"
                                                                    Format="HH:mm:ss" DataType="System.DateTime" meta:resourcekey="UltraGridColumnResource19">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Header Caption="Check In">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Checkout" Key="Checkout" Width="100px" AllowUpdate="No"
                                                                    Format="HH:mm:ss" DataType="System.DateTime" meta:resourcekey="UltraGridColumnResource20">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Header Caption="Greg Date">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="TotalTime" DataType="System.Double" Key="TotalTime"
                                                                    Width="100px" AllowUpdate="Yes" Hidden="True" meta:resourcekey="UltraGridColumnResource21"
                                                                    Format="###,###,###.##">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Header Caption="Total Project Times">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Overtime" DataType="System.Double" Key="Overtime"
                                                                    Width="100px" AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource22"
                                                                    Format="###,###,###.##">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Header Caption="Project Overtime">
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="HolidayHours" DataType="System.Double" Key="HolidayHours"
                                                                    Width="100px" AllowUpdate="Yes" meta:resourcekey="UltraGridColumnResource23"
                                                                    Format="###,###,###.##">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
                                                                    <Header Caption="Project HolidayHours">
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
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
    </div>
    </form>
</body>
</html>
